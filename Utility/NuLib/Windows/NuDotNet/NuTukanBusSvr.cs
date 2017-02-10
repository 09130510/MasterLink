using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Data;


namespace NuDotNet.Net
{
    public class NuTukanBusSvrNode : NuSockSvrClientNode
    {
        #region -- private items --
        private int m_TestReqCnt = 0;
        private List<NuTukanBusTopic> m_lst_reg_topics;
        #endregion

        #region --  construct / destruct  --
        public NuTukanBusSvrNode(ref Socket accept_socket, int buf_sz, int hbtItv)
            :base(ref accept_socket, buf_sz, hbtItv)
        {
            m_lst_reg_topics = new List<NuTukanBusTopic>();
        }

        ~NuTukanBusSvrNode() { }
        #endregion

        #region -- property --
        public string LoginID { get ;set; }
        public int TestReqCnt { get { return m_TestReqCnt; } }
        public List<NuTukanBusTopic> RegTopics { get { return m_lst_reg_topics; } set { m_lst_reg_topics = value; } }
        #endregion

        #region -- public function --

        public int TukanBusRecv(ref MemoryStream mStream, out string sHdr, out string sID, out string sBody)
        {
            int iIDLen = 0;
            int iBodyLen = 0;
            int iNeedRcvLen = 0;
            //char MsgType = ' ';
            int lByte = 0;
            byte[] bHdr = null;
            byte[] bBody = null;
            string sTmp = "";

            sHdr = sID = sBody = "";
            
            // rcv Hdr 
            lByte = base.SockRecv(ref mStream, NuDataProtocol.BusHdrSz, 1000);
            if (lByte != NuDataProtocol.BusHdrSz)
                return -1;

            sHdr = Encoding.Default.GetString(mStream.ToArray());

            if (sHdr[NuDataProtocol.BusHdrSz - 1] != NuDataProtocol.protocol_end)
            {
                return -1;
            }

            if (!int.TryParse(sHdr.Substring(1, 4), out iIDLen))
                return -1;

            if (!int.TryParse(sHdr.Substring(5, 4), out iBodyLen))
                return -1;
            iNeedRcvLen = iIDLen + iBodyLen;
            // recv Body
            if (iNeedRcvLen > 0)
            {
                lByte = base.SockRecv(ref mStream, iNeedRcvLen, 1000);
                if (lByte !=iNeedRcvLen)
                    return -1;

                //sTmp = Encoding.Default.GetString(mStream.ToArray());
                //sID = sTmp.Substring(0, iIDLen);
                //sBody = sTmp.Substring(iIDLen);

                mStream.Seek(0, SeekOrigin.Begin);

                if (iIDLen > 0)
                {
                    bHdr = new byte[iIDLen];
                    mStream.Read(bHdr, 0, iIDLen);
                    sID = Encoding.Default.GetString(bHdr);
                }
                else
                {
                    sID = "";
                }

                if (iBodyLen > 0)
                {
                    bBody = new byte[iBodyLen];
                    mStream.Read(bBody, 0, iBodyLen);
                    sBody = Encoding.Default.GetString(bBody);
                }
                else
                {
                    sBody = "";
                }
            }
            else
            {
                return -1;
            }

            return (NuDataProtocol.BusHdrSz + iNeedRcvLen);
        }

        public int TukanBusSend(ref MemoryStream mStream)
        {
            int iRtn = 0;

            iRtn = base.SockSend(mStream.ToArray(), 1000);
            if (iRtn <= 0)
                return -1;

            return iRtn;
        }

        public int SockLoginConfirm()
        {
            MemoryStream mStream = new MemoryStream();
            if (NuDataProtocol.genTukanBusStream(LoginID, NuTukanBusMsgType.Login, "Login ok!", ref mStream))
            {
                return TukanBusSend(ref mStream);
            }
                
            return -1;
        }

        public void SetTestReq(int Cnt)
        {
            m_TestReqCnt = Cnt;
        }
        #endregion

    }

    public class NuTukanBusTopic
    {
        private HashSet<NuTukanBusSvrNode> m_ht_reg_nodes;

        public string TopicName { get; set; }
        public HashSet<NuTukanBusSvrNode> Nodes { get { return m_ht_reg_nodes; } }
        public int Count { get { return m_ht_reg_nodes.Count; } }

        public NuTukanBusTopic()
        {
            m_ht_reg_nodes = new HashSet<NuTukanBusSvrNode>();
        }

        public bool Register(NuTukanBusSvrNode Node)
        {

            if (m_ht_reg_nodes.Add(Node))
            {
                Node.RegTopics.Add(this);
                return true;
            }
            else
                return false;
        }

        public bool UnRegister(NuTukanBusSvrNode Node)
        {
            Node.RegTopics.Remove(this);
            return m_ht_reg_nodes.Remove(Node);
        }

        public void Clear()
        {
            foreach (NuTukanBusSvrNode Node in m_ht_reg_nodes)
            {
                UnRegister(Node);
            }

            m_ht_reg_nodes.Clear();
        }
    }

    //---------------------------------------------------------
    // TukanBusSvr
    //---------------------------------------------------------
    public class NuTukanBusSvParam : EventArgs
    {
        private NuTukanBusSvrNode m_client;
        private object m_argu;

        public NuTukanBusSvrNode Client { get { return m_client; } }
        public object Argu { get { return m_argu; } }
        public string LoginID { get { return (m_client == null) ? "" : m_client.LoginID; } }

        public NuTukanBusSvParam(NuTukanBusSvrNode oClient, object oObj)
        {
            m_client = oClient;
            m_argu = oObj;
        }
    }

    public class NuTukanBusSvr : NuSocketServer
    {
        #region --- private items ---
        private Dictionary<string, NuTukanBusSvrNode> m_Nodes;
        private Dictionary<string, NuTukanBusTopic> m_Topics;
        private List<NuTukanBusSvrNode> m_delay_clear_nodes;
        private object m_Argu = null;
        //private MemoryStream m_timer_stream = new MemoryStream();
        //private MemoryStream m_stream = new MemoryStream();
        private NuList<MemoryStream> m_stream_buf = null;
        private NuList<_SendWorkParam> m_param_buf = null;
        #endregion 

        #region --- public property ---
        public object ArguObj { set { m_Argu = value; } }
        #endregion

        #region --- public event ---
        public delegate bool dlgBusSvr(NuTukanBusSvParam oObj);
        public event dlgBusSvr OnLoginEv;
        public event dlgBusSvr OnDisconnectEv;
        public event dlgBusSvr OnDataArrivedEv;
        public event dlgSockServerLog OnLogEv;

        //public event OnTimeOut OnTimeOutEv;
        //public event OnDataArrived OnDataArrivedEv;
        //public event OnHBT OnHBTSendEv;
        //public event OnHBT OnHBTRecvEv;
        //public event OnLogDump OnLogDumpEv;

        
        #endregion

        #region --  construct / destruct  --
        public NuTukanBusSvr(string listen_ip, int listen_port)
            : base(listen_ip, listen_port)
        {
            //m_stream_buf = new NuList<MemoryStream>(() => { return new MemoryStream(); },
            //                                        (object obj) => { ((MemoryStream)obj).Dispose(); });
            //m_param_buf = new NuList<_SendWorkParam>(() => { return new _SendWorkParam(); },
            //                                        (object oObj) => { ((_SendWorkParam)oObj).Dispose(); });

            m_stream_buf = new NuList<MemoryStream>();
            m_param_buf = new NuList<_SendWorkParam>();

            m_Nodes = new Dictionary<string, NuTukanBusSvrNode>();
            m_Topics = new Dictionary<string, NuTukanBusTopic>();
            m_delay_clear_nodes = new List<NuTukanBusSvrNode>();

            OnAcceptEv += new dlgSockServerOnAcceptEvent(NuTukanBusSvr_OnAcceptEv);
            OnClientConnectEv += new dlgSockServerEvent(NuTukanBusSvr_OnClientConnectEv);
            OnClientDisconnectEv += new dlgSockServerEvent(NuTukanBusSvr_OnClientDisconnectEv);
            OnClientTimerEv += new dlgSockServerTimeOutEvent(NuTukanBusSvr_OnClientTimerEv);
            OnClientDataArrivedEv += new dlgSockServerEvent(NuTukanBusSvr_OnClientDataArrivedEv);
        }
        #endregion

        #region --- private function ---
        private bool _AddToNodes(NuTukanBusSvrNode Node)
        {
            if (m_Nodes.ContainsKey(Node.LoginID))
                return false;
            m_Nodes.Add(Node.LoginID, Node);
            return true;
        }

        private void _RmvNodes(string NodeName)
        {
            if (!m_Nodes.ContainsKey(NodeName))
                return;
            m_Nodes.Remove(NodeName);
            return;
        }

        private NuTukanBusSvrNode _FindNode(string NodeName)
        {
            if (!m_Nodes.ContainsKey(NodeName))
                return null;
            return m_Nodes[NodeName];
        }

        private bool _RegTopic(string TopicName, NuTukanBusSvrNode Node)
        {
            NuTukanBusTopic Topic = null;
            bool bRtn = false;
            if (!m_Topics.ContainsKey(TopicName))
            {
                Topic = new NuTukanBusTopic();
                Topic.TopicName = TopicName;
                m_Topics.Add(TopicName, Topic);
            }
            else
            {
                Topic = m_Topics[TopicName];
            }

            lock (Topic)
                bRtn = Topic.Register(Node);
            return bRtn;
        }

        private void _UnRegTopic(string TopicName, NuTukanBusSvrNode Node)
        {
            NuTukanBusTopic Topic = null;
            if (!m_Topics.ContainsKey(TopicName))
                return;
            m_Topics.Remove(TopicName);

            Topic = m_Topics[TopicName];
            lock (Topic)
                Topic.UnRegister(Node);
        }

        private void _UnRegTopic(string TopicName)
        {
            NuTukanBusTopic Topic = null;
            if (!m_Topics.ContainsKey(TopicName))
                return;
            m_Topics.Remove(TopicName);

            lock(Topic)
                Topic.Clear();

         
        }
        
        private void _UnRegTopic(NuTukanBusSvrNode Node)
        {
            NuTukanBusTopic[] aryTopics = new NuTukanBusTopic[Node.RegTopics.Count];
            Node.RegTopics.CopyTo(aryTopics, 0);

            foreach (NuTukanBusTopic Topic in aryTopics)
            {
                lock (Topic)
                    Topic.UnRegister(Node);
            }
        }

        private NuTukanBusTopic _FindTopic(string TopicName)
        {
            if (!m_Topics.ContainsKey(TopicName))
            {
                return null;
            }
            return m_Topics[TopicName];
        }

        #endregion

        #region --- callback function ---
        private NuSockSvrClientNode NuTukanBusSvr_OnAcceptEv(ref Socket sockfd, int iDefaultBufSz, int iTimeIntervalSec)
        {
            return (NuSockSvrClientNode)(new NuTukanBusSvrNode(ref sockfd, iDefaultBufSz, iTimeIntervalSec));

        }

        bool NuTukanBusSvr_OnClientConnectEv(NuSockSvrClientNode Client)
        {
            bool bRtn = false;//, bRtn = false;
            //MemoryStream mStream = new MemoryStream();
            MemoryStream mStream = m_stream_buf.Pop();
            NuTukanBusSvrNode oBusNode = (NuTukanBusSvrNode)Client;
            string sHdr, sID, sBody;
            int iRcvLen = 0;
            NuTukanBusSvParam oParam = null;

            iRcvLen = oBusNode.TukanBusRecv(ref mStream, out sHdr, out sID, out sBody);
            if (sHdr[0] != NuTukanBusMsgType.Login || sID.Length == 0)
            {
                goto EXIT;
            }
            oBusNode.LoginID = sID;

            #region OnLogin event 
            if (OnLoginEv != null)
            {
                oParam = new NuTukanBusSvParam(oBusNode, m_Argu);
                bRtn = OnLoginEv(oParam);
            }
            else
            {
                bRtn = true;
            }
            #endregion

            if (bRtn)
            {
                lock (m_Nodes)
                {
                    if (_FindNode(oBusNode.LoginID) != null)
                    {
                        bRtn = false;
                    }
                    else
                    {
                        #region Send Login complete message to client
                        if (!(bRtn = NuDataProtocol.genTukanBusStream(oBusNode.LoginID, NuTukanBusMsgType.Login, "Login Ok!", ref mStream)))
                        {
                            goto EXIT;
                        }
                        else
                        {
                            if (oBusNode.TukanBusSend(ref mStream) <= 0)
                            {
                                bRtn = false;
                                goto EXIT;
                            }
                        }
                        #endregion

                        _AddToNodes(oBusNode);
                        bRtn = true;
                    }
                }
            }

        EXIT:
            if (!bRtn)
                oBusNode.LoginID = "";

            m_stream_buf.Push(ref mStream);

            return bRtn;
        }

        bool NuTukanBusSvr_OnClientDisconnectEv(NuSockSvrClientNode Client)
        {
            NuTukanBusSvrNode oBusNode = (NuTukanBusSvrNode)Client;
            NuTukanBusSvParam oParam = null;
            if (OnDisconnectEv != null)
            {
                oParam = new NuTukanBusSvParam(oBusNode, m_Argu);
                OnDisconnectEv(oParam);
            }

            if (oBusNode.LoginID.Length > 0)
            {
                lock (m_Nodes)
                    _RmvNodes(oBusNode.LoginID);

                lock (m_Topics)
                    _UnRegTopic(oBusNode);

                lock (m_delay_clear_nodes)
                    m_delay_clear_nodes.Add(oBusNode);
            }
            return true;
        }

        #region client node delay clear 
        private void _ClearWork(object oObj)
        {
            int iCnt = 0;
            NuTukanBusSvrNode node = null;
            List<NuTukanBusSvrNode> nodes = new List<NuTukanBusSvrNode>();
            DateTime Now = (DateTime)oObj;

            lock (m_delay_clear_nodes)
            {
                iCnt = m_delay_clear_nodes.Count;
                if (iCnt > 0)
                {
                    for (int i = 0; i < iCnt; i++)
                    {
                        node = m_delay_clear_nodes[i];
                        if ((Now - node.LastNotAliveTime).TotalSeconds > 5)
                        {
                            nodes.Add(node);
                            
                        }

                    }
                }
            }

            foreach (NuTukanBusSvrNode n in nodes)
            {
                lock(m_delay_clear_nodes)
                    m_delay_clear_nodes.Remove(node);

                if (n.IsAlive)
                    n.Disconnect();

                n.Dispose();

            }
            nodes.Clear();
        }
        #endregion

        void NuTukanBusSvr_OnClientTimerEv(NuSockSvrClientNode Client, DateTime LastRecvTime, DateTime LastSendTime)
        {
            NuTukanBusSvrNode oBusNode = (NuTukanBusSvrNode)Client;
            DateTime Now = DateTime.Now;
            MemoryStream mStream = null;
            double TimeGap_Send = (Now - LastSendTime).TotalSeconds;
            double TimeGap_Recv = (Now - LastRecvTime).TotalSeconds;

            if (Now.Second % 5 == 0)
                this.Invoke(Now, _ClearWork);

            //lock (m_Topics)
            //{
            //    foreach (NuTukanBusTopic topic in m_Topics.Values)
            //    {
            //        System.Diagnostics.Debug.WriteLine(string.Format("Topic[{0}] [{1}]", 
            //            topic.TopicName, topic.Nodes.Count));
            //    }
            //}

            if (TimeGap_Recv >= (2 * oBusNode.HBTInterval))
            {
                oBusNode.Disconnect();
            }
            else if (TimeGap_Recv - oBusNode.HBTInterval >= 1)
            {   // Send test request
                if (oBusNode.TestReqCnt == 0)
                {
                    mStream = m_stream_buf.Pop();
                    if (mStream == null)
                        if (OnLogEv != null)
                            OnLogEv("Memory not enough!");

                    oBusNode.SetTestReq(1);
                    NuDataProtocol.genTukanBusStream(oBusNode.LoginID,
                        NuTukanBusMsgType.TestReq, "test req", ref mStream);

                    if (oBusNode.TukanBusSend(ref mStream) < 0)
                    {
                        oBusNode.Disconnect();
                        m_stream_buf.Push(ref mStream);
                        return;
                    }

                    m_stream_buf.Push(ref mStream);
                }
            }

            if (TimeGap_Send - oBusNode.HBTInterval >= -1)
            {   // Send HBT
                mStream = m_stream_buf.Pop();
                if (mStream == null)
                    if (OnLogEv != null)
                        OnLogEv("Memory not enough!");

                NuDataProtocol.genTukanBusStream(oBusNode.LoginID,
                 NuTukanBusMsgType.HBT, "Heartbeat", ref mStream);

                if (oBusNode.TukanBusSend(ref mStream) < 0)
                {
                    oBusNode.Disconnect();
                    m_stream_buf.Push(ref mStream);
                    return;
                }
                m_stream_buf.Push(ref mStream);
            }
            return;
        }

        #region send to grouop callback function
        internal class _SendWorkParam : IDisposable
        {
            private bool bDisposed = false;
            public MemoryStream Stream = new MemoryStream();
            public NuTukanBusSvrNode SvrNode = null;
            public _SendWorkParam() { }
            ~_SendWorkParam() { }
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
                    Stream.Dispose();
                }
            }
        }

        private void _SendWork(object oObj)
        {
            _SendWorkParam Param = (_SendWorkParam)oObj;
            Param.SvrNode.TukanBusSend(ref Param.Stream);
            m_param_buf.Push(ref Param);
        }
        #endregion

        bool NuTukanBusSvr_OnClientDataArrivedEv(NuSockSvrClientNode Client)
        {
            NuTukanBusSvrNode oBusNode = (NuTukanBusSvrNode)Client;
            MemoryStream mStream = new MemoryStream();
            int nRead = 0;
            int nBodySz = 0;
            string sHdr = "";
            string sBody = "";
            string sID = "";
            NuTukanBusSvParam oParam = null;
            char[] Delimit = new char[] {','};
            string[] aryID = null;
            //while (TukanCli.Client.Available > 0)
            while (oBusNode.AvailableDataSz > 0)
            {
                nRead = oBusNode.TukanBusRecv(ref mStream, out sHdr, out sID, out sBody);
                if (nRead < 0)
                {
                    return false;
                }

                #region 訊息分類
                System.Diagnostics.Debug.WriteLine(string.Format("### {2} Svr [DEBUG] Hdr[{0}] Body[{1}]",
                    sHdr, sBody, oBusNode.LoginID));
                switch (sHdr[0])
                {
                    case NuTukanBusMsgType.Msg:
                        #region Message Handle
                        if (sID.Length > 0)
                        {
                            aryID = sID.Split(Delimit);
                            foreach (string sSubID in aryID)
                            {
                                if (sSubID[0] == '@')
                                {   // send to group 

                                    //_GrpParam param = new _GrpParam(oBusNode.LoginID, sSubID, sBody);
                                    //this.Invoke(param, _SendToGrp);
                                    #region mark
                                    NuTukanBusTopic Topic = null;
                                    lock (m_Topics)
                                        Topic = _FindTopic(sSubID);

                                    if (Topic != null)
                                    {
                                        lock (Topic)
                                        {
                                            foreach (NuTukanBusSvrNode node in Topic.Nodes)
                                            {
                                                _SendWorkParam param = m_param_buf.Pop();
                                                //_SendWorkParam param = new _SendWorkParam();
                                                NuDataProtocol.genTukanBusStream(sSubID, NuTukanBusMsgType.Msg,
                                                                              sBody, ref param.Stream);
                                                param.SvrNode = node;
                                                this.Invoke(param, _SendWork);

                                            }
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {   // send to target one
                                    NuTukanBusSvrNode TargetNode = null;
                                    lock (m_Nodes)
                                        TargetNode = _FindNode(sSubID);

                                    if (TargetNode != null)
                                    {
                                        NuDataProtocol.genTukanBusStream(oBusNode.LoginID, NuTukanBusMsgType.Msg,
                                                                      sBody, ref mStream);
                                        TargetNode.TukanBusSend(ref mStream);
                                    }

                                }
                            }
                        }
                        else
                        {
                            if (NuDataProtocol.genTukanBusStream(oBusNode.LoginID, NuTukanBusMsgType.Reject,
                                                               "Send Msg Fail", ref mStream))
                                oBusNode.TukanBusSend(ref mStream);
                        }
                        #endregion
                        break;
                    case NuTukanBusMsgType.SubscribeReq:
                        #region register Topic
                        if (sID.Length > 0)
                        {
                            aryID = sID.Split(Delimit);
                            foreach (string sSubID in aryID)
                            {
                                if (sSubID.Length == 0)
                                    continue;
                                if (sSubID[0] == '@')
                                {
                                    _RegTopic(sSubID, oBusNode);
                                }
                            }

                            if (NuDataProtocol.genTukanBusStream(sID, NuTukanBusMsgType.SubscribeCfm,
                                                               "Reg Sub OK", ref mStream))
                            {
                                oBusNode.TukanBusSend(ref mStream);
                            }
                            else
                                oBusNode.Disconnect();
                        }
                        else
                        {
                            if (NuDataProtocol.genTukanBusStream(oBusNode.LoginID, NuTukanBusMsgType.Reject,
                                                               "Reg Sub Fail", ref mStream))
                                oBusNode.TukanBusSend(ref mStream);
                        }
                        #endregion
                        break;
                    case NuTukanBusMsgType.Logout:
System.Diagnostics.Debug.WriteLine("==== [DEBUG] ===== logout : " + sID);
                        oBusNode.Disconnect();
                        lock(m_Nodes)
                            _RmvNodes(sID);
                        lock(m_Topics)
                            _UnRegTopic(oBusNode);

                        oBusNode.LoginID = "";

                        lock (m_delay_clear_nodes)
                            m_delay_clear_nodes.Add(oBusNode);
                        break;
                    case NuTukanBusMsgType.HBT:
                        oBusNode.SetTestReq(0);
                        break;
                    case NuTukanBusMsgType.TestReq:
                        #region send heartbeat
                        if (NuDataProtocol.genTukanBusStream(sID, NuTukanBusMsgType.HBT, "", ref mStream))
                        {
                            oBusNode.TukanBusSend(ref mStream);
                        }
                        else
                        {
                            oBusNode.Disconnect();
                        }
                        #endregion
                        break;
                    default:
                        if (OnDataArrivedEv != null)
                        {
                            oParam = new NuTukanBusSvParam(oBusNode, m_Argu);
                            OnDataArrivedEv(oParam);
                        }
                        break;
                }
                #endregion
            }

            return true;
        }

        #endregion

    }
}
