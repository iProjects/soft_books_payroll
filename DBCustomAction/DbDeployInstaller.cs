using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DBCustomAction
{
    [RunInstaller(true)]
    public partial class DbDeployInstaller : Installer
    {
        //default value, it will be overwrite by the installer
        string conStr = "packet size=4096;integrated security=SSPI;" +
                    "data source=\"(local)\";persist security info=False;" +
                    "initial catalog=master";
        string exconStr = "Data Source=.\\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";
        SqlConnection masterConnection = new SqlConnection();
		
        public DbDeployInstaller()
        {
            InitializeComponent();
        }

        private string GetSql(string Name)
        {
            try
            {
                //' Gets the current assembly.
                Assembly Asm = Assembly.GetExecutingAssembly();
                //' Resources are named using a fully qualified name.
                Stream strm = Asm.GetManifestResourceStream(Asm.GetName().Name + "." + Name);
                StreamReader reader = new StreamReader(strm);
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private string[] GetSqls(string Name)
        {
            string sql = GetSql(Name);
            sql = sql.Replace("GO", ";");

            return sql.Split(";".ToCharArray());
        }

        private void ExecuteSqls(string DatabaseName, string ResourceName)
        {
            string[] sqlArr = GetSqls(ResourceName);
            foreach (string sqlstr in sqlArr)
            {
                ExecuteSql(DatabaseName, sqlstr);
            }
        }

        private void ExecuteSql(string DatabaseName, string Sql)
        {
            SqlCommand Command = new SqlCommand(Sql, masterConnection);

            //' Initialize the connection, open it, and set it to the "master" database

            //ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["DBCustomAction.Properties.Settings.masterConnectionString"];

            masterConnection.ConnectionString = exconStr;//setting.ConnectionString;
            Command.Connection.Open();
            Command.Connection.ChangeDatabase(DatabaseName);
            try
            {
                Command.ExecuteNonQuery();
            }
            finally
            {
                Command.Connection.Close();
            }
        }

        private void CreateDB(string strDBName)
        {
            try
            {
                ExecuteSql("master", "CREATE DATABASE " + strDBName);

                //' Creates the tables.
                ExecuteSqls(strDBName, "sql_tables.txt");

                //' Creates the views.
                ExecuteSqls(strDBName, "sql_views.txt");

                //' Creates the stored procedures.
                ExecuteSqls(strDBName, "sql_sps.txt");

                //' Enter initial data now.
                ExecuteSqls(strDBName, "sql_data.txt");
            }
            catch
            {
                throw;
            }
        }

        
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            
            base.Install(stateSaver);

            string dbname = this.Context.Parameters["dbname"];

            string isDBEmbedded = this.Context.Parameters["isDBEmbedded"];

                //conStr = GetEXPRLogin(
                //    Context.Parameters["databaseServer"],
                //    Context.Parameters["userName"],
                //    Context.Parameters["userPass"],
                //    "master");
                conStr = exconStr;
                RijndaelCryptography rijndael = new RijndaelCryptography();
                rijndael.GenKey();
                rijndael.Encrypt(conStr);
                //save information in the state-saver IDictionary
                //to be used in the Uninstall method
                stateSaver.Add("key", rijndael.Key);
                stateSaver.Add("IV", rijndael.IV);
                stateSaver.Add("conStr", rijndael.Encrypted);

            EmbeddedSQLServer EI = new EmbeddedSQLServer();

            if (!EI.IsExpressInstalled())
            {
                throw new Exception("SQL Express is not installed. \r\nKindly install express by typing [SESQLServerInstall.exe -i] at command promt");
                //"There are no SQL Server Express instances installed.
                //Install first.
                //EI.AutostartSQLBrowserService = false;
                //EI.AutostartSQLService = true;
                //EI.Collation = "SQL_Latin1_General_Cp1_CS_AS";
                //EI.DisableNetworkProtocols = false;
                //EI.InstanceName = "SQLEXPRESS";
                //EI.ReportErrors = true;
                //EI.SetupFileLocation = "sqlexpr.exe";  //Provide location for the Express setup file
                //EI.SqlBrowserAccountName = ""; //Blank means LocalSystem
                //EI.SqlBrowserPassword = ""; // N/A
                //EI.SqlDataDirectory = "C:\\Program Files\\Microsoft SQL Server\\";
                //EI.SqlInstallDirectory = "C:\\Program Files\\";
                //EI.SqlInstallSharedDirectory = "C:\\Program Files\\";
                //EI.SqlServiceAccountName = ""; //Blank means Localsystem
                //EI.SqlServicePassword = ""; // N/A
                //EI.SysadminPassword = "ThIsIsALoNgPaSsWoRd1234!!"; //<<Supply a secure sysadmin password>>
                //EI.UseSQLSecurityMode = true;
                
                //EI.InstallExpress();

            }

            if (isDBEmbedded.Equals("N"))
            {
                try
                {
                    CreateDB(dbname);
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }


        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);

            //if (savedState.Contains("conStr"))
            //{
            //    RijndaelCryptography rijndael = new RijndaelCryptography();
            //    rijndael.Key = (byte[])savedState["key"];
            //    rijndael.IV = (byte[])savedState["IV"];
            //    conStr = rijndael.Decrypt((byte[])savedState["conStr"]);
            //}

            //SqlConnection sqlCon = new SqlConnection(conStr);
            //SqlConnectionStringBuilder builder =  new SqlConnectionStringBuilder(conStr);
            

            //ExecuteDrop(sqlCon,builder.InitialCatalog);
        }

    }
}