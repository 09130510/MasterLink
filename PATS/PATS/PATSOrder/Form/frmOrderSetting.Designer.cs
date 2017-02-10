namespace PATSOrder
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.nudLotsLimit = new System.Windows.Forms.NumericUpDown();
            this.nudLots = new System.Windows.Forms.NumericUpDown();
            this.txtContract = new System.Windows.Forms.TextBox();
            this.txtExch = new System.Windows.Forms.TextBox();
            this.chkAlert = new System.Windows.Forms.CheckBox();
            this.chkForbidden = new System.Windows.Forms.CheckBox();
            this.btnQuickLots6 = new System.Windows.Forms.Button();
            this.btnQuickLots5 = new System.Windows.Forms.Button();
            this.btnQuickLots4 = new System.Windows.Forms.Button();
            this.btnQuickLots3 = new System.Windows.Forms.Button();
            this.btnQuickLots2 = new System.Windows.Forms.Button();
            this.btnQuickLots1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkSettingMode = new System.Windows.Forms.CheckBox();
            this.lblLotsLimit = new System.Windows.Forms.Label();
            this.lblLots = new System.Windows.Forms.Label();
            this.txtAmountLimit = new System.Windows.Forms.TextBox();
            this.lblAmountLimit = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnBuy = new System.Windows.Forms.Button();
            this.nudManual = new System.Windows.Forms.NumericUpDown();
            this.lblPrice = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLotsLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLots)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudManual)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtAccount);
            this.groupBox1.Controls.Add(this.nudLotsLimit);
            this.groupBox1.Controls.Add(this.nudLots);
            this.groupBox1.Controls.Add(this.txtContract);
            this.groupBox1.Controls.Add(this.txtExch);
            this.groupBox1.Controls.Add(this.chkAlert);
            this.groupBox1.Controls.Add(this.chkForbidden);
            this.groupBox1.Controls.Add(this.btnQuickLots6);
            this.groupBox1.Controls.Add(this.btnQuickLots5);
            this.groupBox1.Controls.Add(this.btnQuickLots4);
            this.groupBox1.Controls.Add(this.btnQuickLots3);
            this.groupBox1.Controls.Add(this.btnQuickLots2);
            this.groupBox1.Controls.Add(this.btnQuickLots1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chkSettingMode);
            this.groupBox1.Controls.Add(this.lblLotsLimit);
            this.groupBox1.Controls.Add(this.lblLots);
            this.groupBox1.Controls.Add(this.txtAmountLimit);
            this.groupBox1.Controls.Add(this.lblAmountLimit);
            this.groupBox1.ForeColor = System.Drawing.Color.Blue;
            this.groupBox1.Location = new System.Drawing.Point(5, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(168, 265);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "下單控制項";
            // 
            // txtAccount
            // 
            this.txtAccount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAccount.Location = new System.Drawing.Point(62, 23);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.ReadOnly = true;
            this.txtAccount.Size = new System.Drawing.Size(100, 20);
            this.txtAccount.TabIndex = 21;
            // 
            // nudLotsLimit
            // 
            this.nudLotsLimit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nudLotsLimit.Location = new System.Drawing.Point(85, 114);
            this.nudLotsLimit.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudLotsLimit.Name = "nudLotsLimit";
            this.nudLotsLimit.Size = new System.Drawing.Size(77, 20);
            this.nudLotsLimit.TabIndex = 20;
            this.nudLotsLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLotsLimit.ValueChanged += new System.EventHandler(this.nudLots_Validated);
            this.nudLotsLimit.Validated += new System.EventHandler(this.nudLots_Validated);
            // 
            // nudLots
            // 
            this.nudLots.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nudLots.Location = new System.Drawing.Point(85, 91);
            this.nudLots.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudLots.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLots.Name = "nudLots";
            this.nudLots.Size = new System.Drawing.Size(77, 20);
            this.nudLots.TabIndex = 19;
            this.nudLots.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudLots.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLots.ValueChanged += new System.EventHandler(this.nudLots_Validated);
            this.nudLots.Validated += new System.EventHandler(this.nudLots_Validated);
            // 
            // txtContract
            // 
            this.txtContract.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtContract.Location = new System.Drawing.Point(49, 68);
            this.txtContract.Name = "txtContract";
            this.txtContract.ReadOnly = true;
            this.txtContract.Size = new System.Drawing.Size(113, 20);
            this.txtContract.TabIndex = 18;
            // 
            // txtExch
            // 
            this.txtExch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtExch.Location = new System.Drawing.Point(86, 45);
            this.txtExch.Name = "txtExch";
            this.txtExch.ReadOnly = true;
            this.txtExch.Size = new System.Drawing.Size(76, 20);
            this.txtExch.TabIndex = 17;
            // 
            // chkAlert
            // 
            this.chkAlert.AutoSize = true;
            this.chkAlert.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkAlert.Location = new System.Drawing.Point(14, 242);
            this.chkAlert.Name = "chkAlert";
            this.chkAlert.Size = new System.Drawing.Size(134, 17);
            this.chkAlert.TabIndex = 15;
            this.chkAlert.Tag = "SYS;ALERT;Checked";
            this.chkAlert.Text = "下單前跳出詢問視窗";
            this.chkAlert.UseVisualStyleBackColor = true;
            this.chkAlert.CheckedChanged += new System.EventHandler(this.nudLots_Validated);
            this.chkAlert.Validated += new System.EventHandler(this.nudLots_Validated);
            // 
            // chkForbidden
            // 
            this.chkForbidden.AutoSize = true;
            this.chkForbidden.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkForbidden.Location = new System.Drawing.Point(14, 222);
            this.chkForbidden.Name = "chkForbidden";
            this.chkForbidden.Size = new System.Drawing.Size(74, 17);
            this.chkForbidden.TabIndex = 14;
            this.chkForbidden.Tag = "SYS;FORBIDDEN;Checked";
            this.chkForbidden.Text = "禁止下單";
            this.chkForbidden.UseVisualStyleBackColor = true;
            this.chkForbidden.CheckedChanged += new System.EventHandler(this.nudLots_Validated);
            this.chkForbidden.Validated += new System.EventHandler(this.nudLots_Validated);
            // 
            // btnQuickLots6
            // 
            this.btnQuickLots6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuickLots6.Location = new System.Drawing.Point(112, 188);
            this.btnQuickLots6.Name = "btnQuickLots6";
            this.btnQuickLots6.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots6.TabIndex = 13;
            this.btnQuickLots6.Tag = "SYS;QUICKLOTS6;Text";
            this.btnQuickLots6.UseVisualStyleBackColor = true;
            this.btnQuickLots6.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots6.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots5
            // 
            this.btnQuickLots5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuickLots5.Location = new System.Drawing.Point(62, 188);
            this.btnQuickLots5.Name = "btnQuickLots5";
            this.btnQuickLots5.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots5.TabIndex = 12;
            this.btnQuickLots5.Tag = "SYS;QUICKLOTS5;Text";
            this.btnQuickLots5.UseVisualStyleBackColor = true;
            this.btnQuickLots5.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots5.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots4
            // 
            this.btnQuickLots4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuickLots4.Location = new System.Drawing.Point(12, 188);
            this.btnQuickLots4.Name = "btnQuickLots4";
            this.btnQuickLots4.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots4.TabIndex = 11;
            this.btnQuickLots4.Tag = "SYS;QUICKLOTS4;Text";
            this.btnQuickLots4.UseVisualStyleBackColor = true;
            this.btnQuickLots4.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots4.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots3
            // 
            this.btnQuickLots3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuickLots3.Location = new System.Drawing.Point(112, 163);
            this.btnQuickLots3.Name = "btnQuickLots3";
            this.btnQuickLots3.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots3.TabIndex = 10;
            this.btnQuickLots3.Tag = "SYS;QUICKLOTS3;Text";
            this.btnQuickLots3.UseVisualStyleBackColor = true;
            this.btnQuickLots3.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots3.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots2
            // 
            this.btnQuickLots2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuickLots2.Location = new System.Drawing.Point(62, 163);
            this.btnQuickLots2.Name = "btnQuickLots2";
            this.btnQuickLots2.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots2.TabIndex = 9;
            this.btnQuickLots2.Tag = "SYS;QUICKLOTS2;Text";
            this.btnQuickLots2.UseVisualStyleBackColor = true;
            this.btnQuickLots2.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots2.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots1
            // 
            this.btnQuickLots1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnQuickLots1.Location = new System.Drawing.Point(12, 163);
            this.btnQuickLots1.Name = "btnQuickLots1";
            this.btnQuickLots1.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots1.TabIndex = 8;
            this.btnQuickLots1.Tag = "SYS;QUICKLOTS1;Text";
            this.btnQuickLots1.UseVisualStyleBackColor = true;
            this.btnQuickLots1.TextChanged += new System.EventHandler(this.nudLots_Validated);
            this.btnQuickLots1.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(12, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "口數上限";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "下單口數";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(12, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "商品";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "交易所";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "帳號";
            // 
            // chkSettingMode
            // 
            this.chkSettingMode.AutoSize = true;
            this.chkSettingMode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chkSettingMode.Location = new System.Drawing.Point(95, 0);
            this.chkSettingMode.Name = "chkSettingMode";
            this.chkSettingMode.Size = new System.Drawing.Size(74, 17);
            this.chkSettingMode.TabIndex = 0;
            this.chkSettingMode.Text = "設定模式";
            this.chkSettingMode.UseVisualStyleBackColor = true;
            this.chkSettingMode.CheckedChanged += new System.EventHandler(this.chkSettingMode_CheckedChanged);
            // 
            // lblLotsLimit
            // 
            this.lblLotsLimit.AutoSize = true;
            this.lblLotsLimit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLotsLimit.Location = new System.Drawing.Point(12, 141);
            this.lblLotsLimit.Name = "lblLotsLimit";
            this.lblLotsLimit.Size = new System.Drawing.Size(91, 13);
            this.lblLotsLimit.TabIndex = 6;
            this.lblLotsLimit.Text = "自動口數上限：";
            // 
            // lblLots
            // 
            this.lblLots.AutoSize = true;
            this.lblLots.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLots.Location = new System.Drawing.Point(114, 141);
            this.lblLots.Name = "lblLots";
            this.lblLots.Size = new System.Drawing.Size(14, 13);
            this.lblLots.TabIndex = 7;
            this.lblLots.Text = "0";
            // 
            // txtAmountLimit
            // 
            this.txtAmountLimit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtAmountLimit.Location = new System.Drawing.Point(76, 141);
            this.txtAmountLimit.Name = "txtAmountLimit";
            this.txtAmountLimit.Size = new System.Drawing.Size(86, 20);
            this.txtAmountLimit.TabIndex = 23;
            this.txtAmountLimit.Visible = false;
            this.txtAmountLimit.Validated += new System.EventHandler(this.txtAmountLimit_Validated);
            // 
            // lblAmountLimit
            // 
            this.lblAmountLimit.AutoSize = true;
            this.lblAmountLimit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAmountLimit.Location = new System.Drawing.Point(12, 141);
            this.lblAmountLimit.Name = "lblAmountLimit";
            this.lblAmountLimit.Size = new System.Drawing.Size(55, 13);
            this.lblAmountLimit.TabIndex = 22;
            this.lblAmountLimit.Text = "金額上限";
            this.lblAmountLimit.Visible = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnCancel);
            this.groupBox2.Controls.Add(this.txtOrderID);
            this.groupBox2.Controls.Add(this.cboAccount);
            this.groupBox2.Controls.Add(this.btnSell);
            this.groupBox2.Controls.Add(this.btnBuy);
            this.groupBox2.Controls.Add(this.nudManual);
            this.groupBox2.Controls.Add(this.lblPrice);
            this.groupBox2.ForeColor = System.Drawing.Color.Blue;
            this.groupBox2.Location = new System.Drawing.Point(5, 279);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(168, 127);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "手動快速下單";
            // 
            // btnCancel
            // 
            this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCancel.Location = new System.Drawing.Point(124, 99);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(38, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "刪";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtOrderID
            // 
            this.txtOrderID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtOrderID.Location = new System.Drawing.Point(14, 101);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.Size = new System.Drawing.Size(104, 20);
            this.txtOrderID.TabIndex = 5;
            // 
            // cboAccount
            // 
            this.cboAccount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(14, 77);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(148, 21);
            this.cboAccount.TabIndex = 4;
            // 
            // btnSell
            // 
            this.btnSell.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnSell.Location = new System.Drawing.Point(124, 42);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(38, 25);
            this.btnSell.TabIndex = 3;
            this.btnSell.Text = "賣";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // btnBuy
            // 
            this.btnBuy.ForeColor = System.Drawing.Color.Crimson;
            this.btnBuy.Location = new System.Drawing.Point(62, 42);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(38, 25);
            this.btnBuy.TabIndex = 2;
            this.btnBuy.Text = "買";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // nudManual
            // 
            this.nudManual.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nudManual.Location = new System.Drawing.Point(62, 21);
            this.nudManual.Name = "nudManual";
            this.nudManual.Size = new System.Drawing.Size(100, 20);
            this.nudManual.TabIndex = 1;
            this.nudManual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPrice.Location = new System.Drawing.Point(14, 23);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(31, 13);
            this.lblPrice.TabIndex = 0;
            this.lblPrice.Text = "價格";
            // 
            // frmOrderSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(181, 411);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOrderSetting";
            this.Text = "下單";
            this.DockStateChanged += new System.EventHandler(this.frmOrderSetting_DockStateChanged);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLotsLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLots)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudManual)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkAlert;
        private System.Windows.Forms.CheckBox chkForbidden;
        private System.Windows.Forms.Button btnQuickLots6;
        private System.Windows.Forms.Button btnQuickLots5;
        private System.Windows.Forms.Button btnQuickLots4;
        private System.Windows.Forms.Button btnQuickLots3;
        private System.Windows.Forms.Button btnQuickLots2;
        private System.Windows.Forms.Button btnQuickLots1;
        private System.Windows.Forms.Label lblLots;
        private System.Windows.Forms.Label lblLotsLimit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkSettingMode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown nudLotsLimit;
        private System.Windows.Forms.NumericUpDown nudLots;
        private System.Windows.Forms.TextBox txtContract;
        private System.Windows.Forms.TextBox txtExch;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtOrderID;
        private System.Windows.Forms.ComboBox cboAccount;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.NumericUpDown nudManual;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Label lblAmountLimit;
        private System.Windows.Forms.TextBox txtAmountLimit;
    }
}