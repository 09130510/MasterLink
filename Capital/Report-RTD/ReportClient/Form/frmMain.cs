using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Capital.Report.Class;

using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;
using System.Diagnostics;

namespace Capital.Report
{
    public partial class frmMain : Form
    {
        #region Variable
        private string m_DockLayoutFile = @"./DockLayout.xml";
        private frmSetting m_Setting;
        private frmLog m_Log;
        private frmOrder m_Ord;
        private frmMatch m_Mat;
        private frmErr m_Err;
        private frmCancel m_Cancel;
        private frmSummary m_Summary;
        private frmPosition m_Position;
        private frmOrderSetting m_OrderSetting;
        #endregion

        #region Property
        private frmSetting SettingForm
        {
            get
            {
                if (m_Setting == null || m_Setting.IsDisposed) { m_Setting = new frmSetting(); }
                return m_Setting;
            }
        }
        private frmLog LogForm
        {
            get
            {
                if (m_Log == null || m_Log.IsDisposed) { m_Log = new frmLog(); }
                return m_Log;
            }
        }
        private frmOrder OrderForm
        {
            get
            {
                if (m_Ord == null || m_Ord.IsDisposed) { m_Ord = new frmOrder(); }
                return m_Ord;
            }
        }
        private frmMatch MatchForm
        {
            get
            {
                if (m_Mat == null || m_Mat.IsDisposed) { m_Mat = new frmMatch(); }
                return m_Mat;
            }
        }
        private frmErr ErrForm
        {
            get
            {
                if (m_Err == null || m_Err.IsDisposed) { m_Err = new frmErr(); }
                return m_Err;
            }
        }
        private frmCancel CancelForm
        {
            get
            {
                if (m_Cancel == null || m_Cancel.IsDisposed) { m_Cancel = new frmCancel(); }
                return m_Cancel;
            }
        }
        private frmSummary SummaryForm
        {
            get
            {
                if (m_Summary == null || m_Summary.IsDisposed) { m_Summary = new frmSummary(); }
                return m_Summary;
            }
        }
        private frmPosition PositionForm
        {
            get
            {
                if (m_Position == null || m_Position.IsDisposed) { m_Position = new frmPosition(); }
                return m_Position;
            }
        }
        private frmOrderSetting OrderSettingForm
        {
            get
            {
                if (m_OrderSetting == null || m_OrderSetting.IsDisposed) { m_OrderSetting = new frmOrderSetting(); }
                return m_OrderSetting;
            }
        }
        #endregion

        public frmMain()
        {
            InitializeComponent();

            #region Version Info
            object[] attribute = GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            string ostype = Environment.Is64BitProcess ? "x64" : "x86";
#if DEBUG
            Text= $"[{title.Title} - {Process.GetCurrentProcess().Id}] { desc.Description} ({ostype}-D)  V{Application.ProductVersion}";
#else
            Text= $"[{title.Title} - {Process.GetCurrentProcess().Id}] {desc.Description} ({ostype}-R)  V{Application.ProductVersion}";
#endif
            #endregion

            LoadLayout();
            NotificationCenter.Instance.AddObserver(OnConnectChange, "isCapitalConnect");
            NotificationCenter.Instance.AddObserver(OnInitializeChange, "isCapitalInit");
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            string msg;            
            Core.Instance.ReplyDisconnect(out msg);
            Core.Instance.Price.Stop();
        }

        private void tsOrder_Click(object sender, EventArgs e)
        {
            switch ((sender as ToolStripMenuItem).Name)
            {
                case "tsOrder":
                    if (!OrderForm.Created) { OrderForm.Show(dockPanel1, DockState.Document); }
                    OrderForm.Activate();
                    break;
                case "tsCancel":
                    if (!CancelForm.Created) { CancelForm.Show(dockPanel1, DockState.Document); }
                    CancelForm.Activate();
                    break;
                case "tsMatch":
                    if (!MatchForm.Created) { MatchForm.Show(dockPanel1, DockState.Document); }
                    MatchForm.Activate();
                    break;
                case "tsError":
                    if (!ErrForm.Created) { ErrForm.Show(dockPanel1, DockState.Document); }
                    ErrForm.Activate();
                    break;
                case "tsSummary":
                    if (!SummaryForm.Created) { SummaryForm.Show(dockPanel1, DockState.Document); }
                    SummaryForm.Activate();
                    break;
                case "tsPosition":
                    if (!PositionForm.Created) { PositionForm.Show(dockPanel1, DockState.Document); }
                    PositionForm.Activate();
                    break;
            }

        }
        private void tsLayout_Click(object sender, EventArgs e)
        {
            Utility.Save("FORM", "LOCATION", $"{ Location.X},{ Location.Y},{Width},{Height}");
            dockPanel1.SaveAsXml(m_DockLayoutFile);
            OrderForm.SaveLayout();
            MatchForm.SaveLayout();
            ErrForm.SaveLayout();
            CancelForm.SaveLayout();
            SummaryForm.SaveLayout();
            PositionForm.SaveLayout();
            AlertBox.AlertWithoutReply(this, AlertBoxButton.Msg_OK, "版面儲存", "儲存完成！");
        }
        private void tsReloadLayout_Click(object sender, EventArgs e)
        {
            if (File.Exists(m_DockLayoutFile)) { File.Delete(m_DockLayoutFile); }
            LoadLayout();

            OrderForm.ResetLayout();
            MatchForm.ResetLayout();
            ErrForm.ResetLayout();
            CancelForm.ResetLayout();
            SummaryForm.ResetLayout();
            PositionForm.ResetLayout();
        }
        private void tsTickOrder_Click(object sender, EventArgs e)
        {
            frmTick tick = Core.Instance.CreateTickForm(string.Empty);
            if (tick != null) { tick.Show(dockPanel1); }
        }
        private void tsConnect_Click(object sender, EventArgs e)
        {            
            Core.Instance.Initialize();
        }
        private void tsDisconnect_Click(object sender, EventArgs e)
        {
            string msg;            
            Core.Instance.ReplyDisconnect(out msg);
        }
        private void tsFilter_Click(object sender, EventArgs e)
        {
            new frmProductFilter().ShowDialog(this);
        }
        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (((DockPanel)sender).ActiveContent is frmTick)
            {
                frmTick tick = (frmTick)((DockPanel)sender).ActiveContent;
                NotificationCenter.Instance.Post("CHANGEPRODUCT", new Notification(tick.Tick, tick.Product));
            }
        }
        private void toolSripButton1_Click(object sender, EventArgs e)
        {
#if DEBUG
            Core.Instance.Order.TestOrder("./20161109_Reply.log");
#endif
        }

#region Delegate
        private void OnConnectChange(Notification n)
        {
            this.InvokeIfRequired(() =>
            {
                tsStatus.ForeColor = Core.Instance.isCapitalConnect ? Color.DarkGreen : Color.Yellow;
                tsConnect.Enabled = !Core.Instance.isCapitalConnect;
                tsDisconnect.Enabled = Core.Instance.isCapitalConnect;
            });
        }
        private void OnInitializeChange(Notification n)
        {
            tsStatus.InvokeIfRequired(() =>
            {
                tsStatus.ForeColor = Core.Instance.isCapitalInit ? (Core.Instance.isCapitalConnect?Color.DarkGreen:  Color.Yellow ): Color.Crimson;
            });
        }
#endregion

#region Private
        /// <summary>
        /// 載入視窗設定
        /// </summary>
        private void LoadLayout()
        {
            int[] bounds = Array.ConvertAll(Utility.Load<string>("FORM", "LOCATION").Split(','), (input) =>
            {
                int re =  default(int);
                int.TryParse(input, out re);
                return re;
            });
            if (!File.Exists(m_DockLayoutFile))
            {
                SettingForm.Show(dockPanel1, DockState.DockRight);
                OrderSettingForm.Show(SettingForm.Pane, SettingForm);
                LogForm.Show(dockPanel1, DockState.DockBottomAutoHide);

                frmTick tick = Core.Instance.CreateTickForm(string.Empty);
                if (tick != null) { tick.Show(dockPanel1); }
                OrderForm.Show(tick.Pane, DockAlignment.Right, 0.6);
                MatchForm.Show(OrderForm.Pane, DockAlignment.Bottom, 0.5);                
            }
            else
            {
                dockPanel1.LoadFromXml(m_DockLayoutFile, delegate (string persistString)
                {
                    if (persistString == typeof(frmSetting).ToString()) { return SettingForm; }
                    if (persistString == typeof(frmLog).ToString()) { return LogForm; }
                    if (persistString == typeof(frmOrder).ToString()) { return OrderForm; }
                    if (persistString == typeof(frmSummary).ToString()) { return SummaryForm; }
                    if (persistString == typeof(frmMatch).ToString()) { return MatchForm; }
                    if (persistString == typeof(frmErr).ToString()) { return ErrForm; }
                    if (persistString == typeof(frmCancel).ToString()) { return CancelForm; }
                    if (persistString == typeof(frmPosition).ToString()) { return PositionForm; }
                    if (persistString == typeof(frmOrderSetting).ToString()) { return OrderSettingForm; }
                    if (persistString == typeof(frmTick).ToString()) { return Core.Instance.CreateTickForm(string.Empty); }
                    return null;
                });
            }
            if (bounds.Length == 4) { SetBounds(bounds[0], bounds[1], bounds[2], bounds[3]); }
        }
#endregion

        protected override void WndProc(ref Message m)
        {
            Dictionary<FloatWindow, FormWindowState> save = new Dictionary<FloatWindow, FormWindowState>();
            FormWindowState org = this.WindowState;
            foreach (var item in dockPanel1.FloatWindows)
            {
                save.Add(item, item.WindowState);
            }
            base.WndProc(ref m);

            if (this.WindowState != org && this.WindowState == FormWindowState.Minimized)
            {
                foreach (var item in save)
                {
                    item.Key.WindowState = item.Value;
                    item.Key.TopMost = true;
                    item.Key.Show();
                }
            }
        }
    }
}