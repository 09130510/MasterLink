
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PriceLib.Redis
{
    public enum SubscribeType
    {
        //[Description("MD.|MB.")]
        //Stock = 2,
        //[Description("MDF.|MBF.")]
        //Future = 4

        [MktChannel("MDF."), BidOfferChannel("MBF.")]
        Future = 4,
        [MktChannel("MD."), BidOfferChannel("MB.")]
        Stock = 2

    }
}
