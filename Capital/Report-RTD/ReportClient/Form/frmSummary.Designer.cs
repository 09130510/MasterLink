namespace Capital.Report
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
            this.olvSummary = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcCustNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBrokerID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcComID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcALots = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBLots = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcAAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.olvSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // olvSummary
            // 
            this.olvSummary.AllColumns.Add(this.olvColumn1);
            this.olvSummary.AllColumns.Add(this.olvcCustNo);
            this.olvSummary.AllColumns.Add(this.olvcBrokerID);
            this.olvSummary.AllColumns.Add(this.olvcComID);
            this.olvSummary.AllColumns.Add(this.olvcALots);
            this.olvSummary.AllColumns.Add(this.olvcBLots);
            this.olvSummary.AllColumns.Add(this.olvcAAmount);
            this.olvSummary.AllColumns.Add(this.olvcBAmount);
            this.olvSummary.AllowColumnReorder = true;
            this.olvSummary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvcCustNo,
            this.olvcBrokerID,
            this.olvcComID,
            this.olvcALots,
            this.olvcBLots,
            this.olvcAAmount,
            this.olvcBAmount});
            this.olvSummary.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvSummary.EmptyListMsg = "無彙總資料";
            this.olvSummary.EmptyListMsgFont = new System.Drawing.Font("Verdana", 8F);
            this.olvSummary.Font = new System.Drawing.Font("Verdana", 8F);
            this.olvSummary.FullRowSelect = true;
            this.olvSummary.HeaderUsesThemes = false;
            this.olvSummary.Location = new System.Drawing.Point(0, 0);
            this.olvSummary.Name = "olvSummary";
            this.olvSummary.OwnerDraw = true;
            this.olvSummary.ShowCommandMenuOnRightClick = true;
            this.olvSummary.ShowGroups = false;
            this.olvSummary.ShowItemCountOnGroups = true;
            this.olvSummary.Size = new System.Drawing.Size(653, 291);
            this.olvSummary.TabIndex = 1;
            this.olvSummary.UseCellFormatEvents = true;
            this.olvSummary.UseCompatibleStateImageBehavior = false;
            this.olvSummary.UseCustomSelectionColors = true;
            this.olvSummary.UseFilterIndicator = true;
            this.olvSummary.UseFiltering = true;
            this.olvSummary.UseHotItem = true;
            this.olvSummary.UseNotifyPropertyChanged = true;
            this.olvSummary.UseTranslucentHotItem = true;
            this.olvSummary.UseTranslucentSelection = true;
            this.olvSummary.View = System.Windows.Forms.View.Details;
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
            // olvcCustNo
            // 
            this.olvcCustNo.AspectName = "CustNo";
            this.olvcCustNo.AutoCompleteEditor = false;
            this.olvcCustNo.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcCustNo.CellPadding = null;
            this.olvcCustNo.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcCustNo.Hideable = false;
            this.olvcCustNo.IsEditable = false;
            this.olvcCustNo.Text = "帳號";
            this.olvcCustNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcCustNo.Width = 65;
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
            // olvcComID
            // 
            this.olvcComID.AspectName = "ComID";
            this.olvcComID.AutoCompleteEditor = false;
            this.olvcComID.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcComID.CellPadding = null;
            this.olvcComID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcComID.Hideable = false;
            this.olvcComID.IsEditable = false;
            this.olvcComID.Text = "商品";
            this.olvcComID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcComID.Width = 75;
            // 
            // olvcALots
            // 
            this.olvcALots.AspectName = "ALots";
            this.olvcALots.AutoCompleteEditor = false;
            this.olvcALots.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcALots.CellPadding = null;
            this.olvcALots.Groupable = false;
            this.olvcALots.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcALots.IsEditable = false;
            this.olvcALots.Text = "賣量";
            this.olvcALots.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcALots.Width = 50;
            // 
            // olvcBLots
            // 
            this.olvcBLots.AspectName = "BLots";
            this.olvcBLots.AutoCompleteEditor = false;
            this.olvcBLots.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcBLots.CellPadding = null;
            this.olvcBLots.Groupable = false;
            this.olvcBLots.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBLots.IsEditable = false;
            this.olvcBLots.Text = "買量";
            this.olvcBLots.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcBLots.Width = 50;
            // 
            // olvcAAmount
            // 
            this.olvcAAmount.AspectName = "AAmount";
            this.olvcAAmount.AspectToStringFormat = "{0:#,##0.00}";
            this.olvcAAmount.AutoCompleteEditor = false;
            this.olvcAAmount.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcAAmount.CellPadding = null;
            this.olvcAAmount.Groupable = false;
            this.olvcAAmount.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcAAmount.IsEditable = false;
            this.olvcAAmount.Text = "賣金額";
            this.olvcAAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcAAmount.Width = 65;
            // 
            // olvcBAmount
            // 
            this.olvcBAmount.AspectName = "BAmount";
            this.olvcBAmount.AspectToStringFormat = "{0:#,##0.00}";
            this.olvcBAmount.AutoCompleteEditor = false;
            this.olvcBAmount.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcBAmount.CellPadding = null;
            this.olvcBAmount.Groupable = false;
            this.olvcBAmount.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBAmount.IsEditable = false;
            this.olvcBAmount.Text = "買金額";
            this.olvcBAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcBAmount.Width = 65;
            // 
            // frmSummary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(653, 291);
            this.Controls.Add(this.olvSummary);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.Document)));
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Name = "frmSummary";
            this.ShowInTaskbar = false;
            this.Text = "彙總";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSummary_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.olvSummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvSummary;
        private BrightIdeasSoftware.OLVColumn olvcBrokerID;
        private BrightIdeasSoftware.OLVColumn olvcCustNo;
        private BrightIdeasSoftware.OLVColumn olvcComID;
        private BrightIdeasSoftware.OLVColumn olvcALots;
        private BrightIdeasSoftware.OLVColumn olvcBLots;
        private BrightIdeasSoftware.OLVColumn olvcAAmount;
        private BrightIdeasSoftware.OLVColumn olvcBAmount;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
    }
}