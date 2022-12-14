CREATE TABLE [dbo].[Warnings]
(
	[ID_warning] INT IDENTITY NOT NULL PRIMARY KEY,
	[latitude] FLOAT NOT NULL,
	[longitude] FLOAT NOT NULL,
	[province] NVARCHAR(64) NULL,
	[is_active] BINARY(1) NOT NULL,
	[duration_days] INT NULL,
	[ID_post] int NOT NULL REFERENCES Posts(ID_post),
	INDEX Warnings_latitude_longitude_idx (latitude ASC, longitude ASC)
)
