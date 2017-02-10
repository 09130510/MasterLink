using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OrderProcessor.SinoPac;
using Util.Extension;
using Util.Extension.Class;

namespace OrderProcessorTest
{
    public partial class Form1 : Form
    {
        private SinoPacProcessor m_Processor;

        public Form1()
        {
            InitializeComponent();
            
            m_Processor = new SinoPacProcessor();
            m_Processor.ConnectedEvent += new EventHandler(m_Processor_ConnectedEvent);
            m_Processor.DisconnectedEvent += new EventHandler(m_Processor_DisconnectedEvent);
            m_Processor.OrderReplyEvent += new SinoPacProcessor.OrderReplyDelegate(m_Processor_OrderReplyEvent);
            m_Processor.MatchReplyEvent += new SinoPacProcessor.MatchReplyDelegate(m_Processor_MatchReplyEvent);
            NotificationCenter.Instance.AddObserver(OnMessage, m_Processor.GetType().Name);
        }


        private void btnConnect_Click(object sender, EventArgs e)
        {
            m_Processor.Start();
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            m_Processor.Stop();
        }

        private void OnMessage(Notification n)
        {
            new Action(() =>
            {
                string msg = n.Sender.ToString() + "   " + n.Message.ToString();
                listBox1.Items.Add(msg);
                //Console.WriteLine(msg);               
            }).BeginInvoke(listBox1);
        }

        private void m_Processor_ConnectedEvent(object sender, EventArgs e)
        {
            lblStatus.ForeColor = Color.Green;
        }
        private void m_Processor_DisconnectedEvent(object sender, EventArgs e)
        {
            lblStatus.ForeColor = Color.Crimson;
        }
        private void m_Processor_OrderReplyEvent(SinoPacProcessor sender, SinoPacRPT reply)
        {
            
        }
        private void m_Processor_MatchReplyEvent(SinoPacProcessor sender, SinoPacRPT reply)
        {
            
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            SinoPacORD ord = new SinoPacORD();
            ord.Account = txtAccount.Text;
            //ord.Action = OrderProcessor.ActionType.
            ord.ClOrdID = m_Processor.GetSeqno().ToString();
            ord.OrigClOrdID = txtOrigClOrdID.Text;
            txtClOrdID.Text = m_Processor.Seqno.ToString();
            txtOrigClOrdID.Text = m_Processor.Seqno.ToString();
            ord.Currency = txtCurrency.Text;
            ord.Exchange = txtExchange.Text;
            ord.OrderType =  cboOrderType.Text.ToEnum<OrderProcessor.OrderType>(); 
            ord.Price = String.IsNullOrEmpty(txtPrice.Text)?0:double.Parse(txtPrice.Text);
            ord.Qty = String.IsNullOrEmpty(txtQty.Text)?1:int.Parse(txtQty.Text);
            ord.Side = cboSide.Text.ToEnum<OrderProcessor.Side>(); 
            ord.Symbol = txtSymbol.Text;
            ord.TimeInForce = cboTimeInForce.Text.ToEnum<OrderProcessor.TimeInForce>();
            if (sender == btnOrder)
            {
                m_Processor.Order(ord);
            }
            else if(sender == btnCancel)
            {
                m_Processor.Cancel(ord);
            }
            else if (sender == btnReplace)
            {
                m_Processor.Replace(ord);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            btnDisconnect_Click(btnDisconnect, EventArgs.Empty);
        }

        
    }
}
