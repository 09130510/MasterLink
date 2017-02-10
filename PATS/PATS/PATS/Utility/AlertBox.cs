using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PATS.Utility
{
    public partial class AlertBox : Form
    {
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
        }
        private AlertBox(AlertBoxButton Button, string Caption, string Msg)
            : this(Button, Caption)
        {
            txtMsg.Visible = true;
            txtMsg.Text = Msg;
        }
        private AlertBox(AlertBoxButton Button, string Caption, params MsgLine[] MsgLines)
            : this(Button, Caption)
        {
            foreach (var line in MsgLines)
            {
                lstMsg.Items.Add(line.Show);
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
                    this.SetDesktopBounds(x, y, this.Size.Width, this.Size.Height);
                }
                else { this.StartPosition = FormStartPosition.CenterParent; }
            }
            else
            {
                this.StartPosition = FormStartPosition.CenterParent;
            }
            System.Media.SystemSounds.Beep.Play();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        #region For WordWrap
        private void lstMsg_MeasureItem(object sender, MeasureItemEventArgs e)
        {

        }

        private void lstMsg_DrawItem(object sender, DrawItemEventArgs e)
        {

        }
        #endregion

        #region For Window Moving
        private void btnTitle_MouseDown(object sender, MouseEventArgs e)
        {

        }
        private void btnTitle_MouseMove(object sender, MouseEventArgs e)
        {

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
            if (Owner== null)
            {
                return new AlertBox(Button, Caption, Msg).ShowDialog() == DialogResult.OK;
            }
            return Owner.InvokeIfRequired(() =>
            {
                return new AlertBox(Button, Caption, Msg).ShowDialog(Owner) == DialogResult.OK;
            });
            //if ( Owner.InvokeRequired)
            //{
            //    return (bool)Owner.Invoke(new MessageDelegate(Alert), Owner, Button, Caption, Msg);
            //}
            //else
            //{
            //    return new AlertBox(Button, Caption, Msg).ShowDialog(Owner) == DialogResult.OK;
            //}
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
            if (Owner == null)
            {
                new AlertBox(Button, Caption, Msg).ShowDialog();
                return;
            }
            Owner.InvokeIfRequired(() => { new AlertBox(Button, Caption, Msg).ShowDialog(Owner); });
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
            if (Owner == null)
            {
                return new AlertBox(Button, Caption, MsgLines).ShowDialog() == DialogResult.OK;
            }
            return Owner.InvokeIfRequired(() => { return new AlertBox(Button, Caption, MsgLines).ShowDialog(Owner) == DialogResult.OK; });            
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
            if (Owner == null)
            {
                new AlertBox(Button, Caption, MsgLines).ShowDialog();
                return;
            }
            Owner.InvokeIfRequired(() => { new AlertBox(Button, Caption, MsgLines).ShowDialog(Owner); }); 
        }
        #endregion
        
    }
    /// <summary>
	/// 多行訊息使用的訊息行
	/// </summary>
	public class MsgLine
    {
        #region Variable
        private string m_Caption;
        private string m_Message;
        #endregion

        #region Property
        /// <summary>
        /// 顯示的訊息
        /// </summary>
        public string Show
        {
            get
            {
                string split = string.IsNullOrEmpty(m_Message) ? string.Empty : "：";
                return $"{m_Caption}{split}{m_Message}";
            }
        }
        #endregion

        /// <summary>
        /// 訊息行
        /// </summary>
        /// <param name="Caption">訊息標題</param>
        /// <param name="Msg">訊息內容</param>
        public MsgLine(string Caption = "", string Msg = "")
        {
            m_Caption = Caption;
            m_Message = Msg;
        }
        /// <summary>
        /// 訊息行
        /// </summary>
        /// <param name="Caption">訊息標題</param>
        /// <param name="Msg">訊息內容</param>
        public MsgLine(object Caption, object Msg = null) : this(Caption.ToString(), Msg.ToString()) { }
    }
}
