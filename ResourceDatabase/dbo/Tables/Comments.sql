CREATE TABLE [dbo].[Comments]
(
	[ID_comment] INT IDENTITY NOT NULL PRIMARY KEY, 
    [content] NVARCHAR(2000) NOT NULL, 
    [date_created] DATETIME2 NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [score] INT NOT NULL, 
    [ID_author] INT NOT NULL REFERENCES Users(ID_user), 
    [ID_post] INT NULL REFERENCES Posts(ID_post),
    INDEX Comments_date_created_idx (date_created DESC)
)
