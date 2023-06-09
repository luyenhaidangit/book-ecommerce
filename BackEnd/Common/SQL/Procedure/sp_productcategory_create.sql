USE [Thegioididong]
GO
/****** Object:  StoredProcedure [dbo].[sp_productcategory_create]    Script Date: 02/03/2023 12:38:22 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_productcategory_create]
(
    @request NVARCHAR(MAX)
)
AS
BEGIN
    DECLARE @ParentProductCategoryId INT;
	DECLARE @ProductCategoryGroupId INT;
    DECLARE @Name NVARCHAR(80);
    DECLARE @BadgeIcon VARCHAR(800);
	DECLARE @Image VARCHAR(800);
    DECLARE @DisplayOrder INT;
    DECLARE @Published BIT;
    
    
    -- Lấy giá trị các trường từ request
SELECT 
    @ParentProductCategoryId = JSON_VALUE(@request, '$.parentProductCategoryId'),
    @ProductCategoryGroupId = JSON_VALUE(@request, '$.productCategoryGroupId'),
    @Name = JSON_VALUE(@request, '$.name'),
    @BadgeIcon = JSON_VALUE(@request, '$.badgeIcon'),
    @Image = JSON_VALUE(@request, '$.image'),
	@DisplayOrder = JSON_VALUE(@request, '$.displayOrder'),
	@Published = JSON_VALUE(@request, '$.published');

	INSERT INTO ProductCategories (ParentProductCategoryId, ProductCategoryGroupId, Name, BadgeIcon,Image, DisplayOrder, Published)
	VALUES (@ParentProductCategoryId,@ProductCategoryGroupId,@Name,@BadgeIcon,@Image,@DisplayOrder,@Published);

END;
