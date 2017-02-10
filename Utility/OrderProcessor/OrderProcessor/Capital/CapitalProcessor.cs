using SKCOMLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Util.Extension.Class;

namespace OrderProcessor.Capital
{
    public class CapitalProcessor : Processor
    {
        #region Const
        const int OK = 0;
        const int SK_WARNING_LOGIN_READY = 2003;
        #endregion

        #region Static
        //protected static string PATH = @"./";
        //protected static string NAME = "Config.ini";
        public static string LOGSTR = "Log";
        public static string ERRSTR = "ERR";
        #endregion

        #region Event
        public delegate void OnConnectDelegate(string Msg);
        public delegate void OnReplyDelegate(ReplyType ReplyType, Order Ord);
        public delegate void OnErrorDelegate(Order Ord, string Msg);
        public delegate void OnOverseaOpenInterestDelegate(List<OpenInterest> OpenInterests);

        public event OnConnectDelegate OnConnect;
        public event OnConnectDelegate OnDisconnect;
        public event OnReplyDelegate OnOrderReply;
        public event OnReplyDelegate OnMatchReply;
        public event OnReplyDelegate OnCancelReply;
        public event OnErrorDelegate OnErrorReply;
        public event OnOverseaOpenInterestDelegate OnOverseaOpenInterest;
        #endregion

        #region Variable
        private SKCenterLib m_CenterLib;
        private SKOrderLib m_OrderLib;
        private SKReplyLib m_ReplyLib;
        private string m_LoginID;

        //private FOnData m_fData;
        //private FOnConnect m_fConnect;
        //private FOnConnect m_fDisconnect;
        //private FOnComplete m_fComplete;
        //private FOnOverseaFutureOpenInterest m_fOnOverseaFutureOpenInterest;

        private bool m_isCapitalConnect = false;
        private bool m_isCapitalInit = false;
        //private Dictionary<string, Order> m_Order;
        private List<Account> m_Account = new List<Account>();
        private List<OpenInterest> m_OpenInterest;
        private System.Timers.Timer m_RequestTimer = new System.Timers.Timer(5000);
        private bool m_WaitingPositionResponse = false;

        #region EJ
        private Dictionary<string, List<Order>> m_OrdersByPID;
        private Dictionary<string, Order> m_OrdersByOKSeq;
        private Dictionary<string, List<Order>> m_ValidsByPID;
        private Dictionary<string, Order> m_ValidsByOKSeq;
        private Dictionary<string, List<Order>> m_CancelsByPID;
        private Dictionary<string, Order> m_CancelsByOKSeq;
        private Dictionary<string, List<Order>> m_DealsByPID;
        private Dictionary<string, Order> m_DealsByOKSeq;        
        private Dictionary<string, Order> m_ErrsByOKSeq;
        private Dictionary<string, List<Order>> m_ErrsByPID;        
        
        private Dictionary<string, Summary> m_Summary;        
        #endregion
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
        public List<Account> Accounts { get { return m_Account; } }
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

        #region List
        //private IEnumerable<Order> OrderList
        //{
        //    get
        //    {
        //        if (m_Order == null) { return new List<Order>(); }
        //        var orders = from order in m_Order.Values
        //                     where
        //                     order.OrderType == OrderType.N &&
        //                     order.OrderErr == OrderErr.N
        //                     select order;
        //        return orders;
        //    }
        //}
        //private IEnumerable<Order> ValidList
        //{
        //    get
        //    {
        //        if (m_Order == null) { return new List<Order>(); }
        //        var orderGroup = from order in m_Order.Values
        //                         group order by order.OrdNo into g
        //                         select new
        //                         {
        //                             g.Key,
        //                             OrdQty = g.Where(e => e.OrderType == OrderType.N).Sum(e => e.Qty),
        //                             CumQty = g.Where(e => e.OrderType == OrderType.D).Sum(e => e.Qty),
        //                             isCancel = g.Count(e => e.OrderType == OrderType.C) > 0,
        //                             Number = g.Count()
        //                         };
        //        var valid = from order in m_Order.Values
        //                    where
        //                    order.OrderType == OrderType.N &&
        //                    //orderGroup.Count(e => e.Key == order.OrdNo && e.Number == 1) > 0 &&
        //                    orderGroup.Count(e => e.Key == order.OrdNo && e.OrdQty - e.CumQty > 0 && !e.isCancel) > 0 &&
        //                    order.OrderErr == OrderErr.N
        //                    select order;
        //        return valid;
        //    }
        //}
        //private IEnumerable<Order> CancelList
        //{
        //    get
        //    {
        //        if (m_Order == null) { return new List<Order>(); }
        //        var cancel = from order in m_Order.Values
        //                     where
        //                     order.OrderType == OrderProcessor.Capital.OrderType.C &&
        //                     order.OrderErr == OrderErr.N
        //                     select order;
        //        return cancel;
        //    }
        //}
        //private IEnumerable<Order> MatchList
        //{
        //    get
        //    {
        //        if (m_Order == null) { return new List<Order>(); }
        //        var match = from order in m_Order.Values
        //                    where
        //                    order.OrderType == OrderType.D &&
        //                    order.OrderErr == OrderErr.N
        //                    select order;
        //        return match;
        //    }
        //}
        //private IEnumerable<Order> ErrList
        //{
        //    get
        //    {
        //        if (m_Order == null) { return new List<Order>(); }
        //        return m_Order.Values.Where(e => e.OrderErr != OrderErr.N ||
        //        (e.OrderType != OrderType.N && e.OrderType != OrderType.C && e.OrderType != OrderType.D));
        //    }
        //}
        //private IEnumerable<Summary> SummaryList
        //{
        //    get
        //    {
        //        if (m_Order == null) { return new List<Summary>(); }
        //        var summary = from order in m_Order.Values
        //                      where order.OrderType == OrderType.D && order.OrderErr == OrderErr.N
        //                      group order by
        //                         new { BrkID = order.BrokerID, CustNo = order.CustNo, ComID = order.ComID } into g
        //                      select new Summary
        //                      {
        //                          BrokerID = g.Key.BrkID,
        //                          CustNo = g.Key.CustNo,
        //                          ComID = g.Key.ComID,
        //                          ALots = g.Where(e => e.BuySell == BuySell.S).Sum(e => e.Qty),
        //                          BLots = g.Where(e => e.BuySell == BuySell.B).Sum(e => e.Qty),
        //                          AAmount = g.Where(e => e.BuySell == BuySell.S).Sum(e => e.MatchAmount),
        //                          BAmount = g.Where(e => e.BuySell == BuySell.B).Sum(e => e.MatchAmount)
        //                      };
        //        return summary;
        //    }
        //}
        #endregion

        //public CapitalProcessor()
        public CapitalProcessor()
        {
            m_CenterLib = new SKCenterLib();
            m_OrderLib = new SKOrderLib();
            m_ReplyLib = new SKReplyLib();
#if DEBUG
            m_CenterLib.SKCenterLib_Debug(true);
#endif
            m_ReplyLib.OnData += OnData;
            m_ReplyLib.OnConnect += Connected;
            m_ReplyLib.OnDisconnect += Disconnected;
            m_ReplyLib.OnComplete += ConnectComplete;
            m_OrderLib.OnOverseaFutureOpenInterest += OverseaFutureOpenInterest;
            m_OrderLib.OnAccount += OnAccount;

            //m_OrderLib.SKOrderLib_LoadOSCommodity();
            m_RequestTimer.Elapsed += (sender, e) =>
            {
                if (WaitingPositionResponse)
                {
                    GetOpenInterest();
                }
                m_RequestTimer.Stop();
            };
        }

        #region Delegate
        //private void OnData(IntPtr bstrData)
        private void OnData(string bstrUserID, string bstrData)
        {            
            Order o = new Order(bstrData);
            #region EJ
            if (o.OrderErr != OrderErr.N)
            {
                if (!m_ErrsByOKSeq.ContainsKey(o.Key)) { m_ErrsByOKSeq.Add(o.Key, o); }
                if (!m_ErrsByPID.ContainsKey(o.ComID))
                {
                    m_ErrsByPID.Add(o.ComID, new List<Order>());
                }
                m_ErrsByPID[o.ComID].Add(o);
                OnErrorReply?.Invoke(o, "Order Fail");
                return;
            }
            switch (o.OrderType)
            {
                case OrderType.N:
                    if (!m_OrdersByOKSeq.ContainsKey(o.Key)) { m_OrdersByOKSeq.Add(o.Key, o); }
                    if (!m_OrdersByPID.ContainsKey(o.ComID))
                    {
                        m_OrdersByPID.Add(o.ComID, new List<Order>());
                    }
                    m_OrdersByPID[o.ComID].Add(o);

                    //Valid
                    //if (!m_ValidsByOKSeq.ContainsKey(o.OkSeq))
                    //{
                    //    m_ValidsByOKSeq.Add(o.OkSeq, o);
                    //}
                    if (!m_ValidsByPID.ContainsKey(o.ComID))
                    {
                        m_ValidsByPID.Add(o.ComID, new List<Order>());
                    }

                    //SPREAD 資料不夠, 找原單
                    Order order = m_DealsByOKSeq.Values.FirstOrDefault(e => e.OrdNo == o.OrdNo);
                    if (order != null)
                    {
                        o.AfterQty -= order.Qty;
                        o.SumQty += order.Qty;
                        if (o.ComID.Contains("SPD") && (o.SumQty == (o.Qty * 2)))
                        {
                            o.SumQty = o.Qty;
                        }
                        order.SumQty = o.SumQty;
                    }
                    //刪單先回來
                    if (m_CancelsByOKSeq.Values.FirstOrDefault(e => (e.OrdNo == o.OrdNo)) != null)
                    {
                        //刪單後清掉口數
                        o.Qty = 0;
                        o.AfterQty = 0;
                    }
                    //沒有刪單先回來 再變成有效單
                    if (o.AfterQty > 0)
                    {
                        if (!m_ValidsByOKSeq.ContainsKey(o.Key))
                        {
                            m_ValidsByOKSeq.Add(o.Key, o);
                        }
                        m_ValidsByPID[o.ComID].Add(o);
                    }
                    OnOrderReply?.Invoke(ReplyType.OrderReply, o);
                    break;
                case OrderType.C:
                    if (!m_CancelsByOKSeq.ContainsKey(o.Key))
                    {
                        m_CancelsByOKSeq.Add(o.Key, o);
                    }
                    if (!m_CancelsByPID.ContainsKey(o.ComID))
                    {
                        m_CancelsByPID.Add(o.ComID, new List<Order>());
                    }
                    m_CancelsByPID[o.ComID].Add(o);
                    //Remove Valid                    
                    //var validOnCancel = m_OrdersByPID[o.ComID].FirstOrDefault(e => e.OrdNo == o.OrdNo);
                    //找原單, 有找到 移除有效單
                    var validOnCancel = m_OrdersByOKSeq.Values.FirstOrDefault(e => e.OrdNo == o.OrdNo);
                    if (validOnCancel != null)
                    {
                        //刪單後清掉口數
                        validOnCancel.Qty = 0;
                        validOnCancel.AfterQty = 0;
                        m_ValidsByPID[validOnCancel.ComID].Remove(validOnCancel);
                        m_ValidsByOKSeq.Remove(validOnCancel.Key);
                    }
                    OnCancelReply?.Invoke(ReplyType.OrderReply, o);
                    break;
                case OrderType.D:
                    if (!m_DealsByOKSeq.ContainsKey(o.Key))
                    {
                        m_DealsByOKSeq.Add(o.Key, o);
                    }
                    if (!m_DealsByPID.ContainsKey(o.ComID))
                    {
                        m_DealsByPID.Add(o.ComID, new List<Order>());
                    }
                    m_DealsByPID[o.ComID].Add(o);
                    //Remove Valid                    
                    //var validOnMatch = m_OrdersByPID[o.ComID].FirstOrDefault(e => e.OrdNo == o.OrdNo);

                    //找原單
                    var validOnMatch = m_OrdersByOKSeq.Values.FirstOrDefault(e => e.OrdNo == o.OrdNo);
                    if (validOnMatch != null)
                    {                        
                        validOnMatch.AfterQty -= o.Qty;
                        //SPD單會扣超過
                        if (validOnMatch.AfterQty < 0) { validOnMatch.AfterQty = 0; }
                        validOnMatch.SumQty += o.Qty;
                        if (validOnMatch.ComID.Contains("SPD") && validOnMatch.SumQty == validOnMatch.Qty * 2)
                        {
                            validOnMatch.SumQty = validOnMatch.Qty;
                        }
                        //移除有效單
                        if (validOnMatch.AfterQty == 0)
                        {
                            m_ValidsByPID[validOnMatch.ComID].Remove(validOnMatch);
                            m_ValidsByOKSeq.Remove(validOnMatch.Key);
                        }
                        o.SumQty = validOnMatch.SumQty;
                    }
                    //Summary
                    if (!m_Summary.ContainsKey(o.ComID))
                    {
                        m_Summary.Add(o.ComID, new Summary()
                        {
                            BrokerID = o.BrokerID,
                            CustNo = o.CustNo,
                            ComID = o.ComID,
                            ALots = 0,
                            BLots = 0,
                            AAmount = 0,
                            BAmount = 0
                        });
                    }
                    if (o.BuySell == Side.B)
                    {
                        m_Summary[o.ComID].BLots += o.Qty;
                        m_Summary[o.ComID].BAmount += o.Qty * o.Price;
                    }
                    else
                    {
                        m_Summary[o.ComID].ALots += o.Qty;
                        m_Summary[o.ComID].AAmount += o.Qty * o.Price;
                    }
                    OnMatchReply?.Invoke(ReplyType.MatchReply, o);
                    if (!m_WaitingPositionResponse) { GetOpenInterest(); }
                    break;
            }
            #endregion            
        }
        private void Connected(string strID, int nErrorCode)
        {
            isCapitalConnect = true;
            //m_Order.Clear();
            #region EJ
            m_OrdersByPID.Clear();
            m_OrdersByOKSeq.Clear();
            m_ValidsByPID.Clear();
            m_ValidsByOKSeq.Clear();
            m_CancelsByPID.Clear();
            m_CancelsByOKSeq.Clear();
            m_DealsByPID.Clear();
            m_DealsByOKSeq.Clear();
            m_ErrsByOKSeq.Clear();
            m_ErrsByPID.Clear();
            m_Summary.Clear();            
            #endregion

            string strInfo = $"回報連線 {strID}    Code: {GetApiCodeDefine(nErrorCode)}";
            OnConnect?.Invoke(strInfo);
            NotificationCenter.Instance.Post(LOGSTR, new Notification(this, strInfo));
        }
        private void Disconnected(string strID, int nErrorCode)
        {
            isCapitalConnect = false;
            m_LoginID = string.Empty;
            m_Account.Clear();
            string strInfo = $"回報斷線 {strID}    Code: {GetApiCodeDefine(nErrorCode)}";
            OnDisconnect?.Invoke(strInfo);
            //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, strInfo));
            NotificationCenter.Instance.Post(ERRSTR, new Notification(this, strInfo));
        }
        //private void ConnectComplete(int nCode)
        private void ConnectComplete(string bstrUserID)
        {
            //string strInfo = "Server連線確認      Code: " + GetApiCodeDefine(nCode);
            string strInfo = "Server連線確認      UserID: " + bstrUserID;
            OnConnect?.Invoke(strInfo);
            NotificationCenter.Instance.Post(LOGSTR, new Notification(this, strInfo));
        }
        //private void OnAccount(string bstrData)
        private void OnAccount(string bstrLoginID, string bstrData)
        {
            Account acc = new Account(bstrData);
            if (acc.MarketType == MarketType.OF)
            {
                m_Account.Add(acc);
                NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "收到帳號資料: " + acc.ToString()));
            }
        }
        private void OverseaFutureOpenInterest(string bstrData)
        {
            if (!string.IsNullOrEmpty(bstrData))
            {
                if (bstrData.Contains("@@"))
                {
                    WaitingPositionResponse = false;
                    m_OpenInterest = new List<OpenInterest>();
                    NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "未平倉查詢: " + bstrData));
                }
                else if (bstrData.Contains("##"))
                {
                    OnOverseaOpenInterest?.Invoke(m_OpenInterest);
                    NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "未平倉查詢結束" + bstrData));
                }
                else
                {
                    OpenInterest position = new OpenInterest(bstrData);
                    m_OpenInterest.Add(position);
                }
            }
        }
        #endregion

        #region Public
        public void Start(string LoginID, string Password, out string Msg)
        {
            int code;
            Msg = string.Empty;
            m_LoginID = LoginID;

            #region Reply
            
            if ((code = m_CenterLib.SKCenterLib_Login(m_LoginID, Password)) != OK && code != SK_WARNING_LOGIN_READY)
            {
                NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "Login: " + GetApiCodeDefine(code)));
                return;
            }
            //m_Order = new Dictionary<string, Order>();
            #region EJ
            m_ValidsByPID = new Dictionary<string, List<Order>>();
            m_ValidsByOKSeq = new Dictionary<string, Order>();
            m_CancelsByPID = new Dictionary<string, List<Order>>();
            m_CancelsByOKSeq = new Dictionary<string, Order>();
            m_DealsByPID = new Dictionary<string, List<Order>>();
            m_DealsByOKSeq = new Dictionary<string, Order>();
            m_OrdersByPID = new Dictionary<string, List<Order>>();
            m_OrdersByOKSeq = new Dictionary<string, Order>();
            m_ErrsByPID = new Dictionary<string, List<Order>>();
            m_ErrsByOKSeq = new Dictionary<string, Order>();            
            m_Summary = new Dictionary<string, Summary>();
            #endregion
            m_isCapitalInit = true;
            Msg = "回報元件初始化完成";
            NotificationCenter.Instance.Post(LOGSTR, new Notification(this, Msg));
            if ((code = m_ReplyLib.SKReplyLib_ConnectByID(m_LoginID)) != OK)
            {
                isCapitalConnect = false;
                NotificationCenter.Instance.Post(LOGSTR, new Notification(this, GetApiCodeDefine(code)));
                return;
            }
            m_isCapitalConnect = true;
            Msg = "回報元件連線完成";
            NotificationCenter.Instance.Post(LOGSTR, new Notification(this, Msg));            
            #endregion
            #region Order
            string OrderMsg = string.Empty;
            if ((code = m_OrderLib.SKOrderLib_Initialize()) == OK)
            {                
                OrderMsg = "下單元件初始化完成";
                Msg += " " + OrderMsg;
                NotificationCenter.Instance.Post(LOGSTR, new Notification(this, OrderMsg));
            }
            else if (code == SK_WARNING_LOGIN_READY)
            {
                OrderMsg = "下單元件已初始化，無須重複執行";
                Msg += " " + OrderMsg;
                NotificationCenter.Instance.Post(LOGSTR, new Notification(this, OrderMsg));
            }
            else
            {
                isCapitalInit = false;
                OrderMsg = "下單元件初始化失敗 code:" + GetApiCodeDefine(code);
                Msg += " " + OrderMsg;
                //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, OrderMsg));
                NotificationCenter.Instance.Post(ERRSTR, new Notification(this, OrderMsg));
                return;
            }            
            #endregion
            #region Certificate
            //Initialize  Cert
            if ((code = m_OrderLib.ReadCertByID(m_LoginID)) != OK)
            {
                Msg = "憑證讀取失敗 code:" + GetApiCodeDefine(code);
                //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, Msg));
                NotificationCenter.Instance.Post(ERRSTR, new Notification(this, Msg));
            }
            #endregion
            #region Account
            //Get Account
            if ((code = m_OrderLib.GetUserAccount()) != OK)
            {
                Msg = "下單帳號讀取失敗 code:" + m_CenterLib.SKCenterLib_GetReturnCodeMessage(code);
                //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, Msg));
                NotificationCenter.Instance.Post(ERRSTR, new Notification(this, Msg));
            }
            #endregion
            #region LoadCommodity
            //Load Oversea Commodity
            if ((code = m_OrderLib.SKOrderLib_LoadOSCommodity()) != OK)
            {
                Msg = "下單商品讀取失敗 code:" + m_CenterLib.SKCenterLib_GetReturnCodeMessage(code);
                //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, Msg));
                NotificationCenter.Instance.Post(ERRSTR, new Notification(this, Msg));
            }
            #endregion
            NotificationCenter.Instance.Post("isCapitalInit");
            GetOpenInterest();            
        }
        //public void ReplyConnect(string LoginID, out string Msg)
        //public void ReplyConnect(out string Msg)
        //{
        //    Msg = string.Empty;
        //    int code;
        //    if ((code = m_ReplyLib.SKReplyLib_ConnectByID(m_LoginID)) != OK)
        //    {
        //        isCapitalConnect = false;
        //        NotificationCenter.Instance.Post(LOGSTR, new Notification(this, GetApiCodeDefine(code)));
        //        return;
        //    }
        //    m_isCapitalConnect = true;
        //    Msg = "回報元件連線完成";
        //    NotificationCenter.Instance.Post(LOGSTR, new Notification(this, Msg));

        //    //int ret = Functions.SKReplyLib_IsConnectedByID(LoginID);
        //    //if (ret == 0)
        //    //{
        //    //    ret = Functions.SKReplyLib_ConnectByID(LoginID);
        //    //    if (ret != 0)
        //    //    {
        //    //        Msg = "回報元件連線失敗 code:" + GetApiCodeDefine(ret);
        //    //        NotificationCenter.Instance.Post(LOGSTR, new Notification(this, Msg));
        //    //    }
        //    //}
        //}
        //public void ReplyDisconnect(string LoginID, out string Msg)
        public void ReplyDisconnect(out string Msg)
        {
            if (string.IsNullOrEmpty(m_LoginID))
            {
                Msg = "沒有登入帳號資料";
                NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "關閉回報錯誤 " + Msg));
                return;
            }
            Msg = string.Empty;
            int code;
            if ((code = m_ReplyLib.SKReplyLib_CloseByID(m_LoginID)) != OK)
            {
                Msg = GetApiCodeDefine(code);
            }
            NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "關閉回報 " + Msg));
        }
        private OVERSEAFUTUREORDER _GenOverseaOrder(string account, string exchange, string orderHead, string ym, string ym2, Side bs, int qty, double price)
        {
            return new OVERSEAFUTUREORDER()
            {
                bstrFullAccount = account,
                bstrExchangeNo = exchange,
                bstrStockNo = orderHead,
                bstrYearMonth = ym,
                bstrYearMonth2 = ym2,
                sBuySell = short.Parse(bs.Capital().ToString()),
                sNewClose = 0,
                sDayTrade = 0,
                sTradeType = 0,
                sSpecialTradeType = 0,
                nQty = qty,
                bstrOrder = price.ToString(),
                bstrOrderNumerator = "",
                bstrTrigger = "",
                bstrTriggerNumerator = ""
            };
        }
        
        public bool Order(string account, string exchange, string orderHead, string ym, Side bs, int qty, double price, out string returnMsg)
        {
            if (string.IsNullOrEmpty(m_LoginID))
            {
                returnMsg = "無帳號資料";
                //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, returnMsg));
                NotificationCenter.Instance.Post(ERRSTR, new Notification(this, returnMsg));
                return false;
            }
            OVERSEAFUTUREORDER oversea = _GenOverseaOrder(account, exchange, orderHead, ym,"", bs, qty, price);

            int code = m_OrderLib.SendOverseaFutureOrder(m_LoginID, false, ref oversea, out returnMsg);
            string codeMsg = code == OK ? string.Empty : GetApiCodeDefine(code);
            NotificationCenter.Instance.Post(LOGSTR, new Notification(this, $"{codeMsg} {account},{exchange},{orderHead},{ym},{bs},0,0,0,0,{qty},{price}, , , , {returnMsg}"));            
            return code == OK;
        }
        public bool SpreadOrder(string account, string exchange, string orderHead, string ym, string ym2, Side bs, int qty, double price, out string returnMsg)
        {
            if (string.IsNullOrEmpty(m_LoginID))
            {
                returnMsg = "無帳號資料";
                //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, returnMsg));
                NotificationCenter.Instance.Post(ERRSTR, new Notification(this, returnMsg));
                return false;
            }
            OVERSEAFUTUREORDER pAsyncOrder = _GenOverseaOrder(account, exchange, orderHead, ym, ym2, bs, qty, price);
            int nCode = m_OrderLib.SendOverseaFutureSpreadOrder(m_LoginID, false, ref pAsyncOrder, out returnMsg);
            string str = (nCode == 0) ? string.Empty : GetApiCodeDefine(nCode);
            NotificationCenter.Instance.Post(LOGSTR, new Notification(this, $"{str} {account},{exchange},{orderHead},{ym},{ym2},{bs},0,0,0,0,{qty},{price}, , , , {returnMsg}"));
            return (nCode == 0);
        }

        public bool Cancel(string account, string keyNo, out string returnMsg)
        {
            if (string.IsNullOrEmpty(m_LoginID))
            {
                returnMsg = "無帳號資料";
                //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, returnMsg));
                NotificationCenter.Instance.Post(ERRSTR, new Notification(this, returnMsg));
                return false;
            }

            int code = m_OrderLib.OverSeaCancelOrderBySeqNo(m_LoginID, false, account, keyNo, out returnMsg);
            string codeMsg = code == OK ? string.Empty : GetApiCodeDefine(code);
            NotificationCenter.Instance.Post(LOGSTR, new Notification(this,$"{codeMsg} {account},{keyNo},{returnMsg}"));
            return code == OK;

            //int size = 4096;
            //Msg = new StringBuilder();
            //Msg.Length = size;
            //int ret = Functions.OverseaCancelOrderBySeqNo(account, keyNo, Msg, out size);
            //return ret == 0;
        }

        //public IEnumerable<object> GetDataList(ListType listtype)
        //{
        //    switch ((int)listtype)
        //    {
        //        case 1: return ValidList.OrderBy(e => e.Time);
        //        case 2: return CancelList.OrderBy(e => e.Time);
        //        case 3: return ValidList.Union(CancelList).OrderBy(e => e.Time);
        //        case 8: return MatchList.OrderBy(e => e.Time);
        //        case 9: return ValidList.Union(MatchList).OrderBy(e => e.Time);
        //        case 10: return CancelList.Union(MatchList).OrderBy(e => e.Time);
        //        case 11: return ValidList.Union(CancelList).Union(MatchList).OrderBy(e => e.Time);
        //        case 16: return OrderList.OrderBy(e => e.Time);
        //        case 18: return CancelList.Union(OrderList).OrderBy(e => e.Time);
        //        case 24: return MatchList.Union(OrderList).OrderBy(e => e.Time);
        //        case 26: return CancelList.Union(MatchList).Union(OrderList).OrderBy(e => e.Time);
        //        case 32: return SummaryList.OrderBy(e => e.ComID);
        //    }
        //    return null;
        //}
        public void GetOpenInterest()
        {
            if (m_Account == null || m_Account.Count == 0)
            {
                NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "無帳號資料"));
            }
            else if (string.IsNullOrEmpty(m_LoginID))
            {
                //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "無登入帳號資料"));
                NotificationCenter.Instance.Post(ERRSTR, new Notification(this, "無登入帳號資料"));
            }
            else
            {
                WaitingPositionResponse = true;
                foreach (var acc in m_Account)
                {
                    if (acc.MarketType == MarketType.OF)
                    {
                        NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "查詢未平倉明細: " + acc.Key));
                        int code = m_OrderLib.GetOverseaFutureOpenInterest(m_LoginID, acc.Key);
                        if (code != OK)
                        {
                            //NotificationCenter.Instance.Post(LOGSTR, new Notification(this, "查詢未平倉明細錯誤: " + GetApiCodeDefine(code)));
                            NotificationCenter.Instance.Post(ERRSTR, new Notification(this, "查詢未平倉明細錯誤: " + GetApiCodeDefine(code)));
                        }

                        //Functions.GetOverseaFutureOpenInterest(acc.Key);
                    }
                }
            }
        }
        public IEnumerable<double> PriceByOrderNo(string PID, string OrdNo)
        {
            List<Order> orders;
            List<double> re = new List<double>();
            if (m_OrdersByPID.ContainsKey(PID))
            {
                orders = m_OrdersByPID[PID];
                lock (orders)
                {
                    List<double> prices = orders.Where(e => e != null && e.OrdNo == OrdNo).Select(e => e.Price).ToList();                        
                    re.AddRange(prices);
                }
            }
            if (m_DealsByPID.ContainsKey(PID))
            {
                orders = m_DealsByPID[PID];
                lock (orders)
                {
                    List<double> prices = orders.Where(e => e != null && e.OrdNo == OrdNo).Select(e => e.Price).ToList();                        
                    re.AddRange(prices);
                }
            }
            return re.Distinct();
        }

        #endregion

        #region EJ  
        public IEnumerable<Order> Orders(string pid = "")
        {
            if (!string.IsNullOrEmpty(pid))
            {
                return m_OrdersByPID.ContainsKey(pid) ? m_OrdersByPID[pid] : new List<Order>();
            }
            return m_OrdersByOKSeq?.Values;
        }
        public IEnumerable<Order> Valids(string pid = "")
        {
            if (!string.IsNullOrEmpty(pid))
            {
                return m_ValidsByPID.ContainsKey(pid) ? m_ValidsByPID[pid] : new List<Order>();
            }
            else
            {
                return m_ValidsByOKSeq?.Values;
            }
        }
        public IEnumerable<Order> Cancels(string pid = "")
        {
            if (!string.IsNullOrEmpty(pid))
            {
                return m_CancelsByPID.ContainsKey(pid) ? m_CancelsByPID[pid] : new List<Order>();
            }
            return m_CancelsByOKSeq?.Values;
        }
        public IEnumerable<Order> Deals(string pid = "")
        {
            if (!string.IsNullOrEmpty(pid))
            {
                return m_DealsByPID.ContainsKey(pid) ? m_DealsByPID[pid] : new List<Order>();
            }
            return m_DealsByOKSeq?.Values;            
        }
        public IEnumerable<Order> Errs(string pid = "")
        {
            if (!string.IsNullOrEmpty(pid))
            {
                return m_ErrsByPID.ContainsKey(pid) ? m_ErrsByPID[pid] : new List<Order>();
            }
            return m_ErrsByOKSeq?.Values;
        }
        public IEnumerable<Summary> Summaries()
        {
            return m_Summary?.Values;
        }
        #endregion

#if DEBUG
        private void _InitList()
        {
            if (m_OrdersByOKSeq == null)
            {
                m_OrdersByOKSeq = new Dictionary<string, Order>();
            }
            if (m_OrdersByPID == null)
            {
                m_OrdersByPID = new Dictionary<string, List<Order>>();
            }
            if (m_ValidsByOKSeq == null)
            {
                m_ValidsByOKSeq = new Dictionary<string, Order>();
            }
            if (m_ValidsByPID == null)
            {
                m_ValidsByPID = new Dictionary<string, List<Order>>();
            }
            if (m_DealsByOKSeq == null)
            {
                m_DealsByOKSeq = new Dictionary<string, Order>();
            }
            if (m_DealsByPID == null)
            {
                m_DealsByPID = new Dictionary<string, List<Order>>();
            }
            if (m_Summary == null)
            {
                m_Summary = new Dictionary<string, Summary>();
            }
            if (m_CancelsByOKSeq == null)
            {
                m_CancelsByOKSeq = new Dictionary<string, Order>();
            }
            if (m_CancelsByPID == null)
            {
                m_CancelsByPID = new Dictionary<string, List<Order>>();
            }
            if (m_ErrsByOKSeq == null)
            {
                m_ErrsByOKSeq = new Dictionary<string, Order>();
            }
            if (m_ErrsByPID == null)
            {
                m_ErrsByPID = new Dictionary<string, List<Order>>();
            }
        }
        public void TestOrder(string filename)
        {
            ThreadPool.QueueUserWorkItem(delegate (object state) {
                if (File.Exists(filename))
                {
                    _InitList();
                    using (StreamReader reader = new StreamReader(filename))
                    {
                        DateTime minValue = DateTime.MinValue;
                        while (!reader.EndOfStream)
                        {
                            string bstrData = reader.ReadLine();
                            DateTime time2 = Convert.ToDateTime(bstrData.Substring(0, 23));
                            bstrData = bstrData.Substring(23);
                            if ((bstrData.Contains("OF,D,") || bstrData.Contains("OF,C,")) && (minValue != DateTime.MinValue))
                            {
                                new TimeSpan(time2.Ticks - minValue.Ticks);
                            }
                            minValue = time2;
                            this.OnData("SUB0007301", bstrData);
                        }
                    }
                }
            });
        }
        public void TestOrder(int times, string exchange, string comid, string price)
        {
            _InitList();
            ThreadPool.QueueUserWorkItem(delegate (object state) {
                int orderCounts = times * 10;
                for (int j = 0; j < orderCounts; j++)
                {
                    int num5;
                    int testCondition = j % 10;
                    int num4 = j / 10;
                    string replyType = "";
                    string orderError = "";
                    string bs = "";
                    string ordno = "";
                    string qty = "1";
                    string before = "1";
                    string after = "1";
                    switch (testCondition)
                    {
                        case 0:
                            replyType = "N";
                            orderError = "N";
                            bs = "B20";
                            ordno = $"NO{j.ToString().PadLeft(4, '0')}";
                            qty = "8";
                            before = "8";
                            after = "8";
                            break;

                        case 1:
                            replyType = "N";
                            orderError = "N";
                            bs = "B20";
                            ordno = $"NO{j.ToString().PadLeft(4, '0')}";
                            qty = "10";
                            before = "10";
                            after = "10";
                            break;

                        case 2:
                            replyType = "D";
                            orderError = "N";
                            bs = "B20";
                            num5 = (num4 * 10) + 1;
                            ordno = $"NO{num5.ToString().PadLeft(4, '0')}";
                            qty = "2";
                            before = "10";
                            after = "8";
                            break;

                        case 3:
                            replyType = "D";
                            orderError = "N";
                            bs = "B20";
                            num5 = (num4 * 10) + 1;
                            ordno = $"NO{num5.ToString().PadLeft(4, '0')}";
                            qty = "3";
                            before = "8";
                            after = "5";
                            break;

                        case 4:
                            replyType = "C";
                            orderError = "N";
                            bs = "B20";
                            num5 = num4 * 10;
                            ordno = $"NO{num5.ToString().PadLeft(4, '0')}";
                            qty = "8";
                            before = "8";
                            after = "0";
                            break;

                        case 5:
                            replyType = "D";
                            orderError = "N";
                            bs = "B20";
                            num5 = (num4 * 10) + 1;
                            ordno = $"NO{num5.ToString().PadLeft(4, '0')}";
                            qty = "5";
                            before = "5";
                            after = "0";
                            break;

                        case 6:
                            replyType = "N";
                            orderError = "N";
                            bs = "S20";
                            ordno = $"NO{j.ToString().PadLeft(4, '0')}";
                            qty = "6";
                            before = "6";
                            after = "6";
                            break;

                        case 7:
                            replyType = "N";
                            orderError = "N";
                            bs = "S20";
                            ordno = $"NO{j.ToString().PadLeft(4, '0')}";
                            qty = "7";
                            before = "7";
                            after = "7";
                            break;

                        case 8:
                            replyType = "N";
                            orderError = "Y";
                            bs = "S20";
                            ordno = $"NO{j.ToString().PadLeft(4, '0')}";
                            qty = "8";
                            before = "8";
                            after = "8";
                            break;

                        case 9:
                            replyType = "D";
                            orderError = "N";
                            bs = "S20";
                            num5 = (num4 * 10) + 7;
                            ordno = $"NO{num5.ToString().PadLeft(4, '0')}";
                            qty = "7";
                            before = "7";
                            after = "0";
                            break;
                    }
                    string keyno = (replyType == "D") ? "" : j.ToString();
                    string bstrData = $"{keyno},OF,{replyType},{orderError},F020000,9971752,{bs},{exchange},{comid},,{ordno},{price},,,,,,,,,{qty},{before},{after},{DateTime.Now.ToString("yyyy/MM/dd")},{DateTime.Now.ToString("HHmmssfff")},{j},0007301,SALENO,AGENT";
                    this.OnData("SUB0007301", bstrData);
                    Thread.Sleep(1);
                }
            });
        }

#endif

        private string GetApiCodeDefine(int nCode)
        {
            //string strNCode = Enum.GetName(typeof(ApiMessage), nCode);
            string strNCode = m_CenterLib.SKCenterLib_GetReturnCodeMessage(nCode);
            return string.IsNullOrEmpty(strNCode) ? nCode.ToString() : strNCode;
        }
    }
}