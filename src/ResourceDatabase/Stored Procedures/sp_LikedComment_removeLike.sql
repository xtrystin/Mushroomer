CREATE PROCEDURE [dbo].[sp_LikedComment_removeLike]
	@ID_user INT = NULL,
    @ID_comment INT = NULL
AS
	DELETE TOP(1) FROM User_LikedComment
	WHERE (ID_comment = @ID_comment) AND (ID_user = @ID_user);

	IF (@@ROWCOUNT != 0)
		UPDATE TOP(1) comments
		SET score = score - 1
			OUTPUT INSERTED.score
		WHERE ID_comment = @ID_comment;
RETURN
