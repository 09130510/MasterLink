using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util.Extension.Class;
using PCF.Class;
using Util.Extension;
using System.Threading;

namespace PCF
{
    public partial class frmMain : Form
    {
        const string JSONVIEWER = "http://jsonviewer.stack.hu/#";
        #region Variable
        private DataTable m_Table;
        private Dictionary<string, ETF> m_ETFs = new Dictionary<string, ETF>();
        #endregion

        public frmMain()
        {
            InitializeComponent();
            this.Text = Utility.VersionInfo(this);


            Utility.Init(this.panel1.Controls.Cast<Control>().Where(e => e is CheckBox).Cast<CheckBox>().OrderBy(e => e.TabIndex).ToArray());
            lblIP.Text = Utility.INI["SQL"]["IP"];
            if (Utility.SQL == null || Utility.SQL.Length < 1)
            {
                MessageBox.Show("No SQL Setting!");
                return;
            }

            m_Table = Utility.SQL[0].DoQuery("SELECT * FROM tblETF ");
            var V = from row in m_Table.AsEnumerable()
                    select new
                    {
                        ETFCode = row.Field<string>("ETFCODE"),
                        Name = row.Field<string>("NAME"),
                        Market = row.Field<string>("MARKET").ToEnum<Market>(),
                        ETFType = row.Field<string>("ETFTYPE").ToEnum<ETFType>(),
                        Broker = row.Field<string>("BROKER").ToEnum<Broker>(),
                        Address = row.Field<string>("ADDRESS"),
                        SettingItem = row.Field<string>("SETTINGITEM"),
                        YstFutPrice = row.Field<bool>("YSTFUTPRICE")
                    };
            foreach (var item in V)
            {
                ETF etf = null;
                switch (item.Broker)
                {
                    case Broker.YT:
                        if (chkYT.Checked) { etf = new YT(); }
                        break;
                    case Broker.FB:
                        if (chkFB.Checked) { etf = new FB(); }
                        break;
                    case Broker.CH:
                        if (chkCH.Checked) { etf = new CH(); }
                        break;
                    case Broker.FH:
                        if (chkFH.Checked) { etf = new FH(); }
                        break;
                    case Broker.CP:
                        if (chkCP.Checked) { etf = new CP(); }
                        break;
                    case Broker.SP:
                        if (chkSP.Checked) { etf = new SP(); }
                        break;
                }
                if (etf != null)
                {
                    etf.SetValue(item.ETFCode, item.Name, item.Market, item.ETFType, item.Broker, item.Address, item.SettingItem, item.YstFutPrice);
                    m_ETFs.Add(item.ETFCode, etf);
                }
            }
            foreach (var etf in m_ETFs)
            {
                tvETF.Nodes.Add(etf.Value.Node);
                etf.Value.DoParse();
            }
        }

        private void tvETF_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                tabControl1.SelectedIndex = 0;
                //lstMsg.Items.Clear();
                //lstMsg.Items.AddRange(((ETF)e.Node.Tag).ProcessMsg.ToArray());
                txtMsg.Text = string.Empty;
                txtMsg.AppendText(string.Join(System.Environment.NewLine, ((ETF)e.Node.Tag).ProcessMsg.ToArray()));

            }
            //textBox1.Font = new Font("Verdana", 9);
        }

        private void tvETF_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode p = e.Node.Parent;
            TreeNode s = tvETF.SelectedNode;
            if ((e.Node == s || (p != null && (s == p || p.Nodes.Contains(s)))) &&
                e.Node.Tag != null)
            {
                //lstMsg.Items.Clear();
                //lstMsg.Items.AddRange(((ETF)e.Node.Tag).ProcessMsg.ToArray());
                //richTextBox1.Clear();
                txtMsg.Text = string.Empty;
                txtMsg.AppendText(string.Join(System.Environment.NewLine, ((ETF)e.Node.Tag).ProcessMsg.ToArray()));
                //richTextBox1.Font = new Font("Verdana", 9);
            }
        }

        private void tvETF_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right) { return; }
            tsParse.Tag = ((ETF)e.Node.Tag).ETFCode;
            tsParse.Text = "重抓一次:" + dateTimePicker1.Value.ToString(ETF.DATE);
            e.Node.ContextMenuStrip = contextMenuStrip1;
        }

        private void tsParse_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Tag == null) { return; }
            m_ETFs[((ToolStripMenuItem)sender).Tag.ToString()].DoParse(dateTimePicker1.Value);
        }

        private void btnJsonBrowse_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(JSONVIEWER + txtJsonAddress.Text);
            webBrowser1.Refresh();
        }

        private void txtJsonAddress_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnJsonBrowse_Click(btnJsonBrowse, EventArgs.Empty);
            }
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            webBrowser2.Navigate( txtAddress.Text);
            webBrowser2.Refresh();
        }

        private void txtAddress_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBrowser_Click(btnBrowser, EventArgs.Empty);
            }
        }
    }
}