CREATE PROCEDURE [dbo].[sp_Friend_removeFriend]
	@ID_user    INT = NULL,
    @ID_friend INT = NULL
AS
	DELETE TOP(1) User_Friend 
	WHERE (ID_user = @ID_user) AND (ID_friend = @ID_friend);
RETURN
