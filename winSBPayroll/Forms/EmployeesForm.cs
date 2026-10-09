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
    public partial class EmployeesForm : Form
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
        IQueryable<DAL.Employee> _Employees;
        int recordCount = 0;

        public EmployeesForm(string _user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished Employees initialization", TAG));

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

        private void Employees_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewEmployees.MultiSelect = false;
                dataGridViewEmployees.ReadOnly = true;
                dataGridViewEmployees.AutoGenerateColumns = false;

                progressBar_update_default_payroll_items.Visible = false;

                var _employersquery = from ep in db.Employers
                                      where ep.IsActive == true
                                      where ep.IsDeleted == false
                                      select ep;
                List<DAL.Employer> _employers = _employersquery.ToList();
                _employers.Insert(0, new DAL.Employer { Id = 0, Name = "---Select---" });
                cboEmployer.DataSource = _employers;
                cboEmployer.DisplayMember = "Name";
                cboEmployer.ValueMember = "Id";
                cboEmployer.SelectedIndex = 0;

                DataGridViewComboBoxColumn colCboxEmployer = new DataGridViewComboBoxColumn();
                colCboxEmployer.HeaderText = "Employer";
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
                colCboxEmployer.DisplayIndex = 6;
                colCboxEmployer.MinimumWidth = 5;
                colCboxEmployer.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colCboxEmployer.FlatStyle = FlatStyle.Flat;
                colCboxEmployer.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployer.ReadOnly = true;
                if (!this.dataGridViewEmployees.Columns.Contains("cbEmployer"))
                {
                    dataGridViewEmployees.Columns.Add(colCboxEmployer);
                }

                var _departmentsquery = from dp in db.Departments
                                        where dp.IsDeleted == false
                                        select dp;
                List<Department> _Departments = _departmentsquery.ToList();
                DataGridViewComboBoxColumn colCboxDepartment = new DataGridViewComboBoxColumn();
                colCboxDepartment.HeaderText = "Department";
                colCboxDepartment.Name = "cbDepartment";
                colCboxDepartment.DataSource = _Departments;
                // The display member is the name column in the column datasource  
                colCboxDepartment.DisplayMember = "Description";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxDepartment.DataPropertyName = "DepartmentId";
                // The value member is the primary key of the parent table  
                colCboxDepartment.ValueMember = "Id";
                colCboxDepartment.MaxDropDownItems = 10;
                colCboxDepartment.Width = 100;
                colCboxDepartment.DisplayIndex = 5;
                colCboxDepartment.MinimumWidth = 5;
                colCboxDepartment.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colCboxDepartment.FlatStyle = FlatStyle.Flat;
                colCboxDepartment.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxDepartment.ReadOnly = true;
                if (!this.dataGridViewEmployees.Columns.Contains("cbDepartment"))
                {
                    dataGridViewEmployees.Columns.Add(colCboxDepartment);
                }

                var gender = new BindingList<KeyValuePair<string, string>>();
                gender.Add(new KeyValuePair<string, string>("M", "Male"));
                gender.Add(new KeyValuePair<string, string>("F", "Female"));
                DataGridViewComboBoxColumn colCboxGender = new DataGridViewComboBoxColumn();
                colCboxGender.HeaderText = "Gender";
                colCboxGender.Name = "cbGender";
                colCboxGender.DataSource = gender;
                // The display member is the name column in the column datasource  
                colCboxGender.DisplayMember = "Value";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxGender.DataPropertyName = "Gender";
                // The value member is the primary key of the parent table  
                colCboxGender.ValueMember = "Key";
                colCboxGender.MaxDropDownItems = 10;
                colCboxGender.Width = 100;
                colCboxGender.DisplayIndex = 4;
                colCboxGender.MinimumWidth = 5;
                colCboxGender.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colCboxGender.FlatStyle = FlatStyle.Flat;
                colCboxGender.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxGender.ReadOnly = true;
                if (!this.dataGridViewEmployees.Columns.Contains("cbGender"))
                {
                    dataGridViewEmployees.Columns.Add(colCboxGender);
                }

                this.RefreshGrid(1);

                AutoCompleteStringCollection acscEmpNo = new AutoCompleteStringCollection();
                acscEmpNo.AddRange(this.AutoComplete_EmpNos());
                txtEmpNo.AutoCompleteCustomSource = acscEmpNo;
                txtEmpNo.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtEmpNo.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscSurName = new AutoCompleteStringCollection();
                acscSurName.AddRange(this.AutoComplete_SurNames());
                txtSurName.AutoCompleteCustomSource = acscSurName;
                txtSurName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtSurName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscOtherNames = new AutoCompleteStringCollection();
                acscOtherNames.AddRange(this.AutoComplete_OtherNames());
                txtOtherNames.AutoCompleteCustomSource = acscOtherNames;
                txtOtherNames.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtOtherNames.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscDeprtments = new AutoCompleteStringCollection();
                acscDeprtments.AddRange(this.AutoComplete_Deprtments());
                txtDepartment.AutoCompleteCustomSource = acscDeprtments;
                txtDepartment.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtDepartment.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished Employees load", TAG));

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }
        private string[] AutoComplete_EmpNos()
        {
            try
            {
                var empNosquery = from en in rep.GetAllActiveEmployees()
                                  where en.IsDeleted == false
                                  where en.IsActive == true
                                  select en.EmpNo;
                return empNosquery.ToArray();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                return null;
            }
        }
        private string[] AutoComplete_SurNames()
        {
            try
            {
                var surNamesquery = from en in rep.GetAllActiveEmployees()
                                    where en.IsDeleted == false
                                    where en.IsActive == true
                                    select en.Surname;
                return surNamesquery.ToArray();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                return null;
            }
        }
        private string[] AutoComplete_OtherNames()
        {
            try
            {
                var otherNamesquery = from en in rep.GetAllActiveEmployees()
                                      where en.IsDeleted == false
                                      where en.IsActive == true
                                      select en.OtherNames;
                return otherNamesquery.ToArray();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                return null;
            }
        }
        private string[] AutoComplete_Deprtments()
        {
            try
            {
                var departmentsquery = from dp in db.Departments
                                       where dp.IsDeleted == false
                                       select dp.Description;
                return departmentsquery.ToArray();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                return null;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AddEmployee f = new AddEmployee(user, connection, _notificationmessageEventname) { Owner = this };
                f.ShowDialog();
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
                    using (SqlCommand cmd = new SqlCommand("GetEmployeesPageWise", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", NO_OF_RECORDS_PER_PAGE);

                        // Pass Filter Parameters from Form Controls
                        cmd.Parameters.AddWithValue("@EmpNo", string.IsNullOrWhiteSpace(txtEmpNo.Text) ? (object)DBNull.Value : txtEmpNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@Surname", string.IsNullOrWhiteSpace(txtSurName.Text) ? (object)DBNull.Value : txtSurName.Text.Trim());
                        cmd.Parameters.AddWithValue("@OtherNames", string.IsNullOrWhiteSpace(txtOtherNames.Text) ? (object)DBNull.Value : txtOtherNames.Text.Trim());
                        cmd.Parameters.AddWithValue("@Department", string.IsNullOrWhiteSpace(txtDepartment.Text) ? (object)DBNull.Value : txtDepartment.Text.Trim());

                        if (cboEmployer.SelectedIndex > 0 && cboEmployer.SelectedValue != null)
                        {
                            DAL.Employer employer = (DAL.Employer)cboEmployer.SelectedItem;
                            cmd.Parameters.AddWithValue("@EmployerId", employer.Id);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@EmployerId", DBNull.Value);
                        }

                        // If chkInActive is checked, pull inactive employees; otherwise pull active employees
                        cmd.Parameters.AddWithValue("@IsActive", chkInActive.Checked ? false : true);

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
                _Employees = (from DataRow dr in dt.Rows                              
                              select new Employee()
                              {
                                  Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0,
                                  EmpNo = dr["EmpNo"] != DBNull.Value ? dr["EmpNo"].ToString() : string.Empty,
                                  Surname = dr["Surname"] != DBNull.Value ? dr["Surname"].ToString() : string.Empty,
                                  OtherNames = dr["OtherNames"] != DBNull.Value ? dr["OtherNames"].ToString() : string.Empty,
                                  Email = dr["Email"] != DBNull.Value ? dr["Email"].ToString() : string.Empty,
                                  DoB = dr["DoB"] != DBNull.Value ? Convert.ToDateTime(dr["DoB"]) : DateTime.MinValue,
                                  MaritalStatus = dr["MaritalStatus"] != DBNull.Value ? dr["MaritalStatus"].ToString() : string.Empty,
                                  Gender = dr["Gender"] != DBNull.Value ? dr["Gender"].ToString() : string.Empty,
                                  PhoneNo = dr["PhoneNo"] != DBNull.Value ? dr["PhoneNo"].ToString() : string.Empty,
                                  Photo = dr["Photo"] != DBNull.Value ? dr["Photo"].ToString() : string.Empty,
                                  DoE = dr["DoE"] != DBNull.Value ? Convert.ToDateTime(dr["DoE"]) : DateTime.MinValue,
                                  BasicComputation = dr["BasicComputation"] != DBNull.Value ? dr["BasicComputation"].ToString() : string.Empty,
                                  BasicPay = dr["BasicPay"] != DBNull.Value ? Convert.ToDecimal(dr["BasicPay"]) : 0m,
                                  PersonalRelief = dr["PersonalRelief"] != DBNull.Value ? Convert.ToDecimal(dr["PersonalRelief"]) : 0m,
                                  MortgageRelief = dr["MortgageRelief"] != DBNull.Value ? Convert.ToDecimal(dr["MortgageRelief"]) : 0m,
                                  InsuranceRelief = dr["InsuranceRelief"] != DBNull.Value ? Convert.ToDecimal(dr["InsuranceRelief"]) : 0m,
                                  NSSFNo = dr["NSSFNo"] != DBNull.Value ? dr["NSSFNo"].ToString() : string.Empty,
                                  NHIFNo = dr["NHIFNo"] != DBNull.Value ? dr["NHIFNo"].ToString() : string.Empty,
                                  IDNo = dr["IDNo"] != DBNull.Value ? dr["IDNo"].ToString() : string.Empty,
                                  PINNo = dr["PINNo"] != DBNull.Value ? dr["PINNo"].ToString() : string.Empty,
                                  DepartmentId = dr["DepartmentId"] != DBNull.Value ? Convert.ToInt32(dr["DepartmentId"]) : 0,
                                  EmployerId = dr["EmployerId"] != DBNull.Value ? Convert.ToInt32(dr["EmployerId"]) : 0,
                                  PayPoint = dr["PayPoint"] != DBNull.Value ? dr["PayPoint"].ToString() : string.Empty,
                                  EmpGroup = dr["EmpGroup"] != DBNull.Value ? dr["EmpGroup"].ToString() : string.Empty,
                                  EmpPayroll = dr["EmpPayroll"] != DBNull.Value ? dr["EmpPayroll"].ToString() : string.Empty,
                                  PrevEmployer = dr["PrevEmployer"] != DBNull.Value ? dr["PrevEmployer"].ToString() : string.Empty,
                                  DateLeft = dr["DateLeft"] != DBNull.Value ? Convert.ToDateTime(dr["DateLeft"]) : DateTime.MinValue,
                                  PaymentMode = dr["PaymentMode"] != DBNull.Value ? dr["PaymentMode"].ToString() : string.Empty,
                                  TelephoneNo = dr["TelephoneNo"] != DBNull.Value ? dr["TelephoneNo"].ToString() : string.Empty,
                                  ChequeNo = dr["ChequeNo"] != DBNull.Value ? dr["ChequeNo"].ToString() : string.Empty,
                                  BankCode = dr["BankCode"] != DBNull.Value ? dr["BankCode"].ToString() : string.Empty,
                                  BankAccount = dr["BankAccount"] != DBNull.Value ? dr["BankAccount"].ToString() : string.Empty,
                                  LeaveBalance = dr["LeaveBalance"] != DBNull.Value ? dr["LeaveBalance"].ToString() : string.Empty,
                                  IsActive = dr["IsActive"] != DBNull.Value ? Convert.ToBoolean(dr["IsActive"]) : false,
                                  CreatedBy = dr["CreatedBy"] != DBNull.Value ? dr["CreatedBy"].ToString() : string.Empty,
                                  CreatedOn = dr["CreatedOn"] != DBNull.Value ? Convert.ToDateTime(dr["CreatedOn"]) : DateTime.MinValue,
                                  IsDeleted = dr["IsDeleted"] != DBNull.Value ? Convert.ToBoolean(dr["IsDeleted"]) : false,
                                  SystemId = dr["SystemId"] != DBNull.Value ? dr["SystemId"].ToString() : string.Empty

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
                //set the datasource to null
                bindingSourceEmployees.DataSource = null;

                _Employees = _Employees.OrderByDescending(i=>i.EmpNo);

                //set the datasource to a method
                bindingSourceEmployees.DataSource = _Employees;
                dataGridViewEmployees.DataSource = bindingSourceEmployees;
                groupBox2.Text = bindingSourceEmployees.Count.ToString() + "/" + recordCount;
                //foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
                //{
                //    dataGridViewEmployees.Rows[dataGridViewEmployees.Rows.Count - 1].Selected = true;
                //    int nRowIndex = dataGridViewEmployees.Rows.Count - 1;
                //    bindingSourceEmployees.Position = nRowIndex;
                //}
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + _Employees.ToList().Count + " ] records.", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        private void txtEmpNo_Validated(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }
        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.RefreshGrid(1);
                e.Handled = true;
            }
        }
        private void txtSurName_Validated(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }
        private void txtSurName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.RefreshGrid(1);
                e.Handled = true;
            }
        }
        private void txtOtherNames_Validated(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }
        private void txtOtherNames_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.RefreshGrid(1);
                e.Handled = true;
            }
        }
        private void txtDepartment_Validated(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }
        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.RefreshGrid(1);
                e.Handled = true;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEmployees.SelectedRows.Count != 0)
                {
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;

                    Forms.EditEmployee f = new EditEmployee(emp, user, connection, _notificationmessageEventname);
                    f.Text = emp.Surname.ToString().Trim().ToUpper() + "     " + emp.OtherNames.ToString().Trim().ToUpper();
                    f.Owner = this;
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEmployees.SelectedRows.Count != 0)
                {
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;

                    Forms.EditEmployee f = new EditEmployee(emp, user, connection, _notificationmessageEventname) { Owner = this };
                    f.DisableControls();
                    f.Text = emp.Surname.ToString().Trim().ToUpper() + "     " + emp.OtherNames.ToString().Trim().ToUpper();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEmployees.SelectedRows.Count != 0)
                {
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;

                    var _EmployeesWithPayslipDetquery = from br in rep.GetEmployeesWithPayslipDet()
                                                        select br;
                    List<DAL.Employee> _EmployeesWithPayslipDet = _EmployeesWithPayslipDetquery.ToList();

                    var _EmployeesWithPayslipDet_Tempquery = from br in rep.GetEmployeesWithPayslipDet_Temp()
                                                             select br;
                    List<DAL.Employee> _EmployeesWithPayslipDet_Temp = _EmployeesWithPayslipDet_Tempquery.ToList();

                    var _EmployeesWithPayslipMasterquery = from br in rep.GetEmployeesWithPayslipMaster()
                                                           select br;
                    List<DAL.Employee> _EmployeesWithPayslipMaster = _EmployeesWithPayslipMasterquery.ToList();

                    var _EmployeesWithPayslipMaster_Tempquery = from br in rep.GetEmployeesWithPayslipMaster_Temp()
                                                                select br;
                    List<DAL.Employee> _EmployeesWithPayslipMaster_Temp = _EmployeesWithPayslipMaster_Tempquery.ToList();

                    if (_EmployeesWithPayslipDet.Count > 0)
                    {
                        MessageBox.Show("There is a Processed Payroll Associated with this Employee!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (_EmployeesWithPayslipDet_Temp.Count > 0)
                    {
                        MessageBox.Show("There is a Processed Payroll Associated with this Employee!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (_EmployeesWithPayslipMaster.Count > 0)
                    {
                        MessageBox.Show("There is a Processed Payroll Associated with this Employee!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (_EmployeesWithPayslipMaster_Temp.Count > 0)
                    {
                        MessageBox.Show("There is a Processed Payroll Associated with this Employee!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete employee\n" + emp.Surname.ToString().Trim().ToUpper() + "  " + emp.OtherNames.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            de.DeleteEmployee(emp.EmpNo);

                            this.RefreshGrid(1);

                        }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }
        private void dataGridViewEmployees_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewEmployees.SelectedRows.Count != 0)
                {
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;

                    Forms.EditEmployee f = new EditEmployee(emp, user, connection, _notificationmessageEventname);
                    f.Text = emp.Surname.ToString().Trim().ToUpper() + "     " + emp.OtherNames.ToString().Trim().ToUpper();
                    f.Owner = this;
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }
        private void dataGridViewEmployees_DataError(object sender, DataGridViewDataErrorEventArgs e)
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
        private void chkInActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshGrid(1);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        private void btnuploademployees_Click(object sender, EventArgs e)
        {
            Forms.UploadEmployeesForm u = new Forms.UploadEmployeesForm(user, connection, _notificationmessageEventname) { Owner = this };
            u.Show();
        }

        private void btndownload_employees_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Title = "Select an excel file";
                //openFileDialog1.FileName = "";
                //"Text files (*.txt)|*.txt|All files (*.*)|*.*"
                saveFileDialog.Filter = "Excel Files|*.xls;*.xlsx";

                DialogResult result = saveFileDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string strFileName = saveFileDialog.FileName;

                    // use bulkcopy method of upload

                    //clear or backup the destination
                    Download(strFileName, user);

                    Utils.ShowInfo("Download completed successfully");
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }

        private void Download(string strFileName, string User)
        {
            Reports.Excel.CreateExcelDoc excell_app = new Reports.Excel.CreateExcelDoc();

            //creates the main header
            //createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor)
            excell_app.createHeaders(1, 1, "Email", "A1", "A1", 0, "WHITE", true, 10, "n");
            //creates subheaders
            excell_app.createHeaders(1, 2, "EmpNo", "A2", "A2", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 3, "Surname", "A3", "A3", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 4, "OtherNames", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 5, "DoB", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 6, "MaritalStatus", "A2", "A2", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 7, "Gender", "A3", "A3", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 8, "DoE", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 9, "IDNo", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 10, "BasicComputation", "A2", "A2", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 11, "BasicPay", "A3", "A3", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 12, "PersonalRelief", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 13, "MortgageRelief", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 14, "InsuranceRelief", "A2", "A2", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 15, "NSSFNo", "A3", "A3", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 16, "NHIFNo", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 17, "IDNo", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 18, "PINNo", "A3", "A3", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 19, "DepartmentId", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 20, "EmployerId", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 22, "PayPoint", "A2", "A2", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 23, "EmpGroup", "A3", "A3", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 24, "EmpPayroll", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 25, "PrevEmployer", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 26, "DateLeft", "A3", "A3", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 27, "PaymentMode", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 28, "TelephoneNo", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 29, "ChequeNo", "A2", "A2", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 30, "BankCode", "A3", "A3", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 31, "BankAccount", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 32, "LeaveBalance", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 33, "IsActive", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 34, "CreatedBy", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 35, "CreatedOn", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 36, "IsDeleted", "A4", "A4", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 37, "SystemId", "A4", "A4", 0, "WHITE", true, 10, "n");

            int row = 2;
            foreach (var rec in get_all_employees_for_download())
            {
                //add Data to to cells
                int col = 1;
                string addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Id.ToString(), addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.Email, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.FromAmt, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.EmpNo, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.ToAmt, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.Surname, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.ToAmt, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.OtherNames, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.DoB, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.MaritalStatus, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.Gender, addr, addr, 0, "WHITE", true, 10, "n");

                //col++;
                //addr = excell_app.IntAlpha(col) + row;
                ////excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                //excell_app.createHeaders(row, col, rec.Photo, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.DoE, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.BasicComputation, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.BasicPay, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.PersonalRelief, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.MortgageRelief, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.InsuranceRelief, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.NSSFNo, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.NHIFNo, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.IDNo, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.PINNo, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.DepartmentId, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.PayPoint, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.EmpGroup, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.EmpPayroll, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.PrevEmployer, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.DateLeft, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.PaymentMode, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.TelephoneNo, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.ChequeNo, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.BankCode, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.BankAccount, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.LeaveBalance, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.IsActive, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.CreatedBy, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.CreatedOn, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.IsDeleted, addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate, addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.SystemId, addr, addr, 0, "WHITE", true, 10, "n");

                row++;

            }

            excell_app.Save(strFileName);
        }

        private List<upload_download_employee_dto> get_all_employees_for_download()
        {
            List<upload_download_employee_dto> _employees = new List<upload_download_employee_dto>();

            var _employees_from_db = db.Employees.ToList();

            foreach (var emp in _employees_from_db)
            {
                upload_download_employee_dto dto_emp = new upload_download_employee_dto();

                dto_emp.Email = emp.Email;
                dto_emp.EmpNo = emp.EmpNo;
                dto_emp.Surname = emp.Surname;
                dto_emp.OtherNames = emp.OtherNames;
                dto_emp.DoB = emp.DoB.ToString();
                dto_emp.MaritalStatus = get_marital_status(emp.MaritalStatus);
                dto_emp.Gender = emp.Gender;
                dto_emp.Photo = emp.Photo;
                dto_emp.DoE = emp.DoE.ToString();
                dto_emp.BasicComputation = emp.BasicComputation;
                dto_emp.BasicPay = emp.BasicPay.ToString();
                dto_emp.PersonalRelief = emp.PersonalRelief.ToString();
                dto_emp.MortgageRelief = emp.MortgageRelief.ToString();
                dto_emp.InsuranceRelief = emp.InsuranceRelief.ToString();
                dto_emp.NSSFNo = emp.NSSFNo;
                dto_emp.NHIFNo = emp.NHIFNo;
                dto_emp.IDNo = emp.IDNo;
                dto_emp.PINNo = emp.PINNo;
                dto_emp.DepartmentId = get_department_name(emp.DepartmentId);
                dto_emp.EmployerId = get_employer_name(emp.EmployerId);
                dto_emp.PayPoint = emp.PayPoint;
                dto_emp.EmpGroup = emp.EmpGroup;
                dto_emp.EmpPayroll = emp.EmpPayroll;
                dto_emp.PrevEmployer = emp.PrevEmployer;
                dto_emp.DateLeft = emp.DateLeft.ToString();
                dto_emp.PaymentMode = emp.PaymentMode;
                dto_emp.TelephoneNo = emp.TelephoneNo;
                dto_emp.ChequeNo = emp.ChequeNo;
                dto_emp.BankCode = emp.BankCode;
                dto_emp.BankAccount = emp.BankAccount;
                dto_emp.LeaveBalance = emp.LeaveBalance;
                dto_emp.IsActive = emp.IsActive.ToString();
                dto_emp.CreatedBy = emp.CreatedBy;
                dto_emp.CreatedOn = emp.CreatedOn.ToString();
                dto_emp.IsDeleted = emp.IsDeleted.ToString();
                dto_emp.OtherNames = emp.OtherNames;
                dto_emp.SystemId = emp.SystemId;

                _employees.Add(dto_emp);
            }
            return _employees;
        }

        private string get_marital_status(string marital_status)
        {
            string status = string.Empty;
            switch (marital_status)
            {
                case "M":
                    status = "Married";
                    break;
                case "S":
                    status = "Single";
                    break;
                case "D":
                    status = "Divorced";
                    break;
            }
            return status;
        }

        private string get_department_name(int? department_id)
        {
            Department dept = rep.GetDepartments().Where(i => i.Id == department_id).FirstOrDefault();

            return dept.Description;
        }

        private string get_employer_name(int employer_id)
        {
            DAL.Employer emp = rep.GetEmployeeEmployer(employer_id);

            return emp.Name;
        }

        private void btnclearfilter_Click(object sender, EventArgs e)
        {
            txtEmpNo.Text = string.Empty;
            txtSurName.Text = string.Empty;
            txtOtherNames.Text = string.Empty;
            txtDepartment.Text = string.Empty;
            cboEmployer.SelectedIndex = 0;
        }

        private void cboEmployer_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void txtEmpNo_TextChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void txtSurName_TextChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void txtOtherNames_TextChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void txtDepartment_TextChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        /// <summary>
        /// Non-thread-blocking event handler for the Update Default Payroll Items button.
        /// </summary>
        private void btn_update_default_payroll_items_Click(object sender, EventArgs e)
        {
            // Disable button during background processing to prevent double-clicks
            Button btn = sender as Button;
            if (btn != null) btn.Enabled = false;
            progressBar_update_default_payroll_items.Visible = true;

            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += (s, args) =>
            {
                update_default_payroll_items();
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                if (btn != null) btn.Enabled = true;

                if (args.Error != null)
                {
                    NotifyMessage("Error updating default payroll items: " + args.Error.Message);
                }
                else
                {
                    NotifyMessage("Completed updating default payroll items.");
                }

                progressBar_update_default_payroll_items.Visible = false;
            };

            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Efficiently updates default payroll items for all active employees without blocking the thread.
        /// Optimized lookup performance from O(N*M) to O(1) via HashSets.
        /// </summary>
        private void update_default_payroll_items()
        {
            try
            {
                NotifyMessage("Fetching active employees...");
                List<Employee> activeEmployees = (from em in rep.GetAllActiveEmployees()
                                                  where em.IsDeleted == false
                                                  where em.IsActive == true
                                                  select em).ToList();

                NotifyMessage("Fetching active statutory payroll items...");
                List<PayrollItem> statutoryItems = (from pi in rep.GetPayrollItems()
                                                    where pi.Active.Equals(true)
                                                    where pi.Enable.Equals(true)
                                                    where pi.ItemTypeId.Equals("STATUTORY")
                                                    where pi.IsDeleted.Equals(false)
                                                    select pi).ToList();

                if (activeEmployees.Count == 0 || statutoryItems.Count == 0)
                {
                    NotifyMessage("No active employees or statutory items found to process.");
                    return;
                }

                NotifyMessage("Fetching existing employee transactions...");
                List<EmployeeTransaction> allTxns = rep.get_all_employee_transactions()
                                                       .Where(et => !et.IsDeleted ?? false)
                                                       .ToList();

                // HashSet using composite string key "EmployeeId:ItemId" for O(1) fast lookup
                HashSet<string> existingTxnSet = new HashSet<string>(
                    allTxns.Select(t => t.EmployeeId + ":" + t.ItemId)
                );

                int addedCount = 0;

                foreach (Employee emp in activeEmployees)
                {
                    foreach (PayrollItem payItem in statutoryItems)
                    {
                        string key = emp.Id + ":" + payItem.Id;

                        // O(1) lookup check
                        if (!existingTxnSet.Contains(key))
                        {
                            create_employee_transaction(emp, payItem.Id);

                            // Add to HashSet immediately so we don't insert duplicate records in the same batch run
                            existingTxnSet.Add(key);
                            addedCount++;

                            NotifyMessage(string.Format("Added item [{0}] for [{1} {2} - {3}]", payItem.Id, emp.OtherNames, emp.Surname, emp.EmpNo));
                        }
                    }
                }

                NotifyMessage(string.Format("Finished updating default payroll items. Total items added: {0}", addedCount));

                MessageBox.Show(string.Format("Finished updating default payroll items. Total items added: {0}", addedCount), Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                NotifyMessage(ex.ToString());
            }
        }

        /// <summary>
        /// Creates an employee transaction.
        /// </summary>
        private void create_employee_transaction(Employee employee, string payrollItemId)
        {
            try
            {
                EmployeeTransaction et = new EmployeeTransaction
                {
                    EmployeeId = employee.Id,
                    Amount = 0m,
                    InitialAmount = 0m,
                    PostDate = DateTime.Today,
                    EmpNo = employee.EmpNo,
                    ItemId = payrollItemId,
                    Recurrent = true,
                    Processed = false,
                    Enabled = true,
                    TrackYTD = true,
                    CreatedBy = user,
                    LastChangeDate = DateTime.Today,
                    LastChangedBy = user,
                    AuthorizedBy = user,
                    AuthorizeDate = DateTime.Today,
                    Balance = 0m,
                    AccumulativePayment = 0m,
                    ShowYTDInPayslip = true,
                    LoanType = "NON",
                    IsLoanRepaymentInCash = false,
                    IsLoanRepaymentInFull = false,
                    LoanBeingRepayedId = 0,
                    IsSharesRepaymentInCash = false
                };

                rep.Create_Employee_Transaction(et);
            }
            catch (Exception ex)
            {
                NotifyMessage(ex.ToString());
            }
        }

        /// <summary>
        /// Helper to safely invoke notification events on the UI thread when called from background threads.
        /// </summary>
        private void NotifyMessage(string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new System.Action(() => NotifyMessage(message)));
                return;
            }

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(message, TAG));
            }
        }

        private void btn_print_report_Click(object sender, EventArgs e)
        {
            try
            {
                PDFViewer _viewer = new PDFViewer(user, connection, _notificationmessageEventname, true);
                _viewer.WindowState = FormWindowState.Maximized;
                _viewer.StartPosition = FormStartPosition.CenterScreen;
                _viewer.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                NotifyMessage(ex.ToString());
            }
        }









    }


    public class upload_download_employee_dto
    {
        public string Email { get; set; }
        public string EmpNo { get; set; }
        public string Surname { get; set; }
        public string OtherNames { get; set; }
        public string DoB { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string Photo { get; set; }
        public string DoE { get; set; }
        public string BasicComputation { get; set; }
        public string BasicPay { get; set; }
        public string PersonalRelief { get; set; }
        public string MortgageRelief { get; set; }
        public string InsuranceRelief { get; set; }
        public string NSSFNo { get; set; }
        public string NHIFNo { get; set; }
        public string IDNo { get; set; }
        public string PINNo { get; set; }
        public string DepartmentId { get; set; }
        public string EmployerId { get; set; }
        public string PayPoint { get; set; }
        public string EmpGroup { get; set; }
        public string EmpPayroll { get; set; }
        public string PrevEmployer { get; set; }
        public string DateLeft { get; set; }
        public string PaymentMode { get; set; }
        public string TelephoneNo { get; set; }
        public string ChequeNo { get; set; }
        public string BankCode { get; set; }
        public string BankAccount { get; set; }
        public string LeaveBalance { get; set; }
        public string IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string IsDeleted { get; set; }
        public string SystemId { get; set; }
    }

}