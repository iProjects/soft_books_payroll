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
    public partial class Employees : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;
        IQueryable<DAL.Employee> _Employees;

        public Employees(string user, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _user = user;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AddEmployee f = new AddEmployee(_user, connection) { Owner = this };
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public void RefreshGrid()
        {
            bindingSourceEmployees.DataSource = null;
            if (cboEmployer.SelectedIndex != -1)
            {
                try
                {
                    DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;

                    if (chkInActive.Checked)
                    {
                        ApplyFilter();
                        //var _employees = from em in rep.GetAllEmployees()
                        //                 where em.EmployerId == _employer.Id
                        //                 where em.IsDeleted == false
                        //                 select em;
                        //bindingSourceEmployees.DataSource = _employees;
                        //groupBox2.Text = bindingSourceEmployees.Count.ToString();
                        //foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
                        //{
                        //    dataGridViewEmployees.Rows[dataGridViewEmployees.Rows.Count - 1].Selected = true;
                        //    int nRowIndex = dataGridViewEmployees.Rows.Count - 1;
                        //    bindingSourceEmployees.Position = nRowIndex;
                        //}
                    }
                    else
                    {
                        ApplyFilter();
                        //var _employees = from em in rep.GetAllEmployees()
                        //                 where em.EmployerId == _employer.Id
                        //                 where em.IsActive == true
                        //                 where em.IsDeleted == false
                        //                 select em;
                        //bindingSourceEmployees.DataSource = _employees;
                        //groupBox2.Text = bindingSourceEmployees.Count.ToString();
                        //foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
                        //{
                        //    dataGridViewEmployees.Rows[dataGridViewEmployees.Rows.Count - 1].Selected = true;
                        //    int nRowIndex = dataGridViewEmployees.Rows.Count - 1;
                        //    bindingSourceEmployees.Position = nRowIndex;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }


            //try
            //{
            //    //set the datasource to null
            //    bindingSourceEmployees.DataSource = null;
            //    if (chkInActive.Checked)
            //    {
            //        //set the datasource to a method
            //        bindingSourceEmployees.DataSource = rep.GetAllEmployees();
            //        groupBox2.Text = bindingSourceEmployees.Count.ToString();
            //        foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
            //        {
            //            dataGridViewEmployees.Rows[dataGridViewEmployees.Rows.Count - 1].Selected = true;
            //            int nRowIndex = dataGridViewEmployees.Rows.Count - 1;
            //            bindingSourceEmployees.Position = nRowIndex;
            //        }
            //    }
            //    else
            //    {
            //        //set the datasource to null
            //        bindingSourceEmployees.DataSource = null;
            //        //set the datasource to a method
            //        bindingSourceEmployees.DataSource = rep.GetAllActiveEmployees();
            //        groupBox2.Text = bindingSourceEmployees.Count.ToString();
            //        foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
            //        {
            //            dataGridViewEmployees.Rows[dataGridViewEmployees.Rows.Count - 1].Selected = true;
            //            int nRowIndex = dataGridViewEmployees.Rows.Count - 1;
            //            bindingSourceEmployees.Position = nRowIndex;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utils.ShowError(ex);
            //}
        }
        private void Employees_Load(object sender, EventArgs e)
        {
            try
            {
                _Employees = (from em in rep.GetAllActiveEmployees()
                              where em.IsActive == true
                              where em.IsDeleted == false
                              select em).AsQueryable();

                var _employersquery = from ep in db.Employers
                                      where ep.IsActive == true
                                      where ep.IsDeleted == false
                                      select ep;
                List<DAL.Employer> _employers = _employersquery.ToList();
                cboEmployer.DataSource = _employers;
                cboEmployer.DisplayMember = "Name";
                cboEmployer.ValueMember = "Id";

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
                //colCboxGender.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colCboxGender.FlatStyle = FlatStyle.Flat;
                colCboxGender.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxGender.ReadOnly = true;
                if (!this.dataGridViewEmployees.Columns.Contains("cbGender"))
                {
                    dataGridViewEmployees.Columns.Add(colCboxGender);
                }

                dataGridViewEmployees.AutoGenerateColumns = false;
                this.dataGridViewEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourceEmployees.DataSource = rep.GetAllActiveEmployees();
                dataGridViewEmployees.DataSource = bindingSourceEmployees;
                groupBox2.Text = bindingSourceEmployees.Count.ToString();

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
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
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
                Utils.ShowError(ex);
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
                Utils.ShowError(ex);
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
                Utils.ShowError(ex);
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
                Utils.ShowError(ex);
                return null;
            }
        }
        // apply the filter
        private void ApplyFilter()
        {
            try
            {
                bindingSourceEmployees.DataSource = null;
                // set the filter
                var filter = CreateFilter(_Employees);
                this.bindingSourceEmployees.DataSource = filter;
                groupBox2.Text = bindingSourceEmployees.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private IQueryable<DAL.Employee> CreateFilter(IQueryable<DAL.Employee> _employees)
        {
            //none
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              where ep.EmployerId == _employer.Id
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //all
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _EmpNo = txtEmpNo.Text;
                string _SurName = txtSurName.Text;
                string _OtherNames = txtOtherNames.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              join dp in db.Departments on ep.DepartmentId equals dp.Id
                              where ep.EmpNo.StartsWith(_EmpNo)
                              where ep.Surname.StartsWith(_SurName)
                              where ep.EmployerId == _employer.Id
                              where ep.OtherNames.StartsWith(_OtherNames)
                              where dp.Description.StartsWith(_Department)
                              where ep.IsDeleted == false
                              where dp.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //empno
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _EmpNo = txtEmpNo.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              where ep.EmpNo.StartsWith(_EmpNo)
                              where ep.EmployerId == _employer.Id
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //surname
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _SurName = txtSurName.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              where ep.Surname.StartsWith(_SurName)
                              where ep.EmployerId == _employer.Id
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //othernames
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _OtherNames = txtOtherNames.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              where ep.OtherNames.StartsWith(_OtherNames)
                              where ep.EmployerId == _employer.Id
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //departments
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                 && string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              join dp in db.Departments on ep.DepartmentId equals dp.Id
                              where dp.Description.StartsWith(_Department)
                              where ep.EmployerId == _employer.Id
                              where ep.IsDeleted == false
                              where dp.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and surname
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _EmpNo = txtEmpNo.Text;
                string _SurName = txtSurName.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              where ep.EmpNo.StartsWith(_EmpNo)
                              where ep.Surname.StartsWith(_SurName)
                              where ep.EmployerId == _employer.Id
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and othernames
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                 && string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _EmpNo = txtEmpNo.Text;
                string _OtherNames = txtOtherNames.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              where ep.EmpNo.StartsWith(_EmpNo)
                              where ep.EmployerId == _employer.Id
                              where ep.OtherNames.StartsWith(_OtherNames)
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and departments
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _EmpNo = txtEmpNo.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              join dp in db.Departments on ep.DepartmentId equals dp.Id
                              where dp.IsDeleted == false
                              where ep.EmpNo.StartsWith(_EmpNo)
                              where dp.Description.StartsWith(_Department)
                              where ep.IsDeleted == false
                              where ep.EmployerId == _employer.Id
                              select ep).AsQueryable();
                return _employees;
            }
            //surname and othernames
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _SurName = txtSurName.Text;
                string _OtherNames = txtOtherNames.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              where ep.Surname.StartsWith(_SurName)
                              where ep.OtherNames.StartsWith(_OtherNames)
                              where ep.EmployerId == _employer.Id
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //surname and departments
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _SurName = txtSurName.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              join dp in db.Departments on ep.DepartmentId equals dp.Id
                              where ep.Surname.StartsWith(_SurName)
                              where dp.IsDeleted == false
                              where dp.Description.StartsWith(_Department)
                              where ep.EmployerId == _employer.Id
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //othernames and departments
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _OtherNames = txtOtherNames.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              join dp in db.Departments on ep.DepartmentId equals dp.Id
                              where ep.OtherNames.StartsWith(_OtherNames)
                              where dp.Description.StartsWith(_Department)
                              where ep.EmployerId == _employer.Id
                              where dp.IsDeleted == false
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and surname and othernames
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _EmpNo = txtEmpNo.Text;
                string _SurName = txtSurName.Text;
                string _OtherNames = txtOtherNames.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              where ep.EmployerId == _employer.Id
                              where ep.EmpNo.StartsWith(_EmpNo)
                              where ep.Surname.StartsWith(_SurName)
                              where ep.OtherNames.StartsWith(_OtherNames)
                              where ep.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and surname and departments
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                 && !string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _EmpNo = txtEmpNo.Text;
                string _SurName = txtSurName.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              join dp in db.Departments on ep.DepartmentId equals dp.Id
                              where ep.EmpNo.StartsWith(_EmpNo)
                              where ep.EmployerId == _employer.Id
                              where ep.Surname.StartsWith(_SurName)
                              where dp.Description.StartsWith(_Department)
                              where ep.IsDeleted == false
                              where dp.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and othernames and departments
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _EmpNo = txtEmpNo.Text;
                string _OtherNames = txtOtherNames.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              join dp in db.Departments on ep.DepartmentId equals dp.Id
                              where ep.EmpNo.StartsWith(_EmpNo)
                              where ep.EmployerId == _employer.Id
                              where ep.OtherNames.StartsWith(_OtherNames)
                              where dp.Description.StartsWith(_Department)
                              where ep.IsDeleted == false
                              where dp.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            //surname and othernames and departments
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                 && !string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text) && cboEmployer.SelectedIndex != -1)
            {
                DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;
                string _SurName = txtSurName.Text;
                string _OtherNames = txtOtherNames.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                              join dp in db.Departments on ep.DepartmentId equals dp.Id
                              where ep.Surname.StartsWith(_SurName)
                              where ep.OtherNames.StartsWith(_OtherNames)
                              where ep.EmployerId == _employer.Id
                              where dp.Description.StartsWith(_Department)
                              where ep.IsDeleted == false
                              where dp.IsDeleted == false
                              select ep).AsQueryable();
                return _employees;
            }
            return _employees;
        }
        private void txtEmpNo_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void txtEmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
        }
        private void txtSurName_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void txtSurName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
        }
        private void txtOtherNames_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void txtOtherNames_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
        }
        private void txtDepartment_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
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

                    Forms.EditEmployee f = new EditEmployee(emp, _user, connection);
                    f.Text = emp.Surname.ToString().Trim().ToUpper() + "     " + emp.OtherNames.ToString().Trim().ToUpper();
                    f.Owner = this;
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEmployees.SelectedRows.Count != 0)
                {
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;

                    Forms.EditEmployee f = new EditEmployee(emp, _user, connection) { Owner = this };
                    f.DisableControls();
                    f.Text = emp.Surname.ToString().Trim().ToUpper() + "     " + emp.OtherNames.ToString().Trim().ToUpper();
                    f.ShowDialog();
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
                            RefreshGrid();
                        }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewEmployees_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewEmployees.SelectedRows.Count != 0)
                {
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;

                    Forms.EditEmployee f = new EditEmployee(emp, _user, connection);
                    f.Text = emp.Surname.ToString().Trim().ToUpper() + "     " + emp.OtherNames.ToString().Trim().ToUpper();
                    f.Owner = this;
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewEmployees_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void chkInActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSourceEmployees.DataSource = null;
                if (chkInActive.Checked)
                {
                    bindingSourceEmployees.DataSource = null;
                    bindingSourceEmployees.DataSource = rep.GetAllEmployees();
                    groupBox2.Text = bindingSourceEmployees.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
                    {
                        dataGridViewEmployees.Rows[dataGridViewEmployees.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmployees.Rows.Count - 1;
                        bindingSourceEmployees.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourceEmployees.DataSource = null;
                    bindingSourceEmployees.DataSource = rep.GetAllActiveEmployees();
                    groupBox2.Text = bindingSourceEmployees.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
                    {
                        dataGridViewEmployees.Rows[dataGridViewEmployees.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmployees.Rows.Count - 1;
                        bindingSourceEmployees.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void cboEmployer_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindingSourceEmployees.DataSource = null;
            if (cboEmployer.SelectedIndex != -1)
            {
                try
                {
                    DAL.Employer _employer = (DAL.Employer)cboEmployer.SelectedItem;

                    if (chkInActive.Checked)
                    {
                        ApplyFilter();
                        //var _employees = from em in rep.GetAllEmployees()
                        //                 where em.EmployerId == _employer.Id
                        //                 where em.IsDeleted == false
                        //                 select em;
                        //bindingSourceEmployees.DataSource = _employees;
                        //groupBox2.Text = bindingSourceEmployees.Count.ToString();
                        //foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
                        //{
                        //    dataGridViewEmployees.Rows[dataGridViewEmployees.Rows.Count - 1].Selected = true;
                        //    int nRowIndex = dataGridViewEmployees.Rows.Count - 1;
                        //    bindingSourceEmployees.Position = nRowIndex;
                        //}
                    }
                    else
                    {
                        ApplyFilter();
                        //var _employees = from em in rep.GetAllEmployees()
                        //                 where em.EmployerId == _employer.Id
                        //                 where em.IsActive == true
                        //                 where em.IsDeleted == false
                        //                 select em;
                        //bindingSourceEmployees.DataSource = _employees;
                        //groupBox2.Text = bindingSourceEmployees.Count.ToString();
                        //foreach (DataGridViewRow row in dataGridViewEmployees.Rows)
                        //{
                        //    dataGridViewEmployees.Rows[dataGridViewEmployees.Rows.Count - 1].Selected = true;
                        //    int nRowIndex = dataGridViewEmployees.Rows.Count - 1;
                        //    bindingSourceEmployees.Position = nRowIndex;
                        //}
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void btnuploademployees_Click(object sender, EventArgs e)
        {
            Forms.UploadEmployeesForm u = new Forms.UploadEmployeesForm(_user, connection) { Owner = this };
            u.Show();
        }

        private void btndownload_employees_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog.Title = "Select an excel file";
                //openFileDialog1.FileName = "";
                //"Text files (*.txt)|*.txt|All files (*.*)|*.*"
                saveFileDialog.Filter = "Excel Files|*.xlsx | Excel Files|*.xls";

                DialogResult result = saveFileDialog.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    string strFileName = saveFileDialog.FileName;

                    // use bulkcopy method of upload

                    //clear or backup the destination
                    Download(strFileName, _user);

                    MessageBox.Show("Download completed successfully");
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
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