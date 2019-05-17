using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class Benefits : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        public int _benefitid;

        public Benefits(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        public Benefits(int benefitid, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _benefitid = benefitid;
        }

        private void Benefits_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewBenefits.AutoGenerateColumns = false;
                this.dataGridViewBenefits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourceBenefits.DataSource = rep.GetNonDeletedBenefits();
                dataGridViewBenefits.DataSource = bindingSourceBenefits;
                groupBox2.Text = bindingSourceBenefits.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public void RefreshGrid()
        {
            //set the datasource to null
            bindingSourceBenefits.DataSource = null;
            //set the datasource to a method
            bindingSourceBenefits.DataSource = rep.GetNonDeletedBenefits();
            groupBox2.Text = bindingSourceBenefits.Count.ToString();
            foreach (DataGridViewRow row in dataGridViewBenefits.Rows)
            {
                dataGridViewBenefits.Rows[dataGridViewBenefits.Rows.Count - 1].Selected = true;
                int nRowIndex = dataGridViewBenefits.Rows.Count - 1;
                bindingSourceBenefits.Position = nRowIndex;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Forms.AddBenefit ab = new Forms.AddBenefit(connection) { Owner = this };
            ab.ShowDialog();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewBenefits.SelectedRows.Count != 0)
                {
                    DAL.Benefit ben = (DAL.Benefit)bindingSourceBenefits.Current;

                    var _employeewithdepartmentquery = from br in rep.GetEmployeesWithBenefit(ben)
                                                       select br;
                    List<DAL.Employee> _employees = _employeewithdepartmentquery.ToList();

                    if (_employees.Count > 0)
                    {
                        MessageBox.Show("There is an Employee Associated with this Benefit\nDelete the Employee first!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Benefit\n" + ben.Description.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            rep.DeleteBenefit(ben);
                            RefreshGrid();
                        }
                }
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
                if (dataGridViewBenefits.SelectedRows.Count != 0)
                {
                    DAL.Benefit ben = (DAL.Benefit)bindingSourceBenefits.Current;

                    Forms.EditBenefit ab = new Forms.EditBenefit(ben, connection) { Owner = this };
                    ab.Text = ben.Description.ToString();
                    ab.Show();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewBenefits_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewBenefits.SelectedRows.Count != 0)
                {
                    DAL.Benefit ben = (DAL.Benefit)bindingSourceBenefits.Current;

                    Forms.EditBenefit ab = new Forms.EditBenefit(ben, connection) { Owner = this };
                    ab.Text = ben.Description.ToString();
                    ab.Show();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }




    }
}