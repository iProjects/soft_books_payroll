namespace winSBPayroll.Forms
{
    partial class EmployeeTransactionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmployeeTransactionsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkfor = new System.Windows.Forms.CheckBox();
            this.btnclearfilter = new System.Windows.Forms.Button();
            this.cboemployeeno = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbopayrollitem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnexit = new System.Windows.Forms.Button();
            this.btndetails = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView_employee_txn = new System.Windows.Forms.DataGridView();
            this.bindingSource_employee_txn = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1PostDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1EmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1Enabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2Recurrent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column3ItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7AccumulativePayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6IsDeleted = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_employee_txn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_employee_txn)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkEnabled);
            this.groupBox1.Controls.Add(this.chkfor);
            this.groupBox1.Controls.Add(this.btnclearfilter);
            this.groupBox1.Controls.Add(this.cboemployeeno);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbopayrollitem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnexit);
            this.groupBox1.Controls.Add(this.btndetails);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1346, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(625, 27);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 8;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // chkfor
            // 
            this.chkfor.AutoSize = true;
            this.chkfor.Location = new System.Drawing.Point(280, 27);
            this.chkfor.Name = "chkfor";
            this.chkfor.Size = new System.Drawing.Size(38, 17);
            this.chkfor.TabIndex = 7;
            this.chkfor.Text = "for";
            this.chkfor.UseVisualStyleBackColor = true;
            this.chkfor.CheckedChanged += new System.EventHandler(this.chkfor_CheckedChanged);
            // 
            // btnclearfilter
            // 
            this.btnclearfilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnclearfilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclearfilter.Location = new System.Drawing.Point(1146, 23);
            this.btnclearfilter.Name = "btnclearfilter";
            this.btnclearfilter.Size = new System.Drawing.Size(82, 23);
            this.btnclearfilter.TabIndex = 6;
            this.btnclearfilter.Text = "clear filter";
            this.btnclearfilter.UseVisualStyleBackColor = false;
            this.btnclearfilter.Click += new System.EventHandler(this.btnclearfilter_Click);
            // 
            // cboemployeeno
            // 
            this.cboemployeeno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboemployeeno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboemployeeno.FormattingEnabled = true;
            this.cboemployeeno.Location = new System.Drawing.Point(407, 25);
            this.cboemployeeno.Name = "cboemployeeno";
            this.cboemployeeno.Size = new System.Drawing.Size(175, 21);
            this.cboemployeeno.TabIndex = 5;
            this.cboemployeeno.SelectedIndexChanged += new System.EventHandler(this.cboemployeeno_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(337, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "employee no";
            // 
            // cbopayrollitem
            // 
            this.cbopayrollitem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopayrollitem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbopayrollitem.FormattingEnabled = true;
            this.cbopayrollitem.Location = new System.Drawing.Point(77, 25);
            this.cbopayrollitem.Name = "cbopayrollitem";
            this.cbopayrollitem.Size = new System.Drawing.Size(175, 21);
            this.cbopayrollitem.TabIndex = 3;
            this.cbopayrollitem.SelectedIndexChanged += new System.EventHandler(this.cbopayrollitem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "payroll item";
            // 
            // btnexit
            // 
            this.btnexit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnexit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnexit.Location = new System.Drawing.Point(1243, 23);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(59, 23);
            this.btnexit.TabIndex = 1;
            this.btnexit.Text = "exit";
            this.btnexit.UseVisualStyleBackColor = false;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btndetails
            // 
            this.btndetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btndetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndetails.Location = new System.Drawing.Point(1072, 23);
            this.btndetails.Name = "btndetails";
            this.btndetails.Size = new System.Drawing.Size(59, 23);
            this.btndetails.TabIndex = 0;
            this.btndetails.Text = "details";
            this.btndetails.UseVisualStyleBackColor = false;
            this.btndetails.Click += new System.EventHandler(this.btndetails_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView_employee_txn);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1346, 527);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridView_employee_txn
            // 
            this.dataGridView_employee_txn.AllowUserToAddRows = false;
            this.dataGridView_employee_txn.AllowUserToDeleteRows = false;
            this.dataGridView_employee_txn.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView_employee_txn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_employee_txn.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1Id,
            this.Column1PostDate,
            this.Column1EmpNo,
            this.Column1Enabled,
            this.Column2Recurrent,
            this.Column3ItemId,
            this.Column4Amount,
            this.Column5Balance,
            this.Column7AccumulativePayment,
            this.Column6IsDeleted});
            this.dataGridView_employee_txn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_employee_txn.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_employee_txn.Name = "dataGridView_employee_txn";
            this.dataGridView_employee_txn.ReadOnly = true;
            this.dataGridView_employee_txn.Size = new System.Drawing.Size(1340, 508);
            this.dataGridView_employee_txn.TabIndex = 0;
            this.dataGridView_employee_txn.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_employee_txn_CellContentDoubleClick);
            this.dataGridView_employee_txn.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_employee_txn_DataError);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "PostDate";
            this.dataGridViewTextBoxColumn2.HeaderText = "PostDate";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
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
            // Column1Id
            // 
            this.Column1Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1Id.DataPropertyName = "Id";
            this.Column1Id.HeaderText = "Id";
            this.Column1Id.Name = "Column1Id";
            this.Column1Id.ReadOnly = true;
            this.Column1Id.Width = 50;
            // 
            // Column1PostDate
            // 
            this.Column1PostDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1PostDate.DataPropertyName = "PostDate";
            this.Column1PostDate.HeaderText = "PostDate";
            this.Column1PostDate.Name = "Column1PostDate";
            this.Column1PostDate.ReadOnly = true;
            // 
            // Column1EmpNo
            // 
            this.Column1EmpNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1EmpNo.DataPropertyName = "EmpNo";
            this.Column1EmpNo.HeaderText = "EmpNo";
            this.Column1EmpNo.Name = "Column1EmpNo";
            this.Column1EmpNo.ReadOnly = true;
            this.Column1EmpNo.Width = 70;
            // 
            // Column1Enabled
            // 
            this.Column1Enabled.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1Enabled.DataPropertyName = "Enabled";
            this.Column1Enabled.HeaderText = "Enabled";
            this.Column1Enabled.Name = "Column1Enabled";
            this.Column1Enabled.ReadOnly = true;
            this.Column1Enabled.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1Enabled.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2Recurrent
            // 
            this.Column2Recurrent.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2Recurrent.DataPropertyName = "Recurrent";
            this.Column2Recurrent.HeaderText = "Recurrent";
            this.Column2Recurrent.Name = "Column2Recurrent";
            this.Column2Recurrent.ReadOnly = true;
            this.Column2Recurrent.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2Recurrent.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column3ItemId
            // 
            this.Column3ItemId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3ItemId.DataPropertyName = "ItemId";
            this.Column3ItemId.HeaderText = "ItemId";
            this.Column3ItemId.Name = "Column3ItemId";
            this.Column3ItemId.ReadOnly = true;
            // 
            // Column4Amount
            // 
            this.Column4Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4Amount.DataPropertyName = "Amount";
            this.Column4Amount.HeaderText = "Amount";
            this.Column4Amount.Name = "Column4Amount";
            this.Column4Amount.ReadOnly = true;
            // 
            // Column5Balance
            // 
            this.Column5Balance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column5Balance.DataPropertyName = "Balance";
            this.Column5Balance.HeaderText = "Balance";
            this.Column5Balance.Name = "Column5Balance";
            this.Column5Balance.ReadOnly = true;
            // 
            // Column7AccumulativePayment
            // 
            this.Column7AccumulativePayment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column7AccumulativePayment.DataPropertyName = "AccumulativePayment";
            this.Column7AccumulativePayment.HeaderText = "Accumulative Payment";
            this.Column7AccumulativePayment.Name = "Column7AccumulativePayment";
            this.Column7AccumulativePayment.ReadOnly = true;
            // 
            // Column6IsDeleted
            // 
            this.Column6IsDeleted.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column6IsDeleted.DataPropertyName = "IsDeleted";
            this.Column6IsDeleted.HeaderText = "Is Deleted";
            this.Column6IsDeleted.Name = "Column6IsDeleted";
            this.Column6IsDeleted.ReadOnly = true;
            this.Column6IsDeleted.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column6IsDeleted.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // EmployeeTransactionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(1346, 583);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EmployeeTransactionsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Employees Transactions";
            this.Load += new System.EventHandler(this.EmployeeTransactionsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_employee_txn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_employee_txn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btndetails;
        private System.Windows.Forms.BindingSource bindingSource_employee_txn;
        private System.Windows.Forms.ComboBox cbopayrollitem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnclearfilter;
        private System.Windows.Forms.ComboBox cboemployeeno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView_employee_txn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.CheckBox chkfor;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1PostDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1EmpNo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1Enabled;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2Recurrent;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3ItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5Balance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7AccumulativePayment;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6IsDeleted;
    }
}