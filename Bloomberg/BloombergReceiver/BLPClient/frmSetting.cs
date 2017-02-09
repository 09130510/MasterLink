using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using BLPClient.Class;

namespace BLPClient
{
    public partial class frmSetting : DockContent
    {
        public frmSetting()
        {
            InitializeComponent();

            Utility.LoadConfig(this);
        }

        private void txtPSPort_Validated(object sender, EventArgs e)
        {
            Utility.SaveConfig((Control)sender);
        }
    }
}
