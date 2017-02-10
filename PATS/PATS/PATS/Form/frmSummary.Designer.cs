namespace PATS
{
    partial class frmSummary
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSummary));
            this.olvSummary = new BrightIdeasSoftware.ObjectListView();
            this.olvcTraderAccount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcContractKey = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExchangeName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcContractName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcContractDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcALots = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBLots = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcAAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.validatorBase1 = new DevAge.ComponentModel.Validator.ValidatorBase();
            this.highlightTextRenderer1 = new BrightIdeasSoftware.HighlightTextRenderer();
            ((System.ComponentModel.ISupportInitialize)(this.olvSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // olvSummary
            // 
            this.olvSummary.AllColumns.Add(this.olvcContractKey);
            this.olvSummary.AllColumns.Add(this.olvcTraderAccount);
            this.olvSummary.AllColumns.Add(this.olvcExchangeName);
            this.olvSummary.AllColumns.Add(this.olvcContractName);
            this.olvSummary.AllColumns.Add(this.olvcContractDate);
            this.olvSummary.AllColumns.Add(this.olvcALots);
            this.olvSummary.AllColumns.Add(this.olvcBLots);
            this.olvSummary.AllColumns.Add(this.olvcAAmount);
            this.olvSummary.AllColumns.Add(this.olvcBAmount);
            this.olvSummary.AllowColumnReorder = true;
            this.olvSummary.CellEditUseWholeCell = false;
            this.olvSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcContractKey,
            this.olvcALots,
            this.olvcBLots,
            this.olvcAAmount,
            this.olvcBAmount});
            this.olvSummary.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvSummary.EmptyListMsg = "無彙總資料";
            this.olvSummary.EmptyListMsgFont = new System.Drawing.Font("Verdana", 8F);
            this.olvSummary.FullRowSelect = true;
            this.olvSummary.GroupWithItemCountFormat = "";
            this.olvSummary.HeaderWordWrap = true;
            this.olvSummary.HideSelection = false;
            this.olvSummary.IncludeColumnHeadersInCopy = true;
            this.olvSummary.IncludeHiddenColumnsInDataTransfer = true;
            this.olvSummary.Location = new System.Drawing.Point(0, 0);
            this.olvSummary.Name = "olvSummary";
            this.olvSummary.PersistentCheckBoxes = false;
            this.olvSummary.ShowCommandMenuOnRightClick = true;
            this.olvSummary.ShowGroups = false;
            this.olvSummary.ShowHeaderInAllViews = false;
            this.olvSummary.ShowItemToolTips = true;
            this.olvSummary.Size = new System.Drawing.Size(438, 284);
            this.olvSummary.SortGroupItemsByPrimaryColumn = false;
            this.olvSummary.TabIndex = 1;
            this.olvSummary.TintSortColumn = true;
            this.olvSummary.UseCellFormatEvents = true;
            this.olvSummary.UseCompatibleStateImageBehavior = false;
            this.olvSummary.UseFilterIndicator = true;
            this.olvSummary.UseFiltering = true;
            this.olvSummary.UseHotItem = true;
            this.olvSummary.UseNotifyPropertyChanged = true;
            this.olvSummary.UseTranslucentHotItem = true;
            this.olvSummary.UseTranslucentSelection = true;
            this.olvSummary.View = System.Windows.Forms.View.Details;
            // 
            // olvcTraderAccount
            // 
            this.olvcTraderAccount.AspectName = "TraderAccount";
            this.olvcTraderAccount.AutoCompleteEditor = false;
            this.olvcTraderAccount.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcTraderAccount.DisplayIndex = 1;
            this.olvcTraderAccount.Groupable = false;
            this.olvcTraderAccount.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcTraderAccount.IsEditable = false;
            this.olvcTraderAccount.IsVisible = false;
            this.olvcTraderAccount.Text = "帳號";
            this.olvcTraderAccount.Width = 75;
            // 
            // olvcContractKey
            // 
            this.olvcContractKey.AspectName = "Key";
            this.olvcContractKey.AutoCompleteEditor = false;
            this.olvcContractKey.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcContractKey.Groupable = false;
            this.olvcContractKey.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcContractKey.IsEditable = false;
            this.olvcContractKey.Text = "商品";
            this.olvcContractKey.Width = 100;
            // 
            // olvcExchangeName
            // 
            this.olvcExchangeName.AspectName = "ExchangeName";
            this.olvcExchangeName.AutoCompleteEditor = false;
            this.olvcExchangeName.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcExchangeName.DisplayIndex = 2;
            this.olvcExchangeName.Groupable = false;
            this.olvcExchangeName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcExchangeName.IsEditable = false;
            this.olvcExchangeName.IsVisible = false;
            this.olvcExchangeName.Text = "交易所";
            // 
            // olvcContractName
            // 
            this.olvcContractName.AspectName = "ContractName";
            this.olvcContractName.AutoCompleteEditor = false;
            this.olvcContractName.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcContractName.DisplayIndex = 3;
            this.olvcContractName.Groupable = false;
            this.olvcContractName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcContractName.IsEditable = false;
            this.olvcContractName.IsVisible = false;
            this.olvcContractName.Text = "商品名稱";
            // 
            // olvcContractDate
            // 
            this.olvcContractDate.AspectName = "ContractDate";
            this.olvcContractDate.AutoCompleteEditor = false;
            this.olvcContractDate.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcContractDate.DisplayIndex = 4;
            this.olvcContractDate.Groupable = false;
            this.olvcContractDate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcContractDate.IsEditable = false;
            this.olvcContractDate.IsVisible = false;
            this.olvcContractDate.Text = "年月";
            // 
            // olvcALots
            // 
            this.olvcALots.AspectName = "ALots";
            this.olvcALots.AutoCompleteEditor = false;
            this.olvcALots.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcALots.Groupable = false;
            this.olvcALots.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcALots.IsEditable = false;
            this.olvcALots.Text = "賣量";
            this.olvcALots.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcALots.Width = 45;
            // 
            // olvcBLots
            // 
            this.olvcBLots.AspectName = "BLots";
            this.olvcBLots.AutoCompleteEditor = false;
            this.olvcBLots.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcBLots.Groupable = false;
            this.olvcBLots.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBLots.IsEditable = false;
            this.olvcBLots.Text = "買量";
            this.olvcBLots.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcBLots.Width = 45;
            // 
            // olvcAAmount
            // 
            this.olvcAAmount.AspectName = "AAmount";
            this.olvcAAmount.AspectToStringFormat = "{0:#,##0.00}";
            this.olvcAAmount.AutoCompleteEditor = false;
            this.olvcAAmount.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcAAmount.Groupable = false;
            this.olvcAAmount.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcAAmount.IsEditable = false;
            this.olvcAAmount.Text = "賣金額";
            this.olvcAAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // olvcBAmount
            // 
            this.olvcBAmount.AspectName = "BAmount";
            this.olvcBAmount.AspectToStringFormat = "{0:#,##0.00}";
            this.olvcBAmount.AutoCompleteEditor = false;
            this.olvcBAmount.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcBAmount.Groupable = false;
            this.olvcBAmount.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBAmount.IsEditable = false;
            this.olvcBAmount.Text = "買金額";
            this.olvcBAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // frmSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 284);
            this.Controls.Add(this.olvSummary);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSummary";
            this.Text = "彙總";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSummary_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvSummary;
        private BrightIdeasSoftware.OLVColumn olvcTraderAccount;
        private BrightIdeasSoftware.OLVColumn olvcContractKey;
        private BrightIdeasSoftware.OLVColumn olvcExchangeName;
        private BrightIdeasSoftware.OLVColumn olvcContractName;
        private BrightIdeasSoftware.OLVColumn olvcContractDate;
        private BrightIdeasSoftware.OLVColumn olvcALots;
        private BrightIdeasSoftware.OLVColumn olvcBLots;
        private BrightIdeasSoftware.OLVColumn olvcAAmount;
        private BrightIdeasSoftware.OLVColumn olvcBAmount;
        private DevAge.ComponentModel.Validator.ValidatorBase validatorBase1;
        private BrightIdeasSoftware.HighlightTextRenderer highlightTextRenderer1;
    }
}