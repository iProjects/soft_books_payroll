using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using CommonLib;
using BLL.KRA.Models;

namespace BLL.KRA.ModelMakers
{
    public class LoanRepaymentScheduleModelBuilder
    {
        private bool error = false;
        private LoanRepaymentScheduleModel _ViewModel;
        private DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        private int _year;
        private int _period;
        bool _current;
        string fileLogo;
        string slogan;

        public LoanRepaymentScheduleModelBuilder(DAL.Employer employer, bool current, DAL.Payroll payroll, string Conn)
        {
            //initialization
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _current = current;
            _year = payroll.Year;
            _period = payroll.Period;
            _employer = employer;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        public LoanRepaymentScheduleModel Getloanrepaymentshedule()
        {
            try
            {
                if (!error)
                    Build();
                return _ViewModel;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private void Build()
        {
            try
            {
                _ViewModel = new LoanRepaymentScheduleModel(); 
                _ViewModel.PeriodDate = new DateTime(_year, _period, 1);
                _ViewModel.employername = _employer.Name;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.employeraddress = _employer.Address1.Trim() + ", " + _employer.Address2.Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.employertelephone = _employer.Telephone;
                _ViewModel.loanslist = this.Getloansrepayments();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex); 
            }
        }
        private List<loanrepayments> Getloansrepayments()
        {
            try
            {
                List<loanrepayments> loansrepay = new List<loanrepayments>();

                List<psuedovwPayslipDetails> payslipDetails = GetPayslipDetailsList();

                foreach (var ps in payslipDetails)
                {
                    loanrepayments lr = new loanrepayments()
                    {
                        employeename = ps.Surname.Trim() + ",  " + ps.OtherNames.Trim(),
                        employeenumber = ps.EmpNo,
                        loandescription = ps.ItemId,
                        startdate = ps.PostDate,
                        monthamount = ps.Amount,
                        balance = ps.YTD
                    };

                    loansrepay.Add(lr);
                }
                return loansrepay;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private List<DAL.psuedovwPayslipDetails> GetPayslipDetailsList()
        {
            try
            {
                List<DAL.psuedovwPayslipDetails> _NssfPayList = new List<psuedovwPayslipDetails>();
                var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                         where em.EmployerId == _employer.Id
                                         select em.EmpNo;
                List<string> Empnos = _empnosforEmployer.ToList();

                var paylistquery = from p in  rep.GetvwPayslipDetails("LOAN", _current, _period, _year)
                                   where Empnos.Contains(p.EmpNo)
                                   select p;
                List<DAL.psuedovwPayslipDetails> nssfpaylist = paylistquery.ToList();
                foreach (var pay in nssfpaylist)
                {
                    if (!_NssfPayList.Any(i => i.EmpNo == pay.EmpNo))
                    {
                        _NssfPayList.Add(pay);
                    }
                }
                return _NssfPayList;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }























    }
}