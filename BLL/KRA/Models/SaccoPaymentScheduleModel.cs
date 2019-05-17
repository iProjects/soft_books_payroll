using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.KRA.Models
{
    public class SaccoPaymentScheduleModel
    {
        
        public string employername { get; set; }
        public string employertelephone { get; set; }
        public string employeraddress { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; } 
        public DateTime PrintedOn { get; set; }
        public string ItemId { get; set; }
        public string employeename { get; set; }
        public string employeenumber { get; set; }
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
        public List<DAL.vwPayslipDet> payslipdetails { get; set; }
        public List<DAL.GetEmpTransaction> employeetransaction { get; set; }
        public List<DAL.vwPayrollMaster> payrollmaster { get; set; }
        public List<saccorepayment> saccorepaymentschedule { get; set; }

        public decimal TotalMonthAmount
        {
            get
            {
                return (from l in this.saccorepaymentschedule
                        select l.monthamount).Sum();
            }
        }
        public decimal TotalShares
        {
            get
            {
                return (from l in this.saccorepaymentschedule
                        select l.ytdamt).Sum();
            }
        }
    }



    public class saccorepayment
    {
        public saccorepayment() { }
        public string employeename { get; set; }
        public string employeenumber { get; set; }
        public string SaccoDescription { get; set; }
        public decimal monthamount { get; set; }
        public decimal ytdamt { get; set; }


    }
}