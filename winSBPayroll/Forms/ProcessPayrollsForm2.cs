using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.Payroll;
using BLL.DataEntry;
using CommonLib;
using DAL;
using System.Threading;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace winSBPayroll.Forms
{
    public partial class ProcessPayrollsForm2 : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string user;
        public string TAG;
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname_from_parent;
        IQueryable<DAL.Payroll> _Payrolls;
        int recordCount = 0;

        public ProcessPayrollsForm2(string _user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
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

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            user = _user;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished ProcessPayroll initialization", TAG));

        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.Write_To_Log_File_temp_dir(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.Write_To_Log_File_temp_dir(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
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

        private void ProcessPayroll_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPayrolls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewPayrolls.MultiSelect = false;
                dataGridViewPayrolls.ReadOnly = true;
                dataGridViewPayrolls.AutoGenerateColumns = false;

                var _employersquery = from ep in db.Employers
                                      where ep.IsActive == true
                                      where ep.IsDeleted == false
                                      select ep;

                List<DAL.Employer> _employers = _employersquery.ToList();

                DataGridViewComboBoxColumn colCboxEmployer = new DataGridViewComboBoxColumn();
                colCboxEmployer.HeaderText = "Employers";
                colCboxEmployer.Name = "colCboxEmployer";
                colCboxEmployer.DataSource = _employers;
                // The display member is the name column in the column datasource  
                colCboxEmployer.DisplayMember = "Name";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxEmployer.DataPropertyName = "EmployerId";
                // The value member is the primary key of the parent table  
                colCboxEmployer.ValueMember = "Id";
                colCboxEmployer.MaxDropDownItems = 10;
                colCboxEmployer.Width = 100;
                colCboxEmployer.DisplayIndex = 4;
                colCboxEmployer.MinimumWidth = 5;
                colCboxEmployer.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colCboxEmployer.FlatStyle = FlatStyle.Flat;
                colCboxEmployer.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployer.ReadOnly = true;

                if (!this.dataGridViewPayrolls.Columns.Contains("colCboxEmployer"))
                {
                    dataGridViewPayrolls.Columns.Add(colCboxEmployer);
                }

                List<DAL.Employer> _lst_employers = _employersquery.ToList();
                _lst_employers.Insert(0, new DAL.Employer { Id = 0, Name = "---Select---" });
                cboemployer.DisplayMember = "Name";
                cboemployer.ValueMember = "Id";
                cboemployer.DataSource = _lst_employers;
                cboemployer.SelectedIndex = 0;

                var _payroll_years_query = (from p in db.Payrolls
                                            orderby p.Year descending
                                            select p.Year).Distinct();
                _payroll_years_query = _payroll_years_query.OrderByDescending(i => i);
                List<int> _lst_payroll_years = _payroll_years_query.ToList();
                cbopayrollyears.DisplayMember = "Year";
                cbopayrollyears.ValueMember = "Year";
                cbopayrollyears.DataSource = _lst_payroll_years;
                cbopayrollyears.SelectedIndex = -1;

                RefreshGrid(1);

                chkisopen.Checked = true;

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished ProcessPayroll load", TAG));

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        int NO_OF_RECORDS_PER_PAGE = 5;
        int NO_OF_PAGES_TO_DISPLAY = 5;

        public void RefreshGrid(int pageIndex)
        {
            try
            {
                //NO_OF_RECORDS_PER_PAGE = int.Parse(rep.get_setting_value_given_key("NO_OF_RECORDS_PER_PAGE"));
                int no_of_records_per_page_from_db = rep.get_no_of_records_per_page();
                NO_OF_RECORDS_PER_PAGE = no_of_records_per_page_from_db;
                //NO_OF_PAGES_TO_DISPLAY = int.Parse(System.Configuration.ConfigurationManager.AppSettings["NO_OF_PAGES_TO_DISPLAY"]);
                int no_of_pages_to_display_from_db = rep.get_no_of_pages_to_display();
                NO_OF_PAGES_TO_DISPLAY = no_of_pages_to_display_from_db;

                string[] conn_str_arr = connection.Split(';');
                string data_source = string.Empty;
                string data_base = string.Empty;
                string integrated_security = string.Empty;

                for (int i = 0; i < conn_str_arr.Length; i++)
                {
                    if (i == 2)
                    {
                        string[] data_source_arr = conn_str_arr[2].Split('=');
                        data_source = data_source_arr[2];
                    }
                    if (i == 3)
                    {
                        string[] data_base_arr = conn_str_arr[3].Split('=');
                        data_base = data_base_arr[1];
                    }
                    if (i == 4)
                    {
                        string[] integrated_security_arr = conn_str_arr[4].Split('=');
                        integrated_security = integrated_security_arr[1];
                    }
                }

                string constring = @"Data Source=" + data_source + @";Initial Catalog=" + data_base + @";Integrated Security=" + integrated_security;

                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("GetPayrollsPageWise", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", NO_OF_RECORDS_PER_PAGE);

                        // Pass Filter Parameters from Form Controls                      
                        if (cboemployer.SelectedIndex > 0 && cboemployer.SelectedValue != null)
                        {
                            DAL.Employer employer = (DAL.Employer)cboemployer.SelectedItem;
                            cmd.Parameters.AddWithValue("@EmployerId", employer.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@EmployerId", DBNull.Value);
                        }

                        object payroll_year = DBNull.Value;

                        if (cbopayrollyears.SelectedIndex != -1 && cbopayrollyears.SelectedValue != null)
                        {
                            payroll_year = (object)(int)cbopayrollyears.SelectedItem ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@Year", payroll_year);

                        if (chkisopen.Checked)
                        {
                            cmd.Parameters.AddWithValue("@IsOpen", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IsOpen", DBNull.Value);
                        }

                        if (chkprocessed.Checked)
                        {
                            cmd.Parameters.AddWithValue("@Processed", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Processed", DBNull.Value);
                        }

                        // Output Parameter
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                        con.Open();
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        convert_datatable_into_list(dt);
                        BindGrid();
                        con.Close();
                        recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                        this.PopulatePager(recordCount, pageIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString()));
            }
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(NO_OF_RECORDS_PER_PAGE));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            if (pageCount < 1)
            {
                pageCount = 1;
            }

            // Center the visible window of page buttons around currentPage,
            // then clamp it so it never runs past 1 or pageCount.
            startIndex = currentPage - (NO_OF_PAGES_TO_DISPLAY / 2);
            if (startIndex < 1)
            {
                startIndex = 1;
            }

            endIndex = startIndex + NO_OF_PAGES_TO_DISPLAY - 1;
            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = endIndex - NO_OF_PAGES_TO_DISPLAY + 1;
                if (startIndex < 1)
                {
                    startIndex = 1;
                }
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            panelPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(38 * count, 5);
                btnPage.Size = new System.Drawing.Size(40, 20);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.BackColor = Color.LimeGreen;
                btnPage.ForeColor = Color.Black;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                panelPager.Controls.Add(btnPage);
                count++;

                if (page.Selected)
                {
                    btnPage.BackColor = Color.Magenta;
                    btnPage.ForeColor = Color.Black;
                }
            }
        }

        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            this.RefreshGrid(int.Parse(btnPager.Name));
        }

        private void convert_datatable_into_list(DataTable dt)
        {
            try
            {
                _Payrolls = (from DataRow dr in dt.Rows
                             select new Payroll()
                             {
                                 Period = dr["Period"] != DBNull.Value ? Convert.ToInt32(dr["Period"]) : 0,
                                 Year = dr["Year"] != DBNull.Value ? Convert.ToInt32(dr["Year"]) : 0,
                                 EmployerId = dr["EmployerId"] != DBNull.Value ? Convert.ToInt32(dr["EmployerId"]) : 0,
                                 DateRun = dr["DateRun"] != DBNull.Value ? Convert.ToDateTime(dr["DateRun"]) : DateTime.MinValue,
                                 TimeRun = dr["TimeRun"] != DBNull.Value ? dr["TimeRun"].ToString() : string.Empty,
                                 RunBy = dr["RunBy"] != DBNull.Value ? dr["RunBy"].ToString() : string.Empty,
                                 Approved = dr["Approved"] != DBNull.Value ? Convert.ToBoolean(dr["Approved"]) : false,
                                 ApprovedBy = dr["ApprovedBy"] != DBNull.Value ? dr["ApprovedBy"].ToString() : string.Empty,
                                 IsOpen = dr["IsOpen"] != DBNull.Value ? Convert.ToBoolean(dr["IsOpen"]) : false,
                                 Processed = dr["Processed"] != DBNull.Value ? Convert.ToBoolean(dr["Processed"]) : false
                             }).AsQueryable();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString()));
            }
        }

        private void BindGrid()
        {
            try
            {

                _Payrolls = _Payrolls.Where(p => p.IsOpen == true).OrderByDescending(i => i.Processed).ThenByDescending(i => i.DateRun);

                //set the datasource to null
                bindingSourcePayrolls.DataSource = null;
                //set the datasource to a method
                bindingSourcePayrolls.DataSource = _Payrolls;
                dataGridViewPayrolls.DataSource = bindingSourcePayrolls;
                groupBox3.Text = bindingSourcePayrolls.Count.ToString() + "/" + recordCount;
                //foreach (DataGridViewRow row in dataGridViewPayrolls.Rows)
                //{
                //    dataGridViewPayrolls.Rows[dataGridViewPayrolls.Rows.Count - 1].Selected = true;
                //    int nRowIndex = dataGridViewPayrolls.Rows.Count - 1;
                //    bindingSourcePayrolls.Position = nRowIndex;
                //}
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + _Payrolls.ToList().Count + " ] records.", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString()));
            }
        }

        /*To ensure full compatibility with .NET Framework 4.0, the solution replaces async/await (introduced in .NET 4.5) with standard .NET 4.0 asynchronous constructs: Task.Factory.StartNew, TaskScheduler.FromCurrentSynchronizationContext(), and Control.InvokeRequired / Invoke marshalling.
        Implementation Details
        * .NET 4.0 Async Pattern: Uses Task.Factory.StartNew for background processing without requiring .NET 4.5 async/await keywords.
        * UI Thread Marshalling: Captures the UI thread context via TaskScheduler.FromCurrentSynchronizationContext() or explicit this.Invoke() checks to update UI elements safely.
        * Controls Locking: Disables action controls before background execution and re-enables them upon completion.*/

        private void btnProcessPayroll_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayrolls.SelectedRows.Count == 0)
                return;

            try
            {
                DAL.Payroll pay = (DAL.Payroll)bindingSourcePayrolls.Current;

                int periodWc = 0;
                int yearWc = 0;
                int employeridWc = 0;

                DAL.Employer _employer = rep.GetEmployer(pay.EmployerId);

                if (_employer == null)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Error retrieving Employer info" + Environment.NewLine + "Contact Administrator", TAG));
                    MessageBox.Show("Error retrieving Employer info" + Environment.NewLine + "Contact Administrator", Utils.APP_NAME);
                    return;
                }

                bool shouldProcess = false;
                bool simulateRun = false;

                if (!de.Working_Copy_Not_Closed_for_employer(ref periodWc, ref yearWc, ref employeridWc))
                {
                    simulateRun = false;
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Processing Payroll for Period [{0}], Year [{1}], Employer [{2}]", pay.Period, pay.Year, _employer.Name), TAG));
                    shouldProcess = true;
                }
                else if (periodWc == pay.Period && yearWc == pay.Year && employeridWc == pay.EmployerId)
                {
                    if (DialogResult.Yes == MessageBox.Show("Payroll already processed! \nDo you wish to overwrite?", "Confirm Overwrite", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        simulateRun = true;
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Re-Processing Payroll for Period [{0}], Year [{1}], Employer [{2}]", pay.Period, pay.Year, _employer.Name), TAG));
                        shouldProcess = true;
                    }
                }
                else
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("You must close the previously Processed payroll before proceeding", TAG));
                    MessageBox.Show("You must close the previously Processed payroll before proceeding", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

                if (shouldProcess)
                {
                    SetControlsEnabled(false);

                    // Execute payroll process asynchronously in .NET 4.0
                    ProcessPayrollNowAsync(pay.Period, pay.Year, simulateRun, pay.EmployerId, pay);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
                SetControlsEnabled(true);
            }
        }

        private void ProcessPayrollNowAsync(int period, int year, bool simulateRun, int employerId, DAL.Payroll pay)
        {
            // Capture the UI thread scheduler for continuation tasks
            TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            // 1. Get employee count in background task
            Task.Factory.StartNew<int>(() =>
            {
                return rep.GetAllActiveEmployeesforEmployer(employerId).Count();
            })
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(task.Exception.InnerException.ToString(), TAG));
                    Utils.ShowError(task.Exception.InnerException);
                    SetControlsEnabled(true);
                    return;
                }

                int count = task.Result;
                progressbar.Minimum = 0;
                progressbar.Maximum = count;
                progressbar.Value = 0;
                listBoxResults.Items.Clear();

                if (count == 0)
                {
                    string msg = string.Format("No Employees to Process for Employer [ {0} ]", rep.GetEmployer(employerId).Name);
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));
                    Utils.ShowError(new Exception(msg));
                    SetControlsEnabled(true);
                    return;
                }

                string startMsg = string.Format("Processing [ {0} ] Employees for Employer [ {1} ]", count, rep.GetEmployer(employerId).Name);
                NotifyMessage("PAYROLL ENGINE RUNNING...", startMsg);
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("PAYROLL ENGINE RUNNING...", TAG));
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(startMsg, TAG));

                // 2. Execute long-running engine execution on background thread
                Task.Factory.StartNew(() =>
                {
                    CPayroll2 cp = new CPayroll2(user, connection, _notificationmessageEventname);
                    cp.OnCompleteGeneratePayslip += cp_OnCompleteGeneratePayslip;

                    string sError = string.Empty;
                    cp.RunPayroll(simulateRun, period, year, ref sError, employerId);
                    rep.MarkPayroll_for_employer_AsProcessed(period, year, employerId);

                    pay.DateRun = DateTime.Now.Date;
                    pay.TimeRun = DateTime.Now.ToString("HH:mm:ss");
                    pay.RunBy = user;
                    rep.UpdatePayroll_for_employer_DateRun(pay);

                    return sError;
                })
                .ContinueWith(engineTask =>
                {
                    try
                    {
                        if (engineTask.IsFaulted)
                        {
                            Exception ex = engineTask.Exception.InnerException;
                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                            Utils.ShowError(ex);
                        }
                        else
                        {
                            string sError = engineTask.Result;
                            if (!string.IsNullOrEmpty(sError))
                            {
                                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(sError, TAG));
                                Log.WriteToErrorLogFile_and_EventViewer(new Exception(sError));
                            }

                            MessageBox.Show("Successfully run", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    finally
                    {
                        SetControlsEnabled(true);
                        RefreshGrid(1);
                    }
                }, uiScheduler); // Ensures continuation runs back on the UI Thread

            }, uiScheduler);
        }

        // Thread-safe event handler compatible with .NET 4.0
        private void cp_OnCompleteGeneratePayslip(object sender, PayslipCompleteEventArg e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new CPayroll2.PayslipCompleteEventHandler(cp_OnCompleteGeneratePayslip), sender, e);
                return;
            }

            try
            {
                if (e.StatusValue >= progressbar.Minimum && e.StatusValue <= progressbar.Maximum)
                {
                    progressbar.Value = e.StatusValue;
                }

                if (!string.IsNullOrEmpty(e.ErrorMsg))
                {
                    listBoxResults.Items.Add(e.ErrorMsg);
                }

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(e._Template + Environment.NewLine + e.ErrorMsg, TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetControlsEnabled), enabled);
                return;
            }

            btnProcessPayroll.Enabled = enabled;
            btnClosePayroll.Enabled = enabled;
            btnClose.Enabled = enabled;
            cboemployer.Enabled = enabled;
            cbopayrollyears.Enabled = enabled;
            dataGridViewPayrolls.Enabled = enabled;
        }

        private bool PayrollNotClosed(int period, int year)
        {
            bool ret = true;

            return ret;
        }

        public bool NotifyMessage(string _Title, string _Text)
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClosePayroll_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayrolls.SelectedRows.Count != 0)
                {
                    DAL.Payroll pay = (DAL.Payroll)bindingSourcePayrolls.Current;
                    if (pay.Processed)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Close this Payroll?", "Confirm Close", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            // Todo close the payroll here
                            CPayroll cp = new CPayroll(user, connection, _notificationmessageEventname);
                            cp.ArchivePayroll(pay.Period, pay.Year, pay.EmployerId);

                            // Todo code for removing record from the payslipmaster
                            RefreshGrid(1);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You are not allowed to close a non Processed Payroll!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewPayrolls_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteToErrorLogFile(ex);
            }
        }

        private void cboemployer_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void cbopayrollyears_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void chkprocessed_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void chkisopen_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void btnclearfilter_Click(object sender, EventArgs e)
        {
            cboemployer.SelectedIndex = 0;
            cbopayrollyears.SelectedIndex = -1;
            chkisopen.Checked = false;
            chkprocessed.Checked = false;
        }









    }
}






