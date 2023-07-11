namespace winSBPayroll.Forms
{
    partial class TransactionDefinition
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
            System.Windows.Forms.Label txnCodeLabel;
            System.Windows.Forms.Label amountLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TransactionDefinition));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageList = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.TxnCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataEntry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PayrollItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DefaultAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransDefEnabled = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Recurrent = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.TrackYTD = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPageEntry = new System.Windows.Forms.TabPage();
            this.btnAdd = new System.Windows.Forms.Button();
            this.grpDataEntry = new System.Windows.Forms.GroupBox();
            this.rbDelete = new System.Windows.Forms.RadioButton();
            this.rbAddUpdate = new System.Windows.Forms.RadioButton();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cbPayrollItem = new System.Windows.Forms.ComboBox();
            this.txtTxncode = new System.Windows.Forms.TextBox();
            this.trackCheckBox = new System.Windows.Forms.CheckBox();
            this.enabledCheckBox = new System.Windows.Forms.CheckBox();
            this.recurrentCheckBox = new System.Windows.Forms.CheckBox();
            this.amountTextBox = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            txnCodeLabel = new System.Windows.Forms.Label();
            amountLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPageList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPageEntry.SuspendLayout();
            this.grpDataEntry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txnCodeLabel
            // 
            txnCodeLabel.AutoSize = true;
            txnCodeLabel.Enabled = false;
            txnCodeLabel.Location = new System.Drawing.Point(103, 34);
            txnCodeLabel.Name = "txnCodeLabel";
            txnCodeLabel.Size = new System.Drawing.Size(56, 13);
            txnCodeLabel.TabIndex = 42;
            txnCodeLabel.Text = "Txn Code:";
            // 
            // amountLabel
            // 
            amountLabel.AutoSize = true;
            amountLabel.Location = new System.Drawing.Point(76, 154);
            amountLabel.Name = "amountLabel";
            amountLabel.Size = new System.Drawing.Size(83, 13);
            amountLabel.TabIndex = 43;
            amountLabel.Text = "Default Amount:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(95, 121);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(64, 13);
            label1.TabIndex = 55;
            label1.Text = "Payroll Item:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageList);
            this.tabControl1.Controls.Add(this.tabPageEntry);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(974, 378);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageList
            // 
            this.tabPageList.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageList.Controls.Add(this.dataGridView1);
            this.tabPageList.Location = new System.Drawing.Point(4, 22);
            this.tabPageList.Name = "tabPageList";
            this.tabPageList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageList.Size = new System.Drawing.Size(966, 352);
            this.tabPageList.TabIndex = 0;
            this.tabPageList.Text = "Defined Transactions";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightGreen;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TxnCode,
            this.DataEntry,
            this.PayrollItem,
            this.DefaultAmount,
            this.TransDefEnabled,
            this.Recurrent,
            this.TrackYTD});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(960, 346);
            this.dataGridView1.TabIndex = 0;
            // 
            // TxnCode
            // 
            this.TxnCode.DataPropertyName = "TxnCode";
            this.TxnCode.HeaderText = "Transaction Code";
            this.TxnCode.Name = "TxnCode";
            this.TxnCode.ReadOnly = true;
            // 
            // DataEntry
            // 
            this.DataEntry.DataPropertyName = "DataEntry";
            this.DataEntry.HeaderText = "Data Entry";
            this.DataEntry.Name = "DataEntry";
            this.DataEntry.ReadOnly = true;
            // 
            // PayrollItem
            // 
            this.PayrollItem.DataPropertyName = "PayrollItem";
            this.PayrollItem.HeaderText = "Payroll Item";
            this.PayrollItem.Name = "PayrollItem";
            this.PayrollItem.ReadOnly = true;
            // 
            // DefaultAmount
            // 
            this.DefaultAmount.DataPropertyName = "DefaultAmount";
            dataGridViewCellStyle2.Format = "C2";
            dataGridViewCellStyle2.NullValue = null;
            this.DefaultAmount.DefaultCellStyle = dataGridViewCellStyle2;
            this.DefaultAmount.HeaderText = "Default Amount";
            this.DefaultAmount.Name = "DefaultAmount";
            this.DefaultAmount.ReadOnly = true;
            // 
            // TransDefEnabled
            // 
            this.TransDefEnabled.DataPropertyName = "Enabled";
            this.TransDefEnabled.HeaderText = "Enabled";
            this.TransDefEnabled.Name = "TransDefEnabled";
            this.TransDefEnabled.ReadOnly = true;
            // 
            // Recurrent
            // 
            this.Recurrent.DataPropertyName = "Recurrent";
            this.Recurrent.HeaderText = "Recurrent";
            this.Recurrent.Name = "Recurrent";
            this.Recurrent.ReadOnly = true;
            // 
            // TrackYTD
            // 
            this.TrackYTD.DataPropertyName = "TrackYTD";
            this.TrackYTD.HeaderText = "TrackYTD";
            this.TrackYTD.Name = "TrackYTD";
            this.TrackYTD.ReadOnly = true;
            // 
            // tabPageEntry
            // 
            this.tabPageEntry.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageEntry.Controls.Add(this.btnAdd);
            this.tabPageEntry.Controls.Add(this.grpDataEntry);
            this.tabPageEntry.Controls.Add(this.btnUpdate);
            this.tabPageEntry.Controls.Add(label1);
            this.tabPageEntry.Controls.Add(this.cbPayrollItem);
            this.tabPageEntry.Controls.Add(this.txtTxncode);
            this.tabPageEntry.Controls.Add(this.trackCheckBox);
            this.tabPageEntry.Controls.Add(this.enabledCheckBox);
            this.tabPageEntry.Controls.Add(this.recurrentCheckBox);
            this.tabPageEntry.Controls.Add(txnCodeLabel);
            this.tabPageEntry.Controls.Add(amountLabel);
            this.tabPageEntry.Controls.Add(this.amountTextBox);
            this.tabPageEntry.Location = new System.Drawing.Point(4, 22);
            this.tabPageEntry.Name = "tabPageEntry";
            this.tabPageEntry.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEntry.Size = new System.Drawing.Size(966, 352);
            this.tabPageEntry.TabIndex = 1;
            this.tabPageEntry.Text = "Edit Transaction";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(165, 290);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // grpDataEntry
            // 
            this.grpDataEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grpDataEntry.Controls.Add(this.rbDelete);
            this.grpDataEntry.Controls.Add(this.rbAddUpdate);
            this.grpDataEntry.ForeColor = System.Drawing.Color.Black;
            this.grpDataEntry.Location = new System.Drawing.Point(166, 55);
            this.grpDataEntry.Name = "grpDataEntry";
            this.grpDataEntry.Size = new System.Drawing.Size(193, 55);
            this.grpDataEntry.TabIndex = 1;
            this.grpDataEntry.TabStop = false;
            this.grpDataEntry.Text = "Transaction Data Entry";
            // 
            // rbDelete
            // 
            this.rbDelete.AutoSize = true;
            this.rbDelete.Location = new System.Drawing.Point(118, 25);
            this.rbDelete.Name = "rbDelete";
            this.rbDelete.Size = new System.Drawing.Size(56, 17);
            this.rbDelete.TabIndex = 1;
            this.rbDelete.Text = "Delete";
            this.rbDelete.UseVisualStyleBackColor = true;
            // 
            // rbAddUpdate
            // 
            this.rbAddUpdate.AutoSize = true;
            this.rbAddUpdate.Checked = true;
            this.rbAddUpdate.Location = new System.Drawing.Point(15, 25);
            this.rbAddUpdate.Name = "rbAddUpdate";
            this.rbAddUpdate.Size = new System.Drawing.Size(84, 17);
            this.rbAddUpdate.TabIndex = 0;
            this.rbAddUpdate.TabStop = true;
            this.rbAddUpdate.Text = "Add/Update";
            this.rbAddUpdate.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(284, 290);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cbPayrollItem
            // 
            this.cbPayrollItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPayrollItem.FormattingEnabled = true;
            this.cbPayrollItem.Location = new System.Drawing.Point(165, 118);
            this.cbPayrollItem.Name = "cbPayrollItem";
            this.cbPayrollItem.Size = new System.Drawing.Size(188, 21);
            this.cbPayrollItem.TabIndex = 2;
            // 
            // txtTxncode
            // 
            this.txtTxncode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTxncode.Location = new System.Drawing.Point(165, 27);
            this.txtTxncode.Name = "txtTxncode";
            this.txtTxncode.Size = new System.Drawing.Size(188, 20);
            this.txtTxncode.TabIndex = 0;
            // 
            // trackCheckBox
            // 
            this.trackCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.trackCheckBox.Location = new System.Drawing.Point(165, 239);
            this.trackCheckBox.Name = "trackCheckBox";
            this.trackCheckBox.Size = new System.Drawing.Size(88, 24);
            this.trackCheckBox.TabIndex = 6;
            this.trackCheckBox.Text = "Track YTD:";
            // 
            // enabledCheckBox
            // 
            this.enabledCheckBox.Checked = true;
            this.enabledCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enabledCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.enabledCheckBox.Location = new System.Drawing.Point(165, 207);
            this.enabledCheckBox.Name = "enabledCheckBox";
            this.enabledCheckBox.Size = new System.Drawing.Size(75, 24);
            this.enabledCheckBox.TabIndex = 5;
            this.enabledCheckBox.Text = "Enabled:";
            // 
            // recurrentCheckBox
            // 
            this.recurrentCheckBox.Checked = true;
            this.recurrentCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.recurrentCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.recurrentCheckBox.Location = new System.Drawing.Point(165, 175);
            this.recurrentCheckBox.Name = "recurrentCheckBox";
            this.recurrentCheckBox.Size = new System.Drawing.Size(75, 24);
            this.recurrentCheckBox.TabIndex = 4;
            this.recurrentCheckBox.Text = "Recurrent:";
            // 
            // amountTextBox
            // 
            this.amountTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.amountTextBox.Location = new System.Drawing.Point(165, 147);
            this.amountTextBox.Name = "amountTextBox";
            this.amountTextBox.Size = new System.Drawing.Size(188, 20);
            this.amountTextBox.TabIndex = 3;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Location = new System.Drawing.Point(658, 19);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnApply
            // 
            this.btnApply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(754, 19);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = false;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(850, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClode_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOk);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 397);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(980, 62);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(980, 397);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "TxnCode";
            this.dataGridViewTextBoxColumn1.HeaderText = "Transaction Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DataEntry";
            this.dataGridViewTextBoxColumn2.HeaderText = "Data Entry";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "PayrollItem";
            this.dataGridViewTextBoxColumn3.HeaderText = "Payroll Item";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DefaultAmount";
            dataGridViewCellStyle3.Format = "C2";
            dataGridViewCellStyle3.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewTextBoxColumn4.HeaderText = "Default Amount";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 124;
            // 
            // TransactionDefinition
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(980, 459);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TransactionDefinition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transaction  Definition";
            this.Load += new System.EventHandler(this.TransactionDefinition_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPageEntry.ResumeLayout(false);
            this.tabPageEntry.PerformLayout();
            this.grpDataEntry.ResumeLayout(false);
            this.grpDataEntry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageList;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabPage tabPageEntry;
        private System.Windows.Forms.ComboBox cbPayrollItem;
        private System.Windows.Forms.TextBox txtTxncode;
        private System.Windows.Forms.CheckBox trackCheckBox;
        private System.Windows.Forms.CheckBox enabledCheckBox;
        private System.Windows.Forms.CheckBox recurrentCheckBox;
        private System.Windows.Forms.TextBox amountTextBox;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpDataEntry;
        private System.Windows.Forms.RadioButton rbDelete;
        private System.Windows.Forms.RadioButton rbAddUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TxnCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataEntry;
        private System.Windows.Forms.DataGridViewTextBoxColumn PayrollItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultAmount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TransDefEnabled;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Recurrent;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TrackYTD;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

    }
}