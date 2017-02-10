namespace Capital.Report
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
            this.grid1 = new SourceGrid.Grid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboAccount = new System.Windows.Forms.ComboBox();
            this.chkisAscending = new System.Windows.Forms.CheckBox();
            this.chkExtend = new System.Windows.Forms.CheckBox();
            this.cboPID = new System.Windows.Forms.ComboBox();
            this.nudFont = new System.Windows.Forms.NumericUpDown();
            this.nudTickCount = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAlignment = new System.Windows.Forms.Button();
            this.lblSSummary = new System.Windows.Forms.Label();
            this.lblBSummary = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTickCount)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grid1.AutoStretchColumnsToFitWidth = true;
            this.grid1.BackColor = System.Drawing.Color.Black;
            this.grid1.DefaultHeight = 18;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.EnableSort = true;
            this.grid1.FixedRows = 3;
            this.grid1.Font = new System.Drawing.Font("Verdana", 7F);
            this.grid1.Location = new System.Drawing.Point(0, 49);
            this.grid1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.grid1.Name = "grid1";
            this.grid1.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid1.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid1.Size = new System.Drawing.Size(269, 285);
            this.grid1.TabIndex = 1;
            this.grid1.TabStop = true;
            this.grid1.ToolTipText = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cboAccount);
            this.panel1.Controls.Add(this.chkisAscending);
            this.panel1.Controls.Add(this.chkExtend);
            this.panel1.Controls.Add(this.cboPID);
            this.panel1.Controls.Add(this.nudFont);
            this.panel1.Controls.Add(this.nudTickCount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(269, 49);
            this.panel1.TabIndex = 7;
            // 
            // cboAccount
            // 
            this.cboAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAccount.Font = new System.Drawing.Font("Verdana", 7.75F, System.Drawing.FontStyle.Bold);
            this.cboAccount.FormattingEnabled = true;
            this.cboAccount.Location = new System.Drawing.Point(114, 2);
            this.cboAccount.Name = "cboAccount";
            this.cboAccount.Size = new System.Drawing.Size(152, 20);
            this.cboAccount.TabIndex = 7;
            this.cboAccount.SelectedIndexChanged += new System.EventHandler(this.cboAccount_SelectedIndexChanged);
            // 
            // chkisAscending
            // 
            this.chkisAscending.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkisAscending.Checked = true;
            this.chkisAscending.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkisAscending.Image = global::Capital.Report.Properties.Resources._1452262106_stock_sort_ascending_16;
            this.chkisAscending.Location = new System.Drawing.Point(4, 24);
            this.chkisAscending.Name = "chkisAscending";
            this.chkisAscending.Size = new System.Drawing.Size(24, 24);
            this.chkisAscending.TabIndex = 6;
            this.chkisAscending.Tag = "ORDERSETTING;INCREASE;Checked";
            this.chkisAscending.UseVisualStyleBackColor = true;
            this.chkisAscending.CheckedChanged += new System.EventHandler(this.chkisAscending_CheckedChanged);
            // 
            // chkExtend
            // 
            this.chkExtend.AutoSize = true;
            this.chkExtend.Location = new System.Drawing.Point(77, 28);
            this.chkExtend.Name = "chkExtend";
            this.chkExtend.Size = new System.Drawing.Size(65, 17);
            this.chkExtend.TabIndex = 5;
            this.chkExtend.Text = "Extend";
            this.chkExtend.UseVisualStyleBackColor = true;
            this.chkExtend.CheckedChanged += new System.EventHandler(this.chkExtend_CheckedChanged);
            // 
            // cboPID
            // 
            this.cboPID.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboPID.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPID.FormattingEnabled = true;
            this.cboPID.Location = new System.Drawing.Point(4, 2);
            this.cboPID.Name = "cboPID";
            this.cboPID.Size = new System.Drawing.Size(105, 21);
            this.cboPID.TabIndex = 4;
            this.cboPID.DropDown += new System.EventHandler(this.cboPID_DropDown);
            this.cboPID.SelectedIndexChanged += new System.EventHandler(this.cboPID_SelectedIndexChanged);
            this.cboPID.Validated += new System.EventHandler(this.cboPID_SelectedIndexChanged);
            // 
            // nudFont
            // 
            this.nudFont.Font = new System.Drawing.Font("Verdana", 7F);
            this.nudFont.Location = new System.Drawing.Point(184, 27);
            this.nudFont.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudFont.Maximum = new decimal(new int[] {
            22,
            0,
            0,
            0});
            this.nudFont.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudFont.Name = "nudFont";
            this.nudFont.Size = new System.Drawing.Size(42, 19);
            this.nudFont.TabIndex = 1;
            this.nudFont.Tag = "ORDERSETTING;FONTSIZE;Value";
            this.nudFont.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudFont.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudFont.ValueChanged += new System.EventHandler(this.nudFont_ValueChanged);
            // 
            // nudTickCount
            // 
            this.nudTickCount.Font = new System.Drawing.Font("Verdana", 7F);
            this.nudTickCount.Location = new System.Drawing.Point(142, 27);
            this.nudTickCount.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.nudTickCount.Name = "nudTickCount";
            this.nudTickCount.Size = new System.Drawing.Size(42, 19);
            this.nudTickCount.TabIndex = 0;
            this.nudTickCount.Tag = "ORDERSETTING;TICKCOUNT;Value";
            this.nudTickCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudTickCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTickCount.ValueChanged += new System.EventHandler(this.nudTickCount_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAlignment);
            this.panel2.Controls.Add(this.lblSSummary);
            this.panel2.Controls.Add(this.lblBSummary);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 334);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 19);
            this.panel2.TabIndex = 8;
            // 
            // btnAlignment
            // 
            this.btnAlignment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAlignment.Image = global::Capital.Report.Properties.Resources._1451371729_stock_alignment_centered_vertically;
            this.btnAlignment.Location = new System.Drawing.Point(118, 0);
            this.btnAlignment.Name = "btnAlignment";
            this.btnAlignment.Size = new System.Drawing.Size(20, 20);
            this.btnAlignment.TabIndex = 6;
            this.btnAlignment.UseVisualStyleBackColor = true;
            this.btnAlignment.Click += new System.EventHandler(this.btnAlignment_Click);
            // 
            // lblSSummary
            // 
            this.lblSSummary.AutoSize = true;
            this.lblSSummary.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSSummary.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblSSummary.ForeColor = System.Drawing.Color.Green;
            this.lblSSummary.Location = new System.Drawing.Point(257, 0);
            this.lblSSummary.Name = "lblSSummary";
            this.lblSSummary.Size = new System.Drawing.Size(12, 12);
            this.lblSSummary.TabIndex = 5;
            this.lblSSummary.Text = "0";
            this.lblSSummary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBSummary
            // 
            this.lblBSummary.AutoSize = true;
            this.lblBSummary.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBSummary.Font = new System.Drawing.Font("Verdana", 7F);
            this.lblBSummary.ForeColor = System.Drawing.Color.Crimson;
            this.lblBSummary.Location = new System.Drawing.Point(0, 0);
            this.lblBSummary.Name = "lblBSummary";
            this.lblBSummary.Size = new System.Drawing.Size(12, 12);
            this.lblBSummary.TabIndex = 4;
            this.lblBSummary.Text = "0";
            this.lblBSummary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmTick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(269, 353);
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTick";
            this.Text = "frmTick";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTick_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTickCount)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SourceGrid.Grid grid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboPID;
        private System.Windows.Forms.NumericUpDown nudFont;
        private System.Windows.Forms.NumericUpDown nudTickCount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblSSummary;
        private System.Windows.Forms.Label lblBSummary;
        private System.Windows.Forms.CheckBox chkExtend;
        private System.Windows.Forms.Button btnAlignment;
        private System.Windows.Forms.CheckBox chkisAscending;
        private System.Windows.Forms.ComboBox cboAccount;


    }
}