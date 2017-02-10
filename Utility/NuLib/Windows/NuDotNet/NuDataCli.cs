using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

/* *
 * TODO : 目前需要修正的情況是, 
 *        1. 由於採用blocking mode, send 也需要設定timeout
 *        2. 太久沒收到Server端HBT, 沒有斷線
 *        由於沒有設定send timeout時間, 又沒有判斷多久沒收到Server端hbt
 *        所以根本不會由Client端主動發起斷線動作
 *        需修改為
 * */
/* &
 * 2013-04-24 新增 OnError事件 (Server送Error Message下來)
 * 
 * */

namespace NuDotNet.Net
{

    public static class ConnectStatus
    {
        public const string Connect = "Connect";
        public const string Disconnect = "Disconnect";
        public const string Login = "Login";
    }
    public class NuDataCli : IDisposable
    {
        #region --  private items  --
        private static string m_Connect = ConnectStatus.Connect;
        private static string m_Disconnect = ConnectStatus.Disconnect;
        private static string m_Login = ConnectStatus.Login;
        private string m_Status = m_Disconnect;
        private bool bDisposed = false;
        private Thread m_RcvThd;
        private Thread m_SndThd;
        private Socket m_TcpCli;
        private bool m_LoginFlag;
        private byte[] m_buf = new byte[512];
        private int m_buf_sz = 512;
        private int m_HBTInterval;
        private bool Quit;
        private int m_OneSec = 1000000;   // micro sec
        private DateTime m_LastWorkTime = DateTime.Now;

        private string m_IP;
        private int m_Port;

        private bool m_AutoReConnect = false;

        #region protocal 
        private string mp_local_hdr_id = "";
        private string mp_remote_hdr_id = "";
        private string mp_login_id = "";
        #endregion


        private readonly object m_SockSndHdl = new object();
        private readonly object m_SockRcvHdl = new object();
        #endregion

        #region --  public delegate / event  --
        public delegate void ConnectionStatusChange(string Status);
        public event ConnectionStatusChange OnConnStatusChangeEv;
        public delegate bool OnConnectOn(object sender, EventArgs e);
        public event RcvDataHandler OnDataArriveEv;
        public delegate void OnDisconnect(object sender);
        public event OnDisconnect OnDisconnectEv;
        public delegate void RcvDataHandler(object sender, RcvDataArgs e);
        public event OnConnectOn OnConnectEv;
		//public delegate void OnException(string Msg);
		//public event OnException OnExceptionEv;
        public delegate void OnHBT(string sMsg);
        public event OnHBT OnHBTEv;
        public delegate void OnError(object sender, RcvDataArgs e);
        public event OnError OnErrorEv;

        public delegate void RcvDataDump(object sender, RcvDataArgs e);
        public event RcvDataDump OnDataDumpEv;
        public delegate void SndDataDump(string Msg);
        public event SndDataDump OnDataSndDumpEv;        
        #endregion

        #region --  private string enum  --
        #endregion

        #region --  construct / destruct  --
        private void internal_init()
        {
            Quit = false;
            //m_TcpCli = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_TcpCli = null;
            m_LoginFlag = false;
            mp_local_hdr_id = String.Format("{0:00000}", DateTime.Now.Ticks % 100000);
        }
        public NuDataCli()
        {
            internal_init();
            HBTInterval = 30000;  // 30 sec
            mp_login_id = "";
        }

        public NuDataCli(int HBTIvl)
        {
            HBTInterval = HBTIvl;
            internal_init();
        }

        public NuDataCli(String LoginID, int HBTIvl)
        {
            HBTInterval = HBTIvl;
            mp_login_id = LoginID;
            internal_init();
        }

        ~NuDataCli()
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

            if (IsDisposing)
            {
                lock (m_TcpCli)
                {
                    if (m_TcpCli != null)
                    {
                        if (m_TcpCli.Connected)
                        {
                            _sendLogout();
                            if (OnConnStatusChangeEv != null)
                                OnConnStatusChangeEv(m_Disconnect);
                            m_Status = m_Disconnect;

                            m_TcpCli.Close();
                            m_TcpCli = null;
                        }
                    }
                }
                _Stop();
                
                Quit = true;
            }

            bDisposed = true;
        }
        #endregion

        #region --  property  --
        /// <summary>
        /// Heartbeat interval
        /// </summary>
        public int HBTInterval { get { return m_HBTInterval; } set { m_HBTInterval = value; } }
        /// <summary>
        /// connection status (readonly)
        /// </summary>
        public bool IsConnected { get { return (m_TcpCli == null) ? false : m_TcpCli.Connected; } } 
        /// <summary>
        /// login flag
        /// </summary>
        public bool LoginComplete { get { return m_LoginFlag; } set { m_LoginFlag = value; } }
        /// <summary>
        /// login id
        /// </summary>
        public string LoginID { get { return mp_login_id; } set { mp_login_id = value; } }
        /// <summary>
        /// remote server ip
        /// </summary>
        public string ConnectIP { get { return m_IP; } }
        /// <summary>
        /// remote server port
        /// </summary>
        public int ConnectPort { get { return m_Port; } }
        /// <summary>
        /// current session status
        /// </summary>
        public string CurrentStatus { get { return m_Status; } }
        /// <summary>
        /// auto reconnect or not
        /// </summary>
        public bool AutoReConnect { get { return m_AutoReConnect; } set { m_AutoReConnect = value; } }
        #endregion

        #region --  Method  --

        #region Connection close
        public void Disconnect()
        {
            _Stop();
            if (mp_remote_hdr_id != "")
                _Disconnect();
        }
        #endregion

        #region Connection Open
        public bool Connect(string sLoginID, string sIP, string sPort)
        {
            bool bRC = false;
            mp_login_id = sLoginID;
            bRC = _Connect(sIP, sPort);
            if (bRC)
                _Start();
            return bRC;
        }

        public bool Connect(string sIP, string sPort)
        {
            bool bRC = false;
            bRC = _Connect(sIP, sPort);
            if (bRC)
                _Start();
            return bRC;
        }
        #endregion

        #region TCPSend
        public bool TCPSend(string sMsg)
        {
            int iSnd = 0;
            StringBuilder sbData = new StringBuilder(1024);
            int sMsgLen = ASCIIEncoding.Default.GetByteCount(sMsg);
            sbData.Append(NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.Msg, sMsgLen));
            if (sbData.Length == 0)
                return false;
            sbData.Append(sMsg);

            if (OnDataSndDumpEv != null)
            {
                OnDataSndDumpEv(sbData.ToString());
            }

            byte[] bMsg = Encoding.Default.GetBytes(sbData.ToString());
            iSnd = _send(ref bMsg);

            if (OnDataSndDumpEv != null)
            {
                OnDataSndDumpEv(String.Format("Send : {0}", iSnd));
            }

            if (iSnd != bMsg.Length)
                return false;
            return true;
        }
        #endregion

        #endregion

        #region --  private function  --

        #region thread Start
        private void _Start()
        {
            if (m_SndThd == null)
                m_SndThd = new Thread(doHBTWork);
            if (m_SndThd.IsAlive)
                return;
            m_SndThd.IsBackground = true;
            m_SndThd.Start();

            if (m_RcvThd == null)
                m_RcvThd = new Thread(doRcvWork);
            if (m_RcvThd.IsAlive)
                return;
            m_RcvThd.IsBackground = true;
            m_RcvThd.Start();
        }
        #endregion

        #region thread Stop
        private void _Stop()
        {
            if (m_RcvThd != null)
            {
                if (m_RcvThd.IsAlive)
                {
                    m_RcvThd.Abort();
                    m_RcvThd.Join();
                }
                m_RcvThd = null;
            }

            if (m_SndThd != null)
            {
                if (m_SndThd.IsAlive)
                {
                    m_SndThd.Abort();
                    m_SndThd.Join();
                }
                m_SndThd = null;
            }
        }
        #endregion

        private int _recv(ref StringBuilder sb, int size)
        {
            
            int iByte = 0;
            int maxRcvByte = 0;
            sb.Length = 0;

            try
            {
                //lock (m_TcpCli)
                lock (m_SockRcvHdl)
                {
                    while (size > 0)
                    {
                        maxRcvByte = (size > m_buf_sz) ? m_buf_sz : size;
                        iByte = m_TcpCli.Receive(m_buf, maxRcvByte, SocketFlags.None);
                        if (iByte == 0 && sb.Length == 0)
                            return 0;
                        size -= iByte;
                        sb.Append(Encoding.Default.GetString(m_buf), 0, iByte);
                    }
                }
            }
            catch (Exception)
            {				
                return -1;
            }
            return sb.ToString().Length;
        }

        private int _send(ref byte[] sMsg)
        {
            int iSnd = 0;
            //lock (m_TcpCli)
            lock (m_SockSndHdl)
            {
                //if (m_TcpCli.Connected)
                //{
                    try
                    {
                        iSnd = m_TcpCli.Send(sMsg);
                    }
                    catch (Exception )
                    {
                        _Disconnect();
                    }
                //}
            }

            return iSnd;
        }

        private bool _sendHBT()
        {
            string sHBT = NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.HBT, 0);
            int iSnd = 0;
            byte [] bMsg =  Encoding.Default.GetBytes(sHBT);

            if (sHBT == "")
                return false;
            if (OnDataSndDumpEv != null)
            {
                OnDataSndDumpEv(sHBT);
            }

            iSnd = _send(ref bMsg);
            if (iSnd != sHBT.Length)
            {
                _Disconnect();
                return false;
            }

            _UpdateLastWorkTime();

            if (OnHBTEv != null)
            {
                OnHBTEv(string.Format(" < HBT [{0}][{1}]", m_IP, sHBT));
            }

            return true;
        }

        private bool _sendLogout()
        {
            if (mp_remote_hdr_id == "")
                return true;
			string sLogout = NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.Logout, 0);
            int iSnd = 0;
			byte[] bMsg = Encoding.Default.GetBytes(sLogout);

            if (OnDataSndDumpEv != null)
            {
				OnDataSndDumpEv(sLogout);
            }

            iSnd = _send(ref bMsg);
			if (iSnd != sLogout.Length)
                return false;
            return true;
        }

        private void _Disconnect()
        {
            lock (m_TcpCli)
            {
                if (m_TcpCli == null)
                    return;

                if (m_TcpCli.Connected)
                {
                    if (_sendLogout())
                    {
                        m_TcpCli.Shutdown(SocketShutdown.Both);
                        m_TcpCli.Disconnect(false);
                        //m_TcpCli.Close();
                    }
                }

                if (OnConnStatusChangeEv != null)
                    OnConnStatusChangeEv(m_Disconnect);
                m_Status = m_Disconnect;

                m_LoginFlag = false;

                //m_TcpCli = null;
                //m_TcpCli = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
            }
        }

        private bool _Connect(string sIP, string sPort)
        {
            IAsyncResult IAsync = null;
            try
            {
                m_IP = sIP;
                m_Port = Convert.ToInt32(sPort);
                m_Status = m_Disconnect;
                if (m_TcpCli != null)
                    m_TcpCli.Close();
                m_TcpCli = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_TcpCli.ReceiveTimeout = 5000; /// 5 sec
                IAsync = m_TcpCli.BeginConnect(m_IP, m_Port, new AsyncCallback(_ConnectCB), this);
                if (IAsync.AsyncWaitHandle.WaitOne(5000, true))
                {
                    if (m_TcpCli.Connected)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    m_TcpCli.EndConnect(IAsync);
                }
                return false;
            }
            catch (Exception ex)
            {
                //if (IAsync != null)
                //    m_TcpCli.EndConnect(IAsync);
                //throw new Exception(String.Format("NuTCPClient : {0}", ex.Message.ToString()));
                return false;
            }
        }

        private void _ConnectCB(IAsyncResult asyncresult)
        {
            try
            {
                //if (m_TcpCli.Connected)
                if (m_Status == m_Disconnect)
                {
                    m_Status = m_Connect;
                    m_TcpCli.NoDelay = true;
                    m_TcpCli.ReceiveTimeout = m_HBTInterval;

                    m_Status = m_Connect;

                    if (OnConnStatusChangeEv != null)
                        OnConnStatusChangeEv(m_Status);

                    if (OnConnectEv != null)
                    {
                        OnConnectEv(this, EventArgs.Empty);
                    }

                    _SendLogin();
                    m_TcpCli.EndConnect(asyncresult);
                }
                return;
            }
            catch (InvalidOperationException)
            {
                m_Status = m_Disconnect;
                //m_TcpCli.Close();
                //m_TcpCli = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _Disconnect();
                return;
            }
            catch (Exception ex)
            {
                //throw new Exception(String.Format("NuTCPClient : {0}", ex.Message.ToString()));
                return;
            }
        }

        private bool _SendLogin()
        {
            StringBuilder sbData = new StringBuilder(1024);
            int iRC = 0;
            try
            {
                if (m_TcpCli.Connected)
                {
                    // do login
                    if (mp_login_id.Length > 0)
                    {
                        mp_local_hdr_id = String.Format("{0:00000}", int.Parse(mp_local_hdr_id) + 1);
                        sbData.Append(NuDataProtocol.genHdr(mp_local_hdr_id, NuMsgType.Login, ASCIIEncoding.Default.GetByteCount(LoginID)));
                        sbData.Append(LoginID);

                        byte[] bMsg = Encoding.Default.GetBytes(sbData.ToString());
                        iRC = _send(ref bMsg);

                        if (OnDataSndDumpEv != null)
                        {
                            OnDataSndDumpEv(String.Format("Send Login [{0}][{1}]", iRC, sbData.ToString()));
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception(String.Format("NuTCPClient : {0}", ex.Message.ToString()));
                return false;
            }
        }

        public void _UpdateLastWorkTime()
        {
            m_LastWorkTime = DateTime.Now;
        }
        #endregion

        #region --  private work flow  --
        #region doHBTWork
        private void doHBTWork()
        {
            try
            {
                DateTime now = DateTime.Now;
                double TimeGap = 0.0;
                while (true)
                {
                    Thread.Sleep(1000);  /* sleep 1 sec */
                    
                    if (!m_TcpCli.Connected)
                    {
                        m_LoginFlag = false;

                        if (m_AutoReConnect)
                        {
                            try
                            {
                                _Connect(m_IP, m_Port.ToString());
                            }
                            //catch (Exception ex)
                            catch (Exception)
                            {   // can't connect , retry
                                Thread.Sleep(5000);
                            }
                        }
                        else
                        {
                            if (OnDisconnectEv != null)
                                OnDisconnectEv(this);
                        }
                    }

                    now = DateTime.Now;
                    TimeGap = (now - m_LastWorkTime).Seconds - (m_HBTInterval/1000);
                    if (TimeGap >= -1)
                    {
                        lock (m_TcpCli)
                        {
                            if (m_TcpCli.Connected)
                            {
                                if (!m_LoginFlag)
                                    continue;

                                _sendHBT();
                            }
                        }
                    }
                }
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
            catch (ThreadAbortException)
            {
                return;
            }
        }
        #endregion

        #region doRcvWork
        private void doRcvWork()
        {
            int iBodyLen = 0;
            IList read = new ArrayList();
            IList error = new ArrayList();
            StringBuilder sbBody = new StringBuilder(1024);
            StringBuilder sbHdr = new StringBuilder(11);
            while (true)
            {
                try
                {
                    if (Quit)
                        return;

                    sbBody.Length = 0;
                    sbHdr.Length = 0;
                    read.Clear();
                    error.Clear();
                    if (!m_TcpCli.Connected)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    read.Add(m_TcpCli);
                    error.Add(m_TcpCli);
                    Socket.Select(read, null, error, m_OneSec);
                    if (read.Count > 0)
                    {
                        #region receive data 
                        if (m_TcpCli.Available == 0)
                        {
                            _sendHBT();
                            if (!m_TcpCli.Connected)
                            {
                                if (OnDisconnectEv != null)
                                    OnDisconnectEv(this);
                                _Disconnect();
                                continue;
                            }
                        }
                        else
                        {
                            #region handle readable client 
                            while (m_TcpCli.Available > 0)
                            {
                                #region header handle
                                /* header format -------------------------------
                                 * Length    9(4)
                                 * MsgType   X(1)
                                 * ID        X(5)
                                 * End       X(1)   \001
                                 * ---------------------------------------------  */
                                if (_recv(ref sbHdr, 11) <= 0)
                                {   //TODO: data error , should handler 
                                    _Disconnect();
                                    break;
                                }

                                if (sbHdr[sbHdr.Length - 1] != NuDataProtocol.protocol_end)
                                {
                                    _Disconnect();
                                    break;
                                }

                                //iBodyLen = int.Parse(sbHdr.ToString().Substring(0, 4));
                                if (!int.TryParse(sbHdr.ToString().Substring(0, 4), out iBodyLen))
                                {
                                    _Disconnect();
                                    break;
                                }
                                #endregion

                                #region body handle
                                if (iBodyLen > 0)
                                {
                                    if (_recv(ref sbBody, iBodyLen) < 0)
                                    {   //TODO: data error , should handler 
                                        _Disconnect();
                                        break;
                                    }
                                }
                                #endregion

                                #region for debug
                                //release must mark.
#if DEBUG
                                if (OnDataDumpEv != null)
                                {
                                    RcvDataArgs args = new RcvDataArgs(sbHdr.ToString(), sbBody.ToString());
                                    OnDataDumpEv(this, args);
                                }
#endif
                                #endregion

                                #region parsing header
                                switch (sbHdr[4])
                                {
                                    case NuMsgType.Login:  // login success 
                                        mp_remote_hdr_id = sbHdr.ToString().Substring(5, 5);
                                        m_LoginFlag = true;

                                        if (OnConnStatusChangeEv != null)
                                            OnConnStatusChangeEv(m_Login);
                                        m_Status = m_Login;

                                        _sendHBT();
                                        break;
                                    case NuMsgType.Logout:
                                        //Disconnect();
                                        _Disconnect();
                                        break;
                                    case NuMsgType.HBT:
                                        if (OnHBTEv != null)
                                        {
                                            OnHBTEv(string.Format(" > HBT [{0}][{1}]", m_IP, sbHdr.ToString()));
                                        }
                                        break;
                                    case NuMsgType.TestReq:
                                        _sendHBT();
                                        break;
                                    case NuMsgType.Msg:
                                        if (mp_local_hdr_id == sbHdr.ToString().Substring(5, 5))
                                        {
                                            if (OnDataArriveEv != null)
                                            {
                                                RcvDataArgs args = new RcvDataArgs(sbHdr.ToString(), sbBody.ToString());
                                                OnDataArriveEv(this, args);
                                            }
                                        }
                                        //_UpdateLastWorkTime();
                                        break;
                                    case NuMsgType.Error:
                                        if (mp_local_hdr_id == sbHdr.ToString().Substring(5, 5))
                                        {
                                            if (OnErrorEv != null)
                                            {
                                                RcvDataArgs args = new RcvDataArgs(sbHdr.ToString(), sbBody.ToString());
                                                OnErrorEv(this, args);
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }

                    if (error.Count > 0)
                    {
                        _Disconnect();
                        if (OnDisconnectEv != null)
                            OnDisconnectEv(this);
                    }
                }
                catch (ThreadInterruptedException)
                {
                    return;
                }
                catch (ThreadAbortException)
                {
                    return;
                }
				//catch (Exception ex)
				//{
				//    if (OnExceptionEv != null)
				//    {
				//        OnExceptionEv(ex.Message);
				//    }
				//    return;
				//}
            }
        }
        #endregion
        #endregion
    }
}