USE [SBPayrollDB_july_2024]
GO
/****** Object:  StoredProcedure [dbo].[GetPayslipDetsTempPageWise]    Script Date: 02/09/2026 08:48:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[GetPayslipDetsTempPageWise]
      @PageIndex INT = 1
      ,@PageSize INT = 10
      ,@RecordCount INT OUTPUT
	  ,@EmpNo NVARCHAR(100) = NULL
	  ,@Year INT = NULL
	  ,@Period INT = NULL
	  ,@Description NVARCHAR(100) = NULL
	  ,@TaxTracking NVARCHAR(100) = NULL
	  ,@IsStatutory BIT = NULL 
AS
BEGIN
      SET NOCOUNT ON;
      SELECT ROW_NUMBER() OVER
      (
            ORDER BY [Year] DESC, [Period] DESC, [Id] ASC
      )AS RowNumber
            ,[Id]
			,[EmpNo]
			,[EmployeeId]
			,[EmpTxnId]
			,[Period]
			,[Year]
			,[Description]
			,[TaxTracking]
			,[Amount]
			,[DEType]
			,[IsStatutory]
			,[ShowInPayslip]
			,[YTD]
       INTO #Results
      FROM [PayslipDet_Temp] 
	  WHERE (@EmpNo IS NULL OR [EmpNo] = @EmpNo)
	  AND (@Year IS NULL OR [Year] = @Year)
	  AND (@Period IS NULL OR [Period] = @Period)
	  AND (@Description IS NULL OR [Description] = @Description)
	  AND (@TaxTracking IS NULL OR [TaxTracking] = @TaxTracking) 
      AND (@IsStatutory IS NULL OR ISNULL([IsStatutory], 0) = @IsStatutory) -- Fix: treats NULL in database as false (0)
    
      SELECT @RecordCount = COUNT(*)
      FROM #Results
         
      SELECT [Id]
			,[EmpNo]
			,[EmployeeId]
			,[EmpTxnId]
			,[Period]
			,[Year]
			,[Description]
			,[TaxTracking]
			,[Amount]
			,[DEType]
			,[IsStatutory]
			,[ShowInPayslip]
			,[YTD]
      FROM #Results
      WHERE RowNumber BETWEEN(@PageIndex -1) * @PageSize + 1 AND(((@PageIndex -1) * @PageSize + 1) + @PageSize) - 1
   
      DROP TABLE #Results
END



      