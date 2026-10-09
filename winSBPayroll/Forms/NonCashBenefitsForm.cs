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
using System.Data.Objects;

namespace winSBPayroll.Forms
{
    public partial class NonCashBenefitsForm : Form
    {
        DAL.Employee employee;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        private decimal TotalNonCashBenefitsAmount;

        //delegate
        public delegate void BenefitAmountHandler(object sender, BenefitAmountHandlerEventArgs e);
        //event
        public event BenefitAmountHandler OnEmployeeBenefitAmountChanged;


        public NonCashBenefitsForm(DAL.Employee emp, string Conn)
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

        private void NonCashBenefits_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridNonCashBenefits.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridNonCashBenefits.MultiSelect = false;

                // FIXED: Grid must not be entirely ReadOnly for user edits to register
                dataGridNonCashBenefits.ReadOnly = false;
                dataGridNonCashBenefits.AutoGenerateColumns = false;

                //Set the column header names.
                dataGridNonCashBenefits.Columns[0].Name = "EmployeeId";
                dataGridNonCashBenefits.Columns[1].Name = "EmpNo";
                dataGridNonCashBenefits.Columns[2].Name = "empBenefitQuantity";
                dataGridNonCashBenefits.Columns[3].Name = "cRate";
                dataGridNonCashBenefits.Columns[4].Name = "cTotal";

                List<int> _EmpNonCashBenefitsint = (from i in db.EmpNonCashBenefits
                                                    where i.EmpNo == employee.EmpNo
                                                    select i.BenefitId).ToList();

                var _noncashbenefitsquery = from bf in db.Benefits
                                            where bf.IsDeleted == false
                                            select bf;
                List<DAL.Benefit> _noncashbenefits = _noncashbenefitsquery.ToList();
                DataGridViewComboBoxColumn colCboxCategory = new DataGridViewComboBoxColumn();
                colCboxCategory.HeaderText = "Benefit";
                colCboxCategory.Name = "cbBenefitName";
                colCboxCategory.DataSource = _noncashbenefits;
                colCboxCategory.DisplayMember = "Description";
                colCboxCategory.DataPropertyName = "BenefitId";
                colCboxCategory.ValueMember = "Id";
                colCboxCategory.MaxDropDownItems = 10;
                colCboxCategory.DisplayIndex = 1;
                colCboxCategory.MinimumWidth = 200;
                colCboxCategory.FlatStyle = FlatStyle.Flat;
                colCboxCategory.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxCategory.ReadOnly = false;
                if (!this.dataGridNonCashBenefits.Columns.Contains("cbBenefitName"))
                {
                    dataGridNonCashBenefits.Columns.Add(colCboxCategory);
                }

                // FIXED: Bind to EF local cache or direct tracked entities
                var _EmpNonCashBenefitsquery = from eb in db.EmpNonCashBenefits
                                               where eb.EmpNo == employee.EmpNo
                                               select eb;

                bindingSourceNonCashBenefits.DataSource = _EmpNonCashBenefitsquery.ToList();
                groupBox2.Text = bindingSourceNonCashBenefits.Count.ToString();

                dataGridNonCashBenefits.DataSource = bindingSourceNonCashBenefits;

                lblRecordInfo.Text = "Non Cash Benefits record for    " + employee.Surname.Trim() + "  " + employee.OtherNames.Trim();

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
                // FIXED: Commit active cell editing and flush changes to BindingSource
                this.Validate();
                bindingSourceNonCashBenefits.EndEdit();

                // FIXED: Explicitly register added or modified items into the context if using untracked lists
                foreach (var item in bindingSourceNonCashBenefits.List)
                {
                    // C# 4.0 casting check
                    EmpNonCashBenefit benefit = item as EmpNonCashBenefit;
                    if (benefit != null)
                    {
                        // For ObjectContext (EF 4.0 default)
                        ObjectStateEntry stateEntry;
                        if (!db.ObjectStateManager.TryGetObjectStateEntry(benefit, out stateEntry) || stateEntry.State == EntityState.Detached)
                        {
                            db.EmpNonCashBenefits.AddObject(benefit);
                        }
                    }
                }

                // save to the database
                db.SaveChanges();

                MessageBox.Show("Save Successfull!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);

                CalculateTotalNonCashBenefitAmount();

                if (OnEmployeeBenefitAmountChanged != null)
                {
                    OnEmployeeBenefitAmountChanged(this, new BenefitAmountHandlerEventArgs(TotalNonCashBenefitsAmount));
                }

                if (this.Owner is EditEmployee)
                {
                    EditEmployee f = (EditEmployee)this.Owner;
                    f.refresh_emptxn_grid();
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

        private void CalculateTotalNonCashBenefitAmount()
        {
            try
            {
                TotalNonCashBenefitsAmount = 0;
                decimal rowAmount = 0;
                foreach (DataGridViewRow row in dataGridNonCashBenefits.Rows)
                {
                    int quantity = 0; decimal rate = 0;
                    if (row.Cells["empBenefitQuantity"].Value != null)
                    {
                        quantity = Convert.ToInt32(row.Cells["empBenefitQuantity"].Value);
                    }

                    if (row.Cells["cRate"].Value != null)
                    {
                        rate = Convert.ToDecimal(row.Cells["cRate"].Value);
                    }

                    rowAmount = quantity * rate;
                    row.Cells["cTotal"].Value = rowAmount;
                    TotalNonCashBenefitsAmount += rowAmount;
                }

                lblTotals.Text = TotalNonCashBenefitsAmount.ToString("C2");

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridNonCashBenefits_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridViewColumn column = dataGridNonCashBenefits.Columns[e.ColumnIndex];

            if (column.Name == "cbBenefitName")
            {
                CheckBenefit(e);
            }
            if (column.Name == "empBenefitQuantity")
            {
                CheckBenefQuantity(e);
            }
        }

        private void CheckBenefit(DataGridViewCellValidatingEventArgs newValue)
        {
            CalculateTotalNonCashBenefitAmount();
        }

        private void CheckBenefQuantity(DataGridViewCellValidatingEventArgs newValue)
        {
            Int32 ignored = new Int32();
            if (String.IsNullOrEmpty(newValue.FormattedValue.ToString()))
            {
                NotifyUserAndForceRedo("Please enter Quantity", newValue);
            }
            else if (!Int32.TryParse(newValue.FormattedValue.ToString(), out ignored))
            {
                NotifyUserAndForceRedo("Quantity must be an Integer", newValue);
            }
        }

        private void NotifyUserAndForceRedo(string errorMessage, DataGridViewCellValidatingEventArgs newValue)
        {
            MessageBox.Show(errorMessage);
            newValue.Cancel = true;
        }

        private void dataGridNonCashBenefits_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells["EmpNo"].Value = employee.EmpNo;
                e.Row.Cells["EmployeeId"].Value = employee.Id;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridNonCashBenefits_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void dataGridNonCashBenefits_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewCell cell = (DataGridViewCell)dataGridNonCashBenefits.Rows[e.RowIndex].Cells[e.ColumnIndex];
                DataGridViewRow row = dataGridNonCashBenefits.Rows[e.RowIndex];

                if (!row.IsNewRow && cell.ColumnIndex == this.dataGridNonCashBenefits.Columns["cbBenefitName"].Index)
                {
                    cell.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridNonCashBenefits_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            CalculateTotalNonCashBenefitAmount();
        }

        private void dataGridNonCashBenefits_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (dataGridNonCashBenefits.CurrentCell.OwningColumn is DataGridViewComboBoxColumn)
            {
                DataGridViewComboBoxEditingControl editingControl =
                         (DataGridViewComboBoxEditingControl)dataGridNonCashBenefits.EditingControl;
                e.Value = editingControl.SelectedItem;

                DAL.Benefit benefit = (DAL.Benefit)editingControl.SelectedItem;

                if (benefit != null)
                {
                    this.dataGridNonCashBenefits.Rows[dataGridNonCashBenefits.CurrentCell.RowIndex].Cells["cRate"].Value = benefit.Rate.ToString();
                }
            }
            CalculateTotalNonCashBenefitAmount();
        }

        public void DisableControls()
        {
            try
            {
                groupBox2.Enabled = false;
                dataGridNonCashBenefits.Enabled = false;
                btnSave.Enabled = false;
                btnSave.Visible = false;
                btnClose.Location = btnSave.Location;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void lblTotals_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public class BenefitAmountHandlerEventArgs : System.EventArgs
    {
        private decimal bAmount;

        public BenefitAmountHandlerEventArgs(decimal _bAmount)
        {
            this.bAmount = _bAmount;
        }

        public decimal Totalquantity
        {
            get
            {
                return bAmount;
            }
        }
    }
}
