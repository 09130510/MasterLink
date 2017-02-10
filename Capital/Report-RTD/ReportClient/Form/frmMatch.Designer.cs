using System;
using Capital.Report.Class;
namespace Capital.Report
{
    partial class frmMatch
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
            this.olvMatch = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOrdNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcCustNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcMarketType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOrderErr = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcExchangeID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBrokerID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcComID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcStrike = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOrderType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBuySell = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPriceType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcPrice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcMatchAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBeforeQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcAfterQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSumQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcKeyNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOkSeq = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSaleNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSubID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.olvMatch)).BeginInit();
            this.SuspendLayout();
            // 
            // olvMatch
            // 
            this.olvMatch.AllColumns.Add(this.olvColumn1);
            this.olvMatch.AllColumns.Add(this.olvcOrdNo);
            this.olvMatch.AllColumns.Add(this.olvcCustNo);
            this.olvMatch.AllColumns.Add(this.olvcMarketType);
            this.olvMatch.AllColumns.Add(this.olvcOrderErr);
            this.olvMatch.AllColumns.Add(this.olvcExchangeID);
            this.olvMatch.AllColumns.Add(this.olvcBrokerID);
            this.olvMatch.AllColumns.Add(this.olvcComID);
            this.olvMatch.AllColumns.Add(this.olvcStrike);
            this.olvMatch.AllColumns.Add(this.olvcOrderType);
            this.olvMatch.AllColumns.Add(this.olvcBuySell);
            this.olvMatch.AllColumns.Add(this.olvcPriceType);
            this.olvMatch.AllColumns.Add(this.olvcPrice);
            this.olvMatch.AllColumns.Add(this.olvcQty);
            this.olvMatch.AllColumns.Add(this.olvcMatchAmount);
            this.olvMatch.AllColumns.Add(this.olvcBeforeQty);
            this.olvMatch.AllColumns.Add(this.olvcAfterQty);
            this.olvMatch.AllColumns.Add(this.olvcSumQty);
            this.olvMatch.AllColumns.Add(this.olvcDate);
            this.olvMatch.AllColumns.Add(this.olvcTime);
            this.olvMatch.AllColumns.Add(this.olvcKeyNo);
            this.olvMatch.AllColumns.Add(this.olvcOkSeq);
            this.olvMatch.AllColumns.Add(this.olvcSaleNo);
            this.olvMatch.AllColumns.Add(this.olvcSubID);
            this.olvMatch.AllowColumnReorder = true;
            this.olvMatch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvcOrdNo,
            this.olvcCustNo,
            this.olvcComID,
            this.olvcBuySell,
            this.olvcPrice,
            this.olvcQty,
            this.olvcMatchAmount,
            this.olvcTime});
            this.olvMatch.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvMatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvMatch.EmptyListMsg = "無成交資料";
            this.olvMatch.EmptyListMsgFont = new System.Drawing.Font("Verdana", 8F);
            this.olvMatch.Font = new System.Drawing.Font("Verdana", 8F);
            this.olvMatch.FullRowSelect = true;
            this.olvMatch.HeaderUsesThemes = false;
            this.olvMatch.Location = new System.Drawing.Point(0, 0);
            this.olvMatch.Name = "olvMatch";
            this.olvMatch.OwnerDraw = true;
            this.olvMatch.ShowCommandMenuOnRightClick = true;
            this.olvMatch.ShowGroups = false;
            this.olvMatch.ShowItemCountOnGroups = true;
            this.olvMatch.Size = new System.Drawing.Size(1004, 288);
            this.olvMatch.TabIndex = 0;
            this.olvMatch.UseCellFormatEvents = true;
            this.olvMatch.UseCompatibleStateImageBehavior = false;
            this.olvMatch.UseCustomSelectionColors = true;
            this.olvMatch.UseFilterIndicator = true;
            this.olvMatch.UseFiltering = true;
            this.olvMatch.UseHotItem = true;
            this.olvMatch.UseNotifyPropertyChanged = true;
            this.olvMatch.UseTranslucentHotItem = true;
            this.olvMatch.UseTranslucentSelection = true;
            this.olvMatch.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.CellPadding = null;
            this.olvColumn1.Groupable = false;
            this.olvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.Hideable = false;
            this.olvColumn1.Searchable = false;
            this.olvColumn1.ShowTextInHeader = false;
            this.olvColumn1.Sortable = false;
            this.olvColumn1.Text = "";
            this.olvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.UseFiltering = false;
            this.olvColumn1.Width = 5;
            // 
            // olvcOrdNo
            // 
            this.olvcOrdNo.AspectName = "OrdNo";
            this.olvcOrdNo.AutoCompleteEditor = false;
            this.olvcOrdNo.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcOrdNo.CellPadding = null;
            this.olvcOrdNo.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOrdNo.Hideable = false;
            this.olvcOrdNo.IsEditable = false;
            this.olvcOrdNo.Text = "委託書號";
            this.olvcOrdNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOrdNo.Width = 65;
            // 
            // olvcCustNo
            // 
            this.olvcCustNo.AspectName = "CustNo";
            this.olvcCustNo.AutoCompleteEditor = false;
            this.olvcCustNo.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcCustNo.CellPadding = null;
            this.olvcCustNo.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcCustNo.IsEditable = false;
            this.olvcCustNo.Text = "交易帳號";
            this.olvcCustNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcCustNo.Width = 65;
            // 
            // olvcMarketType
            // 
            this.olvcMarketType.AspectName = "MarketType";
            this.olvcMarketType.AutoCompleteEditor = false;
            this.olvcMarketType.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcMarketType.CellPadding = null;
            this.olvcMarketType.DisplayIndex = 2;
            this.olvcMarketType.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcMarketType.IsEditable = false;
            this.olvcMarketType.IsVisible = false;
            this.olvcMarketType.Text = "市場別";
            this.olvcMarketType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcOrderErr
            // 
            this.olvcOrderErr.AspectName = "OrderErr";
            this.olvcOrderErr.AutoCompleteEditor = false;
            this.olvcOrderErr.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcOrderErr.CellPadding = null;
            this.olvcOrderErr.DisplayIndex = 2;
            this.olvcOrderErr.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOrderErr.IsEditable = false;
            this.olvcOrderErr.IsVisible = false;
            this.olvcOrderErr.Text = "錯誤";
            this.olvcOrderErr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcExchangeID
            // 
            this.olvcExchangeID.AspectName = "ExchangeID";
            this.olvcExchangeID.AutoCompleteEditor = false;
            this.olvcExchangeID.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcExchangeID.CellPadding = null;
            this.olvcExchangeID.DisplayIndex = 3;
            this.olvcExchangeID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcExchangeID.IsEditable = false;
            this.olvcExchangeID.IsVisible = false;
            this.olvcExchangeID.Text = "交易所";
            this.olvcExchangeID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcBrokerID
            // 
            this.olvcBrokerID.AspectName = "BrokerID";
            this.olvcBrokerID.AutoCompleteEditor = false;
            this.olvcBrokerID.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcBrokerID.CellPadding = null;
            this.olvcBrokerID.DisplayIndex = 3;
            this.olvcBrokerID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBrokerID.IsEditable = false;
            this.olvcBrokerID.IsVisible = false;
            this.olvcBrokerID.Text = "上手";
            this.olvcBrokerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.olvcComID.Width = 80;
            // 
            // olvcStrike
            // 
            this.olvcStrike.AspectName = "Strike";
            this.olvcStrike.AutoCompleteEditor = false;
            this.olvcStrike.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcStrike.CellPadding = null;
            this.olvcStrike.DisplayIndex = 4;
            this.olvcStrike.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcStrike.IsEditable = false;
            this.olvcStrike.IsVisible = false;
            this.olvcStrike.Text = "履約價";
            this.olvcStrike.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcOrderType
            // 
            this.olvcOrderType.AspectName = "OrderType";
            this.olvcOrderType.AutoCompleteEditor = false;
            this.olvcOrderType.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcOrderType.CellPadding = null;
            this.olvcOrderType.DisplayIndex = 4;
            this.olvcOrderType.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOrderType.IsEditable = false;
            this.olvcOrderType.IsVisible = false;
            this.olvcOrderType.Text = "委託種類";
            this.olvcOrderType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOrderType.Width = 65;
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
            // 
            // olvcPriceType
            // 
            this.olvcPriceType.AspectName = "PriceType";
            this.olvcPriceType.AutoCompleteEditor = false;
            this.olvcPriceType.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcPriceType.CellPadding = null;
            this.olvcPriceType.DisplayIndex = 6;
            this.olvcPriceType.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcPriceType.IsEditable = false;
            this.olvcPriceType.IsVisible = false;
            this.olvcPriceType.Text = "價別";
            this.olvcPriceType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcPrice
            // 
            this.olvcPrice.AspectName = "Price";
            this.olvcPrice.AspectToStringFormat = "{0:#.00}";
            this.olvcPrice.AutoCompleteEditor = false;
            this.olvcPrice.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcPrice.CellPadding = null;
            this.olvcPrice.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcPrice.IsEditable = false;
            this.olvcPrice.Text = "價格";
            this.olvcPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcQty
            // 
            this.olvcQty.AspectName = "Qty";
            this.olvcQty.AutoCompleteEditor = false;
            this.olvcQty.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcQty.CellPadding = null;
            this.olvcQty.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcQty.IsEditable = false;
            this.olvcQty.Text = "口數";
            this.olvcQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcMatchAmount
            // 
            this.olvcMatchAmount.AspectName = "MatchAmount";
            this.olvcMatchAmount.AspectToStringFormat = "{0:#.00}";
            this.olvcMatchAmount.AutoCompleteEditor = false;
            this.olvcMatchAmount.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcMatchAmount.CellPadding = null;
            this.olvcMatchAmount.Groupable = false;
            this.olvcMatchAmount.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcMatchAmount.IsEditable = false;
            this.olvcMatchAmount.Text = "成交金額";
            this.olvcMatchAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcMatchAmount.Width = 70;
            // 
            // olvcBeforeQty
            // 
            this.olvcBeforeQty.AspectName = "BeforeQty";
            this.olvcBeforeQty.AutoCompleteEditor = false;
            this.olvcBeforeQty.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcBeforeQty.CellPadding = null;
            this.olvcBeforeQty.DisplayIndex = 7;
            this.olvcBeforeQty.Groupable = false;
            this.olvcBeforeQty.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBeforeQty.IsEditable = false;
            this.olvcBeforeQty.IsVisible = false;
            this.olvcBeforeQty.Text = "異動前數量";
            this.olvcBeforeQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcAfterQty
            // 
            this.olvcAfterQty.AspectName = "AfterQty";
            this.olvcAfterQty.AutoCompleteEditor = false;
            this.olvcAfterQty.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcAfterQty.CellPadding = null;
            this.olvcAfterQty.DisplayIndex = 7;
            this.olvcAfterQty.Groupable = false;
            this.olvcAfterQty.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcAfterQty.IsEditable = false;
            this.olvcAfterQty.IsVisible = false;
            this.olvcAfterQty.Text = "異動後數量";
            this.olvcAfterQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcSumQty
            // 
            this.olvcSumQty.AspectName = "SumQty";
            this.olvcSumQty.AutoCompleteEditor = false;
            this.olvcSumQty.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcSumQty.CellPadding = null;
            this.olvcSumQty.DisplayIndex = 8;
            this.olvcSumQty.Groupable = false;
            this.olvcSumQty.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSumQty.IsEditable = false;
            this.olvcSumQty.IsVisible = false;
            this.olvcSumQty.Text = "成交口數";
            this.olvcSumQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcDate
            // 
            this.olvcDate.AspectName = "Date";
            this.olvcDate.AutoCompleteEditor = false;
            this.olvcDate.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcDate.CellPadding = null;
            this.olvcDate.DisplayIndex = 7;
            this.olvcDate.Groupable = false;
            this.olvcDate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcDate.IsEditable = false;
            this.olvcDate.IsVisible = false;
            this.olvcDate.Text = "日期";
            this.olvcDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcTime
            // 
            this.olvcTime.AspectName = "Time";
            this.olvcTime.AutoCompleteEditor = false;
            this.olvcTime.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcTime.CellPadding = null;
            this.olvcTime.Groupable = false;
            this.olvcTime.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcTime.IsEditable = false;
            this.olvcTime.Text = "時間";
            this.olvcTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcTime.Width = 65;
            // 
            // olvcKeyNo
            // 
            this.olvcKeyNo.AspectName = "KeyNo";
            this.olvcKeyNo.AutoCompleteEditor = false;
            this.olvcKeyNo.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcKeyNo.CellPadding = null;
            this.olvcKeyNo.DisplayIndex = 8;
            this.olvcKeyNo.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcKeyNo.IsEditable = false;
            this.olvcKeyNo.IsVisible = false;
            this.olvcKeyNo.Text = "委託序號";
            this.olvcKeyNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcOkSeq
            // 
            this.olvcOkSeq.AspectName = "OkSeq";
            this.olvcOkSeq.AutoCompleteEditor = false;
            this.olvcOkSeq.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcOkSeq.CellPadding = null;
            this.olvcOkSeq.DisplayIndex = 8;
            this.olvcOkSeq.Groupable = false;
            this.olvcOkSeq.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOkSeq.IsEditable = false;
            this.olvcOkSeq.IsVisible = false;
            this.olvcOkSeq.Text = "成交序號";
            this.olvcOkSeq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcSaleNo
            // 
            this.olvcSaleNo.AspectName = "SaleNo";
            this.olvcSaleNo.AutoCompleteEditor = false;
            this.olvcSaleNo.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcSaleNo.CellPadding = null;
            this.olvcSaleNo.DisplayIndex = 8;
            this.olvcSaleNo.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSaleNo.IsEditable = false;
            this.olvcSaleNo.IsVisible = false;
            this.olvcSaleNo.Text = "營業員編號";
            this.olvcSaleNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvcSubID
            // 
            this.olvcSubID.AspectName = "SubID";
            this.olvcSubID.AutoCompleteEditor = false;
            this.olvcSubID.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcSubID.CellPadding = null;
            this.olvcSubID.DisplayIndex = 21;
            this.olvcSubID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSubID.IsEditable = false;
            this.olvcSubID.IsVisible = false;
            this.olvcSubID.Text = "子帳帳號";
            this.olvcSubID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // frmMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1004, 288);
            this.Controls.Add(this.olvMatch);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Name = "frmMatch";
            this.ShowInTaskbar = false;
            this.Text = "成交";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMatch_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvMatch)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvMatch;
        private BrightIdeasSoftware.OLVColumn olvcKeyNo;
        private BrightIdeasSoftware.OLVColumn olvcMarketType;
        private BrightIdeasSoftware.OLVColumn olvcOrderType;
        private BrightIdeasSoftware.OLVColumn olvcOrderErr;
        private BrightIdeasSoftware.OLVColumn olvcBrokerID;
        private BrightIdeasSoftware.OLVColumn olvcCustNo;
        private BrightIdeasSoftware.OLVColumn olvcBuySell;
        private BrightIdeasSoftware.OLVColumn olvcPriceType;
        private BrightIdeasSoftware.OLVColumn olvcExchangeID;
        private BrightIdeasSoftware.OLVColumn olvcComID;
        private BrightIdeasSoftware.OLVColumn olvcStrike;
        private BrightIdeasSoftware.OLVColumn olvcOrdNo;
        private BrightIdeasSoftware.OLVColumn olvcPrice;
        private BrightIdeasSoftware.OLVColumn olvcQty;
        private BrightIdeasSoftware.OLVColumn olvcBeforeQty;
        private BrightIdeasSoftware.OLVColumn olvcAfterQty;
        private BrightIdeasSoftware.OLVColumn olvcDate;
        private BrightIdeasSoftware.OLVColumn olvcTime;
        private BrightIdeasSoftware.OLVColumn olvcOkSeq;
        private BrightIdeasSoftware.OLVColumn olvcSaleNo;
        private BrightIdeasSoftware.OLVColumn olvcMatchAmount;
        private BrightIdeasSoftware.OLVColumn olvcSubID;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvcSumQty;
    }
}