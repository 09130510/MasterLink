using System.Drawing;
using System.Windows.Forms;

namespace PriceCalculator
{
    partial class frmLockYP
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.dtpExpired = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(222, 43);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(50, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(109, 5);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(47, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(172, 43);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(50, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Lock";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // dtpExpired
            // 
            this.dtpExpired.Format = DateTimePickerFormat.Short;
            this.dtpExpired.Location = new System.Drawing.Point(53, 44);
            this.dtpExpired.Name = "dtpExpired";
            this.dtpExpired.Size = new System.Drawing.Size(119, 20);
            this.dtpExpired.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Expired";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "Locked";
            // 
            // lblCount
            // 
            this.lblCount.BorderStyle = BorderStyle.Fixed3D;
            this.lblCount.ForeColor = Color.Crimson;
            this.lblCount.Location = new System.Drawing.Point(51, 6);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(58, 20);
            this.lblCount.TabIndex = 7;
            this.lblCount.TextAlign = ContentAlignment.MiddleRight;
            // 
            // frmLockYP
            // 
            this.AcceptButton = btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 70);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpExpired);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnCancel);
            this.Font = new Font("Verdana", 7.5f);
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.Name = "frmLockYP";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Lock Yesterday Price";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.DateTimePicker dtpExpired;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblCount;
    }
}