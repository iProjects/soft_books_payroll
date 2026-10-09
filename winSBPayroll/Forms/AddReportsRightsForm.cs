using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class AddReportsRightsForm : Form
    {

        #region "Private Fields"
        Repository rep;
        SBPayrollDBEntities db;
        string connection; 
        #endregion "Private Fields"

        #region "Constructor"
        public AddReportsRightsForm(string Conn)
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
         
        #endregion "Private Methods"

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsRightValid())
            {
                try
                {
                    spAllowedReportsRolesMenu _right = new spAllowedReportsRolesMenu();
                    if (cboRoles.SelectedIndex != -1)
                    {
                        _right.RoleId = int.Parse(cboRoles.SelectedValue.ToString());
                    }
                    if (cboMenuItem.SelectedIndex != -1)
                    {
                        _right.MenuItemId = int.Parse(cboMenuItem.SelectedValue.ToString());
                    }
                    _right.Allowed = chkAllowed.Checked;


                    if (db.spAllowedReportsRolesMenus.Any(i => i.RoleId == _right.RoleId && i.MenuItemId == _right.MenuItemId))
                    {
                        MessageBox.Show("Right Exist!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.spAllowedReportsRolesMenus.Any(i => i.RoleId == _right.RoleId && i.MenuItemId == _right.MenuItemId))
                    {
                        db.spAllowedReportsRolesMenus.AddObject(_right);
                        db.SaveChanges();

                        ReportsRightsListForm f = (ReportsRightsListForm)this.Owner;
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

        private bool IsRightValid()
        {
            bool noerror = true;
            if (cboRoles.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboRoles, "Select Role!");
                return false;
            }
            if (cboMenuItem.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboMenuItem, "Select Menu Item!");
                return false;
            }
            return noerror;
        } 

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddRightsForm_Load(object sender, EventArgs e)
        {
            try
            {
                var _Rolesquery = from rl in db.spRoles
                                  select rl;
                List<spRole> _Roles = _Rolesquery.ToList();
                cboRoles.DataSource = _Roles;
                cboRoles.ValueMember = "Id";
                cboRoles.DisplayMember = "Description";
                cboRoles.SelectedIndex = -1;

                var _menuItemsquery = (from rl in db.spReportsMenuItems
                                       select rl).OrderBy(i => i.Description);
                List<spReportsMenuItem> _MenuItems = _menuItemsquery.ToList();
                cboMenuItem.DataSource = _MenuItems;
                cboMenuItem.ValueMember = "Id";
                cboMenuItem.DisplayMember = "Description";
                cboMenuItem.SelectedIndex = -1;
            }
            catch (Exception ex)
            {

                Utils.ShowError(ex);

            }
        }
         
  
    }
}
