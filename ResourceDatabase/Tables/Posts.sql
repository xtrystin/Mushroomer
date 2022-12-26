CREATE TABLE [dbo].[Posts] (
    [ID_post]      INT            IDENTITY (1, 1) NOT NULL,
    [title]        NVARCHAR (64)  NOT NULL,
    [contents]     NTEXT          NULL,
    [score]        INT            DEFAULT 0 NOT NULL,
    [date_created] DATETIME2 (3)  DEFAULT CURRENT_TIMESTAMP NOT NULL,
    [type]         CHAR (1)       DEFAULT 'n' NULL,
    [image]        IMAGE          NULL,
    [shop_link]    NVARCHAR (128) NULL,
    [ID_author]    INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_post] ASC),
    FOREIGN KEY ([ID_author]) REFERENCES [dbo].[Users] ([ID_user]),
    INDEX [Posts_date_created_idx] NONCLUSTERED ([date_created] DESC),
    INDEX [Posts_title_idx] NONCLUSTERED ([title])
);

