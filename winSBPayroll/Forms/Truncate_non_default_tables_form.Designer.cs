namespace winSBPayroll.Forms
{
    partial class Truncate_non_default_tables_form
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.LinkLabel();
            this.btn_truncate_database = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar_connect = new System.Windows.Forms.ProgressBar();
            this.cbo_database = new System.Windows.Forms.ComboBox();
            this.cbo_servers = new System.Windows.Forms.ComboBox();
            this.chkRememberServerName = new System.Windows.Forms.CheckBox();
            this.chkRememberPassword = new System.Windows.Forms.CheckBox();
            this.chkRemeberUserName = new System.Windows.Forms.CheckBox();
            this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.btnrefreshdatabases = new System.Windows.Forms.LinkLabel();
            this.btnGetServerList = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkshowpassword = new System.Windows.Forms.CheckBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_user_name = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripSystemNotification = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtlogs = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.contextMenuStripSystemNotification.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btn_truncate_database);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 502);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.LinkColor = System.Drawing.Color.Yellow;
            this.btnClose.Location = new System.Drawing.Point(399, 17);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 25);
            this.btnClose.TabIndex = 3;
            this.btnClose.TabStop = true;
            this.btnClose.Text = "Close";
            this.btnClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClose_LinkClicked);
            // 
            // btn_truncate_database
            // 
            this.btn_truncate_database.AutoSize = true;
            this.btn_truncate_database.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_truncate_database.LinkColor = System.Drawing.Color.Yellow;
            this.btn_truncate_database.Location = new System.Drawing.Point(157, 17);
            this.btn_truncate_database.Name = "btn_truncate_database";
            this.btn_truncate_database.Size = new System.Drawing.Size(212, 25);
            this.btn_truncate_database.TabIndex = 2;
            this.btn_truncate_database.TabStop = true;
            this.btn_truncate_database.Text = "Truncate Database";
            this.btn_truncate_database.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btn_truncate_database_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtlogs);
            this.groupBox2.Controls.Add(this.progressBar_connect);
            this.groupBox2.Controls.Add(this.cbo_database);
            this.groupBox2.Controls.Add(this.cbo_servers);
            this.groupBox2.Controls.Add(this.chkRememberServerName);
            this.groupBox2.Controls.Add(this.chkRememberPassword);
            this.groupBox2.Controls.Add(this.chkRemeberUserName);
            this.groupBox2.Controls.Add(this.chkIntegratedSecurity);
            this.groupBox2.Controls.Add(this.btnrefreshdatabases);
            this.groupBox2.Controls.Add(this.btnGetServerList);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.chkshowpassword);
            this.groupBox2.Controls.Add(this.txt_password);
            this.groupBox2.Controls.Add(this.txt_user_name);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 502);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // progressBar_connect
            // 
            this.progressBar_connect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar_connect.Location = new System.Drawing.Point(3, 476);
            this.progressBar_connect.Name = "progressBar_connect";
            this.progressBar_connect.Size = new System.Drawing.Size(477, 23);
            this.progressBar_connect.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar_connect.TabIndex = 36;
            // 
            // cbo_database
            // 
            this.cbo_database.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_database.FormattingEnabled = true;
            this.cbo_database.Location = new System.Drawing.Point(113, 181);
            this.cbo_database.Name = "cbo_database";
            this.cbo_database.Size = new System.Drawing.Size(256, 21);
            this.cbo_database.TabIndex = 35;
            // 
            // cbo_servers
            // 
            this.cbo_servers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_servers.FormattingEnabled = true;
            this.cbo_servers.Location = new System.Drawing.Point(113, 29);
            this.cbo_servers.Name = "cbo_servers";
            this.cbo_servers.Size = new System.Drawing.Size(256, 21);
            this.cbo_servers.TabIndex = 34;
            // 
            // chkRememberServerName
            // 
            this.chkRememberServerName.AutoSize = true;
            this.chkRememberServerName.Checked = true;
            this.chkRememberServerName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRememberServerName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRememberServerName.Location = new System.Drawing.Point(113, 284);
            this.chkRememberServerName.Name = "chkRememberServerName";
            this.chkRememberServerName.Size = new System.Drawing.Size(139, 17);
            this.chkRememberServerName.TabIndex = 24;
            this.chkRememberServerName.Text = "Remember Server Name";
            this.chkRememberServerName.UseVisualStyleBackColor = true;
            // 
            // chkRememberPassword
            // 
            this.chkRememberPassword.AutoSize = true;
            this.chkRememberPassword.Checked = true;
            this.chkRememberPassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRememberPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRememberPassword.Location = new System.Drawing.Point(266, 251);
            this.chkRememberPassword.Name = "chkRememberPassword";
            this.chkRememberPassword.Size = new System.Drawing.Size(123, 17);
            this.chkRememberPassword.TabIndex = 31;
            this.chkRememberPassword.Text = "Remember Password";
            this.chkRememberPassword.UseVisualStyleBackColor = true;
            // 
            // chkRemeberUserName
            // 
            this.chkRemeberUserName.AutoSize = true;
            this.chkRemeberUserName.Checked = true;
            this.chkRemeberUserName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRemeberUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRemeberUserName.Location = new System.Drawing.Point(266, 284);
            this.chkRemeberUserName.Name = "chkRemeberUserName";
            this.chkRemeberUserName.Size = new System.Drawing.Size(130, 17);
            this.chkRemeberUserName.TabIndex = 30;
            this.chkRemeberUserName.Text = "Remember User Name";
            this.chkRemeberUserName.UseVisualStyleBackColor = true;
            // 
            // chkIntegratedSecurity
            // 
            this.chkIntegratedSecurity.AutoSize = true;
            this.chkIntegratedSecurity.Checked = true;
            this.chkIntegratedSecurity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIntegratedSecurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIntegratedSecurity.Location = new System.Drawing.Point(317, 67);
            this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
            this.chkIntegratedSecurity.Size = new System.Drawing.Size(131, 17);
            this.chkIntegratedSecurity.TabIndex = 29;
            this.chkIntegratedSecurity.Text = "Use integrated security";
            this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
            this.chkIntegratedSecurity.CheckedChanged += new System.EventHandler(this.chkIntegratedSecurity_CheckedChanged);
            // 
            // btnrefreshdatabases
            // 
            this.btnrefreshdatabases.AutoSize = true;
            this.btnrefreshdatabases.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefreshdatabases.LinkColor = System.Drawing.Color.Yellow;
            this.btnrefreshdatabases.Location = new System.Drawing.Point(114, 217);
            this.btnrefreshdatabases.Name = "btnrefreshdatabases";
            this.btnrefreshdatabases.Size = new System.Drawing.Size(156, 20);
            this.btnrefreshdatabases.TabIndex = 25;
            this.btnrefreshdatabases.TabStop = true;
            this.btnrefreshdatabases.Text = "Get Database List";
            this.btnrefreshdatabases.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnrefreshdatabases_LinkClicked);
            // 
            // btnGetServerList
            // 
            this.btnGetServerList.AutoSize = true;
            this.btnGetServerList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetServerList.LinkColor = System.Drawing.Color.Yellow;
            this.btnGetServerList.Location = new System.Drawing.Point(109, 64);
            this.btnGetServerList.Name = "btnGetServerList";
            this.btnGetServerList.Size = new System.Drawing.Size(130, 20);
            this.btnGetServerList.TabIndex = 22;
            this.btnGetServerList.TabStop = true;
            this.btnGetServerList.Text = "Get Server List";
            this.btnGetServerList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnGetServerList_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Sql Servers*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Database Name*";
            // 
            // chkshowpassword
            // 
            this.chkshowpassword.AutoSize = true;
            this.chkshowpassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkshowpassword.Location = new System.Drawing.Point(113, 251);
            this.chkshowpassword.Name = "chkshowpassword";
            this.chkshowpassword.Size = new System.Drawing.Size(99, 17);
            this.chkshowpassword.TabIndex = 28;
            this.chkshowpassword.Text = "Show Password";
            this.chkshowpassword.UseVisualStyleBackColor = true;
            this.chkshowpassword.CheckedChanged += new System.EventHandler(this.chkshowpassword_CheckedChanged);
            // 
            // txt_password
            // 
            this.txt_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_password.Location = new System.Drawing.Point(113, 137);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(256, 20);
            this.txt_password.TabIndex = 27;
            // 
            // txt_user_name
            // 
            this.txt_user_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_user_name.Location = new System.Drawing.Point(113, 97);
            this.txt_user_name.Name = "txt_user_name";
            this.txt_user_name.Size = new System.Drawing.Size(256, 20);
            this.txt_user_name.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "PassWord*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "User Name*";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.Text = "notifyIcon1";
            this.appNotifyIcon.Visible = true;
            // 
            // contextMenuStripSystemNotification
            // 
            this.contextMenuStripSystemNotification.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStripSystemNotification.Name = "contextMenuStripSystemNotification";
            this.contextMenuStripSystemNotification.Size = new System.Drawing.Size(108, 54);
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.homeToolStripMenuItem.Text = "Home";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(104, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // txtlogs
            // 
            this.txtlogs.BackColor = System.Drawing.Color.Black;
            this.txtlogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtlogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtlogs.ForeColor = System.Drawing.Color.Lime;
            this.txtlogs.Location = new System.Drawing.Point(3, 328);
            this.txtlogs.Multiline = true;
            this.txtlogs.Name = "txtlogs";
            this.txtlogs.ReadOnly = true;
            this.txtlogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtlogs.Size = new System.Drawing.Size(477, 148);
            this.txtlogs.TabIndex = 37;
            // 
            // Truncate_non_default_tables_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(483, 556);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Truncate_non_default_tables_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Truncate non default tables";
            this.Load += new System.EventHandler(this.Truncate_non_default_tables_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.contextMenuStripSystemNotification.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel btnClose;
        private System.Windows.Forms.LinkLabel btn_truncate_database;
        private System.Windows.Forms.ComboBox cbo_database;
        private System.Windows.Forms.ComboBox cbo_servers;
        private System.Windows.Forms.CheckBox chkRememberServerName;
        private System.Windows.Forms.CheckBox chkRememberPassword;
        private System.Windows.Forms.CheckBox chkRemeberUserName;
        private System.Windows.Forms.CheckBox chkIntegratedSecurity;
        private System.Windows.Forms.LinkLabel btnrefreshdatabases;
        private System.Windows.Forms.LinkLabel btnGetServerList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkshowpassword;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_user_name;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar_connect;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.NotifyIcon appNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemNotification;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox txtlogs;
    }
}