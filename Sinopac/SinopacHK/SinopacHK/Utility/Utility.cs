using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using OrderProcessor.SinoPac;
using SinopacHK.Class;
using Util.Extension.Class;
using BrightIdeasSoftware;
using MySql.Data.MySqlClient;
using System.Data;

namespace SinopacHK
{
    public class Utility
    {
        public enum NotifyType
        {
            Msg,
            Error
        }

        
        public static Config Config = new Config("./", "Config.ini");
        public static SQLTool SQL = new SQLTool(Load<string>("SQL", "IP"), Load<string>("SQL", "DB"));
        public static SinoPacProcessor Processor = new SinoPacProcessor();
        public static Unit Unit = new Unit();
        public static string DockLayoutFile = @"./DockLayout.xml";

        #region Variable
        private static NuLog m_Log = new NuLog(@"./Log/", DateTime.Now.ToString("yyyyMMdd"));
        private static MySqlConnection m_MySQL;
        #endregion

        #region Property
        public static string SenderCompID { get { return Load<string>("SESSION", "SenderCompID"); } }
        public static string ProductID { get { return Load<string>("ORDERSETTING", "PID"); } }
        public static string Account { get { return Load<string>("ORDER", "Account"); } }
        public static int Qty { get { return Load<int>("ORDERSETTING", "LOTS"); } }
        public static bool StopOrder { get { return Load<bool>("ORDERSETTING", "STOPORDER"); } }
        public static bool OrderAlert { get { return Load<bool>("ORDERSETTING", "ORDERALERT"); } }
        public static int LotsLimit { get { return Load<int>("ORDERSETTING", "LOTSLIMIT"); } }
        public static string MatchSQLIP { get { return Load<string>("MATCHSQL", "IP"); } }
        public static string MatchSQLDB { get { return Load<string>("MATCHSQL", "DB"); } }
        public static string MatchSQLID { get { return Load<string>("MATCHSQL", "ID"); } }
        public static string MatchSQLPWD { get { return Load<string>("MATCHSQL", "PASSWORD"); } }
        public static int AutoLotsLimit { get; set; }
        public static MySqlConnection MySQL
        {
            get
            {
                if (m_MySQL== null)
                {
                    m_MySQL = new MySqlConnection(string.Format("database={3};server={0};User Id={1};Password={2};", MatchSQLIP, MatchSQLID, MatchSQLPWD, MatchSQLDB));
                    m_MySQL.Open();
                    
                }
                return m_MySQL;
            }
        }
        #endregion

        #region Public
        public static T Load<T>(string section, string key)
        {
            return Config.GetSetting<T>(section, key);
        }   
        public static void LoadConfig(Control Control)
        {
            if (Control.Controls != null && Control.Controls.Count > 0)
            {
                foreach (Control subControl in Control.Controls)
                {
                    LoadConfig(subControl);
                }
                Recursive(Control);
            }
            else
            {
                Recursive(Control);
            }
        }        
        public static void SaveConfig(Control control)
        {
            if (!String.IsNullOrEmpty(control.Tag as string))
            {
                Config.Reload();
                string[] tags = control.Tag.ToString().Split(';');
                switch (tags[2])
                {
                    case "Text":
                        //For MultiLine Replace \r\n
                        Config.SetSetting(tags[0], tags[1], control.Text.Replace("\r\n", ";"));
                        break;
                    case "Checked":
                        if (control is CheckBox)
                        {
                            Config.SetSetting(tags[0], tags[1], ((CheckBox)control).Checked);
                        }
                        break;
                    case "Value":
                        if (control is NumericUpDown)
                        {
                            Config.SetSetting(tags[0], tags[1], ((NumericUpDown)control).Value);
                        }
                        break;
                    case "Nodes":
                        Config.SetSetting(tags[0], tags[1], string.Join(";", ((TreeView)control).Nodes[0].Nodes.OfType<TreeNode>().Where(e => e.Checked).Select(e => e.Text).ToArray()));
                        break;
                    default:
                        break;
                }
            }
        }
        public static void Log(string msg)
        {
            m_Log.WrtLogWithFlush(msg);
        }
        /// <summary>
        /// 欄位存檔
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="view"></param>
        public static void SaveLayout( string key, ObjectListView view)
        {
            Config.Reload();
            string state = Convert.ToBase64String(view.SaveState());
            Config.SetSetting("LAYOUT", key, state);
        }
        /// <summary>
        /// 欄位載入
        /// </summary>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="view"></param>
        public static void LoadLayout( string key, ObjectListView view)
        {
            byte[] state = Convert.FromBase64String(Load<string>("LAYOUT", key));
            if (state != null) { view.RestoreState(state); }
        }

        public static DataTable Query(string sql)
        {
            DataTable dt =  new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, MySQL);
            adapter.Fill(dt);
            return dt;
        }
        public static void Execuate(string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, MySQL);
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Private
        private static void Recursive(Control Control)
        {
            if (!String.IsNullOrEmpty(Control.Tag as string))
            {
                string[] tags = Control.Tag.ToString().Split(';');
                PropertyInfo pi = Control.GetType().GetProperty(tags[2]);
                if (pi == null) { return; }
                switch (tags[2])
                {
                    case "Text":
                        //For MultiLine Replace ";"		
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