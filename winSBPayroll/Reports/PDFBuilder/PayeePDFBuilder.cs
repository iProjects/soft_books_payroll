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
    public class PayeePDFBuilder
    {
        public PAYEModel _ViewModel;
        Document document;
        string sFilePDF;
        string Message;

        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); //table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font tcFont1 = new Font(Font.HELVETICA, 9, Font.BOLD);//table cell
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

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = filename;
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
                document = new Document(PageSize.A4.Rotate());
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                document.Open();

                AddHeader();

                AddTableBody();

                AddDocFooter();

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

        private void AddHeader()
        {
            Table payeeTable = new Table(5);
            payeeTable.WidthPercentage = 100;
            payeeTable.Padding = 1;
            payeeTable.Spacing = 1;
            payeeTable.Border = Table.NO_BORDER;

            Cell employernameCell = new Cell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Cell.NO_BORDER;
            payeeTable.AddCell(employernameCell);

            Cell employeraddressCell = new Cell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Cell.NO_BORDER;
            payeeTable.AddCell(employeraddressCell);

            Cell bCell = new Cell(new Phrase("PAYE", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            payeeTable.AddCell(bCell);

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            payeeTable.AddCell(reportNameCell);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            payeeTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            payeeTable.AddCell(logoCell);

            document.Add(payeeTable);
        }

        private void AddTableBody()
        {
            //
            Table payeeTable = new Table(4);
            payeeTable.WidthPercentage = 100;
            payeeTable.Padding = 2;
            payeeTable.Spacing = 1;

            //Add table headers
            Cell cell = new Cell(new Phrase("No", tHFont));
            cell.HorizontalAlignment = Cell.ALIGN_CENTER;
            payeeTable.AddCell(cell);

            Cell nameTCell = new Cell(new Phrase("Name", tHFont));
            nameTCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            payeeTable.AddCell(nameTCell);

            Cell idTCell = new Cell(new Phrase("Pin No", tHFont));
            idTCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            payeeTable.AddCell(idTCell);

            Cell amtCell = new Cell(new Phrase("PAYE Tax\nKshs", tHFont));
            amtCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            payeeTable.AddCell(amtCell);

            //add table details
            foreach (var pay in _ViewModel.PAYEItemList)
            {
                Cell empnocell = new Cell(new Phrase(pay.EmpNo, tcFont));
                empnocell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payeeTable.AddCell(empnocell);

                Cell nameCell = new Cell(new Phrase(pay.Surname.Trim() + ",  " + pay.OtherNames.Trim(), tcFont));
                nameCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payeeTable.AddCell(nameCell);

                Cell pinCell = new Cell(new Phrase(pay.PINNo, tcFont));
                pinCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payeeTable.AddCell(pinCell);

                Cell taxCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", pay.PayeTax), tcFont));
                taxCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payeeTable.AddCell(taxCell);

            }

            Cell totalCell = new Cell(new Phrase("TOTAL PAYMENTS", tcFont1));
            totalCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            totalCell.Colspan = 3;
            payeeTable.AddCell(totalCell);

            Cell totalvalueCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalPAYE), tcFont1));
            totalvalueCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payeeTable.AddCell(totalvalueCell);

            Cell totalItemsCell = new Cell(new Phrase("TOTAL ITEMS", tcFont1));
            totalItemsCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            totalItemsCell.Colspan = 3;
            payeeTable.AddCell(totalItemsCell);

            Cell countCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalItems), tcFont1));
            countCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payeeTable.AddCell(countCell);

            document.Add(payeeTable);
        }


        //document footer
        private void AddDocFooter()
        {

            Table payeeTable = new Table(1);
            payeeTable.WidthPercentage = 100;
            payeeTable.Border = Table.NO_BORDER;

            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            payeeTable.AddCell(sgCell);

            document.Add(payeeTable);
        }




    }
}
