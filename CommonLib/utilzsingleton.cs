using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    /// <summary>
    /// Description of utilzsingleton.
    /// </summary>
    public sealed class utilzsingleton
    {
        // Because the _instance member is made private, the only way to get the single
        // instance is via the static Instance property below. This can also be similarly
        // achieved with a GetInstance() method instead of the property. 
        private static utilzsingleton singleInstance;

        public static utilzsingleton getInstance(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            // The first call will create the one and only instance.
            if (singleInstance == null)
                singleInstance = new utilzsingleton(notificationmessageEventname);
            // Every call afterwards will return the single instance created above.
            return singleInstance;
        }

        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        public string TAG;

        private utilzsingleton(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
        }

        private utilzsingleton()
        {

        }
		

        public string getappsettinggivenkey(string key = "", string defaultvalue = "")
        {
            try
            {

                string configvalue = "";

                configvalue = System.Configuration.ConfigurationManager.AppSettings[key];

                if (configvalue == null || String.IsNullOrEmpty(configvalue))
                {
                    return defaultvalue;
                }
                else
                {
                    return configvalue;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, this.TAG));
                return defaultvalue;
            }
        }

        public error_logger_dto build_error_dto_given_datatable(DataTable dt, int _index)
        {
            error_logger_dto _error_logger_dto = new error_logger_dto();
            _error_logger_dto.error_id = Convert.ToString(dt.Rows[_index][DBContract.error_entity_table.WEIGHT_ID]);
            _error_logger_dto.error_description = Convert.ToString(dt.Rows[_index][DBContract.error_entity_table.WEIGHT_WEIGHT]);
            _error_logger_dto.error_date = Convert.ToString(dt.Rows[_index][DBContract.error_entity_table.WEIGHT_DATE]);
            _error_logger_dto.error_category = Convert.ToString(dt.Rows[_index][DBContract.error_entity_table.WEIGHT_STATUS]);
            _error_logger_dto.created_date = Convert.ToString(dt.Rows[_index][DBContract.error_entity_table.CREATED_DATE]);
            _error_logger_dto.error_source = Convert.ToString(dt.Rows[_index][DBContract.error_entity_table.WEIGHT_APP]);

            return _error_logger_dto;
        }
		



    }
}
