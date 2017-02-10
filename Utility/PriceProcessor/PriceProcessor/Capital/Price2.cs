//using SKCOMLib;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace PriceProcessor.Capital
//{
//    public class Price2
//    {
//        //public struct PriceDOM
//        //{
//        //    public PriceDOM()
//        //    {
//        //    }
//        //}     
//        public struct PQ
//        {
//            public double Price;
//            public int Qty;
//        }

//        #region Variable
//        private CapitalProcessor m_Processor;
//        private double[] m_BestBid;
//        private double[] m_BestAsk;
//        private int[] m_BestBQty;
//        private int[] m_BestAQty;
//        #endregion

//        #region Property
//        public short StockIdx { get; private set; }
//        public string StockNo { get; private set; }
//        public string StockName { get; private set; }
//        public string MarketNo { get; private set; }
//        public string ExchangeNo { get; private set; }
//        public string ExchangeName { get; private set; }
//        public short Decimal { get; private set; }
//        public int Denominator { get; private set; }

//        public double Ref { get; private set; }
//        public double Open { get; private set; }
//        public double High { get; private set; }
//        public double Low { get; private set; }
//        public double Close { get; private set; }
//        public double SettlePrice { get; private set; }
//        public double Market { get; private set; }
//        public double Bid { get; private set; }
//        public double Ask { get; private set; }
//        public int MarketQty { get; private set; }
//        public int BQty { get; private set; }
//        public int AQty { get; private set; }
//        public int TickQty { get; private set; }
//        public int TotalQty { get; private set; }

//        private double[] BestBid
//        {
//            get
//            {
//                if (m_BestBid == null)
//                {
//                    m_BestBid = new double[5];
//                }
//                return m_BestBid;
//            }
//            set { m_BestBid = value; }
//        }
//        private double[] BestAsk
//        {
//            get
//            {
//                if (m_BestAsk == null)
//                {
//                    m_BestAsk = new double[5];
//                }
//                return m_BestAsk;
//            }
//            set { m_BestAsk = value; }
//        }
//        private int[] BestBQty
//        {
//            get
//            {
//                if (m_BestBQty == null) { m_BestBQty = new int[5]; }
//                return m_BestBQty;
//            }
//            set { m_BestBQty = value; }
//        }
//        private int[] BestAQty
//        {
//            get
//            {
//                if (m_BestAQty == null)
//                {
//                    m_BestAQty = new int[5];
//                }
//                return m_BestAQty;
//            }
//            set { m_BestAQty = value; }
//        }
//        #endregion

//        public Price2(CapitalProcessor processor)
//        {
//            m_Processor = processor;
//        }
//        public Price2(CapitalProcessor processor, short stockIdx, SKFOREIGN foreign)
//            : this(processor)
//        {
//            StockIdx = stockIdx;
//            Update(stockIdx, foreign);
//        }
//        public Price2(CapitalProcessor processor, short stockIdx, int[] bid, int[] ask, int[] bidqty, int[] askqty)
//            : this(processor)
//        {
//            StockIdx = stockIdx;
//            Update(stockIdx, bid, ask, bidqty, askqty);
//        }
//        public Price2(CapitalProcessor processor, short stockIdx, SKFOREIGNTICK tick)
//            : this(processor)
//        {
//            StockIdx = stockIdx;
//            Update(stockIdx, tick);
//        }

//        #region Public
//        public void Update(short stockIdx, SKFOREIGN foreign)
//        {
//            if (stockIdx != StockIdx) { return; }
//            StockIdx = foreign.sStockIdx;
//            StockNo = foreign.bstrStockNo.Trim();
//            StockName = foreign.bstrStockName.Trim();
//            MarketNo = foreign.bstrMarketNo.Trim();
//            ExchangeNo = foreign.bstrExchangeNo.Trim();
//            ExchangeName = foreign.bstrExchangeName.Trim();
//            Decimal = foreign.sDecimal;
//            Denominator = foreign.nDenominator;

//            Ref = foreign.nRef / (Math.Pow(10D, Decimal));
//            Open = foreign.nOpen / (Math.Pow(10D, Decimal));
//            High = foreign.nHigh / (Math.Pow(10D, Decimal));
//            Low = foreign.nLow / (Math.Pow(10D, Decimal));
//            Close = foreign.nClose / (Math.Pow(10D, Decimal));
//            SettlePrice = foreign.nSettlePrice / (Math.Pow(10D, Decimal));
//            Bid = foreign.nBid / (Math.Pow(10D, Decimal));
//            Ask = foreign.nAsk / (Math.Pow(10D, Decimal));
//            BQty = foreign.nBc;
//            AQty = foreign.nAc;
//            TickQty = foreign.nTickQty;
//            TotalQty = foreign.nTQty;
//        }
//        public void Update(short stockIdx, int[] bid, int[] ask, int[] bidqty, int[] askqty)
//        {
//            if (stockIdx != StockIdx) { return; }
//            for (int i = 0; i < 5; i++)
//            {
//                BestBQty[i] = bidqty[i];
//                BestBid[i] = Convert.ToDouble(bid[i]) / (Math.Pow(10D, Decimal));
//                BestAQty[4 - i] = askqty[i];
//                BestAsk[4 - i] = Convert.ToDouble(ask[i]) / (Math.Pow(10D, Decimal));
//            }
//        }
//        public void Update(short stockIdx, SKFOREIGNTICK tick)
//        {
//            if (stockIdx != StockIdx) { return; }
//            Market = tick.nClose / (Math.Pow(10D, Decimal));
//            MarketQty = tick.nQty;
//        }

//        public List<PQ> BestBidByTickCount(string tickname, int tickCount)
//        {
//            List<PQ> re = new List<PQ>();
//            int baseIdx = 0;
//            for (int i = 0; i < tickCount; i++)
//            {
//                if (i < BestBid.Length && BestBid[i] != 0D)
//                {
//                    re.Add(new PQ()
//                    {
//                        Price = BestBid[i],
//                        Qty = BestBQty[i]
//                    });
//                    baseIdx = i;
//                }
//                else
//                {
//                    re.Add(new PQ()
//                    {
//                        Price = (double)m_Processor.AddnTick(tickname, (decimal)BestBid[baseIdx], (i - baseIdx) * -1),
//                        Qty = 0
//                    });
//                }
//            }
//            return re;
//        }
//        public List<PQ> BestAskByTickCount(string tickname, int tickCount)
//        {
//            List<PQ> re = new List<PQ>();
//            int baseIdx = BestAsk.Length - 1;
//            for (int i = 0; i < tickCount; i++)
//            {
//                if (i < BestAsk.Length && BestAsk[(BestAsk.Length - 1) - i] != 0D)
//                {
//                    re.Add(new PQ()
//                    {
//                        Price = BestAsk[(BestAsk.Length - 1) - i],
//                        Qty = BestAQty[(BestAQty.Length - 1) - i]
//                    });
//                    baseIdx = (BestAsk.Length - 1) - i;
//                }
//                else
//                {
//                    re.Add(new PQ()
//                    {
//                        Price = (double)m_Processor.AddnTick(tickname, (decimal)BestAsk[baseIdx], (i - (BestAsk.Length - 1 - baseIdx))),
//                        Qty = 0
//                    });
//                }
//            }
//            re.Reverse();
//            return re;
//        }
//        public List<PQ> ExtendBidByTickCount(string tickname, int tickCount)
//        {
//            List<PQ> re = new List<PQ>();
//            bool isUsingBP = Market == 0D || Market <= BestBid[0];
//            decimal initPrice;
//            if (isUsingBP)
//            {
//                initPrice = (decimal)BestBid[0];
//            }
//            else
//            {
//                initPrice = (decimal)Market;
//            }
//            int plus = isUsingBP ? 0 : 1;
//            for (int i = 0; i < tickCount; i++)
//            {
//                double price = (double)m_Processor.AddnTick(tickname, initPrice, (i + plus) * -1);
//                int qtyIdx = -1;

//                for (int idx = 0; idx < BestBid.Length; idx++)
//                {
//                    if (BestBid[idx] == price)
//                    {
//                        qtyIdx = idx;
//                        break;
//                    }
//                }
//                int qty = qtyIdx == -1 ? 0 : BestBQty[qtyIdx];
//                re.Add(new PQ() { Price = price, Qty = qty });
//            }
//            return re;
//        }
//        public List<PQ> ExtendAskByTickCount(string tickname, int tickCount)
//        {
//            List<PQ> re = new List<PQ>();
//            bool isUsingAP = Market == 0D || (BestAsk[4] != 0D && Market >= BestAsk[4]);
//            decimal initPrice;
//            if (isUsingAP)
//            {
//                initPrice = (decimal)BestAsk[4];
//            }
//            else
//            {
//                initPrice = (decimal)Market;
//            }
//            int plus = isUsingAP ? 0 : 1;
//            for (int i = tickCount - 1; i >= 0; i--)
//            {
//                double price = (double)m_Processor.AddnTick(tickname, initPrice, i + plus);
//                int qtyIdx = -1;

//                for (int idx = 0; idx < BestAsk.Length; idx++)
//                {
//                    if (BestAsk[idx] == price)
//                    {
//                        qtyIdx = idx;
//                        break;
//                    }
//                }
//                int qty = qtyIdx == -1 ? 0 : BestAQty[qtyIdx];
//                re.Add(new PQ() { Price = price, Qty = qty });
//            }
//            return re;
//        }
//        #endregion

//        public new string ToString()
//        {
//            return $"{ExchangeNo}, {StockNo}, REF:{Ref}, MP:{Market}, ML:{TickQty}, BP:{Bid}, BL:{BQty}, AP:{Ask} AL:{AQty}, TOTAL:{TotalQty} ";
//        }
//        public static decimal MainNumber(decimal number)
//        {
//            return Math.Truncate(number);
//        }
//        public static decimal SubNumber(decimal number)
//        {
//            return number - MainNumber(number);
//        }
//        public static double MainNumber(double number)
//        {
//            return Math.Truncate(number);
//        }
//        public static double SubNumber(double number)
//        {
//            return number - MainNumber(number);
//        }
//    }
//}
