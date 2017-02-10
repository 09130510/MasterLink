using DevAge.Drawing;
using PATSOrder.Utility;
using SourceCell;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace PATSOrder.Class
{
    public class Row 
    {
        #region View
        public static RectangleBorder Border = RectangleBorder.CreateInsetBorder(1, Color.DimGray, Color.White);
        public static View EmptyView = new View(Color.Black, Color.White);
        public static View FuncView = new View(Color.DimGray, Color.White);
        public static View StatView = new View(Color.Black, Color.White);
        public static View HeaderView = new View(Color.LightGray, Color.Black);
        public static View BPView = new View(Color.Black, Color.White, Color.Crimson);
        public static View MPView = new View(Color.Black, Color.White, Color.Yellow, Color.Yellow, Color.Yellow);
        public static View APView = new View(Color.Black, Color.White, Color.LimeGreen);
        public static string VolumeFormat = "#,##0";
        #endregion Static

        #region Variable
        private string m_PriceFormat;
        private float m_FontSize;

        private CellBase m_Buy;
        private CellBase m_BL;
        private CellBase m_Price;
        private CellBase m_SL;
        private CellBase m_Sell;
        #endregion Variable

        #region Cell
        public CellBase this[ColGenre Column]
        {
            get
            {
                switch (Column)
                {
                    case ColGenre.Buy:
                        return m_Buy;
                    case ColGenre.BL:
                        return m_BL;
                    case ColGenre.Price:
                        return m_Price;
                    case ColGenre.SL:
                        return m_SL;
                    case ColGenre.Sell:
                        return m_Sell;
                }
                return null;
            }
        }
        public CellBase this[int Index]
        {
            get
            {
                return this[(ColGenre)Enum.Parse(typeof(ColGenre), Index.ToString())];
            }
        }
        #endregion Cell

        #region Property
        public RowGenre Genre { get; set; }
        public string PriceFormat
        {
            get { return m_PriceFormat; }
            set
            {
                if (value == m_PriceFormat) { return; }
                m_PriceFormat = value;                
                if (m_Price is TextCell)
                {
                    ((TextCell)m_Price).Format = value;
                }
            }
        }
        public float FontSize
        {
            get { return m_FontSize; }
            set
            {
                if (value == m_FontSize) { return; }
                m_FontSize = value;
                
                foreach (ColGenre col in Enum.GetValues(typeof(ColGenre)))
                {
                    if (this[col] is CHeaderCell) { continue; }
                    Font orig = this[col].Cell.View.Font;
                    this[col].Cell.View.Font = new Font(orig.Name, value, orig.Style, orig.Unit);
                }
            }
        }
        #endregion

        public Row(RowGenre genre)
        {
            Genre = genre;
            InitCell();

        }

        #region Public
        public void SetValue(ColGenre Column, object value)
        {
            switch (Column)
            {
                case ColGenre.Buy:
                    m_Buy.SetValue(value);
                    break;
                case ColGenre.BL:
                    m_BL.SetValue(value);
                    break;
                case ColGenre.Price:
                    m_Price.SetValue(value);
                    break;
                case ColGenre.SL:
                    m_SL.SetValue(value);
                    break;
                case ColGenre.Sell:
                    m_Sell.SetValue(value);
                    break;
            }
        }
        public void SetValue(int Index, object value)
        {
            switch (Index)
            {
                case (int)ColGenre.Buy:
                    m_Buy.SetValue(value);
                    break;
                case (int)ColGenre.BL:
                    m_BL.SetValue(value);
                    break;
                case (int)ColGenre.Price:
                    m_Price.SetValue(value);
                    break;
                case (int)ColGenre.SL:
                    m_SL.SetValue(value);
                    break;
                case (int)ColGenre.Sell:
                    m_Sell.SetValue(value);
                    break;
            }
        }
        public void SetValue(params object[] value)
        {
            if (value == null || value.Length <= 0) { return; }
            for (int i = 0; i < value.Length; i++)
            {
                SetValue(i, value[i]);
            }
        }

        public void ResetValue()
        {
            _ResetBuy();
            _ResetBL();
            _ResetPrice();
            _ResetSL();
            _ResetSell();
        }

        #endregion

        #region Cell Setting
        private void InitCell()
        {
            _InitBuy();
            _InitBL();
            _InitPrice();
            _InitSL();
            _InitSell();
        }
        private void _InitBuy()
        {
            m_Buy = null;
            View v = null;
            switch (Genre)
            {
                case RowGenre.Functional:
                    m_Buy = new CHeaderCell() { Caption = "刪買進" };
                    v = FuncView;
                    break;
                case RowGenre.Statistics:
                    m_Buy = new TextCell() { HasBorder = true, Border = Border };
                    v = StatView;
                    break;
                case RowGenre.Header:
                    m_Buy = new CHeaderCell() { Caption = "買" };
                    v = HeaderView;
                    break;
                case RowGenre.BP:
                    m_Buy = new TextCell() { HasBorder = true, Border = Border };
                    v = BPView;
                    break;
                case RowGenre.MP:
                    m_Buy = new TextCell() { HasBorder = true, Border = Border };
                    v = MPView;
                    break;
                case RowGenre.AP:
                    m_Buy = new TextCell() { HasBorder = true, Border = Border };
                    v = APView;
                    break;
            }

            m_Buy.SetBackColor(v.Buy_BackColor);
            m_Buy.SetFontColor(v.Buy_ForeColor);
            if (m_Buy is TextCell)
            {
                var cell = (TextCell)m_Buy;
                cell.CellType = TextCell.TextType.Int;
                cell.Format = VolumeFormat;
                cell.FontName = CellBase.FontName.Verdana;
            }
            if (Genre == RowGenre.AP || Genre == RowGenre.BP || Genre == RowGenre.MP)
            {
                m_Buy.OnMouseEnter += OnMouseEnter;
                m_Buy.OnClick += OnClick;
            }
        }
        private void _InitBL()
        {
            m_BL = null;
            View v = null;
            switch (Genre)
            {
                case RowGenre.Functional:
                case RowGenre.Statistics:
                case RowGenre.AP:
                    m_BL = new TextCell() { HasBorder = false, Border = Border };
                    v = EmptyView;
                    break;
                case RowGenre.Header:
                    m_BL = new CHeaderCell() { Caption = "買量" };
                    v = HeaderView;
                    break;
                case RowGenre.BP:
                    m_BL = new TextCell() { HasBorder = true, Border = Border };
                    v = BPView;
                    break;
                case RowGenre.MP:
                    m_BL = new TextCell() { HasBorder = true, Border = Border };
                    v = MPView;
                    break;
            }

            m_BL.SetBackColor(v.BL_BackColor);
            m_BL.SetFontColor(v.BL_ForeColor);
            if (m_BL is TextCell)
            {
                var cell = (TextCell)m_BL;
                cell.CellType = TextCell.TextType.Int;
                cell.Format = VolumeFormat;
                cell.FontName = CellBase.FontName.Verdana;
            }
            if (Genre == RowGenre.AP || Genre == RowGenre.BP || Genre == RowGenre.MP)
            {
                m_BL.OnMouseEnter += OnMouseEnter;
            }
        }
        private void _InitPrice()
        {
            m_Price = null;
            View v = null;
            switch (Genre)
            {
                case RowGenre.Functional:
                    m_Price = new CHeaderCell() { Caption = "全刪" };
                    v = FuncView;
                    break;
                case RowGenre.Statistics:
                    m_Price = new CHeaderCell() { Caption = "其他價格" };
                    v = HeaderView;
                    break;
                case RowGenre.Header:
                    m_Price = new CHeaderCell() { Caption = "價格" };
                    v = HeaderView;
                    break;
                case RowGenre.BP:
                    m_Price = new TextCell() { HasBorder = true, Border = Border };
                    v = BPView;
                    break;
                case RowGenre.MP:
                    m_Price = new TextCell() { HasBorder = true, Border = Border };
                    v = MPView;
                    break;
                case RowGenre.AP:
                    m_Price = new TextCell() { HasBorder = true, Border = Border };
                    v = APView;
                    break;
            }

            m_Price.SetBackColor(v.Price_BackColor);
            m_Price.SetFontColor(v.Price_ForeColor);
            if (m_Price is TextCell)
            {
                var cell = (TextCell)m_Price;
                cell.CellType = TextCell.TextType.Double;
                cell.Format = PriceFormat;
                cell.SetValue(0.0);
                cell.FontName = CellBase.FontName.Verdana;
            }
            if (Genre == RowGenre.AP || Genre == RowGenre.BP || Genre == RowGenre.MP)
            {
                m_Price.OnMouseEnter += OnMouseEnter;
            }
        }
        private void _InitSL()
        {
            m_SL = null;
            View v = null;
            switch (Genre)
            {
                case RowGenre.Functional:
                case RowGenre.Statistics:
                case RowGenre.BP:
                    m_SL = new TextCell() { HasBorder = false, Border = Border };
                    v = EmptyView;
                    break;
                case RowGenre.Header:
                    m_SL = new CHeaderCell() { Caption = "賣量" };
                    v = HeaderView;
                    break;
                case RowGenre.MP:
                    m_SL = new TextCell() { HasBorder = true, Border = Border };
                    v = MPView;
                    break;
                case RowGenre.AP:
                    m_SL = new TextCell() { HasBorder = true, Border = Border };
                    v = APView;
                    break;
            }

            m_SL.SetBackColor(v.SL_BackColor);
            m_SL.SetFontColor(v.SL_ForeColor);
            if (m_SL is TextCell)
            {
                var cell = (TextCell)m_SL;
                cell.CellType = TextCell.TextType.Int;
                cell.Format = VolumeFormat;
                cell.FontName = CellBase.FontName.Verdana;
            }
            if (Genre == RowGenre.AP || Genre == RowGenre.BP || Genre == RowGenre.MP)
            {
                m_SL.OnMouseEnter += OnMouseEnter;
            }
        }
        private void _InitSell()
        {
            m_Sell = null;
            View v = null;
            switch (Genre)
            {
                case RowGenre.Functional:
                    m_Sell = new CHeaderCell() { Caption = "刪賣出" };
                    v = FuncView;
                    break;
                case RowGenre.Statistics:
                    m_Sell = new TextCell() { HasBorder = true, Border = Border };
                    v = StatView;
                    break;
                case RowGenre.Header:
                    m_Sell = new CHeaderCell() { Caption = "賣" };
                    v = HeaderView;
                    break;
                case RowGenre.BP:
                    m_Sell = new TextCell() { HasBorder = true, Border = Border };
                    v = BPView;
                    break;
                case RowGenre.MP:
                    m_Sell = new TextCell() { HasBorder = true, Border = Border };
                    v = MPView;
                    break;
                case RowGenre.AP:
                    m_Sell = new TextCell() { HasBorder = true, Border = Border };
                    v = APView;
                    break;
            }

            m_Sell.SetBackColor(v.Sell_BackColor);
            m_Sell.SetFontColor(v.Sell_ForeColor);
            if (m_Sell is TextCell)
            {
                var cell = (TextCell)m_Sell;
                cell.CellType = TextCell.TextType.Int;
                cell.Format = VolumeFormat;
                cell.FontName = CellBase.FontName.Verdana;
            }
            if (Genre == RowGenre.AP || Genre == RowGenre.BP || Genre == RowGenre.MP)
            {
                m_Sell.OnMouseEnter += OnMouseEnter;
                m_Sell.OnClick += OnClick;
            }
        }

        private void _ResetBuy()
        {
            switch (Genre)
            {
                case RowGenre.Functional:
                case RowGenre.Header:
                    break;
                case RowGenre.Statistics:
                case RowGenre.BP:
                case RowGenre.MP:
                case RowGenre.AP:
                    m_Buy.SetValue("");
                    break;
            }
        }
        private void _ResetBL()
        {
            switch (Genre)
            {
                case RowGenre.Functional:
                case RowGenre.Statistics:
                case RowGenre.AP:
                case RowGenre.Header:
                    break;
                case RowGenre.BP:
                case RowGenre.MP:
                    m_BL.SetValue("");
                    break;
            }
        }
        private void _ResetPrice()
        {
            switch (Genre)
            {
                case RowGenre.Functional:
                case RowGenre.Statistics:
                case RowGenre.Header:
                    break;
                case RowGenre.BP:
                case RowGenre.MP:
                case RowGenre.AP:
                    m_Price.SetValue("0");
                    break;
            }
        }
        private void _ResetSL()
        {
            switch (Genre)
            {
                case RowGenre.Functional:
                case RowGenre.Statistics:
                case RowGenre.BP:
                case RowGenre.Header:
                    break;
                case RowGenre.MP:
                case RowGenre.AP:
                    m_SL.SetValue("");
                    break;
            }
        }
        private void _ResetSell()
        {
            switch (Genre)
            {
                case RowGenre.Functional:
                case RowGenre.Header:

                    break;
                case RowGenre.Statistics:
                case RowGenre.BP:
                case RowGenre.MP:
                case RowGenre.AP:
                    m_Sell.SetValue("");
                    break;
            }
        }
        #endregion

        #region Action
        private void OnMouseEnter(CellBase cell, EventArgs e)
        {
            Color color = MPView.Price_ForeColor;
            int colIndex = cell.Cell.Column.Index;
            int priceIndex = (int)ColGenre.Price;

            if (colIndex < priceIndex)
            {
                color = BPView.Price_ForeColor;
            }
            else if (colIndex > priceIndex)
            {
                color = APView.Price_ForeColor;
            }
            cell.SelectCell(priceIndex, true, color);
        }
        private void OnClick(CellBase cell, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            double price = Convert.ToDouble(((TextCell)m_Price).Value);

            int colIndex = cell.Cell.Column.Index;
            char bs = char.MinValue;
            if (colIndex == (int)ColGenre.Buy)
            {
                bs = 'B';
            }
            else if (colIndex == (int)ColGenre.Sell)
            {
                bs = 'S';
            }
            if (bs == char.MinValue) { return; }

            switch (me.Button)
            {
                case MouseButtons.Left:
                    Center.Instance.Post("LOG", $"Left {price} {bs}");
                    break;

                case MouseButtons.Right:
                    Center.Instance.Post("LOG", $"Right {price} {bs}");
                    break;
            }
        }
        #endregion
    }
}