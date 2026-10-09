using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using System.IO;
using System.Threading;

namespace winSBPayroll.Forms
{
    public partial class load_log_file_form : Form
    {
        string user;
        public string TAG;
        //Event declaration:
        //event for publishing messages to output
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        FileSystemWatcher watcher;

        public load_log_file_form(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            TAG = this.GetType().Name;

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished load_log_file_form initialization", TAG));

        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.WriteToErrorLogFile_and_EventViewer(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }
        private void load_log_file_form_Load(object sender, EventArgs e)
        {
            try
            {
                watch();

                load_log_file();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished load_log_file_form load", TAG));

            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }

        private void watch()
        {
            // Create a new FileSystemWatcher and set its properties.
            watcher = new FileSystemWatcher();


            string base_directory = Utils.get_application_path();
            string log_path = Utils.build_file_path(base_directory, "Logs");

            string log_file_name = "error.txt";
            string inputPath = Utils.build_file_path(log_path, log_file_name);


            watcher.Path = log_path;
            /* Watch for changes in LastAccess and LastWrite times, and
            the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
            | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "*.txt*";
            // Add event handler.
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            NotifyMessage("log file", "detected log file changed.");
            copy_to_user_temp();
            load_log_file();
        }

        private void load_log_file()
        {
            try
            {
                string content = Utils.ReadLogFile();
                if (content != null)
                {
                    txtlog.Text = content;
                }
                txtlog.ScrollToCaret();
                txtlog.HideSelection = false;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            load_log_file();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
                appNotifyIcon.ShowBalloonTip(9000);

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private void copy_to_user_temp()
        {
            try
            {
                string base_directory = AppDomain.CurrentDomain.BaseDirectory;
                string reports_path = Path.Combine(base_directory, "Logs");

                string temp_path = Path.GetTempPath();

                DirectoryInfo reports_dir_info = new DirectoryInfo(reports_path);
                DirectoryInfo temp_dir_info = new DirectoryInfo(temp_path);

                var files = reports_dir_info.GetFiles();

                foreach (var report_file_info in files)
                {
                    var _temp_file = Path.Combine(temp_path, report_file_info.Name);

                    System.IO.File.Copy(report_file_info.FullName, _temp_file, true);

                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }


    }
}
