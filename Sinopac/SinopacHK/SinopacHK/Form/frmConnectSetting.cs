using System;
using System.Windows.Forms;
using Util.Extension;
using WeifenLuo.WinFormsUI.Docking;

namespace SinopacHK
{
    public partial class frmConnectSetting : DockContent
    {
        public frmConnectSetting()
        {
            InitializeComponent();

            Utility.LoadConfig(this);
            //Utility.Processor.ConnectedEvent += (sender, e) => { lblStatus.ForeColor = Color.Green; };
            //Utility.Processor.DisconnectedEvent += (sender, e) => { lblStatus.ForeColor = Color.Crimson; };

            Utility.Processor.ConnectedEvent += ConnectedEvent;
            Utility.Processor.DisconnectedEvent += ConnectedEvent;
        }
        private void txtBuyCompID_Validated(object sender, EventArgs e)
        {
            Utility.SaveConfig(sender as Control);
        }

        #region Delegate
        private void ConnectedEvent(object sender, EventArgs e)
        {
            new Action(() =>
            {
                txtBuyCompID.Enabled = !Utility.Processor.isConnect;
                txtSellCompID.Enabled = !Utility.Processor.isConnect;
                txtIP.Enabled = !Utility.Processor.isConnect;
                txtPort.Enabled = !Utility.Processor.isConnect;
                txtTag1.Enabled = !Utility.Processor.isConnect;
            }).BeginInvoke(this);
        }
        #endregion
    }
}