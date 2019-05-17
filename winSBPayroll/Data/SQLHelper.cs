using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Common;
using System.Collections.Specialized;
using CommonLib;

/*
 * SMO requires SQL Server Browser Service to be  running
 */
namespace winSBPayroll.Forms
{
    public class SQLHelper
    {
        #region "public methods"
        static public List<SBSystem> GetDataFromXML()
        {
            return GetDataFromXML("Security/Systems.xml");
        }
        static public List<SBSystem> GetDataFromXML(string filename)
        {
            using (XmlReader xmlRdr = new XmlTextReader(filename))
            {
                return new List<SBSystem>(
                   (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                    select new SBSystem(
                       (string)sysElem.Attribute("Name"),
                       (string)sysElem.Attribute("Application"),
                       (string)sysElem.Attribute("Database"),
                       (string)sysElem.Attribute("Server"),
                       (string)sysElem.Attribute("AttachDB"),
                       (string)sysElem.Attribute("Metadata"),
                       (string)sysElem.Attribute("Version"),
                       (bool)sysElem.Attribute("Default")
                        )).ToList());
            }
        }
        static public List<SBSystem_Exp> GetDataFromSBSystem_ExpXML(string filename)
        {
            using (XmlReader xmlRdr = new XmlTextReader(filename))
            {
                return new List<SBSystem_Exp>(
                   (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                    select new SBSystem_Exp(
                       (string)sysElem.Attribute("Name"),
                       (string)sysElem.Attribute("Application")
                        )).ToList());
            }
        }
        static public List<SBSystem_DTO> GetDataFromSBSystem_DTOXML(string filename)
        {
            using (XmlReader xmlRdr = new XmlTextReader(filename))
            {
                return new List<SBSystem_DTO>(
                   (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
                    select new SBSystem_DTO(
                       (string)sysElem.Attribute("Name"),
                       (string)sysElem.Attribute("Application")
                        )).ToList());
            }
        }
        static public void SaveXML(List<SBSystem> systems, string filename)
        {
            var xml = new XElement("Systems", systems.Select(x => new XElement("System",
                                                new XAttribute("Name", x.Name),
                                                new XAttribute("Application", x.Application),
                                                new XAttribute("Database", x.Database),
                                                new XAttribute("Server", x.Server),
                                                new XAttribute("AttachDB", x.AttachDB),
                                                new XAttribute("Metadata", x.Metadata),
                                                new XAttribute("Version", x.Version),
                                                new XAttribute("Default", x.Default))));
            xml.Save(filename);
        }
        static public void SaveXML(string info, string filename)
        {
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(info);
            xdoc.Save(filename);
        }
        static public SBSystem GetDataDefaultSystem()
        {
            var db = from dbs in GetDataFromXML()
                     where dbs.Default
                     select dbs;
            return db.FirstOrDefault();
        }
        static public bool ServerExists(SBSystem system)
        {
            DataTable dt = SmoApplication.EnumAvailableSqlServers(true);
            bool exists = false;
            foreach (DataRow dr in dt.Rows)
            {
                string name = dr["Name"].ToString().Trim();
                string server = dr["Server"].ToString().Trim();
                string Instance = dr["Instance"].ToString().Trim();
                exists = name.ToUpper().Equals(system.Server.Trim().ToUpper());
                
            }

            return exists;
        }
        static public bool DatabaseExists(SBSystem system)
        {
            Server server = null;
            //string sys_auth_mode_dto_filename = "Resources/sys_auth_mode_dto.xml";
            //SBSystem_DTO auth_dto = null;
            //if (File.Exists(sys_auth_mode_dto_filename))
            //{
            //    using (XmlReader xmlRdr = new XmlTextReader(sys_auth_mode_dto_filename))
            //    {
            //        auth_dto = (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
            //                    select new SBSystem_DTO(
            //                  (string)sysElem.Attribute("Name"),
            //                  (string)sysElem.Attribute("Application")
            //                   )).FirstOrDefault();
            //    }
            //}
            //if (auth_dto.Name.Equals("True", StringComparison.CurrentCultureIgnoreCase))
            //{
            //    string auto_complete_conn_info_filename = "Resources/auto_complete_conn_info_dto.xml";
            //    SBSystem_DCP_DTO dcp_dto = null;
            //    if (File.Exists(auto_complete_conn_info_filename))
            //    {
            //        using (XmlReader xmlRdr = new XmlTextReader(auto_complete_conn_info_filename))
            //        {
            //            dcp_dto = (from sysElem in XDocument.Load(xmlRdr).Element("Systems").Elements("System")
            //                       select new SBSystem_DCP_DTO(
            //                      (string)sysElem.Attribute("Name"),
            //                      (string)sysElem.Attribute("Application"),
            //                      (string)sysElem.Attribute("Database"),
            //                      (bool)sysElem.Attribute("SName"),
            //                      (bool)sysElem.Attribute("UName"),
            //                      (bool)sysElem.Attribute("PName")
            //                       )).FirstOrDefault();
            //        }
            //        ServerConnection srvConn = new ServerConnection();
            //        srvConn.ServerInstance = system.Server;
            //        srvConn.LoginSecure = false;
            //        if (dcp_dto.Application != null)
            //        {
            //            srvConn.Login = dcp_dto.Application;
            //        }
            //        if (dcp_dto.Application != null)
            //        {
            //            srvConn.Password = dcp_dto.Database;
            //        }
            //        server = new Server(srvConn);
            //    }
            //}
            //else
            //{
                server = new Server(system.Server);
            //}
            if (server == null) return false;

            return server.Databases.Contains(system.Database);
        }
        static public string DatabaseVersion(SBSystem system)
        {
            if (!DatabaseExists(system))
                throw new ArgumentException("Database " + system.Database + " does not exist" + "\n\n " + "Do you want to Administer Servers and Databases?", "database");

            Server server = new Server(system.Server);
            string DatabaseVersionExtPropertyKey = system.Application.Trim() + "Version";

            Database db = server.Databases[system.Database];

            var sbverion = db.ExtendedProperties[DatabaseVersionExtPropertyKey];
            if (sbverion == null)
                throw new ArgumentException(" A database named [" + system.Database
                    + "] exists but does not contain Version information which is neccessary for the System [" + system.Application
                    + "]  " + "\n\n " + "Do you want to Administer Servers and Databases?", "version");
            return sbverion.Value.ToString();
        } 
        static public string EntityConnection(SBSystem system)
        {
            return EntityConnection(system, "", "", true);
        }
        static public string EntityConnection(SBSystem system, string userId, string Pwd, bool IntegratedSec)
        {
            // Specify the provider name, server and database.
            string providerName = "System.Data.SqlClient";
            string serverName = system.Server;
            string databaseName = system.Database;

            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder =
            new SqlConnectionStringBuilder();

            // Set the properties for the data source.
            sqlBuilder.DataSource = serverName;
            sqlBuilder.InitialCatalog = databaseName;
            sqlBuilder.IntegratedSecurity = IntegratedSec;
            sqlBuilder.UserID = userId;
            sqlBuilder.Password = Pwd;

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
            string metadata = @"res://*/SBPayrollDBEntities.csdl|res://*/SBPayrollDBEntities.ssdl|res://*/SBPayrollDBEntities.msl";
            metadata.Replace("SBPayrollDBEntities", system.Metadata);

            entityBuilder.Metadata = metadata;

            return entityBuilder.ToString();

        } 
        static public void ScriptDatabase(string ServerName, string DatabaseName)
        {
            ScriptDatabase(@"DBScripts/db.sql", ServerName, DatabaseName);
        }
        static public void ScriptDatabase(string txtExportToFile, string ServerName, string DatabaseName)
        {
            var sb = new StringBuilder();

            var srv = new Server(ServerName);

            Database db = srv.Databases[DatabaseName];

            StringBuilder builder = new StringBuilder();
            try
            {
                Scripter scrp = new Scripter(srv);
                scrp.Options.AppendToFile = false;
                scrp.Options.ToFileOnly = false;
                scrp.Options.ScriptDrops = false;             // Don't script DROPs
                scrp.Options.Indexes = true;                  // Include indexes
                scrp.Options.DriAllConstraints = true;        // Include referential constraints in the script
                scrp.Options.Triggers = true;                 // Include triggers
                scrp.Options.FullTextIndexes = true;          // Include full text indexes
                scrp.Options.NonClusteredIndexes = true;      // Include non-clustered indexes
                scrp.Options.NoCollation = false;             // Include collation
                scrp.Options.Bindings = true;                 // Include bindings
                scrp.Options.SchemaQualify = true;            // Include schema qualification, eg. [dbo]
                scrp.Options.IncludeDatabaseContext = true;
                scrp.Options.AnsiPadding = true;
                scrp.Options.FullTextStopLists = true;
                scrp.Options.IncludeIfNotExists = false;
                scrp.Options.ScriptBatchTerminator = true;
                scrp.Options.ExtendedProperties = true;
                scrp.Options.ClusteredIndexes = true;
                scrp.Options.FullTextCatalogs = true;
                scrp.Options.SchemaQualifyForeignKeysReferences = true;
                scrp.Options.XmlIndexes = true;
                scrp.Options.IncludeHeaders = true;

                // Prefectching may speed things up
                scrp.PrefetchObjects = true;

                var urns = new List<Urn>();

                // Iterate through the tables in database and script each one.
                foreach (Table tb in db.Tables)
                {
                    if (tb.IsSystemObject == false)
                    {
                        // Table is not a system object, so add it.
                        urns.Add(tb.Urn);
                    }
                }

                // Iterate through the views in database and script each one.  Display the script.
                foreach (Microsoft.SqlServer.Management.Smo.View view in db.Views)
                {
                    if (view.IsSystemObject == false)
                    {
                        // View is not a system object, so add it.
                        urns.Add(view.Urn);
                    }
                }

                // Iterate through the stored procedures in database and script each one.  Display the script.
                foreach (StoredProcedure sp in db.StoredProcedures)
                {
                    if (sp.IsSystemObject == false)
                    {
                        // Procedure is not a system object, so add it.
                        urns.Add(sp.Urn);
                    }
                }

                // Start by manually adding DB db
                builder.AppendLine("CREATE DATABASE [" + db.Name + "]");
                builder.AppendLine("USE [" + db.Name + "]");
                builder.AppendLine("GO");

                System.Collections.Specialized.StringCollection sc = scrp.Script(urns.ToArray());
                foreach (string st in sc)
                {
                    // It seems each string is a sensible batch, and putting GO after it makes it work in tools like SSMS.
                    // Wrapping each string in an 'exec' statement would work better if using SqlCommand to run the script.
                    builder.Append(st.Trim(new char[] { '\r', '\n' }) + "\r\nGO\r\n");
                }
            }
            catch (Exception ex)
            { 
                showExceptionError("Couldn't generate script.", ex);
                return;
            }

            try
            {
                System.IO.File.WriteAllText(txtExportToFile, builder.ToString());
                System.Windows.Forms.MessageBox.Show("DB exported to script at: " + txtExportToFile, "SB Payroll", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {  
                showExceptionError("Couldn't save Script file.", ex);
                return;
            }

        }
        static void showExceptionError(string msg, Exception ex)
        {
            Utils.LogEventViewer(ex); 
            msg += ex.Message;
            if (ex.InnerException != null)
                msg += "\n" + ex.InnerException.Message;
            System.Windows.Forms.MessageBox.Show(msg, "SB Payroll", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }
        #endregion "public methods"
    }

}

