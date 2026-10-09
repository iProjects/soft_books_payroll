using System;
using System.Net;

namespace DAL
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string password_hash { get; set; }

        public string password_salt { get; set; }

        public int? RoleId { get; set; }

        public bool? Locked { get; set; }

        public UserModel()
        {

        }

        public UserModel(int userid, string username, string fullname, string pwd, int role, bool locked)
        {
            this.UserId = userid;
            this.UserName = username;
            this.Password = pwd;
            this.RoleId = role;
            this.Locked = locked;
        }


        public UserModel(int userid, string username, string fullname, string pwd, int role, bool locked, string hash, string salt)
        {
            this.UserId = userid;
            this.UserName = username;
            this.Password = pwd;
            this.password_hash = hash;
            this.password_salt = salt;
            this.RoleId = role;
            this.Locked = locked;
        }

    }
}
