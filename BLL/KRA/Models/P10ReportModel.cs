using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;

namespace BLL.KRA
{
    public class P10ReportModel
    {
        public int Year { get; set; }

        public string ReportName
        {
            get
            {
                return " P.A.Y.E – EMPLOYER’S CERTIFICATE \n YEAR " + Year + "……………." ;
            }
        }

        public string EmployerPin { get; set; }
        public string EmployerName { get; set; }
        public string EmployerAddress { get; set; }

        public List<P10TaxRecord> P10tax { get; set; }
        public decimal TotalAuditTax { get; set; }
        public decimal TotalFringeBenefitTax { get; set; }
        public decimal TotalPAYETax
        {
            get
            {
                return (from l in this.P10tax
                        select l.PAYETax).Sum();
            }
        }

        
    }

    public class P10TaxRecord
    {
        public string Month { get; set; }
        public int MonthInt { get; set; }
        public decimal PAYETax { get; set; }
        public decimal AuditTax { get; set; }
        public decimal FringeBenefitTax { get; set; }
        

    }
}
    
