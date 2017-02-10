using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Capital.Report.Class;
using OrderProcessor.Capital;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace Capital.Report
{
    public partial class frmSummary : DockContent
    {
        public frmSummary()
        {
            InitializeComponent();
            Utility.LoadConfig(this);
            _LoadLayout();

            NotificationCenter.Instance.AddObserver(OnCapitalInitialize, "isCapitalInit");
            Core.Instance.Order.OnMatchReply += new CapitalProcessor.OnReplyDelegate(OnMatchReply);
        }
        private void frmSummary_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationCenter.Instance.RemoveObserver(OnCapitalInitialize, "isCapitalInit");
            Core.Instance.Order.OnMatchReply -= new CapitalProcessor.OnReplyDelegate(OnMatchReply);
        }

        #region Delegate
        private void OnCapitalInitialize(Notification n)
        {
            _Summary();
        }
        private void OnMatchReply(ReplyType ReplyType, Order Ord)
        {
            _Summary();
        }
        #endregion

        #region Public
        public void ResetLayout()
        {
            Utility.ResetLayout("FORM", "SUMMARYGRIDLAYOUT");
            _LoadLayout();
        }
        public void SaveLayout()
        {
            Utility.SaveLayout("FORM", "SUMMARYGRIDLAYOUT", olvSummary);
        }
        #endregion

        #region Private
        private void _Summary()
        {
            //IEnumerable<object> list = Core.Instance.GetList(ListType.Summary);
            var list = Core.Instance.Order.Summaries();
            if (list != null && list.Count() > 0)
            {
                olvSummary.SetObjects(list, true);
            }
            else
            {
                olvSummary.Items.Clear();
            }
        }
        private void _LoadLayout()
        {
            olvSummary.Items.Clear();
            Utility.LoadLayout("FORM", "SUMMARYGRIDLAYOUT", olvSummary);
            _Summary();
        }
        #endregion
    }
}