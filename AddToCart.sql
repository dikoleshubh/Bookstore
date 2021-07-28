--use BookStore

--create table Cart(
--CartID int identity(1,1) NOT NULL,
--TotalCost bigint NOT NULL,
--BookCount bigint Not null,
--BookPrice bigint Not null,
--ID int NOT NULL,




--)
--create procedure AddToCart
--as
--select * from dbo.Cart
--go

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
ALTER   PROCEDURE [dbo].[AddToCart]
	-- Add the parameters for the stored procedure here
	@ID bigint,

	@BookID bigint,

	@result bigint  OUT
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
--	DECLARE @result int = 0;
	DECLARE @TotalCost int = 0;

	if((select count(BookID) from Books where BookID = @BookID ) = 1)
	begin
		if((select InStock from Books where BookID = @BookID) = 1)
		begin;
			set @result = 1;
			set @TotalCost = (select BookPrice from Books where BookID = @BookID); 
			
			if((select count(*) from Cart where
			BookID = @BookID and ID = @ID) = 0)
			begin
			
				insert into Cart(ID, BookID, TotalCost) 
				values(@ID, @BookID, @TotalCost);
			end
			else
				update Cart set BookCount = BookCount +1,
				TotalCost = TotalCost + @TotalCost
				where BookID = @BookID and ID = @ID;
				print @TotalCost
		end
		else
		begin
			set @result = 3;
			throw 50003,'Book out of stock',-1;
		end
	end
	else
	begin
		set @result = 2;
		throw 50003,'Book dont exist',-1;
	end

	select Cart.ID, Cart.CartID, Cart.BookID, Books.BookPrice, Books.BookName,
	BookCount, TotalCost from 
	Cart inner join Books on Cart.BookID = Books.BookID
	where Cart.ID = @ID;


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