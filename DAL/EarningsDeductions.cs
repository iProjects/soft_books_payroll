using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EarningsDeductions
    {
        public EarningsDeductions() 
        { 
        
        }

        public int EmpTxnId { get; set; }
        public string ItemType { get; set; }
        public string Description { get; set; }
        public string TaxTracking { get; set; }
        public decimal Amount { get; set; }
        public decimal employernssf { get; set; }
        public bool ShowInPayslip { get; set; }
        public bool IsStatutory { get; set; }
        public bool TrackYTD { get; set; }
        public decimal YTD { get; set; }
        public string DEType { get; set; }  

        public EarningsDeductions(int empTxnId, string itemType, string DE, string Des,  string Taxable, decimal amt, bool Show, bool trackYTD, decimal yTD, bool Statutory)
        {
            EmpTxnId = empTxnId;
            DEType = DE;
            Description = Des;
            TaxTracking = Taxable;
            Amount = amt;
            TrackYTD = trackYTD;
            ShowInPayslip = Show;
            YTD = yTD;
            IsStatutory = Statutory;
            ItemType = itemType;
            
        }
    }
    public class TaxBracket
    {
        public int Bracket {get; set;}
        public decimal From { get; set; }
        public decimal To { get; set; }
        public decimal AmtTaxed { get; set; }
        public decimal Rate { get; set; }
        public decimal Tax { get; set; }

        public TaxBracket(int bracket, decimal from, decimal to, decimal rate, decimal tax, decimal amt)
        {
            this.Bracket = bracket;
            this.From = from;
            this.To = to;
            this.Rate = rate;
            this.Tax = tax;
            this.AmtTaxed = amt;
        }
    }
    public class NonCashBenefits
    {
        public string EmpNo { get; set; }
        public int EmployeeId { get; set; }
        public int Quantity { get; set; }
        public int BenefitId { get; set; }
        public string Description { get; set; }
        public decimal? Amount
        {
            get
            {
                return this.Quantity * this.Rate ;
            }
        }
        public decimal? Rate { get; set; }
    }
    public class StatementDTO
    {
        public DateTime date { get; set; } //Transaction date
        public string Description { get; set; }
        public decimal Amountin { get; set; }
        public decimal EmpNSSFContrib { get; set; }
        public decimal Amountout { get; set; }
        public decimal Balance { get; set; }
        public decimal TotalContribs { get; set; }
    }
    public class ScheduleDTO
    { 
        public string EmpName { get; set; }
        public string EmpNo { get; set; }
        public decimal EmpNSSFContrib { get; set; }
        public decimal Amount { get; set; } 
        public decimal TotalContribs { get; set; }
    }
    public class NssfContributionsDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmpNo { get; set; }
        public decimal EmployeeEarnings { get; set; }
        public decimal PensionableEarnings { get; set; }
        public decimal Tier1PensionableEarnings { get; set; }
        public decimal Tier1EmployeeDeduction { get; set; }
        public decimal Tier1EmployerContribution { get; set; }
        public decimal Tier1TotalContribution { get; set; }
        public decimal Tier2PensionableEarnings { get; set; }
        public decimal Tier2EmployeeDeduction { get; set; }
        public decimal Tier2EmployerContribution { get; set; }
        public decimal Tier2TotalContribution { get; set; }
        public decimal TotalPensionContribution { get; set; } 
    }
    public class EmployeeMonthlyTaxRecord
    {
        public string Month { get; set; }
        public int MonthInt { get; set; }
        public decimal A { get; set; } //BasicPay;
        public decimal B { get; set; } //BenefitsNonCash;
        public decimal C { get; set; } //ValueOfQuarters;
        public decimal D //TotalGrossPay;
        {
            get
            { return this.A + this.B + this.C; }
        }
        public decimal E1
        {
            get
            {
                return .3M * this.A;
            }
        } //DefineContributionRetirementScheme;
        public decimal E2 { get; set; } //Actual pension contribution;
        public decimal E3 { get; set; }  //Fixed;
        public decimal F { get; set; } //DefineContribution;
        public decimal G
        {
            get
            {
                return Lowest(this.E1, this.E2, this.E3) + this.F;

            }
        } //ChargablePay;
        public decimal H { get; set; } //TaxCharged;
        //public decimal J { get; set; } 
        public decimal TotalRelief { get { return this.J + this.J1; } }
        public decimal J { get; set; } //MonthlyRelief;
        public decimal J1 { get; set; } //InsuranceRelief;
        public decimal K { get { return J - TotalRelief; } } //PAYETax;

        private decimal Lowest(decimal a, decimal b, decimal c)
        {
            List<decimal> ld = new List<decimal>() { a, b, c };
            return ld.Min();
        }
    }
    public class EmployersMonthlyTaxRecord
    {
        public string Month { get; set; }
        public int MonthInt { get; set; }
        public decimal A { get; set; } //BasicPay;
        public decimal B { get; set; } //BenefitsNonCash;
        public decimal C { get; set; } //ValueOfQuarters;
        public decimal D //TotalGrossPay;
        {
            get
            { return this.A + this.B + this.C; }
        }
        public decimal E1
        {
            get
            {
                return .3M * this.A;
            }
        } //DefineContributionRetirementScheme;
        public decimal E2 { get; set; } //Actual pension contribution;
        public decimal E3 { get; set; }  //Fixed;
        public decimal F { get; set; } //SavingsPlan;
        public decimal G
        {
            get
            {
                return Lowest(this.E1, this.E2, this.E3) + this.F;

            }
        } //RetirementContributionSavingsPlan;
        public decimal H { get; set; } //ChargablePay;
        public decimal J { get; set; } //TaxCharged;
        public decimal TotalRelief { get { return this.K + this.K1; } }
        public decimal K { get; set; } //MonthlyRelief;
        public decimal K1 { get; set; } //InsuranceRelief;
        public decimal L { get { return J - TotalRelief; } } //PAYETax;

        private decimal Lowest(decimal a, decimal b, decimal c)
        {
            List<decimal> ld = new List<decimal>() { a, b, c };
            return ld.Min();
        }
    }


}
