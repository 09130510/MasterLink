using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceLib.Redis
{
    public class MD
    {
        public string ID { get; private set; }
        public string Time { get; private set; }
        public double MP { get; private set; }
        private MD(string id, string[] msgs)
        {
            ID = id;
            for (int i = 0; i < msgs.Length; i++)
            {
                if (i == 0) { Time = msgs[i]; }
                else if (i == 1)
                {
                    double v;
                    double.TryParse(msgs[i], out v);
                    MP = v;
                }
            }
        }
        public static MD Create(string channel, string msg)
        {
            string[] items = channel.Split('.');
            string[] msgs = msg.Split('|');
            for (int i = 0; i < items.Length; i++)
            {
                if (i == 0 && (items[i] != "MD" && items[i] != "MDF"))
                {
                    return null;
                }
                else if (i == 1)
                {
                    return new MD(items[i], msgs);
                }
            }
            return null;
        }
        public static MD Create(string id, string channel, string msg)
        {
            string[] items = channel.Split('.');
            string[] msgs = msg.Split('|');

            if (items[0] != "MD" && items[0] != "MDF") { return null; }
            if (msgs.Length < 2) { return null; }
            return new MD(id, msgs);
        }
    }
    public class MR
    {
        public double Ref { get; private set; }
        public double Up { get; private set; }
        public double Down { get; private set; }
        private MR(string channel, string msg)
        {
            string[] items = msg.Split('|');
            if (channel == "MR")
            {
                for (int i = 0; i < items.Length; i++)
                {
                    double v = 0D;
                    double.TryParse(items[i], out v);
                    if (i == 1) { Ref = v; }
                    else if (i == 2) { Up = v; }
                    else if (i == 3) { Down = v; }
                }
            }
            else if (channel == "MRF")
            {
                for (int i = 0; i < items.Length; i++)
                {
                    double v = 0D;
                    double.TryParse(items[i], out v);
                    if (i == 0) { Ref = v; }
                    else if (i == 1) { Up = v; }
                    else if (i == 2) { Down = v; }
                }
            }
        }
        public static MR Create(string channel, string msg)
        {
            string item = channel.Split('.')[0];
            if (item != "MR" && item != "MRF") { return null; }
            return new MR(item, msg);
        }
    }
    public class MB
    {
        public string ID { get; private set; }
        public string Time { get; private set; }
        public double[] Bid { get; private set; }
        public int[] BVolume { get; private set; }
        public double[] Ask { get; private set; }
        public int[] AVolume { get; private set; }
        private MB(string id, string msg)
        {
            string[] items = msg.Split('|');
            string[] bid = new string[5] { "-1", "-1", "-1", "-1", "-1" };
            string[] ask = new string[5] { "-1", "-1", "-1", "-1", "-1" };
            string[] bidVol = new string[5] { "-1", "-1", "-1", "-1", "-1" };
            string[] askVol = new string[5] { "-1", "-1", "-1", "-1", "-1" };
            Array.ConstrainedCopy(items, 1, bid, 0, 5);
            Array.ConstrainedCopy(items, 6, bidVol, 0, 5);
            Array.ConstrainedCopy(items, 11, ask, 0, 5);
            Array.ConstrainedCopy(items, 16, askVol, 0, 5);
            ID = id;
            Time = items[0];
            Bid = Array.ConvertAll(bid, (IN) => { return _ConvertDouble(IN); });
            BVolume = Array.ConvertAll(bidVol, (IN) => { return _ConvertInt(IN); });
            Ask = Array.ConvertAll(ask, (IN) => { return _ConvertDouble(IN); });
            AVolume = Array.ConvertAll(askVol, (IN) => { return _ConvertInt(IN); });
        }
        private double _ConvertDouble(string IN)
        {
            return string.IsNullOrEmpty(IN) ? 0D : double.Parse(IN);
        }
        private int _ConvertInt(string IN)
        {
            return string.IsNullOrEmpty(IN) ? 0 : int.Parse(IN);
        }
        public static MB Create(string channel, string msg)
        {
            string[] items = channel.Split('.');

            if (items[0] != "MB" && items[0] != "MBF") { return null; }
            return new MB(items[1], msg);
        }
        public static MB Create(string id, string channel, string msg)
        {
            string[] items = channel.Split('.');
            if (items[0] != "MB" && items[0] != "MBF") { return null; }
            return new MB(id, msg);
        }
    }
}
