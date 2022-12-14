CREATE TABLE [dbo].[Users]
(
	[ID_user] INT IDENTITY NOT NULL PRIMARY KEY, 
    [username] NVARCHAR(64) NOT NULL, 
    [email] NVARCHAR(128) NOT NULL, 
    [type] CHAR NOT NULL DEFAULT 'n', 
    [description] NTEXT NOT NULL DEFAULT '',
    INDEX Users_username_idx (username ASC)
)
