using System;
using System.Linq;
using System.Windows.Forms;
using BLPServer.Class;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;

namespace BLPServer
{
    public partial class frmSubscribe : DockContent
    {
        public frmSubscribe()
        {
            InitializeComponent();

            Utility.LoadConfig(this);
        }

        private void txtSecurities_Validated(object sender, EventArgs e)
        {
            Utility.SaveConfig((Control)sender);
        }
        private void txtFields_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox txtDestination = sender == txtFields ? txtField : txtSecurity;
            TextBox t = (TextBox)sender;
            if (String.IsNullOrEmpty(t.Text)) { return; }
            txtDestination.Text = t.Lines[t.GetLineFromCharIndex(t.GetFirstCharIndexOfCurrentLine())];
        }
        private void btnAddSecurity_Click(object sender, EventArgs e)
        {
            Subscribe(sender == btnAddSecurity ? "SECURITY" : "FIELDS", sender == btnAddSecurity ? txtSecurity.Text : txtField.Text);
            //TextBox ITEMS = sender == btnAddSecurity ? txtSecurities : txtFields;
            //string addITEM = sender == btnAddSecurity ? txtSecurity.Text : txtField.Text;
            //if (String.IsNullOrEmpty(addITEM)) { return; }
            //string[] items = ITEMS.Text.Split(new string[]{"\r\n"},  StringSplitOptions.RemoveEmptyEntries );
            //if (items.Contains(addITEM)) { return; }
            //ITEMS.AppendText(addITEM + "\r\n");
            //_ResetDisplay();   
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
                if (!String.IsNullOrEmpty(item))
                {
                    if (item != removeITEM)
                    {
                        ITEMS.AppendText(item + "\r\n");
                    }
                    else if (sender == btnRemoveSecurity)
                    {
                        _Unsub(item);
                    }
                }
            }
            _ReSubAdnResetDisplay();
        }
        private void btnSubscription_Click(object sender, EventArgs e)
        {
            DoSubscribe();
        }
        private void btnUnsubscription_Click(object sender, EventArgs e)
        {
            if (Utility.Subscriber == null) { return; }
            Utility.Subscriber.SendUnsubscription(txtSecurities.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList());
        }

        #region Public
        public void Subscribe(string Head, string Msg, bool isReSub = false)
        {
            TextBox ITEMS = Head == "SECURITY" ? txtSecurities : txtFields;
            string addITEM = Msg;
            if (String.IsNullOrEmpty(addITEM)) { return; }
            string[] items = ITEMS.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (items.Contains(addITEM) && !isReSub) { return; }

            this.InvokeIfRequired(() => {
                if (!items.Contains(addITEM)) { ITEMS.AppendText(addITEM + "\r\n"); }
                _ReSubAdnResetDisplay();
            });            
        }
        public void DoSubscribe()
        {
            if (Utility.Subscriber == null) { return; }
            Utility.Subscriber.SendSubscription(txtSecurities.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList(), txtFields.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList());
        }
        #endregion

        #region Private
        private void _Unsub(string unsub)
        {
            if (Utility.Subscriber == null) { return; }
            List<string> list = new List<string>();
            list.Add(unsub);
            Utility.Subscriber.SendUnsubscription(list);
        }
        private void _ReSubAdnResetDisplay()
        {
            if (Utility.Subscriber != null)
            {
                btnUnsubscription_Click(btnUnsubscription, EventArgs.Empty);
                if (Utility.Display != null) { Utility.Display.Dispose(); }
                Utility.Display = new Display();
                Utility.SubResultForm.SetCell();
                //Utility.SDataForm.SetCell();
                btnSubscription_Click(btnSubscription, EventArgs.Empty);
            }
        }
        #endregion

    }
}
