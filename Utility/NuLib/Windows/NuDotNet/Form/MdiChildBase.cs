using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NuDotNet.CustomUIControls;

namespace NuDotNet.CustomUIControls
{
    public partial class MdiChildBase : Form
    {
        #region --  private items  --
        private TabControl m_tabCrtl;
        private TabPage m_tabPag;

        #endregion

        #region --  property  --
        public TabControl TabCtrl { set { m_tabCrtl = value; } }
        public TabPage TabPag { set { m_tabPag = value; } get { return m_tabPag; } }
        #endregion

        public MdiChildBase()
        {
            InitializeComponent();

        }


        private void MdiChildBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.TabPag.Dispose();
            if (!m_tabCrtl.HasChildren)
                m_tabCrtl.Visible = false;
        }

        private void MdiChildBase_Activated(object sender, EventArgs e)
        {
            m_tabCrtl.SelectedTab = m_tabPag;
            if (!m_tabCrtl.Visible)
                m_tabCrtl.Visible = true;
        }
    }
}