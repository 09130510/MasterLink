using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using NuDotNet.THDS;

namespace NuDotNet.SocketUtil
{
    #region ###  Sokcet enum
    #endregion

    #region ###  Socket Interface
    /// <summary>
    /// interface : SocketBox事件介面
    /// </summary>
    public interface INuSocketHandler
    {
        NuSocketParam Handle { get; set; }

        /// <summary>
        /// 連線失敗觸發
        /// </summary>
        /// <param name="oParam"></param>
        /// <returns></returns>
        bool OnConnectFail(NuSocketParam oParam);
        /// <summary>
        /// 連線成功處發
        /// </summary>
        /// <param name="oParam"></param>
        /// <returns></returns>
        bool OnConnected(NuSocketParam oParam);
        /// <summary>
        /// 斷線觸發
        /// </summary>
        /// <param name="oParam"></param>
        void OnDisconnected(NuSocketParam oParam);
        /// <summary>
        /// 資料到達觸發
        /// </summary>
        /// <param name="oParam"></param>
        void OnDataArrived(NuSocketParam oParam);
        /// <summary>
        /// 遠端timeout觸發
        /// </summary>
        /// <param name="oParam"></param>
        void OnRemoteTimeout(NuSocketParam oParam);
        /// <summary>
        /// 本地端timeout觸發
        /// </summary>
        /// <param name="oParam"></param>
        void OnLocalTimeout(NuSocketParam oParam);
        /// <summary>
        /// 發生錯誤
        /// </summary>
        /// <param name="sMsg"></param>
        void OnException(string sMsg);
    }
    #endregion
    
    #region ###  SocketBase Object
    /// <summary>
    /// 
    /// </summary>
    public class NuSocketParam : EventArgs, IDisposable
    {
        internal NuSocketBox SocketObj;
        internal NuSockBase SocketNode;

        #region --- construct / destruct ---
        public NuSocketParam()
        {
        }

        ~NuSocketParam() { }

        public void Dispose()
        {
        }
        #endregion 
        public int AvailableSz { get { return (SocketNode != null) ? SocketNode.SockFD.Available : 0; } }
        public string RemoteIP { get { return (SocketNode != null) ? SocketNode.RemoteIP : ""; } }
        public int RemotePort { get { return (SocketNode != null) ? SocketNode.RemotePort : 0; } }
        public int SocketFDHdlNo { get { return (SocketNode == null) ? 0 : SocketNode.SockFD.Handle.ToInt32(); } }
        public int RemoteTimeoutSec
        {
            get
            {
                if (SocketNode == null)
                    return 0;
                if (SocketNode.IsServer)
                    return 0;
                return ((NuSockNode)SocketNode).RemoteTimeoutSec;
            }
        }
        public int LocalTimeoutSec
        {
            get
            {
                if (SocketNode == null)
                    return 0;
                if (SocketNode.IsServer)
                    return 0;
                return ((NuSockNode)SocketNode).LocalTimeoutSec;
            }
        }

        public void Clear()
        {
            SocketObj = null;
            SocketNode = null;
        }

        public bool SetNoDelay(bool bFlag)
        {
            if (SocketNode == null)
                return false;
            SocketNode.SockFD.NoDelay = bFlag;
            return true;
        }

        public bool SetBlocking(bool bFlag)
        {
            if (SocketNode == null)
                return false;
            SocketNode.SockFD.Blocking = bFlag;
            return true;
        }

        public void SetTimeoutSec(int Sec)
        {
            SetTimeoutSec(Sec, Sec);
        }

        public void SetTimeoutSec(int LocalSec, int RemoteSec)
        {
            NuSockNode oNode = null;
            if (SocketNode.IsServer)
                return;

            oNode = (NuSockNode)SocketNode;
            oNode.LocalTimeoutSec = LocalSec - 1;

            oNode.RemoteTimeoutSec = RemoteSec + 1;
        }

        public bool SetAutoReconnect(bool OnOff)
        {
            if (SocketNode == null)
                return false;
            if (SocketNode.IsServer)
                return false;
            NuSockNode oNode = (NuSockNode)SocketNode;
            oNode.AutoReconnect = OnOff;
            return true;
        }

        public object GetArgu()
        {
            return (SocketNode != null) ? SocketNode.GetArgu() : null;
        }

        public int Recv(ref MemoryStream mStream, int Length, int uSec)
        {
            if (SocketNode == null)
                return -1;
            return SocketNode.Recv(ref mStream, Length, uSec);
        }

        public int Send(ref MemoryStream mStream, int uSec)
        {
            if (SocketNode == null)
                return -1;
            return SocketNode.Send(ref mStream, uSec);
        }

        public int Send(ref MemoryStream mStream)
        {
            if (SocketNode == null)
                return -1;
            return SocketNode.Send(ref mStream);
        }

        /// <summary>
        /// 強迫斷線
        /// </summary>
        /// <param name="bForce"></param>
        /// <returns></returns>
        public bool Disconnect(bool bForce = true)
        {
            if (SocketNode == null)
            {
                return false;
            }
            if (SocketNode.IsServer)
            {
                NuSockListener oListener = (NuSockListener)SocketNode;
                oListener.Stop();
                return false;
            }
            else
            {
                NuSockNode oNode = (NuSockNode)SocketNode;
                if (bForce)
                    SetAutoReconnect(false);

                if (!oNode.IsTerminated)
                    oNode.Disconnect();
            }
            return true;
        }
    }

    internal class NuSockBase : IDisposable
    {
        private bool m_disposed = false;
        private Object m_argu;
        protected bool m_terminated = true;
        private Socket m_sockfd = null;

        public NuSocketBox SocketBoxObj { get; private set; }
        public bool IsWorking { get; set; }
        public bool IsServer { get; set; }
        public bool IsTerminated { get { return m_terminated; } set { m_terminated = value; } }
        public Socket SockFD { get { return m_sockfd; } set { m_sockfd = value; } }
        public String LocalIP { get; protected set; }
        public int    LocalPort { get; protected set; }
        public String RemoteIP { get; protected set; }
        public int    RemotePort { get; protected set; }
        public String Name { get; protected set; }

        public virtual void Work(object oObj) {return;}
        public virtual int Send(ref MemoryStream Stream, int uSec) { return -1; }
        public virtual int Send(ref MemoryStream Stream) { return -1; }
        public virtual int Recv(ref MemoryStream Stream, int Length, int uSec) { return -1; }
        #region --- construct / destruct ---
        public NuSockBase(NuSocketBox oObj)
        {
            m_argu = null;
            SocketBoxObj = oObj;
        }
        ~NuSockBase() { }
        protected virtual void Dispose(bool bDisposed)
        {
            if (m_disposed)
                return;

            m_disposed = bDisposed;
            SockFD.Dispose();
            SockFD = null;
        } 
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

        public void SetArgu(Object oObj) 
        { 
            m_argu = oObj; 
        }

        public Object GetArgu()
        { 
            return m_argu;
        }
        
    }
            

    internal class NuSockListener : NuSockBase
    {
        public List<NuSockNode> Nodes;
        public INuSocketHandler Hdlr = null;
        public Thread ThdID { get { return Thread.CurrentThread; } }

        public NuSockListener(NuSocketBox oObj, INuSocketHandler HdlrObj)
            :base(oObj)
        {
            IsServer = true;
            Hdlr = HdlrObj;
            Nodes = new List<NuSockNode>();
        }
        ~NuSockListener() { }

        public void SetConnInfo(String sSvrName, String sIP, int iPort)
        {
            LocalPort = iPort;
            LocalIP = sIP;
            Name = sSvrName;
        }

        public bool Listen(int backlog)
        {
            try
            {
                IPAddress ListenIP = IPAddress.Parse(LocalIP);
                SockFD = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                SockFD.Bind(new IPEndPoint(ListenIP, LocalPort));
                SockFD.Listen(10);
                m_terminated = false;
                return true;
            }
            catch (Exception ex)
            {
                if (Hdlr != null)
                {
                    string sMsg = string.Format("[NuSocketListener] Work : {0}", ex.Message);
                    Hdlr.OnException(sMsg);
                }
                return false;
            }
        }

        public void AddNode(NuSockNode oNode)
        {
            lock(Nodes)
                Nodes.Add(oNode);
        }

        public void RmvNode(NuSockNode oNode)
        {
            lock (Nodes)
                Nodes.Remove(oNode);
        }

        public void Stop()
        {
            m_terminated = true;

            // remove listener 
//            SockFD.Shutdown(SocketShutdown.Both);
            SockFD.Close();
//            SockFD.Dispose();

            IsWorking = true;
            lock (Nodes)
            {
                foreach (NuSockNode oNode in Nodes)
                {
                    NuDebug.WriteLine("Stop : " + oNode.RemotePort + " : " + oNode.RemoteIP);
                    oNode.Disconnect();
                }
            }
            IsWorking = false;
        }
    }

    internal class NuSockNode : NuSockBase
    {
        private object m_recv_lock = new object();
        private object m_send_lock = new object();
        private object m_sock_lock = new object();

        public bool CanDispose { get; private set; }
        public NuSockBase ParentServer { get; private set; }
        public INuSocketHandler Hdlr = null;
        public bool AutoReconnect { set; get; }
        public DateTime LastSendTime { set; get; }
        public DateTime LastRecvTime { set; get; }
        public int LocalTimeoutSec { set; get; }
        public int RemoteTimeoutSec { set; get; }
        public bool Connected { get { return (SockFD == null) ? false : SockFD.Connected; } }

        public NuSocketParam Param { get; private set; }

        public NuSockNode(NuSocketBox oObj, INuSocketHandler HdlrCB)
            :base(oObj)
        {
            IsServer = false;
            IsTerminated = false;
            AutoReconnect = false;
            LastRecvTime = LastSendTime = DateTime.Now;
            LocalTimeoutSec = 1;
            RemoteTimeoutSec = 1;
            Hdlr = HdlrCB;
            ParentServer = null;
            Param = oObj.SockParamGet();
            Param.SocketNode = this;

            CanDispose = false;
        }

        public NuSockNode(NuSocketBox oObj, INuSocketHandler HdlrCB, NuSockBase oListener)
            :base(oObj)
        {
            IsServer = false;
            IsTerminated = false;
            AutoReconnect = false;
            LastRecvTime = LastSendTime = DateTime.Now;
            LocalTimeoutSec = 1;
            RemoteTimeoutSec = 1;
            Hdlr = HdlrCB;
            ParentServer = oListener;
            Param = oObj.SockParamGet();
            Param.SocketNode = this;
        }
        ~NuSockNode()
        {
            NuSocketParam oParam = Param;
            base.SocketBoxObj.SockParamPut(ref oParam);
            Param = null;
        }

        public void SetConnInfo(String sClntName, String sTargetIP, int iTargetPort, string sBindIP, int iBindPort)
        {
            LocalIP = sBindIP;
            LocalPort = iBindPort;
            RemoteIP = sTargetIP;
            RemotePort = iTargetPort;
            Name = sClntName;
        }

        public bool Connect()
        {
            bool bRtn = false;
            try
            {
                lock (m_sock_lock)
                {
                    SockFD = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    if (LocalIP != "" && LocalPort != 0)
                    {
                        IPAddress BindIP = IPAddress.Parse(LocalIP);
                        SockFD.Bind(new IPEndPoint(BindIP, LocalPort));
                    }
                    SockFD.Connect(RemoteIP, RemotePort);
                }
                bRtn = SockFD.Connected;
                if (bRtn == true)
                {
                    LocalIP = ((IPEndPoint)SockFD.LocalEndPoint).Address.ToString();
                    LocalPort = ((IPEndPoint)SockFD.LocalEndPoint).Port;
                }
            }
            catch (SocketException ex)
            {
                bRtn = false;
                SockFD.Dispose();
                SockFD = null;
                string sMsg = string.Format("[NuSockNode] Connect : {0}", ex.Message);
                if (Hdlr != null)
                    Hdlr.OnException(sMsg);
            }
            catch (Exception ex)
            {
                bRtn = false;
                string sMsg = string.Format("[NuSockNode] Connect : {0}", ex.Message);

                if (Hdlr != null)
                    Hdlr.OnException(sMsg);
                else
                {
                    throw new Exception(sMsg);
                }
            }
            finally
            {
            }
            return bRtn;
        }

        public void Disconnect()
        {
            IsTerminated = true;

            lock (m_sock_lock)
            {
                if (Hdlr != null && SockFD != null)
                {
                    Hdlr.OnDisconnected(Param);

//                    SockFD.Shutdown(SocketShutdown.Both);
//                    SockFD.Disconnect(false);
                    SockFD.Close();
                    LocalIP = "";
                    LocalPort = 0;
                }
            }

            CanDispose = true;
        }

        public override int Send(ref MemoryStream Stream)
        {
            int iByte = 0;
            try
            {
                lock (m_send_lock)
                {
                    iByte = SockFD.Send(Stream.ToArray());
                }
                if (iByte > 0)
                    LastSendTime = DateTime.Now;

                return iByte;
            }
            catch (SocketException ex)
            {
                return -1;
            }
        }

        public override int Send(ref MemoryStream Stream, int uSec)
        {
            int iByte = 0;
            try
            {
                if (SockFD.Poll(uSec, SelectMode.SelectWrite))
                {
                    lock (m_send_lock)
                    {
                        iByte = SockFD.Send(Stream.ToArray());
                    }
                    if (iByte > 0)
                        LastSendTime = DateTime.Now;
                    return iByte;
                }
                else
                {
                    return -1;
                }
            }
            catch (SocketException ex)
            {
                //                NuDebug.WriteLine(ex.Message);
                return -1;
            }
            catch (Exception)
            {
                return -2;
            }
        }

        public override int Recv(ref MemoryStream Stream, int Length, int uSec)
        {
            int iBufSz = 512;
            byte[] bBuf = new byte[iBufSz];
            int iLeaveLen = Length;
            int iRcvLen = 0;
            int iLen = 0;
            int iRtn = 0;
            int iSec = 1000000;
            try
            {
                while (iLeaveLen > 0 && uSec > 0)
                {
                    iRcvLen = (iLeaveLen > iBufSz) ? iBufSz : iLeaveLen;
                    if (SockFD == null)
                    {
                        return -2;
                    }

                    if (SockFD.Poll(iSec, SelectMode.SelectRead))
                    {
                        lock (m_recv_lock)
                            iLen = SockFD.Receive(bBuf, iRcvLen, SocketFlags.None);

                        if (iLen == 0)
                        {
                            if (SockFD.Poll(1, SelectMode.SelectError))
                            {
                                return -3;
                            }
                        }
                        if (iLen <= 0)
                        {
                            //                            NuDebug.WriteLine(String.Format("Recv Handle {0} = {1} , ", iLen, SockFD.Handle));
                            return -1;
                        }

                        Stream.Write(bBuf, 0, iLen);
                        iLeaveLen -= iLen;
                        iRtn += iLen;
                    }
                    else
                    {
                        uSec -= iSec;
                    }
                }
                LastRecvTime = DateTime.Now;
                return iRtn;

            }
            catch (SocketException)
            {
                return -1;
            }
            catch (Exception)
            {
                return -2;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NuSocketBox
    {
        // private variables
        // ===============================================
        #region --- private variables ---
        private bool m_terminated = false;
        private NuThreadPool m_thread_pool;
        private Hashtable m_htb_servers;
        private Hashtable m_htb_nodes;
        private Dictionary<string, NuSockListener> m_dic_servers;
        private object m_lock = new object();
        private List<NuSockNode> m_lst_node_delay;
        private NuList<NuSocketParam> m_lst_socket_params;
        private object m_rcv_data_lock = new object();
        private object m_listener_lock = new object();
        private bool m_select_arrived_flag = false;
        #endregion

        // property
        // ===============================================
        #region --- public property ---
        public int SelectInterval { get; set; }
        #endregion

        // Event
        // ===============================================

        // internal private function 
        // ===============================================
        #region --- private function ---
        private NuSockListener _GetListenerObject(INuSocketHandler Hdlr)
        {
            return new NuSockListener(this, Hdlr);
        }
        private NuSockNode _GetNodeObject(INuSocketHandler Hdlr)
        {
            return new NuSockNode(this, Hdlr);
        }

        private void SetWorking(NuSockBase oSockBase, bool bIsWorking)
        {
            oSockBase.IsWorking = bIsWorking;
//NuDebug.WriteLine(bIsWorking.ToString());
            if (bIsWorking == false)
            {
                lock (m_rcv_data_lock)
                    Monitor.Pulse(m_rcv_data_lock);
            }
        }

        private bool AddNode(NuSockNode oNode)
        {
            lock (m_lock)
            {
//                NuDebug.WriteLine(String.Format("====Remote[{0}:{1}] Local[{2}:{3}]", 
//                    oNode.RemoteIP, oNode.RemotePort, oNode.LocalIP, oNode.LocalPort));
                if (m_htb_nodes.ContainsKey(oNode.SockFD))
                    return false;
                m_htb_nodes.Add(oNode.SockFD, oNode);

                if (oNode.ParentServer != null)
                {
                    NuSockListener oListener = (NuSockListener)oNode.ParentServer;
                    oListener.AddNode(oNode);
                }

                if (m_htb_nodes.Count == 1)
                {
                    lock (m_rcv_data_lock)
                        Monitor.Pulse(m_rcv_data_lock);
                }
            }

            return true;
        }

        private bool RmvNode(NuSockNode oNode)
        {
            lock (m_lock)
            {
                if (!m_htb_nodes.ContainsKey(oNode.SockFD))
                    return false;
                m_htb_nodes.Remove(oNode.SockFD);

                if (oNode.ParentServer != null)
                {
                    NuSockListener oListener = (NuSockListener)oNode.ParentServer;
                    oListener.RmvNode(oNode);
                        
                }
            }
            return false;
        }
        #endregion
        // Implementation 
        // ===============================================
        public NuSocketBox()
        {
            m_thread_pool = new NuThreadPool();
            m_htb_servers = new Hashtable();
            m_htb_nodes = new Hashtable();
            m_dic_servers = new Dictionary<string, NuSockListener>();
            m_lst_node_delay = new List<NuSockNode>();
            m_lst_socket_params = new NuList<NuSocketParam>();
            SelectInterval = 1000000;

            m_thread_pool.Invoke(null, _ServerListener);
            m_thread_pool.Invoke(null, _TimerWork);
            m_thread_pool.Invoke(null, _DataArrivedThd);
        }
        ~NuSocketBox(){}

        internal NuSocketParam SockParamGet()
        {
            return m_lst_socket_params.Pop();
        }

        internal void SockParamPut(ref NuSocketParam oObj)
        {
            oObj.Clear();
            m_lst_socket_params.Push(ref oObj);
        }

        public bool Invoke(object oObj, dlgThdCB CBFn, out String sMsg)
        {
            try
            {
                sMsg  = "";
                m_thread_pool.Invoke(oObj, CBFn);
            }
            catch(Exception ex)
            {
                sMsg = String.Format("Thread Pool Invoke Fail! {0}", ex.Message);
                return false;
            }
            return true;
        }

        #region listener flow  &  method 
        private void _Accept(object oObj)
        {
            NuSockListener oListener = (NuSockListener)oObj;
            NuSockNode oNode = new NuSockNode(this, oListener.Hdlr, oListener);
            oNode.SockFD = oListener.SockFD.Accept();
//            oNode.AutoReconnect = false;

            oNode.SetConnInfo("", ((IPEndPoint)oNode.SockFD.RemoteEndPoint).Address.ToString(), 
                                  ((IPEndPoint)oNode.SockFD.RemoteEndPoint).Port,
                                  ((IPEndPoint)oNode.SockFD.LocalEndPoint).Address.ToString(),
                                  ((IPEndPoint)oNode.SockFD.LocalEndPoint).Port);

            if (oNode.Hdlr != null)
            {
                if (oNode.Hdlr.OnConnected(oNode.Param))
                {
                    AddNode(oNode);
                }
                else
                {
                    oNode.Disconnect();
                }
            }

            SetWorking(oListener, false);
        }

        private void _ServerListener(object oObj)
        {
            try
            {
                List<Socket> m_lst_read = new List<Socket>();
                List<Socket> m_lst_err = new List<Socket>();
                List<NuSockBase> m_delete = new List<NuSockBase>();
                NuSockListener oListener = null;
                string sMsg = "";
                while (!m_terminated)
                {
                    #region select list prepare
                    if (m_htb_servers.Count == 0)
                    {
//                        Thread.Sleep(1000);
                        lock (m_listener_lock)
                            Monitor.Wait(m_listener_lock, 5000000);
                        continue;
                    }

                    m_lst_err.Clear();
                    m_lst_read.Clear();
                    m_delete.Clear();
                    lock (m_lock)
                    {
                        foreach(NuSockBase item in m_htb_servers.Values)
                        {
                            if (item.IsTerminated && !item.IsWorking)
                            {
                                m_delete.Add(item);
                                continue;
                            }
                            else if (item.IsTerminated || item.IsWorking)
                                continue;

                            m_lst_read.Add(item.SockFD);
                            m_lst_err.Add(item.SockFD);
                        }
                    }

                    if (m_delete.Count > 0)
                    {
                        lock (m_lock)
                        {
                            foreach (NuSockBase item in m_delete)
                            {
                                oListener = (NuSockListener)item;
                                m_htb_servers.Remove(item.SockFD);
                                m_dic_servers.Remove(oListener.Name);
                                item.Dispose();
                            }
                        }
                    }

                    #endregion

                    if (m_lst_read.Count > 0)
                    {
                        try
                        {
                            Socket.Select(m_lst_read, null, m_lst_err, 1000000);
                        }
                        catch (SocketException)
                        {
                            continue;
                        }
                    }

                    // Accept All Client 
// NuDebug.WriteLine(String.Format("+++++++++++++++++++++ {0} +++++", m_lst_read.Count));
                    foreach (Socket oSock in m_lst_read)
                    {
                        lock (m_lock)
                        {
                            oListener = (NuSockListener)m_htb_servers[oSock];
                            SetWorking(oListener, true);
                        }

                        if (oListener.IsTerminated)
                        {
                            SetWorking(oListener, false);
                            continue;
                        }
//                        NuDebug.WriteLine(String.Format("+++++++++++++++++++++ {0} , {1}", oListener.IsTerminated, m_lst_read.Count));
                        if (!Invoke(oListener, _Accept, out sMsg))
                        {
                            if (oListener.Hdlr != null)
                                oListener.Hdlr.OnException(sMsg);
                        }

                    }

                    //Remove Server 
                    foreach (Socket oSock in m_lst_err)
                    {
                        lock (m_lock)
                        {
                            oListener = (NuSockListener)m_htb_servers[oSock];
                            oListener.IsTerminated = true;
                            SetWorking(oListener, false);
                            
                            
                            foreach (NuSockNode node in oListener.Nodes)
                            {
                                node.Disconnect();
                                lock(m_lock)
                                    m_lst_node_delay.Add(node);
                                RmvNode(node);
                            }

                            m_htb_servers.Remove(oListener.SockFD);
                        }
                    }

                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        public bool AddServer(String Name, String IP, String Port, INuSocketHandler ListenerCB, object oArgu)
        {
            NuSockListener oListener = null;
            int iPort = 0;
            if (ListenerCB == null)
                return false;

            if (!int.TryParse(Port, out iPort))
                return false;

            oListener = _GetListenerObject(ListenerCB);
            oListener.SetConnInfo(Name, IP, iPort);
            oListener.SetArgu(oArgu);

            lock (m_lock)
            {
                if (!m_dic_servers.ContainsKey(Name))
                {
                    if (oListener.Listen(10))
                    {
                        m_dic_servers.Add(Name, oListener);
                        m_htb_servers.Add(oListener.SockFD, oListener);
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }

            NuSocketParam Param = SockParamGet();
            Param.SocketNode = oListener;
            ListenerCB.Handle = Param;

            if (m_htb_servers.Count == 1)
            {
                lock (m_listener_lock)
                    Monitor.Pulse(m_listener_lock);
            }
            return true;
        }
        #endregion

        #region client flow & method
        private void _ClientConnectWork(object oObj)
        {
            NuSockNode oNode = (NuSockNode)oObj;
            string sMsg = "";
            bool bRtn = true;
            try
            {
                do
                {
                    if (oNode.Connect())
                    {
                        if (oNode.Hdlr != null)
                            bRtn = oNode.Hdlr.OnConnected(oNode.Param);

                        if (bRtn)
                        {
                            oNode.IsTerminated = false;
                            AddNode(oNode);
                        }
                    }
                    else
                    {
                        if (oNode.Hdlr != null)
                            oNode.Hdlr.OnConnectFail(oNode.Param);
                        if (oNode.AutoReconnect == true)
                            Thread.Sleep(1000);
                    }
                } while (oNode.Connected == false && oNode.AutoReconnect == true);
            }
            catch (Exception ex)
            {
                if (oNode.Hdlr != null)
                    oNode.Hdlr.OnException(ex.Message);
            }
            finally
            {
                SetWorking(oNode, false);
            }
        }

        public bool AddClient(String Name, String IP, String Port, INuSocketHandler ClientCB, object oArgu, bool AutoReconnect = false)
        {
            NuSockNode oNode = null;
            int iPort = 0;
            string sMsg = "";

            if (ClientCB == null)
                return false;

            if (!int.TryParse(Port, out iPort))
                return false;

            oNode = new NuSockNode(this, ClientCB);
            oNode.SetConnInfo(Name, IP, iPort, "", 0);
            oNode.SetArgu(oArgu);

            oNode.Param.SetAutoReconnect(AutoReconnect);
            
            ClientCB.Handle = oNode.Param;
            SetWorking(oNode, true);
            Invoke(oNode, _ClientConnectWork, out sMsg);

            return true;
        }
        #endregion

        #region dataarrived flow 
        private void _DataArrivedWork(object oObj)
        {
            NuSockNode oNode = (NuSockNode)oObj;
            if (oNode.IsTerminated)
                return;
//            NuSocketParam Param = this.SockParamGet();
//            Param.SocketNode = oNode;
//            oNode.Hdlr.OnDataArrived(Param);

            try
            {
                //while (oNode.SockFD.Poll(SelectInterval, SelectMode.SelectRead)
                //    && !oNode.IsTerminated)
                //{
                //    oNode.Hdlr.OnDataArrived(oNode.Param);
                //}
                while (!oNode.IsTerminated)
                {
                    if (oNode.SockFD.Poll(200, SelectMode.SelectRead))
                    {
                        oNode.Hdlr.OnDataArrived(oNode.Param);
                    }
                    else
                    {
                        if (!m_select_arrived_flag)
                        {
                            NuDebug.WriteLine("release");
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                SetWorking(oNode, false);
            }
            
//            this.SockParamPut(ref Param);


        }

        private void _DataArrivedThd(object oObj)
        {
            try
            {
                List<Socket> lst_read = new List<Socket>();
                List<Socket> lst_err = new List<Socket>();
                List<NuSockNode> lst_expire = new List<NuSockNode>();
                List<NuSockNode> lst_reconnect = new List<NuSockNode>();
                NuSockNode oNode = null;
                string sMsg = "";
                
                while (!m_terminated)
                {
                    #region select list prepare 
                    lst_read.Clear();
                    lst_err.Clear();
                    lst_expire.Clear();
                    lst_reconnect.Clear();

                    lock (m_lock)
                    {
//System.Diagnostics.Debug.WriteLine("rcv data 2" + DateTime.Now.ToString("HH:mm:ss.fff"));
                        foreach (NuSockNode node in m_htb_nodes.Values)
                        {
                            if (node.IsTerminated)
                            {
                                if (node.AutoReconnect)
                                    lst_reconnect.Add(node);
                                else
                                    lst_expire.Add(node);

                                continue;
                            }
                            else if (node.IsWorking)
                            {
                                continue;
                            }
                            else
                            {
                                if (node.SockFD != null)
                                {
                                    lst_err.Add(node.SockFD);
                                    lst_read.Add(node.SockFD);
                                }
                                else
                                {
//                                    NuDebug.WriteLine("SockFD is null");
                                    continue;
                                }
                            }
                        }

                        foreach (NuSockNode node in lst_expire)
                        {
                            RmvNode(node);
                            m_lst_node_delay.Add(node);
                        }

                        foreach (NuSockNode node in lst_reconnect)
                        {
                            RmvNode(node);
                            if (!Invoke(node, _ClientConnectWork, out sMsg))
                                node.Hdlr.OnException(sMsg);
                        }
                    }
                    #endregion

                    if (lst_read.Count > 0)
                    {
                        try
                        {
                            m_select_arrived_flag = true;

                            //NuDebug.WriteLine("Select ----- " + lst_read.Count + " , " + lst_err.Count);

                            Socket.Select(lst_read, null, lst_err, SelectInterval);
                            //NuDebug.WriteLine("Select ##### " + lst_read.Count + " , " + lst_err.Count);
                        }
                        catch (SocketException)
                        {
                            //System.Diagnostics.Debug.WriteLine("Socket Exception");
                            continue;
                        }
                        finally
                        {
                            m_select_arrived_flag = false;
                        }
                    }
                    else
                    {
                        lock (m_rcv_data_lock)
                        {
                            Monitor.Wait(m_rcv_data_lock, 1000);
//                            NuDebug.WriteLine("wait ");
                            continue;
                        }
                    }

                    foreach (Socket oSock in lst_read)
                    {
                        lock (m_lock)
                        {
                            oNode = (NuSockNode)m_htb_nodes[oSock];
                        }
                        SetWorking(oNode, true);
                        if (!Invoke(oNode, _DataArrivedWork, out sMsg))
                        {
                            SetWorking(oNode, false);

                            if (oNode.Hdlr != null)
                                oNode.Hdlr.OnException(sMsg);
                        }
                    }

                    foreach (Socket oSock in lst_err)
                    {
                        lock (m_lock)
                        {
                            oNode = (NuSockNode)m_htb_nodes[oSock];
                        }
                        oNode.Disconnect();

                        // send to delay release
                        lock(m_lock)
                            m_lst_node_delay.Add(oNode);
                    }
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (ThreadInterruptedException)
            {
            }
        }

        #endregion

        #region Timer work flow
        private void _LocalTimeoutWork(object oObj)
        {
            NuSockNode oNode = (NuSockNode)oObj;
//            NuSocketParam Param = SockParamGet();
//            Param.SocketNode = oNode;

//            oNode.Hdlr.OnLocalTimeout(Param);

            oNode.Hdlr.OnLocalTimeout(oNode.Param);
//            SockParamPut(ref Param);
            return;
        }
        private void _RemoteTimeoutWork(object oObj)
        {
            NuSockNode oNode = (NuSockNode)oObj;
//            NuSocketParam Param = SockParamGet();
//            Param.SocketNode = oNode;

//            oNode.Hdlr.OnRemoteTimeout(Param);

            oNode.Hdlr.OnRemoteTimeout(oNode.Param);
//            SockParamPut(ref Param);
            return;
        }

        private void _TimerWork(object oObj)
        {
            DateTime Now = DateTime.Now;
            DateTime LastNow = Now;
            int sleep_ms = 1000;
            string sMsg = "";
            List<NuSockNode> lst_node = new List<NuSockNode>();

            while (!m_terminated)
            {
//                System.Diagnostics.Debug.WriteLine("timework");
                double pending_sec = 0;
                Now = DateTime.Now;
                sleep_ms = 1000 - (Now - LastNow).Milliseconds;

                Thread.Sleep(sleep_ms);

                LastNow = Now;

                #region check timeout event
                if (m_htb_nodes.Count > 0)
                {
                    lock (m_lock)
                    {
                        foreach (NuSockNode oNode in m_htb_nodes.Values)
                        {
                            if (oNode.IsTerminated)
                                continue;
                            if ((pending_sec = (Now - oNode.LastRecvTime).TotalSeconds) > oNode.RemoteTimeoutSec)
                            {
                                if (!Invoke(oNode, _RemoteTimeoutWork, out sMsg))
                                {
                                    oNode.Hdlr.OnException(sMsg);
                                }
                            }

                            if ((Now - oNode.LastSendTime).TotalSeconds > oNode.LocalTimeoutSec)
                            {
                                if (!Invoke(oNode, _LocalTimeoutWork, out sMsg))
                                {
                                    oNode.Hdlr.OnException(sMsg);
                                }
                            }
                        }
                    }
                }

                #endregion

                //check delay release
                if (m_lst_node_delay.Count > 0)
                {

                    lock (m_lock)
                    {
                        List<NuSockNode> lst_remove = new List<NuSockNode>();
                        foreach (NuSockNode oNode in m_lst_node_delay)
                        {
                            if (oNode.CanDispose)
                            {
                                oNode.Dispose();
                                lst_remove.Add(oNode);
                            }
                        }

                        foreach (NuSockNode oNode in lst_remove)
                        {
                            m_lst_node_delay.Remove(oNode);
                        }

                        lst_remove.Clear();
                    }
//                    m_lst_node_delay.Clear();
                }
            }
            return;
        }
        #endregion
    }
    #endregion
}
