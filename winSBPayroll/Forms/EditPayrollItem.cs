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

                InitializeControls();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
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
                    cbItemType.SelectedValue = pi.ItemTypeId;
                }
                if (pi.TaxTracking != null)
                {
                    cbTaxTracking.SelectedValue = pi.TaxTrackingId.Trim();
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
                    pi.AddToPension = chkAddToPension.Checked;
                    pi.Active = chkActive.Checked;

                    rep.UpdatePayrollItems(pi);

                    PayrollItems f = (PayrollItems)this.Owner;
                    f.RefreshGrid();
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
        public void DisableControls()
        {
            txtPayrollItemId.Enabled = false;
            cbItemType.Enabled = false;
            cbTaxTracking.Enabled = false;
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