using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTPTandem.Class;
using System.Diagnostics;

namespace FTPTandem.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = string.Format("{0}-{1}-FTP Tandem-2015/11/27", Application.ProductName, System.Diagnostics.Process.GetCurrentProcess().Id);
            MainClass.mainClass.srnItems.lbLogs = lbLogs;
            MainClass.mainClass.srnItems.dtTransDate = dtTransDate;
            MainClass.mainClass.srnItems.txtEmails = txtEmails;
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = Properties.Settings.Default.Url +  Properties.Settings.Default.DownloadPage;
            lnkURL.Links.Add(link);
            dtTransDate.Value = DateTime.Today;
            InitScreen();
            butExecute_Click(butExecute, EventArgs.Empty);
        }

        private void InitScreen()
        {
            string[] items = new string[Properties.Settings.Default.EmailTo.Count];
            Properties.Settings.Default.EmailTo.CopyTo(items, 0);
            for (int i = 0; i < items.Length; ++i)
            {
                if (items[i] != string.Empty)
                {
                    txtEmails.AppendText(string.Format("{0}\n",items[i]));
                }
            }
        }
        private void butExecute_Click(object sender, EventArgs e)
        {            
            MainClass.mainClass.StartDownload();
            //DateTime date = dtTransDate.Value;
            //MainClass.mainClass.InitDataBase(txtSQLServer.Text, txtSQLDB.Text, txtSQLID.Text, txtSQLPassword.Text, date);
            //MainClass.mainClass.StartFTP();
            //string[] emails =  txtEmails.Lines.ToArray();
            //MainClass.mainClass.SendEmail(date, emails);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void txtEmails_Validated(object sender, EventArgs e)
        {
            Properties.Settings.Default.EmailTo.Clear();
            string[] emails = txtEmails.Lines.ToArray();
            Properties.Settings.Default.EmailTo.AddRange(emails);
        }

        private void lnkURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void butFTP_Click(object sender, EventArgs e)
        {
            DateTime date = dtTransDate.Value;
            MainClass.mainClass.InitDataBase(txtSQLServer.Text, txtSQLDB.Text, txtSQLID.Text, txtSQLPassword.Text, date);
            //MainClass.mainClass.StartFTP();
            //string[] emails = txtEmails.Lines.ToArray();
            //MainClass.mainClass.SendEmail(date, emails);
        }
    }
}
