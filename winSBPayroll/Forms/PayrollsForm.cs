using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;
using System.Threading;
using System.Data.SqlClient;

namespace winSBPayroll.Forms
{
    public partial class PayrollsForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string user;
        public string TAG;
        //Event declaration:
        //event for publishing messages to output
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        IQueryable<DAL.Payroll> _Payrolls;
        int recordCount = 0;

        public PayrollsForm(string _user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            TAG = this.GetType().Name;

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname = notificationmessageEventname;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("Conn");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            user = _user;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished Payrolls initialization", TAG));

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

        private void Payrolls_Load(object sender, EventArgs e)
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
                colCboxEmployer.DisplayIndex = 7;
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
                chkprocessed.Checked = false;

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished Payrolls load", TAG));

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString()));
            }
        }

        public void refresh_years()
        {
            var _payroll_years_query = (from p in db.Payrolls
                                        orderby p.Year descending
                                        select p.Year).Distinct();
            _payroll_years_query = _payroll_years_query.OrderByDescending(i => i);
            List<int> _lst_payroll_years = _payroll_years_query.ToList();
            cbopayrollyears.DisplayMember = "Year";
            cbopayrollyears.ValueMember = "Year";
            cbopayrollyears.DataSource = _lst_payroll_years;

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

                //// 2. Execute Stored Procedure directly through Entity Framework Context
                //List<DAL.Employee> resultList = db.GetPayrollsPageWise( 
                //    pageIndexParam,
                //    pageSizeParam,
                //    empNoParam,
                //    surnameParam,
                //    otherNamesParam,
                //    departmentParam,
                //    employerIdParam,
                //    isActiveParam,
                //    recordCountParam
                //).ToList();

                //// 3. Assign directly to queryable datasource (bypassing manual DataTable conversion)
                //_Employees = resultList.AsQueryable();

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
                //set the datasource to null
                bindingSourcePayrolls.DataSource = null;
                //set the datasource to a method
                bindingSourcePayrolls.DataSource = _Payrolls;
                dataGridViewPayrolls.DataSource = bindingSourcePayrolls;
                groupBox2.Text = bindingSourcePayrolls.Count.ToString() + "/" + recordCount;
                foreach (DataGridViewRow row in dataGridViewPayrolls.Rows)
                {
                    dataGridViewPayrolls.Rows[dataGridViewPayrolls.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridViewPayrolls.Rows.Count - 1;
                    bindingSourcePayrolls.Position = nRowIndex;
                }
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + _Payrolls.ToList().Count + " ] records.", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString()));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddPayroll f = new AddPayroll(user, connection, _notificationmessageEventname) { Owner = this };
            f.ShowDialog();
        }

        private void cbYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void cboemployer_SelectedIndexChanged(object sender, EventArgs e)
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
            try
            {
                cboemployer.SelectedIndex = 0;
                cbopayrollyears.SelectedIndex = -1;
                chkisopen.Checked = false;
                chkprocessed.Checked = false;

                RefreshGrid(1);

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString()));
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
                Log.Write_To_Log_File_temp_dir(ex);
            }
        }

        private void refresh_payroll_years()
        {
            try
            {
                chkisopen.Checked = false;
                chkprocessed.Checked = false;
                cboemployer.SelectedIndex = 0;
                cbopayrollyears.SelectedIndex = -1;

                RefreshGrid(1);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString()));
            }
        }

    }
}