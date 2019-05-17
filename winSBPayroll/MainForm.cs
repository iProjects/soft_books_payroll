using System; 
using System.Collections; 
using System.Collections.Generic;
using System.Diagnostics; 
using System.Drawing;
using System.Drawing.Drawing2D; 
using System.IO;
using System.Linq;
using System.Management;
using System.Management.Instrumentation;
using System.Net; 
using System.Net.NetworkInformation;
using System.Runtime; 
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Threading; 
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using CommonLib;
using DAL; 
using Microsoft.Win32;
using winSBPayroll.Forms;

namespace winSBPayroll
{
    public partial class MainForm : Form
    {

        #region "Private Fields"
        LoginForm parentForm;
        public UserModel LoggedInUser;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        SBSystem system;
        string FILE_NAME = null;
        const string Help_File_Name = "sbpayrollhelpsystem.chm";
        private int updateStatusCounter = 60;
        private int loggedinTimeCounter = 0;
        DateTime _startTime = DateTime.Now;
        string _template;
        #endregion "Private Fields"

        #region "Constructor"
        public MainForm(LoginForm loginForm, string Conn, SBSystem sys)
        {
            InitializeComponent();

            if (sys == null)
                throw new ArgumentNullException("SBSystem");
            system = sys;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            rep = new Repository(connection);
            db = new SBPayrollDBEntities(connection);

            if (loginForm == null)
                throw new ArgumentNullException("loginForm");
            parentForm = loginForm;

            FILE_NAME = @"C:\Program Files\Microsoft SQL Server\100\DTS\Binn\DTSWizard.exe";

        }
        #endregion "Constructor"

        #region  "Authorization"
        public void LogIn()
        {
            try
            {
                LoggedInUser = parentForm.LoggedInUser;
                lblLoggedInUser.Text = "Loggedin:     " + LoggedInUser.UserName;

                NotifyMessage("Log in Successfull.", "Welcome " + LoggedInUser.UserName);

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        #endregion  "Authorization"

        #region  "Helpers"
        private void LoadHelpers()
        {
            try
            {
                this.lblselecteddatabase.Text = string.Empty;
                this.lblversion.Text = string.Empty;
                this.lblLoggedInTime.Text = string.Empty;
                this.lblStatusUpdates.Text = string.Empty;

                AboutBox ab = new AboutBox();
                string AssemblyProduct = ab.AssemblyProduct;
                string AssemblyVersion = ab.AssemblyVersion;
                string AssemblyCopyright = ab.AssemblyCopyright;
                string AssemblyCompany = ab.AssemblyCompany;
                this.Text += AssemblyProduct;
                this.lblselecteddatabase.Text = "Selected Database:     " + system.Database;
                this.lblversion.Text = "Version:     " + AssemblyVersion;
                string s = lblDate.Text;
                this.lblDate.Text = string.Empty;
                this.lblDate.Text += s.Replace("date", "") + DateTime.Today.ToShortDateString();
                this.lblStatusUpdates.Text = string.Empty;
                this.lblStatusUpdates.Visible = false;
                this.toolStripStatusLabel3.Visible = false;
                this.lblLoggedInTime.Text = string.Empty;

                loggedInTimer.Tick += new EventHandler(loggedInTimer_Tick);
                loggedInTimer.Interval = 1000; // 1 second
                loggedInTimer.Start();
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }
        private void loggedInTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                loggedinTimeCounter++;
                DateTime nowDate = DateTime.Now;
                TimeSpan t = nowDate - _startTime;
                lblLoggedInTime.Text = string.Format("{0} : {1} : {2} : {3}", t.Days, t.Hours, t.Minutes, t.Seconds);
                if (loggedinTimeCounter == 1800)
                {
                    //CollectAdminExtraInfo();

                    //if (Utils.IsConnectedToInternet())
                    //{
                    //    //string msFilePDF = @"http://softwareproviders.co.ke/HowToUseSoftBooksPayroll.html";
                    //    //this.webBrowser1.Navigate(msFilePDF);
                    //    lblStatusUpdates.Visible = true;
                    //    toolStripStatusLabel3.Visible = true;
                    //    NotifyMessage("Updates available.", "Check our site for updates...");
                    //}
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }
        private bool CollectAdminExtraInfo()
        {
            try
            {
                ExecuteIPConfigCommands();

                FindComputersConectedToHost();

                GetHostNameandMac();

                GetClientExtraInfo();

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private void updateStatusTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                updateStatusCounter--;
                if (updateStatusCounter == 0)
                {
                    lblStatusUpdates.Text = string.Empty;
                    toolStripStatusLabel3.Visible = false;
                    updateStatusTimer.Stop();
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }
        public void cp_OnuserDTOSelected(object sender, userDTOEventArgs e)
        {
            try
            {
                this.lblStatusUpdates.Text = "Password Changed for user  [ " + e._USer.UserId + " ] at " + DateTime.Now.ToShortTimeString();
                this.lblStatusUpdates.Visible = true;
                this.toolStripStatusLabel3.Visible = true;
                this.updateStatusTimer.Tick += new EventHandler(updateStatusTimer_Tick);
                this.updateStatusTimer.Interval = 1000; // 1 second
                this.updateStatusTimer.Start();

            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }
        private void CloseAllOpenForms()
        {
            try
            {
                List<Form> openForms = new List<Form>();
                foreach (Form f in Application.OpenForms)
                    openForms.Add(f);

                foreach (Form f in openForms)
                {
                    if (f.Name != "MainForm")
                        f.Close();
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }
        private void CollectAdminAppInfo()
        {
            try
            {
                DateTime nowDate = DateTime.Now;
                TimeSpan t = nowDate - _startTime;

                WriteToCurrentUserRegisteryonAppClose(t.ToString());

                //CollectAdminExtraInfo();

                if (Utils.IsConnectedToInternet())
                {
                    string loggederror = Utils.ReadLogFile();
                    string macaddrress = Utils.GetMACAddress();
                    string ipAddresses = Utils.GetFormattedIpAddresses();
                    DateTime _endTime = DateTime.Now;
                    string _totalusagetime = this.ReadFromCurrentUserRegisteryTotalUsageTime();

                    string template = "User [ " + LoggedInUser.UserName + " ] was logged in from [ " + this._startTime.ToString() + " ] to [ " + _endTime.ToString() + " ] total elapsed time [ " + lblLoggedInTime.Text + " ]" + " machine name [ " + FQDN.GetFQDN() + " ] " + " MAC [ " + macaddrress + " ] " + " ip addresses [ " + ipAddresses + " ] " + "Total Usage Time [ " + _totalusagetime + " ] " + "Template [ " + _template + " ] " + " Logged Errors " + " [ " + loggederror + " ] ";

                    Utils.SendEmail(template);
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
            finally
            {
                NotifyMessage("Soft Books Payroll", "Exiting...");
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
            {
                try
                {
                    CollectAdminAppInfo();
                    CloseAllOpenForms();
                }
                catch (Exception ex)
                {
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.UserClosing)
            {
                try
                {
                    CollectAdminAppInfo();
                    CloseAllOpenForms();
                }
                catch (Exception ex)
                {
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                try
                {
                    CollectAdminAppInfo();
                    CloseAllOpenForms();
                }
                catch (Exception ex)
                {
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.FormOwnerClosing)
            {
                try
                {
                    CollectAdminAppInfo();
                    CloseAllOpenForms();
                }
                catch (Exception ex)
                {
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.MdiFormClosing)
            {
                try
                {
                    CollectAdminAppInfo();
                    CloseAllOpenForms();
                }
                catch (Exception ex)
                {
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.None)
            {
                try
                {
                    CollectAdminAppInfo();
                    CloseAllOpenForms();
                }
                catch (Exception ex)
                {
                    Utils.LogEventViewer(ex);
                }
            }
            if (e.CloseReason == CloseReason.TaskManagerClosing)
            {
                try
                {
                    CollectAdminAppInfo();
                    CloseAllOpenForms();
                }
                catch (Exception ex)
                {
                    Utils.LogEventViewer(ex);
                }
            }
        }
        private bool WriteToCurrentUserRegistery()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                RegistryKey MyReg = Registry.CurrentUser.CreateSubKey(registryPath);
                MyReg.SetValue("Company Name", Application.CompanyName);
                MyReg.SetValue("Application Name", Application.ProductName);
                MyReg.SetValue("Version", Application.ProductVersion);
                MyReg.SetValue("Launch Date", DateTime.Now.ToString());
                MyReg.SetValue("Trial Period", "30");
                MyReg.SetValue("Developer", "Kevin Mutugi");
                MyReg.SetValue("Copyright", "Copyright ©  2015 - 2030");
                MyReg.SetValue("GUID", "baedce16-cf28-4a20-a5f3-4c698c242d99");
                MyReg.SetValue("TradeMark", "Soft Books Suite");
                MyReg.SetValue("Phone-Safaricom", "+254717769329");
                MyReg.SetValue("Phone-Zain", "+254736551624");
                MyReg.SetValue("Email", "kevin@softwareproviders.co.ke");
                MyReg.SetValue("Gmail", "kevinmk30@gmail.com");
                MyReg.SetValue("Company Website", "www.softwareproviders.co.ke");
                MyReg.SetValue("Company Email", "info@softwareproviders.co.ke");
                MyReg.Close();
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool WriteToCurrentUserRegisteryonAppClose(string _totalLoggedinTime)
        {
            try
            {
                string _totalusagetime = this.ReadFromCurrentUserRegisteryTotalUsageTime();
                string _lastusagetime = this.ReadFromCurrentUserRegisteryLastUsageTime();

                TimeSpan ts = TimeSpan.Parse(_lastusagetime);
                TimeSpan _tust = TimeSpan.Parse(_totalLoggedinTime);
                TimeSpan _tts = _tust + ts;

                this.DeleteCurrentUserRegistery();

                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                RegistryKey MyReg = Registry.CurrentUser.CreateSubKey(registryPath);
                MyReg.SetValue("Last Usage Time", _totalLoggedinTime);
                MyReg.SetValue("Total Usage Time", _tts);
                MyReg.Close();
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool DeleteCurrentUserRegistery()
        {
            try
            {

                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    MyReg.DeleteValue("Last Usage Time");
                    MyReg.DeleteValue("Total Usage Time");
                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private string ReadFromCurrentUserRegisteryEXP()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    string keyvaluedata = string.Format("{0}", MyReg.GetValue("Trial Period", 30).ToString());
                    return keyvaluedata;
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return null;
            }
        }
        private string ReadFromCurrentUserRegisteryStartDate()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    string keyvaluedata = string.Format("{0}", MyReg.GetValue("Launch Date", DateTime.Now.ToString()).ToString());
                    return keyvaluedata;
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return null;
            }
        }
        private string ReadFromCurrentUserRegisteryTotalUsageTime()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    string keyvaluedata = string.Format("{0}", MyReg.GetValue("Total Usage Time", 0).ToString());
                    return keyvaluedata;
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return null;
            }
        }
        private string ReadFromCurrentUserRegisteryLastUsageTime()
        {
            try
            {
                string registryPath = "SOFTWARE\\" + Application.CompanyName + "\\" + Application.ProductName + "\\" + Application.ProductVersion;
                using (RegistryKey MyReg = Registry.CurrentUser.OpenSubKey(registryPath, RegistryKeyPermissionCheck.ReadWriteSubTree, System.Security.AccessControl.RegistryRights.FullControl))
                {
                    string keyvaluedata = string.Format("{0}", MyReg.GetValue("Last Usage Time", 0).ToString());
                    return keyvaluedata;
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return null;
            }
        }
        private bool CheckSystemLicenseExpiryFromDB()
        {
            try
            {
                var startdate = (from t in db.TechParams
                                 select t.strtdt).FirstOrDefault();

                var expiry_days = (from t in db.TechParams
                                   select t.edc).FirstOrDefault();

                if (startdate == null)
                {
                    DateTime _stardate = DateTime.Now;
                    int _edc = 30;

                    TechParam tp = new TechParam();
                    tp.strtdt = _stardate;
                    tp.edc = _edc;

                    db.TechParams.AddObject(tp);
                    db.SaveChanges();
                }

                if (startdate != null)
                {
                    TimeSpan elapsed_days = DateTime.Now.Date - startdate.Value;
                    int total_elapse_days = elapsed_days.Days;

                    if (total_elapse_days > expiry_days)
                    {
                        DisableApplicationMenus();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool CheckSystemLicenseExpiryFromRegistry()
        {
            try
            {
                string _startdate = this.ReadFromCurrentUserRegisteryStartDate();

                string _expiry_days = this.ReadFromCurrentUserRegisteryEXP();

                var startdate = DateTime.Parse(_startdate).Date;

                var expiry_days = int.Parse(_expiry_days);

                if (startdate == null)
                {
                    WriteToCurrentUserRegistery();
                }

                if (startdate != null)
                {
                    TimeSpan elapsed_days = DateTime.Now.Date - startdate.Date;
                    int total_elapse_days = elapsed_days.Days;

                    if (total_elapse_days > expiry_days)
                    {
                        DisableApplicationMenus();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool CheckSystemLicenseExpiryFromXML()
        {
            try
            {
                string sys_startup_date_filename = "Resources/system_startup_date.xml";
                if (!File.Exists(sys_startup_date_filename))
                {

                    List<SBSystem_Exp> systems = new List<SBSystem_Exp>() { new SBSystem_Exp(DateTime.Now.ToString(), "SBPayroll") };
                    var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                                           new XAttribute("Name", x.Name),
                                           new XAttribute("Application", x.Application))));
                    xml.Save(sys_startup_date_filename);
                }

                string sys_no_of_expiry_days_filename = "Resources/system_no_of_expiry_days.xml";
                if (!File.Exists(sys_no_of_expiry_days_filename))
                {

                    List<SBSystem_Exp> systems = new List<SBSystem_Exp>() { new SBSystem_Exp("30", "SBPayroll") };
                    var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                                           new XAttribute("Name", x.Name),
                                           new XAttribute("Application", x.Application))));
                    xml.Save(sys_no_of_expiry_days_filename);
                }

                DateTime app_start_date;
                using (XmlReader xmlRdr = new XmlTextReader(sys_startup_date_filename))
                {
                    SBSystem_Exp sys_start_date = (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                                                   select new SBSystem_Exp(
                                                      (string)sysElem.Attribute("Name"),
                                                      (string)sysElem.Attribute("Application")
                                                       )).FirstOrDefault();
                    bool appstartdate = DateTime.TryParse(sys_start_date.Name, out app_start_date);
                }

                int no_of_expiry_days;
                using (XmlReader xmlRdr = new XmlTextReader(sys_no_of_expiry_days_filename))
                {
                    SBSystem_Exp sysexp = (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                                           select new SBSystem_Exp(
                                              (string)sysElem.Attribute("Name"),
                                              (string)sysElem.Attribute("Application")
                                               )).FirstOrDefault();
                    bool noofdays = int.TryParse(sysexp.Name, out no_of_expiry_days);
                }

                TimeSpan elapsed_days = DateTime.Now.Date - app_start_date.Date;
                int total_elapse_days = elapsed_days.Days;

                if (total_elapse_days > no_of_expiry_days)
                {
                    DisableApplicationMenus();
                }

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool DisableApplicationMenus()
        {
            try
            {
                this.payrollToolStripMenuItem.Enabled = false;
                this.dataEntryToolStripMenuItem.Enabled = false;
                this.administrationToolStripMenuItem.Enabled = false;
                this.reportsToolStripMenuItem.Enabled = false;

                this.tsbPDFReports.Enabled = false;
                this.tsbOpenPayroll.Enabled = false;
                this.tsbProcessPayroll.Enabled = false;
                this.tspEmployees.Enabled = false;
                this.tsbPayrollItems.Enabled = false;
                this.tsbBanks.Enabled = false;
                this.tsbBenefits.Enabled = false;
                this.tsbDepartments.Enabled = false;
                this.tsbPayeeRates.Enabled = false;
                this.tsbtnNHIFRates.Enabled = false;
                this.tsbTaxCalculator.Enabled = false;

                this.Text += " [ Trial Version ] ";

                statusStripMain.BackColor = Color.Red;
                lblDate.Text += "  Product Activation Failed  ";

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool ExecuteIPConfigCommands()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo sdpsinfo =
new System.Diagnostics.ProcessStartInfo
("ipconfig.exe", "-all");
                // The following commands are needed to
                //redirect the standard output.
                // This means that it will //be redirected to the
                // Process.StandardOutput StreamReader
                // u can try other console applications
                //such as  arp.exe, etc
                sdpsinfo.RedirectStandardOutput = true;
                // redirecting the standard output
                sdpsinfo.UseShellExecute = false;
                // without that console shell window
                sdpsinfo.CreateNoWindow = true;
                // Now we create a process,
                //assign its ProcessStartInfo and start it
                System.Diagnostics.Process p =
                new System.Diagnostics.Process();
                p.StartInfo = sdpsinfo;
                p.Start();
                // well, we should check the return value here...
                //  capturing the output into a string variable...
                string res = p.StandardOutput.ReadToEnd();
                _template += res;
                Debug.Write(res);
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool FindComputersConectedToHost()
        {
            try
            {
                // create the ProcessStartInfo using "cmd" as the program to be run,
                // and "/c " as the parameters.
                // Incidentally, /c tells cmd that we want it to execute the command that follows,
                // and then exit.
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c netstat -a");
                // The following commands are needed to redirect the standard output.
                // This means that it will be redirected to the Process.StandardOutput StreamReader.
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                // Do not create the black window.
                procStartInfo.CreateNoWindow = true;
                // Now we create a process, assign its ProcessStartInfo and start it
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                // Get the output into a string
                string res = proc.StandardOutput.ReadToEnd();
                _template += res;
                Debug.Write(res);
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool GetHostNameandMac()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo sdpsinfo =
new System.Diagnostics.ProcessStartInfo
("cmd.exe", "Getmac,Hostname");
                // The following commands are needed to
                //redirect the standard output.
                // This means that it will //be redirected to the
                // Process.StandardOutput StreamReader
                // u can try other console applications
                //such as  arp.exe, etc
                sdpsinfo.RedirectStandardOutput = true;
                // redirecting the standard output
                sdpsinfo.UseShellExecute = false;
                // without that console shell window
                sdpsinfo.CreateNoWindow = true;
                // Now we create a process,
                //assign its ProcessStartInfo and start it
                System.Diagnostics.Process p =
                new System.Diagnostics.Process();
                p.StartInfo = sdpsinfo;
                p.Start();
                // well, we should check the return value here...
                //  capturing the output into a string variable...
                string res = p.StandardOutput.ReadToEnd();
                _template += res;
                Debug.Write(res);
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private bool GetClientExtraInfo()
        {
            try
            {
                System.Diagnostics.ProcessStartInfo sdpsinfo =
 new System.Diagnostics.ProcessStartInfo
 ("cmd.exe", " NBTSTAT -n,WHOAMI, VER, TASKLIST, GPRESULT /r, NETSTAT,  Assoc, Arp -a");
                // The following commands are needed to
                //redirect the standard output.
                // This means that it will //be redirected to the
                // Process.StandardOutput StreamReader
                // u can try other console applications
                //such as  arp.exe, etc
                sdpsinfo.RedirectStandardOutput = true;
                // redirecting the standard output
                sdpsinfo.UseShellExecute = false;
                // without that console shell window
                sdpsinfo.CreateNoWindow = true;
                // Now we create a process,
                //assign its ProcessStartInfo and start it
                System.Diagnostics.Process p =
                new System.Diagnostics.Process();
                p.StartInfo = sdpsinfo;
                p.Start();
                // well, we should check the return value here...
                //  capturing the output into a string variable...
                string res = p.StandardOutput.ReadToEnd();
                _template += res;
                Debug.Write(res);
                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        #endregion  "Helpers"

        #region  "Private Methods"
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {

                LogIn();

                DisableAllMenus();

                Authorize();

                LoadHelpers();

                //CheckSystemLicenseExpiryFromXML();

                //CheckSystemLicenseExpiryFromDB();

                //CheckSystemLicenseExpiryFromRegistry();

                WriteToCurrentUserRegistery();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void DisableAllMenus()
        {
            try
            {
                this.dataEntryToolStripMenuItem.Enabled = false;
                this.payrollToolStripMenuItem.Enabled = false;
                this.administrationToolStripMenuItem.Enabled = false;
                this.reportsToolStripMenuItem.Enabled = false;

                this.changePasswordToolStripMenuItem.Enabled = false;
                this.openPayrollToolStripMenuItem.Enabled = false;
                this.processPayrollToolStripMenuItem.Enabled = false;
                this.employeesToolStripMenuItem.Enabled = false;
                this.btnBankBranches.Enabled = false;
                this.benefitsToolStripMenuItem.Enabled = false;
                this.departmentsToolStripMenuItem.Enabled = false;
                this.payeeRatesToolStripMenuItem.Enabled = false;
                this.nHIFRatesToolStripMenuItem.Enabled = false;
                this.payrollItemsToolStripMenuItem.Enabled = false;
                this.usersToolStripMenuItem.Enabled = false;
                this.rightsToolStripMenuItem.Enabled = false;
                this.rolesToolStripMenuItem.Enabled = false;
                this.settingsToolStripMenuItem.Enabled = false;
                this.employerToolStripMenuItem.Enabled = false;
                this.databaseControlPanelToolStripMenuItem.Enabled = false;
                this.uploadDownloadWizardToolStripMenuItem.Enabled = false;
                this.pdfReportsToolStripMenuItem.Enabled = false;
                this.taxCalcuToolStripMenuItem.Enabled = false;

                this.tsbPDFReports.Enabled = false;
                this.tsbOpenPayroll.Enabled = false;
                this.tsbProcessPayroll.Enabled = false;
                this.tspEmployees.Enabled = false;
                this.tsbPayrollItems.Enabled = false;
                this.tsbBanks.Enabled = false;
                this.tsbBenefits.Enabled = false;
                this.tsbDepartments.Enabled = false;
                this.tsbPayeeRates.Enabled = false;
                this.tsbtnNHIFRates.Enabled = false;
                this.tsbTaxCalculator.Enabled = false;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void Authorize()
        {
            try
            {
                var allowedmenusquery = from arm in db.spAllowedRoleMenus
                                        where arm.RoleId == LoggedInUser.RoleId
                                        select arm;
                List<spMenuItem> menuitems = new List<spMenuItem>();
                foreach (var armq in allowedmenusquery.ToList())
                {
                    ToolStripMenuItem mnuitem = menuStrip1.Items.Find(armq.spMenuItem.mnuName, true).OfType<ToolStripMenuItem>().FirstOrDefault();

                    if (mnuitem != null && armq.Allowed == true)
                    {
                        mnuitem.Enabled = true;
                    }

                    ToolStripItem tsbitem = toolStrip1.Items.Find(armq.spMenuItem.mnuName, true).OfType<ToolStripItem>().FirstOrDefault();

                    if (tsbitem != null && armq.Allowed == true)
                    {
                        tsbitem.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ChangePasswordForm cp = new ChangePasswordForm(LoggedInUser, this, connection) { Owner = this };
                cp.Text = "Change PassWord -  " + LoggedInUser.UserName.ToString();
                cp.OnuserDTOSelected += new ChangePasswordForm.userDTOHandler(cp_OnuserDTOSelected);
                cp.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void employeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Employees emp = new Forms.Employees(LoggedInUser.UserName, connection);
                emp.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void transactionDefenitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.TransactionDefinition f = new Forms.TransactionDefinition(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void taxTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.TaxTracking f = new Forms.TaxTracking(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void payrollItemTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.PayrollItemTypes f = new Forms.PayrollItemTypes(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void payrollItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.PayrollItems f = new Forms.PayrollItems(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void packTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.PackTransaction f = new Forms.PackTransaction(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void postPackedTransactionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AddEmployeeAdvance f = new Forms.AddEmployeeAdvance(LoggedInUser.UserName, connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void pdfReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                PDFViewer v = new PDFViewer(LoggedInUser.UserName, connection);
                v.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void processPayrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.ProcessPayroll f = new Forms.ProcessPayroll(LoggedInUser.UserName, connection);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void openPayrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Payrolls f = new Forms.Payrolls(this, connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void banksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Banks f = new Forms.Banks(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbProcessPayroll_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.ProcessPayroll f = new Forms.ProcessPayroll(LoggedInUser.UserName, connection);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tspEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Employees emp = new Forms.Employees(LoggedInUser.UserName, connection);
                emp.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbOpenPayroll_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Payrolls f = new Forms.Payrolls(this, connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbBanks_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.BanksForm f = new Forms.BanksForm(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbTransactionDefinitions_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.TransactionDefinition f = new Forms.TransactionDefinition(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbTaxTracking_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.TaxTracking f = new Forms.TaxTracking(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbPayrollItems_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.PayrollItems f = new Forms.PayrollItems(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbPayrollItemTypes_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.PayrollItemTypes f = new Forms.PayrollItemTypes(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbPDFReports_Click(object sender, EventArgs e)
        {
            try
            {
                PDFViewer v = new PDFViewer(LoggedInUser.UserName, connection);
                v.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbPackTransactions_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.PackTransaction f = new Forms.PackTransaction(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void taxCalcuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.TaxCalculatorForm f = new Forms.TaxCalculatorForm(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AboutBox f = new AboutBox();
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Settings f = new Forms.Settings(connection);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Users us = new Forms.Users(connection);
                us.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void transactionDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.TransactionDefinition td = new Forms.TransactionDefinition(connection);
                td.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void employerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Employer empl = new Forms.Employer(connection);
                empl.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void departmentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Departments depart = new Forms.Departments(connection);
                depart.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void nHIFRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.NHIFRates nr = new Forms.NHIFRates(LoggedInUser.UserName, connection);
                nr.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void payeeRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.PayeeRates pr = new Forms.PayeeRates(LoggedInUser.UserName, connection);
                pr.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void benefitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Benefits ben = new Forms.Benefits(connection);
                ben.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbDepartments_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Departments depart = new Forms.Departments(connection);
                depart.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void payrollItemTypesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Forms.PayrollItemTypes f = new Forms.PayrollItemTypes(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void payrollItemsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                Forms.PayrollItems f = new Forms.PayrollItems(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void toolbtnBenefits_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.Benefits ben = new Forms.Benefits(connection);
                ben.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void tsbtnNHIFRates_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.NHIFRates nr = new Forms.NHIFRates(LoggedInUser.UserName, connection);
                nr.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnTaxCalculator_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.TaxCalculatorForm f = new Forms.TaxCalculatorForm(connection);
                f.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnPayeeRates_Click_1(object sender, EventArgs e)
        {
            try
            {
                Forms.PayeeRates p = new Forms.PayeeRates(LoggedInUser.UserName, connection);
                p.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnBankBranches_Click(object sender, EventArgs e)
        {
            try
            {
                winSBPayroll.Forms.BanksForm bank = new winSBPayroll.Forms.BanksForm(connection);
                bank.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utils.IsConnectedToInternet())
                {
                    string msFilePDF = @"http://softwareproviders.co.ke/HowToUseSoftBooksPayroll.html";
                    this.webBrowser1.Navigate(msFilePDF);
                    lblStatusUpdates.Visible = true;
                    toolStripStatusLabel3.Visible = true;
                }
                else
                {
                    NotifyMessage("Soft Books Payroll", "Unable to connect to the Internet...");
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void databaseControlPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DatabaseControlPanelForm dcpf = new DatabaseControlPanelForm();
                dcpf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void lblSelectedDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                SystemDetailsForm sysf = new SystemDetailsForm(system);
                sysf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void uploadDownloadWizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(FILE_NAME))
                {
                    Process p = null;
                    if (p == null)
                    {
                        p = new Process();
                        p.StartInfo.FileName = FILE_NAME;
                        p.Start();
                        p.WaitForExit();
                    }
                }
                else
                {
                    MessageBox.Show("Microsoft SQL Server Data Transfer Wizard Does Not Exist in Path...\n" + @"C:\Program Files\Microsoft SQL Server\100\DTS\Binn\DTSWizard.exe" + "\nCheck your System and make sure the File Exists", "Microsoft SQL Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
                if (Utils.IsConnectedToInternet())
                {
                    string msFilePDF = @"http://softwareproviders.co.ke/HowToUseSoftBooksPayroll.html";
                    this.webBrowser1.Navigate(msFilePDF);
                    lblStatusUpdates.Visible = true;
                    toolStripStatusLabel3.Visible = true;
                }
                else
                {
                    NotifyMessage("Soft Books Payroll", "Unable to connect to the Internet...");
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void rightsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RightsListForm sccf = new RightsListForm(connection);
                sccf.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void rolesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RolesListForm rlf = new RolesListForm(connection);
                rlf.Show();
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
                Application.Exit();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void homeToolStripMenuItemNotify_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("http://softwareproviders.co.ke/HowToUseSoftBooksPayroll.html");
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
                appNotifyIcon.Text = "Soft Books Payroll";
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
        private void newNSSFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                NewNSSFComputationForm rlf = new NewNSSFComputationForm(LoggedInUser.UserName, connection);
                rlf.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        #endregion  "Private Methods"

















    }
}