using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class EditEmployee : Form
    {
        DAL.Employee employee;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.Employee_Ext employee_ext;
        List<DAL.EmployeeTransaction> empTxn;
        string _User;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public EditEmployee(DAL.Employee emp, string user, string Conn)
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
            empTxn = de.EmpTxnList(employee.EmpNo);
        }

        private void InitializeControls()
        {
            try
            {
                if (employee.EmpNo != null)
                {
                    txtEmpNo.Text = employee.EmpNo.Trim();
                }
                if (employee.Email != null)
                {
                    txtEmail.Text = employee.Email.Trim();
                }
                if (employee.Surname != null)
                {
                    txtSurname.Text = employee.Surname.Trim();
                }
                if (employee.OtherNames != null)
                {
                    txtOtherNames.Text = employee.OtherNames.Trim();
                }
                dtpDOB.Value = employee.DoB ?? DateTime.Today;
                if (employee.MaritalStatus != null)
                {
                    cbMaritalStatus.SelectedValue = employee.MaritalStatus.Trim();
                }
                if (employee.Gender != null)
                {
                    cbGender.SelectedValue = employee.Gender.Trim();
                }
                chkIsActive.Checked = employee.IsActive ?? true;
                if (employee.Photo != null)
                {
                    pbPhoto.ImageLocation = employee.Photo.ToString().Trim();
                }
                dtpDOE.Value = employee.DoE ?? DateTime.Today;
                if (employee.BasicComputation != null)
                {
                    cbBasicComputation.SelectedValue = employee.BasicComputation.Trim();
                }
                if (employee.BasicPay != null)
                {
                    txtBasicPay.Text = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", employee.BasicPay);
                }
                if (employee.PersonalRelief != null)
                {
                    txtPersonalRelief.Text = employee.PersonalRelief.ToString();
                }
                if (employee.MortgageRelief != null)
                {
                    txtMortgageRelief.Text = employee.MortgageRelief.ToString();
                }
                if (employee.InsuranceRelief != null)
                {
                    txtInsuranceRelief.Text = employee.InsuranceRelief.ToString();
                }
                if (employee.NSSFNo != null)
                {
                    txtNSSF.Text = employee.NSSFNo.Trim();
                }
                if (employee.NHIFNo != null)
                {
                    txtNHIF.Text = employee.NHIFNo.Trim();
                }
                if (employee.IDNo != null)
                {
                    txtIDNo.Text = employee.IDNo.Trim();
                }
                if (employee.PINNo != null)
                {
                    txtPIN.Text = employee.PINNo.Trim();
                }
                if (employee.DepartmentId != null)
                {
                    cbDepartment.SelectedValue = employee.DepartmentId;
                }
                if (employee.EmployerId != null)
                {
                    cbEmployer.SelectedValue = employee.EmployerId;
                }
                if (employee.PayPoint != null)
                {
                    txtEmployeePaymentPoint.Text = employee.PayPoint.Trim();
                }
                if (employee.EmpGroup != null)
                {
                    cboEmployeeGroup.SelectedValue = employee.EmpGroup.Trim();
                }
                if (employee.EmpPayroll != null)
                {
                    cboEmpPayroll.SelectedValue = employee.EmpPayroll.Trim();
                }
                if (employee.PrevEmployer != null)
                {
                    txtPrevEmployer.Text = employee.PrevEmployer.Trim();
                }
                dtpDateLeft.Value = employee.DateLeft ?? DateTime.Today;
                if (employee.PaymentMode != null)
                {
                    cboModeofPayment.SelectedValue = employee.PaymentMode.Trim();
                }
                switch (employee.PaymentMode)
                {
                    case "M":
                        txtTelephoneNo.Visible = true;
                        if (employee.TelephoneNo != null)
                        {
                            txtTelephoneNo.Text = employee.TelephoneNo;
                            lblpaymentmode.Text = "Enter phone Number*";
                        }
                        break;
                    case "B":
                        txtBankSortCode.Visible = true;
                        if (employee.BankCode != null)
                        {
                            txtBankSortCode.Text = employee.BankCode.Trim();
                        }
                        txtAccount.Visible = true;
                        if (employee.BankAccount != null)
                        {
                            txtAccount.Text = employee.BankAccount;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (IsEmployeeValid())
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtEmpNo.Text))
                    {
                        employee.EmpNo = txtEmpNo.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtEmail.Text))
                    {
                        employee.Email = txtEmail.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtSurname.Text))
                    {
                        employee.Surname = Utils.ConvertFirstLetterToUpper(txtSurname.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtOtherNames.Text))
                    {
                        employee.OtherNames = Utils.ConvertFirstLetterToUpper(txtOtherNames.Text.Trim());
                    }
                    employee.DoB = dtpDOB.Value;
                    if (cbMaritalStatus.SelectedIndex != -1)
                    {
                        employee.MaritalStatus = cbMaritalStatus.SelectedValue.ToString();
                    }
                    if (cbGender.SelectedIndex != -1)
                    {
                        employee.Gender = cbGender.SelectedValue.ToString();
                    }
                    if (!string.IsNullOrEmpty(pbPhoto.ImageLocation))
                    {
                        employee.Photo = pbPhoto.ImageLocation.ToString().Trim();
                    }
                    employee.DoE = dtpDOE.Value;
                    if (cbBasicComputation.SelectedIndex != -1)
                    {
                        employee.BasicComputation = cbBasicComputation.SelectedValue.ToString();
                    }
                    decimal basicpay;
                    if (!string.IsNullOrEmpty(txtBasicPay.Text) && decimal.TryParse(txtBasicPay.Text, out basicpay))
                    {
                        employee.BasicPay = decimal.Parse(txtBasicPay.Text.Trim());
                    }
                    decimal personalrelief;
                    if (!string.IsNullOrEmpty(txtPersonalRelief.Text) && decimal.TryParse(txtPersonalRelief.Text, out personalrelief))
                    {
                        employee.PersonalRelief = decimal.Parse(txtPersonalRelief.Text.Trim());
                    }
                    decimal morgagerelief;
                    if (!string.IsNullOrEmpty(txtMortgageRelief.Text) && decimal.TryParse(txtMortgageRelief.Text, out morgagerelief))
                    {
                        employee.MortgageRelief = decimal.Parse(txtMortgageRelief.Text.Trim());
                    }
                    decimal insurancerelief;
                    if (!string.IsNullOrEmpty(txtInsuranceRelief.Text) && decimal.TryParse(txtInsuranceRelief.Text, out insurancerelief))
                    {
                        employee.InsuranceRelief = decimal.Parse(txtInsuranceRelief.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtNSSF.Text))
                    {
                        employee.NSSFNo = txtNSSF.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtNHIF.Text))
                    {
                        employee.NHIFNo = txtNHIF.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtIDNo.Text))
                    {
                        employee.IDNo = txtIDNo.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtPIN.Text))
                    {
                        employee.PINNo = txtPIN.Text.Trim();
                    }
                    if (cbDepartment.SelectedIndex != -1)
                    {
                        employee.DepartmentId = int.Parse(cbDepartment.SelectedValue.ToString());
                    }
                    if (cbEmployer.SelectedIndex != -1)
                    {
                        employee.EmployerId = int.Parse(cbEmployer.SelectedValue.ToString());
                    }
                    if (!string.IsNullOrEmpty(txtEmployeePaymentPoint.Text))
                    {
                        employee.PayPoint = txtEmployeePaymentPoint.Text.Trim();
                    }
                    if (cboEmployeeGroup.SelectedIndex != -1)
                    {
                        employee.EmpGroup = cboEmployeeGroup.SelectedValue.ToString();
                    }
                    if (cboEmpPayroll.SelectedIndex != -1)
                    {
                        employee.EmpPayroll = cboEmpPayroll.SelectedValue.ToString();
                    }
                    if (!string.IsNullOrEmpty(txtPrevEmployer.Text))
                    {
                        employee.PrevEmployer = txtPrevEmployer.Text.Trim();
                    }
                    employee.DateLeft = dtpDateLeft.Value;
                    if (cboModeofPayment.SelectedIndex != -1)
                    {
                        employee.PaymentMode = cboModeofPayment.SelectedValue.ToString();
                    }
                    switch (cboModeofPayment.SelectedValue.ToString())
                    {
                        case "M":
                            if (string.IsNullOrEmpty(txtTelephoneNo.Text))
                            {
                                txtTelephoneNo.Text = "0";
                                employee.TelephoneNo = txtTelephoneNo.Text.Trim();
                            }
                            else
                            {
                                employee.TelephoneNo = txtTelephoneNo.Text.Trim();
                            }
                            break;
                        case "B":
                            if (string.IsNullOrEmpty(txtAccount.Text))
                            {
                                txtAccount.Text = "0";
                                employee.BankAccount = txtAccount.Text.Trim();
                                employee.BankCode = txtBankSortCode.Text;
                            }
                            else
                            {
                                employee.BankAccount = txtAccount.Text.Trim();
                                employee.BankCode = txtBankSortCode.Text;
                            }
                            break;
                    }
                    employee.IsActive = chkIsActive.Checked;
                    employee.CreatedBy = _User;
                    employee.CreatedOn = DateTime.Now;

                    de.UpdateEmployee(employee);

                    UpdateBasicPay();

                    winSBPayroll.Forms.Employees f = (Employees)this.Owner;
                    f.RefreshGrid();
                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void UpdateBasicPay()
        {
            try
            {
                de.UpdateEmpTxnBasicPay(
                            employee.Id,
                            DateTime.Today,
                            employee.EmpNo,
                            "BASIC",
                            decimal.Parse(txtBasicPay.Text.Trim()));
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void EditEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                //Gender Combox
                var gender = new BindingList<KeyValuePair<string, string>>();
                gender.Add(new KeyValuePair<string, string>("M", "Male"));
                gender.Add(new KeyValuePair<string, string>("F", "Female"));
                cbGender.DataSource = gender;
                cbGender.ValueMember = "Key";
                cbGender.DisplayMember = "Value";

                //Marital Combox
                var marital = new BindingList<KeyValuePair<string, string>>();
                marital.Add(new KeyValuePair<string, string>("M", "Married"));
                marital.Add(new KeyValuePair<string, string>("S", "Single"));
                marital.Add(new KeyValuePair<string, string>("D", "Divorced"));
                cbMaritalStatus.DataSource = marital;
                cbMaritalStatus.ValueMember = "Key";
                cbMaritalStatus.DisplayMember = "Value";

                //payment            
                var paymentmodes = new BindingList<KeyValuePair<string, string>>();
                paymentmodes.Add(new KeyValuePair<string, string>("M", "MPESA"));
                paymentmodes.Add(new KeyValuePair<string, string>("B", "BANKACCOUNT"));
                paymentmodes.Add(new KeyValuePair<string, string>("C", "CASH"));
                cboModeofPayment.DataSource = paymentmodes;
                cboModeofPayment.ValueMember = "Key";
                cboModeofPayment.DisplayMember = "Value";

                //Basic Pay comutation lookup
                var bpay = new BindingList<KeyValuePair<string, string>>();
                bpay.Add(new KeyValuePair<string, string>("M", "Monthly"));
                bpay.Add(new KeyValuePair<string, string>("H", "Hourly"));
                bpay.Add(new KeyValuePair<string, string>("X", "Mixed(Monthly + Hourly)"));
                cbBasicComputation.DataSource = bpay;
                cbBasicComputation.ValueMember = "Key";
                cbBasicComputation.DisplayMember = "Value";

                //Employee Groups
                var _empgroups = new BindingList<KeyValuePair<string, string>>();
                _empgroups.Add(new KeyValuePair<string, string>("FTR", "Full Time - Regular"));
                _empgroups.Add(new KeyValuePair<string, string>("FTT", "Full Time - Temporary"));
                _empgroups.Add(new KeyValuePair<string, string>("PTR", "Part Time - Regular"));
                _empgroups.Add(new KeyValuePair<string, string>("PTT", "Part Time - Temporary"));
                cboEmployeeGroup.DataSource = _empgroups;
                cboEmployeeGroup.ValueMember = "Key";
                cboEmployeeGroup.DisplayMember = "Value";
                cboEmployeeGroup.SelectedIndex = -1;

                var _payrolltypesquery = from pt in db.PayrollTypes
                                         select pt;
                List<PayrollType> _payrolltypes = _payrolltypesquery.ToList();
                cboEmpPayroll.DataSource = _payrolltypes;
                cboEmpPayroll.DisplayMember = "Description";
                cboEmpPayroll.ValueMember = "Description";
                cboEmpPayroll.SelectedIndex = -1;

                pbPhoto.MouseHover += new EventHandler(pbPhoto_MouseHover);
                pbPhoto.MouseLeave += new EventHandler(pbPhoto_MouseLeave);
                pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;

                cbEmployer.DataSource = rep.GetAllActiveEmployers();
                cbEmployer.DisplayMember = "Name";
                cbEmployer.ValueMember = "Id";

                bindingSourceDept.DataSource = rep.GetNonDeletedDepartments();
                cbDepartment.DataSource = bindingSourceDept;
                cbDepartment.DisplayMember = "Description";
                cbDepartment.ValueMember = "Id";

                List<DAL.Benefit> allBenefits = rep.GetNonDeletedBenefits();
                comboBoxBenefit.DataSource = allBenefits;
                comboBoxBenefit.DisplayMember = "Description";
                comboBoxBenefit.ValueMember = "Id";

                cbBenefit.DataSource = allBenefits;
                cbBenefit.DisplayMember = "Description";
                cbBenefit.ValueMember = "Id";

                bindingSourceEmpBenefits.DataSource = de.GetEmpBenefits(employee.EmpNo);
                dataGridViewNonCashBenefits.AutoGenerateColumns = false;
                this.dataGridViewNonCashBenefits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewNonCashBenefits.DataSource = bindingSourceEmpBenefits;

                bindingSourceEmployeeCustomInfo.DataSource = de.GetAllEmployeeCustomInfo(employee.EmpNo);
                dataGridViewCustominfo.AutoGenerateColumns = false;
                this.dataGridViewCustominfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewCustominfo.DataSource = bindingSourceEmployeeCustomInfo;

                bindingSourceEmpTxns.DataSource = rep.ActiveEmpTxnList(employee.EmpNo);
                dataGridViewEmpTxn.AutoGenerateColumns = false;
                this.dataGridViewEmpTxn.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewEmpTxn.DataSource = bindingSourceEmpTxns;
                groupBox5.Text = "Employee Transactions  " + bindingSourceEmpTxns.Count.ToString();

                bindingSourceHourlyPayments.DataSource = de.GetHourlyPaymentsList(employee.EmpNo);
                dataGridHourlyPayments.AutoGenerateColumns = false;
                this.dataGridHourlyPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridHourlyPayments.DataSource = bindingSourceHourlyPayments;

                this.txtEmpNoHourlyPayment.Enabled = false;
                this.txtEmpNo.Enabled = true;
                this.btnNextSeries.Enabled = true;
                this.txtFieldInt.Text = "0";
                this.txtFieldDecimal.Text = "0";
                this.lblNoofYears.Text = string.Empty;

                switch (employee.BasicComputation)
                {
                    case "H":
                        txtBasicPay.Visible = false;
                        lblBasicPay.Visible = false;
                        break;
                    case "M":
                        tabControl1.TabPages.Remove(HourlyPaymentTabPage);
                        txtBasicPay.Visible = true;
                        lblBasicPay.Visible = true;
                        break;
                }

                InitializeControls();

                tabControl1.TabPages.Remove(HourlyPaymentTabPage);
                tabControl1.TabPages.Remove(tabPage5);
                tabControl1.TabPages.Remove(tabPage6);

                AutoCompleteStringCollection acscempno = new AutoCompleteStringCollection();
                acscempno.AddRange(this.AutoComplete_EmpNo());
                txtEmpNo.AutoCompleteCustomSource = acscempno;
                txtEmpNo.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtEmpNo.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscSurname = new AutoCompleteStringCollection();
                acscSurname.AddRange(this.AutoComplete_Surnames());
                txtSurname.AutoCompleteCustomSource = acscSurname;
                txtSurname.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtSurname.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscothernames = new AutoCompleteStringCollection();
                acscothernames.AddRange(this.AutoComplete_OtherNames());
                txtOtherNames.AutoCompleteCustomSource = acscothernames;
                txtOtherNames.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtOtherNames.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscbasicpay = new AutoCompleteStringCollection();
                acscbasicpay.AddRange(this.AutoComplete_BasicPay());
                txtBasicPay.AutoCompleteCustomSource = acscbasicpay;
                txtBasicPay.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBasicPay.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscpersonalrelief = new AutoCompleteStringCollection();
                acscpersonalrelief.AddRange(this.AutoComplete_PersonalRelief());
                txtPersonalRelief.AutoCompleteCustomSource = acscpersonalrelief;
                txtPersonalRelief.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtPersonalRelief.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscmorgagerelief = new AutoCompleteStringCollection();
                acscmorgagerelief.AddRange(this.AutoComplete_MortgageRelief());
                txtMortgageRelief.AutoCompleteCustomSource = acscmorgagerelief;
                txtMortgageRelief.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtMortgageRelief.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscinsurancerelief = new AutoCompleteStringCollection();
                acscinsurancerelief.AddRange(this.AutoComplete_InsuranceRelief());
                txtInsuranceRelief.AutoCompleteCustomSource = acscinsurancerelief;
                txtInsuranceRelief.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtInsuranceRelief.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscnssf = new AutoCompleteStringCollection();
                acscnssf.AddRange(this.AutoComplete_NSSF());
                txtNSSF.AutoCompleteCustomSource = acscnssf;
                txtNSSF.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtNSSF.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscnhif = new AutoCompleteStringCollection();
                acscnhif.AddRange(this.AutoComplete_NHIF());
                txtNHIF.AutoCompleteCustomSource = acscnhif;
                txtNHIF.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtNHIF.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscidno = new AutoCompleteStringCollection();
                acscidno.AddRange(this.AutoComplete_IDNo());
                txtIDNo.AutoCompleteCustomSource = acscidno;
                txtIDNo.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtIDNo.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscpin = new AutoCompleteStringCollection();
                acscpin.AddRange(this.AutoComplete_PIN());
                txtPIN.AutoCompleteCustomSource = acscpin;
                txtPIN.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtPIN.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscsortcode = new AutoCompleteStringCollection();
                acscsortcode.AddRange(this.AutoComplete_BankSortCodes());
                txtBankSortCode.AutoCompleteCustomSource = acscsortcode;
                txtBankSortCode.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBankSortCode.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
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
        private string[] AutoComplete_Surnames()
        {
            try
            {
                var surnamesquery = from bk in rep.GetAllActiveEmployees()
                                    where bk.IsActive == true
                                    where bk.IsDeleted == false
                                    select bk.Surname;
                return surnamesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_OtherNames()
        {
            try
            {
                var othernamesquery = from bk in rep.GetAllActiveEmployees()
                                      where bk.IsActive == true
                                      where bk.IsDeleted == false
                                      select bk.OtherNames;
                return othernamesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_EmpNo()
        {
            try
            {
                var othernamesquery = from bk in rep.GetAllActiveEmployees()
                                      where bk.IsActive == true
                                      where bk.IsDeleted == false
                                      select bk.EmpNo;
                return othernamesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_BasicPay()
        {
            try
            {
                var insurancequery = (from sb in rep.GetAllActiveEmployees()
                                      where sb.IsActive == true
                                      where sb.IsDeleted == false
                                      select sb.BasicPay).Distinct();
                decimal?[] decimalarray = insurancequery.ToArray();
                List<string> items = new List<string>();
                for (int i = 0; i < insurancequery.Count(); i++)
                {
                    string strName = decimalarray[i].ToString();
                    items.Add(strName);
                }
                return items.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_PersonalRelief()
        {
            try
            {
                var insurancequery = (from sb in rep.GetAllActiveEmployees()
                                      where sb.IsActive == true
                                      where sb.IsDeleted == false
                                      select sb.PersonalRelief).Distinct();
                decimal?[] decimalarray = insurancequery.ToArray();
                List<string> items = new List<string>();
                for (int i = 0; i < insurancequery.Count(); i++)
                {
                    string strName = decimalarray[i].ToString();
                    items.Add(strName);
                }
                return items.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_MortgageRelief()
        {
            try
            {
                var insurancequery = (from sb in rep.GetAllActiveEmployees()
                                      where sb.IsActive == true
                                      where sb.IsDeleted == false
                                      select sb.MortgageRelief).Distinct();
                decimal?[] decimalarray = insurancequery.ToArray();
                List<string> items = new List<string>();
                for (int i = 0; i < insurancequery.Count(); i++)
                {
                    string strName = decimalarray[i].ToString();
                    items.Add(strName);
                }
                return items.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_InsuranceRelief()
        {
            try
            {
                var insurancequery = (from sb in rep.GetAllActiveEmployees()
                                      where sb.IsActive == true
                                      where sb.IsDeleted == false
                                      select sb.InsuranceRelief).Distinct();
                decimal?[] decimalarray = insurancequery.ToArray();
                List<string> items = new List<string>();
                for (int i = 0; i < insurancequery.Count(); i++)
                {
                    string strName = decimalarray[i].ToString();
                    items.Add(strName);
                }
                return items.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_NSSF()
        {
            try
            {
                var othernamesquery = from bk in rep.GetAllActiveEmployees()
                                      where bk.IsActive == true
                                      where bk.IsDeleted == false
                                      select bk.NSSFNo;
                return othernamesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_NHIF()
        {
            try
            {
                var othernamesquery = from bk in rep.GetAllActiveEmployees()
                                      where bk.IsActive == true
                                      where bk.IsDeleted == false
                                      select bk.NHIFNo;
                return othernamesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_IDNo()
        {
            try
            {
                var surnamesquery = from bk in rep.GetAllActiveEmployees()
                                    where bk.IsActive == true
                                    where bk.IsDeleted == false
                                    select bk.IDNo;
                return surnamesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_PIN()
        {
            try
            {
                var othernamesquery = from bk in rep.GetAllActiveEmployees()
                                      where bk.IsActive == true
                                      where bk.IsDeleted == false
                                      select bk.PINNo;
                return othernamesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsEmployeeValid()
        {
            bool no_error = true;

            if (string.IsNullOrEmpty(txtEmpNo.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmpNo, "Employee Number cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtSurname.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtSurname, "Surname cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtOtherNames.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtOtherNames, "Other Names cannot be null!");
                return false;
            }
            int minAge = int.Parse(de.SettingLookup("MINAGE"));
            DateTime minDoB = DateTime.Today.AddYears(-minAge);

            if (dtpDOB.Value > minDoB)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(dtpDOB, "Employee Must be 18 Years and Above!");
                return false;
            }
            if (cbMaritalStatus.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cbMaritalStatus, "Select Marital Status!");
                return false;
            }
            if (cbGender.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cbGender, "Select Gender!");
                return false;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmail, "Email cannot be null!");
                return false;
            }
            string emailregex = de.SettingLookup("EMAILREGEX");
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(emailregex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (!rEMail.IsMatch(txtEmail.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmail, "Please provide a valid Email e.g username@domain.com");
                return false;
            }
            if (cbBasicComputation.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cbBasicComputation, "Select Basic Computation!");
                return false;
            }
            if (string.IsNullOrEmpty(txtBasicPay.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBasicPay, "Basic Pay cannot be null!");
                return false;
            }
            decimal basic;
            if (!decimal.TryParse(txtBasicPay.Text, out basic))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBasicPay, "Basic Pay must be a decimal");
                return false;
            }
            if (string.IsNullOrEmpty(txtNSSF.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtNSSF, "NSSF No cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtNHIF.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtNHIF, "NHIF No cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtIDNo.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtIDNo, "ID No cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtPIN.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPIN, "PIN No cannot be null!");
                return false;
            }
            if (cbDepartment.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cbDepartment, "Select Department!");
                return false;
            }
            if (cbEmployer.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cbEmployer, "Select Employer!");
                return false;
            }
            if (cboModeofPayment.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboModeofPayment, "Select Mode of Payment!");
                return false;
            }
            if ("B".Equals(cboModeofPayment.SelectedValue) && string.IsNullOrEmpty(txtBankSortCode.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBankSortCode, "Bank Sort Code cannot be null!");
                return false;
            }
            if ("B".Equals(cboModeofPayment.SelectedValue) && !string.IsNullOrEmpty(txtBankSortCode.Text))
            {
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
            }
            if ("B".Equals(cboModeofPayment.SelectedValue) && string.IsNullOrEmpty(txtAccount.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAccount, "Bank Account cannot be null!");
                return false;
            }
            if ("M".Equals(cboModeofPayment.SelectedValue) && string.IsNullOrEmpty(txtTelephoneNo.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtTelephoneNo, "Phone Number cannot be null!");
                return false;
            }
            return no_error;
        }
        private void btnNextSeries_Click(object sender, EventArgs e)
        {
            try
            {
                txtEmpNo.Text = Utils.NextSeries(txtEmpNo.Text);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnEmpTxnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddEmpTxn f = new AddEmpTxn(employee, _User, connection);
                f.Owner = this;
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnEmpTxnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEmpTxn.SelectedRows.Count != 0)
                {
                    DAL.EmployeeTransaction _empTxn = (DAL.EmployeeTransaction)bindingSourceEmpTxns.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "PENSION" };
                    if (!defaultItems.Contains(_empTxn.ItemId.Trim()))
                    {

                        if ("HOURLY_PAY".Equals(_empTxn.ItemId.Trim()))
                        {
                            List<HourlyPayment> listemphrly = db.HourlyPayments.Where(ben => ben.Empno == employee.EmpNo).ToList();
                            if (listemphrly.Count > 0)
                            {
                                MessageBox.Show("There are Transactions Associated with this HOURLY_PAY.\n Delete the Transactions First!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }

                            //foreach (HourlyPayment emphrly in listemphrly)
                            //{
                            //    db.HourlyPayments.DeleteObject(emphrly);
                            //}
                            //db.SaveChanges();
                            //de.DeleteEmpTxn(_empTxn.Id);
                            //GridRefresh();
                            //MessageBox.Show("Teacher with ID No Exists!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if ("NON_CASH_BENEFIT".Equals(_empTxn.ItemId.Trim()))
                        {

                            List<EmpNonCashBenefit> listempncben = db.EmpNonCashBenefits.Where(ben => ben.EmpNo == employee.EmpNo).ToList();
                            if (listempncben.Count > 0)
                            {
                                MessageBox.Show("There are Transactions Associated with this NON_CASH_BENEFIT.\n Delete the Transactions First!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                            //foreach (EmpNonCashBenefit empben in listempncben)
                            //{
                            //    db.EmpNonCashBenefits.DeleteObject(empben);
                            //}
                            //db.SaveChanges();
                            //de.DeleteEmpTxn(_empTxn.Id);
                            //GridRefresh();
                            //MessageBox.Show("Teacher with ID No Exists!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete?\n" + _empTxn.ItemId.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                            {
                                de.DeleteEmpTxn(_empTxn.Id);
                                GridRefresh();
                            }
                        }
                    }
                    else
                    {
                        //if (employee.BasicComputation.Equals("H"))
                        //{
                        //    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete?\n" + _empTxn.ItemId.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        //    {
                        //        de.DeleteEmpTxn(_empTxn.Id);
                        //        GridRefresh();
                        //    }
                        //}
                        //else
                        //{
                            //MessageBox.Show("Cannot Delete default item\n" + _empTxn.ItemId.ToString().Trim().ToUpper() + "\nif payment mode is monthly.", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}

                        MessageBox.Show("Cannot Delete default item\n" + _empTxn.ItemId.ToString().Trim().ToUpper() + ".", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnEmpTxnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewEmpTxn.SelectedRows.Count != 0)
                {
                    DAL.EmployeeTransaction _empTxn = (DAL.EmployeeTransaction)bindingSourceEmpTxns.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "PENSION" };
                    if (!defaultItems.Contains(_empTxn.ItemId.Trim()))
                    {
                        if ("HOURLY_PAY".Equals(_empTxn.ItemId.Trim()))
                        {
                            HrlyPay hp = new HrlyPay(employee, connection) { Owner = this };
                            hp.OnEmployeeHrlyAmountChanged += new HrlyPay.HrlyAmountHandler(hp_OnEmployeeHrlyAmountChanged);
                            hp.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                            //hp.Owner = this;
                            hp.ShowDialog(this);
                        }
                        else if ("NON_CASH_BENEFIT".Equals(_empTxn.ItemId.Trim()))
                        {
                            NonCashBenefits ncb = new NonCashBenefits(employee, connection) { Owner = this };
                            ncb.OnEmployeeBenefitAmountChanged += new NonCashBenefits.BenefitAmountHandler(ncb_OnEmployeeBenefitAmountChanged);
                            ncb.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                            //ncb.Owner = this;
                            ncb.ShowDialog(this);
                        }
                        else
                        {
                            EditEmpTxn f = new EditEmpTxn(_empTxn, _User, connection) { Owner = this };
                            f.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                            f.ShowDialog();
                        }

                    }
                    else
                    {
                        //if (employee.BasicComputation.Equals("H"))
                        //{
                        //    EditEmpTxn f = new EditEmpTxn(_empTxn, _User, connection) { Owner = this };
                        //    f.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                        //    f.ShowDialog();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Cannot Edit default item\n" + _empTxn.ItemId.ToString().Trim().ToUpper() + "\nif payment mode is monthly.", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}

                        EditEmpTxn f = new EditEmpTxn(_empTxn, _User, connection) { Owner = this };
                        f.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                        f.ShowDialog();

                    }

                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewEmpTxn_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridViewEmpTxn.SelectedRows.Count != 0)
                {
                    DAL.EmployeeTransaction _empTxn = (DAL.EmployeeTransaction)bindingSourceEmpTxns.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF" };
                    if (!defaultItems.Contains(_empTxn.ItemId.Trim()))
                    {
                        if ("HOURLY_PAY".Equals(_empTxn.ItemId.Trim()))
                        {
                            HrlyPay hp = new HrlyPay(employee, connection) { Owner = this };
                            hp.OnEmployeeHrlyAmountChanged += new HrlyPay.HrlyAmountHandler(hp_OnEmployeeHrlyAmountChanged);
                            hp.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                            //hp.Owner = this;
                            hp.ShowDialog(this);
                        }
                        else if ("NON_CASH_BENEFIT".Equals(_empTxn.ItemId.Trim()))
                        {
                            NonCashBenefits ncb = new NonCashBenefits(employee, connection) { Owner = this };
                            ncb.OnEmployeeBenefitAmountChanged += new NonCashBenefits.BenefitAmountHandler(ncb_OnEmployeeBenefitAmountChanged);
                            ncb.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                            //ncb.Owner = this;
                            ncb.ShowDialog(this);
                        }
                        else
                        {
                            EditEmpTxn f = new EditEmpTxn(_empTxn, _User, connection) { Owner = this };
                            f.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                            f.ShowDialog();
                        }

                    }
                    else
                    {
                        //if (employee.BasicComputation.Equals("H"))
                        //{
                        //    EditEmpTxn f = new EditEmpTxn(_empTxn, _User, connection) { Owner = this };
                        //    f.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                        //    f.ShowDialog();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Cannot Edit default item\n" + _empTxn.ItemId.ToString().Trim().ToUpper() + "\nif payment mode is monthly.", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}

                        EditEmpTxn f = new EditEmpTxn(_empTxn, _User, connection) { Owner = this };
                        f.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                        f.ShowDialog();

                    }

                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void ncb_OnEmployeeBenefitAmountChanged(object sender, BenefitAmountHandlerEventArgs e)
        {
            try
            {
                DAL.EmployeeTransaction _empTxn = (DAL.EmployeeTransaction)bindingSourceEmpTxns.Current;
                de.UpdateEmpTxn(_empTxn.Id,
                            DateTime.Today,
                            employee.EmpNo,
                            "NON_CASH_BENEFIT",
                            e.Totalquantity);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void hp_OnEmployeeHrlyAmountChanged(object sender, HrlyAmountHandlerEventArgs e)
        {
            try
            {
                DAL.EmployeeTransaction _empTxn = (DAL.EmployeeTransaction)bindingSourceEmpTxns.Current;
                de.UpdateEmpTxn(_empTxn.Id,
                            DateTime.Today,
                            employee.EmpNo,
                            "HOURLY_PAY",
                            e.TotalHrlyAmount);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public void GridRefresh()
        {
            try
            {
                if (chkEmpTxnIsActive.Checked)
                {
                    bindingSourceEmpTxns.DataSource = null;
                    bindingSourceEmpTxns.DataSource = de.EmpTxnList(employee.EmpNo);
                    groupBox5.Text = "Employee Transactions  " + bindingSourceEmpTxns.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmpTxn.Rows)
                    {
                        dataGridViewEmpTxn.Rows[dataGridViewEmpTxn.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmpTxn.Rows.Count - 1;
                        bindingSourceEmpTxns.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourceEmpTxns.DataSource = null;
                    bindingSourceEmpTxns.DataSource = rep.ActiveEmpTxnList(employee.EmpNo);
                    groupBox5.Text = "Employee Transactions  " + bindingSourceEmpTxns.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmpTxn.Rows)
                    {
                        dataGridViewEmpTxn.Rows[dataGridViewEmpTxn.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmpTxn.Rows.Count - 1;
                        bindingSourceEmpTxns.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        /*public method to diable all controls when form is called by parent from 'View Details' button*/
        public void DisableControls()
        {
            try
            {
                txtBankSortCode.Enabled = false;
                cbBasicComputation.Enabled = false;
                txtAccount.Enabled = false;
                txtBasicPay.Enabled = false;
                dtpDateLeft.Enabled = false;
                cbDepartment.Enabled = false;
                dtpDOB.Enabled = false;
                dtpDOE.Enabled = false;
                cboEmployeeGroup.Enabled = false;
                cbEmployer.Enabled = false;
                txtEmpNo.Enabled = false;
                cboEmpPayroll.Enabled = false;
                cbGender.Enabled = false;
                txtIDNo.Enabled = false;
                chkIsActive.Enabled = false;
                cbMaritalStatus.Enabled = false;
                txtMortgageRelief.Enabled = false;
                txtNHIF.Enabled = false;
                txtNSSF.Enabled = false;
                txtOtherNames.Enabled = false;
                txtEmployeePaymentPoint.Enabled = false;
                txtPersonalRelief.Enabled = false;
                txtPIN.Enabled = false;
                txtPrevEmployer.Enabled = false;
                txtSurname.Enabled = false;
                dataGridViewEmpTxn.Enabled = false;
                btnUpdate.Visible = false;
                btnNextSeries.Enabled = false;
                btnEmpTxnAdd.Visible = false;
                btnEmpTxnEdit.Visible = false;
                btnEmpTxnDelete.Visible = false;
                txtQty.Enabled = false;
                comboBoxBenefit.Enabled = false;
                dataGridViewNonCashBenefits.Enabled = false;
                btnAddBenefit.Enabled = false;
                btnDeleteBenefit.Enabled = false;
                btnUpload.Enabled = false;
                cboModeofPayment.Enabled = false;
                txtTelephoneNo.Enabled = false;
                cboEmpNo.Enabled = false;
                txtFieldName.Enabled = false;
                txtFieldString.Enabled = false;
                txtFieldInt.Enabled = false;
                dateTimePicker1.Enabled = false;
                txtFieldDecimal.Enabled = false;
                btnAddCustomInfo.Enabled = false;
                dataGridViewCustominfo.Enabled = false;
                cboModeofPayment.Enabled = false;
                txtChequeNo.Enabled = false;
                txtTelephoneNo.Enabled = false;
                txtEmpNoHourlyPayment.Enabled = false;
                dtpWorkDate.Enabled = false;
                txtWorkHours.Enabled = false;
                txtRatePerHour.Enabled = false;
                btnAddHourlyPayment.Enabled = false;
                btnEditHourlyPayment.Enabled = false;
                btnDeleteHourlyPayment.Enabled = false;
                dataGridHourlyPayments.Enabled = false;
                txtInsuranceRelief.Enabled = false;
                btnSearchBank.Enabled = false;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewNonCashBenefits_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                de.Save();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnAddBenefit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Enter a valid quantity");
                    return;
                }

                int qty;
                if (!int.TryParse(txtQty.Text, out qty))
                {
                    MessageBox.Show("Enter a valid quantity");
                    return;
                }
                DAL.EmpNonCashBenefit ben = new DAL.EmpNonCashBenefit();
                ben.BenefitId = ((DAL.Benefit)comboBoxBenefit.SelectedItem).Id;
                ben.EmpNo = employee.EmpNo;
                ben.Quantity = qty;

                de.AddEmpBenefit(ben);
                txtQty.Text = "";
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }

        private void btnDeleteBenefit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewNonCashBenefits.SelectedRows.Count != 0)
                {
                    DAL.EmpNonCashBenefit ben = (DAL.EmpNonCashBenefit)bindingSourceEmpBenefits.Current;
                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Benefit\n" + ben.BenefitId.ToString().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        de.DeleteEmpBenefit(ben);
                        RefreshGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnUpdateBenefit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("Enter a valid quantity");
                    return;
                }

                int qty;
                if (!int.TryParse(txtQty.Text, out qty))
                {
                    MessageBox.Show("Enter a valid quantity");
                    return;
                }
                DAL.EmpNonCashBenefit ben = new DAL.EmpNonCashBenefit();
                ben.BenefitId = ((DAL.Benefit)comboBoxBenefit.SelectedItem).Id;
                ben.EmpNo = employee.EmpNo;
                ben.Quantity = qty;

                de.AddEmpBenefit(ben);
                txtQty.Text = "";
                RefreshGrid();
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
                //set the datasource to null
                bindingSourceEmpBenefits.DataSource = null;
                dataGridViewCustominfo.DataSource = null;
                dataGridHourlyPayments.DataSource = null;
                dataGridViewEmpTxn.DataSource = null;

                //set the datasource to a method
                bindingSourceEmpBenefits.DataSource = de.GetEmpBenefits(employee.EmpNo);
                dataGridViewCustominfo.DataSource = de.GetAllEmployeeCustomInfo(employee.EmpNo);
                dataGridHourlyPayments.DataSource = de.GetHourlyPaymentsList(employee.EmpNo);
                dataGridViewEmpTxn.DataSource = dataGridViewEmpTxn.DataSource = bindingSourceEmpTxns;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void bindingSourceEmpBenefits_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteContext == BindingCompleteContext.DataSourceUpdate
                && e.Exception == null)
                e.Binding.BindingManagerBase.EndCurrentEdit();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "select an Image File";
            openFileDialog1.Filter = "Image File (*.jpg)|*.jpg|Image File (*.gif)|*.gif|Image File (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.ShowDialog();
            string strFileName = openFileDialog1.FileName;
            try
            {
                UploadEmployeePhoto(strFileName, _User);
                MessageBox.Show("Upload completed successfully", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error during upload. Error details are  " + ex.Message, Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Upload incomplete", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void UploadEmployeePhoto(string strFileName, string User)
        {

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
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int fieldint;
            if (!int.TryParse(txtFieldInt.Text, out fieldint))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtFieldInt, "Field must be a integer");

            }

            decimal fielddecimal;
            if (!decimal.TryParse(txtFieldDecimal.Text, out fielddecimal))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtFieldDecimal, "Field must be a decimal");

            }
            try
            {
                employee_ext = new DAL.Employee_Ext();
                employee_ext.EmpNo = cboEmpNo.Text;
                employee_ext.ExFieldName = int.Parse(txtFieldName.Text);
                employee_ext.ExFieldStr = txtFieldString.Text;
                employee_ext.ExFieldInt = int.Parse(txtFieldInt.Text);
                employee_ext.ExFieldDate = DateTime.Parse(dateTimePicker1.Text);
                employee_ext.ExFieldDecimal = decimal.Parse(txtFieldDecimal.Text);

                de.AddEmployeeCustomInfo(employee_ext);

                this.RefreshGrid();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewCustominfo_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void cboModeofPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModeofPayment.SelectedIndex != -1)
            {
                try
                {
                    switch (cboModeofPayment.SelectedValue.ToString())
                    {
                        case "M":
                            txtChequeNo.Visible = false;
                            lblpaymentmode.Text = "Enter phone Number*";
                            lblpaymentmode.Visible = true;
                            txtTelephoneNo.Visible = true;
                            gbBank.Visible = false;
                            txtBankSortCode.Visible = false;
                            btnSearchBank.Visible = false;
                            txtAccount.Visible = false;
                            break;
                        case "B":
                            txtChequeNo.Visible = false;
                            gbBank.Location = txtTelephoneNo.Location;
                            gbBank.Visible = true;
                            txtBankSortCode.Visible = true;
                            btnSearchBank.Visible = true;
                            txtAccount.Visible = true;
                            lblpaymentmode.Visible = false;
                            txtTelephoneNo.Visible = false;
                            break;
                        case "C":
                            txtChequeNo.Visible = false;
                            gbBank.Visible = false;
                            txtBankSortCode.Visible = false;
                            btnSearchBank.Visible = false;
                            txtAccount.Visible = false;
                            lblpaymentmode.Visible = false;
                            txtTelephoneNo.Visible = false;
                            break;

                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void btnAddHourlyPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtWorkHours.Text))
                {
                    MessageBox.Show("Give working Hours!");
                    return;
                }

                if (string.IsNullOrEmpty(txtRatePerHour.Text))
                {
                    MessageBox.Show("Give Rate of Pay Per Hour!");
                    return;
                }

                int workhours;
                if (!int.TryParse(txtWorkHours.Text, out workhours))
                {
                    MessageBox.Show("Give valid working hours!");
                    return;
                }

                int rate;
                if (!int.TryParse(txtRatePerHour.Text, out rate))
                {
                    MessageBox.Show("Give  valid Pay Per Hour Rate!");
                    return;
                }

                DAL.HourlyPayment hourpay = new DAL.HourlyPayment();

                hourpay.Empno = employee.EmpNo;
                hourpay.WorkDate = dtpWorkDate.Value;
                hourpay.WorkHours = workhours;
                hourpay.RatePerHour = rate;

                de.SaveHourlyPayment(hourpay);

                txtWorkHours.Text = "";
                txtRatePerHour.Text = "";
                RefreshGrid();

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnEditHourlyPayment_Click(object sender, EventArgs e)
        {
            if (dataGridHourlyPayments.SelectedRows.Count != 0)
            {
                try
                {


                    if ("Edit".Equals(btnEditHourlyPayment.Text))
                    {
                        DAL.HourlyPayment hour = (DAL.HourlyPayment)bindingSourceHourlyPayments.Current;

                        dtpWorkDate.Text = string.Empty;
                        txtWorkHours.Text = string.Empty;
                        txtRatePerHour.Text = string.Empty;

                        dtpWorkDate.Value = (DateTime)hour.WorkDate;
                        txtWorkHours.Text = hour.WorkHours.ToString();
                        txtRatePerHour.Text = hour.RatePerHour.ToString();

                        btnEditHourlyPayment.Text = "Update";

                    }
                    if ("Update".Equals(btnEditHourlyPayment.Text))
                    {
                        btnEditHourlyPayment.Text = "Edit";
                        RefreshGrid();
                    }

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void btnDeleteHourlyPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridHourlyPayments.SelectedRows.Count != 0)
                {
                    DAL.HourlyPayment hour = (DAL.HourlyPayment)bindingSourceHourlyPayments.Current;

                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete?", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        de.DeleteHourlyPayment(hour);
                        RefreshGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void cbBasicComputation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBasicComputation.SelectedIndex != -1)
            {
                try
                {
                    switch (cbBasicComputation.SelectedValue.ToString())
                    {
                        case "H":
                            txtBasicPay.Text = "0";
                            txtBasicPay.Visible = false;
                            lblBasicPay.Visible = false;
                            break;
                        case "M":
                            txtBasicPay.Visible = true;
                            lblBasicPay.Visible = true;
                            break;
                        case "X":
                            txtBasicPay.Visible = true;
                            lblBasicPay.Visible = true;
                            break;
                    }

                    //select employee payroll group and payroll category according to computation type
                    switch (cbBasicComputation.SelectedValue.ToString())
                    {
                        case "H":
                            cboEmployeeGroup.SelectedValue = "PTR";
                            cboEmpPayroll.SelectedValue = "Temporary";
                            break;
                        case "M":
                            cboEmployeeGroup.SelectedValue = "FTR";
                            cboEmpPayroll.SelectedValue = "End-Month";
                            break;
                        case "X":
                            cboEmployeeGroup.SelectedValue = "FTT";
                            cboEmpPayroll.SelectedValue = "End-Month";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void cboEmployeeGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEmployeeGroup.SelectedIndex != -1)
            {
                try
                {
                    switch (cboEmployeeGroup.SelectedValue.ToString())
                    {
                        case "PTR":
                            cboEmpPayroll.SelectedValue = "Temporary";
                            break;
                        case "PTT":
                            cboEmpPayroll.SelectedValue = "Temporary";
                            break;
                        case "FTR":
                            cboEmpPayroll.SelectedValue = "End-Month";
                            break;
                        case "FTT":
                            cboEmpPayroll.SelectedValue = "End-Month";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void pbPhoto_MouseHover(object sender, EventArgs e)
        {
            try
            {
                //setting a border when it is moused over
                ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void pbPhoto_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                //removing the border when the mouse leaves it
                ((PictureBox)sender).BorderStyle = BorderStyle.None;
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
        private void txtBasicPay_KeyDown(object sender, KeyEventArgs e)
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
        private void txtBasicPay_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtPersonalRelief_KeyDown(object sender, KeyEventArgs e)
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
        private void txtPersonalRelief_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtMortgageRelief_KeyDown(object sender, KeyEventArgs e)
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
        private void txtMortgageRelief_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtInsuranceRelief_KeyDown(object sender, KeyEventArgs e)
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
        private void txtInsuranceRelief_KeyPress(object sender, KeyPressEventArgs e)
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
        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int datetoday = DateTime.Today.Year;
                int selecteddate = dtpDOB.Value.Year;
                int noofyrs = datetoday - selecteddate;
                lblNoofYears.Text = noofyrs.ToString() + "  Years";
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmpTxn.SelectedRows.Count != 0)
            {
                try
                {

                    DAL.EmployeeTransaction _empTxn = (DAL.EmployeeTransaction)bindingSourceEmpTxns.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF" };
                    if (!defaultItems.Contains(_empTxn.ItemId.Trim()))
                    {
                        if ("HOURLY_PAY".Equals(_empTxn.ItemId.Trim()))
                        {
                            HrlyPay hp = new HrlyPay(employee, connection) { Owner = this };
                            hp.OnEmployeeHrlyAmountChanged += new HrlyPay.HrlyAmountHandler(hp_OnEmployeeHrlyAmountChanged);
                            hp.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                            hp.DisableControls();
                            hp.ShowDialog(this);
                        }
                        else if ("NON_CASH_BENEFIT".Equals(_empTxn.ItemId.Trim()))
                        {
                            NonCashBenefits ncb = new NonCashBenefits(employee, connection) { Owner = this };
                            ncb.OnEmployeeBenefitAmountChanged += new NonCashBenefits.BenefitAmountHandler(ncb_OnEmployeeBenefitAmountChanged);
                            ncb.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                            ncb.DisableControls();
                            ncb.ShowDialog(this);
                        }
                        else
                        {
                            EditEmpTxn f = new EditEmpTxn(_empTxn, _User, connection) { Owner = this };
                            f.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                            f.DisableControls();
                            f.ShowDialog();
                        }
                    }
                    else
                    {
                        EditEmpTxn f = new EditEmpTxn(_empTxn, _User, connection) { Owner = this };
                        f.Text = _empTxn.ItemId.ToString().Trim().ToUpper();
                        f.DisableControls();
                        f.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void chkEmpTxnIsActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEmpTxnIsActive.Checked)
                {
                    bindingSourceEmpTxns.DataSource = null;
                    bindingSourceEmpTxns.DataSource = de.EmpTxnList(employee.EmpNo);
                    groupBox5.Text = "Employee Transactions  " + bindingSourceEmpTxns.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmpTxn.Rows)
                    {
                        dataGridViewEmpTxn.Rows[dataGridViewEmpTxn.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmpTxn.Rows.Count - 1;
                        bindingSourceEmpTxns.Position = nRowIndex;
                    }
                }
                else
                {
                    bindingSourceEmpTxns.DataSource = null;
                    bindingSourceEmpTxns.DataSource = rep.ActiveEmpTxnList(employee.EmpNo);
                    groupBox5.Text = "Employee Transactions  " + bindingSourceEmpTxns.Count.ToString();
                    foreach (DataGridViewRow row in dataGridViewEmpTxn.Rows)
                    {
                        dataGridViewEmpTxn.Rows[dataGridViewEmpTxn.Rows.Count - 1].Selected = true;
                        int nRowIndex = dataGridViewEmpTxn.Rows.Count - 1;
                        bindingSourceEmpTxns.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void cboEmpPayroll_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboEmpPayroll.SelectedIndex == -1)
                {
                    txtPayrollTypeRemarks.Visible = false;
                    txtPayrollTypeRemarks.Text = string.Empty;
                }
                if (cboEmpPayroll.SelectedIndex != -1)
                {
                    PayrollType _payrolltype = (PayrollType)cboEmpPayroll.SelectedItem;

                    if (_payrolltype != null)
                    {
                        txtPayrollTypeRemarks.Visible = true;
                        txtPayrollTypeRemarks.Text = _payrolltype.Remarks;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtPayrollTypeRemarks_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsDigit(e.KeyChar)) e.Handled = true;
                if (Char.IsLetter(e.KeyChar)) e.Handled = true;
                if (Char.IsNumber(e.KeyChar)) e.Handled = true;
                if (Char.IsPunctuation(e.KeyChar)) e.Handled = true;
                if (Char.IsSurrogate(e.KeyChar)) e.Handled = true;
                if (Char.IsSymbol(e.KeyChar)) e.Handled = true;
                if (Char.IsWhiteSpace(e.KeyChar)) e.Handled = true;
                if (e.KeyChar == (char)Keys.Back) e.Handled = true;
                if (e.KeyChar == (char)Keys.Space) e.Handled = true;
                if (e.KeyChar == (char)Keys.Delete) e.Handled = true;
                if (e.KeyChar == (char)Keys.Clear) e.Handled = true;
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














    }
}