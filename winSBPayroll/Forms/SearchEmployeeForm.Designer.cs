namespace winSBPayroll.Forms
{
    partial class SearchEmployeeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchEmployeeForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpConjuction = new System.Windows.Forms.GroupBox();
            this.rbOr = new System.Windows.Forms.RadioButton();
            this.rbAnd = new System.Windows.Forms.RadioButton();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnRetrieve = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lbCriteria = new System.Windows.Forms.ListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.cbOperator = new System.Windows.Forms.ComboBox();
            this.cbField = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.eEmpno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.esurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOtherNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceEmployees = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.grpConjuction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEmployees)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LimeGreen;
            this.groupBox1.Controls.Add(this.grpConjuction);
            this.groupBox1.Controls.Add(this.lblMessage);
            this.groupBox1.Controls.Add(this.btnRetrieve);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.lbCriteria);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.txtValue);
            this.groupBox1.Controls.Add(this.cbOperator);
            this.groupBox1.Controls.Add(this.cbField);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(841, 162);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please define a criteria";
            // 
            // grpConjuction
            // 
            this.grpConjuction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.grpConjuction.Controls.Add(this.rbOr);
            this.grpConjuction.Controls.Add(this.rbAnd);
            this.grpConjuction.Location = new System.Drawing.Point(589, 12);
            this.grpConjuction.Name = "grpConjuction";
            this.grpConjuction.Size = new System.Drawing.Size(115, 71);
            this.grpConjuction.TabIndex = 3;
            this.grpConjuction.TabStop = false;
            this.grpConjuction.Text = "Conjuction";
            // 
            // rbOr
            // 
            this.rbOr.AutoSize = true;
            this.rbOr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbOr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbOr.Location = new System.Drawing.Point(24, 43);
            this.rbOr.Name = "rbOr";
            this.rbOr.Size = new System.Drawing.Size(47, 20);
            this.rbOr.TabIndex = 1;
            this.rbOr.Text = "OR";
            this.rbOr.UseVisualStyleBackColor = true;
            // 
            // rbAnd
            // 
            this.rbAnd.AutoSize = true;
            this.rbAnd.Checked = true;
            this.rbAnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rbAnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAnd.Location = new System.Drawing.Point(24, 20);
            this.rbAnd.Name = "rbAnd";
            this.rbAnd.Size = new System.Drawing.Size(57, 20);
            this.rbAnd.TabIndex = 0;
            this.rbAnd.TabStop = true;
            this.rbAnd.Text = "AND";
            this.rbAnd.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(64, 20);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 7;
            // 
            // btnRetrieve
            // 
            this.btnRetrieve.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnRetrieve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetrieve.Location = new System.Drawing.Point(723, 127);
            this.btnRetrieve.Name = "btnRetrieve";
            this.btnRetrieve.Size = new System.Drawing.Size(75, 23);
            this.btnRetrieve.TabIndex = 6;
            this.btnRetrieve.Text = "Preview";
            this.btnRetrieve.UseVisualStyleBackColor = false;
            this.btnRetrieve.Click += new System.EventHandler(this.btnRetrieve_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Location = new System.Drawing.Point(723, 50);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lbCriteria
            // 
            this.lbCriteria.BackColor = System.Drawing.Color.White;
            this.lbCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCriteria.DisplayMember = "CriteriaString";
            this.lbCriteria.FormattingEnabled = true;
            this.lbCriteria.Location = new System.Drawing.Point(19, 96);
            this.lbCriteria.Name = "lbCriteria";
            this.lbCriteria.Size = new System.Drawing.Size(520, 54);
            this.lbCriteria.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(723, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtValue
            // 
            this.txtValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtValue.Location = new System.Drawing.Point(328, 51);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(211, 20);
            this.txtValue.TabIndex = 2;
            // 
            // cbOperator
            // 
            this.cbOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOperator.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbOperator.FormattingEnabled = true;
            this.cbOperator.Location = new System.Drawing.Point(161, 50);
            this.cbOperator.Name = "cbOperator";
            this.cbOperator.Size = new System.Drawing.Size(145, 21);
            this.cbOperator.TabIndex = 1;
            // 
            // cbField
            // 
            this.cbField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbField.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbField.FormattingEnabled = true;
            this.cbField.Location = new System.Drawing.Point(19, 51);
            this.cbField.Name = "cbField";
            this.cbField.Size = new System.Drawing.Size(121, 21);
            this.cbField.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Location = new System.Drawing.Point(13, 36);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 0;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(13, 85);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dataGridViewEmployees
            // 
            this.dataGridViewEmployees.AllowUserToAddRows = false;
            this.dataGridViewEmployees.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridViewEmployees.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewEmployees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.eEmpno,
            this.esurname,
            this.ColumnOtherNames});
            this.dataGridViewEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewEmployees.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewEmployees.MultiSelect = false;
            this.dataGridViewEmployees.Name = "dataGridViewEmployees";
            this.dataGridViewEmployees.ReadOnly = true;
            this.dataGridViewEmployees.Size = new System.Drawing.Size(704, 192);
            this.dataGridViewEmployees.TabIndex = 2;
            this.dataGridViewEmployees.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEmployees_CellContentDoubleClick);
            // 
            // eEmpno
            // 
            this.eEmpno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.eEmpno.DataPropertyName = "Empno";
            this.eEmpno.HeaderText = "Empno";
            this.eEmpno.Name = "eEmpno";
            this.eEmpno.ReadOnly = true;
            // 
            // esurname
            // 
            this.esurname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.esurname.DataPropertyName = "Surname";
            this.esurname.HeaderText = "Surname";
            this.esurname.Name = "esurname";
            this.esurname.ReadOnly = true;
            // 
            // ColumnOtherNames
            // 
            this.ColumnOtherNames.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnOtherNames.DataPropertyName = "OtherNames";
            this.ColumnOtherNames.HeaderText = "Other Names";
            this.ColumnOtherNames.Name = "ColumnOtherNames";
            this.ColumnOtherNames.ReadOnly = true;
            this.ColumnOtherNames.Width = 150;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnSubmit);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(710, 162);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(131, 211);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Empno";
            this.dataGridViewTextBoxColumn1.HeaderText = "Empno";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 132;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Surname";
            this.dataGridViewTextBoxColumn2.HeaderText = "Surname";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 131;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Department";
            this.dataGridViewTextBoxColumn3.HeaderText = "Department";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 133;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Gender";
            this.dataGridViewTextBoxColumn4.HeaderText = "Gender";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 132;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "MaritalStatus";
            this.dataGridViewTextBoxColumn5.HeaderText = "Marital Status";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 133;
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.dataGridViewEmployees);
            this.groupBoxResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxResults.Location = new System.Drawing.Point(0, 162);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(710, 211);
            this.groupBoxResults.TabIndex = 3;
            this.groupBoxResults.TabStop = false;
            // 
            // SearchEmployeeForm
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(841, 373);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchEmployeeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Employee";
            this.Load += new System.EventHandler(this.SelectForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpConjuction.ResumeLayout(false);
            this.grpConjuction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEmployees)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBoxResults.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRetrieve;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ListBox lbCriteria;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.ComboBox cbOperator;
        private System.Windows.Forms.ComboBox cbField;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox grpConjuction;
        private System.Windows.Forms.RadioButton rbOr;
        private System.Windows.Forms.RadioButton rbAnd;
        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.BindingSource bindingSourceEmployees;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.DataGridViewTextBoxColumn eEmpno;
        private System.Windows.Forms.DataGridViewTextBoxColumn esurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOtherNames;
    }
}