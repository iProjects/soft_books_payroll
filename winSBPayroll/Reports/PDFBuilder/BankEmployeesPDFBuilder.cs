using System;
using System.IO;
//Payroll
using BLL.KRA.Models;
using CommonLib;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDFBuilder
{
    public class BankEmployeesPDFBuilder
    {
        Document document;
        string Message;
        string sFilePDF;
        BankEmployeesModelReport _ViewModel;

        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); //table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public BankEmployeesPDFBuilder(BankEmployeesModelReport employeesModel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (employeesModel == null)
                throw new ArgumentNullException("BankEmployeesModelReport is null");
            _ViewModel = employeesModel;

            TAG = this.GetType().Name;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed BankEmployeesPDFBuilder", TAG));
        }

        public string GetEmployeePDF()
        {
            try
            {
                BuildPF();
                return sFilePDF;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        /*Build the document **/
        private void BuildPF()
        {
            try
            {
                //step 1 creation of the document
                // Step 1: Initialize document in A4 Landscape orientation
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                // step 2: we create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open the document
                document.Open();

                //add header
                AddDocHeader();

                //add body (combines table headers, row details, and totals into a single repeated-header table)
                AddDocBody();

                //add footer
                AddDocFooter();

                //close the document
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

        private void AddDocHeader()
        {
            PdfPTable employeesTable = new PdfPTable(5);
            employeesTable.WidthPercentage = 100;

            PdfPCell employernameCell = new PdfPCell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(employernameCell);

            PdfPCell employeraddressCell = new PdfPCell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(employeraddressCell);

            PdfPCell bCell = new PdfPCell(new Phrase("BANK EMPLOYEES REPORT", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(bCell);

            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(reportNameCell);

            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            PdfPCell logoCell = new PdfPCell();
            logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
            logoCell.Border = Rectangle.NO_BORDER;
            logoCell.AddElement(img0);
            logoCell.AddElement(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            employeesTable.AddCell(logoCell);

            document.Add(employeesTable);
        }

        private void AddDocBody()
        {
            PdfPTable mainTable = new PdfPTable(10);
            mainTable.WidthPercentage = 100;

            // Mark the first row (headers) to repeat on every page
            mainTable.HeaderRows = 1;

            // 1. Add Header Row
            AddTableHeaders(mainTable);

            // 2. Add Detail Rows
            foreach (var d in _ViewModel.pbe)
            {
                AddTableDetails(mainTable, d);
            }

            // 3. Add Totals Row
            AddTableTotals(mainTable);

            document.Add(mainTable);
        }

        private void AddTableHeaders(PdfPTable table)
        {
            string[] headers = new string[] 
            { 
                "No", "Name", "Gender", "Pin", "Id \n Number", 
                "Department", "Date of \n Employment", "Basic Salary\nKshs", "Account Name", "Account No" 
            };

            foreach (string header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, tHFont));
                cell.Border = Rectangle.BOX;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Padding = 3;
                table.AddCell(cell);
            }
        }

        private void AddTableDetails(PdfPTable table, print_bank_employees paemp)
        {
            PdfPCell empno = new PdfPCell(new Phrase(paemp.employeenumber.ToString(), tcFont));
            empno.Border = Rectangle.BOX;
            empno.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(empno);

            PdfPCell empname = new PdfPCell(new Phrase(paemp.employeename.ToString(), tcFont));
            empname.Border = Rectangle.BOX;
            empname.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(empname);

            PdfPCell empgender = new PdfPCell(new Phrase(paemp.gender, tcFont));
            empgender.Border = Rectangle.BOX;
            empgender.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(empgender);

            PdfPCell pinnumber = new PdfPCell(new Phrase(paemp.pinnumber, tcFont));
            pinnumber.Border = Rectangle.BOX;
            pinnumber.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(pinnumber);

            PdfPCell idnumber = new PdfPCell(new Phrase(paemp.idnumber, tcFont));
            idnumber.Border = Rectangle.BOX;
            idnumber.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(idnumber);

            PdfPCell department = new PdfPCell(new Phrase(paemp.department, tcFont));
            department.Border = Rectangle.BOX;
            department.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(department);

            PdfPCell telephone = new PdfPCell(new Phrase(paemp.dateofemployment.ToString("dd-MMM-yyyy"), tcFont));
            telephone.Border = Rectangle.BOX;
            telephone.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(telephone);

            PdfPCell basic = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", paemp.basicpay), tcFont));
            basic.Border = Rectangle.BOX;
            basic.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(basic);

            PdfPCell paymentmode = new PdfPCell(new Phrase(paemp.AccountName, tcFont));
            paymentmode.Border = Rectangle.BOX;
            paymentmode.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(paymentmode);

            PdfPCell accountno = new PdfPCell(new Phrase(paemp.AccountNo, tcFont));
            accountno.Border = Rectangle.BOX;
            accountno.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(accountno);
        }

        private void AddTableTotals(PdfPTable table)
        {
            PdfPCell total = new PdfPCell(new Phrase("TOTAL", tHfont1));
            total.Border = Rectangle.BOX;
            total.HorizontalAlignment = Element.ALIGN_CENTER;
            total.Colspan = 7;
            table.AddCell(total);

            PdfPCell totalbasic = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.totalbasic), tHfont1));
            totalbasic.Border = Rectangle.BOX;
            totalbasic.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(totalbasic);

            // Empty cells to complete the 10-column row
            PdfPCell emptyCell = new PdfPCell(new Phrase(""));
            emptyCell.Border = Rectangle.BOX;
            emptyCell.Colspan = 2;
            table.AddCell(emptyCell);
        }

        private void AddDocFooter()
        {
            PdfPTable employeesTable = new PdfPTable(1);
            employeesTable.WidthPercentage = 100;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            sgCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(sgCell);

            document.Add(employeesTable);
        }
    }
}
