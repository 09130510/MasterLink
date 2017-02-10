using PATS.Properties;
using PATS.Utility;
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

namespace PATS
{
    public partial class frmMain : Form
    {
        

        public frmMain()
        {
            InitializeComponent();
            Text = Util.VersionInfo();
            VS2012LightTheme theme = new VS2012LightTheme();
            dockPanel1.Theme = theme;            

            Util.Init();
            tsWriteMySQL.Checked = bool.Parse(Util.INI["DEALTOEXCELDB"]["WRITE"]);
            tsPin.Checked = bool.Parse(Util.INI["SYS"]["PIN"]);
            tsBring.Checked = bool.Parse(Util.INI["SYS"]["BRINGFORWARD"]);
            LoadLayout();

            Center.Instance.AddObserver(OnPATS_BeforeReset, Observer.PATS_BeforeReset);
            Center.Instance.AddObserver(OnPATS_AfterReset, Observer.PATS_AfterReset);
            OnPATS_AfterReset(Observer.None, null);
            Util.PATS.OnFillReply += PATS_OnFillUpdate;
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //tsConnect_Click(tsDisconnect, EventArgs.Empty);
        }
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            double portion = 190D / dockPanel1.Width;
            dockPanel1.DockRightPortion = portion;
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
            switch ((sender as ToolStripItem).Name)
            {
                case nameof(tsConnect):
                    if (Util.PATS.isConnected) { return; }
                    tsConnect.Enabled = false;
                    Util.PATS.Connect();
                    break;
                case nameof(tsDisconnect):
                    if (!Util.PATS.isConnected) { return; }
                    tsConnect.Enabled = true;
                    Util.PATS.Disconnect();
                    break;
                case nameof(tsOrder):
                    frmTick tick = new frmTick(Util.TickSeqNo.NextNumber());
                    tick.Show(dockPanel1);
                    break;
                case nameof(tsConnectSetting):
                    Util.SettingForm.Show(dockPanel1, DockState.DockRight);
                    break;
                case nameof(tsOrderSetting):
                    Util.OrderSettingForm.Show(dockPanel1, DockState.DockRight);
                    break;
                case nameof(tsProductSetting):
                    Util.ProdFilterForm.ShowDialog(this);
                    break;
                case nameof(tsOrderReport):
                    Util.OrderRPTForm.Show(dockPanel1);
                    break;
                case nameof(tsDealReport):
                    Util.DealRPTForm.Show(dockPanel1);
                    break;
                case nameof(tsCancelReport):
                    Util.CancelRPTForm.Show(dockPanel1);
                    break;
                case nameof(tsErrReport):
                    Util.ErrRPTForm.Show(dockPanel1);
                    break;
                case nameof(tsSummary):
                    Util.SummaryForm.Show(dockPanel1);
                    break;
            }
        }
        private void tsSaveLayout_Click(object sender, EventArgs e)
        {
            Util.INI["FORMLOCATION"]["MAINLOCATION"] = $"{Location.X},{Location.Y},{Width},{Height}";
            Util.DealRPTForm.SaveLayout();
            Util.OrderRPTForm.SaveLayout();
            Util.ErrRPTForm.SaveLayout();
            Util.CancelRPTForm.SaveLayout();
            Util.SummaryForm.SaveLayout();
            

            dockPanel1.SaveAsXml(Util.DOCKLAYOUTFILE);
            AlertBox.AlertWithoutReply(this, AlertBoxButton.Msg_OK, "版面儲存", "儲存完成！");
        }
        private void tsWriteMySQL_CheckedChanged(object sender, EventArgs e)
        {
            Util.INI["MYSQL"]["WRITE"] = tsWriteMySQL.Checked.ToString();
            Util.SaveConfig();
        }
        private void tsPin_CheckedChanged(object sender, EventArgs e)
        {
            tsPin.Image = tsPin.Checked ? Resources.pin : Resources.pin_outline;
            Util.INI["SYS"]["PIN"] = tsPin.Checked.ToString();
            Util.SaveConfig();
        }
        private void tsBring_CheckedChanged(object sender, EventArgs e)
        {
            tsBring.Image = tsBring.Checked ? Resources.bring_forward_16 : Resources.bring_backward_16;
            Util.INI["SYS"]["BRINGFORWARD"] = tsBring.Checked.ToString();
            Util.SaveConfig();
        }
        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (!(((DockPanel)sender).ActiveContent is frmTick)) { return; }
            frmTick tickForm = (frmTick)((DockPanel)sender).ActiveContent;
            if (tickForm != null)
            {
                Util.OrderSettingForm.ChangeInfo(tickForm.Tick);
                Util.OrderRPTForm.ChangeProduct(tickForm.Tick);
                Util.SummaryForm.ChangeProduct(tickForm.Tick);
            }
        }


        #region Delegate
        private void OnPATS_BeforeReset(Observer msgName, Msg msg)
        {
            Util.PATS.OnConnectStateChanged -= PATS_OnConnectStateChanged;
        }
        private void OnPATS_AfterReset(Observer msgName, Msg msg)
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
                tsConnectAction.Image = e.isConnected ? Resources.Connect : Resources.Disconnect;
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
            
            ////寫db
            //string date = DateTime.Now.ToString("yyyy/MM/dd");
            //string qrysql = string.Format("SELECT COUNT(*) FROM CAPITALMATCH WHERE DATE='{0}' AND ORDNO='{1}'", date, o.OrdNo);
            //DataTable dt = m_SQL.DoQuery(qrysql);
            //if (dt.Rows[0][0].ToInt() <= 0)
            //{
            //    string sql = string.Format("INSERT INTO CAPITALMATCH ([DATE],ORDNO,CUSTNO,COMID,SIDE,LOTS,PRICE,TIME) VALUES ('{0}','{1}','{2}','{3}',{4},{5},{6},'{7}')", date, o.OrdNo, o.CustNo, o.ComID, (o.BuySell == OrderProcessor.Capital.BuySell.B ? 0 : 1), o.Qty, o.Price, o.Time);
            //    try
            //    {
            //        m_SQL.DoExecute(sql);
            //    }
            //    catch (Exception) { }

            //}
        }
        #endregion

        #region For Test
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Util.PATS.OrderType();
            //Util.PATS.Orders();
            string orderid;
            //Util.PATS.Order(new NewOrderStruct(), out orderid);
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Center.Instance.Error(nameof(frmMain), "Test");
            Util.SettingForm.Enabled = true;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Center.Instance.Post(Observer.Log, "Test");
            Util.SettingForm.Enabled = false;
        }
        #endregion

        private void LoadLayout()
        {
            int[] bounds = Array.ConvertAll<string, int>(Util.INI["FORMLOCATION"]["MAINLOCATION"].Split(','), (input) => { return string.IsNullOrEmpty(input) ? 0 : int.Parse(input); });
            if (bounds.Length == 4) { this.SetBounds(bounds[0], bounds[1], bounds[2], bounds[3]); }

            if (!File.Exists(Util.DOCKLAYOUTFILE))
            {
                Util.SettingForm.Show(dockPanel1, DockState.DockRight);
                Util.OrderSettingForm.Show(dockPanel1, DockState.DockRight);
                Util.LogForm.Show(dockPanel1, DockState.DockBottomAutoHide);
                tsConnect_Click(tsOrder, EventArgs.Empty);
            }
            else
            {
                dockPanel1.LoadFromXml(Util.DOCKLAYOUTFILE, (persist) =>
                {
                    switch (persist.Split('.')[1])
                    {
                        case nameof(frmCancelReport): return Util.CancelRPTForm;
                        case nameof(frmDealReport): return Util.DealRPTForm;
                        case nameof(frmErrReport): return Util.ErrRPTForm;
                        case nameof(frmLog): return Util.LogForm;
                        case nameof(frmOrderReport): return Util.OrderRPTForm;
                        case nameof(frmOrderSetting): return Util.OrderSettingForm;
                        case nameof(frmSetting): return Util.SettingForm;
                        case nameof(frmSummary): return Util.SummaryForm;
                        case nameof(frmTick): return new frmTick(Util.TickSeqNo.NextNumber());
                        default: return null;
                    }
                });
            }            
        }

        protected override void WndProc(ref Message m)
        {
            if (string.IsNullOrEmpty(Util.INI["SYS"]["PIN"]) || !bool.Parse(Util.INI["SYS"]["PIN"]))
            {
                base.WndProc(ref m);
                return;
            }

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
                    if (string.IsNullOrEmpty(Util.INI["SYS"]["BRINGFORWARD"]) || !bool.Parse(Util.INI["SYS"]["BRINGFORWARD"]))
                    {
                        item.Key.TopMost = false;
                    }                    
                }
            }
        }

        
    }
}
