using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class AddRolesForm : Form
    {
        #region "Private Fields"
        Repository rep;
        SBPayrollDBEntities db;
        string connection;
        #endregion "Private Fields"

        #region "Constructor"
        public AddRolesForm(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            rep = new Repository(connection);
            db = new SBPayrollDBEntities(connection);
        }
        #endregion "Constructor"

        #region "Private Methods"
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (IsRoleValid())
            {
                try
                {
                    spRole role = new spRole();
                    if (!string.IsNullOrEmpty(txtShortCode.Text))
                    {
                        role.ShortCode = Utils.ConvertFirstLetterToUpper(txtShortCode.Text);
                    }
                    if (!string.IsNullOrEmpty(txtDescription.Text))
                    {
                        role.Description = Utils.ConvertFirstLetterToUpper(txtDescription.Text);
                    }
                    role.IsDeleted = false;

                    if (db.spRoles.Any(i => i.ShortCode == role.ShortCode))
                    {
                        MessageBox.Show("Role Code Exist!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.spRoles.Any(i => i.ShortCode == role.ShortCode))
                    {
                        db.spRoles.AddObject(role);
                        db.SaveChanges();

                        RolesListForm rlf = (RolesListForm)this.Owner;
                        rlf.RefreshGrid();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        public bool IsRoleValid()
        {
            bool noerror = true;
            if (string.IsNullOrEmpty(txtShortCode.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtShortCode, "Short Code cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtDescription, "Description cannot be null!");
                return false;
            }
            return noerror;
        }
        private void AddRolesForm_Load(object sender, EventArgs e)
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
                var _codesquery = from bk in db.spRoles
                                  where bk.IsDeleted == false
                                  select bk.ShortCode;
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
                var _descriptionquery = from bk in db.spRoles
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
        #endregion "Private Methods"




    }
}