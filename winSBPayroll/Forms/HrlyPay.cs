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

namespace winSBPayroll.Forms
{
    public partial class HrlyPay : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.Employee employee;  
        IQueryable hrlyQuery;
        private decimal HrlyAmount;

        //delegate
        public delegate void HrlyAmountHandler(object sender, HrlyAmountHandlerEventArgs e); 
        //event
        public event HrlyAmountHandler OnEmployeeHrlyAmountChanged;


        public HrlyPay(DAL.Employee emp, string Conn)
        {
            InitializeComponent(); 
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            employee = emp;
        }
        private void HrlyPay_Load(object sender, EventArgs args)
        {
            try
            { 

                hrlyQuery = db.HourlyPayments.Where(i => i.Empno == employee.EmpNo).OrderBy(i => i.WorkDate);
               
                // Set the column header names.
                dataGridHourlyPayments.Columns[0].Name = "EmpNo";
                dataGridHourlyPayments.Columns[1].Name = "WorkDate";
                dataGridHourlyPayments.Columns[2].Name = "WorkHours";
                dataGridHourlyPayments.Columns[3].Name = "RatePerHour";
                dataGridHourlyPayments.Columns[4].Name = "TotalPay";


                dataGridHourlyPayments.AutoGenerateColumns = false;
                dataGridHourlyPayments.AllowUserToAddRows = true;
                dataGridHourlyPayments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridHourlyPayments.EditMode = DataGridViewEditMode.EditOnEnter;
                bindingSourceHourlyPayments.DataSource = hrlyQuery;
                groupBox2.Text = bindingSourceHourlyPayments.Count.ToString();
                dataGridHourlyPayments.DataSource = bindingSourceHourlyPayments;

                lblRecordInfo.Text = "Hourly Pay record for   " + employee.Surname.Trim() + "  " + employee.OtherNames.Trim();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private DataTable ConvertPaymentsListToDataTable(List<HourlyPayment> HrlyPayList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EmpNo", typeof(string));
            dt.Columns.Add("WorkDate", typeof(DateTime));
            dt.Columns.Add("WorkHours", typeof(int));
            dt.Columns.Add("RatePerHour", typeof(decimal));
            dt.Columns.Add("TotalPay", typeof(decimal));
            dt.Columns["TotalPay"].Expression = "[WorkHours] * [RatePerHour]";

            foreach (var item in HrlyPayList)
            {
                dt.Rows.Add(item.Empno, item.WorkDate, item.WorkHours, item.RatePerHour);
            }

            return dt;
        }
        private void btnSave_Click(object sender, EventArgs arg)
        {
            try
            {
                //save to the database
                db.SaveChanges();
                MessageBox.Show("Save Successfull!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);

                ComputeTotal();

                if (OnEmployeeHrlyAmountChanged != null)
                {
                    OnEmployeeHrlyAmountChanged(this, new HrlyAmountHandlerEventArgs(HrlyAmount));
                }


                if (this.Owner is EditEmployee)
                {
                    EditEmployee f = (EditEmployee)this.Owner;
                    f.GridRefresh();
                    this.Close();
                }
                else if (this.Owner is AddEmpTxn)
                {
                    AddEmpTxn aet = (AddEmpTxn)this.Owner;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }
        private void ComputeTotal()
        {
            try
            {
                HrlyAmount = 0;
                decimal rowAmount = 0;
                foreach (DataGridViewRow row in dataGridHourlyPayments.Rows)
                {
                    int workhrs = 0; decimal rate = 0;
                    if (row.Cells["WorkHours"].Value != null)
                    {
                        workhrs = (int)row.Cells["WorkHours"].Value;
                    }

                    if (row.Cells["RatePerHour"].Value != null)
                    {
                        rate = (decimal)row.Cells["RatePerHour"].Value;
                    }
                    rowAmount = workhrs * rate;
                    row.Cells["TotalPay"].Value = rowAmount;
                    HrlyAmount += rowAmount;
                }

                lblHrlyPayTotals.Text = HrlyAmount.ToString("C2");

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
        private void dataGridHourlyPayments_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells["EmpNo"].Value = employee.EmpNo;
                e.Row.Cells["WorkHours"].Value = "0";
                e.Row.Cells["RatePerHour"].Value = "0";
            }
            catch (InvalidOperationException ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridHourlyPayments_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                DataGridViewColumn column = dataGridHourlyPayments.Columns[e.ColumnIndex];

                if (column.Name == "WorkDate")
                {
                    CheckWorkDate(e);
                }
                if (column.Name == "WorkHours")
                {
                    CheckWorkHours(e);
                }
                if (column.Name == "RatePerHour")
                {
                    CheckRatePerHour(e);
                }
            }
            catch (InvalidOperationException ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void CheckWorkDate(DataGridViewCellValidatingEventArgs newValue)
        {
            DateTime dt;
            if (!DateTime.TryParse(newValue.FormattedValue.ToString(), out dt))
            {
                AnnotateCell("You did not enter a valid date.", newValue);
            } 
        }
        private void CheckWorkHours(DataGridViewCellValidatingEventArgs newValue)
        {

            Int32 wkhrs = new Int32();
            if (String.IsNullOrEmpty(newValue.FormattedValue.ToString()))
            {
                NotifyUserAndForceRedo("Please enter Work Hour(s)", newValue);
            }
            else if (!Int32.TryParse(newValue.FormattedValue.ToString(), out wkhrs))
            {
                NotifyUserAndForceRedo("Work Hour(s) must be an Integer", newValue);
            }
            else if (Int32.Parse(newValue.FormattedValue.ToString()) < 0)
            {
                NotifyUserAndForceRedo("Work Hour(s) cannot  be less than 1", newValue);
            }
            else if (Int32.Parse(newValue.FormattedValue.ToString()) > 24)
            {
                NotifyUserAndForceRedo("Work Hour(s) cannot be greater than 24", newValue);
            }
        }
        private void CheckRatePerHour(DataGridViewCellValidatingEventArgs newValue)
        {
            decimal rateperhr = new decimal();
            if (String.IsNullOrEmpty(newValue.FormattedValue.ToString()))
            {
                NotifyUserAndForceRedo("Please enter Pay Per Hour", newValue);
            }
            else if (!decimal.TryParse(newValue.FormattedValue.ToString(), out rateperhr))
            {
                NotifyUserAndForceRedo("Pay Per Hour must be a Decimal", newValue);
            }

        }
        private void NotifyUserAndForceRedo(string errorMessage, DataGridViewCellValidatingEventArgs newValue)
        {
            MessageBox.Show(errorMessage);
            newValue.Cancel = true;
        }
        private void AnnotateCell(string errorMessage, DataGridViewCellValidatingEventArgs editEvent)
        {

            DataGridViewCell cell = dataGridHourlyPayments.Rows[editEvent.RowIndex].Cells[editEvent.ColumnIndex];
            cell.ErrorText = errorMessage;
        }
        private void dataGridHourlyPayments_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCell cell = (DataGridViewCell)dataGridHourlyPayments.Rows[e.RowIndex].Cells[e.ColumnIndex];

                DataGridViewRow row = dataGridHourlyPayments.Rows[e.RowIndex];

                if (!row.IsNewRow && cell.ColumnIndex == this.dataGridHourlyPayments.Columns["WorkDate"].Index)
                {
                    cell.ReadOnly = true;
                }
            }
            catch (InvalidOperationException ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridHourlyPayments_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                ComputeTotal();
            }
            catch (InvalidOperationException ex)
            {
                Utils.ShowError(ex);
            }
        }
        public void DisableControls()
        {
            try
            {
                groupBox2.Enabled = false;
                dataGridHourlyPayments.Enabled = false;
                btnSave.Enabled = false;
                btnSave.Visible = false;
                btnClose.Location = btnSave.Location;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }






    }




    public class HrlyAmountHandlerEventArgs : System.EventArgs
    {
        // add local member variables to hold text
        private decimal _Amount; 
        // class constructor
        public HrlyAmountHandlerEventArgs(decimal Amount)
        {
            this._Amount = Amount;
        } 
        // Properties - Viewable by each listener
        public decimal TotalHrlyAmount
        {
            get
            {
                return _Amount;
            }
        }
    }
}
