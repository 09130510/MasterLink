using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NuDotNet;

namespace NuDotNet.CustomUIControls
{
    public partial class MDIFrm : Form
    {
        #region --  private function  --
        private bool SearchTabPage(string sTitle)
        {
            foreach (MdiChildBase frm in this.MdiChildren)
            {
                if (frm.Text.Equals(sTitle))
                {
                    frm.Select();
                    return true;
                }
            }
            return false;
        }

       

        #endregion

        public MDIFrm()
        {
            InitializeComponent();
        }

        #region tabIndexMenu event [tabControl]
        private void tabIndexMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (MdiChildBase frm in this.MdiChildren)
            {
                if (frm.TabPag.Equals(tabIndexMenu.SelectedTab))
                    frm.Select();
            }
        }
        #endregion

        #region --  public property  --
        public TabAppearance Tab_Appearance { set { tabIndexMenu.Appearance = value; } get { return tabIndexMenu.Appearance; } }
        public Font Tab_Font { set { tabIndexMenu.Font = value; } get { return tabIndexMenu.Font; } }
        #endregion

        #region --  public function  --
        public ToolStripMenuItem CreateMenu(string title)
        {
            ToolStripMenuItem mItem = new ToolStripMenuItem(title);
            menuStrip1.Items.Add(mItem);
            return mItem;
        }

        public ToolStripMenuItem CreateSubMenu(ref ToolStripMenuItem menu, string title)
        {
            ToolStripMenuItem mitem = new ToolStripMenuItem(title);
            menu.DropDownItems.Add(mitem);
            return mitem;
        }

        public void AddFormToMdi(MdiChildBase frmBase)
        {
           AddFormToMdi(frmBase, frmBase.Text);
        }

        public void AddFormToMdi(MdiChildBase frmBase, string sTitle)
        {
            if (!SearchTabPage(sTitle))
            {
                #region Add MDIChildForm
                frmBase.Text = sTitle;
                frmBase.MdiParent = this;
                //frmBase.FormBorderStyle = FormBorderStyle.None;
                //frmBase.ControlBox = false;
                //frmBase.MaximizeBox = false;
                //frmBase.MinimizeBox = false;
                //frmBase.Dock = DockStyle.Fill;

                frmBase.WindowState = FormWindowState.Maximized;
                frmBase.TabCtrl = tabIndexMenu;
                frmBase.TabPag = new TabPage();
                frmBase.TabPag.Parent = tabIndexMenu;
                frmBase.TabPag.Text = frmBase.Text;
                frmBase.TabPag.Show();
                frmBase.Select();
                frmBase.Show();
                #endregion
            }
        }

        #endregion
    }
}