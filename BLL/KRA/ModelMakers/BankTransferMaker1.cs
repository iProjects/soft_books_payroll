using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using CommonLib;
using BLL.KRA.Models;

namespace BLL.KRA.ModelMakers
{
    public class BankTransferMaker1
    {
        int _year;
        int _period;
        DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        bool error = false;
        bool _current;
        BankTransferReportModel _ViewModel;
        string fileLogo;
        string slogan;


        public BankTransferMaker1(DAL.Employer employer, bool current, int period, int year, string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _year = year;
            _current = current;
            _period = period;
            _employer = employer;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }

        public BankTransferReportModel GetBankTransferModelBuilder()
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
                _ViewModel = new BankTransferReportModel();
                _ViewModel.employeraddress = _employer.Address1.Trim() + "  " + _employer.Address2.Trim();
                _ViewModel.employername = _employer.Name;
                _ViewModel.EmployerTelephone = _employer.Telephone;
                _ViewModel.AccountName = _employer.AccountName;
                _ViewModel.AccountNo = _employer.AccountNo;
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.AccountSignatory = _employer.AuthorizedSignatory;  
                _ViewModel.Bank = rep.GetEmployerDefaultBank(_employer);
                _ViewModel.BankBranch = rep.GetEmployerDefaultBankBranch(_employer);
                _ViewModel.Year = _year;
                _ViewModel.Period = _period;
                _ViewModel.PrintedOn = DateTime.Today;
                //_ViewModel.BankTransferItems = this.GetBankTransferItem();
               // _ViewModel.EmployerBankTransferItems = this.GetBankTransferItem();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<EmployerBankTransferItem2> GetBankTransferItem()
        {
            try
            {
                List<EmployerBankTransferItem2> _EmployerBankTransferItems = new List<EmployerBankTransferItem2>();
                List<BankTransferItem> _BankTransferItems = new List<BankTransferItem>();
                List<TransferItem> _TransferItems = new List<TransferItem>();
                 
                var employeesBankTransfersquery = from ebt in db.EmployeesBankTransfers
                                                  where ebt.EmployerId == _employer.Id
                                                  select ebt;
                 List<EmployeesBankTransfer> Empnos = employeesBankTransfersquery.ToList();

                //var _employerbanksquery = (from eb in db.EmployerBanks
                //                         join ep in db.Employers on eb.EmployerId equals ep.Id
                //                         where eb.EmployerId == _employer.Id
                //                         where ep.Id == _employer.Id
                //                         select eb.BankSortCode).Distinct();
                //List<string> _banksortcodes = _employerbanksquery.ToList();

                //var _empnosforEmployer = from emt in db.EmployeesBankTransfers
                //                         join em in rep.GetAllActiveEmployees() on emt.EmpNo equals em.EmpNo
                //                         where em.EmployerId == _employer.Id
                //                         where _banksortcodes.Contains(emt.BankSortCode)
                //                         select em.EmpNo;
                //List<string> Empnos = _empnosforEmployer.ToList();

                //List<DAL.psuedovwPayrollMaster> payroll = GetPayrollMasterList();

                //var _bankTransfersquery = from pr in GetPayrollMasterList()
                //                          join em in rep.GetAllActiveEmployees() on pr.EmpNo equals em.EmpNo
                //                          join ep in db.Employers on em.EmployerId equals ep.Id
                //                          join eb in db.EmployerBanks on ep.Id equals eb.EmployerId
                //                          join emt in db.EmployeesBankTransfers on em.EmpNo equals emt.EmpNo
                //                          select new EmployeesBankTransfersModel
                //                          {
                //                              EmpNo = em.EmpNo,
                //                              BankSortCode = emt.BankSortCode,
                //                              EmployerId = ep.Id,
                //                              AccountName = eb.AccountName,
                //                              AccountNo = eb.AccountNo,
                //                              Signatory = eb.Signatory
                //                          };
                //List<EmployeesBankTransfersModel> _bankTransfers = _bankTransfersquery.ToList();

                //foreach (var _banksortcode in _banksortcodes)
                //{
                //    EmployerBankTransferItem _bti = new EmployerBankTransferItem();

                //    _bti.EmployerId = _employer.Id;
                //    _bti._BankSortCode = _banksortcode;
                //    _bti.EmployerBankName = rep.GetBankName(_bti._BankSortCode); 
                //    _bti.EmployerName=rep.GetEmployerName( _bti.EmployerId);

                //    BankTransferItem bti = new BankTransferItem();

                //    bti.BankCode = rep.GetBankCode(_banksortcode);
                //    bti.BankName = rep.GetBankName(_banksortcode);
                //    bti.BranchCode = rep.GetBankBranchCode(_banksortcode);
                //    bti.BranchName = rep.GetBankBranchName(_banksortcode);

                //    foreach (var emp in Empnos)
                //    {
                    //    foreach (var p in payroll.Where(i=>i.EmpNo == emp))
                    //    {
                    //        TransferItem _TransferItem = new TransferItem();

                    //        _TransferItem.AccountNo = p.BankAccount;
                    //        _TransferItem.Amount = p.NetPay;
                    //        _TransferItem.BankSortCode = p.BankSortCode;
                    //        _TransferItem.BranchName = p.BranchName;
                    //        _TransferItem.EmpName = p.Surname.Trim() + ", " + p.OtherNames;
                    //        _TransferItem.EmpNo = p.EmpNo;

                    //        if (!_TransferItems.Any(i => i.EmpNo == _TransferItem.EmpNo))
                    //        {
                    //            _TransferItems.Add(_TransferItem);
                    //        }
                    //    }
                    //}

                    //bti.TransferItems = _TransferItems;

                    //var Items = from p in payroll
                    //            join bk in db.EmployeesBankTransfers on p.EmpNo equals bk.EmpNo
                    //            where bk.BankSortCode == _banksortcode
                    //            where p.BankCode == bti.BankCode
                    //            select new TransferItem
                    //            {
                    //                AccountNo = p.BankAccount,
                    //                Amount = p.NetPay,
                    //                BankSortCode = p.BankSortCode,
                    //                BranchName = p.BranchName,
                    //                EmpName = p.Surname.Trim() + ", " + p.OtherNames,
                    //                EmpNo = p.EmpNo
                    //            };
                    //bti.TransferItems = Items.ToList();

                //foreach (DAL.Bank bank in rep.GetBanks())
                //{
                //    BankTransferItem bti = new BankTransferItem();
                //    bti.BankCode = bank.BankCode;
                //    bti.BankName = bank.BankName;

                //    var Items = from p in payroll
                //                where p.BankCode == bank.BankCode
                //                select new TransferItem
                //                {
                //                    AccountNo = p.BankAccount,
                //                    Amount = p.NetPay,
                //                    BankSortCode = p.BankSortCode,
                //                    BranchName = p.BranchName,
                //                    EmpName = p.Surname.Trim() + ", " + p.OtherNames,
                //                    EmpNo = p.EmpNo
                //                };
                //    bti.TransferItems = Items.ToList();

                    //_BankTransferItems.Add(bti);

                    //_bti.EmployerBankTransferItems = _BankTransferItems;

                    //_EmployerBankTransferItems.Add(_bti);
                //}
                
                return _EmployerBankTransferItems;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private List<DAL.psuedovwPayrollMaster> GetPayrollMasterList()
        {
            try
            {
                List<DAL.psuedovwPayrollMaster> _bankTransfers = new List<psuedovwPayrollMaster>();

                var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                         where em.EmployerId == _employer.Id
                                         select em.EmpNo;
                List<string> Empnos = _empnosforEmployer.ToList();

                var paylistquery = from p in rep.GetPayrollMaster(_current, _period, _year)
                                   where Empnos.Contains(p.EmpNo)
                                   select p;
                List<DAL.psuedovwPayrollMaster> _payrollMasters = paylistquery.ToList();

                foreach (var _bankTransfer in _payrollMasters)
                {
                    if (!_bankTransfers.Any(i => i.EmpNo == _bankTransfer.EmpNo))
                    {
                        _bankTransfers.Add(_bankTransfer);
                    }
                }
                return _bankTransfers;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }













    }
}