CREATE TABLE [dbo].[User_LikedPost] (
    [ID_user] INT NOT NULL,
    [ID_post] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_user] ASC, [ID_post] ASC),
    FOREIGN KEY ([ID_post]) REFERENCES [dbo].[Posts] ([ID_post]),
    FOREIGN KEY ([ID_user]) REFERENCES [dbo].[Users] ([ID_user])
);

