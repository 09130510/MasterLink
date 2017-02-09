using System.Collections.Generic;
using BLParser;


namespace Bloomberg.RTD
{
    /// <summary>
    /// 資料取得
    /// </summary>
    public class RTDItemGetter
    {
        #region Variable
        //private const string m_Account = "SUB0007144";
        //private const string m_Password = "v52500938";
        private const string m_BloombergIP = "192.168.1.6";
        private const int m_SUBPort = 555;
        private const int m_REQPort = 666;

        private BLPRTD m_RTDSvr;
        private Sub m_Sub;
        private Req m_Req;
        //private Dictionary<string, Subscribe> m_SUBData = new Dictionary<string, Subscribe>();
        private SecurityCollection m_Collection;
        #endregion

        /// <summary>
        /// 資料取得
        /// </summary>
        /// <param name="rtds"></param>
        public RTDItemGetter(BLPRTD rtds)
        {
            m_RTDSvr = rtds;
            m_Sub = new Sub(ConnectType.Connect, m_SUBPort, m_BloombergIP);
            m_Sub.OnSUBReceived += new Sub.OnSUBReceivedDelegate(OnReceive);
            m_Sub.SubscribeAll();
            m_Sub.Start();

            m_Req = new Req(ConnectType.Connect, m_REQPort, m_BloombergIP);
            m_Req.OnREPReceived += new Req.OnREPReceivedDelegate(OnREPReceived);
        }        
        
        #region Delegate
        private void OnREPReceived(string Head, object Msg) { }
                
        private void OnReceive(string Head, object Msg)
        {
            if (Msg == null || !(Msg is Parser)) { return; }
            Parser sub = (Parser)Msg;
            if (m_Collection == null)
            {
                m_Collection = sub.Collection;
            }
            else
            {
                m_Collection.Join(sub.Collection);
            }
            m_RTDSvr.UpdateNotify();
        }
        #endregion


        /// <summary>
        /// 更新所有RTDItem資料
        /// </summary>
        /// <param name="datas"></param>
        public void GetRTDItem(List<RTDItem> datas)
        {
            foreach (var item in datas)
            {
                GetRTDItem(item);
            }
        }
        /// <summary>
        /// 更新單一RTDItem資料
        /// </summary>
        /// <param name="data"></param>
        public void GetRTDItem(RTDItem data)
        {
            if (m_Collection != null && m_Collection.Securities.ContainsKey(data.Security))
            {
                object value = m_Collection[data.Security][data.Field];
                if (value != null)
                {
                    data.Value = value;
                    return;
                }

            }
            //m_Req.Request("FIELD", data.Field);
        }

        public void RefillData(RTDItem data)
        {
            m_Req.Request("QUERY", data.Security + ";" + data.Field);
        }
    }
}
