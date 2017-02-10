using PriceCalculator.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PriceCalculator
{
    public partial class frmError : DockContent
    {
        public frmError()
        {
            InitializeComponent();
        }

        public void Err(string msg)
        {
            this.InvokeIfRequired(() =>
            {
                txtError.Text = string.Concat(txtError.Text, "\r\n", DateTime.Now.ToString("HH:mm:ss.fff"), "    ", msg);
                Activate();
            });
        }
    }
}
