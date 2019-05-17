using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;

namespace BLL.Payroll
{
    public class EmployeeReportModel
    {
        public string CompName { get; set; }

        public string ReportName
        {

            get
            {

                return "List of Employees";

            }

        }

        public DateTime PrintedOn { get; set; }

        public List<Employee> EmployeesList { get; set; }

        public int EmpNo;
        public string Surname;
        public string OtherNames;
        public string DoB;
        public string DoE;
        public string MaritalStatus;
        public string Gender;
        public string NSSFNo;
        public string NHIFNo;
        public string PINNo;
        public string BankCode;
        public string IDNo;
        public string BankAccount;
        public string Department;
        public string IsActive;
        public string DateLeft;
        public string PrevEmployer;
        public string BasicPay;
        public string PersonalRelief;
        public string MortgageRelief;
        public string Employer;
        public string PayPoint;
        public string EmpGroup;
        public string EmpPayroll;
       

    }
}
