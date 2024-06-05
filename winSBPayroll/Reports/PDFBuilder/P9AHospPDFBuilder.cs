using System;
using System.IO;
using System.Linq;
using System.Text;
//Payroll
using BLL.KRA;
using CommonLib;
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDF
{
    public class P9AHospPDFBuilder
    {
        P9AHOSPReportModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;

        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 10, Font.NORMAL);//body 
        Font tHFont = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 6, Font.NORMAL);
        Font rms8Normal = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 6, Font.BOLD);
        Font rms8Bold = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);

        string _resourcePath;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public P9AHospPDFBuilder(string ResourcePath, P9AHOSPReportModel P9AHOSPModel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (P9AHOSPModel == null)
                throw new ArgumentNullException("P9AHOSPReportModel is null");
            _ViewModel = P9AHOSPModel;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;
            _resourcePath = ResourcePath;
        }

        public string GetP9AHOSPPDF()
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
        private void BuildPDF()
        {
            try
            {
                // step 1: creation of a document-object
                document = new Document(PageSize.A4.Rotate());

                // step 2: we create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                document.Open();

                PDFGen pdfgen = new PDFGen(_notificationmessageEventname);

                Image img0 = pdfgen.DoGetImageFile(Path.Combine(_resourcePath, "kra2.jpg"));
                img0.Alignment = Image.ALIGN_CENTER;

                Table empInfoTable = new Table(3, 3);
                empInfoTable.WidthPercentage = 100;
                empInfoTable.Border = Table.NO_BORDER;

                Phrase header1 = new Phrase(_ViewModel.ReportName, hFont2);
                Cell c2 = new Cell(header1);
                c2.Border = Cell.NO_BORDER;
                c2.HorizontalAlignment = Cell.ALIGN_CENTER;
                empInfoTable.AddCell(c2, new System.Drawing.Point(1, 1));

                Cell c1 = new Cell(img0);// header1);
                c1.Border = Cell.NO_BORDER;
                c1.HorizontalAlignment = Cell.ALIGN_CENTER;
                empInfoTable.AddCell(c1, new System.Drawing.Point(0, 1));

                empInfoTable.Border = Table.NO_BORDER;
                Chunk employerName = new Chunk("Employers Name ..." + _ViewModel.EmployerName.ToUpper().Trim() + "..", bFont1);
                Cell empCell = new Cell(employerName);
                empCell.Border = Cell.NO_BORDER;
                empInfoTable.AddCell(empCell, new System.Drawing.Point(2, 0));

                Chunk employerPIN = new Chunk("Employer's PIN ..." + _ViewModel.EmployerPin.Trim() + "..", bFont1);
                Cell empPINCell = new Cell(employerPIN);
                empPINCell.Border = Cell.NO_BORDER;
                empInfoTable.AddCell(empPINCell, new System.Drawing.Point(2, 2));

                Chunk employeeName = new Chunk("Employee’s Main Name………" + _ViewModel.EmployeeMainName.Trim() + "……....", bFont1);
                Cell empNameCell = new Cell(employeeName);
                empNameCell.Border = Cell.NO_BORDER;
                empInfoTable.AddCell(empNameCell, new System.Drawing.Point(3, 0));

                Chunk employeeOName = new Chunk("Employee’s Other Names……" + _ViewModel.EmployeeOtherNames.Trim() + "……....", bFont1);
                Cell empeONameCell = new Cell(employeeOName);
                empeONameCell.Border = Cell.NO_BORDER;
                empInfoTable.AddCell(empeONameCell, new System.Drawing.Point(4, 0));

                Chunk employeePIN = new Chunk("Employee’s PIN…………………" + _ViewModel.EmployeePin.Trim() + "………....", bFont1);
                Cell empePINCell = new Cell(employeePIN);
                empePINCell.Border = Cell.NO_BORDER;
                empInfoTable.AddCell(empePINCell, new System.Drawing.Point(4, 2));

                document.Add(empInfoTable);
                document.Add(new Phrase("", new Font(Font.TIMES_ROMAN, 2, Font.NORMAL)));

                Table taxTable = new Table(15, 16);
                taxTable.Padding = 1;
                taxTable.Spacing = 1;
                taxTable.WidthPercentage = 100;

                //Add table headers
                AddTableHeaders(taxTable);

                for (int i = 1; i <= 12; i++)
                {
                    var monthRecs = (from mr in _ViewModel.P9AHospEmpList
                                     where mr.MonthInt == i
                                     select mr).SingleOrDefault();
                    AddTableRow(i, taxTable, monthRecs);
                }

                //Add totals
                AddTotals(taxTable);

                document.Add(taxTable);

                AddFooter();

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
        private void AddTableHeaders(Table aTable)
        {
            aTable.AddCell(new Phrase("Month", tHFont));  //Col 0
            aTable.AddCell(new Phrase("Basic Pay\nKshs", tHFont));//Col 1
            aTable.AddCell(new Phrase("Benefits Non Cash\nKshs", tHFont)); //Col 2
            aTable.AddCell(new Phrase("Value of Quarters\nKshs", tHFont)); //Col 3
            aTable.AddCell(new Phrase("Total Gross Pay\nKshs", tHFont)); //Col 4

            //Col 5
            Cell dCell = new Cell(new Phrase("Defined Contr\nKshs", tHFont));
            dCell.Colspan = 3;
            aTable.AddCell(dCell);
            aTable.AddCell(new Phrase("Savings Plan\nKshs", tHFont)); //Col 6
            aTable.AddCell(new Phrase("Ret Contr\nKshs", tHFont)); //Col 7
            aTable.AddCell(new Phrase("Chargeable Pay\nKshs", tHFont)); //Col 8
            aTable.AddCell(new Phrase("Tax Charged\nKshs", tHFont)); //Col 9
            aTable.AddCell(new Phrase("Monthly Relief\nKshs", tHFont)); //Col 10
            aTable.AddCell(new Phrase("Insurance Relief\nKshs", tHFont)); //Col 11
            aTable.AddCell(new Phrase("PAYE Tax\nKshs", tHFont)); //Col 12

            //Row 2
            aTable.AddCell(new Phrase("", tHFont));  //Col 0
            aTable.AddCell(new Phrase("A", tHFont));//Col 1
            aTable.AddCell(new Phrase("B", tHFont)); //Col 2
            aTable.AddCell(new Phrase("C", tHFont)); //Col 3
            aTable.AddCell(new Phrase("D", tHFont)); //Col 4
            Cell d2Cell = new Cell(new Phrase("E", tHFont)); //Col 5
            d2Cell.Colspan = 3;
            aTable.AddCell(d2Cell);
            aTable.AddCell(new Phrase("F", tHFont)); //Col 6
            aTable.AddCell(new Phrase("G", tHFont)); //Col 7
            aTable.AddCell(new Phrase("H", tHFont)); //Col 8
            aTable.AddCell(new Phrase("J", tHFont)); //Col 9
            aTable.AddCell(new Phrase("K", tHFont)); //Col 10
            aTable.AddCell(new Phrase("", tHFont)); //Col 11
            aTable.AddCell(new Phrase("L", tHFont)); //Col 12

            //Row 3
            aTable.AddCell(new Phrase("", tHFont));  //Col 0
            aTable.AddCell(new Phrase("", tHFont));//Col 1
            aTable.AddCell(new Phrase("", tHFont)); //Col 2
            aTable.AddCell(new Phrase("", tHFont)); //Col 3
            aTable.AddCell(new Phrase("", tHFont)); //Col 4

            aTable.AddCell(new Phrase("E1", tHFont)); //Col 6
            aTable.AddCell(new Phrase("E2", tHFont)); //Col 6
            aTable.AddCell(new Phrase("E3", tHFont)); //Col 6

            aTable.AddCell(new Phrase("Amount Deposited", tHFont)); //Col 6
            aTable.AddCell(new Phrase("The lowest of E + F", tHFont)); //Col 7
            aTable.AddCell(new Phrase("", tHFont)); //Col 8
            aTable.AddCell(new Phrase("", tHFont)); //Col 9
            Cell KCell = new Cell(new Phrase("Total", tHFont)); //Col 5
            KCell.Colspan = 2;
            aTable.AddCell(KCell);
            aTable.AddCell(new Phrase("", tHFont)); //Col 10 
        }
        private void AddTableRow(int Month, Table aTable, EmployersMonthlyTaxRecord monthRecs)
        {
            try
            {
                if (monthRecs != null)
                {
                    //Totals
                    aTable.AddCell(new Phrase(monthRecs.Month, tcFont));  //Col 0

                    Cell A = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.A), tcFont));
                    A.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(A);//Col 1

                    Cell B = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.B), tcFont));
                    B.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(B);//Col 2

                    Cell C = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.C), tcFont));
                    C.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(C);//Col 3

                    Cell D = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.D), tcFont));
                    D.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(D);//Col 4

                    Cell E1 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.E1), tcFont));
                    E1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(E1);//Col 5

                    Cell E2 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.E2), tcFont));
                    E2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(E2);//Col 6

                    Cell E3 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.E3), tcFont));
                    E3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(E3);//Col 7

                    Cell F = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.F), tcFont));
                    F.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(F);//Col 8

                    Cell G = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.G), tcFont));
                    G.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(G);//Col 9

                    Cell H = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.H), tcFont));
                    H.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(H);//Col 10

                    Cell J = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.J), tcFont));
                    J.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(J);//Col 11

                    Cell KCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.K), tcFont)); //Col 12
                    KCell.Colspan = 2;
                    KCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(KCell);

                    Cell L = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.L), tcFont));
                    L.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(L);//Col 13
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void AddTotals(Table aTable)
        {
            try
            {
                //Totals
                aTable.AddCell(new Phrase("Total", tcFont));  //Col 0

                Cell A = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_A), tcFont));
                A.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(A);//Col 1

                Cell B = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_B), tcFont));
                B.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(B);//Col 2

                Cell C = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_C), tcFont));
                C.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(C);//Col 3

                Cell D = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_D), tcFont));
                D.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(D);//Col 4

                Cell E1 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_E1), tcFont));
                E1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(E1);//Col 5

                Cell E2 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_E2), tcFont));
                E2.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(E2);//Col 6

                Cell E3 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_E3), tcFont));
                E3.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(E3);//Col 7

                Cell F = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_F), tcFont));
                F.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(F);//Col 8

                Cell G = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_G), tcFont));
                G.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(G);//Col 9

                Cell H = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_H), tcFont));
                H.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(H);//Col 10

                Cell J = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_J), tcFont));
                J.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(J);//Col 11

                Cell KCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_K), tcFont)); //Col 12
                KCell.Colspan = 2;
                KCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(KCell);

                Cell L = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_L), tcFont));
                L.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(L);//Col 13
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void AddFooter()
        {
            Table aTable = new Table(2);
            aTable.Border = Table.NO_BORDER;
            aTable.Padding = 1;
            aTable.Spacing = 1;
            aTable.WidthPercentage = 100;

            Cell f1 = new Cell(new Phrase("To be completed by Employer at the end of the year", rms8Normal));
            f1.HorizontalAlignment = Cell.ALIGN_LEFT;
            f1.Border = Cell.NO_BORDER;
            aTable.AddCell(f1);
            Cell f2 = new Cell("");
            f2.Border = Cell.NO_BORDER;
            aTable.AddCell(f2);

            Cell f21 = new Cell(new Phrase("TOTAL CHARGEABLE PAY (COL.H) Kshs." + string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_H.ToString("#,##0")), rms8Bold));
            f21.HorizontalAlignment = Cell.ALIGN_LEFT;
            f21.Border = Cell.NO_BORDER;
            aTable.AddCell(f21);
            Cell f22 = new Cell(new Phrase("TOTAL TAX (COL.L) Kshs." + string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total_L.ToString("#,##0")), rms8Bold));
            f22.HorizontalAlignment = Cell.ALIGN_LEFT;
            f22.Border = Cell.NO_BORDER;
            aTable.AddCell(f22);

            Paragraph p2 = new Paragraph("IMPORTANT: ", rms6Bold);
            StringBuilder sb = new StringBuilder();
            sb.Append("1. Use P9A(HOSP)\n(a) For all liable employees and where director/employee recieved");
            sb.AppendLine("benefits in addition to cash emoluments");
            sb.AppendLine("(b) Where an employee is eligible to deposit funds with a registered Home Ownership Savings Plan");
            sb.AppendLine("2 Deductible deposit in respect of any year must not exceed the statutory limit of Kshs. 48,000/=");
            sb.AppendLine("3 Attach the DECLARATION duly signed by the eligible employee to form P9A(HOSP)");
            p2.Add(new Phrase(sb.ToString(), rms8Normal));

            StringBuilder sb1 = new StringBuilder();
            sb1.AppendLine("4.See back of this card for further information required by the Department");
            sb1.AppendLine("P.9A(HOSP)");
            p2.Add(new Phrase(sb1.ToString(), rms8Bold));

            Cell f41 = new Cell(p2);
            f41.HorizontalAlignment = Cell.ALIGN_LEFT;
            f41.Border = Table.NO_BORDER;
            aTable.AddCell(f41);

            StringBuilder sb3 = new StringBuilder();
            sb3.AppendLine("NAMES OF APPROVED INSTITUTION .....................................");
            sb3.AppendLine("REGISTRATION NUMBER OF  APPROVED INSTITUTION..................................");
            sb3.AppendLine("DATE OF REGISTRATION .....................................");

            Cell f42 = new Cell(new Phrase(sb3.ToString(), rms8Bold));
            f42.HorizontalAlignment = Cell.ALIGN_LEFT;
            f42.Border = Table.NO_BORDER;
            aTable.AddCell(f42);

            document.Add(aTable);
        }



















    }
}