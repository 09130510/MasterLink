using PATSOrder.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATSOrder
{
    public partial class frmOrderReport : DockContent
    {
        public frmOrderReport()
        {
            InitializeComponent();
            Util.LoadConfig(this);
            LoadLayout();
        }

        #region Public
        public void SaveLayout()
        {
            Util.INI["FORMLOCATION"]["DRGLAYOUT"] = Convert.ToBase64String(olvOrder.SaveState());
            Util.SaveConfig();
        }
        #endregion

        private void LoadLayout()
        {
            olvOrder.Items.Clear();
            byte[] state = Convert.FromBase64String(Util.INI["FORMLOCATION"]["DRGLAYOUT"]);
            if (state != null)
            {
                olvOrder.RestoreState(state);
            }
        }

        private void frmOrderReport_Load(object sender, EventArgs e)
        {
            var list = Util.PATS.Orders();
            olvOrder.AddObjects(list);
        }
    }
}
