use DvdLibraryApp
go

create procedure GetAll
as

select *
from DVDs
go

create procedure GetOne (@DvdId int)
as

select *
from DVDs
where DVDs.DvdId = @DvdId
go

create procedure [Create] (@Title nvarchar(100), @RealeaseYear int, @Director nvarchar(50), @Rating nvarchar(10), @Notes nvarchar(max))
as

insert into DVDs (Title, RealeaseYear, Director, Rating, Notes) values
(@Title, @RealeaseYear, @Director, @Rating, @Notes)
go

create procedure Edit (@DvdId int, @Title nvarchar(100), @RealeaseYear int, @Director nvarchar(50), @Rating nvarchar(10), @Notes nvarchar(max))
as

update DVDs set
Title = @Title,
RealeaseYear = @RealeaseYear,
Director = @Director,
Rating = @Rating,
Notes = @Notes
where DvdId = @DvdId;
go

create procedure [Delete] (@DvdId int)
as

delete DVDs
where DvdId = @DvdId
go