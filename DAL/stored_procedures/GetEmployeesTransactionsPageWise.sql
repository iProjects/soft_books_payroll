USE [SBPayrollDB_july_2024]
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeesTransactionsPageWise]    Script Date: 02/09/2026 08:48:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeesTransactionsPageWise]
      @PageIndex INT = 1
      ,@PageSize INT = 10
      ,@RecordCount INT OUTPUT
	  ,@ItemId NVARCHAR(1000)=NULL
	  ,@EmpNo NVARCHAR(1000)=NULL	  
      ,@EmployerId INT = NULL
AS
BEGIN
      SET NOCOUNT ON;
      SELECT ROW_NUMBER() OVER
      (
            ORDER BY et.[Id] DESC
      )AS RowNumber
            ,et.[Id]
            ,et.[PostDate]
            ,et.[EmpNo]
			,et.[EmployeeId]
			,emp.[Id] AS EmployerId
			,et.[Enabled]
			,et.[Recurrent]
			,et.[ItemId]
			,et.[Amount]
			,et.[Balance]
			,et.[InitialAmount] 
			,et.[Processed] 
			,et.[TrackYTD] 
			,et.[ShowYTDInPayslip] 
			,et.[CreatedBy] 
			,et.[LastChangedBy] 
			,et.[LastChangeDate] 
			,et.[AuthorizedBy] 
			,et.[AuthorizeDate] 
			,et.[LoanType] 
			,et.[AccumulativePayment] 
			,et.[IsDeleted] 
			,et.[IsLoanRepaymentInFull] 
			,et.[LoanBeingRepayedId] 
			,et.[SaccoBeingRepayedId] 
			,et.[IsLoanRepaymentInCash] 
			,et.[IsSharesRepaymentInCash] 
       INTO #Results
      FROM [EmployeeTransactions] et
      LEFT JOIN [Employees] e ON et.[EmployeeId] = e.[Id]
      LEFT JOIN [Employers] emp ON e.[EmployerId] = emp.[Id]
	  WHERE (@ItemId IS NULL OR et.[ItemId] = @ItemId)
	  AND (@EmpNo IS NULL OR et.[EmpNo] = @EmpNo)
      AND (@EmployerId IS NULL OR emp.[Id] = @EmployerId)
   
      SELECT @RecordCount = COUNT(*)
      FROM #Results
         
      SELECT [Id]
            ,[PostDate]
            ,[EmpNo] 
			,[EmployeeId] 
			,[EmployerId] 
			,[Enabled] 
			,[Recurrent] 
			,[ItemId]
			,[Amount]
			,[Balance]
			,[InitialAmount] 
			,[Processed] 
			,[TrackYTD] 
			,[ShowYTDInPayslip] 
			,[CreatedBy] 
			,[LastChangedBy] 
			,[LastChangeDate] 
			,[AuthorizedBy] 
			,[AuthorizeDate] 
			,[LoanType] 
			,[AccumulativePayment] 
			,[IsDeleted] 
			,[IsLoanRepaymentInFull] 
			,[LoanBeingRepayedId] 
			,[SaccoBeingRepayedId] 
			,[IsLoanRepaymentInCash] 
			,[IsSharesRepaymentInCash] 
      FROM #Results
      WHERE RowNumber BETWEEN(@PageIndex -1) * @PageSize + 1 AND(((@PageIndex -1) * @PageSize + 1) + @PageSize) - 1
   
      DROP TABLE #Results
END



      