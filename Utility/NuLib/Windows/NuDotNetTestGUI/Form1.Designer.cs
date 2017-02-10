namespace NuDotNetTestGUI
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
            this.txtSvr = new System.Windows.Forms.TextBox();
            this.txtCli = new System.Windows.Forms.TextBox();
            this.btnSvrUp = new System.Windows.Forms.Button();
            this.btnAddClient = new System.Windows.Forms.Button();
            this.btnBroadcast = new System.Windows.Forms.Button();
            this.btnCliBroadcast = new System.Windows.Forms.Button();
            this.btnIniSave = new System.Windows.Forms.Button();
            this.btnIniRead = new System.Windows.Forms.Button();
            this.btnIniSave2 = new System.Windows.Forms.Button();
            this.chkSvr = new System.Windows.Forms.CheckBox();
            this.btnIpcSvrUp = new System.Windows.Forms.Button();
            this.btnIpcSvrDown = new System.Windows.Forms.Button();
            this.btnIpcCliDown1 = new System.Windows.Forms.Button();
            this.btnIpcCliUp1 = new System.Windows.Forms.Button();
            this.btnIpcCliDown2 = new System.Windows.Forms.Button();
            this.btnIpcCliUp2 = new System.Windows.Forms.Button();
            this.btnIpcCli1Send = new System.Windows.Forms.Button();
            this.btnIpcCli2Rcv = new System.Windows.Forms.Button();
            this.btnMMapOpen = new System.Windows.Forms.Button();
            this.btnMMapWriter = new System.Windows.Forms.Button();
            this.btnMMapRead = new System.Windows.Forms.Button();
            this.btnMMFSet = new System.Windows.Forms.Button();
            this.btnQueTake = new System.Windows.Forms.Button();
            this.btnQueAdd = new System.Windows.Forms.Button();
            this.btnSvrDown = new System.Windows.Forms.Button();
            this.btnMMapClose = new System.Windows.Forms.Button();
            this.btnThdPool = new System.Windows.Forms.Button();
            this.btnTukanBusUp = new System.Windows.Forms.Button();
            this.btnTukanBusDown = new System.Windows.Forms.Button();
            this.btnBusCliAdd = new System.Windows.Forms.Button();
            this.btnList = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBusClAdd2 = new System.Windows.Forms.Button();
            this.btnBusSvr = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSvr
            // 
            this.txtSvr.Location = new System.Drawing.Point(12, 65);
            this.txtSvr.Multiline = true;
            this.txtSvr.Name = "txtSvr";
            this.txtSvr.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtSvr.Size = new System.Drawing.Size(385, 235);
            this.txtSvr.TabIndex = 0;
            // 
            // txtCli
            // 
            this.txtCli.Location = new System.Drawing.Point(403, 65);
            this.txtCli.Multiline = true;
            this.txtCli.Name = "txtCli";
            this.txtCli.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCli.Size = new System.Drawing.Size(404, 235);
            this.txtCli.TabIndex = 1;
            // 
            // btnSvrUp
            // 
            this.btnSvrUp.Location = new System.Drawing.Point(12, 9);
            this.btnSvrUp.Name = "btnSvrUp";
            this.btnSvrUp.Size = new System.Drawing.Size(75, 23);
            this.btnSvrUp.TabIndex = 2;
            this.btnSvrUp.Text = "ServerUp";
            this.btnSvrUp.UseVisualStyleBackColor = true;
            this.btnSvrUp.Click += new System.EventHandler(this.btnSvrUp_Click);
            // 
            // btnAddClient
            // 
            this.btnAddClient.Location = new System.Drawing.Point(403, 7);
            this.btnAddClient.Name = "btnAddClient";
            this.btnAddClient.Size = new System.Drawing.Size(75, 23);
            this.btnAddClient.TabIndex = 3;
            this.btnAddClient.Text = "AddClient";
            this.btnAddClient.UseVisualStyleBackColor = true;
            this.btnAddClient.Click += new System.EventHandler(this.btnAddClient_Click);
            // 
            // btnBroadcast
            // 
            this.btnBroadcast.Location = new System.Drawing.Point(171, 8);
            this.btnBroadcast.Name = "btnBroadcast";
            this.btnBroadcast.Size = new System.Drawing.Size(62, 23);
            this.btnBroadcast.TabIndex = 4;
            this.btnBroadcast.Text = "送資料";
            this.btnBroadcast.UseVisualStyleBackColor = true;
            this.btnBroadcast.Click += new System.EventHandler(this.btnBroadcast_Click);
            // 
            // btnCliBroadcast
            // 
            this.btnCliBroadcast.Location = new System.Drawing.Point(484, 6);
            this.btnCliBroadcast.Name = "btnCliBroadcast";
            this.btnCliBroadcast.Size = new System.Drawing.Size(75, 23);
            this.btnCliBroadcast.TabIndex = 5;
            this.btnCliBroadcast.Text = "送資料";
            this.btnCliBroadcast.UseVisualStyleBackColor = true;
            this.btnCliBroadcast.Click += new System.EventHandler(this.btnCliBroadcast_Click);
            // 
            // btnIniSave
            // 
            this.btnIniSave.Location = new System.Drawing.Point(94, 307);
            this.btnIniSave.Name = "btnIniSave";
            this.btnIniSave.Size = new System.Drawing.Size(75, 23);
            this.btnIniSave.TabIndex = 6;
            this.btnIniSave.Text = "Ini Save";
            this.btnIniSave.UseVisualStyleBackColor = true;
            this.btnIniSave.Click += new System.EventHandler(this.btnIniSave_Click);
            // 
            // btnIniRead
            // 
            this.btnIniRead.Location = new System.Drawing.Point(12, 307);
            this.btnIniRead.Name = "btnIniRead";
            this.btnIniRead.Size = new System.Drawing.Size(75, 23);
            this.btnIniRead.TabIndex = 7;
            this.btnIniRead.Text = "ini Read";
            this.btnIniRead.UseVisualStyleBackColor = true;
            this.btnIniRead.Click += new System.EventHandler(this.btnIniRead_Click);
            // 
            // btnIniSave2
            // 
            this.btnIniSave2.Location = new System.Drawing.Point(177, 307);
            this.btnIniSave2.Name = "btnIniSave2";
            this.btnIniSave2.Size = new System.Drawing.Size(75, 23);
            this.btnIniSave2.TabIndex = 8;
            this.btnIniSave2.Text = "Ini Save2";
            this.btnIniSave2.UseVisualStyleBackColor = true;
            this.btnIniSave2.Click += new System.EventHandler(this.btnIniSave2_Click);
            // 
            // chkSvr
            // 
            this.chkSvr.AutoSize = true;
            this.chkSvr.Location = new System.Drawing.Point(340, 330);
            this.chkSvr.Name = "chkSvr";
            this.chkSvr.Size = new System.Drawing.Size(15, 14);
            this.chkSvr.TabIndex = 9;
            this.chkSvr.UseVisualStyleBackColor = true;
            // 
            // btnIpcSvrUp
            // 
            this.btnIpcSvrUp.BackColor = System.Drawing.Color.Pink;
            this.btnIpcSvrUp.Location = new System.Drawing.Point(12, 36);
            this.btnIpcSvrUp.Name = "btnIpcSvrUp";
            this.btnIpcSvrUp.Size = new System.Drawing.Size(75, 23);
            this.btnIpcSvrUp.TabIndex = 10;
            this.btnIpcSvrUp.Text = "IpcSvrUp";
            this.btnIpcSvrUp.UseVisualStyleBackColor = false;
            this.btnIpcSvrUp.Click += new System.EventHandler(this.btnIpcSvrUp_Click);
            // 
            // btnIpcSvrDown
            // 
            this.btnIpcSvrDown.BackColor = System.Drawing.Color.Pink;
            this.btnIpcSvrDown.Location = new System.Drawing.Point(94, 37);
            this.btnIpcSvrDown.Name = "btnIpcSvrDown";
            this.btnIpcSvrDown.Size = new System.Drawing.Size(75, 23);
            this.btnIpcSvrDown.TabIndex = 11;
            this.btnIpcSvrDown.Text = "IpcSvrDown";
            this.btnIpcSvrDown.UseVisualStyleBackColor = false;
            this.btnIpcSvrDown.Click += new System.EventHandler(this.btnIpcSvrDown_Click);
            // 
            // btnIpcCliDown1
            // 
            this.btnIpcCliDown1.BackColor = System.Drawing.Color.Pink;
            this.btnIpcCliDown1.Location = new System.Drawing.Point(495, 37);
            this.btnIpcCliDown1.Name = "btnIpcCliDown1";
            this.btnIpcCliDown1.Size = new System.Drawing.Size(86, 23);
            this.btnIpcCliDown1.TabIndex = 13;
            this.btnIpcCliDown1.Text = "IpcCliDown1";
            this.btnIpcCliDown1.UseVisualStyleBackColor = false;
            this.btnIpcCliDown1.Click += new System.EventHandler(this.btnIpcCliDown1_Click);
            // 
            // btnIpcCliUp1
            // 
            this.btnIpcCliUp1.BackColor = System.Drawing.Color.Pink;
            this.btnIpcCliUp1.Location = new System.Drawing.Point(413, 36);
            this.btnIpcCliUp1.Name = "btnIpcCliUp1";
            this.btnIpcCliUp1.Size = new System.Drawing.Size(75, 23);
            this.btnIpcCliUp1.TabIndex = 12;
            this.btnIpcCliUp1.Text = "IpcCliUp1";
            this.btnIpcCliUp1.UseVisualStyleBackColor = false;
            this.btnIpcCliUp1.Click += new System.EventHandler(this.btnIpcCliUp1_Click);
            // 
            // btnIpcCliDown2
            // 
            this.btnIpcCliDown2.BackColor = System.Drawing.Color.Pink;
            this.btnIpcCliDown2.Location = new System.Drawing.Point(485, 307);
            this.btnIpcCliDown2.Name = "btnIpcCliDown2";
            this.btnIpcCliDown2.Size = new System.Drawing.Size(86, 23);
            this.btnIpcCliDown2.TabIndex = 15;
            this.btnIpcCliDown2.Text = "IpcCliDown2";
            this.btnIpcCliDown2.UseVisualStyleBackColor = false;
            this.btnIpcCliDown2.Click += new System.EventHandler(this.btnIpcCliDown2_Click);
            // 
            // btnIpcCliUp2
            // 
            this.btnIpcCliUp2.BackColor = System.Drawing.Color.Pink;
            this.btnIpcCliUp2.Location = new System.Drawing.Point(403, 306);
            this.btnIpcCliUp2.Name = "btnIpcCliUp2";
            this.btnIpcCliUp2.Size = new System.Drawing.Size(75, 23);
            this.btnIpcCliUp2.TabIndex = 14;
            this.btnIpcCliUp2.Text = "IpcCliUp2";
            this.btnIpcCliUp2.UseVisualStyleBackColor = false;
            this.btnIpcCliUp2.Click += new System.EventHandler(this.btnIpcCliUp2_Click);
            // 
            // btnIpcCli1Send
            // 
            this.btnIpcCli1Send.BackColor = System.Drawing.Color.Pink;
            this.btnIpcCli1Send.Location = new System.Drawing.Point(586, 37);
            this.btnIpcCli1Send.Name = "btnIpcCli1Send";
            this.btnIpcCli1Send.Size = new System.Drawing.Size(75, 23);
            this.btnIpcCli1Send.TabIndex = 16;
            this.btnIpcCli1Send.Text = "Send";
            this.btnIpcCli1Send.UseVisualStyleBackColor = false;
            this.btnIpcCli1Send.Click += new System.EventHandler(this.btnIpcCli1Send_Click);
            // 
            // btnIpcCli2Rcv
            // 
            this.btnIpcCli2Rcv.BackColor = System.Drawing.Color.Pink;
            this.btnIpcCli2Rcv.Location = new System.Drawing.Point(577, 306);
            this.btnIpcCli2Rcv.Name = "btnIpcCli2Rcv";
            this.btnIpcCli2Rcv.Size = new System.Drawing.Size(75, 23);
            this.btnIpcCli2Rcv.TabIndex = 17;
            this.btnIpcCli2Rcv.Text = "Recv";
            this.btnIpcCli2Rcv.UseVisualStyleBackColor = false;
            this.btnIpcCli2Rcv.Click += new System.EventHandler(this.btnIpcCli2Rcv_Click);
            // 
            // btnMMapOpen
            // 
            this.btnMMapOpen.BackColor = System.Drawing.Color.LightBlue;
            this.btnMMapOpen.Location = new System.Drawing.Point(238, 7);
            this.btnMMapOpen.Name = "btnMMapOpen";
            this.btnMMapOpen.Size = new System.Drawing.Size(75, 23);
            this.btnMMapOpen.TabIndex = 18;
            this.btnMMapOpen.Text = "MMapOpen";
            this.btnMMapOpen.UseVisualStyleBackColor = false;
            this.btnMMapOpen.Click += new System.EventHandler(this.btnMMapOpen_Click);
            // 
            // btnMMapWriter
            // 
            this.btnMMapWriter.BackColor = System.Drawing.Color.LightBlue;
            this.btnMMapWriter.Location = new System.Drawing.Point(249, 37);
            this.btnMMapWriter.Name = "btnMMapWriter";
            this.btnMMapWriter.Size = new System.Drawing.Size(75, 23);
            this.btnMMapWriter.TabIndex = 19;
            this.btnMMapWriter.Text = "MMapWrite";
            this.btnMMapWriter.UseVisualStyleBackColor = false;
            this.btnMMapWriter.Click += new System.EventHandler(this.btnMMapWriter_Click);
            // 
            // btnMMapRead
            // 
            this.btnMMapRead.BackColor = System.Drawing.Color.LightBlue;
            this.btnMMapRead.Location = new System.Drawing.Point(171, 37);
            this.btnMMapRead.Name = "btnMMapRead";
            this.btnMMapRead.Size = new System.Drawing.Size(75, 23);
            this.btnMMapRead.TabIndex = 20;
            this.btnMMapRead.Text = "MMapRead";
            this.btnMMapRead.UseVisualStyleBackColor = false;
            this.btnMMapRead.Click += new System.EventHandler(this.btnMMapRead_Click);
            // 
            // btnMMFSet
            // 
            this.btnMMFSet.BackColor = System.Drawing.Color.LightBlue;
            this.btnMMFSet.Location = new System.Drawing.Point(330, 36);
            this.btnMMFSet.Name = "btnMMFSet";
            this.btnMMFSet.Size = new System.Drawing.Size(75, 23);
            this.btnMMFSet.TabIndex = 21;
            this.btnMMFSet.Text = "MMapSet";
            this.btnMMFSet.UseVisualStyleBackColor = false;
            this.btnMMFSet.Click += new System.EventHandler(this.btnMMFSet_Click);
            // 
            // btnQueTake
            // 
            this.btnQueTake.Location = new System.Drawing.Point(576, 6);
            this.btnQueTake.Name = "btnQueTake";
            this.btnQueTake.Size = new System.Drawing.Size(75, 23);
            this.btnQueTake.TabIndex = 22;
            this.btnQueTake.Text = "QueTake";
            this.btnQueTake.UseVisualStyleBackColor = true;
            this.btnQueTake.Click += new System.EventHandler(this.btnQueTake_Click);
            // 
            // btnQueAdd
            // 
            this.btnQueAdd.Location = new System.Drawing.Point(657, 6);
            this.btnQueAdd.Name = "btnQueAdd";
            this.btnQueAdd.Size = new System.Drawing.Size(75, 23);
            this.btnQueAdd.TabIndex = 23;
            this.btnQueAdd.Text = "QueAdd";
            this.btnQueAdd.UseVisualStyleBackColor = true;
            this.btnQueAdd.Click += new System.EventHandler(this.btnQueAdd_Click);
            // 
            // btnSvrDown
            // 
            this.btnSvrDown.Location = new System.Drawing.Point(90, 9);
            this.btnSvrDown.Name = "btnSvrDown";
            this.btnSvrDown.Size = new System.Drawing.Size(75, 23);
            this.btnSvrDown.TabIndex = 24;
            this.btnSvrDown.Text = "ServerDown";
            this.btnSvrDown.UseVisualStyleBackColor = true;
            this.btnSvrDown.Click += new System.EventHandler(this.btnSvrDown_Click);
            // 
            // btnMMapClose
            // 
            this.btnMMapClose.BackColor = System.Drawing.Color.LightBlue;
            this.btnMMapClose.Location = new System.Drawing.Point(322, 7);
            this.btnMMapClose.Name = "btnMMapClose";
            this.btnMMapClose.Size = new System.Drawing.Size(75, 23);
            this.btnMMapClose.TabIndex = 25;
            this.btnMMapClose.Text = "MMapClose";
            this.btnMMapClose.UseVisualStyleBackColor = false;
            this.btnMMapClose.Click += new System.EventHandler(this.btnMMapClose_Click);
            // 
            // btnThdPool
            // 
            this.btnThdPool.Location = new System.Drawing.Point(738, 6);
            this.btnThdPool.Name = "btnThdPool";
            this.btnThdPool.Size = new System.Drawing.Size(75, 23);
            this.btnThdPool.TabIndex = 26;
            this.btnThdPool.Text = "ThdPool";
            this.btnThdPool.UseVisualStyleBackColor = true;
            this.btnThdPool.Click += new System.EventHandler(this.btnThdPool_Click);
            // 
            // btnTukanBusUp
            // 
            this.btnTukanBusUp.Location = new System.Drawing.Point(12, 336);
            this.btnTukanBusUp.Name = "btnTukanBusUp";
            this.btnTukanBusUp.Size = new System.Drawing.Size(75, 23);
            this.btnTukanBusUp.TabIndex = 27;
            this.btnTukanBusUp.Text = "BusUp";
            this.btnTukanBusUp.UseVisualStyleBackColor = true;
            this.btnTukanBusUp.Click += new System.EventHandler(this.btnTukanBusUp_Click);
            // 
            // btnTukanBusDown
            // 
            this.btnTukanBusDown.Location = new System.Drawing.Point(90, 336);
            this.btnTukanBusDown.Name = "btnTukanBusDown";
            this.btnTukanBusDown.Size = new System.Drawing.Size(75, 23);
            this.btnTukanBusDown.TabIndex = 28;
            this.btnTukanBusDown.Text = "BusDown";
            this.btnTukanBusDown.UseVisualStyleBackColor = true;
            this.btnTukanBusDown.Click += new System.EventHandler(this.btnTukanBusDown_Click);
            // 
            // btnBusCliAdd
            // 
            this.btnBusCliAdd.Location = new System.Drawing.Point(403, 335);
            this.btnBusCliAdd.Name = "btnBusCliAdd";
            this.btnBusCliAdd.Size = new System.Drawing.Size(75, 23);
            this.btnBusCliAdd.TabIndex = 29;
            this.btnBusCliAdd.Text = "BusCliAdd";
            this.btnBusCliAdd.UseVisualStyleBackColor = true;
            this.btnBusCliAdd.Click += new System.EventHandler(this.btnBusCliAdd_Click);
            // 
            // btnList
            // 
            this.btnList.Location = new System.Drawing.Point(667, 37);
            this.btnList.Name = "btnList";
            this.btnList.Size = new System.Drawing.Size(75, 23);
            this.btnList.TabIndex = 30;
            this.btnList.Text = "NuList";
            this.btnList.UseVisualStyleBackColor = true;
            this.btnList.Click += new System.EventHandler(this.btnList_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(666, 306);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 31;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBusClAdd2
            // 
            this.btnBusClAdd2.Location = new System.Drawing.Point(485, 335);
            this.btnBusClAdd2.Name = "btnBusClAdd2";
            this.btnBusClAdd2.Size = new System.Drawing.Size(75, 23);
            this.btnBusClAdd2.TabIndex = 32;
            this.btnBusClAdd2.Text = "BusCliAdd2";
            this.btnBusClAdd2.UseVisualStyleBackColor = true;
            this.btnBusClAdd2.Click += new System.EventHandler(this.btnBusClAdd2_Click);
            // 
            // btnBusSvr
            // 
            this.btnBusSvr.Location = new System.Drawing.Point(177, 336);
            this.btnBusSvr.Name = "btnBusSvr";
            this.btnBusSvr.Size = new System.Drawing.Size(75, 23);
            this.btnBusSvr.TabIndex = 33;
            this.btnBusSvr.Text = "BusSvr";
            this.btnBusSvr.UseVisualStyleBackColor = true;
            this.btnBusSvr.Click += new System.EventHandler(this.btnBusSvr_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 366);
            this.Controls.Add(this.btnBusSvr);
            this.Controls.Add(this.btnBusClAdd2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnList);
            this.Controls.Add(this.btnBusCliAdd);
            this.Controls.Add(this.btnTukanBusDown);
            this.Controls.Add(this.btnTukanBusUp);
            this.Controls.Add(this.btnThdPool);
            this.Controls.Add(this.btnMMapClose);
            this.Controls.Add(this.btnSvrDown);
            this.Controls.Add(this.btnQueAdd);
            this.Controls.Add(this.btnQueTake);
            this.Controls.Add(this.btnMMFSet);
            this.Controls.Add(this.btnMMapRead);
            this.Controls.Add(this.btnMMapWriter);
            this.Controls.Add(this.btnMMapOpen);
            this.Controls.Add(this.btnIpcCli2Rcv);
            this.Controls.Add(this.btnIpcCli1Send);
            this.Controls.Add(this.btnIpcCliDown2);
            this.Controls.Add(this.btnIpcCliUp2);
            this.Controls.Add(this.btnIpcCliDown1);
            this.Controls.Add(this.btnIpcCliUp1);
            this.Controls.Add(this.btnIpcSvrDown);
            this.Controls.Add(this.btnIpcSvrUp);
            this.Controls.Add(this.chkSvr);
            this.Controls.Add(this.btnIniSave2);
            this.Controls.Add(this.btnIniRead);
            this.Controls.Add(this.btnIniSave);
            this.Controls.Add(this.btnCliBroadcast);
            this.Controls.Add(this.btnBroadcast);
            this.Controls.Add(this.btnAddClient);
            this.Controls.Add(this.btnSvrUp);
            this.Controls.Add(this.txtCli);
            this.Controls.Add(this.txtSvr);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSvr;
        private System.Windows.Forms.TextBox txtCli;
        private System.Windows.Forms.Button btnSvrUp;
        private System.Windows.Forms.Button btnAddClient;
        private System.Windows.Forms.Button btnBroadcast;
        private System.Windows.Forms.Button btnCliBroadcast;
        private System.Windows.Forms.Button btnIniSave;
        private System.Windows.Forms.Button btnIniRead;
        private System.Windows.Forms.Button btnIniSave2;
        private System.Windows.Forms.CheckBox chkSvr;
        private System.Windows.Forms.Button btnIpcSvrUp;
        private System.Windows.Forms.Button btnIpcSvrDown;
        private System.Windows.Forms.Button btnIpcCliDown1;
        private System.Windows.Forms.Button btnIpcCliUp1;
        private System.Windows.Forms.Button btnIpcCliDown2;
        private System.Windows.Forms.Button btnIpcCliUp2;
        private System.Windows.Forms.Button btnIpcCli1Send;
        private System.Windows.Forms.Button btnIpcCli2Rcv;
        private System.Windows.Forms.Button btnMMapOpen;
        private System.Windows.Forms.Button btnMMapWriter;
        private System.Windows.Forms.Button btnMMapRead;
        private System.Windows.Forms.Button btnMMFSet;
        private System.Windows.Forms.Button btnQueTake;
        private System.Windows.Forms.Button btnQueAdd;
        private System.Windows.Forms.Button btnSvrDown;
        private System.Windows.Forms.Button btnMMapClose;
        private System.Windows.Forms.Button btnThdPool;
        private System.Windows.Forms.Button btnTukanBusUp;
        private System.Windows.Forms.Button btnTukanBusDown;
        private System.Windows.Forms.Button btnBusCliAdd;
        private System.Windows.Forms.Button btnList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnBusClAdd2;
        private System.Windows.Forms.Button btnBusSvr;
    }
}

