using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Util.Extension.Class
{
    public class NuLog : IDisposable
    {
        #region --  private variable  --
        private bool bDisposed = false;
        private string m_Dir = "";
        private string m_LogName = "";
        private StreamWriter m_LogStream = null;
        private FileStream m_FHdl = null;
        private BinaryWriter m_bWriter = null;
        #endregion

        #region --  private function  --
        private void OpenLog()
        {
            string sPath = "";
            if (m_LogName == "")
                sPath = string.Format("{0}/{1}.log", m_Dir, DateTime.Now.ToString("yyyyMMdd"));
            else
                sPath = string.Format("{0}/{1}.log", m_Dir, m_LogName);

            if (!Directory.Exists(m_Dir))
            {
                Directory.CreateDirectory(m_Dir);
            }
            m_FHdl = new FileStream(sPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            m_LogStream = new StreamWriter(m_FHdl);
            m_LogStream.AutoFlush = true;
            m_bWriter = new BinaryWriter(m_LogStream.BaseStream);
        }
        #endregion

        #region --  construct / destruct  --
        public NuLog(string szDir, string logName)
        {
            m_Dir = szDir;
            m_LogName = logName;
            OpenLog();
        }

        ~NuLog()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool IsDisposing)
        {
            if (bDisposed)
                return;

            if (IsDisposing)
            {
                if (m_LogStream != null)
                {
                    m_LogStream.Flush();
                    m_LogStream.Close();
                    m_LogStream.Dispose();
                    m_LogStream = null;
                }
            }

            bDisposed = true;
        }
        #endregion

        #region --  public property  --
        public bool AutoFlush
        {
            get { return (m_LogStream != null) ? m_LogStream.AutoFlush : false; }
            set
            {
                if (m_LogStream != null)
                    m_LogStream.AutoFlush = value;
            }
        }
        public string Path { get { return m_Dir; } }
        #endregion

        #region --  public function  --

        #region flush
        public void Flush()
        {
            lock (m_LogStream)
            {
                m_LogStream.Flush();
            }
        }
        #endregion

        #region write to file
        public void WrtLog(string Msg)
        {
            lock (m_LogStream)
            {
                if (m_LogStream.BaseStream.CanWrite)
                {
                    m_LogStream.WriteLine(String.Format("{0} [MSG] {1}",
                        DateTime.Now.ToString("HH:mm:ss.fff"), Msg));
                }
            }
        }

        public void WrtLogWithFlush(string Msg)
        {
            lock (m_LogStream)
            {
                if (m_LogStream.BaseStream.CanWrite)
                {
                    m_LogStream.WriteLine(String.Format("{0} [MSG] {1}",
                        DateTime.Now.ToString("HH:mm:ss.fff"), Msg));
                    m_LogStream.Flush();
                }
            }
        }

        public void WrtErr(string Msg)
        {
            lock (m_LogStream)
            {
                if (m_LogStream.BaseStream.CanWrite)
                    m_LogStream.WriteLine(String.Format("{0} [ERR] {1}",
                        DateTime.Now.ToString("HH:mm:ss.fff"), Msg));
            }
        }

        public void WrtErrWithFlush(string Msg)
        {
            lock (m_LogStream)
            {
                if (m_LogStream.BaseStream.CanWrite)
                {
                    m_LogStream.WriteLine(String.Format("{0} [ERR] {1}",
                        DateTime.Now.ToString("HH:mm:ss.fff"), Msg));
                    m_LogStream.Flush();
                }
            }
        }

        public void WriteBinaryData(string sHdr, byte[] bMsg)
        {
            lock (m_LogStream)
            {
                if (m_LogStream.BaseStream.CanWrite)
                {
                    m_LogStream.Write(String.Format("{0} [BIN] {1}",
                      DateTime.Now.ToString("HH:mm:ss.fff"), sHdr));

                    m_bWriter.Write(bMsg, 0, bMsg.Length);
                    m_LogStream.Write(Environment.NewLine);
                }
            }
        }
        #endregion

        #region ReOpen file
        public void ReOpen()
        {
            lock (m_LogStream)
            {
                m_LogStream.Close();
                OpenLog();
            }
        }
        #endregion

        #endregion
    }
}
