using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using Microsoft.SqlServer.Management.Smo;
using System.Data.Sql;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace winSBPayroll.Forms
{
    public partial class CreateDatabaseUserForm : Form
    {
        //delegate
        public delegate void DatabaseUserHandler(object sender, DatabaseUserEventArgs e);
        //event
        public event DatabaseUserHandler OnDatabaseUserSelected;
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
        List<ServerDatabase> databases;
        List<string> server_names;
        const string SBApplication = "SBPayroll";
        const string Metadata = "SBPayrollDBEntities";
        const string DatabaseVersionExtPropertyKey = SBApplication + "Version";
        //const string DatabaseVersionExtPropertyValue = "1.0.0.0";
        string DatabaseVersionExtPropertyValue = Application.ProductVersion;

        public string ServerName { get; set; }
        public string LoginUserName { get; set; }
        public string LoginPassword { get; set; }

        public CreateDatabaseUserForm(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname = notificationmessageEventname;

            server_names = new List<string>();
            databases = new List<ServerDatabase>();

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished CreateDatabaseUserForm initialization", TAG));

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

        private void CreateDatabaseUserForm_Load(object sender, EventArgs e)
        {
            try
            {
                progressBar_connect.Visible = false;
                chkIntegratedSecurity.Checked = true;
                chkshowpassword.Checked = false;
                chkRemeberUserName.Checked = true;
                chkRememberPassword.Checked = true;
                chkRememberServerName.Checked = true;

                populate_auto_complete_values();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished CreateDatabaseUserForm load", TAG));

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void populate_auto_complete_values()
        {
            try
            {
                string filePath = Utils.AUTO_COMPLETE_DATABASES_FILENAME;

                // Guard 1: Verify file existence
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    return;
                }

                // Retrieve saved login objects safely
                List<SB_Server_Login> auto_complete_from_xml = GetDataFromSB_LoginXML(filePath);

                // Guard 2: Verify collection contains items
                if (auto_complete_from_xml == null || auto_complete_from_xml.Count == 0)
                {
                    return;
                }

                // Use HashSet with OrdinalIgnoreCase to automatically reject duplicate entries
                HashSet<string> uniqueServers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                HashSet<string> uniqueDatabases = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                // Populate unique values while skipping empty or duplicate strings
                foreach (var item in auto_complete_from_xml)
                {
                    if (item != null)
                    {
                        if (!string.IsNullOrWhiteSpace(item.serverName))
                        {
                            uniqueServers.Add(item.serverName.Trim());
                        }

                        if (!string.IsNullOrWhiteSpace(item.databaseName))
                        {
                            uniqueDatabases.Add(item.databaseName.Trim());
                        }
                    }
                }

                // Retrieve last valid record safely for default selection
                SB_Server_Login last_record = auto_complete_from_xml.LastOrDefault(x => x != null);

                // Convert HashSets to List for UI binding
                List<string> serverList = uniqueServers.ToList();
                List<string> databaseList = uniqueDatabases.ToList();

                // Bind Servers without duplicates
                if (serverList.Count > 0)
                {
                    cbo_servers.DataSource = serverList;

                    if (last_record != null && !string.IsNullOrWhiteSpace(last_record.serverName))
                    {
                        string match = serverList.FirstOrDefault(s => string.Equals(s, last_record.serverName.Trim(), StringComparison.OrdinalIgnoreCase));
                        if (match != null)
                        {
                            cbo_servers.SelectedItem = match;
                        }
                    }
                }

                // Bind Databases without duplicates
                if (databaseList.Count > 0)
                {
                    cbo_database.DataSource = databaseList;

                    if (last_record != null && !string.IsNullOrWhiteSpace(last_record.databaseName))
                    {
                        string match = databaseList.FirstOrDefault(d => string.Equals(d, last_record.databaseName.Trim(), StringComparison.OrdinalIgnoreCase));
                        if (match != null)
                        {
                            cbo_database.SelectedItem = match;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (this._notificationmessageEventname != null)
                {
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                }
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

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsUserValid())
            {
                try
                {
                    string serverName = this.cbo_servers.Text;
                    string dbName = this.cbo_database.Text;
                    string new_db_username = txt_new_database_user.Text.Trim();
                    string new_db_password = txt_new_database_password.Text.Trim();

                    // 1. Initialize Server Connection
                    Server server = string.IsNullOrEmpty(serverName) ? new Server() : new Server(serverName);

                    // 2. Create Server Login (if it doesn't already exist)
                    if (!server.Logins.Contains(new_db_username))
                    {
                        Login newLogin = new Login(server, new_db_username)
                        {
                            LoginType = LoginType.SqlLogin
                        };
                        newLogin.Create(new_db_password); // Set login password
                        newLogin.Enable();
                    }

                    // 3. Create Database User under the targeted database
                    if (server.Databases.Contains(dbName))
                    {
                        Database db = server.Databases[dbName];

                        if (!db.Users.Contains(new_db_username))
                        {
                            User dbUser = new User(db, new_db_username)
                            {
                                Login = new_db_username
                            };
                            dbUser.Create();

                            // 4. Assign Database Role (e.g., db_owner)
                            dbUser.AddToRole("db_owner");
                        }

                        MessageBox.Show(string.Format("Database user [ '{0}' ] created successfully!", new_db_username), Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("Database [ '{0}' ] does not exist.", dbName), Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.ShowError(ex);
                }
            }
        }

        private bool IsUserValid()
        {
            errorProvider.Clear();

            bool no_error = true;
            if (cbo_servers.SelectedIndex == -1)
            {
                errorProvider.SetError(cbo_servers, "Select server!");
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Select server!", TAG));
                no_error = false;
            }
            if (cbo_database.SelectedIndex == -1)
            {
                errorProvider.SetError(cbo_database, "Select database!");
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Select database!", TAG));
                no_error = false;
            }
            if (string.IsNullOrEmpty(txt_new_database_user.Text))
            {
                errorProvider.SetError(txt_new_database_user, "User Name cannot be null!");
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("User Name cannot be null!", TAG));
                no_error = false;
            }
            if (string.IsNullOrEmpty(txt_new_database_password.Text))
            {
                errorProvider.SetError(txt_new_database_password, "Password cannot be null!");
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Password cannot be null!", TAG));
                no_error = false;
            }
            return no_error;
        }

        private void chkshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkshowpassword.Checked)
            {
                txt_password.PasswordChar = '\0';
                txt_new_database_password.PasswordChar = '\0';
            }
            else
            {
                txt_password.PasswordChar = '*';
                txt_new_database_password.PasswordChar = '*';
            }
        }

        private void btnGetServerList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                errorProvider.Clear();

                btnGetServerList.Enabled = false;
                btnGetServerList.Text = "Enumerating Servers...";
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Enumerating Servers...", TAG));

                progressBar_connect.Visible = true;

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
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void btnrefreshdatabases_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                errorProvider.Clear();
                if (string.IsNullOrEmpty(cbo_servers.Text))
                {
                    errorProvider.SetError(cbo_servers, "select server!");
                    return;
                }

                btnrefreshdatabases.Enabled = false;
                btnrefreshdatabases.Text = "Enumerating Databases...";
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Enumerating Databases...", TAG));

                current_action = "database";

                progressBar_connect.Visible = true;

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
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //this is the method that the backgroundworker will perform on in the background thread without blocking the UI.
                /* One thing to note! A try catch is not necessary as any exceptions will terminate the
                backgroundWorker and report the error to the "RunWorkerCompleted" event */

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

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                string serverName = null;
                                string instanceName = null;

                                // .NET Framework 4 compatible null checks
                                if (dr["ServerName"] != DBNull.Value)
                                {
                                    serverName = Convert.ToString(dr["ServerName"]);
                                }

                                if (dr["InstanceName"] != DBNull.Value)
                                {
                                    instanceName = Convert.ToString(dr["InstanceName"]);
                                }

                                if (!string.IsNullOrEmpty(serverName))
                                {
                                    string fullName;

                                    if (string.IsNullOrEmpty(instanceName))
                                    {
                                        fullName = serverName;
                                    }
                                    else
                                    {
                                        fullName = serverName + "\\" + instanceName;
                                    }

                                    if (!srvNames.Contains(fullName))
                                    {
                                        srvNames.Add(fullName);
                                    }
                                }
                            }
                        }

                        // Fallback default local names if enumeration yields no results
                        if (srvNames.Count == 0)
                        {
                            string compName = Utils.get_computer_name();

                            srvNames.Add(compName);
                            srvNames.Add(compName + "\\SQLEXPRESS");
                            srvNames.Add(".");
                            srvNames.Add(".\\SQLEXPRESS");
                            srvNames.Add("(local)");
                        }

                        cbo_servers.Invoke(new Action(() =>
                        {
                            //cbo_servers.DataSource = null;
                            cbo_servers.DataSource = srvNames;

                            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Found [ " + srvNames.Count + " ] server(s).", TAG));
                        }));
                    }
                    catch (Exception ex)
                    {
                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                        Log.WriteToErrorLogFile_and_EventViewer(ex);
                    }
                }
                else if (current_action.Equals("database"))
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
                                serverName = cbo_servers.Text.Trim();
                                userName = txt_user_name.Text.Trim();
                                password = txt_password.Text.Trim();
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

                                    cbo_database.Invoke(new Action(() =>
                                    {
                                        cbo_database.DataSource = _databases;
                                    }));

                                    //Invoke(new MethodInvoker(delegate()
                                    //{
                                    //    DisplayServer(server, LoginUserName, LoginPassword);
                                    //}));

                                    //Invoke(new MethodInvoker(delegate()
                                    //{
                                    //    save_auto_complete_login();
                                    //}));

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
                _task_end_time = DateTime.Now;

                var _time_lapsed = _task_end_time.Subtract(_task_start_time);

                string msg = "Task Complete!" + Environment.NewLine + "Task took [ " + _time_lapsed + " ]";

                Invoke(new Action(() =>
                {
                    btnGetServerList.Enabled = true;
                    btnGetServerList.Text = "Get Server List";
                    btnrefreshdatabases.Enabled = true;
                    btnrefreshdatabases.Text = "Get Database List";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));
                }));

                progressBar_connect.Visible = false;

                bgWorker.DoWork -= bgWorker_DoWork;
                bgWorker.RunWorkerCompleted -= bgWorker_WorkComplete;
                bgWorker.ProgressChanged -= bgWorker_ProgressChanged;
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private bool IsServerLoginValid()
        {
            errorProvider.Clear();

            bool noerror = true;
            if (string.IsNullOrEmpty(cbo_servers.Text))
            {
                errorProvider.SetError(cbo_servers, "Server Name cannot be null!");
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Server Name cannot be null!", TAG));
                noerror = false;
            }
            if (!chkIntegratedSecurity.Checked)
            {
                if (string.IsNullOrEmpty(txt_user_name.Text))
                {
                    errorProvider.SetError(txt_user_name, "User Name cannot be null!");
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("User Name cannot be null!", TAG));
                    noerror = false;
                }
                if (string.IsNullOrEmpty(txt_password.Text))
                {
                    errorProvider.SetError(txt_password, "Password cannot be null!");
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Password cannot be null!", TAG));
                    noerror = false;
                }
            }
            return noerror;
        }

        private void cbo_servers_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void save_auto_complete_login()
        {
            try
            {
                string auto_complete_server_login_filename = Utils.AUTO_COMPLETE_SERVER_LOGIN_FILENAME;

                string system = Utils.APP_NAME;
                string serverName = cbo_servers.Text.Trim();
                string databaseName = "";
                string userName = txt_user_name.Text.Trim();
                string password = txt_password.Text.Trim();
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

        public void DisplayServer(Server server, string username, string pwd)
        {
            try
            {
                //if (server.ConnectionContext.IsOpen)
                //{
                //    tabControlDBPanel.TabPages.Remove(tabPageServerSettings);
                //    tabControlDBPanel.TabPages.Remove(tabPageServerConnection);
                //    tabControlDBPanel.TabPages.Remove(tabPageDatabaseSettings);

                //    if (!tabControlDBPanel.TabPages.Contains(tabPageServerSettings))
                //    {
                //        tabControlDBPanel.TabPages.Add(tabPageServerSettings);
                //        tabControlDBPanel.TabPages.Remove(tabPageServerConnection);
                //        tabControlDBPanel.TabPages.Remove(tabPageDatabaseSettings);
                //        lblSrvSttServerName.Text = string.Empty;

                //        lblSrvSttServerName.Text = server.Name;

                //        string result = "";
                //        List<char> listOfChars = pwd.ToList<char>();
                //        int numberOfChars = listOfChars.Count<char>();

                //        char newChar = ' ';
                //        for (var i = 0; i < numberOfChars; i++)
                //        {
                //            newChar = '*';
                //            result += newChar;
                //        }
                //    }

                //    listViewDatabaseList.Items.Clear();

                //    foreach (var s in databases)
                //    {
                //        listViewDatabaseList.Items.Add(new ListViewItem(new string[] 
                //        { 
                //            s.System.Database.ToString(),
                //            s.Size.ToString() + "   MB",   
                //            s.System.Version.ToString()                                
                //        }));
                //    }

                //    foreach (ListViewItem item in listViewDatabaseList.Items)
                //    {
                //        item.ImageIndex = 0;
                //    }
                //    lblDatabaseNo.Text = "Databases  (" + databases.Count.ToString() + ")";
                //}
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
                txt_user_name.Enabled = false;
                txt_password.Enabled = false;
            }
            else
            {
                txt_user_name.Enabled = true;
                txt_password.Enabled = true;
            }
        }



    }

    public class DatabaseUserEventArgs : System.EventArgs
    {
        // add local member variables to hold text  
        private string _username;
        private string _password;

        // class constructor
        public DatabaseUserEventArgs(string username, string password)
        {
            this._username = username;
            this._password = password;
        }

        // Properties - Viewable by each listener 
        public string _UserName
        {
            get
            {
                return _username;
            }
        }
        public string _Password
        {
            get
            {
                return _password;
            }
        }
    }
}
