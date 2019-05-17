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
    public class EmployeesPDFBuilder
    {
        
        Document document;
        string Message;
        string sFilePDF;
        EmployeesModelReport _ViewModel;

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


        public EmployeesPDFBuilder(EmployeesModelReport employeesModel, string FileName)
        {
            if (employeesModel == null)
                throw new ArgumentNullException("EmployeesModelReport is null");
            _ViewModel = employeesModel; 

            sFilePDF = FileName;

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

        private void AddDocHeader()
        {

            Table employeesTable = new Table(5);
            employeesTable.WidthPercentage = 100;
            employeesTable.Padding = 1;
            employeesTable.Spacing = 1;
            employeesTable.Border = Table.NO_BORDER;

            Cell employernameCell = new Cell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Cell.NO_BORDER;
            employeesTable.AddCell(employernameCell);

            Cell employeraddressCell = new Cell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Cell.NO_BORDER;
            employeesTable.AddCell(employeraddressCell);  

            Cell bCell = new Cell(new Phrase("EMPLOYEES REPORT", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            employeesTable.AddCell(bCell);

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            employeesTable.AddCell(reportNameCell);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            employeesTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen();
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            employeesTable.AddCell(logoCell);

            document.Add(employeesTable); 
        }

        private void AddDocBody()
        {

            //Add table headers
            AddTableHeaders();

            //Add table details  
            foreach (var d in _ViewModel.pae)
            {
                AddTableDetails(d);
            }

            //Add table totals
            AddTableTotals();

        }

        //table headers
        private void AddTableHeaders()
        {
            Table employeesTable = new Table(9);
            employeesTable.WidthPercentage = 100;
            employeesTable.Padding = 1;
            employeesTable.Spacing = 1;

            Cell empno = new Cell(new Phrase("No", tHFont));
            empno.Border = Cell.RECTANGLE;
            empno.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(empno);

            Cell empname = new Cell(new Phrase("Name", tHFont));
            empname.Border = Cell.RECTANGLE;
            empname.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(empname);

            Cell empgender = new Cell(new Phrase("Gender", tHFont));
            empgender.Border = Cell.RECTANGLE;
            empgender.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(empgender);

            Cell pinnumber = new Cell(new Phrase("Pin", tHFont));
            pinnumber.Border = Cell.RECTANGLE;
            pinnumber.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(pinnumber);

            Cell idnumber = new Cell(new Phrase("Id \n Number", tHFont));
            idnumber.Border = Cell.RECTANGLE;
            idnumber.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(idnumber);

            Cell department = new Cell(new Phrase("Department", tHFont));
            department.Border = Cell.RECTANGLE;
            department.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(department);

            Cell telephone = new Cell(new Phrase("Date of \n Employment", tHFont));
            telephone.Border = Cell.RECTANGLE;
            telephone.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(telephone);

            Cell basic = new Cell(new Phrase("Basic Salary\nKshs", tHFont));
            basic.Border = Cell.RECTANGLE;
            basic.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(basic);

            Cell paymentmode = new Cell(new Phrase("Payment Mode", tHFont));
            paymentmode.Border = Cell.RECTANGLE;
            paymentmode.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(paymentmode);

            document.Add(employeesTable);
        }

        //table details 
        private void AddTableDetails(printallemployees paemp)
        {
            Table employeesTable = new Table(9);
            employeesTable.WidthPercentage = 100;
            employeesTable.Padding = 1;
            employeesTable.Spacing = 1;

            Cell empno = new Cell(new Phrase(paemp.employeenumber.ToString(), rms8Bold));
            empno.Border = Cell.RECTANGLE;
            empno.HorizontalAlignment = Cell.ALIGN_LEFT;
            employeesTable.AddCell(empno);

            Cell empname = new Cell(new Phrase(paemp.employeename.ToString(), rms8Bold));
            empname.Border = Cell.RECTANGLE;
            empname.HorizontalAlignment = Cell.ALIGN_LEFT;
            employeesTable.AddCell(empname);

            Cell empgender = new Cell(new Phrase(paemp.gender, rms8Bold));
            empgender.Border = Cell.RECTANGLE;
            empgender.HorizontalAlignment = Cell.ALIGN_LEFT;
            employeesTable.AddCell(empgender);

            Cell pinnumber = new Cell(new Phrase(paemp.pinnumber, rms8Bold));
            pinnumber.Border = Cell.RECTANGLE;
            pinnumber.HorizontalAlignment = Cell.ALIGN_LEFT;
            employeesTable.AddCell(pinnumber);

            Cell idnumber = new Cell(new Phrase(paemp.idnumber, rms8Bold));
            idnumber.Border = Cell.RECTANGLE;
            idnumber.HorizontalAlignment = Cell.ALIGN_LEFT;
            employeesTable.AddCell(idnumber);

            Cell department = new Cell(new Phrase(paemp.department, rms8Bold));
            department.Border = Cell.RECTANGLE;
            department.HorizontalAlignment = Cell.ALIGN_LEFT;
            employeesTable.AddCell(department);

            Cell telephone = new Cell(new Phrase(paemp.dateofemployment.ToString("dd-MMM-yyyy"), rms8Bold));
            telephone.Border = Cell.RECTANGLE;
            telephone.HorizontalAlignment = Cell.ALIGN_LEFT;
            employeesTable.AddCell(telephone);

            Cell basic = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", paemp.basicpay), rms8Bold));
            basic.Border = Cell.RECTANGLE;
            basic.HorizontalAlignment = Cell.ALIGN_RIGHT;
            employeesTable.AddCell(basic);

            Cell paymentmode = new Cell(new Phrase(paemp.paymentmode, rms8Bold));
            paymentmode.Border = Cell.RECTANGLE;
            paymentmode.HorizontalAlignment = Cell.ALIGN_CENTER;
            employeesTable.AddCell(paymentmode);

            document.Add(employeesTable);

        }

        //table totals
        private void AddTableTotals()
        {
            Table employeesTable = new Table(9);
            employeesTable.WidthPercentage = 100;
            employeesTable.Padding = 1;
            employeesTable.Spacing = 1;

            Cell total = new Cell(new Phrase("TOTAL", tHfont1));
            total.Border = Cell.RECTANGLE;
            total.HorizontalAlignment = Cell.ALIGN_LEFT;
            total.Colspan = 7;
            employeesTable.AddCell(total);

            Cell totalbasic = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.totalbasic), tHfont1));
            totalbasic.Border = Cell.RECTANGLE;
            totalbasic.HorizontalAlignment = Cell.ALIGN_RIGHT;
            employeesTable.AddCell(totalbasic);

            document.Add(employeesTable);
        }



        //document footer
        private void AddDocFooter()
        {

            Table employeesTable = new Table(1);
            employeesTable.WidthPercentage = 100;
            employeesTable.Border = Table.NO_BORDER; 

            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            employeesTable.AddCell(sgCell); 

            document.Add(employeesTable);

        }



    }
}
