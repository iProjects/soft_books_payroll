using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.KRA.Models
{
    public class BankEmployeesModelReport
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public DateTime PrintedOn { get; set; }
        public string employername { get; set; }
        public string employeraddress { get; set; }
        public string employertelephone { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; } 
        public List<print_bank_employees> pbe { get; set; }
        public DateTime PeriodDate { get; set; }
        public string ReportName
        {
            get
            {
                return "" ;
            }
        }
        public decimal totalbasic
        {
            get
            {
                return pbe.Sum(i => i.basicpay);
            }
        }
    }
     
    public class print_bank_employees
    {

        public string employeenumber { get; set; }
        public string employeename { get; set; }
        public string gender { get; set; }
        public string pinnumber { get; set; }
        public string idnumber { get; set; }
        public string department { get; set; }
        public DateTime dateofemployment { get; set; }
        public decimal basicpay { get; set; }
        public string paymentmode { get; set; }

        public string Bank { get; set; }
        public string BankBranch { get; set; }
        public string bankcode { get; set; }
        public string AccountName { get; set; }
        public string AccountNo { get; set; }
    
    }
}
