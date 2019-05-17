/****** Object:  ForeignKey [FK_BankBranch_Banks]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BankBranch_Banks]') AND parent_object_id = OBJECT_ID(N'[dbo].[BankBranch]'))
ALTER TABLE [dbo].[BankBranch] DROP CONSTRAINT [FK_BankBranch_Banks]
GO
/****** Object:  ForeignKey [FK_PayrollItems_PayrollItemType]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayrollItems_PayrollItemType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayrollItems]'))
ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_PayrollItems_PayrollItemType]
GO
/****** Object:  ForeignKey [FK_PayslipDet_TaxTracking]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayslipDet_TaxTracking]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayslipDet]'))
ALTER TABLE [dbo].[PayslipDet] DROP CONSTRAINT [FK_PayslipDet_TaxTracking]
GO
/****** Object:  ForeignKey [FK_Settings_SettingsGroup]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Settings_SettingsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Settings]'))
ALTER TABLE [dbo].[Settings] DROP CONSTRAINT [FK_Settings_SettingsGroup]
GO
/****** Object:  StoredProcedure [dbo].[CopyPayslipDet]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyPayslipDet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CopyPayslipDet]
GO
/****** Object:  View [dbo].[GetEmpTransactions]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetEmpTransactions]'))
DROP VIEW [dbo].[GetEmpTransactions]
GO
/****** Object:  View [dbo].[vwBankBranches]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwBankBranches]'))
DROP VIEW [dbo].[vwBankBranches]
GO
/****** Object:  View [dbo].[vwPayrollMaster]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayrollMaster]'))
DROP VIEW [dbo].[vwPayrollMaster]
GO
/****** Object:  View [dbo].[vwPayrollMaster_Temp]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayrollMaster_Temp]'))
DROP VIEW [dbo].[vwPayrollMaster_Temp]
GO
/****** Object:  View [dbo].[vwPayslipDet]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayslipDet]'))
DROP VIEW [dbo].[vwPayslipDet]
GO
/****** Object:  View [dbo].[vwPayslipDet_Temp]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayslipDet_Temp]'))
DROP VIEW [dbo].[vwPayslipDet_Temp]
GO
/****** Object:  View [dbo].[vwEmpNonCashBenefits]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwEmpNonCashBenefits]'))
DROP VIEW [dbo].[vwEmpNonCashBenefits]
GO
/****** Object:  StoredProcedure [dbo].[CopyPayMaster]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyPayMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CopyPayMaster]
GO
/****** Object:  Table [dbo].[PayslipDet]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipDet]') AND type in (N'U'))
DROP TABLE [dbo].[PayslipDet]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
DROP TABLE [dbo].[Settings]
GO
/****** Object:  Table [dbo].[BankBranch]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankBranch]') AND type in (N'U'))
DROP TABLE [dbo].[BankBranch]
GO
/****** Object:  Table [dbo].[PayrollItems]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollItems]') AND type in (N'U'))
DROP TABLE [dbo].[PayrollItems]
GO
/****** Object:  Table [dbo].[PayrollItemType]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollItemType]') AND type in (N'U'))
DROP TABLE [dbo].[PayrollItemType]
GO
/****** Object:  Table [dbo].[Payrolls]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payrolls]') AND type in (N'U'))
DROP TABLE [dbo].[Payrolls]
GO
/****** Object:  Table [dbo].[Banks]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Banks]') AND type in (N'U'))
DROP TABLE [dbo].[Banks]
GO
/****** Object:  Table [dbo].[Benefits]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Benefits]') AND type in (N'U'))
DROP TABLE [dbo].[Benefits]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Departments]') AND type in (N'U'))
DROP TABLE [dbo].[Departments]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
DROP TABLE [dbo].[Employee]
GO
/****** Object:  Table [dbo].[Employee_Ext]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee_Ext]') AND type in (N'U'))
DROP TABLE [dbo].[Employee_Ext]
GO
/****** Object:  Table [dbo].[Employee_Ext_Fields]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee_Ext_Fields]') AND type in (N'U'))
DROP TABLE [dbo].[Employee_Ext_Fields]
GO
/****** Object:  Table [dbo].[EmployeeTransactions]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeTransactions]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeeTransactions]
GO
/****** Object:  Table [dbo].[Employer]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employer]') AND type in (N'U'))
DROP TABLE [dbo].[Employer]
GO
/****** Object:  Table [dbo].[EmpNonCashBenefits]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpNonCashBenefits]') AND type in (N'U'))
DROP TABLE [dbo].[EmpNonCashBenefits]
GO
/****** Object:  Table [dbo].[SettingsGroup]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingsGroup]') AND type in (N'U'))
DROP TABLE [dbo].[SettingsGroup]
GO
/****** Object:  Table [dbo].[spUsers]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spUsers]') AND type in (N'U'))
DROP TABLE [dbo].[spUsers]
GO
/****** Object:  Table [dbo].[TaxTracking]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaxTracking]') AND type in (N'U'))
DROP TABLE [dbo].[TaxTracking]
GO
/****** Object:  Table [dbo].[TransactionDef]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransactionDef]') AND type in (N'U'))
DROP TABLE [dbo].[TransactionDef]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
DROP TABLE [dbo].[Transactions]
GO
/****** Object:  Table [dbo].[TxnType]    Script Date: 12/04/2012 17:55:46 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TxnType]') AND type in (N'U'))
DROP TABLE [dbo].[TxnType]
GO
/****** Object:  Table [dbo].[PayslipDet_Temp]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipDet_Temp]') AND type in (N'U'))
DROP TABLE [dbo].[PayslipDet_Temp]
GO
/****** Object:  Table [dbo].[PayslipMaster]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PayslipMaster]
GO
/****** Object:  Table [dbo].[PayslipMaster_Temp]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipMaster_Temp]') AND type in (N'U'))
DROP TABLE [dbo].[PayslipMaster_Temp]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))
DROP TABLE [dbo].[Accounts]
GO
/****** Object:  Table [dbo].[HourlyPayment]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HourlyPayment]') AND type in (N'U'))
DROP TABLE [dbo].[HourlyPayment]
GO
/****** Object:  Table [dbo].[LeaveTransactions]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LeaveTransactions]') AND type in (N'U'))
DROP TABLE [dbo].[LeaveTransactions]
GO
/****** Object:  Table [dbo].[NHIFRates]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NHIFRates]') AND type in (N'U'))
DROP TABLE [dbo].[NHIFRates]
GO
/****** Object:  Table [dbo].[PackedTransactions]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PackedTransactions]') AND type in (N'U'))
DROP TABLE [dbo].[PackedTransactions]
GO
/****** Object:  Table [dbo].[PayeeRates]    Script Date: 12/04/2012 17:55:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayeeRates]') AND type in (N'U'))
DROP TABLE [dbo].[PayeeRates]
GO
/****** Object:  Table [dbo].[PayeeRates]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayeeRates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayeeRates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromAmt] [money] NOT NULL,
	[ToAmt] [money] NOT NULL,
	[Rate] [smallmoney] NOT NULL,
 CONSTRAINT [PK_PayeeRates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[PayeeRates] ON
INSERT [dbo].[PayeeRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (25, 0.0000, 10164.0000, 10.0000)
INSERT [dbo].[PayeeRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (26, 10165.0000, 19741.0000, 15.0000)
INSERT [dbo].[PayeeRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (27, 19741.0000, 29317.0000, 20.0000)
INSERT [dbo].[PayeeRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (28, 29317.0000, 38893.0000, 25.0000)
INSERT [dbo].[PayeeRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (29, 38893.0000, 99999999999.0000, 30.0000)
SET IDENTITY_INSERT [dbo].[PayeeRates] OFF
/****** Object:  Table [dbo].[PackedTransactions]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PackedTransactions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PackedTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PackDate] [datetime] NOT NULL,
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TxnCode] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Amount] [smallmoney] NOT NULL,
	[CreatedBy] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Authorized] [bit] NULL,
 CONSTRAINT [PK_PackedTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[NHIFRates]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NHIFRates]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[NHIFRates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromAmt] [money] NOT NULL,
	[ToAmt] [money] NOT NULL,
	[Rate] [smallmoney] NOT NULL,
 CONSTRAINT [PK_NHIFRates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[NHIFRates] ON
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (49, 1000.0000, 1499.0000, 30.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (50, 1500.0000, 1999.0000, 40.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (51, 2000.0000, 2999.0000, 60.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (52, 3000.0000, 3999.0000, 80.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (53, 4000.0000, 4999.0000, 100.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (54, 5000.0000, 5999.0000, 120.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (55, 6000.0000, 6999.0000, 140.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (56, 7000.0000, 7999.0000, 160.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (57, 8000.0000, 8999.0000, 180.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (58, 9000.0000, 9999.0000, 200.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (59, 10000.0000, 10999.0000, 220.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (60, 11000.0000, 11999.0000, 240.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (61, 12000.0000, 12999.0000, 260.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (62, 13000.0000, 13999.0000, 280.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (63, 14000.0000, 14999.0000, 300.0000)
INSERT [dbo].[NHIFRates] ([Id], [FromAmt], [ToAmt], [Rate]) VALUES (64, 15000.0000, 999999999.0000, 320.0000)
SET IDENTITY_INSERT [dbo].[NHIFRates] OFF
/****** Object:  Table [dbo].[LeaveTransactions]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LeaveTransactions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[LeaveTransactions](
	[Id] [int] NOT NULL,
	[PostDate] [date] NULL,
	[EffectiveDate] [date] NULL,
	[LeaveDesc] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoofDays] [int] NULL,
 CONSTRAINT [PK_LeaveTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[HourlyPayment]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HourlyPayment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HourlyPayment](
	[Empno] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[WorkDate] [date] NOT NULL,
	[WorkHours] [int] NOT NULL,
	[RatePerHour] [money] NOT NULL,
 CONSTRAINT [PK_HourlyPayment_1] PRIMARY KEY CLUSTERED 
(
	[Empno] ASC,
	[WorkDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[HourlyPayment] ([Empno], [WorkDate], [WorkHours], [RatePerHour]) VALUES (N'E1        ', CAST(0x00000000 AS Date), 5, 544.0000)
INSERT [dbo].[HourlyPayment] ([Empno], [WorkDate], [WorkHours], [RatePerHour]) VALUES (N'E1        ', CAST(0x6E360B00 AS Date), 6, 443.0000)
INSERT [dbo].[HourlyPayment] ([Empno], [WorkDate], [WorkHours], [RatePerHour]) VALUES (N'E4        ', CAST(0x63360B00 AS Date), 5, 5656.0000)
/****** Object:  Table [dbo].[Accounts]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] NOT NULL,
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AccountType] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BookBalance] [money] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[PayslipMaster_Temp]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipMaster_Temp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayslipMaster_Temp](
	[Period] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PaymentDate] [date] NOT NULL,
	[PrintedBy] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PrintedOn] [datetime] NOT NULL,
	[EmpName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PayPoint] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PIN] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NHIFNo] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSFNo] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Department] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpGroup] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmpPayroll] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompAddr] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompTel] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayeTax] [decimal](18, 0) NOT NULL,
	[BasicPay] [decimal](18, 0) NOT NULL,
	[Benefits] [decimal](18, 0) NOT NULL,
	[Variables] [decimal](18, 0) NOT NULL,
	[OtherDeductions] [decimal](18, 0) NOT NULL,
	[GrossTaxableEarnings] [decimal](18, 0) NOT NULL,
	[NetTaxableEarnings] [decimal](18, 0) NOT NULL,
	[MortgageRelief] [decimal](18, 0) NOT NULL,
	[GrossTax] [decimal](18, 0) NOT NULL,
	[PersonalRelief] [decimal](18, 0) NOT NULL,
	[PensionEmployer] [decimal](18, 0) NOT NULL,
	[EmployerNSSF] [decimal](18, 0) NOT NULL,
	[PensionEmployee] [decimal](18, 0) NOT NULL,
	[BankBranch] [nchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Account] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSF] [decimal](18, 0) NOT NULL,
	[NHIF] [decimal](18, 0) NOT NULL,
	[NetPay] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_PayslipMaster_Temp] PRIMARY KEY CLUSTERED 
(
	[Period] ASC,
	[Year] ASC,
	[EmpNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[PayslipMaster_Temp] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (2, 2012, N'E1        ', CAST(0x70360B00 AS Date), N'System    ', CAST(0x0000A11500000000 AS DateTime), N'KEVIN, MATIN', N'          ', N'112222              ', N'32223               ', N'32222               ', N'MK', N'          ', N'          ', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(51265 AS Decimal(18, 0)), CAST(117288 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(6468 AS Decimal(18, 0)), CAST(191308 AS Decimal(18, 0)), CAST(191108 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(52427 AS Decimal(18, 0)), CAST(1162 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(2473 AS Decimal(18, 0)), N'55001', N'344322              ', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(59035 AS Decimal(18, 0)))
INSERT [dbo].[PayslipMaster_Temp] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (2, 2012, N'E2        ', CAST(0x70360B00 AS Date), N'System    ', CAST(0x0000A11500000000 AS DateTime), N'RACHEL, SASHA', N'          ', N'38675               ', N'776737              ', N'324334              ', N'FN', N'          ', N'          ', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(5385 AS Decimal(18, 0)), CAST(38230 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(38230 AS Decimal(18, 0)), CAST(38030 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(6547 AS Decimal(18, 0)), CAST(1162 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(2494 AS Decimal(18, 0)), N'55002', N'344434345           ', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(32326 AS Decimal(18, 0)))
INSERT [dbo].[PayslipMaster_Temp] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (2, 2012, N'E3        ', CAST(0x70360B00 AS Date), N'System    ', CAST(0x0000A11500000000 AS DateTime), N'TABITHA, SASHA', N'4444444444', N'33333333333333333333', N'55555555555555555555', N'66666666666666666666', N'FN', N'4444444444', N'4444444444', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(14239 AS Decimal(18, 0)), CAST(67890 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(6789 AS Decimal(18, 0)), CAST(67890 AS Decimal(18, 0)), CAST(67690 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(15401 AS Decimal(18, 0)), CAST(1162 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(4273 AS Decimal(18, 0)), N'01150', N'65555555555565      ', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(46342 AS Decimal(18, 0)))
INSERT [dbo].[PayslipMaster_Temp] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (2, 2012, N'E4        ', CAST(0x70360B00 AS Date), N'System    ', CAST(0x0000A11500000000 AS DateTime), N'YUNASI, WILLIAM', N'5555555555', N'77777777777777777777', N'33333333333333333333', N'55555555555555555555', N'MK', N'6666666666', N'7777777777', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(0 AS Decimal(18, 0)), CAST(279836 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(5656 AS Decimal(18, 0)), CAST(463516 AS Decimal(18, 0)), CAST(463316 AS Decimal(18, 0)), CAST(44444444 AS Decimal(18, 0)), CAST(134089 AS Decimal(18, 0)), CAST(33333333 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(4273 AS Decimal(18, 0)), N'51000', N'66666666666666666666', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(273660 AS Decimal(18, 0)))
INSERT [dbo].[PayslipMaster_Temp] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (2, 2012, N'E5        ', CAST(0x70360B00 AS Date), N'System    ', CAST(0x0000A11500000000 AS DateTime), N'HENRY, MORRIS,  GIBSON', N'4564564564', N'77777777777777777777', N'44444444444444444444', N'55555555555555555555', N'FN', N'4564564646', N'4565464564', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(21102 AS Decimal(18, 0)), CAST(90765 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(90765 AS Decimal(18, 0)), CAST(90565 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(22264 AS Decimal(18, 0)), CAST(1162 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(5646 AS Decimal(18, 0)), N'26001', N'45646433366546456456', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(69143 AS Decimal(18, 0)))
/****** Object:  Table [dbo].[PayslipMaster]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayslipMaster](
	[Period] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PaymentDate] [date] NOT NULL,
	[PrintedBy] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PrintedOn] [datetime] NOT NULL,
	[EmpName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PayPoint] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PIN] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NHIFNo] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSFNo] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Department] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpGroup] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmpPayroll] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CompAddr] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompTel] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayeTax] [decimal](18, 0) NOT NULL,
	[BasicPay] [decimal](18, 0) NOT NULL,
	[Benefits] [decimal](18, 0) NOT NULL,
	[Variables] [decimal](18, 0) NOT NULL,
	[OtherDeductions] [decimal](18, 0) NOT NULL,
	[GrossTaxableEarnings] [decimal](18, 0) NOT NULL,
	[NetTaxableEarnings] [decimal](18, 0) NOT NULL,
	[MortgageRelief] [decimal](18, 0) NOT NULL,
	[GrossTax] [decimal](18, 0) NOT NULL,
	[PersonalRelief] [decimal](18, 0) NOT NULL,
	[PensionEmployer] [decimal](18, 0) NOT NULL,
	[EmployerNSSF] [decimal](18, 0) NOT NULL,
	[PensionEmployee] [decimal](18, 0) NOT NULL,
	[BankBranch] [nchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Account] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSF] [decimal](18, 0) NOT NULL,
	[NHIF] [decimal](18, 0) NOT NULL,
	[NetPay] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_PayslipMaster] PRIMARY KEY CLUSTERED 
(
	[Period] ASC,
	[Year] ASC,
	[EmpNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[PayslipMaster] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (1, 2012, N'E1        ', CAST(0x66360B00 AS Date), N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'KEVIN, MATIN', N'          ', N'112222              ', N'32223               ', N'32222               ', N'MK', N'          ', N'          ', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(5300 AS Decimal(18, 0)), CAST(37890 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(37890 AS Decimal(18, 0)), CAST(37690 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(6462 AS Decimal(18, 0)), CAST(1162 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(2473 AS Decimal(18, 0)), N'55001', N'344322              ', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(32071 AS Decimal(18, 0)))
INSERT [dbo].[PayslipMaster] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (1, 2012, N'E2        ', CAST(0x66360B00 AS Date), N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'RACHEL, SASHA', N'          ', N'38675               ', N'776737              ', N'324334              ', N'FN', N'          ', N'          ', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(5385 AS Decimal(18, 0)), CAST(38230 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(38230 AS Decimal(18, 0)), CAST(38030 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(6547 AS Decimal(18, 0)), CAST(1162 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(2494 AS Decimal(18, 0)), N'55002', N'344434345           ', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(32326 AS Decimal(18, 0)))
INSERT [dbo].[PayslipMaster] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (1, 2012, N'E3        ', CAST(0x66360B00 AS Date), N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'TABITHA, SASHA', N'4444444444', N'33333333333333333333', N'55555555555555555555', N'66666666666666666666', N'FN', N'4444444444', N'4444444444', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(14239 AS Decimal(18, 0)), CAST(67890 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(6789 AS Decimal(18, 0)), CAST(67890 AS Decimal(18, 0)), CAST(67690 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(15401 AS Decimal(18, 0)), CAST(1162 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(4273 AS Decimal(18, 0)), N'01150', N'65555555555565      ', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(46342 AS Decimal(18, 0)))
INSERT [dbo].[PayslipMaster] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (1, 2012, N'E4        ', CAST(0x66360B00 AS Date), N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'YUNASI, WILLIAM', N'5555555555', N'77777777777777777777', N'33333333333333333333', N'55555555555555555555', N'MK', N'6666666666', N'7777777777', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(0 AS Decimal(18, 0)), CAST(279836 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(5656 AS Decimal(18, 0)), CAST(463516 AS Decimal(18, 0)), CAST(463316 AS Decimal(18, 0)), CAST(44444444 AS Decimal(18, 0)), CAST(134089 AS Decimal(18, 0)), CAST(33333333 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(4273 AS Decimal(18, 0)), N'51000', N'66666666666666666666', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(273660 AS Decimal(18, 0)))
INSERT [dbo].[PayslipMaster] ([Period], [Year], [EmpNo], [PaymentDate], [PrintedBy], [PrintedOn], [EmpName], [PayPoint], [PIN], [NHIFNo], [NSSFNo], [Department], [EmpGroup], [EmpPayroll], [CompName], [CompAddr], [CompTel], [PayeTax], [BasicPay], [Benefits], [Variables], [OtherDeductions], [GrossTaxableEarnings], [NetTaxableEarnings], [MortgageRelief], [GrossTax], [PersonalRelief], [PensionEmployer], [EmployerNSSF], [PensionEmployee], [BankBranch], [Account], [NSSF], [NHIF], [NetPay]) VALUES (1, 2012, N'E5        ', CAST(0x66360B00 AS Date), N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'HENRY, MORRIS,  GIBSON', N'4564564564', N'77777777777777777777', N'44444444444444444444', N'55555555555555555555', N'FN', N'4564564646', N'4565464564', N'vike insurance brookers', N'limuru road ngaraparklands', N'0717769329', CAST(21102 AS Decimal(18, 0)), CAST(90765 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(90765 AS Decimal(18, 0)), CAST(90565 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(22264 AS Decimal(18, 0)), CAST(1162 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), CAST(200 AS Decimal(18, 0)), CAST(5646 AS Decimal(18, 0)), N'26001', N'45646433366546456456', CAST(200 AS Decimal(18, 0)), CAST(320 AS Decimal(18, 0)), CAST(69143 AS Decimal(18, 0)))
/****** Object:  Table [dbo].[PayslipDet_Temp]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipDet_Temp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayslipDet_Temp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmpTxnId] [int] NOT NULL,
	[Period] [int] NOT NULL,
	[Year] [int] NULL,
	[Description] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TaxTracking] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[DEType] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsStatutory] [bit] NOT NULL,
	[ShowInPayslip] [bit] NULL,
	[YTD] [decimal](18, 0) NULL,
 CONSTRAINT [PK_PayslipDet_Temp] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[PayslipDet_Temp] ON
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1, N'E1        ', 1871, 2, 2012, N'PAYE', N'PAYE      ', CAST(51265 AS Decimal(18, 0)), N'D', 1, 1, CAST(56565 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (2, N'E1        ', 1872, 2, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(400 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (3, N'E1        ', 1873, 2, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(640 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (4, N'E1        ', 1902, 2, 2012, N'ADVANCE', N'COLLECTION', CAST(3234 AS Decimal(18, 0)), N'D', 0, 1, CAST(3234 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (5, N'E1        ', 1903, 2, 2012, N'KISERIAN SACCO', N'COLLECTION', CAST(3234 AS Decimal(18, 0)), N'D', 0, 1, CAST(3234 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (6, N'E1        ', 1874, 2, 2012, N'BASIC', N'EARNING   ', CAST(37890 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (7, N'E1        ', 1900, 2, 2012, N'NONCASHBENEFIT', N'EARNING   ', CAST(74020 AS Decimal(18, 0)), N'E', 0, 1, CAST(74020 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (8, N'E2        ', 1876, 2, 2012, N'PAYE', N'PAYE      ', CAST(5385 AS Decimal(18, 0)), N'D', 1, 1, CAST(10770 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (9, N'E2        ', 1877, 2, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(400 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (10, N'E2        ', 1878, 2, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(640 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (11, N'E2        ', 1879, 2, 2012, N'BASIC', N'EARNING   ', CAST(38230 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (12, N'E3        ', 1881, 2, 2012, N'PAYE', N'PAYE      ', CAST(14239 AS Decimal(18, 0)), N'D', 1, 1, CAST(28478 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (13, N'E3        ', 1882, 2, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(400 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (14, N'E3        ', 1883, 2, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(640 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (15, N'E3        ', 1896, 2, 2012, N'ADVANCE', N'COLLECTION', CAST(6789 AS Decimal(18, 0)), N'D', 0, 1, CAST(13578 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (16, N'E3        ', 1884, 2, 2012, N'BASIC', N'EARNING   ', CAST(67890 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (17, N'E4        ', 1886, 2, 2012, N'PAYE', N'PAYE      ', CAST(0 AS Decimal(18, 0)), N'D', 1, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (18, N'E4        ', 1887, 2, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(400 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (19, N'E4        ', 1888, 2, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(640 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (20, N'E4        ', 1897, 2, 2012, N'ADVANCE', N'COLLECTION', CAST(5656 AS Decimal(18, 0)), N'D', 0, 1, CAST(11312 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (21, N'E4        ', 1889, 2, 2012, N'BASIC', N'EARNING   ', CAST(67876 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (22, N'E4        ', 1899, 2, 2012, N'NONCASHBENEFIT', N'EARNING   ', CAST(183680 AS Decimal(18, 0)), N'E', 0, 1, CAST(367360 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (23, N'E5        ', 1891, 2, 2012, N'PAYE', N'PAYE      ', CAST(21102 AS Decimal(18, 0)), N'D', 1, 1, CAST(42204 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (24, N'E5        ', 1892, 2, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(400 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (25, N'E5        ', 1893, 2, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(640 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet_Temp] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (26, N'E5        ', 1894, 2, 2012, N'BASIC', N'EARNING   ', CAST(90765 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[PayslipDet_Temp] OFF
/****** Object:  Table [dbo].[TxnType]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TxnType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TxnType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TxnType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transactions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PostDate] [datetime] NULL,
	[Amount] [money] NULL,
	[DRCR] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Narrative] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TxnType] [int] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[TransactionDef]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransactionDef]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TransactionDef](
	[TxnCode] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DataEntry] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PayrollItem] [nchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DefaultAmount] [smallmoney] NULL,
	[Enabled] [bit] NULL,
	[Recurrent] [bit] NULL,
	[TrackYTD] [bit] NULL,
 CONSTRAINT [PK_TransactionDef] PRIMARY KEY CLUSTERED 
(
	[TxnCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[TaxTracking]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaxTracking]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TaxTracking](
	[TaxTrackingId] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_TaxTracking] PRIMARY KEY CLUSTERED 
(
	[TaxTrackingId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[TaxTracking] ([TaxTrackingId], [Description]) VALUES (N'COLLECTION', N'Collection for An Account')
INSERT [dbo].[TaxTracking] ([TaxTrackingId], [Description]) VALUES (N'DEDUCTIBLE', N'Tax deductible')
INSERT [dbo].[TaxTracking] ([TaxTrackingId], [Description]) VALUES (N'EARNING   ', N'Earning')
INSERT [dbo].[TaxTracking] ([TaxTrackingId], [Description]) VALUES (N'NONE      ', N'Not Taxable')
INSERT [dbo].[TaxTracking] ([TaxTrackingId], [Description]) VALUES (N'PAYE      ', N'Income Taxes')
/****** Object:  Table [dbo].[spUsers]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spUsers](
	[UserId] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Password] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Role] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Locked] [bit] NULL,
 CONSTRAINT [PK_spUsers] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[spUsers] ([UserId], [Password], [Role], [Locked]) VALUES (N'sys', N'sys', N'ADMINISTRATOR', 0)
/****** Object:  Table [dbo].[SettingsGroup]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingsGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SettingsGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Parent] [int] NOT NULL,
 CONSTRAINT [PK_SettingsGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[SettingsGroup] ON
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (1, N'Setttings', 0)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (2, N'Statutory Computations', 1)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (3, N'General', 1)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (4, N'NSSF', 6)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (5, N'PAYE', 2)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (6, N'Pension', 2)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (7, N'Security', 1)
SET IDENTITY_INSERT [dbo].[SettingsGroup] OFF
/****** Object:  Table [dbo].[EmpNonCashBenefits]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpNonCashBenefits]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmpNonCashBenefits](
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Benefit] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Rate] [money] NOT NULL,
 CONSTRAINT [PK_NonCashBenefits] PRIMARY KEY CLUSTERED 
(
	[EmpNo] ASC,
	[Benefit] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[EmpNonCashBenefits] ([EmpNo], [Benefit], [Quantity], [Rate]) VALUES (N'E1        ', 40, 9, 3280.0000)
INSERT [dbo].[EmpNonCashBenefits] ([EmpNo], [Benefit], [Quantity], [Rate]) VALUES (N'E1        ', 41, 5, 8900.0000)
INSERT [dbo].[EmpNonCashBenefits] ([EmpNo], [Benefit], [Quantity], [Rate]) VALUES (N'E4        ', 40, 56, 3280.0000)
/****** Object:  Table [dbo].[Employer]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employer]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address1] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address2] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Telphone] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PIN] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Logo] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NHIF] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSF] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BankBranchSortCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccountName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccountNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AuthorizedSignatory] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Employer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Employer] ON
INSERT [dbo].[Employer] ([Id], [Name], [Address1], [Address2], [Telphone], [PIN], [Email], [Logo], [NHIF], [NSSF], [BankBranchSortCode], [AccountName], [AccountNo], [AuthorizedSignatory]) VALUES (15, N'vike insurance brookers', N'limuru road ngara', N'parklands', N'0717769329', N'38273', N'mymail@gmail,com', N'C:\working projects\projects\051012\KuzarFoliaAgriProducts 12th august\KuzarFoliaAgriProducts\Resources\wallpapers\CA-wp3.jpg', N'33333', N'2222222', N'59001', N'vike', N'2312334322', N'director')
SET IDENTITY_INSERT [dbo].[Employer] OFF
/****** Object:  Table [dbo].[EmployeeTransactions]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeTransactions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployeeTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ItemId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Amount] [money] NOT NULL,
	[Recurrent] [bit] NOT NULL,
	[Processed] [bit] NULL,
	[Enabled] [bit] NOT NULL,
	[TrackYTD] [bit] NULL,
	[ShowYTDInPayslip] [bit] NULL,
	[Balance] [money] NULL,
	[InitialAmount] [money] NULL,
	[CreatedBy] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastChangedBy] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastChangeDate] [datetime] NULL,
	[AuthorizedBy] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AuthorizeDate] [datetime] NULL,
	[LoanType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccumulativePayment] [money] NULL,
 CONSTRAINT [PK_EmployeeTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeTransactions]') AND name = N'IX_EmployeeTransactions')
CREATE UNIQUE NONCLUSTERED INDEX [IX_EmployeeTransactions] ON [dbo].[EmployeeTransactions] 
(
	[EmpNo] ASC,
	[ItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[EmployeeTransactions] ON
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1871, CAST(0x0000A10700000000 AS DateTime), N'E1        ', N'PAYE', 51264.8000, 1, 0, 1, 1, 1, 5300.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1872, CAST(0x0000A10700000000 AS DateTime), N'E1        ', N'NSSF', 200.0000, 1, 0, 1, 1, 1, 200.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1873, CAST(0x0000A10700000000 AS DateTime), N'E1        ', N'NHIF', 320.0000, 1, 0, 1, 1, 1, 320.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1874, CAST(0x0000A11500000000 AS DateTime), N'E1        ', N'BASIC', 37890.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1875, CAST(0x0000A10700000000 AS DateTime), N'E1        ', N'PENSION', 0.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1876, CAST(0x0000A10700000000 AS DateTime), N'E2        ', N'PAYE', 5384.5000, 1, 0, 1, 1, 1, 5385.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1877, CAST(0x0000A10700000000 AS DateTime), N'E2        ', N'NSSF', 200.0000, 1, 0, 1, 1, 1, 200.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1878, CAST(0x0000A10700000000 AS DateTime), N'E2        ', N'NHIF', 320.0000, 1, 0, 1, 1, 1, 320.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1879, CAST(0x0000A10700000000 AS DateTime), N'E2        ', N'BASIC', 38230.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1880, CAST(0x0000A10700000000 AS DateTime), N'E2        ', N'PENSION', 0.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10700000000 AS DateTime), N'System    ', CAST(0x0000A10700000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1881, CAST(0x0000A10B00000000 AS DateTime), N'E3        ', N'PAYE', 14239.4000, 1, 0, 1, 1, 1, 14239.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1882, CAST(0x0000A10B00000000 AS DateTime), N'E3        ', N'NSSF', 200.0000, 1, 0, 1, 1, 1, 200.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1883, CAST(0x0000A10B00000000 AS DateTime), N'E3        ', N'NHIF', 320.0000, 1, 0, 1, 1, 1, 320.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1884, CAST(0x0000A10B00000000 AS DateTime), N'E3        ', N'BASIC', 67890.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1885, CAST(0x0000A10B00000000 AS DateTime), N'E3        ', N'PENSION', 0.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1886, CAST(0x0000A10B00000000 AS DateTime), N'E4        ', N'PAYE', 0.0000, 1, 0, 1, 1, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1887, CAST(0x0000A10B00000000 AS DateTime), N'E4        ', N'NSSF', 200.0000, 1, 0, 1, 1, 1, 200.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1888, CAST(0x0000A10B00000000 AS DateTime), N'E4        ', N'NHIF', 320.0000, 1, 0, 1, 1, 1, 320.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1889, CAST(0x0000A10C00000000 AS DateTime), N'E4        ', N'BASIC', 67876.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1890, CAST(0x0000A10B00000000 AS DateTime), N'E4        ', N'PENSION', 0.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1891, CAST(0x0000A10B00000000 AS DateTime), N'E5        ', N'PAYE', 21101.9000, 1, 0, 1, 1, 1, 21102.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1892, CAST(0x0000A10B00000000 AS DateTime), N'E5        ', N'NSSF', 200.0000, 1, 0, 1, 1, 1, 200.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1893, CAST(0x0000A10B00000000 AS DateTime), N'E5        ', N'NHIF', 320.0000, 1, 0, 1, 1, 1, 320.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1894, CAST(0x0000A10B00000000 AS DateTime), N'E5        ', N'BASIC', 90765.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1895, CAST(0x0000A10B00000000 AS DateTime), N'E5        ', N'PENSION', 0.0000, 1, 0, 1, 0, 1, 0.0000, NULL, N'ToComplete', N'System    ', CAST(0x0000A10B00000000 AS DateTime), N'System    ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1896, CAST(0x0000A10B00000000 AS DateTime), N'E3        ', N'ADVANCE', 6789.0000, 1, 0, 1, 1, 1, 6789.0000, NULL, N'sys       ', N'sys       ', CAST(0x0000A10B00000000 AS DateTime), N'          ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1897, CAST(0x0000A10B00000000 AS DateTime), N'E4        ', N'ADVANCE', 5656.0000, 1, 0, 1, 1, 1, 5656.0000, NULL, N'sys       ', N'sys       ', CAST(0x0000A10B00000000 AS DateTime), N'          ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1898, CAST(0x0000A10C00000000 AS DateTime), N'E4        ', N'HRLYPAY', 28280.0000, 1, 0, 1, 1, 1, 0.0000, NULL, N'sys       ', N'sys       ', CAST(0x0000A10B00000000 AS DateTime), N'          ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1899, CAST(0x0000A10C00000000 AS DateTime), N'E4        ', N'NONCASHBENEFIT', 183680.0000, 1, 0, 1, 1, 1, 183680.0000, NULL, N'sys       ', N'sys       ', CAST(0x0000A10B00000000 AS DateTime), N'          ', CAST(0x0000A10B00000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1900, CAST(0x0000A11100000000 AS DateTime), N'E1        ', N'NONCASHBENEFIT', 74020.0000, 1, 0, 1, 1, 1, 0.0000, NULL, N'sys       ', N'sys       ', CAST(0x0000A11100000000 AS DateTime), N'          ', CAST(0x0000A11100000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1901, CAST(0x0000A11100000000 AS DateTime), N'E1        ', N'HRLYPAY', 5378.0000, 1, 0, 1, 1, 1, 0.0000, NULL, N'sys       ', N'sys       ', CAST(0x0000A11100000000 AS DateTime), N'          ', CAST(0x0000A11100000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1902, CAST(0x0000A11500000000 AS DateTime), N'E1        ', N'ADVANCE', 3234.0000, 1, 0, 1, 1, 1, 0.0000, NULL, N'sys       ', N'sys       ', CAST(0x0000A11500000000 AS DateTime), N'          ', CAST(0x0000A11500000000 AS DateTime), NULL, NULL)
INSERT [dbo].[EmployeeTransactions] ([Id], [PostDate], [EmpNo], [ItemId], [Amount], [Recurrent], [Processed], [Enabled], [TrackYTD], [ShowYTDInPayslip], [Balance], [InitialAmount], [CreatedBy], [LastChangedBy], [LastChangeDate], [AuthorizedBy], [AuthorizeDate], [LoanType], [AccumulativePayment]) VALUES (1903, CAST(0x0000A11500000000 AS DateTime), N'E1        ', N'KISERIAN SACCO', 3234.0000, 1, 0, 1, 1, 1, 0.0000, NULL, N'sys       ', N'sys       ', CAST(0x0000A11500000000 AS DateTime), N'          ', CAST(0x0000A11500000000 AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[EmployeeTransactions] OFF
/****** Object:  Table [dbo].[Employee_Ext_Fields]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee_Ext_Fields]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee_Ext_Fields](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Employee_Ext_Fields] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Employee_Ext]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee_Ext]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee_Ext](
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ExFieldName] [int] NOT NULL,
	[ExFieldStr] [varchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExFieldInt] [int] NULL,
	[ExFieldDate] [datetime] NULL,
	[ExFieldDecimal] [numeric](18, 0) NULL,
 CONSTRAINT [PK_Employee_Ext] PRIMARY KEY CLUSTERED 
(
	[EmpNo] ASC,
	[ExFieldName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee](
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Surname] [nchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OtherNames] [nchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DoB] [smalldatetime] NULL,
	[DoE] [datetime] NOT NULL,
	[MaritalStatus] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Gender] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSFNo] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NHIFNo] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PINNo] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BankCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IDNo] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BankAccount] [nchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Department] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NULL,
	[DateLeft] [datetime] NULL,
	[PrevEmployer] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BasicPay] [money] NULL,
	[BasicComputation] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PersonalRelief] [money] NULL,
	[MortgageRelief] [money] NULL,
	[Employer] [int] NOT NULL,
	[PayPoint] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpGroup] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpPayroll] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Photo] [varchar](990) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedOn] [datetime] NULL,
	[CreatedBy] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PaymentMode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TelephoneNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LeaveBalance] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ChequeNo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmpNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Employee] ([EmpNo], [Surname], [OtherNames], [DoB], [DoE], [MaritalStatus], [Gender], [NSSFNo], [NHIFNo], [PINNo], [BankCode], [IDNo], [BankAccount], [Department], [IsActive], [DateLeft], [PrevEmployer], [BasicPay], [BasicComputation], [PersonalRelief], [MortgageRelief], [Employer], [PayPoint], [EmpGroup], [EmpPayroll], [Photo], [CreatedOn], [CreatedBy], [PaymentMode], [TelephoneNo], [LeaveBalance], [ChequeNo]) VALUES (N'E1        ', N'KEVIN                         ', N'MATIN                                             ', CAST(0x735F040A AS SmallDateTime), CAST(0x0000A107011C2304 AS DateTime), N'M', N'M', N'32222               ', N'32223               ', N'112222              ', N'55001', N'232233', N'344322              ', N'MK', 1, CAST(0x0000A06C00000000 AS DateTime), N'', 37890.0000, N'X', 0.0000, 0.0000, 15, N'          ', N'          ', N'          ', N'C:\working projects\projects\051012\KuzarFoliaAgriProducts 12th august\KuzarFoliaAgriProducts\Resources\wallpapers\AU-wp3.jpg', CAST(0x0000A107011C7D01 AS DateTime), N'ToComplete', N'BANKACCOUNT', NULL, NULL, NULL)
INSERT [dbo].[Employee] ([EmpNo], [Surname], [OtherNames], [DoB], [DoE], [MaritalStatus], [Gender], [NSSFNo], [NHIFNo], [PINNo], [BankCode], [IDNo], [BankAccount], [Department], [IsActive], [DateLeft], [PrevEmployer], [BasicPay], [BasicComputation], [PersonalRelief], [MortgageRelief], [Employer], [PayPoint], [EmpGroup], [EmpPayroll], [Photo], [CreatedOn], [CreatedBy], [PaymentMode], [TelephoneNo], [LeaveBalance], [ChequeNo]) VALUES (N'E2        ', N'RACHEL                        ', N'SASHA                                             ', CAST(0x5B1D040C AS SmallDateTime), CAST(0x0000A107011CB9B4 AS DateTime), N'M', N'F', N'324334              ', N'776737              ', N'38675               ', N'55002', N'2378633', N'344434345           ', N'FN', 1, CAST(0x0000A06C00000000 AS DateTime), N'', 38230.0000, N'X', 0.0000, 0.0000, 15, N'          ', N'          ', N'          ', N'C:\working projects\projects\051012\KuzarFoliaAgriProducts 12th august\KuzarFoliaAgriProducts\Resources\wallpapers\CA-wp2.jpg', CAST(0x0000A107011CE887 AS DateTime), N'ToComplete', N'BANKACCOUNT', NULL, NULL, NULL)
INSERT [dbo].[Employee] ([EmpNo], [Surname], [OtherNames], [DoB], [DoE], [MaritalStatus], [Gender], [NSSFNo], [NHIFNo], [PINNo], [BankCode], [IDNo], [BankAccount], [Department], [IsActive], [DateLeft], [PrevEmployer], [BasicPay], [BasicComputation], [PersonalRelief], [MortgageRelief], [Employer], [PayPoint], [EmpGroup], [EmpPayroll], [Photo], [CreatedOn], [CreatedBy], [PaymentMode], [TelephoneNo], [LeaveBalance], [ChequeNo]) VALUES (N'E3        ', N'TABITHA                       ', N'SASHA                                             ', CAST(0x63B1053C AS SmallDateTime), CAST(0x0000A10B01705347 AS DateTime), N'M', N'F', N'66666666666666666666', N'55555555555555555555', N'33333333333333333333', N'01150', N'44444444444444444444', N'65555555555565      ', N'FN', 1, CAST(0x0000A06C00000000 AS DateTime), N'54444444444444444444444444444444444444444444444444', 67890.0000, N'X', 0.0000, 0.0000, 15, N'4444444444', N'4444444444', N'4444444444', N'C:\working projects\projects\051012\KuzarFoliaAgriProducts 12th august\KuzarFoliaAgriProducts\Resources\wallpapers\AU-wp5.jpg', CAST(0x0000A10B0170C310 AS DateTime), N'ToComplete', N'BANKACCOUNT', NULL, NULL, NULL)
INSERT [dbo].[Employee] ([EmpNo], [Surname], [OtherNames], [DoB], [DoE], [MaritalStatus], [Gender], [NSSFNo], [NHIFNo], [PINNo], [BankCode], [IDNo], [BankAccount], [Department], [IsActive], [DateLeft], [PrevEmployer], [BasicPay], [BasicComputation], [PersonalRelief], [MortgageRelief], [Employer], [PayPoint], [EmpGroup], [EmpPayroll], [Photo], [CreatedOn], [CreatedBy], [PaymentMode], [TelephoneNo], [LeaveBalance], [ChequeNo]) VALUES (N'E4        ', N'YUNASI                        ', N'WILLIAM                                           ', CAST(0x6AD3053F AS SmallDateTime), CAST(0x0000A10B0170EC79 AS DateTime), N'M', N'M', N'55555555555555555555', N'33333333333333333333', N'77777777777777777777', N'51000', N'11111111111111111111', N'66666666666666666666', N'MK', 1, CAST(0x0000A06C00000000 AS DateTime), N'55555555555555555555555555555555555555555', 67876.0000, N'X', 33333333.0000, 44444444.0000, 15, N'5555555555', N'6666666666', N'7777777777', N'C:\working projects\projects\051012\KuzarFoliaAgriProducts 12th august\KuzarFoliaAgriProducts\Resources\wallpapers\CA-wp4.jpg', CAST(0x0000A10B0171282F AS DateTime), N'ToComplete', N'BANKACCOUNT', NULL, NULL, NULL)
INSERT [dbo].[Employee] ([EmpNo], [Surname], [OtherNames], [DoB], [DoE], [MaritalStatus], [Gender], [NSSFNo], [NHIFNo], [PINNo], [BankCode], [IDNo], [BankAccount], [Department], [IsActive], [DateLeft], [PrevEmployer], [BasicPay], [BasicComputation], [PersonalRelief], [MortgageRelief], [Employer], [PayPoint], [EmpGroup], [EmpPayroll], [Photo], [CreatedOn], [CreatedBy], [PaymentMode], [TelephoneNo], [LeaveBalance], [ChequeNo]) VALUES (N'E5        ', N'HENRY                         ', N'MORRIS,  GIBSON                                   ', CAST(0x69660540 AS SmallDateTime), CAST(0x0000A10B01715C05 AS DateTime), N'S', N'M', N'55555555555555555555', N'44444444444444444444', N'77777777777777777777', N'26001', N'66666666666666666666', N'45646433366546456456', N'FN', 1, CAST(0x0000A06C00000000 AS DateTime), N'', 90765.0000, N'X', 0.0000, 0.0000, 15, N'4564564564', N'4564564646', N'4565464564', N'C:\working projects\projects\051012\KuzarFoliaAgriProducts 12th august\KuzarFoliaAgriProducts\Resources\wallpapers\AU-wp2.jpg', CAST(0x0000A10B0171A639 AS DateTime), N'ToComplete', N'BANKACCOUNT', NULL, NULL, NULL)
/****** Object:  Table [dbo].[Departments]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Departments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Departments](
	[Code] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Departments] ([Code], [Description]) VALUES (N'FN', N'FINANCE')
INSERT [dbo].[Departments] ([Code], [Description]) VALUES (N'MK', N'MARKETING')
/****** Object:  Table [dbo].[Benefits]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Benefits]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Benefits](
	[BenefitId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rate] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Benefits] PRIMARY KEY CLUSTERED 
(
	[BenefitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Benefits] ON
INSERT [dbo].[Benefits] ([BenefitId], [Name], [Rate]) VALUES (40, N'DRIVER', CAST(3280 AS Decimal(18, 0)))
INSERT [dbo].[Benefits] ([BenefitId], [Name], [Rate]) VALUES (41, N'AYA', CAST(8900 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[Benefits] OFF
/****** Object:  Table [dbo].[Banks]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Banks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Banks](
	[BankCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BankName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Banks] PRIMARY KEY CLUSTERED 
(
	[BankCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'01', N'Kenya Commercial Bank Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'02', N'Standard Chartered Bank')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'03', N'Barclays Bank of Kenya Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'05', N'Bank of India')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'06', N'Bank of Baroda (Kenya) Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'07', N'Commercial Bank of Africa Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'08', N'Habib Bank Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'09', N'Central Bank of Kenya')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'10', N'Prime Bank Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'11', N'Co-operative Bank Of Kenya')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'12', N'National Bank of Kenya')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'14', N'Oriental Commercial Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'16', N'Citi Bank')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'17', N'Habib Bank')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'18', N'Middle East Bank')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'19', N'Bank of Africa Kenya Ltd.')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'20', N' Dubai Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'23', N'Consolidated Bank of Kenya')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'25', N' Credit Bank Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'26', N'Trans-National Bank Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'30', N'Chase Bank')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'31', N'Cfc Stanbic Bank ')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'35', N' African BankingCorporation Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'39', N'Imperial Bank Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'41', N'NIC Bank Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'42', N' Giro Commercial')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'43', N'Ecobank Kenya Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'49', N'Equatorial Commercial Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'50', N'Paramount Universal Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'51', N'Jamii Bora Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'53', N'Fina Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'54', N'Victoria Commercial Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'55', N'Guardian Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'57', N'I & M Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'59', N'Development Bank of Kenya Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'60', N'Fidelity Commercial Bank Ltd.')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'63', N'Diamond Trust Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'66', N'K-Rep Bank limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'68', N'Equity Bank ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'70', N'Family Bank ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'72', N'Gulf African Bank')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'74', N'First Community Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'76', N'UBA Kenya Bank')
/****** Object:  Table [dbo].[Payrolls]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payrolls]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Payrolls](
	[Period] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Employer] [int] NULL,
	[DateRun] [datetime] NULL,
	[RunBy] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Approved] [bit] NULL,
	[ApprovedBy] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsOpen] [bit] NOT NULL,
	[Processed] [bit] NOT NULL,
 CONSTRAINT [PK_Payrolls_1] PRIMARY KEY CLUSTERED 
(
	[Period] ASC,
	[Year] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Payrolls] ([Period], [Year], [Employer], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (1, 2012, 15, CAST(0x0000A10700000000 AS DateTime), N'sys       ', 0, N'          ', 0, 1)
INSERT [dbo].[Payrolls] ([Period], [Year], [Employer], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (2, 2012, 15, CAST(0x0000A10700000000 AS DateTime), N'sys       ', 0, N'          ', 1, 1)
INSERT [dbo].[Payrolls] ([Period], [Year], [Employer], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (3, 2012, 15, CAST(0x0000A10700000000 AS DateTime), N'sys       ', 0, N'          ', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [Employer], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (4, 2012, 15, CAST(0x0000A10700000000 AS DateTime), N'sys       ', 0, N'          ', 1, 0)
/****** Object:  Table [dbo].[PayrollItemType]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollItemType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayrollItemType](
	[PayrollItemTypeId] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Parent] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_PayrollItemType] PRIMARY KEY CLUSTERED 
(
	[PayrollItemTypeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'ADDITION  ', N'ADDITION  ', N'Addition')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'BENEFIT   ', N'ADDITION  ', N'Non Cash Benefits')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'DEDUCTION ', N'DEDUCTION ', N'Deduction')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'EMPECONTR ', N'EMPECONTR ', N'Pension - Employee Contribution')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'EMPYCONTR ', N'EMPYCONTR ', N'Pension - Employer Contribution')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'HRLYPAY   ', N'HRLYPAY   ', N'Hourly Pay')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'LOAN      ', N'DEDUCTION ', N'LOAN      ')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'SACCO     ', N'DEDUCTION ', N'SACCO     ')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'SALARY    ', N'SALARY    ', N'Salary')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'STATUTORY ', N'STATUTORY ', N'Statutory Recovery')
INSERT [dbo].[PayrollItemType] ([PayrollItemTypeId], [Parent], [Description]) VALUES (N'TAX       ', N'TAX       ', N'Payroll Tax')
/****** Object:  Table [dbo].[PayrollItems]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayrollItems](
	[ItemId] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ItemType] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TaxTracking] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayableTo] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GLAccount] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Active] [bit] NULL,
	[AddToPension] [bit] NULL,
	[DefaultItem] [bit] NULL,
	[Enable] [bit] NULL,
	[ReFField] [int] NULL,
 CONSTRAINT [PK_PayrollItems] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[PayrollItems] ([ItemId], [ItemType], [TaxTracking], [PayableTo], [GLAccount], [Active], [AddToPension], [DefaultItem], [Enable], [ReFField]) VALUES (N'ADVANCE', N'DEDUCTION ', N'COLLECTION', N'EMPLOYER', N'25684', 1, 0, 1, NULL, 444444)
INSERT [dbo].[PayrollItems] ([ItemId], [ItemType], [TaxTracking], [PayableTo], [GLAccount], [Active], [AddToPension], [DefaultItem], [Enable], [ReFField]) VALUES (N'BASIC          ', N'SALARY    ', N'EARNING             ', N'EMPLOYEE  ', N'32568', 1, 1, 1, NULL, NULL)
INSERT [dbo].[PayrollItems] ([ItemId], [ItemType], [TaxTracking], [PayableTo], [GLAccount], [Active], [AddToPension], [DefaultItem], [Enable], [ReFField]) VALUES (N'HRLYPAY', N'HRLYPAY   ', N'EARNING   ', N'EMPLOYEE', N'52148', 1, 0, 1, NULL, 32659)
INSERT [dbo].[PayrollItems] ([ItemId], [ItemType], [TaxTracking], [PayableTo], [GLAccount], [Active], [AddToPension], [DefaultItem], [Enable], [ReFField]) VALUES (N'KISERIAN SACCO', N'SACCO     ', N'COLLECTION', N'HHHHHHHHHHHHHHHHHHHH', N'7777777777', 1, 0, 0, NULL, 6578)
INSERT [dbo].[PayrollItems] ([ItemId], [ItemType], [TaxTracking], [PayableTo], [GLAccount], [Active], [AddToPension], [DefaultItem], [Enable], [ReFField]) VALUES (N'NHIF           ', N'STATUTORY ', N'COLLECTION', N'NHIF      ', N'25684', 1, 0, 1, NULL, NULL)
INSERT [dbo].[PayrollItems] ([ItemId], [ItemType], [TaxTracking], [PayableTo], [GLAccount], [Active], [AddToPension], [DefaultItem], [Enable], [ReFField]) VALUES (N'NONCASHBENEFIT', N'ADDITION  ', N'EARNING   ', N'EMPLOYEE', N'38373', 1, 0, 1, NULL, 32658)
INSERT [dbo].[PayrollItems] ([ItemId], [ItemType], [TaxTracking], [PayableTo], [GLAccount], [Active], [AddToPension], [DefaultItem], [Enable], [ReFField]) VALUES (N'NSSF           ', N'STATUTORY ', N'DEDUCTIBLE          ', N'NSSF      ', N'54875', 1, 0, 1, NULL, NULL)
INSERT [dbo].[PayrollItems] ([ItemId], [ItemType], [TaxTracking], [PayableTo], [GLAccount], [Active], [AddToPension], [DefaultItem], [Enable], [ReFField]) VALUES (N'PAYE           ', N'TAX       ', N'PAYE                ', N'PAYMASTER ', N'54847', 1, 0, 1, NULL, NULL)
/****** Object:  Table [dbo].[BankBranch]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankBranch]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BankBranch](
	[BankSortCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BranchCode] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Bank] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BranchName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_BankBranch] PRIMARY KEY CLUSTERED 
(
	[BankSortCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01091', N'091', N'01', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01092', N'092', N'01', N'CPC')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01094', N'094', N'01', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01095', N'095', N'01', N'Wote')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01100', N'100', N'01', N'Moi Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01101', N'101', N'01', N'Kipande House')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01102', N'102', N'01', N'Treasury Square')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01103', N'103', N'01', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01104', N'104', N'01', N'KICC')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01105', N'105', N'01', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01106', N'106', N'01', N'Kericho')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01107', N'107', N'01', N'Tom Mboya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01108', N'108', N'01', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01109', N'109', N'01', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01110', N'110', N'01', N'Kakamega')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01111', N'111', N'01', N'Kilindini')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01112', N'112', N'01', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01113', N'113', N'01', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01114', N'114', N'01', N'River Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01115', N'115', N'01', N'Muranga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01116', N'116', N'01', N'Embu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01117', N'117', N'01', N'Kangema')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01119', N'119', N'01', N'Kiambu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01120', N'120', N'01', N'Karatina')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01121', N'121', N'01', N'Siaya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01122', N'122', N'01', N'Nyahururu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01123', N'123', N'01', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01124', N'124', N'01', N'Mumias')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01125', N'125', N'01', N'Nanyuki')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01127', N'127', N'01', N'Moyale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01129', N'129', N'01', N'Kikuyu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01130', N'130', N'01', N'Tala')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01131', N'131', N'01', N'Kajiado')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01133', N'133', N'01', N'Custody Services')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01134', N'134', N'01', N'Matuu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01135', N'135', N'01', N'Kitui')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01136', N'136', N'01', N'Mvita')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01137', N'137', N'01', N'Jogoo Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01140', N'140', N'01', N'Marsabit')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01141', N'141', N'01', N'Sarit Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01142', N'142', N'01', N'Loitokitok')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01143', N'143', N'01', N'Nandi Hills')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01144', N'144', N'01', N'Lodwar')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01145', N'145', N'01', N'UN Gigiri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01146', N'146', N'01', N'Hola')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01147', N'147', N'01', N'Ruiru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01148', N'148', N'01', N'Mwingi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01149', N'149', N'01', N'Kitale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01150', N'150', N'01', N'Mandera')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01151', N'151', N'01', N'Kapenguria')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01152', N'152', N'01', N'Kabarnet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01153', N'153', N'01', N'Wajir')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01154', N'154', N'01', N'Maralal')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01155', N'155', N'01', N'Limuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01157', N'157', N'01', N'Ukunda')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01161', N'161', N'01', N'Ongata Rongai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01162', N'162', N'01', N'Kitengela')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01163', N'163', N'01', N'Eldama Ravine')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01164', N'164', N'01', N'Kibwezi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01166', N'166', N'01', N'Kapsabet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01167', N'167', N'01', N'University Way')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01168', N'168', N'01', N'Eldoret West')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01169', N'169', N'01', N'Garissa ')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01173', N'173', N'01', N'Lamu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01174', N'174', N'01', N'Kilifi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01175', N'175', N'01', N'Milimani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01176', N'176', N'01', N'Nyamira')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01177', N'177', N'01', N'Mukurwe-ini')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01180', N'180', N'01', N'Village Market')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01181', N'181', N'01', N'Bomet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01183', N'183', N'01', N'Mbale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01184', N'184', N'01', N'Narok')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01185', N'158', N'01', N'Iten')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01186', N'186', N'01', N'Voi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01188', N'188', N'01', N'Webuye')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01189', N'159', N'01', N'Gilgil')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01190', N'190', N'01', N'Naivasha')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01191', N'191', N'01', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01192', N'192', N'01', N'Migori')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01193', N'193', N'01', N'Githunguri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01194', N'194', N'01', N'Machakos')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01195', N'195', N'01', N'Kerugoya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01196', N'196', N'01', N'Chuka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01197', N'197', N'01', N'Bungoma')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01198', N'198', N'01', N'Wundanyi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01199', N'199', N'01', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01201', N'201', N'01', N'Capital Hill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01202', N'202', N'01', N'Karen')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01203', N'203', N'01', N'Lokichogio')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01204', N'204', N'01', N'Gateway')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01205', N'205', N'01', N'Buruburu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01206', N'206', N'01', N'Chogoria')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01207', N'207', N'01', N'Kangari')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01208', N'208', N'01', N'Kianyaga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01209', N'209', N'01', N'Nkubu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01210', N'210', N'01', N'Ol Kalou')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01211', N'211', N'01', N'Makuyu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01212', N'212', N'01', N'Mwea')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01213', N'213', N'01', N'Njabini')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01214', N'214', N'01', N'Gatundu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01215', N'215', N'01', N'Emali')
GO
print 'Processed 100 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01216', N'216', N'01', N'Isiolo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01217', N'217', N'01', N'Flamingo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01218', N'218', N'01', N'Njoro')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01219', N'219', N'01', N'Mutomo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01220', N'220', N'01', N'Mariakani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01221', N'221', N'01', N'Mpeketoni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01222', N'222', N'01', N'Mtito Andei')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01223', N'223', N'01', N'Mtwapa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01224', N'224', N'01', N'Taveta')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01225', N'225', N'01', N'Kengeleni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01226', N'226', N'01', N'Garsen')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01227', N'227', N'01', N'Watamu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01228', N'228', N'01', N'Bondo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01229', N'229', N'01', N'Busia')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01230', N'230', N'01', N'Homa Bay')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01231', N'231', N'01', N'Kapsowar')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01232', N'232', N'01', N'Kehancha')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01233', N'233', N'01', N'Keroka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01234', N'234', N'01', N'Kilgoris')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01235', N'235', N'01', N'Kimilili')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01236', N'236', N'01', N'Litein')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01237', N'237', N'01', N'Londiani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01238', N'238', N'01', N'Luanda')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01239', N'239', N'01', N'Malaba')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01240', N'240', N'01', N'Muhoroni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01241', N'241', N'01', N'Oyugis')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01242', N'242', N'01', N'Ugunja')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01243', N'243', N'01', N'United Mall')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01244', N'244', N'01', N'Serem')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01245', N'245', N'01', N'Sondu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01246', N'246', N'01', N'Kisumu West')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01247', N'247', N'01', N'Marigat')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01248', N'248', N'01', N'Moi''s Bridge')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01249', N'249', N'01', N'Mashariki')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01250', N'250', N'01', N'Naro Moru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01251', N'251', N'01', N'Kiriaini')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01252', N'252', N'01', N'Egerton University')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01253', N'253', N'01', N'Maua')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01254', N'254', N'01', N'Kawangware')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01255', N'255', N'01', N'Kimathi Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01256', N'256', N'01', N'Namanga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01257', N'257', N'01', N'Gikomba')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01258', N'258', N'01', N'Kwale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01259', N'259', N'01', N'Prestige Plaza')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01260', N'260', N'01', N'Kariobangi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01263', N'263', N'01', N'Biashara Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01266', N'266', N'01', N'Ngara')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01267', N'267', N'01', N'Kyuso')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01270', N'270', N'01', N'Masii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01271', N'271', N'01', N'Menengai Crater')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01272', N'272', N'01', N'Town Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01278', N'278', N'01', N'Makindu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01283', N'283', N'01', N'Rongo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01284', N'284', N'01', N'Isibania')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01285', N'285', N'01', N'Kiserian')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01286', N'286', N'01', N'Mwembe Tayari')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01287', N'287', N'01', N'Kisauni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01288', N'288', N'01', N'Haile Selassie')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01289', N'289', N'01', N'Mama Ngina')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01290', N'290', N'01', N'Garden Plaza')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01291', N'291', N'01', N'Sarit Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01292', N'292', N'01', N'CPC Bulk Corporate Chqs')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'01293', N'293', N'01', N'Trade Services')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02000', N'000', N'02', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02001', N'001', N'02', N'Kericho')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02002', N'002', N'02', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02003', N'003', N'02', N'Kitale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02004', N'004', N'02', N'Treasury Square')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02005', N'005', N'02', N'Maritime')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02006', N'006', N'02', N'Kenyatta')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02007', N'007', N'02', N'Kimathi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02008', N'008', N'02', N'Moi Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02009', N'009', N'02', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02010', N'010', N'02', N'Nanyuki')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02011', N'011', N'02', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02012', N'012', N'02', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02015', N'015', N'02', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02016', N'016', N'02', N'Machakos')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02017', N'017', N'02', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02019', N'019', N'02', N'Harambee')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02020', N'020', N'02', N'Kiambu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02053', N'053', N'02', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02054', N'054', N'02', N'Kakamega')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02060', N'060', N'02', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02063', N'063', N'02', N'Haile Selassie')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02064', N'064', N'02', N'Koinange Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02071', N'071', N'02', N'Yaya Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02072', N'072', N'02', N'Ruaraka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02074', N'074', N'02', N'Langata')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02075', N'075', N'02', N'Makupa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02076', N'076', N'02', N'Karen')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02077', N'077', N'02', N'Muthaiga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02078', N'078', N'02', N'Customer Service Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02079', N'079', N'02', N'Customer Service Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02080', N'080', N'02', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02081', N'081', N'02', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02082', N'082', N'02', N'Uper Hill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02083', N'083', N'02', N'Mombasa-Nyali')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'02084', N'084', N'02', N'Chiromo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03002', N'002', N'03', N'Kapsabet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03003', N'003', N'03', N'Eldoret Std & Prestige')
GO
print 'Processed 200 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03004', N'004', N'03', N'Embu Std & Prestige')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03005', N'005', N'03', N'Murang''a')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03006', N'006', N'03', N'Kapenguria')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03007', N'007', N'03', N'Kericho')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03008', N'008', N'03', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03009', N'009', N'03', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03010', N'010', N'03', N'South C')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03011', N'011', N'03', N'Limuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03012', N'012', N'03', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03013', N'013', N'03', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03014', N'014', N'03', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03015', N'015', N'03', N'Kitui')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03016', N'016', N'03', N'Nkrumah Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03017', N'017', N'03', N'Garissa ')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03018', N'018', N'03', N'Nyamira')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03019', N'019', N'03', N'Kilifi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03020', N'020', N'03', N'Waiyaki Way')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03023', N'023', N'03', N'Gilgil')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03024', N'024', N'03', N'Githurai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03027', N'027', N'03', N'Nakuru East')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03028', N'028', N'03', N'Buruburu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03029', N'029', N'03', N'Bomet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03030', N'030', N'03', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03031', N'031', N'03', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03032', N'032', N'03', N'Port Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03033', N'033', N'03', N'Gikomba')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03034', N'034', N'03', N'Kawangware')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03035', N'035', N'03', N'Mbale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03036', N'036', N'03', N'Plaza Premier Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03037', N'037', N'03', N'River Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03038', N'038', N'03', N'Upper River Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03039', N'039', N'03', N'Mumias')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03040', N'040', N'03', N'Machakos')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03042', N'042', N'03', N'Isiolo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03043', N'043', N'03', N'Ngong')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03044', N'044', N'03', N'Maua')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03045', N'045', N'03', N'Hurlingham')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03046', N'046', N'03', N'Makupa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03047', N'047', N'03', N'Development Hse')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03049', N'049', N'03', N'Lavington')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03050', N'050', N'03', N'Eastleigh II')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03051', N'051', N'03', N'Homa Bay')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03052', N'052', N'03', N'Rongai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03053', N'053', N'03', N'Othaya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03054', N'054', N'03', N'Voi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03055', N'055', N'03', N'Muthaiga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03057', N'057', N'03', N'Githunguri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03058', N'058', N'03', N'Webuye')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03060', N'060', N'03', N'Chuka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03061', N'061', N'03', N'Nakumatt Westgate')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03062', N'062', N'03', N'Kabarnet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03063', N'063', N'03', N'Kerugoya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03064', N'064', N'03', N'Taveta')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03065', N'065', N'03', N'Karen Std&Prestige')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03066', N'066', N'03', N'Wundanyi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03067', N'067', N'03', N'Ruaraka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03069', N'069', N'03', N'Wote')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03070', N'070', N'03', N'Enterprise prestige centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03071', N'071', N'03', N'Nakumatt Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03072', N'072', N'03', N'Juja')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03073', N'073', N'03', N'ABC Prestige')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03074', N'074', N'03', N'Kikuyu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03075', N'075', N'03', N'Moi Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03077', N'077', N'03', N'Plaza Business Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03078', N'078', N'03', N'Kiriaini')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03079', N'079', N'03', N'Avon Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03080', N'080', N'03', N'Migori')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03082', N'082', N'03', N'Haile Selassie')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03083', N'083', N'03', N'University of Nairobi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03086', N'086', N'03', N'Nairobi west')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03087', N'087', N'03', N'Parkland Highbridge')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03088', N'088', N'03', N'Busia')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03089', N'089', N'03', N'Pangani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03093', N'093', N'03', N'Kariobangi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03094', N'094', N'03', N'QueensWay')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'03095', N'095', N'03', N'Nakumatt Ebakasi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'05000', N'000', N'05', N'Nairobi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'05001', N'001', N'05', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'05002', N'002', N'05', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'05003', N'003', N'05', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'06000', N'000', N'06', N'Nairobi Main')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'06002', N'002', N'06', N'Digo rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'06004', N'004', N'06', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'06005', N'005', N'06', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'06006', N'006', N'06', N'Sarit Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'06007', N'007', N'06', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'06008', N'008', N'06', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'06009', N'009', N'06', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07000', N'000', N'07', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07001', N'001', N'07', N'Upper Hill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07002', N'002', N'07', N'Wabera')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07003', N'003', N'07', N'Mama Ngina Br/Hilton Agency')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07004', N'004', N'07', N'Westlands Br/ILRI Agency')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07005', N'005', N'07', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07006', N'006', N'07', N'Mamlaka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07007', N'007', N'07', N'Village Mkt Br/US Emb/Icraf Ag')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07008', N'008', N'07', N'Cargo Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07009', N'009', N'07', N'Park Side')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07016', N'016', N'07', N'Galleria')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07017', N'017', N'07', N'Junction')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07020', N'020', N'07', N'Moi Avenue Mombasa')
GO
print 'Processed 300 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07021', N'021', N'07', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07022', N'022', N'07', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07023', N'023', N'07', N'Bamburi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07024', N'024', N'07', N'Diani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07025', N'025', N'07', N'Changamwe')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07026', N'026', N'07', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'07027', N'027', N'07', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'08046', N'046', N'08', N'Mobasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'08047', N'047', N'08', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'08048', N'048', N'08', N'Nairobi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'09000', N'000', N'09', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'09001', N'001', N'09', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'09002', N'002', N'09', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'09003', N'003', N'09', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'09004', N'004', N'09', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10000', N'000', N'10', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10001', N'001', N'10', N'Kenindia')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10002', N'002', N'10', N'Biashara')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10003', N'003', N'10', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10004', N'004', N'10', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10005', N'005', N'10', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10006', N'006', N'10', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10007', N'007', N'10', N'Parklands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10008', N'008', N'10', N'Riverside Drive')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10009', N'009', N'10', N'Card centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10010', N'010', N'10', N'Hurlingham')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10011', N'011', N'10', N'Capital Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10012', N'012', N'10', N'Nyali')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10014', N'014', N'10', N'Kamukunji')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'10015', N'015', N'10', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11000', N'000', N'11', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11002', N'002', N'11', N'Co-op Hse')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11003', N'003', N'11', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11004', N'004', N'11', N'Nkrumah Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11005', N'005', N'11', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11006', N'006', N'11', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11007', N'007', N'11', N'Industrial Are')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11008', N'008', N'11', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11009', N'009', N'11', N'Machakos')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11010', N'010', N'11', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11011', N'011', N'11', N'Ukulima')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11012', N'012', N'11', N'Chuka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11013', N'013', N'11', N'Wakulima Market')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11014', N'014', N'11', N'Moi Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11015', N'015', N'11', N'Naivasha')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11017', N'017', N'11', N'Nyahururu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11018', N'018', N'11', N'Chuka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11019', N'019', N'11', N'Wakulima Market')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11020', N'020', N'11', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11021', N'021', N'11', N'Kiambu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11022', N'022', N'11', N'Homabay')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11023', N'023', N'11', N'Embu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11024', N'024', N'11', N'Kericho')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11025', N'025', N'11', N'Bungoma')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11026', N'026', N'11', N'Muranga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11027', N'027', N'11', N'Kayole')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11028', N'028', N'11', N'Karatina')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11029', N'029', N'11', N'Ukunda')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11030', N'030', N'11', N'Mtwapa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11031', N'031', N'11', N'University way')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11032', N'032', N'11', N'BuruBuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11033', N'033', N'11', N'AthiRiver')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11034', N'034', N'11', N'Mumias')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11035', N'035', N'11', N'Stima Plaza')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11036', N'036', N'11', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11037', N'037', N'11', N'Upper Hill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11038', N'038', N'11', N'Ongata Rongai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11039', N'039', N'11', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11040', N'040', N'11', N'Nacico Plaza')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11041', N'041', N'11', N'Kariobangi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11042', N'042', N'11', N'Kawangware')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11043', N'043', N'11', N'Makutano')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11044', N'044', N'11', N'Parliament Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11045', N'045', N'11', N'Kimathi Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11046', N'046', N'11', N'Kitale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11047', N'047', N'11', N'Githurai Agency')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11048', N'048', N'11', N'Maua')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11049', N'049', N'11', N'City Hall')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11050', N'050', N'11', N'Digo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11051', N'051', N'11', N'Nairobi Business Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11052', N'052', N'11', N'Kakamega')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11053', N'053', N'11', N'Migori')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11055', N'055', N'11', N'Nkubu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11056', N'056', N'11', N'Enterprise Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11057', N'057', N'11', N'Busia')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11058', N'058', N'11', N'Siaya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11059', N'059', N'11', N'Voi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11060', N'060', N'11', N'Mariakani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11061', N'061', N'11', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11062', N'062', N'11', N'Zimmerman')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11063', N'063', N'11', N'Nakuru East')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11064', N'064', N'11', N'Kitengela')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11065', N'065', N'11', N'Aga Khan Walk')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11066', N'066', N'11', N'Narok')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11067', N'067', N'11', N'Kitui')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11068', N'068', N'11', N'Nanyuki')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11069', N'069', N'11', N'Embakasi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11070', N'070', N'11', N'Kibera')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11071', N'071', N'11', N'Siakago')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11072', N'072', N'11', N'Kapsabet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11073', N'073', N'11', N'Mbita')
GO
print 'Processed 400 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11074', N'074', N'11', N'Kangemi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11075', N'075', N'11', N'Dandora')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11077', N'077', N'11', N'Tala')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11078', N'078', N'11', N'Gikomba')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11079', N'079', N'11', N'River Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11080', N'080', N'11', N'Nyamira')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11081', N'081', N'11', N'Garissa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11082', N'082', N'11', N'Bomet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11083', N'083', N'11', N'Keroka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11084', N'084', N'11', N'Gilgil')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11085', N'085', N'11', N'Tom Mboya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11086', N'086', N'11', N'Likoni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11088', N'088', N'11', N'Mwingi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11089', N'089', N'11', N'Mwingi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11090', N'090', N'11', N'Webuye')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11100', N'100', N'11', N'Ndhiwa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'11102', N'102', N'11', N'Isiolo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12002', N'002', N'12', N'Kenyatta Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12003', N'003', N'12', N'Harambee Avenue NBK  Building')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12004', N'004', N'12', N'Hill Plaza')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12005', N'005', N'12', N'Busia')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12006', N'006', N'12', N'Kiambu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12007', N'007', N'12', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12008', N'008', N'12', N'Karatina')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12009', N'009', N'12', N'Narok')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12010', N'010', N'12', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12011', N'011', N'12', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12012', N'012', N'12', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12013', N'013', N'12', N'Kitale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12015', N'015', N'12', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12016', N'016', N'12', N'Limuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12017', N'017', N'12', N'Kitui')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12019', N'019', N'12', N'Bungoma')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12020', N'020', N'12', N'Nkurumah Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12021', N'021', N'12', N'Kapsabet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12022', N'022', N'12', N'Awendo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12023', N'023', N'12', N'Portway Hse')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12025', N'025', N'12', N'Hospital')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12026', N'026', N'12', N'Ruiru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12027', N'027', N'12', N'Ongata Rongai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12028', N'028', N'12', N'Embu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12029', N'029', N'12', N'Kakamega')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12031', N'031', N'12', N'Ukunda')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12032', N'032', N'12', N'Upper Hill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12033', N'033', N'12', N'Nandi Hills')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12034', N'034', N'12', N'Migori')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12035', N'035', N'12', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12036', N'036', N'12', N'Times Tower')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12037', N'037', N'12', N'Maua')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12038', N'038', N'12', N'Wilson Airport')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12039', N'039', N'12', N'J.K.I.A.')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12040', N'040', N'12', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12042', N'042', N'12', N'Mutomo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12043', N'043', N'12', N'Kianjai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12044', N'044', N'12', N'Kenyatta University')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12045', N'045', N'12', N'St. Paul''s University')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12046', N'046', N'12', N'Moi University')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12047', N'047', N'12', N'Moi International Airport, Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12050', N'050', N'12', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'12099', N'099', N'12', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'14000', N'000', N'14', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'14004', N'004', N'14', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'14005', N'005', N'14', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'14006', N'006', N'14', N'Kitale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'14007', N'007', N'14', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'16000', N'000', N'16', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'16400', N'400', N'16', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'17000', N'000', N'17', N'Main Branch')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'17001', N'001', N'17', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'17002', N'002', N'17', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'18000', N'000', N'18', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'18002', N'002', N'18', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'18003', N'003', N'18', N'Milimani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'18004', N'004', N'18', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19000', N'000', N'19', N'Nairobi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19001', N'001', N'19', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19002', N'002', N'19', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19003', N'003', N'19', N'Uhuru Highway')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19004', N'004', N'19', N'River Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19006', N'006', N'19', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19007', N'007', N'19', N'Ruaraka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19008', N'008', N'19', N'Monrovia Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19009', N'009', N'19', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19010', N'010', N'19', N'Ngong Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19011', N'011', N'19', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19012', N'012', N'19', N'Embakasi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19013', N'013', N'19', N'Kericho')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19014', N'014', N'19', N'Changamwe')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19015', N'015', N'19', N'Bungoma')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19017', N'017', N'19', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'19018', N'018', N'19', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'20001', N'001', N'20', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'20002', N'002', N'20', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'20003', N'003', N'20', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'20004', N'004', N'20', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23000', N'000', N'23', N'Harambee Avenue Harambee Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23001', N'001', N'23', N'Murang''a')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23002', N'002', N'23', N'Embu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23003', N'003', N'23', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23004', N'004', N'23', N'Koinange Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23005', N'005', N'23', N'Thika')
GO
print 'Processed 500 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23006', N'006', N'23', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23007', N'007', N'23', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23009', N'009', N'23', N'Maua')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23010', N'010', N'23', N'Isiolo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23011', N'011', N'23', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23013', N'013', N'23', N'Umoja')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23014', N'014', N'23', N'River Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23015', N'015', N'23', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'23016', N'016', N'23', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'25000', N'000', N'25', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'25001', N'001', N'25', N'Koinange Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'25002', N'002', N'25', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'25003', N'003', N'25', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'25004', N'004', N'25', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'25005', N'005', N'25', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'25006', N'006', N'25', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26001', N'001', N'26', N'Nairobi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26002', N'002', N'26', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26003', N'003', N'26', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26005', N'005', N'26', N'MIA')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26006', N'006', N'26', N'JKIA')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26007', N'007', N'26', N'Kirinyaga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26008', N'008', N'26', N'Kabarak')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26009', N'009', N'26', N'Olenguruone')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26010', N'010', N'26', N'Kericho')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26011', N'011', N'26', N'Nandi Hills')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26012', N'012', N'26', N'EPZ')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26013', N'013', N'26', N'Sheikh Karume')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'26014', N'014', N'26', N'Kabarnet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30000', N'000', N'30', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30001', N'001', N'30', N'City Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30003', N'003', N'30', N'Village Market')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30004', N'004', N'30', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30005', N'005', N'30', N'Hurlingham')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30006', N'006', N'30', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30007', N'007', N'30', N'Parklands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30008', N'008', N'30', N'Riverside Mews')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30009', N'009', N'30', N'Iman Banking Centre Riverside Mews')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30010', N'010', N'30', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30011', N'011', N'30', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30012', N'012', N'30', N'Donholm')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30014', N'014', N'30', N'Ngara Mini Branch Peace Towers')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30015', N'015', N'30', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'30016', N'016', N'30', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31000', N'000', N'31', N'Clearing Centre,Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31002', N'002', N'31', N'Kenyatta Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31003', N'003', N'31', N'Digo Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31004', N'004', N'31', N'Waiyaki Way')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31005', N'005', N'31', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31006', N'006', N'31', N'Harambee Avenue Nairobi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31007', N'007', N'31', N'Chiromo Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31008', N'008', N'31', N'International House')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31009', N'009', N'31', N'Nkrumah')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31010', N'010', N'31', N'Upper Hill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31011', N'011', N'31', N'Naivasha')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31012', N'012', N'31', N'Westgate')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31013', N'013', N'31', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31014', N'014', N'31', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31015', N'015', N'31', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31016', N'016', N'31', N'Nyerere')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31017', N'017', N'31', N'Nanyuki')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31018', N'018', N'31', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31020', N'020', N'31', N'Gikomba')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31021', N'021', N'31', N'Ruaraka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31022', N'022', N'31', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31023', N'023', N'31', N'Karen')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31024', N'024', N'31', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'31999', N'999', N'31', N'Central Processing CfC Centre,HeadOffice')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'35000', N'000', N'35', N'Koinange Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'35001', N'001', N'35', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'35002', N'002', N'35', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'35004', N'004', N'35', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'35005', N'005', N'35', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'35006', N'006', N'35', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'35007', N'007', N'35', N'Libra House')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'35008', N'008', N'35', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39001', N'001', N'39', N'IPS')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39002', N'002', N'39', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39003', N'003', N'39', N'Upper Hill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39004', N'004', N'39', N'Parklands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39006', N'006', N'39', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39007', N'007', N'39', N'Watamu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39008', N'008', N'39', N'Diani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39009', N'009', N'39', N'Kilifi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39010', N'010', N'39', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39011', N'011', N'39', N'Karen')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39012', N'012', N'39', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39014', N'014', N'39', N'Changamwe')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39015', N'015', N'39', N'Riverside')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39016', N'016', N'39', N'Likoni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'39018', N'018', N'39', N'Village Market')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41000', N'000', N'41', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41101', N'101', N'41', N'City Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41102', N'102', N'41', N'NIC Hse')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41103', N'103', N'41', N'Harbor Hse')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41105', N'105', N'41', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41106', N'106', N'41', N'Junction')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41107', N'107', N'41', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41108', N'108', N'41', N'Nyali')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41109', N'109', N'41', N'Nkurumah Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41111', N'111', N'41', N'Prestige')
GO
print 'Processed 600 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41112', N'112', N'41', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41113', N'113', N'41', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41114', N'114', N'41', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41115', N'115', N'41', N'Galleria (Bomas)')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41116', N'116', N'41', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41117', N'117', N'41', N'Village Market')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'41118', N'118', N'41', N'Mombasa Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'42001', N'001', N'42', N'Banda Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'42004', N'004', N'42', N'Kimathi Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'42005', N'005', N'42', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'42006', N'006', N'42', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'42007', N'007', N'42', N'Parklands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43000', N'000', N'43', N'Ecobank Towers')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43002', N'002', N'43', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43003', N'003', N'43', N'Plaza 2000')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43004', N'004', N'43', N'Westminister')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43005', N'005', N'43', N'Chambers')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43007', N'007', N'43', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43008', N'008', N'43', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43009', N'009', N'43', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43010', N'010', N'43', N'Kitale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43011', N'011', N'43', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43012', N'012', N'43', N'Karatina')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43013', N'013', N'43', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43014', N'014', N'43', N'United Mall')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43015', N'015', N'43', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43016', N'016', N'43', N'Jomo Kenyatta Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43017', N'017', N'43', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43018', N'018', N'43', N'Busia')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43019', N'019', N'43', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43020', N'020', N'43', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43021', N'021', N'43', N'Gikomba')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43023', N'023', N'43', N'Valley Arcade')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'43100', N'100', N'43', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49000', N'000', N'49', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49001', N'001', N'49', N'Nairobi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49002', N'002', N'49', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49003', N'003', N'49', N'Westlands The Mall The Mall')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49005', N'005', N'49', N'Chester')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49007', N'007', N'49', N'Waiyaki Way')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49008', N'008', N'49', N'Kakamega')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49009', N'009', N'49', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49011', N'011', N'49', N'Nyali')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49012', N'012', N'49', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'49013', N'013', N'49', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'50000', N'000', N'50', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'50001', N'001', N'50', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'50002', N'002', N'50', N'Parklands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'50003', N'003', N'50', N'Koinange Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'5004', N'004', N'50', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'51000', N'000', N'51', N'Koinange Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53001', N'001', N'53', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53002', N'002', N'53', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53003', N'003', N'53', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53004', N'004', N'53', N'Lavington')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53005', N'005', N'53', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53006', N'006', N'53', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53007', N'007', N'53', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53008', N'008', N'53', N'Muthaiga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53010', N'010', N'53', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53011', N'011', N'53', N'Gikomba')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53012', N'012', N'53', N'Ngong Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'53013', N'013', N'53', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'54001', N'001', N'54', N'Nairobi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'54002', N'002', N'54', N'Riverside Drive')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'55001', N'001', N'55', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'55002', N'002', N'55', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'55004', N'004', N'55', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'55005', N'005', N'55', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'55006', N'006', N'55', N'Main Branch')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'55007', N'007', N'55', N'Mombasa Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57000', N'000', N'57', N'Kenyatta Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57001', N'001', N'57', N'2nd Ngong Avenue I & M Bank House')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57002', N'002', N'57', N'Sarit Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57003', N'003', N'57', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57004', N'004', N'57', N'Biashara')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57005', N'005', N'57', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57007', N'007', N'57', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57008', N'008', N'57', N'Karen')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57009', N'009', N'57', N'Panari Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57010', N'010', N'57', N'Parklands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57011', N'011', N'57', N'Wilson Airport')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57012', N'012', N'57', N'Ongata Rongai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57013', N'013', N'57', N'South C Shopping South C')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57014', N'014', N'57', N'Nyali Cinemax')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57015', N'015', N'57', N'Langata Link')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57016', N'016', N'57', N'Lavington')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57018', N'018', N'57', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'57098', N'098', N'57', N'Card Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'59001', N'001', N'59', N'Loita Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'59002', N'002', N'59', N'Ngong Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'60000', N'000', N'60', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'60001', N'001', N'60', N'City Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'60002', N'002', N'60', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'60003', N'003', N'60', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'60004', N'004', N'60', N'Diani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'60006', N'006', N'60', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63000', N'000', N'63', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63001', N'001', N'63', N'Nation Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63002', N'002', N'63', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63003', N'003', N'63', N'Kisumu')
GO
print 'Processed 700 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63005', N'005', N'63', N'Parklands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63006', N'006', N'63', N'Westgate')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63008', N'008', N'63', N'Mombasa Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63009', N'009', N'63', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63011', N'011', N'63', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63012', N'012', N'63', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63013', N'013', N'63', N'OTC')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63014', N'014', N'63', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63015', N'015', N'63', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63016', N'016', N'63', N'Changamwe')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63017', N'017', N'63', N'T-Mall')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63018', N'018', N'63', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63019', N'019', N'63', N'Village Market')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63020', N'020', N'63', N'Diani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63021', N'021', N'63', N'Bungoma')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63023', N'023', N'63', N'Prestige')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63024', N'024', N'63', N'Buru Buru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63025', N'025', N'63', N'Kitengela')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63026', N'026', N'63', N'Jomo Kenyatta')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63027', N'027', N'63', N'Kakamega')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63028', N'028', N'63', N'Kericho')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63029', N'029', N'63', N'Upper Hill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63030', N'030', N'63', N'Wabera Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63031', N'031', N'63', N'Karen')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63032', N'032', N'63', N'Voi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63034', N'034', N'63', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63035', N'035', N'63', N'Diamond Plaza')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63036', N'036', N'63', N'Cross Roads')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'63050', N'050', N'63', N'Tom Mboya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66000', N'000', N'66', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66001', N'001', N'66', N'Naivasha Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66002', N'002', N'66', N'Moi Avenue -Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66003', N'003', N'66', N'Kenyatta Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66004', N'004', N'66', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66005', N'005', N'66', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66007', N'007', N'66', N'Embu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66008', N'008', N'66', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66009', N'009', N'66', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66010', N'010', N'66', N'Kericho')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66011', N'011', N'66', N'Kangemi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66012', N'012', N'66', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66013', N'013', N'66', N'Kerugoya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66014', N'014', N'66', N'Kenyatta Mkt')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66015', N'015', N'66', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66016', N'016', N'66', N'Chuka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66017', N'017', N'66', N'Kitui')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66018', N'018', N'66', N'Machakos')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66019', N'019', N'66', N'Nanyuki')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66021', N'021', N'66', N'Emali')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66022', N'022', N'66', N'Naivasha')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66023', N'023', N'66', N'Nyahururu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66024', N'024', N'66', N'Isiolo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66025', N'025', N'66', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66026', N'026', N'66', N'Kitale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66027', N'027', N'66', N'Kibwezi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66028', N'028', N'66', N'Bungoma')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66031', N'031', N'66', N'Mtwapa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66033', N'033', N'66', N'Moi Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66034', N'034', N'66', N'Mwea')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66035', N'035', N'66', N'Kengeleni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'66036', N'036', N'66', N'Kilimani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68000', N'000', N'68', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68001', N'001', N'68', N'Corporate')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68002', N'002', N'68', N'Fourways')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68003', N'003', N'68', N'Kangema')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68004', N'004', N'68', N'Karatina')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68006', N'006', N'68', N'Muraradia')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68007', N'007', N'68', N'Kangari')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68008', N'008', N'68', N'Othaya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68009', N'009', N'68', N'Thika Plaza')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68010', N'010', N'68', N'Kerugoya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68011', N'011', N'68', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68012', N'012', N'68', N'Tom Mboya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68013', N'013', N'68', N'Nakuru Gatehouse Gate Hse')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68014', N'014', N'68', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68015', N'015', N'68', N'Mama Ngina')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68017', N'017', N'68', N'Community')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68018', N'018', N'68', N'Community Corporate')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68019', N'019', N'68', N'Embu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68020', N'020', N'68', N'Naivasha')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68021', N'021', N'68', N'Chuka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68022', N'022', N'68', N'Murang''a')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68023', N'023', N'68', N'Molo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68024', N'024', N'68', N'Harambee')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68025', N'025', N'68', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68026', N'026', N'68', N'Kimathi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68027', N'027', N'68', N'Nanyuki')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68029', N'029', N'68', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68030', N'030', N'68', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68031', N'031', N'68', N'Nakuru Kenyatta Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68032', N'032', N'68', N'Kariobangi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68033', N'033', N'68', N'Kitale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68034', N'034', N'68', N'Thika Kenyatta Highway')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68035', N'035', N'68', N'Knut Hse')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68036', N'036', N'68', N'Narok')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68037', N'037', N'68', N'Nkubu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68038', N'038', N'68', N'Mwea')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68040', N'040', N'68', N'Maua')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68041', N'041', N'68', N'Isiolo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68042', N'042', N'68', N'Kagio')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68043', N'043', N'68', N'Gikomba')
GO
print 'Processed 800 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68044', N'044', N'68', N'Ukunda')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68045', N'045', N'68', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68046', N'046', N'68', N'Mombasa Digo Rd Digo Rd')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68047', N'047', N'68', N'Moi Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68048', N'048', N'68', N'Bungoma')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68049', N'049', N'68', N'Kapsabet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68050', N'050', N'68', N'Kakamega')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68052', N'052', N'68', N'Nyamira')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68053', N'053', N'68', N'Litein')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68055', N'055', N'68', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68056', N'056', N'68', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68057', N'057', N'68', N'Kikuyu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68058', N'058', N'68', N'Garissa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68059', N'059', N'68', N'Mwingi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68060', N'060', N'68', N'Machakos')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68061', N'061', N'68', N'Ongata Rongai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68062', N'062', N'68', N'Ol-Kalou')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68064', N'064', N'68', N'Kiambu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68065', N'065', N'68', N'Kayole')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68066', N'066', N'68', N'Gatundu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68067', N'067', N'68', N'Wote')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68068', N'068', N'68', N'Mumias')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68069', N'069', N'68', N'Limuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68070', N'070', N'68', N'Kitengela')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68071', N'071', N'68', N'Githurai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68072', N'072', N'68', N'Kitui')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68073', N'073', N'68', N'Ngong')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68074', N'074', N'68', N'Loitoktok')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68076', N'076', N'68', N'Mbita')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68077', N'077', N'68', N'Gilgil')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68078', N'078', N'68', N'Busia')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68079', N'079', N'68', N'Voi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68080', N'080', N'68', N'Enterprise Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68081', N'081', N'68', N'Equity Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68082', N'082', N'68', N'Donholm')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68083', N'083', N'68', N'Mukurwe-ini')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68084', N'084', N'68', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68085', N'085', N'68', N'Namanga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68088', N'088', N'68', N'OTC')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68089', N'089', N'68', N'Kenol')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68090', N'090', N'68', N'Tala')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68091', N'091', N'68', N'Ngara')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68092', N'092', N'68', N'Nandi Hills')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68093', N'093', N'68', N'Githunguri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68094', N'094', N'68', N'Tea Room')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68095', N'095', N'68', N'Buru Buru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68096', N'096', N'68', N'Mbale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68097', N'097', N'68', N'Siaya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68098', N'098', N'68', N'Homa Bay')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68099', N'099', N'68', N'Lodwar')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68100', N'100', N'68', N'Mandera')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68101', N'101', N'68', N'Marsabit')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68102', N'102', N'68', N'Moyale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68103', N'103', N'68', N'Wajir')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68104', N'104', N'68', N'Meru Makutano')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68105', N'105', N'68', N'Malaba')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68106', N'106', N'68', N'Kilifi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68107', N'107', N'68', N'Kapenguria')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68108', N'108', N'68', N'Mombasa Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68110', N'110', N'68', N'Maralal')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68111', N'111', N'68', N'Kimende')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68112', N'112', N'68', N'Luanda')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68113', N'113', N'68', N'KU')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68114', N'114', N'68', N'Kengeleni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68115', N'115', N'68', N'Nyeri Kimathi Way EK Wachira Bldg')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68116', N'116', N'68', N'Migori')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68117', N'117', N'68', N'Kibera')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68118', N'118', N'68', N'Kasarani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68119', N'119', N'68', N'Mtwapa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68120', N'120', N'68', N'Changamwe')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68122', N'122', N'68', N'Bomet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68123', N'123', N'68', N'Kilgoris')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68124', N'124', N'68', N'Keroka')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68125', N'125', N'68', N'Karen')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68126', N'126', N'68', N'Kisumu Angawa Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68127', N'127', N'68', N'Mpeketoni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68128', N'128', N'68', N'Nairobi West')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68129', N'129', N'68', N'Kenyatta Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68130', N'130', N'68', N'City Hall')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'68131', N'131', N'68', N'Eldama Ravine')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70000', N'000', N'70', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70001', N'001', N'70', N'Kiambu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70002', N'002', N'70', N'Githunguri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70003', N'003', N'70', N'Sonalux')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70004', N'004', N'70', N'Gatundu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70005', N'005', N'70', N'Thika')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70006', N'006', N'70', N'Murang''a')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70007', N'007', N'70', N'kangari')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70008', N'008', N'70', N'Kiria-ini')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70009', N'009', N'70', N'Kangema')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70011', N'011', N'70', N'Othaya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70014', N'014', N'70', N'Cargen Hse')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70018', N'018', N'70', N'Nakuru Finance')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70019', N'019', N'70', N'Nakuru Njoro Hse')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70021', N'021', N'70', N'Dagoreti')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70023', N'023', N'70', N'Nyahururu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70024', N'024', N'70', N'Ruiru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70025', N'025', N'70', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70026', N'026', N'70', N'Nyamira')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70027', N'027', N'70', N'Kisii')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70031', N'031', N'70', N'Industrial Area')
GO
print 'Processed 900 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70033', N'033', N'70', N'Donholm')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70035', N'035', N'70', N'Fourways Retail')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70038', N'038', N'70', N'KTDA Plaza')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70041', N'041', N'70', N'Kariobangi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70042', N'042', N'70', N'Gikomba Area 42')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70043', N'043', N'70', N'Gikomba')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70045', N'045', N'70', N'Githurai')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70046', N'046', N'70', N'Kilimani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70047', N'047', N'70', N'Limuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70049', N'049', N'70', N'Kagwe')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70051', N'051', N'70', N'Banana')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70053', N'053', N'70', N'Naivasha')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70055', N'055', N'70', N'Nyeri')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70057', N'057', N'70', N'Kerugoya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70058', N'058', N'70', N'Tom Mboya')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70059', N'059', N'70', N'River Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70061', N'061', N'70', N'Kayole')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70062', N'062', N'70', N'Nkubu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70063', N'063', N'70', N'Meru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70065', N'065', N'70', N'KTDA Corporate')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70069', N'069', N'70', N'Kitui')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70071', N'071', N'70', N'Kitengela')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70072', N'072', N'70', N'Kitui')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70073', N'073', N'70', N'Machakos')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70075', N'075', N'70', N'Embu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70077', N'077', N'70', N'Bungoma')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70078', N'078', N'70', N'Kakamega')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70079', N'079', N'70', N'Busia')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70083', N'083', N'70', N'Molo')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70085', N'085', N'70', N'Eldoret')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70095', N'095', N'70', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70096', N'096', N'70', N'Kenyatta Avenue,Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'70097', N'097', N'70', N'Kapsabet')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72000', N'000', N'72', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72001', N'001', N'72', N'Central Clearing Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72002', N'002', N'72', N'Upperhill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72003', N'003', N'72', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72004', N'004', N'72', N'Kenyatta Avenue')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72005', N'005', N'72', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72007', N'007', N'72', N'Lamu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72008', N'008', N'72', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72009', N'009', N'72', N'Muthaiga')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'72010', N'010', N'72', N'Bondeni')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74001', N'001', N'74', N'Wabera Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74002', N'002', N'74', N'Eastleigh')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74003', N'003', N'74', N'Mombasa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74004', N'004', N'74', N'Garissa')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74005', N'005', N'74', N'Eastleigh II')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74006', N'006', N'74', N'Malindi')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74007', N'007', N'74', N'Kisumu')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74008', N'008', N'74', N'Kimathi Street')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74009', N'009', N'74', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74010', N'010', N'74', N'South C')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74011', N'011', N'74', N'Industrial Area')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74012', N'012', N'74', N'Masalani')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74013', N'013', N'74', N'Habaswein')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74014', N'014', N'74', N'Wajir')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74015', N'015', N'74', N'Moyale')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74016', N'016', N'74', N'Nakuru')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74017', N'017', N'74', N'Mombasa 11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'74999', N'999', N'74', N'Head Office/Clearing Centre')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'76001', N'001', N'76', N'Westlands')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'76002', N'002', N'76', N'Enterprise Road')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'76003', N'003', N'76', N'Upper Hill')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'76099', N'099', N'76', N'Head Office')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [Bank], [BranchName]) VALUES (N'95000', N'000', N'59', N'Head Office')
/****** Object:  Table [dbo].[Settings]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Settings](
	[SSKey] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SSValue] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SSValueType] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SGroup] [int] NOT NULL,
	[SSSystem] [bit] NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SSKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'DEFCONTR', N'20000', N'N', N'Max Defined Contribution', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'DEFCONTRSCHEME', N'2', N'E1,2,3', N'Default contribution scheme', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'EMPNSSF', N'200', N'N', N'Employers NSFF contribution', 4, 1)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'MAXTRIES', N'3', N'N', N'Maximum password retries', 7, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'MINAGE', N'18', N'N', N'Minimum employement age', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'MRELIEF', N'1162', N'N', N'Married persons relief', 2, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'NSSFMAX', N'200', N'N', N'Max NSSF contribution', 4, 1)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'NSSFVAL', N'5', N'N', N'NSSFVAL', 4, 1)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PAYEEMIN', N'11136', N'N', N'Minimum earning to start charging PAYE', 5, 1)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PAYSLIPDETAILS', N'1', N'N', N'Payslip details layout', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PAYSLIPPERPAGE', N'2', N'N', N'Payslips per page. Valid values 1 or 2', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PAYSLIPTYPE', N'VIKE', N'S', N'Payslip to display', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PEN1E', N'6', N'N', N'Employee''s pension contribution %', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PEN1R', N'7.5', N'N', N'Employer''s pension contribution %', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PRELIEF', N'1162', N'N', N'Personal relief', 2, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PWDSIZE', N'5', N'N', N'Password size', 7, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'REPORTPATH', N'C:\Projects\SPPayroll\Dev\Reports', N'S', N'Report Output Path', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'RESOURCEPATH', N'C:\SPPayroll\Resource', N'S', N'Resource Path', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'SPLANMAX', N'4000', N'N', N'Savings Plan Maximum', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'SRELIEF', N'1162', N'N', N'Single Personal Relief', 2, 0)
/****** Object:  Table [dbo].[PayslipDet]    Script Date: 12/04/2012 17:55:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipDet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayslipDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmpTxnId] [int] NOT NULL,
	[Period] [int] NOT NULL,
	[Year] [int] NULL,
	[Description] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TaxTracking] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Amount] [decimal](18, 0) NOT NULL,
	[DEType] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsStatutory] [bit] NOT NULL,
	[ShowInPayslip] [bit] NULL,
	[YTD] [decimal](18, 0) NULL,
 CONSTRAINT [PK_PayslipDet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[PayslipDet] ON
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1615, N'E1        ', 1871, 1, 2012, N'PAYE', N'PAYE      ', CAST(5300 AS Decimal(18, 0)), N'D', 1, 1, CAST(5300 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1616, N'E1        ', 1872, 1, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1617, N'E1        ', 1873, 1, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(320 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1618, N'E1        ', 1874, 1, 2012, N'BASIC', N'EARNING   ', CAST(37890 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1619, N'E2        ', 1876, 1, 2012, N'PAYE', N'PAYE      ', CAST(5385 AS Decimal(18, 0)), N'D', 1, 1, CAST(5385 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1620, N'E2        ', 1877, 1, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1621, N'E2        ', 1878, 1, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(320 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1622, N'E2        ', 1879, 1, 2012, N'BASIC', N'EARNING   ', CAST(38230 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1623, N'E3        ', 1881, 1, 2012, N'PAYE', N'PAYE      ', CAST(14239 AS Decimal(18, 0)), N'D', 1, 1, CAST(14239 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1624, N'E3        ', 1882, 1, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1625, N'E3        ', 1883, 1, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(320 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1626, N'E3        ', 1896, 1, 2012, N'ADVANCE', N'COLLECTION', CAST(6789 AS Decimal(18, 0)), N'D', 0, 1, CAST(6789 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1627, N'E3        ', 1884, 1, 2012, N'BASIC', N'EARNING   ', CAST(67890 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1628, N'E4        ', 1886, 1, 2012, N'PAYE', N'PAYE      ', CAST(0 AS Decimal(18, 0)), N'D', 1, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1629, N'E4        ', 1887, 1, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1630, N'E4        ', 1888, 1, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(320 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1631, N'E4        ', 1897, 1, 2012, N'ADVANCE', N'COLLECTION', CAST(5656 AS Decimal(18, 0)), N'D', 0, 1, CAST(5656 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1632, N'E4        ', 1889, 1, 2012, N'BASIC', N'EARNING   ', CAST(67876 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1633, N'E4        ', 1899, 1, 2012, N'NONCASHBENEFIT', N'EARNING   ', CAST(183680 AS Decimal(18, 0)), N'E', 0, 1, CAST(183680 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1634, N'E5        ', 1891, 1, 2012, N'PAYE', N'PAYE      ', CAST(21102 AS Decimal(18, 0)), N'D', 1, 1, CAST(21102 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1635, N'E5        ', 1892, 1, 2012, N'NSSF', N'DEDUCTIBLE', CAST(200 AS Decimal(18, 0)), N'D', 1, 1, CAST(200 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1636, N'E5        ', 1893, 1, 2012, N'NHIF', N'COLLECTION', CAST(320 AS Decimal(18, 0)), N'D', 1, 1, CAST(320 AS Decimal(18, 0)))
INSERT [dbo].[PayslipDet] ([Id], [EmpNo], [EmpTxnId], [Period], [Year], [Description], [TaxTracking], [Amount], [DEType], [IsStatutory], [ShowInPayslip], [YTD]) VALUES (1637, N'E5        ', 1894, 1, 2012, N'BASIC', N'EARNING   ', CAST(90765 AS Decimal(18, 0)), N'E', 0, 1, CAST(0 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[PayslipDet] OFF
/****** Object:  StoredProcedure [dbo].[CopyPayMaster]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyPayMaster]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CopyPayMaster]
	/*
	(
	@parameter1 int = 5,
	@parameter2 datatype OUTPUT
	)
	*/
AS
	/* SET NOCOUNT ON */
INSERT INTO PayslipMaster
                         (Period, Year, EmpNo, PaymentDate, PrintedBy, PrintedOn, EmpName, PayPoint, PIN, EmpGroup, EmpPayroll, CompName, CompAddr, CompTel, PayeTax, BasicPay, 
                         Benefits, Variables, OtherDeductions, GrossTaxableEarnings, NetTaxableEarnings, MortgageRelief, GrossTax, PersonalRelief, PensionEmployer, EmployerNSSF, 
                         PensionEmployee, BankBranch, Account, NSSF, NHIF, NetPay, Department, NSSFNo, NHIFNo)
SELECT        Period, Year, EmpNo, PaymentDate, PrintedBy, PrintedOn, EmpName, PayPoint, PIN, EmpGroup, EmpPayroll, CompName, CompAddr, CompTel, PayeTax, BasicPay, 
                         Benefits, Variables, OtherDeductions, GrossTaxableEarnings, NetTaxableEarnings, MortgageRelief, GrossTax, PersonalRelief, PensionEmployer, EmployerNSSF, 
                         PensionEmployee, BankBranch, Account, NSSF, NHIF, NetPay, Department, NSSFNo, NHIFNo
FROM            PayslipMaster_Temp
SELECT        Period, [Year], EmpNo, PaymentDate, PrintedBy, PrintedOn, EmpName, PayPoint, PIN, EmpGroup, EmpPayroll, CompName, CompAddr, CompTel, PayeTax, BasicPay, 
                         Benefits, Variables, OtherDeductions, GrossTaxableEarnings, NetTaxableEarnings, MortgageRelief, GrossTax, PersonalRelief, PensionEmployer, EmployerNSSF, 
                         PensionEmployee, BankBranch, Account, NSSF, NHIF, NetPay, Department, NSSFNo, NHIFNo
FROM            PayslipMaster_Temp
	RETURN
' 
END
GO
/****** Object:  View [dbo].[vwEmpNonCashBenefits]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwEmpNonCashBenefits]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwEmpNonCashBenefits]
AS
SELECT     dbo.EmpNonCashBenefits.EmpNo, dbo.EmpNonCashBenefits.BenefitId, dbo.EmpNonCashBenefits.Quantity, dbo.Benefits.Name, dbo.Benefits.Rate
FROM         dbo.Benefits INNER JOIN
                      dbo.EmpNonCashBenefits ON dbo.Benefits.BenefitId = dbo.EmpNonCashBenefits.BenefitId
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwEmpNonCashBenefits', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[31] 4[30] 2[21] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Benefits"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 99
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "EmpNonCashBenefits"
            Begin Extent = 
               Top = 6
               Left = 227
               Bottom = 99
               Right = 378
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwEmpNonCashBenefits'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'vwEmpNonCashBenefits', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwEmpNonCashBenefits'
GO
/****** Object:  View [dbo].[vwPayslipDet_Temp]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayslipDet_Temp]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwPayslipDet_Temp]
AS
SELECT     dbo.Employee.EmpNo, dbo.Employee.Surname, dbo.Employee.OtherNames, dbo.EmployeeTransactions.ItemId, dbo.EmployeeTransactions.Balance, 
                      CAST(CAST(dbo.PayslipDet_Temp.Year AS varchar) + ''-'' + CAST(dbo.PayslipDet_Temp.Period AS varchar) + ''-'' + CAST(1 AS varchar) AS DATETIME) AS TxnDate, 
                      dbo.PayrollItems.ReFField, dbo.EmployeeTransactions.Amount AS RepayAmount, dbo.EmployeeTransactions.PostDate, dbo.EmployeeTransactions.InitialAmount, 
                      dbo.EmployeeTransactions.LoanType, dbo.Employee.Employer, dbo.Employee.BankAccount, dbo.Employee.BankCode, dbo.Employee.PaymentMode, 
                      dbo.PayrollItems.ItemType, dbo.PayrollItemType.Parent, dbo.PayslipDet_Temp.EmpTxnId, dbo.PayslipDet_Temp.Period, dbo.PayslipDet_Temp.Year, 
                      dbo.PayslipDet_Temp.Description, dbo.PayslipDet_Temp.TaxTracking, dbo.PayslipDet_Temp.Amount, dbo.PayslipDet_Temp.DEType, 
                      dbo.PayslipDet_Temp.IsStatutory, dbo.PayslipDet_Temp.ShowInPayslip, dbo.PayslipDet_Temp.YTD
FROM         dbo.Employee INNER JOIN
                      dbo.EmployeeTransactions ON dbo.Employee.EmpNo = dbo.EmployeeTransactions.EmpNo INNER JOIN
                      dbo.PayrollItems ON dbo.EmployeeTransactions.ItemId = dbo.PayrollItems.ItemId INNER JOIN
                      dbo.PayrollItemType ON dbo.PayrollItems.ItemType = dbo.PayrollItemType.PayrollItemTypeId INNER JOIN
                      dbo.PayslipDet_Temp ON dbo.EmployeeTransactions.Id = dbo.PayslipDet_Temp.EmpTxnId
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayslipDet_Temp', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[21] 4[29] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Employee"
            Begin Extent = 
               Top = 11
               Left = 14
               Bottom = 119
               Right = 179
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "EmployeeTransactions"
            Begin Extent = 
               Top = 9
               Left = 267
               Bottom = 117
               Right = 452
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItems"
            Begin Extent = 
               Top = 32
               Left = 526
               Bottom = 140
               Right = 677
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItemType"
            Begin Extent = 
               Top = 126
               Left = 269
               Bottom = 219
               Right = 437
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayslipDet_Temp"
            Begin Extent = 
               Top = 126
               Left = 68
               Bottom = 234
               Right = 219
            End
            DisplayFlags = 280
            TopColumn = 8
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 28
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayslipDet_Temp'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayslipDet_Temp', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayslipDet_Temp'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayslipDet_Temp', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayslipDet_Temp'
GO
/****** Object:  View [dbo].[vwPayslipDet]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayslipDet]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwPayslipDet]
AS
SELECT     dbo.EmployeeTransactions.Id, dbo.Employee.EmpNo, dbo.Employee.Surname, dbo.Employee.OtherNames, dbo.EmployeeTransactions.ItemId, 
                      dbo.EmployeeTransactions.Balance, dbo.PayrollItems.ReFField, dbo.EmployeeTransactions.Amount AS RepayAmount, dbo.PayslipDet.Period, dbo.PayslipDet.Year, 
                      CAST(CAST(dbo.PayslipDet.Year AS varchar) + ''-'' + CAST(dbo.PayslipDet.Period AS varchar) + ''-'' + CAST(1 AS varchar) AS DATETIME) AS TxnDate, 
                      dbo.EmployeeTransactions.PostDate, dbo.PayslipDet.Description, dbo.PayslipDet.YTD, dbo.EmployeeTransactions.InitialAmount, 
                      dbo.EmployeeTransactions.LoanType, dbo.Employee.Employer, dbo.Employee.BankAccount, dbo.Employee.BankCode, dbo.Employee.PaymentMode, 
                      dbo.PayrollItems.ItemType, dbo.PayslipDet.DEType, dbo.PayslipDet.EmpTxnId, dbo.PayslipDet.TaxTracking, dbo.PayslipDet.ShowInPayslip, 
                      dbo.PayslipDet.IsStatutory, dbo.PayslipDet.Amount, dbo.PayrollItemType.Parent
FROM         dbo.Employee INNER JOIN
                      dbo.EmployeeTransactions ON dbo.Employee.EmpNo = dbo.EmployeeTransactions.EmpNo INNER JOIN
                      dbo.PayrollItems ON dbo.EmployeeTransactions.ItemId = dbo.PayrollItems.ItemId INNER JOIN
                      dbo.PayslipDet ON dbo.EmployeeTransactions.Id = dbo.PayslipDet.EmpTxnId INNER JOIN
                      dbo.PayrollItemType ON dbo.PayrollItems.ItemType = dbo.PayrollItemType.PayrollItemTypeId
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayslipDet', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[44] 4[7] 2[45] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[47] 4[21] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Employee"
            Begin Extent = 
               Top = 5
               Left = 11
               Bottom = 214
               Right = 135
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "EmployeeTransactions"
            Begin Extent = 
               Top = 6
               Left = 177
               Bottom = 272
               Right = 355
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItems"
            Begin Extent = 
               Top = 31
               Left = 579
               Bottom = 265
               Right = 762
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayslipDet"
            Begin Extent = 
               Top = 0
               Left = 407
               Bottom = 231
               Right = 558
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItemType"
            Begin Extent = 
               Top = 119
               Left = 808
               Bottom = 212
               Right = 972
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 28
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Wi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayslipDet'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayslipDet', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'dth = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 9900
         Alias = 1170
         Table = 4380
         Output = 1215
         Append = 1400
         NewValue = 1170
         SortType = 690
         SortOrder = 585
         GroupBy = 1350
         Filter = 735
         Or = 705
         Or = 570
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayslipDet'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayslipDet', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayslipDet'
GO
/****** Object:  View [dbo].[vwPayrollMaster_Temp]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayrollMaster_Temp]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwPayrollMaster_Temp]
AS
SELECT     dbo.Banks.BankName, dbo.BankBranch.BranchName, dbo.Employee.Surname, dbo.Employee.OtherNames, dbo.BankBranch.BranchCode, 
                      dbo.BankBranch.BankSortCode, dbo.Employee.PaymentMode, dbo.Employer.BankBranchSortCode, dbo.Employee.BankAccount, dbo.Employee.IDNo, 
                      dbo.Banks.BankCode, dbo.PayslipMaster_Temp.Period, dbo.PayslipMaster_Temp.Year, dbo.PayslipMaster_Temp.PaymentDate, dbo.PayslipMaster_Temp.PrintedBy, 
                      dbo.PayslipMaster_Temp.PrintedOn, dbo.PayslipMaster_Temp.EmpName, dbo.PayslipMaster_Temp.PayPoint, dbo.PayslipMaster_Temp.PIN, 
                      dbo.PayslipMaster_Temp.NHIFNo, dbo.PayslipMaster_Temp.Department, dbo.PayslipMaster_Temp.NSSFNo, dbo.PayslipMaster_Temp.EmpGroup, 
                      dbo.PayslipMaster_Temp.EmpPayroll, dbo.PayslipMaster_Temp.CompName, dbo.PayslipMaster_Temp.CompAddr, dbo.PayslipMaster_Temp.CompTel, 
                      dbo.PayslipMaster_Temp.PayeTax, dbo.PayslipMaster_Temp.BasicPay, dbo.PayslipMaster_Temp.Benefits, dbo.PayslipMaster_Temp.OtherDeductions, 
                      dbo.PayslipMaster_Temp.GrossTaxableEarnings, dbo.PayslipMaster_Temp.NetTaxableEarnings, dbo.PayslipMaster_Temp.GrossTax, 
                      dbo.PayslipMaster_Temp.EmployerNSSF, dbo.PayslipMaster_Temp.PensionEmployee, dbo.PayslipMaster_Temp.BankBranch, dbo.PayslipMaster_Temp.Account, 
                      dbo.PayslipMaster_Temp.NSSF, dbo.PayslipMaster_Temp.NHIF, dbo.PayslipMaster_Temp.NetPay, dbo.Employee.EmpNo, dbo.PayslipMaster_Temp.Variables, 
                      dbo.PayslipMaster_Temp.MortgageRelief, dbo.PayslipMaster_Temp.PersonalRelief, dbo.PayslipMaster_Temp.PensionEmployer
FROM         dbo.Employee INNER JOIN
                      dbo.PayslipMaster_Temp ON dbo.Employee.EmpNo = dbo.PayslipMaster_Temp.EmpNo LEFT OUTER JOIN
                      dbo.Banks INNER JOIN
                      dbo.BankBranch ON dbo.Banks.BankCode = dbo.BankBranch.Bank ON dbo.Employee.BankCode = dbo.BankBranch.BankSortCode CROSS JOIN
                      dbo.Employer
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayrollMaster_Temp', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[45] 4[30] 2[12] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4[30] 2[40] 3) )"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1[53] 4) )"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4[60] 2) )"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4) )"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 8
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Employee"
            Begin Extent = 
               Top = 0
               Left = 187
               Bottom = 108
               Right = 352
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayslipMaster_Temp"
            Begin Extent = 
               Top = 110
               Left = 170
               Bottom = 218
               Right = 356
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Banks"
            Begin Extent = 
               Top = 6
               Left = 465
               Bottom = 84
               Right = 616
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BankBranch"
            Begin Extent = 
               Top = 6
               Left = 654
               Bottom = 114
               Right = 805
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employer"
            Begin Extent = 
               Top = 99
               Left = 499
               Bottom = 207
               Right = 680
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      PaneHidden = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 45
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayrollMaster_Temp'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayrollMaster_Temp', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'       Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1515
         Table = 2130
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayrollMaster_Temp'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayrollMaster_Temp', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayrollMaster_Temp'
GO
/****** Object:  View [dbo].[vwPayrollMaster]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayrollMaster]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwPayrollMaster]
AS
SELECT     dbo.Banks.BankName, dbo.BankBranch.BranchName, dbo.Employee.EmpNo, dbo.Employee.Surname, dbo.Employee.OtherNames, dbo.PayslipMaster.NetPay, 
                      dbo.PayslipMaster.GrossTaxableEarnings, dbo.PayslipMaster.BasicPay, dbo.PayslipMaster.NHIF, dbo.PayslipMaster.NSSF, dbo.PayslipMaster.PayeTax, 
                      dbo.PayslipMaster.OtherDeductions, dbo.PayslipMaster.PensionEmployee, dbo.PayslipMaster.Period, dbo.PayslipMaster.Year, dbo.PayslipMaster.Benefits, 
                      dbo.PayslipMaster.EmployerNSSF, dbo.PayslipMaster.EmpName, dbo.PayslipMaster.CompName, dbo.PayslipMaster.NetTaxableEarnings, 
                      dbo.PayslipMaster.PrintedOn, dbo.PayslipMaster.BankBranch, dbo.PayslipMaster.Account, dbo.PayslipMaster.GrossTax, dbo.PayslipMaster.CompTel, 
                      dbo.PayslipMaster.CompAddr, dbo.PayslipMaster.PIN, dbo.BankBranch.BranchCode, dbo.BankBranch.BankSortCode, dbo.Employee.PaymentMode, 
                      dbo.Employer.BankBranchSortCode, dbo.PayslipMaster.NHIFNo, dbo.PayslipMaster.NSSFNo, dbo.PayslipMaster.PayPoint, dbo.PayslipMaster.PrintedBy, 
                      dbo.PayslipMaster.PaymentDate, dbo.Employee.BankAccount, dbo.PayslipMaster.EmpGroup, dbo.PayslipMaster.EmpPayroll, dbo.PayslipMaster.Department, 
                      dbo.Employee.IDNo, dbo.Banks.BankCode, dbo.PayslipMaster.Variables, dbo.PayslipMaster.MortgageRelief, dbo.PayslipMaster.PersonalRelief, 
                      dbo.PayslipMaster.PensionEmployer
FROM         dbo.PayslipMaster INNER JOIN
                      dbo.Employee ON dbo.PayslipMaster.EmpNo = dbo.Employee.EmpNo LEFT OUTER JOIN
                      dbo.Banks INNER JOIN
                      dbo.BankBranch ON dbo.Banks.BankCode = dbo.BankBranch.Bank ON dbo.Employee.BankCode = dbo.BankBranch.BankSortCode CROSS JOIN
                      dbo.Employer
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayrollMaster', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[37] 4[4] 2[39] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[50] 2[25] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4[30] 2[40] 3) )"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[56] 3) )"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2[66] 3) )"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4[50] 3) )"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 4
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -96
         Left = -96
      End
      Begin Tables = 
         Begin Table = "PayslipMaster"
            Begin Extent = 
               Top = 6
               Left = 617
               Bottom = 266
               Right = 803
            End
            DisplayFlags = 280
            TopColumn = 21
         End
         Begin Table = "Employee"
            Begin Extent = 
               Top = 4
               Left = 422
               Bottom = 215
               Right = 575
            End
            DisplayFlags = 280
            TopColumn = 9
         End
         Begin Table = "Banks"
            Begin Extent = 
               Top = 21
               Left = 14
               Bottom = 117
               Right = 165
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BankBranch"
            Begin Extent = 
               Top = 21
               Left = 199
               Bottom = 154
               Right = 360
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employer"
            Begin Extent = 
               Top = 0
               Left = 820
               Bottom = 108
               Right = 1001
            End
            DisplayFlags = 280
            TopColumn = 10
         End
      End
   End
   Begin SQLPane = 
      PaneHidden = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 40
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayrollMaster'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayrollMaster', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
      Begin ColumnWidths = 11
         Column = 3525
         Alias = 1845
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayrollMaster'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayrollMaster', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayrollMaster'
GO
/****** Object:  View [dbo].[vwBankBranches]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwBankBranches]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwBankBranches]
AS
SELECT     dbo.Banks.BankName + '' - '' + dbo.BankBranch.BranchName AS BankBranchName, dbo.BankBranch.BankSortCode, dbo.Banks.BankName, dbo.BankBranch.Bank, 
                      dbo.BankBranch.BranchCode, dbo.BankBranch.BranchName, dbo.Banks.BankCode
FROM         dbo.BankBranch INNER JOIN
                      dbo.Banks ON dbo.BankBranch.Bank = dbo.Banks.BankCode
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwBankBranches', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[36] 4[28] 2[10] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "BankBranch"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 181
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Banks"
            Begin Extent = 
               Top = 6
               Left = 227
               Bottom = 169
               Right = 378
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 2475
         Width = 2535
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwBankBranches'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'vwBankBranches', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwBankBranches'
GO
/****** Object:  View [dbo].[GetEmpTransactions]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetEmpTransactions]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[GetEmpTransactions]
AS
SELECT     dbo.Employee.EmpNo, dbo.Employee.Surname, dbo.Employee.MaritalStatus, dbo.Employee.IsActive, dbo.EmployeeTransactions.ItemId, 
                      dbo.EmployeeTransactions.Amount, dbo.EmployeeTransactions.Recurrent, dbo.EmployeeTransactions.Enabled, dbo.Employee.BasicPay, 
                      dbo.Departments.Description AS Department, dbo.Departments.Code AS DeptCode, dbo.EmployeeTransactions.TrackYTD AS EmpTrack, 
                      dbo.EmployeeTransactions.Balance, dbo.PayrollItems.ItemType, dbo.PayrollItems.TaxTracking, dbo.PayrollItems.Active, dbo.PayrollItems.AddToPension, 
                      dbo.EmployeeTransactions.ShowYTDInPayslip, dbo.EmployeeTransactions.Id AS EmpTxnId, dbo.EmployeeTransactions.Processed, 
                      dbo.PayrollItemType.Parent
FROM         dbo.Employee INNER JOIN
                      dbo.EmployeeTransactions ON dbo.Employee.EmpNo = dbo.EmployeeTransactions.EmpNo INNER JOIN
                      dbo.Departments ON dbo.Employee.Department = dbo.Departments.Code INNER JOIN
                      dbo.PayrollItems ON dbo.EmployeeTransactions.ItemId = dbo.PayrollItems.ItemId INNER JOIN
                      dbo.PayrollItemType ON dbo.PayrollItems.ItemType = dbo.PayrollItemType.PayrollItemTypeId
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'GetEmpTransactions', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[39] 4[18] 2[19] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4[30] 2[40] 3) )"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 3
   End
   Begin DiagramPane = 
      PaneHidden = 
      Begin Origin = 
         Top = -96
         Left = -288
      End
      Begin Tables = 
         Begin Table = "Employee"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 172
               Right = 191
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "EmployeeTransactions"
            Begin Extent = 
               Top = 6
               Left = 229
               Bottom = 224
               Right = 397
            End
            DisplayFlags = 280
            TopColumn = 8
         End
         Begin Table = "Departments"
            Begin Extent = 
               Top = 0
               Left = 404
               Bottom = 78
               Right = 555
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "PayrollItems"
            Begin Extent = 
               Top = 53
               Left = 409
               Bottom = 194
               Right = 560
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItemType"
            Begin Extent = 
               Top = 43
               Left = 584
               Bottom = 136
               Right = 748
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetEmpTransactions'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'GetEmpTransactions', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'       Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetEmpTransactions'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPaneCount' , N'SCHEMA',N'dbo', N'VIEW',N'GetEmpTransactions', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetEmpTransactions'
GO
/****** Object:  StoredProcedure [dbo].[CopyPayslipDet]    Script Date: 12/04/2012 17:55:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyPayslipDet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[CopyPayslipDet]
	
		/*(

	@Period int,
	@Year int
	
	)
	*/
AS
	/* SET NOCOUNT ON */
INSERT INTO PayslipDet
                         (EmpTxnId, EmpNo, Period, Year, Description, TaxTracking, Amount, DEType, IsStatutory, ShowInPayslip, YTD)
SELECT        EmpTxnId, EmpNo, Period, Year, Description, TaxTracking, Amount, DEType, IsStatutory, ShowInPayslip, YTD
FROM            PayslipDet_Temp


/*Update YTD for items that are tracked*/
UPDATE       EmployeeTransactions
SET                Balance = PayslipDet_Temp.Amount + EmployeeTransactions.Balance
FROM            EmployeeTransactions INNER JOIN
                         PayslipDet_Temp ON EmployeeTransactions.Id = PayslipDet_Temp.EmpTxnId
WHERE        (EmployeeTransactions.TrackYTD = 1)

/*Update processed flag for items that are not recurrent so that they are not 
processed again*/
UPDATE    EmployeeTransactions
SET       Processed = 1
WHERE     (Recurrent = 0)
	RETURN
' 
END
GO
/****** Object:  ForeignKey [FK_BankBranch_Banks]    Script Date: 12/04/2012 17:55:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BankBranch_Banks]') AND parent_object_id = OBJECT_ID(N'[dbo].[BankBranch]'))
ALTER TABLE [dbo].[BankBranch]  WITH CHECK ADD  CONSTRAINT [FK_BankBranch_Banks] FOREIGN KEY([Bank])
REFERENCES [dbo].[Banks] ([BankCode])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BankBranch_Banks]') AND parent_object_id = OBJECT_ID(N'[dbo].[BankBranch]'))
ALTER TABLE [dbo].[BankBranch] CHECK CONSTRAINT [FK_BankBranch_Banks]
GO
/****** Object:  ForeignKey [FK_PayrollItems_PayrollItemType]    Script Date: 12/04/2012 17:55:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayrollItems_PayrollItemType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayrollItems]'))
ALTER TABLE [dbo].[PayrollItems]  WITH CHECK ADD  CONSTRAINT [FK_PayrollItems_PayrollItemType] FOREIGN KEY([ItemType])
REFERENCES [dbo].[PayrollItemType] ([PayrollItemTypeId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayrollItems_PayrollItemType]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayrollItems]'))
ALTER TABLE [dbo].[PayrollItems] CHECK CONSTRAINT [FK_PayrollItems_PayrollItemType]
GO
/****** Object:  ForeignKey [FK_PayslipDet_TaxTracking]    Script Date: 12/04/2012 17:55:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayslipDet_TaxTracking]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayslipDet]'))
ALTER TABLE [dbo].[PayslipDet]  WITH CHECK ADD  CONSTRAINT [FK_PayslipDet_TaxTracking] FOREIGN KEY([TaxTracking])
REFERENCES [dbo].[TaxTracking] ([TaxTrackingId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayslipDet_TaxTracking]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayslipDet]'))
ALTER TABLE [dbo].[PayslipDet] CHECK CONSTRAINT [FK_PayslipDet_TaxTracking]
GO
/****** Object:  ForeignKey [FK_Settings_SettingsGroup]    Script Date: 12/04/2012 17:55:45 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Settings_SettingsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Settings]'))
ALTER TABLE [dbo].[Settings]  WITH CHECK ADD  CONSTRAINT [FK_Settings_SettingsGroup] FOREIGN KEY([SGroup])
REFERENCES [dbo].[SettingsGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Settings_SettingsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Settings]'))
ALTER TABLE [dbo].[Settings] CHECK CONSTRAINT [FK_Settings_SettingsGroup]
GO
