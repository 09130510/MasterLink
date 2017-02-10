using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceLib.Redis
{
    [AttributeUsage(AttributeTargets.Field)]
    public class BidOfferChannelAttribute : Attribute
    {
        public string Channel { get; set; }

        public BidOfferChannelAttribute(string channel)
        {
            Channel = channel;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class MktChannelAttribute : Attribute
    {
        public string Channel { get; set; }

        public MktChannelAttribute(string channel)
        {
            Channel = channel;
        }
    }
}
