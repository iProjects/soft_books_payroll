namespace winSBPayroll.Forms
{
    partial class AddEmpTxn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEmpTxn));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowYTDinPayslip = new System.Windows.Forms.CheckBox();
            this.chkTrackYTD = new System.Windows.Forms.CheckBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkRecurrent = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_ytd_amount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtYTDAmount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbItemId = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_item_type = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_tax_tracking = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(131, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(266, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 365);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(444, 45);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txt_tax_tracking);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_item_type);
            this.groupBox2.Controls.Add(this.chkShowYTDinPayslip);
            this.groupBox2.Controls.Add(this.chkTrackYTD);
            this.groupBox2.Controls.Add(this.chkEnabled);
            this.groupBox2.Controls.Add(this.chkRecurrent);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lbl_ytd_amount);
            this.groupBox2.Controls.Add(this.txtAmount);
            this.groupBox2.Controls.Add(this.txtYTDAmount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbItemId);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(444, 365);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // chkShowYTDinPayslip
            // 
            this.chkShowYTDinPayslip.AutoSize = true;
            this.chkShowYTDinPayslip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowYTDinPayslip.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkShowYTDinPayslip.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkShowYTDinPayslip.Location = new System.Drawing.Point(131, 317);
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
            this.chkTrackYTD.Location = new System.Drawing.Point(131, 286);
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
            this.chkEnabled.Location = new System.Drawing.Point(131, 224);
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
            this.chkRecurrent.Location = new System.Drawing.Point(131, 255);
            this.chkRecurrent.Name = "chkRecurrent";
            this.chkRecurrent.Size = new System.Drawing.Size(70, 17);
            this.chkRecurrent.TabIndex = 3;
            this.chkRecurrent.Text = "Recurrent";
            this.chkRecurrent.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Amount*";
            // 
            // lbl_ytd_amount
            // 
            this.lbl_ytd_amount.AutoSize = true;
            this.lbl_ytd_amount.Location = new System.Drawing.Point(14, 184);
            this.lbl_ytd_amount.Name = "lbl_ytd_amount";
            this.lbl_ytd_amount.Size = new System.Drawing.Size(110, 13);
            this.lbl_ytd_amount.TabIndex = 12;
            this.lbl_ytd_amount.Text = "Year To Date Amount";
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtAmount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtAmount.Location = new System.Drawing.Point(131, 140);
            this.txtAmount.MaxLength = 10;
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(241, 20);
            this.txtAmount.TabIndex = 1;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            this.txtAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmount_KeyPress);
            // 
            // txtYTDAmount
            // 
            this.txtYTDAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYTDAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtYTDAmount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtYTDAmount.Location = new System.Drawing.Point(131, 180);
            this.txtYTDAmount.MaxLength = 10;
            this.txtYTDAmount.Name = "txtYTDAmount";
            this.txtYTDAmount.Size = new System.Drawing.Size(241, 20);
            this.txtYTDAmount.TabIndex = 2;
            this.txtYTDAmount.TextChanged += new System.EventHandler(this.txtYTDAmount_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 24);
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
            this.cbItemId.SelectedIndexChanged += new System.EventHandler(this.cbItemId_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(98, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Type";
            // 
            // txt_item_type
            // 
            this.txt_item_type.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_item_type.Enabled = false;
            this.txt_item_type.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_item_type.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_item_type.Location = new System.Drawing.Point(131, 60);
            this.txt_item_type.MaxLength = 10;
            this.txt_item_type.Name = "txt_item_type";
            this.txt_item_type.Size = new System.Drawing.Size(241, 20);
            this.txt_item_type.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Tax Tracking";
            // 
            // txt_tax_tracking
            // 
            this.txt_tax_tracking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_tax_tracking.Enabled = false;
            this.txt_tax_tracking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txt_tax_tracking.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_tax_tracking.Location = new System.Drawing.Point(131, 100);
            this.txt_tax_tracking.MaxLength = 10;
            this.txt_tax_tracking.Name = "txt_tax_tracking";
            this.txt_tax_tracking.Size = new System.Drawing.Size(241, 20);
            this.txt_tax_tracking.TabIndex = 18;
            // 
            // AddEmpTxn
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(444, 410);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddEmpTxn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Employee Transaction";
            this.Load += new System.EventHandler(this.AddEmpTxn_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkShowYTDinPayslip;
        private System.Windows.Forms.CheckBox chkTrackYTD;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.CheckBox chkRecurrent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_ytd_amount;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TextBox txtYTDAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbItemId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_tax_tracking;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_item_type;
    }
}