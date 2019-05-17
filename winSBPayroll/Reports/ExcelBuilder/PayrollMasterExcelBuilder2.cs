using System;
using System.IO;
using System.Linq;
using BLL.KRA;
using BLL;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Reports.Excel
{
    public class PayrollMasterExcelBuilder2
    {

        //private attributes 
        PayrollMasterModel _ViewModel;
        CreateExcelDoc document;
        string Message;
        string sFileExcel;
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        //constructor
        public PayrollMasterExcelBuilder2(PayrollMasterModel payrollMasterModel, string FileName, string Conn)
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

            _ViewModel = payrollMasterModel;
            sFileExcel = FileName;
        }

        public string GetExcel()
        {
            BuildExcel();
            document.Save(sFileExcel);
            return sFileExcel;
        }

        /***Build the report***/
        private void BuildExcel()
        {
            // step 1: creation of a document-object
            document = new CreateExcelDoc();

            try
            {
                // we Add a Header 
                int row = 1; int col = 1;

                AddDocHeader(ref row, ref col);

                AddDocBody(ref row, ref col);

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


        /*Build the document **/

        private void AddDocHeader(ref int row, ref int col)
        {
            //createHeaders(int row, int col, string htext, string cell1, string cell2, int mergeColumns, string b, bool font, int size, string fcolor)
            col = 2; row = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.employername, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.employeraddress, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "MASTER PAYROLL", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.ReportName, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
        }

        private void AddDocBody(ref int row, ref int col)
        {
            //Put table headers
            BodyAddtableHeaders(ref  row, ref  col);

            //Put table detail

            foreach (var d in _ViewModel.paymaster)
            {
                BodAddTableDetail(d, ref  row, ref  col);

            }

            //put table footer
            AddDocBodyTableTotals(ref  row, ref  col);

        }



        private void BodyAddtableHeaders(ref int row, ref int col)
        {
            //row 1
            row = row + 2; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "No", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Name", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Bank", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Department", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Basic Pay\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

           
            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Gross Pay\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "PAYE \n(Net Tax)\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "NSSF\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "NHIF\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Other \nDeductions\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Total \nDeductions\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Net Pay\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
        }



        private void BodAddTableDetail(DAL.psuedovwPayrollMaster rec, ref int row, ref int col)
        {
            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.EmpNo, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.Surname.Trim() + ",  " + rec.OtherNames.Trim(), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.BankName + " - " + rec.BranchName, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.Department, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.BasicPay.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            
            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.GrossTaxableEarnings.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.PayeTax.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.NSSF.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.NHIF.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.OtherDeductions.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            decimal tDeductions = rec.PayeTax + rec.NHIF + rec.NSSF + rec.OtherDeductions;
            document.createHeaders(row, col, tDeductions.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, rec.NetPay.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }


        private void AddDocBodyTableTotals(ref int row, ref int col)
        {
            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "TOTALS", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

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
            document.createHeaders(row, col, _ViewModel.TotalBasicPay.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            decimal gp = _ViewModel.paymaster.Sum(a => a.GrossTaxableEarnings);
            document.createHeaders(row, col, gp.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.PAYE.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.NSSF.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.NHIF.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.paymaster.Sum(rec => rec.OtherDeductions).ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            decimal tDeductions = _ViewModel.paymaster.Sum(rec => rec.PayeTax + rec.NHIF + rec.NSSF + rec.OtherDeductions);
            document.createHeaders(row, col, tDeductions.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.Salaries.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

        //document footer
        private void AddDocFooter(ref int row, ref int col)
        {

            row = row + 2; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Please write the following cheques: ", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row = row + 2; col = 1;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Salaries             :       " + _ViewModel.Salaries.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++; col = 1;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "N.S.S.F(Employer Inclusive)  :       " + _ViewModel.deductionNSSF.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            row++; col = 1;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "N.H.I.F              :       " + _ViewModel.NHIF.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++; col = 1;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Paymaster General    :       " + _ViewModel.PAYE.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++; col = 1;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Totals    :       " + (_ViewModel.Salaries + _ViewModel.deductionNSSF + _ViewModel.NHIF + _ViewModel.PAYE).ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }
    }
}