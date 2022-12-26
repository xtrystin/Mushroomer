CREATE PROCEDURE [dbo].[sp_LikedComment_addLike]
	@ID_user    INT = NULL,
    @ID_comment INT = NULL
AS
	INSERT User_LikedComment(ID_user,ID_comment)
	VALUES(@ID_user, @ID_comment);

	UPDATE TOP(1) Comments
	SET score = score + 1
		OUTPUT INSERTED.score
	WHERE ID_comment = @ID_comment;
RETURN
