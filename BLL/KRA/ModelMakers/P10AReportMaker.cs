using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using CommonLib;

namespace BLL.KRA
{
    public class P10AReportMaker
    {
        private P10AReportModel p10A;
        public DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        private int _year;
        bool current;
        string fileLogo;
        string slogan;

        public P10AReportMaker(DAL.Employer employer, bool _current, int Yr, string Conn)
        {
            //initialization
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            current = _current;
            _employer = employer;
            _year = Yr;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        public P10AReportModel GetP10A()
        {
            try
            {
                BuildP10A();
                return p10A;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void BuildP10A()
        {
            try
            {
                p10A = new P10AReportModel();
                p10A.Year = _year;
                p10A.EmployerName = _employer.Name;
                p10A.EmployerPin = _employer.PIN;
                p10A.P10AList = this.GetAllEmpTaxRecord();
                p10A.WCPS = this.GetWCPS();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private decimal GetWCPS() ///TODO Complete implementing WCPS when you know what it is.
        {
            return 0; //For now all WCPS = 0
        }
        //loop through all employees collecting tax record
        private List<TaxRecord> GetAllEmpTaxRecord()  //for the year
        {
            try
            {
                List<TaxRecord> empTax = new List<TaxRecord>();
                foreach (var emp in rep.GetAllActiveEmployeesforEmployer(_employer.Id))
                {
                    empTax.Add(GetEmpTaxRecord(emp));
                }
                return empTax;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        //compute tax record for an employee
        private TaxRecord GetEmpTaxRecord(Employee employee)
        {
            try
            {
                TaxRecord tr = new TaxRecord();
                tr.EmployeePin = employee.PINNo;
                tr.EmployeeName = employee.Surname.Trim() + ", " + employee.OtherNames.Trim();
                tr.Emoluments = rep.GetEmployeeTotalEmoluments(current, employee.EmployerId, employee.EmpNo, _year);
                tr.TaxDeducted = rep.GetEmployeeTotalTaxDeducted(current, employee.EmployerId, employee.EmpNo, _year);
                return tr;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }




    }
}