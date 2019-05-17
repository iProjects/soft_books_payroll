using System;
using System.Net; 

namespace DAL
{
    public class EmployerBanksModel
    {
        public int Id { get; set; }

        public int EmployerBankId { get; set; }

        public int? EmployerId { get; set; }

        public string BankSortCode { get; set; }

        public string BankBranchName { get; set; }

        public string AccountName { get; set; }

        public string AccountNo { get; set; }

        public string Signatory { get; set; }

        public bool? IsDefault { get; set; } 
         
    }

    public class EmployeesBankTransfersModel
    {
        public int Id { get; set; }

        public int? EmployerBankId { get; set; } 

        public int? EmployerId { get; set; }

        public string BankSortCode { get; set; }

        public string BankBranchName { get; set; }

        public string AccountName { get; set; }

        public string AccountNo { get; set; }

        public string Signatory { get; set; }

        public bool? IsDefault { get; set; }

        public string EmpNo { get; set; }

        public int? EmployeeId { get; set; }
    }
}
