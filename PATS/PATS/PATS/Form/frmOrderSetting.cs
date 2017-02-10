using PATS.Class;
using PATS.Utility;
using PriceLib.PATS;
using System;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATS
{
    public partial class frmOrderSetting : DockContent
    {
        private class AmountLimit
        {
            const string SPLIT = ",";
            public string TradeAccount;
            public string Exch;
            public string Contract;
            public int Amount;
            public AmountLimit(string str)
            {
                string[] items = str.Split(new string[] { SPLIT }, StringSplitOptions.None);
                for (int i = 0; i < items.Length; i++)
                {
                    if (i == 0) { TradeAccount = items[i]; }
                    if (i == 1) { Exch = items[i]; }
                    if (i == 2) { Contract = items[i]; }
                    if (i == 3) { Amount = int.Parse(items[i]); }
                }
            }
            public bool isEqual(string acc, string exch, string contract)
            {
                return acc == TradeAccount && exch == Exch && contract == Contract;
            }

            public override string ToString()
            {
                return string.Join(SPLIT, TradeAccount, Exch, Contract, Amount) + ";";
            }
        }
        #region Variable
        private Tick m_Tick;
        #endregion


        public frmOrderSetting()
        {
            InitializeComponent();
            Util.LoadConfig(this);

            Util.PATS.OnPriceUpdate += PATS_OnPriceUpdate;
        }
        private void frmOrderSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.PATS.OnPriceUpdate -= PATS_OnPriceUpdate;
        }

        private void frmOrderSetting_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.Parent.Parent.Size = new Size(197, 455);
            }
        }
        private void chkSettingMode_CheckedChanged(object sender, EventArgs e)
        {
            nudLotsLimit.Enabled = chkForbidden.Enabled = lblLots.Visible = lblLotsLimit.Visible = txtExch.Visible= chkAlert.Enabled = !chkSettingMode.Checked;
            cboExch.Visible = chkSettingMode.Checked;
            cboExch.Items.Clear();
            if (Util.ExchangeInfo != null)
            {
                cboExch.Items.AddRange(Util.ExchangeInfo.Exchs);
                cboExch.SelectedItem = txtExch.Text;
            }
            nudLots.Select(0, nudLots.Value.ToString().Length);
            nudLots.Focus();
            if (!string.IsNullOrEmpty(txtAccount.Text) && !string.IsNullOrEmpty(txtExch.Text) && !string.IsNullOrEmpty(txtContract.Text))
            {
                lblAmountLimit.Visible = txtAmountLimit.Visible = chkSettingMode.Checked;
                txtAmountLimit.Text = _GetLimit();
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
                int lots = default(int);
                int.TryParse((sender as Button).Text, out lots);
                nudLots.Value = lots;
            }
        }
        private void btnBuy_Click(object sender, EventArgs e)
        {

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
        private void nudLots_Validated(object sender, EventArgs e)
        {
            Util.SaveConfig(sender as Control);
        }
        
        private void txtAmountLimit_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAmountLimit.Text) || m_Tick == null)
            {
                return;
            }
        }

        private void PATS_OnPriceUpdate(string key, PriceStruct price)
        {
            //throw new NotImplementedException();
        }

        public void ChangeInfo(Tick tick)
        {
            ChangeAccount(tick);
            ChangeProduct(tick);
        }
        public void ChangeAccount(Tick tick)
        {
            if (tick != m_Tick) { m_Tick = tick; }
            if (tick.Account == null) {
                txtAccount.Text = string.Empty;
            }
            else
            {
                txtAccount.Text = tick.Account.TraderAccount;
            }            
        }
        public void ChangeProduct(Tick tick)
        {
            if (tick != m_Tick) { m_Tick = tick; }
            if (tick.ProductInfo == null)
            {
                txtExch.Text = txtContract.Text = string.Empty;
                cboOrderType.Items.Clear();                  
            }
            else
            {
                string oldType = string.IsNullOrEmpty(cboOrderType.Text) || txtExch.Text != m_Tick.ProductInfo.Exch ? Util.ExchangeInfo.DefaultType(m_Tick.ProductInfo.Exch) : cboOrderType.Text;
                txtExch.Text = m_Tick.ProductInfo.Exch;
                cboOrderType.Items.Clear();
                cboOrderType.Items.AddRange(Util.ExchangeInfo[txtExch.Text].ToArray());
                
                cboOrderType.SelectedItem = oldType;
                
                
                //cboOrderType.Items.AddRange(m_Tick.ProductInfo.OrderTypes.ToArray());
                //string dType = Util.INI["SYS"]["DEFAULTORDERTYPE"];
                //if (!string.IsNullOrEmpty(dType) && cboOrderType.Items.Contains(dType))
                //{
                //    cboOrderType.SelectedItem = dType;
                //}
                txtContract.Text = $"{m_Tick.ProductInfo.Commodity},{m_Tick.ProductInfo.Date}";
            }
        }
        public void SelectedOrder(OrderDetailStruct order)
        {

        }

        private string _GetLimit()
        {
            string[] items = Util.INI["SYS"]["AUTOLIMITAMOUNT"].Split(';');
            foreach (var item in items)
            {
                AmountLimit limit = new AmountLimit(item);
                if (limit.isEqual(m_Tick.Account.TraderAccount, m_Tick.ProductInfo.Exch, m_Tick.ProductInfo.Commodity))
                {
                    return limit.Amount.ToString();
                }
            }
            return "0";
        }
        private void _SetLimit(int amount)
        {
            string[] items = Util.INI["SYS"]["AUTOLIMITAMOUNT"].Split(';');
        }

        private void cboExch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboOrderType.Items.Clear();
            cboOrderType.Items.AddRange(Util.ExchangeInfo[cboExch.Text].ToArray());
            cboOrderType.SelectedItem = Util.ExchangeInfo.DefaultType(cboExch.Text);
        }

        private void cboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkSettingMode.Checked)
            {
                Util.ExchangeInfo.SaveDefaultType(cboExch.Text, cboOrderType.Text);
            }
        }
    }
}