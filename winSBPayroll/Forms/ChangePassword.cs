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
    public partial class ChangePasswordForm : Form
    {
        Repository rep;
        string connection;
        MainForm _mainform;
        UserModel _user;
        //delegate
        public delegate void userDTOHandler(object sender, userDTOEventArgs e);
        //event
        public event userDTOHandler OnuserDTOSelected;

        public ChangePasswordForm(UserModel user, MainForm mainform, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            rep = new Repository(connection);

            if (user == null)
                throw new ArgumentNullException("User");
            _user = user;

            if (mainform == null)
                throw new ArgumentNullException("MainForm");
            _mainform = mainform;


        }

        private void ChangePasswordForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (_user != null)
                {
                    txtusername.Text = _user.UserName;
                    txtusername.Enabled = false;
                }
                txtNewPassword.Text = string.Empty;
                txtConfirmPassword.Text = string.Empty;
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (is_Validate())
            {
                try
                {
                    _user.Password = txtNewPassword.Text.Trim();

                    string salt = Utils.create_random_salt();
                    string salted_password = salt + _user.Password;
                    string password_salt_hash = Utils.get_SHA512_hash(salted_password);

                    _user.password_hash = password_salt_hash;
                    _user.password_salt = salt;

                    _user.Password = Utils.encrypt_string(_user.Password);

                    rep.ChangePassword(_user);

                    MessageBox.Show("Password Changed Successfully!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    OnuserDTOSelected(this, new userDTOEventArgs(_user));

                    this.Close();

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
            if (string.IsNullOrEmpty(txtusername.Text))
            {
                errorProvider1.SetError(txtusername, "User Name cannot be null!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtOldPassword.Text))
            {
                errorProvider1.SetError(txtOldPassword, "Old Password cannot be null!");
                no_error = false;
            }
            if (!string.IsNullOrEmpty(txtOldPassword.Text))
            {
                //check if we are dealing with an authentic user.

                string username = txtusername.Text;
                string password = txtOldPassword.Text;

                var user_that_exists = rep.GetUserbyUserName(username);
                if (user_that_exists != null)
                {
                    if (user_that_exists.Locked)
                    {
                        Utils.ShowError(new Exception("User is locked. \nplease contact the administrator."));
                        no_error = false;
                        return false;
                    }

                    string salt = user_that_exists.password_salt;
                    string salted_password = salt + password;
                    string password_salt_hash = Utils.get_SHA512_hash(salted_password);

                    string hash_from_db = user_that_exists.password_hash;

                    bool is_hash_equal = hash_from_db.Equals(password_salt_hash);

                    if (is_hash_equal)
                    {

                    }
                    else
                    {
                        Utils.ShowError(new Exception("incorrect password. \nif you forgot your password please contact the administrator."));
                        no_error = false;
                        return false;
                    }
                }
                else
                {
                    Utils.ShowError(new Exception("user [ " + username + " ] does not exist."));
                    no_error = false;
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                errorProvider1.SetError(txtNewPassword, "New Password cannot be null!");
                no_error = false;
            }
            decimal passwordsize;
            if (!decimal.TryParse(rep.SettingLookup("PWDSIZE"), out passwordsize))
            {
                MessageBox.Show("Cannot retrieve Password Size from Settings . See your Administrator!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                no_error = false;
            }
            if (txtNewPassword.Text.Trim().Length < passwordsize)
            {
                MessageBox.Show("New Password length must be more than [ " + passwordsize + " ] characters!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                errorProvider1.SetError(txtNewPassword, "New Password length must be more than [ " + passwordsize + " ] characters!");
                no_error = false;
            }
            if (string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                errorProvider1.SetError(txtConfirmPassword, "Confirm Passsword cannnot be null!");
                no_error = false;
            }
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password must Match!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                errorProvider1.SetError(txtConfirmPassword, "Password must Match!");
                no_error = false;
            }
            return no_error;
        }
    }

    public class userDTOEventArgs : System.EventArgs
    {
        UserModel _user;

        public userDTOEventArgs(UserModel user)
        {
            _user = user;
        }

        public UserModel _USer
        {
            get
            {
                return _user;
            }
        }
    }
}


