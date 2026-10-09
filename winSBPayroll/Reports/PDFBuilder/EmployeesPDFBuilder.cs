using System;
using System.Globalization;
using System.IO;
using BLL.KRA.Models;
using CommonLib;
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

        // DEFINED FONTS - Using standard Font constructors without BaseColor or FontFamily
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD);
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); // body
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); // body
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD); // body
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); // table Header
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); // table Header
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL); // table cell
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);

        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public EmployeesPDFBuilder(EmployeesModelReport employeesModel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (employeesModel == null)
                throw new ArgumentNullException("EmployeesModelReport is null");
            _ViewModel = employeesModel;

            TAG = this.GetType().Name;
            _notificationmessageEventname = notificationmessageEventname;
            sFilePDF = FileName;

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed EmployeesPDFBuilder", TAG));
            }
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

        /* Build the document */
        private void BuildPF()
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

        private void AddDocHeader()
        {
            PdfPTable employeesTable = new PdfPTable(5);
            employeesTable.WidthPercentage = 100;

            // Employer Name Header
            PdfPCell employernameCell = new PdfPCell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(employernameCell);

            // Employer Address Header
            PdfPCell employeraddressCell = new PdfPCell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(employeraddressCell);

            // Report Title Header
            PdfPCell bCell = new PdfPCell(new Phrase("EMPLOYEES REPORT", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(bCell);

            // Report Subtitle Header
            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(reportNameCell);

            // Printed Date
            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(PrintedonCell);

            // Create Logo & Slogan Column
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
            employeesTable.AddCell(logoCell);

            document.Add(employeesTable);
        }

        // Single PdfPTable setup for recurring headers across pages
        private void AddDocBody()
        {
            //  total columns 26
            PdfPTable employeesTable = new PdfPTable(26);
            employeesTable.WidthPercentage = 100;
            employeesTable.SpacingBefore = 10f;

            // Adjusted column width ratios for 25 columns
            employeesTable.SetWidths(new float[] { 5f, 9f, 6f, 6f, 8f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f, 6f });

            // Header 1..11
            PdfPCell PdfPCell_empno = new PdfPCell(new Phrase("No", tHFont));
            PdfPCell_empno.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell_empno.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCell_empno.Padding = 3f;
            employeesTable.AddCell(PdfPCell_empno);

            PdfPCell PdfPCell_empname = new PdfPCell(new Phrase("Name", tHFont));
            PdfPCell_empname.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell_empname.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCell_empname.Padding = 3f;
            employeesTable.AddCell(PdfPCell_empname);

            PdfPCell PdfPCell_telephoneno = new PdfPCell(new Phrase("Phone \nNumber", tHFont));
            PdfPCell_telephoneno.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell_telephoneno.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCell_telephoneno.Padding = 3f;
            employeesTable.AddCell(PdfPCell_telephoneno);

            PdfPCell PdfPCell_empgender = new PdfPCell(new Phrase("Email", tHFont));
            PdfPCell_empgender.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell_empgender.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCell_empgender.Padding = 3f;
            employeesTable.AddCell(PdfPCell_empgender);

            PdfPCell PdfPCell_pinnumber = new PdfPCell(new Phrase("Pin", tHFont));
            PdfPCell_pinnumber.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell_pinnumber.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCell_pinnumber.Padding = 3f;
            employeesTable.AddCell(PdfPCell_pinnumber);

            PdfPCell PdfPCell_idnumber = new PdfPCell(new Phrase("Id \nNumber", tHFont));
            PdfPCell_idnumber.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell_idnumber.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCell_idnumber.Padding = 3f;
            employeesTable.AddCell(PdfPCell_idnumber);

            PdfPCell PdfPCell_department = new PdfPCell(new Phrase("Department", tHFont));
            PdfPCell_department.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCell_department.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCell_department.Padding = 3f;
            employeesTable.AddCell(PdfPCell_department);

            PdfPCell PdfPCellbasic_pay = new PdfPCell(new Phrase("Basic \nPay", tHFont));
            PdfPCellbasic_pay.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellbasic_pay.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellbasic_pay.Padding = 3f;
            employeesTable.AddCell(PdfPCellbasic_pay);

            PdfPCell PdfPCellTotal_Non_Cash_Benefits = new PdfPCell(new Phrase("Non \nCash \nBenefits", tHFont));
            PdfPCellTotal_Non_Cash_Benefits.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellTotal_Non_Cash_Benefits.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellTotal_Non_Cash_Benefits.Padding = 3f;
            employeesTable.AddCell(PdfPCellTotal_Non_Cash_Benefits);

            PdfPCell PdfPCellTotal_Other_Payments = new PdfPCell(new Phrase("Other \nPayments", tHFont));
            PdfPCellTotal_Other_Payments.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellTotal_Other_Payments.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellTotal_Other_Payments.Padding = 3f;
            employeesTable.AddCell(PdfPCellTotal_Other_Payments);

            PdfPCell PdfPCellgross_taxable_pay = new PdfPCell(new Phrase("Gross \nTaxable \nPay", tHFont));
            PdfPCellgross_taxable_pay.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellgross_taxable_pay.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellgross_taxable_pay.Padding = 3f;
            employeesTable.AddCell(PdfPCellgross_taxable_pay);

            PdfPCell PdfPCellstatutory_deductions = new PdfPCell(new Phrase("Statutory \nDeductions", tHFont));
            PdfPCellstatutory_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellstatutory_deductions.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellstatutory_deductions.Padding = 3f;
            employeesTable.AddCell(PdfPCellstatutory_deductions);

            PdfPCell PdfPCellgross_taxable_pay_minus_statutory = new PdfPCell(new Phrase("Gross \nTaxable \nPay \nMinus \nStatutory", tHFont));
            PdfPCellgross_taxable_pay_minus_statutory.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellgross_taxable_pay_minus_statutory.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellgross_taxable_pay_minus_statutory.Padding = 3f;
            employeesTable.AddCell(PdfPCellgross_taxable_pay_minus_statutory);

            PdfPCell PdfPCellgrosstax = new PdfPCell(new Phrase("Gross \nTax", tHFont));
            PdfPCellgrosstax.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellgrosstax.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellgrosstax.Padding = 3f;
            employeesTable.AddCell(PdfPCellgrosstax);

            PdfPCell PdfPCelltotalrelief = new PdfPCell(new Phrase("Total \nRelief", tHFont));
            PdfPCelltotalrelief.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCelltotalrelief.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCelltotalrelief.Padding = 3f;
            employeesTable.AddCell(PdfPCelltotalrelief);

            PdfPCell PdfPCellpaye = new PdfPCell(new Phrase("Payee", tHFont));
            PdfPCellpaye.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellpaye.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellpaye.Padding = 3f;
            employeesTable.AddCell(PdfPCellpaye);

            PdfPCell PdfPCellnet_pay_after_tax = new PdfPCell(new Phrase("Gross \nPay \nAfter \nTax", tHFont));
            PdfPCellnet_pay_after_tax.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellnet_pay_after_tax.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellnet_pay_after_tax.Padding = 3f;
            employeesTable.AddCell(PdfPCellnet_pay_after_tax);

            PdfPCell PdfPCellloan_deductions = new PdfPCell(new Phrase("Loan \nDeductions", tHFont));
            PdfPCellloan_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellloan_deductions.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellloan_deductions.Padding = 3f;
            employeesTable.AddCell(PdfPCellloan_deductions);

            PdfPCell PdfPCellsacco_deductions = new PdfPCell(new Phrase("Sacco \nDeductions", tHFont));
            PdfPCellsacco_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellsacco_deductions.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellsacco_deductions.Padding = 3f;
            employeesTable.AddCell(PdfPCellsacco_deductions);

            PdfPCell PdfPCellremaining_deductions = new PdfPCell(new Phrase("Remaining \nDeductions", tHFont));
            PdfPCellremaining_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellremaining_deductions.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellremaining_deductions.Padding = 3f;
            employeesTable.AddCell(PdfPCellremaining_deductions);

            PdfPCell PdfPCellvoluntary_deductions = new PdfPCell(new Phrase("Voluntary \nDeductions", tHFont));
            PdfPCellvoluntary_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellvoluntary_deductions.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellvoluntary_deductions.Padding = 3f;
            employeesTable.AddCell(PdfPCellvoluntary_deductions);

            PdfPCell PdfPCelltotal_deductions = new PdfPCell(new Phrase("Total \nDeductions", tHFont));
            PdfPCelltotal_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCelltotal_deductions.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCelltotal_deductions.Padding = 3f;
            employeesTable.AddCell(PdfPCelltotal_deductions);

            PdfPCell PdfPCellNon_Taxable_Cash_Allowances = new PdfPCell(new Phrase("Non \nTaxable \nCash \nAllowances", tHFont));
            PdfPCellNon_Taxable_Cash_Allowances.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellNon_Taxable_Cash_Allowances.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellNon_Taxable_Cash_Allowances.Padding = 3f;
            employeesTable.AddCell(PdfPCellNon_Taxable_Cash_Allowances);

            PdfPCell PdfPCellnet_pay_after_every_other_deductions = new PdfPCell(new Phrase("Net \nPay \nAfter \nDeductions", tHFont));
            PdfPCellnet_pay_after_every_other_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellnet_pay_after_every_other_deductions.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellnet_pay_after_every_other_deductions.Padding = 3f;
            employeesTable.AddCell(PdfPCellnet_pay_after_every_other_deductions);

            PdfPCell PdfPCellnet_pay_take_home = new PdfPCell(new Phrase("Net \nPay \nTake \nHome", tHFont));
            PdfPCellnet_pay_take_home.HorizontalAlignment = Element.ALIGN_CENTER;
            PdfPCellnet_pay_take_home.VerticalAlignment = Element.ALIGN_MIDDLE;
            PdfPCellnet_pay_take_home.Padding = 3f;
            employeesTable.AddCell(PdfPCellnet_pay_take_home);

            // Header 26: 2/3 Rule Status
            PdfPCell ruleCellHeader = new PdfPCell(new Phrase("2/3 \nRule \n Status", tHFont));
            ruleCellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            ruleCellHeader.VerticalAlignment = Element.ALIGN_MIDDLE;
            ruleCellHeader.Padding = 3f;
            employeesTable.AddCell(ruleCellHeader);

            // Repeat the header row on top of every page
            employeesTable.HeaderRows = 1;

            // Accumulator variables for column totals
            decimal totalBasicPay = 0m;
            decimal totalNonCashBenefits = 0m;
            decimal totalOtherPayments = 0m;
            decimal totalGrossTaxablePay = 0m;
            decimal totalStatutoryDeductions = 0m;
            decimal totalGrossTaxablePayMinusStatutory = 0m;
            decimal totalGrossTax = 0m;
            decimal totalRelief = 0m;
            decimal totalPayee = 0m;
            decimal totalNetPayAfterTax = 0m;
            decimal totalLoanDeductions = 0m;
            decimal totalSaccoDeductions = 0m;
            decimal totalRemainingDeductions = 0m;
            decimal totalVoluntaryDeductions = 0m;
            decimal totalDeductions = 0m;
            decimal totalNonTaxableEarnings = 0m;
            decimal totalNetPayAfterEveryOtherDeductions = 0m;
            decimal totalNetPayTakeHome = 0m;

            // Table Details
            foreach (var _employee in _ViewModel._lst_employees)
            {
                // Accumulate running sums
                totalBasicPay += _employee.basic_pay;
                totalNonCashBenefits += _employee.Total_Non_Cash_Benefits;
                totalOtherPayments += _employee.total_all_other_payments;
                totalGrossTaxablePay += _employee.gross_taxable_pay;
                totalStatutoryDeductions += _employee.statutory_deductions;
                totalGrossTaxablePayMinusStatutory += _employee.gross_taxable_pay_minus_statutory;
                totalGrossTax += _employee.gross_tax;
                totalRelief += _employee.Total_Relief;
                totalPayee += _employee.payee;
                totalNetPayAfterTax += _employee.net_pay_after_tax;
                totalLoanDeductions += _employee.loan_deductions;
                totalSaccoDeductions += _employee.sacco_deductions;
                totalRemainingDeductions += _employee.remaining_deductions;
                totalVoluntaryDeductions += _employee.voluntary_deductions;
                totalDeductions += _employee.total_deductions;
                totalNonTaxableEarnings += _employee.Non_Taxable_Earnings;
                totalNetPayAfterEveryOtherDeductions += _employee.net_pay_after_every_other_deductions;
                totalNetPayTakeHome += _employee.net_pay_take_home;

                PdfPCell employeenumber = new PdfPCell(new Phrase(_employee.employeenumber.ToString(), tcFont));
                employeenumber.HorizontalAlignment = Element.ALIGN_CENTER;
                employeenumber.Padding = 2f;
                employeesTable.AddCell(employeenumber);

                PdfPCell employeename = new PdfPCell(new Phrase(_employee.employeename.ToString(), tcFont));
                employeename.HorizontalAlignment = Element.ALIGN_LEFT;
                employeename.Padding = 2f;
                employeesTable.AddCell(employeename);

                PdfPCell PhoneNo = new PdfPCell(new Phrase(_employee.PhoneNo, tcFont));
                PhoneNo.HorizontalAlignment = Element.ALIGN_CENTER;
                PhoneNo.Padding = 2f;
                employeesTable.AddCell(PhoneNo);

                PdfPCell email = new PdfPCell(new Phrase(_employee.email, tcFont));
                email.HorizontalAlignment = Element.ALIGN_CENTER;
                email.Padding = 2f;
                employeesTable.AddCell(email);

                PdfPCell pinnumber = new PdfPCell(new Phrase(_employee.pinnumber, tcFont));
                pinnumber.HorizontalAlignment = Element.ALIGN_CENTER;
                pinnumber.Padding = 2f;
                employeesTable.AddCell(pinnumber);

                PdfPCell idnumber = new PdfPCell(new Phrase(_employee.idnumber, tcFont));
                idnumber.HorizontalAlignment = Element.ALIGN_CENTER;
                idnumber.Padding = 2f;
                employeesTable.AddCell(idnumber);

                PdfPCell department = new PdfPCell(new Phrase(_employee.department, tcFont));
                department.HorizontalAlignment = Element.ALIGN_LEFT;
                department.Padding = 2f;
                employeesTable.AddCell(department);

                PdfPCell basic_pay = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.basic_pay), tcFont));
                basic_pay.HorizontalAlignment = Element.ALIGN_CENTER;
                basic_pay.Padding = 2f;
                employeesTable.AddCell(basic_pay);

                PdfPCell Total_Non_Cash_Benefits = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.Total_Non_Cash_Benefits), tcFont));
                Total_Non_Cash_Benefits.HorizontalAlignment = Element.ALIGN_CENTER;
                Total_Non_Cash_Benefits.Padding = 2f;
                employeesTable.AddCell(Total_Non_Cash_Benefits);

                PdfPCell total_all_other_payments = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.total_all_other_payments), tcFont));
                total_all_other_payments.HorizontalAlignment = Element.ALIGN_CENTER;
                total_all_other_payments.Padding = 2f;
                employeesTable.AddCell(total_all_other_payments);

                PdfPCell gross_taxable_pay = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.gross_taxable_pay), tcFont));
                gross_taxable_pay.HorizontalAlignment = Element.ALIGN_CENTER;
                gross_taxable_pay.Padding = 2f;
                employeesTable.AddCell(gross_taxable_pay);

                PdfPCell statutory_deductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.statutory_deductions), tcFont));
                statutory_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
                statutory_deductions.Padding = 2f;
                employeesTable.AddCell(statutory_deductions);

                PdfPCell gross_taxable_pay_minus_statutory = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.gross_taxable_pay_minus_statutory), tcFont));
                gross_taxable_pay_minus_statutory.HorizontalAlignment = Element.ALIGN_CENTER;
                gross_taxable_pay_minus_statutory.Padding = 2f;
                employeesTable.AddCell(gross_taxable_pay_minus_statutory);

                PdfPCell gross_tax = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.gross_tax), tcFont));
                gross_tax.HorizontalAlignment = Element.ALIGN_CENTER;
                gross_tax.Padding = 2f;
                employeesTable.AddCell(gross_tax);

                PdfPCell Total_Relief = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.Total_Relief), tcFont));
                Total_Relief.HorizontalAlignment = Element.ALIGN_CENTER;
                Total_Relief.Padding = 2f;
                employeesTable.AddCell(Total_Relief);

                PdfPCell paye = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.payee), tcFont));
                paye.HorizontalAlignment = Element.ALIGN_CENTER;
                paye.Padding = 2f;
                employeesTable.AddCell(paye);

                PdfPCell net_pay_after_tax = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.net_pay_after_tax), tcFont));
                net_pay_after_tax.HorizontalAlignment = Element.ALIGN_CENTER;
                net_pay_after_tax.Padding = 2f;
                employeesTable.AddCell(net_pay_after_tax);

                PdfPCell cellNhif = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.loan_deductions), tcFont));
                cellNhif.HorizontalAlignment = Element.ALIGN_CENTER;
                cellNhif.Padding = 2f;
                employeesTable.AddCell(cellNhif);

                PdfPCell sacco_deductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.sacco_deductions), tcFont));
                sacco_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
                sacco_deductions.Padding = 2f;
                employeesTable.AddCell(sacco_deductions);

                PdfPCell remaining_deductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.remaining_deductions), tcFont));
                remaining_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
                remaining_deductions.Padding = 2f;
                employeesTable.AddCell(remaining_deductions);

                PdfPCell voluntary_deductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.voluntary_deductions), tcFont));
                voluntary_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
                voluntary_deductions.Padding = 2f;
                employeesTable.AddCell(voluntary_deductions);

                PdfPCell total_deductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.total_deductions), tcFont));
                total_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
                total_deductions.Padding = 2f;
                employeesTable.AddCell(total_deductions);

                PdfPCell Non_Taxable_Cash_Allowances = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.Non_Taxable_Earnings), tcFont));
                Non_Taxable_Cash_Allowances.HorizontalAlignment = Element.ALIGN_CENTER;
                Non_Taxable_Cash_Allowances.Padding = 2f;
                employeesTable.AddCell(Non_Taxable_Cash_Allowances);

                PdfPCell net_pay_after_every_other_deductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.net_pay_after_every_other_deductions), tcFont));
                net_pay_after_every_other_deductions.HorizontalAlignment = Element.ALIGN_CENTER;
                net_pay_after_every_other_deductions.Padding = 2f;
                employeesTable.AddCell(net_pay_after_every_other_deductions);

                PdfPCell net_pay_take_home = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _employee.net_pay_take_home), tcFont));
                net_pay_take_home.HorizontalAlignment = Element.ALIGN_CENTER;
                net_pay_take_home.Padding = 2f;
                employeesTable.AddCell(net_pay_take_home);

                // 2/3 Rule Value Cell (Highlights NON-COMPLIANT status)
                Font ruleFont = _employee.PassTwoThirdsRule
                    ? tcFont
                    : new Font(Font.HELVETICA, 8, Font.BOLD, Color.RED);

                PdfPCell TwoThirdsRuleStatus = new PdfPCell(new Phrase(_employee.TwoThirdsRuleStatus, ruleFont));
                TwoThirdsRuleStatus.HorizontalAlignment = Element.ALIGN_CENTER;
                TwoThirdsRuleStatus.Padding = 2f;
                employeesTable.AddCell(TwoThirdsRuleStatus);
            }

            // Append Totals Row
            PdfPCell totalLabelCell = new PdfPCell(new Phrase("TOTALS", tHFont));
            totalLabelCell.Colspan = 7;
            totalLabelCell.HorizontalAlignment = Element.ALIGN_CENTER;
            totalLabelCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            totalLabelCell.Padding = 3f;
            employeesTable.AddCell(totalLabelCell);

            PdfPCell cellTotalBasicPay = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalBasicPay), tHFont));
            cellTotalBasicPay.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalBasicPay.Padding = 3f;
            employeesTable.AddCell(cellTotalBasicPay);

            PdfPCell cellTotalNonCashBenefits = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalNonCashBenefits), tHFont));
            cellTotalNonCashBenefits.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalNonCashBenefits.Padding = 3f;
            employeesTable.AddCell(cellTotalNonCashBenefits);

            PdfPCell cellTotalOtherPayments = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalOtherPayments), tHFont));
            cellTotalOtherPayments.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalOtherPayments.Padding = 3f;
            employeesTable.AddCell(cellTotalOtherPayments);

            PdfPCell cellTotalGrossTaxablePay = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalGrossTaxablePay), tHFont));
            cellTotalGrossTaxablePay.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalGrossTaxablePay.Padding = 3f;
            employeesTable.AddCell(cellTotalGrossTaxablePay);

            PdfPCell cellTotalStatutoryDeductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalStatutoryDeductions), tHFont));
            cellTotalStatutoryDeductions.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalStatutoryDeductions.Padding = 3f;
            employeesTable.AddCell(cellTotalStatutoryDeductions);

            PdfPCell cellTotalGrossTaxablePayMinusStatutory = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalGrossTaxablePayMinusStatutory), tHFont));
            cellTotalGrossTaxablePayMinusStatutory.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalGrossTaxablePayMinusStatutory.Padding = 3f;
            employeesTable.AddCell(cellTotalGrossTaxablePayMinusStatutory);

            PdfPCell cellTotalGrossTax = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalGrossTax), tHFont));
            cellTotalGrossTax.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalGrossTax.Padding = 3f;
            employeesTable.AddCell(cellTotalGrossTax);

            PdfPCell cellTotalRelief = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalRelief), tHFont));
            cellTotalRelief.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalRelief.Padding = 3f;
            employeesTable.AddCell(cellTotalRelief);

            PdfPCell cellTotalPayee = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalPayee), tHFont));
            cellTotalPayee.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalPayee.Padding = 3f;
            employeesTable.AddCell(cellTotalPayee);

            PdfPCell cellTotalNetPayAfterTax = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalNetPayAfterTax), tHFont));
            cellTotalNetPayAfterTax.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalNetPayAfterTax.Padding = 3f;
            employeesTable.AddCell(cellTotalNetPayAfterTax);

            PdfPCell cellTotalLoanDeductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalLoanDeductions), tHFont));
            cellTotalLoanDeductions.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalLoanDeductions.Padding = 3f;
            employeesTable.AddCell(cellTotalLoanDeductions);

            PdfPCell cellTotalSaccoDeductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalSaccoDeductions), tHFont));
            cellTotalSaccoDeductions.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalSaccoDeductions.Padding = 3f;
            employeesTable.AddCell(cellTotalSaccoDeductions);

            PdfPCell cellTotalRemainingDeductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalRemainingDeductions), tHFont));
            cellTotalRemainingDeductions.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalRemainingDeductions.Padding = 3f;
            employeesTable.AddCell(cellTotalRemainingDeductions);

            PdfPCell cellTotalVoluntaryDeductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalVoluntaryDeductions), tHFont));
            cellTotalVoluntaryDeductions.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalVoluntaryDeductions.Padding = 3f;
            employeesTable.AddCell(cellTotalVoluntaryDeductions);

            PdfPCell cellTotalDeductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalDeductions), tHFont));
            cellTotalDeductions.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalDeductions.Padding = 3f;
            employeesTable.AddCell(cellTotalDeductions);

            PdfPCell cellTotalNonTaxableEarnings = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalNonTaxableEarnings), tHFont));
            cellTotalNonTaxableEarnings.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalNonTaxableEarnings.Padding = 3f;
            employeesTable.AddCell(cellTotalNonTaxableEarnings);

            PdfPCell cellTotalNetPayAfterEveryOtherDeductions = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalNetPayAfterEveryOtherDeductions), tHFont));
            cellTotalNetPayAfterEveryOtherDeductions.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalNetPayAfterEveryOtherDeductions.Padding = 3f;
            employeesTable.AddCell(cellTotalNetPayAfterEveryOtherDeductions);

            PdfPCell cellTotalNetPayTakeHome = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", totalNetPayTakeHome), tHFont));
            cellTotalNetPayTakeHome.HorizontalAlignment = Element.ALIGN_CENTER;
            cellTotalNetPayTakeHome.Padding = 3f;
            employeesTable.AddCell(cellTotalNetPayTakeHome);

            PdfPCell cellEmptyRuleStatus = new PdfPCell(new Phrase("", tHFont));
            cellEmptyRuleStatus.HorizontalAlignment = Element.ALIGN_CENTER;
            cellEmptyRuleStatus.Padding = 3f;
            employeesTable.AddCell(cellEmptyRuleStatus);

            document.Add(employeesTable);
        }


        // Document footer
        private void AddDocFooter()
        {
            PdfPTable employeesTable = new PdfPTable(1);
            employeesTable.WidthPercentage = 100;
            employeesTable.SpacingBefore = 20f;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            sgCell.Border = Rectangle.NO_BORDER;
            employeesTable.AddCell(sgCell);

            document.Add(employeesTable);
        }
    }
}
