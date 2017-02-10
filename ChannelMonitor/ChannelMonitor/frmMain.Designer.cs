namespace ChannelMonitor
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
            this.tsDisplayAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsIPLabel = new System.Windows.Forms.ToolStripLabel();
            this.tsIP = new System.Windows.Forms.ToolStripTextBox();
            this.tsPortLabel = new System.Windows.Forms.ToolStripLabel();
            this.tsPort = new System.Windows.Forms.ToolStripTextBox();
            this.tsIntervalLabel = new System.Windows.Forms.ToolStripLabel();
            this.tsInterval = new System.Windows.Forms.ToolStripTextBox();
            this.tsAdd = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(770, 338);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(770, 363);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // dockPanel1
            // 
            this.dockPanel1.DefaultFloatWindowSize = new System.Drawing.Size(520, 360);
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(770, 338);
            this.dockPanel1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Verdana", 7F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDisplayAdd,
            this.toolStripSeparator1,
            this.tsSeparator1,
            this.tsIPLabel,
            this.tsIP,
            this.tsPortLabel,
            this.tsPort,
            this.tsIntervalLabel,
            this.tsInterval,
            this.tsAdd,
            this.tsSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(770, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // tsDisplayAdd
            // 
            this.tsDisplayAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDisplayAdd.Font = new System.Drawing.Font("Verdana", 7F);
            this.tsDisplayAdd.Image = global::ChannelMonitor.Properties.Resources._1477983371_BT_arrow_right;
            this.tsDisplayAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDisplayAdd.Name = "tsDisplayAdd";
            this.tsDisplayAdd.Size = new System.Drawing.Size(23, 22);
            this.tsDisplayAdd.Text = "RIGHT";
            this.tsDisplayAdd.Click += new System.EventHandler(this.tsDisplayAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsSeparator1
            // 
            this.tsSeparator1.Name = "tsSeparator1";
            this.tsSeparator1.Size = new System.Drawing.Size(6, 25);
            this.tsSeparator1.Visible = false;
            // 
            // tsIPLabel
            // 
            this.tsIPLabel.Name = "tsIPLabel";
            this.tsIPLabel.Size = new System.Drawing.Size(17, 22);
            this.tsIPLabel.Text = "IP";
            this.tsIPLabel.Visible = false;
            // 
            // tsIP
            // 
            this.tsIP.Name = "tsIP";
            this.tsIP.Size = new System.Drawing.Size(90, 25);
            this.tsIP.Visible = false;
            // 
            // tsPortLabel
            // 
            this.tsPortLabel.Name = "tsPortLabel";
            this.tsPortLabel.Size = new System.Drawing.Size(27, 22);
            this.tsPortLabel.Text = "Port";
            this.tsPortLabel.Visible = false;
            // 
            // tsPort
            // 
            this.tsPort.Name = "tsPort";
            this.tsPort.Size = new System.Drawing.Size(45, 25);
            this.tsPort.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tsPort.Visible = false;
            // 
            // tsIntervalLabel
            // 
            this.tsIntervalLabel.Name = "tsIntervalLabel";
            this.tsIntervalLabel.Size = new System.Drawing.Size(15, 22);
            this.tsIntervalLabel.Text = "秒";
            this.tsIntervalLabel.Visible = false;
            // 
            // tsInterval
            // 
            this.tsInterval.Name = "tsInterval";
            this.tsInterval.Size = new System.Drawing.Size(35, 25);
            this.tsInterval.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tsInterval.Visible = false;
            // 
            // tsAdd
            // 
            this.tsAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsAdd.Image = global::ChannelMonitor.Properties.Resources._1468906387_add_list;
            this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAdd.Name = "tsAdd";
            this.tsAdd.Size = new System.Drawing.Size(23, 22);
            this.tsAdd.Text = "toolStripButton1";
            this.tsAdd.Visible = false;
            this.tsAdd.Click += new System.EventHandler(this.tsAdd_Click);
            // 
            // tsSeparator2
            // 
            this.tsSeparator2.Name = "tsSeparator2";
            this.tsSeparator2.Size = new System.Drawing.Size(6, 25);
            this.tsSeparator2.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 363);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
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
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private System.Windows.Forms.ToolStripButton tsAdd;
        private System.Windows.Forms.ToolStripButton tsDisplayAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator tsSeparator1;
        private System.Windows.Forms.ToolStripLabel tsIPLabel;
        private System.Windows.Forms.ToolStripTextBox tsIP;
        private System.Windows.Forms.ToolStripLabel tsPortLabel;
        private System.Windows.Forms.ToolStripTextBox tsPort;
        private System.Windows.Forms.ToolStripTextBox tsInterval;
        private System.Windows.Forms.ToolStripLabel tsIntervalLabel;
        private System.Windows.Forms.ToolStripSeparator tsSeparator2;
    }
}

