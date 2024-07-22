namespace winSBPayroll.Forms
{
    partial class DatabaseControlPanelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatabaseControlPanelForm));
            this.lblDatabaseVersion = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.lblDatabaseSize = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.tabControlDBPanel = new System.Windows.Forms.TabControl();
            this.tabPageServerSettings = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.listViewDatabaseList = new System.Windows.Forms.ListView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCreateLoginUser = new System.Windows.Forms.LinkLabel();
            this.gbRestore = new System.Windows.Forms.GroupBox();
            this.txtNewDatabaseName = new System.Windows.Forms.TextBox();
            this.btnRestoreNow = new System.Windows.Forms.LinkLabel();
            this.btnRestore = new System.Windows.Forms.LinkLabel();
            this.btnCreateDBScripts = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnSeeDetails = new System.Windows.Forms.LinkLabel();
            this.btnCreateNewDatabase = new System.Windows.Forms.LinkLabel();
            this.btnBackUp = new System.Windows.Forms.LinkLabel();
            this.lblDatabaseNo = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtlog_settings = new System.Windows.Forms.TextBox();
            this.btnLaunchApp = new System.Windows.Forms.LinkLabel();
            this.lblMsg = new System.Windows.Forms.Label();
            this.progressBar_settings = new System.Windows.Forms.ProgressBar();
            this.btnQuitDatabaseManagement = new System.Windows.Forms.LinkLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChangeServer = new System.Windows.Forms.LinkLabel();
            this.lblSrvSttServerName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPageServerConnection = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.progressBar_connect = new System.Windows.Forms.ProgressBar();
            this.btnWebsite = new System.Windows.Forms.LinkLabel();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.contextMenuStripCopyEmail = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnConnect = new System.Windows.Forms.LinkLabel();
            this.btnQuitChangeSever = new System.Windows.Forms.LinkLabel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnload_log_file = new System.Windows.Forms.LinkLabel();
            this.btnLoginAssistance = new System.Windows.Forms.LinkLabel();
            this.btnSQLServerInstallation = new System.Windows.Forms.LinkLabel();
            this.chkRememberServerName = new System.Windows.Forms.CheckBox();
            this.groupBoxLoginCreds = new System.Windows.Forms.GroupBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.chkRemeberUserName = new System.Windows.Forms.CheckBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.chkRememberPassword = new System.Windows.Forms.CheckBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.txtlog_connect = new System.Windows.Forms.TextBox();
            this.txtConnectionLoginErrors = new System.Windows.Forms.TextBox();
            this.chkIntegratedSecurity = new System.Windows.Forms.CheckBox();
            this.btnGetServerList = new System.Windows.Forms.LinkLabel();
            this.cboserver = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPageDatabaseSettings = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnChangeDatabaseSettingsDatabase = new System.Windows.Forms.LinkLabel();
            this.lblDataBaseName = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnChangeDatabaseSettingsServer = new System.Windows.Forms.LinkLabel();
            this.lbldbSttServerName = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.txtSQLDatabaseStructure = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnDatabaseSettingsQuit = new System.Windows.Forms.LinkLabel();
            this.bindingSourceServerName = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSourceDatabases = new System.Windows.Forms.BindingSource(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.contextMenuStripSystemNotification = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControlDBPanel.SuspendLayout();
            this.tabPageServerSettings.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.gbRestore.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageServerConnection.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.contextMenuStripCopyEmail.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBoxLoginCreds.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.tabPageDatabaseSettings.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDatabases)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.contextMenuStripSystemNotification.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDatabaseVersion
            // 
            this.lblDatabaseVersion.AutoSize = true;
            this.lblDatabaseVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseVersion.Location = new System.Drawing.Point(91, 146);
            this.lblDatabaseVersion.Name = "lblDatabaseVersion";
            this.lblDatabaseVersion.Size = new System.Drawing.Size(19, 15);
            this.lblDatabaseVersion.TabIndex = 28;
            this.lblDatabaseVersion.Text = "...";
            this.toolTip1.SetToolTip(this.lblDatabaseVersion, "Database Version");
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(40, 146);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(45, 13);
            this.label26.TabIndex = 27;
            this.label26.Text = "Version:";
            // 
            // lblDatabaseSize
            // 
            this.lblDatabaseSize.AutoSize = true;
            this.lblDatabaseSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseSize.Location = new System.Drawing.Point(91, 91);
            this.lblDatabaseSize.Name = "lblDatabaseSize";
            this.lblDatabaseSize.Size = new System.Drawing.Size(19, 15);
            this.lblDatabaseSize.TabIndex = 26;
            this.lblDatabaseSize.Text = "...";
            this.toolTip1.SetToolTip(this.lblDatabaseSize, "Database Size");
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(55, 93);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(30, 13);
            this.label20.TabIndex = 25;
            this.label20.Text = "Size:";
            // 
            // tabControlDBPanel
            // 
            this.tabControlDBPanel.Controls.Add(this.tabPageServerSettings);
            this.tabControlDBPanel.Controls.Add(this.tabPageServerConnection);
            this.tabControlDBPanel.Controls.Add(this.tabPageDatabaseSettings);
            this.tabControlDBPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDBPanel.Location = new System.Drawing.Point(0, 0);
            this.tabControlDBPanel.Name = "tabControlDBPanel";
            this.tabControlDBPanel.SelectedIndex = 0;
            this.tabControlDBPanel.Size = new System.Drawing.Size(829, 457);
            this.tabControlDBPanel.TabIndex = 0;
            this.tabControlDBPanel.SelectedIndexChanged += new System.EventHandler(this.tabControlDBPanel_SelectedIndexChanged);
            // 
            // tabPageServerSettings
            // 
            this.tabPageServerSettings.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageServerSettings.Controls.Add(this.groupBox3);
            this.tabPageServerSettings.Controls.Add(this.groupBox2);
            this.tabPageServerSettings.Controls.Add(this.groupBox1);
            this.tabPageServerSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageServerSettings.Name = "tabPageServerSettings";
            this.tabPageServerSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServerSettings.Size = new System.Drawing.Size(821, 431);
            this.tabPageServerSettings.TabIndex = 1;
            this.tabPageServerSettings.Text = "SQL Server Settings";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.lblDatabaseNo);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(815, 303);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Database Management";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.listViewDatabaseList);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox5.Location = new System.Drawing.Point(3, 47);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(485, 253);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            // 
            // listViewDatabaseList
            // 
            this.listViewDatabaseList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewDatabaseList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDatabaseList.FullRowSelect = true;
            this.listViewDatabaseList.GridLines = true;
            this.listViewDatabaseList.HideSelection = false;
            this.listViewDatabaseList.LabelEdit = true;
            this.listViewDatabaseList.Location = new System.Drawing.Point(3, 16);
            this.listViewDatabaseList.MultiSelect = false;
            this.listViewDatabaseList.Name = "listViewDatabaseList";
            this.listViewDatabaseList.Size = new System.Drawing.Size(479, 234);
            this.listViewDatabaseList.TabIndex = 0;
            this.toolTip1.SetToolTip(this.listViewDatabaseList, "A list of Databases found in the Server\r\nwhose  Structure conform to the software" +
        "\'s version.\r\n");
            this.listViewDatabaseList.UseCompatibleStateImageBehavior = false;
            this.listViewDatabaseList.View = System.Windows.Forms.View.Details;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnCreateLoginUser);
            this.groupBox4.Controls.Add(this.gbRestore);
            this.groupBox4.Controls.Add(this.btnRestore);
            this.groupBox4.Controls.Add(this.btnCreateDBScripts);
            this.groupBox4.Controls.Add(this.linkLabel1);
            this.groupBox4.Controls.Add(this.btnSeeDetails);
            this.groupBox4.Controls.Add(this.btnCreateNewDatabase);
            this.groupBox4.Controls.Add(this.btnBackUp);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox4.Location = new System.Drawing.Point(488, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(324, 284);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            // 
            // btnCreateLoginUser
            // 
            this.btnCreateLoginUser.AutoSize = true;
            this.btnCreateLoginUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateLoginUser.LinkColor = System.Drawing.Color.Yellow;
            this.btnCreateLoginUser.Location = new System.Drawing.Point(14, 224);
            this.btnCreateLoginUser.Name = "btnCreateLoginUser";
            this.btnCreateLoginUser.Size = new System.Drawing.Size(163, 16);
            this.btnCreateLoginUser.TabIndex = 27;
            this.btnCreateLoginUser.TabStop = true;
            this.btnCreateLoginUser.Text = "Create Database User";
            this.toolTip1.SetToolTip(this.btnCreateLoginUser, "Create a Database User.\r\n This User is able to login in to the database,\r\nwith a " +
        "UserName and a Password.\r\n\r\n\r\n");
            this.btnCreateLoginUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCreateLoginUser_LinkClicked);
            // 
            // gbRestore
            // 
            this.gbRestore.Controls.Add(this.txtNewDatabaseName);
            this.gbRestore.Controls.Add(this.btnRestoreNow);
            this.gbRestore.Location = new System.Drawing.Point(9, 120);
            this.gbRestore.Name = "gbRestore";
            this.gbRestore.Size = new System.Drawing.Size(297, 69);
            this.gbRestore.TabIndex = 0;
            this.gbRestore.TabStop = false;
            this.gbRestore.Text = "Change Name To";
            this.gbRestore.Visible = false;
            // 
            // txtNewDatabaseName
            // 
            this.txtNewDatabaseName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewDatabaseName.Location = new System.Drawing.Point(8, 19);
            this.txtNewDatabaseName.Name = "txtNewDatabaseName";
            this.txtNewDatabaseName.Size = new System.Drawing.Size(255, 20);
            this.txtNewDatabaseName.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtNewDatabaseName, "Name for the Database that will be  created during Restore");
            // 
            // btnRestoreNow
            // 
            this.btnRestoreNow.AutoSize = true;
            this.btnRestoreNow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestoreNow.LinkColor = System.Drawing.Color.Yellow;
            this.btnRestoreNow.Location = new System.Drawing.Point(166, 44);
            this.btnRestoreNow.Name = "btnRestoreNow";
            this.btnRestoreNow.Size = new System.Drawing.Size(97, 16);
            this.btnRestoreNow.TabIndex = 19;
            this.btnRestoreNow.TabStop = true;
            this.btnRestoreNow.Text = "Restore Now";
            this.toolTip1.SetToolTip(this.btnRestoreNow, "Select a location to Restore from.\r\nThis location is where the Back up media is.");
            this.btnRestoreNow.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnRestoreNow_LinkClicked);
            // 
            // btnRestore
            // 
            this.btnRestore.AutoSize = true;
            this.btnRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.LinkColor = System.Drawing.Color.Yellow;
            this.btnRestore.Location = new System.Drawing.Point(12, 99);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(63, 16);
            this.btnRestore.TabIndex = 19;
            this.btnRestore.TabStop = true;
            this.btnRestore.Text = "Restore";
            this.toolTip1.SetToolTip(this.btnRestore, "Restore a Database.This can be a previously\r\nbacked up database.");
            this.btnRestore.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnRestore_LinkClicked);
            // 
            // btnCreateDBScripts
            // 
            this.btnCreateDBScripts.AutoSize = true;
            this.btnCreateDBScripts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateDBScripts.LinkColor = System.Drawing.Color.Yellow;
            this.btnCreateDBScripts.Location = new System.Drawing.Point(12, 15);
            this.btnCreateDBScripts.Name = "btnCreateDBScripts";
            this.btnCreateDBScripts.Size = new System.Drawing.Size(149, 16);
            this.btnCreateDBScripts.TabIndex = 24;
            this.btnCreateDBScripts.TabStop = true;
            this.btnCreateDBScripts.Text = "Generate DB Scripts";
            this.toolTip1.SetToolTip(this.btnCreateDBScripts, "Enables you to generate scripts from existing databases.");
            this.btnCreateDBScripts.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCreateDBScripts_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Yellow;
            this.linkLabel1.Location = new System.Drawing.Point(14, 252);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(122, 16);
            this.linkLabel1.TabIndex = 23;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Update Systems";
            this.toolTip1.SetToolTip(this.linkLabel1, "This is where you tell the system\r\n where to find the Server and the Database.\r\n");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnUpdateSystems_LinkClicked);
            // 
            // btnSeeDetails
            // 
            this.btnSeeDetails.AutoSize = true;
            this.btnSeeDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSeeDetails.LinkColor = System.Drawing.Color.Yellow;
            this.btnSeeDetails.Location = new System.Drawing.Point(12, 43);
            this.btnSeeDetails.Name = "btnSeeDetails";
            this.btnSeeDetails.Size = new System.Drawing.Size(89, 16);
            this.btnSeeDetails.TabIndex = 21;
            this.btnSeeDetails.TabStop = true;
            this.btnSeeDetails.Text = "See Details";
            this.toolTip1.SetToolTip(this.btnSeeDetails, "Enables you to see details of the selected Database.");
            this.btnSeeDetails.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSeeDetails_LinkClicked);
            // 
            // btnCreateNewDatabase
            // 
            this.btnCreateNewDatabase.AutoSize = true;
            this.btnCreateNewDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateNewDatabase.LinkColor = System.Drawing.Color.Yellow;
            this.btnCreateNewDatabase.Location = new System.Drawing.Point(14, 196);
            this.btnCreateNewDatabase.Name = "btnCreateNewDatabase";
            this.btnCreateNewDatabase.Size = new System.Drawing.Size(160, 16);
            this.btnCreateNewDatabase.TabIndex = 20;
            this.btnCreateNewDatabase.TabStop = true;
            this.btnCreateNewDatabase.Text = "Create New Database";
            this.toolTip1.SetToolTip(this.btnCreateNewDatabase, "Create or ReCreate a Database.\r\n");
            this.btnCreateNewDatabase.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCreateNewDatabase_LinkClicked);
            // 
            // btnBackUp
            // 
            this.btnBackUp.AutoSize = true;
            this.btnBackUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackUp.LinkColor = System.Drawing.Color.Yellow;
            this.btnBackUp.Location = new System.Drawing.Point(12, 71);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Size = new System.Drawing.Size(67, 16);
            this.btnBackUp.TabIndex = 18;
            this.btnBackUp.TabStop = true;
            this.btnBackUp.Text = "Back Up";
            this.toolTip1.SetToolTip(this.btnBackUp, "Back up a Database to a location,\r\nwhere you can Restore the database again later" +
        ".");
            this.btnBackUp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnBackUp_LinkClicked);
            // 
            // lblDatabaseNo
            // 
            this.lblDatabaseNo.AutoSize = true;
            this.lblDatabaseNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDatabaseNo.Location = new System.Drawing.Point(26, 20);
            this.lblDatabaseNo.Name = "lblDatabaseNo";
            this.lblDatabaseNo.Size = new System.Drawing.Size(90, 15);
            this.lblDatabaseNo.TabIndex = 2;
            this.lblDatabaseNo.Text = "Database No";
            this.toolTip1.SetToolTip(this.lblDatabaseNo, "Number of Databases in the Server.");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtlog_settings);
            this.groupBox2.Controls.Add(this.btnLaunchApp);
            this.groupBox2.Controls.Add(this.lblMsg);
            this.groupBox2.Controls.Add(this.progressBar_settings);
            this.groupBox2.Controls.Add(this.btnQuitDatabaseManagement);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 356);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(815, 72);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // txtlog_settings
            // 
            this.txtlog_settings.BackColor = System.Drawing.Color.Black;
            this.txtlog_settings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtlog_settings.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtlog_settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtlog_settings.ForeColor = System.Drawing.Color.Lime;
            this.txtlog_settings.HideSelection = false;
            this.txtlog_settings.Location = new System.Drawing.Point(366, 16);
            this.txtlog_settings.Multiline = true;
            this.txtlog_settings.Name = "txtlog_settings";
            this.txtlog_settings.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtlog_settings.Size = new System.Drawing.Size(446, 53);
            this.txtlog_settings.TabIndex = 2;
            // 
            // btnLaunchApp
            // 
            this.btnLaunchApp.AutoSize = true;
            this.btnLaunchApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaunchApp.LinkColor = System.Drawing.Color.Yellow;
            this.btnLaunchApp.Location = new System.Drawing.Point(221, 16);
            this.btnLaunchApp.Name = "btnLaunchApp";
            this.btnLaunchApp.Size = new System.Drawing.Size(79, 24);
            this.btnLaunchApp.TabIndex = 0;
            this.btnLaunchApp.TabStop = true;
            this.btnLaunchApp.Text = "Launch";
            this.toolTip1.SetToolTip(this.btnLaunchApp, "Restart Soft Books Payroll\r\n");
            this.btnLaunchApp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnLaunchApp_LinkClicked);
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(17, 16);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(38, 13);
            this.lblMsg.TabIndex = 21;
            this.lblMsg.Text = "Ready";
            // 
            // progressBar_settings
            // 
            this.progressBar_settings.Location = new System.Drawing.Point(6, 43);
            this.progressBar_settings.Name = "progressBar_settings";
            this.progressBar_settings.Size = new System.Drawing.Size(201, 23);
            this.progressBar_settings.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar_settings.TabIndex = 20;
            // 
            // btnQuitDatabaseManagement
            // 
            this.btnQuitDatabaseManagement.AutoSize = true;
            this.btnQuitDatabaseManagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitDatabaseManagement.LinkColor = System.Drawing.Color.Yellow;
            this.btnQuitDatabaseManagement.Location = new System.Drawing.Point(314, 16);
            this.btnQuitDatabaseManagement.Name = "btnQuitDatabaseManagement";
            this.btnQuitDatabaseManagement.Size = new System.Drawing.Size(42, 20);
            this.btnQuitDatabaseManagement.TabIndex = 1;
            this.btnQuitDatabaseManagement.TabStop = true;
            this.btnQuitDatabaseManagement.Text = "Quit";
            this.toolTip1.SetToolTip(this.btnQuitDatabaseManagement, "Exit");
            this.btnQuitDatabaseManagement.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnQuitDatabaseManagement_LinkClicked);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnChangeServer);
            this.groupBox1.Controls.Add(this.lblSrvSttServerName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(815, 50);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL Server Settings";
            // 
            // btnChangeServer
            // 
            this.btnChangeServer.AutoSize = true;
            this.btnChangeServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeServer.LinkColor = System.Drawing.Color.Yellow;
            this.btnChangeServer.Location = new System.Drawing.Point(592, 19);
            this.btnChangeServer.Name = "btnChangeServer";
            this.btnChangeServer.Size = new System.Drawing.Size(71, 20);
            this.btnChangeServer.TabIndex = 17;
            this.btnChangeServer.TabStop = true;
            this.btnChangeServer.Text = "Change";
            this.toolTip1.SetToolTip(this.btnChangeServer, "Change the Logged in Server.");
            this.btnChangeServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnChangeServer_LinkClicked);
            // 
            // lblSrvSttServerName
            // 
            this.lblSrvSttServerName.AutoSize = true;
            this.lblSrvSttServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSrvSttServerName.Location = new System.Drawing.Point(73, 24);
            this.lblSrvSttServerName.Name = "lblSrvSttServerName";
            this.lblSrvSttServerName.Size = new System.Drawing.Size(19, 13);
            this.lblSrvSttServerName.TabIndex = 1;
            this.lblSrvSttServerName.Text = "...";
            this.toolTip1.SetToolTip(this.lblSrvSttServerName, "Server.");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // tabPageServerConnection
            // 
            this.tabPageServerConnection.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageServerConnection.Controls.Add(this.groupBox7);
            this.tabPageServerConnection.Controls.Add(this.groupBox6);
            this.tabPageServerConnection.Location = new System.Drawing.Point(4, 22);
            this.tabPageServerConnection.Name = "tabPageServerConnection";
            this.tabPageServerConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageServerConnection.Size = new System.Drawing.Size(821, 431);
            this.tabPageServerConnection.TabIndex = 2;
            this.tabPageServerConnection.Text = "Connect to SQL Server";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.progressBar_connect);
            this.groupBox7.Controls.Add(this.btnWebsite);
            this.groupBox7.Controls.Add(this.txtEmail);
            this.groupBox7.Controls.Add(this.btnConnect);
            this.groupBox7.Controls.Add(this.btnQuitChangeSever);
            this.groupBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox7.Location = new System.Drawing.Point(3, 346);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(815, 82);
            this.groupBox7.TabIndex = 1;
            this.groupBox7.TabStop = false;
            // 
            // progressBar_connect
            // 
            this.progressBar_connect.Location = new System.Drawing.Point(6, 39);
            this.progressBar_connect.Name = "progressBar_connect";
            this.progressBar_connect.Size = new System.Drawing.Size(230, 23);
            this.progressBar_connect.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar_connect.TabIndex = 4;
            // 
            // btnWebsite
            // 
            this.btnWebsite.AutoSize = true;
            this.btnWebsite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnWebsite.LinkColor = System.Drawing.Color.Yellow;
            this.btnWebsite.Location = new System.Drawing.Point(293, 20);
            this.btnWebsite.Name = "btnWebsite";
            this.btnWebsite.Size = new System.Drawing.Size(147, 13);
            this.btnWebsite.TabIndex = 3;
            this.btnWebsite.TabStop = true;
            this.btnWebsite.Text = "www.softwareproviders.co.ke";
            this.toolTip1.SetToolTip(this.btnWebsite, "Our Website.\r\n");
            this.btnWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnWebsite_LinkClicked);
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.ContextMenuStrip = this.contextMenuStripCopyEmail;
            this.txtEmail.Location = new System.Drawing.Point(292, 42);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(178, 20);
            this.txtEmail.TabIndex = 2;
            this.txtEmail.Text = "softwareproviders254@gmail.com";
            this.toolTip1.SetToolTip(this.txtEmail, "Our Email.\r\n\r\n");
            this.txtEmail.Click += new System.EventHandler(this.txtEmail_Click);
            this.txtEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEmail_KeyPress);
            // 
            // contextMenuStripCopyEmail
            // 
            this.contextMenuStripCopyEmail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyEmailToolStripMenuItem});
            this.contextMenuStripCopyEmail.Name = "contextMenuStripCopyEmail";
            this.contextMenuStripCopyEmail.Size = new System.Drawing.Size(103, 26);
            // 
            // copyEmailToolStripMenuItem
            // 
            this.copyEmailToolStripMenuItem.Name = "copyEmailToolStripMenuItem";
            this.copyEmailToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyEmailToolStripMenuItem.Text = "Copy";
            this.copyEmailToolStripMenuItem.Click += new System.EventHandler(this.copyEmailToolStripMenuItem_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.AutoSize = true;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.LinkColor = System.Drawing.Color.Yellow;
            this.btnConnect.Location = new System.Drawing.Point(575, 28);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(88, 24);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.TabStop = true;
            this.btnConnect.Text = "Connect";
            this.toolTip1.SetToolTip(this.btnConnect, "Connect to SQL Server.\r\n");
            this.btnConnect.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnConnect_LinkClicked);
            // 
            // btnQuitChangeSever
            // 
            this.btnQuitChangeSever.AutoSize = true;
            this.btnQuitChangeSever.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuitChangeSever.LinkColor = System.Drawing.Color.Yellow;
            this.btnQuitChangeSever.Location = new System.Drawing.Point(685, 28);
            this.btnQuitChangeSever.Name = "btnQuitChangeSever";
            this.btnQuitChangeSever.Size = new System.Drawing.Size(48, 24);
            this.btnQuitChangeSever.TabIndex = 1;
            this.btnQuitChangeSever.TabStop = true;
            this.btnQuitChangeSever.Text = "Quit";
            this.toolTip1.SetToolTip(this.btnQuitChangeSever, "Exit Application.\r\n");
            this.btnQuitChangeSever.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnQuitChangeSever_LinkClicked);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnload_log_file);
            this.groupBox6.Controls.Add(this.btnLoginAssistance);
            this.groupBox6.Controls.Add(this.btnSQLServerInstallation);
            this.groupBox6.Controls.Add(this.chkRememberServerName);
            this.groupBox6.Controls.Add(this.groupBoxLoginCreds);
            this.groupBox6.Controls.Add(this.groupBox12);
            this.groupBox6.Controls.Add(this.chkIntegratedSecurity);
            this.groupBox6.Controls.Add(this.btnGetServerList);
            this.groupBox6.Controls.Add(this.cboserver);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox6.Location = new System.Drawing.Point(3, 3);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(815, 343);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            // 
            // btnload_log_file
            // 
            this.btnload_log_file.AutoSize = true;
            this.btnload_log_file.BackColor = System.Drawing.Color.Transparent;
            this.btnload_log_file.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnload_log_file.LinkColor = System.Drawing.Color.Yellow;
            this.btnload_log_file.Location = new System.Drawing.Point(709, 81);
            this.btnload_log_file.Name = "btnload_log_file";
            this.btnload_log_file.Size = new System.Drawing.Size(60, 13);
            this.btnload_log_file.TabIndex = 6;
            this.btnload_log_file.TabStop = true;
            this.btnload_log_file.Text = "load log file";
            this.btnload_log_file.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnload_log_file_LinkClicked);
            // 
            // btnLoginAssistance
            // 
            this.btnLoginAssistance.AutoSize = true;
            this.btnLoginAssistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoginAssistance.LinkColor = System.Drawing.Color.Yellow;
            this.btnLoginAssistance.Location = new System.Drawing.Point(414, 150);
            this.btnLoginAssistance.Name = "btnLoginAssistance";
            this.btnLoginAssistance.Size = new System.Drawing.Size(129, 13);
            this.btnLoginAssistance.TabIndex = 4;
            this.btnLoginAssistance.TabStop = true;
            this.btnLoginAssistance.Text = "Cannot connect to Server";
            this.toolTip1.SetToolTip(this.btnLoginAssistance, "Find help on how to login.");
            this.btnLoginAssistance.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnLoginAssistance_LinkClicked);
            // 
            // btnSQLServerInstallation
            // 
            this.btnSQLServerInstallation.AutoSize = true;
            this.btnSQLServerInstallation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSQLServerInstallation.LinkColor = System.Drawing.Color.Yellow;
            this.btnSQLServerInstallation.Location = new System.Drawing.Point(600, 150);
            this.btnSQLServerInstallation.Name = "btnSQLServerInstallation";
            this.btnSQLServerInstallation.Size = new System.Drawing.Size(200, 13);
            this.btnSQLServerInstallation.TabIndex = 5;
            this.btnSQLServerInstallation.TabStop = true;
            this.btnSQLServerInstallation.Text = "How to install  and Configure SQL Server";
            this.toolTip1.SetToolTip(this.btnSQLServerInstallation, "Find help on how to install and configure SQL Server.");
            this.btnSQLServerInstallation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSQLServerInstallation_LinkClicked);
            // 
            // chkRememberServerName
            // 
            this.chkRememberServerName.AutoSize = true;
            this.chkRememberServerName.Checked = true;
            this.chkRememberServerName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRememberServerName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRememberServerName.Location = new System.Drawing.Point(417, 75);
            this.chkRememberServerName.Name = "chkRememberServerName";
            this.chkRememberServerName.Size = new System.Drawing.Size(139, 17);
            this.chkRememberServerName.TabIndex = 1;
            this.chkRememberServerName.Text = "Remember Server Name";
            this.toolTip1.SetToolTip(this.chkRememberServerName, "Enables the application to remember the last logged in server \r\nso you do not nee" +
        "d to type the name or search again next\r\ntime when the application starts.\r\n");
            this.chkRememberServerName.UseVisualStyleBackColor = true;
            // 
            // groupBoxLoginCreds
            // 
            this.groupBoxLoginCreds.Controls.Add(this.groupBox13);
            this.groupBoxLoginCreds.Controls.Add(this.groupBox14);
            this.groupBoxLoginCreds.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxLoginCreds.Location = new System.Drawing.Point(3, 166);
            this.groupBoxLoginCreds.Name = "groupBoxLoginCreds";
            this.groupBoxLoginCreds.Size = new System.Drawing.Size(809, 88);
            this.groupBoxLoginCreds.TabIndex = 6;
            this.groupBoxLoginCreds.TabStop = false;
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.chkRemeberUserName);
            this.groupBox13.Controls.Add(this.txtusername);
            this.groupBox13.Controls.Add(this.label9);
            this.groupBox13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox13.Location = new System.Drawing.Point(3, 16);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(449, 69);
            this.groupBox13.TabIndex = 0;
            this.groupBox13.TabStop = false;
            // 
            // chkRemeberUserName
            // 
            this.chkRemeberUserName.AutoSize = true;
            this.chkRemeberUserName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRemeberUserName.Location = new System.Drawing.Point(96, 45);
            this.chkRemeberUserName.Name = "chkRemeberUserName";
            this.chkRemeberUserName.Size = new System.Drawing.Size(130, 17);
            this.chkRemeberUserName.TabIndex = 1;
            this.chkRemeberUserName.Text = "Remember User Name";
            this.toolTip1.SetToolTip(this.chkRemeberUserName, "Enables the application to remember the last logged in\r\n User Name so you do not " +
        "need to type the name again\r\n next time you need to login.");
            this.chkRemeberUserName.UseVisualStyleBackColor = true;
            // 
            // txtusername
            // 
            this.txtusername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtusername.Location = new System.Drawing.Point(91, 16);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(204, 20);
            this.txtusername.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtusername, "Type the Login/User Name here.\r\n\r\n");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(25, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "User Name:";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.chkRememberPassword);
            this.groupBox14.Controls.Add(this.txtpassword);
            this.groupBox14.Controls.Add(this.label10);
            this.groupBox14.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox14.Location = new System.Drawing.Point(452, 16);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(354, 69);
            this.groupBox14.TabIndex = 1;
            this.groupBox14.TabStop = false;
            // 
            // chkRememberPassword
            // 
            this.chkRememberPassword.AutoSize = true;
            this.chkRememberPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkRememberPassword.Location = new System.Drawing.Point(85, 44);
            this.chkRememberPassword.Name = "chkRememberPassword";
            this.chkRememberPassword.Size = new System.Drawing.Size(123, 17);
            this.chkRememberPassword.TabIndex = 1;
            this.chkRememberPassword.Text = "Remember Password";
            this.toolTip1.SetToolTip(this.chkRememberPassword, "Enables the application to remember the last logged in\r\n Password so you do not n" +
        "eed to type the Password again\r\n next time you need to login.Tick this check box" +
        " with caution!\r\n");
            this.chkRememberPassword.UseVisualStyleBackColor = true;
            // 
            // txtpassword
            // 
            this.txtpassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpassword.Location = new System.Drawing.Point(85, 18);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(204, 20);
            this.txtpassword.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtpassword, "Type the Password here.\r\n");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(26, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Password:";
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.txtlog_connect);
            this.groupBox12.Controls.Add(this.txtConnectionLoginErrors);
            this.groupBox12.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox12.Location = new System.Drawing.Point(3, 254);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(809, 86);
            this.groupBox12.TabIndex = 7;
            this.groupBox12.TabStop = false;
            // 
            // txtlog_connect
            // 
            this.txtlog_connect.BackColor = System.Drawing.Color.Black;
            this.txtlog_connect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtlog_connect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtlog_connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtlog_connect.ForeColor = System.Drawing.Color.Lime;
            this.txtlog_connect.HideSelection = false;
            this.txtlog_connect.Location = new System.Drawing.Point(319, 16);
            this.txtlog_connect.Multiline = true;
            this.txtlog_connect.Name = "txtlog_connect";
            this.txtlog_connect.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtlog_connect.Size = new System.Drawing.Size(487, 67);
            this.txtlog_connect.TabIndex = 1;
            // 
            // txtConnectionLoginErrors
            // 
            this.txtConnectionLoginErrors.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConnectionLoginErrors.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtConnectionLoginErrors.HideSelection = false;
            this.txtConnectionLoginErrors.Location = new System.Drawing.Point(3, 16);
            this.txtConnectionLoginErrors.Multiline = true;
            this.txtConnectionLoginErrors.Name = "txtConnectionLoginErrors";
            this.txtConnectionLoginErrors.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConnectionLoginErrors.Size = new System.Drawing.Size(316, 67);
            this.txtConnectionLoginErrors.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtConnectionLoginErrors, "Connection Errors.\r\n");
            this.txtConnectionLoginErrors.Visible = false;
            // 
            // chkIntegratedSecurity
            // 
            this.chkIntegratedSecurity.AutoSize = true;
            this.chkIntegratedSecurity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkIntegratedSecurity.Location = new System.Drawing.Point(105, 137);
            this.chkIntegratedSecurity.Name = "chkIntegratedSecurity";
            this.chkIntegratedSecurity.Size = new System.Drawing.Size(131, 17);
            this.chkIntegratedSecurity.TabIndex = 3;
            this.chkIntegratedSecurity.Text = "Use integrated security";
            this.toolTip1.SetToolTip(this.chkIntegratedSecurity, resources.GetString("chkIntegratedSecurity.ToolTip"));
            this.chkIntegratedSecurity.UseVisualStyleBackColor = true;
            this.chkIntegratedSecurity.CheckedChanged += new System.EventHandler(this.chkIntegratedSecurity_CheckedChanged);
            // 
            // btnGetServerList
            // 
            this.btnGetServerList.AutoSize = true;
            this.btnGetServerList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetServerList.LinkColor = System.Drawing.Color.Yellow;
            this.btnGetServerList.Location = new System.Drawing.Point(100, 104);
            this.btnGetServerList.Name = "btnGetServerList";
            this.btnGetServerList.Size = new System.Drawing.Size(130, 20);
            this.btnGetServerList.TabIndex = 2;
            this.btnGetServerList.TabStop = true;
            this.btnGetServerList.Text = "Get Server List";
            this.toolTip1.SetToolTip(this.btnGetServerList, "Enumerates a list of available instances of SQL Server that are installed on the " +
        "network.\r\n");
            this.btnGetServerList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnGetServerList_LinkClicked);
            // 
            // cboserver
            // 
            this.cboserver.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboserver.FormattingEnabled = true;
            this.cboserver.Location = new System.Drawing.Point(97, 73);
            this.cboserver.Name = "cboserver";
            this.cboserver.Size = new System.Drawing.Size(295, 21);
            this.cboserver.TabIndex = 0;
            this.toolTip1.SetToolTip(this.cboserver, "Type or select the server here.\r\nTypically in the form ComputerName\\InstanceName." +
        "\r\n");
            this.cboserver.SelectedIndexChanged += new System.EventHandler(this.cboServerName_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(94, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(347, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Typically in the form - Server\\Instance(Computer_Name\\SQLEXPRESS)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(21, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Server Name:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(22, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(764, 16);
            this.label12.TabIndex = 20;
            this.label12.Text = "SB Payroll  needs to know where to find  the Database. Please enter in the detail" +
    "s below and click on Connect.";
            // 
            // tabPageDatabaseSettings
            // 
            this.tabPageDatabaseSettings.BackColor = System.Drawing.Color.LimeGreen;
            this.tabPageDatabaseSettings.Controls.Add(this.groupBox8);
            this.tabPageDatabaseSettings.Controls.Add(this.groupBox11);
            this.tabPageDatabaseSettings.Controls.Add(this.groupBox9);
            this.tabPageDatabaseSettings.Controls.Add(this.groupBox10);
            this.tabPageDatabaseSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageDatabaseSettings.Name = "tabPageDatabaseSettings";
            this.tabPageDatabaseSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDatabaseSettings.Size = new System.Drawing.Size(821, 431);
            this.tabPageDatabaseSettings.TabIndex = 3;
            this.tabPageDatabaseSettings.Text = "SQL Database Settings";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lblDatabaseVersion);
            this.groupBox8.Controls.Add(this.label26);
            this.groupBox8.Controls.Add(this.lblDatabaseSize);
            this.groupBox8.Controls.Add(this.label20);
            this.groupBox8.Controls.Add(this.btnChangeDatabaseSettingsDatabase);
            this.groupBox8.Controls.Add(this.lblDataBaseName);
            this.groupBox8.Controls.Add(this.label24);
            this.groupBox8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox8.Location = new System.Drawing.Point(3, 78);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(815, 199);
            this.groupBox8.TabIndex = 20;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "SQL Database Settings";
            // 
            // btnChangeDatabaseSettingsDatabase
            // 
            this.btnChangeDatabaseSettingsDatabase.AutoSize = true;
            this.btnChangeDatabaseSettingsDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeDatabaseSettingsDatabase.LinkColor = System.Drawing.Color.Yellow;
            this.btnChangeDatabaseSettingsDatabase.Location = new System.Drawing.Point(603, 39);
            this.btnChangeDatabaseSettingsDatabase.Name = "btnChangeDatabaseSettingsDatabase";
            this.btnChangeDatabaseSettingsDatabase.Size = new System.Drawing.Size(71, 20);
            this.btnChangeDatabaseSettingsDatabase.TabIndex = 24;
            this.btnChangeDatabaseSettingsDatabase.TabStop = true;
            this.btnChangeDatabaseSettingsDatabase.Text = "Change";
            this.toolTip1.SetToolTip(this.btnChangeDatabaseSettingsDatabase, "Change the Database");
            this.btnChangeDatabaseSettingsDatabase.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnChangeDatabaseSettingsDatabase_LinkClicked);
            // 
            // lblDataBaseName
            // 
            this.lblDataBaseName.AutoSize = true;
            this.lblDataBaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataBaseName.Location = new System.Drawing.Point(91, 39);
            this.lblDataBaseName.Name = "lblDataBaseName";
            this.lblDataBaseName.Size = new System.Drawing.Size(19, 15);
            this.lblDataBaseName.TabIndex = 21;
            this.lblDataBaseName.Text = "...";
            this.toolTip1.SetToolTip(this.lblDataBaseName, "Database Name");
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(29, 39);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(56, 13);
            this.label24.TabIndex = 20;
            this.label24.Text = "Database:";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnChangeDatabaseSettingsServer);
            this.groupBox11.Controls.Add(this.lbldbSttServerName);
            this.groupBox11.Controls.Add(this.label18);
            this.groupBox11.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox11.Location = new System.Drawing.Point(3, 3);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(815, 73);
            this.groupBox11.TabIndex = 7;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "SQL Server Settings";
            // 
            // btnChangeDatabaseSettingsServer
            // 
            this.btnChangeDatabaseSettingsServer.AutoSize = true;
            this.btnChangeDatabaseSettingsServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeDatabaseSettingsServer.LinkColor = System.Drawing.Color.Yellow;
            this.btnChangeDatabaseSettingsServer.Location = new System.Drawing.Point(603, 30);
            this.btnChangeDatabaseSettingsServer.Name = "btnChangeDatabaseSettingsServer";
            this.btnChangeDatabaseSettingsServer.Size = new System.Drawing.Size(71, 20);
            this.btnChangeDatabaseSettingsServer.TabIndex = 24;
            this.btnChangeDatabaseSettingsServer.TabStop = true;
            this.btnChangeDatabaseSettingsServer.Text = "Change";
            this.toolTip1.SetToolTip(this.btnChangeDatabaseSettingsServer, "Change the Server");
            this.btnChangeDatabaseSettingsServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnChangeDatabaseSettingsServer_LinkClicked);
            // 
            // lbldbSttServerName
            // 
            this.lbldbSttServerName.AutoSize = true;
            this.lbldbSttServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldbSttServerName.Location = new System.Drawing.Point(71, 30);
            this.lbldbSttServerName.Name = "lbldbSttServerName";
            this.lbldbSttServerName.Size = new System.Drawing.Size(19, 15);
            this.lbldbSttServerName.TabIndex = 21;
            this.lbldbSttServerName.Text = "...";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(30, 31);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 20;
            this.label18.Text = "Server:";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.txtSQLDatabaseStructure);
            this.groupBox9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox9.Location = new System.Drawing.Point(3, 277);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(815, 89);
            this.groupBox9.TabIndex = 3;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "SQL Database Structure";
            // 
            // txtSQLDatabaseStructure
            // 
            this.txtSQLDatabaseStructure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSQLDatabaseStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSQLDatabaseStructure.Location = new System.Drawing.Point(3, 16);
            this.txtSQLDatabaseStructure.Multiline = true;
            this.txtSQLDatabaseStructure.Name = "txtSQLDatabaseStructure";
            this.txtSQLDatabaseStructure.Size = new System.Drawing.Size(809, 70);
            this.txtSQLDatabaseStructure.TabIndex = 29;
            this.txtSQLDatabaseStructure.Text = "Structure conform to the software\'s version";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnDatabaseSettingsQuit);
            this.groupBox10.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox10.Location = new System.Drawing.Point(3, 366);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(815, 62);
            this.groupBox10.TabIndex = 2;
            this.groupBox10.TabStop = false;
            // 
            // btnDatabaseSettingsQuit
            // 
            this.btnDatabaseSettingsQuit.AutoSize = true;
            this.btnDatabaseSettingsQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatabaseSettingsQuit.LinkColor = System.Drawing.Color.Yellow;
            this.btnDatabaseSettingsQuit.Location = new System.Drawing.Point(603, 16);
            this.btnDatabaseSettingsQuit.Name = "btnDatabaseSettingsQuit";
            this.btnDatabaseSettingsQuit.Size = new System.Drawing.Size(48, 24);
            this.btnDatabaseSettingsQuit.TabIndex = 20;
            this.btnDatabaseSettingsQuit.TabStop = true;
            this.btnDatabaseSettingsQuit.Text = "Quit";
            this.btnDatabaseSettingsQuit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnDatabaseSettingsQuit_LinkClicked);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
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
            this.homeToolStripMenuItem.Click += new System.EventHandler(this.homeToolStripMenuItem_Click);
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
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // appNotifyIcon
            // 
            this.appNotifyIcon.ContextMenuStrip = this.contextMenuStripSystemNotification;
            this.appNotifyIcon.Text = "notifyIcon1";
            this.appNotifyIcon.Visible = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = " Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Server";
            this.dataGridViewTextBoxColumn2.HeaderText = "Server";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 80;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Version";
            this.dataGridViewTextBoxColumn3.HeaderText = "Version";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // DatabaseControlPanelForm
            // 
            this.AcceptButton = this.btnConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnQuitDatabaseManagement;
            this.ClientSize = new System.Drawing.Size(829, 457);
            this.Controls.Add(this.tabControlDBPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DatabaseControlPanelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Soft Books Payroll  - Database Control Panel";
            this.Load += new System.EventHandler(this.DatabaseControlPanelForm_Load);
            this.tabControlDBPanel.ResumeLayout(false);
            this.tabPageServerSettings.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.gbRestore.ResumeLayout(false);
            this.gbRestore.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageServerConnection.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.contextMenuStripCopyEmail.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBoxLoginCreds.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.tabPageDatabaseSettings.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceServerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDatabases)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.contextMenuStripSystemNotification.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDatabaseVersion;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label lblDatabaseSize;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TabControl tabControlDBPanel;
        private System.Windows.Forms.TabPage tabPageServerSettings;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.LinkLabel btnSeeDetails;
        private System.Windows.Forms.LinkLabel btnCreateNewDatabase;
        private System.Windows.Forms.LinkLabel btnRestoreNow;
        private System.Windows.Forms.LinkLabel btnBackUp;
        private System.Windows.Forms.Label lblDatabaseNo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel btnQuitDatabaseManagement;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel btnChangeServer;
        private System.Windows.Forms.Label lblSrvSttServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPageServerConnection;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.LinkLabel btnConnect;
        private System.Windows.Forms.LinkLabel btnQuitChangeSever;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.LinkLabel btnGetServerList;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.ComboBox cboserver;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabPageDatabaseSettings;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.LinkLabel btnChangeDatabaseSettingsDatabase;
        private System.Windows.Forms.Label lblDataBaseName;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.LinkLabel btnChangeDatabaseSettingsServer;
        private System.Windows.Forms.Label lbldbSttServerName;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox txtSQLDatabaseStructure;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.LinkLabel btnDatabaseSettingsQuit;
        private System.Windows.Forms.BindingSource bindingSourceServerName;
        private System.Windows.Forms.BindingSource bindingSourceDatabases;
        private System.Windows.Forms.ListView listViewDatabaseList;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox txtConnectionLoginErrors;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ProgressBar progressBar_settings;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox chkIntegratedSecurity;
        private System.Windows.Forms.LinkLabel btnCreateDBScripts;
        private System.Windows.Forms.GroupBox gbRestore;
        private System.Windows.Forms.TextBox txtNewDatabaseName;
        private System.Windows.Forms.LinkLabel btnRestore;
        private System.Windows.Forms.LinkLabel btnCreateLoginUser;
        private System.Windows.Forms.LinkLabel btnLaunchApp;
        private System.Windows.Forms.CheckBox chkRememberServerName;
        private System.Windows.Forms.GroupBox groupBoxLoginCreds;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.CheckBox chkRemeberUserName;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.CheckBox chkRememberPassword;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.LinkLabel btnSQLServerInstallation;
        private System.Windows.Forms.LinkLabel btnLoginAssistance;
        private System.Windows.Forms.LinkLabel btnWebsite;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSystemNotification;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon appNotifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripCopyEmail;
        private System.Windows.Forms.ToolStripMenuItem copyEmailToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.LinkLabel btnload_log_file;
        private System.Windows.Forms.ProgressBar progressBar_connect;
        private System.Windows.Forms.TextBox txtlog_settings;
        private System.Windows.Forms.TextBox txtlog_connect;
    }
}