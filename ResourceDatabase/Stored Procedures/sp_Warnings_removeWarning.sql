CREATE PROCEDURE [dbo].[sp_Users_removeWarning]
	@warningID int = NULL
AS
	DELETE TOP(1) FROM Warnings 
	WHERE Warnings.ID_warning = @warningID;
RETURN 0
