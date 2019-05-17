using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class PayslipReader
    {
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string _EmpNo;
        int _EmployeeId;
        int _PaymentPeriod;
        int _Year;

        public PayslipReader(int EmployeeId,string emp, int period, int year, string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _EmployeeId = EmployeeId;
            _EmpNo = emp;
            _PaymentPeriod = period;
            _Year = year;
        }

        public Payslip CreatePayslipFromPayslipMaster(bool temp)
        {
            return rep.RetrievePayslip(temp, _EmployeeId,_EmpNo, _PaymentPeriod, this._Year);
        }





    }


    public class NssfComputationModel
    {
        public int id { get; set; }
        public decimal EmployeeEarnings { get; set; }
        public decimal PensionableEarnigs { get; set; }
        public decimal Tier1PensionableEarnings { get; set; }
        public decimal Tier1EmployeeDeduction { get; set; }
        public decimal Tier1EmployerContribution { get; set; }
        public decimal Tier1TotalContribution { get; set; }
        public decimal Tier2PensionableEarnings { get; set; }
        public decimal Tier2EmployeeDeduction { get; set; }
        public decimal Tier2EmployerContribution { get; set; }
        public decimal Tier2TotalContribution { get; set; }
        public decimal TotalPensionContribution { get; set; }

    }
}
