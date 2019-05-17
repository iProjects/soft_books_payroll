namespace winSBPayroll.Forms
{
    partial class EditPayrollItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditPayrollItem));
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPayrollItemId = new System.Windows.Forms.TextBox();
            this.cbItemType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkAddToPension = new System.Windows.Forms.CheckBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.txtPayableTo = new System.Windows.Forms.TextBox();
            this.cbTaxTracking = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.bindingSourceItemType = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceTaxTracking = new System.Windows.Forms.BindingSource(this.components);
            this.txtGlAccount = new System.Windows.Forms.TextBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkIsDefault = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceItemType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTaxTracking)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Location = new System.Drawing.Point(116, 19);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(234, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Description*";
            // 
            // txtPayrollItemId
            // 
            this.txtPayrollItemId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPayrollItemId.Enabled = false;
            this.txtPayrollItemId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPayrollItemId.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPayrollItemId.Location = new System.Drawing.Point(106, 29);
            this.txtPayrollItemId.MaxLength = 150;
            this.txtPayrollItemId.Name = "txtPayrollItemId";
            this.txtPayrollItemId.Size = new System.Drawing.Size(264, 20);
            this.txtPayrollItemId.TabIndex = 0;
            // 
            // cbItemType
            // 
            this.cbItemType.BackColor = System.Drawing.Color.White;
            this.cbItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemType.DropDownWidth = 190;
            this.cbItemType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbItemType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbItemType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbItemType.FormattingEnabled = true;
            this.cbItemType.Location = new System.Drawing.Point(106, 62);
            this.cbItemType.Name = "cbItemType";
            this.cbItemType.Size = new System.Drawing.Size(262, 21);
            this.cbItemType.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "GL Account";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Payable To";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tax Tracking*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Item Type*";
            // 
            // chkAddToPension
            // 
            this.chkAddToPension.AutoSize = true;
            this.chkAddToPension.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAddToPension.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkAddToPension.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkAddToPension.Location = new System.Drawing.Point(106, 264);
            this.chkAddToPension.Name = "chkAddToPension";
            this.chkAddToPension.Size = new System.Drawing.Size(99, 17);
            this.chkAddToPension.TabIndex = 7;
            this.chkAddToPension.Text = "Add To Pension";
            this.chkAddToPension.UseVisualStyleBackColor = true;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkActive.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkActive.Location = new System.Drawing.Point(106, 233);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(53, 17);
            this.chkActive.TabIndex = 6;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // txtPayableTo
            // 
            this.txtPayableTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPayableTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtPayableTo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPayableTo.Location = new System.Drawing.Point(106, 134);
            this.txtPayableTo.MaxLength = 20;
            this.txtPayableTo.Name = "txtPayableTo";
            this.txtPayableTo.Size = new System.Drawing.Size(264, 20);
            this.txtPayableTo.TabIndex = 3;
            // 
            // cbTaxTracking
            // 
            this.cbTaxTracking.BackColor = System.Drawing.Color.White;
            this.cbTaxTracking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTaxTracking.DropDownWidth = 150;
            this.cbTaxTracking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbTaxTracking.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cbTaxTracking.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cbTaxTracking.FormattingEnabled = true;
            this.cbTaxTracking.Location = new System.Drawing.Point(106, 98);
            this.cbTaxTracking.Name = "cbTaxTracking";
            this.cbTaxTracking.Size = new System.Drawing.Size(262, 21);
            this.cbTaxTracking.TabIndex = 2;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtGlAccount
            // 
            this.txtGlAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGlAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtGlAccount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtGlAccount.Location = new System.Drawing.Point(106, 167);
            this.txtGlAccount.MaxLength = 10;
            this.txtGlAccount.Name = "txtGlAccount";
            this.txtGlAccount.Size = new System.Drawing.Size(262, 20);
            this.txtGlAccount.TabIndex = 4;
            // 
            // txtReference
            // 
            this.txtReference.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtReference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtReference.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtReference.Location = new System.Drawing.Point(106, 200);
            this.txtReference.MaxLength = 4;
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(264, 20);
            this.txtReference.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Reference";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnUpdate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 325);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(424, 61);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkIsDefault);
            this.groupBox2.Controls.Add(this.txtReference);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtPayrollItemId);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbItemType);
            this.groupBox2.Controls.Add(this.txtGlAccount);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbTaxTracking);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtPayableTo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.chkActive);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.chkAddToPension);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(424, 325);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // chkIsDefault
            // 
            this.chkIsDefault.AutoSize = true;
            this.chkIsDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIsDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.chkIsDefault.ForeColor = System.Drawing.SystemColors.WindowText;
            this.chkIsDefault.Location = new System.Drawing.Point(106, 295);
            this.chkIsDefault.Name = "chkIsDefault";
            this.chkIsDefault.Size = new System.Drawing.Size(68, 17);
            this.chkIsDefault.TabIndex = 9;
            this.chkIsDefault.Text = "Is Default";
            this.chkIsDefault.UseVisualStyleBackColor = true;
            // 
            // EditPayrollItem
            // 
            this.AcceptButton = this.btnUpdate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(424, 386);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditPayrollItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EditPayrollItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceItemType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTaxTracking)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPayrollItemId;
        private System.Windows.Forms.ComboBox cbItemType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkAddToPension;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.TextBox txtPayableTo;
        private System.Windows.Forms.ComboBox cbTaxTracking;
        private System.Windows.Forms.BindingSource bindingSourceItemType;
        private System.Windows.Forms.BindingSource bindingSourceTaxTracking;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.TextBox txtGlAccount;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkIsDefault;
    }
}