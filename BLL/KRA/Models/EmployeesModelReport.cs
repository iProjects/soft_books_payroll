using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.KRA.Models
{
    public class EmployeesModelReport
    {
        public int Year { get; set; }
        public int Period { get; set; }
        public DateTime PrintedOn { get; set; }
        public string employername { get; set; }
        public string employeraddress { get; set; }
        public string employertelephone { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; }
        public List<print_employees> _lst_employees { get; set; }
        public DateTime PeriodDate { get; set; }
        public string ReportName
        {
            get
            {
                return "";
            }
        }
        public decimal total_basic_pay
        {
            get
            {
                return _lst_employees.Sum(i => i.basic_pay);
            }
        }
        public decimal total_net_pay
        {
            get
            {
                return _lst_employees.Sum(i => i.net_pay_after_every_other_deductions);
            }
        }
        public decimal total_gross_pay
        {
            get
            {
                return _lst_employees.Sum(i => i.gross_taxable_pay);
            }
        }
        public decimal total_paye
        {
            get
            {
                return _lst_employees.Sum(i => i.payee);
            }
        }
        public decimal total_statutory_deductions
        {
            get
            {
                return _lst_employees.Sum(i => i.statutory_deductions);
            }
        }
        public decimal total_voluntary_deductions
        {
            get
            {
                return _lst_employees.Sum(i => i.voluntary_deductions);
            }
        }


    }

    public class print_employees
    {
        public string employeenumber { get; set; }
        public string employeename { get; set; }
        public string gender { get; set; }
        public string pinnumber { get; set; }
        public string idnumber { get; set; }
        public string email { get; set; }
        public string PhoneNo { get; set; }
        public string department { get; set; }
        public DateTime dateofemployment { get; set; }
        public string paymentmode { get; set; }
        public string nssf_no { get; set; }
        public string nhif_no { get; set; }

        //Earnings
        public decimal basic_pay { get; set; }
        public decimal Taxable_Cash_Allowances { get; set; }
        public decimal Total_Non_Cash_Benefits { get; set; }
        public decimal Non_Taxable_Earnings { get; set; }
        public decimal total_all_other_payments { get; set; }
        public decimal gross_taxable_pay { get; set; }

        public decimal gross_taxable_pay_minus_statutory { get; set; }
        public decimal net_pay_after_tax { get; set; }
        public decimal net_pay_after_every_other_deductions { get; set; }
        public decimal net_pay_take_home { get; set; }

        // PAYE
        public decimal payee { get; set; }
        public decimal gross_tax { get; set; }
        public decimal Total_Relief { get; set; }

        // Deductions 
        public decimal statutory_deductions { get; set; }
        public decimal loan_deductions { get; set; }
        public decimal sacco_deductions { get; set; }
        public decimal remaining_deductions { get; set; }
        public decimal voluntary_deductions { get; set; }
        public decimal total_deductions { get; set; }

        // 2/3 Rule Evaluation
        public bool PassTwoThirdsRule { get; set; }
        public string TwoThirdsRuleStatus
        {
            get
            {
                return PassTwoThirdsRule ? "COMPLIANT" : "NON-COMPLIANT";
            }
        }

    }





}
