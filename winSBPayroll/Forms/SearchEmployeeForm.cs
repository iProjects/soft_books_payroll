using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;
using DAL.Criteria;

namespace winSBPayroll.Forms
{


    public partial class SearchEmployeeForm : Form
    {
        static int index;
        List<Field> empFields = new List<Field>();
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        CriteriaBuilder criteriaBuilder = new CriteriaBuilder();
        List<DAL.Employee> employees;

        //delegate
        public delegate void EmployeeSelectHandler(object sender, EmployeeSelectEventArgs e);
        //event
        public event EmployeeSelectHandler OnEmployeeListSelected;

        public SearchEmployeeForm(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void SelectForm_Load(object sender, EventArgs e)
        {
            try
            {
                empFields.Add(new Field("EmpNo", "string"));
                empFields.Add(new Field("Surname", "string"));
                empFields.Add(new Field("DoB", "date"));
                empFields.Add(new Field("DoE", "date"));
                empFields.Add(new Field("Department", "string"));
                empFields.Add(new Field("MaritalStatus", "string"));
                empFields.Add(new Field("PayPoint", "string"));
                empFields.Add(new Field("EmpGroup", "string"));
                empFields.Add(new Field("EmpPayroll", "string"));
                empFields.Add(new Field("Gender", "string"));

                cbField.DataSource = empFields;
                cbField.DisplayMember = "Name";
                cbField.ValueMember = "Name";

                cbOperator.DataSource = Op.GetList();
                cbOperator.DisplayMember = "Description";
                cbOperator.ValueMember = "Symbol";

                lbCriteria.DataSource = criteriaBuilder.CriterionItemList();

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
                colCboxDepartment.DataPropertyName = "Department";
                // The value member is the primary key of the parent table  
                colCboxDepartment.ValueMember = "Code";
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
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValue.Text))
            {
                CriterionItem cr = GetValidCriterionItem();
                if (cr != null)
                {
                    criteriaBuilder.AddCriterionItem(cr);
                    index++;
                }

                //refresh
                ListBoxRefresh();
            }
        }

        public void ListBoxRefresh()
        {
            lbCriteria.DataSource = null;
            lbCriteria.DataSource = criteriaBuilder.CriterionItemList();
        }

        private CriterionItem GetValidCriterionItem()
        {
            Field field = (Field)cbField.SelectedItem;
            Op Op = (Op)cbOperator.SelectedItem;
            string FValue = txtValue.Text;
            conjuction cj;

            string FieldType = field.Type;


            if (criteriaBuilder.IsFirstItem())
            {
                cj = conjuction.nil;
            }
            else
            {
                if (rbAnd.Checked)
                {
                    cj = conjuction.and;
                }
                else cj = conjuction.or;
            }

            switch (FieldType.ToLower())
            {
                case "string":
                    FValue = string.Format("{0}", FValue);
                    break;
                case "decimal":
                    decimal d;
                    if (!decimal.TryParse(FValue, out d))
                    {
                        lblMessage.Text = "Please enter a number in the field value";
                        return null;
                    }
                    break;
                case "date":
                    DateTime dd;
                    if (!DateTime.TryParse(FValue, out dd))
                    {
                        lblMessage.Text = "Please enter a date in the field value";
                        return null;
                    }
                    FValue = string.Format("{0}", FValue); //do a date format
                    break;
                case "like":
                    FValue = string.Format("{0}", FValue);
                    break;
            }


            //clean. no error
            Criterion cr = new Criterion(cj, field.Name, Op, FValue);
            return new CriterionItem("index" + index, cr);

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lbCriteria.SelectedItem != null)
            {
                CriterionItem selCriterionItem = (CriterionItem)lbCriteria.SelectedValue;
                criteriaBuilder.Remove(selCriterionItem);

                //refresh
                ListBoxRefresh();
            }
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            try
            {
                employees = de.GetEmployeesFromCriteria(criteriaBuilder.CriterionItemList());
                bindingSourceEmployees.DataSource = employees;                
                groupBoxResults.Text = employees.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnSubmit_Click(object sender, EventArgs e)
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }

    public class EmployeeSelectEventArgs : System.EventArgs
    {
        // add local member variables to hold text
        private DAL.Employee _employee;

        // class constructor
        public EmployeeSelectEventArgs(DAL.Employee employee)
        {
            this._employee = employee;
        } 
        
        // Properties - Viewable by each listener
        public DAL.Employee _Employee
        {
            get
            {
                return _employee;
            }
        }
    }


}
