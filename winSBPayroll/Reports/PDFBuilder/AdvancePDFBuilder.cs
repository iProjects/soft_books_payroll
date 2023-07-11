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

            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed AdvancePDFBuilder", TAG));

            sFilePDF = FileName;
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
                document = new Document(PageSize.A4);

                // step 2:create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open document
                document.Open();

                //document header
                AddDocHeader();

                //document body
                AddDocBody();

                //document footer
                AddDocFooter();

                //close document
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


        //document header
        private void AddDocHeader()
        {

            Table advanceTable = new Table(5);
            advanceTable.WidthPercentage = 100;
            advanceTable.Padding = 1;
            advanceTable.Spacing = 1;
            advanceTable.Border = Table.NO_BORDER;

            Cell employernameCell = new Cell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Cell.NO_BORDER;
            advanceTable.AddCell(employernameCell);

            Cell employeraddressCell = new Cell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Cell.NO_BORDER;
            advanceTable.AddCell(employeraddressCell);

            Cell bCell = new Cell(new Phrase("ADVANCE", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            advanceTable.AddCell(bCell);

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            advanceTable.AddCell(reportNameCell);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            advanceTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            advanceTable.AddCell(logoCell);

            document.Add(advanceTable);

        }

        //document body
        private void AddDocBody()
        {

            //Add table headers
            AddTableHeaders();

            //Add table details  
            foreach (var d in _ViewModel.EmployeAadvanceList)
            {
                AddTableDetails(d);
            }

            //Add table totals
            AddTableTotals();

        }

        //table headers
        private void AddTableHeaders()
        {
            Table advancetable = new Table(3);
            advancetable.WidthPercentage = 100;
            advancetable.Spacing = 1;
            advancetable.Padding = 1;

            Cell employeenumber = new Cell(new Phrase("No", tHfont1));
            employeenumber.Border = Cell.RECTANGLE;
            employeenumber.HorizontalAlignment = Cell.ALIGN_CENTER;
            advancetable.AddCell(employeenumber);

            Cell employeename = new Cell(new Phrase("Name", tHfont1));
            employeename.Border = Cell.RECTANGLE;
            employeename.HorizontalAlignment = Cell.ALIGN_CENTER;
            advancetable.AddCell(employeename);

            Cell amount = new Cell(new Phrase("Amount\nKshs", tHfont1));
            amount.Border = Cell.RECTANGLE;
            amount.HorizontalAlignment = Cell.ALIGN_CENTER;
            advancetable.AddCell(amount);

            document.Add(advancetable);

        }

        //table details 
        private void AddTableDetails(advance adv)
        {
            Table advancetable = new Table(3);
            advancetable.WidthPercentage = 100;
            advancetable.Spacing = 1;
            advancetable.Padding = 1;

            Cell empno = new Cell(new Phrase(adv.employeeno.ToString().Trim().ToUpper(), bfont1));
            empno.HorizontalAlignment = Cell.ALIGN_LEFT;
            advancetable.AddCell(empno);

            Cell empname = new Cell(new Phrase(adv.employeename.ToString().ToUpper().Trim(), bfont1));
            empname.HorizontalAlignment = Cell.ALIGN_LEFT;
            advancetable.AddCell(empname);

            Cell adamount = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", adv.advanceamount), bfont1));
            adamount.HorizontalAlignment = Cell.ALIGN_RIGHT;
            advancetable.AddCell(adamount);

            document.Add(advancetable);
        }

        //table totals
        private void AddTableTotals()
        {
            Table advancetable = new Table(3);
            advancetable.WidthPercentage = 100;
            advancetable.Spacing = 1;
            advancetable.Padding = 1;

            Cell total = new Cell(new Phrase("TOTAL", rms8Normal));
            total.HorizontalAlignment = Cell.ALIGN_LEFT;
            total.Colspan = 2;
            advancetable.AddCell(total);

            Cell totalamount = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel._totalAdvance), rms8Normal));
            totalamount.HorizontalAlignment = Cell.ALIGN_RIGHT;
            advancetable.AddCell(totalamount);

            document.Add(advancetable);
        }

        //document footer
        private void AddDocFooter()
        {

            Table advanceTable = new Table(1);
            advanceTable.WidthPercentage = 100;
            advanceTable.Border = Table.NO_BORDER;

            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            advanceTable.AddCell(sgCell);

            document.Add(advanceTable);

        }


    }
}