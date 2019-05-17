using System; 
using System.Collections;
using System.Collections.Generic; 
using System.ComponentModel;
using System.Configuration; 
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO; 
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection; 
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms; 
using System.Xml; 
using System.Xml.Linq;
using BLL.DataEntry;
using CommonLib; 
using DAL; 
using Microsoft.Win32;
using Splash;

namespace winSBPayroll.Forms
{
    public partial class LoginForm : Form
    {
        #region "Private Fields"
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
        #endregion "Private Fields"

        #region "Constructor"
        public LoginForm(SBSystem defsys)
        {
            InitializeComponent();

            this.txtUserName.Focus();

            _SBsystems = GetDataFromXML();
            cbSystems.DataSource = _SBsystems;
            cbSystems.DisplayMember = "Name";
            cbSystems.ValueMember = "Name";
            cbSystems.SelectedValue = defsys.Name;

            if (defsys != null)
            {
                _defaultsys = defsys;
            }

        }
        #endregion "Constructor"

        #region "public Fields"
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
            get { return this.txtUserName.Text.Trim(); }
        }
        /// <summary>
        /// Returns the password entered within the UI.
        /// </summary>
        public string Password
        {
            get { return this.txtPassword.Text.Trim(); }
        }
        public bool UseIntegratedSecurity
        {
            get { return chkIntegratedSecurity.Checked; }
        }
        public SBSystem SelectedSystem
        {
            get
            {
                return (SBSystem)cbSystems.SelectedItem;
            }
        }
        #endregion "public Fields"

        #region Authentication
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
                SaveAutoCompleteUsers(user.UserName);
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
        #endregion

        #region "Private Methods"
        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                AboutBox ab = new AboutBox();
                groupBoxServerLogin.Visible = false;
                string AssemblyProduct = ab.AssemblyProduct;
                string AssemblyVersion = ab.AssemblyVersion;
                string AssemblyCopyright = ab.AssemblyCopyright;
                string AssemblyCompany = ab.AssemblyCompany;
                this.Text = AssemblyProduct;
                this.lblcopyright.Text = "Copyright ©  " + DateTime.Now.Year.ToString() + "  " + AssemblyCompany + " - All Rights Reserved";

                lblLoggedInTime.Text = string.Empty;

                loggedInTimer.Tick += new EventHandler(loggedInTimer_Tick);
                loggedInTimer.Interval = 1000; // 1 second
                loggedInTimer.Start();

                AutoCompleteStringCollection acsccls = new AutoCompleteStringCollection();
                acsccls.AddRange(this.AutoComplete_Users());
                txtUserName.AutoCompleteCustomSource = acsccls;
                txtUserName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtUserName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                txtUserName.Text = "sys";
                txtPassword.Text = "sys";
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_Users()
        {
            try
            {
                string auto_complete_users_filename = "Resources/auto_complete_users.xml";
                List<string> logged_usernames = new List<string>();
                if (File.Exists(auto_complete_users_filename))
                {
                    List<SBSystem_Exp> successfully_logged_users = SQLHelper.GetDataFromSBSystem_ExpXML(auto_complete_users_filename);
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
                    Authenticate(this.ConnectionString, this.UserName, this.Password);
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
        private bool IsLoginValid()
        {
            bool noerror = true;
            if (chkIntegratedSecurity.Checked == true && string.IsNullOrEmpty(txtUserName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtUserName, "User Name cannot be null!");
                return false;
            }
            if (chkIntegratedSecurity.Checked == true && string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPassword, "Password cannot be null!");
                return false;
            }
            if (chkIntegratedSecurity.Checked == false && string.IsNullOrEmpty(txtServerLoginUserName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtServerLoginUserName, "User Name cannot be null!");
                return false;
            }
            if (chkIntegratedSecurity.Checked == false && string.IsNullOrEmpty(txtServerLoginPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtServerLoginPassword, "Password cannot be null!");
                return false;
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
        private void txtUserId_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled =
        !string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && cbSystems.SelectedIndex != -1;
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled =
       !string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && cbSystems.SelectedIndex != -1;
        }
        private void cbSystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOK.Enabled =
      !string.IsNullOrEmpty(txtUserName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && cbSystems.SelectedIndex != -1;
        }
        #endregion "Private Methods"

        #region  "Helpers"
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
        private bool SaveAutoCompleteUsers(string username)
        {
            try
            {
                string auto_complete_users_filename = "Resources/auto_complete_users.xml";
                if (File.Exists(auto_complete_users_filename))
                {

                    List<string> logged_usernames = new List<string>();
                    List<SBSystem_Exp> successfully_logged_users = SQLHelper.GetDataFromSBSystem_ExpXML(auto_complete_users_filename);
                    foreach (var item in successfully_logged_users)
                    {
                        logged_usernames.Add(item.Name);
                    }
                    if (!logged_usernames.Contains(username))
                    {
                        XDocument doc = XDocument.Load(auto_complete_users_filename);
                        doc.Element("Systems").Add(
                            new XElement("System",
                            new XAttribute("Name", username),
                            new XAttribute("Application", DateTime.Now.ToString())));
                        doc.Save(auto_complete_users_filename);
                    }
                }
                if (!File.Exists(auto_complete_users_filename))
                {

                    List<SBSystem_Exp> systems = new List<SBSystem_Exp>() { new SBSystem_Exp("sys", DateTime.Now.ToString()) };

                    var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                                           new XAttribute("Name", x.Name),
                                           new XAttribute("Application", x.Application))));
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
        #endregion  "Helpers"

        #region Main & Splash Screen
        const string SystemsConfigFile = "Security/Systems.xml";
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                SplashScreen.ShowSplashScreen();
                Application.DoEvents();
                SplashScreen.SetStatus("Checking for [ " + SystemsConfigFile + " ] ...");
                if (!File.Exists(SystemsConfigFile))
                    throw new FileNotFoundException("SB Payroll cannot locate configuration file " + SystemsConfigFile);

                SplashScreen.SetStatus("Checking for Default System...");
                SBSystem defSys = SQLHelper.GetDataDefaultSystem();
                if (defSys == null)
                    throw new ArgumentException("No Default System is Set", "system");

                //SplashScreen.SetStatus("Connecting to the SQL Server [" + defSys.Server + "]");
                //if (!SQLHelper.ServerExists(defSys))
                //    throw new ArgumentException("Unable to connect to Server [" + defSys.Server + "] ", "server");

                SplashScreen.SetStatus("Checking for a valid Database...");
                if (!SQLHelper.DatabaseExists(defSys))
                    throw new ArgumentException("Database [ " + defSys.Database + " ] does not exist in Server [ " + defSys.Server + " ] ", "database");

                SplashScreen.SetStatus("Checking for Database Version...");
                string dbver = SQLHelper.DatabaseVersion(defSys);
                string sysver = Assembly.GetEntryAssembly().GetName().Version.ToString();
                if (!dbver.Equals(sysver))
                    throw new ArgumentException("Database and System Version do not match; the Database may not be usable. Use a Database Migration Tool", "version");


                SplashScreen.SetStatus("Checking Defaults Tables are populated...");
                System.Threading.Thread.Sleep(400);

                SplashScreen.SetStatus("Checking for a valid License...");
                System.Threading.Thread.Sleep(2000);
                SplashScreen.CloseForm();
                Application.Run(new Forms.LoginForm(defSys));
            }
            catch (ArgumentException argex)
            {
                string msgex = string.Empty;
                if (argex.ParamName.Equals("server") || argex.ParamName.Equals("system") || argex.ParamName.Equals("database") || argex.ParamName.Equals("version"))
                {
                    switch (argex.ParamName)
                    {
                        case "system":
                            msgex = "Do you want to search other Servers ?";
                            break;
                        case "server":
                            msgex = "The default Server does not exist. Do you want to search for other Servers ?";
                            break;
                        case "database":
                            msgex = "Do you want to create a Database ?";
                            break;
                        case "version":
                            msgex = "Do you want to create a Database with current Version ?";
                            break;
                    }

                    string msg = argex.Message;
                    if (argex.InnerException != null)
                        msg += "\n" + argex.InnerException.Message;

                    msg += "\n\n " + msgex;

                    if (DialogResult.Yes == MessageBox.Show(msg, "SB Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        Forms.DatabaseControlPanelForm f = new DatabaseControlPanelForm();
                        f.ShowDialog();
                    }
                }
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException smoex
)
            {
                string msg = smoex.Message;
                if (smoex.InnerException != null)
                    msg += "\n" + smoex.InnerException.Message
                        + "\n\n " + "Do you want to Administer Servers and Databases ?";

                if (DialogResult.Yes == MessageBox.Show(msg, "SB Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Forms.DatabaseControlPanelForm f = new DatabaseControlPanelForm();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += "\n" + ex.InnerException.Message
                        + "\n\n " + "Do you want to Administer Servers and Databases ?";

                if (DialogResult.Yes == MessageBox.Show(msg, "SB Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    Forms.DatabaseControlPanelForm f = new DatabaseControlPanelForm();
                    f.ShowDialog();
                }
            }
        }
        #endregion















    }
}