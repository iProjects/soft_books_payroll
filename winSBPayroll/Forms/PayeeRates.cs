using System;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class PayeeRates : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _User;

        public PayeeRates(string user, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _User = user;
        }

        private void PayeeRates_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPayeeRates.AutoGenerateColumns = false;
                this.dataGridViewPayeeRates.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourcePayeeRates.DataSource = de.GetPayeeRates();
                dataGridViewPayeeRates.DataSource = bindingSourcePayeeRates;
                groupBox2.Text = bindingSourcePayeeRates.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AddPayeeRate apr = new Forms.AddPayeeRate(connection) { Owner = this };
                apr.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public void RefreshGrid()
        {
            try
            {
                //set the datasource to null
                bindingSourcePayeeRates.DataSource = null;
                //set the datasource to a method
                bindingSourcePayeeRates.DataSource = de.GetPayeeRates();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "select an excel file";
            //openFileDialog1.FileName = "";
            //"Text files (*.txt)|*.txt|All files (*.*)|*.*"
            openFileDialog1.Filter = "Excel Files|*.xls|Excel Files |*.xlsx";

            openFileDialog1.ShowDialog();

            string strFileName = openFileDialog1.FileName;

            // use bulkcopy method of upload
            try
            {
                //clear or backup the destination
                UploadPayeeRates(true, strFileName, _User);
                MessageBox.Show("Upload completed successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error during upload. Error details are  " + ex.Message);

                MessageBox.Show("Upload incomplete");
                return;
            }
        }
        private void UploadPayeeRates(bool Overwrite, string strFileName, string User)
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
                    catch (Exception e)
                    {
                        MessageBox.Show("Error: " + e.Message);
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
        private void btnDownload_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Select an excel file";
            //openFileDialog1.FileName = "";
            //"Text files (*.txt)|*.txt|All files (*.*)|*.*"
            saveFileDialog1.Filter = "Excel Files|*.xls|Excel Files |*.xlsx";


            saveFileDialog1.ShowDialog();

            string strFileName = saveFileDialog1.FileName;

            // use bulkcopy method of upload
            try
            {
                //clear or backup the destination
                Download(strFileName, _User);
                MessageBox.Show("Download completed successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error during download. Error details are  " + ex.Message);

                MessageBox.Show("Download incomplete");
                return;
            }
        }
        private void Download(string strFileName, string User)
        {
            Reports.Excel.CreateExcelDoc excell_app = new Reports.Excel.CreateExcelDoc();

            //creates the main header
            //createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor)
            excell_app.createHeaders(1, 1, "Id", "A1", "A1", 0, "WHITE", true, 10, "n");
            //creates subheaders
            excell_app.createHeaders(1, 2, "FromAmt", "A2", "A2", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 3, "ToAmt", "A3", "A3", 0, "WHITE", true, 10, "n");
            excell_app.createHeaders(1, 4, "Rate", "A4", "A4", 0, "WHITE", true, 10, "n");

            int row = 2;
            foreach (var rec in de.PayeeRatesTable())
            {
                //add Data to to cells
                int col = 1;
                string addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Id.ToString(), addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.Id.ToString(), addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.FromAmt.ToString(), addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.FromAmt.ToString(), addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.ToAmt.ToString(), addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.ToAmt.ToString(), addr, addr, 0, "WHITE", true, 10, "n");

                col++;
                addr = excell_app.IntAlpha(col) + row;
                //excell_app.addData(row, col, rec.Rate.ToString(), addr, addr, "#,##0");
                excell_app.createHeaders(row, col, rec.Rate.ToString(), addr, addr, 0, "WHITE", true, 10, "n");

                row++;
            }
        }


























    }
}