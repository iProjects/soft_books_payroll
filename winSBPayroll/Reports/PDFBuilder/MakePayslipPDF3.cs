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

namespace winSBPayroll.Reports.PDF
{
    public class MakePayslipPDF3
    {

        Payslip _ViewModel1;
        Payslip _ViewModel2;
        Document document;
        string Message;
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
         
        public MakePayslipPDF3(Payslip PayslipModel1, Payslip PayslipModel2, Document doc, string Conn)
        {
            //if (PayslipModel1 == null)
            //    throw new ArgumentNullException("PayslipViewModel is null");
            _ViewModel1 = PayslipModel1;

            //if (PayslipModel2 == null)
            //    throw new ArgumentNullException("PayslipViewModel is null");
            _ViewModel2 = PayslipModel2;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            document = doc;

            InitializeFonts();
        }

        #region "fonts"
        public void InitializeFonts()
        {
            try
            {
                //DEFINED FONTS
                employerNameFont = new Font(int.Parse(rep.SettingLookup("AllpayslipEmployerNameFontFamily")), float.Parse(rep.SettingLookup("AllpayslipEmployerNameFontSize")), Font.BOLD);
                documentHeadersFont = new Font(int.Parse(rep.SettingLookup("AllpayslipDocumentHeadersFontFamily")), float.Parse(rep.SettingLookup("AllpayslipDocumentHeadersFontSize")), Font.BOLD);
                bodyHeaderFont = new Font(int.Parse(rep.SettingLookup("AllpayslipBodyHeaderFontFamily")), float.Parse(rep.SettingLookup("AllpayslipBodyHeaderFontSize")), Font.NORMAL);
                bodyHeaderDataFont = new Font(int.Parse(rep.SettingLookup("AllpayslipBodyHeaderDataFontFamily")), float.Parse(rep.SettingLookup("AllpayslipBodyHeaderDataFontSize")), Font.NORMAL);
                deductionsFont = new Font(int.Parse(rep.SettingLookup("AllpayslipDeductionsFontFamily")), float.Parse(rep.SettingLookup("AllpayslipDeductionsFontSize")), Font.BOLD);
                tableHeaderCellFont = new Font(int.Parse(rep.SettingLookup("AllpayslipTableHeaderCellFontFamily")), float.Parse(rep.SettingLookup("AllpayslipTableHeaderCellFontSize")), Font.BOLD);
                tableDataCellFont = new Font(int.Parse(rep.SettingLookup("AllpayslipTableDataCellFontFamily")), float.Parse(rep.SettingLookup("AllpayslipTableDataCellFontSize")), Font.NORMAL);
                signatureFont = new Font(int.Parse(rep.SettingLookup("AllpayslipSignatureFontFamily")), float.Parse(rep.SettingLookup("AllpayslipSignatureFontSize")), Font.NORMAL);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        #endregion "fonts"


        public void BuildPDF()
        {
            try
            {
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100; 
                    payslipTable11.Padding = 1;
                    payslipTable11.Border = Rectangle.BOX;

                    Cell aCell = new Cell(new Phrase(_ViewModel1.EmployerName.ToUpper().Trim() + "    " + _ViewModel1.EmployerAddress.Trim() + "\n", employerNameFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    payslipTable11.AddCell(aCell);

                    Cell bCell = new Cell(new Phrase("PAYSLIP", new Font(Font.TIMES_ROMAN, 10, Font.BOLD, Color.BLACK)));
                    bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    bCell.Colspan = 2;
                    bCell.Border = Cell.BOX;
                    payslipTable11.AddCell(bCell);

                    Cell emCell1 = new Cell(new Phrase("Payroll Period:", bodyHeaderFont));
                    emCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    emCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(emCell1);

                    Cell empCell2 = new Cell(new Phrase(_ViewModel1.PayrollMonth + " / " + _ViewModel1.PayrollYear.ToString(), bodyHeaderDataFont));
                    empCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    empCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(empCell2);

                    Cell printedonCell1 = new Cell(new Phrase("Printed On:", bodyHeaderFont));
                    printedonCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    printedonCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(printedonCell1);

                    Cell printedonCell2 = new Cell(new Phrase(_ViewModel1.PrintedOn.ToString("dd-MMM-yyyy"), bodyHeaderDataFont));
                    printedonCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    printedonCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(printedonCell2);

                    Cell cCell1 = new Cell(new Phrase("Employee No:", bodyHeaderFont));
                    cCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(cCell1);

                    Cell cCell2 = new Cell(new Phrase(_ViewModel1.EmpNo, bodyHeaderDataFont));
                    cCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    cCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(cCell2);

                    Cell dCell1 = new Cell(new Phrase("Employee :", bodyHeaderFont));
                    dCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    dCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(dCell1);

                    Cell dCell2 = new Cell(new Phrase(_ViewModel1.EmpName, bodyHeaderDataFont));
                    dCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    dCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(dCell2);

                    Cell eCell1 = new Cell(new Phrase("Pin No:", bodyHeaderFont));
                    eCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    eCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(eCell1);

                    Cell eCell2 = new Cell(new Phrase(_ViewModel1.PINNo, bodyHeaderDataFont));
                    eCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    eCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(eCell2);

                    Cell nssfCell1 = new Cell(new Phrase("NSSF No:", bodyHeaderFont));
                    nssfCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nssfCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(nssfCell1);

                    Cell nssfCell2 = new Cell(new Phrase(_ViewModel1.NSSFNO, bodyHeaderDataFont));
                    nssfCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nssfCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(nssfCell2);

                    Cell nhifCell1 = new Cell(new Phrase("NHIF No:", bodyHeaderFont));
                    nhifCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nhifCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(nhifCell1);

                    Cell nhifCell2 = new Cell(new Phrase(_ViewModel1.NHIFNO, bodyHeaderDataFont));
                    nhifCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nhifCell2.Border = Cell.BOX;
                    payslipTable11.AddCell(nhifCell2);

                    Cell departmentCell1 = new Cell(new Phrase("Department:", bodyHeaderFont));
                    departmentCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    departmentCell1.Border = Cell.BOX;
                    payslipTable11.AddCell(departmentCell1);

                    Cell departmentCell2 = new Cell(new Phrase(_ViewModel1.Department, bodyHeaderDataFont));
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell aCell = new Cell(new Phrase(_ViewModel2.EmployerName.ToUpper().Trim() + "    " + _ViewModel2.EmployerAddress.Trim() + "\n", employerNameFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    payslipTable2.AddCell(aCell);

                    Cell bCell = new Cell(new Phrase("PAYSLIP", new Font(Font.TIMES_ROMAN, 10, Font.BOLD, Color.BLACK)));
                    bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    bCell.Colspan = 2;
                    bCell.Border = Cell.BOX;
                    payslipTable2.AddCell(bCell);

                    Cell emCell1 = new Cell(new Phrase("Payroll Period:", bodyHeaderFont));
                    emCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    emCell1.Border = Cell.BOX;
                    payslipTable2.AddCell(emCell1);

                    Cell empCell2 = new Cell(new Phrase(_ViewModel2.PayrollMonth + " / " + _ViewModel2.PayrollYear.ToString(), bodyHeaderDataFont));
                    empCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    empCell2.Border = Cell.BOX;
                    payslipTable2.AddCell(empCell2);

                    Cell printedCell1 = new Cell(new Phrase("Printed On:", bodyHeaderFont));
                    printedCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    printedCell1.Border = Cell.BOX;
                    payslipTable2.AddCell(printedCell1);

                    Cell printedCell2 = new Cell(new Phrase(_ViewModel2.PrintedOn.ToString("dd-MMM-yyyy"), bodyHeaderDataFont));
                    printedCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    printedCell2.Border = Cell.BOX;
                    payslipTable2.AddCell(printedCell2);

                    Cell noCell = new Cell(new Phrase("Employee No:", bodyHeaderFont));
                    noCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    noCell.Border = Cell.BOX;
                    payslipTable2.AddCell(noCell);

                    Cell noCell2 = new Cell(new Phrase(_ViewModel2.EmpNo, bodyHeaderDataFont));
                    noCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    noCell2.Border = Cell.BOX;
                    payslipTable2.AddCell(noCell2);

                    Cell empnameCell1 = new Cell(new Phrase("Employee :", bodyHeaderFont));
                    empnameCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    empnameCell1.Border = Cell.BOX;
                    payslipTable2.AddCell(empnameCell1);

                    Cell empnameCell2 = new Cell(new Phrase(_ViewModel2.EmpName, bodyHeaderDataFont));
                    empnameCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    empnameCell2.Border = Cell.BOX;
                    payslipTable2.AddCell(empnameCell2);

                    Cell pinCell = new Cell(new Phrase("Pin No:", bodyHeaderFont));
                    pinCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    pinCell.Border = Cell.BOX;
                    payslipTable2.AddCell(pinCell);

                    Cell pinCell2 = new Cell(new Phrase(_ViewModel2.PINNo, bodyHeaderDataFont));
                    pinCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    pinCell2.Border = Cell.BOX;
                    payslipTable2.AddCell(pinCell2);

                    Cell nssfCell = new Cell(new Phrase("NSSF No:", bodyHeaderFont));
                    nssfCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nssfCell.Border = Cell.BOX;
                    payslipTable2.AddCell(nssfCell);

                    Cell nssfnoCell2 = new Cell(new Phrase(_ViewModel2.NSSFNO, bodyHeaderDataFont));
                    nssfnoCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nssfnoCell2.Border = Cell.BOX;
                    payslipTable2.AddCell(nssfnoCell2);

                    Cell nhifnoCell1 = new Cell(new Phrase("NHIF No:", bodyHeaderFont));
                    nhifnoCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nhifnoCell1.Border = Cell.BOX;
                    payslipTable2.AddCell(nhifnoCell1);

                    Cell nhifnoCell2 = new Cell(new Phrase(_ViewModel2.NHIFNO, bodyHeaderDataFont));
                    nhifnoCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    nhifnoCell2.Border = Cell.BOX;
                    payslipTable2.AddCell(nhifnoCell2);

                    Cell deptCell2 = new Cell(new Phrase("Department:", bodyHeaderFont));
                    deptCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    deptCell2.Border = Cell.BOX;
                    payslipTable2.AddCell(deptCell2);

                    Cell deptCell1 = new Cell(new Phrase(_ViewModel2.Department, bodyHeaderDataFont));
                    deptCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    deptCell1.Border = Cell.BOX;
                    payslipTable2.AddCell(deptCell1);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"

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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
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

                    foreach (var op in _ViewModel1.ExcludeOtherPayments)
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

                    Cell topvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalOtherPayments), tableDataCellFont));
                    topvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(topvCell);

                    Cell saCell = new Cell(new Phrase("TOTAL PAYMENTS", tableHeaderCellFont));
                    saCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(saCell);

                    Cell tpCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalPayments), tableHeaderCellFont));
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
                if (_ViewModel2 == null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;
                    payslipTable2.HasBorders();
                    payslipTable2.HasToFitPageTable();
                    payslipTable2.DefaultRowspan = int.Parse((_ViewModel1.ExcludeOtherPayments.Count * -1).ToString());

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {
                    int rows = 1 + _ViewModel1.ExcludeOtherPayments.Count + 2;

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;
                    payslipTable2.HasBorders();
                    payslipTable2.HasToFitPageTable();

                    Cell titleCell = new Cell(new Phrase("PAYMENTS\n", documentHeadersFont));
                    titleCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    titleCell.Colspan = 2;
                    titleCell.Border = Cell.BOX;
                    payslipTable2.AddCell(titleCell);

                    foreach (var op in _ViewModel2.ExcludeOtherPayments)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(aopCell);
                    }

                    Cell topCell = new Cell(new Phrase("Total Other Payments", tableDataCellFont));
                    topCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(topCell);

                    Cell topvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalOtherPayments), tableDataCellFont));
                    topvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(topvCell);

                    Cell totalCell = new Cell(new Phrase("TOTAL PAYMENTS", tableHeaderCellFont));
                    totalCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalCell);

                    Cell totalCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalPayments), tableHeaderCellFont));
                    totalCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalCell1);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
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

                    Cell t1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.PayeDetail.TaxablePay), tableHeaderCellFont));
                    t1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(t1Cell);

                    Cell tdCell = new Cell(new Phrase("TAX DUE", tableHeaderCellFont));
                    tdCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tdCell);

                    Cell td1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.PayeDetail.TaxDue), tableHeaderCellFont));
                    td1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(td1Cell);

                    Cell prCell = new Cell(new Phrase("Personal Relief", tableDataCellFont));
                    prCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(prCell);

                    Cell pr1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.PayeDetail.PersonalRelief), tableDataCellFont));
                    pr1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(pr1Cell);

                    Cell insuranceCell = new Cell(new Phrase("Life Insurance Relief", tableDataCellFont));
                    insuranceCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(insuranceCell);

                    Cell insuranceCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.PayeDetail.InsuranceRelief), tableDataCellFont));
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell payeCell1 = new Cell(new Phrase("PAYE DETAILS\n", documentHeadersFont));
                    payeCell1.HorizontalAlignment = Cell.ALIGN_CENTER;
                    payeCell1.Colspan = 2;
                    payeCell1.Border = Cell.BOX;
                    payslipTable2.AddCell(payeCell1);

                    Cell taxCell1 = new Cell(new Phrase("TAXABLE PAY", tableHeaderCellFont));
                    taxCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(taxCell1);

                    Cell taxCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.PayeDetail.TaxablePay), tableHeaderCellFont));
                    taxCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(taxCell2);

                    Cell taxCell3 = new Cell(new Phrase("TAX DUE", tableHeaderCellFont));
                    taxCell3.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(taxCell3);

                    Cell payeedetCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.PayeDetail.TaxDue), tableHeaderCellFont));
                    payeedetCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(payeedetCell);

                    Cell personalCell = new Cell(new Phrase("Personal Relief", tableDataCellFont));
                    personalCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(personalCell);

                    Cell personalCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.PayeDetail.PersonalRelief), tableDataCellFont));
                    personalCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(personalCell1);

                    Cell insuranceCell2 = new Cell(new Phrase("Life Insurance Relief", tableDataCellFont));
                    insuranceCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(insuranceCell2);

                    Cell insuranceCell3 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.PayeDetail.InsuranceRelief), tableDataCellFont));
                    insuranceCell3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(insuranceCell3);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
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

                    foreach (var op in _ViewModel1.ExcludeOtherDeductions)
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

                    Cell ttlvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalAmountLoansAmount), tableDataCellFont));
                    ttlvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(ttlvCell);

                    Cell tsaccoCell = new Cell(new Phrase("Total Welfare Contributions", tableDataCellFont));
                    tsaccoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tsaccoCell);

                    Cell ttsaccoCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalAmountSaccoAmount), tableDataCellFont));
                    ttsaccoCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(ttsaccoCell);

                    Cell todCell = new Cell(new Phrase("Total Other Deductions", tableDataCellFont));
                    todCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(todCell);

                    Cell todvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalOtherDeductions), tableDataCellFont));
                    todvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(todvCell);

                    Cell tdCell = new Cell(new Phrase("TOTAL DEDUCTIONS", tableHeaderCellFont));
                    tdCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tdCell);

                    Cell td1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalDeductions), tableHeaderCellFont));
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell deductionsCell = new Cell(new Phrase("DEDUCTIONS\n", documentHeadersFont));
                    deductionsCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    deductionsCell.Colspan = 2;
                    deductionsCell.Border = Cell.BOX;
                    payslipTable2.AddCell(deductionsCell);

                    foreach (var op in _ViewModel2.ExcludeOtherDeductions)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(aopCell);
                    }

                    Cell ttlCell = new Cell(new Phrase("Welfare Contributions", tableDataCellFont));
                    ttlCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(ttlCell);

                    Cell ttlvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalAmountLoansAmount), tableDataCellFont));
                    ttlvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(ttlvCell);

                    Cell tsaccoCell = new Cell(new Phrase("Total Welfare Contributions", tableDataCellFont));
                    tsaccoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(tsaccoCell);

                    Cell ttsaccoCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalAmountSaccoAmount), tableDataCellFont));
                    ttsaccoCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(ttsaccoCell);

                    Cell todCell = new Cell(new Phrase("Total Other Deductions", tableDataCellFont));
                    todCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(todCell);

                    Cell todvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalOtherDeductions), tableDataCellFont));
                    todvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(todvCell);

                    Cell totalCell = new Cell(new Phrase("TOTAL DEDUCTIONS", tableHeaderCellFont));
                    totalCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalCell);

                    Cell totalCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalDeductions), tableHeaderCellFont));
                    totalCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalCell1);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
                {

                    Table payslipTable1 = new Table(2);
                    payslipTable1.WidthPercentage = 100; 
                    payslipTable1.Border = Rectangle.BOX;
                    payslipTable1.Padding = 1;

                    Cell npCell = new Cell(new Phrase("NET PAY", tableHeaderCellFont));
                    npCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(npCell);

                    Cell npvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.NetPay), tableHeaderCellFont));
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell netpayCell = new Cell(new Phrase("NET PAY", tableHeaderCellFont));
                    netpayCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(netpayCell);

                    Cell netpayCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.NetPay), tableHeaderCellFont));
                    netpayCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(netpayCell1);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
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

                    foreach (var op in _ViewModel1.OtherPayments)
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

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalOtherPayments), tableDataCellFont));
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell paymentsCell = new Cell(new Phrase("Other Payments\n", documentHeadersFont));
                    paymentsCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    paymentsCell.Colspan = 2;
                    paymentsCell.Border = Cell.BOX;
                    payslipTable2.AddCell(paymentsCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable2);

                    foreach (var op in _ViewModel2.OtherPayments)
                    {
                        Cell paymentsCell1 = new Cell(new Phrase(op.Description, tableDataCellFont));
                        paymentsCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(paymentsCell1);

                        Cell paymentsCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        paymentsCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(paymentsCell2);
                    }

                    Cell totalCell = new Cell(new Phrase("Totals", deductionsFont));
                    totalCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalCell);

                    Cell totalCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalOtherPayments), tableDataCellFont));
                    totalCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalCell1);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
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

                    foreach (var op in _ViewModel1.NonCashPayments)
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

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalNonCashPayments), tableDataCellFont));
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell aCell = new Cell(new Phrase("Non Cash Payments\n", documentHeadersFont));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    aCell.Border = Cell.BOX;
                    payslipTable2.AddCell(aCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable2);

                    foreach (var op in _ViewModel2.NonCashPayments)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tableDataCellFont));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(aopCell);
                    }

                    Cell erCell = new Cell(new Phrase("Totals", deductionsFont));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalNonCashPayments), tableDataCellFont));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(mvCell);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
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

                    foreach (var op in _ViewModel1.OtherDeductionsList)
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

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalOtherDeductions), tableDataCellFont));
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell deductionsCell = new Cell(new Phrase("Other Deductions\n", documentHeadersFont));
                    deductionsCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    deductionsCell.Colspan = 2;
                    deductionsCell.Border = Cell.BOX;
                    payslipTable2.AddCell(deductionsCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable2);

                    foreach (var op in _ViewModel2.OtherDeductionsList)
                    {
                        Cell deductionsCell1 = new Cell(new Phrase(op.Description, tableDataCellFont));
                        deductionsCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(deductionsCell1);

                        Cell deductionsCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        deductionsCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(deductionsCell2);
                    }

                    Cell totalsCell = new Cell(new Phrase("Totals", deductionsFont));
                    totalsCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalsCell);

                    Cell totalsCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalOtherDeductions), tableDataCellFont));
                    totalsCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell1);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
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

                    foreach (var op in _ViewModel1.Loans)
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

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalAmountLoansAmount), tableDataCellFont));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(mvCell);

                    decimal _balance = decimal.Parse(_ViewModel1.TotalAmountLoansBalance.ToString());
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(3);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell loansCell = new Cell(new Phrase("Employee LOANS\n", documentHeadersFont));
                    loansCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    loansCell.Border = Cell.BOX;
                    loansCell.Colspan = 3;
                    payslipTable2.AddCell(loansCell);

                    PayslipLoanDetailsHeader(payslipTable2);

                    foreach (var op in _ViewModel2.Loans)
                    {
                        Cell loansCell1 = new Cell(new Phrase(op.Description, tableDataCellFont));
                        loansCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(loansCell1);

                        Cell loansCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        loansCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(loansCell2);

                        decimal _tbalance = decimal.Parse(op.YTD.ToString());
                        if (_tbalance < 0)
                        {
                            _tbalance = _tbalance * -1;
                        }
                        Cell loansCell3 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _tbalance), tableDataCellFont));
                        loansCell3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(loansCell3);
                    }

                    Cell totalsCell1 = new Cell(new Phrase("Totals", deductionsFont));
                    totalsCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalsCell1);

                    Cell totalsCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalAmountLoansAmount), tableDataCellFont));
                    totalsCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell2);

                    decimal _balance = decimal.Parse(_ViewModel1.TotalAmountLoansBalance.ToString());
                    if (_balance < 0)
                    {
                        _balance = _balance * -1;
                    }
                    Cell totalsCell3 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _balance), tableDataCellFont));
                    totalsCell3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell3);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Border = Rectangle.BOX;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
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

                    foreach (var op in _ViewModel1.SACCOContributions)
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

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalAmountSaccoAmount), tableDataCellFont));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(mvCell);

                    Cell ltbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalAmountSaccoBalance), tableDataCellFont));
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(3);
                    payslipTable2.WidthPercentage = 100; 
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell saccoCell = new Cell(new Phrase("Welfare Contributions\n", documentHeadersFont));
                    saccoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    saccoCell.Border = Cell.BOX;
                    saccoCell.Colspan = 3;
                    payslipTable2.AddCell(saccoCell);

                    PayslipSaccoDetailsHeader(payslipTable2);

                    foreach (var op in _ViewModel2.SACCOContributions)
                    {
                        Cell saccoCell1 = new Cell(new Phrase(op.Description, tableDataCellFont));
                        saccoCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(saccoCell1);

                        Cell saccoCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tableDataCellFont));
                        saccoCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(saccoCell2);

                        Cell saccoCell3 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.YTD), tableDataCellFont));
                        saccoCell3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(saccoCell3);
                    }

                    Cell totalsCell = new Cell(new Phrase("Totals", deductionsFont));
                    totalsCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalsCell);

                    Cell totalsCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalAmountSaccoAmount), tableDataCellFont));
                    totalsCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell1);

                    Cell totalsCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalAmountSaccoBalance), tableDataCellFont));
                    totalsCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell2);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
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
                if (_ViewModel1 == null)
                {
                    Table payslipTable11 = new Table(2);
                    payslipTable11.WidthPercentage = 100;
                    payslipTable11.Padding = 1;

                    _payslipTable.InsertTable(payslipTable11);
                }
                if (_ViewModel1 != null)
                {

                    Table payslipTable1 = new Table(1);
                    payslipTable1.WidthPercentage = 100;
                    payslipTable1.Border = Table.NO_BORDER;

                    Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", signatureFont));
                    sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    sgCell.Border = Cell.BOX;
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
                if (_ViewModel2 == null)
                {
                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Padding = 1;

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(1);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Table.NO_BORDER;

                    Cell footerCell = new Cell(new Phrase("Signature.....................................................................................................", signatureFont));
                    footerCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    footerCell.Border = Cell.BOX;
                    payslipTable2.AddCell(footerCell);

                    _payslipTable.InsertTable(payslipTable2);

                }
                #endregion "payslip2"
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return;
            }
        }













    }
}