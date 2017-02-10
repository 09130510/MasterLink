using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using log4net;
using System.Windows.Forms;

namespace PriceLib.PATS
{
    public class ConnectStateEventArgs : EventArgs
    {
        public SocketLinkState HostState { get; private set; }
        public SocketLinkState PriceState { get; private set; }
        public LogonState LogonState { get; private set; }
        public bool DLComplete { get; private set; }
        public bool isConnected { get; private set; }
        public ConnectStateEventArgs(SocketLinkState host, SocketLinkState price, LogonState logon, bool dlComplete, bool isconnected)
        {
            HostState = host;
            PriceState = price;
            LogonState = logon;
            DLComplete = dlComplete;
            isConnected = isconnected;
        }
    }

    //public delegate void OnPriceUpdateDelegate(string key, PriceStruct price);
    //public delegate void OnFillUpdateDelegate(string key, FillStruct fill);
    //public delegate void OnOrderUpdateDelegate(string key, OrderDetailStruct order);
    public delegate void OnErrorDelegate(OrderDetailStruct order, string msg);
    public delegate void OnFillDelegate(string key, FillStruct fill);
    public delegate void OnPriceUpdateDelegate(string key, PriceStruct price);
    public delegate void OnReplyDelegate(string key, OrderDetailStruct order);

    public partial class PATSLib : PriceLib
    {
        #region Const
        private const string VER = "v2.8.3";
        private const string APPID = "MarketWinner";
        private const string APPVER = "v0.0.1";
        private const string LID = "IBE1612M002P";
        private const int LEVEL = 255;
        //private const int LEVEL = 83;
        #endregion

        #region Event                
        public event EventHandler<ConnectStateEventArgs> OnConnectStateChanged;
        public event OnPriceUpdateDelegate OnPriceUpdate;
        public event OnErrorDelegate OnErrorReply;
        public event OnFillDelegate OnFillReply;
        public event OnReplyDelegate OnOrderReply;
        public event OnReplyDelegate OnCancelReply;
        #endregion

        #region Variable
        private ProcAddr m_LogonAddr;
        private ProcAddr m_ForcedLogoutAddr;
        private ProcAddr m_DLCompleteAddr;
        private LinkProcAddr m_HostLinkAddr;
        private LinkProcAddr m_PriceLinkAddr;
        private StatusProcAddr m_ContractStatusAddr;
        private TickerUpdateProcAddr m_TickAddr;
        private PriceProcAddr m_PriceAddr;
        private FillProcAddr m_FillAddr;
        private OrderProcAddr m_OrderAddr;

        private SocketLinkState m_HostState;
        private SocketLinkState m_PriceState;
        private LogonStatusStruct m_LogonStatus = new LogonStatusStruct();

        //private Dictionary<string, ContractStruct> m_Contracts;
        private Dictionary<string, int> m_Subs = new Dictionary<string, int>();

        private Dictionary<string, List<OrderDetailStruct>> m_CancelsByCKey;
        private Dictionary<string, OrderDetailStruct> m_CancelsByID;
        private Dictionary<string, List<FillStruct>> m_DealsByCKey;
        private Dictionary<string, FillStruct> m_DealsByID;
        private Dictionary<string, List<OrderDetailStruct>> m_ErrsByCKey;
        private Dictionary<string, OrderDetailStruct> m_ErrsByID;
        private Dictionary<string, List<OrderDetailStruct>> m_OrdersByCKey;
        private Dictionary<string, OrderDetailStruct> m_OrdersByID;
        private Dictionary<string, Summary> m_Summary;
        private Dictionary<string, List<OrderDetailStruct>> m_ValidsByCKey;
        private Dictionary<string, OrderDetailStruct> m_ValidsByID;
        #endregion

        #region Property
        public string OrderIP { get; private set; }
        public string OrderPort { get; private set; }
        public string PriceIP { get; private set; }
        public string PricePort { get; private set; }
        public string UserID { get; private set; }
        public string Pwd { get; private set; }

        public bool isInitialize { get; private set; }
        public bool isConnected
        {
            get
            {
                return HostState == SocketLinkState.ptLinkConnected && PriceState == SocketLinkState.ptLinkConnected && LogonState == LogonState.ptLogonSucceeded && DLComplete;
            }
        }
        private SocketLinkState HostState { get { return m_HostState; } }
        private SocketLinkState PriceState { get { return m_PriceState; } }
        private LogonState LogonState { get { return (LogonState)Enum.Parse(typeof(LogonState), ((int)m_LogonStatus.Status).ToString()); } }
        private bool DoLogon { get; set; }
        private bool DLComplete { get; set; }
        #endregion

        static PATSLib()
        {
            ResourceExtractor.ExtractResourceToFile("PriceLib.Dll.SSLSocketLib.dll", "SSLSocketLib.dll", true);            
            ResourceExtractor.ExtractResourceToFile("PriceLib.Dll.PATSAPI.dll", "PATSAPI.dll", true);
        }

        public PATSLib(string orderip, string orderport, string priceip, string priceport, string user, string pwd)
            : base()
        {
            m_Log = LogManager.GetLogger(typeof(PATSLib));
            m_HostState = SocketLinkState.ptLinkClosed;
            m_PriceState = SocketLinkState.ptLinkClosed;

            OrderIP = orderip;
            OrderPort = orderport;
            PriceIP = priceip;
            PricePort = priceport;
            UserID = user;
            Pwd = pwd;
            isInitialize = Initialization();
        }

        private bool Initialization()
        {
            string path = Application.StartupPath + @"\PATSLogs\";
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
            ptSetClientPath(path);
            int result = ptInitialise((char)EnvironmentType.ptClient, VER, APPID, APPVER, LID, false);
            if (!isValid(result, nameof(Initialization))) { return false; }
            ptEnable(LEVEL);
            m_LogonAddr = new ProcAddr(_LogonStatus);
            m_ForcedLogoutAddr = new ProcAddr(_ForcedLogout);
            m_DLCompleteAddr = new ProcAddr(_DataDLComplete);
            m_HostLinkAddr = new LinkProcAddr(_HostLinkStateChange);
            m_PriceLinkAddr = new LinkProcAddr(_PriceLinkStateChange);
            m_ContractStatusAddr = new StatusProcAddr(_ContractStatusChange);
            m_TickAddr = new TickerUpdateProcAddr(_TickChange);
            m_PriceAddr = new PriceProcAddr(_PriceChange);
            m_FillAddr = new FillProcAddr(_FillChange);
            m_OrderAddr = new OrderProcAddr(_OrderChange);

            result = ptRegisterCallback((int)CallbackType.ptLogonStatus, m_LogonAddr);
            isValid(result, nameof(ptRegisterCallback));
            result = ptRegisterCallback((int)CallbackType.ptForcedLogout, m_ForcedLogoutAddr);
            isValid(result, nameof(ptRegisterCallback));
            result = ptRegisterCallback((int)CallbackType.ptDataDLComplete, m_DLCompleteAddr);
            isValid(result, nameof(ptRegisterCallback));
            result = ptRegisterLinkStateCallback((int)CallbackType.ptHostLinkStateChange, m_HostLinkAddr);
            isValid(result, nameof(ptRegisterLinkStateCallback));
            result = ptRegisterLinkStateCallback((int)CallbackType.ptPriceLinkStateChange, m_PriceLinkAddr);
            isValid(result, nameof(ptRegisterLinkStateCallback));
            result = ptRegisterStatusCallback((int)CallbackType.ptStatusChange, m_ContractStatusAddr);
            isValid(result, nameof(ptRegisterStatusCallback));
            result = ptRegisterTickerCallback((int)CallbackType.ptTickerUpdate, m_TickAddr);
            isValid(result, nameof(ptRegisterTickerCallback));
            result = ptRegisterPriceCallback((int)CallbackType.ptPriceUpdate, m_PriceAddr);
            isValid(result, nameof(ptRegisterPriceCallback));
            result = ptRegisterFillCallback((int)CallbackType.ptFill, m_FillAddr);
            isValid(result, nameof(ptRegisterFillCallback));
            result = ptRegisterOrderCallback((int)CallbackType.ptOrder, m_OrderAddr);
            isValid(result, nameof(ptRegisterOrderCallback));
            return true;
        }

        #region Delegate
        private void _LogonStatus()
        {
            int result = ptGetLogonStatus(ref m_LogonStatus);
            isValid(result, nameof(_LogonStatus));
            DoLogon = false;
            OnConnectStateChanged?.Invoke(this, new ConnectStateEventArgs(HostState, PriceState, LogonState, DLComplete, isConnected));
            m_Log.Info($"{nameof(_LogonStatus)}:{m_LogonStatus.Status}");
        }
        private void _ForcedLogout()
        {
            m_LogonStatus.Status = (byte)LogonState.ptForcedOut;
            OnConnectStateChanged?.Invoke(this, new ConnectStateEventArgs(HostState, PriceState, LogonState, DLComplete, isConnected));
            m_Log.Info($"{nameof(_ForcedLogout)}:{m_LogonStatus.Status}");
        }
        private void _DataDLComplete()
        {
            DLComplete = true;

            if (m_Subs != null && m_Subs.Count > 0)
            {
                foreach (var item in m_Subs.Keys)
                {
                    Subscribe(item);
                    //string[] items = item.Split(',');
                    //if (items.Length < 3) { continue; }
                    //isValid(ptSubscribePrice(items[0], items[1], items[2]), nameof(ptSubscribePrice));
                }
            }
            OnConnectStateChanged?.Invoke(this, new ConnectStateEventArgs(HostState, PriceState, LogonState, DLComplete, isConnected));
            m_Log.Info($"Raise Download Complete");
            GetOrders();
            GetFills();
        }
        private void _HostLinkStateChange(ref LinkStateStruct linkState)
        {
            Enum.TryParse(((int)linkState.NewState).ToString(), out m_HostState);
            if (HostState == SocketLinkState.ptLinkConnected)
            {
                int reTry = 5;
                while (reTry > 0 && !DoLogon)
                {
                    if (reTry != 5) { Thread.Sleep(1000); }
                    LogonStruct logonDetails = new LogonStruct { UserID = UserID, Password = Pwd, Reset = 'N', Reports = 'N' };
                    //ptLogOn(ref logonDetails);
                    int result = ptLogOn(ref logonDetails);
                    DoLogon = isValid(result, nameof(_HostLinkStateChange));
                    reTry = !DoLogon ? reTry - 1 : 0;
                }
            }
            OnConnectStateChanged?.Invoke(this, new ConnectStateEventArgs(HostState, PriceState, LogonState, DLComplete, isConnected));
            m_Log.Info($"{nameof(_HostLinkStateChange)}:{HostState}");
        }
        private void _PriceLinkStateChange(ref LinkStateStruct linkState)
        {
            Enum.TryParse(((int)linkState.NewState).ToString(), out m_PriceState);
            OnConnectStateChanged?.Invoke(this, new ConnectStateEventArgs(HostState, PriceState, LogonState, DLComplete, isConnected));
            m_Log.Info($"{nameof(_PriceLinkStateChange)}:{PriceState}");
        }
        private void _ContractStatusChange(ref StatusUpdStruct status)
        {
            ContractDateMarketStatus mktstatus;
            Enum.TryParse(status.Status.ToString(), out mktstatus);
            m_Log.Info($"{nameof(_ContractStatusChange)}:{GetContractKey(status.ExchangeName, status.ContractName, status.ContractDate)}-{mktstatus}");
        }
        private void _TickChange(ref TickerUpdStruct tick)
        {
            //tick.Last ==> LastPrice有變動
            //if (tick.Last == 'N' || !m_Subs.ContainsKey(key)) { return; }            
            if (!m_Subs.ContainsKey(tick.Key)) { return; }

            decimal last, bid, offer;
            //不管Last有沒有變 都要抓
            if (!decimal.TryParse(tick.LastPrice, out last)) { last = MktPrice.NULLVALUE; }
            if (tick.Offer == 'N')
            {
                offer = MktPrice.NULLVALUE;
            }
            else if (!decimal.TryParse(tick.OfferPrice, out offer))
            {
                offer = MktPrice.NULLVALUE;
            }
            if (tick.Bid == 'N')
            {
                bid = MktPrice.NULLVALUE;
            }
            else if (!decimal.TryParse(tick.BidPrice, out bid))
            {
                bid = MktPrice.NULLVALUE;
            }
            #region DEBUG
            //if (tick.Last == 'Y')
            //{
            //    PriceStruct p;
            //    ptGetPriceForContract(tick.ExchangeName, tick.ContractName, tick.ContractDate, out p);
            //    Console.WriteLine(tick.Last + " " + tick.LastPrice + " " + tick.LastVolume + ":" + p.Last0.Price + " " + p.Last0.Volume);
            //}
            #endregion
            RaiseMktPrice(tick.Key, MktPrice.NULLVALUE, last, offer, bid, tick.OfferVolume, tick.BidVolume);
        }
        private void _PriceChange(ref PriceUpdStruct price)
        {
            string key = price.Key;
            if (!m_Subs.ContainsKey(key)) { return; }

            decimal close, last, offer, bid;
            int offervolume, bidvolume;
            PriceStruct p;
            ptGetPriceForContract(price.ExchangeName, price.ContractName, price.ContractDate, out p);

            #region OldVersion
            //if ((p.ChangeMask & (int)PriceChange.ptChangeLast) != (int)PriceChange.ptChangeLast &&
            //    (p.ChangeMask & (int)PriceChange.ptChangeClosing) != (int)PriceChange.ptChangeClosing &&
            //    (p.ChangeMask & (int)PriceChange.ptChangeBidDOM) != (int)PriceChange.ptChangeBidDOM &&
            //    (p.ChangeMask & (int)PriceChange.ptChangeOfferDOM) != (int)PriceChange.ptChangeOfferDOM)
            //{
            //    return;
            //}
            //RaiseMktPrice(key, decimal.Parse(p.Closing.Price), decimal.Parse(p.Last0.Price), decimal.Parse(p.OfferDOM0.Price), decimal.Parse(p.BidDOM0.Price), p.OfferDOM0.Volume, p.BidDOM0.Volume);
            #endregion
            //不管Last有沒有變 都要抓
            if (!decimal.TryParse(p.Last0.Price, out last)) { last = MktPrice.NULLVALUE; }
            if (!decimal.TryParse(p.Closing.Price, out close))
            {
                //沒有昨收改抓昨日結算價
                if (!decimal.TryParse(p.pvSODStl.Price, out close))
                {
                    close = MktPrice.NULLVALUE;
                }
            }

            if (((p.ChangeMask & (int)PriceChange.ptChangeOfferDOM) != (int)PriceChange.ptChangeOfferDOM))
            {
                offer = MktPrice.NULLVALUE;
                offervolume = -1;
            }
            else
            {
                if (!decimal.TryParse(p.OfferDOM0.Price, out offer)) { offer = MktPrice.NULLVALUE; }
                offervolume = p.OfferDOM0.Volume;
            }
            if (((p.ChangeMask & (int)PriceChange.ptChangeBidDOM) != (int)PriceChange.ptChangeBidDOM)
            )
            {
                bid = MktPrice.NULLVALUE;
                bidvolume = -1;
            }
            else
            {
                if (!decimal.TryParse(p.BidDOM0.Price, out bid)) { bid = MktPrice.NULLVALUE; }
                bidvolume = p.BidDOM0.Volume;
            }

            RaiseMktPrice(key, close, last, offer, bid, offervolume, bidvolume);
            OnPriceUpdate?.Invoke(key, p);
        }
        private void _FillChange(ref FillUpdStruct fillUpd)
        {
            FillStruct fill;
            int result = ptGetFillByID(fillUpd.FillID, out fill);
            if (isValid(result, nameof(_FillChange)))
            {
                _Fills(fill);
                OnFillReply?.Invoke(fill.Key, fill);
            }
            else
            {

            }
        }
        private void _OrderChange(ref OrderUpdStruct orderUpd)
        {
            OrderDetailStruct order;
            int result = ptGetOrderByID(orderUpd.OrderID, out order, 0);
            if (!isValid(result, nameof(_OrderChange)))
            {
                return;
            }
            string key = order.Key;
            _Order(key, order);

            switch (order.State)
            {
                case OrderState.ptQueued:
                case OrderState.ptSent:
                case OrderState.ptWorking:
                    _Valid(key, order);
                    OnOrderReply?.Invoke(key, order);
                    break;
                case OrderState.ptRejected:
                case OrderState.ptExternalCancelled:
                    _Error(key, order);
                    _RemoveValid(key, order.OrderID);
                    OnErrorReply?.Invoke(order, order.State.ToString());
                    break;
                case OrderState.ptCancelled:
                case OrderState.ptCancelPending:
                    _Cancel(key, order);
                    _RemoveValid(key, order.OrderID);
                    OnCancelReply?.Invoke(key, order);
                    break;
                case OrderState.ptPartFilled:
                case OrderState.ptFilled:
                case OrderState.ptUnconfirmedFilled:
                case OrderState.ptUnconfirmedPartFilled:
                    _FillRemoveValid(key, order.OrderID);
                    break;
            }
        }
        #endregion

        #region Public
        public void Connect()
        {
            //Link to Server
            int result = ptSetSSL('N');
            isValid(result, nameof(ptSetSSL));
            result = ptSetHostAddress(OrderIP, OrderPort);
            isValid(result, nameof(ptSetHostAddress));
            result = ptSetPriceAddress(PriceIP, PricePort);
            isValid(result, nameof(ptSetPriceAddress));
            result = ptReady();
            isValid(result, nameof(ptReady));
        }
        public void Disconnect()
        {
            if (m_Subs.Count > 0)
            {
                for (int i = m_Subs.Count - 1; i >= 0; i--)
                {
                    Unsubscribe(m_Subs.ElementAt(i).Key, true);
                }
            }
            int result = ptDisconnect();
            isValid(result, nameof(ptDisconnect));
        }
        public bool Subscribe(string exchange, string commodity, string date, string key = "")
        {
            if (string.IsNullOrEmpty(key)) { key = GetContractKey(exchange, commodity, date); }            
            if (!m_Subs.ContainsKey(key))
            {
                m_Subs.Add(key, 1);
                int result = ptSubscribePrice(exchange, commodity, date);
                return isValid(result, nameof(ptSubscribePrice));
            }
            m_Subs[key]++;
            if (m_Newest.ContainsKey(key)) { RaiseMktPrice(m_Newest[key]); }
            return true;
        }
        public bool Subscribe(string substr)
        {
            if (string.IsNullOrEmpty(substr)) { return false; }
            string[] items = substr.Split(',');
            if (items.Length < 3) { return false; }
            return Subscribe(items[0], items[1], items[2], substr);
        }
        public void Unsubscribe(string unsubstr, bool ignoreCnt = false)
        {
            if (string.IsNullOrEmpty(unsubstr)) { return; }
            var items = unsubstr.Split('.');
            if (items.Length < 3) { return; }
            Unsubscribe(items[0], items[1], items[2], unsubstr, ignoreCnt);
        }
        public void Unsubscribe(string exch, string commodity, string date, string key = "", bool ignoreCnt = false)
        {
            if (string.IsNullOrEmpty(key)) { key = GetContractKey(exch, commodity, date); }
            if (!isConnected) { return; }
            if (!m_Subs.ContainsKey(key)) { return; }
            m_Subs[key]--;

            if ((m_Subs[key] <= 0) || ignoreCnt)
            {
                m_Subs.Remove(key);
            }
        }
        public void UnsubscribeAll()
        {
            foreach (var sub in m_Subs.Keys)
            {
                string[] items = sub.Split(',');
                if (items.Length < 3) { continue; }
                int result = ptUnsubscribePrice(items[0], items[1], items[2]);
                isValid(result, nameof(ptUnsubscribePrice));
            }
            m_Subs.Clear();
        }
        public void RefreshPrice(string exch, string commodity, string date)
        {
            int result = ptPriceSnapshot(exch, commodity, date, 0);
            isValid(result, nameof(ptPriceSnapshot));
        }

        public bool DoCancel(string orderID)
        {
            int result = ptCancelOrder(orderID);
            return isValid(result, nameof(ptCancelOrder));
        }
        public bool DoCancel(string exch, string commodity, string date, string traderAccount)
        {
            int result = ptCancelOrders(traderAccount, exch, commodity, date);
            return isValid(result, nameof(ptCancelOrders));
        }
        public bool DoCancelBuys(string exch, string commodity, string date, string traderAccount)
        {
            int result = ptCancelBuys(traderAccount, exch, commodity, date);
            return isValid(result, nameof(ptCancelBuys));
        }
        public bool DoCancelSells(string exch, string commodity, string date, string traderAccount)
        {
            int result = ptCancelSells(traderAccount, exch, commodity, date);
            return isValid(result, nameof(ptCancelSells));
        }
        public bool DoCancelAll(string traderAccount)
        {
            int result = ptCancelAll(traderAccount);
            return isValid(result, nameof(ptCancelAll));
        }
        public bool DoOrder(string exch, string commodity, string date, string orderType, string account, string price, int lots, bool buy, bool active, out string orderID)
        {
            NewOrderStruct newOrder = new NewOrderStruct
            {
                ExchangeName = exch,
                ContractName = commodity,
                ContractDate = date,
                OrderType = orderType,
                TraderAccount = account,
                Price = price,
                Price2 = price,
                Lots = lots,
                LocalTrader = account,
                Inactive = active ? 'Y' : 'N',
                BuyOrSell = buy ? 'B' : 'S'
            };
            int result = ptAddOrder(newOrder, out orderID);
            return isValid(result, nameof(ptAddOrder));
        }

        public IEnumerable<OrderDetailStruct> OrderRPTs(string contractKey = "")
        {
            if (!string.IsNullOrEmpty(contractKey))
            {

                if (m_OrdersByCKey == null || !m_OrdersByCKey.ContainsKey(contractKey))
                {
                    return null;
                }
                return m_OrdersByCKey[contractKey];
            }
            else
            {
                if (m_OrdersByID == null)
                {
                    return null;
                }
                return m_OrdersByID.Values;
            }
        }
        public IEnumerable<FillStruct> DealRPTs(string contractKey = "")
        {
            if (!string.IsNullOrEmpty(contractKey))
            {
                if (m_DealsByCKey == null || !m_DealsByCKey.ContainsKey(contractKey))
                {
                    return null;
                }
                return m_DealsByCKey[contractKey];
            }
            else
            {
                if (m_DealsByID == null)
                {
                    return null;
                }
                return m_DealsByID.Values;
            }
        }
        public IEnumerable<OrderDetailStruct> ValidRPTs(string contractKey)
        {
            if (!string.IsNullOrEmpty(contractKey))
            {
                if (m_ValidsByCKey== null || !m_ValidsByCKey.ContainsKey(contractKey))
                {
                    return null;
                }
                return m_ValidsByCKey[contractKey];
            }
            else
            {
                if (m_ValidsByID== null)
                {
                    return null;
                }
                return m_ValidsByID.Values;
            }
        }
        public IEnumerable<OrderDetailStruct> CancelRPTs(string contractKey = "")
        {
            if (!string.IsNullOrEmpty(contractKey))
            {
                if (m_CancelsByCKey == null || !m_CancelsByCKey.ContainsKey(contractKey))
                {
                    return null;
                }
                return m_CancelsByCKey[contractKey];
            }
            else
            {
                if (m_CancelsByID == null)
                {
                    return null;
                }
                return m_CancelsByID.Values;
            }
        }
        public IEnumerable<OrderDetailStruct> ErrRPTs(string contractKey = "")
        {
            if (!string.IsNullOrEmpty(contractKey))
            {
                if (m_ErrsByCKey == null || !m_ErrsByCKey.ContainsKey(contractKey))
                {
                    return null;
                }
                return m_ErrsByCKey[contractKey];
            }
            else
            {
                if (m_ErrsByID == null)
                {
                    return null;
                }
                return m_ErrsByID.Values;
            }
        }
        public IEnumerable<Summary> Summaries()
        {
            return m_Summary != null ? m_Summary.Values : null;
        }
        //public void OrderType()
        //{
        //    //For Test??
        //    int cnt ;
        //    ptCountOrderTypes(out cnt);
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        OrderTypeStruct ordertype;
        //        string t;
        //        ptGetOrderType(i, out ordertype, out t);
        //    }
        //}
        public Dictionary<string, List<string>> OrderType()
        {
            Dictionary<string, List<string>> re = new Dictionary<string, List<string>>();
            int count = -1;
            int result = ptCountOrderTypes(out count);
            isValid(result, nameof(ptCountOrderTypes));
            for (int i = 0; i < count; i++)
            {
                OrderTypeStruct ordertype;
                string str;
                result = ptGetOrderType(i, out ordertype, out str);
                if (isValid(result, nameof(ptGetOrderType)))
                {
                    continue;
                }
                if (!re.ContainsKey(ordertype.ExchangeName))
                {
                    re.Add(ordertype.ExchangeName, new List<string>());
                }
                re[ordertype.ExchangeName].Add(ordertype.OrderType);
            }
            return re;
        }
        public PositionStruct Position(string traderAccount)
        {
            PositionStruct ps;
            int result = ptGetTotalPosition(traderAccount, out ps);
            isValid(result, nameof(ptGetTotalPosition));
            return ps;
        }
        public PositionStruct Position(string exch, string commodity, string date, string traderAccount)
        {
            PositionStruct ps;
            int result = ptGetOpenPosition(exch, commodity, date, traderAccount, out ps);
            isValid(result, nameof(ptGetOpenPosition));
            return ps;
        }
        public PriceStruct Price(string exch, string commodity, string date)
        {
            PriceStruct price;
            ptGetPriceForContract(exch, commodity, date, out price);
            return price;
        }
        public PriceStruct Price(int index)
        {
            PriceStruct price;
            ptGetPrice(index, out price);
            return price;
        }
        public PriceStruct Price(ContractStruct contract)
        {
            return Price(contract.ExchangeName, contract.ContractName, contract.ContractDate);
        }
        public ContractStruct Product(string exch, string commodity, string date)
        {
            ContractStruct contract;
            int result = ptGetContractByName(exch, commodity, date, out contract);
            isValid(result, nameof(ptGetContractByName));
            return contract;
        }
        public ContractStruct Product(int index)
        {
            ContractStruct contract;
            int result = ptGetContract(index, out contract);
            isValid(result, nameof(ptGetContract));
            return contract;
        }
        public Dictionary<int, ContractStruct> Products()
        {
            if (!DLComplete) { return null; }
            Dictionary<int, ContractStruct> re = new Dictionary<int, ContractStruct>();
            int cnt;
            int result = ptCountContracts(out cnt);
            isValid(result, nameof(ptCountContracts));
            for (int i = 0; i < cnt; i++)
            {
                ContractStruct c;
                result = ptGetContract(i, out c);
                if (!isValid(result, nameof(ptGetContract)))
                {
                    continue;
                }
                if (c.Tradable != 'Y') { continue; }
                //其他的商品 (ex:Option)
                if (c.ContractDate.Split(' ').Length > 1) { continue; }
                //其他條件, 先拿掉
                //if (!string.IsNullOrEmpty(exch) && c.ExchangeName != exch) { continue; }
                //if (!string.IsNullOrEmpty(c) && c.ContractName != c) { continue; }
                //if (!string.IsNullOrEmpty(date) && c.ContractDate != date) { continue; }
                re.Add(i, c);
            }
            return re;
        }
        public TraderAcctStruct Trader(int index)
        {
            TraderAcctStruct trader;
            int result = ptGetTrader(index, out trader);
            isValid(result, nameof(ptGetTrader));
            return trader;        
        }
        public Dictionary<int, TraderAcctStruct> Traders()
        {
            if (!DLComplete) { return null; }
            Dictionary<int, TraderAcctStruct> re = new Dictionary<int, TraderAcctStruct>();
            int cnt;
            int result = ptCountTraders(out cnt);
            isValid(result, nameof(ptCountTraders));
            for (int i = 0; i < cnt; i++)
            {
                TraderAcctStruct acc;
                result = ptGetTrader(i, out acc);
                if (!isValid(result, nameof(ptGetTrader)))
                {
                    continue;
                }
                if (acc.Tradable == 'Y') { re.Add(i, acc); }
            }
            return re;
        }
        #endregion

        private void GetOrders()
        {
            int orderCnt;
            m_OrdersByID = new Dictionary<string, OrderDetailStruct>();
            m_OrdersByCKey = new Dictionary<string, List<OrderDetailStruct>>();
            m_ValidsByCKey = new Dictionary<string, List<OrderDetailStruct>>();
            m_ValidsByID = new Dictionary<string, OrderDetailStruct>();
            m_CancelsByCKey = new Dictionary<string, List<OrderDetailStruct>>();
            m_CancelsByID = new Dictionary<string, OrderDetailStruct>();
            m_ErrsByCKey = new Dictionary<string, List<OrderDetailStruct>>();
            m_ErrsByID = new Dictionary<string, OrderDetailStruct>();
            m_Summary = new Dictionary<string, Summary>();
            int result = ptCountOrders(out orderCnt);
            isValid(result, nameof(ptCountOrders));
            for (int i = 0; i < orderCnt; i++)
            {
                OrderDetailStruct detail;
                result = ptGetOrder(i, out detail);
                if (!isValid(result, nameof(ptGetOrder)))
                {
                    continue;
                }
                string key = detail.Key;
                _Order(key, detail);
                switch (detail.State)
                {
                    case OrderState.ptQueued:
                    case OrderState.ptSent:
                    case OrderState.ptWorking:
                        _Valid(key, detail);
                        OnOrderReply?.Invoke(key, detail);
                        break;

                    case OrderState.ptRejected:
                    case OrderState.ptExternalCancelled:
                        _Error(key, detail);
                        _RemoveValid(key, detail.OrderID);
                        OnErrorReply?.Invoke(detail, detail.State.ToString());
                        break;

                    case OrderState.ptCancelled:
                    case OrderState.ptCancelPending:
                        _Cancel(key, detail);
                        _RemoveValid(key, detail.OrderID);
                        OnCancelReply?.Invoke(key, detail);
                        break;

                    case OrderState.ptPartFilled:
                    case OrderState.ptFilled:
                    case OrderState.ptUnconfirmedFilled:
                    case OrderState.ptUnconfirmedPartFilled:
                        _FillRemoveValid(key, detail.OrderID);
                        break;
                }
            }
        }
        private void GetFills()
        {
            int fillCnt;

            m_DealsByID = new Dictionary<string, FillStruct>();
            m_DealsByCKey = new Dictionary<string, List<FillStruct>>();
            int result = ptCountFills(out fillCnt);
            isValid(result, nameof(ptCountFills));

            for (int i = 0; i < fillCnt; i++)
            {
                FillStruct fill;
                result = ptGetFill(i, out fill);
                if (!isValid(result, nameof(ptGetFill)))
                {
                    continue;
                }
                _Fills(fill);
                OnFillReply?.Invoke(fill.Key, fill);
            }
        }
        private bool isValid(int ret, string functionName)
        {
            if (ret == 0)
            {
                m_Log.Info($"[{functionName}]: Valid");
            }
            else
            {
                m_Log.Error($"[{functionName}]:{ptGetErrorMessage(ret)}");
            }
            return ret == 0;
        }
        private void _Cancel(string contractKey, OrderDetailStruct order)
        {
            if (!m_CancelsByCKey.ContainsKey(contractKey))
            {
                m_CancelsByCKey.Add(contractKey, new List<OrderDetailStruct>());
            }
            m_CancelsByCKey[contractKey].Add(order);
            if (!m_CancelsByID.ContainsKey(order.OrderID))
            {
                m_CancelsByID.Add(order.OrderID, order);
            }
        }
        private void _Order(string contractKey, OrderDetailStruct order)
        {
            if (!m_OrdersByCKey.ContainsKey(contractKey))
            {
                m_OrdersByCKey.Add(contractKey, new List<OrderDetailStruct>());
            }
            m_OrdersByCKey[contractKey].Add(order);
            if (!m_OrdersByID.ContainsKey(order.OrderID))
            {
                m_OrdersByID.Add(order.OrderID, order);
            }
        }
        private void _Valid(string contractKey, OrderDetailStruct order)
        {
            if (!m_ValidsByCKey.ContainsKey(contractKey))
            {
                m_ValidsByCKey.Add(contractKey, new List<OrderDetailStruct>());
            }
            m_ValidsByCKey[contractKey].Add(order);
            if (!m_ValidsByID.ContainsKey(order.OrderID))
            {
                m_ValidsByID.Add(order.OrderID, order);
            }
        }
        private void _Error(string contractKey, OrderDetailStruct order)
        {
            if (!m_ErrsByCKey.ContainsKey(contractKey))
            {
                m_ErrsByCKey.Add(contractKey, new List<OrderDetailStruct>());
            }
            m_ErrsByCKey[contractKey].Add(order);
            if (!m_ErrsByID.ContainsKey(order.OrderID))
            {
                m_ErrsByID.Add(order.OrderID, order);
            }
        }
        private void _Fills(FillStruct fill)
        {
            if (fill.FillType == (int)FillType.ptNettedFill) { return; }

            double price;
            string key = fill.Key;
            if (!m_DealsByCKey.ContainsKey(key))
            {
                m_DealsByCKey.Add(key, new List<FillStruct>());
            }
            m_DealsByCKey[key].Add(fill);
            if (!m_DealsByID.ContainsKey(fill.FillId))
            {
                m_DealsByID.Add(fill.FillId, fill);
            }
            _FillRemoveValid(key, fill.OrderID);
            if (!m_Summary.ContainsKey(key))
            {
                Summary summary = new Summary
                {
                    ExchangeName = fill.ExchangeName,
                    ContractName = fill.ContractName,
                    ContractDate = fill.ContractDate,
                    Key = key,
                    TraderAccount = fill.TraderAccount,
                    ALots = 0,
                    AAmount = 0D,
                    BLots = 0,
                    BAmount = 0D
                };
                m_Summary.Add(key, summary);
            }
            double.TryParse(fill.Price, out price);
            if (fill.BuyOrSell == 'B')
            {
                m_Summary[key].BLots += fill.Lots;
                m_Summary[key].BAmount += fill.Lots * price;
            }
            else
            {
                m_Summary[key].ALots += fill.Lots;
                m_Summary[key].AAmount += fill.Lots * price;
            }
        }
        private void _RemoveValid(string contractKey, string orderid)
        {
            if (!m_OrdersByID.ContainsKey(orderid)) { return; }

            OrderDetailStruct item = m_OrdersByID[orderid];
            if (m_ValidsByID.ContainsKey(item.OrderID))
            {
                m_ValidsByCKey[contractKey].Remove(item);
                m_ValidsByID.Remove(item.OrderID);
            }
        }
        private void _FillRemoveValid(string contractKey, string orderid)
        {
            if (!m_OrdersByID.ContainsKey(orderid)) { return; }

            OrderDetailStruct item = m_OrdersByID[orderid];
            if (m_ValidsByID.ContainsKey(item.OrderID) && (item.AmountFilled == item.Lots))
            {
                m_ValidsByCKey[contractKey].Remove(item);
                m_ValidsByID.Remove(item.OrderID);
            }
        }

        private string GetContractKey(string exch, string contract, string date)
        {
            return $"{exch},{contract},{date}";
        }


        //#region Delegate
        //private bool Initialization()
        //{
        //    string path = System.Windows.Forms.Application.StartupPath + "\\PATSLogs\\";
        //    if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
        //    ptSetClientPath(path);
        //    if (!isValid(ptInitialise((char)EnvironmentType.ptClient, VER, APPID, APPVER, LID, false), "Init")) { return false; }
        //    ptEnable(LEVEL);

        //    m_LogonAddr = new ProcAddr(_LogonStatus);
        //    m_ForcedLogoutAddr = new ProcAddr(_ForcedLogout);
        //    m_DLCompleteAddr = new ProcAddr(_DataDLComplete);
        //    m_HostLinkAddr = new LinkProcAddr(_HostLinkStateChange);
        //    m_PriceLinkAddr = new LinkProcAddr(_PriceLinkStateChange);
        //    m_ContractStatusAddr = new StatusProcAddr(_ContractStatusChange);
        //    m_TickAddr = new TickerUpdateProcAddr(_TickChange);
        //    m_PriceAddr = new PriceProcAddr(_PriceChange);
        //    m_FillAddr = new FillProcAddr(_FillChange);
        //    m_OrderAddr = new OrderProcAddr(_OrderChange);

        //    //Logon Status
        //    isValid(ptRegisterCallback((int)CallbackType.ptLogonStatus, m_LogonAddr), "LogonStatus Init");
        //    //Forced Logout
        //    isValid(ptRegisterCallback((int)CallbackType.ptForcedLogout, m_ForcedLogoutAddr), "ForcedLogout Init");
        //    //Download Complete
        //    isValid(ptRegisterCallback((int)CallbackType.ptDataDLComplete, m_DLCompleteAddr), "DLComplete Init");
        //    //Host Link
        //    isValid(ptRegisterLinkStateCallback((int)CallbackType.ptHostLinkStateChange, m_HostLinkAddr), "HostLinkStateChange Init");
        //    //Price Link
        //    isValid(ptRegisterLinkStateCallback((int)CallbackType.ptPriceLinkStateChange, m_PriceLinkAddr), "PriceLinkStateChange Init");
        //    //ContractStatus
        //    isValid(ptRegisterStatusCallback((int)CallbackType.ptStatusChange, m_ContractStatusAddr), "StatusChange Init");
        //    //TickUpdate
        //    isValid(ptRegisterTickerCallback((int)CallbackType.ptTickerUpdate, m_TickAddr), "TickerUpdate Init");
        //    //PriceUpdate
        //    isValid(ptRegisterPriceCallback((int)CallbackType.ptPriceUpdate, m_PriceAddr), "PriceUpdate Init");
        //    isValid(ptRegisterFillCallback((int)CallbackType.ptFill, m_FillAddr), "Fill Init");
        //    isValid(ptRegisterOrderCallback((int)CallbackType.ptOrder, m_OrderAddr), "Order Init");
        //    return true;
        //}
        //private void _HostLinkStateChange(ref LinkStateStruct linkState)
        //{
        //    Enum.TryParse<SocketLinkState>(((int)linkState.NewState).ToString(), out m_HostState);
        //    switch (HostState)
        //    {
        //        case SocketLinkState.ptLinkConnected:
        //            //Console.WriteLine("Host Link Connected");
        //            if (!DoLogon)
        //            {
        //                Thread.Sleep(1000);
        //                LogonStruct logonStruct = new LogonStruct() { UserID = UserID, Password = Pwd, Reset = 'N', Reports = 'N' };
        //                DoLogon = isValid(ptLogOn(ref logonStruct), "_HostLinkStateChange");
        //            }
        //            break;
        //        case SocketLinkState.ptLinkOpened:
        //            break;
        //        case SocketLinkState.ptLinkConnecting:
        //            break;
        //        case SocketLinkState.ptLinkClosed:
        //            break;
        //        case SocketLinkState.ptLinkInvalid:
        //            break;
        //        default:
        //            break;
        //    }
        //    RaiseEvent(new ConnectStateEventArgs(HostState, PriceState, LogonState, DLComplete, isConnected));
        //}
        //private void _PriceLinkStateChange(ref LinkStateStruct linkState)
        //{
        //    Enum.TryParse<SocketLinkState>(((int)linkState.NewState).ToString(), out m_PriceState);
        //    //Console.WriteLine("Price Link Connected");
        //    RaiseEvent(new ConnectStateEventArgs(HostState, PriceState, LogonState, DLComplete, isConnected));
        //}
        //private void _ContractStatusChange(ref StatusUpdStruct status)
        //{
        //    //Console.WriteLine(status.ExchangeName + status.ContractName + status.ContractDate + "  " + Enum.Parse(typeof(ContractDateMarketStatus), status.Status.ToString()));
        //}
        //private void _LogonStatus()
        //{
        //    isValid(ptGetLogonStatus(ref m_LogonStatus), "_LogonStatus");
        //    switch (LogonState)
        //    {
        //        case LogonState.ptLogonFailed:
        //            //登入失敗，本情況不會在正式環境中出現
        //            break;
        //        case LogonState.ptLogonSucceeded:
        //            //Console.WriteLine("Logon Succeeded");
        //            //登入成功
        //            break;
        //        case LogonState.ptForcedOut:
        //            //強制登出
        //            break;
        //        case LogonState.ptObsoleteVers:
        //            //ptInitialise呼叫方式在現行API已經不支援
        //            break;
        //        case LogonState.ptWrongEnv:
        //            //請勿在正式環境中使用測試環境設定
        //            break;
        //        case LogonState.ptDatabaseErr:
        //            //Pat System 無法連結至資料庫
        //            break;
        //        case LogonState.ptInvalidUser:
        //            //未在系統註冊的使用者
        //            break;
        //        case LogonState.ptLogonRejected:
        //            //使用者被拒絕登入，可能的原因是帳號被停用或是提供錯誤的密碼
        //            break;
        //        case LogonState.ptInvalidAppl:
        //            //ApplicationID或LicenseID錯誤
        //            break;
        //        case LogonState.ptLoggedOn:
        //            //該帳戶在其他地方已經為登入狀態
        //            break;
        //        case LogonState.ptInvalidLogonState:
        //            //登入時發生未知的錯誤
        //            break;
        //        default:
        //            break;
        //    }
        //    DoLogon = false;
        //    RaiseEvent(new ConnectStateEventArgs(HostState, PriceState, LogonState, DLComplete, isConnected));
        //}
        //private void _ForcedLogout()
        //{
        //    m_LogonStatus.Status = (byte)LogonState.ptForcedOut;
        //    RaiseEvent(new ConnectStateEventArgs(HostState, PriceState, LogonState, DLComplete, isConnected));
        //}


        //private void _TickChange(ref TickerUpdStruct tick)
        //{
        //    string key = GetContractKey(tick);
        //    //tick.Last ==> LastPrice有變動
        //    //if (tick.Last == 'N' || !m_Subs.ContainsKey(key)) { return; }            
        //    if (!m_Subs.ContainsKey(key)) { return; }

        //    decimal last, bid, offer;
        //    int offervolume, bidvolume;
        //    //不管Last有沒有變 都要抓
        //    if (!decimal.TryParse(tick.LastPrice, out last)) { last = MktPrice.NULLVALUE; }
        //    if (tick.Offer == 'N')
        //    {
        //        offer = MktPrice.NULLVALUE;
        //        offervolume = -1;
        //    }
        //    else
        //    {
        //        if (!decimal.TryParse(tick.OfferPrice, out offer)) { offer = MktPrice.NULLVALUE; }
        //        offervolume = tick.OfferVolume;
        //    }
        //    if (tick.Bid == 'N')
        //    {
        //        bid = MktPrice.NULLVALUE;
        //        bidvolume = -1;
        //    }
        //    else
        //    {
        //        if (!decimal.TryParse(tick.BidPrice, out bid)) { bid = MktPrice.NULLVALUE; }
        //        bidvolume = tick.BidVolume;
        //    }
        //    #region DEBUG
        //    //if (tick.Last == 'Y')
        //    //{
        //    //    PriceStruct p;
        //    //    ptGetPriceForContract(tick.ExchangeName, tick.ContractName, tick.ContractDate, out p);
        //    //    Console.WriteLine(tick.Last + " " + tick.LastPrice + " " + tick.LastVolume + ":" + p.Last0.Price + " " + p.Last0.Volume);
        //    //}
        //    #endregion
        //    RaiseMktPrice(key, MktPrice.NULLVALUE, last, offer, bid, tick.OfferVolume, tick.BidVolume);
        //}
        //private void _PriceChange(ref PriceUpdStruct price)
        //{
        //    string key = GetContractKey(price);
        //    if (!m_Subs.ContainsKey(key)) { return; }

        //    decimal close, last, offer, bid;
        //    int offervolume, bidvolume;
        //    PriceStruct p;
        //    ptGetPriceForContract(price.ExchangeName, price.ContractName, price.ContractDate, out p);

        //    #region OldVersion
        //    //if ((p.ChangeMask & (int)PriceChange.ptChangeLast) != (int)PriceChange.ptChangeLast &&
        //    //    (p.ChangeMask & (int)PriceChange.ptChangeClosing) != (int)PriceChange.ptChangeClosing &&
        //    //    (p.ChangeMask & (int)PriceChange.ptChangeBidDOM) != (int)PriceChange.ptChangeBidDOM &&
        //    //    (p.ChangeMask & (int)PriceChange.ptChangeOfferDOM) != (int)PriceChange.ptChangeOfferDOM)
        //    //{
        //    //    return;
        //    //}
        //    //RaiseMktPrice(key, decimal.Parse(p.Closing.Price), decimal.Parse(p.Last0.Price), decimal.Parse(p.OfferDOM0.Price), decimal.Parse(p.BidDOM0.Price), p.OfferDOM0.Volume, p.BidDOM0.Volume);
        //    #endregion
        //    //不管Last有沒有變 都要抓
        //    if (!decimal.TryParse(p.Last0.Price, out last)) { last = MktPrice.NULLVALUE; }
        //    if (!decimal.TryParse(p.Closing.Price, out close))
        //    {
        //        //沒有昨收改抓昨日結算價
        //        if (!decimal.TryParse(p.pvSODStl.Price, out close))
        //        {
        //            close = MktPrice.NULLVALUE;
        //        }
        //    }

        //    if (((p.ChangeMask & (int)PriceChange.ptChangeOfferDOM) != (int)PriceChange.ptChangeOfferDOM))
        //    {
        //        offer = MktPrice.NULLVALUE;
        //        offervolume = -1;
        //    }
        //    else
        //    {
        //        if (!decimal.TryParse(p.OfferDOM0.Price, out offer)) { offer = MktPrice.NULLVALUE; }
        //        offervolume = p.OfferDOM0.Volume;
        //    }
        //    if (((p.ChangeMask & (int)PriceChange.ptChangeBidDOM) != (int)PriceChange.ptChangeBidDOM)
        //    )
        //    {
        //        bid = MktPrice.NULLVALUE;
        //        bidvolume = -1;
        //    }
        //    else
        //    {
        //        if (!decimal.TryParse(p.BidDOM0.Price, out bid)) { bid = MktPrice.NULLVALUE; }
        //        bidvolume = p.BidDOM0.Volume;
        //    }

        //    RaiseMktPrice(key, close, last, offer, bid, offervolume, bidvolume);
        //    RaiseEvent(key, p);
        //}
        //private void _FillChange(ref FillUpdStruct fillUpd)
        //{
        //    FillStruct fill;
        //    ptGetFillByID(fillUpd.FillID, out fill);
        //    RaiseEvent(GetContractKey(fill), fill);
        //}
        //private void _OrderChange(ref OrderUpdStruct orderUpd)
        //{
        //    OrderDetailStruct order;
        //    ptGetOrderByID(orderUpd.OrderID, out order);
        //    RaiseEvent(GetContractKey(order), order);
        //}
        //#endregion

        //#region Public
        //public void Connect()
        //{
        //    //Do Link to Server
        //    isValid(ptSetSSL('N'), "Connect");
        //    isValid(ptSetHostAddress(OrderIP, OrderPort), "Connect");
        //    isValid(ptSetPriceAddress(PriceIP, PricePort), "Connect");
        //    isValid(ptReady(), "Connect");
        //    //m_Subs = new Dictionary<string, int>();
        //}
        //public void Disconnect()
        //{
        //    if (m_Subs.Count > 0)
        //    {
        //        for (int i = m_Subs.Count - 1; i >= 0; i--)
        //        {
        //            Unsubscribe(m_Subs.ElementAt(i).Key, true);
        //        }
        //    }
        //    isValid(ptDisconnect(), "Disconnect");
        //}
        //public bool Subscribe(string exchange, string commodity, string date, string key = "")
        //{
        //    if (String.IsNullOrEmpty(key)) { key = GetContractKey(exchange, commodity, date); }
        //    //if (!isConnected) { return false; }
        //    //if (!m_Contracts.ContainsKey(key)) { return false; }
        //    if (!m_Subs.ContainsKey(key))
        //    {
        //        m_Subs.Add(key, 1);
        //        return isValid(ptSubscribePrice(exchange, commodity, date), "Subscribe");
        //    }
        //    m_Subs[key]++;
        //    if (m_Newest.ContainsKey(key)) { RaiseMktPrice(m_Newest[key]); }
        //    return true;
        //}
        //public bool Subscribe(string substr)
        //{
        //    if (string.IsNullOrEmpty(substr)) { return false; }
        //    string[] items = substr.Split(',');
        //    if (items.Length < 3) { return false; }
        //    return Subscribe(items[0], items[1], items[2], substr);
        //}
        //public void Unsubscribe(string exchange, string commodity, string date, string key = "", bool ignoreCnt = false)
        //{
        //    if (String.IsNullOrEmpty(key)) { key = GetContractKey(exchange, commodity, date); }
        //    if (!isConnected) { return; }
        //    //if (!m_Contracts.ContainsKey(key)) { return; }
        //    if (!m_Subs.ContainsKey(key)) { return; }
        //    m_Subs[key]--;
        //    if (m_Subs[key] <= 0 || ignoreCnt)
        //    {
        //        isValid(ptUnsubscribePrice(exchange, commodity, date), "Unsubscribe");
        //        m_Subs.Remove(key);
        //    }
        //}
        //public void Unsubscribe(string unsubstr, bool ignoreCnt = false)
        //{
        //    if (string.IsNullOrEmpty(unsubstr)) { return; }
        //    string[] items = unsubstr.Split(',');
        //    if (items.Length < 3) { return; }
        //    Unsubscribe(items[0], items[1], items[2], unsubstr, ignoreCnt);
        //}
        //public void UnsubscribeAll()
        //{
        //    foreach (var item in m_Subs.Keys)
        //    {
        //        string[] items = item.Split(',');
        //        if (items.Length < 3) { return; }
        //        isValid(ptUnsubscribePrice(items[0], items[1], items[2]), "UnsubscribeAll");
        //    }
        //    m_Subs.Clear();
        //}
        //public Dictionary<int, ContractStruct> Products(string exch = "", string contract = "", string date = "")
        //{
        //    if (!DLComplete) { return null; }
        //    Dictionary<int, ContractStruct> products = new Dictionary<int, ContractStruct>();
        //    int Cnt;
        //    isValid(ptCountContracts(out Cnt), "Products");
        //    for (int i = 0; i < Cnt; i++)
        //    {
        //        ContractStruct c = new ContractStruct();
        //        ptGetContract(i, out c);
        //        if (c.Tradable == 'Y' && c.ContractDate.Split(' ').Length <= 1 && c.NumberOfLegs == 1)
        //        {
        //            if (!string.IsNullOrEmpty(exch) && c.ExchangeName != exch) { continue; }
        //            if (!string.IsNullOrEmpty(contract) && c.ContractName != contract) { continue; }
        //            if (!string.IsNullOrEmpty(date) && c.ContractDate != date) { continue; }
        //            products.Add(i, c);
        //        }
        //    }
        //    return products;
        //}
        //public Dictionary<int, TraderAcctStruct> Traders()
        //{
        //    if (!DLComplete) { return null; }
        //    Dictionary<int, TraderAcctStruct> traders = new Dictionary<int, TraderAcctStruct>();
        //    int Cnt;
        //    isValid(ptCountTraders(out Cnt), "Traders");
        //    for (int i = 0; i < Cnt; i++)
        //    {
        //        TraderAcctStruct t = new TraderAcctStruct();
        //        ptGetTrader(i, out t);

        //        if (t.Tradable == 'Y') { traders.Add(i, t); }
        //    }

        //    return traders;
        //}
        //public ContractStruct Product(int index)
        //{
        //    ContractStruct contract;
        //    isValid(ptGetContract(index, out contract), "Product");
        //    return contract;
        //}
        //public ContractStruct Product(string exch, string contract, string date)
        //{
        //    ContractStruct c;
        //    isValid(ptGetContractByName(exch, contract, date, out c), "Product");
        //    return c;
        //}
        //public TraderAcctStruct Trader(int index)
        //{
        //    TraderAcctStruct acct;
        //    isValid(ptGetTrader(index, out acct), "Trade");
        //    return acct;
        //}
        //public PriceStruct Price(ContractStruct contract)
        //{
        //    return Price(contract.ExchangeName, contract.ContractName, contract.ContractDate);
        //}
        //public PriceStruct Price(string exchange, string contractname, string date)
        //{
        //    PriceStruct price;
        //    isValid(ptGetPriceForContract(exchange, contractname, date, out price), "GetPriceByName");
        //    return price;
        //}
        //public PriceStruct Price(int index)
        //{
        //    PriceStruct price;
        //    isValid(ptGetPrice(index, out price), "GetPriceByIndex");
        //    return price;
        //}
        //public void RefreshPrice(string exchange, string contractname, string date)
        //{
        //    isValid(ptPriceSnapshot(exchange, contractname, date, 0), "PriceSnapshot");
        //}
        //public PositionStruct Position(string traderAccount)
        //{
        //    PositionStruct postion;
        //    isValid(ptGetTotalPosition(traderAccount, out postion), "Position");
        //    return postion;
        //}

        //public PositionStruct Position(string exch, string commodity, string date, string traderAccount)
        //{
        //    PositionStruct postion;
        //    isValid(ptGetOpenPosition(exch, commodity, date, traderAccount, out postion), "Position");
        //    return postion;
        //}
        ////public string AvgPrice(string exch, string commodity, string date, string traderAccount)
        ////{
        ////    string avgPrice;
        ////    isValid(ptGetAveragePrice(exch, commodity, date, traderAccount, out avgPrice), "Position");
        ////    return avgPrice;
        ////}
        //public List<FillStruct> Fills()
        //{
        //    List<FillStruct> re = new List<FillStruct>();
        //    int fillCnt;
        //    ptCountFills(out fillCnt);
        //    for (int i = 0; i < fillCnt; i++)
        //    {
        //        FillStruct fill;
        //        ptGetFill(i, out fill);
        //        if (fill.FillType != (int)FillType.ptNettedFill) { re.Add(fill); }
        //    }
        //    return re;
        //}
        //public List<OrderDetailStruct> Orders()
        //{
        //    List<OrderDetailStruct> re = new List<OrderDetailStruct>();
        //    int orderCnt;
        //    ptCountOrders(out orderCnt);
        //    for (int i = 0; i < orderCnt; i++)
        //    {
        //        OrderDetailStruct order;
        //        ptGetOrder(i, out order);
        //        re.Add(order);
        //    }
        //    return re;
        //}
        //#endregion

        //#region Utility
        //private bool isValid(int ret, string functionName)
        //{
        //    if (ret == 0) { return true; }

        //    //Console.WriteLine(functionName + " failed to execute because " + ptGetErrorMessage(ret));
        //    return false;
        //}
        //private void RaiseEvent(EventArgs e)
        //{
        //    if (e is ConnectStateEventArgs)
        //    {
        //        OnConnectStateChanged?.Invoke(this, (ConnectStateEventArgs)e);
        //    }
        //}
        //private void RaiseEvent(string key, object obj)
        //{
        //    if (obj == null) { return; }
        //    if (obj is PriceStruct)
        //    {
        //        OnPriceUpdate?.Invoke(key, (PriceStruct)obj);
        //    }
        //    else if (obj is FillStruct)
        //    {
        //        OnFillUpdate?.Invoke(key, (FillStruct)obj);
        //    }
        //}
        //private string GetContractKey(ContractStruct contract)
        //{
        //    return GetContractKey(contract.ExchangeName, contract.ContractName, contract.ContractDate);
        //}
        //private string GetContractKey(TickerUpdStruct tick)
        //{
        //    return GetContractKey(tick.ExchangeName, tick.ContractName, tick.ContractDate);
        //}
        //private string GetContractKey(PriceUpdStruct price)
        //{
        //    return GetContractKey(price.ExchangeName, price.ContractName, price.ContractDate);
        //}
        //private string GetContractKey(FillStruct fill)
        //{
        //    return GetContractKey(fill.ExchangeName, fill.ContractName, fill.ContractDate);
        //}
        //private string GetContractKey(OrderDetailStruct order)
        //{
        //    return GetContractKey(order.ExchangeName, order.ContractName, order.ContractDate);
        //}
        //private string GetContractKey(string exchange, string contract, string date)
        //{
        //    return string.Format("{0},{1},{2}", exchange, contract, date);
        //}
        //#endregion
    }
}