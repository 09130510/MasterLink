using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PriceProcessor.Capital;
using Util.Extension;

namespace PriceProcessorTest
{
    public partial class Form1 : Form
    {
        private CapitalProcessor m_Processor;

        public Form1()
        {
            InitializeComponent();
            m_Processor = new CapitalProcessor();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_Processor.OnOverseaProduct += new CapitalProcessor.OnOverseaProductDelegate(m_Processor_OnOverseaProduct);
            m_Processor.OnPriceChange += new CapitalProcessor.OnPriceChangeDelegate(m_Processor_OnPriceChange);

            string msg;
            m_Processor.Start("SUB0007144", "v52500938", out msg);

        }

        void m_Processor_OnPriceChange(string PID, string TickName, Price price)
        {
            new Action(() =>
            {
                listBox1.Items.Insert(0,price.ToString());
            }).BeginInvoke(listBox1);
        }

        private void m_Processor_OnOverseaProduct(string Exchange,string PID)
        {
            if (Exchange.Contains("OSE"))
            {
                comboBox1.Items.Add(PID);    
            }            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {            
            m_Processor.Subscribe(0,comboBox1.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Processor.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
