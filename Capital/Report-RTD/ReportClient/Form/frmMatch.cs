using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Capital.Report.Class;
using OrderProcessor.Capital;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;
using System.Threading;

namespace Capital.Report
{
    public partial class frmMatch : DockContent
    {
        public frmMatch()
        {
            InitializeComponent();
            _LoadLayout();

            NotificationCenter.Instance.AddObserver(OnCapitalInitialize, "isCapitalInit");
            NotificationCenter.Instance.AddObserver(OnFilterReceive, "DataFilter");
            Core.Instance.Order.OnMatchReply += new CapitalProcessor.OnReplyDelegate(OnMatchReply);
            _Match();
        }
        private void frmMatch_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationCenter.Instance.RemoveObserver(OnCapitalInitialize, "isCapitalInit");
            NotificationCenter.Instance.RemoveObserver(OnFilterReceive, "DataFilter");
            Core.Instance.Order.OnMatchReply -= new CapitalProcessor.OnReplyDelegate(OnMatchReply);
        }

        #region Delegate
        private void OnMatchReply(ReplyType ReplyType, Order Ord)
        {
            _Match();
        }
        private void OnCapitalInitialize(Notification n)
        {
            _Match();
        }
        private void OnFilterReceive(Notification n)
        {
            string Type = n.Sender == null ? string.Empty : n.Sender.ToString();
            string filter = n.Message == null ? string.Empty : n.Message.ToString();
            if (string.IsNullOrEmpty(filter))
            {
                olvMatch.DefaultRenderer = null;
                olvMatch.ModelFilter = null;
            }
            else
            {
                TextMatchFilter filter1 = TextMatchFilter.Contains(olvMatch, filter);
                filter1.Columns = new OLVColumn[] { this.olvcOrdNo };
                if (Type == "Filter")
                {
                    olvMatch.ModelFilter = new CompositeAllFilter(new List<IModelFilter> { filter1 });
                    olvMatch.DefaultRenderer = null;
                }
                else if (Type == "Renderer")
                {
                    olvMatch.DefaultRenderer = new HighlightTextRenderer(filter1);
                    olvMatch.ModelFilter = null;
                }
                olvMatch.Refresh();
            }
        }
        #endregion

        #region Public
        public void ResetLayout()
        {
            Utility.ResetLayout("FORM", "MATCHGRIDLAYOUT");
            _LoadLayout();
        }
        public void SaveLayout()
        {
            Utility.SaveLayout("FORM", "MATCHGRIDLAYOUT", olvMatch);
        }
        #endregion

        #region Private
        private void _Match()
        {
            //IEnumerable<object> list = Core.Instance.GetList(OrderProcessor.Capital.ListType.Match);
            var list = Core.Instance.Order.Deals();
            if (list != null && list.Count() > 0)
            {
                olvMatch.SetObjects(list, true);
            }
            else
            {
                olvMatch.Items.Clear();
            }
        }
        private void _Match(Order o)
        {
            ThreadPool.QueueUserWorkItem((args) =>
            {
                olvMatch.AddObject(args);
            }, o);
        }

        private void _LoadLayout()
        {
            olvMatch.Items.Clear();
            Utility.LoadLayout("FORM", "MATCHGRIDLAYOUT", olvMatch);
            _Match();
        }
        #endregion
    }
}
