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
    public class NuTukanCli : NuSocketClient 
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
        //private NuQueue<RcvDataArgs> m_work_que = new NuQueue<RcvDataArgs>(3000);
        private NuQueue<RcvDataArgs> m_work_que = new NuQueue<RcvDataArgs>();

        #region protocal 
        private string mp_local_hdr_id = "";
        private string mp_remote_hdr_id = "";
        private string mp_login_id = "";
        #endregion

        #region public property
        public string Tukan_Local_Hdr_ID { get { return mp_local_hdr_id; } }
        public string Tukan_Remote_Hdr_ID { get { return mp_remote_hdr_id; } }
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
        /// NuTukanCli construct
        /// </summary>
        public NuTukanCli():base(512)
        {
            internal_init();
            HBTInterval = 30;  // 30 sec
            mp_login_id = "";

            base.OnBaseConnectEv += new OnBaseConnect(NuTukanCli_OnBaseConnectEv);
            base.OnBaseDisconnectEv += new OnBaseDisconnect(NuTukanCli_OnBaseDisconnectEv);
            base.OnBaseDataArriveEv += new RcvBaseDataHandler(NuTukanCli_OnBaseDataArriveEv);
            base.OnTimerEv += new OnTimer(NuTukanCli_OnTimerEv);
            
            //long job thread
            m_work_thd = new Thread(_WorkThd);
            m_work_thd.IsBackground = true;
            m_work_thd.Start();
        }

        /// <summary>
        /// NuTukanCli construct
        /// </summary>
        /// <param name="hbt_inteval">Heartbeat time interval (sec)</param>
        public NuTukanCli(int hbt_inteval)
            : base(4096)
        {
            internal_init();
            HBTInterval = hbt_inteval;
            mp_login_id = "";

            base.OnBaseConnectEv += new OnBaseConnect(NuTukanCli_OnBaseConnectEv);
            base.OnBaseDisconnectEv += new OnBaseDisconnect(NuTukanCli_OnBaseDisconnectEv);
            base.OnBaseDataArriveEv += new RcvBaseDataHandler(NuTukanCli_OnBaseDataArriveEv);
            base.OnTimerEv += new OnTimer(NuTukanCli_OnTimerEv);

            //long job thread
            m_work_thd = new Thread(_WorkThd);
            m_work_thd.IsBackground = true;
            m_work_thd.Start();
        }

        /// <summary>
        /// NuTukanCli destruct
        /// </summary>
        ~NuTukanCli()
        {
            m_Quit = true;
            //System.Diagnostics.Debug.WriteLine(m_Quit.ToString());
            //RcvDataArgs args = new RcvDataArgs("QUIT", "DATA");
            //m_work_que.Enqueue(args);

            //if (m_work_thd != null)
            //{
            //    if (m_work_thd.IsAlive)
            //    {
            //        m_work_thd.Abort();
            //        m_work_thd.Join();
            //    }
            //}
            Dispose(false);
            
        }
        #endregion
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
        private bool NuTukanCli_OnBaseConnectEv(object obj)
        {
            if (OnConnStatusChangeEv != null)
                OnConnStatusChangeEv(enSockStatus.Connect);
            m_Status = enSockStatus.Connect;
//System.Diagnostics.Debug.WriteLine("Send Login");
            _SendLogin();

            return true;
        }

        private void NuTukanCli_OnBaseDisconnectEv(object obj)
        {
            if (OnConnStatusChangeEv != null)
                OnConnStatusChangeEv(enSockStatus.Disconnect);
            m_Status = enSockStatus.Disconnect;
            m_LoginFlag = false;
        }

        private void NuTukanCli_OnBaseDataArriveEv(object obj)
        {
            NuTukanCli client = (NuTukanCli)obj;
            MemoryStream mStream = new MemoryStream(1024);
            string sHdr = "";
            string sBody = "";
            int iBodyLen = 0;
           
            //while (client.SocketObject.Available > 0)
            while(client.AvailableDataSz > 0)
            {
                /* header format -------------------------------
                 * Length    9(4)
                 * MsgType   X(1)
                 * ID        X(5)
                 * End       X(1)   \001
                 * ---------------------------------------------  */
                mStream.SetLength(0);
                if (client.TCPRecvUntil(ref mStream, 11) != 11)
                    return;

                sHdr = Encoding.Default.GetString(mStream.ToArray());
                if (sHdr[sHdr.Length - 1] != NuDataProtocol.protocol_end)
                {
                    //base.Disconnect();
                    base.InternalDisconnect();
                    return;
                }

                // get body length
                if (!int.TryParse(sHdr.ToString().Substring(0, 4), out iBodyLen))
                {
                    //base.Disconnect();
                    base.InternalDisconnect();
                    return;
                }

                #region body handle
                if (iBodyLen > 0)
                {
                    mStream.SetLength(0);
                    if (base.TCPRecvUntil(ref mStream, iBodyLen) != iBodyLen)
                    {   //TODO: data error , should handler 
                        //base.Disconnect();
                        base.InternalDisconnect();
                        return;
                    }
                    sBody =  Encoding.Default.GetString(mStream.ToArray());
                }
                #endregion

                #region parsing header
                switch (sHdr[4])
                {
                    case NuMsgType.Login:  // login success 
                        mp_remote_hdr_id = sHdr.ToString().Substring(5, 5);
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
                    case NuMsgType.Logout:
                        //base.Disconnect();
                        base.InternalDisconnect();
                        break;
                    case NuMsgType.HBT:
                        if (OnHBTEv != null)
                            OnHBTEv(string.Format(" > HBT [{0}][{1}] <local, remote>[{2},{3}]", m_IP, sHdr.ToString(), Tukan_Local_Hdr_ID, Tukan_Remote_Hdr_ID));
                        
                        m_HBTDelayCnt = 0;
                        break;
                    case NuMsgType.TestReq:
                        _sendHBT();
                        break;
                    case NuMsgType.Msg:
                        if (mp_local_hdr_id == sHdr.ToString().Substring(5, 5))
                        {
                            if (OnDataArriveEv != null)
                            {
                                RcvDataArgs args = new RcvDataArgs(sHdr.ToString(), sBody.ToString());
                                //OnDataArriveEv(this, args);
                                m_work_que.Enqueue(args);
                            }
                        }
                        else
                        {
                            if (OnErrorEv != null)
                            {
                                RcvDataArgs args = new RcvDataArgs(sHdr.ToString(), sBody.ToString());
                                OnErrorEv(this, args);
                            }
                        }
                        break;
                    case NuMsgType.Error:
                        if (mp_local_hdr_id == sHdr.ToString().Substring(5, 5))
                        {
                            if (OnErrorEv != null)
                            {
                                RcvDataArgs args = new RcvDataArgs(sHdr.ToString(), sBody.ToString());
                                //OnErrorEv(this, args);
                                m_work_que.Enqueue(args);
                            }
                        }
                        break;
                    default:
                        break;
                }
                #endregion
            }
        }

        void NuTukanCli_OnTimerEv(object obj, DateTime LastRecvTime, DateTime LastSendTime)
        {
            NuTukanCli client = (NuTukanCli)obj;
            double iDiffTimeMS_Send = (DateTime.Now - LastSendTime).TotalSeconds;
            double iDiffTimeMS_Recv = (DateTime.Now - LastRecvTime).TotalSeconds;

            if (iDiffTimeMS_Recv >= (2 * m_HBTInterval))
            {
                if (m_LoginFlag)
                {
                    if (mp_remote_hdr_id != "")
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
        public new bool Connect(string sLoginID, string sIP, string sPort)
        {
            bool bRC = false;
            mp_login_id = sLoginID;
            bRC = base.Connect(sIP, sPort);
            return bRC;
        }
        #endregion

        #region Connection close
        public void Disconnect()
        {
            if (mp_remote_hdr_id != "")
                _sendLogout();
                
            base.Disconnect();
        }
        #endregion

        #region TCPSend
        public new bool TCPSend(string sMsg)
        {
            int iSnd = 0;
            string sHdr = "";
            StringBuilder sbData = new StringBuilder(1024);
            int sMsgLen = ASCIIEncoding.Default.GetByteCount(sMsg);

            sHdr = NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.Msg, sMsgLen);
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

        public bool TCPSendBroadcast(string sMsg)
        {
            int iSnd = 0;
            string sHdr = "";
            StringBuilder sbData = new StringBuilder(1024);
            int sMsgLen = ASCIIEncoding.Default.GetByteCount(sMsg);

            sHdr = NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.Broadcast, sMsgLen);
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

        #endregion

        #region --  private function  --
        private bool _sendHBT()
        {
            string sHBT = NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.HBT, 0);
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
                OnHBTEv(string.Format(" < HBT [{0}][{1}] <local, remote>[{2},{3}]", m_IP, sHBT, Tukan_Local_Hdr_ID, Tukan_Remote_Hdr_ID));
            }

            return true;
        }

        private bool _sendTestRequest()
        {
            string sTestReq = NuDataProtocol.genHdr(mp_remote_hdr_id, NuMsgType.TestReq, 0);
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
                //OnHBTEv(string.Format(" < TestRequest [{0}][{1}]", m_IP, sTestReq));
                OnHBTEv(string.Format(" < TestRequest [{0}][{1}] <local, remote>[{2},{3}]", m_IP, sTestReq, Tukan_Local_Hdr_ID, Tukan_Remote_Hdr_ID));
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
                    // do login
                    if (mp_login_id.Length > 0)
                    {
                        mp_local_hdr_id = String.Format("{0:00000}", int.Parse(mp_local_hdr_id) + 1);
                        sbData.Append(NuDataProtocol.genHdr(mp_local_hdr_id, NuMsgType.Login, ASCIIEncoding.Default.GetByteCount(LoginID)));
                        sbData.Append(LoginID);

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

                    switch (args.GetHdr[4])
                    {
                        case NuMsgType.Msg:
                            OnDataArriveEv(this, args);
                            break;
                        case NuMsgType.Error:
                            OnErrorEv(this, args);
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