using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Splash;
using winSBPayroll.Forms;
using CommonLib;

namespace winSBPayroll
{
    static class Program
    {
        const string SystemsConfigFile = "Security/Systems.xml";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();

            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);

                SplashScreen.ShowSplashScreen();

                Application.DoEvents();

                SplashScreen.SetStatus("Checking for [ " + SystemsConfigFile + " ] ...");
                if (!File.Exists(SystemsConfigFile))
                    throw new FileNotFoundException(Utils.APP_NAME_SB + " cannot locate configuration file " + SystemsConfigFile);

                SplashScreen.SetStatus("Checking for Default System...");
                SBSystem defSys = SQLHelper.GetDataDefaultSystem();
                if (defSys == null)
                    throw new ArgumentException("No Default System is Set", "system");

                //SplashScreen.SetStatus("Connecting to the SQL Server [" + defSys.Server + "]");
                //SplashScreen.SetStatus("Connecting to the Server...");
                //if (!SQLHelper.ServerExists(defSys))
                //    throw new ArgumentException("Unable to connect to Server [" + defSys.Server + "] ", "server");

                SplashScreen.SetStatus("Checking for a valid Database...");
                if (!SQLHelper.DatabaseExists(defSys))
                    throw new ArgumentException("Database [ " + defSys.Database + " ] does not exist in Server [ " + defSys.Server + " ] ", "database");

                SplashScreen.SetStatus("Checking for Database Version...");
                string dbver = SQLHelper.DatabaseVersion(defSys);
                string sysver = Assembly.GetEntryAssembly().GetName().Version.ToString();
                if (!dbver.Equals(sysver))
                    throw new ArgumentException("Database and System Version do not match; the Database may not be usable. Use a Database Migration Tool", "version");

                SplashScreen.SetStatus("Checking Defaults Tables are populated...");

                System.Threading.Thread.Sleep(400);

                SplashScreen.SetStatus("Checking for a valid License...");

                System.Threading.Thread.Sleep(2000);

                SplashScreen.CloseForm();

                Application.Run(new Forms.login_form(defSys));
                //Application.Run(new DatabaseControlPanelForm());

                Application.DoEvents();

            }
            catch (ArgumentException argex)
            {
                string msgex = string.Empty;
                if (argex.ParamName.Equals("server") || argex.ParamName.Equals("system") || argex.ParamName.Equals("database") || argex.ParamName.Equals("version"))
                {
                    switch (argex.ParamName)
                    {
                        case "system":
                            msgex = "Do you want to search other Servers ?";
                            break;
                        case "server":
                            msgex = "The default Server does not exist. Do you want to search for other Servers ?";
                            break;
                        case "database":
                            msgex = "Do you want to create a Database ?";
                            break;
                        case "version":
                            msgex = "Do you want to create a Database with current Version ?";
                            break;
                    }

                    string msg = argex.Message;
                    if (argex.InnerException != null)
                        msg += "\n" + argex.InnerException.Message;

                    msg += "\n\n " + msgex;

                    if (DialogResult.Yes == MessageBox.Show(msg, Utils.APP_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                    {
                        DatabaseControlPanelForm f = new DatabaseControlPanelForm();
                        f.ShowDialog();
                    }
                }
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException smoex)
            {
                string msg = smoex.Message;
                if (smoex.InnerException != null)
                    msg += "\n" + smoex.InnerException.Message
                        + "\n\n " + "Do you want to Administer Servers and Databases ?";

                if (DialogResult.Yes == MessageBox.Show(msg, Utils.APP_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    DatabaseControlPanelForm f = new DatabaseControlPanelForm();
                    f.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += "\n" + ex.InnerException.Message
                        + "\n\n " + "Do you want to Administer Servers and Databases ?";

                if (DialogResult.Yes == MessageBox.Show(msg, Utils.APP_NAME, MessageBoxButtons.YesNo, MessageBoxIcon.Information))
                {
                    DatabaseControlPanelForm f = new DatabaseControlPanelForm();
                    f.ShowDialog();
                }
            }
        }

        static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            Console.WriteLine(ex.ToString());
        }

        static void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception ex = e.Exception;
            Console.WriteLine(ex.ToString());
        }


    }
}
