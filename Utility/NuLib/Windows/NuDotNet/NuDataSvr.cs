using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Data;

/* *
 * TODO : 找時間修改成NonBlocking mode的Socket程式, 需修改以下幾點
 *        1. Socket.blocking = false
 *        2. SockRecv需新增Timeout參數, 並將內容改為根據傳入的FD select之後再進行接收
 *        3. Login 端的async mode移除, 採用while loop機制, 否則async mode與nonblocking衝突
 * */

namespace NuDotNet.Net
{
    public class NuDataSvrClientNode : IDisposable
    {
        #region -- private items --
        Socket m_client;
        DateTime m_lastTime;
        int m_hbtItv;

        byte[] m_buf;
        int m_bufsz;

        #region protocal
        private string mp_local_hdr_id = "";
        private string mp_remote_hdr_id = "";
        private string mp_login_id = "";
        #endregion

        private readonly object m_SockSndHdl = new object();
        private readonly object m_SockRcvHdl = new object();

        #endregion

        #region --  construct / destruct  --
        public NuDataSvrClientNode(ref Socket accept_socket)
        {
            m_client = accept_socket;
            m_client.ReceiveTimeout = 2000; // 2 sec
            m_client.SendTimeout = 2000; // 0.5 sec
            m_lastTime = DateTime.Now;
            m_hbtItv = 30;
            m_buf = new byte[1024];
            m_bufsz = 1024;
        }
        public void Dispose()
        {
            m_client.Dispose();
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }

        ~NuDataSvrClientNode() { }
        #endregion

        #region -- property --
        public Socket Client { get { return m_client; } }
        public string ClientIP { get { return ((IPEndPoint)m_client.RemoteEndPoint).Address.ToString(); } }
        public Int32 ClientFD { get { return m_client.Handle.ToInt32(); } }
        public DateTime LastWorkTime { get { return m_lastTime; } set { m_lastTime = value; } }
        public int HBTInterval { get { return m_hbtItv; } set { m_hbtItv = value; } }
        public string LoginID { get { return mp_login_id; } set { mp_login_id = value; } }
        public string ClientHdr { get { return mp_remote_hdr_id; } set { mp_remote_hdr_id = value; } }
        public string ServerHdr { get { return mp_local_hdr_id; } set { mp_local_hdr_id = value; } }
        #endregion

        #region -- private function --
        #endregion

        #region -- public function --
        public void UpdateLastWorkTime()
        {
            LastWorkTime = DateTime.Now;
        }

        public int SockRecv(ref StringBuilder sbMsg, int length)
        {
            int iByte = 0;
            int MaxRcvPart = 0;
            int RcvCnt = 0;
            
            sbMsg.Length = 0;
            try
            {
                lock (m_SockRcvHdl)
                {
                    while (length > 0)
                    {
                        //avoid busy loop
                        if (RcvCnt++ >= 5)
                        {
                            if (RcvCnt >= 10)
                                return -1;
                            Thread.Sleep(100);
                        }

                        MaxRcvPart = (length > m_bufsz) ? m_bufsz : length;
                        iByte = m_client.Receive(m_buf, MaxRcvPart, SocketFlags.None);
                        if (iByte == 0 && sbMsg.Length == 0)
                            return 0;
                        length -= iByte;
                        sbMsg.Append(Encoding.Default.GetString(m_buf), 0, iByte);
                    }
                }
            }
            catch (Exception)
            {
                return -1;
            }
            //UpdateLastWorkTime();

            return sbMsg.Length;
        }

        public int SockSend(ref StringBuilder sbMsg)
        {
            int iSndCnt = 0;
            int iRtn = 0;
            int sMsgLen = ASCIIEncoding.Default.GetByteCount(sbMsg.ToString());
            byte[] bMsg = Encoding.Default.GetBytes(sbMsg.ToString());
            byte[] bHdr = Encoding.Default.GetBytes(NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.Msg, sMsgLen));

            lock (m_SockSndHdl)
            {
                if (m_client.Connected)
                {
                    try
                    {
                        //iSndCnt += m_client.Send(bHdr);
                        //iSndCnt += m_client.Send(bMsg);
                        iRtn = m_client.Send(bHdr);
                        if (iRtn <= 0)
                            return -1;
                        iSndCnt += iRtn;

                        iRtn = m_client.Send(bMsg);
                        if (iRtn <= 0)
                            return -1;
                        iSndCnt += iRtn;
                    }
                    catch (Exception ex)
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }

            UpdateLastWorkTime();
            return iSndCnt;
        }

        public int SockLoginConfirm()
        {
            int iSndCnt = 0;
            byte[] bHdr = Encoding.Default.GetBytes(NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.Login, 0));

            lock (m_client)
            {
                if (m_client.Connected)
                {
                    try
                    {
                        iSndCnt = m_client.Send(bHdr);
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                }
            }

            UpdateLastWorkTime();
            return 0;
        }

        #endregion

    }

    //---------------------------------------------------------
    // DataSvr 
    //---------------------------------------------------------
    public class NuDataSvr
    {
        #region -- private items --
        bool m_work = true;
        Hashtable m_htMap = null;
        List<NuDataSvrClientNode> m_clients = null;
        List<Socket> m_socklst = null;

        private IPAddress m_ip = null;
        int m_port = 0;
        Socket m_tcpsvr = null;
        
        // for thread 
        Thread m_ThdListener = null;
        Thread m_ThdMain = null;

        // protocol
        private int mp_hdr_num = 0;
        #endregion 

        #region --  construct / destruct  --
        public NuDataSvr(string listen_ip, int listen_port)
        {
            m_ip = IPAddress.Parse(listen_ip);
            m_port = listen_port;
            m_tcpsvr = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            m_htMap = new Hashtable();
            m_clients = new List<NuDataSvrClientNode>(5);
            m_socklst = new List<Socket>();
            mp_hdr_num = 0;
        }
        ~NuDataSvr() { }
        #endregion

        #region -- private flow / function--
        private void _AddNode(NuDataSvrClientNode node)
        {
            m_htMap.Add(node.Client.Handle, node);
            m_clients.Add(node);
            m_socklst.Add(node.Client);
        }

        private void _RemoveNode(NuDataSvrClientNode node)
        {
            if (OnDisconnectEv != null)
                OnDisconnectEv(node);

            m_socklst.Remove(node.Client);
            m_clients.Remove(node);
            m_htMap.Remove(node.Client.Handle);
            node.Dispose();
        }

        private bool _ChkLogin(NuDataSvrClientNode node)
        {
            StringBuilder sbMsg = new StringBuilder(128);
            int body_len = 0;
            char MsgType = ' ';
            int iByte = 0;

            Socket sockfd = node.Client;
            // rcv Hdr 
            iByte = node.SockRecv(ref sbMsg, 11);
            if (iByte != 11)
                return false;

            MsgType = sbMsg[4];
            
            //body_len = int.Parse(sbMsg.ToString(0, 4));
            if (!int.TryParse(sbMsg.ToString(0, 4), out body_len))
                return false;
             
            node.ClientHdr = sbMsg.ToString(5, 5);

            if (MsgType != 'A')
                return false;

            // recv Body
            iByte = node.SockRecv(ref sbMsg, body_len);
            node.LoginID = sbMsg.ToString();

            // send login confirm
            sbMsg.Length = 0;
            mp_hdr_num = mp_hdr_num + 1;
            node.ServerHdr = String.Format("{0:00000}", mp_hdr_num);
            //sbMsg.Append(NuDataProtocol.genHdr(mp_hdr_num.ToString(), NuMsgType.Login, 0));
            
            //node.SockSend(ref sbMsg);
            node.SockLoginConfirm();

            return true;
        }

        private bool _SendHBT(NuDataSvrClientNode node)
        {
            StringBuilder sbMsg = new StringBuilder(12);
            Socket sockfd = node.Client;
            sbMsg.Append(NuDataProtocol.genHdr(node.ServerHdr, NuMsgType.HBT, 0));
            node.SockSend(ref sbMsg);
            if (OnHBTSendEv != null)
            {
                OnHBTSendEv(node, sbMsg.ToString());
            }
            return true;
        }

        private bool _sendLogout(NuDataSvrClientNode node)
        {
            string sLogout = NuDataProtocol.genHdr(node.ServerHdr, NuMsgType.Logout, 0);
            int iSnd = 0;
            byte[] bMsg = Encoding.Default.GetBytes(sLogout);
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append(bMsg);

            iSnd = node.SockSend(ref sbMsg);
            return true;
        }

        private void _Listener()
        {
            bool bRtn = true;
            bool bLogin = false;
            NuDataSvrClientNode client_node = null;
            Socket sockfd = null;
            ArrayList acceptList = new ArrayList();
            double TimeGap = 0;

            m_tcpsvr.Bind(new IPEndPoint(m_ip, m_port));
            m_tcpsvr.Listen(10);

            try
            {
                while (m_work)
                {
                    #region check connection accept 
                    acceptList.Clear();
                    acceptList.Add(m_tcpsvr);
                    Socket.Select(acceptList, null, null, 1000000);
                    for (int i = 0; i < acceptList.Count; i++)
                    {
                        bRtn = true;
                        sockfd = ((Socket)(acceptList[i])).Accept();
                        client_node = new NuDataSvrClientNode(ref sockfd);

                        #region Check Login 
                        bLogin = _ChkLogin(client_node);
                        #endregion

                        if (bLogin)
                        {
                            if (OnConnectEv != null)
                                bRtn = OnConnectEv(client_node);

                            if (bRtn)
                                _AddNode(client_node);
                            else
                            {
                                _sendLogout(client_node);
                                sockfd.Close();
                                sockfd.Dispose();
                            }
                        }
                        else
                        {
                            sockfd.Close();
                            sockfd.Dispose();
                        }
                    }
                    #endregion 

                    #region check timeout
                    DateTime now = DateTime.Now;
                    for(int i = 0; i < m_clients.Count; i++)
                    {
                        NuDataSvrClientNode node = m_clients[i];
                        TimeGap =  (now - node.LastWorkTime).TotalSeconds - node.HBTInterval;
                        if (TimeGap > 5)
                        {
                            // 不要即時移除連線, 或許client端處理太慢, 等待對方自行斷線或是endday
                            //if (OnLogDumpEv != null)
                            //    OnLogDumpEv(String.Format("{0}-{1}-{2} socket remove!", node.ClientFD, node.ClientIP, node.LoginID));
                            //_RemoveNode(node);
                        }
                        else if (TimeGap > 10)
                        {
                            if (OnTimeOutEv != null)
                                OnTimeOutEv(node);
                        }
                        else if (TimeGap >= -1)
                        {
                            // send hbt
                            _SendHBT(node);
                        }
                        
                    }
                    #endregion
                }
            }
            catch (ThreadAbortException) { return; }
            catch (ThreadInterruptedException) { return; }
        }

        private void _RecvData()
        {
            int iRC = 0;
            int iLength = 0;
            List<Socket> rlst = null;
            List<Socket> errlst = null;
            NuDataSvrClientNode node = null;
            StringBuilder sbHdr = new StringBuilder(12);
            StringBuilder sbBody = new StringBuilder(512);
            RcvDataArgs data = null;

            try
            {
                while (m_work)
                {
                    if (m_socklst.Count == 0)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    errlst = new List<Socket>(m_socklst);
                    rlst = new List<Socket>(m_socklst);
                    Socket.Select(rlst, null, errlst, 1000000);

                    //foreach (Socket sockfd in rlst)
                    for (int i = 0; i < rlst.Count; i++)
                    {
                        Socket sockfd = rlst[i];
                        if ( (node = (NuDataSvrClientNode)m_htMap[sockfd.Handle]) == null)
                            continue;
                        iRC = 0;
                        sbHdr.Length = 0;
                        sbBody.Length = 0;
                        iRC = node.SockRecv(ref sbHdr, 11);
                        if (iRC <= 0)
                        {
                            _RemoveNode(node);
                            continue;
                        }
                        else if (iRC > 0)
                        {
                            if (sbHdr.Length == 11 &&
                                    int.TryParse(sbHdr.ToString(0, 4), out iLength))
                                node.SockRecv(ref sbBody, iLength);
                            else
                            {
                                _RemoveNode(node);
                                continue;
                            }
                        }
                        
                        if (OnDataArrivedEv != null)
                        {
                            data = new RcvDataArgs(sbHdr.ToString(), sbBody.ToString());
                            OnDataArrivedEv(node, data);
                        }
                    }

                    for (int i = 0; i < errlst.Count; i++)
                    {
                        Socket sockfd = rlst[i];
                        if ((node = (NuDataSvrClientNode)m_htMap[sockfd.Handle]) == null)
                            continue;
                        _RemoveNode(node);
                    }
                }
            }
            catch (ThreadAbortException) { return; }
            catch (ThreadInterruptedException) { return; }
        }
        #endregion

        #region -- public event --
        public delegate bool OnConnect(NuDataSvrClientNode Client);
        public delegate void OnDisConnect(NuDataSvrClientNode Client);
        public delegate void OnTimeOut(NuDataSvrClientNode Client);
        public delegate void OnDataArrived(NuDataSvrClientNode Client, RcvDataArgs e);
        public delegate void OnHBTSend(NuDataSvrClientNode Client, string sMsg);
        public delegate void OnLogDump(string sMsg);
        public event OnConnect OnConnectEv;
        public event OnDisConnect OnDisconnectEv;
        public event OnTimeOut OnTimeOutEv;
        public event OnDataArrived OnDataArrivedEv;
        public event OnHBTSend OnHBTSendEv;
        public event OnLogDump OnLogDumpEv;
        #endregion

        #region -- public property --
        #endregion

        #region -- public function --
        public void Start()
        {
            m_work = true;
            m_ThdListener = new Thread(_Listener);
            m_ThdMain = new Thread(_RecvData);
            m_ThdListener.IsBackground = true;
            m_ThdListener.Start();
            m_ThdMain.IsBackground = true;
            m_ThdMain.Start();
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
        }

        public void Broadcast(ref StringBuilder sbMsg)
        {
            int iRtn = 0;
            NuDataSvrClientNode node = null;
            for (int i = 0; i < m_clients.Count; i++)
            {
                node = m_clients[i];
                iRtn = node.SockSend(ref sbMsg);
                if (iRtn < 0)
                {
                    if(OnLogDumpEv!= null)
                        OnLogDumpEv(String.Format("{0}-{1}-{2} socket send fail!", node.ClientFD, node.ClientIP, node.LoginID));
                }
            }
        }

        #endregion

    }
}
