using BrightIdeasSoftware;
using PATS.Utility;
using PriceLib.PATS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATS
{
    public partial class frmDealReport : DockContent
    {
        public frmDealReport()
        {
            InitializeComponent();
            LoadLayout();

            Util.PATS.OnFillReply += PATS_OnFillReply;
            Center.Instance.AddObserver(OnFilterReceive, Observer.OrderFilter);

            //FillStruct f = new FillStruct() {  TraderAccount =  "ABCDEFG"};
            //olvDeal.AddObject(f);
        }
        private void frmDealReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.PATS.OnFillReply -= PATS_OnFillReply;
            Center.Instance.RemoveObserver(OnFilterReceive,Observer.OrderFilter);
        }

        #region Delegate
        private void PATS_OnFillReply(string key, FillStruct fill)
        {
            _Match();
        }
        private void OnFilterReceive(Observer MsgName, Msg Msg)
        {
            string Type = Msg.Sender == null ? string.Empty : Msg.Sender.ToString();
            string filter = Msg.Message == null ? string.Empty : Msg.Message.ToString();
            if (string.IsNullOrEmpty(filter))
            {
                olvDeal.DefaultRenderer = null;
                olvDeal.ModelFilter = null;
            }
            else
            {
                TextMatchFilter filter1 = TextMatchFilter.Contains(olvDeal, filter);
                filter1.Columns = new OLVColumn[] { olvcOrderID};
                if (Type == "Filter")
                {   
                    olvDeal.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { filter1 });
                    olvDeal.DefaultRenderer = null;
                }
                else if (Type == "Renderer")
                {
                    olvDeal.ModelFilter = null;
                    olvDeal.DefaultRenderer = new HighlightTextRenderer(filter1) ;
                }
            }
            olvDeal.Refresh();
        }
        #endregion

        #region Public
        public void SaveLayout()
        {
            Util.INI["FORMLOCATION"]["DRGLAYOUT"] = Convert.ToBase64String(olvDeal.SaveState());
            Util.SaveConfig();
        }
        #endregion

        #region Private
        private void _Match()
        {
            //IEnumerable<object> list = Core.Instance.GetList(OrderProcessor.Capital.ListType.Match);
            var list = Util.PATS.DealRPTs();
            if (list != null && list.Count() > 0)
            {
                olvDeal.SetObjects(list, true);
            }
            else
            {
                olvDeal.Items.Clear();
            }
        }
        private void LoadLayout()
        {
            byte[] state = Convert.FromBase64String(Util.INI["FORMLOCATION"]["DRGLAYOUT"]);
            if (state != null && state.Length > 0)
            {
                olvDeal.RestoreState(state);
            }
        }
        #endregion
    }
}
