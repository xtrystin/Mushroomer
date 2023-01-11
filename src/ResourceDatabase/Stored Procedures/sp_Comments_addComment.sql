CREATE PROCEDURE [dbo].[sp_Comments_addComment]
    @content      NVARCHAR(2000)  = 'noContentSent',
    @date_created DATETIME2(7)    = SYSDATETIME,
    @ID_author    INT             = NULL,
    @ID_post      INT             = NULL
AS
	INSERT Comments(content,date_created,ID_author,ID_post, score)
		OUTPUT INSERTED.ID_comment
	VALUES(@content, @date_created, @ID_author, @ID_post, 0)
RETURN
