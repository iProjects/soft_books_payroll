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
    public class NHIFPDFBuilder
    {
        public NHIFReportModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;

        //DEFINED fONTS
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); //table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font helv8Font = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell 
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        //constructor
        public NHIFPDFBuilder(NHIFReportModel nhifreportmodel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (nhifreportmodel == null)
                throw new ArgumentNullException("NHIFReportModel is null");
            _ViewModel = nhifreportmodel;

            _notificationmessageEventname = notificationmessageEventname;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed NHIFPDFBuilder", TAG));

            sFilePDF = FileName;
        }
        public string GetPDF()
        {
            try
            {
                BuildNHIFPDF();
                return sFilePDF;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        /*Build the document **/
        private void BuildNHIFPDF()
        {

            try
            {
                //step 1 creation of the document
                document = new Document(PageSize.A4.Rotate());

                // step 2: we create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open the document
                document.Open();

                //add header
                AddDocHeader();

                //add body
                AddDocBody();

                //add footer
                AddDocFooter();

                //close the document
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

            Table nhifTable = new Table(5);
            nhifTable.WidthPercentage = 100;
            nhifTable.Padding = 1;
            nhifTable.Spacing = 1;
            nhifTable.Border = Table.NO_BORDER;

            Cell EmployerNameCell = new Cell(new Phrase(_ViewModel.EmployerName.ToUpper() + "\n", new Font(Font.TIMES_ROMAN, 15, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            EmployerNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            EmployerNameCell.Colspan = 5;
            EmployerNameCell.Border = Cell.NO_BORDER;
            nhifTable.AddCell(EmployerNameCell);

            Cell employerAdressCell = new Cell(new Phrase(_ViewModel.EmpAddress, new Font(Font.TIMES_ROMAN, 9, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employerAdressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employerAdressCell.Colspan = 5;
            employerAdressCell.Border = Cell.NO_BORDER;
            nhifTable.AddCell(employerAdressCell);

            Cell bCell = new Cell(new Phrase("NHIF", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            nhifTable.AddCell(bCell);

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            nhifTable.AddCell(reportNameCell);

            Cell employerNHIFCell = new Cell(new Phrase("NHIF No: " + _ViewModel.EmployerCode.Trim(), hFont2));
            employerNHIFCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employerNHIFCell.Border = Cell.NO_BORDER;
            employerNHIFCell.Colspan = 5;
            nhifTable.AddCell(employerNHIFCell);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            nhifTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            nhifTable.AddCell(logoCell);

            document.Add(nhifTable);
        }


        //document body
        private void AddDocBody()
        {

            Table nhifTable = new Table(6);
            nhifTable.Border = Table.RECTANGLE;
            nhifTable.Border = Table.ALIGN_CENTER;
            nhifTable.Padding = 1;
            nhifTable.Spacing = 1;
            nhifTable.WidthPercentage = 100;

            //add table header
            AddBodyTableHeaders(nhifTable);

            //addtable details
            foreach (var n in _ViewModel.PayList)
            {
                AddBodyTableDetails(n, nhifTable);
            }

            //add table totals
            AddBodyTableTotal(nhifTable);

            document.Add(nhifTable);

        }

        //table headers
        private void AddBodyTableHeaders(Table nhifTable)
        {
            Cell employeenumberCell = new Cell(new Phrase("No", tHfont1));
            employeenumberCell.Border = Cell.RECTANGLE;
            employeenumberCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nhifTable.AddCell(employeenumberCell);

            Cell surNameCell = new Cell(new Phrase("Surname", tHfont1));
            surNameCell.Border = Cell.RECTANGLE;
            surNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nhifTable.AddCell(surNameCell);

            Cell otherNamesCell = new Cell(new Phrase("Other Names", tHfont1));
            otherNamesCell.Border = Cell.RECTANGLE;
            otherNamesCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nhifTable.AddCell(otherNamesCell);

            Cell idNoCell = new Cell(new Phrase("Id No", tHfont1));
            idNoCell.Border = Cell.RECTANGLE;
            idNoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nhifTable.AddCell(idNoCell);

            Cell nhifNoCell = new Cell(new Phrase("NHIF No", tHfont1));
            nhifNoCell.Border = Cell.RECTANGLE;
            nhifNoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nhifTable.AddCell(nhifNoCell);

            Cell amountCell = new Cell(new Phrase("Amount\nKshs", tHfont1));
            amountCell.Border = Cell.RECTANGLE;
            amountCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nhifTable.AddCell(amountCell);
        }

        //table details
        private void AddBodyTableDetails(DAL.psuedovwPayrollMaster nhifRec, Table nhifTable)
        {
            Cell A = new Cell(new Phrase(nhifRec.EmpNo, helv8Font));
            A.HorizontalAlignment = Cell.ALIGN_LEFT;
            nhifTable.AddCell(A);//Col 1

            Cell B = new Cell(new Phrase(nhifRec.Surname, helv8Font));
            B.HorizontalAlignment = Cell.ALIGN_LEFT;
            nhifTable.AddCell(B);//Col 2

            Cell C = new Cell(new Phrase(nhifRec.OtherNames, helv8Font));
            C.HorizontalAlignment = Cell.ALIGN_LEFT;
            nhifTable.AddCell(C);//Col 3

            Cell D = new Cell(new Phrase(nhifRec.IDNo, helv8Font));
            D.HorizontalAlignment = Cell.ALIGN_LEFT;
            nhifTable.AddCell(D);//Col 4

            Cell E1 = new Cell(new Phrase(nhifRec.NHIFNo, helv8Font));
            E1.HorizontalAlignment = Cell.ALIGN_LEFT;
            nhifTable.AddCell(E1);//Col 5

            Cell E2 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", nhifRec.NHIF), helv8Font));
            E2.HorizontalAlignment = Cell.ALIGN_RIGHT;
            nhifTable.AddCell(E2);//Col 6
        }


        //table totals
        private void AddBodyTableTotal(Table nhifTable)
        {
            Cell E1 = new Cell(new Phrase("TOTAL", tHFont));
            E1.Colspan = 5;
            E1.HorizontalAlignment = Cell.ALIGN_LEFT;
            nhifTable.AddCell(E1);

            Cell E2 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalNHIF), tHFont));
            E2.HorizontalAlignment = Cell.ALIGN_RIGHT;
            nhifTable.AddCell(E2);//Col 6

        }


        //document footer
        private void AddDocFooter()
        {

            Table nhifTable = new Table(1);
            nhifTable.WidthPercentage = 100;
            nhifTable.Border = Table.NO_BORDER;

            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            nhifTable.AddCell(sgCell);

            document.Add(nhifTable);
        }



    }
}
