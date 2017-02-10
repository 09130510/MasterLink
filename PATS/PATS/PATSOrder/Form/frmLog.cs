using PATSOrder.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATSOrder
{
    public partial class frmLog : DockContent
    {

        public frmLog()
        {
            InitializeComponent();
            Center.Instance.AddObserver(ReceiveLog, Center.ALL);
            //Center.Instance.AddErrObserver(ReceiveErr);
        }
        private void frmLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Center.Instance.RemoveObserver(ReceiveLog, Center.ALL);
            //Center.Instance.RemoveErrObserver(ReceiveErr);
        }

        private void ReceiveLog(string msgName,Msg msg)
        {
            txtLog.BeginInvokeIfRequired(() =>
            {
                txtLog.AppendText($"{DateTime.Now.ToString("HH:mm:ss.ffff")}\t[{msg.Sender}]\t{msg.Message}\r\n", msgName==Center.ERR? FontStyle.Bold: FontStyle.Regular);
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
