using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using CommonLib;

namespace winSBPayroll.Forms
{
    public partial class CreateDB : Form
    {
        const string DatabaseVersionExtPropertyKey = "SBPayrollVersion";
        const string DatabaseVersionExtPropertyValue = "1.0.0.0";
        const string version = @"IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'SBPayrollVersion' , NULL,NULL, NULL,NULL, NULL,NULL)) 
                               EXEC dbo.sp_addextendedproperty @name=N'SBPayrollVersion', @value=N'1.0.0.0' 
                               GO";

        public CreateDB()
        {
            InitializeComponent();
        }

        private string dbstring = string.Empty;

        private void CreateDB_Load(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Going to look for all SQL Servers in the Network this might take some time.\n Do you want to proceed...", Utils.APP_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    DataTable dt = SmoApplication.EnumAvailableSqlServers();
                    foreach (DataRow dr in dt.Rows)
                    {
                        this.cboServers.Items.Add(dr[0]);
                    }
                }
                else
                {
                    this.Close();
                }
                StreamReader sr = new StreamReader("Data/SBPayrollDB.sql");
                dbstring = sr.ReadToEnd();
                sr.Close();
                richTextScript.Text = dbstring;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                Debug.WriteLine(ex.ToString());
                this.txtProgressStatus.Text = ex.Message;
                this.txtProgressStatus.ScrollToCaret();

            }
        }

        private void btnCreateDB_Click(object sender, EventArgs e)
        {
            this.txtProgressStatus.Text = string.Empty;
            StringBuilder sb = new StringBuilder();
           
            try
            {

                //refresh dbstring with probable changes
                string cmd = richTextScript.Text;
                dbstring = "";
                //dbstring = version + cmd;
                dbstring = cmd;
                sb.AppendLine(string.Format("Starting to create Database  '{0}'   with login to   '{1}'  ", this.txtDBName.Text, this.tbBTSAppUser.Text));
                sb.AppendLine("Connecting to Server....."); 
                this.txtProgressStatus.Text = sb.ToString();
                this.txtProgressStatus.ScrollToCaret();
                //Connect to the local, default instance of SQL Server.
                string srvname = this.cboServers.Text as string;
                Server srv;
                if (srvname == null)
                {
                    srv = new Server();
                    sb.AppendLine("Connected to local SQL Server");
                }
                else
                {
                    srv = new Server(srvname);
                    sb.AppendLine(string.Format("Connected to  Server  '{0}' " , srvname));
                }

                this.txtProgressStatus.Text = sb.ToString();
                this.txtProgressStatus.ScrollToCaret();
                //Define a Database object variable by supplying the server and the database name arguments in the constructor.
                Database db = srv.Databases[this.txtDBName.Text.Trim()];
                if (db != null)
                {
                    if (MessageBox.Show(string.Format("The Database  '{0}'  already exists do you want to drop it?", this.txtDBName.Text), "Database", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        db.Drop();
                    }
                    else
                    {
                        if (MessageBox.Show(string.Format("Create Tables and Stored Procedures on existing Database   '{0}' ?", this.txtDBName.Text), "Tables and Stored Procedures", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            sb.AppendLine("Creating Tables and Stored Procedures on existing Database.....");
                            this.txtProgressStatus.Text = sb.ToString();
                            db.ExecuteNonQuery(dbstring);
                            sb.AppendLine(string.Format("Created Tables and Stored Procedures on Database   '{0}'", this.txtDBName.Text));
                            sb.AppendLine("Success!");
                            this.txtProgressStatus.Text = sb.ToString();
                            this.txtProgressStatus.ScrollToCaret();
                            NotifyMessage(Utils.APP_NAME, "Database " + this.txtDBName.Text + " Created Successfully.");
                        }
                        sb.AppendLine("Proceed or select another Database");
                        this.txtProgressStatus.Text = sb.ToString();
                        this.txtProgressStatus.ScrollToCaret();
                        return;
                    }
                }

                sb.AppendLine("Creating the Database.....");
                db = new Database(srv, this.txtDBName.Text);
                this.txtProgressStatus.Text = sb.ToString();
                this.txtProgressStatus.ScrollToCaret();
                
                //Create the database on the instance of SQL Server.
                db.Create();

                sb.AppendLine(string.Format("Created the Database '{0}' ", this.txtDBName.Text));
                sb.AppendLine("Creating Tables and Stored Procedures.....");
                this.txtProgressStatus.Text = sb.ToString();
                this.txtProgressStatus.ScrollToCaret();
                //'Reference the database and display the date when it was created.
                db.ExecuteNonQuery(dbstring);
                sb.AppendLine(string.Format("Created Tables and Stored Procedures on Database   '{0}'", this.txtDBName.Text));
                sb.AppendLine("Success!");
                this.txtProgressStatus.Text = sb.ToString();
                this.txtProgressStatus.ScrollToCaret();
                NotifyMessage(Utils.APP_NAME, "Database " + this.txtDBName.Text + " Created Successfully.");
            }
            catch (Exception ex)
            { 
                sb.AppendLine("Failer:" + ex.Message);
                if (ex.InnerException != null)
                    sb.AppendLine("Inner exception:" + ex.InnerException.Message);
                this.txtProgressStatus.Text = sb.ToString();
                this.txtProgressStatus.ScrollToCaret();

            }

            sb.AppendLine("Proceed or select another Database");
            this.txtProgressStatus.Text = sb.ToString();
            this.txtProgressStatus.ScrollToCaret();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowseScriptFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Title = "Select SQL Script File";
                openFileDialog1.Filter = "SQL Script File (*.sql)|*.sql";
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string FileName = this.openFileDialog1.FileName;
                    txtScriptFilePath.Text = FileName;
                }
                if (System.IO.File.Exists(txtScriptFilePath.Text))
                {
                    dbstring = string.Empty;
                    StreamReader sr = new StreamReader(txtScriptFilePath.Text);
                    dbstring = sr.ReadToEnd();
                    sr.Close();
                }
                richTextScript.Text = dbstring;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex); 
            }
        }
        public bool NotifyMessage(string _Title, string _Text)
        {
            try
            {
                appNotifyIcon.Text = Utils.APP_NAME;
                appNotifyIcon.Icon = new Icon("Resources/Icons/Dollar.ico");
                appNotifyIcon.BalloonTipIcon = ToolTipIcon.Info; 
                appNotifyIcon.BalloonTipTitle = _Title;
                appNotifyIcon.BalloonTipText = _Text;
                appNotifyIcon.Visible = true;
                appNotifyIcon.ShowBalloonTip(900000);

                return true;
            }
            catch (Exception ex)
            {
                Utils.LogEventViewer(ex);
                return false;
            }
        }


    }
}
