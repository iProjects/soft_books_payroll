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
    public partial class AddEmployeeLoan : Form
    {
        DataEntry de ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;

        public AddEmployeeLoan(string user, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _user = user; 
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEmployeeLoan_Load(object sender, EventArgs e)
        {
            try
            {
            var loantype = new BindingList<KeyValuePair<string, string>>();
            loantype.Add(new KeyValuePair<string, string>("NORMAL", "NORMAL"));
            loantype.Add(new KeyValuePair<string, string>("DEVELOPMENT", "DEVELOPMENT"));
            loantype.Add(new KeyValuePair<string, string>("SCHOOL", "SCHOOL"));
            loantype.Add(new KeyValuePair<string, string>("EMERGENCY", "EMERGENCY"));
            cboLoanType.DataSource = loantype;
            cboLoanType.ValueMember = "Key";
            cboLoanType.DisplayMember = "Value";

            cboEmployee.DataSource = rep.GetAllActiveEmployees();
            cboEmployee.DisplayMember = "Surname";
            cboEmployee.ValueMember = "EmpNo";
            cboEmployee.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (is_validate())
            {
                try
                {
                    de.AddEmployeeLoan(
                        DateTime.Today,
                        cboEmployee.SelectedValue.ToString(),
                        "LOAN",
                        decimal.Parse(txtAmoundeductable.Text.Trim()),
                        chkRecurrent.Checked,
                        true,
                        false,
                        chkTrackYTD.Checked,
                        _user,
                        _user,
                        DateTime.Today,
                        "",
                        DateTime.Today,
                        decimal.Parse(txtAmountBorrowed.Text.Trim()),
                        cboLoanType.SelectedValue.ToString());

                    this.Close();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }


        private bool is_validate()
        {
            bool no_error = true;

            if (cboEmployee.SelectedValue == null)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboEmployee, "Select an Employee!");
                return false;
            }

            
            if (cboLoanType.SelectedValue == null)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cboLoanType, "Select  Loan Type!");
                return false;
            }

            if (txtAmountBorrowed.Text == null)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmountBorrowed, "Amount Borrowed cannot be null!");
                return false;
            }

            decimal amtbor;
            if (!decimal.TryParse(txtAmountBorrowed.Text, out amtbor))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmountBorrowed, "Amount Borrowed must be  decimal!");
                return false;
            }

            if (txtAmoundeductable.Text == null)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmoundeductable, "Amount Deductable cannot be null!");
                return false;
            }

            decimal amtded;
            if (!decimal.TryParse(txtAmoundeductable.Text, out amtded))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmoundeductable, "Amount Deductable must be  decimal!");
                return false;
            }

            return no_error;
        }
   

    }
}
