using Util.Extension.Class;

namespace OrderProcessor.Capital
{
    public class Summary : NotifyableClass
    {
        #region Variable
        private string m_BrokerID;
        private string m_CustNo;
        private string m_ComID;
        private int m_ALots;
        private int m_BLots;
        private double m_AAmount;
        private double m_BAmount;
        #endregion

        #region Property
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
        public int ALots
        {
            get { return m_ALots; }
            set
            {
                if (value == m_ALots) { return; }
                m_ALots = value;
                OnPropertyChangedNoVerify(nameof(ALots));
            }
        }
        public int BLots
        {
            get { return m_BLots; }
            set
            {
                if (value == m_BLots) { return; }
                m_BLots = value;
                OnPropertyChangedNoVerify(nameof(BLots));
            }
        }
        public double AAmount
        {
            get { return m_AAmount; }
            set
            {
                if (value == m_AAmount) { return; }
                m_AAmount = value;
                OnPropertyChangedNoVerify(nameof(AAmount));
            }
        }
        public double BAmount
        {
            get { return m_BAmount; }
            set
            {
                if (value == m_BAmount) { return; }
                m_BAmount = value;
                OnPropertyChangedNoVerify(nameof(BAmount));
            }
        }
        #endregion

        public override string ToString()
        {
            return $"{BrokerID}, {CustNo}, {ComID}, {ALots}, {BLots}, {AAmount}, {BAmount}";
        }
    }
}
