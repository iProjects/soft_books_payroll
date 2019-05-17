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
    public class Payslip1PDFBuilder
    {
         
        Payslip _ViewModel;
        Document document;
        string sFilePDF;
        string Message;


        Font hFont1 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 9, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);//body 
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.NORMAL);//body
        Font tHFont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); //table Header
        Font tcFont1 = new Font(Font.HELVETICA, 6, Font.NORMAL);//table cell
        Font tcFont3 = new Font(Font.HELVETICA, 8, Font.BOLD);//table cell
        Font tcFont4 = new Font(Font.TIMES_ROMAN, 7, Font.NORMAL);//table cell
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);


        public Payslip1PDFBuilder(Payslip PayslipModel, string FileName)
        {
            if (PayslipModel == null)
                throw new ArgumentNullException("Payslip is null");
            _ViewModel = PayslipModel;

            sFilePDF = FileName;
        }

        public string GetPDF()
        {
            BuildPayslipPDF();
            return sFilePDF;
        }

        public void BuildPayslipPDF()
        {
            try
            {
                // step 1: creation of a document-object
                if (_ViewModel.Orientation.Equals("L"))
                { document = new Document(PageSize.A4.Rotate()); }
                else
                {
                    document = new Document(PageSize.A4);
                }
                // step 2: create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                //open document
                document.Open();

                // we Add a Header 
                AddHeader();

                // Summary
                AddSummary();

                //Benefits
                //AddBenefits();       

                //Earnings and Deductions table
                AddEarningsAndDeductions();

                //Tax Computation
                AddTaxComputationInfo();


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


        private void AddHeader()
        {
            Table hTable = new Table(2);
            hTable.WidthPercentage = 100;
            hTable.Padding = 1;
            hTable.Spacing = 0;

            Cell emplyaCell = new Cell(new Phrase(_ViewModel.EmployerName.ToUpper().Trim() + "\n", hFont1));
            emplyaCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            emplyaCell.BackgroundColor = ExtendedColor.GREEN;
            emplyaCell.Colspan = 2;
            hTable.AddCell(emplyaCell);

            Cell aCell = new Cell(new Phrase("PAYSLIP\n", hFont1));
            aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            aCell.Border = Cell.NO_BORDER;
            aCell.Colspan = 2;
            hTable.AddCell(aCell);

            Cell bCell = new Cell(new Phrase("For the period " + _ViewModel.PayPeriod.ToString("MMM-yyyy"), hFont2));
            bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            bCell.Colspan = 2;
            bCell.Border = Cell.NO_BORDER;
            hTable.AddCell(bCell);

            Cell cCell1 = new Cell(new Phrase("Employee No:", bFont1));
            cCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
            cCell1.Border = Cell.NO_BORDER;
            hTable.AddCell(cCell1);

            Cell cCell2 = new Cell(new Phrase(_ViewModel.EmpNo, bFont2));
            cCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
            cCell2.Border = Cell.NO_BORDER;
            hTable.AddCell(cCell2);


            Cell dCell1 = new Cell(new Phrase("Employee Name:", bFont1));
            dCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
            dCell1.Border = Cell.NO_BORDER;
            hTable.AddCell(dCell1);

            Cell dCell2 = new Cell(new Phrase(_ViewModel.EmpName, bFont2));
            dCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
            dCell2.Border = Cell.NO_BORDER;
            hTable.AddCell(dCell2);

            Cell eCell1 = new Cell(new Phrase("Payment Date:", bFont1));
            eCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
            eCell1.Border = Cell.NO_BORDER;
            hTable.AddCell(eCell1);

            Cell eCell2 = new Cell(new Phrase(_ViewModel.PaymentDate.ToString("dd-MMM-yyyy"), bFont2));
            eCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
            eCell2.Border = Cell.NO_BORDER;
            hTable.AddCell(eCell2);

            document.Add(hTable);
        }

        private void AddSummary()
        {
            
            Table aTable;
            aTable = new Table(5, 2);  
            aTable.Padding = 2;
            aTable.Spacing = 0;
            aTable.AutoFillEmptyCells = true;
            aTable.WidthPercentage = 100;

            Cell aCell = new Cell(new Phrase("SUMMARY\n", hFont2));
            aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            aCell.Colspan = 5;
            aCell.BorderWidthLeft = 1;
            aTable.AddCell(aCell);


            aTable.AddCell(new Phrase("Total Basic\nKshs", bFont1), new System.Drawing.Point(1, 0));
            aTable.AddCell(new Phrase("Variables\nKshs", bFont1), new System.Drawing.Point(1, 1));
            aTable.AddCell(new Phrase("Stat Ded\nKshs", bFont1), new System.Drawing.Point(1, 2));
            aTable.AddCell(new Phrase("Other Deductions\nKshs", bFont1), new System.Drawing.Point(1, 3));
            aTable.AddCell(new Phrase("Net Salary\nKshs", bFont1), new System.Drawing.Point(1, 4));

            Cell basicCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.BasicPay), tcFont4));
            basicCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(basicCell, new System.Drawing.Point(2, 0));

            Cell VariablesCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.Variables), tcFont4));
            VariablesCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(VariablesCell, new System.Drawing.Point(2, 1));

            Cell tsdCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalStatutoryDeductions), tcFont4));
            tsdCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(tsdCell, new System.Drawing.Point(2, 2));

            Cell odCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.OtherDeductions), tcFont4));
            odCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(odCell, new System.Drawing.Point(2, 3));

            Cell NetSalaryCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NetSalary), tcFont4));
            NetSalaryCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            aTable.AddCell(NetSalaryCell, new System.Drawing.Point(2, 4));

            document.Add(aTable);
        }

        private void AddBenefits()
        {
            Table hTable = new Table(2);
            hTable.WidthPercentage = 100;
            hTable.Padding = 1;  

            Cell aCell = new Cell(new Phrase("BENEFITS\n", hFont2));
            aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            aCell.Colspan = 2;
            aCell.BorderWidthLeft = 1;
            hTable.AddCell(aCell);

             
            document.Add(hTable); 
        }

        private void AddEarningsAndDeductions()
        {
           

            //Add earnings
            Table earningsTable = new Table(3); 
            earningsTable.Padding = 1;
            earningsTable.Spacing = 0;



            //Add table headings
            AddEarningsAndDeductionsTableHeadings(earningsTable);

            //Add table details

            foreach (var e in _ViewModel.Earnings)
            {
                //create a column for each earning
                Cell desCell = new Cell(new Phrase(e.Description, tcFont4));
                desCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                earningsTable.AddCell(desCell);

                Cell aCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", e.Amount), tcFont4));
                aCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                earningsTable.AddCell(aCell);

                Cell YTDCell = new Cell(new Phrase("", tcFont4));
                if (e.TrackYTD)
                {
                    YTDCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", e.YTD), tcFont4));
                    YTDCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                }

                earningsTable.AddCell(YTDCell);
            }

            //add deductions
            Table deductionsTable = new Table(3); //New table with 2 columns
            deductionsTable.Padding = 1;
            deductionsTable.Spacing = 0;

            //Add table headings
            AddEarningsAndDeductionsTableHeadings(deductionsTable);

            foreach (var e in _ViewModel.AllDeductions)
            {
                //create a row for each earning
                Cell desCell = new Cell(new Phrase(e.Description, tcFont4));
                desCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                deductionsTable.AddCell(desCell);

                Cell aCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", e.Amount), tcFont4));
                aCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                deductionsTable.AddCell(aCell);

                Cell YTDCell = new Cell(new Phrase("", tcFont4));
                if (e.TrackYTD)
                {
                    YTDCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", e.YTD), tcFont4));
                    YTDCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                }

                deductionsTable.AddCell(YTDCell);
            }

            Table earningsAndDeductionsTable = new Table(2, 2);    // 2 rows, 2 columns
            earningsAndDeductionsTable.Padding = 1;
            earningsAndDeductionsTable.Spacing = 0;
            earningsAndDeductionsTable.WidthPercentage = 100;



            //insert earnings and deductions table;
            earningsAndDeductionsTable.AddCell(new Phrase("EARNINGS", hFont2), new System.Drawing.Point(1, 0));

            earningsAndDeductionsTable.AddCell(new Phrase("DEDUCTIONS", hFont2), new System.Drawing.Point(1, 1));
            earningsAndDeductionsTable.InsertTable(earningsTable, new System.Drawing.Point(2, 0));

            earningsAndDeductionsTable.InsertTable(deductionsTable, new System.Drawing.Point(2, 1));

            //Add totals
            //Total Earnings
            Cell teCell = new Cell(new Phrase("Total Earnings  " + string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalEarnings.ToString("#,##0")), tHFont1));
            teCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            earningsAndDeductionsTable.AddCell(teCell, new System.Drawing.Point(3, 0));

            //Total Deductions
            Cell tdCell = new Cell(new Phrase("Total Deductions  " + string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalDeductions.ToString("#,##0")), tHFont1));
            tdCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            earningsAndDeductionsTable.AddCell(tdCell, new System.Drawing.Point(3, 1));

            //add the table to the document
            document.Add(earningsAndDeductionsTable);
        }

        private void AddEarningsAndDeductionsTableHeadings(Table aTable)
        {
            List<string> th = new List<string>() { "Item", "Month Amount", "YTD Amount" };
            foreach (var e in th)
            {
                //create a row for each earning
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


            Cell aCell = new Cell(new Phrase("TAX COMPUTATION", hFont2));
            aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            aCell.Colspan = 2;
            aCell.BorderWidthLeft = 1;
            taxDetails.AddCell(aCell);


            taxDetails.AddCell(new Phrase("TAXABLE EARNINGS", tcFont3));
            Cell earnCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.GrossTaxableEarnings), tcFont4));
            earnCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(earnCell);


            taxDetails.AddCell(new Phrase(" - Allowable Deductions", tcFont3));
            Cell adCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.AllowableDeductions), tcFont4));
            adCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(adCell);

            taxDetails.AddCell(new Phrase(" - Mortgage/Insurance Relief", tcFont3));
            Cell mrCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.MortgageRelief), tcFont4));
            mrCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(mrCell);

            taxDetails.AddCell(new Phrase("=NET TAXABLE EARNINGS", tcFont3));
            Cell ntCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NetTaxableEarnings), tcFont4));
            ntCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(ntCell);

            taxDetails.AddCell(new Phrase("GROSS TAX", tcFont3));
            Cell gtCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.GrossTax), tcFont4));
            gtCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(gtCell);

            taxDetails.AddCell(new Phrase(" - Personal Relief", tcFont3));
            Cell prCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PersonalRelief), tcFont4));
            prCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(prCell);

            taxDetails.AddCell(new Phrase("NET TAX", tcFont3));
            Cell netTaxCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NetTax), tcFont4));
            netTaxCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            taxDetails.AddCell(netTaxCell);


            document.Add(taxDetails);
        }


        //document footer
        private void AddDocFooter()
        {

            Table netsalaryinfotable = new Table(1);
            netsalaryinfotable.WidthPercentage = 100;
            netsalaryinfotable.Border = Table.NO_BORDER;

            Cell sgCell = new Cell(new Phrase("Signature.............................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            sgCell.Border = Cell.NO_BORDER;
            netsalaryinfotable.AddCell(sgCell);


            document.Add(netsalaryinfotable);

        }


    }
}
