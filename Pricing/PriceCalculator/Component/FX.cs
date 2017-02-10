using log4net;
using PriceCalculator.Utility;
using PriceLib.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalculator.Component
{
    public class FX : NotifyableClass
    {
        #region Variable
        private const string CMPN = " CMPN Curncy";
        private const string TPFT = " TPFT Curncy";
        private ILog m_Log = LogManager.GetLogger(typeof(FX));
        private bool m_QuoteFromCMPN;
        private bool m_QuoteFromTPFT;
        private double m_Rate;
        #endregion

        #region Property            
        public string ETFCode { get; private set; }
        public string Base { get; private set; }
        public string Quoted { get; private set; }
        public string BLQuoteStr
        {
            get
            {
                string str = m_QuoteFromCMPN ? " CMPN Curncy" : (m_QuoteFromTPFT ? " TPFT Curncy" : string.Empty);
                if (!string.IsNullOrEmpty(str))
                {
                    return $"{Quoted}{str}";
                }
                return string.Empty;
            }
        }
        public bool QuoteFromCMPN
        {
            get { return m_QuoteFromCMPN; }
            set
            {
                if (value == m_QuoteFromCMPN) { return; }
                m_QuoteFromCMPN = value;
                if (m_QuoteFromCMPN && m_QuoteFromTPFT)
                {
                    QuoteFromTPFT = false;
                }
                OnPropertyChanged(nameof(QuoteFromCMPN));
                if (Util.FXRates != null)
                {
                    Util.FXRates.WriteQuoteSetting();
                }
            }
        }
        public bool QuoteFromTPFT
        {
            get { return m_QuoteFromTPFT; }
            set
            {
                if (value == m_QuoteFromTPFT) { return; }
                m_QuoteFromTPFT = value;
                if (m_QuoteFromTPFT && m_QuoteFromCMPN)
                {
                    QuoteFromCMPN = false;
                }
                OnPropertyChanged(nameof(QuoteFromTPFT));
                if (Util.FXRates != null)
                {
                    Util.FXRates.WriteQuoteSetting();
                }
            }
        }
        public double Rate
        {
            get { return m_Rate; }
            set
            {
                if (value == m_Rate) { return; }
                m_Rate = value;
                if (Util.FXRates != null) { Util.FXRates.RaiseFXRateUpdate(this); }
                OnPropertyChanged(nameof(Rate));
            }
        }
        #endregion

        public FX()
        {
            Util.MiddleREDIS.OnValueUpdated += new RedisPublishLib.OnValueUpdateDelegate(MiddleREDIS_OnChannelUpdate);
        }
        public FX(string etfcode, byte[] quote, byte[] rate, string baseCurncy = "USD")
            : this(etfcode, Encoding.UTF8.GetString(quote), Encoding.UTF8.GetString(quote), baseCurncy)
        { }
        public FX(string etfcode, string quote, string rate, string baseCurncy = "USD")
            : this()
        {
            ETFCode = etfcode;
            Base = baseCurncy;
            Quoted = quote;
            Rate = rate.ToDouble(0D);

            string[] cmpn = Util.INI["SYS"]["CMPNQUOTE"].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            string[] tpft = Util.INI["SYS"]["TPFTQUOTE"].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            _LoadQuoteSetting(cmpn, ref m_QuoteFromCMPN);
            _LoadQuoteSetting(tpft, ref m_QuoteFromTPFT);

        }

        #region Delegate
        private void MiddleREDIS_OnChannelUpdate(string channel, string item, string value)
        {
            if (item.Trim() == BLQuoteStr)
            {
                Rate = value.ToDouble(0D);
            }
        }
        #endregion

        #region Private
        private void _LoadQuoteSetting(string[] Quote, ref bool QuoteSetting)
        {
            for (int i = 0; i < Quote.Length; i++)
            {
                string[] curncy = Quote[i].Split('|');
                if ((curncy.Length == 2) && (curncy[0] == ETFCode))
                {
                    QuoteSetting = curncy[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Contains(Quoted);
                    break;
                }
            }
        }
        #endregion
    }
}
