  
USE [BookStore]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER   PROCEDURE [dbo].[ForgotPass]
	-- Add the parameters for the stored procedure here
	@Email nvarchar(250)


AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		DECLARE @result int = 0;

    -- Insert statements for procedure here
	if((select count(UserEmail) from Customer where UserEmail = @Email) = 1)
	   
		begin;
		set @result = 1;
		end
		select UserEmail from Customer where UserEmail = @Email
		
return @result;

END