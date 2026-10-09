using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib
{
    public sealed class mssqlapisingleton
    {
        // Because the _instance member is made private, the only way to get the single
        // instance is via the static Instance property below. This can also be similarly
        // achieved with a GetInstance() method instead of the property.
        private static mssqlapisingleton singleInstance;

        public static mssqlapisingleton getInstance(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            // The first call will create the one and only instance.
            if (singleInstance == null)
                singleInstance = new mssqlapisingleton(notificationmessageEventname);
            // Every call afterwards will return the single instance created above.
            return singleInstance;
        }

        private string CONNECTION_STRING = @"Data Source=.\SQLEXPRESS;Database=SBSchoolDB;User Id=sa;Password=123456789";
        private const string db_name = "SBSchoolDB";//DBContract.DATABASE_NAME;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        private string TAG;

        private mssqlapisingleton(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;
            try
            {
                TAG = this.GetType().Name;
                setconnectionstring();
                createdatabaseonfirstload();
                createtablesonfirstload();
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
            }
        }

        private mssqlapisingleton()
        {

        }
        private void setconnectionstring()
        {
            try
            {
                mssqlconnectionstringdto _connectionstringdto = new mssqlconnectionstringdto();

                _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["mssql_datasource"];
                _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["mssql_database"];
                _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["mssql_userid"];
                _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["mssql_password"];
                _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["mssql_port"];

                CONNECTION_STRING = buildconnectionstringfromobject(_connectionstringdto);

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
            }
        }

        string buildconnectionstringfromobject(mssqlconnectionstringdto _connectionstringdto)
        {
            string CONNECTION_STRING = @"Data Source=" + _connectionstringdto.datasource + ";" +
            "Database=" + _connectionstringdto.database + ";" +
            "User Id=" + _connectionstringdto.userid + ";" +
            "Password=" + _connectionstringdto.password;
            return CONNECTION_STRING;
        }
        void createdatabaseonfirstload()
        {
            mssqlconnectionstringdto _connectionstringdto = new mssqlconnectionstringdto();

            _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["mssql_datasource"];
            _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["mssql_database"];
            _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["mssql_userid"];
            _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["mssql_password"];
            _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["mssql_port"];
            _connectionstringdto.new_database_name = System.Configuration.ConfigurationManager.AppSettings["mssql_database"];

            createdatabasegivenname(_connectionstringdto);
        }
        void createtablesonfirstload()
        {
            mssqlconnectionstringdto _connectionstringdto = new mssqlconnectionstringdto();

            _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["mssql_datasource"];
            _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["mssql_database"];
            _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["mssql_userid"];
            _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["mssql_password"];
            _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["mssql_port"];
            _connectionstringdto.new_database_name = System.Configuration.ConfigurationManager.AppSettings["mssql_database"];

            createtables(_connectionstringdto);
        }
        public responsedto createdatabasegivenname(mssqlconnectionstringdto _connectionstringdto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                _connectionstringdto.database = "master";

                string CONNECTION_STRING = buildconnectionstringfromobject(_connectionstringdto);

                string query = "CREATE DATABASE " + _connectionstringdto.new_database_name + ";";

                using (var con = new SqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        _responsedto.isresponseresultsuccessful = true;
                        _responsedto.responsesuccessmessage = "successfully created database [ " + _connectionstringdto.new_database_name + " ] in " + DBContract.mssql + ".";
                        _responsedto.responseresultobject = _connectionstringdto;
                        return _responsedto;
                    }
                }
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }
        public bool createdatabase()
        {
            try
            {
                string _default_database_name = utilzsingleton.getInstance(_notificationmessageEventname).getappsettinggivenkey("mssql_database", "july");
                string query = "CREATE DATABASE " + _default_database_name + ";";
                using (var con = new SqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return false;
            }
        }
        public responsedto createtables(mssqlconnectionstringdto _connectionstringdto)
        {
            responsedto _responsedto = new responsedto();
            responsedto _innerresponsedto = new responsedto();
            try
            {

                _connectionstringdto.database = _connectionstringdto.new_database_name;
                string CONNECTION_STRING = buildconnectionstringfromobject(_connectionstringdto);

                bool does_table_exist_in_db = checkiftableexists(CONNECTION_STRING, "tblweights");

                //Create the table 
                string SQL_CREATE_CROPS_TABLE = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + DBContract.error_entity_table.TABLE_NAME + "]') AND type in (N'U'))";
                SQL_CREATE_CROPS_TABLE += " BEGIN ";
                SQL_CREATE_CROPS_TABLE += " CREATE TABLE " + DBContract.error_entity_table.TABLE_NAME + " (" +
                      DBContract.error_entity_table.WEIGHT_ID + " INT IDENTITY(1,1) PRIMARY KEY NOT NULL, " +
                      DBContract.error_entity_table.WEIGHT_WEIGHT + " TEXT, " +
                      DBContract.error_entity_table.WEIGHT_DATE + " TEXT, " +
                      DBContract.error_entity_table.WEIGHT_STATUS + " TEXT, " +
                      DBContract.error_entity_table.CREATED_DATE + " TEXT, " +
                      DBContract.error_entity_table.WEIGHT_APP + " TEXT " +
                      " )";
                SQL_CREATE_CROPS_TABLE += "END";

                _innerresponsedto = createtable(SQL_CREATE_CROPS_TABLE, CONNECTION_STRING);
                if (_innerresponsedto.isresponseresultsuccessful)
                    _responsedto.responsesuccessmessage += _innerresponsedto.responsesuccessmessage;
                else
                    _responsedto.responseerrormessage += _innerresponsedto.responseerrormessage;

                string successmsg = "successfully created tables in database [ " + _connectionstringdto.database + " ] - server [ " + DBContract.mssql + " ].";
                int msg_length = successmsg.Length;
                msg_length = msg_length + 1;
                int stars_printed = 0;
                string str_stars = "";
                string str_newline = Environment.NewLine;

                while (stars_printed != msg_length)
                {
                    str_stars += "*";
                    ++stars_printed;
                }

                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += str_stars;
                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += successmsg;
                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += str_newline;
                _responsedto.responsesuccessmessage += str_stars;

                _responsedto.isresponseresultsuccessful = true;
                return _responsedto;

            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage += Environment.NewLine + ex.Message;
                return _responsedto;
            }
        }

        bool checkiftableexists(string CONNECTION_STRING, string table_name)
        {
            try
            {
                string query = "SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + table_name + "]') AND type in (N'U')";

                using (var con = new SqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    //open a new command
                    using (var cmd = new SqlCommand(query, con))
                    {
                        //execute the query  
                        int _rows_affected = cmd.ExecuteNonQuery();
                        Console.WriteLine("_rows_affected [ " + _rows_affected + " ]");

                        var da = new SqlDataAdapter(cmd);
                        var dt = new DataTable();
                        da.Fill(dt);
                        da.Dispose();

                        int _rows_count = dt.Rows.Count;
                        if (_rows_count > 0) return true;
                        else return false;
                    }
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return false;
            }
        }

        public responsedto createtable(string query, string CONNECTION_STRING)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                //setup the connection to the database
                using (var con = new SqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    //open a new command
                    using (var cmd = new SqlCommand(query, con))
                    {
                        //execute the query  
                        int _rows_affected = cmd.ExecuteNonQuery();
                        Console.WriteLine("_rows_affected [ " + _rows_affected + " ]");

                        _responsedto.isresponseresultsuccessful = true;
                        string[] _conn_arr = CONNECTION_STRING.Split(new char[] { ';' });
                        _conn_arr.SetValue("", _conn_arr.Length - 1);
                        _conn_arr.SetValue("", _conn_arr.Length - 2);
                        string _sanitized_conn_arr = _conn_arr[0] + ";" + _conn_arr[1];
                        _responsedto.responsesuccessmessage += "successfully executed query [ " + query + " ] against connection [ " + _sanitized_conn_arr + " ]." + Environment.NewLine;
                        return _responsedto;
                    }
                }
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                Exception _exception = ex.GetBaseException();
                _responsedto.responseerrormessage += Environment.NewLine + "error executing query [ " + query + " ].";
                _responsedto.responseerrormessage += Environment.NewLine + Environment.NewLine + _exception.Message + Environment.NewLine;
                return _responsedto;
            }
        }

        public responsedto checkmssqlconnectionstate()
        {
            responsedto _responsedto = new responsedto();
            try
            {
                mssqlconnectionstringdto _connectionstringdto = getmssqlconnectionstringdto();

                _responsedto = checkconnectionasadmin(_connectionstringdto);

                return _responsedto;
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }

        public mssqlconnectionstringdto getmssqlconnectionstringdto()
        {
            try
            {
                mssqlconnectionstringdto _connectionstringdto = new mssqlconnectionstringdto();

                _connectionstringdto.datasource = System.Configuration.ConfigurationManager.AppSettings["mssql_datasource"];
                _connectionstringdto.database = System.Configuration.ConfigurationManager.AppSettings["mssql_database"];
                _connectionstringdto.userid = System.Configuration.ConfigurationManager.AppSettings["mssql_userid"];
                _connectionstringdto.password = System.Configuration.ConfigurationManager.AppSettings["mssql_password"];
                _connectionstringdto.port = System.Configuration.ConfigurationManager.AppSettings["mssql_port"];

                return _connectionstringdto;

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return null;
            }
        }

        public responsedto checkconnectionasadmin(mssqlconnectionstringdto _connectionstringdto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string CONNECTION_STRING = @"Data Source=" + _connectionstringdto.datasource + ";" +
                "Database=" + _connectionstringdto.database + ";" +
                "User Id=" + _connectionstringdto.userid + ";" +
                "Password=" + _connectionstringdto.password;

                string query = DBContract.SELECT_ALL_QUERY;

                int count = getrecordscountgiventable(DBContract.error_entity_table.TABLE_NAME, CONNECTION_STRING);

                if (count != -1)
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "connection to mssql successfull. records count [ " + count + " ]";
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responseerrormessage = "connection to mssql failed.";
                    return _responsedto;
                }
            }
            catch (Exception ex)
            {
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }

        public int getrecordscountgiventable(string tablename, string CONNECTION_STRING)
        {
            string query = "SELECT * FROM " + tablename;
            DataTable dt = getallrecordsglobal(query, CONNECTION_STRING);
            if (dt != null)
                return dt.Rows.Count;
            else
                return -1;
        }
        public bool isdbconnectionalive(string CONNECTION_STRING)
        {
            var con = new SqlConnection(CONNECTION_STRING);
            con.Open();
            return true;
        }

        public bool isdbconnectionalive()
        {
            try
            {
                //setup the connection to the database
                var con = new SqlConnection(CONNECTION_STRING);
                con.Open();
                return true;
            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                return false;
            }

        }

        public DataTable getallrecordsglobal(string query, string CONNECTION_STRING)
        {
            if (!isdbconnectionalive(CONNECTION_STRING)) return null;

            if (string.IsNullOrEmpty(query.Trim()))
                return null;
            using (var con = new SqlConnection(CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new SqlCommand(query, con))
                {
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
        }

        public DataTable getallrecordsglobal(string query)
        {
            if (!isdbconnectionalive()) return null;

            if (string.IsNullOrEmpty(query.Trim()))
                return null;
            using (var con = new SqlConnection(CONNECTION_STRING))
            {
                con.Open();
                using (var cmd = new SqlCommand(query, con))
                {
                    var da = new SqlDataAdapter(cmd);
                    var dt = new DataTable();
                    da.Fill(dt);
                    da.Dispose();
                    return dt;
                }
            }
        }
        public DataTable getallrecordsglobal()
        {
            DataTable dt = getallrecordsglobal(DBContract.SELECT_ALL_QUERY);
            return dt;
        }

        public List<error_logger_dto> getallweights()
        {
            List<error_logger_dto> weights = new List<error_logger_dto>();
            using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                using (SqlCommand cmd = new SqlCommand(DBContract.SELECT_ALL_QUERY, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        error_logger_dto dto = new error_logger_dto();
                        dto.error_id = reader.GetString(0);
                        dto.error_description = reader.GetString(1);
                        dto.error_date = reader.GetString(2);
                        dto.error_category = reader.GetString(3);
                        dto.created_date = reader.GetString(4);
                        dto.error_source = reader.GetString(5);

                        weights.Add(dto);
                    }
                }
            }
            return weights;
        }

        public int insertgeneric(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;
            //setup the connection to the database
            using (var con = new SqlConnection(CONNECTION_STRING))
            {
                con.Open();
                //open a new command
                using (var cmd = new SqlCommand(query, con))
                {
                    //set the arguments given in the query
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    //execute the query and get the number of row affected
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
                return numberOfRowsAffected;
            }
        }

        public int insertgeneric(string query, Dictionary<string, object> args, string CONNECTION_STRING)
        {
            int numberOfRowsAffected;
            if (CONNECTION_STRING == null)
            {
                numberOfRowsAffected = insertgeneric(query, args);
                return numberOfRowsAffected;
            }
            else if (String.IsNullOrEmpty(CONNECTION_STRING))
            {
                numberOfRowsAffected = insertgeneric(query, args);
                return numberOfRowsAffected;
            }
            else
            {

                //setup the connection to the database
                using (var con = new SqlConnection(CONNECTION_STRING))
                {
                    con.Open();
                    //open a new command
                    using (var cmd = new SqlCommand(query, con))
                    {
                        //set the arguments given in the query
                        foreach (var pair in args)
                        {
                            cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                        }
                        //execute the query and get the number of row affected
                        numberOfRowsAffected = cmd.ExecuteNonQuery();
                    }
                    return numberOfRowsAffected;
                }
            }
        }

        public responsedto createweightindatabase(error_logger_dto _weight_record_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string query = "INSERT INTO " +
                DBContract.error_entity_table.TABLE_NAME +
                " ( " +
                DBContract.error_entity_table.WEIGHT_WEIGHT + ", " +
                DBContract.error_entity_table.WEIGHT_DATE + ", " +
                DBContract.error_entity_table.WEIGHT_STATUS + ", " +
                DBContract.error_entity_table.CREATED_DATE + ", " +
                DBContract.error_entity_table.WEIGHT_APP +
                " ) VALUES(@weight_weight, @weight_date, @weight_status, @created_date, @weight_app)";

                //here we are setting the parameter values that will be actually replaced in the query in Execute method
                var args = new Dictionary<string, object>
			    {
				    {"@weight_weight", _weight_record_dto.error_description},
				    {"@weight_date", _weight_record_dto.error_date},
                    {"@weight_status", "active"},
				    {"@created_date", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt")},
                    {"@weight_app", _weight_record_dto.error_source}
			    };

                int numberOfRowsAffected = insertgeneric(query, args, CONNECTION_STRING);
                if (numberOfRowsAffected != 1)
                {
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage = "Record creation failed in " + DBContract.mssql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responseerrormessage, TAG));
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "Record created successfully in " + DBContract.mssql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responsesuccessmessage, TAG));
                    return _responsedto;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }
        public responsedto updateweightindatabase(error_logger_dto _weight_record_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string query = "UPDATE " +
                DBContract.error_entity_table.TABLE_NAME +
                " SET " +
                DBContract.error_entity_table.WEIGHT_WEIGHT + " = @weight_weight, " +
                DBContract.error_entity_table.WEIGHT_DATE + " = @weight_date " +
                "WHERE " +
                DBContract.error_entity_table.WEIGHT_ID + " = @weight_id";

                //here we are setting the parameter values that will be actually replaced in the query in Execute method
                var args = new Dictionary<string, object>
			    {
				    {"@weight_id", _weight_record_dto.error_id},
				    {"@weight_weight", _weight_record_dto.error_description},
				    {"@weight_date", _weight_record_dto.error_date}
			    };

                int numberOfRowsAffected = updategeneric(query, args);
                if (numberOfRowsAffected != 1)
                {
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage = "Record update failed in " + DBContract.mssql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responseerrormessage, TAG));
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "Record updated successfully in " + DBContract.mssql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responsesuccessmessage, TAG));
                    return _responsedto;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }
        public int updategeneric(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;
            //setup the connection to the database
            using (var con = new SqlConnection(CONNECTION_STRING))
            {
                con.Open();
                //open a new command
                using (var cmd = new SqlCommand(query, con))
                {
                    //set the arguments given in the query
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    //execute the query and get the number of row affected
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
                return numberOfRowsAffected;
            }
        }
        public responsedto delete_weight_by_changing_status_in_database(error_logger_dto _weight_record_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string query = "UPDATE " +
                DBContract.error_entity_table.TABLE_NAME +
                " SET " +
                DBContract.error_entity_table.WEIGHT_STATUS + " = @weight_status " +
                "WHERE " +
                DBContract.error_entity_table.WEIGHT_ID + " = @weight_id";

                //here we are setting the parameter values that will be actually replaced in the query in Execute method
                var args = new Dictionary<string, object>
			    {
				    {"@weight_id", _weight_record_dto.error_id},
				    {"@weight_status", "inactive"}
			    };

                int numberOfRowsAffected = updategeneric(query, args);
                if (numberOfRowsAffected != 1)
                {
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage = "Record delete failed in " + DBContract.mssql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responseerrormessage, TAG));
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "Record deleted successfully in " + DBContract.mssql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responsesuccessmessage, TAG));
                    return _responsedto;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }
        public responsedto deleteweightindatabase(error_logger_dto _weight_record_dto)
        {
            responsedto _responsedto = new responsedto();
            try
            {
                string query = "DELETE FROM " +
                DBContract.error_entity_table.TABLE_NAME +
                " WHERE " +
                DBContract.error_entity_table.WEIGHT_ID + " = @weight_id";

                //here we are setting the parameter values that will be actually replaced in the query in Execute method
                var args = new Dictionary<string, object>
				{
					{"@weight_id", _weight_record_dto.error_id}  
				};

                int numberOfRowsAffected = deletegeneric(query, args);
                if (numberOfRowsAffected != 1)
                {
                    _responsedto.isresponseresultsuccessful = false;
                    _responsedto.responseerrormessage = "Record delete failed in " + DBContract.mssql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responseerrormessage, TAG));
                    return _responsedto;
                }
                else
                {
                    _responsedto.isresponseresultsuccessful = true;
                    _responsedto.responsesuccessmessage = "Record deleted successfully in " + DBContract.mssql + ".";
                    this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(_responsedto.responsesuccessmessage, TAG));
                    return _responsedto;
                }

            }
            catch (Exception ex)
            {
                this._notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.Message, TAG));
                _responsedto.isresponseresultsuccessful = false;
                _responsedto.responseerrormessage = ex.Message;
                return _responsedto;
            }
        }
        public int deletegeneric(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;
            //setup the connection to the database
            using (var con = new SqlConnection(CONNECTION_STRING))
            {
                con.Open();
                //open a new command
                using (var cmd = new SqlCommand(query, con))
                {
                    //set the arguments given in the query
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }
                    //execute the query and get the number of row affected
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
                return numberOfRowsAffected;
            }
        }








    }
}
