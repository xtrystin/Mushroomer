CREATE TABLE [dbo].[User_Friend] (
    [ID_user]    INT           NOT NULL,
    [ID_friend]  INT           NOT NULL,
    [date_added] DATETIME2 (3) DEFAULT CURRENT_TIMESTAMP NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_user] ASC, [ID_friend] ASC),
    FOREIGN KEY ([ID_friend]) REFERENCES [dbo].[Users] ([ID_user]),
    FOREIGN KEY ([ID_user]) REFERENCES [dbo].[Users] ([ID_user])
);

