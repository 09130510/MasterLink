namespace FTPTandem.UI
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lnkURL = new System.Windows.Forms.LinkLabel();
            this.dtTransDate = new System.Windows.Forms.DateTimePicker();
            this.butExecute = new System.Windows.Forms.Button();
            this.txtEmails = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.gbSQL = new System.Windows.Forms.GroupBox();
            this.txtSQLPassword = new System.Windows.Forms.TextBox();
            this.txtSQLID = new System.Windows.Forms.TextBox();
            this.txtSQLDB = new System.Windows.Forms.TextBox();
            this.txtSQLServer = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.gbFTP = new System.Windows.Forms.GroupBox();
            this.txtFTPFileName = new System.Windows.Forms.TextBox();
            this.txtFTPDirectory = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFTPPassword = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtFTPIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbLogs = new System.Windows.Forms.ListBox();
            this.butFTP = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbSQL.SuspendLayout();
            this.gbFTP.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.butFTP);
            this.splitContainer1.Panel1.Controls.Add(this.lnkURL);
            this.splitContainer1.Panel1.Controls.Add(this.dtTransDate);
            this.splitContainer1.Panel1.Controls.Add(this.butExecute);
            this.splitContainer1.Panel1.Controls.Add(this.txtEmails);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.gbSQL);
            this.splitContainer1.Panel1.Controls.Add(this.gbFTP);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lbLogs);
            this.splitContainer1.Size = new System.Drawing.Size(555, 300);
            this.splitContainer1.SplitterDistance = 176;
            this.splitContainer1.TabIndex = 0;
            // 
            // lnkURL
            // 
            this.lnkURL.AutoSize = true;
            this.lnkURL.Location = new System.Drawing.Point(478, 154);
            this.lnkURL.Name = "lnkURL";
            this.lnkURL.Size = new System.Drawing.Size(53, 12);
            this.lnkURL.TabIndex = 7;
            this.lnkURL.TabStop = true;
            this.lnkURL.Text = "檔案位置";
            this.lnkURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkURL_LinkClicked);
            // 
            // dtTransDate
            // 
            this.dtTransDate.CustomFormat = "yyyy/M/dd";
            this.dtTransDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTransDate.Location = new System.Drawing.Point(204, 149);
            this.dtTransDate.Name = "dtTransDate";
            this.dtTransDate.Size = new System.Drawing.Size(90, 22);
            this.dtTransDate.TabIndex = 6;
            this.dtTransDate.Value = new System.DateTime(2015, 8, 24, 16, 14, 0, 0);
            // 
            // butExecute
            // 
            this.butExecute.Location = new System.Drawing.Point(317, 149);
            this.butExecute.Name = "butExecute";
            this.butExecute.Size = new System.Drawing.Size(75, 23);
            this.butExecute.TabIndex = 5;
            this.butExecute.Text = "Execute";
            this.butExecute.UseVisualStyleBackColor = true;
            this.butExecute.Click += new System.EventHandler(this.butExecute_Click);
            // 
            // txtEmails
            // 
            this.txtEmails.Location = new System.Drawing.Point(386, 24);
            this.txtEmails.Multiline = true;
            this.txtEmails.Name = "txtEmails";
            this.txtEmails.Size = new System.Drawing.Size(161, 115);
            this.txtEmails.TabIndex = 4;
            this.txtEmails.Validated += new System.EventHandler(this.txtEmails_Validated);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(409, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "EMailTo:";
            // 
            // gbSQL
            // 
            this.gbSQL.Controls.Add(this.txtSQLPassword);
            this.gbSQL.Controls.Add(this.txtSQLID);
            this.gbSQL.Controls.Add(this.txtSQLDB);
            this.gbSQL.Controls.Add(this.txtSQLServer);
            this.gbSQL.Controls.Add(this.label9);
            this.gbSQL.Controls.Add(this.label8);
            this.gbSQL.Controls.Add(this.label7);
            this.gbSQL.Controls.Add(this.label6);
            this.gbSQL.Location = new System.Drawing.Point(204, 3);
            this.gbSQL.Name = "gbSQL";
            this.gbSQL.Size = new System.Drawing.Size(176, 140);
            this.gbSQL.TabIndex = 2;
            this.gbSQL.TabStop = false;
            this.gbSQL.Text = "SQL";
            // 
            // txtSQLPassword
            // 
            this.txtSQLPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FTPTandem.Properties.Settings.Default, "SQL_Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSQLPassword.Location = new System.Drawing.Point(64, 106);
            this.txtSQLPassword.Name = "txtSQLPassword";
            this.txtSQLPassword.Size = new System.Drawing.Size(100, 22);
            this.txtSQLPassword.TabIndex = 7;
            this.txtSQLPassword.Text = global::FTPTandem.Properties.Settings.Default.SQL_Password;
            // 
            // txtSQLID
            // 
            this.txtSQLID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FTPTandem.Properties.Settings.Default, "SQL_ID", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSQLID.Location = new System.Drawing.Point(64, 77);
            this.txtSQLID.Name = "txtSQLID";
            this.txtSQLID.Size = new System.Drawing.Size(100, 22);
            this.txtSQLID.TabIndex = 6;
            this.txtSQLID.Text = global::FTPTandem.Properties.Settings.Default.SQL_ID;
            // 
            // txtSQLDB
            // 
            this.txtSQLDB.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FTPTandem.Properties.Settings.Default, "SQL_DB", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSQLDB.Location = new System.Drawing.Point(64, 49);
            this.txtSQLDB.Name = "txtSQLDB";
            this.txtSQLDB.Size = new System.Drawing.Size(100, 22);
            this.txtSQLDB.TabIndex = 5;
            this.txtSQLDB.Text = global::FTPTandem.Properties.Settings.Default.SQL_DB;
            // 
            // txtSQLServer
            // 
            this.txtSQLServer.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FTPTandem.Properties.Settings.Default, "SQL_Server", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtSQLServer.Location = new System.Drawing.Point(64, 21);
            this.txtSQLServer.Name = "txtSQLServer";
            this.txtSQLServer.Size = new System.Drawing.Size(100, 22);
            this.txtSQLServer.TabIndex = 4;
            this.txtSQLServer.Text = global::FTPTandem.Properties.Settings.Default.SQL_Server;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "Password";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "ID";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "DB";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "Server";
            // 
            // gbFTP
            // 
            this.gbFTP.Controls.Add(this.txtFTPFileName);
            this.gbFTP.Controls.Add(this.txtFTPDirectory);
            this.gbFTP.Controls.Add(this.label5);
            this.gbFTP.Controls.Add(this.label4);
            this.gbFTP.Controls.Add(this.txtFTPPassword);
            this.gbFTP.Controls.Add(this.textBox1);
            this.gbFTP.Controls.Add(this.txtFTPIP);
            this.gbFTP.Controls.Add(this.label3);
            this.gbFTP.Controls.Add(this.label2);
            this.gbFTP.Controls.Add(this.label1);
            this.gbFTP.Location = new System.Drawing.Point(3, 3);
            this.gbFTP.Name = "gbFTP";
            this.gbFTP.Size = new System.Drawing.Size(195, 170);
            this.gbFTP.TabIndex = 1;
            this.gbFTP.TabStop = false;
            this.gbFTP.Text = "FTP";
            // 
            // txtFTPFileName
            // 
            this.txtFTPFileName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FTPTandem.Properties.Settings.Default, "FileName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFTPFileName.Location = new System.Drawing.Point(76, 139);
            this.txtFTPFileName.Name = "txtFTPFileName";
            this.txtFTPFileName.Size = new System.Drawing.Size(100, 22);
            this.txtFTPFileName.TabIndex = 9;
            this.txtFTPFileName.Text = global::FTPTandem.Properties.Settings.Default.FileName;
            // 
            // txtFTPDirectory
            // 
            this.txtFTPDirectory.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FTPTandem.Properties.Settings.Default, "FTPDirectory", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFTPDirectory.Location = new System.Drawing.Point(76, 109);
            this.txtFTPDirectory.Name = "txtFTPDirectory";
            this.txtFTPDirectory.Size = new System.Drawing.Size(100, 22);
            this.txtFTPDirectory.TabIndex = 8;
            this.txtFTPDirectory.Text = global::FTPTandem.Properties.Settings.Default.FTPDirectory;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 142);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "FileName";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "Directory";
            // 
            // txtFTPPassword
            // 
            this.txtFTPPassword.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FTPTandem.Properties.Settings.Default, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFTPPassword.Location = new System.Drawing.Point(76, 80);
            this.txtFTPPassword.Name = "txtFTPPassword";
            this.txtFTPPassword.Size = new System.Drawing.Size(100, 22);
            this.txtFTPPassword.TabIndex = 5;
            this.txtFTPPassword.Text = global::FTPTandem.Properties.Settings.Default.Password;
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FTPTandem.Properties.Settings.Default, "UserName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBox1.Location = new System.Drawing.Point(76, 52);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = global::FTPTandem.Properties.Settings.Default.UserName;
            // 
            // txtFTPIP
            // 
            this.txtFTPIP.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FTPTandem.Properties.Settings.Default, "FtpIP", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtFTPIP.Location = new System.Drawing.Point(76, 24);
            this.txtFTPIP.Name = "txtFTPIP";
            this.txtFTPIP.Size = new System.Drawing.Size(100, 22);
            this.txtFTPIP.TabIndex = 3;
            this.txtFTPIP.Text = global::FTPTandem.Properties.Settings.Default.FtpIP;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "UserName";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // lbLogs
            // 
            this.lbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLogs.FormattingEnabled = true;
            this.lbLogs.ItemHeight = 12;
            this.lbLogs.Location = new System.Drawing.Point(0, 0);
            this.lbLogs.Name = "lbLogs";
            this.lbLogs.Size = new System.Drawing.Size(555, 120);
            this.lbLogs.TabIndex = 0;
            // 
            // butFTP
            // 
            this.butFTP.Location = new System.Drawing.Point(398, 149);
            this.butFTP.Name = "butFTP";
            this.butFTP.Size = new System.Drawing.Size(75, 23);
            this.butFTP.TabIndex = 8;
            this.butFTP.Text = "FTP";
            this.butFTP.UseVisualStyleBackColor = true;
            this.butFTP.Click += new System.EventHandler(this.butFTP_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 300);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbSQL.ResumeLayout(false);
            this.gbSQL.PerformLayout();
            this.gbFTP.ResumeLayout(false);
            this.gbFTP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbLogs;
        private System.Windows.Forms.GroupBox gbSQL;
        private System.Windows.Forms.GroupBox gbFTP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFTPPassword;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtFTPIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFTPFileName;
        private System.Windows.Forms.TextBox txtFTPDirectory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSQLPassword;
        private System.Windows.Forms.TextBox txtSQLDB;
        private System.Windows.Forms.TextBox txtSQLServer;
        private System.Windows.Forms.TextBox txtEmails;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button butExecute;
        private System.Windows.Forms.TextBox txtSQLID;
        private System.Windows.Forms.DateTimePicker dtTransDate;
        private System.Windows.Forms.LinkLabel lnkURL;
        private System.Windows.Forms.Button butFTP;
    }
}

