using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using PriceCalculator.Utility;
using log4net;
using System.Reflection;

namespace PriceCalculator
{
    public partial class frmConnection : DockContent
    {
        private ILog m_Log = LogManager.GetLogger(typeof(frmConnection));

        public frmConnection()
        {
            InitializeComponent();
            
            //ipush元件是32bit
            radiPush.Enabled = !Environment.Is64BitProcess;            
            _LoadConfig();
        }
        private void radRedis_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rad = (RadioButton)sender;
            Util.Info(m_Log, nameof(radRedis_CheckedChanged), rad.Text + " Checked:" + rad.Checked);
            if (!rad.Checked) { return; }
            Util.Calculator.Source = (Source)Enum.Parse(typeof(Source), rad.Tag.ToString());
            _WriteConfig(rad);
        }
        private void chkPATS_CheckedChanged(object sender, EventArgs e)
        {
            Util.Info(m_Log, nameof(chkPATS_CheckedChanged), "PATS Checked:" + chkPATS.Checked);
            Util.Calculator.ConnectToPATS = chkPATS.Checked;
            _WriteConfig((CheckBox)sender);
        }

        private void _LoadConfig()
        {
            string internalSource = Util.INI["SYS"]["INTERNALSOURCE"];

            foreach (var panelControl in flowLayoutPanel1.Controls)
            {
                if (!(panelControl is GroupBox)) { continue; }
                GroupBox gBox = (GroupBox)panelControl;
                string setting = gBox.Text.Contains("Internal") ? internalSource : "";
                foreach (var control in gBox.Controls)
                {
                    if (!(control is RadioButton)) { continue; }
                    RadioButton rad = (RadioButton)control;
                    rad.Checked = setting == rad.Tag.ToString();
                }
            }

            radRedis.Text = $"Redis  {Util.INI["REDIS"]["IP"]}:{Util.INI["REDIS"]["PORT"]}";
            radiPush.Text = $"iPush  {Util.INI["IPUSH"]["IP"]}";
            chkPATS.Text = $"{Util.INI["PATS"]["PRICEIP"]}:{Util.INI["PATS"]["PRICEPORT"]}, {Util.INI["PATS"]["USER"]}";
            chkCapital.Text = $"Capital {Util.INI["CAPITAL"]["USR"]}";
            lblSQL.Text = $"{Util.INI["SQL"]["IP"]}:{Util.INI["SQL"]["DB"]}";
        }
        private void _WriteConfig(Control ctrl)
        {
            switch (ctrl.Tag.ToString())
            {
                case "None":
                case "iPush":
                case "Redis":
                    Util.INI["SYS"]["INTERNALSOURCE"] = ctrl.Tag.ToString();
                    break;
                case "PATS":
                    Util.INI["PATS"]["CONNECT"] = ((CheckBox)ctrl).Checked.ToString();
                    break;
            }
            Util.WriteConfig();
        }
    }
}
