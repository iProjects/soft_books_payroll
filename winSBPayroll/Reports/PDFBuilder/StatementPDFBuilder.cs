using System;
using System.IO;
//Payroll
using BLL;
using BLL.DataEntry;
using CommonLib;
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;
using BLL.KRA.Models;

namespace winSBPayroll.Reports.PDFBuilder
{
    public class StatementPDFBuilder
    {
        StatementModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;
        string _Itemid;
        DataEntry de;
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

        public StatementPDFBuilder(string Itemid, StatementModel statementmodel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (statementmodel == null)
                throw new ArgumentNullException("StatementModel is null");
            _ViewModel = statementmodel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _notificationmessageEventname = notificationmessageEventname;

            if (Itemid == null)
                throw new ArgumentNullException("Itemid is null");
            _Itemid = Itemid;

            sFilePDF = FileName;

        }

        public string GetstatementPDF()
        {
            BuildStatementPDF();
            return sFilePDF;
        }


        /*Build the document **/
        private void BuildStatementPDF()
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

            Cell bCell = new Cell(new Phrase("STATEMENT FOR: ", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            statementInfoTable.AddCell(bCell);

            Cell employeenameCell = new Cell(new Phrase(_ViewModel.employeename, hFont1));
            employeenameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeenameCell.Border = Cell.NO_BORDER;
            employeenameCell.Colspan = 5;
            statementInfoTable.AddCell(employeenameCell);

            Cell sidCell = new Cell(new Phrase(_ViewModel.itemid, hFont1));
            sidCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            sidCell.Border = Cell.NO_BORDER;
            sidCell.Colspan = 5;
            statementInfoTable.AddCell(sidCell);

            Cell PDCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PDCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PDCell.Colspan = 4;
            PDCell.Border = Cell.NO_BORDER;
            statementInfoTable.AddCell(PDCell);

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
        //table headers
        private void AddNSSFTableHeaders()
        {
            Table statementInfoTable = new Table(5);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Padding = 1;
            statementInfoTable.Spacing = 1;

            Chunk date = new Chunk("Post Date", tcFont1);
            Cell statementdate = new Cell(date);
            statementdate.Border = Cell.RECTANGLE;
            statementdate.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(statementdate);

            Chunk amountin = new Chunk("Amount\nKshs", tcFont1);
            Cell statementamountin = new Cell(amountin);
            statementamountin.Border = Cell.RECTANGLE;
            statementamountin.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(statementamountin);

            Cell balance = new Cell(new Chunk("Balance\nKshs", tcFont1));
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

            Chunk date = new Chunk("Post Date", tcFont1);
            Cell statementdate = new Cell(date);
            statementdate.Border = Cell.RECTANGLE;
            statementdate.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(statementdate);

            Chunk amountin = new Chunk("Amount\nKshs", tcFont1);
            Cell statementamountin = new Cell(amountin);
            statementamountin.Border = Cell.RECTANGLE;
            statementamountin.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(statementamountin);

            Cell balance = new Cell(new Chunk("Balance\nKshs", tcFont1));
            balance.Border = Cell.RECTANGLE;
            balance.HorizontalAlignment = Cell.ALIGN_CENTER;
            statementInfoTable.AddCell(balance);

            document.Add(statementInfoTable);
        }
        //table details
        private void AddNSSFTableDetails()
        {
            Table statementInfoTable = new Table(5);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.Padding = 1;
            statementInfoTable.Spacing = 1;
            statementInfoTable.Border = Table.RECTANGLE;

            foreach (var item in _ViewModel._Statementlist)
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

            foreach (var item in _ViewModel._Statementlist)
            {
                AddTableBodyDetails(item, statementInfoTable);
            }

            document.Add(statementInfoTable);
        }
        //table details
        private void AddNSSFTableBodyDetails(StatementDTO det, Table statementInfoTable)
        {
            Cell A = new Cell(new Phrase(det.date.ToString("dd-MMM-yyyy"), tcFont));
            A.HorizontalAlignment = Cell.ALIGN_LEFT;
            statementInfoTable.AddCell(A);

            Cell C = new Cell(new Phrase(det.Amountin.ToString("#,##0"), tcFont));
            C.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(C);

            Cell E = new Cell(new Phrase(det.Balance.ToString("#,##0"), tcFont));
            E.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(E);

            Cell D = new Cell(new Phrase(det.EmpNSSFContrib.ToString("#,##0"), tcFont));
            D.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(D);

            Cell F = new Cell(new Phrase(det.TotalContribs.ToString("#,##0"), tcFont));
            F.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(F);
        }
        //table  details
        private void AddTableBodyDetails(StatementDTO det, Table statementInfoTable)
        {
            Cell A = new Cell(new Phrase(det.date.ToString("dd-MMM-yyyy"), tcFont));
            A.HorizontalAlignment = Cell.ALIGN_LEFT;
            statementInfoTable.AddCell(A);

            Cell C = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", det.Amountin), tcFont));
            C.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(C);

            decimal _tbalance = decimal.Parse(det.Balance.ToString());
            if (_tbalance < 0)
            {
                _tbalance = _tbalance * -1;
            }
            Cell E = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _tbalance), tcFont));
            E.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(E);
        }
        //table totals
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

            Cell total = new Cell(new Phrase("TOTAL", tcFont1));
            total.HorizontalAlignment = Cell.ALIGN_LEFT;
            statementInfoTable.AddCell(total);

            Cell totalamountin = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.totalamountin), tcFont1));
            totalamountin.Colspan = 1;
            totalamountin.HorizontalAlignment = Cell.ALIGN_RIGHT;
            statementInfoTable.AddCell(totalamountin);

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
