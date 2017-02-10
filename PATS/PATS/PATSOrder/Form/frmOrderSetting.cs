using PATSOrder.Class;
using PATSOrder.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATSOrder
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
                string[] items = str.Split(new string[]{ SPLIT },  StringSplitOptions.None);
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
                return acc == TradeAccount &&exch == Exch && contract == Contract;
            }

            public override string ToString()
            {
                return string.Join(SPLIT, TradeAccount, Exch, Contract, Amount) + ";";
            }
        }        
        private Tick m_Tick;

        public frmOrderSetting()
        {
            InitializeComponent();
            Util.LoadConfig(this);
        }
        private void frmOrderSetting_DockStateChanged(object sender, EventArgs e)
        {
            if (this.DockState == DockState.Float)
            {
                this.Parent.Parent.Size = new Size(197, 455);
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
        private void chkSettingMode_CheckedChanged(object sender, EventArgs e)
        {
            nudLotsLimit.Enabled = chkForbidden.Enabled = lblLots.Visible = lblLotsLimit.Visible = chkAlert.Enabled = !chkSettingMode.Checked;
            nudLots.Select(0, nudLots.Value.ToString().Length);
            nudLots.Focus();
            if (!string.IsNullOrEmpty(txtAccount.Text) && !string.IsNullOrEmpty(txtExch.Text) && !string.IsNullOrEmpty(txtContract.Text))
            {
                lblAmountLimit.Visible = txtAmountLimit.Visible = chkSettingMode.Checked;
                txtAmountLimit.Text = _GetLimit();
            }
        }    
        private void txtAmountLimit_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAmountLimit.Text) || m_Tick== null)
            {
                return;
            }
            
        }

        public void Change(Tick tick)
        {
            ChangeAccount(tick);
            ChangeProduct(tick);
        }
        public void ChangeAccount(Tick tick)
        {
            if (tick != m_Tick) { m_Tick = tick; }
            if (tick.Account == null) { return; }
            txtAccount.Text = tick.Account.TraderAccount;
        }
        public void ChangeProduct(Tick tick)
        {
            if (tick != m_Tick) { m_Tick = tick; }
            if (tick.ProductInfo == null) { return; }
            txtExch.Text = m_Tick.ProductInfo.Exch;
            txtContract.Text = $"{m_Tick.ProductInfo.Commodity},{m_Tick.ProductInfo.Date}";
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

        
    }
}
