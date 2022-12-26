CREATE PROCEDURE [dbo].[sp_Comments_getFromPostID]
	@postID int = 0
AS
	SELECT [ID_comment], [content], [date_created], [score], [ID_author], [ID_post], [Users].[username], [Users].[type] FROM Comments INNER JOIN Users ON Comments.ID_author=Users.ID_user WHERE Comments.ID_post = @postID ORDER BY Comments.date_created DESC
RETURN