namespace ComponentTest
{
	partial class WeakTest
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
			this.grid1 = new SourceGrid.Grid();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// grid1
			// 
			this.grid1.EnableSort = true;
			this.grid1.Location = new System.Drawing.Point(81, 39);
			this.grid1.Name = "grid1";
			this.grid1.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
			this.grid1.SelectionMode = SourceGrid.GridSelectionMode.Cell;
			this.grid1.Size = new System.Drawing.Size(200, 100);
			this.grid1.TabIndex = 20;
			this.grid1.TabStop = true;
			this.grid1.ToolTipText = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(303, 92);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(57, 21);
			this.button1.TabIndex = 21;
			this.button1.Text = "Dispose";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(303, 66);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(57, 21);
			this.button2.TabIndex = 22;
			this.button2.Text = "Add";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(294, 146);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(57, 21);
			this.button3.TabIndex = 23;
			this.button3.Text = "Dispose";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// WeakTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(363, 179);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.grid1);
			this.Name = "WeakTest";
			this.Text = "Form2";
			this.ResumeLayout(false);

		}

		#endregion

		private SourceGrid.Grid grid1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
	}
}