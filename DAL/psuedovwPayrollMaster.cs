using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class psuedovwPayrollMaster
    {
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string BankSortCode { get; set; }
        public string EmpNo { get; set; }
        public int EmployeeId { get; set; }
        public string Surname { get; set; }
        public decimal OtherDeductions { get; set; }
        public string OtherNames { get; set; }
        public string BankAccount { get; set; }
        public decimal NetPay { get; set; }
        public string Department { get; set; }
        public decimal GrossTaxableEarnings { get; set; }
        public decimal BasicPay { get; set; }
        public decimal NHIF { get; set; }
        public decimal NSSF { get; set; }
        public decimal EmployerNSSF { get; set; }
        public string EmployerName { get; set; }
        public decimal PayeTax { get; set; }
        public decimal PensionEmployee { get; set; }
        public string PaymentMode { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }
        public string IDNo { get; set; }
        public string ItemId { get; set; }
        public string PINNo { get; set; }
        public string NHIFNo { get; set; }
        public string NSSFNo { get; set; }
        public decimal Benefits { get; set; }
        public decimal Balance { get; set; }
        public decimal MonthAmount { get; set; }
        public decimal YTD { get; set; }
        public decimal Variables { get; set; }
        public decimal NetTaxableEarnings { get; set; }
        public decimal MortgageRelief { get; set; }
        public decimal PersonalRelief { get; set; }
        public decimal GrossTax { get; set; }
        public List<EarningsDeductions> EmployeeEarnings { get; set; }
        public List<EarningsDeductions> EmployeeDeductions { get; set; }
        public List<EarningsDeductions> AllOtherDeductions { get; set; }
        public List<EarningsDeductions> ExcludeBasicPayments { get; set; }
        public List<EarningsDeductions> GetMainEarnings
        {
            get
            {
                return this.EmployeeEarnings.Where(e => e.TaxTracking.Trim().Equals("EARNING") || e.ItemType.Trim() == "SALARY" || e.Description.Trim() == "NON_CASH_BENEFIT" || e.Description.Trim() == "HOURLY_PAY").ToList();
            }
        }
        public List<EarningsDeductions> GetOtherEarnings
        {
            get
            {
                return this.EmployeeEarnings.Where(e => e.TaxTracking.Trim().Equals("EARNING"))
                    .Where((e => e.ItemType.Trim() != "SALARY")).Where((e => e.Description.Trim() != "NON_CASH_BENEFIT")).Where((e => e.Description.Trim() != "HOURLY_PAY")).ToList();
            }
        }
        public decimal TotalOtherPayments
        {
            get
            {
                return GetMainEarnings.Sum(e => e.Amount);
            }
        }
        public List<EarningsDeductions> SACCODeductions
        {
            get
            {
                return this.EmployeeDeductions.Where(d => d.ItemType.Trim().Equals("SACCO")).ToList();
            }
        }
        public decimal TotalSACCODeductions
        {
            get
            {
                return SACCODeductions.Sum(e => e.Amount);
            }
        }
        public List<EarningsDeductions> LoansDeductions
        {
            get
            {
                return this.EmployeeDeductions.Where(d => d.ItemType.Trim().Equals("LOAN")).ToList();
            }
        }
        public decimal TotalLoansDeductions
        {
            get
            {
                return LoansDeductions.Sum(e => e.Amount);
            }
        }
        public List<EarningsDeductions> GetMainDeductions
        {
            get
            {
                return EmployeeDeductions.Where(d => d.ItemType.ToUpper().Trim().Contains("EMPECONTR") || d.ItemType.ToUpper().Trim().Contains("STATUTORY") || d.ItemType.ToUpper().Trim().Contains("TAX")).ToList();
            }
        }
        public List<EarningsDeductions> GetOtherDeductions
        {
            get
            {
                return EmployeeDeductions.Where(d => d.ItemType.ToUpper().Trim().Contains("EMPECONTR") || d.ItemType.ToUpper().Trim().Contains("STATUTORY") || d.ItemType.ToUpper().Trim().Contains("TAX") || !d.ItemType.ToUpper().Trim().Contains("LOAN") || !d.ItemType.ToUpper().Trim().Contains("SACCO")).ToList();
            }
        }


    }






    public class psuedovwPayslipDetails
    {
        public string EmpNo { get; set; }
        public string Surname { get; set; }
        public string OtherNames { get; set; }
        public string ItemId { get; set; }
        public decimal Balance { get; set; }
        public int ReFField { get; set; }
        public decimal RepayAmount { get; set; }
        public int Period { get; set; }
        public int Year { get; set; }
        public DateTime TxnDate { get; set; }
        public DateTime PostDate { get; set; }
        public string Description { get; set; }
        public decimal YTD { get; set; }
        public decimal InitialAmount { get; set; }
        public string LoanType { get; set; }
        public int Employer { get; set; }
        public string BankAccount { get; set; }
        public string BankCode { get; set; }
        public string PaymentMode { get; set; }
        public string ItemType { get; set; }
        public string DEType { get; set; }
        public int EmpTxnId { get; set; }
        public string TaxTracking { get; set; }
        public bool ShowInPayslip { get; set; }
        public bool IsStatutory { get; set; }
        public decimal Amount { get; set; }
        public string Parent { get; set; }
      
    }


    public class ReportsEngineCompleteEventArg : System.EventArgs
    {
        private int svalue;
        private string _template;

        public ReportsEngineCompleteEventArg(int value, string template)
        {
            svalue = value;
            _template = template;
        }

        public int StatusValue
        {
            get { return svalue; }
        }

        public string _Template
        {
            get { return _template; }
        }
    }
}
