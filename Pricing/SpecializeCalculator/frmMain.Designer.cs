namespace PriceCalculator
{
    partial class frmMain
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsPATSStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2003Theme1 = new WeifenLuo.WinFormsUI.Docking.VS2003Theme();
            this.muMenu = new System.Windows.Forms.MenuStrip();
            this.tsFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.tsFutureBase = new System.Windows.Forms.ToolStripMenuItem();            
            this.tsAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tsPATSConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.muMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dockPanel1);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(835, 202);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(835, 248);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.muMenu);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPATSStatus,
            this.tsPATSConnect});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(835, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // tsPATSStatus
            // 
            this.tsPATSStatus.Name = "tsPATSStatus";
            this.tsPATSStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // dockPanel1
            // 
            this.dockPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Font = new System.Drawing.Font("Verdana", 8F);
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(835, 202);
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.Theme = this.vS2003Theme1;
            // 
            // muMenu
            // 
            this.muMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.muMenu.Font = new System.Drawing.Font("Courier New", 9F);
            this.muMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFunction,
            this.tsAbout});
            this.muMenu.Location = new System.Drawing.Point(0, 0);
            this.muMenu.Name = "muMenu";
            this.muMenu.Size = new System.Drawing.Size(835, 24);
            this.muMenu.TabIndex = 0;
            this.muMenu.Text = "menuStrip1";
            // 
            // tsFunction
            // 
            this.tsFunction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFutureBase});
            this.tsFunction.Font = new System.Drawing.Font("Verdana", 9F);
            this.tsFunction.Name = "tsFunction";
            this.tsFunction.Size = new System.Drawing.Size(122, 20);
            this.tsFunction.Text = "Calculation Type";
            // 
            // tsFutureBase
            // 
            this.tsFutureBase.Name = "tsFutureBase";
            this.tsFutureBase.Size = new System.Drawing.Size(152, 22);
            this.tsFutureBase.Text = "[Fut. Base]";
            this.tsFutureBase.Click += new System.EventHandler(this.tsFutureBase_Click);            
            // 
            // tsAbout
            // 
            this.tsAbout.Font = new System.Drawing.Font("Verdana", 9F);
            this.tsAbout.Name = "tsAbout";
            this.tsAbout.Size = new System.Drawing.Size(56, 20);
            this.tsAbout.Text = "About";
            this.tsAbout.Click += new System.EventHandler(this.tsAbout_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1464790772_Paypal.png");
            this.imageList1.Images.SetKeyName(1, "1464790309_pay_pal_social_online_media_connect.ico");
            // 
            // tsPATSConnect
            // 
            this.tsPATSConnect.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsPATSConnect.Image = global::PriceCalculator.Properties.Resources._1464790309_pay_pal_social_online_media_connect;
            this.tsPATSConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPATSConnect.Name = "tsPATSConnect";
            this.tsPATSConnect.Size = new System.Drawing.Size(23, 20);
            this.tsPATSConnect.Click += new System.EventHandler(this.tsPATSConnect_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 248);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.muMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.Text = "Form1";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.muMenu.ResumeLayout(false);
            this.muMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private WeifenLuo.WinFormsUI.Docking.VS2003Theme vS2003Theme1;
        private System.Windows.Forms.ToolStripStatusLabel tsPATSStatus;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton tsPATSConnect;
        private System.Windows.Forms.MenuStrip muMenu;
        private System.Windows.Forms.ToolStripMenuItem tsFunction;
        private System.Windows.Forms.ToolStripMenuItem tsFutureBase;        
        private System.Windows.Forms.ToolStripMenuItem tsAbout;
    }
}

