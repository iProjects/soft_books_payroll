USE [SBPayrollDB_july_2024]
GO
/****** Object:  StoredProcedure [dbo].[GetEmployeesPageWise]    Script Date: 02/09/2026 08:48:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[GetEmployeesPageWise]
      @PageIndex INT = 1,
      @PageSize INT = 10,
      @EmpNo NVARCHAR(50) = NULL,
      @Surname NVARCHAR(100) = NULL,
      @OtherNames NVARCHAR(100) = NULL,
      @Department NVARCHAR(100) = NULL,
      @EmployerId INT = NULL,
      @IsActive BIT = NULL,
      @RecordCount INT OUTPUT
AS
BEGIN
      SET NOCOUNT ON;

      SELECT ROW_NUMBER() OVER
      (
            ORDER BY e.[Id] DESC
      ) AS RowNumber,
            e.[Id],
            e.[EmpNo],
            e.[Surname],
            e.[OtherNames],
            e.[Email],
            e.[DoB],
            e.[MaritalStatus],
            e.[Gender],
			e.[PhoneNo],
            e.[Photo],
            e.[DoE],
            e.[BasicComputation],
            e.[BasicPay],
            e.[PersonalRelief],
            e.[MortgageRelief],
            e.[InsuranceRelief],
            e.[NSSFNo],
            e.[NHIFNo],
            e.[IDNo],
            e.[PINNo],
            e.[DepartmentId],
            e.[EmployerId],
            e.[PayPoint],
            e.[EmpGroup],
            e.[EmpPayroll],
            e.[PrevEmployer],
            e.[DateLeft],
            e.[PaymentMode],
            e.[TelephoneNo],
            e.[ChequeNo],
            e.[BankCode],
            e.[BankAccount],
            e.[LeaveBalance],
            e.[IsActive],
            e.[CreatedBy],
            e.[CreatedOn],
            e.[IsDeleted],
            e.[SystemId]
      INTO #Results
      FROM [Employees] e
      LEFT JOIN [Departments] d ON e.DepartmentId = d.Id
      WHERE e.IsDeleted = 0
        AND (@IsActive IS NULL OR e.IsActive = @IsActive)
        AND (@EmployerId IS NULL OR e.EmployerId = @EmployerId)
        AND (@EmpNo IS NULL OR @EmpNo = '' OR e.EmpNo LIKE '%' + @EmpNo + '%')
        AND (@Surname IS NULL OR @Surname = '' OR e.Surname LIKE '%' + @Surname + '%')
        AND (@OtherNames IS NULL OR @OtherNames = '' OR e.OtherNames LIKE '%' + @OtherNames + '%')
        AND (@Department IS NULL OR @Department = '' OR d.Description LIKE '%' + @Department + '%');
   
      SELECT @RecordCount = COUNT(*) FROM #Results;
         
      SELECT [Id],
            [EmpNo],
            [Surname],
            [OtherNames],
            [Email],
            [DoB],
            [MaritalStatus],
            [Gender],
			[PhoneNo],
            [Photo],
            [DoE],
            [BasicComputation],
            [BasicPay],
            [PersonalRelief],
            [MortgageRelief],
            [InsuranceRelief],
            [NSSFNo],
            [NHIFNo],
            [IDNo],
            [PINNo],
            [DepartmentId],
            [EmployerId],
            [PayPoint],
            [EmpGroup],
            [EmpPayroll],
            [PrevEmployer],
            [DateLeft],
            [PaymentMode],
            [TelephoneNo],
            [ChequeNo],
            [BankCode],
            [BankAccount],
            [LeaveBalance],
            [IsActive],
            [CreatedBy],
            [CreatedOn],
            [IsDeleted],
            [SystemId]
      FROM #Results
      WHERE RowNumber BETWEEN (@PageIndex - 1) * @PageSize + 1 AND (@PageIndex * @PageSize);
   
      DROP TABLE #Results;
END