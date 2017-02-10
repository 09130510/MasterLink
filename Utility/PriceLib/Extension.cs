using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using PriceLib.Redis;

namespace PriceLib
{
    public static class Extension
    {
        public static string Description(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }

        public static string BidOfferChannel(this Enum value)
        {
            BidOfferChannelAttribute[] attributes = (BidOfferChannelAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(BidOfferChannelAttribute), false);
            if ((attributes != null) && (attributes.Length != 0))
            {
                return attributes[0].Channel;
            }
            return string.Empty;
        }
        public static string MktChannel(this Enum value)
        {
            MktChannelAttribute[] attributes = (MktChannelAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(MktChannelAttribute), false);
            if ((attributes != null) && (attributes.Length != 0))
            {
                return attributes[0].Channel;
            }
            return string.Empty;
        }

    }
}
