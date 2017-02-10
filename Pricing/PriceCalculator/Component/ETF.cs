using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PriceCalculator.Utility;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using log4net;
using System.Reflection;
using System.Timers;
using SourceCell;
using System.Drawing;
using PriceLib;

namespace PriceCalculator.Component
{
    public class ETF : IDisposable
    {
        private const string SQL = "SELECT D.* , E.PRUNIT * E.TRADEUNIT AS PRSHARES FROM ETFForBrian..tblETFDaily D LEFT JOIN ETFForBrian..tblETF E ON D.ETFCode =E.ETFCode WHERE D.ETFCODE='{0}' AND D.DATADATE='{1}' ";

        #region Variable
        private ILog m_Log = LogManager.GetLogger(typeof(ETF));
        private Timer m_Timer;

        private bool m_disposed = false;
        private Dictionary<string, decimal> m_Variations = new Dictionary<string, decimal>();
        private CalculationType m_CType = CalculationType.None;
        private decimal m_IIV;
        private decimal m_CalcFundAssetValue;
        private double m_DecimalPlaces;
        #endregion

        #region Cell
        private TextCell c_ETFCode;
        private TextCell c_IIV;
        [JsonIgnore]
        public TextCell cETFCode
        {
            get
            {
                if (c_ETFCode== null)
                {
                    c_ETFCode = new TextCell() { CellType = TextCell.TextType.String, HasBorder= true, Value = ETFCode, TextAlignment =  DevAge.Drawing.ContentAlignment.MiddleLeft, BackColor= Color.LightGray};
                }
                return c_ETFCode;
            }
        }
        [JsonIgnore]
        public TextCell cIIV
        {
            get
            {
                if (c_IIV== null)
                {
                    c_IIV = new TextCell() { CellType= TextCell.TextType.Decimal, HasBorder= true, Value= IIV, TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight};
                    int decimalplaces = -1;
                    if (int.TryParse(Util.INI["SYS"]["DECIMALPLACES"], out decimalplaces))
                    {
                        c_IIV.Format = "#.".PadRight(decimalplaces + 2, '0');
                    }
                }
                return c_IIV;
            }
        }
        #endregion

        #region Property
        private Dictionary<string, Composition> STKs { get { return Util.STK[CollectionKey.ETFCode, ETFCode]; } }
        private Dictionary<string, Composition> FUTs { get { return Util.FUT[CollectionKey.ETFCode, ETFCode]; } }
        private Dictionary<string, Composition> FUNDs { get { return Util.FUND[CollectionKey.ETFCode, ETFCode]; } }
        private Dictionary<string, FX> FXs { get { return Util.FXRates[ETFCode]; } }
        private Dictionary<string, List<Asset>> Cashs { get { return Util.CASH[AssetKey.ETFCode, ETFCode]; } }
        private Dictionary<string, List<Asset>> Margins { get { return Util.MARGIN[AssetKey.ETFCode, ETFCode]; } }
        private Dictionary<string, List<Asset>> Forwards { get { return Util.FORWARD[AssetKey.ETFCode, ETFCode]; } }

        public string ETFCode { get; private set; }
        public double FundAssetValue { get; private set; }
        public double PublicShares { get; private set; }
        public int PRShares { get; private set; }
        public decimal NAV { get; private set; }
        public int CashDiff { get; private set; }
        public CalculationType CType
        {
            get
            {
                if (m_CType == CalculationType.None)
                {
                    if (STKs != null && STKs.Values.Sum(e => e.TotalUnits) == 0)
                    {                        
                        m_CType = CalculationType.PCFUnits;
                    }
                    else { m_CType = CalculationType.TotalUnits; }
                    Util.Info(m_Log, "ETF.CType", ETFCode + " CalculationType:" + m_CType);
                }
                return m_CType;
            }
        }        
        public decimal IIV
        {
            get { return m_IIV; }
            private set
            {
                if (value == m_IIV) { return; }
                m_IIV = value;                
            }
        }
        public bool Sended { get; set; }
        #endregion

        public ETF(string etfcode, DateTime datadate)
        {
            Util.Info(m_Log, nameof(ETF), $"Create {etfcode}");
            ETFCode = etfcode;
            //if (!string.IsNullOrEmpty(Util.INI["SYS"]["DECIMALPLACES"]))
            //{             
            int deci = Util.INI["SYS"]["DECIMALPLACES"].ToInt(MktPrice.NULLINT);
                m_DecimalPlaces =deci == MktPrice.NULLINT? MktPrice.NULLINT: Math.Pow(10.0, deci);
                //if (m_DecimalPlaces < 0D)
                //{
                //    m_DecimalPlaces = Math.Pow(10D, 10D);
                //}
            //}

            DataTable dt = Util.SQL.DoQuery(string.Format(SQL, etfcode, datadate.ToString(Util.DATESTR)));
            if (dt == null) { return; }
            foreach (DataRow row in dt.Rows)
            {
                FundAssetValue = Convert.ToDouble(row["FUNDASSETVALUE"]);
                m_CalcFundAssetValue = (decimal)FundAssetValue;
                PublicShares = Convert.ToDouble(row["PUBLICSHARES"]);
                PRShares = Convert.ToInt32(row["PRSHARES"]);
                NAV = PublicShares <=0?0M:(decimal)(FundAssetValue / PublicShares);
                IIV = NAV;
                CashDiff = Convert.ToInt32(row["CASHDIFF"]);
                break;
            }            
            _Start(Util.Union(CollectionKey.ETFCode, ETFCode));

            m_Timer = new Timer(5000);
            m_Timer.Elapsed += (sender, e) =>
            {
                if (!Sended && Util.Calculator.SendToChannel)
                {
                    Util.PublishToMiddle(ETFCode, IIV);
                }
                Sended = false;
            };
            m_Timer.Start();            
        }

        #region Public
        public bool Calc(string id, decimal variation)
        {
            lock (m_Variations)
            {
                decimal pre = 0M;
                decimal preNAV = 0M;
                if (!m_Variations.ContainsKey(id))
                {
                    m_Variations.Add(id, variation);
                }
                else
                {
                    if (m_Variations[id] == variation) { return false; }
                    pre = m_Variations[id];
                    m_Variations[id] = variation;
                }
                //preNAV = IIV - pre;
                //IIV += (variation - pre);                
                preNAV = (m_CalcFundAssetValue - pre) / ((decimal)PublicShares);
                m_CalcFundAssetValue += variation - pre;
                IIV = m_CalcFundAssetValue / (decimal)PublicShares;

                //if (Math.Floor(preNAV * m_DecimalPlaces) == Math.Floor(IIV * m_DecimalPlaces))
                //{
                //    return false;
                //}
                if (m_DecimalPlaces == MktPrice.NULLINT)
                {
                    return true;
                }
                else
                {
                    return !(preNAV != IIV && Math.Floor(preNAV * (decimal)m_DecimalPlaces) == Math.Floor(IIV * (decimal)m_DecimalPlaces));
                }
            }
            //return true;
        }
        public void AssignFundAssetValue( double assetValue)
        {
            m_CalcFundAssetValue += (decimal)(assetValue - FundAssetValue);
            FundAssetValue = assetValue;
        }
        public void AssignMktPrice(CollectionType cType,string pid,decimal price )
        {
            Composition composition = null;
            switch (cType)
            {
                case CollectionType.Stock:
                    composition = STKs[pid];
                    break;

                case CollectionType.Future:
                    composition = FUTs[pid];
                    break;

                case CollectionType.Fund:
                    composition = FUNDs[pid];
                    break;
            }
            if (composition != null)
            {
                composition.AssignedMP = price;
            }

        }
        public void AssignYstPrice(CollectionType cType, string pid, decimal price)
        {
            Composition composition = null;
            switch (cType)
            {
                case CollectionType.Stock:
                    composition = STKs[pid];
                    break;

                case CollectionType.Future:
                    composition = FUTs[pid];
                    break;

                case CollectionType.Fund:
                    composition = FUNDs[pid];
                    break;
            }
            if (composition != null)
            {
                composition.AssignedYP = price;
            }
        }
        public void LockYP(DateTime expired)
        {
            if (STKs != null)
            {
                foreach (var item in STKs.Values)
                {
                    ((STK)item).LockYP(expired);
                }
            }
            if (FUTs != null)
            {
                foreach (var item in FUTs.Values)
                {
                    ((FUT)item).LockYP(expired);
                }
            }
            if (FUNDs != null)
            {
                foreach (var item in FUNDs.Values)
                {
                    ((FUND)item).LockYP(expired);
                }
            }            
        }
        public new string ToString()
        {
            string re = JsonConvert.SerializeObject(this);
            JObject obj = new JObject();
            obj["ETF"] = re;            
            if (STKs != null)
            {
                JArray stkarray = new JArray();

                foreach (var stk in STKs.Values)
                {
                    stkarray.Add(JsonConvert.SerializeObject(stk));
                }
                obj["STK"] = stkarray;
            }
            if (FUTs != null)
            {
                JArray futarray = new JArray();
                foreach (var fut in FUTs.Values)
                {
                    futarray.Add(JsonConvert.SerializeObject(fut));
                }
                obj["FUT"] = futarray;
            }

            if (FUNDs != null)
            {
                JArray fundarray = new JArray();
                foreach (var fund in FUNDs.Values)
                {
                    fundarray.Add(JsonConvert.SerializeObject(fund));
                }
                obj["FUND"] = fundarray;
            }
            if (FXs != null)
            {
                JArray fxarray = new JArray();
                foreach (var fx in FXs.Values)
                {
                    fxarray.Add(JsonConvert.SerializeObject(fx));
                }
                obj["FXRate"] = fxarray;
            }
            if (Cashs!= null)
            {
                JArray casharray = new JArray();
                foreach (var cashs in Cashs.Values)
                {
                    foreach (Asset cash in cashs)
                    {
                        casharray.Add(JsonConvert.SerializeObject(cash));
                    } 
                }
                obj["Cash"] = casharray;
            }
            if (Margins != null)
            {
                JArray marginarray = new JArray();
                foreach (var margins in Margins.Values)
                {
                    foreach (var margin in margins)
                    {
                        marginarray.Add(JsonConvert.SerializeObject(margin));
                    }                    
                }
                obj["Margin"] = marginarray;
            }
            if (Forwards != null)
            {
                JArray forwardarray = new JArray();
                foreach (var forwards in Forwards.Values)
                {
                    foreach (var forward in forwards)
                    {
                        forwardarray.Add(JsonConvert.SerializeObject(forward));
                    }                    
                }
                obj["Forwod"] = forwardarray;
            }
            return obj.ToString();
        }
        #endregion

        #region Private
        private void _Start(IEnumerable<Composition> items)
        {
            Util.Info(m_Log, nameof(ETF._Start), $"{ETFCode} Start");
            if (items == null) { return; }
            foreach (var item in items)
            {
                item.Start(this);
            }
        }
        private void _Stop(IEnumerable<Composition> items)
        {
            Util.Info(m_Log, nameof(ETF._Stop), $"{ETFCode} Stop");
            if (items == null) { return; }
            foreach (var item in items)
            {
                item.Stop();
            }
        }
        #endregion

        #region IDisposable 成員

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="IsDisposing"></param>
        protected void Dispose(bool IsDisposing)
        {
            if (m_disposed) return;

            if (IsDisposing) { DoDispose(); }
            m_disposed = true;
        }
        /// <summary>
        /// Do something when disposing
        /// </summary>
        protected virtual void DoDispose()
        {
            _Stop(Util.Union(CollectionKey.ETFCode, ETFCode));
            m_Timer.Stop();            
        }
        #endregion
    }
}