namespace FixInitiator.UI
{
    partial class FmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.下單ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.下單ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.butOrdSvr = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下單ToolStripMenuItem,
            this.logToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(578, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 下單ToolStripMenuItem
            // 
            this.下單ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.下ToolStripMenuItem,
            this.下單ToolStripMenuItem1});
            this.下單ToolStripMenuItem.Name = "下單ToolStripMenuItem";
            this.下單ToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.下單ToolStripMenuItem.Text = "設定";
            // 
            // 下ToolStripMenuItem
            // 
            this.下ToolStripMenuItem.Name = "下ToolStripMenuItem";
            this.下ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.下ToolStripMenuItem.Text = "FIX 設定";
            // 
            // 下單ToolStripMenuItem1
            // 
            this.下單ToolStripMenuItem1.Name = "下單ToolStripMenuItem1";
            this.下單ToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.下單ToolStripMenuItem1.Text = "下單";
            this.下單ToolStripMenuItem1.Click += new System.EventHandler(this.下單ToolStripMenuItem1_Click);
            // 
            // logToolStripMenuItem
            // 
            this.logToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logToolStripMenuItem1});
            this.logToolStripMenuItem.Name = "logToolStripMenuItem";
            this.logToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.logToolStripMenuItem.Text = "系統記錄";
            // 
            // logToolStripMenuItem1
            // 
            this.logToolStripMenuItem1.Name = "logToolStripMenuItem1";
            this.logToolStripMenuItem1.Size = new System.Drawing.Size(98, 22);
            this.logToolStripMenuItem1.Text = "Log";
            this.logToolStripMenuItem1.Click += new System.EventHandler(this.logToolStripMenuItem1_Click);
            // 
            // butOrdSvr
            // 
            this.butOrdSvr.BackColor = System.Drawing.Color.Red;
            this.butOrdSvr.ForeColor = System.Drawing.Color.White;
            this.butOrdSvr.Location = new System.Drawing.Point(525, 0);
            this.butOrdSvr.Name = "butOrdSvr";
            this.butOrdSvr.Size = new System.Drawing.Size(53, 24);
            this.butOrdSvr.TabIndex = 1;
            this.butOrdSvr.Text = "連線";
            this.butOrdSvr.UseVisualStyleBackColor = false;
            this.butOrdSvr.Click += new System.EventHandler(this.butOrdSvr_Click);
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 24);
            this.Controls.Add(this.butOrdSvr);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmMain_FormClosing);
            this.Load += new System.EventHandler(this.FmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button butOrdSvr;
        private System.Windows.Forms.ToolStripMenuItem 下單ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 下單ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem1;


    }
}

