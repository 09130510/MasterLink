using System;
using System.Collections.Generic;
using System.Data;
using log4net;
using PriceCalculator.Utility;
using System.Reflection;

namespace PriceCalculator.Component
{
    /// <summary>
    /// 分類過的商品資料, 可以用不同種類的key尋找商品
    /// </summary>
    public class Collection
    {
        private const string FUND = "SELECT F.*, L.ExpiredDate AS ExpiredDate, ISNULL(L.YP,0) AS LockYP FROM ETFFORBRIAN..TBLFUNDOFPCF F LEFT JOIN (SELECT * FROM tblFixYstPrice WHERE Convert(varchar(20), ExpiredDate, 112) >= Convert(varchar(20), GETDATE(), 112)  AND Type = '{1}') L ON L.ETFCode = F.ETFCode AND L.PID = F.PID  WHERE DATADATE='{0}' ";
        private const string FUT = "SELECT FP.*, ISNULL(F.EXCHANGE,'') AS EXCHANGE, ISNULL(F.CURRENCY, '') AS CURRENCY, ISNULL(F.CVALUE, 1) AS CVALUE,ISNULL(F.CAPITALFORMAT, '') AS CAPITALFORMAT, ISNULL(F.PATSFORMAT, '') AS PATSFORMAT,ISNULL(F.IPUSHFORMAT, '') AS IPUSHFORMAT, ISNULL(F.REDISFORMAT, '') AS REDISFORMAT,ISNULL(F.XQUOTEFORMAT, '') AS XQUOTEFORMAT, L.ExpiredDate AS ExpiredDate, ISNULL(L.YP,0) AS LockYP FROM TBLFUTUREOFPCF FP LEFT JOIN TBLFUTURE F ON FP.HEAD=F.HEAD LEFT JOIN(SELECT* FROM tblFixYstPrice WHERE Convert(varchar(20), ExpiredDate, 112) >= Convert(varchar(20), GETDATE(), 112)  AND Type = '{1}') L ON FP.ETFCode=L.ETFCode AND FP.PID=L.PID WHERE DATADATE='{0}'";
        private const string STK = "SELECT S.*, L.ExpiredDate AS ExpiredDate, ISNULL(L.YP,0) AS LockYP FROM ETFFORBRIAN..TBLSTOCKOFPCF S LEFT JOIN (SELECT * FROM tblFixYstPrice WHERE Convert(varchar(20), ExpiredDate, 112) >= Convert(varchar(20), GETDATE(), 112)  AND Type = '{1}') L ON L.ETFCode = S.ETFCode AND L.PID = S.PID  WHERE DATADATE='{0}'";


        private ILog m_Log = LogManager.GetLogger(typeof(Collection));
        /// <summary>
        /// Level 1: ETFCode
        /// Level 2: PID
        /// </summary>
        private Dictionary<string, Dictionary<string, Composition>> m_ETFCodes = new Dictionary<string, Dictionary<string, Composition>>();
        /// <summary>
        /// Level 1: PID 
        /// Level 2: ETFCode
        /// </summary>
        private Dictionary<string, Dictionary<string, Composition>> m_PIDs = new Dictionary<string, Dictionary<string, Composition>>();
        /// <summary>
        /// Level 1: Redis
        /// Level 2: ETFCode
        /// </summary>
        private Dictionary<string, Dictionary<string, Composition>> m_Redises = new Dictionary<string, Dictionary<string, Composition>>();
        /// <summary>
        /// Level 1: iPush
        /// Level 2: ETFCode
        /// </summary>
        private Dictionary<string, Dictionary<string, Composition>> m_iPushs = new Dictionary<string, Dictionary<string, Composition>>();
        /// <summary>
        /// Level 1: Capital
        /// Level 2: ETFCode
        /// </summary>
        private Dictionary<string, Dictionary<string, Composition>> m_Capitals = new Dictionary<string, Dictionary<string, Composition>>();
        /// <summary>
        /// Level 1: PATS
        /// Level 2: ETFCode
        /// </summary>
        private Dictionary<string, Dictionary<string, Composition>> m_PATSs = new Dictionary<string, Dictionary<string, Composition>>();
        /// <summary>
        /// Level 1: Currency
        /// Level 2: ETFCode
        /// </summary>
        private Dictionary<string, Dictionary<string, Composition>> m_Currencies = new Dictionary<string, Dictionary<string, Composition>>();

        public Dictionary<string, Composition> this[CollectionKey type, string Key]
        {
            get
            {
                switch (type)
                {
                    case CollectionKey.ETFCode:
                        return m_ETFCodes.ContainsKey(Key) ? m_ETFCodes[Key] : null;
                    case CollectionKey.PID:
                        return m_PIDs.ContainsKey(Key) ? m_PIDs[Key] : null;
                    case CollectionKey.Redis:
                        return m_Redises.ContainsKey(Key) ? m_Redises[Key] : null;
                    case CollectionKey.iPush:
                        return m_iPushs.ContainsKey(Key) ? m_iPushs[Key] : null;
                    case CollectionKey.Capital:
                        return m_Capitals.ContainsKey(Key) ? m_Capitals[Key] : null;
                    case CollectionKey.PATS:
                        return m_PATSs.ContainsKey(Key) ? m_PATSs[Key] : null;
                    case CollectionKey.Currency:
                        return m_Currencies.ContainsKey(Key) ? m_Currencies[Key] : null;
                }
                return null;
            }
        }

        //private Collection(CollectionType collectionType, DateTime date)
        private Collection(CollectionType collectionType, string date)
        {
            Util.Info(m_Log, nameof(Collection), $"Create {collectionType} Collection");

            string SQL = string.Empty;
            Composition comp = null;
            switch (collectionType)
            {
                case CollectionType.Stock:
                    SQL = string.Format(STK, date, "STK" );
                    break;
                case CollectionType.Future:
                    SQL = string.Format(FUT, date, "FUT");
                    break;
                case CollectionType.Fund:
                    SQL = string.Format(FUND, date, "FUND");
                    break;
                default:
                    Util.Error(m_Log, nameof(Collection), $"{collectionType} No SQL String");
                    return;
            }            
            DataTable dt = Util.SQL.DoQuery(SQL);
            if (dt == null || dt.Rows.Count <= 0)
            {
                Util.Info(m_Log, nameof(Collection), $"No Collection Data:{SQL}");
                return;
            }
            foreach (DataRow row in dt.Rows)
            {
                switch (collectionType)
                {
                    case CollectionType.Stock:
                        comp = new STK(row);
                        break;
                    case CollectionType.Future:
                        comp = new FUT(row);
                        break;
                    case CollectionType.Fund:
                        comp = new FUND(row);
                        break;
                }
                if (comp == null)
                {
                    Util.Error(m_Log, nameof(Collection), $"Create Composition Fail: {collectionType}, {row}");
                    continue;
                }
                //以ETFCode為Key的Composition List
                if (!m_ETFCodes.ContainsKey(comp.ETFCode))
                {
                    m_ETFCodes.Add(comp.ETFCode, new Dictionary<string, Composition>());
                }
                //以ETFCode為Key的Composition List, 第二層的KEY是PID
                if (!m_ETFCodes[comp.ETFCode].ContainsKey(comp.PID))
                {
                    m_ETFCodes[comp.ETFCode].Add(comp.PID, comp);
                }

                //以PID為Key的Composition List
                if (!m_PIDs.ContainsKey(comp.PID))
                {
                    m_PIDs.Add(comp.PID, new Dictionary<string, Composition>());
                }
                //以PID為Key的Composition List, 第二層的KEY是ETFCode
                if (!m_PIDs[comp.PID].ContainsKey(comp.ETFCode))
                {
                    m_PIDs[comp.PID].Add(comp.ETFCode, comp);
                }

                //以Redis訂閱字串為Key的Composition List
                if (!m_Redises.ContainsKey(comp.Redis))
                {
                    m_Redises.Add(comp.Redis, new Dictionary<string, Composition>());
                }
                //以Redis訂閱字串為Key的Composition List, 第二層的KEY是ETFCode
                if (!m_Redises[comp.Redis].ContainsKey(comp.ETFCode))
                {
                    m_Redises[comp.Redis].Add(comp.ETFCode, comp);
                }

                //以iPush訂閱字串為Key的Composition List
                if (!m_iPushs.ContainsKey(comp.iPush))
                {
                    m_iPushs.Add(comp.iPush, new Dictionary<string, Composition>());
                }
                //以iPush訂閱字串為Key的Composition List, 第二層的KEY是ETFCode
                if (!m_iPushs[comp.iPush].ContainsKey(comp.ETFCode))
                {
                    m_iPushs[comp.iPush].Add(comp.ETFCode, comp);
                }

                //以Capital訂閱字串為Key的Composition List
                if (!m_Capitals.ContainsKey(comp.Capital))
                {
                    m_Capitals.Add(comp.Capital, new Dictionary<string, Composition>());
                }
                //以Capital訂閱字串為Key的Composition List, 第二層的KEY是ETFCode
                if (!m_Capitals[comp.Capital].ContainsKey(comp.ETFCode))
                {
                    m_Capitals[comp.Capital].Add(comp.ETFCode, comp);
                }
                //空的就不加進去了?? 我想想
                if (!string.IsNullOrEmpty(comp.PATS))
                {
                    //以PATS訂閱字串為Key的Composition List
                    if (!m_PATSs.ContainsKey(comp.PATS))
                    {
                        m_PATSs.Add(comp.PATS, new Dictionary<string, Composition>());
                    }
                    //以PATS訂閱字串為Key的Composition List, 第二層的KEY是ETFCode
                    if (!m_PATSs[comp.PATS].ContainsKey(comp.ETFCode))
                    {
                        m_PATSs[comp.PATS].Add(comp.ETFCode, comp);
                    }
                }

                //以幣別為Key的Composition List
                if (!m_Currencies.ContainsKey(comp.BaseCurncy.ToString()))
                {
                    m_Currencies.Add(comp.BaseCurncy.ToString(), new Dictionary<string, Composition>());
                }
                //以幣別為Key的Composition List, 第二層的KEY是ETFCode
                if (!m_Currencies[comp.BaseCurncy.ToString()].ContainsKey(comp.ETFCode))
                {
                    m_Currencies[comp.BaseCurncy.ToString()].Add(comp.ETFCode, comp);
                }
            }
        }
        //public static Collection Create(CollectionType type, DateTime date)
        public static Collection Create(CollectionType type, string date)
        {
            return new Collection(type, date);
        }
        /// <summary>
        /// 以不同的Key找商品
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool Contains(CollectionKey type, string Key)
        {
            switch (type)
            {
                case CollectionKey.ETFCode:
                    return m_ETFCodes.ContainsKey(Key);
                case CollectionKey.PID:
                    return m_PIDs.ContainsKey(Key);
                case CollectionKey.Redis:
                    return m_Redises.ContainsKey(Key);
                case CollectionKey.iPush:
                    return m_iPushs.ContainsKey(Key);
                case CollectionKey.Capital:
                    return m_Capitals.ContainsKey(Key);
                case CollectionKey.PATS:
                    return m_PATSs.ContainsKey(Key);
                case CollectionKey.Currency:
                    return m_Currencies.ContainsKey(Key);
            }
            return false;
        }
    }
}