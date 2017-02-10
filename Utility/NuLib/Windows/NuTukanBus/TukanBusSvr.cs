using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Text;
using System.Data;
using NuDotNet;
using NuDotNet.SocketUtil;

namespace NuTukanBus
{
    public class TukanBusSvr : INuSocketHandler
    {
        internal class _TukanNode_t
        {
            public int SockFDHdl { get; set; }
            public string LoginID { get; set; }
            public NuSocketParam Node { get; private set; }

            private int m_timeout_cnt = 0;

            public _TukanNode_t(NuSocketParam oParam)
            {
                Node = oParam;
            }

            public void ResetTimeout()
            {
                m_timeout_cnt = 0;
            }

            public void SetTimeout()
            {
                m_timeout_cnt++;
            }

            public bool NeedDisconnect()
            {

                if (m_timeout_cnt >= 2)
                    return true;

                return false;
            }

            public int SendHBT(ref NuSocketParam oParam)
            {
                MemoryStream mStream = new MemoryStream();
                TukanBusMsg oMsg = new TukanBusMsg();
                oMsg.MsgType = TukanBusMsgType.HBT;
                oMsg.ID = LoginID;
                TukanBusProtol.Pack(ref oMsg, ref mStream);
                return oParam.Send(ref mStream, 1);
            }

            public int SendTestReq(ref NuSocketParam oParam)
            {
                MemoryStream mStream = new MemoryStream();
                TukanBusMsg oMsg = new TukanBusMsg();
                oMsg.MsgType = TukanBusMsgType.TestReq;
                oMsg.ID = LoginID;
                TukanBusProtol.Pack(ref oMsg, ref mStream);
                return oParam.Send(ref mStream, 1);
            }

            public bool Send(TukanBusMsg oMsg)
            {
                MemoryStream mStream = new MemoryStream();
                int iByte = 0;

                TukanBusProtol.Pack(ref oMsg, ref mStream);
                if ((iByte = Node.Send(ref mStream)) < 0)
                    return false;

                return true;
            } 
        }

        internal class _Topic_t
        {
            #region --- private variable ---
            private List<_TukanNode_t> m_reg_nodes = null;
            private object m_lock = new object();
            dlgTukanExceptionAlert AlertCB = null;
            #endregion

            public List<_TukanNode_t> RegNodes { get { return m_reg_nodes; } }

            #region --- construct / destruct ---
            public _Topic_t(dlgTukanExceptionAlert AlertFn)
            {
                m_reg_nodes = new List<_TukanNode_t>();
                AlertCB = AlertFn;
            }
            ~_Topic_t() { }
            #endregion

            #region --- public method ---
            public void Register(ref _TukanNode_t oNode)
            {
                lock (m_lock)
                {
                    if (!m_reg_nodes.Contains(oNode))
                        m_reg_nodes.Add(oNode);
                }
            }

            public void UnRegister(ref _TukanNode_t oNode)
            {
                lock (m_lock)
                {
                    if (m_reg_nodes.Contains(oNode))
                        m_reg_nodes.Remove(oNode);
                }
            }

            public void Send(TukanBusMsg oMsg)
            {
                MemoryStream mStream = new MemoryStream();

                lock (m_lock)
                {
                    int iByte = 0;
                    TukanBusProtol.Pack(ref oMsg, ref mStream);
//NuDebug.WriteLine(String.Format("DEBUG broadcast count = {0}", m_reg_nodes.Count));
                    foreach (_TukanNode_t oNode in m_reg_nodes)
                    {
//                        try
//                        {
                            if ((iByte = oNode.Node.Send(ref mStream)) < 0)
                            {
                                AlertCB(String.Format(
                                    "{0} [{1}] send data fail! [{2}]", oNode.LoginID, oNode.Node.RemoteIP, iByte.ToString()));
                            }
//                        }
//                        catch
//                        {
//                            continue;
//                        }
                    }
                }

            }

            #endregion
        }

        #region --- private variable ---
        private object m_lock = new object();
        private Hashtable m_htbSess = null;
        private Dictionary<string, _TukanNode_t> m_dic_nodes = null;
        private Dictionary<string, _Topic_t> m_dic_topics = null;
        #endregion

        #region --- public event ---
        public event dlgTukanCB OnConnectEv;
        public event dlgTukanCB OnDisconnectEv;
        public event dlgTukanCB OnLoginCompleteEv;
        public event dlgTukanCB OnMsgArriveEv;
        public event dlgTukanCB OnRejectEv;
        public event dlgTukanCB OnSubscribeCfmEv;
        public event dlgTukanCB OnErrorEv;
        public event dlgTukanCB OnHBTOutEv;
        public event dlgTukanCB OnHBTInEv;
        public event dlgTukanExceptionAlert OnExceptionAlert;
        #endregion

        #region --- private function ---
        #endregion

        #region --- construct / destruct ---
        public TukanBusSvr()
        {
            m_htbSess = new Hashtable();
            m_dic_nodes = new Dictionary<string, _TukanNode_t>();
            m_dic_topics = new Dictionary<string, _Topic_t>();
        }

        ~TukanBusSvr() { }
        #endregion
        
        public NuSocketParam Handle { get; set; }

        #region --- public function ---
        public void Close()
        {
            if (Handle != null)
            {
                Handle.Disconnect();
                Handle.Dispose();
            }
        }
        #endregion

        #region --- implement callback function ---
        public bool OnConnectFail(NuSocketParam oParam)
        {
            return true;
        }

        public bool OnConnected(NuSocketParam oParam)
        {
            bool bRtn = true;
            TukanBusMsg oMsg = new TukanBusMsg();
            MemoryStream mStream = new MemoryStream();

            oParam.SetTimeoutSec(30);
            oParam.SetBlocking(false);

            if (!TukanFn.RcvTukanMsg(ref oParam, ref oMsg))
                return false;

            if (oMsg.MsgType != TukanBusMsgType.Login)
                return false;

            oMsg.LoginID = oMsg.ID;

            if (OnConnectEv != null)
                bRtn = OnConnectEv(oParam, oMsg);

            if (bRtn == true)
            {
                _TukanNode_t oNode = null;
                lock (m_lock)
                {
                    if (!m_htbSess.ContainsKey(oParam.SocketFDHdlNo))
                    {
                        oNode = new _TukanNode_t(oParam);
                        oNode.LoginID = oMsg.LoginID;
                        oNode.SockFDHdl = oParam.SocketFDHdlNo;

                        if (m_dic_nodes.ContainsKey(oNode.LoginID))
                        {
                            _TukanNode_t oNode_Del = m_dic_nodes[oNode.LoginID];
                            m_htbSess.Remove(oNode_Del.Node.SocketFDHdlNo);
                            m_dic_nodes.Remove(oNode.LoginID);

                            foreach (_Topic_t t in m_dic_topics.Values)
                            {
                                t.UnRegister(ref oNode_Del);
                            }
                        }
                        m_htbSess.Add(oParam.SocketFDHdlNo, oNode);
                        m_dic_nodes.Add(oNode.LoginID, oNode);
                    }
                    else
                        oNode = (_TukanNode_t)m_htbSess[oParam.SocketFDHdlNo];

                    oNode.ResetTimeout();
                }

                if (OnLoginCompleteEv != null)
                    OnLoginCompleteEv(oParam, oMsg);
            }
            else
            {
                oMsg.MsgType = TukanBusMsgType.Reject;
                oMsg.SetBody(String.Format("{0} login fail!", oMsg.ID));
            }

            TukanBusProtol.Pack(ref oMsg, ref mStream);
            if (oParam.Send(ref mStream, 1) <= 0)
            {
                bRtn = false;
                oParam.Disconnect();
            }

            return bRtn;
        }

        public void OnDisconnected(NuSocketParam oParam)
        {
            _TukanNode_t oNode = null;
            TukanBusMsg oMsg = new TukanBusMsg();
            // remove
            try
            {
                lock (m_lock)
                {
                    if (m_htbSess.ContainsKey(oParam.SocketFDHdlNo))
                    {
                        oNode = (_TukanNode_t)m_htbSess[oParam.SocketFDHdlNo];
                        m_htbSess.Remove(oParam.SocketFDHdlNo);
                        m_dic_nodes.Remove(oNode.LoginID);

                        foreach (_Topic_t t in m_dic_topics.Values)
                        {
                            t.UnRegister(ref oNode);
                        }
                        oMsg.LoginID = oNode.LoginID;
                    }
                }

                if (OnDisconnectEv != null)
                {
                    oMsg.ErrorMsg = String.Format("{0} disconnect", oParam.RemoteIP);
                    OnDisconnectEv(oParam, oMsg);
                }
            }
            catch (Exception ex)
            {
                if (OnExceptionAlert != null)
                    OnExceptionAlert(ex.Message);
            }
            return;
        }

        private bool _SendReject(ref _TukanNode_t oNode, TukanBusMsg oMsg)
        {
            int iRtn = 0;
            MemoryStream mStream = new MemoryStream();
            oMsg.MsgType = TukanBusMsgType.Reject;
            TukanBusProtol.Pack(ref oMsg, ref mStream);
            iRtn = oNode.Node.Send(ref mStream, 1);
            return (iRtn > 0) ? true : false;
        }

        private bool _SendSubscribeCfm(ref _TukanNode_t oNode, TukanBusMsg oMsg)
        {
            int iRtn = 0;
            MemoryStream mStream = new MemoryStream();
            oMsg.MsgType = TukanBusMsgType.SubscribeCfm;
            TukanBusProtol.Pack(ref oMsg, ref mStream);
            iRtn = oNode.Node.Send(ref mStream, 1);
            return (iRtn > 0) ? true : false;
        }

        public void OnDataArrived(NuSocketParam oParam)
        {
            TukanBusMsg oMsg = new TukanBusMsg();
            _TukanNode_t oNode = null;
            _TukanNode_t oTargetNode = null;
            String[] aryTopic = null;
            _Topic_t oTopic = null;

            lock (m_lock)
            {
                if (!m_htbSess.ContainsKey(oParam.SocketFDHdlNo))
                    return;
                oNode = (_TukanNode_t)m_htbSess[oParam.SocketFDHdlNo];
                oMsg.LoginID = oNode.LoginID;
            }

            if (TukanFn.RcvTukanMsg(ref oParam, ref oMsg))
            {
                switch (oMsg.MsgType)
                {
                    case TukanBusMsgType.Login:
                        // login message should be recv in connected event
                        if (OnErrorEv != null)
                            OnErrorEv(oParam, oMsg);
                        break;
                    case TukanBusMsgType.Logout:
                        if (OnMsgArriveEv != null)
                            OnMsgArriveEv(oParam, oMsg);

                        oParam.Disconnect();
                        break;
                    case TukanBusMsgType.TestReq:
                        if (OnHBTInEv != null)
                            OnHBTInEv(oParam, oMsg);
                        oNode.ResetTimeout();
                        _SendHBT(ref oParam);
                        break;
                    case TukanBusMsgType.HBT:
                        if (OnHBTInEv != null)
                            OnHBTInEv(oParam, oMsg);
                        oNode.ResetTimeout();
                        break;
                    case TukanBusMsgType.SubscribeReq:
                        if (OnMsgArriveEv != null)
                            OnMsgArriveEv(oParam, oMsg);

                        if (oMsg.ID.Length < 0)
                        {
                            _SendReject(ref oNode, oMsg);
                        }
                        else
                        {
                            aryTopic = oMsg.ID.Split(TukanBusProtol.TopicDelimit);
                            foreach (String sTopic in aryTopic)
                            {
                                if (sTopic[0] != TukanBusProtol.TopicGroupLeader)
                                    continue;
                                lock (m_lock)
                                {
                                    if (m_dic_topics.ContainsKey(sTopic))
                                        oTopic = m_dic_topics[sTopic];
                                    else
                                    {
                                        oTopic = new _Topic_t(OnExceptionAlert);
                                        m_dic_topics.Add(sTopic, oTopic);
                                    }
                                }
                                oTopic.Register(ref oNode);
                            }
//                            NuDebug.WriteLine("DEBUG - substring confirm " + oNode.LoginID);

                            _SendSubscribeCfm(ref oNode, oMsg);
                        }
                        break;
                    case TukanBusMsgType.Msg:
                        if (OnMsgArriveEv != null)
                            OnMsgArriveEv(oParam, oMsg);

                        // TODO send to target 
                        if (oMsg.ID.Length < 0)
                        {
                            _SendReject(ref oNode, oMsg);
                        }
                        else
                        {
                            aryTopic = oMsg.ID.Split(TukanBusProtol.TopicDelimit);
                            foreach (String sTopic in aryTopic)
                            {
                                if (sTopic[0] == TukanBusProtol.TopicGroupLeader)
                                {   // broadcast
                                    lock (m_lock)
                                    {
                                        if (!m_dic_topics.ContainsKey(sTopic))
                                            continue;
                                        oTopic = m_dic_topics[sTopic];
                                    }

                                    oMsg.ID = sTopic;
                                    oTopic.Send(oMsg);
                                }
                                else
                                {   // send to target one
                                    lock (m_lock)
                                    {
                                        if (!m_dic_nodes.ContainsKey(sTopic))
                                        {
                                            oTargetNode = null;
                                        }
                                        else
                                        {
                                            oTargetNode = m_dic_nodes[sTopic];
                                        }
                                    }

                                    if (oTargetNode == null)
                                    {
                                        TukanBusMsg oRejectMsg = new TukanBusMsg();
                                        oRejectMsg.ID = sTopic;
                                        oRejectMsg.SetBody("Target not exists.");
                                        _SendReject(ref oNode, oMsg);
                                    }
                                    else
                                    {
                                        oMsg.ID = oMsg.LoginID;
                                        if (!oTargetNode.Send(oMsg))
                                        {
                                            TukanBusMsg oRejectMsg = new TukanBusMsg();
                                            oRejectMsg.ID = sTopic;
                                            oRejectMsg.SetBody("Send to target fail.");
                                            _SendReject(ref oNode, oMsg);
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        if (OnErrorEv != null)
                            OnErrorEv(oParam, oMsg);
                        break;
                            
                }
                
            }
            else
            {
                if (OnErrorEv != null)
                {
                    oMsg.ErrorMsg = "recv message fail!";
                    OnErrorEv(oParam, oMsg);
                }
                oParam.Disconnect(false);
            }

            return;
        }

        public void OnRemoteTimeout(NuSocketParam oParam)
        {
            _TukanNode_t oNode = null;
            lock (m_lock)
            {
                if (!m_htbSess.ContainsKey(oParam.SocketFDHdlNo))
                    return;
                oNode = (_TukanNode_t)m_htbSess[oParam.SocketFDHdlNo];
            }

            if (oNode.NeedDisconnect())
            {
                oParam.Disconnect();
            }
            else
            {
                // send test req
                MemoryStream mStream = new MemoryStream();
                TukanBusMsg oMsg = new TukanBusMsg();
                oMsg.MsgType = TukanBusMsgType.TestReq;
                oMsg.ID = oNode.LoginID;
                TukanBusProtol.Pack(ref oMsg, ref mStream);
                if (oParam.Send(ref mStream, 1) > 0)
                {
                    if (OnHBTOutEv != null)
                        OnHBTOutEv(oParam, oMsg);
                }

                oNode.SetTimeout();
                oNode.SendTestReq(ref oParam);
            }
        }

        private int _SendHBT(ref NuSocketParam oParam)
        {
            int iRtn = 0;
            _TukanNode_t oNode = null;
            lock (m_lock)
            {
                if (!m_htbSess.ContainsKey(oParam.SocketFDHdlNo))
                    return -1;
                oNode = (_TukanNode_t)m_htbSess[oParam.SocketFDHdlNo];
            }

            MemoryStream mStream = new MemoryStream();
            TukanBusMsg oMsg = new TukanBusMsg();
            oMsg.MsgType = TukanBusMsgType.HBT;
            oMsg.ID = oNode.LoginID;
            TukanBusProtol.Pack(ref oMsg, ref mStream);
            if ((iRtn = oParam.Send(ref mStream, 1)) > 0)
            {
                if (OnHBTOutEv != null)
                    OnHBTOutEv(oParam, oMsg);
            }
            return iRtn;
        }

        public void OnLocalTimeout(NuSocketParam oParam)
        {
            if (_SendHBT(ref oParam) < 0)
            {
                if (OnExceptionAlert != null)
                {
                    OnExceptionAlert(
                        String.Format("Send HBT to {0} Fail!", oParam.RemoteIP)
                        );
                }
                oParam.Disconnect();
            }
        }

        public void OnException(string sMsg)
        {
            if (OnExceptionAlert != null)
                OnExceptionAlert(sMsg);
        }
        #endregion
    }
}
