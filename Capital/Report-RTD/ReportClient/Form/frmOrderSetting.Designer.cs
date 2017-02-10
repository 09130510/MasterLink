namespace Capital.Report
{
    partial class frmOrderSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderSetting));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSell = new System.Windows.Forms.Button();
            this.lblLotsLimit = new System.Windows.Forms.Label();
            this.txtKeyNo = new System.Windows.Forms.TextBox();
            this.btnBuy = new System.Windows.Forms.Button();
            this.lblAmountLimit = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.nudManualPrice = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.chkSettingMode = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.nudLots = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtExchange = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOrderHead = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkOrderAlert = new System.Windows.Forms.CheckBox();
            this.nudLotsLimit = new System.Windows.Forms.NumericUpDown();
            this.btnQuickLots1 = new System.Windows.Forms.Button();
            this.btnQuickLots6 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnQuickLots5 = new System.Windows.Forms.Button();
            this.btnQuickLots4 = new System.Windows.Forms.Button();
            this.lblLots = new System.Windows.Forms.Label();
            this.btnQuickLots3 = new System.Windows.Forms.Button();
            this.btnQuickLots2 = new System.Windows.Forms.Button();
            this.txtAmountLimit = new System.Windows.Forms.TextBox();
            this.chkStopOrder = new System.Windows.Forms.CheckBox();
            this.txtOrderYM = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudManualPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLots)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLotsLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(123, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(38, 25);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "刪單";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSell
            // 
            this.btnSell.Enabled = false;
            this.btnSell.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnSell.Location = new System.Drawing.Point(111, 40);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(38, 25);
            this.btnSell.TabIndex = 20;
            this.btnSell.Text = "賣";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // lblLotsLimit
            // 
            this.lblLotsLimit.AutoSize = true;
            this.lblLotsLimit.Location = new System.Drawing.Point(9, 176);
            this.lblLotsLimit.Name = "lblLotsLimit";
            this.lblLotsLimit.Size = new System.Drawing.Size(91, 13);
            this.lblLotsLimit.TabIndex = 8;
            this.lblLotsLimit.Text = "自動口數上限：";
            // 
            // txtKeyNo
            // 
            this.txtKeyNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtKeyNo.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtKeyNo.Location = new System.Drawing.Point(9, 100);
            this.txtKeyNo.Name = "txtKeyNo";
            this.txtKeyNo.Size = new System.Drawing.Size(114, 20);
            this.txtKeyNo.TabIndex = 23;
            this.txtKeyNo.Tag = "";
            // 
            // btnBuy
            // 
            this.btnBuy.Enabled = false;
            this.btnBuy.ForeColor = System.Drawing.Color.Crimson;
            this.btnBuy.Location = new System.Drawing.Point(50, 39);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(38, 25);
            this.btnBuy.TabIndex = 15;
            this.btnBuy.Text = "買";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // lblAmountLimit
            // 
            this.lblAmountLimit.AutoSize = true;
            this.lblAmountLimit.Location = new System.Drawing.Point(9, 176);
            this.lblAmountLimit.Name = "lblAmountLimit";
            this.lblAmountLimit.Size = new System.Drawing.Size(55, 13);
            this.lblAmountLimit.TabIndex = 20;
            this.lblAmountLimit.Text = "金額上限";
            this.lblAmountLimit.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cboAccount);
            this.groupBox1.Controls.Add(this.txtKeyNo);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSell);
            this.groupBox1.Controls.Add(this.btnBuy);
            this.groupBox1.Controls.Add(this.nudManualPrice);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(3, 310);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 128);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "手動快速下單";
            // 
            // cboAccount
            // 
            this.cboAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAccount.Font = new System.Drawing.Font("Verdana", 8F);
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(9, 77);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(151, 21);
            this.cboAccount.TabIndex = 24;
            // 
            // nudManualPrice
            // 
            this.nudManualPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudManualPrice.DecimalPlaces = 2;
            this.nudManualPrice.Location = new System.Drawing.Point(50, 19);
            this.nudManualPrice.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudManualPrice.Minimum = new decimal(new int[] {
            9999,
            0,
            0,
            -2147483648});
            this.nudManualPrice.Name = "nudManualPrice";
            this.nudManualPrice.Size = new System.Drawing.Size(111, 20);
            this.nudManualPrice.TabIndex = 5;
            this.nudManualPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudManualPrice.ThousandsSeparator = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "價格";
            // 
            // chkSettingMode
            // 
            this.chkSettingMode.AutoSize = true;
            this.chkSettingMode.Location = new System.Drawing.Point(83, -1);
            this.chkSettingMode.Name = "chkSettingMode";
            this.chkSettingMode.Size = new System.Drawing.Size(74, 17);
            this.chkSettingMode.TabIndex = 18;
            this.chkSettingMode.Text = "設定模式";
            this.chkSettingMode.UseVisualStyleBackColor = true;
            this.chkSettingMode.CheckedChanged += new System.EventHandler(this.chkSettingMode_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "下單口數";
            // 
            // nudLots
            // 
            this.nudLots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLots.Location = new System.Drawing.Point(78, 124);
            this.nudLots.Maximum = new decimal(new int[] {
            9000000,
            0,
            0,
            0});
            this.nudLots.Name = "nudLots";
            this.nudLots.Size = new System.Drawing.Size(83, 20);
            this.nudLots.TabIndex = 3;
            this.nudLots.Tag = "ORDERSETTING;LOTS;Value";
            this.nudLots.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLots.ThousandsSeparator = true;
            this.nudLots.ValueChanged += new System.EventHandler(this.nudLots_Validated);
            this.nudLots.Validated += new System.EventHandler(this.nudLots_Validated);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtOrderYM);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lblLotsLimit);
            this.groupBox2.Controls.Add(this.txtAccount);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtExchange);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtOrderHead);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.chkSettingMode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.nudLots);
            this.groupBox2.Controls.Add(this.chkOrderAlert);
            this.groupBox2.Controls.Add(this.nudLotsLimit);
            this.groupBox2.Controls.Add(this.btnQuickLots1);
            this.groupBox2.Controls.Add(this.btnQuickLots6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnQuickLots5);
            this.groupBox2.Controls.Add(this.btnQuickLots4);
            this.groupBox2.Controls.Add(this.lblLots);
            this.groupBox2.Controls.Add(this.btnQuickLots3);
            this.groupBox2.Controls.Add(this.btnQuickLots2);
            this.groupBox2.Controls.Add(this.txtAmountLimit);
            this.groupBox2.Controls.Add(this.chkStopOrder);
            this.groupBox2.Controls.Add(this.lblAmountLimit);
            this.groupBox2.Location = new System.Drawing.Point(3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 300);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "下單控制項";
            // 
            // txtAccount
            // 
            this.txtAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAccount.Font = new System.Drawing.Font("Verdana", 7.75F, System.Drawing.FontStyle.Bold);
            this.txtAccount.Location = new System.Drawing.Point(46, 20);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.ReadOnly = true;
            this.txtAccount.Size = new System.Drawing.Size(115, 20);
            this.txtAccount.TabIndex = 28;
            this.txtAccount.Tag = "";
            this.txtAccount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "帳號";
            // 
            // txtExchange
            // 
            this.txtExchange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExchange.Font = new System.Drawing.Font("Verdana", 7.75F, System.Drawing.FontStyle.Bold);
            this.txtExchange.Location = new System.Drawing.Point(78, 44);
            this.txtExchange.Name = "txtExchange";
            this.txtExchange.ReadOnly = true;
            this.txtExchange.Size = new System.Drawing.Size(83, 20);
            this.txtExchange.TabIndex = 26;
            this.txtExchange.Tag = "";
            this.txtExchange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "交易所";
            // 
            // txtOrderHead
            // 
            this.txtOrderHead.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderHead.Font = new System.Drawing.Font("Verdana", 7.75F, System.Drawing.FontStyle.Bold);
            this.txtOrderHead.Location = new System.Drawing.Point(59, 69);
            this.txtOrderHead.Name = "txtOrderHead";
            this.txtOrderHead.ReadOnly = true;
            this.txtOrderHead.Size = new System.Drawing.Size(102, 20);
            this.txtOrderHead.TabIndex = 24;
            this.txtOrderHead.Tag = "";
            this.txtOrderHead.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "商品";
            // 
            // chkOrderAlert
            // 
            this.chkOrderAlert.AutoSize = true;
            this.chkOrderAlert.Location = new System.Drawing.Point(11, 278);
            this.chkOrderAlert.Name = "chkOrderAlert";
            this.chkOrderAlert.Size = new System.Drawing.Size(134, 17);
            this.chkOrderAlert.TabIndex = 16;
            this.chkOrderAlert.Tag = "ORDERSETTING;ORDERALERT;Checked";
            this.chkOrderAlert.Text = "下單前跳出詢問視窗";
            this.chkOrderAlert.UseVisualStyleBackColor = true;
            this.chkOrderAlert.Validated += new System.EventHandler(this.nudLots_Validated);
            // 
            // nudLotsLimit
            // 
            this.nudLotsLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudLotsLimit.Location = new System.Drawing.Point(78, 148);
            this.nudLotsLimit.Maximum = new decimal(new int[] {
            9000000,
            0,
            0,
            0});
            this.nudLotsLimit.Name = "nudLotsLimit";
            this.nudLotsLimit.Size = new System.Drawing.Size(83, 20);
            this.nudLotsLimit.TabIndex = 5;
            this.nudLotsLimit.Tag = "ORDERSETTING;LOTSLIMIT;Value";
            this.nudLotsLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLotsLimit.ThousandsSeparator = true;
            this.nudLotsLimit.ValueChanged += new System.EventHandler(this.nudLots_Validated);
            this.nudLotsLimit.Validated += new System.EventHandler(this.nudLots_Validated);
            // 
            // btnQuickLots1
            // 
            this.btnQuickLots1.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots1.Location = new System.Drawing.Point(9, 197);
            this.btnQuickLots1.Name = "btnQuickLots1";
            this.btnQuickLots1.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots1.TabIndex = 6;
            this.btnQuickLots1.Tag = "ORDERSETTING;QUITCKLOTS1;Text";
            this.btnQuickLots1.UseVisualStyleBackColor = true;
            this.btnQuickLots1.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots1.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots6
            // 
            this.btnQuickLots6.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots6.Location = new System.Drawing.Point(109, 225);
            this.btnQuickLots6.Name = "btnQuickLots6";
            this.btnQuickLots6.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots6.TabIndex = 14;
            this.btnQuickLots6.Tag = "ORDERSETTING;QUITCKLOTS6;Text";
            this.btnQuickLots6.UseVisualStyleBackColor = true;
            this.btnQuickLots6.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots6.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "口數上限";
            // 
            // btnQuickLots5
            // 
            this.btnQuickLots5.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots5.Location = new System.Drawing.Point(59, 225);
            this.btnQuickLots5.Name = "btnQuickLots5";
            this.btnQuickLots5.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots5.TabIndex = 13;
            this.btnQuickLots5.Tag = "ORDERSETTING;QUITCKLOTS5;Text";
            this.btnQuickLots5.UseVisualStyleBackColor = true;
            this.btnQuickLots5.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots5.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots4
            // 
            this.btnQuickLots4.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots4.Location = new System.Drawing.Point(9, 225);
            this.btnQuickLots4.Name = "btnQuickLots4";
            this.btnQuickLots4.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots4.TabIndex = 12;
            this.btnQuickLots4.Tag = "ORDERSETTING;QUITCKLOTS4;Text";
            this.btnQuickLots4.UseVisualStyleBackColor = true;
            this.btnQuickLots4.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots4.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // lblLots
            // 
            this.lblLots.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLots.AutoSize = true;
            this.lblLots.Location = new System.Drawing.Point(109, 176);
            this.lblLots.Name = "lblLots";
            this.lblLots.Size = new System.Drawing.Size(14, 13);
            this.lblLots.TabIndex = 9;
            this.lblLots.Text = "0";
            // 
            // btnQuickLots3
            // 
            this.btnQuickLots3.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots3.Location = new System.Drawing.Point(109, 197);
            this.btnQuickLots3.Name = "btnQuickLots3";
            this.btnQuickLots3.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots3.TabIndex = 11;
            this.btnQuickLots3.Tag = "ORDERSETTING;QUITCKLOTS3;Text";
            this.btnQuickLots3.UseVisualStyleBackColor = true;
            this.btnQuickLots3.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots3.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots2
            // 
            this.btnQuickLots2.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots2.Location = new System.Drawing.Point(59, 197);
            this.btnQuickLots2.Name = "btnQuickLots2";
            this.btnQuickLots2.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots2.TabIndex = 10;
            this.btnQuickLots2.Tag = "ORDERSETTING;QUITCKLOTS2;Text";
            this.btnQuickLots2.UseVisualStyleBackColor = true;
            this.btnQuickLots2.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots2.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // txtAmountLimit
            // 
            this.txtAmountLimit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAmountLimit.Location = new System.Drawing.Point(78, 172);
            this.txtAmountLimit.Name = "txtAmountLimit";
            this.txtAmountLimit.Size = new System.Drawing.Size(83, 20);
            this.txtAmountLimit.TabIndex = 21;
            this.txtAmountLimit.Tag = "ORDERSETTING;AUTOLIMITAMOUNT;Text";
            this.txtAmountLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAmountLimit.Visible = false;
            this.txtAmountLimit.Validated += new System.EventHandler(this.txtAmountLimit_Validated);
            // 
            // chkStopOrder
            // 
            this.chkStopOrder.AutoSize = true;
            this.chkStopOrder.Location = new System.Drawing.Point(11, 256);
            this.chkStopOrder.Name = "chkStopOrder";
            this.chkStopOrder.Size = new System.Drawing.Size(74, 17);
            this.chkStopOrder.TabIndex = 15;
            this.chkStopOrder.Tag = "ORDERSETTING;STOPORDER;Checked";
            this.chkStopOrder.Text = "禁止下單";
            this.chkStopOrder.UseVisualStyleBackColor = true;
            this.chkStopOrder.Validated += new System.EventHandler(this.nudLots_Validated);
            // 
            // txtOrderYM
            // 
            this.txtOrderYM.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderYM.Font = new System.Drawing.Font("Verdana", 7.75F, System.Drawing.FontStyle.Bold);
            this.txtOrderYM.Location = new System.Drawing.Point(46, 94);
            this.txtOrderYM.Name = "txtOrderYM";
            this.txtOrderYM.ReadOnly = true;
            this.txtOrderYM.Size = new System.Drawing.Size(115, 20);
            this.txtOrderYM.TabIndex = 30;
            this.txtOrderYM.Tag = "";
            this.txtOrderYM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "年月";
            // 
            // frmOrderSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(175, 443);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOrderSetting";
            this.Text = "下單";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrderSetting_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudManualPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLots)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLotsLimit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Label lblLotsLimit;
        private System.Windows.Forms.TextBox txtKeyNo;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.Label lblAmountLimit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudManualPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkSettingMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudLots;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkOrderAlert;
        private System.Windows.Forms.NumericUpDown nudLotsLimit;
        private System.Windows.Forms.Button btnQuickLots1;
        private System.Windows.Forms.Button btnQuickLots6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnQuickLots5;
        private System.Windows.Forms.Button btnQuickLots4;
        private System.Windows.Forms.Label lblLots;
        private System.Windows.Forms.Button btnQuickLots3;
        private System.Windows.Forms.Button btnQuickLots2;
        private System.Windows.Forms.TextBox txtAmountLimit;
        private System.Windows.Forms.CheckBox chkStopOrder;
        private System.Windows.Forms.TextBox txtOrderHead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtExchange;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboAccount;
        private System.Windows.Forms.TextBox txtOrderYM;
        private System.Windows.Forms.Label label7;
    }
}