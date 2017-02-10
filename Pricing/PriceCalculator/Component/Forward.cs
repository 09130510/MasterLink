using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PriceCalculator.Component
{
    public class Forward: Asset
    {
        public override int Direction { get { return -1; } }

        public Forward(DataRow row) : base(row) { }
    }
}
