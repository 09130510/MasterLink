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
    public partial class frmAllow : Form
    {
        private string m_Identity;
        private Dictionary<string, List<string>> m_Allow;

        public frmAllow(string identity, Dictionary<string, List<string>> allow)
        {
            InitializeComponent();

            m_Identity = identity;
            m_Allow = allow;
            tsDisplayAdd_Click(tsDisplayAdd, EventArgs.Empty);
        }
        private void frmAllow_Load(object sender, EventArgs e)
        {
            ResetItem();
        }
        private void frmAllow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
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
            //if (string.IsNullOrEmpty(tsChannel.Text) || string.IsNullOrEmpty(tsItem.Text))
            //{
            //    return;
            //}
            if (string.IsNullOrEmpty(tsChannel.Text) )
            {
                return;
            }
            if (!m_Allow.ContainsKey(tsChannel.Text))
            {
                m_Allow.Add(tsChannel.Text, new List<string>());
            }
            if (!string.IsNullOrEmpty(tsItem.Text) &&!m_Allow[tsChannel.Text].Contains(tsItem.Text) )
            {
                m_Allow[tsChannel.Text].Add(tsItem.Text);
            }
            ResetItem();
            tsChannel.Text = tsItem.Text = string.Empty;
            _SaveAllow();
        }
        private void lstChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstChannel.SelectedItems.Count > 0)
            {
                lstItem.Items.Clear();
                foreach (var item in lstChannel.SelectedItems)
                {
                    lblChannel.Text = item.ToString();
                    if (m_Allow.ContainsKey(item.ToString()))
                    {
                        foreach (var sub in m_Allow[item.ToString()])
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
                for (int i = m_Allow.Count - 1; i >= 0; i--)
                {
                    if (lstChannel.SelectedIndices.Contains(i))
                    {
                        m_Allow.Remove(m_Allow.Keys.ElementAt(i));
                    }
                }
                ResetItem();
                _SaveAllow();
            }
        }
        private void lstItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lstItem.SelectedItems.Count > 0)
            {
                for (int i = m_Allow.Count - 1; i >= 0; i--)
                {
                    var channel = m_Allow.ElementAt(i);
                    for (int j = channel.Value.Count - 1; j >= 0; j--)
                    {
                        string item = $"{channel.Key}-{channel.Value.ElementAt(j)}";
                        if (lstItem.SelectedItems.Contains(item))
                        {
                            channel.Value.RemoveAt(j);
                        }
                        //if (channel.Value.Count == 0)
                        //{
                        //    m_Allow.Remove(channel.Key);
                        //}
                    }
                }

                _SaveAllow();
                ResetItem();
            }
        }        

        public void ResetItem()
        {
            lstChannel.Items.Clear();
            lstChannel.Items.AddRange(m_Allow.Keys.ToArray());
            _ShowAllItem();
        }

        private void _SaveAllow()
        {
            if (!frmMain.INI.Sections.ContainsSection(m_Identity)) { return; }
            frmMain.INI[m_Identity]["ALLOW"] = m_Allow.SectionString();
            var parser = new FileIniDataParser();
            parser.WriteFile("Config.ini", frmMain.INI);
        }
        private void _ShowAllItem()
        {
            lblChannel.Text = "All";
            lstItem.Items.Clear();

            foreach (var v in m_Allow)
            {
                foreach (var item in v.Value)
                {
                    lstItem.Items.Add($"{v.Key}-{item}");
                }
            }
        }
    }
}