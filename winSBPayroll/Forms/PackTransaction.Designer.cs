namespace winSBPayroll.Forms
{
    partial class PackTransaction
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
            System.Windows.Forms.Label postDateLabel;
            System.Windows.Forms.Label empNoLabel;
            System.Windows.Forms.Label txnCodeLabel;
            System.Windows.Forms.Label amountLabel;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PackTransaction));
            this.employeeTransactionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.transactionTypesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPageAdd = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelect = new System.Windows.Forms.Button();
            this.cbTxnCode = new System.Windows.Forms.ComboBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.txtEmployeeNos = new System.Windows.Forms.TextBox();
            this.dtpPostDate = new System.Windows.Forms.DateTimePicker();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageList = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.EmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TxnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PackDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreatedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Authorized = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPageUpload = new System.Windows.Forms.TabPage();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtUpload = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            postDateLabel = new System.Windows.Forms.Label();
            empNoLabel = new System.Windows.Forms.Label();
            txnCodeLabel = new System.Windows.Forms.Label();
            amountLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.employeeTransactionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionTypesBindingSource)).BeginInit();
            this.tabPageAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPageUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // postDateLabel
            // 
            postDateLabel.AutoSize = true;
            postDateLabel.Location = new System.Drawing.Point(48, 40);
            postDateLabel.Name = "postDateLabel";
            postDateLabel.Size = new System.Drawing.Size(57, 13);
            postDateLabel.TabIndex = 17;
            postDateLabel.Text = "Post Date:";
            // 
            // empNoLabel
            // 
            empNoLabel.AutoSize = true;
            empNoLabel.Location = new System.Drawing.Point(49, 82);
            empNoLabel.Name = "empNoLabel";
            empNoLabel.Size = new System.Drawing.Size(56, 13);
            empNoLabel.TabIndex = 21;
            empNoLabel.Text = "Employee:";
            // 
            // txnCodeLabel
            // 
            txnCodeLabel.AutoSize = true;
            txnCodeLabel.Location = new System.Drawing.Point(49, 124);
            txnCodeLabel.Name = "txnCodeLabel";
            txnCodeLabel.Size = new System.Drawing.Size(56, 13);
            txnCodeLabel.TabIndex = 25;
            txnCodeLabel.Text = "Txn Code:";
            // 
            // amountLabel
            // 
            amountLabel.AutoSize = true;
            amountLabel.Location = new System.Drawing.Point(59, 166);
            amountLabel.Name = "amountLabel";
            amountLabel.Size = new System.Drawing.Size(46, 13);
            amountLabel.TabIndex = 27;
            amountLabel.Text = "Amount:";
            // 
            // employeeTransactionsBindingSource
            // 
            this.employeeTransactionsBindingSource.DataMember = "EmployeeTransactions";
            // 
            // transactionTypesBindingSource
            // 
            this.transactionTypesBindingSource.DataMember = "TransactionTypes";
            // 
            // tabPageAdd
            // 
            this.tabPageAdd.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageAdd.Controls.Add(this.label1);
            this.tabPageAdd.Controls.Add(this.btnSelect);
            this.tabPageAdd.Controls.Add(this.cbTxnCode);
            this.tabPageAdd.Controls.Add(this.txtEmployeeNos);
            this.tabPageAdd.Controls.Add(this.dtpPostDate);
            this.tabPageAdd.Controls.Add(this.btnAdd);
            this.tabPageAdd.Controls.Add(postDateLabel);
            this.tabPageAdd.Controls.Add(empNoLabel);
            this.tabPageAdd.Controls.Add(txnCodeLabel);
            this.tabPageAdd.Controls.Add(amountLabel);
            this.tabPageAdd.Controls.Add(this.txtAmount);
            this.tabPageAdd.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdd.Name = "tabPageAdd";
            this.tabPageAdd.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdd.Size = new System.Drawing.Size(695, 241);
            this.tabPageAdd.TabIndex = 0;
            this.tabPageAdd.Text = "Add Transactions";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "If a payroll item already exists, it will be overwritten";
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Location = new System.Drawing.Point(298, 74);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 2;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // cbTxnCode
            // 
            this.cbTxnCode.DataSource = this.bindingSource1;
            this.cbTxnCode.DisplayMember = "TxnCode";
            this.cbTxnCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTxnCode.FormattingEnabled = true;
            this.cbTxnCode.Location = new System.Drawing.Point(111, 118);
            this.cbTxnCode.Name = "cbTxnCode";
            this.cbTxnCode.Size = new System.Drawing.Size(172, 21);
            this.cbTxnCode.TabIndex = 3;
            this.cbTxnCode.ValueMember = "TxnCode";
            this.cbTxnCode.SelectedIndexChanged += new System.EventHandler(this.cbTxnCode_SelectedIndexChanged);
            // 
            // txtEmployeeNos
            // 
            this.txtEmployeeNos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmployeeNos.Enabled = false;
            this.txtEmployeeNos.Location = new System.Drawing.Point(111, 77);
            this.txtEmployeeNos.MaxLength = 30;
            this.txtEmployeeNos.Name = "txtEmployeeNos";
            this.txtEmployeeNos.Size = new System.Drawing.Size(172, 20);
            this.txtEmployeeNos.TabIndex = 1;
            // 
            // dtpPostDate
            // 
            this.dtpPostDate.Location = new System.Drawing.Point(111, 36);
            this.dtpPostDate.Name = "dtpPostDate";
            this.dtpPostDate.Size = new System.Drawing.Size(172, 20);
            this.dtpPostDate.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(156, 198);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAmount
            // 
            this.txtAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmount.Location = new System.Drawing.Point(111, 160);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(172, 20);
            this.txtAmount.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(604, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageList);
            this.tabControl1.Controls.Add(this.tabPageAdd);
            this.tabControl1.Controls.Add(this.tabPageUpload);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(703, 303);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageList
            // 
            this.tabPageList.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageList.Controls.Add(this.dataGridView1);
            this.tabPageList.Location = new System.Drawing.Point(4, 22);
            this.tabPageList.Name = "tabPageList";
            this.tabPageList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageList.Size = new System.Drawing.Size(695, 277);
            this.tabPageList.TabIndex = 2;
            this.tabPageList.Text = "Transactions List";
            // 
            // dataGridViewTaxCalculator
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.PaleGreen;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmpNo,
            this.TxnCode,
            this.Amount,
            this.PackDate,
            this.CreatedBy,
            this.Authorized});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(689, 271);
            this.dataGridView1.TabIndex = 0;
            // 
            // EmpNo
            // 
            this.EmpNo.DataPropertyName = "EmpNo";
            this.EmpNo.HeaderText = "EmpNo";
            this.EmpNo.Name = "EmpNo";
            this.EmpNo.ReadOnly = true;
            // 
            // TxnCode
            // 
            this.TxnCode.DataPropertyName = "TxnCode";
            this.TxnCode.HeaderText = "Txn  Code";
            this.TxnCode.Name = "TxnCode";
            this.TxnCode.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.Amount.DefaultCellStyle = dataGridViewCellStyle2;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // PackDate
            // 
            this.PackDate.DataPropertyName = "PackDate";
            this.PackDate.HeaderText = "Pack Date";
            this.PackDate.Name = "PackDate";
            this.PackDate.ReadOnly = true;
            // 
            // CreatedBy
            // 
            this.CreatedBy.DataPropertyName = "CreateBy";
            this.CreatedBy.HeaderText = "Created By";
            this.CreatedBy.Name = "CreatedBy";
            this.CreatedBy.ReadOnly = true;
            // 
            // Authorized
            // 
            this.Authorized.DataPropertyName = "Authorized";
            this.Authorized.HeaderText = "Authorized";
            this.Authorized.Name = "Authorized";
            this.Authorized.ReadOnly = true;
            // 
            // tabPageUpload
            // 
            this.tabPageUpload.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageUpload.Controls.Add(this.btnUpload);
            this.tabPageUpload.Controls.Add(this.btnBrowse);
            this.tabPageUpload.Controls.Add(this.txtUpload);
            this.tabPageUpload.Controls.Add(this.label2);
            this.tabPageUpload.Location = new System.Drawing.Point(4, 22);
            this.tabPageUpload.Name = "tabPageUpload";
            this.tabPageUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUpload.Size = new System.Drawing.Size(695, 241);
            this.tabPageUpload.TabIndex = 1;
            this.tabPageUpload.Text = "Upload";
            // 
            // btnUpload
            // 
            this.btnUpload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnUpload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload.Location = new System.Drawing.Point(29, 108);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 2;
            this.btnUpload.Text = "&Upload";
            this.btnUpload.UseVisualStyleBackColor = false;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(409, 68);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "&Browse";
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtUpload
            // 
            this.txtUpload.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpload.Location = new System.Drawing.Point(29, 71);
            this.txtUpload.Name = "txtUpload";
            this.txtUpload.Size = new System.Drawing.Size(374, 20);
            this.txtUpload.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(348, 39);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select the Excel file to upload from. Please make sure the fields are valid\r\nand " +
    "are in the following format - EmpNo, TxnCode, Amount\r\n\r\n";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 322);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(709, 51);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(709, 322);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // PackTransaction
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(709, 373);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PackTransaction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pack Transactions";
            this.Load += new System.EventHandler(this.PackTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.employeeTransactionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transactionTypesBindingSource)).EndInit();
            this.tabPageAdd.ResumeLayout(false);
            this.tabPageAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPageUpload.ResumeLayout(false);
            this.tabPageUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource employeeTransactionsBindingSource;
        private System.Windows.Forms.BindingSource transactionTypesBindingSource;
        private System.Windows.Forms.TabPage tabPageAdd;
        private System.Windows.Forms.ComboBox cbTxnCode;
        private System.Windows.Forms.TextBox txtEmployeeNos;
        private System.Windows.Forms.DateTimePicker dtpPostDate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageUpload;
        private System.Windows.Forms.TabPage tabPageList;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtUpload;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TxnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PackDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreatedBy;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Authorized;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}