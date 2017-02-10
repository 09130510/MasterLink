using Capital.Report.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Capital.Report
{
    public partial class frmProductFilter : Form
    {
        public frmProductFilter()
        {
            InitializeComponent();
            LoadProduct();
        }
        private void frmProductFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) { Close(); }
        }
        
        private void btnReload_Click(object sender, EventArgs e)
        {
            var products = Core.Instance.ProductFilters();
            if (products == null) { return; }
            foreach (var item in products)
            {
                tvAll.Nodes.Add(item);
            }
        }
        private void btnAddContract_Click(object sender, EventArgs e)
        {
            string value = $"{txtExch.Text},{txtContract.Text}";
            if (!lstFilter.Items.Contains(value)) { lstFilter.Items.Add(value); }
            txtExch.Text = txtContract.Text = string.Empty;
            Utility.SaveConfig(lstFilter);
        }        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in tvAll.Nodes)
            {
                if (node.Checked && !lstFilter.Items.Contains(node.Text))
                {
                    lstFilter.Items.Add(node.Text);
                }
                node.Checked = false;
            }
            Utility.SaveConfig(lstFilter);
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            for (int i = lstFilter.SelectedItems.Count - 1; i >= 0; i--)
            {
                lstFilter.Items.Remove(lstFilter.SelectedItems[i]);
            }
            if (lstFilter.Items.Count > 0) { lstFilter.SelectedIndex = 0; }
            Utility.SaveConfig(lstFilter);
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            List<int> tobeSelect = new List<int>();
            for (int i = 0; i < lstFilter.Items.Count; i++)
            {
                if (lstFilter.SelectedIndices == null || lstFilter.SelectedIndices.Count <= 0) { break; }
                if (i == lstFilter.SelectedIndices[0] && ((i - 1) >= 0))
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
            Utility.SaveConfig(lstFilter);
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
            Utility.SaveConfig(lstFilter);
        }
        private void lstFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemove.Enabled = btnUp.Enabled = btnDown.Enabled = true;
            if (lstFilter.Items.Count <= 0 || lstFilter.SelectedIndex < 0) { return; }
            string[] items = lstFilter.Items[lstFilter.SelectedIndex].ToString().Split(',');
            for (int i = 0; i < items.Length; i++)
            {
                if (i == 0) { txtExch.Text = items[i]; }
                if (i == 1) { txtContract.Text = items[i]; }                
            }
        }
        private void lstFilter_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                btnRemove_Click(btnRemove, EventArgs.Empty);
            }
        }
        private void txtExch_TextChanged(object sender, EventArgs e)
        {
            btnAddContract.Enabled = !string.IsNullOrEmpty(txtExch.Text) && !string.IsNullOrEmpty(txtContract.Text);
        }

        private void LoadProduct()
        {
            tvAll.Nodes.Clear();
            Utility.LoadConfig(lstFilter);
            btnReload_Click(btnReload, EventArgs.Empty);

            btnAdd.Enabled = tvAll.Nodes.Count > 0;
        }
    }
}
