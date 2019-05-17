using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class vwPayslipDet_TempModel
    {
        public string EmpNo { get; set; }

        public string Surname { get; set; }

        public string OtherNames { get; set; }

        public string ItemId { get; set; }

        public decimal Balance { get; set; }

        public DateTime TxnDate { get; set; }

        public int ReFField { get; set; }

        public decimal RepayAmount { get; set; }

        public DateTime PostDate { get; set; }

        public decimal InitialAmount { get; set; }

        public string LoanType { get; set; }

        public int EmployerId { get; set; }

        public string BankAccount { get; set; }

        public string BankCode { get; set; }

        public string PaymentMode { get; set; }

        public string ItemTypeId { get; set; }

        public string Parent { get; set; }

        public int EmpTxnId { get; set; }

        public int Period { get; set; }

        public int Year { get; set; }

        public string Description { get; set; }

        public string TaxTracking { get; set; }

        public decimal Amount { get; set; }

        public string DEType { get; set; }

        public bool IsStatutory { get; set; }

        public bool ShowInPayslip { get; set; }

        public decimal YTD { get; set; } 

    }
}
