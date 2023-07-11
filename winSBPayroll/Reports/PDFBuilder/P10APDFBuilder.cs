using System;
using System.IO;
using BLL;
using BLL.DataEntry;
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
    public class P10APDFBuilder
    {
        P10AReportModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;
        string _resourcePath;

        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 10, Font.NORMAL);//body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font tHFont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); //table Header
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public P10APDFBuilder(string ResourcePath, P10AReportModel p10AModel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (p10AModel == null)
                throw new ArgumentNullException("P10AReportModel is null");
            _ViewModel = p10AModel;

            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed P10APDFBuilder", TAG));

            sFilePDF = FileName;
            _resourcePath = ResourcePath;
        }

        public string GetP10APDF()
        {
            BuildPDF();
            return sFilePDF;
        }
        private void BuildPDF()
        {
            try
            {
                // step 1: creation of a document-object
                document = new Document(PageSize.A4);

                // step 2: we create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                document.Open();

                PDFGen pdfgen = new PDFGen(_notificationmessageEventname);

                Image img0 = pdfgen.DoGetImageFile(Path.Combine(_resourcePath, "kra2.jpg"));
                img0.Alignment = Image.ALIGN_LEFT;

                Table empInfoTable = new Table(1);
                empInfoTable.WidthPercentage = 100;
                empInfoTable.Border = Table.NO_BORDER;

                Phrase header1 = new Phrase(_ViewModel.ReportName, hFont2);
                Cell c2 = new Cell(header1);
                c2.Border = Cell.NO_BORDER;
                c2.HorizontalAlignment = Cell.ALIGN_CENTER;
                // c1.Colspan = 3;
                empInfoTable.AddCell(c2, new System.Drawing.Point(1, 0));

                Cell c1 = new Cell(img0);// header1);
                c1.Border = Cell.NO_BORDER;
                c1.HorizontalAlignment = Cell.ALIGN_CENTER;
                // c1.Colspan = 3;

                empInfoTable.AddCell(c1, new System.Drawing.Point(0, 0));

                Chunk name = new Chunk("P.A.Y.E SUPPORTING LIST FOR END OF YEAR CERTIFICATE: YEAR.." + _ViewModel.Year.ToString() + "..", bFont2);
                Cell emp2Cell = new Cell(name);
                emp2Cell.Border = Cell.NO_BORDER;
                empInfoTable.AddCell(emp2Cell, new System.Drawing.Point(2, 0));

                Chunk formname = new Chunk("P10A", bFont1);
                Cell emp1Cell = new Cell(formname);
                emp1Cell.Border = Cell.NO_BORDER;
                emp1Cell.HorizontalAlignment = Cell.ALIGN_LEFT;
                empInfoTable.AddCell(emp1Cell, new System.Drawing.Point(3, 0));

                Chunk employerName = new Chunk("Employer's Name ..." + _ViewModel.EmployerName.ToUpper().Trim() + "..........                                                   Employer's PIN ..." + _ViewModel.EmployerPin.Trim() + "..", bFont1);
                Cell empCell = new Cell(employerName);
                empCell.Border = Cell.NO_BORDER;
                empInfoTable.AddCell(empCell, new System.Drawing.Point(4, 0));

                document.Add(empInfoTable);
                document.Add(new Phrase("", new Font(Font.TIMES_ROMAN, 2, Font.NORMAL)));

                Table taxTable = new Table(4);
                taxTable.Padding = 1;
                taxTable.Spacing = 1;
                taxTable.WidthPercentage = 100;

                //Add table headers
                AddTableHeaders(taxTable);

                //Add table details
                foreach (var tr in _ViewModel.P10AList)
                {
                    AddTableRow(taxTable, tr);
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
            aTable.AddCell(new Phrase("EMPLOYEE'S PIN", tHFont));  //Col 0
            aTable.AddCell(new Phrase("EMPLOYEE'S NAME", tHFont));//Col 1
            aTable.AddCell(new Phrase("TOTAL EMOLUMENTS\nKshs", tHFont)); //Col 2
            aTable.AddCell(new Phrase("PAYE DEDUCTED\nKshs", tHFont)); //Col 3
        }
        private void AddTableRow(Table aTable, TaxRecord tr)
        {
            if (tr != null)
            {
                //Totals 
                Cell B = new Cell(new Phrase(tr.EmployeePin, tcFont));
                B.HorizontalAlignment = Cell.ALIGN_LEFT;
                aTable.AddCell(B);//Col 2

                Cell C = new Cell(new Phrase(tr.EmployeeName, tcFont));
                C.HorizontalAlignment = Cell.ALIGN_LEFT;
                aTable.AddCell(C);//Col 3

                Cell D = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", tr.Emoluments), tcFont));
                D.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(D);//Col 2

                Cell E = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", tr.TaxDeducted), tcFont));
                E.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(E);//Col 2 
            }
        }
        private void AddTotals(Table aTable)
        {
            //Total Emoluments
            aTable.AddCell(new Phrase("", tcFont));  //Col 0
            aTable.AddCell(new Phrase("TOTAL EMOLUMENTS", tHFont1));  //Col 2

            Cell A = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalEmoluments), tHFont1));
            A.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(A);//Col 3
            aTable.AddCell(new Phrase("", tcFont));  //Col 4

            //Row2
            Cell r2c1 = new Cell(new Phrase("", tcFont));
            r2c1.Rowspan = 2;
            aTable.AddCell(r2c1);  //Col 0
            aTable.AddCell(new Phrase("TOTAL TAX", tHFont1));  //Col 2
            Cell r2c3 = new Cell(new Phrase("", tcFont));
            r2c3.Rowspan = 2;
            aTable.AddCell(r2c3);  //Col 3
            Cell B = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalTaxDeducted), tHFont1));
            B.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(B);//Col 4

            //Row3
            //aTable.AddCell(new Phrase("", tcFont));  //Col 0
            aTable.AddCell(new Phrase("TOTAL WCPS", tHFont1));  //Col 2
            //aTable.AddCell(new Phrase("", tcFont));  //Col 3
            Cell C = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.WCPS), tHFont1));
            C.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(C);//Col 4

            //Row 4
            aTable.AddCell(new Phrase("", tcFont));  //Col 1
            Cell D = new Cell(new Phrase("*TAX ON LUMPSUM/AUDIT/INTEREST/PENALTY", tHFont1));
            D.Colspan = 2;

            aTable.AddCell(D);//Col 2
            aTable.AddCell(new Phrase("", tcFont));  //Col 3

            //Row 5
            aTable.AddCell(new Phrase("", tcFont));  //Col 1
            Cell E = new Cell(new Phrase("*TAX DEDUCTED / TOTAL C/F TO NEXT LIST", tHFont1));
            E.Colspan = 2;
            aTable.AddCell(E);//Col 2
            aTable.AddCell(new Phrase("", tcFont));  //Col 3
        }
        private void AddFooter()
        {
            document.Add(new Phrase("\nNB: KRA SHALL NOT ACCEPT ANY LIABILITY FOR LOSS OR DAMAGE INCURRED AS A RESULT OF ALTERATION OF THIS FORM", tHFont1));
        }















    }
}