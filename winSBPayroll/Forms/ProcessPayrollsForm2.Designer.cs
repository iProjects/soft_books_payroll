namespace winSBPayroll.Forms
{
    partial class ProcessPayrollsForm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessPayrollsForm2));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClosePayroll = new System.Windows.Forms.Button();
            this.btnProcessPayroll = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressbar = new System.Windows.Forms.ToolStripProgressBar();
            this.dataGridViewPayrolls = new System.Windows.Forms.DataGridView();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.bindingSourcePayrolls = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkprocessed = new System.Windows.Forms.CheckBox();
            this.btnclearfilter = new System.Windows.Forms.Button();
            this.chkisopen = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboemployer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbopayrollyears = new System.Windows.Forms.ComboBox();
            this.txtlog = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panelPager = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnIsOpen = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnProcessed = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColumnTimeRun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1DateRun = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayrolls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePayrolls)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(976, 20);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(51, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Exit";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClosePayroll
            // 
            this.btnClosePayroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClosePayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClosePayroll.Location = new System.Drawing.Point(123, 19);
            this.btnClosePayroll.Name = "btnClosePayroll";
            this.btnClosePayroll.Size = new System.Drawing.Size(89, 23);
            this.btnClosePayroll.TabIndex = 1;
            this.btnClosePayroll.Text = "Close Payroll";
            this.btnClosePayroll.UseVisualStyleBackColor = false;
            this.btnClosePayroll.Click += new System.EventHandler(this.btnClosePayroll_Click);
            // 
            // btnProcessPayroll
            // 
            this.btnProcessPayroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnProcessPayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessPayroll.Location = new System.Drawing.Point(12, 19);
            this.btnProcessPayroll.Name = "btnProcessPayroll";
            this.btnProcessPayroll.Size = new System.Drawing.Size(94, 23);
            this.btnProcessPayroll.TabIndex = 0;
            this.btnProcessPayroll.Text = "Process Payroll";
            this.btnProcessPayroll.UseVisualStyleBackColor = false;
            this.btnProcessPayroll.Click += new System.EventHandler(this.btnProcessPayroll_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtStatus,
            this.progressbar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 505);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1049, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip";
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.PaleGreen;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(39, 17);
            this.txtStatus.Text = "Ready";
            // 
            // progressbar
            // 
            this.progressbar.BackColor = System.Drawing.Color.PaleGreen;
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(200, 16);
            // 
            // dataGridViewPayrolls
            // 
            this.dataGridViewPayrolls.AllowUserToAddRows = false;
            this.dataGridViewPayrolls.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridViewPayrolls.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPayrolls.BackgroundColor = System.Drawing.Color.LightGreen;
            this.dataGridViewPayrolls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPayrolls.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnYear,
            this.ColumnPeriod,
            this.ColumnIsOpen,
            this.ColumnProcessed,
            this.ColumnTimeRun,
            this.Column1DateRun});
            this.dataGridViewPayrolls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPayrolls.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewPayrolls.MultiSelect = false;
            this.dataGridViewPayrolls.Name = "dataGridViewPayrolls";
            this.dataGridViewPayrolls.ReadOnly = true;
            this.dataGridViewPayrolls.Size = new System.Drawing.Size(477, 431);
            this.dataGridViewPayrolls.TabIndex = 0;
            this.dataGridViewPayrolls.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridViewPayrolls_DataError);
            // 
            // listBoxResults
            // 
            this.listBoxResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.Location = new System.Drawing.Point(0, 0);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(556, 258);
            this.listBoxResults.TabIndex = 1;
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.Text = "notifyIcon1";
            this.appNotifyIcon.Visible = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkprocessed);
            this.groupBox1.Controls.Add(this.btnclearfilter);
            this.groupBox1.Controls.Add(this.chkisopen);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboemployer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbopayrollyears);
            this.groupBox1.Controls.Add(this.btnProcessPayroll);
            this.groupBox1.Controls.Add(this.btnClosePayroll);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1049, 55);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // chkprocessed
            // 
            this.chkprocessed.AutoSize = true;
            this.chkprocessed.Location = new System.Drawing.Point(746, 24);
            this.chkprocessed.Name = "chkprocessed";
            this.chkprocessed.Size = new System.Drawing.Size(75, 17);
            this.chkprocessed.TabIndex = 19;
            this.chkprocessed.Text = "processed";
            this.chkprocessed.UseVisualStyleBackColor = true;
            this.chkprocessed.CheckedChanged += new System.EventHandler(this.chkprocessed_CheckedChanged);
            // 
            // btnclearfilter
            // 
            this.btnclearfilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnclearfilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclearfilter.Location = new System.Drawing.Point(888, 20);
            this.btnclearfilter.Name = "btnclearfilter";
            this.btnclearfilter.Size = new System.Drawing.Size(66, 23);
            this.btnclearfilter.TabIndex = 18;
            this.btnclearfilter.Text = "clear filter";
            this.btnclearfilter.UseVisualStyleBackColor = false;
            this.btnclearfilter.Click += new System.EventHandler(this.btnclearfilter_Click);
            // 
            // chkisopen
            // 
            this.chkisopen.AutoSize = true;
            this.chkisopen.Location = new System.Drawing.Point(620, 23);
            this.chkisopen.Name = "chkisopen";
            this.chkisopen.Size = new System.Drawing.Size(110, 17);
            this.chkisopen.TabIndex = 17;
            this.chkisopen.Text = "open payrolls only";
            this.chkisopen.UseVisualStyleBackColor = true;
            this.chkisopen.CheckedChanged += new System.EventHandler(this.chkisopen_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(229, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Employer";
            // 
            // cboemployer
            // 
            this.cboemployer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboemployer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboemployer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboemployer.ForeColor = System.Drawing.Color.Black;
            this.cboemployer.FormattingEnabled = true;
            this.cboemployer.Location = new System.Drawing.Point(285, 19);
            this.cboemployer.Name = "cboemployer";
            this.cboemployer.Size = new System.Drawing.Size(135, 21);
            this.cboemployer.TabIndex = 14;
            this.cboemployer.SelectedIndexChanged += new System.EventHandler(this.cboemployer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(444, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Year";
            // 
            // cbopayrollyears
            // 
            this.cbopayrollyears.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbopayrollyears.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbopayrollyears.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbopayrollyears.ForeColor = System.Drawing.Color.Black;
            this.cbopayrollyears.FormattingEnabled = true;
            this.cbopayrollyears.Location = new System.Drawing.Point(479, 21);
            this.cbopayrollyears.Name = "cbopayrollyears";
            this.cbopayrollyears.Size = new System.Drawing.Size(113, 21);
            this.cbopayrollyears.TabIndex = 11;
            this.cbopayrollyears.SelectedIndexChanged += new System.EventHandler(this.cbopayrollyears_SelectedIndexChanged);
            // 
            // txtlog
            // 
            this.txtlog.BackColor = System.Drawing.Color.Black;
            this.txtlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.ForeColor = System.Drawing.Color.Lime;
            this.txtlog.Location = new System.Drawing.Point(0, 0);
            this.txtlog.Name = "txtlog";
            this.txtlog.Size = new System.Drawing.Size(556, 169);
            this.txtlog.TabIndex = 0;
            this.txtlog.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.splitContainer2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(562, 450);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 16);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listBoxResults);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.txtlog);
            this.splitContainer2.Size = new System.Drawing.Size(556, 431);
            this.splitContainer2.SplitterDistance = 258;
            this.splitContainer2.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.panelPager);
            this.groupBox3.Controls.Add(this.dataGridViewPayrolls);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(483, 450);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // panelPager
            // 
            this.panelPager.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPager.Location = new System.Drawing.Point(3, 406);
            this.panelPager.Name = "panelPager";
            this.panelPager.Size = new System.Drawing.Size(477, 41);
            this.panelPager.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1049, 450);
            this.splitContainer1.SplitterDistance = 483;
            this.splitContainer1.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Year";
            this.dataGridViewTextBoxColumn1.HeaderText = "Year";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DateRun";
            this.dataGridViewTextBoxColumn2.HeaderText = "DateRun";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 70;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Period";
            this.dataGridViewTextBoxColumn3.HeaderText = "Period";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // ColumnYear
            // 
            this.ColumnYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnYear.DataPropertyName = "Year";
            this.ColumnYear.HeaderText = "Year";
            this.ColumnYear.Name = "ColumnYear";
            this.ColumnYear.ReadOnly = true;
            this.ColumnYear.Width = 60;
            // 
            // ColumnPeriod
            // 
            this.ColumnPeriod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnPeriod.DataPropertyName = "Period";
            this.ColumnPeriod.HeaderText = "Period";
            this.ColumnPeriod.Name = "ColumnPeriod";
            this.ColumnPeriod.ReadOnly = true;
            this.ColumnPeriod.Width = 70;
            // 
            // ColumnIsOpen
            // 
            this.ColumnIsOpen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnIsOpen.DataPropertyName = "IsOpen";
            this.ColumnIsOpen.HeaderText = "IsOpen";
            this.ColumnIsOpen.Name = "ColumnIsOpen";
            this.ColumnIsOpen.ReadOnly = true;
            this.ColumnIsOpen.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnIsOpen.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnIsOpen.Width = 60;
            // 
            // ColumnProcessed
            // 
            this.ColumnProcessed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnProcessed.DataPropertyName = "Processed";
            this.ColumnProcessed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ColumnProcessed.HeaderText = "Processed";
            this.ColumnProcessed.Name = "ColumnProcessed";
            this.ColumnProcessed.ReadOnly = true;
            this.ColumnProcessed.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnProcessed.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnProcessed.Width = 60;
            // 
            // ColumnTimeRun
            // 
            this.ColumnTimeRun.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnTimeRun.DataPropertyName = "TimeRun";
            this.ColumnTimeRun.HeaderText = "Time Run";
            this.ColumnTimeRun.Name = "ColumnTimeRun";
            this.ColumnTimeRun.ReadOnly = true;
            this.ColumnTimeRun.Width = 80;
            // 
            // Column1DateRun
            // 
            this.Column1DateRun.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1DateRun.DataPropertyName = "DateRun";
            this.Column1DateRun.HeaderText = "DateRun";
            this.Column1DateRun.Name = "Column1DateRun";
            this.Column1DateRun.ReadOnly = true;
            // 
            // ProcessPayrollsForm2
            // 
            this.AcceptButton = this.btnProcessPayroll;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(1049, 527);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessPayrollsForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Payroll";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ProcessPayroll_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayrolls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePayrolls)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnProcessPayroll;
        private System.Windows.Forms.DataGridView dataGridViewPayrolls;
        private System.Windows.Forms.BindingSource bindingSourcePayrolls;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
        private System.Windows.Forms.ToolStripProgressBar progressbar;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.Button btnClosePayroll;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NotifyIcon appNotifyIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtlog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panelPager;
        private System.Windows.Forms.Button btnclearfilter;
        private System.Windows.Forms.CheckBox chkisopen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboemployer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbopayrollyears;
        private System.Windows.Forms.CheckBox chkprocessed;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPeriod;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnIsOpen;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnProcessed;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTimeRun;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1DateRun;
    }
}