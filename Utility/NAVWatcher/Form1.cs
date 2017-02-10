using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PriceLib.Redis;
using System.Diagnostics;

namespace NAVWatcher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Text =Util.VersionInfo(this);            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Add(new SubscriptionBox());
        }

        private void tsClear_Click(object sender, EventArgs e)
        {
            for (int i = flowLayoutPanel1.Controls.Count -1; i >= 0; i--)
            {
                SubscriptionBox box = (SubscriptionBox)flowLayoutPanel1.Controls[i];
                if (box.Channel == "" && box.Item == "")
                {
                    flowLayoutPanel1.Controls.Remove(box);
                    box.Dispose();
                }
            }            
        }

        private void tsConnect_Click(object sender, EventArgs e)
        {
            if (tsIP.Text == "" || tsPort.Text == "") { return; }
            if (tsConnect.Text=="Go")
            {
                tsIP.Enabled = tsPort.Enabled = false;
                Util.RedisLib = new RedisLib(tsIP.Text, int.Parse(tsPort.Text), Process.GetCurrentProcess().Id);                
            }
                   
        }
    }
}
