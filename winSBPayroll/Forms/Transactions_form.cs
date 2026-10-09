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
using System.Threading;

namespace winSBPayroll.Forms
{
    public partial class Transactions_form : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;

        DateTime startDate = DateTime.Now;
        public string TAG;
        // Timers namespace rather than Threading
        System.Timers.Timer elapsed_timer = new System.Timers.Timer(); // Doesn't require any args
        private int _TimeCounter = 0;
        DateTime _startDate = DateTime.Now;
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        private event EventHandler<notificationmessageEventArgs> _parent_notificationmessageEventname;

        public Transactions_form(string user, string Conn, EventHandler<notificationmessageEventArgs> parent_notificationmessageEventname)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _user = user;

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _parent_notificationmessageEventname = parent_notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished Transactions_form initialization", TAG));

        }

        private void transactions_form_Load(object sender, EventArgs e)
        {
            //initialize current running time timer
            elapsed_timer.Interval = 1000;
            elapsed_timer.Elapsed += elapsed_timer_Elapsed; // Uses an event instead of a delegate
            elapsed_timer.Start(); // Start the timer

            //app version
            var dll_ver = System.Reflection.Assembly.GetAssembly(typeof(Repository)).GetName().Version.ToString();
            groupBox1.Text = "Build version - " + dll_ver;

            update_dashboard();

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished Transactions_form load", TAG));
        }

        void update_dashboard()
        {
            try
            {
                int employee_transactions = db.EmployeeTransactions.Count();
                btn_employee_transactions.Text = "Employee Transactions [ " + employee_transactions + " ]";
                btnemployee_transactions.Text = "Employee Transactions [ " + employee_transactions + " ]";
                int payslipdet_temp = db.PayslipDet_Temp.Count();
                btn_payslip_details_temp.Text = "Payslipdet Temp [ " + payslipdet_temp + " ]";
                payslipDetailsTempToolStripMenuItem.Text = "Payslipdet Temp [ " + payslipdet_temp + " ]";
                int payslipdets = db.PayslipDets.Count();
                btn_payslip_details.Text = "Payslip Details [ " + payslipdets + " ]";
                payslipDetailsToolStripMenuItem.Text = "Payslip Details [ " + payslipdets + " ]";
                int payslip_master_temp = db.PayslipMaster_Temp.Count();
                btn_payslip_master_temp.Text = "Payslip Master Temp [ " + payslip_master_temp + " ]";
                payslipMasterTempToolStripMenuItem.Text = "Payslip Master Temp [ " + payslip_master_temp + " ]";
                int payslip_master = db.PayslipMasters.Count();
                btn_payslip_master.Text = "Payslip Master [ " + payslip_master + " ]";
                payslipMasterToolStripMenuItem.Text = "Payslip Master [ " + payslip_master + " ]";
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.LogEventViewer(ex);
            }
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            this._notificationmessageEventname.Invoke(sender, new notificationmessageEventArgs(ex.Message, TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            this._notificationmessageEventname.Invoke(sender, new notificationmessageEventArgs(ex.Message, TAG));
        }

        //Event handler declaration:
        public void notificationmessageHandler(object sender, notificationmessageEventArgs args)
        {
            try
            {
                /* Handler logic */

                //Invoke(new Action(() =>
                //{

                notificationdto _notificationdto = new notificationdto();

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + args.message;

                _notificationdto._notification_message = _logtext;
                _notificationdto._created_datetime = dateTimenow;
                _notificationdto.TAG = args.TAG;

                this._parent_notificationmessageEventname.Invoke(sender, new notificationmessageEventArgs(args.message, TAG));

                _lstnotificationdto.Add(_notificationdto);

                var _lstmsgdto = from msgdto in _lstnotificationdto
                                 orderby msgdto._created_datetime descending
                                 select msgdto._notification_message;

                String[] _logflippedlines = _lstmsgdto.ToArray();

                txtlog.Lines = _logflippedlines;
                txtlog.ScrollToCaret();

                //}));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void elapsed_timer_Elapsed(object sender, EventArgs e)
        {
            try
            {
                _TimeCounter++;
                DateTime nowDate = DateTime.Now;
                TimeSpan t = nowDate - _startDate;
                lbltimelapsed.Text = string.Format("{0} : {1} : {2} : {3}", t.Days, t.Hours, t.Minutes, t.Seconds);

                DateTime currentDate = DateTime.Now;
                String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

                lblrunningtime.Text = dateTimenow;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btnemployee_transactions_Click(object sender, EventArgs e)
        {
            EmployeeTransactionsForm EmployeeTransactionsForm = new EmployeeTransactionsForm(_user, connection, _notificationmessageEventname) { Owner = this };
            EmployeeTransactionsForm.Show();
        }

        private void payslipDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            payslip_details_form payslip_details_form = new payslip_details_form(_user, connection, _notificationmessageEventname) { Owner = this };
            payslip_details_form.Show();
        }

        private void payslipMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            payslip_master_form payslip_master_form = new payslip_master_form(_user, connection, _notificationmessageEventname) { Owner = this };
            payslip_master_form.Show();
        }

        private void payslipDetailsTempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            payslip_details_temp_form payslip_details_temp_form = new payslip_details_temp_form(_user, connection, _notificationmessageEventname) { Owner = this };
            payslip_details_temp_form.Show();
        }

        private void payslipMasterTempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            payslip_master_temp_form payslip_master_temp_form = new payslip_master_temp_form(_user, connection, _notificationmessageEventname) { Owner = this };
            payslip_master_temp_form.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_employee_transactions_Click(object sender, EventArgs e)
        {
            btnemployee_transactions_Click(sender, e);
        }

        private void btn_payslip_details_temp_Click(object sender, EventArgs e)
        {
            payslipDetailsTempToolStripMenuItem_Click(sender, e);
        }

        private void btn_payslip_details_Click(object sender, EventArgs e)
        {
            payslipDetailsToolStripMenuItem_Click(sender, e);
        }

        private void btn_payslip_master_temp_Click(object sender, EventArgs e)
        {
            payslipMasterTempToolStripMenuItem_Click(sender, e);
        }

        private void btn_payslip_master_Click(object sender, EventArgs e)
        {
            payslipMasterToolStripMenuItem_Click(sender, e);
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            exitToolStripMenuItem_Click(sender, e);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }







    }
}
