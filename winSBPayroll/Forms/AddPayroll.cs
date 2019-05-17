using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;
using System.Linq;

namespace winSBPayroll.Forms
{

    public partial class AddPayroll : Form
    {

        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        DAL.UserModel _User;

        public AddPayroll(DAL.UserModel user, string Conn)
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (is_Validate())
            {
                try
                {
                    Payroll payroll = new Payroll();
                    payroll.Period = int.Parse(cboPeriod.SelectedValue.ToString());
                    payroll.Year = int.Parse(txtYear.Text.Trim());
                    payroll.EmployerId = int.Parse(cbEmployer.SelectedValue.ToString());
                    payroll.DateRun = DateTime.Parse(txtDateRun.Text.Trim());
                    payroll.RunBy = _User.UserName;
                    payroll.Approved = false;
                    payroll.ApprovedBy = "";
                    payroll.IsOpen = true;
                    payroll.Processed = false;

                    if (rep.GetPayrolls().Any(i => i.Period == payroll.Period && i.Year == payroll.Year))
                    {
                        MessageBox.Show("Payroll Exists!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!rep.GetPayrolls().Any(i => i.Period == payroll.Period && i.Year == payroll.Year))
                    {
                        rep.AddPayroll(payroll);

                        Payrolls f = (Payrolls)this.Owner;
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
        private bool is_Validate()
        {
            bool no_error = true;
            if (cboPeriod.SelectedIndex == -1)
            {
                errorProvider1.Clear(); //clear all Error Messages
                errorProvider1.SetError(cboPeriod, "Select Period!");
                return false;
            }
            if (string.IsNullOrEmpty(txtYear.Text))
            {
                errorProvider1.Clear(); //clear all Error Messages
                errorProvider1.SetError(txtYear, "Year cannot be null!");
                return false;
            }
            int yr;
            if (!int.TryParse(txtYear.Text.Trim(), out yr))
            {
                errorProvider1.Clear(); //clear all Error Messages
                errorProvider1.SetError(txtYear, "Year must be an Integer!");
                return false;
            }
            if (cbEmployer.SelectedIndex == -1)
            {
                errorProvider1.Clear(); //Clear all Error Messages
                errorProvider1.SetError(cbEmployer, "Select Employer!");
                return false;
            }
            //if (string.IsNullOrEmpty(txtDateRun.Text))
            //{
            //    errorProvider1.Clear(); //clear all Error Messages
            //    errorProvider1.SetError(txtDateRun, "Date Run cannot be null!");
            //    return false;
            //}
            //DateTime daterun = DateTime.Parse(txtDateRun.Text);
            //DateTime datenow = DateTime.Today;
            //if (daterun < datenow)
            //{
            //    errorProvider1.Clear(); //Clear all Error Messages
            //    errorProvider1.SetError(txtDateRun, "Date Run must be greater than Today!");
            //    return false;
            //}
            if (string.IsNullOrEmpty(txtRunBy.Text))
            {
                errorProvider1.Clear(); //clear all Error Messages
                errorProvider1.SetError(txtRunBy, "Run By cannot be null!");
                return false;
            }
            return no_error;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddPayroll_Load(object sender, EventArgs e)
        {
            try
            {
                txtRunBy.Text = _User.UserName;
                txtDateRun.Text = DateTime.Today.ToShortDateString();

                cbEmployer.ValueMember = "Id";
                cbEmployer.DisplayMember = "Name";
                cbEmployer.DataSource = de.GetEmployers();

                var months = new BindingList<KeyValuePair<int, string>>();
                months.Add(new KeyValuePair<int, string>(1, "January"));
                months.Add(new KeyValuePair<int, string>(2, "February"));
                months.Add(new KeyValuePair<int, string>(3, "March"));
                months.Add(new KeyValuePair<int, string>(4, "April"));
                months.Add(new KeyValuePair<int, string>(5, "May"));
                months.Add(new KeyValuePair<int, string>(6, "June"));
                months.Add(new KeyValuePair<int, string>(7, "July"));
                months.Add(new KeyValuePair<int, string>(8, "August"));
                months.Add(new KeyValuePair<int, string>(9, "September"));
                months.Add(new KeyValuePair<int, string>(10, "October"));
                months.Add(new KeyValuePair<int, string>(11, "November"));
                months.Add(new KeyValuePair<int, string>(12, "December"));

                cboPeriod.DataSource = months;
                cboPeriod.ValueMember = "Key";
                cboPeriod.DisplayMember = "Value";
                cboPeriod.SelectedValue = DateTime.Today.Month;

                txtYear.Text = DateTime.Today.Year.ToString();

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);

            }
        }






    }
}