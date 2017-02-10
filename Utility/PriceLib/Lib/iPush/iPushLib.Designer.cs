namespace PriceLib.iPush
{
    partial class iPushLib
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(iPushLib));
            this.axXQuote = new AxXQUOTELib.AxXQuote();
            ((System.ComponentModel.ISupportInitialize)(this.axXQuote)).BeginInit();
            this.SuspendLayout();
            // 
            // axXQuote
            // 
            this.axXQuote.Enabled = true;
            this.axXQuote.Location = new System.Drawing.Point(33, 15);
            this.axXQuote.Name = "axXQuote";
            this.axXQuote.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axXQuote.OcxState")));
            this.axXQuote.Size = new System.Drawing.Size(100, 50);
            this.axXQuote.TabIndex = 0;
            // 
            // iPushLib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axXQuote);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "iPushLib";
            this.Size = new System.Drawing.Size(124, 69);
            ((System.ComponentModel.ISupportInitialize)(this.axXQuote)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxXQUOTELib.AxXQuote axXQuote;
    }
}
