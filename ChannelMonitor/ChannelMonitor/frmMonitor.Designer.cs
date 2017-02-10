namespace ChannelMonitor
{
    partial class frmMonitor
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
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.grid1 = new SourceGrid.Grid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsSetAllow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSetDeny = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsIP = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsPort = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsInterval = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsConnect = new System.Windows.Forms.ToolStripButton();
            this.tsDisconnect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAllow = new System.Windows.Forms.ToolStripButton();
            this.tsDeny = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSetStop = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.grid1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(504, 297);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(504, 322);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // grid1
            // 
            this.grid1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grid1.AutoStretchColumnsToFitWidth = true;
            this.grid1.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut) 
            | SourceGrid.ClipboardMode.Paste) 
            | SourceGrid.ClipboardMode.Delete)));
            this.grid1.ContextMenuStrip = this.contextMenuStrip1;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.EnableSort = true;
            this.grid1.FixedColumns = 1;
            this.grid1.FixedRows = 2;
            this.grid1.Location = new System.Drawing.Point(0, 0);
            this.grid1.Name = "grid1";
            this.grid1.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.grid1.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.grid1.Size = new System.Drawing.Size(504, 297);
            this.grid1.TabIndex = 0;
            this.grid1.TabStop = true;
            this.grid1.ToolTipText = "";
            this.grid1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.grid1_PreviewKeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSetAllow,
            this.tsSetDeny,
            this.toolStripSeparator6,
            this.tsSetStop,
            this.tsRestart});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 120);
            // 
            // tsSetAllow
            // 
            this.tsSetAllow.Name = "tsSetAllow";
            this.tsSetAllow.Size = new System.Drawing.Size(152, 22);
            this.tsSetAllow.Text = "Allow";
            this.tsSetAllow.Click += new System.EventHandler(this.tsSetAllow_Click);
            // 
            // tsSetDeny
            // 
            this.tsSetDeny.Name = "tsSetDeny";
            this.tsSetDeny.Size = new System.Drawing.Size(152, 22);
            this.tsSetDeny.Text = "Deny";
            this.tsSetDeny.Click += new System.EventHandler(this.tsSetDeny_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Verdana", 7F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.tsIP,
            this.toolStripLabel2,
            this.tsPort,
            this.toolStripSeparator2,
            this.tsInterval,
            this.toolStripLabel3,
            this.toolStripSeparator4,
            this.tsConnect,
            this.tsDisconnect,
            this.toolStripSeparator1,
            this.toolStripSeparator3,
            this.tsAllow,
            this.tsDeny,
            this.toolStripSeparator5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(504, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(17, 22);
            this.toolStripLabel1.Text = "IP";
            // 
            // tsIP
            // 
            this.tsIP.Font = new System.Drawing.Font("Verdana", 7F);
            this.tsIP.Name = "tsIP";
            this.tsIP.Size = new System.Drawing.Size(85, 25);
            this.tsIP.TextChanged += new System.EventHandler(this.tsIP_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(27, 22);
            this.toolStripLabel2.Text = "Port";
            // 
            // tsPort
            // 
            this.tsPort.Font = new System.Drawing.Font("Verdana", 7F);
            this.tsPort.Name = "tsPort";
            this.tsPort.Size = new System.Drawing.Size(45, 25);
            this.tsPort.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tsPort.TextChanged += new System.EventHandler(this.tsIP_TextChanged);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsInterval
            // 
            this.tsInterval.Font = new System.Drawing.Font("Verdana", 7F);
            this.tsInterval.Name = "tsInterval";
            this.tsInterval.Size = new System.Drawing.Size(35, 25);
            this.tsInterval.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tsInterval.TextChanged += new System.EventHandler(this.tsIP_TextChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(15, 22);
            this.toolStripLabel3.Text = "秒";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tsConnect
            // 
            this.tsConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsConnect.Image = global::ChannelMonitor.Properties.Resources.Connect;
            this.tsConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsConnect.Name = "tsConnect";
            this.tsConnect.Size = new System.Drawing.Size(23, 22);
            this.tsConnect.Text = "Connect";
            this.tsConnect.Click += new System.EventHandler(this.tsConnect_Click);
            // 
            // tsDisconnect
            // 
            this.tsDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsDisconnect.Enabled = false;
            this.tsDisconnect.Image = global::ChannelMonitor.Properties.Resources.Disconnect;
            this.tsDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDisconnect.Name = "tsDisconnect";
            this.tsDisconnect.Size = new System.Drawing.Size(23, 22);
            this.tsDisconnect.ToolTipText = "Disconnect";
            this.tsDisconnect.Click += new System.EventHandler(this.tsDisconnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsAllow
            // 
            this.tsAllow.Image = global::ChannelMonitor.Properties.Resources._1478006034_document_text_accept;
            this.tsAllow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAllow.Name = "tsAllow";
            this.tsAllow.Size = new System.Drawing.Size(23, 22);
            this.tsAllow.ToolTipText = "Accept";
            this.tsAllow.Click += new System.EventHandler(this.tsAllow_Click);
            // 
            // tsDeny
            // 
            this.tsDeny.Font = new System.Drawing.Font("Verdana", 7F);
            this.tsDeny.Image = global::ChannelMonitor.Properties.Resources._1477901281_File_delete;
            this.tsDeny.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsDeny.Name = "tsDeny";
            this.tsDeny.Size = new System.Drawing.Size(23, 22);
            this.tsDeny.ToolTipText = "Deny";
            this.tsDeny.Click += new System.EventHandler(this.tsDeny_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(149, 6);
            // 
            // tsSetStop
            // 
            this.tsSetStop.Name = "tsSetStop";
            this.tsSetStop.Size = new System.Drawing.Size(152, 22);
            this.tsSetStop.Text = "Stop";
            this.tsSetStop.Click += new System.EventHandler(this.tsSetStop_Click);
            // 
            // tsRestart
            // 
            this.tsRestart.Name = "tsRestart";
            this.tsRestart.Size = new System.Drawing.Size(152, 22);
            this.tsRestart.Text = "ReStart";
            this.tsRestart.Click += new System.EventHandler(this.tsRestart_Click);
            // 
            // frmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 322);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMonitor";
            this.Text = "frmMonitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMonitor_FormClosing);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private SourceGrid.Grid grid1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsSetDeny;
        private System.Windows.Forms.ToolStripButton tsDeny;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox tsIP;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tsPort;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton tsConnect;
        private System.Windows.Forms.ToolStripButton tsDisconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox tsInterval;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsAllow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsSetAllow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsSetStop;
        private System.Windows.Forms.ToolStripMenuItem tsRestart;
    }
}