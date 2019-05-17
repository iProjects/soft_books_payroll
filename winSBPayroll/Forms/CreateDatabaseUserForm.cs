using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLib;

namespace winSBPayroll.Forms
{
    public partial class CreateDatabaseUserForm : Form
    {
        //delegate
        public delegate void DatabaseUserHandler(object sender, DatabaseUserEventArgs e);
        //event
        public event DatabaseUserHandler OnDatabaseUserSelected;

        public CreateDatabaseUserForm()
        {
            InitializeComponent();
        }

        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void btnCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (IsUserValid())
            {                
                    try
                    {
                        string _UserName = txtUserName.Text;
                        string _Password = txtPassword.Text;

                        OnDatabaseUserSelected(this, new DatabaseUserEventArgs(_UserName, _Password));
                        
                        this.Close();

                    }
                    catch (Exception ex)
                    {
                        Utils.ShowError(ex);
                    }
                 
            }
        }
        private bool IsUserValid()
        {
            bool noerror = true;
            if(string.IsNullOrEmpty(txtUserName.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtUserName, "User Name cannot be null!");
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPassword, "Password cannot be null!");
                return false;
            }
            return noerror;
        }
        private void CreateDatabaseUserForm_Load(object sender, EventArgs e)
        {

        }
    }

    public class DatabaseUserEventArgs : System.EventArgs
    {
        // add local member variables to hold text  
        private string _username;
        private string  _password;

        // class constructor
        public DatabaseUserEventArgs(string username,string password)
        {
            this._username = username;
            this._password = password;
        }

        // Properties - Viewable by each listener 
        public string _UserName
        {
            get
            {
                return _username;
            }
        }
        public string _Password
        {
            get
            {
                return _password;
            }
        }
    }
}
