
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using DAL;
using CommonLib;

namespace BLL.Payroll
{
    public class CPayroll2 : IDisposable
    {
        private readonly string _user;
        private readonly string _connectionString;
        private readonly SBPayrollDBEntities _db;
        private readonly Repository _rep;

        public event EventHandler<PayslipCompleteEventArg> OnCompleteGeneratePayslip;
        public event EventHandler<notificationmessageEventArgs> NotificationEvent;
        public delegate void PayslipCompleteEventHandler(object sender, PayslipCompleteEventArg e);
        public event EventHandler<notificationmessageEventArgs> _notificationmessageEventname;
        string TAG;

        public CPayroll2(string user, string connectionString, EventHandler<notificationmessageEventArgs> notificationmessageEventname)
        {
            if (user == null) throw new ArgumentNullException("user");
            if (connectionString == null) throw new ArgumentNullException("connectionString");

            _user = user;
            _connectionString = connectionString;
            
            TAG = this.GetType().Name;

            _db = new SBPayrollDBEntities(_connectionString);
            _rep = new Repository(_connectionString);
        }

        public bool RunPayroll(bool simulate, int period, int year, ref string msg, int employerId)
        {
            try
            {
                if (simulate)
                {
                    _rep.ClearPayslipDet_Temp(period, year, employerId);
                    _rep.ClearPayslipMaster_Temp(period, year, employerId);
                }
                else
                {
                    _rep.ClearPayslipDet(period, year, employerId);
                    _rep.ClearPayslipMaster(period, year, employerId);
                }

                // Load Settings once for entire run (.NET 4 Optimization)
                PayrollCalculationContext config = LoadPayrollConfig();
                List<Employee> employees = _rep.GetAllActiveEmployeesforEmployer(employerId);
                int totalEmps = employees.Count;

                PayslipMaker2 payMaker = new PayslipMaker2(period, year, _user, _db, _rep, config);

                for (int i = 0; i < totalEmps; i++)
                {
                    Employee emp = employees[i];
                    bool hasError = false; 

                    Payslip payslip = payMaker.CreatePayslipFromTransactions(emp);

                    if (payslip == null || payMaker.HasError)
                    {
                        hasError = true;
                        msg = string.Format("Payslip failed for [{0}]: {1}", emp.EmpNo, payMaker.ErrorMsg);
                        Log.WriteToErrorLogFile(new Exception(msg));
                    }
                    else
                    {
                        try
                        {
                            _rep.SavePayslip(simulate, payslip);
                            msg = string.Format("Payslip for [{0}] completed successfully.", emp.EmpNo);
                        }
                        catch (Exception ex)
                        {
                            hasError = true;
                            msg = string.Format("Error saving payslip [{0}]: {1}", emp.EmpNo, ex.Message);
                            Log.WriteToErrorLogFile(ex);
                        }
                    }

                    var dept = _db.Departments.FirstOrDefault(d => d.Id == emp.DepartmentId);
                    string deptDesc = dept != null ? dept.Description : "N/A";

                    if (OnCompleteGeneratePayslip != null)
                    {
                        string infoText = string.Format("Emp No: {0}{1}Name: {2} {3}{1}Dept: {4}",
                            emp.EmpNo,
                            Environment.NewLine,
                            emp.Surname,
                            emp.OtherNames,
                            deptDesc);

                        OnCompleteGeneratePayslip(this, new PayslipCompleteEventArg(i + 1, hasError, msg, infoText));
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                Notify(string.Format("Fatal error processing payroll: {0}", ex.Message));
                return false;
            }
        }

        public void ArchivePayroll(int period, int year, int employerId)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _rep.ClearPayslipDet(period, year, employerId);
                    _rep.ClearPayslipMaster(period, year, employerId);

                    _rep.CopyPayslipsToPayroll(period, year);

                    _rep.ClearPayslipDet_Temp(period, year, employerId);
                    _rep.ClearPayslipMaster_Temp(period, year, employerId);

                    _rep.mark_payroll_for_employer_as_closed(period, year, employerId);

                    scope.Complete();
                    Notify(string.Format("Payroll closed and archived successfully for Employer {0}, Period {1}/{2}.", employerId, period, year));
                }
                catch (Exception ex)
                {
                    Log.WriteToErrorLogFile(ex);
                    Notify(string.Format("Archiving failed: {0}", ex.Message));
                    throw;
                }
            }
        }

        private PayrollCalculationContext LoadPayrollConfig()
        {
            decimal mRelief = 0M;
            decimal.TryParse(_rep.SettingLookup("MRELIEF"), out mRelief);

            decimal sRelief = 0M;
            decimal.TryParse(_rep.SettingLookup("SRELIEF"), out sRelief);

            decimal peRate = 0M;
            decimal.TryParse(_rep.SettingLookup("PEN1E"), out peRate);

            decimal prRate = 0M;
            decimal.TryParse(_rep.SettingLookup("PEN1R"), out prRate);

            decimal minPaye = 0M;
            decimal.TryParse(_rep.SettingLookup("PAYEEMIN"), out minPaye);

            var payeeRates = _rep.PayeeRates();

            return new PayrollCalculationContext
            {
                DefaultReliefMarried = mRelief,
                DefaultReliefSingle = sRelief,
                EmployeeRate = peRate,
                EmployerRate = prRate,
                PensionSchemeFlag = _rep.SettingLookup("DEFCONTRSCHEME"),
                MinimumPaye = minPaye,
                PayeeRates = payeeRates != null ? payeeRates : new List<PayeeRate>()
            };
        }

        private void Notify(string message)
        {
            if (NotificationEvent != null)
            {
                NotificationEvent(this, new notificationmessageEventArgs(message, "CPayroll"));
            }
        }

        public void Dispose()
        {
            if (_db != null)
            {
                _db.Dispose();
            }
        }
    }
}








