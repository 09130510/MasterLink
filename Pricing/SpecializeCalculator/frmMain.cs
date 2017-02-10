using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriceCalculator
{
    public partial class frmMain : Form
    {
        #region Variable
        private frmAbout m_AboutForm;
        #endregion

        public frmMain()
        {
            InitializeComponent();
            Util.Init();
            Text = Util.VersionInfo(this);

            m_AboutForm = new frmAbout();
            //m_AboutForm.Show(dockPanel1);
            Util.FUTBaseForm.Show(dockPanel1);
            Util.PATS.OnConnectStateChanged += new EventHandler<PriceLib.PATS.ConnectStateEventArgs>(PATS_OnConnectStateChanged);
        }
        private void tsFutureBase_Click(object sender, EventArgs e)
        {
            Util.FUTBaseForm.Show(dockPanel1);
        }
        private void tsAbout_Click(object sender, EventArgs e)
        {
            if (m_AboutForm.IsDisposed || m_AboutForm == null)
            {
                m_AboutForm = new frmAbout();
            }
            m_AboutForm.Show(dockPanel1);
        }
        private void tsPATSConnect_Click(object sender, EventArgs e)
        {
            if (!Util.PATS.isConnected)
            {
                Util.PATS.Connect();
                tsPATSConnect.Enabled = false;
            }
            else
            {
                Util.PATS.Disconnect();
            }
        }

        #region Delegate
        private void PATS_OnConnectStateChanged(object sender, PriceLib.PATS.ConnectStateEventArgs e)
        {
            tsPATSConnect.InvokeIfRequired(() =>
            {
                if (!tsPATSConnect.Enabled) { tsPATSConnect.Enabled = true; }

                tsPATSConnect.Image = e.isConnected ? imageList1.Images[0] : imageList1.Images[1];
                if (e.isConnected)
                {
                    tsPATSConnect.Text = string.Empty;
                }
                else
                {
                    tsPATSConnect.Text = $"{e.LogonState} {e.HostState} {e.PriceState}";
                }
            });
        }
        #endregion
    }
}