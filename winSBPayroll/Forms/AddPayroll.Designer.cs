namespace winSBPayroll.Forms
{
    partial class AddPayroll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPayroll));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtDateRun = new System.Windows.Forms.TextBox();
            this.txtRunBy = new System.Windows.Forms.TextBox();
            this.cbEmployer = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cboPeriod = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(275, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(116, 19);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 194);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Run By*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Period in Months*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Year*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Owner*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 154);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 10;
            this.label6.Tag = "";
            this.label6.Text = "Date Run*";
            // 
            // txtYear
            // 
            this.txtYear.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtYear.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtYear.Location = new System.Drawing.Point(116, 71);
            this.txtYear.MaxLength = 4;
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(234, 20);
            this.txtYear.TabIndex = 1;
            // 
            // txtDateRun
            // 
            this.txtDateRun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDateRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtDateRun.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDateRun.Location = new System.Drawing.Point(116, 150);
            this.txtDateRun.MaxLength = 10;
            this.txtDateRun.Name = "txtDateRun";
            this.txtDateRun.Size = new System.Drawing.Size(234, 20);
            this.txtDateRun.TabIndex = 3;
            // 
            // txtRunBy
            // 
            this.txtRunBy.BackColor = System.Drawing.SystemColors.Window;
            this.txtRunBy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRunBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtRunBy.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtRunBy.Location = new System.Drawing.Point(116, 189);
            this.txtRunBy.MaxLength = 10;
            this.txtRunBy.Name = "txtRunBy";
            this.txtRunBy.ReadOnly = true;
            this.txtRunBy.Size = new System.Drawing.Size(234, 20);
            this.txtRunBy.TabIndex = 4;
            // 
            // cbEmployer
            // 
            this.cbEmployer.BackColor = System.Drawing.Color.White;
            this.cbEmployer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmployer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbEmployer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbEmployer.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbEmployer.FormattingEnabled = true;
            this.cbEmployer.Location = new System.Drawing.Point(116, 110);
            this.cbEmployer.Name = "cbEmployer";
            this.cbEmployer.Size = new System.Drawing.Size(237, 21);
            this.cbEmployer.TabIndex = 2;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // cboPeriod
            // 
            this.cboPeriod.BackColor = System.Drawing.Color.White;
            this.cboPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPeriod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cboPeriod.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cboPeriod.FormattingEnabled = true;
            this.cboPeriod.Location = new System.Drawing.Point(116, 31);
            this.cboPeriod.Name = "cboPeriod";
            this.cboPeriod.Size = new System.Drawing.Size(234, 21);
            this.cboPeriod.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 252);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 56);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRunBy);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboPeriod);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbEmployer);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtDateRun);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtYear);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(400, 252);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // AddPayroll
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(400, 308);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddPayroll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Payroll";
            this.Load += new System.EventHandler(this.AddPayroll_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.TextBox txtDateRun;
        private System.Windows.Forms.TextBox txtRunBy;
        private System.Windows.Forms.ComboBox cbEmployer;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ComboBox cboPeriod;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}