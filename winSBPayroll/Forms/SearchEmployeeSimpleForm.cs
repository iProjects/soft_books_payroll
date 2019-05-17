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
using DAL.Criteria;

namespace winSBPayroll.Forms
{
    public partial class SearchEmployeeSimpleForm : Form
    {
        static int index;
        List<Field> empFields = new List<Field>();
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        CriteriaBuilder criteriaBuilder = new CriteriaBuilder();
        IQueryable<DAL.Employee> _Employees;
        //delegate
        public delegate void EmployeeSelectHandler(object sender, EmployeeSelectEventArgs e);
        //event
        public event EmployeeSelectHandler OnEmployeeListSelected;

        public SearchEmployeeSimpleForm(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void SearchEmployeeSimpleForm_Load(object sender, EventArgs e)
        {
            try
            {
                _Employees = rep.GetAllActiveEmployees().Where(i => i.IsActive == true).AsQueryable();

                var _departmentsquery = from dp in db.Departments
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
                colCboxDepartment.DisplayIndex = 4;
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
                colCboxGender.DisplayIndex = 3;
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
                dataGridViewEmployees.DataSource = bindingSourceEmployees;
                groupBoxResults.Text = bindingSourceEmployees.Count.ToString();

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
                // set the filter
                var filter = CreateFilter(_Employees);
                this.bindingSourceEmployees.DataSource = filter;
                groupBoxResults.Text = bindingSourceEmployees.Count.ToString();
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
                && string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text))
            {
                return _employees;
            }
            //all
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _EmpNo = txtEmpNo.Text;
                string _SurName = txtSurName.Text;
                string _OtherNames = txtOtherNames.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             join dp in db.Departments on ep.DepartmentId equals dp.Id
                             where ep.EmpNo.StartsWith(_EmpNo)
                             where ep.Surname.StartsWith(_SurName)
                             where ep.OtherNames.StartsWith(_OtherNames)
                             where dp.Description.StartsWith(_Department)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //empno
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _EmpNo = txtEmpNo.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             where ep.EmpNo.StartsWith(_EmpNo)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //surname
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _SurName = txtSurName.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             where ep.Surname.StartsWith(_SurName)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //othernames
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _OtherNames = txtOtherNames.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             where ep.OtherNames.StartsWith(_OtherNames)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //departments
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                 && string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             join dp in db.Departments on ep.DepartmentId equals dp.Id
                             where dp.Description.StartsWith(_Department)
                             where ep.IsActive == true
                             select ep).AsQueryable();
                return _employees;
            }
            //empno and surname
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _EmpNo = txtEmpNo.Text;
                string _SurName = txtSurName.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             where ep.EmpNo.StartsWith(_EmpNo)
                             where ep.Surname.StartsWith(_SurName)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and othernames
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                 && string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _EmpNo = txtEmpNo.Text;
                string _OtherNames = txtOtherNames.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             where ep.EmpNo.StartsWith(_EmpNo)
                             where ep.OtherNames.StartsWith(_OtherNames)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and departments
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _EmpNo = txtEmpNo.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             join dp in db.Departments on ep.DepartmentId equals dp.Id
                             where ep.EmpNo.StartsWith(_EmpNo)
                             where dp.Description.StartsWith(_Department)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //surname and othernames
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _SurName = txtSurName.Text;
                string _OtherNames = txtOtherNames.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             where ep.Surname.StartsWith(_SurName)
                             where ep.OtherNames.StartsWith(_OtherNames)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //surname and departments
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _SurName = txtSurName.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             join dp in db.Departments on ep.DepartmentId equals dp.Id
                             where ep.Surname.StartsWith(_SurName)
                             where dp.Description.StartsWith(_Department)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //othernames and departments
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _OtherNames = txtOtherNames.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             join dp in db.Departments on ep.DepartmentId equals dp.Id
                             where ep.OtherNames.StartsWith(_OtherNames)
                             where dp.Description.StartsWith(_Department)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and surname and othernames
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && !string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _EmpNo = txtEmpNo.Text;
                string _SurName = txtSurName.Text;
                string _OtherNames = txtOtherNames.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             where ep.EmpNo.StartsWith(_EmpNo)
                             where ep.Surname.StartsWith(_SurName)
                             where ep.OtherNames.StartsWith(_OtherNames)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and surname and departments
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                 && !string.IsNullOrEmpty(txtSurName.Text) && string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _EmpNo = txtEmpNo.Text;
                string _SurName = txtSurName.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             join dp in db.Departments on ep.DepartmentId equals dp.Id
                             where ep.EmpNo.StartsWith(_EmpNo)
                             where ep.Surname.StartsWith(_SurName)
                             where dp.Description.StartsWith(_Department)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //empno and othernames and departments
            if (!string.IsNullOrEmpty(txtEmpNo.Text)
                && string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _EmpNo = txtEmpNo.Text;
                string _OtherNames = txtOtherNames.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             join dp in db.Departments on ep.DepartmentId equals dp.Id
                             where ep.EmpNo.StartsWith(_EmpNo)
                             where ep.OtherNames.StartsWith(_OtherNames)
                             where dp.Description.StartsWith(_Department)
                             where ep.IsActive == true
                              select ep).AsQueryable();
                return _employees;
            }
            //surname and othernames and departments
            if (string.IsNullOrEmpty(txtEmpNo.Text)
                 && !string.IsNullOrEmpty(txtSurName.Text) && !string.IsNullOrEmpty(txtOtherNames.Text) && !string.IsNullOrEmpty(txtDepartment.Text))
            {
                string _SurName = txtSurName.Text;
                string _OtherNames = txtOtherNames.Text;
                string _Department = txtDepartment.Text;
                _employees = (from ep in rep.GetAllActiveEmployees()
                             join dp in db.Departments on ep.DepartmentId equals dp.Id
                             where ep.Surname.StartsWith(_SurName)
                             where ep.OtherNames.StartsWith(_OtherNames)
                             where dp.Description.StartsWith(_Department)
                             where ep.IsActive == true
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
        private void dataGridViewEmployees_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewEmployees.SelectedRows.Count > 0)
            {
                try
                {
                    Employee selectedEmployee = (Employee)bindingSourceEmployees.Current;
                    OnEmployeeListSelected(this, new EmployeeSelectEventArgs(selectedEmployee));

                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
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

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dataGridViewEmployees.SelectedRows.Count > 0)
            {
                try
                {
                    Employee selectedEmployee = (Employee)bindingSourceEmployees.Current;
                    OnEmployeeListSelected(this, new EmployeeSelectEventArgs(selectedEmployee));

                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }





    }
}