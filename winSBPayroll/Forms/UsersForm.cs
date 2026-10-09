//Standard WinForms TextBox.AutoCompleteCustomSource only matches strings that start with the typed text (prefix matching) and cannot be configured natively for substring/contains matching (%string%).
//To achieve matching at any position (contains matching) in WinForms, the recommended approach is to handle the TextChanged event dynamically using a ListBox popup overlay (or ToolStripDropDown) to display matching options.
//Here is the updated UsersForm.cs using a dynamic popup list box approach:
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
    public partial class UsersForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string user;
        public string TAG;

        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        IQueryable<DAL.spUser> _Users;
        int recordCount = 0;

        // Collections to hold autocomplete data source lists
        private List<string> _userNameList = new List<string>();
        private List<string> _surnameList = new List<string>();
        private List<string> _otherNamesList = new List<string>();

        // Controls for custom autocomplete popup
        private ListBox _listBoxAutoComplete;
        private TextBox _currentActiveTextBox;

        public UsersForm(string _user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished UsersForm initialization", TAG));
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

        private void Users_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewUsers.MultiSelect = false;
                dataGridViewUsers.ReadOnly = true;
                dataGridViewUsers.AutoGenerateColumns = false;

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
                colUserRole.DisplayIndex = 4;
                colUserRole.MinimumWidth = 5;
                colUserRole.Width = 150;
                colUserRole.FlatStyle = FlatStyle.Flat;
                colUserRole.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                colUserRole.DefaultCellStyle.NullValue = "--- Select ---";
                colUserRole.ReadOnly = true;
                if (!this.dataGridViewUsers.Columns.Contains("cbUserRole"))
                {
                    dataGridViewUsers.Columns.Add(colUserRole);
                }

                RefreshGrid();

                // Initialize data lists for searching
                _userNameList = this.AutoComplete_UserName().ToList();
                _surnameList = this.AutoComplete_SurNames().ToList();
                _otherNamesList = this.AutoComplete_OtherNames().ToList();

                // Setup dynamic popup ListBox
                InitializeAutoCompleteListBox();

                // Wire up custom autocomplete handlers for textboxes
                AttachCustomAutoComplete(txtusername, _userNameList);
                AttachCustomAutoComplete(txtsurname, _surnameList);
                AttachCustomAutoComplete(txtothernames, _otherNamesList);

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished UsersForm load", TAG));
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        #region Custom Autocomplete Logic

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
            txtBox.AutoCompleteMode = AutoCompleteMode.None; // Disable native autocomplete

            txtBox.TextChanged += (sender, e) =>
            {
                // Re-filter the DataGridView
                RefreshGrid();

                if (!txtBox.Focused) return;

                string filterText = txtBox.Text.Trim();
                if (string.IsNullOrEmpty(filterText))
                {
                    _listBoxAutoComplete.Visible = false;
                    return;
                }

                // Filter matches at ANY position (contains matching, case-insensitive)
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
                // Hide popup when focus leaves unless clicking the ListBox
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

                // Refresh grid after selecting an option
                RefreshGrid();
            }
        }

        #endregion

        private string[] AutoComplete_UserName()
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
                return new string[0];
            }
        }

        private string[] AutoComplete_SurNames()
        {
            try
            {
                var users_query = from en in db.spUsers
                                  where en.IsDeleted == false
                                  where en.Locked == false
                                  select en.Surname;
                return users_query.ToArray();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                return new string[0];
            }
        }

        private string[] AutoComplete_OtherNames()
        {
            try
            {
                var users_query = from en in db.spUsers
                                  where en.IsDeleted == false
                                  where en.Locked == false
                                  select en.OtherNames;
                return users_query.ToArray();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                return new string[0];
            }
        }

        //Here is the updated RefreshGrid() method, along with how to trigger it when text input changes in the autocomplete textboxes (txtusername, txtsurname, and txtothernames).
        public void RefreshGrid()
        {
            try
            {
                bindingSourceUsers.DataSource = null;

                //// Retrieve current filter values from textboxes
                string userNameFilter = txtusername.Text.Trim();
                string surnameFilter = txtsurname.Text.Trim();
                string otherNamesFilter = txtothernames.Text.Trim();

                // Query database base dataset
                var _usersquery = from us in db.spUsers
                                  where us.IsDeleted == false
                                  select us;

                // Filter by UserName (contains search)
                if (!string.IsNullOrEmpty(userNameFilter))
                {
                    _usersquery = _usersquery.Where(us => us.UserName != null && us.UserName.Contains(userNameFilter));
                }

                // Filter by Surname (contains search)
                if (!string.IsNullOrEmpty(surnameFilter))
                {
                    _usersquery = _usersquery.Where(us => us.Surname != null && us.Surname.Contains(surnameFilter));
                }

                // Filter by OtherNames (contains search)
                if (!string.IsNullOrEmpty(otherNamesFilter))
                {
                    _usersquery = _usersquery.Where(us => us.OtherNames != null && us.OtherNames.Contains(otherNamesFilter));
                }

                bindingSourceUsers.DataSource = _usersquery.ToList();
                dataGridViewUsers.DataSource = bindingSourceUsers;
                groupBox3.Text = bindingSourceUsers.Count.ToString();

                // Select the last row if results exist
                if (dataGridViewUsers.Rows.Count > 0)
                {
                    int nRowIndex = dataGridViewUsers.Rows.Count - 1;
                    dataGridViewUsers.Rows[nRowIndex].Selected = true;
                    bindingSourceUsers.Position = nRowIndex;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.spUser user = (DAL.spUser)bindingSourceUsers.Current;
                    Forms.EditUserForm f = new Forms.EditUserForm(user, connection) { Owner = this };
                    f.Text = user.UserName.ToString().Trim().ToUpper();
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.AddUserForm au = new Forms.AddUserForm(connection) { Owner = this };
                au.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewUsers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.spUser user = (DAL.spUser)bindingSourceUsers.Current;
                    Forms.EditUserForm f = new Forms.EditUserForm(user, connection) { Owner = this };
                    f.Text = user.UserName.ToString().Trim().ToUpper();
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void dataGridViewUsers_DataError(object sender, DataGridViewDataErrorEventArgs e)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count != 0)
            {
                try
                {
                    spUser user = (spUser)bindingSourceUsers.Current;
                    if (DialogResult.Yes == MessageBox.Show("Are you sure you want to delete User\n" + user.UserName.ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                    {
                        db.spUsers.DeleteObject(user);
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
    }
}

//Key Changes:
// * Disabled Default Native AutoComplete: Set txtBox.AutoCompleteMode = AutoCompleteMode.None; to override standard prefix matching.
// * IndexOf String Comparison: Used x.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0 to match text occurring at any position in the string.
// * Dynamic Dropdown Overlay: Added a floating ListBox dynamically rendered beneath the focused text box with keyboard navigation (Up, Down, Enter, Escape) support.
