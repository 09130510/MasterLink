namespace SKCOMTester
{
    partial class SKReply
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSignal = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listMessage = new System.Windows.Forms.ListBox();
            this.lblConnectID = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSignal);
            this.groupBox1.Location = new System.Drawing.Point(429, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(51, 54);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Signal";
            // 
            // lblSignal
            // 
            this.lblSignal.AutoSize = true;
            this.lblSignal.Font = new System.Drawing.Font("PMingLiU", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblSignal.ForeColor = System.Drawing.Color.Red;
            this.lblSignal.Location = new System.Drawing.Point(13, 18);
            this.lblSignal.Name = "lblSignal";
            this.lblSignal.Size = new System.Drawing.Size(32, 22);
            this.lblSignal.TabIndex = 0;
            this.lblSignal.Text = "●";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(328, 16);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(95, 37);
            this.btnDisconnect.TabIndex = 10;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(210, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 37);
            this.button1.TabIndex = 9;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listMessage
            // 
            this.listMessage.FormattingEnabled = true;
            this.listMessage.HorizontalScrollbar = true;
            this.listMessage.ItemHeight = 12;
            this.listMessage.Location = new System.Drawing.Point(3, 63);
            this.listMessage.Name = "listMessage";
            this.listMessage.Size = new System.Drawing.Size(813, 292);
            this.listMessage.TabIndex = 12;
            // 
            // lblConnectID
            // 
            this.lblConnectID.AutoSize = true;
            this.lblConnectID.Location = new System.Drawing.Point(31, 28);
            this.lblConnectID.Name = "lblConnectID";
            this.lblConnectID.Size = new System.Drawing.Size(17, 12);
            this.lblConnectID.TabIndex = 13;
            this.lblConnectID.Text = "ID";
            // 
            // SKReply
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblConnectID);
            this.Controls.Add(this.listMessage);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.button1);
            this.Name = "SKReply";
            this.Size = new System.Drawing.Size(834, 375);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblSignal;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listMessage;
        private System.Windows.Forms.Label lblConnectID;
    }
}
