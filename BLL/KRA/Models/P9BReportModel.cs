using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;


namespace BLL.KRA
{
    public class P9BReportModel
    {
        public string CompName { get; set; }
        public int Year { get; set; }
        public string ReportName
        {
            get
            {
                return "DOMESTIC TAXES DEPARTMENT \n TAX DEDUCTION CARD YEAR " + Year + "….";
            }
        }
        public DateTime PrintedOn { get; set; }
        public string EmployerName { get; set; }
        public string EmployeeMainName { get; set; }
        public string EmployeeOtherNames { get; set; }
        public string EmployerPin { get; set; }
        public string EmployeePin { get; set; }
        public List<EmployeeMonthlyTaxRecord> P9BEmpList { get; set; }
        public decimal Total_A
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.A).Sum();
            }
        }
        public decimal Total_B
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.B).Sum();
            }
        }
        public decimal Total_C
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.C).Sum();
            }
        }
        public decimal Total_D
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.D).Sum();
            }
        }
        public decimal Total_E1
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.E1).Sum();
            }
        }
        public decimal Total_E2
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.E2).Sum();
            }
        }
        public decimal Total_E3
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.E3).Sum();
            }
        }
        public decimal Total_F
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.F).Sum();
            }
        }
        public decimal Total_G
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.G).Sum();
            }
        }
        public decimal Total_H
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.H).Sum();
            }
        }
        //public decimal Total_J
        //{
        //    get
        //    {
        //        return (from l in this.P9BEmpList
        //                select l.J).Sum();
        //    }
        //}
        public decimal Total_J
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.TotalRelief).Sum();
            }
        }
        public decimal Total_K
        {
            get
            {
                return (from l in this.P9BEmpList
                        select l.K).Sum();
            }
        }
        public string NameOfApprovedInstitution { get; set; }
        public string RegNumberofApprovedInstitution { get; set; }
        public DateTime DateOfRegistration { get; set; }
        //Pg 2
        //public string DateEmployeeComencedIfDuringYear;
        //public string NameandAddressofOldEmployer;
        //public string DateLeftifDuringYear;
        //public string NameandAddressofNewEmployer;
        //public string MonthlyRent;
        //public string Gratuity;
        //public string CalculationofTaxOnBenefits;
        //public string EmployeeLoan;
        //public string RateDifference;
        //public string MonthlyBenefits;
        //public string MotorCars;
        //public string TotalBenefitinYear;
        //public string EmployerCertificateName;
        //public string EmployerCertificateAddress;
        //public string EmployerCertificateSignature;
        //public string EmployerCertificateDateandStamp;
    }

   
}
