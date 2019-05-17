using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Diagnostics;
using CommonLib;

namespace BLL.Payroll
{
    public class EmployeeReportMaker
    {

        string _User;

        Employee comp;

        Repository rep = new Repository();



        public EmployeeReportMaker(string User)
        {

            _User = User;



            try
            {

                comp = null;// rep.GetEmployee();

            }

            catch (Exception ex)
            {

                //Debug.WriteLine(ex.Message); //log
               Utils.ShowError(ex);
            }

        }



        public EmployeeReportModel CreateListOfEmployeesReport()
        {

            //create a new empreport

            EmployeeReportModel empReprort = new EmployeeReportModel();



            //populate the emp report

            empReprort.CompName = comp.EmpNo;

            empReprort.PrintedOn = DateTime.Now;

            try
            {

                empReprort.EmployeesList = rep.GetAllActiveEmployees();

            }

            catch (Exception ex)
            {

                //Debug.WriteLine(ex.Message); //log
               Utils.ShowError(ex);
            }



            //return the populated report

            return empReprort;

        }


    }
}
