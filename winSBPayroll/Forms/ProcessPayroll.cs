using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.Payroll;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class ProcessPayroll : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _User;
        public string TAG;
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        //Event declaration:
        //event for publishing messages to output
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname_from_parent;

        public ProcessPayroll(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _notificationmessageEventname_from_parent = notificationmessageEventname_from_parent;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _User = user;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished ProcessPayroll initialization", TAG));

        }

        private void ProcessPayroll_Load(object sender, EventArgs e)
        {
            try
            {
                var _employersquery = from ep in db.Employers
                                      where ep.IsActive == true
                                      where ep.IsDeleted == false
                                      select ep;

                List<DAL.Employer> _employers = _employersquery.ToList();

                DataGridViewComboBoxColumn colCboxEmployer = new DataGridViewComboBoxColumn();
                colCboxEmployer.HeaderText = "Employers";
                colCboxEmployer.Name = "colCboxEmployer";
                colCboxEmployer.DataSource = _employers;
                // The display member is the name column in the column datasource  
                colCboxEmployer.DisplayMember = "Name";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxEmployer.DataPropertyName = "EmployerId";
                // The value member is the primary key of the parent table  
                colCboxEmployer.ValueMember = "Id";
                colCboxEmployer.MaxDropDownItems = 10;
                colCboxEmployer.Width = 100;
                colCboxEmployer.DisplayIndex = 4;
                colCboxEmployer.MinimumWidth = 5;
                colCboxEmployer.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colCboxEmployer.FlatStyle = FlatStyle.Flat;
                colCboxEmployer.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployer.ReadOnly = true;

                if (!this.dataGridViewPayrolls.Columns.Contains("colCboxEmployer"))
                {
                    dataGridViewPayrolls.Columns.Add(colCboxEmployer);
                }

                dataGridViewPayrolls.AutoGenerateColumns = false;
                this.dataGridViewPayrolls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                //load all payrolls not closed
                //var _payrolls_query = db.Payrolls.Where(p => p.IsOpen == true).OrderByDescending(i => i.DateRun).ThenByDescending(i => i.Year).ThenByDescending(i => i.Period).ThenByDescending(i => i.Processed).ToList();
                //List<DAL.Payroll> openPayrolls = de.GetPayrolls(PayrollState.Open);
                var _payrolls_query = db.Payrolls.Where(p => p.IsOpen == true).OrderByDescending(i => i.Processed).ThenByDescending(i => i.DateRun).ToList();
                List<DAL.Payroll> openPayrolls = _payrolls_query.ToList();

                bindingSourcePayrolls.DataSource = openPayrolls;
                dataGridViewPayrolls.DataSource = bindingSourcePayrolls;
                groupBox3.Text = bindingSourcePayrolls.Count.ToString();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished ProcessPayroll load", TAG));

            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        }

        private void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
        }

        //Event handler declaration:
        private void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            try
            {
                /* Handler logic */


                notificationdto _notificationdto = new notificationdto();

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                _lstnotificationdto.Add(_notificationdto);
                Console.WriteLine(args.message);
                _notificationmessageEventname_from_parent.Invoke(this, new notificationmessageEventArgs(args.message, TAG));

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                if (_logflippedlines.Length > 5000)
                {
                    _lstnotificationdto.Clear();
                }

                txtlog.Lines = _logflippedlines;
                txtlog.ScrollToCaret();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void btnProcessPayroll_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayrolls.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.Payroll pay = (DAL.Payroll)bindingSourcePayrolls.Current;

                    // see if the payslip_master_temp has records and they are for this payroll
                    //if not, decline processing
                    //set up
                    int periodWc = 0;
                    int yearWc = 0;
                    int employeridWc = 0;

                    DAL.Employer _employer = rep.GetEmployer(pay.EmployerId);

                    if (_employer != null)
                    {

                    }
                    else
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Error retrieving Employer info" + Environment.NewLine + "Contact Administrator", TAG));
                        MessageBox.Show("Error retrieving Employer info" + Environment.NewLine + "Contact Administrator", Utils.APP_NAME);
                        return;
                    }

                    // if (!de.WorkingCopyNotClosed(ref periodWc, ref yearWc))
                    if (!de.Working_Copy_Not_Closed_for_employer(ref periodWc, ref yearWc, ref employeridWc))
                    {
                        bool simulateRun = false;

                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Processing Payroll for Period [{0}], Year [{1}], Employer [{2}]", pay.Period, pay.Year, _employer.Name), TAG));

                        ProcessPayrollNow(pay.Period, pay.Year, simulateRun, pay.EmployerId);

                        pay.DateRun = DateTime.Now.Date;
                        pay.RunBy = _User;

                        rep.UpdatePayroll_for_employer_DateRun(pay);
                    }
                    else if (periodWc == pay.Period && yearWc == pay.Year && employeridWc == pay.EmployerId)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Payroll already processed! \nDo you wish to overwrite?", "Confirm Overwrite", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            bool simulateRun = true;

                            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Re-Processing Payroll for Period [{0}], Year [{1}], Employer [{2}]", pay.Period, pay.Year, _employer.Name), TAG));

                            ProcessPayrollNow(pay.Period, pay.Year, simulateRun, pay.EmployerId);

                            pay.DateRun = DateTime.Now.Date;
                            pay.RunBy = _User;

                            rep.UpdatePayroll_for_employer_DateRun(pay);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must close the previously Processed payroll before proceeding", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                    Utils.ShowError(ex);
                }
                finally
                {
                    RefreshGrid();
                }
            }
        }
        private void ProcessPayrollNow(int period, int year, bool simulateRun, int EmployerId)
        {
            try
            {
                int count = rep.GetAllActiveEmployeesforEmployer(EmployerId).Count();

                progressbar.Minimum = 0;
                progressbar.Maximum = count;
                listBoxResults.Items.Clear();

                if (count == 0)
                {
                    string msg = string.Format("No Employees to Process for Employer [ {0} ]", rep.GetEmployer(EmployerId).Name);
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));
                    Utils.ShowError(new Exception(msg));
                    return;
                }
                else
                {
                    string msg = string.Format("Processing [ {0} ] Employees for Employer [ {1} ]", count, rep.GetEmployer(EmployerId).Name);
                    NotifyMessage("PAYROLL ENGINE RUNNING...", msg);
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(msg, TAG));
                }

                CPayroll cp = new CPayroll(_User, connection);
                //subscribe to the events
                cp.OnCompleteGeneratePayslip += new CPayroll.PayslipCompleteEventHandler(cp_OnCompleteGeneratePayslip);

                string sError = string.Empty;

                cp.RunPayroll(simulateRun, period, year, ref sError, EmployerId);

                rep.MarkPayroll_for_employer_AsProcessed(period, year, EmployerId);

                if (sError != "")
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(sError, TAG));
                    Log.WriteToErrorLogFile_and_EventViewer(new Exception(sError));
                };

                MessageBox.Show("Successfully run", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
            finally
            {
                RefreshGrid();
            }
        }
        private bool PayrollNotClosed(int period, int year)
        {
            bool ret = true;

            return ret;
        }
        //Handle the event
        private void cp_OnCompleteGeneratePayslip(object sender, PayslipCompleteEventArg e)
        {
            try
            {
                progressbar.Value = e.StatusValue;
                //NotifyMessage("PAYROLL ENGINE RUNNING...", e._Template + Environment.NewLine + e.ErrorMsg);
                listBoxResults.Items.Add(e.ErrorMsg);
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(e._Template + Environment.NewLine + e.ErrorMsg, TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        public bool NotifyMessage(string _Title, string _Text)
        {
            try
            {
                appNotifyIcon.Text = Utils.APP_NAME;
                appNotifyIcon.Icon = new Icon("Resources/Icons/Dollar.ico");
                appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                appNotifyIcon.BalloonTipTitle = _Title;
                appNotifyIcon.BalloonTipText = _Text;
                appNotifyIcon.Visible = true;
                appNotifyIcon.ShowBalloonTip(900000);

                return true;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
                return false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void RefreshGrid()
        {
            try
            {
                bindingSourcePayrolls.DataSource = null;
                dataGridViewPayrolls.DataSource = null;

                //load all payrolls not closed
                //List<DAL.Payroll> openPayrolls = de.GetPayrolls(PayrollState.Open);
                var _payrolls_query = db.Payrolls.Where(p => p.IsOpen == true).OrderByDescending(i => i.Processed).ToList();
                List<DAL.Payroll> openPayrolls = _payrolls_query.ToList();

                bindingSourcePayrolls.DataSource = openPayrolls;
                dataGridViewPayrolls.DataSource = bindingSourcePayrolls;
                groupBox3.Text = bindingSourcePayrolls.Count.ToString();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void btnClosePayroll_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayrolls.SelectedRows.Count != 0)
                {
                    DAL.Payroll pay = (DAL.Payroll)bindingSourcePayrolls.Current;
                    if (pay.Processed)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Close this Payroll?", "Confirm Close", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            // Todo close the payroll here
                            CPayroll cp = new CPayroll(_User, connection);
                            cp.ArchivePayroll(pay.Period, pay.Year, pay.EmployerId);

                            // Todo code for removing record from the payslipmaster
                            RefreshGrid();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You are not allowed to close a non Processed Payroll!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewPayrolls_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                throw e.Exception;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteToErrorLogFile(ex);
            }
        }









    }
}






