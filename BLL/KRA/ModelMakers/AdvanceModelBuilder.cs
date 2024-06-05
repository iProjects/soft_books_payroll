using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using BLL;
using BLL.DataEntry;
using BLL.KRA.Models;
using CommonLib;
using DAL;

namespace BLL.KRA.ModelMakers
{
    public class AdvanceModelBuilder
    {
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        AdvanceReportModel _ViewModel;
        bool error = false;
        int _year;
        int _period;
        DAL.Employer _employer;
        bool _current;
        string fileLogo;
        string slogan;

        //contructor
        public AdvanceModelBuilder(DAL.Employer employer, bool current, int period, int year, string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _current = current;
            _employer = employer;
            _year = year;
            _period = period;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        //public getter 
        public AdvanceReportModel GetAdvanceModel()
        {
            try
            {
                Build();
                return _ViewModel;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        //private builder
        private void Build()
        {
            try
            {
                _ViewModel = new AdvanceReportModel();
                _ViewModel.PeriodDate = new DateTime(_year, _period, 1);
                _ViewModel.employertelephone = _employer.Telephone.ToString().Trim();
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.employername = _employer.Name.ToString().ToUpper();
                _ViewModel.employeraddress = _employer.Address1.ToString().Trim() + " " + _employer.Address2.ToString().Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.EmployeAadvanceList = this.GetEmployeAadvanceList();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<advance> GetEmployeAadvanceList()
        {
            try
            {
                List<advance> adv = new List<advance>();
                List<psuedovwPayslipDetails> payslipDetails = GetPayslipDetailsList();

                foreach (var ps in payslipDetails)
                {
                    advance _adv = new advance();

                    _adv.employeeno = ps.EmpNo;
                    _adv.employeename = ps.Surname.ToString().Trim().ToUpper() + ",  " + ps.OtherNames.ToString().Trim().ToUpper();
                    _adv.dateposted = ps.PostDate;
                    _adv.advanceamount = ps.Amount;
                    adv.Add(_adv);

                }
                return adv;
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

                var paylistquery = from p in rep.GetvwPayslipDetailsAdvance("ADVANCE", _current, _period, _year)
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