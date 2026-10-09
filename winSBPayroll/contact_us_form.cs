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

namespace winSBPayroll
{
    public partial class contact_us_form : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;
        public string TAG;
        string msg = string.Empty;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public contact_us_form(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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
            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished contact_us_form initialization", TAG));

        }

        public contact_us_form(string user, string Conn, string _msg, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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
            msg = _msg;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname = notificationmessageEventname;

            txtmessage.Text = msg;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished contact_us_form initialization", TAG));

        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
        }

        private void contact_us_form_Load(object sender, EventArgs e)
        {

        }

        private void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                string name = string.Empty;
                string email = string.Empty;
                string message = string.Empty;

                errorProvider.Clear();

                if (is_record_valid())
                {
                    if (!string.IsNullOrEmpty(txtname.Text))
                    {
                        name = txtname.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtemail.Text))
                    {
                        email = txtemail.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtmessage.Text))
                    {
                        message = txtmessage.Text.Trim();
                    }

                    StringBuilder template = new StringBuilder();

                    template.Append("Name [ " + name + " ] ");
                    template.Append("Email [ " + email + " ] ");
                    template.Append("Message [ " + message + " ] ");
                    template.Append("MACAddress [ " + Utils.GetMACAddress() + " ] ");

                    if (Utils.IsConnectedToInternet())
                    {
                        Utils.SendEmail(template.ToString());
                    }
                    else
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Error sending your message." + Environment.NewLine + "Check your internet connection.", TAG));

                        Utils.ShowError(new Exception("Error sending your message." + Environment.NewLine + "Check your internet connection."));
                    }
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }
        private bool is_record_valid()
        {
            bool no_error = true;

            if (string.IsNullOrEmpty(txtname.Text))
            {
                errorProvider.SetError(txtname, "Name cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtemail.Text))
            {
                errorProvider.SetError(txtemail, "Email cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtmessage.Text))
            {
                errorProvider.SetError(txtmessage, "Message cannot be null!");
                no_error = false;
            }
            return no_error;
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
