CREATE TABLE [dbo].[Posts]
(
	[ID_post] INT IDENTITY NOT NULL PRIMARY KEY, 
    [title] NVARCHAR(64) NOT NULL, 
    [contents] NTEXT NULL, 
    [score] INT NOT NULL DEFAULT 0, 
    [date_created] DATETIME2(3) NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    [type] CHAR(1) NULL DEFAULT 'n',
    [image] IMAGE NULL, 
    [shop_link] NVARCHAR(128) NULL, 
    [ID_author] INT NOT NULL REFERENCES Users(ID_user),
    INDEX Posts_date_created_idx (date_created DESC),
    INDEX Posts_title_idx (title ASC)
)
