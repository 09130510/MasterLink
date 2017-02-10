using PriceCalculator.Component;
using PriceCalculator.Utility;
using SourceCell;
using SourceGrid;
using SourceGrid.Cells;
using SourceGrid.Selection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PriceCalculator
{
    public partial class frmIIV : DockContent
    {
        private CHeaderCell cETFCode;
        private CHeaderCell cIIV;


        public frmIIV()
        {
            InitializeComponent();

            cETFCode = new CHeaderCell() { Caption = "ETFCode", Sortable = true };
            cIIV = new CHeaderCell() { Caption = "IIV", Sortable = true };
            gdNAV.Selection.SelectionChanged += new RangeRegionChangedEventHandler(Selection_SelectionChanged);
            RefreshGrid();
        }
        private void frmNAV2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Unbind();
        }


        private void Selection_SelectionChanged(object sender, RangeRegionChangedEventArgs e)
        {
            int row = ((FreeSelection)sender).ActivePosition.Row;
            if (row > 0)
            {
                Util.ETFSelectForm.Select(gdNAV[row, 0].Value.ToString());
            }

        }

        public void AddRow(ETF etf)
        {
            gdNAV.Redim(gdNAV.RowsCount + 1, 2);
            gdNAV[gdNAV.RowsCount - 1, 0] = etf.cETFCode.Field;
            gdNAV[gdNAV.RowsCount - 1, 1] = etf.cIIV.Field;
            gdNAV.InvokeIfRequired(() => ((SourceGrid.Cells.ColumnHeader)gdNAV[0, 0]).Sort(true));
            gdNAV.AutoSizeCells();
        }
        public void RemoveRow(ETF etf)
        {
            if (etf.cETFCode.Field.Grid == null) { return; }
            int index = etf.cETFCode.Field.Row.Index;
            gdNAV.Rows.Remove(index);
            gdNAV.InvokeIfRequired(() => ((SourceGrid.Cells.ColumnHeader)gdNAV[0, 0]).Sort(true));
            gdNAV.AutoSizeCells();
            etf.cETFCode.Field.UnBindToGrid();
            etf.cIIV.Field.UnBindToGrid();
        }
        public void Select(string etfcode)
        {
            gdNAV.Selection.ResetSelection(false);
            for (int i = 1; i < gdNAV.RowsCount; i++)
            {
                if (gdNAV[i, 0].Value.ToString() == etfcode)
                {
                    gdNAV.Selection.SelectRow(i, true);
                    gdNAV.ShowCell(new Position(i, 0), false);
                    return;
                }
            }
        }
        public void RefreshGrid()
        {
            InitGrid();
            gdNAV.Redim(Util.Calculator.IIVs.Count + 1, 2);
            for (int i = 0; i < Util.Calculator.IIVs.Count; i++)
            {
                ETF etf = Util.Calculator.IIVs.Values.ElementAt<ETF>(i);
                gdNAV[i + 1, 0] = etf.cETFCode.Field;
                gdNAV[i + 1, 1] = etf.cIIV.Field;
            }
            gdNAV.InvokeIfRequired(() => ((SourceGrid.Cells.ColumnHeader)gdNAV[0, 0]).Sort(true));
            gdNAV.AutoSizeCells();
        }
        private void InitGrid()
        {
            Unbind();
            gdNAV.Redim(1, 2);
            gdNAV[0, 0] = cETFCode.Field;
            gdNAV[0, 1] = cIIV.Field;

        }
        private void Unbind()
        {
            for (int i = 0; i < gdNAV.RowsCount; i++)
            {
                for (int j = 0; j < gdNAV.ColumnsCount; j++)
                {
                    if (gdNAV[i, j] != null)
                    {
                        gdNAV[i, j].UnBindToGrid();
                    }
                }
            }

        }
    }
}
