namespace NuDotNetTestGUI
{
    partial class TukanBusClient
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtBoard = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtLoginInfo = new System.Windows.Forms.TextBox();
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.txtTopic = new System.Windows.Forms.TextBox();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnReg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBoard
            // 
            this.txtBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoard.Location = new System.Drawing.Point(21, 88);
            this.txtBoard.Multiline = true;
            this.txtBoard.Name = "txtBoard";
            this.txtBoard.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBoard.Size = new System.Drawing.Size(482, 238);
            this.txtBoard.TabIndex = 8;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(282, 10);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 7;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(201, 10);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "起動";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtLoginInfo
            // 
            this.txtLoginInfo.Location = new System.Drawing.Point(21, 10);
            this.txtLoginInfo.Name = "txtLoginInfo";
            this.txtLoginInfo.Size = new System.Drawing.Size(165, 22);
            this.txtLoginInfo.TabIndex = 5;
            // 
            // chkShow
            // 
            this.chkShow.AutoSize = true;
            this.chkShow.Location = new System.Drawing.Point(363, 12);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(77, 16);
            this.chkShow.TabIndex = 11;
            this.chkShow.Text = "checkBox1";
            this.chkShow.UseVisualStyleBackColor = true;
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Location = new System.Drawing.Point(201, 43);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(75, 23);
            this.btnSendMsg.TabIndex = 12;
            this.btnSendMsg.Text = "Send";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // txtTopic
            // 
            this.txtTopic.Location = new System.Drawing.Point(21, 41);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Size = new System.Drawing.Size(84, 22);
            this.txtTopic.TabIndex = 13;
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(282, 44);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(198, 22);
            this.txtMsg.TabIndex = 14;
            // 
            // btnReg
            // 
            this.btnReg.Location = new System.Drawing.Point(111, 43);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(75, 23);
            this.btnReg.TabIndex = 15;
            this.btnReg.Text = "註冊";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // TukanBusClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 338);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.txtTopic);
            this.Controls.Add(this.btnSendMsg);
            this.Controls.Add(this.chkShow);
            this.Controls.Add(this.txtBoard);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtLoginInfo);
            this.Name = "TukanBusClient";
            this.Text = "TukanBusClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoard;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtLoginInfo;
        private System.Windows.Forms.CheckBox chkShow;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btnReg;
    }
}