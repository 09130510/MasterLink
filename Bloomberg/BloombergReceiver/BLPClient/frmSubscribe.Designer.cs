namespace BLPClient
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
            this.SuspendLayout();
            // 
            // txtSecurities
            // 
            this.txtSecurities.Location = new System.Drawing.Point(8, 30);
            this.txtSecurities.Multiline = true;
            this.txtSecurities.Name = "txtSecurities";
            this.txtSecurities.ReadOnly = true;
            this.txtSecurities.Size = new System.Drawing.Size(185, 97);
            this.txtSecurities.TabIndex = 28;
            this.txtSecurities.Tag = "SUBSCRIBE;SECURITIES;Text";
            this.txtSecurities.TextChanged += new System.EventHandler(this.txtSecurities_Validated);
            this.txtSecurities.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtFields_MouseDown);
            // 
            // txtFields
            // 
            this.txtFields.Location = new System.Drawing.Point(8, 158);
            this.txtFields.Multiline = true;
            this.txtFields.Name = "txtFields";
            this.txtFields.ReadOnly = true;
            this.txtFields.Size = new System.Drawing.Size(185, 97);
            this.txtFields.TabIndex = 29;
            this.txtFields.Tag = "SUBSCRIBE;FIELDS;Text";
            this.txtFields.TextChanged += new System.EventHandler(this.txtSecurities_Validated);
            this.txtFields.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtFields_MouseDown);
            // 
            // btnUnsubscription
            // 
            this.btnUnsubscription.Location = new System.Drawing.Point(111, 259);
            this.btnUnsubscription.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnUnsubscription.Name = "btnUnsubscription";
            this.btnUnsubscription.Size = new System.Drawing.Size(82, 27);
            this.btnUnsubscription.TabIndex = 27;
            this.btnUnsubscription.Text = "Unsub";
            this.btnUnsubscription.UseVisualStyleBackColor = true;
            this.btnUnsubscription.Click += new System.EventHandler(this.btnUnsubscription_Click);
            // 
            // btnSubscription
            // 
            this.btnSubscription.Location = new System.Drawing.Point(8, 259);
            this.btnSubscription.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnSubscription.Name = "btnSubscription";
            this.btnSubscription.Size = new System.Drawing.Size(82, 27);
            this.btnSubscription.TabIndex = 26;
            this.btnSubscription.Text = "Sub";
            this.btnSubscription.UseVisualStyleBackColor = true;
            this.btnSubscription.Click += new System.EventHandler(this.btnSubscription_Click);
            // 
            // btnRemoveField
            // 
            this.btnRemoveField.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnRemoveField.Location = new System.Drawing.Point(164, 130);
            this.btnRemoveField.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRemoveField.Name = "btnRemoveField";
            this.btnRemoveField.Size = new System.Drawing.Size(29, 27);
            this.btnRemoveField.TabIndex = 25;
            this.btnRemoveField.Text = "-";
            this.btnRemoveField.UseVisualStyleBackColor = true;
            this.btnRemoveField.Click += new System.EventHandler(this.btnRemoveSecurity_Click);
            // 
            // btnAddField
            // 
            this.btnAddField.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnAddField.Location = new System.Drawing.Point(134, 130);
            this.btnAddField.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnAddField.Name = "btnAddField";
            this.btnAddField.Size = new System.Drawing.Size(29, 27);
            this.btnAddField.TabIndex = 24;
            this.btnAddField.Text = "+";
            this.btnAddField.UseVisualStyleBackColor = true;
            this.btnAddField.Click += new System.EventHandler(this.btnAddSecurity_Click);
            // 
            // txtField
            // 
            this.txtField.Location = new System.Drawing.Point(44, 132);
            this.txtField.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtField.Name = "txtField";
            this.txtField.Size = new System.Drawing.Size(87, 21);
            this.txtField.TabIndex = 23;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 137);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "欄位";
            // 
            // btnRemoveSecurity
            // 
            this.btnRemoveSecurity.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnRemoveSecurity.Location = new System.Drawing.Point(164, 2);
            this.btnRemoveSecurity.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnRemoveSecurity.Name = "btnRemoveSecurity";
            this.btnRemoveSecurity.Size = new System.Drawing.Size(29, 27);
            this.btnRemoveSecurity.TabIndex = 21;
            this.btnRemoveSecurity.Text = "-";
            this.btnRemoveSecurity.UseVisualStyleBackColor = true;
            this.btnRemoveSecurity.Click += new System.EventHandler(this.btnRemoveSecurity_Click);
            // 
            // btnAddSecurity
            // 
            this.btnAddSecurity.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold);
            this.btnAddSecurity.Location = new System.Drawing.Point(134, 2);
            this.btnAddSecurity.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.btnAddSecurity.Name = "btnAddSecurity";
            this.btnAddSecurity.Size = new System.Drawing.Size(29, 27);
            this.btnAddSecurity.TabIndex = 20;
            this.btnAddSecurity.Text = "+";
            this.btnAddSecurity.UseVisualStyleBackColor = true;
            this.btnAddSecurity.Click += new System.EventHandler(this.btnAddSecurity_Click);
            // 
            // txtSecurity
            // 
            this.txtSecurity.Location = new System.Drawing.Point(44, 4);
            this.txtSecurity.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.txtSecurity.Name = "txtSecurity";
            this.txtSecurity.Size = new System.Drawing.Size(87, 21);
            this.txtSecurity.TabIndex = 19;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "商品";
            // 
            // frmSubscribe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 290);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.txtSecurities);
            this.Controls.Add(this.txtFields);
            this.Controls.Add(this.btnUnsubscription);
            this.Controls.Add(this.btnSubscription);
            this.Controls.Add(this.btnRemoveField);
            this.Controls.Add(this.btnAddField);
            this.Controls.Add(this.txtField);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRemoveSecurity);
            this.Controls.Add(this.btnAddSecurity);
            this.Controls.Add(this.txtSecurity);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSubscribe";
            this.Text = "訂閱";
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
    }
}