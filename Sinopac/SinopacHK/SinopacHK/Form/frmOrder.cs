using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using OrderProcessor.SinoPac;
using Util.Extension;

namespace SinopacHK
{
    public partial class frmOrder : DockContent
    {
        public frmOrder()
        {
            InitializeComponent();
            Utility.LoadConfig(this);
        }

        private void nudFont_ValueChanged(object sender, EventArgs e)
        {
            if (Utility.Unit.Tick != null)
            {
                Utility.Unit.Tick.ToSetFontSize(nudFont.Value);
                grid1.AutoSizeCells();
                this.SetBounds(this.Location.X, this.Location.Y, this.Width + 1, this.Height);
                this.SetBounds(this.Location.X, this.Location.Y, this.Width - 1, this.Height);
                Utility.SaveConfig(sender as Control);
            }
        }
        private void nudTickCount_ValueChanged(object sender, EventArgs e)
        {
            Utility.SaveConfig(sender as Control);
            Utility.Unit.TickCount = (int)nudTickCount.Value;

            SetCell();
        }

        public void SetOrderSummary(double buy, double sell)
        {
            new Action(() =>
            {
                lblBSummary.Text = buy.ToString("#,##0");
                lblSSummary.Text = sell.ToString("#,##0");
            }).BeginInvoke(this);
        }

        #region Private
        private void SetCell()
        {
            for (int i = 0; i < grid1.RowsCount; i++)
            {
                for (int j = 0; j < grid1.ColumnsCount; j++)
                {
                    grid1[i, j].UnBindToGrid();
                }
            }
            grid1.Rows.Clear();
            grid1.Redim(Utility.Unit.Tick.RowCnt, Utility.Unit.Tick.ColCnt);
            foreach (var item in Utility.Unit.Tick.TickList)
            {
                grid1.SetCell(item.Key, item.Value);
            }
            grid1.AutoSizeCells();
        }
        #endregion
    }
}
