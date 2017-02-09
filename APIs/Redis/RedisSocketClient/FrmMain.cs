using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace RedisSocket
{
    public partial class FrmMain : Form
    {
        public static string APName = "";                           //AP名稱
        public static FrmMain MainFrm = null;                           //AP名稱

        public FrmMain()
        {
            InitializeComponent();
            APName = this.ProductName;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            MainFrm = this;

            //啟動解析封包程序
            Thread threadP = new Thread(new ThreadStart(RedisSocket.InitialInfo));
            threadP.Priority = ThreadPriority.Highest;
            threadP.IsBackground = true;
            threadP.Start();
        }

        #region 外部Class變更顯示用的資訊 SetLabelCallback(string controlName, string text);
        delegate void SetLabelCallback(string controlName, string text);
        public void SetControlText(string controlName, string text)
        {
            if (Controls[controlName] != null)
            {
                Control control = Controls[controlName];
                //進行資料委派
                if (control != null)
                {
                    if (control.InvokeRequired)
                    {
                        SetLabelCallback d = new SetLabelCallback(SetControlText);
                        this.Invoke(d, new object[] { controlName, text });
                    }
                    else
                    {
                        RichTextBox AA = (RichTextBox)control;

                        //text = text + "," + DateTime.Now.ToString("yyyy/MM/dd HH:mm:sssssss")+"\r\n";
                        text = text + "\r\n";
                        AA.AppendText(text);

                        //control.Text = text;
                    }
                }
            }
        }

        public void SetControlText2(string controlName, string text)
        {
            if (Controls[controlName] != null)
            {
                Control control = Controls[controlName];
                //進行資料委派
                if (control != null)
                {
                    if (control.InvokeRequired)
                    {
                        SetLabelCallback d = new SetLabelCallback(SetControlText2);
                        this.Invoke(d, new object[] { controlName, text });
                    }
                    else
                    {
                        control.Text = text;
                    }
                }
            }
        }
        #endregion
    }
}
