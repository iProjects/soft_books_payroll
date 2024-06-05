using System;
using System.Collections.Generic;
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
    public class PayrollMasterPDFBuilder2
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
        System.Collections.Generic.List<string> _earnings;
        System.Collections.Generic.List<string> _deductions;

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
        public PayrollMasterPDFBuilder2(System.Collections.Generic.List<string> earnings, System.Collections.Generic.List<string> deductions, PayrollMasterModel payrollMasterModel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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

            _earnings = earnings;
            _deductions = deductions;
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
                if (System.IO.File.Exists(sFilePDF))
                {
                    System.IO.File.Delete(sFilePDF);
                }

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

            int Cols = 2 + _earnings.Count + 1 + _deductions.Count + 4;
            Table payrollMasterTable = new Table(Cols);
            payrollMasterTable.WidthPercentage = 100;

            int[] colWidthPercentages = new int[Cols];
            //initialize the first 2
            int initialcols = 2;
            colWidthPercentages[0] = 3;
            colWidthPercentages[1] = 5;

            int others = colWidthPercentages.Sum();
            int dif = 100 - others;
            int othercols = Cols - initialcols;
            int colsize = dif / othercols;
            for (int i = initialcols; i < Cols; i++)
            {
                colWidthPercentages[i] = colsize;
            }

            payrollMasterTable.SetWidths(colWidthPercentages);
            payrollMasterTable.Spacing = 1;
            payrollMasterTable.Padding = 1;

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


            foreach (var sub in _earnings)
            {
                Cell markCell = new Cell(new Phrase(sub + "\nKshs", tHFont));
                markCell.Border = Cell.RECTANGLE;
                markCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                payrollMasterTable.AddCell(markCell);
            }

            Cell c7 = new Cell(new Phrase("Gross Pay\nKshs", tHFont));
            c7.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(c7);

            foreach (var sub in _deductions)
            {
                Cell markCell = new Cell(new Phrase(sub + "\nKshs", tHFont));
                markCell.Border = Cell.RECTANGLE;
                markCell.HorizontalAlignment = Cell.ALIGN_CENTER;
                payrollMasterTable.AddCell(markCell);
            }

            Cell loanCell = new Cell(new Phrase("Loans\nKshs", tHFont));
            loanCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(loanCell);

            Cell saccoCell = new Cell(new Phrase("Saccos \nContributions \nKshs", tHFont));
            saccoCell.HorizontalAlignment = Cell.ALIGN_CENTER;
            payrollMasterTable.AddCell(saccoCell);

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


            foreach (var sub in this._earnings)
            {
                var _rec = rec.GetMainEarnings.Where(s => s.Description == sub).SingleOrDefault();
                if (_rec != null)
                {
                    decimal item = decimal.Parse(_rec.Amount.ToString());

                    Cell markCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", item), helv8Font));
                    markCell.Border = Cell.RECTANGLE;
                    markCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
                else
                {
                    Cell markCell = new Cell(new Phrase("0", helv8Font));
                    markCell.Border = Cell.RECTANGLE;
                    markCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
            }

            Cell t1Cell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.GrossTaxableEarnings), helv8Font));
            t1Cell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(t1Cell);

            foreach (var sub in this._deductions)
            {
                var _rec = rec.GetOtherDeductions.Where(s => s.Description == sub).SingleOrDefault();
                if (_rec != null)
                {
                    decimal item = decimal.Parse(_rec.Amount.ToString());

                    //if(sub.Equals("NSSF"))
                    //{
                    //    switch (rep.SettingLookup("NSSFCOMPUTATIONMETHOD").ToUpper())
                    //    {
                    //        case "OLD":
                    //            break;
                    //        case "NEW":
                    //            int lowerearninglimit = int.Parse(rep.SettingLookup("NSSFMINLOWEREARNINGLIMIT"));
                    //            int upperearninglimit = int.Parse(rep.SettingLookup("NSSFMAXUPPEREARNINGLIMIT"));
                    //            decimal _employeecontributionpecentage = int.Parse(rep.SettingLookup("NSSFEMPLOYEECONTRIBUTIONPERCENTAGE"));
                    //            decimal _employercontributionpercentage = int.Parse(rep.SettingLookup("NSSFEMPLOYERCONTRIBUTIONPERCENTAGE"));
                    //            NssfContributionsDTO _NssfContributionsDTO = rep.ComputeNssfContributions(rec.EmpNo);
                    //            item = _NssfContributionsDTO.TotalPensionContribution;
                    //            break;
                    //    }
                    //}                    

                    Cell markCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", item), helv8Font));
                    markCell.Border = Cell.RECTANGLE;
                    markCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
                else
                {
                    Cell markCell = new Cell(new Phrase("0", helv8Font));
                    markCell.Border = Cell.RECTANGLE;
                    markCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
            }

            Cell loanCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.TotalLoansDeductions), helv8Font));
            loanCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(loanCell);

            Cell saccoCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", rec.TotalSACCODeductions), helv8Font));
            saccoCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(saccoCell);

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
            Cell enoCell = new Cell(new Phrase("TOTALS", tHFont));
            enoCell.Colspan = 2;
            enoCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(enoCell);

            foreach (var sub in this._earnings)
            {
                var totalOtherPayments = _ViewModel.paymaster.Sum(p => p.GetMainEarnings.Where(e => e.Description == sub).Sum(i => i.Amount));

                if (totalOtherPayments != null)
                {
                    decimal item = decimal.Parse(totalOtherPayments.ToString());

                    Cell markCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", item), helv8Font));
                    markCell.Border = Cell.RECTANGLE;
                    markCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
                else
                {
                    Cell markCell = new Cell(new Phrase("0", helv8Font));
                    markCell.Border = Cell.RECTANGLE;
                    markCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
            }

            decimal gp = _ViewModel.paymaster.Sum(a => a.GrossTaxableEarnings);
            Cell tCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", gp), tHFont));
            tCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(tCell);

            foreach (var sub in this._deductions)
            {
                var totalOtherDeductions = _ViewModel.paymaster.Sum(p => p.GetOtherDeductions.Where(e => e.Description == sub).Sum(i => i.Amount));

                //List<DAL.psuedovwPayrollMaster> _PayrollMasterList = new List<psuedovwPayrollMaster>();

                //var _rec = rec.GetOtherDeductions.Where(s => s.Description == sub).SingleOrDefault();
                //if (_rec != null)

                //foreach (var pay in _ViewModel.paymaster)
                //{
                //    if (sub.Equals("NSSF"))
                //    {
                //        switch (rep.SettingLookup("NSSFCOMPUTATIONMETHOD").ToUpper())
                //        {
                //            case "OLD":
                //                break;
                //            case "NEW":
                //                int lowerearninglimit = int.Parse(rep.SettingLookup("NSSFMINLOWEREARNINGLIMIT"));
                //                int upperearninglimit = int.Parse(rep.SettingLookup("NSSFMAXUPPEREARNINGLIMIT"));
                //                decimal _employeecontributionpecentage = int.Parse(rep.SettingLookup("NSSFEMPLOYEECONTRIBUTIONPERCENTAGE"));
                //                decimal _employercontributionpercentage = int.Parse(rep.SettingLookup("NSSFEMPLOYERCONTRIBUTIONPERCENTAGE"));
                //                NssfContributionsDTO _NssfContributionsDTO = rep.ComputeNssfContributions(pay.EmpNo);
                //                totalOtherDeductions += _NssfContributionsDTO.TotalPensionContribution;
                //                break;
                //        }
                //    } 
                //} 

                if (totalOtherDeductions != null)
                {
                    decimal item = decimal.Parse(totalOtherDeductions.ToString());

                    Cell markCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", item), helv8Font));
                    markCell.Border = Cell.RECTANGLE;
                    markCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
                else
                {
                    Cell markCell = new Cell(new Phrase("0", helv8Font));
                    markCell.Border = Cell.RECTANGLE;
                    markCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
            }

            Cell loanCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalLoansDeductions), tHFont));
            loanCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(loanCell);

            Cell saccoCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", _ViewModel.TotalSACCODeductions), tHFont));
            saccoCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(saccoCell);

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

            Cell totalsCell = new Cell(new Phrase("Totals:", rms8Normal));
            totalsCell.HorizontalAlignment = Cell.ALIGN_LEFT;
            payrollMasterTable.AddCell(totalsCell);

            Cell totalsvalueCell = new Cell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N0}", (_ViewModel.Salaries + _ViewModel.deductionNSSF + _ViewModel.NHIF + _ViewModel.PAYE).ToString("#,##0")), rms8Normal));
            totalsvalueCell.HorizontalAlignment = Cell.ALIGN_RIGHT;
            payrollMasterTable.AddCell(totalsvalueCell);

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
