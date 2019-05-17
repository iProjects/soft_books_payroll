using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using CommonLib;

namespace BLL.KRA
{
    public class P10ReportMaker
    {

        private P10ReportModel p10;
        private Employee employee;
        private Employer employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        private int _year;
        private string _EmpNo;
        public bool error = false;
        bool _current;
        string fileLogo;
        string slogan;

        public P10ReportMaker(DAL.Employer _employer, bool current, int Yr, string Conn)
        {
            try
            {
                //initialization
                if (string.IsNullOrEmpty(Conn))
                    throw new ArgumentNullException("Conn");
                connection = Conn;

                db = new SBPayrollDBEntities(connection);
                rep = new Repository(connection);

                _year = Yr;
                _current = current;

                employer = _employer;

                fileLogo = employer.Logo;
                slogan = employer.Slogan;
            }
            catch (Exception ex)
            {
                error = true;
                Utils.ShowError(ex);
            }

        }

        public P10ReportModel BuildP10()
        {
            try
            {
                //instatiate the Model class
                p10 = new P10ReportModel();
                p10.Year = _year;
                p10.EmployerPin = employer.PIN;
                p10.EmployerName = employer.Name;
                p10.EmployerAddress = employer.Address1.Trim() + " " + employer.Address2.Trim();
                p10.P10tax = this.GetMonthlyTax();

                return p10;

            }
            catch (Exception ex)
            {
                error = true;
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
            System.Globalization.DateTimeFormatInfo mfi = new

 System.Globalization.DateTimeFormatInfo();

            List<P10TaxRecord> TaxRec = new List<P10TaxRecord>();
            List<PayslipMaster> taxrecordlist = rep.GetEmpTaxRecord(_EmpNo, _year);

            foreach (PayslipMaster pm in taxrecordlist)
            {
                P10TaxRecord tr = new P10TaxRecord();
                //populate
                tr.MonthInt = pm.Period; // all periods = month
                tr.Month = mfi.GetAbbreviatedMonthName(tr.MonthInt);
                tr.PAYETax = pm.PayeTax;
                //tr.AuditTax = this.GetAuditTax;
                //tr.FringeBenefitTax = this.GetFringeTax;

                TaxRec.Add(tr);
            }

            return TaxRec;

        }

        private List<P10TaxRecord> GetEmployeeMonthlyTaxRecords()
        {
            System.Globalization.DateTimeFormatInfo mfi = new

 System.Globalization.DateTimeFormatInfo();

            List<P10TaxRecord> TaxRec = new List<P10TaxRecord>();

            List<psuedovwPayrollMaster> taxrec = rep.GetEmployeeTaxRecord(_current, _EmpNo, _year);

            foreach (psuedovwPayrollMaster pm in taxrec)
            {
                P10TaxRecord tr = new P10TaxRecord();
                //populate
                tr.MonthInt = pm.Period; // all periods = month
                tr.Month = mfi.GetAbbreviatedMonthName(tr.MonthInt);
                tr.PAYETax = pm.PayeTax;
                //tr.AuditTax = this.GetAuditTax;
                //tr.FringeBenefitTax = this.GetFringeTax;

                TaxRec.Add(tr);
            }

            return TaxRec;
        }






    }

}
