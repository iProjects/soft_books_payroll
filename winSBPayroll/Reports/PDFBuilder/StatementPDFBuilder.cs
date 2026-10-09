using System;
using System.IO;
// Payroll
using BLL;
using BLL.DataEntry;
using CommonLib;
using DAL;
// iTextSharp
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;
using BLL.KRA.Models;

namespace winSBPayroll.Reports.PDFBuilder
{
    public class StatementPDFBuilder
    {
        private StatementModel _ViewModel;
        private Document document;
        private string Message;
        private string sFilePDF;
        private string _Itemid;
        private DataEntry de;
        private SBPayrollDBEntities db;
        private Repository rep;
        private string connection;

        // Standard iTextSharp Font and Color usage (compatible with .NET 4)
        private Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        private Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        private Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);
        private Font tcFont1 = new Font(Font.HELVETICA, 6, Font.NORMAL);
        private Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);

        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        private string TAG;

        public StatementPDFBuilder(string Itemid, StatementModel statementmodel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (statementmodel == null)
                throw new ArgumentNullException("StatementModel is null");
            _ViewModel = statementmodel;

            TAG = this.GetType().Name;

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

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed StatementPDFBuilder", TAG));
        }

        public string GetstatementPDF()
        {
            BuildStatementPDF();
            return sFilePDF;
        }

        private void BuildStatementPDF()
        {
            try
            {
                // Step 1: Create Document
                // Step 1: Initialize document in A4 Landscape orientation
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                // Step 2: Get Writer Instance
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                // Step 3: Open Document
                document.Open();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document header...", TAG));
                AddDocHeader();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document body...", TAG));
                AddDocBody();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document footer...", TAG));
                AddDocFooter();

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

        private void AddDocHeader()
        {
            PdfPTable statementInfoTable = new PdfPTable(5);
            statementInfoTable.WidthPercentage = 100;

            PdfPCell employernameCell = new PdfPCell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(employernameCell);

            PdfPCell employeraddressCell = new PdfPCell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(employeraddressCell);

            PdfPCell bCell = new PdfPCell(new Phrase("STATEMENT FOR: ", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(bCell);

            PdfPCell employeenameCell = new PdfPCell(new Phrase(_ViewModel.employeename, hFont1));
            employeenameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employeenameCell.Colspan = 5;
            employeenameCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(employeenameCell);

            PdfPCell sidCell = new PdfPCell(new Phrase(_ViewModel.itemid, hFont1));
            sidCell.HorizontalAlignment = Element.ALIGN_CENTER;
            sidCell.Colspan = 5;
            sidCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(sidCell);

            PdfPCell PDCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PDCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PDCell.Colspan = 4;
            PDCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(PDCell);

            // Create logo image
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;

            PdfPCell logoCell = new PdfPCell(img0);
            logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
            logoCell.Border = Rectangle.NO_BORDER;
            logoCell.AddElement(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            statementInfoTable.AddCell(logoCell);

            document.Add(statementInfoTable);
        }

        private void AddDocBody()
        {
            if (_Itemid.ToString().Trim().Equals("NSSF", StringComparison.OrdinalIgnoreCase))
            {
                AddNSSFTableBody();
            }
            else
            {
                AddTableBody();
            }
        }

        private void AddNSSFTableBody()
        {
            PdfPTable statementInfoTable = new PdfPTable(5);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.HeaderRows = 1; // Repeat Table Header on every page

            // Add Header Row
            statementInfoTable.AddCell(CreateHeaderCell("Post Date"));
            statementInfoTable.AddCell(CreateHeaderCell("Amount\nKshs"));
            statementInfoTable.AddCell(CreateHeaderCell("Balance\nKshs"));
            statementInfoTable.AddCell(CreateHeaderCell("Employer's Contribution\nKshs"));
            statementInfoTable.AddCell(CreateHeaderCell("Totals\nKshs"));

            // Add Data Rows
            foreach (var item in _ViewModel._Statementlist)
            {
                statementInfoTable.AddCell(CreateDataCell(item.date.ToString("dd-MMM-yyyy"), Element.ALIGN_LEFT));
                statementInfoTable.AddCell(CreateDataCell(item.Amountin.ToString("#,##0"), Element.ALIGN_RIGHT));
                statementInfoTable.AddCell(CreateDataCell(item.Balance.ToString("#,##0"), Element.ALIGN_RIGHT));
                statementInfoTable.AddCell(CreateDataCell(item.EmpNSSFContrib.ToString("#,##0"), Element.ALIGN_RIGHT));
                statementInfoTable.AddCell(CreateDataCell(item.TotalContribs.ToString("#,##0"), Element.ALIGN_RIGHT));
            }

            // Add Totals Row
            PdfPCell totalsCell = new PdfPCell(new Phrase("TOTAL", tcFont1));
            totalsCell.HorizontalAlignment = Element.ALIGN_LEFT;
            totalsCell.Colspan = 4;
            statementInfoTable.AddCell(totalsCell);

            PdfPCell totalContribCell = new PdfPCell(new Phrase(_ViewModel.totalContributions.ToString("#,##0"), tcFont1));
            totalContribCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            statementInfoTable.AddCell(totalContribCell);

            document.Add(statementInfoTable);
        }

        private void AddTableBody()
        {
            PdfPTable statementInfoTable = new PdfPTable(3);
            statementInfoTable.WidthPercentage = 100;
            statementInfoTable.HeaderRows = 1; // Repeat Table Header on every page

            // Add Header Row
            statementInfoTable.AddCell(CreateHeaderCell("Post Date"));
            statementInfoTable.AddCell(CreateHeaderCell("Amount\nKshs"));
            statementInfoTable.AddCell(CreateHeaderCell("Balance\nKshs"));

            // Add Data Rows
            foreach (var item in _ViewModel._Statementlist)
            {
                statementInfoTable.AddCell(CreateDataCell(item.date.ToString("dd-MMM-yyyy"), Element.ALIGN_LEFT));
                statementInfoTable.AddCell(CreateDataCell(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", item.Amountin), Element.ALIGN_RIGHT));

                decimal _tbalance = decimal.Parse(item.Balance.ToString());
                if (_tbalance < 0)
                {
                    _tbalance = _tbalance * -1;
                }

                statementInfoTable.AddCell(CreateDataCell(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _tbalance), Element.ALIGN_RIGHT));
            }

            // Add Totals Row
            PdfPCell total = new PdfPCell(new Phrase("TOTAL", tcFont1));
            total.HorizontalAlignment = Element.ALIGN_LEFT;
            total.Colspan = 2;
            statementInfoTable.AddCell(total);

            PdfPCell totalamountin = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.totalamountin), tcFont1));
            totalamountin.HorizontalAlignment = Element.ALIGN_RIGHT;
            statementInfoTable.AddCell(totalamountin);

            document.Add(statementInfoTable);
        }

        private PdfPCell CreateHeaderCell(string title)
        {
            PdfPCell cell = new PdfPCell(new Phrase(title, tcFont1));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.BOX;
            return cell;
        }

        private PdfPCell CreateDataCell(string text, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, tcFont));
            cell.HorizontalAlignment = alignment;
            cell.Border = Rectangle.BOX;
            return cell;
        }

        private void AddDocFooter()
        {
            PdfPTable statementInfoTable = new PdfPTable(1);
            statementInfoTable.WidthPercentage = 100;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            sgCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(sgCell);

            document.Add(statementInfoTable);
        }
    }
}
