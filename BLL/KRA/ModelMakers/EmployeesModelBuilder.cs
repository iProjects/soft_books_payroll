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
                _ViewModel.pe = this.GetEmployees();
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
                                         select em.EmpNo;

                List<string> Empnos = _empnosforEmployer.ToList();

                var payrollmasterquery = from p in rep.GetPayrollMaster(_current, _period, _year)
                                         where Empnos.Contains(p.EmpNo)
                                         select p;

                List<DAL.psuedovwPayrollMaster> employees_payroll = payrollmasterquery.ToList();

                foreach (var emp_pay in employees_payroll)
                {
                    print_employees pe = new print_employees();
                    pe.employeenumber = emp_pay.EmpNo;
                    pe.employeename = emp_pay.Surname + ",  " + emp_pay.OtherNames;

                    var employee_query = from emp in db.Employees
                                         where emp.Id == emp_pay.EmployeeId
                                         select emp;
                    DAL.Employee _employee = employee_query.FirstOrDefault();

                    pe.gender = _employee.Gender;
                    pe.pinnumber = emp_pay.PINNo;
                    pe.idnumber = emp_pay.IDNo;

                    pe.department = emp_pay.Department;
                    pe.dateofemployment = _employee.DoE ?? DateTime.Today;
                    pe.basicpay = (decimal)emp_pay.NetPay;
                    pe.telephone_no = _employee.TelephoneNo;
                    pe.nssf_no = _employee.NSSFNo;
                    pe.nhif_no = _employee.NHIFNo;

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


















    }
}
