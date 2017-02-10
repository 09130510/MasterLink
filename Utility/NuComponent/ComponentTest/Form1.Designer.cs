namespace ComponentTest
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
		/// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
		/// 修改這個方法的內容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button9 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton4 = new System.Windows.Forms.RadioButton();
			this.radioButton5 = new System.Windows.Forms.RadioButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.grid1 = new SourceGrid.Grid();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(149, 32);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(99, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Msg_OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(149, 61);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(99, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Msg_OKCancel";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(149, 90);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(99, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "Multi_OK";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(149, 119);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(99, 23);
			this.button4.TabIndex = 3;
			this.button4.Text = "Multi_OKCancel";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Font = new System.Drawing.Font("Verdana", 7F);
			this.button5.Location = new System.Drawing.Point(64, 175);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(28, 28);
			this.button5.TabIndex = 4;
			this.button5.Text = "LS";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
			this.button5.MouseLeave += new System.EventHandler(this.button9_MouseLeave);
			// 
			// button6
			// 
			this.button6.Font = new System.Drawing.Font("Verdana", 7F);
			this.button6.Location = new System.Drawing.Point(92, 175);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(28, 28);
			this.button6.TabIndex = 5;
			this.button6.Text = "SL";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
			this.button6.MouseLeave += new System.EventHandler(this.button9_MouseLeave);
			// 
			// button7
			// 
			this.button7.Font = new System.Drawing.Font("Verdana", 7F);
			this.button7.Location = new System.Drawing.Point(64, 203);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(28, 28);
			this.button7.TabIndex = 6;
			this.button7.Text = "LL";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
			this.button7.MouseLeave += new System.EventHandler(this.button9_MouseLeave);
			// 
			// button8
			// 
			this.button8.Font = new System.Drawing.Font("Verdana", 7F);
			this.button8.Location = new System.Drawing.Point(92, 203);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(28, 28);
			this.button8.TabIndex = 7;
			this.button8.Text = "SS";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
			this.button8.MouseLeave += new System.EventHandler(this.button9_MouseLeave);
			// 
			// button9
			// 
			this.button9.Font = new System.Drawing.Font("Verdana", 7F);
			this.button9.Location = new System.Drawing.Point(78, 189);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(28, 28);
			this.button9.TabIndex = 8;
			this.button9.Text = "N";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
			this.button9.MouseLeave += new System.EventHandler(this.button9_MouseLeave);
			// 
			// checkBox1
			// 
			this.checkBox1.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox1.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.checkBox1.Location = new System.Drawing.Point(147, 175);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(42, 22);
			this.checkBox1.TabIndex = 9;
			this.checkBox1.Text = "+C-P";
			this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.MouseEnter += new System.EventHandler(this.checkBox5_MouseEnter);
			this.checkBox1.MouseLeave += new System.EventHandler(this.checkBox5_MouseLeave);
			// 
			// checkBox2
			// 
			this.checkBox2.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox2.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.checkBox2.Location = new System.Drawing.Point(189, 175);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(42, 22);
			this.checkBox2.TabIndex = 10;
			this.checkBox2.Text = "-C+P";
			this.checkBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.MouseEnter += new System.EventHandler(this.checkBox5_MouseEnter);
			this.checkBox2.MouseLeave += new System.EventHandler(this.checkBox5_MouseLeave);
			// 
			// checkBox3
			// 
			this.checkBox3.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox3.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.checkBox3.Location = new System.Drawing.Point(147, 212);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(42, 22);
			this.checkBox3.TabIndex = 11;
			this.checkBox3.Text = "+C+P";
			this.checkBox3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox3.UseVisualStyleBackColor = true;
			this.checkBox3.MouseEnter += new System.EventHandler(this.checkBox5_MouseEnter);
			this.checkBox3.MouseLeave += new System.EventHandler(this.checkBox5_MouseLeave);
			// 
			// checkBox4
			// 
			this.checkBox4.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox4.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.checkBox4.Location = new System.Drawing.Point(189, 212);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(42, 22);
			this.checkBox4.TabIndex = 12;
			this.checkBox4.Text = "-C-P";
			this.checkBox4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox4.UseVisualStyleBackColor = true;
			this.checkBox4.MouseEnter += new System.EventHandler(this.checkBox5_MouseEnter);
			this.checkBox4.MouseLeave += new System.EventHandler(this.checkBox5_MouseLeave);
			// 
			// checkBox5
			// 
			this.checkBox5.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBox5.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.checkBox5.Location = new System.Drawing.Point(168, 194);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(42, 22);
			this.checkBox5.TabIndex = 13;
			this.checkBox5.Text = "N";
			this.checkBox5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBox5.UseVisualStyleBackColor = true;
			this.checkBox5.MouseEnter += new System.EventHandler(this.checkBox5_MouseEnter);
			this.checkBox5.MouseLeave += new System.EventHandler(this.checkBox5_MouseLeave);
			// 
			// radioButton1
			// 
			this.radioButton1.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton1.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.radioButton1.Location = new System.Drawing.Point(246, 189);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(42, 22);
			this.radioButton1.TabIndex = 14;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "+C-P";
			this.radioButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip1.SetToolTip(this.radioButton1, "[+]Call [-]Put");
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
			this.radioButton1.MouseEnter += new System.EventHandler(this.radioButton5_MouseEnter);
			this.radioButton1.MouseLeave += new System.EventHandler(this.radioButton5_MouseLeave);
			// 
			// radioButton2
			// 
			this.radioButton2.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton2.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.radioButton2.Location = new System.Drawing.Point(288, 188);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(42, 22);
			this.radioButton2.TabIndex = 15;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "-C+P";
			this.radioButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip1.SetToolTip(this.radioButton2, "[-]Call [+]Put");
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
			this.radioButton2.MouseEnter += new System.EventHandler(this.radioButton5_MouseEnter);
			this.radioButton2.MouseLeave += new System.EventHandler(this.radioButton5_MouseLeave);
			// 
			// radioButton3
			// 
			this.radioButton3.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton3.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.radioButton3.Location = new System.Drawing.Point(246, 211);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(42, 22);
			this.radioButton3.TabIndex = 16;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "+C+P";
			this.radioButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip1.SetToolTip(this.radioButton3, "[+]Call [+]Put");
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
			this.radioButton3.MouseEnter += new System.EventHandler(this.radioButton5_MouseEnter);
			this.radioButton3.MouseLeave += new System.EventHandler(this.radioButton5_MouseLeave);
			// 
			// radioButton4
			// 
			this.radioButton4.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton4.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.radioButton4.Location = new System.Drawing.Point(288, 211);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(42, 22);
			this.radioButton4.TabIndex = 17;
			this.radioButton4.TabStop = true;
			this.radioButton4.Text = "-C-P";
			this.radioButton4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip1.SetToolTip(this.radioButton4, "[-]Call [-]Put");
			this.radioButton4.UseVisualStyleBackColor = true;
			this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
			this.radioButton4.MouseEnter += new System.EventHandler(this.radioButton5_MouseEnter);
			this.radioButton4.MouseLeave += new System.EventHandler(this.radioButton5_MouseLeave);
			// 
			// radioButton5
			// 
			this.radioButton5.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButton5.Font = new System.Drawing.Font("Verdana", 6.5F);
			this.radioButton5.Location = new System.Drawing.Point(267, 167);
			this.radioButton5.Name = "radioButton5";
			this.radioButton5.Size = new System.Drawing.Size(42, 22);
			this.radioButton5.TabIndex = 18;
			this.radioButton5.TabStop = true;
			this.radioButton5.Text = "N";
			this.radioButton5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip1.SetToolTip(this.radioButton5, "Normal");
			this.radioButton5.UseVisualStyleBackColor = true;
			this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
			this.radioButton5.MouseEnter += new System.EventHandler(this.radioButton5_MouseEnter);
			this.radioButton5.MouseLeave += new System.EventHandler(this.radioButton5_MouseLeave);
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 100;
			this.toolTip1.AutoPopDelay = 10000;
			this.toolTip1.InitialDelay = 100;
			this.toolTip1.ReshowDelay = 20;
			this.toolTip1.ShowAlways = true;
			// 
			// grid1
			// 
			this.grid1.EnableSort = true;
			this.grid1.Location = new System.Drawing.Point(468, 13);
			this.grid1.Name = "grid1";
			this.grid1.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
			this.grid1.SelectionMode = SourceGrid.GridSelectionMode.Cell;
			this.grid1.Size = new System.Drawing.Size(200, 100);
			this.grid1.TabIndex = 19;
			this.grid1.TabStop = true;
			this.grid1.ToolTipText = "";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(730, 273);
			this.Controls.Add(this.grid1);
			this.Controls.Add(this.radioButton5);
			this.Controls.Add(this.radioButton4);
			this.Controls.Add(this.radioButton3);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.checkBox5);
			this.Controls.Add(this.button9);
			this.Controls.Add(this.button8);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.checkBox4);
			this.Controls.Add(this.checkBox3);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton4;
		private System.Windows.Forms.RadioButton radioButton5;
		private System.Windows.Forms.ToolTip toolTip1;
		private SourceGrid.Grid grid1;
	}
}

