CREATE PROCEDURE [dbo].[sp_Warning_getFiltered]
	@minLat float = NULL,
	@maxLat float = NULL,
	@minLon float = NULL,
	@maxLon float = NULL,
	@onlyActive BIT = 1
AS
	IF (@onlyActive = 1)
		SELECT * FROM Warnings
		INNER JOIN Posts ON Warnings.ID_post=Posts.ID_post
		WHERE (latitude BETWEEN @minLat AND @maxLat) AND 
		(longitude BETWEEN @minLon AND @maxLon) AND 
		Warnings.is_active=1;
	ELSE
		SELECT * FROM Warnings
		INNER JOIN Posts ON Warnings.ID_post=Posts.ID_post 
		WHERE (latitude BETWEEN @minLat AND @maxLat) AND 
		(longitude BETWEEN @minLon AND @maxLon);
RETURN