using Notifier.Class;
using CustomUIControls;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notifier
{
    public partial class frmStopNotify : Form
    {
        private const ChannelStyle STYLE = ChannelStyle.Red;
        private object m_CloseCall = null;
        //private AllowInfo m_EditingAllowInfo = null;
        private Dictionary<string, ServerInfo> m_Servers = new Dictionary<string, ServerInfo>();
        private Dictionary<ServerInfo, Monitor> m_Monitors = new Dictionary<ServerInfo, Monitor>();

        public static IniData INI { get; private set; }
        public static Dictionary<ServerInfo, List<AllowInfo>> Allows { get; set; } = new Dictionary<ServerInfo, List<AllowInfo>>();

        public frmStopNotify()
        {
            InitializeComponent();

            Text = niTask.Text = VersionInfo();
            if (INI == null)
            {
                var parser = new FileIniDataParser();
                INI = parser.ReadFile("Config.ini");
            }

            var items = INI["SETTING"]["SERVER"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in items)
            {
                var server = ServerInfo.Create(item.Split('|'));
                if (server == null || m_Servers.ContainsKey(server.Key))
                {
                    continue;
                }
                m_Servers.Add(server.Key, server);
            }
            cboServer.Items.AddRange(m_Servers.Values.Select(e => e.Name).ToArray());

            var allows = INI["SETTING"]["STOPNOTIFY"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var allow in allows)
            {
                var info = allow.Split('|');
                if (!m_Servers.ContainsKey(info[0]))
                {
                    continue;
                }
                var server = m_Servers[info[0]];
                var ainfo = AllowInfo.Create(info);
                //Monitor m = new Monitor(server, ainfo);
                if (!Allows.ContainsKey(server))
                {
                    Allows.Add(server, new List<AllowInfo>());
                    Allows[server].Add(ainfo);
                }
                else
                {
                    Allows[server].Add(ainfo);
                }
                if (!m_Monitors.ContainsKey(server))
                {
                    //m_Monitors.Add(server, new Monitor(server, ainfo));
                    m_Monitors.Add(server, new Monitor(server));
                    m_Monitors[server].Start();
                }
                //else
                //{
                //    m_Monitors[server].Allow(ainfo);
                //}
                listView1.Items.Add(ainfo.ListViewItem(server));
            }

            //niTask.ShowBalloonTip(10000, "AAA", "AAA", ToolTipIcon.Error);
            //new frmMain().Show();
        }
        private void frmSetting_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                //niTask.Visible = true;
                //niTask.ShowBalloonTip(500);
                this.Hide();
            }

            else if (FormWindowState.Normal == this.WindowState)
            {
                //niTask.Visible = false;
            }
        }
        private void frmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_CloseCall == null)
            {
                e.Cancel = true;
                this.Hide();
                //WindowState = FormWindowState.Minimized;
            }
        }

        private void niTask_Click(object sender, EventArgs e)
        {
            //Point p = Cursor.Position;
            //p.Y -= 50;
            //tsTaskMenu.Show(p);
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(niTask, null);
        }
        private void niTask_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            tsSetting_Click(tsSetting, EventArgs.Empty);
        }
        private void tsSubscribe_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var monitor in m_Monitors)
            {
                if (tsSubscribe.Checked)
                {
                    monitor.Value.Start();
                    niTask.Icon = new Icon(GetType(), "Running.ico");
                }
                else
                {
                    monitor.Value.Stop();
                    niTask.Icon = new Icon(GetType(), "Stop.ico");
                }
            }
        }
        private void tsSetting_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        private void tsExit_Click(object sender, EventArgs e)
        {
            m_CloseCall = sender;
            Close();
        }
        private void tsAdd_CheckedChanged(object sender, EventArgs e)
        {
            if (tsAdd.Checked) { tsModify.Checked = false; }
            splitContainer1.Panel1Collapsed = !tsAdd.Checked;
            btnAdd.Visible = tsAdd.Checked;
            //m_EditingAllowInfo = null;
            cboServer.SelectedItem = null;
            txtChannel.Text = txtItem.Text = txtSeconds.Text = string.Empty;
        }
        private void tsModify_CheckedChanged(object sender, EventArgs e)
        {
            if (tsModify.Checked) { tsAdd.Checked = false; }
            splitContainer1.Panel1Collapsed = !tsModify.Checked;
            btnAdd.Visible = tsAdd.Checked;
            if (listView1.SelectedItems.Count <= 0 && listView1.Items.Count > 0)
            {
                listView1.Items[0].Selected = true;
            }
        }
        private void tsDelete_Click(object sender, EventArgs e)
        {
            var ainfo = _SelectedAllowInfo();
            if (ainfo == null) { return; }
            var server = (ServerInfo)listView1.SelectedItems[0].Tag;
            Allows[server].Remove(ainfo);
            listView1.Items.Remove(listView1.SelectedItems[0]);
            m_Monitors[server].Remove(ainfo);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            tsDelete.Enabled = listView1.SelectedItems.Count > 0;
            //if (listView1.SelectedItems.Count == 0)
            //{
            //    tsModify.Checked = false;
            //    return;
            //}

            //tsModify.Checked = true;
            //var ainfo = _SelectedAllowInfo();
            //if (ainfo == null) { return; }
            //m_EditingAllowInfo = ainfo;
            //cboServer.SelectedItem = ((ServerInfo)listView1.SelectedItems[0].Tag).Name;
            //txtChannel.Text = ainfo.Channel;
            //txtItem.Text = ainfo.Item;
            //txtSeconds.Text = ainfo.Interval.ToString();
        }
        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                tsDelete_Click(tsDelete, EventArgs.Empty);
            }
        }
        //private void cboStyle_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    if (e.Index >= 0)
        //    {
        //        ComboBox combo = sender as ComboBox;
        //        e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.FromName(combo.Items[e.Index].ToString())), new Point(e.Bounds.X, e.Bounds.Y));
        //    }
        //}
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ServerInfo server = m_Servers.Values.First(entry => entry.Name == cboServer.Text);
            AllowInfo ainfo = AllowInfo.Create(txtChannel.Text, txtItem.Text, txtSeconds.Text, STYLE.ToString());
            if (ainfo == null) { return; }
            if (!Allows.ContainsKey(server))
            {
                Allows.Add(server, new List<AllowInfo>());
                Allows[server].Add(ainfo);
            }
            else
            {
                Allows[server].Add(ainfo);
            }
            if (!m_Monitors.ContainsKey(server))
            {
                m_Monitors.Add(server, new Monitor(server));
                m_Monitors[server].Start();
            }

            listView1.Items.Add(ainfo.ListViewItem(server));
        }


        private string VersionInfo()
        {
            Assembly assembly = GetType().Assembly;
            object[] attribute = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            return string.Format("[{0} - {3}]  {1}  V{2}", title.Title, desc.Description, Application.ProductVersion, Process.GetCurrentProcess().Id);
        }
        private AllowInfo _SelectedAllowInfo()
        {
            if (listView1.SelectedItems.Count <= 0) { return null; }
            ServerInfo server = (ServerInfo)listView1.SelectedItems[0].Tag;
            string channel = listView1.SelectedItems[0].SubItems[1].Text.ToString();
            string item = listView1.SelectedItems[0].SubItems[2].Text.ToString();
            return Allows[server].FirstOrDefault(entry => entry.Key == $"{channel}|{item}");
        }
        private void _Save()
        {
            foreach (var ainfo in Allows)
            {

            }
        }
        
    }
}