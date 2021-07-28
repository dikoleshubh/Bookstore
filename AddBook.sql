--use BookStore
----CREATE TABLE Books (
--  --  BookID int identity(1,1) NOT NULL,
--    --BookName varchar(255),
--    --BookDescription varchar(255),
--    --BookImage varchar(255),
  
-- --   AuthorName varchar(255),
--	AuthorID int NOT NULL,
--	BookQuantity int NOT NULL,
--	BookPrice int NOT NULL,
   
--	 InStock  varchar(255),

--);--

--create procedure AddBook
--as
--select * from dbo.Books
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
ALTER   PROCEDURE [dbo].[AddBook]
	-- Add the parameters for the stored procedure here

	@AuthorName nvarchar(50),
	@BookName NVARCHAR(50),
	@BookDescription nvarchar(max),
	@BookImage nvarchar(max),
	@BookPrice int,
	@BookQuantity int
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @Identity table (ID nvarchar(100));
	DECLARE @BookIdentity table (ID bigint);
	DECLARE @AuthorID bigint;
	DECLARE @result int = 0;

	set @AuthorID = (select AuthorID from Author where AuthorName = @AuthorName)
	if(IsNull(@AuthorID, 0) = 0)
	begin
		insert into Author output inserted.AuthorID into @Identity values(@AuthorName);
	    set @AuthorID = (select ID from @Identity);
	end

	insert into Books(BookName, BookDescription, BookImage, BookPrice, AuthorID, BookQuantity)
	output inserted.BookID into @BookIdentity
	values(@BookName, @BookDescription, @BookImage, @BookPrice, @AuthorID, @BookQuantity);

	
	
	select BookID, BookName, BookDescription, BookImage, BookPrice, Books.AuthorID, InStock,
	BookQuantity, AuthorName
	from Books inner join Author on Books.AuthorID = Author.AuthorID where
	 BookID = (select ID from @BookIdentity);
	
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


