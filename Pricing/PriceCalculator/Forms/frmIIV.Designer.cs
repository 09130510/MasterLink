using SourceGrid;
using System.Drawing;
using System.Windows.Forms;

namespace PriceCalculator
{
    partial class frmIIV
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIIV));
            this.gdNAV = new SourceGrid.Grid();
            this.SuspendLayout();
            // 
            // gdNAV
            // 
            this.gdNAV.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gdNAV.AutoStretchColumnsToFitWidth = true;
            this.gdNAV.BackColor = System.Drawing.SystemColors.Control;
            this.gdNAV.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut) 
            | SourceGrid.ClipboardMode.Paste) 
            | SourceGrid.ClipboardMode.Delete)));
            this.gdNAV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdNAV.EnableSort = true;
            this.gdNAV.FixedColumns = 1;
            this.gdNAV.FixedRows = 1;
            this.gdNAV.Location = new System.Drawing.Point(0, 0);
            this.gdNAV.Margin = new System.Windows.Forms.Padding(5);
            this.gdNAV.Name = "gdNAV";
            this.gdNAV.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gdNAV.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gdNAV.Size = new System.Drawing.Size(331, 284);
            this.gdNAV.TabIndex = 2;
            this.gdNAV.TabStop = true;
            this.gdNAV.ToolTipText = "";
            // 
            // frmIIV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 284);
            this.Controls.Add(this.gdNAV);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIIV";
            this.Text = "IIV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNAV2_FormClosing);
            this.ResumeLayout(false);

        }
        #endregion
        private Grid gdNAV;
    }
}