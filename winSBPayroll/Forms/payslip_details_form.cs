using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace winSBPayroll.Forms
{
    public partial class payslip_details_form : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;
        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        IQueryable<DAL.PayslipDet> _lst_payslip_det;
        int recordCount = 0;

        public payslip_details_form(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished payslip_details_form initialization", TAG));
        }

        private void payslip_details_form_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView_payslip.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_payslip.MultiSelect = false;
                dataGridView_payslip.ReadOnly = true;
                dataGridView_payslip.AutoGenerateColumns = false;

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
                colCboxEmployeeOtherNames.DisplayIndex = 3;
                colCboxEmployeeOtherNames.MinimumWidth = 5;
                colCboxEmployeeOtherNames.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colCboxEmployeeOtherNames.FlatStyle = FlatStyle.Flat;
                colCboxEmployeeOtherNames.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployeeOtherNames.ReadOnly = true;

                if (!this.dataGridView_payslip.Columns.Contains("colCboxEmployeeOtherNames"))
                {
                    dataGridView_payslip.Columns.Add(colCboxEmployeeOtherNames);
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
                colCboxEmployeeSurname.DisplayIndex = 4;
                colCboxEmployeeSurname.MinimumWidth = 5;
                colCboxEmployeeSurname.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colCboxEmployeeSurname.FlatStyle = FlatStyle.Flat;
                colCboxEmployeeSurname.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployeeSurname.ReadOnly = true;

                if (!this.dataGridView_payslip.Columns.Contains("colCboxEmployeeSurname"))
                {
                    dataGridView_payslip.Columns.Add(colCboxEmployeeSurname);
                }

                var _payrollitem_query = from pi in rep.GetActivePayrollItems()
                                         where pi.Enable == true
                                         where pi.Active == true
                                         where pi.IsDeleted == false
                                         orderby pi.Id ascending
                                         select pi;

                List<DAL.PayrollItem> _payrollitem_lst = _payrollitem_query.ToList();
                _payrollitem_lst.Insert(0, new PayrollItem { Id = "---Select---" });
                cbopayrollitem.DisplayMember = "Id";
                cbopayrollitem.ValueMember = "Id";
                cbopayrollitem.DataSource = _payrollitem_lst;
                cbopayrollitem.SelectedIndex = 0;

                var _employeesquery = (from pi in rep.get_all_employee_transactions()
                                       orderby pi.EmpNo ascending
                                       select pi.EmpNo).Distinct();

                List<string> _employees_lst = _employeesquery.ToList();
                _employees_lst.Insert(0, "---Select---");
                cboemployeeno.DisplayMember = "EmpNo";
                cboemployeeno.ValueMember = "EmpNo";
                cboemployeeno.DataSource = _employees_lst;
                cboemployeeno.SelectedIndex = 0;

                var _payroll_years_query = (from p in db.Payrolls
                                            orderby p.Year ascending
                                            select p.Year).Distinct();
                _payroll_years_query = _payroll_years_query.OrderByDescending(i => i);
                List<int> _lst_payroll_years = _payroll_years_query.ToList();
                cbopayrollyears.DisplayMember = "Year";
                cbopayrollyears.ValueMember = "Year";
                cbopayrollyears.DataSource = _lst_payroll_years;
                cbopayrollyears.SelectedIndex = -1;

                var _payroll_periods_query = (from p in db.Payrolls
                                              orderby p.Year ascending
                                              select p.Period).Distinct();
                _payroll_periods_query = _payroll_periods_query.OrderByDescending(i => i);
                List<int> _lst_payroll_periods = _payroll_periods_query.ToList();
                cbopayrollperiods.DisplayMember = "Period";
                cbopayrollperiods.ValueMember = "Period";
                cbopayrollperiods.DataSource = _lst_payroll_periods;
                cbopayrollperiods.SelectedIndex = -1;

                var _tax_tracking_query = from pi in db.TaxTrackings
                                          orderby pi.Id ascending
                                          select pi;

                List<DAL.TaxTracking> _tax_tracking_lst = _tax_tracking_query.ToList();
                _tax_tracking_lst.Insert(0, new TaxTracking { Id = "---Select---" });
                cbotaxtracking.DisplayMember = "Id";
                cbotaxtracking.ValueMember = "Id";
                cbotaxtracking.DataSource = _tax_tracking_lst;
                cbotaxtracking.SelectedIndex = 0;

                this.RefreshGrid(1);

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished payslip_details_form load", TAG));
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
                    using (SqlCommand cmd = new SqlCommand("GetPayslipDetsPageWise", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", NO_OF_RECORDS_PER_PAGE);
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                        object empno = DBNull.Value;
                        if (cboemployeeno.SelectedIndex > 0 && cboemployeeno.SelectedValue != null)
                        {
                            empno = (object)(string)cboemployeeno.SelectedItem ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@EmpNo", empno);

                        object itemid = DBNull.Value;
                        if (cbopayrollitem.SelectedIndex > 0 && cbopayrollitem.SelectedValue != null)
                        {
                            PayrollItem _payrollitem = (PayrollItem)cbopayrollitem.SelectedItem;
                            itemid = (object)_payrollitem.Id ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@Description", itemid);

                        object payroll_year = DBNull.Value;
                        if (cbopayrollyears.SelectedIndex != -1 && cbopayrollyears.SelectedValue != null)
                        {
                            payroll_year = (object)(int)cbopayrollyears.SelectedItem ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@Year", payroll_year);

                        object payroll_period = DBNull.Value;
                        if (cbopayrollperiods.SelectedIndex != -1 && cbopayrollperiods.SelectedValue != null)
                        {
                            payroll_period = (object)(int)cbopayrollperiods.SelectedItem ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@Period", payroll_period);

                        object tax_tracking = DBNull.Value;
                        if (cbotaxtracking.SelectedIndex > 0 && cbotaxtracking.SelectedValue != null)
                        {
                            TaxTracking _taxtracking = (TaxTracking)cbotaxtracking.SelectedItem;
                            tax_tracking = (object)_taxtracking.Id ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@TaxTracking", tax_tracking);

                        if (chk_is_statutory.Checked)
                        {
                            cmd.Parameters.AddWithValue("@IsStatutory", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IsStatutory", DBNull.Value);
                        }

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
                _lst_payslip_det = (from DataRow dr in dt.Rows
                                    select new PayslipDet()
                                    {
                                        Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0,
                                        EmpNo = dr["EmpNo"] != DBNull.Value ? dr["EmpNo"].ToString() : string.Empty,
                                        EmployeeId = dr["EmployeeId"] != DBNull.Value ? Convert.ToInt32(dr["EmployeeId"]) : 0,
                                        EmpTxnId = dr["EmpTxnId"] != DBNull.Value ? Convert.ToInt32(dr["EmpTxnId"]) : 0,
                                        Period = dr["Period"] != DBNull.Value ? Convert.ToInt32(dr["Period"]) : 0,
                                        Year = dr["Year"] != DBNull.Value ? Convert.ToInt32(dr["Year"]) : 0,
                                        Description = dr["Description"] != DBNull.Value ? dr["Description"].ToString() : string.Empty,
                                        TaxTracking = dr["TaxTracking"] != DBNull.Value ? dr["TaxTracking"].ToString() : string.Empty,
                                        Amount = dr["Amount"] != DBNull.Value ? Convert.ToDecimal(dr["Amount"]) : 0m,
                                        DEType = dr["DEType"] != DBNull.Value ? dr["DEType"].ToString() : string.Empty,
                                        IsStatutory = dr["IsStatutory"] != DBNull.Value && Convert.ToBoolean(dr["IsStatutory"]),
                                        ShowInPayslip = dr["ShowInPayslip"] != DBNull.Value && Convert.ToBoolean(dr["ShowInPayslip"]),
                                        YTD = dr["YTD"] != DBNull.Value ? Convert.ToDecimal(dr["YTD"]) : 0m
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
                bindingSource_payslip.DataSource = null;
                //set the datasource to a method
                bindingSource_payslip.DataSource = _lst_payslip_det;
                dataGridView_payslip.DataSource = bindingSource_payslip;
                groupBox3.Text = bindingSource_payslip.Count.ToString() + "/" + recordCount;
                foreach (DataGridViewRow row in dataGridView_payslip.Rows)
                {
                    dataGridView_payslip.Rows[dataGridView_payslip.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridView_payslip.Rows.Count - 1;
                    bindingSource_payslip.Position = nRowIndex;
                }
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + _lst_payslip_det.ToList().Count + " ] records.", TAG));
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
                cbopayrollitem.SelectedIndex = 0;
                cboemployeeno.SelectedIndex = 0;
                cbopayrollyears.SelectedIndex = -1;
                cbopayrollperiods.SelectedIndex = -1;
                cbotaxtracking.SelectedIndex = 0;
                chk_is_statutory.Checked = false;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView_payslip_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        private void cbopayrollitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void cboemployeeno_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void cbopayrollyears_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void cbopayrollperiods_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void cbotaxtracking_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

        private void chk_is_statutory_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshGrid(1);
        }

    }
}
