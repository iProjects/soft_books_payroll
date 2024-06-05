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
    public class MakePayslipPDF2
    {
        Payslip _ViewModel1;
        Payslip _ViewModel2;
        Document document;
        string Message;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        ////DEFINED FONTS
        Font hFont1 = new Font(Font.TIMES_ROMAN, 7, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 7, Font.NORMAL);//body 
        Font bFont2 = new Font(Font.TIMES_ROMAN, 7, Font.NORMAL);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 7, Font.NORMAL); //table Header
        Font tHFont1 = new Font(Font.TIMES_ROMAN, 7, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 6, Font.NORMAL);//table cell
        Font tcFont2 = new Font(Font.HELVETICA, 6, Font.NORMAL);//table cell
        Font tcFont3 = new Font(Font.HELVETICA, 6, Font.BOLD);//table cell
        Font tcFont4 = new Font(Font.TIMES_ROMAN, 6, Font.NORMAL);//table cell
        Font rms10Normal = new Font(Font.HELVETICA, 7, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public MakePayslipPDF2(Payslip PayslipModel1, Payslip PayslipModel2, Document doc, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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

            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed MakePayslipPDF2", TAG));

        }

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

                //Add Other Deductions
                AddOtherDeductions(payslipsTable);

                //Add Employee Loans
                AddEmployeeLoans(payslipsTable);

                //Add Sacco Contributions
                AddSACCOContributions(payslipsTable);

                //Add Document Footer
                AddDocFooter(payslipsTable);

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

                    Cell aCell = new Cell(new Phrase(_ViewModel1.EmployerName.ToUpper().Trim() + "\n", hFont1));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    payslipTable11.AddCell(aCell);

                    Cell emailCell = new Cell(new Phrase(_ViewModel1.EmployerAddress.Trim() + "\n", hFont1));
                    emailCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    emailCell.Colspan = 2;
                    payslipTable11.AddCell(emailCell);

                    Cell bCell = new Cell(new Phrase("PAYSLIP", new Font(Font.TIMES_ROMAN, 10, Font.BOLD, Color.BLACK)));
                    bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    bCell.Colspan = 2;
                    //bCell.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(bCell);

                    Cell emCell1 = new Cell(new Phrase("Payroll Month:", bFont1));
                    emCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //emCell1.Border = Cell.NO_BORDER; 
                    payslipTable11.AddCell(emCell1);

                    Cell empCell2 = new Cell(new Phrase(_ViewModel1.PayrollMonth, bFont2));
                    empCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //empCell2.Border = Cell.NO_BORDER; 
                    payslipTable11.AddCell(empCell2);

                    Cell periodCell = new Cell(new Phrase("Payroll Year:", bFont1));
                    periodCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //periodCell.Border = Cell.NO_BORDER; 
                    payslipTable11.AddCell(periodCell);

                    Cell periodCell2 = new Cell(new Phrase(_ViewModel1.PayrollYear.ToString(), bFont2));
                    periodCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //periodCell2.Border = Cell.NO_BORDER; 
                    payslipTable11.AddCell(periodCell2);

                    Cell printedonCell1 = new Cell(new Phrase("Printed On:", bFont1));
                    printedonCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //printedonCell1.Border = Cell.NO_BORDER; 
                    payslipTable11.AddCell(printedonCell1);

                    Cell printedonCell2 = new Cell(new Phrase(_ViewModel1.PrintedOn.ToString("dd-MMM-yyyy"), bFont2));
                    printedonCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //printedonCell2.Border = Cell.NO_BORDER; 
                    payslipTable11.AddCell(printedonCell2);

                    Cell cCell1 = new Cell(new Phrase("Employee No:", bFont1));
                    cCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //cCell1.Border = Cell.NO_BORDER; 
                    payslipTable11.AddCell(cCell1);

                    Cell cCell2 = new Cell(new Phrase(_ViewModel1.EmpNo, bFont2));
                    cCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //cCell2.Border = Cell.NO_BORDER; 
                    payslipTable11.AddCell(cCell2);

                    Cell dCell1 = new Cell(new Phrase("Employee :", bFont1));
                    dCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //dCell1.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(dCell1);

                    Cell dCell2 = new Cell(new Phrase(_ViewModel1.EmpName, bFont2));
                    dCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //dCell2.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(dCell2);

                    Cell eCell1 = new Cell(new Phrase("Pin No:", bFont1));
                    eCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //eCell1.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(eCell1);

                    Cell eCell2 = new Cell(new Phrase(_ViewModel1.PINNo, bFont2));
                    eCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //eCell2.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(eCell2);

                    Cell nssfCell1 = new Cell(new Phrase("NSSF No:", bFont1));
                    nssfCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //nssfCell1.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(nssfCell1);

                    Cell nssfCell2 = new Cell(new Phrase(_ViewModel1.NSSFNO, bFont2));
                    nssfCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //nssfCell2.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(nssfCell2);

                    Cell nhifCell1 = new Cell(new Phrase("NHIF No:", bFont1));
                    nhifCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //nhifCell1.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(nhifCell1);

                    Cell nhifCell2 = new Cell(new Phrase(_ViewModel1.NHIFNO, bFont2));
                    nhifCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //nhifCell2.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(nhifCell2);

                    Cell departmentCell1 = new Cell(new Phrase("Department:", bFont1));
                    departmentCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //departmentCell1.Border = Cell.NO_BORDER;
                    payslipTable11.AddCell(departmentCell1);

                    Cell departmentCell2 = new Cell(new Phrase(_ViewModel1.Department, bFont2));
                    departmentCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //departmentCell2.Border = Cell.NO_BORDER;
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

                    Cell nameCell = new Cell(new Phrase(_ViewModel2.EmployerName.ToUpper().Trim() + "\n", hFont1));
                    nameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    nameCell.Colspan = 2;
                    payslipTable2.AddCell(nameCell);

                    Cell addressCell = new Cell(new Phrase(_ViewModel2.EmployerAddress.Trim() + "\n", hFont1));
                    addressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    addressCell.Colspan = 2;
                    payslipTable2.AddCell(addressCell);

                    Cell titleCell = new Cell(new Phrase("PAYSLIP", new Font(Font.TIMES_ROMAN, 10, Font.BOLD, Color.BLACK)));
                    titleCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    titleCell.Colspan = 2;
                    //titleCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(titleCell);

                    Cell monthCell = new Cell(new Phrase("Payroll Month:", bFont1));
                    monthCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //monthCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(monthCell);

                    Cell monthCell2 = new Cell(new Phrase(_ViewModel2.PayrollMonth, bFont2));
                    monthCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //monthCell2.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(monthCell2);

                    Cell periodCell1 = new Cell(new Phrase("Payroll Year:", bFont1));
                    periodCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //periodCell1.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(periodCell1);

                    Cell periodCell12 = new Cell(new Phrase(_ViewModel2.PayrollYear.ToString(), bFont2));
                    periodCell12.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //periodCell12.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(periodCell12);

                    Cell printedCell1 = new Cell(new Phrase("Printed On:", bFont1));
                    printedCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //printedCell1.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(printedCell1);

                    Cell printedCell2 = new Cell(new Phrase(_ViewModel2.PrintedOn.ToString("dd-MMM-yyyy"), bFont2));
                    printedCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //printedCell2.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(printedCell2);

                    Cell noCell = new Cell(new Phrase("Employee No:", bFont1));
                    noCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //noCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(noCell);

                    Cell noCell2 = new Cell(new Phrase(_ViewModel2.EmpNo, bFont2));
                    noCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //noCell2.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(noCell2);

                    Cell empnameCell1 = new Cell(new Phrase("Employee :", bFont1));
                    empnameCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //empnameCell1.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(empnameCell1);

                    Cell empnameCell2 = new Cell(new Phrase(_ViewModel2.EmpName, bFont2));
                    empnameCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //empnameCell2.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(empnameCell2);

                    Cell pinCell = new Cell(new Phrase("Pin No:", bFont1));
                    pinCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //pinCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(pinCell);

                    Cell pinCell2 = new Cell(new Phrase(_ViewModel2.PINNo, bFont2));
                    pinCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //pinCell2.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(pinCell2);

                    Cell nssfCell = new Cell(new Phrase("NSSF No:", bFont1));
                    nssfCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //nssfCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(nssfCell);

                    Cell nssfnoCell2 = new Cell(new Phrase(_ViewModel2.NSSFNO, bFont2));
                    nssfnoCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //nssfnoCell2.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(nssfnoCell2);

                    Cell nhifnoCell1 = new Cell(new Phrase("NHIF No:", bFont1));
                    nhifnoCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //nhifnoCell1.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(nhifnoCell1);

                    Cell nhifnoCell2 = new Cell(new Phrase(_ViewModel2.NHIFNO, bFont2));
                    nhifnoCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //nhifnoCell2.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(nhifnoCell2);

                    Cell deptCell2 = new Cell(new Phrase("Department:", bFont1));
                    deptCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //deptCell2.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(deptCell2);

                    Cell deptCell1 = new Cell(new Phrase(_ViewModel2.Department, bFont2));
                    deptCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    //deptCell1.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(deptCell1);

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

                    Cell aCell = new Cell(new Phrase("PAYMENTS\n", hFont2));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    //aCell.Border = Cell.NO_BORDER;
                    payslipTable1.AddCell(aCell);

                    foreach (var op in _ViewModel1.Payments)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);
                    }

                    Cell saCell = new Cell(new Phrase("TOTAL PAYMENTS", tcFont3));
                    saCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(saCell);

                    Cell tpCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalPayments), tcFont3));
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

                    _payslipTable.InsertTable(payslipTable2);
                }
                if (_ViewModel2 != null)
                {

                    Table payslipTable2 = new Table(2);
                    payslipTable2.WidthPercentage = 100;
                    payslipTable2.Border = Rectangle.BOX;
                    payslipTable2.Padding = 1;

                    Cell titleCell = new Cell(new Phrase("PAYMENTS\n", hFont2));
                    titleCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    titleCell.Colspan = 2;
                    //titleCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(titleCell);

                    foreach (var op in _ViewModel2.Payments)
                    {
                        Cell paymentsCell = new Cell(new Phrase(op.Description, tcFont4));
                        paymentsCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(paymentsCell);

                        Cell amountCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        amountCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(amountCell);
                    }

                    Cell totalCell = new Cell(new Phrase("TOTAL PAYMENTS", tcFont3));
                    totalCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalCell);

                    Cell totalCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalPayments), tcFont3));
                    totalCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalCell1);

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

                    Cell aCell = new Cell(new Phrase("PAYE DETAILS\n", hFont2));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    //aCell.Border = Cell.NO_BORDER;
                    payslipTable1.AddCell(aCell);

                    Cell tCell = new Cell(new Phrase("TAXABLE PAY", tcFont3));
                    tCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tCell);

                    Cell t1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.PayeDetail.TaxablePay), tcFont3));
                    t1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(t1Cell);

                    Cell tdCell = new Cell(new Phrase("TAX DUE", tcFont3));
                    tdCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tdCell);

                    Cell td1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.PayeDetail.TaxDue), tcFont3));
                    td1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(td1Cell);

                    Cell prCell = new Cell(new Phrase("Personal Relief", tcFont4));
                    prCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(prCell);

                    Cell pr1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.PayeDetail.PersonalRelief), tcFont4));
                    pr1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(pr1Cell);

                    Cell insuranceCell = new Cell(new Phrase("Life Insurance Relief", tcFont4));
                    insuranceCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(insuranceCell);

                    Cell insuranceCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.PayeDetail.InsuranceRelief), tcFont4));
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

                    Cell payeCell1 = new Cell(new Phrase("PAYE DETAILS\n", hFont2));
                    payeCell1.HorizontalAlignment = Cell.ALIGN_CENTER;
                    payeCell1.Colspan = 2;
                    //payeCell1.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(payeCell1);

                    Cell taxCell1 = new Cell(new Phrase("TAXABLE PAY", tcFont3));
                    taxCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(taxCell1);

                    Cell taxCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.PayeDetail.TaxablePay), tcFont3));
                    taxCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(taxCell2);

                    Cell taxCell3 = new Cell(new Phrase("TAX DUE", tcFont3));
                    taxCell3.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(taxCell3);

                    Cell payeedetCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.PayeDetail.TaxDue), tcFont3));
                    payeedetCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(payeedetCell);

                    Cell personalCell = new Cell(new Phrase("Personal Relief", tcFont4));
                    personalCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(personalCell);

                    Cell personalCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.PayeDetail.PersonalRelief), tcFont4));
                    personalCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(personalCell1);

                    Cell insuranceCell2 = new Cell(new Phrase("Life Insurance Relief", tcFont4));
                    insuranceCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(insuranceCell2);

                    Cell insuranceCell3 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.PayeDetail.InsuranceRelief), tcFont4));
                    insuranceCell3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(insuranceCell3);

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

                    Cell aCell = new Cell(new Phrase("DEDUCTIONS\n", hFont2));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    //aCell.Border = Cell.NO_BORDER;
                    payslipTable1.AddCell(aCell);

                    foreach (var op in _ViewModel1.Deductions)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);
                    }

                    Cell tdCell = new Cell(new Phrase("TOTAL DEDUCTIONS", tcFont3));
                    tdCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(tdCell);

                    Cell td1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalDeductions), tcFont3));
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

                    Cell deductionsCell = new Cell(new Phrase("DEDUCTIONS\n", hFont2));
                    deductionsCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    deductionsCell.Colspan = 2;
                    //deductionsCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(deductionsCell);

                    foreach (var op in _ViewModel2.Deductions)
                    {
                        Cell descriptionCell = new Cell(new Phrase(op.Description, tcFont4));
                        descriptionCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(descriptionCell);

                        Cell amountCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        amountCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(amountCell);
                    }

                    Cell totalCell = new Cell(new Phrase("TOTAL DEDUCTIONS", tcFont3));
                    totalCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalCell);

                    Cell totalCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalDeductions), tcFont3));
                    totalCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalCell1);

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

                    Cell npCell = new Cell(new Phrase("NET PAY", tcFont3));
                    npCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(npCell);

                    Cell npvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.NetPay), tcFont3));
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

                    Cell netpayCell = new Cell(new Phrase("NET PAY", tcFont3));
                    netpayCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(netpayCell);

                    Cell netpayCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.NetPay), tcFont3));
                    netpayCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(netpayCell1);

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

                    Cell desCell = new Cell(new Phrase(e, tHFont1));
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

                    Cell aCell = new Cell(new Phrase("Other Payments\n", hFont2));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    //aCell.Border = Cell.NO_BORDER;
                    payslipTable1.AddCell(aCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable1);

                    foreach (var op in _ViewModel1.OtherPayments)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);
                    }

                    Cell erCell = new Cell(new Phrase("Totals", tHFont1));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalOtherPayments), tcFont4));
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

                    Cell paymentsCell = new Cell(new Phrase("Other Payments\n", hFont2));
                    paymentsCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    paymentsCell.Colspan = 2;
                    //paymentsCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(paymentsCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable2);

                    foreach (var op in _ViewModel2.OtherPayments)
                    {
                        Cell paymentsCell1 = new Cell(new Phrase(op.Description, tcFont4));
                        paymentsCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(paymentsCell1);

                        Cell paymentsCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        paymentsCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(paymentsCell2);
                    }

                    Cell totalCell = new Cell(new Phrase("Totals", tHFont1));
                    totalCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalCell);

                    Cell totalCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalOtherPayments), tcFont4));
                    totalCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalCell1);

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

                    Cell aCell = new Cell(new Phrase("Other Deductions\n", hFont2));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 2;
                    //aCell.Border = Cell.NO_BORDER;
                    payslipTable1.AddCell(aCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable1);

                    foreach (var op in _ViewModel1.OtherDeductionsList)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);
                    }

                    Cell erCell = new Cell(new Phrase("Totals", tHFont1));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalOtherDeductions), tcFont4));
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

                    Cell deductionsCell = new Cell(new Phrase("Other Deductions\n", hFont2));
                    deductionsCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    deductionsCell.Colspan = 2;
                    //deductionsCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(deductionsCell);

                    PayslipOtherPaymentsDeductionsHeaders(payslipTable2);

                    foreach (var op in _ViewModel2.OtherDeductionsList)
                    {
                        Cell deductionsCell1 = new Cell(new Phrase(op.Description, tcFont4));
                        deductionsCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(deductionsCell1);

                        Cell deductionsCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        deductionsCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(deductionsCell2);
                    }

                    Cell totalsCell = new Cell(new Phrase("Totals", tHFont1));
                    totalsCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalsCell);

                    Cell totalsCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalOtherDeductions), tcFont4));
                    totalsCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell1);

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

                    Cell desCell = new Cell(new Phrase(e, tHFont1));
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

                    Cell desCell = new Cell(new Phrase(e, tHFont1));
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

                    Cell aCell = new Cell(new Phrase("Employee LOANS\n", hFont2));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aCell.Colspan = 3;
                    //aCell.Border = Cell.NO_BORDER;
                    payslipTable1.AddCell(aCell);

                    PayslipLoanDetailsHeader(payslipTable1);

                    foreach (var op in _ViewModel1.Loans)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);

                        Cell lbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        lbCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(lbCell);

                    }

                    Cell erCell = new Cell(new Phrase("Totals", tHFont1));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalAmountLoansAmount), tcFont4));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(mvCell);

                    Cell ltbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalAmountLoansBalance), tcFont4));
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

                    Cell loansCell = new Cell(new Phrase("Employee LOANS\n", hFont2));
                    loansCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    //loansCell.Border = Cell.NO_BORDER;
                    loansCell.Colspan = 3;
                    payslipTable2.AddCell(loansCell);

                    PayslipLoanDetailsHeader(payslipTable2);

                    foreach (var op in _ViewModel2.Loans)
                    {
                        Cell loansCell1 = new Cell(new Phrase(op.Description, tcFont4));
                        loansCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(loansCell1);

                        Cell loansCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        loansCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(loansCell2);

                        Cell loansCell3 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        loansCell3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(loansCell3);

                    }

                    Cell totalsCell1 = new Cell(new Phrase("Totals", tHFont1));
                    totalsCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalsCell1);

                    Cell totalsCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalAmountLoansAmount), tcFont4));
                    totalsCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell2);

                    Cell totalsCell3 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalAmountLoansBalance), tcFont4));
                    totalsCell3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell3);

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

                    Cell aCell = new Cell(new Phrase("SACCO Contributions\n", hFont2));
                    aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    //aCell.Border = Cell.NO_BORDER;
                    aCell.Colspan = 3;
                    payslipTable1.AddCell(aCell);

                    PayslipSaccoDetailsHeader(payslipTable1);

                    foreach (var op in _ViewModel1.SACCOContributions)
                    {
                        Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                        desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable1.AddCell(desopCell);

                        Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(aopCell);

                        Cell scCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.YTD), tcFont4));
                        scCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable1.AddCell(scCell);
                    }

                    Cell erCell = new Cell(new Phrase("Totals", tHFont1));
                    erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable1.AddCell(erCell);

                    Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalAmountSaccoAmount), tcFont4));
                    mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable1.AddCell(mvCell);

                    Cell ltbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel1.TotalAmountSaccoBalance), tcFont4));
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

                    Cell saccoCell = new Cell(new Phrase("SACCO Contributions\n", hFont2));
                    saccoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    //saccoCell.Border = Cell.NO_BORDER;
                    saccoCell.Colspan = 3;
                    payslipTable2.AddCell(saccoCell);

                    PayslipSaccoDetailsHeader(payslipTable2);

                    foreach (var op in _ViewModel2.SACCOContributions)
                    {
                        Cell saccoCell1 = new Cell(new Phrase(op.Description, tcFont4));
                        saccoCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                        payslipTable2.AddCell(saccoCell1);

                        Cell saccoCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                        saccoCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(saccoCell2);

                        Cell saccoCell3 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.YTD), tcFont4));
                        saccoCell3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                        payslipTable2.AddCell(saccoCell3);
                    }

                    Cell totalsCell = new Cell(new Phrase("Totals", tHFont1));
                    totalsCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable2.AddCell(totalsCell);

                    Cell totalsCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalAmountSaccoAmount), tcFont4));
                    totalsCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell1);

                    Cell totalsCell2 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel2.TotalAmountSaccoBalance), tcFont4));
                    totalsCell2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable2.AddCell(totalsCell2);

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

                    Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
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

                    Cell footerCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
                    footerCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    footerCell.Border = Cell.NO_BORDER;
                    payslipTable2.AddCell(footerCell);

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