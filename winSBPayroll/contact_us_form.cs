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

        public contact_us_form(string user, string Conn)
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

                errorProvider1.Clear();

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
                        Utils.ShowError(new Exception("Error sending your message." + Environment.NewLine + "Check your internet connection."));
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }
        private bool is_record_valid()
        {
            bool no_error = true;

            if (string.IsNullOrEmpty(txtname.Text))
            {
                errorProvider1.SetError(txtname, "Name cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtemail.Text))
            {
                errorProvider1.SetError(txtemail, "Email cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtmessage.Text))
            {
                errorProvider1.SetError(txtmessage, "Message cannot be null!");
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
