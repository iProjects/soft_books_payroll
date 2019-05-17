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
    public partial class PayrollItems : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public PayrollItems(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void PayrollItems_Load(object sender, EventArgs e)
        {
            try
            {
                var itemtypesquery = from it in db.PayrollItemTypes
                                     select it;
                DataGridViewComboBoxColumn colCboxItemType = new DataGridViewComboBoxColumn();
                colCboxItemType.HeaderText = "Item Type";
                colCboxItemType.Name = "cbItemType";
                colCboxItemType.DataSource = itemtypesquery.ToList();
                // The display member is the name column in the column datasource  
                colCboxItemType.DisplayMember = "Description";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxItemType.DataPropertyName = "ItemTypeId";
                // The value member is the primary key of the parent table  
                colCboxItemType.ValueMember = "Id";
                colCboxItemType.MaxDropDownItems = 10;
                colCboxItemType.Width = 140;
                colCboxItemType.DisplayIndex = 1;
                colCboxItemType.MinimumWidth = 5;
                colCboxItemType.FlatStyle = FlatStyle.Flat;
                colCboxItemType.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxItemType.ReadOnly = true;
                if (!this.dataGridViewPayrollItems.Columns.Contains("cbItemType"))
                {
                    dataGridViewPayrollItems.Columns.Add(colCboxItemType);
                }

                var taxtrackingquery = from tt in db.TaxTrackings
                                       select tt;
                DataGridViewComboBoxColumn colCboxTaxTracking = new DataGridViewComboBoxColumn();
                colCboxTaxTracking.HeaderText = "Tax Tracking";
                colCboxTaxTracking.Name = "cbTaxTracking";
                colCboxTaxTracking.DataSource = taxtrackingquery.ToList();
                // The display member is the name column in the column datasource  
                colCboxTaxTracking.DisplayMember = "Description";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxTaxTracking.DataPropertyName = "TaxTrackingId";
                // The value member is the primary key of the parent table  
                colCboxTaxTracking.ValueMember = "Id";
                colCboxTaxTracking.MaxDropDownItems = 10;
                colCboxTaxTracking.Width = 140;
                colCboxTaxTracking.DisplayIndex = 2;
                colCboxTaxTracking.MinimumWidth = 5;
                colCboxTaxTracking.FlatStyle = FlatStyle.Flat;
                colCboxTaxTracking.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxTaxTracking.ReadOnly = true;
                if (!this.dataGridViewPayrollItems.Columns.Contains("cbTaxTracking"))
                {
                    dataGridViewPayrollItems.Columns.Add(colCboxTaxTracking);
                }

                var payrollitemsquery = from pi in rep.GetActivePayrollItems()
                                        select pi;
                bindingSourcePayrollItems.DataSource = payrollitemsquery.ToList();
                dataGridViewPayrollItems.AutoGenerateColumns = false;
                this.dataGridViewPayrollItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewPayrollItems.DataSource = bindingSourcePayrollItems;
                groupBox2.Text = bindingSourcePayrollItems.Count.ToString();
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Forms.AddPayrollItem f = new AddPayrollItem(connection) { Owner = this };
            f.ShowDialog();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayrollItems.SelectedRows.Count != 0)
            {
                try
                {

                    DAL.PayrollItem payrollitem = (DAL.PayrollItem)bindingSourcePayrollItems.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "NON_CASH_BENEFIT", "HOURLY_PAY", "ADVANCE" };
                    if (defaultItems.Contains(payrollitem.Id.Trim()))
                    {
                        MessageBox.Show("Cannot Edit Default Payroll Item " + payrollitem.Id.Trim(), "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Forms.EditPayrollItem f = new EditPayrollItem(payrollitem, connection) { Owner = this };
                        f.Text = payrollitem.Id.ToString().Trim().ToUpper();
                        f.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayrollItems.SelectedRows.Count != 0)
                {

                    DAL.PayrollItem payrollitem = (DAL.PayrollItem)bindingSourcePayrollItems.Current;

                    var _employeewithpayrollitemsquery = from br in rep.GetEmployeesWithPayrollItemTransactions(payrollitem)
                                                         select br;
                    List<DAL.Employee> _employees = _employeewithpayrollitemsquery.ToList();


                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "NON_CASH_BENEFIT", "HOURLY_PAY", "ADVANCE" };
                    if (defaultItems.Contains(payrollitem.Id.Trim()))
                    {
                        MessageBox.Show("Cannot Delete Default Payroll Item " + payrollitem.Id.Trim(), "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if ((_employees.Count > 0))
                    {
                        MessageBox.Show("There is an Employee Associated with this Payroll Item\nDelete the Employee first!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Delete Payroll Item\n " + payrollitem.Id.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            rep.DeletePayrollItem(payrollitem);
                            RefreshGrid();
                        }
                }
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
                if (chkInActive.Checked)
                {
                    bindingSourcePayrollItems.DataSource = null;
                    bindingSourcePayrollItems.DataSource = rep.GetAllPayrollItems();
                    groupBox2.Text = bindingSourcePayrollItems.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewPayrollItems.Rows)
                    {
                        dataGridViewPayrollItems.Rows[dataGridViewPayrollItems.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewPayrollItems.Rows.Count - 1;
                        bindingSourcePayrollItems.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourcePayrollItems.DataSource = null;
                    bindingSourcePayrollItems.DataSource = rep.GetActivePayrollItems();
                    groupBox2.Text = bindingSourcePayrollItems.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewPayrollItems.Rows)
                    {
                        dataGridViewPayrollItems.Rows[dataGridViewPayrollItems.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewPayrollItems.Rows.Count - 1;
                        bindingSourcePayrollItems.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayrollItems.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.PayrollItem payrollitem = (DAL.PayrollItem)bindingSourcePayrollItems.Current;
                    Forms.EditPayrollItem f = new EditPayrollItem(payrollitem, connection) { Owner = this };
                    f.Text = payrollitem.Id.ToString().Trim().ToUpper();
                    f.DisableControls();
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void dataGridViewPayrollItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewPayrollItems.SelectedRows.Count != 0)
            {
                try
                { 
                    DAL.PayrollItem payrollitem = (DAL.PayrollItem)bindingSourcePayrollItems.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "NON_CASH_BENEFIT", "HOURLY_PAY", "ADVANCE" };
                    if (defaultItems.Contains(payrollitem.Id.Trim()))
                    {
                        MessageBox.Show("Cannot Edit Default Payroll Item " + payrollitem.Id.Trim(), "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Forms.EditPayrollItem f = new EditPayrollItem(payrollitem, connection) { Owner = this };
                        f.Text = payrollitem.Id.ToString().Trim().ToUpper();
                        f.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void chkInActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkInActive.Checked)
                {
                    bindingSourcePayrollItems.DataSource = null;
                    bindingSourcePayrollItems.DataSource = rep.GetAllPayrollItems();
                    groupBox2.Text = bindingSourcePayrollItems.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewPayrollItems.Rows)
                    {
                        dataGridViewPayrollItems.Rows[dataGridViewPayrollItems.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewPayrollItems.Rows.Count - 1;
                        bindingSourcePayrollItems.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourcePayrollItems.DataSource = null;
                    bindingSourcePayrollItems.DataSource = rep.GetActivePayrollItems();
                    groupBox2.Text = bindingSourcePayrollItems.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewPayrollItems.Rows)
                    {
                        dataGridViewPayrollItems.Rows[dataGridViewPayrollItems.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewPayrollItems.Rows.Count - 1;
                        bindingSourcePayrollItems.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewPayrollItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }






    }
}