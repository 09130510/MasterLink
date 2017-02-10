namespace PriceCalculator
{
    partial class frmConnection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConnection));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radNone = new System.Windows.Forms.RadioButton();
            this.radiPush = new System.Windows.Forms.RadioButton();
            this.radRedis = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkCapital = new System.Windows.Forms.CheckBox();
            this.chkPATS = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSQL = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.radNone);
            this.groupBox1.Controls.Add(this.radiPush);
            this.groupBox1.Controls.Add(this.radRedis);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(68, 105);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Internal Stk / Fut ";
            // 
            // radNone
            // 
            this.radNone.AutoSize = true;
            this.radNone.Location = new System.Drawing.Point(6, 23);
            this.radNone.Name = "radNone";
            this.radNone.Size = new System.Drawing.Size(54, 17);
            this.radNone.TabIndex = 2;
            this.radNone.TabStop = true;
            this.radNone.Tag = "None";
            this.radNone.Text = "None";
            this.radNone.UseVisualStyleBackColor = true;
            this.radNone.CheckedChanged += new System.EventHandler(this.radRedis_CheckedChanged);
            // 
            // radiPush
            // 
            this.radiPush.AutoSize = true;
            this.radiPush.Location = new System.Drawing.Point(6, 69);
            this.radiPush.Name = "radiPush";
            this.radiPush.Size = new System.Drawing.Size(55, 17);
            this.radiPush.TabIndex = 1;
            this.radiPush.TabStop = true;
            this.radiPush.Tag = "iPush";
            this.radiPush.Text = "iPush";
            this.radiPush.UseVisualStyleBackColor = true;
            this.radiPush.CheckedChanged += new System.EventHandler(this.radRedis_CheckedChanged);
            // 
            // radRedis
            // 
            this.radRedis.AutoSize = true;
            this.radRedis.Location = new System.Drawing.Point(6, 46);
            this.radRedis.Name = "radRedis";
            this.radRedis.Size = new System.Drawing.Size(56, 17);
            this.radRedis.TabIndex = 0;
            this.radRedis.TabStop = true;
            this.radRedis.Tag = "Redis";
            this.radRedis.Text = "Redis";
            this.radRedis.UseVisualStyleBackColor = true;
            this.radRedis.CheckedChanged += new System.EventHandler(this.radRedis_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.chkCapital);
            this.groupBox2.Controls.Add(this.chkPATS);
            this.groupBox2.Location = new System.Drawing.Point(77, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(75, 81);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Foreign Fut ";
            // 
            // chkCapital
            // 
            this.chkCapital.AutoSize = true;
            this.chkCapital.Checked = true;
            this.chkCapital.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCapital.Enabled = false;
            this.chkCapital.Font = new System.Drawing.Font("Verdana", 7F);
            this.chkCapital.Location = new System.Drawing.Point(6, 23);
            this.chkCapital.Name = "chkCapital";
            this.chkCapital.Size = new System.Drawing.Size(63, 16);
            this.chkCapital.TabIndex = 9;
            this.chkCapital.Text = "Capital";
            this.chkCapital.UseVisualStyleBackColor = true;
            // 
            // chkPATS
            // 
            this.chkPATS.AutoSize = true;
            this.chkPATS.Font = new System.Drawing.Font("Verdana", 7F);
            this.chkPATS.Location = new System.Drawing.Point(6, 46);
            this.chkPATS.Name = "chkPATS";
            this.chkPATS.Size = new System.Drawing.Size(52, 16);
            this.chkPATS.TabIndex = 2;
            this.chkPATS.Tag = "PATS";
            this.chkPATS.Text = "PATS";
            this.chkPATS.UseVisualStyleBackColor = true;
            this.chkPATS.CheckedChanged += new System.EventHandler(this.chkPATS_CheckedChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(328, 296);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.Controls.Add(this.lblSQL);
            this.groupBox3.Location = new System.Drawing.Point(158, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(15, 54);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "SQL Info.";
            // 
            // lblSQL
            // 
            this.lblSQL.AutoSize = true;
            this.lblSQL.Location = new System.Drawing.Point(9, 25);
            this.lblSQL.Name = "lblSQL";
            this.lblSQL.Size = new System.Drawing.Size(0, 13);
            this.lblSQL.TabIndex = 0;
            // 
            // frmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 296);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom)));
            this.Font = new System.Drawing.Font("Verdana", 8F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConnection";
            this.ShowInTaskbar = false;
            this.Text = "Source";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RadioButton radiPush;
        private System.Windows.Forms.RadioButton radRedis;
        private System.Windows.Forms.RadioButton radNone;
        private System.Windows.Forms.CheckBox chkPATS;
        private System.Windows.Forms.CheckBox chkCapital;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblSQL;
    }
}