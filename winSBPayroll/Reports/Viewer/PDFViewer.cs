using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BLL.DataEntry;
using BLL.KRA;
using BLL.KRA.ModelMakers;
using BLL.KRA.Models;
using CommonLib;
using DAL;
//--- Add the following to make this code work
using iTextSharp.text;
using VVX; 
using winSBPayroll.Forms;

namespace winSBPayroll.Forms
{
    public partial class PDFViewer : Form
    {
        #region "Private Fields"
        private string msAppName = "SB Payroll Report.....";
        PDFGen mPdf;
        string msFilePDF = "";
        string msFolder = "";
        List<Employee> _employeesList;
        List<DAL.Payroll> openPayrolls;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        static bool showExcel = false;
        string _user;
        string _resourcesPath = null;
        string connection;
        List<NssfContributionsDTO> _nssfcomputations = new List<NssfContributionsDTO>();
        #endregion "Private Fields"

        #region "Public Fields"
        //delegate
        public delegate void ReportsProcessCompleteEventHandler(object sender, ReportsProcessCompleteEventArg e);
        //event
        public event ReportsProcessCompleteEventHandler OnCompleteReportsProcess;
        //delegate
        public delegate void ReportsEngineCompleteEventHandler(object sender, ReportsEngineCompleteEventArg e);
        //event
        public event ReportsEngineCompleteEventHandler OnCompleteReportsEngine;
        #endregion "Public Fields"

        #region "Constructor"
        public PDFViewer(string user, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            rep = new Repository(connection);
            db = new SBPayrollDBEntities(connection);

            _user = user;

            DisableAllMenus();

            Authorize();

            //--- init the folder in which generated PDF's will be saved.
            msFolder = Application.ExecutablePath;
            int n = msFolder.LastIndexOf(@"\");
            msFolder = msFolder.Substring(0, n + 1);

            SetResourcePath();
            mPdf = new PDFGen(_resourcesPath, connection);

            //NavigateToHomePage();
        }
        public PDFViewer(List<NssfContributionsDTO> nssfcomputations, string user, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            rep = new Repository(connection);
            db = new SBPayrollDBEntities(connection);

            _user = user;

            DisableAllMenus();

            Authorize();

            //--- init the folder in which generated PDF's will be saved.
            msFolder = Application.ExecutablePath;
            int n = msFolder.LastIndexOf(@"\");
            msFolder = msFolder.Substring(0, n + 1);

            SetResourcePath();
            mPdf = new PDFGen(_resourcesPath, connection);

            _nssfcomputations = nssfcomputations;

            ShowNewNSSF("pdf", _nssfcomputations);
        }
        #endregion "Constructor"

        #region "Private Methods"
        #region "General Purpose Helpers for this Form"
        //************************************************************
        /// <summary>
        /// Refreshes the window's Caption/Title bar
        /// </summary>
        private void DoUpdateCaption()
        {
            try
            {
                this.Text = this.msAppName;

                if (this.msFilePDF.Length == 0)
                {
                    this.Text += "<...no PDF file created...>";
                }
                else
                {
                    FileInfo fi = new FileInfo(this.msFilePDF);
                    this.Text += @"...\" + fi.Name;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void DoPreProcess(object sender, EventArgs e)
        {
            this.ctlInfoLBL.Text = string.Empty;
        }
        private void DoPostProcess(object sender, EventArgs e)
        {
            try
            {
                string dir = rep.SettingLookup("REPORTPATH");
                string sRet = dir + "\\" + msFilePDF;
                int pdfCount = Directory.GetFiles(dir, "*.pdf", SearchOption.TopDirectoryOnly).Length;
                int excelCount = Directory.GetFiles(dir, "*.xls", SearchOption.TopDirectoryOnly).Length;
                int _totalFiles = pdfCount + excelCount;
                this.ctlInfoLBL.Text = msFilePDF.ToString() + "     [  " + _totalFiles.ToString() + "  ] ";
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private string GetURI(string sFile)
        {
            string sRet;
            try
            {
                string dir = de.SettingLookup("REPORTPATH");
                sRet = dir + "\\" + sFile;
                //check if directory exists.
                if (!System.IO.Directory.Exists(dir))
                {
                    sRet = msFolder + "Output\\" + sFile;
                }
            }
            catch (Exception e)
            {
                Utils.ShowError(e);
                sRet = msFolder + "Output\\" + sFile;
            }
            return sRet;
        }
        private void SetResourcePath()
        {
            string sRet = string.Empty;
            try
            {
                string dir = de.SettingLookup("RESOURCEPATH");
                if (!System.IO.Directory.Exists(dir))
                {
                    sRet = msFolder + "Resources\\";
                }
                else
                {
                    sRet = dir;
                }

                this._resourcesPath = sRet;
            }
            catch (Exception e)
            {
                Utils.ShowError(e);
                this._resourcesPath = msFolder + "Resources\\";
            }
        }
        private void DoShowPDF(string sFilePDF)
        {
            this.DoUpdateCaption();
            this.webBrowser1.Navigate(GetURI(sFilePDF));
        }
        #endregion "General Purpose Helpers for this Form"
        #region "datagridview"
        private void dataGridViewByProducts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if ("STATEMENT".Equals(cbopayrollproducts.Text.Trim().ToUpper()))
            {
                if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayrollItem.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employee _employee = (DAL.Employee)bindingSourceEmployees.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                    DAL.PayrollItem _Payrollitem = (DAL.PayrollItem)bindingSourcePayrollItem.Current;
                    int Year = int.Parse(cbYr.Text);
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;

                    StatementModel statementmodel = ProcessStatement(_employee, Year, pay.Period, _Payrollitem);
                    msFilePDF = "Statement " + _employee.EmpNo.Trim() + "  " + _Payrollitem.Id.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString();

                    DoPreProcess(sender, e);
                    string app = string.Empty;
                    if (showExcel)
                    {
                        msFilePDF = msFilePDF + ".xls";
                        app = "excel";
                    }
                    else
                    {
                        msFilePDF = msFilePDF + ".pdf";
                        app = "pdf";
                    }

                    if (mPdf.ShowStatement(app, _Payrollitem.Id, statementmodel, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select a Payroll Year , an Employee and a Payroll By Product ",
                      "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else //schedule
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewPayrollItem.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                    DAL.PayrollItem _payrollitem = (DAL.PayrollItem)bindingSourcePayrollItem.Current;

                    SheduleReportModel schedule = ProcessSchedule(pay, _payrollitem);
                    msFilePDF = "Schedule " + _payrollitem.Id.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString();

                    string app = string.Empty;
                    DoPreProcess(sender, e);
                    if (showExcel)
                    {
                        msFilePDF = msFilePDF + ".xls";
                        app = "excel";
                    }
                    else
                    {
                        msFilePDF = msFilePDF + ".pdf";
                        app = "pdf";
                    }

                    if (mPdf.ShowShedule(app, _payrollitem.Id, schedule, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select a Payroll Year and a Payroll By Product ",
                      "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void dataGridViewEmployee_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnViewPayslip_Click(sender, e);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void dataGridViewEmployers_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                bindingSourceEmployees.DataSource = null;

                if (dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    var _Employeesquery = from ep in rep.GetAllActiveEmployees()
                                          where ep.IsActive == true
                                          where ep.IsDeleted == false
                                          where ep.EmployerId == _employer.Id
                                          select ep;
                    List<Employee> _Employees = _Employeesquery.ToList();

                    bindingSourceEmployees.DataSource = _Employees;
                    dataGridViewEmployee.AutoGenerateColumns = false;
                    this.dataGridViewEmployee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewEmployee.DataSource = bindingSourceEmployees;
                    groupBox1.Text = bindingSourceEmployees.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        #endregion "datagridview"
        #region "buttons"
        private void btnEmployeeReportbutton_Click(object sender, EventArgs e)
        {
            DoPreProcess(sender, e);

            msFilePDF = "EmpList.pdf";

            if (mPdf.EmployeeListing(GetURI(msFilePDF)))
            {
                DoShowPDF(msFilePDF);
            }
            this.DoPostProcess(sender, e);
        }
        private void btnFilterEmployees_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.SearchEmployeeSimpleForm f = new Forms.SearchEmployeeSimpleForm(connection);

                //connect to the delegate
                f.OnEmployeeListSelected += new SearchEmployeeSimpleForm.EmployeeSelectHandler(HandleSelectedEmployeeList);
                f.ShowDialog();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnViewPayslip_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    string payslipType = de.SettingLookup("PAYSLIPTYPE");
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    msFilePDF = "Payslip " + emp.EmpNo.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";

                    PayslipReader pr = new PayslipReader(emp.Id,emp.EmpNo, pay.Period, pay.Year, connection);
                    Payslip pslip = pr.CreatePayslipFromPayslipMaster(chkUseCurrentPayroll.Checked);

                    DoPreProcess(sender, e);

                    if (mPdf.ShowPayslip(payslipType, pslip, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }

                    this.DoPostProcess(sender, e);
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select a Payroll Year, an Employer and an Employee",
                    "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnP9A_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    msFilePDF = "P9A " + emp.EmpNo.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";

                    P9AReportMaker p9AMaker = new P9AReportMaker(_employer, chkUseCurrentPayroll.Checked, emp.Id, emp.EmpNo, pay.Year, connection);
                    P9AReportModel p9A = p9AMaker.GetP9A();

                    DoPreProcess(sender, e);

                    if (mPdf.ShowP9A(p9A, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year, an Employer and an Employee",
                    "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnP9AHOSP_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    msFilePDF = "P9AHOSP " + emp.EmpNo.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";

                    P9AHospReportMaker p9AMaker = new P9AHospReportMaker(_employer, chkUseCurrentPayroll.Checked, emp.Id, emp.EmpNo, pay.Year, connection);
                    P9AHOSPReportModel p9A = p9AMaker.GetP9Hosp();

                    DoPreProcess(sender, e);

                    if (mPdf.ShowP9AHOSP(p9A, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year, an Employer and an Employee",
                    "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnP9B_Click(object sender, EventArgs e)
        {
            if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    msFilePDF = "P9B " + emp.EmpNo.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";

                    P9BReportMaker p9BMaker = new P9BReportMaker(_employer, chkUseCurrentPayroll.Checked, emp.Id, emp.EmpNo, pay.Year, connection);
                    P9BReportModel p9B = p9BMaker.GetP9B();

                    DoPreProcess(sender, e);

                    if (mPdf.ShowP9B(p9B, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);
                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year, an Employer and an Employee",
                     "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnP10A_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    msFilePDF = "P10A " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";

                    P10AReportMaker p10AMaker = new P10AReportMaker(_employer, chkUseCurrentPayroll.Checked, pay.Year, connection);
                    P10AReportModel p10A = p10AMaker.GetP10A();

                    DoPreProcess(sender, e);

                    if (mPdf.ShowP10A(p10A, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                    "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void btnP10_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    msFilePDF = "P10 " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";

                    P10ReportMaker p10maker = new P10ReportMaker(_employer, chkUseCurrentPayroll.Checked, pay.Year, connection);
                    P10ReportModel p10model = p10maker.BuildP10();

                    DoPreProcess(sender, e);

                    if (mPdf.ShowP10(p10model, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                   "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnp11_Click(object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                try
                {

                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    msFilePDF = "p11 " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";

                    P11ReportMaker p11Maker = new P11ReportMaker(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                    P11ReportModel p11 = p11Maker.GetP11Model();

                    DoPreProcess(sender, e);

                    if (mPdf.ShowP11(p11, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                   "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnViewAllPayslip_Click(object sender, EventArgs e)
        {

            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                if (_employeesList == null)
                    _employeesList = rep.GetAllActiveEmployees();

                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                msFilePDF = "All Payslip " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";

                List<Payslip> payslips = new List<Payslip>();
                foreach (var emp in _employeesList)
                {
                    try
                    {
                        //read payslip form payslipmaster + payslipdet
                        PayslipReader pr = new PayslipReader(emp.Id,emp.EmpNo, pay.Period, pay.Year, connection);
                        payslips.Add(pr.CreatePayslipFromPayslipMaster(chkUseCurrentPayroll.Checked));

                    }
                    catch (Exception ex)
                    {
                        Utils.ShowError(ex);
                        return;
                    }
                }
                DoPreProcess(sender, e);

                if (mPdf.ShowPayslip(payslips, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                   "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnShowPDF_Click(object sender, EventArgs e)
        {
            ToggleExcelPDF();
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            ToggleExcelPDF();
        }
        private void btnShow_Click(object sender, EventArgs e)
        {
            if ("STATEMENT".Equals(cbopayrollproducts.Text.Trim().ToUpper()))
            {
                if (dataGridViewEmployee.SelectedRows.Count != 0 && dataGridViewPayrollItem.SelectedRows.Count != 0 && dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employee emp = (DAL.Employee)bindingSourceEmployees.Current;
                    DAL.PayrollItem _Payrollitem = (DAL.PayrollItem)bindingSourcePayrollItem.Current;
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                    int Year = int.Parse(cbYr.Text);

                    StatementModel statementmodel = ProcessStatement(emp, Year, pay.Period, _Payrollitem);
                    msFilePDF = "Statement " + emp.EmpNo.Trim() + "  " + _Payrollitem.Id.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString();

                    DoPreProcess(sender, e);
                    string app = string.Empty;
                    if (showExcel)
                    {
                        msFilePDF = msFilePDF + ".xls";
                        app = "excel";
                    }
                    else
                    {
                        msFilePDF = msFilePDF + ".pdf";
                        app = "pdf";
                    }

                    if (mPdf.ShowStatement(app, _Payrollitem.Id, statementmodel, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select a Payroll Year , an Employee and a Payroll By Product ",
                      "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else //schedule
            {
                if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewPayrollItem.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    Payroll pay = (Payroll)bindingSourcePayroll.Current;
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                    DAL.PayrollItem _payrollitem = (DAL.PayrollItem)bindingSourcePayrollItem.Current;

                    SheduleReportModel schedule = ProcessSchedule(pay, _payrollitem);
                    msFilePDF = "Schedule " + _payrollitem.Id.Trim() + "  " + pay.Period.ToString() + "  " + pay.Year.ToString();

                    string app = string.Empty;
                    DoPreProcess(sender, e);
                    if (showExcel)
                    {
                        msFilePDF = msFilePDF + ".xls";
                        app = "excel";
                    }
                    else
                    {
                        msFilePDF = msFilePDF + ".pdf";
                        app = "pdf";
                    }

                    if (mPdf.ShowShedule(app, _payrollitem.Id, schedule, GetURI(msFilePDF)))
                    {
                        DoShowPDF(msFilePDF);
                    }
                    this.DoPostProcess(sender, e);
                }
                else
                {
                    MessageBox.Show("Please select a Payroll Year and a Payroll By Product ",
                      "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnTsbExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnRemoveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                bindingSourceEmployees.DataSource = null;
                if (dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    var _Employeesquery = from ep in rep.GetAllActiveEmployees()
                                          where ep.IsActive == true
                                          where ep.IsDeleted == false
                                          where ep.EmployerId == _employer.Id
                                          select ep;
                    List<Employee> _Employees = _Employeesquery.ToList();

                    bindingSourceEmployees.DataSource = _Employees;
                    dataGridViewEmployee.AutoGenerateColumns = false;
                    this.dataGridViewEmployee.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridViewEmployee.DataSource = bindingSourceEmployees;
                    groupBox1.Text = bindingSourceEmployees.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnReportsFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string _ReportsDir = rep.SettingLookup("REPORTPATH");
                if (Directory.Exists(_ReportsDir))
                {
                    string _filetoSelect = _ReportsDir + "\\" + msFilePDF;
                    // opens the folder in explorer and selects the displayed file
                    string args = string.Format("/Select, {0}", _filetoSelect);
                    if (System.IO.File.Exists(_filetoSelect))
                    {
                        ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", args);
                        System.Diagnostics.Process.Start(pfi);
                    }
                    else
                    {
                        Utils.ShowError(new Exception("File \n[ " + _filetoSelect + " ] \ndoes not exist."));
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

        }
        #endregion "buttons"
        #region "custom"
        private void NavigateToHomePage()
        {
            string sFile = "iTextSharpTutorial.html";

            string sPath = sFile;
            for (int i = 0; i < 6; i++)
            {
                if (VVX.File.Exists(sPath))
                {
                    FileInfo fi = new FileInfo(sPath);
                    this.webBrowser1.Navigate(fi.FullName);
                    break;
                }
                sPath = @"..\" + sPath;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void PDFViewer_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeControls();

                RefreshGrid();
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
                dataGridViewPayroll.AutoGenerateColumns = false;
                this.dataGridViewPayroll.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                cbYr.DataSource = de.GetPayrollYears();
                cbYr.DisplayMember = "Year";

                dataGridViewPayrollItem.AutoGenerateColumns = false;
                this.dataGridViewPayrollItem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourcePayrollItem.DataSource = de.GetActivePayrollItems();
                dataGridViewPayrollItem.DataSource = bindingSourcePayrollItem;
                groupBox2.Text = bindingSourcePayrollItem.Count.ToString();

                cbopayrollproducts.SelectedIndex = 0;

                dataGridViewEmployers.AutoGenerateColumns = false;
                this.dataGridViewEmployers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourceEmployers.DataSource = rep.GetAllActiveEmployers();
                dataGridViewEmployers.DataSource = bindingSourceEmployers;
                groupBox4.Text = bindingSourceEmployers.Count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void Authorize()
        {
            try
            {
                var _dbuserquery = (from us in db.spUsers
                                    where us.UserName == _user
                                    select us).FirstOrDefault();
                spUser LoggedInUser = _dbuserquery;
                if (LoggedInUser != null)
                {
                    var allowedmenusquery = from arm in db.spAllowedReportsRolesMenus
                                            where arm.RoleId == LoggedInUser.RoleId
                                            select arm;
                    List<spMenuItem> menuitems = new List<spMenuItem>();
                    foreach (var armq in allowedmenusquery.ToList())
                    {
                        ToolStripItem tsbitem = toolStrip1.Items.Find(armq.spReportsMenuItem.mnuName, true).OfType<ToolStripItem>().FirstOrDefault();

                        if (tsbitem != null && armq.Allowed == true)
                        {
                            tsbitem.Enabled = true;
                        }

                        Button btnitem = panel3.Controls.Find(armq.spReportsMenuItem.mnuName, true).OfType<Button>().FirstOrDefault();

                        if (btnitem != null && armq.Allowed == true)
                        {
                            btnitem.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void DisableAllMenus()
        {
            try
            {
                this.btnViewPayslip.Enabled = false;
                this.btnP9A.Enabled = false;
                this.btnP9AHOSP.Enabled = false;
                this.btnP9B.Enabled = false;
                this.btnP10.Enabled = false;
                this.btnP10A.Enabled = false;
                this.btnp11.Enabled = false;
                this.btnViewAllPayslip.Enabled = false;
                this.cboItemTypesReports.Enabled = false;
                this.btnShowPDF.Enabled = false;
                this.btnExcel.Enabled = false;
                this.btnShow.Enabled = false;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        //hander
        private void HandleSelectedEmployeeList(object sender, EmployeeSelectEventArgs e)
        {
            try
            {
                SetEmpNos(e._Employee);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        //Implementation
        public void SetEmpNos(DAL.Employee _Employee)
        {
            try
            {
                if (_Employee != null)
                {
                    bindingSourceEmployees.DataSource = _Employee;
                    groupBox1.Text = bindingSourceEmployees.Count.ToString();
                }
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
        private void RefreshGrid()
        {
            try
            {
                //start with process 
                if (chkUseCurrentPayroll.Checked)
                {
                    //For current payroll; isOpen = true; Processed = true
                    openPayrolls = de.GetPayrolls(PayrollState.OpenProcessed);
                }
                else
                {
                    //For current payroll; isOpen = false; Processed = true
                    openPayrolls = de.GetPayrolls(PayrollState.NotOpenProcessed);
                }

                bindingSourcePayroll.DataSource = openPayrolls;
                dataGridViewPayroll.DataSource = bindingSourcePayroll;
                groupBox3.Text = bindingSourcePayroll.Count.ToString();

                cbYr_SelectedIndexChanged(this, null);
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }
        private void ShowPayrollMaster(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create payeeModel
                PayrollMasterBuilder pm = new PayrollMasterBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                PayrollMasterModel payModel = pm.GetPayrollMaster();

                var _engs = rep.GetAllEarnings();
                var _ddct = rep.GetAllOtherDeductions();

                List<string> earnings = (from ea in _engs
                                         orderby ea.Description descending
                                         select ea.Description).Distinct().ToList();
                List<string> deductions = (from dd in _ddct
                                           orderby dd.Description descending
                                           select dd.Description).Distinct().ToList();

                DoPreProcess(sender, e);
                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "Payroll Master " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "Payroll Master " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .xls";
                }

                if (mPdf.ShowPayrollMaster(earnings, deductions, app, payModel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                  "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowPAYE(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {

                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create payeeModle
                PAYEModelBuilder pmb = new PAYEModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Year, pay.Period, connection);
                PAYEModel payeModel = pmb.GetPAYEModel();
                DoPreProcess(sender, e);
                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "PAYE " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "PAYE " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + ".xls";
                }

                if (mPdf.ShowPayee(app, payeModel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                 "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowNSSF(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {

                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create model
                NSSFReportBuilder nssfreportbuilder = new NSSFReportBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                NSSFReportModel nssfreportmodel = nssfreportbuilder.GetNSSFReport();

                DoPreProcess(sender, e);
                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "NSSF " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "NSSF " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + ".xls";
                }

                if (mPdf.ShowNSSF(_employer, app, nssfreportmodel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                 "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowNewNSSF(string app, List<NssfContributionsDTO> nssfcomputations)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                int Period = DateTime.Now.Month;
                int year = DateTime.Now.Year;

                //create model
                NSSFReportBuilder nssfreportbuilder = new NSSFReportBuilder(chkUseCurrentPayroll.Checked, Period, year, connection);
                NSSFReportModel nssfreportmodel = nssfreportbuilder.GetNewNSSFReport();

                DoPreProcess(this, null);
                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "NSSF " + "  " + Period.ToString() + "  " + year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "NSSF " + "  " + Period.ToString() + "  " + year.ToString() + ".xls";
                }

                if (mPdf.ShowNewNSSF(app, nssfreportmodel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(this, null);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                 "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowNetSalary(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {

                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create model
                NetSalaryReportMaker netsalaryreportmaker = new NetSalaryReportMaker(_employer, chkUseCurrentPayroll.Checked, pay.Year, pay.Period, connection);
                NetSalaryReportModel netsalaryreportmodel = netsalaryreportmaker.GetNetSalaryModel();

                DoPreProcess(sender, e);
                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "Net Salary " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + ".pdf";
                }
                else
                {
                    msFilePDF = "Net Salary " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + ".xls";
                }

                if (mPdf.ShowNetSalary(app, netsalaryreportmodel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                 "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowNHIF(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {

                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create model
                NHIFReportBuilder nhifreportbuilder = new NHIFReportBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                NHIFReportModel nhifreportmodel = nhifreportbuilder.GetNHIFReport();

                DoPreProcess(sender, e);
                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "NHIF " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + ".pdf";
                }
                else
                {
                    msFilePDF = "NHIF " + "  " + pay.Period.ToString() + "  " + pay.Year.ToString() + ".xls";
                }

                if (mPdf.ShowNHIF(app, nhifreportmodel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                 "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowLoanRepaymentSchedule(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {

                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create model
                LoanRepaymentScheduleModelBuilder lrmbuilder = new LoanRepaymentScheduleModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay, connection);
                LoanRepaymentScheduleModel loanrepaymentshedulemodel = lrmbuilder.Getloanrepaymentshedule();

                DoPreProcess(sender, e);

                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "Loan Repayments " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "Loan Repayments " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .xls";
                }

                if (mPdf.ShowLoanRepaymentShedule(app, loanrepaymentshedulemodel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                 "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowSaccoPaymentSchedule(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {

                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create model
                SaccoPaymentScheduleModelBuilder spsmbuilder = new SaccoPaymentScheduleModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay, connection);
                SaccoPaymentScheduleModel saccopaymentshedulemodel = spsmbuilder.Getsaccopaymentshedule();

                DoPreProcess(sender, e);
                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "Sacco Contributions " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "Sacco Contributions  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .xls";
                }

                if (mPdf.ShowSaccoPaymentShedule(app, saccopaymentshedulemodel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                 "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private StatementModel ProcessStatement(Employee emp, int Yr, int period, DAL.PayrollItem payrollitem)
        {
            try
            {
                if (dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    StatementModelBuilder sb = new StatementModelBuilder(_employer, chkUseCurrentPayroll.Checked, Yr, period, emp.Id, emp.EmpNo, payrollitem, connection);
                    StatementModel statement = sb.GetStatement();
                    return statement;
                }
                return null;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private SheduleReportModel ProcessSchedule(DAL.Payroll payroll, DAL.PayrollItem payrollitem)
        {
            try
            {
                if (dataGridViewEmployers.SelectedRows.Count != 0)
                {
                    DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                    SheduleModelBuilder sheduleMaker = new SheduleModelBuilder(_employer, chkUseCurrentPayroll.Checked, payroll, payrollitem, connection);
                    SheduleReportModel schedule = sheduleMaker.Getshedulemodel();
                    return schedule;
                }
                return null;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void ShowBankTransfer(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create model
                BankTransferMaker bbtbuilder = new BankTransferMaker(_employer,chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                BankTransferReportModel bbtreportmodel = bbtbuilder.GetBankTransferModelBuilder();

                DoPreProcess(sender, e);

                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "Bank Transfer  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "Bank Transfer  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .xls";
                }

                if (mPdf.ShowBankTransfer(app, bbtreportmodel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year  ",
                 "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ShowBankBranchTransfer(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {

                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create model
                BankBranchTransferModelBuilder bbtbuilder = new BankBranchTransferModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                BankTransferModel bbtreportmodel = bbtbuilder.GetBankBranchTransferModelBuilder();

                DoPreProcess(sender, e);

                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "Bank Branch Transfer " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "Bank Branch Transfer  " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .xls";
                }

                if (mPdf.ShowBankBranchTransfer(app, bbtreportmodel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }

                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year  ",
                "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowEmployeeReport(string app, object sender, EventArgs e)
        {
            if (cbYr.SelectedIndex != -1 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;
                int year = int.Parse(cbYr.SelectedValue.ToString());

                //create model
                EmployeesModelBuilder bbtbuilder = new EmployeesModelBuilder(_employer, connection);
                EmployeesModelReport bbtreportmodel = bbtbuilder.GetEmployeesModel();

                DoPreProcess(sender, e);
                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "Employees " + year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "Employees " + year.ToString() + " .xls";
                }

                if (mPdf.ShowEmployees(app, bbtreportmodel, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ShowAdvanceSchedule(string app, object sender, EventArgs e)
        {
            if (dataGridViewPayroll.SelectedRows.Count != 0 && dataGridViewEmployers.SelectedRows.Count != 0)
            {
                Payroll pay = (Payroll)bindingSourcePayroll.Current;
                DAL.Employer _employer = (DAL.Employer)bindingSourceEmployers.Current;

                //create model
                AdvanceModelBuilder advancebuilder = new AdvanceModelBuilder(_employer, chkUseCurrentPayroll.Checked, pay.Period, pay.Year, connection);
                AdvanceReportModel advancereport = advancebuilder.GetAdvanceModel();

                DoPreProcess(sender, e);

                if ("pdf".Equals(app.ToLower()))
                {
                    msFilePDF = "Advances " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .pdf";
                }
                else
                {
                    msFilePDF = "Advances " + pay.Period.ToString() + "  " + pay.Year.ToString() + " .xls";
                }

                if (mPdf.ShowAdvanceSchedule(app, advancereport, GetURI(msFilePDF)))
                {
                    DoShowPDF(msFilePDF);
                }
                this.DoPostProcess(sender, e);
            }
            else
            {
                MessageBox.Show("Please select  a Payroll Year and an Employer",
                 "SB Payroll Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ToggleExcelPDF()
        {
            if (showExcel)
            {
                showExcel = false;
                btnExcel.Checked = showExcel;
                btnShowPDF.Checked = !showExcel;
            }
            else
            {
                showExcel = true;
                btnExcel.Checked = showExcel;
                btnShowPDF.Checked = !showExcel;
            }
        }
        #endregion "custom"
        #region "combobox"
        private void cbYr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbYr.SelectedItem != null && openPayrolls != null)
                {
                    string filter = "Year =" + cbYr.Text;
                    List<Payroll> filterd = (from l in openPayrolls
                                             where l.Year == (int)cbYr.SelectedItem
                                             select l).ToList();
                    bindingSourcePayroll.DataSource = filterd;
                    groupBox3.Text = bindingSourcePayroll.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void chkUseCurrentPayroll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RefreshGrid();
                if (chkUseCurrentPayroll.Checked)
                {
                    chkUseCurrentPayroll.Text = "Using Current Payroll (click to use closed payrolls)";
                }
                else
                {
                    chkUseCurrentPayroll.Text = "Using Closed Payrolls (click to use current payroll)";
                }
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
            }
        }
        private void cboItemTypesReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (showExcel)
            {
                switch (cboItemTypesReports.Text)
                {
                    case "Payroll Master":
                        ShowPayrollMaster("excel", sender, e);
                        break;
                    case "NHIF":
                        ShowNHIF("excel", sender, e);
                        break;
                    case "NSSF":
                        ShowNSSF("excel", sender, e);
                        break;
                    case "PAYE":
                        ShowPAYE("excel", sender, e);
                        break;
                    case "Net Salary":
                        ShowNetSalary("excel", sender, e);
                        break;
                    case "Bank Transfer":
                        ShowBankTransfer("excel", sender, e);
                        break;
                    case "Bank Branch Transfer":
                        ShowBankBranchTransfer("excel", sender, e);
                        break;
                    case "Loan Repayments":
                        ShowLoanRepaymentSchedule("excel", sender, e);
                        break;
                    case "Sacco Contributions":
                        ShowSaccoPaymentSchedule("excel", sender, e);
                        break;
                    case "Advances":
                        ShowAdvanceSchedule("excel", sender, e);
                        break;
                    case "Employees":
                        ShowEmployeeReport("excel", sender, e);
                        break;
                }
            }
            else
            {
                switch (cboItemTypesReports.Text)
                {
                    case "Payroll Master":
                        ShowPayrollMaster("pdf", sender, e);
                        break;
                    case "NHIF":
                        ShowNHIF("pdf", sender, e);
                        break;
                    case "NSSF":
                        ShowNSSF("pdf", sender, e);
                        break;
                    case "PAYE":
                        ShowPAYE("pdf", sender, e);
                        break;
                    case "Net Salary":
                        ShowNetSalary("pdf", sender, e);
                        break;
                    case "Bank Transfer":
                        ShowBankTransfer("pdf", sender, e);
                        break;
                    case "Bank Branch Transfer":
                        ShowBankBranchTransfer("pdf", sender, e);
                        break;
                    case "Loan Repayments":
                        ShowLoanRepaymentSchedule("pdf", sender, e);
                        break;
                    case "Sacco Contributions":
                        ShowSaccoPaymentSchedule("pdf", sender, e);
                        break;
                    case "Advances":
                        ShowAdvanceSchedule("pdf", sender, e);
                        break;
                    case "Employees":
                        ShowEmployeeReport("pdf", sender, e);
                        break;
                }
            }
        }
        #endregion "combobox"

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        #endregion "Private Methods"
    }


    public class ReportsProcessCompleteEventArg : System.EventArgs
    {
        private int svalue;

        public ReportsProcessCompleteEventArg(int value)
        {
            svalue = value;
        }

        public int StatusValue
        {
            get { return svalue; }
        }
    }

}

    


