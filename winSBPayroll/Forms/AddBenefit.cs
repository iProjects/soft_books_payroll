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
    public partial class AddBenefit : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public AddBenefit(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (is_Validate())
            {
                try
                {
                    Benefit _Benefit = new Benefit();
                    if (!string.IsNullOrEmpty(txtName.Text))
                    {
                        _Benefit.Description = Utils.ConvertFirstLetterToUpper(txtName.Text.Trim());
                    }
                    decimal rate;
                    if (!string.IsNullOrEmpty(txtRate.Text) && decimal.TryParse(txtRate.Text, out rate))
                    {
                        _Benefit.Rate = decimal.Parse(txtRate.Text);
                    }
                    _Benefit.IsDeleted = false;

                    if (db.Benefits.Any(c => c.Description == _Benefit.Description && c.IsDeleted==false))
                    {
                        MessageBox.Show("Description Exist!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.Benefits.Any(c => c.Description == _Benefit.Description && c.IsDeleted==false))
                    {
                        db.Benefits.AddObject(_Benefit);
                        db.SaveChanges();

                        Benefits f = (Benefits)this.Owner;
                        f.RefreshGrid();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private bool is_Validate()
        {
            bool no_error = true;

            if (string.IsNullOrEmpty(txtName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtName, "Description cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtRate.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtRate, "Rate cannot be null!");
                return false;
            }
            decimal ratepay;
            if (!decimal.TryParse(txtRate.Text, out ratepay))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtRate, "Rate must be decimal!");
                return false;
            }
            return no_error;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }
        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            // Initialize the flag to false.
            nonNumberEntered = false;

            // Determine whether the keystroke is a number from the top of the keyboard.
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad.
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace.
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed.
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumberEntered = true;
                    }
                }
            }
            //If shift key was pressed, it'st not a number.
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumberEntered = true;
            }
        }
        private void AddBenefit_Load(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteStringCollection acscsrtcd = new AutoCompleteStringCollection();
                acscsrtcd.AddRange(this.AutoComplete_Name());
                txtName.AutoCompleteCustomSource = acscsrtcd;
                txtName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscdscrptn = new AutoCompleteStringCollection();
                acscdscrptn.AddRange(this.AutoComplete_Rate());
                txtRate.AutoCompleteCustomSource = acscdscrptn;
                txtRate.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtRate.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_Name()
        {
            try
            {
                var _dscriptionquery = from bk in db.Benefits
                                       where bk.IsDeleted == false
                                       select bk.Description;
                return _dscriptionquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_Rate()
        {
            try
            {
                var _ratequery = (from sb in db.Benefits
                                  where sb.IsDeleted == false
                                  select sb.Rate).Distinct();
                decimal?[] decimalarray = _ratequery.ToArray();
                List<string> items = new List<string>();
                for (int i = 0; i < _ratequery.Count(); i++)
                {
                    string strName = decimalarray[i].ToString();
                    items.Add(strName);
                }
                return items.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }




    }
}