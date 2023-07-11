namespace winSBPayroll.Forms
{
    partial class login_form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login_form));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBoxServerLogin = new System.Windows.Forms.GroupBox();
            this.txtServerLoginPassword = new System.Windows.Forms.TextBox();
            this.txtServerLoginUserName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkremember = new System.Windows.Forms.CheckBox();
            this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.cbosystem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.loggedInTimer = new System.Windows.Forms.Timer(this.components);
            this.logstatus = new System.Windows.Forms.StatusStrip();
            this.lblcopyright = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLoggedInTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1.SuspendLayout();
            this.groupBoxServerLogin.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.logstatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnclose);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 248);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 63);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnclose.ForeColor = System.Drawing.Color.Black;
            this.btnclose.Image = global::winSBPayroll.Properties.Resources.Cancel;
            this.btnclose.Location = new System.Drawing.Point(220, 19);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(66, 32);
            this.btnclose.TabIndex = 1;
            this.btnclose.Text = "&Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnClose_Clicked);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Image = global::winSBPayroll.Properties.Resources.Add;
            this.btnOK.Location = new System.Drawing.Point(98, 18);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 32);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Clicked);
            // 
            // groupBoxServerLogin
            // 
            this.groupBoxServerLogin.Controls.Add(this.txtServerLoginPassword);
            this.groupBoxServerLogin.Controls.Add(this.txtServerLoginUserName);
            this.groupBoxServerLogin.Controls.Add(this.label4);
            this.groupBoxServerLogin.Controls.Add(this.label5);
            this.groupBoxServerLogin.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxServerLogin.Location = new System.Drawing.Point(0, 148);
            this.groupBoxServerLogin.Name = "groupBoxServerLogin";
            this.groupBoxServerLogin.Size = new System.Drawing.Size(402, 100);
            this.groupBoxServerLogin.TabIndex = 1;
            this.groupBoxServerLogin.TabStop = false;
            this.groupBoxServerLogin.Text = "server login";
            // 
            // txtServerLoginPassword
            // 
            this.txtServerLoginPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerLoginPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtServerLoginPassword.Location = new System.Drawing.Point(99, 55);
            this.txtServerLoginPassword.Name = "txtServerLoginPassword";
            this.txtServerLoginPassword.PasswordChar = '*';
            this.txtServerLoginPassword.Size = new System.Drawing.Size(260, 25);
            this.txtServerLoginPassword.TabIndex = 1;
            // 
            // txtServerLoginUserName
            // 
            this.txtServerLoginUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerLoginUserName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtServerLoginUserName.Location = new System.Drawing.Point(99, 19);
            this.txtServerLoginUserName.Name = "txtServerLoginUserName";
            this.txtServerLoginUserName.Size = new System.Drawing.Size(260, 25);
            this.txtServerLoginUserName.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(25, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Password*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(16, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "User Name*";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkremember);
            this.groupBox3.Controls.Add(this.chkIntegratedSecurity);
            this.groupBox3.Controls.Add(this.cbosystem);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtpassword);
            this.groupBox3.Controls.Add(this.txtusername);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(402, 148);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "system login";
            // 
            // chkremember
            // 
            this.chkremember.AutoSize = true;
            this.chkremember.Checked = true;
            this.chkremember.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkremember.Location = new System.Drawing.Point(287, 117);
            this.chkremember.Name = "chkremember";
            this.chkremember.Size = new System.Drawing.Size(72, 17);
            this.chkremember.TabIndex = 4;
            this.chkremember.Text = "remember";
            this.chkremember.UseVisualStyleBackColor = true;
            // 
            // chkIntegratedSecurity
            // 
            this.chkIntegratedSecurity.AutoSize = true;
            this.chkIntegratedSecurity.Checked = true;
            this.chkIntegratedSecurity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIntegratedSecurity.Location = new System.Drawing.Point(99, 117);
            this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
            this.chkIntegratedSecurity.Size = new System.Drawing.Size(141, 17);
            this.chkIntegratedSecurity.TabIndex = 3;
            this.chkIntegratedSecurity.Text = "Use Integrated Security*";
            this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
            this.chkIntegratedSecurity.CheckedChanged += new System.EventHandler(this.chkIntegratedSecurity_CheckedChanged);
            // 
            // cbosystem
            // 
            this.cbosystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbosystem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbosystem.FormattingEnabled = true;
            this.cbosystem.Location = new System.Drawing.Point(99, 86);
            this.cbosystem.Name = "cbosystem";
            this.cbosystem.Size = new System.Drawing.Size(260, 21);
            this.cbosystem.TabIndex = 2;
            this.cbosystem.SelectedIndexChanged += new System.EventHandler(this.cbSystems_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(38, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "System*";
            // 
            // txtpassword
            // 
            this.txtpassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpassword.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtpassword.Location = new System.Drawing.Point(99, 54);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(260, 25);
            this.txtpassword.TabIndex = 1;
            this.txtpassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtusername
            // 
            this.txtusername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtusername.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtusername.Location = new System.Drawing.Point(99, 18);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(260, 25);
            this.txtusername.TabIndex = 0;
            this.txtusername.TextChanged += new System.EventHandler(this.txtUserName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(25, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name*";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // logstatus
            // 
            this.logstatus.BackgroundImage = global::winSBPayroll.Properties.Resources.Signin;
            this.logstatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblcopyright,
            this.lblLoggedInTime});
            this.logstatus.Location = new System.Drawing.Point(0, 311);
            this.logstatus.Name = "logstatus";
            this.logstatus.Size = new System.Drawing.Size(402, 22);
            this.logstatus.TabIndex = 0;
            this.logstatus.Text = "statusStrip1";
            // 
            // lblcopyright
            // 
            this.lblcopyright.Name = "lblcopyright";
            this.lblcopyright.Size = new System.Drawing.Size(257, 17);
            this.lblcopyright.Text = "© Software Providers 2013 - All Rights Reserved";
            // 
            // lblLoggedInTime
            // 
            this.lblLoggedInTime.Name = "lblLoggedInTime";
            this.lblLoggedInTime.Size = new System.Drawing.Size(96, 17);
            this.lblLoggedInTime.Text = "lblLoggedInTime";
            // 
            // login_form
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnclose;
            this.ClientSize = new System.Drawing.Size(402, 333);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBoxServerLogin);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.logstatus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "login_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.login_form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBoxServerLogin.ResumeLayout(false);
            this.groupBoxServerLogin.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.logstatus.ResumeLayout(false);
            this.logstatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip logstatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBoxServerLogin;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ToolStripStatusLabel lblcopyright;
        private System.Windows.Forms.ToolStripStatusLabel lblLoggedInTime;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbosystem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIntegratedSecurity;
        private System.Windows.Forms.TextBox txtServerLoginPassword;
        private System.Windows.Forms.TextBox txtServerLoginUserName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Timer loggedInTimer;
        private System.Windows.Forms.CheckBox chkremember;
    }
}