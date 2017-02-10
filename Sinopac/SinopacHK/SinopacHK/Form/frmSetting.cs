using System;
using System.Diagnostics;
using System.Windows.Forms;
using OrderProcessor;
using SinopacHK.Class;
using Util.Extension;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace SinopacHK
{
    public partial class frmSetting : DockContent
    {
        public frmSetting()
        {
            InitializeComponent();

            cboPID.Items.AddRange(ProductCollection.Instance.ToArray());
            ProductCollection.MarketPriceEvent += OnPrice;
            NotificationCenter.Instance.AddObserver(SelectedOrder, "SELECTEDORDER");

            Utility.LoadConfig(this);
        }

        private void txtPID_Validated(object sender, EventArgs e)
        {
            Utility.SaveConfig(sender as Control);

            if (sender == txtAmountLimit) { _SetAutoLots(); }
        }
        private void chkSettingMode_CheckedChanged(object sender, EventArgs e)
        {
            cboPID.Enabled = !chkSettingMode.Checked;
            nudLotsLimit.Enabled = !chkSettingMode.Checked;
            chkStopOrder.Enabled = !chkSettingMode.Checked;
            chkOrderAlert.Enabled = !chkSettingMode.Checked;
            nudLots.Select(0, nudLots.Value.ToString().Length);
            nudLots.Focus();
            lblLots.Visible = !chkSettingMode.Checked;
            lblLotsLimit.Visible = !chkSettingMode.Checked;
            lblAmountLimit.Visible = chkSettingMode.Checked;
            txtAmountLimit.Visible = chkSettingMode.Checked;
            lblOrderSeqno.Visible = chkSettingMode.Checked;
            txtSeqno.Visible = chkSettingMode.Checked; ;
        }
        private void cboPID_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductCollection.Select(cboPID.Text);
            nudLots.Increment = ProductCollection.Selected.Units;
            nudManualPrice.Value = 0;
            Utility.Unit.Tick.UpdateAllStatus();

            _SetAutoLots();
            //linkLabel1.Text = new HKProcessor(cboPID.Text).URL.ToString();
            linkLabel1.Links.Clear();

            string url = ProductCollection.Selected.Getter.URL.ToString();
            LinkLabel.Link link = new LinkLabel.Link(0, url.Length-1, url);
            linkLabel1.Links.Add(link);
            txtPID_Validated(cboPID, EventArgs.Empty);
        }
        private void btnBuy_Click(object sender, EventArgs e)
        {
            Utility.Unit.Deal.Order(sender == btnBuy ? Side.B : Side.S, nudManualPrice.Value);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtClOrdID.Text)) { return; }

            Utility.Unit.Deal.Cancel(txtClOrdID.Text.ToUpper());
            txtClOrdID.Text = string.Empty;
        }
        private void btnQuickLots1_Click(object sender, EventArgs e)
        {
            if (chkSettingMode.Checked)
            {
                (sender as Button).Text = nudLots.Value.ToString();
            }
            else
            {
                nudLots.Value = int.Parse((sender as Button).Text);
            }
        }
        private void nudManualPrice_ValueChanged(object sender, EventArgs e)
        {
            decimal increment, decrement;
            new Action(() =>
            {
                ProductCollection.Selected.Getter.Tick(ProductCollection.Selected.TickName, nudManualPrice.Value, out increment, out decrement);
                nudManualPrice.Increment = increment;
            }).BeginInvoke(this);
        }

        private void OnPrice(string PID, string TickName, decimal Price)
        {
            if (nudManualPrice.Value == 0) { nudManualPrice.Value = Price; }
            _SetAutoLots();
            //HKProcessor getter = new HKProcessor("");
            //decimal[] bp = getter.TickIncrement(Price, 20);
            //decimal[] ap = getter.TickDecrement(Price, 20);
            //for (int i = bp.Length-1; i >=0; i--)
            //{
            //    Console.WriteLine("BP" + (i + 1) + ": " + bp[i]);
            //}
            //Console.WriteLine(PID + ": " + Price);
            //for (int i = 0; i < ap.Length; i++)
            //{
            //    Console.WriteLine("AP" + (i + 1) + ": " + ap[i]);
            //}
        }
        private void SelectedOrder(Notification n)
        {
            txtClOrdID.Text = n.Message.ToString();
        }
        private void _SetAutoLots()
        {
            new Action(() =>
            {
                int lots = 0;
                //decimal denominator = ProductCollection.CurrentMarketPrice * ProductCollection.Selected.Units;
                //if (denominator > 0)
                if (ProductCollection.CurrentMarketPrice>0)
                {
                    //lots = (int)(decimal.Parse(txtAmountLimit.Text) / denominator);
                    lots = (int)(decimal.Parse(txtAmountLimit.Text) / ProductCollection.CurrentMarketPrice);
                }
                Utility.AutoLotsLimit = lots;
                lblLots.Text = lots.ToString();
            }).BeginInvoke(lblLots);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Process.Start(linkLabel1.Text);
            Process.Start(e.Link.LinkData.ToString());
        }
    }
}
