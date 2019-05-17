namespace winSBPayroll.Forms
{
    partial class EditEmpTxn
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditEmpTxn));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkProcessed = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowYTDinPayslip = new System.Windows.Forms.CheckBox();
            this.chkTrackYTD = new System.Windows.Forms.CheckBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkRecurrent = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtYTDAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbItemId = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(239, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(131, 21);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 263);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 56);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // chkProcessed
            // 
            this.chkProcessed.AutoSize = true;
            this.chkProcessed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkProcessed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkProcessed.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkProcessed.Location = new System.Drawing.Point(131, 235);
            this.chkProcessed.Name = "chkProcessed";
            this.chkProcessed.Size = new System.Drawing.Size(73, 17);
            this.chkProcessed.TabIndex = 8;
            this.chkProcessed.Text = "Processed";
            this.chkProcessed.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkShowYTDinPayslip);
            this.groupBox2.Controls.Add(this.chkTrackYTD);
            this.groupBox2.Controls.Add(this.chkProcessed);
            this.groupBox2.Controls.Add(this.chkEnabled);
            this.groupBox2.Controls.Add(this.chkRecurrent);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtAmount);
            this.groupBox2.Controls.Add(this.txtYTDAmount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbItemId);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 263);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // chkShowYTDinPayslip
            // 
            this.chkShowYTDinPayslip.AutoSize = true;
            this.chkShowYTDinPayslip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowYTDinPayslip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkShowYTDinPayslip.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkShowYTDinPayslip.Location = new System.Drawing.Point(131, 207);
            this.chkShowYTDinPayslip.Name = "chkShowYTDinPayslip";
            this.chkShowYTDinPayslip.Size = new System.Drawing.Size(165, 17);
            this.chkShowYTDinPayslip.TabIndex = 6;
            this.chkShowYTDinPayslip.Text = "Show Year To Date In Payslip";
            this.chkShowYTDinPayslip.UseVisualStyleBackColor = true;
            // 
            // chkTrackYTD
            // 
            this.chkTrackYTD.AutoSize = true;
            this.chkTrackYTD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTrackYTD.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkTrackYTD.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkTrackYTD.Location = new System.Drawing.Point(131, 179);
            this.chkTrackYTD.Name = "chkTrackYTD";
            this.chkTrackYTD.Size = new System.Drawing.Size(118, 17);
            this.chkTrackYTD.TabIndex = 5;
            this.chkTrackYTD.Text = "Track Year To Date";
            this.chkTrackYTD.UseVisualStyleBackColor = true;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkEnabled.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkEnabled.Location = new System.Drawing.Point(131, 131);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(62, 17);
            this.chkEnabled.TabIndex = 4;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            // 
            // chkRecurrent
            // 
            this.chkRecurrent.AutoSize = true;
            this.chkRecurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRecurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkRecurrent.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkRecurrent.Location = new System.Drawing.Point(131, 156);
            this.chkRecurrent.Name = "chkRecurrent";
            this.chkRecurrent.Size = new System.Drawing.Size(70, 17);
            this.chkRecurrent.TabIndex = 3;
            this.chkRecurrent.Text = "Recurrent";
            this.chkRecurrent.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Amount*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Year To Date Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtAmount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmount.Location = new System.Drawing.Point(131, 56);
            this.txtAmount.MaxLength = 10;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(241, 20);
            this.txtAmount.TabIndex = 1;
            // 
            // txtYTDAmount
            // 
            this.txtYTDAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYTDAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtYTDAmount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtYTDAmount.Location = new System.Drawing.Point(131, 92);
            this.txtYTDAmount.MaxLength = 10;
            this.txtYTDAmount.Name = "txtYTDAmount";
            this.txtYTDAmount.Size = new System.Drawing.Size(241, 20);
            this.txtYTDAmount.TabIndex = 2;
            this.txtYTDAmount.TextChanged += new System.EventHandler(this.txtYTDAmount_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Payroll ItemId*";
            // 
            // cbItemId
            // 
            this.cbItemId.BackColor = System.Drawing.Color.White;
            this.cbItemId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemId.DropDownWidth = 200;
            this.cbItemId.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbItemId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbItemId.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbItemId.FormattingEnabled = true;
            this.cbItemId.Location = new System.Drawing.Point(131, 19);
            this.cbItemId.Name = "cbItemId";
            this.cbItemId.Size = new System.Drawing.Size(241, 21);
            this.cbItemId.TabIndex = 0;
            // 
            // EditEmpTxn
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(444, 319);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditEmpTxn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EditEmpTxn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkProcessed;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkShowYTDinPayslip;
        private System.Windows.Forms.CheckBox chkTrackYTD;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.CheckBox chkRecurrent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtYTDAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbItemId;
    }
}