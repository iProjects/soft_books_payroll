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
    public partial class payslip_master_temp_form : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;
        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        IQueryable<DAL.PayslipMaster> _lst_payslip_master;
        int recordCount = 0;

        public payslip_master_temp_form(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished payslip_master_temp_form initialization", TAG));
        }

        private void payslip_master_temp_form_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView_payslip.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_payslip.MultiSelect = false;
                dataGridView_payslip.ReadOnly = true;
                dataGridView_payslip.AutoGenerateColumns = false;

                var _employers_query = (from pi in db.PayslipMaster_Temp
                                        orderby pi.CompName descending
                                        select pi.CompName).Distinct();

                List<string> _employers_lst = _employers_query.ToList();
                _employers_lst.Insert(0, "---Select---");
                cbo_employers.DisplayMember = "CompName";
                cbo_employers.ValueMember = "CompName";
                cbo_employers.DataSource = _employers_lst;
                cbo_employers.SelectedIndex = 0;

                var _employeesquery = (from pi in db.PayslipMaster_Temp
                                       orderby pi.EmpNo descending
                                       select pi.EmpNo).Distinct();

                List<string> _employees_lst = _employeesquery.ToList();
                _employees_lst.Insert(0, "---Select---");
                cboemployeeno.DisplayMember = "EmpNo";
                cboemployeeno.ValueMember = "EmpNo";
                cboemployeeno.DataSource = _employees_lst;
                cboemployeeno.SelectedIndex = 0;

                var _employee_names_query = (from pi in db.PayslipMaster_Temp
                                             orderby pi.EmpName ascending
                                             select pi.EmpName).Distinct();

                List<string> _employee_names_lst = _employee_names_query.ToList();
                _employee_names_lst.Insert(0, "---Select---");
                cbo_employee_names.DisplayMember = "EmpName";
                cbo_employee_names.ValueMember = "EmpName";
                cbo_employee_names.DataSource = _employee_names_lst;
                cbo_employee_names.SelectedIndex = 0;

                var _departments_query = (from pi in db.PayslipMaster_Temp
                                          select pi.Department).Distinct();

                List<string> _departments_lst = _departments_query.ToList();
                _departments_lst.Insert(0, "---Select---");
                cbo_departments.DisplayMember = "Id";
                cbo_departments.ValueMember = "Id";
                cbo_departments.DataSource = _departments_lst;
                cbo_departments.SelectedIndex = 0;

                var _payroll_years_query = (from p in db.PayslipMaster_Temp
                                            orderby p.Year ascending
                                            select p.Year).Distinct();
                _payroll_years_query = _payroll_years_query.OrderByDescending(i => i);
                List<int> _lst_payroll_years = _payroll_years_query.ToList();
                cbopayrollyears.DisplayMember = "Year";
                cbopayrollyears.ValueMember = "Year";
                cbopayrollyears.DataSource = _lst_payroll_years;
                cbopayrollyears.SelectedIndex = -1;

                var _payroll_periods_query = (from p in db.PayslipMaster_Temp
                                              orderby p.Year ascending
                                              select p.Period).Distinct();
                _payroll_periods_query = _payroll_periods_query.OrderByDescending(i => i);
                List<int> _lst_payroll_periods = _payroll_periods_query.ToList();
                cbopayrollperiods.DisplayMember = "Period";
                cbopayrollperiods.ValueMember = "Period";
                cbopayrollperiods.DataSource = _lst_payroll_periods;
                cbopayrollperiods.SelectedIndex = -1;

                this.RefreshGrid(1);

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished payslip_master_temp_form load", TAG));

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
                    using (SqlCommand cmd = new SqlCommand("GetPayslipMasterTempPageWise", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", NO_OF_RECORDS_PER_PAGE);
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;

                        object employer = DBNull.Value;
                        if (cbo_employers.SelectedIndex > 0 && cbo_employers.SelectedValue != null)
                        {
                            employer = (object)(string)cbo_employers.SelectedItem ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@CompName", employer);

                        object emp_name = DBNull.Value;
                        if (cbo_employee_names.SelectedIndex > 0 && cbo_employee_names.SelectedValue != null)
                        {
                            emp_name = (object)(string)cbo_employee_names.SelectedItem ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@EmpName", emp_name);

                        object empno = DBNull.Value;
                        if (cboemployeeno.SelectedIndex > 0 && cboemployeeno.SelectedValue != null)
                        {
                            empno = (object)(string)cboemployeeno.SelectedItem ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@EmpNo", empno);

                        object department = DBNull.Value;
                        if (cbo_departments.SelectedIndex > 0 && cbo_departments.SelectedValue != null)
                        {
                            department = (object)(string)cbo_departments.SelectedItem ?? DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@Department", department);

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
                _lst_payslip_master = (from DataRow dr in dt.Rows
                                       select new PayslipMaster()
                                       {
                                           Period = dr.Table.Columns.Contains("Period") && dr["Period"] != DBNull.Value ? Convert.ToInt32(dr["Period"]) : 0,
                                           Year = dr.Table.Columns.Contains("Year") && dr["Year"] != DBNull.Value ? Convert.ToInt32(dr["Year"]) : 0,
                                           EmpNo = dr.Table.Columns.Contains("EmpNo") && dr["EmpNo"] != DBNull.Value ? dr["EmpNo"].ToString() : string.Empty,
                                           EmployeeId = dr.Table.Columns.Contains("EmployeeId") && dr["EmployeeId"] != DBNull.Value ? Convert.ToInt32(dr["EmployeeId"]) : 0,
                                           PaymentDate = dr.Table.Columns.Contains("PaymentDate") && dr["PaymentDate"] != DBNull.Value ? Convert.ToDateTime(dr["PaymentDate"]) : DateTime.MinValue,
                                           PrintedBy = dr.Table.Columns.Contains("PrintedBy") && dr["PrintedBy"] != DBNull.Value ? dr["PrintedBy"].ToString() : string.Empty,
                                           PrintedOn = dr.Table.Columns.Contains("PrintedOn") && dr["PrintedOn"] != DBNull.Value ? Convert.ToDateTime(dr["PrintedOn"]) : DateTime.MinValue,
                                           EmpName = dr.Table.Columns.Contains("EmpName") && dr["EmpName"] != DBNull.Value ? dr["EmpName"].ToString() : string.Empty,
                                           PayPoint = dr.Table.Columns.Contains("PayPoint") && dr["PayPoint"] != DBNull.Value ? dr["PayPoint"].ToString() : string.Empty,
                                           PIN = dr.Table.Columns.Contains("PIN") && dr["PIN"] != DBNull.Value ? dr["PIN"].ToString() : string.Empty,
                                           NHIFNo = dr.Table.Columns.Contains("NHIFNo") && dr["NHIFNo"] != DBNull.Value ? dr["NHIFNo"].ToString() : string.Empty,
                                           NSSFNo = dr.Table.Columns.Contains("NSSFNo") && dr["NSSFNo"] != DBNull.Value ? dr["NSSFNo"].ToString() : string.Empty,
                                           Department = dr.Table.Columns.Contains("Department") && dr["Department"] != DBNull.Value ? dr["Department"].ToString() : string.Empty,
                                           EmpGroup = dr.Table.Columns.Contains("EmpGroup") && dr["EmpGroup"] != DBNull.Value ? dr["EmpGroup"].ToString() : string.Empty,
                                           EmpPayroll = dr.Table.Columns.Contains("EmpPayroll") && dr["EmpPayroll"] != DBNull.Value ? dr["EmpPayroll"].ToString() : string.Empty,
                                           CompName = dr.Table.Columns.Contains("CompName") && dr["CompName"] != DBNull.Value ? dr["CompName"].ToString() : string.Empty,
                                           CompAddr = dr.Table.Columns.Contains("CompAddr") && dr["CompAddr"] != DBNull.Value ? dr["CompAddr"].ToString() : string.Empty,
                                           CompTel = dr.Table.Columns.Contains("CompTel") && dr["CompTel"] != DBNull.Value ? dr["CompTel"].ToString() : string.Empty,
                                           PayeTax = dr.Table.Columns.Contains("PayeTax") && dr["PayeTax"] != DBNull.Value ? Convert.ToDecimal(dr["PayeTax"]) : 0m,
                                           BasicPay = dr.Table.Columns.Contains("BasicPay") && dr["BasicPay"] != DBNull.Value ? Convert.ToDecimal(dr["BasicPay"]) : 0m,
                                           Benefits = dr.Table.Columns.Contains("Benefits") && dr["Benefits"] != DBNull.Value ? Convert.ToDecimal(dr["Benefits"]) : 0m,
                                           Variables = dr.Table.Columns.Contains("Variables") && dr["Variables"] != DBNull.Value ? Convert.ToDecimal(dr["Variables"]) : 0m,
                                           OtherDeductions = dr.Table.Columns.Contains("OtherDeductions") && dr["OtherDeductions"] != DBNull.Value ? Convert.ToDecimal(dr["OtherDeductions"]) : 0m,
                                           GrossTaxableEarnings = dr.Table.Columns.Contains("GrossTaxableEarnings") && dr["GrossTaxableEarnings"] != DBNull.Value ? Convert.ToDecimal(dr["GrossTaxableEarnings"]) : 0m,
                                           NetTaxableEarnings = dr.Table.Columns.Contains("NetTaxableEarnings") && dr["NetTaxableEarnings"] != DBNull.Value ? Convert.ToDecimal(dr["NetTaxableEarnings"]) : 0m,
                                           MortgageRelief = dr.Table.Columns.Contains("MortgageRelief") && dr["MortgageRelief"] != DBNull.Value ? Convert.ToDecimal(dr["MortgageRelief"]) : 0m,
                                           InsuranceRelief = dr.Table.Columns.Contains("InsuranceRelief") && dr["InsuranceRelief"] != DBNull.Value ? Convert.ToDecimal(dr["InsuranceRelief"]) : 0m,
                                           GrossTax = dr.Table.Columns.Contains("GrossTax") && dr["GrossTax"] != DBNull.Value ? Convert.ToDecimal(dr["GrossTax"]) : 0m,
                                           PersonalRelief = dr.Table.Columns.Contains("PersonalRelief") && dr["PersonalRelief"] != DBNull.Value ? Convert.ToDecimal(dr["PersonalRelief"]) : 0m,
                                           PensionEmployer = dr.Table.Columns.Contains("PensionEmployer") && dr["PensionEmployer"] != DBNull.Value ? Convert.ToDecimal(dr["PensionEmployer"]) : 0m,
                                           EmployerNSSF = dr.Table.Columns.Contains("EmployerNSSF") && dr["EmployerNSSF"] != DBNull.Value ? Convert.ToDecimal(dr["EmployerNSSF"]) : 0m,
                                           PensionEmployee = dr.Table.Columns.Contains("PensionEmployee") && dr["PensionEmployee"] != DBNull.Value ? Convert.ToDecimal(dr["PensionEmployee"]) : 0m,
                                           BankBranch = dr.Table.Columns.Contains("BankBranch") && dr["BankBranch"] != DBNull.Value ? dr["BankBranch"].ToString() : string.Empty,
                                           Account = dr.Table.Columns.Contains("Account") && dr["Account"] != DBNull.Value ? dr["Account"].ToString() : string.Empty,
                                           NSSF = dr.Table.Columns.Contains("NSSF") && dr["NSSF"] != DBNull.Value ? Convert.ToDecimal(dr["NSSF"]) : 0m,
                                           NHIF = dr.Table.Columns.Contains("NHIF") && dr["NHIF"] != DBNull.Value ? Convert.ToDecimal(dr["NHIF"]) : 0m,
                                           NetPay = dr.Table.Columns.Contains("NetPay") && dr["NetPay"] != DBNull.Value ? Convert.ToDecimal(dr["NetPay"]) : 0m
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
                bindingSource_payslip.DataSource = _lst_payslip_master;
                dataGridView_payslip.DataSource = bindingSource_payslip;
                groupBox3.Text = bindingSource_payslip.Count.ToString() + "/" + recordCount;
                foreach (DataGridViewRow row in dataGridView_payslip.Rows)
                {
                    dataGridView_payslip.Rows[dataGridView_payslip.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridView_payslip.Rows.Count - 1;
                    bindingSource_payslip.Position = nRowIndex;
                }

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + _lst_payslip_master.ToList().Count + " ] records.", TAG));
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
                cbo_departments.SelectedIndex = 0;
                cbo_employers.SelectedIndex = 0;
                cbo_employee_names.SelectedIndex = 0;
                cbopayrollyears.SelectedIndex = -1;
                cbopayrollperiods.SelectedIndex = -1;
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

        private void cbo_employers_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void cbo_employee_othernames_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void cboemployeeno_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void cbo_departments_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void cbopayrollyears_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void cbopayrollperiods_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void btndetails_Click(object sender, EventArgs e)
        {

        }

    }
}
