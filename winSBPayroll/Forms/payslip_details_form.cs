using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;

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
                colCboxEmployeeOtherNames.Width = 100;
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
                var _employersquery = from ep in db.Employers
                                      where ep.IsActive == true
                                      where ep.IsDeleted == false
                                      select ep;

                List<DAL.Employer> _employers = _employersquery.ToList();

                DataGridViewComboBoxColumn colCboxEmployer = new DataGridViewComboBoxColumn();
                colCboxEmployer.HeaderText = "Employer";
                colCboxEmployer.Name = "colCboxEmployer";
                colCboxEmployer.DataSource = _employers;
                // The display member is the name column in the column datasource  
                colCboxEmployer.DisplayMember = "Name";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxEmployer.DataPropertyName = "EmployerId";
                // The value member is the primary key of the parent table  
                colCboxEmployer.ValueMember = "Id";
                colCboxEmployer.MaxDropDownItems = 10;
                colCboxEmployer.Width = 150;
                colCboxEmployer.DisplayIndex = 5;
                colCboxEmployer.MinimumWidth = 5;
                colCboxEmployer.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colCboxEmployer.FlatStyle = FlatStyle.Flat;
                colCboxEmployer.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployer.ReadOnly = true;

                if (!this.dataGridView_payslip.Columns.Contains("colCboxEmployer"))
                {
                    dataGridView_payslip.Columns.Add(colCboxEmployer);
                }

                dataGridView_payslip.AutoGenerateColumns = false;
                this.dataGridView_payslip.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                btnclearfilter_Click(sender, e);

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished payslip_details_form load", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void btnclearfilter_Click(object sender, EventArgs e)
        {
            try
            {
                var _payslips_query = from p in db.PayslipDet_Temp
                                      orderby p.Year descending, p.Period descending, p.EmployeeId descending
                                      select p;

                bindingSource_payslip.DataSource = _payslips_query.ToList();
                dataGridView_payslip.DataSource = bindingSource_payslip;
                groupBox2.Text = bindingSource_payslip.Count.ToString();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + _payslips_query.ToList().Count + " ] records.", TAG));
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
    }
}
