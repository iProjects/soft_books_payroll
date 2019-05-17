using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using CommonLib;

namespace BLL.KRA
{
    public class P9AHospReportMaker
    {
        private bool error = false;
        private P9AHOSPReportModel p9Hosp;
        public DAL.Employer _employer;
        private Employee employee;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        private int _year;
        private string _EmpNo;
        int _EmployeeId;
        bool current;
        string fileLogo;
        string slogan;

        //constructor
        public P9AHospReportMaker(DAL.Employer employer, bool _current, int EmployeeId, string empNo, int Yr, string Conn)
        {
            //initialization
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            current = _current;
            _year = Yr;
            _EmpNo = empNo;
            _EmployeeId = EmployeeId;
            employee = rep.GetEmployee(_EmployeeId);
            _employer = employer;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        public P9AHOSPReportModel GetP9Hosp()
        {
            try
            {
                BuildP9AHosp();

                return p9Hosp;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void BuildP9AHosp()
        {
            try
            {
                p9Hosp = new P9AHOSPReportModel();

                p9Hosp.Year = _year;
                p9Hosp.CompName = _employer.Name;
                p9Hosp.EmployerName = _employer.Name;
                p9Hosp.DateOfRegistration = employee.DoE ?? DateTime.Today;
                p9Hosp.EmployeeMainName = employee.Surname;
                p9Hosp.EmployeeOtherNames = employee.OtherNames;
                p9Hosp.EmployeePin = employee.PINNo;
                p9Hosp.EmployerPin = _employer.PIN;
                p9Hosp.NameOfApprovedInstitution = _employer.Name;
                p9Hosp.PrintedOn = DateTime.Today;
                p9Hosp.RegNumberofApprovedInstitution = _employer.PIN;
                p9Hosp.P9AHospEmpList = this.GetEmployeeMonthlyTax();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<EmployersMonthlyTaxRecord> GetEmployeeMonthlyTax()
        {
            try
            {
                System.Globalization.DateTimeFormatInfo mfi = new
    System.Globalization.DateTimeFormatInfo();

                List<EmployersMonthlyTaxRecord> empTax = new List<EmployersMonthlyTaxRecord>();

                List<EmployersMonthlyTaxRecord> taxrec = rep.GetEmployerTaxRecord(current, _EmployeeId, _EmpNo, _year);

                foreach (EmployersMonthlyTaxRecord pm in taxrec)
                {
                    EmployersMonthlyTaxRecord tr = new EmployersMonthlyTaxRecord();
                    //populate
                    tr.MonthInt = pm.MonthInt; // all periods = month
                    tr.Month = mfi.GetAbbreviatedMonthName(tr.MonthInt);
                    tr.A = pm.A;
                    tr.B = pm.B;
                    tr.C = pm.C;
                    tr.E2 = pm.E2;
                    tr.E3 = decimal.Parse(rep.SettingLookup("DEFCONTR"));
                    decimal maxSavingsPlan = decimal.Parse(rep.SettingLookup("SPLANMAX"));
                    decimal contrSavingsPlan = rep.SavingsPlan(_EmployeeId,_EmpNo);
                    tr.F = Math.Min(maxSavingsPlan, contrSavingsPlan);
                    tr.H = pm.H;
                    tr.J = pm.J;
                    tr.K = pm.K;
                    tr.K1 = pm.K1;
                    //tr.L = computed

                    //add to list
                    empTax.Add(tr);
                }
                return empTax;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private List<EmployersMonthlyTaxRecord> GetEmployeeMonthlyTaxRecords()
        {
            try
            {
                System.Globalization.DateTimeFormatInfo mfi = new

    System.Globalization.DateTimeFormatInfo();

                List<EmployersMonthlyTaxRecord> empTax = new List<EmployersMonthlyTaxRecord>();

                List<EmployersMonthlyTaxRecord> taxrec = rep.GetEmployerTaxRecord(current,_EmployeeId, _EmpNo, _year);

                foreach (EmployersMonthlyTaxRecord pm in taxrec)
                {
                    EmployersMonthlyTaxRecord tr = new EmployersMonthlyTaxRecord();
                    //populate
                    tr.MonthInt = pm.MonthInt; // all periods = month
                    tr.Month = mfi.GetAbbreviatedMonthName(tr.MonthInt);
                    tr.A = pm.A;
                    tr.B = pm.B;
                    tr.C = pm.C;
                    tr.E2 = pm.E2;
                    tr.E3 = decimal.Parse(rep.SettingLookup("DEFCONTR"));
                    decimal maxSavingsPlan = decimal.Parse(rep.SettingLookup("SPLANMAX"));
                    decimal contrSavingsPlan = rep.SavingsPlan(_EmployeeId,_EmpNo);
                    tr.F = Math.Min(maxSavingsPlan, contrSavingsPlan);
                    tr.H = pm.H;
                    tr.J = pm.J;
                    tr.K = pm.K;
                    tr.K1 = pm.K1;
                    //tr.L = computed

                    //add to list
                    empTax.Add(tr);
                }

                return empTax;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }




    }
}