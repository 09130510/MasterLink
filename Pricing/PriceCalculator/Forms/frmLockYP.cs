using PriceCalculator.Component;
using PriceCalculator.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PriceCalculator
{
    public partial class frmLockYP : Form
    {
        private ETF m_ETF;

        public frmLockYP(ETF etf)
        {
            InitializeComponent();
            m_ETF = etf;
            Text = $"{etf.ETFCode} Lock Price";
            dtpExpired.MinDate = dtpExpired.Value = DateTime.Now.AddDays(1);
            lblCount.Text = Util.SQL.Query<int>($"SELECT COUNT(*) FROM ETFForBrian..tblFixYstPrice WHERE ETFCode='{m_ETF.ETFCode}' ").First().ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (AlertBox.Alert(this, AlertBoxButton.OKCancel, "Clear", $"是否清除 [{m_ETF.ETFCode}] 已鎖定資料共 - { lblCount.Text}筆？"))
            {
                Util.SQL.DoExecute($"DELETE FROM ETFForBrian..tblFixYstPrice WHERE ETFCode='{m_ETF.ETFCode}' ");
                lblCount.Text = Util.SQL.Query<int>($"SELECT COUNT(*) FROM ETFForBrian..tblFixYstPrice WHERE ETFCode='{m_ETF.ETFCode}' ", null).First().ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (AlertBox.Alert(this, AlertBoxButton.OKCancel, "Lock Price", "需要重開程式後價格鎖定才會生效\r\n\r\n是否確定鎖定？"))
            {
                m_ETF.LockYP(dtpExpired.Value);
                DialogResult = DialogResult.OK;
            }

        }
    }
}
