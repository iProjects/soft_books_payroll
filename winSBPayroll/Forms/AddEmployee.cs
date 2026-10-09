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
    public partial class AddEmployee : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _User;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;
        public string TAG;
        //Event declaration:
        //event for publishing messages to output
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public AddEmployee(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _User = user;

            TAG = this.GetType().Name;

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished AddEmployee initialization", TAG));

        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {
            try
            {
                //Put default figures
                txtMortgageRelief.Text = "0";
                txtPersonalRelief.Text = "0";
                txtInsuranceRelief.Text = "0";

                //Gender Combox
                var gender = new BindingList<KeyValuePair<string, string>>();
                gender.Add(new KeyValuePair<string, string>("M", "Male"));
                gender.Add(new KeyValuePair<string, string>("F", "Female"));
                cbo_gender.DataSource = gender;
                cbo_gender.ValueMember = "Key";
                cbo_gender.DisplayMember = "Value";
                cbo_gender.SelectedIndex = -1;

                //Marital Combox
                var marital = new BindingList<KeyValuePair<string, string>>();
                marital.Add(new KeyValuePair<string, string>("M", "Married"));
                marital.Add(new KeyValuePair<string, string>("S", "Single"));
                marital.Add(new KeyValuePair<string, string>("D", "Divorced"));
                cbo_marital_status.DataSource = marital;
                cbo_marital_status.ValueMember = "Key";
                cbo_marital_status.DisplayMember = "Value";
                cbo_marital_status.SelectedIndex = -1;

                //Employee Groups
                var _empgroups = new BindingList<KeyValuePair<string, string>>();
                _empgroups.Add(new KeyValuePair<string, string>("FTR", "Full Time - Regular"));
                _empgroups.Add(new KeyValuePair<string, string>("FTT", "Full Time - Temporary"));
                _empgroups.Add(new KeyValuePair<string, string>("PTR", "Part Time - Regular"));
                _empgroups.Add(new KeyValuePair<string, string>("PTT", "Part Time - Temporary"));
                cbo_employee_group.DataSource = _empgroups;
                cbo_employee_group.ValueMember = "Key";
                cbo_employee_group.DisplayMember = "Value";
                cbo_employee_group.SelectedIndex = -1;

                //defaults
                txtEmpNo.Text = Utils.NextSeries(de.LastEmployeeNo());

                decimal relief;
                if (!decimal.TryParse(de.SettingLookup("PRELIEF"), out relief))
                {
                    Utils.ShowError(new Exception("Unable to retrieve PRELIEF setting"));
                    relief = 0;
                }
                txtPersonalRelief.Text = relief.ToString();

                //payment mode
                var paymentmodes = new BindingList<KeyValuePair<string, string>>();
                paymentmodes.Add(new KeyValuePair<string, string>("M", "MPESA"));
                paymentmodes.Add(new KeyValuePair<string, string>("B", "BANKACCOUNT"));
                paymentmodes.Add(new KeyValuePair<string, string>("C", "CASH"));
                cbo_mode_of_payment.DataSource = paymentmodes;
                cbo_mode_of_payment.ValueMember = "Key";
                cbo_mode_of_payment.DisplayMember = "Value";
                cbo_mode_of_payment.SelectedIndex = -1;

                //Basic Pay comutation lookup
                var bpay = new BindingList<KeyValuePair<string, string>>();
                bpay.Add(new KeyValuePair<string, string>("M", "Monthly"));
                bpay.Add(new KeyValuePair<string, string>("H", "Hourly"));
                bpay.Add(new KeyValuePair<string, string>("X", "Mixed(Monthly + Hourly)"));
                cbo_basic_computation.DataSource = bpay;
                cbo_basic_computation.ValueMember = "Key";
                cbo_basic_computation.DisplayMember = "Value";
                cbo_basic_computation.SelectedIndex = -1;

                var _payrolltypesquery = from pt in db.PayrollTypes
                                         select pt;
                List<PayrollType> _payrolltypes = _payrolltypesquery.ToList();
                cbo_emp_payroll.DataSource = _payrolltypes;
                cbo_emp_payroll.DisplayMember = "Description";
                cbo_emp_payroll.ValueMember = "Description";
                cbo_emp_payroll.SelectedIndex = -1;

                pbPhoto.SizeMode = PictureBoxSizeMode.StretchImage;

                var _departmentsquery = from dp in rep.GetNonDeletedDepartments()
                                        select dp;
                List<Department> _Departments = _departmentsquery.ToList();
                cbo_department.DataSource = _Departments;
                cbo_department.DisplayMember = "Description";
                cbo_department.ValueMember = "Id";
                cbo_department.SelectedIndex = -1;

                cbo_employer.DataSource = rep.GetAllActiveEmployers();
                cbo_employer.DisplayMember = "Name";
                cbo_employer.ValueMember = "Id";

                pbPhoto.Visible = true;

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

                int minage = int.Parse(rep.SettingLookup("MINAGE"));
                dtpDOB.Value = DateTime.Today.AddYears(minage * -1);

                string _current_emp_no = get_next_employee_no();
                string _next_emp_no = Utils.NextSeries(_current_emp_no);
                txtEmpNo.Text = _next_emp_no;

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished AddEmployee load", TAG));

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
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

        string get_next_employee_no()
        {
            try
            {
                Employer _emp = (Employer)cbo_employer.SelectedItem;

                var _EmpNo = (from bk in rep.GetAllActiveEmployees()
                              where bk.IsActive == true
                              where bk.EmployerId.Equals(_emp.Id)
                              where bk.IsDeleted == false
                              orderby bk.Id descending
                              select bk.EmpNo).FirstOrDefault();
                return _EmpNo;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                return null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider.Clear();
            if (IsEmployeeValid())
            {
                try
                {
                    DAL.Employee employee = new DAL.Employee();
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
                    if (cbo_marital_status.SelectedIndex != -1)
                    {
                        employee.MaritalStatus = cbo_marital_status.SelectedValue.ToString();
                    }
                    if (cbo_gender.SelectedIndex != -1)
                    {
                        employee.Gender = cbo_gender.SelectedValue.ToString();
                    }
                    if (pbPhoto.ImageLocation != null)
                    {
                        employee.Photo = pbPhoto.ImageLocation.ToString().Trim();
                    }
                    employee.DoE = dtpDOE.Value;
                    if (cbo_basic_computation.SelectedIndex != -1)
                    {
                        employee.BasicComputation = cbo_basic_computation.SelectedValue.ToString();
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
                    if (cbo_department.SelectedIndex != -1)
                    {
                        employee.DepartmentId = int.Parse(cbo_department.SelectedValue.ToString());
                    }
                    if (cbo_employer.SelectedIndex != -1)
                    {
                        employee.EmployerId = int.Parse(cbo_employer.SelectedValue.ToString());
                    }
                    if (!string.IsNullOrEmpty(txtEmployeePaymentPoint.Text))
                    {
                        employee.PayPoint = txtEmployeePaymentPoint.Text.Trim();
                    }
                    if (cbo_employee_group.SelectedIndex != -1)
                    {
                        employee.EmpGroup = cbo_employee_group.SelectedValue.ToString();
                    }
                    if (cbo_emp_payroll.SelectedIndex != -1)
                    {
                        employee.EmpPayroll = cbo_emp_payroll.SelectedValue.ToString();
                    }
                    if (!string.IsNullOrEmpty(txtPrevEmployer.Text))
                    {
                        employee.PrevEmployer = txtPrevEmployer.Text.Trim();
                    }
                    employee.DateLeft = dtpDateLeft.Value;
                    if (cbo_mode_of_payment.SelectedIndex != -1)
                    {
                        employee.PaymentMode = cbo_mode_of_payment.SelectedValue.ToString();
                    }
                    switch (cbo_mode_of_payment.SelectedValue.ToString())
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
                                employee.BankCode = txtBankSortCode.Text.ToString();
                            }
                            else
                            {
                                employee.BankAccount = txtAccount.Text.Trim();
                                employee.BankCode = txtBankSortCode.Text.ToString();
                            }
                            break;
                    }
                    employee.IsActive = true; //on entry, all employees will be active. a System Administrator can change this
                    employee.IsDeleted = false;
                    employee.CreatedBy = _User;
                    employee.CreatedOn = DateTime.Now;
                    employee.SystemId = "ws";

                    DAL.Employee returnedEmployee = rep.CreateEmployee(employee);

                    if (returnedEmployee != null)
                    {
                        //add defualt employee transactions
                        de.AddDefaultEmpTransactions(returnedEmployee.Id, returnedEmployee.EmpNo, returnedEmployee.BasicPay ?? 0, _User);

                        EmployeesForm f = (EmployeesForm)this.Owner;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsEmployeeValid()
        {
            bool no_error = true;

            if (string.IsNullOrEmpty(txtEmpNo.Text))
            {
                errorProvider.SetError(txtEmpNo, "Employee Number cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtSurname.Text))
            {
                errorProvider.SetError(txtSurname, "Surname cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtOtherNames.Text))
            {
                errorProvider.SetError(txtOtherNames, "Other Names cannot be null!");
                no_error = false;
            }
            int minAge = int.Parse(de.SettingLookup("MINAGE"));
            DateTime minDoB = DateTime.Today.AddYears(-minAge);

            if (dtpDOB.Value > minDoB)
            {
                errorProvider.SetError(dtpDOB, "Employee Must be 18 Years and Above!");
                no_error = false;
            }
            if (cbo_marital_status.SelectedIndex == -1)
            {
                errorProvider.SetError(cbo_marital_status, "Select Marital Status!");
                no_error = false;
            }
            if (cbo_gender.SelectedIndex == -1)
            {
                errorProvider.SetError(cbo_gender, "Select Gender!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Email cannot be null!");
                no_error = false;
            }
            string emailregex = de.SettingLookup("EMAILREGEX");
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(emailregex, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            if (!rEMail.IsMatch(txtEmail.Text))
            {
                errorProvider.SetError(txtEmail, "Please provide a valid Email e.g username@domain.com");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtphone_no.Text))
            {
                errorProvider.SetError(txtphone_no, "Phone No cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtIDNo.Text))
            {
                errorProvider.SetError(txtIDNo, "ID No cannot be null!");
                no_error = false;
            }
            if (cbo_basic_computation.SelectedIndex == -1)
            {
                errorProvider.SetError(cbo_basic_computation, "Select Basic Computation!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtBasicPay.Text))
            {
                errorProvider.SetError(txtBasicPay, "Basic Pay cannot be null!");
                no_error = false;
            }
            decimal basic;
            if (!decimal.TryParse(txtBasicPay.Text, out basic))
            {
                errorProvider.SetError(txtBasicPay, "Basic Pay must be a decimal");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtNSSF.Text))
            {
                errorProvider.SetError(txtNSSF, "NSSF No cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtNHIF.Text))
            {
                errorProvider.SetError(txtNHIF, "NHIF No cannot be null!");
                no_error = false;
            }            
            if (string.IsNullOrEmpty(txtPIN.Text))
            {
                errorProvider.SetError(txtPIN, "PIN No cannot be null!");
                no_error = false;
            }
            if (cbo_department.SelectedIndex == -1)
            {
                errorProvider.SetError(cbo_department, "Select Department!");
                no_error = false;
            }
            if (cbo_employer.SelectedIndex == -1)
            {
                errorProvider.SetError(cbo_employer, "Select Employer!");
                no_error = false;
            }
            if (cbo_mode_of_payment.SelectedIndex == -1)
            {
                errorProvider.SetError(cbo_mode_of_payment, "Select Mode of Payment!");
                no_error = false;
            }
            if ("B".Equals(cbo_mode_of_payment.SelectedValue) && string.IsNullOrEmpty(txtBankSortCode.Text))
            {
                errorProvider.SetError(txtBankSortCode, "Bank Sort Code cannot be null!");
                no_error = false;
            }
            if ("B".Equals(cbo_mode_of_payment.SelectedValue) && !string.IsNullOrEmpty(txtBankSortCode.Text))
            {
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
            }
            if ("B".Equals(cbo_mode_of_payment.SelectedValue) && string.IsNullOrEmpty(txtAccount.Text))
            {
                errorProvider.SetError(txtAccount, "Bank Account cannot be null!");
                no_error = false;
            }
            if ("M".Equals(cbo_mode_of_payment.SelectedValue) && string.IsNullOrEmpty(txtTelephoneNo.Text))
            {
                errorProvider.SetError(txtTelephoneNo, "Phone Number cannot be null!");
                no_error = false;
            }
            return no_error;
        }

        private void btnNextSeries_Click(object sender, EventArgs e)
        {
            txtEmpNo.Text = Utils.NextSeries(txtEmpNo.Text.Trim());
        }

        private void btnUploadEmployeePhoto_Click(object sender, EventArgs e)
        {

            openFileDialog.Title = "select an Image File";
            openFileDialog.Filter = "Image File (*.jpg)|*.jpg|Image File (*.gif)|*.gif|Image File (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog.ShowDialog();
            string strFileName = openFileDialog.FileName;
            try
            {
                UploadEmployeePhoto(strFileName, _User);
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Upload completed successfully", TAG));
                MessageBox.Show("Upload completed successfully", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
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

        private void cboModeofPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_mode_of_payment.SelectedIndex != -1)
            {
                try
                {
                    switch (cbo_mode_of_payment.SelectedValue.ToString())
                    {
                        case "M":
                            txtChequeNo.Visible = false;
                            lblpaymentmode.Text = "Enter phone Number*";
                            lblpaymentmode.Visible = true;
                            txtTelephoneNo.Visible = true;
                            gbBank.Visible = false;
                            txtBankSortCode.Visible = false;
                            txtAccount.Visible = false;
                            btnSearchBank.Visible = false;
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

        private void cbBasicComputation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbo_basic_computation.SelectedIndex != -1)
            {
                try
                {
                    switch (cbo_basic_computation.SelectedValue.ToString())
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
                    switch (cbo_basic_computation.SelectedValue.ToString())
                    {
                        case "H":
                            cbo_employee_group.SelectedValue = "PTR";
                            cbo_emp_payroll.SelectedValue = "Temporary";
                            break;
                        case "M":
                            cbo_employee_group.SelectedValue = "FTR";
                            cbo_emp_payroll.SelectedValue = "End-Month";
                            break;
                        case "X":
                            cbo_employee_group.SelectedValue = "FTT";
                            cbo_emp_payroll.SelectedValue = "End-Month";
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
            if (cbo_employee_group.SelectedIndex != -1)
            {
                try
                {
                    switch (cbo_employee_group.SelectedValue.ToString())
                    {
                        case "PTR":
                            cbo_emp_payroll.SelectedValue = "Temporary";
                            break;
                        case "PTT":
                            cbo_emp_payroll.SelectedValue = "Temporary";
                            break;
                        case "FTR":
                            cbo_emp_payroll.SelectedValue = "End-Month";
                            break;
                        case "FTT":
                            cbo_emp_payroll.SelectedValue = "End-Month";
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
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
        private void cboEmpPayroll_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbo_emp_payroll.SelectedIndex == -1)
                {
                    txtPayrollTypeRemarks.Visible = false;
                    txtPayrollTypeRemarks.Text = string.Empty;
                }
                if (cbo_emp_payroll.SelectedIndex != -1)
                {
                    PayrollType _payrolltype = (PayrollType)cbo_emp_payroll.SelectedItem;

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

        private void cbo_employer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                get_last_saved_employee_code_from_db();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG)); 
            }
        }

        void get_last_saved_employee_code_from_db()
        {
            try
            {
                if (cbo_employer.SelectedItem != null)
                {
                    Employer _emp = (Employer)cbo_employer.SelectedItem;
                    string _empcode = string.Empty;

                    if (!string.IsNullOrEmpty(_emp.Code))
                    {
                        var _last_saved_employee_code_for_employer = (from emp in rep.GetAllActiveEmployees()
                                                                      where emp.EmployerId.Equals(_emp.Id)
                                                                      orderby emp.Id descending
                                                                      select emp.EmpNo).FirstOrDefault();

                        if (_last_saved_employee_code_for_employer != null)
                        {
                            _empcode = _last_saved_employee_code_for_employer;
                        }
                        else
                        {
                            _empcode = _emp.Code;
                        }

                        string _next_code = Utils.NextSeries(_empcode);

                        txtEmpNo.Text = _next_code;
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG)); 
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





    }
}