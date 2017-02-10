using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using PriceCalculator.Component;
using PriceCalculator.Utility;
using log4net;
using System.Reflection;

namespace PriceCalculator
{
    public partial class frmETFSelect : DockContent
    {
        private ILog m_Log = LogManager.GetLogger(typeof(frmETFSelect));

        public frmETFSelect()
        {
            InitializeComponent();            
        }

        private void etfBox_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) { return; }
            Util.FXRates.frmFX.SetCurncy(e.Node.Name);
            Util.Info(m_Log, nameof(etfBox_AfterCheck), $"{e.Node.Name} Selected:{e.Node.Checked}");
            if (e.Node.Checked)
            {
                Util.Calculator.Add(new ETF(e.Node.Name, DateTime.Now));
            }
            else
            {                
                Util.Calculator.Remove(e.Node.Name);
            }
            Util.INI["SYS"]["CALCETF"] = string.Join(";", etfBox.CheckedNodes.ToArray());
            Util.WriteConfig();
        }
        private void etfBox_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0) { return; }
            Util.FXRates.frmFX.SetCurncy(e.Node.Name);
            Util.IIVForm.Select(e.Node.Name);
        }


        public void Add(string etfcode)
        {
            etfBox.Add(etfcode);
        }
        public void Detail()
        {
            if (!string.IsNullOrEmpty(etfBox.SelectedETFCode))
            {
                etfBox.Detail(etfBox.SelectedETFCode);
            }
        }
        public void LockYP()
        {
            if (!string.IsNullOrEmpty(etfBox.SelectedETFCode))
            {
                etfBox.LockYP(etfBox.SelectedETFCode);
            }
        }
        public void Select(string etfcode)
        {
            etfBox.Select(etfcode);
        }

    }
}
