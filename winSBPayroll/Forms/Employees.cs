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
                && string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text ) && cboEmployer.SelectedIndex != -1)
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
                _employees =( from ep in rep.GetAllActiveEmployees()
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
         




    }
}