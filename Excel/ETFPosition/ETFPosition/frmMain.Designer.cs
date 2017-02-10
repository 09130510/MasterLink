namespace ETFPosition
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.tsStatus = new System.Windows.Forms.StatusStrip();
            this.tsFX = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsStatusTxt = new System.Windows.Forms.ToolStripStatusLabel();
            this.chkAuto = new System.Windows.Forms.CheckBox();
            this.dtpDataDate = new System.Windows.Forms.DateTimePicker();
            this.dtpForeignDataDate = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsGenerate = new System.Windows.Forms.ToolStripButton();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsMail = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSetting = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tsStatus.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.tsStatus);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.chkAuto);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dtpDataDate);
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dtpForeignDataDate);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(497, 1);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(497, 63);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // tsStatus
            // 
            this.tsStatus.Dock = System.Windows.Forms.DockStyle.None;
            this.tsStatus.Font = new System.Drawing.Font("Verdana", 9F);
            this.tsStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFX,
            this.toolStripSeparator1,
            this.tsStatusTxt});
            this.tsStatus.Location = new System.Drawing.Point(0, 0);
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(497, 23);
            this.tsStatus.TabIndex = 0;
            // 
            // tsFX
            // 
            this.tsFX.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsFX.Name = "tsFX";
            this.tsFX.Size = new System.Drawing.Size(0, 18);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 23);
            // 
            // tsStatusTxt
            // 
            this.tsStatusTxt.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsStatusTxt.Name = "tsStatusTxt";
            this.tsStatusTxt.Size = new System.Drawing.Size(0, 18);
            // 
            // chkAuto
            // 
            this.chkAuto.AutoSize = true;
            this.chkAuto.Location = new System.Drawing.Point(33, 76);
            this.chkAuto.Name = "chkAuto";
            this.chkAuto.Size = new System.Drawing.Size(74, 17);
            this.chkAuto.TabIndex = 5;
            this.chkAuto.Text = "自動產生";
            this.chkAuto.UseVisualStyleBackColor = true;
            this.chkAuto.CheckedChanged += new System.EventHandler(this.chkAuto_CheckedChanged);
            // 
            // dtpDataDate
            // 
            this.dtpDataDate.Location = new System.Drawing.Point(3, 129);
            this.dtpDataDate.Name = "dtpDataDate";
            this.dtpDataDate.Size = new System.Drawing.Size(100, 20);
            this.dtpDataDate.TabIndex = 0;
            // 
            // dtpForeignDataDate
            // 
            this.dtpForeignDataDate.Location = new System.Drawing.Point(0, 0);
            this.dtpForeignDataDate.Name = "dtpForeignDataDate";
            this.dtpForeignDataDate.Size = new System.Drawing.Size(100, 20);
            this.dtpForeignDataDate.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Verdana", 8F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripLabel2,
            this.tsGenerate,
            this.tsOpen,
            this.tsMail,
            this.toolStripSeparator3,
            this.tsSetting,
            this.toolStripSeparator2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(497, 39);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(31, 36);
            this.toolStripLabel1.Text = "日期";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(55, 36);
            this.toolStripLabel2.Text = "國外庫存";
            // 
            // tsGenerate
            // 
            this.tsGenerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsGenerate.Enabled = false;
            this.tsGenerate.Image = global::ETFPosition.Properties.Resources.printer2;
            this.tsGenerate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsGenerate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsGenerate.Name = "tsGenerate";
            this.tsGenerate.Size = new System.Drawing.Size(36, 36);
            this.tsGenerate.ToolTipText = "產生報表";
            this.tsGenerate.Click += new System.EventHandler(this.tsGenerate_Click);
            // 
            // tsOpen
            // 
            this.tsOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsOpen.Image = global::ETFPosition.Properties.Resources.excel5;
            this.tsOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(36, 36);
            this.tsOpen.ToolTipText = "開啟報表";
            this.tsOpen.Click += new System.EventHandler(this.tsOpen_Click);
            // 
            // tsMail
            // 
            this.tsMail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsMail.Image = global::ETFPosition.Properties.Resources.mail3;
            this.tsMail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsMail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMail.Name = "tsMail";
            this.tsMail.Size = new System.Drawing.Size(36, 36);
            this.tsMail.ToolTipText = "寄出";
            this.tsMail.Click += new System.EventHandler(this.tsMail_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tsSetting
            // 
            this.tsSetting.CheckOnClick = true;
            this.tsSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSetting.Image = global::ETFPosition.Properties.Resources.Settings;
            this.tsSetting.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsSetting.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSetting.Name = "tsSetting";
            this.tsSetting.Size = new System.Drawing.Size(36, 36);
            this.tsSetting.ToolTipText = "設定";
            this.tsSetting.Visible = false;
            this.tsSetting.CheckedChanged += new System.EventHandler(this.tsSetting_CheckedChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            this.toolStripSeparator2.Visible = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 63);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Form1";
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.PerformLayout();
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tsStatus.ResumeLayout(false);
            this.tsStatus.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip tsStatus;
        private System.Windows.Forms.ToolStripButton tsGenerate;
        private System.Windows.Forms.ToolStripButton tsSetting;
        private System.Windows.Forms.DateTimePicker dtpDataDate;
        private System.Windows.Forms.DateTimePicker dtpForeignDataDate;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsOpen;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsMail;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusTxt;
        private System.Windows.Forms.ToolStripStatusLabel tsFX;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox chkAuto;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
    }
}

