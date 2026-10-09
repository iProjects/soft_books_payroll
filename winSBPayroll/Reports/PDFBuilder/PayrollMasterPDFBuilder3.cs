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
    public class PayrollMasterPDFBuilder3
    {
        // Private attributes 
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

        // Define fonts
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
        Font rms8Normal = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD);
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD);
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD);
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD);
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL);
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public PayrollMasterPDFBuilder3(System.Collections.Generic.List<string> earnings, System.Collections.Generic.List<string> deductions, PayrollMasterModel payrollMasterModel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
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

            TAG = this.GetType().Name;

            _notificationmessageEventname = notificationmessageEventname;

            sFilePDF = FileName;

            _earnings = earnings;

            _deductions = deductions;

            _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed PayrollMasterPDFBuilder3", TAG));
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
                if (System.IO.File.Exists(sFilePDF))
                {
                    System.IO.File.Delete(sFilePDF);
                }

                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20);

                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create));

                document.Open();

                AddDocHeader();
                AddDocBody();
                AddFooter();
                AddDocFooter();

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
            PdfPTable payrollMasterTable = new PdfPTable(5);
            payrollMasterTable.WidthPercentage = 100;

            PdfPCell employernameCell = new PdfPCell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employernameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employernameCell.Colspan = 5;
            employernameCell.Border = PdfPCell.NO_BORDER;
            payrollMasterTable.AddCell(employernameCell);

            PdfPCell employeraddressCell = new PdfPCell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK)));
            employeraddressCell.HorizontalAlignment = Element.ALIGN_CENTER;
            employeraddressCell.Colspan = 5;
            employeraddressCell.Border = PdfPCell.NO_BORDER;
            payrollMasterTable.AddCell(employeraddressCell);

            PdfPCell bCell = new PdfPCell(new Phrase("MASTER PAYROLL", hFont1));
            bCell.HorizontalAlignment = Element.ALIGN_CENTER;
            bCell.Colspan = 5;
            bCell.Border = PdfPCell.NO_BORDER;
            payrollMasterTable.AddCell(bCell);

            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2));
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER;
            reportNameCell.Colspan = 5;
            reportNameCell.Border = PdfPCell.NO_BORDER;
            payrollMasterTable.AddCell(reportNameCell);

            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2));
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT;
            PrintedonCell.Colspan = 4;
            PrintedonCell.Border = PdfPCell.NO_BORDER;
            payrollMasterTable.AddCell(PrintedonCell);

            PDFGen pdfgen = new PDFGen(_notificationmessageEventname);
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo);
            img0.Alignment = Image.ALIGN_MIDDLE;

            PdfPCell logoCell = new PdfPCell();
            logoCell.Border = PdfPCell.NO_BORDER;
            logoCell.HorizontalAlignment = Element.ALIGN_LEFT;
            logoCell.AddElement(img0);
            logoCell.AddElement(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK)));
            payrollMasterTable.AddCell(logoCell);

            document.Add(payrollMasterTable);
        }

        private void AddDocBody()
        {
            int Cols = 2 + _earnings.Count + 1 + _deductions.Count + 4;
            PdfPTable payrollMasterTable = new PdfPTable(Cols);
            payrollMasterTable.WidthPercentage = 100;
            payrollMasterTable.SpacingBefore = 5f;
            payrollMasterTable.SpacingAfter = 5f;

            float[] colWidthPercentages = new float[Cols];
            int initialcols = 2;
            colWidthPercentages[0] = 3f;
            colWidthPercentages[1] = 5f;

            float others = colWidthPercentages[0] + colWidthPercentages[1];
            float dif = 100f - others;
            int othercols = Cols - initialcols;
            float colsize = dif / othercols;
            for (int i = initialcols; i < Cols; i++)
            {
                colWidthPercentages[i] = colsize;
            }

            payrollMasterTable.SetWidths(colWidthPercentages);

            // 1. Add headers
            BodyAddtableHeaders(payrollMasterTable);

            // 2. Set repeating header row for new page overflow[span_1](start_span)[span_1](end_span)
            payrollMasterTable.HeaderRows = 1;

            // 3. Add details
            foreach (var d in _ViewModel.paymaster)
            {
                BodAddTableDetail(d, payrollMasterTable);
            }

            // 4. Add totals
            AddDocBodyTableTotals(payrollMasterTable);

            // Add table to document
            document.Add(payrollMasterTable);
        }

        private void BodyAddtableHeaders(PdfPTable payrollMasterTable)
        {
            PdfPCell c1 = new PdfPCell(new Phrase("No", tHFont));
            c1.HorizontalAlignment = Element.ALIGN_CENTER;
            payrollMasterTable.AddCell(c1);

            PdfPCell c2 = new PdfPCell(new Phrase("Name", tHFont));
            c2.HorizontalAlignment = Element.ALIGN_CENTER;
            payrollMasterTable.AddCell(c2);

            foreach (var sub in _earnings)
            {
                PdfPCell markCell = new PdfPCell(new Phrase(sub + "\nKshs", tHFont));
                markCell.HorizontalAlignment = Element.ALIGN_CENTER;
                payrollMasterTable.AddCell(markCell);
            }

            PdfPCell c7 = new PdfPCell(new Phrase("Gross Pay\nKshs", tHFont));
            c7.HorizontalAlignment = Element.ALIGN_CENTER;
            payrollMasterTable.AddCell(c7);

            foreach (var sub in _deductions)
            {
                PdfPCell markCell = new PdfPCell(new Phrase(sub + "\nKshs", tHFont));
                markCell.HorizontalAlignment = Element.ALIGN_CENTER;
                payrollMasterTable.AddCell(markCell);
            }

            PdfPCell loanCell = new PdfPCell(new Phrase("Loans\nKshs", tHFont));
            loanCell.HorizontalAlignment = Element.ALIGN_CENTER;
            payrollMasterTable.AddCell(loanCell);

            PdfPCell saccoCell = new PdfPCell(new Phrase("Saccos \nContributions \nKshs", tHFont));
            saccoCell.HorizontalAlignment = Element.ALIGN_CENTER;
            payrollMasterTable.AddCell(saccoCell);

            PdfPCell c12 = new PdfPCell(new Phrase("Total \nDeductions\nKshs", tHFont));
            c12.HorizontalAlignment = Element.ALIGN_CENTER;
            payrollMasterTable.AddCell(c12);

            PdfPCell c13 = new PdfPCell(new Phrase("Net Pay\nKshs", tHFont));
            c13.HorizontalAlignment = Element.ALIGN_CENTER;
            payrollMasterTable.AddCell(c13);
        }

        private void BodAddTableDetail(DAL.psuedovwPayrollMaster rec, PdfPTable payrollMasterTable)
        {
            PdfPCell enoCell = new PdfPCell(new Phrase(rec.EmpNo, helv8Font));
            enoCell.HorizontalAlignment = Element.ALIGN_LEFT;
            payrollMasterTable.AddCell(enoCell);

            PdfPCell enameCell = new PdfPCell(new Phrase(rec.Surname + ",  " + rec.OtherNames, helv8Font));
            enameCell.HorizontalAlignment = Element.ALIGN_LEFT;
            payrollMasterTable.AddCell(enameCell);

            foreach (var sub in this._earnings)
            {
                var _rec = rec.GetMainEarnings.Where(s => s.Description == sub).SingleOrDefault();
                if (_rec != null)
                {
                    decimal item = decimal.Parse(_rec.Amount.ToString());
                    PdfPCell markCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", item), helv8Font));
                    markCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
                else
                {
                    PdfPCell markCell = new PdfPCell(new Phrase("0", helv8Font));
                    markCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
            }

            PdfPCell t1Cell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", rec.GrossTaxableEarnings), helv8Font));
            t1Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(t1Cell);

            foreach (var sub in this._deductions)
            {
                var _rec = rec.GetOtherDeductions.Where(s => s.Description == sub).SingleOrDefault();
                if (_rec != null)
                {
                    decimal item = decimal.Parse(_rec.Amount.ToString());
                    PdfPCell markCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", item), helv8Font));
                    markCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
                else
                {
                    PdfPCell markCell = new PdfPCell(new Phrase("0", helv8Font));
                    markCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
            }

            PdfPCell loanCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", rec.TotalLoansDeductions), helv8Font));
            loanCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(loanCell);

            PdfPCell saccoCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", rec.TotalSACCODeductions), helv8Font));
            saccoCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(saccoCell);

            decimal tDeductions = rec.PayeTax + rec.NHIF + rec.NSSF + rec.OtherDeductions;
            PdfPCell d5Cell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", tDeductions), helv8Font));
            d5Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d5Cell);

            PdfPCell d6Cell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", rec.NetPay), helv8Font));
            d6Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d6Cell);
        }

        private void AddDocBodyTableTotals(PdfPTable payrollMasterTable)
        {
            PdfPCell enoCell = new PdfPCell(new Phrase("TOTALS", tHFont));
            enoCell.Colspan = 2;
            enoCell.HorizontalAlignment = Element.ALIGN_LEFT;
            payrollMasterTable.AddCell(enoCell);

            foreach (var sub in this._earnings)
            {
                var totalOtherPayments = _ViewModel.paymaster.Sum(p => p.GetMainEarnings.Where(e => e.Description == sub).Sum(i => i.Amount));

                if (totalOtherPayments != null)
                {
                    decimal item = decimal.Parse(totalOtherPayments.ToString());
                    PdfPCell markCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", item), helv8Font));
                    markCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
                else
                {
                    PdfPCell markCell = new PdfPCell(new Phrase("0", helv8Font));
                    markCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
            }

            decimal gp = _ViewModel.paymaster.Sum(a => a.GrossTaxableEarnings);
            PdfPCell tCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", gp), tHFont));
            tCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(tCell);

            foreach (var sub in this._deductions)
            {
                var totalOtherDeductions = _ViewModel.paymaster.Sum(p => p.GetOtherDeductions.Where(e => e.Description == sub).Sum(i => i.Amount));

                if (totalOtherDeductions != null)
                {
                    decimal item = decimal.Parse(totalOtherDeductions.ToString());
                    PdfPCell markCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", item), helv8Font));
                    markCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
                else
                {
                    PdfPCell markCell = new PdfPCell(new Phrase("0", helv8Font));
                    markCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    payrollMasterTable.AddCell(markCell);
                }
            }

            PdfPCell loanCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalLoansDeductions), tHFont));
            loanCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(loanCell);

            PdfPCell saccoCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalSACCODeductions), tHFont));
            saccoCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(saccoCell);

            decimal tDeductions = _ViewModel.paymaster.Sum(rec => rec.PayeTax + rec.NHIF + rec.NSSF + rec.OtherDeductions);
            PdfPCell d5Cell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", tDeductions), tHFont));
            d5Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d5Cell);

            PdfPCell d6Cell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.Salaries), tHFont));
            d6Cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(d6Cell);
        }

        private void AddFooter()
        {
            PdfPTable payrollMasterTable = new PdfPTable(2);
            payrollMasterTable.WidthPercentage = 50;
            payrollMasterTable.HorizontalAlignment = Element.ALIGN_LEFT;

            PdfPCell netsalarypayable = new PdfPCell(new Phrase("Please write the following cheques:", rms6Bold));
            //netsalarypayable.Border = PdfPCell.NO_BORDER;
            netsalarypayable.HorizontalAlignment = Element.ALIGN_LEFT;
            netsalarypayable.Colspan = 2;
            payrollMasterTable.AddCell(netsalarypayable);

            PdfPCell netsalaryverified = new PdfPCell(new Phrase("Salaries:", rms8Normal));
            //netsalaryverified.Border = PdfPCell.NO_BORDER;
            netsalaryverified.HorizontalAlignment = Element.ALIGN_LEFT;
            payrollMasterTable.AddCell(netsalaryverified);

            PdfPCell netsalaryverified1 = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.Salaries.ToString("#,##0")), rms8Normal));
            //netsalaryverified1.Border = PdfPCell.NO_BORDER;
            netsalaryverified1.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(netsalaryverified1);

            PdfPCell netsalaryauthorized = new PdfPCell(new Phrase("N.S.S.F(Employer Inclusive):       ", rms8Normal));
            //netsalaryauthorized.Border = PdfPCell.NO_BORDER;
            netsalaryauthorized.HorizontalAlignment = Element.ALIGN_LEFT;
            payrollMasterTable.AddCell(netsalaryauthorized);

            PdfPCell netsalaryauthorized1 = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.deductionNSSF.ToString("#,##0")), rms8Normal));
            //netsalaryauthorized1.Border = PdfPCell.NO_BORDER;
            netsalaryauthorized1.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(netsalaryauthorized1);

            PdfPCell shifCell = new PdfPCell(new Phrase("S.H.I.F :", rms8Normal));
            //shifCell.Border = PdfPCell.NO_BORDER;
            shifCell.HorizontalAlignment = Element.ALIGN_LEFT;
            payrollMasterTable.AddCell(shifCell);

            PdfPCell sgCell1 = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.SHIF.ToString("#,##0")), rms8Normal));
            //sgCell1.Border = PdfPCell.NO_BORDER;
            sgCell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(sgCell1);

            PdfPCell ahlCell = new PdfPCell(new Phrase("A.H.L :", rms8Normal));
            //ahlCell.Border = PdfPCell.NO_BORDER;
            ahlCell.HorizontalAlignment = Element.ALIGN_LEFT;
            payrollMasterTable.AddCell(ahlCell);

            PdfPCell sgCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.AHL.ToString("#,##0")), rms8Normal));
            //sgCell.Border = PdfPCell.NO_BORDER;
            sgCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(sgCell);

            PdfPCell pgCell = new PdfPCell(new Phrase("Paymaster General:", rms8Normal));
            //pgCell.Border = PdfPCell.NO_BORDER;
            pgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            payrollMasterTable.AddCell(pgCell);

            PdfPCell pgCell1 = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.PAYE.ToString("#,##0")), rms8Normal));
            //pgCell1.Border = PdfPCell.NO_BORDER;
            pgCell1.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(pgCell1);

            PdfPCell totalsCell = new PdfPCell(new Phrase("Totals:", rms8Normal));
            //totalsCell.Border = PdfPCell.NO_BORDER;
            totalsCell.HorizontalAlignment = Element.ALIGN_LEFT;
            payrollMasterTable.AddCell(totalsCell);

            PdfPCell totalsvalueCell = new PdfPCell(new Phrase(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0:N2}", (_ViewModel.Salaries + _ViewModel.deductionNSSF + _ViewModel.NHIF + _ViewModel.PAYE).ToString("#,##0")), rms8Normal));
            //totalsvalueCell.Border = PdfPCell.NO_BORDER;
            totalsvalueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            payrollMasterTable.AddCell(totalsvalueCell);

            document.Add(payrollMasterTable);
        }

        private void AddDocFooter()
        {
            PdfPTable payrollMasterTable = new PdfPTable(1);
            payrollMasterTable.WidthPercentage = 100;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal));
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT;
            sgCell.Border = PdfPCell.NO_BORDER;
            payrollMasterTable.AddCell(sgCell);

            document.Add(payrollMasterTable);
        }
    }
}


