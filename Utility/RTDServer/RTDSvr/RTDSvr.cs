using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace RTDSvr
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RTDServer : IRTDSvr
    {
        #region Variable
        private List<IRTDClnt> m_Clients;
        #endregion

        public RTDServer()
        {
            m_Clients = new List<IRTDClnt>();
        }

        public void Register()
        {
            var c = OperationContext.Current.GetCallbackChannel<IRTDClnt>();
            if (!m_Clients.Contains(c))
            {
                m_Clients.Add(c);
            }
        }
        public void Unregister()
        {
            var c = OperationContext.Current.GetCallbackChannel<IRTDClnt>();
            if (m_Clients.Contains(c))
            {
                m_Clients.Remove(c);
            }
        }

        public void SendValues(string Account, string ComID, SummaryItem Item, object Value)
        {
            foreach (var c in m_Clients)
            {
                c.ValueToClnt(Account, ComID, Item, Value);
            }
        }

        public void HeartBeat()
        {
            foreach (var c in m_Clients)
            {
                c.HeartBeat();
            }
        }
    }
}
