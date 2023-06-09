USE [Thegioididong]
GO
/****** Object:  StoredProcedure [dbo].[sp_slide_getslidespublic]    Script Date: 07/03/2023 12:32:52 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_slide_getslidespublic]
(
@request NVARCHAR(MAX)
)
AS
    BEGIN
	DECLARE @PageIndex INT;
	DECLARE @PageSize INT;
	DECLARE @TotalRecords INT;
	DECLARE @TotalPages FLOAT;
	DECLARE @Page VARCHAR(160);
	DECLARE @Position VARCHAR(160);
	
	SELECT @PageIndex = JSON_VALUE(@request, '$.pageIndex');
	SELECT @PageSize = JSON_VALUE(@request, '$.pageSize');
	SELECT @Page = JSON_VALUE(@request, '$.page');
	SELECT @Position= JSON_VALUE(@request, '$.position');
	
	IF (@PageSize IS NULL OR @PageSize <= 0)
BEGIN
    SET NOCOUNT ON;

	SELECT @PageIndex AS PageIndex, @PageSize AS PageSize, 
	(
	 SELECT COUNT(*) FROM Slides AS s
        WHERE s.Published = 1
            AND (s.Page = @Page OR @Page IS NULL OR @Page = '')
            AND (s.Position = @Position OR @Position IS NULL OR @Position = '')
    ) AS TotalRecords,
	1 AS TotalPages, (
        SELECT s.Id, s.Name, s.Page, s.Position, (
        SELECT st.Title, st.Image, st.URL FROM SlideItems AS st
        WHERE st.SlideId = s.Id
        FOR JSON PATH
    ) AS SlideItems
    FROM Slides AS s
    WHERE s.Published = 1
        AND (s.Page = @Page OR @Page IS NULL OR @Page = '')
        AND (s.Position = @Position OR @Position IS NULL OR @Position = '')
    FOR JSON PATH
    ) AS Items 

    
END
ELSE
BEGIN
    SET NOCOUNT ON;
    SELECT (ROW_NUMBER() OVER (ORDER BY s.Id DESC)) AS RowNumber, s.Id, s.Name, s.Page, s.Position INTO #Temp FROM Slides AS s
    WHERE s.Published = 1
        AND (s.Page = @Page OR @Page IS NULL OR @Page = '')
        AND (s.Position = @Position OR @Position IS NULL OR @Position = '')

    SELECT @TotalRecords = COUNT(*) FROM #Temp;

    SELECT @TotalPages = CEILING(CAST(@TotalRecords AS FLOAT) / @PageSize);

    SELECT @PageIndex AS PageIndex, @PageSize AS PageSize, @TotalRecords AS TotalRecords, @TotalPages AS TotalPages, (
        SELECT tp.Id, tp.Name, tp.Page, tp.Position, (
            SELECT st.Title, st.Image, st.URL FROM SlideItems AS st
            WHERE st.SlideId = tp.Id
            FOR JSON PATH
        ) AS SlideItems 
        FROM #Temp AS tp
        WHERE (tp.RowNumber BETWEEN (@PageIndex - 1) * @PageSize + 1 AND ((@PageIndex - 1) * @PageSize + 1) + @PageSize - 1) OR @PageIndex = -1
        FOR JSON PATH
    ) AS Items 

    DROP TABLE #Temp
END

END;
