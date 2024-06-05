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
    public class ShedulePDFBuilder
    {
        SheduleReportModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;
        DataEntry de;
        string _Itemid;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); //table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font tcFont1 = new Font(Font.HELVETICA, 6, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public ShedulePDFBuilder(string Itemid, SheduleReportModel shedulemodel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (shedulemodel == null)
                throw new ArgumentNullException("SheduleReportModel is null");
            _ViewModel = shedulemodel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            if (Itemid == null)
                throw new ArgumentNullException("Itemid is null");
            _Itemid = Itemid;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;
        }

        public string GetshedulePDF()
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

                //open document
                document.Open();

                //Add Header 
                AddDocHeader();

                //Add Body
                AddDocBody();

                //Add Footer
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

            Table statementInfoTable = new Table(5);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Padding = 1;
            statementInfoTable.Spacing = 1;
            statementInfoTable.Border = Table.NO_BORDER;

            Cell employernameCell = new Cell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Cell.NO_BORDER;
            statementInfoTable.AddCell(employernameCell);

            Cell employeraddressCell = new Cell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Cell.NO_BORDER;
            statementInfoTable.AddCell(employeraddressCell);

            Cell bCell = new Cell(new Phrase("SCHEDULE FOR:", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            statementInfoTable.AddCell(bCell);

            Cell sidCell = new Cell(new Phrase(_ViewModel.itemid, hFont1));
            sidCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            sidCell.Border = Cell.NO_BORDER;
            sidCell.Colspan = 5;
            statementInfoTable.AddCell(sidCell);

            Cell reportCell = new Cell(new Phrase(_ViewModel.ReportName, hFont1));
            reportCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportCell.Colspan = 5;
            reportCell.Border = Cell.NO_BORDER;
            statementInfoTable.AddCell(reportCell);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            statementInfoTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            statementInfoTable.AddCell(logoCell);

            document.Add(statementInfoTable);
        }
        //document body
        private void AddDocBody()
        {
            switch (_Itemid.ToString().Trim())
            {
                case "NSSF":
                    AddNSSFTableBody();
                    break;
                default:
                    AddTableBody();
                    break;
            }
        }
        private void AddNSSFTableBody()
        {

            AddNSSFTableHeaders();

            AddNSSFTableDetails();

            AddNSSFTableTotals();

        }
        private void AddTableBody()
        {

            AddTableHeaders();

            AddTableDetails();

            AddTableTotals();

        }
        //Nssf table headers
        private void AddNSSFTableHeaders()
        {
            Table statementInfoTable = new Table(5);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Padding = 1;
            statementInfoTable.Spacing = 1;

            Chunk empNoChunk = new Chunk("No", tcFont1);
            Cell empNoCell = new Cell(empNoChunk);
            empNoCell.Border = Cell.RECTANGLE;
            empNoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(empNoCell);

            Chunk empNameChuck = new Chunk("Name", tcFont1);
            Cell empNameCell = new Cell(empNameChuck);
            empNameCell.Border = Cell.RECTANGLE;
            empNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(empNameCell);

            Cell balance = new Cell(new Chunk("Amount\nKshs", tcFont1));
            balance.Border = Cell.RECTANGLE;
            balance.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(balance);

            Chunk empContribChunk = new Chunk("Employer's Contribution\nKshs", tcFont1);
            Cell empContribCell = new Cell(empContribChunk);
            empContribCell.Border = Cell.RECTANGLE;
            empContribCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(empContribCell);

            Chunk totalContribChunk = new Chunk("Totals\nKshs", tcFont1);
            Cell totalContribCell = new Cell(totalContribChunk);
            totalContribCell.Border = Cell.RECTANGLE;
            totalContribCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(totalContribCell);

            document.Add(statementInfoTable);
        }
        //table headers
        private void AddTableHeaders()
        {
            Table statementInfoTable = new Table(3);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Padding = 1;
            statementInfoTable.Spacing = 1;

            Chunk empNoChunk = new Chunk("No", tcFont1);
            Cell empNoCell = new Cell(empNoChunk);
            empNoCell.Border = Cell.RECTANGLE;
            empNoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(empNoCell);

            Chunk empNameChuck = new Chunk("Name", tcFont1);
            Cell empNameCell = new Cell(empNameChuck);
            empNameCell.Border = Cell.RECTANGLE;
            empNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(empNameCell);

            Cell balance = new Cell(new Chunk("Amount\nKshs", tcFont1));
            balance.Border = Cell.RECTANGLE;
            balance.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(balance);

            document.Add(statementInfoTable);
        }
        //Nssf table details
        private void AddNSSFTableDetails()
        {
            Table statementInfoTable = new Table(5);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Padding = 1;
            statementInfoTable.Spacing = 1;
            statementInfoTable.Border = Table.RECTANGLE;

            foreach (var item in _ViewModel._Schedulelist)
            {
                AddNSSFTableBodyDetails(item, statementInfoTable);
            }

            document.Add(statementInfoTable);
        }
        //table details
        private void AddTableDetails()
        {
            Table statementInfoTable = new Table(3);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Padding = 1;
            statementInfoTable.Spacing = 1;
            statementInfoTable.Border = Table.RECTANGLE;

            foreach (var item in _ViewModel._Schedulelist)
            {
                AddTableBodyDetails(item, statementInfoTable);
            }

            document.Add(statementInfoTable);
        }
        //Nssf table Body details
        private void AddNSSFTableBodyDetails(ScheduleDTO det, Table statementInfoTable)
        {
            Cell A = new Cell(new Phrase(det.EmpNo, tcFont));
            A.HorizontalAlignment = Cell.ALIGN_LEFT;
            statementInfoTable.AddCell(A);

            Cell C = new Cell(new Phrase(det.EmpName, tcFont));
            C.HorizontalAlignment = Cell.ALIGN_LEFT;
            statementInfoTable.AddCell(C);

            Cell E = new Cell(new Phrase(det.Amount.ToString("#,##0"), tcFont));
            E.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(E);

            Cell D = new Cell(new Phrase(det.EmpNSSFContrib.ToString("#,##0"), tcFont));
            D.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(D);

            Cell F = new Cell(new Phrase(det.TotalContribs.ToString("#,##0"), tcFont));
            F.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(F);
        }
        //table  Body details
        private void AddTableBodyDetails(ScheduleDTO det, Table statementInfoTable)
        {
            Cell A = new Cell(new Phrase(det.EmpNo, tcFont));
            A.HorizontalAlignment = Cell.ALIGN_LEFT;
            statementInfoTable.AddCell(A);

            Cell C = new Cell(new Phrase(det.EmpName, tcFont));
            C.HorizontalAlignment = Cell.ALIGN_LEFT;
            statementInfoTable.AddCell(C);

            Cell E = new Cell(new Phrase(det.Amount.ToString("#,##0"), tcFont));
            E.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(E);
        }
        //Nssf table totals
        private void AddNSSFTableTotals()
        {
            Table statementInfoTable = new Table(5);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Border = Table.RECTANGLE;
            statementInfoTable.Padding = 1;
            statementInfoTable.Spacing = 1;

            Cell totalsCell = new Cell(new Phrase("TOTAL", tcFont1));
            totalsCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            totalsCell.Colspan = 4;
            statementInfoTable.AddCell(totalsCell);

            Cell totalContribCell = new Cell(new Phrase(_ViewModel.totalContributions.ToString("#,##0"), tcFont1));
            totalContribCell.Border = Cell.RECTANGLE;
            totalContribCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(totalContribCell);

            document.Add(statementInfoTable);

        }
        //table totals
        private void AddTableTotals()
        {

            Table statementInfoTable = new Table(3);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Border = Table.RECTANGLE;
            statementInfoTable.Padding = 1;
            statementInfoTable.Spacing = 1;

            Cell totalsCell = new Cell(new Phrase("TOTAL", tcFont1));
            totalsCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            totalsCell.Colspan = 2;
            statementInfoTable.AddCell(totalsCell);

            Cell totalContribCell = new Cell(new Phrase(_ViewModel.TotalAmount.ToString("#,##0"), tcFont1));
            totalContribCell.Border = Cell.RECTANGLE;
            totalContribCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(totalContribCell);

            document.Add(statementInfoTable);

        }
        //document footer
        private void AddDocFooter()
        {

            Table statementInfoTable = new Table(1);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Border = Table.NO_BORDER;

            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            statementInfoTable.AddCell(sgCell);

            document.Add(statementInfoTable);

        }

    }

}
