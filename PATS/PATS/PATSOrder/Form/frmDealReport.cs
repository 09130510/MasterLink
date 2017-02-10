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
    public partial class frmDealReport : DockContent
    {
        public frmDealReport()
        {
            InitializeComponent();
            LoadLayout();
        }

        private void frmDealReport_Load(object sender, EventArgs e)
        {
            var list = Util.PATS.Fills();
            olvDeal.AddObjects(list);
        }

        #region Public
        public void SaveLayout()
        {
            Util.INI["FORMLOCATION"]["DRGLAYOUT"] = Convert.ToBase64String(olvDeal.SaveState());
            Util.SaveConfig();
        }
        #endregion

        private void LoadLayout()
        {
            byte[] state = Convert.FromBase64String(Util.INI["FORMLOCATION"]["DRGLAYOUT"]);
            if (state != null && state.Length > 0)
            {
                olvDeal.RestoreState(state);
            }
        }
    }
}
