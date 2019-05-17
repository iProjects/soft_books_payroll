using System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using CommonLib;
using System.Data;

namespace winSBPayroll.Forms
{

    public class SystemsForm : System.Windows.Forms.Form
    {

        const string SystemsConfigFile = "Security/Systems.xml";

        /*Default values */
        const string APPLICATION = "SBPayroll";
        const string VERSION = "1.0.0.0";
        const string METADATA = "SBPayrollDBEntities";

        private LinkLabel btnSetasDefault;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        DataSet ds;
        BindingSource bs;
        private DataGridView dataGridViewSystems;
        private DataGridViewTextBoxColumn SysApplication;
        private DataGridViewTextBoxColumn SysMetadata;
        private DataGridViewTextBoxColumn SysVersion;
        private DataGridViewTextBoxColumn AttachDB;
        private DataGridViewTextBoxColumn AppName;
        private DataGridViewTextBoxColumn Server;
        private DataGridViewTextBoxColumn Database;
        private DataGridViewCheckBoxColumn Default;
        private GroupBox groupBox3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        string TableName;

        public SystemsForm()
        {
            InitializeComponent();

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //validate

                //get data
                string xmlData = ds.GetXml();

                //save to file
                ds.WriteXml(SystemsConfigFile);
                MessageBox.Show("Successfully saved file " + SystemsConfigFile, "SB Payroll", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += "\n" + ex.InnerException.Message;
                MessageBox.Show(msg, "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void SystemsForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                bs = new BindingSource(); //Private Variable class level 
                ds = new DataSet();
                ds.ReadXml(SystemsConfigFile);

                bs.DataSource = ds;
                TableName = ds.Tables[0].TableName;
                bs.DataMember = TableName;

                dataGridViewSystems.AutoGenerateColumns = false;
                this.dataGridViewSystems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewSystems.DataSource = bs;

            }
            catch (Exception ex)
            {

                string msg = ex.Message;
                if (ex.InnerException != null)
                    msg += "\n" + ex.InnerException.Message;
                MessageBox.Show(msg, "SB Payroll",  MessageBoxButtons.OK,  MessageBoxIcon.Information);

            }
        } 
        
        private void btnSetasDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*
             * set the selected record default = true. set all others default = false
             */
            if (dataGridViewSystems.SelectedRows.Count != 0)
            {
                foreach (DataRowView row in ds.Tables[0].DefaultView)
                {
                    var dr = (DataRowView)bs.Current;

                    string i = row["Name"].ToString();
                    string b = dr["Name"].ToString();
                    if (row["Name"].ToString() == dr["Name"].ToString())
                    {
                        row["Default"] = true;
                    }
                    else
                    {
                        row["Default"] = false;
                    }
                }

            }
        }

        private void dataGridViewSystems_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {

            e.Row.Cells["SysApplication"].Value = APPLICATION;
            e.Row.Cells["AttachDB"].Value = "";
            e.Row.Cells["SysMetadata"].Value = METADATA;
            e.Row.Cells["SysVersion"].Value = VERSION;
        }


        #region "Windows Form Designer generated code"
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemsForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetasDefault = new System.Windows.Forms.LinkLabel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridViewSystems = new System.Windows.Forms.DataGridView();
            this.Default = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SysApplication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SysMetadata = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SysVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttachDB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AppName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Server = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Database = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSystems)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(506, 19);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 24);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(662, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 24);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 278);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(894, 55);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridViewSystems);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(894, 278);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Systems Configured";
            // 
            // btnSetasDefault
            // 
            this.btnSetasDefault.AutoSize = true;
            this.btnSetasDefault.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetasDefault.LinkColor = System.Drawing.Color.Yellow;
            this.btnSetasDefault.Location = new System.Drawing.Point(18, 61);
            this.btnSetasDefault.Name = "btnSetasDefault";
            this.btnSetasDefault.Size = new System.Drawing.Size(105, 16);
            this.btnSetasDefault.TabIndex = 23;
            this.btnSetasDefault.TabStop = true;
            this.btnSetasDefault.Text = "Set as Default";
            this.btnSetasDefault.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnSetasDefault_LinkClicked);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSetasDefault);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox3.Location = new System.Drawing.Point(726, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(165, 259);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            // 
            // dataGridViewSystems
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSystems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewSystems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSystems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SysApplication,
            this.SysMetadata,
            this.SysVersion,
            this.AttachDB,
            this.AppName,
            this.Server,
            this.Database,
            this.Default});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSystems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewSystems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSystems.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewSystems.Name = "dataGridViewSystems";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSystems.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewSystems.Size = new System.Drawing.Size(723, 259);
            this.dataGridViewSystems.TabIndex = 25;
            this.dataGridViewSystems.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridViewSystems_DefaultValuesNeeded);
            // 
            // Default
            // 
            this.Default.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Default.DataPropertyName = "Default";
            this.Default.HeaderText = "Default";
            this.Default.Name = "Default";
            this.Default.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Server";
            this.dataGridViewTextBoxColumn2.HeaderText = "Server";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Database";
            this.dataGridViewTextBoxColumn3.HeaderText = "Database";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn4.DataPropertyName = "AttachDB";
            this.dataGridViewTextBoxColumn4.HeaderText = "AttachDB";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn5.HeaderText = "Name";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 200;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Server";
            this.dataGridViewTextBoxColumn6.HeaderText = "Server";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Database";
            this.dataGridViewTextBoxColumn7.HeaderText = "Database";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 150;
            // 
            // SysApplication
            // 
            this.SysApplication.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SysApplication.DataPropertyName = "Application";
            this.SysApplication.HeaderText = "Application";
            this.SysApplication.Name = "SysApplication";
            this.SysApplication.Visible = false;
            // 
            // SysMetadata
            // 
            this.SysMetadata.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SysMetadata.DataPropertyName = "Metadata";
            this.SysMetadata.HeaderText = "Metadata";
            this.SysMetadata.Name = "SysMetadata";
            this.SysMetadata.Visible = false;
            // 
            // SysVersion
            // 
            this.SysVersion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SysVersion.DataPropertyName = "Version";
            this.SysVersion.HeaderText = "Version";
            this.SysVersion.Name = "SysVersion";
            this.SysVersion.Visible = false;
            // 
            // AttachDB
            // 
            this.AttachDB.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AttachDB.DataPropertyName = "AttachDB";
            this.AttachDB.HeaderText = "AttachDB";
            this.AttachDB.Name = "AttachDB";
            this.AttachDB.Visible = false;
            // 
            // AppName
            // 
            this.AppName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AppName.DataPropertyName = "Name";
            this.AppName.HeaderText = "Name";
            this.AppName.Name = "AppName";
            this.AppName.Width = 200;
            // 
            // Server
            // 
            this.Server.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Server.DataPropertyName = "Server";
            this.Server.HeaderText = "Server";
            this.Server.Name = "Server";
            this.Server.Width = 200;
            // 
            // Database
            // 
            this.Database.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Database.DataPropertyName = "Database";
            this.Database.HeaderText = "Database";
            this.Database.Name = "Database";
            this.Database.Width = 150;
            // 
            // SystemsForm
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(894, 333);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SystemsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Systems";
            this.Load += new System.EventHandler(this.SystemsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSystems)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.ComponentModel.IContainer components;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion "Windows Form Designer generated code"

       


    }
}
