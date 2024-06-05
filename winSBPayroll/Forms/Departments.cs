using System;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;
using System.Collections.Generic;
using System.Linq;

namespace winSBPayroll.Forms
{
    public partial class Departments : Form
    {

        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public Departments(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewDepartments.AutoGenerateColumns = false;
                this.dataGridViewDepartments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourceDepartments.DataSource = rep.GetNonDeletedDepartments();
                dataGridViewDepartments.DataSource = bindingSourceDepartments;
                groupBox2.Text = bindingSourceDepartments.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        public void RefreshGrid()
        {
            try
            {
                //set the datasource to null
                bindingSourceDepartments.DataSource = null;
                //set the datasource to a method
                bindingSourceDepartments.DataSource = rep.GetNonDeletedDepartments();
                groupBox2.Text = bindingSourceDepartments.Count.ToString();
                foreach (DataGridViewRow row in dataGridViewDepartments.Rows)
                {
                    dataGridViewDepartments.Rows[dataGridViewDepartments.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridViewDepartments.Rows.Count - 1;
                    bindingSourceDepartments.Position = nRowIndex;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AddDepartment ad = new Forms.AddDepartment(connection) { Owner = this };
                ad.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewDepartments.SelectedRows.Count != 0)
                {
                    DAL.Department dep = (DAL.Department)bindingSourceDepartments.Current;
                    Forms.EditDepartment f = new EditDepartment(dep, connection) { Owner = this };
                    f.Text = dep.Description.ToString().Trim().ToUpper();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewDepartments.SelectedRows.Count != 0)
                {
                    DAL.Department dep = (DAL.Department)bindingSourceDepartments.Current;

                    var _employeewithdepartmentquery = from br in rep.GetEmployeesWithDepartment(dep)
                                                       select br;
                    List<DAL.Employee> _employees = _employeewithdepartmentquery.ToList();

                    if (_employees.Count > 0)
                    {
                        MessageBox.Show("There is an Employee Associated with this Department\nDelete the Employee first!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Department\n" + dep.Description.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            rep.DeleteDepartment(dep);
                            RefreshGrid();
                        }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewDepartments_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewDepartments.SelectedRows.Count != 0)
                {
                    DAL.Department dep = (DAL.Department)bindingSourceDepartments.Current;
                    Forms.EditDepartment f = new EditDepartment(dep, connection) { Owner = this };
                    f.Text = dep.Description.ToString().Trim().ToUpper();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }



    }
}