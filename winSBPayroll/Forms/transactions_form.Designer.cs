namespace winSBPayroll.Forms
{
    partial class transactions_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(transactions_form));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnemployee_transactions = new System.Windows.Forms.ToolStripMenuItem();
            this.payslipDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.payslipMasterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblrunningtime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbltimelapsed = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtlog = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnemployee_transactions,
            this.payslipDetailsToolStripMenuItem,
            this.payslipMasterToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1112, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnemployee_transactions
            // 
            this.btnemployee_transactions.Name = "btnemployee_transactions";
            this.btnemployee_transactions.Size = new System.Drawing.Size(138, 20);
            this.btnemployee_transactions.Text = "employee transactions";
            this.btnemployee_transactions.Click += new System.EventHandler(this.btnemployee_transactions_Click);
            // 
            // payslipDetailsToolStripMenuItem
            // 
            this.payslipDetailsToolStripMenuItem.Name = "payslipDetailsToolStripMenuItem";
            this.payslipDetailsToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.payslipDetailsToolStripMenuItem.Text = "payslip details";
            this.payslipDetailsToolStripMenuItem.Click += new System.EventHandler(this.payslipDetailsToolStripMenuItem_Click);
            // 
            // payslipMasterToolStripMenuItem
            // 
            this.payslipMasterToolStripMenuItem.Name = "payslipMasterToolStripMenuItem";
            this.payslipMasterToolStripMenuItem.Size = new System.Drawing.Size(95, 20);
            this.payslipMasterToolStripMenuItem.Text = "payslip master";
            this.payslipMasterToolStripMenuItem.Click += new System.EventHandler(this.payslipMasterToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.exitToolStripMenuItem.Text = "exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblrunningtime,
            this.toolStripStatusLabel1,
            this.lbltimelapsed});
            this.statusStrip1.Location = new System.Drawing.Point(0, 678);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1112, 22);
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
            this.groupBox1.Controls.Add(this.txtlog);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1112, 654);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtlog
            // 
            this.txtlog.BackColor = System.Drawing.Color.Black;
            this.txtlog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtlog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog.ForeColor = System.Drawing.Color.Lime;
            this.txtlog.Location = new System.Drawing.Point(3, 16);
            this.txtlog.Name = "txtlog";
            this.txtlog.Size = new System.Drawing.Size(1106, 635);
            this.txtlog.TabIndex = 0;
            this.txtlog.Text = "";
            // 
            // transactions_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(1112, 700);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "transactions_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "transactions";
            this.Load += new System.EventHandler(this.transactions_form_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
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
    }
}