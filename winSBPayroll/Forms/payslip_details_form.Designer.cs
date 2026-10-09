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
            this.panelPager = new System.Windows.Forms.Panel();
            this.dataGridView_payslip = new System.Windows.Forms.DataGridView();
            this.Column1Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2EmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6TaxTracking = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8IsStatutory = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column9YTD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_is_statutory = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbotaxtracking = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbopayrollperiods = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbopayrollyears = new System.Windows.Forms.ComboBox();
            this.cboemployeeno = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbopayrollitem = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btndetails = new System.Windows.Forms.Button();
            this.btnclearfilter = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
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
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_payslip)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_payslip)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelPager);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 614);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1194, 58);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // panelPager
            // 
            this.panelPager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPager.Location = new System.Drawing.Point(3, 16);
            this.panelPager.Name = "panelPager";
            this.panelPager.Size = new System.Drawing.Size(1188, 39);
            this.panelPager.TabIndex = 1;
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
            this.dataGridView_payslip.Size = new System.Drawing.Size(1188, 538);
            this.dataGridView_payslip.TabIndex = 1;
            this.dataGridView_payslip.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_payslip_DataError);
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
            this.Column5Description.Width = 150;
            // 
            // Column6TaxTracking
            // 
            this.Column6TaxTracking.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column6TaxTracking.DataPropertyName = "TaxTracking";
            this.Column6TaxTracking.HeaderText = "Tax Tracking";
            this.Column6TaxTracking.Name = "Column6TaxTracking";
            this.Column6TaxTracking.ReadOnly = true;
            this.Column6TaxTracking.Width = 150;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_is_statutory);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbotaxtracking);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbopayrollperiods);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbopayrollyears);
            this.groupBox1.Controls.Add(this.cboemployeeno);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbopayrollitem);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btndetails);
            this.groupBox1.Controls.Add(this.btnclearfilter);
            this.groupBox1.Controls.Add(this.btnexit);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1194, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chk_is_statutory
            // 
            this.chk_is_statutory.AutoSize = true;
            this.chk_is_statutory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chk_is_statutory.Location = new System.Drawing.Point(871, 24);
            this.chk_is_statutory.Name = "chk_is_statutory";
            this.chk_is_statutory.Size = new System.Drawing.Size(76, 17);
            this.chk_is_statutory.TabIndex = 21;
            this.chk_is_statutory.Text = "Is Statutory";
            this.chk_is_statutory.UseVisualStyleBackColor = true;
            this.chk_is_statutory.CheckedChanged += new System.EventHandler(this.chk_is_statutory_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(670, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Tax Tracking";
            // 
            // cbotaxtracking
            // 
            this.cbotaxtracking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbotaxtracking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbotaxtracking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbotaxtracking.ForeColor = System.Drawing.Color.Black;
            this.cbotaxtracking.FormattingEnabled = true;
            this.cbotaxtracking.Location = new System.Drawing.Point(746, 23);
            this.cbotaxtracking.Name = "cbotaxtracking";
            this.cbotaxtracking.Size = new System.Drawing.Size(105, 21);
            this.cbotaxtracking.TabIndex = 19;
            this.cbotaxtracking.SelectedIndexChanged += new System.EventHandler(this.cbotaxtracking_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(531, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Period";
            // 
            // cbopayrollperiods
            // 
            this.cbopayrollperiods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopayrollperiods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbopayrollperiods.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbopayrollperiods.ForeColor = System.Drawing.Color.Black;
            this.cbopayrollperiods.FormattingEnabled = true;
            this.cbopayrollperiods.Location = new System.Drawing.Point(570, 23);
            this.cbopayrollperiods.Name = "cbopayrollperiods";
            this.cbopayrollperiods.Size = new System.Drawing.Size(79, 21);
            this.cbopayrollperiods.TabIndex = 17;
            this.cbopayrollperiods.SelectedIndexChanged += new System.EventHandler(this.cbopayrollperiods_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(394, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Year";
            // 
            // cbopayrollyears
            // 
            this.cbopayrollyears.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopayrollyears.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbopayrollyears.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbopayrollyears.ForeColor = System.Drawing.Color.Black;
            this.cbopayrollyears.FormattingEnabled = true;
            this.cbopayrollyears.Location = new System.Drawing.Point(429, 24);
            this.cbopayrollyears.Name = "cbopayrollyears";
            this.cbopayrollyears.Size = new System.Drawing.Size(91, 21);
            this.cbopayrollyears.TabIndex = 15;
            this.cbopayrollyears.SelectedIndexChanged += new System.EventHandler(this.cbopayrollyears_SelectedIndexChanged);
            // 
            // cboemployeeno
            // 
            this.cboemployeeno.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboemployeeno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboemployeeno.FormattingEnabled = true;
            this.cboemployeeno.Location = new System.Drawing.Point(258, 24);
            this.cboemployeeno.Name = "cboemployeeno";
            this.cboemployeeno.Size = new System.Drawing.Size(122, 21);
            this.cboemployeeno.TabIndex = 14;
            this.cboemployeeno.SelectedIndexChanged += new System.EventHandler(this.cboemployeeno_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Employee No";
            // 
            // cbopayrollitem
            // 
            this.cbopayrollitem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopayrollitem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbopayrollitem.FormattingEnabled = true;
            this.cbopayrollitem.Location = new System.Drawing.Point(75, 23);
            this.cbopayrollitem.Name = "cbopayrollitem";
            this.cbopayrollitem.Size = new System.Drawing.Size(101, 21);
            this.cbopayrollitem.TabIndex = 12;
            this.cbopayrollitem.SelectedIndexChanged += new System.EventHandler(this.cbopayrollitem_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Payroll Item";
            // 
            // btndetails
            // 
            this.btndetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btndetails.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btndetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndetails.Location = new System.Drawing.Point(964, 21);
            this.btndetails.Name = "btndetails";
            this.btndetails.Size = new System.Drawing.Size(59, 23);
            this.btndetails.TabIndex = 9;
            this.btndetails.Text = "details";
            this.btndetails.UseVisualStyleBackColor = false;
            // 
            // btnclearfilter
            // 
            this.btnclearfilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnclearfilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclearfilter.Location = new System.Drawing.Point(1032, 21);
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
            this.btnexit.Location = new System.Drawing.Point(1123, 21);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(59, 23);
            this.btnexit.TabIndex = 7;
            this.btnexit.Text = "exit";
            this.btnexit.UseVisualStyleBackColor = false;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView_payslip);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 57);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1194, 557);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
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
            this.dataGridViewTextBoxColumn7.Width = 70;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.DataPropertyName = "YTD";
            this.dataGridViewTextBoxColumn8.HeaderText = "YTD";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // payslip_details_form
            // 
            this.AcceptButton = this.btndetails;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btndetails;
            this.ClientSize = new System.Drawing.Size(1194, 672);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "payslip_details_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payslip Details";
            this.Load += new System.EventHandler(this.payslip_details_form_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_payslip)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_payslip)).EndInit();
            this.groupBox3.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panelPager;
        private System.Windows.Forms.ComboBox cboemployeeno;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbopayrollitem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btndetails;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbopayrollyears;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbopayrollperiods;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbotaxtracking;
        private System.Windows.Forms.CheckBox chk_is_statutory;
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