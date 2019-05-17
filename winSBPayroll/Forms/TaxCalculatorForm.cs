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
    public partial class TaxCalculatorForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public TaxCalculatorForm(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                decimal pay;
                if (!decimal.TryParse(txtTaxablePay.Text, out pay))
                {
                    MessageBox.Show("Enter a valid taxable pay");
                    return;
                }
                PayslipMaker pm = new PayslipMaker(pay, connection);
                Payslip pslip = pm.CreateAnonymousPayslip();
                dataGridViewTaxCalculator.DataSource = pslip.TaxBracketList;
                lblGrossTax.Text = pslip.GrossTax.ToString("C2");
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void TaxCalculatorForm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewTaxCalculator.AutoGenerateColumns = false;
                this.dataGridViewTaxCalculator.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void txtTaxablePay_KeyDown(object sender, KeyEventArgs e)
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
        private void txtTaxablePay_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (nonNumberEntered == true)
                {
                    if (e.KeyChar == 13)
                    {

                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }


    }
}