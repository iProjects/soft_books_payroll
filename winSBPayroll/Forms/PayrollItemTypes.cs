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
    public partial class PayrollItemTypes : Form
    {
        DataEntry de ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public PayrollItemTypes(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PayrollItemTypes_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AutoGenerateColumns = false;
                this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.DataSource = de.PayrooItemTypeList();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            
            }
            

        }
    }
}
