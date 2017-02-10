namespace SinopacHK
{
    partial class frmAlive
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
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvcSymbol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcClOrdID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSide = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcOrderPrice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcLeavesQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvcSymbol);
            this.objectListView1.AllColumns.Add(this.olvcClOrdID);
            this.objectListView1.AllColumns.Add(this.olvcSide);
            this.objectListView1.AllColumns.Add(this.olvcOrderPrice);
            this.objectListView1.AllColumns.Add(this.olvcLeavesQty);
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcSymbol,
            this.olvcClOrdID,
            this.olvcSide,
            this.olvcOrderPrice,
            this.olvcLeavesQty});
            this.objectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListView1.Font = new System.Drawing.Font("Verdana", 8F);
            this.objectListView1.Location = new System.Drawing.Point(0, 0);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.Size = new System.Drawing.Size(341, 288);
            this.objectListView1.TabIndex = 0;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            this.objectListView1.SelectedIndexChanged += new System.EventHandler(this.objectListView1_SelectedIndexChanged);
            this.objectListView1.DoubleClick += new System.EventHandler(this.objectListView1_DoubleClick);
            // 
            // olvcSymbol
            // 
            this.olvcSymbol.AspectName = "ProductID";
            this.olvcSymbol.CellPadding = null;
            this.olvcSymbol.Groupable = false;
            this.olvcSymbol.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSymbol.Hideable = false;
            this.olvcSymbol.IsEditable = false;
            this.olvcSymbol.Text = "商品";
            // 
            // olvcClOrdID
            // 
            this.olvcClOrdID.AspectName = "LastVaildClOrdID";
            this.olvcClOrdID.CellPadding = null;
            this.olvcClOrdID.Groupable = false;
            this.olvcClOrdID.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcClOrdID.Hideable = false;
            this.olvcClOrdID.IsEditable = false;
            this.olvcClOrdID.Text = "在途書號";
            this.olvcClOrdID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcClOrdID.Width = 80;
            // 
            // olvcSide
            // 
            this.olvcSide.AspectName = "Side";
            this.olvcSide.CellPadding = null;
            this.olvcSide.Groupable = false;
            this.olvcSide.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSide.Hideable = false;
            this.olvcSide.IsEditable = false;
            this.olvcSide.Text = "買賣";
            this.olvcSide.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSide.Width = 50;
            // 
            // olvcOrderPrice
            // 
            this.olvcOrderPrice.AspectName = "OrderPrice";
            this.olvcOrderPrice.AspectToStringFormat = "{0:#,##0.000}";
            this.olvcOrderPrice.CellPadding = null;
            this.olvcOrderPrice.Groupable = false;
            this.olvcOrderPrice.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcOrderPrice.Hideable = false;
            this.olvcOrderPrice.IsEditable = false;
            this.olvcOrderPrice.Text = "價格";
            this.olvcOrderPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcOrderPrice.Width = 55;
            // 
            // olvcLeavesQty
            // 
            this.olvcLeavesQty.AspectName = "LeavesQty";
            this.olvcLeavesQty.AspectToStringFormat = "{0:#,##0}";
            this.olvcLeavesQty.CellPadding = null;
            this.olvcLeavesQty.Groupable = false;
            this.olvcLeavesQty.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcLeavesQty.Hideable = false;
            this.olvcLeavesQty.IsEditable = false;
            this.olvcLeavesQty.Text = "股數";
            this.olvcLeavesQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcLeavesQty.Width = 50;
            // 
            // frmAlive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 288);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.objectListView1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Name = "frmAlive";
            this.Text = "在途";
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.OLVColumn olvcClOrdID;
        private BrightIdeasSoftware.OLVColumn olvcSide;
        private BrightIdeasSoftware.OLVColumn olvcLeavesQty;
        private BrightIdeasSoftware.OLVColumn olvcOrderPrice;
        private BrightIdeasSoftware.OLVColumn olvcSymbol;

    }
}