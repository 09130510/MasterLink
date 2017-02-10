using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PriceCalculator.Utility;
using WeifenLuo.WinFormsUI.Docking;
using log4net;

namespace PriceCalculator
{
    public partial class frmMain2 : Form
    {
        private ILog m_Log = LogManager.GetLogger(typeof(frmMain2));

        public frmMain2()
        {
            InitializeComponent();
            
            Text = Util.VersionInfo(this);
            Util.Init(StatusBar.Items.Cast<ToolStripStatusLabel>().Where(e => e.Tag != null).ToArray());

            Util.ConnectForm.Show(dockPanel1, DockState.DockLeft);
            Util.ErrorForm.Show(dockPanel1, DockState.DockLeft);
            Util.ETFSelectForm.Show(dockPanel1, DockState.DockLeft);
            Util.FXRates.frmFX.Show(Util.ETFSelectForm.Pane, DockAlignment.Bottom, 0.4);
            Util.IIVForm.Show(dockPanel1);

            CheckBox chk = new CheckBox();
            chk.Text = $"Send to REDIS: {Util.INI["MIDDLEREDIS"]["IP"]}:{Util.INI["MIDDLEREDIS"]["PORT"]}@";
            chk.Font = new Font("Verdana", 7);
            chk.Checked = Util.Calculator.SendToChannel = Convert.ToBoolean(Util.INI["SYS"]["SENDTOCHANNEL"]);            
            chk.CheckedChanged += (sender, e) =>
            {
                CheckBox c = (CheckBox)sender;
                Util.Calculator.SendToChannel = c.Checked;
                Util.INI["SYS"]["SENDTOCHANNEL"] = c.Checked.ToString();
                Util.WriteConfig();
                Util.Calculator.Publish();
                c.ForeColor = c.Checked ? Color.Crimson : Color.Black;
            };
            ToolStripControlHost host = new ToolStripControlHost(chk) { Margin=  new Padding(10,0,0,0)};
            toolStrip1.Items.Add(host);
            TextBox txt = new TextBox();
            txt.Text = Util.INI["MIDDLEREDIS"]["CHANNEL"];
            txt.Font = new Font("Verdana", 7);
            txt.Size = new Size(80, 25);
            txt.Validated += (sender, e) =>
            {
                TextBox t = (TextBox)sender;
                Util.INI["MIDDLEREDIS"]["CHANNEL"] = t.Text;
                Util.WriteConfig();
                Util.PublishChannel = t.Text;
            };
            host = new ToolStripControlHost(txt);
            toolStrip1.Items.Add(host);
            StatusBar.Items.Add(Util.StatusLabel);
        }
        private void frmMain2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.Calculator.Dispose();
        }


        private void miConnection_Click(object sender, EventArgs e)
        {
            Util.ConnectForm.Activate();
        }

        private void miError_Click(object sender, EventArgs e)
        {
            Util.ErrorForm.Activate();
        }

        private void miETFSelect_Click(object sender, EventArgs e)
        {
            Util.ETFSelectForm.Activate();
        }

        private void miFXRate_Click(object sender, EventArgs e)
        {
            Util.FXRates.frmFX.Activate();
        }
        private void miIIV_Click(object sender, EventArgs e)
        {
            if ((Util.IIVForm == null) || Util.IIVForm.IsDisposed)
            {
                Util.IIVForm = new frmIIV();
                Util.IIVForm.Show(this.dockPanel1);
            }
            else
            {
                Util.IIVForm.Activate();
            }
        }

        private void miLockYP_Click(object sender, EventArgs e)
        {            
            Util.ETFSelectForm.LockYP();
        }

        private void miNAVDetail_Click(object sender, EventArgs e)
        {
            Util.ETFSelectForm.Detail();
        }

        private void tsOSCapital_DoubleClick(object sender, EventArgs e)
        {
            Util.Calculator.CapitalReconnect();
        }
    }
}