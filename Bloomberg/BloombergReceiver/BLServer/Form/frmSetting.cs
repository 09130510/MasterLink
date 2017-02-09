using System;
using System.Windows.Forms;
using BLPServer.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace BLPServer
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
