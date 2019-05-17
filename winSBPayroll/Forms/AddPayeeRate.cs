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
    public partial class AddPayeeRate : Form
    {
        DataEntry de  ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public AddPayeeRate(string Conn)
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (is_Validate())
            {
                try
                {
                    de.AddPayeeRate(
                       decimal.Parse(txtFromAmt.Text.Trim()),
                        decimal.Parse(txtToAmt.Text.Trim()),
                        decimal.Parse(txtRate.Text.Trim())
                        );
                    
                    PayeeRates f = (PayeeRates)this.Owner;
                    f.RefreshGrid();
                    this.Close();
                }

                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }

        }

        public bool is_Validate()
        {
            bool no_error = true;

            if (txtFromAmt.Text == string.Empty)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtFromAmt, "Empty From Amount not allowed!");
                return false;
            }

            if (txtToAmt.Text == string.Empty)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtToAmt, "Empty To Amount not allowed!");
                return false;
            }

            if (txtRate.Text == string.Empty)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtRate, "Empty Rate not allowed!");
                return false;
            }

            decimal frompay;
            if (!decimal.TryParse(txtFromAmt.Text, out frompay))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtFromAmt, "Enter a valid From Amount!");
                return false;
            }

            decimal topay;
            if (!decimal.TryParse(txtToAmt.Text,out topay))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtToAmt, "Enter a valid To Amount!");
                return false;
            }

            decimal ratepay;
            if (!decimal.TryParse(txtRate.Text, out ratepay))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtRate, "Enter a valid Rate!");
                return false;
            }           
             

            return no_error;
        }

        private void AddPayeeRate_Load(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

       

        
    }
}
