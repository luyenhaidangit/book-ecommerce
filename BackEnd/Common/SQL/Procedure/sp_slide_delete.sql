USE [Thegioididong]
GO
/****** Object:  StoredProcedure [dbo].[sp_slide_delete]    Script Date: 01/03/2023 7:31:41 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_slide_delete]
(
    @id INT
)
AS
BEGIN
    -- Xóa các SlideItem trước
    DELETE FROM SlideItems WHERE SlideId = @id;
    -- Xóa Slide
    DELETE FROM Slides WHERE Id = @id;
END;