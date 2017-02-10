using PATSOrder.Class;
using PATSOrder.Properties;
using PATSOrder.Utility;
using PriceLib.PATS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PATSOrder
{
    public partial class frmTick : DockContent
    {
        #region Variable
        private Tick m_Tick = null;
        #endregion

        #region Property
        public Tick Tick { get { return m_Tick; } }
        public int SeqNo { get; } = 0;        

        #endregion

        public frmTick(int seqno)
        {
            InitializeComponent();
            SeqNo = seqno;
            cboPID.DisplayMember = "Key";
            cboAccount.DisplayMember = "TraderAccount";
            _Caption();
            Util.LoadConfig(this);

            m_Tick = new Tick((int)nudTickNumber.Value);
            _SetCell();

            Center.Instance.AddObserver(OnPATSChange, nameof(Util.PATS));
            OnPATSChange(nameof(Util.PATS), new Msg(null, nameof(Util.ProductInfos)));
            OnPATSChange(nameof(Util.PATS), new Msg(null, nameof(Util.AccountInfos)));

        }
        private void frmTick_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.TickSeqNo.Remove(SeqNo);
            Center.Instance.RemoveObserver(OnPATSChange, nameof(Util.PATS));
        }

        private void cboPID_SelectedIndexChanged(object sender, EventArgs e)
        {
            _Caption();
            m_Tick.ProductInfo = (ProductInfo)cboPID.SelectedItem;
            Util.OrderSettingForm.ChangeProduct(m_Tick);
        }
        private void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Tick.Account = (AccountInfo)cboAccount.SelectedItem;
            Util.OrderSettingForm.ChangeAccount(m_Tick);
        }
        private void btnProductFilter_Click(object sender, EventArgs e)
        {
            //filter.Show(this.DockPanel, new Rectangle(0, 0, 300, 300));            
            Util.ProdFilterForm.ShowDialog(this);
            //Util.ProdFilterForm.Show(this);
            //Util.ProdFilterForm.Activate();
        }
        private void btnAlignment_Click(object sender, EventArgs e)
        {
            grid2.CustomScrollPosition = new Point(0, 0);

            int visibleRows = grid2.GetVisibleRows(true).Count;
            int displayRange = visibleRows - grid2.FixedRows;
            int centralRow = displayRange / 2 + grid2.FixedRows - 1;
            int mpRow = m_Tick.CellList[RowGenre.MP][0][ColGenre.Price].Cell.Row.Index;
            if (mpRow <= centralRow) { return; }
            int showRow = mpRow + centralRow - grid2.FixedRows;
            grid2.ShowCell(new SourceGrid.Position(showRow, (int)ColGenre.Price), true);
        }
        private void nudFontSize_ValueChanged(object sender, EventArgs e)
        {
            m_Tick.ChangeRowPrperty(nameof(Row.FontSize), (float)nudFontSize.Value);
            grid2.AutoSizeCells();
        }
        private void nudTickNumber_ValueChanged(object sender, EventArgs e)
        {
            if (m_Tick != null)
            {
                m_Tick.Dispose();
            }
            m_Tick = new Tick((int)nudTickNumber.Value, chkExtend.Checked, !chkSorting.Checked, (float)nudFontSize.Value) { ProductInfo = (ProductInfo)cboPID.SelectedItem };
            _SetCell();
        }
        private void chkSorting_CheckedChanged(object sender, EventArgs e)
        {
            chkSorting.Image = !chkSorting.Checked ? Resources._1452261702_stock_sort_ascending : Resources._1452261698_stock_sort_descending;
            m_Tick.isAscending = !chkSorting.Checked;
            _SetCell();
        }
        private void chkExtend_CheckedChanged(object sender, EventArgs e)
        {
            m_Tick.isExtend = chkExtend.Checked;
        }

        #region Delegate
        private void OnPATSChange(string MsgName, Msg Msg)
        {
            switch (Msg.Message.ToString())
            {
                case nameof(Util.ProductInfos):
                    if (Util.ProductInfos == null) { return; }
                    cboPID.InvokeIfRequired(() =>
                    {
                        cboPID.Items.Clear();
                        //cboPID.DataSource = Util.ProductInfos;
                        cboPID.Items.AddRange(Util.ProductInfos.ToArray());
                    });
                    break;
                case nameof(Util.AccountInfos):
                    if (Util.AccountInfos == null) { return; }
                    cboAccount.InvokeIfRequired(() =>
                    {
                        cboAccount.Items.Clear();
                        //cboAccount.DataSource = Util.AccountInfos;
                        cboAccount.Items.AddRange(Util.AccountInfos.ToArray());
                    })
; break;
                default:
                    break;
            }
        }
        #endregion

        #region Private
        private void _Caption()
        {
            this.Text = $"[{SeqNo}] {cboPID.Text}";
        }
        private void _SetCell()
        {
            grid2.Rows.Clear();
            grid2.Redim(m_Tick.RowCount, m_Tick.ColCount);
            //Cell排序處理
            var RowsByClass = m_Tick.CellList.OrderBy(e => m_Tick.isAscending ? e.Key.AscendingNo() : e.Key.DescendingNo()).Select(e => e.Value);
            int rCnt = 0;
            foreach (var rows in RowsByClass)
            {
                foreach (var row in rows)
                {
                    for (int cCnt = 0; cCnt < m_Tick.ColCount; cCnt++)
                    {
                        if (row[cCnt].Cell.Grid != null) { row[cCnt].Cell.UnBindToGrid(); }
                        grid2[rCnt, cCnt] = row[cCnt].Cell;
                    }
                    rCnt++;
                }
            }
            grid2.AutoSizeCells();
            grid2.Refresh();
        }
        #endregion
    }
}
