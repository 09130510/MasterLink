using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PriceCalculator
{
    partial class frmMain2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain2));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.tsRedisLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsRedis = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsiPushLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsiPush = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsOSCapitalLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsOSCapital = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPATSLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPATS = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2003Theme1 = new WeifenLuo.WinFormsUI.Docking.VS2003Theme();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.miIIV = new System.Windows.Forms.ToolStripMenuItem();
            this.miFXRate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miETFSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.miConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.miError = new System.Windows.Forms.ToolStripMenuItem();
            this.miAction = new System.Windows.Forms.ToolStripMenuItem();
            this.miNAVDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.miLockYP = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.StatusBar);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.dockPanel1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(535, 399);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(535, 473);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // StatusBar
            // 
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.StatusBar.Font = new System.Drawing.Font("Verdana", 8F);
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRedisLabel,
            this.tsRedis,
            this.toolStripStatusLabel2,
            this.tsiPushLabel,
            this.tsiPush,
            this.toolStripStatusLabel3,
            this.tsOSCapitalLabel,
            this.tsOSCapital,
            this.toolStripStatusLabel6,
            this.tsPATSLabel,
            this.tsPATS,
            this.toolStripStatusLabel1});
            this.StatusBar.Location = new System.Drawing.Point(0, 0);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(535, 25);
            this.StatusBar.TabIndex = 1;
            // 
            // tsRedisLabel
            // 
            this.tsRedisLabel.Name = "tsRedisLabel";
            this.tsRedisLabel.Size = new System.Drawing.Size(38, 20);
            this.tsRedisLabel.Text = "Redis";
            // 
            // tsRedis
            // 
            this.tsRedis.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsRedis.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsRedis.ForeColor = System.Drawing.Color.Crimson;
            this.tsRedis.Image = global::PriceCalculator.Properties.Resources.logout;
            this.tsRedis.Name = "tsRedis";
            this.tsRedis.Size = new System.Drawing.Size(20, 20);
            this.tsRedis.Tag = "Redis";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(4, 20);
            // 
            // tsiPushLabel
            // 
            this.tsiPushLabel.Name = "tsiPushLabel";
            this.tsiPushLabel.Size = new System.Drawing.Size(37, 20);
            this.tsiPushLabel.Text = "iPush";
            // 
            // tsiPush
            // 
            this.tsiPush.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsiPush.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsiPush.ForeColor = System.Drawing.Color.Crimson;
            this.tsiPush.Image = ((System.Drawing.Image)(resources.GetObject("tsiPush.Image")));
            this.tsiPush.Name = "tsiPush";
            this.tsiPush.Size = new System.Drawing.Size(20, 20);
            this.tsiPush.Tag = "iPush";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(4, 20);
            // 
            // tsOSCapitalLabel
            // 
            this.tsOSCapitalLabel.Name = "tsOSCapitalLabel";
            this.tsOSCapitalLabel.Size = new System.Drawing.Size(64, 20);
            this.tsOSCapitalLabel.Text = "OSCapital";
            // 
            // tsOSCapital
            // 
            this.tsOSCapital.AutoToolTip = true;
            this.tsOSCapital.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsOSCapital.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.tsOSCapital.DoubleClickEnabled = true;
            this.tsOSCapital.ForeColor = System.Drawing.Color.Crimson;
            this.tsOSCapital.Image = ((System.Drawing.Image)(resources.GetObject("tsOSCapital.Image")));
            this.tsOSCapital.Name = "tsOSCapital";
            this.tsOSCapital.Size = new System.Drawing.Size(20, 20);
            this.tsOSCapital.Tag = "OSCapital";
            this.tsOSCapital.DoubleClick += new System.EventHandler(this.tsOSCapital_DoubleClick);
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(4, 20);
            // 
            // tsPATSLabel
            // 
            this.tsPATSLabel.Name = "tsPATSLabel";
            this.tsPATSLabel.Size = new System.Drawing.Size(37, 20);
            this.tsPATSLabel.Text = "PATS";
            // 
            // tsPATS
            // 
            this.tsPATS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)(((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.tsPATS.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter;
            this.tsPATS.ForeColor = System.Drawing.Color.Crimson;
            this.tsPATS.Image = ((System.Drawing.Image)(resources.GetObject("tsPATS.Image")));
            this.tsPATS.Name = "tsPATS";
            this.tsPATS.Size = new System.Drawing.Size(20, 20);
            this.tsPATS.Tag = "PATS";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(4, 20);
            // 
            // dockPanel1
            // 
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DockLeftPortion = 0.5D;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.ShowDocumentIcon = true;
            this.dockPanel1.Size = new System.Drawing.Size(535, 399);
            this.dockPanel1.TabIndex = 0;
            this.dockPanel1.Theme = this.vS2003Theme1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Verdana", 9F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFunction,
            this.miAction});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(535, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miFunction
            // 
            this.miFunction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miIIV,
            this.miFXRate,
            this.toolStripSeparator1,
            this.miETFSelect,
            this.miConnection,
            this.miError});
            this.miFunction.Font = new System.Drawing.Font("Verdana", 9F);
            this.miFunction.Name = "miFunction";
            this.miFunction.Size = new System.Drawing.Size(43, 20);
            this.miFunction.Text = "功能";
            // 
            // miIIV
            // 
            this.miIIV.Font = new System.Drawing.Font("Verdana", 9F);
            this.miIIV.Image = global::PriceCalculator.Properties.Resources.invoice;
            this.miIIV.Name = "miIIV";
            this.miIIV.Size = new System.Drawing.Size(122, 22);
            this.miIIV.Text = "淨值";
            this.miIIV.Click += new System.EventHandler(this.miIIV_Click);
            // 
            // miFXRate
            // 
            this.miFXRate.Image = global::PriceCalculator.Properties.Resources.bank;
            this.miFXRate.Name = "miFXRate";
            this.miFXRate.Size = new System.Drawing.Size(122, 22);
            this.miFXRate.Text = "匯率";
            this.miFXRate.Click += new System.EventHandler(this.miFXRate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // miETFSelect
            // 
            this.miETFSelect.Image = global::PriceCalculator.Properties.Resources.issue;
            this.miETFSelect.Name = "miETFSelect";
            this.miETFSelect.Size = new System.Drawing.Size(122, 22);
            this.miETFSelect.Text = "商品選擇";
            this.miETFSelect.Click += new System.EventHandler(this.miETFSelect_Click);
            // 
            // miConnection
            // 
            this.miConnection.Image = global::PriceCalculator.Properties.Resources.link;
            this.miConnection.Name = "miConnection";
            this.miConnection.Size = new System.Drawing.Size(122, 22);
            this.miConnection.Text = "連線";
            this.miConnection.Click += new System.EventHandler(this.miConnection_Click);
            // 
            // miError
            // 
            this.miError.Image = global::PriceCalculator.Properties.Resources.advertising;
            this.miError.Name = "miError";
            this.miError.Size = new System.Drawing.Size(122, 22);
            this.miError.Text = "錯誤";
            this.miError.Click += new System.EventHandler(this.miError_Click);
            // 
            // miAction
            // 
            this.miAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miNAVDetail,
            this.miLockYP});
            this.miAction.Font = new System.Drawing.Font("Verdana", 9F);
            this.miAction.Name = "miAction";
            this.miAction.Size = new System.Drawing.Size(43, 20);
            this.miAction.Text = "動作";
            // 
            // miNAVDetail
            // 
            this.miNAVDetail.Image = global::PriceCalculator.Properties.Resources.category;
            this.miNAVDetail.Name = "miNAVDetail";
            this.miNAVDetail.Size = new System.Drawing.Size(195, 22);
            this.miNAVDetail.Text = "NAV Detail";
            this.miNAVDetail.Click += new System.EventHandler(this.miNAVDetail_Click);
            // 
            // miLockYP
            // 
            this.miLockYP.Name = "miLockYP";
            this.miLockYP.Size = new System.Drawing.Size(195, 22);
            this.miLockYP.Text = "Lock Ysterday Price";
            this.miLockYP.Click += new System.EventHandler(this.miLockYP_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Verdana", 9F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(535, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "bank.ico");
            this.imageList1.Images.SetKeyName(1, "category.ico");
            this.imageList1.Images.SetKeyName(2, "issue.ico");
            this.imageList1.Images.SetKeyName(3, "link.ico");
            this.imageList1.Images.SetKeyName(4, "list.ico");
            this.imageList1.Images.SetKeyName(5, "advertising.ico");
            // 
            // frmMain2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 473);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain2";
            this.Text = "frmMain2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain2_FormClosing);
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem miAction;
        private ToolStripMenuItem miConnection;
        private ToolStripMenuItem miError;
        private ToolStripMenuItem miETFSelect;
        private ToolStripMenuItem miFunction;
        private ToolStripMenuItem miFXRate;
        private ToolStripMenuItem miIIV;
        private ToolStripMenuItem miLockYP;
        private ToolStripMenuItem miNAVDetail;
        private StatusStrip StatusBar;
        private ToolStrip toolStrip1;        
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel toolStripStatusLabel3;
        private ToolStripStatusLabel toolStripStatusLabel6;
        private ToolStripStatusLabel tsiPush;
        private ToolStripStatusLabel tsiPushLabel;
        private ToolStripStatusLabel tsOSCapital;
        private ToolStripStatusLabel tsOSCapitalLabel;
        private ToolStripStatusLabel tsPATS;
        private ToolStripStatusLabel tsPATSLabel;
        private ToolStripStatusLabel tsRedis;
        private ToolStripStatusLabel tsRedisLabel;
        private VS2003Theme vS2003Theme1;
        private ImageList imageList1;
    }
}