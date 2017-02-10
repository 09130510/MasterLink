using System;
using System.Drawing;
using System.Windows.Forms;

namespace Capital.Report.Class
{	
	/// <summary>
	/// 自製 MessageBox
	/// </summary>
	public partial class AlertBox : Form
	{
		#region Delegate
		private delegate bool MessageDelegate(Form Owner, AlertBoxButton Button, string Caption, string Msg);
		private delegate void NRMessageDelegate(Form Owner, AlertBoxButton Button, string Caption, string Msg);
		private delegate bool MultiLineMessageDelegate(Form Owner, AlertBoxButton Button, string Caption, params MsgLine[] MsgLines);
		private delegate void NRMultiLineMessageDelegate(Form Owner, AlertBoxButton Button, string Caption, params MsgLine[] MsgLines);
		#endregion

		#region Variable
		private int m_CurrX;
		private int m_CurrY;
		private bool m_WndMove = false;
		#endregion

		private AlertBox(AlertBoxButton Button, string Caption)
		{
			InitializeComponent();
			btnTitle.Text = Caption;			
			switch (Button)
			{
				case AlertBoxButton.Msg_OK:
				case AlertBoxButton.Error_OK:
					btnOK.Width *= 2;
                    if (Button == AlertBoxButton.Error_OK) { btnTitle.ForeColor = Color.Crimson; }
					break;
				case AlertBoxButton.YesNo:
					btnOK.Text = "Yes";
					btnCancel.Text = "No";
					break;
				case AlertBoxButton.OKCancel:
				default:
					break;
			}
            btnOK.Focus();
		}
		private AlertBox(AlertBoxButton Button, string Caption, string Msg)
			: this(Button, Caption)
		{
            //txtMsg.Visible = true;
            //txtMsg.Text = Msg;
            lblMsg.Text = Msg;
		}
		private AlertBox(AlertBoxButton Button, string Caption, params MsgLine[] MsgLines)
			: this(Button, Caption)
		{
			foreach (var line in MsgLines)
			{
                //lstMsg.Items.Add(line.Show);
                lblMsg.AppendText(line.Caption + "：", Color.Black);//, new Font("微軟正黑體", 9, FontStyle.Bold));
                lblMsg.AppendText(line.Msg+"\r\n", line.MsgColor);
                //lblMsg.Text += line.Show + "\r\n";
            }			
		}

		private void AlertBox_Load(object sender, EventArgs e)
		{
			if (Owner != null)
			{
				Point cursorPosition = Owner.PointToClient(Cursor.Position);
				if ((cursorPosition.X <= Owner.ClientRectangle.Width && cursorPosition.Y <= Owner.ClientRectangle.Height))
				{
					//把OK Button剛好開在Mouse上, 算過位子的
					int x = MousePosition.X - 55 < 0 ? 0 : MousePosition.X - 55;
					int y = MousePosition.Y - 145 < 0 ? 0 : MousePosition.Y - 145;
                    SetDesktopBounds(x, y, this.Size.Width, this.Size.Height);
				}
				else { StartPosition = FormStartPosition.CenterParent; }
			}
			else
			{
                StartPosition = FormStartPosition.CenterParent;
			}
			System.Media.SystemSounds.Beep.Play();
		}
		private void btnOK_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		#region For WordWrap
		//private void lstMsg_MeasureItem(object sender, MeasureItemEventArgs e)
		//{
		//	e.ItemHeight = (int)e.Graphics.MeasureString(lstMsg.Items[e.Index].ToString(), lstMsg.Font, lstMsg.Width).Height;
		//}

		//private void lstMsg_DrawItem(object sender, DrawItemEventArgs e)
		//{
		//	e.DrawBackground();
		//	e.DrawFocusRectangle();
		//	e.Graphics.DrawString(lstMsg.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
		//}
		#endregion

		#region For Window Moving
		private void btnTitle_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				m_CurrX = e.X;
				m_CurrY = e.Y;
				m_WndMove = true;
			}
		}
		private void btnTitle_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_WndMove) { this.Location = new Point(this.Left + e.X - m_CurrX, this.Top + e.Y - m_CurrY); }
		}
		private void btnTitle_MouseUp(object sender, MouseEventArgs e)
		{
			m_WndMove = false;
		}
		#endregion

		#region Static Aleeeeeeeeeeeeeeeeeeeeeeeeert Function
		/// <summary>
		/// Show Message
		/// </summary>
		/// <param name="Owner">控制視窗位置用; 可為null</param>
		/// <param name="Button">Button Style</param>
		/// <param name="Caption">標題</param>
		/// <param name="Msg">訊息</param>
		/// <returns>User是否按下OK</returns>
		public static bool Alert(Form Owner, AlertBoxButton Button, string Caption, string Msg)
		{
			if (Owner != null && Owner.InvokeRequired)
			{
				return (bool)Owner.Invoke(new MessageDelegate(Alert),Owner, Button, Caption, Msg );
			}
			else
			{
				if (Owner == null)
				{
                    return new AlertBox(Button, Caption, Msg).ShowDialog() == DialogResult.OK;
				}
				else
                {
                    return new AlertBox(Button, Caption, Msg).ShowDialog(Owner) == DialogResult.OK;
				}
			}			
		}
		/// <summary>
		/// Show Message Without Reply
		/// </summary>
		/// <param name="Owner">控制視窗位置用; 可為null</param>
		/// <param name="Button">Button Style</param>
		/// <param name="Caption">標題</param>
		/// <param name="Msg">訊息</param>
		public static void AlertWithoutReply(Form Owner, AlertBoxButton Button, string Caption, string Msg)
		{
			if (Owner != null && Owner.InvokeRequired)
			{
				Owner.Invoke(new NRMessageDelegate(AlertWithoutReply), Owner, Button, Caption, Msg);
			}
			else
			{
				if (Owner == null)
				{
					new AlertBox(Button, Caption, Msg).ShowDialog();
				}
				else
				{
					new AlertBox(Button, Caption, Msg).ShowDialog(Owner);
				}				
			}
		}
		/// <summary>
		/// Show Message
		/// </summary>
		/// <param name="Owner">控制視窗位置用; 可為null</param>
		/// <param name="Button">Button Style</param>
		/// <param name="Caption">標題</param>
		/// <param name="MsgLines">多行訊息</param>
		/// <returns>User是否按下OK</returns>
		public static bool Alert(Form Owner, AlertBoxButton Button, string Caption, params MsgLine[] MsgLines)
		{
			if (Owner != null && Owner.InvokeRequired)
			{
				return (bool)Owner.Invoke(new MultiLineMessageDelegate(Alert), Owner, Button, Caption, MsgLines);
			}
			else
			{
				if (Owner == null)
				{
                    return new AlertBox(Button, Caption, MsgLines).ShowDialog() == DialogResult.OK;
				}
				else
				{
                    return new AlertBox(Button, Caption, MsgLines).ShowDialog(Owner) == DialogResult.OK;
				}
			}			
		}
		/// <summary>
		/// Show Message Without Reply
		/// </summary>
		/// <param name="Owner">控制視窗位置用; 可為null</param>
		/// <param name="Button">Button Style</param>
		/// <param name="Caption">標題</param>
		/// <param name="MsgLines">多行訊息</param>
		public static void AlertWithoutReply(Form Owner, AlertBoxButton Button, string Caption, params MsgLine[] MsgLines)
		{
			if (Owner != null && Owner.InvokeRequired)
			{
				Owner.BeginInvoke(new NRMultiLineMessageDelegate(AlertWithoutReply), Owner, Button, Caption, MsgLines);
			}
			else
			{
				if (Owner == null)
				{
					new AlertBox(Button, Caption, MsgLines).ShowDialog();
				}
				else
				{
					new AlertBox(Button, Caption, MsgLines).ShowDialog(Owner);
				}
			}
		}
        #endregion       
    }

    /// <summary>
    /// 多行訊息使用的訊息行
    /// </summary>
    public class MsgLine
    {        

        #region Property
        public string Caption { get; private set; }
        public string Msg { get; private set; }
        public Color MsgColor { get; private set; }
		/// <summary>
		/// 顯示的訊息
		/// </summary>
		public string Show { get { return $"{Caption}{(string.IsNullOrEmpty(Msg) ? string.Empty : "：")}{Msg}"; } }
		#endregion

		/// <summary>
		/// 訊息行
		/// </summary>
		/// <param name="Caption">訊息標題</param>
		/// <param name="Msg">訊息內容</param>
		public MsgLine(string caption = "", string msg = "", Color msgColor =  default(Color))
		{
			Caption = caption;
			Msg = msg;
            MsgColor = msgColor;
		}
		/// <summary>
		/// 訊息行
		/// </summary>
		/// <param name="Caption">訊息標題</param>
		/// <param name="Msg">訊息內容</param>
		public MsgLine(object Caption, object Msg = null, Color msgColor = default(Color)) : this(Caption.ToString(), Msg.ToString(), msgColor) { }
	}
}
