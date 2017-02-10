using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;
using FixInitiator.Class;

namespace FixInitiator.UI
{
    public partial class FmMain : Form
    {
        private FmLogs fmLogs_;
        private FmOrder fmOrder_;
        public FmMain()
        {
            InitializeComponent();
        }

        private void FmMain_Load(object sender, EventArgs e)
        {
            Text = string.Format("{0}-{1}", System.Diagnostics.Process.GetCurrentProcess().Id, System.Diagnostics.Process.GetCurrentProcess().ProcessName);
        }

        private void butOrdSvr_Click(object sender, EventArgs e)
        {
            MainClass.mainClass.srnItems.butOrdSvr = butOrdSvr;
            if (!MainClass.mainClass.isLogon)
                MainClass.mainClass.StartInitiator();
            else
                MainClass.mainClass.StopInitiator();
        }



        private void logToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fmLogs_ == null || fmLogs_.IsDisposed)
            {
                fmLogs_ = new FmLogs();
                fmLogs_.Show();
            }
            else
            {
                if (fmLogs_.WindowState == FormWindowState.Minimized)
                    fmLogs_.WindowState = FormWindowState.Normal;
                fmLogs_.Activate();
            }
        }

        private void 下單ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (fmOrder_ == null || fmOrder_.IsDisposed)
            {
                fmOrder_ = new FmOrder();
                fmOrder_.Show();
            }
            else
            {
                if (fmOrder_.WindowState == FormWindowState.Minimized)
                    fmOrder_.WindowState = FormWindowState.Normal;
                fmOrder_.Activate();
            }
        }

        private void FmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.TradeDate = DateTime.Today;
            Properties.Settings.Default.SeqNo = MainClass.mainClass.seqNo;
            Properties.Settings.Default.Save();
        }
    }
}
