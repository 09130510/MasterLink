namespace PriceCalculator
{
    partial class ETFBox
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

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ETFBox));
            this.tvETF = new System.Windows.Forms.TreeView();
            this.cmMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsNAVDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLockYP = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cboClassification = new System.Windows.Forms.ComboBox();
            this.cmMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvETF
            // 
            this.tvETF.CheckBoxes = true;
            this.tvETF.ContextMenuStrip = this.cmMenu;
            this.tvETF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvETF.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvETF.HideSelection = false;
            this.tvETF.ImageIndex = 0;
            this.tvETF.ImageList = this.imageList1;
            this.tvETF.Indent = 19;
            this.tvETF.Location = new System.Drawing.Point(0, 21);
            this.tvETF.Name = "tvETF";
            this.tvETF.SelectedImageIndex = 0;
            this.tvETF.Size = new System.Drawing.Size(175, 142);
            this.tvETF.TabIndex = 2;
            this.tvETF.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvETF_AfterCheck);
            this.tvETF.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvETF_AfterSelect);
            this.tvETF.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvETF_NodeMouseClick);
            // 
            // cmMenu
            // 
            this.cmMenu.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNAVDetail,
            this.tsLockYP});
            this.cmMenu.Name = "cmMenu";
            this.cmMenu.Size = new System.Drawing.Size(159, 48);
            // 
            // tsNAVDetail
            // 
            this.tsNAVDetail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsNAVDetail.Name = "tsNAVDetail";
            this.tsNAVDetail.Size = new System.Drawing.Size(158, 22);
            this.tsNAVDetail.Text = "NAV Detail";
            this.tsNAVDetail.Click += new System.EventHandler(this.tsNAVDetail_Click);
            // 
            // tsLockYP
            // 
            this.tsLockYP.Name = "tsLockYP";
            this.tsLockYP.Size = new System.Drawing.Size(158, 22);
            this.tsLockYP.Text = "Lock PCF Price";
            this.tsLockYP.Click += new System.EventHandler(this.tsLockYP_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "empty.png");
            this.imageList1.Images.SetKeyName(1, "lock.ico");
            this.imageList1.Images.SetKeyName(2, "lock_gray2.ico");
            // 
            // cboClassification
            // 
            this.cboClassification.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboClassification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClassification.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboClassification.FormattingEnabled = true;
            this.cboClassification.Items.AddRange(new object[] {
            "None",
            "Broker",
            "Market"});
            this.cboClassification.Location = new System.Drawing.Point(0, 0);
            this.cboClassification.Name = "cboClassification";
            this.cboClassification.Size = new System.Drawing.Size(175, 21);
            this.cboClassification.TabIndex = 3;
            this.cboClassification.SelectedIndexChanged += new System.EventHandler(this.cboClassification_SelectedIndexChanged);
            // 
            // ETFBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvETF);
            this.Controls.Add(this.cboClassification);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Name = "ETFBox";
            this.Size = new System.Drawing.Size(175, 163);
            this.cmMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvETF;
        private System.Windows.Forms.ComboBox cboClassification;
        private System.Windows.Forms.ContextMenuStrip cmMenu;
        private System.Windows.Forms.ToolStripMenuItem tsNAVDetail;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem tsLockYP;
    }
}
