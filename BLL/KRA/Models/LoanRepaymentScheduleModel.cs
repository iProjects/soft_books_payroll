using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;


namespace BLL.KRA.Models
{
    public class LoanRepaymentScheduleModel
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public string employername { get; set; }
        public string employertelephone { get; set; }
        public string employeraddress { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; } 
        public DateTime PrintedOn { get; set; }
        public string ItemId { get; set; }
        public string employeename { get; set; }
        public string employeenumber { get; set; }
        public DateTime PeriodDate { get; set; }
        public decimal balance { get; set; }
        public string loandescription { get; set; }
        public List<DAL.vwPayslipDet> loanrepay { get; set; }
        public List<loanrepayments> loanslist { get; set; }
        public string ReportName
        {
            get
            {
                return "For the period  " + PeriodDate.ToString("MMM-yyyy");
            }
        }

        public decimal TotalMonthAmount
        {
            get
            {
                return loanslist.Sum(t => t.monthamount);
            }
        }

        public decimal TotalBalance
        {
            get
            {
                return loanslist.Sum(t => t.balance);
            }
        }
    }



    public class loanrepayments
    {
        public loanrepayments() { }
        public string employeename { get; set; }
        public string employeenumber { get; set; }        
        public string loandescription { get; set; }
        public DateTime startdate { get; set; }
        public decimal monthamount { get; set; }
        public decimal balance { get; set; }
    }
}
