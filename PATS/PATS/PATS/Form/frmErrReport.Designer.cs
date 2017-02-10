namespace PATS
{
    partial class frmErrReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmErrReport));
            this.olvError = new BrightIdeasSoftware.ObjectListView();
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
            ((System.ComponentModel.ISupportInitialize)(this.olvError)).BeginInit();
            this.SuspendLayout();
            // 
            // olvError
            // 
            this.olvError.AllColumns.Add(this.olvcIndex);
            this.olvError.AllColumns.Add(this.olvcHistoric);
            this.olvError.AllColumns.Add(this.olvcOrderID);
            this.olvError.AllColumns.Add(this.olvcExchOrderID);
            this.olvError.AllColumns.Add(this.olvcTraderAccount);
            this.olvError.AllColumns.Add(this.olvcOrderType);
            this.olvError.AllColumns.Add(this.olvcKey);
            this.olvError.AllColumns.Add(this.olvcExchangeName);
            this.olvError.AllColumns.Add(this.olvcContractName);
            this.olvError.AllColumns.Add(this.olvcContractDate);
            this.olvError.AllColumns.Add(this.olvcBuyOrSell);
            this.olvError.AllColumns.Add(this.olvcPrice);
            this.olvError.AllColumns.Add(this.olvcLots);
            this.olvError.AllColumns.Add(this.olvcAmountFilled);
            this.olvError.AllColumns.Add(this.olvcNoOfFills);
            this.olvError.AllColumns.Add(this.olvcAveragePrice);
            this.olvError.AllColumns.Add(this.olvcStatus);
            this.olvError.AllColumns.Add(this.olvcOpenOrClose);
            this.olvError.AllColumns.Add(this.olvcTimeSent);
            this.olvError.AllColumns.Add(this.olvcTimeHostRecd);
            this.olvError.AllColumns.Add(this.olvcTimeExchRecd);
            this.olvError.AllColumns.Add(this.olvcTimeExchAckn);
            this.olvError.AllColumns.Add(this.olvcNonExecReason);
            this.olvError.AllowColumnReorder = true;
            this.olvError.CellEditUseWholeCell = false;
            this.olvError.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
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
            this.olvError.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvError.EmptyListMsg = "無錯誤資料";
            this.olvError.EmptyListMsgFont = new System.Drawing.Font("Verdana", 8F);
            this.olvError.FullRowSelect = true;
            this.olvError.GroupWithItemCountFormat = "";
            this.olvError.HeaderWordWrap = true;
            this.olvError.HideSelection = false;
            this.olvError.Location = new System.Drawing.Point(0, 0);
            this.olvError.Name = "olvError";
            this.olvError.ShowCommandMenuOnRightClick = true;
            this.olvError.ShowGroups = false;
            this.olvError.ShowItemCountOnGroups = true;
            this.olvError.ShowItemToolTips = true;
            this.olvError.Size = new System.Drawing.Size(635, 377);
            this.olvError.SortGroupItemsByPrimaryColumn = false;
            this.olvError.TabIndex = 0;
            this.olvError.TintSortColumn = true;
            this.olvError.UseCellFormatEvents = true;
            this.olvError.UseCompatibleStateImageBehavior = false;
            this.olvError.UseFilterIndicator = true;
            this.olvError.UseFiltering = true;
            this.olvError.UseHotItem = true;
            this.olvError.UseNotifyPropertyChanged = true;
            this.olvError.UseTranslucentHotItem = true;
            this.olvError.UseTranslucentSelection = true;
            this.olvError.View = System.Windows.Forms.View.Details;
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
            // frmErrReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(635, 377);
            this.Controls.Add(this.olvError);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmErrReport";
            this.Text = "錯誤";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrderReport_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvError)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvError;
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