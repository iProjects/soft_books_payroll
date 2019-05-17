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
using DAL.Criteria;

namespace winSBPayroll.Forms
{
    public partial class SearchBanksSimpleForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        CriteriaBuilder criteriaBuilder = new CriteriaBuilder();
        IQueryable<DAL.vwBankBranch> _Banks;
        //delegate
        public delegate void BankSelectHandler(object sender, BankSelectEventArgs e);
        //event
        public event BankSelectHandler OnBankListSelected;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;


        public SearchBanksSimpleForm(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

        }

        private void SearchBanksSimpleForm_Load(object sender, EventArgs e)
        {
            try
            {
                _Banks = db.vwBankBranches;

                dataGridViewBanks.AutoGenerateColumns = false;
                this.dataGridViewBanks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewBanks.DataSource = bindingSourceBanks;
                groupBoxResults.Text = bindingSourceBanks.Count.ToString();

                AutoCompleteStringCollection acscBankCode = new AutoCompleteStringCollection();
                acscBankCode.AddRange(this.AutoComplete_BankCodes());
                txtBankCode.AutoCompleteCustomSource = acscBankCode;
                txtBankCode.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBankCode.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscBankName = new AutoCompleteStringCollection();
                acscBankName.AddRange(this.AutoComplete_BankNames());
                txtBankName.AutoCompleteCustomSource = acscBankName;
                txtBankName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBankName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscBranchCode = new AutoCompleteStringCollection();
                acscBranchCode.AddRange(this.AutoComplete_BranchCodes());
                txtBranchCode.AutoCompleteCustomSource = acscBranchCode;
                txtBranchCode.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBranchCode.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscBranchName = new AutoCompleteStringCollection();
                acscBranchName.AddRange(this.AutoComplete_BranchNames());
                txtBranchName.AutoCompleteCustomSource = acscBranchName;
                txtBranchName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBranchName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_BankCodes()
        {
            try
            {
                var bankcodesquery = from bk in db.vwBankBranches
                                     select bk.BankCode;
                return bankcodesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_BankNames()
        {
            try
            {
                var banknamesquery = from bk in db.vwBankBranches
                                     select bk.BankName;
                return banknamesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_BranchCodes()
        {
            try
            {
                var branchcodesquery = from bk in db.vwBankBranches
                                       select bk.BranchCode;
                return branchcodesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        private string[] AutoComplete_BranchNames()
        {
            try
            {
                var branchnamesquery = from bk in db.vwBankBranches
                                       select bk.BranchName;
                return branchnamesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        // apply the filter
        private void ApplyFilter()
        {
            try
            {
                // set the filter
                var filter = CreateFilter(_Banks);
                this.bindingSourceBanks.DataSource = filter;
                groupBoxResults.Text = bindingSourceBanks.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }
        private IQueryable<DAL.vwBankBranch> CreateFilter(IQueryable<DAL.vwBankBranch> _banks)
        {
            //none
            if (string.IsNullOrEmpty(txtBankCode.Text)
                && string.IsNullOrEmpty(txtBankName.Text) && string.IsNullOrEmpty(txtBranchCode.Text) && string.IsNullOrEmpty(txtBranchName.Text))
            {
                return _banks;
            }
            //all
            if (!string.IsNullOrEmpty(txtBankCode.Text)
                && !string.IsNullOrEmpty(txtBankName.Text) && !string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankCode = txtBankCode.Text.Trim();
                string _BankName = txtBankName.Text.Trim();
                string _BranchCode = txtBranchCode.Text.Trim();
                string _BranchName = txtBranchName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankCode.StartsWith(_BankCode)
                         where exm.BankName.StartsWith(_BankName)
                         where exm.BranchCode.StartsWith(_BranchCode)
                         where exm.BranchName.StartsWith(_BranchName)
                         select exm;
                return _banks;
            }
            //_BankCode
            if (!string.IsNullOrEmpty(txtBankCode.Text) && string.IsNullOrEmpty(txtBankName.Text) && string.IsNullOrEmpty(txtBranchCode.Text) && string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankCode = txtBankCode.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankCode.StartsWith(_BankCode)
                         select exm;
                return _banks;
            }
            //_BankName
            if (string.IsNullOrEmpty(txtBankCode.Text) && !string.IsNullOrEmpty(txtBankName.Text) && string.IsNullOrEmpty(txtBranchCode.Text) && string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankName = txtBankName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankName.StartsWith(_BankName)
                         select exm;
                return _banks;
            }
            //_BranchCode
            if (string.IsNullOrEmpty(txtBankCode.Text) && string.IsNullOrEmpty(txtBankName.Text) && !string.IsNullOrEmpty(txtBranchCode.Text) && string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BranchCode = txtBranchCode.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BranchCode.StartsWith(_BranchCode)
                         select exm;
                return _banks;
            }
            //_BranchName
            if (string.IsNullOrEmpty(txtBankCode.Text) && string.IsNullOrEmpty(txtBankName.Text) && string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BranchName = txtBranchName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BranchName.StartsWith(_BranchName)
                         select exm;
                return _banks;
            }
            //_BankCode and _BankName
            if (!string.IsNullOrEmpty(txtBankCode.Text) && !string.IsNullOrEmpty(txtBankName.Text) && string.IsNullOrEmpty(txtBranchCode.Text) && string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankCode = txtBankCode.Text.Trim();
                string _BankName = txtBankName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankCode.StartsWith(_BankCode)
                         where exm.BankName.StartsWith(_BankName)
                         select exm;
                return _banks;
            }
            //_BankCode and _BranchCode
            if (!string.IsNullOrEmpty(txtBankCode.Text) && string.IsNullOrEmpty(txtBankName.Text) && !string.IsNullOrEmpty(txtBranchCode.Text) && string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankCode = txtBankCode.Text.Trim();
                string _BranchCode = txtBranchCode.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankCode.StartsWith(_BankCode)
                         where exm.BranchCode.StartsWith(_BranchCode)
                         select exm;
                return _banks;
            }
            //_BankCode and _BranchName
            if (!string.IsNullOrEmpty(txtBankCode.Text) && string.IsNullOrEmpty(txtBankName.Text) && string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankCode = txtBankCode.Text.Trim();
                string _BranchName = txtBranchName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankCode.StartsWith(_BankCode)
                         where exm.BranchName.StartsWith(_BranchName)
                         select exm;
                return _banks;
            }
            //_BankName and _BranchCode
            if (string.IsNullOrEmpty(txtBankCode.Text) && !string.IsNullOrEmpty(txtBankName.Text) && !string.IsNullOrEmpty(txtBranchCode.Text) && string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankName = txtBankName.Text.Trim();
                string _BranchCode = txtBranchCode.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankName.StartsWith(_BankName)
                         where exm.BranchCode.StartsWith(_BranchCode)
                         select exm;
                return _banks;
            }
            //_BankName and _BranchName
            if (string.IsNullOrEmpty(txtBankCode.Text) && !string.IsNullOrEmpty(txtBankName.Text) && string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankName = txtBankName.Text.Trim();
                string _BranchName = txtBranchName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankName.StartsWith(_BankName)
                         where exm.BranchName.StartsWith(_BranchName)
                         select exm;
                return _banks;
            }

            //_BranchCode and _BranchName
            if (string.IsNullOrEmpty(txtBankName.Text) && string.IsNullOrEmpty(txtBankCode.Text) && !string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BranchCode = txtBranchCode.Text.Trim();
                string _BranchName = txtBranchName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BranchCode.StartsWith(_BranchCode)
                         where exm.BranchName.StartsWith(_BranchName)
                         select exm;
                return _banks;
            }
            //_BankCode and _BankName and _BranchCode
            if (!string.IsNullOrEmpty(txtBankCode.Text) && !string.IsNullOrEmpty(txtBankName.Text) && !string.IsNullOrEmpty(txtBranchCode.Text) && string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankCode = txtBankCode.Text.Trim();
                string _BankName = txtBankName.Text.Trim();
                string _BranchCode = txtBranchCode.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankCode.StartsWith(_BankCode)
                         where exm.BankName.StartsWith(_BankName)
                         where exm.BranchCode.StartsWith(_BranchCode)
                         select exm;
                return _banks;
            }
            //_BankCode and _BankName and _BranchName
            if (!string.IsNullOrEmpty(txtBankCode.Text) && !string.IsNullOrEmpty(txtBankName.Text) && string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankCode = txtBankCode.Text.Trim();
                string _BankName = txtBankName.Text.Trim();
                string _BranchName = txtBranchName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankCode.StartsWith(_BankCode)
                         where exm.BankName.StartsWith(_BankName)
                         where exm.BranchName.StartsWith(_BranchName)
                         select exm;
                return _banks;
            }
            //_BankName and _BranchCode and _BranchName
            if (string.IsNullOrEmpty(txtBankCode.Text) && !string.IsNullOrEmpty(txtBankName.Text) && !string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankName = txtBankName.Text.Trim();
                string _BranchCode = txtBranchCode.Text.Trim();
                string _BranchName = txtBranchName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankName.StartsWith(_BankName)
                         where exm.BranchCode.StartsWith(_BranchCode)
                         where exm.BranchName.StartsWith(_BranchName)
                         select exm;
                return _banks;
            }
            //_BankCode and _BranchCode and _BranchName
            if (!string.IsNullOrEmpty(txtBankCode.Text) && string.IsNullOrEmpty(txtBankName.Text) && !string.IsNullOrEmpty(txtBranchCode.Text) && !string.IsNullOrEmpty(txtBranchName.Text))
            {
                _banks = null;
                string _BankCode = txtBankCode.Text.Trim();
                string _BranchCode = txtBranchCode.Text.Trim();
                string _BranchName = txtBranchName.Text.Trim();
                _banks = from exm in db.vwBankBranches
                         where exm.BankCode.StartsWith(_BankCode)
                         where exm.BranchCode.StartsWith(_BranchCode)
                         where exm.BranchName.StartsWith(_BranchName)
                         select exm;
                return _banks;
            }
            return _banks;
        }

        private void txtBankName_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void txtBankName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
        }

        private void txtBranchName_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtBranchName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                ApplyFilter();
                e.Handled = true;
            }
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
                        ApplyFilter(); 
                    } 
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtBranchCode_Validated(object sender, EventArgs e)
        {
            ApplyFilter();

        }
        private void txtBankCode_KeyDown(object sender, KeyEventArgs e)
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
        private void txtBankCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (nonNumberEntered == true)
                {
                    if (e.KeyChar == 13)
                    {
                        ApplyFilter(); 
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtBankCode_Validated(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void dataGridViewBanks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewBanks.SelectedRows.Count > 0)
            {
                try
                {
                    vwBankBranch selectedBankBranch = (vwBankBranch)bindingSourceBanks.Current;
                    OnBankListSelected(this, new BankSelectEventArgs(selectedBankBranch));

                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void dataGridViewBanks_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnSubmit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dataGridViewBanks.SelectedRows.Count > 0)
            {
                try
                {
                    vwBankBranch selectedBankBranch = (vwBankBranch)bindingSourceBanks.Current;
                    OnBankListSelected(this, new BankSelectEventArgs(selectedBankBranch));

                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }


    }
}