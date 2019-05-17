using System;
using System.IO;
using BLL.KRA.Models;
using BLL;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Reports.Excel
{
    public class BankTransferExcelBuilder
    {
      
        #region "private attributes"
        BankTransferReportModel _bbtreportmodel;
        CreateExcelDoc document;
        string Message;
        string sFileExcel;
        #endregion "private attributes"

        #region "Constructor"
        public BankTransferExcelBuilder(BankTransferReportModel bbtreportmodel, string FileName)
        {
            _bbtreportmodel = bbtreportmodel;
            sFileExcel = FileName;
        }
        #endregion "Constructor"

        public string GetExcel()
        {
            BuildBankBranchTransferExcel();
            document.Save(sFileExcel);
            return sFileExcel;
        }

        /*Build the document **/
        private void BuildBankBranchTransferExcel()
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
            document.createHeaders(row, col, _bbtreportmodel.employername, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _bbtreportmodel.employeraddress, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _bbtreportmodel.EmployerTelephone, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Printed On: " + _bbtreportmodel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            //Bank address
            
            row = row + 2;col = 1;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "THE  MANAGER", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _bbtreportmodel.Bank, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _bbtreportmodel.BankBranch, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

          
            //Salutation
            row = row + 2; col = 1;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Dear Sir / Madam", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row = row + 2;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "RE: CREDIT TRANSFER - SALARY PROCESSING", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row = row + 2;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Below is a list of Names, Banks, Branches, Accounts and Amounts to be credited in their respective accounts", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

        private void AddDocBody(ref int row, ref int col)
        {

            foreach (var bankTransferItem in _bbtreportmodel.BankTransferItems)
            {
                AddBodyTable(bankTransferItem, ref  row, ref  col);
            }
            AddTableTotals(ref  row, ref  col);
        }

        //table details
        private void AddBodyTable(BankTransferItem bti, ref int row, ref int col)
        {
            row = row + 2; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Bank Name : " + bti.BankName, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            //add table header
            AddBodytableHeaders(ref row, ref col);

            //add table details
            foreach (var ti in bti.TransferItems)
            {
                AddDocBodyTableDetails(ti, ref  row, ref  col);
            }

            AddDocBodyTableTotals(bti, ref  row, ref  col);
        }

        //table headers
        private void AddBodytableHeaders(ref int row, ref int col)
        {
            //row 1
            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "No", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Name", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Branch", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Account No", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Amount\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
        }

        //table details
        private void AddDocBodyTableDetails(TransferItem det, ref int row, ref int col)
        {
            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.EmpNo, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.EmpName, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.BranchName, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.AccountNo, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.Amount.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");


        }

        //table totals
        private void AddDocBodyTableTotals(BankTransferItem bti, ref int row, ref int col)
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
            document.createHeaders(row, col, bti.Total.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

        //add table totals
        public void AddTableTotals(ref int row, ref int col)
        {
            row = row + 2; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Grand Total = ", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _bbtreportmodel.GrandTotal.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

        //document footer
        private void AddDocFooter(ref int row, ref int col)
        {

            row = row + 2; ; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            string s = "Kindly credit the Accounts with the amounts specified on the above date and debit Account: ";
            s = s + _bbtreportmodel.AccountName + " Account No: " + _bbtreportmodel.AccountNo;
            document.createHeaders(row, col, s, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            s = "Yours faithfully";
            document.createHeaders(row, col, s, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row = row + 2;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Account Signatory..." + _bbtreportmodel.AccountSignatory, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            s = "FOR... " + _bbtreportmodel.employername;
            document.createHeaders(row, col, s, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }





    }
}
