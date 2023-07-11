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
    public partial class transactions_form : Form
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

        public transactions_form(string user, string Conn)
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

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished transactions_form initialization", TAG));

        }

        private void transactions_form_Load(object sender, EventArgs e)
        {

            //initialize current running time timer
            elapsed_timer.Interval = 1000;
            elapsed_timer.Elapsed += elapsed_timer_Elapsed; // Uses an event instead of a delegate
            elapsed_timer.Start(); // Start the timer

            //app version
            var _buid_version = Application.ProductVersion;
            groupBox1.Text = "build version - " + _buid_version;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished transactions_form load", TAG));
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

                Log.WriteToErrorLogFile(new Exception(args.message));


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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
