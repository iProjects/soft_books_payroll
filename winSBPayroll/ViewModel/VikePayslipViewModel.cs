using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace winSBPayroll.ViewModel
{
    public class VikePayslipViewModel
    {
        Payslip _Payslip;

        public VikePayslipViewModel(Payslip payslip)
        {
            _Payslip = payslip;
        }
        public string PayrollMonth
        {
            get
            {
                return _Payslip.PayPeriod.ToString("MMMM");
            }
        }
        public string PayrollYear
        {
            get
            {
                return _Payslip.PayPeriod.ToString("yyyy");
            }
        }
        public DateTime PrintedOn
        {
            get
            {
                return DateTime.Today;
            }
        }
        //employee
        public string EmpNo
        {
            get
            {
                return _Payslip.EmpNo;
            }
        }
        public string EmpName
        {
            get
            {
                return _Payslip.EmpName;
            }
        }
        public string PINNo
        {
            get
            {
                return _Payslip.PINNo;
            }
        }
        public string IDNo
        {
            get
            {
                return _Payslip.IDNo;
            }
        }
        public string NSSFNO
        {
            get
            {
                return _Payslip.NSSFNO;
            }
        }
        public string NHIFNO
        {
            get
            {
                return _Payslip.NHIFNO;
            }
        }
        public string Department
        {
            get
            {
                return _Payslip.Department;
            }
        }
        //employerbank
        public string EmployerName
        {
            get
            {
                return _Payslip.EmployerName;
            }
        }
        public string EmployerAddress
        {
            get
            {
                return _Payslip.EmployerAddress;
            }
        }
        public string EmployerTelephone
        {
            get
            {
                return _Payslip.EmployerTelephone;
            }
        }
        public string EmployerEmail
        {
            get
            {
                return _Payslip.EmployerEmail;
            }
        }
        //... 
        public decimal GrossTaxablePay
        {
            get
            {
                return _Payslip.GrossTaxableEarnings;
            }
        }
        public decimal GrossTaxableEarnings
        {
            get
            {
                return _Payslip.GrossTaxableEarnings;
            }
        }
        public decimal GROSS_PAY_AFTER_TAX
        {
            get
            {
                return _Payslip.GROSS_PAY_AFTER_TAX;
            }
        }

        public decimal NET_PAY_AFTER_DEDUCTIONS
        {
            get
            {
                return _Payslip.NET_PAY_AFTER_DEDUCTIONS;
            }
        }

        public decimal NET_PAY_TAKE_HOME
        {
            get
            {
                return _Payslip.NET_PAY_TAKE_HOME;
            }
        }

        public PAYEDEATIL PayeDetail
        {
            get
            {
                return new PAYEDEATIL
                 {
                     TaxablePay = _Payslip.NetTaxableEarnings,
                     TaxDue = _Payslip.NetTax,
                     GrossTax = _Payslip.GrossTax,
                     PersonalRelief = _Payslip.PersonalRelief,
                     InsuranceRelief = _Payslip.InsuranceRelief,
                     MortgageRelief = _Payslip.MortgageRelief,

                 };
            }
        }
        public List<EarningsDeductions> Taxable_Cash_Allowances_Minus_Benefits
        {
            get
            {
                return _Payslip.Taxable_Cash_Allowances_Minus_Benefits;
            }
        }
        public decimal Total_Taxable_Cash_Allowances_Minus_Benefits
        {
            get
            {
                return _Payslip.Total_Taxable_Cash_Allowances_Minus_Benefits;
            }
        }

        public List<EarningsDeductions> Non_Taxable_Earnings_Only
        {
            get
            {
                return _Payslip.Non_Taxable_Earnings_Only;
            }
        }

        public decimal TOTAL_NON_TAXABLE_EARNINGS_ONLY
        {
            get
            {
                return _Payslip.TOTAL_NON_TAXABLE_EARNINGS_ONLY;
            }
        }
        public List<EarningsDeductions> Payments
        {
            get
            {
                return _Payslip.Earnings;
            }
        }
        public decimal TotalPayments
        {
            get
            {
                return Payments.Sum(p => p.Amount);
            }
        }
        public List<EarningsDeductions> ExcludeOtherPayments
        {
            get
            {
                return _Payslip.ExcludeOtherPayments;
            }
        }
        public List<EarningsDeductions> Deductions
        {
            get
            {
                return _Payslip.AllDeductions;
            }
        }
        //public decimal TotalDeductions
        //{
        //    get
        //    {
        //        return _Payslip.TotalDeductions;
        //    }
        //}
        public decimal TotalDeductions
        {
            get
            {
                return _Payslip.TotalDeductions;
            }
        }
        public List<EarningsDeductions> ExcludeOtherDeductions
        {
            get
            {
                return _Payslip.ExcludeOtherDeductions;
            }
        } 
        public List<NonCashBenefits> NonCashPayments
        {
            get
            {
                return _Payslip.NonCashPayments;
            }
        }
        public decimal? TotalNonCashPayments
        {
            get
            {
                return _Payslip.TotalNonCashPayments;
            }
        }
        public decimal TotalStatutoryDeductions
        {
            get
            {
                return _Payslip.TotalStatutoryDeductions;
            }
        }
        public List<EarningsDeductions> statutory_deductions_exclude_payee
        {
            get
            {
                return _Payslip.statutory_deductions_exclude_payee;
            }
        }
        public decimal total_statutory_deductions_exclude_payee
        {
            get
            {
                return _Payslip.total_statutory_deductions_exclude_payee;
            }
        }

        public decimal TOTAL_VOLUNTARY_DEDUCTIONS
        {
            get
            {
                return _Payslip.TOTAL_VOLUNTARY_DEDUCTIONS;
            }
        }
        public decimal NetPay
        {
            get
            {
                return _Payslip.total_statutory_deductions_exclude_payee;
            }
        }
        public List<EarningsDeductions> OtherPayments
        {
            get
            {
                return _Payslip.OtherPayments;
            }
        }
        public decimal TotalOtherPayments
        {
            get
            {
                return OtherPayments.Sum(e => e.Amount);
            }
        }
        public decimal TotalOtherDeductions
        {
            get
            {
                return OtherDeductions.Sum(e => e.Amount);
            }
        }
        public List<EarningsDeductions> OtherDeductions
        {
            get
            {
                return _Payslip.AllOtherDeductions;
            }
        }

        public List<EarningsDeductions> Loans
        {
            get
            {
                return _Payslip.Loans;
            }
        }
        public decimal TotalAmountLoansAmount
        {
            get
            {
                return _Payslip.Total_Loans_Repayment_Amount;
            }
        }
        public decimal TotalAmountLoansBalance
        {
            get
            {
                return _Payslip.Total_Loans_Balance;
            }
        }
        public decimal TotalAmountSaccoAmount
        {
            get
            {
                return _Payslip.Total_Sacco_Contribution_Amount;
            }
        }
        public decimal TotalAmountSaccoBalance
        {
            get
            {
                return _Payslip.Total_Sacco_Shares_Balance;
            }
        }
        public List<EarningsDeductions> SACCOContributions
        {
            get
            {
                return _Payslip.SACCOContributions;
            }
        }
    }


    public class PAYEDEATIL
    {
        public decimal TaxablePay { get; set; }
        public decimal TaxDue { get; set; }
        public decimal GrossTax { get; set; }
        public decimal PersonalRelief { get; set; }
        public decimal InsuranceRelief { get; set; }
        public decimal MortgageRelief { get; set; }
    }
}
