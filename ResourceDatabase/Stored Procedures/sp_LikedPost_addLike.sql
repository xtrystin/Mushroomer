CREATE PROCEDURE [dbo].[sp_LikedPost_addLike]
	@ID_user INT = NULL,
    @ID_post INT = NULL
AS
	INSERT User_LikedPost(ID_user,ID_post)
	VALUES(@ID_user, @ID_post);

	UPDATE TOP(1) Posts
	SET score = score + 1
	OUTPUT INSERTED.score
	WHERE ID_post = @ID_post;
RETURN
