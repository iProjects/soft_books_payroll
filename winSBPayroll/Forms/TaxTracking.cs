using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.DataEntry;
using DAL;
using CommonLib;

namespace winSBPayroll.Forms
{
    public partial class TaxTracking : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public TaxTracking(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void TaxTracking_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewTaxTracking.AutoGenerateColumns = false;
                bindingSource1.DataSource = de.ListTaxTracking();
                dataGridViewTaxTracking.DataSource = bindingSource1;
                this.dataGridViewTaxTracking.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
                bindingSource1.DataSource = null;
                bindingSource1.DataSource = de.ListTaxTracking();
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



    }
}