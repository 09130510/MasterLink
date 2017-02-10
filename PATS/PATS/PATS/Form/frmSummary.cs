using BrightIdeasSoftware;
using PATS.Class;
using PATS.Utility;
using PriceLib.PATS;
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

namespace PATS
{
    public partial class frmSummary : DockContent
    {
        public frmSummary()
        {
            InitializeComponent();
            LoadLayout();

            Util.PATS.OnFillReply += PATS_OnFillReply;
        }
        private void frmSummary_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.PATS.OnFillReply += PATS_OnFillReply;
            
        }
        private void PATS_OnFillReply(string key, FillStruct fill)
        {
            _Summary();
        }

        public void ChangeProduct(Tick tick)
        {
            if (tick.ProductInfo == null || string.IsNullOrEmpty(tick.ProductInfo.Key))
            {
                olvSummary.DefaultRenderer = null;
                olvSummary.ModelFilter = null;
            }
            else
            {
                string filter = tick.ProductInfo.Key;
                TextMatchFilter filter1 = TextMatchFilter.Contains(olvSummary, filter);
                filter1.Columns = new OLVColumn[] { olvcContractKey };
                olvSummary.DefaultRenderer = new HighlightTextRenderer(filter1) { FillBrush =  Brushes.Aqua};             
            }
            olvSummary.Refresh();
        }
        public void SaveLayout()
        {
            Util.INI["FORMLOCATION"]["SGLAYOUT"] = Convert.ToBase64String(olvSummary.SaveState());
            Util.SaveConfig();
        }


        private void _Summary()
        {
            var list = Util.PATS.Summaries();
            if (list != null && list.Count() > 0)
            {
                olvSummary.SetObjects(list, true);
            }
            else
            {
                olvSummary.Items.Clear();
            }
        }
        private void LoadLayout()
        {
            olvSummary.Items.Clear();
            byte[] state = Convert.FromBase64String(Util.INI["FORMLOCATION"]["SGLAYOUT"]);
            if (state != null && state.Length >0)
            {
                olvSummary.RestoreState(state);
            }
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //TextMatchFilter filter = new TextMatchFilter(this.olvSummary, "ABCDEFG111");
        //    TextMatchFilter filter = TextMatchFilter.Contains(this.olvSummary, "ABCDEFG111");
        //    this.olvSummary.DefaultRenderer = new HighlightTextRenderer(filter);
        //    this.olvSummary.AdditionalFilter = filter;
        //    olvSummary.Refresh();
        //}
    }
}
