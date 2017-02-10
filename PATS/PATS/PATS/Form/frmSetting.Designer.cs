namespace PATS
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
            this.headerGroupBox1 = new DevAge.Windows.Forms.HeaderGroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMySQLTable = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMySQLDB = new System.Windows.Forms.TextBox();
            this.txtMySQLPwd = new System.Windows.Forms.TextBox();
            this.txtMySQLID = new System.Windows.Forms.TextBox();
            this.txtMySQLIP = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
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
            this.headerGroupBox1.SuspendLayout();
            this.hgbPATS.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.headerGroupBox1);
            this.flowLayoutPanel1.Controls.Add(this.hgbPATS);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(181, 445);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // headerGroupBox1
            // 
            this.headerGroupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.headerGroupBox1.Controls.Add(this.comboBox1);
            this.headerGroupBox1.Controls.Add(this.label1);
            this.headerGroupBox1.Controls.Add(this.txtMySQLTable);
            this.headerGroupBox1.Controls.Add(this.label15);
            this.headerGroupBox1.Controls.Add(this.txtMySQLDB);
            this.headerGroupBox1.Controls.Add(this.txtMySQLPwd);
            this.headerGroupBox1.Controls.Add(this.txtMySQLID);
            this.headerGroupBox1.Controls.Add(this.txtMySQLIP);
            this.headerGroupBox1.Controls.Add(this.label11);
            this.headerGroupBox1.Controls.Add(this.label12);
            this.headerGroupBox1.Controls.Add(this.label13);
            this.headerGroupBox1.Controls.Add(this.label14);
            this.headerGroupBox1.Font = new System.Drawing.Font("Verdana", 8F);
            this.headerGroupBox1.ForeColor = System.Drawing.Color.Blue;
            this.headerGroupBox1.Image = null;
            this.headerGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.headerGroupBox1.Name = "headerGroupBox1";
            this.headerGroupBox1.Size = new System.Drawing.Size(172, 156);
            this.headerGroupBox1.TabIndex = 2;
            this.headerGroupBox1.TabStop = false;
            this.headerGroupBox1.Text = "Deals to Excel DB";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "MYSQL",
            "MSSQL"});
            this.comboBox1.Location = new System.Drawing.Point(83, 24);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(83, 21);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.Tag = "DEALTOEXCELDB;SQLTYPE;Text";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(5, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "SQL Type";
            // 
            // txtMySQLTable
            // 
            this.txtMySQLTable.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtMySQLTable.ForeColor = System.Drawing.Color.Green;
            this.txtMySQLTable.Location = new System.Drawing.Point(68, 133);
            this.txtMySQLTable.Name = "txtMySQLTable";
            this.txtMySQLTable.Size = new System.Drawing.Size(98, 20);
            this.txtMySQLTable.TabIndex = 9;
            this.txtMySQLTable.Tag = "DEALTOEXCELDB;Table;Text";
            this.txtMySQLTable.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 8F);
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(5, 137);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(38, 13);
            this.label15.TabIndex = 8;
            this.label15.Text = "Table";
            // 
            // txtMySQLDB
            // 
            this.txtMySQLDB.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtMySQLDB.ForeColor = System.Drawing.Color.Green;
            this.txtMySQLDB.Location = new System.Drawing.Point(68, 112);
            this.txtMySQLDB.Name = "txtMySQLDB";
            this.txtMySQLDB.Size = new System.Drawing.Size(98, 20);
            this.txtMySQLDB.TabIndex = 7;
            this.txtMySQLDB.Tag = "DEALTOEXCELDB;DB;Text";
            this.txtMySQLDB.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtMySQLPwd
            // 
            this.txtMySQLPwd.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtMySQLPwd.ForeColor = System.Drawing.Color.Green;
            this.txtMySQLPwd.Location = new System.Drawing.Point(68, 85);
            this.txtMySQLPwd.Name = "txtMySQLPwd";
            this.txtMySQLPwd.PasswordChar = '*';
            this.txtMySQLPwd.Size = new System.Drawing.Size(98, 20);
            this.txtMySQLPwd.TabIndex = 6;
            this.txtMySQLPwd.Tag = "DEALTOEXCELDB;PWD;Text";
            this.txtMySQLPwd.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtMySQLID
            // 
            this.txtMySQLID.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtMySQLID.ForeColor = System.Drawing.Color.Green;
            this.txtMySQLID.Location = new System.Drawing.Point(68, 65);
            this.txtMySQLID.Name = "txtMySQLID";
            this.txtMySQLID.Size = new System.Drawing.Size(98, 20);
            this.txtMySQLID.TabIndex = 5;
            this.txtMySQLID.Tag = "DEALTOEXCELDB;ID;Text";
            this.txtMySQLID.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // txtMySQLIP
            // 
            this.txtMySQLIP.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtMySQLIP.ForeColor = System.Drawing.Color.Green;
            this.txtMySQLIP.Location = new System.Drawing.Point(68, 45);
            this.txtMySQLIP.Name = "txtMySQLIP";
            this.txtMySQLIP.Size = new System.Drawing.Size(98, 20);
            this.txtMySQLIP.TabIndex = 4;
            this.txtMySQLIP.Tag = "DEALTOEXCELDB;IP;Text";
            this.txtMySQLIP.Validated += new System.EventHandler(this.txtSQLIP_Validated);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8F);
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(5, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "DB";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8F);
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(5, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "PWD";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8F);
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(5, 69);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(21, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "ID";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 8F);
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(5, 49);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "IP";
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
            this.hgbPATS.Location = new System.Drawing.Point(3, 165);
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
            this.ClientSize = new System.Drawing.Size(181, 445);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSetting";
            this.Text = "連線設定";
            this.DockStateChanged += new System.EventHandler(this.frmSetting_DockStateChanged);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.headerGroupBox1.ResumeLayout(false);
            this.headerGroupBox1.PerformLayout();
            this.hgbPATS.ResumeLayout(false);
            this.hgbPATS.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DevAge.Windows.Forms.HeaderGroupBox hgbPATS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPATSPwd;
        private System.Windows.Forms.TextBox txtPATSUser;
        private System.Windows.Forms.TextBox txtPATSPricePort;
        private System.Windows.Forms.TextBox txtPATSPriceIP;
        private System.Windows.Forms.TextBox txtPATSHostPort;
        private System.Windows.Forms.TextBox txtPATSHostIP;
        private System.Windows.Forms.Button btnResetPATS;
        private DevAge.Windows.Forms.HeaderGroupBox headerGroupBox1;
        private System.Windows.Forms.TextBox txtMySQLTable;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtMySQLDB;
        private System.Windows.Forms.TextBox txtMySQLPwd;
        private System.Windows.Forms.TextBox txtMySQLID;
        private System.Windows.Forms.TextBox txtMySQLIP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
    }
}