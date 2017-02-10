using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NuDotNet.Net;

namespace NuDotNetTestGUI
{
    public class TestServer : NuTukanSvr
    {
        public TestServer(string ip, int port)
            :base(ip, port)
        {
            this.OnConnectEv +=new NuTukanSvr.OnConnect(m_svr_OnConnectEv);
            this.OnDisconnectEv += new NuTukanSvr.OnDisConnect(m_svr_OnDisconnectEv);
            this.OnHBTSendEv += new NuTukanSvr.OnHBT(m_svr_OnHBTSendEv);
            this.OnHBTRecvEv += new NuTukanSvr.OnHBT(m_svr_OnHBTRecvEv);
            this.OnDataArrivedEv += new NuTukanSvr.OnDataArrived(m_svr_OnDataArrivedEv);
            this.OnExceptionEv += new NuSocketServer.dlgSockServerLog(m_svr_OnExceptionEv);
            //this.OnDEBUGLog += new NuSocketServer.dlgSockServerLog(m_svr_OnDEBUGLog);
        }

        void m_svr_OnDEBUGLog(string sMsg)
        {
            if (LogEv != null)
            {
                LogEv(String.Format("DEBUG - {0}",
                    sMsg));
            }
        }

        void m_svr_OnHBTRecvEv(NuTukanSvrNode Client, string sMsg)
        {
            if (LogEv != null)
            {
                LogEv(String.Format("<HBT> recv  FD[{0}] - {1} - {2}",
                    Client.ClientFD.ToString(),
                    Client.ClientHdr,
                    Client.LoginID));
            }
        }

        void m_svr_OnExceptionEv(string sMsg)
        {
            if (LogEv != null) LogEv(sMsg);
        }

        void m_svr_OnDataArrivedEv(NuTukanSvrNode Client, RcvDataArgs e)
        {
            if (LogEv != null)
            {
                LogEv(String.Format("<RcvData>  FD[{0}] - {1} - {2} [{3}-{4}]",
                    Client.ClientFD.ToString(),
                    Client.ClientHdr,
                    Client.LoginID, 
                    e.GetHdr, 
                    e.GetBody));
            }            
        }

        void m_svr_OnHBTSendEv(NuTukanSvrNode Client, string sMsg)
        {
            if (LogEv != null)
            {
                LogEv(String.Format("<HBT> send  FD[{0}] - {1} - {2}",
                    Client.ClientFD.ToString(),
                    Client.ClientHdr,
                    Client.LoginID));
            }
        }

        void m_svr_OnDisconnectEv(NuTukanSvrNode Client)
        {
            if (LogEv != null)
            {
                LogEv(String.Format("<Disconnect>  FD[{0}] - {1} - {2}",
                    Client.ClientFD.ToString(),
                    Client.ClientHdr,
                    Client.LoginID));
            }
            
        }

        bool m_svr_OnConnectEv(NuTukanSvrNode Client)
        {
            if (LogEv != null)
            {
                LogEv(String.Format("<Connect>  FD[{0}] - {1} - {2}", 
                    Client.ClientFD.ToString(), 
                    Client.ClientHdr, 
                    Client.LoginID));
            }
            return true;
        }

        ~TestServer()
        { }

        public delegate void dlgLog(string data);
        public event dlgLog LogEv;

        public void Start()
        {
            base.Start();
            if (LogEv != null)
            {
                LogEv(String.Format("Start Listen {0}:{1}", 
                      ListenIP,
                      ListenPort));
            }
        }

        public void Stop()
        {
            base.Stop();
        }

        public void Broadcase(string sMsg)
        {
            StringBuilder sb = new StringBuilder(sMsg);
            base.Broadcast(ref sb);
        }
    }

}
