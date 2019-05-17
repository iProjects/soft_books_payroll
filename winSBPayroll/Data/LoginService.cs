using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text; 
using System.Windows.Forms;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public class LoginService
    {
        Repository rep;
        AuthenticationService authSevice; 
        bool connected = false;

        public LoginService()
        {
            rep = new Repository();
        }

        public bool Connect(string connectionName)
        {
            connected = rep.Connect(connectionName);
            return connected;
        }
        public void Authenticate(string conn, string user, string password, Action<UserModel> successCallback, Action<string, string> failureCallback)
        {
            if (!connected)
            {
                if (!this.Connect(conn))
                {
                    failureCallback("E100", "Not connected to a valid database, please establish a connection first");
                    return;
                }
            }

            //create an authentication service
            if (authSevice == null)
            {
                authSevice = new AuthenticationService(rep);
            }

            authSevice.Authenticate(user, password, successCallback, failureCallback);

        }
        private void OnContextCreated()
        {
            if (!CheckIfSqlServerIsRunning()) //if sql server is not running  then try to  connect to the  server
            {
                //override the default connection string with new
                //this.Connection.ConnectionString = GetAlternateConnection();
            }
        }
        //check if  sqlserver specified in connection string  is running.
        private bool CheckIfSqlServerIsRunning()
        {
            try
            {
                //this.Connection.Open(); //try to open the connection
                //this.Connection.Close();
                return true; //sql server connection got opened successfully return true
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        //Gets the alternate connection of the failover server.
        private static string GetAlternateConnection()
        {
            string providerName = "System.Data.SqlClient";
            var sqlBuilder = new SqlConnectionStringBuilder(); //build your connectionstring
            sqlBuilder.IntegratedSecurity = true;

            var entityConnectionBuilder = new EntityConnectionStringBuilder();
            entityConnectionBuilder.Provider = providerName;
            entityConnectionBuilder.ProviderConnectionString = sqlBuilder.ToString();
            return entityConnectionBuilder.ToString();

        }
    }
}




