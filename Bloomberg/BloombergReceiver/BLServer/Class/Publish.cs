using BLParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLPServer.Class
{
    public class Publish
    {
        public string Key { get; private set; }
        public double MP { get; set; } = 0;
        public double AP { get; set; } = 0;
        public double BP { get; set; } = 0;
        public double MidP
        {
            get
            {
                if (BP == 0 || AP == 0)
                {
                    return MP;
                }
                return (AP + BP) / 2;
            }
        }


        public Publish(Security s)
        {
            Key = s.Name;            
        }

        public void Update(Security s)
        {
            if (Key != s.Name) { return; }
            if (s.Values.ContainsKey("LAST_PRICE"))
            {
                MP = Convert.ToDouble(s.Values["LAST_PRICE"]);
            }
            if (s.Values.ContainsKey("BID"))
            {
                BP = Convert.ToDouble(s.Values["BID"]);
            }
            if (s.Values.ContainsKey("ASK"))
            {
                AP = Convert.ToDouble(s.Values["ASK"]);
            }
        }
    }
}
