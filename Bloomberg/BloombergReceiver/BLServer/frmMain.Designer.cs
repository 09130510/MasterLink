using WeifenLuo.WinFormsUI.Docking;

namespace BLPServer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsStatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsConnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPublish = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsWindow = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsSubscribe = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMsg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer2.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer2.ContentPanel.SuspendLayout();
            this.toolStripContainer2.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(650, 393);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(650, 418);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // dockPanel1
            // 
            this.dockPanel1.AllowEndUserNestedDocking = false;
            this.dockPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockBottomPortion = 0.35D;
            this.dockPanel1.DockRightPortion = 0.27D;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(650, 371);
            this.dockPanel1.SupportDeeplyNestedContent = true;
            this.dockPanel1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Verdana", 8F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.tsStatus,
            this.toolStripSeparator1,
            this.tsConnect,
            this.toolStripSeparator3,
            this.tsPublish,
            this.toolStripSeparator4,
            this.toolStripSeparator7,
            this.tsWindow});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(650, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsStatus
            // 
            this.tsStatus.BackColor = System.Drawing.Color.Crimson;
            this.tsStatus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tsStatus.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsStatus.ForeColor = System.Drawing.Color.Crimson;
            this.tsStatus.ImageTransparentColor = System.Drawing.Color.Crimson;
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(19, 22);
            this.tsStatus.Text = "█";
            this.tsStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsConnect
            // 
            this.tsConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsConnect.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsConnect.Image = ((System.Drawing.Image)(resources.GetObject("tsConnect.Image")));
            this.tsConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsConnect.Name = "tsConnect";
            this.tsConnect.Size = new System.Drawing.Size(73, 22);
            this.tsConnect.Text = "Bloomberg";
            this.tsConnect.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsPublish
            // 
            this.tsPublish.CheckOnClick = true;
            this.tsPublish.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsPublish.Image = ((System.Drawing.Image)(resources.GetObject("tsPublish.Image")));
            this.tsPublish.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPublish.Name = "tsPublish";
            this.tsPublish.Size = new System.Drawing.Size(51, 22);
            this.tsPublish.Tag = "REDIS;PUBLISH;Checked";
            this.tsPublish.Text = "Publish";
            this.tsPublish.CheckedChanged += new System.EventHandler(this.tsPublish_CheckedChanged);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // tsWindow
            // 
            this.tsWindow.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSubscribe,
            this.tsRequest,
            this.toolStripSeparator5,
            this.tsSetting,
            this.toolStripSeparator6,
            this.tsMsg});
            this.tsWindow.Image = ((System.Drawing.Image)(resources.GetObject("tsWindow.Image")));
            this.tsWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsWindow.Name = "tsWindow";
            this.tsWindow.Size = new System.Drawing.Size(70, 22);
            this.tsWindow.Text = "Windows";
            // 
            // tsSubscribe
            // 
            this.tsSubscribe.Name = "tsSubscribe";
            this.tsSubscribe.Size = new System.Drawing.Size(130, 22);
            this.tsSubscribe.Text = "Subscribe";
            this.tsSubscribe.Click += new System.EventHandler(this.tsSubscribe_Click);
            // 
            // tsRequest
            // 
            this.tsRequest.Name = "tsRequest";
            this.tsRequest.Size = new System.Drawing.Size(130, 22);
            this.tsRequest.Text = "Query";
            this.tsRequest.Click += new System.EventHandler(this.tsRequest_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(127, 6);
            // 
            // tsSetting
            // 
            this.tsSetting.Name = "tsSetting";
            this.tsSetting.Size = new System.Drawing.Size(130, 22);
            this.tsSetting.Text = "Setting";
            this.tsSetting.Click += new System.EventHandler(this.tsSetting_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(127, 6);
            // 
            // tsMsg
            // 
            this.tsMsg.Name = "tsMsg";
            this.tsMsg.Size = new System.Drawing.Size(130, 22);
            this.tsMsg.Text = "Message";
            this.tsMsg.Click += new System.EventHandler(this.tsMsg_Click);
            // 
            // toolStripContainer2
            // 
            // 
            // toolStripContainer2.BottomToolStripPanel
            // 
            this.toolStripContainer2.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Controls.Add(this.dockPanel1);
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(650, 371);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.LeftToolStripPanelVisible = false;
            this.toolStripContainer2.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.RightToolStripPanelVisible = false;
            this.toolStripContainer2.Size = new System.Drawing.Size(650, 418);
            this.toolStripContainer2.TabIndex = 3;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // toolStripContainer2.TopToolStripPanel
            // 
            this.toolStripContainer2.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(650, 22);
            this.statusStrip1.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 418);
            this.Controls.Add(this.toolStripContainer2);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer2.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer2.ContentPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer2.TopToolStripPanel.PerformLayout();
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsConnect;
        private System.Windows.Forms.ToolStripLabel tsStatus;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsPublish;
        private System.Windows.Forms.ToolStripDropDownButton tsWindow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsMsg;
        private System.Windows.Forms.ToolStripMenuItem tsRequest;
        private System.Windows.Forms.ToolStripMenuItem tsSetting;
        private System.Windows.Forms.ToolStripMenuItem tsSubscribe;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}

