using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using NuDotNet;
using NuDotNet.Net;

namespace NuDotNetTestGUI
{
    public partial class TukanBusClntTest : Form
    {
        private bool m_quit = false;
        private NuTukanBusClnt m_client = null;
        private char[] m_delimits = new char[2] { '@', ':' };
        private string m_loginID = "";
        private Thread m_thread = null;
        public TukanBusClntTest()
        {
            InitializeComponent();
            txtTopic.Text = "@Grp";
        }

        public String LoginInfo { get { return txtLoginInfo.Text; } set { txtLoginInfo.Text = value; } }

        private void btnStart_Click(object sender, EventArgs e)
        {
            string[] aryConnInfo = txtLoginInfo.Text.Split(m_delimits);
            m_loginID = aryConnInfo[0];

            //if (m_client == null)
            {
                if (m_client == null)
                {
                    m_client = new NuTukanBusClnt(30);

                    m_client.OnConnStatusChangeEv += new NuTukanBusClnt.ConnectionStatusChange(m_client_OnConnStatusChangeEv);
                    m_client.OnHBTEv += new NuTukanBusClnt.OnHBT(_ShowMsg);
                    m_client.OnSubscribeReqEv += new NuTukanBusClnt.OnSubScribeReq(m_client_OnSubscribeReqEv);
                    m_client.OnSubcribeCfmEv += new NuTukanBusClnt.OnSubcribeCfm(m_client_OnSubcribeCfmEv);
                    m_client.OnDataArriveEv += new NuTukanBusClnt.OnDataArrive(m_client_OnDataArriveEv);
                    m_client.OnExceptionEv += new NuSocketClient.OnException(_ShowMsg);
                    m_client.OnErrorEv += new NuTukanBusClnt.OnError(m_client_OnErrorEv);
                }
                m_client.Connect(aryConnInfo[0], aryConnInfo[1], aryConnInfo[2]);
//                bool bRtn = false;
//                for (int i = 0; i < 10; i++)
//                {
                    
//                    bRtn = m_client.Disconnect();

//System.Diagnostics.Debug.WriteLine("Call Disconnect " + i.ToString() + "   " + bRtn.ToString());
//                    if (i % 3 == 0)
//                        m_client.Connect(aryConnInfo[0], aryConnInfo[1], aryConnInfo[2]);
//                }

                //m_client.Dispose();
                //while (m_client.IsConnected)
                //{
                //    m_client.TCPSend("@Grp", "After login test");
                //    return;
                //}
            }
        }

        void m_client_OnErrorEv(object sender, RcvDataArgs e)
        {
            _ShowMsg(String.Format("[{2}] Hdr[{0}] Body[{1}] RcvTime[{3}]", e.GetHdr, e.GetBody, e.GetID, 
                DateTime.Now.ToString("HH:mm:ss.fff")));
        }

        void m_client_OnDataArriveEv(object sender, RcvDataArgs e)
        {
            _ShowMsg(String.Format("[{2}] Hdr[{0}] Body[{1}] RcvTime[{3}]", e.GetHdr, e.GetBody, e.GetID, 
                DateTime.Now.ToString("HH:mm:ss.fff")));
        }

        void m_client_OnSubcribeCfmEv(string RequestID, RcvDataArgs e)
        {
            _ShowMsg(String.Format("[{2}] Hdr[{0}] Body[{1}]", e.GetHdr, e.GetBody, e.GetID));
        }

        void m_client_OnSubscribeReqEv()
        {
            //m_client.SubscribeReq("@Grp");
            return;
        }
     
        void m_client_OnConnStatusChangeEv(enSockStatus Status)
        {
            _ShowMsg(Status.ToString());
        }

        private delegate void dlgShowMsg(string sMsg);
        private void _ShowMsg(string sMsg)
        {
            if (txtBoard.InvokeRequired)
            {
                if (!txtBoard.IsDisposed)
                {
                    dlgShowMsg dg = new dlgShowMsg(_ShowMsg);
                    //txtBoard.BeginInvoke(dg, sMsg);
                    txtBoard.Invoke(dg, sMsg);
                }
            }
            else
            {
                try
                {
                    //if (txtBoard.Lines.Length > 1000)
                    //{
                    //    txtBoard.Text.Remove(0, 200);
                    //    txtBoard.Clear();
                    //}
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

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (m_client != null)
            {
                m_client.Disconnect();
                //m_client.StopAllThd();
                //m_client.Dispose();
                //m_client = null;
            }

        }


        private void btnSendGrp_Click(object sender, EventArgs e)
        {
            string sTopic = txtTopic.Text;
            for (int i = 0; i < 20; i++)
            {
                m_client.TCPSend(sTopic, 
                    i.ToString() + " ==================== send from " + m_client.LoginID + " " + DateTime.Now.ToString("HH:mm:ss.fff"));
            }
            //m_client.TCPSend("@Grp", "test grp");
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            string sTopic = txtTopic.Text;
            if (m_client != null)
                m_client.SubscribeReq(sTopic);
        }


        public void _Work()
        {
            try
            {
                int i = 0;
                while (!m_quit)
                {
                    //Thread.Sleep(1000);
                    //Thread.Sleep(500);
                    if (m_client != null)
                    {

                        if (this.txtTopic.Text.Length > 0)
                        {
                            if (i > 0)
                                return;
                            String sData = String.Format(" [{0}] send <{1}>",
                                 m_client.LoginID, DateTime.Now.ToString("HH:mm:ss.fff"));

                            System.Diagnostics.Debug.WriteLine(
                            String.Format("[DEBUG] {2} {0} - [{1}][{3}", m_client.LoginID, txtTopic.Text, Thread.CurrentThread.ManagedThreadId, sData));

                            if (!m_client.TCPSend(this.txtTopic.Text, sData))
                            {
                                _ShowMsg("######### Send Fail ########");
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }

                }
            }
            catch (ThreadInterruptedException)
            {
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void _TryConnect()
        {
        }
        private void btnThdUp_Click(object sender, EventArgs e)
        {
            if (m_thread == null)
            {
                m_thread = new Thread(_Work);
                m_thread.IsBackground = true;
                m_thread.Start();
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (m_thread != null)
            {
                m_thread.Abort();
                m_thread = null;
            }
        }

        private void TukanBusClnt_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_thread != null)
            {
                m_thread.Abort();
                m_thread.Join();
                m_thread = null;
            }
            if (m_client != null)
                m_client.Dispose();
            //this.Dispose();
        }
    }
}
