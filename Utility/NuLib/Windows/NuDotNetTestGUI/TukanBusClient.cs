using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NuDotNet.SocketUtil;
using NuDotNet;
using NuTukanBus;


namespace NuDotNetTestGUI
{
    public partial class TukanBusClient : Form
    {
        private char[] m_delimits = new char[2] { '@', ':' };
//        private NuSocketBox m_socket_box = new NuSocketBox();
        private NuSocketBox m_socket_box = null;
        private string m_login_id = "";
        TukanBusClnt Hdlr = null;

        public TukanBusClient(NuSocketBox oObj)
        {
            InitializeComponent();

            txtTopic.Text = "@Grp";
            txtMsg.Text = "send message test";
            m_socket_box = oObj;

            this.FormClosed += new FormClosedEventHandler(TukanBusClient_FormClosed);
        }

        void TukanBusClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Hdlr != null)
                Hdlr.Close();
        }

        public String LoginInfo { get { return txtLoginInfo.Text; } set { txtLoginInfo.Text = value; } }
        private delegate void dlgShowMsg(string sMsg);
        private void _ShowMsg(string sMsg)
        {
            if (txtBoard.InvokeRequired)
            {
                if (!txtBoard.IsDisposed)
                {
                    dlgShowMsg dg = new dlgShowMsg(_ShowMsg);
                    txtBoard.Invoke(dg, sMsg);
                }
            }
            else
            {
                try
                {
                    if (!chkShow.Checked)
                    {
                        string sTime = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff") + " - ";
                        txtBoard.AppendText(sTime + sMsg + Environment.NewLine);
                    }
                }
                catch
                {
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string[] aryConnInfo = txtLoginInfo.Text.Split(m_delimits);
            string sLoginID = aryConnInfo[0];
            string sIP = aryConnInfo[1];
            string sPort = aryConnInfo[2];
            m_login_id = sLoginID;

            if (Hdlr == null)
            {
                Hdlr = new TukanBusClnt();
                Hdlr.OnExceptionAlert += new dlgTukanExceptionAlert(Hdlr_OnExceptionAlert);
                Hdlr.OnHBTInEv += new dlgTukanCB(Hdlr_OnHBTInEv);
                Hdlr.OnHBTOutEv += new dlgTukanCB(Hdlr_OnHBTOutEv);
                Hdlr.OnConnectEv += new dlgTukanCB(Hdlr_OnConnectEv);
                Hdlr.OnLoginCompleteEv += new dlgTukanCB(Hdlr_OnLoginCompleteEv);
                Hdlr.OnSubscribeCfmEv += new dlgTukanCB(Hdlr_OnSubscribeCfmEv);
                Hdlr.OnDisconnectEv += new dlgTukanCB(Hdlr_OnDisconnectEv);
                Hdlr.OnRejectEv += new dlgTukanCB(Hdlr_OnRejectEv);
                Hdlr.OnMsgArriveEv += new dlgTukanCB(Hdlr_OnMsgArriveEv);
                Hdlr.OnErrorEv += new dlgTukanCB(Hdlr_OnErrorEv);

                m_socket_box.AddClient(sLoginID, sIP, sPort, Hdlr, m_login_id, true);
            }
        }

        bool Hdlr_OnErrorEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg("OnError : " + oMsg.ErrorMsg);
            return true;
        }

        bool Hdlr_OnMsgArriveEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            //_ShowMsg("MsgArrived : " + oMsg.BodyAsciiString);
            _ShowMsg(String.Format("MsgArrived : ID={0}, Body=[{1}]", oMsg.ID, oMsg.BodyAsciiString));
            return true;
        }

        bool Hdlr_OnRejectEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
           
            _ShowMsg(String.Format("Reject [{0}]LoginID[{1}] ID[{2}] Body[{3}] Error[{4}]", 
                                    oMsg.MsgType, oMsg.LoginID, oMsg.ID, oMsg.BodyAsciiString, oMsg.ErrorMsg));
            return true;
        }

        bool Hdlr_OnDisconnectEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("Disconnect [{0}]LoginID[{1}] ID[{2}] Body[{3}] Error[{4}]", 
                                    oMsg.MsgType, oMsg.LoginID, oMsg.ID, oMsg.BodyAsciiString, oMsg.ErrorMsg));
            return true;
        }

        bool Hdlr_OnSubscribeCfmEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("SubcribeCfm [{0}]LoginID[{1}] ID[{2}] Body[{3}] Error[{4}]", 
                                    oMsg.MsgType, oMsg.LoginID, oMsg.ID, oMsg.BodyAsciiString, oMsg.ErrorMsg));
            return true;
        }

        bool Hdlr_OnLoginCompleteEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            MemoryStream mStream = new MemoryStream();
            _ShowMsg("login complete");
//            TukanBusProtol.GenSubscribeReq(ref mStream, new List<string> { "@Grp" });
            TukanBusProtol.GenSubscribeReq(ref mStream, new List<string> { txtTopic.Text.Trim() });
           
            if (oParam.Send(ref mStream, 1) > 0)
                return true;
            else
                return false;

        }

        bool Hdlr_OnConnectEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            MemoryStream mStream = new MemoryStream();
            TukanBusProtol.GenLoginReq(ref mStream, m_login_id);
            oParam.SetAutoReconnect(true);
            oParam.SetNoDelay(true);
            oParam.SetTimeoutSec(5, 20);
        
            _ShowMsg("Send Login");
            if (oParam.Send(ref mStream, 1) > 0)
                return true;
            else
                return false;
        }

        bool Hdlr_OnHBTOutEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("Out[{0}, {1}] remote ID[{2}] login ID[{3}]", oMsg.MsgType, 
                oMsg.BodyAsciiString, oMsg.ID, oMsg.LoginID));
            return true;
        }

        bool Hdlr_OnHBTInEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("In [{0}, {1}] remote ID[{2}] login ID[{3}]", oMsg.MsgType, 
                oMsg.BodyAsciiString, oMsg.ID, oMsg.LoginID));
            return true;
        }

        void Hdlr_OnExceptionAlert(string sMsg)
        {
            _ShowMsg(sMsg);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (Hdlr != null)
            {
                System.Diagnostics.Debug.WriteLine("stop");
                Hdlr.Handle.Disconnect();
                Hdlr = null;
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            MemoryStream mStream = new MemoryStream();
            string sTopic = txtTopic.Text.Trim();
            string sMsg = txtMsg.Text.Trim();
            int iCnt = 0;
            if (Hdlr == null)
                return;
            if (sTopic.Length > 0 && sMsg.Length > 0)
            {
                TukanBusProtol.GenMsgReq(ref mStream, sTopic, sMsg);

//                for(int i = 0 ; i < 20; i++)
                iCnt = Hdlr.SendTo(ref mStream, 1);

                _ShowMsg("Send Rtn = " + iCnt.ToString());
            } 

        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            MemoryStream mStream = new MemoryStream();
            TukanBusProtol.GenSubscribeReq(ref mStream, new List<string> { txtTopic.Text.Trim() });
           
            if (Hdlr.SendTo(ref mStream, 1) > 0)
                _ShowMsg(String.Format("Send subscribereq for {0} ok.", txtTopic.Text.Trim()));
            else
                _ShowMsg(String.Format("Send subscribereq for {0} fail.", txtTopic.Text.Trim()));

        }
    }
}
