using System;

namespace Capital.RTD
{
    /// <summary>
    /// Capital Order/Match Reply Data
    /// </summary>
    public class Order
    {
        #region Variable
        private string m_KeyNo;
        private MarketType m_MarketType = default(MarketType);
        private OrderType m_OrderType = default(OrderType);
        private OrderErr m_OrderErr = default(OrderErr);
        private string m_BrokerID;
        private string m_CustNo;
        private string m_SubID;
        private BuySell m_BuySell = default(BuySell);
        private PriceType m_PriceType = default(PriceType);
        private string m_ExchangeID;
        private string m_ComID;
        private string m_Strike;
        private string m_OrdNo;
        private double m_Price = 0.0;
        private int m_Qty = 0;
        private double m_MatchAmount = 0.0;
        private int m_BeforeQty = 0;
        private int m_AfterQty = 0;
        private DateTime m_Date;
        private string m_Time;
        private string m_OkSeq;
        private string m_SaleNo;
        #endregion

        #region Property
        /// <summary>
        /// 委託序號
        /// </summary>
        public string KeyNo
        {
            get { return m_KeyNo; }
            set
            {
                if (value == m_KeyNo) { return; }
                m_KeyNo = value;
            }
        }
        /// <summary>
        /// 市場別
        /// </summary>
        public MarketType MarketType
        {
            get { return m_MarketType; }
            set
            {
                if (value == m_MarketType) { return; }
                m_MarketType = value;
            }
        }
        /// <summary>
        /// 委託種類
        /// </summary>
        public OrderType OrderType
        {
            get { return m_OrderType; }
            set
            {
                if (value == m_OrderType) { return; }
                m_OrderType = value;
            }
        }
        /// <summary>
        /// 錯誤
        /// </summary>
        public OrderErr OrderErr
        {
            get { return m_OrderErr; }
            set
            {
                if (value == m_OrderErr) { return; }
                m_OrderErr = value;
            }
        }
        /// <summary>
        /// 上手
        /// </summary>
        public string BrokerID
        {
            get { return m_BrokerID; }
            set
            {
                if (value == m_BrokerID) { return; }
                m_BrokerID = value;
            }
        }
        /// <summary>
        /// 交易帳號
        /// PS. 回報與下單API的帳號欄位定義不同(回報.SubID=下單CustNo), 為了統一, 所以把回報資料裡的CustNo, SubID互換
        /// </summary>
        public string CustNo
        {
            get { return m_SubID; }
            set
            {
                if (value == m_SubID) { return; }
                m_SubID = value;
            }
        }
        /// <summary>
        /// 子帳帳號
        /// PS. 回報與下單API的帳號欄位定義不同(回報.SubID=下單CustNo), 為了統一, 所以把回報資料裡的CustNo, SubID互換
        /// </summary>
        public string SubID
        {
            get { return m_CustNo; }
            set
            {
                if (value == m_CustNo) { return; }
                m_CustNo = value;
            }
        }
        /// <summary>
        /// 買賣
        /// </summary>
        public BuySell BuySell
        {
            get { return m_BuySell; }
            set
            {
                if (value == m_BuySell) { return; }
                m_BuySell = value;
            }
        }
        /// <summary>
        /// 價格種類
        /// </summary>
        public PriceType PriceType
        {
            get { return m_PriceType; }
            set
            {
                if (value == m_PriceType) { return; }
                m_PriceType = value;
            }
        }
        /// <summary>
        /// 交易所
        /// </summary>
        public string ExchangeID
        {
            get { return m_ExchangeID; }
            set
            {
                if (value == m_ExchangeID) { return; }
                m_ExchangeID = value;
            }
        }
        /// <summary>
        /// 商品
        /// </summary>
        public string ComID
        {
            get { return m_ComID; }
            set
            {
                if (value == m_ComID) { return; }
                m_ComID = value;
            }
        }
        /// <summary>
        /// 履約價
        /// </summary>
        public string Strike
        {
            get { return m_Strike; }
            set
            {
                if (value == m_Strike) { return; }
                m_Strike = value;
            }
        }
        /// <summary>
        /// 委託書號
        /// </summary>
        public string OrdNo
        {
            get { return m_OrdNo; }
            set
            {
                if (value == m_OrdNo) { return; }
                m_OrdNo = value;
            }
        }
        /// <summary>
        /// 委託/成交價
        /// </summary>
        public double Price
        {
            get { return m_Price; }
            set
            {
                if (value == m_Price) { return; }
                m_Price = value;
            }
        }
        /// <summary>
        /// 口數
        /// </summary>
        public int Qty
        {
            get { return m_Qty; }
            set
            {
                if (value == m_Qty) { return; }
                m_Qty = value;
            }
        }
        /// <summary>
        /// 成交價金
        /// </summary>
        public double MatchAmount
        {
            get { return m_MatchAmount; }
            set
            {
                if (value == m_MatchAmount) { return; }
                m_MatchAmount = value;
            }
        }
        /// <summary>
        /// 異動前口數
        /// </summary>
        public int BeforeQty
        {
            get { return m_BeforeQty; }
            set
            {
                if (value == m_BeforeQty) { return; }
                m_BeforeQty = value;
            }
        }
        /// <summary>
        /// 異動後口數
        /// </summary>
        public int AfterQty
        {
            get { return m_AfterQty; }
            set
            {
                if (value == m_AfterQty) { return; }
                m_AfterQty = value;
            }
        }
        /// <summary>
        /// 交易日期
        /// </summary>
        public DateTime Date
        {
            get { return m_Date; }
            set
            {
                if (value == m_Date) { return; }
                m_Date = value;
            }
        }
        /// <summary>
        /// 交易時間
        /// </summary>
        public string Time
        {
            get { return m_Time; }
            set
            {
                if (value == m_Time) { return; }
                m_Time = value;
            }
        }
        /// <summary>
        /// 成交序號
        /// </summary>
        public string OkSeq
        {
            get { return m_OkSeq; }
            set
            {
                if (value == m_OkSeq) { return; }
                m_OkSeq = value;
            }
        }
        /// <summary>
        /// 營業員編號
        /// </summary>
        public string SaleNo
        {
            get { return m_SaleNo; }
            set
            {
                if (value == m_SaleNo) { return; }
                m_SaleNo = value;
            }
        }
        #endregion

        public Order(string data)
        {
            string[] items = data.Split(',');
            for (int i = 0; i < items.Length; i++)
            {
                if (i == 0) { KeyNo = items[i].Trim(); }
                if (i == 1) { Enum.TryParse(items[i].Trim(), out m_MarketType); }
                if (i == 2) { Enum.TryParse(items[i].Trim(), out m_OrderType); }
                if (i == 3) { Enum.TryParse(items[i].Trim(), out m_OrderErr); }
                if (i == 4) { BrokerID = items[i].Trim(); }
                if (i == 5) { m_CustNo = items[i].Trim(); }
                if (i == 6)
                {
                    Enum.TryParse(items[i].Substring(0, 1), out m_BuySell);
                    Enum.TryParse(items[i].Substring(1, 1), out m_PriceType);                    
                }
                if (i == 7) { ExchangeID = items[i].Trim(); }
                if (i == 8)
                {
                    ComID = (items[i].Length >= 19 ? items[i].Substring(0, 19) : items[i]).Replace(" ", "");
                }
                if (i == 9) { Strike = items[i]; }
                if (i == 10) { OrdNo = items[i]; }
                if (i == 11)
                {
                    double p = 0D;
                    if (double.TryParse(items[i], out p)) { p = p / 100000; }
                    Price = p;
                }
                if (i == 20)
                {
                    int.TryParse( items[i], out m_Qty);
                    MatchAmount = Price * Qty;
                }
                if (i == 21) { int.TryParse(items[i],out m_BeforeQty); }
                if (i == 22) { int.TryParse(items[i], out m_AfterQty); }
                if (i == 23)
                {
                    int year = int.Parse(items[i].Substring(0, 4));
                    int month = int.Parse(items[i].Substring(4, 2));
                    int day = int.Parse(items[i].Substring(6, 2));
                    Date = new DateTime(year, month, day);                    
                }
                if (i == 24)
                {
                    string t = items[i].Trim();
                    Time = t.Substring(0, 2) + ":" + t.Substring(2, 2) + ":" + t.Substring(4, 2);
                }
                if (i == 25) { OkSeq = items[i].Trim(); }
                if (i == 26) { m_SubID = items[i].Trim(); }
            }
        }
        ///// <summary>
        ///// Capital Order/Match Reply Data
        ///// </summary>
        ///// <param name="data"></param>
        //public Order(DataItem data)
        //{
        //    KeyNo = data.strKeyNo.Trim();
        //    Enum.TryParse<MarketType>(data.strMarketType, out m_MarketType);
        //    Enum.TryParse<OrderType>(data.strType, out m_OrderType);
        //    Enum.TryParse<OrderErr>(data.strOrderErr, out m_OrderErr);
        //    BrokerID = data.strBroker.Trim();
        //    m_CustNo = data.strCustNo.Trim();
        //    m_SubID = data.strSubID.Trim();
        //    Enum.TryParse<BuySell>(data.strBuySell.Substring(0, 1), out m_BuySell);
        //    Enum.TryParse<PriceType>(data.strBuySell.Substring(1, 1), out m_PriceType);
        //    ExchangeID = data.strExchangeID.Trim();
        //    ComID = data.strComId.Substring(0, 19).Replace(" ", "");
        //    Strike = data.strStrikePrice.Trim();
        //    OrdNo = data.strOrderNo.Trim();
        //    double p;
        //    double.TryParse(data.strPrice, out p);
        //    Price = p / 100000;
        //    int.TryParse(data.strQty, out m_Qty);
        //    MatchAmount = Price * Qty;
        //    int.TryParse(data.strBeforeQty, out m_BeforeQty);
        //    int.TryParse(data.strAfterQty, out m_AfterQty);
        //    int year = int.Parse(data.strDate.Substring(0, 4));
        //    int month = int.Parse(data.strDate.Substring(4, 2));
        //    int day = int.Parse(data.strDate.Substring(6, 2));
        //    Date = new DateTime(year, month, day);
        //    string t = data.strTime.Trim();
        //    Time = t.Substring(0, 2) + ":" + t.Substring(2, 2) + ":" + t.Substring(4, 2);
        //    OkSeq = data.strOkSeq.Trim();
        //    SaleNo = data.strSaleNo.Trim();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}", KeyNo, MarketType, OrderType, OrderErr, BrokerID, CustNo, SubID, BuySell, PriceType, ExchangeID, ComID, Strike, OrdNo, Price, Qty, MatchAmount, BeforeQty, AfterQty, Date, Time, OkSeq, SaleNo);
        }
    }
}
