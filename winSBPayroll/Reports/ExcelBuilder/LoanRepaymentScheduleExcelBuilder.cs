using System;
using System.IO;
using BLL.KRA.Models;
using winSBPayroll.Reports.Excel;
using BLL;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Reports.ExcelBuilder
{
    public class LoanRepaymentScheduleExcelBuilder
    {
        //private attributes 
        LoanRepaymentScheduleModel _loanrepaymentshedulemodel;
        CreateExcelDoc document;
        string Message;
        string sFileExcel;


        //constructor
        public LoanRepaymentScheduleExcelBuilder(LoanRepaymentScheduleModel loanrepaymentshedulemodel, string FileName)
        {
            _loanrepaymentshedulemodel = loanrepaymentshedulemodel;
            sFileExcel = FileName;
        }

        public string GetExcel()
        {
            BuildloanrepaymentsheduleExcel();
            document.Save(sFileExcel);
            return sFileExcel;
        }

        /*Build the document **/
        private void BuildloanrepaymentsheduleExcel()
        {
            // step 1: creation of a document-object
            document = new CreateExcelDoc();

            try
            {
                //Add  Header 
                int row = 1; int col = 1;
                AddDocHeader(ref row, ref col);

                //Add  Body
                AddDocBody(ref row, ref col);

                //Add Footer
                AddDocFooter(ref row, ref col);

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

        /*Build the document**/

        private void AddDocHeader(ref int row, ref int col)
        {
            //createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor)

            col = 2; row = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _loanrepaymentshedulemodel.employername, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _loanrepaymentshedulemodel.employeraddress, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "LOANS REPAYMENT", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");


            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _loanrepaymentshedulemodel.ReportName, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Printed on: " + _loanrepaymentshedulemodel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }


        private void AddDocBody(ref int row, ref int col)
        {
            //Add table headers
            AddBodytableHeaders(ref  row, ref  col);

            //Add table detail
            foreach (var d in _loanrepaymentshedulemodel.loanslist)
            {
                AddBodyTableDetail(d, ref  row, ref  col);

            }

            //Add table footer
            AddDocBodyTableTotals(ref  row, ref  col);

        }

        //table headers
        private void AddBodytableHeaders(ref int row, ref int col)
        {
            //row 1
            row = row + 2; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "EMPLOYEE NO", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "EMPLOYEE NAME", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "DESCRIPTION", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "START DATE", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "MONTH AMOUNT", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "BALANCE", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");


        }

        //table details
        private void AddBodyTableDetail(loanrepayments tr, ref int row, ref int col)
        {

            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, tr.employeenumber, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, tr.employeename, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, tr.loandescription, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, tr.startdate.ToString("dd-MMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            col++;

            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, tr.monthamount.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            col++;

            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, tr.balance.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            col++;




        }

        //table footer
        private void AddDocBodyTableTotals(ref int row, ref int col)
        {
            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Total Amount", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _loanrepaymentshedulemodel.TotalMonthAmount.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _loanrepaymentshedulemodel.TotalBalance.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

        //document footer
        private void AddDocFooter(ref int row, ref int col)
        {


        }
    }
}