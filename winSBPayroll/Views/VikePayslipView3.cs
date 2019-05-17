using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CommonLib;
using BLL.DataEntry;
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using winSBPayroll.ViewModel;

namespace winSBPayroll.Views
{
    public class VikepayslipView3
    {

        VikePayslipViewModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        #region "fonts"
        //DEFINED FONTS
        Font employerNameFont;
        Font documentHeadersFont;
        Font bodyHeaderFont;
        Font bodyHeaderDataFont;
        Font deductionsFont;
        Font tableHeaderCellFont;
        Font tableDataCellFont;
        Font signatureFont;
        #endregion "fonts"

        public VikepayslipView3(VikePayslipViewModel PayslipModel, string FileName, string Conn)
        {
            if (PayslipModel == null)
                throw new ArgumentNullException("VikePayslipViewModel is null");
            _ViewModel = PayslipModel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            sFilePDF = FileName;

            InitializeFonts();
        }

        #region "fonts"
        public void InitializeFonts()
        {
            try
            {
                //DEFINED FONTS
                employerNameFont = new Font(int.Parse(rep.SettingLookup("payslipEmployerNameFontFamily")), float.Parse(rep.SettingLookup("payslipEmployerNameFontSize")), Font.BOLD);
                documentHeadersFont = new Font(int.Parse(rep.SettingLookup("payslipDocumentHeadersFontFamily")), float.Parse(rep.SettingLookup("payslipDocumentHeadersFontSize")), Font.BOLD);
                bodyHeaderFont = new Font(int.Parse(rep.SettingLookup("payslipBodyHeaderFontFamily")), float.Parse(rep.SettingLookup("payslipBodyHeaderFontSize")), Font.NORMAL);
                bodyHeaderDataFont = new Font(int.Parse(rep.SettingLookup("payslipBodyHeaderDataFontFamily")), float.Parse(rep.SettingLookup("payslipBodyHeaderDataFontSize")), Font.NORMAL);
                deductionsFont = new Font(int.Parse(rep.SettingLookup("payslipDeductionsFontFamily")), float.Parse(rep.SettingLookup("payslipDeductionsFontSize")), Font.BOLD);
                tableHeaderCellFont = new Font(int.Parse(rep.SettingLookup("payslipTableHeaderCellFontFamily")), float.Parse(rep.SettingLookup("payslipTableHeaderCellFontSize")), Font.BOLD);
                tableDataCellFont = new Font(int.Parse(rep.SettingLookup("payslipTableDataCellFontFamily")), float.Parse(rep.SettingLookup("payslipTableDataCellFontSize")), Font.NORMAL);
                signatureFont = new Font(int.Parse(rep.SettingLookup("payslipSignatureFontFamily")), float.Parse(rep.SettingLookup("payslipSignatureFontSize")), Font.NORMAL);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        #endregion "fonts"

        public string GetPDF()
        {
            try
            {
                BuildPDF();
                return sFilePDF;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        public void BuildPDF()
        {
            try
            {
                //step 1: creation of a document-object
                document = new Document(PageSize.A4, 10, 10, 10, 10);

                //step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document,
                    new FileStream(sFilePDF, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));

                //open document
                document.Open();

                int Cols = 3;
                Table payslipsTable = new Table(Cols);
                payslipsTable.WidthPercentage = 100;
                payslipsTable.Width = 100;
                payslipsTable.Padding = 1;
                payslipsTable.Border = 0;
                payslipsTable.BorderWidth = 0;
                payslipsTable.BorderWidthBottom = 0;
                payslipsTable.BorderWidthTop = 0;
                payslipsTable.BorderWidthLeft = 0;
                payslipsTable.BorderWidthRight = 0;

                int[] colWidthPercentages = new int[Cols];
                colWidthPercentages[0] = 45;
                colWidthPercentages[1] = 5;
                colWidthPercentages[2] = 45;

                payslipsTable.SetWidths(colWidthPercentages);

                //Add  Header 
                AddHeader(payslipsTable);

                //Add Payments 
                AddPayments(payslipsTable);

                //Add Payee Details 
                AddPAYEDetails(payslipsTable);

                //Add Deductions
                AddDeductions(payslipsTable);

                //Add Summary
                AddSummary(payslipsTable);

                //Add Other Payments
                AddOtherPayments(payslipsTable);

                //Add Non Cash Payments
                AddNonCashPayments(payslipsTable);

                //Add Other Deductions
                AddOtherDeductions(payslipsTable);

                //Add Employee Loans
                AddEmployeeLoans(payslipsTable);

                //Add Sacco Contributions
                AddSACCOContributions(payslipsTable);

                //Add Document Footer
                //AddDocFooter(payslipsTable);

                document.Add(payslipsTable);

                //Finished
                document.Close();
            }
            catch (DocumentException de)
            {
                this.Message = de.Message;
            }
            catch (IOException ioe)
            {
                this.Message = ioe.Message;
            }
            catch (Exception ex)
            {
               Log.WriteToErrorLogFile(ex);
            }
        }

        private void AddHeader(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Padding = 1;
                    payslipTable11.Border = Rectangle.BOX;

                    Cell aCell = new Cell(new Phrase(_ViewModel.EmployerName.ToUpper().Trim() + "    " + _ViewModel.EmployerAddress.Trim() + "\n", employerNameFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    payslipTable11.AddCell(aCell);

                    Cell bCell = new Cell(new Phrase("PAYSLIP", new Font(Font.TIMES_ROMAN, 9, Font.BOLD, Color.BLACK)));
                    bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    bCell.Colspan = 2;
                    bCell.Border = Cell.BOX;
                    payslipTable11.AddCell(bCell);

                    Cell emCell1 = new Cell(new Phrase("Payroll Period:", bodyHeaderFont));
                    emCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    emCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(emCell1);

                    Cell empCell2 = new Cell(new Phrase(_ViewModel.PayrollMonth + " / " + _ViewModel.PayrollYear.ToString(), bodyHeaderDataFont));
                    empCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    empCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(empCell2);

                    Cell printedonCell1 = new Cell(new Phrase("Printed On:", bodyHeaderFont));
                    printedonCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    printedonCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(printedonCell1);

                    Cell printedonCell2 = new Cell(new Phrase(_ViewModel.PrintedOn.ToString("dd-MMM-yyyy"), bodyHeaderDataFont));
                    printedonCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    printedonCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(printedonCell2);

                    Cell cCell1 = new Cell(new Phrase("Employee No:", bodyHeaderFont));
                    cCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(cCell1);

                    Cell cCell2 = new Cell(new Phrase(_ViewModel.EmpNo, bodyHeaderDataFont));
                    cCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(cCell2);

                    Cell dCell1 = new Cell(new Phrase("Employee :", bodyHeaderFont));
                    dCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    dCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(dCell1);

                    Cell dCell2 = new Cell(new Phrase(_ViewModel.EmpName, bodyHeaderDataFont));
                    dCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    dCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(dCell2);

                    Cell eCell1 = new Cell(new Phrase("Pin No:", bodyHeaderFont));
                    eCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    eCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(eCell1);

                    Cell eCell2 = new Cell(new Phrase(_ViewModel.PINNo, bodyHeaderDataFont));
                    eCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    eCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(eCell2);

                    Cell nssfCell1 = new Cell(new Phrase("NSSF No:", bodyHeaderFont));
                    nssfCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nssfCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(nssfCell1);

                    Cell nssfCell2 = new Cell(new Phrase(_ViewModel.NSSFNO, bodyHeaderDataFont));
                    nssfCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nssfCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(nssfCell2);

                    Cell nhifCell1 = new Cell(new Phrase("NHIF No:", bodyHeaderFont));
                    nhifCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nhifCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(nhifCell1);

                    Cell nhifCell2 = new Cell(new Phrase(_ViewModel.NHIFNO, bodyHeaderDataFont));
                    nhifCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nhifCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(nhifCell2);

                    Cell departmentCell1 = new Cell(new Phrase("Department:", bodyHeaderFont));
                    departmentCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    departmentCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(departmentCell1);

                    Cell departmentCell2 = new Cell(new Phrase(_ViewModel.Department, bodyHeaderDataFont));
                    departmentCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    departmentCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(departmentCell2);

                    _payslipTable.InsertTable(payslipTable11);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;



                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddPayments(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(2);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell aCell = new Cell(new Phrase("PAYMENTS\n", documentHeadersFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    aCell.Border = Cell.BOX;
                    payslipTable1.AddCell(aCell);

                    foreach (var op in _ViewModel.ExcludeOtherPayments)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);
                    }

                    Cell topCell = new Cell(new Phrase("Total Other Payments", tableDataCellFont));
                    topCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(topCell);

                    Cell topvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalOtherPayments), tableDataCellFont));
                    topvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(topvCell);

                    Cell saCell = new Cell(new Phrase("TOTAL PAYMENTS", tableHeaderCellFont));
                    saCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(saCell);

                    Cell tpCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalPayments), tableHeaderCellFont));
                    tpCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(tpCell);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;



                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }

        }

        private void AddPAYEDetails(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(2);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell aCell = new Cell(new Phrase("PAYE DETAILS\n", documentHeadersFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    aCell.Border = Cell.BOX;
                    payslipTable1.AddCell(aCell);

                    Cell tCell = new Cell(new Phrase("TAXABLE PAY", tableHeaderCellFont));
                    tCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tCell);

                    Cell t1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PayeDetail.TaxablePay), tableHeaderCellFont));
                    t1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(t1Cell);

                    Cell tdCell = new Cell(new Phrase("TAX DUE", tableHeaderCellFont));
                    tdCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tdCell);

                    Cell td1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PayeDetail.TaxDue), tableHeaderCellFont));
                    td1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(td1Cell);

                    Cell prCell = new Cell(new Phrase("Personal Relief", tableDataCellFont));
                    prCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(prCell);

                    Cell pr1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PayeDetail.PersonalRelief), tableDataCellFont));
                    pr1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(pr1Cell);

                    Cell insuranceCell = new Cell(new Phrase("Life Insurance Relief", tableDataCellFont));
                    insuranceCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(insuranceCell);

                    Cell insuranceCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PayeDetail.InsuranceRelief), tableDataCellFont));
                    insuranceCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(insuranceCell1);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;


                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddDeductions(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(2);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell aCell = new Cell(new Phrase("DEDUCTIONS\n", documentHeadersFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    aCell.Border = Cell.BOX;
                    payslipTable1.AddCell(aCell);

                    foreach (var op in _ViewModel.ExcludeOtherDeductions)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);
                    }

                    Cell ttlCell = new Cell(new Phrase("Total Loans", tableDataCellFont));
                    ttlCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(ttlCell);

                    Cell ttlvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalAmountLoansAmount), tableDataCellFont));
                    ttlvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(ttlvCell);

                    Cell tsaccoCell = new Cell(new Phrase("Total Welfare Contributions", tableDataCellFont));
                    tsaccoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tsaccoCell);

                    Cell ttsaccoCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalAmountSaccoAmount), tableDataCellFont));
                    ttsaccoCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(ttsaccoCell);

                    Cell todCell = new Cell(new Phrase("Total Other Deductions", tableDataCellFont));
                    todCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(todCell);

                    Cell todvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalOtherDeductions), tableDataCellFont));
                    todvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(todvCell);

                    Cell tdCell = new Cell(new Phrase("TOTAL DEDUCTIONS", tableHeaderCellFont));
                    tdCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tdCell);

                    Cell td1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalDeductions), tableHeaderCellFont));
                    td1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(td1Cell);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;



                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddSummary(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(2);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell npCell = new Cell(new Phrase("NET PAY", tableHeaderCellFont));
                    npCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(npCell);

                    Cell npvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NetPay), tableHeaderCellFont));
                    npvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(npvCell);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;


                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void PayslipOtherPaymentsDeductionsHeaders(Table aTable)
        {
            try
            {
                List<string> th = new List<string>() { "Description", "Amount" };

                foreach (var e in th)
                {

                    Cell desCell = new Cell(new Phrase(e, deductionsFont));
                    desCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aTable.AddCell(desCell);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddOtherPayments(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(2);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell aCell = new Cell(new Phrase("Other Payments\n", documentHeadersFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    aCell.Border = Cell.BOX;
                    payslipTable1.AddCell(aCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable1);

                    foreach (var op in _ViewModel.OtherPayments)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);
                    }

                    Cell erCell = new Cell(new Phrase("Totals", deductionsFont));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalOtherPayments), tableDataCellFont));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(mvCell);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;



                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }
        private void AddNonCashPayments(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(2);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell aCell = new Cell(new Phrase("Non Cash Payments\n", documentHeadersFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    aCell.Border = Cell.BOX;
                    payslipTable1.AddCell(aCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable1);

                    foreach (var op in _ViewModel.NonCashPayments)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);
                    }

                    Cell erCell = new Cell(new Phrase("Totals", deductionsFont));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalNonCashPayments), tableDataCellFont));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(mvCell);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;



                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }
        private void AddOtherDeductions(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(2);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell aCell = new Cell(new Phrase("Other Deductions\n", documentHeadersFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    aCell.Border = Cell.BOX;
                    payslipTable1.AddCell(aCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable1);

                    foreach (var op in _ViewModel.OtherDeductions)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);
                    }

                    Cell erCell = new Cell(new Phrase("Totals", deductionsFont));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalOtherDeductions), tableDataCellFont));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(mvCell);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void PayslipLoanDetailsHeader(Table aTable)
        {
            try
            {
                List<string> th = new List<string>() { "Description", "Amount", "Balance" };

                foreach (var e in th)
                {

                    Cell desCell = new Cell(new Phrase(e, deductionsFont));
                    desCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aTable.AddCell(desCell);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void PayslipSaccoDetailsHeader(Table aTable)
        {
            try
            {
                List<string> th = new List<string>() { "Description", "Amount", "Total Shares" };

                foreach (var e in th)
                {

                    Cell desCell = new Cell(new Phrase(e, deductionsFont));
                    desCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aTable.AddCell(desCell);
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddEmployeeLoans(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(3);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell aCell = new Cell(new Phrase("Employee LOANS\n", documentHeadersFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 3;
                    aCell.Border = Cell.BOX;
                    payslipTable1.AddCell(aCell);

                    PayslipLoanDetailsHeader(payslipTable1);

                    foreach (var op in _ViewModel.Loans)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);

                        decimal _tbalance = decimal.Parse(op.YTD.ToString());
                        if (_tbalance < 0)
                        {
                            _tbalance = _tbalance * -1;
                        }
                        Cell lbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _tbalance), tableDataCellFont));
                        lbCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(lbCell);

                    }

                    Cell erCell = new Cell(new Phrase("Totals", deductionsFont));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalAmountLoansAmount), tableDataCellFont));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(mvCell);

                    decimal _balance = decimal.Parse(_ViewModel.TotalAmountLoansBalance.ToString());
                    if (_balance < 0)
                    {
                        _balance = _balance * -1;
                    }
                    Cell ltbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _balance), tableDataCellFont));
                    ltbCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(ltbCell);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(3);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;


                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddSACCOContributions(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(3);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell aCell = new Cell(new Phrase("Welfare Contributions\n", documentHeadersFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Border = Cell.BOX;
                    aCell.Colspan = 3;
                    payslipTable1.AddCell(aCell);

                    PayslipSaccoDetailsHeader(payslipTable1);

                    foreach (var op in _ViewModel.SACCOContributions)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);

                        Cell scCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.YTD), tableDataCellFont));
                        scCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(scCell);
                    }

                    Cell erCell = new Cell(new Phrase("Totals", deductionsFont));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalAmountSaccoAmount), tableDataCellFont));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(mvCell);

                    Cell ltbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalAmountSaccoBalance), tableDataCellFont));
                    ltbCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(ltbCell);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(3);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;



                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddDocFooter(Table _payslipTable)
        {
            try
            {
                #region "payslip1"
                if (_ViewModel == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable1 = new Table(1);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Table.NO_BORDER;

                    Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", signatureFont));
                    sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    sgCell.Border = Cell.NO_BORDER;
                    payslipTable1.AddCell(sgCell);

                    _payslipTable.InsertTable(payslipTable1);

                }
                #endregion "payslip1"

                #region "middle column"
                Table middleTable = new Table(2);
                middleTable.WidthPercentage = 100;
                middleTable.Padding = 1;

                _payslipTable.InsertTable(middleTable);
                #endregion "middle column"

                #region "payslip2"
                if (_ViewModel == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel != null)
                {

                    Table payslipTable2 = new Table(1);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Table.NO_BORDER;



                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
                //document.Add(payslipTable1);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }












    }
}