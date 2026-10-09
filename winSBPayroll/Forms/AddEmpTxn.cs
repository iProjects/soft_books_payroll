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

        private void AddEmpTxn_Load(object sender, EventArgs e)
        {
            try
            {
                var PayrollItem_query = from it in de.GetPayrollItems(employee.EmpNo)
                                        select it;
                List<DAL.PayrollItem> _lst_PayrollItem_querys = PayrollItem_query.ToList();
                _lst_PayrollItem_querys.Insert(0, new DAL.PayrollItem { Id = "---Select---", Description = "---Select---" });
                cbItemId.DataSource = _lst_PayrollItem_querys.ToList();
                cbItemId.DisplayMember = "Id";
                cbItemId.ValueMember = "Id";
                cbItemId.SelectedIndex = 0;

                chkEnabled.Checked = true;

                txtAmount.Text = "0";
                txtYTDAmount.Text = "0";

                txt_item_type.Enabled = false;
                txt_tax_tracking.Enabled = false;
                txt_item_type.Text = string.Empty;
                txt_tax_tracking.Text = string.Empty;
                lbl_ytd_amount.Text = "Year To Date Amount";
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider.Clear(); //Clear all Error Messages
            if (IsEmployeeTransactionValid())
            {
                try
                {
                    decimal _amount = 0;
                    if (!string.IsNullOrEmpty(txtAmount.Text) && decimal.TryParse(txtAmount.Text, out _amount))
                    {

                    }

                    if (!string.IsNullOrEmpty(txt_item_type.Text) && txt_item_type.Text.Equals("DEDUCTION") || txt_item_type.Text.Equals("LOAN") || txt_item_type.Text.Equals("SACCO"))
                    {
                        // -------------------------------------------------------------
                        // 2/3 Payroll Rule Validation Check
                        // -------------------------------------------------------------
                        if (!FulfillsTwoThirdsPayrollRule(_amount))
                        {
                            if (DialogResult.Yes == MessageBox.Show("Transaction violates the 2/3 Payroll Rule.\n" +
                                "Total deductions cannot exceed two-thirds (66.67%) of the employee's basic pay. \n Do you wish to proceed", Utils.APP_NAME, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning))
                            {
                                create_transaction();
                            }
                        }
                    }
                    else
                    {
                        create_transaction();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        void create_transaction()
        {
            EmployeeTransaction et = new EmployeeTransaction();

            var _employerquery = (from ep in db.Employers
                                  select ep).FirstOrDefault();
            DAL.Employer _Employer = _employerquery;

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
            decimal _amount;
            if (!string.IsNullOrEmpty(txtAmount.Text) && decimal.TryParse(txtAmount.Text, out _amount))
            {
                et.Amount = _amount;
                et.InitialAmount = _amount;
            }            
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
                MessageBox.Show("Employee Transaction for item \n" + et.ItemId + "Exists!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (!db.EmployeeTransactions.Any(i => i.EmpNo == et.EmpNo && i.ItemId == et.ItemId))
            {
                db.EmployeeTransactions.AddObject(et);
                db.SaveChanges();
            }

            EditEmployee f = (EditEmployee)this.Owner;
            f.refresh_emptxn_grid();
            this.Close();
        }

        private bool IsEmployeeTransactionValid()
        {
            bool no_error = true;
            if (cbItemId.SelectedIndex == 0)
            {
                errorProvider.SetError(cbItemId, "Select a payroll Item!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                errorProvider.SetError(txtAmount, "Amount cannot be null!");
                no_error = false;
            }
            decimal Amount;
            if (!decimal.TryParse(txtAmount.Text, out Amount))
            {
                errorProvider.SetError(txtAmount, "Amount must be Decimal!");
                no_error = false;
            }
            decimal YTDAmount;
            if (!string.IsNullOrEmpty(txtYTDAmount.Text) && !decimal.TryParse(txtYTDAmount.Text, out YTDAmount))
            {
                errorProvider.SetError(txtYTDAmount, "Year To Date Amount must be Decimal!");
                no_error = false;
            }
            if (cbItemId.SelectedIndex != 0)
            {
                DAL.PayrollItem _payrollitem = (DAL.PayrollItem)cbItemId.SelectedItem;

                switch (_payrollitem.ItemTypeId.Trim())
                {
                    case "LOAN":

                        if (string.IsNullOrEmpty(txtYTDAmount.Text))
                        {
                            errorProvider.SetError(txtYTDAmount, "Year To Date Amount cannot be null for Loans!");
                            no_error = false;
                        }
                        if (!string.IsNullOrEmpty(txtYTDAmount.Text))
                        {
                            decimal ytd = decimal.Parse(txtYTDAmount.Text);
                            if (ytd > 0)
                            {
                                errorProvider.SetError(txtYTDAmount, "Year To Date Amount must be set as Negative for Loans e.g  -25000");
                                no_error = false;
                            }
                        }
                        break;
                }
            }
            return no_error;
        }

        /// <summary>
        /// Verifies whether adding newDeductionAmount satisfies the 2/3 Payroll Rule (.NET 4 / C# 4 Compatible).
        /// </summary>
        private bool FulfillsTwoThirdsPayrollRule(decimal newDeductionAmount)
        {
            // 1. Get employee basic pay (assuming basic pay field is BasicPay on DAL.Employee)
            decimal basicPay = employee.BasicPay ?? 0M;

            if (basicPay <= 0M)
            {
                // If basic pay is not set, allow or fail based on business logic. 
                // Returning true here to avoid blocking zero-salary edge cases if handled elsewhere.
                return true;
            }

            // 2. Maximum total deduction allowed (2/3 of Basic Salary)
            decimal maxAllowedDeductions = (2M / 3M) * basicPay;

            // 3. Sum existing active/recurrent deductions for this employee
            string empNo = employee.EmpNo;

            // Sum existing transaction amounts registered under employee
            decimal existingDeductions = db.EmployeeTransactions
                .Where(t => t.EmpNo == empNo && t.Enabled == true)
                .ToList()
                .Select(t => t.Amount)
                .Sum();

            // 4. Calculate proposed total deductions
            decimal totalProposedDeductions = existingDeductions + Math.Abs(newDeductionAmount);

            // 5. Rule condition: Total deductions must be <= 2/3 of basic pay
            return totalProposedDeductions <= maxAllowedDeductions;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbItemId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txt_item_type.Text = string.Empty;
                txt_tax_tracking.Text = string.Empty;
                lbl_ytd_amount.Text = "Year To Date Amount";

                if (cbItemId.SelectedIndex != 0)
                {
                    DAL.PayrollItem _payrollitem = (DAL.PayrollItem)cbItemId.SelectedItem;

                    txt_item_type.Text = _payrollitem.ItemTypeId;
                    txt_tax_tracking.Text = _payrollitem.TaxTrackingId;

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
                        NonCashBenefitsForm b = new NonCashBenefitsForm(employee, connection) { Owner = this };
                        b.OnEmployeeBenefitAmountChanged += new NonCashBenefitsForm.BenefitAmountHandler(b_OnEmployeeBenefitAmountChanged);
                        b.ShowDialog(this);
                    }
                    else
                    {
                        this.txtAmount.Enabled = true;
                    }

                    switch (_payrollitem.ItemTypeId.Trim())
                    {
                        case "LOAN":
                            txtYTDAmount.Text = string.Empty;
                            chkEnabled.Checked = true;
                            chkRecurrent.Checked = true;
                            chkShowYTDinPayslip.Checked = true;
                            chkTrackYTD.Checked = true;
                            lbl_ytd_amount.Text = "(Year To Date Amount)";
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