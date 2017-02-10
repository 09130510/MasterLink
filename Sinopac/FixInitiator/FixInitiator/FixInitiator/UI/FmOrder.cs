using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FixInitiator.Class.Public;
using FixInitiator.Class;

namespace FixInitiator.UI
{
    public partial class FmOrder : Form
    {
        public FmOrder()
        {
            InitializeComponent();
        }

        private void FmOrder_Load(object sender, EventArgs e)
        {
            InitScreen();
        }

        private void InitScreen()
        {
            cbSide.Items.Clear();
            cbSide.Items.Add(new ComboItem("BORROW", Convert.ToByte('0')));
            cbSide.Items.Add(new ComboItem("BUY", Convert.ToByte('1')));
            cbSide.Items.Add(new ComboItem("SELL", Convert.ToByte('2')));
            cbSide.Items.Add(new ComboItem("BUY_MINUS", Convert.ToByte('3')));
            cbSide.Items.Add(new ComboItem("SELL_PLUS", Convert.ToByte('4')));
            cbSide.Items.Add(new ComboItem("SELL_SHORT", Convert.ToByte('5')));
            cbSide.Items.Add(new ComboItem("SELL_SHORT_EXEMPT", Convert.ToByte('6')));
            cbSide.Items.Add(new ComboItem("UNDISCLOSED", Convert.ToByte('7')));
            cbSide.Items.Add(new ComboItem("CROSS", Convert.ToByte('8')));
            cbSide.Items.Add(new ComboItem("CROSS_SHORT", Convert.ToByte('9')));
            cbSide.Items.Add(new ComboItem("CROSS_SHORT_EXEMPT", Convert.ToByte('A')));
            cbSide.Items.Add(new ComboItem("AS_DEFINED", Convert.ToByte('B')));
            cbSide.Items.Add(new ComboItem("OPPOSITE", Convert.ToByte('C')));
            cbSide.Items.Add(new ComboItem("SUBSCRIBE", Convert.ToByte('D')));
            cbSide.Items.Add(new ComboItem("REDEEM", Convert.ToByte('E')));
            cbSide.Items.Add(new ComboItem("LEND", Convert.ToByte('F')));
            cbSide.Items.Add(new ComboItem("BORROW", Convert.ToByte('G')));
            cbSide.SelectedIndex = 1;

            cbOrdType.Items.Clear();
            cbOrdType.Items.Add(new ComboItem("MARKET", Convert.ToByte('1')));
            cbOrdType.Items.Add(new ComboItem("LIMIT", Convert.ToByte('2')));
            cbOrdType.Items.Add(new ComboItem("STOP", Convert.ToByte('3')));
            cbOrdType.Items.Add(new ComboItem("STOP_LIMIT", Convert.ToByte('4')));
            cbOrdType.Items.Add(new ComboItem("MARKET_ON_CLOSE", Convert.ToByte('5')));
            cbOrdType.Items.Add(new ComboItem("WITH_OR_WITHOUT", Convert.ToByte('6')));
            cbOrdType.Items.Add(new ComboItem("LIMIT_OR_BETTER", Convert.ToByte('7')));
            cbOrdType.Items.Add(new ComboItem("LIMIT_WITH_OR_WITHOUT", Convert.ToByte('8')));
            cbOrdType.Items.Add(new ComboItem("ON_BASIS", Convert.ToByte('9')));
            cbOrdType.Items.Add(new ComboItem("ON_CLOSE", Convert.ToByte('A')));
            cbOrdType.Items.Add(new ComboItem("LIMIT_ON_CLOSE", Convert.ToByte('B')));
            cbOrdType.Items.Add(new ComboItem("FOREX_MARKET", Convert.ToByte('C')));
            cbOrdType.Items.Add(new ComboItem("PREVIOUSLY_QUOTED", Convert.ToByte('D')));
            cbOrdType.Items.Add(new ComboItem("PREVIOUSLY_INDICATED", Convert.ToByte('E')));
            cbOrdType.Items.Add(new ComboItem("FOREX_LIMIT", Convert.ToByte('F')));
            cbOrdType.Items.Add(new ComboItem("FOREX_SWAP", Convert.ToByte('G')));
            cbOrdType.Items.Add(new ComboItem("FOREX_PREVIOUSLY_QUOTED", Convert.ToByte('H')));
            cbOrdType.Items.Add(new ComboItem("FUNARI", Convert.ToByte('I')));
            cbOrdType.Items.Add(new ComboItem("MARKET_IF_TOUCHED", Convert.ToByte('J')));
            cbOrdType.Items.Add(new ComboItem("MARKET_WITH_LEFTOVER_AS_LIMIT", Convert.ToByte('K')));
            cbOrdType.Items.Add(new ComboItem("PREVIOUS_FUND_VALUATION_POINT", Convert.ToByte('L')));
            cbOrdType.Items.Add(new ComboItem("NEXT_FUND_VALUATION_POINT", Convert.ToByte('M')));
            cbOrdType.SelectedIndex = 1;

            cbTimeInForce.Items.Clear();
            cbTimeInForce.Items.Add(new ComboItem("Day", Convert.ToByte('0')));
            cbTimeInForce.Items.Add(new ComboItem("GOOD_TILL_CANCEL", Convert.ToByte('1')));
            cbTimeInForce.Items.Add(new ComboItem("AT_THE_OPENING", Convert.ToByte('2')));
            cbTimeInForce.Items.Add(new ComboItem("IMMEDIATE_OR_CANCEL", Convert.ToByte('3')));
            cbTimeInForce.Items.Add(new ComboItem("FILL_OR_KILL", Convert.ToByte('4')));
            cbTimeInForce.Items.Add(new ComboItem("GOOD_TILL_CROSSING", Convert.ToByte('5')));
            cbTimeInForce.Items.Add(new ComboItem("GOOD_TILL_DATE", Convert.ToByte('6')));
            cbTimeInForce.Items.Add(new ComboItem("AT_THE_CLOSE", Convert.ToByte('7')));
            cbTimeInForce.SelectedIndex = 0;

        }

        

        private void butOrder_Click(object sender, EventArgs e)
        {
            MainClass.mainClass.NewOrderSingle(txtAccount.Text, txtSymbol.Text, (char)(cbSide.SelectedItem as ComboItem).value ,
               (char)(cbOrdType.SelectedItem as ComboItem).value , double.Parse(txtPrice.Text), double.Parse(txtOrderQty.Text), (char)(cbTimeInForce.SelectedItem as ComboItem).value);
                
                
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            MainClass.mainClass.OrderCancelRequest(txtClOrdID.Text, txtAccount.Text, txtSymbol.Text, (char)(cbSide.SelectedItem as ComboItem).value);
        }
    }
}
