--Create Orders table
create table Orders(
	OrderId int identity (1,1) primary key,
	OrderDate Date not null,
	BooksQty int not null,
	OrderPrice float not null,
	ActualPrice float not null,
	BookId int not null foreign key (BookId) references Books(BookId),
	UserId int not null foreign key (UserId) references Users(UserId),
	AddressId int not null foreign key (AddressId) references Address(AddressId)
	)


--Stored Procedures for Orders

-----Add Orders-----
alter procedure spAddOrders
(
	@BookId int,
	@UserId int,
	@AddressId int
)
as
	declare @OrderPrice float;
	declare @ActualPrice float;
	declare @BookQuantity int;
BEGIN TRY
		BEGIN TRY
		IF (EXISTS(SELECT * FROM Books WHERE BookId = @BookId))
		begin
			IF (EXISTS(SELECT * FROM Address WHERE AddressId = @AddressId))
			begin
				BEGIN TRANSACTION
					SELECT @BookQuantity = BooksQty FROM Cart Where BookId = @BookId AND UserId = @UserId;	
					set @OrderPrice = (select DiscountPrice FROM Books WHERE BookId = @BookId);
					set @ActualPrice = (select ActualPrice FROM Books WHERE BookId = @BookId);
				
					if((select Quantity from Books where BookId=@BookId)>= @BookQuantity)
					BEGIN
						insert into Orders
						values(getdate(), @BookQuantity, @OrderPrice * @BookQuantity, @ActualPrice * @BookQuantity, @BookId, @UserId, @AddressId);

						update Books set Quantity = Quantity-@BookQuantity where BookId = @BookId;

						delete from Cart where BookId=@BookId and UserId=@UserId;
					END
					else
						begin
							select 2
						end

				COMMIT TRANSACTION
			end
			else
				begin
					select 3
				end
		end
		else
			begin
				select 4
			end
		
END TRY

		BEGIN CATCH
			ROLLBACK TRANSACTION
		END CATCH
	
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




---Get All Orders---

create procedure spGetAllOrders
(
	@UserId int
)
as
BEGIN TRY
	SELECT	Books.BookName,
			Books.BookImage,
			Books.Author, 
		    Orders.ActualPrice,
			Orders.OrderPrice,
			Orders.OrderDate,
			Orders.BooksQty,
			Orders.BookId,
			Orders.OrderId,
			Orders.UserId,
			Orders.AddressId
	FROM Orders 
	INNER JOIN Books
	ON Orders.BookId = Books.BookId
	where Orders.UserId = @UserId

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
