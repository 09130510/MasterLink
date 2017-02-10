using System.Drawing;

namespace PATSOrder.Class
{
    /// <summary>
    /// 顏色
    /// </summary>
    public class View
    {
        #region Property
        public Color Buy_BackColor { get; set; } = Color.Black;
        public Color Buy_ForeColor { get; set; } = Color.White;
        public Color BL_BackColor { get; set; } = Color.Black;
        public Color BL_ForeColor { get; set; } = Color.White;
        public Color Price_BackColor { get; set; } = Color.Black;
        public Color Price_ForeColor { get; set; } = Color.White;
        public Color SL_BackColor { get; set; } = Color.Black;
        public Color SL_ForeColor { get; set; } = Color.White;
        public Color Sell_BackColor { get; set; } = Color.Black;
        public Color Sell_ForeColor { get; set; } = Color.White;
        #endregion

        public View(Color? backColor = null, Color? foreColor = null)
        {
            if ((backColor ?? Color.Empty) != Color.Empty)
            {
                Buy_BackColor = BL_BackColor = Price_BackColor = SL_BackColor = Sell_BackColor = (Color)backColor;
            }
            if ((foreColor ?? Color.Empty) != Color.Empty)
            {
                Buy_ForeColor = BL_ForeColor = Price_ForeColor = SL_ForeColor = Sell_ForeColor = (Color)foreColor;
            }
        }
        public View(Color? backColor, Color? foreColor, Color? priceForeColor = null, Color? blForeColor = null, Color? slForeColor = null, Color? orderForeColor = null) : this(backColor, foreColor)
        {
            if ((priceForeColor ?? Color.Empty) != Color.Empty)
            {
                Price_ForeColor = (Color)priceForeColor;
            }
            if ((blForeColor ?? Color.Empty) != Color.Empty)
            {
                BL_ForeColor = (Color)blForeColor;
            }
            if ((slForeColor ?? Color.Empty) != Color.Empty)
            {
                SL_ForeColor = (Color)slForeColor;
            }
            if ((orderForeColor ?? Color.Empty) != Color.Empty)
            {
                Buy_ForeColor = Sell_ForeColor = (Color)orderForeColor;
            }
        }
    }
}