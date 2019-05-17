namespace winSBPayroll.Forms
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbSystems = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.tslSatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.logstatus = new System.Windows.Forms.StatusStrip();
            this.lblcopyright = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLoggedInTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBoxServerLogin = new System.Windows.Forms.GroupBox();
            this.txtServerLoginPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtServerLoginUserName = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.loggedInTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.logstatus.SuspendLayout();
            this.groupBoxServerLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClose);
            this.groupBox2.Controls.Add(this.btnOK);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 256);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(402, 55);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Black;
            this.btnClose.Image = global::winSBPayroll.Properties.Resources.Cancel;
            this.btnClose.Location = new System.Drawing.Point(222, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 32);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Clicked);
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnOK.Location = new System.Drawing.Point(106, 15);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(66, 32);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Clicked);
            // 
            // cbSystems
            // 
            this.cbSystems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSystems.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbSystems.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSystems.FormattingEnabled = true;
            this.cbSystems.Location = new System.Drawing.Point(90, 84);
            this.cbSystems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbSystems.Name = "cbSystems";
            this.cbSystems.Size = new System.Drawing.Size(260, 21);
            this.cbSystems.TabIndex = 2;
            this.cbSystems.SelectedIndexChanged += new System.EventHandler(this.cbSystems_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(26, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "System*";
            // 
            // chkIntegratedSecurity
            // 
            this.chkIntegratedSecurity.AutoSize = true;
            this.chkIntegratedSecurity.Checked = true;
            this.chkIntegratedSecurity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIntegratedSecurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIntegratedSecurity.ForeColor = System.Drawing.Color.Black;
            this.chkIntegratedSecurity.Location = new System.Drawing.Point(90, 120);
            this.chkIntegratedSecurity.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
            this.chkIntegratedSecurity.Size = new System.Drawing.Size(138, 17);
            this.chkIntegratedSecurity.TabIndex = 3;
            this.chkIntegratedSecurity.Text = "Use Integrated Security*";
            this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
            this.chkIntegratedSecurity.CheckedChanged += new System.EventHandler(this.chkIntegratedSecurity_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIntegratedSecurity);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbSystems);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(402, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System Login";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(90, 51);
            this.txtPassword.MaxLength = 20;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(260, 25);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User Name*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(14, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password*";
            // 
            // txtUserName
            // 
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.Location = new System.Drawing.Point(90, 20);
            this.txtUserName.MaxLength = 20;
            this.txtUserName.Name = "txtUserId";
            this.txtUserName.Size = new System.Drawing.Size(260, 25);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.TextChanged += new System.EventHandler(this.txtUserId_TextChanged);
            // 
            // tslSatus
            // 
            this.tslSatus.ActiveLinkColor = System.Drawing.Color.Black;
            this.tslSatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.tslSatus.ForeColor = System.Drawing.Color.Red;
            this.tslSatus.Name = "tslSatus";
            this.tslSatus.Size = new System.Drawing.Size(0, 17);
            this.tslSatus.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // logstatus
            // 
            this.logstatus.BackgroundImage = global::winSBPayroll.Properties.Resources.Signin;
            this.logstatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblcopyright,
            this.lblLoggedInTime});
            this.logstatus.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.logstatus.Location = new System.Drawing.Point(0, 311);
            this.logstatus.Name = "logstatus";
            this.logstatus.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.logstatus.Size = new System.Drawing.Size(402, 22);
            this.logstatus.TabIndex = 7;
            // 
            // lblcopyright
            // 
            this.lblcopyright.ForeColor = System.Drawing.Color.Black;
            this.lblcopyright.Name = "lblcopyright";
            this.lblcopyright.Size = new System.Drawing.Size(242, 17);
            this.lblcopyright.Text = "© Software Providers 2013 - All Rights Reserved";
            // 
            // lblLoggedInTime
            // 
            this.lblLoggedInTime.ForeColor = System.Drawing.Color.Black;
            this.lblLoggedInTime.Name = "lblLoggedInTime";
            this.lblLoggedInTime.Size = new System.Drawing.Size(71, 17);
            this.lblLoggedInTime.Text = "loggedInTime";
            // 
            // groupBoxServerLogin
            // 
            this.groupBoxServerLogin.Controls.Add(this.txtServerLoginPassword);
            this.groupBoxServerLogin.Controls.Add(this.label4);
            this.groupBoxServerLogin.Controls.Add(this.label5);
            this.groupBoxServerLogin.Controls.Add(this.txtServerLoginUserName);
            this.groupBoxServerLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxServerLogin.ForeColor = System.Drawing.Color.Black;
            this.groupBoxServerLogin.Location = new System.Drawing.Point(0, 157);
            this.groupBoxServerLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxServerLogin.Name = "groupBoxServerLogin";
            this.groupBoxServerLogin.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBoxServerLogin.Size = new System.Drawing.Size(402, 99);
            this.groupBoxServerLogin.TabIndex = 1;
            this.groupBoxServerLogin.TabStop = false;
            this.groupBoxServerLogin.Text = "Server Login";
            // 
            // txtServerLoginPassword
            // 
            this.txtServerLoginPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerLoginPassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerLoginPassword.Location = new System.Drawing.Point(90, 61);
            this.txtServerLoginPassword.MaxLength = 20;
            this.txtServerLoginPassword.Name = "txtServerLoginPassword";
            this.txtServerLoginPassword.PasswordChar = '*';
            this.txtServerLoginPassword.Size = new System.Drawing.Size(260, 25);
            this.txtServerLoginPassword.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(8, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "User Name*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.White;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(16, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Password*";
            // 
            // txtServerLoginUserName
            // 
            this.txtServerLoginUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerLoginUserName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerLoginUserName.Location = new System.Drawing.Point(90, 22);
            this.txtServerLoginUserName.MaxLength = 20;
            this.txtServerLoginUserName.Name = "txtServerLoginUserName";
            this.txtServerLoginUserName.Size = new System.Drawing.Size(260, 25);
            this.txtServerLoginUserName.TabIndex = 0;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(402, 333);
            this.Controls.Add(this.groupBoxServerLogin);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.logstatus);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Login_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.logstatus.ResumeLayout(false);
            this.logstatus.PerformLayout();
            this.groupBoxServerLogin.ResumeLayout(false);
            this.groupBoxServerLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbSystems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkIntegratedSecurity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.ToolStripStatusLabel tslSatus;
        private System.Windows.Forms.StatusStrip logstatus;
        private System.Windows.Forms.GroupBox groupBoxServerLogin;
        private System.Windows.Forms.TextBox txtServerLoginPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtServerLoginUserName;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel lblcopyright;
        private System.Windows.Forms.Timer loggedInTimer;
        private System.Windows.Forms.ToolStripStatusLabel lblLoggedInTime;

    }
}