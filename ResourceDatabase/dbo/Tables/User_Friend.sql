CREATE TABLE [dbo].[User_Friend]
(
	[ID_user] INT NOT NULL REFERENCES Users(ID_user), 
    [ID_friend] INT NOT NULL REFERENCES Users(ID_user), 
    [date_added] DATETIME2(3) NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    PRIMARY KEY ([ID_user],[ID_friend])
)