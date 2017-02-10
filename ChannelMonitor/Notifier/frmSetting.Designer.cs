namespace Notifier
{
    partial class frmSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetting));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnAdd = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.cboServer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSeconds = new System.Windows.Forms.TextBox();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.gdStop = new SourceGrid.Grid();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsModify = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsExit1 = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpStopNotify = new System.Windows.Forms.TabPage();
            this.tpInfoNotify = new System.Windows.Forms.TabPage();
            this.toolStripContainer2 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cboStyle2 = new System.Windows.Forms.ComboBox();
            this.btnAdd2 = new System.Windows.Forms.Button();
            this.txtItem2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtChannel2 = new System.Windows.Forms.TextBox();
            this.cboServer2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gdInfo = new SourceGrid.Grid();
            this.tpQuestionNotify = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.cboServer1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtChannel1 = new System.Windows.Forms.TextBox();
            this.txtItem1 = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpStopNotify.SuspendLayout();
            this.tpInfoNotify.SuspendLayout();
            this.toolStripContainer2.ContentPanel.SuspendLayout();
            this.toolStripContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tpQuestionNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(570, 128);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(3, 3);
            this.toolStripContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(570, 153);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnAdd);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.cboServer);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.txtSeconds);
            this.splitContainer1.Panel1.Controls.Add(this.txtChannel);
            this.splitContainer1.Panel1.Controls.Add(this.txtItem);
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gdStop);
            this.splitContainer1.Size = new System.Drawing.Size(570, 128);
            this.splitContainer1.SplitterDistance = 36;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.ImageIndex = 0;
            this.btnAdd.ImageList = this.imageList1;
            this.btnAdd.Location = new System.Drawing.Point(514, 5);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(55, 26);
            this.btnAdd.TabIndex = 29;
            this.btnAdd.Text = "New";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1468906387_add_list.ico");
            this.imageList1.Images.SetKeyName(1, "Blue.ico");
            this.imageList1.Images.SetKeyName(2, "Gray.ico");
            this.imageList1.Images.SetKeyName(3, "Green.ico");
            this.imageList1.Images.SetKeyName(4, "Orange.ico");
            this.imageList1.Images.SetKeyName(5, "Red.ico");
            this.imageList1.Images.SetKeyName(6, "1478006134_Login_in.ico");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 7F);
            this.label4.Location = new System.Drawing.Point(402, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "Seconds";
            // 
            // cboServer
            // 
            this.cboServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboServer.FormattingEnabled = true;
            this.cboServer.Location = new System.Drawing.Point(50, 8);
            this.cboServer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboServer.Name = "cboServer";
            this.cboServer.Size = new System.Drawing.Size(91, 21);
            this.cboServer.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 7F);
            this.label2.Location = new System.Drawing.Point(141, 12);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "Channel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 7F);
            this.label1.Location = new System.Drawing.Point(10, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "Server";
            // 
            // txtSeconds
            // 
            this.txtSeconds.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSeconds.Location = new System.Drawing.Point(453, 8);
            this.txtSeconds.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSeconds.Name = "txtSeconds";
            this.txtSeconds.Size = new System.Drawing.Size(46, 21);
            this.txtSeconds.TabIndex = 25;
            this.txtSeconds.Text = "60";
            this.txtSeconds.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtChannel
            // 
            this.txtChannel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChannel.Location = new System.Drawing.Point(192, 8);
            this.txtChannel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(70, 21);
            this.txtChannel.TabIndex = 23;
            // 
            // txtItem
            // 
            this.txtItem.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItem.Location = new System.Drawing.Point(262, 8);
            this.txtItem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(140, 21);
            this.txtItem.TabIndex = 24;
            // 
            // gdStop
            // 
            this.gdStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gdStop.AutoStretchColumnsToFitWidth = true;
            this.gdStop.BackColor = System.Drawing.SystemColors.Control;
            this.gdStop.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut) 
            | SourceGrid.ClipboardMode.Paste) 
            | SourceGrid.ClipboardMode.Delete)));
            this.gdStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdStop.EnableSort = true;
            this.gdStop.FixedColumns = 1;
            this.gdStop.FixedRows = 1;
            this.gdStop.Location = new System.Drawing.Point(0, 0);
            this.gdStop.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gdStop.Name = "gdStop";
            this.gdStop.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gdStop.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gdStop.Size = new System.Drawing.Size(570, 128);
            this.gdStop.TabIndex = 1;
            this.gdStop.TabStop = true;
            this.gdStop.ToolTipText = "";
            this.gdStop.Click += new System.EventHandler(this.grid1_Click);
            this.gdStop.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.grid1_PreviewKeyDown);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator6,
            this.tsAdd,
            this.toolStripSeparator3,
            this.tsModify,
            this.toolStripSeparator4,
            this.tsDelete,
            this.toolStripSeparator5,
            this.tsExit1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(570, 25);
            this.toolStrip2.Stretch = true;
            this.toolStrip2.TabIndex = 1;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // tsAdd
            // 
            this.tsAdd.CheckOnClick = true;
            this.tsAdd.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsAdd.Image = global::Notifier.Properties.Resources._1468906387_add_list;
            this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAdd.Name = "tsAdd";
            this.tsAdd.Size = new System.Drawing.Size(49, 22);
            this.tsAdd.Text = "Add";
            this.tsAdd.CheckedChanged += new System.EventHandler(this.tsAdd_CheckedChanged);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsModify
            // 
            this.tsModify.CheckOnClick = true;
            this.tsModify.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsModify.Image = global::Notifier.Properties.Resources._1478006112_Edit;
            this.tsModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsModify.Name = "tsModify";
            this.tsModify.Size = new System.Drawing.Size(64, 22);
            this.tsModify.Text = "Modify";
            this.tsModify.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.toolStripSeparator4.Visible = false;
            // 
            // tsDelete
            // 
            this.tsDelete.Enabled = false;
            this.tsDelete.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsDelete.Image = global::Notifier.Properties.Resources._1477653410_No;
            this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDelete.Name = "tsDelete";
            this.tsDelete.Size = new System.Drawing.Size(64, 22);
            this.tsDelete.Text = "Delete";
            this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsExit1
            // 
            this.tsExit1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsExit1.Font = new System.Drawing.Font("Verdana", 7F);
            this.tsExit1.Image = global::Notifier.Properties.Resources._1478006134_Login_in;
            this.tsExit1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsExit1.Name = "tsExit1";
            this.tsExit1.Size = new System.Drawing.Size(45, 22);
            this.tsExit1.Text = "Exit";
            this.tsExit1.Visible = false;
            this.tsExit1.Click += new System.EventHandler(this.tsExit1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl1.Controls.Add(this.tpStopNotify);
            this.tabControl1.Controls.Add(this.tpInfoNotify);
            this.tabControl1.Controls.Add(this.tpQuestionNotify);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 25);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(584, 192);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            // 
            // tpStopNotify
            // 
            this.tpStopNotify.Controls.Add(this.toolStripContainer1);
            this.tpStopNotify.ImageIndex = 5;
            this.tpStopNotify.Location = new System.Drawing.Point(4, 29);
            this.tpStopNotify.Name = "tpStopNotify";
            this.tpStopNotify.Padding = new System.Windows.Forms.Padding(3);
            this.tpStopNotify.Size = new System.Drawing.Size(576, 159);
            this.tpStopNotify.TabIndex = 0;
            this.tpStopNotify.Text = "Stop";
            this.tpStopNotify.UseVisualStyleBackColor = true;
            // 
            // tpInfoNotify
            // 
            this.tpInfoNotify.Controls.Add(this.toolStripContainer2);
            this.tpInfoNotify.ImageIndex = 1;
            this.tpInfoNotify.Location = new System.Drawing.Point(4, 29);
            this.tpInfoNotify.Name = "tpInfoNotify";
            this.tpInfoNotify.Padding = new System.Windows.Forms.Padding(3);
            this.tpInfoNotify.Size = new System.Drawing.Size(576, 159);
            this.tpInfoNotify.TabIndex = 1;
            this.tpInfoNotify.Text = "Information";
            this.tpInfoNotify.UseVisualStyleBackColor = true;
            // 
            // toolStripContainer2
            // 
            this.toolStripContainer2.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer2.ContentPanel
            // 
            this.toolStripContainer2.ContentPanel.Controls.Add(this.splitContainer2);
            this.toolStripContainer2.ContentPanel.Size = new System.Drawing.Size(570, 128);
            this.toolStripContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer2.LeftToolStripPanelVisible = false;
            this.toolStripContainer2.Location = new System.Drawing.Point(3, 3);
            this.toolStripContainer2.Name = "toolStripContainer2";
            this.toolStripContainer2.RightToolStripPanelVisible = false;
            this.toolStripContainer2.Size = new System.Drawing.Size(570, 153);
            this.toolStripContainer2.TabIndex = 0;
            this.toolStripContainer2.Text = "toolStripContainer2";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.cboStyle2);
            this.splitContainer2.Panel1.Controls.Add(this.btnAdd2);
            this.splitContainer2.Panel1.Controls.Add(this.txtItem2);
            this.splitContainer2.Panel1.Controls.Add(this.label8);
            this.splitContainer2.Panel1.Controls.Add(this.txtChannel2);
            this.splitContainer2.Panel1.Controls.Add(this.cboServer2);
            this.splitContainer2.Panel1.Controls.Add(this.label7);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gdInfo);
            this.splitContainer2.Size = new System.Drawing.Size(570, 128);
            this.splitContainer2.SplitterDistance = 121;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 0;
            // 
            // cboStyle2
            // 
            this.cboStyle2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStyle2.FormattingEnabled = true;
            this.cboStyle2.Location = new System.Drawing.Point(6, 1);
            this.cboStyle2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboStyle2.Name = "cboStyle2";
            this.cboStyle2.Size = new System.Drawing.Size(107, 21);
            this.cboStyle2.TabIndex = 35;
            this.cboStyle2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboStyle2_DrawItem);
            // 
            // btnAdd2
            // 
            this.btnAdd2.Enabled = false;
            this.btnAdd2.ImageIndex = 0;
            this.btnAdd2.ImageList = this.imageList1;
            this.btnAdd2.Location = new System.Drawing.Point(6, 127);
            this.btnAdd2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAdd2.Name = "btnAdd2";
            this.btnAdd2.Size = new System.Drawing.Size(107, 26);
            this.btnAdd2.TabIndex = 34;
            this.btnAdd2.Text = "New";
            this.btnAdd2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd2.UseVisualStyleBackColor = true;
            this.btnAdd2.Click += new System.EventHandler(this.btnAdd1_Click);
            // 
            // txtItem2
            // 
            this.txtItem2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItem2.Location = new System.Drawing.Point(6, 106);
            this.txtItem2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtItem2.Name = "txtItem2";
            this.txtItem2.Size = new System.Drawing.Size(107, 21);
            this.txtItem2.TabIndex = 31;
            this.txtItem2.Validated += new System.EventHandler(this.cboServer2_Validated);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.DimGray;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Font = new System.Drawing.Font("Verdana", 7F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(6, 64);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 21);
            this.label8.TabIndex = 30;
            this.label8.Text = "Channel";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtChannel2
            // 
            this.txtChannel2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChannel2.Location = new System.Drawing.Point(6, 85);
            this.txtChannel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtChannel2.Name = "txtChannel2";
            this.txtChannel2.Size = new System.Drawing.Size(107, 21);
            this.txtChannel2.TabIndex = 29;
            this.txtChannel2.Validated += new System.EventHandler(this.cboServer2_Validated);
            // 
            // cboServer2
            // 
            this.cboServer2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServer2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboServer2.FormattingEnabled = true;
            this.cboServer2.Location = new System.Drawing.Point(6, 43);
            this.cboServer2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboServer2.Name = "cboServer2";
            this.cboServer2.Size = new System.Drawing.Size(107, 21);
            this.cboServer2.TabIndex = 27;
            this.cboServer2.Validated += new System.EventHandler(this.cboServer2_Validated);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.DimGray;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = new System.Drawing.Font("Verdana", 7F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 21);
            this.label7.TabIndex = 28;
            this.label7.Text = "Server";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gdInfo
            // 
            this.gdInfo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gdInfo.AutoStretchColumnsToFitWidth = true;
            this.gdInfo.BackColor = System.Drawing.SystemColors.Control;
            this.gdInfo.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut) 
            | SourceGrid.ClipboardMode.Paste) 
            | SourceGrid.ClipboardMode.Delete)));
            this.gdInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdInfo.EnableSort = true;
            this.gdInfo.FixedColumns = 1;
            this.gdInfo.FixedRows = 1;
            this.gdInfo.Location = new System.Drawing.Point(0, 0);
            this.gdInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gdInfo.Name = "gdInfo";
            this.gdInfo.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gdInfo.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gdInfo.Size = new System.Drawing.Size(448, 128);
            this.gdInfo.TabIndex = 2;
            this.gdInfo.TabStop = true;
            this.gdInfo.ToolTipText = "";
            this.gdInfo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gdInfo_PreviewKeyDown);
            // 
            // tpQuestionNotify
            // 
            this.tpQuestionNotify.Controls.Add(this.label3);
            this.tpQuestionNotify.Controls.Add(this.btnSend);
            this.tpQuestionNotify.Controls.Add(this.cboServer1);
            this.tpQuestionNotify.Controls.Add(this.label5);
            this.tpQuestionNotify.Controls.Add(this.label6);
            this.tpQuestionNotify.Controls.Add(this.txtChannel1);
            this.tpQuestionNotify.Controls.Add(this.txtItem1);
            this.tpQuestionNotify.Controls.Add(this.txtValue);
            this.tpQuestionNotify.ImageIndex = 2;
            this.tpQuestionNotify.Location = new System.Drawing.Point(4, 29);
            this.tpQuestionNotify.Name = "tpQuestionNotify";
            this.tpQuestionNotify.Size = new System.Drawing.Size(576, 159);
            this.tpQuestionNotify.TabIndex = 4;
            this.tpQuestionNotify.Text = "Question";
            this.tpQuestionNotify.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 7F);
            this.label3.Location = new System.Drawing.Point(156, 44);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 12);
            this.label3.TabIndex = 36;
            this.label3.Text = "Value";
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(334, 38);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 35;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // cboServer1
            // 
            this.cboServer1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServer1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboServer1.FormattingEnabled = true;
            this.cboServer1.Location = new System.Drawing.Point(57, 13);
            this.cboServer1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cboServer1.Name = "cboServer1";
            this.cboServer1.Size = new System.Drawing.Size(91, 21);
            this.cboServer1.TabIndex = 28;
            this.cboServer1.Validated += new System.EventHandler(this.cboServer1_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 7F);
            this.label5.Location = new System.Drawing.Point(148, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "Channel";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 7F);
            this.label6.Location = new System.Drawing.Point(17, 17);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 12);
            this.label6.TabIndex = 31;
            this.label6.Text = "Server";
            // 
            // txtChannel1
            // 
            this.txtChannel1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChannel1.Location = new System.Drawing.Point(199, 13);
            this.txtChannel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtChannel1.Name = "txtChannel1";
            this.txtChannel1.Size = new System.Drawing.Size(70, 21);
            this.txtChannel1.TabIndex = 29;
            this.txtChannel1.Validated += new System.EventHandler(this.cboServer1_Validated);
            // 
            // txtItem1
            // 
            this.txtItem1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItem1.Location = new System.Drawing.Point(269, 13);
            this.txtItem1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtItem1.Name = "txtItem1";
            this.txtItem1.Size = new System.Drawing.Size(140, 21);
            this.txtItem1.TabIndex = 30;
            this.txtItem1.Validated += new System.EventHandler(this.cboServer1_Validated);
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(199, 40);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(100, 20);
            this.txtValue.TabIndex = 3;
            this.txtValue.Validated += new System.EventHandler(this.cboServer1_Validated);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Verdana", 7F);
            this.button1.ImageIndex = 6;
            this.button1.ImageList = this.imageList1;
            this.button1.Location = new System.Drawing.Point(554, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 26);
            this.button1.TabIndex = 2;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.tsExit1_Click);
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 192);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(665, 580);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimumSize = new System.Drawing.Size(600, 230);
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStopNotify_FormClosing);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpStopNotify.ResumeLayout(false);
            this.tpInfoNotify.ResumeLayout(false);
            this.toolStripContainer2.ContentPanel.ResumeLayout(false);
            this.toolStripContainer2.ResumeLayout(false);
            this.toolStripContainer2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tpQuestionNotify.ResumeLayout(false);
            this.tpQuestionNotify.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private SourceGrid.Grid gdStop;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton tsAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsModify;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton tsDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsExit1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSeconds;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpStopNotify;
        private System.Windows.Forms.TabPage tpInfoNotify;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tpQuestionNotify;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ComboBox cboServer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtChannel1;
        private System.Windows.Forms.TextBox txtItem1;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripContainer toolStripContainer2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private SourceGrid.Grid gdInfo;
        private System.Windows.Forms.TextBox txtItem2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtChannel2;
        private System.Windows.Forms.ComboBox cboServer2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAdd2;
        private System.Windows.Forms.ComboBox cboStyle2;
    }
}