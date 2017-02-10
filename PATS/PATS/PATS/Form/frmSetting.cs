using PATS.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATS
{
    public partial class frmSetting : DockContent
    {
        #region Variable
        private bool m_Enable = true;
        #endregion

        #region Property
        public bool Enable
        {
            get { return m_Enable; }
            set
            {
                if (value == m_Enable) { return; }
                m_Enable = value;
                _Enable(this);
            }
        }
        #endregion

        public frmSetting()
        {
            InitializeComponent();
            Util.LoadConfig(this);

        }
        private void frmSetting_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.Parent.Parent.Size = new Size(197, 455);
            }
        }

        private void txtSQLIP_Validated(object sender, EventArgs e)
        {            
            Util.SaveConfig(sender as Control);            
        }
        private void _Enable(Control c)
        {
            foreach (Control sub in c.Controls)
            {
                if (sub.Controls?.Count > 0)
                {
                    _Enable(sub);
                }
            }
            c.BeginInvokeIfRequired(() => { c.Enabled = m_Enable; });
        }

        private void btnResetPATS_Click(object sender, EventArgs e)
        {
            Util.ResetPATS();
        }

        
    }
}
