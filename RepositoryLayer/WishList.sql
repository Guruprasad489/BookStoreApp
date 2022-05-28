--Create WishList table
create table WishList(
	WishListId int identity (1,1) primary key,
	UserId int not null foreign key (UserId) references Users(UserId),
	BookId int not null foreign key (BookId) references Books(BookId)
	)


--Stored Procedures for WishList

-----Add to WishList-----
alter procedure spAddToWishList
(
	@UserId int,
	@BookId int
)
as
BEGIN TRY
	IF (NOT EXISTS(SELECT * FROM WishList WHERE BookId = @BookId and UserId = @UserId))
		begin
			insert into WishList
			values( @UserId, @BookId);
		end
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



---Remove from WishList---

create procedure spRemoveFromWishList
(
	@WishListId int
)
as
BEGIN TRY
	delete from WishList where WishListId = @WishListId;
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



---Get all WishList Items---

create procedure spGetAllWishList
(
	@UserId int
)
as
BEGIN TRY
	select 
		c.WishListId,
		c.BookId,
		c.UserId,
		b.BookName,
		b.BookImage,
		b.Author,
		b.DiscountPrice,
		b.ActualPrice		
	from WishList c
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




