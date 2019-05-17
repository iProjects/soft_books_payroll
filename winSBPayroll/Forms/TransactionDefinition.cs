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
    public partial class TransactionDefinition : Form
    {
        DataEntry de ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public TransactionDefinition(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void TransactionDefinition_Load(object sender, EventArgs e)
        {
            bindingSource1.DataSource = de.TxnDefList();
            dataGridView1.DataSource = bindingSource1;
            dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            cbPayrollItem.DataSource = de.GetActivePayrollItems();
            cbPayrollItem.DisplayMember = "ItemId";
            cbPayrollItem.ValueMember = "ItemId";

            txtTxncode.DataBindings.Add(new Binding("Text", bindingSource1, "TxnCode"));
            //rbAddUpdate.DataBindings.Add(new Binding("Checked", bindingSourcePayrolls, "DataEntry"));
            cbPayrollItem.DataBindings.Add(new Binding("Text", bindingSource1, "PayrollItem"));
            amountTextBox.DataBindings.Add(new Binding("Text", bindingSource1, "DefaultAmount"));
            enabledCheckBox.DataBindings.Add(new Binding("Checked", bindingSource1, "Enabled"));
            recurrentCheckBox.DataBindings.Add(new Binding("Checked", bindingSource1, "Recurrent"));
            trackCheckBox.DataBindings.Add(new Binding("Checked", bindingSource1, "TrackYTD"));
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string dataentry;
            if (rbAddUpdate.Checked)
            {
                dataentry = "A";
            }
            else dataentry = "D";

            if (is_validate())
            {

                errorProvider1.Clear();

                // Write Coding to add into database
                try
                {
                    de.CreateTxnDef(txtTxncode.Text,
                        dataentry,
                        cbPayrollItem.Text,
                        decimal.Parse(amountTextBox.Text),
                        enabledCheckBox.Checked,
                        recurrentCheckBox.Checked,
                        trackCheckBox.Checked);

                    //Refresh
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = de.TxnDefList();
                    tabPageList.Focus();

                    //Clear the screen
                    txtTxncode.Text = "";
                    amountTextBox.Text = "";
                    enabledCheckBox.Checked = true;
                    recurrentCheckBox.Checked = true;
                    trackCheckBox.Checked = false;

                    MessageBox.Show("Data Successfully Added");

                    this.Close();
                    TransactionDefinition f = (TransactionDefinition)this.Owner;
                    f.RefreshGrid();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        public void RefreshGrid()
        {
            bindingSource1.DataSource = null;
            bindingSource1.DataSource = de.TxnDefList();


        }

        private void btnClode_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private bool is_validate()
        {

            bool no_error = true;

            if (txtTxncode.Text == string.Empty)
            {

                errorProvider1.SetError(txtTxncode, "Text Missing");

                return false;

            }

            // Clear all Error Messages


           decimal i;
           bool ret = decimal.TryParse(amountTextBox.Text, out i);
           if (!ret )
           {
               errorProvider1.Clear(); // Clear all Error Messages
               errorProvider1.SetError(amountTextBox, "Enter a valid Default Amount");
               return false;
           }


            if (cbPayrollItem.Text == string.Empty)
            {
                errorProvider1.Clear(); // Clear all Error Messages
                errorProvider1.SetError(cbPayrollItem, "Select a valid payroll Item");
                return false;

            }


            return no_error;

        }

        private void btnApply_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string dataentry;
            if (rbAddUpdate.Checked)
            {
                dataentry = "A";
            }
            else dataentry = "D";

            if (is_validate())
            {

                errorProvider1.Clear();

                // Write Coding to add into database
                try
                {
                    de.CreateTxnDef(txtTxncode.Text,
                        dataentry,
                        cbPayrollItem.Text,
                        decimal.Parse(amountTextBox.Text),
                        enabledCheckBox.Checked,
                        recurrentCheckBox.Checked,
                        trackCheckBox.Checked);

                    //Refresh
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = de.TxnDefList();
                    tabPageList.Focus();

                    //Clear the screen
                    txtTxncode.Text = "";
                    amountTextBox.Text = "";
                    enabledCheckBox.Checked = true;
                    recurrentCheckBox.Checked = true;
                    trackCheckBox.Checked = false;

                    MessageBox.Show("Data Successfully Updated");

                    this.Close();
                    TransactionDefinition f = (TransactionDefinition)this.Owner;
                    f.RefreshGrid();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
          
        } 
    }
}
