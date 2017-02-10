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
    public partial class frmErrReport : DockContent
    {
        public frmErrReport()
        {
            InitializeComponent();            
            LoadLayout();

            Util.PATS.OnErrorReply += PATS_OnErrorReply;
            Center.Instance.AddObserver(OnFilterReceive, Observer.OrderFilter);
        }
        private void frmOrderReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.PATS.OnErrorReply -= PATS_OnErrorReply;
            Center.Instance.RemoveObserver(OnFilterReceive, Observer.OrderFilter);
        }

        #region Delegate        
        private void PATS_OnErrorReply(OrderDetailStruct order, string msg)
        {
            _Error();
        }
        private void OnFilterReceive(Observer MsgName, Msg Msg)
        {
            string Type = Msg.Sender == null ? string.Empty : Msg.Sender.ToString();
            string filter = Msg.Message == null ? string.Empty : Msg.Message.ToString();
            //沒有聯動
            if (string.IsNullOrEmpty(filter))
            {
                olvError.DefaultRenderer = null;
                olvError.ModelFilter = null;
            }
            else
            {
                TextMatchFilter filter1 = TextMatchFilter.Contains(olvError, filter);
                filter1.Columns = new OLVColumn[] { olvcOrderID };

                if (Type == "Filter")   //過濾
                {   
                    olvError.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { filter1 });
                    olvError.DefaultRenderer = null;
                }
                else if (Type == "Renderer")    //聯動
                {
                    olvError.ModelFilter = null;
                    olvError.DefaultRenderer = new HighlightTextRenderer(filter1);
                }
            }
            olvError.Refresh();
        }
        #endregion

        #region Public
        public void SaveLayout()
        {
            Util.INI["FORMLOCATION"]["ERGLAYOUT"] = Convert.ToBase64String(olvError.SaveState());
            Util.SaveConfig();
        }
        #endregion

        #region Private
        private void _Error()
        {
            var list = Util.PATS.ErrRPTs();
            if (list != null && list.Count() > 0)
            {
                olvError.SetObjects(list, true);
            }
            else
            {
                olvError.Items.Clear();
            }
        }
        private void LoadLayout()
        {
            olvError.Items.Clear();
            byte[] state = Convert.FromBase64String(Util.INI["FORMLOCATION"]["ERGLAYOUT"]);
            if (state != null && state.Length >0)
            {
                olvError.RestoreState(state);
            }
        }
        #endregion
    }
}