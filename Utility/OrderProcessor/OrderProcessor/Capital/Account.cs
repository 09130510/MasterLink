using Util.Extension;
using Util.Extension.Class;

namespace OrderProcessor.Capital
{
    public class Account : NotifyableClass
    {
        #region Variable
        private MarketType m_MarketType;
        private string m_BrokerID;
        private string m_CustNo;
        private string m_Account;
        private string m_Name;
        #endregion

        #region Property
        public string Key { get { return BrokerID + CustNo; } }
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
        public string AccountNo
        {
            get { return m_Account; }
            set
            {
                if (value == m_Account) { return; }
                m_Account = value;
                OnPropertyChangedNoVerify(nameof(AccountNo));
            }
        }
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value == m_Name) { return; }
                m_Name = value;
                OnPropertyChangedNoVerify(nameof(Name));
            }
        }
        #endregion

        public Account(string data)
        {
            string[] list = data.Split(',');
            for (int i = 0; i < list.Length; i++)
            {
                if (i == 0) { MarketType = list[i].Trim().ToEnum<MarketType>(); continue; }
                if (i == 1) { BrokerID = list[i].Trim(); continue; }
                if (i == 3) { CustNo = list[i].Trim(); continue; }
                if (i == 4) { AccountNo = list[i].Trim(); continue; }
                if (i == 5) { Name = list[i].Trim(); continue; }
            }
            
        }

        public override string ToString()
        {
            return $"{MarketType}, {BrokerID}, {CustNo}, {AccountNo}, {Name}";
        }
    }
}