USE [Thegioididong]
GO
/****** Object:  StoredProcedure [dbo].[sp_user_register]    Script Date: 02/03/2023 10:10:07 SA ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[sp_user_register]
(
@request NVARCHAR(MAX)
)
AS
    BEGIN

    DECLARE @Name NVARCHAR(160);
    DECLARE @Birthday NVARCHAR(160);
    DECLARE @Sex NVARCHAR(160);
	DECLARE @Andress NVARCHAR(4000);
	DECLARE @NumberPhone VARCHAR(800);
	DECLARE @Email VARCHAR(800);

	DECLARE @Username NVARCHAR(160);
    DECLARE @Password VARCHAR(800);
	DECLARE @Image NVARCHAR(4000);
    DECLARE @Role VARCHAR(80);
    
    -- Lấy giá trị các trường từ request
SELECT 
    @Name = JSON_VALUE(@request, '$.user.name'),
    @Birthday = JSON_VALUE(@request, '$.user.birthDay'),
    @Sex = JSON_VALUE(@request, '$.user.sex'),
    @Andress = JSON_VALUE(@request, '$.user.andress'),
	@NumberPhone = JSON_VALUE(@request, '$.user.numberPhone'),
	@Email = JSON_VALUE(@request, '$.user.email'),
	@Username = JSON_VALUE(@request, '$.account.username'),
	@Password = JSON_VALUE(@request, '$.account.password'),
	@Image = JSON_VALUE(@request, '$.account.image'),
	@Role = JSON_VALUE(@request, '$.account.role');


	INSERT INTO Users(Name,Birthday,Sex,Andress,NumberPhone,Email) VALUES (@Name, @Birthday, @Sex, @Andress,@NumberPhone,@Email);
	DECLARE @userId INT = SCOPE_IDENTITY();

	IF(@userId IS NOT NULL)
	 BEGIN
	   INSERT INTO Accounts
                (
				 UserId,
				 Username,
				 Password,
				 Image,
				 JoinDate,
				 EndDate,
				 Role
                )
		SELECT 
				@userId,	
				@Username,
				@Password,
				@Image,
				GETDATE(),
				NULL,
				@Role;
END
END