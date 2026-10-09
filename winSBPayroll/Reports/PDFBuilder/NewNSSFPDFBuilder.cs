using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using BLL.DataEntry;
using BLL.KRA;
using CommonLib;
using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;
using winSBPayroll.ViewModel;

namespace winSBPayroll.Reports.PDF
{
    public class NewNSSFPDFBuilder
    {
        public NSSFReportModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        Employer _employer;

        // DEFINED FONTS - Using standard Font constructors (without BaseColor or FontFamily)
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD);
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD);
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL);
        Font helv8Font = new Font(Font.HELVETICA, 8, Font.NORMAL);
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);

        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;
        List<NssfContributionsDTO> _nssfcomputations;

        // Constructors
        public NewNSSFPDFBuilder(Employer employer, NSSFReportModel nssfreportmodel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname, List<NssfContributionsDTO> nssfcomputations)
        {
            if (nssfreportmodel == null)
                throw new ArgumentNullException("NSSFReportModel is null");
            _ViewModel = nssfreportmodel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
            sFilePDF = FileName;
            _employer = employer;
            _nssfcomputations = nssfcomputations;

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed NewNSSFPDFBuilder", TAG));
            }
        }

        public NewNSSFPDFBuilder(NSSFReportModel nssfreportmodel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (nssfreportmodel == null)
                throw new ArgumentNullException("NSSFReportModel is null");
            _ViewModel = nssfreportmodel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
            sFilePDF = FileName;

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed NewNSSFPDFBuilder", TAG));
            }
        }

        public string GetPDF()
        {
            BuildPDF();
            return sFilePDF;
        }

        private void BuildPDF()
        {
            try
            {
                // Step 1: Initialize document in A4 Landscape orientation
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                // Step 2: Bind PdfWriter to file stream
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                // Step 3: Open document
                document.Open();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document header...", TAG));

                // Add header
                AddDocHeader();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document body...", TAG));

                // Add body
                AddDocBody();

                if (_notificationmessageEventname != null)
                    _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating document footer...", TAG));

                // Add footer
                AddDocFooter();

                // Close document stream
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

        // Document header using PdfPTable and PdfPCell
        private void AddDocHeader()
        {
            PdfPTable nssfTable = new PdfPTable(5);
            nssfTable.WidthPercentage = 100;

            PdfPCell EmployerNameCell = new PdfPCell(new Phrase(_ViewModel.EmployerName.ToUpper() + "\n", new Font(Font.TIMES_ROMAN, 15, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            EmployerNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            EmployerNameCell.Colspan = 5;
            EmployerNameCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(EmployerNameCell);

            PdfPCell employerAdressCell = new PdfPCell(new Phrase(_ViewModel.EmpAddress, new Font(Font.TIMES_ROMAN, 9, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employerAdressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employerAdressCell.Colspan = 5;
            employerAdressCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(employerAdressCell);

            PdfPCell bCell = new PdfPCell(new Phrase("NSSF", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(bCell);

            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(reportNameCell);

            PdfPCell employerNSSFCell = new PdfPCell(new Phrase("NSSF No: " + _ViewModel.EmployerCode.Trim(), hFont2));
            employerNSSFCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employerNSSFCell.Border = Rectangle.NO_BORDER;
            employerNSSFCell.Colspan = 5;
            nssfTable.AddCell(employerNSSFCell);

            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(PrintedonCell);

            // Create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);

            PdfPCell logoCell = new PdfPCell();
            logoCell.Border = Rectangle.NO_BORDER;
            logoCell.HorizontalAlignment = Element.ALIGN_LEFT;

            if (img0 != null)
            {
                img0.Alignment = Image.ALIGN_MIDDLE;
                logoCell.AddElement(img0);
            }

            logoCell.AddElement(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            nssfTable.AddCell(logoCell);

            document.Add(nssfTable);
        }

        // Document body using PdfPTable and PdfPCell
        private void AddDocBody()
        {
            PdfPTable nssfInfoTable = new PdfPTable(14);
            nssfInfoTable.WidthPercentage = 100;
            nssfInfoTable.SpacingBefore = 10f;

            if (_notificationmessageEventname != null)
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating table header...", TAG));

            // Add table header
            AddBodyTableHeaders(nssfInfoTable);

            // Repeat the header row on top of every page
            nssfInfoTable.HeaderRows = 1;

            if (_notificationmessageEventname != null)
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating table details...", TAG));

            // Add table details
            foreach (var n in _ViewModel._NssfContributions)
            {
                AddBodyTableDetails(n, nssfInfoTable);
            }

            if (_notificationmessageEventname != null)
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("creating table footer...", TAG));

            // Add table totals
            AddBodyTableTotal(nssfInfoTable);

            document.Add(nssfInfoTable);
        }

        // Table headers using PdfPCell
        private void AddBodyTableHeaders(PdfPTable nssfTable)
        {
            string[] headers = new string[]
            {
                "No",
                "Employee Earning",
                "Pensionable Earnings",
                "Tier1 Pensionable Earnings",
                "Tier1 Employee Deduction",
                "Tier1 Employer Contribution",
                "Tier1 Total Contribution",
                "Tier2 Pensionable Earnings",
                "Tier2 Employee Deduction",
                "Tier2 Employer Contribution",
                "Tier2 Total Contribution",
                "Total Employee Contribution",
                "Total Employer Contribution",
                "Total Pension Contribution"
            };

            foreach (string header in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(header, tHfont1));
                cell.Border = Rectangle.BOX;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                cell.Padding = 3f;
                nssfTable.AddCell(cell);
            }
        }

        // Table details using PdfPCell
        private void AddBodyTableDetails(NssfContributionsDTO _nssfcomputation, PdfPTable nssfTable)
        {
            PdfPCell A = new PdfPCell(new Phrase(_nssfcomputation.EmpNo, helv8Font));
            A.HorizontalAlignment = Element.ALIGN_LEFT;
            A.Padding = 2f;
            nssfTable.AddCell(A); // Col 1

            PdfPCell B = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.EmployeeEarnings), helv8Font));
            B.HorizontalAlignment = Element.ALIGN_RIGHT;
            B.Padding = 2f;
            nssfTable.AddCell(B); // Col 2

            PdfPCell C = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.PensionableEarnings), helv8Font));
            C.HorizontalAlignment = Element.ALIGN_RIGHT;
            C.Padding = 2f;
            nssfTable.AddCell(C); // Col 3

            PdfPCell D = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.Tier1PensionableEarnings), helv8Font));
            D.HorizontalAlignment = Element.ALIGN_RIGHT;
            D.Padding = 2f;
            nssfTable.AddCell(D); // Col 4

            PdfPCell E = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.Tier1EmployeeDeduction), helv8Font));
            E.HorizontalAlignment = Element.ALIGN_RIGHT;
            E.Padding = 2f;
            nssfTable.AddCell(E); // Col 5

            PdfPCell F = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.Tier1EmployerContribution), helv8Font));
            F.HorizontalAlignment = Element.ALIGN_RIGHT;
            F.Padding = 2f;
            nssfTable.AddCell(F); // Col 6

            PdfPCell G = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.Tier1TotalContribution), helv8Font));
            G.HorizontalAlignment = Element.ALIGN_RIGHT;
            G.Padding = 2f;
            nssfTable.AddCell(G); // Col 7

            PdfPCell H = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.Tier2PensionableEarnings), helv8Font));
            H.HorizontalAlignment = Element.ALIGN_RIGHT;
            H.Padding = 2f;
            nssfTable.AddCell(H); // Col 8

            PdfPCell I = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.Tier2EmployeeDeduction), helv8Font));
            I.HorizontalAlignment = Element.ALIGN_RIGHT;
            I.Padding = 2f;
            nssfTable.AddCell(I); // Col 9

            PdfPCell J = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.Tier2EmployerContribution), helv8Font));
            J.HorizontalAlignment = Element.ALIGN_RIGHT;
            J.Padding = 2f;
            nssfTable.AddCell(J); // Col 10

            PdfPCell K = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.Tier2TotalContribution), helv8Font));
            K.HorizontalAlignment = Element.ALIGN_RIGHT;
            K.Padding = 2f;
            nssfTable.AddCell(K); // Col 11

            PdfPCell L = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.TotalEmployeeContribution), helv8Font));
            L.HorizontalAlignment = Element.ALIGN_RIGHT;
            L.Padding = 2f;
            nssfTable.AddCell(L); // Col 11

            PdfPCell M = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.TotalEmployerContribution), helv8Font));
            M.HorizontalAlignment = Element.ALIGN_RIGHT;
            M.Padding = 2f;
            nssfTable.AddCell(M); // Col 11

            PdfPCell N = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _nssfcomputation.TotalPensionContribution), helv8Font));
            N.HorizontalAlignment = Element.ALIGN_RIGHT;
            N.Padding = 2f;
            nssfTable.AddCell(N); // Col 12
        }

        // Table totals using PdfPCell
        private void AddBodyTableTotal(PdfPTable nssfTable)
        {
            PdfPCell E1 = new PdfPCell(new Phrase("TOTAL(Inclusive Employer's NSSF)", rms8Bold));
            E1.Colspan = 11;
            E1.HorizontalAlignment = Element.ALIGN_LEFT;
            E1.Padding = 3f;
            nssfTable.AddCell(E1);

            PdfPCell E2 = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.Total), rms6Bold));
            E2.HorizontalAlignment = Element.ALIGN_RIGHT;
            E2.Padding = 3f;
            nssfTable.AddCell(E2); // Col 12
        }

        // Document footer using PdfPTable and PdfPCell
        private void AddDocFooter()
        {
            PdfPTable nssfTable = new PdfPTable(1);
            nssfTable.WidthPercentage = 100;
            nssfTable.SpacingBefore = 20f;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            sgCell.Border = Rectangle.NO_BORDER;
            nssfTable.AddCell(sgCell);

            document.Add(nssfTable);
        }
    }
}
