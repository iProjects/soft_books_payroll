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
    public partial class EmployerForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;
        public string TAG;
        //Event declaration:
        //event for publishing messages to output
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        IQueryable<DAL.Employer> _Employers;
        int recordCount = 0;

        public EmployerForm(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _user = user;

            TAG = this.GetType().Name;

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished EmployerForm initialization", TAG));

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

        private void EmployerForm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewEmployer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewEmployer.MultiSelect = false;
                dataGridViewEmployer.ReadOnly = true;
                dataGridViewEmployer.AutoGenerateColumns = false;

                RefreshGrid(1);

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished EmployerForm load", TAG));

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
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
                    using (SqlCommand cmd = new SqlCommand("GetEmployersPageWise", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", NO_OF_RECORDS_PER_PAGE);

                        // Pass Filter Parameters from Form Controls
                        cmd.Parameters.AddWithValue("@Name", string.IsNullOrWhiteSpace(txt_name.Text) ? (object)DBNull.Value : txt_name.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(txt_email.Text) ? (object)DBNull.Value : txt_email.Text.Trim());
                        cmd.Parameters.AddWithValue("@Telephone", string.IsNullOrWhiteSpace(txt_telephone.Text) ? (object)DBNull.Value : txt_telephone.Text.Trim());

                        if (chkInActive.Checked)
                        {
                            cmd.Parameters.AddWithValue("@IsActive", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IsActive", DBNull.Value);
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
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
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
                _Employers = (from DataRow dr in dt.Rows
                              select new Employer()
                              {
                                  Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0,
                                  Name = dr["Name"] != DBNull.Value ? dr["Name"].ToString() : string.Empty,
                                  Email = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : string.Empty,
                                  Telephone = dr["Telephone"] != DBNull.Value ? dr["Telephone"].ToString() : string.Empty,
                                  Address1 = dr["Address1"] != DBNull.Value ? dr["Address1"].ToString() : string.Empty,
                                  Address2 = dr["Address2"] != DBNull.Value ? dr["Address2"].ToString() : string.Empty,
                                  Slogan = dr["Slogan"] != DBNull.Value ? dr["Slogan"].ToString() : string.Empty,
                                  AuthorizedSignatory = dr["AuthorizedSignatory"] != DBNull.Value ? dr["AuthorizedSignatory"].ToString() : string.Empty,
                                  PIN = dr["PIN"] != DBNull.Value ? dr["PIN"].ToString() : string.Empty,
                                  NHIF = dr["NHIF"] != DBNull.Value ? dr["NHIF"].ToString() : string.Empty,
                                  NSSF = dr["NSSF"] != DBNull.Value ? dr["NSSF"].ToString() : string.Empty,
                                  NoOfEmployees = dr["NoOfEmployees"] != DBNull.Value ? Convert.ToInt32(dr["NoOfEmployees"]) : 0,
                                  BankBranchSortCode = dr["BankBranchSortCode"] != DBNull.Value ? dr["BankBranchSortCode"].ToString() : string.Empty,
                                  AccountName = dr["AccountName"] != DBNull.Value ? dr["AccountName"].ToString() : string.Empty,
                                  AccountNo = dr["AccountNo"] != DBNull.Value ? dr["AccountNo"].ToString() : string.Empty,
                                  Logo = dr["Logo"] != DBNull.Value ? dr["Logo"].ToString() : string.Empty,
                                  IsDefault = dr["IsDefault"] != DBNull.Value ? Convert.ToBoolean(dr["IsDefault"]) : false,
                                  IsActive = dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : false,
                                  IsDeleted = dr["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(dr["IsDeleted"]) : false

                              }).AsQueryable();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        private void BindGrid()
        {
            try
            {
                //get_no_of_employees();

                //set the datasource to null
                bindingSourceEmployer.DataSource = null;
                //set the datasource to a method
                bindingSourceEmployer.DataSource = _Employers;
                dataGridViewEmployer.DataSource = bindingSourceEmployer;
                groupBox4.Text = bindingSourceEmployer.Count.ToString() + "/" + recordCount;
                foreach (DataGridViewRow row in dataGridViewEmployer.Rows)
                {
                    dataGridViewEmployer.Rows[dataGridViewEmployer.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridViewEmployer.Rows.Count - 1;
                    bindingSourceEmployer.Position = nRowIndex;
                }
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + _Employers.ToList().Count + " ] records.", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        void get_no_of_employees()
        {
            foreach (var emp in _Employers)
            {
                var employee_count = rep.GetAllActiveEmployeesforEmployer(emp.Id).Count;
                emp.NoOfEmployees = employee_count;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AddEmployer ae = new Forms.AddEmployer(connection, _notificationmessageEventname) { Owner = this };
                ae.CheckForDefaultBank();
                ae.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEmployer.SelectedRows.Count != 0)
                {
                    DAL.Employer emp = (DAL.Employer)bindingSourceEmployer.Current;
                    Forms.EditEmployer f = new EditEmployer(emp, connection, _notificationmessageEventname) { Owner = this };
                    f.Text = emp.Name.ToString().Trim().ToUpper();
                    f.CheckForDefaultBank();
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewEmployer_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewEmployer.SelectedRows.Count != 0)
                {
                    DAL.Employer emp = (DAL.Employer)bindingSourceEmployer.Current;

                    Forms.EditEmployer f = new EditEmployer(emp, connection, _notificationmessageEventname) { Owner = this };
                    f.Text = emp.Name.ToString().Trim().ToUpper();
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEmployer.SelectedRows.Count != 0)
                {
                    DAL.Employer emp = (DAL.Employer)bindingSourceEmployer.Current;

                    var _Employeesquery = from em in rep.GetAllActiveEmployees()
                                          where em.EmployerId == emp.Id
                                          where em.IsDeleted == false
                                          select em;
                    List<Employee> _Employees = _Employeesquery.ToList();

                    var _EmployerBanksquery = from em in db.EmployerBanks
                                              select em;
                    List<EmployerBank> _EmployerBanks = _EmployerBanksquery.ToList();

                    var _payrolls_query = db.Payrolls.Where(p => p.EmployerId == emp.Id).ToList();

                    List<DAL.Payroll> openPayrolls = _payrolls_query.ToList();

                    if (_Employees.Count > 0)
                    {
                        MessageBox.Show("There is an Employee Associated with this Employer.\n Delete the Employee First!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (_EmployerBanks.Count > 0)
                    {
                        MessageBox.Show("There is an Employer Bank Associated with this Employer.\n Delete the Employer Bank First!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (openPayrolls.Count > 0)
                    {
                        MessageBox.Show("There is a Payroll Associated with this Employer.\n Delete the Payroll First!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Employer\n" + emp.Name.Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            rep.DeleteEmployer(emp);
                            RefreshGrid(1);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void chkInActive_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void btnclearfilter_Click(object sender, EventArgs e)
        {
            txt_name.Text = string.Empty;
            txt_email.Text = string.Empty;
            txt_telephone.Text = string.Empty;
        }

        private void dataGridViewEmployer_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void txt_email_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void txt_telephone_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }












    }
}