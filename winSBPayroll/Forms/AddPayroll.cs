using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;
using System.Linq;
using System.Threading;

namespace winSBPayroll.Forms
{

    public partial class AddPayroll : Form
    {

        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string user;
        public string TAG;
        //Event declaration:
        //event for publishing messages to output
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public AddPayroll(string _user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            TAG = this.GetType().Name;

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname = notificationmessageEventname;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("Conn");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            user = _user;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished AddPayroll initialization", TAG));

        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.Write_To_Log_File_temp_dir(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.Write_To_Log_File_temp_dir(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void AddPayroll_Load(object sender, EventArgs e)
        {
            try
            {
                txtRunBy.Text = user;
                txtDateRun.Text = DateTime.Today.ToShortDateString();

                cbEmployer.ValueMember = "Id";
                cbEmployer.DisplayMember = "Name";
                cbEmployer.DataSource = de.GetEmployers();

                var months = new BindingList<KeyValuePair<int, string>>();
                months.Add(new KeyValuePair<int, string>(1, "January"));
                months.Add(new KeyValuePair<int, string>(2, "February"));
                months.Add(new KeyValuePair<int, string>(3, "March"));
                months.Add(new KeyValuePair<int, string>(4, "April"));
                months.Add(new KeyValuePair<int, string>(5, "May"));
                months.Add(new KeyValuePair<int, string>(6, "June"));
                months.Add(new KeyValuePair<int, string>(7, "July"));
                months.Add(new KeyValuePair<int, string>(8, "August"));
                months.Add(new KeyValuePair<int, string>(9, "September"));
                months.Add(new KeyValuePair<int, string>(10, "October"));
                months.Add(new KeyValuePair<int, string>(11, "November"));
                months.Add(new KeyValuePair<int, string>(12, "December"));

                cboPeriod.ValueMember = "Key";
                cboPeriod.DisplayMember = "Value";
                cboPeriod.DataSource = months;
                cboPeriod.SelectedValue = DateTime.Today.Month;

                txtYear.Text = DateTime.Today.Year.ToString();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished AddPayroll load", TAG));

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);

            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isvalid())
            {
                try
                {
                    Payroll payroll = new Payroll();
                    payroll.Period = int.Parse(cboPeriod.SelectedValue.ToString());
                    payroll.Year = int.Parse(txtYear.Text.Trim());
                    payroll.EmployerId = int.Parse(cbEmployer.SelectedValue.ToString());
                    payroll.DateRun = DateTime.Parse(txtDateRun.Text.Trim());
                    payroll.RunBy = user;
                    payroll.Approved = false;
                    payroll.ApprovedBy = user;
                    payroll.IsOpen = true;
                    payroll.Processed = false;
                    payroll.ProcessedBy = user;

                    if (rep.GetPayrolls().Any(i => i.Period == payroll.Period && i.Year == payroll.Year && i.EmployerId == payroll.EmployerId))
                    {
                        MessageBox.Show("Payroll for year [ " + payroll.Year + " ] period [ " + payroll.Period + " ] employer [ " + cbEmployer.Text + " ]  Exists!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!rep.GetPayrolls().Any(i => i.Period == payroll.Period && i.Year == payroll.Year && i.EmployerId == payroll.EmployerId))
                    {
                        rep.AddPayroll_for_employer(payroll);

                        PayrollsForm f = (PayrollsForm)this.Owner;
                        f.refresh_years();
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
        }
        private bool isvalid()
        {
            bool noerror = true;
            if (cboPeriod.SelectedIndex == -1)
            {
                errorProvider.SetError(cboPeriod, "Select Period!");
                noerror = false;
            }
            if (string.IsNullOrEmpty(txtYear.Text))
            {
                errorProvider.SetError(txtYear, "Year cannot be null!");
                noerror = false;
            }
            int yr;
            if (!int.TryParse(txtYear.Text.Trim(), out yr))
            {
                errorProvider.SetError(txtYear, "Year must be an Integer!");
                noerror = false;
            }
            if (cbEmployer.SelectedIndex == -1)
            {
                errorProvider.SetError(cbEmployer, "Select Employer!");
                noerror = false;
            }
            //if (string.IsNullOrEmpty(txtDateRun.Text))
            //{ 
            //    errorProvider1.SetError(txtDateRun, "Date Run cannot be null!");
            //    noerror = false;
            //}
            //DateTime daterun = DateTime.Parse(txtDateRun.Text);
            //DateTime datenow = DateTime.Today;
            //if (daterun < datenow)
            //{ 
            //    errorProvider1.SetError(txtDateRun, "Date Run must be greater than Today!");
            //    noerror = false;
            //}
            if (string.IsNullOrEmpty(txtRunBy.Text))
            {
                errorProvider.SetError(txtRunBy, "Run By cannot be null!");
                noerror = false;
            }
            return noerror;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }





    }
}