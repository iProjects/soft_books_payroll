using System; 
using System.Collections;
using System.Collections.Generic; 
using System.Data;
using System.Data.Sql; 
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing; 
using System.Drawing.Drawing2D;
using System.IO; 
using System.Linq; 
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms; 
using System.Xml;
using System.Xml.Linq; 
using CommonLib;
using Microsoft.SqlServer.Management.Common; 
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32; 

namespace winSBPayroll.Forms
{
    public partial class DatabaseControlPanelForm : Form
    {

        #region "Private Fields"
        const string SBApplication = "SBPayroll";
        const string Metadata = "SBPayrollDBEntities";
        const string DatabaseVersionExtPropertyKey = SBApplication + "Version";
        const string DatabaseVersionExtPropertyValue = "1.0.0.0";
        const string tempFile = @"DBScripts\db.sql";
        List<ServerDatabase> databases;
        List<string> srvNames;
        #endregion "Private Fields"

        #region "Constructor"
        public DatabaseControlPanelForm()
        {
            InitializeComponent();
            srvNames = new List<string>();
            databases = new List<ServerDatabase>();
        }
        #endregion "Constructor"

        #region "Public Properties"
        public string ServerName { get; set; }
        public string LoginUserName { get; set; }
        public string LoginPassword { get; set; }
        #endregion "Public Properties"

        #region "Private Methods"
        private void btnCreateNewDatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                CreateDB f = new CreateDB();
                f.ShowDialog();

            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex); 
            }
        }
        private Server GetServer(string serverName, string userName, string password)
        {
            ServerConnection conn = new ServerConnection(serverName, userName, password);
            Server myServer = new Server(conn);
            return myServer;
        }
        private Server GetServer(string serverName)
        {
            Server myServer = new Server(serverName);
            return myServer;
        }
        #region "BackUp & Restore Version 1"
        #region "BackUp"
        private bool BackupDatabase(string _sWhereToBackup, string DatabaseName)
        {
            try
            {
                // Filename
                string sFileName = string.Format("{0}\\{1}.bak", _sWhereToBackup, DatabaseName);

                // Connection 
                string servername = lblSrvSttServerName.Text;
                if (string.IsNullOrEmpty(servername))
                    throw new ArgumentNullException("servername");
                Server oServer = GetServer(servername);

                // Backup
                Backup backup = new Backup();
                backup.Action = BackupActionType.Database;
                backup.Database = DatabaseName;
                backup.Incremental = false;
                backup.Initialize = true;
                backup.LogTruncation = BackupTruncateLogType.Truncate;

                // Backup Device
                BackupDeviceItem backupItemDevice = new BackupDeviceItem(sFileName, DeviceType.File);
                backup.Devices.Add(backupItemDevice);

                // Start Backup
                backup.SqlBackup(oServer);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }
        #endregion "BackUp"
        #region "Restore"
        private void RestoreDataBase(string BackupFilePath,
            string destinationDatabaseName,
            string DatabaseFolder,
            string DatabaseFileName,
            string DatabaseLogFileName)
        {
            try
            {
                string servername = lblSrvSttServerName.Text;
                if (string.IsNullOrEmpty(servername))
                    throw new ArgumentNullException("servername");
                Server myServer = GetServer(servername);

                Restore myRestore = new Restore();
                myRestore.Database = destinationDatabaseName;
                Database currentDb = myServer.Databases[destinationDatabaseName];
                if (currentDb != null)
                    myServer.KillAllProcesses(destinationDatabaseName);
                myRestore.Devices.AddDevice(BackupFilePath, DeviceType.File);
                string DataFileLocation = DatabaseFolder + "\\" + destinationDatabaseName + ".mdf";
                string LogFileLocation = DatabaseFolder + "\\" + destinationDatabaseName + "_log.ldf";

                System.Data.DataTable logicalRestoreFiles = myRestore.ReadFileList(myServer); myRestore.RelocateFiles.Add(new RelocateFile(logicalRestoreFiles.Rows[0][0].ToString(), DataFileLocation)); myRestore.RelocateFiles.Add(new RelocateFile(logicalRestoreFiles.Rows[1][0].ToString(), LogFileLocation));
                myRestore.ReplaceDatabase = true;
                myRestore.PercentCompleteNotification = 10;
                myRestore.PercentComplete +=
                    new PercentCompleteEventHandler(myRestore_PercentComplete);
                myRestore.Complete += new ServerMessageEventHandler(myRestore_Complete);

                WriteToLogAndConsole(string.Format("Restoring:{0}", destinationDatabaseName));
                myRestore.SqlRestore(myServer);
                currentDb = myServer.Databases[destinationDatabaseName];
                currentDb.SetOnline();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        #endregion "Restore"
        #endregion "BackUp & Restore Version 1"
        #region "BackUp & Restore Version 2"
        #region "BackUp"
        public void BackupDatabase(SqlConnectionStringBuilder csb, string destinationPath)
        {
            try
            {
                ServerConnection connection = new ServerConnection(csb.DataSource, csb.UserID, csb.Password);
                Server sqlServer = new Server(connection);

                Backup bkpDatabase = new Backup();
                bkpDatabase.Action = BackupActionType.Database;
                bkpDatabase.Database = csb.InitialCatalog;
                BackupDeviceItem bkpDevice = new BackupDeviceItem(destinationPath, DeviceType.File);
                bkpDatabase.Devices.Add(bkpDevice);
                bkpDatabase.SqlBackup(sqlServer);
                connection.Disconnect();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }

        }
        #endregion "BackUp"
        #region "Restore"
        /*
         * USE THIS WHEN YOU WANT TO RESTORE TO THE SAME DATABASE AS BEFORE
         */
        public void RestoreDatabase(String databaseName, String backUpFile, String serverName, String userName, String password)
        {
            try
            {
                ServerConnection connection = new ServerConnection(serverName, userName, password);
                Server sqlServer = new Server(connection);
                Restore rstDatabase = new Restore();
                rstDatabase.Action = RestoreActionType.Database;
                rstDatabase.Database = databaseName;
                BackupDeviceItem bkpDevice = new BackupDeviceItem(backUpFile, DeviceType.File);
                rstDatabase.Devices.Add(bkpDevice);
                rstDatabase.ReplaceDatabase = true;
                rstDatabase.SqlRestore(sqlServer);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
            }
        }
        #endregion "Restore"
        #endregion "BackUp & Restore Version 2"
        private void myRestore_Complete
            (object sender, Microsoft.SqlServer.Management.Common.ServerMessageEventArgs e)
        {
            WriteToLogAndConsole(e.ToString() + " Complete");
        }
        private void myRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            WriteToLogAndConsole(e.Percent.ToString() + "% Complete");
        }
        private void WriteToLogAndConsole(string msg)
        {
            lblMsg.Text = msg;
        }
        private void CreateLogin(string sqlLoginName, string sqlLoginPassword, string databaseName)
        {

            try
            {
                string servername = lblSrvSttServerName.Text;
                if (string.IsNullOrEmpty(servername))
                    throw new ArgumentNullException("servername");
                //connect to the server
                Server myServer = GetServer(servername);

                Login newLogin = myServer.Logins[sqlLoginName];
                if (newLogin != null)
                    newLogin.Drop();
                newLogin = new Login(myServer, sqlLoginName);
                newLogin.PasswordPolicyEnforced = false;
                newLogin.LoginType = LoginType.SqlLogin; //SqlLogin not anything else
                newLogin.Create(sqlLoginPassword);
                //Create DatabaseUser
                string MainDbName = "model";
                DatabaseMapping mapping =
                    new DatabaseMapping(newLogin.Name, MainDbName, newLogin.Name);
                Database currentDb = myServer.Databases[databaseName];
                //initialize new User object and say to which database it belongs and its name
                Microsoft.SqlServer.Management.Smo.User dbUser =
                    new Microsoft.SqlServer.Management.Smo.User(currentDb, newLogin.Name);
                //associate the user to login name, login name should be valid login name
                dbUser.Login = sqlLoginName;
                // here's we create the user on the database
                dbUser.Create();
                // grant acces in database for created user account
                dbUser.AddToRole("db_owner");
                //dbUser.AddToRole("sysadmin");
                //dbUser.AddToRole("serveradmin"); 
                MessageBox.Show(string.Format("User [ {0} ] Created Successsfully on Database  [ {1} ] ", sqlLoginName, databaseName), "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void btnRestore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (gbRestore.Visible)
            {
                gbRestore.Visible = false;
            }
            else
            {
                gbRestore.Visible = true;
            }
        }
        private void sqlRestore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void sqlRestore_Complete(object sender, ServerMessageEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void btnRestoreNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider1.Clear();
            if (string.IsNullOrEmpty(txtNewDatabaseName.Text))
            {
                errorProvider1.SetError(txtNewDatabaseName, "Database Name cannot be null!");
                return;
            }
            if (!string.IsNullOrEmpty(txtNewDatabaseName.Text))
            {
                try
                {
                    // Create OpenFileDialog 
                    Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                    ofd.Title = "Select Back Up File";
                    ofd.Filter = "Back Up File (*.bak)|*.bak";
                    Nullable<bool> result = ofd.ShowDialog();
                    // Process open file dialog box results
                    if (result == true)
                    {
                        // Get the selected file name and display in a TextBox  
                        string BackupFilePath = ofd.FileName;
                        string filename = Path.GetFileNameWithoutExtension(BackupFilePath);
                        string directoryPath = Path.GetDirectoryName(BackupFilePath);
                        string destinationDatabaseName = txtNewDatabaseName.Text;

                        string DatabaseFolder = directoryPath;
                        string DatabaseFileName = filename + "_DATA";
                        string DatabaseLogFileName = filename + "_LOG";

                        RestoreDataBase(BackupFilePath,
                         destinationDatabaseName,
                         DatabaseFolder,
                         DatabaseFileName,
                         DatabaseLogFileName);

                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void btnBackUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (listViewDatabaseList.SelectedItems.Count != 0)
            {
                try
                {
                    folderBrowserDialog.Description = "Select a folder to backup to.  Make sure the folder name does not contain space";
                    DialogResult result = folderBrowserDialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        ListViewItem.ListViewSubItem dbName = listViewDatabaseList.SelectedItems[0].SubItems[0];
                        string foldername = this.folderBrowserDialog.SelectedPath;
                        BackupDatabase(foldername, dbName.Text);
                        MessageBox.Show("Backup complete!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex); 
                }
            }
        }
        public void ProgressEventHandler(object sender, PercentCompleteEventArgs e)
        {
            this.progressBar1.Value = e.Percent;
        }
        private void btnSeeDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider1.Clear();
            if (listViewDatabaseList.SelectedItems.Count != 0)
            {

                try
                {
                    string _servername = lblSrvSttServerName.Text.Trim();

                    foreach (ListViewItem lvi in listViewDatabaseList.SelectedItems)
                    {
                        ListViewItem.ListViewSubItem dbName = lvi.SubItems[0];
                        ListViewItem.ListViewSubItem dbSize = lvi.SubItems[1];
                        ListViewItem.ListViewSubItem dbVersion = lvi.SubItems[2];

                        if (!tabControlDBPanel.TabPages.Contains(tabPageDatabaseSettings))
                        {
                            tabControlDBPanel.TabPages.Add(tabPageDatabaseSettings);
                            tabControlDBPanel.TabPages.Remove(tabPageServerSettings);
                            lblDataBaseName.Text = dbName.Text;
                            lblDatabaseSize.Text = dbSize.Text;
                            lblDatabaseVersion.Text = dbVersion.Text;
                            lbldbSttServerName.Text = _servername;

                        }
                    }

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex); 
                }
            }
        }
        private void btnSetasDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void btnQuitDatabaseManagement_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void btnGetServerList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string InstanceName = dr["InstanceName"].ToString();
                        string ServerName = dr["ServerName"].ToString();
                        if (string.IsNullOrEmpty(InstanceName) || string.IsNullOrEmpty(ServerName))
                        {
                            this.ServerName = ServerName;
                            this.srvNames.Add(this.ServerName);
                        }

                        if (!string.IsNullOrEmpty(InstanceName) && !string.IsNullOrEmpty(ServerName))
                        {
                            this.ServerName = ServerName + "\\" + InstanceName;
                            this.srvNames.Add(this.ServerName);
                        }
                    }
                    cboServerName.DataSource = this.srvNames;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void btnDefaultdatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void btnSQLServerInstallationInstructions_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private bool Save_Conn_Info_in_Config()
        {
            try
            {
                if (!chkIntegratedSec.Checked && !string.IsNullOrEmpty(txtServerLoginUserName.Text) && !string.IsNullOrEmpty(cboServerName.Text))
                {
                    SBSystem_DTO sysdto = new SBSystem_DTO(txtServerLoginUserName.Text, cboServerName.Text);
                    Save_Auto_complete_username_info(sysdto);
                }
                if (!string.IsNullOrEmpty(cboServerName.Text))
                {
                    SBSystem_DTO sysdto = new SBSystem_DTO(cboServerName.Text, DateTime.Now.ToString());
                    Save_Auto_complete_servername_info(sysdto);
                }

                Save_Auto_Complete_Conn_Info_in_Config();

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool Save_Auto_Complete_Conn_Info_in_Config()
        {
            try
            {
                string auto_complete_conn_info_filename = "Resources/auto_complete_conn_info_dto.xml";
                //sname,uname,pname,!int
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboServerName.Text) && chkRemeberUserName.Checked && !string.IsNullOrEmpty(txtServerLoginUserName.Text) && chkRememberPassword.Checked && !string.IsNullOrEmpty(txtServerLoginPassword.Text) && !chkIntegratedSec.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                           new XAttribute("Name", cboServerName.Text),
                                            new XAttribute("Application", txtServerLoginUserName.Text),
                                              new XAttribute("Database", txtServerLoginPassword.Text),
                                              new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                              new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                            new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //sname,uname,!pname,!int
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboServerName.Text) && chkRemeberUserName.Checked && !string.IsNullOrEmpty(txtServerLoginUserName.Text) && !chkRememberPassword.Checked && !chkIntegratedSec.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                           new XAttribute("Name", cboServerName.Text),
                                            new XAttribute("Application", txtServerLoginUserName.Text),
                                              new XAttribute("Database", ""),
                                              new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                              new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                            new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //sname,!uname,!pname,int
                if (chkIntegratedSec.Checked && chkRememberServerName.Checked && !string.IsNullOrEmpty(cboServerName.Text))
                {
                    var xml = new XElement("Systems", new XElement("System",
                                           new XAttribute("Name", cboServerName.Text),
                                            new XAttribute("Application", ""),
                                              new XAttribute("Database", ""),
                                              new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                              new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                            new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //!sname,!uname,!pname,!int
                if (!chkRememberServerName.Checked && !chkRemeberUserName.Checked && !chkRememberPassword.Checked && !chkIntegratedSec.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                            new XAttribute("Name", ""),
                                             new XAttribute("Application", ""),
                                               new XAttribute("Database", ""),
                                               new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                               new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                             new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //!sname,!uname,!pname,int
                if (!chkRememberServerName.Checked && chkIntegratedSec.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                            new XAttribute("Name", ""),
                                             new XAttribute("Application", ""),
                                               new XAttribute("Database", ""),
                                               new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                               new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                             new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //sname,uname,pname,int
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboServerName.Text) && chkRemeberUserName.Checked && chkRememberPassword.Checked && chkIntegratedSec.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                            new XAttribute("Name", cboServerName.Text),
                                             new XAttribute("Application", ""),
                                               new XAttribute("Database", ""),
                                               new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                               new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                             new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //sname,!uname,!pname,!int
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboServerName.Text) && !chkRemeberUserName.Checked && !chkRememberPassword.Checked && !chkIntegratedSec.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                            new XAttribute("Name", cboServerName.Text),
                                             new XAttribute("Application", ""),
                                               new XAttribute("Database", ""),
                                               new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                               new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                             new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //sname,uname,!pname,int
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboServerName.Text) && chkRemeberUserName.Checked && !chkRememberPassword.Checked && chkIntegratedSec.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                            new XAttribute("Name", cboServerName.Text),
                                             new XAttribute("Application", ""),
                                               new XAttribute("Database", ""),
                                               new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                               new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                             new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool Save_Auto_complete_username_info(SBSystem_DTO sysdto)
        {
            try
            {
                string auto_complete_server_username_filename = "Resources/auto_complete_server_username_dto.xml";                
                if (File.Exists(auto_complete_server_username_filename))
                {
                    List<SBSystem_DTO> logged_users_dto = new List<SBSystem_DTO>();
                    SBSystem_DTO sys_dto = new SBSystem_DTO(sysdto.Name, sysdto.Application);
                    List<SBSystem_DTO> successfully_logged_users = SQLHelper.GetDataFromSBSystem_DTOXML(auto_complete_server_username_filename);
                    foreach (var item in successfully_logged_users)
                    {
                        logged_users_dto.Add(item);
                    }
                    if (!logged_users_dto.Any(i => i.Name == sysdto.Name && i.Application == sysdto.Application))
                    {
                        XDocument doc = XDocument.Load(auto_complete_server_username_filename);
                        doc.Element("Systems").Add(
                             new XElement("System",
                                           new XAttribute("Name", sysdto.Name),
                                            new XAttribute("Application", sysdto.Application)));
                        doc.Save(auto_complete_server_username_filename);
                    }
                }
                if (!File.Exists(auto_complete_server_username_filename))
                {

                    var xml = new XElement("Systems", new XElement("System",
                                           new XAttribute("Name", sysdto.Name),
                                            new XAttribute("Application", sysdto.Application)));
                    xml.Save(auto_complete_server_username_filename);
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool Save_Auto_complete_servername_info(SBSystem_DTO sysdto)
        {
            try
            {
                string auto_complete_servername_filename = "Resources/auto_complete_servername_dto.xml";          
                if (File.Exists(auto_complete_servername_filename))
                {

                    List<string> logged_usernames = new List<string>();
                    List<SBSystem_DTO> successfully_logged_users = SQLHelper.GetDataFromSBSystem_DTOXML(auto_complete_servername_filename);
                    foreach (var item in successfully_logged_users)
                    {
                        logged_usernames.Add(item.Name);
                    }
                    if (!logged_usernames.Contains(cboServerName.Text))
                    {
                        XDocument doc = XDocument.Load(auto_complete_servername_filename);
                        doc.Element("Systems").Add(
                             new XElement("System",
                                           new XAttribute("Name", sysdto.Name),
                                            new XAttribute("Application", sysdto.Application)));
                        doc.Save(auto_complete_servername_filename);
                    }
                }
                if (!File.Exists(auto_complete_servername_filename))
                {
                    var xml = new XElement("Systems", new XElement("System",
                                           new XAttribute("Name", sysdto.Name),
                                            new XAttribute("Application", sysdto.Application)));
                    xml.Save(auto_complete_servername_filename);
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private void btnConnect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            errorProvider1.Clear();

            if (!chkIntegratedSec.Checked)
            {
                if (!IsServerLoginValid())
                {
                    MessageBox.Show("Incorrect Credentials, make sure the ServerName, UserName and Password are entered.", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            try
            {
                txtConnectionLoginErrors.Text = string.Empty;

                this.ServerName = (string)cboServerName.Text.Trim();
                this.LoginUserName = (string)txtServerLoginUserName.Text.Trim();
                this.LoginPassword = (string)txtServerLoginPassword.Text.Trim();

                Server server = ConnectToServer(ServerName, LoginUserName, LoginPassword, chkIntegratedSec.Checked);

                if (server.ConnectionContext.IsOpen)
                {
                    databases.Clear();
                    databases = GetServerDatabases(server);

                    DisplayServer(server, LoginUserName, LoginPassword);

                    NotifyMessage("Connected to Server.", "Edition: " + server.Edition + Environment.NewLine + "IsClustered: " + server.IsClustered + Environment.NewLine + "Build: " + server.BuildNumber + Environment.NewLine + "Net Name: " + server.NetName + Environment.NewLine + "Instance: " + server.InstanceName + Environment.NewLine + "Physical Memory: " + server.PhysicalMemory.ToString() + Environment.NewLine + "Platform: " + server.Platform + Environment.NewLine + "Processors: " + server.Processors + Environment.NewLine + "Type: " + server.ServerType + Environment.NewLine + "Service Id: " + server.ServiceInstanceId + Environment.NewLine + "Start Mode: " + server.ServiceStartMode.ToString() + Environment.NewLine + "State: " + server.State);
                }
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionException smoex)
            {
                Log.WriteToErrorLogFile(smoex);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
        public Server ConnectToServer(string srv, string user, string pwd, bool intsec)
        {
            try
            {
                Server server = new Server(srv);
                server.ConnectionContext.LoginSecure = intsec;
                if (!intsec)
                {
                    server.ConnectionContext.Login = user;
                    server.ConnectionContext.Password = pwd;
                }
                server.ConnectionContext.Connect();
                Save_Conn_Info_in_Config();
                return server;
            }
            catch (Exception ex)
            { 
                string msg = ex.Message;
                if (ex.InnerException != null)
                {
                    msg += "\n" + ex.InnerException.Message;
                }
                txtConnectionLoginErrors.Text = msg;
                txtConnectionLoginErrors.Visible = true;
                Utils.LogEventViewer(ex);
                return null;
            }
        }
        public void DisplayServer(Server server, string username, string pwd)
        {
            try
            {
                if (server.ConnectionContext.IsOpen)
                {
                    tabControlDBPanel.TabPages.Remove(tabPageServerSettings);
                    tabControlDBPanel.TabPages.Remove(tabPageServerConnection);
                    tabControlDBPanel.TabPages.Remove(tabPageDatabaseSettings);

                    if (!tabControlDBPanel.TabPages.Contains(tabPageServerSettings))
                    {
                        tabControlDBPanel.TabPages.Add(tabPageServerSettings);
                        tabControlDBPanel.TabPages.Remove(tabPageServerConnection);
                        tabControlDBPanel.TabPages.Remove(tabPageDatabaseSettings);
                        lblSrvSttServerName.Text = string.Empty;

                        lblSrvSttServerName.Text = server.Name;

                        string result = "";
                        List<char> listOfChars = pwd.ToList<char>();
                        int numberOfChars = listOfChars.Count<char>();

                        char newChar = ' ';
                        for (var i = 0; i < numberOfChars; i++)
                        {
                            newChar = '*';
                            result += newChar;
                        }


                    }
                    listViewDatabaseList.Items.Clear();

                    foreach (var s in databases)
                    {
                        listViewDatabaseList.Items.Add(new ListViewItem(new string[]
                            { 
                                s.System.Database.ToString(),
                                s.Size.ToString() + "   MB",   
                                s.System.Version.ToString() 
                               
                            }

                                ));
                    }
                    foreach (ListViewItem item in listViewDatabaseList.Items)
                    {
                        item.ImageIndex = 0;
                    }
                    lblDatabaseNo.Text = "Databases  (" + databases.Count.ToString() + ")";
                }
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
            }
        }
        private bool IsServerLoginValid()
        {
            bool noerror = true;
            if (string.IsNullOrEmpty(cboServerName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboServerName, "Server Name cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtServerLoginUserName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtServerLoginUserName, "User Name cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtServerLoginPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtServerLoginPassword, "Password cannot be null!");
                return false;
            }
            return noerror;
        }
        private void btnQuitChangeSever_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void btnChangeServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider1.Clear();
            try
            {

                if (!tabControlDBPanel.TabPages.Contains(tabPageServerConnection))
                {
                    tabControlDBPanel.TabPages.Add(tabPageServerConnection);
                    tabControlDBPanel.TabPages.Remove(tabPageServerSettings);
                    txtConnectionLoginErrors.Text = string.Empty;
                    txtConnectionLoginErrors.Visible = false;

                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void DatabaseControlPanelForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtConnectionLoginErrors.Text = string.Empty;
                txtConnectionLoginErrors.Visible = false;

                listViewDatabaseList.View = System.Windows.Forms.View.Details;
                listViewDatabaseList.GridLines = true;
                listViewDatabaseList.FullRowSelect = true;
                listViewDatabaseList.MultiSelect = false;
                listViewDatabaseList.Columns.Add("", "Database", 200);
                listViewDatabaseList.Columns.Add("", "Size", 150); 
                listViewDatabaseList.Columns.Add("", "Version", -2);

                tabControlDBPanel.TabPages.Remove(tabPageServerSettings);
                tabControlDBPanel.TabPages.Remove(tabPageServerConnection);
                tabControlDBPanel.TabPages.Remove(tabPageDatabaseSettings);

                if (!tabControlDBPanel.TabPages.Contains(tabPageServerConnection))
                {
                    tabControlDBPanel.TabPages.Add(tabPageServerConnection);
                    tabControlDBPanel.TabPages.Remove(tabPageServerSettings);
                    tabControlDBPanel.TabPages.Remove(tabPageDatabaseSettings);
                }
                ImageList photoList = new ImageList();

                photoList.TransparentColor = Color.Blue;

                photoList.ColorDepth = ColorDepth.Depth32Bit;

                photoList.ImageSize = new Size(10, 10);
                photoList.Images.Add(Image.FromFile("Resources/CreateDB.jpg"));
                listViewDatabaseList.SmallImageList = photoList;

                AutoCompleteStringCollection acscsn = new AutoCompleteStringCollection();
                acscsn.AddRange(this.AutoComplete_ServerNames());
                cboServerName.AutoCompleteCustomSource = acscsn;
                cboServerName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                cboServerName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscslun = new AutoCompleteStringCollection();
                acscslun.AddRange(this.AutoComplete_UsersNames());
                txtServerLoginUserName.AutoCompleteCustomSource = acscslun;
                txtServerLoginUserName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtServerLoginUserName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                InitializeControls();

                NotifyMessage("Database Control Panel", "Database Control Panel helps to set up the system.");

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_ServerNames()
        {
            try
            {
                string auto_complete_servername_filename = "Resources/auto_complete_servername_dto.xml";
                List<string> logged_usernames = new List<string>();
                if (File.Exists(auto_complete_servername_filename))
                {
                    List<SBSystem_DTO> successfully_logged_users = SQLHelper.GetDataFromSBSystem_DTOXML(auto_complete_servername_filename);
                    foreach (var item in successfully_logged_users)
                    {
                        logged_usernames.Add(item.Name);
                    }
                }
                return logged_usernames.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_UsersNames()
        {
            try
            {
                string auto_complete_server_username_filename = "Resources/auto_complete_server_username_dto.xml";
                List<string> logged_usernames = new List<string>();
                if (File.Exists(auto_complete_server_username_filename))
                {
                    List<SBSystem_DTO> successfully_logged_users = SQLHelper.GetDataFromSBSystem_DTOXML(auto_complete_server_username_filename);
                    foreach (var item in successfully_logged_users)
                    {
                        logged_usernames.Add(item.Name);
                    }
                }
                return logged_usernames.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private bool InitializeControls()
        {
            try
            {
                string auto_complete_conn_info_filename = "Resources/auto_complete_conn_info_dto.xml";
                if (File.Exists(auto_complete_conn_info_filename))
                {
                    using (XmlReader xmlRdr = new XmlTextReader(auto_complete_conn_info_filename))
                    {
                        SBSystem_DCP_DTO dcp_dto = (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                                                    select new SBSystem_DCP_DTO(
                                                   (string)sysElem.Attribute("Name"),
                                                   (string)sysElem.Attribute("Application"),
                                                   (string)sysElem.Attribute("Database"),
                                                   (bool)sysElem.Attribute("SName"),
                                                   (bool)sysElem.Attribute("UName"),
                                                   (bool)sysElem.Attribute("PName")
                                                    )).FirstOrDefault();
                        if (dcp_dto.Name != null)
                        {
                            cboServerName.Text = dcp_dto.Name;
                        }
                        if (dcp_dto.Application != null)
                        {
                            txtServerLoginUserName.Text = dcp_dto.Application;
                        }
                        if (dcp_dto.Database != null)
                        {
                            txtServerLoginPassword.Text = dcp_dto.Database;
                        }
                        if (dcp_dto.Defaultsn != null)
                        {
                            chkRememberServerName.Checked = dcp_dto.Defaultsn;
                        }
                        if (dcp_dto.Defaultun != null)
                        {
                            chkRemeberUserName.Checked = dcp_dto.Defaultun;
                        }
                        if (dcp_dto.Defaultpd != null)
                        {
                            chkRememberPassword.Checked = dcp_dto.Defaultpd;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }
        }
        private void cboServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        } 
        private List<ServerDatabase> GetServerDatabases(Server server)
        {
            foreach (Database dtb in server.Databases)
            {
                if (!dtb.IsSystemObject && dtb.IsAccessible)
                {
                    var sbverion = dtb.ExtendedProperties[DatabaseVersionExtPropertyKey];
                    if (sbverion != null)
                    {

                        databases.Add(
                            new ServerDatabase(
                                  new SBSystem("newsys", SBApplication, dtb.Name, server.Name, "", Metadata, sbverion.Value.ToString(), false), dtb.Size)
                                  );
                    }
                }
            }
            return databases;
        }
        private void btnChangeDatabaseSettingsServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider1.Clear();
            try
            {
                if (!tabControlDBPanel.TabPages.Contains(tabPageServerConnection))
                {
                    tabControlDBPanel.TabPages.Add(tabPageServerConnection);
                    tabControlDBPanel.TabPages.Remove(tabPageDatabaseSettings);
                    txtConnectionLoginErrors.Text = string.Empty;
                    txtConnectionLoginErrors.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void btnChangeDatabaseSettingsDatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (!tabControlDBPanel.TabPages.Contains(tabPageServerSettings))
                {
                    tabControlDBPanel.TabPages.Add(tabPageServerSettings);
                    tabControlDBPanel.TabPages.Remove(tabPageDatabaseSettings);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void btnUpgradeDatabase_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void btnDatabaseSettingsLaunchSBSchool_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void btnDatabaseSettingsQuit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private void chkIntegratedSec_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntegratedSec.Checked)
            {
                groupBoxLoginCreds.Enabled = false;
            }
            else
            {
                groupBoxLoginCreds.Enabled = true;
            }
        }
        private void btnUpdateSystems_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                SystemsForm f = new SystemsForm();
                f.ShowDialog();
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex); 
            }
        } 
        private void btnCreateDBScripts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                if (listViewDatabaseList.SelectedItems.Count != 0)
                {
                    var item = listViewDatabaseList.SelectedItems[0];
                    string srv = lblSrvSttServerName.Text;
                    string db = item.SubItems[0].Text;
                     
                    SMOScripting.ScriptForm f = new SMOScripting.ScriptForm(new Server(srv), db);
                    f.Show();

                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }

        }
        private void btnCreateLoginUser_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if (listViewDatabaseList.SelectedItems.Count != 0)
            {
                try
                {
                    CreateDatabaseUserForm cduf = new CreateDatabaseUserForm() { Owner = this };
                    cduf.OnDatabaseUserSelected += new CreateDatabaseUserForm.DatabaseUserHandler(saf_OnSetDatabaseUserSelected);
                    cduf.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex); 
                }
            }
            else
            {
                MessageBox.Show("Select a Database", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void saf_OnSetDatabaseUserSelected(object sender, DatabaseUserEventArgs e)
        {
            SetDatabaseUser(e._UserName, e._Password);
        }
        private void SetDatabaseUser(string _Username, string _Password)
        {
            if (listViewDatabaseList.SelectedItems.Count != 0)
            {
                try
                {
                    ListViewItem.ListViewSubItem __SelectedDb = listViewDatabaseList.SelectedItems[0].SubItems[0];
                    string _Database = __SelectedDb.Text.ToString().Trim();
                    if (!string.IsNullOrEmpty(_Username) && !string.IsNullOrEmpty(_Password) && !string.IsNullOrEmpty(_Database))
                    {
                        CreateLogin(_Username, _Password, _Database);
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex); 
                }

            }
        }       
        private void btnLaunchApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Application.Restart();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }         
        private void tabControlDBPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControlDBPanel.SelectedTab == this.tabPageServerConnection)
                {
                    lblMsg.Text = "Ready";
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnSQLServerInstallation_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://softwareproviders.co.ke/MicrosoftSQLServer2008InstallGuide.html");
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }
        private void btnLoginAssistance_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://softwareproviders.co.ke/SoftBooksPayrollDatabaseControlPanel.html");
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start("http://softwareproviders.co.ke");
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar)) e.Handled = true;
                if (Char.IsLetter(e.KeyChar)) e.Handled = true;
                if (Char.IsNumber(e.KeyChar)) e.Handled = true;
                if (Char.IsPunctuation(e.KeyChar)) e.Handled = true;
                if (Char.IsSurrogate(e.KeyChar)) e.Handled = true;
                if (Char.IsSymbol(e.KeyChar)) e.Handled = true;
                if (Char.IsWhiteSpace(e.KeyChar)) e.Handled = true;
                if (e.KeyChar == (char)Keys.Back) e.Handled = true;
                if (e.KeyChar == (char)Keys.Space) e.Handled = true;
                if (e.KeyChar == (char)Keys.Delete) e.Handled = true;
                if (e.KeyChar == (char)Keys.Clear) e.Handled = true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtEmail_Click(object sender, EventArgs e)
        {
            try
            {
                txtEmail.SelectAll();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void copyEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtEmail.Text))
                {
                    Clipboard.SetText(txtEmail.Text);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NotifyMessage("Database Control Panel", "Exiting...");
                this.Close();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://softwareproviders.co.ke/SoftBooksPayrollDatabaseControlPanel.html");
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public bool NotifyMessage(string _Title, string _Text)
        {
            try
            {
                appNotifyIcon.Text = "Database Control Panel";
                appNotifyIcon.Icon = new Icon("Resources/Icons/Dollar.ico");
                appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                appNotifyIcon.ContextMenuStrip = contextMenuStripSystemNotification;
                appNotifyIcon.BalloonTipTitle = _Title;
                appNotifyIcon.BalloonTipText = _Text;
                appNotifyIcon.Visible = true;
                appNotifyIcon.ShowBalloonTip(900000);

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        #endregion "Private Methods"

        

    }

    public class ServerDatabase
    {
        public SBSystem System;
        public double Size;
        public ServerDatabase(SBSystem system, double sz)
        {
            this.System = system;
            this.Size = sz;
        }
    }

}
