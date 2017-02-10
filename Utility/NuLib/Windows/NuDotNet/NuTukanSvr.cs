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
    public class NuTukanSvrNode : NuSockSvrClientNode
    {
        #region -- private items --

        #region protocal
        private string mp_local_hdr_id = "";
        private string mp_remote_hdr_id = "";
        private string mp_login_id = "";
        #endregion

        //private DateTime m_LastHBTTime = DateTime.Now;
        //private bool m_IsTimeOut = false;

        private int m_TestReqCnt = 0;
        #endregion

        #region --  construct / destruct  --
        public NuTukanSvrNode(ref Socket accept_socket, int buf_sz, int hbtItv)
            :base(ref accept_socket, buf_sz, hbtItv)
        {
        }

        ~NuTukanSvrNode() { }
        #endregion

        #region -- property --
        public string LoginID { get { return mp_login_id; } set { mp_login_id = value; } }
        public string ClientHdr { get { return mp_remote_hdr_id; } set { mp_remote_hdr_id = value; } }
        public string ServerHdr { get { return mp_local_hdr_id; } set { mp_local_hdr_id = value; } }
        //public bool IsTimeout { get { return m_IsTimeOut; } }
        public int TestReqCnt { get { return m_TestReqCnt; } }
        #endregion

        #region -- public function --

        public int TukanSockRecv(ref MemoryStream mStream, int length)
        {
            return base.SockRecv(ref mStream, length);
        }

        public int TukanSockRecv(ref MemoryStream mStream, int length, int TimeMS)
        {
            return base.SockRecv(ref mStream, length, TimeMS);
        }

        public int TukanSockSend(ref StringBuilder sbMsg)
        {
            int iRtn = 0;
            int sMsgLen = ASCIIEncoding.Default.GetByteCount(sbMsg.ToString());
            byte[] bMsg = Encoding.Default.GetBytes(sbMsg.ToString());
            byte[] bHdr = Encoding.Default.GetBytes(NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.Msg, sMsgLen));

            MemoryStream mStream = new MemoryStream(sMsgLen + NuDataProtocol.HdrSz);

            mStream.Write(bHdr, 0, NuDataProtocol.HdrSz);
            mStream.Write(bMsg, 0, bMsg.Length);

            iRtn = base.SockSend(mStream.ToArray(), 1000);
            if (iRtn <= 0)
                return -1;

            return iRtn;
        }

        public int SockLoginConfirm()
        {
            int iSndCnt = 0;
            //byte[] bMsg = Encoding.Default.GetBytes(NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.Login, 0));
            byte[] bMsg = Encoding.Default.GetBytes(NuDataProtocol.genHdr(mp_local_hdr_id, NuMsgType.Login, 0));
            //System.Diagnostics.Debug.WriteLine(
            //    string.Format("Remote[{0}] Local[{1}]{2}", mp_remote_hdr_id, mp_local_hdr_id, mp_login_id)
            //    );
            if (bMsg.Length > 0)
            {
                iSndCnt = this.SockSend(bMsg);
            }
            //m_IsTimeOut = false;
            return iSndCnt;
        }

        //public bool NeedHBT(DateTime Now)
        //{
        //    double dGap_HBT = (Now - m_LastHBTTime).TotalSeconds;
        //    double dGap = (Now - base.LastWorkTime).TotalSeconds;

        //    if (dGap > (2 * base.HBTInterval))
        //    {
        //        m_IsTimeOut = true;
        //        return true;
        //    }
        //    else if (dGap_HBT > (base.HBTInterval - 1))
        //    {
        //        m_LastHBTTime = Now;
        //        //System.Diagnostics.Debug.WriteLine(dGap);
        //        return true;
        //    }

        //    return false;

        //}
        public void SetTestReq(int Cnt)
        {
            m_TestReqCnt = Cnt;
        }
        #endregion

    }

    //---------------------------------------------------------
    // DataSvr 
    //---------------------------------------------------------
    public class NuTukanSvr : NuSocketServer
    {
        #region -- private items --
        // protocol
        private int mp_hdr_num = 0;
        #endregion 

        #region for debug
        public delegate void dlgTukanLog(string sMsg);
        public event dlgTukanLog TukanLogEv;
        private void _DEBUG_LOG(string sMsg)
        {
            //if (TukanLogEv != null)
            //    TukanLogEv(sMsg);
        }
        #endregion
        #region --  construct / destruct  --
        public NuTukanSvr(string listen_ip, int listen_port)
            :base(listen_ip, listen_port)
        {
            mp_hdr_num = 0;

            OnAcceptEv += new dlgSockServerOnAcceptEvent(NuTukanSvr_OnAcceptEv);
            OnClientTimerEv += new dlgSockServerTimeOutEvent(NuTukanSvr_OnClientTimerEv);

            OnClientConnectEv += new dlgSockServerEvent(NuTukanSvr_OnClientConnectEv);
            OnClientDisconnectEv += new dlgSockServerEvent(NuTukanSvr_OnClientDisconnectEv);

            OnClientDataArrivedEv += new dlgSockServerEvent(NuTukanSvr_OnClientDataArrivedEv);
        }

        bool NuTukanSvr_OnClientDataArrivedEv(NuSockSvrClientNode Client)
        {
            NuTukanSvrNode TukanCli = (NuTukanSvrNode)Client;
            int nRead = 0;
            int nBodySz = 0;
            string sHdr = "";
            string sBody = "";
            MemoryStream mStream = new MemoryStream();

            //while (TukanCli.Client.Available > 0)
            while (TukanCli.AvailableDataSz > 0)
            {
                nRead = TukanCli.TukanSockRecv(ref mStream, NuDataProtocol.HdrSz);
                if (nRead != NuDataProtocol.HdrSz)
                    return false;

                sHdr = Encoding.Default.GetString(mStream.ToArray());
                if (!int.TryParse(sHdr.Substring(0, 4), out nBodySz))
                    return false;

                if (nBodySz > 0)
                {
                    nRead = TukanCli.TukanSockRecv(ref mStream, nBodySz);
                    if (nRead != nBodySz)
                        return false;
                    sBody = Encoding.Default.GetString(mStream.ToArray());
                }
                else
                {
                    sBody = "";
                }

                #region 訊息分類
                switch (sHdr[4])
                {
                    case NuMsgType.Broadcast:
                        StringBuilder sbTmp = new StringBuilder(sBody);
                        Broadcast(ref sbTmp, TukanCli.ClientFD);
                        break;
                    case NuMsgType.HBT:
                        if (OnHBTRecvEv != null)
                            OnHBTRecvEv(TukanCli, sHdr);
                        TukanCli.SetTestReq(0);
                        //System.Diagnostics.Debug.WriteLine("HBT");
                        break;
                    case NuMsgType.TestReq:
                        _SendHBT(TukanCli);
                        //System.Diagnostics.Debug.WriteLine("TEST");
                        _DEBUG_LOG("Test Req");
                        break;
                    case NuMsgType.Logout:
                        // disconnect
                        //System.Diagnostics.Debug.WriteLine("Logout");
                        _DEBUG_LOG("LogOut");
                        //TukanCli.Disconnect();
                        CloseClinet(TukanCli);
                        break;
                    default:
                        //System.Diagnostics.Debug.WriteLine("MSG");
                        if (OnDataArrivedEv != null)
                        {
                            RcvDataArgs args = new RcvDataArgs(sHdr, sBody);
                            OnDataArrivedEv(TukanCli, args);
                        }
                        break;
                }
                #endregion
            }
            
            return true;
        }

        void NuTukanSvr_OnClientTimerEv(NuSockSvrClientNode Client, DateTime LastRecvTime, DateTime LastSendTime)
        {
            NuTukanSvrNode TukanCli = (NuTukanSvrNode)Client;
            DateTime Now = DateTime.Now;

            double TimeGap_Send = (Now - LastSendTime).TotalSeconds;
            double TimeGap_Recv = (Now - LastRecvTime).TotalSeconds;

            if (TimeGap_Recv >= (2 * TukanCli.HBTInterval))
            {
                TukanCli.Disconnect();
            }
            else if (TimeGap_Recv - TukanCli.HBTInterval >= 1)
            {
                //TODO SendTestReq
                if (TukanCli.TestReqCnt == 0)
                {
                    TukanCli.SetTestReq(1);
                    _SendTestRequest(TukanCli);
                }
                
            }

            if (TimeGap_Send - TukanCli.HBTInterval >= -1)
            {
                _SendHBT(TukanCli);
            }

            //TimeGap = (Now - TukanCli.LastWorkTime).TotalSeconds - TukanCli.HBTInterval;


            //超過設定的HBT區間10秒以上, 就當作TimeOut
            //if (TimeGap > 10)
            //{
            //    if (OnTimeOutEv != null)
            //        OnTimeOutEv(TukanCli);
            //}
            ////else if (TimeGap >= -1)
            ////{
            ////    if (!_SendHBT(TukanCli))
            ////    {
            ////        TukanCli.Disconnect();
            ////    }
            ////}

            //if (TukanCli.NeedHBT(Now))
            //{
            //    if (TukanCli.IsTimeout)
            //        TukanCli.Disconnect();
            //    else if (!_SendHBT(TukanCli))
            //        TukanCli.Disconnect();
            //}
            return;
        }

        NuSockSvrClientNode NuTukanSvr_OnAcceptEv(ref Socket sockfd, int iDefaultBufSz, int iTimeIntervalSec)
        {
            return (NuSockSvrClientNode)(new NuTukanSvrNode(ref sockfd, iDefaultBufSz, iTimeIntervalSec));
            
        }

        bool NuTukanSvr_OnClientConnectEv(NuSockSvrClientNode Client)
        {
            bool bLogin = false;//, bRtn = false;
            MemoryStream mStream = new MemoryStream();
            NuTukanSvrNode TukanCli = null;
            TukanCli = (NuTukanSvrNode)Client;
            
            bLogin = _ChkLogin(TukanCli);

            if (bLogin)
            {
                if (OnConnectEv != null)
                    bLogin = OnConnectEv(TukanCli);

                if (bLogin)
                    _SendHBT(TukanCli);
            }
            else
            {
                _sendLogout(TukanCli);
            }

            return bLogin;
        }

        bool NuTukanSvr_OnClientDisconnectEv(NuSockSvrClientNode Client)
        {
            NuTukanSvrNode TukanCli = (NuTukanSvrNode)Client;
            if (OnDisconnectEv != null)
                OnDisconnectEv(TukanCli);
            return true;
        }

        ~NuTukanSvr() { }
        #endregion

        #region -- private flow / function--
        private bool _ChkLogin(NuTukanSvrNode node)
        {
            //StringBuilder sbMsg = new StringBuilder(128);
            MemoryStream mStream = new MemoryStream();
            string sHdr = "";
            int body_len = 0;
            char MsgType = ' ';
            int lByte = 0;

            _DEBUG_LOG("DEBUG - check Login");
            Socket sockfd = node.Client;
            // rcv Hdr 
            lByte = node.TukanSockRecv(ref mStream, 11, 1000);
            _DEBUG_LOG("DEBUG - get header 1:" + lByte.ToString());
            if (lByte != 11)
                return false;
            
            sHdr = Encoding.Default.GetString(mStream.ToArray());
            _DEBUG_LOG("DEBUG - get header 2:" + sHdr);
            MsgType = sHdr[4];
            if (!int.TryParse(sHdr.Substring(0, 4), out body_len))
                return false;
            node.ClientHdr = sHdr.Substring(5, 5);

            if (MsgType != NuMsgType.Login)
                return false;

            // recv Body
            if (body_len > 0)
            {
                lByte = node.TukanSockRecv(ref mStream, body_len);
                node.LoginID = Encoding.Default.GetString(mStream.ToArray());
            }
            else
                node.LoginID = "";

            // send login confirm
            mp_hdr_num = mp_hdr_num + 1;
            node.ServerHdr = String.Format("{0:00000}", mp_hdr_num);

            if (node.SockLoginConfirm() > 0)
            {
                _DEBUG_LOG("DEBUG - Login confirm true");
                return true;
            }
            else
            {
                _DEBUG_LOG("DEBUG - Login confirm false");
                return false;
            }
        }

        private bool _SendHBT(NuTukanSvrNode node)
        {
            //string sHBT = NuDataProtocol.genHdr(node.ServerHdr, NuMsgType.HBT, 0);
            string sHBT = NuDataProtocol.genHdr(node.ClientHdr, NuMsgType.HBT, 0);
            if (sHBT.Length <= 0)
                return false;
            byte[] bMsg = Encoding.Default.GetBytes(sHBT);

            if (node.SockSend(bMsg, 2000) < 0)
                return false;

            if (OnHBTSendEv != null)
            {
                OnHBTSendEv(node, sHBT);
            }
            return true;
        }

        private bool _SendTestRequest(NuTukanSvrNode node)
        {
            string sTestReq = NuDataProtocol.genHdr(node.ClientHdr, NuMsgType.TestReq, 0);
            if (sTestReq.Length <= 0)
                return false;
            byte[] bMsg = Encoding.Default.GetBytes(sTestReq);

            if (node.SockSend(bMsg, 2000) < 0)
                return false;

            if (OnHBTSendEv != null)
            {
                OnHBTSendEv(node, sTestReq);
            }
            return true;
        }

        private bool _sendLogout(NuTukanSvrNode node)
        {
            int iSnd = 0;
            string sLogout = NuDataProtocol.genHdr(node.ClientHdr, NuMsgType.Logout, 0);
            if (sLogout.Length <= 0)
                return false;
            
            byte[] bMsg = Encoding.Default.GetBytes(sLogout);
            iSnd = node.SockSend(bMsg);
            return true;
        }
        
        #endregion

        #region -- public event --
        public delegate bool OnConnect(NuTukanSvrNode Client);
        public delegate void OnDisConnect(NuTukanSvrNode Client);
        public delegate void OnTimeOut(NuTukanSvrNode Client);
        public delegate void OnDataArrived(NuTukanSvrNode Client, RcvDataArgs e);
        public delegate void OnHBT(NuTukanSvrNode Client, string sMsg);
        public delegate void OnLogDump(string sMsg);
        //public event OnConnect OnConnectEv;
        protected event OnConnect OnConnectEv;
        public event OnDisConnect OnDisconnectEv;
        public event OnTimeOut OnTimeOutEv;
        public event OnDataArrived OnDataArrivedEv;
        public event OnHBT OnHBTSendEv;
        public event OnHBT OnHBTRecvEv;
        //public event OnLogDump OnLogDumpEv;
        #endregion

        #region -- public function --
        public new void Broadcast(ref StringBuilder sbMsg)
        {
            NuTukanSvrNode node = null;
            lock (m_clients)
            {
                for (int i = 0; i < m_clients.Count; i++)
                {
                    node = (NuTukanSvrNode)m_clients[i];
                    node.TukanSockSend(ref sbMsg);
                }
            }
        }
        public void Broadcast(ref StringBuilder sbMsg, Int32 SockFD)
        {
            NuTukanSvrNode node = null;
            lock (m_clients)
            {
                for (int i = 0; i < m_clients.Count; i++)
                {
                    node = (NuTukanSvrNode)m_clients[i];
                    if (node.ClientFD != SockFD)
                        node.TukanSockSend(ref sbMsg);
                }
            }
        }


        #endregion

    }
}
