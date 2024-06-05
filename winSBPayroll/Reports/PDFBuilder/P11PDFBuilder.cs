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
    public class P11PDFBuilder
    {
        P11ReportModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;

        //DEFINED fONTS
        Font hFont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLDITALIC);
        Font hFont2 = new Font(Font.HELVETICA, 12, Font.BOLD);
        Font bFont1 = new Font(Font.HELVETICA, 7, Font.NORMAL);
        Font bFont2 = new Font(Font.TIMES_ROMAN, 7, Font.NORMAL);
        Font thFont1 = new Font(Font.TIMES_ROMAN, 7, Font.BOLD);
        Font thFont2 = new Font(Font.HELVETICA, 7, Font.BOLD);
        Font tcFont1 = new Font(Font.HELVETICA, 7, Font.BOLD);
        Font tcFont2 = new Font(Font.TIMES_ROMAN, 7, Font.NORMAL);

        string _resourcePath;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        //constructor
        public P11PDFBuilder(string ResourcePath, P11ReportModel P11Model, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (P11Model == null)
                throw new ArgumentNullException("P11ReportModel is null");
            _ViewModel = P11Model;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;
            _resourcePath = ResourcePath;
        }

        public string GetP11PDF()
        {
            BuildPDF();
            return sFilePDF;
        }
        /*Build the document **/
        private void BuildPDF()
        {
            try
            {
                // step 1: creation of a document-object
                document = new Document(PageSize.A4);

                // step 2: create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open document
                document.Open();

                //Add Header 
                AddDocHeader();

                //Add Body
                AddDocBody();

                //add Footer
                AddDocFooter();

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

            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);

            Image img0 = pdfgen.DoGetImageFile(Path.Combine(_resourcePath, "kra2.jpg"));
            img0.Alignment = Image.ALIGN_CENTER;


            Table p11taxInfoTable = new Table(4);
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.NO_BORDER;


            Cell header = new Cell(img0);
            header.Border = Cell.NO_BORDER;
            header.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(header, new System.Drawing.Point(0, 1));

            Cell p11reportname = new Cell(new Phrase("Income  Tax Department", hFont1));
            p11reportname.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11reportname.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(p11reportname, new System.Drawing.Point(2, 1));


            Cell emppayeecreditslip = new Cell(new Phrase("P.A.Y.E CREDIT SLIP", hFont2));
            emppayeecreditslip.HorizontalAlignment = Cell.ALIGN_CENTER;
            emppayeecreditslip.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(emppayeecreditslip, new System.Drawing.Point(3, 1));

            Cell emporiginal = new Cell(new Phrase("ORIGINAL", tcFont2));
            emporiginal.HorizontalAlignment = Cell.ALIGN_RIGHT;
            emporiginal.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(emporiginal, new System.Drawing.Point(3, 3));


            Cell empserialno = new Cell(new Phrase("SERIAL NO......................", tcFont2));
            empserialno.HorizontalAlignment = Cell.ALIGN_LEFT;
            empserialno.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(empserialno, new System.Drawing.Point(4, 2));


            Cell emppayrollmonth = new Cell(new Phrase("Pay Roll Month..." + _ViewModel.payrollmonth, tcFont2));
            emppayrollmonth.HorizontalAlignment = Cell.ALIGN_LEFT;
            emppayrollmonth.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(emppayrollmonth, new System.Drawing.Point(5, 2));


            Cell empemployerpin = new Cell(new Phrase("Employer's P.I.N..." + "  " + _ViewModel.employerpin, tcFont2));
            empemployerpin.HorizontalAlignment = Cell.ALIGN_LEFT;
            empemployerpin.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(empemployerpin, new System.Drawing.Point(6, 2));

            document.Add(p11taxInfoTable);

            p11taxInfoTable = new Table(1);
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.NO_BORDER;


            Cell empemployername = new Cell(new Phrase("Employer's Name..." + _ViewModel.employername.ToUpper() + "\nTo...........................................Bank. \nPlease pay the Central Bank of Kenya , for the credit of the Paymaster -  General, PAYE Account No.   05-010-0056 \n " + "Amount..........." + _ViewModel.totalpayee.ToString("C2") + "\n(In words)  Kenya Shillings...........................................................................................................................................\n", tcFont2));
            empemployername.HorizontalAlignment = Cell.ALIGN_LEFT;
            empemployername.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(empemployername, new System.Drawing.Point(0, 0));


            Cell emppayment = new Cell(new Phrase("NATURE  OF  PAYMENT.", new Font(Font.TIMES_ROMAN, 8, Font.BOLD | Font.UNDERLINE)));
            emppayment.HorizontalAlignment = Cell.ALIGN_LEFT;
            emppayment.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(emppayment, new System.Drawing.Point(1, 0));

            Cell payment = new Cell(new Phrase("1...................................................................\n2.....................................................................", tcFont2));
            payment.HorizontalAlignment = Cell.ALIGN_LEFT;
            payment.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(payment, new System.Drawing.Point(2, 0));

            Cell pay = new Cell(new Phrase(".", bFont2));
            pay.HorizontalAlignment = Cell.ALIGN_LEFT;
            pay.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(pay, new System.Drawing.Point(3, 0));

            document.Add(p11taxInfoTable);
        }

        //document body
        private void AddDocBody()
        {
            //Add table headers
            AddTableHeaders();

            //Add table details            
            AddTableDetails();

            //Add table totals
            AddTableTotals();
        }
        //table headers
        private void AddTableHeaders()
        {
            Table p11taxInfoTable = new Table(5);
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.RECTANGLE;
            p11taxInfoTable.Border = Table.ALIGN_CENTER;
            p11taxInfoTable.WidthPercentage = 100;

            Chunk blank = new Chunk("", thFont1);
            Cell p11blank = new Cell(blank);
            p11blank.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11blank, new System.Drawing.Point(0, 0));

            Chunk notes = new Chunk("NOTES/COINS", thFont1);
            Cell p11notes = new Cell(notes);
            p11notes.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11notes, new System.Drawing.Point(0, 1));

            Chunk payetax = new Chunk("1. PAYE  TAX\nKSHS.", thFont1);
            Cell p11payetax = new Cell(payetax);
            p11payetax.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11payetax, new System.Drawing.Point(0, 2));

            Chunk interest = new Chunk("2. PAYE  \nINTEREST/PENALTY  \nKSHS.", thFont1);
            Cell p11interest = new Cell(interest);
            p11interest.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11interest, new System.Drawing.Point(0, 3));

            Chunk others = new Chunk("3. OTHERS  \n(WCPS . ETC)  \nKSHS.", thFont1);
            Cell p11others = new Cell(others);
            p11others.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11others, new System.Drawing.Point(0, 4));

            document.Add(p11taxInfoTable);
        }
        //table details
        private void AddTableDetails()
        {
            //first column
            Table p11taxInfoTable = new Table(5);
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.ALIGN_CENTER;

            Chunk bankstamp = new Chunk("Bank  Stamp  & \nTeller Initials", bFont1);
            Cell p11bankstamp = new Cell(bankstamp);
            p11bankstamp.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11bankstamp.Rowspan = 12;
            p11taxInfoTable.AddCell(p11bankstamp, new System.Drawing.Point(1, 0));

            //second column       
            p11taxInfoTable.Border = Table.ALIGN_CENTER;
            p11taxInfoTable.WidthPercentage = 100;

            Chunk thousand = new Chunk("Shs.1000/-each", bFont1);
            Cell p11thousand = new Cell(thousand);
            p11thousand.HorizontalAlignment = Cell.ALIGN_RIGHT;
            p11taxInfoTable.AddCell(p11thousand, new System.Drawing.Point(1, 1));

            Chunk five = new Chunk("Shs.500/-", bFont1);
            Cell p11five = new Cell(five);
            p11five.HorizontalAlignment = Cell.ALIGN_RIGHT;
            p11taxInfoTable.AddCell(p11five, new System.Drawing.Point(2, 1));

            Chunk two = new Chunk("Shs.200/-", bFont1);
            Cell p11two = new Cell(two);
            p11two.HorizontalAlignment = Cell.ALIGN_RIGHT;
            p11taxInfoTable.AddCell(p11two, new System.Drawing.Point(3, 1));

            Chunk hundred = new Chunk("Shs.100/-", bFont1);
            Cell p11hundred = new Cell(hundred);
            p11hundred.HorizontalAlignment = Cell.ALIGN_RIGHT;
            p11taxInfoTable.AddCell(p11hundred, new System.Drawing.Point(4, 1));

            Chunk fifty = new Chunk("Shs.50/-", bFont1);
            Cell p11fifty = new Cell(fifty);
            p11fifty.HorizontalAlignment = Cell.ALIGN_RIGHT;
            p11taxInfoTable.AddCell(p11fifty, new System.Drawing.Point(5, 1));

            Chunk twenty = new Chunk("Shs.20/-", bFont1);
            Cell p11twenty = new Cell(twenty);
            p11twenty.HorizontalAlignment = Cell.ALIGN_RIGHT;
            p11taxInfoTable.AddCell(p11twenty, new System.Drawing.Point(6, 1));

            Chunk ten = new Chunk("Shs.10/-", bFont1);
            Cell p11ten = new Cell(ten);
            p11ten.HorizontalAlignment = Cell.ALIGN_RIGHT;
            p11taxInfoTable.AddCell(p11ten, new System.Drawing.Point(7, 1));

            Chunk fivefive = new Chunk("Shs.5/-", bFont1);
            Cell p11fivefive = new Cell(fivefive);
            p11fivefive.HorizontalAlignment = Cell.ALIGN_RIGHT;
            p11taxInfoTable.AddCell(p11fivefive, new System.Drawing.Point(8, 1));

            Chunk othercoins = new Chunk("OTHER  COINS", thFont2);
            Cell p11othercoins = new Cell(othercoins);
            p11othercoins.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11othercoins, new System.Drawing.Point(9, 1));

            Chunk chequeno = new Chunk("CHEQUE NO.", thFont2);
            Cell p11chequeno = new Cell(chequeno);
            p11chequeno.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11chequeno.Rowspan = 2;
            p11taxInfoTable.AddCell(p11chequeno, new System.Drawing.Point(10, 1));

            //third column           
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.ALIGN_CENTER;

            Chunk thousand3 = new Chunk("", bFont1);
            Cell p11thousand3 = new Cell(thousand3);
            p11thousand3.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11thousand3, new System.Drawing.Point(1, 2));

            Chunk five3 = new Chunk("", bFont1);
            Cell p11five3 = new Cell(five3);
            p11five3.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11five3, new System.Drawing.Point(2, 2));

            Chunk two3 = new Chunk("", bFont1);
            Cell p11two3 = new Cell(two3);
            p11two3.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11two3, new System.Drawing.Point(3, 2));

            Chunk hundred3 = new Chunk("", bFont1);
            Cell p11hundred3 = new Cell(hundred3);
            p11hundred3.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11hundred3, new System.Drawing.Point(4, 2));

            Chunk fifty3 = new Chunk("", bFont1);
            Cell p11fifty3 = new Cell(fifty3);
            p11fifty3.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11fifty3, new System.Drawing.Point(5, 2));

            Chunk twenty3 = new Chunk("", bFont1);
            Cell p11twenty3 = new Cell(twenty3);
            p11twenty3.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11twenty3, new System.Drawing.Point(6, 2));


            Cell p11ten3 = new Cell(new Phrase("", bFont1));
            p11ten3.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11ten3, new System.Drawing.Point(7, 2));


            Cell p11othercoins33 = new Cell(new Phrase("", bFont1));
            p11othercoins33.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11othercoins33, new System.Drawing.Point(8, 2));


            Cell p11othercoins3 = new Cell(new Phrase("", bFont1));
            p11othercoins3.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11othercoins3, new System.Drawing.Point(9, 2));


            Cell p11chequeno3 = new Cell(new Phrase("", bFont1));
            p11chequeno3.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11chequeno3, new System.Drawing.Point(10, 2));


            Cell p11chequeno33 = new Cell(new Phrase("", bFont1));
            p11chequeno33.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11chequeno33, new System.Drawing.Point(11, 2));

            Cell rowtwelve = new Cell(new Phrase("", bFont1));
            rowtwelve.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(rowtwelve, new System.Drawing.Point(12, 2));

            //fourth column         
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.ALIGN_CENTER;

            Chunk thousand4 = new Chunk("", bFont1);
            Cell p11thousand4 = new Cell(thousand4);
            p11thousand4.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11thousand4, new System.Drawing.Point(1, 3));

            Chunk five4 = new Chunk("", bFont1);
            Cell p11five4 = new Cell(five4);
            p11five4.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11five4, new System.Drawing.Point(2, 3));

            Chunk two4 = new Chunk("", bFont1);
            Cell p11two4 = new Cell(two4);
            p11two4.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11two4, new System.Drawing.Point(3, 3));

            Chunk hundred4 = new Chunk("", bFont1);
            Cell p11hundred4 = new Cell(hundred4);
            p11hundred4.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11hundred4, new System.Drawing.Point(4, 3));

            Chunk fifty4 = new Chunk("", bFont1);
            Cell p11fifty4 = new Cell(fifty4);
            p11fifty4.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11fifty4, new System.Drawing.Point(5, 3));

            Chunk twenty4 = new Chunk("", bFont1);
            Cell p11twenty4 = new Cell(twenty4);
            p11twenty4.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11twenty4, new System.Drawing.Point(6, 3));

            Chunk ten4 = new Chunk("", bFont1);
            Cell p11ten4 = new Cell(ten4);
            p11ten4.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11ten4, new System.Drawing.Point(7, 3));

            Chunk othercoins44 = new Chunk("", bFont1);
            Cell p11othercoins44 = new Cell(othercoins44);
            p11othercoins44.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11othercoins44, new System.Drawing.Point(8, 3));

            Chunk othercoins4 = new Chunk("", bFont1);
            Cell p11othercoins4 = new Cell(othercoins4);
            p11othercoins4.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11othercoins4, new System.Drawing.Point(9, 3));

            Chunk chequeno4 = new Chunk("", bFont1);
            Cell p11chequeno4 = new Cell(chequeno4);
            p11chequeno4.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11chequeno4, new System.Drawing.Point(10, 3));

            Chunk chequeno44 = new Chunk("", bFont1);
            Cell p11chequeno44 = new Cell(chequeno44);
            p11chequeno44.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11chequeno44, new System.Drawing.Point(11, 3));

            Cell colfourrowtwelve = new Cell(new Phrase("", bFont1));
            colfourrowtwelve.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(colfourrowtwelve, new System.Drawing.Point(12, 3));


            //fifth  column        
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.ALIGN_CENTER;

            Chunk thousand5 = new Chunk("", bFont1);
            Cell p11thousand5 = new Cell(thousand5);
            p11thousand5.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11thousand5, new System.Drawing.Point(1, 4));

            Chunk five5 = new Chunk("", bFont1);
            Cell p11five5 = new Cell(five5);
            p11five5.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11five5, new System.Drawing.Point(2, 4));

            Chunk two5 = new Chunk("", bFont1);
            Cell p11two5 = new Cell(two5);
            p11two5.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11two5, new System.Drawing.Point(3, 4));

            Chunk hundred5 = new Chunk("", bFont1);
            Cell p11hundred5 = new Cell(hundred5);
            p11hundred5.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11hundred5, new System.Drawing.Point(4, 4));

            Chunk fifty5 = new Chunk("", bFont1);
            Cell p11fifty5 = new Cell(fifty5);
            p11fifty5.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11fifty5, new System.Drawing.Point(5, 4));

            Chunk twenty5 = new Chunk("", bFont1);
            Cell p11twenty5 = new Cell(twenty5);
            p11twenty5.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11twenty5, new System.Drawing.Point(6, 4));

            Chunk ten5 = new Chunk("", bFont1);
            Cell p11ten5 = new Cell(ten5);
            p11ten5.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11ten5, new System.Drawing.Point(7, 4));

            Chunk othercoins55 = new Chunk("", bFont1);
            Cell p11othercoins55 = new Cell(othercoins55);
            p11othercoins55.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11othercoins55, new System.Drawing.Point(8, 4));

            Chunk othercoins5 = new Chunk("", bFont1);
            Cell p11othercoins5 = new Cell(othercoins5);
            p11othercoins5.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11othercoins5, new System.Drawing.Point(9, 4));

            Chunk chequeno5 = new Chunk("", bFont1);
            Cell p11chequeno5 = new Cell(chequeno5);
            p11chequeno5.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11chequeno5, new System.Drawing.Point(10, 4));

            Chunk chequeno55 = new Chunk("", bFont1);
            Cell p11chequeno55 = new Cell(chequeno55);
            p11chequeno55.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11chequeno55, new System.Drawing.Point(11, 4));

            Cell colfiverowtwelve = new Cell(new Phrase("", bFont1));
            colfiverowtwelve.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(colfiverowtwelve, new System.Drawing.Point(12, 4));

            document.Add(p11taxInfoTable);
        }
        //table totals
        private void AddTableTotals()
        {
            Table p11taxInfoTable = new Table(5);
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.ALIGN_CENTER;

            Chunk stamp = new Chunk("", bFont1);
            Cell p11stamp = new Cell(stamp);
            p11stamp.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11stamp, new System.Drawing.Point(1, 0));

            Chunk notescoins = new Chunk("TOTAL  CREDITS", thFont2);
            Cell p11notescoins = new Cell(notescoins);
            p11notescoins.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11notescoins, new System.Drawing.Point(1, 1));

            Chunk payeetaxtotal = new Chunk(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.totalpayee), bFont1);
            Cell p11payeetaxtotal = new Cell(payeetaxtotal);
            p11payeetaxtotal.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11payeetaxtotal, new System.Drawing.Point(1, 2));

            Chunk payeeinterest = new Chunk("", bFont1);
            Cell p11payeeinterest = new Cell(payeeinterest);
            p11payeeinterest.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11payeeinterest, new System.Drawing.Point(1, 3));

            Chunk otherswcps = new Chunk("", bFont1);
            Cell p11otherswcps = new Cell(otherswcps);
            p11otherswcps.HorizontalAlignment = Cell.ALIGN_CENTER;
            p11taxInfoTable.AddCell(p11otherswcps, new System.Drawing.Point(1, 4));

            document.Add(p11taxInfoTable);
        }
        //document footer
        private void AddDocFooter()
        {
            Table p11taxInfoTable = new Table(1);
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.NO_BORDER;


            Chunk officerincharge = new Chunk("To  Officer  in  Charge..................................................  Income Tax  Office", tcFont2);
            Cell p11officerincharge = new Cell(officerincharge);
            p11officerincharge.Border = Cell.NO_BORDER;
            p11officerincharge.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11officerincharge, new System.Drawing.Point(0, 0));

            Chunk paying = new Chunk("(A)  PAYING - IN CERTIFICATE", thFont1);
            Cell p11paying = new Cell(paying);
            p11paying.Border = Cell.NO_BORDER;
            p11paying.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11paying, new System.Drawing.Point(1, 0));

            Chunk empemonuments = new Chunk("We/I certify that the above represents:- ", tcFont2);
            Cell p11empemonuments = new Cell(empemonuments);
            p11empemonuments.Border = Cell.NO_BORDER;
            p11empemonuments.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11empemonuments, new System.Drawing.Point(2, 0));

            Chunk cumulativetotal = new Chunk("The full amount of PAYE tax which was required to be deducted from employee's emoluments for the month shown\n  above and includes any amounts brought forward from the previous months for which the cumulative total was less than\n  100/- .", tcFont2);
            Cell p11cumulativetotals = new Cell(cumulativetotal);
            p11cumulativetotals.Border = Cell.NO_BORDER;
            p11cumulativetotals.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11cumulativetotals, new System.Drawing.Point(3, 0));

            Chunk nilcertificate = new Chunk("(B)  NIL  CERTIFICATE", thFont1);
            Cell p11nilcertificate = new Cell(nilcertificate);
            p11nilcertificate.Border = Cell.NO_BORDER;
            p11nilcertificate.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11nilcertificate, new System.Drawing.Point(4, 0));

            Chunk nil = new Chunk("We/I certify that for the month shown above the amount of PAYE required to be deducted from employees emoluments was Nil,\n or less than Kshs.100/=  which  sum is being carried forward.", tcFont2);
            Cell p11nil = new Cell(nil);
            p11nil.Border = Cell.NO_BORDER;
            p11nil.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11nil, new System.Drawing.Point(5, 0));

            Chunk femployername = new Chunk("EMPLOYER'S  NAME..." + _ViewModel.employername, thFont1);
            Cell p11femployername = new Cell(femployername);
            p11femployername.Border = Cell.NO_BORDER;
            p11femployername.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11femployername, new System.Drawing.Point(6, 0));

            Chunk fpostaladdress = new Chunk("POSTAL ADDRESS..." + _ViewModel.employeraddress + "  " + "TELEPHONE..." + _ViewModel.employertelephone, thFont1);
            Cell p11fpostaladdress = new Cell(fpostaladdress);
            p11fpostaladdress.Border = Cell.NO_BORDER;
            p11fpostaladdress.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11fpostaladdress, new System.Drawing.Point(7, 0));

            Chunk payingofficer = new Chunk("NAME OF PAYING OFFICER...........................", thFont1);
            Cell p11payingofficer = new Cell(payingofficer);
            p11payingofficer.Border = Cell.NO_BORDER;
            p11payingofficer.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11payingofficer, new System.Drawing.Point(8, 0));

            Chunk signature = new Chunk("SIGNATURE........................" + "DATE....................................", thFont1);
            Cell p11signature = new Cell(signature);
            p11signature.Border = Cell.NO_BORDER;
            p11signature.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11signature, new System.Drawing.Point(9, 0));

            document.Add(p11taxInfoTable);

            p11taxInfoTable = new Table(2);
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.NO_BORDER;

            Chunk acceptingbank = new Chunk("NOTE  TO ACCEPTING BANK", new Font(Font.TIMES_ROMAN, 8, Font.BOLD | Font.UNDERLINE));
            Cell p11acceptingbank = new Cell(acceptingbank);
            p11acceptingbank.Border = Cell.NO_BORDER;
            p11acceptingbank.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11acceptingbank, new System.Drawing.Point(0, 0));

            Chunk original = new Chunk("Original - Income Tax Department", tcFont1);
            Cell p11original = new Cell(original);
            p11original.Border = Cell.NO_BORDER;
            p11original.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11original, new System.Drawing.Point(1, 0));

            Chunk duplicate = new Chunk("Duplicate - Remitting Bank", tcFont1);
            Cell p11duplicate = new Cell(duplicate);
            p11duplicate.Border = Cell.NO_BORDER;
            p11duplicate.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11duplicate, new System.Drawing.Point(2, 0));

            Chunk triplicate = new Chunk("Triplicate - To remain in book as employers receipt.", tcFont1);
            Cell p11triplicate = new Cell(triplicate);
            p11triplicate.Border = Cell.NO_BORDER;
            p11triplicate.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11taxInfoTable.AddCell(p11triplicate, new System.Drawing.Point(3, 0));

            document.Add(p11taxInfoTable);

            p11taxInfoTable = new Table(1);
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.NO_BORDER;

            Cell analysis = new Cell(new Phrase("ANALYSIS OF TOTAL CREDITS AND LIABLE EMPLOYEES", new Font(Font.TIMES_ROMAN, 7, Font.BOLD | Font.UNDERLINE)));
            analysis.HorizontalAlignment = Cell.ALIGN_LEFT;
            analysis.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(analysis, new System.Drawing.Point(0, 0));

            Cell completed = new Cell(new Phrase("To be completed by Employer", tcFont2));
            completed.HorizontalAlignment = Cell.ALIGN_LEFT;
            completed.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(completed, new System.Drawing.Point(1, 0));

            document.Add(p11taxInfoTable);

            Table p11totaltaxInfoTable = new Table(2);
            p11totaltaxInfoTable.WidthPercentage = 100;

            Chunk totalpayeeamt = new Chunk("TOTAL PAYE/INT/PEN. PAID " + _ViewModel.totalpayee.ToString("C2"), tcFont1);
            Cell p11totalpayeeamt = new Cell(totalpayeeamt);
            p11totalpayeeamt.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11totaltaxInfoTable.AddCell(p11totalpayeeamt, new System.Drawing.Point(0, 0));


            Chunk payeeaudit = new Chunk("PAYE AUDIT TAX Kshs...................", tcFont1);
            Cell p11payeeaudit = new Cell(payeeaudit);
            p11payeeaudit.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11totaltaxInfoTable.AddCell(p11payeeaudit, new System.Drawing.Point(0, 1));

            Chunk totalwcps = new Chunk("TOTAL W C P S  Kshs......................", tcFont1);
            Cell p11totalwcps = new Cell(totalwcps);
            p11totalwcps.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11totaltaxInfoTable.AddCell(p11totalwcps, new System.Drawing.Point(1, 0));

            Chunk totalcredit = new Chunk("TOTAL CREDIT  Kshs......................", tcFont1);
            Cell p11totalcredit = new Cell(totalcredit);
            p11totalcredit.HorizontalAlignment = Cell.ALIGN_LEFT;
            p11totaltaxInfoTable.AddCell(p11totalcredit, new System.Drawing.Point(1, 1));

            document.Add(p11totaltaxInfoTable);

            p11taxInfoTable = new Table(1);
            p11taxInfoTable.WidthPercentage = 100;
            p11taxInfoTable.Border = Table.NO_BORDER;

            Cell liable = new Cell(new Phrase("Liable  Employees are employees from whose monthly emoluments the total tax paid is deducted.", tcFont2));
            liable.HorizontalAlignment = Cell.ALIGN_LEFT;
            liable.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(liable, new System.Drawing.Point(0, 0));

            Cell reporttype = new Cell(new Phrase("P11", tcFont1));
            reporttype.HorizontalAlignment = Cell.ALIGN_LEFT;
            reporttype.Border = Cell.NO_BORDER;
            p11taxInfoTable.AddCell(reporttype, new System.Drawing.Point(1, 0));

            document.Add(p11taxInfoTable);
        }




















    }
}