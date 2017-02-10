using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace NuDotNet
{
    public class NuMultiMap<TKey, TVal>
    {
        #region --  private items  --
        private Dictionary<TKey, List<TVal>> m_dic;
        #endregion

        #region --  construct / destruct  --
        public NuMultiMap()
        {
            m_dic = new Dictionary<TKey, List<TVal>>();
        }

        public NuMultiMap(int InitSize)
        {
            m_dic = new Dictionary<TKey, List<TVal>>(InitSize);
        }
        #endregion

        #region --  property  --
        public List<TVal> this[TKey Key]
        {
            get { return m_dic.ContainsKey(Key) ? m_dic[Key] : null; }
        }

        public TVal this[TKey Key, int idx]
        {
            get { return m_dic.ContainsKey(Key) ? 
                       (idx >= m_dic[Key].Count ? default(TVal) : m_dic[Key][idx]) 
                                                 : default(TVal); }
        }

        public Dictionary<TKey, List<TVal>>.KeyCollection Keys
        {
            get { return m_dic.Keys; }
        }

        public Dictionary<TKey, List<TVal>>.ValueCollection Vals
        {
            get { return m_dic.Values; }
        }

        public int Count
        {
            get { return m_dic.Count; }
        }
        #endregion

        #region --  Public Method  --
        public int Add(TKey Key, TVal Val)
        {
            
            List<TVal> lstVal;
            if (m_dic.ContainsKey(Key))
            {
                lstVal = lstVal = m_dic[Key];
                lstVal.Add(Val);
            }
            else
            {
                lstVal = new List<TVal>(2);
                lstVal.Add(Val);
                m_dic.Add(Key, lstVal);
            }
            return lstVal.Count;
        }

        public bool Remove(TKey Key)
        {
            if (m_dic.ContainsKey(Key))
                m_dic.Remove(Key);
            else
                return false;
            return true;
        }

        public bool ContainKey(TKey Key)
        {
            return m_dic.ContainsKey(Key);
        }

        public IEnumerator<KeyValuePair<TKey, List<TVal>>> GetEnumerator()
        {
            foreach (KeyValuePair<TKey, List<TVal>> pair in m_dic)
            {
                yield return pair;
            }
        }

        public void Clear()
        {
            m_dic.Clear();
        }


        #endregion
    }
}
