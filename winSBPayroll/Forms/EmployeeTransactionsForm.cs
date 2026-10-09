using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace winSBPayroll.Forms
{
    public partial class EmployeeTransactionsForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;
        IQueryable<DAL.EmployeeTransaction> _lst_employee_txn;
        int recordCount = 0;
        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public EmployeeTransactionsForm(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _user = user;

            _notificationmessageEventname = notificationmessageEventname;
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished EmployeeTransactionsForm initialization", TAG));
        }

        private void EmployeeTransactionsForm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView_employee_txn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_employee_txn.MultiSelect = false;
                dataGridView_employee_txn.ReadOnly = true;
                dataGridView_employee_txn.AutoGenerateColumns = false;

                var _employees_query = from ep in db.Employees
                                       where ep.IsActive == true
                                       where ep.IsDeleted == false
                                       select ep;

                List<DAL.Employee> _lst_employees = _employees_query.ToList();

                DataGridViewComboBoxColumn colCboxEmployeeOtherNames = new DataGridViewComboBoxColumn();
                colCboxEmployeeOtherNames.HeaderText = "OtherNames";
                colCboxEmployeeOtherNames.Name = "colCboxEmployeeOtherNames";
                colCboxEmployeeOtherNames.DataSource = _lst_employees;
                // The display member is the name column in the column datasource  
                colCboxEmployeeOtherNames.DisplayMember = "OtherNames";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxEmployeeOtherNames.DataPropertyName = "EmployeeId";
                // The value member is the primary key of the parent table  
                colCboxEmployeeOtherNames.ValueMember = "Id";
                colCboxEmployeeOtherNames.MaxDropDownItems = 10;
                colCboxEmployeeOtherNames.Width = 150;
                colCboxEmployeeOtherNames.DisplayIndex = 6;
                colCboxEmployeeOtherNames.MinimumWidth = 5;
                colCboxEmployeeOtherNames.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colCboxEmployeeOtherNames.FlatStyle = FlatStyle.Flat;
                colCboxEmployeeOtherNames.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployeeOtherNames.ReadOnly = true;

                if (!this.dataGridView_employee_txn.Columns.Contains("colCboxEmployeeOtherNames"))
                {
                    dataGridView_employee_txn.Columns.Add(colCboxEmployeeOtherNames);
                }

                DataGridViewComboBoxColumn colCboxEmployeeSurname = new DataGridViewComboBoxColumn();
                colCboxEmployeeSurname.HeaderText = "Surname";
                colCboxEmployeeSurname.Name = "colCboxEmployeeSurname";
                colCboxEmployeeSurname.DataSource = _lst_employees;
                // The display member is the name column in the column datasource  
                colCboxEmployeeSurname.DisplayMember = "Surname";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxEmployeeSurname.DataPropertyName = "EmployeeId";
                // The value member is the primary key of the parent table  
                colCboxEmployeeSurname.ValueMember = "Id";
                colCboxEmployeeSurname.MaxDropDownItems = 10;
                colCboxEmployeeSurname.Width = 100;
                colCboxEmployeeSurname.DisplayIndex = 7;
                colCboxEmployeeSurname.MinimumWidth = 5;
                colCboxEmployeeSurname.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colCboxEmployeeSurname.FlatStyle = FlatStyle.Flat;
                colCboxEmployeeSurname.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployeeSurname.ReadOnly = true;

                if (!this.dataGridView_employee_txn.Columns.Contains("colCboxEmployeeSurname"))
                {
                    dataGridView_employee_txn.Columns.Add(colCboxEmployeeSurname);
                }

                var _employersquery = from ep in db.Employers
                                      where ep.IsActive == true
                                      where ep.IsDeleted == false
                                      select ep;
                List<DAL.Employer> _employers = _employersquery.ToList();

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
                if (!this.dataGridView_employee_txn.Columns.Contains("cbEmployer"))
                {
                    //dataGridView_employee_txn.Columns.Add(colCboxEmployer);
                }

                var _payrollitem_query = from pi in rep.GetActivePayrollItems()
                                         where pi.Enable == true
                                         where pi.Active == true
                                         where pi.IsDeleted == false
                                         orderby pi.Id descending
                                         select pi;

                List<DAL.PayrollItem> _payrollitem_lst = _payrollitem_query.ToList();
                _payrollitem_lst.Insert(0, new PayrollItem { Id = "---Select---" });
                cbopayrollitem.DisplayMember = "Id";
                cbopayrollitem.ValueMember = "Id";
                cbopayrollitem.DataSource = _payrollitem_lst;
                cbopayrollitem.SelectedIndex = 0;

                var _employeesquery = (from pi in rep.get_all_employee_transactions()
                                       orderby pi.EmpNo descending
                                       select pi.EmpNo).Distinct();

                List<string> _employees_lst = _employeesquery.ToList();
                _employees_lst.Insert(0, "---Select---");
                cboemployeeno.DisplayMember = "EmpNo";
                cboemployeeno.ValueMember = "EmpNo";
                cboemployeeno.DataSource = _employees_lst;
                cboemployeeno.SelectedIndex = 0;

                var _employers_query = (from pi in db.Employers
                                        orderby pi.Id descending
                                        select pi).Distinct();

                List<Employer> _employers_lst = _employers_query.ToList();
                _employers_lst.Insert(0, new Employer { Id = 0, Name = "---Select---" });
                cbo_employers.DisplayMember = "Name";
                cbo_employers.ValueMember = "Id";
                cbo_employers.DataSource = _employers_lst;
                cbo_employers.SelectedIndex = 0;

                this.RefreshGrid(1);

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished EmployeeTransactionsForm load", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
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
                    using (SqlCommand cmd = new SqlCommand("GetEmployeesTransactionsPageWise", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", NO_OF_RECORDS_PER_PAGE);
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                        object itemid = DBNull.Value;
                        if (cbopayrollitem.SelectedIndex > 0 && cbopayrollitem.SelectedValue != null)
                        {
                            PayrollItem _payrollitem = (PayrollItem)cbopayrollitem.SelectedItem;
                            itemid = (object)_payrollitem.Id ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@ItemId", itemid);

                        object empno = DBNull.Value;
                        if (cboemployeeno.SelectedIndex > 0 && cboemployeeno.SelectedValue != null)
                        {
                            empno = (object)(string)cboemployeeno.SelectedItem ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@EmpNo", empno);

                        object employer_id = DBNull.Value;
                        if (cbo_employers.SelectedIndex > 0 && cbo_employers.SelectedValue != null)
                        {
                            Employer _employer = (Employer)cbo_employers.SelectedItem;
                            employer_id = (object)_employer.Id ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@EmployerId", employer_id);

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
                Utils.ShowError(ex);
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
                _lst_employee_txn = (from DataRow dr in dt.Rows
                                     select new EmployeeTransaction()
                                     {
                                         Id = Convert.ToInt32(dr["Id"]),
                                         EmpNo = dr["EmpNo"].ToString(),
                                         PostDate = Convert.ToDateTime(dr["PostDate"]),
                                         EmployeeId = Convert.ToInt32(dr["EmployeeId"]),
                                         //EmployerId = Convert.ToInt32(dr["EmployerId"]),
                                         Enabled = Convert.ToBoolean(dr["Enabled"]),
                                         Recurrent = Convert.ToBoolean(dr["Recurrent"]),
                                         ItemId = dr["ItemId"].ToString(),
                                         Amount = Convert.ToDecimal(dr["Amount"]),
                                         Balance = Convert.ToDecimal(dr["Balance"]),
                                         InitialAmount = Convert.ToDecimal(dr["InitialAmount"]),
                                         Processed = Convert.ToBoolean(dr["Processed"]),
                                         TrackYTD = Convert.ToBoolean(dr["TrackYTD"]),
                                         ShowYTDInPayslip = Convert.ToBoolean(dr["ShowYTDInPayslip"]),
                                         CreatedBy = dr["CreatedBy"].ToString(),
                                         LastChangedBy = dr["LastChangedBy"].ToString(),
                                         LastChangeDate = Convert.ToDateTime(dr["LastChangeDate"]),
                                         AuthorizedBy = dr["AuthorizedBy"].ToString(),
                                         AuthorizeDate = Convert.ToDateTime(dr["AuthorizeDate"]),
                                         LoanType = dr["LoanType"].ToString(),
                                         AccumulativePayment = Convert.ToDecimal(dr["AccumulativePayment"]),
                                         IsDeleted = Convert.ToBoolean(dr["IsDeleted"]),
                                         IsLoanRepaymentInFull = Convert.ToBoolean(dr["IsLoanRepaymentInFull"]),
                                         LoanBeingRepayedId = Convert.ToInt32(dr["LoanBeingRepayedId"]),
                                         SaccoBeingRepayedId = Convert.ToInt32(dr["SaccoBeingRepayedId"]),
                                         IsLoanRepaymentInCash = Convert.ToBoolean(dr["IsLoanRepaymentInCash"]),
                                         IsSharesRepaymentInCash = Convert.ToBoolean(dr["IsSharesRepaymentInCash"])
                                     }).AsQueryable();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void BindGrid()
        {
            try
            {
                //set the datasource to null
                bindingSource_employee_txn.DataSource = null;
                //set the datasource to a method
                bindingSource_employee_txn.DataSource = _lst_employee_txn;
                dataGridView_employee_txn.DataSource = bindingSource_employee_txn;
                groupBox3.Text = bindingSource_employee_txn.Count.ToString() + "/" + recordCount;
                foreach (DataGridViewRow row in dataGridView_employee_txn.Rows)
                {
                    dataGridView_employee_txn.Rows[dataGridView_employee_txn.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridView_employee_txn.Rows.Count - 1;
                    bindingSource_employee_txn.Position = nRowIndex;
                }
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + _lst_employee_txn.ToList().Count + " ] records.", TAG));
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnclearfilter_Click(object sender, EventArgs e)
        {
            try
            {
                cboemployeeno.SelectedIndex = 0;
                cbopayrollitem.SelectedIndex = 0;
                cbo_employers.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        public void RefreshGrid()
        {
            this.RefreshGrid(1);
        }

        private void cbopayrollitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void cboemployeeno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void chkfor_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btndetails_Click(object sender, EventArgs e)
        {
            open_transaction_details();
        }

        private void dataGridView_employee_txn_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            open_transaction_details();
        }

        void open_transaction_details()
        {
            if (dataGridView_employee_txn.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.EmployeeTransaction _emptxn = (DAL.EmployeeTransaction)bindingSource_employee_txn.Current;

                    emptxndetailsform emptxnfrm = new emptxndetailsform(_user, connection, _notificationmessageEventname, _emptxn) { Owner = this };
                    emptxnfrm.Text = _emptxn.Id.ToString().Trim().ToUpper();
                    emptxnfrm.ShowDialog();

                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.ShowError(ex);
                }
            }
        }

        private void dataGridView_employee_txn_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        private void cbo_employers_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }




    }
}
