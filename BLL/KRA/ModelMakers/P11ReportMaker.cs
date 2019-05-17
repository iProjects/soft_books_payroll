using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using CommonLib;
using System.Globalization;

namespace BLL.KRA
{
    public class P11ReportMaker
    {

        P11ReportModel p11reportmodel;
        public DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        int _year;
        int _period;
        bool current;
        string fileLogo;
        string slogan;
        public bool error = false;

        public P11ReportMaker(DAL.Employer employer, bool _current, int period, int year, string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            current = _current;
            _employer = employer;
            _year = year;
            _period = period;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        public P11ReportModel GetP11Model()
        {
            try
            {
                Build();
                return p11reportmodel;
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
                p11reportmodel = new P11ReportModel();
                p11reportmodel.Year = _year;
                p11reportmodel.period = _period;
                p11reportmodel.employername = _employer.Name;
                p11reportmodel.employertelephone = _employer.Telephone;
                p11reportmodel.employeraddress = _employer.Address1 + "," + _employer.Address2;
                p11reportmodel.employerpin = _employer.PIN;
                p11reportmodel.payrollmaster = GetPayrollMasterList();
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

                var payrollmasterquery = from p in rep.GetPayrollMaster(current, _period, _year)
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