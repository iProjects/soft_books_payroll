using System;
using System.IO;
using System.Text;
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
    public class BankBranchTransferPDFBuilder
    {  
        BankTransferModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;

        //DEFINED fONTS
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 10, Font.NORMAL);//BODY
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//TABLE HEADER
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 6, Font.NORMAL);
        Font rms8Normal = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 6, Font.BOLD);
        Font rms8Bold = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);


        //constructor
        public BankBranchTransferPDFBuilder(BankTransferModel bbtransfermodel, string FileName)
        {
            if (bbtransfermodel == null)
                throw new ArgumentNullException("BankTransferModel is null");
            _ViewModel = bbtransfermodel; 

            sFilePDF = FileName;
        }

        public string GetPDF()
        {
            BuildPDF();
            return sFilePDF;
        }


        /*Build the document **/
        private void BuildPDF()
        {
            try
            {
                //step 1 creation of the document
                document = new Document(PageSize.A4);

                // step 2:create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open document
                document.Open();

                //document body
                AddDocBody();

                //document footer
                //AddFooter();

                //close document
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
            Table bankTransferTable = new Table(5);
            bankTransferTable.WidthPercentage = 100;
            bankTransferTable.Padding = 1;
            bankTransferTable.Spacing = 1;
            bankTransferTable.Border = Table.NO_BORDER;

            Cell employernameCell = new Cell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Cell.NO_BORDER;
            bankTransferTable.AddCell(employernameCell);

            Cell employeraddressCell = new Cell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Cell.NO_BORDER;
            bankTransferTable.AddCell(employeraddressCell);

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont1));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            bankTransferTable.AddCell(reportNameCell);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("The Manager");
            sb.AppendLine(_ViewModel.Bank);
            sb.AppendLine(_ViewModel.BankBranch);
            Cell salutationCell = new Cell(new Chunk(sb.ToString(), hfont2));
            salutationCell.Border = Cell.NO_BORDER;
            salutationCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            salutationCell.Colspan = 5;
            bankTransferTable.AddCell(salutationCell); 

            Cell reportdateCell = new Cell(new Phrase("Print Date:  " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), tHfont1));
            reportdateCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            reportdateCell.Colspan = 4;
            reportdateCell.Border = Cell.NO_BORDER;
            bankTransferTable.AddCell(reportdateCell);
            
            //create the logo
            PDFGen pdfgen = new PDFGen();
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            bankTransferTable.AddCell(logoCell);

            document.Add(bankTransferTable);

            document.Add(new Phrase("\n\nDear Sir/Madam", new Font(Font.TIMES_ROMAN, 8, Font.NORMAL)));

            document.Add(new Phrase("\nRE: CREDIT TRANSFER - SALARY PROCESSING", new Font(Font.TIMES_ROMAN, 8, Font.BOLD | Font.UNDERLINE)));

            document.Add(new Phrase("\n\nBelow is a list of Names, Banks, Branches, Account and Amounts to be credited in their respective Accounts", new Font(Font.TIMES_ROMAN, 8, Font.NORMAL)));
        }

        #region Body
        //document body
        private void AddDocBody()
        {
            if (_ViewModel.BankTransferItems.Count == 0)
            {
                //document header
                AddDocHeader();
                //document footer
                AddFooter();
            }
            foreach (var bankTransferItem in _ViewModel.BankTransferItems)
            {
                AddBankTransferItem(bankTransferItem);
                document.NewPage();
            }

        }

        private void AddBankTransferItem(BankTransferItem bti)
        {

            //document header
            AddDocHeader();


            Table bankTransferTable = new Table(5);
            bankTransferTable.Padding = 1;
            bankTransferTable.Spacing = 1;
            bankTransferTable.WidthPercentage = 100;

            Cell A = new Cell(new Phrase("Bank :  " + bti.BankName, tcFont));
            A.HorizontalAlignment = Cell.ALIGN_CENTER;
            A.Colspan = 5;
            bankTransferTable.AddCell(A);

            //add table header
            AddBankTransferItemTableHeaders(bankTransferTable);

            //add table details
            foreach (var ti in bti.TransferItems)
            {
                AddTransferItem(ti, bankTransferTable);
            }

            //add table totals
            AddBankTransferItemTableTotals(bti, bankTransferTable);

            document.Add(bankTransferTable);

        }


        //table header
        private void AddBankTransferItemTableHeaders(Table bankTransferTable)
        {
            Cell employeenumberCell = new Cell(new Phrase("No", tHfont1));
            employeenumberCell.Border = Cell.RECTANGLE;
            employeenumberCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bankTransferTable.AddCell(employeenumberCell);

            Cell employeenameCell = new Cell(new Phrase("Name", tHfont1));
            employeenameCell.Border = Cell.RECTANGLE;
            employeenameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bankTransferTable.AddCell(employeenameCell);

            Cell branchCell = new Cell(new Phrase("Branch", tHfont1));
            branchCell.Border = Cell.RECTANGLE;
            branchCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bankTransferTable.AddCell(branchCell);

            Cell accountNoCell = new Cell(new Phrase("Account No", tHfont1));
            accountNoCell.Border = Cell.RECTANGLE;
            accountNoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bankTransferTable.AddCell(accountNoCell);

            Cell amountCell = new Cell(new Phrase("Amount\nKshs", tHfont1));
            amountCell.Border = Cell.RECTANGLE;
            amountCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bankTransferTable.AddCell(amountCell);
        }



        //table  details
        private void AddTransferItem(TransferItem det, Table atable)
        {

            Cell A = new Cell(new Phrase(det.EmpNo, tcFont));
            A.HorizontalAlignment = Cell.ALIGN_LEFT;
            atable.AddCell(A);

            Cell B = new Cell(new Phrase(det.EmpName, tcFont));
            B.HorizontalAlignment = Cell.ALIGN_LEFT;
            atable.AddCell(B);


            Cell D = new Cell(new Phrase(det.BranchName, tcFont));
            D.HorizontalAlignment = Cell.ALIGN_LEFT;
            atable.AddCell(D);

            Cell E = new Cell(new Phrase(det.AccountNo, tcFont));
            E.HorizontalAlignment = Cell.ALIGN_LEFT;
            atable.AddCell(E);

            Cell F = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", det.Amount), tcFont));
            F.HorizontalAlignment = Cell.ALIGN_RIGHT;
            atable.AddCell(F);
        }

        //table totals
        private void AddBankTransferItemTableTotals(BankTransferItem bti, Table atable)
        {


            Cell E = new Cell(new Phrase("TOTAL", tHfont1));
            E.HorizontalAlignment = Cell.ALIGN_LEFT;
            E.Colspan = 4;
            atable.AddCell(E);

            Cell F = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", bti.Total), tcFont));
            F.HorizontalAlignment = Cell.ALIGN_RIGHT;
            atable.AddCell(F);
        }

        #endregion

        //document footer
        private void AddFooter()
        {

            Table bankTransferTable = new Table(6);
            bankTransferTable.Padding = 1;
            bankTransferTable.Spacing = 1;
            bankTransferTable.WidthPercentage = 100;

            Cell G = new Cell(new Phrase("Grand Total = ", hfont2));
            G.HorizontalAlignment = Cell.ALIGN_RIGHT;
            G.Colspan = 5;
            bankTransferTable.AddCell(G);

            Cell H = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.GrandTotal), hfont2));
            H.HorizontalAlignment = Cell.ALIGN_RIGHT;
            bankTransferTable.AddCell(H);


            document.Add(bankTransferTable);


            document.Add(new Phrase("\nKindly credit the Accounts with the amounts specified on the above date and debit Account: " + _ViewModel.AccountName + " Account No: " + _ViewModel.AccountNo + ".", new Font(Font.TIMES_ROMAN, 8, Font.NORMAL)));

            document.Add(new Phrase("\nYOURS FAITHFULLY \n\n\n", new Font(Font.BOLD, 8, Font.NORMAL)));
            document.Add(new Phrase(_ViewModel.AccountSignatory, new Font(Font.BOLD, 8, Font.NORMAL)));
            document.Add(new Phrase("\nFOR: " + _ViewModel.employername, new Font(Font.TIMES_ROMAN, 8, Font.BOLD | Font.UNDERLINE)));


        }



    }
}