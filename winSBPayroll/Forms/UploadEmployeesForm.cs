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
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace winSBPayroll.Forms
{
    public partial class UploadEmployeesForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string user;
        public string TAG;
        //Event declaration:
        //event for publishing messages to output
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname_from_parent;
        IQueryable<DAL.Employee> _Employees;
        public List<notificationdto> _lstnotificationdto = new List<notificationdto>();

        public UploadEmployeesForm(string _user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname_from_parent)
        {
            InitializeComponent();

            TAG = this.GetType().Name;

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

            //Subscribing to the event: 
            //Dynamically:
            //EventName += HandlerName;
            _notificationmessageEventname += notificationmessageHandler;
            _notificationmessageEventname_from_parent += notificationmessageEventname_from_parent;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            user = _user;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished UploadEmployeesForm initialization", TAG));

        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.Write_To_Log_File_temp_dir(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.Write_To_Log_File_temp_dir(ex);
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

        private void UploadEmployeesForm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView_valid_records.AutoGenerateColumns = false;
                this.dataGridView_valid_records.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView_valid_records.DataSource = bindingSource_valid_records;

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished UploadEmployeesForm load", TAG));

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnUploadFromExcel_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog.Title = "select an excel file";
                //openFileDialog1.FileName = "";
                //"Text files (*.txt)|*.txt|All files (*.*)|*.*"
                openFileDialog.Filter = "Excel Files|*.xls;*.xlsx";

                openFileDialog.ShowDialog();

                string strFileName = openFileDialog.FileName;

                // use bulkcopy method of upload
                try
                { 
                    display_records(strFileName, user);
                     
                    Utils.ShowInfo("Upload completed successfully");
                }
                catch (Exception ex)
                {
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG)); 
                    Utils.ShowError(ex);
                }
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }
        private void display_records(string strFileName, string User)
        {
            string query = null;
            string SourceConnectionString = "";
            string strFileType = System.IO.Path.GetExtension(strFileName).ToString().ToLower();

            //Check file type
            if (strFileType != ".xls" && strFileType != ".xlsx")
            {
                throw new Exception("File Type not Excel");
            }

            //Connection String to Excel Workbook
            if (strFileType.Trim() == ".xls")
            {
                SourceConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName
                    + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                SourceConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                    + strFileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }

            query = "SELECT Email, EmpNo, Surname, OtherNames FROM [Sheet1$]";

            using (var sourceConnection = new OleDbConnection(SourceConnectionString))
            {
                using (var cmd = new OleDbCommand(query, sourceConnection))
                {
                    sourceConnection.Open();
                     
                    DataTable dt = new DataTable();
                    dt.Load(cmd.ExecuteReader());
                    refresh_grid(dt);
                }
            }
        }

        private void refresh_grid(DataTable dt)
        {
            try
            {
                bindingSource_valid_records.DataSource = dt;
                dataGridView_valid_records.DataSource = bindingSource_valid_records;
                string counta = bindingSource_valid_records.Count.ToString();
                groupBox2.Text = counta;

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("loaded [ " + counta + " ] records...", TAG));
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void UploadEmployees(bool Overwrite, string strFileName, string User)
        {
            string query = null;
            string SourceConnectionString = "";
            string strFileType = System.IO.Path.GetExtension(strFileName).ToString().ToLower();

            //Check file type
            if (strFileType != ".xls" && strFileType != ".xlsx")
            {
                throw new Exception("File Type not Excel");
            }

            //Connection String to Excel Workbook
            if (strFileType.Trim() == ".xls")
            {
                SourceConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + strFileName
                    + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                SourceConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                    + strFileName + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            }
            string destinationConnectionString = this.GetConnectionString("DestinationConnectionString");

            query = "SELECT Id, FromAmt, ToAmt, Rate FROM [Sheet1$]";

            using (var myConnection = new OleDbConnection(SourceConnectionString))

            using (var destinationConnection = new SqlConnection(destinationConnectionString))

            using (var bulkCopy = new SqlBulkCopy(destinationConnection))
            {

                //Map first column in source to second column in sql table (skipping the ID column).

                //Excel schema[Vehicle] Table schema[ID, Vehicle, QueueDate, QueueStatus, QueuePriority]
                //bulkCopy.ColumnMappings.Add(Excel, Sql)
                bulkCopy.ColumnMappings.Add("Id", "Id"); //
                bulkCopy.ColumnMappings.Add("FromAmt", "FromAmt"); //
                bulkCopy.ColumnMappings.Add("ToAmt", "ToAmt");
                bulkCopy.ColumnMappings.Add("Rate", "Rate");

                bulkCopy.DestinationTableName = "PayeeRates";

                if (Overwrite)
                {
                    try
                    {
                        string deleteQuery = "Delete from PayeeRates";
                        var delcmd = new SqlCommand(deleteQuery, destinationConnection);
                        destinationConnection.Open();
                        delcmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                        Utils.ShowError(ex);
                    }
                    finally
                    {
                        destinationConnection.Close();
                    }
                }

                using (var myCommand = new OleDbCommand(query, myConnection))
                {
                    myConnection.Open();

                    destinationConnection.Open();

                    var myReader = myCommand.ExecuteReader();

                    bulkCopy.WriteToServer(myReader);
                }
            }
        }
        //Add data to repository 
        public string GetConnectionString(string str)
        {
            //variable to hold our return value
            string conn = string.Empty;
            //check if a value was provided
            if (!string.IsNullOrEmpty(str))
            {
                //name provided so search for that connection
                conn = ConfigurationManager.ConnectionStrings[str].ConnectionString;
            }
            else
            //name not provided, get the 'default' connection
            {
                conn = ConfigurationManager.ConnectionStrings["DestinationConnectionString"].ConnectionString;
            }
            //return the value
            return conn;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool overwrite = false;
                if (DialogResult.Yes == MessageBox.Show("Do you wish to overwrite?", "Confirm Overwrite", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    overwrite = true;
                }
                 
                //clear or backup the destination
                //UploadEmployees(overwrite, strFileName, user);
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewEmployees_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                Log.Write_To_Log_File_temp_dir(ex);
            }
        }


    }
}
