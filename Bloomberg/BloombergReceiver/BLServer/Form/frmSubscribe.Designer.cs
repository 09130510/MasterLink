namespace BLPServer
{
    partial class frmSubscribe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSubscribe));
            this.txtSecurities = new System.Windows.Forms.TextBox();
            this.txtFields = new System.Windows.Forms.TextBox();
            this.btnUnsubscription = new System.Windows.Forms.Button();
            this.btnSubscription = new System.Windows.Forms.Button();
            this.btnRemoveField = new System.Windows.Forms.Button();
            this.btnAddField = new System.Windows.Forms.Button();
            this.txtField = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRemoveSecurity = new System.Windows.Forms.Button();
            this.btnAddSecurity = new System.Windows.Forms.Button();
            this.txtSecurity = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSecurities
            // 
            this.txtSecurities.Location = new System.Drawing.Point(4, 28);
            this.txtSecurities.Multiline = true;
            this.txtSecurities.Name = "txtSecurities";
            this.txtSecurities.ReadOnly = true;
            this.txtSecurities.Size = new System.Drawing.Size(159, 90);
            this.txtSecurities.TabIndex = 28;
            this.txtSecurities.Tag = "SUBSCRIBE;SECURITIES;Text";
            this.txtSecurities.TextChanged += new System.EventHandler(this.txtSecurities_Validated);
            this.txtSecurities.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtFields_MouseDown);
            // 
            // txtFields
            // 
            this.txtFields.Location = new System.Drawing.Point(4, 30);
            this.txtFields.Multiline = true;
            this.txtFields.Name = "txtFields";
            this.txtFields.ReadOnly = true;
            this.txtFields.Size = new System.Drawing.Size(159, 90);
            this.txtFields.TabIndex = 29;
            this.txtFields.Tag = "SUBSCRIBE;FIELDS;Text";
            this.txtFields.TextChanged += new System.EventHandler(this.txtSecurities_Validated);
            this.txtFields.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtFields_MouseDown);
            // 
            // btnUnsubscription
            // 
            this.btnUnsubscription.Location = new System.Drawing.Point(93, 123);
            this.btnUnsubscription.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUnsubscription.Name = "btnUnsubscription";
            this.btnUnsubscription.Size = new System.Drawing.Size(70, 25);
            this.btnUnsubscription.TabIndex = 27;
            this.btnUnsubscription.Text = "Unsub";
            this.btnUnsubscription.UseVisualStyleBackColor = true;
            this.btnUnsubscription.Click += new System.EventHandler(this.btnUnsubscription_Click);
            // 
            // btnSubscription
            // 
            this.btnSubscription.Location = new System.Drawing.Point(4, 123);
            this.btnSubscription.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSubscription.Name = "btnSubscription";
            this.btnSubscription.Size = new System.Drawing.Size(70, 25);
            this.btnSubscription.TabIndex = 26;
            this.btnSubscription.Text = "Sub";
            this.btnSubscription.UseVisualStyleBackColor = true;
            this.btnSubscription.Click += new System.EventHandler(this.btnSubscription_Click);
            // 
            // btnRemoveField
            // 
            this.btnRemoveField.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnRemoveField.Location = new System.Drawing.Point(138, 4);
            this.btnRemoveField.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRemoveField.Name = "btnRemoveField";
            this.btnRemoveField.Size = new System.Drawing.Size(25, 25);
            this.btnRemoveField.TabIndex = 25;
            this.btnRemoveField.Text = "-";
            this.btnRemoveField.UseVisualStyleBackColor = true;
            this.btnRemoveField.Click += new System.EventHandler(this.btnRemoveSecurity_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnAddField.Location = new System.Drawing.Point(112, 4);
            this.btnAddField.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(25, 25);
            this.btnAddField.TabIndex = 24;
            this.btnAddField.Text = "+";
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddSecurity_Click);
            // 
            // txtField
            // 
            this.txtField.Location = new System.Drawing.Point(35, 6);
            this.txtField.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtField.Name = "txtField";
            this.txtField.Size = new System.Drawing.Size(75, 19);
            this.txtField.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 12);
            this.label2.TabIndex = 22;
            this.label2.Text = "Field";
            // 
            // btnRemoveSecurity
            // 
            this.btnRemoveSecurity.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnRemoveSecurity.Location = new System.Drawing.Point(138, 2);
            this.btnRemoveSecurity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnRemoveSecurity.Name = "btnRemoveSecurity";
            this.btnRemoveSecurity.Size = new System.Drawing.Size(25, 25);
            this.btnRemoveSecurity.TabIndex = 21;
            this.btnRemoveSecurity.Text = "-";
            this.btnRemoveSecurity.UseVisualStyleBackColor = true;
            this.btnRemoveSecurity.Click += new System.EventHandler(this.btnRemoveSecurity_Click);
            // 
            // btnAddSecurity
            // 
            this.btnAddSecurity.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnAddSecurity.Location = new System.Drawing.Point(112, 2);
            this.btnAddSecurity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddSecurity.Name = "btnAddSecurity";
            this.btnAddSecurity.Size = new System.Drawing.Size(25, 25);
            this.btnAddSecurity.TabIndex = 20;
            this.btnAddSecurity.Text = "+";
            this.btnAddSecurity.UseVisualStyleBackColor = true;
            this.btnAddSecurity.Click += new System.EventHandler(this.btnAddSecurity_Click);
            // 
            // txtSecurity
            // 
            this.txtSecurity.Location = new System.Drawing.Point(35, 4);
            this.txtSecurity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSecurity.Name = "txtSecurity";
            this.txtSecurity.Size = new System.Drawing.Size(75, 19);
            this.txtSecurity.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "ID";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(174, 282);
            this.flowLayoutPanel1.TabIndex = 30;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtSecurities);
            this.panel1.Controls.Add(this.txtSecurity);
            this.panel1.Controls.Add(this.btnAddSecurity);
            this.panel1.Controls.Add(this.btnRemoveSecurity);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(167, 121);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtFields);
            this.panel2.Controls.Add(this.txtField);
            this.panel2.Controls.Add(this.btnUnsubscription);
            this.panel2.Controls.Add(this.btnAddField);
            this.panel2.Controls.Add(this.btnSubscription);
            this.panel2.Controls.Add(this.btnRemoveField);
            this.panel2.Location = new System.Drawing.Point(3, 130);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(167, 151);
            this.panel2.TabIndex = 1;
            // 
            // frmSubscribe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 282);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
            this.Font = new System.Drawing.Font("Verdana", 7F);
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSubscribe";
            this.Text = "Subscribe";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSecurities;
        private System.Windows.Forms.TextBox txtFields;
        private System.Windows.Forms.Button btnUnsubscription;
        private System.Windows.Forms.Button btnSubscription;
        private System.Windows.Forms.Button btnRemoveField;
        private System.Windows.Forms.Button btnAddField;
        private System.Windows.Forms.TextBox txtField;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRemoveSecurity;
        private System.Windows.Forms.Button btnAddSecurity;
        private System.Windows.Forms.TextBox txtSecurity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}