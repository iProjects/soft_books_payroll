//Here is the complete updated users_in_roles_form.cs code. It implements a custom overlay popup list box for substring (contains) autocomplete matching across both txtuser and txtrole, and updates RefreshGrid() to filter the grid using db.spUsers and db.spRoles lookup tables.
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
using System.Threading;

namespace winSBPayroll.Forms
{
    public partial class users_in_roles_form : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string user;
        public string TAG;

        // Event declaration for publishing messages
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        IQueryable<DAL.spUsersInRole> _UsersinRole;
        int recordCount = 0;

        // AutoComplete Data Collections
        private List<string> _userList = new List<string>();
        private List<string> _roleList = new List<string>();

        // Custom Overlay Controls
        private ListBox _listBoxAutoComplete;
        private TextBox _currentActiveTextBox;

        public users_in_roles_form(string _user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);

            TAG = this.GetType().Name;

            _notificationmessageEventname = notificationmessageEventname;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("Conn");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            user = _user;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished users_in_roles_form initialization", TAG));
        }

        private void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Log.Write_To_Log_File_temp_dir(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Log.Write_To_Log_File_temp_dir(ex);
            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
        }

        private void users_in_roles_form_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewdataGridView_UsersInRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewdataGridView_UsersInRoles.MultiSelect = false;
                dataGridViewdataGridView_UsersInRoles.ReadOnly = true;
                dataGridViewdataGridView_UsersInRoles.AutoGenerateColumns = false;

                var _usersquery = from usr in db.spUsers
                                  select usr;
                List<spUser> _Users = _usersquery.ToList();
                DataGridViewComboBoxColumn colUser = new DataGridViewComboBoxColumn();
                colUser.HeaderText = "User";
                colUser.Name = "colUser";
                colUser.DataSource = _Users;
                colUser.DisplayMember = "UserName";
                colUser.DataPropertyName = "UserId";
                colUser.ValueMember = "Id";
                colUser.MaxDropDownItems = 10;
                colUser.DisplayIndex = 0;
                colUser.MinimumWidth = 5;
                colUser.Width = 200;
                colUser.FlatStyle = FlatStyle.Flat;
                colUser.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colUser.DefaultCellStyle.NullValue = "--- Select ---";
                colUser.ReadOnly = true;
                if (!this.dataGridViewdataGridView_UsersInRoles.Columns.Contains("colUser"))
                {
                    dataGridViewdataGridView_UsersInRoles.Columns.Add(colUser);
                }

                var _rolesquery = from rls in db.spRoles
                                  select rls;
                List<spRole> _UserRoles = _rolesquery.ToList();
                DataGridViewComboBoxColumn colUserRole = new DataGridViewComboBoxColumn();
                colUserRole.HeaderText = "Role";
                colUserRole.Name = "cbUserRole";
                colUserRole.DataSource = _UserRoles;
                colUserRole.DisplayMember = "Description";
                colUserRole.DataPropertyName = "RoleId";
                colUserRole.ValueMember = "Id";
                colUserRole.MaxDropDownItems = 10;
                colUserRole.DisplayIndex = 1;
                colUserRole.MinimumWidth = 5;
                colUserRole.Width = 200;
                colUserRole.FlatStyle = FlatStyle.Flat;
                colUserRole.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colUserRole.DefaultCellStyle.NullValue = "--- Select ---";
                colUserRole.ReadOnly = true;
                if (!this.dataGridViewdataGridView_UsersInRoles.Columns.Contains("cbUserRole"))
                {
                    dataGridViewdataGridView_UsersInRoles.Columns.Add(colUserRole);
                }

                RefreshGrid();

                // Initialize list sources for custom matching
                _userList = (this.AutoComplete_user() ?? new string[0]).ToList();
                _roleList = (this.AutoComplete_role() ?? new string[0]).ToList();

                // Setup dynamic popup ListBox overlay
                InitializeAutoCompleteListBox();

                // Wire up contains-matching autocomplete controls
                AttachCustomAutoComplete(txtuser, _userList);
                AttachCustomAutoComplete(txtrole, _roleList);

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished users_in_roles_form load", TAG));
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        #region Custom Contains-Matching AutoComplete

        private void InitializeAutoCompleteListBox()
        {
            _listBoxAutoComplete = new ListBox
            {
                Visible = false,
                TabStop = false
            };

            this.Controls.Add(_listBoxAutoComplete);
            _listBoxAutoComplete.BringToFront();

            _listBoxAutoComplete.Click += (sender, e) => SelectAutoCompleteItem();
            _listBoxAutoComplete.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SelectAutoCompleteItem();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    _listBoxAutoComplete.Visible = false;
                }
            };
        }

        private void AttachCustomAutoComplete(TextBox txtBox, List<string> sourceList)
        {
            txtBox.AutoCompleteMode = AutoCompleteMode.None; // Disable default prefix matching

            txtBox.TextChanged += (sender, e) =>
            {
                // Live filter grid as text changes
                RefreshGrid();

                if (!txtBox.Focused) return;

                string filterText = txtBox.Text.Trim();
                if (string.IsNullOrEmpty(filterText))
                {
                    _listBoxAutoComplete.Visible = false;
                    return;
                }

                // Match substring anywhere in the text (case-insensitive)
                var matches = sourceList
                    .Where(x => !string.IsNullOrEmpty(x) && x.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0)
                    .Distinct()
                    .Take(20)
                    .ToList();

                if (matches.Any())
                {
                    _currentActiveTextBox = txtBox;
                    _listBoxAutoComplete.DataSource = matches;

                    Point location = txtBox.Parent.PointToScreen(txtBox.Location);
                    Point formLocation = this.PointToClient(location);

                    _listBoxAutoComplete.Left = formLocation.X;
                    _listBoxAutoComplete.Top = formLocation.Y + txtBox.Height;
                    _listBoxAutoComplete.Width = txtBox.Width;
                    _listBoxAutoComplete.Height = Math.Min(120, (matches.Count * 18) + 10);
                    _listBoxAutoComplete.Visible = true;
                    _listBoxAutoComplete.BringToFront();
                }
                else
                {
                    _listBoxAutoComplete.Visible = false;
                }
            };

            txtBox.KeyDown += (sender, e) =>
            {
                if (_listBoxAutoComplete.Visible)
                {
                    if (e.KeyCode == Keys.Down)
                    {
                        if (_listBoxAutoComplete.SelectedIndex < _listBoxAutoComplete.Items.Count - 1)
                            _listBoxAutoComplete.SelectedIndex++;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (_listBoxAutoComplete.SelectedIndex > 0)
                            _listBoxAutoComplete.SelectedIndex--;
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Enter)
                    {
                        SelectAutoCompleteItem();
                        e.Handled = true;
                    }
                    else if (e.KeyCode == Keys.Escape)
                    {
                        _listBoxAutoComplete.Visible = false;
                        e.Handled = true;
                    }
                }
            };

            txtBox.Leave += (sender, e) =>
            {
                if (!_listBoxAutoComplete.Focused)
                {
                    _listBoxAutoComplete.Visible = false;
                }
            };
        }

        private void SelectAutoCompleteItem()
        {
            if (_listBoxAutoComplete.SelectedItem != null && _currentActiveTextBox != null)
            {
                _currentActiveTextBox.Text = _listBoxAutoComplete.SelectedItem.ToString();
                _currentActiveTextBox.SelectionStart = _currentActiveTextBox.Text.Length;
                _listBoxAutoComplete.Visible = false;

                RefreshGrid();
            }
        }

        #endregion

        private string[] AutoComplete_user()
        {
            try
            {
                var users_query = from en in db.spUsers
                                  where en.IsDeleted == false
                                  where en.Locked == false
                                  select en.UserName;
                return users_query.ToArray();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                return null;
            }
        }

        private string[] AutoComplete_role()
        {
            try
            {
                var rolequery = from en in db.spRoles
                                where en.IsDeleted == false
                                select en.Description;
                return rolequery.ToArray();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                return null;
            }
        }

        public void RefreshGrid()
        {
            try
            {
                bindingSource_UsersInRoles.DataSource = null;

                string userFilter = txtuser.Text.Trim();
                string roleFilter = txtrole.Text.Trim();

                // Join spUsersInRoles with spUsers and spRoles to perform contains filtering on UserName and Description
                var _UsersInRolesquery = from uir in db.spUsersInRoles
                                         join usr in db.spUsers on uir.UserId equals usr.Id into usrGroup
                                         from usr in usrGroup.DefaultIfEmpty()
                                         join rls in db.spRoles on uir.RoleId equals rls.Id into rlsGroup
                                         from rls in rlsGroup.DefaultIfEmpty()
                                         select new { uir, usr, rls };

                if (!string.IsNullOrEmpty(userFilter))
                {
                    _UsersInRolesquery = _UsersInRolesquery.Where(x => x.usr != null && x.usr.UserName != null && x.usr.UserName.Contains(userFilter));
                }

                if (!string.IsNullOrEmpty(roleFilter))
                {
                    _UsersInRolesquery = _UsersInRolesquery.Where(x => x.rls != null && x.rls.Description != null && x.rls.Description.Contains(roleFilter));
                }

                var filteredList = _UsersInRolesquery.Select(x => x.uir).ToList();

                bindingSource_UsersInRoles.DataSource = filteredList;
                dataGridViewdataGridView_UsersInRoles.DataSource = bindingSource_UsersInRoles;
                groupBox3.Text = bindingSource_UsersInRoles.Count.ToString();

                if (dataGridViewdataGridView_UsersInRoles.Rows.Count > 0)
                {
                    int nRowIndex = dataGridViewdataGridView_UsersInRoles.Rows.Count - 1;
                    dataGridViewdataGridView_UsersInRoles.Rows[nRowIndex].Selected = true;
                    bindingSource_UsersInRoles.Position = nRowIndex;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewdataGridView_UsersInRoles.SelectedRows.Count != 0)
            {
                try
                {
                    spUsersInRole userinrole = (spUsersInRole)bindingSource_UsersInRoles.Current;
                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete User Role", "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        db.spUsersInRoles.DeleteObject(userinrole);
                        db.SaveChanges();
                        RefreshGrid();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewdataGridView_UsersInRoles_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
            }
        }
    }
}

