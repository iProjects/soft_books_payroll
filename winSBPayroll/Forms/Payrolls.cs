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

namespace winSBPayroll.Forms
{
    public partial class Payrolls : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        MainForm MainForm;
        public DAL.UserModel user;

        public Payrolls(MainForm f, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            MainForm = f;
            user = f.LoggedInUser;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddPayroll f = new AddPayroll(user, connection) { Owner = this };
            f.ShowDialog();
        }

        private void Payrolls_Load(object sender, EventArgs e)
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

                var _emp_query = from emp in db.Employers
                                 where emp.IsActive == true
                                 where emp.IsDeleted == false
                                 orderby emp.Id descending
                                 select emp;
                List<DAL.Employer> _lst_employers = _emp_query.ToList();

                cboemployer.DisplayMember = "Name";
                cboemployer.ValueMember = "Id";
                cboemployer.DataSource = _lst_employers;

                var _payroll_years_query = (from p in db.Payrolls
                                            orderby p.Year descending
                                            select p.Year).Distinct();

                _payroll_years_query = _payroll_years_query.OrderByDescending(i => i);

                List<int> _lst_payroll_years = _payroll_years_query.ToList();

                //_lst_payroll_years.Sort((a, b) => b.CompareTo(a));

                cbopayrollyears.DisplayMember = "Year";
                cbopayrollyears.ValueMember = "Year";
                cbopayrollyears.DataSource = _lst_payroll_years;

                dataGridViewPayrolls.AutoGenerateColumns = false;
                this.dataGridViewPayrolls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                bindingSourcePayrolls.DataSource = null;
                dataGridViewPayrolls.DataSource = null;

                chkisopen.Checked = true;
                chkfor.Checked = true;

                RefreshGrid();

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        public void RefreshGrid()
        {
            refresh_payroll_years();
            filter_payrolls("ey");
        }

        private void cbYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter_payrolls("y");
        }

        private void cboemployer_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter_payrolls("e");
        }

        private void chkfor_CheckedChanged(object sender, EventArgs e)
        {
            filter_payrolls("ey");
        }

        private void chkisopen_CheckedChanged(object sender, EventArgs e)
        {
            filter_payrolls("ey");
        }

        private void filter_payrolls(string sender)
        {
            try
            {
                bindingSourcePayrolls.DataSource = null;
                dataGridViewPayrolls.DataSource = null;

                if (cboemployer.SelectedIndex != -1 && cbopayrollyears.SelectedIndex != -1)
                {
                    DAL.Employer _employer = (DAL.Employer)cboemployer.SelectedItem;
                    var _selected_employer = _employer.Id;

                    var _selected_year = (int)cbopayrollyears.SelectedItem;

                    var is_for = chkfor.Checked;

                    var is_open = chkisopen.Checked;

                    var _payrolls = from p in db.Payrolls
                                    orderby p.Year descending, p.Period descending
                                    select p;

                    if (is_open)
                    {
                        _payrolls = from p in _payrolls
                                    where p.IsOpen.Equals(is_open)
                                    orderby p.Year descending, p.Period descending
                                    select p;
                    }

                    if (is_for)
                    {
                        _payrolls = from p in _payrolls
                                    where p.EmployerId.Equals(_selected_employer)
                                    where p.Year.Equals(_selected_year)
                                    orderby p.Year descending, p.Period descending
                                    select p;
                    }
                    else
                    {
                        if (sender.Equals("e"))
                        {
                            _payrolls = from p in _payrolls
                                        where p.EmployerId.Equals(_selected_employer)
                                        orderby p.Year descending, p.Period descending
                                        select p;
                        }
                        else if (sender.Equals("y"))
                        {
                            _payrolls = from p in _payrolls
                                        where p.Year.Equals(_selected_year)
                                        orderby p.Year descending, p.Period descending
                                        select p;
                        }
                        else
                        {
                            _payrolls = from p in _payrolls
                                        orderby p.Year descending, p.Period descending
                                        select p;
                        }
                    }

                    var _lst_payrolls = _payrolls.ToList();

                    bindingSourcePayrolls.DataSource = _lst_payrolls;
                    dataGridViewPayrolls.DataSource = bindingSourcePayrolls;
                    groupBox1.Text = bindingSourcePayrolls.Count.ToString();
                }
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
                var _payrolls = from p in db.Payrolls
                                orderby p.Year descending, p.Period descending
                                select p;

                var _lst_payrolls = _payrolls.ToList();

                bindingSourcePayrolls.DataSource = _lst_payrolls;
                dataGridViewPayrolls.DataSource = bindingSourcePayrolls;
                groupBox1.Text = bindingSourcePayrolls.Count.ToString();

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewPayrolls_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                throw e.Exception;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }

        private void refresh_payroll_years()
        {
            try
            {
                var _payroll_years_query = (from p in db.Payrolls
                                            orderby p.Year descending
                                            select p.Year).Distinct();

                _payroll_years_query = _payroll_years_query.OrderByDescending(i => i);

                List<int> _lst_payroll_years = _payroll_years_query.ToList();

                //_lst_payroll_years.Sort((a, b) => b.CompareTo(a));

                cbopayrollyears.DisplayMember = "Year";
                cbopayrollyears.ValueMember = "Year";
                cbopayrollyears.DataSource = _lst_payroll_years;

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }

    }
}