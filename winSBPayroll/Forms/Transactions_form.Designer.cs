namespace winSBPayroll.Forms
{
    partial class Transactions_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Transactions_form));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnemployee_transactions = new System.Windows.Forms.ToolStripMenuItem();
            this.payslipDetailsTempToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.payslipDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.payslipMasterTempToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.payslipMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblrunningtime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbltimelapsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_payslip_master = new System.Windows.Forms.Button();
            this.btn_employee_transactions = new System.Windows.Forms.Button();
            this.btn_payslip_details_temp = new System.Windows.Forms.Button();
            this.btn_payslip_master_temp = new System.Windows.Forms.Button();
            this.btn_payslip_details = new System.Windows.Forms.Button();
            this.txtlog = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnemployee_transactions,
            this.payslipDetailsTempToolStripMenuItem,
            this.payslipDetailsToolStripMenuItem,
            this.payslipMasterTempToolStripMenuItem,
            this.payslipMasterToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(919, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnemployee_transactions
            // 
            this.btnemployee_transactions.Name = "btnemployee_transactions";
            this.btnemployee_transactions.Size = new System.Drawing.Size(139, 20);
            this.btnemployee_transactions.Text = "&Employee Transactions";
            this.btnemployee_transactions.Click += new System.EventHandler(this.btnemployee_transactions_Click);
            // 
            // payslipDetailsTempToolStripMenuItem
            // 
            this.payslipDetailsTempToolStripMenuItem.Name = "payslipDetailsTempToolStripMenuItem";
            this.payslipDetailsTempToolStripMenuItem.Size = new System.Drawing.Size(126, 20);
            this.payslipDetailsTempToolStripMenuItem.Text = "Payslip D&etails Temp";
            this.payslipDetailsTempToolStripMenuItem.Click += new System.EventHandler(this.payslipDetailsTempToolStripMenuItem_Click);
            // 
            // payslipDetailsToolStripMenuItem
            // 
            this.payslipDetailsToolStripMenuItem.Name = "payslipDetailsToolStripMenuItem";
            this.payslipDetailsToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.payslipDetailsToolStripMenuItem.Text = "&Payslip Details";
            this.payslipDetailsToolStripMenuItem.Click += new System.EventHandler(this.payslipDetailsToolStripMenuItem_Click);
            // 
            // payslipMasterTempToolStripMenuItem
            // 
            this.payslipMasterTempToolStripMenuItem.Name = "payslipMasterTempToolStripMenuItem";
            this.payslipMasterTempToolStripMenuItem.Size = new System.Drawing.Size(127, 20);
            this.payslipMasterTempToolStripMenuItem.Text = "Pays&lip Master Temp";
            this.payslipMasterTempToolStripMenuItem.Click += new System.EventHandler(this.payslipMasterTempToolStripMenuItem_Click);
            // 
            // payslipMasterToolStripMenuItem
            // 
            this.payslipMasterToolStripMenuItem.Name = "payslipMasterToolStripMenuItem";
            this.payslipMasterToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.payslipMasterToolStripMenuItem.Text = "Payslip &Master";
            this.payslipMasterToolStripMenuItem.Click += new System.EventHandler(this.payslipMasterToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblrunningtime,
            this.toolStripStatusLabel1,
            this.lbltimelapsed});
            this.statusStrip1.Location = new System.Drawing.Point(0, 559);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(919, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblrunningtime
            // 
            this.lblrunningtime.Name = "lblrunningtime";
            this.lblrunningtime.Size = new System.Drawing.Size(76, 17);
            this.lblrunningtime.Text = "running time";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusLabel1.Text = "          ";
            // 
            // lbltimelapsed
            // 
            this.lbltimelapsed.Name = "lbltimelapsed";
            this.lbltimelapsed.Size = new System.Drawing.Size(71, 17);
            this.lbltimelapsed.Text = "elapsedtime";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_exit);
            this.groupBox1.Controls.Add(this.btn_payslip_master);
            this.groupBox1.Controls.Add(this.btn_employee_transactions);
            this.groupBox1.Controls.Add(this.btn_payslip_details_temp);
            this.groupBox1.Controls.Add(this.btn_payslip_master_temp);
            this.groupBox1.Controls.Add(this.btn_payslip_details);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 535);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btn_exit
            // 
            this.btn_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_exit.Location = new System.Drawing.Point(20, 358);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(209, 49);
            this.btn_exit.TabIndex = 5;
            this.btn_exit.Text = "E&xit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_payslip_master
            // 
            this.btn_payslip_master.Location = new System.Drawing.Point(20, 292);
            this.btn_payslip_master.Name = "btn_payslip_master";
            this.btn_payslip_master.Size = new System.Drawing.Size(209, 49);
            this.btn_payslip_master.TabIndex = 4;
            this.btn_payslip_master.Text = "Payslip &Master";
            this.btn_payslip_master.UseVisualStyleBackColor = true;
            this.btn_payslip_master.Click += new System.EventHandler(this.btn_payslip_master_Click);
            // 
            // btn_employee_transactions
            // 
            this.btn_employee_transactions.Location = new System.Drawing.Point(20, 28);
            this.btn_employee_transactions.Name = "btn_employee_transactions";
            this.btn_employee_transactions.Size = new System.Drawing.Size(209, 49);
            this.btn_employee_transactions.TabIndex = 0;
            this.btn_employee_transactions.Text = "&Employee Transactions";
            this.btn_employee_transactions.UseVisualStyleBackColor = true;
            this.btn_employee_transactions.Click += new System.EventHandler(this.btn_employee_transactions_Click);
            // 
            // btn_payslip_details_temp
            // 
            this.btn_payslip_details_temp.Location = new System.Drawing.Point(20, 94);
            this.btn_payslip_details_temp.Name = "btn_payslip_details_temp";
            this.btn_payslip_details_temp.Size = new System.Drawing.Size(209, 49);
            this.btn_payslip_details_temp.TabIndex = 3;
            this.btn_payslip_details_temp.Text = "Payslip D&etails Temp";
            this.btn_payslip_details_temp.UseVisualStyleBackColor = true;
            this.btn_payslip_details_temp.Click += new System.EventHandler(this.btn_payslip_details_temp_Click);
            // 
            // btn_payslip_master_temp
            // 
            this.btn_payslip_master_temp.Location = new System.Drawing.Point(20, 226);
            this.btn_payslip_master_temp.Name = "btn_payslip_master_temp";
            this.btn_payslip_master_temp.Size = new System.Drawing.Size(209, 49);
            this.btn_payslip_master_temp.TabIndex = 1;
            this.btn_payslip_master_temp.Text = "Pays&lip Master Temp";
            this.btn_payslip_master_temp.UseVisualStyleBackColor = true;
            this.btn_payslip_master_temp.Click += new System.EventHandler(this.btn_payslip_master_temp_Click);
            // 
            // btn_payslip_details
            // 
            this.btn_payslip_details.Location = new System.Drawing.Point(20, 160);
            this.btn_payslip_details.Name = "btn_payslip_details";
            this.btn_payslip_details.Size = new System.Drawing.Size(209, 49);
            this.btn_payslip_details.TabIndex = 2;
            this.btn_payslip_details.Text = "&Payslip Details";
            this.btn_payslip_details.UseVisualStyleBackColor = true;
            this.btn_payslip_details.Click += new System.EventHandler(this.btn_payslip_details_Click);
            // 
            // txtlog
            // 
            this.txtlog.BackColor = System.Drawing.Color.Black;
            this.txtlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.ForeColor = System.Drawing.Color.Lime;
            this.txtlog.Location = new System.Drawing.Point(3, 16);
            this.txtlog.Name = "txtlog";
            this.txtlog.Size = new System.Drawing.Size(620, 516);
            this.txtlog.TabIndex = 0;
            this.txtlog.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtlog);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(293, 24);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(626, 535);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // Transactions_form
            // 
            this.AcceptButton = this.btn_employee_transactions;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btn_exit;
            this.ClientSize = new System.Drawing.Size(919, 581);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Transactions_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transactions";
            this.Load += new System.EventHandler(this.transactions_form_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnemployee_transactions;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblrunningtime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lbltimelapsed;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtlog;
        private System.Windows.Forms.ToolStripMenuItem payslipDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem payslipMasterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem payslipDetailsTempToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem payslipMasterTempToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_payslip_master;
        private System.Windows.Forms.Button btn_payslip_details_temp;
        private System.Windows.Forms.Button btn_payslip_details;
        private System.Windows.Forms.Button btn_payslip_master_temp;
        private System.Windows.Forms.Button btn_employee_transactions;
    }
}