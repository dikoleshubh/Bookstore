GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ==========
ALTER PROCEDURE [dbo].[RegisterAdmin]
	
	@AdminEmail		varchar(100),
	@AdminPassword		varchar(50)
AS
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN

	SET NOCOUNT ON;
	DECLARE @new_identity INTEGER = 0;
	declare @Identity table (AdminID int)
	DECLARE @result bit = 0;
    
	
	Insert into Admin(AdminEmail,AdminPassword) output Inserted.AdminID into @Identity
	VALUES( @AdminEmail, @AdminPassword);
	SELECT @new_identity = (select AdminID from @Identity);

	
	end