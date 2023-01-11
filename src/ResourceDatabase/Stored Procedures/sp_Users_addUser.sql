CREATE PROCEDURE [dbo].[sp_Users_addUser]
    @username NVARCHAR(64), 
    @email NVARCHAR(128), 
    @type CHAR = 'n', 
    @description NTEXT = ''
AS
	INSERT Users(username,email,type,description)
        OUTPUT INSERTED.ID_user
    VALUES (@username, @email, @type, @description)
RETURN