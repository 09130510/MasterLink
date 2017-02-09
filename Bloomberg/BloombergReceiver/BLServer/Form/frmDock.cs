using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace BLPServer
{
    public partial class frmDock : DockContent
    {
        public frmDock()
        {
            InitializeComponent();

            VS2003Theme t = new VS2003Theme();
            dockPanel1.Theme = t;
        }
    }
}
