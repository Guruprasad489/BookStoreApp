--Create Books table
create table Books(
	BookId int identity (1,1) primary key,
	BookName varchar(200) not null,
	Author varchar(200) not null,
	BookImage varchar(max) not null,
	BookDetail varchar(max) not null,
	DiscountPrice float not null,
	ActualPrice float not null,
	Quantity int not null,
	Rating float,
	RatingCount int
	)


	--Stored Procedures for Books

-----Add Book-----
create procedure spAddBook
(
    @BookName varchar(200),
	@Author varchar(200),
	@BookImage varchar(200),
	@BookDetail varchar(max),
	@DiscountPrice float,
	@ActualPrice float,
	@Quantity int,
	@Rating float,
	@RatingCount int,
	@BookId int output
)
as
BEGIN TRY
	insert into Books
	values(@BookName, @Author, @BookImage, @BookDetail, @DiscountPrice, @ActualPrice, @Quantity, @Rating, @RatingCount);
	set @BookId = SCOPE_IDENTITY()
	return @BookId;
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


---Update Book---

create procedure spUpdateBook
(
	@BookId int,
	@BookName varchar(200),
	@Author varchar(200),
	@BookImage varchar(200),
	@BookDetail varchar(max),
	@DiscountPrice float,
	@ActualPrice float,
	@Quantity int,
	@Rating float,
	@RatingCount int
)
as
BEGIN TRY
	update Books 
	set BookName = @BookName, Author = @Author, BookImage = @BookImage, BookDetail = @BookDetail, DiscountPrice = @DiscountPrice, ActualPrice = @ActualPrice, Quantity = @Quantity, Rating = @Rating, RatingCount = @RatingCount where BookId = @BookId;
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


---Delete Book---

create procedure spDeleteBook
(
	@BookId int
)
as
BEGIN TRY
	delete from Books where BookId = @BookId;
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



---Get all Books---

create procedure spGetAllBooks
as
BEGIN TRY
	select * from Books;
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


---get Book by BookId---

create procedure spGetBookById
(
	@BookId int
)
as
BEGIN TRY
	select * from Books where BookId = @BookId;
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



