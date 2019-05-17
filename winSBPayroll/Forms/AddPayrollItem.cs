using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class AddPayrollItem : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;


        public AddPayrollItem(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Is_PayrollItemValid())
            {
                try
                {
                    DAL.PayrollItem pi = new DAL.PayrollItem();
                    if (!string.IsNullOrEmpty(txtPayrollItemId.Text))
                    {
                        pi.Id = Utils.ConvertFirstLetterToUpper(txtPayrollItemId.Text.Trim());
                        pi.Description = Utils.ConvertFirstLetterToUpper(txtPayrollItemId.Text.Trim());
                    }
                    if (cbItemType.SelectedIndex != -1)
                    {
                        pi.ItemTypeId = cbItemType.SelectedValue.ToString();
                    }
                    if (cbTaxTracking.SelectedIndex != -1)
                    {
                        pi.TaxTrackingId = cbTaxTracking.SelectedValue.ToString();
                    }
                    if (!string.IsNullOrEmpty(txtPayableTo.Text))
                    {
                        pi.PayableTo = Utils.ConvertFirstLetterToUpper(txtPayableTo.Text.ToString().Trim());
                    }
                    if (!string.IsNullOrEmpty(txtGlAccount.Text))
                    {
                        pi.GLAccount = txtGlAccount.Text.Trim();
                    } 
                    int reference;
                    if (!string.IsNullOrEmpty(txtReference.Text) && int.TryParse(txtReference.Text, out reference))
                    {
                        pi.ReFField = int.Parse(txtReference.Text.Trim());
                    }
                    pi.Enable = true;
                    pi.Active = chkActive.Checked;
                    pi.AddToPension = chkAddToPension.Checked;
                    pi.DefaultItem = false;
                    pi.IsDeleted = false;

                    if (db.PayrollItems.Any(i => i.Id == pi.Id && i.IsDeleted==false))
                    {
                        MessageBox.Show("Item Description Exist!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.PayrollItems.Any(i => i.Id == pi.Id && i.IsDeleted==false))
                    {
                        db.AddToPayrollItems(pi);
                        db.SaveChanges();

                        Forms.PayrollItems f = (Forms.PayrollItems)this.Owner;
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

        private bool Is_PayrollItemValid()
        {
            bool no_error = true;
            if (string.IsNullOrEmpty(txtPayrollItemId.Text))
            {
                errorProvider1.Clear();
                errorProvider1.SetError(txtPayrollItemId, "Description cannot be null");
                return false;
            }
            if (cbItemType.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cbItemType, "Select Item Type");
                return false;
            }
            if (cbTaxTracking.SelectedIndex == -1)
            {
                errorProvider1.Clear();
                errorProvider1.SetError(cbTaxTracking, "Select  Tax Tracking");
                return false;
            }
            return no_error;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void AddPayrollItem_Load(object sender, EventArgs e)
        {
            try
            {
                var itemtypequery = from it in db.PayrollItemTypes
                                    select it;
                cbItemType.DataSource = itemtypequery.ToList();
                cbItemType.ValueMember = "Id";
                cbItemType.DisplayMember = "Description";
                cbItemType.SelectedIndex = -1;

                var taxtrackingquery = from tt in db.TaxTrackings
                                       select tt;
                cbTaxTracking.DataSource = taxtrackingquery.ToList();
                cbTaxTracking.ValueMember = "Id";
                cbTaxTracking.DisplayMember = "Description";
                cbTaxTracking.SelectedIndex = -1;

                AutoCompleteStringCollection acsccls = new AutoCompleteStringCollection();
                acsccls.AddRange(this.AutoComplete_Description());
                txtPayrollItemId.AutoCompleteCustomSource = acsccls;
                txtPayrollItemId.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtPayrollItemId.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string[] AutoComplete_Description()
        {
            try
            {
                var  descriptionquery = from cs in db.PayrollItems
                                        where cs.IsDeleted == false
                                        where cs.Active== true
                                             select cs.Description;
                return descriptionquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void cbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> pensionItems = new List<string>() { "SALARY", "ADDITION" };
                DAL.PayrollItemType pi;
                if (cbItemType.SelectedItem != null)
                {
                    pi = (DAL.PayrollItemType)cbItemType.SelectedItem;
                    if (pensionItems.Contains(pi.Id.Trim()))
                    {
                        chkAddToPension.Enabled = true;
                    }
                    else
                    {
                        chkAddToPension.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }



    }
}