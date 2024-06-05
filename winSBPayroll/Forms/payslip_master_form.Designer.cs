namespace winSBPayroll.Forms
{
    partial class payslip_master_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(payslip_master_form));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnclearfilter = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView_payslip = new System.Windows.Forms.DataGridView();
            this.bindingSource_payslip = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3EmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4BasicPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2PayeTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3GrossTaxableEarnings = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4NetTaxableEarnings = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6NetPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1OtherDeductions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5GrossTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1NSSF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2NHIF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_payslip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_payslip)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnclearfilter);
            this.groupBox1.Controls.Add(this.btnexit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1308, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnclearfilter
            // 
            this.btnclearfilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnclearfilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclearfilter.Location = new System.Drawing.Point(1140, 19);
            this.btnclearfilter.Name = "btnclearfilter";
            this.btnclearfilter.Size = new System.Drawing.Size(82, 23);
            this.btnclearfilter.TabIndex = 8;
            this.btnclearfilter.Text = "clear filter";
            this.btnclearfilter.UseVisualStyleBackColor = false;
            this.btnclearfilter.Click += new System.EventHandler(this.btnclearfilter_Click);
            // 
            // btnexit
            // 
            this.btnexit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnexit.Location = new System.Drawing.Point(1237, 19);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(59, 23);
            this.btnexit.TabIndex = 7;
            this.btnexit.Text = "exit";
            this.btnexit.UseVisualStyleBackColor = false;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView_payslip);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1308, 554);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView_payslip
            // 
            this.dataGridView_payslip.AllowUserToAddRows = false;
            this.dataGridView_payslip.AllowUserToDeleteRows = false;
            this.dataGridView_payslip.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_payslip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_payslip.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1Year,
            this.Column2Period,
            this.Column3EmpNo,
            this.Column4BasicPay,
            this.Column2PayeTax,
            this.Column3GrossTaxableEarnings,
            this.Column4NetTaxableEarnings,
            this.Column6NetPay,
            this.Column1OtherDeductions,
            this.Column5GrossTax,
            this.Column1NSSF,
            this.Column2NHIF,
            this.Column1Department});
            this.dataGridView_payslip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_payslip.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_payslip.Name = "dataGridView_payslip";
            this.dataGridView_payslip.ReadOnly = true;
            this.dataGridView_payslip.Size = new System.Drawing.Size(1302, 535);
            this.dataGridView_payslip.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Year";
            this.dataGridViewTextBoxColumn1.HeaderText = "Year";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Period";
            this.dataGridViewTextBoxColumn2.HeaderText = "Period";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "EmpNo";
            this.dataGridViewTextBoxColumn3.HeaderText = "EmpNo";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 70;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "BasicPay";
            this.dataGridViewTextBoxColumn4.HeaderText = "BasicPay";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 70;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Department";
            this.dataGridViewTextBoxColumn5.HeaderText = "Department";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 70;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "PayeTax";
            this.dataGridViewTextBoxColumn6.HeaderText = "PayeTax";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 70;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "GrossTaxableEarnings";
            this.dataGridViewTextBoxColumn7.HeaderText = "Gross Taxable Earnings";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "NetTaxableEarnings";
            this.dataGridViewTextBoxColumn8.HeaderText = "Net Taxable Earnings";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn9.DataPropertyName = "GrossTax";
            this.dataGridViewTextBoxColumn9.HeaderText = "GrossTax";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 70;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn10.DataPropertyName = "NSSF";
            this.dataGridViewTextBoxColumn10.HeaderText = "NSSF";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 50;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn11.DataPropertyName = "NHIF";
            this.dataGridViewTextBoxColumn11.HeaderText = "NHIF";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 50;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn12.DataPropertyName = "NetPay";
            this.dataGridViewTextBoxColumn12.HeaderText = "NetPay";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn13.DataPropertyName = "NetPay";
            this.dataGridViewTextBoxColumn13.HeaderText = "NetPay";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // Column1Year
            // 
            this.Column1Year.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1Year.DataPropertyName = "Year";
            this.Column1Year.HeaderText = "Year";
            this.Column1Year.Name = "Column1Year";
            this.Column1Year.ReadOnly = true;
            this.Column1Year.Width = 60;
            // 
            // Column2Period
            // 
            this.Column2Period.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2Period.DataPropertyName = "Period";
            this.Column2Period.HeaderText = "Period";
            this.Column2Period.Name = "Column2Period";
            this.Column2Period.ReadOnly = true;
            this.Column2Period.Width = 60;
            // 
            // Column3EmpNo
            // 
            this.Column3EmpNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3EmpNo.DataPropertyName = "EmpNo";
            this.Column3EmpNo.HeaderText = "EmpNo";
            this.Column3EmpNo.Name = "Column3EmpNo";
            this.Column3EmpNo.ReadOnly = true;
            this.Column3EmpNo.Width = 70;
            // 
            // Column4BasicPay
            // 
            this.Column4BasicPay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4BasicPay.DataPropertyName = "BasicPay";
            this.Column4BasicPay.HeaderText = "BasicPay";
            this.Column4BasicPay.Name = "Column4BasicPay";
            this.Column4BasicPay.ReadOnly = true;
            this.Column4BasicPay.Width = 70;
            // 
            // Column2PayeTax
            // 
            this.Column2PayeTax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2PayeTax.DataPropertyName = "PayeTax";
            this.Column2PayeTax.HeaderText = "PayeTax";
            this.Column2PayeTax.Name = "Column2PayeTax";
            this.Column2PayeTax.ReadOnly = true;
            this.Column2PayeTax.Width = 70;
            // 
            // Column3GrossTaxableEarnings
            // 
            this.Column3GrossTaxableEarnings.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3GrossTaxableEarnings.DataPropertyName = "GrossTaxableEarnings";
            this.Column3GrossTaxableEarnings.HeaderText = "Gross Taxable Earnings";
            this.Column3GrossTaxableEarnings.Name = "Column3GrossTaxableEarnings";
            this.Column3GrossTaxableEarnings.ReadOnly = true;
            // 
            // Column4NetTaxableEarnings
            // 
            this.Column4NetTaxableEarnings.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4NetTaxableEarnings.DataPropertyName = "NetTaxableEarnings";
            this.Column4NetTaxableEarnings.HeaderText = "Net Taxable Earnings";
            this.Column4NetTaxableEarnings.Name = "Column4NetTaxableEarnings";
            this.Column4NetTaxableEarnings.ReadOnly = true;
            // 
            // Column6NetPay
            // 
            this.Column6NetPay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column6NetPay.DataPropertyName = "NetPay";
            this.Column6NetPay.HeaderText = "NetPay";
            this.Column6NetPay.Name = "Column6NetPay";
            this.Column6NetPay.ReadOnly = true;
            this.Column6NetPay.Width = 70;
            // 
            // Column1OtherDeductions
            // 
            this.Column1OtherDeductions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1OtherDeductions.DataPropertyName = "OtherDeductions";
            this.Column1OtherDeductions.HeaderText = "Other Deductions";
            this.Column1OtherDeductions.Name = "Column1OtherDeductions";
            this.Column1OtherDeductions.ReadOnly = true;
            // 
            // Column5GrossTax
            // 
            this.Column5GrossTax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column5GrossTax.DataPropertyName = "GrossTax";
            this.Column5GrossTax.HeaderText = "GrossTax";
            this.Column5GrossTax.Name = "Column5GrossTax";
            this.Column5GrossTax.ReadOnly = true;
            // 
            // Column1NSSF
            // 
            this.Column1NSSF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1NSSF.DataPropertyName = "NSSF";
            this.Column1NSSF.HeaderText = "NSSF";
            this.Column1NSSF.Name = "Column1NSSF";
            this.Column1NSSF.ReadOnly = true;
            this.Column1NSSF.Width = 60;
            // 
            // Column2NHIF
            // 
            this.Column2NHIF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2NHIF.DataPropertyName = "NHIF";
            this.Column2NHIF.HeaderText = "NHIF";
            this.Column2NHIF.Name = "Column2NHIF";
            this.Column2NHIF.ReadOnly = true;
            this.Column2NHIF.Width = 60;
            // 
            // Column1Department
            // 
            this.Column1Department.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1Department.DataPropertyName = "Department";
            this.Column1Department.HeaderText = "Department";
            this.Column1Department.Name = "Column1Department";
            this.Column1Department.ReadOnly = true;
            // 
            // payslip_master_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(1308, 613);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "payslip_master_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "payslip master";
            this.Load += new System.EventHandler(this.payslip_master_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_payslip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_payslip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnclearfilter;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.DataGridView dataGridView_payslip;
        private System.Windows.Forms.BindingSource bindingSource_payslip;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2Period;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3EmpNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4BasicPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2PayeTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3GrossTaxableEarnings;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4NetTaxableEarnings;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6NetPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1OtherDeductions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5GrossTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1NSSF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2NHIF;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1Department;
    }
}