namespace winSBPayroll.Forms
{
    partial class payslip_details_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(payslip_details_form));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView_payslip = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnclearfilter = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.bindingSource_payslip = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2EmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6TaxTracking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8IsStatutory = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column9YTD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_payslip)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_payslip)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView_payslip);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 57);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1194, 537);
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
            this.Column1Id,
            this.Column3Year,
            this.Column4Period,
            this.Column2EmpNo,
            this.Column5Description,
            this.Column6TaxTracking,
            this.Column7Amount,
            this.Column8IsStatutory,
            this.Column9YTD});
            this.dataGridView_payslip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_payslip.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_payslip.Name = "dataGridView_payslip";
            this.dataGridView_payslip.ReadOnly = true;
            this.dataGridView_payslip.Size = new System.Drawing.Size(1188, 518);
            this.dataGridView_payslip.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnclearfilter);
            this.groupBox1.Controls.Add(this.btnexit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1194, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnclearfilter
            // 
            this.btnclearfilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnclearfilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclearfilter.Location = new System.Drawing.Point(994, 19);
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
            this.btnexit.Location = new System.Drawing.Point(1091, 19);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(59, 23);
            this.btnexit.TabIndex = 7;
            this.btnexit.Text = "exit";
            this.btnexit.UseVisualStyleBackColor = false;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Year";
            this.dataGridViewTextBoxColumn2.HeaderText = "Year";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Period";
            this.dataGridViewTextBoxColumn3.HeaderText = "Period";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 80;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "EmpNo";
            this.dataGridViewTextBoxColumn4.HeaderText = "EmpNo";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn5.HeaderText = "Item";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "TaxTracking";
            this.dataGridViewTextBoxColumn6.HeaderText = "Tax Tracking";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Amount";
            this.dataGridViewTextBoxColumn7.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "YTD";
            this.dataGridViewTextBoxColumn8.HeaderText = "YTD";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // Column1Id
            // 
            this.Column1Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1Id.DataPropertyName = "Id";
            this.Column1Id.HeaderText = "Id";
            this.Column1Id.Name = "Column1Id";
            this.Column1Id.ReadOnly = true;
            this.Column1Id.Width = 60;
            // 
            // Column3Year
            // 
            this.Column3Year.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3Year.DataPropertyName = "Year";
            this.Column3Year.HeaderText = "Year";
            this.Column3Year.Name = "Column3Year";
            this.Column3Year.ReadOnly = true;
            this.Column3Year.Width = 80;
            // 
            // Column4Period
            // 
            this.Column4Period.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4Period.DataPropertyName = "Period";
            this.Column4Period.HeaderText = "Period";
            this.Column4Period.Name = "Column4Period";
            this.Column4Period.ReadOnly = true;
            this.Column4Period.Width = 80;
            // 
            // Column2EmpNo
            // 
            this.Column2EmpNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2EmpNo.DataPropertyName = "EmpNo";
            this.Column2EmpNo.HeaderText = "EmpNo";
            this.Column2EmpNo.Name = "Column2EmpNo";
            this.Column2EmpNo.ReadOnly = true;
            // 
            // Column5Description
            // 
            this.Column5Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column5Description.DataPropertyName = "Description";
            this.Column5Description.HeaderText = "Item";
            this.Column5Description.Name = "Column5Description";
            this.Column5Description.ReadOnly = true;
            this.Column5Description.Width = 120;
            // 
            // Column6TaxTracking
            // 
            this.Column6TaxTracking.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column6TaxTracking.DataPropertyName = "TaxTracking";
            this.Column6TaxTracking.HeaderText = "Tax Tracking";
            this.Column6TaxTracking.Name = "Column6TaxTracking";
            this.Column6TaxTracking.ReadOnly = true;
            // 
            // Column7Amount
            // 
            this.Column7Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column7Amount.DataPropertyName = "Amount";
            this.Column7Amount.HeaderText = "Amount";
            this.Column7Amount.Name = "Column7Amount";
            this.Column7Amount.ReadOnly = true;
            this.Column7Amount.Width = 70;
            // 
            // Column8IsStatutory
            // 
            this.Column8IsStatutory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column8IsStatutory.DataPropertyName = "IsStatutory";
            this.Column8IsStatutory.HeaderText = "Is Statutory";
            this.Column8IsStatutory.Name = "Column8IsStatutory";
            this.Column8IsStatutory.ReadOnly = true;
            this.Column8IsStatutory.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column8IsStatutory.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column9YTD
            // 
            this.Column9YTD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column9YTD.DataPropertyName = "YTD";
            this.Column9YTD.HeaderText = "YTD";
            this.Column9YTD.Name = "Column9YTD";
            this.Column9YTD.ReadOnly = true;
            // 
            // payslip_details_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(1194, 594);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "payslip_details_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "payslip details";
            this.Load += new System.EventHandler(this.payslip_details_form_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_payslip)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_payslip)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4Period;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2EmpNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6TaxTracking;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7Amount;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column8IsStatutory;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9YTD;
    }
}