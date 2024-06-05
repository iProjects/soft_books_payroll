using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using BLL.KRA;
using BLL.KRA.Models;
//Payroll
using BLL.Payroll;
using CommonLib;
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using winSBPayroll.Reports.Excel;
using winSBPayroll.Reports.ExcelBuilder;
using winSBPayroll.Reports.PDF;
using winSBPayroll.Reports.PDFBuilder;
using winSBPayroll.ViewModel;
using winSBPayroll.Views;

namespace VVX
{
    public class PDFGen
    {

        #region "Properties"
        private bool bRet = false;
        private string resourcePath;
        private string sMsg = "";
        string connection;
        Repository rep;
        int _counter = 0;
        SBPayrollDBEntities db;
        public string Message
        {
            get { return sMsg; }
            set { sMsg = value; }
        }
        public bool Success
        {
            get { return bRet; }
            set { bRet = value; }
        }
        //delegate
        public delegate void ReportsEngineCompleteEventHandler(object sender, ReportsEngineCompleteEventArg e);
        //event
        public event ReportsEngineCompleteEventHandler OnCompleteReportsEngine;
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        #endregion "Properties"

        #region "Constructor"
        public PDFGen(EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;
        }
        public PDFGen(string ResourcePath, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            rep = new Repository(connection);
            db = new SBPayrollDBEntities(connection);

            _notificationmessageEventname = notificationmessageEventname;

            resourcePath = ResourcePath;

        }
        #endregion "Constructor"

        #region "Helper methods"
        /// <summary>
        /// Safely attempts to insert an image file into the document
        /// </summary>
        /// <param name="document">iTextSharp Document in which it needs to be inserted</param>
        /// <param name="sFilename">the name of the file to be inserted</param>
        /// <returns>false if failed to do so</returns>
        private bool DoInsertImageFile(Document document, string sFilename, bool bInsertMsg)
        {
            bool bRet = false;

            try
            {
                if (File.Exists(sFilename) == false)
                {
                    string sMsg = "Unable to find '" + sFilename + "' in the current folder.\n\n"
                                + "Would you like to locate it?";
                    if (MsgBox.Confirm(sMsg))
                        sFilename = FileDialog.GetFilenameToOpen(FileDialog.FileType.Image);
                }

                Image img = null;
                if (File.Exists(sFilename))
                {
                    this.DoGetImageFile(sFilename, out img);
                }

                if (img != null)
                {
                    document.Add(img);
                    bRet = true;
                }
                else
                {
                    if (bInsertMsg)
                        document.Add(new Paragraph(sFilename + " not found"));
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

            return bRet;
        }
        public Image DoGetImageFile(string sFilename)
        {
            bool bRet = false;
            Image img = null;

            try
            {
                if (File.Exists(sFilename) == false)
                {
                    string sMsg = "Unable to find '" + sFilename + "' in the current folder.\n\n"
                                + "Would you like to locate it?";
                    if (MsgBox.Confirm(sMsg))
                        sFilename = FileDialog.GetFilenameToOpen(FileDialog.FileType.Image);
                }

                if (File.Exists(sFilename))
                {
                    img = Image.GetInstance(sFilename);
                }

                bRet = (img != null);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

            return img;
        }
        private bool DoGetImageFile(string sFilename, out Image img)
        {
            bool bRet = false;
            img = null;

            try
            {
                if (File.Exists(sFilename) == false)
                {
                    string sMsg = "Unable to find '" + sFilename + "' in the current folder.\n\n"
                                + "Would you like to locate it?";
                    if (MsgBox.Confirm(sMsg))
                        sFilename = FileDialog.GetFilenameToOpen(FileDialog.FileType.Image);
                }

                if (File.Exists(sFilename))
                {
                    img = Image.GetInstance(sFilename);
                }

                bRet = (img != null);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

            return bRet;
        }
        private bool DoLocateImageFile(ref string sFilename)
        {
            bool bRet = false;

            try
            {
                if (File.Exists(sFilename) == false)
                {
                    string sMsg = "Unable to find '" + sFilename + "' in the current folder.\n\n"
                                + "Would you like to locate it?";

                    if (MsgBox.Confirm(sMsg))
                        sFilename = FileDialog.GetFilenameToOpen(FileDialog.FileType.Image);
                }

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }

            return bRet = File.Exists(sFilename);
        }
        #endregion "Helper methods"

        #region Payslip
        public bool ShowPayslip(string Paysliptype, Payslip pslip, string sFilePDF)
        {
            //delete file if exists
            if (File.Exists(sFilePDF)) File.Delete(sFilePDF);

            bRet = false;
            try
            {
                if ("VIKE".Equals(Paysliptype))
                {
                    //VikepayslipView pm = new VikepayslipView(new VikePayslipViewModel(pslip), sFilePDF, connection);
                    //VikepayslipView2 pm = new VikepayslipView2(new VikePayslipViewModel(pslip), sFilePDF,connection);
                    VikepayslipView3 pm = new VikepayslipView3(new VikePayslipViewModel(pslip), sFilePDF, connection, _notificationmessageEventname);
                    pm.GetPDF();
                    return true;
                }
                else
                {
                    //Build the defualt payslip 
                    VikepayslipView2 pm = new VikepayslipView2(new VikePayslipViewModel(pslip), sFilePDF, connection, _notificationmessageEventname);
                    pm.GetPDF();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }
        }
        public bool ShowPayslip(List<Payslip> pslips, string sFilePDF)
        {
            bRet = false;
            try
            {
                //PayslipMakerAll pm = new PayslipMakerAll(pslips, sFilePDF, connection);
                //pm.GetPDF();

                PayslipMakerAll2 pm = new PayslipMakerAll2(pslips, sFilePDF, connection, _notificationmessageEventname);
                pm.GetPDF();

                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }
        }
        #endregion

        #region By Products
        public bool ShowByproduct(PayrollItem Item, List<Payslip> pslips, string sFilePDF)
        {
            return false;
        }
        #endregion

        #region Employee Listing

        public bool EmployeeListing(string sFilePDF)
        {

            bRet = false;
            //Make a new employee listing report

            EmployeeReportMaker epm = new EmployeeReportMaker("System User");
            EmployeeReportModel empReport = epm.CreateListOfEmployeesReport();
            //Creates an instance of the iTextSharp.text.Document-object:
            Document document = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            try
            {

                //Creates a Writer that listens to this document and writes the document to the Stream of your choice:
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));
                //Opens the document:
                document.Open();
                //Draw the created employee list on the document
                //Adds content to the document:
                document.Add(new Paragraph(empReport.CompName));
                document.Add(new Paragraph(empReport.ReportName));
                document.Add(new Paragraph("Printed on: " + empReport.PrintedOn.ToShortDateString()));

                //do the employee list as a table of 3 columns
                Table empTable = new Table(3);
                //add the table headers
                empTable.AddCell("EmpNo");
                empTable.AddCell("Name");
                empTable.AddCell("DoB");

                Table empTable1 = new Table(3);
                //add the table headers
                empTable1.AddCell("DoE");
                empTable1.AddCell("MaritalStatus");
                empTable1.AddCell("Gender");

                Table empTable2 = new Table(3);
                //add the table headers
                empTable2.AddCell("NSSFNo");
                empTable2.AddCell("NHIFNo");
                empTable2.AddCell("PINNo");

                Table empTable3 = new Table(3);

                //add the table headers

                empTable3.AddCell("BankCode");

                empTable3.AddCell("IDNo");

                empTable3.AddCell("BankAccount");

                Table empTable4 = new Table(3);

                //add the table headers

                empTable4.AddCell("Department");

                empTable4.AddCell("IsActive");

                empTable4.AddCell("DateLeft");

                Table empTable5 = new Table(3);

                //add the table headers

                empTable5.AddCell("PrevEmployer");

                empTable5.AddCell("BasicPay");

                empTable5.AddCell("PersonalRelief");

                Table empTable6 = new Table(3);

                //add the table headers

                empTable6.AddCell("MortgageRelief");

                empTable6.AddCell("Employer");

                empTable6.AddCell("PayPoint");

                Table empTable7 = new Table(3);

                //add the table headers

                empTable7.AddCell("EmpGroup");

                empTable7.AddCell("EmpPayroll");




                //loop through employee list adding a record as a row

                foreach (var emp in empReport.EmployeesList)
                {

                    empTable.AddCell(emp.EmpNo);

                    empTable.AddCell(emp.Surname.Trim() + "," + emp.OtherNames);

                    empTable.AddCell(emp.DoB.ToString());

                    empTable.AddCell(emp.DoE.ToString());

                    empTable.AddCell(emp.MaritalStatus);

                    empTable.AddCell(emp.Gender);

                    empTable.AddCell(emp.NSSFNo);

                    empTable.AddCell(emp.NHIFNo);

                    empTable.AddCell(emp.PINNo);

                    empTable.AddCell(emp.BankCode);

                    empTable.AddCell(emp.IDNo);

                    empTable.AddCell(emp.BankAccount);

                    var _departmentquery = from dp in db.Departments
                                           where dp.Id == emp.DepartmentId
                                           select dp;
                    Department _department = _departmentquery.FirstOrDefault();

                    empTable.AddCell(_department.Description);

                    empTable.AddCell(emp.IsActive.ToString());

                    empTable.AddCell(emp.DateLeft.ToString());

                    empTable.AddCell(emp.PrevEmployer);

                    empTable.AddCell(emp.BasicPay.ToString());

                    empTable.AddCell(emp.PersonalRelief.ToString());

                    empTable.AddCell(emp.MortgageRelief.ToString());

                    empTable.AddCell(emp.Employer.ToString());

                    empTable.AddCell(emp.PayPoint.ToString());

                    empTable.AddCell(emp.EmpGroup.ToString());

                    empTable.AddCell(emp.EmpPayroll.ToString());


                }



                document.Add(empTable);
                document.Add(empTable1);
                document.Add(empTable2);
                document.Add(empTable3);
                document.Add(empTable4);
                document.Add(empTable5);
                document.Add(empTable6);
                document.Add(empTable7);

                bRet = true;

            }

            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
            finally
            {

                document.Close();

            }
            if (bRet)
            {
                this.Message = sFilePDF + " has been created";
            }
            return bRet;

        }

        #endregion

        #region P9A

        public bool ShowP9A(P9AReportModel p9A, string sFilePDF)
        {
            bRet = false;
            try
            {
                P9APDFBuilder p9AMaker = new P9APDFBuilder(resourcePath, p9A, sFilePDF, _notificationmessageEventname);
                p9AMaker.GetPDF();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }


        #endregion

        #region P9AHosp

        public bool ShowP9AHOSP(P9AHOSPReportModel p9A, string sFilePDF)
        {
            bRet = false;
            try
            {
                P9AHospPDFBuilder p9Maker = new P9AHospPDFBuilder(resourcePath, p9A, sFilePDF, _notificationmessageEventname);
                p9Maker.GetP9AHOSPPDF();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }


        #endregion

        #region P9B

        public bool ShowP9B(P9BReportModel p9B, string sFilePDF)
        {
            bRet = false;
            try
            {
                P9BPDFBuilder p9BMaker = new P9BPDFBuilder(resourcePath, p9B, sFilePDF, _notificationmessageEventname);
                p9BMaker.GetP9BPDF();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        #endregion

        #region P10

        public bool ShowP10(P10ReportModel p10, string sFilePDF)
        {
            bRet = false;
            try
            {
                P10PDFMaker p10Maker = new P10PDFMaker(resourcePath, p10, sFilePDF, _notificationmessageEventname);
                p10Maker.GetP10PDF();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        #endregion

        #region P10A

        public bool ShowP10A(P10AReportModel p10a, string sFilePDF)
        {
            bRet = false;
            try
            {
                P10APDFBuilder p10AMaker = new P10APDFBuilder(resourcePath, p10a, sFilePDF, _notificationmessageEventname);
                p10AMaker.GetP10APDF();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        #endregion

        #region P11

        public bool ShowP11(P11ReportModel p11, string sFilePDF)
        {
            bRet = false;
            try
            {
                P11PDFBuilder p11Builder = new P11PDFBuilder(resourcePath, p11, sFilePDF, _notificationmessageEventname);
                p11Builder.GetP11PDF();
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        #endregion

        #region Payee

        public bool ShowPayee(string app, PAYEModel payModel, string sFilePDF)
        {

            bRet = false;
            try
            {
                if ("pdf".Equals(app.ToLower()))
                {
                    PayeePDFBuilder pmaker = new PayeePDFBuilder(payModel, sFilePDF, _notificationmessageEventname);
                    pmaker.GetPayeePDF();
                    return true;
                }
                else
                {

                    PAYEExcelBuilder pe = new PAYEExcelBuilder(payModel, sFilePDF, _notificationmessageEventname);
                    pe.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }


        }

        #endregion

        #region Payroll Master

        public bool ShowPayrollMaster(System.Collections.Generic.List<string> earnings, System.Collections.Generic.List<string> deductions, string app, PayrollMasterModel pMaster, string sFilePDF)
        {
            bRet = false;
            try
            {
                if ("pdf".Equals(app.ToLower()))
                {
                    //PayrollMasterPDFBuilder payPdfMaker = new PayrollMasterPDFBuilder(pMaster, sFilePDF,connection);
                    PayrollMasterPDFBuilder2 payPdfMaker = new PayrollMasterPDFBuilder2(earnings, deductions, pMaster, sFilePDF, connection, _notificationmessageEventname);
                    payPdfMaker.GetPDF();
                }
                else
                {
                    //PayrollMasterExcelBuilder pe = new PayrollMasterExcelBuilder(pMaster, sFilePDF, connection, _notificationmessageEventname);
                    PayrollMasterExcelBuilder pe = new PayrollMasterExcelBuilder(earnings, deductions, pMaster, sFilePDF, connection, _notificationmessageEventname);
                    pe.GetExcel();

                }
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        #endregion

        #region NHIF

        public bool ShowNHIF(string app, NHIFReportModel nhifreportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {
                if ("pdf".Equals(app.ToLower()))
                {
                    NHIFPDFBuilder payPdfMaker = new NHIFPDFBuilder(nhifreportmodel, sFilePDF, _notificationmessageEventname);
                    payPdfMaker.GetPDF();
                    return true;
                }
                else
                {
                    NHIFExcelBuilder pe = new NHIFExcelBuilder(nhifreportmodel, sFilePDF, _notificationmessageEventname);
                    pe.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }
        }
        #endregion

        #region NSSF
        public bool ShowNSSF(DAL.Employer _employer, string app, NSSFReportModel nssfreportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {
                if ("pdf".Equals(app.ToLower()))
                {
                    NSSFPDFBuilder payPdfMaker = new NSSFPDFBuilder(_employer, nssfreportmodel, sFilePDF, connection, _notificationmessageEventname);
                    payPdfMaker.GetPDF();
                    return true;
                }
                else
                {
                    NSSFExcelBuilder pe = new NSSFExcelBuilder(nssfreportmodel, sFilePDF, _notificationmessageEventname);
                    pe.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }
        }
        public bool ShowNewNSSF(string app, NSSFReportModel nssfreportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {
                if ("pdf".Equals(app.ToLower()))
                {
                    NSSFPDFBuilder payPdfMaker = new NSSFPDFBuilder(nssfreportmodel, sFilePDF, connection, _notificationmessageEventname);
                    payPdfMaker.GetPDF();
                    return true;
                }
                else
                {
                    NSSFExcelBuilder pe = new NSSFExcelBuilder(nssfreportmodel, sFilePDF, _notificationmessageEventname);
                    pe.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }
        }
        #endregion

        #region BankTransfer

        public bool ShowBankTransfer(string app, BankTransferReportModel bbtreportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {
                if ("pdf".Equals(app.ToLower()))
                {
                    BankTransferPDFBuilder payPdfMaker = new BankTransferPDFBuilder(bbtreportmodel, sFilePDF, _notificationmessageEventname);
                    payPdfMaker.GetPDF();
                    return true;
                }
                else
                {

                    BankTransferExcelBuilder pe = new BankTransferExcelBuilder(bbtreportmodel, sFilePDF, _notificationmessageEventname);
                    pe.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }


        }
        #endregion

        #region BankBranchTransfer

        public bool ShowBankBranchTransfer(string app, BankTransferModel bbtreportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {
                if ("pdf".Equals(app.ToLower()))
                {
                    BankBranchTransferPDFBuilder payPdfMaker = new BankBranchTransferPDFBuilder(bbtreportmodel, sFilePDF, _notificationmessageEventname);
                    payPdfMaker.GetPDF();
                    return true;
                }
                else
                {

                    BankBranchExcelBuilder pe = new BankBranchExcelBuilder(bbtreportmodel, sFilePDF, _notificationmessageEventname);
                    pe.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }
        }

        #endregion

        #region schedule
        public bool ShowShedule(string app, string itemid, SheduleReportModel model, string sFilePDF)
        {

            bRet = false;
            if ("pdf".Equals(app))
            {
                try
                {
                    ShedulePDFBuilder smaker = new ShedulePDFBuilder(itemid, model, sFilePDF, connection, _notificationmessageEventname);
                    smaker.GetshedulePDF();
                    return true;

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return false;
                }
            }
            else //application is excel
            {
                try
                {
                    ScheduleExcelBuilder schExcelBuilder = new ScheduleExcelBuilder(itemid, model, sFilePDF, connection, _notificationmessageEventname);
                    schExcelBuilder.GetExcel();
                    return true;

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return false;
                }
            }

        }

        #endregion

        #region loanrepaymentschedule

        public bool ShowLoanRepaymentShedule(string app, LoanRepaymentScheduleModel loanrepaymentshedulemodel, string sFilePDF)
        {

            bRet = false;
            if ("pdf".Equals(app))
            {
                try
                {
                    LoanRepaymentSchedulePDFBuilder lrspdfbuilder = new LoanRepaymentSchedulePDFBuilder(loanrepaymentshedulemodel, sFilePDF, _notificationmessageEventname);
                    lrspdfbuilder.GetshedulePDF();
                    return true;

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return false;
                }
            }
            else //application is excel
            {
                try
                {
                    LoanRepaymentScheduleExcelBuilder lrsebuilder = new LoanRepaymentScheduleExcelBuilder(loanrepaymentshedulemodel, sFilePDF, _notificationmessageEventname);
                    lrsebuilder.GetExcel();
                    return true;

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return false;
                }
            }

        }

        #endregion

        #region saccopaymentschedule

        public bool ShowSaccoPaymentShedule(string app, SaccoPaymentScheduleModel saccopaymentshedulemodel, string sFilePDF)
        {

            bRet = false;
            if ("pdf".Equals(app))
            {
                try
                {
                    SaccoPaymentSchedulePDFBuilder lrspdfbuilder = new SaccoPaymentSchedulePDFBuilder(saccopaymentshedulemodel, sFilePDF, _notificationmessageEventname);
                    lrspdfbuilder.GetshedulePDF();
                    return true;

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return false;
                }
            }
            else //application is excel
            {
                try
                {
                    SaccoPaymentScheduleExcelBuilder lrsebuilder = new SaccoPaymentScheduleExcelBuilder(saccopaymentshedulemodel, sFilePDF, _notificationmessageEventname);
                    lrsebuilder.GetExcel();
                    return true;

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return false;
                }
            }

        }

        #endregion

        #region statement

        public bool ShowStatement(string app, string itemid, BLL.KRA.Models.StatementModel statementmodel, string sFilePDF)
        {

            bRet = false;
            if ("pdf".Equals(app))
            {
                try
                {
                    StatementPDFBuilder
                        statementpdfbuilder = new StatementPDFBuilder(itemid, statementmodel, sFilePDF, connection, _notificationmessageEventname);
                    statementpdfbuilder.GetstatementPDF();
                    return true;

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return false;
                }
            }
            else
            {
                try
                {
                    StatementExcelBuilder sexcel = new StatementExcelBuilder(itemid, statementmodel, sFilePDF, connection, _notificationmessageEventname);
                    sexcel.GetExcel();

                    return true;

                }
                catch (Exception ex)
                {
                    Utils.ShowError(ex);
                    return false;
                }
            }

        }

        #endregion

        #region NetSalary

        public bool ShowNetSalary(string app, NetSalaryReportModel netsalaryreportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {

                if ("pdf".Equals(app.ToLower()))
                {
                    NetSalaryPDFBuilder netsalarypdfbuilder = new NetSalaryPDFBuilder(netsalaryreportmodel, sFilePDF, _notificationmessageEventname);
                    netsalarypdfbuilder.GetNetSalaryPDF();
                    return true;
                }
                else
                {

                    winSBPayroll.Reports.ExcelBuilder.NetSalaryExcelBuilder netsalaryexcelbuilder = new winSBPayroll.Reports.ExcelBuilder.NetSalaryExcelBuilder(netsalaryreportmodel, sFilePDF, _notificationmessageEventname);
                    netsalaryexcelbuilder.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        #endregion

        #region Employees

        public bool ShowBankEmployees(string app, BankEmployeesModelReport employeereportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {

                if ("pdf".Equals(app.ToLower()))
                {
                    BankEmployeesPDFBuilder employeespdfbuilder = new BankEmployeesPDFBuilder(employeereportmodel, sFilePDF, _notificationmessageEventname);
                    employeespdfbuilder.GetEmployeePDF();
                    return true;
                }
                else
                {

                    BankEmployeeExcelBuilder employeeexcelbuilder = new BankEmployeeExcelBuilder(employeereportmodel, sFilePDF, _notificationmessageEventname);
                    employeeexcelbuilder.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        public bool ShowCashEmployees(string app, CashEmployeesModelReport employeereportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {

                if ("pdf".Equals(app.ToLower()))
                {
                    CashEmployeesPDFBuilder employeespdfbuilder = new CashEmployeesPDFBuilder(employeereportmodel, sFilePDF, _notificationmessageEventname);
                    employeespdfbuilder.GetEmployeePDF();
                    return true;
                }
                else
                {

                    CashEmployeeExcelBuilder employeeexcelbuilder = new CashEmployeeExcelBuilder(employeereportmodel, sFilePDF, _notificationmessageEventname);
                    employeeexcelbuilder.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        public bool ShowMpesaEmployees(string app, MpesaEmployeesModelReport employeereportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {

                if ("pdf".Equals(app.ToLower()))
                {
                    MpesaEmployeesPDFBuilder employeespdfbuilder = new MpesaEmployeesPDFBuilder(employeereportmodel, sFilePDF, _notificationmessageEventname);
                    employeespdfbuilder.GetEmployeePDF();
                    return true;
                }
                else
                {

                    MpesaEmployeeExcelBuilder employeeexcelbuilder = new MpesaEmployeeExcelBuilder(employeereportmodel, sFilePDF, _notificationmessageEventname);
                    employeeexcelbuilder.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }
        public bool ShowEmployees(string app, EmployeesModelReport employeereportmodel, string sFilePDF)
        {
            bRet = false;
            try
            {

                if ("pdf".Equals(app.ToLower()))
                {
                    EmployeesPDFBuilder employeespdfbuilder = new EmployeesPDFBuilder(employeereportmodel, sFilePDF, _notificationmessageEventname);
                    employeespdfbuilder.GetEmployeePDF();
                    return true;
                }
                else
                {

                    EmployeeExcelBuilder employeeexcelbuilder = new EmployeeExcelBuilder(employeereportmodel, sFilePDF, _notificationmessageEventname);
                    employeeexcelbuilder.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        #endregion

        #region Advance

        public bool ShowAdvanceSchedule(string app, AdvanceReportModel advancereport, string sFilePDF)
        {
            bRet = false;
            try
            {

                if ("pdf".Equals(app.ToLower()))
                {
                    AdvancePDFBuilder advancepdfbuilder = new AdvancePDFBuilder(advancereport, sFilePDF, _notificationmessageEventname);
                    advancepdfbuilder.GetPDF();
                    return true;
                }
                else
                {

                    AdvanceExcelBuilder advanceexcelbuilder = new AdvanceExcelBuilder(advancereport, sFilePDF, _notificationmessageEventname);
                    advanceexcelbuilder.GetExcel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return false;
            }

        }

        #endregion


    }
}