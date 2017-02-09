using System;
using System.Collections.Generic;
using System.Linq;
using Bloomberglp.Blpapi;

namespace BLParser
{
    [Serializable]
    public class SecurityCollection
    {
        private Dictionary<string, Security> m_Securities;

        public Security this[string Name]
        {
            get
            {
                return this.Securities.ContainsKey(Name) ? this.Securities[Name] : null;
            }
        }
        public bool HasSecurities { get { return Securities != null && Securities.Count > 0; } }
        public Dictionary<string, Security> Securities
        {
            get
            {
                if (m_Securities == null) { m_Securities = new Dictionary<string, Security>(); }
                return m_Securities;
            }
        }

        public SecurityCollection(Event e)
        {
            Subscribe(e);
        }
        public SecurityCollection(Element e)
        {
            AddResponse(e);
        }

        internal void AddResponse(Element e)
        {
            //Console.WriteLine(e.NumValues);
            //Console.WriteLine(e.Elements.Count());            

            int securityCnt = e.NumValues;
            for (int i = 0; i < securityCnt; ++i)
            {
                Security s = new Security(e.GetValueAsElement(i));
                if (Securities.ContainsKey(s.Name))
                {
                    //Console.WriteLine("Join");
                    Securities[s.Name].Join(s);
                    //Console.WriteLine("Join Complete");
                }
                else
                {
                    //Console.WriteLine("Add");
                    Securities.Add(s.Name, s);
                    //Console.WriteLine("Add Complete");
                }
            }
        }
        internal void Subscribe(Event e)
        {
            foreach (Message msg in e)
            {
                Security s = new Security(msg);
                if (Securities.ContainsKey(s.Name))
                {
                    Securities[s.Name].Join(s);
                }
                else
                {
                    Securities.Add(s.Name, s);
                }

            }
        }

        public void Join(SecurityCollection s)
        {

            foreach (var item in s.Securities)
            {
                if (this.Securities.ContainsKey(item.Key))
                {
                    this.Securities[item.Key].Join(item.Value);
                }
                else
                {
                    this.Securities.Add(item.Key, item.Value);
                }
            }

        }
    }
}