using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PayrollMasterModel
    {
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public PayrollMasterModel(string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        public string employername { get; set; }
        public string employeraddress { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
        public List<DAL.psuedovwPayrollMaster> paymaster { get; set; }
        public DateTime PeriodDate
        {
            get
            {
                return new DateTime(Year, Period, 1);
            }
        }
        public string ReportName
        {
            get
            {
                return "For the period " + PeriodDate.ToString("MMM-yyyy");
            }
        }
        public DateTime PayPeriod
        {
            get
            {
                return new DateTime(Year, Period, 1);
            }
        }
        public DateTime PrintedOn { get; set; }
        public decimal Salaries
        {
            get
            {
                return paymaster.Sum(s => s.NetPay);
            }
        }
        public decimal NSSF
        {
            get
            {
                return paymaster.Sum(d => d.NSSF);
            }
        }
        public decimal EmployerNSSF
        {
            get
            {
                return paymaster.Sum(d => d.EmployerNSSF);
            }
        }
        public decimal deductionNSSF
        {
            get
            {
                decimal TotalNSSF = 0;
                switch (rep.SettingLookup("NSSFCOMPUTATIONMETHOD").ToUpper())
                {
                    case "OLD":
                        TotalNSSF = NSSF + EmployerNSSF;
                        break;
                    case "NEW":
                        TotalNSSF = NSSF;
                        break;
                }
                return TotalNSSF;
                //return NSSF + EmployerNSSF;
            }
        }
        public decimal NHIF
        {
            get
            {
                return paymaster.Sum(d => d.NHIF);
            }
        }
        public decimal PAYE
        {
            get
            {
                return paymaster.Sum(d => d.PayeTax);
            }
        }
        public decimal TotalBasicPay
        {
            get
            {
                return paymaster.Sum(d => d.BasicPay);
            }
        }
        public decimal TotalEarnings
        {
            get
            {
                return paymaster.Sum(d => d.EmployeeEarnings.Sum(i => i.Amount));
            }
        }
        public decimal TotalDeductions
        {
            get
            {
                return paymaster.Sum(d => d.EmployeeDeductions.Sum(i => i.Amount));
            }
        }
        public int EarningsCount
        {
            get
            {
                return paymaster.Select(i => i.EmployeeEarnings).Count();
            }
        }
        public int DeductionsCount
        {
            get
            {
                return paymaster.Select(i => i.EmployeeDeductions).Count();
            }
        }
        public int TotalOtherDeductions
        {
            get
            {
                return paymaster.Select(i => i.OtherDeductions).Count();
            }
        }
        public decimal TotalSACCODeductions
        {
            get
            {
                return paymaster.Sum(d => d.TotalSACCODeductions);
            }
        }
        public decimal TotalLoansDeductions
        {
            get
            {
                return paymaster.Sum(d => d.TotalLoansDeductions);
            }
        }





    }
}