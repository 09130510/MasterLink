namespace SinopacHK
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
            this.grid1 = new SourceGrid.Grid();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSSummary = new System.Windows.Forms.Label();
            this.lblBSummary = new System.Windows.Forms.Label();
            this.nudFont = new System.Windows.Forms.NumericUpDown();
            this.nudTickCount = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTickCount)).BeginInit();
            this.SuspendLayout();
            // 
            // grid1
            // 
            this.grid1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grid1.AutoStretchColumnsToFitWidth = true;
            this.grid1.AutoStretchRowsToFitHeight = true;
            this.grid1.BackColor = System.Drawing.Color.White;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.EnableSort = true;
            this.grid1.FixedRows = 3;
            this.grid1.Font = new System.Drawing.Font("Verdana", 8F);
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.grid1.Name = "grid1";
            this.grid1.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid1.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid1.Size = new System.Drawing.Size(294, 264);
            this.grid1.TabIndex = 0;
            this.grid1.TabStop = true;
            this.grid1.ToolTipText = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSSummary);
            this.panel1.Controls.Add(this.lblBSummary);
            this.panel1.Controls.Add(this.nudFont);
            this.panel1.Controls.Add(this.nudTickCount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 264);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 24);
            this.panel1.TabIndex = 5;
            // 
            // lblSSummary
            // 
            this.lblSSummary.AutoSize = true;
            this.lblSSummary.Location = new System.Drawing.Point(76, 5);
            this.lblSSummary.Name = "lblSSummary";
            this.lblSSummary.Size = new System.Drawing.Size(14, 13);
            this.lblSSummary.TabIndex = 3;
            this.lblSSummary.Text = "0";
            this.lblSSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBSummary
            // 
            this.lblBSummary.AutoSize = true;
            this.lblBSummary.Location = new System.Drawing.Point(9, 5);
            this.lblBSummary.Name = "lblBSummary";
            this.lblBSummary.Size = new System.Drawing.Size(14, 13);
            this.lblBSummary.TabIndex = 2;
            this.lblBSummary.Text = "0";
            this.lblBSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudFont
            // 
            this.nudFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudFont.Font = new System.Drawing.Font("Verdana", 7F);
            this.nudFont.Location = new System.Drawing.Point(250, 3);
            this.nudFont.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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
            this.nudFont.Size = new System.Drawing.Size(41, 19);
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
            this.nudTickCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.nudTickCount.Font = new System.Drawing.Font("Verdana", 7F);
            this.nudTickCount.Location = new System.Drawing.Point(197, 3);
            this.nudTickCount.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.nudTickCount.Name = "nudTickCount";
            this.nudTickCount.Size = new System.Drawing.Size(41, 19);
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
            // frmOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 288);
            this.Controls.Add(this.grid1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmOrder";
            this.Text = "下單";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTickCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private SourceGrid.Grid grid1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nudFont;
        private System.Windows.Forms.NumericUpDown nudTickCount;
        private System.Windows.Forms.Label lblSSummary;
        private System.Windows.Forms.Label lblBSummary;
    }
}