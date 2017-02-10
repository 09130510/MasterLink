namespace Capital.Report
{
    partial class frmPosition
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
            this.olvPosition = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExchangeID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExchangeName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBrokerID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcCustNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcComID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcComName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBuySell = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcMP = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcAvgP = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcYstCP = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcProfitLoss = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.olvPosition)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // olvPosition
            // 
            this.olvPosition.AllColumns.Add(this.olvColumn1);
            this.olvPosition.AllColumns.Add(this.olvcExchangeID);
            this.olvPosition.AllColumns.Add(this.olvcExchangeName);
            this.olvPosition.AllColumns.Add(this.olvcBrokerID);
            this.olvPosition.AllColumns.Add(this.olvcCustNo);
            this.olvPosition.AllColumns.Add(this.olvcComID);
            this.olvPosition.AllColumns.Add(this.olvcComName);
            this.olvPosition.AllColumns.Add(this.olvcBuySell);
            this.olvPosition.AllColumns.Add(this.olvcQty);
            this.olvPosition.AllColumns.Add(this.olvcMP);
            this.olvPosition.AllColumns.Add(this.olvcAvgP);
            this.olvPosition.AllColumns.Add(this.olvcYstCP);
            this.olvPosition.AllColumns.Add(this.olvcProfitLoss);
            this.olvPosition.AllowColumnReorder = true;
            this.olvPosition.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvcExchangeID,
            this.olvcBrokerID,
            this.olvcCustNo,
            this.olvcComID,
            this.olvcBuySell,
            this.olvcQty,
            this.olvcMP,
            this.olvcAvgP,
            this.olvcYstCP,
            this.olvcProfitLoss});
            this.olvPosition.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvPosition.EmptyListMsg = "無未平倉資料";
            this.olvPosition.EmptyListMsgFont = new System.Drawing.Font("Verdana", 8F);
            this.olvPosition.Font = new System.Drawing.Font("Verdana", 8F);
            this.olvPosition.FullRowSelect = true;
            this.olvPosition.HeaderUsesThemes = false;
            this.olvPosition.Location = new System.Drawing.Point(0, 30);
            this.olvPosition.Name = "olvPosition";
            this.olvPosition.OwnerDraw = true;
            this.olvPosition.ShowCommandMenuOnRightClick = true;
            this.olvPosition.ShowGroups = false;
            this.olvPosition.ShowItemCountOnGroups = true;
            this.olvPosition.Size = new System.Drawing.Size(700, 240);
            this.olvPosition.TabIndex = 2;
            this.olvPosition.UseCellFormatEvents = true;
            this.olvPosition.UseCompatibleStateImageBehavior = false;
            this.olvPosition.UseCustomSelectionColors = true;
            this.olvPosition.UseFilterIndicator = true;
            this.olvPosition.UseFiltering = true;
            this.olvPosition.UseHotItem = true;
            this.olvPosition.UseNotifyPropertyChanged = true;
            this.olvPosition.UseTranslucentHotItem = true;
            this.olvPosition.UseTranslucentSelection = true;
            this.olvPosition.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Groupable = false;
            this.olvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Hideable = false;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Searchable = false;
            this.olvColumn1.ShowTextInHeader = false;
            this.olvColumn1.Sortable = false;
            this.olvColumn1.Text = "";
            this.olvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.UseFiltering = false;
            this.olvColumn1.Width = 5;
            // 
            // olvcExchangeID
            // 
            this.olvcExchangeID.AspectName = "ExchangeID";
            this.olvcExchangeID.AutoCompleteEditor = false;
            this.olvcExchangeID.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcExchangeID.CellPadding = null;
            this.olvcExchangeID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcExchangeID.IsEditable = false;
            this.olvcExchangeID.Text = "交易所";
            this.olvcExchangeID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcExchangeName
            // 
            this.olvcExchangeName.AspectName = "ExchangeName";
            this.olvcExchangeName.AutoCompleteEditor = false;
            this.olvcExchangeName.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcExchangeName.CellPadding = null;
            this.olvcExchangeName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcExchangeName.IsEditable = false;
            this.olvcExchangeName.IsVisible = false;
            this.olvcExchangeName.Text = "名稱";
            this.olvcExchangeName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcExchangeName.Width = 65;
            // 
            // olvcBrokerID
            // 
            this.olvcBrokerID.AspectName = "BrokerID";
            this.olvcBrokerID.AutoCompleteEditor = false;
            this.olvcBrokerID.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcBrokerID.CellPadding = null;
            this.olvcBrokerID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBrokerID.IsEditable = false;
            this.olvcBrokerID.Text = "上手";
            this.olvcBrokerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBrokerID.Width = 65;
            // 
            // olvcCustNo
            // 
            this.olvcCustNo.AspectName = "CustNo";
            this.olvcCustNo.AutoCompleteEditor = false;
            this.olvcCustNo.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcCustNo.CellPadding = null;
            this.olvcCustNo.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcCustNo.IsEditable = false;
            this.olvcCustNo.Text = "帳號";
            this.olvcCustNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcCustNo.Width = 65;
            // 
            // olvcComID
            // 
            this.olvcComID.AspectName = "ComID";
            this.olvcComID.AutoCompleteEditor = false;
            this.olvcComID.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcComID.CellPadding = null;
            this.olvcComID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcComID.IsEditable = false;
            this.olvcComID.Text = "商品";
            this.olvcComID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcComID.Width = 75;
            // 
            // olvcComName
            // 
            this.olvcComName.AspectName = "ComName";
            this.olvcComName.AutoCompleteEditor = false;
            this.olvcComName.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcComName.CellPadding = null;
            this.olvcComName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcComName.IsEditable = false;
            this.olvcComName.IsVisible = false;
            this.olvcComName.Text = "名稱";
            this.olvcComName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcBuySell
            // 
            this.olvcBuySell.AspectName = "BuySell";
            this.olvcBuySell.AutoCompleteEditor = false;
            this.olvcBuySell.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcBuySell.CellPadding = null;
            this.olvcBuySell.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBuySell.IsEditable = false;
            this.olvcBuySell.Text = "買賣";
            this.olvcBuySell.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBuySell.Width = 50;
            // 
            // olvcQty
            // 
            this.olvcQty.AspectName = "Qty";
            this.olvcQty.AspectToStringFormat = "{0:#,###}";
            this.olvcQty.AutoCompleteEditor = false;
            this.olvcQty.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcQty.CellPadding = null;
            this.olvcQty.Groupable = false;
            this.olvcQty.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcQty.IsEditable = false;
            this.olvcQty.Text = "數量";
            this.olvcQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcQty.Width = 50;
            // 
            // olvcMP
            // 
            this.olvcMP.AspectName = "MP";
            this.olvcMP.AspectToStringFormat = "{0:#,###.#0}";
            this.olvcMP.AutoCompleteEditor = false;
            this.olvcMP.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcMP.CellPadding = null;
            this.olvcMP.Groupable = false;
            this.olvcMP.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcMP.IsEditable = false;
            this.olvcMP.Text = "市價";
            this.olvcMP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcMP.Width = 65;
            // 
            // olvcAvgP
            // 
            this.olvcAvgP.AspectName = "AvgP";
            this.olvcAvgP.AspectToStringFormat = "{0:#,###.#0}";
            this.olvcAvgP.AutoCompleteEditor = false;
            this.olvcAvgP.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcAvgP.CellPadding = null;
            this.olvcAvgP.Groupable = false;
            this.olvcAvgP.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcAvgP.IsEditable = false;
            this.olvcAvgP.Text = "均價";
            this.olvcAvgP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcAvgP.Width = 65;
            // 
            // olvcYstCP
            // 
            this.olvcYstCP.AspectName = "YstCP";
            this.olvcYstCP.AspectToStringFormat = "{0:#,###.#0}";
            this.olvcYstCP.AutoCompleteEditor = false;
            this.olvcYstCP.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcYstCP.CellPadding = null;
            this.olvcYstCP.Groupable = false;
            this.olvcYstCP.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcYstCP.IsEditable = false;
            this.olvcYstCP.Text = "昨日結算價";
            this.olvcYstCP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcYstCP.Width = 65;
            // 
            // olvcProfitLoss
            // 
            this.olvcProfitLoss.AspectName = "ProfitLoss";
            this.olvcProfitLoss.AspectToStringFormat = "{0:#,###.#0}";
            this.olvcProfitLoss.AutoCompleteEditor = false;
            this.olvcProfitLoss.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcProfitLoss.CellPadding = null;
            this.olvcProfitLoss.Groupable = false;
            this.olvcProfitLoss.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcProfitLoss.IsEditable = false;
            this.olvcProfitLoss.Text = "損益";
            this.olvcProfitLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcProfitLoss.Width = 90;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTime);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 30);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "最後更新時間: ";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(185, 8);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(28, 13);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "N/A";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "取得";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmPosition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(700, 270);
            this.Controls.Add(this.olvPosition);
            this.Controls.Add(this.panel1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Name = "frmPosition";
            this.ShowInTaskbar = false;
            this.Text = "未平倉";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPosition_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvPosition)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvPosition;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnRefresh;
        private BrightIdeasSoftware.OLVColumn olvcExchangeID;
        private BrightIdeasSoftware.OLVColumn olvcExchangeName;
        private BrightIdeasSoftware.OLVColumn olvcBrokerID;
        private BrightIdeasSoftware.OLVColumn olvcCustNo;
        private BrightIdeasSoftware.OLVColumn olvcComID;
        private BrightIdeasSoftware.OLVColumn olvcComName;
        private BrightIdeasSoftware.OLVColumn olvcBuySell;
        private BrightIdeasSoftware.OLVColumn olvcQty;
        private BrightIdeasSoftware.OLVColumn olvcMP;
        private BrightIdeasSoftware.OLVColumn olvcAvgP;
        private BrightIdeasSoftware.OLVColumn olvcYstCP;
        private BrightIdeasSoftware.OLVColumn olvcProfitLoss;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
    }
}