using System;
using System.IO;
using BLL;
using BLL.DataEntry;
// Payroll
using BLL.KRA;
using CommonLib;
using DAL;
// iTextSharp
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDFBuilder
{
    public class SchedulePDFBuilder
    {
        private SheduleReportModel _ViewModel;
        private Document document;
        private string Message;
        private string sFilePDF;
        private DataEntry de;
        private string _Itemid;
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

        public SchedulePDFBuilder(string Itemid, SheduleReportModel shedulemodel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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

            TAG = this.GetType().Name;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed SchedulePDFBuilder", TAG));
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
                // Step 1: Creation of a document-object
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                // Step 2: Create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                // Step 3: Open document
                document.Open();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document header...", TAG));
                AddDocHeader();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document body...", TAG));
                AddDocBody();

                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document footer...", TAG));
                AddDocFooter();

                // Close document
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

        // Document header
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

            PdfPCell bCell = new PdfPCell(new Phrase("SCHEDULE FOR:", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(bCell);

            PdfPCell sidCell = new PdfPCell(new Phrase(_ViewModel.itemid, hFont1));
            sidCell.HorizontalAlignment = Element.ALIGN_CENTER;
            sidCell.Colspan = 5;
            sidCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(sidCell);

            PdfPCell reportCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont1));
            reportCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportCell.Colspan = 5;
            reportCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(reportCell);

            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            statementInfoTable.AddCell(PrintedonCell);

            // Create the logo
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

        // Document body
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
            statementInfoTable.AddCell(CreateHeaderCell("No"));
            statementInfoTable.AddCell(CreateHeaderCell("Name"));
            statementInfoTable.AddCell(CreateHeaderCell("Amount\nKshs"));
            statementInfoTable.AddCell(CreateHeaderCell("Employer's Contribution\nKshs"));
            statementInfoTable.AddCell(CreateHeaderCell("Totals\nKshs"));

            // Add Data Rows
            foreach (var item in _ViewModel._Schedulelist)
            {
                statementInfoTable.AddCell(CreateDataCell(item.EmpNo, Element.ALIGN_LEFT));
                statementInfoTable.AddCell(CreateDataCell(item.EmpName, Element.ALIGN_LEFT));
                statementInfoTable.AddCell(CreateDataCell(item.Amount.ToString("#,##0"), Element.ALIGN_RIGHT));
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
            statementInfoTable.AddCell(CreateHeaderCell("No"));
            statementInfoTable.AddCell(CreateHeaderCell("Name"));
            statementInfoTable.AddCell(CreateHeaderCell("Amount\nKshs"));

            // Add Data Rows
            foreach (var item in _ViewModel._Schedulelist)
            {
                statementInfoTable.AddCell(CreateDataCell(item.EmpNo, Element.ALIGN_LEFT));
                statementInfoTable.AddCell(CreateDataCell(item.EmpName, Element.ALIGN_LEFT));
                statementInfoTable.AddCell(CreateDataCell(item.Amount.ToString("#,##0"), Element.ALIGN_RIGHT));
            }

            // Add Totals Row
            PdfPCell totalsCell = new PdfPCell(new Phrase("TOTAL", tcFont1));
            totalsCell.HorizontalAlignment = Element.ALIGN_LEFT;
            totalsCell.Colspan = 2;
            statementInfoTable.AddCell(totalsCell);

            PdfPCell totalContribCell = new PdfPCell(new Phrase(_ViewModel.TotalAmount.ToString("#,##0"), tcFont1));
            totalContribCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            statementInfoTable.AddCell(totalContribCell);

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

        // Document footer
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
