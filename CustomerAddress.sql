
--create table AddressBook(


--    AddressID int identity (1,1) NOT NULL,
--    ID bigint NOT NULL,
--	Name nvarchar(50),
--	Pincode int NOT NULL,
	
--	PhoneNumber bigint,
--	Address nvarchar(100),
--	City nvarchar(50),

--	AddressType nvarchar(50)












--)

--select * from dbo.AddressBook

--create procedure InsertAddress
--as
--select * from dbo.AddressBook
--go;

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
ALTER     PROCEDURE [dbo].[InsertAddress]
	-- Add the parameters for the stored procedure here
	@ID bigint,
	@Name nvarchar(50),
	@Pincode int,
	
	@PhoneNumber bigint,
	@Address nvarchar(100),
	@City nvarchar(50),
	
	@AddressType nvarchar(50)
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @new_identity table(ID int);
	DECLARE @result int = 0;

	insert into AddressBook(ID,
	Name,Pincode,PhoneNumber,Address,City
	, AddressType) output inserted.AddressID into @new_identity
	values(@ID,
	@Name,
	@Pincode,
	@PhoneNumber,
	
	@Address,
	@City,

	@AddressType);

	select * from AddressBook where AddressID = (select ID from @new_identity);

	set @result = 1;
COMMIT TRANSACTION;	
return @result;
END TRY
BEGIN CATCH
--SELECT ERROR_NUMBER() as ErrorNumber, ERROR_MESSAGE() as ErrorMessage;
IF(XACT_STATE()) = -1
	BEGIN
		PRINT
		'transaction is uncommitable' + ' rolling back transaction'
		ROLLBACK TRANSACTION;
		print @result;
		return @result;
	END;
ELSE IF(XACT_STATE()) = 1
	BEGIN
		PRINT
		'transaction is commitable' + ' commiting back transaction'
		COMMIT TRANSACTION;
		print @result;
		return @result;
	END;
END CATCH
	
END