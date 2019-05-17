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
    public partial class AddEmployeeAdvance : Form
    {
        DataEntry de  ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _User;


        public AddEmployeeAdvance(string user, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
            _User = user;
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEmployeeAdvance_Load(object sender, EventArgs e)
        {
            cboEmployee.DataSource = rep.GetAllActiveEmployees();
            cboEmployee.DisplayMember = "Surname";
            cboEmployee.ValueMember = "EmpNo";
            cboEmployee.SelectedIndex = -1;

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (is_validate())
            {
                try
                {
                de.AddEmployeeAdvance(DateTime.Today,
                           cboEmployee.SelectedValue.ToString(),
                           "ADVANCE",
                           decimal.Parse(txtAmount.Text.Trim()),
                           false,
                           true,
                           false,
                           false,
                           _User,
                           _User,
                           DateTime.Today,
                           "",
                           DateTime.Today);

                this.Close();
                }
                catch(Exception ex)
                {
                   Utils.ShowError(ex);
                }
            }
        }
                   
      

        private bool is_validate()
        {
            bool no_error = true;

            if (string.IsNullOrEmpty(txtAmount.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmount, "Amount cannot be null!");
                return false;
            }

            if (cboEmployee.SelectedValue==null)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboEmployee, "Select an Employee!");
                return false;
            }

            decimal amt;
            if (!decimal.TryParse(txtAmount.Text,out amt))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmount, "Amount must be decimal!");
                return false;
            }

            return no_error;
        }

        
    }
}
