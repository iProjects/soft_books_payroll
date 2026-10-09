using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Text;

namespace winSBPayroll
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            this.Text = String.Format("About {0}", app_assembly_info.AssemblyTitle);
            this.labelProductName.Text = app_assembly_info.AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", app_assembly_info.AssemblyVersion) + "                " + String.Format("Build {0}", app_assembly_info.Build_Number);
            this.labelCopyright.Text = app_assembly_info.AssemblyCopyright + " - " + DateTime.Now.Year;
            this.labelCompanyName.Text = app_assembly_info.AssemblyCompany;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Soft Books Payroll is Part of Soft Books Suite");
            sb.AppendLine("Contact the developer");
            sb.AppendLine("Phone no : 071776932");
            sb.AppendLine("Email : kevinmk30@gmail.com");
            sb.AppendLine("Soft Books Suite comprises:");
            sb.AppendLine("Soft Books Payroll");
            sb.AppendLine("Soft Books School");
            sb.AppendLine("Soft Books Sacco");
            sb.AppendLine("Fanikiwa Peer to Peer Lending");
            this.textBoxDescription.Text = sb.ToString();
        }

        private void AboutBox_Load(object sender, EventArgs e)
        {

        }




    }
}
