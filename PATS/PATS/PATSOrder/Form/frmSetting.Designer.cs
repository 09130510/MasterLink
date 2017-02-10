namespace PATSOrder
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.hgbSQL = new DevAge.Windows.Forms.HeaderGroupBox();
            this.txtSQLDB = new System.Windows.Forms.TextBox();
            this.txtSQLPwd = new System.Windows.Forms.TextBox();
            this.txtSQLID = new System.Windows.Forms.TextBox();
            this.txtSQLIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hgbPATS = new DevAge.Windows.Forms.HeaderGroupBox();
            this.btnResetPATS = new System.Windows.Forms.Button();
            this.txtPATSPwd = new System.Windows.Forms.TextBox();
            this.txtPATSUser = new System.Windows.Forms.TextBox();
            this.txtPATSPricePort = new System.Windows.Forms.TextBox();
            this.txtPATSPriceIP = new System.Windows.Forms.TextBox();
            this.txtPATSHostPort = new System.Windows.Forms.TextBox();
            this.txtPATSHostIP = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.hgbSQL.SuspendLayout();
            this.hgbPATS.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.hgbSQL);
            this.flowLayoutPanel1.Controls.Add(this.hgbPATS);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(181, 417);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // hgbSQL
            // 
            this.hgbSQL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.hgbSQL.Controls.Add(this.txtSQLDB);
            this.hgbSQL.Controls.Add(this.txtSQLPwd);
            this.hgbSQL.Controls.Add(this.txtSQLID);
            this.hgbSQL.Controls.Add(this.txtSQLIP);
            this.hgbSQL.Controls.Add(this.label4);
            this.hgbSQL.Controls.Add(this.label3);
            this.hgbSQL.Controls.Add(this.label2);
            this.hgbSQL.Controls.Add(this.label1);
            this.hgbSQL.Font = new System.Drawing.Font("Verdana", 8F);
            this.hgbSQL.ForeColor = System.Drawing.Color.Blue;
            this.hgbSQL.Image = null;
            this.hgbSQL.Location = new System.Drawing.Point(3, 3);
            this.hgbSQL.Name = "hgbSQL";
            this.hgbSQL.Size = new System.Drawing.Size(172, 111);
            this.hgbSQL.TabIndex = 0;
            this.hgbSQL.TabStop = false;
            this.hgbSQL.Text = "SQL";
            // 
            // txtSQLDB
            // 
            this.txtSQLDB.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtSQLDB.ForeColor = System.Drawing.Color.Green;
            this.txtSQLDB.Location = new System.Drawing.Point(68, 84);
            this.txtSQLDB.Name = "txtSQLDB";
            this.txtSQLDB.Size = new System.Drawing.Size(98, 20);
            this.txtSQLDB.TabIndex = 7;
            this.txtSQLDB.Tag = "SQL;DB;Text";
            this.txtSQLDB.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtSQLPwd
            // 
            this.txtSQLPwd.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtSQLPwd.ForeColor = System.Drawing.Color.Green;
            this.txtSQLPwd.Location = new System.Drawing.Point(68, 58);
            this.txtSQLPwd.Name = "txtSQLPwd";
            this.txtSQLPwd.PasswordChar = '*';
            this.txtSQLPwd.Size = new System.Drawing.Size(98, 20);
            this.txtSQLPwd.TabIndex = 6;
            this.txtSQLPwd.Tag = "SQL;PWD;Text";
            this.txtSQLPwd.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtSQLID
            // 
            this.txtSQLID.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtSQLID.ForeColor = System.Drawing.Color.Green;
            this.txtSQLID.Location = new System.Drawing.Point(68, 38);
            this.txtSQLID.Name = "txtSQLID";
            this.txtSQLID.Size = new System.Drawing.Size(98, 20);
            this.txtSQLID.TabIndex = 5;
            this.txtSQLID.Tag = "SQL;ID;Text";
            this.txtSQLID.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtSQLIP
            // 
            this.txtSQLIP.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtSQLIP.ForeColor = System.Drawing.Color.Green;
            this.txtSQLIP.Location = new System.Drawing.Point(68, 18);
            this.txtSQLIP.Name = "txtSQLIP";
            this.txtSQLIP.Size = new System.Drawing.Size(98, 20);
            this.txtSQLIP.TabIndex = 4;
            this.txtSQLIP.Tag = "SQL;IP;Text";
            this.txtSQLIP.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8F);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(5, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "DB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 8F);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(5, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "PWD";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(5, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // hgbPATS
            // 
            this.hgbPATS.Controls.Add(this.btnResetPATS);
            this.hgbPATS.Controls.Add(this.txtPATSPwd);
            this.hgbPATS.Controls.Add(this.txtPATSUser);
            this.hgbPATS.Controls.Add(this.txtPATSPricePort);
            this.hgbPATS.Controls.Add(this.txtPATSPriceIP);
            this.hgbPATS.Controls.Add(this.txtPATSHostPort);
            this.hgbPATS.Controls.Add(this.txtPATSHostIP);
            this.hgbPATS.Controls.Add(this.label10);
            this.hgbPATS.Controls.Add(this.label9);
            this.hgbPATS.Controls.Add(this.label8);
            this.hgbPATS.Controls.Add(this.label7);
            this.hgbPATS.Controls.Add(this.label6);
            this.hgbPATS.Controls.Add(this.label5);
            this.hgbPATS.Font = new System.Drawing.Font("Verdana", 8F);
            this.hgbPATS.ForeColor = System.Drawing.Color.Blue;
            this.hgbPATS.Image = null;
            this.hgbPATS.Location = new System.Drawing.Point(3, 120);
            this.hgbPATS.Name = "hgbPATS";
            this.hgbPATS.Size = new System.Drawing.Size(172, 176);
            this.hgbPATS.TabIndex = 1;
            this.hgbPATS.TabStop = false;
            this.hgbPATS.Text = "PATS";
            // 
            // btnResetPATS
            // 
            this.btnResetPATS.Location = new System.Drawing.Point(115, 149);
            this.btnResetPATS.Name = "btnResetPATS";
            this.btnResetPATS.Size = new System.Drawing.Size(53, 21);
            this.btnResetPATS.TabIndex = 12;
            this.btnResetPATS.Text = "Reset";
            this.btnResetPATS.UseVisualStyleBackColor = true;
            this.btnResetPATS.Click += new System.EventHandler(this.btnResetPATS_Click);
            // 
            // txtPATSPwd
            // 
            this.txtPATSPwd.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtPATSPwd.ForeColor = System.Drawing.Color.Green;
            this.txtPATSPwd.Location = new System.Drawing.Point(68, 123);
            this.txtPATSPwd.Name = "txtPATSPwd";
            this.txtPATSPwd.Size = new System.Drawing.Size(98, 20);
            this.txtPATSPwd.TabIndex = 11;
            this.txtPATSPwd.Tag = "PATS;PWD;Text";
            this.txtPATSPwd.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtPATSUser
            // 
            this.txtPATSUser.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtPATSUser.ForeColor = System.Drawing.Color.Green;
            this.txtPATSUser.Location = new System.Drawing.Point(68, 103);
            this.txtPATSUser.Name = "txtPATSUser";
            this.txtPATSUser.Size = new System.Drawing.Size(98, 20);
            this.txtPATSUser.TabIndex = 10;
            this.txtPATSUser.Tag = "PATS;USER;Text";
            this.txtPATSUser.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtPATSPricePort
            // 
            this.txtPATSPricePort.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtPATSPricePort.ForeColor = System.Drawing.Color.Green;
            this.txtPATSPricePort.Location = new System.Drawing.Point(68, 77);
            this.txtPATSPricePort.Name = "txtPATSPricePort";
            this.txtPATSPricePort.Size = new System.Drawing.Size(98, 20);
            this.txtPATSPricePort.TabIndex = 9;
            this.txtPATSPricePort.Tag = "PATS;PRICEPORT;Text";
            this.txtPATSPricePort.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtPATSPriceIP
            // 
            this.txtPATSPriceIP.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtPATSPriceIP.ForeColor = System.Drawing.Color.Green;
            this.txtPATSPriceIP.Location = new System.Drawing.Point(68, 57);
            this.txtPATSPriceIP.Name = "txtPATSPriceIP";
            this.txtPATSPriceIP.Size = new System.Drawing.Size(98, 20);
            this.txtPATSPriceIP.TabIndex = 8;
            this.txtPATSPriceIP.Tag = "PATS;PRICEIP;Text";
            this.txtPATSPriceIP.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtPATSHostPort
            // 
            this.txtPATSHostPort.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtPATSHostPort.ForeColor = System.Drawing.Color.Green;
            this.txtPATSHostPort.Location = new System.Drawing.Point(68, 37);
            this.txtPATSHostPort.Name = "txtPATSHostPort";
            this.txtPATSHostPort.Size = new System.Drawing.Size(98, 20);
            this.txtPATSHostPort.TabIndex = 7;
            this.txtPATSHostPort.Tag = "PATS;HOSTPORT;Text";
            this.txtPATSHostPort.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtPATSHostIP
            // 
            this.txtPATSHostIP.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtPATSHostIP.ForeColor = System.Drawing.Color.Green;
            this.txtPATSHostIP.Location = new System.Drawing.Point(68, 17);
            this.txtPATSHostIP.Name = "txtPATSHostIP";
            this.txtPATSHostIP.Size = new System.Drawing.Size(98, 20);
            this.txtPATSHostIP.TabIndex = 6;
            this.txtPATSHostIP.Tag = "PATS;HOSTIP;Text";
            this.txtPATSHostIP.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8F);
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(5, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "PWD";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8F);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(5, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "USER";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8F);
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(5, 81);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Price Port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8F);
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(5, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Price IP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8F);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(5, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Host Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(5, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Host IP";
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(181, 417);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetting";
            this.Text = "設定";
            this.DockStateChanged += new System.EventHandler(this.frmSetting_DockStateChanged);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.hgbSQL.ResumeLayout(false);
            this.hgbSQL.PerformLayout();
            this.hgbPATS.ResumeLayout(false);
            this.hgbPATS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevAge.Windows.Forms.HeaderGroupBox hgbSQL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevAge.Windows.Forms.HeaderGroupBox hgbPATS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSQLDB;
        private System.Windows.Forms.TextBox txtSQLPwd;
        private System.Windows.Forms.TextBox txtSQLID;
        private System.Windows.Forms.TextBox txtSQLIP;
        private System.Windows.Forms.TextBox txtPATSPwd;
        private System.Windows.Forms.TextBox txtPATSUser;
        private System.Windows.Forms.TextBox txtPATSPricePort;
        private System.Windows.Forms.TextBox txtPATSPriceIP;
        private System.Windows.Forms.TextBox txtPATSHostPort;
        private System.Windows.Forms.TextBox txtPATSHostIP;
        private System.Windows.Forms.Button btnResetPATS;
    }
}