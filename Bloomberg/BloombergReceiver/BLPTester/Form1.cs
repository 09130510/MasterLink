using System;
using System.Windows.Forms;
using BLParser;
using Util.Extension.ZMQ;


namespace BLPTester
{
    public partial class Form1 : Form
    {
        private Req REQ;
        public Form1()
        {
            InitializeComponent();


            


            Sub s = new Sub(ConnectType.Connect, 555,"192.168.1.6");
            s.SubscribeAll();
            s.OnSUBReceived += new Sub.OnSUBReceivedDelegate(s_OnReceive);
            s.Start();

            REQ = new Req(ConnectType.Connect, 666,"192.168.1.6");
        }

        void s_OnReceive(string Head, object Msg)
        {
            //throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            REQ.Request("SECURITY", "1707 TT Equity");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            REQ.Request("FIELDS", "LOW");
        }
    }
}
