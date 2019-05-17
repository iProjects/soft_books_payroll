using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using System.Linq;
using CommonLib;
using BLL.KRA.Models;

namespace BLL.KRA.ModelMakers
{
    public class EmployeesModelBuilder
    {
        EmployeesModelReport _ViewModel;
        public bool error = false;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.Employer _employer;
        string fileLogo;
        string slogan;

        public EmployeesModelBuilder(DAL.Employer employer, string Conn)
        {
            //initialize
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _employer = employer;

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
                _ViewModel.pae = this.GetEmployees();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private List<printallemployees> GetEmployees()
        {
            try
            {
                List<printallemployees> esmr = new List<Models.printallemployees>();
                List<Employee> employees = rep.GetAllActiveEmployeesforEmployer(_employer.Id);
                foreach (var e in employees)
                {
                    printallemployees emp = new printallemployees();
                    emp.employeenumber = e.EmpNo;
                    emp.employeename = e.Surname + ",  " + e.OtherNames;
                    emp.gender = e.Gender;
                    emp.pinnumber = e.PINNo;
                    emp.idnumber = e.IDNo;

                    var _departmentquery = from dp in db.Departments
                                           where dp.Id == e.DepartmentId
                                           select dp;
                    Department _department = _departmentquery.FirstOrDefault();

                    emp.department = _department.Description;
                    emp.dateofemployment = e.DoE ?? DateTime.Today;
                    emp.basicpay = (decimal)e.BasicPay;
                    emp.paymentmode = e.PaymentMode;

                    esmr.Add(emp);
                }
                return esmr;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }


















    }
}
