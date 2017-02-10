using OrderProcessor.Capital;
using System;
using System.Windows.Forms;
using Util.Extension;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace Capital.Report
{
    public partial class frmLog : DockContent
    {
        public frmLog()
        {
            InitializeComponent();
            NotificationCenter.Instance.AddObserver(OnRecvLog, CapitalProcessor.LOGSTR);
            NotificationCenter.Instance.AddObserver(OnRecvErr, CapitalProcessor.ERRSTR);
        }
        private void frmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationCenter.Instance.RemoveObserver(OnRecvLog, CapitalProcessor.LOGSTR);
            NotificationCenter.Instance.RemoveObserver(OnRecvErr, CapitalProcessor.ERRSTR);
        }

        private void OnRecvLog(Notification n)
        {
            new Action(() =>
            {
                lstLog.Items.Insert(0, $"{(DateTime.Now.ToString("HH:mm:ss.ffff"))}\t[{n.Sender}]\t{n.Message}");
            }).BeginInvoke(lstLog);
        }
        private void OnRecvErr(Notification n)
        {
            Class.AlertBox.AlertWithoutReply(null, Class.AlertBoxButton.Error_OK, "錯誤", n.Message.ToString().Trim());
            OnRecvLog(n);
        }
    }
}
