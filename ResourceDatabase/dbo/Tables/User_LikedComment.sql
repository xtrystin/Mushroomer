CREATE TABLE [dbo].[User_LikedComment]
(
	[ID_user] INT NOT NULL REFERENCES Users(ID_user), 
    [ID_comment] INT NOT NULL REFERENCES Comments(ID_comment), 
    PRIMARY KEY ([ID_user],[ID_comment])
)
