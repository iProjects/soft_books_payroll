using System;
using System.Globalization;
using System.IO;
using BLL;
using BLL.DataEntry;
using BLL.KRA;
using CommonLib;
using DAL;
// iTextSharp namespaces for PDF manipulation
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDF
{
    /// <summary>
    /// Generates the SHIF PDF report using PdfPTable and PdfPCell.
    /// Configured with repeating table headers on each page and targeting .NET Framework 4.0.
    /// </summary>
    public class SHIFPDFBuilder
    {
        public SHIFReportModel _ViewModel;
        private Document document;
        private string Message;
        private string sFilePDF;

        // DEFINED FONTS - Using standard Font constructors (without BaseColor or FontFamily)
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

        // Constructor
        public SHIFPDFBuilder(SHIFReportModel shifreportmodel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (shifreportmodel == null)
                throw new ArgumentNullException("SHIFReportModel is null");
            _ViewModel = shifreportmodel;

            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
            sFilePDF = FileName;

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed SHIFPDFBuilder", TAG));
            }
        }

        public string GetPDF()
        {
            try
            {
                BuildSHIFPDF();
                return sFilePDF;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        /// <summary>
        /// Main orchestration method to build the SHIF PDF document.
        /// </summary>
        private void BuildSHIFPDF()
        {
            try
            {
                // Step 1: Initialize document in A4 Landscape orientation
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                // Step 2: Bind PdfWriter to file stream
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                // Step 3: Open document
                document.Open();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document header...", TAG));

                // Add document header
                AddDocHeader();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document body...", TAG));

                // Add main data body table
                AddDocBody();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document footer...", TAG));

                // Add document footer
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
            PdfPTable shifTable = new PdfPTable(5);
            shifTable.WidthPercentage = 100;

            // Employer Name Header
            PdfPCell EmployerNameCell = new PdfPCell(new Phrase(_ViewModel.EmployerName.ToUpper() + "\n", new Font(Font.TIMES_ROMAN, 15, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            EmployerNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            EmployerNameCell.Colspan = 5;
            EmployerNameCell.Border = Rectangle.NO_BORDER;
            shifTable.AddCell(EmployerNameCell);

            // Employer Address Header
            PdfPCell employerAdressCell = new PdfPCell(new Phrase(_ViewModel.EmpAddress, new Font(Font.TIMES_ROMAN, 9, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employerAdressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employerAdressCell.Colspan = 5;
            employerAdressCell.Border = Rectangle.NO_BORDER;
            shifTable.AddCell(employerAdressCell);

            // Report Title Header
            PdfPCell bCell = new PdfPCell(new Phrase("SHIF", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            shifTable.AddCell(bCell);

            // Report Subtitle Header
            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Rectangle.NO_BORDER;
            shifTable.AddCell(reportNameCell);

            // Printed Date
            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            shifTable.AddCell(PrintedonCell);

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
            shifTable.AddCell(logoCell);

            document.Add(shifTable);
        }

        /// <summary>
        /// Builds main grid layout containing employee SHIF details and configures page header repeating[span_3](start_span)[span_3](end_span).
        /// </summary>
        private void AddDocBody()
        {
            PdfPTable shifTable = new PdfPTable(5);
            shifTable.WidthPercentage = 100;
            shifTable.SpacingBefore = 10f;

            // Set explicit column widths (No, Surname, Other Names, ID No, Amount)
            shifTable.SetWidths(new float[] { 10f, 25f, 25f, 20f, 20f });

            if (_notificationmessageEventname != null)
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating table header...", TAG));

            // Add Header Row
            AddBodyTableHeaders(shifTable);

            // Repeat the header row on top of every new page created by table page breaks
            shifTable.HeaderRows = 1;

            if (_notificationmessageEventname != null)
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating table details...", TAG));

            // Add Detail Rows
            foreach (var n in _ViewModel.PayList)
            {
                AddBodyTableDetails(n, shifTable);
            }

            // Add Total Row
            AddBodyTableTotal(shifTable);

            document.Add(shifTable);
        }

        /// <summary>
        /// Adds structural headers to the dataset grid[span_4](start_span)[span_4](end_span).
        /// </summary>
        private void AddBodyTableHeaders(PdfPTable shifTable)
        {
            string[] headers = new string[] { "No", "Surname", "Other Names", "Id No", "Amount\nKshs" };

            foreach (string header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, tHfont1));
                cell.Border = Rectangle.BOX;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Padding = 4f;
                shifTable.AddCell(cell);
            }
        }

        /// <summary>
        /// Generates individual employee detail rows[span_5](start_span)[span_5](end_span).
        /// </summary>
        private void AddBodyTableDetails(DAL.psuedovwPayrollMaster shifRec, PdfPTable shifTable)
        {
            // Col 1: Employee Number
            PdfPCell cellA = new PdfPCell(new Phrase(shifRec.EmpNo, helv8Font));
            cellA.HorizontalAlignment = Element.ALIGN_LEFT;
            cellA.Padding = 3f;
            shifTable.AddCell(cellA);

            // Col 2: Surname
            PdfPCell cellB = new PdfPCell(new Phrase(shifRec.Surname, helv8Font));
            cellB.HorizontalAlignment = Element.ALIGN_LEFT;
            cellB.Padding = 3f;
            shifTable.AddCell(cellB);

            // Col 3: Other Names
            PdfPCell cellC = new PdfPCell(new Phrase(shifRec.OtherNames, helv8Font));
            cellC.HorizontalAlignment = Element.ALIGN_LEFT;
            cellC.Padding = 3f;
            shifTable.AddCell(cellC);

            // Col 4: National ID
            PdfPCell cellD = new PdfPCell(new Phrase(shifRec.IDNo, helv8Font));
            cellD.HorizontalAlignment = Element.ALIGN_LEFT;
            cellD.Padding = 3f;
            shifTable.AddCell(cellD);

            // Col 5: SHIF Amount
            PdfPCell cellE2 = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", shifRec.SHIF), helv8Font));
            cellE2.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellE2.Padding = 3f;
            shifTable.AddCell(cellE2);
        }

        /// <summary>
        /// Renders aggregate totals at the bottom of the dataset grid[span_6](start_span)[span_6](end_span).
        /// </summary>
        private void AddBodyTableTotal(PdfPTable shifTable)
        {
            PdfPCell totalLabelCell = new PdfPCell(new Phrase("TOTAL", tHFont));
            totalLabelCell.Colspan = 4;
            totalLabelCell.HorizontalAlignment = Element.ALIGN_LEFT;
            totalLabelCell.Padding = 4f;
            shifTable.AddCell(totalLabelCell);

            PdfPCell totalValueCell = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalSHIF), tHFont));
            totalValueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalValueCell.Padding = 4f;
            shifTable.AddCell(totalValueCell);
        }

        /// <summary>
        /// Appends standard document footer/signature area[span_7](start_span)[span_7](end_span).
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
