using SKCOMLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Util.Extension.Class;

namespace PriceProcessor.Capital
{
    public class CapitalProcessor : Processor
    {
        #region Const
        const int OK = 0;
        const int SK_WARNING_TS_READY = 2003;
        const int SK_SUBJECT_CONNECTION_CONNECTED = 3001;
        const int SK_SUBJECT_CONNECT_DISCONNECT = 3002;
        #endregion

        #region Event
        public delegate void OnOverseaProductDelegate(string Exchange, string PID);
        public delegate void OnPriceChangeDelegate(string PID, string TickName, Price price);
        public delegate void OnErrorDelegate(string MethodName, System.Exception ex);

        public event OnOverseaProductDelegate OnOverseaProduct;
        public event OnPriceChangeDelegate OnPriceChange;
        public event OnErrorDelegate OnError;
        #endregion

        #region Variable
        private bool m_isCapitalConnect = false;
        private bool m_isCapitalInit = false;

        private System.Timers.Timer m_Timer;
        private SKCenterLib m_CenterLib;
        private SKOSQuoteLib m_OSQuoteLib;
        private static Dictionary<string, MappingInfo> m_FutureTickName;
        private Dictionary<string, Product> m_Products = new Dictionary<string, Product>();
        #endregion

        #region Property
        public bool isCapitalConnect
        {
            get { return m_isCapitalConnect; }
            private set
            {
                if (value == m_isCapitalConnect) { return; }
                m_isCapitalConnect = value;
                NotificationCenter.Instance.Post(nameof(isCapitalConnect));
            }
        }
        public bool isCapitalInit
        {
            get { return m_isCapitalInit; }
            private set
            {
                m_isCapitalInit = value;
                NotificationCenter.Instance.Post(nameof(isCapitalInit));
            }
        }
        public Dictionary<string, Product> Products { get { return m_Products; } }
        public Price this[string StockNo]
        {
            get
            {
                if (!m_Products.ContainsKey(StockNo)) { return null; }
                return m_Products[StockNo].Price;
            }
            set { m_Products[StockNo].Price = value; }
        }
        public Price this[short StockIdx]
        {
            get
            {
                Product product = m_Products.Values.FirstOrDefault(e => e.Price != null && e.Price.StockIdx == StockIdx);                
                return product?.Price;
            }
            set
            {
                Product p = m_Products.Values.FirstOrDefault(e => e.Price != null && e.Price.StockIdx == StockIdx);
                if (p != null) { p.Price = value; }
            }
        }
        #endregion

        public CapitalProcessor()
            : base()
        {
            m_CenterLib = new SKCenterLib();
            m_OSQuoteLib = new SKOSQuoteLib();
            m_OSQuoteLib.OnConnect += OnConnect;
            m_OSQuoteLib.OnNotifyQuote += OnQuoteUpdate;
            m_OSQuoteLib.OnNotifyTicks += OnNotifyTicks;
            m_OSQuoteLib.OnNotifyBest5 += OnNotifyBest5;
            m_OSQuoteLib.OnOverseaProducts += OnOverseaProducts;
            m_OSQuoteLib.OnNotifyServerTime += OnNotifyServerTime;
            
            m_Timer = new System.Timers.Timer(60000);
            m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
        }


        #region Delegate
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            int code;
            if ((code = m_OSQuoteLib.SKOSQuoteLib_RequestServerTime()) != OK)
            {
                OnError?.Invoke("RequestServerTime", new CapitalException(m_CenterLib, code));
            }
            //Functions.SKOSQuoteLib_RequestServerTime();
        }

        #endregion

        #region CallBack Function
        private void OnConnect(int nKind, int nErrorCode)
        {
            if (nKind == SK_SUBJECT_CONNECTION_CONNECTED)
            {
                isCapitalConnect = true;
                m_Timer.Enabled = true;
            }
            else if (nKind == SK_SUBJECT_CONNECT_DISCONNECT)
            {
                isCapitalConnect = false;
                m_Timer.Enabled = false;
            }
        }
        private void OnQuoteUpdate(short nStockIdx)
        {
            SKFOREIGN Foreign = new SKFOREIGN();
            int code;
            if ((code = m_OSQuoteLib.SKOSQuoteLib_GetStockByIndex(nStockIdx, ref Foreign)) != OK)
            {
                OnError?.Invoke("GetStockByIndex", new CapitalException(m_CenterLib, code));
                return;
            }
            
            short stockidx = Foreign.sStockIdx;
            string stockNo = Foreign.bstrStockNo;

            if (this[stockNo] != null)
            {
                this[stockNo].Update(stockidx, Foreign);
            }
            else
            {
                this[stockNo] = new Price(this, stockidx, Foreign);
            }
            OnPriceChange?.Invoke(stockNo, m_Products[stockNo].TickName, m_Products[stockNo].Price);
        }
        private void OnNotifyTicks(short sStockIdx, int nPtr, int nTime, int nClose, int nQty)
        {
            SKFOREIGNTICK tTick = new SKFOREIGNTICK();
            int code;
            if ((code = m_OSQuoteLib.SKOSQuoteLib_GetTick(sStockIdx, nPtr, ref tTick)) != OK)
            {
                OnError?.Invoke("GetTicks", new CapitalException(m_CenterLib, code));
                return;
            }            
            if (this[sStockIdx] != null)
            {
                this[sStockIdx].Update(sStockIdx, tTick);
            }
            else
            {
                this[sStockIdx] = new Price(this, sStockIdx, tTick);
            }
            if (OnPriceChange != null)
            {
                Product p = m_Products.Values.FirstOrDefault(e => e.Price != null && e.Price.StockIdx == sStockIdx);
                if (p != null) { OnPriceChange(p.StockNo, p.TickName, p.Price); }                
            }
        }
        private void OnNotifyBest5(short sStockIdx, int nBestBid1, int nBestBidQty1, int nBestBid2, int nBestBidQty2, int nBestBid3, int nBestBidQty3, int nBestBid4, int nBestBidQty4, int nBestBid5, int nBestBidQty5, int nBestAsk1, int nBestAskQty1, int nBestAsk2, int nBestAskQty2, int nBestAsk3, int nBestAskQty3, int nBestAsk4, int nBestAskQty4, int nBestAsk5, int nBestAskQty5)
        {            
            int[] BastBid = new int[] { nBestBid1, nBestBid2, nBestBid3, nBestBid4, nBestBid5 };
            int[] BastAsk = new int[] { nBestAsk1, nBestAsk2, nBestAsk3, nBestAsk4, nBestAsk5 };
            int[] BastBidQty = new int[] { nBestBidQty1, nBestBidQty2, nBestBidQty3, nBestBidQty4, nBestBidQty5 };
            int[] BastAskQty = new int[] { nBestAskQty1, nBestAskQty2, nBestAskQty3, nBestAskQty4, nBestAskQty5 };

            if (this[sStockIdx] != null)
            {
                this[sStockIdx].Update(sStockIdx, BastBid, BastAsk, BastBidQty, BastAskQty);
            }
            else
            {
                this[sStockIdx] = new Price(this, sStockIdx, BastBid, BastAsk, BastBidQty, BastAskQty);
            }
            if (OnPriceChange != null)
            {
                Product p = m_Products.Values.FirstOrDefault(e => e.Price != null && e.Price.StockIdx == sStockIdx);
                if (p != null) { OnPriceChange(p.StockNo, p.TickName, p.Price); }
            }
        }
        private void OnOverseaProducts(string strProducts)
        {
            Product p = new Product(strProducts, FutureTickName(strProducts));
            if (!m_Products.ContainsKey(p.StockNo) && !string.IsNullOrEmpty(p.TickName))
            {
                m_Products.Add(p.StockNo, p);
                OnOverseaProduct?.Invoke(p.Exchange, p.StockNo);
            }
        }
        private void OnNotifyServerTime(short sHour, short sMinute, short sSecond) { }
        #endregion

        #region Public
        public void Start(string LoginID, string Password, out string Msg)
        {
            Msg = string.Empty;
            int code;
            if ((code = m_CenterLib.SKCenterLib_Login(LoginID, Password)) != OK && code != SK_WARNING_TS_READY)
            {
                OnError?.Invoke("Login", new CapitalException(m_CenterLib, code));
            }
            if ((code = m_OSQuoteLib.SKOSQuoteLib_Initialize()) == OK)
            {
                m_isCapitalInit = true;
            }
            else if (code == SK_WARNING_TS_READY)
            {
                Msg = "元件已初始化過\r\n";
            }
            else
            {
                m_isCapitalInit = false;
                OnError?.Invoke("Initialize", new CapitalException(m_CenterLib, code));
                return;
            }
            if ((code = m_OSQuoteLib.SKOSQuoteLib_EnterMonitor()) != OK)
            {
                isCapitalConnect = false;
                OnError?.Invoke("EnterMonitor", new CapitalException(m_CenterLib, code));
                return;
            }
            NotificationCenter.Instance.Post("isCapitalInit");
            m_OSQuoteLib.SKOSQuoteLib_RequestOverseaProducts();
        }
        public void Stop()
        {
            int code;
            if (m_OSQuoteLib.SKOSQuoteLib_IsConnected() != OK) return;
            if ((code = m_OSQuoteLib.SKOSQuoteLib_LeaveMonitor()) != OK)
            {
                OnError?.Invoke("Disconnect", new CapitalException(m_CenterLib, code));
            }            
        }
        public void Subscribe(short pageno, Product product)
        {
            int code;
            if ((code = m_OSQuoteLib.SKOSQuoteLib_RequestStocks(ref pageno, product.SubStr)) != OK)
            {
                OnError?.Invoke("SubscribeStock", new CapitalException(m_CenterLib, code));
            }
            if ((code = m_OSQuoteLib.SKOSQuoteLib_RequestTicks(ref pageno, product.SubStr)) != OK)
            {                
                OnError?.Invoke("SubscribeTicks", new CapitalException(m_CenterLib, code));
            }            
            Request(product);
        }
        public void Subscribe(short pageno, string pid)
        {
            if (m_Products.ContainsKey(pid))
            {
                Subscribe(pageno, m_Products[pid]);
            }
        }
        public void Unsubscribe(short pageno, Product product)
        {
            int code;
            if ((code = m_OSQuoteLib.SKOSQuoteLib_RequestStocks(ref pageno, product.SubStr)) != OK)
            {
                OnError?.Invoke("UnsubscribeStock", new CapitalException(m_CenterLib, code));
            }
            if ((code = m_OSQuoteLib.SKOSQuoteLib_RequestTicks(ref pageno, product.SubStr)) != OK)
            {
                OnError?.Invoke("UnsubscribeTicks", new CapitalException(m_CenterLib, code));
            }
        }
        public void Unsubscribe(short pageno, string pid)
        {
            if (m_Products.ContainsKey(pid))
            {
                Unsubscribe(pageno, m_Products[pid]);
            }
        }
        public void Request( Product product)
        {
            if (this[product.StockNo] == null)
            {
                SKFOREIGN f = new SKFOREIGN();
                int code = m_OSQuoteLib.SKOSQuoteLib_GetStockByNo(product.SubStr, ref f);
                if (code != 0)
                {
                    this[product.StockNo] = new Price(this);
                }
                else
                {
                    this[product.StockNo] = new Price(this, f.sStockIdx, f);
                }
            }
            OnPriceChange?.Invoke(product.StockNo,product.TickName, this[product.StockNo]);
        }
        public void Request(string pid)
        {
            if (m_Products.ContainsKey(pid))
            {
                Request(m_Products[pid]);
            }
        }

        public static string TickName(string orderKey)
        {
            var tick = m_FutureTickName.Values.FirstOrDefault(e => e.OrderKey == orderKey);
            return tick == null ? string.Empty : tick.TickName;
        }
        
        //public Product GetProuct(string exchange, string orderHead)
        //{
        //    return Products.Values.FirstOrDefault(e => e.Exchange == exchange && e.OrderHead == orderHead);
        //}
        #endregion

        #region Private
        private MappingInfo FutureTickName(string Info)
        {
            string key;
            string[] infos = Info.Split(',');
            if (!infos[2].Contains("SPD"))
            {
                key = infos[0] + "," + (infos[2].Length > 4 ? (infos[2].Substring(0, infos[2].Length - 4)) : infos[2]);
            }
            else
            {
                string[] items = infos[2].Split(new string[] { "SPD" }, StringSplitOptions.None);
                key = $"{infos[0]},{items[0]}SPD";
            }
            

            return !m_FutureTickName.ContainsKey(key) ? null : m_FutureTickName[key];
        }
        #endregion

        protected override void InitTick()
        {
            base.InitTick();

            Config Config = new Config(PATH, NAME);
            SQLTool sql = new SQLTool(Config.GetSetting<string>("SQL", "IP"),
                Config.GetSetting<string>("SQL", "DB"),
                Config.GetSetting<string>("SQL", "ID"),
                Config.GetSetting<string>("SQL", "PASSWORD"));

            if (m_FutureTickName == null) { m_FutureTickName = new Dictionary<string, MappingInfo>(); }
            m_FutureTickName.Clear();
            DataTable dtFuture = sql.DoQuery("SELECT * FROM PRODUCTINFO WHERE PRODUCTTYPE='FUTURE'");
            if (dtFuture != null && dtFuture.Rows.Count > 0)
            {
                foreach (DataRow row in dtFuture.Rows)
                {
                    MappingInfo mi = new MappingInfo(row["EXCHANGE"], row["TICKNAME"], row["PRODUCTID"], row["ORDERHEAD"]);
                    if (!m_FutureTickName.ContainsKey(mi.Key))
                    {
                        m_FutureTickName.Add(mi.Key, mi);
                    }
                }
            }
        }
    }

    
}