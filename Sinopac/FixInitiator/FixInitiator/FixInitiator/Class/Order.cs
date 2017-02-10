using System;
using System.Collections.Generic;
using System.Text;

namespace FixInitiator.Class
{
    class Order  //委託回報
    {
        public string systemID { get; private set; }
        public DateTime tradeDate { get; private set; }
        public int seqNo { get; private set; }
        public string symbol { get; private set; }
        public string account { get; private set; }
        public int orderQty { get; private set; }
        public int cumQty { get; private set; }
        public string execID { get; private set; }
        public char execTransType { get; private set; }
        public double lastPx { get; set; }
        public int lastShares { get; set; }
        public char side { get; private set; }
        public char execType { get; private set; }
        public int leaveQty { get; set; }
        public double avgPx { get; set; }
        public string clOrdID { get; private set; }


        public Order(string systemID, DateTime tradeDate, int seqNo, string symbol, string account, int orderQty, int cumQty, string execID, char execTransType, double lastPx, int lastShares, char side, char execType, int leaveQty, double avgPx, string clOrdID)
        {
            this.systemID = systemID ;
            this.tradeDate= tradeDate ;
            this.seqNo = seqNo ;
            this.symbol = symbol;
            this.account = account ;
            this.orderQty = orderQty ;
            this.cumQty = cumQty ;
            this.execID =execID ;
            this.execTransType = execTransType;
            this.lastPx = lastPx ;
            this.lastShares = lastShares ;
            this.side = side ;
            this.execType = execType;
            this.leaveQty = leaveQty ;
            this.avgPx = avgPx ;
            this.clOrdID = clOrdID;

        }

    }
}
