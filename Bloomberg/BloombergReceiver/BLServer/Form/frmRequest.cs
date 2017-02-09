using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using BLPServer.Class;

namespace BLPServer
{
    public partial class frmRequest : DockContent
    {
        public frmRequest()
        {
            InitializeComponent();
        }

        private void btnAddSecurity_Click(object sender, EventArgs e)
        {
            TextBox ITEMS = sender == btnAddSecurity ? txtSecurities : txtFields;
            string addITEM = sender == btnAddSecurity ? txtSecurity.Text : txtField.Text;
            if (String.IsNullOrEmpty(addITEM)) { return; }
            string[] items = ITEMS.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Contains(addITEM)) { return; }
            ITEMS.AppendText(addITEM + "\r\n");
        }
        private void btnRemoveSecurity_Click(object sender, EventArgs e)
        {
            TextBox ITEMS = sender == btnRemoveSecurity ? txtSecurities : txtFields;
            string removeITEM = sender == btnRemoveSecurity ? txtSecurity.Text : txtField.Text;
            if (String.IsNullOrEmpty(removeITEM)) { return; }
            string[] items = ITEMS.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (!items.Contains(removeITEM)) { return; }
            ITEMS.Text = string.Empty;
            foreach (var item in items)
            {
                if (!String.IsNullOrEmpty(item) && item != removeITEM)
                {
                    ITEMS.AppendText(item + "\r\n");
                }
            }
        }
        private void btnRequest_Click(object sender, EventArgs e)
        {
            if (Utility.Subscriber == null) { return; }
            SendRequest(txtSecurities.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList(), txtFields.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList());
        }
        private void txtSecurities_MouseDown(object sender, MouseEventArgs e)
        {            
            TextBox txtDestination = sender == txtFields ? txtField : txtSecurity;
            TextBox t = (TextBox)sender;
            if (String.IsNullOrEmpty(t.Text)) { return; }
            txtDestination.Text = t.Lines[t.GetLineFromCharIndex(t.GetFirstCharIndexOfCurrentLine())];
        }

        #region Public
        public void SendRequest(List<string> Securities, List<string> Fields)
        {
            Utility.Subscriber.SendRequest(DateTime.Now.ToString("HHmmssffff"), Securities, Fields);
        }
        public void SendRequest(string Security, string Field)
        {
            Utility.Subscriber.SendRequest(DateTime.Now.ToString("HHmmssffff"), new List<string>() { Security }, new List<string>() { Field });
        }
        #endregion
        
    }
}
