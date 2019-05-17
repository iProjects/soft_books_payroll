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
    public partial class Employer : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public Employer(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AddEmployer ae = new Forms.AddEmployer(connection) { Owner = this };
                ae.CheckForDefaultBank();
                ae.ShowDialog();
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
                if (dataGridViewEmployer.SelectedRows.Count != 0)
                {
                    DAL.Employer emp = (DAL.Employer)bindingSourceEmployer.Current;
                    Forms.EditEmployer f = new EditEmployer(emp, connection) { Owner = this };
                    f.Text = emp.Name.ToString().Trim().ToUpper();
                    f.CheckForDefaultBank();
                    f.Show();
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
        public void RefreshGrid()
        {
            try
            {
                bindingSourceEmployer.DataSource = null;
                if (chkInActive.Checked)
                {
                    //set the datasource to null
                    bindingSourceEmployer.DataSource = null;
                    //set the datasource to a method
                    bindingSourceEmployer.DataSource = rep.GetAllEmployers();
                    groupBox2.Text = bindingSourceEmployer.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmployer.Rows)
                    {
                        dataGridViewEmployer.Rows[dataGridViewEmployer.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmployer.Rows.Count - 1;
                        bindingSourceEmployer.Position = nRowIndex;
                    }
                }
                else
                {
                    //set the datasource to null
                    bindingSourceEmployer.DataSource = null;
                    //set the datasource to a method
                    bindingSourceEmployer.DataSource = rep.GetAllActiveEmployers();
                    groupBox2.Text = bindingSourceEmployer.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmployer.Rows)
                    {
                        dataGridViewEmployer.Rows[dataGridViewEmployer.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmployer.Rows.Count - 1;
                        bindingSourceEmployer.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void Employer_Load(object sender, EventArgs e)
        {
            try
            {
                List<DAL.Employer> employers = rep.GetAllActiveEmployers();
                bindingSourceEmployer.DataSource = employers;
                dataGridViewEmployer.AutoGenerateColumns = false;
                this.dataGridViewEmployer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewEmployer.DataSource = bindingSourceEmployer;
                groupBox2.Text = bindingSourceEmployer.Count.ToString(); 
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewEmployer_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewEmployer.SelectedRows.Count != 0)
                {
                    DAL.Employer emp = (DAL.Employer)bindingSourceEmployer.Current;

                    Forms.EditEmployer f = new EditEmployer(emp, connection) { Owner = this };
                    f.Text = emp.Name.ToString().Trim().ToUpper();
                    f.Show();
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
                if (dataGridViewEmployer.SelectedRows.Count != 0)
                {
                    DAL.Employer emp = (DAL.Employer)bindingSourceEmployer.Current;

                    var _Employeesquery = from em in rep.GetAllActiveEmployees()
                                          where em.EmployerId == emp.Id
                                          where em.IsDeleted == false
                                          select em;
                    List<Employee> _Employees = _Employeesquery.ToList();

                    var _EmployerBanksquery = from em in db.EmployerBanks
                                              select em;
                    List<EmployerBank> _EmployerBanks = _EmployerBanksquery.ToList();

                    if (_Employees.Count > 0)
                    {
                        MessageBox.Show("There is an Employee Associated with this Employer.\n Delete the Employee First!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (_EmployerBanks.Count > 0)
                    {
                        MessageBox.Show("There is an Employer Bank Associated with this Employer.\n Delete the Employer Bank First!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Employer\n" + emp.Name.Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            rep.DeleteEmployer(emp);
                            RefreshGrid();
                        }
                    }
                }
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
                bindingSourceEmployer.DataSource = null;
                if (chkInActive.Checked)
                {
                    //set the datasource to null
                    bindingSourceEmployer.DataSource = null;
                    //set the datasource to a method
                    bindingSourceEmployer.DataSource = rep.GetAllEmployers();
                    groupBox2.Text = bindingSourceEmployer.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmployer.Rows)
                    {
                        dataGridViewEmployer.Rows[dataGridViewEmployer.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmployer.Rows.Count - 1;
                        bindingSourceEmployer.Position = nRowIndex;
                    }
                }
                else
                {
                    //set the datasource to null
                    bindingSourceEmployer.DataSource = null;
                    //set the datasource to a method
                    bindingSourceEmployer.DataSource = rep.GetAllActiveEmployers();
                    groupBox2.Text = bindingSourceEmployer.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmployer.Rows)
                    {
                        dataGridViewEmployer.Rows[dataGridViewEmployer.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmployer.Rows.Count - 1;
                        bindingSourceEmployer.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        





























    }
}