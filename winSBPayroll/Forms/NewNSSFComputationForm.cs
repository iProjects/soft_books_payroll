using System; 
using System.Collections.Generic; 
using System.Configuration;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.OleDb; 
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry; 
using CommonLib;
using DAL;
using DAL.Criteria;

namespace winSBPayroll.Forms
{
    public partial class NewNSSFComputationForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _User;

        public NewNSSFComputationForm(string user, string Conn)
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

        private void NewNSSFComputationForm_Load(object sender, EventArgs e)
        {
            try
            {
                btnPrint.Visible = false;

                var _employeesurnamequery = from dp in rep.GetAllActiveEmployees()
                                            where dp.IsDeleted == false
                                            where dp.IsActive == true 
                                            select dp;
                List<Employee> _EmployeeSurname = _employeesurnamequery.ToList();
                DataGridViewComboBoxColumn colCboxSurname = new DataGridViewComboBoxColumn();
                colCboxSurname.HeaderText = "Surname";
                colCboxSurname.Name = "cbSurname";
                colCboxSurname.DataSource = _EmployeeSurname;
                // The display member is the name column in the column datasource  
                colCboxSurname.DisplayMember = "Surname";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxSurname.DataPropertyName = "EmpNo";
                // The value member is the primary key of the parent table  
                colCboxSurname.ValueMember = "EmpNo";
                colCboxSurname.MaxDropDownItems = 10;
                colCboxSurname.Width = 60;
                colCboxSurname.DisplayIndex = 2;
                colCboxSurname.MinimumWidth = 5;
                //colCboxSurname.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colCboxSurname.FlatStyle = FlatStyle.Flat;
                colCboxSurname.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxSurname.ReadOnly = true;
                if (!this.dataGridViewNssfContributions.Columns.Contains("cbSurname"))
                {
                    //dataGridViewNssfContributions.Columns.Add(colCboxSurname);
                }

                var _employeeothernamesquery = from dp in rep.GetAllActiveEmployees()
                                               where dp.IsDeleted == false
                                               where dp.IsActive == true 
                                               select dp;
                List<Employee> _EmployeeOtherNames = _employeeothernamesquery.ToList();
                DataGridViewComboBoxColumn colCboxOtherNames = new DataGridViewComboBoxColumn();
                colCboxOtherNames.HeaderText = "OtherNames";
                colCboxOtherNames.Name = "cbOtherNames";
                colCboxOtherNames.DataSource = _EmployeeOtherNames;
                // The display member is the name column in the column datasource  
                colCboxOtherNames.DisplayMember = "OtherNames";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxOtherNames.DataPropertyName = "EmpNo";
                // The value member is the primary key of the parent table  
                colCboxOtherNames.ValueMember = "EmpNo";
                colCboxOtherNames.MaxDropDownItems = 10;
                colCboxOtherNames.Width = 80;
                colCboxOtherNames.DisplayIndex = 3;
                colCboxOtherNames.MinimumWidth = 5;
                //colCboxOtherNames.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colCboxOtherNames.FlatStyle = FlatStyle.Flat;
                colCboxOtherNames.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxOtherNames.ReadOnly = true;
                if (!this.dataGridViewNssfContributions.Columns.Contains("cbOtherNames"))
                {
                    //dataGridViewNssfContributions.Columns.Add(colCboxOtherNames);
                }

                List<NssfContributionsDTO> _nssfContributions = new List<NssfContributionsDTO>();
                var _employeesquery = from ep in rep.GetAllActiveEmployees()
                                      where ep.IsActive == true
                                      where ep.IsDeleted == false 
                                      select ep;
                List<Employee> _employees = _employeesquery.ToList();
                foreach (var emp in _employees)
                {
                    NssfContributionsDTO _nssfcontribution = new NssfContributionsDTO();
                    _nssfcontribution = rep.ComputeNssfContributions(emp.Id);

                    if (!_nssfContributions.Any(i => i.EmployeeId == _nssfcontribution.EmployeeId))
                    {
                        _nssfContributions.Add(_nssfcontribution);
                    }
                }

                int counter = 1;
                foreach (var nssf in _nssfContributions)
                {
                    nssf.Id = counter;
                    counter++;
                }

                dataGridViewNssfContributions.AutoGenerateColumns = false;
                this.dataGridViewNssfContributions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourceNewNSSFComputation.DataSource = _nssfContributions;
                dataGridViewNssfContributions.DataSource = bindingSourceNewNSSFComputation;
                groupBox2.Text = bindingSourceNewNSSFComputation.Count.ToString();
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
                bindingSourceNewNSSFComputation.DataSource = null;
                //set the datasource to a method
                bindingSourceNewNSSFComputation.DataSource = de.GetPayeeRates();
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
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                List<NssfContributionsDTO> _nssfcomputations = (List<NssfContributionsDTO>)bindingSourceNewNSSFComputation.List;
                PDFViewer _viewer = new PDFViewer(_nssfcomputations, _User, connection);
                _viewer.WindowState = FormWindowState.Normal;
                _viewer.StartPosition = FormStartPosition.CenterScreen;
                _viewer.Show();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

















    }
}