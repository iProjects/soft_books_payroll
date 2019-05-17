using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.Payroll;
using BLL.DataEntry;
using CommonLib;
using DAL; 

namespace winSBPayroll.Forms
{
    public partial class ProcessPayroll : Form
    {
        DataEntry de  ;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _User;

        public ProcessPayroll(string user, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _User = user;
        }

        private void btnProcessPayroll_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayrolls.SelectedRows.Count != 0)
            {
                DAL.Payroll pay = (DAL.Payroll)bindingSourcePayrolls.Current;

                try
                {
                    // see if the payslip_master_temp has records and they are for this payroll
                    //if not, decline processing
                    //set up
                    int periodWc = 0;
                    int yearWc = 0;
                    if (!de.WorkingCopyNotClosed(ref periodWc, ref yearWc))
                    {
                        bool simulateRun = false;
                        ProcessPayrollNow(pay.Period, pay.Year, simulateRun);

                        pay.DateRun = DateTime.Now.Date;

                        rep.UpdatePayrollDateRun(pay);
                    }
                    else if (periodWc == pay.Period && yearWc == pay.Year)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Payroll already processed! \nDo you wish to overwrite?", "Confirm Overwrite", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            bool simulateRun = true;
                            ProcessPayrollNow(pay.Period, pay.Year, simulateRun);

                            pay.DateRun = DateTime.Now.Date;

                            rep.UpdatePayrollDateRun(pay);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must close the previously Processed payroll before proceeding", "SB Payroll");
                    }
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return;
                }
            }
        }
        private void ProcessPayrollNow(int period, int year, bool simulateRun)
        {
            try
            {
                pbStatus.Minimum = 0;
                pbStatus.Maximum = rep.GetAllActiveEmployees().Count();
                listBoxResults.Items.Clear();
                CPayroll cp = new CPayroll(_User, connection);
                //subscribe to the events
                cp.OnCompleteGeneratePayslip += new CPayroll.PayslipCompleteEventHandler(cp_OnCompleteGeneratePayslip);
                string sError = string.Empty; 
                cp.RunPayroll(simulateRun, period, year, ref sError);
                de.MarkPayrollAsProcessed(period, year);
                if (sError != "")
                {
                    Log.WriteToErrorLogFile(new Exception(sError));
                };
                MessageBox.Show("Successfully run", "SB Payroll");
                RefreshGrid();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private bool PayrollNotClosed(int period, int year)
        {
            bool ret = true;

            return ret;
        }
        //Handle the event
        private void cp_OnCompleteGeneratePayslip(object sender, PayslipCompleteEventArg e)
        {
            try
            {
                pbStatus.Value = e.StatusValue;
                NotifyMessage("PAYROLL ENGINE RUNNING...", e._Template + Environment.NewLine + e.ErrorMsg);
                listBoxResults.Items.Add(e.ErrorMsg);                
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public bool NotifyMessage(string _Title, string _Text)
        {
            try
            {
                appNotifyIcon.Text = "Soft Books Payroll";
                appNotifyIcon.Icon = new Icon("Resources/Icons/Dollar.ico");
                appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                appNotifyIcon.BalloonTipTitle = _Title;
                appNotifyIcon.BalloonTipText = _Text;
                appNotifyIcon.Visible = true;
                appNotifyIcon.ShowBalloonTip(900000);

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }
        private void ProcessPayroll_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridViewPayrolls.AutoGenerateColumns = false;
                //load all payrolls not closed
                List<DAL.Payroll> openPayrolls = de.GetPayrolls(PayrollState.Open);
                bindingSourcePayrolls.DataSource = openPayrolls;
                this.dataGridViewPayrolls.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewPayrolls.DataSource = bindingSourcePayrolls;
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
        public void RefreshGrid()
        {
            try
            {
                bindingSourcePayrolls.DataSource = null;
                List<DAL.Payroll> openPayrolls = de.GetPayrolls(PayrollState.Open);
                bindingSourcePayrolls.DataSource = openPayrolls;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnClosePayroll_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewPayrolls.SelectedRows.Count != 0)
                {
                    DAL.Payroll pay = (DAL.Payroll)bindingSourcePayrolls.Current;
                    if (pay.Processed)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Are you sure you want to Close this Payroll?", "Confirm Close", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                        {
                            // Todo close the payroll here
                            CPayroll cp = new CPayroll(_User, connection);
                            cp.ArchivePayroll(pay.Period, pay.Year);

                            // Todo code for removing record from the payslipmaster
                            RefreshGrid();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You are not allowed to close a non Processed Payroll!","SB Payroll");
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


        
       

           
