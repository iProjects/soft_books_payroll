namespace winSBPayroll.Forms
{
    partial class ProcessPayroll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessPayroll));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnClosePayroll = new System.Windows.Forms.Button();
            this.btnProcessPayroll = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridViewPayrolls = new System.Windows.Forms.DataGridView();
            this.ColumnYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPeriod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel4 = new System.Windows.Forms.Panel();
            this.listBoxResults = new System.Windows.Forms.ListBox();
            this.bindingSourcePayrolls = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayrolls)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePayrolls)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LimeGreen;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnClosePayroll);
            this.panel1.Controls.Add(this.btnProcessPayroll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(842, 42);
            this.panel1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(296, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnClosePayroll
            // 
            this.btnClosePayroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClosePayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClosePayroll.Location = new System.Drawing.Point(144, 12);
            this.btnClosePayroll.Name = "btnClosePayroll";
            this.btnClosePayroll.Size = new System.Drawing.Size(105, 23);
            this.btnClosePayroll.TabIndex = 1;
            this.btnClosePayroll.Text = "Close Payroll";
            this.btnClosePayroll.UseVisualStyleBackColor = false;
            this.btnClosePayroll.Click += new System.EventHandler(this.btnClosePayroll_Click);
            // 
            // btnProcessPayroll
            // 
            this.btnProcessPayroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnProcessPayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProcessPayroll.Location = new System.Drawing.Point(12, 12);
            this.btnProcessPayroll.Name = "btnProcessPayroll";
            this.btnProcessPayroll.Size = new System.Drawing.Size(105, 23);
            this.btnProcessPayroll.TabIndex = 0;
            this.btnProcessPayroll.Text = "Process Payroll";
            this.btnProcessPayroll.UseVisualStyleBackColor = false;
            this.btnProcessPayroll.Click += new System.EventHandler(this.btnProcessPayroll_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LimeGreen;
            this.panel2.Controls.Add(this.statusStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 324);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(842, 49);
            this.panel2.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtStatus,
            this.pbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 27);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(842, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtStatus
            // 
            this.txtStatus.BackColor = System.Drawing.Color.PaleGreen;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(38, 17);
            this.txtStatus.Text = "Ready";
            // 
            // pbStatus
            // 
            this.pbStatus.BackColor = System.Drawing.Color.PaleGreen;
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(100, 16);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.panel3.Controls.Add(this.dataGridViewPayrolls);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 42);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(235, 282);
            this.panel3.TabIndex = 2;
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
            this.ColumnPeriod});
            this.dataGridViewPayrolls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPayrolls.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewPayrolls.MultiSelect = false;
            this.dataGridViewPayrolls.Name = "dataGridViewPayrolls";
            this.dataGridViewPayrolls.ReadOnly = true;
            this.dataGridViewPayrolls.Size = new System.Drawing.Size(235, 282);
            this.dataGridViewPayrolls.TabIndex = 3;
            // 
            // ColumnYear
            // 
            this.ColumnYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColumnYear.DataPropertyName = "Year";
            this.ColumnYear.HeaderText = "Year";
            this.ColumnYear.Name = "ColumnYear";
            this.ColumnYear.ReadOnly = true;
            // 
            // ColumnPeriod
            // 
            this.ColumnPeriod.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnPeriod.DataPropertyName = "Period";
            this.ColumnPeriod.HeaderText = "Period";
            this.ColumnPeriod.Name = "ColumnPeriod";
            this.ColumnPeriod.ReadOnly = true;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panel4.Controls.Add(this.listBoxResults);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(235, 42);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(607, 282);
            this.panel4.TabIndex = 3;
            // 
            // listBoxResults
            // 
            this.listBoxResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBoxResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxResults.FormattingEnabled = true;
            this.listBoxResults.Location = new System.Drawing.Point(0, 0);
            this.listBoxResults.Name = "listBoxResults";
            this.listBoxResults.Size = new System.Drawing.Size(607, 282);
            this.listBoxResults.TabIndex = 4;
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.Text = "notifyIcon1";
            this.appNotifyIcon.Visible = true;
            // 
            // ProcessPayroll
            // 
            this.AcceptButton = this.btnProcessPayroll;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(842, 373);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProcessPayroll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Process Payroll";
            this.Load += new System.EventHandler(this.ProcessPayroll_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPayrolls)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourcePayrolls)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnProcessPayroll;
        private System.Windows.Forms.DataGridView dataGridViewPayrolls;
        private System.Windows.Forms.BindingSource bindingSourcePayrolls;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtStatus;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ListBox listBoxResults;
        private System.Windows.Forms.Button btnClosePayroll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPeriod;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NotifyIcon appNotifyIcon;
    }
}