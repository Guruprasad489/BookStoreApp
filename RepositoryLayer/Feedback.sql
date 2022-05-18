--Create Feedback table
create table Feedback(
	FeedbackId int identity (1,1) primary key,
	Rating int not null,
	Comment varchar(max) not null,
	BookId int not null foreign key (BookId) references Books(BookId),
	UserId int not null foreign key (UserId) references Users(UserId)
	)


--Stored Procedures for Feedback

-----Add Feedback-----
create procedure spAddFeedback
(
    @Rating int,
	@Comment varchar(max),
	@BookId int,
	@UserId int
)
as
	declare @AvgRating int;
BEGIN TRY
    
    IF (EXISTS(SELECT * FROM Feedback WHERE BookId = @BookId and UserId=@UserId))
		select 1;
	else
		BEGIN TRY
			BEGIN TRANSACTION
				insert into Feedback
				values(@Rating, @Comment, @BookId, @UserId);

				set @AvgRating = (select AVG(Rating) from Feedback where BookId = @BookId);

				update Books set Rating = @AvgRating, RatingCount = (RatingCount+1) where BookId = @BookId;
			COMMIT TRANSACTION
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




---Get All Feedback---

create procedure spGetAllFeedback
(
	@BookId int
)
as
BEGIN TRY
	SELECT Feedback.FeedbackId,
		   Feedback.UserId,
		   Feedback.BookId,
		   Feedback.Comment,
		   Feedback.Rating, 
		   Users.FullName 
	FROM Users 
	INNER JOIN Feedback 
	ON Feedback.UserId = Users.UserId WHERE BookId=@BookId

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
