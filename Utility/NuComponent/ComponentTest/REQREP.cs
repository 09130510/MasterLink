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
    public partial class REQREP : Form
    {
        private Req req;
        private Rep rep;
        public REQREP()
        {
            InitializeComponent();

            rep = new Rep(ConnectType.Bind, 666);
            rep.OnREQReceived += new Rep.OnREQReceivedDelegate(rep_OnReqReceived);
            
            //rep.OnReqReceived += new Rep.OnRequestDelegate(rep_OnRequest);
            rep.Start();


            req = new Req(ConnectType.Connect, 666);
            req.OnREPReceived += new Req.OnREPReceivedDelegate(req_OnREPReceived);
            //req.OnResponse += new Req.OnResponseDelegate(req_OnResponse);
        }

        void req_OnREPReceived(string Head, object Msg)
        {
            Console.WriteLine("////" +Head +"|"+ Msg);
        }

        void rep_OnReqReceived(string Head, string Msg)
        {
            Console.WriteLine("*****" + Head + "|"+ Msg);
            if (Head== "2222222")
            {
                rep.Response("return", "TEST");
                return;
            }
            rep.Response("return");   
        }

        //void req_OnResponse(string Msg)
        //{
        //    Console.WriteLine("////" + Msg);
        //}

        //void rep_OnRequest(string RequestStr)
        //{
        //    Console.WriteLine("*****" + RequestStr);
        //    rep.Response("return");   
        //}

        private void REQREP_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            req.Request("1111111","message1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            req.Request("2222222","message2");
        }
    }
}
