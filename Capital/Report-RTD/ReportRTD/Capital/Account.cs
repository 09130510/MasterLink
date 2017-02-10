using System;

namespace Capital.RTD
{
    /// <summary>
    /// Capital Account Data
    /// </summary>
    public class Account
    {
        #region Variable
        private MarketType m_MarketType = default(MarketType);
        private string m_BrokerID;
        private string m_CustNo;
        private string m_LoginAccount;
        private string m_Name;
        #endregion

        #region Property
        /// <summary>
        /// 識別鍵
        /// </summary>
        public string Key { get { return BrokerID + CustNo; } }
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
        /// </summary>
        public string CustNo
        {
            get { return m_CustNo; }
            set
            {
                if (value == m_CustNo) { return; }
                m_CustNo = value;
            }
        }
        /// <summary>
        /// 登入帳號
        /// </summary>
        public string LoginAccount
        {
            get { return m_LoginAccount; }
            set
            {
                if (value == m_LoginAccount) { return; }
                m_LoginAccount = value;
            }
        }
        /// <summary>
        /// 帳號名稱
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (value == m_Name) { return; }
                m_Name = value;
            }
        }
        #endregion

        /// <summary>
        /// Capital Account Data
        /// </summary>
        /// <param name="data"></param>
        public Account(string data)
        {
            string[] list = data.Split(',');
            Enum.TryParse<MarketType>(list[0].Trim(), out m_MarketType);
            BrokerID = list[1].Trim();
            CustNo = list[3].Trim();
            LoginAccount = list[4].Trim();
            Name = list[5].Trim();
        }

        /// <summary>
        /// 內容
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}", MarketType, BrokerID, CustNo, LoginAccount, Name);
        }
    }
}