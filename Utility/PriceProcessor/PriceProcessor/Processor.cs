using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Util.Extension.Class;
using System.Data;

namespace PriceProcessor
{
    public abstract class Processor
    {
        #region Static
        protected static string PATH = @"./";
        protected static string NAME = "Config.ini";
        #endregion

        #region Variable
        //protected static Dictionary<string, Dictionary<decimal, decimal>> m_Tick;
        protected static Dictionary<string, Dictionary<decimal, TickInfo>> m_Tick;

        //protected static Dictionary<string, string> m_FutureTickName;
        protected string m_PID;
        #endregion

        public Processor()
        {
            InitTick();
        }

        #region Public
        public virtual decimal[] TickIncrement(string tickname, decimal price, int count)
        {
            if (count == 0) { return new decimal[0]; }
            decimal[] re = new decimal[count];
            decimal increment, decrement;
            Tick(tickname, price, out increment, out decrement);
            re[count - 1] = price + increment;
            for (int i = count - 2; i >= 0; i--)
            {
                Tick(tickname, re[i + 1], out increment, out decrement);
                re[i] = re[i + 1] + increment;
            }
            return re;
        }
        public virtual decimal[] TickDecrement(string tickname, decimal price, int count)
        {
            if (count == 0) { return new decimal[0]; }
            decimal[] re = new decimal[count];
            decimal increment, decrement;
            Tick(tickname, price, out increment, out decrement);
            re[0] = price - decrement;
            for (int i = 1; i < count; i++)
            {
                Tick(tickname, re[i - 1], out increment, out decrement);
                re[i] = re[i - 1] - decrement;
            }
            return re;
        }
        /// <summary>
        /// 買往下, 賣往上
        /// </summary>
        /// <param name="price"></param>
        /// <param name="side">0:買 1:賣</param>
        /// <returns></returns>
        public virtual decimal PriceUnit(string tickname, decimal price, int side)
        {
            decimal increment, decrement;
            Tick(tickname, price, out increment, out decrement);

            int quotient = (int)(price / increment);
            if (side == 0)
            {
                return quotient * price;
            }
            else if (side == 1)
            {
                return (quotient + 1) * price;
            }
            return price;
        }
        //public virtual void Tick(string tickname, decimal price, out decimal increment, out decimal decrement)
        //{
        //    increment = default(decimal);
        //    decrement = default(decimal);

        //    //if (price <= 0) { return; }
        //    if (!m_Tick.ContainsKey(tickname)) { return; }
        //    var fit_tick = m_Tick[tickname].FirstOrDefault(e => price <= e.Key).Value;
        //    var over_tick = m_Tick[tickname].FirstOrDefault(e => (price + fit_tick.PerTick) <= e.Key).Value;

        //    if (price < fit_tick.UpLimit)
        //    {
        //        increment = fit_tick.PerTick;
        //        decrement = fit_tick.PerTick;
        //    }
        //    else
        //    {
        //        increment = over_tick.PerTick;
        //        if (price == fit_tick.UpLimit)
        //        {
        //            decrement = fit_tick.PerTick;
        //        }
        //        else
        //        {
        //            decrement = over_tick.PerTick;
        //        }
        //    }
        //    //var fit_tick = m_Tick[tickname].FirstOrDefault(e => price <= e.Key);
        //    //var over_tick = m_Tick[tickname].FirstOrDefault(e => (price + fit_tick.Value) <= e.Key);

        //    //if (price < fit_tick.Key)
        //    //{
        //    //    increment = fit_tick.Value;
        //    //    decrement = fit_tick.Value;
        //    //}
        //    //else
        //    //{
        //    //    increment = over_tick.Value;
        //    //    if (price == fit_tick.Key)
        //    //    {
        //    //        decrement = fit_tick.Value;
        //    //    }
        //    //    else
        //    //    {
        //    //        decrement = over_tick.Value;
        //    //    }
        //    //}


        //    //foreach (var item in ticksize)
        //    //{   
        //    //    if (price <= item.Key)
        //    //    {
        //    //        if (price <item.Key &&increment == default(decimal))
        //    //        {
        //    //            increment = item.Value;
        //    //        }
        //    //        if (decrement == default(decimal))
        //    //        {
        //    //            decrement = item.Value;
        //    //        }
        //    //    }                
        //    //    if (increment != default(decimal) && decrement != default(decimal)) { return; }
        //    //}
        //}
        //public virtual decimal AddnTick(string tickname, decimal price, int nTick)
        //{            
        //    decimal increment, decrement;
        //    if (price == 0M) { return 0M; }
        //    if (nTick > 0)
        //    {
        //        for (int i = 0; i < nTick; i++)
        //        {
        //            Tick(tickname, price, out increment, out decrement);
        //            price += increment;
        //        }
        //    }
        //    else if (nTick < 0)
        //    {
        //        for (int i = 0; i < Math.Abs(nTick); i++)
        //        {
        //            Tick(tickname, price, out increment, out decrement);
        //            price -= decrement;
        //        }
        //    }
        //    return price;
        //}
        public static decimal AddnTick(string tickname, decimal price, int nTick)
        {
            decimal increment, decrement;
            if (price == 0M) { return 0M; }
            if (nTick > 0)
            {
                for (int i = 0; i < nTick; i++)
                {
                    Tick(tickname, price, out increment, out decrement);
                    price += increment;
                }
            }
            else if (nTick < 0)
            {
                for (int i = 0; i < Math.Abs(nTick); i++)
                {
                    Tick(tickname, price, out increment, out decrement);
                    price -= decrement;
                }
            }
            return price;
        }
        public static void Tick(string tickname, decimal price, out decimal increment, out decimal decrement)
        {
            increment = default(decimal);
            decrement = default(decimal);

            //if (price <= 0) { return; }
            if (!m_Tick.ContainsKey(tickname)) { return; }
            var fit_tick = m_Tick[tickname].FirstOrDefault(e => price <= e.Key).Value;
            var over_tick = m_Tick[tickname].FirstOrDefault(e => (price + fit_tick.PerTick) <= e.Key).Value;

            if (price < fit_tick.UpLimit)
            {
                increment = fit_tick.PerTick;
                decrement = fit_tick.PerTick;
            }
            else
            {
                increment = over_tick.PerTick;
                if (price == fit_tick.UpLimit)
                {
                    decrement = fit_tick.PerTick;
                }
                else
                {
                    decrement = over_tick.PerTick;
                }
            }           
        }
        public static List<PQ> BestBidByTickCount(string tickname, int tickCount, double[] bestBid, int[] bestBQty)
        {
            List<PQ> re = new List<PQ>();
            int baseIdx = 0;
            for (int i = 0; i < tickCount; i++)
            {
                if (i < bestBid.Length && bestBid[i] != 0D)
                {
                    re.Add(new PQ()
                    {
                        Price = bestBid[i],
                        Qty = bestBQty[i]
                    });
                    baseIdx = i;
                }
                else
                {
                    re.Add(new PQ()
                    {
                        Price = (double)AddnTick(tickname, (decimal)bestBid[baseIdx], (i - baseIdx) * -1),
                        Qty = 0
                    });
                }
            }
            return re;
        }
        public static List<PQ> BestAskByTickCount(string tickname, int tickCount, double[] bestAsk, int[] bestAQty)
        {
            List<PQ> re = new List<PQ>();
            int baseIdx = bestAsk.Length - 1;
            for (int i = 0; i < tickCount; i++)
            {
                if (i < bestAsk.Length && bestAsk[(bestAsk.Length - 1) - i] != 0D)
                {
                    re.Add(new PQ()
                    {
                        Price = bestAsk[(bestAsk.Length - 1) - i],
                        Qty = bestAQty[(bestAQty.Length - 1) - i]
                    });
                    baseIdx = (bestAsk.Length - 1) - i;
                }
                else
                {
                    re.Add(new PQ()
                    {
                        Price = (double)AddnTick(tickname, (decimal)bestAsk[baseIdx], (i - (bestAsk.Length - 1 - baseIdx))),
                        Qty = 0
                    });
                }
            }
            re.Reverse();
            return re;
        }
        public static List<PQ> ExtendBidByTickCount(string tickname, int tickCount, decimal initPrice, bool isUsingBP = false, double[] bestBid = null, int[] bestBQty = null)
        {
            List<PQ> re = new List<PQ>();
            //bool isUsingBP = Market == 0D || Market <= BestBid[0];
            //decimal initPrice;
            //if (isUsingBP)
            //{
            //    initPrice = (decimal)BestBid[0];
            //}
            //else
            //{
            //    initPrice = (decimal)Market;
            //}
            int plus = isUsingBP ? 0 : 1;
            for (int i = 0; i < tickCount; i++)
            {
                double price = (double)AddnTick(tickname, initPrice, (i + plus) * -1);
                int qtyIdx = -1;
                if (bestBid != null)
                {
                    for (int idx = 0; idx < bestBid.Length; idx++)
                    {
                        if (bestBid[idx] == price)
                        {
                            qtyIdx = idx;
                            break;
                        }
                    }
                }
                int qty = qtyIdx == -1 ? 0 : bestBQty[qtyIdx];
                re.Add(new PQ() { Price = price, Qty = qty });
            }
            return re;
        }
        public static List<PQ> ExtendAskByTickCount(string tickname, int tickCount, decimal initPrice, bool isUsingAP = false, double[] bestAsk = null, int[] bestAQty = null)
        {
            List<PQ> re = new List<PQ>();
            //bool isUsingAP = Market == 0D || (BestAsk[4] != 0D && Market >= BestAsk[4]);
            //decimal initPrice;
            //if (isUsingAP)
            //{
            //    initPrice = (decimal)BestAsk[4];
            //}
            //else
            //{
            //    initPrice = (decimal)Market;
            //}
            int plus = isUsingAP ? 0 : 1;
            for (int i = tickCount - 1; i >= 0; i--)
            {
                double price = (double)AddnTick(tickname, initPrice, i + plus);
                int qtyIdx = -1;

                if (bestAsk != null)
                {
                    for (int idx = 0; idx < bestAsk.Length; idx++)
                    {
                        if (bestAsk[idx] == price)
                        {
                            qtyIdx = idx;
                            break;
                        }
                    }
                }
                int qty = qtyIdx == -1 ? 0 : bestAQty[qtyIdx];
                re.Add(new PQ() { Price = price, Qty = qty });
            }
            return re;
        }
        #endregion

        #region Private
        protected virtual void InitTick()
        {
            Config Config = new Config(PATH, NAME);
            //if (m_Tick == null) { m_Tick = new Dictionary<string, Dictionary<decimal, decimal>>(); }
            if (m_Tick == null) {
                m_Tick = new Dictionary<string, Dictionary<decimal, TickInfo>>(); }
            m_Tick.Clear();
            SQLTool sql = new SQLTool(Config.GetSetting<string>("SQL", "IP"),
                Config.GetSetting<string>("SQL", "DB"),
                Config.GetSetting<string>("SQL", "ID"),
                Config.GetSetting<string>("SQL", "PASSWORD"));

            var ticks = sql.Query<TickInfo>("SELECT * FROM TICKSIZE ");
            foreach (var tick in ticks)
            {
                if (!m_Tick.ContainsKey(tick.TickName))
                {
                    m_Tick.Add(tick.TickName, new Dictionary<decimal, TickInfo>());
                }
                if (!m_Tick[tick.TickName].ContainsKey(tick.UpLimit))
                {
                    m_Tick[tick.TickName].Add(tick.UpLimit, tick);
                }
            }
            //DataTable dt = sql.DoQuery("SELECT * FROM TICKSIZE ");
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        string name = row["TICKNAME"].ToString();                    
            //        if (!m_Tick.ContainsKey(name))
            //        {
            //            m_Tick.Add(name, new Dictionary<decimal, decimal>());
            //        }
            //        decimal uplimit = Convert.ToDecimal(row["UPLIMIT"]);
            //        decimal tick = Convert.ToDecimal(row["TICK"]);
            //        if (!m_Tick[name].ContainsKey(uplimit))
            //        {
            //            m_Tick[name].Add(uplimit, tick);
            //        }
            //    }
            //}
        }
        #endregion

    }
    public struct PQ
    {
        public double Price;
        public int Qty;
    }
}