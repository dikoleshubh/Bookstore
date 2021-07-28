use BookStore

/*CREATE TABLE Admin (
    AdminID int identity(1,1)  NOT NULL, 
    AdminEmail varchar(255),
    AdminPassword varchar(255),
    
);*/
CREATE PROCEDURE RegisterAdmin
AS
SELECT * FROM dbo.Admin
GO