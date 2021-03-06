﻿using System;
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
    public partial class EditBank : Form
    {
        DataEntry de  ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        Bank _bank;
        // Boolean flag used to determine when a character other than a number is entered.
        private bool nonNumberEntered = false;

        public EditBank(Bank bank, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            if (bank == null)
                throw new ArgumentNullException("Bank");
            _bank = bank;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditBank_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControls();

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
        private void InitializeControls()
        {
            try
            {
                if (_bank.BankCode != null)
                {
                    txtBankCode.Text = _bank.BankCode.ToString();
                    txtBankCode.Enabled = false;
                }
                if (_bank.BankName != null)
                {
                    txtBankName.Text = _bank.BankName;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (is_validate())
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtBankName.Text))
                    {
                        _bank.BankName = Utils.ConvertFirstLetterToUpper(txtBankName.Text.Trim());
                    }

                    rep.UpdateBank(_bank);

                    BanksForm b = (BanksForm)this.Owner;
                    b.RefreshBankGrid();
                    this.Close();
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
