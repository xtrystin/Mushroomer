CREATE PROCEDURE [dbo].[sp_Friend_addFriend]
	@ID_user    INT = NULL,
    @ID_friend INT = NULL
AS
	INSERT User_Friend(ID_user,ID_friend, date_added)
	VALUES(@ID_user, @ID_friend, CURRENT_TIMESTAMP);
RETURN
