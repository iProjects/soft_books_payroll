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
    public partial class EditPayrollItem : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.PayrollItem pi;

        public EditPayrollItem(DAL.PayrollItem pitem, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            if (pitem == null)
                throw new ArgumentNullException("PayrollItem");
            pi = pitem;

        }

        private void EditPayrollItem_Load(object sender, EventArgs e)
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

                AutoCompleteStringCollection acsccls = new AutoCompleteStringCollection();
                acsccls.AddRange(this.AutoComplete_Description());
                txtPayrollItemId.AutoCompleteCustomSource = acsccls;
                txtPayrollItemId.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtPayrollItemId.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                InitializeControls();
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

        private void InitializeControls()
        {
            try
            {
                if (pi.Id != null)
                {
                    txtPayrollItemId.Text = pi.Id;
                }
                if (pi.ItemTypeId != null)
                {
                    cbo_item_types.SelectedValue = pi.ItemTypeId;
                }
                if (pi.TaxTrackingId != null)
                {
                    cbo_tax_trackings.SelectedValue = pi.TaxTrackingId.Trim();
                }
                if (pi.PayableTo != null)
                {
                    txtPayableTo.Text = pi.PayableTo;
                }
                if (pi.GLAccount != null)
                {
                    txtGlAccount.Text = pi.GLAccount;
                }
                if (pi.ReFField != null)
                {
                    txtReference.Text = pi.ReFField.ToString();
                }
                if (pi.Active != null)
                {
                    chkActive.Checked = pi.Active.Value;
                }
                if (pi.AddToPension != null)
                {
                    chkAddToPension.Checked = pi.AddToPension.Value;
                }
                if (pi.DefaultItem != null)
                {
                    chkIsDefault.Checked = pi.DefaultItem.Value;
                    chkIsDefault.Enabled = false;
                }
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
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (Is_PayrollItemValid())
            {
                try
                {
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
                    pi.AddToPension = chkAddToPension.Checked;
                    pi.Active = chkActive.Checked;

                    rep.UpdatePayrollItems(pi);

                    PayrollItemsForm f = (PayrollItemsForm)this.Owner;
                    f.RefreshGrid(1);
                    this.Close();
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
        public void DisableControls()
        {
            txtPayrollItemId.Enabled = false;
            cbo_item_types.Enabled = false;
            cbo_tax_trackings.Enabled = false;
            txtPayableTo.Enabled = false;
            txtGlAccount.Enabled = false;
            btnUpdate.Enabled = false;
            btnUpdate.Visible = false;
            txtReference.Enabled = false;
            chkActive.Enabled = false;
            chkAddToPension.Enabled = false;
            chkIsDefault.Enabled = false;
        }





    }
}