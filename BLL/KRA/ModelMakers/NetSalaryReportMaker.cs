using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using CommonLib;

namespace BLL.KRA
{
    public class NetSalaryReportMaker
    {

        NetSalaryReportModel _ViewModel;
        DAL.Employer _employer;
        int _year;
        int _period;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        public bool error = false;
        bool _current;
        string fileLogo;
        string slogan;

        public NetSalaryReportMaker(DAL.Employer employer, bool current, int year, int period, string Conn)
        {
            //initialize
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

        public NetSalaryReportModel GetNetSalaryModel()
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
            _ViewModel = new NetSalaryReportModel();
            _ViewModel.employername = _employer.Name;
            _ViewModel.employeraddress = _employer.Address1.Trim() + " " + _employer.Address2.Trim();
            _ViewModel.employertelephone = _employer.Telephone;
            _ViewModel.CompanyLogo = fileLogo;
            _ViewModel.CompanySlogan = slogan;
            _ViewModel.paymaster = GetPayrollMasterList();
            _ViewModel.employerpin = _employer.PIN;
            _ViewModel.PrintedOn = DateTime.Today;
            _ViewModel.period = _period;
            _ViewModel.year = _year;
            _ViewModel.salarylist = this.PopulateNetSalary();
        }
        private List<SalaryList> PopulateNetSalary()
        {
            List<SalaryList> saccorepayment = new List<SalaryList>();
            List<DAL.psuedovwPayrollMaster> payroll = GetPayrollMasterList();

            foreach (var pm in payroll)
            {
                SalaryList sr = new SalaryList()
                {
                    employeeno = pm.EmpNo,
                    employeename = pm.Surname.Trim() + ",  " + pm.OtherNames.Trim(),
                    employeepin = pm.PINNo,
                    totalamount = pm.NetPay
                };

                saccorepayment.Add(sr);
            }
            return saccorepayment;
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