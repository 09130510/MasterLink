
using PATSOrder.Class;
using PATSOrder.Properties;
using PATSOrder.Utility;
using PriceLib.PATS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATSOrder
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
            this.Text = Util.VersionInfo();
            Util.Init();
            LoadLayout();
            Center.Instance.AddObserver(OnPATS_BeforeReset, Util.PATS_BEFORERESET);
            Center.Instance.AddObserver(OnPATS_AfterReset, Util.PATS_AFTERRESET);


            OnPATS_AfterReset("", null);
            Util.PATS.OnFillUpdate += PATS_OnFillUpdate;
        }
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            double portion = 190D / dockPanel1.Width;            
            dockPanel1.DockRightPortion = portion;            
        }

        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (!(((DockPanel)sender).ActiveContent is frmTick)) { return; }
            frmTick tickForm = (frmTick)((DockPanel)sender).ActiveContent;
            if (tickForm != null)
            {
                Util.OrderSettingForm.Change(tickForm.Tick);
            }
        }
        private void tsConnectAction_ButtonClick(object sender, EventArgs e)
        {
            tsConnectAction.BeginInvokeIfRequired(() =>
            {
                tsConnectAction.Text = string.Empty;
            });
        }
        private void tsConnect_Click(object sender, EventArgs e)
        {
            if (Util.PATS.isConnected) { return; }
            tsConnect.Enabled = false;
            Util.PATS.Connect();
        }
        private void tsDisconnect_Click(object sender, EventArgs e)
        {
            if (!Util.PATS.isConnected) { return; }
            tsConnect.Enabled = true;
            Util.PATS.Disconnect();
        }
        private void tsOrder_Click(object sender, EventArgs e)
        {
            frmTick tick = new frmTick(Util.TickSeqNo.NextNumber());
            tick.Show(dockPanel1);
        }
        private void tsConnectSetting_Click(object sender, EventArgs e)
        {
            Util.SettingForm.Show(dockPanel1, DockState.DockRight);
        }
        private void tsProductSetting_Click(object sender, EventArgs e)
        {
            Util.ProdFilterForm.ShowDialog(this);
        }
        private void tsOrderReport_Click(object sender, EventArgs e)
        {
            Util.OrderRPTForm.Show(dockPanel1);
        }
        private void tsDealReport_Click(object sender, EventArgs e)
        {
            Util.DealRPTForm.Show(dockPanel1);
        }
        private void tsCancelReport_Click(object sender, EventArgs e)
        {
            Util.CancelRPTForm.Show(dockPanel1);
        }
        private void tsErrReport_Click(object sender, EventArgs e)
        {
            Util.ErrRPTForm.Show(dockPanel1);
        }
        private void tsSummary_Click(object sender, EventArgs e)
        {
            Util.SummaryForm.Show(dockPanel1);
        }
        private void tsSaveLayout_Click(object sender, EventArgs e)
        {
            Util.INI["FORMLOCATION"]["MAINLOCATION"] = $"{Location.X},{Location.Y},{Width},{Height}";
            dockPanel1.SaveAsXml(Util.DOCKLAYOUTFILE);
            Util.DealRPTForm.SaveLayout();

            AlertBox.AlertWithoutReply(this, AlertBoxButton.Msg_OK, "版面儲存", "儲存完成！");
        }
        private void tsOrderSetting_Click(object sender, EventArgs e)
        {
            Util.OrderSettingForm.Show(dockPanel1, DockState.DockRight);
        }

        #region Delegate
        private void OnPATS_BeforeReset(string msgName, Msg msg)
        {
            Util.PATS.OnConnectStateChanged -= PATS_OnConnectStateChanged;
        }
        private void OnPATS_AfterReset(string msgName, Msg msg)
        {
            Util.PATS.OnConnectStateChanged += PATS_OnConnectStateChanged;
        }
        private void PATS_OnConnectStateChanged(object sender, ConnectStateEventArgs e)
        {
            this.BeginInvokeIfRequired(() =>
            {
                //tsConnectAction.Text = e.isConnected ? "█" : $"█ {e.LogonState} {e.HostState} {e.PriceState}";
                //tsConnectAction.ForeColor = e.isConnected ? Color.Green : Color.Crimson;
                tsConnectAction.Text = e.isConnected ? string.Empty : $"{e.LogonState} {e.HostState} {e.PriceState}";
                tsConnectAction.Image = e.isConnected ? Resources._1469092136_connect : Resources._1469092096_disconnect;
                tsConnect.Enabled = !e.isConnected;
                tsDisconnect.Enabled = e.isConnected;
            });
            Util.SettingForm.BeginInvokeIfRequired(() =>
            {
                Util.SettingForm.Enable = !e.isConnected;
            });

            //if (e.DLComplete)
            //{
            //    if (Util.ProductInfos == null)
            //    {
            //        Util.ProductInfos = ProductInfo.Convert(Util.PATS.Products());
            //        Center.Instance.Post(nameof(Util.PATS), nameof(Util.ProductInfos));
            //    }
            //    if (Util.AccountInfos == null)
            //    {
            //        Util.AccountInfos = AccountInfo.Convert(Util.PATS.Traders());
            //        Center.Instance.Post(nameof(Util.PATS), nameof(Util.AccountInfos));
            //    }                
            //}
            //else
            //{
            //    if (Util.ProductInfos!= null)
            //    {
            //        Util.ProductInfos.Clear();
            //        Center.Instance.Post(nameof(Util.PATS), nameof(Util.ProductInfos));
            //    }
            //    if (Util.AccountInfos != null)
            //    {
            //        Util.AccountInfos.Clear();
            //        Center.Instance.Post(nameof(Util.PATS), nameof(Util.AccountInfos));
            //    }
            //}
        }
        private void PATS_OnFillUpdate(string key, FillStruct fill)
        {

        }
        #endregion

        #region For Test
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Center.Instance.Error(nameof(frmMain), "Test");
            Util.SettingForm.Enabled = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Center.Instance.Post(nameof(Util.LOG), "Test");
            Util.SettingForm.Enabled = false;
        }
        #endregion
        

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Util.PATS.Orders();
        }

        private void LoadLayout()
        {
            int[] bounds = Array.ConvertAll<string, int>(Util.INI["FORMLOCATION"]["MAINLOCATION"].Split(','), (input) => { return string.IsNullOrEmpty(input) ? 0 : int.Parse(input); });
            if (bounds.Length == 4) { this.SetBounds(bounds[0], bounds[1], bounds[2], bounds[3]); }

            if (!File.Exists(Util.DOCKLAYOUTFILE))
            {
                Util.SettingForm.Show(dockPanel1, DockState.DockRight);
                Util.OrderSettingForm.Show(dockPanel1, DockState.DockRight);
                Util.LogForm.Show(dockPanel1, DockState.DockBottomAutoHide);
                tsOrder_Click(tsOrder, EventArgs.Empty);
            }
            else
            {
                dockPanel1.LoadFromXml(Util.DOCKLAYOUTFILE, (persist) =>
                {
                    switch (persist.Split('.')[1])
                    {
                        case nameof(frmSetting): return Util.SettingForm;
                        case nameof(frmOrderSetting): return Util.OrderSettingForm;
                        case nameof(frmLog): return Util.LogForm;
                        case nameof(frmTick): return new frmTick(Util.TickSeqNo.NextNumber());
                        default: return null;
                    }
                });
            }
        }
        
    }
}