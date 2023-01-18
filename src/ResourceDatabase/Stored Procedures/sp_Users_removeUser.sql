CREATE PROCEDURE [dbo].[sp_Users_removeUser]
	@userID int = NULL
AS
	DELETE TOP(1) FROM Users WHERE Users.ID_user = @userID;
RETURN 0
