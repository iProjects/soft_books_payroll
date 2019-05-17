using System; 
using System.Collections.Generic;
using System.Globalization; 
using System.IO;
using System.Linq;
using System.Text;
using BLL.DataEntry;
//Payroll
using BLL.KRA; 
using CommonLib;
using DAL;  
//--- Add the following to make itext work
using iTextSharp.text; 
using iTextSharp.text.pdf;
using VVX;
using winSBPayroll.ViewModel;

namespace winSBPayroll.Reports.PDF
{
    public class NSSFPDFBuilder
    {
        public NSSFReportModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        Employer _employer;

        //DEFINED fONTS
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 10, Font.NORMAL);//body 
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); //table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 9, Font.BOLD);//TABLE HEADER
        Font helv8Font = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 6, Font.NORMAL);
        Font rms8Normal = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 6, Font.BOLD);
        Font rms8Bold = new Font(Font.TIMES_ROMAN, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);

        //constructor
        public NSSFPDFBuilder(Employer employer, NSSFReportModel nssfreportmodel, string FileName, string Conn)
        {
            if (nssfreportmodel == null)
                throw new ArgumentNullException("NSSFReportModel is null");
            _ViewModel = nssfreportmodel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            sFilePDF = FileName;

            _employer = employer;
        }
        public NSSFPDFBuilder( NSSFReportModel nssfreportmodel, string FileName, string Conn)
        {
            if (nssfreportmodel == null)
                throw new ArgumentNullException("NSSFReportModel is null");
            _ViewModel = nssfreportmodel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

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
            Table nssfTable = new Table(5);
            nssfTable.WidthPercentage = 100;
            nssfTable.Padding = 1;
            nssfTable.Spacing = 1;
            nssfTable.Border = Table.NO_BORDER;

            Cell EmployerNameCell = new Cell(new Phrase(_ViewModel.EmployerName.ToUpper() + "\n", new Font(Font.TIMES_ROMAN, 15, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            EmployerNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            EmployerNameCell.Colspan = 5;
            EmployerNameCell.Border = Cell.NO_BORDER;
            nssfTable.AddCell(EmployerNameCell);

            Cell employerAdressCell = new Cell(new Phrase(_ViewModel.EmpAddress, new Font(Font.TIMES_ROMAN, 9, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employerAdressCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employerAdressCell.Colspan = 5;
            employerAdressCell.Border = Cell.NO_BORDER;
            nssfTable.AddCell(employerAdressCell);

            Cell bCell = new Cell(new Phrase("NSSF", hFont1));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Cell.NO_BORDER;
            nssfTable.AddCell(bCell);

            Cell reportNameCell = new Cell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Cell.NO_BORDER;
            nssfTable.AddCell(reportNameCell);

            Cell employerNSSFCell = new Cell(new Phrase("NSSF No: " + _ViewModel.EmployerCode.Trim(), hFont2));
            employerNSSFCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            employerNSSFCell.Border = Cell.NO_BORDER;
            employerNSSFCell.Colspan = 5;
            nssfTable.AddCell(employerNSSFCell);

            Cell PrintedonCell = new Cell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Cell.NO_BORDER;
            nssfTable.AddCell(PrintedonCell);

            //create the logo
            PDFGen pdfgen = new PDFGen();
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;
            Cell logoCell = new Cell(img0);
            logoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            logoCell.Border = Cell.NO_BORDER;
            logoCell.Add(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            nssfTable.AddCell(logoCell);

            document.Add(nssfTable);
        }
        //document body
        private void AddDocBody()
        {
            Table nssfInfoTable = new Table(6);
            nssfInfoTable.Border = Table.RECTANGLE;
            nssfInfoTable.Border = Table.ALIGN_CENTER;
            nssfInfoTable.Padding = 1;
            nssfInfoTable.Spacing = 1;
            nssfInfoTable.WidthPercentage = 100;

            //add table header
            AddBodyTableHeaders(nssfInfoTable);

            //addtable details
            foreach (var n in _ViewModel.PayList)
            {
                AddBodyTableDetails(n, nssfInfoTable);
            }

            //add table totals
            AddBodyTableTotal(nssfInfoTable);

            document.Add(nssfInfoTable);
        }
        //table headers
        private void AddBodyTableHeaders(Table nssfTable)
        {
            Cell employeenumberCell = new Cell(new Phrase("No", tHfont1));
            employeenumberCell.Border = Cell.RECTANGLE;
            employeenumberCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nssfTable.AddCell(employeenumberCell);

            Cell surNameCell = new Cell(new Phrase("Surname", tHfont1));
            surNameCell.Border = Cell.RECTANGLE;
            surNameCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nssfTable.AddCell(surNameCell);

            Cell otherNamesCell = new Cell(new Phrase("Other Names", tHfont1));
            otherNamesCell.Border = Cell.RECTANGLE;
            otherNamesCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nssfTable.AddCell(otherNamesCell);

            Cell idNoCell = new Cell(new Phrase("Id No", tHfont1));
            idNoCell.Border = Cell.RECTANGLE;
            idNoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nssfTable.AddCell(idNoCell);

            Cell nssfNoCell = new Cell(new Phrase("NSSF No", tHfont1));
            nssfNoCell.Border = Cell.RECTANGLE;
            nssfNoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nssfTable.AddCell(nssfNoCell);

            Cell amountCell = new Cell(new Phrase("Amount\n (Inclusive Employer's NSSF)\n Kshs", tHfont1));
            amountCell.Border = Cell.RECTANGLE;
            amountCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            nssfTable.AddCell(amountCell);
        }
        //table details
        private void AddBodyTableDetails(DAL.psuedovwPayrollMaster nssfRec, Table nssfTable)
        {
            Cell A = new Cell(new Phrase(nssfRec.EmpNo, helv8Font));
            A.HorizontalAlignment = Cell.ALIGN_LEFT;
            nssfTable.AddCell(A);//Col 1

            Cell B = new Cell(new Phrase(nssfRec.Surname, helv8Font));
            B.HorizontalAlignment = Cell.ALIGN_LEFT;
            nssfTable.AddCell(B);//Col 2

            Cell C = new Cell(new Phrase(nssfRec.OtherNames, helv8Font));
            C.HorizontalAlignment = Cell.ALIGN_LEFT;
            nssfTable.AddCell(C);//Col 3

            Cell D = new Cell(new Phrase(nssfRec.IDNo, helv8Font));
            D.HorizontalAlignment = Cell.ALIGN_LEFT;
            nssfTable.AddCell(D);//Col 4

            Cell E1 = new Cell(new Phrase(nssfRec.NSSFNo, helv8Font));
            E1.HorizontalAlignment = Cell.ALIGN_LEFT;
            nssfTable.AddCell(E1);//Col 5

            Cell E2 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", nssfRec.NSSF), helv8Font));
            E2.HorizontalAlignment = Cell.ALIGN_RIGHT;
            nssfTable.AddCell(E2);//Col 6

            //switch (rep.SettingLookup("NSSFCOMPUTATIONMETHOD").ToUpper())
            //{
            //    case "OLD":
            //        Cell E2 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", nssfRec.NSSF + nssfRec.EmployerNSSF), helv8Font));
            //        E2.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //        nssfTable.AddCell(E2);//Col 6
            //        break;
            //    case "NEW":
            //        int lowerearninglimit = int.Parse(rep.SettingLookup("NSSFMINLOWEREARNINGLIMIT"));
            //        int upperearninglimit = int.Parse(rep.SettingLookup("NSSFMAXUPPEREARNINGLIMIT"));
            //        decimal _employeecontributionamount = nssfRec.NSSF;
            //        decimal _employercontributionamount = nssfRec.EmployerNSSF;
            //        decimal _employeecontributionpecentage = int.Parse(rep.SettingLookup("NSSFEMPLOYEECONTRIBUTIONPERCENTAGE"));
            //        decimal _employercontributionpercentage = int.Parse(rep.SettingLookup("NSSFEMPLOYERCONTRIBUTIONPERCENTAGE"));
            //        NssfContributionsDTO _NssfContributionsDTO = rep.ComputeNssfContributions(nssfRec.EmpNo);
            //        Cell E3 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _NssfContributionsDTO.TotalPensionContribution), helv8Font));
            //        E3.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //        nssfTable.AddCell(E3);//Col 6
            //        break;
            //}
        }
        //table totals
        private void AddBodyTableTotal(Table nssfTable)
        {
            Cell E1 = new Cell(new Phrase("TOTAL(Inclusive Employer's NSSF)", rms8Bold));
            E1.Colspan = 5;
            E1.HorizontalAlignment = Cell.ALIGN_LEFT;
            nssfTable.AddCell(E1);

            Cell E2 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total), rms8Bold));
            E2.HorizontalAlignment = Cell.ALIGN_RIGHT;
            nssfTable.AddCell(E2);//Col 6 

            //switch (rep.SettingLookup("NSSFCOMPUTATIONMETHOD").ToUpper())
            //{
            //    case "OLD":
            //        Cell E1 = new Cell(new Phrase("TOTAL(Inclusive Employer's NSSF)", rms8Bold));
            //        E1.Colspan = 5;
            //        E1.HorizontalAlignment = Cell.ALIGN_LEFT;
            //        nssfTable.AddCell(E1);

            //        Cell E2 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Total), rms8Bold));
            //        E2.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //        nssfTable.AddCell(E2);//Col 6 
            //        break;
            //    case "NEW":
            //        List<NssfContributionsDTO> _nssfContributions = new List<NssfContributionsDTO>();
            //        var _employeesquery = from ep in rep.GetAllActiveEmployees()
            //                              where ep.IsActive == true
            //                              where ep.IsDeleted == false
            //                              where ep.EmployerId == _employer.Id
            //                              select ep;
            //        List<Employee> _employees = _employeesquery.ToList();
            //        foreach (var emp in _employees)
            //        {
            //            NssfContributionsDTO _nssfcontribution = new NssfContributionsDTO();
            //            _nssfcontribution = rep.ComputeNssfContributions(emp.EmpNo);

            //            if (!_nssfContributions.Any(i => i.EmpNo == _nssfcontribution.EmpNo))
            //            {
            //                _nssfContributions.Add(_nssfcontribution);
            //            }
            //        }
            //        decimal NewNssfTotal = _nssfContributions.Sum(t => t.TotalPensionContribution);

            //        Cell E4 = new Cell(new Phrase("TOTAL(Inclusive Employer's NSSF)", rms8Bold));
            //        E4.Colspan = 5;
            //        E4.HorizontalAlignment = Cell.ALIGN_LEFT;
            //        nssfTable.AddCell(E4);

            //        Cell E3 = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", NewNssfTotal), helv8Font));
            //        E3.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //        nssfTable.AddCell(E3);//Col 6
            //        break;
            //}
        }
        //document footer
        private void AddDocFooter()
        {
            Table nssfTable = new Table(1);
            nssfTable.WidthPercentage = 100;
            nssfTable.Border = Table.NO_BORDER;

            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            nssfTable.AddCell(sgCell);

            document.Add(nssfTable);
        }


























    }
}