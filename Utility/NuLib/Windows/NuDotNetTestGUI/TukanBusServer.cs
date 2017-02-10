using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NuDotNet.SocketUtil;
using NuTukanBus;

namespace NuDotNetTestGUI
{
    public partial class TukanBusServer : Form
    {
        public String ServiceInfo { get { return txtService.Text; } set { txtService.Text = value; } }
        private char[] m_delimits = new char[2] { '@', ':' };
        private NuSocketBox m_socket_box = null;
        private TukanBusSvr Hdlr = null; 

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
//                    if (!chkShow.Checked)
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

        public TukanBusServer(NuSocketBox oObj)
        {
            InitializeComponent();
            m_socket_box = oObj;
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (Hdlr != null)
                return;

            string[] aryStr = null;
            aryStr = txtService.Text.Split(m_delimits);
            if (aryStr.Length < 3)
                return;

            string sSvrName = aryStr[0];
            string sIP = aryStr[1];
            string sPort = aryStr[2];

            Hdlr = new TukanBusSvr();
            Hdlr.OnConnectEv += new dlgTukanCB(Hdlr_OnConnectEv);
            Hdlr.OnDisconnectEv += new dlgTukanCB(Hdlr_OnDisconnectEv);
            Hdlr.OnHBTInEv += new dlgTukanCB(Hdlr_OnHBTInEv);
            Hdlr.OnHBTOutEv += new dlgTukanCB(Hdlr_OnHBTOutEv);
            Hdlr.OnLoginCompleteEv += new dlgTukanCB(Hdlr_OnLoginCompleteEv);
            Hdlr.OnRejectEv += new dlgTukanCB(Hdlr_OnRejectEv);
            Hdlr.OnExceptionAlert += new dlgTukanExceptionAlert(Hdlr_OnExceptionAlert);
            Hdlr.OnErrorEv += new dlgTukanCB(Hdlr_OnErrorEv);
            Hdlr.OnMsgArriveEv += new dlgTukanCB(Hdlr_OnMsgArriveEv);

            if (m_socket_box.AddServer(sSvrName, sIP, sPort, Hdlr, null))
            {
                _ShowMsg(String.Format("{0}-{1}:{2} Server Start.", sSvrName, sIP, sPort));
            }
            else
            {
                _ShowMsg(String.Format("{0}-{1}:{2} Server Start fail.", sSvrName, sIP, sPort));
            }

        }

        bool Hdlr_OnDisconnectEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("OnDisconnect {0}@{1}:{2}  [{3}]", oMsg.ID, oParam.RemoteIP, oParam.RemotePort,
                oMsg.ErrorMsg));
            return true;
        }

        bool Hdlr_OnMsgArriveEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("DataArrived {0}@{1}:{2}  [{3}]", oMsg.ID, oParam.RemoteIP, oParam.RemotePort,
                oMsg.BodyAsciiString));
            return true;
        }

        bool Hdlr_OnErrorEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("ErrorEv {0}@{1}:{2}  [{3}]", oMsg.ID, oParam.RemoteIP, oParam.RemotePort,
                oMsg.ErrorMsg));
            return true;
        }

        void Hdlr_OnExceptionAlert(string sMsg)
        {
            _ShowMsg(sMsg);
        }

        bool Hdlr_OnRejectEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("Reject {0}@{1}:{2}  [{3}]", oMsg.ID, oParam.RemoteIP, oParam.RemotePort,
                oMsg.BodyAsciiString));
            return true;
        }

        bool Hdlr_OnLoginCompleteEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("Login complete {0}@{1}:{2}  [{3}]", oMsg.ID, oParam.RemoteIP, oParam.RemotePort,
                oMsg.BodyAsciiString));
            return true;
        }

        bool Hdlr_OnHBTOutEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("HBT out {0}@{1}:{2}  [{3}]", oMsg.ID, oParam.RemoteIP, oParam.RemotePort,
                oMsg.BodyAsciiString));
            return true;
        }

        bool Hdlr_OnHBTInEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("HBT in  {0}@{1}:{2}  [{3}]", oMsg.ID, oParam.RemoteIP, oParam.RemotePort,
                oMsg.BodyAsciiString));
            return true;
        }

        bool Hdlr_OnConnectEv(NuSocketParam oParam, TukanBusMsg oMsg)
        {
            _ShowMsg(String.Format("LOGIN {0}@{1}:{2}  [{3}]", oMsg.ID, oParam.RemoteIP, oParam.RemotePort,
                oMsg.BodyAsciiString));
            return true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (Hdlr != null)
            {
                Hdlr.Close();
                Hdlr = null;
            }
        }
    }
}
