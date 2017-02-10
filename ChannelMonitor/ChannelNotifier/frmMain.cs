using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notifier
{
    public partial class frmMain : Form
    {
        private int m_CurrX;
        private int m_CurrY;
        private bool m_WndMove = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_DragDrop(object sender, DragEventArgs e)
        {
            this.SetBounds(Cursor.Position.X, Cursor.Position.Y, this.Width, this.Height);
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                m_CurrX = e.X;
                m_CurrY = e.Y;
                m_WndMove = true;
            }
        }

        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_WndMove) { this.Location = new Point(this.Left + e.X - m_CurrX, this.Top + e.Y - m_CurrY); }
        }

        private void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            m_WndMove = false;
        }
    }
}
