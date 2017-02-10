using System;
using QuickFix42;

namespace OrderProcessor.SinoPac
{
    public class SinoPacRPT
    {
        private CxlRejReason m_CxlRejReason = CxlRejReason.None;
        private CxlRejResponseTo m_CxlRejResponseTo = CxlRejResponseTo.None;

        public int MsgSeqNo { get; private set; }
        public string SendingTime { get; private set; }
        public string Account { get; private set; }
        public double AvgPx { get; private set; }
        public string ClOrdID { get; private set; }
        public double CumQty { get; private set; }
        public string Currency { get; private set; }
        public double LastPx { get; private set; }
        public double LastQty { get; private set; }
        public string OrderID { get; private set; }
        public double Qty { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
        public OrderType OrderType { get; private set; }
        public string OrigClOrdID { get; private set; }
        public double Price { get; private set; }
        public Side Side { get; private set; }
        public string Symbol { get; private set; }
        public string Msg { get; private set; }
        public DateTime TransactTime { get; private set; }
        public ExecType ExecType { get; private set; }
        public double LeavesQty { get; private set; }
        public string Exchange { get; private set; }
        public CxlRejResponseTo CxlRejResponseTo
        {
            get { return m_CxlRejResponseTo; }
            private set { m_CxlRejResponseTo = value; }
        }
        public CxlRejReason CxlRejReason
        {
            get { return m_CxlRejReason; }
            private set { m_CxlRejReason = value; }
        }

        public SinoPacRPT(ExecutionReport rpt)
        {
            Message.Header header = rpt.getHeader();
            MsgSeqNo = header.getInt(34);
            SendingTime = header.getString(52);

            if (rpt.isSetAccount()) { Account = rpt.getAccount().getValue(); }
            if (rpt.isSetAvgPx()) { AvgPx = rpt.getAvgPx().getValue(); }
            if (rpt.isSetClOrdID()) { ClOrdID = rpt.getClOrdID().getValue(); }
            if (rpt.isSetCumQty()) { CumQty = rpt.getCumQty().getValue(); }
            if (rpt.isSetCurrency()) { Currency = rpt.getCurrency().getValue(); }
            if (rpt.isSetLastPx()) { LastPx = rpt.getLastPx().getValue(); }
            if (rpt.isSetLastShares()) { LastQty = rpt.getLastShares().getValue(); }
            if (rpt.isSetOrderID()) { OrderID = rpt.getOrderID().getValue(); }
            if (rpt.isSetOrderQty()) { Qty = rpt.getOrderQty().getValue(); }
            if (rpt.isSetOrdStatus())
            { OrderStatus = rpt.getOrdStatus().getValue().ToEnumByFIX<OrderStatus>(); }
            if (rpt.isSetOrdType()) { OrderType = rpt.getOrdType().getValue().ToEnumByFIX<OrderType>(); }
            if (rpt.isSetOrigClOrdID()) { OrigClOrdID = rpt.getOrigClOrdID().getValue(); }
            if (rpt.isSetPrice()) { Price = rpt.getPrice().getValue(); }
            if (rpt.isSetSide()) { Side = rpt.getSide().getValue().ToEnumByFIX<Side>(); }
            if (rpt.isSetSymbol()) { Symbol = rpt.getSymbol().getValue(); }
            if (rpt.isSetText()) { Msg = rpt.getText().getValue(); }
            if (rpt.isSetTransactTime()) { TransactTime = rpt.getTransactTime().getValue(); }
            if (rpt.isSetExecType()) { ExecType = rpt.getExecType().getValue().ToEnumByFIX<ExecType>(); }
            if (rpt.isSetLeavesQty()) { LeavesQty = rpt.getLeavesQty().getValue(); }
            if (rpt.isSetSecurityExchange()) { Exchange = rpt.getSecurityExchange().getValue(); }
        }
        public SinoPacRPT(OrderCancelReject rpt)
        {
            Message.Header header = rpt.getHeader();
            MsgSeqNo = header.getInt(34);
            SendingTime = header.getString(52);

            if (rpt.isSetAccount()) { Account = rpt.getAccount().getValue(); }
            if (rpt.isSetClOrdID()) { ClOrdID = rpt.getClOrdID().getValue(); }
            if (rpt.isSetOrderID()) { OrderID = rpt.getOrderID().getValue(); }
            if (rpt.isSetOrdStatus())
            { OrderStatus = rpt.getOrdStatus().getValue().ToEnumByFIX<OrderStatus>(); }
            if (rpt.isSetOrigClOrdID()) { OrigClOrdID = rpt.getOrigClOrdID().getValue(); }
            if (rpt.isSetText()) { Msg = rpt.getText().getValue(); }
            if (rpt.isSetTransactTime()) { TransactTime = rpt.getTransactTime().getValue(); }
            if (rpt.isSetCxlRejResponseTo())
            { CxlRejResponseTo = rpt.getCxlRejResponseTo().getValue().ToEnumByFIX<CxlRejResponseTo>(); }
            if (rpt.isSetCxlRejReason())
            {
                CxlRejReason reason;
                Enum.TryParse<CxlRejReason>(rpt.getCxlRejReason().getValue().ToString(), out reason);
                CxlRejReason = reason;
            }
            else
            {
                CxlRejReason = OrderProcessor.CxlRejReason.None;
            }
        }
    }
}
