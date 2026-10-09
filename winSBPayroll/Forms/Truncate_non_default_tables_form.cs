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
    public partial class Truncate_non_default_tables_form : Form
    {
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
        List<ServerDatabase> databases;
        List<string> server_names;
        const string SBApplication = "SBPayroll";
        const string Metadata = "SBPayrollDBEntities";
        const string DatabaseVersionExtPropertyKey = SBApplication + "Version";
        //const string DatabaseVersionExtPropertyValue = "1.0.0.0";
        string DatabaseVersionExtPropertyValue = Application.ProductVersion;
        string TAG;
        List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname_from_parent;

        public Truncate_non_default_tables_form(EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
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

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished Truncate_non_default_tables_form initialization", TAG));

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

                txtlogs.Lines = _logflippedlines;
                txtlogs.ScrollToCaret();

                Log.WriteToErrorLogFile_and_EventViewer(new Exception(_logtext));

                //}));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Truncate_non_default_tables_form_Load(object sender, EventArgs e)
        {
            try
            {
                progressBar_connect.Visible = false;
                chkIntegratedSecurity.Checked = true;
                chkshowpassword.Checked = false;
                chkRemeberUserName.Checked = true;
                chkRememberPassword.Checked = true;
                chkRememberServerName.Checked = true;
                txt_password.Enabled = false;
                txt_user_name.Enabled = false;

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
            if (!chkIntegratedSecurity.Checked)
            {
                if (string.IsNullOrEmpty(txt_user_name.Text))
                {
                    errorProvider.SetError(txt_user_name, "User Name cannot be null!");
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("User Name cannot be null!", TAG));
                    no_error = false;
                }
                if (string.IsNullOrEmpty(txt_password.Text))
                {
                    errorProvider.SetError(txt_password, "Password cannot be null!");
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Password cannot be null!", TAG));
                    no_error = false;
                }
            }
            if (cbo_database.SelectedIndex == -1)
            {
                errorProvider.SetError(cbo_database, "Select database!");
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Select database!", TAG));
                no_error = false;
            }
            return no_error;
        }

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        void truncate_table(string table_name)
        {
            try
            {
                string _server = string.Empty;
                string _database = string.Empty;

                string serverName = string.Empty;
                string dbName = string.Empty;

                Invoke(new Action(() =>
                {
                    _server = cbo_servers.Text.Trim();
                    _database = cbo_database.Text.Trim();

                    serverName = this.cbo_servers.Text;
                    dbName = this.cbo_database.Text;
                }));

                // 1. Initialize Server Connection
                Server server = string.IsNullOrEmpty(serverName) ? new Server() : new Server(serverName);

                if (server == null)
                {

                    Invoke(new Action(() =>
                    {
                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Server cannot be null!", TAG));
                    }));

                }
                else
                {
                    if (server.Databases.Contains(dbName))
                    {
                        Database db = server.Databases[dbName];

                        if (db.Tables.Contains(table_name))
                        {
                            Table tb = db.Tables[table_name];

                            Invoke(new Action(() =>
                            {
                                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Truncating table [ " + table_name + " ]...", TAG));
                            }));

                            tb.TruncateData();

                            Invoke(new Action(() =>
                            {
                                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Successfully truncated table [ " + table_name + " ]...", TAG));
                            }));
                        }
                    }
                    else
                    {

                        Invoke(new Action(() =>
                        {
                            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Database [ " + dbName + " ]  does not exist!", TAG));
                        }));
                    }
                }
            }
            catch (Exception ex)
            {

                Invoke(new Action(() =>
                {
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                }));

            }
        }

        private void btn_truncate_database_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsUserValid())
            {
                try
                {
                    errorProvider.Clear();
                    if (string.IsNullOrEmpty(cbo_servers.Text))
                    {
                        errorProvider.SetError(cbo_servers, "select server!");
                        return;
                    }

                    StringBuilder sb = new StringBuilder();

                    string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                    string data_path = Path.Combine(base_directory, "Data");
                    string file_name = "working_tables.txt";

                    var _data_file = Path.Combine(data_path, file_name);

                    // Read all lines into a string array
                    string[] lines = File.ReadAllLines(_data_file);

                    // Loop through the array using a traditional for loop
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Trim().Length > 0)
                        {
                            sb.AppendLine(lines[i]);
                        }
                    }

                    string msg = "You are about to delete data in tables.\n Are you sure you want to proceed..." + Environment.NewLine + sb.ToString();

                    if (MessageBox.Show(msg, Utils.APP_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        btn_truncate_database.Enabled = false;
                        btn_truncate_database.Text = "Truncating tables...";
                        this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Truncating tables...", TAG));

                        progressBar_connect.Visible = true;

                        current_action = "truncate";

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
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.ShowError(ex);
                }
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

                progressBar_connect.Visible = true;

                current_action = "database";

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

                                    cbo_database.Invoke(new Action(() =>
                                    {
                                        cbo_database.DataSource = _databases;
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
                else if (current_action.Equals("truncate"))
                {
                    string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                    string data_path = Path.Combine(base_directory, "Data");
                    string file_name = "working_tables.txt";

                    var _data_file = Path.Combine(data_path, file_name);

                    // Read all lines into a string array
                    string[] lines = File.ReadAllLines(_data_file);

                    // Loop through the array using a traditional for loop
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i].Trim().Length > 0)
                        {
                            Invoke(new Action(() =>
                            {
                                Console.WriteLine(string.Format("Line {0}: {1}", i + 1, lines[i]));

                                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Truncating database [ " + lines[i] + " ]", TAG));
                            }));

                            truncate_table(lines[i]);
                        }
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

                    btn_truncate_database.Enabled = true;
                    btn_truncate_database.Text = "Truncate Database";

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

        private void chkshowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkshowpassword.Checked)
            {
                txt_password.PasswordChar = '\0';
            }
            else
            {
                txt_password.PasswordChar = '*';
            }
        }







    }
}
