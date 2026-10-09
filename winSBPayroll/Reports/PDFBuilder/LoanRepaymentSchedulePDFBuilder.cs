using System;
using System.Globalization;
using System.IO;
using BLL;
using BLL.DataEntry;
// Payroll
using BLL.KRA.Models;
using CommonLib;
using DAL;
// --- Add the following to make itext work
using iTextSharp.text;
using iTextSharp.text.pdf;
using VVX;

namespace winSBPayroll.Reports.PDFBuilder
{
    public class LoanRepaymentSchedulePDFBuilder
    {
        LoanRepaymentScheduleModel _ViewModel;
        Document document;
        string Message;
        string sFilePDF;

        // DEFINED FONTS - Using standard Font constructors without BaseColor or FontFamily
        Font hFont1 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD); //[span_0](start_span)[span_0](end_span)
        Font hfont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD); //[span_1](start_span)[span_1](end_span)
        Font hFont2 = new Font(Font.TIMES_ROMAN, 10, Font.BOLD); //[span_2](start_span)[span_2](end_span)
        Font bfont1 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); // body[span_3](start_span)[span_3](end_span)
        Font bFont2 = new Font(Font.TIMES_ROMAN, 8, Font.BOLD); // body[span_4](start_span)[span_4](end_span)
        Font bFont3 = new Font(Font.TIMES_ROMAN, 12, Font.BOLD); // body[span_5](start_span)[span_5](end_span)
        Font tHFont = new Font(Font.TIMES_ROMAN, 9, Font.BOLD); // table Header[span_6](start_span)[span_6](end_span)
        Font tHfont1 = new Font(Font.TIMES_ROMAN, 11, Font.BOLD); // table Header[span_7](start_span)[span_7](end_span)
        Font tcFont = new Font(Font.HELVETICA, 8, Font.NORMAL); // table cell[span_8](start_span)[span_8](end_span)
        Font rms6Normal = new Font(Font.TIMES_ROMAN, 9, Font.NORMAL); //[span_9](start_span)[span_9](end_span)
        Font rms10Bold = new Font(Font.HELVETICA, 10, Font.BOLD); //[span_10](start_span)[span_10](end_span)
        Font rms6Bold = new Font(Font.TIMES_ROMAN, 10, Font.BOLD); //[span_11](start_span)[span_11](end_span)
        Font rms8Bold = new Font(Font.HELVETICA, 8, Font.BOLD); //[span_12](start_span)[span_12](end_span)
        Font rms9Bold = new Font(Font.HELVETICA, 9, Font.BOLD); //[span_13](start_span)[span_13](end_span)
        Font rms10Normal = new Font(Font.HELVETICA, 10, Font.NORMAL); //[span_14](start_span)[span_14](end_span)

        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public LoanRepaymentSchedulePDFBuilder(LoanRepaymentScheduleModel loanrepaymentshedulemodel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (loanrepaymentshedulemodel == null)
                throw new ArgumentNullException("LoanRepaymentScheduleModel is null"); //[span_15](start_span)[span_15](end_span)
            _ViewModel = loanrepaymentshedulemodel; //[span_16](start_span)[span_16](end_span)

            TAG = this.GetType().Name; //[span_17](start_span)[span_17](end_span)

            _notificationmessageEventname = notificationmessageEventname; //[span_18](start_span)[span_18](end_span)

            sFilePDF = FileName; //[span_19](start_span)[span_19](end_span)

            if (_notificationmessageEventname != null)
            {
                _notificationmessageEventname.Invoke(this, new notificationmessageEventArgs("Constructed LoanRepaymentSchedulePDFBuilder", TAG)); //[span_20](start_span)[span_20](end_span)
            }
        }

        public string GetshedulePDF()
        {
            BuildPDF(); //[span_21](start_span)[span_21](end_span)
            return sFilePDF; //[span_22](start_span)[span_22](end_span)
        }

        private void BuildPDF()
        {
            try
            {
                // step 1: creation of a document-object
                document = new Document(PageSize.A4.Rotate(), 20, 20, 20, 20); //[span_23](start_span)[span_23](end_span)

                // step 2: we create a writer that listens to the document
                PdfWriter.GetInstance(document, new FileStream(sFilePDF, FileMode.Create)); //[span_24](start_span)[span_24](end_span)

                document.Open(); //[span_25](start_span)[span_25](end_span)

                // document header
                AddDocHeader(); //[span_26](start_span)[span_26](end_span)

                // Main repayment schedule table created using PdfPTable to support recurring headers
                PdfPTable rTable = new PdfPTable(6);
                rTable.WidthPercentage = 100; //[span_27](start_span)[span_27](end_span)
                rTable.SpacingBefore = 10f;
                rTable.SetWidths(new float[] { 10f, 25f, 25f, 15f, 12.5f, 12.5f });

                // Add table headers and configure page repeating headers
                AddTableHeaders(rTable); //[span_28](start_span)[span_28](end_span)
                rTable.HeaderRows = 1;

                // Add table details
                foreach (var tr in _ViewModel.loanslist) //[span_29](start_span)[span_29](end_span)
                {
                    AddTableRow(rTable, tr); //[span_30](start_span)[span_30](end_span)
                }

                // Add totals
                AddTotals(rTable); //[span_31](start_span)[span_31](end_span)

                document.Add(rTable); //[span_32](start_span)[span_32](end_span)
                AddDocFooter(); //[span_33](start_span)[span_33](end_span)

                document.Close(); //[span_34](start_span)[span_34](end_span)
            }
            catch (DocumentException de)
            {
                Log.WriteToErrorLogFile(de); //[span_35](start_span)[span_35](end_span)
            }
            catch (IOException ioe)
            {
                Log.WriteToErrorLogFile(ioe); //[span_36](start_span)[span_36](end_span)
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex); //[span_37](start_span)[span_37](end_span)
            }
        }

        // document header
        private void AddDocHeader()
        {
            PdfPTable loanRePaymentsTable = new PdfPTable(5);
            loanRePaymentsTable.WidthPercentage = 100; //[span_38](start_span)[span_38](end_span)

            PdfPCell employernameCell = new PdfPCell(new Phrase(_ViewModel.employername.ToUpper(), new Font(Font.TIMES_ROMAN, 12, Font.BOLD | Font.UNDERLINE, Color.BLACK))); //[span_39](start_span)[span_39](end_span)
            employernameCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_40](start_span)[span_40](end_span)
            employernameCell.Colspan = 5; //[span_41](start_span)[span_41](end_span)
            employernameCell.Border = Rectangle.NO_BORDER; //[span_42](start_span)[span_42](end_span)
            loanRePaymentsTable.AddCell(employernameCell); //[span_43](start_span)[span_43](end_span)

            PdfPCell employeraddressCell = new PdfPCell(new Phrase(_ViewModel.employeraddress, new Font(Font.TIMES_ROMAN, 10, Font.BOLD | Font.UNDERLINE, Color.BLACK))); //[span_44](start_span)[span_44](end_span)
            employeraddressCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_45](start_span)[span_45](end_span)
            employeraddressCell.Colspan = 5; //[span_46](start_span)[span_46](end_span)
            employeraddressCell.Border = Rectangle.NO_BORDER; //[span_47](start_span)[span_47](end_span)
            loanRePaymentsTable.AddCell(employeraddressCell); //[span_48](start_span)[span_48](end_span)

            PdfPCell bCell = new PdfPCell(new Phrase("LOAN REPAYMENTS", hFont1)); //[span_49](start_span)[span_49](end_span)
            bCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_50](start_span)[span_50](end_span)
            bCell.Colspan = 5; //[span_51](start_span)[span_51](end_span)
            bCell.Border = Rectangle.NO_BORDER; //[span_52](start_span)[span_52](end_span)
            loanRePaymentsTable.AddCell(bCell); //[span_53](start_span)[span_53](end_span)

            PdfPCell reportNameCell = new PdfPCell(new Phrase(_ViewModel.ReportName, hFont2)); //[span_54](start_span)[span_54](end_span)
            reportNameCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_55](start_span)[span_55](end_span)
            reportNameCell.Colspan = 5; //[span_56](start_span)[span_56](end_span)
            reportNameCell.Border = Rectangle.NO_BORDER; //[span_57](start_span)[span_57](end_span)
            loanRePaymentsTable.AddCell(reportNameCell); //[span_58](start_span)[span_58](end_span)

            PdfPCell PrintedonCell = new PdfPCell(new Phrase("Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), hFont2)); //[span_59](start_span)[span_59](end_span)
            PrintedonCell.HorizontalAlignment = Element.ALIGN_LEFT; //[span_60](start_span)[span_60](end_span)
            PrintedonCell.Colspan = 4; //[span_61](start_span)[span_61](end_span)
            PrintedonCell.Border = Rectangle.NO_BORDER; //[span_62](start_span)[span_62](end_span)
            loanRePaymentsTable.AddCell(PrintedonCell); //[span_63](start_span)[span_63](end_span)

            // create the logo
            PDFGen pdfgen = new PDFGen(_notificationmessageEventname); //[span_64](start_span)[span_64](end_span)
            Image img0 = pdfgen.DoGetImageFile(_ViewModel.CompanyLogo); //[span_65](start_span)[span_65](end_span)

            PdfPCell logoCell = new PdfPCell();
            logoCell.Border = Rectangle.NO_BORDER; //[span_66](start_span)[span_66](end_span)
            logoCell.HorizontalAlignment = Element.ALIGN_LEFT; //[span_67](start_span)[span_67](end_span)

            if (img0 != null)
            {
                img0.Alignment = Image.ALIGN_MIDDLE; //[span_68](start_span)[span_68](end_span)
                logoCell.AddElement(img0); //[span_69](start_span)[span_69](end_span)
            }

            logoCell.AddElement(new Phrase(_ViewModel.CompanySlogan, new Font(Font.HELVETICA, 8, Font.ITALIC, Color.BLACK))); //[span_70](start_span)[span_70](end_span)
            loanRePaymentsTable.AddCell(logoCell); //[span_71](start_span)[span_71](end_span)

            document.Add(loanRePaymentsTable); //[span_72](start_span)[span_72](end_span)
        }

        private void AddTableHeaders(PdfPTable loanRePaymentsTable)
        {
            PdfPCell employeeNoCell = new PdfPCell(new Phrase("No", tHFont)); //[span_73](start_span)[span_73](end_span)
            employeeNoCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_74](start_span)[span_74](end_span)
            employeeNoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            employeeNoCell.Padding = 3f;
            loanRePaymentsTable.AddCell(employeeNoCell); //[span_75](start_span)[span_75](end_span)

            PdfPCell employeeNameCell = new PdfPCell(new Phrase("Name", tHFont)); //[span_76](start_span)[span_76](end_span)
            employeeNameCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_77](start_span)[span_77](end_span)
            employeeNameCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            employeeNameCell.Padding = 3f;
            loanRePaymentsTable.AddCell(employeeNameCell); //[span_78](start_span)[span_78](end_span)

            PdfPCell descriptionCell = new PdfPCell(new Phrase("Description", tHFont)); //[span_79](start_span)[span_79](end_span)
            descriptionCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_80](start_span)[span_80](end_span)
            descriptionCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            descriptionCell.Padding = 3f;
            loanRePaymentsTable.AddCell(descriptionCell); //[span_81](start_span)[span_81](end_span)

            PdfPCell startDateCell = new PdfPCell(new Phrase("Start Date", tHFont)); //[span_82](start_span)[span_82](end_span)
            startDateCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_83](start_span)[span_83](end_span)
            startDateCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            startDateCell.Padding = 3f;
            loanRePaymentsTable.AddCell(startDateCell); //[span_84](start_span)[span_84](end_span)

            PdfPCell mothAmountCell = new PdfPCell(new Phrase("Month Amount\nKshs", tHFont)); //[span_85](start_span)[span_85](end_span)
            mothAmountCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_86](start_span)[span_86](end_span)
            mothAmountCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            mothAmountCell.Padding = 3f;
            loanRePaymentsTable.AddCell(mothAmountCell); //[span_87](start_span)[span_87](end_span)

            PdfPCell balanceCell = new PdfPCell(new Phrase("Balance\nKshs", tHFont)); //[span_88](start_span)[span_88](end_span)
            balanceCell.HorizontalAlignment = Element.ALIGN_CENTER; //[span_89](start_span)[span_89](end_span)
            balanceCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            balanceCell.Padding = 3f;
            loanRePaymentsTable.AddCell(balanceCell); //[span_90](start_span)[span_90](end_span)
        }

        private void AddTableRow(PdfPTable loanRePaymentsTable, loanrepayments tr)
        {
            if (tr != null) //[span_91](start_span)[span_91](end_span)
            {
                PdfPCell B = new PdfPCell(new Phrase(tr.employeenumber, tcFont)); //[span_92](start_span)[span_92](end_span)
                B.HorizontalAlignment = Element.ALIGN_LEFT; //[span_93](start_span)[span_93](end_span)
                B.Padding = 2f;
                loanRePaymentsTable.AddCell(B); //[span_94](start_span)[span_94](end_span)

                PdfPCell C = new PdfPCell(new Phrase(tr.employeename, tcFont)); //[span_95](start_span)[span_95](end_span)
                C.HorizontalAlignment = Element.ALIGN_LEFT; //[span_96](start_span)[span_96](end_span)
                C.Padding = 2f;
                loanRePaymentsTable.AddCell(C); //[span_97](start_span)[span_97](end_span)

                PdfPCell D = new PdfPCell(new Phrase(tr.loandescription, tcFont)); //[span_98](start_span)[span_98](end_span)
                D.HorizontalAlignment = Element.ALIGN_LEFT; //[span_99](start_span)[span_99](end_span)
                D.Padding = 2f;
                loanRePaymentsTable.AddCell(D); //[span_100](start_span)[span_100](end_span)

                PdfPCell E = new PdfPCell(new Phrase(tr.startdate.ToString("dd-MMM-yyyy"), tcFont)); //[span_101](start_span)[span_101](end_span)
                E.HorizontalAlignment = Element.ALIGN_LEFT; //[span_102](start_span)[span_102](end_span)
                E.Padding = 2f;
                loanRePaymentsTable.AddCell(E); //[span_103](start_span)[span_103](end_span)

                PdfPCell F = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", tr.monthamount), tcFont)); //[span_104](start_span)[span_104](end_span)
                F.HorizontalAlignment = Element.ALIGN_RIGHT; //[span_105](start_span)[span_105](end_span)
                F.Padding = 2f;
                loanRePaymentsTable.AddCell(F); //[span_106](start_span)[span_106](end_span)

                decimal _tbalance = decimal.Parse(tr.balance.ToString()); //[span_107](start_span)[span_107](end_span)
                if (_tbalance < 0) //[span_108](start_span)[span_108](end_span)
                {
                    _tbalance = _tbalance * -1; //[span_109](start_span)[span_109](end_span)
                }
                PdfPCell G = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _tbalance), tcFont)); //[span_110](start_span)[span_110](end_span)
                G.HorizontalAlignment = Element.ALIGN_RIGHT; //[span_111](start_span)[span_111](end_span)
                G.Padding = 2f;
                loanRePaymentsTable.AddCell(G); //[span_112](start_span)[span_112](end_span)
            }
            else
            {
                PdfPCell ErCell = new PdfPCell(new Phrase("Payroll Must Be Closed!", tcFont)); //[span_113](start_span)[span_113](end_span)
                ErCell.HorizontalAlignment = Element.ALIGN_LEFT; //[span_114](start_span)[span_114](end_span)
                ErCell.Colspan = 6;
                ErCell.Padding = 2f;
                loanRePaymentsTable.AddCell(ErCell); //[span_115](start_span)[span_115](end_span)
            }
        }

        private void AddTotals(PdfPTable loanRePaymentsTable)
        {
            PdfPCell I = new PdfPCell(new Phrase("TOTAL", rms10Bold)); //[span_116](start_span)[span_116](end_span)
            I.HorizontalAlignment = Element.ALIGN_LEFT; //[span_117](start_span)[span_117](end_span)
            I.Colspan = 4; //[span_118](start_span)[span_118](end_span)
            I.Padding = 3f;
            loanRePaymentsTable.AddCell(I); //[span_119](start_span)[span_119](end_span)

            PdfPCell A = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _ViewModel.TotalMonthAmount), rms10Bold)); //[span_120](start_span)[span_120](end_span)
            A.HorizontalAlignment = Element.ALIGN_RIGHT; //[span_121](start_span)[span_121](end_span)
            A.Padding = 3f;
            loanRePaymentsTable.AddCell(A); //[span_122](start_span)[span_122](end_span)

            decimal _tbalance = decimal.Parse(_ViewModel.TotalBalance.ToString()); //[span_123](start_span)[span_123](end_span)
            if (_tbalance < 0) //[span_124](start_span)[span_124](end_span)
            {
                _tbalance = _tbalance * -1; //[span_125](start_span)[span_125](end_span)
            }
            PdfPCell V = new PdfPCell(new Phrase(string.Format(CultureInfo.InvariantCulture, "{0:N2}", _tbalance), rms10Bold)); //[span_126](start_span)[span_126](end_span)
            V.HorizontalAlignment = Element.ALIGN_RIGHT; //[span_127](start_span)[span_127](end_span)
            V.Padding = 3f;
            loanRePaymentsTable.AddCell(V); //[span_128](start_span)[span_128](end_span)
        }

        // document footer
        private void AddDocFooter()
        {
            PdfPTable loanRePaymentsTable = new PdfPTable(1);
            loanRePaymentsTable.WidthPercentage = 100; //[span_129](start_span)[span_129](end_span)
            loanRePaymentsTable.SpacingBefore = 20f;

            PdfPCell sgCell = new PdfPCell(new Phrase("Signature.....................................................................................................", rms10Normal)); //[span_130](start_span)[span_130](end_span)
            sgCell.HorizontalAlignment = Element.ALIGN_LEFT; //[span_131](start_span)[span_131](end_span)
            sgCell.Border = Rectangle.NO_BORDER; //[span_132](start_span)[span_132](end_span)
            loanRePaymentsTable.AddCell(sgCell); //[span_133](start_span)[span_133](end_span)

            document.Add(loanRePaymentsTable); //[span_134](start_span)[span_134](end_span)
        }
    }
}
