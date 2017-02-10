//#define DEBUG_PRINT 
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

namespace NuDotNet.Net
{
    /* ************************************************************
     * History:
     * 
     * ************************************************************/
    /// <summary>
    /// Socket Client object base .
    /// </summary>
    public class NuSocketClient : IDisposable
    {
        #region --  private items  --
        private bool bDisposed = false;
        private Thread m_RcvThd;
        private Thread m_SndThd;
        private Socket m_TcpCli;

        private byte[] m_buf = null;
        private int m_buf_sz = 0;
        private bool m_Quit;

        private bool m_connecting_flag = false;

        /// <summary>
        /// 一秒 (us)
        /// </summary>
        protected const int m_OneSec_us = 1000000;   // micro sec

        /// <summary>
        /// 一秒 (ms)
        /// </summary>
        protected const int m_OneSec_ms = 1000;

        /// <summary>
        /// 最後送資料時間 (DateTime)
        /// </summary>
        protected DateTime m_LastSendTime = DateTime.Now;
        /// <summary>
        /// 最後收資料時間
        /// </summary>
        protected DateTime m_LastRecvTime = DateTime.Now;

        private int m_TimeInterval = m_OneSec_ms;               //Timer interval
        private object m_Obj = null;
        private IList m_select_fd_list = new ArrayList();

        /// <summary>
        /// remote server IP
        /// </summary>
        protected string m_IP;

        /// <summary>
        /// remote server port
        /// </summary>
        protected int m_Port;

        /// <summary>
        /// auto reconnect flag.
        /// </summary>
        protected bool m_AutoReConnect = false;

        private readonly object m_SockSndHdl = new object();
        private readonly object m_SockRcvHdl = new object();
        private readonly object m_SockHdl = new object();
        #endregion

        #region --  public delegate / event  --

        protected delegate bool OnBaseConnect(object obj);
        /// <summary>
        /// Socket 連線連上觸發
        /// </summary>
        protected event OnBaseConnect OnBaseConnectEv;

        protected delegate void OnBaseDisconnect(object obj);
        /// <summary>
        /// Socket 斷線觸發
        /// </summary>
        protected event OnBaseDisconnect OnBaseDisconnectEv;
        
        public delegate void RcvBaseDataHandler(object obj);
        /// <summary>
        /// 資料抵達觸發, 請將obj轉型為 NuSocketClient 物件, 使用TCPRecv接收資料
        /// </summary>
        protected event RcvBaseDataHandler OnBaseDataArriveEv;

        public delegate void DataDump(byte[] bMsg);
        /// <summary>
        /// 收資料觸發
        /// </summary>
        public event DataDump OnRcvDataDumpEv;

        /// <summary>
        /// 送資料前後觸發
        /// </summary>
        public event DataDump OnSndDataDumpEv;

        public delegate void OnTimer(object obj, DateTime LastRecvTime, DateTime LastSendTime);
        /// <summary>
        /// 於指定時間區間觸發, 預設1秒
        /// </summary>
        public event OnTimer OnTimerEv;

        public delegate void OnException(string sMsg);
        /// <summary>
        /// Exception 觸發
        /// </summary>
        public event OnException OnExceptionEv;
        #endregion

        #region --  construct / destruct  --
        private void internal_init()
        {
            m_Quit = true;
            m_TcpCli = null;
        }
        public NuSocketClient(int RcvBufferSize)
        {
            internal_init();
            m_buf_sz = RcvBufferSize;
            m_buf = new byte[m_buf_sz];
        }

        ~NuSocketClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }

        protected virtual void Dispose(bool IsDisposing)
        {
            if (bDisposed)
                return;

            if (m_TcpCli == null)
                return;

            if (IsDisposing)
            {
                //lock (m_TcpCli)
                lock(m_SockHdl)
                {
                    if (m_TcpCli != null)
                    {
                        if (m_TcpCli.Connected)
                        {
                            m_TcpCli.Close();
                        }
                        m_TcpCli.Dispose();
                        m_TcpCli = null;
                    }
                }
                _Stop();
                
                m_Quit = true;
                bDisposed = true;
            }
        }
        #endregion

        #region --  property  --
        /// <summary>
        /// 取回自訂物件
        /// </summary>
        public object ArgvObject { get { return m_Obj; } }
        /// <summary>
        /// socket 物件
        /// </summary>
        public Socket SocketObject { get { return m_TcpCli; } }

        /// <summary>
        /// connection status (readonly)
        /// </summary>
        public bool IsConnected { get { return (m_TcpCli == null) ? false : m_TcpCli.Connected; } } 

        /// <summary>
        /// remote server ip
        /// </summary>
        public string ConnectIP { get { return m_IP; } }

        /// <summary>
        /// remote server port
        /// </summary>
        public int ConnectPort { get { return m_Port; } }

        /// <summary>
        /// auto reconnect or not
        /// </summary>
        public bool AutoReConnect { get { return m_AutoReConnect; } set { m_AutoReConnect = value; } }

        public int AvailableDataSz
        {
            get
            {
                if (m_TcpCli == null)
                    return -1;
                else if (!m_TcpCli.Connected)
                    return -1;
                else
                    return m_TcpCli.Available;
            }
        }
        #endregion

        #region --  private function  --

        #region thread Start
        private void _Start()
        {
            if (m_SndThd == null)
                m_SndThd = new Thread(doTimeWork);

            if (!m_SndThd.IsAlive)
            {
                m_SndThd.IsBackground = true;
                m_SndThd.Start();
            }

            if (m_RcvThd == null)
                m_RcvThd = new Thread(doRcvWork);

            if (!m_RcvThd.IsAlive)
            {
                m_RcvThd.IsBackground = true;
                m_RcvThd.Start();
            }
        }
        #endregion

        #region thread Stop
        private void _Stop()
        {
            m_Quit = true;
            if (m_RcvThd != null)
            {
                if (m_RcvThd.IsAlive)
                {
                    m_RcvThd.Abort();
                    //m_RcvThd.Join();
                }
                m_RcvThd = null;
            }

            if (m_SndThd != null)
            {
                if (m_SndThd.IsAlive)
                {
                    m_SndThd.Abort();
                    //m_SndThd.Join();
                }
                m_SndThd = null;
            }
        }
        #endregion

        private int _recv(ref MemoryStream mStream, int size)
        {

            int iByte = 0;
            int maxRcvByte = 0;
            mStream.SetLength(0);

            try
            {
                lock (m_SockRcvHdl)
                {
                    while (size > 0)
                    {
                        maxRcvByte = (size > m_buf_sz) ? m_buf_sz : size;
                        iByte = m_TcpCli.Receive(m_buf, maxRcvByte, SocketFlags.None);
                        if (iByte == 0 && mStream.Length == 0)
                            return 0;
                        size -= iByte;
                        mStream.Write(m_buf, 0, iByte);
                    }
                }
            }
            catch (Exception ex)
            {
                if (OnExceptionEv != null)
                    OnExceptionEv(String.Format("_recv : {0}[{1}]", ex.Message.ToString(), ex.StackTrace.ToString()));
                return -1;
            }
            return (int)mStream.Length;
        }

        private int _send(ref byte[] sMsg)
        {
            List<Socket> socklst = new List<Socket>();
            List<Socket> sockerrlst = new List<Socket>();

            int iSnd = 0;
            lock (m_SockSndHdl)
            {
                socklst.Add(m_TcpCli);
                sockerrlst.Add(m_TcpCli);
                try
                {
                    Socket.Select(null, socklst, sockerrlst, m_OneSec_us);
                    if (socklst.Count > 0)
                    {
                        iSnd = m_TcpCli.Send(sMsg);
                    }
                    else if (sockerrlst.Count > 0)
                    {
                        return -1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    if (OnExceptionEv != null)
                        OnExceptionEv(String.Format("_send : {0}[{1}]", ex.Message.ToString(), ex.StackTrace.ToString()));
                    return -1;
                }
            }

            return iSnd;
        }

        private void _Disconnect()
        {
            if (m_TcpCli == null)
                return;

            lock(m_SockHdl)
            {
                if (m_TcpCli == null)
                    return;

                if (m_TcpCli.Connected)
                {
                    m_TcpCli.Shutdown(SocketShutdown.Both);
                    m_TcpCli.Disconnect(false);
                }
                if (OnBaseDisconnectEv != null)
                {
                    //System.Diagnostics.Debug.WriteLine("======> event disconnect ");
                    OnBaseDisconnectEv(this);
                }

                m_TcpCli.Dispose();
                m_TcpCli = null;
            }

            
        }


        private void Thd_Connect_Work(object oObj)
        {
            String[] ConnectInfo = ((String)oObj).Split(':');

            m_connecting_flag = true;
            _Connect(ConnectInfo[0], ConnectInfo[1]);
            m_connecting_flag = false;
        }

        private bool _Connect(string sIP, string sPort)
        {
            try
            {
                bool bRC = false;
                m_IP = sIP;
                m_Port = Convert.ToInt32(sPort);

                lock (m_SockHdl)
                {
                    if (m_Quit)
                    {
                        return false;
                    }
                    if (m_TcpCli != null)
                        m_TcpCli.Close();
                    m_TcpCli = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    m_TcpCli.NoDelay = true;

                    m_TcpCli.Connect(m_IP, m_Port);

                    if (m_TcpCli.Connected)
                    {
                        if (OnBaseConnectEv != null)
                        {
                            //System.Diagnostics.Debug.WriteLine("==========> event On Connect ");
                            OnBaseConnectEv(this);
                        }
                    }

                    return m_TcpCli.Connected;
                }
            }
            catch (SocketException)
            {
                if (OnExceptionEv != null)
                    OnExceptionEv(String.Format("Connection to {0}:{1} fail! ", m_IP, m_Port));
                return false;
            }
            catch (Exception ex)
            {
                if (OnExceptionEv != null)
                    OnExceptionEv(ex.Message.ToString() + "[" +
                                  ex.StackTrace.ToString() + "]");
                return false;
            }
        }

        private void _UpdateLastRecvTime()
        {
            m_LastRecvTime = DateTime.Now;
        }

        private void _UpdateLastSendTime()
        {
            m_LastSendTime = DateTime.Now;
        }
        #endregion

        #region --  private work flow  --
        #region doTimeWork
        private void doTimeWork()
        {
            try
            {
                DateTime now = DateTime.Now;
                while (!m_Quit)
                {
                    Thread.Sleep(m_TimeInterval);
//System.Diagnostics.Debug.WriteLine("m_Quit = " + m_Quit.ToString());
                    #region check connection alive
                    // Connection thread 執行中
                    if (m_connecting_flag)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }
                    //如果沒連上, 再連一次  2013-11-18
                    if (!IsConnected)
                    {
                        if (m_AutoReConnect)
                        {
                            try
                            {
                                if (!_Connect(m_IP, m_Port.ToString()))
                                {
                                    continue;
                                }
//System.Diagnostics.Debug.WriteLine("_Connect complete");
                            }
                            catch (Exception)
                            {   // can't connect , retry
//System.Diagnostics.Debug.WriteLine("Connect Exception");
                                Thread.Sleep(5000);
                            }
                        }
                        else
                            continue;
                    }
                    #endregion

                    if (m_TcpCli != null)
                    {
                        //lock (m_TcpCli)
                        lock(m_SockHdl)
                        {
                            if (m_TcpCli != null)
                            {
                                if (m_TcpCli.Connected)
                                {
                                    if (OnTimerEv != null)
                                        OnTimerEv(m_Obj, m_LastRecvTime, m_LastSendTime);
                                }
                            }
                        }
                    }
                }
            }
            catch (ThreadInterruptedException)
            {
#if DEBUG_PRINT
                System.Diagnostics.Debug.WriteLine("client timer stop exception interrupt");
#endif
                return;
            }
            catch (ThreadAbortException)
            {
#if DEBUG_PRINT
                System.Diagnostics.Debug.WriteLine("client timer stop exception abort");
#endif
                return;
            }
            catch(Exception ex)
            {
#if DEBUG_PRINT
                System.Diagnostics.Debug.WriteLine("client timer stop exception");
#endif
                if (OnExceptionEv != null)
                    OnExceptionEv(ex.Message.ToString());
            }
        }
        #endregion

        #region doRcvWork
        private void doRcvWork()
        {
            IList read = new ArrayList();
            IList error = new ArrayList();
            int sleep_ms = 1000;
            int sleep_us = m_OneSec_us * 1;
            MemoryStream mStream = new MemoryStream(2048);

            while (!m_Quit)
            {
                try
                {
                    if (m_Quit)
                        return;

                    //mStream.Seek(0, SeekOrigin.Begin);
                    read.Clear();
                    error.Clear();
                    if (!IsConnected)
                    {
                        Thread.Sleep(sleep_ms);
                        continue;
                    }

                    read.Add(m_TcpCli);
                    error.Add(m_TcpCli);
                    try
                    {
                        Socket.Select(read, null, error, sleep_us);
                        
                    }
                    catch (SocketException)
                    {
                        continue;
                    }
                    if (read.Count > 0)
                    {
                        #region receive data 
                        //if (m_TcpCli.Available == 0)
                        if (AvailableDataSz == 0)
                        {
                            if (m_TcpCli.Connected)
                            {
                                _Disconnect();
                                continue;
                            }
                        }
                        else if (AvailableDataSz < 0)
                        {
                            continue;
                        }
                        else
                        {
                            if (OnBaseDataArriveEv != null)
                                OnBaseDataArriveEv(this);
                            else
                            {
                                if (_recv(ref mStream, m_TcpCli.Available) < 0)
                                    _Disconnect();
                                // 取回不使用, 直接丟掉
                            }
                        }
                        #endregion
                    }

                    if (error.Count > 0)
                    {
                        _Disconnect();
                    }
                }
                catch (ThreadInterruptedException)
                {
#if DEBUG_PRINT
                    System.Diagnostics.Debug.WriteLine("client recv stop exception interrupt");
#endif
                    m_Quit = true;
                }
                catch (ThreadAbortException)
                {
#if DEBUG_PRINT
                    System.Diagnostics.Debug.WriteLine("client recv stop exception abort");
#endif
                    m_Quit = true;
                }
                catch (Exception ex)
                {
#if DEBUG_PRINT
                    System.Diagnostics.Debug.WriteLine("client recv stop exception");
#endif
                    if (OnExceptionEv != null)
                    {
                        OnExceptionEv(ex.Message);
                    }
                    //else
                    //{
                    //    //throw new Exception(ex.Message.ToString() + "[" + ex.StackTrace.ToString() + "]");
                    //}
                    throw new Exception(ex.Message.ToString() + "[" + ex.StackTrace.ToString() + "]");
                }
            }
        }
        #endregion
        #endregion

        #region --  public function  --
        /// <summary>
        /// assign user object to this oebject.
        /// </summary>
        /// <param name="obj"></param>
        public void SetArgvObject(ref object obj)
        {
            m_Obj = obj;
        }

        #region Connection close
        /// <summary>
        /// Close connection and stop all background threads
        /// </summary>
        public bool Disconnect()
        {
            if (m_connecting_flag)
                return false;

            m_Quit = true;
            _Disconnect();

            return true;
            //_Stop();
        }
        public void InternalDisconnect()
        {
            _Disconnect();
        }
        #endregion

        #region Connection Open
        /// <summary>
        /// Start connection and start all background threads
        /// </summary>
        /// <param name="sIP"></param>
        /// <param name="sPort"></param>
        /// <returns></returns>
        public bool Connect(string sIP, string sPort)
        //protected bool Connect(string sIP, string sPort)
        {
            #region mark
            //bool bRC = false;
            //if (m_Quit)
            //{
            //    _Stop();
            //    m_Quit = false;
            //}

            //bRC = _Connect(sIP, sPort);
            //_Start();
            //return bRC;
            #endregion

            bool bRC = true;

            if (m_connecting_flag)
            {
                return false;
            }

            lock (m_SockHdl)
            {
                if (m_TcpCli != null)
                    if (m_TcpCli.Connected) 
                        return false;
            }
                        

            Thread thdConnect = new Thread(Thd_Connect_Work);
            thdConnect.IsBackground = true;

            _Stop();
            m_Quit = false;

            //bRC = _Connect(sIP, sPort);
            thdConnect.Start(String.Format("{0}:{1}", sIP, sPort));

            _Start();

            return bRC;
        }
        #endregion

        public void StopAllThd()
        {
            _Stop();
        }

        #region TCPSend
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="byteMsg"></param>
        ///// <returns></returns>
        //public int TCPSend(ref byte[] byteMsg)
        //{
        //    int iSnd = _send(ref byteMsg);
        //    if (iSnd > 0)
        //        _UpdateLastWorkTime();

        //    if (OnSndDataDumpEv != null)
        //        OnSndDataDumpEv(byteMsg);
        //    return iSnd;
        //}

        /// <summary>
        /// send data to remote server
        /// </summary>
        /// <param name="byteMsg">data</param>
        /// <param name="bUpdLastWorkTime">update the last work time, for internal timeout use</param>
        /// <returns></returns>
        public int TCPSend(byte[] byteMsg)
        {
            int iSnd = _send(ref byteMsg);
            if (iSnd > 0)
                _UpdateLastSendTime();

            if (OnSndDataDumpEv != null)
                OnSndDataDumpEv(byteMsg);
            return iSnd;
        }

        #endregion

        #region TCPRecv
        public int TCPRecv(ref MemoryStream mStream, int iLen, int timeout)
        {
            int iRcv = 0; ;
            try
            {
                lock (m_select_fd_list)
                {
                    m_select_fd_list.Clear();
                    m_select_fd_list.Add(m_TcpCli);
                    Socket.Select(m_select_fd_list, null, null, timeout * m_OneSec_us);
                    if (m_select_fd_list.Count > 0)
                    {
                        iRcv = _recv(ref mStream, iLen);
                        if (iRcv == iLen)
                            _UpdateLastRecvTime();

                        if (OnRcvDataDumpEv != null)
                            OnRcvDataDumpEv(mStream.ToArray());
                    }
                }
                return iRcv;
            }
            catch (Exception ex)
            {
                if (OnExceptionEv != null)
                    OnExceptionEv(String.Format("TCPRecv : {0} [{1}]",
                        ex.Message.ToString(), ex.StackTrace.ToString()));

                return -1;
            }
            
        }

        public int TCPRecvUntil(ref MemoryStream mStream, int iLen)
        {
            int iRcv = 0; ;
            int iNeedRcv = iLen;
            try
            {
                lock (m_select_fd_list)
                {
                    while (iNeedRcv > 0)
                    {
                        m_select_fd_list.Clear();
                        m_select_fd_list.Add(m_TcpCli);
                        Socket.Select(m_select_fd_list, null, null, 10 * m_OneSec_us);
                        if (m_select_fd_list.Count > 0)
                        {
                            iRcv = _recv(ref mStream, iNeedRcv);
                            if (iRcv < 0)
                                return -1;
                            iNeedRcv -= iRcv;
                        }
                    }
                    _UpdateLastRecvTime();

                    if (OnRcvDataDumpEv != null)
                        OnRcvDataDumpEv(mStream.ToArray());
                }
                return iLen;
            }
            catch (Exception ex)
            {
                if (OnExceptionEv != null)
                    OnExceptionEv(String.Format("TCPRecvUntil : {0} [{1}]",
                        ex.Message.ToString(), ex.StackTrace.ToString()));
                return -1;
            }
        }
        #endregion

        #endregion
    }
}