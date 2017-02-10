using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace RTDServer
{
    public partial class Form1 : Form
    {
        private ServiceHost host;
        private RTDSvr.RTDServer svr;
            
        public Form1()
        {
            InitializeComponent();

            Uri baseAddress = new Uri("net.tcp://localhost:8080/rtd");
            svr = new RTDSvr.RTDServer();
            host = new ServiceHost(svr, baseAddress);
            host.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ////RTDServer s = new RTDServer();
            ////s.SendItem("0007144", "JTI201512", SubItem.BLot, 0);
            //Type myType = Type.GetTypeFromProgID("ML.RTD");
            //dynamic obj = Activator.CreateInstance(myType);
            //obj.SendItem("0007144", "JTI201512", SubItem.BLot, 20);
            
            svr.SendValues("0007144","JTI201512", RTDSvr.SummaryItem.BLot,6);
        }
    }
}
