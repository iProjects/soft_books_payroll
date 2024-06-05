using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class EditEmpTxn : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.EmployeeTransaction _empTxn;
        string _User;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public EditEmpTxn(DAL.EmployeeTransaction empTxn, string User, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _User = User;
            _empTxn = empTxn;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (IsEmployeeTransactionValid())
            {
                try
                { 
                    decimal _amount;
                    if (!string.IsNullOrEmpty(txtAmount.Text) && decimal.TryParse(txtAmount.Text, out _amount))
                    {
                        _empTxn.Amount = _amount;
                    }
                    decimal _YTDAmount;
                    if (!string.IsNullOrEmpty(txtYTDAmount.Text) && decimal.TryParse(txtYTDAmount.Text, out _YTDAmount))
                    {
                        _empTxn.Balance = _YTDAmount;
                    }  
                    _empTxn.Recurrent = chkRecurrent.Checked;
                    _empTxn.Enabled = chkEnabled.Checked;
                    _empTxn.TrackYTD = chkTrackYTD.Checked; 
                    _empTxn.LastChangeDate = DateTime.Today;
                    _empTxn.LastChangedBy = _User;  
                    _empTxn.ShowYTDInPayslip = chkShowYTDinPayslip.Checked;

                    rep.UpdateEmpTxn(_empTxn);

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
        private void EditEmpTxn_Load(object sender, EventArgs e)
        {
            try
            {
                cbItemId.DataSource = de.GetPayrollItems();
                cbItemId.DisplayMember = "Id";
                cbItemId.ValueMember = "Id";

                InitializeControls();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void InitializeControls()
        {
            try
            {
                cbItemId.Enabled = false;
                cbItemId.SelectedValue = _empTxn.ItemId.Trim();
                txtAmount.Text = _empTxn.Amount.ToString();
                chkRecurrent.Checked = _empTxn.Recurrent;
                chkEnabled.Checked = _empTxn.Enabled;
                chkTrackYTD.Checked = _empTxn.TrackYTD.Value;
                chkShowYTDinPayslip.Checked = _empTxn.ShowYTDInPayslip.Value;
                txtYTDAmount.Text = _empTxn.Balance.ToString();
                chkProcessed.Checked = _empTxn.Processed.Value;
                chkProcessed.Enabled = false;
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
                //decimal ytd=decimal.Parse(txtYTDAmount.Text);

                //if (!string.IsNullOrEmpty(txtYTDAmount.Text) && ytd >0)
                //{
                //    chkTrackYTD.Checked = true;
                //    chkShowYTDinPayslip.Checked = true;
                //}
                //if (string.IsNullOrEmpty(txtYTDAmount.Text))
                //{
                //    chkTrackYTD.Checked = false;
                //    chkShowYTDinPayslip.Checked = false;
                //}
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
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
        public void DisableControls()
        {
            try
            {
                groupBox2.Enabled = false;
                btnUpdate.Enabled = false;
                btnUpdate.Visible = false;
                btnClose.Location = btnUpdate.Location;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }







    }
}