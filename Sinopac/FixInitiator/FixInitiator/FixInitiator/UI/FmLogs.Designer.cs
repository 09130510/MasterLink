namespace FixInitiator.UI
{
    partial class FmLogs
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
            this.lbLogs = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLogs
            // 
            this.lbLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbLogs.FormattingEnabled = true;
            this.lbLogs.ItemHeight = 12;
            this.lbLogs.Location = new System.Drawing.Point(0, 0);
            this.lbLogs.Name = "lbLogs";
            this.lbLogs.Size = new System.Drawing.Size(814, 206);
            this.lbLogs.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exortToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 26);
            // 
            // exortToolStripMenuItem
            // 
            this.exortToolStripMenuItem.Name = "exortToolStripMenuItem";
            this.exortToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exortToolStripMenuItem.Text = "Export";
            this.exortToolStripMenuItem.Click += new System.EventHandler(this.exortToolStripMenuItem_Click);
            // 
            // FmLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 206);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.lbLogs);
            this.Name = "FmLogs";
            this.Text = "FmLogs";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmLogs_FormClosing);
            this.Load += new System.EventHandler(this.FmLogs_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbLogs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exortToolStripMenuItem;
    }
}