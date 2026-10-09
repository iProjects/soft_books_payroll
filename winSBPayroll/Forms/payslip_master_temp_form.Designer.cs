namespace winSBPayroll.Forms
{
    partial class payslip_master_temp_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(payslip_master_temp_form));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbopayrollperiods = new System.Windows.Forms.ComboBox();
            this.cbo_employee_names = new System.Windows.Forms.ComboBox();
            this.cbo_employers = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btndetails = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cbopayrollyears = new System.Windows.Forms.ComboBox();
            this.cboemployeeno = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbo_departments = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnclearfilter = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelPager = new System.Windows.Forms.Panel();
            this.dataGridView_payslip = new System.Windows.Forms.DataGridView();
            this.bindingSource_payslip = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
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
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2CompName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3EmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4BasicPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3GrossTaxableEarnings = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4NetTaxableEarnings = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6NetPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1NSSF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSHIF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAHL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5GrossTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2PayeTax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1OtherDeductions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_payslip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_payslip)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbopayrollperiods);
            this.groupBox1.Controls.Add(this.cbo_employee_names);
            this.groupBox1.Controls.Add(this.cbo_employers);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btndetails);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbopayrollyears);
            this.groupBox1.Controls.Add(this.cboemployeeno);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbo_departments);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnclearfilter);
            this.groupBox1.Controls.Add(this.btnexit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1300, 59);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(934, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 31;
            this.label4.Text = "Period";
            // 
            // cbopayrollperiods
            // 
            this.cbopayrollperiods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopayrollperiods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbopayrollperiods.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbopayrollperiods.ForeColor = System.Drawing.Color.Black;
            this.cbopayrollperiods.FormattingEnabled = true;
            this.cbopayrollperiods.Location = new System.Drawing.Point(973, 21);
            this.cbopayrollperiods.Name = "cbopayrollperiods";
            this.cbopayrollperiods.Size = new System.Drawing.Size(54, 21);
            this.cbopayrollperiods.TabIndex = 5;
            this.cbopayrollperiods.SelectedIndexChanged += new System.EventHandler(this.cbopayrollperiods_SelectedIndexChanged);
            // 
            // cbo_employee_names
            // 
            this.cbo_employee_names.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_employee_names.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_employee_names.FormattingEnabled = true;
            this.cbo_employee_names.Location = new System.Drawing.Point(282, 21);
            this.cbo_employee_names.Name = "cbo_employee_names";
            this.cbo_employee_names.Size = new System.Drawing.Size(175, 21);
            this.cbo_employee_names.TabIndex = 1;
            this.cbo_employee_names.SelectedIndexChanged += new System.EventHandler(this.cbo_employee_othernames_SelectedIndexChanged);
            // 
            // cbo_employers
            // 
            this.cbo_employers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_employers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_employers.FormattingEnabled = true;
            this.cbo_employers.Location = new System.Drawing.Point(68, 22);
            this.cbo_employers.Name = "cbo_employers";
            this.cbo_employers.Size = new System.Drawing.Size(118, 21);
            this.cbo_employers.TabIndex = 0;
            this.cbo_employers.SelectedIndexChanged += new System.EventHandler(this.cbo_employers_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Employer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "Employee Name";
            // 
            // btndetails
            // 
            this.btndetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btndetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndetails.Location = new System.Drawing.Point(1045, 19);
            this.btndetails.Name = "btndetails";
            this.btndetails.Size = new System.Drawing.Size(59, 23);
            this.btndetails.TabIndex = 6;
            this.btndetails.Text = "details";
            this.btndetails.UseVisualStyleBackColor = false;
            this.btndetails.Click += new System.EventHandler(this.btndetails_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(806, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Year";
            // 
            // cbopayrollyears
            // 
            this.cbopayrollyears.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopayrollyears.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbopayrollyears.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbopayrollyears.ForeColor = System.Drawing.Color.Black;
            this.cbopayrollyears.FormattingEnabled = true;
            this.cbopayrollyears.Location = new System.Drawing.Point(841, 20);
            this.cbopayrollyears.Name = "cbopayrollyears";
            this.cbopayrollyears.Size = new System.Drawing.Size(75, 21);
            this.cbopayrollyears.TabIndex = 4;
            this.cbopayrollyears.SelectedIndexChanged += new System.EventHandler(this.cbopayrollyears_SelectedIndexChanged);
            // 
            // cboemployeeno
            // 
            this.cboemployeeno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboemployeeno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboemployeeno.FormattingEnabled = true;
            this.cboemployeeno.Location = new System.Drawing.Point(536, 21);
            this.cboemployeeno.Name = "cboemployeeno";
            this.cboemployeeno.Size = new System.Drawing.Size(91, 21);
            this.cboemployeeno.TabIndex = 2;
            this.cboemployeeno.SelectedIndexChanged += new System.EventHandler(this.cboemployeeno_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(463, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Employee No";
            // 
            // cbo_departments
            // 
            this.cbo_departments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_departments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_departments.FormattingEnabled = true;
            this.cbo_departments.Location = new System.Drawing.Point(698, 20);
            this.cbo_departments.Name = "cbo_departments";
            this.cbo_departments.Size = new System.Drawing.Size(102, 21);
            this.cbo_departments.TabIndex = 3;
            this.cbo_departments.SelectedIndexChanged += new System.EventHandler(this.cbo_departments_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(633, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Department";
            // 
            // btnclearfilter
            // 
            this.btnclearfilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnclearfilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclearfilter.Location = new System.Drawing.Point(1128, 19);
            this.btnclearfilter.Name = "btnclearfilter";
            this.btnclearfilter.Size = new System.Drawing.Size(82, 23);
            this.btnclearfilter.TabIndex = 7;
            this.btnclearfilter.Text = "clear filter";
            this.btnclearfilter.UseVisualStyleBackColor = false;
            this.btnclearfilter.Click += new System.EventHandler(this.btnclearfilter_Click);
            // 
            // btnexit
            // 
            this.btnexit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnexit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnexit.Location = new System.Drawing.Point(1225, 19);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(59, 23);
            this.btnexit.TabIndex = 8;
            this.btnexit.Text = "exit";
            this.btnexit.UseVisualStyleBackColor = false;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelPager);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 606);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1300, 56);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // panelPager
            // 
            this.panelPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPager.Location = new System.Drawing.Point(3, 16);
            this.panelPager.Name = "panelPager";
            this.panelPager.Size = new System.Drawing.Size(1294, 37);
            this.panelPager.TabIndex = 0;
            // 
            // dataGridView_payslip
            // 
            this.dataGridView_payslip.AllowUserToAddRows = false;
            this.dataGridView_payslip.AllowUserToDeleteRows = false;
            this.dataGridView_payslip.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_payslip.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_payslip.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2CompName,
            this.Column3EmpNo,
            this.Column1EmpName,
            this.Column1Year,
            this.Column2Period,
            this.Column4BasicPay,
            this.Column3GrossTaxableEarnings,
            this.Column4NetTaxableEarnings,
            this.Column6NetPay,
            this.Column1NSSF,
            this.ColumnSHIF,
            this.ColumnAHL,
            this.Column5GrossTax,
            this.Column2PayeTax,
            this.Column1OtherDeductions,
            this.Column1Department});
            this.dataGridView_payslip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_payslip.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_payslip.Name = "dataGridView_payslip";
            this.dataGridView_payslip.ReadOnly = true;
            this.dataGridView_payslip.Size = new System.Drawing.Size(1294, 528);
            this.dataGridView_payslip.TabIndex = 1;
            this.dataGridView_payslip.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_payslip_DataError);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView_payslip);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 59);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1300, 547);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
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
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn14.DataPropertyName = "PayeTax";
            this.dataGridViewTextBoxColumn14.HeaderText = "PayeTax";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Width = 70;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn15.DataPropertyName = "OtherDeductions";
            this.dataGridViewTextBoxColumn15.HeaderText = "Other Deductions";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.ReadOnly = true;
            // 
            // Column2CompName
            // 
            this.Column2CompName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2CompName.DataPropertyName = "CompName";
            this.Column2CompName.HeaderText = "Company Name";
            this.Column2CompName.Name = "Column2CompName";
            this.Column2CompName.ReadOnly = true;
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
            // Column1EmpName
            // 
            this.Column1EmpName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1EmpName.DataPropertyName = "EmpName";
            this.Column1EmpName.HeaderText = "Employee Name";
            this.Column1EmpName.Name = "Column1EmpName";
            this.Column1EmpName.ReadOnly = true;
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
            // Column4BasicPay
            // 
            this.Column4BasicPay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4BasicPay.DataPropertyName = "BasicPay";
            this.Column4BasicPay.HeaderText = "BasicPay";
            this.Column4BasicPay.Name = "Column4BasicPay";
            this.Column4BasicPay.ReadOnly = true;
            this.Column4BasicPay.Width = 70;
            // 
            // Column3GrossTaxableEarnings
            // 
            this.Column3GrossTaxableEarnings.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3GrossTaxableEarnings.DataPropertyName = "GrossTaxableEarnings";
            this.Column3GrossTaxableEarnings.HeaderText = "Gross Taxable Earnings";
            this.Column3GrossTaxableEarnings.Name = "Column3GrossTaxableEarnings";
            this.Column3GrossTaxableEarnings.ReadOnly = true;
            this.Column3GrossTaxableEarnings.Width = 90;
            // 
            // Column4NetTaxableEarnings
            // 
            this.Column4NetTaxableEarnings.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4NetTaxableEarnings.DataPropertyName = "NetTaxableEarnings";
            this.Column4NetTaxableEarnings.HeaderText = "Net Taxable Earnings";
            this.Column4NetTaxableEarnings.Name = "Column4NetTaxableEarnings";
            this.Column4NetTaxableEarnings.ReadOnly = true;
            this.Column4NetTaxableEarnings.Width = 90;
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
            // Column1NSSF
            // 
            this.Column1NSSF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1NSSF.DataPropertyName = "NSSF";
            this.Column1NSSF.HeaderText = "NSSF";
            this.Column1NSSF.Name = "Column1NSSF";
            this.Column1NSSF.ReadOnly = true;
            this.Column1NSSF.Width = 60;
            // 
            // ColumnSHIF
            // 
            this.ColumnSHIF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnSHIF.DataPropertyName = "SHIF";
            this.ColumnSHIF.HeaderText = "SHIF";
            this.ColumnSHIF.Name = "ColumnSHIF";
            this.ColumnSHIF.ReadOnly = true;
            this.ColumnSHIF.Width = 60;
            // 
            // ColumnAHL
            // 
            this.ColumnAHL.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnAHL.DataPropertyName = "AHL";
            this.ColumnAHL.HeaderText = "AHL";
            this.ColumnAHL.Name = "ColumnAHL";
            this.ColumnAHL.ReadOnly = true;
            this.ColumnAHL.Width = 60;
            // 
            // Column5GrossTax
            // 
            this.Column5GrossTax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column5GrossTax.DataPropertyName = "GrossTax";
            this.Column5GrossTax.HeaderText = "GrossTax";
            this.Column5GrossTax.Name = "Column5GrossTax";
            this.Column5GrossTax.ReadOnly = true;
            this.Column5GrossTax.Width = 80;
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
            // Column1OtherDeductions
            // 
            this.Column1OtherDeductions.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1OtherDeductions.DataPropertyName = "OtherDeductions";
            this.Column1OtherDeductions.HeaderText = "Other Deductions";
            this.Column1OtherDeductions.Name = "Column1OtherDeductions";
            this.Column1OtherDeductions.ReadOnly = true;
            this.Column1OtherDeductions.Width = 90;
            // 
            // Column1Department
            // 
            this.Column1Department.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1Department.DataPropertyName = "Department";
            this.Column1Department.HeaderText = "Department";
            this.Column1Department.Name = "Column1Department";
            this.Column1Department.ReadOnly = true;
            // 
            // payslip_master_temp_form
            // 
            this.AcceptButton = this.btndetails;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnexit;
            this.ClientSize = new System.Drawing.Size(1300, 662);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "payslip_master_temp_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payslip Master Temp";
            this.Load += new System.EventHandler(this.payslip_master_temp_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_payslip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_payslip)).EndInit();
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelPager;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbopayrollyears;
        private System.Windows.Forms.ComboBox cboemployeeno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbo_departments;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btndetails;
        private System.Windows.Forms.ComboBox cbo_employee_names;
        private System.Windows.Forms.ComboBox cbo_employers;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbopayrollperiods;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2CompName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3EmpNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2Period;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4BasicPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3GrossTaxableEarnings;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4NetTaxableEarnings;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6NetPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1NSSF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSHIF;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnAHL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5GrossTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2PayeTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1OtherDeductions;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1Department;
    }
}