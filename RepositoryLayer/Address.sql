--Create Address table
create table Address(
	AddressId int identity (1,1) primary key,
	Address varchar(200) not null,
	City varchar(200) not null,
	State varchar(200) not null,
	TypeId int not null foreign key (TypeId) references AddressType(TypeId),
	UserId int not null foreign key (UserId) references Users(UserId)
	)


-- Address type table--
create table AddressType(
	TypeId int identity (1,1) primary key,
	AddressType varchar(200)
	)

insert into AddressType values ('Home');
insert into AddressType values ('Work');
insert into AddressType values ('Others');



--Stored Procedures for Address

-----Add Address-----
create procedure spAddAddress
(
    @Address varchar(200),
	@City varchar(200),
	@State varchar(200),
	@TypeId int,
	@UserId int
)
as
BEGIN TRY
	insert into Address
	values(@Address, @City, @State, @TypeId, @UserId);
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



---Delete Address---

alter procedure spDeleteAddress
(
	@AddressId int,
	@UserId int
)
as
BEGIN TRY
	delete from Address where AddressId = @AddressId and UserId = @UserId;
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


---Update Address---

create procedure spUpdateAddress
(
	@AddressId int,
	@Address varchar(200),
	@City varchar(200),
	@State varchar(200),
	@TypeId int,
	@UserId int
)
as
BEGIN TRY
If EXISTS(SELECT * FROM AddressType WHERE TypeId=@TypeId)
	BEGIN
	update Address set Address = @Address, City = @City, State = @State, TypeId = @TypeId  
		   where AddressId = @AddressId and UserId = @UserId;
	END
	Else
		BEGIN
			SELECT 2
		END

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


---Get Address---

alter procedure spGetAddress
(
	@UserId int,
	@AddressId int
)
as
BEGIN TRY
	select * from Address where UserId = @UserId and AddressId = @AddressId;
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


---Get All Address---

create procedure spGetAllAddress
(
	@UserId int
)
as
BEGIN TRY
	select * from Address where UserId = @UserId;
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
