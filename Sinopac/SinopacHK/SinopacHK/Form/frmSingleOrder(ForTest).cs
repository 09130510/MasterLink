using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using SinopacHK.Class;
using PriceProcessor.HK;

namespace SinopacHK
{
    public partial class frmSingleOrder : DockContent
    {
        public frmSingleOrder()
        {
            InitializeComponent();
            Utility.LoadConfig(this);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            HKProcessor pg = new HKProcessor("2827",0);
            pg.MarketPriceEvent += new PriceProcessor.HK.HKProcessor.MarketPriceDelegate(pg_OnPrice);
            pg.Start();
        }

        void pg_OnPrice(string PID, decimal Price)
        {
            //Console.WriteLine(PID + ":" + Price);
        }

    }
}
