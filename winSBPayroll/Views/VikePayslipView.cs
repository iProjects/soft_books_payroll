using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CommonLib;
using BLL.DataEntry;
using DAL;
//--- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using winSBPayroll.ViewModel;

namespace winSBPayroll.Views
{
    public class VikepayslipView
    {

        VikePayslipViewModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        ////DEFINED FONTS
        Font hFont1 = new Font(Font.TIMES_ROMAN, 7, Font.BOLD);
        Font hFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD);
        Font bFont1 = new Font(Font.TIMES_ROMAN, 7, Font.NORMAL);//body 
        Font bFont2 = new Font(Font.TIMES_ROMAN, 7, Font.NORMAL);//body
        Font tHFont = new Font(Font.TIMES_ROMAN, 7, Font.NORMAL); //table Header
        Font tHFont1 = new Font(Font.TIMES_ROMAN, 7, Font.BOLD); //table Header
        Font tcFont = new Font(Font.HELVETICA, 6, Font.NORMAL);//table cell
        Font tcFont2 = new Font(Font.HELVETICA, 6, Font.NORMAL);//table cell
        Font tcFont3 = new Font(Font.HELVETICA, 6, Font.BOLD);//table cell
        Font tcFont4 = new Font(Font.TIMES_ROMAN, 6, Font.NORMAL);//table cell
        Font rms10Normal = new Font(Font.HELVETICA, 7, Font.NORMAL);


        public VikepayslipView(VikePayslipViewModel PayslipModel, string FileName, string Conn)
        {

            if (PayslipModel == null)
                throw new ArgumentNullException("VikePayslipViewModel is null");
            _ViewModel = PayslipModel;


            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            sFilePDF = FileName;

        }

        public string GetPDF()
        {
            try
            {
                BuildPDF();
                return sFilePDF;
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return null;
            }
        }

        private void BuildPDF()
        {
            try
            {
                // step 1: creation of a document-object
                document = new Document(PageSize.A4, 10, 10, 10, 10);

                // step 2: we create a writer that listens to the document
                PdfWriter writer = PdfWriter.GetInstance(document,
                    new FileStream(sFilePDF, FileMode.Create, FileAccess.Write, FileShare.ReadWrite));

                //open document
                document.Open();

                //Add  Header 
                AddHeader();

                //Add Payments 
                AddPayments();

                //Add Payee Details 
                AddPAYEDetails();

                //Add Deductions
                AddDeductions();

                //Add Summary
                AddSummary();

                //Add Other Payments
                AddOtherPayments();

                //Add Other Deductions
                AddOtherDeductions();

                //Add Employee Loans
                AddEmployeeLoans();

                //Add Sacco Contributions
                AddSACCOContributions();

                //Add Document Footer
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
            try
            {
                Table payslipTable = new Table(2);
                payslipTable.WidthPercentage = 100;
                payslipTable.Padding = 1;

                Cell aCell = new Cell(new Phrase(_ViewModel.EmployerName.ToUpper().Trim() + "\n", hFont1));
                aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aCell.Colspan = 2;
                payslipTable.AddCell(aCell);

                Cell emailCell = new Cell(new Phrase(_ViewModel.EmployerAddress.Trim() + "\n", hFont1));
                emailCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                emailCell.Colspan = 2;
                payslipTable.AddCell(emailCell);

                Cell bCell = new Cell(new Phrase("PAYSLIP", new Font(Font.TIMES_ROMAN, 8, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
                bCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                bCell.Colspan = 2;
                bCell.Border = Cell.NO_BORDER;
                payslipTable.AddCell(bCell);

                Cell emCell1 = new Cell(new Phrase("Payroll Month:", bFont1));
                emCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                emCell1.Border = Cell.NO_BORDER;
                payslipTable.AddCell(emCell1);

                Cell empCell2 = new Cell(new Phrase(_ViewModel.PayrollMonth, bFont2));
                empCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                empCell2.Border = Cell.NO_BORDER;
                payslipTable.AddCell(empCell2);

                Cell periodCell = new Cell(new Phrase("Payroll Year:", bFont1));
                periodCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                periodCell.Border = Cell.NO_BORDER;
                payslipTable.AddCell(periodCell);

                Cell periodCell2 = new Cell(new Phrase(_ViewModel.PayrollYear.ToString(), bFont2));
                periodCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                periodCell2.Border = Cell.NO_BORDER;
                payslipTable.AddCell(periodCell2);

                Cell printedonCell1 = new Cell(new Phrase("Printed On:", bFont1));
                printedonCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                printedonCell1.Border = Cell.NO_BORDER;
                payslipTable.AddCell(printedonCell1);

                Cell printedonCell2 = new Cell(new Phrase(_ViewModel.PrintedOn.ToString("dd-MMM-yyyy"), bFont2));
                printedonCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                printedonCell2.Border = Cell.NO_BORDER;
                payslipTable.AddCell(printedonCell2);

                Cell cCell1 = new Cell(new Phrase("Employee No:", bFont1));
                cCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                cCell1.Border = Cell.NO_BORDER;
                payslipTable.AddCell(cCell1);

                Cell cCell2 = new Cell(new Phrase(_ViewModel.EmpNo, bFont2));
                cCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                cCell2.Border = Cell.NO_BORDER;
                payslipTable.AddCell(cCell2);


                Cell dCell1 = new Cell(new Phrase("Employee :", bFont1));
                dCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                dCell1.Border = Cell.NO_BORDER;
                payslipTable.AddCell(dCell1);

                Cell dCell2 = new Cell(new Phrase(_ViewModel.EmpName, bFont2));
                dCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                dCell2.Border = Cell.NO_BORDER;
                payslipTable.AddCell(dCell2);

                Cell eCell1 = new Cell(new Phrase("Pin No:", bFont1));
                eCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                eCell1.Border = Cell.NO_BORDER;
                payslipTable.AddCell(eCell1);

                Cell eCell2 = new Cell(new Phrase(_ViewModel.PINNo, bFont2));
                eCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                eCell2.Border = Cell.NO_BORDER;
                payslipTable.AddCell(eCell2);

                Cell nssfCell1 = new Cell(new Phrase("NSSF No:", bFont1));
                nssfCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                nssfCell1.Border = Cell.NO_BORDER;
                payslipTable.AddCell(nssfCell1);

                Cell nssfCell2 = new Cell(new Phrase(_ViewModel.NSSFNO, bFont2));
                nssfCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                nssfCell2.Border = Cell.NO_BORDER;
                payslipTable.AddCell(nssfCell2);

                Cell nhifCell1 = new Cell(new Phrase("NHIF No:", bFont1));
                nhifCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                nhifCell1.Border = Cell.NO_BORDER;
                payslipTable.AddCell(nhifCell1);

                Cell nhifCell2 = new Cell(new Phrase(_ViewModel.NHIFNO, bFont2));
                nhifCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                nhifCell2.Border = Cell.NO_BORDER;
                payslipTable.AddCell(nhifCell2);

                Cell departmentCell1 = new Cell(new Phrase("Department:", bFont1));
                departmentCell1.HorizontalAlignment = Cell.ALIGN_LEFT;
                departmentCell1.Border = Cell.NO_BORDER;
                payslipTable.AddCell(departmentCell1);

                Cell departmentCell2 = new Cell(new Phrase(_ViewModel.Department, bFont2));
                departmentCell2.HorizontalAlignment = Cell.ALIGN_LEFT;
                departmentCell2.Border = Cell.NO_BORDER;
                payslipTable.AddCell(departmentCell2);


                document.Add(payslipTable);

            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddPayments()
        {
            try
            {
                Table payslipTable = new Table(2);
                payslipTable.WidthPercentage = 100;
                payslipTable.Padding = 1;


                Cell aCell = new Cell(new Phrase("PAYMENTS\n", hFont2));
                aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aCell.Colspan = 2;
                aCell.BorderWidthLeft = 1;
                payslipTable.AddCell(aCell);

                foreach (var op in _ViewModel.Payments)
                {
                    Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                    desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable.AddCell(desopCell);

                    Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                    aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable.AddCell(aopCell);
                }

                Cell saCell = new Cell(new Phrase("TOTAL PAYMENTS", tcFont3));
                saCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(saCell);

                Cell tpCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalPayments), tcFont3));
                tpCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(tpCell);

                document.Add(payslipTable);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }

        }

        private void AddPAYEDetails()
        {
            try
            {
                Table payslipTable = new Table(2);
                payslipTable.WidthPercentage = 100;
                payslipTable.Padding = 1;

                Cell aCell = new Cell(new Phrase("PAYE DETAILS\n", hFont2));
                aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aCell.Colspan = 2;
                payslipTable.AddCell(aCell);

                Cell tCell = new Cell(new Phrase("TAXABLE PAY", tcFont3));
                tCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(tCell);

                Cell t1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PayeDetail.TaxablePay), tcFont3));
                t1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(t1Cell);


                Cell tdCell = new Cell(new Phrase("TAX DUE", tcFont3));
                tdCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(tdCell);

                Cell td1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PayeDetail.TaxDue), tcFont3));
                td1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(td1Cell);

                Cell prCell = new Cell(new Phrase("Personal Relief", tcFont4));
                prCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(prCell);

                Cell pr1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PayeDetail.PersonalRelief), tcFont4));
                pr1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(pr1Cell);

                Cell insuranceCell = new Cell(new Phrase("Life Insurance Relief", tcFont4));
                insuranceCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(insuranceCell);

                Cell insuranceCell1 = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.PayeDetail.InsuranceRelief), tcFont4));
                insuranceCell1.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(insuranceCell1);


                document.Add(payslipTable);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }


        private void AddDeductions()
        {
            try
            {
                Table payslipTable = new Table(2);
                payslipTable.WidthPercentage = 100;
                payslipTable.Padding = 1;

                Cell aCell = new Cell(new Phrase("DEDUCTIONS\n", hFont2));
                aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aCell.Colspan = 2;
                aCell.BorderWidthLeft = 1;
                payslipTable.AddCell(aCell);

                foreach (var op in _ViewModel.Deductions)
                {
                    Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                    desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable.AddCell(desopCell);

                    Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                    aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable.AddCell(aopCell);
                }

                Cell tdCell = new Cell(new Phrase("TOTAL DEDUCTIONS", tcFont3));
                tdCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(tdCell);

                Cell td1Cell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalDeductions), tcFont3));
                td1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(td1Cell);

                document.Add(payslipTable);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddSummary()
        {
            try
            {
                Table payslipTable = new Table(2);
                payslipTable.WidthPercentage = 100;
                payslipTable.Padding = 1;

                Cell npCell = new Cell(new Phrase("NET PAY", tcFont3));
                npCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(npCell);

                Cell npvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.NetPay), tcFont3));
                npvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(npvCell);

                document.Add(payslipTable);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }


        private void PayslipOtherPaymentsDeductionsHeaders(Table aTable)
        {
            try
            {
                List<string> th = new List<string>() { "Description", "Amount" };

                foreach (var e in th)
                {

                    Cell desCell = new Cell(new Phrase(e, tHFont1));
                    desCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aTable.AddCell(desCell);
                }
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddOtherPayments()
        {
            try
            {
                Table payslipTable = new Table(2);
                payslipTable.WidthPercentage = 100;
                payslipTable.Padding = 1;

                Cell aCell = new Cell(new Phrase("Other Payments\n", hFont2));
                aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aCell.Colspan = 2;
                aCell.BorderWidthLeft = 1;
                payslipTable.AddCell(aCell);

                PayslipOtherPaymentsDeductionsHeaders(payslipTable);

                foreach (var op in _ViewModel.OtherPayments)
                {
                    Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                    desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable.AddCell(desopCell);

                    Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                    aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable.AddCell(aopCell);
                }

                Cell erCell = new Cell(new Phrase("Totals", tHFont1));
                erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(erCell);

                Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalOtherPayments), tcFont4));
                mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(mvCell);

                document.Add(payslipTable);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddOtherDeductions()
        {
            try
            {
                Table payslipTable = new Table(2);
                payslipTable.WidthPercentage = 100;
                payslipTable.Padding = 1;


                Cell aCell = new Cell(new Phrase("Other Deductions\n", hFont2));
                aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aCell.Colspan = 2;
                aCell.BorderWidthLeft = 1;
                payslipTable.AddCell(aCell);

                PayslipOtherPaymentsDeductionsHeaders(payslipTable);

                foreach (var op in _ViewModel.OtherDeductions)
                {
                    Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                    desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable.AddCell(desopCell);

                    Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                    aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable.AddCell(aopCell);
                }

                Cell erCell = new Cell(new Phrase("Totals", tHFont1));
                erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(erCell);

                Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalOtherDeductions), tcFont4));
                mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(mvCell);


                document.Add(payslipTable);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }

        private void PayslipLoanDetailsHeader(Table aTable)
        {
            try
            {
                List<string> th = new List<string>() { "Description", "Amount", "Balance" };

                foreach (var e in th)
                {

                    Cell desCell = new Cell(new Phrase(e, tHFont1));
                    desCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aTable.AddCell(desCell);
                }
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }

        private void PayslipSaccoDetailsHeader(Table aTable)
        {
            try
            {
                List<string> th = new List<string>() { "Description", "Amount", "Total Shares" };

                foreach (var e in th)
                {

                    Cell desCell = new Cell(new Phrase(e, tHFont1));
                    desCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                    aTable.AddCell(desCell);
                }
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }


        private void AddEmployeeLoans()
        {
            try
            {
                Table payslipTable = new Table(3);
                payslipTable.WidthPercentage = 100;
                payslipTable.Padding = 1;

                Cell aCell = new Cell(new Phrase("Employee LOANS\n", hFont2));
                aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aCell.BorderWidthLeft = 1;
                aCell.Colspan = 3;
                payslipTable.AddCell(aCell);

                PayslipLoanDetailsHeader(payslipTable);

                foreach (var op in _ViewModel.Loans)
                {
                    Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                    desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable.AddCell(desopCell);

                    Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                    aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable.AddCell(aopCell);

                    Cell lbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                    lbCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable.AddCell(lbCell);

                }

                Cell erCell = new Cell(new Phrase("Totals", tHFont1));
                erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(erCell);

                Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalAmountLoansAmount), tcFont4));
                mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(mvCell);

                Cell ltbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalAmountLoansBalance), tcFont4));
                ltbCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(ltbCell);

                document.Add(payslipTable);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }

        private void AddSACCOContributions()
        {
            try
            {
                Table payslipTable = new Table(3);
                payslipTable.WidthPercentage = 100;
                payslipTable.Padding = 1;


                Cell aCell = new Cell(new Phrase("SACCO Contributions\n", hFont2));
                aCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                aCell.BorderWidthLeft = 1;
                aCell.Colspan = 3;
                payslipTable.AddCell(aCell);

                PayslipSaccoDetailsHeader(payslipTable);

                foreach (var op in _ViewModel.SACCOContributions)
                {
                    Cell desopCell = new Cell(new Phrase(op.Description, tcFont4));
                    desopCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                    payslipTable.AddCell(desopCell);

                    Cell aopCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.Amount), tcFont4));
                    aopCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable.AddCell(aopCell);

                    Cell scCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", op.YTD), tcFont4));
                    scCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payslipTable.AddCell(scCell);
                }


                Cell erCell = new Cell(new Phrase("Totals", tHFont1));
                erCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                payslipTable.AddCell(erCell);

                Cell mvCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalAmountSaccoAmount), tcFont4));
                mvCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(mvCell);

                Cell ltbCell = new Cell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalAmountSaccoBalance), tcFont4));
                ltbCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                payslipTable.AddCell(ltbCell);

                document.Add(payslipTable);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }
        }

        //document footer
        private void AddDocFooter()
        {
            try
            {
                Table payslipTable = new Table(1);
                payslipTable.WidthPercentage = 100;
                payslipTable.Border = Table.NO_BORDER;

                Cell sgCell = new Cell(new Phrase("Signature.....................................................................................................", rms10Normal));
                sgCell.HorizontalAlignment = Cell.ALIGN_LEFT;
                sgCell.Border = Cell.NO_BORDER;
                payslipTable.AddCell(sgCell);


                document.Add(payslipTable);
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return;
            }

        }


    }
}