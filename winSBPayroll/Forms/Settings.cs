using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.DataEntry;
using CommonLib;
using DAL;

namespace winSBPayroll.Forms
{
    public partial class Settings : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        public string _SSKEY;
        List<SettingsGroup> settings;
        bool busy = false;

        public Settings(string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);
        }

        public Settings(string sskey, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _SSKEY = sskey;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                treeViewSettingsGroup.CheckBoxes = true;
                treeViewSettingsGroup.FullRowSelect = true;
                treeViewSettingsGroup.HideSelection = false;
                treeViewSettingsGroup.HotTracking = true;
                treeViewSettingsGroup.ShowLines = true;
                treeViewSettingsGroup.ShowNodeToolTips = true;
                treeViewSettingsGroup.ShowPlusMinus = true;
                treeViewSettingsGroup.ShowRootLines = true;

                var settingsQuery = db.SettingsGroups.Include("Settings");
                settings = settingsQuery.ToList();

                dataGridViewSettings.AutoGenerateColumns = false;
                this.dataGridViewSettings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                bindingSourceSettingGroup.DataSource = settings;
                dataGridViewSettings.DataSource = bindingSourceSettings;

                BuildTree();
             }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        public void RefreshGrid()
        {
            try
            {
                bindingSourceSettingGroup.DataSource = null;
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BuildTree()
        {
            try
            {

                treeViewSettingsGroup.Nodes.Clear(); // Clear any existing items
                treeViewSettingsGroup.BeginUpdate(); // prevent overhead and flicker

                // load all the lowest tree nodes
                TreeBuilder tb = new TreeBuilder();
                tb.PopulateTreeView(treeViewSettingsGroup, settings);

                treeViewSettingsGroup.EndUpdate(); // re-enable the tree
                treeViewSettingsGroup.Refresh(); // refresh the treeview display
                treeViewSettingsGroup.ExpandAll(); // expand all nodes
                if (treeViewSettingsGroup.Nodes.Count > 0)
                {
                    // Select the root node
                    treeViewSettingsGroup.SelectedNode = treeViewSettingsGroup.Nodes[0];
                }
                int count = treeViewSettingsGroup.GetNodeCount(true);
                groupBox2.Text = "Settings Groups  " + count.ToString();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                db.SaveChanges();
                MessageBox.Show("Save Sucessfull!", "SB Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void treeViewSettingsGroup_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // Get the node that was clicked
                TreeNode selectedNode = e.Node;

                if (selectedNode != null)
                {
                    int itemId = int.Parse(selectedNode.Name);
                    var settingGroup = settings.Where(s => s.Id == itemId).SingleOrDefault();
                    if (settingGroup != null)
                        bindingSourceSettings.DataSource = settingGroup.Settings;
                    groupBox3.Text = "Settings  " + bindingSourceSettings.Count.ToString();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void treeViewSettingsGroup_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (busy) return;
            busy = true;
            try
            {
                checkNodes(e.Node, e.Node.Checked);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
            finally
            {
                busy = false;
            }
        }
        private void checkNodes(TreeNode node, bool check)
        {
            foreach (TreeNode child in node.Nodes)
            {
                child.Checked = check;
                checkNodes(child, check);
            }
        }
        private void btnExpandAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                treeViewSettingsGroup.ExpandAll();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnCollapseAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                treeViewSettingsGroup.CollapseAll();
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }








    }
}