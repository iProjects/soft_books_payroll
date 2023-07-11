using System;
using System.IO;
using BLL.DataEntry;
using BLL.KRA;
using CommonLib;
using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDF
{
    public class NetSalaryPDFBuilder
    {
        public NetSalaryReportModel _ViewModel;
        Document document;
        string sFilePDF;
        public PdfWriter writer;
        public bool error = false;
        string Message;

        //DEFINED FONTS
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); //table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        //constructor
        public NetSalaryPDFBuilder(NetSalaryReportModel netsalarymodel, string filename, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (netsalarymodel == null)
                throw new ArgumentNullException("NetSalaryReportModel is null");
            _ViewModel = netsalarymodel;

            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed NetSalaryPDFBuilder", TAG));

            sFilePDF = filename;
        }


        public string GetNetSalaryPDF()
        {
            try
            {
                BuildNetSalaryPDF();
                return sFilePDF;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        /*Build the document **/
        private void BuildNetSalaryPDF()
        {
            try
            {
                // step 1: creation of a document-object
                document = new Document(PageSize.A4);

                // step 2: create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open document
                document.Open();

                //Add Header 
                AddDocHeader();

                //Add Body
                AddDocBody();

                //add Footer
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
            Table netSalaryTable = new Table(5);
            netSalaryTable.WidthPercentage = 100;
            netSalaryTable.Padding = 1;
            netSalaryTable.Spacing = 1;
            netSalaryTable.Border = Table.NO_BORDER;

            Cell employernameCell = new Cell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Cell.NO_BORDER;
            netSalaryTable.AddCell(employernameCell);

            Cell employeraddressCell = new Cell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Cell.NO_BORDER;
            netSalaryTable.AddCell(employeraddressCell);


            Cell bCell = new Cell(new Phrase("NET SALARIES", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            netSalaryTable.AddCell(bCell);

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            netSalaryTable.AddCell(reportNameCell);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            netSalaryTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            netSalaryTable.AddCell(logoCell);

            document.Add(netSalaryTable);

        }

        //document body
        private void AddDocBody()
        {

            //Add table headers
            AddTableHeaders();

            //Add table details  
            foreach (var d in _ViewModel.salarylist)
            {
                AddTableDetails(d);
            }

            //Add table totals
            AddTableTotals();

        }

        //table headers
        private void AddTableHeaders()
        {
            Table netSalaryTable = new Table(4);
            netSalaryTable.WidthPercentage = 100;
            netSalaryTable.Padding = 1;
            netSalaryTable.Spacing = 1;

            Chunk employeenumber = new Chunk("No", tHFont);
            Cell netsalaryemployeenumber = new Cell(employeenumber);
            netsalaryemployeenumber.Border = Cell.RECTANGLE;
            netsalaryemployeenumber.HorizontalAlignment = Cell.ALIGN_CENTER;
            netsalaryemployeenumber.VerticalAlignment = Cell.ALIGN_CENTER;
            netSalaryTable.AddCell(netsalaryemployeenumber, new System.Drawing.Point(0, 0));

            Chunk employeename = new Chunk("Name", tHFont);
            Cell netsalaryemployeename = new Cell(employeename);
            netsalaryemployeename.Border = Cell.RECTANGLE;
            netsalaryemployeename.HorizontalAlignment = Cell.ALIGN_CENTER;
            netsalaryemployeename.VerticalAlignment = Cell.ALIGN_CENTER;
            netSalaryTable.AddCell(netsalaryemployeename, new System.Drawing.Point(0, 1));

            Chunk reference = new Chunk("Pin", tHFont);
            Cell netsalaryreference = new Cell(reference);
            netsalaryreference.Border = Cell.RECTANGLE;
            netsalaryreference.HorizontalAlignment = Cell.ALIGN_CENTER;
            netsalaryreference.VerticalAlignment = Cell.ALIGN_CENTER;
            netSalaryTable.AddCell(netsalaryreference, new System.Drawing.Point(0, 2));

            Chunk amount = new Chunk("Net Salary\nKshs", tHFont);
            Cell netsalaryamount = new Cell(amount);
            netsalaryamount.Border = Cell.RECTANGLE;
            netsalaryamount.HorizontalAlignment = Cell.ALIGN_CENTER;
            netsalaryamount.VerticalAlignment = Cell.ALIGN_CENTER;
            netSalaryTable.AddCell(netsalaryamount, new System.Drawing.Point(0, 3));

            document.Add(netSalaryTable);

        }

        //table details 
        private void AddTableDetails(SalaryList pay)
        {
            Table netSalaryTable = new Table(4);
            netSalaryTable.WidthPercentage = 100;
            netSalaryTable.Spacing = 1;
            netSalaryTable.Padding = 1;

            Cell empno = new Cell(new Phrase(pay.employeeno, tcFont));
            empno.HorizontalAlignment = Cell.ALIGN_LEFT;
            netSalaryTable.AddCell(empno, new System.Drawing.Point(0, 0));

            Cell surname = new Cell(new Phrase(pay.employeename.Trim(), tcFont));
            surname.HorizontalAlignment = Cell.ALIGN_LEFT;
            netSalaryTable.AddCell(surname, new System.Drawing.Point(0, 1));

            Cell pinno = new Cell(new Phrase(pay.employeepin, tcFont));
            pinno.HorizontalAlignment = Cell.ALIGN_LEFT;
            netSalaryTable.AddCell(pinno, new System.Drawing.Point(0, 2));

            Cell netpay = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", pay.totalamount), tcFont));
            netSalaryTable.DefaultHorizontalAlignment = Cell.ALIGN_RIGHT;
            netSalaryTable.AddCell(netpay, new System.Drawing.Point(0, 3));

            document.Add(netSalaryTable);

        }

        //table totals
        private void AddTableTotals()
        {
            Table netSalaryTable = new Table(4);
            netSalaryTable.WidthPercentage = 100;
            netSalaryTable.Spacing = 1;
            netSalaryTable.Padding = 1;

            Cell total = new Cell(new Phrase("TOTAL", rms6Bold));
            total.HorizontalAlignment = Cell.ALIGN_LEFT;
            total.Colspan = 3;
            netSalaryTable.AddCell(total, new System.Drawing.Point(0, 0));

            Cell totalamount = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.totalamount), rms6Bold));
            totalamount.HorizontalAlignment = Cell.ALIGN_RIGHT;
            netSalaryTable.AddCell(totalamount, new System.Drawing.Point(0, 3));

            document.Add(netSalaryTable);
        }

        //document footer
        private void AddDocFooter()
        {

            Table netSalaryTable = new Table(1);
            netSalaryTable.WidthPercentage = 100;
            netSalaryTable.Border = Table.NO_BORDER;

            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            netSalaryTable.AddCell(sgCell);

            document.Add(netSalaryTable);

        }


    }
}
