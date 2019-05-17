using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using CommonLib;

namespace BLL.KRA
{
    public class P10ReportMaker1
    {

        private P10ReportModel p10;
        public DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        private int _year;
        public bool error = false;
        bool _current;
        string fileLogo;
        string slogan;

        public P10ReportMaker1(DAL.Employer employer, bool current, int Yr, string Conn)
        {
            //initialization
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _year = Yr;
            _current = current;
            _employer = employer;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        public P10ReportModel BuildP10()
        {
            try
            {
                //instatiate the Model class
                p10 = new P10ReportModel();
                p10.Year = _year;
                p10.EmployerPin = _employer.PIN;
                p10.EmployerName = _employer.Name;
                p10.EmployerAddress = _employer.Address1.Trim() + " " + _employer.Address2.Trim();
                p10.P10tax = this.GetMonthlyTax();
                return p10;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private decimal GetAuditTax()
        {
            return 0;
        }
        private decimal GetFringeTax()
        {
            return 0;
        }
        private List<P10TaxRecord> GetMonthlyTax()
        {
            try
            {
                System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

                List<P10TaxRecord> TaxRec = new List<P10TaxRecord>();

                var _employeesquery = from emp in db.Employees
                                      where emp.EmployerId == _employer.Id
                                      select emp;
                List<Employee> _Employees = _employeesquery.ToList();
                foreach (var employee in _Employees)
                {
                    List<EmployersMonthlyTaxRecord> taxrecordlist = rep.GetEmployerTaxRecord(_current, employee.Id, employee.EmpNo, _year);

                    foreach (EmployersMonthlyTaxRecord pm in taxrecordlist)
                    {
                        P10TaxRecord tr = new P10TaxRecord();
                        //populate
                        tr.MonthInt = pm.MonthInt; // all periods = month
                        tr.Month = mfi.GetAbbreviatedMonthName(tr.MonthInt);
                        tr.PAYETax = pm.J;

                        TaxRec.Add(tr);
                    }
                }
                return TaxRec;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private List<P10TaxRecord> GetEmployeeMonthlyTaxRecords()
        {
            try
            {
                System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
                List<P10TaxRecord> TaxRec = new List<P10TaxRecord>();
                List<psuedovwPayrollMaster> taxrec = GetPayrollMasterList();

                foreach (psuedovwPayrollMaster pm in taxrec)
                {
                    P10TaxRecord tr = new P10TaxRecord();
                    //populate
                    tr.MonthInt = pm.Period; // all periods = month
                    tr.Month = mfi.GetAbbreviatedMonthName(tr.MonthInt);
                    tr.PAYETax = pm.PayeTax;

                    TaxRec.Add(tr);
                }
                return TaxRec;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private List<DAL.psuedovwPayrollMaster> GetPayrollMasterList()
        {
            try
            {
                List<DAL.psuedovwPayrollMaster> _lstPayrollMaster = new List<psuedovwPayrollMaster>();

                var _employeequery = from em in rep.GetAllActiveEmployees()
                                     where em.EmployerId == _employer.Id
                                     select em;
                List<Employee> _Employees = _employeequery.ToList();

                foreach (var employee in _Employees)
                {
                    var _taxrecordsquery = from p in rep.GetvwEmployeeTaxRecord(_current, employee.Id, employee.EmpNo, _year)
                                           select p;
                    List<DAL.psuedovwPayrollMaster> _TaxRecords = _taxrecordsquery.ToList();
                    foreach (var tax in _TaxRecords)
                    {
                        if (!_lstPayrollMaster.Any(i => i.EmpNo == tax.EmpNo))
                        {
                            _lstPayrollMaster.Add(tax);
                        }
                    }
                }
                return _lstPayrollMaster;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }



    }
}