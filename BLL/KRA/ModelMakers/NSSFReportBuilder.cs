using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLib;
using DAL;

namespace BLL.KRA
{
    public class NSSFReportBuilder
    {
        NSSFReportModel _ViewModel;
        private int _period;
        public int _year;
        public bool error = false;
        public DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        bool _current;
        string fileLogo;
        string slogan;
        string msFilePDF = "";
        string msFolder = "";
        string _resourcesPath = null;

        public NSSFReportBuilder(DAL.Employer employer, bool current, int period, int year, string Conn)
        {
            try
            {
                //initialization
                if (string.IsNullOrEmpty(Conn))
                    throw new ArgumentNullException("connection");
                connection = Conn;

                db = new SBPayrollDBEntities(connection);
                rep = new Repository(connection);

                _year = year;
                _period = period;
                _current = current;
                _employer = employer;

                fileLogo = _employer.Logo;
                slogan = _employer.Slogan;
            }
            catch (Exception ex)
            {
                error = true;
                Utils.ShowError(ex);
            }
        }
        public NSSFReportBuilder( bool current, int period, int year, string Conn)
        {
            try
            {
                //initialization
                if (string.IsNullOrEmpty(Conn))
                    throw new ArgumentNullException("connection");
                connection = Conn;

                db = new SBPayrollDBEntities(connection);
                rep = new Repository(connection);

                _year = year;
                _period = period;
                _current = current;

                SetResourcePath();

                fileLogo = rep.SettingLookup("COMPANYLOGO");
                slogan = rep.SettingLookup("COMPANYSLOGAN");

                _employer = rep.GetEmployer();

                if (_employer != null)
                {
                    fileLogo = _employer.Logo;
                    slogan = _employer.Slogan;
                }
            }
            catch (Exception ex)
            {
                error = true;
                Utils.ShowError(ex);
            }
        }
        private void SetResourcePath()
        {
            string sRet = string.Empty;
            try
            {
                string dir = rep.SettingLookup("RESOURCEPATH");
                if (!System.IO.Directory.Exists(dir))
                {
                    sRet = msFolder + "Resources\\";
                }
                else
                {
                    sRet = dir;
                }

                this._resourcesPath = sRet;
            }
            catch (Exception e)
            {
                Utils.ShowError(e);
                this._resourcesPath = msFolder + "Resources\\";
            }
        }
        public NSSFReportModel GetNSSFReport()
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
        public NSSFReportModel GetNewNSSFReport()
        {
            try
            {
                BuildNewNssf();
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
                _ViewModel = new NSSFReportModel(connection);
                _ViewModel.EmployerCode = _employer.NSSF;
                _ViewModel.EmpAddress = _employer.Address1.Trim() + "  " + _employer.Address2.Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.EmployerName = _employer.Name;
                _ViewModel.Period = _period;
                _ViewModel.Year = _year;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.PayList = this.GetNssfPayList(); 
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
            }
        }
        private void BuildNewNssf()
        {
            try
            {
                _ViewModel = new NSSFReportModel(connection);
                _ViewModel.EmployerCode = _employer.NSSF;
                _ViewModel.EmpAddress = _employer.Address1.Trim() + "  " + _employer.Address2.Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.EmployerName = _employer.Name;
                _ViewModel.Period = _period;
                _ViewModel.Year = _year;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.PayList = this.GetNewNssfPayList();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<DAL.psuedovwPayrollMaster> GetNssfPayList()
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
        private List<DAL.psuedovwPayrollMaster> GetNewNssfPayList()
        {
            try
            {
                List<DAL.psuedovwPayrollMaster> _NssfPayList = new List<psuedovwPayrollMaster>(); 

                var paylistquery = from p in rep.GetPayrollMaster(_current, _period, _year) 
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