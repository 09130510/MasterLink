using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevAge.Drawing;
using OrderProcessor;
using OrderProcessor.SinoPac;
using PriceProcessor.HK;
using SourceCell;
using SourceGrid;
using SourceGrid.Cells;
using Util.Extension.Class;

namespace SinopacHK.Class
{
    /// <summary>
    /// 換商品 => 重訂，不重建
    /// 改TickCount => 重建
    /// </summary>
    public class Tick : NotifyDisposableClass
    {
        #region View
        private static RectangleBorder Border = RectangleBorder.CreateInsetBorder(1, Color.Silver, Color.White);
        private static Color BuyBC = Color.FromArgb(50, 255, 30, 142);
        private static Color SellBC = Color.FromArgb(50, 0, 128, 255);
        private static Color BuyFC = Color.Crimson;
        private static Color SellFC = Color.Green;
        private static Color StatBC = Color.White;
        #endregion

        #region Variable
        private Unit m_Unit;
        #endregion

        #region Property
        private Deal Deal { get { return m_Unit.Deal; } }
        private ProductInfo Product { get { return ProductCollection.Selected; } }
        private int TickCount { get; set; }
        /// <summary>
        /// 最大顯示Tick
        /// </summary>
        private decimal MaxTick
        {
            get
            {
                if (TickCount == 0) { return (decimal)cMP.Value; }
                return (decimal)cBP[TickCount-1].Value;
            }
        }
        /// <summary>
        /// 最小顯示Tick
        /// </summary>
        private decimal MinTick
        {
            get
            {
                if (TickCount == 0) { return (decimal)cMP.Value; }
                return (decimal)cAP[0].Value;
            }
        }
        /// <summary>
        /// Tick區的Row Count
        /// </summary>
        public int RowCnt { get { return TickCount * 2 + 4; } }
        /// <summary>
        /// Tick區的ColumnCount
        /// </summary>
        public int ColCnt { get { return 3; } }
        #endregion

        #region TickCell
        private Dictionary<Position, ICell> m_TickList;
        public Dictionary<Position, ICell> TickList
        {
            get
            {
                if (m_TickList == null)
                {
                    m_TickList = new Dictionary<Position, ICell>();
                    //Column的順序是看ColumnType
                    foreach (ColumnType col in Enum.GetValues(typeof(ColumnType)))
                    {
                        //跳過ColumnType的Default值
                        if (col == ColumnType.None) { continue; }
                        int rowIdx = 0;
                        //Row的順序是看RowType
                        foreach (RowType row in Enum.GetValues(typeof(RowType)))
                        {
                            //跳過RowType的Default值
                            if (row == RowType.None) { continue; }

                            var pi = this.GetType().GetProperties().FirstOrDefault(p => p.Col() == col && p.Row() == row);
                            //找不到欄位, 塞空欄位
                            if (pi == null)
                            {
                                EmptyCell(row, col, ref rowIdx);
                                continue;
                            }

                            if (pi.PropertyType.BaseType == typeof(CellBase))
                            {
                                SingleCell(row, col, pi, ref rowIdx);
                            }
                            else if (pi.PropertyType.BaseType == typeof(Array))
                            {
                                for (int i = TickCount - 1; i >= 0; i--)
                                {
                                    ArrayCell(row, col, pi, i, ref rowIdx);
                                }
                            }
                        }
                    }
                }
                if (Product != null)
                {
                    OnPrice(ProductCollection.CurrentPID, ProductCollection.Selected.TickName, ProductCollection.CurrentMarketPrice);
                }
                return m_TickList;
            }
        }
        #region Functional
        private CHeaderCell m_cDelete_Buy;
        private CHeaderCell m_cDelete;
        private CHeaderCell m_cDelete_Sell;

        [Cell(RowType.Functional, ColumnType.Buy)]
        public CHeaderCell cDelete_Buy { get { return m_cDelete_Buy; } }
        [Cell(RowType.Functional, ColumnType.Price)]
        public CHeaderCell cDelete { get { return m_cDelete; } }
        [Cell(RowType.Functional, ColumnType.Sell)]
        public CHeaderCell cDelete_Sell { get { return m_cDelete_Sell; } }
        #endregion
        #region Statistics
        private TextCell m_cStatistics_Buy;
        private CHeaderCell m_cStatistics;
        private TextCell m_cStatistics_Sell;
        [Cell(RowType.Statistics, ColumnType.Buy)]
        public TextCell cStatistics_Buy { get { return m_cStatistics_Buy; } }
        [Cell(RowType.Statistics, ColumnType.Price)]
        public CHeaderCell cStatistics { get { return m_cStatistics; } }
        [Cell(RowType.Statistics, ColumnType.Sell)]
        public TextCell cStatistics_Sell { get { return m_cStatistics_Sell; } }
        #endregion
        #region Header
        private CHeaderCell m_cHeader_Buy;
        private CHeaderCell m_cHeader;
        private CHeaderCell m_cHeader_Sell;
        [Cell(RowType.Header, ColumnType.Buy)]
        public CHeaderCell cHeader_Buy { get { return m_cHeader_Buy; } }
        [Cell(RowType.Header, ColumnType.Price)]
        public CHeaderCell cHeader { get { return m_cHeader; } }
        [Cell(RowType.Header, ColumnType.Sell)]
        public CHeaderCell cHeader_Sell { get { return m_cHeader_Sell; } }
        #endregion
        #region BP
        private TextCell[] m_cBP_Buy;
        private TextCell[] m_cBP;
        private TextCell[] m_cBP_Sell;
        [Cell(RowType.BP, ColumnType.Buy)]
        public TextCell[] cBP_Buy { get { return m_cBP_Buy; } }
        [Cell(RowType.BP, ColumnType.Price)]
        public TextCell[] cBP { get { return m_cBP; } }
        [Cell(RowType.BP, ColumnType.Sell)]
        public TextCell[] cBP_Sell { get { return m_cBP_Sell; } }
        #endregion
        #region MP
        private TextCell m_cMP_Buy;
        private TextCell m_cMP;
        private TextCell m_cMP_Sell;
        [Cell(RowType.MP, ColumnType.Buy)]
        public TextCell cMP_Buy { get { return m_cMP_Buy; } }
        [Cell(RowType.MP, ColumnType.Price)]
        public TextCell cMP { get { return m_cMP; } }
        [Cell(RowType.MP, ColumnType.Sell)]
        public TextCell cMP_Sell { get { return m_cMP_Sell; } }
        #endregion
        #region AP
        private TextCell[] m_cAP_Buy;
        private TextCell[] m_cAP;
        private TextCell[] m_cAP_Sell;
        [Cell(RowType.AP, ColumnType.Buy)]
        public TextCell[] cAP_Buy { get { return m_cAP_Buy; } }
        [Cell(RowType.AP, ColumnType.Price)]
        public TextCell[] cAP { get { return m_cAP; } }
        [Cell(RowType.AP, ColumnType.Sell)]
        public TextCell[] cAP_Sell { get { return m_cAP_Sell; } }
        #endregion
        #endregion

        public Tick(Unit unit, int tickCount)
        {
            m_Unit = unit;
            TickCount = tickCount;
            InitCell();

            NotificationCenter.Instance.AddObserver(OnProductChanging, "PRODUCTCHANGING");
           
            Utility.Processor.OrderReplyEvent += OrderReplyEvent;
            Utility.Processor.MatchReplyEvent += OrderReplyEvent;
            ProductCollection.MarketPriceEvent += OnPrice;
        }
        protected override void DoDispose()
        {
            ProductCollection.MarketPriceEvent -= OnPrice;
            Utility.Processor.OrderReplyEvent -= OrderReplyEvent;
            Utility.Processor.MatchReplyEvent -= OrderReplyEvent;
            NotificationCenter.Instance.RemoveObserver(OnProductChanging, "PRODUCTCHANGING");
            
        }

        #region Delegate
        /// <summary>
        /// 收到回報, 更新在途資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="reply"></param>
        private void OrderReplyEvent(SinoPacProcessor sender, SinoPacRPT reply)
        {
            UpdateAllStatus();
        }
        /// <summary>
        /// 更換商品, 清空價格
        /// </summary>
        /// <param name="n"></param>
        private void OnProductChanging(Notification n)
        {
            cMP.SetValue(0M);
            for (int i = 0; i < TickCount; i++)
            {
                cAP[i].SetValue(0M);
                cBP[i].SetValue(0M);
            }            
        }        
        /// <summary>
        /// 更新價格
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="price"></param>
        private void OnPrice(string productID,string tickname, decimal price)
        {
            cMP.SetValue(price);
            #region AP / BP
            decimal[] bp = ProductCollection.Selected.Getter.TickIncrement(tickname,price, TickCount);
            decimal[] ap = ProductCollection.Selected.Getter.TickDecrement(tickname,price, TickCount);
            for (int i = 0; i < TickCount; i++)
            {
                cBP[(TickCount-1)-i].SetValue(bp[i]);
                cAP[(TickCount-1)-i].SetValue(ap[i]);
            }
            UpdateAllStatus();
            #endregion
        }

        private void OnClick(CellBase sender, EventArgs e)
        {
            if (sender.Tag == null || Deal == null || Product == null) { return; }
            MouseEventArgs me = (MouseEventArgs)e;
            decimal price = (decimal)((TextCell)sender.Tag).Cell.Value;
            Side side = Side.B;
            if ((sender.Cell.Column.Index + 1) == (int)ColumnType.Sell)
            {
                side = Side.S;
            }
            if (me.Button == MouseButtons.Left)
            {
                Deal.Order(side, price);
            }
            else if (me.Button == MouseButtons.Right)
            {
                Deal.Cancel(side, price);
            }
        }
        #endregion

        #region Cell Function
        /// <summary>
        /// 建立欄位
        /// </summary>
        private void InitCell()
        {
            #region Functional
            CreateCell(ref m_cDelete_Buy, "刪買進", null, (sender, e) => { Deal.Cancel(Side.B); }, Color.DimGray, Color.White);
            CreateCell(ref m_cDelete, "全刪", null, (sender, e) => { Deal.Cancel(); }, Color.DimGray, Color.White);
            CreateCell(ref m_cDelete_Sell, "刪賣出", null, (sender, e) => { Deal.Cancel(Side.S); }, Color.DimGray, Color.White);
            #endregion
            #region Statistics
            CreateCell(ref m_cStatistics_Buy, "", "", -1, StatBC, null, null,
                (sender, e) =>
                {
                    if (((MouseEventArgs)e).Button != MouseButtons.Right) { return; }
                    Deal.Cancel(Side.B, MaxTick, MinTick);
                });
            CreateCell(ref m_cStatistics, "其他價格");
            CreateCell(ref m_cStatistics_Sell, "", "", -1, StatBC, null, null,
                (sender, e) =>
                {
                    if (((MouseEventArgs)e).Button != MouseButtons.Right) { return; }
                    Deal.Cancel(Side.S, MaxTick, MinTick);
                });
            #endregion
            #region Header
            CreateCell(ref m_cHeader_Buy, "買進");
            CreateCell(ref m_cHeader, "報價");
            CreateCell(ref m_cHeader_Sell, "賣出");
            #endregion
            #region MP (價格欄位要先處理)
            CreateCell(ref m_cMP, 0m, "", -1, null, Color.Orange, SelectPriceCell, null, false, "#,##0.000");
            CreateCell(ref m_cMP_Buy, "", "", -1, BuyBC, null, SelectPriceCell, OnClick, false, "", cMP);
            CreateCell(ref m_cMP_Sell, "", "", -1, SellBC, null, SelectPriceCell, OnClick, false, "", cMP);
            #endregion
            #region AP/BP (價格欄位要先處理)
            m_cBP_Buy = new TextCell[TickCount];
            m_cBP = new TextCell[TickCount];
            m_cBP_Sell = new TextCell[TickCount];
            m_cAP_Buy = new TextCell[TickCount];
            m_cAP = new TextCell[TickCount];
            m_cAP_Sell = new TextCell[TickCount];
            for (int i = 0; i < TickCount; i++)
            {
                #region BP
                CreateCell(ref m_cBP[i], 0M, "", -1, null, BuyFC, SelectPriceCell, null, false, "#,##0.000");
                CreateCell(ref m_cBP_Buy[i], "", "", -1, BuyBC, null, SelectPriceCell, OnClick, false, "", cBP[i]);
                CreateCell(ref m_cBP_Sell[i], "", "", -1, SellBC, null, SelectPriceCell, OnClick, false, "", cBP[i]);
                #endregion
                #region AP
                CreateCell(ref m_cAP[i], 0M, "", -1, null, SellFC, SelectPriceCell, null, false, "#,##0.000");
                CreateCell(ref m_cAP_Buy[i], "", "", -1, BuyBC, null, SelectPriceCell, OnClick, false, "", cAP[i]);
                CreateCell(ref m_cAP_Sell[i], "", "", -1, SellBC, null, SelectPriceCell, OnClick, false, "", cAP[i]);
                #endregion
            }
            #endregion
        }

        private void SingleCell(RowType row, ColumnType col, PropertyInfo pi, ref int rowIdx)
        {
            object cell = pi.GetValue(this, null);
            m_TickList.Add(new Position(rowIdx, (int)col - 1), cell == null ? null : ((CellBase)cell).Cell);
            rowIdx++;
        }
        private void ArrayCell(RowType row, ColumnType col, PropertyInfo pi, int arrayIdx, ref int rowIdx)
        {
            m_TickList.Add(new Position(rowIdx, (int)col - 1), (((TextCell[])pi.GetValue(this, null))[arrayIdx]).Cell);
            rowIdx++;
        }
        private void EmptyCell(RowType row, ColumnType col, ref int rowIdx)
        {
            TextCell c = new TextCell("", typeof(string)) { HasBorder = true, Border = Border, DefaultBackColor = Color.Silver };
            c.OnMouseEnter += new OnMouseEnterDelegate(SelectPriceCell);
            m_TickList.Add(new Position(rowIdx, (int)col - 1), c.Cell);
            rowIdx++;
        }
        private void SelectPriceCell(CellBase cell, EventArgs e)
        {
            Color? sel;
            if (cell.Cell.Column.Index < (int)ColumnType.Price - 1)
            {
                sel = BuyFC;
            }
            else if (cell.Cell.Column.Index == (int)ColumnType.Price - 1)
            {
                sel = null;
            }
            else
            {
                sel = SellFC;
            }
            cell.SelectCell(1, true, sel);
        }

        private void CreateCell(ref TextCell cell, object value, string binding = "", int bindingIndex = -1, Color? back = null, Color? font = null, OnMouseEnterDelegate onMouseEnter = null, OnClickDelegate onClick = null, bool enable = false, string format = "", object tag = null)
        {
            if (cell != null) return;
            Color bc = back == null ? Color.White : (Color)back;
            Color fc = font == null ? Color.Black : (Color)font;
            cell = new TextCell(value, value.GetType()) { DefaultBackColor = bc, DefaultFontColor = fc, Enable = enable, HasBorder = true, Border = Border, Format = format, FontName = CellBase.FontName.Verdana, FontSize = Utility.Load<float>("ORDERSETTING", "FONTSIZE"), Tag = tag };
            if (!String.IsNullOrEmpty(binding)) cell.SetDataBinding(this.GetType(), binding, this, bindingIndex);
            cell.OnClick += onClick;
            cell.OnMouseEnter += onMouseEnter;
        }
        private void CreateCell(ButtonCell cell, string caption, Color? font = null, OnClickDelegate onClick = null)
        {
            if (cell != null) return;
            Color fc = font == null ? Color.Black : (Color)font;
            cell = new ButtonCell() { Caption = caption, DefaultFontColor = fc };
            cell.OnClick += onClick;
        }
        private void CreateCell(ref CHeaderCell cell, string caption, OnDoubleClickDelegate onDoubleClick = null, OnClickDelegate onClick = null, Color? bc = null, Color? fc = null, int rowspan = 1, int colspan = 1)
        {
            if (cell != null) { return; }
            cell = new CHeaderCell() { Caption = caption, RowSpan = rowspan, ColumnSpan = colspan, DefaultBackColor = Color.LightGray, DefaultFontColor = Color.Black, FontSize = 8 };
            if (bc != null) { cell.BackColor = (Color)bc; }
            if (fc != null) { cell.FontColor = (Color)fc; }
            cell.OnDoubleClick += onDoubleClick;
            cell.OnClick += onClick;
        }
        #endregion

        /// <summary>
        /// 更新在途股數
        /// </summary>
        public void UpdateAllStatus()
        {
            if (Deal == null) { return; }
            foreach (RowType row in Enum.GetValues(typeof(RowType)))
            {
                if (row == RowType.None || row == RowType.Header || row == RowType.Functional /*|| row == RowType.Manual */|| row == RowType.Statistics)
                { continue; }//跳過沒Price的Row
                //抓出Price那個 Column
                var PriceProperty = this.GetType().GetProperties().FirstOrDefault(p => p.Row() == row && p.Col() == ColumnType.Price);
                if (PriceProperty == null) { continue; }	//沒Price Column, 跳過

                if (PriceProperty.PropertyType.BaseType == typeof(CellBase))	//Price 是單獨欄位
                {
                    CellBase PriceCell = (CellBase)PriceProperty.GetValue(this, null);
                    decimal price = (decimal)PriceCell.Cell.Value;
                    var OrderProperties = this.GetType().GetProperties().Where(p => p.Row() == row
                        && (p.Col() == ColumnType.Buy || p.Col() == ColumnType.Sell));
                    if (OrderProperties == null || OrderProperties.Count() == 0) { continue; }	//換下一Row
                    foreach (var OrderCell in OrderProperties)
                    {
                        if (OrderCell.Col() == ColumnType.Buy)
                        {
                            ((CellBase)OrderCell.GetValue(this, null)).SetValue(Deal.OrderStatus(Side.B, price));
                        }
                        else if (OrderCell.Col() == ColumnType.Sell)
                        {
                            ((CellBase)OrderCell.GetValue(this, null)).SetValue(Deal.OrderStatus(Side.S, price));
                        }
                    }
                }
                else if (PriceProperty.PropertyType.BaseType == typeof(Array))	//Price 是Array
                {
                    CellBase[] PriceCells = (CellBase[])PriceProperty.GetValue(this, null);
                    for (int i = 0; i < PriceCells.Length; i++)
                    {
                        decimal price = (decimal)PriceCells[i].Cell.Value;
                        var OrderProperties = this.GetType().GetProperties().Where(p => p.Row() == row
                        && (p.Col() == ColumnType.Buy || p.Col() == ColumnType.Sell));
                        if (OrderProperties == null || OrderProperties.Count() == 0) { continue; }	//換下一Row
                        foreach (var OrderCells in OrderProperties)
                        {
                            ((CellBase[])OrderCells.GetValue(this, null))[i].SetValue(Deal.OrderStatus(OrderCells.Col() == ColumnType.Buy ? Side.B : Side.S, price));
                        }
                    }
                }
            }
            cStatistics_Buy.SetValue(Deal.OrderStatus(Side.B, MaxTick, MinTick));
            cStatistics_Sell.SetValue(Deal.OrderStatus(Side.S, MaxTick, MinTick));
        }

        #region Public
        public void ToSetFontSize(decimal size)
        {
            foreach (var item in TickList.Values)
            {
                if (item.GetType().Name == "ColumnHeader") { continue; }
                Font orig = item.View.Font;
                Font f = new System.Drawing.Font(orig.Name, (float)size, orig.Style, orig.Unit);
                item.View.Font = f;
            }
        }
        #endregion
    }
}