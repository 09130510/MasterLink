using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GQuoteTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void axGQuoteOpr1_ConnectReady(object sender, EventArgs e)
        {
            //SymbolID = 1：證券(TSE + OTC)
            //SymbolID = 2：期貨(FUT + OBFUT)
            //SymbolID = 3：選擇權(OPT + OBOPT)
            //SymbolID = 4：上市(TSE)
            //SymbolID = 5：上櫃(OTC)
            //SymbolID = 6：國內期貨(FUT)
            //SymbolID = 7：國外期貨(OBFUT)
            //SymbolID = 8：國內興櫃(REG)
            //SymbolID = 9：國內證券(TSE + OTC + REG)
            //SymbolID = 10：國內期貨選擇權(FUT + OPT)
            //SymbolID = 11：國內選擇權(OPT)
            //SymbolID = 12：國外選擇權(OBOPT)

            button2.ForeColor = Color.ForestGreen;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            axGQuoteOpr1.DoConnect(@"./ML192.168.5.116.ini", "GQUOTE_ENTERPRISE", "OptRisk", "ml2856");
        }

        private void axGQuoteOpr1_Data(object sender, AxGQUOTEOPRLib._DGQuoteOprEvents_DataEvent e)
        {
            Console.WriteLine(
                $"{DateTime.Now.ToString("HH:mm:ss.fff")} {axGQuoteOpr1.GetSymbolInfoString(e.nSymbolIndex, 0)}: {axGQuoteOpr1.GetSymbolInfoValue(e.nSymbolIndex, 3)}");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {            
            //Console.WriteLine(axGQuoteOpr1.SymbolLookup(textBox2.Text));
            var items = axGQuoteOpr1.SymbolLookup(textBox2.Text).Split(';');
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            axGQuoteOpr1.DoSub(textBox1.Text);
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            axGQuoteOpr1.DoDisconnect();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            axGQuoteOpr1.DoUnsub(textBox1.Text);            
        }

        private void axGQuoteOpr1_WriteLog(object sender, AxGQUOTEOPRLib._DGQuoteOprEvents_WriteLogEvent e)
        {
            Console.WriteLine($"{e.nType}: {e.strLog}");
        }

        private void axGQuoteOpr1_Kick(object sender, AxGQUOTEOPRLib._DGQuoteOprEvents_KickEvent e)
        {
            
        }

        private void axGQuoteOpr2_ConnectFail(object sender, AxGQUOTEOPRLib._DGQuoteOprEvents_ConnectFailEvent e)
        {

        }

        private void axGQuoteOpr2_ConnectLost(object sender, EventArgs e)
        {

        }
    }
}
