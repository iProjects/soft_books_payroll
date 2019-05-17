using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using CommonLib;

namespace BLL.KRA
{
    public class PayrollMasterBuilder
    {
        int _year;
        int _period;
        DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection; 
        bool _current;
        PayrollMasterModel _ViewModel;
        string fileLogo;
        string slogan;

        public PayrollMasterBuilder(DAL.Employer employer, bool current, int period, int year, string Conn)
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

        public PayrollMasterModel GetPayrollMaster()
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
                _ViewModel = new PayrollMasterModel( connection);
                _ViewModel.employeraddress = _employer.Address1.Trim() + " " + _employer.Address2.Trim();
                _ViewModel.employername = _employer.Name;
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.Period = _period;
                _ViewModel.Year = _year;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.paymaster = GetPayrollMasterList();
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
                List<DAL.psuedovwPayrollMaster> _PayrollMasterList = new List<psuedovwPayrollMaster>();

                var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                         where em.EmployerId == _employer.Id
                                         select em.EmpNo;
                List<string> Empnos = _empnosforEmployer.ToList();

                var payrollmasterquery = from p in rep.GetPayrollMaster(_current, _period, _year)
                                         where Empnos.Contains(p.EmpNo)
                                         select p;
                List<DAL.psuedovwPayrollMaster> payrollmaster = payrollmasterquery.ToList();
                foreach (var pay in payrollmaster)
                {
                    if (!_PayrollMasterList.Any(i => i.EmpNo == pay.EmpNo))
                    {
                        _PayrollMasterList.Add(pay);
                    }
                }
                //foreach (var pay in _PayrollMasterList.Select(i=>i.GetMainDeductions.Where(n=>n.Description.Equals("NSSF"))))
                //{
                //    switch (rep.SettingLookup("NSSFCOMPUTATIONMETHOD").ToUpper())
                //    {
                //        case "OLD":
                //            break;
                //        case "NEW":
                //            int lowerearninglimit = int.Parse(rep.SettingLookup("NSSFMINLOWEREARNINGLIMIT"));
                //            int upperearninglimit = int.Parse(rep.SettingLookup("NSSFMAXUPPEREARNINGLIMIT"));
                //            decimal _employeecontributionpecentage = int.Parse(rep.SettingLookup("NSSFEMPLOYEECONTRIBUTIONPERCENTAGE"));
                //            decimal _employercontributionpercentage = int.Parse(rep.SettingLookup("NSSFEMPLOYERCONTRIBUTIONPERCENTAGE"));
                //            //NssfContributionsDTO _NssfContributionsDTO = rep.ComputeNssfContributions(pay.Select(i=>i.);
                //            //pay.NSSF = _NssfContributionsDTO.TotalPensionContribution;
                //            break;
                //    }
                //}

                return _PayrollMasterList;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }


    }
}