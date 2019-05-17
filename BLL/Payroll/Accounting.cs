using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using DAL;

namespace BLL.Payroll
{
    class Accounting
    {
        private Repository rep = new Repository();


        public void PostAccount(string EmpNo, string TxnCode, decimal Amount, string TxnType)
        {
            if (EmpNo == "")
            {
                return;
            }

            if (TxnCode == "")
            {
                return;
            }

            if (TxnType == "")
            {
                return;
            }

            if (TxnType == "D")
            {
                Amount = Amount * -1;
            }
            /*
            
            string sError = "";
            da.GetDBResults(ref sError, "UpdateAccount",
              "@EmpNo", EmpNo,
              "@TxnCode", TxnCode,
              "@Amount", Amount
              );

            if (sError != "") {throw new Exception(sError); }
            */
        }
        /*
        public decimal GetAccountBal(string EmpNo, string Account)
        {
            decimal bal = 0.00M;
            if (EmpNo == "")
            {
                return bal;
            }

            if (Account == "")
            {
                return bal;
            }

            
            string sError = "";
            DbDataReader dr =  da.GetDBResults(ref sError, "GetAccounntBal",
              "@EmpNo", EmpNo,
              "@Account", Account
              );

            if (sError != "") { throw new Exception(sError); }

            try
            {
                dr.Read();
                return (decimal )dr["Balance"];
            }
            catch(Exception ex)
            { 
                throw ex; 
            }
               
        }

        public void OpenAccount(string EmpNo, string Account, string Description, string AccType)
        {
            string sError = "";
            da.GetDBResults(ref sError, "OpenAccount",
              "@Account", Account,
              "@Description", Description ,
              "@EmpNo", EmpNo,
              "@AccountType", AccType 
              );

            if (sError != "") { throw new Exception(sError); }

        }
        */
    }

}
