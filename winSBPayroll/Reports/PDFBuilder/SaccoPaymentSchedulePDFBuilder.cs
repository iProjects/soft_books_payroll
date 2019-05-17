using System;
using System.IO;
using BLL;
using BLL.DataEntry;
//Payroll
using BLL.KRA.Models;
using CommonLib;
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDFBuilder
{
    public class SaccoPaymentSchedulePDFBuilder
    { 
         SaccoPaymentScheduleModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;


        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        Font tHFont = new Font(Font.TIMES_ROMAN, 10, Font.BOLD); 
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);
        Font tcFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 6, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 11, Font.BOLD);
        Font rms11Bold = new Font(Font.HELVETICA, 11, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);


        public SaccoPaymentSchedulePDFBuilder(SaccoPaymentScheduleModel SaccoPaymentScheduleModel, string FileName)
        {
            if (SaccoPaymentScheduleModel == null)
                throw new ArgumentNullException("SaccoPaymentScheduleModel is null");
            _ViewModel = SaccoPaymentScheduleModel;

            sFilePDF = FileName;
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
                // step 1: creation of a document-object
                document = new Document(PageSize.A4.Rotate());

                // step 2: we create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));


                document.Open();


                //document header
                AddDocHeader();


               
                Table rTable = new Table(5);
                rTable.Padding = 1;
                rTable.Spacing = 1;
                rTable.WidthPercentage = 100;

                //Add table headers
                AddTableHeaders(rTable);

                //Add table details
                foreach (var tr in _ViewModel.saccorepaymentschedule)
                {                    
                    AddTableRow(rTable, tr);
                }

                //Add totals
                AddTotals(rTable);

                document.Add(rTable);
                AddFooter();

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

            Table saccoPaymentsTable = new Table(5);
            saccoPaymentsTable.WidthPercentage = 100;
            saccoPaymentsTable.Padding = 1;
            saccoPaymentsTable.Spacing = 1;
            saccoPaymentsTable.Border = Table.NO_BORDER;

            Cell employernameCell = new Cell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Cell.NO_BORDER;
            saccoPaymentsTable.AddCell(employernameCell);

            Cell employeraddressCell = new Cell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Cell.NO_BORDER;
            saccoPaymentsTable.AddCell(employeraddressCell);

            Cell bCell = new Cell(new Phrase("SACCO CONTRIBUTIONS", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            saccoPaymentsTable.AddCell(bCell);  

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            saccoPaymentsTable.AddCell(reportNameCell); 

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            saccoPaymentsTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen();
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            saccoPaymentsTable.AddCell(logoCell);

            document.Add(saccoPaymentsTable);
        }

        private void AddTableHeaders(Table saccoPaymentsTable)
        {
            Cell employeeNoCell = new Cell(new Phrase("No", tHFont));
            employeeNoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            saccoPaymentsTable.AddCell(employeeNoCell);

            Cell employeeNameCell = new Cell(new Phrase("Name", tHFont));
            employeeNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            saccoPaymentsTable.AddCell(employeeNameCell);

            Cell descriptionCell = new Cell(new Phrase("Description", tHFont));
            descriptionCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            saccoPaymentsTable.AddCell(descriptionCell);

            Cell monthlyContribCell = new Cell(new Phrase("Monthly Contribution\nKshs", tHFont));
            monthlyContribCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            saccoPaymentsTable.AddCell(monthlyContribCell);

            Cell totalSharesCell = new Cell(new Phrase("Total Shares\nKshs", tHFont));
            totalSharesCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            saccoPaymentsTable.AddCell(totalSharesCell); 

        }

        private void AddTableRow(Table saccoPaymentsTable, saccorepayment tr)
        {
            if (tr != null)
            {
                //Totals

                Cell B = new Cell(new Phrase(tr.employeenumber , tcFont));
                B.HorizontalAlignment = Cell.ALIGN_LEFT;
                saccoPaymentsTable.AddCell(B);//Col 2

                Cell C = new Cell(new Phrase(tr.employeename , tcFont));
                C.HorizontalAlignment = Cell.ALIGN_LEFT;
                saccoPaymentsTable.AddCell(C);//Col 3

                Cell G = new Cell(new Phrase(tr.SaccoDescription, tcFont));
                G.HorizontalAlignment = Cell.ALIGN_LEFT;
                saccoPaymentsTable.AddCell(G);//Col 2

                Cell D = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", tr.monthamount), tcFont)); 
                D.HorizontalAlignment = Cell.ALIGN_RIGHT;
                saccoPaymentsTable.AddCell(D);//Col 2

                Cell E = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", tr.ytdamt), tcFont));
                E.HorizontalAlignment = Cell.ALIGN_RIGHT;
                saccoPaymentsTable.AddCell(E);//Col 2 

            }
        }


        private void AddTotals(Table saccoPaymentsTable)
        {

            Cell D = new Cell(new Phrase("TOTAL", rms10Bold));
            D.HorizontalAlignment = Cell.ALIGN_LEFT;
            D.Colspan = 3;
            saccoPaymentsTable.AddCell(D);

            Cell A = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalMonthAmount), rms10Bold));
            A.HorizontalAlignment = Cell.ALIGN_RIGHT;
            saccoPaymentsTable.AddCell(A);//Col 3    

            Cell B = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalShares), rms10Bold));
            B.HorizontalAlignment = Cell.ALIGN_RIGHT;
            saccoPaymentsTable.AddCell(B);//Col 3 
          }

        private void AddFooter()
        { 
            document.Add(new Phrase("\nSignature .....................................................................................................", rms10Normal));
            
        }


       


    }
}
