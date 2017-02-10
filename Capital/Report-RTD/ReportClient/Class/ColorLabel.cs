using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Capital.Report.Class
{
    public class ColorText
    {
        public string Text { get; private set; }
        public Color ForeColor { get; private set; }
        public Font Font { get; private set; }
        public ColorText(string text, Color fore = default(Color), Font font = null)
        {
            Text = text;
            ForeColor = fore;
            Font = font;
        }
    }
    public class ColorLabel : Label
    {
        private bool m_Growing;
        private List<ColorText> m_List;

        public int MaxWidth { get; set; }

        public ColorLabel()
            : base()
        {
            
            m_List = new List<ColorText>();
            AutoSize = false;
            SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
            ResizeRedraw = true;
            DoubleBuffered = true;
        }

        public void AppendText(string text, Color foreColor = default(Color), Font font = null)
        {
            m_List.Add(new ColorText(text, foreColor, font));
            base.Text = string.Concat(m_List.Select(e => e.Text).ToArray());
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                m_List.Clear();
                AppendText(value);
            }
        }                
        protected override void OnPaint(PaintEventArgs e)
        {   
            float x = 0F;
            float y = 0F;            
            for (int i = 0; i < m_List.Count; i++)
            {
                string txt = m_List[i].Text;
                Color c = m_List[i].ForeColor == default(Color) ? ForeColor : m_List[i].ForeColor;
                Font font = m_List[i].Font == null ? Font : m_List[i].Font;
                using (Brush brush = new SolidBrush(c))
                {
                    StringFormat f = new StringFormat(StringFormat.GenericDefault);

                    f.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                    
                    int breakpoint = _BreakPoint(e, x, f, font, txt);
                    while (breakpoint != -1)
                    {
                        string temp = txt.Substring(0, breakpoint);
                        txt = txt.Replace(temp, string.Empty);
                        e.Graphics.DrawString(temp, font, brush, x, y);
                        x = 0F;
                        
                        y += e.Graphics.MeasureString(temp, font, ClientSize.Width, StringFormat.GenericTypographic).Height;
                        breakpoint= _BreakPoint(e, x, f, font, txt);
                        
                    }
                    
                    e.Graphics.DrawString(txt, font, brush, x, y);
                    x += e.Graphics.MeasureString(txt, font, ClientSize.Width, f).Width;
                    
                    //e.Graphics.DrawString(txt, font, brush, x, y);
                    //x += e.Graphics.MeasureString(txt, font, ClientSize.Width, f).Width;
                    if (txt.Contains("\r\n"))
                    {
                        x = 0F;
                        y += e.Graphics.MeasureString(txt, font, ClientSize.Width, StringFormat.GenericTypographic).Height;                        
                    }
                    if (x > ClientSize.Width) { break; }
                }
            }
            _ResizeLabel();
            //
            //SetBounds(Location.X, Location.Y, MaxWidth, (int)y+25);

            //this.Refresh();

            //Parent.Refresh();
            //_ResizeLabel();
        }
        //protected override void OnClientSizeChanged(EventArgs e)
        //{

        //    base.OnClientSizeChanged(e);
        //    _ResizeLabel();
        //}
        //protected override void OnTextChanged(EventArgs e)
        //{   
        //    base.OnTextChanged(e);
        //    _ResizeLabel();
        //}
        

        private int _BreakPoint(PaintEventArgs e, float x, StringFormat format, Font font, string txt)
        {
            int breakpoint = -1;
            for (int j = 0; j < txt.Length; j++)
            {
                if (x + e.Graphics.MeasureString(txt.Substring(0, j), font, ClientSize.Width, format).Width >= MaxWidth)
                {
                    breakpoint = j-1 ;
                    break;
                }
            }
            return breakpoint;
        }
        private void _ResizeLabel()
        {
            if (m_Growing) return;
            try
            {
                m_Growing = true;
                Size sz = new Size(MaxWidth, Int32.MaxValue);
                sz = TextRenderer.MeasureText(this.Text, this.Font, sz, TextFormatFlags.WordBreak);
                
                this.Height = sz.Height;
                this.Width = sz.Width;
            }
            finally
            {
                m_Growing = false;
            }
        }
    }
}
