using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using CommonLib;


namespace BLL.Payroll
{

    public class CPayroll
    {

        string _User; ///TODO put logged in user
        BLL.DataEntry.DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        //delegate
        public delegate void PayslipCompleteEventHandler(object sender, PayslipCompleteEventArg e);
        //event
        public event PayslipCompleteEventHandler OnCompleteGeneratePayslip;

        public CPayroll(string user, string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new BLL.DataEntry.DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _User = user;
        }

        #region Payroll
        public void RunPayroll(bool simulate, int PayrollPeriod, int year, ref string sError, int EmployerId)
        {
            GeneratePayslipsForAll(simulate, PayrollPeriod, year, ref  sError, EmployerId);
        }

        public void ArchivePayroll(int period, int Year, int EmployerId)
        {
            //Remove previous records first if any
            try
            {
                rep.ClearPayslipDet(period, Year, EmployerId);
                rep.ClearPayslipMaster(period, Year, EmployerId);


                //Transfer records from working table PayslipMaster to payroll
                //and update YTD too
                rep.CopyPayslipsToPayroll(period, Year);

                //Clear PayslipMaster + det table
                rep.ClearPayslipDet_Temp(period, Year, EmployerId);
                rep.ClearPayslipMaster_Temp(period, Year, EmployerId);

                //Mark Payroll as processed
                //rep.MarkPayrollAsClosed(period, Year);
                rep.mark_payroll_for_employer_as_closed(period, Year, EmployerId);

            }
            catch (Exception e)
            {
                Log.WriteToErrorLogFile(e);
            }
        }

        public void LoadArchivedPayroll(int payrollId)
        {
            //Clear PayslipMaster table
            //rep.ClearPayslip();
        }

        #endregion


        #region Payslips
        public bool GeneratePayslipsForAll(bool simulate, int PayrollPeriod, int Year, ref string sError, int EmployerId)
        {
            bool ret = true;

            //cleare records in payslip and payslip det where period === PayrollPeriod
            try
            {
                if (simulate)
                {
                    rep.ClearPayslipDet_Temp(PayrollPeriod, Year, EmployerId);
                    rep.ClearPayslipMaster_Temp(PayrollPeriod, Year, EmployerId);
                }
                else
                {
                    rep.ClearPayslipDet(PayrollPeriod, Year, EmployerId);
                    rep.ClearPayslipMaster(PayrollPeriod, Year, EmployerId);
                }
            }
            catch (Exception e)
            {
                Log.WriteToErrorLogFile(e);
                return false;
            }

            //Recreate transactions
            List<Employee> emps = rep.GetAllActiveEmployeesforEmployer(EmployerId);
            int count = emps.Count();

            for (int value = 0; value < count; value++) //all active 
            {
                Employee emp = emps[value];
                string Msg = string.Empty;
                bool error = false;
                if (!GeneratePayslip(simulate, PayrollPeriod, Year, emp, _User, ref Msg, ref error))
                {
                    Log.WriteToErrorLogFile(new Exception("Payslip for  [" + emp.EmpNo + "] not completed"));
                    ret = false;
                }

                var _departmentquery = from dp in db.Departments
                                       where dp.Id == emp.DepartmentId
                                       select dp;
                Department _department = _departmentquery.FirstOrDefault();

                //raise the event here
                OnCompleteGeneratePayslip(this, new PayslipCompleteEventArg(value + 1, error, Msg, "Emp  No: " + emp.EmpNo + Environment.NewLine + "Name: " + emp.Surname + " " + emp.OtherNames + Environment.NewLine + "Department: " + _department.Description));

            }
            return ret;
        }
        public bool GeneratePayslip(bool simulate, int PayrollPeriod, int Year, Employee Empdvr, string User, ref string Msg, ref bool error)
        {
            string EmpNo = Empdvr.EmpNo.Trim();

            try
            {
                PayslipMaker payMaker = new PayslipMaker(PayrollPeriod, Year, Empdvr.Id, EmpNo, User, connection);
                if (!payMaker._erorr)
                {
                    try
                    {
                        Payslip payslip = payMaker.CreatePayslipFromTransactions();
                        if (payslip == null)
                        {
                            Msg = "Payslip for [" + EmpNo.Trim() + "] not successful \n Error = See error log file";
                            Log.WriteToErrorLogFile(new Exception(Msg));
                            error = true;
                            return false;
                        }

                        string saveErr = string.Empty;
                        if (!SavePayslip(simulate, payslip, ref saveErr))
                        {
                            Msg = "Payslip for [" + EmpNo.Trim() + "] not successful\n Error = " + saveErr;
                            Log.WriteToErrorLogFile(new Exception(Msg));
                            error = true;
                            return false;
                        }


                        Msg = "Payslip for [" + EmpNo.Trim() + "] Month [" + PayrollPeriod + "] Year [" + Year + "] successfuly completed";
                        Log.WriteToErrorLogFile(new Exception(Msg));
                        return true;

                    }
                    catch (Exception ex)
                    {
                        Log.WriteToErrorLogFile(ex);
                        Utils.ShowError(ex);
                        return false;
                    }
                }
                else
                {
                    Msg = "Payslip for [" + EmpNo.Trim() + "] not successful\n Error = " + payMaker._errMsg;
                    Log.WriteToErrorLogFile(new Exception(Msg));
                    error = true;
                    return false;
                }

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                Utils.ShowError(ex);
                return false;
            }
        }

        public bool SavePayslip(bool Simulate, Payslip payslip, ref string error)
        {
            //call the appropraite rep.save method
            try
            {
                rep.SavePayslip(Simulate, payslip);
                error = "";
                return true;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
                error = ex.Message;
                return false;
            }
        }

        #endregion


    }

    public class PayslipCompleteEventArg : System.EventArgs
    {
        private int svalue;
        private bool _error;
        private string _errorMsg;
        private string _template;

        public PayslipCompleteEventArg(int value, bool error, string errorMsg, string template)
        {
            svalue = value;
            _error = error;
            _errorMsg = errorMsg;
            _template = template;
        }

        public int StatusValue
        {
            get { return svalue; }
        }
        public string ErrorMsg
        {
            get { return _errorMsg; }
        }
        public bool Error
        {
            get { return _error; }
        }
        public string _Template
        {
            get { return _template; }
        }
    }


}
