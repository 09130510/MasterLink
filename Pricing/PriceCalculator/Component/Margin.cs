using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace PriceCalculator.Component
{
    public class Margin : Asset
    {
        public Margin(DataRow row) : base(row) { }
    }
}
