--Create Cart table
create table Cart(
	CartId int identity (1,1) primary key,
	BooksQty int default 1,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)


--Stored Procedures for Cart

-----Add to Cart-----
create procedure spAddToCart
(
    @BooksQty int,
	@UserId int,
	@BookId int
)
as
BEGIN TRY
	insert into Cart
	values(@BooksQty, @UserId, @BookId);
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



---Remove from Cart---

create procedure spRemoveFromCart
(
	@CartId int
)
as
BEGIN TRY
	delete from Cart where CartId = @CartId;
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



---Get all Cart Items---

alter procedure spGetAllCart
(
	@UserId int
)
as
BEGIN TRY
	select 
		c.CartId,
		c.BookId,
		c.UserId,
		c.BooksQty,
		b.BookName,
		b.BookImage,
		b.Author,
		b.DiscountPrice,
		b.ActualPrice		
	from Cart c
	inner join Books b
	on c.BookId = b.BookId
	where c.UserId = @UserId;
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


---Update Quantity---

create procedure spUpdateQtyInCart
(
	@CartId int,
	@BooksQty int
)
as
BEGIN TRY
	update Cart set BooksQty = @BooksQty where CartId = @CartId;
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



