using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using BLL.DataEntry;
using BLL.KRA;
using CommonLib;
using DAL;
// iTextSharp namespaces for PDF manipulation
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;
using winSBPayroll.ViewModel;

namespace winSBPayroll.Reports.PDF
{
    /// <summary>
    /// Generates the NSSF PDF report using iTextSharp's PdfPTable and PdfPCell classes.
    /// Compatible with .NET Framework 4.0.
    /// </summary>
    public class NSSFPDFBuilder
    {
        public NSSFReportModel _ViewModel;
        private Document document;
        private string Message;
        private string sFilePDF;
        private SBPayrollDBEntities db;
        private Repository rep;
        private string connection;
        private Employer _employer;

        // DEFINED FONTS - Using BaseColor for .NET 4.0 / iTextSharp 5 compatibility
        private Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        private Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        private Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        private Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        private Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        private Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        private Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD);
        private Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD);
        private Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);
        private Font helv8Font = new Font(Font.HELVETICA, 8, Font.NORMAL);
        private Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        private Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        private Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        private Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        private Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        private Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);

        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        private string TAG;

        // Constructors
        public NSSFPDFBuilder(Employer employer, NSSFReportModel nssfreportmodel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (nssfreportmodel == null)
                throw new ArgumentNullException("NSSFReportModel is null");
            _ViewModel = nssfreportmodel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
            sFilePDF = FileName;
            _employer = employer;

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed NSSFPDFBuilder", TAG));
            }
        }

        public NSSFPDFBuilder(NSSFReportModel nssfreportmodel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (nssfreportmodel == null)
                throw new ArgumentNullException("NSSFReportModel is null");
            _ViewModel = nssfreportmodel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
            sFilePDF = FileName;
            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
        }

        public string GetPDF()
        {
            BuildPDF();
            return sFilePDF;
        }

        /// <summary>
        /// Main orchestration method to build the PDF document.
        /// </summary>
        private void BuildPDF()
        {
            try
            {
                // Step 1: Initialize document (A4 Landscape orientation)
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                // Step 2: Bind PdfWriter to file stream
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                // Step 3: Open the document
                document.Open();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document header...", TAG));

                // Add header content
                AddDocHeader();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document body...", TAG));

                // Add main table content
                AddDocBody();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document footer...", TAG));

                // Add footer content
                AddDocFooter();

                // Close document stream
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

        /// <summary>
        /// Generates document header section using PdfPTable and PdfPCell[span_2](start_span)[span_2](end_span).
        /// </summary>
        private void AddDocHeader()
        {
            // Create a 5-column table for header elements[span_3](start_span)[span_3](end_span)
            PdfPTable nssfTable = new PdfPTable(5);
            nssfTable.WidthPercentage = 100;

            // Employer Name Header
            PdfPCell EmployerNameCell = new PdfPCell(new Phrase(_ViewModel.EmployerName.ToUpper() + "\n", new Font(Font.TIMES_ROMAN, 15, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            EmployerNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            EmployerNameCell.Colspan = 5;
            EmployerNameCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(EmployerNameCell);

            // Employer Address Header
            PdfPCell employerAdressCell = new PdfPCell(new Phrase(_ViewModel.EmpAddress, new Font(Font.TIMES_ROMAN, 9, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employerAdressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employerAdressCell.Colspan = 5;
            employerAdressCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(employerAdressCell);

            // Report Title Header
            PdfPCell bCell = new PdfPCell(new Phrase("NSSF", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(bCell);

            // Report Subtitle
            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(reportNameCell);

            // NSSF Code
            PdfPCell employerNSSFCell = new PdfPCell(new Phrase("NSSF No: " + _ViewModel.EmployerCode.Trim(), hFont2));
            employerNSSFCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employerNSSFCell.Colspan = 5;
            employerNSSFCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(employerNSSFCell);

            // Printed Date Metadata
            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(PrintedonCell);

            // Logo & Slogan Column
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);

            PdfPCell logoCell = new PdfPCell();
            logoCell.Border = Rectangle.NO_BORDER;
            logoCell.HorizontalAlignment = Element.ALIGN_LEFT;

            if (img0 != null)
            {
                img0.Alignment = Image.ALIGN_MIDDLE;
                logoCell.AddElement(img0);
            }

            logoCell.AddElement(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            nssfTable.AddCell(logoCell);

            // Append header to document
            document.Add(nssfTable);
        }

        /// <summary>
        /// Builds main grid layout containing employee NSSF details[span_4](start_span)[span_4](end_span).
        /// </summary>
        private void AddDocBody()
        {
            // Initialize 6-column PdfPTable for dataset view[span_5](start_span)[span_5](end_span)
            PdfPTable nssfInfoTable = new PdfPTable(6);
            nssfInfoTable.WidthPercentage = 100;
            nssfInfoTable.SpacingBefore = 10f;

            // Define relative width ratios for columns
            nssfInfoTable.SetWidths(new float[] { 10f, 20f, 20f, 15f, 15f, 20f });

            // Add Header Row
            AddBodyTableHeaders(nssfInfoTable);

            // Add Detail Rows
            foreach (var n in _ViewModel.PayList)
            {
                AddBodyTableDetails(n, nssfInfoTable);
            }

            // Add Footer Row
            AddBodyTableTotal(nssfInfoTable);

            // Append main body to document
            document.Add(nssfInfoTable);
        }

        /// <summary>
        /// Adds structural headers to the dataset grid[span_6](start_span)[span_6](end_span).
        /// </summary>
        private void AddBodyTableHeaders(PdfPTable nssfTable)
        {
            string[] headers = new string[]
            {
                "No",
                "Surname",
                "Other Names",
                "Id No",
                "NSSF No",
                "Amount\n Kshs"
            };

            foreach (string header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, tHfont1));
                cell.Border = Rectangle.BOX;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Padding = 4f;
                nssfTable.AddCell(cell);
            }
        }

        /// <summary>
        /// Generates individual employee detail rows[span_7](start_span)[span_7](end_span).
        /// </summary>
        private void AddBodyTableDetails(DAL.psuedovwPayrollMaster nssfRec, PdfPTable nssfTable)
        {
            // Col 1: Employee Number
            PdfPCell cellA = new PdfPCell(new Phrase(nssfRec.EmpNo, helv8Font));
            cellA.HorizontalAlignment = Element.ALIGN_LEFT;
            cellA.Padding = 3f;
            nssfTable.AddCell(cellA);

            // Col 2: Surname
            PdfPCell cellB = new PdfPCell(new Phrase(nssfRec.Surname, helv8Font));
            cellB.HorizontalAlignment = Element.ALIGN_LEFT;
            cellB.Padding = 3f;
            nssfTable.AddCell(cellB);

            // Col 3: Other Names
            PdfPCell cellC = new PdfPCell(new Phrase(nssfRec.OtherNames, helv8Font));
            cellC.HorizontalAlignment = Element.ALIGN_LEFT;
            cellC.Padding = 3f;
            nssfTable.AddCell(cellC);

            // Col 4: National ID
            PdfPCell cellD = new PdfPCell(new Phrase(nssfRec.IDNo, helv8Font));
            cellD.HorizontalAlignment = Element.ALIGN_LEFT;
            cellD.Padding = 3f;
            nssfTable.AddCell(cellD);

            // Col 5: NSSF Number
            PdfPCell cellE1 = new PdfPCell(new Phrase(nssfRec.NSSFNo, helv8Font));
            cellE1.HorizontalAlignment = Element.ALIGN_LEFT;
            cellE1.Padding = 3f;
            nssfTable.AddCell(cellE1);

            // Col 6: Formatted Amount
            PdfPCell cellE2 = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", nssfRec.NSSF), helv8Font));
            cellE2.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellE2.Padding = 3f;
            nssfTable.AddCell(cellE2);
        }

        /// <summary>
        /// Renders aggregate totals at the bottom of the dataset grid[span_8](start_span)[span_8](end_span).
        /// </summary>
        private void AddBodyTableTotal(PdfPTable nssfTable)
        {
            // Span overall description across first 5 columns
            PdfPCell totalLabelCell = new PdfPCell(new Phrase("TOTAL(Inclusive Employer's NSSF)", rms8Bold));
            totalLabelCell.Colspan = 5;
            totalLabelCell.HorizontalAlignment = Element.ALIGN_LEFT;
            totalLabelCell.Padding = 4f;
            nssfTable.AddCell(totalLabelCell);

            // Render sum total in the final column
            PdfPCell totalValueCell = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.Total), rms8Bold));
            totalValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalValueCell.Padding = 4f;
            nssfTable.AddCell(totalValueCell);
        }

        /// <summary>
        /// Appends standard document footer/signatures[span_9](start_span)[span_9](end_span).
        /// </summary>
        private void AddDocFooter()
        {
            PdfPTable footerTable = new PdfPTable(1);
            footerTable.WidthPercentage = 100;
            footerTable.SpacingBefore = 20f;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            sgCell.Border = Rectangle.NO_BORDER;
            footerTable.AddCell(sgCell);

            document.Add(footerTable);
        }
    }
}
