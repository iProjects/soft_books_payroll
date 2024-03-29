﻿using System;
using System.IO;
using BLL.KRA.Models;
using winSBPayroll.Reports.Excel;
using BLL;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Reports.ExcelBuilder
{
    public class EmployeeExcelBuilder
    {


        //private attributes 
        EmployeesModelReport _emreportmodel;
        CreateExcelDoc document;
        string Message;
        string sFileExcel;


        //constructor
        public EmployeeExcelBuilder(EmployeesModelReport emreportmodel, string FileName)
        {
            _emreportmodel = emreportmodel;
            sFileExcel = FileName;
        }

        public string GetExcel()
        {
            BuildEmployeeExcel();
            document.Save(sFileExcel);
            return sFileExcel;
        }


        /*Build the document **/
        private void BuildEmployeeExcel()
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
            document.createHeaders(row, col, _emreportmodel.employername, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _emreportmodel.employeraddress, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "EMPLOYEES REPORT", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            row++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "Printed on: " + _emreportmodel.PrintedOn.ToString("dd-dddd-MMMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");


          
        }


        private void AddDocBody(ref int row, ref int col)
        {
            //Add table headers
            AddBodytableHeaders(ref  row, ref  col);

            //Add table detail
            foreach (var d in _emreportmodel.pae)
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
            document.createHeaders(row, col, "GENDER", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "PIN NUMBER", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "ID NUMBER", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "DEPARTMENT", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "DATE OF EMPLOYMENT", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "BASIC SALARY", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "PAYMENT MODE", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");


            

        }

        //table details
        private void AddBodyTableDetail(printallemployees paemp, ref int row, ref int col)
        {

            row++; col = 1;
            string cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, paemp.employeenumber, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, paemp.employeename, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, paemp.gender, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, paemp.pinnumber, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, paemp.idnumber, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, paemp.department, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, paemp.dateofemployment.ToString("dd-MMM-yyyy"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, paemp.basicpay.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, paemp.paymentmode, cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        

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
            document.createHeaders(row, col, "", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, "", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            
            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col, _emreportmodel.totalbasic.ToString("#,##0"), cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");
            
            col++;
            cellrangeaddr1 = document.IntAlpha(col) + row;
            document.createHeaders(row, col,"", cellrangeaddr1, cellrangeaddr1, 0, "WHITE", true, 10, "n");

        }

        //document footer
        private void AddDocFooter(ref int row, ref int col)
        {

            
        }

    }
}
