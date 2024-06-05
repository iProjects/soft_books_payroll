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
    public partial class AddUserForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public AddUserForm(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }


        private void AddUSer_Load(object sender, EventArgs e)
        {
            try
            {
                txtPhoto.Enabled = false;
                var admnrls = from rl in db.spRoles
                              select rl;

                List<spRole> adminRoles = admnrls.ToList();
                cboRole.DataSource = adminRoles;
                cboRole.ValueMember = "Id";
                cboRole.DisplayMember = "Description";
                cboRole.SelectedIndex = -1;

                var gender = new BindingList<KeyValuePair<string, string>>();
                gender.Add(new KeyValuePair<string, string>("M", "Male"));
                gender.Add(new KeyValuePair<string, string>("F", "Female"));
                cboGender.DataSource = gender;
                cboGender.ValueMember = "Key";
                cboGender.DisplayMember = "Value";
                cboGender.SelectedIndex = -1;

                var informby = new BindingList<KeyValuePair<string, string>>();
                informby.Add(new KeyValuePair<string, string>("SMS", "Sms"));
                informby.Add(new KeyValuePair<string, string>("EMAIL", "Email"));
                cboInformBy.DataSource = informby;
                cboInformBy.ValueMember = "Key";
                cboInformBy.DisplayMember = "Value";
                cboInformBy.SelectedIndex = -1;

                imgUserPhoto.SizeMode = PictureBoxSizeMode.StretchImage;

                AutoCompleteStringCollection acscdscrptn = new AutoCompleteStringCollection();
                acscdscrptn.AddRange(this.AutoComplete_UserName());
                txtUserName.AutoCompleteCustomSource = acscdscrptn;
                txtUserName.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtUserName.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_UserName()
        {
            try
            {
                var usernamequery = from bk in db.spUsers
                                    where bk.Locked == false
                                    select bk.UserName;
                return usernamequery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        private void btnAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsUserValid())
            {
                try
                {
                    spUser _user = new spUser();

                    if (!string.IsNullOrEmpty(txtUserName.Text))
                    {
                        _user.UserName = txtUserName.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtPassword.Text))
                    {
                        _user.Password = txtPassword.Text.Trim();
                    }
                    if (cboRole.SelectedIndex != -1)
                    {
                        _user.RoleId = int.Parse(cboRole.SelectedValue.ToString());
                    }
                    if (!string.IsNullOrEmpty(txtSurName.Text))
                    {
                        _user.Surname = txtSurName.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtOtherNames.Text))
                    {
                        _user.OtherNames = txtOtherNames.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtNationalId.Text))
                    {
                        _user.NationalID = txtNationalId.Text.Trim();
                    }
                    _user.DateOfBirth = dtpDOB.Value;
                    if (cboGender.SelectedIndex != -1)
                    {
                        _user.Gender = cboGender.SelectedValue.ToString();
                    }
                    if (cboInformBy.SelectedIndex != -1)
                    {
                        _user.InformBy = cboInformBy.SelectedValue.ToString();
                    }
                    if (!string.IsNullOrEmpty(txtEmail.Text))
                    {
                        _user.Email = txtEmail.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtTelephone.Text))
                    {
                        _user.Telephone = txtTelephone.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtPhoto.Text))
                    {
                        _user.Photo = txtPhoto.Text.Trim();
                    }
                    _user.Locked = chkLocked.Checked;
                    _user.IsDeleted = false;
                    _user.SystemId = "ws";
                    _user.Status = "A";
                    _user.DateJoined = DateTime.Now;

                    string salt = Utils.create_random_salt();
                    string salted_password = salt + _user.Password;
                    string password_salt_hash = Utils.get_SHA512_hash(salted_password);

                    _user.password_hash = password_salt_hash;
                    _user.password_salt = salt;

                    _user.Password = Utils.encrypt_string(_user.Password);

                    if (db.spUsers.Any(i => i.UserName == _user.UserName))
                    {
                        MessageBox.Show("User Name Exist!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.spUsers.Any(i => i.UserName == _user.UserName))
                    {
                        db.spUsers.AddObject(_user);
                        db.SaveChanges();

                        UsersForm f = (UsersForm)this.Owner;
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
        public bool IsUserValid()
        {
            bool no_error = true;
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                errorProvider1.SetError(txtUserName, "User Name cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.SetError(txtPassword, "Password cannot be null!");
                no_error = false;
            }
            if (cboRole.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboRole, "Select Role!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtSurName.Text))
            {
                errorProvider1.SetError(txtSurName, "SurName cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtOtherNames.Text))
            {
                errorProvider1.SetError(txtOtherNames, "OtherNames cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtNationalId.Text))
            {
                errorProvider1.SetError(txtNationalId, "National Id cannot be null!");
                no_error = false;
            }
            if (cboGender.SelectedIndex == -1)
            {
                errorProvider1.SetError(cboGender, "Select Gender!");
                no_error = false;
            }
            return no_error;
        }
        private void btnUploadPhoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Title = "select an Image File";

            openFileDialog1.Filter = "Image File (*.jpg)|*.jpg|Image File (*.gif)|*.gif|Image File (*.png)|*.png|All files (*.*)|*.*";

            openFileDialog1.ShowDialog();

            string strFileName = openFileDialog1.FileName;

            try
            {
                UploadUserPhoto(strFileName);
                MessageBox.Show("Upload completed successfully", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error during upload." + Environment.NewLine + "Error details are:  " + ex.Message, Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);

                MessageBox.Show("Upload incomplete", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void UploadUserPhoto(string strFileName)
        {
            txtPhoto.Text = string.Empty;
            string strFileType = System.IO.Path.GetExtension(strFileName).ToString().ToLower();

            //check file exists
            if (!System.IO.File.Exists(strFileName))
            {
                MessageBox.Show("Error Loading Photo." + Environment.NewLine + " File Does not Exist!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Check file type
            if (strFileType != ".jpg" && strFileType != ".gif" && strFileType != ".png")
            {
                throw new Exception("File Type not Image");
            }
            //display  image
            if (strFileType.Trim() == ".jpg")
            {
                imgUserPhoto.ImageLocation = strFileName;
                txtPhoto.Text = strFileName;
                txtPhoto.SelectAll();
            }
            else if (strFileType.Trim() == ".gif")
            {
                imgUserPhoto.ImageLocation = strFileName;
                txtPhoto.Text = strFileName;
                txtPhoto.SelectAll();
            }
            else if (strFileType.Trim() == ".png")
            {
                imgUserPhoto.ImageLocation = strFileName;
                txtPhoto.Text = strFileName;
                txtPhoto.SelectAll();
            }
        }
        private void txtPhoto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar)) e.Handled = true;
            if (Char.IsLetter(e.KeyChar)) e.Handled = true;
            if (Char.IsNumber(e.KeyChar)) e.Handled = true;
            if (Char.IsPunctuation(e.KeyChar)) e.Handled = true;
            if (Char.IsSurrogate(e.KeyChar)) e.Handled = true;
            if (Char.IsSymbol(e.KeyChar)) e.Handled = true;
            if (Char.IsWhiteSpace(e.KeyChar)) e.Handled = true;
            if (e.KeyChar == (char)Keys.Back) e.Handled = true;
            if (e.KeyChar == (char)Keys.Space) e.Handled = true;
            if (e.KeyChar == (char)Keys.Delete) e.Handled = true;
            if (e.KeyChar == (char)Keys.Clear) e.Handled = true;
        }



    }
}