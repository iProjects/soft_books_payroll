namespace winSBPayroll.Forms
{
    partial class CreateDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateDB));
            this.txtProgressStatus = new System.Windows.Forms.TextBox();
            this.tbBTSAppUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.cboServers = new System.Windows.Forms.ComboBox();
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
            this.btnBrowseScriptFile = new System.Windows.Forms.Button();
            this.txtScriptFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProgressStatus
            // 
            this.txtProgressStatus.BackColor = System.Drawing.Color.White;
            this.txtProgressStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtProgressStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtProgressStatus.Location = new System.Drawing.Point(3, 325);
            this.txtProgressStatus.Multiline = true;
            this.txtProgressStatus.Name = "txtProgressStatus";
            this.txtProgressStatus.ReadOnly = true;
            this.txtProgressStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtProgressStatus.Size = new System.Drawing.Size(497, 120);
            this.txtProgressStatus.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtProgressStatus, "Status");
            // 
            // tbBTSAppUser
            // 
            this.tbBTSAppUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBTSAppUser.Location = new System.Drawing.Point(138, 90);
            this.tbBTSAppUser.Name = "tbBTSAppUser";
            this.tbBTSAppUser.Size = new System.Drawing.Size(260, 20);
            this.tbBTSAppUser.TabIndex = 2;
            this.tbBTSAppUser.Text = "Administrator";
            this.toolTip1.SetToolTip(this.tbBTSAppUser, "User");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = " Application Users*:";
            // 
            // txtDBName
            // 
            this.txtDBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDBName.Location = new System.Drawing.Point(138, 55);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(261, 20);
            this.txtDBName.TabIndex = 1;
            this.txtDBName.Text = "SBPayrollDB";
            this.toolTip1.SetToolTip(this.txtDBName, "Type Database Name");
            // 
            // cboServers
            // 
            this.cboServers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboServers.FormattingEnabled = true;
            this.cboServers.Location = new System.Drawing.Point(138, 19);
            this.cboServers.Name = "cboServers";
            this.cboServers.Size = new System.Drawing.Size(261, 21);
            this.cboServers.Sorted = true;
            this.cboServers.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cboServers, "Select or type Server Name");
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
            this.btnCreateDB.Location = new System.Drawing.Point(245, 19);
            this.btnCreateDB.Name = "btnCreateDB";
            this.btnCreateDB.Size = new System.Drawing.Size(128, 23);
            this.btnCreateDB.TabIndex = 0;
            this.btnCreateDB.Text = "Create Database";
            this.toolTip1.SetToolTip(this.btnCreateDB, "Create Database");
            this.btnCreateDB.UseVisualStyleBackColor = false;
            this.btnCreateDB.Click += new System.EventHandler(this.btnCreateDB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Database Name*:";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(420, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(58, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.toolTip1.SetToolTip(this.btnClose, "Exit");
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnCreateDB);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 448);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 54);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.txtProgressStatus);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 448);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 174);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(497, 151);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextScript);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(489, 125);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Script";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextScript
            // 
            this.richTextScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextScript.Location = new System.Drawing.Point(3, 3);
            this.richTextScript.Name = "richTextScript";
            this.richTextScript.Size = new System.Drawing.Size(483, 119);
            this.richTextScript.TabIndex = 1;
            this.richTextScript.Text = "";
            this.toolTip1.SetToolTip(this.richTextScript, "Scripts loaded to be executed against the database");
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(489, 125);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Progress";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(483, 119);
            this.textBox1.TabIndex = 16;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBrowseScriptFile);
            this.groupBox3.Controls.Add(this.txtScriptFilePath);
            this.groupBox3.Controls.Add(this.cboServers);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txtDBName);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.tbBTSAppUser);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(497, 158);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            // 
            // btnBrowseScriptFile
            // 
            this.btnBrowseScriptFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnBrowseScriptFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowseScriptFile.Location = new System.Drawing.Point(404, 122);
            this.btnBrowseScriptFile.Name = "btnBrowseScriptFile";
            this.btnBrowseScriptFile.Size = new System.Drawing.Size(58, 23);
            this.btnBrowseScriptFile.TabIndex = 4;
            this.btnBrowseScriptFile.Text = "Browse";
            this.toolTip1.SetToolTip(this.btnBrowseScriptFile, "Select the location of the file containing the scripts");
            this.btnBrowseScriptFile.UseVisualStyleBackColor = false;
            this.btnBrowseScriptFile.Click += new System.EventHandler(this.btnBrowseScriptFile_Click);
            // 
            // txtScriptFilePath
            // 
            this.txtScriptFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtScriptFilePath.Location = new System.Drawing.Point(138, 125);
            this.txtScriptFilePath.Name = "txtScriptFilePath";
            this.txtScriptFilePath.Size = new System.Drawing.Size(260, 20);
            this.txtScriptFilePath.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtScriptFilePath, "Path to file containing scripts to execute against the Database,\r\nif you do not w" +
                    "ant to use the default script.\r\n");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Script File:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.Text = "notifyIcon1";
            this.appNotifyIcon.Visible = true;
            // 
            // CreateDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(503, 502);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtProgressStatus;
        private System.Windows.Forms.TextBox tbBTSAppUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.ComboBox cboServers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCreateDB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtScriptFilePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowseScriptFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richTextScript;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NotifyIcon appNotifyIcon;
    }
}

