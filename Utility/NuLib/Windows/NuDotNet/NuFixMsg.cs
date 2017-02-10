using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace NuDotNet
{
    public class NuFixMsgReader
    {
        #region --  private items  --
        private NuMultiMap<int, string> m_fixmap = new NuMultiMap<int, string>(128);
        private string m_fixmsg_str;
        #endregion

        #region --  private items  --
        public char cSep001 = Convert.ToChar(1);
        public char cSepEqual = '=';
        #endregion

        #region --  construct / destruct  --
        public NuFixMsgReader() { }
        ~NuFixMsgReader() { }
        #endregion

        #region --  property  --
        /// <summary>
        /// fix message
        /// </summary>
        public string FixMsg
        {
            get { return m_fixmsg_str; }
            set { m_fixmsg_str = value; }
        }

        /// <summary>
        /// 取得fix tag的值, 若無此tag則回傳NULL
        /// </summary>
        /// <param name="tag">fix tag</param>
        /// <returns></returns>
        public string this[int tag]
        {
            get { return m_fixmap[tag] == null ? default(string) : m_fixmap[tag][0]; }
        }

        /// <summary>
        /// 取得fix tag的值, 若無此tag則回傳NULL
        /// 若為group, 則可透過index指定要取回第幾個
        /// </summary>
        /// <param name="tag">fix tag</param>
        /// <param name="idx">index</param>
        /// <returns></returns>
        public string this[int tag, int idx]
        {
            get { return m_fixmap[tag, idx]; }
        }
        #endregion

        #region --  Public Method  --

        #region Load Fix Message
        /// <summary>
        /// Load and store fix message 
        /// </summary>
        /// <param name="sFixMsg">ref fix message</param>
        public void LoadFixMsg(string sFixMsg)
        {
            string[] arData;
            m_fixmsg_str = sFixMsg;
            int iSepPos = 0;

            arData = m_fixmsg_str.Split(cSep001);

            foreach (string sTmp in arData)
            {
                iSepPos = sTmp.IndexOf(cSepEqual);
                if (iSepPos <= 0)
                    continue;
                m_fixmap.Add(int.Parse(sTmp.Substring(0, iSepPos)), sTmp.Substring(iSepPos + 1));
            }
        }
        #endregion

        #region Clear
        /// <summary>
        /// clear all resource
        /// </summary>
        public void Clear()
        {
            m_fixmap.Clear();
            m_fixmsg_str = "";
            //m_fixout.Length = 0;
        }
        #endregion

		#region Count
		public int Count(int tag)
		{
			return ContainKey(tag) ? m_fixmap[tag].Count : 0;
		}
		public int Count(string tag)
		{
			return String.IsNullOrEmpty(tag) ? default(int) : Count(int.Parse(tag));
		}
		#endregion

        #region ContainKey
        /// <summary>
        /// fix tag 是否存在
        /// </summary>
        /// <param name="tag">fix tag</param>
        /// <returns></returns>
        public bool ContainKey(int tag)
		{
			return m_fixmap.ContainKey(tag);
		}
        /// <summary>
        /// fix tag 是否存在
        /// </summary>
        /// <param name="tag">fix tag</param>
        /// <returns></returns>
		public bool ContainKey(string tag)
		{
			return String.IsNullOrEmpty(tag) ? false : ContainKey(int.Parse(tag));
		}
        #endregion

        #region Get RepeatingValue
        public List<string> RepeatingValue(int tag)
		{
			return ContainKey(tag) ? m_fixmap[tag] : null;
		}
		public List<string> RepeatingValue(string tag)
		{
			return !String.IsNullOrEmpty(tag) && ContainKey(tag) ? m_fixmap[int.Parse(tag)] : null;
        }
        #endregion

        #endregion
    }

    public class NuFixMsgWriter
    {
        #region --  private items  --
        private StringBuilder m_fixout;
        public char cSep001 = Convert.ToChar(1);
        public char cSepEqual = '=';
        #endregion

        #region --  construct / destruct  --
        public NuFixMsgWriter()
        {
            m_fixout = new StringBuilder(512);
        }

        ~NuFixMsgWriter() { }
        #endregion

        #region --  public property
        public string FixMsg { get { return m_fixout.ToString(); } }
        #endregion

        #region --  public Method  --
        public void Clear()
        {
            m_fixout.Length = 0;
        }

        public int Add(string Tag, string Val)
        {
            m_fixout.AppendFormat("{0}{1}{2}{3}", Tag, cSepEqual, Val, cSep001);
            return m_fixout.Length;
        }

        public int Add(int Tag, string Val)
        {
            m_fixout.AppendFormat("{0}{1}{2}{3}", Tag.ToString(), cSepEqual, Val, cSep001);
            return m_fixout.Length;
        }

		public int Add(string FixMsg)
		{
			m_fixout.Append(FixMsg);
			return m_fixout.Length;
		}

		public int Insert(int Index, string Tag, string Val)
		{
			m_fixout.Insert(Index, string.Format("{0}{1}{2}{3}", Tag.ToString(), cSepEqual, Val, cSep001));
			return m_fixout.Length;
		}
		public int Insert(int Index, int Tag, string Val)
		{
			m_fixout.Insert(Index, string.Format("{0}{1}{2}{3}", Tag.ToString(), cSepEqual, Val, cSep001));
			return m_fixout.Length;
		}
		public int Insert(int Index, string FixMsg)
		{
			m_fixout.Insert(Index, FixMsg);
			return m_fixout.Length;
		}
        #endregion
    }
}
