using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using CommonLib;


namespace DAL
{
    public class PayslipMaker
    {
        #region "Private Properties"
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        #region "Employee info"
        decimal _EmployeeRate;
        decimal _EmployerRate;
        string _EmpNo;
        int _EmployeeId;
        string _MaritalStatus;
        decimal _Basic;
        Employee employee;
        Employer employer;
        #endregion "Employee info"
        #region "Settings & House keeping"
        decimal _defaultRelief_Married;
        decimal _defaultRelief_Single;
        string _User;
        DateTime _PaymentDate = DateTime.Now;
        public int _PaymentPeriod;
        public int _Year;
        private string pensionSchemeFlag;
        public bool _erorr = false;
        public string _errMsg;
        private bool anonymous = false;
        #endregion "Settings & House keeping"
        #region "employee tax info"
        decimal _PensionableEarnings;
        #endregion "employee tax info"

        #endregion "Private Properties"

        #region Constructors
        //used to make payslip for an employee
        public PayslipMaker(int Period, int Year, int EmployeeId, string EmpNo, string User, string Conn)
        {
            try
            {
                if (string.IsNullOrEmpty(Conn))
                    throw new ArgumentNullException("connection");
                connection = Conn;

                db = new SBPayrollDBEntities(connection);
                rep = new Repository(connection);

                _PaymentPeriod = Period;
                _Year = Year;
                _EmployeeId = EmployeeId;
                _EmpNo = EmpNo;
                _User = User;
                anonymous = false;

                //load settings
                if (!Load())
                {
                    _erorr = true;
                    _errMsg = "Unable to load configs. please check the settings table";
                    Log.WriteToErrorLogFile(new Exception(_errMsg)); //log

                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }

        //used to make anonymous payslip
        public PayslipMaker(decimal TaxablePay, string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _Basic = TaxablePay;
            anonymous = true;

            if (!Load())
            {
                _erorr = true;
                Log.WriteToErrorLogFile(new Exception("Unable to load configs. please check the settings table")); //log
            }
        }
        #endregion

        #region Public Members
        public Payslip CreatePayslipFromTransactions()
        {
            //create payslip from loaded info. 
            Payslip payslip = new Payslip();

            if (!AddEmployeeEmployer(payslip))
            {
                return null;
            }

            if (!AddDeductions(payslip))
            {
                return null;
            }

            if (!AddPensionInfo(payslip))
            {
                return null;
            }

            if (!AddTaxInfo(payslip))
            {
                return null;
            }

            return payslip;
        }
        public Payslip CreateAnonymousPayslip()
        {
            Payslip payslip = new Payslip();
            payslip.PaymentDate = DateTime.Today;
            payslip.PrintedBy = "System";
            payslip.PrintedOn = DateTime.Today;

            //employee info
            payslip.EmpName = "System";
            payslip.EmpNo = "System";
            payslip.EmployeeId = -1;
            payslip.PayPoint = "System";  //HQ, etc

            payslip.PINNo = "System";
            payslip.EmpGroup = "System"; //PP,Temp
            payslip.EmpPayroll = "System"; //Exec; surb
            payslip.Period = DateTime.Today.Month;
            payslip.Year = DateTime.Today.Year;

            //employer info    
            payslip.EmployerId = -1;
            payslip.EmployerName = "System";
            payslip.EmployerAddress = "System";
            payslip.EmployerTelephone = "System";

            //Payslip
            payslip.BasicPay = 0;

            //payslip.Benefits = null;
            payslip.Variables = 0;
            //payslip.StatutoryDeductions = new List<EarningsDeductions>();
            //payslip.OtherDeductions = 0;
            payslip.Earnings = new List<EarningsDeductions>();
            //payslip.Deductions = new List<EarningsDeductions>();

            //Payslip summary
            //Tax Details
            payslip.GrossTaxableEarnings = _Basic;
            // payslip.AllowableDeductions = this.AllowableDeductions();
            payslip.MortgageRelief = 0;

            List<TaxBracket> tb = new List<TaxBracket>();
            payslip.GrossTax = this.PAYELookUP(_Basic, ref tb); //For anonymous only this.GrossTax();
            payslip.TaxBracketList = tb;

            payslip.PersonalRelief = _defaultRelief_Married;

            //PensionTotals
            payslip.PensionEmployer = 0;
            payslip.PensionEmployee = 0;

            //Payment
            payslip.BankBranch = "";
            payslip.Account = "";

            return payslip;
        }
        #endregion

        #region private build members
        private bool AddEmployeeEmployer(Payslip payslip)
        {
            try
            {
                payslip.PaymentDate = _PaymentDate;
                payslip.PrintedBy = _User;
                payslip.PrintedOn = DateTime.Today;

                //emp info
                payslip.EmpName = employee.Surname.Trim() + ", " + employee.OtherNames;
                payslip.EmpNo = employee.EmpNo;
                payslip.EmployeeId = employee.Id;
                payslip.PayPoint = employee.PayPoint;  //HQ, etc

                payslip.PINNo = employee.PINNo;

                var _departmentquery = from dp in db.Departments
                                       where dp.Id == employee.DepartmentId
                                       select dp;
                Department _department = _departmentquery.FirstOrDefault();

                payslip.Department = _department.Description;
                payslip.NHIFNO = employee.NHIFNo;
                payslip.NSSFNO = employee.NSSFNo;
                payslip.EmpGroup = employee.EmpGroup; //PP,Temp
                payslip.EmpPayroll = employee.EmpPayroll; //Exec; surb
                payslip.Period = _PaymentPeriod;
                payslip.Year = _Year;

                //employer info    
                payslip.EmployerName = employer.Name;
                payslip.EmployerId = employer.Id;
                payslip.EmployerAddress = employer.Address1 + ", " + employer.Address2;
                payslip.EmployerTelephone = employer.Telephone;
                payslip.EmployerEmail = employer.Email;

                //Payment
                payslip.BankBranch = employee.BankCode;
                payslip.Account = employee.BankAccount;

                return true;
            }
            catch (Exception e)
            {
                Log.WriteToErrorLogFile(e);
                return false;
            }
        }

        private bool AddPensionInfo(Payslip payslip)
        {
            try
            {
                //PensionTotals
                decimal employeeContribution = 0, employercontribution = 0;
                decimal empAmt1 = 0, emplAmt1 = 0, empAmt2 = 0, emplAmt2 = 0, empAmt3 = 0, emplAmt3 = 0;

                empAmt1 = this.GetEmployeeContribution();
                emplAmt1 = this.GetEmployerContribution();

                empAmt2 = this.GetEmployeeContribution();
                emplAmt2 = 0;

                empAmt3 = empAmt1 + empAmt2;
                emplAmt3 = emplAmt1 + emplAmt2;
                switch (pensionSchemeFlag)
                {
                    case "1":
                        /*
                         * Pension is contributory at a rate defined in settings a
                         * PEN1E -employee contribution rate  
                         * PEN1R - emplyer contribution rate
                         * Pensionable earnings are defined by earnings marked with AddToPension flag
                         * 
                         * Employee only or Employer only contribution scheme can be achieved by setting
                         * PEN1E or PEN1R rates to 0
                         */
                        employeeContribution = empAmt1;
                        employercontribution = emplAmt1;
                        break;
                    case "2":
                        /*
                         * Only the employee contributes.
                         * The amount is set by adding pension item as a payroll item with 
                         * ITEMTYPE = EMPECONTR 	
                         * TAXTRACKING = DEDUCTIBLE
                         * The items must also be named Pension
                         */
                        employeeContribution = empAmt2;
                        employercontribution = emplAmt2;
                        break;
                    case "3":
                        /*
                         *Mixed case of 1 and 2
                         */
                        employeeContribution = empAmt3;
                        employercontribution = emplAmt3;
                        break;

                }
                payslip.PensionEmployer = employercontribution;
                payslip.PensionEmployee = employeeContribution;

                UpdateEmployeePension(pensionSchemeFlag, payslip);

                //update employerbank contribution
                payslip.NSSFEmployer = decimal.Parse(rep.SettingLookup("EMPNSSF"));

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }

        private void UpdateEmployeePension(string pensionSchemeFlag, Payslip payslip)
        {
            if ("1".Equals(pensionSchemeFlag) || "3".Equals(pensionSchemeFlag))
            {

                if (payslip.PensionEmployee > 0)
                {
                    //update Pension deduction that was initially added as a place holder
                    bool found = false;
                    EarningsDeductions d = null;
                    int i = 0;
                    while (!found && i < payslip.AllDeductions.Count)
                    {
                        d = payslip.AllDeductions[i];
                        found = "PENSION".Equals(d.Description.Trim());
                        i++;
                    }

                    if (found)
                    {
                        d.Amount = payslip.PensionEmployee;
                        d.employernssf = payslip.NSSFEmployer;
                    }
                    else
                    {
                        //there must have been an error
                        string msg = "Error in processing pension for " + _EmpNo;
                        throw new Exception(msg);

                    }

                }
            }
        }

        private bool AddTaxInfo(Payslip payslip)
        {
            try
            {

                payslip.HourlyPay = rep.GetHourlyPay(_EmployeeId);

                //Payslip
                /* If hrly is taxable 
                      1. make it the basic
                      2. remove from earnings*/
                if (this.employee.BasicComputation.Equals("H"))
                {
                    this.Basic = payslip.HourlyPay;
                }
                else if (this.employee.BasicComputation.Equals("X"))
                {
                    this.Basic += payslip.HourlyPay;
                }
                else
                {
                }

                payslip.BasicPay = this.Basic + rep.Allowances(_EmployeeId);

                payslip.TotalBenefits = this.NonCashBenefits();
                payslip.Variables = this.rep.NonTaxableEarnings(_EmployeeId); //these are nontaxable allowances
                payslip.Earnings = this.Earnings();


                //Payslip summary
                //Tax Details
                payslip.GrossTaxableEarnings = payslip.BasicPay + payslip.TotalBenefits;


                //must have computed grosstabale earning and allowable deductions
                //Nettaxable requires pension to have been added
                payslip.GrossTax = this.GrossTax(payslip.NetTaxableEarnings);

                payslip.PersonalRelief = this.PersonalRelief();
                payslip.MortgageRelief = this.MortgageRelief();
                payslip.InsuranceRelief = this.InsuranceRelief();

                //update PAYE deduction that was initially added as a place holder
                foreach (var d in payslip.AllDeductions)
                {
                    if ("PAYE".Equals(d.Description.Trim()))
                    {
                        d.Amount = payslip.NetTax;
                        d.IsStatutory = true;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }

        private bool AddDeductions(Payslip payslip)
        {
            /*This adds all deductions in employee transactions including
             *1. zero place holders for  PENSION, PAYE 
             *2. actual values for NSSF, NHIF
             */
            try
            {
                payslip.AllDeductions = this.Deductions();
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }
        #endregion

        //load general settings
        private bool Load()
        {
            try
            {
                //Settings
                decimal.TryParse(rep.SettingLookup("MRELIEF"), out _defaultRelief_Married);
                decimal.TryParse(rep.SettingLookup("SRELIEF"), out _defaultRelief_Single);
                //Settings
                decimal.TryParse(rep.SettingLookup("PEN1E"), out _EmployeeRate);
                decimal.TryParse(rep.SettingLookup("PEN1R"), out _EmployerRate);

                pensionSchemeFlag = rep.SettingLookup("DEFCONTRSCHEME");

                //load employee info
                if (!anonymous)
                {
                    employee = rep.GetEmployee(_EmployeeId);

                    _MaritalStatus = employee.MaritalStatus;

                    decimal _HourlyPay = rep.GetHourlyPay(_EmployeeId);

                    if (this.employee.BasicComputation.Equals("H"))
                    {
                        _Basic = _HourlyPay;
                    }
                    else if (this.employee.BasicComputation.Equals("X"))
                    {
                        _Basic = employee.BasicPay ?? 0;
                        _Basic += _HourlyPay;
                    }
                    else
                    {
                        _Basic = employee.BasicPay ?? 0;
                    }


                    //Employer Record
                    employer = rep.GetEmployeeEmployer(employee.EmployerId);

                    _PensionableEarnings = rep.PensionableEarnings(_EmployeeId);
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                _erorr = true;
                _errMsg = "Error processing Load() \n" + ex.Message;
                return false;
            }

        }

        #region Payslip Item Computations/Lookups

        private decimal Basic
        {
            get
            {
                return _Basic;
            }
            set
            {
                _Basic = value;
            }
        }

        private decimal NonCashBenefits()
        {
            /* Sum all noncash taxable earnings 
             */
            return rep.NonCashBenefits(_EmployeeId);

        }

        /**/
        private decimal PAYELookUP(decimal NetTaxable, ref List<TaxBracket> TaxBracketList)
        {
            /*
             * NetTaxable = GrossTaxtable - allowable deductions
             * 
             */

            decimal minPaye;
            decimal.TryParse(rep.SettingLookup("PAYEEMIN"), out minPaye);

            //If taxable is less than minimim, return NON TAXABLE
            decimal taxable = NetTaxable; //save taxable in a local variable
            if (taxable < minPaye) { return 0; }

            //Lets lookup payee
            decimal payee = 0.0M; decimal amtTaxed;
            List<PayeeRate> taxTable = rep.PayeeRates();
            int bracketId = 0;
            bool cont = true;
            //loop through the taxtable computing the neccessary taxes within the bracket
            //while returning the taxed amount
            foreach (var bracket in taxTable)
            {
                ++bracketId;
                bool isLastBracket = (taxTable.Count == bracketId);
                if (cont)
                {
                    decimal taxinBracket = TaxInBracket(NetTaxable, taxable, bracket.FromAmt, bracket.ToAmt, bracket.Rate, isLastBracket, out  amtTaxed, out cont);

                    payee += taxinBracket;

                    TaxBracketList.Add(new TaxBracket(bracketId, bracket.FromAmt, bracket.ToAmt, bracket.Rate / 100, taxinBracket, amtTaxed));

                }
                else
                    break;

                //keep reducing amt to tax by amt already taxed
                taxable -= amtTaxed;
            }
            return payee;
        }

        private decimal TaxInBracket(decimal TaxableIncome, decimal bal, decimal From, decimal To,
            decimal Rate, bool isLastBracket, out decimal amtTaxed, out bool cont)
        {
            if (isLastBracket)  //last bracket
            {
                amtTaxed = bal;
                decimal tax = amtTaxed * Rate / 100;
                cont = false;
                return tax;
            }
            else if (TaxableIncome > To)  //greator than the bracket
            {
                amtTaxed = (To - From);
                decimal tax = amtTaxed * Rate / 100;
                cont = true;
                return tax;
            }
            else
            {
                amtTaxed = bal;
                decimal tax = amtTaxed * Rate / 100;
                cont = false;
                return tax;
            }
        }

        private decimal GrossTax(decimal NetTaxableAmt)
        {
            //now lookup payee
            List<TaxBracket> tb = new List<TaxBracket>();
            decimal tax = this.PAYELookUP(NetTaxableAmt, ref tb);
            return tax;
        }

        private decimal PersonalRelief()
        {
            decimal emprelief = rep.PersonalRelief(_EmployeeId);
            if (emprelief != 0) return emprelief;

            //emprelief is 0 or not set, return defaault according to marital status;
            if (_MaritalStatus.ToUpper().Equals("S"))
            {
                emprelief = _defaultRelief_Single;
            }
            else
            {
                emprelief = _defaultRelief_Married;
            }

            return emprelief;
        }

        private decimal MortgageRelief()
        {
            return rep.MortgageRelief(_EmployeeId);
        }

        private decimal InsuranceRelief()
        {
            return rep.InsuranceRelief(_EmployeeId);
        }

        private decimal AllowableDeductions()
        {
            /*DEDUCTBLE items; NSSF not in the emp table*/
            return this.NSSF() + rep.AllowableDeductions(_EmployeeId);
        }

        private decimal NHIF()
        {

            return rep.NHIFLookUp(_Basic);
        }

        private decimal NSSF()
        {
            decimal _nssf = 0;
            switch (rep.SettingLookup("NSSFCOMPUTATIONMETHOD").ToUpper())
            {
                case "OLD":
                    //Confirm how it is arrived at
                    _nssf = rep.NSSFLookUp();
                    break;
                case "NEW":
                    int lowerearninglimit = int.Parse(rep.SettingLookup("NSSFMINLOWEREARNINGLIMIT"));
                    int upperearninglimit = int.Parse(rep.SettingLookup("NSSFMAXUPPEREARNINGLIMIT"));
                    decimal _employeecontributionpecentage = int.Parse(rep.SettingLookup("NSSFEMPLOYEECONTRIBUTIONPERCENTAGE"));
                    decimal _employercontributionpercentage = int.Parse(rep.SettingLookup("NSSFEMPLOYERCONTRIBUTIONPERCENTAGE"));
                    NssfContributionsDTO _NssfContributionsDTO = rep.ComputeNssfContributions(this._EmployeeId);
                    _nssf = _NssfContributionsDTO.TotalPensionContribution;
                    break;
            }
            return _nssf;
        }


        private List<EarningsDeductions> Earnings()
        {
            List<EarningsDeductions> alle = rep.EmployeeEarnings(_EmployeeId);
            foreach (var d in alle)
            {
                EarningsDeductions noncash = new EarningsDeductions
                                    {
                                        DEType = "E",
                                        ShowInPayslip = true,
                                        TrackYTD = false,
                                        YTD = 0,
                                        EmpTxnId = d.EmpTxnId,
                                        TaxTracking = "EARNING",
                                        Amount = d.Amount
                                    };
            }
            return alle;
        }

        private List<EarningsDeductions> Deductions() //All deductions
        {
            List<EarningsDeductions> alld = rep.EmployeeDeductions(_EmployeeId);
            foreach (var d in alld)
            {
                if ("NSSF".Equals(d.Description.Trim()))
                {
                    decimal nssf_from_txn = (from etxn in rep.get_all_employee_transactions()
                                             where etxn.EmployeeId.Equals(_EmployeeId)
                                             where etxn.ItemId.Equals("NSSF")
                                             select etxn.Amount).FirstOrDefault();

                    if (employee.BasicComputation.Equals("H") && nssf_from_txn == 0.0M)
                    {
                        d.Amount = 0.0M;
                        d.IsStatutory = true;
                    }
                    else if (employee.BasicComputation.Equals("X") && nssf_from_txn == 0.0M)
                    {
                        d.Amount = 0.0M;
                        d.IsStatutory = true;
                    }
                    else if (employee.BasicComputation.Equals("B") && nssf_from_txn == 0.0M)
                    {
                        d.Amount = 0.0M;
                        d.IsStatutory = true;
                    }
                    else
                    {
                        d.Amount = this.NSSF();
                        d.IsStatutory = true;
                    }
                }
                else if ("NHIF".Equals(d.Description.Trim()))
                {
                    decimal nhif_from_txn = (from etxn in rep.get_all_employee_transactions()
                                             where etxn.EmployeeId.Equals(_EmployeeId)
                                             where etxn.ItemId.Equals("NHIF")
                                             select etxn.Amount).FirstOrDefault();

                    if (employee.BasicComputation.Equals("H") && nhif_from_txn == 0.0M)
                    {
                        d.Amount = 0.0M;
                        d.IsStatutory = true;
                    }
                    else if (employee.BasicComputation.Equals("X") && nhif_from_txn == 0.0M)
                    {
                        d.Amount = 0.0M;
                        d.IsStatutory = true;
                    }
                    else if (employee.BasicComputation.Equals("B") && nhif_from_txn == 0.0M)
                    {
                        d.Amount = 0.0M;
                        d.IsStatutory = true;
                    }
                    else
                    {
                        d.Amount = this.NHIF();
                        d.IsStatutory = true;
                    }
                }

            }
            return alld;
        }
        private List<NonCashBenefits> EmpNonCashPayments() //All deductions
        {
            List<NonCashBenefits> alld = rep.GetNonCashBenefitsList(_EmployeeId);

            return alld;
        }
        #endregion


        #region Pension

        private decimal EmployeePensionRate
        {
            get
            {
                return _EmployeeRate;
            }
            set
            {
                _EmployeeRate = value;
            }
        }

        private decimal EmployerPensionRate
        {
            get
            {
                return _EmployerRate;
            }
            set
            {
                _EmployerRate = value;
            }
        }

        private decimal GetEmployeeContribution()
        {
            return (_PensionableEarnings * _EmployeeRate) / 100;
        }

        private decimal GetEmployerContribution()
        {
            return (_PensionableEarnings * _EmployerRate) / 100;
        }

        #endregion

    }
}
