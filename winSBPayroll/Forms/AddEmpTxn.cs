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
    public partial class AddEmpTxn : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.Employee employee;
        string _User;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public AddEmpTxn(DAL.Employee emp, string user, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            employee = emp;

            _User = user;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsEmployeeTransactionValid())
            {
                try
                {
                    var _employerquery = (from ep in db.Employers
                                          select ep).FirstOrDefault();
                    DAL.Employer _Employer = _employerquery;

                    EmployeeTransaction et = new EmployeeTransaction();
                    decimal _amount;
                    if (!string.IsNullOrEmpty(txtAmount.Text) && decimal.TryParse(txtAmount.Text, out _amount))
                    {
                        et.Amount = decimal.Parse(txtAmount.Text.Trim());
                    }
                    et.PostDate = DateTime.Today;
                    et.EmployeeId = employee.Id;
                    if (employee.EmpNo != null)
                    {
                        et.EmpNo = employee.EmpNo;
                    }
                    if (cbItemId.SelectedIndex != -1)
                    {
                        et.ItemId = cbItemId.SelectedValue.ToString();
                    }
                    et.Recurrent = chkRecurrent.Checked;
                    et.Processed = false;
                    et.InitialAmount = 0M;
                    et.AccumulativePayment = 0M;
                    et.Enabled = true;
                    et.TrackYTD = chkTrackYTD.Checked;
                    et.CreatedBy = _User;
                    et.LastChangeDate = DateTime.Today;
                    et.LastChangedBy = _User;
                    if (_Employer.AuthorizedSignatory != null)
                    {
                        et.AuthorizedBy = _Employer.AuthorizedSignatory;
                    }
                    et.AuthorizeDate = DateTime.Today;
                    decimal _YTDAmount;
                    if (!string.IsNullOrEmpty(txtYTDAmount.Text) && decimal.TryParse(txtYTDAmount.Text, out _YTDAmount))
                    {
                        et.Balance = _YTDAmount;
                    }
                    et.ShowYTDInPayslip = chkShowYTDinPayslip.Checked;

                    if (db.EmployeeTransactions.Any(i => i.EmpNo == et.EmpNo && i.ItemId == et.ItemId))
                    {
                        MessageBox.Show("Employee Transaction for item \n" + et.ItemId + "Exists!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.EmployeeTransactions.Any(i => i.EmpNo == et.EmpNo && i.ItemId == et.ItemId))
                    {
                        db.EmployeeTransactions.AddObject(et);
                        db.SaveChanges();
                    }

                    EditEmployee f = (EditEmployee)this.Owner;
                    f.GridRefresh();
                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            } 
        }
        private bool IsEmployeeTransactionValid()
        {
            bool no_error = true;
            if (cbItemId.SelectedIndex == -1)
            {
                errorProvider1.Clear(); //Clear all Error Messages
                errorProvider1.SetError(cbItemId, "Select a payroll Item!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmount, "Amount cannot be null!");
                return false;
            }
            decimal Amount;
            if (!decimal.TryParse(txtAmount.Text, out Amount))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmount, "Amount must be Decimal!");
                return false;
            }
            decimal YTDAmount;
            if (!string.IsNullOrEmpty(txtYTDAmount.Text) && !decimal.TryParse(txtYTDAmount.Text, out YTDAmount))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtYTDAmount, "Year To Date Amount must be Decimal!");
                return false;
            }
            if (cbItemId.SelectedIndex != -1)
            {
                DAL.PayrollItem _payrollitem = (DAL.PayrollItem)cbItemId.SelectedItem;

                switch (_payrollitem.ItemTypeId.Trim())
                {
                    case "LOAN":

                        if (string.IsNullOrEmpty(txtYTDAmount.Text))
                        {
                            errorProvider1.Clear();
                            errorProvider1.SetError(txtYTDAmount, "Year To Date Amount cannot be null for Loans!");
                            return false;
                        } 
                        if (!string.IsNullOrEmpty(txtYTDAmount.Text))
                        {
                            decimal ytd = decimal.Parse(txtYTDAmount.Text);
                            if (ytd > 0)
                            {
                                errorProvider1.Clear();
                                errorProvider1.SetError(txtYTDAmount, "Year To Date Amount must be set as Negative for Loans e.g  -25000");
                                return false;
                            }
                        }
                        break;
                }
            }
            return no_error;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        private void AddEmpTxn_Load(object sender, EventArgs e)
        {
            try
            {
                cbItemId.DataSource = de.GetPayrollItems(employee.EmpNo);
                cbItemId.DisplayMember = "Id";
                cbItemId.ValueMember = "Id";
                cbItemId.SelectedIndex = -1;

                chkEnabled.Checked = true;

                txtAmount.Text = "0";
                txtYTDAmount.Text = "0";
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void cbItemId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ("HOURLY_PAY".Equals(cbItemId.Text.Trim()))
                {
                    this.txtAmount.Enabled = false;
                    HrlyPay f = new HrlyPay(employee, connection) { Owner = this };
                    f.OnEmployeeHrlyAmountChanged += new HrlyPay.HrlyAmountHandler(f_OnEmployeeHrlyAmountChanged);
                    f.ShowDialog(this);
                }
                else if ("NON_CASH_BENEFIT".Equals(cbItemId.Text.Trim()))
                {
                    this.txtAmount.Enabled = false;
                    NonCashBenefits b = new NonCashBenefits(employee, connection) { Owner = this };
                    b.OnEmployeeBenefitAmountChanged += new NonCashBenefits.BenefitAmountHandler(b_OnEmployeeBenefitAmountChanged);
                    b.ShowDialog(this);
                }
                else
                {
                    this.txtAmount.Enabled = true;
                }

                if (cbItemId.SelectedIndex != -1)
                {
                    DAL.PayrollItem _payrollitem = (DAL.PayrollItem)cbItemId.SelectedItem;

                    switch (_payrollitem.ItemTypeId.Trim())
                    {
                        case "LOAN":
                            txtYTDAmount.Text = string.Empty;
                            chkEnabled.Checked = true;
                            chkRecurrent.Checked = true;
                            chkShowYTDinPayslip.Checked = true;
                            chkTrackYTD.Checked = true;
                            break;
                        case "SACCO": 
                            chkEnabled.Checked = true;
                            chkRecurrent.Checked = true;
                            chkShowYTDinPayslip.Checked = true;
                            chkTrackYTD.Checked = true;
                            break;
                        default:
                            chkEnabled.Checked = true;
                            chkRecurrent.Checked = true;
                            chkShowYTDinPayslip.Checked = true;
                            chkTrackYTD.Checked = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void b_OnEmployeeBenefitAmountChanged(object sender, BenefitAmountHandlerEventArgs e)
        {
            this.txtAmount.Text = e.Totalquantity.ToString();
        }

        private void f_OnEmployeeHrlyAmountChanged(object sender, HrlyAmountHandlerEventArgs e)
        {
            this.txtAmount.Text = e.TotalHrlyAmount.ToString();
        }
        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
            //If shift key was pressed, it'st not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }
        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (nonNumberEntered == true)
                {
                    if (e.KeyChar == 13)
                    {

                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtYTDAmount_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
            //If shift key was pressed, it'st not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }
        private void txtYTDAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (nonNumberEntered == true)
                {
                    if (e.KeyChar == 13)
                    {

                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        } 
        private void txtYTDAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtYTDAmount.Text))
                {
                    chkTrackYTD.Checked = true;
                    chkShowYTDinPayslip.Checked = true;
                }
                if (string.IsNullOrEmpty(txtYTDAmount.Text))
                {
                    chkTrackYTD.Checked = false;
                    chkShowYTDinPayslip.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
         


    }
}