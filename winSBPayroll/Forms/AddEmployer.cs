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
    public partial class AddEmployer : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        List<EmployerBanksModel> _employer_banks = new List<EmployerBanksModel>();
        List<Employer_Authorized_Signatories_Model> _employer_signatories = new List<Employer_Authorized_Signatories_Model>();
        List<Employer_Centers_Model> _employer_centers = new List<Employer_Centers_Model>();
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        public string TAG;
        //Event declaration:
        //event for publishing messages to output
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public AddEmployer(string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            TAG = this.GetType().Name;

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished Employees initialization", TAG));

        }

        private void AddEmployer_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView_banks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_banks.MultiSelect = false;
                dataGridView_banks.ReadOnly = true;
                dataGridView_banks.AutoGenerateColumns = false;

                dataGridView_signatories.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_signatories.MultiSelect = false;
                dataGridView_signatories.ReadOnly = true;
                dataGridView_signatories.AutoGenerateColumns = false;

                dataGridView_centers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_centers.MultiSelect = false;
                dataGridView_centers.ReadOnly = true;
                dataGridView_centers.AutoGenerateColumns = false;

                txtLogo.Enabled = false;

                _employer_banks = new List<EmployerBanksModel>();
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
                colCboxBankBranch.Width = 150;
                colCboxBankBranch.DisplayIndex = 1;
                colCboxBankBranch.MinimumWidth = 5;
                colCboxBankBranch.FlatStyle = FlatStyle.Flat;
                colCboxBankBranch.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxBankBranch.ReadOnly = true;
                //colCboxBankBranch.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridView_banks.Columns.Contains("cbBankBranch"))
                {
                    dataGridView_banks.Columns.Add(colCboxBankBranch);
                }
                  
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

                AutoCompleteStringCollection acsc_authorized_signatories = new AutoCompleteStringCollection();
                acsc_authorized_signatories.AddRange(this.AutoComplete_Authorized_Signatories());
                txt_authorized_signatory.AutoCompleteCustomSource = acsc_authorized_signatories;
                txt_authorized_signatory.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txt_authorized_signatory.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acsc_designation = new AutoCompleteStringCollection();
                acsc_designation.AddRange(this.AutoComplete_Desination());
                txt_designation.AutoCompleteCustomSource = acsc_designation;
                txt_designation.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txt_designation.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acsc_center_description = new AutoCompleteStringCollection();
                acsc_center_description.AddRange(this.AutoComplete_Center_Description());
                txt_center_description.AutoCompleteCustomSource = acsc_center_description;
                txt_center_description.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txt_center_description.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                this.CheckForDefaultBank();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished AddEmployer load", TAG));

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
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
                var account_names_query = from bk in db.EmployerBanks
                                          select bk.AccountName;
                return account_names_query.ToArray();
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
                var account_no_query = from bk in db.EmployerBanks
                                       select bk.AccountNo;
                return account_no_query.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_Authorized_Signatories()
        {
            try
            {
                var names_query = from bk in db.Employer_Authorized_Signatories
                                  select bk.Name;
                return names_query.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        private string[] AutoComplete_Desination()
        {
            try
            {
                var designation_query = from bk in db.Employer_Authorized_Signatories
                                        select bk.Designation;
                return designation_query.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        private string[] AutoComplete_Center_Description()
        {
            try
            {
                var description_query = from bk in db.Employer_Centers
                                        select bk.Description;
                return description_query.ToArray();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Is_Employer_Valid())
            {
                try
                {
                    DAL.Employer _employer = new DAL.Employer();
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
                        _employer.Telephone = txtTelephone.Text.ToString().Trim();
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
                    if (!string.IsNullOrEmpty(txtcode.Text))
                    {
                        _employer.Code = Utils.ConvertFirstLetterToUpper(txtcode.Text.ToString().Trim().ToUpper());
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
                        _employer.AccountNo = Utils.ConvertFirstLetterToUpper(txtAccountNo.Text.ToString().Trim());
                    }
                    _employer.IsActive = true;
                    _employer.IsDeleted = false;

                    if (db.Employers.Any(c => c.Name == _employer.Name || c.Code == _employer.Code))
                    {
                        MessageBox.Show("Employer with name [ " + _employer.Name + " ] or Employer with code [ " + _employer.Code + " ] Exists !", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.Employers.Any(c => c.Name == _employer.Name || c.Code == _employer.Code))
                    {
                        int employerid = rep.AddEmployer(_employer);

                        Add_Employer_Banks(employerid);
                        Add_Employer_Signatories(employerid);
                        Add_Employer_Centers(employerid);

                        EmployerForm f = (EmployerForm)this.Owner;
                        f.RefreshGrid(1);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.ShowError(ex);
                }
            }
            else
            {
                MessageBox.Show("There are errors in the Form. \nCheck all tabs for missing data.", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void Add_Employer_Banks(int employerid)
        {
            foreach (var _bank in _employer_banks)
            {
                _bank.EmployerId = employerid;
                rep.AddEmployerBank(_bank);
            }
        }

        void Add_Employer_Signatories(int employerid)
        {
            foreach (var _signatory in _employer_signatories)
            {
                _signatory.EmployerId = employerid;
                if (!rep.Get_All_Employer_Signatories(employerid).Any(c => c.Name == _signatory.Name))
                {
                    rep.Add_Employer_Signatory(_signatory);
                }
            }
        }

        void Add_Employer_Centers(int employerid)
        {
            foreach (var _center in _employer_centers)
            {
                _center.EmployerId = employerid;
                if (!rep.Get_All_Employer_Centers(employerid).Any(c => c.Description == _center.Description))
                {
                    rep.Add_Employer_Center(_center);
                }
            }
        }

        private bool Is_Employer_Valid()
        {
            bool no_error = true;
            if (string.IsNullOrEmpty(txtName.Text))
            {
                errorProvider.SetError(txtName, "Employer name cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtAddress1.Text))
            {
                errorProvider.SetError(txtAddress1, "Address cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtTelephone.Text))
            {
                errorProvider.SetError(txtTelephone, "Telephone cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtPIN.Text))
            {
                errorProvider.SetError(txtPIN, "PIN cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Email cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtNHIF.Text))
            {
                errorProvider.SetError(txtNHIF, "SHIF cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtNSSF.Text))
            {
                errorProvider.SetError(txtNSSF, "NSSF cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtLogo.Text))
            {
                errorProvider.SetError(txtLogo, "Logo cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtSlogan.Text))
            {
                errorProvider.SetError(txtSlogan, "Slogan cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtcode.Text))
            {
                errorProvider.SetError(txtcode, "Code cannot be null!");
                no_error = false;
            }
            if (dataGridView_banks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Must have atleast one Bank!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabPages.IndexOf(tabPageBankingDetails)];
                no_error = false;
            }
            return no_error;
        }

        private bool Is_Employer_Bank_Valid()
        {
            bool no_error = true;
            if (string.IsNullOrEmpty(txtBankSortCode.Text))
            {
                errorProvider.SetError(txtBankSortCode, "Bank Sort Code cannot be null!");
                no_error = false;
            }
            int banksortcode;
            if (!int.TryParse(txtBankSortCode.Text, out banksortcode))
            {
                errorProvider.SetError(txtBankSortCode, "Bank Sort Code must be integer!");
                no_error = false;
            }
            string _banksortcode = txtBankSortCode.Text.Trim();
            var _branchquery = (from bb in rep.GetAllBranches()
                                where bb.BankSortCode == _banksortcode
                                select bb).FirstOrDefault();
            BankBranch _branch = _branchquery;
            if (_branch == null)
            {
                errorProvider.SetError(txtBankSortCode, "Bank Sort Code does not exist!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtAccountName.Text))
            {
                errorProvider.SetError(txtAccountName, "Account Name cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtAccountNo.Text))
            {
                errorProvider.SetError(txtAccountNo, "Account No cannot be null!");
                no_error = false;
            }
            return no_error;
        }

        private bool Is_Employer_Signatory_Valid()
        {
            bool no_error = true;
            if (string.IsNullOrEmpty(txt_authorized_signatory.Text))
            {
                errorProvider.SetError(txt_authorized_signatory, "Authorized Signatory cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txt_designation.Text))
            {
                errorProvider.SetError(txt_designation, "Designation cannot be null!");
                no_error = false;
            }
            return no_error;
        }

        private bool Is_Employer_Center_Valid()
        {
            bool no_error = true;
            if (string.IsNullOrEmpty(txt_center_description.Text))
            {
                errorProvider.SetError(txt_center_description, "Description cannot be null!");
                no_error = false;
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
                    string strFileName = ofd.FileName;
                    string strFileType = System.IO.Path.GetExtension(strFileName).ToString().ToLower();

                    //Check file type
                    if (strFileType != ".jpg" && strFileType != ".gif" && strFileType != ".png")
                    {
                        throw new Exception("File Type not Image");

                    }
                    //display  image
                    if (strFileType.Trim() == ".jpg")
                    {
                        pbPhoto.ImageLocation = strFileName;
                    }
                    else if (strFileType.Trim() == ".gif")
                    {
                        pbPhoto.ImageLocation = strFileName;
                    }
                    else if (strFileType.Trim() == ".png")
                    {
                        pbPhoto.ImageLocation = strFileName;
                    }

                    txtLogo.Text = ofd.FileName.ToString().Trim();
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
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
        private void btnAddBank_Click(object sender, EventArgs e)
        {
            if (Is_Employer_Bank_Valid())
            {
                try
                {
                    EmployerBanksModel _bank = new EmployerBanksModel();
                    _bank.BankSortCode = Utils.ConvertFirstLetterToUpper(txtBankSortCode.Text.Trim());
                    _bank.AccountName = Utils.ConvertFirstLetterToUpper(txtAccountName.Text.Trim());
                    _bank.AccountNo = Utils.ConvertFirstLetterToUpper(txtAccountNo.Text.Trim());
                    _bank.IsDefault = chkIsDefault.Checked;

                    if (_employer_banks.Any(i => i.BankSortCode == _bank.BankSortCode))
                    {
                        MessageBox.Show("Bank Exist!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!_employer_banks.Any(i => i.BankSortCode == _bank.BankSortCode))
                    {
                        _employer_banks.Add(_bank);
                        Refresh_Banks_Grid();
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_banks.SelectedRows.Count != 0)
                {
                    EmployerBanksModel _bank = (EmployerBanksModel)bindingSource_banks.Current;

                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Employer Bank", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        _employer_banks.Remove(_bank);
                        Refresh_Banks_Grid();
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

        private void Refresh_Banks_Grid()
        {
            try
            {
                bindingSource_banks.DataSource = null;
                int rowid = 1;
                foreach (var row in _employer_banks)
                {
                    row.Id = rowid;
                    rowid++;
                }
                bindingSource_banks.DataSource = _employer_banks;
                groupBox3.Text = bindingSource_banks.Count.ToString();
                dataGridView_banks.DataSource = bindingSource_banks;
                foreach (DataGridViewRow row in dataGridView_banks.Rows)
                {
                    dataGridView_banks.Rows[dataGridView_banks.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridView_banks.Rows.Count - 1;
                    bindingSource_banks.Position = nRowIndex;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void Refresh_Signatories_Grid()
        {
            try
            {
                bindingSource_signatories.DataSource = null;
                int rowid = 1;
                foreach (var row in _employer_signatories)
                {
                    row.Id = rowid;
                    rowid++;
                }
                bindingSource_signatories.DataSource = _employer_signatories;
                groupBox7.Text = bindingSource_signatories.Count.ToString();
                dataGridView_signatories.DataSource = bindingSource_signatories;
                foreach (DataGridViewRow row in dataGridView_signatories.Rows)
                {
                    dataGridView_signatories.Rows[dataGridView_signatories.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridView_signatories.Rows.Count - 1;
                    bindingSource_signatories.Position = nRowIndex;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void Refresh_Centers_Grid()
        {
            try
            {
                bindingSource_centers.DataSource = null;
                int rowid = 1;
                foreach (var row in _employer_centers)
                {
                    row.Id = rowid;
                    rowid++;
                }
                bindingSource_centers.DataSource = _employer_centers;
                groupBox8.Text = bindingSource_centers.Count.ToString();
                dataGridView_centers.DataSource = bindingSource_centers;
                foreach (DataGridViewRow row in dataGridView_centers.Rows)
                {
                    dataGridView_centers.Rows[dataGridView_centers.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridView_centers.Rows.Count - 1;
                    bindingSource_centers.Position = nRowIndex;
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
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteToErrorLogFile(ex);
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
                if (dataGridView_banks.SelectedRows.Count > 0)
                {
                    if (!Valid_Banks())
                    {
                        MessageBox.Show("No default Bank is set!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private bool Valid_Banks()
        {
            bool valid = false;
            var defaultbank = (from eb in _employer_banks
                               where eb.IsDefault == true
                               select eb).FirstOrDefault();

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
                var defaultbank = (from eb in _employer_banks
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
                    chkIsDefault.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtcode_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_add_authorized_sinatory_Click(object sender, EventArgs e)
        {
            if (Is_Employer_Signatory_Valid())
            {
                try
                {
                    Employer_Authorized_Signatories_Model _auth_sign = new Employer_Authorized_Signatories_Model();
                    _auth_sign.Name = Utils.ConvertFirstLetterToUpper(txt_authorized_signatory.Text.Trim());
                    _auth_sign.Designation = Utils.ConvertFirstLetterToUpper(txt_designation.Text.Trim());

                    if (_employer_signatories.Any(i => i.Name == _auth_sign.Name))
                    {
                        MessageBox.Show("Name Exist!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!_employer_signatories.Any(i => i.Name == _auth_sign.Name))
                    {
                        _employer_signatories.Add(_auth_sign);

                        Refresh_Signatories_Grid();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void btn_delete_authorized_sinatory_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_signatories.SelectedRows.Count != 0)
                {
                    Employer_Authorized_Signatories_Model _auth_sign = (Employer_Authorized_Signatories_Model)bindingSource_signatories.Current;

                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Employer Authorized Signatory", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        _employer_signatories.Remove(_auth_sign);

                        Refresh_Signatories_Grid();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btn_add_center_Click(object sender, EventArgs e)
        {
            if (Is_Employer_Center_Valid())
            {
                try
                {
                    Employer_Centers_Model _center = new Employer_Centers_Model();
                    _center.Description = Utils.ConvertFirstLetterToUpper(txt_center_description.Text.Trim());

                    if (_employer_centers.Any(i => i.Description == _center.Description))
                    {
                        MessageBox.Show("Center Exist!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!_employer_centers.Any(i => i.Description == _center.Description))
                    {
                        _employer_centers.Add(_center);

                        Refresh_Centers_Grid();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void btn_delete_center_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView_centers.SelectedRows.Count != 0)
                {
                    Employer_Centers_Model _center = (Employer_Centers_Model)bindingSource_centers.Current;

                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Employer Center", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        _employer_centers.Remove(_center);

                        Refresh_Centers_Grid();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridView_signatories_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteToErrorLogFile(ex);
            }
        }

        private void dataGridView_centers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteToErrorLogFile(ex);
            }
        }




































    }
}