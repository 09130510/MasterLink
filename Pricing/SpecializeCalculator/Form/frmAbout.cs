using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Text.RegularExpressions;

namespace PriceCalculator
{
    public partial class frmAbout : DockContent
    {
        public frmAbout()
        {
            InitializeComponent();

            foreach (Control Control in this.Controls)
            {
                _Info(Control);
            }
        }

        private void _Info(Control control)
        {
            foreach (Control subcontrol in control.Controls)
            {
                if (!(subcontrol is ColorLabel))
                {
                    _Info(subcontrol);
                    continue;
                }
                ColorLabel lbl = (ColorLabel)subcontrol;
                if (lbl.Tag == null || string.IsNullOrEmpty(lbl.Tag.ToString()))
                {
                    continue;
                }
                foreach (var info in Util.INI[lbl.Tag.ToString()])
                {
                    // lbl.Text +=string.Format("{0}: {1}\r\n", info.KeyName, info.Value);
                    lbl.AppendText($"[{info.KeyName}] ", Color.Empty);
                    if (info.Value.Contains('|') && info.Value.Contains(';'))
                    {
                        lbl.AppendText("\r\n", Color.Empty);
                        string[] records = info.Value.Split('|');
                        for (int i = 0; i < records.Length; i++)
                        {
                            if (string.IsNullOrEmpty(records[i])) { continue; }
                            string[] items = records[i].Split(';');
                            for (int j = 0; j < items.Length; j++)
                            {
                                if (string.IsNullOrEmpty(items[j])) { continue; }
                                if (j == 0)
                                {
                                    lbl.AppendText($"[{i}]", Color.Maroon);
                                    lbl.AppendText($"   {items[j]}\r\n", Color.Green);
                                }
                                else
                                {
                                    lbl.AppendText($"    \t{items[j]}\r\n", Color.Green);
                                }
                            }
                        }
                    }
                    else
                    {
                        lbl.AppendText($"{info.Value}\r\n", Color.Green);
                    }
                }
            }
        }
    }
}
