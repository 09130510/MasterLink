namespace GQuoteTest
{
    partial class Form1
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
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.axGQuoteOpr1 = new AxGQUOTEOPRLib.AxGQuoteOpr();
            ((System.ComponentModel.ISupportInitialize)(this.axGQuoteOpr1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(66, 22);
            this.button1.TabIndex = 2;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(240, 473);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(66, 28);
            this.button2.TabIndex = 3;
            this.button2.Text = "Query";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 85);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(256, 204);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 295);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(222, 22);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "2";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 478);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(222, 22);
            this.textBox2.TabIndex = 6;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(12, 329);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(329, 138);
            this.richTextBox2.TabIndex = 7;
            this.richTextBox2.Text = "欲查詢的關鍵字，可以空白隔開數個『交集』條件，例如 \nTXO 200809 表示查詢滿足商品資訊中有TXO且有200809兩個字串的所有商品。\nServer由商品" +
    "ID與商品名稱中進行搜尋。\n\n保留字：有三個保留字STK、FUT、OPT，\n保留字需放在第一個搜尋條件且不能單獨使用，例如\n“STK 2303” 表示搜尋所有股" +
    "票滿足2303字串的商品。";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(240, 295);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(52, 22);
            this.button3.TabIndex = 8;
            this.button3.Text = "Sub";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(298, 295);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(52, 22);
            this.button4.TabIndex = 9;
            this.button4.Text = "Unsub";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // axGQuoteOpr1
            // 
            this.axGQuoteOpr1.Enabled = true;
            this.axGQuoteOpr1.Location = new System.Drawing.Point(93, 23);
            this.axGQuoteOpr1.Name = "axGQuoteOpr1";
            this.axGQuoteOpr1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axGQuoteOpr1.OcxState")));
            this.axGQuoteOpr1.Size = new System.Drawing.Size(55, 56);
            this.axGQuoteOpr1.TabIndex = 10;
            this.axGQuoteOpr1.WriteLog += new AxGQUOTEOPRLib._DGQuoteOprEvents_WriteLogEventHandler(this.axGQuoteOpr1_WriteLog);
            this.axGQuoteOpr1.ConnectReady += new System.EventHandler(this.axGQuoteOpr1_ConnectReady);
            this.axGQuoteOpr1.ConnectLost += new System.EventHandler(this.axGQuoteOpr1_ConnectReady);
            this.axGQuoteOpr1.ConnectFail += new AxGQUOTEOPRLib._DGQuoteOprEvents_ConnectFailEventHandler(this.axGQuoteOpr2_ConnectFail);
            this.axGQuoteOpr1.Data += new AxGQUOTEOPRLib._DGQuoteOprEvents_DataEventHandler(this.axGQuoteOpr1_Data);
            this.axGQuoteOpr1.Kick += new AxGQUOTEOPRLib._DGQuoteOprEvents_KickEventHandler(this.axGQuoteOpr1_Kick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 531);
            this.Controls.Add(this.axGQuoteOpr1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.axGQuoteOpr1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private AxGQUOTEOPRLib.AxGQuoteOpr axGQuoteOpr1;
    }
}

