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
    public partial class PackTransaction : Form
    {
        DataEntry de  ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        List<string> EmpNos;

        public PackTransaction(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        //Implementation
        private void SetEmpNos(List<string> emps)
        {
            EmpNos = emps;
            if (EmpNos.Count() > 4)
            {
                txtEmployeeNos.Text = string.Join(";", EmpNos.Take(4)) + "...";
            }
            else
            {
                txtEmployeeNos.Text = string.Join(";", EmpNos);
            }
        }

        private void PackTransaction_Load(object sender, EventArgs e)
        {
            cbTxnCode.DataSource = de.TxnDefList();
            cbTxnCode.ValueMember = "TxnCode";
            cbTxnCode.DisplayMember = "TxnCode";
            //bindingSourcePayrolls.DataSource = de.packedTransactionList();
            //listBox1.DataSource = bindingSourcePayrolls;
            
            //dtpPostDate.DataBindings.Add(new Binding("value",bindingSourcePayrolls,"Post Date"));
            //txtEmployeeNo.DataBindings.Add(new Binding("text", bindingSourcePayrolls, "Employee"));
            //cbTxnCode.DataBindings.Add(new Binding("name", bindingSourcePayrolls, "Txn Code"));
            //txtAmount.DataBindings.Add(new Binding("text", bindingSourcePayrolls, "Amount"));
            EmpNos = new List<string>();

            //g .DataSource 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = de.GetPackedTxnList();
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //vALIDATE
            if (is_validate())
            {

                try
                {
                    foreach (string emp in EmpNos)
                    {
                        de.CreatePackedTxn(
                            DateTime.Parse(dtpPostDate.Text),
                            emp,
                            cbTxnCode.Text.ToString(),
                            decimal.Parse(txtAmount.Text));

                        EmpNos = new List<string>();
                        txtAmount.Text = "";
                        txtEmployeeNos.Text = "";
                        this.Close();
                        ListBoxRefresh();
                    }
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

            DateTime datetoday = DateTime.Today;

            if (this.dtpPostDate.Value > datetoday)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(dtpPostDate, "Post  Date cannot be Beyond Today!");
                return false;
            }

            if (this.txtEmployeeNos.Text == string.Empty)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtEmployeeNos, "Empty Employee Number not allowed");
                return false;
            }

            if (this.cbTxnCode.Text == string.Empty)
            {
                errorProvider1.Clear(); //Clear all Error Messages
                errorProvider1.SetError(cbTxnCode, "Select a valid  Item");
                return false;
            }

            if (this.txtAmount.Text == string.Empty)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtAmount, "Empty Amount not allowed");
                return false;
            }

            return no_error;

        }


        private void ListBoxRefresh()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = de.GetPackedTxnList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            Forms.SearchEmployeeForm f = new SearchEmployeeForm(connection);
            f.OnEmployeeListSelected += new SearchEmployeeForm.EmployeeSelectHandler(HandleSelectedEmployeeList);
            f.Show();
        }

        //hander
        private void HandleSelectedEmployeeList(object sender, EmployeeSelectEventArgs e)
        { 
            //SetEmpNos(e._Employee);
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

        }

        private void btnPack_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (string emp in EmpNos)
                {
                    de.CreatePackedTxn(
                        DateTime.Parse(dtpPostDate.Text),
                        emp,
                        cbTxnCode.Text.ToString(),
                        decimal.Parse(txtAmount.Text));
                }
            }
            catch (Exception ex)
            {
                string msg = "ErrorMsg = " + ex.Message;
                if (ex.InnerException != null) msg += "\nExtended ErrorMsg = " + ex.InnerException.Message;
                MessageBox.Show(msg);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {

        }

        private void cbTxnCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
