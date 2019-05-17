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
    public partial class Banks : Form
    {
        DataEntry de  ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _User = "Sys";

        public Banks(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void Banks_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoGenerateColumns = false;
                bindingSource1.DataSource = de.GetBanks();
                this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DataSource = bindingSource1;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Forms.AddBank b = new  Forms.AddBank() { Owner = this };
            //b.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void RefreshGrid()
        {
            ////set the datasource to null
            //bindingSourcePayrolls.DataSource = null;
            ////set the datasource to a method
            //bindingSourcePayrolls.DataSource = de.GetBanks();

        }

        private void btnUpload_Click(object sender, EventArgs e)
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
                UploadBank(true, strFileName, _User);
                MessageBox.Show("Upload completed successfully", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error during upload. Error details are  " + ex.Message, "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Upload incomplete", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            

        }

        #region Uploading

        private void UploadBank(bool Overwrite, string strFileName, string User)
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

            query = "SELECT BankCode,BankName FROM [Sheet1$]";

            using (var myConnection = new OleDbConnection(SourceConnectionString))

            using (var destinationConnection = new SqlConnection(destinationConnectionString))

            using (var bulkCopy = new SqlBulkCopy(destinationConnection))
            {

                //Map first column in source to second column in sql table (skipping the ID column).

                //Excel schema[Vehicle] Table schema[ID, Vehicle, QueueDate, QueueStatus, QueuePriority]
                //bulkCopy.ColumnMappings.Add(Excel, Sql)
                bulkCopy.ColumnMappings.Add("BankCode", "BankCode"); //
                bulkCopy.ColumnMappings.Add("BankName", "BankName"); //

                bulkCopy.DestinationTableName = "Banks";

                if (Overwrite)
                {
                    try
                    {
                        string deleteQuery = "Delete from Banks";
                        var delcmd = new SqlCommand(deleteQuery, destinationConnection);
                        destinationConnection.Open();
                        delcmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error: " + e.Message, "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count != 0)
                {
                    DAL.Bank bank = (DAL.Bank)bindingSource1.Current;

                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete ", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        de.DeleteBank(bank.BankCode);
                        RefreshGrid();

                    }

                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        
    }
}
