using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Capital.Report.Class;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace Capital.Report
{
    public partial class frmErr : DockContent
    {
        public frmErr()
        {
            InitializeComponent();
            _LoadLayout();

            NotificationCenter.Instance.AddObserver(OnCapitalInitialize, "isCapitalInit");
            Core.Instance.Order.OnErrorReply += new OrderProcessor.Capital.CapitalProcessor.OnErrorDelegate(OnErrorReply);
        }
        private void frmOthers_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationCenter.Instance.RemoveObserver(OnCapitalInitialize, "isCapitalInit");
            Core.Instance.Order.OnErrorReply -= new OrderProcessor.Capital.CapitalProcessor.OnErrorDelegate(OnErrorReply);
        }

        #region Delegate
        private void OnErrorReply(OrderProcessor.Capital.Order Ord, string Msg)
        {
            _Error();
        }
        private void OnCapitalInitialize(Notification n)
        {
            _Error();
        }
        #endregion

        #region Public
        public void ResetLayout()
        {
            Utility.ResetLayout("FORM", "ERRORGRIDLAYOUT");
            _LoadLayout();
        }
        public void SaveLayout()
        {
            Utility.SaveLayout("FORM", "ERRORGRIDLAYOUT", olvError);
        }
        #endregion

        #region Private
        private void _Error()
        {
            //IEnumerable<object> list = Core.Instance.GetList(OrderProcessor.Capital.ListType.Error);
            var list = Core.Instance.Order.Errs();
            if (list != null && list.Count() > 0)
            {
                olvError.SetObjects(list, true);
            }
            else
            {
                olvError.Items.Clear();
            }
        }
        private void _LoadLayout()
        {
            olvError.Items.Clear();
            Utility.LoadLayout("FORM", "ERRORGRIDLAYOUT", olvError);
            _Error();
        }
        #endregion
    }
}