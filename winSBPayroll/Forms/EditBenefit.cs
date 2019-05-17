using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.DataEntry;
using DAL;
using CommonLib;

namespace winSBPayroll.Forms
{
    public partial class EditBenefit : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.Benefit _benefit;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public EditBenefit(DAL.Benefit benefit, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _benefit = benefit;

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (is_Validate())
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtName.Text))
                    {
                        _benefit.Description = Utils.ConvertFirstLetterToUpper(txtName.Text.Trim());
                    }
                    decimal rate;
                    if (!string.IsNullOrEmpty(txtRate.Text) && decimal.TryParse(txtRate.Text, out rate))
                    {
                        _benefit.Rate = decimal.Parse(txtRate.Text);
                    }

                    rep.UpdateBenefit(_benefit);

                    Benefits b = (Benefits)this.Owner;
                    b.RefreshGrid();
                    this.Close();
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
        private void EditBenefit_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControls();

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
        private void InitializeControls()
        {
            try
            {
                if (_benefit.Description != null)
                {
                    txtName.Text = _benefit.Description;
                }
                if (_benefit.Rate != null)
                {
                    txtRate.Text = _benefit.Rate.ToString();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }






    }
}