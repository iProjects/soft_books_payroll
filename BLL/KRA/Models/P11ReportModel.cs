using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.KRA
{
    public class P11ReportModel
    {
        
        public int Year { get; set; }
        public int period { get; set; }
        public string employername { get; set; }
        public string employerpin { get; set; }        
        public string bank { get; set; }
        public DateTime PeriodDate
        {
            get
            {
                return new DateTime(Year, period, 1);
            }
        }

        public string payrollmonth
        {
            get
            {
                return PeriodDate.ToString("MMM-yyyy");
            }
        }
        public string employeraddress { get; set; }
        public string employertelephone { get; set; }
        public List<DAL.psuedovwPayrollMaster> payrollmaster { get; set; }
        public decimal totalpayee
        {
            get
            {
                return payrollmaster.Sum(p => p.PayeTax);
            }
        }
        public string payingofficer { get; set; }
        public List<TaxRecords> P11List { get; set; }
    }


    public class TaxRecords
    {
        public int Year { get; set; }
        public int period { get; set; }
        public string employername { get; set; }
        public string employerpin { get; set; }
        public string bank { get; set; }
    }
}
