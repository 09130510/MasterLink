using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Util.Extension;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using Util.Extension.Class;
using SinopacHK.Class;

namespace SinopacHK
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            NotificationCenter.Instance.AddObserver(OnProductChanged, "PRODUCTCHANGED");
            //VersionInfo();

            Utility.Processor.ConnectedEvent += (sender, e) =>
            {
                new Action(() =>
                {
                    tsStatus.ForeColor = Color.DarkGreen;
                }).BeginInvoke(tsStatus);
            };
            Utility.Processor.DisconnectedEvent += (sender, e) =>
            {
                new Action(() =>
                {
                    tsStatus.ForeColor = Color.Firebrick;
                }).BeginInvoke(tsStatus);
            };

            LoadLayout();

            tsConnect_Click(tsConnect, EventArgs.Empty);
        }

        private void tsConnect_Click(object sender, EventArgs e)
        {
            if (sender == tsConnect)
            {
                Utility.Processor.isResetSeq = true;
                Utility.Processor.Start();
            }
            else if (sender == tsLayout)
            {
                Utility.Processor.Stop();
            }
        }
        private void tsLayout_Click(object sender, EventArgs e)
        {
            Utility.Config.Reload();
            Utility.Config.SetSetting("LAYOUT", "LOCATION", string.Format("{0},{1},{2},{3}", Location.X, Location.Y, Width, Height));
            dockPanel1.SaveAsXml(Utility.DockLayoutFile);
            Utility.Unit.SaveLayout();
            AlertBox.AlertWithoutReply(null, AlertBoxButton.Msg_OK, "版面儲存", "儲存完成！");
        }
        private void tsReport_CheckedChanged(object sender, EventArgs e)
        {
            if (!tsReport.Checked)
            {
                if (!Utility.Unit.AliveForm.Visible)
                {
                    return;
                }
                Utility.Unit.AliveForm.Close();
                Utility.Unit.MatchForm.Close();
                this.SetBounds(this.Location.X, this.Location.Y, this.Width - 279, this.Height);
            }
            else
            {
                if (Utility.Unit.AliveForm.Visible)
                {
                    return;
                }
                Utility.Unit.AliveForm.Show(Utility.Unit.OrderForm.Pane, DockAlignment.Right, 0.5);
                Utility.Unit.MatchForm.Show(Utility.Unit.AliveForm.Pane, DockAlignment.Bottom, 0.5); this.SetBounds(this.Location.X, this.Location.Y, this.Width + 279, this.Height);
            }
            if (tsSetting.Checked) { dockPanel1.DockRightPortion = (194d / this.Width); }
        }
        private void tsSetting_CheckedChanged(object sender, EventArgs e)
        {
            if (!tsSetting.Checked)
            {
                if (!Utility.Unit.SettingForm.Visible)
                {
                    return;
                }
                Utility.Unit.SettingForm.Hide();
                Utility.Unit.ConnectForm.Hide();
                this.SetBounds(this.Location.X, this.Location.Y, this.Width - 194, this.Height);
            }
            else
            {
                if (Utility.Unit.SettingForm.Visible)
                {
                    return;
                }
                Utility.Unit.ConnectForm.Show(dockPanel1, DockState.DockRight);
                Utility.Unit.SettingForm.Show(dockPanel1, DockState.DockRight);
                this.SetBounds(this.Location.X, this.Location.Y, this.Width + 194, this.Height);
            }
            if (tsSetting.Checked) { dockPanel1.DockRightPortion = (194d / this.Width); }
        }

        private void VersionInfo()
        {

            object[] attribute = this.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = this.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            //this.Text = Application.ProductName + "   [" + title.Title + "]  " + desc.Description + "  V" + Application.ProductVersion;       
            //string pid = "[" + (ProductCollection.Selected == null ? string.Empty : ProductCollection.Selected.ProductID) + "]";
            string account = "[" + Utility.Load<string>("ORDER", "Account") + "]";
            this.Text = account + "   " + title.Title + "  " + desc.Description + "  V" + Application.ProductVersion;
        }
        /// <summary>
        /// 載入視窗設定
        /// </summary>
        private void LoadLayout()
        {
            int[] bounds = Array.ConvertAll<string, int>(Utility.Load<string>("LAYOUT", "LOCATION").Split(','), (input) => { return !String.IsNullOrEmpty(input) ? int.Parse(input) : 0; });
            Utility.Unit.LoadLayout(dockPanel1);
            if (bounds.Length == 4) { this.SetBounds(bounds[0], bounds[1], bounds[2], bounds[3]); }
        }
        private void OnProductChanged(Notification n)
        {
            VersionInfo();
        }
    }
}
