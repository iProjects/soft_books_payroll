using System;
using System.Collections.Generic; 
using System.Linq;
using DAL;

namespace winSBPayroll.Forms
{   
   // [Export(typeof(IAuthenticationService))]
    public class AuthenticationService //: IAuthenticationService
    {
        private readonly Repository userRepository;

       // [ImportingConstructor]
        public AuthenticationService(Repository userRepository)
        {
            this.userRepository = userRepository;
        }     

        public void Authenticate(string user, string password, Action<UserModel> successCallback, Action<string,string> failureCallback)
        {
            this.userRepository.GetUsers((users) => 
                {
                    var userObtained = ObtainUserByNameAndPassword(user, password, users);
                    if (userObtained != null)
                    {
                        this.AuthenticatedUserName = userObtained.UserName;
                        if (userObtained.Locked ?? false)
                        {
                            failureCallback("E201", "User is locked");
                        }
                        else
                        {
                            successCallback(userObtained);
                        }
                    }
                    else
                    {
                        failureCallback("E200","Could not find user with specified user name and password");
                    }
                });
        }

        private static UserModel ObtainUserByNameAndPassword(string username, string password, IList<UserModel> users)
        {
            //return users.Where((u) => u.UserName == username && u.Password == password).FirstOrDefault();
            return users.Where((u) => u.UserName == username && u.Password == password).FirstOrDefault();
        }


        public string AuthenticatedUserName
        {
            get;
            private set;
        }
    }
}
