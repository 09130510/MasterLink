using PATS.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATS
{
    public partial class frmLog : DockContent
    {

        public frmLog()
        {
            InitializeComponent();
            Center.Instance.AddObserver(ReceiveLog, Observer.All);
            //Center.Instance.AddErrObserver(ReceiveErr);
        }
        private void frmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Center.Instance.RemoveObserver(ReceiveLog, Observer.All);
            //Center.Instance.RemoveErrObserver(ReceiveErr);
        }

        private void ReceiveLog(Observer msgName, Msg msg)
        {
            txtLog.BeginInvokeIfRequired(() =>
            {
                txtLog.AppendText($"{DateTime.Now.ToString("HH:mm:ss.ffff")}\t[{msg.Sender}]\t{msg.Message}\r\n", msgName ==Observer.Error ? FontStyle.Bold : FontStyle.Regular);
            });
        }
        //private void ReceiveErr(string msgName, Msg msg)
        //{
        //    txtLog.BeginInvokeIfRequired(() =>
        //    {
        //        txtLog.AppendText($"{DateTime.Now.ToString("HH:mm:ss.ffff")}\t[{msg.Sender}]\t{msg.Message}\r\n", FontStyle.Bold);
        //    });
        //}
    }
}
