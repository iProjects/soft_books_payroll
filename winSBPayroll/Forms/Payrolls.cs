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
    public partial class Payrolls : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        MainForm MainForm;
        public DAL.UserModel user;
        List<DAL.Payroll> openPayrolls;

        public Payrolls(MainForm f, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            MainForm = f;
            user = f.LoggedInUser;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddPayroll f = new AddPayroll(user, connection) { Owner = this };
            f.ShowDialog();
        }

        private void Payrolls_Load(object sender, EventArgs e)
        {
            try
            {
                //initialize period
                cbYr.DataSource = de.GetPayrollYears();
                cbYr.DisplayMember = "Year";
                //cbYr.SelectedIndex = 0;

                dataGridViewPayrolls.AutoGenerateColumns = false;
                this.dataGridViewPayrolls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewPayrolls.DataSource = bindingSourcePayrolls;
                groupBox1.Text = bindingSourcePayrolls.Count.ToString();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }

        public void RefreshGrid()
        {
            try
            {

                openPayrolls = null;
                openPayrolls = de.GetPayrolls();
                cbYr.DataSource = null;
                cbYr.DataSource = de.GetPayrollYears();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }


        private void btnViewDetails_Click(object sender, EventArgs e)
        {

        }

        private void cbYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbYr.SelectedItem != null && openPayrolls != null)
            {
                int Year = (int)(cbYr.SelectedItem);
                string filter = "Year =" + Year;
                List<DAL.Payroll> filterd = (from l in openPayrolls
                                             where l.Year == Year
                                             select l).ToList();
                bindingSourcePayrolls.DataSource = filterd;
                groupBox1.Text = bindingSourcePayrolls.Count.ToString();
            }
            else
            {
                bindingSourcePayrolls.DataSource = openPayrolls;
                groupBox1.Text = bindingSourcePayrolls.Count.ToString();
            }
        }
    }
}