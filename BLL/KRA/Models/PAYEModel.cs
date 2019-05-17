using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;


namespace BLL.KRA
{
   public  class PAYEModel
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public string employername { get; set; }
        public string employeraddress { get; set; }
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
                return PeriodDate.ToString("MMM-yyyy");
            }
        }

        public string ReportName
        {
            get
            {
                return "For the period " + PeriodDate.ToString("MMM-yyyy");
            }
        }

       // public PayrollItem PayrollItem { get; set; }
        public List<DAL.psuedovwPayrollMaster> PAYEItemList { get; set; }

        public decimal TotalPAYE {
            get {
                return PAYEItemList.Sum(i => i.PayeTax);
            }
        }
        public decimal TotalItems
        {
            get
            {
                return PAYEItemList.Count();
            }
        }
    }


}
