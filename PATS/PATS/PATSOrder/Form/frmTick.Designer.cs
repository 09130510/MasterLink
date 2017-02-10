using SourceGrid;

namespace PATSOrder
{
    partial class frmTick
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTick));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cboPID = new System.Windows.Forms.ComboBox();
            this.chkExtend = new System.Windows.Forms.CheckBox();
            this.nudFontSize = new System.Windows.Forms.NumericUpDown();
            this.chkSorting = new System.Windows.Forms.CheckBox();
            this.nudTickNumber = new System.Windows.Forms.NumericUpDown();
            this.btnProductFilter = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblBSummary = new System.Windows.Forms.Label();
            this.btnAlignment = new System.Windows.Forms.Button();
            this.lblSSummary = new System.Windows.Forms.Label();
            this.grid2 = new SourceGrid.Grid();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTickNumber)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 69F));
            this.tableLayoutPanel1.Controls.Add(this.cboPID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkExtend, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudFontSize, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.chkSorting, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.nudTickNumber, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnProductFilter, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cboAccount, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(286, 55);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cboPID
            // 
            this.cboPID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPID.DropDownWidth = 200;
            this.cboPID.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboPID.FormattingEnabled = true;
            this.cboPID.Location = new System.Drawing.Point(32, 3);
            this.cboPID.Name = "cboPID";
            this.cboPID.Size = new System.Drawing.Size(140, 21);
            this.cboPID.TabIndex = 12;
            this.cboPID.SelectedIndexChanged += new System.EventHandler(this.cboPID_SelectedIndexChanged);
            // 
            // chkExtend
            // 
            this.chkExtend.AutoSize = true;
            this.chkExtend.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkExtend.Location = new System.Drawing.Point(107, 28);
            this.chkExtend.Name = "chkExtend";
            this.chkExtend.Size = new System.Drawing.Size(65, 24);
            this.chkExtend.TabIndex = 3;
            this.chkExtend.Text = "Extend";
            this.chkExtend.UseVisualStyleBackColor = true;
            this.chkExtend.CheckedChanged += new System.EventHandler(this.chkExtend_CheckedChanged);
            // 
            // nudFontSize
            // 
            this.nudFontSize.DecimalPlaces = 2;
            this.nudFontSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudFontSize.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nudFontSize.Location = new System.Drawing.Point(223, 28);
            this.nudFontSize.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudFontSize.Name = "nudFontSize";
            this.nudFontSize.Size = new System.Drawing.Size(53, 20);
            this.nudFontSize.TabIndex = 5;
            this.nudFontSize.Tag = "SYS;FONTSIZE;Value;8";
            this.nudFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.nudFontSize.ValueChanged += new System.EventHandler(this.nudFontSize_ValueChanged);
            // 
            // chkSorting
            // 
            this.chkSorting.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSorting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSorting.Image = global::PATSOrder.Properties.Resources._1452261702_stock_sort_ascending;
            this.chkSorting.Location = new System.Drawing.Point(0, 25);
            this.chkSorting.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.chkSorting.Name = "chkSorting";
            this.chkSorting.Size = new System.Drawing.Size(29, 28);
            this.chkSorting.TabIndex = 7;
            this.chkSorting.Tag = "SYS;ASCENDING;Checked";
            this.chkSorting.UseVisualStyleBackColor = true;
            this.chkSorting.CheckedChanged += new System.EventHandler(this.chkSorting_CheckedChanged);
            // 
            // nudTickNumber
            // 
            this.nudTickNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudTickNumber.Location = new System.Drawing.Point(178, 28);
            this.nudTickNumber.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudTickNumber.Name = "nudTickNumber";
            this.nudTickNumber.Size = new System.Drawing.Size(39, 20);
            this.nudTickNumber.TabIndex = 9;
            this.nudTickNumber.Tag = "SYS;TICKNUMBER;Value;5";
            this.nudTickNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudTickNumber.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudTickNumber.ValueChanged += new System.EventHandler(this.nudTickNumber_ValueChanged);
            // 
            // btnProductFilter
            // 
            this.btnProductFilter.AutoSize = true;
            this.btnProductFilter.Font = new System.Drawing.Font("Verdana", 6F);
            this.btnProductFilter.ImageIndex = 0;
            this.btnProductFilter.ImageList = this.imageList1;
            this.btnProductFilter.Location = new System.Drawing.Point(0, 0);
            this.btnProductFilter.Margin = new System.Windows.Forms.Padding(0);
            this.btnProductFilter.Name = "btnProductFilter";
            this.btnProductFilter.Size = new System.Drawing.Size(29, 25);
            this.btnProductFilter.TabIndex = 10;
            this.btnProductFilter.UseVisualStyleBackColor = true;
            this.btnProductFilter.Click += new System.EventHandler(this.btnProductFilter_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1468478111_stock_standard-filter.ico");
            this.imageList1.Images.SetKeyName(1, "1468478122_stock_filter-data-by-criteria.ico");
            this.imageList1.Images.SetKeyName(2, "1468478134_filled_filter.png");
            this.imageList1.Images.SetKeyName(3, "1468492823_filter.ico");
            // 
            // cboAccount
            // 
            this.cboAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tableLayoutPanel1.SetColumnSpan(this.cboAccount, 2);
            this.cboAccount.DropDownWidth = 120;
            this.cboAccount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(178, 3);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(98, 21);
            this.cboAccount.TabIndex = 13;
            this.cboAccount.SelectedIndexChanged += new System.EventHandler(this.cboAccount_SelectedIndexChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblBSummary, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnAlignment, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblSSummary, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 355);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(286, 23);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // lblBSummary
            // 
            this.lblBSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBSummary.ForeColor = System.Drawing.Color.Crimson;
            this.lblBSummary.Location = new System.Drawing.Point(3, 0);
            this.lblBSummary.Name = "lblBSummary";
            this.lblBSummary.Size = new System.Drawing.Size(124, 23);
            this.lblBSummary.TabIndex = 0;
            this.lblBSummary.Text = "0";
            this.lblBSummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnAlignment
            // 
            this.btnAlignment.Image = global::PATSOrder.Properties.Resources._1451371729_stock_alignment_centered_vertically;
            this.btnAlignment.Location = new System.Drawing.Point(130, 0);
            this.btnAlignment.Margin = new System.Windows.Forms.Padding(0);
            this.btnAlignment.Name = "btnAlignment";
            this.btnAlignment.Size = new System.Drawing.Size(25, 23);
            this.btnAlignment.TabIndex = 1;
            this.btnAlignment.UseVisualStyleBackColor = true;
            this.btnAlignment.Click += new System.EventHandler(this.btnAlignment_Click);
            // 
            // lblSSummary
            // 
            this.lblSSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSSummary.ForeColor = System.Drawing.Color.Green;
            this.lblSSummary.Location = new System.Drawing.Point(158, 0);
            this.lblSSummary.Name = "lblSSummary";
            this.lblSSummary.Size = new System.Drawing.Size(125, 23);
            this.lblSSummary.TabIndex = 2;
            this.lblSSummary.Text = "0";
            this.lblSSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grid2
            // 
            this.grid2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grid2.AutoStretchColumnsToFitWidth = true;
            this.grid2.BackColor = System.Drawing.Color.Black;
            this.grid2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grid2.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut) 
            | SourceGrid.ClipboardMode.Paste) 
            | SourceGrid.ClipboardMode.Delete)));
            this.grid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid2.EnableSort = true;
            this.grid2.FixedRows = 3;
            this.grid2.Font = new System.Drawing.Font("Verdana", 7F);
            this.grid2.Location = new System.Drawing.Point(0, 55);
            this.grid2.Name = "grid2";
            this.grid2.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid2.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid2.Size = new System.Drawing.Size(286, 300);
            this.grid2.TabIndex = 1;
            this.grid2.TabStop = true;
            this.grid2.ToolTipText = "";
            // 
            // frmTick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(286, 378);
            this.Controls.Add(this.grid2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTick";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTick_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTickNumber)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkExtend;
        private System.Windows.Forms.NumericUpDown nudFontSize;
        private Grid grid2;
        private System.Windows.Forms.CheckBox chkSorting;
        private System.Windows.Forms.NumericUpDown nudTickNumber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblBSummary;
        private System.Windows.Forms.Button btnAlignment;
        private System.Windows.Forms.Label lblSSummary;
        private System.Windows.Forms.ComboBox cboPID;
        private System.Windows.Forms.Button btnProductFilter;
        private System.Windows.Forms.ComboBox cboAccount;
        private System.Windows.Forms.ImageList imageList1;
    }
}