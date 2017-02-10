using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using SKCOMLib;

namespace PriceLib.Capital
{
    public partial class OSCapitalLib : PriceLib
    {
        private class CapitalSubInfo
        {
            public int PageNo;
            public int Subscriptions;
        }

        private const int OK = 0;
        private const int SK_SUBJECT_CONNECT_DISCONNECT = 3002;
        private const int SK_SUBJECT_CONNECT_DISCONNECT_NORMAL = 10004;
        private const int SK_SUBJECT_CONNECTION_CONNECTED = 3001;
        private const int SK_WARNING_TS_READY = 2003;

        #region Event
        public event EventHandler OnStatusChange;
        #endregion

        #region Variable
        private SKCenterLib m_CenterLib;
        private SKOSQuoteLib m_OSQuoteLib;
        private PageCollection m_PageCollection;
        private StatusEnum m_Status;
        ///// <summary>
        ///// Level1 Key: pageno
        ///// Level2 Key: subitem
        ///// Level2 Value: Sub Count
        ///// </summary>
        //private Dictionary<int, Dictionary<string, int>> m_Subs;
        /// <summary>
        /// Key: StockId
        /// Value: Capital Price
        /// </summary>
        private Dictionary<short, CapitalPrice> m_CapitalPrices;
        private List<IndexInfo> m_Indexes = new List<IndexInfo>();
        private System.Timers.Timer m_RequestServerTime;
        #endregion

        #region Property
        public string User { get; private set; }
        public string Pwd { get; private set; }
        public StatusEnum Status
        {
            get { return m_Status; }
            private set
            {
                if (value == m_Status) { return; }
                m_Status = value;
                OnStatusChange?.Invoke(m_Status, EventArgs.Empty);
            }
        }
        #endregion
        static OSCapitalLib()
        {
            ResourceExtractor.ExtractResourceToFile("PriceLib.Dll.SKOSQuoteLib.dll", "SKOSQuoteLib.dll", true);
        }
        public OSCapitalLib()
            : base()
        {
            m_Log = LogManager.GetLogger(typeof(OSCapitalLib));
            m_PageCollection = new PageCollection();
            m_CapitalPrices = new Dictionary<short, CapitalPrice>();
            m_CenterLib = new SKCenterLib();
            m_OSQuoteLib = new SKOSQuoteLib();

            m_OSQuoteLib.OnConnect += OnQuoteConnect;
            m_OSQuoteLib.OnNotifyQuote += OnQuoteUpdate;
            m_OSQuoteLib.OnNotifyServerTime += OnNotifyServerTime;
            m_OSQuoteLib.OnOverseaProducts += OnOverseaProducts;

            m_RequestServerTime = new System.Timers.Timer(30000);
            m_RequestServerTime.Elapsed += new System.Timers.ElapsedEventHandler(RequestServerTime);
        }

        #region Delegate
        private void RequestServerTime(object sender, System.Timers.ElapsedEventArgs e)
        {
            int code = m_OSQuoteLib.SKOSQuoteLib_RequestServerTime();
            if (code != OK)
            {
                throw new CapitalException(m_CenterLib, code);
            }
        }

        public void OnQuoteConnect(int nKind, int nErrorCode)
        {
            if (nKind == SK_SUBJECT_CONNECTION_CONNECTED)
            {
                Status = StatusEnum.Connected;
                m_RequestServerTime.Start();
            }
            else if (nKind == SK_SUBJECT_CONNECT_DISCONNECT)
            {
                Status = StatusEnum.Disconnected;
                m_RequestServerTime.Stop();
                if (nErrorCode != SK_SUBJECT_CONNECT_DISCONNECT_NORMAL)
                {
                    Connect(User, Pwd);
                }
            }
            if (nErrorCode != OK)
            {
                throw new CapitalException(m_CenterLib, nErrorCode);
            }
        }
        public void OnQuoteUpdate(short nStockIdx)
        {
            SKFOREIGN Foreign = new SKFOREIGN();
            int code = m_OSQuoteLib.SKOSQuoteLib_GetStockByIndex(nStockIdx, ref Foreign);

            if (code != OK )
            {
                throw new CapitalException(m_CenterLib, code);
            }


            if (!m_CapitalPrices.ContainsKey(nStockIdx))
            {
                m_CapitalPrices.Add(nStockIdx, new CapitalPrice(nStockIdx));
            }
            m_CapitalPrices[nStockIdx].Update(nStockIdx, Foreign);
            RaiseMktPrice(m_CapitalPrices[nStockIdx].GetMktPrice());
        }
        public void OnNotifyServerTime(short sHour, short sMinute, short sSecond)
        {

        }
        private void OnOverseaProducts(string bstrValue)
        {
            IndexInfo item = IndexInfo.Create(bstrValue);
            if (item != null)
            {
                m_Indexes.Add(item);
            }
        }

        #endregion

        #region Public
        public void Connect(string user, string pwd)
        {
            User = user;
            Pwd = pwd;
            //Login
            int code = m_CenterLib.SKCenterLib_Login(User, Pwd);
            if (code != OK && code != SK_WARNING_TS_READY)
            {
                throw new CapitalException(m_CenterLib, code);
            }
            //Initialize
            code = m_OSQuoteLib.SKOSQuoteLib_Initialize();
            if (code == OK)
            {
                Status = StatusEnum.InitializeSuccess;
            }
            else if (code == SK_WARNING_TS_READY)
            {
                Status = StatusEnum.Initialized;
            }
            else
            {
                Status = StatusEnum.InitializeFail;
                throw new CapitalException(m_CenterLib, code);
            }
            //Monitor
            code = m_OSQuoteLib.SKOSQuoteLib_EnterMonitor();
            if (code != OK)
            {
                Status = StatusEnum.ConnectFail;
                throw new CapitalException(m_CenterLib, code);
            }

            Status = StatusEnum.Connected;
            //Get Products
            m_OSQuoteLib.SKOSQuoteLib_RequestOverseaProducts();
        }
        public void Disconnect()
        {
            int code = m_OSQuoteLib.SKOSQuoteLib_LeaveMonitor();
            if (code != OK)
            {
                throw new CapitalException(m_CenterLib, code);
            }
        }
        public void Subscribe(params string[] subitems)
        {
            lock (m_CapitalPrices)
            {
                foreach (var item in subitems)
                {
                    SubPage sub = m_PageCollection.GetSubPage(item);
                    short pageno = sub.PageNo;
                    int code = m_OSQuoteLib.SKOSQuoteLib_RequestStocks(ref pageno, sub.ToString());
                    if (code != OK) { throw new CapitalException(m_CenterLib, code); }
                    //系統給的PageNO
                    sub.PageNo = pageno;
                    //第一次訂, 有價就先丟
                    CapitalPrice price = m_CapitalPrices.Values.FirstOrDefault(e => e.Key == item);
                    if (price != null)
                    {
                        RaiseMktPrice(price.GetMktPrice());
                    }
                }
            }
        }
        public void Unsubscribe(params string[] subitems)
        {
            foreach (var item in subitems)
            {
                SubPage unsub = m_PageCollection.GetUnsubPage(item);
                if (unsub == null) { continue; }
                short pageno = unsub.PageNo;
                int code = m_OSQuoteLib.SKOSQuoteLib_RequestStocks(ref pageno, unsub.ToString());
                if (code != OK) { throw new CapitalException(m_CenterLib, code); }
            }
        }
        public void UnsubscribeAll()
        {
            foreach (var page in m_PageCollection.Pages)
            {
                short pageno = page.PageNo;
                page.Clear();

                int code = m_OSQuoteLib.SKOSQuoteLib_RequestStocks(ref pageno, page.ToString());
                if (code != OK) { throw new CapitalException(m_CenterLib, code); }
            }
            m_PageCollection = new PageCollection();
        }

        
        #endregion

        #region Private
        //private void _FindPageNo(out int pageno, out string substring, string subitem)
        //{
        //    substring = string.Empty;
        //    pageno = -1;
        //    //找有沒有訂過
        //    var v = (from keypair in m_Subs
        //             where keypair.Value.ContainsKey(subitem)
        //             select new CapitalSubInfo
        //             {
        //                 PageNo = keypair.Key,
        //                 Subscriptions = keypair.Value[subitem]
        //             }).FirstOrDefault();
        //    if (v == null)
        //    {
        //        for (int i = 0; i < m_Subs.Count; i++)
        //        {
        //            if (m_Subs[i].Count < 50)
        //            {
        //                m_Subs[i].Add(subitem, 0);
        //                pageno = i;
        //                break;
        //            }
        //        }
        //        if (pageno == -1)
        //        {
        //            m_Subs.Add(m_Subs.Count, new Dictionary<string, int>());
        //            m_Subs[m_Subs.Count - 1].Add(subitem, 0);
        //            pageno = m_Subs.Count - 1;
        //        }
        //        substring = string.Join("#", m_Subs[pageno].Keys.ToArray());
        //    }
        //    else
        //    {
        //        pageno = v.PageNo;
        //        m_Subs[pageno][subitem] += 1;
        //    }
        //}
        //private void _FindUnSubPageNo(out int pageno, out string substring, string unsubitem)
        //{
        //    substring = string.Empty;
        //    pageno = -1;
        //    //找有沒有訂過
        //    var v = (from keypair in m_Subs
        //             where keypair.Value.ContainsKey(unsubitem)
        //             select new CapitalSubInfo
        //             {
        //                 PageNo = keypair.Key,
        //                 Subscriptions = keypair.Value[unsubitem]
        //             }).FirstOrDefault();
        //    if (v != null)
        //    {
        //        pageno = v.PageNo;
        //        if (m_Subs[pageno][unsubitem] - 1 <= 0)
        //        {
        //            substring = string.Join("#", m_Subs[pageno].Keys.ToArray()).Replace(unsubitem, "").Replace("##", "#");
        //        }
        //        else
        //        {
        //            m_Subs[pageno][unsubitem] -= 1;
        //        }
        //    }
        //}
        #endregion
    }
}