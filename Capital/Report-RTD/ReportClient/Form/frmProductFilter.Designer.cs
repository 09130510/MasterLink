﻿namespace Capital.Report
{
    partial class frmProductFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProductFilter));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.tvAll = new System.Windows.Forms.TreeView();
            this.btnReload = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.lstFilter = new System.Windows.Forms.ListBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.txtExch = new System.Windows.Forms.MaskedTextBox();
            this.txtContract = new System.Windows.Forms.MaskedTextBox();
            this.btnAddContract = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnAdd, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnRemove, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnUp, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnDown, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.tvAll, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnReload, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lstFilter, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(575, 275);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAdd.Enabled = false;
            this.btnAdd.ImageIndex = 1;
            this.btnAdd.ImageList = this.imageList1;
            this.btnAdd.Location = new System.Drawing.Point(264, 54);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(38, 38);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "1468479705_Arrow_Direction_Move_Back_Left_Previous.ico");
            this.imageList1.Images.SetKeyName(1, "1468479713_Arrow_Direction_Move_Next_Forward_Right.ico");
            this.imageList1.Images.SetKeyName(2, "1468479719_Arrow_Direction_Move_Down_Download_Decrease.ico");
            this.imageList1.Images.SetKeyName(3, "1468479728_Arrow_Direction_Move_Up_Increase_Upload.ico");
            // 
            // btnRemove
            // 
            this.btnRemove.AutoSize = true;
            this.btnRemove.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRemove.Enabled = false;
            this.btnRemove.ImageIndex = 0;
            this.btnRemove.ImageList = this.imageList1;
            this.btnRemove.Location = new System.Drawing.Point(264, 104);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(38, 38);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnUp
            // 
            this.btnUp.AutoSize = true;
            this.btnUp.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUp.Enabled = false;
            this.btnUp.ImageIndex = 3;
            this.btnUp.ImageList = this.imageList1;
            this.btnUp.Location = new System.Drawing.Point(264, 154);
            this.btnUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(38, 38);
            this.btnUp.TabIndex = 3;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.AutoSize = true;
            this.btnDown.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnDown.Enabled = false;
            this.btnDown.ImageIndex = 2;
            this.btnDown.ImageList = this.imageList1;
            this.btnDown.Location = new System.Drawing.Point(264, 204);
            this.btnDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(38, 38);
            this.btnDown.TabIndex = 4;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // tvAll
            // 
            this.tvAll.CheckBoxes = true;
            this.tvAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvAll.Font = new System.Drawing.Font("Courier New", 9F);
            this.tvAll.Indent = 23;
            this.tvAll.Location = new System.Drawing.Point(3, 4);
            this.tvAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tvAll.Name = "tvAll";
            this.tableLayoutPanel1.SetRowSpan(this.tvAll, 5);
            this.tvAll.Size = new System.Drawing.Size(255, 267);
            this.tvAll.TabIndex = 6;
            // 
            // btnReload
            // 
            this.btnReload.AutoSize = true;
            this.btnReload.ImageIndex = 0;
            this.btnReload.ImageList = this.imageList2;
            this.btnReload.Location = new System.Drawing.Point(264, 4);
            this.btnReload.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(44, 41);
            this.btnReload.TabIndex = 0;
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "1468482660_refresh.ico");
            this.imageList2.Images.SetKeyName(1, "1468906387_add_list.ico");
            // 
            // lstFilter
            // 
            this.lstFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstFilter.FormattingEnabled = true;
            this.lstFilter.Location = new System.Drawing.Point(316, 54);
            this.lstFilter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lstFilter.Name = "lstFilter";
            this.tableLayoutPanel1.SetRowSpan(this.lstFilter, 4);
            this.lstFilter.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstFilter.Size = new System.Drawing.Size(256, 217);
            this.lstFilter.TabIndex = 7;
            this.lstFilter.Tag = "ORDERSETTING;FILTER;Items";
            this.lstFilter.SelectedIndexChanged += new System.EventHandler(this.lstFilter_SelectedIndexChanged);
            this.lstFilter.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.lstFilter_PreviewKeyDown);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.txtExch);
            this.flowLayoutPanel1.Controls.Add(this.txtContract);
            this.flowLayoutPanel1.Controls.Add(this.btnAddContract);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(316, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(256, 44);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // txtExch
            // 
            this.txtExch.AsciiOnly = true;
            this.txtExch.Location = new System.Drawing.Point(3, 11);
            this.txtExch.Margin = new System.Windows.Forms.Padding(3, 11, 0, 0);
            this.txtExch.Name = "txtExch";
            this.txtExch.PromptChar = '-';
            this.txtExch.Size = new System.Drawing.Size(76, 20);
            this.txtExch.TabIndex = 0;
            this.txtExch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtExch.TextChanged += new System.EventHandler(this.txtExch_TextChanged);
            // 
            // txtContract
            // 
            this.txtContract.AsciiOnly = true;
            this.txtContract.Location = new System.Drawing.Point(79, 11);
            this.txtContract.Margin = new System.Windows.Forms.Padding(0, 11, 0, 0);
            this.txtContract.Name = "txtContract";
            this.txtContract.PromptChar = '-';
            this.txtContract.Size = new System.Drawing.Size(76, 20);
            this.txtContract.TabIndex = 1;
            this.txtContract.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtContract.TextChanged += new System.EventHandler(this.txtExch_TextChanged);
            // 
            // btnAddContract
            // 
            this.btnAddContract.Enabled = false;
            this.btnAddContract.ImageIndex = 1;
            this.btnAddContract.ImageList = this.imageList2;
            this.btnAddContract.Location = new System.Drawing.Point(158, 5);
            this.btnAddContract.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.btnAddContract.Name = "btnAddContract";
            this.btnAddContract.Size = new System.Drawing.Size(33, 30);
            this.btnAddContract.TabIndex = 3;
            this.btnAddContract.UseVisualStyleBackColor = true;
            this.btnAddContract.Click += new System.EventHandler(this.btnAddContract_Click);
            // 
            // frmProductFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 275);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.KeyPreview = true;
            this.Name = "frmProductFilter";
            this.Text = "商品挑選";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmProductFilter_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.TreeView tvAll;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ListBox lstFilter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.MaskedTextBox txtExch;
        private System.Windows.Forms.MaskedTextBox txtContract;
        private System.Windows.Forms.Button btnAddContract;
    }
}