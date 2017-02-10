using System.Collections.Generic;
using System.Data;
using System.Linq;
using PriceProcessor.HK;
using Util.Extension;
using Util.Extension.Class;
using System;

namespace SinopacHK.Class
{
    /// <summary>
    /// 商品列表
    /// </summary>
    public class ProductCollection
    {
        //public static event HKProcessor.MarketPriceDelegate MarketPriceEvent;
        public static event ProductInfo.MarketPriceDelegate MarketPriceEvent;

        #region Variable
        private static ProductCollection m_Instance;
        private static ProductInfo m_Selected =null;
        private static string m_CurrentPID = string.Empty;
        private static decimal m_CurrentMarketPrice = 0M;
        private Dictionary<string, ProductInfo> m_List;
        #endregion

        #region Property
        public static ProductCollection Instance { get { return m_Instance ?? (m_Instance = new ProductCollection()); } }
        /// <summary>
        /// 目前商品資料
        /// </summary>
        public static ProductInfo Selected { get { return m_Selected; } }
        /// <summary>
        /// 目前商品代號
        /// </summary>
        public static string CurrentPID { get { return m_CurrentPID; } }
        /// <summary>
        /// 目前商品市價
        /// </summary>
        public static decimal CurrentMarketPrice { get { return m_CurrentMarketPrice; } }
        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        private ProductInfo this[string ProductID]
        {
            get
            {
                return m_List.ContainsKey(ProductID) ? m_List[ProductID] : null;
            }
        }
        #endregion

        private ProductCollection()
        {
            m_List = new Dictionary<string, ProductInfo>();

            //DataTable dt = Utility.SQL.DoQuery("SELECT * FROM PRODUCTINFO WHERE CURRENCY='HKD' ");
            DataTable dt = Utility.SQL.DoQuery("SELECT * FROM PRODUCTINFO WHERE EXCHANGE='SEHK'");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    ProductInfo pi = new ProductInfo(row["EXCHANGE"], row["PRODUCTID"], row["TRADEUNITS"], row["CURRENCY"], row["TICKNAME"]);
                    m_List.Add(pi.ProductID, pi);
                }
            }
        }
        /// <summary>
        /// 商品清單
        /// </summary>
        /// <returns></returns>
        public string[] ToArray()
        {
            return m_List.Keys.ToArray();
        }
        /// <summary>
        /// 選擇商品
        /// </summary>
        /// <param name="ProductID"></param>
        public static void Select(string ProductID)
        {
            try
            {
                if (m_Selected != null)
                {
                    if (m_Selected.ProductID == ProductID) { return; }
                    m_Selected.Stop();
                    //m_Selected.Getter.MarketPriceEvent -= new PriceProcessor.Processor.MarketPriceDelegate(Getter_MarketPriceEvent);
                    m_Selected.OnMarketPrice = null;
                    NotificationCenter.Instance.Post("PRODUCTCHANGING");
                }

                m_Selected = ProductCollection.Instance[ProductID];
                m_CurrentPID = string.Empty;
                m_CurrentMarketPrice = 0;
                //m_Selected.Getter.MarketPriceEvent += new PriceProcessor.Processor.MarketPriceDelegate(Getter_MarketPriceEvent);
                //m_Selected.OnMarketPrice = new PriceProcessor.HK.HKProcessor.MarketPriceDelegate(Getter_MarketPriceEvent);
                m_Selected.OnMarketPrice += Getter_MarketPriceEvent;  
                if (m_Selected != null) { m_Selected.Start(); }
                NotificationCenter.Instance.Post("PRODUCTCHANGED");
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        private static void Getter_MarketPriceEvent(string PID, string TickName, decimal Price)
        {
            m_CurrentPID = PID;
            m_CurrentMarketPrice = Price;
            if (MarketPriceEvent != null) { MarketPriceEvent(PID, TickName, Price); }
        }
    }
    /// <summary>
    /// 商品資訊
    /// </summary>
    public class ProductInfo
    {
        #region Variable
        private HKProcessor m_Getter;
        public delegate void MarketPriceDelegate(string PID, string TickName, decimal Price);
        public MarketPriceDelegate OnMarketPrice;
        //public HKProcessor.MarketPriceDelegate m_OnMarketPrice;
        #endregion

        #region Property
        public string ExchangeID { get; private set; }
        public string ProductID { get; private set; }
        public int Units { get; private set; }
        public string Currency { get; private set; }
        public string TickName { get; private set; }
        public HKProcessor Getter { get { return m_Getter; } }
        //public HKProcessor.MarketPriceDelegate OnMarketPrice
        //{
        //    set
        //    {
        //        m_Getter.MarketPriceEvent -= m_OnMarketPrice;
        //        m_OnMarketPrice = value;
        //        m_Getter.MarketPriceEvent += m_OnMarketPrice;
        //    }
        //}
        
        #endregion

        public ProductInfo(string exchange, string productID, int units, string currency, string tickname)
        {
            ExchangeID = exchange;
            ProductID = productID;
            Units = units;
            Currency = currency;
            TickName = tickname;

            m_Getter = new HKProcessor(ProductID,3000);
            m_Getter.MarketPriceEvent += new HKProcessor.MarketPriceDelegate(Getter_MarketPriceEvent);
        }

        private void Getter_MarketPriceEvent(string PID, decimal Price)
        {
            if (OnMarketPrice!= null)
            {
                OnMarketPrice(PID, TickName, Price);
            }
        }
        public ProductInfo(object exchange, object productID, object units, object currency, object tickname)
            : this(exchange.ToString(), productID.ToString(), units.ToInt(), currency.ToString(), tickname.ToString())
        { }

        /// <summary>
        /// 接收價格
        /// </summary>
        public void Start() { m_Getter.Start(); }
        /// <summary>
        /// 停止接收價格
        /// </summary>
        public void Stop() { m_Getter.Stop(); }
    }
}
