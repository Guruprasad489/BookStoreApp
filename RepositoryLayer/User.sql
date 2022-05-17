--Create BookStore Database
create database BookStore;

--Show databases
EXEC sp_databases;

--Switch(To Use) to database
use BookStore;

--Create Users table 
create table Users(
	UserId int identity (1,1) primary key,
	FullName varchar(200) not null,
	EmailId varchar(200) not null unique,
	Password varchar(200) not null,
	MobileNumber bigint not null
	)


--Stored Procedures for users

-----Registration-----
create procedure spUserRegistration
(
    @FullName varchar(200),
	@EmailId varchar(200),
    @Password varchar(200),
    @MobileNumber bigint
)
as
BEGIN TRY
	insert into Users
	values(@FullName, @EmailId, @Password, @MobileNumber)
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

-- exec spUserRegistration;


-----Login-----
create procedure spUserLogin
(
	@EmailId varchar(200),
    @Password varchar(200)
)
as
BEGIN TRY
	select * from Users where EmailId = @EmailId and Password = @Password;
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



---Forgot password-----

create procedure spUserForgotPassword
(
	@EmailId varchar(200)
)
as
BEGIN TRY
	select * from Users where EmailId = @EmailId;
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


-- Reset Password---

create procedure spUserResetPassword
(
	@EmailId varchar(200),
	@Password varchar(200)
)
as
BEGIN TRY
	update Users 
	set Password = @Password where EmailId = @EmailId;
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
