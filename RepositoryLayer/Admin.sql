--Create Admin table
create table Admin(
	AdminId int identity (1,1) primary key,
	FullName varchar(200) not null,
	EmailId varchar(200) not null,
	Password varchar(200) not null,
	MobileNumber varchar(200) not null,
	Address varchar(max) not null
	)

INSERT INTO Admin VALUES ('Admin','admin@bookstore.com', 'Admin@12345', '+91 8163475881', '42, 14th Main, 15th Cross, Sector 4 ,opp to BDA complex, near Kumarakom restaurant, HSR Layout, Bangalore 560034');

--Stored Procedures for Admin

-----Login-----
create procedure spAdminLogin
(
	@EmailId varchar(200),
    @Password varchar(200)
)
as
BEGIN TRY
	select * from Admin where EmailId = @EmailId and Password = @Password;
END TRY
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
END CATCH
