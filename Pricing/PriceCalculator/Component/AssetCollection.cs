using log4net;
using PriceCalculator.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PriceCalculator.Component
{
    public class AssetCollection
    {
        private const string CASH = "SELECT * FROM ETFForBrian..tblCashOfPCF Where DataDate='{0}' ";
        private const string FORWARD = "SELECT * FROM ETFForBrian..tblForwardOfPCF Where DataDate='{0}' ";
        private const string MARGIN = "SELECT * FROM ETFForBrian..tblMarginOfPCF Where DataDate='{0}' ";

        #region Variable
        private ILog m_Log = LogManager.GetLogger(typeof(AssetCollection));
        private Dictionary<string, Dictionary<string, List<Asset>>> m_BaseCrncys = new Dictionary<string, Dictionary<string, List<Asset>>>();
        private Dictionary<string, Dictionary<string, List<Asset>>> m_ETFCodes = new Dictionary<string, Dictionary<string, List<Asset>>>();
        #endregion

        #region Property
        public Dictionary<string, List<Asset>> this[AssetKey type, string Key]
        {
            get
            {
                switch (type)
                {
                    case AssetKey.ETFCode:
                        return m_ETFCodes.ContainsKey(Key) ? m_ETFCodes[Key] : null;
                    case AssetKey.BaseCrncy:
                        return m_BaseCrncys.ContainsKey(Key) ? m_BaseCrncys[Key] : null;
                    default:
                        return null;
                }

            }
        }

        #endregion

        //public static AssetCollection Create(AssetType type, DateTime date)
        public static AssetCollection Create(AssetType type, string date)
        {
            return new AssetCollection(type, date);
        }
        //private AssetCollection(AssetType assetType, DateTime date)
        private AssetCollection(AssetType assetType, string date)
        {
            Util.Info(m_Log, nameof(AssetCollection), $"Create {assetType} Collection");
            string str = string.Empty;
            Asset asset = null;
            switch (assetType)
            {
                case AssetType.Cash:
                    str = "SELECT * FROM ETFForBrian..tblCashOfPCF Where DataDate='{0}' ";
                    break;

                case AssetType.Margin:
                    str = "SELECT * FROM ETFForBrian..tblMarginOfPCF Where DataDate='{0}' ";
                    break;

                case AssetType.Forward:
                    str = "SELECT * FROM ETFForBrian..tblForwardOfPCF Where DataDate='{0}' ";
                    break;
                default:
                    Util.Error(m_Log, nameof(AssetCollection), $"{assetType} No SQL String");
                    return;
            }
            //string sqlstring = string.Format(str, date.ToString(Util.DATESTR));
            string sqlstring = string.Format(str, date);
            DataTable dt = Util.SQL.DoQuery(sqlstring);
            if (dt == null || dt.Rows.Count <= 0)
            {
                Util.Info(m_Log, nameof(AssetCollection), $"No Asset Data:{sqlstring}");
                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                switch (assetType)
                {
                    case AssetType.Cash:
                        asset = new Cash(row);
                        break;
                    case AssetType.Margin:
                        asset = new Margin(row);
                        break;
                    case AssetType.Forward:
                        asset = new Forward(row);
                        break;
                }
                if (asset == null)
                {
                    Util.Error(m_Log, nameof(AssetCollection), $"Create Asset Fail: {assetType}, {row}");
                    continue;
                }

                //以ETFCode為Key的Asset List
                if (!m_ETFCodes.ContainsKey(asset.ETFCode))
                {
                    m_ETFCodes.Add(asset.ETFCode, new Dictionary<string, List<Asset>>());
                }
                //以ETFCode為Key的Asset List, 第二層的KEY是BaseCrncy
                if (!m_ETFCodes[asset.ETFCode].ContainsKey(asset.BaseCrncy))
                {
                    m_ETFCodes[asset.ETFCode].Add(asset.BaseCrncy, new List<Asset>());
                }
                m_ETFCodes[asset.ETFCode][asset.BaseCrncy].Add(asset);

                //以BaseCrncy為Key的Asset List
                if (!m_BaseCrncys.ContainsKey(asset.BaseCrncy))
                {
                    m_BaseCrncys.Add(asset.BaseCrncy, new Dictionary<string, List<Asset>>());
                }
                //以BaseCrncy為Key的Asset List, 第二層的KEY是ETFCode
                if (!m_BaseCrncys[asset.BaseCrncy].ContainsKey(asset.ETFCode))
                {
                    m_BaseCrncys[asset.BaseCrncy].Add(asset.ETFCode, new List<Asset>());
                }
                m_BaseCrncys[asset.BaseCrncy][asset.ETFCode].Add(asset);
            }
        }

        /// <summary>
        /// 以不同的Key找商品
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool Contains(AssetKey type, string Key)
        {
            if (type != AssetKey.ETFCode)
            {
                return ((type == AssetKey.BaseCrncy) && m_BaseCrncys.ContainsKey(Key));
            }
            return m_ETFCodes.ContainsKey(Key);
        }
    }
}
