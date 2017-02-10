using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevAge.Drawing;
using PriceProcessor.Capital;
using SourceCell;
using SourceGrid;
using SourceGrid.Cells;
using Util.Extension.Class;
using PriceProcessor;

namespace Capital.Report.Class
{
    ///<summary>
    ///換商品 => 重訂，不重建
    ///改TickCount, 改Expend => 重建
    ///</summary>
    public class Tick : NotifyDisposableClass
    {
        #region View
        private static RectangleBorder Border = RectangleBorder.CreateInsetBorder(1, Color.DimGray, Color.White);
        private static Color ABC = Color.Black;//Color.FromArgb(50, 0, 128, 255);
        private static Color AFC = Color.LimeGreen;
        private static Color AOBC = Color.Black;
        private static Color AOFC = Color.White;
        private static Color BBC = Color.Black;//Color.FromArgb(50, 255, 30, 142);        
        private static Color BFC = Color.Crimson;
        private static Color BOBC = Color.Black;
        private static Color BOFC = Color.White;
        private static Color BtnBC = Color.DimGray;
        private static Color BtnFC = Color.White;
        private static Color EmptyBC = Color.Black;
        private static Color MBC = Color.Black;
        private static Color MFC = Color.Yellow;
        private static Color StatBC = Color.Black;//Color.White;
        private static Color StatFC = Color.White;
        #endregion

        #region Variable
        private string m_PID;
        private short m_PageNo = -1;
        private bool m_Extend = false;
        private bool m_isAscending = true;

        private Dictionary<Position, ICell> m_TickList;
        private Dictionary<ICell, Dictionary<OrderProcessor.Side, CellBase>> m_OrderCell;
        private CHeaderCell m_cDelete_Buy;
        private CHeaderCell m_cDelete;
        private CHeaderCell m_cDelete_Sell;
        private TextCell m_cStatistics_Buy;
        private CHeaderCell m_cStatistics;
        private TextCell m_cStatistics_Sell;
        private CHeaderCell m_cHeader_Buy;
        private CHeaderCell m_cHeader_BLots;
        private CHeaderCell m_cHeader;
        private CHeaderCell m_cHeader_SLots;
        private CHeaderCell m_cHeader_Sell;
        private TextCell[] m_cBP_Buy;
        private TextCell[] m_cBP_BLots;
        private TextCell[] m_cBP;
        private TextCell[] m_cBP_Sell;
        private TextCell m_cMP_Buy;
        private TextCell m_cMP_BLots;
        private TextCell m_cMP;
        private TextCell m_cMP_SLots;
        private TextCell m_cMP_Sell;
        private TextCell[] m_cAP_Buy;
        private TextCell[] m_cAP;
        private TextCell[] m_cAP_SLots;
        private TextCell[] m_cAP_Sell;
        #endregion

        #region Property
        public string Account { get; set; }
        public string Exchange { get; set; }
        public string PID
        {
            get { return m_PID; }
            set
            {
                if (value == m_PID) { return; }
                Utility.Log(this, "Tick", $"變更商品:{m_PID}→{value}");
                m_PID = value;
                //清空Format
                if (cMP != null)
                {
                    cMP.Format = string.Empty;
                    for (int i = 0; i < TickCount; i++)
                    {
                        cBP[i].Format = string.Empty;
                        cAP[i].Format = string.Empty;
                    }
                }
                //訂閱
                Core.Instance.SubPrice(m_PageNo, m_PID);
            }
        }
        public string OrderHead { get; set; }
        public string YM { get; set; }
        public string YM2 { get; set; }
        public string OrderPID { get; set; }
        private int TickCount { get; set; }
        /// <summary>
        /// 最大顯示Tick
        /// </summary>
        private double MaxTick
        {
            get
            {
                if (TickCount == 0) { return (double)cMP.Value; }
                return (double)cAP[0].Value;
            }
        }
        /// <summary>
        /// 最小顯示Tick
        /// </summary>
        private double MinTick
        {
            get
            {
                if (TickCount == 0) { return (double)cMP.Value; }
                return (double)cBP[TickCount - 1].Value;
            }
        }
        /// <summary>
        /// Tick區的Row Count
        /// </summary>
        public int RowCnt { get { return TickCount * 2 + 4; } }
        /// <summary>
        /// Tick區的ColumnCount
        /// </summary>
        public int ColCnt { get { return 5; } }
        public bool isAscending
        {
            get { return m_isAscending; }
            set
            {
                if (value == m_isAscending) { return; }
                m_isAscending = value;

                if (m_isAscending)
                {
                    _SetCellAscending();
                }
                else
                {
                    _SetCellDescending();
                }
            }
        }
        #endregion

        #region TickCell        
        private void _InitOrderCell()
        {
            m_OrderCell = new Dictionary<ICell, Dictionary<OrderProcessor.Side, CellBase>>();
            m_OrderCell.Add(m_cMP.Cell, new Dictionary<OrderProcessor.Side, CellBase>()
            {
                [OrderProcessor.Side.B] = m_cMP_Buy,
                [OrderProcessor.Side.S] = m_cMP_Sell
            });
            for (int i = 0; i < TickCount; i++)
            {
                m_OrderCell.Add(m_cBP[i].Cell, new Dictionary<OrderProcessor.Side, CellBase>()
                {
                    [OrderProcessor.Side.B] = m_cBP_Buy[i],
                    [OrderProcessor.Side.S] = m_cBP_Sell[i]
                });
                m_OrderCell.Add(m_cAP[i].Cell, new Dictionary<OrderProcessor.Side, CellBase>()
                {
                    [OrderProcessor.Side.B] = m_cAP_Buy[i],
                    [OrderProcessor.Side.S] = m_cAP_Sell[i]
                });
            }
        }
        public Dictionary<Position, ICell> TickList
        {
            get
            {
                if (m_TickList != null) { return m_TickList; }

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
                            if (row != RowType.AP && row != RowType.BP)
                            {
                                EmptyCell(row, col, ref rowIdx);
                            }
                            else
                            {
                                for (int i = TickCount - 1; i >= 0; i--)
                                {
                                    EmptyCell(row, col, ref rowIdx);
                                }
                            }
                        }
                        else if (pi.PropertyType.BaseType == typeof(CellBase))
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
                _InitOrderCell();
                //建完Cell處理排序
                if (isAscending)
                {
                    _SetCellAscending();
                }
                else
                {
                    _SetCellDescending();
                }


                return m_TickList;
            }

        }
        #region Functional
        [Cell(RowType.Functional, ColumnType.Buy)]
        public CHeaderCell cDelete_Buy { get { return m_cDelete_Buy; } }
        [Cell(RowType.Functional, ColumnType.Price)]
        public CHeaderCell cDelete { get { return m_cDelete; } }
        [Cell(RowType.Functional, ColumnType.Sell)]
        public CHeaderCell cDelete_Sell { get { return m_cDelete_Sell; } }
        #endregion
        #region Statistics        
        [Cell(RowType.Statistics, ColumnType.Buy)]
        public TextCell cStatistics_Buy { get { return m_cStatistics_Buy; } }
        [Cell(RowType.Statistics, ColumnType.Price)]
        public CHeaderCell cStatistics { get { return m_cStatistics; } }
        [Cell(RowType.Statistics, ColumnType.Sell)]
        public TextCell cStatistics_Sell { get { return m_cStatistics_Sell; } }
        #endregion
        #region Header

        [Cell(RowType.Header, ColumnType.Buy)]
        public CHeaderCell cHeader_Buy { get { return m_cHeader_Buy; } }
        [Cell(RowType.Header, ColumnType.BLots)]
        public CHeaderCell cHeader_BLots { get { return m_cHeader_BLots; } }
        [Cell(RowType.Header, ColumnType.Price)]
        public CHeaderCell cHeader { get { return m_cHeader; } }
        [Cell(RowType.Header, ColumnType.SLots)]
        public CHeaderCell cHeader_SLots { get { return m_cHeader_SLots; } }
        [Cell(RowType.Header, ColumnType.Sell)]
        public CHeaderCell cHeader_Sell { get { return m_cHeader_Sell; } }
        #endregion
        #region BP        
        [Cell(RowType.BP, ColumnType.Buy)]
        public TextCell[] cBP_Buy { get { return m_cBP_Buy; } }
        [Cell(RowType.BP, ColumnType.BLots)]
        public TextCell[] cBP_BLots { get { return m_cBP_BLots; } }
        [Cell(RowType.BP, ColumnType.Price)]
        public TextCell[] cBP { get { return m_cBP; } }
        [Cell(RowType.BP, ColumnType.Sell)]
        public TextCell[] cBP_Sell { get { return m_cBP_Sell; } }
        #endregion
        #region MP        
        [Cell(RowType.MP, ColumnType.Buy)]
        public TextCell cMP_Buy { get { return m_cMP_Buy; } }
        [Cell(RowType.MP, ColumnType.BLots)]
        public TextCell cMP_BLots { get { return m_cMP_BLots; } }
        [Cell(RowType.MP, ColumnType.Price)]
        public TextCell cMP { get { return m_cMP; } }
        [Cell(RowType.MP, ColumnType.SLots)]
        public TextCell cMP_SLots { get { return m_cMP_SLots; } }
        [Cell(RowType.MP, ColumnType.Sell)]
        public TextCell cMP_Sell { get { return m_cMP_Sell; } }
        #endregion
        #region AP        
        [Cell(RowType.AP, ColumnType.Buy)]
        public TextCell[] cAP_Buy { get { return m_cAP_Buy; } }
        [Cell(RowType.AP, ColumnType.Price)]
        public TextCell[] cAP { get { return m_cAP; } }
        [Cell(RowType.AP, ColumnType.SLots)]
        public TextCell[] cAP_SLots { get { return m_cAP_SLots; } }
        [Cell(RowType.AP, ColumnType.Sell)]
        public TextCell[] cAP_Sell { get { return m_cAP_Sell; } }
        #endregion
        #endregion

        public Tick(short pageno, string account, string exchange, string pid, string orderhead, string ym, string ym2, int tickCount, bool extend, bool isascending)
        {
            m_PageNo = pageno;
            Account = account;
            Exchange = exchange;
            OrderHead = orderhead;
            YM = ym;
            YM2 = ym2;
            PID = pid;
            TickCount = tickCount;
            m_isAscending = isascending;
            m_Extend = extend;
            InitCell();

            Core.Instance.Order.OnOrderReply += new OrderProcessor.Capital.CapitalProcessor.OnReplyDelegate(OnOrderReply);
            Core.Instance.Order.OnMatchReply += new OrderProcessor.Capital.CapitalProcessor.OnReplyDelegate(OnOrderReply);
            Core.Instance.Order.OnCancelReply += new OrderProcessor.Capital.CapitalProcessor.OnReplyDelegate(OnOrderReply);
            Core.Instance.OnPriceChange += new CapitalProcessor.OnPriceChangeDelegate(OnPriceChange);

            OnPriceChange(pid, CapitalProcessor.TickName($"{Exchange},{OrderHead}"), Core.Instance.Price[PID]);
        }

        protected override void DoDispose()
        {
            Core.Instance.Order.OnOrderReply -= new OrderProcessor.Capital.CapitalProcessor.OnReplyDelegate(OnOrderReply);
            Core.Instance.Order.OnMatchReply -= new OrderProcessor.Capital.CapitalProcessor.OnReplyDelegate(OnOrderReply);
            Core.Instance.Order.OnCancelReply -= new OrderProcessor.Capital.CapitalProcessor.OnReplyDelegate(OnOrderReply);
            Core.Instance.OnPriceChange -= new CapitalProcessor.OnPriceChangeDelegate(OnPriceChange);
        }

        #region Delegate
        private void OnOrderReply(OrderProcessor.Capital.ReplyType ReplyType, OrderProcessor.Capital.Order Ord)
        {
            if (Ord.ComID != OrderPID) { return; }
            if (Ord.Price > MaxTick || Ord.Price < MinTick)
            {
                if (Ord.BuySell == OrderProcessor.Side.B)
                {
                    cStatistics_Buy.SetValue(Core.Instance.OrderStatus(OrderPID, OrderProcessor.Side.B, MaxTick, MinTick));
                }
                else
                {
                    cStatistics_Sell.SetValue(Core.Instance.OrderStatus(OrderPID, OrderProcessor.Side.S, MaxTick, MinTick));
                }
            }
            else
            {
                foreach (var cell in m_OrderCell.Keys)
                {
                    //if ((double)cell.Value != Ord.Price) { continue; }
                    //m_OrderCell[cell][Ord.BuySell].SetValue(Core.Instance.OrderStatus(OrderPID, Ord.BuySell, Ord.Price));
                    foreach (double price in Core.Instance.Order.PriceByOrderNo(Ord.ComID, Ord.OrdNo))
                    {
                        if (((double)cell.Value) == price)
                        {
                            m_OrderCell[cell][Ord.BuySell].SetValue(Core.Instance.OrderStatus(OrderPID, Ord.BuySell, price));
                        }
                    }
                }
            }
            //UpdateAllStatus();
        }
        private void OnPriceChange(string pid, string TickName, Price price)
        {
            if (pid != m_PID || price == null) { return; }
            _SetFormat(TickName, price.Market == 0D ? (decimal)price.Bid : (decimal)price.Market);
            //if (price.Denominator== -1)
            //{
                cMP.SetValue(price.Market);
            //}
            //else
            //{
            
                //cMP.SetValue($"{Price.MainNumber(price.Market)}'{(Price.SubNumber(price.Market) * price.Denominator)}");
            //}
            
            cMP_BLots.SetValue(price.TickQty);
            cMP_SLots.SetValue(price.TotalQty);
            #region AP / BP
            bool isUsingBP = price.Market == 0D || price.Market <= price.BestBid[0];
            decimal initBPrice = isUsingBP ? (decimal)price.BestBid[0] : (decimal)price.Market;
            var BPQ = m_Extend ?
                Processor.ExtendBidByTickCount(TickName, TickCount,initBPrice, isUsingBP,price.BestBid, price.BestBQty) : 
                Processor.BestBidByTickCount(TickName, TickCount,price.BestBid, price.BestBQty);

            bool isUsingAP = price.Market == 0D || (price.BestAsk[4] != 0D && price.Market >= price.BestAsk[4]);
            decimal initAPrice = isUsingAP ? (decimal)price.BestAsk[4] : (decimal)price.Market;
            var APQ = m_Extend ?
                Processor.ExtendAskByTickCount(TickName, TickCount, initAPrice, isUsingAP, price.BestAsk, price.BestAQty) :
                Processor.BestAskByTickCount(TickName, TickCount, price.BestAsk, price.BestAQty);
            //List<PQ> BPQ = m_Extend ? price.ExtendBidByTickCount(TickName, TickCount) : price.BestBidByTickCount(TickName, TickCount);
            //List<PQ> APQ = m_Extend ? price.ExtendAskByTickCount(TickName, TickCount) : price.BestAskByTickCount(TickName, TickCount);
            double[] bestb = BPQ.Select(e => e.Price).ToArray();
            double[] besta = APQ.Select(e => e.Price).ToArray();
            int[] bestbq = BPQ.Select(e => e.Qty).ToArray();
            int[] bestaq = APQ.Select(e => e.Qty).ToArray();

            if (isAscending)
            {
                _SetPriceByAscending(bestb, besta, bestbq, bestaq);
            }
            else
            {
                _SetPriceByDescending(bestb, besta, bestbq, bestaq);
            }
            if (m_OrderCell== null)
            {
                _InitOrderCell();
            }
            //Status
            foreach (var priceCell in m_OrderCell)
            {
                double statusPrice = (double)priceCell.Key.Value;
                foreach (var bs in priceCell.Value.Keys)
                {
                    m_OrderCell[priceCell.Key][bs].SetValue(Core.Instance.OrderStatus(OrderPID, bs, statusPrice));
                }
            }
            cStatistics_Buy.SetValue(Core.Instance.OrderStatus(OrderPID, OrderProcessor.Side.B, MaxTick, MinTick));
            cStatistics_Sell.SetValue(Core.Instance.OrderStatus(OrderPID, OrderProcessor.Side.S, MaxTick, MinTick));
            //UpdateAllStatus();
            #endregion
        }
        private void OnClick(CellBase sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            double price = (double)((TextCell)sender.Tag).Cell.Value;
            OrderProcessor.Side side = OrderProcessor.Side.B;
            if ((sender.Cell.Column.Index + 1) == (int)ColumnType.Sell)
            {
                side = OrderProcessor.Side.S;
            }
            if (me.Button == MouseButtons.Left)
            {
                Utility.Log(this, "Order", $"{side} Side: {price}");

                Core.Instance.DoOrder(Account, Exchange, OrderHead, YM, YM2, side, price);
            }
            else if (me.Button == MouseButtons.Right)
            {
                Utility.Log(this, "Cancel", $"{side} Side: {price}");
                Core.Instance.DoCancel(Account, OrderPID, side, price);
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
            CreateCell(ref m_cDelete_Buy, "刪買進", null,
                (sender, e) =>
                {
                    Utility.Log(this, "Cancel", "Buy Side");
                    Core.Instance.DoCancel(Account, OrderPID, OrderProcessor.Side.B);
                }
                , BtnBC, BtnFC);
            CreateCell(ref m_cDelete, "全刪", null,
                (sender, e) =>
                {
                    Utility.Log(this, "Cancel", "All");
                    Core.Instance.DoCancel(Account, OrderPID);
                }, BtnBC, BtnFC);
            CreateCell(ref m_cDelete_Sell, "刪賣出", null,
                (sender, e) =>
                {
                    Utility.Log(this, "Cancel", "Sell Side");
                    Core.Instance.DoCancel(Account, OrderPID, OrderProcessor.Side.S);
                }
                , BtnBC, BtnFC);
            #endregion
            #region Statistics
            CreateCell(ref m_cStatistics_Buy, "", "", -1, StatBC, StatFC, null,
                (sender, e) =>
                {
                    if (((MouseEventArgs)e).Button != MouseButtons.Right) { return; }
                    Utility.Log(this, "Cancel", $"Buy Side: <{MinTick} >{MaxTick}");
                    Core.Instance.DoCancel(Account, OrderPID, OrderProcessor.Side.B, MaxTick, MinTick);
                });
            CreateCell(ref m_cStatistics, "其他價格");
            CreateCell(ref m_cStatistics_Sell, "", "", -1, StatBC, StatFC, null,
                (sender, e) =>
                {
                    if (((MouseEventArgs)e).Button != MouseButtons.Right) { return; }
                    Utility.Log(this, "Cancel", $"Sell Side: <{MinTick} >{MaxTick}");
                    Core.Instance.DoCancel(Account, OrderPID, OrderProcessor.Side.S, MaxTick, MinTick);
                });
            #endregion
            #region Header
            CreateCell(ref m_cHeader_Buy, "買");
            CreateCell(ref m_cHeader_BLots, "買量");
            CreateCell(ref m_cHeader, "價格");
            CreateCell(ref m_cHeader_SLots, "賣量");
            CreateCell(ref m_cHeader_Sell, "賣");
            #endregion
            #region MP (價格欄位要先處理)
            CreateCell(ref m_cMP, 0D, "", -1, MBC, MFC, SelectPriceCell, null, false);
            CreateCell(ref m_cMP_Buy, "", "", -1, BOBC, BOFC, SelectPriceCell, OnClick, false, "", cMP);
            CreateCell(ref m_cMP_BLots, "", "", -1, MBC, MFC, SelectPriceCell, null, false, "", cMP);
            CreateCell(ref m_cMP_Sell, 0, "", -1, AOBC, AOFC, SelectPriceCell, OnClick, false, "#,###", cMP);
            CreateCell(ref m_cMP_SLots, 0, "", -1, MBC, MFC, SelectPriceCell, null, false, "#,###", cMP);
            #endregion
            #region AP/BP (價格欄位要先處理)
            m_cBP_Buy = new TextCell[TickCount];
            m_cBP_BLots = new TextCell[TickCount];
            m_cBP = new TextCell[TickCount];
            m_cBP_Sell = new TextCell[TickCount];
            m_cAP_Buy = new TextCell[TickCount];
            m_cAP = new TextCell[TickCount];
            m_cAP_SLots = new TextCell[TickCount];
            m_cAP_Sell = new TextCell[TickCount];
            for (int i = 0; i < TickCount; i++)
            {
                #region BP
                CreateCell(ref m_cBP[i], 0D, "", -1, BBC, BFC, SelectPriceCell, null, false);
                CreateCell(ref m_cBP_Buy[i], "", "", -1, BOBC, BOFC, SelectPriceCell, OnClick, false, "", cBP[i]);
                CreateCell(ref m_cBP_BLots[i], 0, "", -1, BBC, BOFC, SelectPriceCell, null, false, "#,###", cBP[i]);
                CreateCell(ref m_cBP_Sell[i], "", "", -1, AOBC, AOFC, SelectPriceCell, OnClick, false, "", cBP[i]);
                #endregion
                #region AP
                CreateCell(ref m_cAP[i], 0D, "", -1, BBC, AFC, SelectPriceCell, null, false);
                CreateCell(ref m_cAP_Buy[i], "", "", -1, BOBC, BOFC, SelectPriceCell, OnClick, false, "", cAP[i]);
                CreateCell(ref m_cAP_Sell[i], "", "", -1, AOBC, AOFC, SelectPriceCell, OnClick, false, "", cAP[i]);
                CreateCell(ref m_cAP_SLots[i], 0, "", -1, ABC, AOFC, SelectPriceCell, null, false, "#,###", cAP[i]);
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
            TextCell c = new TextCell("", typeof(string)) { HasBorder = true, Border = Border, DefaultBackColor = EmptyBC };
            c.OnMouseEnter += new OnMouseEnterDelegate(SelectPriceCell);
            m_TickList.Add(new Position(rowIdx, (int)col - 1), c.Cell);
            rowIdx++;
        }
        private void SelectPriceCell(CellBase cell, EventArgs e)
        {
            Color? sel;
            if (cell.Cell.Column.Index < (int)ColumnType.Price - 1)
            {
                sel = BFC;
            }
            else if (cell.Cell.Column.Index == (int)ColumnType.Price - 1)
            {
                sel = null;
            }
            else
            {
                sel = AFC;
            }
            cell.SelectCell(2, true, sel);
        }

        private void CreateCell(ref TextCell cell, object value, string binding = "", int bindingIndex = -1, Color? back = null, Color? font = null, OnMouseEnterDelegate onMouseEnter = null, OnClickDelegate onClick = null, bool enable = false, string format = "", object tag = null)
        {
            if (cell != null) return;
            Color bc = back == null ? Color.White : (Color)back;
            Color fc = font == null ? Color.Black : (Color)font;
            cell = new TextCell(value, value.GetType()) { DefaultBackColor = bc, DefaultFontColor = fc, Enable = enable, HasBorder = true, Border = Border, Format = format, FontName = CellBase.FontName.Verdana, FontSize = 8, Tag = tag };
            if (!string.IsNullOrEmpty(binding)) cell.SetDataBinding(this.GetType(), binding, this, bindingIndex);
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

        #region Public        
        public void ToSetFontSize(decimal size)
        {
            foreach (var item in TickList.Values)
            {
                if (item.GetType().Name == "ColumnHeader") { continue; }
                Font orig = item.View.Font;
                Font f = new Font(orig.Name, (float)size, orig.Style, orig.Unit);
                item.View.Font = f;
            }
            Utility.Log(this, "FontSize", size.ToString());
        }
        #endregion

        #region Private
        private void _SetFormat(string tickname, decimal price)
        {
            if (cMP.Format != string.Empty) { return; }
            decimal inTick, deTick;
            //Core.Instance.Price.Tick(tickname, price, out inTick, out deTick);
            PriceProcessor.Processor.Tick(tickname, price, out inTick, out deTick);
            if (inTick == 0M) { return; }
            string numStr = inTick.ToString();
            int idxOfDot = numStr.IndexOf('.');
            int DecimalCount = numStr.Length - (idxOfDot < 0 ? 0 : idxOfDot) - 1;

            string DecimalFormat = ".".PadRight(DecimalCount + 1, '0');
            string Format = "#,##0" + (DecimalFormat == "." ? "" : DecimalFormat);
            cMP.Format = Format;
            for (int i = 0; i < TickCount; i++)
            {
                cBP[i].Format = Format;
                cAP[i].Format = Format;
            }
        }
        private void _SetCellAscending()
        {
            if (m_TickList == null) { return; }
            int BPStartRow = (int)RowType.BP - 1;
            Position MarkPosition = new Position(BPStartRow, (int)ColumnType.Price - 1);
            ICell MarkCell = TickList[MarkPosition];
            if (MarkCell == cBP[TickCount - 1].Cell) { return; }

            for (int r = BPStartRow; r < BPStartRow + TickCount; r++)
            {
                for (int c = 0; c < ColCnt; c++)
                {
                    Position b = new Position(r, c);
                    Position a = new Position(r + TickCount + 1, c);
                    ICell TempCell = TickList[b];
                    TickList[b] = TickList[a];
                    TickList[a] = TempCell;
                }
            }
            Core.Instance.Price.Request(PID);
            Utility.Log(this, "Sorting", "Ascending");
        }
        private void _SetCellDescending()
        {
            if (m_TickList == null) { return; }
            int BPStartRow = (int)RowType.BP - 1;
            Position MarkPosition = new Position((int)RowType.BP - 1, (int)ColumnType.Price - 1);
            ICell MarkCell = TickList[MarkPosition];
            if (MarkCell != cBP[TickCount - 1].Cell) { return; }

            for (int r = BPStartRow; r < BPStartRow + TickCount; r++)
            {
                for (int c = 0; c < ColCnt; c++)
                {
                    Position b = new Position(r, c);
                    Position a = new Position(r + TickCount + 1, c);
                    ICell TempCell = TickList[b];
                    TickList[b] = TickList[a];
                    TickList[a] = TempCell;
                }
            }
            Core.Instance.Price.Request(PID);
            Utility.Log(this, "Sorting", "Descending");
        }
        private void _SetPriceByAscending(double[] bestb, double[] besta, int[] bestbq, int[] bestaq)
        {
            for (int i = 0; i < TickCount; i++)
            {
                cBP[i].SetValue(bestb[i]);
                cAP[i].SetValue(besta[i]);
                cBP_BLots[i].SetValue(bestbq[i] != 0 ? bestbq[i].ToString() : "");
                cAP_SLots[i].SetValue(bestaq[i] != 0 ? bestaq[i].ToString() : "");
            }
        }
        private void _SetPriceByDescending(double[] bestb, double[] besta, int[] bestbq, int[] bestaq)
        {
            for (int i = 0; i < TickCount; i++)
            {
                cBP[TickCount - 1 - i].SetValue(bestb[i]);
                cAP[TickCount - 1 - i].SetValue(besta[i]);
                cBP_BLots[TickCount - 1 - i].SetValue(bestbq[i] != 0 ? bestbq[i].ToString() : "");
                cAP_SLots[TickCount - 1 - i].SetValue(bestaq[i] != 0 ? bestaq[i].ToString() : "");
            }
        }
        #endregion
    }
}
