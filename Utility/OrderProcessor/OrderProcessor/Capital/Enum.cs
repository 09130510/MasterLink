using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderProcessor.Capital
{
    public enum MarketType
    {
        /// <summary>
        /// 證券
        /// </summary>
        TS,
        /// <summary>
        /// 盤後
        /// </summary>
        TA,
        /// <summary>
        /// 零股
        /// </summary>
        TL,
        /// <summary>
        /// 期貨
        /// </summary>
        TF,
        /// <summary>
        /// 選擇權
        /// </summary>
        TO,
        /// <summary>
        /// 海外期貨
        /// </summary>
        OF,
        /// <summary>
        /// 海外選擇權
        /// </summary>
        OO
    }
    public enum OrderType
    {
        /// <summary>
        /// 委託
        /// </summary>
        N,
        /// <summary>
        /// 取消
        /// </summary>
        C,
        /// <summary>
        /// 改量
        /// </summary>
        U,
        /// <summary>
        /// 成交
        /// </summary>
        D,
        /// <summary>
        /// 報價
        /// </summary>
        Q,
        /// <summary>
        /// 改價
        /// </summary>
        P
    }
    public enum OrderErr
    {
        /// <summary>
        /// 失敗
        /// </summary>
        Y,
        /// <summary>
        /// 逾時
        /// </summary>
        T,
        /// <summary>
        /// 正常
        /// </summary>
        N
    }
    //用Side替代
    //public enum BuySell
    //{
    //    /// <summary>
    //    /// 買
    //    /// </summary>
    //    B,
    //    /// <summary>
    //    /// 賣
    //    /// </summary>
    //    S
    //}
    public enum PriceType
    {
        /// <summary>
        /// 市價
        /// </summary>
        Market = 1,
        /// <summary>
        /// 限價
        /// </summary>
        Limit = 2,
        /// <summary>
        /// 停損
        /// </summary>
        Stop = 3,
        /// <summary>
        /// 停損限價
        /// </summary>
        StopLimit = 4,
        /// <summary>
        /// 收市
        /// </summary>
        CloseMarket = 5
    }
    public enum ReplyType
    {
        OrderReply,
        MatchReply
    }
    public enum ListType
    {
        Vaild = 1,
        Cancel = 2,
        Error = 4,
        Match = 8,
        Order = 16,
        Summary = 32
    }
    public enum TransactionStatus
    {
        Order,
        Cancel,
        Filled,
        PartialFill,
        PartialFillCancel
    }
}