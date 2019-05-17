namespace winSBPayroll.Forms
{
    partial class HrlyPay
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HrlyPay));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.bindingSourceHourlyPayments = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRecordInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHrlyPayTotals = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridHourlyPayments = new System.Windows.Forms.DataGridView();
            this.EmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkDate = new winSBPayroll.Forms.CalendarColumn();
            this.WorkHours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RatePerHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceHourlyPayments)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHourlyPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.SandyBrown;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(473, 15);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.SandyBrown;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(575, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 329);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(671, 44);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRecordInfo);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lblHrlyPayTotals);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(671, 44);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // lblRecordInfo
            // 
            this.lblRecordInfo.AutoSize = true;
            this.lblRecordInfo.BackColor = System.Drawing.Color.SandyBrown;
            this.lblRecordInfo.Location = new System.Drawing.Point(9, 16);
            this.lblRecordInfo.Name = "lblRecordInfo";
            this.lblRecordInfo.Size = new System.Drawing.Size(237, 13);
            this.lblRecordInfo.TabIndex = 5;
            this.lblRecordInfo.Text = "Hourly Pay record for <<>> for the month of <<>>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.SandyBrown;
            this.label2.Location = new System.Drawing.Point(343, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Totals";
            // 
            // lblHrlyPayTotals
            // 
            this.lblHrlyPayTotals.AutoSize = true;
            this.lblHrlyPayTotals.BackColor = System.Drawing.Color.SandyBrown;
            this.lblHrlyPayTotals.Location = new System.Drawing.Point(394, 16);
            this.lblHrlyPayTotals.Name = "lblHrlyPayTotals";
            this.lblHrlyPayTotals.Size = new System.Drawing.Size(13, 13);
            this.lblHrlyPayTotals.TabIndex = 8;
            this.lblHrlyPayTotals.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridHourlyPayments);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(671, 285);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // dataGridHourlyPayments
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridHourlyPayments.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridHourlyPayments.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridHourlyPayments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridHourlyPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridHourlyPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmpNo,
            this.WorkDate,
            this.WorkHours,
            this.RatePerHour,
            this.TotalPay});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridHourlyPayments.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridHourlyPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridHourlyPayments.Location = new System.Drawing.Point(3, 16);
            this.dataGridHourlyPayments.MultiSelect = false;
            this.dataGridHourlyPayments.Name = "dataGridHourlyPayments";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridHourlyPayments.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridHourlyPayments.Size = new System.Drawing.Size(665, 266);
            this.dataGridHourlyPayments.TabIndex = 0;
            this.dataGridHourlyPayments.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridHourlyPayments_CellEnter);
            this.dataGridHourlyPayments.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridHourlyPayments_CellValidating);
            this.dataGridHourlyPayments.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridHourlyPayments_DefaultValuesNeeded);
            this.dataGridHourlyPayments.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridHourlyPayments_RowLeave);
            // 
            // EmpNo
            // 
            this.EmpNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.EmpNo.DataPropertyName = "EmpNo";
            this.EmpNo.HeaderText = "EmpNo";
            this.EmpNo.Name = "EmpNo";
            this.EmpNo.ReadOnly = true;
            this.EmpNo.Visible = false;
            // 
            // WorkDate
            // 
            this.WorkDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.WorkDate.DataPropertyName = "WorkDate";
            dataGridViewCellStyle3.Format = "D";
            this.WorkDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.WorkDate.HeaderText = "Work Date";
            this.WorkDate.Name = "WorkDate";
            this.WorkDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.WorkDate.Width = 150;
            // 
            // WorkHours
            // 
            this.WorkHours.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.WorkHours.DataPropertyName = "WorkHours";
            this.WorkHours.HeaderText = "No of Hrs";
            this.WorkHours.Name = "WorkHours";
            // 
            // RatePerHour
            // 
            this.RatePerHour.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RatePerHour.DataPropertyName = "RatePerHour";
            this.RatePerHour.HeaderText = "Rate/Hr";
            this.RatePerHour.Name = "RatePerHour";
            // 
            // TotalPay
            // 
            this.TotalPay.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TotalPay.DataPropertyName = "TotalPay";
            this.TotalPay.HeaderText = "Total Amount";
            this.TotalPay.Name = "TotalPay";
            this.TotalPay.ReadOnly = true;
            // 
            // HrlyPay
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(671, 373);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HrlyPay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hourly Pay";
            this.Load += new System.EventHandler(this.HrlyPay_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceHourlyPayments)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHourlyPayments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.BindingSource bindingSourceHourlyPayments;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblRecordInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblHrlyPayTotals;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridHourlyPayments;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpNo;
        private CalendarColumn WorkDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkHours;
        private System.Windows.Forms.DataGridViewTextBoxColumn RatePerHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalPay;
    }
}