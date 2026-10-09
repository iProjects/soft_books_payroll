using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using CommonLib;
using BLL.KRA.Models;

namespace BLL.KRA.ModelMakers
{
    public class MpesaEmployeesModelBuilder
    {
        MpesaEmployeesModelReport _ViewModel;
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

        public MpesaEmployeesModelBuilder(DAL.Employer employer, bool current, int period, int year, string Conn)
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


        public MpesaEmployeesModelReport GetEmployeesModel()
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
                _ViewModel = new MpesaEmployeesModelReport();
                _ViewModel.employername = _employer.Name.ToString().ToUpper();
                _ViewModel.employeraddress = _employer.Address1.ToString().Trim() + " " + _employer.Address2.ToString().Trim();
                _ViewModel.employertelephone = _employer.Telephone.ToString().Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.pme = this.GetEmployees();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<print_mpesa_employees> GetEmployees()
        {
            try
            {
                List<print_mpesa_employees> lst_pme = new List<Models.print_mpesa_employees>();

                var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                         where em.EmployerId == _employer.Id
                                         select em.EmpNo;

                List<string> Empnos = _empnosforEmployer.ToList();

                var payrollmasterquery = from p in rep.GetPayrollMaster(_current, _period, _year)
                                         where Empnos.Contains(p.EmpNo)
                                         where p.PaymentMode.Equals("M")
                                         select p;

                List<DAL.psuedovwPayrollMaster> employees_payroll = payrollmasterquery.ToList();

                foreach (var emp_pay in employees_payroll)
                {
                    print_mpesa_employees pme = new print_mpesa_employees();
                    pme.employeenumber = emp_pay.EmpNo;
                    pme.employeename = emp_pay.Surname + ",  " + emp_pay.OtherNames;

                    var employee_query = from emp in db.Employees
                                         where emp.Id == emp_pay.EmployeeId
                                         select emp;
                    DAL.Employee _employee = employee_query.FirstOrDefault();

                    pme.gender = _employee.Gender;
                    pme.pinnumber = emp_pay.PINNo;
                    pme.idnumber = emp_pay.IDNo;

                    pme.department = emp_pay.Department;
                    pme.dateofemployment = _employee.DoE ?? DateTime.Today;
                    pme.basicpay = (decimal)emp_pay.NetPay;

                    switch (emp_pay.PaymentMode)
                    {
                        case "M":
                            pme.paymentmode = "MPESA";
                            break;
                    }

                    pme.phone_no = _employee.TelephoneNo;

                    lst_pme.Add(pme);
                }
                return lst_pme;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }


















    }
}
