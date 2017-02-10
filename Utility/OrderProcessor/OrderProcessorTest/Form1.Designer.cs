namespace OrderProcessorTest
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnOrder = new System.Windows.Forms.Button();
            this.cboTimeInForce = new System.Windows.Forms.ComboBox();
            this.cboSide = new System.Windows.Forms.ComboBox();
            this.cboOrderType = new System.Windows.Forms.ComboBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtExchange = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtClOrdID = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCurrency = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.txtOrigClOrdID = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnReplace = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(399, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "委託種類[40]";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(154, 37);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "買賣[54]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(170, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "TimeInForce[59]";
            // 
            // btnOrder
            // 
            this.btnOrder.Location = new System.Drawing.Point(441, 59);
            this.btnOrder.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnOrder.Name = "btnOrder";
            this.btnOrder.Size = new System.Drawing.Size(48, 25);
            this.btnOrder.TabIndex = 28;
            this.btnOrder.Text = "下單";
            this.btnOrder.UseVisualStyleBackColor = true;
            this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // cboTimeInForce
            // 
            this.cboTimeInForce.FormattingEnabled = true;
            this.cboTimeInForce.Items.AddRange(new object[] {
            "ROD",
            "IOC",
            "FOK"});
            this.cboTimeInForce.Location = new System.Drawing.Point(272, 5);
            this.cboTimeInForce.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboTimeInForce.Name = "cboTimeInForce";
            this.cboTimeInForce.Size = new System.Drawing.Size(57, 21);
            this.cboTimeInForce.TabIndex = 27;
            this.cboTimeInForce.Text = "ROD";
            // 
            // cboSide
            // 
            this.cboSide.FormattingEnabled = true;
            this.cboSide.Items.AddRange(new object[] {
            "B",
            "S"});
            this.cboSide.Location = new System.Drawing.Point(209, 33);
            this.cboSide.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboSide.Name = "cboSide";
            this.cboSide.Size = new System.Drawing.Size(57, 21);
            this.cboSide.TabIndex = 26;
            this.cboSide.Text = "B";
            // 
            // cboOrderType
            // 
            this.cboOrderType.FormattingEnabled = true;
            this.cboOrderType.Items.AddRange(new object[] {
            "Market",
            "Limit",
            "Stop",
            "StopLimit",
            "MarketOnClose"});
            this.cboOrderType.Location = new System.Drawing.Point(478, 33);
            this.cboOrderType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboOrderType.Name = "cboOrderType";
            this.cboOrderType.Size = new System.Drawing.Size(80, 21);
            this.cboOrderType.TabIndex = 25;
            this.cboOrderType.Text = "Limit";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(384, 5);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(66, 20);
            this.txtQty.TabIndex = 24;
            this.txtQty.Tag = "";
            this.txtQty.Text = "200";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(329, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "股數[38]";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(333, 33);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(66, 20);
            this.txtPrice.TabIndex = 22;
            this.txtPrice.Tag = "";
            this.txtPrice.Text = "24.65";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "委託價[44]";
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(88, 33);
            this.txtSymbol.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(66, 20);
            this.txtSymbol.TabIndex = 20;
            this.txtSymbol.Tag = "";
            this.txtSymbol.Text = "2827";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "商品代碼[55]";
            // 
            // txtAccount
            // 
            this.txtAccount.Enabled = false;
            this.txtAccount.Location = new System.Drawing.Point(57, 5);
            this.txtAccount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(113, 20);
            this.txtAccount.TabIndex = 18;
            this.txtAccount.Tag = "ORDER;Tag1;Text";
            this.txtAccount.Text = "714023400002";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "帳戶[1]";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDisconnect.Location = new System.Drawing.Point(455, 251);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(88, 25);
            this.btnDisconnect.TabIndex = 33;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConnect.Location = new System.Drawing.Point(367, 251);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(88, 25);
            this.btnConnect.TabIndex = 32;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 30F);
            this.lblStatus.ForeColor = System.Drawing.Color.Crimson;
            this.lblStatus.Location = new System.Drawing.Point(333, 232);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(32, 53);
            this.lblStatus.TabIndex = 34;
            this.lblStatus.Text = "●";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtExchange
            // 
            this.txtExchange.Location = new System.Drawing.Point(112, 61);
            this.txtExchange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtExchange.Name = "txtExchange";
            this.txtExchange.Size = new System.Drawing.Size(66, 20);
            this.txtExchange.TabIndex = 36;
            this.txtExchange.Tag = "";
            this.txtExchange.Text = "SEHK";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 65);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 13);
            this.label8.TabIndex = 35;
            this.label8.Text = "交易所[100, 207]";
            // 
            // txtClOrdID
            // 
            this.txtClOrdID.Enabled = false;
            this.txtClOrdID.Location = new System.Drawing.Point(254, 61);
            this.txtClOrdID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtClOrdID.Name = "txtClOrdID";
            this.txtClOrdID.Size = new System.Drawing.Size(66, 20);
            this.txtClOrdID.TabIndex = 38;
            this.txtClOrdID.Tag = "";
            this.txtClOrdID.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(178, 65);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 13);
            this.label9.TabIndex = 37;
            this.label9.Text = "ClordID[11]";
            // 
            // txtCurrency
            // 
            this.txtCurrency.Location = new System.Drawing.Point(375, 61);
            this.txtCurrency.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCurrency.Name = "txtCurrency";
            this.txtCurrency.Size = new System.Drawing.Size(66, 20);
            this.txtCurrency.TabIndex = 40;
            this.txtCurrency.Tag = "";
            this.txtCurrency.Text = "HKD";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(320, 65);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 13);
            this.label10.TabIndex = 39;
            this.label10.Text = "幣別[15]";
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 93);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(610, 147);
            this.listBox1.TabIndex = 41;
            // 
            // txtOrigClOrdID
            // 
            this.txtOrigClOrdID.Location = new System.Drawing.Point(550, 5);
            this.txtOrigClOrdID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOrigClOrdID.Name = "txtOrigClOrdID";
            this.txtOrigClOrdID.Size = new System.Drawing.Size(66, 20);
            this.txtOrigClOrdID.TabIndex = 43;
            this.txtOrigClOrdID.Tag = "";
            this.txtOrigClOrdID.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(450, 9);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 13);
            this.label11.TabIndex = 42;
            this.label11.Text = "OrigClordID[41]";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(489, 59);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(48, 25);
            this.btnCancel.TabIndex = 44;
            this.btnCancel.Text = "刪單";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // btnReplace
            // 
            this.btnReplace.Location = new System.Drawing.Point(537, 59);
            this.btnReplace.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnReplace.Name = "btnReplace";
            this.btnReplace.Size = new System.Drawing.Size(48, 25);
            this.btnReplace.TabIndex = 45;
            this.btnReplace.Text = "改單";
            this.btnReplace.UseVisualStyleBackColor = true;
            this.btnReplace.Click += new System.EventHandler(this.btnOrder_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 288);
            this.Controls.Add(this.btnReplace);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtOrigClOrdID);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtCurrency);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtClOrdID);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtExchange);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnOrder);
            this.Controls.Add(this.cboTimeInForce);
            this.Controls.Add(this.cboSide);
            this.Controls.Add(this.cboOrderType);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSymbol);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAccount);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnOrder;
        private System.Windows.Forms.ComboBox cboTimeInForce;
        private System.Windows.Forms.ComboBox cboSide;
        private System.Windows.Forms.ComboBox cboOrderType;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtExchange;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtClOrdID;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCurrency;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox txtOrigClOrdID;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnReplace;
    }
}

