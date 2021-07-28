


create procedure sp_LoginDetails
(
@Email varchar(255),
@Password varchar(255)


)
AS
BEGIN
   DECLARE @IsLoginCorrect BIT
   SET @IsLoginCorrect=0
   IF EXISTS (SELECT * FROM DBO.Customer WHERE UserEmail=@Email and UserPassword=@Password)
   Begin 
   set @IsLoginCorrect=1
   End

   SELECT @IsLoginCorrect AS '@IsLoginCorrect'



END
