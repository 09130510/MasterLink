namespace PATS
{
    partial class frmOrderReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderReport));
            this.olvOrder = new BrightIdeasSoftware.ObjectListView();
            this.olvcIndex = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcHistoric = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOrderID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExchOrderID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTraderAccount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOrderType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcKey = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExchangeName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcContractName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcContractDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBuyOrSell = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPrice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcLots = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcAmountFilled = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcNoOfFills = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcAveragePrice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOpenOrClose = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTimeSent = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTimeHostRecd = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTimeExchRecd = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTimeExchAckn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcNonExecReason = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAction = new System.Windows.Forms.Label();
            this.gupAction = new System.Windows.Forms.GroupBox();
            this.radRenderer = new System.Windows.Forms.RadioButton();
            this.radFilter = new System.Windows.Forms.RadioButton();
            this.radNone = new System.Windows.Forms.RadioButton();
            this.chkValid = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.olvOrder)).BeginInit();
            this.panel1.SuspendLayout();
            this.gupAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // olvOrder
            // 
            this.olvOrder.AllColumns.Add(this.olvcIndex);
            this.olvOrder.AllColumns.Add(this.olvcHistoric);
            this.olvOrder.AllColumns.Add(this.olvcOrderID);
            this.olvOrder.AllColumns.Add(this.olvcExchOrderID);
            this.olvOrder.AllColumns.Add(this.olvcTraderAccount);
            this.olvOrder.AllColumns.Add(this.olvcOrderType);
            this.olvOrder.AllColumns.Add(this.olvcKey);
            this.olvOrder.AllColumns.Add(this.olvcExchangeName);
            this.olvOrder.AllColumns.Add(this.olvcContractName);
            this.olvOrder.AllColumns.Add(this.olvcContractDate);
            this.olvOrder.AllColumns.Add(this.olvcBuyOrSell);
            this.olvOrder.AllColumns.Add(this.olvcPrice);
            this.olvOrder.AllColumns.Add(this.olvcLots);
            this.olvOrder.AllColumns.Add(this.olvcAmountFilled);
            this.olvOrder.AllColumns.Add(this.olvcNoOfFills);
            this.olvOrder.AllColumns.Add(this.olvcAveragePrice);
            this.olvOrder.AllColumns.Add(this.olvcStatus);
            this.olvOrder.AllColumns.Add(this.olvcOpenOrClose);
            this.olvOrder.AllColumns.Add(this.olvcTimeSent);
            this.olvOrder.AllColumns.Add(this.olvcTimeHostRecd);
            this.olvOrder.AllColumns.Add(this.olvcTimeExchRecd);
            this.olvOrder.AllColumns.Add(this.olvcTimeExchAckn);
            this.olvOrder.AllColumns.Add(this.olvcNonExecReason);
            this.olvOrder.AllowColumnReorder = true;
            this.olvOrder.CellEditUseWholeCell = false;
            this.olvOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcIndex,
            this.olvcOrderID,
            this.olvcKey,
            this.olvcBuyOrSell,
            this.olvcPrice,
            this.olvcLots,
            this.olvcAmountFilled,
            this.olvcNoOfFills,
            this.olvcAveragePrice,
            this.olvcStatus,
            this.olvcOpenOrClose});
            this.olvOrder.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvOrder.EmptyListMsg = "無委託資料";
            this.olvOrder.EmptyListMsgFont = new System.Drawing.Font("Verdana", 8F);
            this.olvOrder.FullRowSelect = true;
            this.olvOrder.GroupWithItemCountFormat = "";
            this.olvOrder.HeaderWordWrap = true;
            this.olvOrder.HideSelection = false;
            this.olvOrder.IncludeColumnHeadersInCopy = true;
            this.olvOrder.IncludeHiddenColumnsInDataTransfer = true;
            this.olvOrder.Location = new System.Drawing.Point(0, 33);
            this.olvOrder.Name = "olvOrder";
            this.olvOrder.PersistentCheckBoxes = false;
            this.olvOrder.ShowCommandMenuOnRightClick = true;
            this.olvOrder.ShowGroups = false;
            this.olvOrder.ShowHeaderInAllViews = false;
            this.olvOrder.ShowItemToolTips = true;
            this.olvOrder.Size = new System.Drawing.Size(635, 344);
            this.olvOrder.SortGroupItemsByPrimaryColumn = false;
            this.olvOrder.TabIndex = 0;
            this.olvOrder.TintSortColumn = true;
            this.olvOrder.UseCellFormatEvents = true;
            this.olvOrder.UseCompatibleStateImageBehavior = false;
            this.olvOrder.UseFilterIndicator = true;
            this.olvOrder.UseFiltering = true;
            this.olvOrder.UseHotItem = true;
            this.olvOrder.UseNotifyPropertyChanged = true;
            this.olvOrder.UseTranslucentHotItem = true;
            this.olvOrder.UseTranslucentSelection = true;
            this.olvOrder.View = System.Windows.Forms.View.Details;
            this.olvOrder.SelectedIndexChanged += new System.EventHandler(this.olvOrder_SelectedIndexChanged);
            // 
            // olvcIndex
            // 
            this.olvcIndex.AspectName = "Index";
            this.olvcIndex.Text = "序號";
            this.olvcIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcIndex.Width = 40;
            // 
            // olvcHistoric
            // 
            this.olvcHistoric.AspectName = "Historic";
            this.olvcHistoric.DisplayIndex = 1;
            this.olvcHistoric.IsVisible = false;
            this.olvcHistoric.Text = "歷史";
            this.olvcHistoric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcHistoric.Width = 40;
            // 
            // olvcOrderID
            // 
            this.olvcOrderID.AspectName = "OrderID";
            this.olvcOrderID.Text = "委託代碼";
            this.olvcOrderID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcOrderID.Width = 80;
            // 
            // olvcExchOrderID
            // 
            this.olvcExchOrderID.AspectName = "ExchOrderID";
            this.olvcExchOrderID.DisplayIndex = 3;
            this.olvcExchOrderID.IsVisible = false;
            this.olvcExchOrderID.Text = "委託書號";
            // 
            // olvcTraderAccount
            // 
            this.olvcTraderAccount.AspectName = "TraderAccount";
            this.olvcTraderAccount.DisplayIndex = 3;
            this.olvcTraderAccount.IsVisible = false;
            this.olvcTraderAccount.Text = "帳號";
            this.olvcTraderAccount.Width = 75;
            // 
            // olvcOrderType
            // 
            this.olvcOrderType.AspectName = "OrderType";
            this.olvcOrderType.DisplayIndex = 2;
            this.olvcOrderType.IsVisible = false;
            this.olvcOrderType.Text = "種類";
            // 
            // olvcKey
            // 
            this.olvcKey.AspectName = "Key";
            this.olvcKey.IsEditable = false;
            this.olvcKey.Text = "商品";
            this.olvcKey.Width = 100;
            // 
            // olvcExchangeName
            // 
            this.olvcExchangeName.AspectName = "ExchangeName";
            this.olvcExchangeName.DisplayIndex = 6;
            this.olvcExchangeName.IsEditable = false;
            this.olvcExchangeName.IsVisible = false;
            this.olvcExchangeName.Text = "交易所";
            // 
            // olvcContractName
            // 
            this.olvcContractName.AspectName = "ContractName";
            this.olvcContractName.DisplayIndex = 7;
            this.olvcContractName.IsVisible = false;
            this.olvcContractName.Text = "名稱";
            // 
            // olvcContractDate
            // 
            this.olvcContractDate.AspectName = "ContractDate";
            this.olvcContractDate.DisplayIndex = 8;
            this.olvcContractDate.IsVisible = false;
            this.olvcContractDate.Text = "年月";
            // 
            // olvcBuyOrSell
            // 
            this.olvcBuyOrSell.AspectName = "BuyOrSell";
            this.olvcBuyOrSell.Text = "買賣";
            this.olvcBuyOrSell.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBuyOrSell.Width = 40;
            // 
            // olvcPrice
            // 
            this.olvcPrice.AspectName = "Price";
            this.olvcPrice.AspectToStringFormat = "{0:#,##0.00}";
            this.olvcPrice.Text = "價格";
            this.olvcPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // olvcLots
            // 
            this.olvcLots.AspectName = "Lots";
            this.olvcLots.Text = "口數";
            this.olvcLots.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcLots.Width = 45;
            // 
            // olvcAmountFilled
            // 
            this.olvcAmountFilled.AspectName = "AmountFilled";
            this.olvcAmountFilled.Text = "已成交";
            this.olvcAmountFilled.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcAmountFilled.Width = 50;
            // 
            // olvcNoOfFills
            // 
            this.olvcNoOfFills.AspectName = "NoOfFills";
            this.olvcNoOfFills.Text = "成交次數";
            this.olvcNoOfFills.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcNoOfFills.Width = 65;
            // 
            // olvcAveragePrice
            // 
            this.olvcAveragePrice.AspectName = "AveragePrice";
            this.olvcAveragePrice.AspectToStringFormat = "{0:#,##0.00}";
            this.olvcAveragePrice.Text = "均價";
            this.olvcAveragePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // olvcStatus
            // 
            this.olvcStatus.AspectName = "State";
            this.olvcStatus.Text = "狀態";
            // 
            // olvcOpenOrClose
            // 
            this.olvcOpenOrClose.AspectName = "OpenOrClose";
            this.olvcOpenOrClose.Text = "開平倉";
            this.olvcOpenOrClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOpenOrClose.Width = 50;
            // 
            // olvcTimeSent
            // 
            this.olvcTimeSent.AspectName = "TimeSent";
            this.olvcTimeSent.DisplayIndex = 17;
            this.olvcTimeSent.IsVisible = false;
            this.olvcTimeSent.Text = "送出時間";
            // 
            // olvcTimeHostRecd
            // 
            this.olvcTimeHostRecd.AspectName = "TimeHoseRecd";
            this.olvcTimeHostRecd.DisplayIndex = 18;
            this.olvcTimeHostRecd.IsVisible = false;
            this.olvcTimeHostRecd.Text = "主機接收時間";
            // 
            // olvcTimeExchRecd
            // 
            this.olvcTimeExchRecd.AspectName = "TimeExchRecd";
            this.olvcTimeExchRecd.DisplayIndex = 19;
            this.olvcTimeExchRecd.IsVisible = false;
            this.olvcTimeExchRecd.Text = "交易所接收時間";
            // 
            // olvcTimeExchAckn
            // 
            this.olvcTimeExchAckn.AspectName = "TimeExchAckn";
            this.olvcTimeExchAckn.DisplayIndex = 20;
            this.olvcTimeExchAckn.IsVisible = false;
            this.olvcTimeExchAckn.Text = "交易所回覆時間";
            // 
            // olvcNonExecReason
            // 
            this.olvcNonExecReason.AspectName = "NonExecReason";
            this.olvcNonExecReason.DisplayIndex = 21;
            this.olvcNonExecReason.IsVisible = false;
            this.olvcNonExecReason.Text = "未執行原因";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblAction);
            this.panel1.Controls.Add(this.gupAction);
            this.panel1.Controls.Add(this.chkValid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 33);
            this.panel1.TabIndex = 1;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(137, 9);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(55, 13);
            this.lblAction.TabIndex = 8;
            this.lblAction.Text = "選取動作";
            // 
            // gupAction
            // 
            this.gupAction.Controls.Add(this.radRenderer);
            this.gupAction.Controls.Add(this.radFilter);
            this.gupAction.Controls.Add(this.radNone);
            this.gupAction.Location = new System.Drawing.Point(202, -3);
            this.gupAction.Name = "gupAction";
            this.gupAction.Size = new System.Drawing.Size(194, 31);
            this.gupAction.TabIndex = 7;
            this.gupAction.TabStop = false;
            // 
            // radRenderer
            // 
            this.radRenderer.AutoSize = true;
            this.radRenderer.Location = new System.Drawing.Point(132, 11);
            this.radRenderer.Name = "radRenderer";
            this.radRenderer.Size = new System.Drawing.Size(49, 17);
            this.radRenderer.TabIndex = 2;
            this.radRenderer.Text = "聯動";
            this.radRenderer.UseVisualStyleBackColor = true;
            this.radRenderer.CheckedChanged += new System.EventHandler(this.radNone_CheckedChanged);
            // 
            // radFilter
            // 
            this.radFilter.AutoSize = true;
            this.radFilter.Location = new System.Drawing.Point(65, 10);
            this.radFilter.Name = "radFilter";
            this.radFilter.Size = new System.Drawing.Size(49, 17);
            this.radFilter.TabIndex = 1;
            this.radFilter.Text = "過濾";
            this.radFilter.UseVisualStyleBackColor = true;
            this.radFilter.CheckedChanged += new System.EventHandler(this.radNone_CheckedChanged);
            // 
            // radNone
            // 
            this.radNone.AutoSize = true;
            this.radNone.Checked = true;
            this.radNone.Location = new System.Drawing.Point(13, 10);
            this.radNone.Name = "radNone";
            this.radNone.Size = new System.Drawing.Size(37, 17);
            this.radNone.TabIndex = 0;
            this.radNone.TabStop = true;
            this.radNone.Text = "無";
            this.radNone.UseVisualStyleBackColor = true;
            this.radNone.CheckedChanged += new System.EventHandler(this.radNone_CheckedChanged);
            // 
            // chkValid
            // 
            this.chkValid.AutoSize = true;
            this.chkValid.Checked = true;
            this.chkValid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkValid.Location = new System.Drawing.Point(9, 8);
            this.chkValid.Name = "chkValid";
            this.chkValid.Size = new System.Drawing.Size(98, 17);
            this.chkValid.TabIndex = 6;
            this.chkValid.Tag = "SYS;ONLYDISPLAYVALID;Checked";
            this.chkValid.Text = "只顯示有效單";
            this.chkValid.UseVisualStyleBackColor = true;
            this.chkValid.CheckedChanged += new System.EventHandler(this.chkValid_CheckedChanged);
            // 
            // frmOrderReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 377);
            this.Controls.Add(this.olvOrder);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOrderReport";
            this.Text = "委託";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrderReport_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvOrder)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gupAction.ResumeLayout(false);
            this.gupAction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvOrder;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.GroupBox gupAction;
        private System.Windows.Forms.RadioButton radRenderer;
        private System.Windows.Forms.RadioButton radFilter;
        private System.Windows.Forms.RadioButton radNone;
        private System.Windows.Forms.CheckBox chkValid;
        private BrightIdeasSoftware.OLVColumn olvcIndex;
        private BrightIdeasSoftware.OLVColumn olvcHistoric;
        private BrightIdeasSoftware.OLVColumn olvcOrderID;
        private BrightIdeasSoftware.OLVColumn olvcExchOrderID;
        private BrightIdeasSoftware.OLVColumn olvcTraderAccount;
        private BrightIdeasSoftware.OLVColumn olvcOrderType;
        private BrightIdeasSoftware.OLVColumn olvcExchangeName;
        private BrightIdeasSoftware.OLVColumn olvcContractName;
        private BrightIdeasSoftware.OLVColumn olvcContractDate;
        private BrightIdeasSoftware.OLVColumn olvcBuyOrSell;
        private BrightIdeasSoftware.OLVColumn olvcPrice;
        private BrightIdeasSoftware.OLVColumn olvcLots;
        private BrightIdeasSoftware.OLVColumn olvcAmountFilled;
        private BrightIdeasSoftware.OLVColumn olvcNoOfFills;
        private BrightIdeasSoftware.OLVColumn olvcAveragePrice;
        private BrightIdeasSoftware.OLVColumn olvcStatus;
        private BrightIdeasSoftware.OLVColumn olvcOpenOrClose;
        private BrightIdeasSoftware.OLVColumn olvcTimeSent;
        private BrightIdeasSoftware.OLVColumn olvcTimeHostRecd;
        private BrightIdeasSoftware.OLVColumn olvcTimeExchRecd;
        private BrightIdeasSoftware.OLVColumn olvcTimeExchAckn;
        private BrightIdeasSoftware.OLVColumn olvcNonExecReason;
        private BrightIdeasSoftware.OLVColumn olvcKey;
    }
}