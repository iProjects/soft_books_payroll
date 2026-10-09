USE [SBPayrollDB_july_2024]
GO
/****** Object:  StoredProcedure [dbo].[GetPayrollItemsPageWise]    Script Date: 02/09/2026 08:48:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
CREATE PROCEDURE [dbo].[GetPayrollItemsPageWise]
      @PageIndex INT = 1
      ,@PageSize INT = 10
      ,@RecordCount INT OUTPUT
	  ,@Id VARCHAR(50) = NULL
      ,@Description VARCHAR(255) = NULL
      ,@ItemTypeId VARCHAR(50) = NULL
      ,@TaxTrackingId VARCHAR(50) = NULL
      ,@DefaultItem BIT = NULL
AS
BEGIN
      SET NOCOUNT ON;

	  -- Populate temporary table with filtered data
      SELECT ROW_NUMBER() OVER
      (
            ORDER BY [Id] ASC
      )AS RowNumber,
            [Id],
            [Description],
            [ItemTypeId],
            [TaxTrackingId],
            [PayableTo],
            [GLAccount],
            [ReFField],
            [DefaultItem],
            [AddToPension],
            [Active],
            [Enable],
            [IsDeleted]
       INTO #Results
      FROM [PayrollItems] 
	  WHERE (@Description IS NULL OR [Description] LIKE '%' + @Description + '%')
        AND (@ItemTypeId IS NULL OR [ItemTypeId] = @ItemTypeId)
        AND (@TaxTrackingId IS NULL OR [TaxTrackingId] = @TaxTrackingId) 
        AND (@DefaultItem IS NULL OR ISNULL([DefaultItem], 0) = @DefaultItem)
   
	 -- Capture total matching records count
      SELECT @RecordCount = COUNT(*)
      FROM #Results
         
	-- Fetch the paged record set
      SELECT [Id],
             [Description],
             [ItemTypeId],
             [TaxTrackingId],
             [PayableTo],
             [GLAccount],
             [ReFField],
             [DefaultItem],
             [AddToPension],
             [Active],
             [Enable],
             [IsDeleted]
      FROM #Results
      WHERE RowNumber BETWEEN(@PageIndex -1) * @PageSize + 1 AND(((@PageIndex -1) * @PageSize + 1) + @PageSize) - 1
   
      DROP TABLE #Results
END



      