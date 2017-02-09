using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;
using Microsoft.Office.Interop.Excel;
using System.Threading;


namespace Bloomberg.RTD
{
    [Guid("4D30E2CA-69C1-4B08-9B1E-D5C7900F73E3")]
    [ProgId("Bloomberg.RTD")]
    [ComVisible(true)]
    public class BLPRTD : IRtdServer
    {
        #region Variable
        private IRTDUpdateEvent m_xlRTDUpdate;
        private System.Timers.Timer m_tmrTimer;
        private List<RTDItem> m_Subscribes;
        private RTDItemGetter m_Getter = null;
        #endregion

        /// <summary>
        /// 啟動
        /// </summary>
        /// <param name="CallbackObject"></param>
        /// <returns></returns>
        public int ServerStart(IRTDUpdateEvent CallbackObject)
        {
            m_xlRTDUpdate = CallbackObject;
            m_Getter = new RTDItemGetter(this);

            m_tmrTimer = new System.Timers.Timer(1000);
            m_tmrTimer.Elapsed += tmrTimer_Elapsed;
            return 1;
        }
        /// <summary>
        /// Excel進行RTD訂閱
        /// </summary>
        /// <param name="TopicID"></param>
        /// <param name="Strings"></param>
        /// <param name="GetNewValues"></param>
        /// <returns></returns>
        public dynamic ConnectData(int TopicID, ref Array Strings, ref bool GetNewValues)
        {
            GetNewValues = true;
            try
            {
                if (m_Subscribes == null) { m_Subscribes = new List<RTDItem>(); }
                RTDItem temp = new RTDItem(Strings);
                m_Getter.GetRTDItem(temp);
                if (temp.Value == null)
                {
                    //ThreadPool.QueueUserWorkItem((e) =>
                    //{
                        m_Getter.RefillData(temp);
                    //});
                }
                //if (temp.Value != null) { m_Subscribes.Add(temp); }
                m_Subscribes.Add(temp);
            }
            catch (Exception ex)
            {
                return "ERROR IN QUERY. " + ex.Message;
            }
            if (!m_tmrTimer.Enabled) { m_tmrTimer.Start(); }

            foreach (var item in m_Subscribes)
            {
                if (item.Equals(Strings))
                {
                    if (item.TopicID == -1) { item.TopicID = TopicID; }
                    return item.Value;
                }
            }
            return "Unrecognized";
        }
        /// <summary>
        /// Excel移除RTD訂閱
        /// </summary>
        /// <param name="TopicID"></param>
        public void DisconnectData(int TopicID)
        {
            for (int i = m_Subscribes.Count - 1; i > 0; i--)
            {
                if (m_Subscribes[i].TopicID == TopicID)
                {
                    m_Subscribes.RemoveAt(i);
                }
            }
            if ((m_Subscribes == null || m_Subscribes.Count == 0) && m_tmrTimer.Enabled)
            {
                m_tmrTimer.Stop();
            }
        }
        /// <summary>
        /// 與Excel HB
        /// </summary>
        /// <returns></returns>
        public int Heartbeat() { return 1; }
        /// <summary>
        /// Excel要求更新資料
        /// </summary>
        /// <param name="TopicCount"></param>
        /// <returns></returns>
        public Array RefreshData(ref int TopicCount)
        {
            object[,] rets = new object[2, m_Subscribes.Count];
            int count = 0;

            foreach (var item in m_Subscribes)
            {
                //if (item.TopicID!=-1)
                //{
                rets[0, count] = item.TopicID;
                rets[1, count] = item.Value;
                //}
                count++;
            }
            TopicCount = m_Subscribes.Count;
            return rets;
        }
        /// <summary>
        /// 終止
        /// </summary>
        public void ServerTerminate()
        {
            m_xlRTDUpdate = null;
            if (m_tmrTimer.Enabled) { m_tmrTimer.Stop(); }

            m_tmrTimer.Elapsed -= tmrTimer_Elapsed;
            m_tmrTimer.Dispose();

        }
        /// <summary>
        /// 發送更新提示給Excel
        /// </summary>
        public void UpdateNotify()
        {
            try
            {
                m_Getter.GetRTDItem(m_Subscribes);
                m_xlRTDUpdate.UpdateNotify();
            }
            catch (Exception) { }

        }
        /// <summary>
        /// 定時取得未平倉資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateNotify();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        [ComRegisterFunction]
        public static void RegisterFunction(Type t)
        {
            Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(@"CLSID\{" + t.GUID.ToString().ToUpper() + @"}\Programmable");
            var key = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"CLSID\{" + t.GUID.ToString().ToUpper() + @"}\InprocServer32", true);
            if (key != null)
                key.SetValue("", System.Environment.SystemDirectory + @"\mscoree.dll", Microsoft.Win32.RegistryValueKind.String);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        [ComUnregisterFunction]
        public static void UnregisterFunction(Type t)
        {
            Microsoft.Win32.Registry.ClassesRoot.DeleteSubKey(@"CLSID\{" + t.GUID.ToString().ToUpper() + @"}\Programmable");
        }
    }
}
