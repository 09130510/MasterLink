using System;
using System.Drawing;
using System.Windows.Forms;
using Capital.Report.Class;
using Util.Extension;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace Capital.Report
{
    public partial class frmSetting : DockContent
    {
        public frmSetting()
        {
            InitializeComponent();
            Utility.LoadConfig(this);
        }        
        private void txtAccount_Validated(object sender, EventArgs e)
        {
            Utility.SaveConfig(sender as Control);
        }        
    }
}
