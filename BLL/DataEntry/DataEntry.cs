using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Criteria;

namespace BLL.DataEntry
{
    public class DataEntry
    {
        string connection;
        Repository rep;

        public DataEntry(string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            rep = new Repository(connection);
        }

        public void Save()
        {
            rep.Save();
        }

        #region Employee

        public int TotalEmployees()
        {
            return rep.TotalEmployees();
        }

        public string LastEmployeeNo()
        {
            return rep.LastEmployeeNo();
        }

        public List<Employee> GetAllEmployees()
        {
            return rep.GetAllEmployees();
        }

        public Employee GetEmployee(int EmployeeId)
        {
            return rep.GetEmployee(EmployeeId);
        }

        public List<Employee> GetEmployeesFromCriteria(List<CriterionItem> Criteria)
        {
            return rep.GetEmployeesFromCriteria(Criteria);
        }

        public List<string> GetEmployeIdsFromCriteria(List<CriterionItem> Criteria)
        {
            return rep.GetEmployeIdsFromCriteria(Criteria);
        }

        public void SaveEmployee(string Empno, string Surname, string Bank, string Account, int Dept,
            int Employer)
        {
            rep.SaveEmployee(Empno, Surname, Bank, Account, Dept, Employer);
        }

        public bool SaveEmployee(Employee emp)
        {
            return rep.SaveEmployee(emp);
        }

        public void AddDefaultEmpTransactions(int employeeid, string empNo, decimal BasicPay, string _User)
        {
            rep.AddDefaultEmpTransactions(employeeid, empNo, BasicPay, _User);
        }

        public void UpdateEmployee(Employee emp)
        {
            rep.UpdateEmployee(emp);
        }

        public void DeleteEmployee(string EmpNo)
        {
            rep.DeleteEmployee(EmpNo);
        }


        #endregion

        #region Employee_Ext

        public void AddEmployeeCustomInfo(Employee_Ext employee_ext)
        {
            rep.AddEmployeeCustomInfo(employee_ext);


        }

        public List<Employee_Ext> GetAllEmployeeCustomInfo(string empno)
        {

            return rep.GetAllEmployeeCustomInfo(empno);

        }


        #endregion

        #region Bank

        public List<Bank> GetBanks()
        {
            return rep.GetBanks();
        }

        public List<vwBankBranch> GetBankBranches()
        {
            return rep.GetBankBranches();
        }

        public bool GetBankBranch(string sortCode, ref string BankName, ref string BranchName)
        {
            return rep.GetBankBranch(sortCode, ref BankName, ref BranchName);
        }

        public IQueryable<Bank> GetBankQuery()
        {
            return rep.GetBankQuery();
        }

        public void SaveBank(string bankcode, string bankname)
        {
            rep.AddNewBank(bankcode, bankname);
        }

        public void AddBankBranch(string branchsortcode, string branchcode, string bankCode, string branchname)
        {
            rep.AddBankBranch(branchsortcode, branchcode, bankCode, branchname);
        }

        public void UpdateBank(string bankcode, string bankname)
        {
            rep.UpdateBank(bankcode, bankname);
        }

        public void DeleteBankBranch(string banksortcode, string bank)
        {
            rep.DeleteBankBranch(banksortcode, bank);
        }

        public void DeleteAllBankBranches(string bank)
        {
            rep.DeleteAllBankBranches(bank);
        }

        public void UpdateBankBranch(string banksortcode, string branchcode, string bank, string branchname)
        {
            rep.UpdateBankBranch(banksortcode, branchcode, bank, branchname);
        }
        public void DeleteBank(string bankcode)
        {
            rep.DeleteBank(bankcode);
        }

        #endregion

        #region Benefits

        public List<Benefit> GetBenefits()
        {
            return rep.GetBenefits();
        }

        public void AddBenefit(string name, decimal rate)
        {
            rep.AddBenefit(name, rate);
        }

        public void DeleteBenefit(int benefitid)
        {
            rep.DeleteBenefit(benefitid);
        }

        public void UpdateBenefit(DAL.Benefit benefit, string name, decimal rate)
        {
            rep.UpdateBenefit(benefit, name, rate);
        }

        #endregion

        #region Emp Benefits

        public void AddEmpBenefit(EmpNonCashBenefit b)
        {
            rep.AddEmpBenefit(b);
        }
        public IQueryable<EmpNonCashBenefit> GetEmpBenefits(string empno)
        {
            return rep.GetEmpBenefits(empno);
        }
        public List<EditEmpNonCashBenefit> GetEditEmpBenefits(string empno)
        {
            return rep.GetEditEmpBenefits(empno);
        }
        public void UpdateEmpBenefits(List<DAL.EditEmpNonCashBenefit> benefits)
        {
            rep.UpdateEmpBenefits(benefits);
        }

        public void DeleteEmpBenefit(EmpNonCashBenefit b)
        {
            rep.DeleteEmpBenefit(b);
        }

        public void DeleteEmpNonCashBenefit(EmpNonCashBenefit b)
        {
            rep.DeleteEmpNonCashBenefit(b);
        }

        #endregion

        #region Employer

        public List<Employer> GetEmployers()
        {
            return rep.GetAllEmployers();

        }
        public List<Employer> GetAllActiveEmployers()
        {
            return rep.GetAllActiveEmployers();
        }

        public Employer GetEmployer(int empl)
        {
            return rep.GetEmployer(empl);

        }

        public void AddEmployer(Employer _employer)
        {
            rep.AddEmployer(_employer);
        }

        public void UpdateEmployer(Employer _employer)
        {
            rep.UpdateEmployer(_employer);
        }

        #endregion

        #region Department

        public List<Department> GetDepartments()
        {
            return rep.GetDepartments();
        }

        public void AddDepartment(string code, string description)
        {
            rep.AddDepartment(code, description);
        }

        public void DeleteDepartment(string code)
        {
            rep.DeleteDepartment(code);
        }
        public void UpdateDepartment(string code, string description)
        {
            rep.UpdateDepartment(code, description);
        }
        #endregion

        #region Employee Payroll Transactions

        /*
         * Procedures in emp txn are:
         * 1. Pack transactions
         * 2. Post packed transactionsa
         */
        public void CreateEmpTxn(int employeeid, DateTime pdate,
            string empno, string payrollItemId, decimal amt, bool recur, bool enable,
            bool processed, bool track, string user, string modifiedby, DateTime modifieddate,
            string auth, DateTime authDate, bool show, decimal ytdAmt, string loantype)
        {
            rep.CreateEmpTxn(employeeid, pdate,
             empno, payrollItemId, amt, recur, enable,
             processed, track, user, modifiedby, modifieddate,
             auth, authDate, show, ytdAmt, loantype);
        }

        public void AddEmployeeAdvance(DateTime pdate,
            string empno, string payrollItemId, decimal amt, bool recur, bool enable,
            bool processed, bool track, string user, string modifiedby, DateTime modifieddate,
            string auth, DateTime authDate)
        {
            rep.AddEmployeeAdvance(pdate,
             empno, payrollItemId, amt, recur, enable,
             processed, track, user, modifiedby, modifieddate,
             auth, authDate);
        }

        public void AddEmployeeLoan(DateTime pdate,
           string empno, string payrollItemId, decimal amt, bool recur, bool enable,
           bool processed, bool track, string user, string modifiedby, DateTime modifieddate,
           string auth, DateTime authDate, decimal ytdAmt, string loantype)
        {
            rep.AddEmployeeLoan(pdate,
             empno, payrollItemId, amt, recur, enable,
             processed, track, user, modifiedby, modifieddate,
             auth, authDate, ytdAmt, loantype);
        }

        public void UpdateEmpTxnBasicPay(int employeeid, DateTime pdate, string empno, string payrollItemId, decimal amt)
        {
            rep.UpdateEmpTxnBasicPay(employeeid, pdate, empno, payrollItemId, amt);
        }

        public void UpdateEmpTxn(int id, DateTime pdate, string empno, string payrollItemId, decimal amt)
        {
            rep.UpdateEmpTxn(id, pdate, empno, payrollItemId, amt);
        }



        public void UpdateEmpTxn(int id, DateTime pdate,
            string empno, string payrollItemId, decimal amt, bool recur, bool enable,
           bool track, decimal ytdAmt, string user, string modifiedby, DateTime modifieddate,
            string auth, DateTime authDate, string loantype)
        {
            rep.UpdateEmpTxn(id, pdate,
             empno, payrollItemId, amt, recur, enable,
            track, ytdAmt, user, modifiedby, modifieddate,
             auth, authDate, loantype);
        }

        public void DeleteEmpTxn(int id)
        {
            rep.DeleteEmpTxn(id);
        }

        public void DeleteNonCashBenefitsEmpTxn(EmployeeTransaction emptn)
        {
        }

        public List<EmployeeTransaction> EmpTxnList(string empno)
        {
            return rep.EmpTxnList(empno);
        }

        #endregion

        #region TaxTracking

        public void CreateTaxTracking(string Id, string Description)
        {
            rep.CreateTaxTracking(Id, Description);
        }

        public void UpdateTaxTracking(string Id, string Description)
        {
            rep.UpdateTaxTracking(Id, Description);
        }

        public List<TaxTracking> ListTaxTracking()
        {
            return rep.ListTaxTracking();
        }

        public void DeleteTaxTracking(string Id)
        {
            rep.DeleteTaxTracking(Id);
        }
        #endregion

        #region PayrollItemType

        public void CreatePayrollItemTypes(string id, string description)
        {
            rep.CreatePayrollItemTypes(id, description);
        }

        public void UpdatePayrollItemTypes(string id, string description)
        {
            rep.UpdatePayrollItemTypes(id, description);
        }
        public void DeletePayrollItemTypes(string id, string description)
        {
            rep.DeletePayrollItemTypes(id, description);

        }
        public List<PayrollItemType> PayrooItemTypeList()
        {
            return rep.PayrooItemTypeList();
        }



        #endregion

        #region PayrollItems

        public void CreatePayrollItems(string id, string itemtype, string taxtracking, string payableto, string glaccount, bool active, bool addtopension, int reference)
        {
            rep.CreatePayrollItems(id, itemtype, taxtracking, payableto, glaccount, active, addtopension, reference);
        }

        public void UpdatePayrollItems(string id, string itemtype, string taxtracking, string payableto,
            string glaccount, bool active, bool addtopension, bool defaultItem, int reference)
        {
            if (defaultItem)
            {
                rep.UpdatePayrollItems(id, glaccount);
            }
            else
            {
                rep.UpdatePayrollItems(id, itemtype, taxtracking, payableto, glaccount, active, addtopension, reference);
            }
        }

        public void DeletePayrollItems(string id, bool defaultItem)
        {
            if (!defaultItem)
            {
                rep.DeletePayrollItems(id);
            }
        }
        public void DeletePayrollItems(string id)
        {
            PayrollItem pi = rep.GetPayrollItem(id);

            if (!pi.DefaultItem.Value)
            {
                rep.DeletePayrollItems(id);
            }
        }
        public PayrollItem GetPayrollItem(string itemId)
        {
            return rep.GetPayrollItem(itemId);
        }
        public List<PayrollItem> GetPayrollItems(string empno)
        {
            return rep.PayrollItemlist(empno);
        }

        public List<Benefit> NonCashBenefitsList(string EmpNo)
        {
            return rep.NonCashBenefitsList(EmpNo);
        }


        public List<PayrollItem> GetPayrollItems()
        {
            return rep.GetPayrollItems();
        }

        public List<PayrollItem> GetEditPayrollItemlist()
        {
            return rep.GetEditPayrollItemlist();
        }

        public List<PayrollItem> GetActivePayrollItems()
        {
            return rep.GetActivePayrollItems();
        }

        #endregion

        #region Transaction Definition

        public void CreateTxnDef(string txncode, string dataentry, string payrollItemId, decimal amt,
            bool enabled, bool recur, bool track)
        {
            rep.CreateTxnDef(txncode, dataentry, payrollItemId, amt, enabled, recur, track);
        }

        public List<TransactionDef> TxnDefList()
        {
            return rep.TxnDefList();
        }
        public TransactionDef GetTxnDef(string TxnCode)
        {
            return rep.GetTxnDef(TxnCode);
        }

        public void DeleteTxnDef(string txncode)
        {
            rep.DeleteTxnDef(txncode);
        }
        public void UpdateTxnDef(string txncode, string dataentry, string payrollItemId, decimal amt,
            bool enabled, bool recur, bool track)
        {
            rep.UpdateTxnDef(txncode, dataentry, payrollItemId, amt, enabled, recur, track);
        }
        #endregion

        #region Packed Transactions

        public void CreatePackedTxn(DateTime packdate, string emp, string txncode, decimal amt, string user,
            bool authorized)
        {
            rep.CreatePackedTxn(packdate, emp, txncode, amt, user, authorized);
        }

        public void CreatePackedTxn(DateTime packdate, string empno, string txncode, decimal amount)
        {
            rep.CreatePackedTxn(packdate, empno, txncode, amount);
        }
        public List<PackedTransaction> GetPackedTxnList()
        {
            return rep.GetPackedTxnList();
        }
        public void ClearPackedTransactions()
        {
            rep.ClearPackedTransactions();
        }
        #endregion

        #region Payroll

        public List<DAL.Payroll> GetPayrolls()
        {
            return rep.GetPayrolls();
        }
        public List<DAL.Payroll> GetPayrolls(int year)
        {
            return rep.GetPayrolls(year);
        }

        public bool WorkingCopyNotClosed(ref int period, ref int year)
        {
            return rep.WorkingCopyNotClosed(ref period, ref year);
        }

        public bool Working_Copy_Not_Closed_for_employer(ref int period, ref int year, ref int employerid)
        {
            return rep.Working_Copy_Not_Closed_for_employer(ref period, ref year, ref employerid);
        }

        public List<int> GetPayrollYears()
        {
            return rep.GetPayrollYears();
        }

        public List<DAL.Payroll> GetAllPayrolls()
        {
            return rep.GetAllPayrolls();
        }

        public List<DAL.Payroll> GetPayrolls(PayrollState pstate)
        {
            if (pstate == PayrollState.OpenProcessed)
            {
                return rep.GetPayrolls(true, true);
            }
            else if (pstate == PayrollState.OpenNotProcessed)
            {
                return rep.GetPayrolls(true, false);
            }
            else if (pstate == PayrollState.NotOpenProcessed)
            {
                return rep.GetPayrolls(false, true);
            }
            else if (pstate == PayrollState.NotOpenNotProcessed)
            {
                return rep.GetPayrolls(false, true);
            }
            else if (pstate == PayrollState.Open)
            {
                return rep.GetOpenPayrolls(true);
            }
            else if (pstate == PayrollState.Closed)
            {
                return rep.GetOpenPayrolls(false);
            }
            else if (pstate == PayrollState.Processed)
            {
                return rep.GetProcessedPayrolls(true);
            }
            else if (pstate == PayrollState.NotProcessed)
            {
                return rep.GetProcessedPayrolls(false);
            }
            else //Default is not processed
            {
                return rep.GetProcessedPayrolls(false);
            }
        }

        public List<DAL.Payroll> GetPayrolls(bool OpenFlag, bool ProcessedFlag)
        {
            return rep.GetPayrolls(OpenFlag, ProcessedFlag);
        }

        public void MarkPayrollAsClosed(int period, int year)
        {
            rep.MarkPayrollAsClosed(period, year);
        }

        public void MarkPayrollAsProcessed(int period, int year)
        {
            rep.MarkPayrollAsProcessed(period, year);
        }

        public void ClearPayroll(int payPeriod, int Year, int EmployerId)
        {
            rep.ClearPayslipDet(payPeriod, Year, EmployerId);
            rep.ClearPayslipMaster(payPeriod, Year, EmployerId);
        }

        public void AddPayroll(int period, int year, int owner, DateTime daterun, string runby,
            bool approved, string approvedby, bool isopen, bool processed)
        {
            rep.AddPayroll(period, year, owner, daterun, runby,
             approved, approvedby, isopen, processed);

        }

        #endregion

        #region User

        public bool Register(string username, string pwd, int roleid)
        {
            return rep.Register(username, pwd, roleid);
        }

        public bool Authenticate(string userId, string pwd, int Maxtries, int tries, ref string message, ref string errCode)
        {
            if (tries > Maxtries)
            {
                message = "Maximum tries exceeded; User locked ";
                errCode = "100";
                rep.LockUser(userId);
                return false;
            }

            if (!rep.CheckUserExists(userId, pwd))
            {
                message = "Username or password not correct";
                errCode = "101";
                return false;
            }

            if (rep.IsUserLocked(userId))
            {
                message = "User locked, please contact the administrator";
                errCode = "102";
                return false;
            }

            ///TODO continue checking all authentiction conditions

            return true;
        }

        public bool ChangePassword(string userId, string pwd)
        {
            return rep.ChangePassword(userId, pwd);
        }

        public void LockUser(string userId)
        {
            rep.LockUser(userId);
        }

        public UserModel GetUser(string userId)
        {
            return rep.GetUser(userId);
        }

        public List<spUser> GetUsers()
        {
            return rep.GetUsers();
        }

        public void AddUser(string username, string password, int roleid, bool locked)
        {
            rep.AddUser(username, password, roleid, locked);
        }

        public void UpdateUser(DAL.spUser user, string password, int roleid, bool locked)
        {
            rep.UpdateUser(user, password, roleid, locked);
        }

        #endregion

        #region Employee Autocomplete
        public string[] AutoComplete_PayPoints()
        {
            return rep.AutoComplete_PayPoints();
        }

        public string[] AutoComplete_EmpGroups()
        {
            return rep.AutoComplete_EmpGroups();
        }

        public string[] AutoComplete_EmpPayRolls()
        {
            return rep.AutoComplete_EmpPayRolls();
        }
        #endregion

        #region setting

        public string SettingLookup(string Key)
        {
            return rep.SettingLookup(Key);
        }

        public List<Setting> GetSettings()
        {
            return rep.GetSettings();
        }


        public void EditSetting(DAL.Setting setting, string ssvalue)
        {

            rep.EditSetting(setting, ssvalue);
        }

        public List<SettingsGroup> GetSettingsGroup()
        {
            return rep.GetSettingsGroup();
        }
        #endregion

        #region PayeeRates

        public List<PayeeRate> GetPayeeRates()
        {
            return rep.GetPayeerates();
        }

        public void AddPayeeRate(decimal fromamount, decimal toamount, decimal rate)
        {
            rep.AddPayeeRate(fromamount, toamount, rate);
        }


        public List<PayeeRate> PayeeRatesTable()
        {
            return rep.PayeeRatesTable();
        }


        #endregion

        #region NHIFRate

        public void AddNHIFRate(decimal fromamount, decimal toamount, decimal rate)
        {
            rep.AddNHIFRate(fromamount, toamount, rate);
        }

        public List<NHIFRate> NHIFTable()
        {
            return rep.NHIFTable();
        }


        #endregion


        #region HourlyPayment

        public void SaveHourlyPayment(HourlyPayment hourlypay)
        {
            rep.SaveHourlyPayment(hourlypay);
        }

        public void UpdateHourlyPayment(HourlyPayment hourlypay)
        {
            rep.UpdateHourlyPayment(hourlypay);
        }

        public void DeleteHourlyPayment(HourlyPayment hourlypay)
        {
            rep.DeleteHourlyPayment(hourlypay);
        }

        public List<HourlyPayment> GetHourlyPaymentsList(string empno)
        {
            return rep.GetHourlyPaymentsList(empno);
        }

        #endregion


    }


    public enum PayrollState
    {
        Open,
        Closed,
        Processed,
        NotProcessed,
        OpenNotProcessed,
        OpenProcessed,
        NotOpenProcessed,
        NotOpenNotProcessed
    }




}