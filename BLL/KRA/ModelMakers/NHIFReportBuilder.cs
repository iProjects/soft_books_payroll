using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib;
using DAL;

namespace BLL.KRA
{
    public class NHIFReportBuilder
    {
        public NHIFReportModel _nhifreportmodel;
        private int _period;
        public int _year;
        public bool error = false;
        public DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        NHIFReportModel _ViewModel;
        bool _current;
        string fileLogo;
        string slogan;

        public NHIFReportBuilder(DAL.Employer employer, bool currrent, int period, int year, string Conn)
        {
            //initialization
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _year = year;
            _period = period;
            _current = currrent;
            _employer = employer;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        public NHIFReportModel GetNHIFReport()
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
        private void Build()
        {
            try
            {
                _ViewModel = new NHIFReportModel();
                _ViewModel.EmployerCode = _employer.NHIF;
                _ViewModel.EmpAddress = _employer.Address1.Trim() + "  " + _employer.Address2.Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.EmployerName = _employer.Name;
                _ViewModel.Period = _period;
                _ViewModel.Year = _year;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.PayList = GetPayrollMasterList();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<DAL.psuedovwPayrollMaster> GetPayrollMasterList()
        {
            try
            {
                List<DAL.psuedovwPayrollMaster> _NssfPayList = new List<psuedovwPayrollMaster>();
                var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                         where em.EmployerId == _employer.Id
                                         select em.EmpNo;
                List<string> Empnos = _empnosforEmployer.ToList();

                var paylistquery = from p in rep.GetPayrollMaster(_current, _period, _year)
                                   where Empnos.Contains(p.EmpNo)
                                   select p;
                List<DAL.psuedovwPayrollMaster> nssfpaylist = paylistquery.ToList();
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