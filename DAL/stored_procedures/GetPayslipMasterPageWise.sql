USE [SBPayrollDB_july_2024]
GO
/****** Object:  StoredProcedure [dbo].[GetPayslipMasterPageWise]    Script Date: 02/09/2026 08:48:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[GetPayslipMasterPageWise]
      @PageIndex INT = 1
      ,@PageSize INT = 10
      ,@RecordCount INT OUTPUT
      ,@CompName VARCHAR(100) = NULL 
	  ,@EmpName VARCHAR(100) = NULL 
	  ,@EmpNo VARCHAR(100) = NULL 
	  ,@Department VARCHAR(100) = NULL 
      ,@Period INT = NULL
      ,@Year INT = NULL
AS
BEGIN
      SET NOCOUNT ON;

      SELECT ROW_NUMBER() OVER
      (
            ORDER BY [Year] DESC, [Period] DESC, [EmpNo] ASC
      ) AS RowNumber
            ,[Period]
            ,[Year]
            ,[EmpNo]
            ,[EmployeeId]
            ,[PaymentDate]
            ,[PrintedBy]
            ,[PrintedOn]
            ,[EmpName]
            ,[PayPoint]
            ,[PIN]
            ,[NHIFNo]
            ,[NSSFNo]
            ,[Department]
            ,[EmpGroup]
            ,[EmpPayroll]
            ,[CompName]
            ,[CompAddr]
            ,[CompTel]
            ,[PayeTax]
            ,[BasicPay]
            ,[Benefits]
            ,[Variables]
            ,[OtherDeductions]
            ,[GrossTaxableEarnings]
            ,[NetTaxableEarnings]
            ,[MortgageRelief]
            ,[InsuranceRelief]
            ,[GrossTax]
            ,[PersonalRelief]
            ,[PensionEmployer]
            ,[EmployerNSSF]
            ,[PensionEmployee]
            ,[BankBranch]
            ,[Account]
            ,[NSSF]
            ,[NHIF]
            ,[NetPay]
      INTO #Results
      FROM [PayslipMaster] 
      WHERE (@CompName IS NULL OR [CompName] = @CompName) 
        AND (@EmpName IS NULL OR [EmpName] = @EmpName)
		AND (@EmpNo IS NULL OR [EmpNo] = @EmpNo)
		AND (@Department IS NULL OR [Department] = @Department)
		AND (@Period IS NULL OR [Period] = @Period)
        AND (@Year IS NULL OR [Year] = @Year)
   
      SELECT @RecordCount = COUNT(*)
      FROM #Results
         
      SELECT [Period]
            ,[Year]
            ,[EmpNo]
            ,[EmployeeId]
            ,[PaymentDate]
            ,[PrintedBy]
            ,[PrintedOn]
            ,[EmpName]
            ,[PayPoint]
            ,[PIN]
            ,[NHIFNo]
            ,[NSSFNo]
            ,[Department]
            ,[EmpGroup]
            ,[EmpPayroll]
            ,[CompName]
            ,[CompAddr]
            ,[CompTel]
            ,[PayeTax]
            ,[BasicPay]
            ,[Benefits]
            ,[Variables]
            ,[OtherDeductions]
            ,[GrossTaxableEarnings]
            ,[NetTaxableEarnings]
            ,[MortgageRelief]
            ,[InsuranceRelief]
            ,[GrossTax]
            ,[PersonalRelief]
            ,[PensionEmployer]
            ,[EmployerNSSF]
            ,[PensionEmployee]
            ,[BankBranch]
            ,[Account]
            ,[NSSF]
            ,[NHIF]
            ,[NetPay]
      FROM #Results
      WHERE RowNumber BETWEEN (@PageIndex - 1) * @PageSize + 1 AND @PageIndex * @PageSize
   
      DROP TABLE #Results
END
GO



