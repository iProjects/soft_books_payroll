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
    public class PayeePDFBuilder
    {
        public PAYEModel _ViewModel;
        Document document;
        string sFilePDF;
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
        Font tcFont1 = new Font(Font.HELVETICA, 9, Font.BOLD); // table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);

        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public PayeePDFBuilder(PAYEModel payeeModel, string filename, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (payeeModel == null)
                throw new ArgumentNullException("PAYEModel is null");
            _ViewModel = payeeModel;

            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
            sFilePDF = filename;

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed PayeePDFBuilder", TAG));
            }
        }

        public string GetPayeePDF()
        {
            try
            {
                BuildPayeePDF();
                return sFilePDF;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        private void BuildPayeePDF()
        {
            try
            {
                // Step 1: Initialize document in A4 Landscape orientation
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                // Step 2: Bind PdfWriter to file stream
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                // Step 3: Open document
                document.Open();

                // Add header
                AddHeader();

                // Add main table body
                AddTableBody();

                // Add footer
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

        private void AddHeader()
        {
            PdfPTable payeeTable = new PdfPTable(5);
            payeeTable.WidthPercentage = 100;

            // Employer Name Header
            PdfPCell employernameCell = new PdfPCell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Rectangle.NO_BORDER;
            payeeTable.AddCell(employernameCell);

            // Employer Address Header
            PdfPCell employeraddressCell = new PdfPCell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Rectangle.NO_BORDER;
            payeeTable.AddCell(employeraddressCell);

            // Report Title Header
            PdfPCell bCell = new PdfPCell(new Phrase("PAYE", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            payeeTable.AddCell(bCell);

            // Report Subtitle Header
            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Rectangle.NO_BORDER;
            payeeTable.AddCell(reportNameCell);

            // Printed Date
            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            payeeTable.AddCell(PrintedonCell);

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
            payeeTable.AddCell(logoCell);

            document.Add(payeeTable);
        }

        private void AddTableBody()
        {
            PdfPTable payeeTable = new PdfPTable(4);
            payeeTable.WidthPercentage = 100;
            payeeTable.SpacingBefore = 10f;

            // Explicit column width ratios (No, Name, Pin No, PAYE Tax)
            payeeTable.SetWidths(new float[] { 15f, 45f, 20f, 20f });

            // Add table headers
            PdfPCell cell = new PdfPCell(new Phrase("No", tHFont));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.VerticalAlignment = Element.ALIGN_MIDDLE;
            cell.Padding = 3f;
            payeeTable.AddCell(cell);

            PdfPCell nameTCell = new PdfPCell(new Phrase("Name", tHFont));
            nameTCell.HorizontalAlignment = Element.ALIGN_CENTER;
            nameTCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            nameTCell.Padding = 3f;
            payeeTable.AddCell(nameTCell);

            PdfPCell idTCell = new PdfPCell(new Phrase("Pin No", tHFont));
            idTCell.HorizontalAlignment = Element.ALIGN_CENTER;
            idTCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            idTCell.Padding = 3f;
            payeeTable.AddCell(idTCell);

            PdfPCell amtCell = new PdfPCell(new Phrase("PAYE Tax\nKshs", tHFont));
            amtCell.HorizontalAlignment = Element.ALIGN_CENTER;
            amtCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            amtCell.Padding = 3f;
            payeeTable.AddCell(amtCell);

            // Repeat the header row on top of every page
            payeeTable.HeaderRows = 1;

            // Add table details
            foreach (var pay in _ViewModel.PAYEItemList)
            {
                PdfPCell empnocell = new PdfPCell(new Phrase(pay.EmpNo, tcFont));
                empnocell.HorizontalAlignment = Element.ALIGN_LEFT;
                empnocell.Padding = 2f;
                payeeTable.AddCell(empnocell);

                PdfPCell nameCell = new PdfPCell(new Phrase(pay.Surname.Trim() + ",  " + pay.OtherNames.Trim(), tcFont));
                nameCell.HorizontalAlignment = Element.ALIGN_LEFT;
                nameCell.Padding = 2f;
                payeeTable.AddCell(nameCell);

                PdfPCell pinCell = new PdfPCell(new Phrase(pay.PINNo, tcFont));
                pinCell.HorizontalAlignment = Element.ALIGN_LEFT;
                pinCell.Padding = 2f;
                payeeTable.AddCell(pinCell);

                PdfPCell taxCell = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", pay.PayeTax), tcFont));
                taxCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                taxCell.Padding = 2f;
                payeeTable.AddCell(taxCell);
            }

            // Total Payments Summary Row
            PdfPCell totalCell = new PdfPCell(new Phrase("TOTAL PAYMENTS", tcFont1));
            totalCell.HorizontalAlignment = Element.ALIGN_LEFT;
            totalCell.Colspan = 3;
            totalCell.Padding = 3f;
            payeeTable.AddCell(totalCell);

            PdfPCell totalvalueCell = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalPAYE), tcFont1));
            totalvalueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalvalueCell.Padding = 3f;
            payeeTable.AddCell(totalvalueCell);

            // Total Items Summary Row
            PdfPCell totalItemsCell = new PdfPCell(new Phrase("TOTAL ITEMS", tcFont1));
            totalItemsCell.HorizontalAlignment = Element.ALIGN_LEFT;
            totalItemsCell.Colspan = 3;
            totalItemsCell.Padding = 3f;
            payeeTable.AddCell(totalItemsCell);

            PdfPCell countCell = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalItems), tcFont1));
            countCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            countCell.Padding = 3f;
            payeeTable.AddCell(countCell);

            document.Add(payeeTable);
        }

        // Document footer
        private void AddDocFooter()
        {
            PdfPTable payeeTable = new PdfPTable(1);
            payeeTable.WidthPercentage = 100;
            payeeTable.SpacingBefore = 20f;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            sgCell.Border = Rectangle.NO_BORDER;
            payeeTable.AddCell(sgCell);

            document.Add(payeeTable);
        }
    }
}
