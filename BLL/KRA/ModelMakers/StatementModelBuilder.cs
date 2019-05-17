using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using BLL;
using CommonLib;
using BLL.KRA.Models;

namespace BLL.KRA.ModelMakers
{
    public class StatementModelBuilder
    {
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        private bool error = false;
        StatementModel _ViewModel;
        DAL.PayrollItem _payrollitem;
        DAL.Employee employee;
        DAL.Employer _employer;
        int _year;
        int _EmployeeId;
        int _period;
        string itemid;
        EmployeeTransaction emptxn;
        bool _current;
        string fileLogo;
        string slogan;

        public StatementModelBuilder(DAL.Employer employer, bool current, int Yr, int period, int EmployeeId, string empNo, DAL.PayrollItem payrollitem, string Conn)
        {
            //initialize
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _current = current;
            _EmployeeId = EmployeeId;
            employee = rep.GetEmployee(_EmployeeId);
            _employer = employer;
            _ViewModel = new StatementModel();
            _payrollitem = payrollitem;
            _year = Yr;
            _period = period;
            itemid = payrollitem.Id;
            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;

            var et = rep.GetEmployeeTxn(employee.EmpNo, _payrollitem.Id);
            if (et.Count() > 0)
                emptxn = et.First();
        }

        public Models.StatementModel GetStatement()
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
                _ViewModel.year = _year;
                _ViewModel.employername = _employer.Name.Trim();
                _ViewModel.employeename = employee.Surname.Trim() + ",  " + employee.OtherNames.Trim();
                _ViewModel.employeraddress = _employer.Address1.Trim() + " " + _employer.Address2.Trim();
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.itemid = itemid;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.PeriodDate = new DateTime(_year, _period, 1);
                if (emptxn != null)
                {
                    _ViewModel.BookBalance = emptxn.Balance ?? 0;
                    _ViewModel.InitialAmount = emptxn.InitialAmount ?? 0;
                    _ViewModel.StartDate = emptxn.PostDate;
                }
                _ViewModel._Statementlist = this.GetEmployeeStatement();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<StatementDTO> GetEmployeeStatement()
        {
            try
            {
                decimal _empContrib = int.Parse(rep.SettingLookup("EMPNSSF"));
                List<StatementDTO> lst = new List<StatementDTO>();
                List<StatementDTO> _ItemTransactions = rep.GetStatementDTOfromvwPayslipDet(employee.EmpNo, _payrollitem.Id);

                foreach (StatementDTO item in _ItemTransactions)
                {
                    StatementDTO sDTO = new StatementDTO();
                    sDTO.date = item.date;
                    sDTO.Amountin = item.Amountin > 0 ? item.Amountin : 0; //cr
                    sDTO.Amountout = item.Amountout > 0 ? 0 : item.Amountout; //dr
                    sDTO.Description = item.Description;
                    sDTO.Balance = item.Balance;
                    if (_empContrib != null)
                    {
                        sDTO.EmpNSSFContrib = _empContrib;
                        sDTO.TotalContribs = sDTO.Amountin + sDTO.EmpNSSFContrib;
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