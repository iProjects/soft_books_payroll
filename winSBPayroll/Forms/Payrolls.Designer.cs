namespace winSBPayroll.Forms
{
    partial class Payrolls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Payrolls));
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboemployer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbopayrollyears = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridViewPayrolls = new System.Windows.Forms.DataGridView();
            this.bindingSourcePayrolls = new System.Windows.Forms.BindingSource(this.components);
            this.chkfor = new System.Windows.Forms.CheckBox();
            this.chkisopen = new System.Windows.Forms.CheckBox();
            this.btnclearfilter = new System.Windows.Forms.Button();
            this.Approved = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IsOpen = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Processed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Year = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Period = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dDateRun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RunByk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayrolls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePayrolls)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LimeGreen;
            this.panel2.Controls.Add(this.btnclearfilter);
            this.panel2.Controls.Add(this.chkisopen);
            this.panel2.Controls.Add(this.chkfor);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cboemployer);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cbopayrollyears);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1010, 44);
            this.panel2.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Employer";
            // 
            // cboemployer
            // 
            this.cboemployer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboemployer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboemployer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboemployer.ForeColor = System.Drawing.Color.Black;
            this.cboemployer.FormattingEnabled = true;
            this.cboemployer.Location = new System.Drawing.Point(65, 13);
            this.cboemployer.Name = "cboemployer";
            this.cboemployer.Size = new System.Drawing.Size(213, 21);
            this.cboemployer.TabIndex = 6;
            this.cboemployer.SelectedIndexChanged += new System.EventHandler(this.cboemployer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(468, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Year";
            // 
            // cbopayrollyears
            // 
            this.cbopayrollyears.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopayrollyears.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbopayrollyears.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbopayrollyears.ForeColor = System.Drawing.Color.Black;
            this.cbopayrollyears.FormattingEnabled = true;
            this.cbopayrollyears.Location = new System.Drawing.Point(503, 13);
            this.cbopayrollyears.Name = "cbopayrollyears";
            this.cbopayrollyears.Size = new System.Drawing.Size(113, 21);
            this.cbopayrollyears.TabIndex = 0;
            this.cbopayrollyears.SelectedIndexChanged += new System.EventHandler(this.cbYr_SelectedIndexChanged);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(892, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(699, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridViewPayrolls);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1010, 492);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // dataGridViewPayrolls
            // 
            this.dataGridViewPayrolls.AllowUserToAddRows = false;
            this.dataGridViewPayrolls.AllowUserToDeleteRows = false;
            this.dataGridViewPayrolls.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPayrolls.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPayrolls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPayrolls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Year,
            this.Period,
            this.dDateRun,
            this.RunByk,
            this.Approved,
            this.IsOpen,
            this.Processed});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPayrolls.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewPayrolls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPayrolls.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewPayrolls.MultiSelect = false;
            this.dataGridViewPayrolls.Name = "dataGridViewPayrolls";
            this.dataGridViewPayrolls.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPayrolls.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewPayrolls.Size = new System.Drawing.Size(1004, 473);
            this.dataGridViewPayrolls.TabIndex = 4;
            this.dataGridViewPayrolls.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewPayrolls_DataError);
            // 
            // chkfor
            // 
            this.chkfor.AutoSize = true;
            this.chkfor.Location = new System.Drawing.Point(302, 15);
            this.chkfor.Name = "chkfor";
            this.chkfor.Size = new System.Drawing.Size(38, 17);
            this.chkfor.TabIndex = 8;
            this.chkfor.Text = "for";
            this.chkfor.UseVisualStyleBackColor = true;
            this.chkfor.CheckedChanged += new System.EventHandler(this.chkfor_CheckedChanged);
            // 
            // chkisopen
            // 
            this.chkisopen.AutoSize = true;
            this.chkisopen.Location = new System.Drawing.Point(346, 15);
            this.chkisopen.Name = "chkisopen";
            this.chkisopen.Size = new System.Drawing.Size(110, 17);
            this.chkisopen.TabIndex = 9;
            this.chkisopen.Text = "open payrolls only";
            this.chkisopen.UseVisualStyleBackColor = true;
            this.chkisopen.CheckedChanged += new System.EventHandler(this.chkisopen_CheckedChanged);
            // 
            // btnclearfilter
            // 
            this.btnclearfilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnclearfilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclearfilter.Location = new System.Drawing.Point(792, 11);
            this.btnclearfilter.Name = "btnclearfilter";
            this.btnclearfilter.Size = new System.Drawing.Size(82, 23);
            this.btnclearfilter.TabIndex = 10;
            this.btnclearfilter.Text = "clear filter";
            this.btnclearfilter.UseVisualStyleBackColor = false;
            this.btnclearfilter.Click += new System.EventHandler(this.btnclearfilter_Click);
            // 
            // Approved
            // 
            this.Approved.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Approved.DataPropertyName = "Approved";
            this.Approved.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Approved.HeaderText = "Approved";
            this.Approved.Name = "Approved";
            this.Approved.ReadOnly = true;
            // 
            // IsOpen
            // 
            this.IsOpen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IsOpen.DataPropertyName = "IsOpen";
            this.IsOpen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IsOpen.HeaderText = "Is Open";
            this.IsOpen.Name = "IsOpen";
            this.IsOpen.ReadOnly = true;
            // 
            // Processed
            // 
            this.Processed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Processed.DataPropertyName = "Processed";
            this.Processed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Processed.HeaderText = "Processed";
            this.Processed.Name = "Processed";
            this.Processed.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Year";
            this.dataGridViewTextBoxColumn1.HeaderText = "Year";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Period";
            this.dataGridViewTextBoxColumn2.HeaderText = "Period";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DateRun";
            this.dataGridViewTextBoxColumn3.HeaderText = "Date Run";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "RunBy";
            this.dataGridViewTextBoxColumn4.HeaderText = "RunBy";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // Year
            // 
            this.Year.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Year.DataPropertyName = "Year";
            this.Year.HeaderText = "Year";
            this.Year.Name = "Year";
            this.Year.ReadOnly = true;
            // 
            // Period
            // 
            this.Period.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Period.DataPropertyName = "Period";
            this.Period.HeaderText = "Period";
            this.Period.Name = "Period";
            this.Period.ReadOnly = true;
            // 
            // dDateRun
            // 
            this.dDateRun.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dDateRun.DataPropertyName = "DateRun";
            this.dDateRun.HeaderText = "Date Run";
            this.dDateRun.Name = "dDateRun";
            this.dDateRun.ReadOnly = true;
            // 
            // RunByk
            // 
            this.RunByk.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RunByk.DataPropertyName = "RunBy";
            this.RunByk.HeaderText = "RunBy";
            this.RunByk.Name = "RunByk";
            this.RunByk.ReadOnly = true;
            // 
            // Payrolls
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1010, 536);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Payrolls";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payrolls";
            this.Load += new System.EventHandler(this.Payrolls_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayrolls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePayrolls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bindingSourcePayrolls;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbopayrollyears;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridViewPayrolls;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboemployer;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.CheckBox chkfor;
        private System.Windows.Forms.CheckBox chkisopen;
        private System.Windows.Forms.Button btnclearfilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Year;
        private System.Windows.Forms.DataGridViewTextBoxColumn Period;
        private System.Windows.Forms.DataGridViewTextBoxColumn dDateRun;
        private System.Windows.Forms.DataGridViewTextBoxColumn RunByk;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Approved;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsOpen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Processed;
    }
}
