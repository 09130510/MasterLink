

namespace PriceCalculator
{
    partial class frmNAVDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNAVDetail));
            this.tvJson = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtJson = new System.Windows.Forms.RichTextBox();
            this.cmYstPrice = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsYstPrice = new System.Windows.Forms.ToolStripTextBox();
            this.cmFundAssetValue = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsFundAssetValue = new System.Windows.Forms.ToolStripTextBox();
            this.cmMktPrice = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMktPrice = new System.Windows.Forms.ToolStripTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.cmYstPrice.SuspendLayout();
            this.cmFundAssetValue.SuspendLayout();
            this.cmMktPrice.SuspendLayout();
            this.SuspendLayout();
            // 
            // tvJson
            // 
            this.tvJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvJson.Location = new System.Drawing.Point(3, 3);
            this.tvJson.Name = "tvJson";
            this.tvJson.Size = new System.Drawing.Size(449, 256);
            this.tvJson.TabIndex = 0;
            this.tvJson.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvJson_NodeMouseClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(463, 288);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tvJson);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(455, 262);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tree";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtJson);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(455, 262);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Text";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtJson
            // 
            this.txtJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtJson.Location = new System.Drawing.Point(3, 3);
            this.txtJson.Name = "txtJson";
            this.txtJson.Size = new System.Drawing.Size(449, 256);
            this.txtJson.TabIndex = 0;
            this.txtJson.Text = "";
            // 
            // cmYstPrice
            // 
            this.cmYstPrice.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmYstPrice.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsYstPrice});
            this.cmYstPrice.Name = "cmYstPrice";
            this.cmYstPrice.Size = new System.Drawing.Size(141, 26);
            // 
            // tsYstPrice
            // 
            this.tsYstPrice.AcceptsTab = true;
            this.tsYstPrice.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsYstPrice.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsYstPrice.Name = "tsYstPrice";
            this.tsYstPrice.Size = new System.Drawing.Size(80, 20);
            this.tsYstPrice.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tsYstPrice.ToolTipText = "不會觸發重新計算";
            this.tsYstPrice.TextChanged += new System.EventHandler(this.tsYstPrice_TextChanged);
            // 
            // cmFundAssetValue
            // 
            this.cmFundAssetValue.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmFundAssetValue.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFundAssetValue});
            this.cmFundAssetValue.Name = "cmYstPrice";
            this.cmFundAssetValue.Size = new System.Drawing.Size(141, 26);
            // 
            // tsFundAssetValue
            // 
            this.tsFundAssetValue.AcceptsTab = true;
            this.tsFundAssetValue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsFundAssetValue.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsFundAssetValue.Name = "tsFundAssetValue";
            this.tsFundAssetValue.Size = new System.Drawing.Size(80, 20);
            this.tsFundAssetValue.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tsFundAssetValue.ToolTipText = "不會觸發重新計算";
            this.tsFundAssetValue.TextChanged += new System.EventHandler(this.tsPublicShares_TextChanged);
            // 
            // cmMktPrice
            // 
            this.cmMktPrice.Font = new System.Drawing.Font("Verdana", 8F);
            this.cmMktPrice.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMktPrice});
            this.cmMktPrice.Name = "cmYstPrice";
            this.cmMktPrice.Size = new System.Drawing.Size(141, 26);
            // 
            // tsMktPrice
            // 
            this.tsMktPrice.AcceptsTab = true;
            this.tsMktPrice.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsMktPrice.Font = new System.Drawing.Font("Verdana", 8F);
            this.tsMktPrice.Name = "tsMktPrice";
            this.tsMktPrice.Size = new System.Drawing.Size(80, 20);
            this.tsMktPrice.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tsMktPrice.ToolTipText = "不會觸發重新計算";
            this.tsMktPrice.TextChanged += new System.EventHandler(this.tsMktPrice_TextChanged);
            // 
            // frmNAVDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 288);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmNAVDetail";
            this.Text = "NAV Detail";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.cmYstPrice.ResumeLayout(false);
            this.cmYstPrice.PerformLayout();
            this.cmFundAssetValue.ResumeLayout(false);
            this.cmFundAssetValue.PerformLayout();
            this.cmMktPrice.ResumeLayout(false);
            this.cmMktPrice.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvJson;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox txtJson;
        private System.Windows.Forms.ContextMenuStrip cmFundAssetValue;
        private System.Windows.Forms.ContextMenuStrip cmMktPrice;
        private System.Windows.Forms.ContextMenuStrip cmYstPrice;
        private System.Windows.Forms.ToolStripTextBox tsFundAssetValue;
        private System.Windows.Forms.ToolStripTextBox tsMktPrice;
        private System.Windows.Forms.ToolStripTextBox tsYstPrice;


    }
}