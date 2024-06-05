using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL; 


namespace winSBPayroll.Forms
{
    public partial class AddBank : Form
    {
        DataEntry de  ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public AddBank(string Conn)
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
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (is_validate())
            {
                try
                {
                    Bank _Bank = new Bank();
                    if (!string.IsNullOrEmpty(txtBankCode.Text))
                    {
                        _Bank.BankCode = txtBankCode.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtBankName.Text))
                    {
                        _Bank.BankName = Utils.ConvertFirstLetterToUpper(txtBankName.Text.Trim());
                    }

                    if (db.Banks.Any(c => c.BankCode == _Bank.BankCode))
                    {
                        MessageBox.Show("Bank Code Exist!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.Banks.Any(c => c.BankCode == _Bank.BankCode))
                    {
                        db.Banks.AddObject(_Bank);
                        db.SaveChanges();


                        BanksForm b = (BanksForm)this.Owner;
                        b.RefreshBankGrid();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private bool is_validate()
        { 
            bool no_error = true; 
            if (string.IsNullOrEmpty(txtBankName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBankName, "Name cannot be null!");
                return false;
            } 
            if (string.IsNullOrEmpty(txtBankCode.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtBankCode, "Code cannot be null!");
                return false;
            }  
            return no_error; 
        } 
        private void AddBank_Load(object sender, EventArgs e)
        {
            try
            {
                string _BankCode = Utils.NextSeries(NextBankCode());
                txtBankCode.Text = _BankCode;

                AutoCompleteStringCollection acscbnknm = new AutoCompleteStringCollection();
                acscbnknm.AddRange(this.AutoComplete_BankName());
                txtBankName.AutoCompleteCustomSource = acscbnknm;
                txtBankName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBankName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscbnkcd = new AutoCompleteStringCollection();
                acscbnkcd.AddRange(this.AutoComplete_BankCode());
                txtBankCode.AutoCompleteCustomSource = acscbnkcd;
                txtBankCode.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtBankCode.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_BankName()
        {
            try
            {
                var banknamequery = from cs in db.Banks 
                                       select cs.BankName;
                return banknamequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_BankCode()
        {
            try
            {
                var bankcodequery = from cs in db.Banks
                                       select cs.BankCode;
                return bankcodequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string NextBankCode()
        {
            try
            {
                var cn = (from c in db.Banks
                          orderby c.BankCode descending
                          select c).FirstOrDefault();
                if (cn == null)
                    return "0";
                return cn.BankCode.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return "0";
            }
        }
        private void txtBankCode_KeyDown(object sender, KeyEventArgs e)
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
        private void txtBankCode_KeyPress(object sender, KeyPressEventArgs e)
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
