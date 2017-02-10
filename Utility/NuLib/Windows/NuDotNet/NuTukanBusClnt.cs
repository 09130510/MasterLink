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
    /// <summary>
    /// Tukan Client 
    /// </summary>
    public class NuTukanBusClnt : NuSocketClient 
    {
        #region --  private items  --
        private enSockStatus m_Status = enSockStatus.NotAvailable;
        
        private bool m_LoginFlag = false;
        private byte[] m_buf = new byte[512];

        private int m_HBTInterval;

        private DateTime m_LastHBTTime = DateTime.Now;
        private int m_HBTDelayCnt = 1;

        private bool m_Quit = false;

        //working thread
        private Thread m_work_thd = null;
        private NuQueue<RcvDataArgs> m_work_que = new NuQueue<RcvDataArgs>();

        #region protocal 
        private string mp_local_hdr_id = "";
        private string mp_login_id = "";
        #endregion

        #endregion

        #region --  public delegate / event  --
        public delegate void OnLoginComplete();
        public event OnLoginComplete OnLoginCompleteEv;
            
        public delegate void ConnectionStatusChange(enSockStatus Status);
        /// <summary>
        /// 連線狀態改變
        /// </summary>
        public event ConnectionStatusChange OnConnStatusChangeEv;

        public delegate void OnDataArrive(object sender, RcvDataArgs e);
        /// <summary>
        /// 資料接收事件
        /// </summary>
        public event OnDataArrive OnDataArriveEv;

        public delegate void OnHBT(string sMsg);
        /// <summary>
        /// 收送HBT
        /// </summary>
        public event OnHBT OnHBTEv;

        public delegate void OnError(object sender, RcvDataArgs e);
        /// <summary>
        /// 發生錯誤
        /// </summary>
        public event OnError OnErrorEv;

        public delegate void OnSubScribeReq();
        /// <summary>
        /// 登入後觸發訂閱內容
        /// </summary>
        public event OnSubScribeReq OnSubscribeReqEv;
        
        public delegate void OnSubcribeCfm(string RequestID, RcvDataArgs e);
        /// <summary>
        /// 訂閱結果
        /// </summary>
        public event OnSubcribeCfm OnSubcribeCfmEv;

        #endregion

        #region --  construct / destruct  --
        private void internal_init()
        {
            long hdr_num = 0;
            m_LoginFlag = false;

            hdr_num = DateTime.Now.Ticks % 100000;
            if (hdr_num > 50001)
                hdr_num -= 50000;
            else if (hdr_num == 0)
                hdr_num = 1;

            mp_local_hdr_id = String.Format("{0:00000}", hdr_num);
        }

        /// <summary>
        /// NuTukanBusClnt construct
        /// </summary>
        public NuTukanBusClnt():base(512)
        {
            internal_init();
            HBTInterval = 30;  // 30 sec
            mp_login_id = "";

            base.OnBaseConnectEv += new OnBaseConnect(NuTukanBusClnt_OnBaseConnectEv);
            base.OnBaseDisconnectEv += new OnBaseDisconnect(NuTukanBusClnt_OnBaseDisconnectEv);
            base.OnBaseDataArriveEv += new RcvBaseDataHandler(NuTukanBusClnt_OnBaseDataArriveEv);
            base.OnTimerEv += new OnTimer(NuTukanBusClnt_OnTimerEv);
            
            //long job thread
            m_work_thd = new Thread(_WorkThd);
            m_work_thd.IsBackground = true;
            m_work_thd.Start();
        }

        /// <summary>
        /// NuTukanBusClnt construct
        /// </summary>
        /// <param name="hbt_inteval">Heartbeat time interval (sec)</param>
        public NuTukanBusClnt(int hbt_inteval)
            : base(4096)
        {
            internal_init();
            HBTInterval = hbt_inteval;
            mp_login_id = "";

            base.OnBaseConnectEv += new OnBaseConnect(NuTukanBusClnt_OnBaseConnectEv);
            base.OnBaseDisconnectEv += new OnBaseDisconnect(NuTukanBusClnt_OnBaseDisconnectEv);
            base.OnBaseDataArriveEv += new RcvBaseDataHandler(NuTukanBusClnt_OnBaseDataArriveEv);
            base.OnTimerEv += new OnTimer(NuTukanBusClnt_OnTimerEv);

            //long job thread
            m_work_thd = new Thread(_WorkThd);
            m_work_thd.IsBackground = true;
            m_work_thd.Start();
        }

        /// <summary>
        /// NuTukanBusClnt destruct
        /// </summary>
        ~NuTukanBusClnt()
        {
            m_Quit = true;
            Dispose(false);
            
        }
        #endregion

        /// <summary>
        /// 停止所有工作中的thread
        /// </summary>
        public void StopAllThd()
        {
            if (m_work_thd != null)
            {
                if (m_work_thd.IsAlive)
                {
                    m_work_thd.Abort();
                    //m_work_thd.Join();
                }
            }

            base.StopAllThd();
        }

        #region --  BaseEvent implement  --
        private bool NuTukanBusClnt_OnBaseConnectEv(object obj)
        {
            if (OnConnStatusChangeEv != null)
                OnConnStatusChangeEv(enSockStatus.Connect);
            m_Status = enSockStatus.Connect;
//System.Diagnostics.Debug.WriteLine("Send Login");
            _SendLogin();

            if (OnSubscribeReqEv != null)
                OnSubscribeReqEv();
            return true;
        }

        private void NuTukanBusClnt_OnBaseDisconnectEv(object obj)
        {
            if (OnConnStatusChangeEv != null)
                OnConnStatusChangeEv(enSockStatus.Disconnect);
            m_Status = enSockStatus.Disconnect;
            m_LoginFlag = false;
        }

        private void NuTukanBusClnt_OnBaseDataArriveEv(object obj)
        {
            NuTukanBusClnt client = (NuTukanBusClnt)obj;
            MemoryStream mStream = new MemoryStream(1024);
            string sHdr = "";
            string sRcvData = "";
            string sBody = "";
            string sID = "";
            int iBodyLen = 0;
            int iIDLen = 0;
            int iNeedRcvLen = 0;
           
            //while (client.SocketObject.Available > 0)
            while(client.AvailableDataSz > 0)
            {
                /* header format -------------------------------
                 * MsgType   X(1)
                 * IDLen     9(4)
                 * MsgLen    9(4)
                 * End       X(1)   \001
                 * ---------------------------------------------  */
                mStream.SetLength(0);
                if (client.TCPRecvUntil(ref mStream, 10) != 10)
                    return;

                sHdr = Encoding.Default.GetString(mStream.ToArray());
                if (sHdr[sHdr.Length - 1] != NuDataProtocol.protocol_end)
                {
                    base.InternalDisconnect();
                    return;
                }

                // get id length 
                if (!int.TryParse(sHdr.ToString().Substring(1, 4), out iIDLen))
                {
                    base.InternalDisconnect();
                    return;
                }

                // get body length
                if (!int.TryParse(sHdr.ToString().Substring(5, 4), out iBodyLen))
                {
                    base.InternalDisconnect();
                    return;
                }

                #region body handle
                iNeedRcvLen = iIDLen + iBodyLen;

                if (iNeedRcvLen > 0)
                {
                    mStream.SetLength(0);
                    if (base.TCPRecvUntil(ref mStream, iNeedRcvLen) != iNeedRcvLen)
                    {  
                        base.InternalDisconnect();
                        return;
                    }
                    sRcvData = Encoding.Default.GetString(mStream.ToArray());
                    sID = sRcvData.Substring(0, iIDLen);
                    sBody = sRcvData.Substring(iIDLen);

                    sHdr += sID;
                }
                #endregion

                #region parsing header
                switch (sHdr[0])
                {
                    case NuTukanBusMsgType.Login:  // login success 
                        m_LoginFlag = true;

                        if (OnConnStatusChangeEv != null)
                            OnConnStatusChangeEv(enSockStatus.Login);
                        m_Status = enSockStatus.Login;
                        
                        m_HBTDelayCnt = 0;
                        m_LastHBTTime = DateTime.Now;

                        if (OnLoginCompleteEv != null)
                        {
                            OnLoginCompleteEv();
                        }
                        
                        _sendHBT();
                        break;
                    case NuTukanBusMsgType.Logout:
                        base.InternalDisconnect();
                        break;
                    case NuTukanBusMsgType.HBT:
                        if (OnHBTEv != null)
                            OnHBTEv(string.Format(" > HBT [{0}][{1}]", m_IP, sHdr));
                        
                        m_HBTDelayCnt = 0;
                        break;
                    case NuTukanBusMsgType.TestReq:
                        _sendHBT();
                        break;
                    case NuTukanBusMsgType.Msg:
                        if (OnDataArriveEv != null)
                        {
                            RcvDataArgs args = new RcvDataArgs(sHdr, sBody);
                            m_work_que.Enqueue(args);
                        }
                        break;
                    case NuTukanBusMsgType.Reject:
                        if (OnErrorEv != null)
                        {
                            RcvDataArgs args = new RcvDataArgs(sHdr, sBody);
                            m_work_que.Enqueue(args);
                        }
                        break;
                    case NuTukanBusMsgType.SubscribeCfm:
                        if (OnSubcribeCfmEv != null)
                        {
                            RcvDataArgs args = new RcvDataArgs(sHdr, sBody);
                            m_work_que.Enqueue(args);
                        }
                        break;
                    default:
                        break;
                }
                #endregion
            }
        }

        void NuTukanBusClnt_OnTimerEv(object obj, DateTime LastRecvTime, DateTime LastSendTime)
        {
            NuTukanBusClnt client = (NuTukanBusClnt)obj;
            double iDiffTimeMS_Send = (DateTime.Now - LastSendTime).TotalSeconds;
            double iDiffTimeMS_Recv = (DateTime.Now - LastRecvTime).TotalSeconds;

            if (iDiffTimeMS_Recv >= (2 * m_HBTInterval))
            {
                if (m_LoginFlag)
                {
                    if (mp_login_id != "")
                        _sendLogout();

                    base.InternalDisconnect();
                    m_LoginFlag = false;
                }
            }
            else if (iDiffTimeMS_Recv - m_HBTInterval >= 1)
            {
                if (m_LoginFlag)
                {
                    if (m_HBTDelayCnt == 0)
                        _sendTestRequest();
                    ++m_HBTDelayCnt;
                }
            }

            if (iDiffTimeMS_Send - m_HBTInterval >= -1)
            {
                _sendHBT();
            }
        }

        #endregion

        #region --  property  --
        /// <summary>
        /// Heartbeat interval, 單位 : sec
        /// </summary>
        public int HBTInterval { get { return m_HBTInterval; } set { m_HBTInterval = value; } }
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
        public enSockStatus CurrentStatus { get { return m_Status; } }
        /// <summary>
        /// auto reconnect or not
        /// </summary>
        public bool AutoReConnect { get { return m_AutoReConnect; } set { m_AutoReConnect = value; } }
        #endregion

        #region --  public Method  --

        #region Connection Open
        /// <summary>
        /// 起動連線作業
        /// </summary>
        /// <param name="sLoginID">登入ID</param>
        /// <param name="sIP">Remote IP</param>
        /// <param name="sPort">Remote Port</param>
        /// <returns></returns>
        public new bool Connect(string sLoginID, string sIP, string sPort)
        {
            bool bRC = false;
            mp_login_id = sLoginID;
            bRC = base.Connect(sIP, sPort);

            //System.Diagnostics.Debug.WriteLine("Connect " + bRC.ToString());

            return bRC;
        }
        #endregion

        #region Connection close
        /// <summary>
        /// 結束連線
        /// </summary>
        public bool Disconnect()
        {
            if (mp_login_id != "" && m_LoginFlag == true)
                _sendLogout();
                
            return base.Disconnect();
        }
        #endregion

        #region TCPSend
        /// <summary>
        /// 傳送字串
        /// </summary>
        /// <param name="sTargetID">遠端ID</param>
        /// <param name="sMsg">Message</param>
        /// <returns></returns>
        public new bool TCPSend(string sTargetID, string sMsg)
        {
            int iSnd = 0;
            string sHdr = "";
            StringBuilder sbData = new StringBuilder(1024);
            int iMsgLen = ASCIIEncoding.Default.GetByteCount(sMsg);
            int iIDLen = ASCIIEncoding.Default.GetByteCount(sTargetID);

            sHdr = NuDataProtocol.genTukanBusHdr(sTargetID, NuTukanBusMsgType.Msg, iIDLen, iMsgLen);
            if (sHdr.Length == 0)
                return false;

            sbData.Append(sHdr);
            sbData.Append(sMsg);

            byte[] bMsg = Encoding.Default.GetBytes(sbData.ToString());
            iSnd = base.TCPSend(bMsg);

            if (iSnd != bMsg.Length)
                return false;
            return true;
        }
        #endregion

        #region SubscribeReq
        /// <summary>
        /// 訂閱ID 
        /// </summary>
        /// <param name="RequestID">訂閱的ID, 群組使用@開頭, ex. @Rpt </param>
        /// <returns></returns>
        public bool SubscribeReq(string RequestID)
        {
            int iSnd = 0;
            string sHdr = "";
            StringBuilder sbData = new StringBuilder(1024);
            int iMsgLen = 0;
            int iIDLen = ASCIIEncoding.Default.GetByteCount(RequestID);

            sHdr = NuDataProtocol.genTukanBusHdr(RequestID, NuTukanBusMsgType.SubscribeReq, iIDLen, iMsgLen);
            if (sHdr.Length == 0)
                return false;

            sbData.Append(sHdr);

            byte[] bMsg = Encoding.Default.GetBytes(sbData.ToString());
            iSnd = base.TCPSend(bMsg);

            if (iSnd != bMsg.Length)
                return false;
            return true;
        }
        #endregion


        #endregion

        #region --  private function  --
        private bool _sendHBT()
        {
            int id_len = ASCIIEncoding.Default.GetByteCount(mp_login_id);
            string sHBT = NuDataProtocol.genTukanBusHdr(mp_login_id, NuTukanBusMsgType.HBT, id_len, 0);
            int iSnd = 0;
            byte [] bMsg =  Encoding.Default.GetBytes(sHBT);

            if (bMsg.Length == 0)
                return false;

            iSnd = base.TCPSend(bMsg);
            if (iSnd != bMsg.Length)
            {
                //base.Disconnect();
                base.InternalDisconnect();
                return false;
            }

            if (OnHBTEv != null)
            {
                OnHBTEv(string.Format(" < HBT [{0}][{1}]", m_IP, sHBT));
            }

            return true;
        }

        private bool _sendTestRequest()
        {
            int id_len = ASCIIEncoding.Default.GetByteCount(mp_login_id);
            string sTestReq = NuDataProtocol.genTukanBusHdr(mp_login_id, NuTukanBusMsgType.TestReq, id_len, 0);
            int iSnd = 0;
            byte[] bMsg = Encoding.Default.GetBytes(sTestReq);

            if (bMsg.Length == 0)
                return false;

            iSnd = base.TCPSend(bMsg);
            if (iSnd != bMsg.Length)
            {
                base.InternalDisconnect();
                return false;
            }

            if (OnHBTEv != null)
            {
                OnHBTEv(string.Format(" < TestRequest [{0}][{1}]", m_IP, sTestReq));
            }

            return true;
        }

        private bool _sendLogout()
        {
            if (mp_login_id == "")
                return true;
            int id_len = ASCIIEncoding.Default.GetByteCount(mp_login_id);
			string sLogout = NuDataProtocol.genTukanBusHdr(mp_login_id, NuTukanBusMsgType.Logout, id_len, 0);
            int iSnd = 0;
			byte[] bMsg = Encoding.Default.GetBytes(sLogout);

            iSnd = base.TCPSend(bMsg);
            if (iSnd != bMsg.Length)
                return false;
            return true;
        }

        private bool _SendLogin()
        {
            StringBuilder sbData = new StringBuilder(1024);
            int iRC = 0;
            try
            {
                if (base.IsConnected)
                {
                    int id_len = ASCIIEncoding.Default.GetByteCount(mp_login_id);
                    string sLogin = NuDataProtocol.genTukanBusHdr(mp_login_id, NuTukanBusMsgType.Login, id_len, 0);
                    // do login
                    if (mp_login_id.Length > 0)
                    {
                        sbData.Append(sLogin);

                        byte[] bMsg = Encoding.Default.GetBytes(sbData.ToString());
                        //System.Diagnostics.Debug.WriteLine("send login");
                        iRC = base.TCPSend(bMsg);
                        //System.Diagnostics.Debug.WriteLine("send login done");
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region --  work thread  --
        private void _WorkThd()
        {
            RcvDataArgs args = null;
            try
            {
                while (!m_Quit)
                {
                    args = m_work_que.Dequeue();
					if (args == null)
						continue;

                    switch (args.GetHdr[0])
                    {
                        case NuTukanBusMsgType.Msg:
                            OnDataArriveEv(this, args);
                            break;
                        case NuTukanBusMsgType.Reject:
                            OnErrorEv(this, args);
                            break;
                        case NuTukanBusMsgType.SubscribeCfm:
                            OnSubcribeCfmEv(args.GetHdr.Substring(10), args);
                            break;
                        default:
                            break;
                    }
                }
                return;
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
            catch (ThreadAbortException)
            {
                return;
            }
			//2013/08/29 會抓走Client的Exception所以拿掉
			//catch (Exception ex)
			//{
			//    return;
			//}

            
        }
        #endregion

    }
}