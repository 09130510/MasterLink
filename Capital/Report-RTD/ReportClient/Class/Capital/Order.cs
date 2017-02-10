//using System;
//using Util.Extension;
//using Util.Extension.Class;

//namespace Capital.Report.Class
//{
//    public class Order : NotifyableClass
//    {
//        #region Variable
//        private string m_KeyNo;
//        private MarketType m_MarketType;
//        private OrderType m_OrderType;
//        private OrderErr m_OrderErr;
//        private string m_BrokerID;
//        private string m_CustNo;
//        private string m_SubID;
//        private BuySell m_BuySell;
//        private PriceType m_PriceType;
//        private string m_ExchangeID;
//        private string m_ComID;
//        private string m_Strike;
//        private string m_OrdNo;
//        private double m_Price;
//        private int m_Qty;
//        private double m_MatchAmount;
//        private int m_BeforeQty;
//        private int m_AfterQty;
//        private DateTime m_Date;
//        private string m_Time;
//        private string m_OkSeq;
//        private string m_SaleNo;
//        #endregion

//        #region Property
//        public string KeyNo
//        {
//            get { return m_KeyNo; }
//            set
//            {
//                if (value == m_KeyNo) { return; }
//                m_KeyNo = value;
//                OnPropertyChangedNoVerify("KeyNo");
//            }
//        }
//        public MarketType MarketType
//        {
//            get { return m_MarketType; }
//            set
//            {
//                if (value == m_MarketType) { return; }
//                m_MarketType = value;
//                OnPropertyChangedNoVerify("MarketType");
//            }
//        }
//        public OrderType OrderType
//        {
//            get { return m_OrderType; }
//            set
//            {
//                if (value == m_OrderType) { return; }
//                m_OrderType = value;
//                OnPropertyChangedNoVerify("OrderType");
//            }
//        }
//        public OrderErr OrderErr
//        {
//            get { return m_OrderErr; }
//            set
//            {
//                if (value == m_OrderErr) { return; }
//                m_OrderErr = value;
//                OnPropertyChangedNoVerify("OrderErr");
//            }
//        }
//        public string BrokerID
//        {
//            get { return m_BrokerID; }
//            set
//            {
//                if (value == m_BrokerID) { return; }
//                m_BrokerID = value;
//                OnPropertyChangedNoVerify("BrokerID");
//            }
//        }
//        public string CustNo
//        {
//            get { return m_CustNo; }
//            set
//            {
//                if (value == m_CustNo) { return; }
//                m_CustNo = value;
//                OnPropertyChangedNoVerify("CustNo");
//            }
//        }
//        public string SubID
//        {
//            get { return m_SubID; }
//            set
//            {
//                if (value == m_SubID) { return; }
//                m_SubID = value;
//                OnPropertyChangedNoVerify("SubID");
//            }
//        }
//        public BuySell BuySell
//        {
//            get { return m_BuySell; }
//            set
//            {
//                if (value == m_BuySell) { return; }
//                m_BuySell = value;
//                OnPropertyChangedNoVerify("BuySell");
//            }
//        }
//        public PriceType PriceType
//        {
//            get { return m_PriceType; }
//            set
//            {
//                if (value == m_PriceType) { return; }
//                m_PriceType = value;
//                OnPropertyChangedNoVerify("PriceType");
//            }
//        }
//        public string ExchangeID
//        {
//            get { return m_ExchangeID; }
//            set
//            {
//                if (value == m_ExchangeID) { return; }
//                m_ExchangeID = value;
//                OnPropertyChangedNoVerify("ExchangeID");
//            }
//        }
//        public string ComID
//        {
//            get { return m_ComID; }
//            set
//            {
//                if (value == m_ComID) { return; }
//                m_ComID = value;
//                OnPropertyChangedNoVerify("ComID");
//            }
//        }
//        public string Strike
//        {
//            get { return m_Strike; }
//            set
//            {
//                if (value == m_Strike) { return; }
//                m_Strike = value;
//                OnPropertyChangedNoVerify("Strike");
//            }
//        }
//        public string OrdNo
//        {
//            get { return m_OrdNo; }
//            set
//            {
//                if (value == m_OrdNo) { return; }
//                m_OrdNo = value;
//                OnPropertyChangedNoVerify("OrdNo");
//            }
//        }
//        public double Price
//        {
//            get { return m_Price; }
//            set
//            {
//                if (value == m_Price) { return; }
//                m_Price = value;
//                OnPropertyChangedNoVerify("Price");
//            }
//        }
//        public int Qty
//        {
//            get { return m_Qty; }
//            set
//            {
//                if (value == m_Qty) { return; }
//                m_Qty = value;
//                OnPropertyChangedNoVerify("Qty");
//            }
//        }
//        public double MatchAmount
//        {
//            get { return m_MatchAmount; }
//            set
//            {
//                if (value == m_MatchAmount) { return; }
//                m_MatchAmount = value;
//                OnPropertyChangedNoVerify("MatchAmount");
//            }
//        }
//        public int BeforeQty
//        {
//            get { return m_BeforeQty; }
//            set
//            {
//                if (value == m_BeforeQty) { return; }
//                m_BeforeQty = value;
//                OnPropertyChangedNoVerify("BeforeQty");
//            }
//        }
//        public int AfterQty
//        {
//            get { return m_AfterQty; }
//            set
//            {
//                if (value == m_AfterQty) { return; }
//                m_AfterQty = value;
//                OnPropertyChangedNoVerify("AfterQty");
//            }
//        }
//        public DateTime Date
//        {
//            get { return m_Date; }
//            set
//            {
//                if (value == m_Date) { return; }
//                m_Date = value;
//                OnPropertyChangedNoVerify("Date");
//            }
//        }
//        public string Time
//        {
//            get { return m_Time; }
//            set
//            {
//                if (value == m_Time) { return; }
//                m_Time = value;
//                OnPropertyChangedNoVerify("Time");
//            }
//        }
//        public string OkSeq
//        {
//            get { return m_OkSeq; }
//            set
//            {
//                if (value == m_OkSeq) { return; }
//                m_OkSeq = value;
//                OnPropertyChangedNoVerify("OkSeq");
//            }
//        }
//        public string SaleNo
//        {
//            get { return m_SaleNo; }
//            set
//            {
//                if (value == m_SaleNo) { return; }
//                m_SaleNo = value;
//                OnPropertyChangedNoVerify("SaleNo");
//            }
//        }
//        #endregion

//        public Order(DataItem data)
//        {
//            KeyNo = data.strKeyNo.Trim();
//            MarketType = data.strMarketType.ToEnum<MarketType>();
//            OrderType = data.strType.ToEnum<OrderType>();
//            OrderErr = data.strOrderErr.ToEnum<OrderErr>();
//            BrokerID = data.strBroker.Trim();
//            CustNo = data.strCustNo.Trim();
//            SubID = data.strSubID.Trim();
//            BuySell = data.strBuySell.Substring(0, 1).ToEnum<BuySell>();
//            PriceType = data.strBuySell.Substring(1, 1).ToEnum<PriceType>();
//            ExchangeID = data.strExchangeID.Trim();
//            ComID = data.strComId.Substring(0, 19).Replace(" ", "");
//            Strike = data.strStrikePrice.Trim();
//            OrdNo = data.strOrderNo.Trim();
//            Price = data.strPrice.ToDouble() / 100000;
//            Qty = data.strQty.ToInt();
//            MatchAmount = Price * Qty;
//            BeforeQty = data.strBeforeQty.ToInt();
//            AfterQty = data.strAfterQty.ToInt();
//            Date = data.strDate.ToDateTime();
//            string t = data.strTime.Trim();
//            Time = t.Substring(0, 2) + ":" + t.Substring(2, 2) + ":" + t.Substring(4, 2);
//            OkSeq = data.strOkSeq.Trim();
//            SaleNo = data.strSaleNo.Trim();
//        }

//        public override string ToString()
//        {
//            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}", KeyNo, MarketType, OrderType, OrderErr, BrokerID, CustNo, SubID, BuySell, PriceType, ExchangeID, ComID, Strike, OrdNo, Price, Qty, MatchAmount, BeforeQty, AfterQty, Date, Time, OkSeq, SaleNo);
//        }
//    }
//}