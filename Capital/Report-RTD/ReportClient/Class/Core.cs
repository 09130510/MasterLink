using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using PriceProcessor.Capital;
using Util.Extension;
using Util.Extension.Class;
using static PriceProcessor.Capital.CapitalProcessor;
using System.Drawing;

namespace Capital.Report.Class
{
    public class Core
    {
        private struct SelectedOrder
        {
            public string OrdNo;
            public int Qty;
        }

        #region Event
        public event OnOverseaProductDelegate OnOverseaProduct;
        public event OnPriceChangeDelegate OnPriceChange;
        #endregion

        #region Variable
        private static Core m_Instance;
        private CapitalProcessor m_Price;
        private OrderProcessor.Capital.CapitalProcessor m_Order;

        private SQLTool m_SQL;
        private Dictionary<int, frmTick> m_TickForms = new Dictionary<int, frmTick>();
        private System.Timers.Timer m_RequestTimer = new System.Timers.Timer(5000);
        #endregion

        #region Property
        public static Core Instance { get { return m_Instance ?? (m_Instance = new Core()); } }
        public bool isCapitalInit
        {
            get { return Order.isCapitalInit && Price.isCapitalInit; }
        }
        public bool isCapitalConnect
        {
            get { return Order.isCapitalConnect && Price.isCapitalConnect; }
        }
        public CapitalProcessor Price { get { return m_Price; } }
        public OrderProcessor.Capital.CapitalProcessor Order { get { return m_Order; } }

        public int LotsLimit { get { return Utility.Load<int>("ORDERSETTING", "LOTSLIMIT"); } }
        public int AutoLotsLimit { get; set; }
        #endregion

        private Core()
        {
            m_SQL = new SQLTool(Utility.Load<string>("SQL", "IP"), Utility.Load<string>("SQL", "DB"), Utility.Load<string>("SQL", "ID"), Utility.Load<string>("SQL", "PASSWORD"));

            m_Price = new CapitalProcessor();
            m_Price.OnOverseaProduct += new OnOverseaProductDelegate(OverseaProduct);
            m_Price.OnPriceChange += new OnPriceChangeDelegate(PriceChange);
            m_Price.OnError += OnError;

            m_Order = new OrderProcessor.Capital.CapitalProcessor();
            m_Order.OnMatchReply += new OrderProcessor.Capital.CapitalProcessor.OnReplyDelegate(OnMatchReply);
            m_Order.OnOverseaOpenInterest += new OrderProcessor.Capital.CapitalProcessor.OnOverseaOpenInterestDelegate(OnOverseaOpenInterest);
        }

        #region Delegate
        private void OnError(string MethodName, Exception ex)
        {
            Utility.Log(m_Price, MethodName, ex.Message);
        }
        private void PriceChange(string PID, string TickName, Price price)
        {
            OnPriceChange?.Invoke(PID, TickName, price);
        }
        private void OverseaProduct(string Exchange, string PID)
        {
            OnOverseaProduct?.Invoke(Exchange, PID);
        }

        private void OnMatchReply(OrderProcessor.Capital.ReplyType ReplyType, OrderProcessor.Capital.Order ord)
        {
            Utility.Log(this, "MatchToSQL", ord.ToString());
            Match2SQL(ord);
        }
        private void OnOverseaOpenInterest(List<OrderProcessor.Capital.OpenInterest> OpenInterests)
        {
            if (OpenInterests == null) { return; }
            foreach (var item in OpenInterests)
            {
                Utility.Log(this, "OIToSQL", item.ToString());
                OpenInterest2SQL(item);
            }
        }
        #endregion

        #region Public
        //public void Initialize(string account, string password)
        public void Initialize()
        {
            string msg;
            string id = Utility.Load<string>("login", "account");
            string pwd = Utility.Load<string>("login", "pwd");
            Utility.Log(this, "Price Initialize", $"{id}:{pwd}");
            m_Price.Start(id, pwd, out msg);

            Utility.Log(this, "Order Initialize", $"{id}:{pwd}");
            //m_Order.Start(account, password, out msg);
            m_Order.Start(id, pwd, out msg);
        }
        //public void ReplyDisconnect(string account, out string Msg)
        public void ReplyDisconnect(out string Msg)
        {
            Msg = string.Empty;
            //m_Order.ReplyDisconnect(account, out Msg);
            m_Order.ReplyDisconnect(out Msg);
            m_Price.Stop();
            //Utility.Log(this, "PriceStop", Msg);
        }
        //public string[] Products(string productFilter)
        public string[] Products()
        {
            List<string> re = new List<string>();
            string[] filters = Utility.Load<string>("ORDERSETTING", "FILTER").Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            if (filters.Length == 0)
            {
                foreach (var product in m_Price.Products)
                {
                    re.Add(product.Key);
                }
            }
            else
            {
                foreach (string filter in filters)
                {
                    foreach (var product in m_Price.Products)
                    {
                        if ($"{product.Value.Exchange},{product.Value.PIDHead}" == filter)
                        {
                            re.Add(product.Key);
                        }
                    }
                }
            }
            return re.ToArray();
        }
        public string[] ProductFilters()
        {
            return m_Price.Products.Values.Select(e => e.Exchange + "," + e.PIDHead).Distinct().ToArray();
        }

        #region Tick Form
        public frmTick CreateTickForm(string pid)
        {
            for (short i = 0; i < 50; i++)
            {
                if (!m_TickForms.ContainsKey(i))
                {
                    frmTick tick = new frmTick(i, pid);
                    m_TickForms.Add(i, tick);
                    return tick;
                }
            }
            Utility.Log(this, "TickFormErr", "Tick Form > 50");
            return null;
        }
        public void DisposeTickForm(int pageno) { m_TickForms.Remove(pageno); }
        #endregion
        #region Price Sub / UnSub
        public void SubPrice(short pageno, string pid)
        {
            m_Price.Subscribe(pageno, pid);
            Utility.Log(this, "PriceSub", $"No:{pageno} PID:{pid}");
        }
        public void UnsubPrice(short pageno, string pid)
        {
            m_Price.Unsubscribe(pageno, pid);
            Utility.Log(this, "PriceUnsub", $"No:{pageno} PID:{pid}");
        }
        #endregion
        #endregion

        #region Private
        /// <summary>
        /// 成交寫入資料庫
        /// </summary>
        /// <param name="o"></param>        
        private void Match2SQL(OrderProcessor.Capital.Order o)
        {
            string date = DateTime.Now.ToString("yyyy/MM/dd");
            string qrysql = $"SELECT COUNT(*) FROM CAPITALMATCH WHERE DATE='{date}' AND ORDNO='{ o.OrdNo}'";
            DataTable dt = m_SQL.DoQuery(qrysql);
            if (dt.Rows[0][0].ToInt() <= 0)
            {
                string sql = $"INSERT INTO CAPITALMATCH ([DATE],ORDNO,CUSTNO,COMID,SIDE,LOTS,PRICE,TIME) VALUES ('{date}','{o.OrdNo}','{o.CustNo}','{o.ComID}',{(o.BuySell == OrderProcessor.Side.B ? 0 : 1)},{o.Qty},{o.Price},'{o.Time}')";
                try
                {
                    m_SQL.DoExecute(sql);
                }
                catch (Exception) { }

            }
        }
        /// <summary>
        /// 未平倉寫入資料庫
        /// </summary>
        /// <param name="op"></param>        
        private void OpenInterest2SQL(OrderProcessor.Capital.OpenInterest op)
        {
            string today = DateTime.Now.ToString("yyyy/MM/dd");
            string exist = $"SELECT * FROM CAPITALOPENINTEREST WHERE [DATE]='{today}' AND EXCHANGEID='{op.ExchangeID}' AND BROKERID='{op.BrokerID}' AND CUSTNO='{op.CustNo}' AND COMID='{op.ComID}'";
            string sql;
            DataTable dtExist = m_SQL.DoQuery(exist);
            if (dtExist == null || dtExist.Rows.Count <= 0)
            {
                //新增
                sql = $"INSERT INTO CAPITALOPENINTEREST ([DATE],EXCHANGEID,EXCHANGENAME,BROKERID,CUSTNO,COMID,COMNAME,BUYSELL,QTY,MARKETPRICE,AVERAGEPRICE,YSTCLOSEPRICE,PROFITLOSS) VALUES ('{today}','{op.ExchangeID}','{op.ExchangeName}','{op.BrokerID}','{op.CustNo}','{op.ComID}','{op.ComName}',{(op.BuySell == OrderProcessor.Side.B ? 0 : 1)},{op.Qty},{op.MP},{op.AvgP},{op.YstCP},{op.ProfitLoss})";
            }
            else
            {
                //更新
                sql = $"UPDATE CAPITALOPENINTEREST SET BUYSELL={(op.BuySell == OrderProcessor.Side.B ? 0 : 1)}, QTY={op.Qty}, MARKETPRICE={op.MP}, AVERAGEPRICE={op.AvgP}, YSTCLOSEPRICE={op.YstCP}, PROFITLOSS={op.ProfitLoss}, UPDATETIME=Convert(varchar(20),GETDATE(),114) WHERE [DATE]='{today}' AND EXCHANGEID='{op.ExchangeID}' AND BROKERID='{op.BrokerID}' AND CUSTNO='{op.CustNo}' AND COMID='{op.ComID}'";
            }
            m_SQL.DoExecute(sql);
        }
        #endregion

        #region Order
        private bool _CanOrder(string Exchange, string OrderHead, string YM, String YM2, double Price)
        {
            string msg;
            if (!Order.isCapitalConnect) { return false; }
            if (Utility.Load<bool>("ORDERSETTING", "STOPORDER"))
            {
                msg = "已勾選【禁止下單】！";
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單錯誤", msg);
                Utility.Log(this, "Order", msg);
                return false;
            }
            if (string.IsNullOrEmpty(Exchange) || string.IsNullOrEmpty(OrderHead) || string.IsNullOrEmpty(YM))
            {
                msg = "尚未選擇商品，不下單！";
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單錯誤",
                                    new MsgLine("商品錯誤", msg));
                Utility.Log(this, "Order", msg);
                return false;
            }
            if (Price <= 0D && string.IsNullOrEmpty(YM2))
            {
                msg = "委託價 = 0，不下單！";
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單錯誤",
                                    new MsgLine("價格錯誤", msg));
                Utility.Log(this, "Order", msg);
                return false;
            }
            int qty = Utility.Load<int>("ORDERSETTING", "LOTS");
            if (qty == 0)
            {
                msg = "口數 = 0，不下單！";
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單錯誤",
                                    new MsgLine("數量錯誤", msg));
                Utility.Log(this, "Order", msg);
                return false;
            }
            //股數限制
            int limit = (LotsLimit != 0 && AutoLotsLimit != 0 ? Math.Min(LotsLimit, AutoLotsLimit) : Math.Max(LotsLimit, AutoLotsLimit));

            if (qty > limit)
            {
                msg = $"口數 [{qty}] > 口數上限 [{limit}]";
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單錯誤",
                    new MsgLine("數量錯誤", msg));
                Utility.Log(this, "Order", msg);
                return false;
            }
            return true;
        }
        private bool _CanCancel(string Account, string KeyNo)
        {
            string msg;
            if (!Order.isCapitalConnect) { return false; }
            if (Utility.Load<bool>("ORDERSETTING", "STOPORDER"))
            {
                msg = "已勾選【禁止下單】！";
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單錯誤", msg);
                Utility.Log(this, "Cancel", msg);
                return false;
            }
            if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(KeyNo))
            {
                msg = "尚未選擇商品，不下單！";
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單錯誤",
                                    new MsgLine("商品錯誤", msg));
                Utility.Log(this, "Cancel", msg);
                return false;
            }
            return true;
        }
        public void DoOrder(Tick t, OrderProcessor.Side Side, double Price)
        {
            DoOrder(t.Account, t.Exchange, t.OrderHead, t.YM, t.YM2, Side, Price);
        }
        public void DoOrder(string Account, string Exchange, string OrderHead, string YM, string YM2, OrderProcessor.Side Side, double Price, int qty = -1)
        {
            if (!_CanOrder(Exchange, OrderHead, YM, YM2, Price)) { return; }

            string msg;
            //int qty = Utility.Load<int>("ORDERSETTING", "LOTS");
            if (qty == -1)
            {
                qty = Utility.Load<int>("ORDERSETTING", "LOTS");
            }

            if (Utility.Load<bool>("ORDERSETTING", "ORDERALERT"))
            {
                //下單前詢問
                Color color = Side == OrderProcessor.Side.B ? Color.Crimson : Color.ForestGreen;
                if (!AlertBox.Alert(null, AlertBoxButton.OKCancel, "下單",
                    new MsgLine("帳號", Account, color),
                    new MsgLine("交易所", Exchange, color),
                    new MsgLine("商品", OrderHead, color),
                    new MsgLine("月份", !string.IsNullOrEmpty(YM2) ? $"{YM}/{YM2}" : YM, color),
                    new MsgLine("買賣", Side, color),
                    new MsgLine("口數", qty.ToString("#,##0"), color),
                    new MsgLine("價格", Price, color)))
                {
                    Utility.Log(this, "Order", "Cancel");
                    return;
                }
            }

            if (string.IsNullOrEmpty(YM2))
            {
                bool re = Order.Order(Account, Exchange, OrderHead, YM, Side, qty, Price, out msg);
                Utility.Log(this, "Order", msg);
                if (!re)
                {

                    AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單錯誤",
                        new MsgLine("帳號", Account),
                        new MsgLine("交易所", Exchange),
                        new MsgLine("商品", OrderHead),
                        new MsgLine("月份", YM),
                        new MsgLine("買賣", Side),
                        new MsgLine("口數", qty),
                        new MsgLine("價格", Price),
                        new MsgLine("錯誤", msg, Color.Crimson));
                }
            }
            else
            {
                bool re = Order.SpreadOrder(Account, Exchange, OrderHead, YM, YM2, Side, qty, Price, out msg);
                Utility.Log(this, "SpreadOrder", msg);
                if (!re)
                {
                    AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "轉倉下單錯誤",
                        new MsgLine("帳號", Account),
                        new MsgLine("交易所", Exchange),
                        new MsgLine("商品", OrderHead),
                        new MsgLine("月份", !string.IsNullOrEmpty(YM2) ? $"{YM}/{YM2}" : YM),
                        new MsgLine("買賣", Side),
                        new MsgLine("口數", qty),
                        new MsgLine("價格", Price),
                        new MsgLine("錯誤", msg, Color.Crimson));
                }
            }
        }

        public void DoCancel(string Account, string OrderPID, OrderProcessor.Side Side, double Price)
        {
            lock (this)
            {
                var orders = from order in Order.Valids(OrderPID)
                             where
                             order.Price == Price && order.BuySell == Side
                             select order.KeyNo;
                _Cancel(Account, orders);
            }
        }
        public void DoCancel(string Account, string OrderPID, OrderProcessor.Side Side, double Max, double Min)
        {
            lock (this)
            {
                var orders = from order in Order.Valids(OrderPID)
                             where order.Price > Max || order.Price < Min
                             select order.KeyNo;
                _Cancel(Account, orders);
            }
        }
        public void DoCancel(string Account, string OrderPID, OrderProcessor.Side Side)
        {
            lock (this)
            {
                var orders = from order in Order.Valids(OrderPID)
                             where order.BuySell == Side
                             select order.KeyNo;
                _Cancel(Account, orders);
            }
        }
        public void DoCancel(string Account, string OrderPID)
        {
            lock (this)
            {
                var orders = from order in Order.Valids(OrderPID)
                             select order.KeyNo;
                _Cancel(Account, orders);
            }
        }
        public bool DoCancel2(string Account, string KeyNo)
        {
            if (!_CanCancel(Account, KeyNo)) { return false; }

            string sb;
            bool re = Order.Cancel(Account, KeyNo, out sb);
            Utility.Log(this, "Cancel", sb.ToString());
            if (!re)
            {
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "刪單錯誤",
                    new MsgLine("帳號", Account),
                    new MsgLine("KeyNo", KeyNo),
                    new MsgLine("錯誤", sb, Color.Crimson));
            }
            return re;
        }
        private void _Cancel(string Account, IEnumerable<string> KeyNOs)
        {
            if (KeyNOs == null || KeyNOs.Count() <= 0) { return; }
            foreach (string KeyNo in KeyNOs)
            {
                DoCancel2(Account, KeyNo);
            }
        }
        #endregion

        #region Status
        public string OrderStatus(string OrderPID, OrderProcessor.Side side)
        {
            if (string.IsNullOrEmpty(OrderPID)) { return string.Empty; }
            lock (this)
            {
                var orders = from order in Order.Valids(OrderPID)
                             where order.BuySell == side
                             select new SelectedOrder
                             {
                                 Qty = order.SumQty,
                                 OrdNo = order.OrdNo
                             };
                int qty = orders.Sum(e => e.Qty);
                return qty == 0 ? "" : qty.ToString();
            }
        }
        public string OrderStatus(string OrderPID, OrderProcessor.Side side, double price)
        {
            if (string.IsNullOrEmpty(OrderPID) || price == 0D) { return string.Empty; }
            lock (this)
            {
                var orders = from order in Order.Valids(OrderPID)
                             where order.BuySell == side && order.Price == price
                             select new SelectedOrder
                             {
                                 Qty = order.AfterQty,
                                 OrdNo = order.OrdNo
                             };
                int qty = orders.Sum(e => e.Qty);
                return qty == 0 ? "" : qty.ToString();
            }
        }
        public string OrderStatus(string OrderPID, OrderProcessor.Side side, double max, double min)
        {
            if (string.IsNullOrEmpty(OrderPID) || (max == 0D && min == 0D)) { return string.Empty; }
            lock (this)
            {
                var orders = from order in Order.Valids(OrderPID)
                             where order.BuySell == side && (order.Price > max || order.Price < min)
                             select new SelectedOrder
                             {
                                 Qty = order.AfterQty,
                                 OrdNo = order.OrdNo
                             };
                int qty = orders.Sum(e => e.Qty);
                return qty == 0 ? "" : qty.ToString();
            }
        }
        #endregion
    }
}