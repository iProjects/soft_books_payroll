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
        private void AddPayrollItem_Load(object sender, EventArgs e)
        {
            try
            {
                var itemtype_query = from it in db.PayrollItemTypes
                                     select it;
                List<DAL.PayrollItemType> _lst_itemtypes = itemtype_query.ToList();
                _lst_itemtypes.Insert(0, new DAL.PayrollItemType { Id = "", Description = "---Select---" });
                cbo_item_types.DataSource = _lst_itemtypes.ToList();
                cbo_item_types.ValueMember = "Id";
                cbo_item_types.DisplayMember = "Description";
                cbo_item_types.SelectedIndex = 0;

                var taxtracking_query = from tt in db.TaxTrackings
                                        select tt;
                List<DAL.TaxTracking> _lst_taxtrackings = taxtracking_query.ToList();
                _lst_taxtrackings.Insert(0, new DAL.TaxTracking { Id = "", Description = "---Select---" });
                cbo_tax_trackings.DataSource = _lst_taxtrackings.ToList();
                cbo_tax_trackings.ValueMember = "Id";
                cbo_tax_trackings.DisplayMember = "Description";
                cbo_tax_trackings.SelectedIndex = 0;

                txtGlAccount.Text = "SB General Ledger Account";
                txtPayableTo.Text = "Employee";
                txtReference.Text = "100";

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
                var descriptionquery = from cs in db.PayrollItems
                                       where cs.IsDeleted == false
                                       where cs.Active == true
                                       select cs.Description;
                return descriptionquery.ToArray();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
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
                    if (cbo_item_types.SelectedIndex != -1)
                    {
                        pi.ItemTypeId = cbo_item_types.SelectedValue.ToString();
                    }
                    if (cbo_tax_trackings.SelectedIndex != -1)
                    {
                        pi.TaxTrackingId = cbo_tax_trackings.SelectedValue.ToString();
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
                    pi.Active = chkActive.Checked;
                    pi.AddToPension = chkAddToPension.Checked;
                    pi.DefaultItem = chkdefault.Checked;
                    pi.Enable = true;
                    pi.IsDeleted = false;

                    if (db.PayrollItems.Any(i => i.Id == pi.Id && i.IsDeleted == false))
                    {
                        MessageBox.Show("Item Description Exist!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (!db.PayrollItems.Any(i => i.Id == pi.Id && i.IsDeleted == false))
                    {
                        db.AddToPayrollItems(pi);
                        db.SaveChanges();

                        Forms.PayrollItemsForm f = (Forms.PayrollItemsForm)this.Owner;
                        f.RefreshGrid(1);
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
                errorProvider.Clear();
                errorProvider.SetError(txtPayrollItemId, "Description cannot be null");
                return false;
            }
            if (cbo_item_types.SelectedIndex == 0)
            {
                errorProvider.Clear();
                errorProvider.SetError(cbo_item_types, "Select Item Type");
                return false;
            }
            if (cbo_tax_trackings.SelectedIndex == 0)
            {
                errorProvider.Clear();
                errorProvider.SetError(cbo_tax_trackings, "Select  Tax Tracking");
                return false;
            }
            if (string.IsNullOrEmpty(txtPayableTo.Text))
            {
                errorProvider.Clear();
                errorProvider.SetError(txtPayableTo, "Payable To cannot be null");
                return false;
            }
            if (string.IsNullOrEmpty(txtGlAccount.Text))
            {
                errorProvider.Clear();
                errorProvider.SetError(txtGlAccount, "General Ledger cannot be null");
                return false;
            }
            if (string.IsNullOrEmpty(txtReference.Text))
            {
                errorProvider.Clear();
                errorProvider.SetError(txtReference, "Reference cannot be null");
                return false;
            }
            return no_error;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> pensionItems = new List<string>() { "SALARY", "ADDITION" };
                DAL.PayrollItemType pi;
                if (cbo_item_types.SelectedItem != null)
                {
                    pi = (DAL.PayrollItemType)cbo_item_types.SelectedItem;
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