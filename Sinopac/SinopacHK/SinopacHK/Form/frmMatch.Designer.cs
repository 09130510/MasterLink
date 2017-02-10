namespace SinopacHK
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
            this.components = new System.ComponentModel.Container();
            this.objectListView1 = new BrightIdeasSoftware.ObjectListView();
            this.olvcSymbol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcClOrdID2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcSide2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcAvgPrice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvcMatchQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsClear = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // objectListView1
            // 
            this.objectListView1.AllColumns.Add(this.olvcSymbol);
            this.objectListView1.AllColumns.Add(this.olvcClOrdID2);
            this.objectListView1.AllColumns.Add(this.olvcSide2);
            this.objectListView1.AllColumns.Add(this.olvcAvgPrice);
            this.objectListView1.AllColumns.Add(this.olvcMatchQty);
            this.objectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvcSymbol,
            this.olvcClOrdID2,
            this.olvcSide2,
            this.olvcAvgPrice,
            this.olvcMatchQty});
            this.objectListView1.ContextMenuStrip = this.contextMenuStrip1;
            this.objectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListView1.Location = new System.Drawing.Point(0, 0);
            this.objectListView1.Name = "objectListView1";
            this.objectListView1.Size = new System.Drawing.Size(341, 288);
            this.objectListView1.TabIndex = 0;
            this.objectListView1.UseCompatibleStateImageBehavior = false;
            this.objectListView1.View = System.Windows.Forms.View.Details;
            this.objectListView1.SelectedIndexChanged += new System.EventHandler(this.objectListView1_SelectedIndexChanged);
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
            // olvcClOrdID2
            // 
            this.olvcClOrdID2.AspectName = "LastVaildClOrdID";
            this.olvcClOrdID2.CellPadding = null;
            this.olvcClOrdID2.Groupable = false;
            this.olvcClOrdID2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcClOrdID2.Hideable = false;
            this.olvcClOrdID2.IsEditable = false;
            this.olvcClOrdID2.Text = "成交書號";
            this.olvcClOrdID2.Width = 80;
            // 
            // olvcSide2
            // 
            this.olvcSide2.AspectName = "Side";
            this.olvcSide2.CellPadding = null;
            this.olvcSide2.Groupable = false;
            this.olvcSide2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSide2.Hideable = false;
            this.olvcSide2.IsEditable = false;
            this.olvcSide2.Text = "買賣";
            this.olvcSide2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcSide2.Width = 50;
            // 
            // olvcAvgPrice
            // 
            this.olvcAvgPrice.AspectName = "AvgPrice";
            this.olvcAvgPrice.AspectToStringFormat = "{0:#,##0.000}";
            this.olvcAvgPrice.CellPadding = null;
            this.olvcAvgPrice.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcAvgPrice.Hideable = false;
            this.olvcAvgPrice.IsEditable = false;
            this.olvcAvgPrice.Text = "均價";
            this.olvcAvgPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcAvgPrice.Width = 55;
            // 
            // olvcMatchQty
            // 
            this.olvcMatchQty.AspectName = "MatchQty";
            this.olvcMatchQty.AspectToStringFormat = "{0:#,##0}";
            this.olvcMatchQty.CellPadding = null;
            this.olvcMatchQty.Groupable = false;
            this.olvcMatchQty.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvcMatchQty.Hideable = false;
            this.olvcMatchQty.IsEditable = false;
            this.olvcMatchQty.Text = "成交";
            this.olvcMatchQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvcMatchQty.Width = 50;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsClear});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 48);
            // 
            // tsClear
            // 
            this.tsClear.Name = "tsClear";
            this.tsClear.Size = new System.Drawing.Size(152, 22);
            this.tsClear.Text = "清除";
            this.tsClear.Click += new System.EventHandler(this.tsClear_Click);
            // 
            // frmMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 288);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.objectListView1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Name = "frmMatch";
            this.Text = "成交";
            ((System.ComponentModel.ISupportInitialize)(this.objectListView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListView1;
        private BrightIdeasSoftware.OLVColumn olvcClOrdID2;
        private BrightIdeasSoftware.OLVColumn olvcSide2;
        private BrightIdeasSoftware.OLVColumn olvcAvgPrice;
        private BrightIdeasSoftware.OLVColumn olvcMatchQty;
        private BrightIdeasSoftware.OLVColumn olvcSymbol;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsClear;
    }
}