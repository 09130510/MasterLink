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
    public partial class frmSubscriptionData : DockContent
    {
        public frmSubscriptionData()
        {
            InitializeComponent();

            SetCell();
        }

        public void SetCell()
        {
            if (Utility.Display != null)
            {
                Utility.Display.SetCell(grid1);
            }
        }
    }
}
