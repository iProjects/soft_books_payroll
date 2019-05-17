using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL.KRA.Models
{
    public class StatementModel
    {
       public string employername { get; set; }
       public string employeeno{ get; set; }
       public string itemid { get; set; }
       public string employeraddress { get; set; }
       public string employeename { get; set; }      
       public string reference { get; set; }
       public string CompanyLogo { get; set; }
       public string CompanySlogan { get; set; } 
       public DateTime PrintedOn { get; set; }
       public int year { get; set; }
       public decimal BookBalance { get; set;}
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
       public decimal totalamountin
       {
           get
           {
               return _Statementlist.Sum(e => e.Amountin);
           }
       }
       public decimal totalamountout
       {
           get
           {
               return _Statementlist.Sum(e => e.Amountout);
           }
       }
       public decimal totalEmpContrib
       {
           get
           {
               return _Statementlist.Sum(e => e.EmpNSSFContrib);
           }
       }
       public decimal RunningBalance
       {
           get
           {
               return totalamountin - totalamountout;
           }
       }
        
       public decimal totalrunningbalance
       {
           get
           {

               return _Statementlist.Sum(e => e.Balance);
           }
       
       }
       public decimal totalContributions
       {
           get
           {

               return _Statementlist.Sum(e => e.TotalContribs);
           }

       }
       public List<StatementDTO> _Statementlist { get; set; }
 
    } 
}
