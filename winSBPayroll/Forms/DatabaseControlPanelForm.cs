extern alias extendend;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using CommonLib;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace winSBPayroll.Forms
{
    public partial class DatabaseControlPanelForm : Form
    {

        #region "Private Fields"
        const string SBApplication = "SBPayroll";
        const string Metadata = "SBPayrollDBEntities";
        const string DatabaseVersionExtPropertyKey = SBApplication + "Version";
        //const string DatabaseVersionExtPropertyValue = "1.0.0.0";
        string DatabaseVersionExtPropertyValue = Application.ProductVersion;
        const string tempFile = @"DBScripts\db.sql";
        List<ServerDatabase> databases;
        List<string> server_names;
        /* to use a BackgroundWorker object to perform time-intensive operations in a background thread.
            You need to:
            1. Define a worker method that does the time-intensive work and call it from an event handler for the DoWork
            event of a BackgroundWorker.
            2. Start the execution with RunWorkerAsync. Any argument required by the worker method attached to DoWork
            can be passed in via the DoWorkEventArgs parameter to RunWorkerAsync.
            In addition to the DoWork event the BackgroundWorker class also defines two events that should be used for
            interacting with the user interface. These are optional.
            The RunWorkerCompleted event is triggered when the DoWork handlers have completed.
            The ProgressChanged event is triggered when the ReportProgress method is called. */
        BackgroundWorker bgWorker = new BackgroundWorker();
        private string current_action = string.Empty;
        //task start time
        DateTime _task_start_time = DateTime.Now;
        //task end time
        DateTime _task_end_time = DateTime.Now;
        string TAG;
        List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname_from_parent;
        #endregion "Private Fields"

        #region "Constructor"
        public DatabaseControlPanelForm()
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _notificationmessageEventname_from_parent += notificationmessageHandler;

            server_names = new List<string>();
            databases = new List<ServerDatabase>();

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished main_form initialization", TAG));

        }
        public DatabaseControlPanelForm(EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _notificationmessageEventname_from_parent = notificationmessageEventname_from_parent;

            server_names = new List<string>();
            databases = new List<ServerDatabase>();

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished main_form initialization", TAG));

        }
        #endregion "Constructor"

        #region "Public Properties"
        public string ServerName { get; set; }
        public string LoginUserName { get; set; }
        public string LoginPassword { get; set; }
        #endregion "Public Properties"

        #region "Private Methods"
        private void DatabaseControlPanelForm_Load(object sender, EventArgs e)
        {
            try
            {
                txtConnectionLoginErrors.Text = string.Empty;
                txtConnectionLoginErrors.Visible = false;
                txtEmail.Text = "softwareproviders254@gmail.com";
                progressBar_connect.Visible = false;
                progressBar_settings.Visible = false;

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
                cboserver.AutoCompleteCustomSource = acscsn;
                cboserver.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                cboserver.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscslun = new AutoCompleteStringCollection();
                acscslun.AddRange(this.AutoComplete_UsersNames());
                txtusername.AutoCompleteCustomSource = acscslun;
                txtusername.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtusername.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                InitializeControls();

                populate_auto_complete_values();

                NotifyMessage("Database Control Panel", "Database Control Panel helps to set up the system.");

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished DatabaseControlPanelForm load", TAG));

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            this._notificationmessageEventname.Invoke(sender, new notificationmessageEventArgs(ex.Message, TAG));
        }

        private void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            this._notificationmessageEventname.Invoke(sender, new notificationmessageEventArgs(ex.Message, TAG));
        }

        //Event handler declaration:
        private void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            try
            {
                /* Handler logic */

                //Invoke(new Action(() =>
                //{

                notificationdto _notificationdto = new notificationdto();

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                _lstnotificationdto.Add(_notificationdto);
                Console.WriteLine(args.message);
                _notificationmessageEventname_from_parent.Invoke(this, new notificationmessageEventArgs(args.message, TAG));

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                txtlog_connect.Lines = _logflippedlines;
                txtlog_connect.ScrollToCaret();

                txtlog_settings.Lines = _logflippedlines;
                txtlog_settings.ScrollToCaret();

                Log.WriteToErrorLogFile_and_EventViewer(new Exception(_logtext));

                //}));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void InitializeControls()
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
                            cboserver.Text = dcp_dto.Name;
                        }
                        if (dcp_dto.Application != null)
                        {
                            txtusername.Text = dcp_dto.Application;
                        }
                        if (dcp_dto.Database != null)
                        {
                            txtpassword.Text = dcp_dto.Database;
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
                MessageBox.Show(string.Format("User [ {0} ] Created Successsfully on Database  [ {1} ] ", sqlLoginName, databaseName), Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void btnRestoreNow_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider.Clear();
            if (string.IsNullOrEmpty(txtNewDatabaseName.Text))
            {
                errorProvider.SetError(txtNewDatabaseName, "Database Name cannot be null!");
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
                        MessageBox.Show("Backup complete!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        public void ProgressEventHandler(object sender, extendend::Microsoft.SqlServer.Management.Smo.PercentCompleteEventArgs e)
        {
            this.progressBar_settings.Value = e.Percent;
        }
        private void btnSeeDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider.Clear();
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
                errorProvider.Clear();

                btnGetServerList.Enabled = false;
                btnConnect.Enabled = false;
                //btnQuitChangeSever.Enabled = false;

                current_action = "server";

                _task_start_time = DateTime.Now;

                //This allows the BackgroundWorker to be cancelled in between tasks
                bgWorker.WorkerSupportsCancellation = true;
                //This allows the worker to report progress between completion of tasks...
                //this must also be used in conjunction with the ProgressChanged event
                bgWorker.WorkerReportsProgress = true;

                //this assigns event handlers for the backgroundWorker
                bgWorker.DoWork += bgWorker_DoWork;
                bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;
                /* When you wish to have something occur when a change in progress
                    occurs, (like the completion of a specific task) the "ProgressChanged"
                    event handler is used. Note that ProgressChanged events may be invoked
                    by calls to bgWorker.ReportProgress(...) only if bgWorker.WorkerReportsProgress
                    is set to true. */
                bgWorker.ProgressChanged += bgWorker_ProgressChanged;

                //tell the backgroundWorker to raise the "DoWork" event, thus starting it.
                //Check to make sure the background worker is not already running.
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync();


                //DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources();
                //if (dt.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //    {
                //        string InstanceName = dr["InstanceName"].ToString();
                //        string ServerName = dr["ServerName"].ToString();
                //        if (string.IsNullOrEmpty(InstanceName) || string.IsNullOrEmpty(ServerName))
                //        {
                //            this.ServerName = ServerName;
                //            this.server_names.Add(this.ServerName);
                //        }

                //        if (!string.IsNullOrEmpty(InstanceName) && !string.IsNullOrEmpty(ServerName))
                //        {
                //            this.ServerName = ServerName + "\\" + InstanceName;
                //            this.server_names.Add(this.ServerName);
                //        }
                //    }
                //    cboServerName.DataSource = this.server_names;
                //}


            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //this is the method that the backgroundworker will perform on in the background thread without blocking the UI.
                /* One thing to note! A try catch is not necessary as any exceptions will terminate the
                backgroundWorker and report the error to the "RunWorkerCompleted" event */

                progressBar_connect.Invoke(new Action(() =>
                {
                    progressBar_connect.Visible = true;
                }));

                if (current_action.Equals("server"))
                {
                    try
                    {
                        Invoke(new Action(() =>
                        {
                            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Enumerating available servers...", TAG));
                        }));

                        List<string> srvNames = new List<string>();
                        DataTable dt = SqlDataSourceEnumerator.Instance.GetDataSources();
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string InstanceName = dr["InstanceName"].ToString();
                                string ServerName = dr["ServerName"].ToString();

                                if (string.IsNullOrEmpty(InstanceName) || string.IsNullOrEmpty(ServerName))
                                {
                                    srvNames.Add(ServerName);

                                    Invoke(new Action(() =>
                                    {
                                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ServerName, TAG));
                                    }));
                                }

                                if (!string.IsNullOrEmpty(InstanceName) && !string.IsNullOrEmpty(ServerName))
                                {
                                    ServerName = ServerName + "\\" + InstanceName;
                                    srvNames.Add(ServerName);

                                    Invoke(new Action(() =>
                                    {
                                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ServerName, TAG));

                                    }));
                                }
                            }

                            int count = srvNames.Count;

                            Debug.WriteLine("servers: " + count);

                            Invoke(new Action(() =>
                            {
                                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("found [ " + count + " ] servers.", TAG));

                            }));

                            cboserver.Invoke(new Action(() =>
                            {
                                cboserver.DataSource = srvNames;
                            }));

                        }
                        else
                        {
                            string computer_name = Utils.get_computer_name();

                            srvNames.Add(computer_name);
                            srvNames.Add(computer_name + "\\sqlexpress");
                            srvNames.Add(".");
                            srvNames.Add(".\\sqlexpress");

                            cboserver.Invoke(new Action(() =>
                            {
                                cboserver.DataSource = srvNames;
                            }));

                        }
                    }
                    catch (Exception ex)
                    {
                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                        Log.WriteToErrorLogFile_and_EventViewer(ex);
                    }
                }
                else
                {
                    try
                    {
                        errorProvider.Clear();
                        bool isvalid = false;

                        Invoke(new Action(() =>
                        {
                            isvalid = IsServerLoginValid();
                        }));

                        if (isvalid)
                        {
                            string serverName = string.Empty;
                            string userName = string.Empty;
                            string password = string.Empty;
                            bool IntegratedSecurity = false;

                            Invoke(new Action(() =>
                            {
                                serverName = cboserver.Text.Trim();
                                userName = txtusername.Text.Trim();
                                password = txtpassword.Text.Trim();
                                IntegratedSecurity = chkIntegratedSecurity.Checked;
                            }));

                            Invoke(new Action(() =>
                            {
                                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Establishing connection to the server.", TAG));
                            }));

                            Microsoft.SqlServer.Management.Smo.Server server = ConnectToServer(serverName, userName, password, IntegratedSecurity);

                            if (server == null)
                            {
                                Invoke(new Action(() =>
                                {
                                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Failed to connect to server.", TAG));
                                }));
                            }
                            else
                            {
                                if (server.ConnectionContext.IsOpen)
                                {
                                    string msg = "Edition: " + server.Edition + Environment.NewLine +
                                        "IsClustered: " + server.IsClustered + Environment.NewLine +
                                        "Build: " + server.BuildNumber + Environment.NewLine +
                                        "Net Name: " + server.NetName + Environment.NewLine +
                                        "Instance: " + server.InstanceName + Environment.NewLine +
                                        "Physical Memory: " + server.PhysicalMemory.ToString() + Environment.NewLine +
                                        "Platform: " + server.Platform + Environment.NewLine +
                                        "Processors: " + server.Processors + Environment.NewLine +
                                        "Type: " + server.ServerType + Environment.NewLine +
                                        "Service Id: " + server.ServiceInstanceId + Environment.NewLine +
                                        "Start Mode: " + server.ServiceStartMode.ToString() + Environment.NewLine +
                                        "State: " + server.State;

                                    Invoke(new Action(() =>
                                    {
                                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Connected to Server successfully.", TAG));

                                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));
                                    }));

                                    NotifyMessage("Connected to Server successfully.", msg);

                                    List<string> _databases = new List<string>();

                                    _databases = Get_Server_Databases(server);

                                    int count = _databases.Count;

                                    Debug.WriteLine("databases: " + count);

                                    databases.Clear();
                                    databases = GetServerDatabases(server);

                                    Invoke(new Action(() =>
                                    {
                                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("found [ " + count + " ] databases.", TAG));
                                    }));

                                    this.ServerName = serverName;
                                    this.LoginUserName = userName;
                                    this.LoginPassword = password;

                                    Invoke(new MethodInvoker(delegate()
                                    {
                                        DisplayServer(server, LoginUserName, LoginPassword);
                                    }));

                                    Invoke(new MethodInvoker(delegate()
                                    {
                                        save_auto_complete_login();
                                    }));

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                        Log.WriteToErrorLogFile_and_EventViewer(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }

        /*This is how the ProgressChanged event method signature looks like:*/
        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // Things to be done when a progress change has been reported
            /* The ProgressChangedEventArgs gives access to a percentage,
            allowing for easy reporting of how far along a process is*/
            int progress = e.ProgressPercentage;
        }

        private void bgWorker_WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                bgWorker.DoWork -= bgWorker_DoWork;
                bgWorker.RunWorkerCompleted -= bgWorker_WorkComplete;
                bgWorker.ProgressChanged -= bgWorker_ProgressChanged;

                _task_end_time = DateTime.Now;

                var _time_lapsed = _task_end_time.Subtract(_task_start_time);

                string msg = "Task Complete!" + Environment.NewLine + "Task took [ " + _time_lapsed + " ]";

                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));

                btnGetServerList.Enabled = true;
                btnConnect.Enabled = true;
                btnQuitChangeSever.Enabled = true;

                progressBar_connect.Invoke(new Action(() =>
                {
                    progressBar_connect.Visible = false;
                }));
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private bool IsServerLoginValid()
        {
            bool noerror = true;
            if (string.IsNullOrEmpty(cboserver.Text))
            {
                errorProvider.SetError(cboserver, "Server Name cannot be null!");
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Server Name cannot be null!", TAG));
                noerror = false;
            }
            if (!chkIntegratedSecurity.Checked)
            {
                if (string.IsNullOrEmpty(txtusername.Text))
                {
                    errorProvider.SetError(txtusername, "User Name cannot be null!");
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("User Name cannot be null!", TAG));
                    noerror = false;
                }
                if (string.IsNullOrEmpty(txtpassword.Text))
                {
                    errorProvider.SetError(txtpassword, "Password cannot be null!");
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Password cannot be null!", TAG));
                    noerror = false;
                }
            }
            return noerror;
        }
        private void btnConnect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                errorProvider.Clear();
                if (string.IsNullOrEmpty(cboserver.Text))
                {
                    errorProvider.SetError(cboserver, "select server!");
                    return;
                }

                btnConnect.Enabled = false;
                btnGetServerList.Enabled = false;
                //btnQuitChangeSever.Enabled = false;

                current_action = "connect";

                _task_start_time = DateTime.Now;

                //This allows the BackgroundWorker to be cancelled in between tasks
                bgWorker.WorkerSupportsCancellation = true;
                //This allows the worker to report progress between completion of tasks...
                //this must also be used in conjunction with the ProgressChanged event
                bgWorker.WorkerReportsProgress = true;

                //this assigns event handlers for the backgroundWorker
                bgWorker.DoWork += bgWorker_DoWork;
                bgWorker.RunWorkerCompleted += bgWorker_WorkComplete;
                /* When you wish to have something occur when a change in progress
                    occurs, (like the completion of a specific task) the "ProgressChanged"
                    event handler is used. Note that ProgressChanged events may be invoked
                    by calls to bgWorker.ReportProgress(...) only if bgWorker.WorkerReportsProgress
                    is set to true. */
                bgWorker.ProgressChanged += bgWorker_ProgressChanged;

                //tell the backgroundWorker to raise the "DoWork" event, thus starting it.
                //Check to make sure the background worker is not already running.
                if (!bgWorker.IsBusy)
                    bgWorker.RunWorkerAsync();

                //errorProvider.Clear();

                //if (!chkIntegratedSecurity.Checked)
                //{
                //    if (!IsServerLoginValid())
                //    {
                //        MessageBox.Show("Incorrect Credentials, make sure the ServerName, UserName and Password are entered.", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }
                //}

                //txtConnectionLoginErrors.Text = string.Empty;

                //this.ServerName = (string)cboserver.Text.Trim();
                //this.LoginUserName = (string)txtusername.Text.Trim();
                //this.LoginPassword = (string)txtpassword.Text.Trim();

                //Server server = ConnectToServer(ServerName, LoginUserName, LoginPassword, chkIntegratedSecurity.Checked);

                //if (server.ConnectionContext.IsOpen)
                //{
                //    databases.Clear();
                //    databases = GetServerDatabases(server);

                //    DisplayServer(server, LoginUserName, LoginPassword);

                //    NotifyMessage("Connected to Server.", "Edition: " + server.Edition + Environment.NewLine + "IsClustered: " + server.IsClustered + Environment.NewLine + "Build: " + server.BuildNumber + Environment.NewLine + "Net Name: " + server.NetName + Environment.NewLine + "Instance: " + server.InstanceName + Environment.NewLine + "Physical Memory: " + server.PhysicalMemory.ToString() + Environment.NewLine + "Platform: " + server.Platform + Environment.NewLine + "Processors: " + server.Processors + Environment.NewLine + "Type: " + server.ServerType + Environment.NewLine + "Service Id: " + server.ServiceInstanceId + Environment.NewLine + "Start Mode: " + server.ServiceStartMode.ToString() + Environment.NewLine + "State: " + server.State);
                //}

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }
        private List<string> Get_Server_Databases(Microsoft.SqlServer.Management.Smo.Server server)
        {
            List<string> databases = new List<string>();
            try
            {
                Invoke(new Action(() =>
                {
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("enumerating available databases...", TAG));
                }));

                foreach (Microsoft.SqlServer.Management.Smo.Database dtb in server.Databases)
                {
                    if (!dtb.IsSystemObject && dtb.IsAccessible)
                    {
                        var sbverion = dtb.ExtendedProperties[DatabaseVersionExtPropertyKey];
                        if (sbverion != null)
                        {
                            databases.Add(dtb.Name);

                            string msg = "Name: " + dtb.Name + Environment.NewLine + "Size: " + dtb.Size + " MB" + Environment.NewLine + "Owner: " + dtb.Owner + Environment.NewLine + "Tables count: " + dtb.Tables.Count;

                            Invoke(new Action(() =>
                            {
                                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));

                            }));

                            Debug.WriteLine(msg);

                            //databases.Add(
                            //    new ServerDatabase(
                            //          new SBSystem("newsys", SBApplication, dtb.Name, server.Name, "", Metadata, sbverion.Value.ToString(), false), dtb.Size)
                            //          );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
            return databases;
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
                //Save_Conn_Info_in_Config();
                return server;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                {
                    msg += "\n" + ex.InnerException.Message;
                }
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                return null;
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

        private void save_auto_complete_login()
        {
            try
            {
                string auto_complete_server_login_filename = Utils.AUTO_COMPLETE_SERVER_LOGIN_FILENAME;

                string system = Utils.APP_NAME;
                string serverName = cboserver.Text.Trim();
                string databaseName = "";
                string userName = txtusername.Text.Trim();
                string password = txtpassword.Text.Trim();
                password = Utils.encrypt_string(password);
                bool IntegratedSecurity = chkIntegratedSecurity.Checked;
                bool remember_username = chkRemeberUserName.Checked;
                bool remember_password = chkRememberPassword.Checked;
                bool remember_servername = chkRememberServerName.Checked;

                if (File.Exists(auto_complete_server_login_filename))
                {
                    //List<SB_Server_Login> successfully_logged_users = GetDataFromSB_LoginXML(auto_complete_server_login_filename);

                    //var exists = successfully_logged_users.Where(i => i.userName.Equals(userName) && i.databaseName.Equals(databaseName) && i.serverName.Equals(serverName) && i.system.Equals(system)).FirstOrDefault();

                    DataSet ds = new DataSet();

                    ds.ReadXml(auto_complete_server_login_filename);

                    int count = ds.Tables[0].Rows.Count;

                    for (int i = 0; i < count; i++)
                    {
                        ds.Tables[0].DefaultView.RowFilter = "userName = '" + userName + "' and databaseName = '" + databaseName + "' and serverName = '" + serverName + "' and system = '" + system + "'";

                        DataTable dt = (ds.Tables[0].DefaultView).ToTable();

                        if (dt.Rows.Count > 0)
                        {
                            ds.Tables[0].Rows[i].Delete();
                        }
                    }

                    //get data
                    string xmlData = ds.GetXml();

                    //save to file
                    ds.WriteXml(auto_complete_server_login_filename);

                    XDocument doc = XDocument.Load(auto_complete_server_login_filename);
                    doc.Element("Systems").Add(
                        new XElement("System",
                        new XAttribute("system", system),
                        new XAttribute("serverName", serverName),
                        new XAttribute("databaseName", databaseName),
                        new XAttribute("userName", userName),
                        new XAttribute("password", password),
                        new XAttribute("IntegratedSecurity", IntegratedSecurity.ToString()),
                        new XAttribute("remember_username", remember_username.ToString()),
                        new XAttribute("remember_password", remember_password.ToString()),
                        new XAttribute("remember_servername", remember_servername.ToString())
                        ));

                    doc.Save(auto_complete_server_login_filename);

                }

                if (!File.Exists(auto_complete_server_login_filename))
                {
                    List<SB_Server_Login> systems = new List<SB_Server_Login>() { 
                        new SB_Server_Login(
                           system, 
                           serverName,
                           databaseName,
                           userName,
                           password,
                           IntegratedSecurity.ToString(),
                           remember_username.ToString(),                            
                           remember_password.ToString(),     
                           remember_servername.ToString()     
                            )};

                    var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                            new XAttribute("system", x.system),
                            new XAttribute("serverName", x.serverName),
                            new XAttribute("databaseName", x.databaseName),
                            new XAttribute("userName", x.userName),
                            new XAttribute("password", x.password),
                            new XAttribute("IntegratedSecurity", x.IntegratedSecurity),
                            new XAttribute("remember_username", x.remember_username),
                            new XAttribute("remember_password", x.remember_password),
                            new XAttribute("remember_servername", x.remember_servername)
                                           )));

                    xml.Save(auto_complete_server_login_filename);
                }
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }
        private List<SB_Server_Login> GetDataFromSB_LoginXML(string filename)
        {
            using (XmlReader xmlRdr = new XmlTextReader(filename))
            {
                return new List<SB_Server_Login>(
                   (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                    select new SB_Server_Login(
                       (string)sysElem.Attribute("system"),
                       (string)sysElem.Attribute("serverName"),
                       (string)sysElem.Attribute("databaseName"),
                       (string)sysElem.Attribute("userName"),
                       (string)sysElem.Attribute("password"),
                       (string)sysElem.Attribute("IntegratedSecurity"),
                       (string)sysElem.Attribute("remember_username"),
                       (string)sysElem.Attribute("remember_password"),
                       (string)sysElem.Attribute("remember_servername")
                                )).ToList());
            }
        }
        private void populate_auto_complete_values()
        {
            try
            {
                List<SB_Server_Login> auto_complete_from_xml = GetDataFromSB_LoginXML(Utils.AUTO_COMPLETE_SERVER_LOGIN_FILENAME);

                List<string> saved_servers = new List<string>();
                List<string> saved_databases = new List<string>();

                SB_Server_Login last_record = auto_complete_from_xml.Last();

                if (last_record == null)
                {
                    return;
                }

                foreach (var item in auto_complete_from_xml)
                {
                    if (!saved_servers.Contains(item.serverName))
                    {
                        saved_servers.Add(item.serverName);
                    }
                    if (!saved_databases.Contains(item.databaseName))
                    {
                        saved_databases.Add(item.databaseName);
                    }
                }

                //cbosystem.SelectedValue = last_record.system;

                //cbodatabase.DataSource = saved_databases;
                //cbodatabase.SelectedItem = last_record.databaseName;


                if (bool.Parse(last_record.remember_username))
                {
                    txtusername.Text = last_record.userName;
                }

                if (bool.Parse(last_record.remember_password))
                {
                    string password = Utils.decrypt_string(last_record.password);
                    txtpassword.Text = password;
                }

                if (bool.Parse(last_record.remember_servername))
                {
                    cboserver.DataSource = saved_servers;
                    cboserver.SelectedItem = last_record.serverName;
                }

                chkIntegratedSecurity.Checked = bool.Parse(last_record.IntegratedSecurity);

                chkRemeberUserName.Checked = bool.Parse(last_record.remember_username);
                chkRememberPassword.Checked = bool.Parse(last_record.remember_password);
                chkRememberServerName.Checked = bool.Parse(last_record.remember_servername);

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private bool Save_Conn_Info_in_Config()
        {
            try
            {
                if (!chkIntegratedSecurity.Checked && !string.IsNullOrEmpty(txtusername.Text) && !string.IsNullOrEmpty(cboserver.Text))
                {
                    SBSystem_DTO sysdto = new SBSystem_DTO(txtusername.Text, cboserver.Text);
                    Save_Auto_complete_username_info(sysdto);
                }
                if (!string.IsNullOrEmpty(cboserver.Text))
                {
                    SBSystem_DTO sysdto = new SBSystem_DTO(cboserver.Text, DateTime.Now.ToString());
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
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboserver.Text) && chkRemeberUserName.Checked && !string.IsNullOrEmpty(txtusername.Text) && chkRememberPassword.Checked && !string.IsNullOrEmpty(txtpassword.Text) && !chkIntegratedSecurity.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                           new XAttribute("Name", cboserver.Text),
                                            new XAttribute("Application", txtusername.Text),
                                              new XAttribute("Database", txtpassword.Text),
                                              new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                              new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                            new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //sname,uname,!pname,!int
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboserver.Text) && chkRemeberUserName.Checked && !string.IsNullOrEmpty(txtusername.Text) && !chkRememberPassword.Checked && !chkIntegratedSecurity.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                           new XAttribute("Name", cboserver.Text),
                                            new XAttribute("Application", txtusername.Text),
                                              new XAttribute("Database", ""),
                                              new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                              new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                            new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //sname,!uname,!pname,int
                if (chkIntegratedSecurity.Checked && chkRememberServerName.Checked && !string.IsNullOrEmpty(cboserver.Text))
                {
                    var xml = new XElement("Systems", new XElement("System",
                                           new XAttribute("Name", cboserver.Text),
                                            new XAttribute("Application", ""),
                                              new XAttribute("Database", ""),
                                              new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                              new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                            new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //!sname,!uname,!pname,!int
                if (!chkRememberServerName.Checked && !chkRemeberUserName.Checked && !chkRememberPassword.Checked && !chkIntegratedSecurity.Checked)
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
                if (!chkRememberServerName.Checked && chkIntegratedSecurity.Checked)
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
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboserver.Text) && chkRemeberUserName.Checked && chkRememberPassword.Checked && chkIntegratedSecurity.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                            new XAttribute("Name", cboserver.Text),
                                             new XAttribute("Application", ""),
                                               new XAttribute("Database", ""),
                                               new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                               new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                             new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //sname,!uname,!pname,!int
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboserver.Text) && !chkRemeberUserName.Checked && !chkRememberPassword.Checked && !chkIntegratedSecurity.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                            new XAttribute("Name", cboserver.Text),
                                             new XAttribute("Application", ""),
                                               new XAttribute("Database", ""),
                                               new XAttribute("SName", chkRememberServerName.Checked.ToString()),
                                               new XAttribute("UName", chkRemeberUserName.Checked.ToString()),
                                             new XAttribute("PName", chkRememberPassword.Checked.ToString())));
                    xml.Save(auto_complete_conn_info_filename);
                }
                //sname,uname,!pname,int
                if (chkRememberServerName.Checked && !string.IsNullOrEmpty(cboserver.Text) && chkRemeberUserName.Checked && !chkRememberPassword.Checked && chkIntegratedSecurity.Checked)
                {
                    var xml = new XElement("Systems", new XElement("System",
                                            new XAttribute("Name", cboserver.Text),
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
                    if (!logged_usernames.Contains(cboserver.Text))
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
                        }));
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
        private void btnQuitChangeSever_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void btnChangeServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            errorProvider.Clear();
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
            errorProvider.Clear();
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
        private void chkIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntegratedSecurity.Checked)
            {

                groupBoxLoginCreds.Invoke(new Action(() =>
                {
                    groupBoxLoginCreds.Enabled = false;
                }));
            }
            else
            {
                groupBoxLoginCreds.Invoke(new Action(() =>
                {
                    groupBoxLoginCreds.Enabled = true;
                }));
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
                MessageBox.Show("Select a Database", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void btnload_log_file_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            load_log_file_form log_form = new load_log_file_form();
            log_form.Show();
        }

        #endregion "Private Methods"

        #region "helpers"

        private Microsoft.SqlServer.Management.Smo.Server GetServer(string serverName, string userName, string password)
        {
            ServerConnection conn = new ServerConnection(serverName, userName, password);
            Microsoft.SqlServer.Management.Smo.Server myServer = new Microsoft.SqlServer.Management.Smo.Server(conn);
            return myServer;
        }
        private Microsoft.SqlServer.Management.Smo.Server GetServer(string serverName)
        {
            Microsoft.SqlServer.Management.Smo.Server myServer = new Microsoft.SqlServer.Management.Smo.Server(serverName);
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
                Microsoft.SqlServer.Management.Smo.Server oServer = GetServer(servername);

                // Backup
                extendend::Microsoft.SqlServer.Management.Smo.Backup backup = new extendend::Microsoft.SqlServer.Management.Smo.Backup();
                backup.Action = extendend::Microsoft.SqlServer.Management.Smo.BackupActionType.Database;
                backup.Database = DatabaseName;
                backup.Incremental = false;
                backup.Initialize = true;
                backup.LogTruncation = extendend::Microsoft.SqlServer.Management.Smo.BackupTruncateLogType.Truncate;

                // Backup Device
                extendend::Microsoft.SqlServer.Management.Smo.BackupDeviceItem backupItemDevice = new extendend::Microsoft.SqlServer.Management.Smo.BackupDeviceItem(sFileName, extendend::Microsoft.SqlServer.Management.Smo.DeviceType.File);
                backup.Devices.Add(backupItemDevice);

                // Start Backup
                backup.SqlBackup(oServer);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
        public bool backup_database_automatically(string servername, string DatabaseName, string back_up_path, string formatted_filename)
        {
            try
            {
                string formated_db_name = DatabaseName + "_" + formatted_filename + ".bak";
                // Filename
                string sFileName = Utils.build_file_path(back_up_path, formated_db_name);
                //string sFileName = string.Format("{0}\\{1}.bak", _sWhereToBackup, formated_db_name);

                // Connection 
                //string servername = lblSrvSttServerName.Text;
                if (string.IsNullOrEmpty(servername))
                    throw new ArgumentNullException("servername");
                Microsoft.SqlServer.Management.Smo.Server oServer = GetServer(servername);

                // Backup
                extendend::Microsoft.SqlServer.Management.Smo.Backup backup = new extendend::Microsoft.SqlServer.Management.Smo.Backup();
                backup.Action = extendend::Microsoft.SqlServer.Management.Smo.BackupActionType.Database;
                backup.Database = DatabaseName;
                backup.Incremental = false;
                backup.Initialize = true;
                backup.LogTruncation = extendend::Microsoft.SqlServer.Management.Smo.BackupTruncateLogType.Truncate;

                // Backup Device
                extendend::Microsoft.SqlServer.Management.Smo.BackupDeviceItem backupItemDevice = new extendend::Microsoft.SqlServer.Management.Smo.BackupDeviceItem(sFileName, extendend::Microsoft.SqlServer.Management.Smo.DeviceType.File);
                backup.Devices.Add(backupItemDevice);

                // Start Backup
                backup.SqlBackup(oServer);

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                Log.WriteToErrorLogFile(ex);
                return false;
            }

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
                Microsoft.SqlServer.Management.Smo.Server myServer = GetServer(servername);

                extendend::Microsoft.SqlServer.Management.Smo.Restore myRestore = new extendend::Microsoft.SqlServer.Management.Smo.Restore();
                myRestore.Database = destinationDatabaseName;
                Database currentDb = myServer.Databases[destinationDatabaseName];
                if (currentDb != null)
                    myServer.KillAllProcesses(destinationDatabaseName);
                myRestore.Devices.AddDevice(BackupFilePath, extendend::Microsoft.SqlServer.Management.Smo.DeviceType.File);
                string DataFileLocation = DatabaseFolder + "\\" + destinationDatabaseName + ".mdf";
                string LogFileLocation = DatabaseFolder + "\\" + destinationDatabaseName + "_log.ldf";

                System.Data.DataTable logicalRestoreFiles = myRestore.ReadFileList(myServer); myRestore.RelocateFiles.Add(new extendend::Microsoft.SqlServer.Management.Smo.RelocateFile(logicalRestoreFiles.Rows[0][0].ToString(), DataFileLocation)); myRestore.RelocateFiles.Add(new extendend::Microsoft.SqlServer.Management.Smo.RelocateFile(logicalRestoreFiles.Rows[1][0].ToString(), LogFileLocation));
                myRestore.ReplaceDatabase = true;
                myRestore.PercentCompleteNotification = 10;
                myRestore.PercentComplete +=
                    new extendend::Microsoft.SqlServer.Management.Smo.PercentCompleteEventHandler(myRestore_PercentComplete);
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
        public void BackupDatabase(SqlConnectionStringBuilder conn_str, string destinationPath)
        {
            try
            {
                ServerConnection connection = new ServerConnection(conn_str.DataSource, conn_str.UserID, conn_str.Password);
                Microsoft.SqlServer.Management.Smo.Server sqlServer = new Microsoft.SqlServer.Management.Smo.Server(connection);

                extendend::Microsoft.SqlServer.Management.Smo.Backup bkpDatabase = new extendend::Microsoft.SqlServer.Management.Smo.Backup();
                bkpDatabase.Action = extendend::Microsoft.SqlServer.Management.Smo.BackupActionType.Database;
                bkpDatabase.Database = conn_str.InitialCatalog;
                extendend::Microsoft.SqlServer.Management.Smo.BackupDeviceItem bkpDevice = new extendend::Microsoft.SqlServer.Management.Smo.BackupDeviceItem(destinationPath, extendend::Microsoft.SqlServer.Management.Smo.DeviceType.File);
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
                Microsoft.SqlServer.Management.Smo.Server sqlServer = new Microsoft.SqlServer.Management.Smo.Server(connection);
                extendend::Microsoft.SqlServer.Management.Smo.Restore rstDatabase = new extendend::Microsoft.SqlServer.Management.Smo.Restore();
                rstDatabase.Action = extendend::Microsoft.SqlServer.Management.Smo.RestoreActionType.Database;
                rstDatabase.Database = databaseName;
                extendend::Microsoft.SqlServer.Management.Smo.BackupDeviceItem bkpDevice = new extendend::Microsoft.SqlServer.Management.Smo.BackupDeviceItem(backUpFile, extendend::Microsoft.SqlServer.Management.Smo.DeviceType.File);
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
        private void myRestore_PercentComplete(object sender, extendend::Microsoft.SqlServer.Management.Smo.PercentCompleteEventArgs e)
        {
            WriteToLogAndConsole(e.Percent.ToString() + "% Complete");
        }
        private void WriteToLogAndConsole(string msg)
        {
            lblMsg.Text = msg;
        }

        private void sqlRestore_PercentComplete(object sender, extendend::Microsoft.SqlServer.Management.Smo.PercentCompleteEventArgs e)
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
        #endregion "helpers"

    }

}
