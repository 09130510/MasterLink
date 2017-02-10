#define OPENUNTILCLOSE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notifier
{
    public partial class NotifyBox : Form
    {
        [DllImport("user32.dll")]
        private static extern Boolean ShowWindow(IntPtr hWnd, Int32 nCmdShow);

        public enum TaskbarStates
        {
            hidden = 0,
            appearing = 1,
            visible = 2,
            disappearing = 3
        }

        #region Variable
        private Bitmap m_BGImg = null;
        private Bitmap m_CImg = null;
        private Point m_CImgLocation;
        private Size m_CImgSize;
        private Rectangle RealTitleRectangle;
        private Rectangle RealContentRectangle;
        private Rectangle WorkAreaRectangle;
        private Timer m_Timer = new Timer();
        private TaskbarStates taskbarState = TaskbarStates.hidden;
        private string m_TitleText;
        private string m_ContentText;
        private Color normalTitleColor = Color.FromArgb(214, 38, 38);
        private Color hoverTitleColor = Color.FromArgb(214, 38, 38);
        private Color normalContentColor = Color.FromArgb(214, 38, 38);
        private Color hoverContentColor = Color.FromArgb(214, 38, 38);
        private Font normalTitleFont = new Font("Verdana", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        private Font hoverTitleFont = new Font("Verdana", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        private Font normalContentFont = new Font("Verdana", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        private Font hoverContentFont = new Font("Verdana", 14, FontStyle.Bold, GraphicsUnit.Pixel);
        private int nShowEvents;
        private int nHideEvents;
        private int nVisibleEvents;
        private int nIncrementShow;
        private int nIncrementHide;
        private int m_CurrX;
        private int m_CurrY;
        private bool m_WndMove = false;
        private bool bIsMouseOverPopup = false;
        private bool bIsMouseOverClose = false;
        private bool bIsMouseOverContent = false;
        private bool bIsMouseOverTitle = false;
        private bool bIsMouseDown = false;
        private bool bKeepVisibleOnMouseOver = true;
        private bool bReShowOnMouseOver = true;
        #endregion

        #region Property
        /// <summary>
        /// Get the current TaskbarState (hidden, showing, visible, hiding)
        /// </summary>
        public TaskbarStates TaskbarState { get { return taskbarState; } }

        /// <summary>
        /// Get/Set the popup Title Text
        /// </summary>
        public string TitleText
        {
            get { return m_TitleText; }
            set
            {
                m_TitleText = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the popup Content Text
        /// </summary>
        public string ContentText
        {
            get { return m_ContentText; }
            set
            {
                m_ContentText = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the Normal Title Color
        /// </summary>
        public Color NormalTitleColor
        {
            get { return normalTitleColor; }
            set
            {
                normalTitleColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the Hover Title Color
        /// </summary>
        public Color HoverTitleColor
        {
            get { return hoverTitleColor; }
            set
            {
                hoverTitleColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the Normal Content Color
        /// </summary>
        public Color NormalContentColor
        {
            get { return normalContentColor; }
            set
            {
                normalContentColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the Hover Content Color
        /// </summary>
        public Color HoverContentColor
        {
            get { return hoverContentColor; }
            set
            {
                hoverContentColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the Normal Title Font
        /// </summary>
        public Font NormalTitleFont
        {
            get { return normalTitleFont; }
            set
            {
                normalTitleFont = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the Hover Title Font
        /// </summary>
        public Font HoverTitleFont
        {
            get { return hoverTitleFont; }
            set
            {
                hoverTitleFont = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the Normal Content Font
        /// </summary>
        public Font NormalContentFont
        {
            get { return normalContentFont; }
            set
            {
                normalContentFont = value;
                Refresh();
            }
        }

        /// <summary>
        /// Get/Set the Hover Content Font
        /// </summary>
        public Font HoverContentFont
        {
            get { return hoverContentFont; }
            set
            {
                hoverContentFont = value;
                Refresh();
            }
        }

        /// <summary>
        /// Indicates if the popup should remain visible when the mouse pointer is over it.
        /// </summary>
        public bool KeepVisibleOnMousOver
        {
            get { return bKeepVisibleOnMouseOver; }
            set { bKeepVisibleOnMouseOver = value; }
        }

        /// <summary>
        /// Indicates if the popup should appear again when mouse moves over it while it's disappearing.        
        /// </summary>
        public bool ReShowOnMouseOver
        {
            get { return bReShowOnMouseOver; }
            set { bReShowOnMouseOver = value; }
        }

        public Rectangle TitleRectangle { get; set; }
        public Rectangle ContentRectangle { get; set; }
        public bool TitleClickable { get; set; } = false;
        public bool ContentClickable { get; set; } = true;
        public bool CloseClickable { get; set; } = true;
        public bool EnableSelectionRectangle { get; set; } = true;
        public event EventHandler TitleClick = null;
        public event EventHandler ContentClick = null;
        public event EventHandler CloseClick = null;
        #endregion

        public NotifyBox(IWin32Window owner)
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Minimized;
            base.Show(owner);
            base.Hide();
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = false;
            TopMost = true;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false;

            m_Timer.Enabled = true;
            m_Timer.Tick += new EventHandler(OnTimer);
        }

        #region Delegate
        protected void OnTimer(Object obj, EventArgs ea)
        {
            switch (taskbarState)
            {
                case TaskbarStates.appearing:
                    if (Height < m_BGImg.Height)
                        SetBounds(Left, Top - nIncrementShow, Width, Height + nIncrementShow);
                    else
                    {
                        m_Timer.Stop();
                        Height = m_BGImg.Height;
                        m_Timer.Interval = nVisibleEvents;
                        taskbarState = TaskbarStates.visible;
                        m_Timer.Start();
                    }
                    break;

                case TaskbarStates.visible:
#if OPENUNTILCLOSE
#else
                    timer.Stop();
                    timer.Interval = nHideEvents;
                    // Added Rev 002
                    if ((bKeepVisibleOnMouseOver && !bIsMouseOverPopup) || (!bKeepVisibleOnMouseOver))
                    {
                        taskbarState = TaskbarStates.disappearing;
                    }
                    //taskbarState = TaskbarStates.disappearing;		// Rev 002
                    timer.Start();
#endif
                    break;

                case TaskbarStates.disappearing:
#if OPENUNTILCLOSE
#else
                    // Added Rev 002
                    if (bReShowOnMouseOver && bIsMouseOverPopup)
                    {
                        taskbarState = TaskbarStates.appearing;
                    }
                    else
                    {
                        if (Top < WorkAreaRectangle.Bottom)
                            SetBounds(Left, Top + nIncrementHide, Width, Height - nIncrementHide);
                        else
                            Hide();
                    }
#endif
                    break;
            }

        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            bIsMouseOverPopup = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            bIsMouseOverPopup = false;
            bIsMouseOverClose = false;
            bIsMouseOverTitle = false;
            bIsMouseOverContent = false;
            Refresh();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool bContentModified = false;

            if ((e.X > m_CImgLocation.X) && (e.X < m_CImgLocation.X + m_CImgSize.Width) && (e.Y > m_CImgLocation.Y) && (e.Y < m_CImgLocation.Y + m_CImgSize.Height) && CloseClickable)
            {
                if (!bIsMouseOverClose)
                {
                    bIsMouseOverClose = true;
                    bIsMouseOverTitle = false;
                    bIsMouseOverContent = false;
                    Cursor = Cursors.Hand;
                    bContentModified = true;
                }
            }
            else if (RealContentRectangle.Contains(new Point(e.X, e.Y)) && ContentClickable)
            {
                if (!bIsMouseOverContent)
                {
                    bIsMouseOverClose = false;
                    bIsMouseOverTitle = false;
                    bIsMouseOverContent = true;
                    Cursor = Cursors.Hand;
                    bContentModified = true;
                }
            }
            else if (RealTitleRectangle.Contains(new Point(e.X, e.Y)) && TitleClickable)
            {
                if (!bIsMouseOverTitle)
                {
                    bIsMouseOverClose = false;
                    bIsMouseOverTitle = true;
                    bIsMouseOverContent = false;
                    Cursor = Cursors.Hand;
                    bContentModified = true;
                }
            }
            else
            {
                if (bIsMouseOverClose || bIsMouseOverTitle || bIsMouseOverContent)
                    bContentModified = true;

                bIsMouseOverClose = false;
                bIsMouseOverTitle = false;
                bIsMouseOverContent = false;
                Cursor = Cursors.Default;
            }

            if (m_WndMove)
            {
                Location = new Point(Left + e.X - m_CurrX, Top + e.Y - m_CurrY);
            }

            if (bContentModified)
                Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            bIsMouseDown = true;

            if (e.Button == MouseButtons.Left)
            {
                m_CurrX = e.X;
                m_CurrY = e.Y;
                m_WndMove = true;
            }
            if (bIsMouseOverClose)
                Refresh();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            bIsMouseDown = false;
            m_WndMove = false;

            if (bIsMouseOverClose)
            {
                Hide();

                CloseClick?.Invoke(this, new EventArgs());
            }
            else if (bIsMouseOverTitle)
            {
                TitleClick?.Invoke(this, new EventArgs());
            }
            else if (bIsMouseOverContent)
            {
                ContentClick?.Invoke(this, new EventArgs());
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            grfx.PageUnit = GraphicsUnit.Pixel;

            Graphics offScreenGraphics;
            Bitmap offscreenBitmap;

            offscreenBitmap = new Bitmap(m_BGImg.Width, m_BGImg.Height);
            offScreenGraphics = Graphics.FromImage(offscreenBitmap);

            if (m_BGImg != null)
            {
                offScreenGraphics.DrawImage(m_BGImg, 0, 0, m_BGImg.Width, m_BGImg.Height);
            }

            DrawCloseButton(offScreenGraphics);
            DrawText(offScreenGraphics);

            grfx.DrawImage(offscreenBitmap, 0, 0);
        }
        #endregion

        #region Public        
        /// <summary>
        /// Displays the popup for a certain amount of time
        /// </summary>
        /// <param name="strTitle">The string which will be shown as the title of the popup</param>
        /// <param name="strContent">The string which will be shown as the content of the popup</param>
        /// <param name="nTimeToShow">Duration of the showing animation (in milliseconds)</param>
        /// <param name="nTimeToStay">Duration of the visible state before collapsing (in milliseconds)</param>
        /// <param name="nTimeToHide">Duration of the hiding animation (in milliseconds)</param>
        /// <returns>Nothing</returns>
        public void Show(string strTitle, string strContent, int nTimeToShow, int nTimeToStay, int nTimeToHide)
        {
            WorkAreaRectangle = Screen.GetWorkingArea(WorkAreaRectangle);
            m_TitleText = strTitle;
            m_ContentText = strContent;
            nVisibleEvents = nTimeToStay;
            CalculateMouseRectangles();

            // We calculate the pixel increment and the timer value for the showing animation
            int nEvents;
            if (nTimeToShow > 10)
            {
                nEvents = Math.Min((nTimeToShow / 10), m_BGImg.Height);
                nShowEvents = nTimeToShow / nEvents;
                nIncrementShow = m_BGImg.Height / nEvents;
            }
            else
            {
                nShowEvents = 10;
                nIncrementShow = m_BGImg.Height;
            }

            // We calculate the pixel increment and the timer value for the hiding animation
            if (nTimeToHide > 10)
            {
                nEvents = Math.Min((nTimeToHide / 10), m_BGImg.Height);
                nHideEvents = nTimeToHide / nEvents;
                nIncrementHide = m_BGImg.Height / nEvents;
            }
            else
            {
                nHideEvents = 10;
                nIncrementHide = m_BGImg.Height;
            }

            switch (taskbarState)
            {
                case TaskbarStates.hidden:
                    taskbarState = TaskbarStates.appearing;
                    SetBounds(WorkAreaRectangle.Right - m_BGImg.Width - 17, WorkAreaRectangle.Bottom - 15, m_BGImg.Width, 0);
                    m_Timer.Interval = nShowEvents;
                    m_Timer.Start();
                    // We Show the popup without stealing focus
                    ShowWindow(this.Handle, 4);
                    break;

                case TaskbarStates.appearing:
                    Refresh();
                    break;

                case TaskbarStates.visible:
                    m_Timer.Stop();
                    m_Timer.Interval = nVisibleEvents;
                    m_Timer.Start();
                    Refresh();
                    break;

                case TaskbarStates.disappearing:
                    m_Timer.Stop();
                    taskbarState = TaskbarStates.visible;
                    SetBounds(WorkAreaRectangle.Right - m_BGImg.Width - 17, WorkAreaRectangle.Bottom - m_BGImg.Height - 15, m_BGImg.Width, m_BGImg.Height);
                    m_Timer.Interval = nVisibleEvents;
                    m_Timer.Start();
                    Refresh();
                    break;
            }
        }

        /// <summary>
        /// Hides the popup
        /// </summary>
        /// <returns>Nothing</returns>
        public new void Hide()
        {
            if (taskbarState != TaskbarStates.hidden)
            {
                m_Timer.Stop();
                taskbarState = TaskbarStates.hidden;
                base.Hide();
            }
        }

        /// <summary>
        /// Sets the background bitmap and its transparency color
        /// </summary>
        /// <param name="strFilename">Path of the Background Bitmap on the disk</param>
        /// <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
        /// <returns>Nothing</returns>
        public void SetBackgroundBitmap(string strFilename, Color transparencyColor)
        {
            m_BGImg = new Bitmap(strFilename);
            Width = m_BGImg.Width;
            Height = m_BGImg.Height;
            Region = BitmapToRegion(m_BGImg, transparencyColor);
        }

        /// <summary>
        /// Sets the background bitmap and its transparency color
        /// </summary>
        /// <param name="image">Image/Bitmap object which represents the Background Bitmap</param>
        /// <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
        /// <returns>Nothing</returns>
        public void SetBackgroundBitmap(Image image, Color transparencyColor)
        {
            m_BGImg = new Bitmap(image);
            Width = m_BGImg.Width;
            Height = m_BGImg.Height;
            Region = BitmapToRegion(m_BGImg, transparencyColor);
        }

        /// <summary>
        /// Sets the 3-State Close Button bitmap, its transparency color and its coordinates
        /// </summary>
        /// <param name="strFilename">Path of the 3-state Close button Bitmap on the disk (width must a multiple of 3)</param>
        /// <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
        /// <param name="position">Location of the close button on the popup</param>
        /// <returns>Nothing</returns>
        public void SetCloseBitmap(string strFilename, Color transparencyColor, Point position)
        {
            m_CImg = new Bitmap(strFilename);
            m_CImg.MakeTransparent(transparencyColor);
            m_CImgSize = new Size(m_CImg.Width / 3, m_CImg.Height);
            m_CImgLocation = position;
        }

        /// <summary>
        /// Sets the 3-State Close Button bitmap, its transparency color and its coordinates
        /// </summary>
        /// <param name="image">Image/Bitmap object which represents the 3-state Close button Bitmap (width must be a multiple of 3)</param>
        /// <param name="transparencyColor">Color of the Bitmap which won't be visible</param>
        /// /// <param name="position">Location of the close button on the popup</param>
        /// <returns>Nothing</returns>
        public void SetCloseBitmap(Image image, Color transparencyColor, Point position)
        {
            m_CImg = new Bitmap(image);
            m_CImg.MakeTransparent(transparencyColor);
            //m_CImgSize = new Size(m_CImg.Width, m_CImg.Height);
            m_CImgSize = new Size(m_CImg.Width/3, m_CImg.Height);
            m_CImgLocation = position;
        }
        #endregion

        #region Private
        private void DrawCloseButton(Graphics grfx)
        {
            if (m_CImg != null)
            {
                Rectangle rectDest = new Rectangle(m_CImgLocation, m_CImgSize);
                Rectangle rectSrc;

                if (bIsMouseOverClose)
                {
                    if (bIsMouseDown)
                        rectSrc = new Rectangle(new Point(m_CImgSize.Width * 2, 0), m_CImgSize);
                    else
                        rectSrc = new Rectangle(new Point(m_CImgSize.Width, 0), m_CImgSize);
                }
                else
                    rectSrc = new Rectangle(new Point(0, 0), m_CImgSize);


                grfx.DrawImage(m_CImg, rectDest, rectSrc, GraphicsUnit.Pixel);
            }
        }

        private void DrawText(Graphics grfx)
        {
            if (m_TitleText != null && m_TitleText.Length != 0)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.FitBlackBox;
                sf.Trimming = StringTrimming.EllipsisCharacter;             // Added Rev 002
                if (bIsMouseOverTitle)
                    grfx.DrawString(m_TitleText, hoverTitleFont, new SolidBrush(hoverTitleColor), TitleRectangle, sf);
                else
                    grfx.DrawString(m_TitleText, normalTitleFont, new SolidBrush(normalTitleColor), TitleRectangle, sf);
            }

            if (m_ContentText != null && m_ContentText.Length != 0)
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
                sf.Trimming = StringTrimming.Word;                          // Added Rev 002

                if (bIsMouseOverContent)
                {
                    grfx.DrawString(m_ContentText, hoverContentFont, new SolidBrush(hoverContentColor), ContentRectangle, sf);
                    if (EnableSelectionRectangle)
                        ControlPaint.DrawBorder3D(grfx, RealContentRectangle, Border3DStyle.Etched, Border3DSide.Top | Border3DSide.Bottom | Border3DSide.Left | Border3DSide.Right);

                }
                else
                    grfx.DrawString(m_ContentText, normalContentFont, new SolidBrush(normalContentColor), ContentRectangle, sf);
            }
        }

        private void CalculateMouseRectangles()
        {
            Graphics grfx = CreateGraphics();
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.MeasureTrailingSpaces;
            SizeF sizefTitle = grfx.MeasureString(m_TitleText, hoverTitleFont, TitleRectangle.Width, sf);
            SizeF sizefContent = grfx.MeasureString(m_ContentText, hoverContentFont, ContentRectangle.Width, sf);
            grfx.Dispose();

            // Added Rev 002
            //We should check if the title size really fits inside the pre-defined title rectangle
            if (sizefTitle.Height > TitleRectangle.Height)
            {
                RealTitleRectangle = new Rectangle(TitleRectangle.Left, TitleRectangle.Top, TitleRectangle.Width, TitleRectangle.Height);
            }
            else
            {
                RealTitleRectangle = new Rectangle(TitleRectangle.Left, TitleRectangle.Top, (int)sizefTitle.Width, (int)sizefTitle.Height);
            }
            RealTitleRectangle.Inflate(0, 2);

            // Added Rev 002
            //We should check if the Content size really fits inside the pre-defined Content rectangle
            if (sizefContent.Height > ContentRectangle.Height)
            {
                RealContentRectangle = new Rectangle((ContentRectangle.Width - (int)sizefContent.Width) / 2 + ContentRectangle.Left, ContentRectangle.Top, (int)sizefContent.Width, ContentRectangle.Height);
            }
            else
            {
                RealContentRectangle = new Rectangle((ContentRectangle.Width - (int)sizefContent.Width) / 2 + ContentRectangle.Left, (ContentRectangle.Height - (int)sizefContent.Height) / 2 + ContentRectangle.Top, (int)sizefContent.Width, (int)sizefContent.Height);
            }
            RealContentRectangle.Inflate(0, 2);
        }

        private Region BitmapToRegion(Bitmap bitmap, Color transparencyColor)
        {
            if (bitmap == null)
                throw new ArgumentNullException("Bitmap", "Bitmap cannot be null!");

            int height = bitmap.Height;
            int width = bitmap.Width;

            GraphicsPath path = new GraphicsPath();

            for (int j = 0; j < height; j++)
                for (int i = 0; i < width; i++)
                {
                    if (bitmap.GetPixel(i, j) == transparencyColor)
                        continue;

                    int x0 = i;

                    while ((i < width) && (bitmap.GetPixel(i, j) != transparencyColor))
                        i++;

                    path.AddRectangle(new Rectangle(x0, j, i - x0, 1));
                }

            Region region = new Region(path);
            path.Dispose();
            return region;
        }
        #endregion
    }
}