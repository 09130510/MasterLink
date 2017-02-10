using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Capital.Report.Class;
using PriceProcessor.Capital;
using Util.Extension;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace Capital.Report
{
    public partial class frmOrderSetting : DockContent
    {
        private class Limit
        {
            public string Account;
            public string Exchange;
            public string OrderHead;
            public int Amount;

            public Limit() { }
            public Limit(string str)
            {
                string[] items = str.Split(',');
                Account = items[0];
                Exchange = items[1];
                OrderHead = items[2];
                Amount = int.Parse(items[3]);
            }
            public bool isEqual(string account, string exchange, string head)
            {
                return Account == account && Exchange == exchange && OrderHead == head;
            }

            public override string ToString()
            {
                return string.Join(",", Account, Exchange, OrderHead, Amount) + ";";
            }
        }

        #region Variable
        private Tick m_Tick;
        //private bool m_WaitForUpdateTick = false;
        #endregion

        public frmOrderSetting()
        {
            InitializeComponent();

            Core.Instance.OnPriceChange += new CapitalProcessor.OnPriceChangeDelegate(OnPriceChange);
            NotificationCenter.Instance.AddObserver(SelectedOrder, "SELECTEDORDER");
            NotificationCenter.Instance.AddObserver(ChangeProduct, "CHANGEPRODUCT");
            Utility.LoadConfig(this);
        }
        private void frmOrderSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            Core.Instance.OnPriceChange -= new CapitalProcessor.OnPriceChangeDelegate(OnPriceChange);
            NotificationCenter.Instance.RemoveObserver(SelectedOrder, "SELECTEDORDER");
            NotificationCenter.Instance.RemoveObserver(ChangeProduct, "CHANGEPRODUCT");
        }
        private void chkSettingMode_CheckedChanged(object sender, EventArgs e)
        {
            nudLotsLimit.Enabled = !chkSettingMode.Checked;
            chkStopOrder.Enabled = !chkSettingMode.Checked;
            chkOrderAlert.Enabled = !chkSettingMode.Checked;
            nudLots.Select(0, nudLots.Value.ToString().Length);
            nudLots.Focus();
            lblLots.Visible = !chkSettingMode.Checked;
            lblLotsLimit.Visible = !chkSettingMode.Checked;
            if (!string.IsNullOrEmpty(txtAccount.Text) && !string.IsNullOrEmpty(txtExchange.Text) && !string.IsNullOrEmpty(txtOrderHead.Text))
            {
                lblAmountLimit.Visible = chkSettingMode.Checked;
                txtAmountLimit.Visible = chkSettingMode.Checked;
                txtAmountLimit.Text = (_GetLimit()).ToString();
            }
        }
        private void btnBuy_Click(object sender, EventArgs e)
        {
            Utility.Log(this, "Order", $"Side: {(sender == btnBuy ? "Buy" : "Sell")}  Price: {nudManualPrice.Value}");
            Core.Instance.DoOrder(m_Tick, sender == btnBuy ? OrderProcessor.Side.B : OrderProcessor.Side.S, (double)nudManualPrice.Value);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboAccount.Text) || string.IsNullOrEmpty(txtKeyNo.Text))
            {
                Class.AlertBox.AlertWithoutReply(this, Class.AlertBoxButton.Error_OK, "刪單", "請輸入完整資訊！");

            }
            else
            {
                Utility.Log(this, "Cancel", $"Account: {cboAccount.Text} KeyNo: {txtKeyNo.Text}");
                Core.Instance.DoCancel2(cboAccount.Text, txtKeyNo.Text);
                cboAccount.Text = string.Empty;
                txtKeyNo.Text = string.Empty;
            }
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
        private void nudLots_Validated(object sender, EventArgs e)
        {
            Utility.SaveConfig(sender as Control);
        }
        private void txtAmountLimit_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAmountLimit.Text) || m_Tick == null) { return; }

            _SetLimit(int.Parse(txtAmountLimit.Text));
            _SetAutoLots();
        }

        #region Delegate
        private void ChangeProduct(Notification n)
        {
            m_Tick = (Tick)n.Sender;
            nudManualPrice.Value = 0M;
            if (m_Tick == null)
            {
                txtOrderHead.Text = string.Empty;
                txtOrderYM.Text = string.Empty;
                txtExchange.Text = string.Empty;
                txtAccount.Text = string.Empty;

                lblLots.Text = "0";
                btnBuy.Enabled = btnSell.Enabled = false;
            }
            else
            {
                txtOrderHead.Text = m_Tick.OrderHead;
                txtOrderYM.Text = m_Tick.OrderPID?.Replace(m_Tick.OrderHead, string.Empty);
                txtExchange.Text = m_Tick.Exchange;
                txtAccount.Text = m_Tick.Account;

                cboAccount.Items.Clear();
                cboAccount.Items.AddRange(Core.Instance.Order.Accounts.Select(E => E.BrokerID + E.CustNo).ToArray());
                _SetAutoLots();
                btnBuy.Enabled = btnSell.Enabled = true;
                //m_WaitForUpdateTick = true;

                decimal inTick, deTick;                
                string tickname = CapitalProcessor.TickName($"{m_Tick.Exchange},{m_Tick.OrderHead}");
                decimal mp ;
                decimal.TryParse(m_Tick.cMP.Value.ToString(), out mp);
                PriceProcessor.Processor.Tick(tickname, mp, out inTick, out deTick);
                string numStr = inTick.ToString();
                int Dot = numStr.IndexOf('.');

                nudManualPrice.DecimalPlaces = numStr.Length - (Dot < 0 ? 0 : Dot) - 1;
                nudManualPrice.Increment = inTick;
                nudManualPrice.Value = mp;
            }
        }
        private void OnPriceChange(string PID, string TickName, Price price)
        {
            if (m_Tick != null && PID == m_Tick.PID)
            {
                //換地方
                //if (m_WaitForUpdateTick && price.Market != 0D)
                //{
                //    new Action(() =>
                //    {
                //        decimal inTick, deTick;
                //        //Core.Instance.Price.Tick(TickName, (decimal)price.Market, out inTick, out deTick);
                //        PriceProcessor.Processor.Tick(TickName, (decimal)price.Market, out inTick, out deTick);
                //        string numStr = inTick.ToString();
                //        int Dot = numStr.IndexOf('.');

                //        nudManualPrice.DecimalPlaces = numStr.Length - (Dot < 0 ? 0 : Dot) - 1;
                //        nudManualPrice.Increment = inTick;
                //        nudManualPrice.Value = (decimal)price.Market;
                //        //nudManualPrice.Focus();
                //    }).BeginInvoke(nudManualPrice);
                //    m_WaitForUpdateTick = false;
                //}

                _SetAutoLots();
            }
        }
        private void SelectedOrder(Notification n)
        {
            OrderProcessor.Capital.Order o = (OrderProcessor.Capital.Order)n.Message;
            cboAccount.Text = o.BrokerID + o.CustNo;
            txtKeyNo.Text = o.KeyNo;
        }
        #endregion

        #region Private
        private void _SetAutoLots()
        {

            int limit = _GetLimit();
            int lots = 0;
            double price = (double)m_Tick.cMP.Value;
            if (price > 0)
            {
                lots = (int)(limit / price);
            }
            Core.Instance.AutoLotsLimit = lots;
            new Action(() =>
            {
                lblLots.Text = lots.ToString();
            }).BeginInvoke(lblLots);
        }
        private int _GetLimit()
        {
            string LimitStr = Utility.Load<string>("ORDERSETTING", "AUTOLIMITAMOUNT");
            if (string.IsNullOrEmpty(LimitStr)) { return 0; }
            string[] LimitItems = LimitStr.Split(';');
            List<Limit> Limits = new List<Limit>();
            foreach (var item in LimitItems)
            {
                if (string.IsNullOrEmpty(item)) { continue; }
                Limit L = new Limit(item);
                Limits.Add(L);
            }
            foreach (var L in Limits)
            {
                if (L.isEqual(m_Tick.Account, m_Tick.Exchange, m_Tick.OrderHead))
                {
                    return L.Amount;
                }
            }
            return 0;
        }
        private void _SetLimit(int amount)
        {
            string LimitStr = Utility.Load<string>("ORDERSETTING", "AUTOLIMITAMOUNT");
            string[] LimitItems = LimitStr.Split(';');
            List<Limit> Limits = new List<Limit>();
            foreach (var item in LimitItems)
            {
                if (string.IsNullOrEmpty(item)) { continue; }
                Limit L = new Limit(item);
                if (Limits.FirstOrDefault(e => e.Account == L.Account && e.Exchange == L.Exchange && e.OrderHead == L.OrderHead) == null)
                {
                    Limits.Add(L);
                }
            }

            Limit now = Limits.FirstOrDefault(e => e.Account == m_Tick.Account && e.Exchange == m_Tick.Exchange && e.OrderHead == m_Tick.OrderHead);
            if (now == null)
            {
                now = new Limit() { Account = m_Tick.Account, Exchange = m_Tick.Exchange, OrderHead = m_Tick.OrderHead };
                Limits.Add(now);
            }
            now.Amount = amount;
            string str = "";
            foreach (var item in Limits)
            {
                str += item.ToString();
            }
            Utility.Save("ORDERSETTING", "AUTOLIMITAMOUNT", str);
        }
        #endregion

    }
}
