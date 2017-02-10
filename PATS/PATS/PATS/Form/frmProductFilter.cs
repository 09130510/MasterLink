using PATS.Utility;
using PriceLib.PATS;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace PATS
{
    public partial class frmProductFilter : Form
    {
        public frmProductFilter()
        {
            InitializeComponent();
            LoadConfig();

            Center.Instance.AddObserver(OnPATSChange, Observer.PATS);
            OnPATSChange(Observer.PATS, new Msg(null, nameof(Util.ProductInfos)));
            //LoadProduct();


        }
        private void frmProductFilter_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void frmProductFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void tvAll_AfterSelect(object sender, TreeViewEventArgs e)
        {
            btnRemove.Enabled = btnUp.Enabled = btnDown.Enabled = false;

            if (e.Node.Level == 0)
            {
                txtExch.Text = e.Node.Text;
                txtContract.Text = txtDate.Text = string.Empty;
            }
            if (e.Node.Level == 1)
            {
                txtExch.Text = e.Node.Parent.Text;
                txtContract.Text = e.Node.Text;
                txtDate.Text = string.Empty;
            }
            if (e.Node.Level == 2)
            {
                txtExch.Text = e.Node.Parent.Parent.Text;
                txtContract.Text = e.Node.Parent.Text;
                txtDate.Text = e.Node.Text;
            }
        }
        private void tvAll_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
        }
        private void lstFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = btnUp.Enabled = btnDown.Enabled = true;

            if (lstFilter.Items.Count <= 0|| lstFilter.SelectedIndex <0) { return; }
            string[] items = lstFilter.Items[lstFilter.SelectedIndex].ToString().Split(',');
            for (int i = 0; i < items.Length; i++)
            {
                if (i == 0) { txtExch.Text = items[i]; }
                if (i == 1) { txtContract.Text = items[i]; }
                if (i == 2) { txtDate.Text = items[i]; }
            }            
        }
        private void lstFilter_Validated(object sender, EventArgs e)
        {

        }
        private void lstFilter_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnRemove_Click(btnRemove, EventArgs.Empty);
            }
        }
        private void btnReload_Click(object sender, EventArgs e) { LoadProduct(); }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Util.ProductInfos == null) { return; }
            foreach (TreeNode exch in tvAll.Nodes)
            {
                foreach (TreeNode contract in exch.Nodes)
                {
                    foreach (TreeNode date in contract.Nodes)
                    {
                        if (date.Checked && date.Tag != null)
                        {
                            date.Checked = false;
                            ContractStruct c = (ContractStruct)date.Tag;
                            if (!lstFilter.Items.Contains(c.Key))
                            {
                                lstFilter.Items.Add(c.Key);
                            }
                        }
                    }
                    contract.Checked = false;
                }
                exch.Checked = false;
            }
            SaveConfig();
        }
        private void btnAddContract_Click(object sender, EventArgs e)
        {
            string key = $"{txtExch.Text.Trim().ToUpper()},{txtContract.Text.Trim().ToUpper()},{txtDate.Text.ToUpper()}";
            if (!lstFilter.Items.Contains(key))
            {
                lstFilter.Items.Add(key);
            }
            SaveConfig();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            for (int i = lstFilter.SelectedItems.Count - 1; i >= 0; i--)
            {
                lstFilter.Items.Remove(lstFilter.SelectedItems[i]);
            }
            if (lstFilter.Items.Count > 0) { lstFilter.SelectedIndex = 0; }
            SaveConfig();
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            List<int> tobeSelect = new List<int>();
            for (int i = 0; i < lstFilter.Items.Count; i++)
            {
                if (lstFilter.SelectedIndices == null || lstFilter.SelectedIndices.Count <= 0) { break; }
                if (i == lstFilter.SelectedIndices[0] && i - 1 >= 0)
                {
                    lstFilter.SetSelected(i, false);
                    var temp = lstFilter.Items[i - 1];
                    lstFilter.Items[i - 1] = lstFilter.Items[i];
                    lstFilter.Items[i] = temp;
                    tobeSelect.Add(i - 1);
                }
            }
            foreach (var idx in tobeSelect)
            {
                lstFilter.SetSelected(idx, true);
            }
            SaveConfig();

        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            List<int> tobeSelect = new List<int>();
            for (int i = lstFilter.Items.Count - 1; i >= 0; i--)
            {
                if (lstFilter.SelectedIndices == null || lstFilter.SelectedIndices.Count <= 0) { break; }
                if (i == lstFilter.SelectedIndices[lstFilter.SelectedIndices.Count - 1] && (i + 1) < lstFilter.Items.Count)
                {
                    lstFilter.SetSelected(i, false);
                    var temp = lstFilter.Items[i + 1];
                    lstFilter.Items[i + 1] = lstFilter.Items[i];
                    lstFilter.Items[i] = temp;
                    tobeSelect.Add(i + 1);
                }
            }
            foreach (var idx in tobeSelect)
            {
                lstFilter.SetSelected(idx, true);
            }
            SaveConfig();
        }
        private void txtExch_TextChanged(object sender, EventArgs e)
        {
            btnAddContract.Enabled = !string.IsNullOrEmpty(txtExch.Text) && !string.IsNullOrEmpty(txtContract.Text) && !string.IsNullOrEmpty(txtDate.Text);
        }

        #region Delegate
        private void OnPATSChange(Observer MsgName, Msg Msg)
        {
            if (Msg.Message.ToString() == nameof(Util.ProductInfos) && Util.ProductInfos!= null)
            {
                LoadProduct();
            }            
        }
        #endregion

        #region Private
        private void LoadProduct()
        {
            this.BeginInvokeIfRequired(() =>
            {
                tvAll.Nodes.Clear();
                var products = Util.PATS.Products();
                if (products == null) { return; }
                foreach (var info in products.Values)
                {
                    if (!tvAll.Nodes.ContainsKey(info.ExchangeName))
                    {
                        tvAll.Nodes.Add(new TreeNode(info.ExchangeName) { Name = info.ExchangeName });
                    }
                    TreeNode exch = tvAll.Nodes[info.ExchangeName];

                    if (!exch.Nodes.ContainsKey(info.ContractName))
                    {
                        exch.Nodes.Add(new TreeNode(info.ContractName) { Name = info.ContractName });
                    }
                    TreeNode contract = exch.Nodes[info.ContractName];

                    if (!contract.Nodes.ContainsKey(info.ContractDate))
                    {
                        contract.Nodes.Add(new TreeNode(info.ContractDate) { Name = info.ContractDate, Tag = info });
                    }
                }
                btnAdd.Enabled = tvAll.Nodes.Count > 0;
            });
        }
        private void LoadConfig()
        {
            string[] filters = Util.INI["SYS"]["FILTER"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var filter in filters)
            {
                lstFilter.Items.Add(filter);
            }
        }
        private void SaveConfig()
        {
            ThreadPool.QueueUserWorkItem((e) =>
            {
                string re = string.Empty;
                foreach (var item in lstFilter.Items)
                {
                    re += item.ToString() + ";";
                }
                Util.INI["SYS"]["FILTER"] = re;
                Util.SaveConfig();
                if (Util.ProductInfos != null)
                {
                    Util.ReloadProductInfo();
                }
            });
        }
        #endregion
    }
}