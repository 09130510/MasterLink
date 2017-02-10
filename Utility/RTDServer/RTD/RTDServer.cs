using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;


namespace RTDServer
{
    [Guid("1451601C-FF7B-44d5-B737-6C2C49A269F3")]
    [ProgId("ML.RTD")]
    [ComVisible(true)]
    public class RTDServer: IRtdServer
    {
        #region Variable
        private IRTDUpdateEvent m_xlRTDUpdate;
        private List<SubData> m_Subscribes;
        #endregion

        #region IRtdServer 成員

        public int ServerStart(IRTDUpdateEvent CallbackObject)
        {
            m_xlRTDUpdate = CallbackObject;

            return 1;
        }
        public dynamic ConnectData(int TopicID, ref Array Strings, ref bool GetNewValues)
        {
            GetNewValues = true;
            string account = Strings.GetValue(0).ToString();
            string comid = Strings.GetValue(1).ToString();
            SubItem item =  default(SubItem);
            Enum.TryParse<SubItem>(Strings.GetValue(2).ToString(), out item);

            if (m_Subscribes == null)
            {
                m_Subscribes = new List<SubData>();
                SubData sub = new SubData();
                sub.Account = account;
                sub.ComID = comid;
                sub.Item = item;
                //sub.TopicID = TopicID;
                m_Subscribes.Add(sub);
            }
            

            foreach (var s in m_Subscribes)
            {
                if (s.Equals(account, comid, item))
                {
                    //??不同人訂同一個item怎辦
                    if (s.TopicID == -1)
                    {
                        s.TopicID = TopicID;
                    }
                    return s.Value;
                }
            }
            return "Unrecognized requested";
            //return "Subscribe ok";
        }

        public void DisconnectData(int TopicID)
        {
            for (int i = m_Subscribes.Count-1  ; i >0; i--)
            {
                if (m_Subscribes[i].TopicID==TopicID)
                {
                    m_Subscribes.RemoveAt(i);
                }
            }
        }

        public int Heartbeat()
        {
            return 1;
        }

        public Array RefreshData(ref int TopicCount)
        {
            object[,] rets = new object[2, m_Subscribes.Count];
            int counter = 0;
            foreach (var s in m_Subscribes)
            {
                if (s.TopicID!=-1)
                {
                    rets[0, counter] = s.TopicID;
                    rets[1, counter] = s.Value;
                }                
                counter++;
            }
            TopicCount = m_Subscribes.Count;
            return rets;
        }

        

        public void ServerTerminate()
        {
            m_xlRTDUpdate = null;

        }

        #endregion

        public void SendItem(string account, string comID, SubItem item, object value)
        {
            if (m_Subscribes == null)
            {
                m_Subscribes = new List<SubData>();
                SubData sub = new SubData();
                sub.Account = account;
                sub.ComID = comID;
                sub.Item = item;                                                                                                                                            
                m_Subscribes.Add(sub);
            }            

            foreach (var s in m_Subscribes)
            {
                if (s.Equals(account, comID, item))
                {
                    s.Value= value;
                }
            }
            if (m_xlRTDUpdate != null) { m_xlRTDUpdate.UpdateNotify(); }
        }
    }
}
