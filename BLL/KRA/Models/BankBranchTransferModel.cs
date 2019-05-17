using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.KRA.Models
{
    public class BankTransferModel
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

        public List<BankTransferItem> BankTransferItems { get; set; }

        public decimal GrandTotal
        {
            get
            {

                return BankTransferItems.Sum(t => t.Total);
            }
        }        
    }

    public class BankTransferItem
    {
        public BankTransferItem() { }
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
    public class TransferItem
    {
        public TransferItem() { }
        public string EmpNo { get; set; }
        public string EmpName { get; set; }
        public string BankSortCode { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public decimal Amount { get; set; }
    }

    
}
