CREATE PROCEDURE [dbo].[sp_Posts_getFromUserID]
	@userID int
AS
	SELECT Posts.ID_post FROM Posts	WHERE Posts.ID_author = @userID ORDER BY Posts.date_created DESC;
RETURN;
