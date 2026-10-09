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
                return "For The Period  " + PeriodDate.ToString("MMM-yyyy");
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
        public List<DAL.NssfContributionsDTO> _NssfContributions { get; set; }

        public decimal Total
        {
            get
            {
                return PayList.Sum(t => t.NSSF);
            }
        }
        public decimal TotalPensionContribution
        {
            get
            {
                return _NssfContributions.Sum(t => t.TotalPensionContribution);
            }
        }
        public decimal Total_Employee_contribution
        {
            get
            {
                return _NssfContributions.Sum(t => t.TotalEmployeeContribution);
            }
        }
        public decimal Total_Employer_contribution
        {
            get
            {
                return _NssfContributions.Sum(t => t.TotalEmployerContribution);
            }
        }
        public decimal NewNssfTotal { get; set; }





    }
}