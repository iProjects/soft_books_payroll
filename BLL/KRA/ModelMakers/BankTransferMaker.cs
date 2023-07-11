using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using CommonLib;
using BLL.KRA.Models;

namespace BLL.KRA.ModelMakers
{
    public class BankTransferMaker
    {
        int _year;
        int _period;
        Employer employer;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        bool error = false;
        bool _current;
        BankTransferReportModel _ViewModel;
        string fileLogo;
        string slogan;


        public BankTransferMaker( DAL.Employer _employer, bool current, int period, int year, string Conn)
        {
            try
            {
                if (string.IsNullOrEmpty(Conn))
                    throw new ArgumentNullException("Conn");
                connection = Conn;

                db = new SBPayrollDBEntities(connection);
                rep = new Repository(connection);

                _year = year;
                _current = current;
                _period = period;
                employer = _employer;
                
                fileLogo = _employer.Logo;
                slogan = _employer.Slogan;
            }
            catch (Exception ex)
            {
                error = true;
                Utils.ShowError(ex);
            }
        }

        private void Build()
        {
            try
            {
                _ViewModel = new BankTransferReportModel();
                _ViewModel.employeraddress = employer.Address1.Trim() + "  " + employer.Address2.Trim();
                _ViewModel.employername = employer.Name;
                _ViewModel.EmployerTelephone = employer.Telephone;
                _ViewModel.AccountName = employer.AccountName;
                _ViewModel.AccountNo = employer.AccountNo;
                _ViewModel.CompanyLogo = fileLogo;
                _ViewModel.CompanySlogan = slogan;
                _ViewModel.AccountSignatory = employer.AuthorizedSignatory;
                string bank = "";
                string branch = "Unkown";
                rep.GetBankBranch(employer.BankBranchSortCode, ref bank, ref branch);
                _ViewModel.Bank = bank;
                _ViewModel.BankBranch = branch;
                _ViewModel.Year = _year;
                _ViewModel.Period = _period;
                _ViewModel.PrintedOn = DateTime.Today;
                _ViewModel.BankTransferItems = this.GetBankTransferItem();
            }
            catch (Exception ex)
            {
                error = true;
                Utils.ShowError(ex);
            }
        }

        private List<BankTransferItem> GetBankTransferItem()
        {
            List<BankTransferItem> BankTransferItems = new List<BankTransferItem>();

            var _empnosforEmployer = from em in rep.GetAllActiveEmployees()
                                     where em.EmployerId == employer.Id
                                     select em.EmpNo;

            List<string> Empnos = _empnosforEmployer.ToList();

            var payrollmasterquery = from p in rep.GetPayrollMaster(_current, _period, _year)
                                     where Empnos.Contains(p.EmpNo)
                                     where p.PaymentMode.Equals("B")
                                     select p; 

            List<DAL.psuedovwPayrollMaster> payroll = payrollmasterquery.ToList();

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


        public BankTransferReportModel GetBankTransferModelBuilder()
        {
            if (!error) Build();
            return _ViewModel;
        }


    }
}
