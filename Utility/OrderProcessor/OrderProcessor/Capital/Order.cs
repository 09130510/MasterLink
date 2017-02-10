using System;
using Util.Extension;
using Util.Extension.Class;

namespace OrderProcessor.Capital
{
    public class Order : NotifyDisposableClass
    {
        #region Variable
        private int m_BeforeQty;
        private int m_AfterQty;
        private string m_BrokerID;
        private Side m_BuySell;
        private string m_ComID;
        private string m_CustNo;
        private DateTime m_Date;
        private string m_ExchangeID;
        private string m_KeyNo;
        private MarketType m_MarketType;
        private double m_MatchAmount;
        private string m_OkSeq;
        private OrderErr m_OrderErr;
        private OrderType m_OrderType;
        private string m_OrdNo;
        private double m_Price;
        private PriceType m_PriceType;
        private int m_Qty;
        private string m_SaleNo;
        private string m_Strike;
        private string m_SubID;
        private int m_SumQty = 0;
        private string m_Time;
        #endregion
        
        
        #region Property
        public string Key { get { return OkSeq + OrdNo; } }
        public string KeyNo
        {
            get { return m_KeyNo; }
            set
            {
                if (value == m_KeyNo) { return; }
                m_KeyNo = value;
                OnPropertyChangedNoVerify(nameof(KeyNo));
            }
        }
        public MarketType MarketType
        {
            get { return m_MarketType; }
            set
            {
                if (value == m_MarketType) { return; }
                m_MarketType = value;
                OnPropertyChangedNoVerify(nameof(MarketType));
            }
        }
        public OrderType OrderType
        {
            get { return m_OrderType; }
            set
            {
                if (value == m_OrderType) { return; }
                m_OrderType = value;
                OnPropertyChangedNoVerify(nameof(OrderType));
            }
        }
        public OrderErr OrderErr
        {
            get { return m_OrderErr; }
            set
            {
                if (value == m_OrderErr) { return; }
                m_OrderErr = value;
                OnPropertyChangedNoVerify(nameof(OrderErr));
            }
        }
        public string BrokerID
        {
            get { return m_BrokerID; }
            set
            {
                if (value == m_BrokerID) { return; }
                m_BrokerID = value;
                OnPropertyChangedNoVerify(nameof(BrokerID));
            }
        }
        public string CustNo
        {
            get { return m_CustNo; }
            set
            {
                if (value == m_CustNo) { return; }
                m_CustNo = value;
                OnPropertyChangedNoVerify(nameof(CustNo));
            }
        }
        public string SubID
        {
            get { return m_SubID; }
            set
            {
                if (value == m_SubID) { return; }
                m_SubID = value;
                OnPropertyChangedNoVerify(nameof(SubID));
            }
        }
        public Side BuySell
        {
            get { return m_BuySell; }
            set
            {
                if (value == m_BuySell) { return; }
                m_BuySell = value;
                OnPropertyChangedNoVerify(nameof(BuySell));
            }
        }
        public PriceType PriceType
        {
            get { return m_PriceType; }
            set
            {
                if (value == m_PriceType) { return; }
                m_PriceType = value;
                OnPropertyChangedNoVerify(nameof(PriceType));
            }
        }
        public string ExchangeID
        {
            get { return m_ExchangeID; }
            set
            {
                if (value == m_ExchangeID) { return; }
                m_ExchangeID = value;
                OnPropertyChangedNoVerify(nameof(ExchangeID));
            }
        }
        public string ComID
        {
            get { return m_ComID; }
            set
            {
                if (value == m_ComID) { return; }
                m_ComID = value;
                OnPropertyChangedNoVerify(nameof(ComID));
            }
        }
        public string OrderHead { get; private set; }
        public string YM { get;private set; }
        public string YM2 { get; private set; }
        public string SpreadYM { get { return string.IsNullOrEmpty(YM2)?"": $"{YM}/{YM2}"; } }
        public string Strike
        {
            get { return m_Strike; }
            set
            {
                if (value == m_Strike) { return; }
                m_Strike = value;
                OnPropertyChangedNoVerify(nameof(Strike));
            }
        }
        public string OrdNo
        {
            get { return m_OrdNo; }
            set
            {
                if (value == m_OrdNo) { return; }
                m_OrdNo = value;
                OnPropertyChangedNoVerify(nameof(OrdNo));
            }
        }
        public double Price
        {
            get { return m_Price; }
            set
            {
                if (value == m_Price) { return; }
                m_Price = value;
                OnPropertyChangedNoVerify(nameof(Price));
            }
        }
        public int Qty
        {
            get { return m_Qty; }
            set
            {
                if (value == m_Qty) { return; }
                m_Qty = value;
                OnPropertyChangedNoVerify(nameof(Qty));
            }
        }
        public double MatchAmount
        {
            get { return m_MatchAmount; }
            set
            {
                if (value == m_MatchAmount) { return; }
                m_MatchAmount = value;
                OnPropertyChangedNoVerify(nameof(MatchAmount));
            }
        }
        public int BeforeQty
        {
            get { return m_BeforeQty; }
            set
            {
                if (value == m_BeforeQty) { return; }
                m_BeforeQty = value;
                OnPropertyChangedNoVerify(nameof(BeforeQty));
            }
        }
        public int AfterQty
        {
            get { return m_AfterQty; }
            set
            {
                if (value == m_AfterQty) { return; }
                m_AfterQty = value;
                OnPropertyChangedNoVerify(nameof(AfterQty));
            }
        }
        public int SumQty
        {
            get { return m_SumQty; }
            set
            {
                if (value == m_SumQty) { return; }
                m_SumQty = value;
                OnPropertyChangedNoVerify(nameof(SumQty));
            }
        }
        public DateTime Date
        {
            get { return m_Date; }
            set
            {
                if (value == m_Date) { return; }
                m_Date = value;
                OnPropertyChangedNoVerify(nameof(Date));
            }
        }
        public string Time
        {
            get { return m_Time; }
            set
            {
                if (value == m_Time) { return; }
                m_Time = value;
                OnPropertyChangedNoVerify(nameof(Time));
            }
        }
        public string OkSeq
        {
            get { return m_OkSeq; }
            set
            {
                if (value == m_OkSeq) { return; }
                m_OkSeq = value;
                OnPropertyChangedNoVerify(nameof(OkSeq));
            }
        }
        public string SaleNo
        {
            get { return m_SaleNo; }
            set
            {
                if (value == m_SaleNo) { return; }
                m_SaleNo = value;
                OnPropertyChangedNoVerify(nameof(SaleNo));
            }
        }
        #endregion

        public Order(string data)
        {
            string[] items = data.Split(',');
            for (int i = 0; i < items.Length; i++)
            {
                if (i == 0) { KeyNo = items[i].Trim(); continue; }
                if (i == 1) { MarketType = items[i].Trim().ToEnum<MarketType>(); continue; }
                if (i == 2) { OrderType = items[i].Trim().ToEnum<OrderType>(); continue; }
                if (i == 3) { OrderErr = items[i].Trim().ToEnum<OrderErr>(); continue; }
                if (i == 4) { BrokerID = items[i].Trim(); continue; }
                if (i == 5) { CustNo = items[i].Trim(); continue; }
                if (i == 6)
                {
                    BuySell = items[i].Substring(0, 1)[0].ToEnumByCapital<Side>();
                    PriceType = items[i].Substring(1, 1).ToEnum<PriceType>();
                    continue;
                }
                if (i == 7) { ExchangeID = items[i].Trim(); continue;  }
                if (i == 8)
                {
                    ComID = (items[i].Length >= 19 ? items[i].Substring(0, 19) : items[i]).Replace(" ", "");
                    if (ComID.Contains("/"))
                    {
                        string[] values = ComID.Split('/');
                        OrderHead = values[0].Substring(0, ComID.Length - 6);
                        YM = values[0].Replace(OrderHead, string.Empty);
                        YM2 = values[1];
                    }
                    else
                    {
                        OrderHead = ComID.Substring(0, ComID.Length - 6);
                        YM = ComID.Replace(OrderHead, string.Empty);
                    }                    
                    continue;
                }
                if (i == 9) { Strike = items[i].Trim(); continue; }
                if (i == 10) { OrdNo = items[i].Trim(); continue; }
                if (i == 11) { Price = items[i].ToDouble() / 100000D; continue; }
                if (i == 20)
                {
                    Qty = items[i].ToInt();
                    MatchAmount = Price * Qty;
                    continue;
                }
                if (i == 21) { BeforeQty = items[i].ToInt(); continue; }
                if (i == 22) { AfterQty = items[i].ToInt(); continue; }
                if (i == 23) { Date = items[i].ToDateTime(); continue; }
                if (i == 24)
                {
                    string t = items[i].Trim();
                    Time = t.Substring(0, 2) + ":" + t.Substring(2, 2) + ":" + t.Substring(4, 2);
                    continue;
                }
                if (i == 25) { OkSeq = items[i].Trim(); continue; }
                if (i == 26) { SubID = items[i].Trim(); continue; }
            }
        }

        public override string ToString()
        {
            return $"{KeyNo}, {MarketType}, {OrderType}, {OrderErr}, {BrokerID}, {CustNo}, {SubID}, {BuySell}, {PriceType}, {ExchangeID}, {ComID}, {Strike}, {OrdNo}, {Price}, {Qty}, {MatchAmount}, {BeforeQty}, {AfterQty}, {Date}, {Time}, {OkSeq}, {SaleNo}";
        }

        protected override void DoDispose() { }
    }
}
