using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;

namespace BLL.KRA
{
    public class P10AReportModel
    {
        public int Year { get; set; }
        public string ReportName
        {
            get
            {
                return "DOMESTIC TAXES DEPARTMENT \nEMPLOYER'S QUARTERLY RETURN";
            }
        }
        public string EmployerName { get; set; }
        public string EmployerPin { get; set; }

        public string TaxonLumpSumAuditInterestPenalty { get; set; }
        public List<TaxRecord> P10AList { get; set; }
        public decimal WCPS { get; set; }
        public decimal TotalEmoluments
        {
            get
            {
                return (from l in this.P10AList
                        select l.Emoluments).Sum();
            }
        }

        public decimal TotalTaxDeducted
        {
            get
            {
                return (from l in this.P10AList
                        select l.TaxDeducted).Sum();
            }
        }

    }

    public class TaxRecord
    {
        public string EmployeeName { get; set; }
        public string EmployeePin { get; set; }
        public decimal Emoluments { get; set; }
        public decimal TaxDeducted { get; set; }

    }

}
