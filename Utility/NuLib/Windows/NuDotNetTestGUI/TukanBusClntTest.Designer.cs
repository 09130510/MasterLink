namespace NuDotNetTestGUI
{
    partial class TukanBusClntTest
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
            this.txtLoginInfo = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtBoard = new System.Windows.Forms.TextBox();
            this.btnSendGrp = new System.Windows.Forms.Button();
            this.btnReg = new System.Windows.Forms.Button();
            this.btnThdUp = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtTopic = new System.Windows.Forms.TextBox();
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // txtLoginInfo
            // 
            this.txtLoginInfo.Location = new System.Drawing.Point(12, 13);
            this.txtLoginInfo.Name = "txtLoginInfo";
            this.txtLoginInfo.Size = new System.Drawing.Size(165, 22);
            this.txtLoginInfo.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(192, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "起動";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(273, 13);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtBoard
            // 
            this.txtBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoard.Location = new System.Drawing.Point(12, 73);
            this.txtBoard.Multiline = true;
            this.txtBoard.Name = "txtBoard";
            this.txtBoard.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBoard.Size = new System.Drawing.Size(493, 219);
            this.txtBoard.TabIndex = 4;
            // 
            // btnSendGrp
            // 
            this.btnSendGrp.Location = new System.Drawing.Point(90, 46);
            this.btnSendGrp.Name = "btnSendGrp";
            this.btnSendGrp.Size = new System.Drawing.Size(75, 23);
            this.btnSendGrp.TabIndex = 5;
            this.btnSendGrp.Text = "SendGrp";
            this.btnSendGrp.UseVisualStyleBackColor = true;
            this.btnSendGrp.Click += new System.EventHandler(this.btnSendGrp_Click);
            // 
            // btnReg
            // 
            this.btnReg.Location = new System.Drawing.Point(171, 46);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(75, 23);
            this.btnReg.TabIndex = 6;
            this.btnReg.Text = "Reg";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // btnThdUp
            // 
            this.btnThdUp.Location = new System.Drawing.Point(354, 42);
            this.btnThdUp.Name = "btnThdUp";
            this.btnThdUp.Size = new System.Drawing.Size(75, 23);
            this.btnThdUp.TabIndex = 7;
            this.btnThdUp.Text = "ThdUp";
            this.btnThdUp.UseVisualStyleBackColor = true;
            this.btnThdUp.Click += new System.EventHandler(this.btnThdUp_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(435, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "ThdDown";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtTopic
            // 
            this.txtTopic.Location = new System.Drawing.Point(12, 46);
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.Size = new System.Drawing.Size(72, 22);
            this.txtTopic.TabIndex = 9;
            // 
            // chkShow
            // 
            this.chkShow.AutoSize = true;
            this.chkShow.Location = new System.Drawing.Point(261, 50);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(77, 16);
            this.chkShow.TabIndex = 10;
            this.chkShow.Text = "checkBox1";
            this.chkShow.UseVisualStyleBackColor = true;
            // 
            // TukanBusClnt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 304);
            this.Controls.Add(this.chkShow);
            this.Controls.Add(this.txtTopic);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnThdUp);
            this.Controls.Add(this.btnReg);
            this.Controls.Add(this.btnSendGrp);
            this.Controls.Add(this.txtBoard);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtLoginInfo);
            this.Name = "TukanBusClnt";
            this.Text = "TukanBusClnt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TukanBusClnt_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLoginInfo;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtBoard;
        private System.Windows.Forms.Button btnSendGrp;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.Button btnThdUp;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtTopic;
        private System.Windows.Forms.CheckBox chkShow;
    }
}