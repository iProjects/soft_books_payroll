using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using BLL;
using BLL.DataEntry;
using CommonLib;
using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using winSBPayroll.ViewModel;

namespace winSBPayroll.Reports.PDF
{
    public class PayslipMakerAll3
    {
        List<Payslip> payslipList;
        Document document;
        string Message;
        string sFilePDF;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        #region "fonts"
        Font employerNameFont;
        Font documentHeadersFont;
        Font bodyHeaderFont;
        Font bodyHeaderDataFont;
        Font deductionsFont;
        Font tableHeaderCellFont;
        Font tableDataCellFont;
        Font signatureFont;
        #endregion "fonts"

        public PayslipMakerAll3(List<Payslip> PayslipList, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            if (PayslipList == null)
                throw new ArgumentNullException("Payslip List is null");
            payslipList = PayslipList;

            TAG = this.GetType().Name;

            _notificationmessageEventname = notificationmessageEventname;
            sFilePDF = FileName;

            InitializeFonts();

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed PayslipMakerAll3", TAG));
            }
        }

        #region "fonts"
        public void InitializeFonts()
        {
            try
            {
                employerNameFont = new Font(int.Parse(rep.SettingLookup("payslipEmployerNameFontFamily")), float.Parse(rep.SettingLookup("payslipEmployerNameFontSize")), Font.BOLD);
                documentHeadersFont = new Font(int.Parse(rep.SettingLookup("payslipDocumentHeadersFontFamily")), float.Parse(rep.SettingLookup("payslipDocumentHeadersFontSize")), Font.BOLD);
                bodyHeaderFont = new Font(int.Parse(rep.SettingLookup("payslipBodyHeaderFontFamily")), float.Parse(rep.SettingLookup("payslipBodyHeaderFontSize")), Font.NORMAL);
                bodyHeaderDataFont = new Font(int.Parse(rep.SettingLookup("payslipBodyHeaderDataFontFamily")), float.Parse(rep.SettingLookup("payslipBodyHeaderDataFontSize")), Font.NORMAL);
                deductionsFont = new Font(int.Parse(rep.SettingLookup("payslipDeductionsFontFamily")), float.Parse(rep.SettingLookup("payslipDeductionsFontSize")), Font.BOLD);
                tableHeaderCellFont = new Font(int.Parse(rep.SettingLookup("payslipTableHeaderCellFontFamily")), float.Parse(rep.SettingLookup("payslipTableHeaderCellFontSize")), Font.BOLD);
                tableDataCellFont = new Font(int.Parse(rep.SettingLookup("payslipTableDataCellFontFamily")), float.Parse(rep.SettingLookup("payslipTableDataCellFontSize")), Font.NORMAL);
                signatureFont = new Font(int.Parse(rep.SettingLookup("payslipSignatureFontFamily")), float.Parse(rep.SettingLookup("payslipSignatureFontSize")), Font.NORMAL);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        #endregion "fonts"

        public string GetPDF()
        {
            BuildPDF();
            return sFilePDF;
        }

        private void BuildPDF()
        {
            try
            {
                document = new Document(PageSize.A4, 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));

                document.Open();

                // Calculate available dimensions for columns
                float pageWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
                float pageHeight = document.PageSize.Height - document.TopMargin - document.BottomMargin;
                float halfWidth = pageWidth / 2f;

                // Define Left Column [0] and Right Column [1]
                float[][] columns = new float[][] 
                {
                    new float[] { document.LeftMargin, document.BottomMargin, document.LeftMargin + halfWidth - 5f, document.BottomMargin + pageHeight },
                    new float[] { document.LeftMargin + halfWidth + 5f, document.BottomMargin, document.LeftMargin + pageWidth, document.BottomMargin + pageHeight }
                };

                IEnumerable<Tuple<Payslip, Payslip>> PairedPayslips = payslipList.AsPairs();

                foreach (var pair in PairedPayslips)
                {
                    List<Payslip> currentPairList = new List<Payslip>();
                    if (pair.Item1 != null) currentPairList.Add(pair.Item1);
                    if (pair.Item2 != null) currentPairList.Add(pair.Item2);

                    // Maintain single ColumnText instance for the current page
                    ColumnText ct = new ColumnText(writer.DirectContent);
                    int currentColumn = 0;
                    ct.SetSimpleColumn(columns[currentColumn][0], columns[currentColumn][1], columns[currentColumn][2], columns[currentColumn][3]);

                    foreach (Payslip item in currentPairList)
                    {
                        VikePayslipViewModel vm = new VikePayslipViewModel(item);

                        PdfPTable outerPayslipTable = new PdfPTable(1);
                        outerPayslipTable.WidthPercentage = 100f;
                        outerPayslipTable.SplitRows = true;
                        outerPayslipTable.SplitLate = false;

                        AddHeader(outerPayslipTable, vm);
                        AddPayments(outerPayslipTable, vm);
                        AddPAYEDetails(outerPayslipTable, vm);
                        AddDeductions(outerPayslipTable, vm);
                        AddSummary(outerPayslipTable, vm);
                        AddNon_Taxable_Earnings_Only(outerPayslipTable, vm);
                        AddOtherPayments(outerPayslipTable, vm);
                        AddNonCashPayments(outerPayslipTable, vm);
                        AddOtherDeductions(outerPayslipTable, vm);
                        AddEmployeeLoans(outerPayslipTable, vm);
                        AddSACCOContributions(outerPayslipTable, vm);

                        // Add elements into the column stream without resetting position
                        ct.AddElement(outerPayslipTable);

                        while (ColumnText.HasMoreText(ct.Go()))
                        {
                            currentColumn++;
                            if (currentColumn >= columns.Length)
                            {
                                currentColumn = 0;
                                document.NewPage();
                            }
                            ct.SetSimpleColumn(columns[currentColumn][0], columns[currentColumn][1], columns[currentColumn][2], columns[currentColumn][3]);
                        }

                        // If processing the first payslip in a pair, force movement to Right Column for the next payslip
                        if (currentColumn == 0 && currentPairList.Count > 1)
                        {
                            currentColumn = 1;
                            ct.SetSimpleColumn(columns[currentColumn][0], columns[currentColumn][1], columns[currentColumn][2], columns[currentColumn][3]);
                        }
                    }

                    document.NewPage();
                }

                document.Close();
            }
            catch (DocumentException deEx)
            {
                this.Message = deEx.Message;
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


        private void Notify(string message)
        {
            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs(message, TAG));
            }
        }

        #region "Payslip Table Builders"
        private void AddHeader(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(2);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase(_ViewModel.EmployerName.ToUpper().Trim() + "    " + _ViewModel.EmployerAddress.Trim() + "\n", employerNameFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 2;
            innerTable.AddCell(aCell);

            PdfPCell bCell = new PdfPCell(new Phrase("PAYSLIP", new Font(Font.TIMES_ROMAN, 9, Font.BOLD, Color.BLACK)));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 2;
            innerTable.AddCell(bCell);

            AddPairToTable(innerTable, "Payroll Period:", _ViewModel.PayrollMonth + " / " + _ViewModel.PayrollYear.ToString());
            AddPairToTable(innerTable, "Printed On:", _ViewModel.PrintedOn.ToString("dd-MMM-yyyy"));
            AddPairToTable(innerTable, "Employee No:", _ViewModel.EmpNo);
            AddPairToTable(innerTable, "Employee :", _ViewModel.EmpName);
            AddPairToTable(innerTable, "Pin No:", _ViewModel.PINNo);
            AddPairToTable(innerTable, "NSSF No:", _ViewModel.NSSFNO);
            AddPairToTable(innerTable, "ID No:", _ViewModel.IDNo);
            AddPairToTable(innerTable, "Department:", _ViewModel.Department);

            _payslipTable.AddCell(innerTable);
        }

        private void AddPayments(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(2);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase("PAYMENTS\n", documentHeadersFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 2;
            innerTable.AddCell(aCell);

            foreach (var op in _ViewModel.ExcludeOtherPayments)
            {
                AddRowToTable(innerTable, op.Description, string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.Amount), tableDataCellFont);
            }

            AddRowToTable(innerTable, "Total Other Payments", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalOtherPayments), tableDataCellFont);

            AddRowToTable(innerTable, "GROSS PAY", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.GrossTaxableEarnings), tableHeaderCellFont);

            _payslipTable.AddCell(innerTable);
        }

        private void AddPAYEDetails(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(2);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase("PAYE DETAILS\n", documentHeadersFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 2;
            innerTable.AddCell(aCell);

            AddRowToTable(innerTable, "GROSS TAXABLE PAY MINUS STATUTORIES", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.PayeDetail.TaxablePay), tableHeaderCellFont);
            AddRowToTable(innerTable, "GROSS TAX", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.PayeDetail.GrossTax), tableHeaderCellFont);
            AddRowToTable(innerTable, "TAX DUE", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.PayeDetail.TaxDue), tableHeaderCellFont);
            AddRowToTable(innerTable, "Personal Relief", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.PayeDetail.PersonalRelief), tableDataCellFont);
            AddRowToTable(innerTable, "Life Insurance Relief", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.PayeDetail.InsuranceRelief), tableDataCellFont);
            AddRowToTable(innerTable, "Mortgage Relief", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.PayeDetail.MortgageRelief), tableDataCellFont);

            AddRowToTable(innerTable, "GROSS PAY AFTER TAX", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.GROSS_PAY_AFTER_TAX), tableHeaderCellFont);

            _payslipTable.AddCell(innerTable);
        }

        private void AddDeductions(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(2);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase("DEDUCTIONS\n", documentHeadersFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 2;
            innerTable.AddCell(aCell);

            foreach (var op in _ViewModel.statutory_deductions_exclude_payee)
            {
                if (op.Description.Equals("NHIF"))
                {
                    continue;
                }
                if (op.Description.Equals("PAYE"))
                {
                    continue;
                }
                AddRowToTable(innerTable, op.Description, string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.Amount), tableDataCellFont);
            }

            AddRowToTable(innerTable, "Total Statutory Deductions", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.total_statutory_deductions_exclude_payee), deductionsFont);

            foreach (var op in _ViewModel.ExcludeOtherDeductions)
            {
                if (op.Description.Equals("PAYE"))
                {
                    AddRowToTable(innerTable, op.Description, string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.Amount), tableDataCellFont);
                }
            }

            AddRowToTable(innerTable, "Total Loans", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalAmountLoansAmount), tableDataCellFont);
            AddRowToTable(innerTable, "Total Welfare Contributions", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalAmountSaccoAmount), tableDataCellFont);
            AddRowToTable(innerTable, "Total Other Deductions", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalOtherDeductions), tableDataCellFont);
            AddRowToTable(innerTable, "TOTAL VOLUNTARY DEDUCTIONS", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TOTAL_VOLUNTARY_DEDUCTIONS), tableHeaderCellFont);
            AddRowToTable(innerTable, "NET PAY AFTER DEDUCTIONS", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.NET_PAY_AFTER_DEDUCTIONS), tableHeaderCellFont);

            _payslipTable.AddCell(innerTable);
        }

        private void AddSummary(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(2);
            innerTable.WidthPercentage = 100;

            AddRowToTable(innerTable, " Non Taxable Allowances", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TOTAL_NON_TAXABLE_EARNINGS_ONLY), tableDataCellFont);
            AddRowToTable(innerTable, "NET PAY TAKE HOME", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.NET_PAY_TAKE_HOME), tableHeaderCellFont);

            _payslipTable.AddCell(innerTable);
        }

        private void AddOtherPayments(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(2);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase("Other Payments\n", documentHeadersFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 2;
            innerTable.AddCell(aCell);

            PayslipOtherPaymentsDeductionsHeaders(innerTable);

            foreach (var op in _ViewModel.OtherPayments)
            {
                AddRowToTable(innerTable, op.Description, string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.Amount), tableDataCellFont);
            }

            AddRowToTable(innerTable, "Totals", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalOtherPayments), deductionsFont);

            _payslipTable.AddCell(innerTable);
        }

        private void AddNon_Taxable_Earnings_Only(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(2);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase("Non Taxable Earnings\n", documentHeadersFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 2;
            innerTable.AddCell(aCell);

            PayslipOtherPaymentsDeductionsHeaders(innerTable);

            foreach (var op in _ViewModel.Non_Taxable_Earnings_Only)
            {
                AddRowToTable(innerTable, op.Description, string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.Amount), tableDataCellFont);
            }

            AddRowToTable(innerTable, "Totals", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TOTAL_NON_TAXABLE_EARNINGS_ONLY), deductionsFont);

            _payslipTable.AddCell(innerTable);
        }

        private void AddNonCashPayments(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(2);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase("Non Cash Payments\n", documentHeadersFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 2;
            innerTable.AddCell(aCell);

            PayslipOtherPaymentsDeductionsHeaders(innerTable);

            foreach (var op in _ViewModel.NonCashPayments)
            {
                AddRowToTable(innerTable, op.Description, string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.Amount), tableDataCellFont);
            }

            AddRowToTable(innerTable, "Totals", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalNonCashPayments), deductionsFont);

            _payslipTable.AddCell(innerTable);
        }

        private void AddOtherDeductions(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(2);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase("Other Deductions\n", documentHeadersFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 2;
            innerTable.AddCell(aCell);

            PayslipOtherPaymentsDeductionsHeaders(innerTable);

            foreach (var op in _ViewModel.OtherDeductions)
            {
                AddRowToTable(innerTable, op.Description, string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.Amount), tableDataCellFont);
            }

            AddRowToTable(innerTable, "Totals", string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalOtherDeductions), deductionsFont);

            _payslipTable.AddCell(innerTable);
        }

        private void AddEmployeeLoans(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(3);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase("Employee LOANS\n", documentHeadersFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 3;
            innerTable.AddCell(aCell);

            PayslipLoanDetailsHeader(innerTable);

            foreach (var op in _ViewModel.Loans)
            {
                innerTable.AddCell(CreateCell(op.Description, tableDataCellFont, Element.ALIGN_LEFT));
                innerTable.AddCell(CreateCell(string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.Amount), tableDataCellFont, Element.ALIGN_RIGHT));

                decimal _tbalance = Math.Abs(decimal.Parse(op.YTD.ToString()));
                innerTable.AddCell(CreateCell(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _tbalance), tableDataCellFont, Element.ALIGN_RIGHT));
            }

            innerTable.AddCell(CreateCell("Totals", deductionsFont, Element.ALIGN_LEFT));
            innerTable.AddCell(CreateCell(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalAmountLoansAmount), tableDataCellFont, Element.ALIGN_RIGHT));

            decimal _balance = Math.Abs(decimal.Parse(_ViewModel.TotalAmountLoansBalance.ToString()));
            innerTable.AddCell(CreateCell(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _balance), tableDataCellFont, Element.ALIGN_RIGHT));

            _payslipTable.AddCell(innerTable);
        }

        private void AddSACCOContributions(PdfPTable _payslipTable, VikePayslipViewModel _ViewModel)
        {
            if (_ViewModel == null) return;

            PdfPTable innerTable = new PdfPTable(3);
            innerTable.WidthPercentage = 100;

            PdfPCell aCell = new PdfPCell(new Phrase("Welfare Contributions\n", documentHeadersFont));
            aCell.HorizontalAlignment = Element.ALIGN_CENTER;
            aCell.Colspan = 3;
            innerTable.AddCell(aCell);

            PayslipSaccoDetailsHeader(innerTable);

            foreach (var op in _ViewModel.SACCOContributions)
            {
                innerTable.AddCell(CreateCell(op.Description, tableDataCellFont, Element.ALIGN_LEFT));
                innerTable.AddCell(CreateCell(string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.Amount), tableDataCellFont, Element.ALIGN_RIGHT));
                innerTable.AddCell(CreateCell(string.Format(CultureInfo.InvariantCulture, "{0:N2}", op.YTD), tableDataCellFont, Element.ALIGN_RIGHT));
            }

            innerTable.AddCell(CreateCell("Totals", deductionsFont, Element.ALIGN_LEFT));
            innerTable.AddCell(CreateCell(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalAmountSaccoAmount), tableDataCellFont, Element.ALIGN_RIGHT));
            innerTable.AddCell(CreateCell(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalAmountSaccoBalance), tableDataCellFont, Element.ALIGN_RIGHT));

            _payslipTable.AddCell(innerTable);
        }
        #endregion

        #region "Helpers"
        private PdfPCell CreateCell(string text, Font font, int alignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.HorizontalAlignment = alignment;
            return cell;
        }

        private void AddPairToTable(PdfPTable table, string label, string value)
        {
            table.AddCell(CreateCell(label, bodyHeaderFont, Element.ALIGN_LEFT));
            table.AddCell(CreateCell(value, bodyHeaderDataFont, Element.ALIGN_LEFT));
        }

        private void AddRowToTable(PdfPTable table, string col1, string col2, Font font)
        {
            table.AddCell(CreateCell(col1, font, Element.ALIGN_LEFT));
            table.AddCell(CreateCell(col2, font, Element.ALIGN_RIGHT));
        }

        private void PayslipOtherPaymentsDeductionsHeaders(PdfPTable aTable)
        {
            string[] th = new string[] { "Description", "Amount" };
            foreach (string e in th)
            {
                aTable.AddCell(CreateCell(e, deductionsFont, Element.ALIGN_CENTER));
            }
        }

        private void PayslipLoanDetailsHeader(PdfPTable aTable)
        {
            string[] th = new string[] { "Description", "Amount", "Balance" };
            foreach (string e in th)
            {
                aTable.AddCell(CreateCell(e, deductionsFont, Element.ALIGN_CENTER));
            }
        }

        private void PayslipSaccoDetailsHeader(PdfPTable aTable)
        {
            string[] th = new string[] { "Description", "Amount", "Total Shares" };
            foreach (string e in th)
            {
                aTable.AddCell(CreateCell(e, deductionsFont, Element.ALIGN_CENTER));
            }
        }
        #endregion
    }








}
