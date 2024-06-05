namespace winSBPayroll.Forms
{
    partial class UploadEmployeesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UploadEmployeesForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtlog = new System.Windows.Forms.RichTextBox();
            this.dataGridView_valid_records = new System.Windows.Forms.DataGridView();
            this.dataGridView_invalid_records = new System.Windows.Forms.DataGridView();
            this.bindingSource_valid_records = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource_invalid_records = new System.Windows.Forms.BindingSource(this.components);
            this.btnUpload_from_excel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_valid_records)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invalid_records)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_valid_records)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_invalid_records)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnUpload_from_excel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1039, 64);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtlog);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 508);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1039, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView_invalid_records);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 375);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1039, 133);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "invalid records";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dataGridView_valid_records);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 64);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1039, 311);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "valid records";
            // 
            // txtlog
            // 
            this.txtlog.BackColor = System.Drawing.Color.Black;
            this.txtlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.ForeColor = System.Drawing.Color.Lime;
            this.txtlog.Location = new System.Drawing.Point(3, 16);
            this.txtlog.Name = "txtlog";
            this.txtlog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtlog.Size = new System.Drawing.Size(1033, 81);
            this.txtlog.TabIndex = 0;
            this.txtlog.Text = "";
            // 
            // dataGridView_valid_records
            // 
            this.dataGridView_valid_records.AllowUserToAddRows = false;
            this.dataGridView_valid_records.AllowUserToDeleteRows = false;
            this.dataGridView_valid_records.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_valid_records.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_valid_records.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_valid_records.Name = "dataGridView_valid_records";
            this.dataGridView_valid_records.ReadOnly = true;
            this.dataGridView_valid_records.Size = new System.Drawing.Size(1033, 292);
            this.dataGridView_valid_records.TabIndex = 0;
            // 
            // dataGridView_invalid_records
            // 
            this.dataGridView_invalid_records.AllowUserToAddRows = false;
            this.dataGridView_invalid_records.AllowUserToDeleteRows = false;
            this.dataGridView_invalid_records.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_invalid_records.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_invalid_records.Location = new System.Drawing.Point(3, 16);
            this.dataGridView_invalid_records.Name = "dataGridView_invalid_records";
            this.dataGridView_invalid_records.ReadOnly = true;
            this.dataGridView_invalid_records.Size = new System.Drawing.Size(1033, 114);
            this.dataGridView_invalid_records.TabIndex = 0;
            // 
            // btnUpload_from_excel
            // 
            this.btnUpload_from_excel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnUpload_from_excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpload_from_excel.Location = new System.Drawing.Point(21, 21);
            this.btnUpload_from_excel.Name = "btnUpload_from_excel";
            this.btnUpload_from_excel.Size = new System.Drawing.Size(117, 23);
            this.btnUpload_from_excel.TabIndex = 0;
            this.btnUpload_from_excel.Text = "&Upload_From_Excel";
            this.btnUpload_from_excel.UseVisualStyleBackColor = false;
            this.btnUpload_from_excel.Click += new System.EventHandler(this.btnUploadFromExcel_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(167, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(271, 21);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // UploadEmployeesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(1039, 608);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UploadEmployeesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload Employees";
            this.Load += new System.EventHandler(this.UploadEmployeesForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_valid_records)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_invalid_records)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_valid_records)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_invalid_records)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpload_from_excel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox txtlog;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView_invalid_records;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dataGridView_valid_records;
        private System.Windows.Forms.BindingSource bindingSource_valid_records;
        private System.Windows.Forms.BindingSource bindingSource_invalid_records;

    }
}