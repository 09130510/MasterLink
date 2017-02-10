//using Util.Extension;
//using Util.Extension.Class;

//namespace Capital.Report.Class
//{
//    public class Account : NotifyableClass
//    {
//        #region Variable
//        private MarketType m_MarketType;
//        private string m_BrokerID;
//        private string m_CustNo;
//        private string m_Account;
//        private string m_Name;
//        #endregion

//        #region Property
//        public string Key { get { return BrokerID + CustNo; } }
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
//        public string AccountNo
//        {
//            get { return m_Account; }
//            set
//            {
//                if (value == m_Account) { return; }
//                m_Account = value;
//                OnPropertyChangedNoVerify("Account");
//            }
//        }
//        public string Name
//        {
//            get { return m_Name; }
//            set
//            {
//                if (value == m_Name) { return; }
//                m_Name = value;
//                OnPropertyChangedNoVerify("Name");
//            }
//        }
//        #endregion

//        public Account(string data)
//        {
//            string[] list = data.Split(',');
//            MarketType = list[0].Trim().ToEnum<MarketType>();
//            BrokerID = list[1].Trim();
//            CustNo = list[3].Trim();
//            AccountNo = list[4].Trim();
//            Name = list[5].Trim();
//        }

//        public override string ToString()
//        {
//            return string.Format("{0}, {1}, {2}, {3}, {4}", MarketType, BrokerID, CustNo, AccountNo, Name);
//        }
//    }
//}
