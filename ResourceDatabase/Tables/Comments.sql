CREATE TABLE [dbo].[Comments] (
    [ID_comment]   INT             IDENTITY (1, 1) NOT NULL,
    [content]      NVARCHAR (2000) NOT NULL,
    [date_created] DATETIME2 (7)   DEFAULT CURRENT_TIMESTAMP NOT NULL,
    [score]        INT             NOT NULL,
    [ID_author]    INT             NOT NULL,
    [ID_post]      INT             NULL,
    PRIMARY KEY CLUSTERED ([ID_comment] ASC),
    FOREIGN KEY ([ID_author]) REFERENCES [dbo].[Users] ([ID_user]),
    FOREIGN KEY ([ID_post]) REFERENCES [dbo].[Posts] ([ID_post]),
    INDEX [Comments_date_created_idx] NONCLUSTERED ([date_created] DESC)
);

