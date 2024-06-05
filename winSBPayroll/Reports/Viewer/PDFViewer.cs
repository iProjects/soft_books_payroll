using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using BLL.KRA;
using BLL.KRA.ModelMakers;
using BLL.KRA.Models;
using CommonLib;
using DAL;
//--- Add the following to make this code work
using iTextSharp.text;
using VVX;
using winSBPayroll.Forms;
using System.Threading;

namespace winSBPayroll.Forms
{
    public partial class PDFViewer : Form
    {
        #region "Private Fields"
        private string msAppName = "SB Payroll Report.....";
        PDFGen pdf_generator;
        string current_file_name = "";
        string msFolder = "";
        List<Employee> _employeesList;
        List<DAL.Payroll> openPayrolls;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        static bool showExcel = false;
        string _user;
        string _resourcesPath = null;
        string connection;
        List<NssfContributionsDTO> _nssfcomputations = new List<NssfContributionsDTO>();
        string TAG;
        List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname_from_parent;
        #endregion "Private Fields"

        #region "Public Fields"
        //delegate
        delegate void ReportsProcessCompleteEventHandler(object sender, ReportsProcessCompleteEventArg e);
        //event
        event ReportsProcessCompleteEventHandler OnCompleteReportsProcess;
        //delegate
        delegate void ReportsEngineCompleteEventHandler(object sender, ReportsEngineCompleteEventArg e);
        //event
        event ReportsEngineCompleteEventHandler OnCompleteReportsEngine;
        #endregion "Public Fields"

        #region "Constructor"
        public PDFViewer(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            rep = new Repository(connection);
            db = new SBPayrollDBEntities(connection);

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _notificationmessageEventname_from_parent = notificationmessageEventname_from_parent;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished PDFViewer initialization", TAG));

            _user = user;

            DisableAllMenus();

            Authorize();

            //--- init the folder in which generated PDF's will be saved.
            msFolder = AppDomain.CurrentDomain.BaseDirectory;
            int n = msFolder.LastIndexOf(@"\");
            msFolder = msFolder.Substring(0, n + 1);

            SetResourcePath();

            pdf_generator = new PDFGen(_resourcesPath, connection, _notificationmessageEventname);

        }
        public PDFViewer(List<NssfContributionsDTO> nssfcomputations, string user, string Conn)
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            rep = new Repository(connection);
            db = new SBPayrollDBEntities(connection);

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished PDFViewer initialization", TAG));

            _user = user;

            DisableAllMenus();

            Authorize();

            //--- init the folder in which generated PDF's will be saved.
            msFolder = AppDomain.CurrentDomain.BaseDirectory;
            int n = msFolder.LastIndexOf(@"\");
            msFolder = msFolder.Substring(0, n + 1);

            SetResourcePath();

            pdf_generator = new PDFGen(_resourcesPath, connection, _notificationmessageEventname);

            _nssfcomputations = nssfcomputations;

            ShowNewNSSF("pdf", _nssfcomputations);
        }

        #endregion "Constructor"

        #region "General Purpose Helpers for this Form"
        //************************************************************
        /// <summary>
        /// Refreshes the window's Caption/Title bar
        /// </summary>
        private void DoUpdateCaption()
        {
            try
            {
                this.Text = this.msAppName;

                if (this.current_file_name.Length == 0)
                {
                    this.Text += "<...no PDF file created...>";
                }
                else
                {
                    FileInfo fi = new FileInfo(get_reports_uri(this.current_file_name));
                    this.Text += @"...\" + fi.Name;
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void DoPreProcess(object sender, EventArgs e)
        {
            this.lblstatusinfo.Text = string.Empty;
            string msg = "processing report...";
            this.lblstatusinfo.Text = msg;
            this.Text = msg;
        }
        public string pathlookup(string folder)
        {
            try
            {
                string app_dir = Utils.get_application_path();
                var dir = Path.Combine(app_dir, folder);


                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                return dir;

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                return null;
            }
        }
        private void DoPostProcess(object sender, EventArgs e)
        {
            try
            {
                string dir = pathlookup("reports");
                string sRet = Utils.build_file_path(dir, current_file_name);
                int pdfCount = Directory.GetFiles(dir, "*.pdf", SearchOption.TopDirectoryOnly).Length;
                int excelCount = Directory.GetFiles(dir, "*.xls", SearchOption.TopDirectoryOnly).Length;
                int _totalFiles = pdfCount + excelCount;
                this.lblstatusinfo.Text = current_file_name.ToString() + "     [  " + _totalFiles.ToString() + "  ] ";

                copy_to_user_temp();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private string get_reports_uri(string sFile)
        {
            string sRet;
            try
            {
                string dir = pathlookup("reports");
                sRet = Utils.build_file_path(dir, sFile);

                //check if directory exists.
                if (!System.IO.Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return sRet;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
                return "";
            }
        }
        private void SetResourcePath()
        {
            string sRet = string.Empty;
            try
            {
                string dir = pathlookup("resources");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                else
                {
                    sRet = dir;
                }

                this._resourcesPath = sRet;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
                this._resourcesPath = Utils.build_file_path(msFolder, "resources");
            }
        }
        private void DoShowPDF(string sFilePDF)
        {
            this.DoUpdateCaption();
            this.webBrowser.Navigate(get_reports_uri(sFilePDF));
        }
        private void NavigateToHomePage()
        {
            try
            {
                string help_file = "index.html";

                string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                string help_path = Path.Combine(base_directory, "help");
                string help_file_path = Path.Combine(help_path, help_file);

                FileInfo fi = new FileInfo(help_file_path);

                if (fi.Exists)
                    this.webBrowser.Navigate(fi.FullName);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }

        private void copy_to_user_temp()
        {
            try
            {
                string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                string reports_path = Path.Combine(base_directory, "Reports");

                string temp_path = Path.GetTempPath();

                DirectoryInfo reports_dir_info = new DirectoryInfo(reports_path);
                DirectoryInfo temp_dir_info = new DirectoryInfo(temp_path);

                var files = reports_dir_info.GetFiles();

                foreach (var report_file_info in files)
                {
                    var _temp_file = Path.Combine(temp_path, report_file_info.Name);

                    System.IO.File.Copy(report_file_info.FullName, _temp_file, true);

                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void lblstatusinfo_Click(object sender, EventArgs e)
        {
            try
            {
                string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                string reports_path = Path.Combine(base_directory, "Reports");

                if (Directory.Exists(reports_path))
                {
                    string _filetoSelect = Path.Combine(reports_path, current_file_name);
                    // opens the folder in explorer and selects the displayed file
                    string args = string.Format("/Select, {0}", _filetoSelect);
                    ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", args);
                    System.Diagnostics.Process.Start(pfi);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private bool NotifyMessage(string _Title, string _Text)
        {
            try
            {
                appNotifyIcon.Text = Utils.APP_NAME;
                appNotifyIcon.Icon = new Icon("Resources/Icons/Dollar.ico");
                appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                appNotifyIcon.BalloonTipTitle = _Title;
                appNotifyIcon.BalloonTipText = _Text;
                appNotifyIcon.Visible = true;
                appNotifyIcon.ShowBalloonTip(900000);

                return true;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
                return false;
            }
        }

        #endregion "General Purpose Helpers for this Form"

        #region "Private Methods"

        #region "initialization"

        private void PDFViewer_Load(object sender, EventArgs e)
        {
            try
            {
                NavigateToHomePage();
                InitializeControls();
                RefreshGrid();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished PDFViewer load", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void InitializeControls()
        {
            try
            {
                var _employersquery = from ep in db.Employers
                                      where ep.IsActive == true
                                      where ep.IsDeleted == false
                                      select ep;

                List<DAL.Employer> _employers = _employersquery.ToList();
                DataGridViewComboBoxColumn colCboxEmployer = new DataGridViewComboBoxColumn();
                colCboxEmployer.HeaderText = "Employers";
                colCboxEmployer.Name = "cbEmployer";
                colCboxEmployer.DataSource = _employers;
                // The display member is the name column in the column datasource  
                colCboxEmployer.DisplayMember = "Name";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxEmployer.DataPropertyName = "EmployerId";
                // The value member is the primary key of the parent table  
                colCboxEmployer.ValueMember = "Id";
                colCboxEmployer.MaxDropDownItems = 10;
                colCboxEmployer.Width = 100;
                colCboxEmployer.DisplayIndex = 3;
                colCboxEmployer.MinimumWidth = 5;
                colCboxEmployer.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colCboxEmployer.FlatStyle = FlatStyle.Flat;
                colCboxEmployer.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployer.ReadOnly = true;
                if (!this.dataGridViewPayroll.Columns.Contains("cbEmployer"))
                {
                    dataGridViewPayroll.Columns.Add(colCboxEmployer);
                }

                var _payroll_years_query = (from p in db.Payrolls
                                            orderby p.Year descending
                                            select p.Year).Distinct();

                _payroll_years_query = _payroll_years_query.OrderByDescending(i => i);

                List<int> _lst_payroll_years = _payroll_years_query.ToList();

                cbopayrollyears.DisplayMember = "Year";
                cbopayrollyears.ValueMember = "Year";
                cbopayrollyears.DataSource = _lst_payroll_years;

                cbopayrollproducts.SelectedIndex = 0;

                dataGridViewPayroll.AutoGenerateColumns = false;
                this.dataGridViewPayroll.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridViewEmployers.AutoGenerateColumns = false;
                this.dataGridViewEmployers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridViewEmployee.AutoGenerateColumns = false;
                this.dataGridViewEmployee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                dataGridViewPayrollItem.AutoGenerateColumns = false;
                this.dataGridViewPayrollItem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                bindingSourcePayrollItem.DataSource = de.GetActivePayrollItems();
                dataGridViewPayrollItem.DataSource = bindingSourcePayrollItem;
                groupBox2.Text = bindingSourcePayrollItem.Count.ToString();

                Dictionary<string, string> reports_items = new Dictionary<string, string>(){
                    {"Payroll Master","Payroll Master"},
                    {"PAYE","PAYE"},
                    {"NSSF","NSSF"},
                    {"NHIF","NHIF"},
                    {"Net Salary","Net Salary"},
                    {"Bank Transfer","Bank Transfer"},
                    {"Bank Branch Transfer","Bank Branch Transfer"},
                    {"Loan Repayments","Loan Repayments"},
                    {"Sacco Contributions","Sacco Contributions"},
                    {"Advances","Advances"},
                    {"Employees","Employees"},
                    {"Bank Employees","Bank Employees"},
                    {"Mpesa Employees","Mpesa Employees"},
                    {"Cash Employees","Cash Employees"}
                };

                cboItemTypesReports.ComboBox.DisplayMember = "key";
                cboItemTypesReports.ComboBox.ValueMember = "value";
                cboItemTypesReports.ComboBox.DataSource = new BindingSource(reports_items, null);

                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPagePayrolls)];
                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPageEmployers)];
                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPageEmployees)];
                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPagePayrollByProducts)];
                tabControlReportsData.SelectedTab = tabControlReportsData.TabPages[tabControlReportsData.TabPages.IndexOf(tabPagePayrolls)];
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        }

        //Event handler declaration:
        private void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            try
            {
                /* Handler logic */
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

                if (_logflippedlines.Length > 5000)
                {
                    _lstnotificationdto.Clear();
                }

                txtlog.Lines = _logflippedlines;
                txtlog.ScrollToCaret();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RefreshGrid()
        {
            try
            {
                //start with process 
                if (chkUseCurrentPayroll.Checked)
                {
                    //For current payroll; isOpen = true; Processed = true
                    openPayrolls = de.GetPayrolls(PayrollState.OpenProcessed);
                }
                else
                {
                    //For current payroll; isOpen = false; Processed = true
                    openPayrolls = de.GetPayrolls(PayrollState.NotOpenProcessed);
                }

                bindingSourcePayroll.DataSource = openPayrolls.OrderByDescending(i => i.DateRun).ToList();
                dataGridViewPayroll.DataSource = bindingSourcePayroll;
                groupBox3.Text = bindingSourcePayroll.Count.ToString();

                cbYr_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }
        private void Authorize()
        {
            try
            {
                var _dbuserquery = (from us in db.spUsers
                                    where us.UserName == _user
                                    select us).FirstOrDefault();

                spUser LoggedInUser = _dbuserquery;

                if (LoggedInUser != null)
                {
                    var allowedmenusquery = from arm in db.spAllowedReportsRolesMenus
                                            where arm.RoleId == LoggedInUser.RoleId
                                            select arm;

                    foreach (var armq in allowedmenusquery.ToList())
                    {
                        ToolStripItem tsbitem = toolStrip1.Items.Find(armq.spReportsMenuItem.mnuName, true).OfType<ToolStripItem>().FirstOrDefault();

                        if (tsbitem != null && armq.Allowed == true)
                        {
                            tsbitem.Enabled = true;
                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Authorized menu [ {0} ]", armq.spReportsMenuItem.mnuName), TAG));
                        }

                        ToolStripButton tsbutton = toolStrip1.Items.Find(armq.spReportsMenuItem.mnuName, true).OfType<ToolStripButton>().FirstOrDefault();

                        if (tsbutton != null && armq.Allowed == true)
                        {
                            tsbutton.Enabled = true;
                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Authorized menu [ {0} ]", armq.spReportsMenuItem.mnuName), TAG));
                        }

                        Button btnitem = panel3.Controls.Find(armq.spReportsMenuItem.mnuName, true).OfType<Button>().FirstOrDefault();

                        if (btnitem != null && armq.Allowed == true)
                        {
                            btnitem.Enabled = true;
                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Authorized menu [ {0} ]", armq.spReportsMenuItem.mnuName), TAG));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void DisableAllMenus()
        {
            try
            {
                this.btnViewPayslip.Enabled = false;
                this.btnP9A.Enabled = false;
                this.btnP9AHOSP.Enabled = false;
                this.btnP9B.Enabled = false;
                this.btnP10.Enabled = false;
                this.btnP10A.Enabled = false;
                this.btnp11.Enabled = false;
                this.btnViewAllPayslip.Enabled = false;
                this.cboItemTypesReports.Enabled = false;
                this.btnShowPDF.Enabled = false;
                this.btnShowExcel.Enabled = false;
                this.btnShow_Statement_or_Schedule.Enabled = false;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        //hander
        private void HandleSelectedEmployeeList(object sender, EmployeeSelectEventArgs e)
        {
            try
            {
                SetEmpNos(e._Employee);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        //Implementation
        public void SetEmpNos(DAL.Employee _Employee)
        {
            try
            {
                if (_Employee != null)
                {
                    bindingSourceEmployees.DataSource = _Employee;
                    groupBox1.Text = bindingSourceEmployees.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion "initialization"

        #region "datagridview"
        private void dataGridViewByProducts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ("STATEMENT".Equals(cbopayrollproducts.Text.Trim().ToUpper()))
                {
                    if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayrollItem.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                    {
                        DAL.Employee _employee = (DAL.Employee)bindingSourceEmployees.Current;
                        DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                        DAL.PayrollItem _Payrollitem = (DAL.PayrollItem)bindingSourcePayrollItem.Current;
                        int Year = int.Parse(cbopayrollyears.Text);
                        Payroll pay = (Payroll)bindingSourcePayroll.Current;

                        StatementModel statementmodel = ProcessStatement(_employee, Year, pay.Period, _Payrollitem);
                        current_file_name = "Statement " + _employee.EmpNo.Trim() + "  " + _Payrollitem.Id.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name;

                        DoPreProcess(sender, e);

                        string app = string.Empty;
                        if (showExcel)
                        {
                            current_file_name = current_file_name + ".xlsx";
                            app = "excel";
                        }
                        else
                        {
                            current_file_name = current_file_name + ".pdf";
                            app = "pdf";
                        }

                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                        if (pdf_generator.ShowStatement(app, _Payrollitem.Id, statementmodel, get_reports_uri(current_file_name)))
                        {
                            DoShowPDF(current_file_name);
                        }
                        this.DoPostProcess(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Please select a Payroll Year , an Employee and a Payroll By Product ",
                          "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else //schedule
                {
                    if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewPayrollItem.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                    {
                        Payroll pay = (Payroll)bindingSourcePayroll.Current;
                        DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                        DAL.PayrollItem _payrollitem = (DAL.PayrollItem)bindingSourcePayrollItem.Current;

                        SheduleReportModel schedule = ProcessSchedule(pay, _payrollitem);
                        current_file_name = "Schedule " + _payrollitem.Id.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name;

                        string app = string.Empty;

                        DoPreProcess(sender, e);

                        if (showExcel)
                        {
                            current_file_name = current_file_name + ".xlsx";
                            app = "excel";
                        }
                        else
                        {
                            current_file_name = current_file_name + ".pdf";
                            app = "pdf";
                        }

                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                        if (pdf_generator.ShowShedule(app, _payrollitem.Id, schedule, get_reports_uri(current_file_name)))
                        {
                            DoShowPDF(current_file_name);
                        }
                        this.DoPostProcess(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Please select a Payroll Year and a Payroll By Product ",
                          "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewEmployee_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnViewPayslip_Click(sender, e);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewEmployers_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSourceEmployees.DataSource = null;

                if (dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    var _Employeesquery = from ep in rep.GetAllActiveEmployees()
                                          where ep.IsActive == true
                                          where ep.IsDeleted == false
                                          where ep.EmployerId == _employer.Id
                                          select ep;
                    List<DAL.Employee> _Employees = _Employeesquery.ToList();

                    bindingSourceEmployees.DataSource = _Employees;
                    dataGridViewEmployee.DataSource = bindingSourceEmployees;
                    groupBox1.Text = bindingSourceEmployees.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewPayroll_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSourceEmployers.DataSource = null;

                if (dataGridViewPayroll.SelectedRows.Count != 0)
                {
                    DAL.Payroll pay = (DAL.Payroll)bindingSourcePayroll.Current;

                    var _Employersquery = from emp in db.Employers
                                          where emp.IsActive == true
                                          where emp.IsDeleted == false
                                          where emp.Id == pay.EmployerId
                                          orderby emp.Id descending
                                          select emp;

                    List<DAL.Employer> _Employers = _Employersquery.ToList();

                    bindingSourceEmployers.DataSource = _Employers;
                    dataGridViewEmployers.DataSource = bindingSourceEmployers;
                    groupBox4.Text = bindingSourceEmployers.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void tabControlReportsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selected_tab = tabControlReportsData.SelectedTab.Name;

                switch (selected_tab)
                {
                    case "tabPageEmployees":
                        DAL.Employee _employee = (DAL.Employee)bindingSourceEmployees.Current;
                        break;
                    case "tabPageEmployers":
                        DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                        dataGridViewEmployers_SelectionChanged(sender, e);
                        break;
                    case "tabPagePayrollByProducts":
                        DAL.PayrollItem _Payrollitem = (DAL.PayrollItem)bindingSourcePayrollItem.Current;
                        break;
                    case "tabPagePayrolls":
                        Payroll pay = (Payroll)bindingSourcePayroll.Current;
                        dataGridViewPayroll_SelectionChanged(sender, e);
                        break;
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewPayroll_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                throw e.Exception;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void dataGridViewEmployers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                throw e.Exception;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void dataGridViewEmployee_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                throw e.Exception;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void dataGridViewPayrollItem_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                throw e.Exception;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        #endregion "datagridview"

        #region "buttons"
        private void btnEmployeeReportbutton_Click(object sender, EventArgs e)
        {
            try
            {
                DoPreProcess(sender, e);

                current_file_name = "EmpList.pdf";

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                if (pdf_generator.EmployeeListing(get_reports_uri(current_file_name)))
                {
                    DoShowPDF(current_file_name);
                }
                this.DoPostProcess(sender, e);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void btnFilterEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.SearchEmployeeSimpleForm f = new Forms.SearchEmployeeSimpleForm(connection);

                //connect to the delegate
                f.OnEmployeeListSelected += new SearchEmployeeSimpleForm.EmployeeSelectHandler(HandleSelectedEmployeeList);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void btnP9A_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    current_file_name = "P9A " + emp.EmpNo.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    P9AReportMaker p9AMaker = new P9AReportMaker(_employer, chkUseCurrentPayroll.Checked, emp.Id, emp.EmpNo, pay.Year, connection);
                    P9AReportModel p9A = p9AMaker.GetP9A();

                    DoPreProcess(sender, e);

                    if (pdf_generator.ShowP9A(p9A, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Log.WriteToErrorLogFile_and_EventViewer(ex);
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year, an Employer and an Employee",
                    "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnP9AHOSP_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    current_file_name = "P9AHOSP " + emp.EmpNo.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    P9AHospReportMaker p9AMaker = new P9AHospReportMaker(_employer, chkUseCurrentPayroll.Checked, emp.Id, emp.EmpNo, pay.Year, connection);
                    P9AHOSPReportModel p9A = p9AMaker.GetP9Hosp();

                    DoPreProcess(sender, e);

                    if (pdf_generator.ShowP9AHOSP(p9A, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Log.WriteToErrorLogFile_and_EventViewer(ex);
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year, an Employer and an Employee",
                    "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnP9B_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    current_file_name = "P9B " + emp.EmpNo.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    P9BReportMaker p9BMaker = new P9BReportMaker(_employer, chkUseCurrentPayroll.Checked, emp.Id, emp.EmpNo, pay.Year, connection);
                    P9BReportModel p9B = p9BMaker.GetP9B();

                    DoPreProcess(sender, e);

                    if (pdf_generator.ShowP9B(p9B, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Log.WriteToErrorLogFile_and_EventViewer(ex);
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year, an Employer and an Employee",
                     "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnP10A_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    current_file_name = "P10A " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    P10AReportMaker p10AMaker = new P10AReportMaker(_employer, chkUseCurrentPayroll.Checked, pay.Year, connection);
                    P10AReportModel p10A = p10AMaker.GetP10A();

                    DoPreProcess(sender, e);

                    if (pdf_generator.ShowP10A(p10A, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);

                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Log.WriteToErrorLogFile_and_EventViewer(ex);
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                    "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void btnP10_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    current_file_name = "P10 " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    P10ReportMaker p10maker = new P10ReportMaker(_employer, chkUseCurrentPayroll.Checked, pay.Year, connection);
                    P10ReportModel p10model = p10maker.BuildP10();

                    DoPreProcess(sender, e);

                    if (pdf_generator.ShowP10(p10model, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);

                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Log.WriteToErrorLogFile_and_EventViewer(ex);
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                   "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnp11_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {

                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    current_file_name = "p11 " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    P11ReportMaker p11Maker = new P11ReportMaker(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    P11ReportModel p11 = p11Maker.GetP11Model();

                    DoPreProcess(sender, e);

                    if (pdf_generator.ShowP11(p11, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);

                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Log.WriteToErrorLogFile_and_EventViewer(ex);
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                   "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnViewPayslip_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    string payslipType = de.SettingLookup("PAYSLIPTYPE");
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    current_file_name = "Payslip " + emp.EmpNo.Trim() + " " + emp.Surname + " " + emp.OtherNames + " " + pay.Period.ToString() + " " + pay.Year.ToString() + " " + _employer.Name + ".pdf";

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    PayslipReader pr = new PayslipReader(emp.Id, emp.EmpNo, pay.Period, pay.Year, connection);
                    Payslip payslip = pr.CreatePayslipFromPayslipMaster(chkUseCurrentPayroll.Checked);

                    if (payslip == null)
                    {
                        MessageBox.Show("Paslip for this employee does not exist, please check if Payroll for this Employee and Employer is processed.",
                                           "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    DoPreProcess(sender, e);

                    if (pdf_generator.ShowPayslip(payslipType, payslip, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }

                    this.DoPostProcess(sender, e);
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Log.WriteToErrorLogFile_and_EventViewer(ex);
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select a Payroll Year, an Employer and an Employee",
                    "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnViewAllPayslip_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                DAL.Payroll pay = (DAL.Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                List<DAL.Employee> _employees_List = rep.GetAllActiveEmployeesforEmployer(_employer.Id);

                current_file_name = "All Payslip " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                List<DAL.Payslip> payslips = new List<DAL.Payslip>();

                foreach (var emp in _employees_List)
                {
                    try
                    {
                        //read payslip form payslipmaster + payslipdet
                        PayslipReader pr = new PayslipReader(emp.Id, emp.EmpNo, pay.Period, pay.Year, connection);
                        DAL.Payslip payslip = pr.CreatePayslipFromPayslipMaster(chkUseCurrentPayroll.Checked);
                        if (payslip != null)
                        {
                            payslips.Add(payslip);
                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("generated payslip for Employee [ {0} ], Period [ {1} ], Year [{2}], Employer [ {3} ]", emp.OtherNames + " " + emp.Surname, pay.Period, pay.Year, _employer.Name), TAG));
                        }
                    }
                    catch (Exception ex)
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                        Log.WriteToErrorLogFile_and_EventViewer(ex);
                        Utils.ShowError(ex);
                        return;
                    }
                }

                if (payslips.Count == 0)
                {
                    string msg = string.Format("Error processing payslips for [ {0} ] employees, please check if Payroll for these Employees for Employer [ {1} ] are processed.", _employees_List.Count, _employer.Name);
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));
                    MessageBox.Show(msg, "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DoPreProcess(sender, e);

                if (pdf_generator.ShowPayslip(payslips, get_reports_uri(current_file_name)))
                {
                    DoShowPDF(current_file_name);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                   "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnShowPDF_Click(object sender, EventArgs e)
        {
            ToggleExcelPDF();
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            ToggleExcelPDF();
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if ("STATEMENT".Equals(cbopayrollproducts.Text.Trim().ToUpper()))
                {
                    if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayrollItem.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                    {
                        DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                        DAL.PayrollItem _Payrollitem = (DAL.PayrollItem)bindingSourcePayrollItem.Current;
                        Payroll pay = (Payroll)bindingSourcePayroll.Current;
                        DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                        int Year = int.Parse(cbopayrollyears.Text);

                        StatementModel statementmodel = ProcessStatement(emp, Year, pay.Period, _Payrollitem);
                        current_file_name = "Statement " + emp.EmpNo.Trim() + "  " + _Payrollitem.Id.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name;

                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                        DoPreProcess(sender, e);

                        string app = string.Empty;
                        if (showExcel)
                        {
                            current_file_name = current_file_name + ".xlsx";
                            app = "excel";
                        }
                        else
                        {
                            current_file_name = current_file_name + ".pdf";
                            app = "pdf";
                        }

                        if (pdf_generator.ShowStatement(app, _Payrollitem.Id, statementmodel, get_reports_uri(current_file_name)))
                        {
                            DoShowPDF(current_file_name);
                        }
                        this.DoPostProcess(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Please select a Payroll Year , an Employee and a Payroll By Product ",
                          "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else //schedule
                {
                    if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewPayrollItem.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                    {
                        Payroll pay = (Payroll)bindingSourcePayroll.Current;
                        DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                        DAL.PayrollItem _payrollitem = (DAL.PayrollItem)bindingSourcePayrollItem.Current;

                        SheduleReportModel schedule = ProcessSchedule(pay, _payrollitem);
                        current_file_name = "Schedule " + _payrollitem.Id.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name;

                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                        string app = string.Empty;

                        DoPreProcess(sender, e);
                        if (showExcel)
                        {
                            current_file_name = current_file_name + ".xlsx";
                            app = "excel";
                        }
                        else
                        {
                            current_file_name = current_file_name + ".pdf";
                            app = "pdf";
                        }

                        if (pdf_generator.ShowShedule(app, _payrollitem.Id, schedule, get_reports_uri(current_file_name)))
                        {
                            DoShowPDF(current_file_name);
                        }
                        this.DoPostProcess(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Please select a Payroll Year and a Payroll By Product ",
                          "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void btnTsbExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void btnRemoveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                bindingSourceEmployees.DataSource = null;
                if (dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    var _Employeesquery = from ep in rep.GetAllActiveEmployees()
                                          where ep.IsActive == true
                                          where ep.IsDeleted == false
                                          where ep.EmployerId == _employer.Id
                                          select ep;
                    List<Employee> _Employees = _Employeesquery.ToList();

                    bindingSourceEmployees.DataSource = _Employees;
                    dataGridViewEmployee.AutoGenerateColumns = false;
                    this.dataGridViewEmployee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewEmployee.DataSource = bindingSourceEmployees;
                    groupBox1.Text = bindingSourceEmployees.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        #endregion "buttons"
        #region "custom"
        private void ShowPayrollMaster(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create payeeModel
                    PayrollMasterBuilder pm = new PayrollMasterBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    PayrollMasterModel payModel = pm.GetPayrollMaster();

                    var _engs = rep.GetAllEarnings();
                    var _ddct = rep.GetAllOtherDeductions();

                    List<string> earnings = (from ea in _engs
                                             orderby ea.Description descending
                                             select ea.Description).Distinct().ToList();
                    List<string> deductions = (from dd in _ddct
                                               orderby dd.Description descending
                                               select dd.Description).Distinct().ToList();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Payroll Master " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Payroll Master " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowPayrollMaster(earnings, deductions, app, payModel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    //MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowPAYE(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {

                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create payeeModle
                    PAYEModelBuilder pmb = new PAYEModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Year, pay.Period, connection);
                    PAYEModel payeModel = pmb.GetPAYEModel();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "PAYE " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "PAYE " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowPayee(app, payeModel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowNSSF(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {

                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    NSSFReportBuilder nssfreportbuilder = new NSSFReportBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    NSSFReportModel nssfreportmodel = nssfreportbuilder.GetNSSFReport();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "NSSF " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "NSSF " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowNSSF(_employer, app, nssfreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowNewNSSF(string app, List<NssfContributionsDTO> nssfcomputations)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;

                    int Period = DateTime.Now.Month;
                    int year = DateTime.Now.Year;

                    //create model
                    NSSFReportBuilder nssfreportbuilder = new NSSFReportBuilder(chkUseCurrentPayroll.Checked, Period, year, connection);
                    NSSFReportModel nssfreportmodel = nssfreportbuilder.GetNewNSSFReport();

                    DoPreProcess(this, null);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "NSSF " + "  " + Period.ToString() + "  " + year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "NSSF " + "  " + Period.ToString() + "  " + year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowNewNSSF(app, nssfreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(this, null);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowNetSalary(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {

                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    NetSalaryReportMaker netsalaryreportmaker = new NetSalaryReportMaker(_employer, chkUseCurrentPayroll.Checked, pay.Year, pay.Period, connection);
                    NetSalaryReportModel netsalaryreportmodel = netsalaryreportmaker.GetNetSalaryModel();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Net Salary " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Net Salary " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowNetSalary(app, netsalaryreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowNHIF(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {

                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    NHIFReportBuilder nhifreportbuilder = new NHIFReportBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    NHIFReportModel nhifreportmodel = nhifreportbuilder.GetNHIFReport();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "NHIF " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "NHIF " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowNHIF(app, nhifreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowLoanRepaymentSchedule(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {

                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    LoanRepaymentScheduleModelBuilder lrmbuilder = new LoanRepaymentScheduleModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay, connection);
                    LoanRepaymentScheduleModel loanrepaymentshedulemodel = lrmbuilder.Getloanrepaymentshedule();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Loan Repayments " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Loan Repayments " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowLoanRepaymentShedule(app, loanrepaymentshedulemodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowSaccoPaymentSchedule(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {

                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    SaccoPaymentScheduleModelBuilder spsmbuilder = new SaccoPaymentScheduleModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay, connection);
                    SaccoPaymentScheduleModel saccopaymentshedulemodel = spsmbuilder.Getsaccopaymentshedule();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Sacco Contributions " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Sacco Contributions  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowSaccoPaymentShedule(app, saccopaymentshedulemodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private StatementModel ProcessStatement(Employee emp, int Yr, int period, DAL.PayrollItem payrollitem)
        {
            try
            {
                if (dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    StatementModelBuilder sb = new StatementModelBuilder(_employer, chkUseCurrentPayroll.Checked, Yr, period, emp.Id, emp.EmpNo, payrollitem, connection);
                    StatementModel statement = sb.GetStatement();
                    return statement;
                }
                return null;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
                return null;
            }
        }
        private SheduleReportModel ProcessSchedule(DAL.Payroll payroll, DAL.PayrollItem payrollitem)
        {
            try
            {
                if (dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    SheduleModelBuilder sheduleMaker = new SheduleModelBuilder(_employer, chkUseCurrentPayroll.Checked, payroll, payrollitem, connection);
                    SheduleReportModel schedule = sheduleMaker.Getshedulemodel();
                    return schedule;
                }
                return null;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
                return null;
            }
        }
        private void ShowBankTransfer(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    BankTransferMaker bbtbuilder = new BankTransferMaker(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    BankTransferReportModel bbtreportmodel = bbtbuilder.GetBankTransferModelBuilder();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Bank Transfer  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Bank Transfer  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowBankTransfer(app, bbtreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }

        private void ShowBankBranchTransfer(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {

                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    BankBranchTransferModelBuilder bbtbuilder = new BankBranchTransferModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    BankTransferModel bbtreportmodel = bbtbuilder.GetBankBranchTransferModelBuilder();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Bank Branch Transfer " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Bank Branch Transfer  " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowBankBranchTransfer(app, bbtreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }

                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year ", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowEmployeeReport(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    EmployeesModelBuilder bbtbuilder = new EmployeesModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    EmployeesModelReport bbtreportmodel = bbtbuilder.GetEmployeesModel();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Employees " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Employees " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowEmployees(app, bbtreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowBankEmployeeReport(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    BankEmployeesModelBuilder bbtbuilder = new BankEmployeesModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    BankEmployeesModelReport bbtreportmodel = bbtbuilder.GetEmployeesModel();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Bank Employees " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Bank Employees " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowBankEmployees(app, bbtreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowCashEmployeeReport(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    CashEmployeesModelBuilder bbtbuilder = new CashEmployeesModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    CashEmployeesModelReport bbtreportmodel = bbtbuilder.GetEmployeesModel();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Cash Employees " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Cash Employees " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowCashEmployees(app, bbtreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowMpesaEmployeeReport(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    MpesaEmployeesModelBuilder bbtbuilder = new MpesaEmployeesModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    MpesaEmployeesModelReport bbtreportmodel = bbtbuilder.GetEmployeesModel();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Mpesa Employees " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Mpesa Employees " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowMpesaEmployees(app, bbtreportmodel, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ShowAdvanceSchedule(string app, object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    //create model
                    AdvanceModelBuilder advancebuilder = new AdvanceModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    AdvanceReportModel advancereport = advancebuilder.GetAdvanceModel();

                    DoPreProcess(sender, e);

                    if ("pdf".Equals(app.ToLower()))
                    {
                        current_file_name = "Advances " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".pdf";
                    }
                    else
                    {
                        current_file_name = "Advances " + pay.Period.ToString() + "  " + pay.Year.ToString() + "  " + _employer.Name + ".xlsx";
                    }

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("generating report [ " + current_file_name + " ]", TAG));

                    if (pdf_generator.ShowAdvanceSchedule(app, advancereport, get_reports_uri(current_file_name)))
                    {
                        DoShowPDF(current_file_name);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select  a Payroll Year and an Employer", "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
                Utils.ShowError(ex);
                return;
            }
        }
        private void ToggleExcelPDF()
        {
            if (showExcel)
            {
                showExcel = false;
                btnShowExcel.Checked = showExcel;
                btnShowPDF.Checked = !showExcel;
            }
            else
            {
                showExcel = true;
                btnShowExcel.Checked = showExcel;
                btnShowPDF.Checked = !showExcel;
            }
        }
        #endregion "custom"
        #region "combobox"
        private void cbYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbopayrollyears.SelectedItem != null && openPayrolls != null)
                {
                    string filter = "Year =" + cbopayrollyears.Text;
                    List<Payroll> filterd = (from l in openPayrolls
                                             where l.Year == (int)cbopayrollyears.SelectedItem
                                             select l).ToList();
                    bindingSourcePayroll.DataSource = filterd;
                    groupBox3.Text = bindingSourcePayroll.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void chkUseCurrentPayroll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshGrid();
                if (chkUseCurrentPayroll.Checked)
                {
                    chkUseCurrentPayroll.Text = "Using Current Payroll (click to use closed payrolls)";
                }
                else
                {
                    chkUseCurrentPayroll.Text = "Using Closed Payrolls (click to use current payroll)";
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }
        private void cboItemTypesReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (showExcel)
            {
                switch (cboItemTypesReports.Text)
                {
                    case "Payroll Master":
                        ShowPayrollMaster("excel", sender, e);
                        break;
                    case "NHIF":
                        ShowNHIF("excel", sender, e);
                        break;
                    case "NSSF":
                        ShowNSSF("excel", sender, e);
                        break;
                    case "PAYE":
                        ShowPAYE("excel", sender, e);
                        break;
                    case "Net Salary":
                        ShowNetSalary("excel", sender, e);
                        break;
                    case "Bank Transfer":
                        ShowBankTransfer("excel", sender, e);
                        break;
                    case "Bank Branch Transfer":
                        ShowBankBranchTransfer("excel", sender, e);
                        break;
                    case "Loan Repayments":
                        ShowLoanRepaymentSchedule("excel", sender, e);
                        break;
                    case "Sacco Contributions":
                        ShowSaccoPaymentSchedule("excel", sender, e);
                        break;
                    case "Advances":
                        ShowAdvanceSchedule("excel", sender, e);
                        break;
                    case "Employees":
                        ShowEmployeeReport("excel", sender, e);
                        break;
                    case "Bank Employees":
                        ShowBankEmployeeReport("excel", sender, e);
                        break;
                    case "Mpesa Employees":
                        ShowMpesaEmployeeReport("excel", sender, e);
                        break;
                    case "Cash Employees":
                        ShowCashEmployeeReport("excel", sender, e);
                        break;

                }
            }
            else
            {
                switch (cboItemTypesReports.Text)
                {
                    case "Payroll Master":
                        ShowPayrollMaster("pdf", sender, e);
                        break;
                    case "NHIF":
                        ShowNHIF("pdf", sender, e);
                        break;
                    case "NSSF":
                        ShowNSSF("pdf", sender, e);
                        break;
                    case "PAYE":
                        ShowPAYE("pdf", sender, e);
                        break;
                    case "Net Salary":
                        ShowNetSalary("pdf", sender, e);
                        break;
                    case "Bank Transfer":
                        ShowBankTransfer("pdf", sender, e);
                        break;
                    case "Bank Branch Transfer":
                        ShowBankBranchTransfer("pdf", sender, e);
                        break;
                    case "Loan Repayments":
                        ShowLoanRepaymentSchedule("pdf", sender, e);
                        break;
                    case "Sacco Contributions":
                        ShowSaccoPaymentSchedule("pdf", sender, e);
                        break;
                    case "Advances":
                        ShowAdvanceSchedule("pdf", sender, e);
                        break;
                    case "Employees":
                        ShowEmployeeReport("pdf", sender, e);
                        break;
                    case "Bank Employees":
                        ShowBankEmployeeReport("pdf", sender, e);
                        break;
                    case "Mpesa Employees":
                        ShowMpesaEmployeeReport("pdf", sender, e);
                        break;
                    case "Cash Employees":
                        ShowCashEmployeeReport("pdf", sender, e);
                        break;
                }
            }
        }
        #endregion "combobox"

        #endregion "Private Methods"



    }


    public class ReportsProcessCompleteEventArg : System.EventArgs
    {
        private int svalue;

        public ReportsProcessCompleteEventArg(int value)
        {
            svalue = value;
        }

        public int StatusValue
        {
            get { return svalue; }
        }
    }

}




