using Notifier.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Notifier
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            Util.Init(this);
            niNotify.Text = Util.VersionInfo();
            Util.StopNotify.Show();
        }

        private void niNotify_Click(object sender, EventArgs e)
        {
            MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            mi.Invoke(niNotify, null);
        }
        private void niNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        private void tsSubscribe_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var monitor in Util.Monitors)
            {
                if (tsSubscribe.Checked)
                {
                    monitor.Value.MonitorStart();
                    niNotify.Icon = new Icon(GetType(), "Running.ico");
                }
                else
                {
                    monitor.Value.MonitorStop();
                    niNotify.Icon = new Icon(GetType(), "Stop.ico");
                }
            }
        }
        private void tsStopNotify_Click(object sender, EventArgs e)
        {
            Util.StopNotify.Show();
        }
        private void tsSave_Click(object sender, EventArgs e)
        {
            List<string> saveitem = new List<string>();
            foreach (var info in frmSetting.StopNotifies.Values)
            {
                foreach (var item in info)
                {
                    saveitem.Add(item.ToString());
                }
            }
            Util.INI["SETTING"]["STOPNOTIFY"] = string.Join(";", saveitem.ToArray());

            saveitem.Clear();
            foreach (var info in frmSetting.InfoNotifies.Values)
            {
                foreach (var item in info)
                {
                    saveitem.Add(item.ToString());
                }
            }
            Util.INI["SETTING"]["INFONOTIFY"] = string.Join(";", saveitem.ToArray());

            Util.WriteConfig();
            MessageBox.Show("存檔完成！", "設定存檔", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            //frmStopNotify.Allows.Select(entry=>entry.Value.ToArray())
        }
        private void tsExit_Click(object sender, EventArgs e)
        {
            Util.StopNotify.CancelObj = this;
            Util.StopNotify.Close();
            Close();
        }
    }
}
