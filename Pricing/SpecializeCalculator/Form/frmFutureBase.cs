#define SUDPIDMODIFY

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using SourceCell;
using System.Reflection;
using SourceGrid;

namespace PriceCalculator
{
    public partial class frmFutureBase : DockContent
    {
        #region Variable
        private List<FutureBase> m_Calculators = new List<FutureBase>();
        #endregion

        #region Property
        public List<FutureBase> Calculators { get { return m_Calculators; } }
        #endregion

        public frmFutureBase()
        {
            InitializeComponent();
            //m_CellProperties = typeof(Calculator).GetProperties().Where(e => e.isCellProperty()).ToArray();
            _Head();
            string[] calcs = Util.INI["SYS"]["CALCETF"].Split(new char[] { FutureBase.MAINSPLIT }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var calc in calcs)
            {
                Add(new FutureBase(calc.Split(FutureBase.SUBSPLIT)));
            }
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            Util.INI["SYS"]["CALCETF"] = string.Join(FutureBase.MAINSPLIT.ToString(), m_Calculators.Select(entry => entry.ToString()).ToArray());
            Util.WriteConfig();
            AlertBox.AlertWithoutReply(this, AlertBoxButton.Msg_OK, "設定", "存檔成功！");
        }
        private void tsAdd_Click(object sender, EventArgs e)
        {
#if !SUDPIDMODIFY
            Add(new FutureBase(string.Empty));            
#else
            frmModify modify = new frmModify($"新增", string.Empty, new FutureBase(string.Empty));

            if (modify.ShowDialog() == DialogResult.OK)
            {
                modify.FutureBase.ChangeYstNAV(modify.ChgYstNAV);
                Add(modify.FutureBase);
            }
#endif            
        }
        private void tsMinus_Click(object sender, EventArgs e)
        {
            if (grid1.Selection.IsEmpty()) { return; }
            if (AlertBox.Alert(this, AlertBoxButton.YesNo, "刪除報價設定", "是否刪除選取的報價設定？"))
            {
                Minus();
            }
        }
        private void grid1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                tsMinus_Click(tsMinus, EventArgs.Empty);
            }
        }

        #region Public
        public void Add(FutureBase calc)
        {
            grid1.Redim(grid1.RowsCount + 1, grid1.ColumnsCount);
            Calculators.Add(calc);
            for (int i = 0; i < calc.CellList.Count; i++)
            {
                grid1[grid1.RowsCount - 1, i] = calc.CellList[i].Cell;
            }
        }
        public void Minus()
        {
            RangeRegion range = grid1.Selection.GetSelectionRegion();
            foreach (var r in range)
            {
                for (int i = r.End.Row; i >= r.Start.Row; i--)
                {
                    FutureBase cal = (FutureBase)grid1[i, 0].Tag;
                    Calculators.Remove(cal);
                    cal.Dispose();
                }
            }

            for (int i = 0; i < grid1.RowsCount; i++)
            {
                for (int j = 0; j < grid1.ColumnsCount; j++)
                {
                    if (grid1[i, j] != null) { grid1[i, j].UnBindToGrid(); }
                }
            }

            _Head();
            grid1.Redim(Calculators.Count + 1, grid1.ColumnsCount);
            for (int i = 1; i < Calculators.Count + 1; i++)
            {
                FutureBase calc = Calculators[i - 1];
                for (int j = 0; j < calc.CellList.Count; j++)
                {
                    grid1[i, j] = calc.CellList[j].Cell;
                }
            }
            grid1.Refresh();
        }
        #endregion

        private void _Head()
        {
            grid1.Rows.Clear();
            string[] headers = FutureBase.Headers();
            grid1.Redim(1, headers.Length);
            for (int i = 0; i < headers.Length; i++)
            {
                grid1[0, i] = new CHeaderCell() { Caption = headers[i] }.Field;
            }
        }
    }
}
