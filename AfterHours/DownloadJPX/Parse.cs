using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadJPX
{
    public class Parse
    {
        private static  Dictionary<string, string> m_ParsePID;

        private static Dictionary<string, string> ParsePID
        {
            get
            {
                if (m_ParsePID == null)
                {
                    m_ParsePID = new Dictionary<string, string>();
                    m_ParsePID.Add("TOPIXM", "JMT");
                    m_ParsePID.Add("TOPIX", "JTI");                    
                }
                return m_ParsePID;
            }
        }
        public DateTime Date { get; private set; }
        public string PID { get; private set; }
        public string YM { get; private set; }
        public double SettlePrice { get; private set; }

        public string SQL
        {
            get
            {
                return string.Format("INSERT INTO MM..SETTLEPRICE (DATE, PID, YM, SETTLEPRICE) VALUES ('{0}','{1}','{2}',{3}) ", Date, PID, YM, SettlePrice);
            }
        }

        public Parse(DateTime date, string[] data)
        {
            Date = date;
            foreach (var name in ParsePID.Keys)
            {
                if (data[1].Contains(name))
                {
                    PID = m_ParsePID[name];
                    break;
                }
            }
            YM = data[3];
            SettlePrice = double.Parse(data[5]);
        }

        
    }
}
