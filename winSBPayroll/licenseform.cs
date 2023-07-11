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
using System.Security.Cryptography;
using System.Diagnostics;

namespace winSBPayroll
{
    public partial class licenseform : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;
        public string TAG;

        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        public licenseform(string user, string Conn)
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

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished licenseform initialization", TAG));

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

        private void licenseform_Load(object sender, EventArgs e)
        {
            //app version
            var _buid_version = app_assembly_info.AssemblyVersion;
            groupBox2.Text = "build version - " + _buid_version;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished licenseform load", TAG));
        }

        private void activateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //_notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("validating your license...", TAG));
                //_notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("licensing your app...", TAG));

                //DateTime currentDate = DateTime.Now;
                //String dateTimenow = currentDate.ToString("yyyy");
                //string macaddrress = Utils.GetMACAddress();

                //string product_identifier = (macaddrress + "-" + app_assembly_info.AssemblyProduct + "-" + app_assembly_info.AssemblyVersion + "-" + app_assembly_info.AssemblyCompany + "-" + app_assembly_info.AssemblyCopyright + "-" + dateTimenow).ToLower();

                //Debug.WriteLine(string.Format("Md5 license key : {0}", Utils.get_Md5_hash(product_identifier)));
                //Debug.WriteLine(string.Format("Md5 license key : {0}", Utils.format_license_key(Utils.get_Md5_hash(product_identifier))));
                //Debug.WriteLine(string.Format("sha512 license key : {0}", Utils.get_SHA512_hash(product_identifier)));
                //Debug.WriteLine(string.Format("sha512 license key : {0}", Utils.format_license_key(Utils.get_SHA512_hash(product_identifier))));

                //_notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Md5 license key : {0}", Utils.get_Md5_hash(product_identifier)), TAG));
                //_notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("Md5 license key : {0}", Utils.format_license_key(Utils.get_Md5_hash(product_identifier))), TAG));

                //_notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("sha512 license key : {0}", Utils.get_SHA512_hash(product_identifier)), TAG));
                //_notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(string.Format("sha512 license key : {0}", Utils.format_license_key(Utils.get_SHA512_hash(product_identifier))), TAG));

                contact_us_form cuf = new contact_us_form(_user, connection);
                cuf.Show();

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile_and_EventViewer(ex);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtlog_KeyDown(object sender, KeyEventArgs e)
        {
            //e.SuppressKeyPress = true;
            Console.WriteLine(e.KeyValue);
            Console.WriteLine(e.KeyData);
            Console.WriteLine(e.KeyCode);
        }

    }
}
