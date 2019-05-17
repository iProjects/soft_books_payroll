using System; 
using System.Collections.Generic;
using System.Collections.ObjectModel; 
using System.ComponentModel;
using System.Configuration;
using System.Data; 
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing; 
using System.Linq;
using System.Text; 
using System.Windows.Forms;
using BLL.DataEntry; 
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class AddBankBranch : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.Bank _bank;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public AddBankBranch(DAL.Bank bank, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            if (bank == null)
                throw new ArgumentNullException("Bank");
            _bank = bank;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void InitializeControls()
        {
            try
            {
                if (_bank.BankCode != null)
                {
                    txtBankCode.Text = _bank.BankCode.ToString();
                    txtBankCode.Enabled = false;
                }
                if (_bank.BankName != null)
                {
                    txtBankName.Text = _bank.BankName;
                    txtBankName.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void AddBankBranch_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControls();

                string _BranchCode = Utils.NextSeries(NextBranchCode());
                txtBranchCode.Text = _BranchCode;

                AutoCompleteStringCollection acsccls = new AutoCompleteStringCollection();
                acsccls.AddRange(this.AutoComplete_BranchName());
                txtBranchName.AutoCompleteCustomSource = acsccls;
                txtBranchName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBranchName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscbrnchcd = new AutoCompleteStringCollection();
                acscbrnchcd.AddRange(this.AutoComplete_BranchCode());
                txtBranchCode.AutoCompleteCustomSource = acscbrnchcd;
                txtBranchCode.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBranchCode.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_BranchName()
        {
            try
            {
                var branchnamequery = from cs in db.BankBranches
                                      select cs.BranchName;
                return branchnamequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_BranchCode()
        {
            try
            {
                var branchcodequery = from cs in db.BankBranches
                                      select cs.BranchCode;
                return branchcodequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string NextBranchCode()
        {
            try
            {
                var cn = (from c in db.BankBranches
                          where c.Bank.BankCode == _bank.BankCode
                          orderby c.BranchCode descending
                          select c).FirstOrDefault();
                if (cn == null)
                    return "0";
                return cn.BranchCode.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return "0";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (is_Validate())
            {
                try
                {
                    string banksortcode = txtBankCode.Text + txtBranchCode.Text;

                    BankBranch _BankBranch = new BankBranch();
                    if (banksortcode != null)
                    {
                        _BankBranch.BankSortCode = banksortcode.ToString();
                    }
                    if (!string.IsNullOrEmpty(txtBranchCode.Text))
                    {
                        _BankBranch.BranchCode = txtBranchCode.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtBankCode.Text))
                    {
                        _BankBranch.Bank.BankCode = txtBankCode.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtBranchName.Text))
                    {
                        _BankBranch.BranchName = Utils.ConvertFirstLetterToUpper(txtBranchName.Text.Trim());
                    }

                    if (db.BankBranches.Any(c => c.Bank == _BankBranch.Bank && c.BranchCode == _BankBranch.BranchCode))
                    {
                        MessageBox.Show("Branch Code Exist!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.BankBranches.Any(c => c.Bank == _BankBranch.Bank && c.BranchCode == _BankBranch.BranchCode))
                    {
                        db.BankBranches.AddObject(_BankBranch);
                        db.SaveChanges();

                        BanksForm banks = (BanksForm)this.Owner;
                        banks.RefreshBranchGrid();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private bool is_Validate()
        {
            bool noerror = true;

            if (string.IsNullOrEmpty(txtBranchName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBranchName, "Name cannot be null!");
                return false;
            }

            if (string.IsNullOrEmpty(txtBranchCode.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBranchCode, "Code cannot be null!");
                return false;
            }
            return noerror;
        }
        private void txtBranchCode_KeyDown(object sender, KeyEventArgs e)
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
        private void txtBranchCode_KeyPress(object sender, KeyPressEventArgs e)
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





    }
}