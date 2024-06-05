using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class EmployeeTransactionsForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;

        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public EmployeeTransactionsForm(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished EmployeeTransactionsForm initialization", TAG));
        }

        private void EmployeeTransactionsForm_Load(object sender, EventArgs e)
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
                colCboxEmployeeOtherNames.DisplayIndex = 4;
                colCboxEmployeeOtherNames.MinimumWidth = 5;
                colCboxEmployeeOtherNames.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colCboxEmployeeOtherNames.FlatStyle = FlatStyle.Flat;
                colCboxEmployeeOtherNames.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployeeOtherNames.ReadOnly = true;

                if (!this.dataGridView_employee_txn.Columns.Contains("colCboxEmployeeOtherNames"))
                {
                    dataGridView_employee_txn.Columns.Add(colCboxEmployeeOtherNames);
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
                colCboxEmployeeSurname.DisplayIndex = 5;
                colCboxEmployeeSurname.MinimumWidth = 5;
                colCboxEmployeeSurname.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colCboxEmployeeSurname.FlatStyle = FlatStyle.Flat;
                colCboxEmployeeSurname.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployeeSurname.ReadOnly = true;

                if (!this.dataGridView_employee_txn.Columns.Contains("colCboxEmployeeSurname"))
                {
                    dataGridView_employee_txn.Columns.Add(colCboxEmployeeSurname);
                }

                var _payrollitem_query = from pi in rep.GetActivePayrollItems()
                                         where pi.Enable == true
                                         where pi.Active == true
                                         where pi.IsDeleted == false
                                         select pi;

                List<DAL.PayrollItem> _payrollitem_lst = _payrollitem_query.ToList();
                cbopayrollitem.DisplayMember = "Id";
                cbopayrollitem.ValueMember = "Id";
                cbopayrollitem.DataSource = _payrollitem_lst;

                var _employeesquery = (from pi in rep.get_all_employee_transactions()
                                        select pi.EmpNo).Distinct();

                List<string> _employees_lst = _employeesquery.ToList();
                cboemployeeno.DisplayMember = "EmpNo";
                cboemployeeno.ValueMember = "EmpNo";
                cboemployeeno.DataSource = _employees_lst;

                dataGridView_employee_txn.AutoGenerateColumns = false;
                this.dataGridView_employee_txn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                RefreshGrid();
                
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished EmployeeTransactionsForm load", TAG));
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
                var txn = from t in rep.get_all_employee_transactions()
                          orderby t.Id descending
                          select t;

                bindingSource_employee_txn.DataSource = txn.ToList();
                dataGridView_employee_txn.DataSource = bindingSource_employee_txn;
                groupBox2.Text = bindingSource_employee_txn.Count.ToString();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + txn.ToList().Count + " ] records.", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        public void RefreshGrid()
        {
            filter_employee_transactions("pe");
        }

        private void cbopayrollitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter_employee_transactions("p");
        }

        private void cboemployeeno_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter_employee_transactions("e");
        }

        private void chkfor_CheckedChanged(object sender, EventArgs e)
        {
            filter_employee_transactions("pe");
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            filter_employee_transactions("pe");
        }

        private void filter_employee_transactions(string sender)
        {
            try
            {
                if (cbopayrollitem.SelectedIndex != -1 && cboemployeeno.SelectedIndex != -1)
                {
                    DAL.PayrollItem _payrollitem = (DAL.PayrollItem)cbopayrollitem.SelectedItem;
                    var _selected_payrollitem = _payrollitem.Id;

                    string _employeeno = (string)cboemployeeno.SelectedItem;
                    var _selected_employee = _employeeno;

                    var is_for = chkfor.Checked;

                    var is_enabled = chkEnabled.Checked;

                    var _transactions = from t in db.EmployeeTransactions
                                        orderby t.Id descending
                                        select t;

                    if (is_enabled)
                    {
                        _transactions = from t in _transactions
                                        where t.Enabled.Equals(is_enabled)
                                        orderby t.Id descending
                                        select t;
                    }

                    if (is_for)
                    {
                        _transactions = (from t in _transactions
                                         where t.ItemId.Equals(_selected_payrollitem)
                                         where t.EmpNo.Equals(_selected_employee)
                                         orderby t.Id descending
                                         select t);
                    }
                    else
                    {
                        if (sender.Equals("p"))
                        {
                            _transactions = from t in _transactions
                                            where t.ItemId.Equals(_selected_payrollitem)
                                            orderby t.Id descending
                                            select t;
                        }
                        else if (sender.Equals("e"))
                        {
                            _transactions = from t in _transactions
                                            where t.EmpNo.Equals(_selected_employee)
                                            orderby t.Id descending
                                            select t;
                        }
                        else
                        {
                            _transactions = from t in _transactions
                                            orderby t.Id descending
                                            select t;
                        }
                    }

                    var _lst_employee_txn = _transactions.ToList();

                    bindingSource_employee_txn.DataSource = _lst_employee_txn;
                    dataGridView_employee_txn.DataSource = bindingSource_employee_txn;
                    groupBox2.Text = bindingSource_employee_txn.Count.ToString();

                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("filtered [ " + _lst_employee_txn.Count + " ] records.", TAG));
                }
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

        private void btndetails_Click(object sender, EventArgs e)
        {
            if (dataGridView_employee_txn.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.EmployeeTransaction _emptxn = (DAL.EmployeeTransaction)bindingSource_employee_txn.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "NON_CASH_BENEFIT", "HOURLY_PAY", "ADVANCE" };
                    //if (defaultItems.Contains(_emptxn.Id.ToString().Trim()))
                    //{
                    //    MessageBox.Show("Cannot Edit Default Payroll Item " + _emptxn.Id.ToString().Trim(), Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    emptxndetailsform emptxnfrm = new emptxndetailsform(_user, connection, _notificationmessageEventname, _emptxn) { Owner = this };
                    emptxnfrm.Text = _emptxn.Id.ToString().Trim().ToUpper();
                    emptxnfrm.ShowDialog();
                    //}
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.ShowError(ex);
                }
            }
        }

        private void dataGridView_employee_txn_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView_employee_txn.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.EmployeeTransaction _emptxn = (DAL.EmployeeTransaction)bindingSource_employee_txn.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "NON_CASH_BENEFIT", "HOURLY_PAY", "ADVANCE" };
                    //if (defaultItems.Contains(_emptxn.Id.ToString().Trim()))
                    //{
                    //    MessageBox.Show("Cannot Edit Default Payroll Item " + _emptxn.Id.ToString().Trim(), Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    emptxndetailsform emptxnfrm = new emptxndetailsform(_user, connection, _notificationmessageEventname, _emptxn) { Owner = this };
                    emptxnfrm.Text = _emptxn.Id.ToString().Trim().ToUpper();
                    emptxnfrm.ShowDialog();
                    //}
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.ShowError(ex);
                }
            }
        }

        private void dataGridView_employee_txn_DataError(object sender, DataGridViewDataErrorEventArgs e)
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




    }
}
