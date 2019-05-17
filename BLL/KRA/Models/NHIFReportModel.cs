using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.KRA
{
    public class NHIFReportModel
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public string EmployerName { get; set; }
        public string EmpAddress { get; set; }
        public string EmployerCode { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; } 
        public DateTime PrintedOn { get; set; }
        public DateTime PeriodDate
        {
            get
            {
                return new DateTime(Year, Period, 1);
            }
        }

        public string MonthofContribution
        {
            get
            {
                return PeriodDate.ToString("yyyy-MM");
            }
        }
        public string ReportName 
        {
            get 
            {
                return "For  The  Period  " + PeriodDate.ToString("MMM-yyyy");
            }
        }
        public List<DAL.psuedovwPayrollMaster> PayList { get; set; }
        public decimal TotalNHIF
        {
            get
            {

                return PayList.Sum(t => t.NHIF);
            }
        }

    }
}
