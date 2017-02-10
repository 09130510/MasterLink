using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Data;
using NuDotNet.THDS;


namespace NuDotNet.Net
{
    
    //---------------------------------------------------------
    // NuSocketServer Node
    //---------------------------------------------------------
    #region Server client Node 
    /// <summary>
    /// NuSocketServer 的 client 物件
    /// </summary>
    public class NuSockSvrClientNode : IDisposable
    {
        #region -- private items --
        private Socket m_client = null;
        private DateTime m_LastRecvTime;
        private DateTime m_LastSendTime;
        private int m_hbtItv = 30;

        private byte[] m_buf;
        private int m_bufsz;

        private List<Socket> m_rlist = null;

        private object m_obj = null;

        private readonly object m_SockSndHdl = new object();
        private readonly object m_SockRcvHdl = new object();

        private bool m_IsAlive = true;
        private DateTime m_LastNotAliveTime = DateTime.Now;
        #endregion

        #region --  construct / destruct  --
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="accept_socket"></param>
        /// <param name="buf_sz"></param>
        /// <param name="hbtItv"></param>
        public NuSockSvrClientNode(ref Socket accept_socket, int buf_sz, int hbtItv)
        {
            m_client = accept_socket;
            m_client.NoDelay = true;
            m_client.Blocking = false;
            
            m_LastRecvTime = m_LastSendTime = DateTime.Now;
            if (hbtItv > 0)
                m_hbtItv = hbtItv;          // sec

            m_buf = new byte[buf_sz];
            m_bufsz = buf_sz;

            m_rlist = new List<Socket>();
            m_rlist.Add(accept_socket);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (m_client != null)
                m_client.Dispose();
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }
        /// <summary>
        /// 解構式
        /// </summary>
        ~NuSockSvrClientNode() { }
        #endregion

        #region -- property --
        /// <summary>
        /// Client Socket object
        /// </summary>
        public Socket Client { get { return m_client; } }
        /// <summary>
        /// Client IP
        /// </summary>
        public string ClientIP { get { return ((IPEndPoint)m_client.RemoteEndPoint).Address.ToString(); } }
        /// <summary>
        /// Client Port
        /// </summary>
        public string ClientPort { get { return ((IPEndPoint)m_client.RemoteEndPoint).Port.ToString(); } }
        /// <summary>
        /// Cleint 
        /// </summary>
        public Int32 ClientFD { get { return m_client.Handle.ToInt32(); } }
        /// <summary>
        /// 最後送出資料的時間
        /// </summary>
        public DateTime LastSendTime { get { return m_LastSendTime; } set { m_LastSendTime = value; } }
        /// <summary>
        /// 最後收到資料的時間
        /// </summary>
        public DateTime LastRecvTime { get { return m_LastRecvTime; } set { m_LastRecvTime = value; } }
        /// <summary>
        /// HeartBeat 間隔
        /// </summary>
        public int HBTInterval { get { return m_hbtItv; } set { m_hbtItv = value; } }
        public object GetObject { get { return m_obj; } }
        public bool IsAlive { get { return m_IsAlive; } }
        
        public int AvailableDataSz
        {
            get
            {
                if (m_client == null)
                    return -1;
                else if (!m_client.Connected)
                    return -1;
                else
                    return m_client.Available;
            }
        }
        public DateTime LastNotAliveTime { get { return m_LastNotAliveTime; } }
        #endregion

        #region -- private function --
        private void _UpdateLastSendTime()
        {
            m_LastSendTime = DateTime.Now;
        }
        private void _UpdateLastRecvTime()
        {
            m_LastRecvTime = DateTime.Now;
        }
        private void _SetNotAlive()
        {
            if (m_IsAlive)
            {
                m_IsAlive = false;
                m_LastNotAliveTime = DateTime.Now;
            }
        }
        #endregion

        #region -- public function --
        public void Disconnect()
        {
            m_client.Shutdown(SocketShutdown.Both);
            m_client.Disconnect(false);
            _SetNotAlive();
        }
        public void SetObject(object obj)
        {
            m_obj = obj;
        }

        /// <summary>
        /// NuSockSvrClientNode receive function
        /// </summary>
        /// <param name="mStream"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int SockRecv(ref MemoryStream mStream, int length)
        {
            return SockRecv(ref mStream, length, 100);
        }

        public int SockRecv(ref MemoryStream mStream, int length, int TimeMS)
        {
            int iByte = 0;
            int MaxRcvPart = 0;
            int iRetryCnt = 0;


            mStream.SetLength(0);
            try
            {
                lock (m_SockRcvHdl)
                {
                    while (length > 0)
                    {
                        List<Socket> rlist = new List<Socket>(m_rlist);
                        List<Socket> errlist = new List<Socket>(m_rlist);

                        MaxRcvPart = (length > m_bufsz) ? m_bufsz : length;

                        if (iRetryCnt++ > 10)
                        {
                            _SetNotAlive();
                            return -1;
                        }

                        Socket.Select(rlist, null, errlist, NuVariable.OneMs_us * TimeMS);
                        if (errlist.Count > 0)
                        {
                            _SetNotAlive();
                            return -1;
                        }

                        if (rlist.Count <= 0)
                        {
                            return (int)mStream.Length;
                        }
                        else
                        {
                            iByte = m_client.Receive(m_buf, MaxRcvPart, SocketFlags.None);
                            if (iByte == 0 && mStream.Length == 0)
                                return 0;
                            else if (iByte < 0)
                            {
                                _SetNotAlive();
                                return -1;
                            }

                            length -= iByte;
                            mStream.Write(m_buf, 0, iByte);
                        }
                    }
                }
            }
            catch (Exception)
            {
                _SetNotAlive();
                return -1;
            }

            _UpdateLastRecvTime();

            return (int)mStream.Length;
        }

        /// <summary>
        /// NuSockSvrClientNode send function
        /// </summary>
        /// <param name="byteMsg"></param>
        /// <returns></returns>
        public int SockSend(byte[] byteMsg)
        {
            int iSndCnt = 0;
            lock (m_SockSndHdl)
            {
                if (m_client.Connected)
                {
                    try
                    {
                        iSndCnt = m_client.Send(byteMsg);
                        if (iSndCnt <= 0)
                        {
                            _SetNotAlive();
                            return -1;
                        }
                    }
                    catch
                    {
                        _SetNotAlive();
                        return -1;
                    }
                }
                else
                {
                    _SetNotAlive();
                    return -1;
                }
            }
            
            _UpdateLastSendTime();
            return iSndCnt;
        }

        public int SockSend(byte[] byteMsg, int TimeMS)
        {
            int iSndCnt = 0;
            List<Socket> socklst = new List<Socket>();
            List<Socket> sockerrlst = new List<Socket>();

            lock (m_SockSndHdl)
            {
                socklst.Add(m_client);
                sockerrlst.Add(m_client);
                try
                {
                    Socket.Select(null, socklst, sockerrlst, TimeMS * 1000);
                    if (socklst.Count > 0)
                    {
                        iSndCnt = m_client.Send(byteMsg);
                        if (iSndCnt <= 0)
                        {
                            _SetNotAlive();
                            return -1;
                        }
                    }
                    else if (sockerrlst.Count > 0)
                    {
                        _SetNotAlive();
                        return -1;
                    }
                    else
                    {
                        _SetNotAlive();
                        return -1;
                    }
                }
                catch
                {
                    _SetNotAlive();
                    return -1;
                }
            }

            _UpdateLastSendTime();
            return iSndCnt;
        }

        #endregion

    }
    #endregion

    //---------------------------------------------------------
    // NuSocketServer 
    //---------------------------------------------------------
    #region Socket Server
    public class NuSocketServer
    {
        #region -- private items --
        bool m_work = true;
        Hashtable m_htMap = null;
        
        /// <summary>
        /// All client list
        /// </summary>
        public List<NuSockSvrClientNode> m_clients = null;
        List<Socket> m_socklst = null;
        HashSet<Socket> m_work_socklst = null;

        private IPAddress m_ip = null;
        int m_port = 0;
        Socket m_tcpsvr = null;
        int m_default_interval_sec = 30;
        int m_default_buffer_sz = 2048;

        private int m_delay_clear_sec = 3;
        Thread m_ThdListener = null;
        Thread m_ThdMain = null;

        // thread pool
        //List<Thread> m_thread_pool = new List<Thread>();
        //NuQueue<NuSockSvrClientNode> m_work_client_que = new NuQueue<NuSockSvrClientNode>(64);

        NuThreadPool m_threads = new NuThreadPool();
        #endregion 

        #region --  construct / destruct  --
        public NuSocketServer(string listen_ip, int listen_port)
        {
            m_ip = IPAddress.Parse(listen_ip);
            m_port = listen_port;

            m_htMap = new Hashtable();
            m_clients = new List<NuSockSvrClientNode>(5);
            m_socklst = new List<Socket>();
            
            m_work_socklst = new HashSet<Socket>();
        }
        ~NuSocketServer() { }
        #endregion

        #region -- private flow / function--
        private void _AddNode(NuSockSvrClientNode node)
        {
            lock (m_htMap)
                m_htMap.Add(node.Client.Handle, node);

            lock (m_clients)
                m_clients.Add(node);

            lock (m_socklst)
                m_socklst.Add(node.Client);
        }

        private void _RemoveNode(NuSockSvrClientNode node)
        {
            if (OnClientDisconnectEv != null)
                OnClientDisconnectEv(node);

            lock(m_socklst)
                m_socklst.Remove(node.Client);

            // 斷線時,可能還存在work list裡面
            // 若沒移除, 下次連上的user得到同一個Socket handler就會有問題
            lock (m_work_socklst)
                m_work_socklst.Remove(node.Client);

            lock(m_clients)
                m_clients.Remove(node);
            
            lock(m_htMap)
                m_htMap.Remove(node.Client.Handle);

            node.Dispose();
        }

        private void _Listener()
        {
            bool bRtn = true;
            NuSockSvrClientNode client_node = null;
            Socket sockfd = null;
            ArrayList acceptList = new ArrayList();
            DateTime checkTime = DateTime.Now;

            try
            {
                m_tcpsvr.Bind(new IPEndPoint(m_ip, m_port));
                m_tcpsvr.Listen(10);

                while (m_work)
                {
                    checkTime = DateTime.Now;

                    #region check connection accept
                    acceptList.Clear();
                    acceptList.Add(m_tcpsvr);
                    
                    Socket.Select(acceptList, null, null, 1000000);
                    for (int i = 0; i < acceptList.Count; i++)
                    {
                        bRtn = true;
                        sockfd = ((Socket)(acceptList[i])).Accept();
                        if (OnAcceptEv != null)
                        {
                            client_node = OnAcceptEv(ref sockfd, m_default_buffer_sz, m_default_interval_sec);
                        }
                        else
                            client_node = new NuSockSvrClientNode(ref sockfd, m_default_buffer_sz, m_default_interval_sec);

                        if (OnClientConnectEv != null)
                            bRtn = OnClientConnectEv(client_node);

                        if (bRtn)
                            _AddNode(client_node);
                        else
                        {
                            sockfd.Close();
                            sockfd.Dispose();
                        }
                    }
                    #endregion

                    #region check timeout
                    lock (m_clients)
                    {
                        for (int i = 0; i < m_clients.Count; i++)
                        {
                            NuSockSvrClientNode node = m_clients[i];
                            if (!node.IsAlive)
                            {
                                if ( (checkTime - node.LastNotAliveTime).TotalSeconds > 5)
                                    _RemoveNode(node);
                            }
                            else if (OnClientTimerEv != null)
                                OnClientTimerEv(node, node.LastRecvTime, node.LastSendTime);
                        }
                    }
                    #endregion
                }
            }
            catch (ThreadAbortException) { return; }
            catch (ThreadInterruptedException) { return; }
            catch (Exception ex)
            {
                string sException = ex.Message.ToString() + "[" +
                                  ex.StackTrace.ToString() + "]";

                if (OnExceptionEv != null)
                    OnExceptionEv(sException);

                //throw new Exception(sException);
            }
        }

        private void _RecvData()
        {
            List<Socket> rlst = null;
            List<Socket> errlst = null;
            NuSockSvrClientNode node = null;
            StringBuilder sbHdr = new StringBuilder(12);
            StringBuilder sbBody = new StringBuilder(512);

            try
            {
                while (m_work)
                {

                    if (m_socklst.Count == 0)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    lock (m_socklst)
                    {
                        rlst = new List<Socket>(m_socklst);
                        errlst = new List<Socket>(m_socklst);
                    }

                    try
                    {
                        Socket.Select(rlst, null, errlst, NuVariable.OneMs_us * 500);
                    }
                    catch (SocketException)
                    {
                        List<Socket> lst = new List<Socket>();
                        lock (m_socklst)
                        {
                            foreach (Socket s in m_socklst)
                            {
                                if (s.Connected==false)
                                    lst.Add(s);
                            }
                        }
                        foreach (Socket s in lst)
                        {
                            m_socklst.Remove(s);
                        }
                        continue;
                    }
                    

                    for (int i = 0; i < rlst.Count; i++)
                    {
                        Socket sockfd = (Socket)rlst[i];
                        if ((node = (NuSockSvrClientNode)m_htMap[sockfd.Handle]) == null)
                            continue;

                        //沒有資料就當作斷線處理
                        //System.Diagnostics.Debug.WriteLine(String.Format("Socket[{0}]{1}{2}", node.ClientFD, node.Client.Available, node.IsAlive));
                        
                        if (node.IsAlive && node.AvailableDataSz > 0)
                        {
                            lock (m_socklst)
                                m_socklst.Remove(node.Client);

                            lock (m_work_socklst)
                                m_work_socklst.Add(node.Client);

                            m_threads.Invoke(node, _DoClientWork);
                            
                        }
                        else if (!node.IsAlive || node.AvailableDataSz <= 0)
                        {
                            //System.Diagnostics.Debug.WriteLine((DateTime.Now - node.LastNotAliveTime).TotalSeconds);
                            if ((DateTime.Now - node.LastNotAliveTime).TotalSeconds >= m_delay_clear_sec)
                            {
                                _RemoveNode(node);
                            }
                            continue;

                        }
                    }

                    for (int i = 0; i < errlst.Count; i++)
                    {
                        Socket sockfd = (Socket)rlst[i];
                        if ((node = (NuSockSvrClientNode)m_htMap[sockfd.Handle]) == null)
                            continue;
                        _RemoveNode(node);
                    }
                }
            }
            catch (ThreadAbortException) { return; }
            catch (ThreadInterruptedException) { return; }
            catch (Exception ex)
            {
                string sException = ex.Message.ToString() + "[" +
                                  ex.StackTrace.ToString() + "]";

                System.Diagnostics.Debug.WriteLine(sException);
                if (OnExceptionEv != null)
                    OnExceptionEv(sException);

                //throw new Exception(sException);
            }
        }


        private void _DoClientWork(object oObj)
        {
            try
            {
                List<Socket> sock_lst = new List<Socket>();
                NuSockSvrClientNode node = (NuSockSvrClientNode)oObj;
                sock_lst.Clear();
                sock_lst.Add(node.Client);
                while (node.AvailableDataSz > 0)
                {
                    Socket.Select(sock_lst, null, null, NuVariable.OneMs_us * 50);
                    if (sock_lst.Count > 0)
                    {
                        if (OnClientDataArrivedEv != null)
                            OnClientDataArrivedEv(node);
                    }
                }

                lock (m_work_socklst)
                    m_work_socklst.Remove(node.Client);

                lock (m_socklst)
                    m_socklst.Add(node.Client);
                    
            }
            catch (ThreadAbortException ex) { throw ex; }
            catch (ThreadInterruptedException ex) { throw ex; }
            catch (Exception ex)
            {
                string sException = ex.Message.ToString() + "[" +
                  ex.StackTrace.ToString() + "]";

                System.Diagnostics.Debug.WriteLine("A- " + sException);
                if (OnExceptionEv != null)
                    OnExceptionEv(sException);

                //throw new Exception(sException);
            }
        }
        #endregion

        #region -- public event --
        public delegate NuSockSvrClientNode dlgSockServerOnAcceptEvent(ref Socket sockfd, int iDefaultBufSz, int iTimeIntervalSec);
        protected event dlgSockServerOnAcceptEvent OnAcceptEv;

        public delegate void dlgSockServerTimeOutEvent(NuSockSvrClientNode Client, DateTime LastRecvTime, DateTime LastSendTime);
        public delegate bool dlgSockServerEvent(NuSockSvrClientNode Client);
        /// <summary>
        /// Client 連上觸發
        /// </summary>
        protected event dlgSockServerEvent OnClientConnectEv;
        /// <summary>
        /// Client 斷線觸發
        /// </summary>
        protected event dlgSockServerEvent OnClientDisconnectEv;
        /// <summary>
        /// Client 每秒觸發
        /// </summary>
        protected event dlgSockServerTimeOutEvent OnClientTimerEv;
        /// <summary>
        /// Client 資料送達觸發
        /// </summary>
        protected event dlgSockServerEvent OnClientDataArrivedEv;

        public delegate void dlgSockServerLog(string sMsg);
        /// <summary>
        /// Server 發生 Exception
        /// </summary>
        public event dlgSockServerLog OnExceptionEv;
        /// <summary>
        /// Server dump 資料
        /// </summary>
        //public event dlgSockServerLog OnLogDumpEv;
        //public event dlgSockServerLog OnDEBUGLog;
        #endregion

        #region -- public property --
        public string ListenIP { get { return m_ip.ToString(); } }
        public int ListenPort { get { return m_port; } }
        #endregion

        #region -- public function --
        public void Start()
        {
            m_work = true;
            m_tcpsvr = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            if (m_ThdListener == null)
                m_ThdListener = new Thread(_Listener);

            if (!m_ThdListener.IsAlive)
            {
                m_ThdListener.IsBackground = true;
                m_ThdListener.Start();
            }

            if (m_ThdMain == null)
                m_ThdMain = new Thread(_RecvData);

            if (!m_ThdMain.IsAlive)
            {
                m_ThdMain.IsBackground = true;
                m_ThdMain.Start();
            }
        }

        public void Stop()
        {
            m_work = false;

            Thread.Sleep(10);

            if (m_ThdListener.IsAlive)
            {
                m_ThdListener.Abort();
                m_ThdListener.Join();
            }
            if (m_ThdMain.IsAlive)
            {
                m_ThdMain.Abort();
                m_ThdMain.Join();
            }

            m_threads.Dispose();

            foreach (NuSockSvrClientNode client in m_clients)
            {
                client.Disconnect();
            }
            m_clients.Clear();
            m_tcpsvr.Close();
            m_tcpsvr.Dispose();

        }

        protected void Broadcast(ref StringBuilder sbMsg)
        {
            int iRtn = 0;
            NuSockSvrClientNode node = null;
            lock (m_clients)
            {
                for (int i = 0; i < m_clients.Count; i++)
                {
                    node = m_clients[i];
                    iRtn = node.SockSend(Encoding.Default.GetBytes(sbMsg.ToString()));
                    if (iRtn < 0)
                    {
                        System.Diagnostics.Debug.WriteLine("remove node");
                        _RemoveNode(node);

                        if (OnExceptionEv != null)
                            OnExceptionEv(String.Format("{0}-{1}-{2} socket send fail! disconnect", node.ClientFD, node.ClientIP));
                    }
                    
                }
            }
        }

        public void CloseClinet(NuSockSvrClientNode node)
        {
            node.Disconnect();
            //_RemoveNode(node);
            //lock (m_work_socklst)
            //    m_work_socklst.Remove(node.Client);
            
        }

        protected void Invoke(object oObj, dlgThdCB WorkFn)
        {
            m_threads.Invoke(oObj, WorkFn);
        }

        #endregion

    }
    #endregion

}
