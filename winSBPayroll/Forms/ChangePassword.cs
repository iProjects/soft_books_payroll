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
                    txtUserId.Text = _user.UserName;
                    txtUserId.Enabled = false;
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
                    if (!string.IsNullOrEmpty(txtNewPassword.Text))
                    {
                        _user.Password = txtNewPassword.Text.Trim();
                    }

                    rep.ChangePassword(_user);

                    MessageBox.Show("Password Changed Successfully!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
            if (string.IsNullOrEmpty(txtOldPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtOldPassword, "Old Password cannot be null!");
                return false;
            }
            if (!string.IsNullOrEmpty(txtOldPassword.Text))
            {
                //check if we are dealing with an authentic guy.
                string mesage = string.Empty;
                string errCode = string.Empty;
                bool auth = rep.Authenticate(txtUserId.Text.Trim(), txtOldPassword.Text.Trim(), 1, 1, ref mesage, ref errCode);
                if (!auth)
                {
                    MessageBox.Show("Incorrect Password!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorProvider1.Clear();
                    errorProvider1.SetError(txtOldPassword, "Incorrect Password!");
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtNewPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtNewPassword, "New Password cannot be null!");
                return false;
            }
            decimal passwordsize;
            if (!decimal.TryParse(rep.SettingLookup("PWDSIZE"), out passwordsize))
            {
                MessageBox.Show("Cannot retrieve Password Size from Settings . See your Administrator!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (txtNewPassword.Text.Trim().Length < passwordsize)
            {
                MessageBox.Show("Password length must be more than [ " + passwordsize + " ] characters!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                errorProvider1.Clear();
                errorProvider1.SetError(txtNewPassword, "Password length must be more than [ " + passwordsize + " ] characters!");
                return false;
            }
            if (string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtConfirmPassword, "Confirm Passsword cannnot be null!");
                return false;
            }
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password must Match!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                errorProvider1.Clear();
                errorProvider1.SetError(txtConfirmPassword, "Password must Match!");
                return false;
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

    
