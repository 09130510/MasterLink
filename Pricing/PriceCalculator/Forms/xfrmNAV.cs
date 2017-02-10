using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using PriceCalculator.Utility;
using System.Threading;
using log4net;

namespace PriceCalculator
{
    public partial class frmNAV : DockContent
    {
        private ILog m_Log = LogManager.GetLogger(typeof(frmNAV));
        public frmNAV()
        {
            InitializeComponent();

            //dataGridView1.DataSource = Util.Calculator.DisplayTable;
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
            dataGridView1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            string decimalPlaces = Util.INI["SYS"]["DECIMALPLACES"];
            if (!String.IsNullOrEmpty(decimalPlaces))
            {
                dataGridView1.Columns[1].DefaultCellStyle.Format = "#." + new string('0', int.Parse(decimalPlaces));
            }                
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
           // Update();
        }
        //public void Update(DataView view)
        //{
        //    dataGridView1.InvokeIfRequired(() =>
        //    {
        //        dataGridView1.Update();
        //    });
        //}
    }
}
