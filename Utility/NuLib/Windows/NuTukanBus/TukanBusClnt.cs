using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NuDotNet;
using NuDotNet.SocketUtil;

namespace NuTukanBus
{
    public class TukanBusClnt : INuSocketHandler
    {
        
        private NuList<TukanBusMsg> m_lst_msgs = null;
        private bool IsLogin { set; get; }
        private NuSocketParam m_handle = null;
        private bool m_send_testreq_flag = false;

        public NuSocketParam Handle { get; set; }
        public String LoginID { get; set; }

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

        public TukanBusClnt()
        {
            m_lst_msgs = new NuList<TukanBusMsg>();
            IsLogin = false;
        }

        private bool _SendHBT()
        {
            if (LoginID == null)
                return false;
            MemoryStream mStream = new MemoryStream();
            TukanBusMsg oMsg = new TukanBusMsg();
            oMsg.Clear();
            oMsg.MsgType = TukanBusMsgType.HBT;
            oMsg.LoginID = LoginID;
            oMsg.ID = LoginID;
//            oMsg.Body = "";
            oMsg.SetBody("");
            TukanBusProtol.Pack(ref oMsg, ref mStream);

            if (Handle.Send(ref mStream, 1) > 0)
            {
                if (OnHBTOutEv != null)
                {
                    OnHBTOutEv(Handle, oMsg);
                }
                return true;
            }
            else
            {
                Handle.Disconnect(false);
                return false;
            }
        }

        private bool _SendTestReq()
        {
            if (LoginID == null)
                return false;
            MemoryStream mStream = new MemoryStream();
            TukanBusMsg oMsg = new TukanBusMsg();
            oMsg.Clear();
            oMsg.MsgType = TukanBusMsgType.TestReq;
            oMsg.ID = LoginID;
            oMsg.LoginID = LoginID;
//            oMsg.Body = "test req";
            oMsg.SetBody("test req");
            TukanBusProtol.Pack(ref oMsg, ref mStream);

            m_send_testreq_flag = true;

            if (Handle.Send(ref mStream, 1) > 0)
            {
                System.Diagnostics.Debug.WriteLine("send test req");
                if (OnHBTOutEv != null)
                {
                    OnHBTOutEv(Handle, oMsg);
                }
                return true;
            }
            else
            {
                Handle.Disconnect(false);
                return false;
            }
        }

        public bool OnConnectFail(NuSocketParam oParam)
        {
            bool bRtn = true;
            if (OnErrorEv != null)
            {
                TukanBusMsg oMsg = m_lst_msgs.Pop();
                oMsg.Clear();
                oMsg.ErrorMsg = String.Format("connect to IP[{0}] Port[{1}] fail!", oParam.RemoteIP, oParam.RemotePort);
                bRtn = OnErrorEv(Handle, oMsg);

                m_lst_msgs.Push(ref oMsg);
            }
            return bRtn;
        }

        public bool OnConnected(NuSocketParam oParam)
        {
            bool bRtn = true;
            m_send_testreq_flag = false;
            LoginID = "";

            if (OnConnectEv != null)
            {
                TukanBusMsg oMsg = m_lst_msgs.Pop();
                oMsg.Clear();
                bRtn = OnConnectEv(Handle, oMsg);
                m_lst_msgs.Push(ref oMsg);
            }
            return bRtn;
        }

        public void OnDisconnected(NuSocketParam oParam)
        {
            if (OnDisconnectEv != null)
            {
                TukanBusMsg oMsg = m_lst_msgs.Pop();
                oMsg.Clear();
                // send logout
                oMsg.ID = LoginID;
                oMsg.MsgType = TukanBusMsgType.Logout;
                MemoryStream mStream = new MemoryStream();
                TukanBusProtol.Pack(ref oMsg, ref mStream);
                oParam.Send(ref mStream);

                // call disconnect event
                oMsg.Clear();
                oMsg.ErrorMsg = "Disconnect";
                OnDisconnectEv(Handle, oMsg);
                m_lst_msgs.Push(ref oMsg);
            }
            IsLogin = false;
        }

        public void OnDataArrived(NuSocketParam oParam)
        {
            TukanBusMsg oMsg = new TukanBusMsg();
            oMsg.Clear();
            if (TukanFn.RcvTukanMsg(ref oParam, ref oMsg))
            {
//NuDebug.WriteLine("DEBUG - Client recv " + LoginID);
                oMsg.LoginID = LoginID;
                switch (oMsg.MsgType)
                {
                    case TukanBusMsgType.Login:
                        IsLogin = true;
                        LoginID = oMsg.ID;
                        oMsg.LoginID = LoginID;
                        if (OnLoginCompleteEv != null)
                            OnLoginCompleteEv(Handle, oMsg);
                        break;
                    case TukanBusMsgType.Reject:
                        if (!IsLogin)
                            return;
                        if (OnRejectEv != null)
                            OnRejectEv(oParam, oMsg);
                        break;
                    case TukanBusMsgType.SubscribeCfm:
                        if (!IsLogin)
                            return;
                        if (OnSubscribeCfmEv != null)
                            OnSubscribeCfmEv(Handle, oMsg);
                        break;
                    case TukanBusMsgType.TestReq:
                        if (!IsLogin)
                            return;
                        _SendHBT();
                        break;
                    case TukanBusMsgType.HBT:
                        if (!IsLogin)
                            return;
                        if (m_send_testreq_flag)
                            m_send_testreq_flag = false;
                        if (OnHBTInEv != null)
                            OnHBTInEv(Handle, oMsg);
                        break;
                    case TukanBusMsgType.Msg:
                        if (!IsLogin)
                            return;
                        if (OnMsgArriveEv!= null)
                            OnMsgArriveEv(Handle, oMsg);
                        break;
                    default:
                        return;
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
        }

        public void OnRemoteTimeout(NuSocketParam oParam)
        {
            if (IsLogin)
            {
                if (_SendTestReq())
                {
                    m_send_testreq_flag = true;
                }
                else
                {
                    oParam.Disconnect(false);
                }
            }
            return; 
        }

        public void OnLocalTimeout(NuSocketParam oParam)
        {
            if (IsLogin)
            {
                if (!_SendHBT())
                    oParam.Disconnect(false);
            }
        }

        public void OnException(string sMsg)
        {
            if (OnExceptionAlert != null)
                OnExceptionAlert(sMsg);
        }


        #region public method 
        public int SendTo(ref MemoryStream mStream, int Sec)
        {
            if (Handle == null)
                return -1;
            return Handle.Send(ref mStream, Sec);
        }

        public void Close()
        {
            if (Handle != null)
            {
                Handle.Disconnect();
                Handle.Dispose();
            }
        }
        #endregion
    }
}
