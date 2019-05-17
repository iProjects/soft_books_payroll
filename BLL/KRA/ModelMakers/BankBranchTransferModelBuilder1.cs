using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using CommonLib;
using BLL.KRA.Models;

namespace BLL.KRA.ModelMakers
{
    public class BankBranchTransferModelBuilder1
    {

        int _year;
        int _period;
        DAL.Employer _employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        bool error = false;
        bool _current;
        BankTransferModel _ViewModel;
        string fileLogo;
        string slogan;

        public BankBranchTransferModelBuilder1(DAL.Employer employer, bool current, int period, int year, string Conn)
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

        public BankTransferModel GetBankBranchTransferModelBuilder()
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
        public BankTransferModel GetBankBranchForTransfers()
        {
            try
            {
                BuildForTransfers();
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
                _ViewModel = new BankTransferModel();
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
                _ViewModel.BankTransferItems = this.GetBankTransferItem();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void BuildForTransfers()
        {
            try
            {
                _ViewModel = new BankTransferModel();
                _ViewModel.employeraddress = _employer.Address1.Trim() + "  " + _employer.Address2.Trim();
                _ViewModel.employername = _employer.Name;
                _ViewModel.EmployerTelephone = _employer.Telephone;
                _ViewModel.AccountName = _employer.AccountName;
                _ViewModel.AccountNo = _employer.AccountNo;
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.AccountSignatory = _employer.AuthorizedSignatory;
                _ViewModel.Bank = rep.GetEmployerDefaultBankForTransfers(_employer);
                _ViewModel.BankBranch = rep.GetEmployerDefaultBankBranchForTransfers(_employer);
                _ViewModel.Year = _year;
                _ViewModel.Period = _period;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.BankTransferItems = this.GetBankBrachItemsForTransfers();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private List<BankTransferItem> GetBankTransferItem()
        {
            try
            {
                List<BankTransferItem> BankTransferItems = new List<BankTransferItem>();
                List<DAL.psuedovwPayrollMaster> payroll = rep.GetPayrollMaster(_current, _period, _year);

                foreach (DAL.Bank bank in rep.GetBanks())
                {
                    BankTransferItem bti = new BankTransferItem();
                    bti.BankCode = bank.BankCode;
                    bti.BankName = bank.BankName;

                    var Items = from p in payroll
                                where p.BankCode == bank.BankCode
                                select new TransferItem
                                {
                                    AccountNo = p.BankAccount,
                                    Amount = p.NetPay,
                                    BankSortCode = p.BankSortCode,
                                    BranchName = p.BranchName,
                                    EmpName = p.Surname.Trim() + ", " + p.OtherNames,
                                    EmpNo = p.EmpNo
                                };
                    bti.TransferItems = Items.ToList();

                    if (Items.Count() > 0)
                        BankTransferItems.Add(bti);

                }
                return BankTransferItems;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
        private List<BankTransferItem> GetBankBrachItemsForTransfers()
        {
            try
            {

                List<EmployerBankTransferItem2> _EmployerBankTransferItems = new List<EmployerBankTransferItem2>();
                List<BankTransferItem> BankTransferItems = new List<BankTransferItem>();
                List<DAL.psuedovwPayrollMaster> payroll = rep.GetPayrollMaster(_current, _period, _year);

                foreach (DAL.Bank bank in rep.GetBanks())
                {
                    BankTransferItem bti = new BankTransferItem();
                    bti.BankCode = bank.BankCode;
                    bti.BankName = bank.BankName;

                    var Items = from p in payroll
                                where p.BankCode == bank.BankCode
                                select new TransferItem
                                {
                                    AccountNo = p.BankAccount,
                                    Amount = p.NetPay,
                                    BankSortCode = p.BankSortCode,
                                    BranchName = p.BranchName,
                                    EmpName = p.Surname.Trim() + ", " + p.OtherNames,
                                    EmpNo = p.EmpNo
                                };
                    bti.TransferItems = Items.ToList();

                    if (Items.Count() > 0)
                        BankTransferItems.Add(bti);

                }
                return BankTransferItems;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                return null;
            }
        }
















    }
}