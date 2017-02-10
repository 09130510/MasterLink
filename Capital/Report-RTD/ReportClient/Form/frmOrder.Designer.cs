using System;
using Capital.Report.Class;
namespace Capital.Report
{
    partial class frmOrder
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
            this.components = new System.ComponentModel.Container();
            this.olvOrder = new BrightIdeasSoftware.ObjectListView();
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
            this.olvcBeforeQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcAfterQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSumQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcTime = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcKeyNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOkSeq = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSaleNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSubID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cmChangeOrder = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmSell1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSell2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSell3 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSell4 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSell5 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSell6 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSell7 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSell8 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSell9 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmSell10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmPrice = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmBuy1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBuy2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBuy3 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBuy4 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBuy5 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBuy6 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBuy7 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBuy8 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBuy9 = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBuy10 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblAction = new System.Windows.Forms.Label();
            this.gupAction = new System.Windows.Forms.GroupBox();
            this.radRenderer = new System.Windows.Forms.RadioButton();
            this.radFilter = new System.Windows.Forms.RadioButton();
            this.radNone = new System.Windows.Forms.RadioButton();
            this.chkValid = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.olvOrder)).BeginInit();
            this.cmChangeOrder.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gupAction.SuspendLayout();
            this.SuspendLayout();
            // 
            // olvOrder
            // 
            this.olvOrder.AllColumns.Add(this.olvColumn1);
            this.olvOrder.AllColumns.Add(this.olvcOrdNo);
            this.olvOrder.AllColumns.Add(this.olvcCustNo);
            this.olvOrder.AllColumns.Add(this.olvcMarketType);
            this.olvOrder.AllColumns.Add(this.olvcOrderErr);
            this.olvOrder.AllColumns.Add(this.olvcExchangeID);
            this.olvOrder.AllColumns.Add(this.olvcBrokerID);
            this.olvOrder.AllColumns.Add(this.olvcComID);
            this.olvOrder.AllColumns.Add(this.olvcStrike);
            this.olvOrder.AllColumns.Add(this.olvcOrderType);
            this.olvOrder.AllColumns.Add(this.olvcBuySell);
            this.olvOrder.AllColumns.Add(this.olvcPriceType);
            this.olvOrder.AllColumns.Add(this.olvcPrice);
            this.olvOrder.AllColumns.Add(this.olvcQty);
            this.olvOrder.AllColumns.Add(this.olvcBeforeQty);
            this.olvOrder.AllColumns.Add(this.olvcAfterQty);
            this.olvOrder.AllColumns.Add(this.olvcSumQty);
            this.olvOrder.AllColumns.Add(this.olvcDate);
            this.olvOrder.AllColumns.Add(this.olvcTime);
            this.olvOrder.AllColumns.Add(this.olvcKeyNo);
            this.olvOrder.AllColumns.Add(this.olvcOkSeq);
            this.olvOrder.AllColumns.Add(this.olvcSaleNo);
            this.olvOrder.AllColumns.Add(this.olvcSubID);
            this.olvOrder.AllowColumnReorder = true;
            this.olvOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvcOrdNo,
            this.olvcCustNo,
            this.olvcComID,
            this.olvcOrderType,
            this.olvcBuySell,
            this.olvcPrice,
            this.olvcQty,
            this.olvcSumQty,
            this.olvcTime});
            this.olvOrder.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvOrder.EmptyListMsg = "無委託資料";
            this.olvOrder.EmptyListMsgFont = new System.Drawing.Font("Verdana", 8F);
            this.olvOrder.Font = new System.Drawing.Font("Verdana", 8F);
            this.olvOrder.FullRowSelect = true;
            this.olvOrder.HeaderUsesThemes = false;
            this.olvOrder.Location = new System.Drawing.Point(0, 29);
            this.olvOrder.Name = "olvOrder";
            this.olvOrder.OwnerDraw = true;
            this.olvOrder.OwnerDrawnHeader = true;
            this.olvOrder.ShowCommandMenuOnRightClick = true;
            this.olvOrder.ShowGroups = false;
            this.olvOrder.ShowItemCountOnGroups = true;
            this.olvOrder.Size = new System.Drawing.Size(567, 259);
            this.olvOrder.TabIndex = 0;
            this.olvOrder.UseCellFormatEvents = true;
            this.olvOrder.UseCompatibleStateImageBehavior = false;
            this.olvOrder.UseCustomSelectionColors = true;
            this.olvOrder.UseFilterIndicator = true;
            this.olvOrder.UseFiltering = true;
            this.olvOrder.UseHotItem = true;
            this.olvOrder.UseNotifyPropertyChanged = true;
            this.olvOrder.UseTranslucentHotItem = true;
            this.olvOrder.UseTranslucentSelection = true;
            this.olvOrder.View = System.Windows.Forms.View.Details;
            this.olvOrder.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.olvOrder_CellRightClick);
            this.olvOrder.SelectedIndexChanged += new System.EventHandler(this.olvOrder_SelectedIndexChanged);
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
            this.olvcOrderType.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOrderType.IsEditable = false;
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
            this.olvcSumQty.Groupable = false;
            this.olvcSumQty.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSumQty.IsEditable = false;
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
            this.olvcSubID.DisplayIndex = 20;
            this.olvcSubID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSubID.IsEditable = false;
            this.olvcSubID.IsVisible = false;
            this.olvcSubID.Text = "子帳帳號";
            this.olvcSubID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmChangeOrder
            // 
            this.cmChangeOrder.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmChangeOrder.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmSell1,
            this.cmSell2,
            this.cmSell3,
            this.cmSell4,
            this.cmSell5,
            this.cmSell6,
            this.cmSell7,
            this.cmSell8,
            this.cmSell9,
            this.cmSell10,
            this.toolStripSeparator2,
            this.cmPrice,
            this.toolStripSeparator1,
            this.cmBuy1,
            this.cmBuy2,
            this.cmBuy3,
            this.cmBuy4,
            this.cmBuy5,
            this.cmBuy6,
            this.cmBuy7,
            this.cmBuy8,
            this.cmBuy9,
            this.cmBuy10});
            this.cmChangeOrder.Name = "cmChangeOrder";
            this.cmChangeOrder.Size = new System.Drawing.Size(153, 500);
            // 
            // cmSell1
            // 
            this.cmSell1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell1.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell1.Image = global::Capital.Report.Properties.Resources.stock_down_with_subpoints;
            this.cmSell1.Name = "cmSell1";
            this.cmSell1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D1)));
            this.cmSell1.ShowShortcutKeys = false;
            this.cmSell1.Size = new System.Drawing.Size(152, 22);
            this.cmSell1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell1.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmSell2
            // 
            this.cmSell2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell2.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell2.Name = "cmSell2";
            this.cmSell2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D2)));
            this.cmSell2.ShowShortcutKeys = false;
            this.cmSell2.Size = new System.Drawing.Size(152, 22);
            this.cmSell2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell2.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmSell3
            // 
            this.cmSell3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell3.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell3.Name = "cmSell3";
            this.cmSell3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D3)));
            this.cmSell3.ShowShortcutKeys = false;
            this.cmSell3.Size = new System.Drawing.Size(152, 22);
            this.cmSell3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell3.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmSell4
            // 
            this.cmSell4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell4.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell4.Name = "cmSell4";
            this.cmSell4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D4)));
            this.cmSell4.ShowShortcutKeys = false;
            this.cmSell4.Size = new System.Drawing.Size(152, 22);
            this.cmSell4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell4.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmSell5
            // 
            this.cmSell5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell5.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell5.Name = "cmSell5";
            this.cmSell5.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D5)));
            this.cmSell5.ShowShortcutKeys = false;
            this.cmSell5.Size = new System.Drawing.Size(152, 22);
            this.cmSell5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell5.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmSell6
            // 
            this.cmSell6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell6.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell6.Name = "cmSell6";
            this.cmSell6.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D5)));
            this.cmSell6.ShowShortcutKeys = false;
            this.cmSell6.Size = new System.Drawing.Size(152, 22);
            this.cmSell6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell6.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmSell7
            // 
            this.cmSell7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell7.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell7.Name = "cmSell7";
            this.cmSell7.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D5)));
            this.cmSell7.ShowShortcutKeys = false;
            this.cmSell7.Size = new System.Drawing.Size(152, 22);
            this.cmSell7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell7.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmSell8
            // 
            this.cmSell8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell8.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell8.Name = "cmSell8";
            this.cmSell8.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D5)));
            this.cmSell8.ShowShortcutKeys = false;
            this.cmSell8.Size = new System.Drawing.Size(152, 22);
            this.cmSell8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell8.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmSell9
            // 
            this.cmSell9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell9.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell9.Name = "cmSell9";
            this.cmSell9.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D5)));
            this.cmSell9.ShowShortcutKeys = false;
            this.cmSell9.Size = new System.Drawing.Size(152, 22);
            this.cmSell9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell9.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmSell10
            // 
            this.cmSell10.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmSell10.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmSell10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmSell10.Name = "cmSell10";
            this.cmSell10.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D5)));
            this.cmSell10.ShowShortcutKeys = false;
            this.cmSell10.Size = new System.Drawing.Size(152, 22);
            this.cmSell10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmSell10.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // cmPrice
            // 
            this.cmPrice.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmPrice.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cmPrice.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmPrice.Image = global::Capital.Report.Properties.Resources.sell;
            this.cmPrice.Name = "cmPrice";
            this.cmPrice.Size = new System.Drawing.Size(152, 22);
            this.cmPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // cmBuy1
            // 
            this.cmBuy1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy1.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy1.Name = "cmBuy1";
            this.cmBuy1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.cmBuy1.ShowShortcutKeys = false;
            this.cmBuy1.Size = new System.Drawing.Size(152, 22);
            this.cmBuy1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy1.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmBuy2
            // 
            this.cmBuy2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy2.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy2.Name = "cmBuy2";
            this.cmBuy2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.cmBuy2.ShowShortcutKeys = false;
            this.cmBuy2.Size = new System.Drawing.Size(152, 22);
            this.cmBuy2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy2.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmBuy3
            // 
            this.cmBuy3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy3.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy3.Name = "cmBuy3";
            this.cmBuy3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.cmBuy3.ShowShortcutKeys = false;
            this.cmBuy3.Size = new System.Drawing.Size(152, 22);
            this.cmBuy3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy3.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmBuy4
            // 
            this.cmBuy4.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy4.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy4.Name = "cmBuy4";
            this.cmBuy4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.cmBuy4.ShowShortcutKeys = false;
            this.cmBuy4.Size = new System.Drawing.Size(152, 22);
            this.cmBuy4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy4.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmBuy5
            // 
            this.cmBuy5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy5.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy5.Name = "cmBuy5";
            this.cmBuy5.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.cmBuy5.ShowShortcutKeys = false;
            this.cmBuy5.Size = new System.Drawing.Size(152, 22);
            this.cmBuy5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy5.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmBuy6
            // 
            this.cmBuy6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy6.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy6.Name = "cmBuy6";
            this.cmBuy6.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.cmBuy6.ShowShortcutKeys = false;
            this.cmBuy6.Size = new System.Drawing.Size(152, 22);
            this.cmBuy6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy6.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmBuy7
            // 
            this.cmBuy7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy7.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy7.Name = "cmBuy7";
            this.cmBuy7.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.cmBuy7.ShowShortcutKeys = false;
            this.cmBuy7.Size = new System.Drawing.Size(152, 22);
            this.cmBuy7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy7.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmBuy8
            // 
            this.cmBuy8.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy8.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy8.Name = "cmBuy8";
            this.cmBuy8.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.cmBuy8.ShowShortcutKeys = false;
            this.cmBuy8.Size = new System.Drawing.Size(152, 22);
            this.cmBuy8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy8.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmBuy9
            // 
            this.cmBuy9.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy9.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy9.Name = "cmBuy9";
            this.cmBuy9.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.cmBuy9.ShowShortcutKeys = false;
            this.cmBuy9.Size = new System.Drawing.Size(152, 22);
            this.cmBuy9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy9.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // cmBuy10
            // 
            this.cmBuy10.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmBuy10.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmBuy10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmBuy10.Image = global::Capital.Report.Properties.Resources.stock_up_with_subpoints;
            this.cmBuy10.Name = "cmBuy10";
            this.cmBuy10.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
            this.cmBuy10.ShowShortcutKeys = false;
            this.cmBuy10.Size = new System.Drawing.Size(152, 22);
            this.cmBuy10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmBuy10.Click += new System.EventHandler(this.cmBuy_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblAction);
            this.panel1.Controls.Add(this.gupAction);
            this.panel1.Controls.Add(this.chkValid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(567, 29);
            this.panel1.TabIndex = 1;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(175, 8);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(55, 13);
            this.lblAction.TabIndex = 5;
            this.lblAction.Text = "選取動作";
            // 
            // gupAction
            // 
            this.gupAction.Controls.Add(this.radRenderer);
            this.gupAction.Controls.Add(this.radFilter);
            this.gupAction.Controls.Add(this.radNone);
            this.gupAction.Location = new System.Drawing.Point(230, -3);
            this.gupAction.Name = "gupAction";
            this.gupAction.Size = new System.Drawing.Size(166, 29);
            this.gupAction.TabIndex = 4;
            this.gupAction.TabStop = false;
            // 
            // radRenderer
            // 
            this.radRenderer.AutoSize = true;
            this.radRenderer.Location = new System.Drawing.Point(113, 10);
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
            this.radFilter.Location = new System.Drawing.Point(56, 9);
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
            this.radNone.Location = new System.Drawing.Point(11, 9);
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
            this.chkValid.Location = new System.Drawing.Point(12, 7);
            this.chkValid.Name = "chkValid";
            this.chkValid.Size = new System.Drawing.Size(98, 17);
            this.chkValid.TabIndex = 2;
            this.chkValid.Tag = "FORM;ONLYVALID;Checked";
            this.chkValid.Text = "只顯示有效單";
            this.chkValid.UseVisualStyleBackColor = true;
            this.chkValid.CheckedChanged += new System.EventHandler(this.chkValid_CheckedChanged);
            // 
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 288);
            this.Controls.Add(this.olvOrder);
            this.Controls.Add(this.panel1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Name = "frmOrder";
            this.ShowInTaskbar = false;
            this.Text = "委託";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmOrder_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvOrder)).EndInit();
            this.cmChangeOrder.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.gupAction.ResumeLayout(false);
            this.gupAction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvOrder;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkValid;
        private System.Windows.Forms.GroupBox gupAction;
        private System.Windows.Forms.RadioButton radRenderer;
        private System.Windows.Forms.RadioButton radFilter;
        private System.Windows.Forms.RadioButton radNone;
        private System.Windows.Forms.Label lblAction;
        private BrightIdeasSoftware.OLVColumn olvcSubID;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvcSumQty;
        private System.Windows.Forms.ContextMenuStrip cmChangeOrder;
        private System.Windows.Forms.ToolStripMenuItem cmBuy1;
        private System.Windows.Forms.ToolStripMenuItem cmBuy2;
        private System.Windows.Forms.ToolStripMenuItem cmBuy3;
        private System.Windows.Forms.ToolStripMenuItem cmBuy4;
        private System.Windows.Forms.ToolStripMenuItem cmBuy10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmSell1;
        private System.Windows.Forms.ToolStripMenuItem cmSell2;
        private System.Windows.Forms.ToolStripMenuItem cmSell3;
        private System.Windows.Forms.ToolStripMenuItem cmSell4;
        private System.Windows.Forms.ToolStripMenuItem cmSell5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cmPrice;
        private System.Windows.Forms.ToolStripMenuItem cmSell6;
        private System.Windows.Forms.ToolStripMenuItem cmSell7;
        private System.Windows.Forms.ToolStripMenuItem cmSell8;
        private System.Windows.Forms.ToolStripMenuItem cmSell9;
        private System.Windows.Forms.ToolStripMenuItem cmSell10;
        private System.Windows.Forms.ToolStripMenuItem cmBuy5;
        private System.Windows.Forms.ToolStripMenuItem cmBuy6;
        private System.Windows.Forms.ToolStripMenuItem cmBuy7;
        private System.Windows.Forms.ToolStripMenuItem cmBuy8;
        private System.Windows.Forms.ToolStripMenuItem cmBuy9;
    }
}