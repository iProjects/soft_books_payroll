namespace winSBPayroll.Forms
{
    partial class SearchEmployeeSimpleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchEmployeeSimpleForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDepartment = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSurName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOtherNames = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmpNo = new System.Windows.Forms.TextBox();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.dataGridViewEmployees = new System.Windows.Forms.DataGridView();
            this.eEmpno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.esurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOtherNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bindingSourceEmployees = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBoxResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEmployees)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LimeGreen;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDepartment);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSurName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtOtherNames);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtEmpNo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(841, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please define a criteria";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(373, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Department:";
            // 
            // txtDepartment
            // 
            this.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDepartment.Location = new System.Drawing.Point(445, 58);
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.Size = new System.Drawing.Size(236, 20);
            this.txtDepartment.TabIndex = 36;
            this.txtDepartment.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDepartment_KeyPress);
            this.txtDepartment.Validated += new System.EventHandler(this.txtDepartment_Validated);
            // 
            // btnSubmit
            // 
            this.btnSubmit.AutoSize = true;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.LinkColor = System.Drawing.Color.Yellow;
            this.btnSubmit.Location = new System.Drawing.Point(726, 16);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(84, 25);
            this.btnSubmit.TabIndex = 29;
            this.btnSubmit.TabStop = true;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSubmit_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.LinkColor = System.Drawing.Color.Yellow;
            this.btnClose.Location = new System.Drawing.Point(726, 51);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 25);
            this.btnClose.TabIndex = 28;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Close";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "SurName:";
            // 
            // txtSurName
            // 
            this.txtSurName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSurName.Location = new System.Drawing.Point(75, 59);
            this.txtSurName.Name = "txtSurName";
            this.txtSurName.Size = new System.Drawing.Size(244, 20);
            this.txtSurName.TabIndex = 21;
            this.txtSurName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSurName_KeyPress);
            this.txtSurName.Validated += new System.EventHandler(this.txtSurName_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(371, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "OtherNames:";
            // 
            // txtOtherNames
            // 
            this.txtOtherNames.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOtherNames.Location = new System.Drawing.Point(443, 25);
            this.txtOtherNames.Name = "txtOtherNames";
            this.txtOtherNames.Size = new System.Drawing.Size(238, 20);
            this.txtOtherNames.TabIndex = 22;
            this.txtOtherNames.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOtherNames_KeyPress);
            this.txtOtherNames.Validated += new System.EventHandler(this.txtOtherNames_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Emp No:";
            // 
            // txtEmpNo
            // 
            this.txtEmpNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmpNo.Location = new System.Drawing.Point(75, 23);
            this.txtEmpNo.Name = "txtEmpNo";
            this.txtEmpNo.Size = new System.Drawing.Size(245, 20);
            this.txtEmpNo.TabIndex = 20;
            this.txtEmpNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmpNo_KeyPress);
            this.txtEmpNo.Validated += new System.EventHandler(this.txtEmpNo_Validated);
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.dataGridViewEmployees);
            this.groupBoxResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxResults.Location = new System.Drawing.Point(0, 88);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(841, 285);
            this.groupBoxResults.TabIndex = 4;
            this.groupBoxResults.TabStop = false;
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
            this.dataGridViewEmployees.Size = new System.Drawing.Size(835, 266);
            this.dataGridViewEmployees.TabIndex = 2;
            this.dataGridViewEmployees.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewEmployees_CellContentDoubleClick);
            this.dataGridViewEmployees.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewEmployees_DataError);
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
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Empno";
            this.dataGridViewTextBoxColumn1.HeaderText = "Empno";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Surname";
            this.dataGridViewTextBoxColumn2.HeaderText = "Surname";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "OtherNames";
            this.dataGridViewTextBoxColumn3.HeaderText = "Other Names";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // SearchEmployeeSimpleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(841, 373);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchEmployeeSimpleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Employee";
            this.Load += new System.EventHandler(this.SearchEmployeeSimpleForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewEmployees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceEmployees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSurName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOtherNames;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmpNo;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.DataGridView dataGridViewEmployees;
        private System.Windows.Forms.DataGridViewTextBoxColumn eEmpno;
        private System.Windows.Forms.DataGridViewTextBoxColumn esurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOtherNames;
        private System.Windows.Forms.BindingSource bindingSourceEmployees;
        private System.Windows.Forms.LinkLabel btnClose;
        private System.Windows.Forms.LinkLabel btnSubmit;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDepartment;
    }
}