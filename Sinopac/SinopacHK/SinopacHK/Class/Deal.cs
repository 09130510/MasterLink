using System;
using System.Collections.Generic;
using System.Linq;
using OrderProcessor;
using OrderProcessor.SinoPac;
using Util.Extension;
using Util.Extension.Class;

namespace SinopacHK.Class
{
    /// <summary>
    /// 下單處理 
    /// 更換商品只刪單不重建
    /// </summary>
    public class Deal : NotifyDisposableClass
    {
        #region Variable
        private Unit m_Unit;
        private List<FIXOrder> m_Orders = new List<FIXOrder>();
        #endregion

        #region Property
        private ProductInfo Product { get { return m_Unit.Product; } }
        private int OrderQty { get { return m_Unit.OrderQty; } }
        public bool isConnect { get { return Utility.Processor.isConnect; } }
        public List<FIXOrder> AliveOrders
        {
            get
            {
                return m_Orders.Where(e => e.isAlive).ToList();
            }
        }
        public List<FIXOrder> Matchs
        {
            get
            {
                return m_Orders.Where(e => e.MatchQty > 0
                    /* && e.ProductID == ProductCollection.Selected.ProductID*/).ToList();
            }
        }
        #endregion

        public Deal(Unit unit)
        {
            m_Unit = unit;
            NotificationCenter.Instance.AddObserver(OnProductChanged, "PRODUCTCHANGED");
            Utility.Processor.OrderReplyEvent += OrderReplyEvent;
            Utility.Processor.MatchReplyEvent += MatchReplyEvent;
        }
        protected override void DoDispose()
        {
            Cancel();
            Utility.Processor.OrderReplyEvent -= new SinoPacProcessor.OrderReplyDelegate(OrderReplyEvent);
            Utility.Processor.MatchReplyEvent -= new SinoPacProcessor.MatchReplyDelegate(MatchReplyEvent);
            //NotificationCenter.Instance.RemoveObserver(OnProductChanging, "PRODUCTCHANGING");
        }

        #region Delegate
        private void OnProductChanged(Notification n)
        {
            //Cancel();
            ((frmOrder)Utility.Unit.OrderForm).SetOrderSummary(OrderStatus(Side.B), OrderStatus(Side.S));
        }
        private void OrderReplyEvent(SinoPacProcessor sender, SinoPacRPT reply)
        {
            NotificationCenter.Instance.Post(Utility.NotifyType.Msg.ToString(), new Notification("Deal",
                string.Format("ClOrdID:{0} OrdNo:{1} Symbol:{2} Price:{3} Qty:{4} ExecType:{5} Msg:{6}", reply.ClOrdID, reply.OrderID, reply.Symbol, reply.Price, reply.Qty, reply.ExecType, reply.Msg)));

            FIXOrder fixorder = m_Orders.FirstOrDefault(e => e.isMyClOrdID(reply.ClOrdID));
            if (fixorder == null) { return; }
            fixorder.AddRPT(reply);

            if ((reply.ExecType == OrderProcessor.ExecType.New || reply.ExecType == OrderProcessor.ExecType.Canceled) && reply.CxlRejReason == CxlRejReason.None)
            {
            }
            else
            {
                NotificationCenter.Instance.Post(Utility.NotifyType.Error.ToString(), new Notification("Deal", string.Format("ClOrdID:{0}\r\nOrdNo:{1}\r\nSymbol:{2}\r\nPrice:{3}\r\nQty:{4}\r\nExecType:{5}\r\nMsg:{6}", reply.ClOrdID, reply.OrderID, reply.Symbol, reply.Price, reply.Qty, reply.ExecType, reply.Msg)));
            }
            ((frmOrder)Utility.Unit.OrderForm).SetOrderSummary(OrderStatus(Side.B), OrderStatus(Side.S));
            NotificationCenter.Instance.Post("ORDERREPLY", new Notification(this, fixorder));
        }
        private void MatchReplyEvent(SinoPacProcessor sender, SinoPacRPT reply)
        {
            FIXOrder fixorder = m_Orders.FirstOrDefault(e => e.isMyClOrdID(reply.ClOrdID));
            if (fixorder == null) { return; }
            fixorder.AddMAT(reply);
            ((frmOrder)Utility.Unit.OrderForm).SetOrderSummary(OrderStatus(Side.B), OrderStatus(Side.S));

            //PS.價格要*1000 (因為EXCEL撈出來後會/1000)
            string sql = string.Format("INSERT INTO ORDERREPORT(CREATEDATETIME,REPORTTYPE,TRADECOMPANYID,ORDERNUMBER,ACCOUNT,PRODUCTID,PRODUCTNAME,ERRORCODE,PRICE,QUANTITY,TRANSACTIONTYPE) VALUES(NOW(),'02','{0}','{1}','{2}','{3}H','{4}H','00',{5},{6},'{7}') ", Utility.SenderCompID, reply.OrderID, reply.Account, reply.Symbol, reply.Symbol, reply.Price *1000, reply.Qty, reply.Side == Side.B?"1":"2");
            Utility.Execuate(sql);
            NotificationCenter.Instance.Post("MATCHREPLY", new Notification(this, fixorder));
        }
        #endregion

        #region Order
        /// <summary>
        /// 下單
        /// </summary>
        /// <param name="side">買賣</param>
        /// <param name="price">價格</param>
        public void Order(Side side, decimal price)
        {
            if (!_CanOrder(price)) { return; }


            SinoPacORD order = new SinoPacORD();
            order.ClOrdID = _ClOrdID();
            order.Account = Utility.Account;
            order.Exchange = ProductCollection.Selected.ExchangeID;
            order.Symbol = ProductCollection.Selected.ProductID;
            order.Side = side;
            order.Qty = Utility.Qty;
            order.OrderType = OrderType.Limit;
            order.Price = (double)price;
            order.Currency = ProductCollection.Selected.Currency;
            order.TimeInForce = TimeInForce.ROD;
            if (Utility.OrderAlert)
            {
                //下單前詢問
                if (!AlertBox.Alert(null, AlertBoxButton.OKCancel, "下單",
                    new MsgLine("ClOrdID", order.ClOrdID),
                    new MsgLine("Exch.", order.Exchange),
                    new MsgLine("Symbol", order.Symbol),
                    new MsgLine("Side", order.Side),
                    new MsgLine("Qty", order.Qty),
                    new MsgLine("Price", order.Price)))
                { return; }
            }
            //下單
            Utility.Processor.Order(order);
            FIXOrder fixorder = new FIXOrder(order);
            m_Orders.Add(fixorder);
        }

        /// <summary>
        /// 刪單
        /// </summary>
        /// <param name="side">買賣</param>
        /// <param name="price">價格</param>
        public void Cancel(Side side, decimal price)
        {
            if (!_CanCancel()) { return; }

            var orders = from order in m_Orders
                         where order.ProductID == ProductCollection.Selected.ProductID &&
                               order.OrderPrice == price && order.Side == side
                         select order;
            if (orders != null && orders.Count() > 0)
            {
                foreach (FIXOrder fix in orders)
                {
                    Utility.Processor.Cancel(fix.CancelOrder(_ClOrdID()));
                }
            }
        }
        /// <summary>
        /// 刪範圍外的單
        /// </summary>
        /// <param name="side">買賣</param>
        /// <param name="max">上限</param>
        /// <param name="min">下限</param>
        public void Cancel(Side side, decimal max, decimal min)
        {
            if (!_CanCancel()) { return; }

            var orders = from order in m_Orders
                         where order.ProductID == ProductCollection.Selected.ProductID &&
                               (order.OrderPrice > max || order.OrderPrice < min) && order.isAlive
                         select order;
            if (orders != null && orders.Count() > 0)
            {
                foreach (FIXOrder fix in orders)
                {
                    Utility.Processor.Cancel(fix.CancelOrder(_ClOrdID()));
                }
            }
        }
        /// <summary>
        /// 刪買/賣單
        /// </summary>
        /// <param name="side">買賣</param>
        public void Cancel(Side side)
        {
            if (!_CanCancel()) { return; }

            var orders = from order in m_Orders
                         where order.ProductID == ProductCollection.Selected.ProductID && order.Side == side && order.isAlive
                         select order;
            if (orders != null && orders.Count() > 0)
            {
                foreach (FIXOrder fix in orders)
                {
                    Utility.Processor.Cancel(fix.CancelOrder(_ClOrdID()));
                }
            }
        }
        /// <summary>
        /// 刪指定單
        /// </summary>
        /// <param name="clordid">指定的ClOrdID</param>
        public void Cancel(string clordid)
        {
            if (!_CanCancel()) { return; }

            foreach (var item in m_Orders)
            {
                if (item.isMyClOrdID(clordid))
                {
                    Utility.Processor.Cancel(item.CancelOrder(_ClOrdID()));
                    break;
                }
            }
        }
        /// <summary>
        /// 刪所有的單
        /// </summary>
        public void Cancel()
        {
            if (!_CanCancel()) { return; }

            var orders = from order in m_Orders
                         where order.ProductID == ProductCollection.Selected.ProductID && order.isAlive
                         select order;
            if (orders != null && orders.Count() > 0)
            {
                foreach (FIXOrder fix in orders)
                {
                    Utility.Processor.Cancel(fix.CancelOrder(_ClOrdID()));
                }
            }
        }
        #endregion

        #region Status
        public double OrderStatus(Side side)
        {
            if (m_Orders.Count == 0) { return 0; }
            return (from order in m_Orders
                    where order.ProductID == ProductCollection.Selected.ProductID && order.Side == side
                    select order.LeavesQty).Sum();
        }
        /// <summary>
        /// 統計未成交股數
        /// </summary>
        /// <param name="side">買賣</param>
        /// <param name="price">價格</param>
        /// <returns></returns>
        public string OrderStatus(Side side, decimal price)
        {
            if (m_Orders.Count == 0) { return string.Empty; }
            var status = from order in m_Orders
                         where order.ProductID == ProductCollection.Selected.ProductID &&
                               order.OrderPrice == price && order.Side == side
                         select order.LeavesQty;
            double sum = status.Sum();
            return sum == 0 ? string.Empty : sum.ToString();
        }
        /// <summary>
        /// 統計範圍外的未成交股數
        /// </summary>
        /// <param name="side">買賣</param>
        /// <param name="max">上限</param>
        /// <param name="min">下限</param>
        /// <returns></returns>
        public string OrderStatus(Side side, decimal max, decimal min)
        {
            if (m_Orders.Count == 0) { return string.Empty; }
            var status = from order in m_Orders
                         where order.ProductID == ProductCollection.Selected.ProductID &&
                               (order.OrderPrice > max || order.OrderPrice < min) && order.Side == side
                         select order.LeavesQty;
            double sum = status.Sum();
            return sum == 0 ? string.Empty : sum.ToString();
        }
        #endregion

        #region Private
        /// <summary>
        /// ClOrdID編號
        /// 換日編號會重編
        /// </summary>
        /// <returns></returns>
        private string _ClOrdID()
        {
            return string.Format("{0,-2}{1,-4}{2:0000}", "QO", DateTime.Now.ToString("MMdd"), Utility.Processor.GetSeqno());
        }
        /// <summary>
        /// 判斷是否能下單
        /// </summary>
        /// <returns></returns>
        private bool _CanOrder(decimal price)
        {
            if (!isConnect) { return false; }
            if (Utility.StopOrder)
            {
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單", "已勾選【禁止下單】！");
                return false;
            }
            if (price == 0)
            {
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單",
                                    new MsgLine("下單價格錯誤"),
                                    new MsgLine("委託價 = 0，不下單！"));
                return false;
            }
            //股數限制
            int limit = (Utility.LotsLimit != 0 && Utility.AutoLotsLimit != 0 ? Math.Min(Utility.LotsLimit, Utility.AutoLotsLimit) : Math.Max(Utility.LotsLimit, Utility.AutoLotsLimit));
            if (Utility.Qty > limit)
            {
                AlertBox.AlertWithoutReply(null, AlertBoxButton.Error_OK, "下單",
                    new MsgLine("下單數量錯誤"),
                    new MsgLine("股數[" + Utility.Qty + "] > 股數上限[" + limit + "]"));
                return false;
            }
            return true;
        }
        private bool _CanCancel()
        {
            if (!isConnect) { return false; }
            if (m_Orders.Count <= 0) { return false; }
            return true;
        }
        #endregion
    }
}