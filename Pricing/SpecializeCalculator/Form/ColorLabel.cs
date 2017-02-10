using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace PriceCalculator
{
    public class ColorLabel : Label
    {
        #region Variable
        private List<string> m_TextList;
        private List<Color> m_ColorList;
        #endregion

        public ColorLabel()
            : base()
        {
            m_TextList = new List<string>();
            m_ColorList = new List<Color>();
        }

        public void AppendText(string text, Color color)
        {
            m_TextList.Add(text);
            m_ColorList.Add(color);
            base.Text = string.Concat(m_TextList.ToArray());         
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                //base.Text = value;
                m_TextList.Clear();
                m_ColorList.Clear();
                AppendText(value, Color.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {            
            float x = 0F;
            float y = 0F;
            for (int i = 0; i < m_TextList.Count; i++)
            {
                string txt = m_TextList[i];
                Color c = m_ColorList[i];
                using (Brush brush = new SolidBrush(c == Color.Empty ? ForeColor : c))
                {
                    StringFormat f = new StringFormat(StringFormat.GenericDefault);

                    f.FormatFlags |=  StringFormatFlags.MeasureTrailingSpaces;
                    e.Graphics.DrawString(txt, Font, brush, x, y);
                    x += e.Graphics.MeasureString(txt, Font, ClientSize.Width, f).Width;
                    if (txt.Contains("\r\n"))
                    {
                        x = 0F;
                        y += e.Graphics.MeasureString(txt, Font, ClientSize.Width, StringFormat.GenericTypographic).Height;
                    }
                    if (x > ClientSize.Width) { break; }
                }
            }
        }
    }
}
