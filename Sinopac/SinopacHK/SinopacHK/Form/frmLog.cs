using System;
using System.Windows.Forms;
using Util.Extension;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace SinopacHK
{
    public partial class frmLog : DockContent
    {
        public frmLog()
        {
            InitializeComponent();
            NotificationCenter.Instance.AddObserver(OnRecvMsg, Utility.NotifyType.Msg.ToString());
            NotificationCenter.Instance.AddObserver(OnRecvError, Utility.NotifyType.Error.ToString());
        }
        private void frmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationCenter.Instance.RemoveObserver(OnRecvMsg, Utility.NotifyType.Msg.ToString());
            NotificationCenter.Instance.RemoveObserver(OnRecvError, Utility.NotifyType.Error.ToString());
        }

        #region Delegate
        private void OnRecvMsg(Notification n)
        {
            new Action(() =>
            {
                lstLog.Items.Insert(0, string.Format("{0}   {1}", DateTime.Now.ToString("HH:mm:ss.fff"), n.Message));
            }).BeginInvoke(this);
        }

        private void OnRecvError(Notification n)
        {
            AlertBox.AlertWithoutReply(this, AlertBoxButton.Error_OK, "Error", n.Message.ToString());
        }
        #endregion
    }
}
