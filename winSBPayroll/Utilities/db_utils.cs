using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; 
using console_dto;
using console_commonlib; 

namespace winSBPayroll
{
    public sealed class db_utils
    {
        // Because the _instance member is made private, the only way to get the single
        // instance is via the static Instance property below. This can also be similarly
        // achieved with a GetInstance() method instead of the property.
        private static db_utils singleInstance;

        public static db_utils getInstance(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            // The first call will create the one and only instance.
            if (singleInstance == null)
                singleInstance = new db_utils(notificationmessageEventname);
            // Every call afterwards will return the single instance created above.
            return singleInstance;
        }

        public static db_utils getInstance()
        {
            // The first call will create the one and only instance.
            if (singleInstance == null)
                singleInstance = new db_utils();
            // Every call afterwards will return the single instance created above.
            return singleInstance;
        }

        public db_utils()
        {
            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new
UnhandledExceptionEventHandler(UnhandledException);
            System.Windows.Forms.Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished db_utils initialization", TAG));

        }

        public string TAG;
        //Event declaration:
        //event for publishing messages to output
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        //list to hold messages
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();

        public db_utils(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.Write_To_Log_File_web(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.Write_To_Log_File_web(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
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

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void log_messages(string message, string TAG)
        {
            notificationdto _notificationdto = new notificationdto();
            DateTime currentDate = DateTime.Now;
            String dateTimenow = currentDate.ToString("dd-MM-yyyy HH:mm:ss tt");

            String _logtext = Environment.NewLine + "[ " + dateTimenow + " ]   " + message;

            _notificationdto._notification_message = _logtext;
            _notificationdto._created_datetime = dateTimenow;
            _notificationdto.TAG = TAG;

            create_log_in_database(_notificationdto);
        }

        private void create_log_in_database(notificationdto _notificationdto)
        {
            try
            {
                log_dto dto = new log_dto();
                dto.message = _notificationdto._notification_message;
                dto.timestamp = _notificationdto._created_datetime;
                dto.tag = _notificationdto.TAG;

                //save data in database.
                List<console_dto.responsedto> _lstresponse = new List<console_dto.responsedto>();
                _lstresponse = console_business.businesslayerapisingleton.getInstance(_notificationmessageEventname).create_log_in_database(dto);
                 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }



    }
}
