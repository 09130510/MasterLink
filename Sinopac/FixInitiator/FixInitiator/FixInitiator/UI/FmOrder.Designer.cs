namespace FixInitiator.UI
{
    partial class FmOrder
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtClOrdID = new System.Windows.Forms.TextBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.txtOrderQty = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtSymbol = new System.Windows.Forms.TextBox();
            this.txtAccount = new System.Windows.Forms.TextBox();
            this.butOrder = new System.Windows.Forms.Button();
            this.cbTimeInForce = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbOrdType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSide = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tcReport = new System.Windows.Forms.TabControl();
            this.tpValidOrders = new System.Windows.Forms.TabPage();
            this.dgVaidOrder = new System.Windows.Forms.DataGridView();
            this.dcTradeDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcSymbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcOrderID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcSide = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcLastPx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcLastShares = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcLeaveQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcAvgPx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcClOrdID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dcCancel = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tpMatch = new System.Windows.Forms.TabPage();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tcReport.SuspendLayout();
            this.tpValidOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgVaidOrder)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.txtClOrdID);
            this.splitContainer1.Panel1.Controls.Add(this.butCancel);
            this.splitContainer1.Panel1.Controls.Add(this.txtOrderQty);
            this.splitContainer1.Panel1.Controls.Add(this.txtPrice);
            this.splitContainer1.Panel1.Controls.Add(this.txtSymbol);
            this.splitContainer1.Panel1.Controls.Add(this.txtAccount);
            this.splitContainer1.Panel1.Controls.Add(this.butOrder);
            this.splitContainer1.Panel1.Controls.Add(this.cbTimeInForce);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.cbOrdType);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.cbSide);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tcReport);
            this.splitContainer1.Size = new System.Drawing.Size(788, 351);
            this.splitContainer1.SplitterDistance = 127;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtClOrdID
            // 
            this.txtClOrdID.Location = new System.Drawing.Point(521, 12);
            this.txtClOrdID.Name = "txtClOrdID";
            this.txtClOrdID.Size = new System.Drawing.Size(100, 22);
            this.txtClOrdID.TabIndex = 21;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(627, 12);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(58, 23);
            this.butCancel.TabIndex = 20;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // txtOrderQty
            // 
            this.txtOrderQty.Location = new System.Drawing.Point(88, 93);
            this.txtOrderQty.Name = "txtOrderQty";
            this.txtOrderQty.Size = new System.Drawing.Size(89, 22);
            this.txtOrderQty.TabIndex = 14;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(88, 64);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(89, 22);
            this.txtPrice.TabIndex = 12;
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(88, 35);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Size = new System.Drawing.Size(89, 22);
            this.txtSymbol.TabIndex = 11;
            // 
            // txtAccount
            // 
            this.txtAccount.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::FixInitiator.Properties.Settings.Default, "Account", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtAccount.Location = new System.Drawing.Point(88, 6);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.Size = new System.Drawing.Size(89, 22);
            this.txtAccount.TabIndex = 8;
            this.txtAccount.Text = global::FixInitiator.Properties.Settings.Default.Account;
            // 
            // butOrder
            // 
            this.butOrder.Location = new System.Drawing.Point(304, 9);
            this.butOrder.Name = "butOrder";
            this.butOrder.Size = new System.Drawing.Size(58, 23);
            this.butOrder.TabIndex = 19;
            this.butOrder.Text = "Order";
            this.butOrder.UseVisualStyleBackColor = true;
            this.butOrder.Click += new System.EventHandler(this.butOrder_Click);
            // 
            // cbTimeInForce
            // 
            this.cbTimeInForce.FormattingEnabled = true;
            this.cbTimeInForce.Location = new System.Drawing.Point(198, 37);
            this.cbTimeInForce.Name = "cbTimeInForce";
            this.cbTimeInForce.Size = new System.Drawing.Size(72, 20);
            this.cbTimeInForce.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 97);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "股數";
            // 
            // cbOrdType
            // 
            this.cbOrdType.FormattingEnabled = true;
            this.cbOrdType.Location = new System.Drawing.Point(198, 66);
            this.cbOrdType.Name = "cbOrdType";
            this.cbOrdType.Size = new System.Drawing.Size(72, 20);
            this.cbOrdType.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "委託價";
            // 
            // cbSide
            // 
            this.cbSide.FormattingEnabled = true;
            this.cbSide.Location = new System.Drawing.Point(198, 8);
            this.cbSide.Name = "cbSide";
            this.cbSide.Size = new System.Drawing.Size(72, 20);
            this.cbSide.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "商品代碼";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "帳戶";
            // 
            // tcReport
            // 
            this.tcReport.Controls.Add(this.tpValidOrders);
            this.tcReport.Controls.Add(this.tpMatch);
            this.tcReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcReport.Location = new System.Drawing.Point(0, 0);
            this.tcReport.Name = "tcReport";
            this.tcReport.SelectedIndex = 0;
            this.tcReport.Size = new System.Drawing.Size(788, 220);
            this.tcReport.TabIndex = 0;
            // 
            // tpValidOrders
            // 
            this.tpValidOrders.Controls.Add(this.dgVaidOrder);
            this.tpValidOrders.Location = new System.Drawing.Point(4, 22);
            this.tpValidOrders.Name = "tpValidOrders";
            this.tpValidOrders.Padding = new System.Windows.Forms.Padding(3);
            this.tpValidOrders.Size = new System.Drawing.Size(780, 194);
            this.tpValidOrders.TabIndex = 0;
            this.tpValidOrders.Text = "在途單";
            this.tpValidOrders.UseVisualStyleBackColor = true;
            // 
            // dgVaidOrder
            // 
            this.dgVaidOrder.AllowUserToAddRows = false;
            this.dgVaidOrder.AllowUserToDeleteRows = false;
            this.dgVaidOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgVaidOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dcTradeDate,
            this.dcSymbol,
            this.dcOrderID,
            this.dcSide,
            this.dcLastPx,
            this.dcLastShares,
            this.dcLeaveQty,
            this.dcAvgPx,
            this.dcClOrdID,
            this.dcCancel});
            this.dgVaidOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgVaidOrder.Location = new System.Drawing.Point(3, 3);
            this.dgVaidOrder.Name = "dgVaidOrder";
            this.dgVaidOrder.ReadOnly = true;
            this.dgVaidOrder.RowHeadersVisible = false;
            this.dgVaidOrder.RowTemplate.Height = 24;
            this.dgVaidOrder.Size = new System.Drawing.Size(774, 188);
            this.dgVaidOrder.TabIndex = 1;
            // 
            // dcTradeDate
            // 
            this.dcTradeDate.HeaderText = "交易日";
            this.dcTradeDate.Name = "dcTradeDate";
            this.dcTradeDate.ReadOnly = true;
            this.dcTradeDate.Width = 70;
            // 
            // dcSymbol
            // 
            this.dcSymbol.HeaderText = "股票";
            this.dcSymbol.Name = "dcSymbol";
            this.dcSymbol.ReadOnly = true;
            this.dcSymbol.Width = 60;
            // 
            // dcOrderID
            // 
            this.dcOrderID.HeaderText = "委託書";
            this.dcOrderID.Name = "dcOrderID";
            this.dcOrderID.ReadOnly = true;
            this.dcOrderID.Width = 65;
            // 
            // dcSide
            // 
            this.dcSide.HeaderText = "B/S";
            this.dcSide.Name = "dcSide";
            this.dcSide.ReadOnly = true;
            this.dcSide.Width = 50;
            // 
            // dcLastPx
            // 
            this.dcLastPx.HeaderText = "委價";
            this.dcLastPx.Name = "dcLastPx";
            this.dcLastPx.ReadOnly = true;
            this.dcLastPx.Width = 70;
            // 
            // dcLastShares
            // 
            this.dcLastShares.HeaderText = "委股數";
            this.dcLastShares.Name = "dcLastShares";
            this.dcLastShares.ReadOnly = true;
            this.dcLastShares.Width = 70;
            // 
            // dcLeaveQty
            // 
            this.dcLeaveQty.HeaderText = "在途股數";
            this.dcLeaveQty.Name = "dcLeaveQty";
            this.dcLeaveQty.ReadOnly = true;
            this.dcLeaveQty.Width = 80;
            // 
            // dcAvgPx
            // 
            this.dcAvgPx.HeaderText = "均價";
            this.dcAvgPx.Name = "dcAvgPx";
            this.dcAvgPx.ReadOnly = true;
            this.dcAvgPx.Width = 80;
            // 
            // dcClOrdID
            // 
            this.dcClOrdID.HeaderText = "ClOrdID";
            this.dcClOrdID.Name = "dcClOrdID";
            this.dcClOrdID.ReadOnly = true;
            // 
            // dcCancel
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = "取消";
            this.dcCancel.DefaultCellStyle = dataGridViewCellStyle1;
            this.dcCancel.HeaderText = "";
            this.dcCancel.Name = "dcCancel";
            this.dcCancel.ReadOnly = true;
            this.dcCancel.Width = 70;
            // 
            // tpMatch
            // 
            this.tpMatch.Location = new System.Drawing.Point(4, 22);
            this.tpMatch.Name = "tpMatch";
            this.tpMatch.Padding = new System.Windows.Forms.Padding(3);
            this.tpMatch.Size = new System.Drawing.Size(780, 194);
            this.tpMatch.TabIndex = 1;
            this.tpMatch.Text = "成交";
            this.tpMatch.UseVisualStyleBackColor = true;
            // 
            // FmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 351);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FmOrder";
            this.Text = "FmOrder";
            this.Load += new System.EventHandler(this.FmOrder_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tcReport.ResumeLayout(false);
            this.tpValidOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgVaidOrder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtOrderQty;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtSymbol;
        private System.Windows.Forms.TextBox txtAccount;
        private System.Windows.Forms.Button butOrder;
        private System.Windows.Forms.ComboBox cbTimeInForce;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbOrdType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSide;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tcReport;
        private System.Windows.Forms.TabPage tpValidOrders;
        private System.Windows.Forms.TabPage tpMatch;
        private System.Windows.Forms.DataGridView dgVaidOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcTradeDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcSymbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcOrderID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcSide;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcLastPx;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcLastShares;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcLeaveQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcAvgPx;
        private System.Windows.Forms.DataGridViewTextBoxColumn dcClOrdID;
        private System.Windows.Forms.DataGridViewButtonColumn dcCancel;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.TextBox txtClOrdID;

    }
}