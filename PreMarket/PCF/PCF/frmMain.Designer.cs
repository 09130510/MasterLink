namespace PCF
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblIP = new System.Windows.Forms.Label();
            this.chkSP = new System.Windows.Forms.CheckBox();
            this.chkFH = new System.Windows.Forms.CheckBox();
            this.chkCP = new System.Windows.Forms.CheckBox();
            this.chkCH = new System.Windows.Forms.CheckBox();
            this.chkFB = new System.Windows.Forms.CheckBox();
            this.chkYT = new System.Windows.Forms.CheckBox();
            this.tvETF = new System.Windows.Forms.TreeView();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsParse = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnJsonBrowse = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txtJsonAddress = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.webBrowser2 = new System.Windows.Forms.WebBrowser();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.lblIP);
            this.panel1.Controls.Add(this.chkSP);
            this.panel1.Controls.Add(this.chkFH);
            this.panel1.Controls.Add(this.chkCP);
            this.panel1.Controls.Add(this.chkCH);
            this.panel1.Controls.Add(this.chkFB);
            this.panel1.Controls.Add(this.chkYT);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(836, 52);
            this.panel1.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(10, 4);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIP.ForeColor = System.Drawing.Color.Purple;
            this.lblIP.Location = new System.Drawing.Point(10, 30);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(48, 14);
            this.lblIP.TabIndex = 6;
            this.lblIP.Text = "label1";
            // 
            // chkSP
            // 
            this.chkSP.AutoSize = true;
            this.chkSP.Checked = true;
            this.chkSP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSP.Location = new System.Drawing.Point(472, 27);
            this.chkSP.Name = "chkSP";
            this.chkSP.Size = new System.Drawing.Size(71, 17);
            this.chkSP.TabIndex = 5;
            this.chkSP.Text = "SinoPac";
            this.chkSP.UseVisualStyleBackColor = true;
            // 
            // chkFH
            // 
            this.chkFH.AutoSize = true;
            this.chkFH.Location = new System.Drawing.Point(410, 27);
            this.chkFH.Name = "chkFH";
            this.chkFH.Size = new System.Drawing.Size(63, 17);
            this.chkFH.TabIndex = 4;
            this.chkFH.Text = "FuHaw";
            this.chkFH.UseVisualStyleBackColor = true;
            // 
            // chkCP
            // 
            this.chkCP.AutoSize = true;
            this.chkCP.Location = new System.Drawing.Point(339, 27);
            this.chkCP.Name = "chkCP";
            this.chkCP.Size = new System.Drawing.Size(66, 17);
            this.chkCP.TabIndex = 3;
            this.chkCP.Text = "Capital";
            this.chkCP.UseVisualStyleBackColor = true;
            // 
            // chkCH
            // 
            this.chkCH.AutoSize = true;
            this.chkCH.Location = new System.Drawing.Point(472, 7);
            this.chkCH.Name = "chkCH";
            this.chkCH.Size = new System.Drawing.Size(115, 17);
            this.chkCH.TabIndex = 2;
            this.chkCH.Text = "CathayHoldings";
            this.chkCH.UseVisualStyleBackColor = true;
            // 
            // chkFB
            // 
            this.chkFB.AutoSize = true;
            this.chkFB.Location = new System.Drawing.Point(410, 7);
            this.chkFB.Name = "chkFB";
            this.chkFB.Size = new System.Drawing.Size(61, 17);
            this.chkFB.TabIndex = 1;
            this.chkFB.Text = "FuBon";
            this.chkFB.UseVisualStyleBackColor = true;
            // 
            // chkYT
            // 
            this.chkYT.AutoSize = true;
            this.chkYT.Location = new System.Drawing.Point(339, 7);
            this.chkYT.Name = "chkYT";
            this.chkYT.Size = new System.Drawing.Size(65, 17);
            this.chkYT.TabIndex = 0;
            this.chkYT.Text = "Yuanta";
            this.chkYT.UseVisualStyleBackColor = true;
            // 
            // tvETF
            // 
            this.tvETF.CheckBoxes = true;
            this.tvETF.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvETF.HideSelection = false;
            this.tvETF.Location = new System.Drawing.Point(0, 52);
            this.tvETF.Name = "tvETF";
            this.tvETF.Size = new System.Drawing.Size(243, 373);
            this.tvETF.TabIndex = 3;
            this.tvETF.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.tvETF_AfterCheck);
            this.tvETF.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvETF_AfterSelect);
            this.tvETF.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvETF_NodeMouseClick);
            // 
            // txtMsg
            // 
            this.txtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMsg.Font = new System.Drawing.Font("Verdana", 8F);
            this.txtMsg.HideSelection = false;
            this.txtMsg.Location = new System.Drawing.Point(3, 3);
            this.txtMsg.Multiline = true;
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.ReadOnly = true;
            this.txtMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMsg.Size = new System.Drawing.Size(579, 341);
            this.txtMsg.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsParse});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // tsParse
            // 
            this.tsParse.Name = "tsParse";
            this.tsParse.Size = new System.Drawing.Size(124, 22);
            this.tsParse.Text = "重抓一次";
            this.tsParse.Click += new System.EventHandler(this.tsParse_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(243, 52);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(593, 373);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtMsg);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(585, 347);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Message";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.webBrowser1);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(585, 347);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "JSON Viewer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 32);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(579, 312);
            this.webBrowser1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnJsonBrowse);
            this.panel2.Controls.Add(this.txtJsonAddress);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(579, 29);
            this.panel2.TabIndex = 1;
            // 
            // btnJsonBrowse
            // 
            this.btnJsonBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnJsonBrowse.ImageIndex = 6;
            this.btnJsonBrowse.ImageList = this.imageList1;
            this.btnJsonBrowse.Location = new System.Drawing.Point(544, 0);
            this.btnJsonBrowse.Name = "btnJsonBrowse";
            this.btnJsonBrowse.Size = new System.Drawing.Size(32, 29);
            this.btnJsonBrowse.TabIndex = 1;
            this.btnJsonBrowse.UseVisualStyleBackColor = true;
            this.btnJsonBrowse.Click += new System.EventHandler(this.btnJsonBrowse_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "browse.png");
            this.imageList1.Images.SetKeyName(1, "1472726523_Browser_G.ico");
            this.imageList1.Images.SetKeyName(2, "1472726537_icon_344.ico");
            this.imageList1.Images.SetKeyName(3, "1472726545_5348_-_Safari.ico");
            this.imageList1.Images.SetKeyName(4, "url.png");
            this.imageList1.Images.SetKeyName(5, "url2.png");
            this.imageList1.Images.SetKeyName(6, "url3.png");
            // 
            // txtJsonAddress
            // 
            this.txtJsonAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtJsonAddress.Location = new System.Drawing.Point(3, 4);
            this.txtJsonAddress.Name = "txtJsonAddress";
            this.txtJsonAddress.Size = new System.Drawing.Size(535, 20);
            this.txtJsonAddress.TabIndex = 0;
            this.txtJsonAddress.Text = "http://";
            this.txtJsonAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtJsonAddress_KeyUp);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.webBrowser2);
            this.tabPage3.Controls.Add(this.panel3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(585, 347);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Browser";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // webBrowser2
            // 
            this.webBrowser2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser2.Location = new System.Drawing.Point(3, 32);
            this.webBrowser2.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser2.Name = "webBrowser2";
            this.webBrowser2.Size = new System.Drawing.Size(579, 312);
            this.webBrowser2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnBrowser);
            this.panel3.Controls.Add(this.txtAddress);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(579, 29);
            this.panel3.TabIndex = 3;
            // 
            // btnBrowser
            // 
            this.btnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowser.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnBrowser.ImageIndex = 2;
            this.btnBrowser.ImageList = this.imageList1;
            this.btnBrowser.Location = new System.Drawing.Point(544, 0);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(32, 29);
            this.btnBrowser.TabIndex = 1;
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.Location = new System.Drawing.Point(3, 4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(535, 20);
            this.txtAddress.TabIndex = 0;
            this.txtAddress.Text = "http://";
            this.txtAddress.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyUp);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 425);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tvETF);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "PCFGetter";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkFH;
        private System.Windows.Forms.CheckBox chkCP;
        private System.Windows.Forms.CheckBox chkCH;
        private System.Windows.Forms.CheckBox chkFB;
        private System.Windows.Forms.CheckBox chkYT;
        private System.Windows.Forms.CheckBox chkSP;
        private System.Windows.Forms.TreeView tvETF;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsParse;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnJsonBrowse;
        private System.Windows.Forms.TextBox txtJsonAddress;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.WebBrowser webBrowser2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnBrowser;
        private System.Windows.Forms.TextBox txtAddress;
    }
}

