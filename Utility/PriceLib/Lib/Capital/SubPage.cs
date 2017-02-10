using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceLib.Capital
{
    public class SubPage
    {
        private const int LIMIT = 100;
        private Dictionary<string, int> m_Subs =  new Dictionary<string, int>();

        #region Property
        public short PageNo { get; set; }
        public int Counts { get { return m_Subs.Count; } }
        
        #endregion

        public SubPage()
        {
            PageNo = 0;
        }

        #region Public
        public bool Add(string substr)
        {
            if (!m_Subs.ContainsKey(substr))
            {
                if (m_Subs.Count >= 100) { return false; }
                m_Subs.Add(substr, 0);
            }
            m_Subs[substr]++;
            return true;
        }
        public void Remove(string substr)
        {
            if (!m_Subs.ContainsKey(substr)) { return; }
            m_Subs[substr]--;
            if (m_Subs[substr] <= 0) { m_Subs.Remove(substr); }
        }
        public void Clear()
        {
            m_Subs.Clear();
        }
        public bool Contains(string substr)
        {
            return m_Subs.ContainsKey(substr);
        }
        public override string ToString()
        {
            return string.Join("#", m_Subs.Keys.ToArray());
        }
        #endregion

    }
}
