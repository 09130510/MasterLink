namespace BLPServer
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIP = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRRPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPSPort);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(150, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ZMQ";
            // 
            // txtRRPort
            // 
            this.txtRRPort.Location = new System.Drawing.Point(62, 48);
            this.txtRRPort.Name = "txtRRPort";
            this.txtRRPort.Size = new System.Drawing.Size(74, 20);
            this.txtRRPort.TabIndex = 3;
            this.txtRRPort.Tag = "ZMQ;REPPORT;Text";
            this.txtRRPort.Validated += new System.EventHandler(this.txtPSPort_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "REP Port";
            // 
            // txtPSPort
            // 
            this.txtPSPort.Location = new System.Drawing.Point(63, 22);
            this.txtPSPort.Name = "txtPSPort";
            this.txtPSPort.Size = new System.Drawing.Size(74, 20);
            this.txtPSPort.TabIndex = 1;
            this.txtPSPort.Tag = "ZMQ;PUBPORT;Text";
            this.txtPSPort.Validated += new System.EventHandler(this.txtPSPort_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PUB Port";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtIP);
            this.groupBox2.Location = new System.Drawing.Point(3, 84);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 198);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Publish";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(3, 19);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(154, 173);
            this.txtIP.TabIndex = 2;
            this.txtIP.Tag = "REDIS;IP;Text";
            this.txtIP.Text = "";
            this.txtIP.Validated += new System.EventHandler(this.txtPSPort_Validated);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(172, 294);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(172, 294);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetting";
            this.Text = "Setting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtRRPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPSPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtIP;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}