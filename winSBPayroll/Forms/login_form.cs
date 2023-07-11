using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using CommonLib;
using DAL;
using System.Threading;
using System.Security.Cryptography;
using System.Data;

namespace winSBPayroll.Forms
{
    public partial class login_form : Form
    {
        private bool m_bLayoutCalled = false;
        private DateTime m_dt;
        public UserModel LoggedInUser;
        static LoginService loginservice;
        MainForm mainForm;
        private List<SBSystem> _SBsystems;
        public String errorMessage;
        SBSystem _defaultsys;
        private List<string> _SBsystemsstr;
        const string providername = "System.Data.EntityClient";
        const string provider = "System.Data.SqlClient";
        private int loggedinTimeCounter = 0;
        DateTime startDate = DateTime.Now;

        public login_form(SBSystem defsys)
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            this.txtusername.Focus();

            _SBsystems = GetDataFromXML();
            cbosystem.DataSource = _SBsystems;
            cbosystem.DisplayMember = "Name";
            cbosystem.ValueMember = "Name";
            cbosystem.SelectedValue = defsys.Name;

            if (defsys != null)
            {
                _defaultsys = defsys;
            }
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Utils.LogEventViewer(ex);
            Log.WriteToErrorLogFile(ex);
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Utils.LogEventViewer(ex);
            Log.WriteToErrorLogFile(ex);
        }

        public List<string> SBsystemsstr
        {
            get
            {
                return _SBsystemsstr;
            }
            set
            {
                _SBsystemsstr = value;
            }
        }
        private List<SBSystem> SBSystems
        {
            get
            {
                return _SBsystems;
            }
            set
            {
                _SBsystems = value;
            }
        }
        public string ConnectionString
        {
            get
            {
                return SQLHelper.EntityConnection(this.SelectedSystem,
                    this.txtServerLoginUserName.Text.Trim(),
                    this.txtServerLoginPassword.Text.Trim(), this.chkIntegratedSecurity.Checked);
            }
        }
        /// <summary>
        /// Returns the username entered within the UI.
        /// </summary>
        public string UserName
        {
            get
            {
                return this.txtusername.Text.Trim();
            }
        }
        /// <summary>
        /// Returns the password entered within the UI.
        /// </summary>
        public string Password
        {
            get
            {
                return this.txtpassword.Text.Trim();
            }
        }
        public bool UseIntegratedSecurity
        {
            get
            {
                return chkIntegratedSecurity.Checked;
            }
        }
        public SBSystem SelectedSystem
        {
            get
            {
                return (SBSystem)cbosystem.SelectedItem;
            }
        }
        private void login_form_Load(object sender, EventArgs e)
        {

            try
            {
                groupBoxServerLogin.Visible = false;

                string AssemblyProduct = app_assembly_info.AssemblyProduct;
                string AssemblyVersion = app_assembly_info.AssemblyVersion;
                string AssemblyCopyright = app_assembly_info.AssemblyCopyright;
                string AssemblyCompany = app_assembly_info.AssemblyCompany;
                this.Text = AssemblyProduct;
                this.lblcopyright.Text = "Copyright ©  " + DateTime.Now.Year.ToString() + "  " + AssemblyCompany + " - All Rights Reserved";

                lblLoggedInTime.Text = string.Empty;

                loggedInTimer.Tick += new EventHandler(loggedInTimer_Tick);
                loggedInTimer.Interval = 1000; // 1 second
                loggedInTimer.Start();

                AutoCompleteStringCollection acsccls = new AutoCompleteStringCollection();
                acsccls.AddRange(this.AutoComplete_Users());
                txtusername.AutoCompleteCustomSource = acsccls;
                txtusername.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtusername.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                //txtusername.Text = "sys";
                //txtpassword.Text = "sys";

                groupBox1.Text = "build version - " + AssemblyVersion;

                populate_auto_complete_values();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void loggedInTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                loggedinTimeCounter++;
                DateTime nowDate = DateTime.Now;
                TimeSpan t = nowDate - startDate;
                lblLoggedInTime.Text = string.Format("{0} : {1} : {2} : {3}", t.Days, t.Hours, t.Minutes, t.Seconds);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnClose_Clicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Clicked(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (IsLoginValid())
            {
                try
                {
                    //Authenticate(this.ConnectionString, this.UserName, this.Password);
                    validate_login(this.UserName, this.Password);
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
                finally
                {
                    loggedInTimer.Stop();
                }
            }
        }

        private void Authenticate(string conn, string user, string pass)
        {
            try
            {
                loginservice = new LoginService();
                loginservice.Authenticate(conn,
                         user, pass,
                        this.SuccessfulLogin,
                        this.LoginFailed);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void SuccessfulLogin(UserModel user)
        {
            try
            {
                LoggedInUser = user;

                if (mainForm == null)
                {
                    mainForm = new MainForm(this, this.ConnectionString, this.SelectedSystem);
                }

                mainForm.LogIn();
                mainForm.Show();

                this.Hide();
                SaveAutoCompleteUsers();
                save_auto_complete_login();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void LoginFailed(string ErrorCode, string Errormsg)
        {
            try
            {
                MessageBox.Show(
                        "Application Error \n " + Errormsg,
                        "Login Error",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void validate_login(string username, string password)
        {
            try
            {
                Repository rep = new Repository();
                bool connected = rep.Connect(this.ConnectionString);
                if (connected)
                {
                    var user_that_exists = rep.GetUserbyUserName(username);
                    if (user_that_exists != null)
                    {
                        if (user_that_exists.Locked)
                        {
                            Utils.ShowError(new Exception("User is locked. \nplease contact the administrator."));
                            return;
                        }

                        string salt = user_that_exists.password_salt;
                        string salted_password = salt + password;
                        string password_salt_hash = Utils.get_SHA512_hash(salted_password);

                        string hash_from_db = user_that_exists.password_hash;

                        bool is_hash_equal = hash_from_db.Equals(password_salt_hash);

                        if (is_hash_equal)
                        {
                            var usermodel = new UserModel
                                             {
                                                 UserName = user_that_exists.UserName,
                                                 Password = user_that_exists.Password,
                                                 RoleId = user_that_exists.RoleId,
                                                 Locked = user_that_exists.Locked,
                                                 UserId = user_that_exists.Id
                                             };

                            LoggedInUser = usermodel;

                            if (mainForm == null)
                            {
                                mainForm = new MainForm(this, this.ConnectionString, this.SelectedSystem);
                            }

                            mainForm.LogIn();
                            mainForm.Show();

                            this.Hide();
                            SaveAutoCompleteUsers();
                            save_auto_complete_login();
                        }
                        else
                        {
                            Utils.ShowError(new Exception("incorrect password. \nif you forgot your password please contact the administrator."));
                        }
                    }
                    else
                    {
                        Utils.ShowError(new Exception("user [ " + username + " ] does not exist."));
                    }
                }
                else
                {
                    Utils.ShowError(new Exception("Could not create a connection to the database check your connection parameters."));
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
            }
        }

        private bool IsLoginValid()
        {
            bool noerror = true;
            if (string.IsNullOrEmpty(txtusername.Text))
            {
                errorProvider1.SetError(txtusername, "User Name cannot be null!");
                noerror = false;
            }
            if (string.IsNullOrEmpty(txtpassword.Text))
            {
                errorProvider1.SetError(txtpassword, "Password cannot be null!");
                noerror = false;
            }
            if (!chkIntegratedSecurity.Checked)
            {
                if (string.IsNullOrEmpty(txtServerLoginUserName.Text))
                {
                    errorProvider1.SetError(txtServerLoginUserName, "server User Name cannot be null!");
                    noerror = false;
                }
                if (string.IsNullOrEmpty(txtServerLoginPassword.Text))
                {
                    errorProvider1.SetError(txtServerLoginPassword, "server Password cannot be null!");
                    noerror = false;
                }
            }
            return noerror;
        }
        private void chkIntegratedSecurity_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntegratedSecurity.Checked)
            {
                groupBoxServerLogin.Visible = false;
            }
            else
            {
                groupBoxServerLogin.Visible = true;

            }
        }
        private void timer1_Tick(object sender, System.EventArgs e)
        {
            //TimeSpan ts = DateTime.Now.Subtract(m_dt);
            //if( ts.TotalSeconds > 2 )
            //    this.Close();
            //if (m_bLayoutCalled == false)
            //{
            //    m_bLayoutCalled = true;
            //    m_dt = DateTime.Now;
            //    //if (SplashScreen.SplashForm != null)
            //    //    SetOwnerCrossThread( this);
            //    this.Activate();
            //    Splash.SplashScreen.CloseForm();
            //    timer1.Start();
            //}
        }
        public void LoadXMLFromEmbeddedResource(string filename, XmlDocument doc)
        {

            using (Stream stream = this.GetType().Assembly.
                        GetManifestResourceStream(filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    doc.LoadXml(sr.ReadToEnd());
                }
            }
        }
        private Stream GetResourceStream(string resourceFile)
        {
            Stream stream = Utils.GetEmbeddedResourceStream(resourceFile);

            if (stream == null)
                throw new ApplicationException("Missing resource file: " + resourceFile);

            return stream;
        }
        public string GetResourceTextFile(string filename)
        {
            string result = string.Empty;

            using (Stream stream = this.GetType().Assembly.
                       GetManifestResourceStream(filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }
        private List<string> GetFromConfig()
        {
            foreach (ConnectionStringSettings connection in ConfigurationManager.ConnectionStrings)
            {
                SBsystemsstr.Add(connection.Name);
            }
            return SBsystemsstr;

        }
        private List<SBSystem> GetDataFromXML()
        {

            return SQLHelper.GetDataFromXML();

        }
        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            //btnOK.Enabled = !string.IsNullOrEmpty(txtusername.Text) && !string.IsNullOrEmpty(txtpassword.Text) && cbosystem.SelectedIndex != -1;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            //btnOK.Enabled = !string.IsNullOrEmpty(txtusername.Text) && !string.IsNullOrEmpty(txtpassword.Text) && cbosystem.SelectedIndex != -1;
        }

        private void cbSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnOK.Enabled = !string.IsNullOrEmpty(txtusername.Text) && !string.IsNullOrEmpty(txtpassword.Text) && cbosystem.SelectedIndex != -1;
        }

        private bool SaveAutoCompleteUsers()
        {
            try
            {
                string auto_complete_users_filename = Utils.AUTO_COMPLETE_USERS_FILENAME;

                string userName = txtusername.Text.Trim();
                string password = txtpassword.Text.Trim();
                password = Utils.encrypt_string(password);

                if (File.Exists(auto_complete_users_filename))
                {
                    List<SBSystem_Exp> successfully_logged_users = SQLHelper.GetDataFromSBSystem_ExpXML(auto_complete_users_filename);

                    var exists = successfully_logged_users.Where(i => i.Name.Equals(userName)).FirstOrDefault();

                    if (exists == null)
                    {
                        XDocument doc = XDocument.Load(auto_complete_users_filename);
                        doc.Element("Systems").Add(
                            new XElement("System",
                            new XAttribute("Name", userName),
                            new XAttribute("Application", password)
                            ));
                        doc.Save(auto_complete_users_filename);
                    }
                }
                if (!File.Exists(auto_complete_users_filename))
                {
                    List<SBSystem_Exp> systems = new List<SBSystem_Exp>() { 
                        new SBSystem_Exp(
                            userName,
                            password
                        )};

                    var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                                           new XAttribute("Name", x.Name),
                                           new XAttribute("Application", x.Application)
                                           )));
                    xml.Save(auto_complete_users_filename);
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }

        private string[] AutoComplete_Users()
        {
            try
            {
                string auto_complete_users_filename = Utils.AUTO_COMPLETE_USERS_FILENAME;

                List<string> logged_usernames = new List<string>();

                if (File.Exists(auto_complete_users_filename))
                {
                    List<SBSystem_Exp> successfully_logged_users = SQLHelper.GetDataFromSBSystem_ExpXML(auto_complete_users_filename);

                    foreach (var item in successfully_logged_users)
                    {
                        if (!logged_usernames.Contains(item.Name))
                        {
                            logged_usernames.Add(item.Name);
                        }
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

        private void save_auto_complete_login()
        {
            try
            {
                string auto_complete_login_filename = Utils.AUTO_COMPLETE_LOGIN_FILENAME;

                string system = cbosystem.SelectedValue.ToString();
                string serverName = SelectedSystem.Server;
                string databaseName = SelectedSystem.Database;
                string userName = txtusername.Text.Trim();
                string password = txtpassword.Text.Trim();
                password = Utils.encrypt_string(password);
                bool IntegratedSecurity = chkIntegratedSecurity.Checked;
                bool remember = chkremember.Checked;

                if (File.Exists(auto_complete_login_filename))
                {
                    List<SB_Login> successfully_logged_users = GetDataFromSB_LoginXML(auto_complete_login_filename);

                    //var exists = successfully_logged_users.Where(i => i.userName.Equals(userName) && i.databaseName.Equals(databaseName) && i.serverName.Equals(serverName) && i.system.Equals(system)).FirstOrDefault();

                    DataSet ds = new DataSet();

                    ds.ReadXml(auto_complete_login_filename);

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
                    ds.WriteXml(auto_complete_login_filename);

                    XDocument doc = XDocument.Load(auto_complete_login_filename);
                    doc.Element("Systems").Add(
                        new XElement("System",
                        new XAttribute("system", system),
                        new XAttribute("serverName", serverName),
                        new XAttribute("databaseName", databaseName),
                        new XAttribute("userName", userName),
                        new XAttribute("password", password),
                        new XAttribute("IntegratedSecurity", IntegratedSecurity.ToString()),
                        new XAttribute("remember", remember.ToString())
                        ));

                    doc.Save(auto_complete_login_filename);
                }

                if (!File.Exists(auto_complete_login_filename))
                {
                    List<SB_Login> systems = new List<SB_Login>() { 
                        new SB_Login(system, 
                           serverName,
                           databaseName,
                           userName,
                           password,
                           IntegratedSecurity.ToString(),
                           remember.ToString()                            
                            )};

                    var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                            new XAttribute("system", x.system),
                            new XAttribute("serverName", x.serverName),
                            new XAttribute("databaseName", x.databaseName),
                            new XAttribute("userName", x.userName),
                            new XAttribute("password", x.password),
                            new XAttribute("IntegratedSecurity", x.IntegratedSecurity),
                            new XAttribute("remember", x.remember)
                                           )));

                    xml.Save(auto_complete_login_filename);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }
        private List<SB_Login> GetDataFromSB_LoginXML(string filename)
        {
            using (XmlReader xmlRdr = new XmlTextReader(filename))
            {
                return new List<SB_Login>(
                   (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                    select new SB_Login(
                       (string)sysElem.Attribute("system"),
                       (string)sysElem.Attribute("serverName"),
                       (string)sysElem.Attribute("databaseName"),
                       (string)sysElem.Attribute("userName"),
                       (string)sysElem.Attribute("password"),
                       (string)sysElem.Attribute("IntegratedSecurity"),
                       (string)sysElem.Attribute("remember")
                                )).ToList());
            }
        }
        private void populate_auto_complete_values()
        {
            try
            {
                List<SB_Login> auto_complete_from_xml = GetDataFromSB_LoginXML(Utils.AUTO_COMPLETE_LOGIN_FILENAME);

                List<string> saved_servers = new List<string>();
                List<string> saved_databases = new List<string>();

                SB_Login last_record = auto_complete_from_xml.Last();

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

                cbosystem.SelectedValue = last_record.system;
                //cboserver.DataSource = saved_servers;
                //cbodatabase.DataSource = saved_databases;
                txtusername.Text = last_record.userName;

                if (bool.Parse(last_record.remember))
                {
                    string password = last_record.password;
                    password = Utils.decrypt_string(password);
                    txtpassword.Text = password;
                }

                Invoke(new MethodInvoker(
                   delegate()
                   {
                       chkIntegratedSecurity.Checked = bool.Parse(last_record.IntegratedSecurity); 
                   }));
                 
                chkremember.Checked = bool.Parse(last_record.remember);

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }



    }
}
