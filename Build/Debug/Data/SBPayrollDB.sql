/****** Object:  Database [SBPayrollDB3]    Script Date: 04/15/2015 23:58:58 ******/
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'SBPayrollVersion' , NULL,NULL, NULL,NULL, NULL,NULL))
EXEC dbo.sp_addextendedproperty @name=N'SBPayrollVersion', @value=N'1.0.0.0'
GO
/****** Object:  ForeignKey [FK_BankBranch_Banks]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BankBranch_Banks]') AND parent_object_id = OBJECT_ID(N'[dbo].[BankBranch]'))
ALTER TABLE [dbo].[BankBranch] DROP CONSTRAINT [FK_BankBranch_Banks]
GO
/****** Object:  ForeignKey [FK_Employees_Employers]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employees_Employers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employees]'))
ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Employers]
GO
/****** Object:  ForeignKey [FK_PayrollItems_TaxTracking]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayrollItems_TaxTracking]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayrollItems]'))
ALTER TABLE [dbo].[PayrollItems] DROP CONSTRAINT [FK_PayrollItems_TaxTracking]
GO
/****** Object:  ForeignKey [FK_Settings_SettingsGroup]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Settings_SettingsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Settings]'))
ALTER TABLE [dbo].[Settings] DROP CONSTRAINT [FK_Settings_SettingsGroup]
GO
/****** Object:  ForeignKey [FK_spAllowedReportsRolesMenus_spReportsMenuItems]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_spAllowedReportsRolesMenus_spReportsMenuItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[spAllowedReportsRolesMenus]'))
ALTER TABLE [dbo].[spAllowedReportsRolesMenus] DROP CONSTRAINT [FK_spAllowedReportsRolesMenus_spReportsMenuItems]
GO
/****** Object:  ForeignKey [FK_spAllowedRoleMenus_spMenuItems]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_spAllowedRoleMenus_spMenuItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[spAllowedRoleMenus]'))
ALTER TABLE [dbo].[spAllowedRoleMenus] DROP CONSTRAINT [FK_spAllowedRoleMenus_spMenuItems]
GO
/****** Object:  View [dbo].[GetEmpTransactions]    Script Date: 04/15/2015 23:58:59 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetEmpTransactions]'))
DROP VIEW [dbo].[GetEmpTransactions]
GO
/****** Object:  View [dbo].[vwBankBranches]    Script Date: 04/15/2015 23:58:59 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwBankBranches]'))
DROP VIEW [dbo].[vwBankBranches]
GO
/****** Object:  View [dbo].[vwPayrollMaster]    Script Date: 04/15/2015 23:58:59 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayrollMaster]'))
DROP VIEW [dbo].[vwPayrollMaster]
GO
/****** Object:  View [dbo].[vwPayrollMaster_Temp]    Script Date: 04/15/2015 23:58:59 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayrollMaster_Temp]'))
DROP VIEW [dbo].[vwPayrollMaster_Temp]
GO
/****** Object:  View [dbo].[vwPayslipDet]    Script Date: 04/15/2015 23:58:59 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayslipDet]'))
DROP VIEW [dbo].[vwPayslipDet]
GO
/****** Object:  View [dbo].[vwPayslipDet_Temp]    Script Date: 04/15/2015 23:58:59 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayslipDet_Temp]'))
DROP VIEW [dbo].[vwPayslipDet_Temp]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
DROP TABLE [dbo].[Settings]
GO
/****** Object:  StoredProcedure [dbo].[CopyPayMaster]    Script Date: 04/15/2015 23:58:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyPayMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CopyPayMaster]
GO
/****** Object:  StoredProcedure [dbo].[CopyPayslipDet]    Script Date: 04/15/2015 23:58:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CopyPayslipDet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[CopyPayslipDet]
GO
/****** Object:  Table [dbo].[BankBranch]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankBranch]') AND type in (N'U'))
DROP TABLE [dbo].[BankBranch]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employees]') AND type in (N'U'))
DROP TABLE [dbo].[Employees]
GO
/****** Object:  Table [dbo].[PayrollItems]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollItems]') AND type in (N'U'))
DROP TABLE [dbo].[PayrollItems]
GO
/****** Object:  Table [dbo].[spAllowedReportsRolesMenus]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spAllowedReportsRolesMenus]') AND type in (N'U'))
DROP TABLE [dbo].[spAllowedReportsRolesMenus]
GO
/****** Object:  Table [dbo].[spAllowedRoleMenus]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spAllowedRoleMenus]') AND type in (N'U'))
DROP TABLE [dbo].[spAllowedRoleMenus]
GO
/****** Object:  Table [dbo].[spAllowedRoleMenusweb]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spAllowedRoleMenusweb]') AND type in (N'U'))
DROP TABLE [dbo].[spAllowedRoleMenusweb]
GO
/****** Object:  Table [dbo].[spMenuItems]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spMenuItems]') AND type in (N'U'))
DROP TABLE [dbo].[spMenuItems]
GO
/****** Object:  Table [dbo].[spMenus]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spMenus]') AND type in (N'U'))
DROP TABLE [dbo].[spMenus]
GO
/****** Object:  Table [dbo].[spReportsMenuItems]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spReportsMenuItems]') AND type in (N'U'))
DROP TABLE [dbo].[spReportsMenuItems]
GO
/****** Object:  Table [dbo].[spRoles]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spRoles]') AND type in (N'U'))
DROP TABLE [dbo].[spRoles]
GO
/****** Object:  Table [dbo].[spUsers]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spUsers]') AND type in (N'U'))
DROP TABLE [dbo].[spUsers]
GO
/****** Object:  Table [dbo].[spUsersInRoles]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spUsersInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[spUsersInRoles]
GO
/****** Object:  Table [dbo].[TaxTracking]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaxTracking]') AND type in (N'U'))
DROP TABLE [dbo].[TaxTracking]
GO
/****** Object:  Table [dbo].[TechParams]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechParams]') AND type in (N'U'))
DROP TABLE [dbo].[TechParams]
GO
/****** Object:  Table [dbo].[TransactionDef]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TransactionDef]') AND type in (N'U'))
DROP TABLE [dbo].[TransactionDef]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
DROP TABLE [dbo].[Transactions]
GO
/****** Object:  Table [dbo].[TxnType]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TxnType]') AND type in (N'U'))
DROP TABLE [dbo].[TxnType]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND type in (N'U'))
DROP TABLE [dbo].[UserProfile]
GO
/****** Object:  Table [dbo].[PayrollItemType]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollItemType]') AND type in (N'U'))
DROP TABLE [dbo].[PayrollItemType]
GO
/****** Object:  Table [dbo].[Payrolls]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payrolls]') AND type in (N'U'))
DROP TABLE [dbo].[Payrolls]
GO
/****** Object:  Table [dbo].[PayrollTypes]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollTypes]') AND type in (N'U'))
DROP TABLE [dbo].[PayrollTypes]
GO
/****** Object:  Table [dbo].[PayslipDet]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipDet]') AND type in (N'U'))
DROP TABLE [dbo].[PayslipDet]
GO
/****** Object:  Table [dbo].[PayslipDet_Temp]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipDet_Temp]') AND type in (N'U'))
DROP TABLE [dbo].[PayslipDet_Temp]
GO
/****** Object:  Table [dbo].[PayslipMaster]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipMaster]') AND type in (N'U'))
DROP TABLE [dbo].[PayslipMaster]
GO
/****** Object:  Table [dbo].[PayslipMaster_Temp]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipMaster_Temp]') AND type in (N'U'))
DROP TABLE [dbo].[PayslipMaster_Temp]
GO
/****** Object:  Table [dbo].[EmployeesBankTransfers]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeesBankTransfers]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeesBankTransfers]
GO
/****** Object:  Table [dbo].[EmployeeTransactions]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeTransactions]') AND type in (N'U'))
DROP TABLE [dbo].[EmployeeTransactions]
GO
/****** Object:  Table [dbo].[EmployerBanks]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployerBanks]') AND type in (N'U'))
DROP TABLE [dbo].[EmployerBanks]
GO
/****** Object:  Table [dbo].[Employers]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employers]') AND type in (N'U'))
DROP TABLE [dbo].[Employers]
GO
/****** Object:  Table [dbo].[EmpNonCashBenefits]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpNonCashBenefits]') AND type in (N'U'))
DROP TABLE [dbo].[EmpNonCashBenefits]
GO
/****** Object:  Table [dbo].[Banks]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Banks]') AND type in (N'U'))
DROP TABLE [dbo].[Banks]
GO
/****** Object:  Table [dbo].[Benefits]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Benefits]') AND type in (N'U'))
DROP TABLE [dbo].[Benefits]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Departments]') AND type in (N'U'))
DROP TABLE [dbo].[Departments]
GO
/****** Object:  Table [dbo].[Employee_Ext]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee_Ext]') AND type in (N'U'))
DROP TABLE [dbo].[Employee_Ext]
GO
/****** Object:  Table [dbo].[Employee_Ext_Fields]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee_Ext_Fields]') AND type in (N'U'))
DROP TABLE [dbo].[Employee_Ext_Fields]
GO
/****** Object:  Table [dbo].[HourlyPayment]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HourlyPayment]') AND type in (N'U'))
DROP TABLE [dbo].[HourlyPayment]
GO
/****** Object:  Table [dbo].[LeaveTransactions]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LeaveTransactions]') AND type in (N'U'))
DROP TABLE [dbo].[LeaveTransactions]
GO
/****** Object:  Table [dbo].[NHIFRates]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NHIFRates]') AND type in (N'U'))
DROP TABLE [dbo].[NHIFRates]
GO
/****** Object:  Table [dbo].[PackedTransactions]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PackedTransactions]') AND type in (N'U'))
DROP TABLE [dbo].[PackedTransactions]
GO
/****** Object:  Table [dbo].[PayeeRates]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayeeRates]') AND type in (N'U'))
DROP TABLE [dbo].[PayeeRates]
GO
/****** Object:  Table [dbo].[SettingsGroup]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingsGroup]') AND type in (N'U'))
DROP TABLE [dbo].[SettingsGroup]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))
DROP TABLE [dbo].[Accounts]
GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_Membership]') AND type in (N'U'))
DROP TABLE [dbo].[webpages_Membership]
GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_OAuthMembership]') AND type in (N'U'))
DROP TABLE [dbo].[webpages_OAuthMembership]
GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_Roles]') AND type in (N'U'))
DROP TABLE [dbo].[webpages_Roles]
GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 04/15/2015 23:58:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_UsersInRoles]') AND type in (N'U'))
DROP TABLE [dbo].[webpages_UsersInRoles]
GO
/****** Object:  Schema [mato]    Script Date: 04/15/2015 23:58:59 ******/
IF  EXISTS (SELECT * FROM sys.schemas WHERE name = N'mato')
DROP SCHEMA [mato]
GO
/****** Object:  Role [eve]    Script Date: 04/15/2015 23:58:59 ******/
DECLARE @RoleName sysname
set @RoleName = N'eve'
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = @RoleName AND type = 'R')
Begin
	DECLARE @RoleMemberName sysname
	DECLARE Member_Cursor CURSOR FOR
	select [name]
	from sys.database_principals 
	where principal_id in ( 
		select member_principal_id 
		from sys.database_role_members 
		where role_principal_id in (
			select principal_id
			FROM sys.database_principals where [name] = @RoleName  AND type = 'R' ))

	OPEN Member_Cursor;

	FETCH NEXT FROM Member_Cursor
	into @RoleMemberName

	WHILE @@FETCH_STATUS = 0
	BEGIN

		exec sp_droprolemember @rolename=@RoleName, @membername= @RoleMemberName

		FETCH NEXT FROM Member_Cursor
		into @RoleMemberName
	END;

	CLOSE Member_Cursor;
	DEALLOCATE Member_Cursor;
End
GO
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'eve' AND type = 'R')
DROP ROLE [eve]
GO
/****** Object:  Role [francis]    Script Date: 04/15/2015 23:58:59 ******/
DECLARE @RoleName sysname
set @RoleName = N'francis'
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = @RoleName AND type = 'R')
Begin
	DECLARE @RoleMemberName sysname
	DECLARE Member_Cursor CURSOR FOR
	select [name]
	from sys.database_principals 
	where principal_id in ( 
		select member_principal_id 
		from sys.database_role_members 
		where role_principal_id in (
			select principal_id
			FROM sys.database_principals where [name] = @RoleName  AND type = 'R' ))

	OPEN Member_Cursor;

	FETCH NEXT FROM Member_Cursor
	into @RoleMemberName

	WHILE @@FETCH_STATUS = 0
	BEGIN

		exec sp_droprolemember @rolename=@RoleName, @membername= @RoleMemberName

		FETCH NEXT FROM Member_Cursor
		into @RoleMemberName
	END;

	CLOSE Member_Cursor;
	DEALLOCATE Member_Cursor;
End
GO
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'francis' AND type = 'R')
DROP ROLE [francis]
GO
/****** Object:  Role [joan]    Script Date: 04/15/2015 23:58:59 ******/
DECLARE @RoleName sysname
set @RoleName = N'joan'
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = @RoleName AND type = 'R')
Begin
	DECLARE @RoleMemberName sysname
	DECLARE Member_Cursor CURSOR FOR
	select [name]
	from sys.database_principals 
	where principal_id in ( 
		select member_principal_id 
		from sys.database_role_members 
		where role_principal_id in (
			select principal_id
			FROM sys.database_principals where [name] = @RoleName  AND type = 'R' ))

	OPEN Member_Cursor;

	FETCH NEXT FROM Member_Cursor
	into @RoleMemberName

	WHILE @@FETCH_STATUS = 0
	BEGIN

		exec sp_droprolemember @rolename=@RoleName, @membername= @RoleMemberName

		FETCH NEXT FROM Member_Cursor
		into @RoleMemberName
	END;

	CLOSE Member_Cursor;
	DEALLOCATE Member_Cursor;
End
GO
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'joan' AND type = 'R')
DROP ROLE [joan]
GO
/****** Object:  Role [job]    Script Date: 04/15/2015 23:58:59 ******/
DECLARE @RoleName sysname
set @RoleName = N'job'
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = @RoleName AND type = 'R')
Begin
	DECLARE @RoleMemberName sysname
	DECLARE Member_Cursor CURSOR FOR
	select [name]
	from sys.database_principals 
	where principal_id in ( 
		select member_principal_id 
		from sys.database_role_members 
		where role_principal_id in (
			select principal_id
			FROM sys.database_principals where [name] = @RoleName  AND type = 'R' ))

	OPEN Member_Cursor;

	FETCH NEXT FROM Member_Cursor
	into @RoleMemberName

	WHILE @@FETCH_STATUS = 0
	BEGIN

		exec sp_droprolemember @rolename=@RoleName, @membername= @RoleMemberName

		FETCH NEXT FROM Member_Cursor
		into @RoleMemberName
	END;

	CLOSE Member_Cursor;
	DEALLOCATE Member_Cursor;
End
GO
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'job' AND type = 'R')
DROP ROLE [job]
GO
/****** Object:  Role [Kevin]    Script Date: 04/15/2015 23:58:59 ******/
DECLARE @RoleName sysname
set @RoleName = N'Kevin'
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = @RoleName AND type = 'R')
Begin
	DECLARE @RoleMemberName sysname
	DECLARE Member_Cursor CURSOR FOR
	select [name]
	from sys.database_principals 
	where principal_id in ( 
		select member_principal_id 
		from sys.database_role_members 
		where role_principal_id in (
			select principal_id
			FROM sys.database_principals where [name] = @RoleName  AND type = 'R' ))

	OPEN Member_Cursor;

	FETCH NEXT FROM Member_Cursor
	into @RoleMemberName

	WHILE @@FETCH_STATUS = 0
	BEGIN

		exec sp_droprolemember @rolename=@RoleName, @membername= @RoleMemberName

		FETCH NEXT FROM Member_Cursor
		into @RoleMemberName
	END;

	CLOSE Member_Cursor;
	DEALLOCATE Member_Cursor;
End
GO
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'Kevin' AND type = 'R')
DROP ROLE [Kevin]
GO
/****** Object:  Role [mato]    Script Date: 04/15/2015 23:58:59 ******/
DECLARE @RoleName sysname
set @RoleName = N'mato'
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = @RoleName AND type = 'R')
Begin
	DECLARE @RoleMemberName sysname
	DECLARE Member_Cursor CURSOR FOR
	select [name]
	from sys.database_principals 
	where principal_id in ( 
		select member_principal_id 
		from sys.database_role_members 
		where role_principal_id in (
			select principal_id
			FROM sys.database_principals where [name] = @RoleName  AND type = 'R' ))

	OPEN Member_Cursor;

	FETCH NEXT FROM Member_Cursor
	into @RoleMemberName

	WHILE @@FETCH_STATUS = 0
	BEGIN

		exec sp_droprolemember @rolename=@RoleName, @membername= @RoleMemberName

		FETCH NEXT FROM Member_Cursor
		into @RoleMemberName
	END;

	CLOSE Member_Cursor;
	DEALLOCATE Member_Cursor;
End
GO
IF  EXISTS (SELECT * FROM sys.database_principals WHERE name = N'mato' AND type = 'R')
DROP ROLE [mato]
GO
/****** Object:  Role [eve]    Script Date: 04/15/2015 23:58:59 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'eve')
BEGIN
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'eve' AND type = 'R')
CREATE ROLE [eve] AUTHORIZATION [dbo]

END
GO
/****** Object:  Role [francis]    Script Date: 04/15/2015 23:58:59 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'francis')
BEGIN
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'francis' AND type = 'R')
CREATE ROLE [francis] AUTHORIZATION [dbo]

END
GO
/****** Object:  Role [joan]    Script Date: 04/15/2015 23:58:59 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'joan')
BEGIN
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'joan' AND type = 'R')
CREATE ROLE [joan] AUTHORIZATION [dbo]

END
GO
/****** Object:  Role [job]    Script Date: 04/15/2015 23:58:59 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'job')
BEGIN
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'job' AND type = 'R')
CREATE ROLE [job] AUTHORIZATION [dbo]

END
GO
/****** Object:  Role [Kevin]    Script Date: 04/15/2015 23:58:59 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'Kevin')
BEGIN
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'Kevin' AND type = 'R')
CREATE ROLE [Kevin] AUTHORIZATION [dbo]

END
GO
/****** Object:  Role [mato]    Script Date: 04/15/2015 23:58:59 ******/
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'mato')
BEGIN
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'mato' AND type = 'R')
CREATE ROLE [mato] AUTHORIZATION [dbo]

END
GO
/****** Object:  Schema [mato]    Script Date: 04/15/2015 23:58:59 ******/
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'mato')
EXEC sys.sp_executesql N'CREATE SCHEMA [mato] AUTHORIZATION [mato]'
GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_UsersInRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_webpages_UsersInRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (4, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (4, 2)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (4, 3)
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_Roles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_webpages_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[webpages_Roles] ON
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'Administrator')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'Employee')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (3, N'Customer')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (4, N'Registered User')
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_OAuthMembership]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProviderUserId] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_webpages_OAuthMembership] PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[webpages_Membership]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [smalldatetime] NULL,
	[ConfirmationToken] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [smalldatetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PasswordChangedDate] [smalldatetime] NULL,
	[PasswordSalt] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PasswordVerificationTokenExpirationDate] [smalldatetime] NULL,
 CONSTRAINT [PK_webpages_Membership] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (2, CAST(0xA3A80147 AS SmallDateTime), NULL, 1, CAST(0xA417018E AS SmallDateTime), 1, N'AD2S8hQbSHPK6cHC1vUia14Sf1oRrU6jMzMS8AYa7zEIUfOxkjTD/D6UT8VrOKFyxA==', CAST(0xA3A80147 AS SmallDateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (5, CAST(0xA413021F AS SmallDateTime), NULL, 1, NULL, 0, N'ABo9P8cQzsEIDiXLLUPl1kHize6xSKjoi43I4pxyOuKWxzbMXNOBDAHce8HFXdsYHg==', CAST(0xA413021F AS SmallDateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (6, CAST(0xA41400E9 AS SmallDateTime), NULL, 1, NULL, 0, N'AAzJHounAtvFJuQqEUPZ9oa7j9KzqkKc+yyTdBotfVPJeVkwdKrD4qNyPv7WujzfpA==', CAST(0xA41400E9 AS SmallDateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (7, CAST(0xA4140135 AS SmallDateTime), NULL, 1, NULL, 0, N'ANgIJsoF80b1o3OoH6z3po0IntyiZwQfjAUQIEQDNJ8ceyYiBKClO++Um82QDBT7bw==', CAST(0xA4140135 AS SmallDateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (8, CAST(0xA414022F AS SmallDateTime), NULL, 1, NULL, 0, N'AGRgKrxLAZe94NV9m/Xif7IMOWyg1t6By5fqZUg0MTkFx1oo7HZ+KS+vcJnpmbI5tA==', CAST(0xA414022F AS SmallDateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (9, CAST(0xA4250427 AS SmallDateTime), NULL, 1, NULL, 0, N'AOdn9KHJ6fY8maLXts7y2LU/pj9DcsgisbY5DhPhW83F94q2HPbxYhnPWcFkcVRgXg==', CAST(0xA4250427 AS SmallDateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (10, CAST(0xA4270036 AS SmallDateTime), NULL, 1, NULL, 0, N'AIReWECy+M2qOM/F8TeHUWqZd5qVzj1BGUwJPWWdhirRbWvO3SIuSdpPWjmJGpscWQ==', CAST(0xA4270036 AS SmallDateTime), N'', NULL, NULL)
/****** Object:  Table [dbo].[Accounts]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Accounts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Accounts](
	[Id] [int] NOT NULL,
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AccountType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BookBalance] [money] NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[SettingsGroup]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SettingsGroup]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SettingsGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Parent] [int] NOT NULL,
 CONSTRAINT [PK_SettingsGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[SettingsGroup] ON
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (1, N'Settings', 0)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (2, N'Statutory Computations', 1)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (3, N'General', 1)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (4, N'NSSF', 6)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (5, N'PAYE', 2)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (6, N'Pension', 2)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (7, N'Security', 1)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (8, N'Reports', 1)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (9, N'Font Size', 8)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (10, N'Payslip', 9)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (11, N'Font Family', 8)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (12, N'All Payslip', 9)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (13, N'Payslip', 11)
INSERT [dbo].[SettingsGroup] ([Id], [GroupName], [Parent]) VALUES (14, N'All Payslip', 11)
SET IDENTITY_INSERT [dbo].[SettingsGroup] OFF
/****** Object:  Table [dbo].[PayeeRates]    Script Date: 04/15/2015 23:58:58 ******/
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
/****** Object:  Table [dbo].[PackedTransactions]    Script Date: 04/15/2015 23:58:58 ******/
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
	[EmployeeId] [int] NOT NULL,
	[TxnCode] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Amount] [smallmoney] NOT NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Authorized] [bit] NULL,
 CONSTRAINT [PK_PackedTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[NHIFRates]    Script Date: 04/15/2015 23:58:58 ******/
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
/****** Object:  Table [dbo].[LeaveTransactions]    Script Date: 04/15/2015 23:58:58 ******/
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
	[LeaveDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NoofDays] [int] NULL,
 CONSTRAINT [PK_LeaveTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[HourlyPayment]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[HourlyPayment]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[HourlyPayment](
	[EmployeeId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Employee_Ext_Fields]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employee_Ext_Fields]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employee_Ext_Fields](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[FType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Employee_Ext_Fields] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Employee_Ext]    Script Date: 04/15/2015 23:58:58 ******/
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
/****** Object:  Table [dbo].[Departments]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Departments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Departments] ON
INSERT [dbo].[Departments] ([Id], [Code], [Description], [IsDeleted]) VALUES (49, N'FN', N'Finance', 0)
INSERT [dbo].[Departments] ([Id], [Code], [Description], [IsDeleted]) VALUES (50, N'PR', N'Production', 0)
SET IDENTITY_INSERT [dbo].[Departments] OFF
/****** Object:  Table [dbo].[Benefits]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Benefits]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Benefits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rate] [money] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Benefits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Benefits] ON
INSERT [dbo].[Benefits] ([Id], [Description], [Rate], [IsDeleted]) VALUES (20, N'Driver', 5600.0000, 0)
INSERT [dbo].[Benefits] ([Id], [Description], [Rate], [IsDeleted]) VALUES (21, N'Aya', 8900.0000, 0)
INSERT [dbo].[Benefits] ([Id], [Description], [Rate], [IsDeleted]) VALUES (22, N'Gardener', 8590.0000, 0)
SET IDENTITY_INSERT [dbo].[Benefits] OFF
/****** Object:  Table [dbo].[Banks]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Banks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Banks](
	[BankCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BankName] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Banks] PRIMARY KEY CLUSTERED 
(
	[BankCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'01', N'Kenya Commercial Bank Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'02', N'Standard Chartered Bank')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'03', N'Barclays Bank Of Kenya Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'05', N'Bank Of India')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'06', N'Bank of Baroda (Kenya) Limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'07', N'Commercial Bank Of Africa Ltd')
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
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'59', N'Development Bank Of Kenya Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'60', N'Fidelity Commercial Bank Ltd.')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'63', N'Diamond Trust Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'66', N'K-Rep Bank limited')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'68', N'Equity Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'70', N'Family Bank ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'72', N'Gulf African Bank')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'74', N'First Community Bank Ltd')
INSERT [dbo].[Banks] ([BankCode], [BankName]) VALUES (N'76', N'UBA Kenya Bank')
/****** Object:  Table [dbo].[EmpNonCashBenefits]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmpNonCashBenefits]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmpNonCashBenefits](
	[EmployeeId] [int] NOT NULL,
	[EmpNo] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BenefitId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[Rate] [money] NOT NULL,
 CONSTRAINT [PK_EmpNonCashBenefits] PRIMARY KEY CLUSTERED 
(
	[EmpNo] ASC,
	[BenefitId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Employers]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Telephone] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address1] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address2] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Slogan] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AuthorizedSignatory] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PIN] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NHIF] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSF] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BankBranchSortCode] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccountName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccountNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Logo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDefault] [bit] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Employer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[Employers] ON
INSERT [dbo].[Employers] ([Id], [Name], [Email], [Telephone], [Address1], [Address2], [Slogan], [AuthorizedSignatory], [PIN], [NHIF], [NSSF], [BankBranchSortCode], [AccountName], [AccountNo], [Logo], [IsDefault], [IsActive], [IsDeleted]) VALUES (11, N'Software Providers', N'info@softwareproviders.co.ke', N'0717769376', N'34534, Kiserian', N'Nairobi', N'Solutions For All', N'Director', N'12345', N'9809', N'8790', N'01102', N'Salries Account', N'12345', N'D:\data\img\3d\3D Wallpapers Collection 01 (www.yaftabad.com).jpg', 0, 1, 0)
INSERT [dbo].[Employers] ([Id], [Name], [Email], [Telephone], [Address1], [Address2], [Slogan], [AuthorizedSignatory], [PIN], [NHIF], [NSSF], [BankBranchSortCode], [AccountName], [AccountNo], [Logo], [IsDefault], [IsActive], [IsDeleted]) VALUES (20, N'Ukiristo Na Ufanisi Sacco', N'd@s.a', N'0715413144', N'00100, Kiserian', N'Nairobi', N'Finacial Aid For Financial Freedom', N'Director', N'23423', N'323123', N'323423', N'01102', N'Salary', N'24332423423', N'D:\data\img\3d\3D Wallpapers Collection 10 (www.yaftabad.com).jpg', 1, 1, 0)
INSERT [dbo].[Employers] ([Id], [Name], [Email], [Telephone], [Address1], [Address2], [Slogan], [AuthorizedSignatory], [PIN], [NHIF], [NSSF], [BankBranchSortCode], [AccountName], [AccountNo], [Logo], [IsDefault], [IsActive], [IsDeleted]) VALUES (21, N'Zetech College', N'k@w.g', N'0789435678', N'00100, Ngong', N'Nairobi', N'Education Is Power', N'Principal', N'12345', N'12345', N'12345', N'50002', N'Fees Account', N'123456789', N'D:\data\img\3d\3D Wallpapers Collection 07 (www.yaftabad.com).jpg', 0, 1, 0)
INSERT [dbo].[Employers] ([Id], [Name], [Email], [Telephone], [Address1], [Address2], [Slogan], [AuthorizedSignatory], [PIN], [NHIF], [NSSF], [BankBranchSortCode], [AccountName], [AccountNo], [Logo], [IsDefault], [IsActive], [IsDeleted]) VALUES (22, N'Software Providers', N'info@softwareproviders.co.ke', N'254717769329', N'00100, Kiserian', N'Nairobi', N'Solutions For All', N'Director', N'123456', N'123456', N'123456', N'123456', N'Utility Account', N'123456', N'greenmage.jpg', 0, 1, 0)
SET IDENTITY_INSERT [dbo].[Employers] OFF
/****** Object:  Table [dbo].[EmployerBanks]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployerBanks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployerBanks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployerId] [int] NULL,
	[BankSortCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccountName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccountNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Signatory] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsDefault] [bit] NULL,
 CONSTRAINT [PK_EmployerBanks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[EmployeeTransactions]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeeTransactions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployeeTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostDate] [date] NOT NULL,
	[EmpNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Recurrent] [bit] NOT NULL,
	[ItemId] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Amount] [money] NOT NULL,
	[Balance] [money] NULL,
	[InitialAmount] [money] NULL,
	[Processed] [bit] NULL,
	[TrackYTD] [bit] NULL,
	[ShowYTDInPayslip] [bit] NULL,
	[CreatedBy] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastChangedBy] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastChangeDate] [date] NULL,
	[AuthorizedBy] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AuthorizeDate] [date] NULL,
	[LoanType] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
/****** Object:  Table [dbo].[EmployeesBankTransfers]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmployeesBankTransfers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmployeesBankTransfers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BankSortCode] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmployerBankId] [int] NULL,
	[EmployerId] [int] NULL,
	[EmployeeId] [int] NULL,
 CONSTRAINT [PK_EmployeesBankTransfers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[PayslipMaster_Temp]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipMaster_Temp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayslipMaster_Temp](
	[Period] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[EmpNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[PaymentDate] [date] NOT NULL,
	[PrintedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PrintedOn] [datetime] NOT NULL,
	[EmpName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PayPoint] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PIN] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NHIFNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSFNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Department] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpGroup] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpPayroll] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompAddr] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompTel] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayeTax] [money] NOT NULL,
	[BasicPay] [money] NOT NULL,
	[Benefits] [money] NOT NULL,
	[Variables] [money] NOT NULL,
	[OtherDeductions] [money] NOT NULL,
	[GrossTaxableEarnings] [money] NOT NULL,
	[NetTaxableEarnings] [money] NOT NULL,
	[MortgageRelief] [money] NOT NULL,
	[InsuranceRelief] [money] NOT NULL,
	[GrossTax] [money] NOT NULL,
	[PersonalRelief] [money] NOT NULL,
	[PensionEmployer] [money] NOT NULL,
	[EmployerNSSF] [money] NOT NULL,
	[PensionEmployee] [money] NOT NULL,
	[BankBranch] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Account] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSF] [money] NOT NULL,
	[NHIF] [money] NOT NULL,
	[NetPay] [money] NOT NULL,
 CONSTRAINT [PK_PayslipMaster_Temp] PRIMARY KEY CLUSTERED 
(
	[Period] ASC,
	[Year] ASC,
	[EmpNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[PayslipMaster]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipMaster]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayslipMaster](
	[Period] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[EmpNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[PaymentDate] [date] NOT NULL,
	[PrintedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PrintedOn] [datetime] NOT NULL,
	[EmpName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PayPoint] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PIN] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NHIFNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSFNo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Department] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpGroup] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpPayroll] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompAddr] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CompTel] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayeTax] [money] NOT NULL,
	[BasicPay] [money] NOT NULL,
	[Benefits] [money] NOT NULL,
	[Variables] [money] NOT NULL,
	[OtherDeductions] [money] NOT NULL,
	[GrossTaxableEarnings] [money] NOT NULL,
	[NetTaxableEarnings] [money] NOT NULL,
	[MortgageRelief] [money] NOT NULL,
	[InsuranceRelief] [money] NOT NULL,
	[GrossTax] [money] NOT NULL,
	[PersonalRelief] [money] NOT NULL,
	[PensionEmployer] [money] NOT NULL,
	[EmployerNSSF] [money] NOT NULL,
	[PensionEmployee] [money] NOT NULL,
	[BankBranch] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Account] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NSSF] [money] NOT NULL,
	[NHIF] [money] NOT NULL,
	[NetPay] [money] NOT NULL,
 CONSTRAINT [PK_PayslipMaster] PRIMARY KEY CLUSTERED 
(
	[Period] ASC,
	[Year] ASC,
	[EmpNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[PayslipDet_Temp]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipDet_Temp]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayslipDet_Temp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[EmpTxnId] [int] NOT NULL,
	[Period] [int] NOT NULL,
	[Year] [int] NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
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
/****** Object:  Table [dbo].[PayslipDet]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayslipDet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayslipDet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[EmpTxnId] [int] NOT NULL,
	[Period] [int] NOT NULL,
	[Year] [int] NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TaxTracking] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
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
/****** Object:  Table [dbo].[PayrollTypes]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayrollTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Remarks] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_PayrollTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[PayrollTypes] ON
INSERT [dbo].[PayrollTypes] ([Id], [Description], [Remarks]) VALUES (1, N'Law Allowance', N'The Law Allowance is used to pay special separation allowances to qualified retired law enforcement officers through the month in which the retiree reaches age 62.')
INSERT [dbo].[PayrollTypes] ([Id], [Description], [Remarks]) VALUES (2, N'Disability', N'Eligible employees who become temporarily or permanently disabled and are unable to perform their regular work duties may receive partial replacement income.It is the responsibility of the employer and the employee’s physician to determine if the employee is eligible.')
INSERT [dbo].[PayrollTypes] ([Id], [Description], [Remarks]) VALUES (3, N'End-Month', N'Salaried employees are processed on a repeating payroll and paid on the monthly payroll cycle.  Benefits for salaried employees vary, based on the amount of time worked.  Full-time salaried employees earn all regular state employee benefits.')
INSERT [dbo].[PayrollTypes] ([Id], [Description], [Remarks]) VALUES (4, N'Longevity', N'The Longevity  is designed for permanent employees that have provided at least ten years of service  ')
INSERT [dbo].[PayrollTypes] ([Id], [Description], [Remarks]) VALUES (5, N'Student', N'The Student payroll is used to pay students that are employed by and enrolled in an institution as full-time students')
INSERT [dbo].[PayrollTypes] ([Id], [Description], [Remarks]) VALUES (6, N'Temporary', N'The Temporary Payroll is used to pay employees who have temporary employment assignments.  This includes hourly temporary employees , and some temporary salary staff.')
INSERT [dbo].[PayrollTypes] ([Id], [Description], [Remarks]) VALUES (7, N'Over-Time Pay', N'Eligible salaried employees are paid 1.5% for hours worked in excess of 40 hours within a week, with the exception of those employees that are considered exempt.  The exempt or non-exempt status of any particular employee must be determined on the basis of whether duties, responsibilities and salary meet the requirements for exemption.')
INSERT [dbo].[PayrollTypes] ([Id], [Description], [Remarks]) VALUES (8, N'On-Call Pay', N'Additional compensation is paid to designated employees, regardless of appointment type, who are required to serve in “on-call” status.  On-call status applies to an employee that is required to wear a pager and is on stand-by in case of emergency. ')
INSERT [dbo].[PayrollTypes] ([Id], [Description], [Remarks]) VALUES (9, N'Holiday Premium Pay', N'Employees who are required to work on designated state holidays are given, in addition to regular salary, premium pay equal to one-half of their regular straight-time hourly rate for hours worked on state holidays.')
SET IDENTITY_INSERT [dbo].[PayrollTypes] OFF
/****** Object:  Table [dbo].[Payrolls]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payrolls]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Payrolls](
	[Period] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[EmployerId] [int] NULL,
	[DateRun] [datetime] NULL,
	[RunBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Approved] [bit] NULL,
	[ApprovedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
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
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (1, 2014, 11, CAST(0x0000A47B00000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (2, 2014, 11, CAST(0x0000A47B00000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (3, 2014, 11, CAST(0x0000A37100000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (4, 2014, 11, CAST(0x0000A37100000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (5, 2014, 11, CAST(0x0000A3A700000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (6, 2014, 11, CAST(0x0000A3AB00000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (7, 2014, 11, CAST(0x0000A3AC00000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (8, 2014, 11, CAST(0x0000A36F00000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (9, 2014, 11, CAST(0x0000A36F00000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (10, 2014, 11, CAST(0x0000A36F00000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (11, 2014, 11, CAST(0x0000A36F00000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (11, 2015, 11, CAST(0x0000A3EE00000000 AS DateTime), N'sys', 0, N'', 1, 0)
INSERT [dbo].[Payrolls] ([Period], [Year], [EmployerId], [DateRun], [RunBy], [Approved], [ApprovedBy], [IsOpen], [Processed]) VALUES (12, 2014, 11, CAST(0x0000A36F00000000 AS DateTime), N'sys', 0, N'', 1, 0)
/****** Object:  Table [dbo].[PayrollItemType]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollItemType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayrollItemType](
	[Id] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Parent] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_PayrollItemType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'ADDITION', N'ADDITION', N'Addition')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'BENEFIT', N'ADDITION', N'Non Cash Benefits')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'DEDUCTION', N'DEDUCTION', N'Deduction')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'EMPECONTR', N'EMPECONTR', N'Pension - Employee Contribution')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'EMPYCONTR', N'EMPYCONTR', N'Pension - Employer Contribution')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'HOURLY_PAY', N'HOURLY_PAY', N'Hourly Pay')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'LOAN', N'DEDUCTION', N'Loan')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'SACCO', N'DEDUCTION', N'Sacco')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'SALARY', N'SALARY', N'Salary')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'STATUTORY', N'STATUTORY', N'Statutory Recovery')
INSERT [dbo].[PayrollItemType] ([Id], [Parent], [Description]) VALUES (N'TAX', N'TAX', N'Payroll Tax')
/****** Object:  Table [dbo].[UserProfile]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserProfile]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[UserProfile] ON
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (2, N'kevinmk30@gmail.com')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (5, N'm@m.m')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (6, N'denique@denique.denique')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (7, N'87@rte.90')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (8, N'd@w.m')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (9, N'kevinmk40@gmail.com')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (10, N'kevinmk50@gmail.com')
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
/****** Object:  Table [dbo].[TxnType]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TxnType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TxnType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TxnType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Transactions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostDate] [datetime] NULL,
	[Amount] [money] NULL,
	[DRCR] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Narrative] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TxnType] [int] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[TransactionDef]    Script Date: 04/15/2015 23:58:58 ******/
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
/****** Object:  Table [dbo].[TechParams]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechParams]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TechParams](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[strtdt] [date] NULL,
	[edc] [int] NULL,
 CONSTRAINT [PK_TechnicalParameters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[TechParams] ON
INSERT [dbo].[TechParams] ([Id], [strtdt], [edc]) VALUES (9, CAST(0x22410B00 AS Date), 3000000)
SET IDENTITY_INSERT [dbo].[TechParams] OFF
/****** Object:  Table [dbo].[TaxTracking]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TaxTracking]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TaxTracking](
	[Id] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_TaxTracking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[TaxTracking] ([Id], [Description]) VALUES (N'COLLECTION', N'Collection for An Account')
INSERT [dbo].[TaxTracking] ([Id], [Description]) VALUES (N'DEDUCTIBLE', N'Tax deductible')
INSERT [dbo].[TaxTracking] ([Id], [Description]) VALUES (N'EARNING', N'Earning')
INSERT [dbo].[TaxTracking] ([Id], [Description]) VALUES (N'NONE', N'Not Taxable')
INSERT [dbo].[TaxTracking] ([Id], [Description]) VALUES (N'PAYE', N'Income Taxes')
/****** Object:  Table [dbo].[spUsersInRoles]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spUsersInRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spUsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_spUsersInRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[spUsersInRoles] ([UserId], [RoleId]) VALUES (1, 1)
INSERT [dbo].[spUsersInRoles] ([UserId], [RoleId]) VALUES (2, 1)
INSERT [dbo].[spUsersInRoles] ([UserId], [RoleId]) VALUES (3, 1)
INSERT [dbo].[spUsersInRoles] ([UserId], [RoleId]) VALUES (4, 1)
INSERT [dbo].[spUsersInRoles] ([UserId], [RoleId]) VALUES (5, 1)
INSERT [dbo].[spUsersInRoles] ([UserId], [RoleId]) VALUES (7, 1)
INSERT [dbo].[spUsersInRoles] ([UserId], [RoleId]) VALUES (8, 1)
/****** Object:  Table [dbo].[spUsers]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[UserName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Password] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Status] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Locked] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[SystemId] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Surname] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OtherNames] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NationalID] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Telephone] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateJoined] [date] NULL,
	[InformBy] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Photo] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_spUsers_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[spUsers] ON
INSERT [dbo].[spUsers] ([Id], [RoleId], [UserName], [Password], [Status], [Locked], [IsDeleted], [SystemId], [Surname], [OtherNames], [NationalID], [DateOfBirth], [Gender], [Telephone], [Email], [DateJoined], [InformBy], [Photo]) VALUES (1, 1, N'kevinmk30@gmail.com', N'kevinbrian.123', N'A', 0, 0, N'ws', N'k', N'km', N'213123', CAST(0xA63A0B00 AS Date), N'M', NULL, NULL, CAST(0x70390B00 AS Date), N'EMAIL', N'flowermage.jpg')
INSERT [dbo].[spUsers] ([Id], [RoleId], [UserName], [Password], [Status], [Locked], [IsDeleted], [SystemId], [Surname], [OtherNames], [NationalID], [DateOfBirth], [Gender], [Telephone], [Email], [DateJoined], [InformBy], [Photo]) VALUES (6, 1, N'sys', N'sys', N'A', 0, 0, N'ws', N'w', N't', N'5465', CAST(0x6E3A0B00 AS Date), N'M', NULL, NULL, CAST(0x1B3A0B00 AS Date), NULL, N'C:\Dev\SBSacco\SBSacco\WinSBSacco\Resources\bluewebmage.jpg')
INSERT [dbo].[spUsers] ([Id], [RoleId], [UserName], [Password], [Status], [Locked], [IsDeleted], [SystemId], [Surname], [OtherNames], [NationalID], [DateOfBirth], [Gender], [Telephone], [Email], [DateJoined], [InformBy], [Photo]) VALUES (7, 2, N'kevinmk40@gmail.com', N'kevin.123', N'A', 0, 0, N'web', N'kevin', N'kevin', N'1212412', CAST(0xD21F0B00 AS Date), N'M', NULL, N'kevinmk40@gmail.com', CAST(0x80390B00 AS Date), N'EMAIL', N'vlcsnap-00013.jpg')
INSERT [dbo].[spUsers] ([Id], [RoleId], [UserName], [Password], [Status], [Locked], [IsDeleted], [SystemId], [Surname], [OtherNames], [NationalID], [DateOfBirth], [Gender], [Telephone], [Email], [DateJoined], [InformBy], [Photo]) VALUES (8, 2, N'kevinmk50@gmail.com', N'kevin.123', N'A', 0, 0, N'web', N'kevin', N'kevin', N'kevin', CAST(0xD41F0B00 AS Date), N'M', NULL, N'kevinmk50@gmail.com', CAST(0x82390B00 AS Date), N'EMAIL', N'vlcsnap-00014.jpg')
SET IDENTITY_INSERT [dbo].[spUsers] OFF
/****** Object:  Table [dbo].[spRoles]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spRoles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShortCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_spRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[spRoles] ON
INSERT [dbo].[spRoles] ([Id], [ShortCode], [Description], [IsDeleted]) VALUES (1, N'Administrator', N'Administrator', 0)
INSERT [dbo].[spRoles] ([Id], [ShortCode], [Description], [IsDeleted]) VALUES (2, N'Employee', N'Employee', 0)
SET IDENTITY_INSERT [dbo].[spRoles] OFF
/****** Object:  Table [dbo].[spReportsMenuItems]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spReportsMenuItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spReportsMenuItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[mnuName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_spReportsMenuItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[spReportsMenuItems] ON
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (1, N'btnViewPayslip', N'Payslip')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (2, N'btnP9A', N'P9A')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (3, N'btnP9AHOSP', N'P9AHOSP')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (4, N'btnP9B', N'P9B')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (6, N'btnP10', N'P10')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (7, N'btnP10A', N'P10A')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (8, N'btnp11', N'P11')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (9, N'btnViewAllPayslip', N'All Payslips')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (10, N'cboItemTypesReports', N'Reports Item Types')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (11, N'btnShowPDF', N'Show PDF')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (13, N'btnExcel', N'Show Excel')
INSERT [dbo].[spReportsMenuItems] ([Id], [mnuName], [Description]) VALUES (14, N'btnShow', N'Show Shedule/Statement')
SET IDENTITY_INSERT [dbo].[spReportsMenuItems] OFF
/****** Object:  Table [dbo].[spMenus]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spMenus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spMenus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[mnuName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_spMenus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[spMenus] ON
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (2, N'subMenuPayrolls', N'Payrolls - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (3, N'subMenuProcessPayroll', N'Process Payroll -Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (4, N'mnuDataEntry', N'Data Entry - Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (5, N'subMenuEmployees', N'Employees - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (6, N'subMenuBanks', N'Banks - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (7, N'subMenuBenefits', N'Benefits - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (8, N'subMenuDepartments', N'Departments - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (9, N'subMenuPayeeRates', N'Payee Rates - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (10, N'subMenuNhifRates', N'Nhif Rates - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (11, N'subMenuPayrollItems', N'Payroll Items - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (12, N'subMenuNewNSSFComputaion', N'New Nssf Computations - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (13, N'mnuTechnical', N'Technical - Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (14, N'subMenuSettings', N'Settings - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (15, N'subMenuTaxCalculator', N'Tax Calculator - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (16, N'subMenuReports', N'Reports - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (17, N'mnuAdministrator', N'Administrator - Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (18, N'subMenuEmployers', N'Employers - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (19, N'subMenuRoles', N'Roles - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (20, N'subMenuUsers', N'Users - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (21, N'subMenuUsersRoles', N'User Roles - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (22, N'subMenuRights', N'Rights - Sub Menu')
INSERT [dbo].[spMenus] ([Id], [mnuName], [Description]) VALUES (23, N'mnuPayrolls', N'Payrolls - Menu')
SET IDENTITY_INSERT [dbo].[spMenus] OFF
/****** Object:  Table [dbo].[spMenuItems]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spMenuItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spMenuItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[mnuName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_spMenuItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[spMenuItems] ON
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (2, N'payrollToolStripMenuItem', N'Payroll Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (3, N'dataEntryToolStripMenuItem', N'Data Entry Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (4, N'administrationToolStripMenuItem', N'Administration Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (5, N'reportsToolStripMenuItem', N'Reports Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (7, N'changePasswordToolStripMenuItem', N'Change Password Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (8, N'openPayrollToolStripMenuItem', N'Open Payroll Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (9, N'processPayrollToolStripMenuItem', N'Process Payroll Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (10, N'employeesToolStripMenuItem', N'Employees Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (11, N'btnBankBranches', N'Bank Braches Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (12, N'benefitsToolStripMenuItem', N'Benefits Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (13, N'departmentsToolStripMenuItem', N'Departments Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (14, N'payeeRatesToolStripMenuItem', N'Payee Rates Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (15, N'nHIFRatesToolStripMenuItem', N'NHIF Rates Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (16, N'payrollItemsToolStripMenuItem', N'Payroll Items Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (17, N'usersToolStripMenuItem', N'Users Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (18, N'rightsToolStripMenuItem', N'Rights Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (19, N'rolesToolStripMenuItem', N'Roles Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (20, N'settingsToolStripMenuItem', N'Settings Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (21, N'employerToolStripMenuItem', N'Employer Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (22, N'databaseControlPanelToolStripMenuItem', N'Database Control Panel Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (23, N'uploadDownloadWizardToolStripMenuItem', N'Upload and Download Wizard Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (24, N'pdfReportsToolStripMenuItem', N'Reports Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (25, N'taxCalcuToolStripMenuItem', N'Tax Calculator Menu')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (29, N'tsbPDFReports', N'Reports Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (30, N'tsbOpenPayroll', N'Open Payroll Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (31, N'tsbProcessPayroll', N'Process Payroll Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (32, N'tspEmployees', N'Employees Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (33, N'tsbPayrollItems', N'Payroll Items Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (34, N'tsbBanks', N'Banks Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (35, N'tsbBenefits', N'Benefits Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (36, N'tsbDepartments', N'Departments Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (37, N'tsbPayeeRates', N'Payee Rates Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (38, N'tsbtnNHIFRates', N'NHIF Button')
INSERT [dbo].[spMenuItems] ([Id], [mnuName], [Description]) VALUES (39, N'tsbTaxCalculator', N'Tax Calculator Button')
SET IDENTITY_INSERT [dbo].[spMenuItems] OFF
/****** Object:  Table [dbo].[spAllowedRoleMenusweb]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spAllowedRoleMenusweb]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spAllowedRoleMenusweb](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[MenuItemId] [int] NULL,
	[Allowed] [bit] NULL,
 CONSTRAINT [PK_spAllowedRoleMenusweb] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[spAllowedRoleMenusweb] ON
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (3, 1, 1, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (4, 1, 2, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (5, 1, 3, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (6, 1, 4, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (7, 1, 5, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (8, 1, 6, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (9, 1, 7, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (10, 1, 8, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (11, 1, 9, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (12, 1, 10, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (13, 1, 11, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (14, 1, 12, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (15, 1, 13, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (16, 1, 14, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (17, 1, 15, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (18, 1, 16, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (19, 1, 17, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (20, 1, 18, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (21, 1, 19, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (22, 1, 20, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (23, 1, 21, 1)
INSERT [dbo].[spAllowedRoleMenusweb] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (24, 1, 22, 1)
SET IDENTITY_INSERT [dbo].[spAllowedRoleMenusweb] OFF
/****** Object:  Table [dbo].[spAllowedRoleMenus]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spAllowedRoleMenus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spAllowedRoleMenus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[MenuItemId] [int] NOT NULL,
	[Allowed] [bit] NOT NULL,
 CONSTRAINT [PK_spAllowedRoleMenus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[spAllowedRoleMenus] ON
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (6, 1, 4, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (7, 1, 18, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (10, 1, 22, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (11, 1, 17, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (12, 1, 19, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (13, 1, 5, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (19, 1, 36, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (20, 1, 10, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (21, 1, 32, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (22, 1, 21, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (23, 1, 38, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (24, 1, 15, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (25, 1, 3, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (26, 1, 2, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (27, 1, 8, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (28, 1, 23, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (29, 1, 39, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (30, 1, 25, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (31, 1, 20, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (32, 1, 29, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (33, 1, 24, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (34, 1, 31, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (35, 1, 9, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (36, 1, 16, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (37, 1, 37, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (38, 1, 14, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (39, 1, 30, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (40, 1, 33, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (41, 1, 7, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (42, 1, 34, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (43, 1, 11, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (44, 1, 35, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (45, 1, 12, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (46, 1, 13, 1)
INSERT [dbo].[spAllowedRoleMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (47, 2, 7, 1)
SET IDENTITY_INSERT [dbo].[spAllowedRoleMenus] OFF
/****** Object:  Table [dbo].[spAllowedReportsRolesMenus]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spAllowedReportsRolesMenus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[spAllowedReportsRolesMenus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[MenuItemId] [int] NOT NULL,
	[Allowed] [bit] NOT NULL,
 CONSTRAINT [PK_spAllowedReportsRolesMenus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[spAllowedReportsRolesMenus] ON
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (2, 1, 9, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (3, 1, 6, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (4, 1, 7, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (5, 1, 8, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (6, 1, 2, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (7, 1, 3, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (8, 1, 4, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (9, 1, 1, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (10, 1, 13, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (11, 1, 11, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (12, 1, 14, 1)
INSERT [dbo].[spAllowedReportsRolesMenus] ([Id], [RoleId], [MenuItemId], [Allowed]) VALUES (13, 1, 10, 1)
SET IDENTITY_INSERT [dbo].[spAllowedReportsRolesMenus] OFF
/****** Object:  Table [dbo].[PayrollItems]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PayrollItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PayrollItems](
	[Id] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ItemTypeId] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TaxTrackingId] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PayableTo] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GLAccount] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReFField] [int] NULL,
	[DefaultItem] [bit] NULL,
	[AddToPension] [bit] NULL,
	[Active] [bit] NULL,
	[Enable] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_PayrollItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'Addition1', N'Addition1', N'ADDITION', N'EARNING', N'Employee', NULL, NULL, 0, 0, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'ADVANCE', N'ADVANCE', N'DEDUCTION', N'COLLECTION', N'EMPLOYER', N'25684', NULL, 1, 0, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'BASIC', N'BASIC', N'SALARY', N'EARNING', N'EMPLOYEE', N'32568', NULL, 1, 1, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'Deduction1', N'Deduction1', N'DEDUCTION', N'COLLECTION', NULL, NULL, NULL, 0, 0, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'HOURLY_PAY', N'Hourly Pay', N'HOURLY_PAY', N'EARNING', N'EMPLOYEE', N'52148', NULL, 1, 0, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'Loan1', N'Loan1', N'LOAN', N'COLLECTION', NULL, NULL, NULL, 0, 0, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'NHIF', N'NHIF', N'STATUTORY', N'COLLECTION', N'NHIF      ', N'25684', NULL, 1, 0, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'NON_CASH_BENEFIT', N'NON_CASH_BENEFIT', N'ADDITION', N'EARNING', N'EMPLOYEE', N'38373', NULL, 1, 0, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'NSSF', N'NSSF', N'STATUTORY', N'DEDUCTIBLE', N'NSSF      ', N'54875', NULL, 1, 0, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'PAYE', N'PAYE', N'TAX', N'EARNING', N'PAYMASTER', N'54847', NULL, 1, 0, 1, 1, 0)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'Sacco', N'Sacco', N'SACCO', N'COLLECTION', NULL, NULL, NULL, 0, 0, 0, 0, 1)
INSERT [dbo].[PayrollItems] ([Id], [Description], [ItemTypeId], [TaxTrackingId], [PayableTo], [GLAccount], [ReFField], [DefaultItem], [AddToPension], [Active], [Enable], [IsDeleted]) VALUES (N'Sacco1', N'Sacco1', N'SACCO', N'COLLECTION', NULL, NULL, NULL, 0, 0, 1, 1, 0)
/****** Object:  Table [dbo].[Employees]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Employees]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Surname] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OtherNames] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Email] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DoB] [date] NULL,
	[MaritalStatus] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Gender] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Photo] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DoE] [date] NULL,
	[BasicComputation] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BasicPay] [money] NULL,
	[PersonalRelief] [money] NULL,
	[MortgageRelief] [money] NULL,
	[InsuranceRelief] [money] NULL,
	[NSSFNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NHIFNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IDNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PINNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DepartmentId] [int] NULL,
	[EmployerId] [int] NOT NULL,
	[PayPoint] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpGroup] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmpPayroll] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PrevEmployer] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DateLeft] [date] NULL,
	[PaymentMode] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TelephoneNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ChequeNo] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BankCode] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BankAccount] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LeaveBalance] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedOn] [date] NULL,
	[IsDeleted] [bit] NULL,
	[SystemId] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[BankBranch]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BankBranch]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BankBranch](
	[BankSortCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BranchCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BranchName] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[BankCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_BankBranch] PRIMARY KEY CLUSTERED 
(
	[BankSortCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01091', N'091', N'Eastleigh', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01092', N'092', N'CPC', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01094', N'094', N'Head Office', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01095', N'095', N'Wote', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01100', N'100', N'Moi Avenue', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01101', N'101', N'Kipande House', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01102', N'102', N'Treasury Square', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01103', N'103', N'Nakuru', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01104', N'104', N'KICC', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01105', N'105', N'Kisumu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01106', N'106', N'Kericho', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01107', N'107', N'Tom Mboya', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01108', N'108', N'Thika', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01109', N'109', N'Eldoret', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01110', N'110', N'Kakamega', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01111', N'111', N'Kilindini', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01112', N'112', N'Nyeri', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01113', N'113', N'Industrial Area', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01114', N'114', N'River Rd', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01115', N'115', N'Muranga', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01116', N'116', N'Embu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01117', N'117', N'Kangema', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01119', N'119', N'Kiambu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01120', N'120', N'Karatina', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01121', N'121', N'Siaya', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01122', N'122', N'Nyahururu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01123', N'123', N'Meru', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01124', N'124', N'Mumias', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01125', N'125', N'Nanyuki', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01127', N'127', N'Moyale', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01129', N'129', N'Kikuyu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01130', N'130', N'Tala', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01131', N'131', N'Kajiado', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01133', N'133', N'Custody Services', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01134', N'134', N'Matuu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01135', N'135', N'Kitui', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01136', N'136', N'Mvita', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01137', N'137', N'Jogoo Rd', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01140', N'140', N'Marsabit', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01141', N'141', N'Sarit Centre', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01142', N'142', N'Loitokitok', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01143', N'143', N'Nandi Hills', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01144', N'144', N'Lodwar', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01145', N'145', N'UN Gigiri', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01146', N'146', N'Hola', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01147', N'147', N'Ruiru', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01148', N'148', N'Mwingi', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01149', N'149', N'Kitale', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01150', N'150', N'Mandera', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01151', N'151', N'Kapenguria', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01152', N'152', N'Kabarnet', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01153', N'153', N'Wajir', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01154', N'154', N'Maralal', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01155', N'155', N'Limuru', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01157', N'157', N'Ukunda', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01161', N'161', N'Ongata Rongai', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01162', N'162', N'Kitengela', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01163', N'163', N'Eldama Ravine', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01164', N'164', N'Kibwezi', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01166', N'166', N'Kapsabet', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01167', N'167', N'University Way', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01168', N'168', N'Eldoret West', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01169', N'169', N'Garissa ', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01173', N'173', N'Lamu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01174', N'174', N'Kilifi', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01175', N'175', N'Milimani', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01176', N'176', N'Nyamira', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01177', N'177', N'Mukurwe-ini', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01180', N'180', N'Village Market', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01181', N'181', N'Bomet', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01183', N'183', N'Mbale', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01184', N'184', N'Narok', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01185', N'158', N'Iten', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01186', N'186', N'Voi', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01188', N'188', N'Webuye', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01189', N'159', N'Gilgil', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01190', N'190', N'Naivasha', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01191', N'191', N'Kisii', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01192', N'192', N'Migori', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01193', N'193', N'Githunguri', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01194', N'194', N'Machakos', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01195', N'195', N'Kerugoya', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01196', N'196', N'Chuka', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01197', N'197', N'Bungoma', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01198', N'198', N'Wundanyi', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01199', N'199', N'Malindi', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01201', N'201', N'Capital Hill', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01202', N'202', N'Karen', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01203', N'203', N'Lokichogio', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01204', N'204', N'Gateway', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01205', N'205', N'Buruburu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01206', N'206', N'Chogoria', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01207', N'207', N'Kangari', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01208', N'208', N'Kianyaga', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01209', N'209', N'Nkubu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01210', N'210', N'Ol Kalou', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01211', N'211', N'Makuyu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01212', N'212', N'Mwea', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01213', N'213', N'Njabini', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01214', N'214', N'Gatundu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01215', N'215', N'Emali', N'01')
GO
print 'Processed 100 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01216', N'216', N'Isiolo', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01217', N'217', N'Flamingo', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01218', N'218', N'Njoro', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01219', N'219', N'Mutomo', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01220', N'220', N'Mariakani', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01221', N'221', N'Mpeketoni', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01222', N'222', N'Mtito Andei', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01223', N'223', N'Mtwapa', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01224', N'224', N'Taveta', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01225', N'225', N'Kengeleni', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01226', N'226', N'Garsen', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01227', N'227', N'Watamu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01228', N'228', N'Bondo', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01229', N'229', N'Busia', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01230', N'230', N'Homa Bay', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01231', N'231', N'Kapsowar', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01232', N'232', N'Kehancha', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01233', N'233', N'Keroka', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01234', N'234', N'Kilgoris', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01235', N'235', N'Kimilili', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01236', N'236', N'Litein', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01237', N'237', N'Londiani', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01238', N'238', N'Luanda', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01239', N'239', N'Malaba', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01240', N'240', N'Muhoroni', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01241', N'241', N'Oyugis', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01242', N'242', N'Ugunja', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01243', N'243', N'United Mall', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01244', N'244', N'Serem', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01245', N'245', N'Sondu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01246', N'246', N'Kisumu West', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01247', N'247', N'Marigat', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01248', N'248', N'Moi''s Bridge', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01249', N'249', N'Mashariki', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01250', N'250', N'Naro Moru', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01251', N'251', N'Kiriaini', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01252', N'252', N'Egerton University', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01253', N'253', N'Maua', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01254', N'254', N'Kawangware', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01255', N'255', N'Kimathi Street', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01256', N'256', N'Namanga', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01257', N'257', N'Gikomba', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01258', N'258', N'Kwale', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01259', N'259', N'Prestige Plaza', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01260', N'260', N'Kariobangi', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01263', N'263', N'Biashara Street', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01266', N'266', N'Ngara', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01267', N'267', N'Kyuso', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01270', N'270', N'Masii', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01271', N'271', N'Menengai Crater', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01272', N'272', N'Town Centre', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01278', N'278', N'Makindu', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01283', N'283', N'Rongo', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01284', N'284', N'Isibania', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01285', N'285', N'Kiserian', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01286', N'286', N'Mwembe Tayari', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01287', N'287', N'Kisauni', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01288', N'288', N'Haile Selassie', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01289', N'289', N'Mama Ngina', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01290', N'290', N'Garden Plaza', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01291', N'291', N'Sarit Centre', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01292', N'292', N'CPC Bulk Corporate Chqs', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'01293', N'293', N'Trade Services', N'01')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02000', N'000', N'Eldoret', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02001', N'001', N'Kericho', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02002', N'002', N'Kisumu', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02003', N'003', N'Kitale', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02004', N'004', N'Treasury Square', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02005', N'005', N'Maritime', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02006', N'006', N'Kenyatta', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02007', N'007', N'Kimathi', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02008', N'008', N'Moi Avenue', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02009', N'009', N'Nakuru', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02010', N'010', N'Nanyuki', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02011', N'011', N'Nyeri', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02012', N'012', N'Thika', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02015', N'015', N'Westlands', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02016', N'016', N'Machakos', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02017', N'017', N'Meru', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02019', N'019', N'Harambee', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02020', N'020', N'Kiambu', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02053', N'053', N'Industrial Area', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02054', N'054', N'Kakamega', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02060', N'060', N'Malindi', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02063', N'063', N'Haile Selassie', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02064', N'064', N'Koinange Street', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02071', N'071', N'Yaya Centre', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02072', N'072', N'Ruaraka', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02074', N'074', N'Langata', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02075', N'075', N'Makupa', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02076', N'076', N'Karen', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02077', N'077', N'Muthaiga', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02078', N'078', N'Customer Service Centre', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02079', N'079', N'Customer Service Centre', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02080', N'080', N'Eastleigh', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02081', N'081', N'Kisii', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02082', N'082', N'Uper Hill', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02083', N'083', N'Mombasa-Nyali', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'02084', N'084', N'Chiromo', N'02')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03002', N'002', N'Kapsabet', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03003', N'003', N'Eldoret Std & Prestige', N'03')
GO
print 'Processed 200 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03004', N'004', N'Embu Std & Prestige', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03005', N'005', N'Murang''a', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03006', N'006', N'Kapenguria', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03007', N'007', N'Kericho', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03008', N'008', N'Kisii', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03009', N'009', N'Kisumu', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03010', N'010', N'South C', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03011', N'011', N'Limuru', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03012', N'012', N'Malindi', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03013', N'013', N'Meru', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03014', N'014', N'Eastleigh', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03015', N'015', N'Kitui', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03016', N'016', N'Nkrumah Rd', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03017', N'017', N'Garissa ', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03018', N'018', N'Nyamira', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03019', N'019', N'Kilifi', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03020', N'020', N'Waiyaki Way', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03023', N'023', N'Gilgil', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03024', N'024', N'Githurai', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03027', N'027', N'Nakuru East', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03028', N'028', N'Buruburu', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03029', N'029', N'Bomet', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03030', N'030', N'Nyeri', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03031', N'031', N'Thika', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03032', N'032', N'Port Mombasa', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03033', N'033', N'Gikomba', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03034', N'034', N'Kawangware', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03035', N'035', N'Mbale', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03036', N'036', N'Plaza Premier Centre', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03037', N'037', N'River Rd', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03038', N'038', N'Upper River Road', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03039', N'039', N'Mumias', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03040', N'040', N'Machakos', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03042', N'042', N'Isiolo', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03043', N'043', N'Ngong', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03044', N'044', N'Maua', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03045', N'045', N'Hurlingham', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03046', N'046', N'Makupa', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03047', N'047', N'Development Hse', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03049', N'049', N'Lavington', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03050', N'050', N'Eastleigh II', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03051', N'051', N'Homa Bay', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03052', N'052', N'Rongai', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03053', N'053', N'Othaya', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03054', N'054', N'Voi', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03055', N'055', N'Muthaiga', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03057', N'057', N'Githunguri', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03058', N'058', N'Webuye', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03060', N'060', N'Chuka', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03061', N'061', N'Nakumatt Westgate', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03062', N'062', N'Kabarnet', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03063', N'063', N'Kerugoya', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03064', N'064', N'Taveta', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03065', N'065', N'Karen Std&Prestige', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03066', N'066', N'Wundanyi', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03067', N'067', N'Ruaraka', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03069', N'069', N'Wote', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03070', N'070', N'Enterprise prestige centre', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03071', N'071', N'Nakumatt Meru', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03072', N'072', N'Juja', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03073', N'073', N'ABC Prestige', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03074', N'074', N'Kikuyu', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03075', N'075', N'Moi Avenue', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03077', N'077', N'Plaza Business Centre', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03078', N'078', N'Kiriaini', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03079', N'079', N'Avon Centre', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03080', N'080', N'Migori', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03082', N'082', N'Haile Selassie', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03083', N'083', N'University of Nairobi', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03086', N'086', N'Nairobi west', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03087', N'087', N'Parkland Highbridge', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03088', N'088', N'Busia', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03089', N'089', N'Pangani', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03093', N'093', N'Kariobangi', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03094', N'094', N'QueensWay', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'03095', N'095', N'Nakumatt Ebakasi', N'03')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'05000', N'000', N'Nairobi', N'05')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'05001', N'001', N'Mombasa', N'05')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'05002', N'002', N'Industrial Area', N'05')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'05003', N'003', N'Westlands', N'05')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'06000', N'000', N'Nairobi Main', N'06')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'06002', N'002', N'Digo rd', N'06')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'06004', N'004', N'Thika', N'06')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'06005', N'005', N'Kisumu', N'06')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'06006', N'006', N'Sarit Centre', N'06')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'06007', N'007', N'Industrial Area', N'06')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'06008', N'008', N'Eldoret', N'06')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'06009', N'009', N'Nakuru', N'06')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07000', N'000', N'Head Office', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07001', N'001', N'Upper Hill', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07002', N'002', N'Wabera', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07003', N'003', N'Mama Ngina Br/Hilton Agency', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07004', N'004', N'Westlands Br/ILRI Agency', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07005', N'005', N'Industrial Area', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07006', N'006', N'Mamlaka', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07007', N'007', N'Village Mkt Br/US Emb/Icraf Ag', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07008', N'008', N'Cargo Centre', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07009', N'009', N'Park Side', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07016', N'016', N'Galleria', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07017', N'017', N'Junction', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07020', N'020', N'Moi Avenue Mombasa', N'07')
GO
print 'Processed 300 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07021', N'021', N'Meru', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07022', N'022', N'Nakuru', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07023', N'023', N'Bamburi', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07024', N'024', N'Diani', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07025', N'025', N'Changamwe', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07026', N'026', N'Eldoret', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'07027', N'027', N'Kisumu', N'07')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'08046', N'046', N'Mobasa', N'08')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'08047', N'047', N'Malindi', N'08')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'08048', N'048', N'Nairobi', N'08')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'09000', N'000', N'Head Office', N'09')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'09001', N'001', N'Head Office', N'09')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'09002', N'002', N'Mombasa', N'09')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'09003', N'003', N'Kisumu', N'09')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'09004', N'004', N'Eldoret', N'09')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10000', N'000', N'Head Office', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10001', N'001', N'Kenindia', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10002', N'002', N'Biashara', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10003', N'003', N'Mombasa', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10004', N'004', N'Westlands', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10005', N'005', N'Industrial Area', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10006', N'006', N'Kisumu', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10007', N'007', N'Parklands', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10008', N'008', N'Riverside Drive', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10009', N'009', N'Card centre', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10010', N'010', N'Hurlingham', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10011', N'011', N'Capital Centre', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10012', N'012', N'Nyali', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10014', N'014', N'Kamukunji', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'10015', N'015', N'Eldoret', N'10')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11000', N'000', N'Head Office', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11002', N'002', N'Co-op Hse', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11003', N'003', N'Kisumu', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11004', N'004', N'Nkrumah Rd', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11005', N'005', N'Meru', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11006', N'006', N'Nakuru', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11007', N'007', N'Industrial Are', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11008', N'008', N'Kisii', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11009', N'009', N'Machakos', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11010', N'010', N'Nyeri', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11011', N'011', N'Ukulima', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11012', N'012', N'Chuka', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11013', N'013', N'Wakulima Market', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11014', N'014', N'Moi Avenue', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11015', N'015', N'Naivasha', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11017', N'017', N'Nyahururu', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11018', N'018', N'Chuka', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11019', N'019', N'Wakulima Market', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11020', N'020', N'Eastleigh', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11021', N'021', N'Kiambu', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11022', N'022', N'Homabay', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11023', N'023', N'Embu', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11024', N'024', N'Kericho', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11025', N'025', N'Bungoma', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11026', N'026', N'Muranga', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11027', N'027', N'Kayole', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11028', N'028', N'Karatina', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11029', N'029', N'Ukunda', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11030', N'030', N'Mtwapa', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11031', N'031', N'University way', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11032', N'032', N'BuruBuru', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11033', N'033', N'AthiRiver', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11034', N'034', N'Mumias', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11035', N'035', N'Stima Plaza', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11036', N'036', N'Westlands', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11037', N'037', N'Upper Hill', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11038', N'038', N'Ongata Rongai', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11039', N'039', N'Thika', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11040', N'040', N'Nacico Plaza', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11041', N'041', N'Kariobangi', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11042', N'042', N'Kawangware', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11043', N'043', N'Makutano', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11044', N'044', N'Parliament Rd', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11045', N'045', N'Kimathi Street', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11046', N'046', N'Kitale', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11047', N'047', N'Githurai Agency', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11048', N'048', N'Maua', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11049', N'049', N'City Hall', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11050', N'050', N'Digo', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11051', N'051', N'Nairobi Business Centre', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11052', N'052', N'Kakamega', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11053', N'053', N'Migori', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11055', N'055', N'Nkubu', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11056', N'056', N'Enterprise Rd', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11057', N'057', N'Busia', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11058', N'058', N'Siaya', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11059', N'059', N'Voi', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11060', N'060', N'Mariakani', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11061', N'061', N'Malindi', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11062', N'062', N'Zimmerman', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11063', N'063', N'Nakuru East', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11064', N'064', N'Kitengela', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11065', N'065', N'Aga Khan Walk', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11066', N'066', N'Narok', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11067', N'067', N'Kitui', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11068', N'068', N'Nanyuki', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11069', N'069', N'Embakasi', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11070', N'070', N'Kibera', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11071', N'071', N'Siakago', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11072', N'072', N'Kapsabet', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11073', N'073', N'Mbita', N'11')
GO
print 'Processed 400 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11074', N'074', N'Kangemi', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11075', N'075', N'Dandora', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11077', N'077', N'Tala', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11078', N'078', N'Gikomba', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11079', N'079', N'River Road', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11080', N'080', N'Nyamira', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11081', N'081', N'Garissa', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11082', N'082', N'Bomet', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11083', N'083', N'Keroka', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11084', N'084', N'Gilgil', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11085', N'085', N'Tom Mboya', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11086', N'086', N'Likoni', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11088', N'088', N'Mwingi', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11089', N'089', N'Mwingi', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11090', N'090', N'Webuye', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11100', N'100', N'Ndhiwa', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'11102', N'102', N'Isiolo', N'11')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12002', N'002', N'Kenyatta Avenue', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12003', N'003', N'Harambee Avenue NBK  Building', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12004', N'004', N'Hill Plaza', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12005', N'005', N'Busia', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12006', N'006', N'Kiambu', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12007', N'007', N'Meru', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12008', N'008', N'Karatina', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12009', N'009', N'Narok', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12010', N'010', N'Kisii', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12011', N'011', N'Malindi', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12012', N'012', N'Nyeri', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12013', N'013', N'Kitale', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12015', N'015', N'Eastleigh', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12016', N'016', N'Limuru', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12017', N'017', N'Kitui', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12019', N'019', N'Bungoma', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12020', N'020', N'Nkurumah Rd', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12021', N'021', N'Kapsabet', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12022', N'022', N'Awendo', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12023', N'023', N'Portway Hse', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12025', N'025', N'Hospital', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12026', N'026', N'Ruiru', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12027', N'027', N'Ongata Rongai', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12028', N'028', N'Embu', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12029', N'029', N'Kakamega', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12031', N'031', N'Ukunda', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12032', N'032', N'Upper Hill', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12033', N'033', N'Nandi Hills', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12034', N'034', N'Migori', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12035', N'035', N'Westlands', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12036', N'036', N'Times Tower', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12037', N'037', N'Maua', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12038', N'038', N'Wilson Airport', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12039', N'039', N'J.K.I.A.', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12040', N'040', N'Eldoret', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12042', N'042', N'Mutomo', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12043', N'043', N'Kianjai', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12044', N'044', N'Kenyatta University', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12045', N'045', N'St. Paul''s University', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12046', N'046', N'Moi University', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12047', N'047', N'Moi International Airport, Mombasa', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12050', N'050', N'Kisumu', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'12099', N'099', N'Head Office', N'12')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'14000', N'000', N'Head Office', N'14')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'14004', N'004', N'Nakuru', N'14')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'14005', N'005', N'Eldoret', N'14')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'14006', N'006', N'Kitale', N'14')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'14007', N'007', N'Westlands', N'14')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'16000', N'000', N'Head Office', N'16')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'16400', N'400', N'Mombasa', N'16')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'17000', N'000', N'Main Branch', N'17')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'17001', N'001', N'Mombasa', N'17')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'17002', N'002', N'Industrial Area', N'17')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'18000', N'000', N'Head Office', N'18')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'18002', N'002', N'Mombasa', N'18')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'18003', N'003', N'Milimani', N'18')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'18004', N'004', N'Industrial Area', N'18')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19000', N'000', N'Nairobi', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19001', N'001', N'Mombasa', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19002', N'002', N'Westlands', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19003', N'003', N'Uhuru Highway', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19004', N'004', N'River Road', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19006', N'006', N'Kisumu', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19007', N'007', N'Ruaraka', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19008', N'008', N'Monrovia Street', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19009', N'009', N'Nakuru', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19010', N'010', N'Ngong Road', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19011', N'011', N'Eldoret', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19012', N'012', N'Embakasi', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19013', N'013', N'Kericho', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19014', N'014', N'Changamwe', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19015', N'015', N'Bungoma', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19017', N'017', N'Kisii', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'19018', N'018', N'Meru', N'19')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'20001', N'001', N'Head Office', N'20')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'20002', N'002', N'Eastleigh', N'20')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'20003', N'003', N'Mombasa', N'20')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'20004', N'004', N'Nakuru', N'20')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23000', N'000', N'Harambee Avenue Harambee Avenue', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23001', N'001', N'Murang''a', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23002', N'002', N'Embu', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23003', N'003', N'Mombasa', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23004', N'004', N'Koinange Street', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23005', N'005', N'Thika', N'23')
GO
print 'Processed 500 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23006', N'006', N'Meru', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23007', N'007', N'Nyeri', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23009', N'009', N'Maua', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23010', N'010', N'Isiolo', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23011', N'011', N'Head Office', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23013', N'013', N'Umoja', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23014', N'014', N'River Road', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23015', N'015', N'Eldoret', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'23016', N'016', N'Nakuru', N'23')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'25000', N'000', N'Head Office', N'25')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'25001', N'001', N'Koinange Street', N'25')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'25002', N'002', N'Kisumu', N'25')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'25003', N'003', N'Nakuru', N'25')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'25004', N'004', N'Kisii', N'25')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'25005', N'005', N'Westlands', N'25')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'25006', N'006', N'Industrial Area', N'25')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26001', N'001', N'Nairobi', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26002', N'002', N'Mombasa', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26003', N'003', N'Eldoret', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26005', N'005', N'MIA', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26006', N'006', N'JKIA', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26007', N'007', N'Kirinyaga', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26008', N'008', N'Kabarak', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26009', N'009', N'Olenguruone', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26010', N'010', N'Kericho', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26011', N'011', N'Nandi Hills', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26012', N'012', N'EPZ', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26013', N'013', N'Sheikh Karume', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'26014', N'014', N'Kabarnet', N'26')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30000', N'000', N'Head Office', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30001', N'001', N'City Centre', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30003', N'003', N'Village Market', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30004', N'004', N'Mombasa', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30005', N'005', N'Hurlingham', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30006', N'006', N'Eastleigh', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30007', N'007', N'Parklands', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30008', N'008', N'Riverside Mews', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30009', N'009', N'Iman Banking Centre Riverside Mews', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30010', N'010', N'Thika', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30011', N'011', N'Nakuru', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30012', N'012', N'Donholm', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30014', N'014', N'Ngara Mini Branch Peace Towers', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30015', N'015', N'Kisumu', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'30016', N'016', N'Eldoret', N'30')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31000', N'000', N'Clearing Centre,Head Office', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31002', N'002', N'Kenyatta Avenue', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31003', N'003', N'Digo Road', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31004', N'004', N'Waiyaki Way', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31005', N'005', N'Industrial Area', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31006', N'006', N'Harambee Avenue Nairobi', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31007', N'007', N'Chiromo Road', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31008', N'008', N'International House', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31009', N'009', N'Nkrumah', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31010', N'010', N'Upper Hill', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31011', N'011', N'Naivasha', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31012', N'012', N'Westgate', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31013', N'013', N'Kisumu', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31014', N'014', N'Nakuru', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31015', N'015', N'Thika', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31016', N'016', N'Nyerere', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31017', N'017', N'Nanyuki', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31018', N'018', N'Meru', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31020', N'020', N'Gikomba', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31021', N'021', N'Ruaraka', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31022', N'022', N'Eldoret', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31023', N'023', N'Karen', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31024', N'024', N'Kisii', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'31999', N'999', N'Central Processing CfC Centre,HeadOffice', N'31')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'35000', N'000', N'Koinange Street', N'35')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'35001', N'001', N'Westlands', N'35')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'35002', N'002', N'Industrial Area', N'35')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'35004', N'004', N'Kisumu', N'35')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'35005', N'005', N'Eldoret', N'35')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'35006', N'006', N'Meru', N'35')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'35007', N'007', N'Libra House', N'35')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'35008', N'008', N'Nakuru', N'35')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39001', N'001', N'IPS', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39002', N'002', N'Mombasa', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39003', N'003', N'Upper Hill', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39004', N'004', N'Parklands', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39006', N'006', N'Industrial Area', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39007', N'007', N'Watamu', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39008', N'008', N'Diani', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39009', N'009', N'Kilifi', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39010', N'010', N'Eldoret', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39011', N'011', N'Karen', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39012', N'012', N'Thika', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39014', N'014', N'Changamwe', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39015', N'015', N'Riverside', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39016', N'016', N'Likoni', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'39018', N'018', N'Village Market', N'39')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41000', N'000', N'Head Office', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41101', N'101', N'City Centre', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41102', N'102', N'NIC Hse', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41103', N'103', N'Harbor Hse', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41105', N'105', N'Westlands', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41106', N'106', N'Junction', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41107', N'107', N'Nakuru', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41108', N'108', N'Nyali', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41109', N'109', N'Nkurumah Rd', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41111', N'111', N'Prestige', N'41')
GO
print 'Processed 600 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41112', N'112', N'Kisumu', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41113', N'113', N'Thika', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41114', N'114', N'Meru', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41115', N'115', N'Galleria (Bomas)', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41116', N'116', N'Eldoret', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41117', N'117', N'Village Market', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'41118', N'118', N'Mombasa Road', N'41')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'42001', N'001', N'Banda Street', N'42')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'42004', N'004', N'Kimathi Street', N'42')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'42005', N'005', N'Kisumu', N'42')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'42006', N'006', N'Westlands', N'42')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'42007', N'007', N'Parklands', N'42')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43000', N'000', N'Ecobank Towers', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43002', N'002', N'Mombasa', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43003', N'003', N'Plaza 2000', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43004', N'004', N'Westminister', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43005', N'005', N'Chambers', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43007', N'007', N'Eldoret', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43008', N'008', N'Kisumu', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43009', N'009', N'Kisii', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43010', N'010', N'Kitale', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43011', N'011', N'Industrial Area', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43012', N'012', N'Karatina', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43013', N'013', N'Westlands', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43014', N'014', N'United Mall', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43015', N'015', N'Nakuru', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43016', N'016', N'Jomo Kenyatta Avenue', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43017', N'017', N'Nyeri', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43018', N'018', N'Busia', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43019', N'019', N'Malindi', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43020', N'020', N'Meru', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43021', N'021', N'Gikomba', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43023', N'023', N'Valley Arcade', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'43100', N'100', N'Head Office', N'43')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49000', N'000', N'Head Office', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49001', N'001', N'Nairobi', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49002', N'002', N'Mombasa', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49003', N'003', N'Westlands The Mall The Mall', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49005', N'005', N'Chester', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49007', N'007', N'Waiyaki Way', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49008', N'008', N'Kakamega', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49009', N'009', N'Eldoret', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49011', N'011', N'Nyali', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49012', N'012', N'Kisumu', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'49013', N'013', N'Industrial Area', N'49')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'50000', N'000', N'Head Office', N'50')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'50001', N'001', N'Westlands', N'50')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'50002', N'002', N'Parklands', N'50')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'50003', N'003', N'Koinange Street', N'50')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'5004', N'004', N'Mombasa', N'50')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'51000', N'000', N'Koinange Street', N'51')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53001', N'001', N'Head Office', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53002', N'002', N'Industrial Area', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53003', N'003', N'Westlands', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53004', N'004', N'Lavington', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53005', N'005', N'Mombasa', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53006', N'006', N'Nakuru', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53007', N'007', N'Eldoret', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53008', N'008', N'Muthaiga', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53010', N'010', N'Thika', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53011', N'011', N'Gikomba', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53012', N'012', N'Ngong Road', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'53013', N'013', N'Meru', N'53')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'54001', N'001', N'Nairobi', N'54')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'54002', N'002', N'Riverside Drive', N'54')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'55001', N'001', N'Head Office', N'55')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'55002', N'002', N'Westlands', N'55')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'55004', N'004', N'Eldoret', N'55')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'55005', N'005', N'Kisumu', N'55')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'55006', N'006', N'Main Branch', N'55')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'55007', N'007', N'Mombasa Road', N'55')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57000', N'000', N'Kenyatta Avenue', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57001', N'001', N'2nd Ngong Avenue I & M Bank House', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57002', N'002', N'Sarit Centre', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57003', N'003', N'Head Office', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57004', N'004', N'Biashara', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57005', N'005', N'Mombasa', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57007', N'007', N'Kisumu', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57008', N'008', N'Karen', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57009', N'009', N'Panari Centre', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57010', N'010', N'Parklands', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57011', N'011', N'Wilson Airport', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57012', N'012', N'Ongata Rongai', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57013', N'013', N'South C Shopping South C', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57014', N'014', N'Nyali Cinemax', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57015', N'015', N'Langata Link', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57016', N'016', N'Lavington', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57018', N'018', N'Nakuru', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'57098', N'098', N'Card Centre', N'57')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'59001', N'001', N'Loita Street', N'59')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'59002', N'002', N'Ngong Road', N'59')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'60000', N'000', N'Head Office', N'60')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'60001', N'001', N'City Centre', N'60')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'60002', N'002', N'Westlands', N'60')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'60003', N'003', N'Industrial Area', N'60')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'60004', N'004', N'Diani', N'60')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'60006', N'006', N'Mombasa', N'60')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63000', N'000', N'Head Office', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63001', N'001', N'Nation Centre', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63002', N'002', N'Mombasa', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63003', N'003', N'Kisumu', N'63')
GO
print 'Processed 700 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63005', N'005', N'Parklands', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63006', N'006', N'Westgate', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63008', N'008', N'Mombasa Rd', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63009', N'009', N'Industrial Area', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63011', N'011', N'Malindi', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63012', N'012', N'Thika', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63013', N'013', N'OTC', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63014', N'014', N'Eldoret', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63015', N'015', N'Eastleigh', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63016', N'016', N'Changamwe', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63017', N'017', N'T-Mall', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63018', N'018', N'Nakuru', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63019', N'019', N'Village Market', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63020', N'020', N'Diani', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63021', N'021', N'Bungoma', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63023', N'023', N'Prestige', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63024', N'024', N'Buru Buru', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63025', N'025', N'Kitengela', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63026', N'026', N'Jomo Kenyatta', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63027', N'027', N'Kakamega', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63028', N'028', N'Kericho', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63029', N'029', N'Upper Hill', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63030', N'030', N'Wabera Street', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63031', N'031', N'Karen', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63032', N'032', N'Voi', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63034', N'034', N'Meru', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63035', N'035', N'Diamond Plaza', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63036', N'036', N'Cross Roads', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'63050', N'050', N'Tom Mboya', N'63')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66000', N'000', N'Head Office', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66001', N'001', N'Naivasha Road', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66002', N'002', N'Moi Avenue -Mombasa', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66003', N'003', N'Kenyatta Avenue', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66004', N'004', N'Nakuru', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66005', N'005', N'Nyeri', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66007', N'007', N'Embu', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66008', N'008', N'Eldoret', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66009', N'009', N'Kisumu', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66010', N'010', N'Kericho', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66011', N'011', N'Kangemi', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66012', N'012', N'Thika', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66013', N'013', N'Kerugoya', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66014', N'014', N'Kenyatta Mkt', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66015', N'015', N'Kisii', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66016', N'016', N'Chuka', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66017', N'017', N'Kitui', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66018', N'018', N'Machakos', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66019', N'019', N'Nanyuki', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66021', N'021', N'Emali', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66022', N'022', N'Naivasha', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66023', N'023', N'Nyahururu', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66024', N'024', N'Isiolo', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66025', N'025', N'Meru', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66026', N'026', N'Kitale', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66027', N'027', N'Kibwezi', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66028', N'028', N'Bungoma', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66031', N'031', N'Mtwapa', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66033', N'033', N'Moi Avenue', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66034', N'034', N'Mwea', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66035', N'035', N'Kengeleni', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'66036', N'036', N'Kilimani', N'66')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68000', N'000', N'Head Office', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68001', N'001', N'Corporate', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68002', N'002', N'Fourways', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68003', N'003', N'Kangema', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68004', N'004', N'Karatina', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68006', N'006', N'Muraradia', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68007', N'007', N'Kangari', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68008', N'008', N'Othaya', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68009', N'009', N'Thika Plaza', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68010', N'010', N'Kerugoya', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68011', N'011', N'Nyeri', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68012', N'012', N'Tom Mboya', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68013', N'013', N'Nakuru Gatehouse Gate Hse', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68014', N'014', N'Meru', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68015', N'015', N'Mama Ngina', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68017', N'017', N'Community', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68018', N'018', N'Community Corporate', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68019', N'019', N'Embu', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68020', N'020', N'Naivasha', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68021', N'021', N'Chuka', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68022', N'022', N'Murang''a', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68023', N'023', N'Molo', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68024', N'024', N'Harambee', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68025', N'025', N'Mombasa', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68026', N'026', N'Kimathi', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68027', N'027', N'Nanyuki', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68029', N'029', N'Kisumu', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68030', N'030', N'Eldoret', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68031', N'031', N'Nakuru Kenyatta Avenue', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68032', N'032', N'Kariobangi', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68033', N'033', N'Kitale', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68034', N'034', N'Thika Kenyatta Highway', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68035', N'035', N'Knut Hse', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68036', N'036', N'Narok', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68037', N'037', N'Nkubu', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68038', N'038', N'Mwea', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68040', N'040', N'Maua', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68041', N'041', N'Isiolo', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68042', N'042', N'Kagio', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68043', N'043', N'Gikomba', N'68')
GO
print 'Processed 800 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68044', N'044', N'Ukunda', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68045', N'045', N'Malindi', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68046', N'046', N'Mombasa Digo Rd Digo Rd', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68047', N'047', N'Moi Avenue', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68048', N'048', N'Bungoma', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68049', N'049', N'Kapsabet', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68050', N'050', N'Kakamega', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68052', N'052', N'Nyamira', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68053', N'053', N'Litein', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68055', N'055', N'Westlands', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68056', N'056', N'Industrial Area', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68057', N'057', N'Kikuyu', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68058', N'058', N'Garissa', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68059', N'059', N'Mwingi', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68060', N'060', N'Machakos', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68061', N'061', N'Ongata Rongai', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68062', N'062', N'Ol-Kalou', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68064', N'064', N'Kiambu', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68065', N'065', N'Kayole', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68066', N'066', N'Gatundu', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68067', N'067', N'Wote', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68068', N'068', N'Mumias', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68069', N'069', N'Limuru', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68070', N'070', N'Kitengela', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68071', N'071', N'Githurai', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68072', N'072', N'Kitui', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68073', N'073', N'Ngong', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68074', N'074', N'Loitoktok', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68076', N'076', N'Mbita', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68077', N'077', N'Gilgil', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68078', N'078', N'Busia', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68079', N'079', N'Voi', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68080', N'080', N'Enterprise Road', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68081', N'081', N'Equity Centre', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68082', N'082', N'Donholm', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68083', N'083', N'Mukurwe-ini', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68084', N'084', N'Eastleigh', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68085', N'085', N'Namanga', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68088', N'088', N'OTC', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68089', N'089', N'Kenol', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68090', N'090', N'Tala', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68091', N'091', N'Ngara', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68092', N'092', N'Nandi Hills', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68093', N'093', N'Githunguri', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68094', N'094', N'Tea Room', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68095', N'095', N'Buru Buru', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68096', N'096', N'Mbale', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68097', N'097', N'Siaya', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68098', N'098', N'Homa Bay', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68099', N'099', N'Lodwar', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68100', N'100', N'Mandera', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68101', N'101', N'Marsabit', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68102', N'102', N'Moyale', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68103', N'103', N'Wajir', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68104', N'104', N'Meru Makutano', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68105', N'105', N'Malaba', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68106', N'106', N'Kilifi', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68107', N'107', N'Kapenguria', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68108', N'108', N'Mombasa Road', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68110', N'110', N'Maralal', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68111', N'111', N'Kimende', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68112', N'112', N'Luanda', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68113', N'113', N'KU', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68114', N'114', N'Kengeleni', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68115', N'115', N'Nyeri Kimathi Way EK Wachira Bldg', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68116', N'116', N'Migori', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68117', N'117', N'Kibera', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68118', N'118', N'Kasarani', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68119', N'119', N'Mtwapa', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68120', N'120', N'Changamwe', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68122', N'122', N'Bomet', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68123', N'123', N'Kilgoris', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68124', N'124', N'Keroka', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68125', N'125', N'Karen', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68126', N'126', N'Kisumu Angawa Avenue', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68127', N'127', N'Mpeketoni', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68128', N'128', N'Nairobi West', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68129', N'129', N'Kenyatta Avenue', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68130', N'130', N'City Hall', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'68131', N'131', N'Eldama Ravine', N'68')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70000', N'000', N'Head Office', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70001', N'001', N'Kiambu', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70002', N'002', N'Githunguri', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70003', N'003', N'Sonalux', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70004', N'004', N'Gatundu', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70005', N'005', N'Thika', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70006', N'006', N'Murang''a', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70007', N'007', N'kangari', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70008', N'008', N'Kiria-ini', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70009', N'009', N'Kangema', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70011', N'011', N'Othaya', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70014', N'014', N'Cargen Hse', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70018', N'018', N'Nakuru Finance', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70019', N'019', N'Nakuru Njoro Hse', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70021', N'021', N'Dagoreti', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70023', N'023', N'Nyahururu', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70024', N'024', N'Ruiru', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70025', N'025', N'Kisumu', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70026', N'026', N'Nyamira', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70027', N'027', N'Kisii', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70031', N'031', N'Industrial Area', N'70')
GO
print 'Processed 900 total records'
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70033', N'033', N'Donholm', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70035', N'035', N'Fourways Retail', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70038', N'038', N'KTDA Plaza', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70041', N'041', N'Kariobangi', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70042', N'042', N'Gikomba Area 42', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70043', N'043', N'Gikomba', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70045', N'045', N'Githurai', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70046', N'046', N'Kilimani', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70047', N'047', N'Limuru', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70049', N'049', N'Kagwe', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70051', N'051', N'Banana', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70053', N'053', N'Naivasha', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70055', N'055', N'Nyeri', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70057', N'057', N'Kerugoya', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70058', N'058', N'Tom Mboya', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70059', N'059', N'River Road', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70061', N'061', N'Kayole', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70062', N'062', N'Nkubu', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70063', N'063', N'Meru', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70065', N'065', N'KTDA Corporate', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70069', N'069', N'Kitui', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70071', N'071', N'Kitengela', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70072', N'072', N'Kitui', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70073', N'073', N'Machakos', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70075', N'075', N'Embu', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70077', N'077', N'Bungoma', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70078', N'078', N'Kakamega', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70079', N'079', N'Busia', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70083', N'083', N'Molo', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70085', N'085', N'Eldoret', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70095', N'095', N'Mombasa', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70096', N'096', N'Kenyatta Avenue,Mombasa', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'70097', N'097', N'Kapsabet', N'70')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72000', N'000', N'Head Office', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72001', N'001', N'Central Clearing Centre', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72002', N'002', N'Upperhill', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72003', N'003', N'Eastleigh', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72004', N'004', N'Kenyatta Avenue', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72005', N'005', N'Mombasa', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72007', N'007', N'Lamu', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72008', N'008', N'Malindi', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72009', N'009', N'Muthaiga', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'72010', N'010', N'Bondeni', N'72')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74001', N'001', N'Wabera Street', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74002', N'002', N'Eastleigh', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74003', N'003', N'Mombasa', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74004', N'004', N'Garissa', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74005', N'005', N'Eastleigh II', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74006', N'006', N'Malindi', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74007', N'007', N'Kisumu', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74008', N'008', N'Kimathi Street', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74009', N'009', N'Westlands', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74010', N'010', N'South C', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74011', N'011', N'Industrial Area', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74012', N'012', N'Masalani', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74013', N'013', N'Habaswein', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74014', N'014', N'Wajir', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74015', N'015', N'Moyale', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74016', N'016', N'Nakuru', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74017', N'017', N'Mombasa 11', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'74999', N'999', N'Head Office/Clearing Centre', N'74')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'76001', N'001', N'Westlands', N'76')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'76002', N'002', N'Enterprise Road', N'76')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'76003', N'003', N'Upper Hill', N'76')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'76099', N'099', N'Head Office', N'76')
INSERT [dbo].[BankBranch] ([BankSortCode], [BranchCode], [BranchName], [BankCode]) VALUES (N'95000', N'000', N'Head Office', N'59')
/****** Object:  StoredProcedure [dbo].[CopyPayslipDet]    Script Date: 04/15/2015 23:58:59 ******/
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
                         (EmpTxnId, EmpNo, EmployeeId, Period, Year, Description, TaxTracking, Amount, DEType, IsStatutory, ShowInPayslip, YTD)
SELECT        EmpTxnId, EmpNo, EmployeeId, Period, Year, Description, TaxTracking, Amount, DEType, IsStatutory, ShowInPayslip, YTD
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
/****** Object:  StoredProcedure [dbo].[CopyPayMaster]    Script Date: 04/15/2015 23:58:59 ******/
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
                         (Period, Year, EmpNo, EmployeeId, PaymentDate, PrintedBy, PrintedOn, EmpName, PayPoint, PIN, EmpGroup, EmpPayroll, CompName, CompAddr, CompTel, PayeTax, BasicPay, 
                         Benefits, Variables, OtherDeductions, GrossTaxableEarnings, NetTaxableEarnings, MortgageRelief,InsuranceRelief, GrossTax, PersonalRelief, PensionEmployer, EmployerNSSF, 
                         PensionEmployee, BankBranch, Account, NSSF, NHIF, NetPay, Department, NSSFNo, NHIFNo)
SELECT        Period, Year, EmpNo, EmployeeId, PaymentDate, PrintedBy, PrintedOn, EmpName, PayPoint, PIN, EmpGroup, EmpPayroll, CompName, CompAddr, CompTel, PayeTax, BasicPay, 
                         Benefits, Variables, OtherDeductions, GrossTaxableEarnings, NetTaxableEarnings, MortgageRelief, InsuranceRelief, GrossTax, PersonalRelief, PensionEmployer, EmployerNSSF, 
                         PensionEmployee, BankBranch, Account, NSSF, NHIF, NetPay, Department, NSSFNo, NHIFNo
FROM            PayslipMaster_Temp
SELECT        Period, [Year], EmpNo, EmployeeId, PaymentDate, PrintedBy, PrintedOn, EmpName, PayPoint, PIN, EmpGroup, EmpPayroll, CompName, CompAddr, CompTel, PayeTax, BasicPay, 
                         Benefits, Variables, OtherDeductions, GrossTaxableEarnings, NetTaxableEarnings, MortgageRelief,InsuranceRelief, GrossTax, PersonalRelief, PensionEmployer, EmployerNSSF, 
                         PensionEmployee, BankBranch, Account, NSSF, NHIF, NetPay, Department, NSSFNo, NHIFNo
FROM            PayslipMaster_Temp
	RETURN
' 
END
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 04/15/2015 23:58:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Settings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Settings](
	[SSKey] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SSValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SSValueType] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SGroup] [int] NOT NULL,
	[SSSystem] [bit] NOT NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY CLUSTERED 
(
	[SSKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipBodyHeaderDataFontFamily', N'2', N'N', N'Body Header Data Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 14, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipBodyHeaderDataFontSize', N'9', N'N', N'Body Header Data Font  Size', 12, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipBodyHeaderFontFamily', N'2', N'N', N'Body Header Font  Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 14, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipBodyHeaderFontSize', N'9', N'N', N'Body Header Font  Size', 12, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipDeductionsFontFamily', N'2', N'N', N'Deductions/Payments Data Cell Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 14, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipDeductionsFontSize', N'9', N'N', N'Deductions/Payments Data Cell Font  Size', 12, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipDocumentHeadersFontFamily', N'2', N'N', N'Document Headers Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 14, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipDocumentHeadersFontSize', N'9', N'N', N'Document Headers Font  Size', 12, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipEmployerNameFontFamily', N'2', N'N', N'Employer Name Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 14, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipEmployerNameFontSize', N'9', N'N', N'Employer Name Font  Size', 12, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipSignatureFontFamily', N'2', N'N', N'Signature Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 14, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipSignatureFontSize', N'9', N'N', N'Signature Font  Size', 12, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipTableDataCellFontFamily', N'2', N'N', N'Table Data Cell Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 14, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipTableDataCellFontSize', N'9', N'N', N'Table Data Cell Font  Size', 12, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipTableHeaderCellFontFamily', N'2', N'N', N'Table Header Cell Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 14, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'AllpayslipTableHeaderCellFontSize', N'9', N'N', N'Table Header Cell Font  Size', 12, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'COMPANYLOGO', N'C:\Program Files\Software Providers\Soft Books Payroll\Resources\VikeInsuranceBrokerslogoImg.jpg', N'S', N'Company Logo', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'COMPANYSLOGAN', N'Broking with Integrity', N'S', N'Company Slogan', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'DEFCONTR', N'20000', N'N', N'Max Defined Contribution', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'DEFCONTRSCHEME', N'2', N'E1,2,3', N'Default contribution scheme', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'EMAILREGEX', N'^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$', N'S', N'Email Validation Regular Expression Pattern', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'EMPNSSF', N'200', N'N', N'Employers NSFF contribution', 4, 1)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'MAXTRIES', N'3', N'N', N'Maximum password retries', 7, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'MINAGE', N'18', N'N', N'Minimum employement age', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'MRELIEF', N'1162', N'N', N'Married persons relief', 2, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'NSSFCOMPUTATIONMETHOD', N'NEW', N'S', N'NSSF computation Method. Values include OLD or NEW(OLD for old computation, NEW for new computation method)', 4, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'NSSFEMPLOYEECONTRIBUTIONPERCENTAGE', N'6', N'S', N'Employee Contribution Percentage for New NSSF Computation Method', 4, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'NSSFEMPLOYERCONTRIBUTIONPERCENTAGE', N'6', N'S', N'Employer Contribution Percentage for New NSSF Computation Method', 4, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'NSSFMAX', N'200', N'N', N'Max NSSF contribution', 4, 1)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'NSSFMAXUPPEREARNINGLIMIT', N'18000', N'N', N'Maximum Amount for the Upper Earning Limit', 4, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'NSSFMINLOWEREARNINGLIMIT', N'6000', N'N', N'Minimum Amount for the Lower Earning Limit', 4, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'NSSFVAL', N'5', N'N', N'NSSFVAL', 4, 1)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PAYEEMIN', N'11136', N'N', N'Minimum earning to start charging PAYE', 5, 1)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipBodyHeaderDataFontFamily', N'0', N'N', N'Body Header Data Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 13, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipBodyHeaderDataFontSize', N'9', N'N', N'Body Header Data Font  Size', 10, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipBodyHeaderFontFamily', N'0', N'N', N'Body Header Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 13, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipBodyHeaderFontSize', N'9', N'N', N'Body Header Font  Size', 10, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipDeductionsFontFamily', N'0', N'N', N'Deductions/Payments Data Cell Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 13, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipDeductionsFontSize', N'9', N'N', N'Deductions/Payments Data Cell Font  Size', 10, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PAYSLIPDETAILS', N'1', N'N', N'Payslip details layout', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipDocumentHeadersFontFamily', N'0', N'N', N'Document Headers Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 13, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipDocumentHeadersFontSize', N'9', N'N', N'Document Headers Font  Size', 10, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipEmployerNameFontFamily', N'0', N'N', N'Employer Name Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 13, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipEmployerNameFontSize', N'9', N'N', N'Employer Name Font  Size', 10, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PAYSLIPPERPAGE', N'1', N'N', N'Payslips per page. Valid values 1 or 2', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipSignatureFontFamily', N'0', N'N', N'Signature Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 13, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipSignatureFontSize', N'9', N'N', N'Signature Font Size', 10, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipTableDataCellFontFamily', N'0', N'N', N'Table Data Cell Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 13, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipTableDataCellFontSize', N'9', N'N', N'Table Data Cell Font Size', 10, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipTableHeaderCellFontFamily', N'0', N'N', N'Table Header Cell Font Family. Values are  0=Courier, 1=Helvetica, 2=Times Roman 4=Zapfdingbats', 13, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'payslipTableHeaderCellFontSize', N'9', N'N', N'Table Header Cell Font Size', 10, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PAYSLIPTYPE', N'VIKE', N'S', N'Payslip to display', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PEN1E', N'6', N'N', N'Employee''s pension contribution %', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PEN1R', N'7.5', N'N', N'Employer''s pension contribution %', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PRELIEF', N'1162', N'N', N'Personal relief', 2, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'PWDSIZE', N'5', N'N', N'Password size', 7, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'REPORTPATH', N'C:\Program Files\Software Providers\Soft Books Payroll\Reports', N'S', N'Report Output Path', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'RESOURCEPATH', N'C:\Program Files\Software Providers\Soft Books Payroll\Resources\', N'S', N'Resource Path', 3, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'SPLANMAX', N'4000', N'N', N'Savings Plan Maximum', 6, 0)
INSERT [dbo].[Settings] ([SSKey], [SSValue], [SSValueType], [Description], [SGroup], [SSSystem]) VALUES (N'SRELIEF', N'1162', N'N', N'Single Personal Relief', 2, 0)
/****** Object:  View [dbo].[vwPayslipDet_Temp]    Script Date: 04/15/2015 23:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayslipDet_Temp]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwPayslipDet_Temp]
AS
SELECT     dbo.Employees.Id AS EmployeeId, dbo.Employees.EmpNo, dbo.Employees.Surname, dbo.Employees.OtherNames, dbo.EmployeeTransactions.ItemId, 
                      dbo.EmployeeTransactions.Balance, CAST(CAST(dbo.PayslipDet_Temp.Year AS varchar) + ''-'' + CAST(dbo.PayslipDet_Temp.Period AS varchar) 
                      + ''-'' + CAST(1 AS varchar) AS DATETIME) AS TxnDate, dbo.PayrollItems.ReFField, dbo.EmployeeTransactions.Amount AS RepayAmount, 
                      dbo.EmployeeTransactions.PostDate, dbo.EmployeeTransactions.InitialAmount, dbo.EmployeeTransactions.LoanType, dbo.Employees.BankAccount, 
                      dbo.Employees.BankCode, dbo.Employees.PaymentMode, dbo.PayrollItems.ItemTypeId, dbo.PayrollItemType.Parent, dbo.PayslipDet_Temp.EmpTxnId, 
                      dbo.PayslipDet_Temp.Period, dbo.PayslipDet_Temp.Year, dbo.PayslipDet_Temp.Description, dbo.PayslipDet_Temp.TaxTracking, dbo.PayslipDet_Temp.Amount, 
                      dbo.PayslipDet_Temp.DEType, dbo.PayslipDet_Temp.IsStatutory, dbo.PayslipDet_Temp.ShowInPayslip, dbo.PayslipDet_Temp.YTD, dbo.Employees.IsActive, 
                      dbo.Employees.IsDeleted, dbo.Employees.EmployerId, dbo.Employees.SystemId
FROM         dbo.EmployeeTransactions INNER JOIN
                      dbo.PayrollItems ON dbo.EmployeeTransactions.ItemId = dbo.PayrollItems.Id INNER JOIN
                      dbo.PayrollItemType ON dbo.PayrollItems.ItemTypeId = dbo.PayrollItemType.Id INNER JOIN
                      dbo.PayslipDet_Temp ON dbo.EmployeeTransactions.Id = dbo.PayslipDet_Temp.EmpTxnId INNER JOIN
                      dbo.Employees ON dbo.EmployeeTransactions.EmployeeId = dbo.Employees.Id
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayslipDet_Temp', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[45] 4[9] 2[44] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
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
         Configuration = "(H (1[54] 2) )"
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
      ActivePaneConfig = 10
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "EmployeeTransactions"
            Begin Extent = 
               Top = 10
               Left = 228
               Bottom = 298
               Right = 413
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItems"
            Begin Extent = 
               Top = 8
               Left = 614
               Bottom = 256
               Right = 779
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "PayrollItemType"
            Begin Extent = 
               Top = 6
               Left = 810
               Bottom = 120
               Right = 978
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayslipDet_Temp"
            Begin Extent = 
               Top = 11
               Left = 435
               Bottom = 297
               Right = 586
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employees"
            Begin Extent = 
               Top = 11
               Left = 14
               Bottom = 294
               Right = 188
            End
            DisplayFlags = 280
            TopColumn = 8
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      PaneHidden = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 31
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1995
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
       ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayslipDet_Temp'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayslipDet_Temp', NULL,NULL))
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
      End
   End
   Begin CriteriaPane = 
      PaneHidden = 
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
/****** Object:  View [dbo].[vwPayslipDet]    Script Date: 04/15/2015 23:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayslipDet]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwPayslipDet]
AS
SELECT     dbo.EmployeeTransactions.Id, dbo.Employees.EmpNo, dbo.Employees.Surname, dbo.Employees.OtherNames, dbo.EmployeeTransactions.ItemId, 
                      dbo.EmployeeTransactions.Balance, dbo.PayrollItems.ReFField, dbo.EmployeeTransactions.Amount AS RepayAmount, dbo.PayslipDet.Period, dbo.PayslipDet.Year, 
                      CAST(CAST(dbo.PayslipDet.Year AS varchar) + ''-'' + CAST(dbo.PayslipDet.Period AS varchar) + ''-'' + CAST(1 AS varchar) AS DATETIME) AS TxnDate, 
                      dbo.EmployeeTransactions.PostDate, dbo.PayslipDet.Description, dbo.PayslipDet.YTD, dbo.EmployeeTransactions.InitialAmount, 
                      dbo.EmployeeTransactions.LoanType, dbo.Employees.BankAccount, dbo.Employees.BankCode, dbo.Employees.PaymentMode, dbo.PayrollItems.ItemTypeId, 
                      dbo.PayslipDet.DEType, dbo.PayslipDet.EmpTxnId, dbo.PayslipDet.TaxTracking, dbo.PayslipDet.ShowInPayslip, dbo.PayslipDet.IsStatutory, dbo.PayslipDet.Amount, 
                      dbo.PayrollItemType.Parent, dbo.Employees.Id AS EmployeeId, dbo.Employees.IsDeleted, dbo.Employees.IsActive, dbo.Employees.EmployerId, 
                      dbo.Employees.SystemId
FROM         dbo.EmployeeTransactions INNER JOIN
                      dbo.PayrollItems ON dbo.EmployeeTransactions.ItemId = dbo.PayrollItems.Id INNER JOIN
                      dbo.PayslipDet ON dbo.EmployeeTransactions.Id = dbo.PayslipDet.EmpTxnId INNER JOIN
                      dbo.PayrollItemType ON dbo.PayrollItems.ItemTypeId = dbo.PayrollItemType.Id INNER JOIN
                      dbo.Employees ON dbo.EmployeeTransactions.EmployeeId = dbo.Employees.Id
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayslipDet', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[10] 2[39] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[47] 4[21] 3) )"
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
         Configuration = "(H (1[51] 2) )"
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
      ActivePaneConfig = 10
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "EmployeeTransactions"
            Begin Extent = 
               Top = 11
               Left = 217
               Bottom = 258
               Right = 395
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItems"
            Begin Extent = 
               Top = 14
               Left = 595
               Bottom = 259
               Right = 778
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayslipDet"
            Begin Extent = 
               Top = 11
               Left = 425
               Bottom = 259
               Right = 576
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItemType"
            Begin Extent = 
               Top = 5
               Left = 812
               Bottom = 112
               Right = 976
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employees"
            Begin Extent = 
               Top = 10
               Left = 6
               Bottom = 261
               Right = 180
            End
            DisplayFlags = 280
            TopColumn = 12
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      PaneHidden = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 32
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
/****** Object:  View [dbo].[vwPayrollMaster_Temp]    Script Date: 04/15/2015 23:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayrollMaster_Temp]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwPayrollMaster_Temp]
AS
SELECT        dbo.Banks.BankName, dbo.BankBranch.BranchName, dbo.Employees.Surname, dbo.Employees.OtherNames, dbo.BankBranch.BranchCode, 
                         dbo.BankBranch.BankSortCode, dbo.Employees.PaymentMode, dbo.Employers.BankBranchSortCode, dbo.Employees.BankAccount, dbo.Employees.IDNo, 
                         dbo.Banks.BankCode, dbo.PayslipMaster_Temp.Period, dbo.PayslipMaster_Temp.Year, dbo.PayslipMaster_Temp.PaymentDate, 
                         dbo.PayslipMaster_Temp.PrintedBy, dbo.PayslipMaster_Temp.PrintedOn, dbo.PayslipMaster_Temp.EmpName, dbo.PayslipMaster_Temp.PayPoint, 
                         dbo.PayslipMaster_Temp.PIN, dbo.PayslipMaster_Temp.NHIFNo, dbo.PayslipMaster_Temp.Department, dbo.PayslipMaster_Temp.NSSFNo, 
                         dbo.PayslipMaster_Temp.EmpGroup, dbo.PayslipMaster_Temp.EmpPayroll, dbo.PayslipMaster_Temp.CompName, dbo.PayslipMaster_Temp.CompAddr, 
                         dbo.PayslipMaster_Temp.CompTel, dbo.PayslipMaster_Temp.PayeTax, dbo.PayslipMaster_Temp.BasicPay, dbo.PayslipMaster_Temp.Benefits, 
                         dbo.PayslipMaster_Temp.OtherDeductions, dbo.PayslipMaster_Temp.GrossTaxableEarnings, dbo.PayslipMaster_Temp.NetTaxableEarnings, 
                         dbo.PayslipMaster_Temp.GrossTax, dbo.PayslipMaster_Temp.EmployerNSSF, dbo.PayslipMaster_Temp.PensionEmployee, dbo.PayslipMaster_Temp.BankBranch, 
                         dbo.PayslipMaster_Temp.Account, dbo.PayslipMaster_Temp.NSSF, dbo.PayslipMaster_Temp.NHIF, dbo.PayslipMaster_Temp.NetPay, dbo.Employees.EmpNo, 
                         dbo.PayslipMaster_Temp.Variables, dbo.PayslipMaster_Temp.MortgageRelief, dbo.PayslipMaster_Temp.PersonalRelief, 
                         dbo.PayslipMaster_Temp.PensionEmployer, dbo.PayslipMaster_Temp.InsuranceRelief, dbo.PayslipMaster_Temp.EmployeeId, dbo.Employees.EmployerId
FROM            dbo.Employees INNER JOIN
                         dbo.PayslipMaster_Temp ON dbo.Employees.Id = dbo.PayslipMaster_Temp.EmployeeId INNER JOIN
                         dbo.Employers ON dbo.Employees.EmployerId = dbo.Employers.Id INNER JOIN
                         dbo.Banks INNER JOIN
                         dbo.BankBranch ON dbo.Banks.BankCode = dbo.BankBranch.BankCode ON dbo.Employees.BankCode = dbo.BankBranch.BankSortCode
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayrollMaster_Temp', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[16] 4[33] 2[28] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[40] 2[30] 3) )"
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
         Configuration = "(H (2[66] 3) )"
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
         Configuration = "(H (1[19] 2) )"
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
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Employees"
            Begin Extent = 
               Top = 6
               Left = 233
               Bottom = 702
               Right = 420
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayslipMaster_Temp"
            Begin Extent = 
               Top = 10
               Left = 9
               Bottom = 703
               Right = 195
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employers"
            Begin Extent = 
               Top = 11
               Left = 432
               Bottom = 384
               Right = 633
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Banks"
            Begin Extent = 
               Top = 12
               Left = 860
               Bottom = 114
               Right = 1011
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BankBranch"
            Begin Extent = 
               Top = 12
               Left = 677
               Bottom = 163
               Right = 828
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
      Begin ColumnWidths = 51
         Width = 284
         Width = 2505
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
         Column = 1875
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
/****** Object:  View [dbo].[vwPayrollMaster]    Script Date: 04/15/2015 23:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwPayrollMaster]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwPayrollMaster]
AS
SELECT        dbo.Banks.BankName, dbo.BankBranch.BranchName, dbo.Employees.Surname, dbo.Employees.OtherNames, dbo.BankBranch.BranchCode, 
                         dbo.BankBranch.BankSortCode, dbo.Employees.PaymentMode, dbo.Employers.BankBranchSortCode, dbo.Employees.BankAccount, dbo.Employees.IDNo, 
                         dbo.Banks.BankCode, dbo.PayslipMaster.Period, dbo.PayslipMaster.Year, dbo.PayslipMaster.PaymentDate, dbo.PayslipMaster.PrintedBy, 
                         dbo.PayslipMaster.PrintedOn, dbo.PayslipMaster.EmpName, dbo.PayslipMaster.PayPoint, dbo.PayslipMaster.PIN, dbo.PayslipMaster.NHIFNo, 
                         dbo.PayslipMaster.Department, dbo.PayslipMaster.NSSFNo, dbo.PayslipMaster.EmpGroup, dbo.PayslipMaster.EmpPayroll, dbo.PayslipMaster.CompName, 
                         dbo.PayslipMaster.CompAddr, dbo.PayslipMaster.CompTel, dbo.PayslipMaster.PayeTax, dbo.PayslipMaster.BasicPay, dbo.PayslipMaster.Benefits, 
                         dbo.PayslipMaster.OtherDeductions, dbo.PayslipMaster.GrossTaxableEarnings, dbo.PayslipMaster.NetTaxableEarnings, dbo.PayslipMaster.GrossTax, 
                         dbo.PayslipMaster.EmployerNSSF, dbo.PayslipMaster.PensionEmployee, dbo.PayslipMaster.BankBranch, dbo.PayslipMaster.Account, dbo.PayslipMaster.NSSF, 
                         dbo.PayslipMaster.NHIF, dbo.PayslipMaster.NetPay, dbo.Employees.EmpNo, dbo.PayslipMaster.Variables, dbo.PayslipMaster.MortgageRelief, 
                         dbo.PayslipMaster.PersonalRelief, dbo.PayslipMaster.PensionEmployer, dbo.PayslipMaster.InsuranceRelief, dbo.PayslipMaster.EmployeeId, 
                         dbo.Employees.EmployerId
FROM            dbo.Employees INNER JOIN
                         dbo.PayslipMaster ON dbo.Employees.Id = dbo.PayslipMaster.EmployeeId INNER JOIN
                         dbo.Employers ON dbo.Employees.EmployerId = dbo.Employers.Id INNER JOIN
                         dbo.Banks INNER JOIN
                         dbo.BankBranch ON dbo.Banks.BankCode = dbo.BankBranch.BankCode ON dbo.Employees.BankCode = dbo.BankBranch.BankSortCode
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayrollMaster', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[22] 4[7] 2[64] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1[50] 4[25] 3) )"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1[39] 2[27] 3) )"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4[30] 2[40] 3) )"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1[54] 3) )"
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
         Configuration = "(H (1[39] 2) )"
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
      ActivePaneConfig = 2
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PayslipMaster"
            Begin Extent = 
               Top = 8
               Left = 14
               Bottom = 704
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employees"
            Begin Extent = 
               Top = 8
               Left = 222
               Bottom = 698
               Right = 409
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Banks"
            Begin Extent = 
               Top = 6
               Left = 840
               Bottom = 102
               Right = 991
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BankBranch"
            Begin Extent = 
               Top = 5
               Left = 648
               Bottom = 138
               Right = 809
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employers"
            Begin Extent = 
               Top = 8
               Left = 425
               Bottom = 386
               Right = 626
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
      Begin ColumnWidths = 50
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
         Width = 1500
         Wid' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPayrollMaster'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'vwPayrollMaster', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'th = 1500
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
/****** Object:  View [dbo].[vwBankBranches]    Script Date: 04/15/2015 23:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[vwBankBranches]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[vwBankBranches]
AS
SELECT     dbo.Banks.BankName + '' - '' + dbo.BankBranch.BranchName AS BankBranchName, dbo.BankBranch.BankSortCode, dbo.Banks.BankName, 
                      dbo.BankBranch.BankCode, dbo.BankBranch.BranchCode, dbo.BankBranch.BranchName, dbo.Banks.BankCode AS Expr1
FROM         dbo.BankBranch INNER JOIN
                      dbo.Banks ON dbo.BankBranch.BankCode = dbo.Banks.BankCode
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'vwBankBranches', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[37] 4[4] 2[25] 3) )"
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
         Configuration = "(H (1[37] 2) )"
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
      ActivePaneConfig = 10
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
               Bottom = 137
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Banks"
            Begin Extent = 
               Top = 6
               Left = 227
               Bottom = 111
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
      PaneHidden = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 4995
         Width = 2535
         Width = 2415
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
/****** Object:  View [dbo].[GetEmpTransactions]    Script Date: 04/15/2015 23:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[GetEmpTransactions]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [dbo].[GetEmpTransactions]
AS
SELECT     dbo.Employees.Surname, dbo.Employees.MaritalStatus, dbo.Employees.IsActive, dbo.EmployeeTransactions.ItemId, dbo.EmployeeTransactions.Amount, 
                      dbo.EmployeeTransactions.Recurrent, dbo.EmployeeTransactions.Enabled, dbo.Employees.BasicPay, dbo.Departments.Description AS Department, 
                      dbo.Departments.Code AS DeptCode, dbo.EmployeeTransactions.TrackYTD AS EmpTrack, dbo.EmployeeTransactions.Balance, dbo.PayrollItems.ItemTypeId, 
                      dbo.PayrollItems.TaxTrackingId, dbo.PayrollItems.Active, dbo.PayrollItems.AddToPension, dbo.EmployeeTransactions.ShowYTDInPayslip, 
                      dbo.EmployeeTransactions.Id AS EmpTxnId, dbo.EmployeeTransactions.Processed, dbo.PayrollItemType.Parent, dbo.Employees.EmpNo, 
                      dbo.Employees.Id AS EmployeeId, dbo.Employees.OtherNames, dbo.Employees.DoB, dbo.Employees.Gender, dbo.Employees.Photo, dbo.Employees.IsDeleted, 
                      dbo.EmployeeTransactions.PostDate, dbo.PayrollItems.Enable, dbo.Employees.SystemId
FROM         dbo.EmployeeTransactions INNER JOIN
                      dbo.PayrollItems ON dbo.EmployeeTransactions.ItemId = dbo.PayrollItems.Id INNER JOIN
                      dbo.PayrollItemType ON dbo.PayrollItems.ItemTypeId = dbo.PayrollItemType.Id INNER JOIN
                      dbo.Employees ON dbo.EmployeeTransactions.EmployeeId = dbo.Employees.Id INNER JOIN
                      dbo.Departments ON dbo.Employees.DepartmentId = dbo.Departments.Id
'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane1' , N'SCHEMA',N'dbo', N'VIEW',N'GetEmpTransactions', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[18] 2[32] 3) )"
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
         Configuration = "(H (4[24] 2[67] 3) )"
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
         Configuration = "(H (1[48] 2) )"
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
      ActivePaneConfig = 10
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "EmployeeTransactions"
            Begin Extent = 
               Top = 7
               Left = 232
               Bottom = 225
               Right = 428
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItems"
            Begin Extent = 
               Top = 0
               Left = 482
               Bottom = 213
               Right = 645
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PayrollItemType"
            Begin Extent = 
               Top = 7
               Left = 824
               Bottom = 117
               Right = 988
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Employees"
            Begin Extent = 
               Top = 5
               Left = 9
               Bottom = 212
               Right = 183
            End
            DisplayFlags = 280
            TopColumn = 27
         End
         Begin Table = "Departments"
            Begin Extent = 
               Top = 0
               Left = 655
               Bottom = 120
               Right = 806
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
      PaneHidden = 
      Begin Colu' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'GetEmpTransactions'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_DiagramPane2' , N'SCHEMA',N'dbo', N'VIEW',N'GetEmpTransactions', NULL,NULL))
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'mnWidths = 11
         Column = 2160
         Alias = 1050
         Table = 1800
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
/****** Object:  ForeignKey [FK_BankBranch_Banks]    Script Date: 04/15/2015 23:58:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BankBranch_Banks]') AND parent_object_id = OBJECT_ID(N'[dbo].[BankBranch]'))
ALTER TABLE [dbo].[BankBranch]  WITH CHECK ADD  CONSTRAINT [FK_BankBranch_Banks] FOREIGN KEY([BankCode])
REFERENCES [dbo].[Banks] ([BankCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BankBranch_Banks]') AND parent_object_id = OBJECT_ID(N'[dbo].[BankBranch]'))
ALTER TABLE [dbo].[BankBranch] CHECK CONSTRAINT [FK_BankBranch_Banks]
GO
/****** Object:  ForeignKey [FK_Employees_Employers]    Script Date: 04/15/2015 23:58:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employees_Employers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employees]'))
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Employers] FOREIGN KEY([EmployerId])
REFERENCES [dbo].[Employers] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Employees_Employers]') AND parent_object_id = OBJECT_ID(N'[dbo].[Employees]'))
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Employers]
GO
/****** Object:  ForeignKey [FK_PayrollItems_TaxTracking]    Script Date: 04/15/2015 23:58:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayrollItems_TaxTracking]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayrollItems]'))
ALTER TABLE [dbo].[PayrollItems]  WITH CHECK ADD  CONSTRAINT [FK_PayrollItems_TaxTracking] FOREIGN KEY([TaxTrackingId])
REFERENCES [dbo].[TaxTracking] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PayrollItems_TaxTracking]') AND parent_object_id = OBJECT_ID(N'[dbo].[PayrollItems]'))
ALTER TABLE [dbo].[PayrollItems] CHECK CONSTRAINT [FK_PayrollItems_TaxTracking]
GO
/****** Object:  ForeignKey [FK_Settings_SettingsGroup]    Script Date: 04/15/2015 23:58:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Settings_SettingsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Settings]'))
ALTER TABLE [dbo].[Settings]  WITH CHECK ADD  CONSTRAINT [FK_Settings_SettingsGroup] FOREIGN KEY([SGroup])
REFERENCES [dbo].[SettingsGroup] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Settings_SettingsGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[Settings]'))
ALTER TABLE [dbo].[Settings] CHECK CONSTRAINT [FK_Settings_SettingsGroup]
GO
/****** Object:  ForeignKey [FK_spAllowedReportsRolesMenus_spReportsMenuItems]    Script Date: 04/15/2015 23:58:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_spAllowedReportsRolesMenus_spReportsMenuItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[spAllowedReportsRolesMenus]'))
ALTER TABLE [dbo].[spAllowedReportsRolesMenus]  WITH CHECK ADD  CONSTRAINT [FK_spAllowedReportsRolesMenus_spReportsMenuItems] FOREIGN KEY([MenuItemId])
REFERENCES [dbo].[spReportsMenuItems] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_spAllowedReportsRolesMenus_spReportsMenuItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[spAllowedReportsRolesMenus]'))
ALTER TABLE [dbo].[spAllowedReportsRolesMenus] CHECK CONSTRAINT [FK_spAllowedReportsRolesMenus_spReportsMenuItems]
GO
/****** Object:  ForeignKey [FK_spAllowedRoleMenus_spMenuItems]    Script Date: 04/15/2015 23:58:58 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_spAllowedRoleMenus_spMenuItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[spAllowedRoleMenus]'))
ALTER TABLE [dbo].[spAllowedRoleMenus]  WITH CHECK ADD  CONSTRAINT [FK_spAllowedRoleMenus_spMenuItems] FOREIGN KEY([MenuItemId])
REFERENCES [dbo].[spMenuItems] ([Id])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_spAllowedRoleMenus_spMenuItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[spAllowedRoleMenus]'))
ALTER TABLE [dbo].[spAllowedRoleMenus] CHECK CONSTRAINT [FK_spAllowedRoleMenus_spMenuItems]
GO
