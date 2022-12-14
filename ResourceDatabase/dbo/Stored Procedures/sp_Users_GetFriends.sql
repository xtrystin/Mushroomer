CREATE PROCEDURE [dbo].[sp_Users_getFriends]
	@userID int = 0
AS	
	SELECT User_Friend.ID_friend from User_Friend WHERE ID_user = @userID
RETURN
