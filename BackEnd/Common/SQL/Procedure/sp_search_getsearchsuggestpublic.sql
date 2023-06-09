USE [Thegioididong]
GO
/****** Object:  StoredProcedure [dbo].[sp_search_getsearchsuggestpublic]    Script Date: 08/03/2023 4:21:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_search_getsearchsuggestpublic]
@keyword NVARCHAR(MAX)
AS
BEGIN
	IF @keyword IS NULL OR REPLACE(LOWER(@keyword), ' ', '') = ''
		BEGIN
			SELECT NULL AS Products, NULL AS ProductCategories, NULL AS News FOR JSON PATH;
		END
	ELSE
	BEGIN
		SELECT
		(
			SELECT pv.Id AS Id, pv.Name AS Name, p.Image AS Image, pvp.Price AS OriginalPrice, 
			CASE WHEN pvdp.DiscountPercent IS NULL THEN pvp.Price ELSE CAST(ROUND(pvp.Price * (100 - pvdp.DiscountPercent) / 100.0, -4) AS INT) END AS SalePrice, 
			pvdp.DiscountPercent AS DiscountPercentage FROM Products AS p 
			JOIN ProductVariants AS pv ON p.Id = pv.ProductId
			JOIN ProductVariantPrices AS pvp ON pv.Id = pvp.ProductVariantId
			LEFT JOIN ProductVariantDiscountPrice AS pvdp ON pv.Id = pvdp.ProductVariantId
			WHERE REPLACE(LOWER(pv.Name), ' ', '') LIKE '%' + REPLACE(LOWER(@keyword), ' ', '') + '%'
			AND pv.Id IN
				(
					SELECT TOP 1 Id FROM ProductVariants
					WHERE Name = pv.Name
				)
				FOR JSON PATH
			) AS Products,
			(
				SELECT pc.Id, pc.Name FROM ProductCategories AS pc
				WHERE REPLACE(LOWER(pc.Name), ' ', '') LIKE '%' + REPLACE(LOWER(@keyword), ' ', '') + '%'
				ORDER BY pc.DisplayOrder
				FOR JSON PATH
			) AS ProductCategories,
			(
				SELECT n.Id, n.Name, n.Image FROM News AS n
				WHERE REPLACE(LOWER(n.Name), ' ', '') LIKE '%' + REPLACE(LOWER(@keyword), ' ', '') + '%'
				ORDER BY n.Id
				FOR JSON PATH
			) AS News
	END
END;