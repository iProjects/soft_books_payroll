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
using DAL.Criteria;

namespace winSBPayroll.Forms
{
    public partial class SearchBanksForm : Form
    {

        string connection;
        Repository rep;
        SBPayrollDBEntities db;
        static int index;
        List<Field> AccountFields = new List<Field>();
        CriteriaBuilder criteriaBuilder = new CriteriaBuilder();
        List<vwBankBranch> _BankBranches;
        //delegate
        public delegate void BankSelectHandler(object sender, BankSelectEventArgs e);
        //event
        public event BankSelectHandler OnBankListSelected;

        public SearchBanksForm(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;
            rep = new Repository(connection);
            db = new SBPayrollDBEntities(connection);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (dataGridViewBanks.SelectedRows.Count > 0)
            {
                try
                {

                    vwBankBranch selectedBankBranch = (vwBankBranch)bindingSourceBanks.Current;
                    OnBankListSelected(this, new BankSelectEventArgs(selectedBankBranch));

                    this.Close();

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void SearchBanksForm_Load(object sender, EventArgs e)
        {
            try
            {

                AccountFields.Add(new Field("banksortcode", "string"));
                AccountFields.Add(new Field("bankcode", "string"));
                AccountFields.Add(new Field("bankname", "string"));
                AccountFields.Add(new Field("branchcode", "string"));
                AccountFields.Add(new Field("branchname", "string"));

                cbField.DataSource = AccountFields;
                cbField.DisplayMember = "Name";
                cbField.ValueMember = "Name";

                cbOperator.DataSource = Op.GetList();
                cbOperator.DisplayMember = "Description";
                cbOperator.ValueMember = "Symbol";  

                lstCriteria.DataSource = criteriaBuilder.CriterionItemList();

                var _Banksquery = from b in db.Banks
                                  select b;
                List<Bank> _Banks = _Banksquery.ToList();


                DataGridViewComboBoxColumn colBank = new DataGridViewComboBoxColumn();
                colBank.HeaderText = "Bank";
                colBank.Name = "cbBank";
                colBank.DataSource = _Banks;
                colBank.DisplayMember = "BankName";
                colBank.DataPropertyName = "BankCode";
                colBank.ValueMember = "BankCode";
                colBank.MaxDropDownItems = 10;
                colBank.DisplayIndex = 4;
                colBank.MinimumWidth = 5;
                colBank.Width = 200;
                colBank.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                colBank.FlatStyle = FlatStyle.Flat;
                colBank.DefaultCellStyle.NullValue = "--- Select ---";
                colBank.ReadOnly = true;
                if (!this.dataGridViewBanks.Columns.Contains("cbBank"))
                {
                    dataGridViewBanks.Columns.Add(colBank);
                }

                var _BankBranchesquery = from bb in db.BankBranches
                                         select bb;
                List<BankBranch> _BankBranches = _BankBranchesquery.ToList();

                DataGridViewComboBoxColumn colBranch = new DataGridViewComboBoxColumn();
                colBranch.HeaderText = "Branch";
                colBranch.Name = "cbBranch";
                colBranch.DataSource = _BankBranches;
                colBranch.DisplayMember = "BranchName";
                colBranch.DataPropertyName = "BankSortCode";
                colBranch.ValueMember = "BankSortCode";
                colBranch.MaxDropDownItems = 10;
                colBranch.DisplayIndex = 3;
                colBranch.MinimumWidth = 5;
                colBranch.Width = 150;
                colBranch.FlatStyle = FlatStyle.Flat;
                colBranch.DefaultCellStyle.NullValue = "--- Select ---";
                colBranch.ReadOnly = true;
                if (!this.dataGridViewBanks.Columns.Contains("cbBranch"))
                {
                    dataGridViewBanks.Columns.Add(colBranch);
                }

                this.dataGridViewBanks.AutoGenerateColumns = false;
                this.dataGridViewBanks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtValue.Text))
            {
                CriterionItem cr = GetValidCriterionItem();
                if (cr != null)
                {
                    criteriaBuilder.AddCriterionItem(cr);
                    index++;
                }

                //refresh
                ListBoxRefresh();
            }
        }
        public void ListBoxRefresh()
        {
            lstCriteria.DataSource = null;
            lstCriteria.DataSource = criteriaBuilder.CriterionItemList();
        }
        private CriterionItem GetValidCriterionItem()
        {

            Field field = (Field)cbField.SelectedItem;
            Op Op = (Op)cbOperator.SelectedItem;
            string FValue = txtValue.Text;
            conjuction cj;
            string FieldType = field.Type;
            if (criteriaBuilder.IsFirstItem())
            {
                cj = conjuction.nil;
            }
            else
            {
                if (rbAnd.Checked)
                {
                    cj = conjuction.and;
                }
                else cj = conjuction.or;
            }
            switch (FieldType.ToLower())
            {
                case "string":
                    FValue = string.Format("{0}", FValue);
                    break;
                case "decimal":
                    decimal d;
                    if (!decimal.TryParse(FValue, out d))
                    {
                        lblMessage.Text = "Please enter a number in the field value";
                        return null;
                    }
                    break;
                case "date":
                    DateTime dd;
                    if (!DateTime.TryParse(FValue, out dd))
                    {
                        lblMessage.Text = "Please enter a date in the field value";
                        return null;
                    }
                    FValue = string.Format("{0}", FValue); //do a date format
                    break;
                case "like":
                    FValue = string.Format("{0}", FValue);
                    break;
            }
            //clean. no error
            Criterion cr = new Criterion(cj, field.Name, Op, FValue);
            return new CriterionItem("index" + index, cr);

        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstCriteria.SelectedItem != null)
            {
                CriterionItem selCriterionItem = (CriterionItem)lstCriteria.SelectedValue;
                criteriaBuilder.Remove(selCriterionItem);

                //refresh
                ListBoxRefresh();
            }
        }
        private void btnRetrieve_Click(object sender, EventArgs e)
        {

            try
            {
                //get search results
                _BankBranches = rep.GetBankBranchesFromCriteria(criteriaBuilder.CriterionItemList());
                bindingSourceBanks.DataSource = _BankBranches; 
                dataGridViewBanks.DataSource = bindingSourceBanks;
                groupBoxResults.Text = _BankBranches.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void dataGridViewBanks_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewBanks.SelectedRows.Count > 0)
            {
                try
                {

                    vwBankBranch selectedBankBranch = (vwBankBranch)bindingSourceBanks.Current;
                    OnBankListSelected(this, new BankSelectEventArgs(selectedBankBranch));

                    this.Close();

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }

        private void dataGridViewBanks_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            try
            {



            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }

    }



    public class BankSelectEventArgs : System.EventArgs
    {

        // add local member variables to hold text 
        private vwBankBranch _banksortcode;

        // class constructor
        public BankSelectEventArgs(vwBankBranch banksortcode)
        {

            this._banksortcode = banksortcode;
        }

        // Properties - Viewable by each listener 
        public vwBankBranch _BankSortCode
        {
            get
            {
                return _banksortcode;
            }
        }
    }

}
