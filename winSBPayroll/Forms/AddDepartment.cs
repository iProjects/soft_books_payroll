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
    public partial class AddDepartment : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public AddDepartment(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (is_Validate())
            {
                try
                {

                    Department _Department = new Department();
                    if (!string.IsNullOrEmpty(txtShortCode.Text))
                    {
                        _Department.Code = txtShortCode.Text.Trim().ToUpper();
                    }
                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {
                        _Department.Description = Utils.ConvertFirstLetterToUpper(txtDescription.Text.Trim());
                    }
                    _Department.IsDeleted = false;

                    if (db.Departments.Any(c => c.Code == _Department.Code && c.IsDeleted == false))
                    {
                        MessageBox.Show("Code Exist!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.Departments.Any(c => c.Code == _Department.Code && c.IsDeleted == false))
                    {
                        db.Departments.AddObject(_Department);
                        db.SaveChanges();

                        Departments f = (Departments)this.Owner;
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
            if (string.IsNullOrEmpty(txtShortCode.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtShortCode, "Code cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtDescription, "Description cannot be null!");
                return false;
            }
            return no_error;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddDepartment_Load(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteStringCollection acscsrtcd = new AutoCompleteStringCollection();
                acscsrtcd.AddRange(this.AutoComplete_ShortCode());
                txtShortCode.AutoCompleteCustomSource = acscsrtcd;
                txtShortCode.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtShortCode.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                AutoCompleteStringCollection acscdscrptn = new AutoCompleteStringCollection();
                acscdscrptn.AddRange(this.AutoComplete_Description());
                txtDescription.AutoCompleteCustomSource = acscdscrptn;
                txtDescription.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtDescription.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_ShortCode()
        {
            try
            {
                var _codesquery = from bk in db.Departments
                                  where bk.IsDeleted == false
                                  select bk.Code;
                return _codesquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private string[] AutoComplete_Description()
        {
            try
            {
                var _descriptionquery = from bk in db.Departments
                                        where bk.IsDeleted == false
                                        select bk.Description;
                return _descriptionquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }





















    }
}