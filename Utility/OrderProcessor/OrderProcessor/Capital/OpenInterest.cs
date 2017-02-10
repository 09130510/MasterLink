
using Util.Extension;
using Util.Extension.Class;

namespace OrderProcessor.Capital
{
    public class OpenInterest : NotifyableClass
    {
        #region Variable
        private string m_ExchangeID;
        private string m_ExchangeName;
        private string m_BrokerID;
        private string m_CustNo;
        private string m_ComID;
        private string m_ComName;
        private Side m_BuySell;
        private int m_Qty;
        private double m_MP;
        private double m_AvgP;
        private double m_YstCP;
        private double m_ProfitLoss;
        #endregion

        #region Property
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
        public string ExchangeName
        {
            get { return m_ExchangeName; }
            set
            {
                if (value == m_ExchangeName) { return; }
                m_ExchangeName = value;
                OnPropertyChangedNoVerify(nameof(ExchangeName));
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
        public string ComName
        {
            get { return m_ComName; }
            set
            {
                if (value == m_ComName) { return; }
                m_ComName = value;
                OnPropertyChangedNoVerify(nameof(ComName));
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
        public double MP
        {
            get { return m_MP; }
            set
            {
                if (value == m_MP) { return; }
                m_MP = value;
                OnPropertyChangedNoVerify(nameof(MP));
            }
        }
        public double AvgP
        {
            get { return m_AvgP; }
            set
            {
                if (value == m_AvgP) { return; }
                m_AvgP = value;
                OnPropertyChangedNoVerify(nameof(AvgP));
            }
        }
        public double YstCP
        {
            get { return m_YstCP; }
            set
            {
                if (value == m_YstCP) { return; }
                m_YstCP = value;
                OnPropertyChangedNoVerify(nameof(YstCP));
            }
        }
        public double ProfitLoss
        {
            get { return m_ProfitLoss; }
            set
            {
                if (value == m_ProfitLoss) { return; }
                m_ProfitLoss = value;
                OnPropertyChangedNoVerify(nameof(ProfitLoss));
            }
        }
        #endregion

        public OpenInterest(string data)
        {
            string[] list = data.Split(',');
            for (int i = 0; i < list.Length; i++)
            {
                if (i == 0) { ExchangeID = list[i].Trim(); }
                if (i == 1) { ExchangeName = list[i].Trim(); }
                if (i == 2)
                {
                    BrokerID = list[i].Trim().Substring(0, 7);
                    CustNo = list[i].Trim().Substring(7, 7);
                }
                if (i == 3) { ComID = list[i].Replace(" ", "").Trim(); }
                if (i == 4) { ComName = list[i].Trim(); }
                if (i == 5) { BuySell = list[i].Trim()[0].ToEnumByCapital<Side>(); }
                if (i == 6) { Qty = list[i].ToInt(); }
                if (i == 7) { MP = list[i].ToDouble(); }
                if (i == 8) { AvgP = list[i].ToDouble(); }
                if (i == 9) { YstCP = list[i].ToDouble(); }
                if (i == 10) { ProfitLoss = list[i].ToDouble(); }
            }            
        }

        public override string ToString()
        {
            return $"{ExchangeID}, {ExchangeName}, {BrokerID}, {CustNo}, {ComID}, {ComName}, {BuySell}, {Qty}, {MP}, {AvgP}, {YstCP}, {ProfitLoss}";
        }
    }
}
