using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using CommonLib;

namespace BLL.KRA
{
    public class P9BReportMaker
    {
        private bool error = false;
        private P9BReportModel p9B;
        DAL.Employer _employer;
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
        public P9BReportMaker(DAL.Employer employer, bool _current, int EmployeeId, string empNo, int Yr, string Conn)
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

        public P9BReportModel GetP9B()
        {
            try
            {
                BuildP9B();
                return p9B;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void BuildP9B()
        {
            try
            {
                p9B = new P9BReportModel();
                p9B.Year = _year;
                p9B.CompName = _employer.Name;
                p9B.EmployerName = _employer.Name;
                p9B.DateOfRegistration = employee.DoE ?? DateTime.Today;
                p9B.EmployeeMainName = employee.Surname;
                p9B.EmployeeOtherNames = employee.OtherNames;
                p9B.EmployeePin = employee.PINNo;
                p9B.EmployerPin = _employer.PIN;
                p9B.NameOfApprovedInstitution = _employer.Name;
                p9B.RegNumberofApprovedInstitution = _employer.PIN;
                p9B.P9BEmpList = this.GetEmployeeMonthlyTax9B();
                //p9B.P9BEmpList = this.GetEmployeeMonthlyTaxRecords();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<EmployeeMonthlyTaxRecord> GetEmployeeMonthlyTax9B()
        {
            try
            {
                System.Globalization.DateTimeFormatInfo mfi = new
    System.Globalization.DateTimeFormatInfo();

                List<EmployeeMonthlyTaxRecord> empTax = new List<EmployeeMonthlyTaxRecord>();

                List<EmployeeMonthlyTaxRecord> taxrec = rep.GetEmployeeMonthlyTaxRecord(current, _EmpNo, _year);

                foreach (EmployeeMonthlyTaxRecord pm in taxrec)
                {
                    EmployeeMonthlyTaxRecord tr = new EmployeeMonthlyTaxRecord();
                    //populate
                    tr.MonthInt = pm.MonthInt; // all periods = month
                    tr.Month = mfi.GetAbbreviatedMonthName(tr.MonthInt);
                    tr.A = pm.A;
                    tr.B = pm.B;
                    tr.C = pm.C;
                    //tr.D  = computed
                    //tr.D=rep.
                    tr.E2 = pm.E2;
                    tr.E3 = decimal.Parse(rep.SettingLookup("DEFCONTR"));
                    decimal maxSavingsPlan = decimal.Parse(rep.SettingLookup("SPLANMAX"));
                    decimal contrSavingsPlan = rep.SavingsPlan(_EmployeeId ,_EmpNo);
                    tr.F = Math.Min(maxSavingsPlan, contrSavingsPlan);
                    //tr.G = computed;
                    tr.H = pm.H;
                    //tr.J = pm.PayeTax;
                    tr.J = pm.J;
                    tr.J1 = pm.J1;
                    //tr.K = computed 

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
        private List<EmployeeMonthlyTaxRecord> GetEmployeeMonthlyTaxRecords()
        {
            try
            {
                System.Globalization.DateTimeFormatInfo mfi = new

    System.Globalization.DateTimeFormatInfo();

                List<EmployeeMonthlyTaxRecord> empTax = new List<EmployeeMonthlyTaxRecord>();

                List<EmployeeMonthlyTaxRecord> taxrec = rep.GetEmployeeMonthlyTaxRecord(current, _EmpNo, _year);

                foreach (EmployeeMonthlyTaxRecord pm in taxrec)
                {
                    EmployeeMonthlyTaxRecord tr = new EmployeeMonthlyTaxRecord();
                    //populate
                    tr.MonthInt = pm.MonthInt; // all periods = month
                    tr.Month = mfi.GetAbbreviatedMonthName(tr.MonthInt);
                    tr.A = pm.A;
                    tr.B = pm.B;
                    tr.C = pm.C;
                    //tr.D  = computed
                    //tr.D=rep.
                    tr.E2 = pm.E2;
                    tr.E3 = decimal.Parse(rep.SettingLookup("DEFCONTR"));
                    decimal maxSavingsPlan = decimal.Parse(rep.SettingLookup("SPLANMAX"));
                    decimal contrSavingsPlan = rep.SavingsPlan(_EmployeeId,_EmpNo);
                    tr.F = Math.Min(maxSavingsPlan, contrSavingsPlan);
                    //tr.G = computed;
                    tr.H = pm.H;
                    //tr.J = pm.PayeTax;
                    tr.J = pm.J;
                    tr.J1 = pm.J1;
                    //tr.K = computed

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