using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class SystemDetailsForm : Form
    {
        SBSystem sys;

        public SystemDetailsForm(SBSystem sbsystem)
        {
            InitializeComponent();

            if (sbsystem == null)
                throw new ArgumentNullException("sbsystem");
            sys = sbsystem;
        }
        private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }

        private void SystemDetailsForm_Load(object sender, EventArgs e)
        {
            txtSysName.Text = sys.Name;
            txtApplication.Text = sys.Application;
            txtDatabase.Text = sys.Database;
            txtAttachDB.Text = sys.AttachDB;
            txtServer.Text = sys.Server;
            txtMetaData.Text = sys.Metadata; 
            txtVersion.Text = sys.Version;
            chkDefault.Checked = sys.Default;
        }

    }
}
