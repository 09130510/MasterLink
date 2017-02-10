using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceProcessor
{
    public class TickInfo
    {
        public string TickName { get; set; }
        public decimal UpLimit { get; set; }
        public decimal Tick {private  get; set; }
        public decimal Numerator { private get; set; }
        public decimal Denominator { private get; set; }
        public decimal PerTick
        {
            get
            {
                if (Tick==-1)
                {
                    return Numerator / Denominator;
                }
                else
                {
                    return Tick;
                }
            }
        }
    }
}
