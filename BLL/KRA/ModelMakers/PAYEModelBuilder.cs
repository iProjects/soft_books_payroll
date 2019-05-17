using System; 
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq; 
using System.Text;
using System.Text;
using CommonLib; 
using DAL;

namespace BLL.KRA
{
    public class PAYEModelBuilder
    {
        PAYEModel _ViewModel;
        DAL.Employer _employer;
        int _Year;
        int _Period;
        bool _current;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        string fileLogo;
        string slogan;

        public PAYEModelBuilder(Employer employer, bool current, int year, int period,
            string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _employer = employer;
            _Year = year;
            _Period = period;
            _current = current;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        public PAYEModel GetPAYEModel()
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
                _ViewModel = new PAYEModel();
                _ViewModel.Year = _Year;
                _ViewModel.Period = _Period;
                _ViewModel.EmployerCode = _employer.PIN;
                _ViewModel.employeraddress = _employer.Address1.Trim() + " " + _employer.Address2.Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.employername = _employer.Name;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.PAYEItemList = GetPayrollMasterList();
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

                var payrollmasterquery = from p in rep.GetPayrollMaster(_current, _Period, _Year)
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