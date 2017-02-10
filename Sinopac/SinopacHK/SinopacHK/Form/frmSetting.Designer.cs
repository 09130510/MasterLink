namespace SinopacHK
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
            this.label2 = new System.Windows.Forms.Label();
            this.nudLots = new System.Windows.Forms.NumericUpDown();
            this.nudLotsLimit = new System.Windows.Forms.NumericUpDown();
            this.btnQuickLots1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lblLotsLimit = new System.Windows.Forms.Label();
            this.lblLots = new System.Windows.Forms.Label();
            this.btnQuickLots2 = new System.Windows.Forms.Button();
            this.btnQuickLots3 = new System.Windows.Forms.Button();
            this.btnQuickLots4 = new System.Windows.Forms.Button();
            this.btnQuickLots5 = new System.Windows.Forms.Button();
            this.btnQuickLots6 = new System.Windows.Forms.Button();
            this.chkStopOrder = new System.Windows.Forms.CheckBox();
            this.chkOrderAlert = new System.Windows.Forms.CheckBox();
            this.chkSettingMode = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtClOrdID = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnBuy = new System.Windows.Forms.Button();
            this.nudManualPrice = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblAmountLimit = new System.Windows.Forms.Label();
            this.cboPID = new System.Windows.Forms.ComboBox();
            this.txtAmountLimit = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblOrderSeqno = new System.Windows.Forms.Label();
            this.txtSeqno = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudLots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLotsLimit)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudManualPrice)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "下單股數";
            // 
            // nudLots
            // 
            this.nudLots.Increment = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudLots.Location = new System.Drawing.Point(78, 45);
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
            this.nudLots.ValueChanged += new System.EventHandler(this.txtPID_Validated);
            // 
            // nudLotsLimit
            // 
            this.nudLotsLimit.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudLotsLimit.Location = new System.Drawing.Point(78, 70);
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
            this.nudLotsLimit.ValueChanged += new System.EventHandler(this.txtPID_Validated);
            // 
            // btnQuickLots1
            // 
            this.btnQuickLots1.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots1.Location = new System.Drawing.Point(9, 118);
            this.btnQuickLots1.Name = "btnQuickLots1";
            this.btnQuickLots1.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots1.TabIndex = 6;
            this.btnQuickLots1.Tag = "ORDERSETTING;QUITCKLOTS1;Text";
            this.btnQuickLots1.UseVisualStyleBackColor = true;
            this.btnQuickLots1.TextChanged += new System.EventHandler(this.txtPID_Validated);
            this.btnQuickLots1.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "股數上限";
            // 
            // lblLotsLimit
            // 
            this.lblLotsLimit.AutoSize = true;
            this.lblLotsLimit.Location = new System.Drawing.Point(9, 97);
            this.lblLotsLimit.Name = "lblLotsLimit";
            this.lblLotsLimit.Size = new System.Drawing.Size(91, 13);
            this.lblLotsLimit.TabIndex = 8;
            this.lblLotsLimit.Text = "自動股數上限：";
            // 
            // lblLots
            // 
            this.lblLots.AutoSize = true;
            this.lblLots.Location = new System.Drawing.Point(109, 96);
            this.lblLots.Name = "lblLots";
            this.lblLots.Size = new System.Drawing.Size(14, 13);
            this.lblLots.TabIndex = 9;
            this.lblLots.Text = "0";
            // 
            // btnQuickLots2
            // 
            this.btnQuickLots2.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots2.Location = new System.Drawing.Point(59, 118);
            this.btnQuickLots2.Name = "btnQuickLots2";
            this.btnQuickLots2.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots2.TabIndex = 10;
            this.btnQuickLots2.Tag = "ORDERSETTING;QUITCKLOTS2;Text";
            this.btnQuickLots2.UseVisualStyleBackColor = true;
            this.btnQuickLots2.TextChanged += new System.EventHandler(this.txtPID_Validated);
            this.btnQuickLots2.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots3
            // 
            this.btnQuickLots3.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots3.Location = new System.Drawing.Point(109, 118);
            this.btnQuickLots3.Name = "btnQuickLots3";
            this.btnQuickLots3.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots3.TabIndex = 11;
            this.btnQuickLots3.Tag = "ORDERSETTING;QUITCKLOTS3;Text";
            this.btnQuickLots3.UseVisualStyleBackColor = true;
            this.btnQuickLots3.TextChanged += new System.EventHandler(this.txtPID_Validated);
            this.btnQuickLots3.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots4
            // 
            this.btnQuickLots4.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots4.Location = new System.Drawing.Point(9, 146);
            this.btnQuickLots4.Name = "btnQuickLots4";
            this.btnQuickLots4.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots4.TabIndex = 12;
            this.btnQuickLots4.Tag = "ORDERSETTING;QUITCKLOTS4;Text";
            this.btnQuickLots4.UseVisualStyleBackColor = true;
            this.btnQuickLots4.TextChanged += new System.EventHandler(this.txtPID_Validated);
            this.btnQuickLots4.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots5
            // 
            this.btnQuickLots5.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots5.Location = new System.Drawing.Point(59, 146);
            this.btnQuickLots5.Name = "btnQuickLots5";
            this.btnQuickLots5.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots5.TabIndex = 13;
            this.btnQuickLots5.Tag = "ORDERSETTING;QUITCKLOTS5;Text";
            this.btnQuickLots5.UseVisualStyleBackColor = true;
            this.btnQuickLots5.TextChanged += new System.EventHandler(this.txtPID_Validated);
            this.btnQuickLots5.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // btnQuickLots6
            // 
            this.btnQuickLots6.Font = new System.Drawing.Font("Verdana", 7F);
            this.btnQuickLots6.Location = new System.Drawing.Point(109, 146);
            this.btnQuickLots6.Name = "btnQuickLots6";
            this.btnQuickLots6.Size = new System.Drawing.Size(50, 25);
            this.btnQuickLots6.TabIndex = 14;
            this.btnQuickLots6.Tag = "ORDERSETTING;QUITCKLOTS6;Text";
            this.btnQuickLots6.UseVisualStyleBackColor = true;
            this.btnQuickLots6.TextChanged += new System.EventHandler(this.txtPID_Validated);
            this.btnQuickLots6.Click += new System.EventHandler(this.btnQuickLots1_Click);
            // 
            // chkStopOrder
            // 
            this.chkStopOrder.AutoSize = true;
            this.chkStopOrder.Location = new System.Drawing.Point(9, 186);
            this.chkStopOrder.Name = "chkStopOrder";
            this.chkStopOrder.Size = new System.Drawing.Size(74, 17);
            this.chkStopOrder.TabIndex = 15;
            this.chkStopOrder.Tag = "ORDERSETTING;STOPORDER;Checked";
            this.chkStopOrder.Text = "禁止下單";
            this.chkStopOrder.UseVisualStyleBackColor = true;
            this.chkStopOrder.CheckedChanged += new System.EventHandler(this.txtPID_Validated);
            // 
            // chkOrderAlert
            // 
            this.chkOrderAlert.AutoSize = true;
            this.chkOrderAlert.Location = new System.Drawing.Point(9, 208);
            this.chkOrderAlert.Name = "chkOrderAlert";
            this.chkOrderAlert.Size = new System.Drawing.Size(134, 17);
            this.chkOrderAlert.TabIndex = 16;
            this.chkOrderAlert.Tag = "ORDERSETTING;ORDERALERT;Checked";
            this.chkOrderAlert.Text = "下單前跳出詢問視窗";
            this.chkOrderAlert.UseVisualStyleBackColor = true;
            this.chkOrderAlert.CheckedChanged += new System.EventHandler(this.txtPID_Validated);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtClOrdID);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSell);
            this.groupBox1.Controls.Add(this.btnBuy);
            this.groupBox1.Controls.Add(this.nudManualPrice);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(3, 243);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(166, 104);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "手動快速下單";
            // 
            // txtClOrdID
            // 
            this.txtClOrdID.Location = new System.Drawing.Point(9, 74);
            this.txtClOrdID.Name = "txtClOrdID";
            this.txtClOrdID.Size = new System.Drawing.Size(91, 20);
            this.txtClOrdID.TabIndex = 23;
            this.txtClOrdID.Tag = "";
            this.txtClOrdID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(111, 72);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 25);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "刪單";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSell
            // 
            this.btnSell.ForeColor = System.Drawing.Color.DarkGreen;
            this.btnSell.Location = new System.Drawing.Point(111, 43);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(50, 25);
            this.btnSell.TabIndex = 20;
            this.btnSell.Text = "賣";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // btnBuy
            // 
            this.btnBuy.ForeColor = System.Drawing.Color.Crimson;
            this.btnBuy.Location = new System.Drawing.Point(50, 43);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(50, 25);
            this.btnBuy.TabIndex = 15;
            this.btnBuy.Text = "買";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // nudManualPrice
            // 
            this.nudManualPrice.DecimalPlaces = 3;
            this.nudManualPrice.Location = new System.Drawing.Point(50, 19);
            this.nudManualPrice.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nudManualPrice.Name = "nudManualPrice";
            this.nudManualPrice.Size = new System.Drawing.Size(111, 20);
            this.nudManualPrice.TabIndex = 5;
            this.nudManualPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudManualPrice.ThousandsSeparator = true;
            this.nudManualPrice.ValueChanged += new System.EventHandler(this.nudManualPrice_ValueChanged);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblLotsLimit);
            this.groupBox2.Controls.Add(this.lblAmountLimit);
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
            this.groupBox2.Controls.Add(this.cboPID);
            this.groupBox2.Controls.Add(this.txtAmountLimit);
            this.groupBox2.Controls.Add(this.linkLabel1);
            this.groupBox2.Controls.Add(this.lblOrderSeqno);
            this.groupBox2.Controls.Add(this.txtSeqno);
            this.groupBox2.Controls.Add(this.chkStopOrder);
            this.groupBox2.Location = new System.Drawing.Point(3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(166, 232);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "下單控制項";
            // 
            // lblAmountLimit
            // 
            this.lblAmountLimit.AutoSize = true;
            this.lblAmountLimit.Location = new System.Drawing.Point(9, 97);
            this.lblAmountLimit.Name = "lblAmountLimit";
            this.lblAmountLimit.Size = new System.Drawing.Size(55, 13);
            this.lblAmountLimit.TabIndex = 20;
            this.lblAmountLimit.Text = "金額上限";
            this.lblAmountLimit.Visible = false;
            // 
            // cboPID
            // 
            this.cboPID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboPID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPID.FormattingEnabled = true;
            this.cboPID.Location = new System.Drawing.Point(53, 20);
            this.cboPID.Name = "cboPID";
            this.cboPID.Size = new System.Drawing.Size(107, 21);
            this.cboPID.TabIndex = 19;
            this.cboPID.Tag = "ORDERSETTING;PID;Text";
            this.cboPID.SelectedIndexChanged += new System.EventHandler(this.cboPID_SelectedIndexChanged);
            this.cboPID.Validated += new System.EventHandler(this.txtPID_Validated);
            // 
            // txtAmountLimit
            // 
            this.txtAmountLimit.Location = new System.Drawing.Point(78, 94);
            this.txtAmountLimit.Name = "txtAmountLimit";
            this.txtAmountLimit.Size = new System.Drawing.Size(83, 20);
            this.txtAmountLimit.TabIndex = 21;
            this.txtAmountLimit.Tag = "ORDERSETTING;AUTOLOTSLIMITAMOUNT;Text";
            this.txtAmountLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAmountLimit.Visible = false;
            this.txtAmountLimit.Validated += new System.EventHandler(this.txtPID_Validated);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoEllipsis = true;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(11, 21);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(28, 18);
            this.linkLabel1.TabIndex = 21;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "商品";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblOrderSeqno
            // 
            this.lblOrderSeqno.AutoSize = true;
            this.lblOrderSeqno.Location = new System.Drawing.Point(9, 187);
            this.lblOrderSeqno.Name = "lblOrderSeqno";
            this.lblOrderSeqno.Size = new System.Drawing.Size(67, 13);
            this.lblOrderSeqno.TabIndex = 21;
            this.lblOrderSeqno.Text = "下單序號：";
            this.lblOrderSeqno.Visible = false;
            // 
            // txtSeqno
            // 
            this.txtSeqno.Location = new System.Drawing.Point(78, 184);
            this.txtSeqno.Name = "txtSeqno";
            this.txtSeqno.Size = new System.Drawing.Size(83, 20);
            this.txtSeqno.TabIndex = 22;
            this.txtSeqno.Tag = "ORDER;Seqno;Text";
            this.txtSeqno.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSeqno.Visible = false;
            this.txtSeqno.Validated += new System.EventHandler(this.txtPID_Validated);
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(175, 380);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Name = "frmSetting";
            this.Text = "下單設定";
            ((System.ComponentModel.ISupportInitialize)(this.nudLots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLotsLimit)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudManualPrice)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudLots;
        private System.Windows.Forms.NumericUpDown nudLotsLimit;
        private System.Windows.Forms.Button btnQuickLots1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblLotsLimit;
        private System.Windows.Forms.Label lblLots;
        private System.Windows.Forms.Button btnQuickLots2;
        private System.Windows.Forms.Button btnQuickLots3;
        private System.Windows.Forms.Button btnQuickLots4;
        private System.Windows.Forms.Button btnQuickLots5;
        private System.Windows.Forms.Button btnQuickLots6;
        private System.Windows.Forms.CheckBox chkStopOrder;
        private System.Windows.Forms.CheckBox chkOrderAlert;
        private System.Windows.Forms.CheckBox chkSettingMode;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.NumericUpDown nudManualPrice;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cboPID;
        private System.Windows.Forms.Label lblAmountLimit;
        private System.Windows.Forms.TextBox txtAmountLimit;
        private System.Windows.Forms.TextBox txtClOrdID;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox txtSeqno;
        private System.Windows.Forms.Label lblOrderSeqno;
    }
}