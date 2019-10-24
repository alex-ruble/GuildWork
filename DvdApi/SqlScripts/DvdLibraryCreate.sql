create database DvdLibraryApp
go

create table DVDs (
	DvdId int primary key identity,
	Title nvarchar(100) not null,
	RealeaseYear int not null,
	Director nvarchar(50) not null,
	Rating nvarchar(10) not null,
	Notes nvarchar(max) not null
);

select*
from DVDs