using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace winSBPayroll.Utilities
{
    /*Under the NSSF Act, employee pensionable earnings are evaluated against a Lower Earnings Limit (LEL) and an Upper Earnings Limit (UEL) at a contribution rate of 6%.
Calculation Breakdown
 * Tier I Contribution: 6% of earnings up to the LEL.
 * Tier II Contribution: 6% of earnings between LEL and UEL.
 * Total NSSF: Sum of Tier I and Tier II. Employer matches this amount identically.*/

    public class NssfResult
    {
        public decimal GrossEarnings { get; set; }
        public decimal Tier1Employee { get; set; }
        public decimal Tier2Employee { get; set; }
        public decimal TotalEmployeeContribution { get; set; }
        public decimal TotalEmployerContribution { get; set; }

        // Converted from expression-bodied property to a standard getter for C# 4.0
        public decimal TotalDeduction
        {
            get
            {
                return TotalEmployeeContribution + TotalEmployerContribution;
            }
        }
    }

    public class NssfCalculator
    {
        // Thresholds (Configure according to current statutory year)
        private readonly decimal _lowerEarningsLimit;
        private readonly decimal _upperEarningsLimit;
        private const decimal ContributionRate = 0.06m;

        public NssfCalculator(decimal lowerEarningsLimit = 9000m, decimal upperEarningsLimit = 108000m)
        {
            _lowerEarningsLimit = lowerEarningsLimit;
            _upperEarningsLimit = upperEarningsLimit;
        }

        public NssfResult Calculate(decimal grossEarnings)
        {
            if (grossEarnings < 0)
                throw new ArgumentOutOfRangeException("grossEarnings", "Earnings cannot be negative.");

            // Cap pensionable pay at the Upper Earnings Limit
            decimal pensionablePay = Math.Min(grossEarnings, _upperEarningsLimit);

            // Tier I: 6% on earnings up to LEL
            decimal tier1Pay = Math.Min(pensionablePay, _lowerEarningsLimit);
            decimal tier1Employee = tier1Pay * ContributionRate / 100M;

            // Tier II: 6% on earnings between LEL and UEL
            decimal tier2Pay = Math.Max(0m, pensionablePay - _lowerEarningsLimit);
            decimal tier2Employee = tier2Pay * ContributionRate / 100M;

            decimal totalEmployee = tier1Employee + tier2Employee;

            return new NssfResult
            {
                GrossEarnings = grossEarnings,
                Tier1Employee = tier1Employee,
                Tier2Employee = tier2Employee,
                TotalEmployeeContribution = totalEmployee,
                TotalEmployerContribution = totalEmployee // Employer matches 100%
            };
        }
    }

    // Example usage
    public class Program
    {
        public static void Main()
        {
            var calculator = new NssfCalculator(9000m, 108000m);

            decimal grossPay = 50000m;
            var result = calculator.Calculate(grossPay);

            // Replaced string interpolation ($"...") with String.Format / Standard Formatting
            Console.WriteLine(string.Format("Gross Salary: KES {0:N2}", result.GrossEarnings));
            Console.WriteLine(string.Format("Tier I (Employee): KES {0:N2}", result.Tier1Employee));
            Console.WriteLine(string.Format("Tier II (Employee): KES {0:N2}", result.Tier2Employee));
            Console.WriteLine(string.Format("Total Employee Contribution: KES {0:N2}", result.TotalEmployeeContribution));
            Console.WriteLine(string.Format("Total Combined Remittance: KES {0:N2}", result.TotalDeduction));
        }
    }






}
