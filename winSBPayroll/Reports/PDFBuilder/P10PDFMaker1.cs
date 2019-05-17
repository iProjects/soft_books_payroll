using System;
using System.IO;
using System.Linq;
using System.Text;
using BLL.KRA;
using CommonLib;
using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDF
{
    public class P10PDFMaker1
    {

        public P10ReportModel _ViewModel;
        Document document;
        string sFilePDF;
        public Employee emp;
        string _resourcePath;
        string Message;

        Font pfont = new Font(Font.TIMES_ROMAN, 14, Font.BOLD);
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 10, Font.NORMAL);//body 
        Font tHFont = new Font(Font.TIMES_ROMAN, 10, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 10, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 6, Font.NORMAL);
        Font rms8Normal = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 6, Font.BOLD);
        Font rms8Bold = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);


        public P10PDFMaker1(string ResourcePath, P10ReportModel p10Model, string filename)
        {
            if (p10Model == null)
                throw new ArgumentNullException("P10ReportModel is null");
            _ViewModel = p10Model;

            sFilePDF = filename;
            _resourcePath = ResourcePath;
        }

        public string GetP10PDF()
        {
            try
            {
                BuildP10PDF();
                return sFilePDF;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void BuildP10PDF()
        {
            try
            {
                document = new Document(PageSize.A4.Rotate());

                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                document.Open();

                PDFGen pdfgen = new PDFGen();
                Image img0 = pdfgen.DoGetImageFile(_resourcePath + "kra2.jpg");
                img0.Alignment = Image.ALIGN_CENTER; 

                Table empInfoTable = new Table(3, 3);
                empInfoTable.WidthPercentage = 100;
                empInfoTable.Border = Table.NO_BORDER;

                Phrase header1 = new Phrase(_ViewModel.ReportName, hFont2);
                Cell c2 = new Cell(header1);
                c2.Border = Cell.NO_BORDER;
                c2.HorizontalAlignment = Cell.ALIGN_CENTER;
                // c1.Colspan = 3;
                empInfoTable.AddCell(c2, new System.Drawing.Point(1, 1));

                // Phrase header1 = new Phrase(p9A.ReportName, hFont2);
                Cell c1 = new Cell(img0);// header1);
                c1.Border = Cell.NO_BORDER;
                c1.HorizontalAlignment = Cell.ALIGN_CENTER;
                // c1.Colspan = 3;

                empInfoTable.AddCell(c1, new System.Drawing.Point(0, 1));

                Chunk name = new Chunk("_ViewModel", bFont1);
                Cell empPINCell1 = new Cell(name);
                empPINCell1.Border = Cell.NO_BORDER;
                empInfoTable.AddCell(empPINCell1, new System.Drawing.Point(1, 0));

                Chunk employerPIN = new Chunk("Employer's PIN ..." + _ViewModel.EmployerPin.Trim() + "...", bFont1);
                Cell empPINCell = new Cell(employerPIN);
                empPINCell.Border = Cell.NO_BORDER;
                empInfoTable.AddCell(empPINCell, new System.Drawing.Point(1, 2));

                document.Add(empInfoTable);

                Table empInfoTable2 = new Table(1, 4);
                empInfoTable2.WidthPercentage = 100;
                empInfoTable2.Border = Table.NO_BORDER;

                Phrase header2 = new Phrase("To Senior Assistant Commissioner...........\n", bFont1);

                Cell c3 = new Cell(header2);
                c3.Border = Cell.NO_BORDER;
                c3.HorizontalAlignment = Cell.ALIGN_LEFT;
                c3.VerticalAlignment = Cell.ALIGN_CENTER;
                empInfoTable2.AddCell(c3, new System.Drawing.Point(1, 0));

                Chunk info = new Chunk("We/I forward herewith...................Tax Deduction Cards(P9A/P9B)showing the Total tax deducted\n (as listed on P10A) amounting to Kshs........" + _ViewModel.TotalPAYETax.ToString("#,##0") + "\n This Total tax has been paid as follows:-    ", bFont1);
                Cell info1 = new Cell(info);
                info1.Border = Cell.NO_BORDER;
                info1.HorizontalAlignment = Cell.ALIGN_LEFT;
                empInfoTable2.AddCell(info1, new System.Drawing.Point(2, 0));

                Chunk info2 = new Chunk(".", bFont1);
                Cell info3 = new Cell(info2);
                info3.Border = Cell.NO_BORDER;
                info3.HorizontalAlignment = Cell.ALIGN_LEFT;

                empInfoTable2.AddCell(info3, new System.Drawing.Point(3, 0));

                document.Add(empInfoTable2);

                Table taxTable = new Table(5, 14);
                taxTable.Padding = 1;
                taxTable.Spacing = 1;
                taxTable.WidthPercentage = 100;

                //Add table headers
                AddTableHeaders(taxTable);

                for (int i = 1; i <= 12; i++)
                {
                    var monthRecs = (from mr in _ViewModel.P10tax
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
            aTable.AddCell(new Phrase("PAYE TAX\nKshs", tHFont));//Col 1
            aTable.AddCell(new Phrase("AUDIT TAX,INTEREST/PENALTY\nKshs", tHFont)); //Col 2
            aTable.AddCell(new Phrase("FRINGE BENEFIT TAX\nKshs", tHFont)); //Col 3
            aTable.AddCell(new Phrase("DATE PAID(PER RECEIVING BANK STAMP)", tHFont)); //Col 4
        } 
        private void AddTableRow(int Month, Table aTable, P10TaxRecord monthRecs)
        {
            try
            {
                if (monthRecs != null)
                {
                    //Totals
                    aTable.AddCell(new Phrase(monthRecs.Month, tcFont));  //Col 0

                    Cell A = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.PAYETax), tcFont));
                    A.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(A);//Col 1


                    Cell dCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.AuditTax), tcFont));
                    dCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(dCell);

                    Cell C = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", monthRecs.FringeBenefitTax), tcFont));
                    C.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    aTable.AddCell(C);

                    aTable.AddCell(new Phrase("", tcFont));
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
                aTable.AddCell(new Phrase("TOTAL TAX KSHS.", tHFont));  //Col 0

                Cell d1Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalPAYETax), tHFont));
                d1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(d1Cell);

                Cell dCell = new Cell(new Phrase("", tHFont));
                dCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(dCell);

                Cell cell = new Cell(new Phrase("", tHFont));
                cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                aTable.AddCell(cell);

                Cell c1 = new Cell("");
                c1.Border = Cell.NO_BORDER;
                aTable.AddCell(c1);

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

            Paragraph p2 = new Paragraph("NOTE: ", rms6Bold);
            StringBuilder sb = new StringBuilder();
            sb.Append("(1) Attach photostat copies of ALL the Pay-In Credit Slip(P11s) for the year");

            sb.AppendLine("\n(2) Complete this certificate in triplicate and send the two copies with the enclosures to your Income Tax Office not later than 28th FEBRUARY.");
            sb.AppendLine("(3) Provide Statistical information required by Central Bureau of Statistics. (See overleaf)");

            sb.AppendLine("We/I that the particulars entered above are correct.");
            p2.Add(new Phrase(sb.ToString(), rms8Normal));

            Cell f41 = new Cell(p2);
            f41.HorizontalAlignment = Cell.ALIGN_LEFT;
            f41.Border = Table.NO_BORDER;
            aTable.AddCell(f41);

            document.Add(aTable);

            document.Add(new Phrase("\nNAME OF EMPLOYER ............" + _ViewModel.EmployerName.ToUpper(), tcFont));
            document.Add(new Phrase("\nADDRESS ................" + _ViewModel.EmployerAddress, tcFont));
            document.Add(new Phrase("\nDATE.................." + DateTime.Today.ToString("ddd-dd-MMM-yyyy"), tcFont));
            document.Add(new Phrase("\nSIGNATURE ......................................................................", tcFont));

        }






















    }
}