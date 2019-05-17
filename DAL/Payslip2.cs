using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Payslip2
    {
        Payslip _Payslip;

        public Payslip2(Payslip payslip)
        {
            _Payslip = payslip;
        }

        public string PayrollMonth {
            get {
                return _Payslip.PayPeriod.ToString("MMMM");
            }
        }

        public string EmpNo { get { return _Payslip.EmpNo; } }

        public string EmpName { get { return _Payslip.EmpName; } }

        public string PINNo { get { return _Payslip.PINNo; } }

        public string NSSFNO { get { return _Payslip.NSSFNO; } }

        public string NHIFNO { get { return _Payslip.NHIFNO; } }

        public string Department { get { return _Payslip.Department; } }

        public string EmployerName { get { return _Payslip.EmployerName; } }

        //...

        public decimal GrossTaxablePay
        {
            get
            {
                return _Payslip.GrossTaxableEarnings;
            }
        }
        public PAYEDEATIL PayeDetail 
        {
            get
            {
               return new PAYEDEATIL
                {
                    PersonalRelief = _Payslip.PersonalRelief,
                    TaxablePay = _Payslip.NetTaxableEarnings,
                    TaxDue = _Payslip.GrossTax,
                };
            }
        }

        public List<EarningsDeductions> Payments { get { return _Payslip.Earnings; } }

        //public List<NonCashBenefits> Benefits { get { return _Payslip.Benefits; } }

        public List<EarningsDeductions> Deductions { get { return _Payslip.AllDeductions; } }

        public decimal TotalPayments
        {
            get
            {
                return
                    Payments.Sum(p => p.Amount);
            }
        }
        public decimal TotalDeductions { get { return _Payslip.TotalDeductions; } }
        public decimal NetPay { get { return 
            this.TotalPayments-this.TotalDeductions; } }

        public List<EarningsDeductions> OtherPayments {
            get
            {
                return _Payslip.Earnings.Where(e => e.TaxTracking.Trim().Equals("EARNING"))
                    .Where((e => e.ItemType.Trim() != "SALARY")).ToList(); 
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

        public List<EarningsDeductions> OtherDeductions { 
            get {
                return _Payslip.AllDeductions.Where(d => d.ItemType.Trim().Equals("DEDUCTION")).ToList(); 
            } }

        public List<EarningsDeductions> Loans
        {
            get
            {
                return _Payslip.AllDeductions.Where(d => d.ItemType.Trim().Equals("LOAN")).ToList();
            }
        }
        public decimal TotalAmountLoansAmount
        {
            get
            {
                return Loans.Sum(e => e.Amount);
            }
        }

        public decimal TotalAmountLoansBalance
        {
            get
            {
                return Loans.Sum(e => e.YTD);
            }
        }

        public decimal TotalAmountSaccoAmount
        {
            get
            {
                return SACCOContributions.Sum(e => e.Amount);
            }
        }

        public decimal TotalAmountSaccoBalance
        {
            get
            {
                return SACCOContributions.Sum(e => e.YTD);
            }
        }

        public List<EarningsDeductions> SACCOContributions
        {
            get
            {
                return _Payslip.AllDeductions.Where(d => d.ItemType.Trim().Equals("SACCO")).ToList();
            }
        }
    }


   
}
