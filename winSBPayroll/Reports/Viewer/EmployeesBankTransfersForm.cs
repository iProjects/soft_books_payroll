using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BLL.DataEntry;
using DAL;
using CommonLib;

namespace winSBPayroll.Forms
{
    public partial class EmployeesBankTransfersForm : Form
    {
        DataEntry de;
        SBPayrollDBEntities db;
        Repository rep;
        string connection;
        int _employerid;
        List<EmployeesBankTransfersModel> _bankTransfers = new List<EmployeesBankTransfersModel>();

        public EmployeesBankTransfersForm(int employerid, string Conn)
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(Conn))
                throw new ArgumentNullException("connection");
            connection = Conn;

            de = new DataEntry(connection);
            db = new SBPayrollDBEntities(connection);
            rep = new Repository(connection);

            _employerid = employerid;
        }

        private void EmployeesBankTransfersForm_Load(object sender, EventArgs e)
        {
            try
            {
                var _employeeSurnamesquery = from rl in rep.GetAllActiveEmployeesforEmployer(_employerid)
                                             select rl;
                List<DAL.Employee> _employeeSurnames = _employeeSurnamesquery.ToList();
                DataGridViewComboBoxColumn colCboxEmployeeSurname = new DataGridViewComboBoxColumn();
                colCboxEmployeeSurname.HeaderText = "Surname";
                colCboxEmployeeSurname.Name = "cbEmployeeSurname";
                colCboxEmployeeSurname.DataSource = _employeeSurnames;
                // The display member is the name column in the column datasource  
                colCboxEmployeeSurname.DisplayMember = "Surname";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxEmployeeSurname.DataPropertyName = "EmployeeId";
                // The value member is the primary key of the parent table  
                colCboxEmployeeSurname.ValueMember = "Id";
                colCboxEmployeeSurname.MaxDropDownItems = 10;
                colCboxEmployeeSurname.Width = 100;
                colCboxEmployeeSurname.DisplayIndex = 2;
                colCboxEmployeeSurname.MinimumWidth = 5;
                colCboxEmployeeSurname.FlatStyle = FlatStyle.Flat;
                colCboxEmployeeSurname.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployeeSurname.ReadOnly = true;
                //colCboxEmployeeSurname.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridViewEmployeeBankTransfers.Columns.Contains("cbEmployeeSurname"))
                {
                    dataGridViewEmployeeBankTransfers.Columns.Add(colCboxEmployeeSurname);
                }

                var _employeeOtherNamesquery = from rl in rep.GetAllActiveEmployeesforEmployer(_employerid)
                                               select rl;
                List<DAL.Employee> _employeeOtherNames = _employeeOtherNamesquery.ToList();
                DataGridViewComboBoxColumn colCboxEmployeeOtherNames = new DataGridViewComboBoxColumn();
                colCboxEmployeeOtherNames.HeaderText = "OtherNames";
                colCboxEmployeeOtherNames.Name = "cbEmployeeOtherNames";
                colCboxEmployeeOtherNames.DataSource = _employeeOtherNames;
                // The display member is the name column in the column datasource  
                colCboxEmployeeOtherNames.DisplayMember = "OtherNames";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxEmployeeOtherNames.DataPropertyName = "EmployeeId";
                // The value member is the primary key of the parent table  
                colCboxEmployeeOtherNames.ValueMember = "Id";
                colCboxEmployeeOtherNames.MaxDropDownItems = 10;
                colCboxEmployeeOtherNames.Width = 120;
                colCboxEmployeeOtherNames.DisplayIndex = 3;
                colCboxEmployeeOtherNames.MinimumWidth = 5;
                colCboxEmployeeOtherNames.FlatStyle = FlatStyle.Flat;
                colCboxEmployeeOtherNames.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxEmployeeOtherNames.ReadOnly = true;
                //colCboxEmployeeOtherNames.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridViewEmployeeBankTransfers.Columns.Contains("cbEmployeeOtherNames"))
                {
                    dataGridViewEmployeeBankTransfers.Columns.Add(colCboxEmployeeOtherNames);
                }

                var _banksquery = from rl in rep.GetAllEmployerBanks(_employerid)
                                  select rl;
                List<EmployerBanksModel> _banks = _banksquery.ToList();
                DataGridViewComboBoxColumn colCboxBankBranch = new DataGridViewComboBoxColumn();
                colCboxBankBranch.HeaderText = "Bank";
                colCboxBankBranch.Name = "cbBankBranch";
                colCboxBankBranch.DataSource = _banks;
                // The display member is the name column in the column datasource  
                colCboxBankBranch.DisplayMember = "BankBranchName";
                // The DataPropertyName refers to the foreign key column on the datagridview datasource
                colCboxBankBranch.DataPropertyName = "BankSortCode";
                // The value member is the primary key of the parent table  
                colCboxBankBranch.ValueMember = "BankSortCode";
                colCboxBankBranch.MaxDropDownItems = 10;
                colCboxBankBranch.Width = 100;
                colCboxBankBranch.DisplayIndex = 4;
                colCboxBankBranch.MinimumWidth = 5;
                colCboxBankBranch.FlatStyle = FlatStyle.Flat;
                colCboxBankBranch.DefaultCellStyle.NullValue = "--- Select ---";
                colCboxBankBranch.ReadOnly = false;
                colCboxBankBranch.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (!this.dataGridViewEmployeeBankTransfers.Columns.Contains("cbBankBranch"))
                {
                    dataGridViewEmployeeBankTransfers.Columns.Add(colCboxBankBranch);
                }

                var empquery = from ep in rep.GetAllEmployeesBankTransfers(_employerid)
                               select ep;
                _bankTransfers = empquery.ToList();

                if (_bankTransfers.Count == 0)
                {
                    var _EmployeesBankTransfersquery = from ep in rep.GetAllActiveEmployees()
                                                       join emp in db.Employers on ep.EmployerId equals emp.Id
                                                       join empbank in db.EmployerBanks on emp.Id equals empbank.EmployerId
                                                       where emp.Id == _employerid
                                                       where emp.IsActive == true
                                                       where emp.IsDeleted == false
                                                       where ep.IsActive == true
                                                       where ep.IsDeleted == false
                                                       select new EmployeesBankTransfersModel
                                                       {
                                                           EmpNo = ep.EmpNo,
                                                           BankSortCode = empbank.BankSortCode,
                                                           EmployerBankId = empbank.Id,
                                                           EmployerId = emp.Id,
                                                           EmployeeId =ep.Id
                                                       };
                    List<EmployeesBankTransfersModel> _EmployeesBankTransfers = _EmployeesBankTransfersquery.ToList();
                    foreach (var emp in _EmployeesBankTransfers)
                    {
                        if (!_bankTransfers.Any(i => i.EmpNo == emp.EmpNo))
                        {
                            _bankTransfers.Add(emp);
                        }
                    }
                }
                bindingSourceEmployeeBankTransfers.DataSource = _bankTransfers;
                this.dataGridViewEmployeeBankTransfers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridViewEmployeeBankTransfers.AutoGenerateColumns = false;
                dataGridViewEmployeeBankTransfers.DataSource = bindingSourceEmployeeBankTransfers;
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
                _bankTransfers = null;
                _bankTransfers = new List<EmployeesBankTransfersModel>();
                bindingSourceEmployeeBankTransfers.DataSource = null;

                var empquery = from ep in rep.GetAllEmployeesBankTransfers(_employerid)
                               select ep;
                _bankTransfers = empquery.ToList();

                if (_bankTransfers.Count == 0)
                {
                    var _EmployeesBankTransfersquery = from ep in rep.GetAllActiveEmployees()
                                                       join emp in db.Employers on ep.EmployerId equals emp.Id
                                                       join empbank in db.EmployerBanks on emp.Id equals empbank.EmployerId
                                                       where emp.Id == _employerid
                                                       select new EmployeesBankTransfersModel
                                                       {
                                                           EmpNo = ep.EmpNo,
                                                           BankSortCode = empbank.BankSortCode,
                                                           EmployerBankId = empbank.Id,
                                                           EmployerId = emp.Id,
                                                           EmployeeId=ep.Id
                                                       };
                    List<EmployeesBankTransfersModel> _EmployeesBankTransfers = _EmployeesBankTransfersquery.ToList();
                    foreach (var emp in _EmployeesBankTransfers)
                    {
                        if (!_bankTransfers.Any(i => i.EmpNo == emp.EmpNo))
                        {
                            _bankTransfers.Add(emp);
                        }
                    }
                }
                bindingSourceEmployeeBankTransfers.DataSource = _bankTransfers;
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
        private void dataGridViewEmployeeBankTransfers_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                List<EmployeesBankTransfersModel> bankTransfers = (List<EmployeesBankTransfersModel>)bindingSourceEmployeeBankTransfers.List;

                if (bankTransfers.Count != 0)
                {
                    foreach (var emp in bankTransfers)
                    {
                        EmployeesBankTransfersModel empt = new EmployeesBankTransfersModel();
                        empt.Id = emp.Id;
                        empt.EmpNo = emp.EmpNo;
                        empt.BankSortCode = emp.BankSortCode;
                        empt.EmployerId = emp.EmployerId;
                        empt.EmployerBankId = emp.EmployerBankId;
                        empt.EmployeeId = emp.EmployeeId;

                        if (rep.GetAllEmployeesBankTransfers(_employerid).Any(i => i.EmpNo == empt.EmpNo && empt.EmployerId == emp.EmployerId))
                        {
                            rep.UpdateEmployeesBankTransfer(empt);
                        }
                        if (!rep.GetAllEmployeesBankTransfers(_employerid).Any(i => i.EmpNo == empt.EmpNo && empt.EmployerId == emp.EmployerId))
                        {
                            rep.AddEmployeesBankTransfer(empt);
                        }
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex);
            }
        }




    }
}