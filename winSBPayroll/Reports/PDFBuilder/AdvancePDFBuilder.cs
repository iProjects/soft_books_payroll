using System;
using System.IO;
using BLL;
using BLL.DataEntry;
//Payroll
using BLL.KRA.Models;
using CommonLib;
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDFBuilder
{
    public class AdvancePDFBuilder
    {
        AdvanceReportModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;

        //DEFINED fONTS
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);//BODY
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 9, Font.BOLD);//TABLE HEADER
        Font tcFont = new Font(Font.HELVETICA, 10, Font.NORMAL);//table cell
        Font helv8font = new Font(Font.HELVETICA, 12, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);
        Font rms8Normal = new Font(Font.TIMES_ROMAN, 9, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 6, Font.BOLD);
        Font rms8Bold = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        //constructor
        public AdvancePDFBuilder(AdvanceReportModel advancereport, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (advancereport == null)
                throw new ArgumentNullException("AdvanceReportModel is null");
            _ViewModel = advancereport;

            TAG = this.GetType().Name;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed AdvancePDFBuilder", TAG));
        }

        public string GetPDF()
        {
            BuildPDF();
            return sFilePDF;
        }

        /*Build the document **/
        private void BuildPDF()
        {
            try
            {
                //step 1 creation of the document
                // Step 1: Initialize document in A4 Landscape orientation
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                // step 2:create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open document
                document.Open();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document header...", TAG));

                //add header
                AddDocHeader();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document body...", TAG));

                //add body
                AddDocBody();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document footer...", TAG));

                //add footer
                AddDocFooter();

                //close document
                document.Close();
            }
            catch (DocumentException de)
            {
                Log.WriteToErrorLogFile(de);
            }
            catch (IOException ioe)
            {
                Log.WriteToErrorLogFile(ioe);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }

        //document header
        private void AddDocHeader()
        {
            PdfPTable advanceTable = new PdfPTable(5);
            advanceTable.WidthPercentage = 100;

            PdfPCell employernameCell = new PdfPCell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Rectangle.NO_BORDER;
            advanceTable.AddCell(employernameCell);

            PdfPCell employeraddressCell = new PdfPCell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Rectangle.NO_BORDER;
            advanceTable.AddCell(employeraddressCell);

            PdfPCell bCell = new PdfPCell(new Phrase("ADVANCE", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            advanceTable.AddCell(bCell);

            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Rectangle.NO_BORDER;
            advanceTable.AddCell(reportNameCell);

            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            advanceTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            PdfPCell logoCell = new PdfPCell();
            logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
            logoCell.Border = Rectangle.NO_BORDER;
            logoCell.AddElement(img0);
            logoCell.AddElement(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            advanceTable.AddCell(logoCell);

            document.Add(advanceTable);
        }

        //document body
        private void AddDocBody()
        {
            PdfPTable mainTable = new PdfPTable(3);
            mainTable.WidthPercentage = 100;

            // Ensures header repeats on top of each page
            mainTable.HeaderRows = 1;

            // 1. Add table headers
            AddTableHeaders(mainTable);

            // 2. Add table details  
            foreach (var d in _ViewModel.EmployeAadvanceList)
            {
                AddTableDetails(mainTable, d);
            }

            // 3. Add table totals
            AddTableTotals(mainTable);

            document.Add(mainTable);
        }

        //table headers
        private void AddTableHeaders(PdfPTable table)
        {
            string[] headers = new string[] { "No", "Name", "Amount\nKshs" };

            foreach (string header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, tHfont1));
                cell.Border = Rectangle.BOX;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Padding = 3;
                table.AddCell(cell);
            }
        }

        //table details 
        private void AddTableDetails(PdfPTable table, advance adv)
        {
            PdfPCell empno = new PdfPCell(new Phrase(adv.employeeno.ToString().Trim().ToUpper(), bfont1));
            empno.Border = Rectangle.BOX;
            empno.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(empno);

            PdfPCell empname = new PdfPCell(new Phrase(adv.employeename.ToString().ToUpper().Trim(), bfont1));
            empname.Border = Rectangle.BOX;
            empname.HorizontalAlignment = Element.ALIGN_LEFT;
            table.AddCell(empname);

            PdfPCell adamount = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", adv.advanceamount), bfont1));
            adamount.Border = Rectangle.BOX;
            adamount.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(adamount);
        }

        //table totals
        private void AddTableTotals(PdfPTable table)
        {
            PdfPCell total = new PdfPCell(new Phrase("TOTAL", rms8Normal));
            total.Border = Rectangle.BOX;
            total.HorizontalAlignment = Element.ALIGN_LEFT;
            total.Colspan = 2;
            table.AddCell(total);

            PdfPCell totalamount = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel._totalAdvance), rms8Normal));
            totalamount.Border = Rectangle.BOX;
            totalamount.HorizontalAlignment = Element.ALIGN_RIGHT;
            table.AddCell(totalamount);
        }

        //document footer
        private void AddDocFooter()
        {
            PdfPTable advanceTable = new PdfPTable(1);
            advanceTable.WidthPercentage = 100;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            sgCell.Border = Rectangle.NO_BORDER;
            advanceTable.AddCell(sgCell);

            document.Add(advanceTable);
        }
    }
}
