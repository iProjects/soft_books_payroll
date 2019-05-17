using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;


namespace BLL.KRA
{
    public class SheduleReportModel
    {
        public string employername { get; set; }
        public string employeeno { get; set; }
        public string itemid { get; set; }
        public string employeraddress { get; set; }
        public string employeename { get; set; }
        public string CompanyLogo { get; set; }
        public string CompanySlogan { get; set; } 
        public string reference { get; set; }
        public DateTime PrintedOn { get; set; }
        public int year { get; set; }
        public decimal BookBalance { get; set; }
        public decimal InitialAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime PeriodDate { get; set; }

        public string ReportName
        {
            get
            {
                return "For the period " + PeriodDate.ToString("MMM-yyyy");
            }
        }

        public decimal TotalAmount
        {
            get
            {

                return _Schedulelist.Sum(t => t.Amount);
            }
        }
        public decimal totalContributions
        {
            get
            {

                return _Schedulelist.Sum(e => e.TotalContribs);
            }

        }
        public List<ScheduleDTO> _Schedulelist { get; set; }


    }
}
