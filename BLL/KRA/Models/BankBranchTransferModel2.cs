using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.KRA.Models
{
    public class BankTransferModel2
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public string employername { get; set; }
        public string employeraddress { get; set; }
        public string EmployerTelephone { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; } 
        public string ReportName
        {
            get
            {
                return "BANK BRANCHES TRANSFER - SALARIES \nFor The Period  " + new DateTime(Year, Period, 1).ToString("MMM-yyyy");
            }
        }
        public string Bank { get; set; }
        public string BankBranch { get; set; }
        public string bankcode { get; set; }
        public DateTime PrintedOn { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
        public string AccountSignatory { get; set; }

        public List<BankTransferItem2> BankTransferItems { get; set; }
        public List<EmployerBankTransferItem2> EmployerBankTransferItems { get; set; }
        public decimal EmployerGrandTotal
        {
            get
            {

                return EmployerBankTransferItems.Sum(t => t.Total);
            }
        }  
        public decimal GrandTotal
        {
            get
            {

                return BankTransferItems.Sum(t => t.Total);
            }
        }        
    }
    public class EmployerBankTransferItem2
    {
        public EmployerBankTransferItem2() { }
        public int EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string _BankSortCode { get; set; }
        public string EmployerBankName { get; set; }
        public List<BankTransferItem> EmployerBankTransferItems { get; set; }
        public decimal Total
        {
            get
            {
                return EmployerBankTransferItems.Sum(i => i.Total);
            }
        }
    }
    public class BankTransferItem2
    {
        public BankTransferItem2() { }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public List<TransferItem> TransferItems { get; set; }
        public decimal Total
        {
            get
            {
                return TransferItems.Sum(i => i.Amount);
            }
        }
    }
    public class TransferItem2
    {
        public TransferItem2() { }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string BankSortCode { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public decimal Amount { get; set; }
    }

    
}
