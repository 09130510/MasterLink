namespace NAVWatcher
{
    partial class SubscriptionBox
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
            Util.RedisLib.UnsubscribeChannel(Channel, Item);
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValue = new System.Windows.Forms.RichTextBox();
            this.btnSub = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Channel";
            // 
            // txtChannel
            // 
            this.txtChannel.Location = new System.Drawing.Point(54, 3);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(72, 20);
            this.txtChannel.TabIndex = 1;
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(160, 3);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(72, 20);
            this.txtItem.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Item";
            // 
            // txtValue
            // 
            this.txtValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtValue.Location = new System.Drawing.Point(0, 26);
            this.txtValue.Name = "txtValue";
            this.txtValue.ReadOnly = true;
            this.txtValue.Size = new System.Drawing.Size(267, 33);
            this.txtValue.TabIndex = 4;
            this.txtValue.Text = "";
            // 
            // btnSub
            // 
            this.btnSub.Location = new System.Drawing.Point(232, 1);
            this.btnSub.Name = "btnSub";
            this.btnSub.Size = new System.Drawing.Size(32, 23);
            this.btnSub.TabIndex = 5;
            this.btnSub.Text = "Go";
            this.btnSub.UseVisualStyleBackColor = true;
            this.btnSub.Click += new System.EventHandler(this.btnSub_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnSub);
            this.panel1.Controls.Add(this.txtChannel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtItem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 26);
            this.panel1.TabIndex = 6;
            // 
            // SubscriptionBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.Name = "SubscriptionBox";
            this.Size = new System.Drawing.Size(267, 59);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtChannel;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtValue;
        private System.Windows.Forms.Button btnSub;
        private System.Windows.Forms.Panel panel1;
    }
}
