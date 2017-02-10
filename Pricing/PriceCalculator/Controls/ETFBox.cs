using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PriceCalculator.Utility;
using log4net;
using System.Reflection;
using PriceCalculator.Component;

namespace PriceCalculator
{
    public partial class ETFBox : UserControl
    {
        private ILog m_Log = LogManager.GetLogger(typeof(ETFBox));
        private List<string> m_CheckedNodes = new List<string>();

        private struct ETFInfo
        {
            public string ETFCode { get; set; }
            public string Name { get; set; }
            public Market Market { get; set; }
            public Broker Broker { get; set; }
            public bool Lock { get; set; }
        }
        public event TreeViewEventHandler AfterChecked;
        public event TreeViewEventHandler AfterSelected;


        public List<string> CheckedNodes { get { return m_CheckedNodes; } }
        public string Classification
        {
            get { return cboClassification.Text; }
            set
            {
                if (value != cboClassification.Text)
                {
                    cboClassification.Text = value;
                }
            }
        }
        public string SelectedETFCode
        {
            get
            {
                if ((tvETF.SelectedNode != null) && (tvETF.SelectedNode.Level != 0))
                {
                    return tvETF.SelectedNode.Name;
                }
                return string.Empty;
            }

        }

        public ETFBox()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                cboClassification.SelectedIndex = 0;
            }
        }

        private void cboClassification_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Init(cboClassification.Text);
            tvETF.ExpandAll();
        }
        private void tvETF_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    if (node.Checked!= e.Node.Checked)
                    {
                        node.Checked = e.Node.Checked;
                    }                    
                }
            }
            if (e.Node.Level != 0)
            {
                if (e.Node.Checked)
                {
                    if (!m_CheckedNodes.Contains(e.Node.Name))
                    {
                        m_CheckedNodes.Add(e.Node.Name);
                    }
                }
                else
                {
                    if (m_CheckedNodes.Contains(e.Node.Name))
                    {
                        m_CheckedNodes.Remove(e.Node.Name);
                    }
                }
            }

            AfterChecked?.Invoke(sender, e);
        }
        private void tvETF_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelected?.Invoke(this, e);
        }
        private void tvETF_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tvETF.ContextMenuStrip = e.Node.Level == 0 ? null : cmMenu;
            cmMenu.Tag = e.Node.Name;
        }
        private void tsNAVDetail_Click(object sender, EventArgs e)
        {
            Detail(cmMenu.Tag.ToString());
        }
        private void tsLockYP_Click(object sender, EventArgs e)
        {
            LockYP(cmMenu.Tag.ToString());
        }

        #region Private
        private void _Init(string classification)
        {
            Util.Info(m_Log, nameof(_Init), $"ETFBox Classification:{classification}");
            tvETF.Nodes.Clear();
            if (Util.SQL == null) { return; }
            IEnumerable<ETFInfo> etfs = Util.SQL.Query<ETFInfo>("SELECT E.*, CASE WHEN L.ETFCODE IS NULL THEN 0 ELSE 1 END AS LOCK FROM tblETF E LEFT JOIN (select DISTINCT ETFCODE  FROM tblFixYstPrice WHERE Convert(varchar(20),ExpiredDate,112) >= Convert(varchar(20),GETDATE(),112)) L ON E.ETFCode = L.ETFCode ");

            //var etfs = from row in dt.AsEnumerable()
            //           select new ETFInfo
            //           {
            //               ETFCode = row.Field<string>("ETFCODE"),
            //               Name = row.Field<string>("NAME").Trim(),
            //               Market = row.Field<string>("MARKET").ToEnum<Market>(),
            //               Broker = row.Field<string>("BROKER").ToEnum<Broker>()
            //           };
            //string[] calc = Util.INI["SYS"]["CALCETF"].Split(';');
            switch (classification)
            {
                case "Broker":
                    var brokers = etfs.GroupBy(e => e.Broker);
                    foreach (var broker in brokers)
                    {
                        TreeNode brokerNode = new TreeNode(broker.Key.ToString());
                        tvETF.Nodes.Add(brokerNode);
                        _BuildTree(brokerNode, broker);
                    }
                    break;
                case "Market":
                    var markets = etfs.GroupBy(e => e.Market);
                    foreach (var market in markets)
                    {
                        TreeNode node = new TreeNode(market.Key.ToString());
                        tvETF.Nodes.Add(node);
                        _BuildTree(node, market);
                    }
                    break;
                default:
                    TreeNode root = new TreeNode("All");
                    tvETF.Nodes.Add(root);
                    _BuildTree(root, etfs);
                    break;
            }

        }
        private void _BuildTree(TreeNode parents, IEnumerable<ETFInfo> etfs)
        {
            foreach (var info in etfs)
            {
                TreeNode node = new TreeNode(string.Format("[{0}] {1}", info.ETFCode, info.Name, info.Broker))
                {
                    Name = info.ETFCode,
                    ImageKey = info.Lock ? "lock.ico" : string.Empty,
                    SelectedImageKey = info.Lock ? "lock.ico" : string.Empty
                };
                node.Checked = m_CheckedNodes.Contains(node.Name);
                parents.Nodes.Add(node);
            }
        }
        #endregion

        #region Public
        public void Add(string etfcode)
        {
            tvETF.InvokeIfRequired(() =>
            {
                TreeNode[] etfs = tvETF.Nodes.Find(etfcode, true);
                if (etfs.Length > 0 && !etfs[0].Checked)
                {
                    etfs[0].Checked = true;
                }
            });
        }
        public void Remove(string etfcode)
        {
            TreeNode[] etfs = tvETF.Nodes.Find(etfcode, true);
            if (etfs.Length > 0 && etfs[0].Checked)
            {
                etfs[0].Checked = false;
            }
        }
        public void Detail(string etfcode)
        {
            ETF etf = Util.Calculator.GetETF(etfcode);
            if (etf != null)
            {
                new frmNAVDetail(etf).Show();
            }
        }
        public void LockYP(string etfcode)
        {
            ETF etf = Util.Calculator.GetETF(etfcode);
            if (etf == null) { return; }

            //int num = Util.SQL.Query<int>($"SELECT COUNT(*) FROM ETFForBrian..tblFixYstPrice WHERE ETFCode='{etf.ETFCode}' ", null).First<int>();
            //int num2 = Util.SQL.Query<int>($"SELECT COUNT(*) FROM ETFForBrian..tblFixYstPrice WHERE ETFCode='{etf.ETFCode}' ", null).First<int>();
            //if ((new frmLockYP(etf).ShowDialog() == DialogResult.OK) || (num != num2))
            if ((new frmLockYP(etf).ShowDialog() == DialogResult.OK))
            {
                TreeNode node1 = tvETF.Nodes[0].Nodes[cmMenu.Tag.ToString()];
                node1.ImageKey = node1.SelectedImageKey = "lock_gray2.ico";
            }
        }
        public void Select(string etfcode)
        {
            switch (Classification)
            {
                case "None":
                    if (tvETF.Nodes[0].Nodes.ContainsKey(etfcode))
                    {
                        tvETF.SelectedNode = tvETF.Nodes[0].Nodes[etfcode];
                    }
                    break;
                case "Broker":
                case "Market":
                    foreach (TreeNode node in tvETF.Nodes)
                    {
                        if (node.Nodes.ContainsKey(etfcode))
                        {
                            tvETF.SelectedNode = node.Nodes[etfcode];
                            break;
                        }
                    }
                    break;
            }
        }
        #endregion
    }
}