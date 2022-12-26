CREATE TABLE [dbo].[User_LikedComment] (
    [ID_user]    INT NOT NULL,
    [ID_comment] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_user] ASC, [ID_comment] ASC),
    FOREIGN KEY ([ID_comment]) REFERENCES [dbo].[Comments] ([ID_comment]),
    FOREIGN KEY ([ID_user]) REFERENCES [dbo].[Users] ([ID_user])
);

