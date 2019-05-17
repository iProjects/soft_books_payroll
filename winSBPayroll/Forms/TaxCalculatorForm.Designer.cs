namespace winSBPayroll.Forms
{
    partial class TaxCalculatorForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaxCalculatorForm));
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTaxCalculator = new System.Windows.Forms.DataGridView();
            this.txtTaxablePay = new System.Windows.Forms.TextBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblGrossTax = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BracketId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FromAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AmountTaxed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTaxCalculator)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Taxable Pay*";
            // 
            // dataGridViewTaxCalculator
            // 
            this.dataGridViewTaxCalculator.AllowUserToAddRows = false;
            this.dataGridViewTaxCalculator.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.dataGridViewTaxCalculator.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewTaxCalculator.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTaxCalculator.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridViewTaxCalculator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTaxCalculator.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BracketId,
            this.FromAmt,
            this.ToAmt,
            this.Rate,
            this.AmountTaxed,
            this.Tax});
            this.dataGridViewTaxCalculator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewTaxCalculator.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewTaxCalculator.MultiSelect = false;
            this.dataGridViewTaxCalculator.Name = "dataGridViewTaxCalculator";
            this.dataGridViewTaxCalculator.ReadOnly = true;
            this.dataGridViewTaxCalculator.Size = new System.Drawing.Size(690, 260);
            this.dataGridViewTaxCalculator.TabIndex = 2;
            // 
            // txtTaxablePay
            // 
            this.txtTaxablePay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTaxablePay.Location = new System.Drawing.Point(84, 19);
            this.txtTaxablePay.Name = "txtTaxablePay";
            this.txtTaxablePay.Size = new System.Drawing.Size(302, 20);
            this.txtTaxablePay.TabIndex = 0;
            this.txtTaxablePay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaxablePay_KeyDown);
            this.txtTaxablePay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTaxablePay_KeyPress);
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnCalculate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculate.Location = new System.Drawing.Point(596, 19);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 1;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(596, 58);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblGrossTax
            // 
            this.lblGrossTax.AutoSize = true;
            this.lblGrossTax.Location = new System.Drawing.Point(85, 63);
            this.lblGrossTax.Name = "lblGrossTax";
            this.lblGrossTax.Size = new System.Drawing.Size(13, 13);
            this.lblGrossTax.TabIndex = 10;
            this.lblGrossTax.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Gross Tax";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtTaxablePay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblGrossTax);
            this.groupBox1.Controls.Add(this.btnCalculate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(696, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewTaxCalculator);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 94);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(696, 279);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Bracket";
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            this.dataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn1.HeaderText = "BracketId";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 108;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "From";
            dataGridViewCellStyle9.Format = "C2";
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTextBoxColumn2.HeaderText = "FromAmt";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 108;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "To";
            dataGridViewCellStyle10.Format = "C2";
            this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewTextBoxColumn3.HeaderText = "ToAmt";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 108;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Rate";
            dataGridViewCellStyle11.Format = "##%";
            dataGridViewCellStyle11.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewTextBoxColumn4.HeaderText = "Rate";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 107;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "AmtTaxed";
            dataGridViewCellStyle12.Format = "C2";
            this.dataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewTextBoxColumn5.HeaderText = "AmountTaxed";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 108;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Tax";
            dataGridViewCellStyle13.Format = "C2";
            dataGridViewCellStyle13.NullValue = null;
            this.dataGridViewTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewTextBoxColumn6.HeaderText = "Tax";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 108;
            // 
            // BracketId
            // 
            this.BracketId.DataPropertyName = "Bracket";
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.BracketId.DefaultCellStyle = dataGridViewCellStyle2;
            this.BracketId.HeaderText = "BracketId";
            this.BracketId.Name = "BracketId";
            this.BracketId.ReadOnly = true;
            // 
            // FromAmt
            // 
            this.FromAmt.DataPropertyName = "From";
            dataGridViewCellStyle3.Format = "C2";
            this.FromAmt.DefaultCellStyle = dataGridViewCellStyle3;
            this.FromAmt.HeaderText = "FromAmt";
            this.FromAmt.Name = "FromAmt";
            this.FromAmt.ReadOnly = true;
            // 
            // ToAmt
            // 
            this.ToAmt.DataPropertyName = "To";
            dataGridViewCellStyle4.Format = "C2";
            this.ToAmt.DefaultCellStyle = dataGridViewCellStyle4;
            this.ToAmt.HeaderText = "ToAmt";
            this.ToAmt.Name = "ToAmt";
            this.ToAmt.ReadOnly = true;
            // 
            // Rate
            // 
            this.Rate.DataPropertyName = "Rate";
            dataGridViewCellStyle5.Format = "##%";
            dataGridViewCellStyle5.NullValue = null;
            this.Rate.DefaultCellStyle = dataGridViewCellStyle5;
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            // 
            // AmountTaxed
            // 
            this.AmountTaxed.DataPropertyName = "AmtTaxed";
            dataGridViewCellStyle6.Format = "C2";
            this.AmountTaxed.DefaultCellStyle = dataGridViewCellStyle6;
            this.AmountTaxed.HeaderText = "AmountTaxed";
            this.AmountTaxed.Name = "AmountTaxed";
            this.AmountTaxed.ReadOnly = true;
            // 
            // Tax
            // 
            this.Tax.DataPropertyName = "Tax";
            dataGridViewCellStyle7.Format = "C2";
            dataGridViewCellStyle7.NullValue = null;
            this.Tax.DefaultCellStyle = dataGridViewCellStyle7;
            this.Tax.HeaderText = "Tax";
            this.Tax.Name = "Tax";
            this.Tax.ReadOnly = true;
            // 
            // TaxCalculatorForm
            // 
            this.AcceptButton = this.btnCalculate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(696, 373);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaxCalculatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tax Calculator";
            this.Load += new System.EventHandler(this.TaxCalculatorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTaxCalculator)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewTaxCalculator;
        private System.Windows.Forms.TextBox txtTaxablePay;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblGrossTax;
        private System.Windows.Forms.DataGridViewTextBoxColumn BracketId;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AmountTaxed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }
}