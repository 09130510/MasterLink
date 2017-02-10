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
    public partial class frmCancelReport : DockContent
    {
        public frmCancelReport()
        {
            InitializeComponent();            
            LoadLayout();
            
            Util.PATS.OnCancelReply += PATS_OnOrderReply;
            Center.Instance.AddObserver(OnFilterReceive, Observer.OrderFilter);
        }       

        private void frmOrderReport_FormClosing(object sender, FormClosingEventArgs e)
        {            
            Util.PATS.OnCancelReply -= PATS_OnOrderReply;
            Center.Instance.RemoveObserver(OnFilterReceive, Observer.OrderFilter);
        }

        #region Delegate        
        private void PATS_OnOrderReply(string key, OrderDetailStruct order)
        {
            _Cancel();
        }
        /// <summary>
        /// 收到聯動設定
        /// </summary>
        /// <param name="n"></param>
        private void OnFilterReceive(Observer MsgName, Msg Msg)
        {
            string Type = Msg.Sender == null ? string.Empty : Msg.Sender.ToString();
            string filter = Msg.Message == null ? string.Empty : Msg.Message.ToString();
            //沒有聯動
            if (string.IsNullOrEmpty(filter))
            {
                olvCancel.DefaultRenderer = null;
                olvCancel.ModelFilter = null;
            }
            else
            {
                TextMatchFilter filter1 = TextMatchFilter.Contains(olvCancel, filter);
                filter1.Columns = new OLVColumn[] { olvcOrderID };

                if (Type == "Filter")   //過濾
                {                    
                    olvCancel.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { filter1 });
                    olvCancel.DefaultRenderer = null;
                }
                else if (Type == "Renderer")    //聯動
                {
                    olvCancel.ModelFilter = null;
                    olvCancel.DefaultRenderer = new HighlightTextRenderer(filter1) ;
                }
            }
            olvCancel.Refresh();
        }    
        #endregion

        #region Public
        public void SaveLayout()
        {
            Util.INI["FORMLOCATION"]["CRGLAYOUT"] = Convert.ToBase64String(olvCancel.SaveState());
            Util.SaveConfig();
        }
        #endregion

        #region Private
        private void _Cancel()
        {
            var list = Util.PATS.CancelRPTs();
            if (list != null && list.Count() > 0)
            {
                olvCancel.SetObjects(list, true);
            }
            else
            {
                olvCancel.Items.Clear();
            }
        }
        private void LoadLayout()
        {
            olvCancel.Items.Clear();
            byte[] state = Convert.FromBase64String(Util.INI["FORMLOCATION"]["CRGLAYOUT"]);
            if (state != null&& state.Length >0 )
            {
                olvCancel.RestoreState(state);
            }
        }
        #endregion
    }
}