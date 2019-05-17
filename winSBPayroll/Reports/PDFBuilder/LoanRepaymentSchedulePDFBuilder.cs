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
    public class LoanRepaymentSchedulePDFBuilder
    {
       
        LoanRepaymentScheduleModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;


        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);//body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 10, Font.BOLD); //table Header
        Font tHFont1 = new Font(Font.TIMES_ROMAN, 9, Font.BOLD);//TABLE HEADER
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 6, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms11Bold = new Font(Font.HELVETICA, 11, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);

        public LoanRepaymentSchedulePDFBuilder(LoanRepaymentScheduleModel loanrepaymentshedulemodel, string FileName)
        {
            if (loanrepaymentshedulemodel == null)
                throw new ArgumentNullException("LoanRepaymentScheduleModel is null");
            _ViewModel = loanrepaymentshedulemodel;

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

                Table rTable = new Table(6);
                rTable.Padding = 1;
                rTable.Spacing = 1;
                rTable.WidthPercentage = 100;

                //Add table headers
                AddTableHeaders(rTable);

                //Add table details
                foreach (var tr in _ViewModel.loanslist)
                {                    
                    AddTableRow(rTable, tr);
                }

                //Add totals
                AddTotals(rTable);

                document.Add(rTable);
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

        //document header
        private void AddDocHeader()
        {

            Table loanRePaymentsTable = new Table(5);
            loanRePaymentsTable.WidthPercentage = 100;
            loanRePaymentsTable.Padding = 1;
            loanRePaymentsTable.Spacing = 1;
            loanRePaymentsTable.Border = Table.NO_BORDER;

            Cell employernameCell = new Cell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Cell.NO_BORDER;
            loanRePaymentsTable.AddCell(employernameCell);

            Cell employeraddressCell = new Cell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Cell.NO_BORDER;
            loanRePaymentsTable.AddCell(employeraddressCell);

            Cell bCell = new Cell(new Phrase("LOAN REPAYMENTS", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            loanRePaymentsTable.AddCell(bCell);

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            loanRePaymentsTable.AddCell(reportNameCell);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            loanRePaymentsTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen();
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            loanRePaymentsTable.AddCell(logoCell);

            document.Add(loanRePaymentsTable);

        }


        private void AddTableHeaders(Table loanRePaymentsTable)
        {
            Cell employeeNoCell = new Cell(new Phrase("No", tHFont));
            employeeNoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            loanRePaymentsTable.AddCell(employeeNoCell);

            Cell employeeNameCell = new Cell(new Phrase("Name", tHFont));
            employeeNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            loanRePaymentsTable.AddCell(employeeNameCell);

            Cell descriptionCell = new Cell(new Phrase("Description", tHFont));
            descriptionCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            loanRePaymentsTable.AddCell(descriptionCell);

            Cell startDateCell = new Cell(new Phrase("Start Date", tHFont));
            startDateCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            loanRePaymentsTable.AddCell(startDateCell);

            Cell mothAmountCell = new Cell(new Phrase("Month Amount\nKshs", tHFont));
            mothAmountCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            loanRePaymentsTable.AddCell(mothAmountCell);

            Cell balanceCell = new Cell(new Phrase("Balance\nKshs", tHFont));
            balanceCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            loanRePaymentsTable.AddCell(balanceCell);  
 
        }

        private void AddTableRow(Table loanRePaymentsTable,  loanrepayments  tr)
        {
            if (tr != null)
            {
                //Totals

                Cell B = new Cell(new Phrase(tr.employeenumber, tcFont));
                B.HorizontalAlignment = Cell.ALIGN_LEFT;
                loanRePaymentsTable.AddCell(B);//Col 2

                Cell C = new Cell(new Phrase(tr.employeename, tcFont));
                C.HorizontalAlignment = Cell.ALIGN_LEFT;
                loanRePaymentsTable.AddCell(C);//Col 3

                Cell D = new Cell(new Phrase(tr.loandescription, tcFont)); //Lookup for descripion
                D.HorizontalAlignment = Cell.ALIGN_LEFT;
                loanRePaymentsTable.AddCell(D);//Col 2

                Cell E = new Cell(new Phrase(tr.startdate.ToString("dd-MMM-yyyy"), tcFont));
                E.HorizontalAlignment = Cell.ALIGN_LEFT;
                loanRePaymentsTable.AddCell(E);//Col 2

                Cell F = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", tr.monthamount), tcFont));
                F.HorizontalAlignment = Cell.ALIGN_RIGHT;
                loanRePaymentsTable.AddCell(F);//Col 2

                decimal _tbalance = decimal.Parse(tr.balance.ToString());
                if (_tbalance < 0)
                {
                    _tbalance = _tbalance * -1;
                }
                Cell G = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _tbalance), tcFont));
                G.HorizontalAlignment = Cell.ALIGN_RIGHT;
                loanRePaymentsTable.AddCell(G);//Col 2

            }
            else 
            {
                Cell ErCell = new Cell(new Phrase("Payroll Must Be Closed!", tcFont));
                ErCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                loanRePaymentsTable.AddCell(ErCell);//Col 2
            
            }
        }



        private void AddTotals(Table loanRePaymentsTable)
        {
            

            Cell I = new Cell(new Phrase("TOTAL", rms10Bold));
            I.HorizontalAlignment = Cell.ALIGN_LEFT;
            I.Colspan = 4;
            loanRePaymentsTable.AddCell(I);

            Cell A = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalMonthAmount), rms10Bold));
            A.HorizontalAlignment = Cell.ALIGN_RIGHT;
            loanRePaymentsTable.AddCell(A);//Col 3    

            decimal _tbalance = decimal.Parse(_ViewModel.TotalBalance.ToString());
            if (_tbalance < 0)
            {
                _tbalance = _tbalance * -1;
            }
            Cell V = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _tbalance), rms10Bold));
            V.HorizontalAlignment = Cell.ALIGN_RIGHT;
            loanRePaymentsTable.AddCell(V);//Col 3 
          }

        //document footer
        private void AddDocFooter()
        {

            Table loanRePaymentsTable = new Table(1);
            loanRePaymentsTable.WidthPercentage = 100;
            loanRePaymentsTable.Border = Table.NO_BORDER;


            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            loanRePaymentsTable.AddCell(sgCell);


            document.Add(loanRePaymentsTable);

        }


       


    }


}
