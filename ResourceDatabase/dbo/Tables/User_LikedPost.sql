CREATE TABLE [dbo].[User_LikedPost]
(
	[ID_user] INT NOT NULL REFERENCES Users(ID_user), 
    [ID_post] INT NOT NULL REFERENCES Posts(ID_post), 
    PRIMARY KEY ([ID_user],[ID_post])
)
