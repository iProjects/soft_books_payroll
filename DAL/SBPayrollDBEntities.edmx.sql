
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 11/26/2022 19:01:19
-- Generated from EDMX file: D:\wakxpx\csharp\visualstudio\2010\SBPayroll\DAL\SBPayrollDBEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [sbpayrolldb_november];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BankBranch_Banks]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BankBranch] DROP CONSTRAINT [FK_BankBranch_Banks];
GO
IF OBJECT_ID(N'[dbo].[FK_Employees_Employers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Employers];
GO
IF OBJECT_ID(N'[dbo].[FK_PayrollItems_TaxTracking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_PayrollItems_TaxTracking];
GO
IF OBJECT_ID(N'[dbo].[FK_Settings_SettingsGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Settings] DROP CONSTRAINT [FK_Settings_SettingsGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_spAllowedReportsRolesMenus_spReportsMenuItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[spAllowedReportsRolesMenus] DROP CONSTRAINT [FK_spAllowedReportsRolesMenus_spReportsMenuItems];
GO
IF OBJECT_ID(N'[dbo].[FK_spAllowedRoleMenus_spMenuItems]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[spAllowedRoleMenus] DROP CONSTRAINT [FK_spAllowedRoleMenus_spMenuItems];
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
IF OBJECT_ID(N'[dbo].[DeductionsTracker]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeductionsTracker];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Employee_Ext]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Ext];
GO
IF OBJECT_ID(N'[dbo].[Employee_Ext_Fields]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee_Ext_Fields];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[EmployeesBankTransfers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeesBankTransfers];
GO
IF OBJECT_ID(N'[dbo].[EmployeeTransactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeTransactions];
GO
IF OBJECT_ID(N'[dbo].[EmployerBanks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployerBanks];
GO
IF OBJECT_ID(N'[dbo].[Employers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employers];
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
IF OBJECT_ID(N'[dbo].[PayrollTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayrollTypes];
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
IF OBJECT_ID(N'[dbo].[spAllowedReportsRolesMenus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spAllowedReportsRolesMenus];
GO
IF OBJECT_ID(N'[dbo].[spAllowedRoleMenus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spAllowedRoleMenus];
GO
IF OBJECT_ID(N'[dbo].[spAllowedRoleMenusweb]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spAllowedRoleMenusweb];
GO
IF OBJECT_ID(N'[dbo].[spMenuItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spMenuItems];
GO
IF OBJECT_ID(N'[dbo].[spMenus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spMenus];
GO
IF OBJECT_ID(N'[dbo].[spReportsMenuItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spReportsMenuItems];
GO
IF OBJECT_ID(N'[dbo].[spRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spRoles];
GO
IF OBJECT_ID(N'[dbo].[spUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spUsers];
GO
IF OBJECT_ID(N'[dbo].[spUsersInRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[spUsersInRoles];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TaxTracking]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TaxTracking];
GO
IF OBJECT_ID(N'[dbo].[TechParams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TechParams];
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
IF OBJECT_ID(N'[dbo].[UserProfile]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProfile];
GO
IF OBJECT_ID(N'[dbo].[webpages_Membership]', 'U') IS NOT NULL
    DROP TABLE [dbo].[webpages_Membership];
GO
IF OBJECT_ID(N'[dbo].[webpages_OAuthMembership]', 'U') IS NOT NULL
    DROP TABLE [dbo].[webpages_OAuthMembership];
GO
IF OBJECT_ID(N'[dbo].[webpages_Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[webpages_Roles];
GO
IF OBJECT_ID(N'[dbo].[webpages_UsersInRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[webpages_UsersInRoles];
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
    [Id] int  NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [AccountType] nvarchar(50)  NOT NULL,
    [BookBalance] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'BankBranches'
CREATE TABLE [dbo].[BankBranches] (
    [BankSortCode] nvarchar(50)  NOT NULL,
    [BranchCode] nvarchar(50)  NOT NULL,
    [BranchName] nvarchar(150)  NOT NULL,
    [BankCode] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Banks'
CREATE TABLE [dbo].[Banks] (
    [BankCode] nvarchar(50)  NOT NULL,
    [BankName] nvarchar(150)  NOT NULL
);
GO

-- Creating table 'Benefits'
CREATE TABLE [dbo].[Benefits] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(50)  NULL,
    [Rate] decimal(19,4)  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'DeductionsTrackers'
CREATE TABLE [dbo].[DeductionsTrackers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PayrollItemId] nvarchar(150)  NULL,
    [ItemTypeDescription] nvarchar(150)  NULL,
    [LoanYTD] decimal(18,0)  NULL,
    [SaccoYTD] decimal(18,0)  NULL,
    [Amount] decimal(18,0)  NULL,
    [EmployeeName] nvarchar(150)  NULL,
    [EmployeeId] int  NULL,
    [EmployeeTransactionId] int  NULL,
    [Year] int  NULL,
    [Term] int  NULL,
    [IsProcessed] bit  NULL,
    [IsClosed] bit  NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(2)  NULL,
    [Description] nvarchar(50)  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'Employee_Ext'
CREATE TABLE [dbo].[Employee_Ext] (
    [EmpNo] nchar(10)  NOT NULL,
    [ExFieldName] int  NOT NULL,
    [ExFieldStr] varchar(250)  NULL,
    [ExFieldInt] int  NULL,
    [ExFieldDate] datetime  NULL,
    [ExFieldDecimal] decimal(18,0)  NULL,
    [EmployerId] int  NULL
);
GO

-- Creating table 'Employee_Ext_Fields'
CREATE TABLE [dbo].[Employee_Ext_Fields] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(50)  NOT NULL,
    [FType] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmpNo] nvarchar(250)  NULL,
    [Surname] nvarchar(250)  NULL,
    [OtherNames] nvarchar(250)  NULL,
    [Email] nvarchar(250)  NULL,
    [DoB] datetime  NULL,
    [MaritalStatus] nvarchar(1)  NULL,
    [Gender] nvarchar(1)  NULL,
    [Photo] nvarchar(max)  NULL,
    [DoE] datetime  NULL,
    [BasicComputation] nvarchar(1)  NULL,
    [BasicPay] decimal(19,4)  NULL,
    [PersonalRelief] decimal(19,4)  NULL,
    [MortgageRelief] decimal(19,4)  NULL,
    [InsuranceRelief] decimal(19,4)  NULL,
    [NSSFNo] nvarchar(250)  NULL,
    [NHIFNo] nvarchar(250)  NULL,
    [IDNo] nvarchar(250)  NULL,
    [PINNo] nvarchar(250)  NULL,
    [DepartmentId] int  NULL,
    [EmployerId] int  NOT NULL,
    [PayPoint] nvarchar(250)  NULL,
    [EmpGroup] nvarchar(250)  NULL,
    [EmpPayroll] nvarchar(250)  NULL,
    [PrevEmployer] nvarchar(250)  NULL,
    [DateLeft] datetime  NULL,
    [PaymentMode] nvarchar(250)  NULL,
    [TelephoneNo] nvarchar(250)  NULL,
    [ChequeNo] nvarchar(250)  NULL,
    [BankCode] nvarchar(250)  NULL,
    [BankAccount] nvarchar(250)  NULL,
    [LeaveBalance] nvarchar(250)  NULL,
    [IsActive] bit  NULL,
    [CreatedBy] nvarchar(250)  NULL,
    [CreatedOn] datetime  NULL,
    [IsDeleted] bit  NULL,
    [SystemId] nvarchar(250)  NULL,
    [IsDisabled] bit  NULL,
    [MpesaReferenceNo] nvarchar(250)  NULL
);
GO

-- Creating table 'EmployeesBankTransfers'
CREATE TABLE [dbo].[EmployeesBankTransfers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmpNo] nvarchar(250)  NULL,
    [BankSortCode] nvarchar(250)  NULL,
    [EmployerBankId] int  NULL,
    [EmployerId] int  NULL,
    [EmployeeId] int  NULL
);
GO

-- Creating table 'EmployeeTransactions'
CREATE TABLE [dbo].[EmployeeTransactions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostDate] datetime  NOT NULL,
    [EmpNo] nvarchar(250)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [Enabled] bit  NOT NULL,
    [Recurrent] bit  NOT NULL,
    [ItemId] nvarchar(150)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [Balance] decimal(19,4)  NULL,
    [InitialAmount] decimal(19,4)  NULL,
    [Processed] bit  NULL,
    [TrackYTD] bit  NULL,
    [ShowYTDInPayslip] bit  NULL,
    [CreatedBy] nvarchar(250)  NULL,
    [LastChangedBy] nvarchar(250)  NULL,
    [LastChangeDate] datetime  NULL,
    [AuthorizedBy] nvarchar(250)  NULL,
    [AuthorizeDate] datetime  NULL,
    [LoanType] nvarchar(250)  NULL,
    [AccumulativePayment] decimal(19,4)  NULL,
    [IsDeleted] bit  NULL,
    [IsLoanRepaymentInFull] bit  NULL,
    [LoanBeingRepayedId] int  NULL,
    [SaccoBeingRepayedId] int  NULL,
    [IsLoanRepaymentInCash] bit  NULL,
    [IsSharesRepaymentInCash] bit  NULL
);
GO

-- Creating table 'EmployerBanks'
CREATE TABLE [dbo].[EmployerBanks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmployerId] int  NULL,
    [BankSortCode] nvarchar(50)  NULL,
    [AccountName] nvarchar(50)  NULL,
    [AccountNo] nvarchar(50)  NULL,
    [Signatory] nvarchar(50)  NULL,
    [IsDefault] bit  NULL
);
GO

-- Creating table 'Employers'
CREATE TABLE [dbo].[Employers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(250)  NULL,
    [Email] nvarchar(250)  NULL,
    [Telephone] nvarchar(250)  NULL,
    [Address1] nvarchar(250)  NULL,
    [Address2] nvarchar(250)  NULL,
    [Slogan] nvarchar(250)  NULL,
    [AuthorizedSignatory] nvarchar(250)  NULL,
    [PIN] nvarchar(250)  NULL,
    [NHIF] nvarchar(250)  NULL,
    [NSSF] nvarchar(250)  NULL,
    [BankBranchSortCode] nvarchar(250)  NULL,
    [AccountName] nvarchar(250)  NULL,
    [AccountNo] nvarchar(250)  NULL,
    [Logo] nvarchar(250)  NULL,
    [IsDefault] bit  NULL,
    [IsActive] bit  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'EmpNonCashBenefits'
CREATE TABLE [dbo].[EmpNonCashBenefits] (
    [EmployeeId] int  NOT NULL,
    [EmpNo] nchar(10)  NOT NULL,
    [BenefitId] int  NOT NULL,
    [Quantity] int  NOT NULL,
    [Rate] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'HourlyPayments'
CREATE TABLE [dbo].[HourlyPayments] (
    [EmployeeId] int  NOT NULL,
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
    [LeaveDesc] nvarchar(50)  NULL,
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
    [EmployeeId] int  NOT NULL,
    [TxnCode] nchar(10)  NOT NULL,
    [Amount] decimal(10,4)  NOT NULL,
    [CreatedBy] nvarchar(50)  NULL,
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
    [Id] nvarchar(150)  NOT NULL,
    [Description] nvarchar(150)  NULL,
    [ItemTypeId] nvarchar(10)  NULL,
    [TaxTrackingId] nvarchar(10)  NULL,
    [PayableTo] nvarchar(50)  NULL,
    [GLAccount] nvarchar(50)  NULL,
    [ReFField] int  NULL,
    [DefaultItem] bit  NULL,
    [AddToPension] bit  NULL,
    [Active] bit  NULL,
    [Enable] bit  NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'PayrollItemTypes'
CREATE TABLE [dbo].[PayrollItemTypes] (
    [Id] nvarchar(10)  NOT NULL,
    [Parent] nvarchar(50)  NULL,
    [Description] nvarchar(50)  NULL,
    [Status] nvarchar(50)  NULL,
    [IsDefault] bit  NULL
);
GO

-- Creating table 'Payrolls'
CREATE TABLE [dbo].[Payrolls] (
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [EmployerId] int  NOT NULL,
    [DateRun] datetime  NULL,
    [RunBy] nvarchar(50)  NULL,
    [Approved] bit  NULL,
    [ApprovedBy] nvarchar(50)  NULL,
    [IsOpen] bit  NOT NULL,
    [Processed] bit  NOT NULL
);
GO

-- Creating table 'PayrollTypes'
CREATE TABLE [dbo].[PayrollTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(250)  NULL,
    [Remarks] nvarchar(max)  NULL
);
GO

-- Creating table 'PayslipDets'
CREATE TABLE [dbo].[PayslipDets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmpNo] nvarchar(250)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [EmployerId] int  NULL,
    [EmpTxnId] int  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NULL,
    [Description] nvarchar(50)  NOT NULL,
    [TaxTracking] nvarchar(10)  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [DEType] nvarchar(1)  NOT NULL,
    [IsStatutory] bit  NOT NULL,
    [ShowInPayslip] bit  NULL,
    [YTD] decimal(18,0)  NULL,
    [IsLoanRepaymentInCash] bit  NULL,
    [IsSharesRepaymentInCash] bit  NULL
);
GO

-- Creating table 'PayslipDet_Temp'
CREATE TABLE [dbo].[PayslipDet_Temp] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EmpNo] nvarchar(250)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [EmployerId] int  NULL,
    [EmpTxnId] int  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NULL,
    [Description] nvarchar(50)  NOT NULL,
    [TaxTracking] nchar(10)  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [DEType] nvarchar(1)  NOT NULL,
    [IsStatutory] bit  NOT NULL,
    [ShowInPayslip] bit  NULL,
    [YTD] decimal(18,0)  NULL,
    [IsLoanRepaymentInCash] bit  NULL,
    [IsSharesRepaymentInCash] bit  NULL
);
GO

-- Creating table 'PayslipMasters'
CREATE TABLE [dbo].[PayslipMasters] (
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [EmpNo] nvarchar(250)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [EmployerId] int  NULL,
    [PaymentDate] datetime  NOT NULL,
    [PrintedBy] nvarchar(100)  NOT NULL,
    [PrintedOn] datetime  NOT NULL,
    [EmpName] nvarchar(100)  NOT NULL,
    [PayPoint] nvarchar(100)  NULL,
    [PIN] nvarchar(100)  NULL,
    [NHIFNo] nvarchar(100)  NULL,
    [NSSFNo] nvarchar(100)  NULL,
    [Department] nvarchar(100)  NULL,
    [EmpGroup] nvarchar(100)  NULL,
    [EmpPayroll] nvarchar(100)  NULL,
    [CompName] nvarchar(100)  NULL,
    [CompAddr] nvarchar(100)  NULL,
    [CompTel] nvarchar(100)  NULL,
    [PayeTax] decimal(19,4)  NOT NULL,
    [BasicPay] decimal(19,4)  NOT NULL,
    [Benefits] decimal(19,4)  NOT NULL,
    [Variables] decimal(19,4)  NOT NULL,
    [OtherDeductions] decimal(19,4)  NOT NULL,
    [GrossTaxableEarnings] decimal(19,4)  NOT NULL,
    [NetTaxableEarnings] decimal(19,4)  NOT NULL,
    [MortgageRelief] decimal(19,4)  NOT NULL,
    [InsuranceRelief] decimal(19,4)  NOT NULL,
    [GrossTax] decimal(19,4)  NOT NULL,
    [PersonalRelief] decimal(19,4)  NOT NULL,
    [PensionEmployer] decimal(19,4)  NOT NULL,
    [EmployerNSSF] decimal(19,4)  NOT NULL,
    [PensionEmployee] decimal(19,4)  NOT NULL,
    [BankBranch] nvarchar(100)  NULL,
    [Account] nvarchar(100)  NULL,
    [NSSF] decimal(19,4)  NOT NULL,
    [NHIF] decimal(19,4)  NOT NULL,
    [NetPay] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'PayslipMaster_Temp'
CREATE TABLE [dbo].[PayslipMaster_Temp] (
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [EmpNo] nvarchar(250)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [EmployerId] int  NULL,
    [PaymentDate] datetime  NOT NULL,
    [PrintedBy] nvarchar(100)  NOT NULL,
    [PrintedOn] datetime  NOT NULL,
    [EmpName] nvarchar(100)  NOT NULL,
    [PayPoint] nvarchar(100)  NULL,
    [PIN] nvarchar(100)  NULL,
    [NHIFNo] nvarchar(100)  NULL,
    [NSSFNo] nvarchar(100)  NULL,
    [Department] nvarchar(100)  NULL,
    [EmpGroup] nvarchar(100)  NULL,
    [EmpPayroll] nvarchar(100)  NULL,
    [CompName] nvarchar(100)  NULL,
    [CompAddr] nvarchar(100)  NULL,
    [CompTel] nvarchar(100)  NULL,
    [PayeTax] decimal(19,4)  NOT NULL,
    [BasicPay] decimal(19,4)  NOT NULL,
    [Benefits] decimal(19,4)  NOT NULL,
    [Variables] decimal(19,4)  NOT NULL,
    [OtherDeductions] decimal(19,4)  NOT NULL,
    [GrossTaxableEarnings] decimal(19,4)  NOT NULL,
    [NetTaxableEarnings] decimal(19,4)  NOT NULL,
    [MortgageRelief] decimal(19,4)  NOT NULL,
    [InsuranceRelief] decimal(19,4)  NOT NULL,
    [GrossTax] decimal(19,4)  NOT NULL,
    [PersonalRelief] decimal(19,4)  NOT NULL,
    [PensionEmployer] decimal(19,4)  NOT NULL,
    [EmployerNSSF] decimal(19,4)  NOT NULL,
    [PensionEmployee] decimal(19,4)  NOT NULL,
    [BankBranch] nvarchar(100)  NULL,
    [Account] nvarchar(100)  NULL,
    [NSSF] decimal(19,4)  NOT NULL,
    [NHIF] decimal(19,4)  NOT NULL,
    [NetPay] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'Settings'
CREATE TABLE [dbo].[Settings] (
    [SSKey] nvarchar(50)  NOT NULL,
    [SSValue] nvarchar(max)  NOT NULL,
    [SSValueType] nvarchar(50)  NOT NULL,
    [Description] nvarchar(500)  NOT NULL,
    [SGroup] int  NOT NULL,
    [SSSystem] bit  NOT NULL
);
GO

-- Creating table 'SettingsGroups'
CREATE TABLE [dbo].[SettingsGroups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupName] nvarchar(50)  NOT NULL,
    [Parent] int  NOT NULL
);
GO

-- Creating table 'spAllowedReportsRolesMenus'
CREATE TABLE [dbo].[spAllowedReportsRolesMenus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NOT NULL,
    [MenuItemId] int  NOT NULL,
    [Allowed] bit  NOT NULL
);
GO

-- Creating table 'spAllowedRoleMenus'
CREATE TABLE [dbo].[spAllowedRoleMenus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NOT NULL,
    [MenuItemId] int  NOT NULL,
    [Allowed] bit  NOT NULL
);
GO

-- Creating table 'spAllowedRoleMenuswebs'
CREATE TABLE [dbo].[spAllowedRoleMenuswebs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NULL,
    [MenuItemId] int  NULL,
    [Allowed] bit  NULL
);
GO

-- Creating table 'spMenuItems'
CREATE TABLE [dbo].[spMenuItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [mnuName] nvarchar(100)  NOT NULL,
    [Description] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'spMenus'
CREATE TABLE [dbo].[spMenus] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [mnuName] nvarchar(100)  NULL,
    [Description] nvarchar(100)  NULL
);
GO

-- Creating table 'spReportsMenuItems'
CREATE TABLE [dbo].[spReportsMenuItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [mnuName] nvarchar(100)  NOT NULL,
    [Description] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'spRoles'
CREATE TABLE [dbo].[spRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ShortCode] nvarchar(50)  NOT NULL,
    [Description] nvarchar(100)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'spUsers'
CREATE TABLE [dbo].[spUsers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleId] int  NOT NULL,
    [UserName] nvarchar(250)  NOT NULL,
    [Password] nvarchar(250)  NOT NULL,
    [Status] nvarchar(1)  NOT NULL,
    [Locked] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [SystemId] nvarchar(250)  NOT NULL,
    [Surname] nvarchar(200)  NULL,
    [OtherNames] nvarchar(200)  NULL,
    [NationalID] nvarchar(200)  NULL,
    [DateOfBirth] datetime  NULL,
    [Gender] nvarchar(50)  NULL,
    [Telephone] nvarchar(200)  NULL,
    [Email] nvarchar(200)  NULL,
    [DateJoined] datetime  NULL,
    [InformBy] nvarchar(200)  NULL,
    [Photo] nvarchar(max)  NULL
);
GO

-- Creating table 'spUsersInRoles'
CREATE TABLE [dbo].[spUsersInRoles] (
    [UserId] int  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TaxTrackings'
CREATE TABLE [dbo].[TaxTrackings] (
    [Id] nvarchar(10)  NOT NULL,
    [Description] nvarchar(50)  NULL,
    [Status] nchar(10)  NULL,
    [IsDefault] bit  NULL
);
GO

-- Creating table 'TechParams'
CREATE TABLE [dbo].[TechParams] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [strtdt] datetime  NULL,
    [edc] int  NULL
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
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostDate] datetime  NULL,
    [Amount] decimal(19,4)  NULL,
    [DRCR] nchar(10)  NULL,
    [Narrative] nvarchar(50)  NULL,
    [TxnType] int  NULL
);
GO

-- Creating table 'TxnTypes'
CREATE TABLE [dbo].[TxnTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'UserProfiles'
CREATE TABLE [dbo].[UserProfiles] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NULL
);
GO

-- Creating table 'webpages_Membership'
CREATE TABLE [dbo].[webpages_Membership] (
    [UserId] int  NOT NULL,
    [CreateDate] datetime  NULL,
    [ConfirmationToken] nvarchar(128)  NULL,
    [IsConfirmed] bit  NULL,
    [LastPasswordFailureDate] datetime  NULL,
    [PasswordFailuresSinceLastSuccess] int  NOT NULL,
    [Password] nvarchar(128)  NOT NULL,
    [PasswordChangedDate] datetime  NULL,
    [PasswordSalt] nvarchar(128)  NOT NULL,
    [PasswordVerificationToken] nvarchar(128)  NULL,
    [PasswordVerificationTokenExpirationDate] datetime  NULL
);
GO

-- Creating table 'webpages_OAuthMembership'
CREATE TABLE [dbo].[webpages_OAuthMembership] (
    [Provider] nvarchar(30)  NOT NULL,
    [ProviderUserId] nvarchar(100)  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'webpages_Roles'
CREATE TABLE [dbo].[webpages_Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'webpages_UsersInRoles'
CREATE TABLE [dbo].[webpages_UsersInRoles] (
    [UserId] int  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'GetEmpTransactions'
CREATE TABLE [dbo].[GetEmpTransactions] (
    [Surname] nvarchar(250)  NULL,
    [MaritalStatus] nvarchar(1)  NULL,
    [IsActive] bit  NULL,
    [ItemId] nvarchar(150)  NOT NULL,
    [Amount] decimal(19,4)  NOT NULL,
    [Recurrent] bit  NOT NULL,
    [Enabled] bit  NOT NULL,
    [BasicPay] decimal(19,4)  NULL,
    [Department] nvarchar(50)  NULL,
    [DeptCode] nvarchar(2)  NULL,
    [EmpTrack] bit  NULL,
    [Balance] decimal(19,4)  NULL,
    [ItemTypeId] nvarchar(10)  NULL,
    [TaxTrackingId] nvarchar(10)  NULL,
    [Active] bit  NULL,
    [AddToPension] bit  NULL,
    [ShowYTDInPayslip] bit  NULL,
    [EmpTxnId] int  NOT NULL,
    [Processed] bit  NULL,
    [Parent] nvarchar(50)  NULL,
    [EmpNo] nvarchar(250)  NULL,
    [EmployeeId] int  NOT NULL,
    [OtherNames] nvarchar(250)  NULL,
    [DoB] datetime  NULL,
    [Gender] nvarchar(1)  NULL,
    [Photo] nvarchar(max)  NULL,
    [IsDeleted] bit  NULL,
    [PostDate] datetime  NOT NULL,
    [Enable] bit  NULL,
    [SystemId] nvarchar(250)  NULL,
    [IsLoanRepaymentInFull] bit  NULL,
    [LoanBeingRepayedId] int  NULL,
    [SaccoBeingRepayedId] int  NULL,
    [IsLoanRepaymentInCash] bit  NULL,
    [IsSharesRepaymentInCash] bit  NULL
);
GO

-- Creating table 'vwBankBranches'
CREATE TABLE [dbo].[vwBankBranches] (
    [BankBranchName] nvarchar(303)  NOT NULL,
    [BankSortCode] nvarchar(50)  NOT NULL,
    [BankName] nvarchar(150)  NOT NULL,
    [BankCode] nvarchar(50)  NOT NULL,
    [BranchCode] nvarchar(50)  NOT NULL,
    [BranchName] nvarchar(150)  NOT NULL,
    [Expr1] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'vwPayrollMasters'
CREATE TABLE [dbo].[vwPayrollMasters] (
    [BankName] nvarchar(150)  NOT NULL,
    [BranchName] nvarchar(150)  NOT NULL,
    [Surname] nvarchar(250)  NULL,
    [OtherNames] nvarchar(250)  NULL,
    [BranchCode] nvarchar(50)  NOT NULL,
    [BankSortCode] nvarchar(50)  NOT NULL,
    [PaymentMode] nvarchar(250)  NULL,
    [BankBranchSortCode] nvarchar(250)  NULL,
    [BankAccount] nvarchar(250)  NULL,
    [IDNo] nvarchar(250)  NULL,
    [BankCode] nvarchar(50)  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [PrintedBy] nvarchar(100)  NOT NULL,
    [PrintedOn] datetime  NOT NULL,
    [EmpName] nvarchar(100)  NOT NULL,
    [PayPoint] nvarchar(100)  NULL,
    [PIN] nvarchar(100)  NULL,
    [NHIFNo] nvarchar(100)  NULL,
    [Department] nvarchar(100)  NULL,
    [NSSFNo] nvarchar(100)  NULL,
    [EmpGroup] nvarchar(100)  NULL,
    [EmpPayroll] nvarchar(100)  NULL,
    [CompName] nvarchar(100)  NULL,
    [CompAddr] nvarchar(100)  NULL,
    [CompTel] nvarchar(100)  NULL,
    [PayeTax] decimal(19,4)  NOT NULL,
    [BasicPay] decimal(19,4)  NOT NULL,
    [Benefits] decimal(19,4)  NOT NULL,
    [OtherDeductions] decimal(19,4)  NOT NULL,
    [GrossTaxableEarnings] decimal(19,4)  NOT NULL,
    [NetTaxableEarnings] decimal(19,4)  NOT NULL,
    [GrossTax] decimal(19,4)  NOT NULL,
    [EmployerNSSF] decimal(19,4)  NOT NULL,
    [PensionEmployee] decimal(19,4)  NOT NULL,
    [BankBranch] nvarchar(100)  NULL,
    [Account] nvarchar(100)  NULL,
    [NSSF] decimal(19,4)  NOT NULL,
    [NHIF] decimal(19,4)  NOT NULL,
    [NetPay] decimal(19,4)  NOT NULL,
    [EmpNo] nvarchar(250)  NULL,
    [Variables] decimal(19,4)  NOT NULL,
    [MortgageRelief] decimal(19,4)  NOT NULL,
    [PersonalRelief] decimal(19,4)  NOT NULL,
    [PensionEmployer] decimal(19,4)  NOT NULL,
    [InsuranceRelief] decimal(19,4)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [EmployerId] int  NOT NULL
);
GO

-- Creating table 'vwPayrollMaster_Temp'
CREATE TABLE [dbo].[vwPayrollMaster_Temp] (
    [BankName] nvarchar(150)  NULL,
    [BranchName] nvarchar(150)  NULL,
    [Surname] nvarchar(250)  NULL,
    [OtherNames] nvarchar(250)  NULL,
    [BranchCode] nvarchar(50)  NULL,
    [BankSortCode] nvarchar(50)  NULL,
    [PaymentMode] nvarchar(250)  NULL,
    [BankBranchSortCode] nvarchar(250)  NULL,
    [BankAccount] nvarchar(250)  NULL,
    [IDNo] nvarchar(250)  NULL,
    [BankCode] nvarchar(50)  NULL,
    [Period] int  NOT NULL,
    [Year] int  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [PrintedBy] nvarchar(100)  NOT NULL,
    [PrintedOn] datetime  NOT NULL,
    [EmpName] nvarchar(100)  NOT NULL,
    [PayPoint] nvarchar(100)  NULL,
    [PIN] nvarchar(100)  NULL,
    [NHIFNo] nvarchar(100)  NULL,
    [Department] nvarchar(100)  NULL,
    [NSSFNo] nvarchar(100)  NULL,
    [EmpGroup] nvarchar(100)  NULL,
    [EmpPayroll] nvarchar(100)  NULL,
    [CompName] nvarchar(100)  NULL,
    [CompAddr] nvarchar(100)  NULL,
    [CompTel] nvarchar(100)  NULL,
    [PayeTax] decimal(19,4)  NOT NULL,
    [BasicPay] decimal(19,4)  NOT NULL,
    [Benefits] decimal(19,4)  NOT NULL,
    [OtherDeductions] decimal(19,4)  NOT NULL,
    [GrossTaxableEarnings] decimal(19,4)  NOT NULL,
    [NetTaxableEarnings] decimal(19,4)  NOT NULL,
    [GrossTax] decimal(19,4)  NOT NULL,
    [EmployerNSSF] decimal(19,4)  NOT NULL,
    [PensionEmployee] decimal(19,4)  NOT NULL,
    [BankBranch] nvarchar(100)  NULL,
    [Account] nvarchar(100)  NULL,
    [NSSF] decimal(19,4)  NOT NULL,
    [NHIF] decimal(19,4)  NOT NULL,
    [NetPay] decimal(19,4)  NOT NULL,
    [EmpNo] nvarchar(250)  NULL,
    [Variables] decimal(19,4)  NOT NULL,
    [MortgageRelief] decimal(19,4)  NOT NULL,
    [PersonalRelief] decimal(19,4)  NOT NULL,
    [PensionEmployer] decimal(19,4)  NOT NULL,
    [InsuranceRelief] decimal(19,4)  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [EmployerId] int  NOT NULL
);
GO

-- Creating table 'vwPayslipDets'
CREATE TABLE [dbo].[vwPayslipDets] (
    [Id] int  NOT NULL,
    [EmpNo] nvarchar(250)  NULL,
    [Surname] nvarchar(250)  NULL,
    [OtherNames] nvarchar(250)  NULL,
    [ItemId] nvarchar(150)  NOT NULL,
    [Balance] decimal(19,4)  NULL,
    [ReFField] int  NULL,
    [RepayAmount] decimal(19,4)  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NULL,
    [TxnDate] datetime  NULL,
    [PostDate] datetime  NOT NULL,
    [Description] nvarchar(50)  NOT NULL,
    [YTD] decimal(18,0)  NULL,
    [InitialAmount] decimal(19,4)  NULL,
    [LoanType] nvarchar(250)  NULL,
    [BankAccount] nvarchar(250)  NULL,
    [BankCode] nvarchar(250)  NULL,
    [PaymentMode] nvarchar(250)  NULL,
    [ItemTypeId] nvarchar(10)  NULL,
    [DEType] nvarchar(1)  NOT NULL,
    [EmpTxnId] int  NOT NULL,
    [TaxTracking] nvarchar(10)  NOT NULL,
    [ShowInPayslip] bit  NULL,
    [IsStatutory] bit  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [Parent] nvarchar(50)  NULL,
    [EmployeeId] int  NOT NULL,
    [IsDeleted] bit  NULL,
    [IsActive] bit  NULL,
    [EmployerId] int  NOT NULL,
    [SystemId] nvarchar(250)  NULL,
    [AccumulativePayment] decimal(19,4)  NULL,
    [IsLoanRepaymentInFull] bit  NULL,
    [IsSharesRepaymentInCash] bit  NULL,
    [IsLoanRepaymentInCash] bit  NULL
);
GO

-- Creating table 'vwPayslipDet_Temp'
CREATE TABLE [dbo].[vwPayslipDet_Temp] (
    [EmployeeId] int  NOT NULL,
    [EmpNo] nvarchar(250)  NULL,
    [Surname] nvarchar(250)  NULL,
    [OtherNames] nvarchar(250)  NULL,
    [ItemId] nvarchar(150)  NOT NULL,
    [Balance] decimal(19,4)  NULL,
    [TxnDate] datetime  NULL,
    [ReFField] int  NULL,
    [RepayAmount] decimal(19,4)  NOT NULL,
    [PostDate] datetime  NOT NULL,
    [InitialAmount] decimal(19,4)  NULL,
    [LoanType] nvarchar(250)  NULL,
    [BankAccount] nvarchar(250)  NULL,
    [BankCode] nvarchar(250)  NULL,
    [PaymentMode] nvarchar(250)  NULL,
    [ItemTypeId] nvarchar(10)  NULL,
    [Parent] nvarchar(50)  NULL,
    [EmpTxnId] int  NOT NULL,
    [Period] int  NOT NULL,
    [Year] int  NULL,
    [Description] nvarchar(50)  NOT NULL,
    [TaxTracking] nchar(10)  NOT NULL,
    [Amount] decimal(18,0)  NOT NULL,
    [DEType] nvarchar(1)  NOT NULL,
    [IsStatutory] bit  NOT NULL,
    [ShowInPayslip] bit  NULL,
    [YTD] decimal(18,0)  NULL,
    [IsActive] bit  NULL,
    [IsDeleted] bit  NULL,
    [EmployerId] int  NOT NULL,
    [SystemId] nvarchar(250)  NULL,
    [AccumulativePayment] decimal(19,4)  NULL,
    [IsLoanRepaymentInFull] bit  NULL,
    [IsSharesRepaymentInCash] bit  NULL,
    [IsLoanRepaymentInCash] bit  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
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

-- Creating primary key on [Id] in table 'Benefits'
ALTER TABLE [dbo].[Benefits]
ADD CONSTRAINT [PK_Benefits]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeductionsTrackers'
ALTER TABLE [dbo].[DeductionsTrackers]
ADD CONSTRAINT [PK_DeductionsTrackers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [EmpNo], [ExFieldName] in table 'Employee_Ext'
ALTER TABLE [dbo].[Employee_Ext]
ADD CONSTRAINT [PK_Employee_Ext]
    PRIMARY KEY CLUSTERED ([EmpNo], [ExFieldName] ASC);
GO

-- Creating primary key on [Id] in table 'Employee_Ext_Fields'
ALTER TABLE [dbo].[Employee_Ext_Fields]
ADD CONSTRAINT [PK_Employee_Ext_Fields]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeesBankTransfers'
ALTER TABLE [dbo].[EmployeesBankTransfers]
ADD CONSTRAINT [PK_EmployeesBankTransfers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeTransactions'
ALTER TABLE [dbo].[EmployeeTransactions]
ADD CONSTRAINT [PK_EmployeeTransactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployerBanks'
ALTER TABLE [dbo].[EmployerBanks]
ADD CONSTRAINT [PK_EmployerBanks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employers'
ALTER TABLE [dbo].[Employers]
ADD CONSTRAINT [PK_Employers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [EmpNo], [BenefitId] in table 'EmpNonCashBenefits'
ALTER TABLE [dbo].[EmpNonCashBenefits]
ADD CONSTRAINT [PK_EmpNonCashBenefits]
    PRIMARY KEY CLUSTERED ([EmpNo], [BenefitId] ASC);
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

-- Creating primary key on [Id] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [PK_PayrollItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PayrollItemTypes'
ALTER TABLE [dbo].[PayrollItemTypes]
ADD CONSTRAINT [PK_PayrollItemTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Period], [Year], [EmployerId] in table 'Payrolls'
ALTER TABLE [dbo].[Payrolls]
ADD CONSTRAINT [PK_Payrolls]
    PRIMARY KEY CLUSTERED ([Period], [Year], [EmployerId] ASC);
GO

-- Creating primary key on [Id] in table 'PayrollTypes'
ALTER TABLE [dbo].[PayrollTypes]
ADD CONSTRAINT [PK_PayrollTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
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

-- Creating primary key on [Id] in table 'spAllowedReportsRolesMenus'
ALTER TABLE [dbo].[spAllowedReportsRolesMenus]
ADD CONSTRAINT [PK_spAllowedReportsRolesMenus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'spAllowedRoleMenus'
ALTER TABLE [dbo].[spAllowedRoleMenus]
ADD CONSTRAINT [PK_spAllowedRoleMenus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'spAllowedRoleMenuswebs'
ALTER TABLE [dbo].[spAllowedRoleMenuswebs]
ADD CONSTRAINT [PK_spAllowedRoleMenuswebs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'spMenuItems'
ALTER TABLE [dbo].[spMenuItems]
ADD CONSTRAINT [PK_spMenuItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'spMenus'
ALTER TABLE [dbo].[spMenus]
ADD CONSTRAINT [PK_spMenus]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'spReportsMenuItems'
ALTER TABLE [dbo].[spReportsMenuItems]
ADD CONSTRAINT [PK_spReportsMenuItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'spRoles'
ALTER TABLE [dbo].[spRoles]
ADD CONSTRAINT [PK_spRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'spUsers'
ALTER TABLE [dbo].[spUsers]
ADD CONSTRAINT [PK_spUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId], [RoleId] in table 'spUsersInRoles'
ALTER TABLE [dbo].[spUsersInRoles]
ADD CONSTRAINT [PK_spUsersInRoles]
    PRIMARY KEY CLUSTERED ([UserId], [RoleId] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [Id] in table 'TaxTrackings'
ALTER TABLE [dbo].[TaxTrackings]
ADD CONSTRAINT [PK_TaxTrackings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TechParams'
ALTER TABLE [dbo].[TechParams]
ADD CONSTRAINT [PK_TechParams]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [TxnCode] in table 'TransactionDefs'
ALTER TABLE [dbo].[TransactionDefs]
ADD CONSTRAINT [PK_TransactionDefs]
    PRIMARY KEY CLUSTERED ([TxnCode] ASC);
GO

-- Creating primary key on [Id] in table 'Transactions'
ALTER TABLE [dbo].[Transactions]
ADD CONSTRAINT [PK_Transactions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TxnTypes'
ALTER TABLE [dbo].[TxnTypes]
ADD CONSTRAINT [PK_TxnTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserId] in table 'UserProfiles'
ALTER TABLE [dbo].[UserProfiles]
ADD CONSTRAINT [PK_UserProfiles]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserId] in table 'webpages_Membership'
ALTER TABLE [dbo].[webpages_Membership]
ADD CONSTRAINT [PK_webpages_Membership]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Provider], [ProviderUserId] in table 'webpages_OAuthMembership'
ALTER TABLE [dbo].[webpages_OAuthMembership]
ADD CONSTRAINT [PK_webpages_OAuthMembership]
    PRIMARY KEY CLUSTERED ([Provider], [ProviderUserId] ASC);
GO

-- Creating primary key on [RoleId] in table 'webpages_Roles'
ALTER TABLE [dbo].[webpages_Roles]
ADD CONSTRAINT [PK_webpages_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [UserId], [RoleId] in table 'webpages_UsersInRoles'
ALTER TABLE [dbo].[webpages_UsersInRoles]
ADD CONSTRAINT [PK_webpages_UsersInRoles]
    PRIMARY KEY CLUSTERED ([UserId], [RoleId] ASC);
GO

-- Creating primary key on [ItemId], [Amount], [Recurrent], [Enabled], [EmpTxnId], [EmployeeId], [PostDate] in table 'GetEmpTransactions'
ALTER TABLE [dbo].[GetEmpTransactions]
ADD CONSTRAINT [PK_GetEmpTransactions]
    PRIMARY KEY CLUSTERED ([ItemId], [Amount], [Recurrent], [Enabled], [EmpTxnId], [EmployeeId], [PostDate] ASC);
GO

-- Creating primary key on [BankBranchName], [BankSortCode], [BankName], [BankCode], [BranchCode], [BranchName], [Expr1] in table 'vwBankBranches'
ALTER TABLE [dbo].[vwBankBranches]
ADD CONSTRAINT [PK_vwBankBranches]
    PRIMARY KEY CLUSTERED ([BankBranchName], [BankSortCode], [BankName], [BankCode], [BranchCode], [BranchName], [Expr1] ASC);
GO

-- Creating primary key on [BankName], [BranchName], [BranchCode], [BankSortCode], [BankCode], [Period], [Year], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayeTax], [BasicPay], [Benefits], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [GrossTax], [EmployerNSSF], [PensionEmployee], [NSSF], [NHIF], [NetPay], [Variables], [MortgageRelief], [PersonalRelief], [PensionEmployer], [InsuranceRelief], [EmployeeId], [EmployerId] in table 'vwPayrollMasters'
ALTER TABLE [dbo].[vwPayrollMasters]
ADD CONSTRAINT [PK_vwPayrollMasters]
    PRIMARY KEY CLUSTERED ([BankName], [BranchName], [BranchCode], [BankSortCode], [BankCode], [Period], [Year], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayeTax], [BasicPay], [Benefits], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [GrossTax], [EmployerNSSF], [PensionEmployee], [NSSF], [NHIF], [NetPay], [Variables], [MortgageRelief], [PersonalRelief], [PensionEmployer], [InsuranceRelief], [EmployeeId], [EmployerId] ASC);
GO

-- Creating primary key on [Period], [Year], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayeTax], [BasicPay], [Benefits], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [GrossTax], [EmployerNSSF], [PensionEmployee], [NSSF], [NHIF], [NetPay], [Variables], [MortgageRelief], [PersonalRelief], [PensionEmployer], [InsuranceRelief], [EmployeeId], [EmployerId] in table 'vwPayrollMaster_Temp'
ALTER TABLE [dbo].[vwPayrollMaster_Temp]
ADD CONSTRAINT [PK_vwPayrollMaster_Temp]
    PRIMARY KEY CLUSTERED ([Period], [Year], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayeTax], [BasicPay], [Benefits], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [GrossTax], [EmployerNSSF], [PensionEmployee], [NSSF], [NHIF], [NetPay], [Variables], [MortgageRelief], [PersonalRelief], [PensionEmployer], [InsuranceRelief], [EmployeeId], [EmployerId] ASC);
GO

-- Creating primary key on [Id], [ItemId], [RepayAmount], [Period], [PostDate], [Description], [DEType], [EmpTxnId], [TaxTracking], [IsStatutory], [Amount], [EmployeeId], [EmployerId] in table 'vwPayslipDets'
ALTER TABLE [dbo].[vwPayslipDets]
ADD CONSTRAINT [PK_vwPayslipDets]
    PRIMARY KEY CLUSTERED ([Id], [ItemId], [RepayAmount], [Period], [PostDate], [Description], [DEType], [EmpTxnId], [TaxTracking], [IsStatutory], [Amount], [EmployeeId], [EmployerId] ASC);
GO

-- Creating primary key on [EmployeeId], [ItemId], [RepayAmount], [PostDate], [EmpTxnId], [Period], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [EmployerId] in table 'vwPayslipDet_Temp'
ALTER TABLE [dbo].[vwPayslipDet_Temp]
ADD CONSTRAINT [PK_vwPayslipDet_Temp]
    PRIMARY KEY CLUSTERED ([EmployeeId], [ItemId], [RepayAmount], [PostDate], [EmpTxnId], [Period], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [EmployerId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BankCode] in table 'BankBranches'
ALTER TABLE [dbo].[BankBranches]
ADD CONSTRAINT [FK_BankBranch_Banks]
    FOREIGN KEY ([BankCode])
    REFERENCES [dbo].[Banks]
        ([BankCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BankBranch_Banks'
CREATE INDEX [IX_FK_BankBranch_Banks]
ON [dbo].[BankBranches]
    ([BankCode]);
GO

-- Creating foreign key on [EmployerId] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_Employees_Employers]
    FOREIGN KEY ([EmployerId])
    REFERENCES [dbo].[Employers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Employees_Employers'
CREATE INDEX [IX_FK_Employees_Employers]
ON [dbo].[Employees]
    ([EmployerId]);
GO

-- Creating foreign key on [TaxTrackingId] in table 'PayrollItems'
ALTER TABLE [dbo].[PayrollItems]
ADD CONSTRAINT [FK_PayrollItems_TaxTracking]
    FOREIGN KEY ([TaxTrackingId])
    REFERENCES [dbo].[TaxTrackings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayrollItems_TaxTracking'
CREATE INDEX [IX_FK_PayrollItems_TaxTracking]
ON [dbo].[PayrollItems]
    ([TaxTrackingId]);
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

-- Creating foreign key on [MenuItemId] in table 'spAllowedReportsRolesMenus'
ALTER TABLE [dbo].[spAllowedReportsRolesMenus]
ADD CONSTRAINT [FK_spAllowedReportsRolesMenus_spReportsMenuItems]
    FOREIGN KEY ([MenuItemId])
    REFERENCES [dbo].[spReportsMenuItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_spAllowedReportsRolesMenus_spReportsMenuItems'
CREATE INDEX [IX_FK_spAllowedReportsRolesMenus_spReportsMenuItems]
ON [dbo].[spAllowedReportsRolesMenus]
    ([MenuItemId]);
GO

-- Creating foreign key on [MenuItemId] in table 'spAllowedRoleMenus'
ALTER TABLE [dbo].[spAllowedRoleMenus]
ADD CONSTRAINT [FK_spAllowedRoleMenus_spMenuItems]
    FOREIGN KEY ([MenuItemId])
    REFERENCES [dbo].[spMenuItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_spAllowedRoleMenus_spMenuItems'
CREATE INDEX [IX_FK_spAllowedRoleMenus_spMenuItems]
ON [dbo].[spAllowedRoleMenus]
    ([MenuItemId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------