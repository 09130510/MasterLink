using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using SKCOMLib;

namespace Capital.RTD
{
    /// <summary>
    /// 資料取得
    /// </summary>
    public class RTDItemGetter
    {
        const string m_Account = "SUB0007301";
        const string m_Password = "v52500938";
        const int OK = 0;
        const int SK_WARNING_LOGIN_READY = 2003;

        #region Variable
        
        private RTDSvr m_RTDSvr;
        private SKCenterLib m_CenterLib;
        private SKOrderLib m_OrderLib;
        private SKReplyLib m_ReplyLib;
        //private FOnData m_fData;
        //private FOnConnect m_fConnect;
        //private FOnConnect m_fDisconnect;
        //private FOnComplete m_fComplete;
        //private FOnOverseaFutureOpenInterest m_fOnOverseaFutureOpenInterest;

        private Dictionary<string, Order> m_Order;
        private List<Account> m_AccountList = new List<Account>();
        private List<OpenInterest> m_OpenInterestList = new List<OpenInterest>();
        private System.Timers.Timer m_RequestTimer = new System.Timers.Timer(5000);
        private bool m_WaitingPositionResponse = false;
        #endregion

        #region Property
        private bool WaitingPositionResponse
        {
            get { return m_WaitingPositionResponse; }
            set
            {
                if (value == m_WaitingPositionResponse) { return; }
                m_WaitingPositionResponse = value;
                if (m_WaitingPositionResponse)
                {
                    m_RequestTimer.Start();
                }
                else
                {
                    m_RequestTimer.Stop();
                }
            }
        }
        #endregion

        /// <summary>
        /// 資料取得
        /// </summary>
        /// <param name="rtds"></param>
        public RTDItemGetter(RTDSvr rtds)
        {
            m_RTDSvr = rtds;            

            //m_fData = new FOnData(OnData);
            //GC.KeepAlive(m_fData);
            //m_fConnect = new FOnConnect(OnConnect);
            //GC.KeepAlive(m_fConnect);
            //m_fDisconnect = new FOnConnect(OnDisconnect);
            //GC.KeepAlive(m_fDisconnect);
            //m_fComplete = new FOnComplete(OnComplete);
            //GC.KeepAlive(m_fComplete);
            //m_fOnOverseaFutureOpenInterest = new FOnOverseaFutureOpenInterest(OnOverseaFutureOpenInterest);
            //GC.KeepAlive(m_fOnOverseaFutureOpenInterest);

            m_RequestTimer.Elapsed += (sender, e) =>
            {
                if (WaitingPositionResponse)
                {
                    OpenInterest();
                }
                m_RequestTimer.Stop();
            };

            ReplyInitialize();
            OrderInitialize();
        }

        /// <summary>
        /// 更新所有RTDItem資料
        /// </summary>
        /// <param name="datas"></param>
        public void GetRTDItem(List<RTDItem> datas)
        {
            foreach (var item in datas)
            {
                GetRTDItem(item);
            }
        }
        /// <summary>
        /// 更新單一RTDItem資料
        /// </summary>
        /// <param name="data"></param>
        public void GetRTDItem(RTDItem data)
        {            
            switch (data.RTDType)
            {
                case "SUMMARY":
                    GetSummary(data);
                    break;
                case "OPENINTEREST":
                    GetOpenInterest(data);
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 連線至Capital
        /// </summary>
        public void Connect()
        {
            m_ReplyLib.SKReplyLib_ConnectByID(m_Account) ;            

            //int ret = Functions.SKReplyLib_IsConnectedByID(m_Account);
            //if (ret == 0)
            //{
            //    Functions.SKReplyLib_ConnectByID(m_Account);
            //}
        }
        /// <summary>
        /// 與Capital斷線
        /// </summary>
        public void Disconnect()
        {
            m_ReplyLib.SKReplyLib_CloseByID(m_Account);
            //Functions.SKReplyLib_CloseByID(m_Account);
        }
        /// <summary>
        /// 要求未平倉資料
        /// </summary>
        public void OpenInterest()
        {
            if (m_Account == null || m_AccountList.Count == 0) { return; }
            try
            {
                WaitingPositionResponse = true;
                foreach (var acc in m_AccountList)
                {
                    if (acc.MarketType == MarketType.OF)
                    {
                        m_OrderLib.GetOverseaFutureOpenInterest(m_Account, acc.Key);
                        //Functions.GetOverseaFutureOpenInterest(acc.Key);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private
        /// <summary>
        /// 取得彙總資料
        /// </summary>
        /// <param name="data"></param>
        private void GetSummary(RTDItem data)
        {
            if (!_CheckAccount(data)) { return; }
            var summary = from order in m_Order.Values
                          where order.OrderType == OrderType.D && order.OrderErr == OrderErr.N
                          group order by
                             new { BrkID = order.BrokerID, CustNo = order.CustNo, ComID = order.ComID } into g
                          select new Summary
                          {
                              BrokerID = g.Key.BrkID,
                              CustNo = g.Key.CustNo,
                              ComID = g.Key.ComID,
                              ALot = g.Where(e => e.BuySell == BuySell.S).Sum(e => e.Qty),
                              BLot = g.Where(e => e.BuySell == BuySell.B).Sum(e => e.Qty),
                              AAmt = g.Where(e => e.BuySell == BuySell.S).Sum(e => e.MatchAmount),
                              BAmt = g.Where(e => e.BuySell == BuySell.B).Sum(e => e.MatchAmount)
                          };
            if (summary.Count() == 0)
            {
                data.Value = -1;
                return;
            }
            IEnumerable<Summary> list = summary;
            if (!string.IsNullOrEmpty(data.CustNo) && !string.IsNullOrEmpty(data.ComID))
            {
                list = summary.Where(e => e.CustNo == data.CustNo && e.ComID == data.ComID);
            }
            else if (!string.IsNullOrEmpty(data.CustNo) && string.IsNullOrEmpty(data.ComID))
            {
                list = summary.Where(e => e.CustNo == data.CustNo);
            }
            else if (string.IsNullOrEmpty(data.CustNo) && !string.IsNullOrEmpty(data.ComID))
            {
                list = summary.Where(e => e.ComID == data.ComID);
            }

            switch (data.Item)
            {
                case "BLOT":
                    data.Value = list.Sum(e => e.BLot);
                    break;
                case "ALOT":
                    data.Value = list.Sum(e => e.ALot);
                    break;
                case "BAMT":
                    data.Value = list.Sum(e => e.BAmt);
                    break;
                case "AAMT":
                    data.Value = list.Sum(e => e.AAmt);
                    break;
                default:
                    data.Value = -1;
                    break;
            }
        }
        private bool _CheckAccount(RTDItem item)
        {
            if (!m_Account.Contains(item.CustNo))
            {
                item.Value = "帳號錯誤: " + m_Account;
                return false;
            }
            return true;
        }
        /// <summary>
        /// 取得未平倉資料
        /// </summary>
        /// <param name="data"></param>
        private void GetOpenInterest(RTDItem data)
        {
            if (!_CheckAccount(data)) { return; }
            var op = from open in m_OpenInterestList
                     where open.CustNo == data.CustNo && open.ComID == data.ComID
                     select open;
            if (op.Count() == 0)
            {
                data.Value = -1;
                return;
            }
            OpenInterest item = op.First();
            switch (data.Item)
            {
                case "LOTS":
                    data.Value = item.Qty * (item.BuySell == BuySell.B ? 1 : -1);
                    break;
                case "MARKETPRICE":
                    data.Value = item.MP;
                    break;
                case "AVGPRICE":
                    data.Value = item.AvgP;
                    break;
                case "CLOSEPRICE":
                    data.Value = item.YstCP;
                    break;
                case "PL":
                    data.Value = item.ProfitLoss;
                    break;
                case "UPDATETIME":
                    data.Value = item.UpdateTime;
                    break;
                default:
                    data.Value = -1;
                    break;
            }
        }
        /// <summary>
        /// 回報初始化
        /// </summary>
        private void ReplyInitialize()
        {
            m_CenterLib = new SKCenterLib();            
            m_ReplyLib = new SKReplyLib();
#if DEBUG
            m_CenterLib.SKCenterLib_Debug(true);
#endif
            m_ReplyLib.OnData += OnData;
            m_ReplyLib.OnConnect += OnConnect;
            m_ReplyLib.OnDisconnect += OnDisconnect;
            m_ReplyLib.OnComplete += OnComplete;            

            int code;
            if ((code = m_CenterLib.SKCenterLib_Login(m_Account, m_Password)) == OK )
            {
                m_Order = new Dictionary<string, Order>();
                Connect();
            }
            //int ret = -1;
            //ret = Functions.SKReplyLib_Initialize(m_Account, m_Password);
            //switch (ret)
            //{
            //    case 0:
            //        Functions.RegisterOnConnectCallBack(m_fConnect);
            //        Functions.RegisterOnDisconnectCallBack(m_fDisconnect);
            //        Functions.RegisterOnDataCallBack(m_fData);
            //        Functions.RegisterOnCompleteCallBack(m_fComplete);
            //        m_Order = new Dictionary<string, Order>();
            //        Connect();
            //        break;
            //    case 2003:
            //    default:
            //        break;
            //}
        }
        /// <summary>
        /// 下單初始化(For 未平倉)
        /// </summary>
        private void OrderInitialize()
        {            
            m_OrderLib = new SKOrderLib();
            m_OrderLib.OnOverseaFutureOpenInterest += OnOverseaFutureOpenInterest;
            m_OrderLib.OnAccount += OnAccount;

            int code;
            if ((code = m_OrderLib.SKOrderLib_Initialize()) == OK)
            {
                m_OrderLib.GetUserAccount();
                OpenInterest();
            }
            //int ret = -1;
            //ret = Functions.SKOrderLib_Initialize(m_Account, m_Password);
            //if (ret == 0)
            //{
            //    FOnGetBSTR fAccount = new FOnGetBSTR(OnAccount);
            //    Functions.RegisterOnAccountCallBack(fAccount);
            //    ret = Functions.GetUserAccount();

            //    Functions.RegisterOnOverseaFutureOpenInterestCallBack(m_fOnOverseaFutureOpenInterest);
            //    OpenInterest();
            //}
        }
        #endregion

        #region CallBack
        ///// <summary>
        ///// 收到回報
        ///// </summary>
        ///// <param name="bstrData"></param>
        //private void OnData(IntPtr bstrData)
        private void OnData(string bstrUserID, string bstrData)
        {
            //IntPtr data = bstrData;
            //DataItem item = (DataItem)Marshal.PtrToStructure(data, typeof(DataItem));

            //Order o = new Order(item);
            Order o = new Order(bstrData);
            if (!m_Order.ContainsKey(o.OkSeq + o.OrdNo)) { m_Order.Add(o.OkSeq + o.OrdNo, o); }
            else { return; }
            if (o.OrderErr == OrderErr.N && o.OrderType == OrderType.D)
            {
                m_RTDSvr.UpdateNotify();
                OpenInterest();
            }
            //Marshal.FreeBSTR(data);
        }
        /// <summary>
        /// 回報連線
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="nErrorCode"></param>
        private void OnConnect(string strID, int nErrorCode)
        {
            m_Order.Clear();
        }
        /// <summary>
        /// 回報斷線
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="nErrorCode"></param>
        private void OnDisconnect(string strID, int nErrorCode) { }
        //private void OnComplete(int nCode) { }
        private void OnComplete(string bstrUserID) { }
        ///// <summary>
        ///// 收到帳戶資料
        ///// </summary>
        ///// <param name="bstrData"></param>
        //private void OnAccount(string bstrData)
        private void OnAccount(string bstrLoginID, string bstrData)
        {
            Account acc = new Account(bstrData);
            m_AccountList.Add(acc);
        }
        /// <summary>
        /// 海外期貨未平倉
        /// </summary>
        /// <param name="bstrData"></param>
        private void OnOverseaFutureOpenInterest(string bstrData)
        {
            if (!String.IsNullOrEmpty(bstrData))
            {
                if (bstrData.Contains("@@"))    //開始接收
                {
                    WaitingPositionResponse = false;
                    m_OpenInterestList = new List<OpenInterest>();
                }
                else if (bstrData.Contains("##"))   //停止
                {
                    m_RTDSvr.UpdateNotify();
                }
                else   //接收中                
                {
                    OpenInterest position = new OpenInterest(bstrData);
                    m_OpenInterestList.Add(position);
                }
            }
        }
        #endregion
    }
}