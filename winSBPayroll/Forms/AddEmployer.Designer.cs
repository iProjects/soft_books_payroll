namespace winSBPayroll.Forms
{
    partial class AddEmployer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEmployer));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageGeneralDetails = new System.Windows.Forms.TabPage();
            this.txtSlogan = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtLogo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNSSF = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNHIF = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPIN = new System.Windows.Forms.TextBox();
            this.NHIF = new System.Windows.Forms.Label();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.NSSF = new System.Windows.Forms.Label();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.tabPageBankingDetails = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridViewEmployerBank = new System.Windows.Forms.DataGridView();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpNHIF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpNSSF = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIsDefault = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkIsDefault = new System.Windows.Forms.CheckBox();
            this.btnAddBank = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtBankSortCode = new System.Windows.Forms.TextBox();
            this.lblBankDetails = new System.Windows.Forms.Label();
            this.btnSearchBank = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAuthSign = new System.Windows.Forms.TextBox();
            this.txtAccountNo = new System.Windows.Forms.TextBox();
            this.bindingSourceBank = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceEmployerBank = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageGeneralDetails.SuspendLayout();
            this.tabPageBankingDetails.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployerBank)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEmployerBank)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(138, 9);
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
            this.btnClose.Location = new System.Drawing.Point(274, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 419);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 38);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 419);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Validating += new System.ComponentModel.CancelEventHandler(this.groupBox2_Validating);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageGeneralDetails);
            this.tabControl1.Controls.Add(this.tabPageBankingDetails);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(494, 400);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPageGeneralDetails
            // 
            this.tabPageGeneralDetails.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageGeneralDetails.Controls.Add(this.txtSlogan);
            this.tabPageGeneralDetails.Controls.Add(this.label12);
            this.tabPageGeneralDetails.Controls.Add(this.btnBrowse);
            this.tabPageGeneralDetails.Controls.Add(this.txtLogo);
            this.tabPageGeneralDetails.Controls.Add(this.label7);
            this.tabPageGeneralDetails.Controls.Add(this.txtAddress1);
            this.tabPageGeneralDetails.Controls.Add(this.label1);
            this.tabPageGeneralDetails.Controls.Add(this.label2);
            this.tabPageGeneralDetails.Controls.Add(this.label3);
            this.tabPageGeneralDetails.Controls.Add(this.txtNSSF);
            this.tabPageGeneralDetails.Controls.Add(this.label4);
            this.tabPageGeneralDetails.Controls.Add(this.txtNHIF);
            this.tabPageGeneralDetails.Controls.Add(this.label5);
            this.tabPageGeneralDetails.Controls.Add(this.label6);
            this.tabPageGeneralDetails.Controls.Add(this.txtEmail);
            this.tabPageGeneralDetails.Controls.Add(this.txtPIN);
            this.tabPageGeneralDetails.Controls.Add(this.NHIF);
            this.tabPageGeneralDetails.Controls.Add(this.txtTelephone);
            this.tabPageGeneralDetails.Controls.Add(this.NSSF);
            this.tabPageGeneralDetails.Controls.Add(this.txtAddress2);
            this.tabPageGeneralDetails.Controls.Add(this.txtName);
            this.tabPageGeneralDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneralDetails.Name = "tabPageGeneralDetails";
            this.tabPageGeneralDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneralDetails.Size = new System.Drawing.Size(486, 374);
            this.tabPageGeneralDetails.TabIndex = 0;
            this.tabPageGeneralDetails.Text = "General";
            // 
            // txtSlogan
            // 
            this.txtSlogan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSlogan.Location = new System.Drawing.Point(91, 329);
            this.txtSlogan.MaxLength = 200;
            this.txtSlogan.Name = "txtSlogan";
            this.txtSlogan.Size = new System.Drawing.Size(307, 20);
            this.txtSlogan.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(45, 332);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Slogan*";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(362, 279);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(36, 23);
            this.btnBrowse.TabIndex = 11;
            this.btnBrowse.Text = ":::";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtLogo
            // 
            this.txtLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLogo.Location = new System.Drawing.Point(91, 273);
            this.txtLogo.MaxLength = 200;
            this.txtLogo.Multiline = true;
            this.txtLogo.Name = "txtLogo";
            this.txtLogo.Size = new System.Drawing.Size(251, 50);
            this.txtLogo.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(54, 283);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Logo*";
            // 
            // txtAddress1
            // 
            this.txtAddress1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress1.Location = new System.Drawing.Point(91, 55);
            this.txtAddress1.MaxLength = 200;
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(307, 20);
            this.txtAddress1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Address1*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Address2*";
            // 
            // txtNSSF
            // 
            this.txtNSSF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNSSF.Location = new System.Drawing.Point(91, 247);
            this.txtNSSF.MaxLength = 200;
            this.txtNSSF.Name = "txtNSSF";
            this.txtNSSF.Size = new System.Drawing.Size(307, 20);
            this.txtNSSF.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Telephone*";
            // 
            // txtNHIF
            // 
            this.txtNHIF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNHIF.Location = new System.Drawing.Point(91, 215);
            this.txtNHIF.MaxLength = 200;
            this.txtNHIF.Name = "txtNHIF";
            this.txtNHIF.Size = new System.Drawing.Size(307, 20);
            this.txtNHIF.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "PIN*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Email*";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Location = new System.Drawing.Point(91, 183);
            this.txtEmail.MaxLength = 200;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(307, 20);
            this.txtEmail.TabIndex = 5;
            // 
            // txtPIN
            // 
            this.txtPIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPIN.Location = new System.Drawing.Point(91, 151);
            this.txtPIN.MaxLength = 200;
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.Size = new System.Drawing.Size(307, 20);
            this.txtPIN.TabIndex = 4;
            // 
            // NHIF
            // 
            this.NHIF.AutoSize = true;
            this.NHIF.Location = new System.Drawing.Point(36, 218);
            this.NHIF.Name = "NHIF";
            this.NHIF.Size = new System.Drawing.Size(53, 13);
            this.NHIF.TabIndex = 7;
            this.NHIF.Text = "NHIF No*";
            // 
            // txtTelephone
            // 
            this.txtTelephone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTelephone.Location = new System.Drawing.Point(91, 119);
            this.txtTelephone.MaxLength = 200;
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(307, 20);
            this.txtTelephone.TabIndex = 3;
            // 
            // NSSF
            // 
            this.NSSF.AutoSize = true;
            this.NSSF.Location = new System.Drawing.Point(33, 250);
            this.NSSF.Name = "NSSF";
            this.NSSF.Size = new System.Drawing.Size(56, 13);
            this.NSSF.TabIndex = 8;
            this.NSSF.Text = "NSSF No*";
            // 
            // txtAddress2
            // 
            this.txtAddress2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress2.Location = new System.Drawing.Point(91, 87);
            this.txtAddress2.MaxLength = 200;
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(307, 20);
            this.txtAddress2.TabIndex = 2;
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(91, 23);
            this.txtName.MaxLength = 200;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(307, 20);
            this.txtName.TabIndex = 0;
            // 
            // tabPageBankingDetails
            // 
            this.tabPageBankingDetails.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageBankingDetails.Controls.Add(this.groupBox3);
            this.tabPageBankingDetails.Controls.Add(this.groupBox4);
            this.tabPageBankingDetails.Location = new System.Drawing.Point(4, 22);
            this.tabPageBankingDetails.Name = "tabPageBankingDetails";
            this.tabPageBankingDetails.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBankingDetails.Size = new System.Drawing.Size(486, 374);
            this.tabPageBankingDetails.TabIndex = 1;
            this.tabPageBankingDetails.Text = "Banking";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridViewEmployerBank);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 202);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(480, 169);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // dataGridViewEmployerBank
            // 
            this.dataGridViewEmployerBank.AllowUserToAddRows = false;
            this.dataGridViewEmployerBank.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridViewEmployerBank.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEmployerBank.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEmployerBank.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewEmployerBank.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployerBank.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.EmpEmail,
            this.EmpNHIF,
            this.EmpNSSF,
            this.ColumnIsDefault});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewEmployerBank.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewEmployerBank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEmployerBank.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewEmployerBank.Name = "dataGridViewEmployerBank";
            this.dataGridViewEmployerBank.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewEmployerBank.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewEmployerBank.Size = new System.Drawing.Size(474, 150);
            this.dataGridViewEmployerBank.TabIndex = 0;
            this.dataGridViewEmployerBank.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewEmployerBank_DataError);
            // 
            // ColumnId
            // 
            this.ColumnId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnId.DataPropertyName = "EmployerBankId";
            this.ColumnId.HeaderText = "Id";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            this.ColumnId.Width = 20;
            // 
            // EmpEmail
            // 
            this.EmpEmail.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EmpEmail.DataPropertyName = "AccountName";
            this.EmpEmail.HeaderText = "Account_Name";
            this.EmpEmail.Name = "EmpEmail";
            this.EmpEmail.ReadOnly = true;
            // 
            // EmpNHIF
            // 
            this.EmpNHIF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EmpNHIF.DataPropertyName = "AccountNo";
            this.EmpNHIF.HeaderText = "Account_No";
            this.EmpNHIF.Name = "EmpNHIF";
            this.EmpNHIF.ReadOnly = true;
            this.EmpNHIF.Width = 80;
            // 
            // EmpNSSF
            // 
            this.EmpNSSF.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EmpNSSF.DataPropertyName = "Signatory";
            this.EmpNSSF.HeaderText = "Signatory";
            this.EmpNSSF.Name = "EmpNSSF";
            this.EmpNSSF.ReadOnly = true;
            this.EmpNSSF.Width = 80;
            // 
            // ColumnIsDefault
            // 
            this.ColumnIsDefault.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnIsDefault.DataPropertyName = "IsDefault";
            this.ColumnIsDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnIsDefault.HeaderText = "IsDefault";
            this.ColumnIsDefault.Name = "ColumnIsDefault";
            this.ColumnIsDefault.ReadOnly = true;
            this.ColumnIsDefault.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnIsDefault.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkIsDefault);
            this.groupBox4.Controls.Add(this.btnAddBank);
            this.groupBox4.Controls.Add(this.btnDelete);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txtBankSortCode);
            this.groupBox4.Controls.Add(this.lblBankDetails);
            this.groupBox4.Controls.Add(this.btnSearchBank);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txtAccountName);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtAuthSign);
            this.groupBox4.Controls.Add(this.txtAccountNo);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(480, 199);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // chkIsDefault
            // 
            this.chkIsDefault.AutoSize = true;
            this.chkIsDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIsDefault.Location = new System.Drawing.Point(120, 146);
            this.chkIsDefault.Name = "chkIsDefault";
            this.chkIsDefault.Size = new System.Drawing.Size(57, 17);
            this.chkIsDefault.TabIndex = 56;
            this.chkIsDefault.Text = "Default";
            this.chkIsDefault.UseVisualStyleBackColor = true;
            // 
            // btnAddBank
            // 
            this.btnAddBank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAddBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddBank.Location = new System.Drawing.Point(142, 168);
            this.btnAddBank.Name = "btnAddBank";
            this.btnAddBank.Size = new System.Drawing.Size(59, 23);
            this.btnAddBank.TabIndex = 5;
            this.btnAddBank.Text = "Add";
            this.btnAddBank.UseVisualStyleBackColor = false;
            this.btnAddBank.Click += new System.EventHandler(this.btnAddBank_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(230, 168);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(59, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(108, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "Authorized Signatory*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Account No*";
            // 
            // txtBankSortCode
            // 
            this.txtBankSortCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBankSortCode.Location = new System.Drawing.Point(120, 25);
            this.txtBankSortCode.MaxLength = 200;
            this.txtBankSortCode.Name = "txtBankSortCode";
            this.txtBankSortCode.Size = new System.Drawing.Size(141, 20);
            this.txtBankSortCode.TabIndex = 0;
            this.txtBankSortCode.TabIndexChanged += new System.EventHandler(this.txtBankSortCode_TabIndexChanged);
            this.txtBankSortCode.TextChanged += new System.EventHandler(this.txtBankSortCode_TextChanged);
            this.txtBankSortCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBankSortCode_KeyDown);
            this.txtBankSortCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBankSortCode_KeyPress);
            this.txtBankSortCode.Leave += new System.EventHandler(this.txtBankSortCode_Leave);
            this.txtBankSortCode.Validated += new System.EventHandler(this.txtBankSortCode_Validated);
            // 
            // lblBankDetails
            // 
            this.lblBankDetails.AutoSize = true;
            this.lblBankDetails.Location = new System.Drawing.Point(28, 51);
            this.lblBankDetails.Name = "lblBankDetails";
            this.lblBankDetails.Size = new System.Drawing.Size(22, 13);
            this.lblBankDetails.TabIndex = 54;
            this.lblBankDetails.Text = ":::::";
            // 
            // btnSearchBank
            // 
            this.btnSearchBank.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSearchBank.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearchBank.Location = new System.Drawing.Point(329, 24);
            this.btnSearchBank.Name = "btnSearchBank";
            this.btnSearchBank.Size = new System.Drawing.Size(36, 23);
            this.btnSearchBank.TabIndex = 4;
            this.btnSearchBank.Text = ":::";
            this.btnSearchBank.UseVisualStyleBackColor = false;
            this.btnSearchBank.Click += new System.EventHandler(this.btnSearchBank_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Account Name*";
            // 
            // txtAccountName
            // 
            this.txtAccountName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountName.Location = new System.Drawing.Point(120, 69);
            this.txtAccountName.MaxLength = 200;
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(270, 20);
            this.txtAccountName.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 28);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 13);
            this.label8.TabIndex = 53;
            this.label8.Text = "Bank Sort Code*";
            // 
            // txtAuthSign
            // 
            this.txtAuthSign.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAuthSign.Location = new System.Drawing.Point(120, 121);
            this.txtAuthSign.MaxLength = 200;
            this.txtAuthSign.Name = "txtAuthSign";
            this.txtAuthSign.Size = new System.Drawing.Size(270, 20);
            this.txtAuthSign.TabIndex = 3;
            // 
            // txtAccountNo
            // 
            this.txtAccountNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAccountNo.Location = new System.Drawing.Point(120, 95);
            this.txtAccountNo.MaxLength = 200;
            this.txtAccountNo.Name = "txtAccountNo";
            this.txtAccountNo.Size = new System.Drawing.Size(270, 20);
            this.txtAccountNo.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 20;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "AccountName";
            this.dataGridViewTextBoxColumn2.HeaderText = "AccountName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "AccountNo";
            this.dataGridViewTextBoxColumn3.HeaderText = "AccountNo";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Signatory";
            this.dataGridViewTextBoxColumn4.HeaderText = "Signatory";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // AddEmployer
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(500, 457);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddEmployer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add  Employer";
            this.Load += new System.EventHandler(this.AddEmployer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageGeneralDetails.ResumeLayout(false);
            this.tabPageGeneralDetails.PerformLayout();
            this.tabPageBankingDetails.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployerBank)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEmployerBank)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.BindingSource bindingSourceBank;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageGeneralDetails;
        private System.Windows.Forms.TextBox txtAddress1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNSSF;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNHIF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPIN;
        private System.Windows.Forms.Label NHIF;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.Label NSSF;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TabPage tabPageBankingDetails;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridViewEmployerBank;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnAddBank;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtBankSortCode;
        private System.Windows.Forms.Label lblBankDetails;
        private System.Windows.Forms.Button btnSearchBank;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAuthSign;
        private System.Windows.Forms.TextBox txtAccountNo;
        private System.Windows.Forms.BindingSource bindingSourceEmployerBank;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.TextBox txtLogo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.CheckBox chkIsDefault;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpEmail;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpNHIF;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpNSSF;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnIsDefault;
        private System.Windows.Forms.TextBox txtSlogan;
        private System.Windows.Forms.Label label12;
    }
}