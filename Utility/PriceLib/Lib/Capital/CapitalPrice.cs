using SKCOMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceLib.Capital
{
    public partial class OSCapitalLib
    {
        private class CapitalPrice
        {
            #region Property
            public short StkIdx { get; private set; }
            public string Exch { get; set; }
            public string ID { get; set; }
            public string Key { get { return Exch + "," + ID; } }
            public int Divisor { get; set; }
            public int YP { get; set; }
            public int MP { get; set; }
            public int AP { get; set; }
            public int AQ { get; set; }
            public int BP { get; set; }
            public int BQ { get; set; }
            #endregion

            public CapitalPrice(short stockIdx)
            {
                StkIdx = stockIdx;
                YP = -1;
                MP = -1;
                AP = -1;
                AQ = -1;
                BP = -1;
                BQ = -1;
            }
            public CapitalPrice(short stockIdx, SKFOREIGN foreign)
                : this(stockIdx)
            {
                Update(stockIdx, foreign);
            }
            public CapitalPrice(short stockIdx, SKTICK tick)
                : this(stockIdx)
            {
                Update(stockIdx, tick);
            }

            #region Public
            public MktPrice GetMktPrice()
            {
                return new MktPrice(Key, YP, MP, AP, BP, AQ, BQ, Divisor);
            }
            public void Update(short stockIdx, SKFOREIGN foreign)
            {
                if (stockIdx != StkIdx) { return; }
                ID = foreign.bstrStockNo.Trim();
                Exch = foreign.bstrExchangeNo.Trim();
                Divisor = (int)Math.Pow(10, foreign.sDecimal);
                YP = foreign.nRef;
                MP = foreign.nClose;
                AP = foreign.nAsk;
                AQ = foreign.nAc;
                BP = foreign.nBid;
                BQ = foreign.nBc;
            }
            public void Update(short stockIdx, SKTICK tick)
            {
                if (stockIdx != StkIdx) { return; }
                MP = tick.nClose;
            }
            #endregion
        }
    }
}
