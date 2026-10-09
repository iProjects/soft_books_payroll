USE [SBPayrollDB_july_2024]
GO
/****** Object:  StoredProcedure [dbo].[GetPayrollsPageWise]    Script Date: 02/09/2026 08:48:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[GetPayrollsPageWise]
      @PageIndex INT = 1
      ,@PageSize INT = 10
      ,@RecordCount INT OUTPUT
	  ,@EmployerId INT = NULL
	  ,@Year INT = NULL
	  ,@IsOpen BIT = NULL
	  ,@Processed BIT = NULL
AS
BEGIN
      SET NOCOUNT ON;
      SELECT ROW_NUMBER() OVER
      (
            ORDER BY [Year] DESC, [Period] DESC, [IsOpen] ASC, [Processed] ASC
      )AS RowNumber
            ,[Period]
			,[Year]
			,[EmployerId]
			,[DateRun]
			,[TimeRun]
			,[RunBy]
			,[Approved]
			,[ApprovedBy]
			,[IsOpen]
			,[Processed]
       INTO #Results
      FROM [Payrolls] 
	  WHERE (@EmployerId IS NULL OR [EmployerId] = @EmployerId)
	  AND (@Year IS NULL OR [Year] = @Year)
	  AND (@IsOpen IS NULL OR ISNULL([IsOpen], 0) = @IsOpen)
      AND (@Processed IS NULL OR ISNULL([Processed], 0) = @Processed) -- Fix: treats NULL in database as false (0)
   
      SELECT @RecordCount = COUNT(*)
      FROM #Results
         
      SELECT [Period]
			,[Year]
			,[EmployerId]
			,[DateRun]
			,[TimeRun]
			,[RunBy]
			,[Approved]
			,[ApprovedBy]
			,[IsOpen]
			,[Processed]
      FROM #Results
      WHERE RowNumber BETWEEN(@PageIndex -1) * @PageSize + 1 AND(((@PageIndex -1) * @PageSize + 1) + @PageSize) - 1
   
      DROP TABLE #Results
END



      