using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriceCalculator.Utility;
using log4net;
using System.Reflection;
using System.Data;
using PriceLib.Redis;

namespace PriceCalculator.Component
{
    public class FXCollection
    {
        public event EventHandler OnFXRateUpdate;

        #region Variable
        private ILog m_Log = LogManager.GetLogger(typeof(FXCollection));
        private Dictionary<string, Dictionary<string, FX>> m_FXs = new Dictionary<string, Dictionary<string, FX>>();
        private frmFXRate m_frmFXRate = new frmFXRate();
        #endregion

        #region Property
        public frmFXRate frmFX { get { return m_frmFXRate; } }
        public double this[string ETFCode, string BaseCurncy, string QuotedCurncy]
        {
            get
            {
                if (!m_FXs.ContainsKey(ETFCode)) { return 1D; }
                if (BaseCurncy == QuotedCurncy) { return 1D; }
                
                if (BaseCurncy == "USD")
                {
                    Util.Debug(m_Log, nameof(FXCollection), $"{ETFCode} Quoted:{QuotedCurncy} Rate:{ m_FXs[ETFCode][QuotedCurncy].Rate}");
                    return m_FXs[ETFCode][QuotedCurncy].Rate;
                }
                if (!m_FXs[ETFCode].ContainsKey(BaseCurncy)) { return 1D; }
                var b = m_FXs[ETFCode][BaseCurncy].Rate;
                var q = m_FXs[ETFCode][QuotedCurncy].Rate;
                var re = q / b;
                Util.Debug(m_Log, nameof(FXCollection), $"{ETFCode} Base:{b} Quoted:{q} Rate:{re}");
                return re;
            }
        }
        public Dictionary<string, FX> this[string ETFCode]
        {
            get
            {
                if (m_FXs.ContainsKey(ETFCode)) { return m_FXs[ETFCode]; }
                return null;
            }
        }
        #endregion

        public FXCollection()
        {
            Util.Info(m_Log, nameof(FXCollection), "Create FXCollection");
            DataTable dt = Util.SQL.DoQuery($"SELECT ETFCODE,BASE, QUOTED,FXRATE AS RATE FROM TBLFXRATE WHERE DATADATE='{DateTime.Now.ToString("yyyy/MM/dd")}' ");
            if (dt == null || dt.Rows.Count <= 0) { return; }

            foreach (DataRow row in dt.Rows)
            {
                FX fx = new FX(row["ETFCode"].ToString(), row["Quoted"].ToString(), row["Rate"].ToString(), row["Base"].ToString());

                //以ETFCode為Key的List
                if (!m_FXs.ContainsKey(fx.ETFCode))
                {
                    m_FXs.Add(fx.ETFCode, new Dictionary<string, FX>());
                }
                //以ETFCode為Key的List, 第二層Key為Quoted
                if (!m_FXs[fx.ETFCode].ContainsKey(fx.Quoted))
                {
                    m_FXs[fx.ETFCode].Add(fx.Quoted, fx);
                }
            }
            Util.MiddleREDIS.Subscribe("FX", "*");
        }

        #region Public
        public List<FX> GetList(string etfcode)
        {
            if (!m_FXs.ContainsKey(etfcode)) { return null; }
            return m_FXs[etfcode].Values.ToList();
        }
        public void WriteQuoteSetting()
        {
            List<string> cmpn = new List<string>(), tpft = new List<string>();
            foreach (var item in m_FXs)
            {
                var CMPNQuoteList = item.Value.Where(e => e.Value.QuoteFromCMPN).Select(e => e.Value.Quoted);
                var TPFTQuoteList = item.Value.Where(e => e.Value.QuoteFromTPFT).Select(e => e.Value.Quoted);
                if (CMPNQuoteList.Count() > 0)
                {
                    cmpn.Add($"{item.Key}|{string.Join(",", CMPNQuoteList)}");
                }
                if (TPFTQuoteList.Count() > 0)
                {
                    tpft.Add($"{item.Key}|{string.Join(",", TPFTQuoteList)}");
                }
            }
            Util.INI["SYS"]["CMPNQUOTE"] = string.Join(";", cmpn);
            Util.INI["SYS"]["TPFTQUOTE"] = string.Join(";", tpft);
            Util.WriteConfig();

        }
        #endregion

        public void RaiseFXRateUpdate(FX fx)
        {
            if (OnFXRateUpdate != null && fx != null)
            {
                OnFXRateUpdate(fx, EventArgs.Empty);
            }
        }
    }
}