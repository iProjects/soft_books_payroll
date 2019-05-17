namespace winSBPayroll.Forms
{
    partial class AddEmployeeLoan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddEmployeeLoan));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkTrackYTD = new System.Windows.Forms.CheckBox();
            this.chkRecurrent = new System.Windows.Forms.CheckBox();
            this.txtAmoundeductable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboLoanType = new System.Windows.Forms.ComboBox();
            this.cboEmployee = new System.Windows.Forms.ComboBox();
            this.txtAmountBorrowed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.btnClose.Location = new System.Drawing.Point(282, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(145, 16);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 273);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(422, 51);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkTrackYTD);
            this.groupBox2.Controls.Add(this.chkRecurrent);
            this.groupBox2.Controls.Add(this.txtAmoundeductable);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cboLoanType);
            this.groupBox2.Controls.Add(this.cboEmployee);
            this.groupBox2.Controls.Add(this.txtAmountBorrowed);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(422, 273);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // chkTrackYTD
            // 
            this.chkTrackYTD.AutoSize = true;
            this.chkTrackYTD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTrackYTD.Location = new System.Drawing.Point(145, 239);
            this.chkTrackYTD.Name = "chkTrackYTD";
            this.chkTrackYTD.Size = new System.Drawing.Size(76, 17);
            this.chkTrackYTD.TabIndex = 5;
            this.chkTrackYTD.Text = "Track YTD";
            this.chkTrackYTD.UseVisualStyleBackColor = true;
            // 
            // chkRecurrent
            // 
            this.chkRecurrent.AutoSize = true;
            this.chkRecurrent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRecurrent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRecurrent.Location = new System.Drawing.Point(145, 202);
            this.chkRecurrent.Name = "chkRecurrent";
            this.chkRecurrent.Size = new System.Drawing.Size(91, 20);
            this.chkRecurrent.TabIndex = 4;
            this.chkRecurrent.Text = "Recurrent";
            this.chkRecurrent.UseVisualStyleBackColor = true;
            // 
            // txtAmoundeductable
            // 
            this.txtAmoundeductable.BackColor = System.Drawing.Color.White;
            this.txtAmoundeductable.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmoundeductable.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmoundeductable.Location = new System.Drawing.Point(145, 159);
            this.txtAmoundeductable.Name = "txtAmoundeductable";
            this.txtAmoundeductable.Size = new System.Drawing.Size(212, 21);
            this.txtAmoundeductable.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Amount Deducted Monthly*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Total Amount Borrowed*";
            // 
            // cboLoanType
            // 
            this.cboLoanType.BackColor = System.Drawing.Color.White;
            this.cboLoanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoanType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboLoanType.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLoanType.ForeColor = System.Drawing.Color.Blue;
            this.cboLoanType.FormattingEnabled = true;
            this.cboLoanType.Location = new System.Drawing.Point(145, 70);
            this.cboLoanType.Name = "cboLoanType";
            this.cboLoanType.Size = new System.Drawing.Size(212, 23);
            this.cboLoanType.TabIndex = 1;
            // 
            // cboEmployee
            // 
            this.cboEmployee.BackColor = System.Drawing.Color.White;
            this.cboEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboEmployee.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmployee.ForeColor = System.Drawing.Color.Blue;
            this.cboEmployee.FormattingEnabled = true;
            this.cboEmployee.Location = new System.Drawing.Point(145, 24);
            this.cboEmployee.Name = "cboEmployee";
            this.cboEmployee.Size = new System.Drawing.Size(212, 23);
            this.cboEmployee.TabIndex = 0;
            // 
            // txtAmountBorrowed
            // 
            this.txtAmountBorrowed.BackColor = System.Drawing.Color.White;
            this.txtAmountBorrowed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAmountBorrowed.Location = new System.Drawing.Point(145, 116);
            this.txtAmountBorrowed.MaxLength = 50;
            this.txtAmountBorrowed.Name = "txtAmountBorrowed";
            this.txtAmountBorrowed.Size = new System.Drawing.Size(212, 20);
            this.txtAmountBorrowed.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Loan Type*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Select Employee*";
            // 
            // AddEmployeeLoan
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(422, 324);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddEmployeeLoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LOAN";
            this.Load += new System.EventHandler(this.AddEmployeeLoan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkTrackYTD;
        private System.Windows.Forms.CheckBox chkRecurrent;
        private System.Windows.Forms.TextBox txtAmoundeductable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboLoanType;
        private System.Windows.Forms.ComboBox cboEmployee;
        private System.Windows.Forms.TextBox txtAmountBorrowed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}