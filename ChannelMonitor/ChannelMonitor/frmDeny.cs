using ChannelMonitor.Class;
using ChannelMonitor.Properties;
using IniParser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChannelMonitor
{
    public partial class frmDeny : Form
    {
        private string m_Identity;
        private Dictionary<string, List<string>> m_Deny;

        public frmDeny(string identity, Dictionary<string, List<string>> deny)
        {
            InitializeComponent();

            m_Identity = identity;
            m_Deny = deny;
        }
        private void frmDeny_Load(object sender, EventArgs e)
        {
            ResetItem();
        }
        private void frmDeny_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void lstChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstChannel.SelectedItems.Count > 0)
            {
                lstItem.Items.Clear();
                foreach (var item in lstChannel.SelectedItems)
                {
                    lblChannel.Text = item.ToString();
                    if (m_Deny.ContainsKey(item.ToString()))
                    {
                        foreach (var sub in m_Deny[item.ToString()])
                        {
                            lstItem.Items.Add($"{item}-{sub}");
                        }
                    }
                }
                if (lstChannel.SelectedItems.Count > 1)
                {
                    lblChannel.Text = "Multi";
                }
            }
            else
            {
                _ShowAllItem();
            }
        }
        private void lstChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lstChannel.SelectedItems.Count > 0)
            {
                for (int i = m_Deny.Count - 1; i >= 0; i--)
                {
                    if (lstChannel.SelectedIndices.Contains(i))
                    {
                        m_Deny.Remove(m_Deny.Keys.ElementAt(i));
                    }
                }
                ResetItem();
                _SaveDeny();
            }
        }
        private void lstItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lstItem.SelectedItems.Count > 0)
            {
                //foreach (var channel in m_Deny)
                //{
                for (int i = m_Deny.Count - 1; i >= 0; i--)
                {
                    var channel = m_Deny.ElementAt(i);
                    for (int j = channel.Value.Count - 1; j >= 0; j--)
                    {
                        string item = $"{channel.Key}-{channel.Value.ElementAt(j)}";
                        if (lstItem.SelectedItems.Contains(item))
                        {
                            channel.Value.RemoveAt(j);
                        }
                        if (channel.Value.Count == 0)
                        {
                            m_Deny.Remove(channel.Key);
                        }
                    }
                }
                //}
                _SaveDeny();
                ResetItem();
            }
        }
        private void tsDisplayAdd_Click(object sender, EventArgs e)
        {
            if (tsDisplayAdd.Text == nameof(frmMain.RIGHT))
            {
                tsSeparator.Visible = tsChannelLabel.Visible = tsChannel.Visible = tsItemLabel.Visible = tsItem.Visible = tsAdd.Visible = true;
                tsDisplayAdd.Image = Resources._1477983381_BT_arrow_left;
                tsDisplayAdd.Text = nameof(frmMain.LEFT);
            }
            else if (tsDisplayAdd.Text == nameof(frmMain.LEFT))
            {
                tsSeparator.Visible = tsChannelLabel.Visible = tsChannel.Visible = tsItemLabel.Visible = tsItem.Visible = tsAdd.Visible = false;
                tsDisplayAdd.Image = Resources._1477983371_BT_arrow_right;
                tsDisplayAdd.Text = nameof(frmMain.RIGHT);
            }
        }
        private void tsAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tsChannel.Text) || string.IsNullOrEmpty(tsItem.Text))
            {
                return;
            }
            if (!m_Deny.ContainsKey(tsChannel.Text))
            {
                m_Deny.Add(tsChannel.Text, new List<string>());
            }
            if (!m_Deny[tsChannel.Text].Contains(tsItem.Text))
            {
                m_Deny[tsChannel.Text].Add(tsItem.Text);
            }
            ResetItem();
            tsChannel.Text = tsItem.Text = string.Empty;
            _SaveDeny();
        }

        public void ResetItem()
        {
            lstChannel.Items.Clear();
            lstChannel.Items.AddRange(m_Deny.Keys.ToArray());
            _ShowAllItem();
        }
        private void _SaveDeny()
        {
            if (!frmMain.INI.Sections.ContainsSection(m_Identity)) { return; }
            frmMain.INI[m_Identity]["DENY"] = m_Deny.SectionString();
            var parser = new FileIniDataParser();
            parser.WriteFile("Config.ini", frmMain.INI);
        }
        private void _ShowAllItem()
        {
            lblChannel.Text = "All";
            lstItem.Items.Clear();

            foreach (var v in m_Deny)
            {
                foreach (var item in v.Value)
                { 
                    lstItem.Items.Add($"{v.Key}-{item}");
                }
            }
        }
    }
}