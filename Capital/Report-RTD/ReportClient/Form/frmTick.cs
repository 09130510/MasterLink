using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Capital.Report.Class;
using Capital.Report.Properties;
using PriceProcessor.Capital;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace Capital.Report
{
    public partial class frmTick : DockContent
    {
        #region Variable
        private Tick m_Tick;
        private short m_SeqNo = -1;
        #endregion

        #region Property
        public Product Product { get { return string.IsNullOrEmpty(cboPID.Text) ? null : Core.Instance.Price.Products[cboPID.Text]; } }
        public int Seqno { get { return m_SeqNo; } }
        public Tick Tick { get { return m_Tick; } }
        #endregion

        public frmTick(short seqno, string pid)
        {
            InitializeComponent();

            m_SeqNo = seqno;
            _FormCaption();
            Utility.LoadConfig(this);

            m_Tick = new Tick(m_SeqNo, "", "", pid, "", "","", (int)nudTickCount.Value, chkExtend.Checked, chkisAscending.Checked);
            _SetCell();

            Core.Instance.OnOverseaProduct += new CapitalProcessor.OnOverseaProductDelegate(OnOverseaProduct);
        }
        private void frmTick_FormClosing(object sender, FormClosingEventArgs e)
        {
            Core.Instance.OnOverseaProduct -= new CapitalProcessor.OnOverseaProductDelegate(OnOverseaProduct);
            Core.Instance.DisposeTickForm(m_SeqNo);
        }

        private void nudFont_ValueChanged(object sender, EventArgs e)
        {
            if (m_Tick != null)
            {
                m_Tick.ToSetFontSize(nudFont.Value);
                grid1.AutoSizeCells();
                this.SetBounds(this.Location.X, this.Location.Y, this.Width + 1, this.Height);
                this.SetBounds(this.Location.X, this.Location.Y, this.Width - 1, this.Height);
            }
            Utility.SaveConfig(sender as Control);
        }
        private void nudTickCount_ValueChanged(object sender, EventArgs e)
        {
            Utility.Log(this, "TickCount", nudTickCount.Value.ToString());
            Utility.SaveConfig(sender as Control);
            if (m_Tick != null) { m_Tick.Dispose(); }

            m_Tick = new Tick(m_SeqNo, cboAccount.Text, Product == null ? "" : Product.Exchange, Product == null ? "" : Product.StockNo, Product == null ? "" : Product.OrderHead, Product == null ? "" : Product.YM1, Product == null ? "" : Product.YM2, (int)nudTickCount.Value, chkExtend.Checked, chkisAscending.Checked);

            _SetCell();
        }
        private void cboPID_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FormCaption();

            if (this.Product != null)
            {
                m_Tick.Exchange = Product.Exchange;
                m_Tick.OrderHead = Product.OrderHead;
                m_Tick.YM = Product.YM1;
                m_Tick.YM2 = Product.YM2;
                m_Tick.OrderPID = Product.OrderPID;                
                m_Tick.PID = Product.StockNo;

                NotificationCenter.Instance.Post("CHANGEPRODUCT", new Notification(m_Tick, Product));
            }
            cboAccount.Items.Clear();
            cboAccount.Items.AddRange(Core.Instance.Order.Accounts.Select(E => E.BrokerID + E.CustNo).ToArray());
            if (cboAccount.Items.Count == 1) { cboAccount.SelectedIndex = 0; }
        }
        private void cboPID_DropDown(object sender, EventArgs e)
        {
            cboPID.Items.Clear();
            cboPID.Items.AddRange(Core.Instance.Products());
            cboPID.Text = m_Tick.PID;
        }

        private void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Product != null)
            {
                m_Tick.Account = cboAccount.Text;
                NotificationCenter.Instance.Post("CHANGEPRODUCT", new Notification(m_Tick, Product));
            }
        }
        private void chkExtend_CheckedChanged(object sender, EventArgs e)
        {
            Utility.Log(this, "Extend", chkExtend.Checked.ToString());
            if (m_Tick != null) { m_Tick.Dispose(); }
            m_Tick = new Tick(m_SeqNo, cboAccount.Text,
                Product == null ? "" : Product.Exchange,
                Product == null ? "" : Product.StockNo,
                Product == null ? "" : Product.OrderHead,
                Product == null ? "" : Product.YM1,
                Product == null ? "" : Product.YM2,
                (int)nudTickCount.Value, chkExtend.Checked, chkisAscending.Checked);
            _SetCell();
        }
        private void chkisAscending_CheckedChanged(object sender, EventArgs e)
        {
            if (m_Tick != null) { m_Tick.isAscending = chkisAscending.Checked; }
            Utility.SaveConfig(sender as Control);

            chkisAscending.Image = chkisAscending.Checked ? Resources._1452262106_stock_sort_ascending_16 : Resources._1452262116_stock_sort_descending_16;
            if (m_Tick != null) { _SetCell(); }
        }
        private void btnAlignment_Click(object sender, EventArgs e)
        {
            grid1.CustomScrollPosition = new Point(0, 0);
            int fixRow = grid1.FixedRows;
            int visibleRow = grid1.GetVisibleRows(true).Count;
            int displayRange = visibleRow - fixRow;
            int centerRow = displayRange / 2 + fixRow - 1;
            var mp = m_Tick.TickList.FirstOrDefault(entry => entry.Value == m_Tick.cMP.Cell);
            int mpRow = mp.Key.Row;
            if (mpRow <= centerRow) { return; }
            int showRow = mpRow + centerRow - fixRow;
            grid1.ShowCell(new SourceGrid.Position(showRow, mp.Key.Column), true);
        }

        #region Delegate
        private void OnOverseaProduct(string Exchange, string PID)
        {
            if (cboPID.Items.Contains(PID)) { return; }
            cboPID.Items.Add(PID);
        }
        #endregion

        #region Private
        private void _FormCaption()
        {
            Text = $"[{m_SeqNo}] {cboPID.Text}";
        }
        private void _SetCell()
        {
            for (int i = 0; i < grid1.RowsCount; i++)
            {
                for (int j = 0; j < grid1.ColumnsCount; j++)
                {
                    grid1[i, j].UnBindToGrid();
                }
            }
            grid1.Rows.Clear();
            grid1.Redim(m_Tick.RowCnt, m_Tick.ColCnt);
            foreach (var item in m_Tick.TickList)
            {
                grid1.SetCell(item.Key, item.Value);
            }
            grid1.AutoSizeCells();
        }
        #endregion
    }
}