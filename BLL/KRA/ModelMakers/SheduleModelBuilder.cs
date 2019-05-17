using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using CommonLib;

namespace BLL.KRA
{
    public class SheduleModelBuilder
    {
        private bool error = false;
        private SheduleReportModel _ViewModel;
        private Employer _employer;
        private PayrollItem _payrollitem;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        private int _year;
        private int _period;
        bool current;
        string fileLogo;
        string slogan;

        public SheduleModelBuilder(DAL.Employer employer, bool _current, DAL.Payroll payroll, DAL.PayrollItem payrollitem, string Conn)
        {

            //initialization
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            current = _current;
            _year = payroll.Year;
            _period = payroll.Period;
            _payrollitem = payrollitem;
            _employer = employer;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        public SheduleReportModel Getshedulemodel()
        {
            try
            {
                Build();
                return _ViewModel;
            }
            catch (Exception ex)
            {
                error = true;
                Utils.ShowError(ex);
                return null;
            }
        }
        private void Build()
        {
            try
            {
                _ViewModel = new SheduleReportModel();
                _ViewModel.PeriodDate = new DateTime(_year, _period, 1);
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.employername = _employer.Name;
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.employeraddress = _employer.Address1.ToString().Trim() + " " + _employer.Address2.ToString().Trim();
                _ViewModel.itemid = _payrollitem.Id;
                _ViewModel._Schedulelist = this.GetEmployeesShedule(); 
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<ScheduleDTO> GetEmployeesShedule()
        {
            try
            {
                decimal _empContrib = int.Parse(rep.SettingLookup("EMPNSSF"));
                List<ScheduleDTO> lst = new List<ScheduleDTO>();

                var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                         where em.EmployerId == _employer.Id
                                         select em.EmpNo;
                List<string> Empnos = _empnosforEmployer.ToList();

                List<ScheduleDTO> _ItemTransactions = (from i in rep.GetScheduleDTOfromvwPayslipDet_Temp(_payrollitem.Id, _period, _year)
                                                       where Empnos.Contains(i.EmpNo)
                                                       select i).ToList();

                foreach (ScheduleDTO item in _ItemTransactions)
                {
                    ScheduleDTO sDTO = new ScheduleDTO();
                    sDTO.EmpNo = item.EmpNo;
                    sDTO.EmpName = item.EmpName;
                    sDTO.Amount = item.Amount;
                    if (_empContrib != null)
                    {
                        sDTO.EmpNSSFContrib = _empContrib;
                        sDTO.TotalContribs = sDTO.Amount + sDTO.EmpNSSFContrib;
                    }
                    lst.Add(sDTO);
                }
                return lst;
            }
            catch (Exception ex)
            { 
                Utils.ShowError(ex);
                return null;
            }
        }



























    }
}