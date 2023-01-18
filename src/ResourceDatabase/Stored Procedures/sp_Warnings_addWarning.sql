CREATE PROCEDURE [dbo].[sp_Warnings_addWarning]
    @latitude     FLOAT(53)    = 0,
    @longitude     FLOAT(53)    = 0,
    @province      NVARCHAR(64) = NULL,
    @is_active     BINARY(1)    = 1,
    @duration_days INT           = NULL,
    @ID_post       INT           = NULL
AS
	INSERT Warnings(latitude,longitude,province,is_active,duration_days,ID_post)
    VALUES(@latitude, @longitude, @province, @is_active, @duration_days, @ID_post)
RETURN 0
