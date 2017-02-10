using System;

namespace Capital.RTD
{
    /// <summary>
    /// Capital 未平倉 Data
    /// </summary>
    public class OpenInterest
    {
        #region Variable
        private string m_ExchangeID;
        private string m_ExchangeName;
        private string m_BrokerID;
        private string m_CustNo;
        private string m_ComID;
        private string m_ComName;
        private BuySell m_BuySell = default(BuySell);
        private int m_Qty = 0;
        private double m_MP = 0.0;
        private double m_AvgP = 0.0;
        private double m_YstCP = 0.0;
        private double m_ProfitLoss = 0.0;
        private string m_UpdateTime;
        #endregion

        #region Property
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
        /// 交易所名稱
        /// </summary>
        public string ExchangeName
        {
            get { return m_ExchangeName; }
            set
            {
                if (value == m_ExchangeName) { return; }
                m_ExchangeName = value;
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
        /// 商品名稱
        /// </summary>
        public string ComName
        {
            get { return m_ComName; }
            set
            {
                if (value == m_ComName) { return; }
                m_ComName = value;
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
        /// 市價
        /// </summary>
        public double MP
        {
            get { return m_MP; }
            set
            {
                if (value == m_MP) { return; }
                m_MP = value;
            }
        }
        /// <summary>
        /// 均價
        /// </summary>
        public double AvgP
        {
            get { return m_AvgP; }
            set
            {
                if (value == m_AvgP) { return; }
                m_AvgP = value;
            }
        }
        /// <summary>
        /// 昨日結算價
        /// </summary>
        public double YstCP
        {
            get { return m_YstCP; }
            set
            {
                if (value == m_YstCP) { return; }
                m_YstCP = value;
            }
        }
        /// <summary>
        /// 損益
        /// </summary>
        public double ProfitLoss
        {
            get { return m_ProfitLoss; }
            set
            {
                if (value == m_ProfitLoss) { return; }
                m_ProfitLoss = value;
            }
        }
        /// <summary>
        /// 更新時間
        /// </summary>
        public string UpdateTime
        {
            get { return m_UpdateTime; }
            set
            {
                if (value == m_UpdateTime) { return; }
                m_UpdateTime = value;
            }
        }
        #endregion

        /// <summary>
        /// Capital 未平倉 Data
        /// </summary>
        /// <param name="data"></param>
        public OpenInterest(string data)
        {
            string[] list = data.Split(',');
            ExchangeID = list[0].Trim();
            ExchangeName = list[1].Trim();
            BrokerID = list[2].Trim().Substring(0, 7);
            CustNo = list[2].Trim().Substring(7, 7);
            ComID = list[3].Replace(" ", "").Trim();
            ComName = list[4].Trim();
            Enum.TryParse<BuySell>(list[5].Trim(), out m_BuySell);
            int.TryParse(list[6], out m_Qty);
            double.TryParse(list[7], out m_MP);
            double.TryParse(list[8], out m_AvgP);
            double.TryParse(list[9], out m_YstCP);
            double.TryParse(list[10], out m_ProfitLoss);
            UpdateTime = DateTime.Now.ToString("HH:mm:ss");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}  {12}", ExchangeID, ExchangeName, BrokerID, CustNo, ComID, ComName, BuySell, Qty, MP, AvgP, YstCP, ProfitLoss, UpdateTime);
        }
    }
}
