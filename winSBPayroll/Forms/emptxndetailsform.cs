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
    public partial class emptxndetailsform : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _user;

        public string TAG;
        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;

        DAL.EmployeeTransaction _emptxn;

        public emptxndetailsform(string user, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname, DAL.EmployeeTransaction emptxn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _user = user;

            _emptxn = emptxn;

            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished emptxndetailsform initialization", TAG));
        }

        private void emptxndetailsform_Load(object sender, EventArgs e)
        {
            try
            {
                if (_emptxn == null)
                    return;

                txtId.Enabled = false;
                txtPostDate.Enabled = false;
                txtEmpNo.Enabled = false;
                txtItemId.Enabled = false;
                txtAmount.Enabled = false;
                txtBalance.Enabled = false;
                txtInitialAmount.Enabled = false;
                chkEnabled.Checked = true;
                chkRecurrent.Enabled = true;
                chkIsDeleted.Enabled = false;

                txtId.Text = _emptxn.Id.ToString();
                txtPostDate.Text = _emptxn.PostDate.ToString();
                txtEmpNo.Text = _emptxn.EmpNo;
                txtItemId.Text = _emptxn.ItemId;
                txtAmount.Text = _emptxn.Amount.ToString();
                txtBalance.Text = _emptxn.Balance.ToString();
                txtInitialAmount.Text = _emptxn.InitialAmount.ToString();
                chkEnabled.Checked = _emptxn.Enabled;
                chkRecurrent.Checked = _emptxn.Recurrent;
                chkIsDeleted.Checked = _emptxn.IsDeleted ?? false;

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("finished emptxndetailsform load", TAG));
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                _emptxn.Enabled = chkEnabled.Checked;
                _emptxn.Recurrent = chkRecurrent.Checked;
                _emptxn.LastChangeDate = DateTime.Today;
                _emptxn.LastChangedBy = _user;

                rep.update_EmployeeTransaction_admin(_emptxn);

                EmployeeTransactionsForm f = (EmployeeTransactionsForm)this.Owner;
                f.RefreshGrid();
                this.Close();
            }
            catch (Exception ex)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(ex.ToString(), TAG));
                Utils.ShowError(ex);
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
