namespace winSBPayroll.Forms
{
    partial class CreateNewDatabase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewDatabase));
            this.tbBTSAppUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCreateDB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextScript = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbo_servers = new System.Windows.Forms.ComboBox();
            this.progressBar_connect = new System.Windows.Forms.ProgressBar();
            this.chkshowpassword = new System.Windows.Forms.CheckBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_user_name = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.btnGetServerList = new System.Windows.Forms.LinkLabel();
            this.btnBrowseScriptFile = new System.Windows.Forms.Button();
            this.txtScriptFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtlogs = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tbBTSAppUser
            // 
            this.tbBTSAppUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBTSAppUser.Location = new System.Drawing.Point(134, 257);
            this.tbBTSAppUser.Name = "tbBTSAppUser";
            this.tbBTSAppUser.Size = new System.Drawing.Size(260, 20);
            this.tbBTSAppUser.TabIndex = 2;
            this.tbBTSAppUser.Text = "Administrator";
            this.toolTip.SetToolTip(this.tbBTSAppUser, "User");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 259);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = " Application Users*:";
            // 
            // txtDBName
            // 
            this.txtDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDBName.Location = new System.Drawing.Point(134, 216);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(261, 20);
            this.txtDBName.TabIndex = 1;
            this.txtDBName.Text = "SBPayrollDB";
            this.toolTip.SetToolTip(this.txtDBName, "new database name");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Sql Servers*:";
            // 
            // btnCreateDB
            // 
            this.btnCreateDB.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnCreateDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateDB.Location = new System.Drawing.Point(300, 19);
            this.btnCreateDB.Name = "btnCreateDB";
            this.btnCreateDB.Size = new System.Drawing.Size(115, 23);
            this.btnCreateDB.TabIndex = 0;
            this.btnCreateDB.Text = "Create Database";
            this.toolTip.SetToolTip(this.btnCreateDB, "Create new Database");
            this.btnCreateDB.UseVisualStyleBackColor = false;
            this.btnCreateDB.Click += new System.EventHandler(this.btnCreateDB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 218);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "New Database Name*:";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(433, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(58, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.toolTip.SetToolTip(this.btnClose, "Exit");
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnCreateDB);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 635);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 54);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.txtlogs);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 635);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 383);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(497, 113);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextScript);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(489, 87);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Script";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextScript
            // 
            this.richTextScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextScript.Location = new System.Drawing.Point(3, 3);
            this.richTextScript.Name = "richTextScript";
            this.richTextScript.Size = new System.Drawing.Size(483, 81);
            this.richTextScript.TabIndex = 1;
            this.richTextScript.Text = "";
            this.toolTip.SetToolTip(this.richTextScript, "Scripts loaded to be executed against the database");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(489, 87);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Progress";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.ForeColor = System.Drawing.Color.Lime;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(483, 81);
            this.textBox1.TabIndex = 16;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbo_servers);
            this.groupBox3.Controls.Add(this.progressBar_connect);
            this.groupBox3.Controls.Add(this.chkshowpassword);
            this.groupBox3.Controls.Add(this.txt_password);
            this.groupBox3.Controls.Add(this.txt_user_name);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.chkIntegratedSecurity);
            this.groupBox3.Controls.Add(this.btnGetServerList);
            this.groupBox3.Controls.Add(this.btnBrowseScriptFile);
            this.groupBox3.Controls.Add(this.txtScriptFilePath);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtDBName);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tbBTSAppUser);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(497, 367);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // cbo_servers
            // 
            this.cbo_servers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbo_servers.FormattingEnabled = true;
            this.cbo_servers.Location = new System.Drawing.Point(136, 19);
            this.cbo_servers.Name = "cbo_servers";
            this.cbo_servers.Size = new System.Drawing.Size(259, 21);
            this.cbo_servers.TabIndex = 27;
            // 
            // progressBar_connect
            // 
            this.progressBar_connect.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar_connect.Location = new System.Drawing.Point(3, 341);
            this.progressBar_connect.Name = "progressBar_connect";
            this.progressBar_connect.Size = new System.Drawing.Size(491, 23);
            this.progressBar_connect.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar_connect.TabIndex = 26;
            // 
            // chkshowpassword
            // 
            this.chkshowpassword.AutoSize = true;
            this.chkshowpassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkshowpassword.Location = new System.Drawing.Point(138, 162);
            this.chkshowpassword.Name = "chkshowpassword";
            this.chkshowpassword.Size = new System.Drawing.Size(99, 17);
            this.chkshowpassword.TabIndex = 25;
            this.chkshowpassword.Text = "Show Password";
            this.toolTip.SetToolTip(this.chkshowpassword, "toggle password visibility");
            this.chkshowpassword.UseVisualStyleBackColor = true;
            this.chkshowpassword.CheckedChanged += new System.EventHandler(this.chkshowpassword_CheckedChanged);
            // 
            // txt_password
            // 
            this.txt_password.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_password.Location = new System.Drawing.Point(138, 136);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(256, 20);
            this.txt_password.TabIndex = 24;
            this.toolTip.SetToolTip(this.txt_password, "database password");
            // 
            // txt_user_name
            // 
            this.txt_user_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_user_name.Location = new System.Drawing.Point(138, 96);
            this.txt_user_name.Name = "txt_user_name";
            this.txt_user_name.Size = new System.Drawing.Size(256, 20);
            this.txt_user_name.TabIndex = 23;
            this.toolTip.SetToolTip(this.txt_user_name, "Database user");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(76, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "PassWord*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(72, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "User Name*";
            // 
            // chkIntegratedSecurity
            // 
            this.chkIntegratedSecurity.AutoSize = true;
            this.chkIntegratedSecurity.Checked = true;
            this.chkIntegratedSecurity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIntegratedSecurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIntegratedSecurity.Location = new System.Drawing.Point(342, 60);
            this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
            this.chkIntegratedSecurity.Size = new System.Drawing.Size(131, 17);
            this.chkIntegratedSecurity.TabIndex = 20;
            this.chkIntegratedSecurity.Text = "Use integrated security";
            this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
            this.chkIntegratedSecurity.CheckedChanged += new System.EventHandler(this.chkIntegratedSecurity_CheckedChanged);
            this.chkIntegratedSecurity.CheckStateChanged += new System.EventHandler(this.chkIntegratedSecurity_CheckStateChanged);
            // 
            // btnGetServerList
            // 
            this.btnGetServerList.AutoSize = true;
            this.btnGetServerList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetServerList.LinkColor = System.Drawing.Color.Yellow;
            this.btnGetServerList.Location = new System.Drawing.Point(134, 57);
            this.btnGetServerList.Name = "btnGetServerList";
            this.btnGetServerList.Size = new System.Drawing.Size(130, 20);
            this.btnGetServerList.TabIndex = 19;
            this.btnGetServerList.TabStop = true;
            this.btnGetServerList.Text = "Get Server List";
            this.btnGetServerList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnGetServerList_LinkClicked);
            // 
            // btnBrowseScriptFile
            // 
            this.btnBrowseScriptFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnBrowseScriptFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseScriptFile.Location = new System.Drawing.Point(418, 296);
            this.btnBrowseScriptFile.Name = "btnBrowseScriptFile";
            this.btnBrowseScriptFile.Size = new System.Drawing.Size(58, 23);
            this.btnBrowseScriptFile.TabIndex = 4;
            this.btnBrowseScriptFile.Text = "Browse";
            this.toolTip.SetToolTip(this.btnBrowseScriptFile, "Select the location of the file containing the scripts");
            this.btnBrowseScriptFile.UseVisualStyleBackColor = false;
            this.btnBrowseScriptFile.Click += new System.EventHandler(this.btnBrowseScriptFile_Click);
            // 
            // txtScriptFilePath
            // 
            this.txtScriptFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScriptFilePath.Location = new System.Drawing.Point(134, 298);
            this.txtScriptFilePath.Name = "txtScriptFilePath";
            this.txtScriptFilePath.Size = new System.Drawing.Size(260, 20);
            this.txtScriptFilePath.TabIndex = 3;
            this.toolTip.SetToolTip(this.txtScriptFilePath, "Path to file containing scripts to execute against the Database,\r\nif you do not w" +
        "ant to use the default script.\r\n");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(75, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Script File:";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "SQL Script File (*.sql)|*.sql";
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.Text = "notifyIcon1";
            this.appNotifyIcon.Visible = true;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // txtlogs
            // 
            this.txtlogs.BackColor = System.Drawing.Color.Black;
            this.txtlogs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtlogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtlogs.ForeColor = System.Drawing.Color.Lime;
            this.txtlogs.Location = new System.Drawing.Point(3, 496);
            this.txtlogs.Multiline = true;
            this.txtlogs.Name = "txtlogs";
            this.txtlogs.ReadOnly = true;
            this.txtlogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtlogs.Size = new System.Drawing.Size(497, 136);
            this.txtlogs.TabIndex = 2;
            this.toolTip.SetToolTip(this.txtlogs, "Status");
            // 
            // CreateDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(503, 689);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateDB";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create Database Wizard";
            this.Load += new System.EventHandler(this.CreateDB_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbBTSAppUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtScriptFilePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowseScriptFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richTextScript;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.NotifyIcon appNotifyIcon;
        private System.Windows.Forms.CheckBox chkIntegratedSecurity;
        private System.Windows.Forms.LinkLabel btnGetServerList;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_user_name;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkshowpassword;
        private System.Windows.Forms.ProgressBar progressBar_connect;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.ComboBox cbo_servers;
        private System.Windows.Forms.TextBox txtlogs;
    }
}

