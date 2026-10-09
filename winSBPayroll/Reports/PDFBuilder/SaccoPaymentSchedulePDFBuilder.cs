using System;
using System.IO;
using BLL;
using BLL.DataEntry;
// Payroll
using BLL.KRA.Models;
using CommonLib;
using DAL;
// iTextSharp
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDFBuilder
{
    public class SaccoPaymentSchedulePDFBuilder
    {
        private SaccoPaymentScheduleModel _ViewModel;
        private Document document;
        private string Message;
        private string sFilePDF;

        // Standard iTextSharp Font and Color usage (compatible with .NET 4)
        private Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        private Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        private Font tHFont = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        private Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);
        private Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        private Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);

        private event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        private string TAG;

        public SaccoPaymentSchedulePDFBuilder(SaccoPaymentScheduleModel SaccoPaymentScheduleModel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (SaccoPaymentScheduleModel == null)
                throw new ArgumentNullException("SaccoPaymentScheduleModel is null");
            _ViewModel = SaccoPaymentScheduleModel;

            TAG = this.GetType().Name;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed SaccoPaymentSchedulePDFBuilder", TAG));
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
                // Step 1: Create document-object
                document = new Document(PageSize.A4.Rotate(), 36, 36, 36, 36);

                // Step 2: Create writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                document.Open();

                // Add Document Header
                AddDocHeader();

                // Main Data Table setup
                PdfPTable rTable = new PdfPTable(5);
                rTable.WidthPercentage = 100;
                rTable.HeaderRows = 1; // Repeat Table Header on top of each page

                // Add table headers
                AddTableHeaders(rTable);

                // Add table details
                foreach (var tr in _ViewModel.saccorepaymentschedule)
                {
                    AddTableRow(rTable, tr);
                }

                // Add totals
                AddTotals(rTable);

                document.Add(rTable);
                AddFooter();

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
            PdfPTable saccoPaymentsTable = new PdfPTable(5);
            saccoPaymentsTable.WidthPercentage = 100;

            PdfPCell employernameCell = new PdfPCell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Rectangle.NO_BORDER;
            saccoPaymentsTable.AddCell(employernameCell);

            PdfPCell employeraddressCell = new PdfPCell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Rectangle.NO_BORDER;
            saccoPaymentsTable.AddCell(employeraddressCell);

            PdfPCell bCell = new PdfPCell(new Phrase("SACCO CONTRIBUTIONS", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            saccoPaymentsTable.AddCell(bCell);

            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Rectangle.NO_BORDER;
            saccoPaymentsTable.AddCell(reportNameCell);

            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            saccoPaymentsTable.AddCell(PrintedonCell);

            // Create logo image
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;

            PdfPCell logoCell = new PdfPCell(img0);
            logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
            logoCell.Border = Rectangle.NO_BORDER;
            logoCell.AddElement(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            saccoPaymentsTable.AddCell(logoCell);

            document.Add(saccoPaymentsTable);
        }

        private void AddTableHeaders(PdfPTable saccoPaymentsTable)
        {
            saccoPaymentsTable.AddCell(CreateHeaderCell("No"));
            saccoPaymentsTable.AddCell(CreateHeaderCell("Name"));
            saccoPaymentsTable.AddCell(CreateHeaderCell("Description"));
            saccoPaymentsTable.AddCell(CreateHeaderCell("Monthly Contribution\nKshs"));
            saccoPaymentsTable.AddCell(CreateHeaderCell("Total Shares\nKshs"));
        }

        private void AddTableRow(PdfPTable saccoPaymentsTable, saccorepayment tr)
        {
            if (tr != null)
            {
                saccoPaymentsTable.AddCell(CreateDataCell(tr.employeenumber, Element.ALIGN_LEFT, tcFont));
                saccoPaymentsTable.AddCell(CreateDataCell(tr.employeename, Element.ALIGN_LEFT, tcFont));
                saccoPaymentsTable.AddCell(CreateDataCell(tr.SaccoDescription, Element.ALIGN_LEFT, tcFont));
                saccoPaymentsTable.AddCell(CreateDataCell(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", tr.monthamount), Element.ALIGN_RIGHT, tcFont));
                saccoPaymentsTable.AddCell(CreateDataCell(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", tr.ytdamt), Element.ALIGN_RIGHT, tcFont));
            }
        }

        private void AddTotals(PdfPTable saccoPaymentsTable)
        {
            PdfPCell totalLabelCell = new PdfPCell(new Phrase("TOTAL", rms10Bold));
            totalLabelCell.HorizontalAlignment = Element.ALIGN_LEFT;
            totalLabelCell.Colspan = 3;
            totalLabelCell.Border = Rectangle.BOX;
            saccoPaymentsTable.AddCell(totalLabelCell);

            saccoPaymentsTable.AddCell(CreateDataCell(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalMonthAmount), Element.ALIGN_RIGHT, rms10Bold));
            saccoPaymentsTable.AddCell(CreateDataCell(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalShares), Element.ALIGN_RIGHT, rms10Bold));
        }

        private PdfPCell CreateHeaderCell(string title)
        {
            PdfPCell cell = new PdfPCell(new Phrase(title, tHFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = Rectangle.BOX;
            return cell;
        }

        private PdfPCell CreateDataCell(string text, int alignment, Font font)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = alignment;
            cell.Border = Rectangle.BOX;
            return cell;
        }

        private void AddFooter()
        {
            document.Add(new Phrase("\nSignature .....................................................................................................", rms10Normal));
        }
    }
}
