namespace PATS
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsConnectAction = new System.Windows.Forms.ToolStripSplitButton();
            this.tsConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsOrder = new System.Windows.Forms.ToolStripButton();
            this.tsReport = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsOrderReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDealReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCancelReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsErrReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBring = new System.Windows.Forms.ToolStripButton();
            this.tsPin = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSaveLayout = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSetting = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsWriteMySQL = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsOrderSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsConnectSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsProductSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dockPanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(874, 523);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(874, 548);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // dockPanel1
            // 
            this.dockPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.ShowDocumentIcon = true;
            this.dockPanel1.Size = new System.Drawing.Size(874, 523);
            this.dockPanel1.SupportDeeplyNestedContent = true;
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.ActiveDocumentChanged += new System.EventHandler(this.dockPanel1_ActiveDocumentChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsConnectAction,
            this.toolStripSeparator1,
            this.tsOrder,
            this.tsReport,
            this.tsBring,
            this.tsPin,
            this.toolStripSeparator2,
            this.tsSaveLayout,
            this.toolStripSeparator3,
            this.tsSetting,
            this.toolStripSeparator7});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStrip1.Size = new System.Drawing.Size(874, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 2;
            // 
            // tsConnectAction
            // 
            this.tsConnectAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsConnect,
            this.tsDisconnect});
            this.tsConnectAction.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsConnectAction.ForeColor = System.Drawing.Color.Crimson;
            this.tsConnectAction.Image = global::PATS.Properties.Resources.Disconnect;
            this.tsConnectAction.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsConnectAction.Name = "tsConnectAction";
            this.tsConnectAction.Size = new System.Drawing.Size(32, 22);
            // 
            // tsConnect
            // 
            this.tsConnect.Name = "tsConnect";
            this.tsConnect.Size = new System.Drawing.Size(98, 22);
            this.tsConnect.Text = "連線";
            this.tsConnect.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // tsDisconnect
            // 
            this.tsDisconnect.Name = "tsDisconnect";
            this.tsDisconnect.Size = new System.Drawing.Size(98, 22);
            this.tsDisconnect.Text = "斷線";
            this.tsDisconnect.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsOrder
            // 
            this.tsOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsOrder.Image = ((System.Drawing.Image)(resources.GetObject("tsOrder.Image")));
            this.tsOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOrder.Name = "tsOrder";
            this.tsOrder.Size = new System.Drawing.Size(36, 22);
            this.tsOrder.Text = "下單";
            this.tsOrder.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // tsReport
            // 
            this.tsReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsOrderReport,
            this.tsDealReport,
            this.tsCancelReport,
            this.tsErrReport,
            this.toolStripSeparator4,
            this.tsSummary});
            this.tsReport.Image = ((System.Drawing.Image)(resources.GetObject("tsReport.Image")));
            this.tsReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsReport.Name = "tsReport";
            this.tsReport.Size = new System.Drawing.Size(45, 22);
            this.tsReport.Text = "回報";
            // 
            // tsOrderReport
            // 
            this.tsOrderReport.Name = "tsOrderReport";
            this.tsOrderReport.Size = new System.Drawing.Size(100, 22);
            this.tsOrderReport.Text = "委託";
            this.tsOrderReport.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // tsDealReport
            // 
            this.tsDealReport.Name = "tsDealReport";
            this.tsDealReport.Size = new System.Drawing.Size(100, 22);
            this.tsDealReport.Text = "成交";
            this.tsDealReport.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // tsCancelReport
            // 
            this.tsCancelReport.Name = "tsCancelReport";
            this.tsCancelReport.Size = new System.Drawing.Size(100, 22);
            this.tsCancelReport.Text = "取消";
            this.tsCancelReport.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // tsErrReport
            // 
            this.tsErrReport.Name = "tsErrReport";
            this.tsErrReport.Size = new System.Drawing.Size(100, 22);
            this.tsErrReport.Text = "錯誤";
            this.tsErrReport.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(97, 6);
            // 
            // tsSummary
            // 
            this.tsSummary.Name = "tsSummary";
            this.tsSummary.Size = new System.Drawing.Size(100, 22);
            this.tsSummary.Text = "彙總";
            this.tsSummary.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // tsBring
            // 
            this.tsBring.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsBring.CheckOnClick = true;
            this.tsBring.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsBring.Image = global::PATS.Properties.Resources.bring_backward_16;
            this.tsBring.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBring.Name = "tsBring";
            this.tsBring.Size = new System.Drawing.Size(23, 22);
            this.tsBring.Text = "toolStripButton1";
            this.tsBring.CheckedChanged += new System.EventHandler(this.tsBring_CheckedChanged);
            // 
            // tsPin
            // 
            this.tsPin.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsPin.CheckOnClick = true;
            this.tsPin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPin.Image = global::PATS.Properties.Resources.pin_outline;
            this.tsPin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPin.Name = "tsPin";
            this.tsPin.Size = new System.Drawing.Size(23, 22);
            this.tsPin.CheckedChanged += new System.EventHandler(this.tsPin_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsSaveLayout
            // 
            this.tsSaveLayout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsSaveLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsSaveLayout.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveLayout.Image")));
            this.tsSaveLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveLayout.Name = "tsSaveLayout";
            this.tsSaveLayout.Size = new System.Drawing.Size(60, 22);
            this.tsSaveLayout.Text = "儲存版面";
            this.tsSaveLayout.Click += new System.EventHandler(this.tsSaveLayout_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsSetting
            // 
            this.tsSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsWriteMySQL,
            this.toolStripSeparator6,
            this.tsOrderSetting,
            this.tsConnectSetting,
            this.toolStripSeparator5,
            this.tsProductSetting,
            this.toolStripMenuItem1});
            this.tsSetting.Image = ((System.Drawing.Image)(resources.GetObject("tsSetting.Image")));
            this.tsSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSetting.Name = "tsSetting";
            this.tsSetting.Size = new System.Drawing.Size(45, 22);
            this.tsSetting.Text = "設定";
            // 
            // tsWriteMySQL
            // 
            this.tsWriteMySQL.CheckOnClick = true;
            this.tsWriteMySQL.Name = "tsWriteMySQL";
            this.tsWriteMySQL.Size = new System.Drawing.Size(190, 22);
            this.tsWriteMySQL.Tag = "DEALTOEXCELDB;WRITE;Checked";
            this.tsWriteMySQL.Text = "寫入Excel DB";
            this.tsWriteMySQL.CheckedChanged += new System.EventHandler(this.tsWriteMySQL_CheckedChanged);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(187, 6);
            // 
            // tsOrderSetting
            // 
            this.tsOrderSetting.Name = "tsOrderSetting";
            this.tsOrderSetting.Size = new System.Drawing.Size(190, 22);
            this.tsOrderSetting.Text = "下單設定";
            this.tsOrderSetting.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // tsConnectSetting
            // 
            this.tsConnectSetting.Name = "tsConnectSetting";
            this.tsConnectSetting.Size = new System.Drawing.Size(190, 22);
            this.tsConnectSetting.Text = "連線設定";
            this.tsConnectSetting.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(187, 6);
            // 
            // tsProductSetting
            // 
            this.tsProductSetting.Name = "tsProductSetting";
            this.tsProductSetting.Size = new System.Drawing.Size(190, 22);
            this.tsProductSetting.Text = "商品篩選";
            this.tsProductSetting.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(190, 22);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 548);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton tsConnectAction;
        private System.Windows.Forms.ToolStripMenuItem tsConnect;
        private System.Windows.Forms.ToolStripMenuItem tsDisconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsOrder;
        private System.Windows.Forms.ToolStripDropDownButton tsReport;
        private System.Windows.Forms.ToolStripMenuItem tsOrderReport;
        private System.Windows.Forms.ToolStripMenuItem tsDealReport;
        private System.Windows.Forms.ToolStripMenuItem tsCancelReport;
        private System.Windows.Forms.ToolStripMenuItem tsErrReport;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsSummary;
        private System.Windows.Forms.ToolStripButton tsSaveLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton tsSetting;
        private System.Windows.Forms.ToolStripMenuItem tsOrderSetting;
        private System.Windows.Forms.ToolStripMenuItem tsConnectSetting;
        private System.Windows.Forms.ToolStripMenuItem tsProductSetting;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsWriteMySQL;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsPin;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton tsBring;
    }
}

