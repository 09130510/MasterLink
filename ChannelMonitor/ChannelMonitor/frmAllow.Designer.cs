namespace ChannelMonitor
{
    partial class frmAllow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAllow));
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstChannel = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lstItem = new System.Windows.Forms.ListBox();
            this.lblChannel = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsDisplayAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsChannelLabel = new System.Windows.Forms.ToolStripLabel();
            this.tsChannel = new System.Windows.Forms.ToolStripTextBox();
            this.tsItemLabel = new System.Windows.Forms.ToolStripLabel();
            this.tsItem = new System.Windows.Forms.ToolStripTextBox();
            this.tsAdd = new System.Windows.Forms.ToolStripButton();
            this.tsSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(461, 387);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(461, 412);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstChannel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(461, 387);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 1;
            // 
            // lstChannel
            // 
            this.lstChannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstChannel.FormattingEnabled = true;
            this.lstChannel.ItemHeight = 12;
            this.lstChannel.Location = new System.Drawing.Point(0, 0);
            this.lstChannel.Name = "lstChannel";
            this.lstChannel.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstChannel.Size = new System.Drawing.Size(201, 387);
            this.lstChannel.TabIndex = 0;
            this.lstChannel.SelectedIndexChanged += new System.EventHandler(this.lstChannel_SelectedIndexChanged);
            this.lstChannel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstChannel_KeyDown);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lstItem);
            this.splitContainer2.Panel1.Controls.Add(this.lblChannel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.btnCancel);
            this.splitContainer2.Panel2.Controls.Add(this.btnSave);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(256, 387);
            this.splitContainer2.SplitterDistance = 264;
            this.splitContainer2.TabIndex = 1;
            // 
            // lstItem
            // 
            this.lstItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItem.FormattingEnabled = true;
            this.lstItem.ItemHeight = 12;
            this.lstItem.Location = new System.Drawing.Point(0, 21);
            this.lstItem.Name = "lstItem";
            this.lstItem.Size = new System.Drawing.Size(256, 366);
            this.lstItem.TabIndex = 0;
            this.lstItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstItem_KeyDown);
            // 
            // lblChannel
            // 
            this.lblChannel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChannel.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChannel.Location = new System.Drawing.Point(0, 0);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(256, 21);
            this.lblChannel.TabIndex = 1;
            this.lblChannel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(84, 11);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(3, 11);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Verdana", 7F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDisplayAdd,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.tsChannelLabel,
            this.tsChannel,
            this.tsItemLabel,
            this.tsItem,
            this.tsAdd,
            this.tsSeparator});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(461, 25);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            // 
            // tsDisplayAdd
            // 
            this.tsDisplayAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsChannelLabel
            // 
            this.tsChannelLabel.Name = "tsChannelLabel";
            this.tsChannelLabel.Size = new System.Drawing.Size(51, 22);
            this.tsChannelLabel.Text = "Channel";
            this.tsChannelLabel.Visible = false;
            // 
            // tsChannel
            // 
            this.tsChannel.Font = new System.Drawing.Font("Verdana", 7F);
            this.tsChannel.Name = "tsChannel";
            this.tsChannel.Size = new System.Drawing.Size(70, 25);
            this.tsChannel.Visible = false;
            // 
            // tsItemLabel
            // 
            this.tsItemLabel.Name = "tsItemLabel";
            this.tsItemLabel.Size = new System.Drawing.Size(32, 22);
            this.tsItemLabel.Text = "Item";
            this.tsItemLabel.Visible = false;
            // 
            // tsItem
            // 
            this.tsItem.Font = new System.Drawing.Font("Verdana", 7F);
            this.tsItem.Name = "tsItem";
            this.tsItem.Size = new System.Drawing.Size(100, 25);
            this.tsItem.Visible = false;
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
            // tsSeparator
            // 
            this.tsSeparator.Name = "tsSeparator";
            this.tsSeparator.Size = new System.Drawing.Size(6, 25);
            this.tsSeparator.Visible = false;
            // 
            // frmAllow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 412);
            this.Controls.Add(this.toolStripContainer1);
            this.Font = new System.Drawing.Font("Verdana", 7F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAllow";
            this.Text = "Allow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAllow_FormClosing);
            this.Load += new System.EventHandler(this.frmAllow_Load);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsDisplayAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstChannel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox lstItem;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ToolStripLabel tsChannelLabel;
        private System.Windows.Forms.ToolStripTextBox tsChannel;
        private System.Windows.Forms.ToolStripLabel tsItemLabel;
        private System.Windows.Forms.ToolStripTextBox tsItem;
        private System.Windows.Forms.ToolStripButton tsAdd;
        private System.Windows.Forms.ToolStripSeparator tsSeparator;
    }
}