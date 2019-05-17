namespace winSBPayroll.Forms
{
    partial class SearchBanksSimpleForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SearchBanksSimpleForm));
            this.bindingSourceBanks = new System.Windows.Forms.BindingSource(this.components);
            this.btnSubmit = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridViewBanks = new System.Windows.Forms.DataGridView();
            this.ColumnBankSortCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBankBranchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBankCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBranchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnBranchCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxResults = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBranchCode = new System.Windows.Forms.TextBox();
            this.txtBankCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBranchName = new System.Windows.Forms.TextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBanks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBanks)).BeginInit();
            this.groupBoxResults.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.AutoSize = true;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnSubmit.LinkColor = System.Drawing.Color.Yellow;
            this.btnSubmit.Location = new System.Drawing.Point(752, 23);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(60, 18);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.TabStop = true;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSubmit_LinkClicked);
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.btnClose.LinkColor = System.Drawing.Color.Yellow;
            this.btnClose.Location = new System.Drawing.Point(752, 53);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(52, 18);
            this.btnClose.TabIndex = 5;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Close";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Bank Name:";
            // 
            // dataGridViewBanks
            // 
            this.dataGridViewBanks.AllowUserToAddRows = false;
            this.dataGridViewBanks.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridViewBanks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewBanks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBanks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnBankSortCode,
            this.ColumnBankBranchName,
            this.ColumnBankName,
            this.ColumnBankCode,
            this.ColumnBranchName,
            this.ColumnBranchCode});
            this.dataGridViewBanks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewBanks.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewBanks.MultiSelect = false;
            this.dataGridViewBanks.Name = "dataGridViewBanks";
            this.dataGridViewBanks.ReadOnly = true;
            this.dataGridViewBanks.Size = new System.Drawing.Size(835, 266);
            this.dataGridViewBanks.TabIndex = 0;
            this.dataGridViewBanks.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBanks_CellContentDoubleClick);
            this.dataGridViewBanks.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewBanks_DataError);
            // 
            // ColumnBankSortCode
            // 
            this.ColumnBankSortCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBankSortCode.DataPropertyName = "BankSortCode";
            this.ColumnBankSortCode.HeaderText = "Bank_Sort_Code";
            this.ColumnBankSortCode.Name = "ColumnBankSortCode";
            this.ColumnBankSortCode.ReadOnly = true;
            // 
            // ColumnBankBranchName
            // 
            this.ColumnBankBranchName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBankBranchName.DataPropertyName = "BankBranchName";
            this.ColumnBankBranchName.HeaderText = "Bank_Branch_Name";
            this.ColumnBankBranchName.Name = "ColumnBankBranchName";
            this.ColumnBankBranchName.ReadOnly = true;
            this.ColumnBankBranchName.Width = 200;
            // 
            // ColumnBankName
            // 
            this.ColumnBankName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBankName.DataPropertyName = "BankName";
            this.ColumnBankName.HeaderText = "Bank_Name";
            this.ColumnBankName.Name = "ColumnBankName";
            this.ColumnBankName.ReadOnly = true;
            this.ColumnBankName.Width = 150;
            // 
            // ColumnBankCode
            // 
            this.ColumnBankCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBankCode.DataPropertyName = "BankCode";
            this.ColumnBankCode.HeaderText = "Bank_Code";
            this.ColumnBankCode.Name = "ColumnBankCode";
            this.ColumnBankCode.ReadOnly = true;
            // 
            // ColumnBranchName
            // 
            this.ColumnBranchName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnBranchName.DataPropertyName = "BranchName";
            this.ColumnBranchName.HeaderText = "Branch_Name";
            this.ColumnBranchName.Name = "ColumnBranchName";
            this.ColumnBranchName.ReadOnly = true;
            this.ColumnBranchName.Width = 150;
            // 
            // ColumnBranchCode
            // 
            this.ColumnBranchCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnBranchCode.DataPropertyName = "BranchCode";
            this.ColumnBranchCode.HeaderText = "Branch_Code";
            this.ColumnBranchCode.Name = "ColumnBranchCode";
            this.ColumnBranchCode.ReadOnly = true;
            // 
            // txtBankName
            // 
            this.txtBankName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBankName.Location = new System.Drawing.Point(97, 52);
            this.txtBankName.MaxLength = 50;
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(256, 20);
            this.txtBankName.TabIndex = 1;
            this.txtBankName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBankName_KeyPress);
            this.txtBankName.Validated += new System.EventHandler(this.txtBankName_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(402, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Branch Code:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Bank Code:";
            // 
            // groupBoxResults
            // 
            this.groupBoxResults.Controls.Add(this.dataGridViewBanks);
            this.groupBoxResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxResults.Location = new System.Drawing.Point(0, 88);
            this.groupBoxResults.Name = "groupBoxResults";
            this.groupBoxResults.Size = new System.Drawing.Size(841, 285);
            this.groupBoxResults.TabIndex = 1;
            this.groupBoxResults.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LimeGreen;
            this.groupBox1.Controls.Add(this.txtBranchCode);
            this.groupBox1.Controls.Add(this.txtBankCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtBranchName);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtBankName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(841, 88);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Please define a criteria";
            // 
            // txtBranchCode
            // 
            this.txtBranchCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchCode.Location = new System.Drawing.Point(476, 22);
            this.txtBranchCode.MaxLength = 50;
            this.txtBranchCode.Name = "txtBranchCode";
            this.txtBranchCode.Size = new System.Drawing.Size(234, 20);
            this.txtBranchCode.TabIndex = 2;
            this.txtBranchCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBranchCode_KeyDown);
            this.txtBranchCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBranchCode_KeyPress);
            this.txtBranchCode.Validated += new System.EventHandler(this.txtBranchCode_Validated);
            // 
            // txtBankCode
            // 
            this.txtBankCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBankCode.Location = new System.Drawing.Point(97, 22);
            this.txtBankCode.MaxLength = 50;
            this.txtBankCode.Name = "txtBankCode";
            this.txtBankCode.Size = new System.Drawing.Size(256, 20);
            this.txtBankCode.TabIndex = 0;
            this.txtBankCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBankCode_KeyDown);
            this.txtBankCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBankCode_KeyPress);
            this.txtBankCode.Validated += new System.EventHandler(this.txtBankCode_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(399, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Branch Name:";
            // 
            // txtBranchName
            // 
            this.txtBranchName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBranchName.Location = new System.Drawing.Point(476, 52);
            this.txtBranchName.MaxLength = 50;
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.Size = new System.Drawing.Size(234, 20);
            this.txtBranchName.TabIndex = 3;
            this.txtBranchName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBranchName_KeyPress);
            this.txtBranchName.Validated += new System.EventHandler(this.txtBranchName_Validated);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Empno";
            this.dataGridViewTextBoxColumn1.HeaderText = "Empno";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Surname";
            this.dataGridViewTextBoxColumn2.HeaderText = "Surname";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 200;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "OtherNames";
            this.dataGridViewTextBoxColumn3.HeaderText = "Other Names";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "BankCode";
            this.dataGridViewTextBoxColumn4.HeaderText = "Bank Code";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "BranchName";
            this.dataGridViewTextBoxColumn5.HeaderText = "Branch Name";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "BranchCode";
            this.dataGridViewTextBoxColumn6.HeaderText = "Branch Code";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // SearchBanksSimpleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(841, 373);
            this.Controls.Add(this.groupBoxResults);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SearchBanksSimpleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Search Banks";
            this.Load += new System.EventHandler(this.SearchBanksSimpleForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceBanks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBanks)).EndInit();
            this.groupBoxResults.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSourceBanks;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.LinkLabel btnSubmit;
        private System.Windows.Forms.LinkLabel btnClose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridView dataGridViewBanks;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxResults;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBranchName;
        private System.Windows.Forms.TextBox txtBranchCode;
        private System.Windows.Forms.TextBox txtBankCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBankSortCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBankBranchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBankCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBranchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnBranchCode;
    }
}