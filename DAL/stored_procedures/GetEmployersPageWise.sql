USE [SBPayrollDB_july_2024]
GO
/****** Object:  StoredProcedure [dbo].[GetEmployersPageWise]    Script Date: 02/09/2026 08:48:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployersPageWise]
      @PageIndex INT = 1,
      @PageSize INT = 10,
      @RecordCount INT OUTPUT, 
      @Name NVARCHAR(100) = NULL,
      @Email NVARCHAR(100) = NULL,
      @Telephone NVARCHAR(100) = NULL, 
      @IsActive BIT = NULL
AS
BEGIN
      SET NOCOUNT ON;

      SELECT ROW_NUMBER() OVER
      (
            ORDER BY e.[Id] DESC
      ) AS RowNumber,
            e.[Id],
            e.[Name],
            e.[Email],
            e.[Telephone],
            e.[Address1],
            e.[Address2],
            e.[Slogan],
            e.[AuthorizedSignatory],
            e.[PIN],
            e.[NHIF],
            e.[NSSF],
            ISNULL(empSummary.EmployeeCount, 0) AS [NoOfEmployees],
            e.[BankBranchSortCode],
            e.[AccountName],
            e.[AccountNo],
            e.[Logo],
            e.[IsDefault],
            e.[IsActive],
            e.[IsDeleted]
      INTO #Results
      FROM [Employers] e
      LEFT JOIN (
            SELECT EmployerId, COUNT(*) AS EmployeeCount
            FROM [Employees]
            WHERE IsDeleted = 0
              AND (@IsActive IS NULL OR IsActive = 1)
            GROUP BY EmployerId
      ) empSummary ON e.[Id] = empSummary.[EmployerId]
      WHERE e.IsDeleted = 0
        AND (@IsActive IS NULL OR e.IsActive = @IsActive)
        AND (@Name IS NULL OR @Name = '' OR e.Name LIKE '%' + @Name + '%')
        AND (@Email IS NULL OR @Email = '' OR e.Email LIKE '%' + @Email + '%')
        AND (@Telephone IS NULL OR @Telephone = '' OR e.Telephone LIKE '%' + @Telephone + '%');
   
      -- Set total output record count
      SELECT @RecordCount = COUNT(*) FROM #Results;
         
      -- Fetch the target page subset
      SELECT [Id],
            [Name],
            [Email],
            [Telephone],
            [Address1],
            [Address2],
            [Slogan],
            [AuthorizedSignatory],
            [PIN],
            [NHIF],
            [NSSF],
            [NoOfEmployees],
            [BankBranchSortCode],
            [AccountName],
            [AccountNo],
            [Logo],
            [IsDefault],
            [IsActive],
            [IsDeleted]
      FROM #Results
      WHERE RowNumber BETWEEN (@PageIndex - 1) * @PageSize + 1 AND (@PageIndex * @PageSize);
   
      DROP TABLE #Results;
END
GO
