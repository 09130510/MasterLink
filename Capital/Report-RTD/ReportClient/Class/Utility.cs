using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Util.Extension.Class;
using PriceProcessor.Capital;

namespace Capital.Report.Class
{
    public enum RowType
    {
        None = 0,
        Functional = 1,
        Statistics = 2,
        Header = 3,
        BP = 4,
        MP = 5,
        AP = 6
    }
    public enum ColumnType
    {
        None = 0,
        Buy = 1,
        BLots = 2,
        Price = 3,
        SLots = 4,
        Sell = 5
    }
    /// <summary>
	/// Alert Box Button Style
	/// </summary>
	public enum AlertBoxButton
    {
        /// <summary>
        /// 出現OK, Caption正常
        /// </summary>
        Msg_OK,
        /// <summary>
        /// 出現OK, Caption為紅字
        /// </summary>
        Error_OK,
        /// <summary>
        /// 出現OK/Cancel
        /// </summary>
        OKCancel,
        /// <summary>
        /// 出現Yes/No
        /// </summary>
        YesNo
    }
    public class Utility
    {
        #region Variable
        //private const string LOGSTR = "Log";
        private static Utility m_Instance;
        private static Config m_Config = new Config("./", "Config.ini");
        private static NuLog m_Log = new NuLog(@"./ClientLog/", DateTime.Now.ToString("yyyyMMdd"));
        #endregion

        #region Property
        public static Utility Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Utility();
                }
                return m_Instance ;
            }
        }
        #endregion

        private Utility() { }

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
            if (!string.IsNullOrEmpty(control.Tag as string))
            {
                m_Config.Reload();
                string[] tags = control.Tag.ToString().Split(';');
                switch (tags[2])
                {
                    case "Text":
                        m_Config.SetSetting(tags[0], tags[1], control.Text.Replace("\r\n", ";"));
                        break;
                    case "Checked":
                        m_Config.SetSetting(tags[0], tags[1], ((CheckBox)control).Checked);
                        break;
                    case "Value":
                        if (control is NumericUpDown)
                        {
                            m_Config.SetSetting(tags[0], tags[1], ((NumericUpDown)control).Value);
                        }
                        break;
                    case "Nodes":
                        m_Config.SetSetting(tags[0], tags[1], string.Join(";", ((TreeView)control).Nodes[0].Nodes.OfType<TreeNode>().Where(e => e.Checked).Select(e => e.Text).ToArray()));
                        break;
                    case "Items":
                        m_Config.SetSetting(tags[0], tags[1], string.Join("|", ((ListBox)control).Items.OfType<string>().ToArray()));
                        break;
                }
            }
        }
        public static void ResetLayout(string session, string key)
        {
            m_Config.Reload();
            m_Config.SetSetting(session, key, "");
        }
        /// <summary>
        /// 欄位存檔
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="view"></param>
        public static void SaveLayout(string session, string key, ObjectListView view)
        {
            m_Config.Reload();
            string state = Convert.ToBase64String(view.SaveState());
            m_Config.SetSetting(session, key, state);
        }
        /// <summary>
        /// 欄位載入
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="view"></param>
        public static void LoadLayout(string session, string key, ObjectListView view)
        {
            byte[] state = Convert.FromBase64String(Load<string>(session, key));
            if (state != null && state.Length >0) { view.RestoreState(state); }
        }
        public static void Log(object sender, string action ,string msg)
        {            
            LogNoNotification($"[{sender}] [{action}]  [{msg}]");
            NotificationCenter.Instance.Post(OrderProcessor.Capital.CapitalProcessor.LOGSTR, new Notification(sender,$"[{action}]   {msg}"));
        }
        public static void LogNoNotification(string msg)
        {
            m_Log.WrtLogWithFlush(msg);
        }
        #endregion

        #region Private
        private static void _Recursive(Control Control)
        {
            if (!string.IsNullOrEmpty(Control.Tag as string))
            {
                string[] tags = Control.Tag.ToString().Split(';');
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
                    case "Items":
                        string[] items = Load<string>(tags[0], tags[1]).Split('|');
                        foreach (var item in items)
                        {
                            ((ListBox)Control).Items.Add(item);
                        }
                        break;                    
                }
            }
        }
        #endregion
    }
}
