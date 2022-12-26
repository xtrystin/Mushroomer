CREATE PROCEDURE [dbo].[sp_Posts_addPost]
    @title NVARCHAR(64) = 'noTitleSent',
    @contents NTEXT NULL,
    @date_created DATETIME2(3) = SYSDATETIME,
    @type CHAR(1) = 'n',
    @image IMAGE = NULL,
    @shop_link NVARCHAR(128) = NULL,
    @ID_author INT
AS
	INSERT Posts(title,contents,date_created,[type],[image],shop_link,ID_author, score)
        OUTPUT INSERTED.ID_post
    VALUES (@title, @contents, @date_created, @type, @image, @shop_link, @ID_author, 0)
RETURN
