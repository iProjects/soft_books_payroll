using System;
using System.Globalization;
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

        // DEFINED FONTS - Using standard Font constructors without BaseColor or FontFamily
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); // body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); // body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD); // body
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); // table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); // table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL); // table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);

        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        // Constructor
        public NetSalaryPDFBuilder(NetSalaryReportModel netsalarymodel, string filename, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (netsalarymodel == null)
                throw new ArgumentNullException("NetSalaryReportModel is null");
            _ViewModel = netsalarymodel;

            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
            sFilePDF = filename;

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed NetSalaryPDFBuilder", TAG));
            }
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

        /* Build the document */
        private void BuildNetSalaryPDF()
        {
            try
            {
                // Step 1: Initialize document in A4 Landscape orientation
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                // Step 2: Bind PdfWriter to file stream
                writer = PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                // Step 3: Open document
                document.Open();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document header...", TAG));

                // Add header
                AddDocHeader();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document body...", TAG));

                // Add body
                AddDocBody();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document footer...", TAG));

                // Add footer
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

        // Document header using PdfPTable and PdfPCell
        private void AddDocHeader()
        {
            PdfPTable netSalaryTable = new PdfPTable(5);
            netSalaryTable.WidthPercentage = 100;

            // Employer Name Header
            PdfPCell employernameCell = new PdfPCell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Rectangle.NO_BORDER;
            netSalaryTable.AddCell(employernameCell);

            // Employer Address Header
            PdfPCell employeraddressCell = new PdfPCell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Rectangle.NO_BORDER;
            netSalaryTable.AddCell(employeraddressCell);

            // Report Title Header
            PdfPCell bCell = new PdfPCell(new Phrase("NET SALARIES", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            netSalaryTable.AddCell(bCell);

            // Report Subtitle Header
            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Rectangle.NO_BORDER;
            netSalaryTable.AddCell(reportNameCell);

            // Printed Date
            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            netSalaryTable.AddCell(PrintedonCell);

            // Create Logo & Slogan Column
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
            netSalaryTable.AddCell(logoCell);

            document.Add(netSalaryTable);
        }

        // Document body constructed as a single PdfPTable to support repeating headers on each page
        private void AddDocBody()
        {
            PdfPTable netSalaryTable = new PdfPTable(4);
            netSalaryTable.WidthPercentage = 100;
            netSalaryTable.SpacingBefore = 10f;

            // Explicit column width ratios (No, Name, Pin, Net Salary)
            netSalaryTable.SetWidths(new float[] { 15f, 45f, 20f, 20f });

            // Table Headers
            PdfPCell cellNo = new PdfPCell(new Phrase("No", tHFont));
            cellNo.HorizontalAlignment = Element.ALIGN_CENTER;
            cellNo.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellNo.Padding = 3f;
            netSalaryTable.AddCell(cellNo);

            PdfPCell cellName = new PdfPCell(new Phrase("Name", tHFont));
            cellName.HorizontalAlignment = Element.ALIGN_CENTER;
            cellName.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellName.Padding = 3f;
            netSalaryTable.AddCell(cellName);

            PdfPCell cellPin = new PdfPCell(new Phrase("Pin", tHFont));
            cellPin.HorizontalAlignment = Element.ALIGN_CENTER;
            cellPin.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellPin.Padding = 3f;
            netSalaryTable.AddCell(cellPin);

            PdfPCell cellAmount = new PdfPCell(new Phrase("Net Salary\nKshs", tHFont));
            cellAmount.HorizontalAlignment = Element.ALIGN_CENTER;
            cellAmount.VerticalAlignment = Element.ALIGN_MIDDLE;
            cellAmount.Padding = 3f;
            netSalaryTable.AddCell(cellAmount);

            // Configure header row to repeat on top of every page
            netSalaryTable.HeaderRows = 1;

            // Table Details
            foreach (var pay in _ViewModel.salarylist)
            {
                PdfPCell empno = new PdfPCell(new Phrase(pay.employeeno, tcFont));
                empno.HorizontalAlignment = Element.ALIGN_LEFT;
                empno.Padding = 2f;
                netSalaryTable.AddCell(empno);

                PdfPCell surname = new PdfPCell(new Phrase(pay.employeename.Trim(), tcFont));
                surname.HorizontalAlignment = Element.ALIGN_LEFT;
                surname.Padding = 2f;
                netSalaryTable.AddCell(surname);

                PdfPCell pinno = new PdfPCell(new Phrase(pay.employeepin, tcFont));
                pinno.HorizontalAlignment = Element.ALIGN_LEFT;
                pinno.Padding = 2f;
                netSalaryTable.AddCell(pinno);

                PdfPCell netpay = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", pay.totalamount), tcFont));
                netpay.HorizontalAlignment = Element.ALIGN_RIGHT;
                netpay.Padding = 2f;
                netSalaryTable.AddCell(netpay);
            }

            // Table Totals
            PdfPCell total = new PdfPCell(new Phrase("TOTAL", rms6Bold));
            total.HorizontalAlignment = Element.ALIGN_LEFT;
            total.Colspan = 3;
            total.Padding = 3f;
            netSalaryTable.AddCell(total);

            PdfPCell totalamount = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.totalamount), rms6Bold));
            totalamount.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalamount.Padding = 3f;
            netSalaryTable.AddCell(totalamount);

            document.Add(netSalaryTable);
        }

        // Document footer
        private void AddDocFooter()
        {
            PdfPTable netSalaryTable = new PdfPTable(1);
            netSalaryTable.WidthPercentage = 100;
            netSalaryTable.SpacingBefore = 20f;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            sgCell.Border = Rectangle.NO_BORDER;
            netSalaryTable.AddCell(sgCell);

            document.Add(netSalaryTable);
        }
    }
}
