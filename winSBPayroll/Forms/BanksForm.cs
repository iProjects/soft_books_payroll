using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;


namespace winSBPayroll.Forms
{
    public partial class BanksForm : Form
    {
        List<DAL.Bank> banksList;
        string _User = "Sys";
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public BanksForm(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BanksForm_Load(object sender, EventArgs e)
        {
            try
            {
                var bankquery = db.Banks.Include("BankBranches");
                banksList = bankquery.ToList();
                bsBanks.DataSource = banksList;
                groupBox1.Text = bsBanks.Count.ToString();
                dgBanks.AutoGenerateColumns = false;
                this.dgBanks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgBanks.DataSource = bsBanks;
                dgBranches.AutoGenerateColumns = false;
                this.dgBranches.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgBranches.DataSource = bsBranches;
                groupBox2.Text = bsBranches.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dgBanks_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgBanks.SelectedRows.Count != 0)
                {
                    DAL.Bank bank = (DAL.Bank)bsBanks.Current;
                    bsBranches.DataSource = bank.BankBranches;
                    groupBox2.Text = bsBranches.Count.ToString();
                    foreach (DataGridViewRow row in dgBranches.Rows)
                    {
                        dgBranches.Rows[dgBranches.Rows.Count - 1].Selected = true;
                        int nRowIndex = dgBranches.Rows.Count - 1;
                        bsBranches.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                BankBranch branch = new BankBranch();
                branch.BankSortCode = branch.Bank + branch.BranchCode;
                db.SaveChanges();
                MessageBox.Show("Successfully saved", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BankBranch branch = new BankBranch();
                branch.BankSortCode = branch.Bank + branch.BranchCode;
                db.SaveChanges();
                MessageBox.Show("Successfully saved", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        #region Uploading

        private void uploadBanksToolStripMenuItem_Click(object sender, EventArgs e)
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
                RefreshBankGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error during upload. Error details are\n  " + ex.Message + "\nColumn names should be formated as\n[BankCode,BankName]", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Upload incomplete", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

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

        private void uploadBranchesToolStripMenuItem_Click(object sender, EventArgs e)
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
                UploadBankBranch(true, strFileName, _User);
                MessageBox.Show("Upload completed successfully", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                RefreshBankGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error during upload. Error details are  " + ex.Message, "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Upload incomplete", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void UploadBankBranch(bool Overwrite, string strFileName, string User)
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

            query = "SELECT BankSortCode, BranchCode, Bank, BranchName FROM [Sheet1$]";

            using (var myConnection = new OleDbConnection(SourceConnectionString))

            using (var destinationConnection = new SqlConnection(destinationConnectionString))

            using (var bulkCopy = new SqlBulkCopy(destinationConnection))
            {

                //Map first column in source to second column in sql table (skipping the ID column).

                //Excel schema[Vehicle] Table schema[ID, Vehicle, QueueDate, QueueStatus, QueuePriority]
                //bulkCopy.ColumnMappings.Add(Excel, Sql)
                bulkCopy.ColumnMappings.Add("BankSortCode", "BankSortCode");
                bulkCopy.ColumnMappings.Add("BranchCode", "BranchCode");
                bulkCopy.ColumnMappings.Add("Bank", "Bank");
                bulkCopy.ColumnMappings.Add("BranchName", "BranchName");

                bulkCopy.DestinationTableName = "BankBranch";

                if (Overwrite)
                {
                    try
                    {
                        //string deleteQuery = "Delete from BankBranch";
                        //var delcmd = new SqlCommand(deleteQuery, destinationConnection);
                        //destinationConnection.Open();
                        //delcmd.ExecuteNonQuery();
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

        #endregion

        #region Downloading

        private void DownloadBanks(string strFileName, string User)
        {
            try
            {
                Reports.Excel.CreateExcelDoc excell_app = new Reports.Excel.CreateExcelDoc();

                //creates the main header
                //createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string color, bool font, int size, string fcolor)
                excell_app.createHeaders(1, 1, "BankCode", "A1", "A1", 0, "WHITE", true, 10, "n");
                //creates subheaders
                excell_app.createHeaders(1, 2, "BankName", "A2", "A2", 0, "WHITE", true, 10, "n");


                int row = 2;
                foreach (var rec in de.GetBanks())
                {
                    //add Data to to cells
                    int col = 1;
                    string addr = excell_app.IntAlpha(col) + row;
                    //excell_app.addData(row, col, rec.BankCode.ToString(), addr, addr, "#,##0");
                    excell_app.createHeaders(row, col, rec.BankCode.ToString(), addr, addr, 0, "WHITE", true, 10, "n");

                    col++;
                    addr = excell_app.IntAlpha(col) + row;
                    //excell_app.addData(row, col, rec.BankName.ToString(), addr, addr, "#,##0");
                    excell_app.createHeaders(row, col, rec.BankName.ToString(), addr, addr, 0, "WHITE", true, 10, "n");

                    row++;

                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void DownloadBankBranhes(string strFileName, string User)
        {
            try
            {
                Reports.Excel.CreateExcelDoc excell_app = new Reports.Excel.CreateExcelDoc();

                //creates the main header
                //createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor)
                excell_app.createHeaders(1, 1, "BankSortCode", "A1", "A1", 0, "WHITE", true, 10, "n");
                //creates subheaders
                excell_app.createHeaders(1, 2, "BranchCode", "A2", "A2", 0, "WHITE", true, 10, "n");
                excell_app.createHeaders(1, 3, "Bank", "A3", "A3", 0, "WHITE", true, 10, "n");
                excell_app.createHeaders(1, 4, "BranchName", "A4", "A4", 0, "WHITE", true, 10, "n");

                int row = 2;
                foreach (var rec in de.GetBankBranches())
                {
                    //add Data to to cells
                    int col = 1;
                    string addr = excell_app.IntAlpha(col) + row;
                    //excell_app.addData(row, col, rec.BankSortCode.ToString(), addr, addr, "#,##0");
                    excell_app.createHeaders(row, col, rec.BankSortCode.ToString(), addr, addr, 0, "WHITE", true, 10, "n");

                    col++;
                    addr = excell_app.IntAlpha(col) + row;
                    //excell_app.addData(row, col, rec.BranchCode.ToString(), addr, addr, "#,##0");
                    excell_app.createHeaders(row, col, rec.BranchCode.ToString(), addr, addr, 0, "WHITE", true, 10, "n");

                    col++;
                    addr = excell_app.IntAlpha(col) + row;
                    //excell_app.addData(row, col, rec.BankName.ToString(), addr, addr, "#,##0");
                    excell_app.createHeaders(row, col, rec.BankName.ToString(), addr, addr, 0, "WHITE", true, 10, "n");

                    col++;
                    addr = excell_app.IntAlpha(col) + row;
                    //excell_app.addData(row, col, rec.BankBranchName.ToString(), addr, addr, "#,##0");

                    excell_app.createHeaders(row, col, rec.BankBranchName.ToString(), addr, addr, 0, "WHITE", true, 10, "n");
                    row++;

                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void lblBanks_Click(object sender, EventArgs e)
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
                DownloadBanks(strFileName, _User);
                MessageBox.Show("Download completed successfully", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error during download. Error details are  " + ex.Message, "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Download incomplete", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void lblBranches_Click(object sender, EventArgs e)
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
                DownloadBankBranhes(strFileName, _User);
                MessageBox.Show("Download completed successfully", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error during download. Error details are  " + ex.Message, "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Download incomplete", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void lblBranches_MouseHover(object sender, EventArgs e)
        {
            lblBranches.ForeColor = Color.Red;
        }

        private void lblBanks_MouseHover(object sender, EventArgs e)
        {
            lblBanks.ForeColor = Color.Red;
        }

        private void lblBanks_MouseLeave(object sender, EventArgs e)
        {
            lblBanks.ForeColor = Color.Black;
        }

        private void lblBranches_MouseLeave(object sender, EventArgs e)
        {
            lblBranches.ForeColor = Color.Black;
        }

        #endregion


        public void RefreshBankGrid()
        {
            try
            {
                db = new SBPayrollDBEntities(connection);
                bsBanks.DataSource = null;
                bsBranches.DataSource = null;
                var bankquery = db.Banks.Include("BankBranches");
                banksList = bankquery.ToList();
                bsBanks.DataSource = banksList;
                groupBox1.Text = bsBanks.Count.ToString();
                DAL.Bank bank = (DAL.Bank)bsBanks.Current;
                foreach (DataGridViewRow row in dgBanks.Rows)
                {
                    dgBanks.Rows[dgBanks.Rows.Count - 1].Selected = true;
                    int nRowIndex = dgBanks.Rows.Count - 1;
                    bsBanks.Position = nRowIndex;
                }
                if (dgBanks.SelectedRows.Count != 0)
                {
                    DAL.Bank _bank = (DAL.Bank)bsBanks.Current;
                    bsBranches.DataSource = _bank.BankBranches;
                    groupBox2.Text = bsBranches.Count.ToString();
                    foreach (DataGridViewRow row in dgBranches.Rows)
                    {
                        dgBranches.Rows[dgBranches.Rows.Count - 1].Selected = true;
                        int nRowIndex = dgBranches.Rows.Count - 1;
                        bsBranches.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public void RefreshBranchGrid()
        {
            try
            {
                db = new SBPayrollDBEntities(connection);
                bsBanks.DataSource = null;
                bsBranches.DataSource = null;
                var bankquery = db.Banks.Include("BankBranches");
                banksList = bankquery.ToList();
                bsBanks.DataSource = banksList;
                groupBox1.Text = bsBanks.Count.ToString();
                DAL.Bank bank = (DAL.Bank)bsBanks.Current;
                foreach (DataGridViewRow row in dgBanks.Rows)
                {
                    dgBanks.Rows[dgBanks.Rows.Count - 1].Selected = true;
                    int nRowIndex = dgBanks.Rows.Count - 1;
                    bsBanks.Position = nRowIndex;
                }
                if (dgBanks.SelectedRows.Count != 0)
                {
                    DAL.Bank _bank = (DAL.Bank)bsBanks.Current;
                    bsBranches.DataSource = _bank.BankBranches;
                    groupBox2.Text = bsBranches.Count.ToString();
                    foreach (DataGridViewRow row in dgBranches.Rows)
                    {
                        dgBranches.Rows[dgBranches.Rows.Count - 1].Selected = true;
                        int nRowIndex = dgBranches.Rows.Count - 1;
                        bsBranches.Position = nRowIndex;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void tsbAddBank_Click(object sender, EventArgs e)
        {
            try
            {

                Forms.AddBank ab = new Forms.AddBank(connection) { Owner = this };
                ab.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void tsbAddBranch_Click(object sender, EventArgs e)
        {
            if (dgBanks.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.Bank bank = (DAL.Bank)bsBanks.Current;
                    Forms.AddBankBranch abb = new Forms.AddBankBranch(bank, connection) { Owner = this };
                    abb.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void tsbAddBankBranch_Click(object sender, EventArgs e)
        {
            if (dgBanks.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.Bank bank = (DAL.Bank)bsBanks.Current;
                    Forms.AddBankBranch abb = new Forms.AddBankBranch(bank, connection) { Owner = this };
                    abb.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }

        }

        private void toolsAddBank_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AddBank ab = new Forms.AddBank(connection) { Owner = this };
                ab.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void tsbEditBankBranch_Click(object sender, EventArgs e)
        {
            if (dgBranches.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.Bank bank = (DAL.Bank)bsBanks.Current;
                    DAL.BankBranch bankbranch = (DAL.BankBranch)bsBranches.Current;
                    Forms.EditBankBranch ebb = new Forms.EditBankBranch(bankbranch, bank, connection) { Owner = this };
                    ebb.Text = bankbranch.BranchName.ToString().Trim().ToUpper();
                    ebb.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }

        }

        private void tsbEditBank_Click(object sender, EventArgs e)
        {
            if (dgBanks.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.Bank bank = (DAL.Bank)bsBanks.Current;
                    Forms.EditBank eb = new Forms.EditBank(bank, connection) { Owner = this };
                    eb.Text = bank.BankName.ToString().Trim().ToUpper();
                    eb.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }

        }

        private void tsbDeleteBank_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgBanks.SelectedRows.Count != 0)
                {
                    Bank bank = (Bank)bsBanks.Current;

                    var _BankBranchesquery = from bb in db.BankBranches
                                             where bb.Bank.BankCode == bank.BankCode
                                             select bb;
                    List<BankBranch> _BankBranches = _BankBranchesquery.ToList();

                   
                     if (_BankBranches.Count > 0)
                    {
                        MessageBox.Show("There is a Branch Associated with this Bank.\n Delete the Branch First!", "Confirm Delete", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Bank\n" + bank.BankName.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            db.Banks.DeleteObject(bank);
                            db.SaveChanges();
                            RefreshBankGrid();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void tsbDeleteBankBranch_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgBranches.SelectedRows.Count != 0)
                {
                    DAL.BankBranch bankbranch = (DAL.BankBranch)bsBranches.Current;

                    var _employeeswithBanksortcodequery = from em in rep.GetAllActiveEmployees()
                                                          join bb in db.BankBranches on em.BankCode equals bb.BankSortCode
                                                          where bb.BranchCode == bankbranch.BranchCode
                                                          select em;

                    List<Employee> _employees = _employeeswithBanksortcodequery.ToList();

                    if (_employees.Count > 0)
                    {
                        MessageBox.Show("There is an Employee Associated with this Branch.\n Delete the Employee First!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete Branch\n" + bankbranch.BranchName.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            db.BankBranches.DeleteObject(bankbranch);
                            db.SaveChanges();
                            RefreshBankGrid();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbAddBank_Click(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbEditBank_Click(sender, e);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tsbDeleteBank_Click(sender, e);
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lblBanks_Click(sender, e);
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tsbAddBankBranch_Click(sender, e);
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tsbEditBankBranch_Click(sender, e);
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            tsbDeleteBankBranch_Click(sender, e);
        }

        private void downloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lblBranches_Click(sender, e);
        }

        private void UploadtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            uploadBanksToolStripMenuItem_Click(sender, e);
        }

        private void dgBanks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgBanks.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.Bank bank = (DAL.Bank)bsBanks.Current;
                    Forms.EditBank eb = new Forms.EditBank(bank, connection) { Owner = this };
                    eb.Text = bank.BankName.ToString().Trim().ToUpper();
                    eb.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }

        }

        private void dgBranches_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgBranches.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.Bank bank = (DAL.Bank)bsBanks.Current;
                    DAL.BankBranch bankbranch = (DAL.BankBranch)bsBranches.Current;
                    Forms.EditBankBranch ebb = new Forms.EditBankBranch(bankbranch, bank, connection) { Owner = this };
                    ebb.Text = bankbranch.BranchName.ToString().Trim().ToUpper();
                    ebb.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }

        }




    }
}