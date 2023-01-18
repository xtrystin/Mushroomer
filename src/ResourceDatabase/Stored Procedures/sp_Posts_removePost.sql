CREATE PROCEDURE [dbo].[sp_Posts_removePost]
	@postID int = NULL
AS
	DELETE TOP(1) FROM Posts 
	WHERE ID_post = @postID;
RETURN 0
