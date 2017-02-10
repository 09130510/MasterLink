namespace PriceCalculator
{
    partial class frmETFSelect
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmETFSelect));
            this.etfBox = new PriceCalculator.ETFBox();
            this.SuspendLayout();
            // 
            // etfBox
            // 
            this.etfBox.Classification = "";
            this.etfBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.etfBox.Font = new System.Drawing.Font("Verdana", 8F);
            this.etfBox.Location = new System.Drawing.Point(0, 0);
            this.etfBox.Name = "etfBox";
            this.etfBox.Size = new System.Drawing.Size(413, 288);
            this.etfBox.TabIndex = 0;
            this.etfBox.AfterChecked += new System.Windows.Forms.TreeViewEventHandler(this.etfBox_AfterCheck);
            this.etfBox.AfterSelected += new System.Windows.Forms.TreeViewEventHandler(this.etfBox_AfterSelect);
            // 
            // frmETFSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 288);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.etfBox);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmETFSelect";
            this.ShowInTaskbar = false;
            this.Text = "ETF";
            this.ResumeLayout(false);

        }

        #endregion
        private ETFBox etfBox;
    }
}