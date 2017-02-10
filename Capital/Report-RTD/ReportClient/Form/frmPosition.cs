using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Capital.Report.Class;
using OrderProcessor.Capital;
using Util.Extension;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace Capital.Report
{
    public partial class frmPosition : DockContent
    {
        public frmPosition()
        {
            InitializeComponent();
            _LoadLayout();

            NotificationCenter.Instance.AddObserver(OnCapitalInitialize, "isCapitalInit");
            Core.Instance.Order.OnOverseaOpenInterest += new CapitalProcessor.OnOverseaOpenInterestDelegate(OnOverseaOpenInterest);

            OnCapitalInitialize(Notification.Empty);
            btnRefresh_Click(btnRefresh, EventArgs.Empty);
        }
        private void frmPosition_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationCenter.Instance.RemoveObserver(OnCapitalInitialize, "isCapitalInit");
            Core.Instance.Order.OnOverseaOpenInterest -= new CapitalProcessor.OnOverseaOpenInterestDelegate(OnOverseaOpenInterest);
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Utility.Log(this, "OpenInterest", "取得未平倉資料");
            if (Core.Instance.isCapitalInit)
            {
                Core.Instance.Order.GetOpenInterest();
            }
        }

        #region Delegate
        private void OnCapitalInitialize(Notification n)
        {
            new Action(() =>
            {
                btnRefresh.Enabled = Core.Instance.isCapitalInit;
            }).BeginInvoke(btnRefresh);
        }
        private void OnOverseaOpenInterest(List<OpenInterest> OpenInterests)
        {
            new Action(() =>
            {
                olvPosition.Items.Clear();
                olvPosition.AddObjects(OpenInterests);
                lblTime.Text = DateTime.Now.ToString("HH:mm:ss.fff");
            }).BeginInvoke(this);
        }
        #endregion

        #region Public
        public void ResetLayout()
        {
            Utility.ResetLayout("FORM", "POSITIONGRIDLAYOUT");
            _LoadLayout();
        }
        public void SaveLayout()
        {
            Utility.SaveLayout("FORM", "POSITIONGRIDLAYOUT", olvPosition);
        }
        #endregion

        #region Private
        private void _LoadLayout()
        {
            olvPosition.Items.Clear();
            Utility.LoadLayout("FORM", "POSITIONGRIDLAYOUT", olvPosition);
        }
        #endregion
    }
}