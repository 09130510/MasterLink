using System;

namespace Capital.RTD
{
    /// <summary>
    /// Summary Data
    /// </summary>
    public class Summary
    {
        #region Variable
        private string m_BrokerID;
        private string m_CustNo;
        private string m_ComID;
        private int m_ALot;
        private int m_BLot;
        private double m_AAmt;
        private double m_BAmt;
        #endregion

        #region Property
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
        /// 賣口數
        /// </summary>
        public int ALot
        {
            get { return m_ALot; }
            set
            {
                if (value == m_ALot) { return; }
                m_ALot = value;
            }
        }
        /// <summary>
        /// 買口數
        /// </summary>
        public int BLot
        {
            get { return m_BLot; }
            set
            {
                if (value == m_BLot) { return; }
                m_BLot = value;
            }
        }
        /// <summary>
        /// 賣金額
        /// </summary>
        public double AAmt
        {
            get { return m_AAmt; }
            set
            {
                if (value == m_AAmt) { return; }
                m_AAmt = value;
            }
        }
        /// <summary>
        /// 買金額
        /// </summary>
        public double BAmt
        {
            get { return m_BAmt; }
            set
            {
                if (value == m_BAmt) { return; }
                m_BAmt = value;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}", BrokerID, CustNo, ComID, ALot, BLot, AAmt, BAmt);
        }
    }
}