using BrightIdeasSoftware;
using PATS.Class;
using PATS.Utility;
using PriceLib.PATS;
using System;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATS
{
    public partial class frmOrderReport : DockContent
    {
        public frmOrderReport()
        {
            InitializeComponent();
            Util.LoadConfig(this);
            LoadLayout();

            Util.PATS.OnOrderReply += PATS_OnOrderReply;
            Util.PATS.OnFillReply += PATS_OnFillReply;
            Util.PATS.OnCancelReply += PATS_OnOrderReply;
            Util.PATS.OnErrorReply += PATS_OnErrorReply;
        }
        private void frmOrderReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.PATS.OnOrderReply -= PATS_OnOrderReply;
            Util.PATS.OnFillReply -= PATS_OnFillReply;
            Util.PATS.OnCancelReply -= PATS_OnOrderReply;
            Util.PATS.OnErrorReply -= PATS_OnErrorReply;
        }

        private void chkValid_CheckedChanged(object sender, EventArgs e)
        {
            _Order();
            Util.SaveConfig(chkValid);
        }
        private void olvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            var radio = gupAction.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (radio != radNone && olvOrder.SelectedObject != null)
            {
                //Center.Instance.Post(Observer.OrderFilter, new Msg(radio == radFilter ? "Filter" : "Renderer", ((OrderDetailStruct)olvOrder.SelectedObject).OrderID));
                Center.Instance.Post(Observer.OrderFilter,  ((OrderDetailStruct)olvOrder.SelectedObject).OrderID, radio == radFilter ? "Filter" : "Renderer");
            }
            else
            {
                Center.Instance.Post(Observer.OrderFilter, null);
            }

            if (olvOrder.SelectedObject != null)
            {
                Util.OrderSettingForm.SelectedOrder((OrderDetailStruct)olvOrder.SelectedObject);
                //Center.Instance.Post(Observer.SelectedOrder, new Notification(null, olvOrder.SelectedObject));
            }
        }
        private void radNone_CheckedChanged(object sender, EventArgs e)
        {
            olvOrder_SelectedIndexChanged(this, EventArgs.Empty);
        }

        #region Delegate
        private void PATS_OnErrorReply(OrderDetailStruct order, string msg)
        {
            _Order();
        }
        private void PATS_OnFillReply(string key, FillStruct fill)
        {
            _Order();
        }
        private void PATS_OnOrderReply(string key, OrderDetailStruct order)
        {
            _Order();
        }
        #endregion

        #region Public
        public void ChangeProduct(Tick tick)
        {
            if (tick.ProductInfo == null || string.IsNullOrEmpty(tick.ProductInfo.Key))
            {
                olvOrder.DefaultRenderer = null;
                olvOrder.ModelFilter = null;
            }
            else
            {
                string filter = tick.ProductInfo.Key;
                TextMatchFilter filter1 = TextMatchFilter.Contains(olvOrder, filter);
                filter1.Columns = new OLVColumn[] { olvcKey };
                olvOrder.DefaultRenderer = new HighlightTextRenderer(filter1) { FillBrush =  System.Drawing.Brushes.Aqua};
            }            
            olvOrder.Refresh();
        }
        public void SaveLayout()
        {
            Util.INI["FORMLOCATION"]["ORGLAYOUT"] = Convert.ToBase64String(olvOrder.SaveState());
            Util.SaveConfig();
        }
        #endregion

        #region Private
        private void _Order()
        {
            var list = chkValid.Checked ? Util.PATS.ValidRPTs() : Util.PATS.OrderRPTs();
            if (list != null && list.Count() > 0)
            {
                olvOrder.SetObjects(list, true);
            }
            else
            {
                olvOrder.Items.Clear();
            }
        }
        private void LoadLayout()
        {
            olvOrder.Items.Clear();
            byte[] state = Convert.FromBase64String(Util.INI["FORMLOCATION"]["ORGLAYOUT"]);
            if (state != null && state.Length >0)
            {
                olvOrder.RestoreState(state);
            }
        }
        #endregion
    }
}