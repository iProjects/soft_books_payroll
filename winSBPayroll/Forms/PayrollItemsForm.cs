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
using System.Data.SqlClient;

namespace winSBPayroll.Forms
{
    public partial class PayrollItemsForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        public string TAG;
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        IQueryable<DAL.PayrollItem> _lst_payroll_items;
        int recordCount = 0;

        public PayrollItemsForm(string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished PayrollItemsForm initialization", TAG));
        }

        private void PayrollItems_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPayrollItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewPayrollItems.MultiSelect = false;
                dataGridViewPayrollItems.ReadOnly = true;
                dataGridViewPayrollItems.AutoGenerateColumns = false;

                var itemtypesquery = from it in db.PayrollItemTypes
                                     select it;
                DataGridViewComboBoxColumn colCboxItemType = new DataGridViewComboBoxColumn();
                colCboxItemType.HeaderText = "Item Type";
                colCboxItemType.Name = "cbItemType";
                colCboxItemType.DataSource = itemtypesquery.ToList();
                // The display member is the name column in the column datasource  
                colCboxItemType.DisplayMember = "Description";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxItemType.DataPropertyName = "ItemTypeId";
                // The value member is the primary key of the parent table  
                colCboxItemType.ValueMember = "Id";
                colCboxItemType.MaxDropDownItems = 10;
                colCboxItemType.Width = 140;
                colCboxItemType.DisplayIndex = 1;
                colCboxItemType.MinimumWidth = 5;
                colCboxItemType.FlatStyle = FlatStyle.Flat;
                colCboxItemType.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxItemType.ReadOnly = true;
                if (!this.dataGridViewPayrollItems.Columns.Contains("cbItemType"))
                {
                    dataGridViewPayrollItems.Columns.Add(colCboxItemType);
                }

                var taxtrackingquery = from tt in db.TaxTrackings
                                       select tt;
                DataGridViewComboBoxColumn colCboxTaxTracking = new DataGridViewComboBoxColumn();
                colCboxTaxTracking.HeaderText = "Tax Tracking";
                colCboxTaxTracking.Name = "cbTaxTracking";
                colCboxTaxTracking.DataSource = taxtrackingquery.ToList();
                // The display member is the name column in the column datasource  
                colCboxTaxTracking.DisplayMember = "Description";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxTaxTracking.DataPropertyName = "TaxTrackingId";
                // The value member is the primary key of the parent table  
                colCboxTaxTracking.ValueMember = "Id";
                colCboxTaxTracking.MaxDropDownItems = 10;
                colCboxTaxTracking.Width = 140;
                colCboxTaxTracking.DisplayIndex = 2;
                colCboxTaxTracking.MinimumWidth = 5;
                colCboxTaxTracking.FlatStyle = FlatStyle.Flat;
                colCboxTaxTracking.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxTaxTracking.ReadOnly = true;
                if (!this.dataGridViewPayrollItems.Columns.Contains("cbTaxTracking"))
                {
                    dataGridViewPayrollItems.Columns.Add(colCboxTaxTracking);
                }

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
                txtdescription.AutoCompleteCustomSource = acsccls;
                txtdescription.AutoCompleteMode =
                    AutoCompleteMode.SuggestAppend;
                txtdescription.AutoCompleteSource =
                     AutoCompleteSource.CustomSource;

                this.RefreshGrid(1);

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished PayrollItemsForm load", TAG));

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

        int NO_OF_RECORDS_PER_PAGE = 10;
        int NO_OF_PAGES_TO_DISPLAY = 10;

        public void RefreshGrid(int pageIndex)
        {
            try
            {
                //NO_OF_RECORDS_PER_PAGE = int.Parse(rep.get_setting_value_given_key("NO_OF_RECORDS_PER_PAGE"));
                int no_of_records_per_page_from_db = rep.get_no_of_records_per_page();
                NO_OF_RECORDS_PER_PAGE = no_of_records_per_page_from_db;
                //NO_OF_PAGES_TO_DISPLAY = int.Parse(System.Configuration.ConfigurationManager.AppSettings["NO_OF_PAGES_TO_DISPLAY"]);
                int no_of_pages_to_display_from_db = rep.get_no_of_pages_to_display();
                NO_OF_PAGES_TO_DISPLAY = no_of_pages_to_display_from_db;

                string[] conn_str_arr = connection.Split(';');
                string data_source = string.Empty;
                string data_base = string.Empty;
                string integrated_security = string.Empty;

                for (int i = 0; i < conn_str_arr.Length; i++)
                {
                    if (i == 2)
                    {
                        string[] data_source_arr = conn_str_arr[2].Split('=');
                        data_source = data_source_arr[2];
                    }
                    if (i == 3)
                    {
                        string[] data_base_arr = conn_str_arr[3].Split('=');
                        data_base = data_base_arr[1];
                    }
                    if (i == 4)
                    {
                        string[] integrated_security_arr = conn_str_arr[4].Split('=');
                        integrated_security = integrated_security_arr[1];
                    }
                }

                string constring = @"Data Source=" + data_source + @";Initial Catalog=" + data_base + @";Integrated Security=" + integrated_security;

                using (SqlConnection con = new SqlConnection(constring))
                {
                    using (SqlCommand cmd = new SqlCommand("GetPayrollItemsPageWise", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
                        cmd.Parameters.AddWithValue("@PageSize", NO_OF_RECORDS_PER_PAGE);
                        cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4);
                        cmd.Parameters["@RecordCount"].Direction = ParameterDirection.Output;
                        
                        // Description Filter
                        object description = string.IsNullOrWhiteSpace(txtdescription.Text)
                            ? DBNull.Value
                            : (object)txtdescription.Text.Trim();
                        cmd.Parameters.AddWithValue("@Description", description);

                        // ItemTypeId Filter
                        object itemTypeId = (cbo_item_types.SelectedIndex > 0 && cbo_item_types.SelectedValue != null && !string.IsNullOrEmpty(cbo_item_types.SelectedValue.ToString()))
                            ? (object)cbo_item_types.SelectedValue.ToString()
                            : DBNull.Value;
                        cmd.Parameters.AddWithValue("@ItemTypeId", itemTypeId);

                        // TaxTrackingId Filter
                        object taxTrackingId = (cbo_tax_trackings.SelectedIndex > 0 && cbo_tax_trackings.SelectedValue != null && !string.IsNullOrEmpty(cbo_tax_trackings.SelectedValue.ToString()))
                            ? (object)cbo_tax_trackings.SelectedValue.ToString()
                            : DBNull.Value;
                        cmd.Parameters.AddWithValue("@TaxTrackingId", taxTrackingId);

                        if (chkdefault.Checked)
                        {
                            cmd.Parameters.AddWithValue("@DefaultItem", true);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@DefaultItem", DBNull.Value);
                        }

                        con.Open();
                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());
                        convert_datatable_into_list(dt);
                        BindGrid();
                        con.Close();
                        recordCount = Convert.ToInt32(cmd.Parameters["@RecordCount"].Value);
                        this.PopulatePager(recordCount, pageIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void PopulatePager(int recordCount, int currentPage)
        {
            List<Page> pages = new List<Page>();
            int startIndex, endIndex;

            //Calculate the Start and End Index of pages to be displayed.
            double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(NO_OF_RECORDS_PER_PAGE));
            int pageCount = (int)Math.Ceiling(dblPageCount);
            if (pageCount < 1)
            {
                pageCount = 1;
            }

            // Center the visible window of page buttons around currentPage,
            // then clamp it so it never runs past 1 or pageCount.
            startIndex = currentPage - (NO_OF_PAGES_TO_DISPLAY / 2);
            if (startIndex < 1)
            {
                startIndex = 1;
            }

            endIndex = startIndex + NO_OF_PAGES_TO_DISPLAY - 1;
            if (endIndex > pageCount)
            {
                endIndex = pageCount;
                startIndex = endIndex - NO_OF_PAGES_TO_DISPLAY + 1;
                if (startIndex < 1)
                {
                    startIndex = 1;
                }
            }

            //Add the First Page Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "First", Value = "1" });
            }

            //Add the Previous Button.
            if (currentPage > 1)
            {
                pages.Add(new Page { Text = "<<", Value = (currentPage - 1).ToString() });
            }

            for (int i = startIndex; i <= endIndex; i++)
            {
                pages.Add(new Page { Text = i.ToString(), Value = i.ToString(), Selected = i == currentPage });
            }

            //Add the Next Button.
            if (currentPage < pageCount)
            {
                pages.Add(new Page { Text = ">>", Value = (currentPage + 1).ToString() });
            }

            //Add the Last Button.
            if (currentPage != pageCount)
            {
                pages.Add(new Page { Text = "Last", Value = pageCount.ToString() });
            }

            //Clear existing Pager Buttons.
            panelPager.Controls.Clear();

            //Loop and add Buttons for Pager.
            int count = 0;
            foreach (Page page in pages)
            {
                Button btnPage = new Button();
                btnPage.Location = new System.Drawing.Point(38 * count, 5);
                btnPage.Size = new System.Drawing.Size(40, 20);
                btnPage.Name = page.Value;
                btnPage.Text = page.Text;
                btnPage.Enabled = !page.Selected;
                btnPage.BackColor = Color.LimeGreen;
                btnPage.ForeColor = Color.Black;
                btnPage.Click += new System.EventHandler(this.Page_Click);
                panelPager.Controls.Add(btnPage);
                count++;

                if (page.Selected)
                {
                    btnPage.BackColor = Color.Magenta;
                    btnPage.ForeColor = Color.Black;
                }
            }
        }

        private void Page_Click(object sender, EventArgs e)
        {
            Button btnPager = (sender as Button);
            this.RefreshGrid(int.Parse(btnPager.Name));
        }

        private void convert_datatable_into_list(DataTable dt)
        {
            try
            {
                _lst_payroll_items = (from DataRow dr in dt.Rows
                                      select new DAL.PayrollItem()
                                      {
                                          Id = dr.Table.Columns.Contains("Id") && dr["Id"] != DBNull.Value
                                              ? dr["Id"].ToString()
                                              : string.Empty,

                                          Description = dr.Table.Columns.Contains("Description") && dr["Description"] != DBNull.Value
                                              ? dr["Description"].ToString()
                                              : string.Empty,

                                          ItemTypeId = dr.Table.Columns.Contains("ItemTypeId") && dr["ItemTypeId"] != DBNull.Value
                                              ? dr["ItemTypeId"].ToString()
                                              : string.Empty,

                                          TaxTrackingId = dr.Table.Columns.Contains("TaxTrackingId") && dr["TaxTrackingId"] != DBNull.Value
                                              ? dr["TaxTrackingId"].ToString()
                                              : string.Empty,

                                          PayableTo = dr.Table.Columns.Contains("PayableTo") && dr["PayableTo"] != DBNull.Value
                                              ? dr["PayableTo"].ToString()
                                              : string.Empty,

                                          GLAccount = dr.Table.Columns.Contains("GLAccount") && dr["GLAccount"] != DBNull.Value
                                              ? dr["GLAccount"].ToString()
                                              : string.Empty,

                                          ReFField = dr.Table.Columns.Contains("ReFField") && dr["ReFField"] != DBNull.Value
                                              ? Convert.ToInt32(dr["ReFField"])
                                              : 0,

                                          DefaultItem = dr.Table.Columns.Contains("DefaultItem") && dr["DefaultItem"] != DBNull.Value
                                              && Convert.ToBoolean(dr["DefaultItem"]),

                                          AddToPension = dr.Table.Columns.Contains("AddToPension") && dr["AddToPension"] != DBNull.Value
                                              && Convert.ToBoolean(dr["AddToPension"]),

                                          Active = dr.Table.Columns.Contains("Active") && dr["Active"] != DBNull.Value
                                              && Convert.ToBoolean(dr["Active"]),

                                          Enable = dr.Table.Columns.Contains("Enable") && dr["Enable"] != DBNull.Value
                                              && Convert.ToBoolean(dr["Enable"]),

                                          IsDeleted = dr.Table.Columns.Contains("IsDeleted") && dr["IsDeleted"] != DBNull.Value
                                              && Convert.ToBoolean(dr["IsDeleted"])
                                      }).AsQueryable();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private void BindGrid()
        {
            try
            {
                //set the datasource to null
                bindingSourcePayrollItems.DataSource = null;
                //set the datasource to a method
                bindingSourcePayrollItems.DataSource = _lst_payroll_items;
                dataGridViewPayrollItems.DataSource = bindingSourcePayrollItems;
                groupBox4.Text = bindingSourcePayrollItems.Count.ToString() + "/" + recordCount;
                foreach (DataGridViewRow row in dataGridViewPayrollItems.Rows)
                {
                    dataGridViewPayrollItems.Rows[dataGridViewPayrollItems.Rows.Count - 1].Selected = true;
                    int nRowIndex = dataGridViewPayrollItems.Rows.Count - 1;
                    bindingSourcePayrollItems.Position = nRowIndex;
                }

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("fetched [ " + _lst_payroll_items.ToList().Count + " ] records.", TAG));
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Forms.AddPayrollItem f = new AddPayrollItem(connection) { Owner = this };
            f.ShowDialog();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayrollItems.SelectedRows.Count != 0)
            {
                try
                {

                    DAL.PayrollItem payrollitem = (DAL.PayrollItem)bindingSourcePayrollItems.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "NON_CASH_BENEFIT", "HOURLY_PAY", "ADVANCE", "SHIF", "AHL" };
                    if (defaultItems.Contains(payrollitem.Id.Trim()))
                    {
                        MessageBox.Show("Cannot Edit Default Payroll Item " + payrollitem.Id.Trim(), Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Forms.EditPayrollItem f = new EditPayrollItem(payrollitem, connection) { Owner = this };
                        f.Text = payrollitem.Id.ToString().Trim().ToUpper();
                        f.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayrollItems.SelectedRows.Count != 0)
                {

                    DAL.PayrollItem payrollitem = (DAL.PayrollItem)bindingSourcePayrollItems.Current;

                    var _employeewithpayrollitemsquery = from br in rep.GetEmployeesWithPayrollItemTransactions(payrollitem)
                                                         select br;
                    List<DAL.Employee> _employees = _employeewithpayrollitemsquery.ToList();


                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "NON_CASH_BENEFIT", "HOURLY_PAY", "ADVANCE", "SHIF", "AHL" };
                    if (defaultItems.Contains(payrollitem.Id.Trim()))
                    {
                        MessageBox.Show("Cannot Delete Default Payroll Item " + payrollitem.Id.Trim(), Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if ((_employees.Count > 0))
                    {
                        MessageBox.Show("There is an Employee Associated with this Payroll Item\nDelete the Employee first!", Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Delete Payroll Item\n " + payrollitem.Id.ToString().Trim().ToUpper(), "Confirm Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            rep.DeletePayrollItem(payrollitem);
                            RefreshGrid(1);
                        }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayrollItems.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.PayrollItem payrollitem = (DAL.PayrollItem)bindingSourcePayrollItems.Current;
                    Forms.EditPayrollItem f = new EditPayrollItem(payrollitem, connection) { Owner = this };
                    f.Text = payrollitem.Id.ToString().Trim().ToUpper();
                    f.DisableControls();
                    f.ShowDialog();
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void dataGridViewPayrollItems_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewPayrollItems.SelectedRows.Count != 0)
            {
                try
                {
                    DAL.PayrollItem payrollitem = (DAL.PayrollItem)bindingSourcePayrollItems.Current;
                    List<string> defaultItems = new List<string>() { "BASIC", "PAYE", "NSSF", "NHIF", "NON_CASH_BENEFIT", "HOURLY_PAY", "ADVANCE", "SHIF", "AHL" };
                    if (defaultItems.Contains(payrollitem.Id.Trim()))
                    {
                        MessageBox.Show("Cannot Edit Default Payroll Item " + payrollitem.Id.Trim(), Utils.APP_NAME, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Forms.EditPayrollItem f = new EditPayrollItem(payrollitem, connection) { Owner = this };
                        f.Text = payrollitem.Id.ToString().Trim().ToUpper();
                        f.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                }
            }
        }
        private void chkInActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshGrid(1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewPayrollItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Log.WriteToErrorLogFile(ex);
            }
        }

        private void txtdescription_TextChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void cbo_item_types_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void cbo_tax_trackings_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }

        private void btnclearfilter_Click(object sender, EventArgs e)
        {
            txtdescription.Text = string.Empty;
            cbo_item_types.SelectedIndex = 0;
            cbo_tax_trackings.SelectedIndex = 0;
            chkdefault.Checked = false;
        }

        private void chkdefault_CheckedChanged(object sender, EventArgs e)
        {
            RefreshGrid(1);
        }






    }
}