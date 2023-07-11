using System;
using System.IO;
using System.Linq;
using BLL;
using BLL.DataEntry;
using BLL.KRA;
using CommonLib;
using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDF
{
    public class PayrollMasterPDFBuilder
    {
        //private attributes 
        PayrollMasterModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        //define fonts
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); //table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font tcFont1 = new Font(Font.HELVETICA, 6, Font.NORMAL);//table cell
        Font helv8Font = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell 
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms8Normal = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        //constructor
        public PayrollMasterPDFBuilder(PayrollMasterModel payrollMasterModel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (payrollMasterModel == null)
                throw new ArgumentNullException("PayrollMasterModel is null");
            _ViewModel = payrollMasterModel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _notificationmessageEventname = notificationmessageEventname;

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
                // step 1: creation of a document-object
                document = new Document(PageSize.A4.Rotate());

                // step 2: we create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open document
                document.Open();

                //Add  Header 
                AddDocHeader();

                //Add body
                AddDocBody();

                //Add footer
                AddFooter();

                //Add doc footer
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


        /*Build the document now**/
        private void AddDocHeader()
        {
            Table payrollMasterTable = new Table(5);
            payrollMasterTable.WidthPercentage = 100;
            payrollMasterTable.Padding = 1;
            payrollMasterTable.Spacing = 1;
            payrollMasterTable.Border = Table.NO_BORDER;

            Cell employernameCell = new Cell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Cell.NO_BORDER;
            payrollMasterTable.AddCell(employernameCell);

            Cell employeraddressCell = new Cell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Cell.NO_BORDER;
            payrollMasterTable.AddCell(employeraddressCell);

            Cell bCell = new Cell(new Phrase("MASTER PAYROLL", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            payrollMasterTable.AddCell(bCell);

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            payrollMasterTable.AddCell(reportNameCell);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            payrollMasterTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            payrollMasterTable.AddCell(logoCell);

            document.Add(payrollMasterTable);

        }

        private void AddDocBody()
        {
            //Create table
            Table payrollMasterTable = new Table(12);
            payrollMasterTable.Padding = 1;
            payrollMasterTable.Spacing = 1;

            payrollMasterTable.WidthPercentage = 100;

            //Put table headers
            BodyAddtableHeaders(payrollMasterTable);

            //Put table detail
            foreach (var d in _ViewModel.paymaster)
            {
                BodAddTableDetail(d, payrollMasterTable);
            }

            //put table footer
            AddDocBodyTableTotals(payrollMasterTable);

            //Addtable to document
            document.Add(payrollMasterTable);
        }


        private void BodyAddtableHeaders(Table payrollMasterTable)
        {
            //row 1
            Cell c1 = new Cell(new Phrase("No", tHFont));
            c1.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c1);

            Cell c2 = new Cell(new Phrase("Name", tHFont));
            c2.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c2);

            Cell c3 = new Cell(new Phrase("Bank", tHFont));
            c3.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c3);

            Cell c4 = new Cell(new Phrase("Department", tHFont));
            c4.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c4);

            Cell c5 = new Cell(new Phrase("Basic Pay\nKshs", tHFont));
            c5.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c5);

            Cell c7 = new Cell(new Phrase("Gross Pay\nKshs", tHFont));
            c7.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c7);

            Cell c8 = new Cell(new Phrase("PAYE \n(Net Tax)\nKshs", tHFont));
            c8.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c8);

            Cell c9 = new Cell(new Phrase("NSSF\nKshs", tHFont));
            c9.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c9);

            Cell c10 = new Cell(new Phrase("NHIF\nKshs", tHFont));
            c10.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c10);

            Cell c11 = new Cell(new Phrase("Other \nDeductions\nKshs", tHFont));
            c11.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c11);

            Cell c12 = new Cell(new Phrase("Total \nDeductions\nKshs", tHFont));
            c12.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c12);

            Cell c13 = new Cell(new Phrase("Net Pay\nKshs", tHFont));
            c13.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c13);


        }

        private void BodAddTableDetail(DAL.psuedovwPayrollMaster rec, Table payrollMasterTable)
        {

            Cell enoCell = new Cell(new Phrase(rec.EmpNo, helv8Font));
            enoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(enoCell);

            Cell enameCell = new Cell(new Phrase(rec.Surname + ",  " + rec.OtherNames, helv8Font));
            enameCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(enameCell);

            Cell c1Cell = new Cell(new Phrase(rec.BankName + " - " + rec.BranchName, helv8Font));
            c1Cell.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(c1Cell);

            Cell c2Cell = new Cell(new Phrase(rec.Department, helv8Font));
            c2Cell.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(c2Cell);

            Cell pCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.BasicPay), helv8Font));
            pCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(pCell);


            Cell t1Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.GrossTaxableEarnings), helv8Font));
            t1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(t1Cell);

            Cell d1Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.PayeTax), helv8Font));
            d1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d1Cell);

            Cell d2Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.NSSF), helv8Font));
            d2Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d2Cell);

            Cell d3Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.NHIF), helv8Font));
            d3Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d3Cell);

            Cell d4Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.OtherDeductions), helv8Font));
            d4Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d4Cell);

            decimal tDeductions = rec.PayeTax + rec.NHIF + rec.NSSF + rec.OtherDeductions;
            Cell d5Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", tDeductions), helv8Font));
            d5Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d5Cell);

            Cell d6Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.NetPay), helv8Font));
            d6Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d6Cell);
        }

        private void AddDocBodyTableTotals(Table payrollMasterTable)
        {
            Cell enoCell = new Cell(new Phrase("TOTAL", tHFont));
            enoCell.Colspan = 4;
            enoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(enoCell);

            Cell pCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalBasicPay), tHFont));
            pCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(pCell);

            decimal gp = _ViewModel.paymaster.Sum(a => a.GrossTaxableEarnings);
            Cell tCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", gp), tHFont));
            tCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(tCell);

            Cell d1Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PAYE), tHFont));
            d1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d1Cell);

            Cell d2Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NSSF), tHFont));
            d2Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d2Cell);

            Cell d3Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NHIF), tHFont));
            d3Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d3Cell);

            Cell d4Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NHIF), tHFont));
            d4Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d4Cell);

            decimal tDeductions = _ViewModel.paymaster.Sum(rec => rec.PayeTax + rec.NHIF + rec.NSSF + rec.OtherDeductions);
            Cell d5Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", tDeductions), tHFont));
            d5Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d5Cell);

            Cell d6Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Salaries), tHFont));
            d6Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d6Cell);
        }

        private void AddFooter()
        {

            Table payrollMasterTable = new Table(2);
            payrollMasterTable.WidthPercentage = 50;
            payrollMasterTable.Padding = 2;
            payrollMasterTable.Spacing = 2;
            payrollMasterTable.Alignment = Table.ALIGN_LEFT;
            payrollMasterTable.Border = Table.NO_BORDER;


            Cell netsalarypayable = new Cell(new Phrase("Please write the following cheques:", rms6Bold));
            netsalarypayable.Border = Cell.NO_BORDER;
            netsalarypayable.HorizontalAlignment = Cell.ALIGN_LEFT;
            netsalarypayable.Colspan = 2;
            payrollMasterTable.AddCell(netsalarypayable);


            Cell netsalaryverified = new Cell(new Phrase("Salaries:", rms8Normal));
            netsalaryverified.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(netsalaryverified);

            Cell netsalaryverified1 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Salaries.ToString("#,##0")), rms8Normal));
            netsalaryverified1.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(netsalaryverified1);


            Cell netsalaryauthorized = new Cell(new Phrase("N.S.S.F(Employer Inclusive):       ", rms8Normal));
            netsalaryauthorized.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(netsalaryauthorized);

            Cell netsalaryauthorized1 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.deductionNSSF.ToString("#,##0")), rms8Normal));
            netsalaryauthorized1.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(netsalaryauthorized1);


            Cell sgCell = new Cell(new Phrase("N.H.I.F :", rms8Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(sgCell);

            Cell sgCell1 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NHIF.ToString("#,##0")), rms8Normal));
            sgCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(sgCell1);


            Cell pgCell = new Cell(new Phrase("Paymaster General:", rms8Normal));
            pgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(pgCell);

            Cell pgCell1 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PAYE.ToString("#,##0")), rms8Normal));
            pgCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(pgCell1);

            document.Add(payrollMasterTable);

        }

        //document footer
        private void AddDocFooter()
        {

            Table payrollMasterTable = new Table(1);
            payrollMasterTable.WidthPercentage = 100;
            payrollMasterTable.Border = Table.NO_BORDER;

            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            payrollMasterTable.AddCell(sgCell);

            document.Add(payrollMasterTable);

        }



    }
}
