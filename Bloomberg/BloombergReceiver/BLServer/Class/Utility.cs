using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BL;
using BLParser;
using PriceLib.Redis;
using System.Diagnostics;
using WeifenLuo.WinFormsUI.Docking;

namespace BLPServer.Class
{
    public class Utility
    {
        #region Variable        
        private static Config m_Config = new Config("./", "Config.ini");
        private static DateTime m_LogCreateTime = DateTime.Now;

        private static Pub m_Pub;
        private static Rep m_Rep;
        private static RedisLib[] m_Redis;
        private static BLSubscriber m_Subscriber;
        private static Display m_Display;

        private static frmSetting m_SettingForm;
        private static frmMsg m_MsgForm;
        private static frmDock m_ReqDockForm;
        private static frmDock m_SubDockForm;

        private static frmSubscribe m_SubSettingForm;
        private static frmRequest m_ReqSettingForm;
        private static frmRequestData m_ReqResultForm;
        private static frmSubscriptionData m_SubResultForm;
        //private static frmSubscribe m_SubscribeForm;
        //private static frmRequest m_RequestForm;
        //private static frmSubscriptionData m_SDataForm;
        //private static frmRequestData m_RDataForm;
        #endregion

        #region Property
        public static Pub PUB
        {
            get { return m_Pub; }
            set { m_Pub = value; }
        }
        public static Rep REP
        {
            get { return m_Rep; }
            set { m_Rep = value; }
        }
        public static RedisLib[] REDIS
        {
            get
            {
                if (m_Redis == null)
                {                    
                    string[] IPs = Load<string>("REDIS", "IP").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);                    
                    m_Redis = new RedisLib[IPs.Length];
                    for (int i = 0; i < IPs.Length; i++)
                    {   
                        string[] items = IPs[i].Split(':');                        
                        m_Redis[i] = new RedisLib(items[0], int.Parse(items[1]), Process.GetCurrentProcess().Id+i);                        
                    }
                    
                   // m_Redis = new RedisLib(Load<string>("REDIS", "IP"), Load<int>("REDIS", "PORT"), Process.GetCurrentProcess().Id);
                }
                return m_Redis;
            }
        }
        public static BLSubscriber Subscriber
        {
            get { return m_Subscriber; }
            set
            {
                NotificationCenter.Instance.Post("BEFORESETSUBSCRIBER");
                m_Subscriber = value;
                NotificationCenter.Instance.Post("AFTERSETSUBSCRIBER");
            }
        }
        public static Display Display
        {
            get { return m_Display; }
            set { m_Display = value; }
        }

        public static frmSetting SettingForm
        {
            get
            {
                if (m_SettingForm == null) { m_SettingForm = new frmSetting(); }
                return m_SettingForm;
            }
        }
        public static frmMsg MsgForm
        {
            get
            {
                if (m_MsgForm == null) { m_MsgForm = new frmMsg(); }
                return m_MsgForm;
            }
        }
        public static frmDock SubDockForm
        {
            get
            {
                if (m_SubDockForm == null)
                {
                    m_SubDockForm = new frmDock() { Text = "Information" };
                    SubSettingForm.Show(m_SubDockForm.dockPanel1, DockState.DockRightAutoHide);
                    SubResultForm.Show(m_SubDockForm.dockPanel1);
                }
                return m_SubDockForm;
            }
        }
        public static frmDock ReqDockForm
        {
            get
            {
                if (m_ReqDockForm == null)
                {
                    m_ReqDockForm = new frmDock() { Text = "查詢" };
                    ReqSettingForm.Show(m_ReqDockForm.dockPanel1, DockState.DockRight);
                    ReqResultForm.Show(m_ReqDockForm.dockPanel1);
                }
                return m_ReqDockForm;
            }
        }
        public static frmSubscribe SubSettingForm
        {
            get
            {
                if (m_SubSettingForm == null) { m_SubSettingForm = new frmSubscribe(); }
                return m_SubSettingForm;
            }
        }
        public static frmRequest ReqSettingForm
        {
            get
            {
                if (m_ReqSettingForm == null) { m_ReqSettingForm = new frmRequest(); }
                return m_ReqSettingForm;
            }
        }
        public static frmSubscriptionData SubResultForm
        {
            get
            {
                if (m_SubResultForm == null) { m_SubResultForm = new frmSubscriptionData(); }
                return m_SubResultForm;
            }
        }
        public static frmRequestData ReqResultForm
        {
            get
            {
                if (m_ReqResultForm == null) { m_ReqResultForm = new frmRequestData(); }
                return m_ReqResultForm;
            }
        }
        //public static frmSubscribe SubscribeForm
        //{
        //    get
        //    {
        //        if (m_SubscribeForm == null) { m_SubscribeForm = new frmSubscribe(); }
        //        return m_SubscribeForm;
        //    }
        //}
        //public static frmRequest RequestForm
        //{
        //    get
        //    {
        //        if (m_RequestForm == null) { m_RequestForm = new frmRequest(); }
        //        return m_RequestForm;
        //    }
        //}
        //public static frmSubscriptionData SDataForm
        //{
        //    get
        //    {
        //        if (m_SDataForm == null) { m_SDataForm = new frmSubscriptionData(); }
        //        return m_SDataForm;
        //    }
        //}
        //public static frmRequestData RDataForm
        //{
        //    get
        //    {
        //        if (m_RDataForm == null) { m_RDataForm = new frmRequestData(); }
        //        return m_RDataForm;
        //    }
        //}
        #endregion

        #region Public
        public static T Load<T>(string section, string key)
        {
            return m_Config.GetSetting<T>(section, key);
        }
        public static void Save(string section, string key, object value)
        {
            m_Config.Reload();
            m_Config.SetSetting(section, key, value);
        }
        public static void LoadConfig(Control Control)
        {
            if (Control.Controls != null && Control.Controls.Count > 0)
            {
                foreach (Control subControl in Control.Controls)
                {
                    LoadConfig(subControl);
                }
                _Recursive(Control);
            }
            else
            {
                _Recursive(Control);
            }
        }
        public static void SaveConfig(Control control)
        {
            if (String.IsNullOrEmpty(control.Tag as string)) { return; }


            m_Config.Reload();

            string[] settings = control.Tag.ToString().Split('|');
            foreach (var setting in settings)
            {
                if (String.IsNullOrEmpty(setting)) { continue; }

                //string[] tags = control.Tag.ToString().Split(';');
                string[] tags = setting.Split(';');
                PropertyInfo pi = control.GetType().GetProperty(tags[2]);
                switch (tags[2])
                {
                    case "Text":
                        m_Config.SetSetting(tags[0], tags[1], control.Text.Replace("\r\n", ";"));
                        break;
                    case "Checked":
                        if (pi != null)
                        {
                            m_Config.SetSetting(tags[0], tags[1], (bool)pi.GetValue(control, null));
                        }
                        //m_Config.SetSetting(tags[0], tags[1], ((CheckBox)control).Checked);
                        break;
                    case "Value":
                        //if (control is NumericUpDown)
                        //{
                        if (pi != null)
                        {
                            m_Config.SetSetting(tags[0], tags[1], pi.GetValue(control, null));
                        }
                        //}
                        break;
                    case "Nodes":
                        m_Config.SetSetting(tags[0], tags[1], string.Join(";", ((TreeNodeCollection)pi.GetValue(control, null))[0].Nodes.OfType<TreeNode>().Where(e => e.Checked).Select(e => e.Text).ToArray()));
                        //m_Config.SetSetting(tags[0], tags[1], string.Join(";", ((TreeView)control).Nodes[0].Nodes.OfType<TreeNode>().Where(e => e.Checked).Select(e => e.Text).ToArray()));
                        break;
                    default:
                        break;
                }
            }
        }
        public static void ResetLayout(string session, string key)
        {
            m_Config.Reload();
            m_Config.SetSetting(session, key, "");
        }
        ///// <summary>
        ///// 欄位存檔
        ///// </summary>
        ///// <param name="session"></param>
        ///// <param name="key"></param>
        ///// <param name="view"></param>
        //public static void SaveLayout(string session, string key, ObjectListView view)
        //{
        //    m_Config.Reload();
        //    string state = Convert.ToBase64String(view.SaveState());
        //    m_Config.SetSetting(session, key, state);
        //}
        ///// <summary>
        ///// 欄位載入
        ///// </summary>
        ///// <param name="session"></param>
        ///// <param name="key"></param>
        ///// <param name="view"></param>
        //public static void LoadLayout(string session, string key, ObjectListView view)
        //{
        //    byte[] state = Convert.FromBase64String(Load<string>(session, key));
        //    if (state != null) { view.RestoreState(state); }
        //}
//        public static void Log(object sender, string action, string msg)
//        {
//#if DEBUG
//            DateTime now = DateTime.Now;
//            if (now.Date > m_LogCreateTime.Date)
//            {
//                m_LogCreateTime = now;
//                m_Log = new NuLog(@"./Log/", m_LogCreateTime.ToString("yyyyMMdd"));
//            }

//            string log = string.Format("[{0}] [{1}]  [{2}]", sender, action, msg);
//            m_Log.WrtLog(msg);   
//#endif
//        }
        #endregion

        #region Private
        private static void _Recursive(Control Control)
        {
            if (String.IsNullOrEmpty(Control.Tag as string)) { return; }

            string[] settings = Control.Tag.ToString().Split('|');
            foreach (var setting in settings)
            {
                if (String.IsNullOrEmpty(setting)) { continue; }

                //string[] tags = Control.Tag.ToString().Split(';');
                string[] tags = setting.Split(';');
                PropertyInfo pi = Control.GetType().GetProperty(tags[2]);
                if (pi == null) { return; }
                switch (tags[2])
                {
                    case "Text":
                        pi.SetValue(Control, Convert.ChangeType(Load<string>(tags[0], tags[1]).Replace(";", "\r\n"), pi.PropertyType), null);
                        break;
                    case "Value":
                    case "Checked":
                        pi.SetValue(Control, Convert.ChangeType(Load<string>(tags[0], tags[1]), pi.PropertyType), null);
                        break;
                    case "Nodes":
                        TreeNodeCollection collection = (TreeNodeCollection)pi.GetValue(Control, null);
                        if (collection == null || collection.Count == 0) { return; }
                        TreeNodeCollection nodes = collection[0].Nodes;
                        string[] val = Load<string>(tags[0], tags[1]).Split(';');
                        foreach (TreeNode node in nodes)
                        {
                            node.Checked = val.Contains(node.Text);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion
    }
}