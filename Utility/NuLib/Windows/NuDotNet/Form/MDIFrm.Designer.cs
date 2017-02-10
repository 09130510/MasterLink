namespace NuDotNet.CustomUIControls
{
    partial class MDIFrm
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該公開 Managed 資源則為 true，否則為 false。</param>
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.tabIndexMenu = new System.Windows.Forms.TabControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // tabIndexMenu
            // 
            this.tabIndexMenu.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabIndexMenu.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabIndexMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabIndexMenu.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabIndexMenu.Location = new System.Drawing.Point(0, 24);
            this.tabIndexMenu.Multiline = true;
            this.tabIndexMenu.Name = "tabIndexMenu";
            this.tabIndexMenu.SelectedIndex = 0;
            this.tabIndexMenu.Size = new System.Drawing.Size(981, 28);
            this.tabIndexMenu.TabIndex = 1;
            this.tabIndexMenu.Visible = false;
            this.tabIndexMenu.SelectedIndexChanged += new System.EventHandler(this.tabIndexMenu_SelectedIndexChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(981, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MDIFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 657);
            this.Controls.Add(this.tabIndexMenu);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MDIFrm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabIndexMenu;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}

