//using Util.Extension;
//using Util.Extension.Class;

//namespace Capital.Report.Class
//{
//    public class OpenInterest : NotifyableClass
//    {
//        #region Variable
//        private string m_ExchangeID;
//        private string m_ExchangeName;
//        private string m_BrokerID;
//        private string m_CustNo;
//        private string m_ComID;
//        private string m_ComName;
//        private BuySell m_BuySell;
//        private int m_Qty;
//        private double m_MP;
//        private double m_AvgP;
//        private double m_YstCP;
//        private double m_ProfitLoss;
//        #endregion

//        #region Property
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
//        public string ExchangeName
//        {
//            get { return m_ExchangeName; }
//            set
//            {
//                if (value == m_ExchangeName) { return; }
//                m_ExchangeName = value;
//                OnPropertyChangedNoVerify("ExchangeName");
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
//        public string ComName
//        {
//            get { return m_ComName; }
//            set
//            {
//                if (value == m_ComName) { return; }
//                m_ComName = value;
//                OnPropertyChangedNoVerify("ComName");
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
//        public double MP
//        {
//            get { return m_MP; }
//            set
//            {
//                if (value == m_MP) { return; }
//                m_MP = value;
//                OnPropertyChangedNoVerify("MP");
//            }
//        }
//        public double AvgP
//        {
//            get { return m_AvgP; }
//            set
//            {
//                if (value == m_AvgP) { return; }
//                m_AvgP = value;
//                OnPropertyChangedNoVerify("AvgP");
//            }
//        }
//        public double YstCP
//        {
//            get { return m_YstCP; }
//            set
//            {
//                if (value == m_YstCP) { return; }
//                m_YstCP = value;
//                OnPropertyChangedNoVerify("YstCP");
//            }
//        }
//        public double ProfitLoss
//        {
//            get { return m_ProfitLoss; }
//            set
//            {
//                if (value == m_ProfitLoss) { return; }
//                m_ProfitLoss = value;
//                OnPropertyChangedNoVerify("ProfitLoss");
//            }
//        }
//        #endregion

//        public OpenInterest(string data)
//        {
//            string[] list = data.Split(',');
//            ExchangeID = list[0].Trim();
//            ExchangeName = list[1].Trim();
//            BrokerID = list[2].Trim().Substring(0, 7);
//            CustNo = list[2].Trim().Substring(7, 7);
//            ComID = list[3].Replace(" ", "").Trim();
//            ComName = list[4].Trim();
//            BuySell = list[5].Trim().ToEnum<BuySell>();
//            Qty = list[6].ToInt();
//            MP = list[7].ToDouble();
//            AvgP = list[8].ToDouble();
//            YstCP = list[9].ToDouble();
//            ProfitLoss = list[10].ToDouble();
//        }

//        public override string ToString()
//        {
//            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}", ExchangeID, ExchangeName, BrokerID, CustNo, ComID, ComName, BuySell, Qty, MP, AvgP, YstCP, ProfitLoss);
//        }
//    }
//}
