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

        public string IDNo { get; set; }

        public string EmpGroup { get; set; } //PP,Temp

        public string EmpPayroll { get; set; } //Exec; surb 

        //employerbank info
        public string EmployerName { get; set; }

        public string EmployerAddress { get; set; }

        public string EmployerTelephone { get; set; }

        public string EmployerEmail { get; set; }

        //the core contract before any additions or deductions
        //fully taxable
        public decimal BasicPay { get; set; }

        public decimal Basic_Pay_Plus_Allowances { get; set; }

        public List<EarningsDeductions> All_Allowances { get; set; }

        public List<EarningsDeductions> Taxable_Cash_Allowances_Minus_Benefits { get; set; }

        public decimal Total_Taxable_Cash_Allowances_Minus_Benefits
        {
            get
            {
                return this.Taxable_Cash_Allowances_Minus_Benefits.Sum(e => e.Amount);
            }
        }

        //taxable allowances eg house allowance
        //fully table if total value exceed 3000 kshs / month
        public decimal Total_Non_Cash_Benefits { get; set; }

        //exempt from tax(paid directly to net pay)
        //added at the very end
        public decimal Non_Taxable_Earnings { get; set; }

        public List<EarningsDeductions> Non_Taxable_Earnings_Only { get; set; }

        public decimal TOTAL_NON_TAXABLE_EARNINGS_ONLY
        {
            get
            {
                return this.Non_Taxable_Earnings_Only.Sum(e => e.Amount);
            }
        }
        public List<EarningsDeductions> statutory_deductions_exclude_payee
        {
            get
            {
                return AllDeductions.Where(d => d.ItemType.ToUpper().Trim().Contains("STATUTORY")).ToList();
            }
        }

        public decimal total_statutory_deductions_exclude_payee
        {
            get
            {
                return this.statutory_deductions_exclude_payee.Sum(e => e.Amount);
            }
        }

        public List<EarningsDeductions> statutory_deductions_include_payee
        {
            get
            {
                return AllDeductions.Where(d => d.ItemType.ToUpper().Trim().Contains("STATUTORY") && d.ItemType.ToUpper().Trim().Contains("TAX")).ToList();
            }
        }

        public decimal total_statutory_deductions_include_payee
        {
            get
            {
                return this.statutory_deductions_include_payee.Sum(e => e.Amount);
            }
        }

        public List<EarningsDeductions> other_deductions_exclude_statutory_payee
        {
            get
            {
                return AllDeductions.Where(d => d.ItemType.ToUpper().Trim().Contains("STATUTORY")).ToList();
            }
        }

        public decimal total_other_deductions_exclude_statutory_payee
        {
            get
            {
                return this.other_deductions_exclude_statutory_payee.Sum(e => e.Amount);
            }
        }

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
                return this.BasicPay + this.Non_Taxable_Earnings - this.TotalStatutoryDeductions - this.OtherDeductions;
            }
        }

        public decimal HourlyPay
        {
            get;
            set;
        }
         
        public List<NonCashBenefits> NonCashPayments { get; set; }

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
                    TaxablePay = this.NetTaxableEarnings,
                    TaxDue = this.NetTax,
                    GrossTax = this.GrossTax,
                    PersonalRelief = this.PersonalRelief,
                    InsuranceRelief = this.InsuranceRelief,
                    MortgageRelief=this.MortgageRelief
                };
            }
        }

        public decimal total_payee_tax
        {
            get
            {
                return PayeDetail.TaxDue;
            }
        }

        public decimal PersonalRelief { get; set; }

        public decimal MortgageRelief { get; set; }

        public decimal InsuranceRelief { get; set; }

        public decimal Total_Relief
        {
            get
            {
                return PersonalRelief + InsuranceRelief + MortgageRelief;
            }
        }

        public decimal Allowances { get; set; }

        //public decimal AllowableDeductions
        //{
        //    get
        //    {
        //        return AllDeductions.Where(d => d.TaxTracking.ToUpper().Trim() == "DEDUCTIBLE").Sum(e => e.Amount);
        //    }
        //}
        public decimal AllowableDeductions
        {
            get
            {
                return AllDeductions.Where(d => d.ItemType.ToUpper().Trim() == "STATUTORY").Sum(e => e.Amount);
            }
        }

        //public decimal StatutoryDeductions
        //{
        //    get
        //    {
        //        return AllDeductions.Where(d => d.ItemType.ToUpper().Trim().Contains("STATUTORY")).Sum(e => e.Amount);
        //    }
        //}

        //public decimal NetTaxableEarnings
        //{
        //    get
        //    {
        //        decimal _NetTaxable = this.GrossTaxableEarnings - this.AllowableDeductions;
        //        if (_NetTaxable < 0) _NetTaxable = 0;
        //        return _NetTaxable;
        //    }
        //}

        public decimal NetTaxableEarnings
        {
            get
            {
                decimal _NetTaxable = this.GrossTaxableEarnings - this.total_statutory_deductions_exclude_payee;
                if (_NetTaxable < 0) _NetTaxable = 0;
                return _NetTaxable;
            }
        }

        //public decimal NetTaxableEarnings
        //{
        //    get
        //    {
        //        decimal allowable = AllDeductions
        //            .Where(d => d.IsStatutory || (d.TaxTracking != null && d.TaxTracking.Trim().Equals("DEDUCTIBLE", StringComparison.OrdinalIgnoreCase)))
        //            .Sum(e => e.Amount);

        //        decimal _NetTaxable = this.GrossTaxableEarnings - allowable;
        //        return _NetTaxable < 0 ? 0M : _NetTaxable;
        //    }
        //}

        public decimal GrossTaxableEarnings { get; set; }
         
        public decimal GrossTax { get; set; }

        public decimal GROSS_TAXABLE_PAY_MINUS_STATUTORY
        {
            get
            {
                return NetTaxableEarnings;
            }
        }

        public decimal GROSS_PAY_AFTER_TAX
        {
            get
            {
                return GROSS_TAXABLE_PAY_MINUS_STATUTORY - total_payee_tax;
            }
        }

        public decimal NET_PAY_AFTER_DEDUCTIONS
        {
            get
            {
                decimal final_net_pay = this.GROSS_PAY_AFTER_TAX - this.TOTAL_VOLUNTARY_DEDUCTIONS;
                if (final_net_pay < 0) final_net_pay = 0;
                return final_net_pay;
            }
        }

        public decimal NET_PAY_TAKE_HOME
        {
            get
            {
                decimal final_net_pay = this.NET_PAY_AFTER_DEDUCTIONS + this.TOTAL_NON_TAXABLE_EARNINGS_ONLY;
                if (final_net_pay < 0) final_net_pay = 0;
                return final_net_pay;
            }
        }

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

        public List<EarningsDeductions> ExcludeOtherPayments
        {
            get
            {
                return this.Earnings.Where(d => d.ItemType.ToUpper().Trim().Contains("SALARY") || d.Description.ToUpper().Trim().Contains("NON_CASH_BENEFIT") || d.Description.ToUpper().Trim().Contains("HOURLY_PAY")).ToList();
            }
        }

        public List<EarningsDeductions> AllDeductions { get; set; }

        public decimal TotalDeductions
        {
            get
            {
                return AllDeductions.Sum(e => e.Amount);
            }
        }

        public List<EarningsDeductions> Deductions
        {
            get
            {
                return this.AllDeductions;
            }
        }

        public List<EarningsDeductions> AllOtherDeductions
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Equals("DEDUCTION")).ToList();
            }
        }

        public List<EarningsDeductions> all_other_deductions_exclude_statutory_sacco_loans
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Equals("DEDUCTION")).ToList();
            }
        }

        public decimal total_all_other_deductions_exclude_statutory_sacco_loans
        {
            get
            {
                return all_other_deductions_exclude_statutory_sacco_loans.Sum(e => e.Amount);
            }
        }

        public List<EarningsDeductions> ExcludeOtherDeductions
        {
            get
            {
                return AllDeductions.Where(d => d.ItemType.ToUpper().Trim().Contains("EMPECONTR") || d.ItemType.ToUpper().Trim().Contains("STATUTORY") || d.ItemType.ToUpper().Trim().Contains("TAX")).ToList();
            }
        }

        public decimal TotalPayslipDeductions
        {
            get
            {
                return this.TotalDeductions;
            }
        }

        public List<EarningsDeductions> REMAINING_DEDUCTIONS
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Contains("DEDUCTION") && !d.ItemType.ToUpper().Trim().Contains("STATUTORY") && !d.ItemType.ToUpper().Trim().Contains("TAX") && !d.ItemType.ToUpper().Trim().Contains("LOAN") && !d.ItemType.ToUpper().Trim().Contains("SACCO") && !d.ItemType.ToUpper().Trim().Contains("EMPECONTR")).ToList();
            }
        }

        public decimal TOTAL_REMAINING_DEDUCTIONS
        {
            get
            {
                return REMAINING_DEDUCTIONS.Sum(e => e.Amount);
            }
        }

        public List<EarningsDeductions> VOLUNTARY_DEDUCTIONS
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Contains("DEDUCTION") && d.ItemType.ToUpper().Trim().Contains("LOAN") && d.ItemType.ToUpper().Trim().Contains("SACCO")).ToList();
            }
        }

        public decimal TOTAL_VOLUNTARY_DEDUCTIONS
        {
            get
            {
                return TOTAL_REMAINING_DEDUCTIONS + Total_Loans_Repayment_Amount + Total_Sacco_Contribution_Amount;
            }
        }

        public decimal NET_PAY_AFTER_VOLUNTARY_DEDUCTIONS
        {
            get
            {
                return TOTAL_REMAINING_DEDUCTIONS + Total_Loans_Repayment_Amount + Total_Sacco_Contribution_Amount;
            }
        }

        public decimal TOTAL_ALL_DEDUCTIONS_GLOBAL
        {
            get
            {
                return total_statutory_deductions_exclude_payee + total_payee_tax + TOTAL_VOLUNTARY_DEDUCTIONS;
            }
        }

        public decimal TotalOtherDeductions
        {
            get
            {
                return AllOtherDeductions.Sum(e => e.Amount);
            }
        }







        public List<EarningsDeductions> Earnings { get; set; }

        public decimal TotalEarnings
        {
            get
            {
                return Earnings.Sum(e => e.Amount);
            }
        }

        public List<EarningsDeductions> Payments
        {
            get
            {
                return this.Earnings;
            }
        }

        public decimal TotalPayments
        {
            get
            {
                return Payments.Sum(p => p.Amount);
            }
        }

        public decimal NetPay
        {
            get
            {
                return this.TotalPayments - this.TotalDeductions;
            }
        }




        public List<EarningsDeductions> Loans
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Equals("LOAN")).ToList();
            }
        }

        public decimal Total_Loans_Repayment_Amount
        {
            get
            {
                return Loans.Sum(e => e.Amount);
            }
        }

        public decimal Total_Loans_Balance
        {
            get
            {
                return Loans.Sum(e => e.YTD);
            }
        }

        public List<EarningsDeductions> SACCOContributions
        {
            get
            {
                return this.AllDeductions.Where(d => d.ItemType.Trim().Equals("SACCO")).ToList();
            }
        }

        public decimal Total_Sacco_Contribution_Amount
        {
            get
            {
                return SACCOContributions.Sum(e => e.Amount);
            }
        }

        public decimal Total_Sacco_Shares_Balance
        {
            get
            {
                return SACCOContributions.Sum(e => e.YTD);
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
        public decimal GrossTax { get; set; }
        public decimal PersonalRelief { get; set; }
        public decimal InsuranceRelief { get; set; }
        public decimal MortgageRelief { get; set; }
    }

}
