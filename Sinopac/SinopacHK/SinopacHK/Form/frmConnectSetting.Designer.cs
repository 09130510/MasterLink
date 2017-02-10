namespace SinopacHK
{
    partial class frmConnectSetting
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuyCompID = new System.Windows.Forms.TextBox();
            this.txtSellCompID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTag1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSQLIP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDB = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSQLID = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buy ID";
            // 
            // txtBuyCompID
            // 
            this.txtBuyCompID.Location = new System.Drawing.Point(54, 19);
            this.txtBuyCompID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtBuyCompID.Name = "txtBuyCompID";
            this.txtBuyCompID.Size = new System.Drawing.Size(105, 20);
            this.txtBuyCompID.TabIndex = 1;
            this.txtBuyCompID.Tag = "SESSION;SenderCompID;Text";
            this.txtBuyCompID.Validated += new System.EventHandler(this.txtBuyCompID_Validated);
            // 
            // txtSellCompID
            // 
            this.txtSellCompID.Location = new System.Drawing.Point(54, 46);
            this.txtSellCompID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSellCompID.Name = "txtSellCompID";
            this.txtSellCompID.Size = new System.Drawing.Size(105, 20);
            this.txtSellCompID.TabIndex = 3;
            this.txtSellCompID.Tag = "SESSION;TargetCompID;Text";
            this.txtSellCompID.Validated += new System.EventHandler(this.txtBuyCompID_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sell ID";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(54, 73);
            this.txtIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(105, 20);
            this.txtIP.TabIndex = 5;
            this.txtIP.Tag = "DEFAULT;SocketConnectHost;Text";
            this.txtIP.Validated += new System.EventHandler(this.txtBuyCompID_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "IP";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(54, 100);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(105, 20);
            this.txtPort.TabIndex = 7;
            this.txtPort.Tag = "SESSION;SocketConnectPort;Text";
            this.txtPort.Validated += new System.EventHandler(this.txtBuyCompID_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 104);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Port";
            // 
            // txtTag1
            // 
            this.txtTag1.Location = new System.Drawing.Point(54, 127);
            this.txtTag1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtTag1.Name = "txtTag1";
            this.txtTag1.Size = new System.Drawing.Size(105, 20);
            this.txtTag1.TabIndex = 9;
            this.txtTag1.Tag = "ORDER;Account;Text";
            this.txtTag1.Validated += new System.EventHandler(this.txtBuyCompID_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 131);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Account";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBuyCompID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTag1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtSellCompID);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(165, 155);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下單設定";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtSQLID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtDB);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtSQLIP);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(7, 175);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(165, 129);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "成回SQL設定";
            // 
            // txtSQLIP
            // 
            this.txtSQLIP.Location = new System.Drawing.Point(64, 19);
            this.txtSQLIP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSQLIP.Name = "txtSQLIP";
            this.txtSQLIP.Size = new System.Drawing.Size(95, 20);
            this.txtSQLIP.TabIndex = 3;
            this.txtSQLIP.Tag = "MATCHSQL;IP;Text";
            this.txtSQLIP.Validated += new System.EventHandler(this.txtBuyCompID_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "IP";
            // 
            // txtDB
            // 
            this.txtDB.Location = new System.Drawing.Point(64, 45);
            this.txtDB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDB.Name = "txtDB";
            this.txtDB.Size = new System.Drawing.Size(95, 20);
            this.txtDB.TabIndex = 5;
            this.txtDB.Tag = "MATCHSQL;DB;Text";
            this.txtDB.Validated += new System.EventHandler(this.txtBuyCompID_Validated);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 49);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "DB";
            // 
            // txtSQLID
            // 
            this.txtSQLID.Location = new System.Drawing.Point(64, 71);
            this.txtSQLID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSQLID.Name = "txtSQLID";
            this.txtSQLID.Size = new System.Drawing.Size(95, 20);
            this.txtSQLID.TabIndex = 7;
            this.txtSQLID.Tag = "MATCHSQL;ID;Text";
            this.txtSQLID.Validated += new System.EventHandler(this.txtBuyCompID_Validated);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 75);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "ID";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(64, 97);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(95, 20);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.Tag = "MATCHSQL;PASSWORD;Text";
            this.txtPassword.Validated += new System.EventHandler(this.txtBuyCompID_Validated);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8F);
            this.label9.Location = new System.Drawing.Point(3, 101);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Password";
            // 
            // frmConnectSetting
            // 
            this.AutoHidePortion = 0.4D;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 314);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmConnectSetting";
            this.Text = "連線設定";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBuyCompID;
        private System.Windows.Forms.TextBox txtSellCompID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTag1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSQLID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSQLIP;
        private System.Windows.Forms.Label label6;
    }
}