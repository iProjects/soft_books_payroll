using System;
using System.Collections.Generic;
using System.Linq;
using CommonLib;

namespace DAL
{
    public class PayrollCalculationContext
    {
        public decimal DefaultReliefMarried { get; set; }
        public decimal DefaultReliefSingle { get; set; }
        public decimal EmployeeRate { get; set; }
        public decimal EmployerRate { get; set; }
        public string PensionSchemeFlag { get; set; }
        public decimal MinimumPaye { get; set; }
        public List<PayeeRate> PayeeRates { get; set; }

        public PayrollCalculationContext()
        {
            PayeeRates = new List<PayeeRate>();
        }
    }

    public class PayslipMaker2
    {
        private readonly SBPayrollDBEntities _db;
        private readonly Repository _rep;
        private readonly PayrollCalculationContext _config;
        private readonly int _paymentPeriod;
        private readonly int _year;
        private readonly string _user;

        public string ErrorMsg { get; private set; }
        public bool HasError { get; private set; }

        public PayslipMaker2(
            int period,
            int year,
            string user,
            SBPayrollDBEntities db,
            Repository rep,
            PayrollCalculationContext config)
        {
            _paymentPeriod = period;
            _year = year;
            _user = user;
            _db = db;
            _rep = rep;
            _config = config;
        }

        public Payslip CreatePayslipFromTransactions(Employee employee)
        {
            try
            {
                var employer = _rep.GetEmployeeEmployer(employee.EmployerId);
                if (employer == null)
                {
                    throw new InvalidOperationException(string.Format("Employer for Employee ID {0} not found.", employee.Id));
                }

                string empSurname = employee.Surname != null ? employee.Surname.Trim() : string.Empty;

                var payslip = new Payslip
                {
                    PaymentDate = DateTime.Now,
                    PrintedBy = _user,
                    PrintedOn = DateTime.Today,
                    EmpName = string.Format("{0}, {1}", empSurname, employee.OtherNames),
                    EmpNo = employee.EmpNo,
                    EmployeeId = employee.Id,
                    PayPoint = employee.PayPoint,
                    PINNo = employee.PINNo,
                    IDNo = employee.IDNo,
                    NHIFNO = employee.NHIFNo,
                    NSSFNO = employee.NSSFNo,
                    EmpGroup = employee.EmpGroup,
                    EmpPayroll = employee.EmpPayroll,
                    Period = _paymentPeriod,
                    Year = _year,
                    EmployerName = employer.Name,
                    EmployerId = employer.Id,
                    EmployerAddress = string.Format("{0}, {1}", employer.Address1, employer.Address2),
                    EmployerTelephone = employer.Telephone,
                    EmployerEmail = employer.Email,
                    BankBranch = employee.BankCode,
                    Account = employee.BankAccount,
                    BasicPay = employee.BasicPay ?? 0M
                };

                // Department Lookup
                var dept = _db.Departments.FirstOrDefault(dp => dp.Id == employee.DepartmentId);
                payslip.Department = dept != null ? dept.Description : string.Empty;

                // Process Pay Items
                AddDeductions(payslip, employee);

                AddPensionInfo(payslip, employee);

                AddTaxInfo(payslip, employee);

                return payslip;
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMsg = ex.Message;
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }

        private void AddDeductions(Payslip payslip, Employee employee)
        {
            payslip.AllDeductions = _rep.EmployeeDeductions(employee.Id);

            decimal basicPay = employee.BasicPay.HasValue ? employee.BasicPay.Value : 0M;

            // Process statutory flags cleanly
            foreach (var d in payslip.AllDeductions)
            {
                string desc = d.Description != null ? d.Description.Trim() : string.Empty;

                if (desc == "NSSF")
                {
                    var nssfObj = _rep.ComputeNssfContributions(employee.Id);
                    //d.Amount = nssfObj != null ? nssfObj.TotalPensionContribution : 0M;
                    d.Amount = nssfObj != null ? nssfObj.TotalEmployeeContribution : 0M;
                    payslip.NSSFEmployer = nssfObj != null ? nssfObj.TotalEmployerContribution : 0M;
                    d.IsStatutory = true;
                    d.ItemType = "STATUTORY";
                }
                else if (desc == "NHIF")
                {
                    //return;
                    //d.Amount = _rep.NHIFLookUp(basicPay);
                    d.Amount = 0M;
                }
                else if (desc == "SHIF")
                {
                    d.Amount = ComputeSHIF(basicPay);
                    d.IsStatutory = true;
                    d.ItemType = "STATUTORY";
                }
                else if (desc == "AHL")
                {
                    d.Amount = ComputeAHL(basicPay);
                    d.IsStatutory = true;
                    d.ItemType = "STATUTORY";
                }

                if (desc == "NSSF" || desc == "NHIF" || desc == "SHIF" || desc == "AHL")
                {
                    d.IsStatutory = true;
                    d.IsStatutory = true;
                    d.ItemType = "STATUTORY";
                }
            }
        }

        private void AddPensionInfo(Payslip payslip, Employee employee)
        {
            decimal pensionableEarnings = _rep.PensionableEarnings(employee.Id);
            decimal empContribution = (pensionableEarnings * _config.EmployeeRate) / 100M;
            decimal employerContribution = (pensionableEarnings * _config.EmployerRate) / 100M;

            payslip.PensionEmployer = employerContribution;
            payslip.PensionEmployee = empContribution;

            //decimal nssfEmp = 0M;
            //decimal.TryParse(_rep.SettingLookup("EMPNSSF"), out nssfEmp);
            //payslip.NSSFEmployer = nssfEmp;
        }

        private void AddTaxInfo(Payslip payslip, Employee employee)
        {
            decimal hourlyPay = _rep.GetHourlyPay(employee.Id);
            decimal basicPay = employee.BasicPay.HasValue ? employee.BasicPay.Value : 0M;

            if (employee.BasicComputation == "H") basicPay = hourlyPay;
            else if (employee.BasicComputation == "X") basicPay += hourlyPay;

            payslip.HourlyPay = hourlyPay;
            payslip.BasicPay = basicPay;

            payslip.Basic_Pay_Plus_Allowances = basicPay + _rep.Allowances(employee.Id);

            decimal Allowances = _rep.Allowances(employee.Id);
            decimal NonCashBenefits = _rep.NonCashBenefits(employee.Id);
            decimal NonTaxableEarnings = _rep.NonTaxableEarnings(employee.Id);

            payslip.Total_Non_Cash_Benefits = NonCashBenefits;
            payslip.Non_Taxable_Earnings = NonTaxableEarnings;
            payslip.Allowances = Allowances;
            payslip.Taxable_Cash_Allowances_Minus_Benefits = _rep.Taxable_Cash_Allowances_Minus_Benefits(employee.Id);
            payslip.Non_Taxable_Earnings_Only = _rep.Non_Taxable_Earnings_Only(employee.Id);

            payslip.Earnings = this.Earnings(employee);

            //basic + taxable non-cash benefits + other payments
            payslip.GrossTaxableEarnings = payslip.BasicPay + payslip.Total_Non_Cash_Benefits + payslip.TotalOtherPayments;

            List<TaxBracket> taxBrackets = new List<TaxBracket>();
            payslip.GrossTax = CalculatePAYE(payslip.NetTaxableEarnings, ref taxBrackets);
            payslip.TaxBracketList = taxBrackets;

            payslip.PersonalRelief = GetPersonalRelief(employee);
            payslip.MortgageRelief = this.MortgageRelief(employee);
            payslip.InsuranceRelief = this.InsuranceRelief(employee);

            var payeItem = payslip.AllDeductions.FirstOrDefault(d => d.Description != null && d.Description.Trim() == "PAYE");
            if (payeItem != null)
            {
                payeItem.Amount = payslip.NetTax;
                payeItem.IsStatutory = true;
            }
        }

        //get only the taxable earnings
        //flag for taxable earnings is taxtracking = earning
        private List<EarningsDeductions> Earnings(Employee employee)
        {
            List<EarningsDeductions> alle = _rep.EmployeeEarnings(employee.Id);
            foreach (var d in alle)
            {
                EarningsDeductions noncash = new EarningsDeductions
                {
                    DEType = "E",
                    ShowInPayslip = true,
                    TrackYTD = true,
                    YTD = 0,
                    EmpTxnId = d.EmpTxnId,
                    TaxTracking = "EARNING",
                    Amount = d.Amount
                };
            }
            return alle;
        }

        private decimal CalculatePAYE(decimal netTaxable, ref List<TaxBracket> brackets)
        {
            if (netTaxable < _config.MinimumPaye) return 0M;

            decimal payee = 0M;
            decimal balanceToTax = netTaxable;

            for (int i = 0; i < _config.PayeeRates.Count; i++)
            {
                var bracket = _config.PayeeRates[i];
                bool isLast = (i == _config.PayeeRates.Count - 1);

                decimal taxableInBracket = isLast ? balanceToTax : Math.Min(balanceToTax, bracket.ToAmt - bracket.FromAmt);
                if (taxableInBracket <= 0) break;

                decimal tax = taxableInBracket * (bracket.Rate / 100M);
                payee += tax;
                brackets.Add(new TaxBracket(i + 1, bracket.FromAmt, bracket.ToAmt, bracket.Rate / 100M, tax, taxableInBracket));

                balanceToTax -= taxableInBracket;
            }

            return payee;
        }

        private decimal GetPersonalRelief(Employee employee)
        {
            decimal empRelief = _rep.PersonalRelief(employee.Id);
            if (empRelief != 0) return empRelief;

            if (employee.MaritalStatus != null && employee.MaritalStatus.Equals("S", StringComparison.OrdinalIgnoreCase))
            {
                return _config.DefaultReliefSingle;
            }

            return _config.DefaultReliefMarried;
        }

        private decimal MortgageRelief(Employee employee)
        {
            return _rep.MortgageRelief(employee.Id);
        }

        private decimal InsuranceRelief(Employee employee)
        {
            return _rep.InsuranceRelief(employee.Id);
        }

        private decimal ComputeSHIF(decimal basic)
        {
            // Evaluates basic * 0.0275m directly with full decimal precision, enforcing the 300 minimum cutoff.
            return Math.Max(basic * 0.0275m, 300m);
        }

        private decimal ComputeAHL(decimal basic)
        {
            // Evaluates basic * 0.015m directly with full decimal precision.
            return basic * 0.015m;
        }

    }
}