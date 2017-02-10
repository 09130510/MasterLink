namespace PATSOrder
{
    partial class frmDealReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDealReport));
            this.olvDeal = new BrightIdeasSoftware.ObjectListView();
            this.olvcOrderID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcFillId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTraderAccount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExch = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcContract = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcContractDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExchOrderId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExchFillId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcFillType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBuyOrSell = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPrice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcLots = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTimeFilled = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTimeHostRecd = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.olvDeal)).BeginInit();
            this.SuspendLayout();
            // 
            // olvDeal
            // 
            this.olvDeal.AllColumns.Add(this.olvcOrderID);
            this.olvDeal.AllColumns.Add(this.olvcFillId);
            this.olvDeal.AllColumns.Add(this.olvcTraderAccount);
            this.olvDeal.AllColumns.Add(this.olvcExch);
            this.olvDeal.AllColumns.Add(this.olvcContract);
            this.olvDeal.AllColumns.Add(this.olvcContractDate);
            this.olvDeal.AllColumns.Add(this.olvcExchOrderId);
            this.olvDeal.AllColumns.Add(this.olvcExchFillId);
            this.olvDeal.AllColumns.Add(this.olvcFillType);
            this.olvDeal.AllColumns.Add(this.olvcBuyOrSell);
            this.olvDeal.AllColumns.Add(this.olvcPrice);
            this.olvDeal.AllColumns.Add(this.olvcLots);
            this.olvDeal.AllColumns.Add(this.olvcTimeFilled);
            this.olvDeal.AllColumns.Add(this.olvcTimeHostRecd);
            this.olvDeal.AllowColumnReorder = true;
            this.olvDeal.CellEditUseWholeCell = false;
            this.olvDeal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcOrderID,
            this.olvcTraderAccount,
            this.olvcExch,
            this.olvcContract,
            this.olvcContractDate,
            this.olvcBuyOrSell,
            this.olvcPrice,
            this.olvcLots,
            this.olvcTimeFilled,
            this.olvcTimeHostRecd});
            this.olvDeal.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvDeal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvDeal.EmptyListMsg = "無成交資料";
            this.olvDeal.EmptyListMsgFont = new System.Drawing.Font("Verdana", 8F);
            this.olvDeal.FullRowSelect = true;
            this.olvDeal.GroupWithItemCountFormat = "";
            this.olvDeal.HideSelection = false;
            this.olvDeal.Location = new System.Drawing.Point(0, 0);
            this.olvDeal.Name = "olvDeal";
            this.olvDeal.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.olvDeal.ShowCommandMenuOnRightClick = true;
            this.olvDeal.ShowGroups = false;
            this.olvDeal.ShowItemCountOnGroups = true;
            this.olvDeal.ShowItemToolTips = true;
            this.olvDeal.Size = new System.Drawing.Size(629, 328);
            this.olvDeal.SortGroupItemsByPrimaryColumn = false;
            this.olvDeal.TabIndex = 0;
            this.olvDeal.TintSortColumn = true;
            this.olvDeal.UseCellFormatEvents = true;
            this.olvDeal.UseCompatibleStateImageBehavior = false;
            this.olvDeal.UseExplorerTheme = true;
            this.olvDeal.UseFilterIndicator = true;
            this.olvDeal.UseFiltering = true;
            this.olvDeal.UseHotItem = true;
            this.olvDeal.UseNotifyPropertyChanged = true;
            this.olvDeal.UseTranslucentHotItem = true;
            this.olvDeal.UseTranslucentSelection = true;
            this.olvDeal.View = System.Windows.Forms.View.Details;
            // 
            // olvcOrderID
            // 
            this.olvcOrderID.AspectName = "OrderID";
            this.olvcOrderID.AutoCompleteEditor = false;
            this.olvcOrderID.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcOrderID.GroupWithItemCountFormat = "";
            this.olvcOrderID.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcOrderID.IsEditable = false;
            this.olvcOrderID.Text = "委託代碼";
            this.olvcOrderID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcOrderID.Width = 80;
            // 
            // olvcFillId
            // 
            this.olvcFillId.AspectName = "FillId";
            this.olvcFillId.AutoCompleteEditor = false;
            this.olvcFillId.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcFillId.DisplayIndex = 1;
            this.olvcFillId.Groupable = false;
            this.olvcFillId.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcFillId.IsEditable = false;
            this.olvcFillId.IsVisible = false;
            this.olvcFillId.Text = "成交代碼";
            // 
            // olvcTraderAccount
            // 
            this.olvcTraderAccount.AspectName = "TraderAccount";
            this.olvcTraderAccount.AutoCompleteEditor = false;
            this.olvcTraderAccount.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcTraderAccount.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcTraderAccount.IsEditable = false;
            this.olvcTraderAccount.Text = "帳號";
            this.olvcTraderAccount.Width = 75;
            // 
            // olvcExch
            // 
            this.olvcExch.AspectName = "ExchangeName";
            this.olvcExch.AutoCompleteEditor = false;
            this.olvcExch.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcExch.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcExch.IsEditable = false;
            this.olvcExch.Text = "交易所";
            // 
            // olvcContract
            // 
            this.olvcContract.AspectName = "ContractName";
            this.olvcContract.AutoCompleteEditor = false;
            this.olvcContract.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcContract.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcContract.IsEditable = false;
            this.olvcContract.Text = "商品";
            // 
            // olvcContractDate
            // 
            this.olvcContractDate.AspectName = "ContractDate";
            this.olvcContractDate.AutoCompleteEditor = false;
            this.olvcContractDate.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcContractDate.Groupable = false;
            this.olvcContractDate.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcContractDate.IsEditable = false;
            this.olvcContractDate.Text = "年月";
            // 
            // olvcExchOrderId
            // 
            this.olvcExchOrderId.AspectName = "ExchOrderId";
            this.olvcExchOrderId.AutoCompleteEditor = false;
            this.olvcExchOrderId.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcExchOrderId.DisplayIndex = 6;
            this.olvcExchOrderId.Groupable = false;
            this.olvcExchOrderId.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcExchOrderId.IsEditable = false;
            this.olvcExchOrderId.IsVisible = false;
            this.olvcExchOrderId.Text = "委託書號";
            // 
            // olvcExchFillId
            // 
            this.olvcExchFillId.AspectName = "ExchangeFillID";
            this.olvcExchFillId.AutoCompleteEditor = false;
            this.olvcExchFillId.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcExchFillId.DisplayIndex = 7;
            this.olvcExchFillId.Groupable = false;
            this.olvcExchFillId.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcExchFillId.IsEditable = false;
            this.olvcExchFillId.IsVisible = false;
            this.olvcExchFillId.Text = "成交代碼";
            // 
            // olvcFillType
            // 
            this.olvcFillType.AspectName = "FillType";
            this.olvcFillType.AutoCompleteEditor = false;
            this.olvcFillType.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcFillType.DisplayIndex = 8;
            this.olvcFillType.Groupable = false;
            this.olvcFillType.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcFillType.IsEditable = false;
            this.olvcFillType.IsVisible = false;
            this.olvcFillType.Text = "成交種類";
            // 
            // olvcBuyOrSell
            // 
            this.olvcBuyOrSell.AspectName = "BuyOrSell";
            this.olvcBuyOrSell.AutoCompleteEditor = false;
            this.olvcBuyOrSell.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcBuyOrSell.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcBuyOrSell.IsEditable = false;
            this.olvcBuyOrSell.Text = "買賣";
            this.olvcBuyOrSell.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBuyOrSell.Width = 40;
            // 
            // olvcPrice
            // 
            this.olvcPrice.AspectName = "Price";
            this.olvcPrice.AspectToStringFormat = "{0:#,##0.00}";
            this.olvcPrice.AutoCompleteEditor = false;
            this.olvcPrice.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcPrice.Groupable = false;
            this.olvcPrice.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcPrice.IsEditable = false;
            this.olvcPrice.Text = "價格";
            this.olvcPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // olvcLots
            // 
            this.olvcLots.AspectName = "Lots";
            this.olvcLots.AspectToStringFormat = "{0:#,##0}";
            this.olvcLots.AutoCompleteEditor = false;
            this.olvcLots.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcLots.Groupable = false;
            this.olvcLots.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcLots.Text = "口數";
            this.olvcLots.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcLots.Width = 45;
            // 
            // olvcTimeFilled
            // 
            this.olvcTimeFilled.AspectName = "TimeFilled";
            this.olvcTimeFilled.AutoCompleteEditor = false;
            this.olvcTimeFilled.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcTimeFilled.Groupable = false;
            this.olvcTimeFilled.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcTimeFilled.IsEditable = false;
            this.olvcTimeFilled.Text = "時間";
            // 
            // olvcTimeHostRecd
            // 
            this.olvcTimeHostRecd.AspectName = "TimeHostRecd";
            this.olvcTimeHostRecd.AutoCompleteEditor = false;
            this.olvcTimeHostRecd.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcTimeHostRecd.Groupable = false;
            this.olvcTimeHostRecd.HeaderFont = new System.Drawing.Font("Verdana", 8F);
            this.olvcTimeHostRecd.IsEditable = false;
            this.olvcTimeHostRecd.Text = "主機時間";
            this.olvcTimeHostRecd.Width = 70;
            // 
            // frmDealReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 328);
            this.Controls.Add(this.olvDeal);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDealReport";
            this.Text = "  成交  ";
            this.Load += new System.EventHandler(this.frmDealReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.olvDeal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvDeal;
        private BrightIdeasSoftware.OLVColumn olvcTimeFilled;
        private BrightIdeasSoftware.OLVColumn olvcExch;
        private BrightIdeasSoftware.OLVColumn olvcContract;
        private BrightIdeasSoftware.OLVColumn olvcContractDate;
        private BrightIdeasSoftware.OLVColumn olvcOrderID;
        private BrightIdeasSoftware.OLVColumn olvcExchOrderId;
        private BrightIdeasSoftware.OLVColumn olvcFillId;
        private BrightIdeasSoftware.OLVColumn olvcExchFillId;
        private BrightIdeasSoftware.OLVColumn olvcFillType;
        private BrightIdeasSoftware.OLVColumn olvcBuyOrSell;
        private BrightIdeasSoftware.OLVColumn olvcPrice;
        private BrightIdeasSoftware.OLVColumn olvcLots;
        private BrightIdeasSoftware.OLVColumn olvcTraderAccount;
        private BrightIdeasSoftware.OLVColumn olvcTimeHostRecd;
    }
}