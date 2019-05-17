using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using CommonLib;
using System.Data.SqlClient;
using System.Data.EntityClient;
using System.Configuration;
using DAL.Criteria;

namespace DAL
{
    public class Repository
    {

        #region "Private Fields"
        SBPayrollDBEntities db;
        #endregion "Private Fields"
        #region "Constructor"
        public Repository()
        {

            //Should be called by login service only
        }
        public Repository(string connection)
        {
            db = new SBPayrollDBEntities(connection);
        }
        #endregion "Constructor"
        #region "Public Methods"
        #region "Database and Connection"
        public bool Connect(
         string providerName,
         string serverName,
         string databaseName,
         string attacheddb,
         string userName,
         string password,
     string metaData,
          bool IntegratedSecurity)
        {
            try
            {
                EntityConnection conn = new EntityConnection(GetConnectionString(
                    providerName,
                    serverName,
                    databaseName,
                    attacheddb,
                    userName,
                    password,
                    metaData,
                    IntegratedSecurity));

                //overwrite the default context with this one
                db = new SBPayrollDBEntities(conn);

                return true;
            }
            catch (Exception ex)
            {
                ///TODO Log error
                Log.WriteToErrorLogFile(ex);
                db = null;
                return false;
            }
        }
        public bool Connect(string connectiostr)
        {
            try
            {
                //overwrite the default context with this one
                //string encConnection = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

                db = new SBPayrollDBEntities(connectiostr);

                return true;
            }
            catch (Exception ex)
            {
                ///TODO Log error
                Log.WriteToErrorLogFile(ex);
                db = null;
                return false;
            }
        }
        public string GetConnectionString(
           string providerName,
           string serverName,
           string databaseName,
           string attacheddb,
           string userName,
           string password,
            string metaData,
            bool IntegratedSecurity)
        {
            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder =
                new SqlConnectionStringBuilder();

            // Set the properties for the data source.
            sqlBuilder.DataSource = serverName;
            sqlBuilder.InitialCatalog = databaseName;
            if (!string.IsNullOrEmpty(attacheddb)) sqlBuilder.AttachDBFilename = attacheddb;
            sqlBuilder.IntegratedSecurity = IntegratedSecurity;
            sqlBuilder.UserID = userName;
            sqlBuilder.Password = password;


            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();

            // Initialize the EntityConnectionStringBuilder.
            EntityConnectionStringBuilder entityBuilder =
                new EntityConnectionStringBuilder();

            //Set the provider name.
            entityBuilder.Provider = providerName;

            // Set the provider-specific connection string.
            entityBuilder.ProviderConnectionString = providerString;

            // Set the Metadata location.
            //entityBuilder.Metadata = string.Format(@"metadata=res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl",
            //    metaData);
            entityBuilder.Metadata = string.Format(@"res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl",
                metaData);
            return entityBuilder.ToString();
        }
        public void GetUsers(Action<IList<UserModel>> callback)
        {
            callback(GetUserList());
        }
        public IList<UserModel> GetUserList()
        {

            var usermodels = from usr in db.spUsers
                             select new UserModel
                             {
                                 UserName = usr.UserName,
                                 Password = usr.Password,
                                 RoleId = usr.RoleId,
                                 Locked = usr.Locked
                             };

            return usermodels.ToList();
        }
        public UserModel GetUser(string _user)
        {

            var usermodel = (from usr in db.spUsers
                             where usr.UserName == _user
                             select new UserModel
                             {
                                 UserName = usr.UserName,
                                 Password = usr.Password,
                                 RoleId = usr.RoleId,
                                 Locked = usr.Locked
                             }).FirstOrDefault();

            return usermodel;
        }
        public void GetUserRoles()
        {

        }
        public void Save()
        {
            try
            {

                db.SaveChanges();
            }
            catch (Exception ex)
            {

                Log.WriteToErrorLogFile(ex);
            }
        }
        #endregion "Database and Connection"
        #region "Login"
        public bool Register(string username, string Pwd, int roleid)
        {
            try
            {
                spUser user = new spUser();

                user.UserName = username;
                user.Password = Pwd;
                user.RoleId = roleid;
                db.AddTospUsers(user);

                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }

        }
        public bool CheckUserExists(string username, string Pwd)
        {
            try
            {
                var us = from users in db.spUsers
                         where users.UserName == username
                         where users.Password == Pwd
                         select users;
                return (us.Count() > 0);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }
        public bool IsUserLocked(string username)
        {
            try
            {
                var us = from users in db.spUsers
                         where users.UserName == username
                         where users.Locked == true
                         select users;
                return (us.Count() > 0);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }
        public void LockUser(string username)
        {
            spUser user;
            try
            {
                var us = from users in db.spUsers
                         where users.UserName == username
                         select users;
                user = us.Single();
                user.Locked = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public spUser GetUserbyUserName(string username)
        {
            try
            {
                var us = from users in db.spUsers
                         where users.UserName == username
                         select users;
                return us.Single();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public bool ChangePassword(string username, string Pwd)
        {
            try
            {
                var us = (from users in db.spUsers
                          where users.UserName == username
                          select users).Single();
                us.Password = Pwd;
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }
        #endregion "Login"
        #region "Users"
        public List<spUser> GetUsers()
        {
            try
            {

                return db.spUsers.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }

        }
        public void AddUser(string username, string password, int roleid, bool locked)
        {
            try
            {

                spUser user = new spUser();
                user.UserName = username;
                user.Password = password;
                user.RoleId = roleid;
                user.Locked = locked;

                db.AddTospUsers(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }
        public void AddUser(spUser user)
        {
            try
            {
                spUser _user = new spUser();
                _user.UserName = user.UserName;
                _user.Password = user.Password;
                _user.RoleId = user.RoleId;
                _user.Surname = user.Surname;
                _user.OtherNames = user.OtherNames;
                _user.NationalID = user.NationalID;
                _user.DateOfBirth = user.DateOfBirth;
                _user.Gender = user.Gender;
                _user.InformBy = user.InformBy;
                _user.Email = user.Email;
                _user.Telephone = user.Telephone;
                _user.Photo = user.Photo;
                _user.Locked = user.Locked;
                _user.IsDeleted = user.IsDeleted;
                _user.SystemId = user.SystemId;
                _user.Status = user.Status;
                _user.DateJoined = user.DateJoined;

                db.AddTospUsers(user);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateUser(DAL.spUser user)
        {
            try
            {
                spUser _user = db.spUsers.First(u => u.Id == user.Id);
                _user.UserName = user.UserName;
                _user.Password = user.Password;
                _user.RoleId = user.RoleId;
                _user.Surname = user.Surname;
                _user.OtherNames = user.OtherNames;
                _user.NationalID = user.NationalID;
                _user.DateOfBirth = user.DateOfBirth;
                _user.Gender = user.Gender;
                _user.InformBy = user.InformBy;
                _user.Email = user.Email;
                _user.Telephone = user.Telephone;
                _user.Photo = user.Photo;
                _user.Locked = user.Locked;
                _user.IsDeleted = user.IsDeleted;
                _user.SystemId = user.SystemId;
                _user.Status = user.Status;
                _user.DateJoined = user.DateJoined;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);

            }
        }
        public void UpdateUser(DAL.spUser user, string password, int roleid, bool locked)
        {
            try
            {

                spUser _user = db.spUsers.First(u => u.Id == user.Id);

                _user.Password = password;
                _user.RoleId = roleid;
                _user.Locked = locked;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);

            }
        }
        public bool ChangePassword(UserModel user)
        {
            try
            {
                var _user = (from us in db.spUsers
                             where us.Id == user.UserId
                             select us).Single();
                _user.Password = user.Password;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);

                return true;
            }

            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }
        public bool Authenticate(string userId, string pwd, int Maxtries, int tries, ref string message, ref string errCode)
        {
            if (tries > Maxtries)
            {
                message = "Maximum tries exceeded; User locked ";
                errCode = "100";
                LockUser(userId);
                return false;
            }
            if (!CheckUserExists(userId, pwd))
            {
                message = "Username or password not correct";
                errCode = "101";
                return false;
            }
            if (IsUserLocked(userId))
            {
                message = "User locked, please contact the administrator";
                errCode = "102";
                return false;
            }
            ///TODO continue checking all authentication conditions

            return true;
        }
        #endregion "Users"
        #region "Roles"
        public void AddRole(spRole role)
        {
            try
            {

                spRole c = new spRole();
                c = role;

                db.spRoles.AddObject(c);
                db.SaveChanges();


            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateRole(spRole role)
        {
            try
            {
                spRole c = db.spRoles.First(r => r.Id == role.Id);
                c.Description = role.Description;
                c.ShortCode = role.ShortCode;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteRole(spRole role)
        {
            try
            {
                spRole c = db.spRoles.Where(r => r.Id == role.Id).Single();
                db.spRoles.DeleteObject(c);
                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }

            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }
        public List<spRole> GetRolesList()
        {
            try
            {
                return db.spRoles.ToList();

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Roles"
        #region "Rights"
        public void UpdateRight(spAllowedRoleMenu right)
        {
            try
            {
                spAllowedRoleMenu _right = db.spAllowedRoleMenus.First(r => r.Id == right.Id);
                _right.RoleId = right.RoleId;
                _right.MenuItemId = right.MenuItemId;
                _right.Allowed = right.Allowed;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateReportsRight(spAllowedReportsRolesMenu right)
        {
            try
            {
                spAllowedReportsRolesMenu _right = db.spAllowedReportsRolesMenus.First(r => r.Id == right.Id);
                _right.RoleId = right.RoleId;
                _right.MenuItemId = right.MenuItemId;
                _right.Allowed = right.Allowed;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        #endregion "Rights"
        #region "NHIFRates"
        public List<NHIFRate> NHIFTable()
        {
            try
            {

                var rt = from n in db.NHIFRates
                         select n;

                return rt.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public decimal NHIFLookUp(decimal Amount)
        {
            try
            {

                var rt = (from r in db.NHIFRates
                          orderby r.FromAmt ascending
                          where r.FromAmt <= Amount
                          where r.ToAmt >= Amount
                          select r).SingleOrDefault();

                decimal nhif = 0.00M;
                if (rt == null) { return nhif; }

                return rt.Rate;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal NSSFLookUp()
        {
            try
            {
                string nssfstr = SettingLookup("NSSFMAX");
                return string.IsNullOrEmpty(nssfstr) ? 0.0M : decimal.Parse(nssfstr);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public void AddNHIFRate(decimal fromamount, decimal toamount, decimal rate)
        {
            try
            {

                NHIFRate nhifrate = new NHIFRate();
                nhifrate.FromAmt = rate;
                nhifrate.ToAmt = toamount;
                nhifrate.Rate = rate;

                db.AddToNHIFRates(nhifrate);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }
        #endregion "NHIFRates"
        #region "NSSF"
        public NssfContributionsDTO ComputeNssfContributions(int EmployeeId)
        {
            try
            {
                Employee _Employee = new Employee();
                var _employeequery = this.GetEmployee(EmployeeId);
                if (_employeequery != null)
                {
                    _Employee = _employeequery;
                }
                decimal Amount = _Employee.BasicPay ?? 0;
                NssfContributionsDTO _NssfContributionsDTO = new NssfContributionsDTO();
                decimal lowerearninglimit = int.Parse(this.SettingLookup("NSSFMINLOWEREARNINGLIMIT"));
                decimal upperearninglimit = int.Parse(this.SettingLookup("NSSFMAXUPPEREARNINGLIMIT"));
                int _employeecontributionpecentage = int.Parse(this.SettingLookup("NSSFEMPLOYEECONTRIBUTIONPERCENTAGE"));
                int _employercontributionpercentage = int.Parse(this.SettingLookup("NSSFEMPLOYERCONTRIBUTIONPERCENTAGE"));

                if (Amount <= lowerearninglimit)
                {
                    _NssfContributionsDTO = new NssfContributionsDTO();
                    _NssfContributionsDTO.EmpNo = _Employee.EmpNo;
                    _NssfContributionsDTO.EmployeeId = _Employee.Id;
                    _NssfContributionsDTO.EmployeeEarnings = Amount;
                    _NssfContributionsDTO.PensionableEarnings = _NssfContributionsDTO.EmployeeEarnings;
                    //tier1
                    _NssfContributionsDTO.Tier1PensionableEarnings = _NssfContributionsDTO.PensionableEarnings;
                    _NssfContributionsDTO.Tier1EmployeeDeduction = (_NssfContributionsDTO.Tier1PensionableEarnings * _employeecontributionpecentage) / 100;
                    _NssfContributionsDTO.Tier1EmployerContribution = _NssfContributionsDTO.Tier1EmployeeDeduction;
                    _NssfContributionsDTO.Tier1TotalContribution = _NssfContributionsDTO.Tier1EmployeeDeduction + _NssfContributionsDTO.Tier1EmployerContribution;
                    //tier2
                    _NssfContributionsDTO.Tier2PensionableEarnings = _NssfContributionsDTO.PensionableEarnings - _NssfContributionsDTO.Tier1PensionableEarnings;
                    _NssfContributionsDTO.Tier2EmployeeDeduction = (_NssfContributionsDTO.Tier2PensionableEarnings * _employeecontributionpecentage) / 100;
                    _NssfContributionsDTO.Tier2EmployerContribution = _NssfContributionsDTO.Tier2EmployeeDeduction;
                    _NssfContributionsDTO.Tier2TotalContribution = _NssfContributionsDTO.Tier2EmployeeDeduction + _NssfContributionsDTO.Tier2EmployerContribution;
                    //total
                    _NssfContributionsDTO.TotalPensionContribution = _NssfContributionsDTO.Tier1TotalContribution + _NssfContributionsDTO.Tier2TotalContribution;
                }

                if (Amount > lowerearninglimit && Amount < upperearninglimit)
                {
                    _NssfContributionsDTO = new NssfContributionsDTO();
                    _NssfContributionsDTO.EmpNo = _Employee.EmpNo;
                    _NssfContributionsDTO.EmployeeId = _Employee.Id;
                    _NssfContributionsDTO.EmployeeEarnings = Amount;
                    _NssfContributionsDTO.PensionableEarnings = _NssfContributionsDTO.EmployeeEarnings;
                    //tier1
                    _NssfContributionsDTO.Tier1PensionableEarnings = lowerearninglimit;
                    _NssfContributionsDTO.Tier1EmployeeDeduction = (_NssfContributionsDTO.Tier1PensionableEarnings * _employeecontributionpecentage) / 100;
                    _NssfContributionsDTO.Tier1EmployerContribution = _NssfContributionsDTO.Tier1EmployeeDeduction;
                    _NssfContributionsDTO.Tier1TotalContribution = _NssfContributionsDTO.Tier1EmployeeDeduction + _NssfContributionsDTO.Tier1EmployerContribution;
                    //tier2
                    _NssfContributionsDTO.Tier2PensionableEarnings = _NssfContributionsDTO.PensionableEarnings - _NssfContributionsDTO.Tier1PensionableEarnings;
                    _NssfContributionsDTO.Tier2EmployeeDeduction = (_NssfContributionsDTO.Tier2PensionableEarnings * _employeecontributionpecentage) / 100;
                    _NssfContributionsDTO.Tier2EmployerContribution = _NssfContributionsDTO.Tier2EmployeeDeduction;
                    _NssfContributionsDTO.Tier2TotalContribution = _NssfContributionsDTO.Tier2EmployeeDeduction + _NssfContributionsDTO.Tier2EmployerContribution;
                    //total
                    _NssfContributionsDTO.TotalPensionContribution = _NssfContributionsDTO.Tier1TotalContribution + _NssfContributionsDTO.Tier2TotalContribution;
                }

                if (Amount >= upperearninglimit)
                {
                    _NssfContributionsDTO = new NssfContributionsDTO();
                    _NssfContributionsDTO.EmpNo = _Employee.EmpNo;
                    _NssfContributionsDTO.EmployeeId = _Employee.Id;
                    _NssfContributionsDTO.EmployeeEarnings = Amount;
                    _NssfContributionsDTO.PensionableEarnings = upperearninglimit;
                    //tier1
                    _NssfContributionsDTO.Tier1PensionableEarnings = lowerearninglimit;
                    _NssfContributionsDTO.Tier1EmployeeDeduction = (_NssfContributionsDTO.Tier1PensionableEarnings * _employeecontributionpecentage) / 100;
                    _NssfContributionsDTO.Tier1EmployerContribution = _NssfContributionsDTO.Tier1EmployeeDeduction;
                    _NssfContributionsDTO.Tier1TotalContribution = _NssfContributionsDTO.Tier1EmployeeDeduction + _NssfContributionsDTO.Tier1EmployerContribution;
                    //tier2
                    _NssfContributionsDTO.Tier2PensionableEarnings = _NssfContributionsDTO.PensionableEarnings - _NssfContributionsDTO.Tier1PensionableEarnings;
                    _NssfContributionsDTO.Tier2EmployeeDeduction = (_NssfContributionsDTO.Tier2PensionableEarnings * _employeecontributionpecentage) / 100;
                    _NssfContributionsDTO.Tier2EmployerContribution = _NssfContributionsDTO.Tier2EmployeeDeduction;
                    _NssfContributionsDTO.Tier2TotalContribution = _NssfContributionsDTO.Tier2EmployeeDeduction + _NssfContributionsDTO.Tier2EmployerContribution;
                    //total
                    _NssfContributionsDTO.TotalPensionContribution = _NssfContributionsDTO.Tier1TotalContribution + _NssfContributionsDTO.Tier2TotalContribution;
                }

                return _NssfContributionsDTO;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "NSSF"
        #region "Benefits"
        public List<Benefit> GetBenefits()
        {
            try
            {
                return db.Benefits.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Benefit> GetNonDeletedBenefits()
        {
            try
            {
                return db.Benefits.Where(i => i.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void AddBenefit(string name, decimal rate)
        {
            try
            {
                Benefit benefit = new Benefit();
                benefit.Description = name;
                benefit.Rate = rate;

                if (!db.Benefits.Any(i => i.Description == benefit.Description))
                {
                    db.Benefits.AddObject(benefit);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateBenefit(DAL.Benefit benefit, string name, decimal rate)
        {
            try
            {
                Benefit _benefit = db.Benefits.First(e => e.Id == benefit.Id);
                _benefit.Description = name;
                _benefit.Rate = rate;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateBenefit(DAL.Benefit benefit)
        {
            try
            {
                Benefit _benefit = db.Benefits.First(e => e.Id == benefit.Id);
                _benefit.Description = benefit.Description;
                _benefit.Rate = benefit.Rate;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteBenefit(int benefitid)
        {
            try
            {
                Benefit emp = db.Benefits.Where(e => e.Id == benefitid).SingleOrDefault();
                emp.IsDeleted = true;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteBenefit(Benefit benefit)
        {
            try
            {
                Benefit emp = db.Benefits.Where(e => e.Id == benefit.Id).SingleOrDefault();
                emp.IsDeleted = true;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        #endregion  "Benefits"
        #region "emp Benefits"
        public void AddEmpBenefit(EmpNonCashBenefit b)
        {
            try
            {
                db.AddToEmpNonCashBenefits(b);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void EditEmpBenefit(EmpNonCashBenefit b)
        {
            try
            {
                EmpNonCashBenefit empb = db.EmpNonCashBenefits.Where(eb => eb.EmpNo == b.EmpNo).Single();
                empb = b;
                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteEmpBenefit(EmpNonCashBenefit b)
        {
            try
            {

                EmpNonCashBenefit benefit = db.EmpNonCashBenefits.Where(eb => eb.BenefitId == b.BenefitId && eb.EmpNo == b.EmpNo).Single();
                db.EmpNonCashBenefits.DeleteObject(benefit);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteEmpNonCashBenefit(EmpNonCashBenefit b)
        {
            try
            {

                EmpNonCashBenefit benefit = db.EmpNonCashBenefits.Where(eb => eb.EmpNo == b.EmpNo).Single();
                db.EmpNonCashBenefits.DeleteObject(benefit);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public IQueryable<DAL.EmpNonCashBenefit> GetEmpBenefits(string empno)
        {
            try
            {

                var ret = (from b in db.EmpNonCashBenefits
                           where b.EmpNo == empno
                           select b);
                return ret;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<DAL.EditEmpNonCashBenefit> GetEditEmpBenefits(string empno)
        {
            try
            {

                var ret = (from b in db.EmpNonCashBenefits
                           where b.EmpNo == empno
                           select new EditEmpNonCashBenefit
                           {
                               Benefit = b,
                               Action = Action.None
                           }).ToList();
                return ret;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Benefit> NonCashBenefitsList(string EmpNo)
        {
            try
            {

                List<int> EmpPayItems = (from i in db.EmpNonCashBenefits
                                         where i.EmpNo == EmpNo
                                         select i.BenefitId).ToList();

                List<Benefit> allPI = (from i in db.Benefits
                                       where !EmpPayItems.Contains(i.Id)
                                       select i).ToList();
                return allPI;

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<NonCashBenefits> GetNonCashBenefitsList(int EmployeeId)
        {
            try
            {
                var noncashbenefits = from nc in db.EmpNonCashBenefits
                                      join bf in db.Benefits on nc.BenefitId equals bf.Id
                                      where nc.EmployeeId == EmployeeId
                                      select new NonCashBenefits
                                      {
                                          BenefitId = nc.BenefitId,
                                          Description = bf.Description,
                                          Quantity = nc.Quantity,
                                          EmpNo = nc.EmpNo,
                                          Rate = bf.Rate,
                                          EmployeeId = EmployeeId
                                      };
                return noncashbenefits.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void UpdateEmpBenefits(List<DAL.EditEmpNonCashBenefit> benefits)
        {
            try
            {
                foreach (var benefit in benefits)
                {
                    switch (benefit.Action)
                    {
                        case Action.None: break;
                        case Action.Add:
                            this.AddEmpBenefit(benefit.Benefit);
                            break;
                        case Action.Edit:
                            this.EditEmpBenefit(benefit.Benefit);
                            break;
                        case Action.Delete:
                            this.DeleteEmpBenefit(benefit.Benefit);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        #endregion "emp Benefits"
        #region "Setting"
        public string SettingLookup(string Key)
        {
            try
            {
                var setting = db.Settings.FirstOrDefault(s => s.SSKey == Key);
                if (setting != null)
                    return setting.SSValue;
                else
                    return null;

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Setting> GetSettings()
        {
            try
            {

                return db.Settings.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void EditSetting(DAL.Setting setting, string ssvalue)
        {
            try
            {


                Setting _setting = db.Settings.First(s => s.SSKey == setting.SSKey);

                _setting.SSValue = ssvalue;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }
        public List<SettingsGroup> GetSettingsGroup()
        {
            try
            {

                return db.SettingsGroups.Include("Settings").ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Setting"
        #region "Employee_Ext"
        public List<Employee> GetEmployeeNumbers()
        {
            try
            {

                return this.GetAllActiveEmployees().ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void AddEmployeeCustomInfo(Employee_Ext employee_ext)
        {
            try
            {

                Employee_Ext _employee_ext = new Employee_Ext();
                _employee_ext.EmpNo = employee_ext.EmpNo;
                _employee_ext.ExFieldName = employee_ext.ExFieldName;
                _employee_ext.ExFieldStr = employee_ext.ExFieldStr;
                _employee_ext.ExFieldInt = employee_ext.ExFieldInt;
                _employee_ext.ExFieldDate = employee_ext.ExFieldDate;
                _employee_ext.ExFieldDecimal = employee_ext.ExFieldDecimal;


                db.AddToEmployee_Ext(_employee_ext);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<Employee_Ext> GetAllEmployeeCustomInfo(string empno)
        {
            try
            {

                return db.Employee_Ext.Where(e => e.EmpNo == empno).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Employee_Ext"
        #region "Employer"
        public int AddEmployer(Employer _employer)
        {
            int id = -1;
            try
            {
                Employer employer = new Employer();
                employer.Name = _employer.Name;
                employer.Address1 = _employer.Address1;
                employer.Address2 = _employer.Address2;
                employer.Telephone = _employer.Telephone;
                employer.PIN = _employer.PIN;
                employer.Email = _employer.Email;
                employer.Logo = _employer.Logo;
                employer.Slogan = _employer.Slogan;
                employer.NHIF = _employer.NHIF;
                employer.NSSF = _employer.NSSF;
                employer.BankBranchSortCode = _employer.BankBranchSortCode;
                employer.AccountName = _employer.AccountName;
                employer.AccountNo = _employer.AccountNo;
                employer.AuthorizedSignatory = _employer.AuthorizedSignatory;
                employer.IsActive = _employer.IsActive;
                employer.IsDeleted = _employer.IsDeleted;

                db.Employers.AddObject(employer);
                db.SaveChanges();

                id = employer.Id;

                return id;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return id;
            }
        }
        public void UpdateEmployer(Employer _employer)
        {
            try
            {
                Employer employer = db.Employers.Where(p => p.Id == _employer.Id).Single();
                employer.Name = _employer.Name;
                employer.Address1 = _employer.Address1;
                employer.Address2 = _employer.Address2;
                employer.Telephone = _employer.Telephone;
                employer.PIN = _employer.PIN;
                employer.Email = _employer.Email;
                employer.Logo = _employer.Logo;
                employer.Slogan = _employer.Slogan;
                employer.NHIF = _employer.NHIF;
                employer.NSSF = _employer.NSSF;
                employer.BankBranchSortCode = _employer.BankBranchSortCode;
                employer.AccountName = _employer.AccountName;
                employer.AccountNo = _employer.AccountNo;
                employer.AuthorizedSignatory = _employer.AuthorizedSignatory;
                employer.IsActive = _employer.IsActive;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteEmployer(Employer _employer)
        {
            try
            {
                Employer employer = db.Employers.Where(p => p.Id == _employer.Id).Single();
                employer.IsDeleted = true;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public Employer GetEmployer()
        {
            try
            {
                return db.Employers.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public Employer GetEmployer(int id)
        {
            try
            {
                return db.Employers.Where(e => e.Id == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string GetEmployerName(int id)
        {
            try
            {
                var _employerquery = (from em in db.Employers
                                      where em.Id == id
                                      select em.Name).FirstOrDefault();
                string _employername = _employerquery;
                return _employername;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public Employer GetEmployeeEmployer(int employerid)
        {
            try
            {
                var _employerquery = (from ep in db.Employers
                                      where ep.Id == employerid
                                      select ep).FirstOrDefault();
                Employer _employer = _employerquery;
                return _employer;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employer> GetAllEmployers()
        {
            try
            {
                var _empquery = from emp in db.Employers
                                where emp.IsDeleted == false
                                select emp;
                List<Employer> _employers = _empquery.ToList();
                return _employers;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employer> GetAllActiveEmployers()
        {
            try
            {
                var _empquery = from emp in db.Employers
                                where emp.IsActive == true
                                where emp.IsDeleted == false
                                select emp;
                List<Employer> _employers = _empquery.ToList();
                return _employers;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Employer"
        #region "Employee"
        public int TotalEmployees()
        {
            try
            {
                return this.GetAllActiveEmployees().Where(e => e.IsActive == true && e.IsDeleted == false).Count();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public List<Employee> GetAllEmployees()
        {
            try
            {
                return this.GetAllActiveEmployees().Where(e => e.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employee> GetAllActiveEmployeesforEmployer(int _employerid)
        {
            try
            {
                var _employeesquery = from em in this.GetAllActiveEmployees()
                                      where em.EmployerId == _employerid
                                      where em.IsActive == true
                                      where em.IsDeleted == false
                                      select em;
                return _employeesquery.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employee> GetAllActiveEmployees()
        {
            try
            {
                return db.Employees.Where(e => e.IsActive == true && e.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employee> GetEmployeesWithDepartment(Department _dept)
        {
            try
            {
                var emps = from e in this.GetAllActiveEmployees()
                           where e.DepartmentId == _dept.Id
                           where e.IsDeleted == false
                           select e;
                return emps.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employee> GetEmployeesWithBenefit(Benefit _benft)
        {
            try
            {
                var emps = from e in this.GetAllActiveEmployees()
                           join eb in db.EmpNonCashBenefits on e.EmpNo equals eb.EmpNo
                           join b in db.Benefits on eb.BenefitId equals b.Id
                           where b.Id == _benft.Id
                           where e.IsDeleted == false
                           select e;
                return emps.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employee> GetEmployeesWithPayrollItemTransactions(PayrollItem _payrollitem)
        {
            try
            {
                var emps = from e in this.GetAllActiveEmployees()
                           join et in db.EmployeeTransactions on e.EmpNo equals et.EmpNo
                           join pi in db.PayrollItems on et.ItemId equals pi.Id
                           where pi.Id == _payrollitem.Id
                           where e.IsDeleted == false
                           select e;
                return emps.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employee> GetEmployeesWithPayslipDet()
        {
            try
            {
                var _employees = from e in this.GetAllActiveEmployees()
                                 join et in db.PayslipDets on e.EmpNo equals et.EmpNo
                                 where e.IsDeleted == false
                                 select e;
                return _employees.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employee> GetEmployeesWithPayslipDet_Temp()
        {
            try
            {
                var _employees = from e in this.GetAllActiveEmployees()
                                 join et in db.PayslipDet_Temp on e.EmpNo equals et.EmpNo
                                 where e.IsDeleted == false
                                 select e;
                return _employees.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employee> GetEmployeesWithPayslipMaster()
        {
            try
            {
                var _employees = from e in this.GetAllActiveEmployees()
                                 join et in db.PayslipMasters on e.EmpNo equals et.EmpNo
                                 where e.IsDeleted == false
                                 select e;
                return _employees.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Employee> GetEmployeesWithPayslipMaster_Temp()
        {
            try
            {
                var _employees = from e in this.GetAllActiveEmployees()
                                 join et in db.PayslipMaster_Temp on e.EmpNo equals et.EmpNo
                                 where e.IsDeleted == false
                                 select e;
                return _employees.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string LastEmployeeNo()
        {
            try
            {
                var emp = this.GetAllActiveEmployees().OrderByDescending(r => r.EmpNo).FirstOrDefault();

                if (emp == null)
                    return "E";
                return emp.EmpNo.Trim();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void DeleteEmployee(string EmpNo)
        {
            try
            {
                Employee emp = this.GetAllActiveEmployees().Where(e => e.EmpNo == EmpNo).SingleOrDefault();
                //emp.IsActive = false;
                //emp.IsDeleted = true;

                db.Employees.DeleteObject(emp);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public Employee GetEmployee(int EmployeeId)
        {
            try
            {
                return this.GetAllActiveEmployees().Where(e => e.Id == EmployeeId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public bool EmployeeExist(string EmpNo)
        {
            try
            {
                Employee emp = this.GetAllActiveEmployees().Where(e => e.EmpNo == EmpNo).SingleOrDefault();
                return (emp != null);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }
        public List<string> GetEmployeIdsFromCriteria(List<CriterionItem> Criteria)
        {
            try
            {
                var emp = from e in GetEmployeesFromCriteria(Criteria)
                          select e.EmpNo.Trim();
                return emp.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public decimal StatutoryDeductions(string EmpNo)
        {
            try
            {
                //All statutory deductions(taxable and non-taxable) other than PAYE

                var TotalDeductions = (from d in db.GetEmpTransactions
                                       where d.ItemTypeId.Trim() == "STATUTORY"
                                       where d.Enabled == true
                                       select d.Amount);
                if (TotalDeductions.Count() == 0)
                    return 0.0M;

                return TotalDeductions.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal AllowableDeductions(int EmployeeId)
        {
            try
            {
                //All deductible

                var TotalDeductions = (from d in db.GetEmpTransactions
                                       where d.EmployeeId == EmployeeId
                                       where d.TaxTrackingId.Trim() == "DEDUCTIBLE"
                                       where d.Enabled == true
                                       select d.Amount);
                if (TotalDeductions.Count() == 0) return 0.0M;
                return TotalDeductions.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal OtherDeductions(string EmpNo)
        {
            try
            {

                var TotalDeductions = (from d in db.GetEmpTransactions
                                       where d.EmpNo == EmpNo
                                       where d.ItemTypeId.Trim() == "DEDUCTION"
                                       select d.Amount);
                if (TotalDeductions.Count() == 0) return 0.0M;
                return TotalDeductions.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal MortgageRelief(int EmployeeId)
        {
            try
            {
                Employee emp = this.GetAllActiveEmployees().Where(e => e.Id == EmployeeId).SingleOrDefault();
                if (emp == null) return 0;

                return emp.MortgageRelief.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal InsuranceRelief(int EmployeeId)
        {
            try
            {
                decimal _InsuranceRelief = 0;

                Employee emp = this.GetAllActiveEmployees().Where(e => e.Id == EmployeeId).SingleOrDefault();
                if (emp == null) return 0;

                decimal empPremium = emp.InsuranceRelief ?? 0;
                _InsuranceRelief = empPremium * 15 / 100;

                return _InsuranceRelief;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal PersonalRelief(int EmployeeId)
        {
            try
            {
                Employee emp = this.GetAllActiveEmployees().Where(e => e.Id == EmployeeId).SingleOrDefault();
                if (emp == null) return 0;

                return emp.PersonalRelief.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal Allowances(int EmployeeId)
        {
            try
            {
                /*
                 * All taxable earnings excluding basic
                 */

                var benefits = (from d in db.GetEmpTransactions
                                where d.EmployeeId == EmployeeId
                                where d.ItemTypeId.Trim() == "ADDITION"
                                where d.TaxTrackingId.Trim() == "EARNING"
                                where d.Enabled == true
                                select d.Amount);

                if (benefits.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return benefits.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal NonTaxableEarnings(int EmployeeId)
        {
            try
            {
                /*
                 * All taxable earnings excluding basic
                 */

                var benefits = (from d in db.GetEmpTransactions
                                where d.EmployeeId == EmployeeId
                                where d.ItemTypeId.Trim() == "ADDITION"
                                where d.TaxTrackingId.Trim() == "NONE"
                                where d.Enabled == true
                                select d.Amount);

                if (benefits.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return benefits.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal NonCashBenefits(int EmployeeId)
        {
            try
            {
                var itm = (from e in db.GetEmpTransactions
                           where e.EmployeeId == EmployeeId
                           where e.ItemId.Trim() == "NON_CASH_BENEFIT"
                           select e.Amount);

                if (itm.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return itm.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public List<Employee> GetEmployeesFromCriteria(List<CriterionItem> Criteria)
        {
            try
            {
                // var emplist = this.GetAllActiveEmployees();
                IQueryable<Employee> query = this.GetAllActiveEmployees().AsQueryable();

                foreach (CriterionItem ci in Criteria)
                {
                    switch (ci.Criterion.FieldName.ToLower())
                    {
                        case "empno":
                            switch (ci.Criterion.Opr.Name)
                            {
                                case "equal": query = query.Where(e => e.EmpNo == ci.Criterion.FValue);
                                    break;
                                case "notequal": query = query.Where(e => e.EmpNo != ci.Criterion.FValue);
                                    break;
                                case "startswith": query = query.Where(e => e.EmpNo.StartsWith(ci.Criterion.FValue));
                                    break;
                                case "endswith": query = query.Where(e => e.EmpNo.EndsWith(ci.Criterion.FValue));
                                    break;
                                case "has": query = query.Where(e => e.EmpNo.Contains(ci.Criterion.FValue));
                                    break;
                            }
                            break;
                        case "surname":
                            switch (ci.Criterion.Opr.Name)
                            {
                                case "equal": query = query.Where(e => e.Surname == ci.Criterion.FValue);
                                    break;
                                case "notequal": query = query.Where(e => e.Surname != ci.Criterion.FValue);
                                    break;
                                case "startswith": query = query.Where(e => e.Surname.StartsWith
                                    (ci.Criterion.FValue));
                                    break;
                                case "endswith": query = query.Where(e => e.Surname.EndsWith(ci.Criterion.FValue));
                                    break;
                                case "has": query = query.Where(e => e.Surname.Contains(ci.Criterion.FValue));
                                    break;
                            }
                            break;
                        case "dob":
                            DateTime dob = DateTime.Parse(ci.Criterion.FValue);
                            switch (ci.Criterion.Opr.Name)
                            {

                                case "equal": query = query.Where(e => e.DoB == dob);
                                    break;
                                case "notequal": query = query.Where(e => e.DoB != dob);
                                    break;
                                case "greatorthan": query = query.Where(e => e.DoB > dob);
                                    break;
                                case "lessthan": query = query.Where(e => e.DoB < dob);
                                    break;
                                case "greatorthanorequal": query = query.Where(e => e.DoB >= dob);
                                    break;
                                case "lessthanorequal": query = query.Where(e => e.DoB <= dob);
                                    break;
                            }
                            break;
                        case "doe":
                            DateTime doe = DateTime.Parse(ci.Criterion.FValue);
                            switch (ci.Criterion.Opr.Name)
                            {

                                case "equal": query = query.Where(e => e.DoE == doe);
                                    break;
                                case "notequal": query = query.Where(e => e.DoE != doe);
                                    break;
                                case "greatorthan": query = query.Where(e => e.DoE > doe);
                                    break;
                                case "lessthan": query = query.Where(e => e.DoE < doe);
                                    break;
                                case "greatorthanorequal": query = query.Where(e => e.DoE >= doe);
                                    break;
                                case "lessthanorequal": query = query.Where(e => e.DoE <= doe);
                                    break;
                            }
                            break;

                        case "pinno":
                            switch (ci.Criterion.Opr.Name)
                            {
                                case "equal": query = query.Where(e => e.PINNo == ci.Criterion.FValue);
                                    break;
                                case "notequal": query = query.Where(e => e.PINNo != ci.Criterion.FValue);
                                    break;
                                case "startswith": query = query.Where(e => e.PINNo.StartsWith(ci.Criterion.FValue));
                                    break;
                                case "endswith": query = query.Where(e => e.PINNo.EndsWith(ci.Criterion.FValue));
                                    break;
                                case "has": query = query.Where(e => e.PINNo.Contains(ci.Criterion.FValue));
                                    break;
                            }
                            break;
                    }
                }

                List<Employee> empl = query.ToList();
                return empl;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void SaveEmployee(String Empno, string Surname, string Bank, string Account,
            int Dept,
            int Employer)
        {
            try
            {
                Employee emp = new Employee();
                emp.EmpNo = Empno;
                emp.Surname = Surname;
                emp.BankCode = Bank;
                emp.BankAccount = Account;
                emp.DepartmentId = Dept;
                emp.EmployerId = Employer;

                SaveEmployee(emp);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public Employee CreateEmployee(Employee _employee)
        {
            try
            {
                Employee employee = new Employee();
                employee.Email = _employee.Email;
                employee.EmpNo = _employee.EmpNo;
                employee.Surname = _employee.Surname;
                employee.OtherNames = _employee.OtherNames;
                employee.DoB = _employee.DoB;
                employee.MaritalStatus = _employee.MaritalStatus;
                employee.Gender = _employee.Gender;
                employee.Photo = _employee.Photo;
                employee.DoE = _employee.DoE;
                employee.BasicComputation = _employee.BasicComputation;
                employee.BasicPay = _employee.BasicPay;
                employee.PersonalRelief = _employee.PersonalRelief;
                employee.MortgageRelief = _employee.MortgageRelief;
                employee.InsuranceRelief = _employee.InsuranceRelief;
                employee.NSSFNo = _employee.NSSFNo;
                employee.NHIFNo = _employee.NHIFNo;
                employee.IDNo = _employee.IDNo;
                employee.PINNo = _employee.PINNo;
                employee.DepartmentId = _employee.DepartmentId;
                employee.EmployerId = _employee.EmployerId;
                employee.PayPoint = _employee.PayPoint;
                employee.EmpGroup = _employee.EmpGroup;
                employee.EmpPayroll = _employee.EmpPayroll;
                employee.PrevEmployer = _employee.PrevEmployer;
                employee.DateLeft = _employee.DateLeft;
                employee.PaymentMode = _employee.PaymentMode;
                employee.TelephoneNo = _employee.TelephoneNo;
                employee.ChequeNo = _employee.ChequeNo;
                employee.BankCode = _employee.BankCode;
                employee.BankAccount = _employee.BankAccount;
                employee.LeaveBalance = _employee.LeaveBalance;
                employee.IsActive = _employee.IsActive;
                employee.CreatedBy = _employee.CreatedBy;
                employee.CreatedOn = _employee.CreatedOn;
                employee.IsDeleted = _employee.IsDeleted;
                employee.SystemId = _employee.SystemId;

                db.Employees.AddObject(employee);
                db.SaveChanges();

                return employee;
            }
            catch (Exception e)
            {
                Log.WriteToErrorLogFile(e);
                return null;
            }
        }
        public bool SaveEmployee(Employee _employee)
        {
            try
            {

                Employee employee = new Employee();
                employee.Email = _employee.Email;
                employee.EmpNo = _employee.EmpNo;
                employee.Surname = _employee.Surname;
                employee.OtherNames = _employee.OtherNames;
                employee.DoB = _employee.DoB;
                employee.MaritalStatus = _employee.MaritalStatus;
                employee.Gender = _employee.Gender;
                employee.Photo = _employee.Photo;
                employee.DoE = _employee.DoE;
                employee.BasicComputation = _employee.BasicComputation;
                employee.BasicPay = _employee.BasicPay;
                employee.PersonalRelief = _employee.PersonalRelief;
                employee.MortgageRelief = _employee.MortgageRelief;
                employee.InsuranceRelief = _employee.InsuranceRelief;
                employee.NSSFNo = _employee.NSSFNo;
                employee.NHIFNo = _employee.NHIFNo;
                employee.IDNo = _employee.IDNo;
                employee.PINNo = _employee.PINNo;
                employee.DepartmentId = _employee.DepartmentId;
                employee.EmployerId = _employee.EmployerId;
                employee.PayPoint = _employee.PayPoint;
                employee.EmpGroup = _employee.EmpGroup;
                employee.EmpPayroll = _employee.EmpPayroll;
                employee.PrevEmployer = _employee.PrevEmployer;
                employee.DateLeft = _employee.DateLeft;
                employee.PaymentMode = _employee.PaymentMode;
                employee.TelephoneNo = _employee.TelephoneNo;
                employee.ChequeNo = _employee.ChequeNo;
                employee.BankCode = _employee.BankCode;
                employee.BankAccount = _employee.BankAccount;
                employee.LeaveBalance = _employee.LeaveBalance;
                employee.IsActive = _employee.IsActive;
                employee.CreatedBy = _employee.CreatedBy;
                employee.CreatedOn = _employee.CreatedOn;
                employee.IsDeleted = _employee.IsDeleted;
                employee.SystemId = _employee.SystemId;

                if (!this.GetAllActiveEmployees().Any(i => i.EmpNo == employee.EmpNo))
                {
                    db.Employees.AddObject(employee);
                    db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.WriteToErrorLogFile(e);
                return false;
            }
        }
        public void UpdateEmployee(Employee _employee)
        {
            try
            {
                Employee employee = this.GetAllActiveEmployees().First(e => e.Id == _employee.Id);

                employee.BasicComputation = _employee.BasicComputation;
                employee.BankCode = _employee.BankCode;
                employee.BankAccount = _employee.BankAccount;
                employee.BasicPay = _employee.BasicPay;
                employee.DateLeft = _employee.DateLeft;
                employee.DepartmentId = _employee.DepartmentId;
                employee.DoB = _employee.DoB;
                employee.DoE = _employee.DoE;
                employee.EmployerId = _employee.EmployerId;
                employee.EmpNo = _employee.EmpNo;
                employee.Email = _employee.Email;
                employee.Gender = _employee.Gender;
                employee.IDNo = _employee.IDNo;
                employee.IsActive = _employee.IsActive;
                employee.MaritalStatus = _employee.MaritalStatus;
                employee.MortgageRelief = _employee.MortgageRelief;
                employee.InsuranceRelief = _employee.InsuranceRelief;
                employee.NHIFNo = _employee.NHIFNo;
                employee.NSSFNo = _employee.NSSFNo;
                employee.OtherNames = _employee.OtherNames;
                employee.EmpGroup = _employee.EmpGroup;
                employee.EmpPayroll = _employee.EmpPayroll;
                employee.PayPoint = _employee.PayPoint;
                employee.PersonalRelief = _employee.PersonalRelief;
                employee.PINNo = _employee.PINNo;
                employee.PrevEmployer = _employee.PrevEmployer;
                employee.Surname = _employee.Surname;
                employee.Photo = _employee.Photo;
                employee.TelephoneNo = _employee.TelephoneNo;
                employee.PaymentMode = _employee.PaymentMode;
                employee.ChequeNo = _employee.ChequeNo;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public decimal TaxableEarnings(int EmployeeId)
        {
            try
            {
                var taxable = (from e in this.EmployeeEarnings(EmployeeId)
                               where e.TaxTracking.Trim() == "EARNING"
                               select e.Amount);
                if (taxable.Count() == 0) return 0.0M;
                return taxable.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal PensionableEarnings(int EmployeeId)
        {

            try
            {

                var pensionable = from emptxn in db.EmployeeTransactions
                                  join pi in db.PayrollItems on emptxn.ItemId equals pi.Id
                                  join pit in db.PayrollItemTypes on pi.ItemTypeId equals pit.Id
                                  join emp in db.Employees on emptxn.EmpNo equals emp.EmpNo
                                  join dp in db.Departments on emp.DepartmentId equals dp.Id
                                  //where emptxn.EmpNo == EmpNo
                                  where emptxn.EmployeeId == EmployeeId
                                  where pi.ItemTypeId.Trim() == "HOURLY_PAY"
                                  where pi.TaxTrackingId.Trim() == "EARNING"
                                  where pi.Enable == true
                                  select emptxn.Amount;

                //var pensionable = (from e in db.GetEmpTransactions
                //                   where e.EmpNo == EmpNo
                //                   where e.TaxTrackingId.Trim() == "EARNING"
                //                   where e.AddToPension == true
                //                   where e.Enabled == true
                //                   select e.Amount);

                if (pensionable.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return pensionable.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }

        }
        public List<EarningsDeductions> EmployeeEarnings(int EmployeeId)
        {

            try
            {
                //This included taxable and non-taxable earnings

                List<string> earningTypes = new List<string>() { "ADDITION", "SALARY" };
                var Earnings = (from d in db.GetEmpTransactions
                                where d.EmployeeId == EmployeeId
                                where earningTypes.Contains(d.ItemTypeId.Trim())
                                where d.Enabled == true
                                select new EarningsDeductions
                                {
                                    DEType = "E",
                                    ShowInPayslip = d.ShowYTDInPayslip ?? false,
                                    TrackYTD = d.EmpTrack ?? false,
                                    YTD = (d.EmpTrack ?? false) ? (d.Amount + d.Balance ?? 0) : 0,
                                    Description = d.ItemId,
                                    EmpTxnId = d.EmpTxnId,
                                    TaxTracking = d.TaxTrackingId,
                                    Amount = d.Amount,
                                    ItemType = d.ItemTypeId
                                });
                return Earnings.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EarningsDeductions> GetAllEarnings()
        {

            try
            {
                //This included taxable and non-taxable earnings

                List<string> earningTypes = new List<string>() { "ADDITION", "SALARY", "HOURLY_PAY" };
                var Earnings = (from d in db.GetEmpTransactions
                                where earningTypes.Contains(d.ItemTypeId.Trim())
                                where d.Enabled == true
                                select new EarningsDeductions
                                {
                                    DEType = "E",
                                    ShowInPayslip = d.ShowYTDInPayslip ?? false,
                                    TrackYTD = d.EmpTrack ?? false,
                                    YTD = (d.EmpTrack ?? false) ? (d.Amount + d.Balance ?? 0) : 0,
                                    Description = d.ItemId,
                                    EmpTxnId = d.EmpTxnId,
                                    TaxTracking = d.TaxTrackingId,
                                    Amount = d.Amount,
                                    ItemType = d.ItemTypeId
                                });
                return Earnings.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        //employees payable via banks
        public List<vwPayrollMaster> GetAllEmployeesWithBanks(int year, int period)
        {
            try
            {
                var ewb = (from e in db.vwPayrollMasters
                           where e.PaymentMode.Trim() == "BANKACCOUNT"
                           where e.Period == period
                           where e.Year == year
                           select e).Distinct().OrderBy(i => i.BankName);
                return ewb.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwPayrollMaster> GetAllBankswithBranches(int year, int period)
        {
            try
            {
                var ewb = (from e in db.vwPayrollMasters
                           where e.PaymentMode.Trim() == "BANKACCOUNT"
                           where e.Period == period
                           where e.Year == year
                           select e);
                return ewb.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public int GetEmployeesWithBanks(int year, int period)
        {
            try
            {
                var ewb = (from e in db.vwPayrollMasters
                           where e.PaymentMode.Trim() == "BANKACCOUNT"
                           where e.Period == period
                           where e.Year == year
                           select e.BankCode);
                return ewb.Count();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public List<EarningsDeductions> EmployeeDeductions(int EmployeeId)
        {
            try
            {
                List<string> ddTypes = new List<string>() { "DEDUCTION", "EMPECONTR", "STATUTORY", "TAX" };
                var Deductions = (from d in db.GetEmpTransactions
                                  where d.EmployeeId == EmployeeId
                                  where ddTypes.Contains(d.Parent.Trim())
                                  where d.Enabled == true
                                  where d.Recurrent == true || (d.Recurrent == false && d.Processed == false)
                                  select new EarningsDeductions
                                  {
                                      EmpTxnId = d.EmpTxnId,
                                      DEType = "D",
                                      ShowInPayslip = d.ShowYTDInPayslip ?? false,
                                      TrackYTD = d.EmpTrack ?? false,
                                      YTD = (d.EmpTrack ?? false) ? (d.Amount + d.Balance ?? 0) : 0,
                                      Description = d.ItemId,
                                      TaxTracking = d.TaxTrackingId,
                                      Amount = d.Amount,
                                      ItemType = d.ItemTypeId
                                  });

                return Deductions.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EarningsDeductions> GetAllDeductions()
        {
            try
            {
                List<string> ddTypes = new List<string>() { "DEDUCTION", "EMPECONTR", "STATUTORY", "TAX" };
                var Deductions = (from d in db.GetEmpTransactions
                                  where ddTypes.Contains(d.Parent.Trim())
                                  where d.Enabled == true
                                  where d.Recurrent == true || (d.Recurrent == false && d.Processed == false)
                                  select new EarningsDeductions
                                  {
                                      EmpTxnId = d.EmpTxnId,
                                      DEType = "D",
                                      ShowInPayslip = d.ShowYTDInPayslip ?? false,
                                      TrackYTD = d.EmpTrack ?? false,
                                      YTD = (d.EmpTrack ?? false) ? (d.Amount + d.Balance ?? 0) : 0,
                                      Description = d.ItemId,
                                      TaxTracking = d.TaxTrackingId,
                                      Amount = d.Amount,
                                      ItemType = d.ItemTypeId
                                  });

                return Deductions.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EarningsDeductions> ExcludeAllDeductions(string EmpNo)
        {
            try
            {
                List<string> ddTypes = new List<string>() { "EMPECONTR", "STATUTORY", "TAX" };
                var Deductions = (from d in db.GetEmpTransactions
                                  where d.EmpNo == EmpNo
                                  where ddTypes.Contains(d.Parent.Trim())
                                  where d.Enabled == true
                                  where d.Recurrent == true || (d.Recurrent == false && d.Processed == false)
                                  select new EarningsDeductions
                                  {
                                      EmpTxnId = d.EmpTxnId,
                                      DEType = "D",
                                      ShowInPayslip = d.ShowYTDInPayslip ?? false,
                                      TrackYTD = d.EmpTrack ?? false,
                                      YTD = (d.EmpTrack ?? false) ? (d.Amount + d.Balance ?? 0) : 0,
                                      Description = d.ItemId,
                                      TaxTracking = d.TaxTrackingId,
                                      Amount = d.Amount
                                  });

                return Deductions.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EarningsDeductions> GetAllOtherDeductions(string EmpNo)
        {
            try
            {
                List<string> _ddTypes = new List<string>() { "DEDUCTION", "EMPECONTR", "STATUTORY", "TAX" };
                List<string> ddTypes = new List<string>() { "LOAN", "SACCO" };
                List<string> earningTypes = new List<string>() { "ADDITION", "SALARY" };
                var Deductions = (from d in db.GetEmpTransactions
                                  where d.EmpNo == EmpNo
                                  where _ddTypes.Contains(d.Parent.Trim())
                                  where !ddTypes.Contains(d.ItemTypeId.Trim())
                                  where !earningTypes.Contains(d.ItemTypeId.Trim())
                                  where d.Enabled == true
                                  where d.Recurrent == true || (d.Recurrent == false && d.Processed == false)
                                  select new EarningsDeductions
                                  {
                                      EmpTxnId = d.EmpTxnId,
                                      DEType = "D",
                                      ShowInPayslip = d.ShowYTDInPayslip ?? false,
                                      TrackYTD = d.EmpTrack ?? false,
                                      YTD = (d.EmpTrack ?? false) ? (d.Amount + d.Balance ?? 0) : 0,
                                      Description = d.ItemId,
                                      TaxTracking = d.TaxTrackingId,
                                      Amount = d.Amount
                                  });

                return Deductions.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EarningsDeductions> GetAllOtherDeductions()
        {
            try
            {
                List<string> _ddTypes = new List<string>() { "DEDUCTION", "EMPECONTR", "STATUTORY", "TAX" };
                List<string> ddTypes = new List<string>() { "LOAN", "SACCO" };
                List<string> earningTypes = new List<string>() { "ADDITION", "SALARY" };
                var Deductions = (from d in db.GetEmpTransactions
                                  where _ddTypes.Contains(d.Parent.Trim())
                                  where !ddTypes.Contains(d.ItemTypeId.Trim())
                                  where !earningTypes.Contains(d.ItemTypeId.Trim())
                                  where d.Enabled == true
                                  where d.Recurrent == true || (d.Recurrent == false && d.Processed == false)
                                  select new EarningsDeductions
                                  {
                                      EmpTxnId = d.EmpTxnId,
                                      DEType = "D",
                                      ShowInPayslip = d.ShowYTDInPayslip ?? false,
                                      TrackYTD = d.EmpTrack ?? false,
                                      YTD = (d.EmpTrack ?? false) ? (d.Amount + d.Balance ?? 0) : 0,
                                      Description = d.ItemId,
                                      TaxTracking = d.TaxTrackingId,
                                      Amount = d.Amount
                                  });

                return Deductions.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<GetEmpTransaction> GetEmpTransactions(string Emp)
        {
            try
            {
                return db.GetEmpTransactions.Where(t => t.EmpNo == Emp).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion  "Employee"
        #region "Payeerates"
        public List<PayeeRate> PayeeRates()
        {
            try
            {


                var Paye = (from d in db.PayeeRates
                            orderby d.FromAmt ascending
                            select d);
                return Paye.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }

        }
        public PayeeRate PayeeRates(int Bracketid)
        {
            try
            {

                var Paye = (from d in db.PayeeRates
                            orderby d.FromAmt ascending
                            where d.Id == Bracketid
                            select d);
                return Paye.Single();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }

        }
        #endregion "Payeerates"
        #region "PayslipMaster"
        public void AddPaySlipMaster(PayslipMaster row)
        {
            try
            {

                db.PayslipMasters.AddObject(row);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<PayslipMaster> GetPaySlipFromMaster(string Emp)
        {
            try
            {

                return db.PayslipMasters.Where(p => p.EmpNo == Emp).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<PayslipMaster> GetPaySlipFromMaster(int year, int period)
        {
            try
            {

                return db.PayslipMasters.Where(p => p.Year == year && p.Period == period).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayrollMaster> GetBankBranchesFromPayrollMaster(bool current, int period, int year)
        {
            if (current)
            {
                return GetBankBranchesFromPayrollMaster_Temp(period, year);
            }
            else
            {
                return GetBankBranchesFromPayrollMaster_Hist(period, year);
            }
        }
        public List<psuedovwPayrollMaster> GetBankBranchesFromPayrollMaster_Hist(int period, int year)
        {
            try
            {

                var pm = db.vwPayrollMasters.Where(v => v.Period == period && v.Year == year)
                    .Select(v => new psuedovwPayrollMaster
                    {
                        BankName = v.BankName,
                        BranchName = v.BranchName,
                        EmpNo = v.EmpNo,
                        Surname = v.Surname,
                        OtherDeductions = v.OtherDeductions,
                        OtherNames = v.OtherNames,
                        BankAccount = v.BankAccount,
                        NetPay = v.NetPay,
                        Department = v.Department,
                        GrossTaxableEarnings = v.GrossTaxableEarnings,
                        BankCode = v.BankCode,
                        BasicPay = v.BasicPay,
                        NHIF = v.NHIF,
                        NSSF = v.NSSF,
                        EmployerNSSF = v.EmployerNSSF,
                        PayeTax = v.PayeTax,
                        PensionEmployee = v.PensionEmployee,
                        Period = v.Period,
                        Year = v.Year,
                        IDNo = v.IDNo,
                        PINNo = v.PIN,
                        NHIFNo = v.NHIFNo,
                        NSSFNo = v.NSSFNo,
                        Benefits = v.Benefits
                    }).OrderBy(i => i.BankCode).ToList();
                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayrollMaster> GetBankBranchesFromPayrollMaster_Temp(int period, int year)
        {
            try
            {

                var pm = db.vwPayrollMaster_Temp
                         .Where(r => r.Period == period && r.Year == year)
                         .Select(v => new psuedovwPayrollMaster
                         {
                             BankName = v.BankName,
                             BranchName = v.BranchName,
                             EmpNo = v.EmpNo,
                             Surname = v.Surname,
                             OtherDeductions = v.OtherDeductions,
                             OtherNames = v.OtherNames,
                             BankAccount = v.BankAccount,
                             NetPay = v.NetPay,
                             Department = v.Department,
                             GrossTaxableEarnings = v.GrossTaxableEarnings,
                             BankCode = v.BankCode,
                             BasicPay = v.BasicPay,
                             NHIF = v.NHIF,
                             NSSF = v.NSSF,
                             EmployerNSSF = v.EmployerNSSF,
                             PayeTax = v.PayeTax,
                             PensionEmployee = v.PensionEmployee,
                             Period = v.Period,
                             Year = v.Year,
                             IDNo = v.IDNo,
                             PINNo = v.PIN,
                             NHIFNo = v.NHIFNo,
                             NSSFNo = v.NSSFNo,
                             Benefits = v.Benefits
                         }).OrderBy(i => i.BankCode).ToList();

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayrollMaster> GetPayrollMaster(bool current, int period, int year)
        {
            if (current)
            {
                return GetPayrollMaster_Temp(period, year);
            }
            else
            {
                return GetPayrollMaster_Hist(period, year);
            }
        }
        public List<psuedovwPayrollMaster> GetPayrollMaster_Hist(int period, int year)
        {
            try
            {
                List<psuedovwPayrollMaster> psuedovwPayrollMasters = new List<psuedovwPayrollMaster>();
                var _vwPayrollMaster_Tempquery = from vw in db.vwPayrollMasters
                                                 where vw.Period == period
                                                 where vw.Year == year
                                                 select vw;
                List<vwPayrollMaster> vwPayrollMaster_Temps = _vwPayrollMaster_Tempquery.ToList();
                foreach (var v in vwPayrollMaster_Temps)
                {
                    psuedovwPayrollMaster _payroll = new psuedovwPayrollMaster();
                    _payroll.BankSortCode = v.BankSortCode;
                    _payroll.BankCode = v.BankCode;
                    _payroll.BankName = v.BankName;
                    _payroll.BranchCode = v.BranchCode;
                    _payroll.BranchName = v.BranchName;
                    _payroll.EmpNo = v.EmpNo;
                    _payroll.Surname = v.Surname;
                    _payroll.OtherDeductions = v.OtherDeductions;
                    _payroll.OtherNames = v.OtherNames;
                    _payroll.BankAccount = v.BankAccount;
                    _payroll.NetPay = v.NetPay;
                    _payroll.Department = v.Department;
                    _payroll.GrossTaxableEarnings = v.GrossTaxableEarnings;
                    _payroll.BankCode = v.BankCode;
                    _payroll.EmployerName = v.CompName;
                    _payroll.BasicPay = v.BasicPay;
                    _payroll.NHIF = v.NHIF;
                    _payroll.NSSF = v.NSSF;
                    _payroll.EmployerNSSF = v.EmployerNSSF;
                    _payroll.PayeTax = v.PayeTax;
                    _payroll.PensionEmployee = v.PensionEmployee;
                    _payroll.Period = v.Period;
                    _payroll.Year = v.Year;
                    _payroll.IDNo = v.IDNo;
                    _payroll.PINNo = v.PIN;
                    _payroll.NHIFNo = v.NHIFNo;
                    _payroll.NSSFNo = v.NSSFNo;
                    _payroll.Benefits = v.Benefits;
                    _payroll.EmployeeDeductions = this.EmployeeDeductions(v.EmployeeId);
                    _payroll.EmployeeEarnings = this.EmployeeEarnings(v.EmployeeId);

                    psuedovwPayrollMasters.Add(_payroll);
                }

                return psuedovwPayrollMasters.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayrollMaster> GetPayrollMaster_Temp(int period, int year)
        {
            try
            {
                List<psuedovwPayrollMaster> psuedovwPayrollMasters = new List<psuedovwPayrollMaster>();
                var _vwPayrollMaster_Tempquery = from vw in db.vwPayrollMaster_Temp
                                                 where vw.Period == period
                                                 where vw.Year == year
                                                 select vw;
                List<vwPayrollMaster_Temp> vwPayrollMaster_Temps = _vwPayrollMaster_Tempquery.ToList();
                foreach (var v in vwPayrollMaster_Temps)
                {
                    psuedovwPayrollMaster _payroll = new psuedovwPayrollMaster();
                    _payroll.BankSortCode = v.BankSortCode;
                    _payroll.BankCode = v.BankCode;
                    _payroll.BankName = v.BankName;
                    _payroll.BranchCode = v.BranchCode;
                    _payroll.BranchName = v.BranchName;
                    _payroll.EmpNo = v.EmpNo;
                    _payroll.Surname = v.Surname;
                    _payroll.OtherDeductions = v.OtherDeductions;
                    _payroll.OtherNames = v.OtherNames;
                    _payroll.BankAccount = v.BankAccount;
                    _payroll.NetPay = v.NetPay;
                    _payroll.Department = v.Department;
                    _payroll.GrossTaxableEarnings = v.GrossTaxableEarnings;
                    _payroll.BankCode = v.BankCode;
                    _payroll.EmployerName = v.CompName;
                    _payroll.BasicPay = v.BasicPay;
                    _payroll.NHIF = v.NHIF;
                    _payroll.NSSF = v.NSSF;
                    _payroll.EmployerNSSF = v.EmployerNSSF;
                    _payroll.PayeTax = v.PayeTax;
                    _payroll.PensionEmployee = v.PensionEmployee;
                    _payroll.Period = v.Period;
                    _payroll.Year = v.Year;
                    _payroll.IDNo = v.IDNo;
                    _payroll.PINNo = v.PIN;
                    _payroll.NHIFNo = v.NHIFNo;
                    _payroll.NSSFNo = v.NSSFNo;
                    _payroll.Benefits = v.Benefits;
                    _payroll.EmployeeDeductions = this.EmployeeDeductions(v.EmployeeId);
                    _payroll.EmployeeEarnings = this.EmployeeEarnings(v.EmployeeId);

                    psuedovwPayrollMasters.Add(_payroll);
                }

                return psuedovwPayrollMasters.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayslipDetails> GetvwPayslipDetails(string itemid, bool current, int period, int year)
        {
            if (current)
            {
                return GetvwPayslipDetails_Temp(itemid, period, year);
            }
            else
            {
                return GetvwPayslipDetails_Hist(itemid, period, year);
            }
        }
        public List<psuedovwPayslipDetails> GetvwPayslipDetails_Hist(string itemid, int period, int year)
        {
            try
            {
                var pm = db.vwPayslipDets.Where(v => v.Period == period && v.Year == year && v.ItemTypeId == itemid)
                    .Select(v => new psuedovwPayslipDetails
                    {

                        EmpNo = v.EmpNo,
                        Surname = v.Surname,
                        OtherNames = v.OtherNames,
                        BankAccount = v.BankAccount,
                        BankCode = v.BankCode,
                        Period = v.Period,
                        PostDate = v.PostDate,
                        Year = v.Year ?? 0,
                        ItemId = v.ItemId,
                        Balance = v.YTD ?? 0,
                        Amount = v.Amount,
                        YTD = v.YTD ?? 0
                    });
                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayslipDetails> GetvwPayslipDetails_Temp(string itemid, int period, int year)
        {
            try
            {

                var pm = db.vwPayslipDet_Temp
                         .Where(r => r.Period == period && r.Year == year && r.ItemTypeId == itemid)
                         .Select(v => new psuedovwPayslipDetails
                         {
                             EmpNo = v.EmpNo,
                             Surname = v.Surname,
                             OtherNames = v.OtherNames,
                             BankAccount = v.BankAccount,
                             BankCode = v.BankCode,
                             Period = v.Period,
                             PostDate = v.PostDate,
                             Year = v.Year ?? 0,
                             ItemId = v.ItemId,
                             Balance = v.YTD ?? 0,
                             Amount = v.Amount,
                             YTD = v.YTD ?? 0
                         });
                //.AsQueryable(); // actually it's not useful after "ToList()" :D

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayslipDetails> GetvwPayslipDetailsAdvance(string itemid, bool current, int period, int year)
        {
            if (current)
            {
                return GetvwPayslipDetailsAdvance_Temp(itemid, period, year);
            }
            else
            {
                return GetvwPayslipDetailsAdvance_Hist(itemid, period, year);
            }
        }
        public List<psuedovwPayslipDetails> GetvwPayslipDetailsAdvance_Hist(string itemid, int period, int year)
        {
            try
            {

                var pm = db.vwPayslipDets.Where(v => v.Period == period && v.Year == year && v.ItemId == itemid)
                    .Select(v => new psuedovwPayslipDetails
                    {

                        EmpNo = v.EmpNo,
                        Surname = v.Surname,
                        OtherNames = v.OtherNames,
                        BankAccount = v.BankAccount,
                        BankCode = v.BankCode,
                        Period = v.Period,
                        Year = v.Year ?? 0,
                        ItemId = v.ItemId,
                        Balance = v.YTD ?? 0,
                        Amount = v.Amount,
                        YTD = v.YTD ?? 0
                    });
                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayslipDetails> GetvwPayslipDetailsAdvance_Temp(string itemid, int period, int year)
        {
            try
            {

                var pm = db.vwPayslipDet_Temp
                         .Where(r => r.Period == period && r.Year == year && r.ItemId == itemid)
                         .Select(v => new psuedovwPayslipDetails
                         {
                             EmpNo = v.EmpNo,
                             Surname = v.Surname,
                             OtherNames = v.OtherNames,
                             BankAccount = v.BankAccount,
                             BankCode = v.BankCode,
                             Period = v.Period,
                             Year = v.Year ?? 0,
                             ItemId = v.ItemId,
                             Balance = v.YTD ?? 0,
                             Amount = v.Amount,
                             YTD = v.YTD ?? 0
                         });
                //.AsQueryable(); // actually it's not useful after "ToList()" :D

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "PayslipMaster"
        #region "Bank & Branches"
        public void AddNewBank(string bankcode, string bankname)
        {
            try
            {
                Bank bank = new Bank();
                bank.BankCode = bankcode;
                bank.BankName = bankname;

                if (!db.Banks.Any(i => i.BankName == bank.BankName && i.BankCode == bank.BankCode))
                {
                    db.Banks.AddObject(bank);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void AddBankBranch(string branchsortcode, string branchcode, string bankCode, string branchname)
        {
            try
            {
                BankBranch bankbranch = new BankBranch();
                bankbranch.BankSortCode = branchsortcode;
                bankbranch.BranchCode = branchcode;
                bankbranch.Bank.BankCode = bankCode;
                bankbranch.BranchName = branchname;

                if (!db.BankBranches.Any(i => i.BranchName == bankbranch.BranchName && i.BankSortCode == bankbranch.BankSortCode && i.Bank == bankbranch.Bank))
                {
                    db.BankBranches.AddObject(bankbranch);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }
        public void UpdateBank(Bank _bank)
        {
            try
            {
                Bank bank = db.Banks.First(b => b.BankCode == _bank.BankCode);
                bank.BankName = _bank.BankName;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateBank(string bankcode, string bankname)
        {
            try
            {
                Bank bank = db.Banks.First(b => b.BankCode == bankcode);
                bank.BankName = bankname;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateBankBranch(string banksortcode, string branchcode, string bank, string branchname)
        {
            try
            {
                BankBranch bankbranch = db.BankBranches.First(bb => bb.Bank.BankCode == bank && bb.BankSortCode == banksortcode);
                bankbranch.BranchCode = branchcode;
                bankbranch.BranchName = branchname;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateBranch(BankBranch branch)
        {
            try
            {
                BankBranch bankbranch = db.BankBranches.First(b => b.Bank == branch.Bank && b.BankSortCode == branch.BankSortCode);
                bankbranch.BranchCode = branch.BranchCode;
                bankbranch.BranchName = branch.BranchName;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteBank(string bankcode)
        {
            try
            {
                Bank _bank = db.Banks.Where(b => b.BankCode == bankcode).Single();
                db.Banks.DeleteObject(_bank);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteBankBranch(string banksortcode, string bank)
        {
            try
            {
                BankBranch bankbranch = db.BankBranches.Where(bb => bb.Bank.BankCode == bank && bb.BankSortCode == banksortcode).Single();

                db.DeleteObject(bankbranch);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteAllBankBranches(string bank)
        {
            try
            {
                var branchesquery = from bb in db.BankBranches
                                    where bb.Bank.BankCode == bank
                                    select bb;
                foreach (var b in branchesquery.ToList())
                {
                    db.BankBranches.DeleteObject(b);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);

            }
        }
        public List<Bank> GetBanks()
        {
            try
            {
                return db.Banks.OrderBy(i => i.BankCode).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwBankBranch> GetBankBranches()
        {
            try
            {
                return db.vwBankBranches.OrderBy(i => i.BankName).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<BankBranch> GetAllBranches()
        {
            try
            {
                var _branches = from bb in db.BankBranches
                                select bb;
                return _branches.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwBankBranch> GetBankBranchesFromCriteria(List<CriterionItem> Criteria)
        {
            try
            {
                IQueryable<vwBankBranch> query = db.vwBankBranches;

                foreach (CriterionItem ci in Criteria)
                {
                    switch (ci.Criterion.FieldName.ToLower())
                    {

                        case "bankcode":
                            switch (ci.Criterion.Opr.Name)
                            {
                                case "equal": query = query.Where(e => e.BankCode == ci.Criterion.FValue);
                                    break;
                                case "notequal": query = query.Where(e => e.BankCode != ci.Criterion.FValue);
                                    break;
                                case "startswith": query = query.Where(e => e.BankCode.StartsWith
                                    (ci.Criterion.FValue));
                                    break;
                                case "endswith": query = query.Where(e => e.BankCode.EndsWith(ci.Criterion.FValue));
                                    break;
                                case "has": query = query.Where(e => e.BankCode.Contains(ci.Criterion.FValue));
                                    break;
                            }
                            break;
                        case "bankname":
                            switch (ci.Criterion.Opr.Name)
                            {
                                case "equal": query = query.Where(e => e.BankName == ci.Criterion.FValue);
                                    break;
                                case "notequal": query = query.Where(e => e.BankName != ci.Criterion.FValue);
                                    break;
                                case "startswith": query = query.Where(e => e.BankName.StartsWith
                                    (ci.Criterion.FValue));
                                    break;
                                case "endswith": query = query.Where(e => e.BankName.EndsWith(ci.Criterion.FValue));
                                    break;
                                case "has": query = query.Where(e => e.BankName.Contains(ci.Criterion.FValue));
                                    break;
                            }
                            break;
                        case "branchcode":
                            switch (ci.Criterion.Opr.Name)
                            {
                                case "equal": query = query.Where(e => e.BranchCode == ci.Criterion.FValue);
                                    break;
                                case "notequal": query = query.Where(e => e.BranchCode != ci.Criterion.FValue);
                                    break;
                                case "startswith": query = query.Where(e => e.BranchCode.StartsWith
                                    (ci.Criterion.FValue));
                                    break;
                                case "endswith": query = query.Where(e => e.BranchCode.EndsWith(ci.Criterion.FValue));
                                    break;
                                case "has": query = query.Where(e => e.BranchCode.Contains(ci.Criterion.FValue));
                                    break;
                            }
                            break;
                        case "branchname":
                            switch (ci.Criterion.Opr.Name)
                            {
                                case "equal": query = query.Where(e => e.BranchName == ci.Criterion.FValue);
                                    break;
                                case "notequal": query = query.Where(e => e.BranchName != ci.Criterion.FValue);
                                    break;
                                case "startswith": query = query.Where(e => e.BranchName.StartsWith
                                    (ci.Criterion.FValue));
                                    break;
                                case "endswith": query = query.Where(e => e.BranchName.EndsWith(ci.Criterion.FValue));
                                    break;
                                case "has": query = query.Where(e => e.BranchName.Contains(ci.Criterion.FValue));
                                    break;
                            }
                            break;
                        case "banksortcode":
                            switch (ci.Criterion.Opr.Name)
                            {
                                case "equal": query = query.Where(e => e.BankSortCode == ci.Criterion.FValue);
                                    break;
                                case "notequal": query = query.Where(e => e.BankSortCode != ci.Criterion.FValue);
                                    break;
                                case "startswith": query = query.Where(e => e.BankSortCode.StartsWith
                                    (ci.Criterion.FValue));
                                    break;
                                case "endswith": query = query.Where(e => e.BankSortCode.EndsWith(ci.Criterion.FValue));
                                    break;
                                case "has": query = query.Where(e => e.BankSortCode.Contains(ci.Criterion.FValue));
                                    break;
                            }
                            break;
                    }
                }
                List<vwBankBranch> empl = query.ToList();
                return empl;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwPayrollMaster> GetBankBranch(string branchsortCode, int bankcode, ref string branchcode)
        {
            try
            {
                var banks = db.vwPayrollMasters.Include("BranchCode");
                return banks.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public bool GetBankBranch(string sortCode, ref string BankName, ref string BranchName)
        {
            try
            {
                var branch = db.BankBranches.Where(b => b.BankSortCode == sortCode).SingleOrDefault();
                BankName = "Unknown"; BranchName = "Unknown";
                if (branch != null)
                {
                    BranchName = branch.BranchName;
                    BankName = branch.Bank.BankName;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }
        public string GetEmployerDefaultBankBranch(DAL.Employer _employer)
        {
            try
            {
                string _Branchname = string.Empty;
                var _empdefaultbanksortcodequery = from emp in db.EmployerBanks
                                                   where emp.EmployerId == _employer.Id
                                                   where emp.IsDefault == true
                                                   select emp;
                EmployerBank _employerBank = _empdefaultbanksortcodequery.FirstOrDefault();
                if (_employerBank != null)
                {
                    var _branchnamequery = from vw in db.vwBankBranches
                                           where vw.BankSortCode == _employerBank.BankSortCode
                                           select vw.BranchName;
                    _Branchname = _branchnamequery.FirstOrDefault();
                }
                return _Branchname;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string GetEmployerDefaultBankBranchForTransfers(DAL.Employer _employer)
        {
            try
            {
                string _Branchname = string.Empty;
                var _empdefaultbanksortcodequery = from emp in db.EmployerBanks
                                                   where emp.EmployerId == _employer.Id
                                                   where emp.IsDefault == true
                                                   select emp;
                EmployerBank _employerBank = _empdefaultbanksortcodequery.FirstOrDefault();
                if (_employerBank != null)
                {
                    var _branchnamequery = from vw in db.vwBankBranches
                                           where vw.BankSortCode == _employerBank.BankSortCode
                                           select vw.BranchName;
                    _Branchname = _branchnamequery.FirstOrDefault();
                }
                return _Branchname;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string GetEmployerDefaultBank(DAL.Employer _employer)
        {
            try
            {
                string _Bankname = string.Empty;
                var _empdefaultbanksortcodequery = from emp in db.EmployerBanks
                                                   where emp.EmployerId == _employer.Id
                                                   where emp.IsDefault == true
                                                   select emp;
                EmployerBank _employerBank = _empdefaultbanksortcodequery.FirstOrDefault();
                if (_employerBank != null)
                {
                    var _banknamequery = from vw in db.vwBankBranches
                                         where vw.BankSortCode == _employerBank.BankSortCode
                                         select vw.BankName;
                    _Bankname = _banknamequery.FirstOrDefault();
                }
                return _Bankname;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string GetEmployerDefaultBankForTransfers(DAL.Employer _employer)
        {
            try
            {
                string _Bankname = string.Empty;
                var _empdefaultbanksortcodequery = from emp in db.EmployerBanks
                                                   where emp.EmployerId == _employer.Id
                                                   where emp.IsDefault == true
                                                   select emp;
                EmployerBank _employerBank = _empdefaultbanksortcodequery.FirstOrDefault();
                if (_employerBank != null)
                {
                    var _banknamequery = from vw in db.vwBankBranches
                                         where vw.BankSortCode == _employerBank.BankSortCode
                                         select vw.BankName;
                    _Bankname = _banknamequery.FirstOrDefault();
                }
                return _Bankname;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public IQueryable<Bank> GetBankQuery()
        {
            try
            {
                var banks = db.Banks.Include("BankBranches");
                return banks;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string GetBankBranchName(string banksortCode)
        {
            try
            {
                var _branchnamequery = (from vw in db.vwBankBranches
                                        where vw.BankSortCode == banksortCode
                                        select vw.BankBranchName).FirstOrDefault();
                return _branchnamequery;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string GetBankBranchCode(string banksortCode)
        {
            try
            {
                var _branchcodequery = (from vw in db.vwBankBranches
                                        where vw.BankSortCode == banksortCode
                                        select vw.BranchCode).FirstOrDefault();
                return _branchcodequery;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string GetBankName(string banksortCode)
        {
            try
            {
                var _banknamequery = (from vw in db.vwBankBranches
                                      where vw.BankSortCode == banksortCode
                                      select vw.BankName).FirstOrDefault();
                return _banknamequery;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string GetBankCode(string banksortCode)
        {
            try
            {
                var _bankcodequery = (from vw in db.vwBankBranches
                                      where vw.BankSortCode == banksortCode
                                      select vw.BankCode).FirstOrDefault();
                return _bankcodequery;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion  "Bank & Branches"
        #region "Department"
        public List<Department> GetDepartments()
        {
            try
            {
                return db.Departments.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Department> GetNonDeletedDepartments()
        {
            try
            {
                return db.Departments.Where(i => i.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void AddDepartment(string code, string description)
        {
            try
            {
                Department department = new Department();
                department.Code = code;
                department.Description = description;

                if (!db.Departments.Any(i => i.Code == department.Code))
                {
                    db.Departments.AddObject(department);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteDepartment(string code)
        {
            try
            {
                Department dt = db.Departments.Where(i => i.Code == code).Single();
                db.Departments.DeleteObject(dt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteDepartment(Department _dt)
        {
            try
            {
                Department dt = db.Departments.Where(i => i.Id == _dt.Id).Single();
                dt.IsDeleted = true;

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateDepartment(string code, string description)
        {
            try
            {
                Department dt = db.Departments.Where(i => i.Code == code).Single();
                dt.Description = description;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateDepartment(Department _department)
        {
            try
            {
                Department dt = db.Departments.Where(i => i.Id == _department.Id).Single();
                dt.Code = _department.Code;
                dt.Description = _department.Description;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        #endregion "Department"
        #region "Payroll"
        public void MarkPayrollAsProcessed(int period, int year)
        {
            try
            {

                var payroll = (from p in db.Payrolls
                               where p.Period == period
                               where p.Year == year
                               select p).SingleOrDefault();
                if (payroll != null) payroll.Processed = true;
                EmployeeTransaction emptrans = new EmployeeTransaction();
                emptrans.Processed = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);

            }
        }
        public void UpdatePayrollDateRun(Payroll payroll)
        {
            try
            {
                Payroll pi = db.Payrolls.Where(i => i.Period == payroll.Period && i.Year == payroll.Year).Single();
                pi.DateRun = payroll.DateRun;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void MarkPayrollAsClosed(int period, int year)
        {
            try
            {

                var payroll = (from p in db.Payrolls
                               where p.Period == period
                               where p.Year == year
                               select p).SingleOrDefault();
                if (payroll != null) payroll.IsOpen = false;
                payroll.Processed = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);

            }
        }
        public void ReOpenPayroll(int period, int year)
        {
            try
            {

                var payroll = (from p in db.Payrolls
                               where p.Period == period
                               where p.Year == year
                               select p).SingleOrDefault();
                if (payroll != null) payroll.IsOpen = true;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);

            }
        }
        public void ClosePayroll()
        { }
        public List<Payroll> GetProcessedPayroll(bool procFlag)
        {
            try
            {

                var payroll = (from p in db.Payrolls
                               where p.Processed == procFlag
                               select p);
                return payroll.ToList();

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;

            }
        }
        public List<Payroll> GetOpenPayroll(bool procFlag)
        {
            try
            {

                var payroll = (from p in db.Payrolls
                               where p.IsOpen == procFlag
                               select p);
                return payroll.ToList();

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;

            }
        }
        public void AddPayroll(int period, int year, int owner, DateTime daterun, string runby,
            bool approved, string approvedby, bool isopen, bool processed)
        {
            try
            {
                Payroll payroll = new Payroll();
                payroll.Period = period;
                payroll.Year = year;
                payroll.EmployerId = owner;
                payroll.DateRun = daterun;
                payroll.RunBy = runby;
                payroll.Approved = approved;
                payroll.ApprovedBy = approvedby;
                payroll.IsOpen = isopen;
                payroll.Processed = processed;

                if (!db.Payrolls.Any(i => i.Period == payroll.Period && i.Year == payroll.Year))
                {
                    db.Payrolls.AddObject(payroll);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Log.WriteToErrorLogFile(e);
                return;
            }
        }
        public void AddPayroll(Payroll _payroll)
        {
            try
            {
                Payroll payroll = new Payroll();
                payroll.Period = _payroll.Period;
                payroll.Year = _payroll.Year;
                payroll.EmployerId = _payroll.EmployerId;
                payroll.DateRun = _payroll.DateRun;
                payroll.RunBy = _payroll.RunBy;
                payroll.Approved = _payroll.Approved;
                payroll.ApprovedBy = _payroll.ApprovedBy;
                payroll.IsOpen = _payroll.IsOpen;
                payroll.Processed = _payroll.Processed;

                if (!db.Payrolls.Any(i => i.Period == payroll.Period && i.Year == payroll.Year))
                {
                    db.Payrolls.AddObject(payroll);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Log.WriteToErrorLogFile(e);
                return;
            }
        }
        public List<Payroll> GetPayrolls()
        {
            try
            {
                return db.Payrolls.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Payroll> GetPayrolls(int year)
        {
            try
            {
                return db.Payrolls.Where(p => p.Year == year).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Payroll"
        #region "Payslip"
        public List<vwPayslipDet> GetEarningsAndDeductionsFromDb(int EmployeeId, string EmpNo, int PaymentPeriod, int year)
        {
            try
            {

                var pm = (from p in db.vwPayslipDets
                          where p.EmployeeId == EmployeeId
                          where p.Period == PaymentPeriod
                          where p.Year == year
                          select p);

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwPayslipDet_Temp> GetEarningsAndDeductionsFromDb_Temp(int EmployeeId, string EmpNo, int PaymentPeriod, int year)
        {
            try
            {
                var pm = (from p in db.vwPayslipDet_Temp
                          where p.EmployeeId == EmployeeId
                          where p.Period == PaymentPeriod
                          where p.Year == year
                          select p);

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwPayslipDet> GetEarningsAndDeductionsFromvwPayslipDet(int EmployeeId, string EmpNo, int PaymentPeriod, int year)
        {
            try
            {

                var pm = (from p in db.vwPayslipDets
                          where p.EmployeeId == EmployeeId
                          where p.Period == PaymentPeriod
                          where p.Year == year
                          select p);

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public PayslipMaster GetPayslipFromMaster(int EmployeeId, string EmpNo, int PaymentPeriod, int year)
        {
            try
            {

                var pm = (from p in db.PayslipMasters
                          where p.Period == PaymentPeriod
                          where p.Year == year
                          where p.EmployeeId == EmployeeId
                          select p).SingleOrDefault();

                return pm;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public PayslipMaster_Temp GetPayslipFromMaster_Temp(int EmployeeId, string EmpNo, int PaymentPeriod, int year)
        {
            try
            {

                var pm = (from p in db.PayslipMaster_Temp
                          where p.Period == PaymentPeriod
                          where p.Year == year
                          where p.EmployeeId == EmployeeId
                          select p).SingleOrDefault();

                return pm;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public Payslip RetrievePayslip(bool temp, int EmployeeId, string EmpNo, int PaymentPeriod, int year)
        {
            if (temp)
            {
                return RetrievePayslip_Temp(EmployeeId, EmpNo, PaymentPeriod, year);
            }
            else
            {
                return RetrievePayslip(EmployeeId, EmpNo, PaymentPeriod, year);
            }
        }
        public Payslip RetrievePayslip(int EmployeeId, string EmpNo, int PaymentPeriod, int year)
        {
            try
            {

                Payslip ps = new Payslip();
                PayslipMaster pm = GetPayslipFromMaster(EmployeeId, EmpNo, PaymentPeriod, year);
                Employee employee = GetEmployee(EmployeeId);

                //Add employee details
                //emp info
                //populate from payslipmaster and payslipdet
                if (pm == null) return null;
                //Payment
                ps.BankBranch = employee.BankCode;
                ps.Account = employee.BankAccount;
                ps.EmpName = pm.EmpName;
                ps.EmpNo = pm.EmpNo;
                ps.PINNo = pm.PIN;
                ps.Department = pm.Department;
                ps.NHIFNO = pm.NHIFNo;
                ps.NSSFNO = pm.NSSFNo;
                ps.Period = pm.Period;
                ps.Year = pm.Year;
                ps.PaymentDate = pm.PaymentDate;
                ps.PrintedBy = pm.PrintedBy;
                ps.PrintedOn = pm.PrintedOn;
                ps.PayPoint = pm.PayPoint;  //HQ, etc             
                ps.EmpGroup = pm.EmpGroup; //PP,Temp
                ps.EmpPayroll = pm.EmpPayroll; //Exec; surb
                //employerbank info    
                ps.EmployerName = pm.CompName;
                ps.EmployerAddress = pm.CompAddr;
                ps.EmployerTelephone = pm.CompTel;
                ps.PayPoint = pm.PayPoint;
                ps.GrossTax = pm.GrossTax;
                ps.BasicPay = pm.BasicPay;
                ps.Variables = pm.Variables;
                ps.GrossTaxableEarnings = pm.GrossTaxableEarnings;
                ps.MortgageRelief = pm.MortgageRelief;
                ps.InsuranceRelief = pm.InsuranceRelief;
                ps.GrossTax = pm.GrossTax;
                ps.PersonalRelief = pm.PersonalRelief;
                ps.PensionEmployer = pm.PensionEmployer;
                ps.PensionEmployee = pm.PensionEmployee;
                ps.BankBranch = pm.BankBranch;
                ps.Account = pm.Account;
                ps.NSSFEmployer = pm.EmployerNSSF;

                List<EarningsDeductions> Earning = new List<EarningsDeductions>();
                List<EarningsDeductions> deductions = new List<EarningsDeductions>();
                List<EarningsDeductions> statutorydeductions = new List<EarningsDeductions>();
                List<NonCashBenefits> NonCashPayments = new List<NonCashBenefits>();

                foreach (var nc in GetNonCashBenefitsList(EmployeeId))
                {
                    NonCashPayments.Add(new NonCashBenefits
                    {
                        BenefitId = nc.BenefitId,
                        Description = nc.Description,
                        EmpNo = nc.EmpNo,
                        Quantity = nc.Quantity,
                        Rate = nc.Rate
                    });
                }

                foreach (var ed in GetEarningsAndDeductionsFromDb(EmployeeId, EmpNo, PaymentPeriod, year))
                {
                    if (ed.DEType == "D")
                    {
                        deductions.Add(new EarningsDeductions(
                            ed.EmpTxnId,
                            ed.ItemTypeId,
                            ed.DEType,
                            ed.Description,
                            ed.TaxTracking,
                            ed.Amount,
                            ed.ShowInPayslip ?? true,
                            true,
                            ed.YTD ?? 0,
                            ed.IsStatutory));

                    }
                    else if (ed.DEType == "E")
                    {
                        Earning.Add(new EarningsDeductions(
                            ed.EmpTxnId,
                            ed.ItemTypeId,
                            ed.DEType,
                            ed.Description,
                            ed.TaxTracking,
                            ed.Amount,
                            ed.ShowInPayslip ?? true,
                            true,
                            ed.YTD ?? 0,
                            false));
                    }
                }

                ps.NonCashPayments = NonCashPayments;
                ps.Earnings = Earning;
                ps.AllDeductions = deductions; //this is actually other deductions


                return ps;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public Payslip RetrievePayslip_Temp(int EmployeeId, string EmpNo, int PaymentPeriod, int year)
        {
            try
            {

                Payslip ps = new Payslip();
                PayslipMaster_Temp pm = GetPayslipFromMaster_Temp(EmployeeId, EmpNo, PaymentPeriod, year);

                if (pm == null) return null;

                //populate from payslipmaster and payslipdet

                ps.Period = pm.Period;
                ps.Year = pm.Year;
                ps.EmpNo = pm.EmpNo;
                ps.PaymentDate = pm.PaymentDate;
                ps.PrintedBy = pm.PrintedBy;
                ps.PrintedOn = pm.PrintedOn;
                ps.EmpName = pm.EmpName;
                ps.PayPoint = pm.PayPoint;
                ps.PINNo = pm.PIN;
                ps.Department = pm.Department;
                ps.NHIFNO = pm.NHIFNo;
                ps.NSSFNO = pm.NSSFNo;
                ps.EmpGroup = pm.EmpGroup;
                ps.EmpPayroll = pm.EmpPayroll;
                ps.EmployerName = pm.CompName;
                ps.EmployerAddress = pm.CompAddr;
                ps.EmployerTelephone = pm.CompTel;
                ps.GrossTax = pm.GrossTax;
                ps.BasicPay = pm.BasicPay;
                ps.Variables = pm.Variables;
                ps.GrossTaxableEarnings = pm.GrossTaxableEarnings;
                ps.MortgageRelief = pm.MortgageRelief;
                ps.InsuranceRelief = pm.InsuranceRelief;
                ps.GrossTax = pm.GrossTax;
                ps.PersonalRelief = pm.PersonalRelief;
                ps.PensionEmployer = pm.PensionEmployer;
                ps.PensionEmployee = pm.PensionEmployee;
                ps.BankBranch = pm.BankBranch;
                ps.Account = pm.Account;
                ps.NSSFEmployer = pm.EmployerNSSF;


                List<EarningsDeductions> Earning = new List<EarningsDeductions>();
                List<EarningsDeductions> deductions = new List<EarningsDeductions>();
                List<EarningsDeductions> statutorydeductions = new List<EarningsDeductions>();
                List<NonCashBenefits> NonCashPayments = new List<NonCashBenefits>();

                foreach (var nc in GetNonCashBenefitsList(EmployeeId))
                {
                    NonCashPayments.Add(new NonCashBenefits
                    {
                        BenefitId = nc.BenefitId,
                        Description = nc.Description,
                        EmpNo = nc.EmpNo,
                        Quantity = nc.Quantity,
                        Rate = nc.Rate
                    });
                }

                foreach (var ed in GetEarningsAndDeductionsFromDb_Temp(EmployeeId, EmpNo, PaymentPeriod, year))
                {
                    if (ed.DEType == "D")
                    {
                        deductions.Add(new EarningsDeductions(
                            ed.EmpTxnId,
                            ed.ItemTypeId,
                            ed.DEType,
                            ed.Description,
                            ed.TaxTracking,
                            ed.Amount,
                            ed.ShowInPayslip ?? true,
                            true,
                            ed.YTD ?? 0,
                            ed.IsStatutory));

                    }
                    else if (ed.DEType == "E")
                    {
                        Earning.Add(new EarningsDeductions(
                            ed.EmpTxnId,
                            ed.ItemTypeId,
                            ed.DEType,
                            ed.Description,
                            ed.TaxTracking,
                            ed.Amount,

                            ed.ShowInPayslip ?? true,
                            true,
                            ed.YTD ?? 0,
                            false));
                    }
                }

                ps.NonCashPayments = NonCashPayments;
                ps.Earnings = Earning;
                ps.AllDeductions = deductions; //this is actually other deductions

                return ps;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void SavePayslip(bool Simulate, Payslip payslip)
        {
            try
            {

                /*1.Create a new Payslipmaster instance
                 * 2.Populate from the payslip
                 * 3.Save the Payslip Master
                 * 4.create the payslip detail
                 *  -for each deduction,earning,Statutory deduction
                 *  4.1 create a new payslip detail
                 *  4.2 populate the new payslip detail,
                 *  4.3 save the payslip detail in the temp file
                 */
                try
                {
                    PayslipMaster_Temp pm = new PayslipMaster_Temp();

                    pm.Period = payslip.Period;
                    pm.Year = payslip.Year;
                    pm.EmpNo = payslip.EmpNo;
                    pm.EmployeeId = payslip.EmployeeId;
                    pm.PaymentDate = payslip.PaymentDate;
                    pm.PrintedBy = payslip.PrintedBy;
                    pm.PrintedOn = payslip.PrintedOn;
                    pm.EmpName = payslip.EmpName;
                    pm.Department = payslip.Department;
                    if (payslip.PayPoint != null)
                    {
                        pm.PayPoint = payslip.PayPoint;
                    }
                    if (payslip.PINNo != null)
                    {
                        pm.PIN = payslip.PINNo;
                    }
                    if (payslip.NSSFNO != null)
                    {
                        pm.NSSFNo = payslip.NSSFNO;
                    }
                    if (payslip.NHIFNO != null)
                    {
                        pm.NHIFNo = payslip.NHIFNO;
                    }
                    if (payslip.EmpGroup != null)
                    {
                        pm.EmpGroup = payslip.EmpGroup;
                    }
                    if (payslip.EmpPayroll != null)
                    {
                        pm.EmpPayroll = payslip.EmpPayroll;
                    }
                    if (payslip.EmployerName != null)
                    {
                        pm.CompName = payslip.EmployerName;
                    }
                    if (payslip.EmployerAddress != null)
                    {
                        pm.CompAddr = payslip.EmployerAddress;
                    }
                    if (payslip.EmployerTelephone != null)
                    {
                        pm.CompTel = payslip.EmployerTelephone;
                    }
                    pm.PayeTax = payslip.NetTax;
                    pm.BasicPay = payslip.BasicPay;
                    pm.Variables = payslip.Variables;
                    pm.OtherDeductions = payslip.OtherDeductions;
                    pm.GrossTaxableEarnings = payslip.GrossTaxableEarnings;
                    pm.NetTaxableEarnings = payslip.NetTaxableEarnings;
                    pm.MortgageRelief = payslip.MortgageRelief;
                    pm.InsuranceRelief = payslip.InsuranceRelief;
                    pm.GrossTax = payslip.GrossTax;
                    pm.PersonalRelief = payslip.PersonalRelief;
                    pm.PensionEmployer = payslip.PensionEmployer;
                    decimal nssf = payslip.AllDeductions.Where(e => e.Description.Trim().ToUpper().Equals("NSSF")).Single().Amount;
                    pm.PensionEmployee = payslip.PensionEmployee + nssf;
                    pm.BankBranch = payslip.BankBranch;
                    if (payslip.Account != null)
                    {
                        pm.Account = payslip.Account;
                    }
                    pm.EmployerNSSF = payslip.NSSFEmployer;
                    pm.NetPay = payslip.NetSalary;
                    pm.NHIF = payslip.AllDeductions.Where(e => e.Description.Trim().ToUpper().Equals("NHIF")).Single().Amount;
                    pm.NSSF = nssf;

                    db.PayslipMaster_Temp.AddObject(pm);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    string msg = "Error in payslip [" + payslip.EmpNo.Trim() + "]";
                    Exception ex = new Exception(msg, e);
                    Log.WriteToErrorLogFile(ex);
                    return;
                }

                //deductions
                foreach (var d in payslip.AllDeductions)
                {
                    PayslipDet_Temp pd = new PayslipDet_Temp();
                    pd.EmpNo = payslip.EmpNo;
                    pd.EmployeeId = payslip.EmployeeId;
                    pd.Period = payslip.Period;
                    pd.Year = payslip.Year;
                    pd.Description = d.Description;
                    pd.TaxTracking = d.TaxTracking;
                    pd.Amount = d.Amount;
                    pd.DEType = d.DEType;
                    pd.IsStatutory = d.IsStatutory;
                    pd.ShowInPayslip = d.ShowInPayslip;
                    pd.YTD = d.YTD;
                    pd.EmpTxnId = d.EmpTxnId;

                    //update YTD if we are not in simulation mode
                    if (!Simulate)
                    {
                        if (d.TrackYTD) db.EmployeeTransactions.Where(et => et.Id == d.EmpTxnId).Single().Balance = d.YTD;
                    }
                    if (d.IsStatutory) db.EmployeeTransactions.Where(et => et.Id == d.EmpTxnId).Single().Amount = d.Amount;


                    db.PayslipDet_Temp.AddObject(pd);
                }

                foreach (var d in payslip.Earnings)
                {
                    PayslipDet_Temp pd = new PayslipDet_Temp();
                    pd.EmpNo = payslip.EmpNo;
                    pd.EmployeeId = payslip.EmployeeId;
                    pd.Period = payslip.Period;
                    pd.Year = payslip.Year;
                    pd.Description = d.Description;
                    pd.TaxTracking = d.TaxTracking;
                    pd.Amount = d.Amount;
                    pd.DEType = d.DEType;
                    pd.IsStatutory = false;
                    pd.ShowInPayslip = d.ShowInPayslip;
                    pd.YTD = d.YTD;
                    pd.EmpTxnId = d.EmpTxnId;
                    //update YTD
                    if (!Simulate)
                    {
                        if (d.TrackYTD) db.EmployeeTransactions.Where(et => et.Id == d.EmpTxnId).Single().Balance = d.YTD;
                    }

                    db.PayslipDet_Temp.AddObject(pd);
                }

                //save to payslip det
                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);

            }
        }
        public void CopyPayslipsToPayroll(int Period, int Year)
        {
            try
            {

                //copy payslipmaster_temp --> payslipmaster
                db.CopyPayMaster();

                //copy payslipdet_temp --> payslipdet
                //also updates the YTD values
                db.CopyPayslipDet();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        //private static void TestBulkInsert4()
        //{
        //    // properties to exclude from mapping
        //    var nonMappedProperties = new string[] { "P1", "P2" };

        //    Func<PropertyDescriptor, bool> expression = x =>
        //        !nonMappedProperties.Contains(x.Name);

        //    //IEnumerable<Album3> data = GetData3();

        //    var bulkCopy = new BulkCopy()
        //    {
        //        BatchSize = 200,
        //        ConnectionString = ConnectionString,
        //        DestinationTableName = "dbo.Albums",
        //        ExpressionFilter = expression
        //    };

        //    bulkCopy.WriteToServer(data, SqlBulkCopyOptions.CheckConstraints);
        //}
        #endregion "Payslip"
        #region "Temp"
        public bool WorkingCopyNotClosed(ref int period, ref int year)
        {
            try
            {
                var cnt = db.PayslipMaster_Temp.Count();
                if (cnt > 0)
                {
                    var rec = db.PayslipMaster_Temp.First();
                    period = rec.Period;
                    year = rec.Year;
                }
                else
                {
                    period = -1;
                    year = -1;
                }
                return cnt > 0;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return false;
            }
        }
        #endregion "Temp"
        #region "Tax tracking"
        public void CreateTaxTracking(string Id, string Description)
        {
            try
            {

                TaxTracking tt = new TaxTracking();
                tt.Id = Id;
                tt.Description = Description;

                db.TaxTrackings.AddObject(tt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateTaxTracking(string Id, string Description)
        {
            try
            {

                TaxTracking tt = db.TaxTrackings.Where(t => t.Id == Id).Single();

                tt.Description = Description;
                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<TaxTracking> ListTaxTracking()
        {
            try
            {

                return db.TaxTrackings.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void DeleteTaxTracking(string Id)
        {
            try
            {

                TaxTracking tt = db.TaxTrackings.Where(t => t.Id == Id).Single();
                db.TaxTrackings.DeleteObject(tt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        #endregion  "Tax tracking"
        #region "Payroll Item Types"
        public void CreatePayrollItemTypes(string id, string description)
        {
            try
            {

                PayrollItemType pit = new PayrollItemType();
                pit.Description = description;
                pit.Id = id;
                db.PayrollItemTypes.AddObject(pit);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdatePayrollItemTypes(string id, string description)
        {
            try
            {

                PayrollItemType pit = db.PayrollItemTypes.Where(p => p.Id == id).Single();
                pit.Description = description;
                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeletePayrollItemTypes(string id, string description)
        {
            try
            {

                PayrollItemType pit = db.PayrollItemTypes.Where(p => p.Id == id).Single();
                db.PayrollItemTypes.DeleteObject(pit);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<PayrollItemType> PayrooItemTypeList()
        {
            try
            {

                return db.PayrollItemTypes.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion  "Payroll Item Types"
        #region "Payroll Items"
        public void CreatePayrollItems(string id, string itemtype, string taxtracking, string payableto, string glaccount, bool active, bool addtopension, int reference)
        {
            try
            {
                PayrollItem pi = new PayrollItem();
                pi.Id = id;
                pi.ItemTypeId = itemtype;
                pi.TaxTrackingId = taxtracking;
                pi.PayableTo = payableto;
                pi.GLAccount = glaccount;
                pi.Active = active;
                pi.AddToPension = addtopension;
                pi.ReFField = reference;

                if (!db.PayrollItems.Any(i => i.Id == pi.Id))
                {
                    db.AddToPayrollItems(pi);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdatePayrollItems(string id, string ItemType, string TaxTrackingId,
            string PayableTo, string GLAccount, bool Active, bool AddToPension, int reference)
        {
            try
            {
                PayrollItem pi = db.PayrollItems.Where(i => i.Id == id).Single();
                pi.ItemTypeId = ItemType;
                pi.TaxTrackingId = TaxTrackingId;
                pi.PayableTo = PayableTo;
                pi.GLAccount = GLAccount;
                pi.Active = Active;
                pi.AddToPension = AddToPension;
                pi.ReFField = reference;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdatePayrollItems(string id, string GLAccount)
        {
            try
            {
                PayrollItem pi = db.PayrollItems.Where(i => i.Id == id).Single();
                pi.GLAccount = GLAccount;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdatePayrollItems(PayrollItem payrollitem)
        {
            try
            {
                PayrollItem _PayrollItem = db.PayrollItems.Where(i => i.Id == payrollitem.Id).Single();
                _PayrollItem.ItemTypeId = payrollitem.ItemTypeId;
                _PayrollItem.TaxTrackingId = payrollitem.TaxTrackingId;
                _PayrollItem.PayableTo = payrollitem.PayableTo;
                _PayrollItem.GLAccount = payrollitem.GLAccount;
                _PayrollItem.ReFField = payrollitem.ReFField;
                _PayrollItem.DefaultItem = payrollitem.DefaultItem;
                _PayrollItem.AddToPension = payrollitem.AddToPension;
                _PayrollItem.Active = payrollitem.Active;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeletePayrollItems(string id)
        {
            try
            {
                PayrollItem pi = db.PayrollItems.Where(i => i.Id == id).Single();
                pi.Active = false;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeletePayrollItem(PayrollItem payrollitem)
        {
            try
            {
                PayrollItem pi = db.PayrollItems.Where(i => i.Id == payrollitem.Id).Single();
                pi.Active = false;
                pi.Enable = false;
                pi.IsDeleted = true;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<PayrollItem> GetActivePayrollItems()
        {
            try
            {
                return db.PayrollItems.Where(i => i.Active == true && i.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<PayrollItem> GetAllPayrollItems()
        {
            try
            {
                return db.PayrollItems.Where(i => i.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<PayrollItem> GetEditPayrollItemlist()
        {
            try
            {
                List<string> ddTypes = new List<string>() { "TAX", "STATUTORY" };
                List<PayrollItem> allPI = (from i in db.PayrollItems
                                           where !ddTypes.Contains(i.ItemTypeId.Trim())
                                           where i.Active == true
                                           where i.IsDeleted == false
                                           select i).ToList();
                return allPI;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<PayrollItem> PayrollItemlist(string EmpNo)
        {
            try
            {
                List<string> EmpPayItems = (from i in db.EmployeeTransactions
                                            where i.EmpNo == EmpNo
                                            select i.ItemId).ToList();

                List<string> ddTypes = new List<string>() { "TAX", "STATUTORY" };
                List<PayrollItem> allPI = (from i in db.PayrollItems
                                           where !ddTypes.Contains(i.Id.Trim())
                                           where !EmpPayItems.Contains(i.Id.Trim())
                                           where i.Active == true
                                           where i.IsDeleted == false
                                           select i).ToList();
                return allPI;

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public PayrollItem GetPayrollItem(string itemId)
        {
            try
            {
                return db.PayrollItems.Where(i => i.Id == itemId && i.Active == true && i.IsDeleted == false).Single();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<PayrollItem> GetPayrollItems()
        {
            try
            {
                return db.PayrollItems.Where(i => i.Active == true && i.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Payroll Items"
        #region "Transaction Definitions"
        public void CreateTxnDef(string txncode, string dataentry, string payrollItemId, decimal amt,
            bool enabled, bool recur, bool track)
        {
            try
            {

                TransactionDef txndef = new TransactionDef();
                txndef.TxnCode = txncode;
                txndef.DataEntry = dataentry;
                txndef.PayrollItem = payrollItemId;
                txndef.DefaultAmount = amt;
                txndef.Enabled = enabled;
                txndef.Recurrent = recur;
                txndef.TrackYTD = track;
                db.AddToTransactionDefs(txndef);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<TransactionDef> TxnDefList()
        {
            try
            {

                return db.TransactionDefs.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void DeleteTxnDef(string txncode)
        {
            try
            {

                TransactionDef txn = db.TransactionDefs.Where(tx => tx.TxnCode == txncode).SingleOrDefault();
                if (txn != null)
                    db.TransactionDefs.DeleteObject(txn);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }
        public void UpdateTxnDef(string txncode, string dataentry, string payrollItemId, decimal amt,
            bool enabled, bool recur, bool track)
        {
            try
            {

                TransactionDef txndef = db.TransactionDefs.Where(tx => tx.TxnCode == txncode).SingleOrDefault();

                txndef.DataEntry = dataentry;
                txndef.PayrollItem = payrollItemId;
                txndef.DefaultAmount = amt;
                txndef.Enabled = enabled;
                txndef.Recurrent = recur;
                txndef.TrackYTD = track;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public TransactionDef GetTxnDef(string TxnCode)
        {
            try
            {

                return db.TransactionDefs.Where(d => d.TxnCode == TxnCode).Single();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Transaction Definitions"
        #region "Packed Transactions"
        public void CreatePackedTxn(DateTime packdate, string emp, string txncode, decimal amt, string user, bool authorized)
        {
            try
            {
                PackedTransaction txn = new PackedTransaction();
                txn.PackDate = packdate;
                txn.EmpNo = emp;
                txn.TxnCode = txncode;
                txn.Amount = amt;
                txn.CreatedBy = user;
                txn.Authorized = false;
                db.PackedTransactions.AddObject(txn);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void CreatePackedTxn(DateTime packdate, string empno, string txncode, decimal amount)
        {
            try
            {

                PackedTransaction txn = new PackedTransaction();
                txn.PackDate = packdate;
                txn.EmpNo = empno;
                txn.TxnCode = txncode;
                txn.Amount = amount;
                txn.Authorized = false;
                db.PackedTransactions.AddObject(txn);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void FlagAuthorize(int id, bool auth)
        {
            try
            {
                //this is the only field that can be changed

                db.PackedTransactions.Where(t => t.Id == id).Single().Authorized = auth;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void ClearPackedTransactions()
        {
            try
            {
                ///TODO check the linq recommended way of truncating a table
                db.ExecuteStoreCommand("TRUNCATE TABLE PackedTransactions");
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<PackedTransaction> GetPackedTxnList()
        {
            try
            {

                return db.PackedTransactions.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Packed Transactions"
        #region "Employee Transactions"
        public void AddDefaultEmpTransactions(int employeeid, string EmpNo, decimal BasicPay, string _User)
        {
            try
            {
                //Add PAYE, NSSF, NHIF
                CreateEmpTxn(employeeid, DateTime.Today,
                    EmpNo,
                    "PAYE",
                    0,
                    true,
                    true,
                    false,
                    true,
                    _User,
                    "System",
                    DateTime.Today,
                    "System",
                    DateTime.Today, true, 0, "");

                CreateEmpTxn(employeeid, DateTime.Today,
                    EmpNo,
                    "NSSF",
                    0,
                    true,
                    true,
                    false,
                    true,
                    _User,
                    "System",
                    DateTime.Today,
                    "System",
                    DateTime.Today, true, 0, "");
                CreateEmpTxn(employeeid, DateTime.Today,
                    EmpNo,
                    "NHIF",
                    0,
                    true,
                    true,
                    false,
                    true,
                    _User,
                    "System",
                    DateTime.Today,
                    "System",
                    DateTime.Today, true, 0, "");
                CreateEmpTxn(employeeid, DateTime.Today, EmpNo,
                    "BASIC",
                    BasicPay,
                    true,
                    true,
                    false,
                    false,
                    _User,
                    "System",
                    DateTime.Today,
                    "System",
                    DateTime.Today, true, 0, "");
                //add pension if flag = 1 or 3
                string pensionSchemeFlag = SettingLookup("DEFCONTRSCHEME");
                //if ("1".Equals(pensionSchemeFlag) || "3".Equals(pensionSchemeFlag))
                if (!string.IsNullOrEmpty(pensionSchemeFlag))
                {
                    CreateEmpTxn(employeeid, DateTime.Today, EmpNo,
                    "PENSION",
                    0,
                    true,
                    true,
                    false,
                    false,
                    _User,
                    "System",
                    DateTime.Today,
                    "System",
                    DateTime.Today, true, 0, "");
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void CreateOrEditEmpTxn(int employeeid, DateTime pdate,
            string empno, string payrollItemId, decimal amt, bool recur, bool enable,
            bool processed, bool track, decimal ytdAmt, string user, string modifiedby, DateTime modifieddate,
            string auth, DateTime authDate, bool show, string loantype)
        {
            try
            {
                //check if the record exists

                var et = from t in db.EmployeeTransactions
                         where t.EmpNo == empno
                         where t.ItemId == payrollItemId
                         select t;
                if (et.Count() == 0)
                {
                    CreateEmpTxn(employeeid, pdate,
                    empno, payrollItemId, amt, recur, enable,
                    processed, track, user, modifiedby, modifieddate,
                    auth, authDate, show, ytdAmt, loantype);
                }
                else
                {
                    EmployeeTransaction ett = et.First();
                    UpdateEmpTxn(ett.Id, pdate,
                 empno, payrollItemId, amt, recur, enable,
                  track, ytdAmt, user, modifiedby, modifieddate,
                 auth, authDate, loantype);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }
        public void CreateEmpTxn(int employeeid, DateTime pdate,
            string empno, string payrollItemId, decimal amt, bool recur, bool enable,
            bool processed, bool track, string user, string modifiedby, DateTime modifieddate,
            string auth, DateTime authDate, bool show, decimal ytdAmt, string loantype)
        {
            try
            {
                EmployeeTransaction et = new EmployeeTransaction();
                et.EmployeeId = employeeid;
                et.Amount = amt;
                et.PostDate = pdate;
                et.EmpNo = empno;
                et.ItemId = payrollItemId;
                et.Recurrent = recur;
                et.Processed = processed;
                et.Enabled = enable;
                et.TrackYTD = track;
                et.CreatedBy = user;
                et.LastChangeDate = modifieddate;
                et.LastChangedBy = modifiedby;
                et.AuthorizedBy = auth;
                et.AuthorizeDate = authDate;
                et.Balance = ytdAmt;
                et.ShowYTDInPayslip = show;

                db.EmployeeTransactions.AddObject(et);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void AddEmployeeAdvance(DateTime pdate,
            string empno, string payrollItemId, decimal amt, bool recur, bool enable,
            bool processed, bool track, string user, string modifiedby, DateTime modifieddate,
            string auth, DateTime authDate)
        {
            try
            {
                EmployeeTransaction et = new EmployeeTransaction();
                et.Amount = amt;
                et.PostDate = pdate;
                et.EmpNo = empno;
                et.ItemId = payrollItemId;
                et.Recurrent = recur;
                et.Processed = processed;
                et.Enabled = enable;
                et.TrackYTD = track;
                et.CreatedBy = user;
                et.LastChangeDate = modifieddate;
                et.LastChangedBy = modifiedby;
                et.AuthorizedBy = auth;
                et.AuthorizeDate = authDate;
                et.ShowYTDInPayslip = false;

                db.EmployeeTransactions.AddObject(et);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void AddEmployeeLoan(DateTime pdate,
           string empno, string payrollItemId, decimal amt, bool recur, bool enable,
           bool processed, bool track, string user, string modifiedby, DateTime modifieddate,
           string auth, DateTime authDate, decimal ytdAmt, string loantype)
        {
            try
            {
                EmployeeTransaction et = new EmployeeTransaction();

                et.Amount = amt;
                et.PostDate = pdate;
                et.EmpNo = empno;
                et.ItemId = payrollItemId;
                et.Recurrent = recur;
                et.Processed = processed;
                et.Enabled = enable;
                et.TrackYTD = track;
                et.CreatedBy = user;
                et.LastChangeDate = modifieddate;
                et.LastChangedBy = modifiedby;
                et.AuthorizedBy = auth;
                et.AuthorizeDate = authDate;
                et.Balance = ytdAmt;
                et.LoanType = loantype;
                et.ShowYTDInPayslip = true;

                db.EmployeeTransactions.AddObject(et);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateEmpTxnAmount(int id, decimal amt, decimal ytdAmt)
        {
            try
            {
                EmployeeTransaction et = db.EmployeeTransactions.Where(i => i.Id == id).Single();
                et.Amount = amt;
                et.Balance = ytdAmt;
                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateEmpTxn(int id, DateTime pdate,
            string empno, string payrollItemId, decimal amt, bool recur, bool enable,
             bool track, decimal ytdAmt, string user, string modifiedby, DateTime modifieddate,
            string auth, DateTime authDate, string loantype)
        {
            try
            {
                EmployeeTransaction et = db.EmployeeTransactions.Where(i => i.Id == id).Single();
                et.Amount = amt;
                et.PostDate = pdate;
                et.EmpNo = empno;
                et.ItemId = payrollItemId;
                et.Recurrent = recur;
                et.Enabled = enable;
                et.TrackYTD = track;
                et.Balance = ytdAmt;
                et.CreatedBy = user;
                et.LastChangeDate = modifieddate;
                et.LastChangedBy = modifiedby;
                et.AuthorizedBy = auth;
                et.AuthorizeDate = authDate;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateEmpTxn(EmployeeTransaction etxn)
        {
            try
            {
                EmployeeTransaction et = db.EmployeeTransactions.Where(i => i.Id == etxn.Id).Single();
                et.Amount = etxn.Amount;
                et.PostDate = etxn.PostDate;
                et.EmpNo = etxn.EmpNo;
                et.ItemId = etxn.ItemId;
                et.Recurrent = etxn.Recurrent;
                et.Enabled = etxn.Enabled;
                et.TrackYTD = etxn.TrackYTD;
                et.Balance = etxn.Balance;
                et.CreatedBy = etxn.CreatedBy;
                et.LastChangeDate = etxn.LastChangeDate;
                et.LastChangedBy = etxn.LastChangedBy;
                et.AuthorizedBy = etxn.AuthorizedBy;
                et.AuthorizeDate = etxn.AuthorizeDate;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateEmpTxn(int id, DateTime pdate, string empno, string payrollItemId, decimal amt)
        {
            try
            {
                EmployeeTransaction et = db.EmployeeTransactions.Where(i => i.Id == id).Single();
                et.PostDate = pdate;
                et.EmpNo = empno;
                et.ItemId = payrollItemId;
                et.Amount = amt;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateEmpTxnBasicPay(int employeeid, DateTime pdate, string empno, string payrollItemId, decimal amt)
        {
            try
            {
                EmployeeTransaction et = db.EmployeeTransactions.Where(i => i.EmployeeId == employeeid && i.ItemId == payrollItemId).Single();
                et.EmployeeId = employeeid;
                et.PostDate = pdate;
                et.EmpNo = empno;
                et.ItemId = payrollItemId;
                et.Amount = amt;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }

        public List<PayslipMaster> GetEmpTaxRecord(string empNo, int Year)
        {
            try
            {

                var tr = from t in db.PayslipMasters
                         where t.EmpNo == empNo
                         where t.Year == Year
                         select t;
                return tr.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }

        public void DeleteEmpTxn(int id)
        {
            try
            {
                EmployeeTransaction et = db.EmployeeTransactions.Where(i => i.Id == id).Single();
                db.EmployeeTransactions.DeleteObject(et);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteNonCashBenefitsEmpTxn(EmployeeTransaction emptn)
        {
            try
            {
                EmployeeTransaction et = db.EmployeeTransactions.Where(i => i.Id == emptn.Id && i.ItemId == "NON_CASH_BENEFIT").Single();

                db.EmployeeTransactions.DeleteObject(et);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<EmployeeTransaction> EmpTxnList(string empno)
        {
            try
            {
                return db.EmployeeTransactions.Where(e => e.EmpNo == empno).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EmployeeTransaction> ActiveEmpTxnList(string empno)
        {
            try
            {
                return db.EmployeeTransactions.Where(e => e.EmpNo == empno && e.Enabled == true).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public EmployeeTransaction GetEmployeeTxn(int Id)
        {
            try
            {
                return db.EmployeeTransactions.Where(e => e.Id == Id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EmployeeTransaction> GetEmployeeTxn(string empno, string itemid)
        {
            try
            {
                var em = db.EmployeeTransactions.Where(e => e.EmpNo == empno && e.ItemId == itemid).ToList();
                return em;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion  "Employee Transactions"
        #region "Payroll"
        public List<Payroll> GetOpenPayrolls(bool openFlag)
        {
            try
            {

                return db.Payrolls.Where(p => p.IsOpen == openFlag).OrderBy(i => i.Year).ThenBy(i => i.Period).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Payroll> GetProcessedPayrolls(bool processedFlag)
        {
            try
            {

                return db.Payrolls.Where(p => p.Processed == processedFlag).OrderBy(i => i.Year).ThenBy(i => i.Period).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Payroll> GetPayrolls(bool openFlag, bool processedFlag)
        {
            try
            {

                return db.Payrolls.Where(p => p.IsOpen == openFlag).Where(p => p.Processed == processedFlag).OrderBy(i => i.Year).ThenBy(i => i.Period).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<Payroll> GetAllPayrolls()
        {
            try
            {

                return db.Payrolls.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<int> GetPayrollYears()
        {
            try
            {

                var py = (from p in db.Payrolls
                          select p.Year).Distinct();
                return py.OrderBy(p => p).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Payroll"
        #region "Payslipdet"
        public void ClearPayslipDet(int payPeriod, int Year)
        {
            try
            {


                db.ExecuteStoreCommand("DELETE FROM PayslipDet WHERE Period = {0} AND Year = {1}", payPeriod, Year);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void ClearPayslipDet_Temp()
        {
            try
            {
                db.ExecuteStoreCommand("TRUNCATE TABLE PayslipDet_Temp");
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void ClearPayslipMaster_Temp()
        {
            try
            {
                db.ExecuteStoreCommand("TRUNCATE TABLE PayslipMaster_Temp");
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        #endregion "Payslipdet"
        #region "vwPayslipDet"
        public List<vwPayslipDet> GetvwPayslipDet()
        {
            try
            {

                return db.vwPayslipDets.OrderBy(i => i.TxnDate).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<StatementDTO> GetStatementDTOfromvwPayslipDet(string emp, string item) //for statements
        {
            try
            {

                return db.vwPayslipDets.Where(e => e.EmpNo == emp &&
                    e.ItemId.ToLower() == item.ToLower())
                    .OrderBy(i => i.TxnDate)
                    .Select(i => new StatementDTO
                        {
                            date = i.TxnDate ?? DateTime.Today,
                            Amountin = i.Amount > 0 ? i.Amount : 0, //cr
                            Amountout = i.Amount > 0 ? 0 : i.Amount, //dr
                            Description = i.Description,
                            Balance = i.YTD ?? 0
                        }).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayslipDetails> GetEmployeeStatementinvwPayslipDet(bool current, string empNo, string item)
        {
            if (current)
            {
                return GetEmployeeStatementinvwPayslipDet_Temp(current, empNo, item);
            }
            else
            {
                return GetEmployeeStatementinvwPayslipDet_Hist(current, empNo, item);
            }
        }
        public List<psuedovwPayslipDetails> GetEmployeeStatementinvwPayslipDet_Hist(bool current, string empNo, string item)
        {
            try
            {

                var pbnm = db.vwPayslipDets
                         .Where(e => e.EmpNo == empNo && e.ItemId.ToLower() == item.ToLower()).ToList();


                var pm = db.vwPayslipDets
                         .Where(e => e.EmpNo == empNo && e.ItemId.ToLower() == item.ToLower())
                         .Select(v => new psuedovwPayslipDetails
                         {

                             EmpNo = v.EmpNo,
                             Surname = v.Surname,
                             OtherNames = v.OtherNames,
                             BankAccount = v.BankAccount,
                             Period = v.Period,
                             Year = v.Year ?? 0,
                             Amount = v.Amount,
                             Balance = v.Balance ?? 0,
                             BankCode = v.BankCode,
                             Description = v.Description,
                             DEType = v.DEType,
                             Employer = v.EmployerId,
                             EmpTxnId = v.EmpTxnId,
                             InitialAmount = v.InitialAmount ?? 0,
                             IsStatutory = v.IsStatutory,
                             ItemId = v.ItemId,
                             ItemType = v.ItemTypeId,
                             LoanType = v.LoanType,
                             Parent = v.Parent,
                             PaymentMode = v.PaymentMode,
                             PostDate = v.PostDate,
                             ReFField = v.ReFField ?? 0,
                             RepayAmount = v.RepayAmount,
                             TxnDate = v.TxnDate ?? DateTime.Today,
                             YTD = v.YTD ?? 0
                         }).OrderBy(i => i.ItemId).ToList();

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayslipDetails> GetEmployeeStatementinvwPayslipDet_Temp(bool current, string empNo, string item)
        {
            try
            {

                var pm = db.vwPayslipDet_Temp
                         .Where(e => e.EmpNo == empNo && e.ItemId.ToLower() == item.ToLower())
                         .Select(v => new psuedovwPayslipDetails
                         {

                             EmpNo = v.EmpNo,
                             Surname = v.Surname,
                             OtherNames = v.OtherNames,
                             BankAccount = v.BankAccount,
                             Period = v.Period,
                             Year = v.Year ?? 0,
                             Amount = v.Amount,
                             Balance = v.Balance ?? 0,
                             BankCode = v.BankCode,
                             Description = v.Description,
                             DEType = v.DEType,
                             Employer = v.EmployerId,
                             EmpTxnId = v.EmpTxnId,
                             InitialAmount = v.InitialAmount ?? 0,
                             IsStatutory = v.IsStatutory,
                             ItemId = v.ItemId,
                             ItemType = v.ItemTypeId,
                             LoanType = v.LoanType,
                             Parent = v.Parent,
                             PaymentMode = v.PaymentMode,
                             PostDate = v.PostDate,
                             ReFField = v.ReFField ?? 0,
                             RepayAmount = v.RepayAmount,
                             TaxTracking = v.TaxTracking,
                             TxnDate = v.TxnDate ?? DateTime.Today,
                             YTD = v.YTD ?? 0
                         }).OrderBy(i => i.ItemId).ToList();

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwPayslipDet> GetvwPayslipDet(string item, int period, int year) //for schedules
        {
            try
            {

                return db.vwPayslipDets.Where(e => e.ItemId.ToLower() == item.ToLower() &&
                    e.Period == period && e.Year == year).OrderBy(i => i.TxnDate).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwPayslipDet_Temp> GetvwPayslipDet_Temp(string item, int period, int year) //for schedules
        {
            try
            {

                return db.vwPayslipDet_Temp.Where(e => e.ItemId.ToLower() == item.ToLower() &&
                    e.Period == period && e.Year == year).OrderBy(i => i.TxnDate).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<ScheduleDTO> GetScheduleDTOfromvwPayslipDet_Temp(string item, int period, int year) //for schedules
        {
            try
            {

                return db.vwPayslipDet_Temp.Where(e => e.ItemId.ToLower() == item.ToLower() &&
                    e.Period == period && e.Year == year)
                    .OrderBy(i => i.TxnDate)
                    .Select(v => new ScheduleDTO
                         {
                             EmpNo = v.EmpNo,
                             EmpName = v.Surname + ",  " + v.OtherNames,
                             Amount = v.Amount
                         })
                         .ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayslipDetails> GetScheduleinvwPayslipDet(bool current, string item, int period, int year)
        {
            if (current)
            {
                return GetScheduleinvwPayslipDet_Temp(current, item, period, year);
            }
            else
            {
                return GetScheduleinvwPayslipDet_Hist(current, item, period, year);
            }
        }
        public List<psuedovwPayslipDetails> GetScheduleinvwPayslipDet_Hist(bool current, string item, int period, int year)
        {
            try
            {

                var pm = db.vwPayslipDets.Where(e => e.ItemId.ToLower() == item.ToLower() && e.Period == period && e.Year == year).Select(v => new psuedovwPayslipDetails
                         {
                             EmpNo = v.EmpNo,
                             Surname = v.Surname,
                             OtherNames = v.OtherNames,
                             BankAccount = v.BankAccount,
                             Period = v.Period,
                             Year = v.Year ?? 0,
                             Amount = v.Amount,
                             Balance = v.Balance ?? 0,
                             BankCode = v.BankCode,
                             Description = v.Description,
                             DEType = v.DEType,
                             Employer = v.EmployerId,
                             EmpTxnId = v.EmpTxnId,
                             InitialAmount = v.InitialAmount ?? 0,
                             IsStatutory = v.IsStatutory,
                             ItemId = v.ItemId,
                             ItemType = v.ItemTypeId,
                             LoanType = v.LoanType,
                             Parent = v.Parent,
                             PaymentMode = v.PaymentMode,
                             PostDate = v.PostDate,
                             ReFField = v.ReFField ?? 0,
                             RepayAmount = v.RepayAmount,
                             TxnDate = v.TxnDate ?? DateTime.Today,
                             YTD = v.YTD ?? 0
                         }).OrderBy(i => i.ItemId).ToList();

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<psuedovwPayslipDetails> GetScheduleinvwPayslipDet_Temp(bool current, string item, int period, int year)
        {
            try
            {

                var pm = db.vwPayslipDet_Temp.Where(e => e.ItemId.ToLower() == item.ToLower() &&
                    e.Period == period && e.Year == year).Select(v => new psuedovwPayslipDetails
                         {
                             EmpNo = v.EmpNo,
                             Surname = v.Surname,
                             OtherNames = v.OtherNames,
                             BankAccount = v.BankAccount,
                             Period = v.Period,
                             Year = v.Year ?? 0,
                             Amount = v.Amount,
                             Balance = v.Balance ?? 0,
                             BankCode = v.BankCode,
                             Description = v.Description,
                             DEType = v.DEType,
                             Employer = v.EmployerId,
                             EmpTxnId = v.EmpTxnId,
                             InitialAmount = v.InitialAmount ?? 0,
                             IsStatutory = v.IsStatutory,
                             ItemId = v.ItemId,
                             ItemType = v.ItemTypeId,
                             LoanType = v.LoanType,
                             Parent = v.Parent,
                             PaymentMode = v.PaymentMode,
                             PostDate = v.PostDate,
                             ReFField = v.ReFField ?? 0,
                             RepayAmount = v.RepayAmount,
                             TaxTracking = v.TaxTracking,
                             TxnDate = v.TxnDate ?? DateTime.Today,
                             YTD = v.YTD ?? 0
                         }).OrderBy(i => i.ItemId).ToList();

                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwPayslipDet> GetvwPayslipDetloan(int period, int year) //for loan schedule
        {
            try
            {

                return db.vwPayslipDets.Where(e => e.ItemTypeId.Trim() == "LOAN" && e.Period == period &&
                e.Year == year).OrderBy(i => i.Period).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<vwPayslipDet> GetvwPayslipDetsacco(int period, int year) //for sacco schedule
        {
            try
            {

                return db.vwPayslipDets.Where(e => e.ItemTypeId.Trim() == "SACCO" && e.Period == period &&
                e.Year == year).OrderBy(i => i.Period).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "vwPayslipDet"
        #region "PayslipMaster"
        public void ClearPayslipMaster(int payPeriod, int Year)
        {
            try
            {

                db.ExecuteStoreCommand("DELETE FROM PayslipMaster WHERE Period = {0} AND Year = {1}", payPeriod, Year);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public PayslipMaster GetPayslipMaster(int period, int Year, string EmpNo)
        {
            try
            {

                var tr = from t in db.PayslipMasters
                         where t.EmpNo == EmpNo
                         where t.Period == period
                         where t.Year == Year
                         select t;
                return tr.Single();
            }

            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "PayslipMaster"
        #region "P9A(HOSP)"
        public List<psuedovwPayrollMaster> GetvwEmployeeTaxRecord(bool current, int EmployeeId, string empNo, int Year)
        {
            if (current)
            {
                return GetvwEmployeeTaxRecord_Temp(EmployeeId, empNo, Year);
            }
            else
            {
                return GetvwEmployeeTaxRecord_Hist(EmployeeId, empNo, Year);
            }
        }
        public List<psuedovwPayrollMaster> GetvwEmployeeTaxRecord_Hist(int EmployeeId, string empNo, int Year)
        {
            try
            {

                var pm = db.vwPayrollMasters.Where(v => v.EmployeeId == EmployeeId && v.Year == Year)
                    .Select(v => new psuedovwPayrollMaster
                    {
                        BankName = v.BankName,
                        BranchName = v.BranchName,
                        EmpNo = v.EmpNo,
                        EmployeeId = v.EmployeeId,
                        Surname = v.Surname,
                        OtherDeductions = v.OtherDeductions,
                        OtherNames = v.OtherNames,
                        BankAccount = v.BankAccount,
                        NetPay = v.NetPay,
                        Department = v.Department,
                        GrossTaxableEarnings = v.GrossTaxableEarnings,
                        BankCode = v.BankCode,
                        BasicPay = v.BasicPay,
                        NHIF = v.NHIF,
                        NSSF = v.NSSF,
                        EmployerNSSF = v.EmployerNSSF,
                        PayeTax = v.PayeTax,
                        PensionEmployee = v.PensionEmployee,
                        Period = v.Period,
                        Year = v.Year,
                        IDNo = v.IDNo,
                        PINNo = v.PIN,
                        GrossTax = v.GrossTax,
                        NHIFNo = v.NHIFNo,
                        NSSFNo = v.NSSFNo,
                        Benefits = v.Benefits,
                        Variables = v.Variables,
                        NetTaxableEarnings = v.NetTaxableEarnings,
                        MortgageRelief = v.MortgageRelief,
                        PersonalRelief = v.PersonalRelief
                    }).OrderBy(i => i.BankCode).ToList();
                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }

        }
        public List<psuedovwPayrollMaster> GetvwEmployeeTaxRecord_Temp(int EmployeeId, string empNo, int Year)
        {
            try
            {

                var pm = db.vwPayrollMaster_Temp.Where(v => v.EmployeeId == EmployeeId && v.Year == Year)
                    .Select(v => new psuedovwPayrollMaster
                    {
                        BankName = v.BankName,
                        BranchName = v.BranchName,
                        EmpNo = v.EmpNo,
                        EmployeeId = v.EmployeeId,
                        Surname = v.Surname,
                        OtherDeductions = v.OtherDeductions,
                        OtherNames = v.OtherNames,
                        BankAccount = v.BankAccount,
                        NetPay = v.NetPay,
                        Department = v.Department,
                        GrossTaxableEarnings = v.GrossTaxableEarnings,
                        BankCode = v.BankCode,
                        BasicPay = v.BasicPay,
                        NHIF = v.NHIF,
                        NSSF = v.NSSF,
                        EmployerNSSF = v.EmployerNSSF,
                        PayeTax = v.PayeTax,
                        PensionEmployee = v.PensionEmployee,
                        GrossTax = v.GrossTax,
                        Period = v.Period,
                        Year = v.Year,
                        IDNo = v.IDNo,
                        PINNo = v.PIN,
                        NHIFNo = v.NHIFNo,
                        NSSFNo = v.NSSFNo,
                        Benefits = v.Benefits,
                        Variables = v.Variables,
                        NetTaxableEarnings = v.NetTaxableEarnings,
                        MortgageRelief = v.MortgageRelief,
                        PersonalRelief = v.PersonalRelief
                    }).OrderBy(i => i.BankCode).ToList();
                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }

        public List<psuedovwPayrollMaster> GetEmployeeTaxRecord(bool current, string empNo, int Year)
        {
            if (current)
            {
                return GetEmployeeTaxRecord_Temp(current, empNo, Year);
            }
            else
            {
                return GetEmployeeTaxRecordr_Hist(current, empNo, Year);
            }
        }

        public List<psuedovwPayrollMaster> GetEmployeeTaxRecordr_Hist(bool current, string empNo, int Year)
        {
            try
            {

                var pm = db.vwPayrollMasters.Where(v => v.EmpNo == empNo && v.Year == Year)
                    .Select(v => new psuedovwPayrollMaster
                    {
                        BankName = v.BankName,
                        BranchName = v.BranchName,
                        EmpNo = v.EmpNo,
                        Surname = v.Surname,
                        OtherDeductions = v.OtherDeductions,
                        OtherNames = v.OtherNames,
                        BankAccount = v.BankAccount,
                        NetPay = v.NetPay,
                        Department = v.Department,
                        GrossTaxableEarnings = v.GrossTaxableEarnings,
                        BankCode = v.BankCode,
                        BasicPay = v.BasicPay,
                        NHIF = v.NHIF,
                        NSSF = v.NSSF,
                        EmployerNSSF = v.EmployerNSSF,
                        PayeTax = v.PayeTax,
                        PensionEmployee = v.PensionEmployee,
                        Period = v.Period,
                        Year = v.Year,
                        IDNo = v.IDNo,
                        PINNo = v.PIN,
                        GrossTax = v.GrossTax,
                        NHIFNo = v.NHIFNo,
                        NSSFNo = v.NSSFNo,
                        Benefits = v.Benefits,
                        Variables = v.Variables,
                        NetTaxableEarnings = v.NetTaxableEarnings,
                        MortgageRelief = v.MortgageRelief,
                        PersonalRelief = v.PersonalRelief
                    }).OrderBy(i => i.BankCode).ToList();
                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }

        }

        public List<psuedovwPayrollMaster> GetEmployeeTaxRecord_Temp(bool current, string empNo, int Year)
        {
            try
            {

                var pm = db.vwPayrollMaster_Temp.Where(v => v.EmpNo == empNo && v.Year == Year)
                    .Select(v => new psuedovwPayrollMaster
                    {
                        BankName = v.BankName,
                        BranchName = v.BranchName,
                        EmpNo = v.EmpNo,
                        Surname = v.Surname,
                        OtherDeductions = v.OtherDeductions,
                        OtherNames = v.OtherNames,
                        BankAccount = v.BankAccount,
                        NetPay = v.NetPay,
                        Department = v.Department,
                        GrossTaxableEarnings = v.GrossTaxableEarnings,
                        BankCode = v.BankCode,
                        BasicPay = v.BasicPay,
                        NHIF = v.NHIF,
                        NSSF = v.NSSF,
                        EmployerNSSF = v.EmployerNSSF,
                        PayeTax = v.PayeTax,
                        PensionEmployee = v.PensionEmployee,
                        GrossTax = v.GrossTax,
                        Period = v.Period,
                        Year = v.Year,
                        IDNo = v.IDNo,
                        PINNo = v.PIN,
                        NHIFNo = v.NHIFNo,
                        NSSFNo = v.NSSFNo,
                        Benefits = v.Benefits,
                        Variables = v.Variables,
                        NetTaxableEarnings = v.NetTaxableEarnings,
                        MortgageRelief = v.MortgageRelief,
                        PersonalRelief = v.PersonalRelief
                    }).OrderBy(i => i.BankCode).ToList();
                return pm.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }

        }

        public List<EmployeeMonthlyTaxRecord> GetEmployeeMonthlyTaxRecord(bool current, string empNo, int Year)
        {
            try
            {

                if (current)
                {
                    return GetEmployeeMonthlyTaxRecord_Temp(empNo, Year);
                }
                else
                {
                    return GetEmployeeMonthlyTaxRecord_Hist(empNo, Year);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EmployeeMonthlyTaxRecord> GetEmployeeMonthlyTaxRecord_Temp(string empNo, int Year)
        {
            try
            {

                var _taxrecordsquery = from pm in db.PayslipMaster_Temp
                                       where pm.EmpNo == empNo
                                       where pm.Year == Year
                                       select new EmployeeMonthlyTaxRecord
                                       {
                                           MonthInt = pm.Period, // all periods = month 
                                           A = pm.BasicPay,
                                           B = pm.Benefits,
                                           C = pm.Variables,
                                           E2 = pm.PensionEmployee,
                                           H = pm.NetTaxableEarnings,
                                           J = pm.PersonalRelief,
                                           J1 = pm.MortgageRelief
                                       };
                return _taxrecordsquery.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EmployeeMonthlyTaxRecord> GetEmployeeMonthlyTaxRecord_Hist(string empNo, int Year)
        {
            try
            {

                var _taxrecordsquery = from pm in db.PayslipMasters
                                       where pm.EmpNo == empNo
                                       where pm.Year == Year
                                       select new EmployeeMonthlyTaxRecord
                                       {
                                           MonthInt = pm.Period, // all periods = month 
                                           A = pm.BasicPay,
                                           B = pm.Benefits,
                                           C = pm.Variables,
                                           E2 = pm.PensionEmployee,
                                           H = pm.NetTaxableEarnings,
                                           J = pm.PersonalRelief,
                                           J1 = pm.MortgageRelief
                                       };
                return _taxrecordsquery.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EmployersMonthlyTaxRecord> GetEmployerTaxRecord(bool current, int EmployeeId, string empNo, int Year)
        {
            try
            {

                if (current)
                {
                    return GetEmployerTaxRecord_Temp(EmployeeId, empNo, Year);
                }
                else
                {
                    return GetEmployerTaxRecord_Hist(EmployeeId, empNo, Year);
                }
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EmployersMonthlyTaxRecord> GetEmployerTaxRecord_Temp(int EmployeeId, string empNo, int Year)
        {
            try
            {

                var _taxrecordsquery = from pm in db.PayslipMaster_Temp
                                       where pm.EmployeeId == EmployeeId
                                       where pm.Year == Year
                                       select new EmployersMonthlyTaxRecord
                                       {
                                           MonthInt = pm.Period, // all periods = month 
                                           A = pm.BasicPay,
                                           B = pm.Benefits,
                                           C = pm.Variables,
                                           E2 = pm.PensionEmployee,
                                           H = pm.NetTaxableEarnings,
                                           J = pm.PayeTax,
                                           K = pm.PersonalRelief,
                                           K1 = pm.MortgageRelief
                                       };
                return _taxrecordsquery.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public List<EmployersMonthlyTaxRecord> GetEmployerTaxRecord_Hist(int EmployeeId, string empNo, int Year)
        {
            try
            {

                var _taxrecordsquery = from pm in db.PayslipMasters
                                       where pm.EmployeeId == EmployeeId
                                       where pm.Year == Year
                                       select new EmployersMonthlyTaxRecord
                                       {
                                           MonthInt = pm.Period, // all periods = month 
                                           A = pm.BasicPay,
                                           B = pm.Benefits,
                                           C = pm.Variables,
                                           E2 = pm.PensionEmployee,
                                           H = pm.NetTaxableEarnings,
                                           J = pm.PayeTax,
                                           K = pm.PersonalRelief,
                                           K1 = pm.MortgageRelief
                                       };
                return _taxrecordsquery.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public decimal GetEmployeeTotalEmoluments(bool current, int EmployeeId, string empNo, int Year)
        {
            if (current)
            {
                return TotalEmoluments_Temp(current, EmployeeId, empNo, Year);
            }
            else
            {
                return TotalEmoluments_Hist(current, EmployeeId, empNo, Year);
            }
        }
        public decimal TotalEmoluments_Temp(bool current, int EmployeeId, string empNo, int Year)
        {
            try
            {

                var tr = from t in db.vwPayrollMaster_Temp
                         where t.EmployeeId == EmployeeId
                         where t.Year == Year
                         select t.BasicPay + t.Benefits + t.Variables;
                if (tr.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return tr.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal TotalEmoluments_Hist(bool current, int EmployeeId, string empNo, int Year)
        {
            try
            {

                var tr = from t in db.vwPayrollMasters
                         where t.EmployeeId == EmployeeId
                         where t.Year == Year
                         select t.BasicPay + t.Benefits + t.Variables;
                if (tr.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return tr.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal TotalEmoluments(int EmployeeId, string empNo, int Year)
        {
            try
            {

                var tr = from t in db.PayslipMasters
                         where t.EmployeeId == EmployeeId
                         where t.Year == Year
                         select t.BasicPay + t.Benefits + t.Variables;
                if (tr.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return tr.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal GetEmployeeTotalTaxDeducted(bool current, int EmployeeId, string empNo, int Year)
        {
            if (current)
            {
                return TotalTaxDeducted_Temp(current, EmployeeId, empNo, Year);
            }
            else
            {
                return TotalTaxDeducted_Hist(current, EmployeeId, empNo, Year);
            }
        }
        public decimal TotalTaxDeducted_Temp(bool current, int EmployeeId, string empNo, int Year)
        {
            try
            {

                var tr = from t in db.vwPayrollMaster_Temp
                         where t.EmployeeId == EmployeeId
                         where t.Year == Year
                         select t.PayeTax;

                if (tr.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return tr.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        public decimal TotalTaxDeducted_Hist(bool current, int EmployeeId, string empNo, int Year)
        {
            try
            {

                var tr = from t in db.vwPayrollMasters
                         where t.EmployeeId == EmployeeId
                         where t.Year == Year
                         select t.PayeTax;

                if (tr.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return tr.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        //public decimal TotalTaxDeducted(string empNo, int Year)
        //{
        //    try
        //    {
        //        
        //        var tr = from t in db.PayslipMasters
        //                 where t.EmpNo == empNo
        //                 where t.Year == Year
        //                 select t.PayeTax;

        //        if (tr.Count() == 0)
        //        {
        //            return 0.0M;
        //        }
        //        else
        //            return tr.Sum();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.WriteToErrorLogFile(ex);
        //        return 0;
        //    }
        //}
        public decimal SavingsPlan(int EmployeeId, string empNo)
        {
            try
            {

                var savingsPlan = (from e in db.GetEmpTransactions
                                   where e.EmployeeId == EmployeeId
                                   where e.TaxTrackingId.Trim() == "SPLAN" //Savings plan
                                   where e.AddToPension == true
                                   where e.Enabled == true
                                   select e.Amount);

                if (savingsPlan.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return savingsPlan.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }

        }
        #endregion  "P9A(HOSP)"
        #region "Employee Autocomplete"
        public string[] AutoComplete_PayPoints()
        {
            try
            {

                var pp = (from e in this.GetAllActiveEmployees()
                          select e.PayPoint).Distinct().OrderBy(name => name);
                return pp.ToArray();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string[] AutoComplete_EmpGroups()
        {
            try
            {

                var pp = (from e in this.GetAllActiveEmployees()
                          select e.EmpGroup).Distinct().OrderBy(name => name);
                return pp.ToArray();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public string[] AutoComplete_EmpPayRolls()
        {
            try
            {

                var pp = (from e in this.GetAllActiveEmployees()
                          select e.EmpPayroll).Distinct().OrderBy(name => name);
                return pp.ToArray();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "Employee Autocomplete"
        #region "Totals Required for P10A"
        public decimal TotalPAYE(string empNo, int period, int year)
        {
            try
            {


                var PAYE = (from e in db.PayslipMasters
                            where e.EmpNo == empNo
                            where e.Period == period
                            where e.Year == year
                            select e.PayeTax);

                if (PAYE.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return PAYE.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }

        }
        public decimal TotalEmolument(string empNo, int period, int year)
        {
            try
            {

                var Emolument = (from e in db.PayslipMasters
                                 where e.EmpNo == empNo
                                 where e.Period == period
                                 where e.Year == year
                                 select e.GrossTax);

                if (Emolument.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return Emolument.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }

        }
        #endregion "Totals Required for P10A"
        #region "PayeeRates"
        public List<PayeeRate> GetPayeerates()
        {
            try
            {

                return db.PayeeRates.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public void AddPayeeRate(decimal fromamount, decimal toamount, decimal rate)
        {
            try
            {

                PayeeRate payeerate = new PayeeRate();
                payeerate.FromAmt = fromamount;
                payeerate.ToAmt = toamount;
                payeerate.Rate = rate;

                db.AddToPayeeRates(payeerate);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<PayeeRate> PayeeRatesTable()
        {
            try
            {

                var rt = from n in db.PayeeRates
                         select n;

                return rt.ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }

        #endregion "PayeeRates"
        #region "HourlyPayment"
        public void SaveHourlyPayment(HourlyPayment hour)
        {
            try
            {
                db.AddToHourlyPayments(hour);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateHourlyPayment(HourlyPayment hour)
        {
            try
            {
                HourlyPayment hpt = db.HourlyPayments.First(h => h.Empno == hour.Empno && h.WorkDate == hour.WorkDate);

                hpt.WorkDate = hour.WorkDate;
                hpt.WorkHours = hour.WorkHours;
                hpt.RatePerHour = hour.RatePerHour;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);

            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }
        public void DeleteHourlyPayment(HourlyPayment hour)
        {
            try
            {
                HourlyPayment hpt = db.HourlyPayments.Where(h => h.Empno == hour.Empno).Where(h => h.WorkDate == hour.WorkDate).FirstOrDefault();

                if (hpt != null)
                    db.HourlyPayments.DeleteObject(hpt);

                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }

        }
        public List<HourlyPayment> GetHourlyPaymentsList(string empno)
        {
            try
            {
                return db.HourlyPayments.Where(e => e.Empno == empno).OrderBy(e => e.WorkDate).ToList();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public decimal GetHourlyPay(int EmployeeId)
        {
            try
            {
                var benefits = from emptxn in db.EmployeeTransactions
                               join pi in db.PayrollItems on emptxn.ItemId equals pi.Id
                               join pit in db.PayrollItemTypes on pi.ItemTypeId equals pit.Id
                               join emp in db.Employees on emptxn.EmpNo equals emp.EmpNo
                               join dp in db.Departments on emp.DepartmentId equals dp.Id
                               //where emptxn.EmpNo == EmpNo
                               where emptxn.EmployeeId == EmployeeId
                               where pi.ItemTypeId.Trim() == "HOURLY_PAY"
                               where pi.TaxTrackingId.Trim() == "EARNING"
                               where pi.Enable == true
                               select emptxn.Amount;

                //var benefits = (from d in db.GetEmpTransactions
                //                where d.EmpNo == EmpNo
                //                where d.ItemTypeId.Trim() == "HOURLY_PAY"
                //                where d.TaxTrackingId.Trim() == "EARNING"
                //                where d.Enabled == true
                //                select d.Amount);

                if (benefits.Count() == 0)
                {
                    return 0.0M;
                }
                else
                    return benefits.Sum();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0M;
            }
        }
        public decimal GetHourlyPay(int EmployeeId, DateTime startdate, DateTime enddate)
        {
            try
            {
                decimal pay = 0;
                var hrlypaylist = db.HourlyPayments.Where(e => e.EmployeeId == EmployeeId).Where(e => e.WorkDate >= startdate).Where(e => e.WorkDate <= enddate);
                if (hrlypaylist.Count() > 0)
                {
                    pay = hrlypaylist.Sum(s => s.WorkHours * s.RatePerHour);
                }
                return pay;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return 0;
            }
        }
        #endregion "HourlyPayment"
        #region "EmployerBanks"
        public void AddEmployerBank(EmployerBanksModel _employerbank)
        {
            try
            {
                EmployerBank employerbank = new EmployerBank();
                employerbank.EmployerId = _employerbank.EmployerId;
                employerbank.BankSortCode = _employerbank.BankSortCode;
                employerbank.AccountName = _employerbank.AccountName;
                employerbank.AccountNo = _employerbank.AccountNo;
                employerbank.Signatory = _employerbank.Signatory;
                employerbank.IsDefault = _employerbank.IsDefault;

                db.EmployerBanks.AddObject(employerbank);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateEmployerBank(EmployerBanksModel _employerbank)
        {
            try
            {
                EmployerBank employerbank = db.EmployerBanks.Where(p => p.Id == _employerbank.Id).Single();
                employerbank.EmployerId = _employerbank.EmployerId;
                employerbank.BankSortCode = _employerbank.BankSortCode;
                employerbank.AccountName = _employerbank.AccountName;
                employerbank.AccountNo = _employerbank.AccountNo;
                employerbank.Signatory = _employerbank.Signatory;
                employerbank.IsDefault = _employerbank.IsDefault;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteEmployerBank(EmployerBanksModel _employerbank)
        {
            try
            {
                EmployerBank bank = db.EmployerBanks.Where(p => p.Id == _employerbank.Id).Single();

                db.EmployerBanks.DeleteObject(bank);
                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<EmployerBanksModel> GetAllEmployerBanks(int employerid)
        {
            try
            {
                List<EmployerBanksModel> _employerbanks = new List<EmployerBanksModel>();
                var _employerbanksquery = from br in db.EmployerBanks
                                          where br.EmployerId == employerid
                                          select br;
                List<EmployerBank> _mplyrbnk = _employerbanksquery.ToList();
                foreach (var _employerbank in _mplyrbnk)
                {
                    EmployerBanksModel employerbank = new EmployerBanksModel();
                    employerbank.Id = _employerbank.Id;
                    employerbank.EmployerId = _employerbank.EmployerId;
                    employerbank.BankSortCode = _employerbank.BankSortCode;
                    employerbank.AccountName = _employerbank.AccountName;
                    employerbank.AccountNo = _employerbank.AccountNo;
                    employerbank.Signatory = _employerbank.Signatory;
                    employerbank.IsDefault = _employerbank.IsDefault;
                    employerbank.BankBranchName = this.GetBankBranchName(employerbank.BankSortCode);

                    _employerbanks.Add(employerbank);
                }

                return _employerbanks;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public EmployerBanksModel GetEmployerBank(int id)
        {
            try
            {
                var _employerbankquery = (from eb in db.EmployerBanks
                                          where eb.Id == id
                                          select new EmployerBanksModel
                                          {
                                              Id = eb.Id,
                                              AccountName = eb.AccountName,
                                              AccountNo = eb.AccountNo,
                                              BankSortCode = eb.BankSortCode,
                                              EmployerId = eb.EmployerId,
                                              Signatory = eb.Signatory,
                                              IsDefault = eb.IsDefault
                                          }).FirstOrDefault();
                EmployerBanksModel _empbank = _employerbankquery;
                return _empbank;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "EmployerBanks"
        #region "EmployeesBankTransfers"
        public void AddEmployeesBankTransfer(EmployeesBankTransfersModel _employerbank)
        {
            try
            {
                EmployeesBankTransfer employerbank = new EmployeesBankTransfer();
                employerbank.EmpNo = _employerbank.EmpNo;
                employerbank.BankSortCode = _employerbank.BankSortCode;
                employerbank.EmployerBankId = _employerbank.EmployerBankId;
                employerbank.EmployerId = _employerbank.EmployerId;
                employerbank.EmployeeId = _employerbank.EmployeeId;

                db.EmployeesBankTransfers.AddObject(employerbank);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void UpdateEmployeesBankTransfer(EmployeesBankTransfersModel _employerbank)
        {
            try
            {
                EmployeesBankTransfer employerbank = db.EmployeesBankTransfers.Where(p => p.Id == _employerbank.Id).Single();
                employerbank.EmpNo = _employerbank.EmpNo;
                employerbank.BankSortCode = _employerbank.BankSortCode;
                employerbank.EmployerBankId = _employerbank.EmployerBankId;
                employerbank.EmployerId = _employerbank.EmployerId;
                employerbank.EmployeeId = _employerbank.EmployeeId;

                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public void DeleteEmployeesBankTransfer(EmployeesBankTransfersModel _employerbank)
        {
            try
            {
                EmployeesBankTransfer bank = db.EmployeesBankTransfers.Where(p => p.Id == _employerbank.Id).Single();

                db.EmployeesBankTransfers.DeleteObject(bank);
                db.SaveChanges(SaveOptions.AcceptAllChangesAfterSave);
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
            }
        }
        public List<EmployeesBankTransfersModel> GetAllEmployeesBankTransfers(int employerid)
        {
            try
            {
                List<EmployeesBankTransfersModel> _employerbanks = new List<EmployeesBankTransfersModel>();
                var _employerbanksquery = from br in db.EmployeesBankTransfers
                                          where br.EmployerId == employerid
                                          select br;
                List<EmployeesBankTransfer> _mplyrbnk = _employerbanksquery.ToList();
                foreach (var _employerbank in _mplyrbnk)
                {
                    EmployeesBankTransfersModel employerbank = new EmployeesBankTransfersModel();
                    employerbank.Id = _employerbank.Id;
                    employerbank.EmpNo = _employerbank.EmpNo;
                    employerbank.BankSortCode = _employerbank.BankSortCode;
                    employerbank.EmployerBankId = _employerbank.EmployerBankId;
                    employerbank.EmployerId = _employerbank.EmployerId;
                    employerbank.EmployeeId = _employerbank.EmployeeId;

                    _employerbanks.Add(employerbank);
                }

                return _employerbanks;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        public EmployeesBankTransfersModel GetEmployeesBankTransfer(int id)
        {
            try
            {
                var _employerbankquery = (from eb in db.EmployeesBankTransfers
                                          where eb.Id == id
                                          select new EmployeesBankTransfersModel
                                          {
                                              Id = eb.Id,
                                              BankSortCode = eb.BankSortCode,
                                              EmpNo = eb.EmpNo,
                                              EmployerBankId = eb.EmployerBankId,
                                              EmployerId = eb.EmployerId,
                                              EmployeeId = eb.EmployeeId
                                          }).FirstOrDefault();
                EmployeesBankTransfersModel _empbank = _employerbankquery;
                return _empbank;
            }
            catch (Exception ex)
            {
                Log.WriteToErrorLogFile(ex);
                return null;
            }
        }
        #endregion "EmployeesBankTransfers"
        #endregion "Public Methods"




    }
}