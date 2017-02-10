namespace Notifier
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.niNotify = new System.Windows.Forms.NotifyIcon();
            this.tsMenu = new System.Windows.Forms.ContextMenuStrip();
            this.tsSubscribe = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // niNotify
            // 
            this.niNotify.ContextMenuStrip = this.tsMenu;
            this.niNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("niNotify.Icon")));
            this.niNotify.Visible = true;
            this.niNotify.Click += new System.EventHandler(this.niNotify_Click);
            this.niNotify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niNotify_MouseDoubleClick);
            // 
            // tsMenu
            // 
            this.tsMenu.Font = new System.Drawing.Font("Verdana", 9F);
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSubscribe,
            this.toolStripSeparator1,
            this.tsSetting,
            this.tsSave,
            this.toolStripSeparator2,
            this.tsExit});
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(136, 104);
            // 
            // tsSubscribe
            // 
            this.tsSubscribe.Checked = true;
            this.tsSubscribe.CheckOnClick = true;
            this.tsSubscribe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsSubscribe.Name = "tsSubscribe";
            this.tsSubscribe.Size = new System.Drawing.Size(135, 22);
            this.tsSubscribe.Text = "Subscribe";
            this.tsSubscribe.CheckedChanged += new System.EventHandler(this.tsSubscribe_CheckedChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(132, 6);
            // 
            // tsSetting
            // 
            this.tsSetting.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsSetting.Name = "tsSetting";
            this.tsSetting.Size = new System.Drawing.Size(135, 22);
            this.tsSetting.Text = "Setting";
            this.tsSetting.Click += new System.EventHandler(this.tsStopNotify_Click);
            // 
            // tsSave
            // 
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(135, 22);
            this.tsSave.Text = "Save";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(132, 6);
            // 
            // tsExit
            // 
            this.tsExit.Image = global::Notifier.Properties.Resources._1478006134_Login_in;
            this.tsExit.Name = "tsExit";
            this.tsExit.Size = new System.Drawing.Size(135, 22);
            this.tsExit.Text = "Exit";
            this.tsExit.Click += new System.EventHandler(this.tsExit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(379, 306);
            this.Font = new System.Drawing.Font("Verdana", 9F);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.tsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon niNotify;
        private System.Windows.Forms.ContextMenuStrip tsMenu;
        private System.Windows.Forms.ToolStripMenuItem tsSubscribe;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsSetting;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsExit;
        private System.Windows.Forms.ToolStripMenuItem tsSave;
    }
}