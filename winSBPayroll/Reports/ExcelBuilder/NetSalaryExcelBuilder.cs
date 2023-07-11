﻿using System;
using System.IO;
using BLL.KRA;
using winSBPayroll.Reports.Excel;
using BLL;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Reports.ExcelBuilder
{
    public class NetSalaryExcelBuilder
    {
        //private attributes 
        NetSalaryReportModel _netsalaryreportmodel;
        CreateExcelDoc document;
        string Message;
        string sFileExcel;
        event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        //constructor
        public NetSalaryExcelBuilder(NetSalaryReportModel netsalaryreportmodel, string FileName, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            _notificationmessageEventname = notificationmessageEventname;

            _netsalaryreportmodel = netsalaryreportmodel;
            sFileExcel = FileName;
        }

        public string GetExcel()
        {
            BuildNetSalaryExcel();
            document.Save(sFileExcel);
            return sFileExcel;
        }


        /*Build the document **/
        private void BuildNetSalaryExcel()
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
            document.createHeaders(row, col, _netsalaryreportmodel.employername, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _netsalaryreportmodel.employeraddress, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "NET SALARY", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _netsalaryreportmodel.ReportName, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Printed on: " + _netsalaryreportmodel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");


        }


        private void AddDocBody(ref int row, ref int col)
        {
            //Add table headers
            AddBodytableHeaders(ref  row, ref  col);

            //Add table detail
            foreach (var d in _netsalaryreportmodel.paymaster)
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
            document.createHeaders(row, col, "EMPLOYEE PIN", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "NET SALARY", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");



        }

        //table details
        private void AddBodyTableDetail(DAL.psuedovwPayrollMaster pay, ref int row, ref int col)
        {

            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, pay.EmpNo, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, pay.Surname.Trim() + " " + pay.OtherNames.Trim(), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, pay.PINNo, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, pay.NetPay.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");



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
            document.createHeaders(row, col, _netsalaryreportmodel.totalamount.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
        }

        //document footer
        private void AddDocFooter(ref int row, ref int col)
        {

        }

    }
}
