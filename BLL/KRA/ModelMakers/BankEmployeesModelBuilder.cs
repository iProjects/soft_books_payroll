using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using CommonLib;
using BLL.KRA.Models;

namespace BLL.KRA.ModelMakers
{
    public class BankEmployeesModelBuilder
    {
        BankEmployeesModelReport _ViewModel;
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

        public BankEmployeesModelBuilder(DAL.Employer employer, bool current, int period, int year, string Conn)
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


        public BankEmployeesModelReport GetEmployeesModel()
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
                _ViewModel = new BankEmployeesModelReport();
                _ViewModel.employername = _employer.Name.ToString().ToUpper();
                _ViewModel.employeraddress = _employer.Address1.ToString().Trim() + " " + _employer.Address2.ToString().Trim();
                _ViewModel.employertelephone = _employer.Telephone.ToString().Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.pbe = this.GetEmployees();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<print_bank_employees> GetEmployees()
        {
            try
            {
                List<print_bank_employees> lst_pbe = new List<Models.print_bank_employees>();

                var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                         where em.EmployerId == _employer.Id
                                         select em.EmpNo;

                List<string> Empnos = _empnosforEmployer.ToList();

                var payrollmasterquery = from p in rep.GetPayrollMaster(_current, _period, _year)
                                         where Empnos.Contains(p.EmpNo)
                                         where p.PaymentMode.Equals("B")
                                         select p;

                List<DAL.psuedovwPayrollMaster> employees_payroll = payrollmasterquery.ToList();

                foreach (var emp_pay in employees_payroll)
                {
                    print_bank_employees pbe = new print_bank_employees();
                    pbe.employeenumber = emp_pay.EmpNo;
                    pbe.employeename = emp_pay.Surname + ",  " + emp_pay.OtherNames;

                    var employee_query = from emp in db.Employees
                                         where emp.Id == emp_pay.EmployeeId
                                         select emp;
                    DAL.Employee _employee = employee_query.FirstOrDefault();

                    pbe.gender = _employee.Gender;
                    pbe.pinnumber = emp_pay.PINNo;
                    pbe.idnumber = emp_pay.IDNo;

                    pbe.department = emp_pay.Department;
                    pbe.dateofemployment = _employee.DoE ?? DateTime.Today;
                    pbe.basicpay = (decimal)emp_pay.NetPay;

                    switch (emp_pay.PaymentMode)
                    {
                        case "B":
                            pbe.paymentmode = "BANK";
                            break;
                    }

                    var bank_query = from bnk in db.Banks
                                     where bnk.BankCode == emp_pay.BankCode
                                     select bnk;
                    DAL.Bank _bank = bank_query.FirstOrDefault();

                    DAL.BankBranch _bank_branch = _bank.BankBranches.FirstOrDefault(i => i.BankCode == _bank.BankCode);

                    var branch_query = from bbrn in db.BankBranches
                                       where bbrn.BranchCode == emp_pay.BranchCode
                                       select bbrn;
                    DAL.BankBranch _branch = branch_query.FirstOrDefault();

                    pbe.AccountName = _bank.BankName + " - " + _branch.BranchName;
                    pbe.AccountNo = emp_pay.BankAccount;
                    pbe.bankcode = _bank.BankCode;

                    lst_pbe.Add(pbe);
                }
                return lst_pbe;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }










    }
}
