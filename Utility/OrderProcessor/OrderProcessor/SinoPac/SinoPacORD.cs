using System;
using System.Collections.Generic;
using QuickFix;
using QuickFix42;

namespace OrderProcessor.SinoPac
{
    public class SinoPacORD
    {
        public ActionType Action { get; set; }

        public string ClOrdID { get; set; }
        public string OrigClOrdID { get; set; }
        public string Account { get; set; }

        public string Exchange { get; set; }
        public string Symbol { get; set; }

        public Side Side { get; set; }
        public int Qty { get; set; }
        public OrderType OrderType { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public TimeInForce TimeInForce { get; set; }

        public NewOrderSingle Order
        {
            get
            {
                NewOrderSingle order = new NewOrderSingle();
                List<Field> fields = new List<Field>();
                order.set(new QuickFix.ClOrdID(ClOrdID));
                order.set(new QuickFix.Account(Account));
                order.set(new QuickFix.HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE));
                order.set(new QuickFix.ExDestination(Exchange));
                order.set(new QuickFix.SecurityExchange(Exchange));
                order.set(new QuickFix.Symbol(Symbol));
                order.set(new QuickFix.IDSource("5"));
                order.set(new QuickFix.Side(Side.FIX()));
                order.set(new QuickFix.OrderQty(Qty));
                order.set(new QuickFix.OrdType(OrderType.FIX()));
                order.set(new QuickFix.Price(Price));
                order.set(new QuickFix.Currency(Currency));
                order.set(new QuickFix.Rule80A(Rule80A.AGENCY_SINGLE_ORDER));
                order.set(new QuickFix.PegDifference('0'));
                order.set(new QuickFix.TimeInForce(TimeInForce.FIX()));
                order.set(new QuickFix.TransactTime(DateTime.Now));
                return order;
            }
        }
        public OrderCancelRequest Cancel
        {
            get
            {
                OrderCancelRequest cancel = new OrderCancelRequest();
                cancel.set(new QuickFix.OrigClOrdID(OrigClOrdID));
                cancel.set(new QuickFix.ClOrdID(ClOrdID));
                cancel.set(new QuickFix.Account(Account));
                cancel.set(new QuickFix.Symbol(Symbol));
                cancel.set(new QuickFix.Side(Side.FIX()));
                cancel.set(new QuickFix.SecurityExchange(Exchange));
                cancel.set(new QuickFix.IDSource("5"));
                cancel.set(new QuickFix.TransactTime(DateTime.Now));
                return cancel;
            }
        }
        public OrderCancelReplaceRequest Replace
        {
            get
            {
                OrderCancelReplaceRequest replace = new OrderCancelReplaceRequest();
                replace.set(new QuickFix.OrigClOrdID(OrigClOrdID));
                replace.set(new QuickFix.ClOrdID(ClOrdID));
                replace.set(new QuickFix.HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE));
                replace.set(new QuickFix.Account(Account));
                replace.set(new QuickFix.Symbol(Symbol));
                replace.set(new QuickFix.Side(Side.FIX()));
                replace.set(new QuickFix.OrderQty(Qty));
                replace.set(new QuickFix.OrdType(OrderType.FIX()));
                replace.set(new QuickFix.SecurityExchange(Exchange));
                replace.set(new QuickFix.IDSource("5"));
                replace.set(new QuickFix.TransactTime(DateTime.Now));
                return replace;
            }
        }

        public void DoAction(SinoPacProcessor processor)
        {
            if (Action == ActionType.None) { return; }
            switch (Action)
            {
                case ActionType.New:
                    processor.Order(this);
                    break;
                case ActionType.Cancel:
                    processor.Cancel(this);
                    break;
                case ActionType.Replace:
                    processor.Replace(this);
                    break;
            }
        }
    }
}