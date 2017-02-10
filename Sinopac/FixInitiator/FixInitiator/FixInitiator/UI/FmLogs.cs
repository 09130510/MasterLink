using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FixInitiator.Class;

namespace FixInitiator.UI
{
    public partial class FmLogs : Form
    {
        public FmLogs()
        {
            InitializeComponent();
        }

        private void FmLogs_Load(object sender, EventArgs e)
        {
            Text = "系統記錄檔";
            string[] logs = MainClass.mainClass.logs.ToArray();
            lbLogs.Items.AddRange(logs);
            MainClass.mainClass.OnMessage += new MainClass.OnMessgaeDelegate(mainClass_OnMessage);
        }

        void mainClass_OnMessage(string data)
        {
            MainClass.mainClass.srnItems.ShowSrnData(data, lbLogs);
        }

        private void FmLogs_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainClass.mainClass.OnMessage -= new MainClass.OnMessgaeDelegate(mainClass_OnMessage);
        }

        

        private void exortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainClass.mainClass.DoExport();
        }
    }
}
