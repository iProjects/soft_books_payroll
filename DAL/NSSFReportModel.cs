using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class NSSFReportModel
    {
        SBPayrollDBEntities db;
        Repository rep;
        string connection;

        public NSSFReportModel(string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        public string EmployerName { get; set; }
        public string EmployerCode { get; set; }
        public string EmpAddress { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; }
        public DateTime PrintedOn { get; set; }
        public int Year { get; set; }
        public int Period { get; set; }
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
                return "For  The  Period  " + PeriodDate.ToString("MMM-yyyy");
            }
        }
        public string MonthofContribution
        {
            get
            {
                return PeriodDate.ToString("yyyy-MM");
            }
        }
        public List<DAL.psuedovwPayrollMaster> PayList { get; set; }
        public decimal Total
        {
            get
            {
                decimal TotalNSSF = 0;
                switch (rep.SettingLookup("NSSFCOMPUTATIONMETHOD").ToUpper())
                {
                    case "OLD":
                        TotalNSSF = PayList.Sum(t => t.NSSF + t.EmployerNSSF);
                        break;
                    case "NEW":
                        TotalNSSF = PayList.Sum(t => t.NSSF);
                        break;
                }
                return TotalNSSF;
            }
        }
        public decimal NewNssfTotal { get; set; }





    }
}