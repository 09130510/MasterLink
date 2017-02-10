using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util.Extension.ZMQ;

namespace ComponentTest
{
    public partial class Form2 : Form
    {
        Pub p;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            p = new Pub(ConnectType.Bind, 5555);
            //p.Start();

            Sub s = new Sub(ConnectType.Connect, 5555);
            s.SubscribeAll();
            s.OnSUBReceived += new Sub.OnSUBReceivedDelegate(s_OnReceive);            
            s.Start();
        }

        void s_OnReceive(string Head, object Msg)
        {
            Console.WriteLine("Head: " + Head);
            Console.WriteLine("Msg: " + Msg.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            p.Send("button1", this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            p.Send("CONNECTED", this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            p.Send("CONNE88888", this);
        }
    }
}
