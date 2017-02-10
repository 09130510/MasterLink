using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NuDotNet.Net;

namespace NuDotNetTestGUI
{
    public class TestClient
    {
        #region private variable 
        NuTukanCli m_client = null;
        #endregion

        public TestClient()
        {
            m_client = new NuTukanCli();
            m_client.AutoReConnect = true;
            m_client.OnConnStatusChangeEv += new NuTukanCli.ConnectionStatusChange(m_client_OnConnStatusChangeEv);
            m_client.OnDataArriveEv += new NuTukanCli.OnDataArrive(m_client_OnDataArriveEv);
            m_client.OnHBTEv += new NuTukanCli.OnHBT(m_client_OnHBTEv);
            m_client.OnExceptionEv += new NuSocketClient.OnException(m_client_OnExceptionEv);
        }
        ~TestClient()
        {
            if (m_client != null)
                m_client.Dispose();
        }

        void m_client_OnExceptionEv(string sMsg)
        {
            if (LogEv != null) LogEv(sMsg);
        }

        void m_client_OnHBTEv(string sMsg)
        {
            if (LogEv != null)
            {
                LogEv(String.Format("HBT: FD[{0}] {1}",
                               m_client.SocketObject.Handle.ToInt32(),
                               sMsg
                               ));
            }
        }

        void m_client_OnDataArriveEv(object sender, RcvDataArgs e)
        {
            if (LogEv != null)
            {
                LogEv(String.Format("DataArrive: FD[{0}] {1}-{2}",
                               m_client.SocketObject.Handle.ToInt32(),
                               e.GetHdr, 
                               e.GetBody
                               ));
            }
        }

        void m_client_OnConnStatusChangeEv(enSockStatus Status)
        {
            if (LogEv != null)
            {
                LogEv(String.Format("StatusChangeEv: FD[{0}] {1}",
                               m_client.SocketObject.Handle.ToInt32(),
                               Status.ToString()
                               ));
            }
        }

        public delegate void dlgLog(string data);
        public event dlgLog LogEv;

        public bool Connect(string ip, string port)
        {
            bool rtn = false;
            rtn = m_client.Connect("Ivan", ip, port);
            if (LogEv != null)
            {
                LogEv(string.Format("Rtn : {0}", 
                    rtn.ToString()));
            }

            return rtn;
            
        }
        public void Disconnect()
        {
            m_client.Disconnect();
        }

        public void SendBroadcase(string sMsg)
        {
            m_client.TCPSendBroadcast(sMsg);
        }

        public void Stop()
        {
            m_client.StopAllThd();
        }
    }
}
