using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.DataEntry;
using DAL;
using CommonLib;

namespace winSBPayroll.Forms
{
    public partial class EditEmployer : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.Employer _employer;
        List<EmployerBanksModel> _employerbanks = new List<EmployerBanksModel>();
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public EditEmployer(DAL.Employer employer, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _employer = employer;
        }

        private void InitializeControls()
        {
            if (_employer.Name != null)
            {
                txtName.Text = _employer.Name.Trim();
            }
            if (_employer.Address1 != null)
            {
                txtAddress1.Text = _employer.Address1.Trim();
            }
            if (_employer.Address2 != null)
            {
                txtAddress2.Text = _employer.Address2.Trim();
            }
            if (_employer.Telephone != null)
            {
                txtTelephone.Text = _employer.Telephone.Trim();
            }
            if (_employer.PIN != null)
            {
                txtPIN.Text = _employer.PIN.Trim();
            }
            if (_employer.Email != null)
            {
                txtEmail.Text = _employer.Email.Trim();
            }
            if (_employer.NHIF != null)
            {
                txtNHIF.Text = _employer.NHIF.Trim();
            }
            if (_employer.NSSF != null)
            {
                txtNSSF.Text = _employer.NSSF.Trim();
            }
            if (_employer.Logo != null)
            {
                txtLogo.Text = _employer.Logo.Trim();
            }
            if (_employer.Slogan != null)
            {
                txtSlogan.Text = _employer.Slogan.Trim();
            }
            if (_employer.BankBranchSortCode != null)
            {
                //txtBankSortCode.Text = _employer.BankBranchSortCode.Trim();
            }
            if (_employer.AccountName != null)
            {
                txtAccountName.Text = _employer.AccountName.Trim();
            }
            if (_employer.AccountNo != null)
            {
                txtAccountNo.Text = _employer.AccountNo.Trim();
            }
            if (_employer.AuthorizedSignatory != null)
            {
                txtAuthSign.Text = _employer.AuthorizedSignatory.Trim();
            }
            if (_employer.IsActive != null)
            {
                chkIsActive.Checked = _employer.IsActive.Value;
            }
        }
        private void EditEmployer_Load(object sender, EventArgs e)
        {
            try
            {
                txtLogo.Enabled = false;

                var _banksquery = from rl in rep.GetBankBranches()
                                  select rl;
                List<vwBankBranch> _banks = _banksquery.ToList();
                DataGridViewComboBoxColumn colCboxBankBranch = new DataGridViewComboBoxColumn();
                colCboxBankBranch.HeaderText = "Bank";
                colCboxBankBranch.Name = "cbBankBranch";
                colCboxBankBranch.DataSource = _banks;
                // The display member is the name column in the column datasource  
                colCboxBankBranch.DisplayMember = "BankBranchName";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxBankBranch.DataPropertyName = "BankSortCode";
                // The value member is the primary key of the parent table  
                colCboxBankBranch.ValueMember = "BankSortCode";
                colCboxBankBranch.MaxDropDownItems = 10;
                colCboxBankBranch.Width = 100;
                colCboxBankBranch.DisplayIndex = 1;
                colCboxBankBranch.MinimumWidth = 5;
                colCboxBankBranch.FlatStyle = FlatStyle.Flat;
                colCboxBankBranch.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxBankBranch.ReadOnly = true;
                //colCboxBankBranch.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridViewEmployerBank.Columns.Contains("cbBankBranch"))
                {
                    dataGridViewEmployerBank.Columns.Add(colCboxBankBranch);
                }

                InitializeControls();

                RefreshGrid();

                AutoCompleteStringCollection acscbsc = new AutoCompleteStringCollection();
                acscbsc.AddRange(this.AutoComplete_BankSortCodes());
                txtBankSortCode.AutoCompleteCustomSource = acscbsc;
                txtBankSortCode.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBankSortCode.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscanm = new AutoCompleteStringCollection();
                acscanm.AddRange(this.AutoComplete_AccountName());
                txtAccountName.AutoCompleteCustomSource = acscanm;
                txtAccountName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtAccountName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscan = new AutoCompleteStringCollection();
                acscan.AddRange(this.AutoComplete_AccountNo());
                txtAccountNo.AutoCompleteCustomSource = acscan;
                txtAccountNo.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtAccountNo.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscbn = new AutoCompleteStringCollection();
                acscbn.AddRange(this.AutoComplete_AuthSign());
                txtAuthSign.AutoCompleteCustomSource = acscbn;
                txtAuthSign.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtAuthSign.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
            finally
            {
                CheckForDefaultBank();
            }
        }
        private string[] AutoComplete_BankSortCodes()
        {
            try
            {
                var bankcodesquery = from bk in db.vwBankBranches
                                     select bk.BankSortCode;
                return bankcodesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_AccountName()
        {
            try
            {
                var bankcodesquery = from bk in db.EmployerBanks
                                     select bk.AccountName;
                return bankcodesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_AccountNo()
        {
            try
            {
                var bankcodesquery = from bk in db.EmployerBanks
                                     select bk.AccountNo;
                return bankcodesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_AuthSign()
        {
            try
            {
                var bankcodesquery = from bk in db.EmployerBanks
                                     select bk.Signatory;
                return bankcodesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (IsEmployerValid())
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtName.Text))
                    {
                        _employer.Name = Utils.ConvertFirstLetterToUpper(txtName.Text.ToString().Trim());
                    }
                    if (!string.IsNullOrEmpty(txtAddress1.Text))
                    {
                        _employer.Address1 = Utils.ConvertFirstLetterToUpper(txtAddress1.Text.ToString().Trim());
                    }
                    if (!string.IsNullOrEmpty(txtAddress2.Text))
                    {
                        _employer.Address2 = Utils.ConvertFirstLetterToUpper(txtAddress2.Text.ToString().Trim());
                    }
                    if (!string.IsNullOrEmpty(txtTelephone.Text))
                    {
                        _employer.Telephone = Utils.ConvertFirstLetterToUpper(txtTelephone.Text.ToString().Trim());
                    }
                    if (!string.IsNullOrEmpty(txtPIN.Text))
                    {
                        _employer.PIN = txtPIN.Text.ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(txtEmail.Text))
                    {
                        _employer.Email = txtEmail.Text.ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(txtNHIF.Text))
                    {
                        _employer.NHIF = txtNHIF.Text.ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(txtNSSF.Text))
                    {
                        _employer.NSSF = txtNSSF.Text.ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(txtLogo.Text))
                    {
                        _employer.Logo = txtLogo.Text.ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(txtSlogan.Text))
                    {
                        _employer.Slogan = Utils.ConvertFirstLetterToUpper(txtSlogan.Text.ToString().Trim());
                    }
                    int banksortcode;
                    if (!string.IsNullOrEmpty(txtBankSortCode.Text) && int.TryParse(txtBankSortCode.Text, out banksortcode))
                    {
                        _employer.BankBranchSortCode = txtBankSortCode.Text;
                    }
                    if (!string.IsNullOrEmpty(txtAccountName.Text))
                    {
                        _employer.AccountName = Utils.ConvertFirstLetterToUpper(txtAccountName.Text.ToString().Trim());
                    }
                    if (!string.IsNullOrEmpty(txtAccountNo.Text))
                    {
                        _employer.AccountNo = txtAccountNo.Text.ToString().Trim();
                    }
                    if (!string.IsNullOrEmpty(txtAuthSign.Text))
                    {
                        _employer.AuthorizedSignatory = Utils.ConvertFirstLetterToUpper(txtAuthSign.Text.ToString().Trim());
                    }
                    _employer.IsActive = chkIsActive.Checked;

                    rep.UpdateEmployer(_employer);

                    SaveEmployerBank(_employerbanks);

                    Employer f = (Employer)this.Owner;
                    f.RefreshGrid();
                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private bool IsEmployerValid()
        {
            bool no_error = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtName, "Employer name cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAddress1.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAddress1, "Address cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtTelephone.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtTelephone, "Telephone cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtPIN.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPIN, "PIN cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmail, "Email cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtNHIF.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtNHIF, "NHIF cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtNSSF.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtNSSF, "NSSF cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtLogo.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtLogo, "Logo cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtSlogan.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtSlogan, "Slogan cannot be null!");
                return false;
            }
            if (dataGridViewEmployerBank.SelectedRows.Count == 0)
            {
                MessageBox.Show("Must have atleast one Bank!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return no_error;
        }
        private bool IsEmployerBankValid()
        {
            bool no_error = true;
            if (string.IsNullOrEmpty(txtBankSortCode.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBankSortCode, "Bank Sort Code cannot be null!");
                return false;
            }
            int banksortcode;
            if (!int.TryParse(txtBankSortCode.Text, out banksortcode))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBankSortCode, "Bank Sort Code must be integer!");
                return false;
            }
            string _banksortcode = txtBankSortCode.Text.Trim();
            var _branchquery = (from bb in rep.GetAllBranches()
                                where bb.BankSortCode == _banksortcode
                                select bb).FirstOrDefault();
            BankBranch _branch = _branchquery;
            if (_branch == null)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBankSortCode, "Bank Sort Code does not exist!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAccountName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAccountName, "Account Name cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAccountNo, "Account No cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAuthSign.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAuthSign, "Authorizing Signatory cannot be null!");
                return false;
            }
            return no_error;
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                // Create OpenFileDialog 
                Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
                ofd.Title = "Select an Image File";
                ofd.Filter = "Image File (*.jpg)|*.jpg|Image File (*.gif)|*.gif|Image File (*.png)|*.png|All files (*.*)|*.*";
                Nullable<bool> result = ofd.ShowDialog();
                // Process open file dialog box results
                if (result == true)
                {
                    txtLogo.Text = ofd.FileName.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnSearchBank_Click(object sender, EventArgs e)
        {
            try
            {
                SearchBanksSimpleForm saf = new Forms.SearchBanksSimpleForm(connection) { Owner = this };
                saf.OnBankListSelected += new SearchBanksSimpleForm.BankSelectHandler(saf_OnBankListSelected);
                saf.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void saf_OnBankListSelected(object sender, BankSelectEventArgs e)
        {
            try
            {
                SetBankSortCode(e._BankSortCode);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void SetBankSortCode(vwBankBranch _vwBankBranch)
        {
            try
            {
                lblBankDetails.Text = string.Empty;
                if (_vwBankBranch != null)
                {
                    txtBankSortCode.Text = _vwBankBranch.BankSortCode.Trim();
                    lblBankDetails.Text = _vwBankBranch.BankBranchName.Trim();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtBankSortCode_Validated(object sender, EventArgs e)
        {
            try
            {
                lblBankDetails.Text = string.Empty;
                if (nonNumberEntered == true)
                {
                    string _banksortcode = txtBankSortCode.Text.Trim();
                    var bankbranchnamequery = (from vw in db.vwBankBranches
                                               where vw.BankSortCode == _banksortcode
                                               select vw).FirstOrDefault();
                    if (bankbranchnamequery != null)
                    {
                        lblBankDetails.Text = bankbranchnamequery.BankBranchName.Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtBankSortCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                lblBankDetails.Text = string.Empty;
                if (nonNumberEntered == true)
                {
                    if (e.KeyChar == 13)
                    {
                        string _banksortcode = txtBankSortCode.Text.Trim();
                        var bankbranchnamequery = (from vw in db.vwBankBranches
                                                   where vw.BankSortCode == _banksortcode
                                                   select vw).FirstOrDefault();
                        if (bankbranchnamequery != null)
                        {
                            lblBankDetails.Text = bankbranchnamequery.BankBranchName.Trim();
                        }
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtBankSortCode_KeyDown(object sender, KeyEventArgs e)
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
        private void txtBankSortCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblBankDetails.Text = string.Empty;
                string _banksortcode = txtBankSortCode.Text.Trim();
                var bankbranchnamequery = (from vw in db.vwBankBranches
                                           where vw.BankSortCode == _banksortcode
                                           select vw).FirstOrDefault();
                if (bankbranchnamequery != null)
                {
                    lblBankDetails.Text = bankbranchnamequery.BankBranchName.Trim();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsEmployerBankValid())
            {
                try
                {
                    EmployerBanksModel _bank = new EmployerBanksModel();
                    _bank.EmployerId = _employer.Id;
                    _bank.BankSortCode = Utils.ConvertFirstLetterToUpper(txtBankSortCode.Text.Trim());
                    _bank.AccountName = Utils.ConvertFirstLetterToUpper(txtAccountName.Text.Trim());
                    _bank.AccountNo = Utils.ConvertFirstLetterToUpper(txtAccountNo.Text.Trim());
                    _bank.Signatory = Utils.ConvertFirstLetterToUpper(txtAuthSign.Text.Trim());
                    _bank.IsDefault = chkIsDefault.Checked;

                    if (_employerbanks.Any(i => i.EmployerId == _bank.EmployerId && i.BankSortCode == _bank.BankSortCode))
                    {
                        MessageBox.Show("Bank Exist!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!_employerbanks.Any(i => i.EmployerId == _bank.EmployerId && i.BankSortCode == _bank.BankSortCode))
                    {
                        _employerbanks.Add(_bank);
                        RefreshGrid();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
                finally
                {
                    CheckForDefaultBank();
                }
            }
        }
        private void SaveEmployerBank(List<EmployerBanksModel> _banks)
        {
            try
            {
                foreach (var _bank in _banks)
                {
                    if (!rep.GetAllEmployerBanks(_employer.Id).Any(i => i.EmployerId == _bank.EmployerId && i.BankSortCode == _bank.BankSortCode))
                    {
                        rep.AddEmployerBank(_bank);
                        RefreshGrid();
                    }
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
                if (dataGridViewEmployerBank.SelectedRows.Count != 0)
                {
                    EmployerBanksModel _bank = (EmployerBanksModel)bindingSourceEmployerBank.Current;

                    EmployerBanksModel _empbank = rep.GetEmployerBank(_bank.Id);

                    if (_empbank != null)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Employer Bank", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            rep.DeleteEmployerBank(_empbank);
                            RefreshGrid();
                        }
                    }
                    if (_empbank == null)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Employer Bank", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            _employerbanks.Remove(_bank);
                            RefreshGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
            finally
            {
                CheckForDefaultBank();
            }
        }
        private void RefreshGrid()
        {
            try
            {
                bindingSourceEmployerBank.DataSource = null;
                var _employerbanksquery = from eb in rep.GetAllEmployerBanks(_employer.Id)
                                          select eb;
                List<EmployerBanksModel> _banks = _employerbanksquery.ToList();

                foreach (var _bank in _banks)
                {
                    if (!_employerbanks.Any(i => i.BankSortCode == _bank.BankSortCode && i.EmployerId == _bank.EmployerId))
                    {
                        _employerbanks.Add(_bank);
                    }
                }
                int rowid = 1;
                foreach (var row in _employerbanks)
                {
                    row.EmployerBankId = rowid;
                    rowid++;
                }

                bindingSourceEmployerBank.DataSource = _employerbanks;
                dataGridViewEmployerBank.AutoGenerateColumns = false;
                this.dataGridViewEmployerBank.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewEmployerBank.DataSource = bindingSourceEmployerBank;
                groupBox3.Text = bindingSourceEmployerBank.Count.ToString();
                foreach (DataGridViewRow row in dataGridViewEmployerBank.Rows)
                {
                    dataGridViewEmployerBank.Rows[dataGridViewEmployerBank.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridViewEmployerBank.Rows.Count - 1;
                    bindingSourceEmployerBank.Position = nRowIndex;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewEmployerBank_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtBankSortCode_Leave(object sender, EventArgs e)
        {
            try
            {
                lblBankDetails.Text = string.Empty;
                string _banksortcode = txtBankSortCode.Text.Trim();
                var bankbranchnamequery = (from vw in db.vwBankBranches
                                           where vw.BankSortCode == _banksortcode
                                           select vw).FirstOrDefault();
                if (bankbranchnamequery != null)
                {
                    lblBankDetails.Text = bankbranchnamequery.BankBranchName.Trim();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtBankSortCode_TabIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblBankDetails.Text = string.Empty;
                string _banksortcode = txtBankSortCode.Text.Trim();
                var bankbranchnamequery = (from vw in db.vwBankBranches
                                           where vw.BankSortCode == _banksortcode
                                           select vw).FirstOrDefault();
                if (bankbranchnamequery != null)
                {
                    lblBankDetails.Text = bankbranchnamequery.BankBranchName.Trim();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void groupBox2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (dataGridViewEmployerBank.SelectedRows.Count > 0)
                {
                    if (!ValidFeesStructure())
                    {
                        MessageBox.Show("No default Bank is set!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private bool ValidFeesStructure()
        {
            bool valid = false;
            var defaultbank = (from fs in _employerbanks
                               where fs.IsDefault == true
                               select fs).FirstOrDefault();

            if (defaultbank != null)
            {
                valid = true;
            }
            if (defaultbank == null)
            {
                valid = false;
            }
            return valid;
        }
        public void CheckForDefaultBank()
        {
            try
            {
                var defaultbank = (from eb in _employerbanks
                                   where eb.IsDefault == true
                                   select eb).FirstOrDefault();

                if (defaultbank != null)
                {
                    chkIsDefault.Enabled = false;
                    chkIsDefault.Checked = false;
                }
                if (defaultbank == null)
                {
                    chkIsDefault.Enabled = true;
                    chkIsDefault.Checked = false;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        } 























    }
}