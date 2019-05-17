using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BLL.DataEntry;
using CommonLib;

namespace winSBPayroll.Forms
{
    public partial class EditDepartment : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.Department _department;

        public EditDepartment(DAL.Department department, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _department = department;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (is_Validate())
                {
                    if (!string.IsNullOrEmpty(txtShortCode.Text))
                    {
                        _department.Code = Utils.ConvertFirstLetterToUpper(txtShortCode.Text.Trim());
                    }
                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {
                        _department.Description = Utils.ConvertFirstLetterToUpper(txtDescription.Text.Trim());
                    }

                    rep.UpdateDepartment(_department);

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
        public bool is_Validate()
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
        private void EditDepartment_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControls();

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
        private void InitializeControls()
        {
            try
            {
                if (_department.Code != null)
                {
                    txtShortCode.Text = _department.Code.Trim();
                }
                if (_department.Description != null)
                {
                    txtDescription.Text = _department.Description.Trim();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }




    }
}