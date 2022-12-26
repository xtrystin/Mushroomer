CREATE TABLE [dbo].[Users] (
    [ID_user]     INT            IDENTITY (1, 1) NOT NULL,
    [username]    NVARCHAR (64)  NOT NULL,
    [email]       NVARCHAR (128) NOT NULL,
    [type]        CHAR (1)       DEFAULT 'n' NOT NULL,
    [description] NTEXT          DEFAULT '' NOT NULL,
    PRIMARY KEY CLUSTERED ([ID_user] ASC),
    INDEX [Users_username_idx] NONCLUSTERED ([username])
);

