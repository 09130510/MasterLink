
namespace OrderProcessor
{
    public enum Side
    {
        [Value4FIX('1'), Value4Capital('0')]
        B,
        [Value4FIX('2'), Value4Capital('1')]
        S
    }
    public enum ActionType
    {
        None,
        New,
        Cancel,
        Replace
    }
    public enum OrderType
    {
        [Value4FIX('1'), Value4Capital('1')]
        Market,
        [Value4FIX('2'), Value4Capital('0')]
        Limit,
        [Value4FIX('3'), Value4Capital('3')]
        Stop,
        [Value4FIX('4'), Value4Capital('2')]
        StopLimit,
        [Value4FIX('5')]
        MarketOnClose
    }
    public enum TimeInForce
    {
        [Value4FIX('0'), Value4Capital('0')]
        ROD,
        [Value4FIX('3'), Value4Capital('1')]
        IOC,
        [Value4FIX('4'), Value4Capital('2')]
        FOK
    }
    public enum OrderStatus
    {
        [Value4FIX(default(char))]
        None,
        [Value4FIX('0')]
        New,
        [Value4FIX('1')]
        PartiallyFilled,
        [Value4FIX('2')]
        Filled,
        [Value4FIX('3')]
        DoneForDay,
        [Value4FIX('4')]
        Canceled,
        [Value4FIX('5')]
        Replaced,
        [Value4FIX('6')]
        PendingCancel,
        [Value4FIX('7')]
        Stopped,
        [Value4FIX('8')]
        Rejected,
        [Value4FIX('9')]
        Suspended,
        [Value4FIX('A')]
        PendingNew,
        [Value4FIX('B')]
        Calculated,
        [Value4FIX('C')]
        Expired,
        [Value4FIX('D')]
        AcceptedForBidding,
        [Value4FIX('E')]
        PendingReplace
    }
    public enum ExecType
    {
        [Value4FIX('0')]
        New,
        [Value4FIX('1')]
        PartialFill,
        [Value4FIX('2')]
        Fill,
        [Value4FIX('3')]
        DoneForDay,
        [Value4FIX('4')]
        Canceled,
        [Value4FIX('5')]
        Replaced,
        [Value4FIX('6')]
        PendingCancel,
        [Value4FIX('7')]
        Stopped,
        [Value4FIX('8')]
        Rejected,
        [Value4FIX('9')]
        Suspended,
        [Value4FIX('A')]
        PendingNew,
        [Value4FIX('B')]
        Calculated,
        [Value4FIX('C')]
        Expired,
        [Value4FIX('D')]
        Restated,
        [Value4FIX('E')]
        PendingReplace
    }
    public enum CxlRejResponseTo
    {
        None,
        [Value4FIX('1')]
        OrderCancelRequest,
        [Value4FIX('2')]
        OrderCancelReplaceRequect
    }
    public enum CxlRejReason
    {
        None = -1,
        [Value4FIX('0')]
        TooLateToCancel = 0,
        [Value4FIX('1')]
        UnknownOrder = 1,
        [Value4FIX('2')]
        BrokerOption = 2,
        [Value4FIX('3')]
        OrderAlreadyInPendingCancelorPendingReplaceStatus = 3
    }
}
