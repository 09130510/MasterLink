namespace PATS
{
    partial class frmCancelReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCancelReport));
            this.olvCancel = new BrightIdeasSoftware.ObjectListView();
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
            ((System.ComponentModel.ISupportInitialize)(this.olvCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // olvCancel
            // 
            this.olvCancel.AllColumns.Add(this.olvcIndex);
            this.olvCancel.AllColumns.Add(this.olvcHistoric);
            this.olvCancel.AllColumns.Add(this.olvcOrderID);
            this.olvCancel.AllColumns.Add(this.olvcExchOrderID);
            this.olvCancel.AllColumns.Add(this.olvcTraderAccount);
            this.olvCancel.AllColumns.Add(this.olvcOrderType);
            this.olvCancel.AllColumns.Add(this.olvcKey);
            this.olvCancel.AllColumns.Add(this.olvcExchangeName);
            this.olvCancel.AllColumns.Add(this.olvcContractName);
            this.olvCancel.AllColumns.Add(this.olvcContractDate);
            this.olvCancel.AllColumns.Add(this.olvcBuyOrSell);
            this.olvCancel.AllColumns.Add(this.olvcPrice);
            this.olvCancel.AllColumns.Add(this.olvcLots);
            this.olvCancel.AllColumns.Add(this.olvcAmountFilled);
            this.olvCancel.AllColumns.Add(this.olvcNoOfFills);
            this.olvCancel.AllColumns.Add(this.olvcAveragePrice);
            this.olvCancel.AllColumns.Add(this.olvcStatus);
            this.olvCancel.AllColumns.Add(this.olvcOpenOrClose);
            this.olvCancel.AllColumns.Add(this.olvcTimeSent);
            this.olvCancel.AllColumns.Add(this.olvcTimeHostRecd);
            this.olvCancel.AllColumns.Add(this.olvcTimeExchRecd);
            this.olvCancel.AllColumns.Add(this.olvcTimeExchAckn);
            this.olvCancel.AllColumns.Add(this.olvcNonExecReason);
            this.olvCancel.AllowColumnReorder = true;
            this.olvCancel.CellEditUseWholeCell = false;
            this.olvCancel.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
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
            this.olvCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvCancel.EmptyListMsg = "無取消資料";
            this.olvCancel.EmptyListMsgFont = new System.Drawing.Font("Verdana", 8F);
            this.olvCancel.FullRowSelect = true;
            this.olvCancel.GroupWithItemCountFormat = "";
            this.olvCancel.HeaderWordWrap = true;
            this.olvCancel.HideSelection = false;
            this.olvCancel.Location = new System.Drawing.Point(0, 0);
            this.olvCancel.Name = "olvCancel";
            this.olvCancel.ShowCommandMenuOnRightClick = true;
            this.olvCancel.ShowGroups = false;
            this.olvCancel.ShowItemCountOnGroups = true;
            this.olvCancel.ShowItemToolTips = true;
            this.olvCancel.Size = new System.Drawing.Size(635, 377);
            this.olvCancel.SortGroupItemsByPrimaryColumn = false;
            this.olvCancel.TabIndex = 0;
            this.olvCancel.TintSortColumn = true;
            this.olvCancel.UseCellFormatEvents = true;
            this.olvCancel.UseCompatibleStateImageBehavior = false;
            this.olvCancel.UseFilterIndicator = true;
            this.olvCancel.UseFiltering = true;
            this.olvCancel.UseHotItem = true;
            this.olvCancel.UseNotifyPropertyChanged = true;
            this.olvCancel.UseTranslucentHotItem = true;
            this.olvCancel.UseTranslucentSelection = true;
            this.olvCancel.View = System.Windows.Forms.View.Details;
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
            // frmCancelReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 377);
            this.Controls.Add(this.olvCancel);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCancelReport";
            this.Text = "取消";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrderReport_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvCancel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvCancel;
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