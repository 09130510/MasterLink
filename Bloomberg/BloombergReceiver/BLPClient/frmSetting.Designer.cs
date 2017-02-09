namespace BLPClient
{
    partial class frmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRRPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPSPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtRRPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPSPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 112);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ZMQ";
            // 
            // txtRRPort
            // 
            this.txtRRPort.Location = new System.Drawing.Point(101, 77);
            this.txtRRPort.Name = "txtRRPort";
            this.txtRRPort.Size = new System.Drawing.Size(74, 20);
            this.txtRRPort.TabIndex = 3;
            this.txtRRPort.Tag = "ZMQ;REQPORT;Text";
            this.txtRRPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRRPort.Validated += new System.EventHandler(this.txtPSPort_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "REQ Port";
            // 
            // txtPSPort
            // 
            this.txtPSPort.Location = new System.Drawing.Point(101, 51);
            this.txtPSPort.Name = "txtPSPort";
            this.txtPSPort.Size = new System.Drawing.Size(74, 20);
            this.txtPSPort.TabIndex = 1;
            this.txtPSPort.Tag = "ZMQ;SUBPORT;Text";
            this.txtPSPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPSPort.Validated += new System.EventHandler(this.txtPSPort_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SUB Port";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(50, 25);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(125, 20);
            this.txtIP.TabIndex = 5;
            this.txtIP.Tag = "ZMQ;IP;Text";
            this.txtIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtIP.Validated += new System.EventHandler(this.txtPSPort_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "IP";
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 290);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetting";
            this.Text = "設定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRRPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPSPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label3;
    }
}