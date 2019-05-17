
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/20/2013 08:14:54
-- Generated from EDMX file: C:\data\lowq\SPPayroll\DAL\SBPayrollDBEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SBPayrollDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BankBranch_Banks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BankBranch] DROP CONSTRAINT [FK_BankBranch_Banks];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollItems_PayrollItemType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_PayrollItems_PayrollItemType];
GO
IF OBJECT_ID(N'[dbo].[FK_PayslipDet_TaxTracking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayslipDet] DROP CONSTRAINT [FK_PayslipDet_TaxTracking];
GO
IF OBJECT_ID(N'[dbo].[FK_Settings_SettingsGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Settings] DROP CONSTRAINT [FK_Settings_SettingsGroup];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[BankBranch]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BankBranch];
GO
IF OBJECT_ID(N'[dbo].[Banks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Banks];
GO
IF OBJECT_ID(N'[dbo].[Benefits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Benefits];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee];
GO
IF OBJECT_ID(N'[dbo].[Employee_Ext]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Ext];
GO
IF OBJECT_ID(N'[dbo].[Employee_Ext_Fields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Ext_Fields];
GO
IF OBJECT_ID(N'[dbo].[EmployeeTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeTransactions];
GO
IF OBJECT_ID(N'[dbo].[Employer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employer];
GO
IF OBJECT_ID(N'[dbo].[EmpNonCashBenefits]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmpNonCashBenefits];
GO
IF OBJECT_ID(N'[dbo].[HourlyPayment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HourlyPayment];
GO
IF OBJECT_ID(N'[dbo].[LeaveTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LeaveTransactions];
GO
IF OBJECT_ID(N'[dbo].[NHIFRates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NHIFRates];
GO
IF OBJECT_ID(N'[dbo].[PackedTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PackedTransactions];
GO
IF OBJECT_ID(N'[dbo].[PayeeRates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayeeRates];
GO
IF OBJECT_ID(N'[dbo].[PayrollItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayrollItems];
GO
IF OBJECT_ID(N'[dbo].[PayrollItemType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayrollItemType];
GO
IF OBJECT_ID(N'[dbo].[Payrolls]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payrolls];
GO
IF OBJECT_ID(N'[dbo].[PayslipDet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayslipDet];
GO
IF OBJECT_ID(N'[dbo].[PayslipDet_Temp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayslipDet_Temp];
GO
IF OBJECT_ID(N'[dbo].[PayslipMaster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayslipMaster];
GO
IF OBJECT_ID(N'[dbo].[PayslipMaster_Temp]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayslipMaster_Temp];
GO
IF OBJECT_ID(N'[dbo].[Settings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Settings];
GO
IF OBJECT_ID(N'[dbo].[SettingsGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SettingsGroup];
GO
IF OBJECT_ID(N'[dbo].[spUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spUsers];
GO
IF OBJECT_ID(N'[dbo].[TaxTracking]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaxTracking];
GO
IF OBJECT_ID(N'[dbo].[TransactionDef]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TransactionDef];
GO
IF OBJECT_ID(N'[dbo].[Transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Transactions];
GO
IF OBJECT_ID(N'[dbo].[TxnType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TxnType];
GO
IF OBJECT_ID(N'[SBPayrollDBModelStoreContainer].[GetEmpTransactions]', 'U') IS NOT NULL
    DROP TABLE [SBPayrollDBModelStoreContainer].[GetEmpTransactions];
GO
IF OBJECT_ID(N'[SBPayrollDBModelStoreContainer].[vwBankBranches]', 'U') IS NOT NULL
    DROP TABLE [SBPayrollDBModelStoreContainer].[vwBankBranches];
GO
IF OBJECT_ID(N'[SBPayrollDBModelStoreContainer].[vwPayrollMaster]', 'U') IS NOT NULL
    DROP TABLE [SBPayrollDBModelStoreContainer].[vwPayrollMaster];
GO
IF OBJECT_ID(N'[SBPayrollDBModelStoreContainer].[vwPayrollMaster_Temp]', 'U') IS NOT NULL
    DROP TABLE [SBPayrollDBModelStoreContainer].[vwPayrollMaster_Temp];
GO
IF OBJECT_ID(N'[SBPayrollDBModelStoreContainer].[vwPayslipDet]', 'U') IS NOT NULL
    DROP TABLE [SBPayrollDBModelStoreContainer].[vwPayslipDet];
GO
IF OBJECT_ID(N'[SBPayrollDBModelStoreContainer].[vwPayslipDet_Temp]', 'U') IS NOT NULL
    DROP TABLE [SBPayrollDBModelStoreContainer].[vwPayslipDet_Temp];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [AccountId] int  NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [AccountType] nchar(10)  NOT NULL,
    [BookBalance] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'BankBranches'
CREATE TABLE [dbo].[BankBranches] (
    [BankSortCode] nvarchar(50)  NOT NULL,
    [BranchCode] nvarchar(50)  NOT NULL,
    [Bank] nvarchar(50)  NOT NULL,
    [BranchName] varchar(50)  NOT NULL
);
GO

-- Creating table 'Banks'
CREATE TABLE [dbo].[Banks] (
    [BankCode] nvarchar(50)  NOT NULL,
    [BankName] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Benefits'
CREATE TABLE [dbo].[Benefits] (
    [BenefitId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Rate] decimal(19,4)  NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Code] varchar(10)  NOT NULL,
    [Description] varchar(50)  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [EmpNo] nchar(10)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [OtherNames] nvarchar(50)  NOT NULL,
    [DoB] datetime  NULL,
    [MaritalStatus] nchar(1)  NULL,
    [Gender] nchar(1)  NULL,
    [Photo] nvarchar(900)  NULL,
    [DoE] datetime  NULL,
    [BasicComputation] nchar(1)  NOT NULL,
    [BasicPay] decimal(19,4)  NULL,
    [PersonalRelief] decimal(19,4)  NULL,
    [MortgageRelief] decimal(19,4)  NULL,
    [NSSFNo] nvarchar(50)  NULL,
    [NHIFNo] nvarchar(50)  NULL,
    [IDNo] nvarchar(50)  NULL,
    [PINNo] nvarchar(50)  NULL,
    [Department] nvarchar(50)  NULL,
    [Employer] int  NOT NULL,
    [PayPoint] nvarchar(50)  NULL,
    [EmpGroup] nvarchar(50)  NULL,
    [EmpPayroll] nvarchar(50)  NULL,
    [PrevEmployer] nvarchar(50)  NULL,
    [DateLeft] datetime  NULL,
    [PaymentMode] nvarchar(50)  NULL,
    [TelephoneNo] nvarchar(50)  NULL,
    [ChequeNo] nvarchar(50)  NULL,
    [BankCode] nvarchar(50)  NULL,
    [BankAccount] nvarchar(50)  NULL,
    [LeaveBalance] nvarchar(50)  NULL,
    [IsActive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedOn] datetime  NULL
);
GO

-- Creating table 'Employee_Ext'
CREATE TABLE [dbo].[Employee_Ext] (
    [EmpNo] nchar(10)  NOT NULL,
    [ExFieldName] int  NOT NULL,
    [ExFieldStr] varchar(250)  NULL,
    [ExFieldInt] int  NULL,
    [ExFieldDate] datetime  NULL,
    [ExFieldDecimal] decimal(18,0)  NULL
);
GO

-- Creating table 'Employee_Ext_Fields'
CREATE TABLE [dbo].[Employee_Ext_Fields] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(50)  NOT NULL,
    [FType] varchar(50)  NOT NULL
);
GO

-- Creating table 'EmployeeTransactions'
CREATE TABLE [dbo].[EmployeeTransactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostDate] datetime  NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [ItemId] varchar(50)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [Recurrent] bit  NOT NULL,
    [Processed] bit  NULL,
    [Enabled] bit  NOT NULL,
    [TrackYTD] bit  NULL,
    [ShowYTDInPayslip] bit  NULL,
    [Balance] decimal(19,4)  NULL,
    [InitialAmount] decimal(19,4)  NULL,
    [CreatedBy] nchar(10)  NULL,
    [LastChangedBy] nchar(10)  NULL,
    [LastChangeDate] datetime  NULL,
    [AuthorizedBy] nchar(10)  NULL,
    [AuthorizeDate] datetime  NULL,
    [LoanType] nvarchar(50)  NULL,
    [AccumulativePayment] decimal(19,4)  NULL
);
GO

-- Creating table 'Employers'
CREATE TABLE [dbo].[Employers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Address1] nvarchar(250)  NULL,
    [Address2] nvarchar(250)  NULL,
    [Telephone] nvarchar(50)  NULL,
    [PIN] nvarchar(50)  NULL,
    [Email] nvarchar(250)  NULL,
    [Logo] nvarchar(250)  NULL,
    [NHIF] nvarchar(50)  NULL,
    [NSSF] nvarchar(50)  NULL,
    [BankBranchSortCode] nvarchar(50)  NULL,
    [AccountName] nvarchar(50)  NULL,
    [AccountNo] nvarchar(50)  NULL,
    [AuthorizedSignatory] nvarchar(50)  NULL
);
GO

-- Creating table 'EmpNonCashBenefits'
CREATE TABLE [dbo].[EmpNonCashBenefits] (
    [EmpNo] nchar(10)  NOT NULL,
    [Benefit] int  NOT NULL,
    [Quantity] int  NOT NULL,
    [Rate] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'HourlyPayments'
CREATE TABLE [dbo].[HourlyPayments] (
    [Empno] nchar(10)  NOT NULL,
    [WorkDate] datetime  NOT NULL,
    [WorkHours] int  NOT NULL,
    [RatePerHour] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'LeaveTransactions'
CREATE TABLE [dbo].[LeaveTransactions] (
    [Id] int  NOT NULL,
    [PostDate] datetime  NULL,
    [EffectiveDate] datetime  NULL,
    [LeaveDesc] varchar(50)  NULL,
    [NoofDays] int  NULL
);
GO

-- Creating table 'NHIFRates'
CREATE TABLE [dbo].[NHIFRates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FromAmt] decimal(19,4)  NOT NULL,
    [ToAmt] decimal(19,4)  NOT NULL,
    [Rate] decimal(10,4)  NOT NULL
);
GO

-- Creating table 'PackedTransactions'
CREATE TABLE [dbo].[PackedTransactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PackDate] datetime  NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [TxnCode] nchar(10)  NOT NULL,
    [Amount] decimal(10,4)  NOT NULL,
    [CreatedBy] nchar(10)  NULL,
    [Authorized] bit  NULL
);
GO

-- Creating table 'PayeeRates'
CREATE TABLE [dbo].[PayeeRates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FromAmt] decimal(19,4)  NOT NULL,
    [ToAmt] decimal(19,4)  NOT NULL,
    [Rate] decimal(10,4)  NOT NULL
);
GO

-- Creating table 'PayrollItems'
CREATE TABLE [dbo].[PayrollItems] (
    [ItemId] varchar(50)  NOT NULL,
    [ItemType] nchar(10)  NULL,
    [TaxTracking] varchar(50)  NULL,
    [PayableTo] varchar(50)  NULL,
    [GLAccount] varchar(50)  NULL,
    [Active] bit  NULL,
    [AddToPension] bit  NULL,
    [DefaultItem] bit  NULL,
    [Enable] bit  NULL,
    [ReFField] int  NULL
);
GO

-- Creating table 'PayrollItemTypes'
CREATE TABLE [dbo].[PayrollItemTypes] (
    [PayrollItemTypeId] nchar(10)  NOT NULL,
    [Parent] nchar(10)  NULL,
    [Description] varchar(50)  NULL
);
GO

-- Creating table 'Payrolls'
CREATE TABLE [dbo].[Payrolls] (
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [Employer] int  NULL,
    [DateRun] datetime  NULL,
    [RunBy] nchar(10)  NULL,
    [Approved] bit  NULL,
    [ApprovedBy] nchar(10)  NULL,
    [IsOpen] bit  NOT NULL,
    [Processed] bit  NOT NULL
);
GO

-- Creating table 'PayslipDets'
CREATE TABLE [dbo].[PayslipDets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [EmpTxnId] int  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NULL,
    [Description] varchar(50)  NOT NULL,
    [TaxTracking] nchar(10)  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [DEType] nvarchar(1)  NOT NULL,
    [IsStatutory] bit  NOT NULL,
    [ShowInPayslip] bit  NULL,
    [YTD] decimal(18,0)  NULL
);
GO

-- Creating table 'PayslipDet_Temp'
CREATE TABLE [dbo].[PayslipDet_Temp] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [EmpTxnId] int  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NULL,
    [Description] varchar(50)  NOT NULL,
    [TaxTracking] nchar(10)  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [DEType] nvarchar(1)  NOT NULL,
    [IsStatutory] bit  NOT NULL,
    [ShowInPayslip] bit  NULL,
    [YTD] decimal(18,0)  NULL
);
GO

-- Creating table 'PayslipMasters'
CREATE TABLE [dbo].[PayslipMasters] (
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [PrintedBy] nchar(10)  NOT NULL,
    [PrintedOn] datetime  NOT NULL,
    [EmpName] varchar(50)  NOT NULL,
    [PayPoint] nchar(10)  NULL,
    [PIN] nchar(20)  NULL,
    [NHIFNo] nchar(20)  NULL,
    [NSSFNo] nchar(20)  NULL,
    [Department] varchar(10)  NULL,
    [EmpGroup] nchar(10)  NULL,
    [EmpPayroll] nchar(10)  NULL,
    [CompName] varchar(50)  NULL,
    [CompAddr] varchar(50)  NULL,
    [CompTel] varchar(50)  NULL,
    [PayeTax] decimal(18,0)  NOT NULL,
    [BasicPay] decimal(18,0)  NOT NULL,
    [Benefits] decimal(18,0)  NOT NULL,
    [Variables] decimal(18,0)  NOT NULL,
    [OtherDeductions] decimal(18,0)  NOT NULL,
    [GrossTaxableEarnings] decimal(18,0)  NOT NULL,
    [NetTaxableEarnings] decimal(18,0)  NOT NULL,
    [MortgageRelief] decimal(18,0)  NOT NULL,
    [GrossTax] decimal(18,0)  NOT NULL,
    [PersonalRelief] decimal(18,0)  NOT NULL,
    [PensionEmployer] decimal(18,0)  NOT NULL,
    [EmployerNSSF] decimal(18,0)  NOT NULL,
    [PensionEmployee] decimal(18,0)  NOT NULL,
    [BankBranch] nchar(5)  NULL,
    [Account] nchar(20)  NULL,
    [NSSF] decimal(18,0)  NOT NULL,
    [NHIF] decimal(18,0)  NOT NULL,
    [NetPay] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'PayslipMaster_Temp'
CREATE TABLE [dbo].[PayslipMaster_Temp] (
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [PrintedBy] nchar(10)  NOT NULL,
    [PrintedOn] datetime  NOT NULL,
    [EmpName] varchar(50)  NOT NULL,
    [PayPoint] nchar(10)  NULL,
    [PIN] nchar(20)  NULL,
    [NHIFNo] nchar(20)  NULL,
    [NSSFNo] nchar(20)  NULL,
    [Department] varchar(10)  NULL,
    [EmpGroup] nchar(10)  NULL,
    [EmpPayroll] nchar(10)  NULL,
    [CompName] varchar(50)  NULL,
    [CompAddr] varchar(50)  NULL,
    [CompTel] varchar(50)  NULL,
    [PayeTax] decimal(18,0)  NOT NULL,
    [BasicPay] decimal(18,0)  NOT NULL,
    [Benefits] decimal(18,0)  NOT NULL,
    [Variables] decimal(18,0)  NOT NULL,
    [OtherDeductions] decimal(18,0)  NOT NULL,
    [GrossTaxableEarnings] decimal(18,0)  NOT NULL,
    [NetTaxableEarnings] decimal(18,0)  NOT NULL,
    [MortgageRelief] decimal(18,0)  NOT NULL,
    [GrossTax] decimal(18,0)  NOT NULL,
    [PersonalRelief] decimal(18,0)  NOT NULL,
    [PensionEmployer] decimal(18,0)  NOT NULL,
    [EmployerNSSF] decimal(18,0)  NOT NULL,
    [PensionEmployee] decimal(18,0)  NOT NULL,
    [BankBranch] nchar(5)  NULL,
    [Account] nchar(20)  NULL,
    [NSSF] decimal(18,0)  NOT NULL,
    [NHIF] decimal(18,0)  NOT NULL,
    [NetPay] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [SSKey] varchar(20)  NOT NULL,
    [SSValue] varchar(max)  NOT NULL,
    [SSValueType] varchar(50)  NOT NULL,
    [Description] varchar(50)  NOT NULL,
    [SGroup] int  NOT NULL,
    [SSSystem] bit  NOT NULL
);
GO

-- Creating table 'SettingsGroups'
CREATE TABLE [dbo].[SettingsGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupName] varchar(50)  NOT NULL,
    [Parent] int  NOT NULL
);
GO

-- Creating table 'spUsers'
CREATE TABLE [dbo].[spUsers] (
    [UserId] varchar(10)  NOT NULL,
    [Password] varchar(10)  NOT NULL,
    [Role] varchar(50)  NOT NULL,
    [Locked] bit  NULL
);
GO

-- Creating table 'TaxTrackings'
CREATE TABLE [dbo].[TaxTrackings] (
    [TaxTrackingId] nchar(10)  NOT NULL,
    [Description] varchar(50)  NULL
);
GO

-- Creating table 'TransactionDefs'
CREATE TABLE [dbo].[TransactionDefs] (
    [TxnCode] nchar(10)  NOT NULL,
    [DataEntry] nchar(1)  NOT NULL,
    [PayrollItem] nchar(15)  NOT NULL,
    [DefaultAmount] decimal(10,4)  NULL,
    [Enabled] bit  NULL,
    [Recurrent] bit  NULL,
    [TrackYTD] bit  NULL
);
GO

-- Creating table 'Transactions'
CREATE TABLE [dbo].[Transactions] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PostDate] datetime  NULL,
    [Amount] decimal(19,4)  NULL,
    [DRCR] nchar(10)  NULL,
    [Narrative] varchar(50)  NULL,
    [TxnType] int  NULL
);
GO

-- Creating table 'TxnTypes'
CREATE TABLE [dbo].[TxnTypes] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Description] varchar(50)  NOT NULL
);
GO

-- Creating table 'GetEmpTransactions'
CREATE TABLE [dbo].[GetEmpTransactions] (
    [EmpNo] nchar(10)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [MaritalStatus] nchar(1)  NULL,
    [IsActive] bit  NULL,
    [ItemId] varchar(50)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [Recurrent] bit  NOT NULL,
    [Enabled] bit  NOT NULL,
    [BasicPay] decimal(19,4)  NULL,
    [Department] varchar(50)  NULL,
    [DeptCode] varchar(10)  NOT NULL,
    [EmpTrack] bit  NULL,
    [Balance] decimal(19,4)  NULL,
    [ItemType] nchar(10)  NULL,
    [TaxTracking] varchar(50)  NULL,
    [Active] bit  NULL,
    [AddToPension] bit  NULL,
    [ShowYTDInPayslip] bit  NULL,
    [EmpTxnId] int  NOT NULL,
    [Processed] bit  NULL,
    [Parent] nchar(10)  NULL
);
GO

-- Creating table 'vwBankBranches'
CREATE TABLE [dbo].[vwBankBranches] (
    [BankBranchName] nvarchar(103)  NOT NULL,
    [BankSortCode] nvarchar(50)  NOT NULL,
    [BankName] nvarchar(50)  NOT NULL,
    [Bank] nvarchar(50)  NOT NULL,
    [BranchCode] nvarchar(50)  NOT NULL,
    [BranchName] varchar(50)  NOT NULL,
    [BankCode] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'vwPayrollMasters'
CREATE TABLE [dbo].[vwPayrollMasters] (
    [BankName] nvarchar(50)  NULL,
    [BranchName] varchar(50)  NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [OtherNames] nvarchar(50)  NOT NULL,
    [NetPay] decimal(18,0)  NOT NULL,
    [GrossTaxableEarnings] decimal(18,0)  NOT NULL,
    [BasicPay] decimal(18,0)  NOT NULL,
    [NHIF] decimal(18,0)  NOT NULL,
    [NSSF] decimal(18,0)  NOT NULL,
    [PayeTax] decimal(18,0)  NOT NULL,
    [OtherDeductions] decimal(18,0)  NOT NULL,
    [PensionEmployee] decimal(18,0)  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [Benefits] decimal(18,0)  NOT NULL,
    [EmployerNSSF] decimal(18,0)  NOT NULL,
    [EmpName] varchar(50)  NOT NULL,
    [CompName] varchar(50)  NULL,
    [NetTaxableEarnings] decimal(18,0)  NOT NULL,
    [PrintedOn] datetime  NOT NULL,
    [BankBranch] nchar(5)  NULL,
    [Account] nchar(20)  NULL,
    [GrossTax] decimal(18,0)  NOT NULL,
    [CompTel] varchar(50)  NULL,
    [CompAddr] varchar(50)  NULL,
    [PIN] nchar(20)  NULL,
    [BranchCode] nvarchar(50)  NULL,
    [BankSortCode] nvarchar(50)  NULL,
    [PaymentMode] nvarchar(50)  NULL,
    [BankBranchSortCode] nvarchar(50)  NULL,
    [NHIFNo] nchar(20)  NULL,
    [NSSFNo] nchar(20)  NULL,
    [PayPoint] nchar(10)  NULL,
    [PrintedBy] nchar(10)  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [BankAccount] nvarchar(50)  NULL,
    [EmpGroup] nchar(10)  NULL,
    [EmpPayroll] nchar(10)  NULL,
    [Department] varchar(10)  NULL,
    [IDNo] nvarchar(50)  NULL,
    [BankCode] nvarchar(50)  NULL,
    [Variables] decimal(18,0)  NOT NULL,
    [MortgageRelief] decimal(18,0)  NOT NULL,
    [PersonalRelief] decimal(18,0)  NOT NULL,
    [PensionEmployer] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'vwPayrollMaster_Temp'
CREATE TABLE [dbo].[vwPayrollMaster_Temp] (
    [BankName] nvarchar(50)  NULL,
    [BranchName] varchar(50)  NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [OtherNames] nvarchar(50)  NOT NULL,
    [BranchCode] nvarchar(50)  NULL,
    [BankSortCode] nvarchar(50)  NULL,
    [PaymentMode] nvarchar(50)  NULL,
    [BankBranchSortCode] nvarchar(50)  NULL,
    [BankAccount] nvarchar(50)  NULL,
    [IDNo] nvarchar(50)  NULL,
    [BankCode] nvarchar(50)  NULL,
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [PrintedBy] nchar(10)  NOT NULL,
    [PrintedOn] datetime  NOT NULL,
    [EmpName] varchar(50)  NOT NULL,
    [PayPoint] nchar(10)  NULL,
    [PIN] nchar(20)  NULL,
    [NHIFNo] nchar(20)  NULL,
    [Department] varchar(10)  NULL,
    [NSSFNo] nchar(20)  NULL,
    [EmpGroup] nchar(10)  NULL,
    [EmpPayroll] nchar(10)  NULL,
    [CompName] varchar(50)  NULL,
    [CompAddr] varchar(50)  NULL,
    [CompTel] varchar(50)  NULL,
    [PayeTax] decimal(18,0)  NOT NULL,
    [BasicPay] decimal(18,0)  NOT NULL,
    [Benefits] decimal(18,0)  NOT NULL,
    [OtherDeductions] decimal(18,0)  NOT NULL,
    [GrossTaxableEarnings] decimal(18,0)  NOT NULL,
    [NetTaxableEarnings] decimal(18,0)  NOT NULL,
    [GrossTax] decimal(18,0)  NOT NULL,
    [EmployerNSSF] decimal(18,0)  NOT NULL,
    [PensionEmployee] decimal(18,0)  NOT NULL,
    [BankBranch] nchar(5)  NULL,
    [Account] nchar(20)  NULL,
    [NSSF] decimal(18,0)  NOT NULL,
    [NHIF] decimal(18,0)  NOT NULL,
    [NetPay] decimal(18,0)  NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [Variables] decimal(18,0)  NOT NULL,
    [MortgageRelief] decimal(18,0)  NOT NULL,
    [PersonalRelief] decimal(18,0)  NOT NULL,
    [PensionEmployer] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'vwPayslipDets'
CREATE TABLE [dbo].[vwPayslipDets] (
    [Id] int  NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [OtherNames] nvarchar(50)  NOT NULL,
    [ItemId] varchar(50)  NOT NULL,
    [Balance] decimal(19,4)  NULL,
    [ReFField] int  NULL,
    [RepayAmount] decimal(19,4)  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NULL,
    [TxnDate] datetime  NULL,
    [PostDate] datetime  NOT NULL,
    [Description] varchar(50)  NOT NULL,
    [YTD] decimal(18,0)  NULL,
    [InitialAmount] decimal(19,4)  NULL,
    [LoanType] nvarchar(50)  NULL,
    [Employer] int  NOT NULL,
    [BankAccount] nvarchar(50)  NULL,
    [BankCode] nvarchar(50)  NULL,
    [PaymentMode] nvarchar(50)  NULL,
    [ItemType] nchar(10)  NULL,
    [DEType] nvarchar(1)  NOT NULL,
    [EmpTxnId] int  NOT NULL,
    [TaxTracking] nchar(10)  NOT NULL,
    [ShowInPayslip] bit  NULL,
    [IsStatutory] bit  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [Parent] nchar(10)  NULL
);
GO

-- Creating table 'vwPayslipDet_Temp'
CREATE TABLE [dbo].[vwPayslipDet_Temp] (
    [EmpNo] nchar(10)  NOT NULL,
    [Surname] nvarchar(50)  NOT NULL,
    [OtherNames] nvarchar(50)  NOT NULL,
    [ItemId] varchar(50)  NOT NULL,
    [Balance] decimal(19,4)  NULL,
    [TxnDate] datetime  NULL,
    [ReFField] int  NULL,
    [RepayAmount] decimal(19,4)  NOT NULL,
    [PostDate] datetime  NOT NULL,
    [InitialAmount] decimal(19,4)  NULL,
    [LoanType] nvarchar(50)  NULL,
    [Employer] int  NOT NULL,
    [BankAccount] nvarchar(50)  NULL,
    [BankCode] nvarchar(50)  NULL,
    [PaymentMode] nvarchar(50)  NULL,
    [ItemType] nchar(10)  NULL,
    [Parent] nchar(10)  NULL,
    [EmpTxnId] int  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NULL,
    [Description] varchar(50)  NOT NULL,
    [TaxTracking] nchar(10)  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [DEType] nvarchar(1)  NOT NULL,
    [IsStatutory] bit  NOT NULL,
    [ShowInPayslip] bit  NULL,
    [YTD] decimal(18,0)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AccountId] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([AccountId] ASC);
GO

-- Creating primary key on [BankSortCode] in table 'BankBranches'
ALTER TABLE [dbo].[BankBranches]
ADD CONSTRAINT [PK_BankBranches]
    PRIMARY KEY CLUSTERED ([BankSortCode] ASC);
GO

-- Creating primary key on [BankCode] in table 'Banks'
ALTER TABLE [dbo].[Banks]
ADD CONSTRAINT [PK_Banks]
    PRIMARY KEY CLUSTERED ([BankCode] ASC);
GO

-- Creating primary key on [BenefitId] in table 'Benefits'
ALTER TABLE [dbo].[Benefits]
ADD CONSTRAINT [PK_Benefits]
    PRIMARY KEY CLUSTERED ([BenefitId] ASC);
GO

-- Creating primary key on [Code] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Code] ASC);
GO

-- Creating primary key on [EmpNo] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([EmpNo] ASC);
GO

-- Creating primary key on [EmpNo], [ExFieldName] in table 'Employee_Ext'
ALTER TABLE [dbo].[Employee_Ext]
ADD CONSTRAINT [PK_Employee_Ext]
    PRIMARY KEY CLUSTERED ([EmpNo], [ExFieldName] ASC);
GO

-- Creating primary key on [ID] in table 'Employee_Ext_Fields'
ALTER TABLE [dbo].[Employee_Ext_Fields]
ADD CONSTRAINT [PK_Employee_Ext_Fields]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeTransactions'
ALTER TABLE [dbo].[EmployeeTransactions]
ADD CONSTRAINT [PK_EmployeeTransactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employers'
ALTER TABLE [dbo].[Employers]
ADD CONSTRAINT [PK_Employers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [EmpNo], [Benefit] in table 'EmpNonCashBenefits'
ALTER TABLE [dbo].[EmpNonCashBenefits]
ADD CONSTRAINT [PK_EmpNonCashBenefits]
    PRIMARY KEY CLUSTERED ([EmpNo], [Benefit] ASC);
GO

-- Creating primary key on [Empno], [WorkDate] in table 'HourlyPayments'
ALTER TABLE [dbo].[HourlyPayments]
ADD CONSTRAINT [PK_HourlyPayments]
    PRIMARY KEY CLUSTERED ([Empno], [WorkDate] ASC);
GO

-- Creating primary key on [Id] in table 'LeaveTransactions'
ALTER TABLE [dbo].[LeaveTransactions]
ADD CONSTRAINT [PK_LeaveTransactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NHIFRates'
ALTER TABLE [dbo].[NHIFRates]
ADD CONSTRAINT [PK_NHIFRates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PackedTransactions'
ALTER TABLE [dbo].[PackedTransactions]
ADD CONSTRAINT [PK_PackedTransactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PayeeRates'
ALTER TABLE [dbo].[PayeeRates]
ADD CONSTRAINT [PK_PayeeRates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ItemId] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [PK_PayrollItems]
    PRIMARY KEY CLUSTERED ([ItemId] ASC);
GO

-- Creating primary key on [PayrollItemTypeId] in table 'PayrollItemTypes'
ALTER TABLE [dbo].[PayrollItemTypes]
ADD CONSTRAINT [PK_PayrollItemTypes]
    PRIMARY KEY CLUSTERED ([PayrollItemTypeId] ASC);
GO

-- Creating primary key on [Period], [Year] in table 'Payrolls'
ALTER TABLE [dbo].[Payrolls]
ADD CONSTRAINT [PK_Payrolls]
    PRIMARY KEY CLUSTERED ([Period], [Year] ASC);
GO

-- Creating primary key on [Id] in table 'PayslipDets'
ALTER TABLE [dbo].[PayslipDets]
ADD CONSTRAINT [PK_PayslipDets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PayslipDet_Temp'
ALTER TABLE [dbo].[PayslipDet_Temp]
ADD CONSTRAINT [PK_PayslipDet_Temp]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Period], [Year], [EmpNo] in table 'PayslipMasters'
ALTER TABLE [dbo].[PayslipMasters]
ADD CONSTRAINT [PK_PayslipMasters]
    PRIMARY KEY CLUSTERED ([Period], [Year], [EmpNo] ASC);
GO

-- Creating primary key on [Period], [Year], [EmpNo] in table 'PayslipMaster_Temp'
ALTER TABLE [dbo].[PayslipMaster_Temp]
ADD CONSTRAINT [PK_PayslipMaster_Temp]
    PRIMARY KEY CLUSTERED ([Period], [Year], [EmpNo] ASC);
GO

-- Creating primary key on [SSKey] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [PK_Settings]
    PRIMARY KEY CLUSTERED ([SSKey] ASC);
GO

-- Creating primary key on [Id] in table 'SettingsGroups'
ALTER TABLE [dbo].[SettingsGroups]
ADD CONSTRAINT [PK_SettingsGroups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'spUsers'
ALTER TABLE [dbo].[spUsers]
ADD CONSTRAINT [PK_spUsers]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [TaxTrackingId] in table 'TaxTrackings'
ALTER TABLE [dbo].[TaxTrackings]
ADD CONSTRAINT [PK_TaxTrackings]
    PRIMARY KEY CLUSTERED ([TaxTrackingId] ASC);
GO

-- Creating primary key on [TxnCode] in table 'TransactionDefs'
ALTER TABLE [dbo].[TransactionDefs]
ADD CONSTRAINT [PK_TransactionDefs]
    PRIMARY KEY CLUSTERED ([TxnCode] ASC);
GO

-- Creating primary key on [ID] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'TxnTypes'
ALTER TABLE [dbo].[TxnTypes]
ADD CONSTRAINT [PK_TxnTypes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [EmpNo], [Surname], [ItemId], [Amount], [Recurrent], [Enabled], [DeptCode], [EmpTxnId] in table 'GetEmpTransactions'
ALTER TABLE [dbo].[GetEmpTransactions]
ADD CONSTRAINT [PK_GetEmpTransactions]
    PRIMARY KEY CLUSTERED ([EmpNo], [Surname], [ItemId], [Amount], [Recurrent], [Enabled], [DeptCode], [EmpTxnId] ASC);
GO

-- Creating primary key on [BankBranchName], [BankSortCode], [BankName], [Bank], [BranchCode], [BranchName], [BankCode] in table 'vwBankBranches'
ALTER TABLE [dbo].[vwBankBranches]
ADD CONSTRAINT [PK_vwBankBranches]
    PRIMARY KEY CLUSTERED ([BankBranchName], [BankSortCode], [BankName], [Bank], [BranchCode], [BranchName], [BankCode] ASC);
GO

-- Creating primary key on [EmpNo], [Surname], [OtherNames], [NetPay], [GrossTaxableEarnings], [BasicPay], [NHIF], [NSSF], [PayeTax], [OtherDeductions], [PensionEmployee], [Period], [Year], [Benefits], [EmployerNSSF], [EmpName], [NetTaxableEarnings], [PrintedOn], [GrossTax], [PrintedBy], [PaymentDate], [Variables], [MortgageRelief], [PersonalRelief], [PensionEmployer] in table 'vwPayrollMasters'
ALTER TABLE [dbo].[vwPayrollMasters]
ADD CONSTRAINT [PK_vwPayrollMasters]
    PRIMARY KEY CLUSTERED ([EmpNo], [Surname], [OtherNames], [NetPay], [GrossTaxableEarnings], [BasicPay], [NHIF], [NSSF], [PayeTax], [OtherDeductions], [PensionEmployee], [Period], [Year], [Benefits], [EmployerNSSF], [EmpName], [NetTaxableEarnings], [PrintedOn], [GrossTax], [PrintedBy], [PaymentDate], [Variables], [MortgageRelief], [PersonalRelief], [PensionEmployer] ASC);
GO

-- Creating primary key on [Surname], [OtherNames], [Period], [Year], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayeTax], [BasicPay], [Benefits], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [GrossTax], [EmployerNSSF], [PensionEmployee], [NSSF], [NHIF], [NetPay], [EmpNo], [Variables], [MortgageRelief], [PersonalRelief], [PensionEmployer] in table 'vwPayrollMaster_Temp'
ALTER TABLE [dbo].[vwPayrollMaster_Temp]
ADD CONSTRAINT [PK_vwPayrollMaster_Temp]
    PRIMARY KEY CLUSTERED ([Surname], [OtherNames], [Period], [Year], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayeTax], [BasicPay], [Benefits], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [GrossTax], [EmployerNSSF], [PensionEmployee], [NSSF], [NHIF], [NetPay], [EmpNo], [Variables], [MortgageRelief], [PersonalRelief], [PensionEmployer] ASC);
GO

-- Creating primary key on [Id], [EmpNo], [Surname], [OtherNames], [ItemId], [RepayAmount], [Period], [PostDate], [Description], [Employer], [DEType], [EmpTxnId], [TaxTracking], [IsStatutory], [Amount] in table 'vwPayslipDets'
ALTER TABLE [dbo].[vwPayslipDets]
ADD CONSTRAINT [PK_vwPayslipDets]
    PRIMARY KEY CLUSTERED ([Id], [EmpNo], [Surname], [OtherNames], [ItemId], [RepayAmount], [Period], [PostDate], [Description], [Employer], [DEType], [EmpTxnId], [TaxTracking], [IsStatutory], [Amount] ASC);
GO

-- Creating primary key on [EmpNo], [Surname], [OtherNames], [ItemId], [RepayAmount], [PostDate], [Employer], [EmpTxnId], [Period], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory] in table 'vwPayslipDet_Temp'
ALTER TABLE [dbo].[vwPayslipDet_Temp]
ADD CONSTRAINT [PK_vwPayslipDet_Temp]
    PRIMARY KEY CLUSTERED ([EmpNo], [Surname], [OtherNames], [ItemId], [RepayAmount], [PostDate], [Employer], [EmpTxnId], [Period], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Bank] in table 'BankBranches'
ALTER TABLE [dbo].[BankBranches]
ADD CONSTRAINT [FK_BankBranch_Banks]
    FOREIGN KEY ([Bank])
    REFERENCES [dbo].[Banks]
        ([BankCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BankBranch_Banks'
CREATE INDEX [IX_FK_BankBranch_Banks]
ON [dbo].[BankBranches]
    ([Bank]);
GO

-- Creating foreign key on [ItemType] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [FK_PayrollItems_PayrollItemType]
    FOREIGN KEY ([ItemType])
    REFERENCES [dbo].[PayrollItemTypes]
        ([PayrollItemTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollItems_PayrollItemType'
CREATE INDEX [IX_FK_PayrollItems_PayrollItemType]
ON [dbo].[PayrollItems]
    ([ItemType]);
GO

-- Creating foreign key on [TaxTracking] in table 'PayslipDets'
ALTER TABLE [dbo].[PayslipDets]
ADD CONSTRAINT [FK_PayslipDet_TaxTracking]
    FOREIGN KEY ([TaxTracking])
    REFERENCES [dbo].[TaxTrackings]
        ([TaxTrackingId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayslipDet_TaxTracking'
CREATE INDEX [IX_FK_PayslipDet_TaxTracking]
ON [dbo].[PayslipDets]
    ([TaxTracking]);
GO

-- Creating foreign key on [SGroup] in table 'Settings'
ALTER TABLE [dbo].[Settings]
ADD CONSTRAINT [FK_Settings_SettingsGroup]
    FOREIGN KEY ([SGroup])
    REFERENCES [dbo].[SettingsGroups]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Settings_SettingsGroup'
CREATE INDEX [IX_FK_Settings_SettingsGroup]
ON [dbo].[Settings]
    ([SGroup]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------