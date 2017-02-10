using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PriceCalculator.Utility;
using BrightIdeasSoftware;
using WeifenLuo.WinFormsUI.Docking;
using log4net;

namespace PriceCalculator
{
    public partial class frmFXRate : DockContent
    {
        private ILog m_Log = LogManager.GetLogger(typeof(frmFXRate));

        public frmFXRate()
        {
            InitializeComponent();
        }

        private void olvFX_CellEditStarting(object sender, CellEditEventArgs e)
        {
            if (e.Control is NumericUpDown)
            {
                ((NumericUpDown)e.Control).Select(0, e.Control.Text.Length);
            }
        }

        public void SetCurncy(string etfcode)
        {
            Text = etfcode;
            olvFX.Items.Clear();
            olvFX.AddObjects(Util.FXRates.GetList(etfcode));
        }
    }
}