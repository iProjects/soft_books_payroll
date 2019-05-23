using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DAL
{
    public class Payslip
    {



        #region Constructor

        public Payslip()
        {

        }

        #endregion

        private string orientation = "P";
        public string Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        public int Period { get; set; }
        public int Year { get; set; }
        public DateTime PayPeriod
        {
            get
            {
                return new DateTime(Year, Period, 1);
            }
        }

        public string PayrollMonth
        {
            get
            {
                return PayPeriod.ToString("MMMM");
            }
        }

        public string PayrollYear
        {
            get
            {
                return PayPeriod.ToString("yyyy");
            }
        }

        public DateTime PaymentDate { get; set; }

        public string PrintedBy { get; set; }

        public DateTime PrintedOn { get; set; }

        //emp info
        public string EmpName { get; set; }

        public string EmpNo { get; set; }

        public int EmployeeId { get; set; }
        public int EmployerId { get; set; }

        public string Department { get; set; }

        public string NHIFNO { get; set; }

        public string NSSFNO { get; set; }

        public string PayPoint { get; set; } //HQ, etc

        public string PINNo { get; set; }

        public string EmpGroup { get; set; } //PP,Temp

        public string EmpPayroll { get; set; } //Exec; surb 

        //employerbank info
        public string EmployerName { get; set; }

        public string EmployerAddress { get; set; }

        public string EmployerTelephone { get; set; }

        public string EmployerEmail { get; set; }

        //Payslip
        public decimal BasicPay { get; set; }

        public decimal TotalBenefits
        {
            get;
            set;
        }

        public decimal Variables { get; set; }

        public List<EarningsDeductions> StatutoryDeductions
        {
            get
            {
                return AllDeductions.Where(d => d.IsStatutory).ToList();
            }
        }

        public decimal TotalStatutoryDeductions
        {
            get
            {
                return this.StatutoryDeductions.Sum(e => e.Amount);
            }
        }

        public decimal OtherDeductions
        {
            get
            {
                return TotalDeductions - TotalStatutoryDeductions;
            }
        }

        public List<EarningsDeductions> OtherDeductionsList
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Equals("DEDUCTION")).ToList();
            }
        }

        public decimal TotalOtherDeductionsList
        {
            get
            {
                return OtherDeductionsList.Sum(e => e.Amount);
            }
        }

        public decimal NetSalary
        {
            get
            {
                return this.BasicPay + this.Variables - this.TotalStatutoryDeductions -
                    this.OtherDeductions;
            }
        }

        public decimal HourlyPay
        {
            get;
            set;
        }

        public List<NonCashBenefits> NonCashPayments { get; set; }

        public List<EarningsDeductions> Earnings { get; set; }

        public List<EarningsDeductions> AllDeductions { get; set; }

        public decimal TotalEarnings
        {
            get
            {
                return Earnings.Sum(e => e.Amount);
            }
        }

        public decimal TotalDeductions
        {
            get
            {
                return AllDeductions.Sum(e => e.Amount);
            }
        }
        public decimal? TotalNonCashPayments
        {
            get
            {
                return NonCashPayments.Sum(p => p.Amount);
            }
        } 
        //Payslip summary
        //Tax Details

        public PAYEDEATIL PayeDetail
        {
            get
            {
                return new PAYEDEATIL
                {
                    PersonalRelief = this.PersonalRelief,
                    TaxablePay = this.NetTaxableEarnings,
                    TaxDue = this.GrossTax,
                    InsuranceRelief = this.InsuranceRelief
                };
            }
        }
        public decimal GrossTaxableEarnings { get; set; }

        public decimal AllowableDeductions
        {
            get
            {
                return AllDeductions.Where(d => d.TaxTracking.ToUpper().Trim() == "DEDUCTIBLE").Sum(e => e.Amount);
            }
        }

        public decimal MortgageRelief { get; set; }

        public decimal InsuranceRelief { get; set; }

        public decimal NetTaxableEarnings
        {
            get
            {
                decimal _NetTaxable = this.GrossTaxableEarnings - this.AllowableDeductions;
                if (_NetTaxable < 0) _NetTaxable = 0;
                return _NetTaxable;
            }
        }

        public decimal GrossTax { get; set; }

        public decimal PersonalRelief { get; set; }

        public decimal NetTax /*This is equivalent to PAYE*/
        {
            get
            {
                decimal netTax = this.GrossTax - this.PersonalRelief - this.MortgageRelief - this.InsuranceRelief;
                if (netTax < 0) netTax = 0;
                return netTax;
            }
        }

        //PensionTotals
        public decimal PensionEmployer { get; set; }

        public decimal PensionEmployee { get; set; }

        public decimal NSSFEmployer { get; set; }

        //Payment
        public string BankBranch { get; set; }

        public string Account { get; set; }

        //Diagonistic
        public List<TaxBracket> TaxBracketList { get; set; }

        public List<EarningsDeductions> Payments
        {
            get
            {
                return this.Earnings;
            }
        }
        public List<EarningsDeductions> ExcludeOtherPayments
        {
            get
            {
                return this.Earnings.Where(d => d.ItemType.ToUpper().Trim().Contains("SALARY") || d.Description.ToUpper().Trim().Contains("NON_CASH_BENEFIT") || d.Description.ToUpper().Trim().Contains("HOURLY_PAY")).ToList();
            }
        }
        public List<EarningsDeductions> Deductions
        {
            get
            {
                return this.AllDeductions;
            }
        }
        public List<EarningsDeductions> ExcludeOtherDeductions
        {
            get
            {
                return AllDeductions.Where(d => d.ItemType.ToUpper().Trim().Contains("EMPECONTR") || d.ItemType.ToUpper().Trim().Contains("STATUTORY") || d.ItemType.ToUpper().Trim().Contains("TAX")).ToList();
            }
        } 
        public decimal TotalPayments
        {
            get
            {
                return Payments.Sum(p => p.Amount);
            }
        }
        public decimal TotalOtherDeductions
        {
            get
            {
                return AllOtherDeductions.Sum(e => e.Amount);
            }
        }
        public List<EarningsDeductions> AllOtherDeductions
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Equals("DEDUCTION")).ToList();
            }
        }
        public decimal TotalPayslipDeductions
        {
            get
            {
                return this.TotalDeductions;
            }
        }
        public decimal NetPay
        {
            get
            {
                return this.TotalPayments - this.TotalDeductions;
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
                return this.AllDeductions.Where(d => d.ItemType.Trim().Equals("SACCO")).ToList();
            }
        }
        public List<EarningsDeductions> Loans
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Equals("LOAN")).ToList();
            }
        } 
        public List<EarningsDeductions> OtherPayslipDeductions
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Equals("DEDUCTION")).ToList();
            }
        } 
        public List<EarningsDeductions> OtherPayments
        {
            get
            {
                return this.Earnings.Where(e => e.TaxTracking.Trim().Equals("EARNING"))
                    .Where((e => e.ItemType.Trim() != "SALARY")).Where((e => e.Description.Trim() != "NON_CASH_BENEFIT")).Where((e => e.Description.Trim() != "HOURLY_PAY")).ToList();
            }
        } 
        public decimal TotalOtherPayments
        {
            get
            {
                return OtherPayments.Sum(e => e.Amount);
            }
        }

    }

    public class PAYEDEATIL
    {
        public decimal TaxablePay { get; set; }
        public decimal TaxDue { get; set; }
        public decimal PersonalRelief { get; set; }
        public decimal InsuranceRelief { get; set; }
    }

}
