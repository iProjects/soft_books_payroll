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

        public PayslipReader(int EmployeeId, string emp, int period, int year, string Conn)
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
            Payslip _Payslip = new Payslip();

            _Payslip = rep.RetrievePayslip(temp, _EmployeeId, _EmpNo, _PaymentPeriod, this._Year);

            _Payslip.Taxable_Cash_Allowances_Minus_Benefits = Taxable_Cash_Allowances(_Payslip);

            _Payslip.Non_Taxable_Earnings_Only = Non_Taxable_Earnings_Only(_Payslip);

            return _Payslip;
        }

        List<EarningsDeductions> Taxable_Cash_Allowances(Payslip _Payslip)
        {
            List<string> itemtypes = new List<string>() { "ADDITION" };
            List<string> taxtrackings = new List<string>() { "EARNING" };
            List<string> items = new List<string>() { "NON_CASH_BENEFIT", "BASIC" };

            var Taxable_Cash_Allowances = (from d in _Payslip.Earnings 
                                           where itemtypes.Contains(d.ItemType.Trim())
                                           where taxtrackings.Contains(d.TaxTracking.Trim())
                                           where !items.Contains(d.Description.Trim())
                                           select new EarningsDeductions
                                           {
                                               DEType = "E",
                                               ShowInPayslip = d.ShowInPayslip,
                                               TrackYTD = d.TrackYTD,
                                               YTD = (d.TrackYTD) ? (d.YTD) : 0,
                                               Description = d.Description.Trim(),
                                               EmpTxnId = d.EmpTxnId,
                                               TaxTracking = d.TaxTracking.Trim(),
                                               Amount = d.Amount,
                                               ItemType = d.ItemType.Trim()
                                           });

            Taxable_Cash_Allowances = Taxable_Cash_Allowances.ToList();

            return Taxable_Cash_Allowances.ToList();
        }

        List<EarningsDeductions> Non_Taxable_Earnings_Only(Payslip _Payslip)
        {
            List<string> itemtypes = new List<string>() { "ADDITION" };
            List<string> taxtrackings = new List<string>() { "NONE" };
            List<string> items = new List<string>() { "NON_CASH_BENEFIT", "BASIC" };

            var Non_Taxable_Earnings = (from d in _Payslip.Earnings 
                                        where itemtypes.Contains(d.ItemType.Trim())
                                        where taxtrackings.Contains(d.TaxTracking.Trim())
                                        where !items.Contains(d.Description.Trim())
                                        select new EarningsDeductions
                                        {
                                            DEType = "E",
                                            ShowInPayslip = d.ShowInPayslip,
                                            TrackYTD = d.TrackYTD,
                                            YTD = (d.TrackYTD) ? (d.YTD) : 0,
                                            Description = d.Description.Trim(),
                                            EmpTxnId = d.EmpTxnId,
                                            TaxTracking = d.TaxTracking.Trim(),
                                            Amount = d.Amount,
                                            ItemType = d.ItemType.Trim()
                                        });

            Non_Taxable_Earnings = Non_Taxable_Earnings.ToList();

            return Non_Taxable_Earnings.ToList();
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
