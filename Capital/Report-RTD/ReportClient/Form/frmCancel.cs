using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Capital.Report.Class;
using OrderProcessor.Capital;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace Capital.Report
{
    public partial class frmCancel : DockContent
    {
        public frmCancel()
        {
            InitializeComponent();
            //載入視窗設定
            _LoadLayout();

            NotificationCenter.Instance.AddObserver(OnCapitalInitialize, "isCapitalInit");
            NotificationCenter.Instance.AddObserver(OnFilterReceive, "DataFilter");
            Core.Instance.Order.OnCancelReply += new CapitalProcessor.OnReplyDelegate(OnCancelReply);
        }
        private void frmCancel_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationCenter.Instance.RemoveObserver(OnCapitalInitialize, "isCapitalInit");
            NotificationCenter.Instance.RemoveObserver(OnFilterReceive, "DataFilter");
            Core.Instance.Order.OnCancelReply -= new CapitalProcessor.OnReplyDelegate(OnCancelReply);
        }

        #region Delegate
        private void OnCancelReply(ReplyType ReplyType, Order Ord)
        {
            _Cancel();
        }
        private void OnCapitalInitialize(Notification n)
        {
            _Cancel();
        }
        /// <summary>
        /// 收到聯動設定
        /// </summary>
        /// <param name="n"></param>
        private void OnFilterReceive(Notification n)
        {
            string Type = n.Sender == null ? string.Empty : n.Sender.ToString();
            string filter = n.Message == null ? string.Empty : n.Message.ToString();
            //沒有聯動
            if (string.IsNullOrEmpty(filter))
            {
                olvCancel.DefaultRenderer = null;
                olvCancel.ModelFilter = null;
            }
            else
            {
                TextMatchFilter filter1 = TextMatchFilter.Contains(olvCancel, filter);
                filter1.Columns = new OLVColumn[] { this.olvcOrdNo };

                if (Type == "Filter")   //過濾
                {
                    olvCancel.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { filter1 });
                    olvCancel.DefaultRenderer = null;
                }
                else if (Type == "Renderer")    //聯動
                {
                    olvCancel.DefaultRenderer = new HighlightTextRenderer(filter1);
                    olvCancel.ModelFilter = null;
                }
                olvCancel.Refresh();
            }
        }
        #endregion

        #region Public
        public void ResetLayout()
        {
            Utility.ResetLayout("FORM", "CANCELGRIDLAYOUT");
            _LoadLayout();
        }
        /// <summary>
        /// 視窗設定存檔
        /// </summary>
        public void SaveLayout()
        {
            Utility.SaveLayout("FORM", "CANCELGRIDLAYOUT", olvCancel);
        }
        #endregion

        #region Private
        private void _Cancel()
        {            
            IEnumerable<object> list = Core.Instance.Order.Cancels();
            if (list != null && list.Count() > 0)
            {
                olvCancel.SetObjects(list, true);
            }
            else
            {
                olvCancel.Items.Clear();
            }
        }
        /// <summary>
        /// 載入設定
        /// </summary>
        private void _LoadLayout()
        {
            olvCancel.Items.Clear();
            Utility.LoadLayout("FORM", "CANCELGRIDLAYOUT", olvCancel);
            _Cancel();
        }
        #endregion
    }
}