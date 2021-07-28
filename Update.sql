USE [BookStore]
GO
/****** Object:  StoredProcedure [dbo].[UpdateBookRecord]    Script Date: 03-05-2021 09:48:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER     PROCEDURE [dbo].[UpdateBook]
	-- Add the parameters for the stored procedure here
	@BookID bigint,
	@BookQuantity int,
	@AuthorName nvarchar(50),
	@BookName NVARCHAR(50),
	@BookDescription nvarchar(max),
	@BookImage nvarchar(max),
	@BookPrice int
AS
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
SET XACT_ABORT on;
SET NOCOUNT ON;
BEGIN
BEGIN TRY
BEGIN TRANSACTION;

	DECLARE @Identity table (ID nvarchar(100));
	DECLARE @AuthorID bigint;
	DECLARE @result int = 0;
		
	if((select count(*) from Books where BookID = @BookID) = 0)
	begin
		set @result = 2; 
		throw 5000,'Book dont exist',-1;
	end
	set @AuthorID = (select AuthorID from Author where AuthorName = @AuthorName)
	if(IsNull(@AuthorID, 0) = 0)
	begin
		insert into Author output inserted.AuthorID into @Identity values(@AuthorName);
	    set @AuthorID = (select ID from @Identity);
	end

	update Books set BookName = @BookName, BookDescription = @BookDescription,
	BookQuantity = @BookQuantity,
	BookImage = @BookImage, BookPrice = @BookPrice, AuthorID = @AuthorID
	where BookID = @BookID;

	select BookID, BookName, BookDescription, BookImage, BookPrice, Books.AuthorID, InStock,
	BookQuantity, AuthorName
	from Books inner join Author on Books.AuthorID = Author.AuthorID where
	 BookID = @BookID;

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