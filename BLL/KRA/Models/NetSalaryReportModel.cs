using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.KRA
{
   public  class NetSalaryReportModel
    {

        public int year { get; set; }
        public int period { get; set; }
        public DateTime PeriodDate
        {
            get
            {
                return new DateTime(year, period, 1);
            }
        }
        public string ReportName
        {
            get
            {
                return "For the period " + PeriodDate.ToString("MMM-yyyy");
            }
        }
        public string reference { get; set; }
        public string employername { get; set; }
        public string employeraddress { get; set; }
        public string employertelephone { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; } 
        public string employeeno { get; set; }
        public decimal employeenetpay { get; set; }
        public string employeename { get; set; }
        public string employeepin { get; set; }
        public string employerpin { get; set; }
        public DateTime PrintedOn { get; set; }
        public decimal totalamount
        {
            get
            {
                return paymaster.Sum(s => s.NetPay);
            }
        }
        public List<DAL.psuedovwPayrollMaster> paymaster { get; set; }
        public List<DAL.vwPayrollMaster> payrollmaster { get; set; }
        public List<SalaryList> salarylist { get; set; }
    }

   public class SalaryList
   {
       public SalaryList() { }
       public string employeeno { get; set; }
       public string employeename { get; set; }
       public decimal totalamount { get; set; }
       public string employeepin { get; set; }
    }

}
