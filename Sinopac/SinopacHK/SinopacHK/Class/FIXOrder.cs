using System;
using System.Collections.Generic;
using System.Linq;
using OrderProcessor;
using OrderProcessor.SinoPac;

namespace SinopacHK.Class
{
    /// <summary>
    /// 下單/回報/成交 資料
    /// </summary>
    public class FIXOrder
    {
        #region Variable
        /// <summary>
        /// 下單資料
        /// </summary>
        private SinoPacORD m_Order;
        /// <summary>
        /// 刪單資料
        /// </summary>
        private SinoPacORD m_Cancel;
        /// <summary>
        /// 改單資料
        /// </summary>
        private List<SinoPacORD> m_Replace;
        /// <summary>
        /// 回報資料(含改單)
        /// </summary>
        private List<SinoPacRPT> m_Rpts = new List<SinoPacRPT>();
        /// <summary>
        /// 成交資料
        /// </summary>
        private List<SinoPacRPT> m_Matchs = new List<SinoPacRPT>();
        #endregion

        #region Property
        /// <summary>
        /// 商品
        /// </summary>
        public string ProductID { get { return m_Order.Symbol; } }
        /// <summary>
        /// 委託書號
        /// </summary>
        public string Ordno { get; private set; }
        /// <summary>
        /// 最後有效ClOrdID
        /// </summary>
        public string LastVaildClOrdID
        {
            get
            {
                if (m_Rpts.Count == 0)
                {
                    //沒有回報, 回傳下單資料的ClOrdID(ClOrdID與成交無關)
                    return m_Order.ClOrdID;
                }
                else
                {
                    //找有效回報
                    var rpts = m_Rpts.Where(e =>
                        e.CxlRejReason == OrderProcessor.CxlRejReason.None &&
                        (e.ExecType == OrderProcessor.ExecType.New ||
                        e.ExecType == OrderProcessor.ExecType.Canceled ||
                        e.ExecType == OrderProcessor.ExecType.Replaced));
                    if (rpts != null && rpts.Count() > 0)
                    {
                        //取最新一個有效回報
                        return rpts.OrderBy(e => e.SendingTime).Last().ClOrdID;
                    }
                    else
                    {
                        //沒有有效回報, 取下單回報
                        return m_Order.ClOrdID;
                    }
                }
            }
        }
        /// <summary>
        /// 委託狀態
        /// </summary>
        public OrderStatus OrderStatus
        {
            get
            {
                if (m_Rpts.Count == 0 && m_Matchs.Count == 0)
                {
                    //沒回報也沒成交
                    return OrderProcessor.OrderStatus.None;
                }
                else
                {
                    //取出回報+成交內 最新的委託狀態
                    return m_Rpts.Union(m_Matchs).OrderBy(e => e.SendingTime).Last().OrderStatus;
                }
            }
        }
        /// <summary>
        /// 成交股數
        /// </summary>
        public double MatchQty { get { return m_Matchs.Sum(e => e.LastQty); } }
        /// <summary>
        /// 未成交股數
        /// </summary>
        public double LeavesQty
        {
            get
            {
                if (m_Rpts.Count == 0 && m_Matchs.Count == 0)
                {
                    //沒委成回
                    return 0;
                }
                else
                {
                    if (m_Rpts.FirstOrDefault(e => e.ExecType == OrderProcessor.ExecType.Canceled) != null)
                    {
                        //刪單了
                        return 0;
                    }
                    if (m_Rpts.FirstOrDefault(e => e.ExecType == OrderProcessor.ExecType.PendingNew && e.OrderStatus == OrderProcessor.OrderStatus.Rejected) != null)
                    {
                        //新單被拒
                        return 0;
                    }
                    //取委回+成回內的 未成交股數
                    return m_Rpts.Union(m_Matchs).OrderBy(e => e.SendingTime).Last().LeavesQty;
                }
            }
        }
        /// <summary>
        /// 是否在途
        /// </summary>
        public bool isAlive { get { return LeavesQty > 0; } }
        /// <summary>
        /// 委託價
        /// </summary>
        public decimal OrderPrice { get { return (decimal)m_Order.Price; } }
        /// <summary>
        /// 買賣邊
        /// </summary>
        public OrderProcessor.Side Side { get { return m_Order.Side; } }
        /// <summary>
        /// 成交均價
        /// </summary>
        public double AvgPrice { get { return m_Matchs.Sum(e => e.LastPx * e.LastQty) / MatchQty; } }
        #endregion

        public FIXOrder(string clordid, string account, string exch, string symbol, OrderProcessor.Side side, int qty, OrderType ordType, double price, string currency, OrderProcessor.TimeInForce timeinforce)
        {
            m_Order = new SinoPacORD();
            m_Order.Action = ActionType.New;
            m_Order.ClOrdID = clordid;
            m_Order.Account = account;
            m_Order.Exchange = exch;
            m_Order.Symbol = symbol;
            m_Order.Side = side;
            m_Order.Qty = qty;
            m_Order.OrderType = ordType;
            m_Order.Price = price;
            m_Order.Currency = currency;
            m_Order.TimeInForce = timeinforce;
        }
        public FIXOrder(SinoPacORD ord) : this(ord.ClOrdID, ord.Account, ord.Exchange, ord.Symbol, ord.Side, ord.Qty, ord.OrderType, ord.Price, ord.Currency, ord.TimeInForce) { }

        #region Cancel /Replace
        /// <summary>
        /// 取消委託
        /// </summary>
        /// <param name="clordid"></param>
        /// <returns></returns>
        public SinoPacORD CancelOrder(string clordid)
        {
            m_Cancel = new SinoPacORD();
            m_Cancel.Action = ActionType.Cancel;
            m_Cancel.OrigClOrdID = LastVaildClOrdID;
            m_Cancel.ClOrdID = clordid;
            m_Cancel.Account = m_Order.Account;
            m_Cancel.Symbol = m_Order.Symbol;
            m_Cancel.Side = m_Order.Side;
            m_Cancel.Exchange = m_Order.Exchange;
            return m_Cancel;
        }
        /// <summary>
        /// 改量
        /// </summary>
        /// <param name="clordid"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public SinoPacORD ReplaceOrder(string clordid, int qty)
        {
            if (m_Replace == null) { m_Replace = new List<SinoPacORD>(); }
            SinoPacORD replace = new SinoPacORD();
            replace.Action = ActionType.Replace;
            replace.OrigClOrdID = LastVaildClOrdID;
            replace.ClOrdID = clordid;
            replace.Account = m_Order.Account;
            replace.Symbol = m_Order.Symbol;
            replace.Side = m_Order.Side;
            replace.Qty = qty;
            replace.OrderType = m_Order.OrderType;
            replace.Exchange = m_Order.Exchange;
            m_Replace.Add(replace);
            return replace;
        }
        #endregion

        #region RPT / MAT
        /// <summary>
        /// 新增委回
        /// </summary>
        /// <param name="rpt"></param>
        public void AddRPT(SinoPacRPT rpt)
        {
            if (m_Rpts == null) { m_Rpts = new List<SinoPacRPT>(); }
            if (rpt.ClOrdID == m_Order.ClOrdID || rpt.ClOrdID == m_Cancel.ClOrdID ||
                (m_Replace.Count(e => e.ClOrdID == rpt.ClOrdID) > 0))
            {
                if (String.IsNullOrEmpty(Ordno)) { Ordno = rpt.OrderID; }
                m_Rpts.Add(rpt);
            }
        }
        /// <summary>
        /// 新增成回
        /// </summary>
        /// <param name="mat"></param>
        public void AddMAT(SinoPacRPT mat)
        {
            if (mat.ClOrdID == m_Order.ClOrdID ||
                (m_Replace.Count(e => e.ClOrdID == mat.ClOrdID) > 0))
            {
                if (m_Matchs == null) { m_Matchs = new List<SinoPacRPT>(); }
                m_Matchs.Add(mat);
            }
        }
        #endregion

        /// <summary>
        /// 判斷是不是相關的ClOrdID
        /// </summary>
        /// <param name="clordid"></param>
        /// <returns></returns>
        public bool isMyClOrdID(string clordid)
        {
            if (m_Order.ClOrdID == clordid ||
                (m_Cancel != null && m_Cancel.ClOrdID == clordid) ||
                (m_Replace != null && m_Replace.Count(e => e.ClOrdID == clordid) > 0))
            {
                return true;
            }
            return false;
        }
    }
}