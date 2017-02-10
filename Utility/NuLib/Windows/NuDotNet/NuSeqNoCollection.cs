using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace NuDotNet
{
	public class NuSeqNoCollection
	{
		private class NuSeqNoCollectionItem
		{
			#region --  private items  --
			private char m_Split = Convert.ToChar(1);
			private StringBuilder m_sbCurrentSeqNo;
			private int m_Len;
			private int m_Body_Len;
			private int m_Offset;
			private string m_Prefix;
			private string m_Body;
			private string m_Suffix;
			#endregion

			#region --  property  --
			public int Offset { get { return m_Offset; } }
			public string Prefix { get { return m_Prefix; } }
			public string Suffix { get { return m_Suffix; } }
			public string Key { get { return m_Prefix + m_Suffix; } }
			public string Value { get { return string.Format("{0}{1}{2}{3}{4}", m_Prefix, m_Split, m_Body, m_Split, m_Suffix); } }
			public string CurrentSeqno { get { return m_sbCurrentSeqNo.ToString(); } }
			#endregion

			#region --  construct / destruct  --
			public NuSeqNoCollectionItem(int iLen, int iOffset, string sPrefix, string sSuffix, int iBeginNo = 0)
			{
				m_Len = iLen;
				m_Offset = iOffset;
				m_Prefix = sPrefix;
				m_Suffix = sSuffix;
				m_Body_Len = m_Len - m_Prefix.Length - m_Suffix.Length;
				m_Body = iBeginNo.ToString().PadLeft(m_Body_Len, '0');

				m_sbCurrentSeqNo = new StringBuilder(m_Len);
				m_sbCurrentSeqNo.Length = 0;
				m_sbCurrentSeqNo.Append(m_Prefix);
				m_sbCurrentSeqNo.Append(m_Body);
				m_sbCurrentSeqNo.Append(m_Suffix);
			}
			public NuSeqNoCollectionItem(int iOffset, string sSeqNo)
			{
				m_Offset = iOffset;
				string[] seq = sSeqNo.Split(m_Split);
				if (seq.Length > 0) m_Prefix = seq[0];
				if (seq.Length > 1) m_Body = seq[1];
				if (seq.Length > 2) m_Suffix = seq[2];
				m_Len = m_Prefix.Length + m_Body.Length + m_Suffix.Length;
				m_Body_Len = m_Body.Length;

				m_sbCurrentSeqNo = new StringBuilder(m_Len);
				m_sbCurrentSeqNo.Length = 0;
				m_sbCurrentSeqNo.Append(m_Prefix);
				m_sbCurrentSeqNo.Append(m_Body);
				m_sbCurrentSeqNo.Append(m_Suffix);
			}
			~NuSeqNoCollectionItem() { }
			#endregion

			#region --  private function  --
			private bool _GenerateNext()
			{
				try
				{
					Int32 iNo = Int32.Parse(m_Body);
					iNo++;
					m_Body = iNo.ToString().PadLeft(m_Body_Len, '0');
					return true;
				}
				catch (Exception ex)
				{
					throw new Exception("_GenerateNext : " + ex.Message.ToString());
				}
			}
			#endregion

			#region --  public method  --
			public bool GetNext()
			{
				lock (m_sbCurrentSeqNo)
				{
					bool bRC = _GenerateNext();

					if (bRC == true)
					{
						m_sbCurrentSeqNo.Length = 0;
						m_sbCurrentSeqNo.Append(m_Prefix);
						m_sbCurrentSeqNo.Append(m_Body);
						m_sbCurrentSeqNo.Append(m_Suffix);
					}
					return bRC;
				}
			}
			public void SetSeqNo(int iSeqno)
			{
				m_Body = iSeqno.ToString().PadLeft(m_Body_Len, '0');

				m_sbCurrentSeqNo = new StringBuilder(m_Len);
				m_sbCurrentSeqNo.Length = 0;
				m_sbCurrentSeqNo.Append(m_Prefix);
				m_sbCurrentSeqNo.Append(m_Body);
				m_sbCurrentSeqNo.Append(m_Suffix);
			}
			#endregion
		}

		#region --  private items  --
		private int m_Len;
		private int m_File_len;
		private byte[] m_Byte;
		private FileStream m_FHdl;
		private Dictionary<string, NuSeqNoCollectionItem> m_Seqno;
		#endregion

		#region --  property  --
		public string this[string Prefix, string Suffix]
		{
			get
			{
				if (!m_Seqno.ContainsKey(Prefix + Suffix)) { AddNewSeqno(Prefix, Suffix); }
				return m_Seqno[Prefix + Suffix].CurrentSeqno;
			}
		}
		public string this[string Prefix]
		{
			get
			{
				if (!m_Seqno.ContainsKey(Prefix)) AddNewSeqno(Prefix, string.Empty);
				return m_Seqno[Prefix].CurrentSeqno;
			}
		}
		#endregion

		#region --  construct / destruct  --
		public NuSeqNoCollection(int iLen, string sFile)
		{
			m_Len = iLen;
			m_File_len = iLen + 2;
			m_Byte = new byte[m_File_len];
			m_Seqno = new Dictionary<string, NuSeqNoCollectionItem>();
			File_Init(sFile);
		}
		~NuSeqNoCollection()
		{
			if (m_FHdl != null)
				m_FHdl.Close();
		}
		#endregion

		#region --  private function  --
		private void File_Init(string sFile)
		{
			if (File.Exists(sFile))
			{
				int offset = 0;
				m_FHdl = new FileStream(sFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
				while (offset < m_FHdl.Length)
				{
					int readlen = m_FHdl.Read(m_Byte, 0, m_File_len);
					if (readlen == m_File_len)
					{
						NuSeqNoCollectionItem s = new NuSeqNoCollectionItem(offset, Encoding.Default.GetString(m_Byte));
						if (!m_Seqno.ContainsKey(s.Key)) m_Seqno.Add(s.Key, s);
						offset += readlen;
					}
				}
			}
			else
			{
				m_FHdl = new FileStream(sFile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
				m_FHdl.Flush();
			}
		}
		#endregion

		#region --  public method  --
		public void AddNewSeqno(string sPreFix, string sSufFix, int iBeginNo = 0)
		{
			if (m_Seqno.ContainsKey(sPreFix + sSufFix)) { return; }
			m_FHdl.Seek(0, SeekOrigin.End);
			NuSeqNoCollectionItem s = new NuSeqNoCollectionItem(m_Len, (int)m_FHdl.Length, sPreFix, sSufFix, iBeginNo);
			m_Seqno.Add(s.Key, s);

			m_FHdl.Write(Encoding.Default.GetBytes(s.Value), 0, s.Value.Length);
			m_FHdl.Flush();
		}
		public bool GetNext(string sPreFix, string sSufFix, out string sSeqNo)
		{
			if (!m_Seqno.ContainsKey(sPreFix + sSufFix)) { AddNewSeqno(sPreFix, sSufFix); }
			NuSeqNoCollectionItem s = m_Seqno[sPreFix + sSufFix];
			bool re = s.GetNext();
			if (re)
			{
				m_FHdl.Seek(s.Offset, SeekOrigin.Begin);
				m_FHdl.Write(Encoding.Default.GetBytes(s.Value), 0, s.Value.Length);
				m_FHdl.Flush();
			}
			sSeqNo = s.CurrentSeqno;
			return re;
		}
		public string SetSeqno(string sPrefix, string sSuffix, int iSeqno)
		{
			string re = string.Empty;
			if (m_Seqno.ContainsKey(sPrefix + sSuffix))
			{
				NuSeqNoCollectionItem s = m_Seqno[sPrefix + sSuffix];
				s.SetSeqNo(iSeqno);
				re = s.CurrentSeqno;
			}
			return re;
		}
		public void Flush()
		{
			m_FHdl.Flush();
		}
		#endregion
	}
}
