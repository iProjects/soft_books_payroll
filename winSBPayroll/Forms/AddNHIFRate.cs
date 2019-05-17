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
    public partial class AddNHIFRate : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public AddNHIFRate(string Conn)
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
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (is_Validate())
            {
                try
                {
                    de.AddNHIFRate(
                       decimal.Parse(txtFromAmount.Text.Trim()),
                        decimal.Parse(txtToAmount.Text.Trim()),
                        decimal.Parse(txtRate.Text.Trim()));

                    NHIFRates f = (NHIFRates)this.Owner;
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
            if (txtFromAmount.Text == string.Empty)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtFromAmount, "Empty From Amount not allowed!");
                return false;
            }
            if (txtToAmount.Text == string.Empty)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtToAmount, "Empty To Amount not allowed!");
                return false;
            }
            if (txtRate.Text == string.Empty)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtRate, "Empty Rate not allowed!");
                return false;
            }
            decimal frompay;
            if (!decimal.TryParse(txtFromAmount.Text, out frompay))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtFromAmount, "Enter a valid From Amount!");
                return false;
            }
            decimal topay;
            if (!decimal.TryParse(txtToAmount.Text, out topay))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtToAmount, "Enter a valid To Amount!");
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











    }
}
