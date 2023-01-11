CREATE PROCEDURE [dbo].[sp_LikedPost_removeLike]
	@ID_user INT = NULL,
    @ID_post INT = NULL
AS
	DELETE TOP(1) FROM User_LikedPost
	WHERE (ID_post = @ID_post) AND (ID_user = @ID_user);

	IF(@@ROWCOUNT != 0)
		UPDATE TOP(1) Posts
		SET score = score - 1
			OUTPUT INSERTED.score
		WHERE ID_post = @ID_post;
RETURN
