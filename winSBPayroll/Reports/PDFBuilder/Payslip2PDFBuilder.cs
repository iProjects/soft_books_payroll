using System;
using System.Collections.Generic;
using System.IO;
using CommonLib;
//Payroll
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDF
{
    public class Payslip2PDFBuilder
    {
        Payslip _ViewModel;
        Document document;
        string Message;
        string sFilePDF;

        ////DEFINED FONTS
        Font hFont1 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL);//body 
        Font tHFont = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); //table Header
        Font tHFont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 6, Font.NORMAL);//table cell
        Font tcFont2 = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font tcFont3 = new Font(Font.HELVETICA, 8, Font.NORMAL);//table cell
        Font tcFont4 = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);//table cell
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public Payslip2PDFBuilder(Payslip PayslipModel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (PayslipModel == null)
                throw new ArgumentNullException("Payslip is null");
            _ViewModel = PayslipModel;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;

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
                // step 1: creation of a document-object
                document = new Document(PageSize.A4);

                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));



                //open document
                document.Open();

                // we Add a Header 
                AddHeader();

                // Summary
                AddSummary();

                //Earnings and Deductions table
                AddEarningsAndDeductions();

                //Tax Computation
                AddTaxComputationInfo();

                //Other Payslip Details
                //AddOtherInfo();

                AddOtherPayslipLoanInfo();

                AddDocFooter();

                //Finished
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

        private void AddHeader()
        {

            Table hTable = new Table(2);
            hTable.WidthPercentage = 100;
            hTable.Padding = 2;
            hTable.Spacing = 0;

            Cell aCell = new Cell(new Phrase("PAYSLIP\n", hFont1));
            aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            aCell.Colspan = 2;
            aCell.BorderWidthLeft = 1;
            hTable.AddCell(aCell);

            Cell bCell = new Cell(new Phrase("For the period " + _ViewModel.PayPeriod.ToString("MMM-yyyy"), hFont2));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 2;
            bCell.Border = Cell.NO_BORDER;
            bCell.BorderWidthLeft = 1;
            hTable.AddCell(bCell);

            Cell cCell1 = new Cell(new Phrase("Employee No:", bFont1));
            cCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
            cCell1.Border = Cell.NO_BORDER;
            cCell1.BorderWidthLeft = 1;
            hTable.AddCell(cCell1);

            Cell cCell2 = new Cell(new Phrase(_ViewModel.EmpNo, bFont1));
            cCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
            cCell2.Border = Cell.NO_BORDER;
            hTable.AddCell(cCell2);


            Cell dCell1 = new Cell(new Phrase("Employee Name:", bFont1));
            dCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
            dCell1.Border = Cell.NO_BORDER;
            dCell1.BorderWidthLeft = 1;
            hTable.AddCell(dCell1);

            Cell dCell2 = new Cell(new Phrase(_ViewModel.EmpName, bFont1));
            dCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
            dCell2.Border = Cell.NO_BORDER;
            hTable.AddCell(dCell2);

            Cell eCell1 = new Cell(new Phrase("Payment Date:", bFont1));
            eCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
            eCell1.Border = Cell.NO_BORDER;
            eCell1.BorderWidthLeft = 1;
            hTable.AddCell(eCell1);

            Cell eCell2 = new Cell(new Phrase(_ViewModel.PaymentDate.ToString("dd-MMM-yyyy"), bFont1));
            eCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
            eCell2.Border = Cell.NO_BORDER;
            hTable.AddCell(eCell2);


            document.Add(hTable);


        }

        private void AddSummary()
        {
            Table aTable;
            aTable = new Table(6, 2);
            aTable.Padding = 2;
            aTable.Alignment = Table.ALIGN_LEFT;
            aTable.Spacing = 0;
            aTable.WidthPercentage = 100;
            aTable.AutoFillEmptyCells = true;


            Cell aCell = new Cell(new Phrase("Summary", hFont1));
            aCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            aCell.Colspan = 6;
            aTable.AddCell(aCell);


            aTable.AddCell(new Phrase("Basic", tHFont), new System.Drawing.Point(1, 0));

            Cell basicCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.BasicPay), tcFont));
            basicCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(basicCell, new System.Drawing.Point(2, 0));

            aTable.AddCell(new Phrase("Benefits", tHFont), new System.Drawing.Point(1, 1));

            Cell BenefitsCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalBenefits), tcFont));
            BenefitsCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(BenefitsCell, new System.Drawing.Point(2, 1));

            aTable.AddCell(new Phrase("Variables", tHFont), new System.Drawing.Point(1, 2));

            Cell VariablesCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Variables), tcFont));
            VariablesCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(VariablesCell, new System.Drawing.Point(2, 2));

            aTable.AddCell(new Phrase("Stat Ded", tHFont), new System.Drawing.Point(1, 3));

            Cell tsdCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalStatutoryDeductions), tcFont));
            tsdCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(tsdCell, new System.Drawing.Point(2, 3));


            aTable.AddCell(new Phrase("Other Deductions", tHFont), new System.Drawing.Point(1, 4));

            Cell odCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.OtherDeductions), tcFont));
            odCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(odCell, new System.Drawing.Point(2, 4));

            aTable.AddCell(new Phrase("Net Salary", tHFont), new System.Drawing.Point(1, 5));

            Cell NetSalaryCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NetSalary), tcFont));
            NetSalaryCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(NetSalaryCell, new System.Drawing.Point(2, 5));



            document.Add(aTable);

        }

        private void AddEarningsAndDeductions()
        {



            //Add earnings
            Table earningsTable = new Table(3); //New table with 2 columns
            earningsTable.Padding = 2;
            earningsTable.WidthPercentage = 100;
            earningsTable.Spacing = 0;


            Cell fCell = new Cell(new Phrase("Earnings", hFont1));
            fCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            fCell.Colspan = 3;
            earningsTable.AddCell(fCell);



            //Add table headings
            AddEarningsAndDeductionsTableHeadings(earningsTable);

            //Add table details

            foreach (var e in _ViewModel.Earnings)
            {
                //create a column for each earning
                Cell desCell = new Cell(new Phrase(e.Description, tcFont4));
                desCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                earningsTable.AddCell(desCell);

                Cell aCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", e.Amount), tcFont3));
                aCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                earningsTable.AddCell(aCell);

                Cell YTDCell = new Cell(new Phrase("", tcFont));
                if (e.TrackYTD)
                {
                    YTDCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", e.YTD), tcFont3));
                    YTDCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                }



                earningsTable.AddCell(YTDCell);
            }



            Cell erCell = new Cell(new Phrase("Total Earnings  ", tHFont));
            erCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            earningsTable.AddCell(erCell);

            Cell mvCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalEarnings.ToString("#,##0")), tHFont));
            mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            earningsTable.AddCell(mvCell);



            document.Add(earningsTable);


            //add deductions
            Table deductionsTable = new Table(3); //New table with 2 columns
            deductionsTable.Padding = 2;
            deductionsTable.WidthPercentage = 100;
            deductionsTable.Spacing = 0;


            Cell dgCell = new Cell(new Phrase("Deductions", hFont1));
            dgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            dgCell.Colspan = 3;
            deductionsTable.AddCell(dgCell);



            //Add table headings
            AddEarningsAndDeductionsTableHeadings(deductionsTable);

            foreach (var e in _ViewModel.AllDeductions)
            {

                Cell desCell = new Cell(new Phrase(e.Description, tcFont4));
                desCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                deductionsTable.AddCell(desCell);

                Cell aCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", e.Amount), tcFont3));
                aCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                deductionsTable.AddCell(aCell);

                Cell YTDCell = new Cell(new Phrase("", tcFont));
                if (e.TrackYTD)
                {
                    YTDCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", e.YTD), tcFont3));
                    YTDCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                }
                deductionsTable.AddCell(YTDCell);

            }

            Cell hkCell = new Cell(new Phrase("Total Deductions  ", tHFont));
            hkCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            deductionsTable.AddCell(hkCell);

            Cell sdCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalDeductions.ToString("#,##0")), tHFont));
            sdCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            deductionsTable.AddCell(sdCell);


            document.Add(deductionsTable);

        }

        private void AddEarningsAndDeductionsTableHeadings(Table aTable)
        {
            List<string> th = new List<string>() { "Item", "Month Amount", "YTD Amount" };
            foreach (var e in th)
            {

                Cell desCell = new Cell(new Phrase(e, tHFont1));
                desCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aTable.AddCell(desCell);
            }
        }

        private void AddTaxComputationInfo()
        {


            Table taxDetails = new Table(2);
            taxDetails.AutoFillEmptyCells = true;
            taxDetails.WidthPercentage = 100;
            taxDetails.Spacing = 2;


            Cell fCell = new Cell(new Phrase("Tax Computation", hFont1));
            fCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            fCell.Colspan = 2;
            taxDetails.AddCell(fCell);


            taxDetails.AddCell(new Phrase("TAXABLE EARNINGS", tHFont));
            Cell earnCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.GrossTaxableEarnings), tcFont2));
            earnCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(earnCell);


            taxDetails.AddCell(new Phrase(" - Allowable Deductions", tcFont2));
            Cell adCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.AllowableDeductions), tcFont2));
            adCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(adCell);

            taxDetails.AddCell(new Phrase(" - Mortgage/Insurance Relief", tcFont2));
            Cell mrCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.MortgageRelief), tcFont2));
            mrCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(mrCell);

            taxDetails.AddCell(new Phrase("=NET TAXABLE EARNINGS", tHFont));
            Cell ntCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NetTaxableEarnings), tcFont2));
            ntCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(ntCell);

            taxDetails.AddCell(new Phrase("GROSS TAX", tHFont));
            Cell gtCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.GrossTax), tcFont2));
            gtCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(gtCell);

            taxDetails.AddCell(new Phrase(" - Personal Relief", tcFont2));
            Cell prCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PersonalRelief), tcFont2));
            prCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(prCell);

            taxDetails.AddCell(new Phrase("NET TAX", tHFont));
            Cell netTaxCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NetTax), tcFont2));
            netTaxCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(netTaxCell);

            document.Add(taxDetails);

        }

        private void PayslipDetailsTableHeaders(Table aTable)
        {
            List<string> th = new List<string>() { "Description", "PayAmount" };
            foreach (var e in th)
            {

                Cell desCell = new Cell(new Phrase(e, tHFont1));
                desCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aTable.AddCell(desCell);
            }
        }

        private void AddOtherInfo()
        {

            Table OtherPayments = new Table(2);
            OtherPayments.AutoFillEmptyCells = true;
            OtherPayments.WidthPercentage = 100;
            OtherPayments.Spacing = 2;


            Cell hCell = new Cell(new Phrase("Other Payments", hFont1));
            hCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            hCell.Colspan = 2;
            OtherPayments.AddCell(hCell);

            PayslipDetailsTableHeaders(OtherPayments);

            foreach (var op in _ViewModel.Earnings)
            {
                if (op.TaxTracking.Trim() == "EARNING" && op.Description.Trim() != "BASIC")
                {

                    Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                    desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    OtherPayments.AddCell(desopCell);

                    Cell aopCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                    aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    OtherPayments.AddCell(aopCell);
                }

            }


            Cell eaopCell = new Cell(new Phrase("Totals", tHFont));
            eaopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            OtherPayments.AddCell(eaopCell);

            //Cell etopCell = new Cell(new Phrase(_ViewModel.TotalEarnings.ToString(), tHFont));
            //etopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //AllOtherPayments.AddCell(etopCell);

            document.Add(OtherPayments);



            Table OtherDeductions = new Table(2);
            OtherDeductions.AutoFillEmptyCells = true;
            OtherDeductions.WidthPercentage = 100;
            OtherDeductions.Spacing = 2;

            Cell fCell = new Cell(new Phrase("Other Deductions", hFont1));
            fCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            fCell.Colspan = 2;
            OtherDeductions.AddCell(fCell);

            PayslipDetailsTableHeaders(OtherDeductions);

            foreach (var od in _ViewModel.AllDeductions)
            {
                if (od.ItemType.Trim() == "DEDUCTION" && od.ItemType.Trim() != "STATUTORY")
                {

                    Cell descCell = new Cell(new Phrase(od.Description, tcFont4));
                    descCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    OtherDeductions.AddCell(descCell);

                    Cell sCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", od.Amount), tcFont4));
                    sCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    OtherDeductions.AddCell(sCell);
                }

            }



            Cell tdCell = new Cell(new Phrase("Totals", tHFont));
            tdCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            OtherDeductions.AddCell(tdCell);

            //Cell tsCell = new Cell(new Phrase(_ViewModel.TotalDeductions.ToString(), tHFont));
            //tsCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //OtherDeductions.AddCell(tsCell);

            document.Add(OtherDeductions);
        }

        private void PayslipDetailsLoansTableHeaders(Table aTable)
        {
            List<string> th = new List<string>() { "Description", "Amount", "Balance" };
            foreach (var e in th)
            {

                Cell payslipheadersCell = new Cell(new Phrase(e, tHFont1));
                payslipheadersCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aTable.AddCell(payslipheadersCell);
            }
        }

        private void PayslipDetailsSaccoTableHeaders(Table aTable)
        {
            List<string> th = new List<string>() { "Description", "Amount", "Total Shares" };
            foreach (var e in th)
            {

                Cell payslipheadersCell = new Cell(new Phrase(e, tHFont1));
                payslipheadersCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aTable.AddCell(payslipheadersCell);
            }
        }

        private void AddOtherPayslipLoanInfo()
        {

            Table employeeloanstable = new Table(3);
            employeeloanstable.AutoFillEmptyCells = true;
            employeeloanstable.WidthPercentage = 100;
            employeeloanstable.Spacing = 2;


            Cell helCell = new Cell(new Phrase("Employee Loans", hFont1));
            helCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            helCell.Colspan = 3;
            employeeloanstable.AddCell(helCell);

            PayslipDetailsLoansTableHeaders(employeeloanstable);

            foreach (var el in _ViewModel.AllDeductions)
            {
                if (el.ItemType.Trim() == "LOAN")
                {
                    Cell descrCell = new Cell(new Phrase(el.Description, tcFont4));
                    descrCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    employeeloanstable.AddCell(descrCell);

                    Cell amtCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", el.Amount), tcFont4));
                    amtCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    employeeloanstable.AddCell(amtCell);

                    Cell BalanceCell = new Cell(new Phrase("", tcFont));
                    if (el.TrackYTD)
                    {
                        BalanceCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", el.YTD), tcFont4));
                        BalanceCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    }
                    employeeloanstable.AddCell(BalanceCell);
                }
            }


            Cell eaCell = new Cell(new Phrase("Totals", tHFont));
            eaCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            employeeloanstable.AddCell(eaCell);

            //Cell etCell = new Cell(new Phrase(_ViewModel.TotalEarnings.ToString(), tHFont));
            //etCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //employeeloanstable.AddCell(etCell);

            //Cell edCell = new Cell(new Phrase(_ViewModel.TotalEarnings.ToString(), tHFont));
            //edCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //employeeloanstable.AddCell(edCell);

            document.Add(employeeloanstable);




            Table SaccoContriTable = new Table(3);
            SaccoContriTable.AutoFillEmptyCells = true;
            SaccoContriTable.WidthPercentage = 100;
            SaccoContriTable.Spacing = 2;
            SaccoContriTable.TableFitsPage = true;

            Cell fCell = new Cell(new Phrase("Sacco Contributions", hFont1));
            fCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            fCell.Colspan = 3;
            SaccoContriTable.AddCell(fCell);

            PayslipDetailsSaccoTableHeaders(SaccoContriTable);

            foreach (var sc in _ViewModel.AllDeductions)
            {

                if (sc.ItemType.Trim() == "SACCO")
                {


                    Cell sacCell = new Cell(new Phrase(sc.Description, tcFont4));
                    sacCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    SaccoContriTable.AddCell(sacCell);

                    Cell yCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", sc.Amount), tcFont4));
                    yCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    SaccoContriTable.AddCell(yCell);

                    Cell LBalance = new Cell(new Phrase("", tcFont));
                    if (sc.TrackYTD)
                    {
                        LBalance = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", sc.YTD), tcFont4));
                        LBalance.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    }
                    SaccoContriTable.AddCell(LBalance);
                }

            }



            Cell tdscCell = new Cell(new Phrase("Totals", tHFont));
            tdscCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            SaccoContriTable.AddCell(tdscCell);

            //Cell tsscCell = new Cell(new Phrase(_ViewModel.TotalEarnings.ToString(), tHFont));
            //tsscCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //SaccoContriTable.AddCell(tsscCell);

            //Cell tvscCell = new Cell(new Phrase(_ViewModel.TotalEarnings.ToString(), tHFont));
            //tvscCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            //SaccoContriTable.AddCell(tvscCell);

            document.Add(SaccoContriTable);
        }

        //document footer
        private void AddDocFooter()
        {

            Table netsalaryinfotable = new Table(1);
            netsalaryinfotable.WidthPercentage = 100;
            netsalaryinfotable.Border = Table.NO_BORDER;


            Cell netsalarypayable = new Cell(new Phrase("Payable To :.................................................", rms10Normal));
            netsalarypayable.Border = Cell.NO_BORDER;
            netsalaryinfotable.AddCell(netsalarypayable);


            Cell netsalaryverified = new Cell(new Phrase("Verified :  ...................................Date........................", rms10Normal));
            netsalaryverified.Border = Cell.NO_BORDER;
            netsalaryinfotable.AddCell(netsalaryverified);


            Cell netsalaryauthorized = new Cell(new Phrase("Authorized : ...................................Date........................", rms10Normal));
            netsalaryauthorized.Border = Cell.NO_BORDER;
            netsalaryinfotable.AddCell(netsalaryauthorized);

            Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            netsalaryinfotable.AddCell(sgCell);


            document.Add(netsalaryinfotable);

        }

    }
}
