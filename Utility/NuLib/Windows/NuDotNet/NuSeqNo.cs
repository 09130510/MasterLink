using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NuDotNet
{
    public class NuSeqNo : IDisposable
    {
        #region --  private items  --
        private readonly string m_Prefix;
        private readonly int m_Prefix_len;
        private readonly string m_Suffix;
        private readonly int m_Suffix_len;
        private readonly string m_Max;
        private readonly string m_Min;
        private readonly int m_len;
        private readonly int m_body_len;
        private StringBuilder m_sbCurrentSeqNo;
        private byte[] m_byte;
        private FileStream m_FHdl;
        #endregion

        #region --  delegate for event  --
        public delegate bool dlgGenerateNext(ref string sNowSeqNoBody, int iBodyLen);
        #endregion

        #region --  public event  --
        public event dlgGenerateNext evGenerateNext;
        #endregion

        #region --  construct / destruct  --
        #region internal_init 
        private void File_init(string sFile)
        {
            try
            {
                if (File.Exists(sFile))
                {
                    int iRead;
                    m_FHdl = new FileStream(sFile, FileMode.Open);
                    iRead = m_FHdl.Read(m_byte, 0, m_len);
                    if (iRead == m_len)
                    {
                        m_sbCurrentSeqNo.Length = 0;
                        m_sbCurrentSeqNo.Append(Encoding.Default.GetString(m_byte));
                    }
                }

                if (m_sbCurrentSeqNo.Length == 0)
                {
                    if (m_Prefix_len > 0)
                        m_sbCurrentSeqNo.Append(m_Prefix);
                    m_sbCurrentSeqNo.Append('0', m_body_len);
                    if (m_Suffix_len > 0)
                        m_sbCurrentSeqNo.AppendFormat(m_Suffix);

                    m_FHdl = new FileStream(sFile, FileMode.CreateNew);
                    m_FHdl.Write(Encoding.Default.GetBytes(m_sbCurrentSeqNo.ToString()), 0, m_sbCurrentSeqNo.Length);
                    m_FHdl.Flush();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("internal_init : {0}", ex.Message.ToString()));
            }
        }
        #endregion

        public NuSeqNo(int Len, string sFile)
        {
            m_sbCurrentSeqNo = new StringBuilder(Len);
            m_byte = new byte[Len];
            m_Prefix = "";
            m_Prefix_len = 0;
            m_Suffix = "";
            m_Suffix_len = 0;
            m_len = Len;
            m_body_len = m_len;

            m_Max = "";
            m_Min = "";
            m_Max = m_Max.PadLeft(Len, '9');
            m_Min = m_Min.PadLeft(Len, '0');

            File_init(sFile);
        }

        public NuSeqNo(int Len, string Prefix, string Suffix, string Max, string Min, string sFile)
        {
            m_sbCurrentSeqNo = new StringBuilder(Len);
            m_byte = new byte[Len];

            m_Prefix = Prefix;
            m_Prefix_len = m_Prefix.Length;

            m_Suffix = Suffix;
            m_Suffix_len = m_Suffix.Length;

            m_len = Len;
            m_body_len = m_len - m_Prefix_len - m_Suffix_len;

            m_Max = (Max.Length == 0) ? m_Max.PadLeft(Len, '9') : Max;
            m_Min = (Min.Length == 0) ? m_Min.PadLeft(Len, '0') : Min;

            File_init(sFile);
        }

        ~NuSeqNo() 
        {
        
        }

        public void Dispose()
        {
            if (m_FHdl != null)
                m_FHdl.Close();
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }

        #endregion

        #region --  property  --
        #region readonly Current SeqNo
        public string CurrentSeqNo
        {
            get { return m_sbCurrentSeqNo.ToString(); }
        }
        #endregion

        #region readonly Prefix
        public string Prefix
        {
            get { return m_Prefix; }
        }
        #endregion

        #region readonly Suffix
        public string Suffix
        {
            get { return m_Suffix; }
        }
        #endregion

        #region readonly Max
        public string Max
        {
            get { return m_Max; }
        }
        #endregion

        #region readonly Min
        public string Min
        {
            get { return m_Min; }
        }
        #endregion
        #endregion

        #region --  private function  --
        private bool _GenerateNext(ref string sSeqNoBody)
        {
            try
            {
                Int32 iNo = Int32.Parse(sSeqNoBody);
                iNo++;
                sSeqNoBody = iNo.ToString().PadLeft(m_body_len, '0');
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("_GenerateNext : " + ex.Message.ToString());
            }
        }
        #endregion

        #region --  public method  --

        #region GetNext
        public bool GetNext(out string OutSeqNo)
        {
            lock (m_sbCurrentSeqNo)
            {
                bool bRC = false;
                string sBody = m_sbCurrentSeqNo.ToString().Substring(m_Prefix_len, m_body_len);
                if (evGenerateNext != null)
                    bRC = evGenerateNext(ref sBody, m_body_len);
                else
                    bRC = _GenerateNext(ref sBody);

                if (bRC == true)
                {
                    m_sbCurrentSeqNo.Length = 0;
                    m_sbCurrentSeqNo.Append(m_Prefix);
                    m_sbCurrentSeqNo.Append(sBody);
                    m_sbCurrentSeqNo.Append(m_Suffix);
                    m_FHdl.Seek(0, SeekOrigin.Begin);
                    m_FHdl.Write(Encoding.Default.GetBytes(m_sbCurrentSeqNo.ToString()), 0, m_sbCurrentSeqNo.Length);
                }

                OutSeqNo = m_sbCurrentSeqNo.ToString();

                return bRC;
            }
        }

        public bool SetSeqNo(ref string sSeqNo)
        {
            lock (m_sbCurrentSeqNo)
            {
                m_sbCurrentSeqNo.Length = 0;
                m_sbCurrentSeqNo.Append(sSeqNo);
                m_FHdl.Seek(0, SeekOrigin.Begin);
                m_FHdl.Write(Encoding.Default.GetBytes(m_sbCurrentSeqNo.ToString()), 0, m_sbCurrentSeqNo.Length);
                m_FHdl.Flush();
            }
            return true;
        }
        #endregion

        #region Flush
        public void Flush()
        {
            m_FHdl.Flush();
        }
        #endregion

        #endregion
    }
}
