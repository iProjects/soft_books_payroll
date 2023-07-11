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
    public class StatementExcelBuilder
    {
        //private attributes 
        StatementModel _ViewModel;
        CreateExcelDoc document;
        string Message;
        string sFileExcel;
        DataEntry de;
        string _Itemid;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        //constructor
        public StatementExcelBuilder(string Itemid, StatementModel statementmodel, string FileName, string Conn, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (statementmodel == null)
                throw new ArgumentNullException("StatementModel is null");
            _ViewModel = statementmodel;

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _notificationmessageEventname = notificationmessageEventname;

            if (Itemid == null)
                throw new ArgumentNullException("Itemid is null");
            _Itemid = Itemid;

            sFileExcel = FileName;
        }

        public string GetExcel()
        {
            Build();
            document.Save(sFileExcel);
            return sFileExcel;
        }

        /*Build the document **/
        private void Build()
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
            document.createHeaders(row, col, _ViewModel.employername, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.employeraddress, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "STATEMENT FOR :", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.employeename.Trim(), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.itemid.ToString().ToUpper().Trim(), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");


            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.ReportName, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Printed on: " + _ViewModel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

        //document body
        private void AddDocBody(ref int row, ref int col)
        {
            switch (_Itemid.ToString().Trim())
            {
                case "NSSF":
                    AddNSSFTableBody(ref  row, ref  col);
                    break;
                default:
                    AddTableBody(ref  row, ref  col);
                    break;
            }
        }
        private void AddNSSFTableBody(ref int row, ref int col)
        {
            //Add table headers
            AddNSSFTableHeaders(ref  row, ref  col);

            AddNSSFTableDetails(ref  row, ref  col);

            AddNSSFTableTotals(ref  row, ref  col);

        }
        private void AddTableBody(ref int row, ref int col)
        {

            AddTableHeaders(ref  row, ref  col);

            AddTableDetails(ref  row, ref  col);

            AddTableTotals(ref  row, ref  col);

        }
        //Nssf table headers
        private void AddNSSFTableHeaders(ref int row, ref int col)
        {
            //row 1
            row = row + 2; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "POST DATE", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "AMOUNT\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "BALANCE\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "EMPLOYER'S CONTRIBUTION\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "TOTALS\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }
        //table headers
        private void AddTableHeaders(ref int row, ref int col)
        {
            //row 1
            row = row + 2; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "POST DATE", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "AMOUNT\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "BALANCE\nKshs", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
        }
        //Nssf table details
        private void AddNSSFTableDetails(ref int row, ref int col)
        {

            foreach (var item in _ViewModel._Statementlist)
            {
                AddNSSFTableBodyDetails(item, ref  row, ref  col);
            }

        }
        //table details
        private void AddTableDetails(ref int row, ref int col)
        {
            foreach (var item in _ViewModel._Statementlist)
            {
                AddTableBodyDetails(item, ref  row, ref  col);
            }
        }
        //Nssf table Body details
        private void AddNSSFTableBodyDetails(StatementDTO det, ref int row, ref int col)
        {
            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.date.ToString("dd-MMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.Amountin.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.Balance.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.EmpNSSFContrib.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.TotalContribs.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
        }
        //table  Body details
        private void AddTableBodyDetails(StatementDTO det, ref int row, ref int col)
        {
            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.date.ToString("dd-MMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.Amountin.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, det.Balance.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
        }
        //Nssf table totals
        private void AddNSSFTableTotals(ref int row, ref int col)
        {
            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "TOTAL", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

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
            document.createHeaders(row, col, _ViewModel.totalContributions.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }
        //table totals
        private void AddTableTotals(ref int row, ref int col)
        {

            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "TOTAL", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _ViewModel.totalamountin.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
        }
        //document footer
        private void AddDocFooter(ref int row, ref int col)
        {
            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Signature.....................................................................................................", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

    }

}