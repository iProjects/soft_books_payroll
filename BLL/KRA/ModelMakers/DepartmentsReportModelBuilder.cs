using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using CommonLib;

namespace BLL.KRA.ModelMakers
{
   public  class DepartmentsReportModelBuilder
    {
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        DAL.Employer _employer;
        string fileLogo;
        string slogan;

        public DepartmentsReportModelBuilder(DAL.Employer employer, string Conn)
        {
            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _employer = employer;

            fileLogo = _employer.Logo;
            slogan = _employer.Slogan;
        }


    }
}
