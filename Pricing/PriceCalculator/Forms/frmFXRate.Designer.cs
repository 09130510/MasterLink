namespace PriceCalculator
{
    partial class frmFXRate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFXRate));
            this.olvFX = new BrightIdeasSoftware.ObjectListView();
            this.olvcFXName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcFXRate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcBLP = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcQuoteFromCMPN = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcQuoteFromTPFT = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.olvFX)).BeginInit();
            this.SuspendLayout();
            // 
            // olvFX
            // 
            this.olvFX.AllColumns.Add(this.olvcFXName);
            this.olvFX.AllColumns.Add(this.olvcFXRate);
            this.olvFX.AllColumns.Add(this.olvcBLP);
            this.olvFX.AllColumns.Add(this.olvcQuoteFromCMPN);
            this.olvFX.AllColumns.Add(this.olvcQuoteFromTPFT);
            this.olvFX.AllowColumnReorder = true;
            this.olvFX.AlternateRowBackColor = System.Drawing.Color.LemonChiffon;
            this.olvFX.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick;
            this.olvFX.CellEditTabChangesRows = true;
            this.olvFX.CellEditUseWholeCell = false;
            this.olvFX.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcFXName,
            this.olvcFXRate,
            this.olvcQuoteFromCMPN,
            this.olvcQuoteFromTPFT});
            this.olvFX.Cursor = System.Windows.Forms.Cursors.Default;
            this.olvFX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olvFX.HideSelection = false;
            this.olvFX.IncludeColumnHeadersInCopy = true;
            this.olvFX.Location = new System.Drawing.Point(0, 0);
            this.olvFX.Name = "olvFX";
            this.olvFX.ShowCommandMenuOnRightClick = true;
            this.olvFX.ShowGroups = false;
            this.olvFX.ShowHeaderInAllViews = false;
            this.olvFX.Size = new System.Drawing.Size(241, 128);
            this.olvFX.SortGroupItemsByPrimaryColumn = false;
            this.olvFX.TabIndex = 0;
            this.olvFX.UseAlternatingBackColors = true;
            this.olvFX.UseCompatibleStateImageBehavior = false;
            this.olvFX.UseFilterIndicator = true;
            this.olvFX.UseFiltering = true;
            this.olvFX.UseHotItem = true;
            this.olvFX.UseNotifyPropertyChanged = true;
            this.olvFX.UseTranslucentHotItem = true;
            this.olvFX.UseTranslucentSelection = true;
            this.olvFX.View = System.Windows.Forms.View.Details;
            this.olvFX.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.olvFX_CellEditStarting);
            // 
            // olvcFXName
            // 
            this.olvcFXName.AspectName = "Quoted";
            this.olvcFXName.AutoCompleteEditor = false;
            this.olvcFXName.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcFXName.Groupable = false;
            this.olvcFXName.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.olvcFXName.Hideable = false;
            this.olvcFXName.IsEditable = false;
            this.olvcFXName.Text = "Quoted";
            this.olvcFXName.Width = 70;
            // 
            // olvcFXRate
            // 
            this.olvcFXRate.AspectName = "Rate";
            this.olvcFXRate.AutoCompleteEditor = false;
            this.olvcFXRate.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None;
            this.olvcFXRate.CellEditUseWholeCell = true;
            this.olvcFXRate.FillsFreeSpace = true;
            this.olvcFXRate.Groupable = false;
            this.olvcFXRate.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcFXRate.Hideable = false;
            this.olvcFXRate.Text = "Rate";
            this.olvcFXRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcFXRate.Width = 80;
            // 
            // olvcBLP
            // 
            this.olvcBLP.AspectName = "";
            this.olvcBLP.CheckBoxes = true;
            this.olvcBLP.DisplayIndex = 2;
            this.olvcBLP.IsVisible = false;
            this.olvcBLP.Text = "";
            this.olvcBLP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcBLP.Width = 20;
            // 
            // olvcQuoteFromCMPN
            // 
            this.olvcQuoteFromCMPN.AspectName = "QuoteFromCMPN";
            this.olvcQuoteFromCMPN.CheckBoxes = true;
            this.olvcQuoteFromCMPN.Text = "C";
            this.olvcQuoteFromCMPN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcQuoteFromCMPN.Width = 22;
            // 
            // olvcQuoteFromTPFT
            // 
            this.olvcQuoteFromTPFT.AspectName = "QuoteFromTPFT";
            this.olvcQuoteFromTPFT.CheckBoxes = true;
            this.olvcQuoteFromTPFT.Text = "T";
            this.olvcQuoteFromTPFT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcQuoteFromTPFT.Width = 20;
            // 
            // frmFXRate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 128);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.olvFX);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFXRate";
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.olvFX)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView olvFX;
        private BrightIdeasSoftware.OLVColumn olvcFXName;
        private BrightIdeasSoftware.OLVColumn olvcFXRate;
        private BrightIdeasSoftware.OLVColumn olvcBLP;
        private BrightIdeasSoftware.OLVColumn olvcQuoteFromCMPN;
        private BrightIdeasSoftware.OLVColumn olvcQuoteFromTPFT;
    }
}