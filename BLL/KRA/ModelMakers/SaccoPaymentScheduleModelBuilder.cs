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
    public class SaccoPaymentScheduleModelBuilder
    {
        private bool error = false;
        private SaccoPaymentScheduleModel _ViewModel;
        private DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        private int _year;
        private int _period;
        bool _current;
        string fileLogo;
        string slogan;

        //constructor
        public SaccoPaymentScheduleModelBuilder(DAL.Employer employer, bool current, DAL.Payroll payroll, string Conn)
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

        public SaccoPaymentScheduleModel Getsaccopaymentshedule()
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
                _ViewModel = new SaccoPaymentScheduleModel();
                _ViewModel.PeriodDate = new DateTime(_year, _period, 1);
                _ViewModel.employername = _employer.Name;
                _ViewModel.employeraddress = _employer.Address1.Trim() + ", " + _employer.Address2.Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.employertelephone = _employer.Telephone;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.saccorepaymentschedule = this.GetSaccoRePayments();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<saccorepayment> GetSaccoRePayments()
        {
            try
            {
                List<saccorepayment> saccorepayment = new List<saccorepayment>();

                var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                         where em.EmployerId == _employer.Id
                                         select em.EmpNo;
                List<string> Empnos = _empnosforEmployer.ToList();

                List<psuedovwPayslipDetails> saccorepaymentlist = (from i in rep.GetvwPayslipDetails("SACCO", _current, _period, _year)
                                                                   where Empnos.Contains(i.EmpNo)
                                                                   select i).ToList();

                foreach (var pm in saccorepaymentlist)
                {
                    saccorepayment sr = new saccorepayment()
                    {
                        employeename = pm.Surname.Trim() + ", " + pm.OtherNames.Trim(),
                        employeenumber = pm.EmpNo,
                        SaccoDescription = pm.ItemId,
                        monthamount = pm.Amount,
                        ytdamt = pm.YTD
                    };
                    saccorepayment.Add(sr);
                }
                return saccorepayment;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }

























    }
}