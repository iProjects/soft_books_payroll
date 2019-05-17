using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.KRA.Models
{
    public class AdvanceReportModel
    {
       
        public DateTime PrintedOn { get; set; }
        public string employername { get; set; }
        public string employeraddress { get; set; }
        public string employertelephone { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; }  
        public int Year { get; set; }
        public int Period { get; set; }
        public DateTime PeriodDate { get; set; }
        public string ReportName
        {
            get
            {
                return "For  The  Period " + PeriodDate.ToString("MMM-yyyy");
            }
        }
        public List<advance> EmployeAadvanceList { get; set; }
        public string MonthofContribution
        {
            get
            {
                return PeriodDate.ToString("yyyy-MM");
            }
        }
        public decimal _totalAdvance
        {
            get
            {
                return EmployeAadvanceList.Sum(i => i.advanceamount);
            }
        }
    }


    public class advance
    {
        public string employeeno { get; set; }
        public string employeename { get; set; }
        public string payrollitemid { get; set; }
        public decimal advanceamount { get; set; }
        public DateTime dateposted { get; set; }

    }
}
