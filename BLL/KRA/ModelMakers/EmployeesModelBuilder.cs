using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using CommonLib;
using BLL.KRA.Models;

namespace BLL.KRA.ModelMakers
{
    public class EmployeesModelBuilder
    {
        EmployeesModelReport _ViewModel;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        bool error = false;
        int _year;
        int _period;
        DAL.Employer _employer;
        bool _current;
        string fileLogo;
        string slogan;

        public EmployeesModelBuilder(DAL.Employer employer, bool current, int period, int year, string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _current = current;
            _employer = employer;
            _year = year;
            _period = period;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }


        public EmployeesModelReport GetEmployeesModel()
        {
            try
            {
                Build();
                return _ViewModel;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        public void Build()
        {
            try
            {
                _ViewModel = new EmployeesModelReport();
                _ViewModel.employername = _employer.Name.ToString().ToUpper();
                _ViewModel.employeraddress = _employer.Address1.ToString().Trim() + " " + _employer.Address2.ToString().Trim();
                _ViewModel.employertelephone = _employer.Telephone.ToString().Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel._lst_employees = this.GetEmployees();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }

        private List<print_employees> GetEmployees()
        {
            try
            {
                List<print_employees> lst_pe = new List<Models.print_employees>();

                var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                         where em.EmployerId == _employer.Id
                                         select em;

                List<string> Empnos = _empnosforEmployer.Select(i => i.EmpNo).ToList();

                var payrollmasterquery = from p in rep.GetPayrollMaster(_current, _period, _year)
                                         where Empnos.Contains(p.EmpNo)
                                         select p;

                List<DAL.psuedovwPayrollMaster> employees_payroll = payrollmasterquery.ToList();

                PayrollCalculationContext config = LoadPayrollConfig();
                PayslipMaker2 payMaker = new PayslipMaker2(_period, _year, "admin", db, rep, config);

                foreach (var emp_pay in employees_payroll)
                {
                    var employee_query = from emp in _empnosforEmployer
                                         where emp.Id == emp_pay.EmployeeId
                                         select emp;

                    DAL.Employee _employee = employee_query.FirstOrDefault();
                    if (_employee == null) continue;

                    Payslip payslip = payMaker.CreatePayslipFromTransactions(_employee);

                    print_employees pe = new print_employees();
                    pe.employeenumber = emp_pay.EmpNo;
                    pe.employeename = emp_pay.Surname + ", " + emp_pay.OtherNames;

                    switch (_employee.Gender)
                    {
                        case "M":
                            pe.gender = "MALE";
                            break;
                        case "F":
                            pe.gender = "FEMALE";
                            break;
                        default:
                            pe.gender = _employee.Gender;
                            break;
                    }

                    pe.pinnumber = emp_pay.PINNo;
                    pe.idnumber = emp_pay.IDNo;
                    pe.email = _employee.Email;
                    pe.PhoneNo = _employee.PhoneNo;
                    pe.department = emp_pay.Department;
                    pe.dateofemployment = _employee.DoE ?? DateTime.Today;

                    // Standard Earnings
                    // Basic Pay
                    pe.basic_pay = (decimal)payslip.BasicPay;

                    // Gross Taxable Earnings
                    pe.gross_taxable_pay = (decimal)payslip.GrossTaxableEarnings;

                    pe.Taxable_Cash_Allowances = (decimal)payslip.Total_Taxable_Cash_Allowances_Minus_Benefits;
                    pe.Non_Taxable_Earnings = (decimal)payslip.Non_Taxable_Earnings;
                    pe.Total_Non_Cash_Benefits = (decimal)payslip.Total_Non_Cash_Benefits;

                    pe.total_all_other_payments = (decimal)payslip.TotalOtherPayments;

                    // Statutory Deductions: AHL + NSSF + SHIF
                    pe.statutory_deductions = (decimal)payslip.total_statutory_deductions_exclude_payee;

                    //Net Taxable Earnings
                    pe.gross_taxable_pay_minus_statutory = (decimal)payslip.GROSS_TAXABLE_PAY_MINUS_STATUTORY;

                    pe.gross_tax = (decimal)payslip.PayeDetail.GrossTax;

                    pe.Total_Relief = (decimal)payslip.Total_Relief;

                    // Statutory Deductions: PAYE 
                    pe.payee = (decimal)payslip.total_payee_tax;// PAYE;

                    //Net Pay After Tax
                    pe.net_pay_after_tax = (decimal)payslip.GROSS_PAY_AFTER_TAX;

                    // Total loan Deductions
                    pe.loan_deductions = (decimal)payslip.Total_Loans_Repayment_Amount;

                    // Total sacco Deductions
                    pe.sacco_deductions = (decimal)payslip.Total_Sacco_Contribution_Amount;

                    // Total remaining Deductions
                    pe.remaining_deductions = (decimal)payslip.TOTAL_REMAINING_DEDUCTIONS;

                    // Total voluntary Deductions
                    pe.voluntary_deductions = (decimal)payslip.TOTAL_VOLUNTARY_DEDUCTIONS;

                    // Total Deductions
                    pe.total_deductions = payslip.TOTAL_ALL_DEDUCTIONS_GLOBAL;

                    //Net after all other deductions
                    pe.net_pay_after_every_other_deductions = payslip.NET_PAY_AFTER_DEDUCTIONS;

                    //Net take home
                    pe.net_pay_take_home = payslip.NET_PAY_TAKE_HOME;

                    pe.nssf_no = _employee.NSSFNo;
                    pe.nhif_no = _employee.NHIFNo;

                    // 2/3 Payroll Rule Check
                    decimal minRequiredNetPay = pe.gross_taxable_pay / 3.0M;
                    pe.PassTwoThirdsRule = pe.net_pay_after_every_other_deductions >= minRequiredNetPay;

                    switch (emp_pay.PaymentMode)
                    {
                        case "M":
                            pe.paymentmode = "MPESA";
                            break;
                        case "B":
                            pe.paymentmode = "BANKACCOUNT";
                            break;
                        case "C":
                            pe.paymentmode = "CASH";
                            break;
                        default:
                            pe.paymentmode = emp_pay.PaymentMode;
                            break;
                    }

                    lst_pe.Add(pe);
                }
                return lst_pe;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

        private PayrollCalculationContext LoadPayrollConfig()
        {
            decimal mRelief = 0M;
            decimal.TryParse(rep.SettingLookup("MRELIEF"), out mRelief);

            decimal sRelief = 0M;
            decimal.TryParse(rep.SettingLookup("SRELIEF"), out sRelief);

            decimal peRate = 0M;
            decimal.TryParse(rep.SettingLookup("PEN1E"), out peRate);

            decimal prRate = 0M;
            decimal.TryParse(rep.SettingLookup("PEN1R"), out prRate);

            decimal minPaye = 0M;
            decimal.TryParse(rep.SettingLookup("PAYEEMIN"), out minPaye);

            var payeeRates = rep.PayeeRates();

            return new PayrollCalculationContext
            {
                DefaultReliefMarried = mRelief,
                DefaultReliefSingle = sRelief,
                EmployeeRate = peRate,
                EmployerRate = prRate,
                PensionSchemeFlag = rep.SettingLookup("DEFCONTRSCHEME"),
                MinimumPaye = minPaye,
                PayeeRates = payeeRates != null ? payeeRates : new List<PayeeRate>()
            };
        }


















    }
}
